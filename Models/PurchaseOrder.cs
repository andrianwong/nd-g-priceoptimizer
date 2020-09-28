using System;
using System.Collections.Generic;
using System.Text;
using Gluh.TechnicalTest.Database;

namespace Gluh.TechnicalTest.Models
{
    public class PurchaseOrder
    {
        public Supplier Supplier { get; set; }

        public List<PurchaseItem> PurchaseItems { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Shipping { get; set; }

        public decimal Total { get; set; }
    }
}
