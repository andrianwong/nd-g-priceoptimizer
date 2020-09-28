using System.Collections.Generic;
using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Models;

/// <summary>
/// Represents a supplier's ability to fulfill requirements
/// </summary>
public class FulfillmentCapability
{
	public Supplier Supplier { get; }
	public IEnumerable<FulfillmentProduct> AvailableProducts { get; }
	public IEnumerable<PurchaseRequirement> ProductsWithInsufficientStock { get; }
	public IEnumerable<PurchaseRequirement> UnrecognizedProducts { get; }
	public decimal SubTotal { get; }
	public decimal ShippingCost { get; }
	public decimal TotalCost => SubTotal + ShippingCost;

	public FulfillmentCapability(Supplier supplier, IEnumerable<FulfillmentProduct> availableProducts, IEnumerable<PurchaseRequirement> productsWithInsufficientStock, IEnumerable<PurchaseRequirement> unrecognizedProducts,
								decimal subTotal, decimal shippingCost)
	{
		Supplier = supplier;
		AvailableProducts = availableProducts;
		ProductsWithInsufficientStock = productsWithInsufficientStock;
		UnrecognizedProducts = unrecognizedProducts;
		SubTotal = subTotal;
		ShippingCost = shippingCost;
	}
}