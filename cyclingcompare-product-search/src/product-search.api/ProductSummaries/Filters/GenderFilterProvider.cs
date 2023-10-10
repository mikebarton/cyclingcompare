using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class GenderFilterProvider : ProductFilterProviderBase
    {
        private readonly IFilterService _filterService;
        public GenderFilterProvider(IFilterService filterService, IMemoryCache cache, ProductSummaryService productSummaryService) : base(productSummaryService, cache)
        {
            _filterService = filterService;
        }

        public override async Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter)
        {
            var productsToFilter = await base.GetProductSummariesByCategory(categoryId);
            var textItems = filter.FilterOptions.Where(x => x.Value).Select(x => x.Key).ToList();
            if (textItems.Count == 0) return productsToFilter;

            var translations = await _filterService.GetFilterTranslations(categoryId, "GEN", textItems);

            var products = productsToFilter.Where(x => translations.Any(y => y.Equals(x.Gender, StringComparison.OrdinalIgnoreCase))).ToList();
            return products;
        }

        public override async Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId)
        {
            var genders = await _filterService.GetGenders(categoryId, categoryFilterGroupId);
            return new Dictionary<string, bool>(genders.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new KeyValuePair<string, bool>(x, false)));
        }
    }
}
