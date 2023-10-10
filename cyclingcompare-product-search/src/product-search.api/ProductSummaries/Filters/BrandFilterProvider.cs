using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class BrandFilterProvider : ProductFilterProviderBase
    {
        private readonly IFilterService _filterService;
        public BrandFilterProvider(IFilterService filterService, IMemoryCache cache, ProductSummaryService productSummaryService) : base(productSummaryService, cache)
        {
            _filterService = filterService;
        }

        public override async Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter)
        {
            var productsToFilter = await base.GetProductSummariesByCategory(categoryId);
            var brands = filter.FilterOptions.Where(x => x.Value).Select(x => x.Key).ToList();
            if (brands.Count == 0) return productsToFilter;

            var products = productsToFilter.Where(x => brands.Any(y => y.Equals(x.Brand, StringComparison.OrdinalIgnoreCase))).ToList();
            return products;
        }

        public override async Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId)
        {
            var brands = await _filterService.GetBrands(categoryId);
            return new Dictionary<string, bool>(brands.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new KeyValuePair<string, bool>(x, false)));
        }
    }
}
