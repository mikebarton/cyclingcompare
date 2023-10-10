using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class CategoryFilter
    {
        public int CategoryFilterId { get; set; }
        public string Name { get; set; }
        public string FilterTypeCode { get; set; }
        public int Order { get; set; }
        public string CategoryId { get; set; }
    }
}
