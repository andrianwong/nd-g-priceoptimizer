using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseItem
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}