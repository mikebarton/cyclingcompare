using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Filters
{
    public class CategoryFilterGroup
    {
        public int CategoryFilterGroupId { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public FilterSelectionType FilterType { get; set; }
        public string MinLabel { get; set; }
        public string MaxLabel { get; set; }
        public int Order { get; set; }
        public string FilterCode { get; set; }
    }

    public enum FilterSelectionType
    {
        RangeNum,
        SelectMany
    }
}
