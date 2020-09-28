using System.Linq;
using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Models;

namespace Gluh.TechnicalTest.Extensions
{
	// todo: unused class should be deleted
	public static class PurchaseOrderExtensions
	{
		public static bool RequiresShipping(this PurchaseOrder purchaseOrder)
		{
			return purchaseOrder.PurchaseItems.Any(item => ProductExtensions.RequiresShipping(item.Product));
		}

		public static decimal DetermineShippingCost(this PurchaseOrder purchaseOrder)
		{
			Supplier supplier = purchaseOrder.Supplier;
            
			if(purchaseOrder.SubTotal < supplier.ShippingCostMinOrderValue ||
				purchaseOrder.SubTotal > supplier.ShippingCostMaxOrderValue)
				purchaseOrder.Shipping = 0;
			else
				purchaseOrder.Shipping = supplier.ShippingCost;

			return purchaseOrder.Shipping;
		}
	}
}