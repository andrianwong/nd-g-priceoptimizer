using System.Collections.Generic;
using Gluh.TechnicalTest.Models;

namespace Gluh.TechnicalTest.Interfaces
{
    public interface IPurchaseOptimizer
    {
        /// <summary>
        /// Calculates the optimal set of supplier to purchase products from.
        /// </summary>
        public IEnumerable<PurchaseOrder> Optimize(IEnumerable<PurchaseRequirement> purchaseRequirements);
    }
}
