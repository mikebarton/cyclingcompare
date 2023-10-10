using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Filters
{
    public class CategoryFilterModel
    {
        public int CategoryFilterId { get; set; }
        public FilterType FilterTypeCode { get; set; }
        public string CategoryId { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int CategoryFilterGroupId { get; set; }
    }

    public enum FilterType
    {
        Size,
        Colour,
        Gender
    }
}
