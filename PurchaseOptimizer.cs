using System;
using System.Collections.Generic;
using System.Linq;
using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Interfaces;
using Gluh.TechnicalTest.Models;

public class PurchaseOptimizer : IPurchaseOptimizer
{
	private readonly IDictionary<int, Supplier> _suppliers;

	public PurchaseOptimizer(IEnumerable<Supplier> suppliers)
	{
		_suppliers = suppliers?.ToDictionary(s => s.ID, s => s) 
					?? throw new ArgumentNullException(nameof(suppliers));
	}
        
	// todo: function name should be changed to better reflect purpose
	private IEnumerable<ProductStock> DetermineProductSupplierPreference(PurchaseRequirement requirement)
	{
		return
			requirement.Product.Stock?.Where(
								supplierStock => supplierStock.StockOnHand < requirement.Quantity
							)
						.OrderBy(
								supplier => supplier.Cost
							);

	}

	/// <summary>
	/// Determine supplier ability to fulfill purchase requirements
	/// </summary>
	/// <param name="supplier"></param>
	/// <param name="requirements"></param>
	/// <param name="allowedInsufficientStockProducts"></param>
	/// <returns></returns>
	private FulfillmentCapability DetermineFulfillmentCapability(Supplier supplier, IEnumerable<PurchaseRequirement> requirements, IEnumerable<int> allowedInsufficientStockProducts = null)
	{
		decimal subTotal = 0, shippingCost = 0;
		bool hasPhysicalProducts = false;
		List<FulfillmentProduct> availableProducts = new List<FulfillmentProduct>();
		List<PurchaseRequirement> unavailableProducts = new List<PurchaseRequirement>();
		List<PurchaseRequirement> unrecognizedProducts = new List<PurchaseRequirement>();
        
		foreach(PurchaseRequirement requirement in requirements)
		{
			if(requirement.Product.Stock == null || requirement.Quantity < 1)
				continue;
            
			// determine supplier product stock level
			ProductStock stock = requirement.Product.Stock.Where(stock => stock.Supplier.ID == supplier.ID).FirstOrDefault();

			// If supplier do not sell specific product
			if(stock == null)
			{
				// add to list of unknown products
				unrecognizedProducts.Add(requirement); 
			}
			else // otherwise evaluate availability for physical and non-physical products.
				 // Service is assumed to be always available
			{
				// keep track of whether fulfillment through this supplier may incur shipping cost due to physical product
				hasPhysicalProducts |= requirement.Product.Type == ProductType.Physical;

				decimal cost = stock.Cost * requirement.Quantity;

				// if there is sufficient stock in hand and product is not specified in allowedInsufficientStockProducts
				if(requirement.Product.Type != ProductType.Service && 
					stock.StockOnHand < requirement.Quantity && 
					!((allowedInsufficientStockProducts?.Contains(requirement.Product.ID)) ?? false) 
				)
				{
					unavailableProducts.Add(requirement);
				}
				else
				{
					availableProducts.Add(new FulfillmentProduct() {Requirement = requirement, SupplierStock = stock} );
					subTotal += cost;
				}
			}
		}

		// determine cost of shipping if at least one physical product will be fulfilled by this supplier
		if(hasPhysicalProducts)
		{
			if(subTotal < supplier.ShippingCostMinOrderValue ||
				subTotal > supplier.ShippingCostMaxOrderValue)
				shippingCost = 0;
			else
				shippingCost = supplier.ShippingCost;
            
		}

		return new FulfillmentCapability(supplier, availableProducts, unavailableProducts, unrecognizedProducts, subTotal, shippingCost);
	}

	/// <summary>
	/// This is a recursive function. Each scenario is determined by working out 'fulfillment capabilities' starting at a particular supplier and then recurse through all other suppliers for 'unfulfilled' products 
	/// </summary>
	/// <param name="purchaseRequirements"></param>
	/// <param name="suppliers"></param>
	/// <returns></returns>
	FulfillmentScenario Optimize(IEnumerable<PurchaseRequirement> purchaseRequirements,
								IEnumerable<Supplier> suppliers)
	{
		FulfillmentScenario optimalScenario = null;
        
		// workout a possible scenario starting from one carrier
		// could be done in paralel with Paralel.For 
		foreach(Supplier supplier in suppliers)
		{
			FulfillmentScenario scenario = new FulfillmentScenario();

			FulfillmentCapability fulfillmentCapability = DetermineFulfillmentCapability(supplier, purchaseRequirements);
            
			scenario.Add(fulfillmentCapability);

			// determine requirements current carrier could not fulfill
			IEnumerable<PurchaseRequirement> pendingRequirements = fulfillmentCapability.ProductsWithInsufficientStock.Concat(fulfillmentCapability.UnrecognizedProducts);
            
			// recuse through other carriers if current supplier unable to fulfill all requirements
			if(pendingRequirements.Count() > 0)
			{
				FulfillmentScenario residualFulfillmentScenario = Optimize(pendingRequirements, suppliers.Where( s => s.ID != supplier.ID));
                
				scenario.Adopt(residualFulfillmentScenario);
			}

			// Determine if this scenario is the most optimal and keep track
			if(optimalScenario == null || scenario.TotalCost < optimalScenario.TotalCost)
			{
				optimalScenario = scenario;
			}
		}
        
		return optimalScenario;
	}

