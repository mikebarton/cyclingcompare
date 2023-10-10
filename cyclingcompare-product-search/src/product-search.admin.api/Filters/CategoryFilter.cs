using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Filters
{
    public class CategoryFilter
    {
        public int? CategoryFilterId { get; set; }
        public string FilterTypeCode { get; set; }
        public string CategoryId { get; set; }
        public bool IsEnabled { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int CategoryFilterGroupId { get; set; }
    }

    public struct FilterIds
    {
        public const string PriceFilter = "price-filter";
        public const string BrandFilter = "brand-filter";
        public const string MerchantFilter = "merchant-filter";
        public const string SizeFilter = "size-filter";
        public const string GenderFilter = "gender-filter";
        public const string ColourFilter = "colour-filter";
    }
}
