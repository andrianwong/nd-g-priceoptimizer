using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Models;

public class FulfillmentProduct
{
	public PurchaseRequirement Requirement { get; set; }
    
	public ProductStock SupplierStock { get; set; }
}