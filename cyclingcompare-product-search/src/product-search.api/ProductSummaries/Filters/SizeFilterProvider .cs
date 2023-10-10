using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class SizeFilterProvider : ProductFilterProviderBase
    {
        private readonly IFilterService _filterService;
        public SizeFilterProvider(IFilterService filterService, IMemoryCache cache, ProductSummaryService productSummaryService) : base(productSummaryService, cache)
        {
            _filterService = filterService;
        }

        public override async Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter)
        {
            var productsToFilter = await base.GetProductSummariesByCategory(categoryId);
            var textItems = filter.FilterOptions.Where(x => x.Value).Select(x => x.Key).ToList();
            if (textItems.Count == 0) return productsToFilter;

            var translations = await _filterService.GetFilterTranslations(categoryId, "SIZ", textItems);

            var products = productsToFilter.Where(x => !string.IsNullOrWhiteSpace(x.Size) && translations.Any(y => Regex.IsMatch(x.Size, y))).ToList();
            return products;
        }

        public override async Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId)
        {
            var sizes = await _filterService.GetSizes(categoryId, categoryFilterGroupId);
            return new Dictionary<string, bool>(sizes.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new KeyValuePair<string, bool>(x, false)));
        }
    }
}
