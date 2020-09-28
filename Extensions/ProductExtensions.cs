using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Extensions
{
	public static class ProductExtensions
	{
		public static bool RequiresShipping(this Product product)
		{
			return product.Type != ProductType.Physical;
		}
	}
}