	/// <summary>
	/// Optimization works by working out all possible purchase order combination scenarios and then selecting cheapest scenario
	/// </summary>
	/// <param name="purchaseRequirements"></param>
	/// <returns>List of purchase order based on requirements</returns>
	public IEnumerable<PurchaseOrder> Optimize(IEnumerable<PurchaseRequirement> purchaseRequirements)
	{
		/* Assumptions:
            - Optimal purchase orders account for shipping cost of each supplier
            - Each PurchaseRequirement must be fulfilled within one purchase order
            - Only Physical product requires shipping
            - Service type product does not require stock in hand
            - Shipping cost is not applicable if a purchase order does not include any Physical product
            - "Background" specifies "‘Optimal’ in this case means the cheapest set of suppliers that have sufficient stock", which means product without sufficient stock is included in an order based on cheapest price best effort, e.g. through supplier with cheapest cost + shipping difference
            - Product without stock at any supplier should tehnically be returned with list of purchase orders within a different model which is never specified or mentioned anywhere and therefore ignored
            - PurchaseRequirement with quantity '0' is not included in any order
            - Three suppliers had the same ID in the test data, which was assumed to be mistake and changed
        */
		
		// determine best ordering scenario, not taking insufficient quantity product into consideration
		FulfillmentScenario optimalScenario = Optimize(purchaseRequirements, _suppliers.Values);

		if(optimalScenario == null)
			return null;

		# region Include products with insufficient stock in one order
        
		// determine list of products where no supplier have sufficient stock
		FulfillmentCapability scenarioLastSupplier = optimalScenario.SupplierFulfillmentCapabilities.Last();
		IEnumerable<PurchaseRequirement> productsWithInsufficientStock =
			scenarioLastSupplier.ProductsWithInsufficientStock.Concat(scenarioLastSupplier.UnrecognizedProducts);

		if(productsWithInsufficientStock.Any())
		{
			Dictionary<int, IEnumerable<ProductStock>> productSupplierPreferences =
				new Dictionary<int, IEnumerable<ProductStock>>(productsWithInsufficientStock.Count());

			// determine product supplier preference by cheapest price
			foreach(PurchaseRequirement requirement in productsWithInsufficientStock)
			{
				productSupplierPreferences.Add(requirement.Product.ID, DetermineProductSupplierPreference(requirement));
			}
            
			foreach(PurchaseRequirement requirement in productsWithInsufficientStock)
			{
				// keep track of which supplier to use and how much extra cost will be incurred
				FulfillmentCapability preferredSupplierCapability = null;
				decimal preferredSupplierCostDifference = 0;

				// Attempt to determine best supplier in order of price preference
				foreach(var supplierStock in productSupplierPreferences[requirement.Product.ID])
				{
					// retrieve existing fulfillment capability (let's call it quote) from optimal scenario
					FulfillmentCapability usedSupplierCapability =
						optimalScenario.GetCapability(supplierStock.Supplier.ID);
                    
					// if supplier is not already part of 'optimal' scenario
					if(usedSupplierCapability == null)
					{
						FulfillmentCapability supplierCapability =
							DetermineFulfillmentCapability(_suppliers[supplierStock.Supplier.ID], 
															new[] {requirement},
															new []{requirement.Product.ID});
						
						// set determined supplier as preferred if current preferred supplier costs more
						if(preferredSupplierCapability == null || preferredSupplierCostDifference >
							supplierCapability.TotalCost)
						{
							preferredSupplierCapability = supplierCapability;
							preferredSupplierCostDifference = supplierCapability.TotalCost;
						}
					}
					else // if preferred supplier is already part of 'optimal' scenario, i.o.w 'quote' already exists
					{
						IEnumerable<PurchaseRequirement> supplierNewRequirements = 
							new[] {requirement}
							.Concat(usedSupplierCapability.AvailableProducts.Select(p => p.Requirement));
						
						FulfillmentCapability supplierCapability =
							DetermineFulfillmentCapability(_suppliers[supplierStock.Supplier.ID], 
															supplierNewRequirements, 
															new []{requirement.Product.ID});
						
						// determine difference in cost from previous 'quote'
						decimal usedSupplierCapabilityCostDifference =
							supplierCapability.TotalCost - usedSupplierCapability.TotalCost;
                        
						// set determined supplier as preferred if current preferred supplier costs more
						if(preferredSupplierCapability == null ||
							usedSupplierCapabilityCostDifference <
							preferredSupplierCostDifference)
						{
							preferredSupplierCapability = supplierCapability;
							preferredSupplierCostDifference = usedSupplierCapabilityCostDifference;
						}
					}

				}

				optimalScenario.Replace(preferredSupplierCapability);
			}
		}
        
		#endregion

		List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>(optimalScenario.SupplierFulfillmentCapabilities.Count());

		// create purchase orders
		foreach(var fulfillmentCapability in optimalScenario.SupplierFulfillmentCapabilities)
		{
			if(!fulfillmentCapability.AvailableProducts.Any())
				continue;
			
			// create list of items within purchase order
			List<PurchaseItem> purchaseItems = new List<PurchaseItem>(
					// PurchaseItem.Price is assumed to be of individual item instead of total
					fulfillmentCapability.AvailableProducts.Select(
							p => new PurchaseItem()
							{
								Product = p.Requirement.Product, Price = p.SupplierStock.Cost,
								Quantity = p.Requirement.Quantity
							}
						)
				);

			PurchaseOrder order = new PurchaseOrder()
			{
				Supplier = fulfillmentCapability.Supplier,
				PurchaseItems = purchaseItems,
				SubTotal = fulfillmentCapability.SubTotal,
				Shipping = fulfillmentCapability.ShippingCost,
				Total = fulfillmentCapability.TotalCost
			};
            
			purchaseOrders.Add(order);
		}

		return purchaseOrders;
	}
}