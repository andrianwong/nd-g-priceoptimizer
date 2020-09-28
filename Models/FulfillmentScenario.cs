using System.Collections.Generic;

public class FulfillmentScenario
{
	private Dictionary<int, FulfillmentCapability> _supplierFulfillmentCapabilities;
	private decimal _totalCost;

	public decimal TotalCost => _totalCost;
    
	public FulfillmentScenario()
	{
		_supplierFulfillmentCapabilities = new Dictionary<int, FulfillmentCapability>();
	}
    
	/// <summary>
	/// Internal function to add fulfillment capability to list and keep track of total cost for scenario
	/// </summary>
	/// <param name="fulfillmentCapability"></param>
	void add(FulfillmentCapability fulfillmentCapability)
	{
		_supplierFulfillmentCapabilities.Add(fulfillmentCapability.Supplier.ID, fulfillmentCapability);

		_totalCost += fulfillmentCapability.TotalCost;
	}
	
	/// <summary>
	/// Adds supplier fulfillment capability to scenario
	/// </summary>
	/// <param name="fulfillmentCapability"></param>
	/// <exception cref="DuplicateScenarioCapabilityException"></exception>
	public void Add(FulfillmentCapability fulfillmentCapability)
	{
		if(_supplierFulfillmentCapabilities.ContainsKey(fulfillmentCapability.Supplier.ID))
			throw new DuplicateScenarioCapabilityException();
        
		add(fulfillmentCapability);
	}

	/// <summary>
	/// Update supplier fulfillment capability. Used when adding 'insufficient stock' items to existing supplier fulfillment capability (quote)
	/// </summary>
	/// <param name="fulfillmentCapability"></param>
	public void Replace(FulfillmentCapability fulfillmentCapability)
	{
		if(_supplierFulfillmentCapabilities.TryGetValue(fulfillmentCapability.Supplier.ID,
														out FulfillmentCapability original))
		{
			_totalCost -= original.TotalCost;
			_supplierFulfillmentCapabilities.Remove(fulfillmentCapability.Supplier.ID);
		}

		add(fulfillmentCapability);
	}

	/// <summary>
	/// Retrieve FulfillmentCapability of specified supplier
	/// </summary>
	/// <param name="supplier"></param>
	/// <returns></returns>
	public FulfillmentCapability GetCapability(int supplier)
	{
		if( _supplierFulfillmentCapabilities.TryGetValue(supplier, out FulfillmentCapability capability))
			return capability;

		return null;
	}

	public IEnumerable<FulfillmentCapability> SupplierFulfillmentCapabilities => _supplierFulfillmentCapabilities.Values;

	/// <summary>
	/// Add supplier fulfillment capabilities from another scenario
	/// </summary>
	/// <param name="scenario"></param>
	public void Adopt(FulfillmentScenario scenario)
	{
		if(scenario == null)
			return;
        
		foreach(KeyValuePair<int,FulfillmentCapability> fulfillmentCapability in scenario._supplierFulfillmentCapabilities)
		{
			add(fulfillmentCapability.Value);
		}
	}
}