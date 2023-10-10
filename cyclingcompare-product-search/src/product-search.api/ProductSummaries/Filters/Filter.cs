using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static product_search.api.ProductSummaries.Constants;

namespace product_search.api.ProductSummaries.Filters
{
    public class Filter
    {
        public int CategoryFilterGroupId { get; set; }
        public string FilterId { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string MinLabel { get; set; }
        public string MaxLabel { get; set; }
        public FilterType FilterType { get; set; }         
    }

}
