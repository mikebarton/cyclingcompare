using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries
{
    public static class Constants
    {
        public struct FilterIds
        {
            public const string PriceFilter = "price-filter";
            public const string BrandFilter = "brand-filter";
            public const string MerchantFilter = "merchant-filter";
            public const string SizeFilter = "size-filter";
            public const string GenderFilter = "gender-filter";
            public const string ColourFilter = "colour-filter";
        }
        public enum FilterType
        {
            RangeNum,
            SelectMany
        }

        
    }
}
