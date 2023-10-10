using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class MerchantFilterProvider : ProductFilterProviderBase
    {
        private readonly IFilterService _filterService;
        public MerchantFilterProvider(IFilterService filterService, IMemoryCache cache, ProductSummaryService productSummaryService) : base(productSummaryService, cache)
        {
            _filterService = filterService;
        }

        public override async Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter)
        {
            var productsToFilter = await base.GetProductSummariesByCategory(categoryId);
            var merchants = filter.FilterOptions.Where(x => x.Value).Select(x => x.Key).ToList();
            if (merchants.Count == 0) return productsToFilter;

            var products = productsToFilter.Where(x => merchants.Any(y => y.Equals(x.MerchantName, StringComparison.OrdinalIgnoreCase))).ToList();
            return products;
        }

        public override async Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId)
        {
            var merchants = await _filterService.GetMerchants(categoryId);
            return new Dictionary<string, bool>(merchants.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new KeyValuePair<string, bool>(x, false)));
        }
    }
}
