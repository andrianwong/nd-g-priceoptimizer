using System;
using System.Collections.Generic;
using System.Text;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest
{
    public class TestData
    {
        private List<Supplier> _suppliers = new List<Supplier>();

        public IEnumerable<Supplier> Suppliers => _suppliers;
        
        public List<PurchaseRequirement> Create()
        {
            var result = new List<PurchaseRequirement>();

            var supplier1 = new Supplier
            {
                ID = 1,
                Name = "Synnex",
                ShippingCost = 10,
                ShippingCostMinOrderValue = 150,
                ShippingCostMaxOrderValue = 1000
            };

            var supplier2 = new Supplier
            {
                ID = 2,
                Name = "Ingram Micro",
                ShippingCost = 0,
                ShippingCostMinOrderValue = 0,
                ShippingCostMaxOrderValue = 0
            };

            var supplier3 = new Supplier
            {
                ID = 3,
                Name = "Tech Data",
                ShippingCost = 10,
                ShippingCostMinOrderValue = 150,
                ShippingCostMaxOrderValue = 1000
            };

            var supplier4 = new Supplier
            {
                ID = 4,
                Name = "Multimedia Technology",
                ShippingCost = 10,
                ShippingCostMinOrderValue = 150,
                ShippingCostMaxOrderValue = 1000
            };

            var supplier5 = new Supplier
            {
                ID = 5,
                Name = "Dicker Data",
                ShippingCost = 9,
                ShippingCostMinOrderValue = 550,
                ShippingCostMaxOrderValue = 99999
            };

            var supplier6 = new Supplier
            {
                ID = 6,
                Name = "Leader",
                ShippingCost = 20,
                ShippingCostMinOrderValue = 0,
                ShippingCostMaxOrderValue = 500
            };

            var supplier7 = new Supplier
            {
                ID = 17,
                Name = "Tech 7",
                ShippingCost = 200,
                ShippingCostMinOrderValue = 50,
                ShippingCostMaxOrderValue = 10000
            };

            var supplier8 = new Supplier
            {
                ID = 11,
                Name = "AData International",
                ShippingCost = 50,
                ShippingCostMinOrderValue = 0,
                ShippingCostMaxOrderValue = 1000
            };

            var supplier9 = new Supplier
            {
                ID = 9,
                Name = "Vince Co",
                ShippingCost = 25,
                ShippingCostMinOrderValue = 50,
                ShippingCostMaxOrderValue = 500
            };

            var supplier10 = new Supplier
            {
                ID = 10,
                Name = "CN Licensing",
                ShippingCost = 149,
                ShippingCostMinOrderValue = 0,
                ShippingCostMaxOrderValue = 25000
            };
            
            _suppliers.Add(supplier1);
            _suppliers.Add(supplier2);
            _suppliers.Add(supplier3);
            _suppliers.Add(supplier4);
            _suppliers.Add(supplier5);
            _suppliers.Add(supplier6);
            _suppliers.Add(supplier7);
            _suppliers.Add(supplier8);
            _suppliers.Add(supplier9);
            _suppliers.Add(supplier10);

            var product1 = new Product
            {
                ID = 1,
                Name = "Google Pixel 3 Phone",
                Type = ProductType.Physical,
            };

            var product2 = new Product
            {
                ID = 2,
                Name = "Lenovo X1 Carbon Laptop",
                Type = ProductType.Physical
            };

            var product3 = new Product
            {
                ID = 3,
                Name = "Microsoft Office 365 Business Premium",
                Type = ProductType.NonPhysical
            };

            var product4 = new Product
            {
                ID = 4,
                Name = "Professional Services - 1 hour",
                Type = ProductType.Service
            };

            var product5 = new Product
            {
                ID = 5,
                Name = "Logitech MK450 Wireless Keyboard and Mouse",
                Type = ProductType.Physical
            };

            var product6 = new Product
            {
                ID = 6,
                Name = "HP 27\" LCD LED Professional Series Monitor",
                Type = ProductType.Physical
            };

            var product7 = new Product
            {
                ID = 7,
                Name = "Symantec Antivius Pro Plus Corporate Edition",
                Type = ProductType.NonPhysical
            };

            var product8 = new Product
            {
                ID = 8,
                Name = "Netgear Nighthawk NH100X Wireless Router",
                Type = ProductType.Physical
            };

            var product9 = new Product
            {
                ID = 9,
                Name = "C9200L DNA Essentials License",
                Type = ProductType.NonPhysical
            };

            var product10 = new Product
            {
                ID = 10,
                Name = "OEM 18RU Rack Black - w/ Lock, Pedestal",
                Type = ProductType.Physical
            };

            var product11 = new Product
            {
                ID = 11,
                Name = "OEM 42RU Rack Black - w/ Lock, Pedestal",
                Type = ProductType.Physical
            };

            var product12 = new Product
            {
                ID = 12,
                Name = "40mm * 5mm 11 Blade Silent Fan 3pin",
                Type = ProductType.Physical
            };

            var product13 = new Product
            {
                ID = 13,
                Name = "Luxe 13 Notebook Bag BLUE",
                Type = ProductType.Physical
            };

            var product14 = new Product
            {
                ID = 14,
                Name = "Luxe 15 Notebook Bag RED",
                Type = ProductType.Physical
            };

            product1.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 1,
                    Cost = 940.56m,
                    StockOnHand = 105,
                    Supplier = supplier1,
                    Product = product1
                },
                new ProductStock
                {
                    ID = 2,
                    Cost = 918.10m,
                    StockOnHand = 2,
                    Supplier = supplier2,
                    Product = product1
                },
                new ProductStock
                {
                    ID = 3,
                    Cost = 935.40m,
                    StockOnHand = 15,
                    Supplier = supplier4,
                    Product = product1
                },
                new ProductStock
                {
                    ID = 4,
                    Cost = 950.00m,
                    StockOnHand = 1,
                    Supplier = supplier9,
                    Product = product1
                }
            };

            product2.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 4,
                    Cost = 1509.49m,
                    StockOnHand = 1,
                    Supplier = supplier2,
                    Product = product2
                },
                new ProductStock
                {
                    ID = 5,
                    Cost = 1489.80m,
                    StockOnHand = 13,
                    Supplier = supplier4,
                    Product = product2
                },
                new ProductStock
                {
                    ID = 6,
                    Cost = 1492.50m,
                    StockOnHand = 15,
                    Supplier = supplier5,
                    Product = product2
                }
            };

            product3.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 7,
                    Cost = 89.91m,
                    StockOnHand = 60,
                    Supplier = supplier1,
                    Product = product3
                },
                new ProductStock
                {
                    ID = 8,
                    Cost = 80.42m,
                    StockOnHand = 60,
                    Supplier = supplier3,
                    Product = product3
                },
                new ProductStock
                {
                    ID = 9,
                    Cost = 71.56m,
                    StockOnHand = 60,
                    Supplier = supplier6,
                    Product = product3
                },
                new ProductStock
                {
                    ID = 1010,
                    Cost = 70.00m,
                    StockOnHand = 0,
                    Supplier = supplier9,
                    Product = product3
                },
                new ProductStock
                {
                    ID = 10,
                    Cost = 69.90m,
                    StockOnHand = 900,
                    Supplier = supplier10,
                    Product = product3
                }
            };

            product4.Stock = default;

            product5.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 10,
                    Cost = 45.49m,
                    StockOnHand = 1405,
                    Supplier = supplier3,
                    Product = product5
                },
                new ProductStock
                {
                    ID = 11,
                    Cost = 46.50m,
                    StockOnHand = 120,
                    Supplier = supplier1,
                    Product = product5
                },
                new ProductStock
                {
                    ID = 12,
                    Cost = 44.50m,
                    StockOnHand = 2,
                    Supplier = supplier6,
                    Product = product5
                },
                new ProductStock
                {
                    ID = 13,
                    Cost = 45.40m,
                    StockOnHand = 130,
                    Supplier = supplier4,
                    Product = product5
                }
            };

            product6.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 14,
                    Cost = 242.75m,
                    StockOnHand = 62,
                    Supplier = supplier2,
                    Product = product6
                },
                new ProductStock
                {
                    ID = 15,
                    Cost = 240.69m,
                    StockOnHand = 18,
                    Supplier = supplier4,
                    Product = product6
                },
                new ProductStock
                {
                    ID = 16,
                    Cost = 201.42m,
                    StockOnHand = 2,
                    Supplier = supplier5,
                    Product = product6
                },
                new ProductStock
                {
                    ID = 17,
                    Cost = 243.19m,
                    StockOnHand = 108,
                    Supplier = supplier6,
                    Product = product6
                }
            };

            product7.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 18,
                    Cost = 67.10m,
                    StockOnHand = 0,
                    Supplier = supplier3,
                    Product = product7
                },
                new ProductStock
                {
                    ID = 19,
                    Cost = 68.42m,
                    StockOnHand = 0,
                    Supplier = supplier1,
                    Product = product7
                },
                new ProductStock
                {
                    ID = 20,
                    Cost = 101.42m,
                    StockOnHand = 9999,
                    Supplier = supplier6,
                    Product = product7
                },
                new ProductStock
                {
                    ID = 21,
                    Cost = 70.19m,
                    StockOnHand = 108,
                    Supplier = supplier4,
                    Product = product7
                },
                new ProductStock
                {
                    ID = 22,
                    Cost = 67.05m,
                    StockOnHand = 5,
                    Supplier = supplier10,
                    Product = product7
                }
            };

            product8.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 19,
                    Cost = 340.91m,
                    StockOnHand = 130,
                    Supplier = supplier1,
                    Product = product8
                },
                new ProductStock
                {
                    ID = 29,
                    Cost = 329.14m,
                    StockOnHand = 10,
                    Supplier = supplier3,
                    Product = product8
                },
                new ProductStock
                {
                    ID = 21,
                    Cost = 301.42m,
                    StockOnHand = 5,
                    Supplier = supplier4,
                    Product = product8
                },
                new ProductStock
                {
                    ID = 22,
                    Cost = 319.00m,
                    StockOnHand = 409,
                    Supplier = supplier5,
                    Product = product8
                }
            };

            product9.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 207,
                    Cost = 277.00m,
                    StockOnHand = 0,
                    Supplier = supplier1,
                    Product = product9
                },
                new ProductStock
                {
                    ID = 209,
                    Cost = 284.14m,
                    StockOnHand = 12,
                    Supplier = supplier3,
                    Product = product9
                },
                new ProductStock
                {
                    ID = 210,
                    Cost = 284.15m,
                    StockOnHand = 4,
                    Supplier = supplier10,
                    Product = product9
                }
            };

            product10.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 35,
                    Cost = 715.91m,
                    StockOnHand = 40,
                    Supplier = supplier1,
                    Product = product10
                },
                new ProductStock
                {
                    ID = 36,
                    Cost = 680.05m,
                    StockOnHand = 60,
                    Supplier = supplier7,
                    Product = product10
                }
            };

            product11.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 40,
                    Cost = 1202.50m,
                    StockOnHand = 1,
                    Supplier = supplier1,
                    Product = product11
                },
                new ProductStock
                {
                    ID = 45,
                    Cost = 1005.30m,
                    StockOnHand = 115,
                    Supplier = supplier7,
                    Product = product11
                },
                new ProductStock
                {
                    ID = 46,
                    Cost = 1350.00m,
                    StockOnHand = 5,
                    Supplier = supplier9,
                    Product = product11
                }
            };

            product12.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 47,
                    Cost = 9.44m,
                    StockOnHand = 40,
                    Supplier = supplier2,
                    Product = product12
                },
                new ProductStock
                {
                    ID = 48,
                    Cost = 9.09m,
                    StockOnHand = 440,
                    Supplier = supplier4,
                    Product = product12
                },
                new ProductStock
                {
                    ID = 49,
                    Cost = 8.40m,
                    StockOnHand = 11,
                    Supplier = supplier6,
                    Product = product12
                },
                new ProductStock
                {
                    ID = 50,
                    Cost = 10.55m,
                    StockOnHand = 120,
                    Supplier = supplier7,
                    Product = product12
                },
                new ProductStock
                {
                    ID = 51,
                    Cost = 17.85m,
                    StockOnHand = 20,
                    Supplier = supplier9,
                    Product = product12
                },
                new ProductStock
                {
                    ID = 52,
                    Cost = 8.35m,
                    StockOnHand = 3,
                    Supplier = supplier3,
                    Product = product12
                },
            };


            product13.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 99,
                    Cost = 28.15m,
                    StockOnHand = 20,
                    Supplier = supplier8,
                    Product = product13
                },
                new ProductStock
                {
                    ID = 100,
                    Cost = 27.10m,
                    StockOnHand = 2,
                    Supplier = supplier9,
                    Product = product13
                }
            };


            product14.Stock = new List<ProductStock>
            {
                new ProductStock
                {
                    ID = 184,
                    Cost = 48.90m,
                    StockOnHand = 4,
                    Supplier = supplier8,
                    Product = product14
                },
                new ProductStock
                {
                    ID = 185,
                    Cost = 34.30m,
                    StockOnHand = 1,
                    Supplier = supplier9,
                    Product = product14
                },
                new ProductStock
                {
                    ID = 186,
                    Cost = 34.90m,
                    StockOnHand = 1,
                    Supplier = supplier3,
                    Product = product14
                },
                new ProductStock
                {
                    ID = 187,
                    Cost = 37.70m,
                    StockOnHand = 1,
                    Supplier = supplier6,
                    Product = product14
                }
            };

            result.AddRange(new List<Models.PurchaseRequirement> {
                new PurchaseRequirement
                {
                    Product = product1,
                    Quantity = 4
                },
                new PurchaseRequirement
                {
                    Product = product2,
                    Quantity = 8
                },
                new PurchaseRequirement
                {
                    Product = product3,
                    Quantity = 50
                },
                new PurchaseRequirement
                {
                    Product = product4,
                    Quantity = 40
                },
                new PurchaseRequirement
                {
                    Product = product5,
                    Quantity = 50
                },
                new PurchaseRequirement
                {
                    Product = product6,
                    Quantity = 25
                },
                new PurchaseRequirement
                {
                    Product = product7,
                    Quantity = 10
                },
                new PurchaseRequirement
                {
                    Product = product8,
                    Quantity = 4
                },
                new PurchaseRequirement
                {
                    Product = product9,
                    Quantity = 2
                },
                new PurchaseRequirement
                {
                    Product = product10,
                    Quantity = 1
                },
                new PurchaseRequirement
                {
                    Product = product11,
                    Quantity = 1
                },
                new PurchaseRequirement
                {
                    Product = product12,
                    Quantity = 5
                },
                new PurchaseRequirement
                {
                    Product = product13,
                    Quantity = 0
                },
                new PurchaseRequirement
                {
                    Product = product14,
                    Quantity = 3
                },
            });

            return result;
        }
    }
}
