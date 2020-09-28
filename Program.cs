using System;
using System.Collections.Generic;
using System.Linq;
using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Interfaces;
using Gluh.TechnicalTest.Models;


namespace Gluh.TechnicalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Purchase Requirements from test data
            TestData testData = new TestData();
            var purchaseRequirements = testData.Create();

            // Initialize Optimizer - Object to be implemented
            IPurchaseOptimizer purchaseOptimizer = new PurchaseOptimizer(testData.Suppliers);

            // Optimize - Calculates the optimal set of suppliers to purchase products from
            var purchaseOrders = purchaseOptimizer.Optimize(purchaseRequirements);

            WriteToConsole(purchaseOrders);
        }

        private static void WriteToConsole(IEnumerable<PurchaseOrder> purchaseOrders)
        {
            Console.WriteLine("Optimized Purchase Order Results:");
            foreach (var purchaseOrder in purchaseOrders)
            {
                Console.WriteLine($"\nSupplier: {purchaseOrder.Supplier.Name}");
                Console.WriteLine("Items:");
                purchaseOrder.PurchaseItems.ForEach(item => Console.WriteLine($"{item.Quantity} X [{item.Product.ID}] {item.Product.Name} @ {item.Price:C}"));
                Console.WriteLine($"SubTotal: $ {purchaseOrder.SubTotal:C}");
                Console.WriteLine($"Shipping: $ {purchaseOrder.Shipping:C}");
                Console.WriteLine($"   Total: $ {purchaseOrder.Total:C}\n");
            }
            Console.WriteLine($"\nAll ({purchaseOrders.Count()}) Orders Total: ${purchaseOrders.Sum(x => x.Total):C}");
        }
    }
}
