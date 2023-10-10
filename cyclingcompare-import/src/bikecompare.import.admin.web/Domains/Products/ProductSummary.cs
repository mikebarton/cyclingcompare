using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Domains.Products
{
    public class ProductSummary
    {
        public string GlobalProductId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public bool IsReviewed { get; set; }
    }
}
