using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class ColourFilterProvider : ProductFilterProviderBase
    {
        private readonly IFilterService _filterService;
        public ColourFilterProvider(IFilterService filterService, IMemoryCache cache, ProductSummaryService productSummaryService) : base(productSummaryService, cache)
        {
            _filterService = filterService;
        }

        public override async Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter)
        {
            var productsForCategory = await base.GetProductSummariesByCategory(categoryId);
            var textItems = filter.FilterOptions.Where(x => x.Value).Select(x => x.Key).ToList();
            if (textItems.Count == 0) return productsForCategory;

            var filterTranslations = await _filterService.GetFilterTranslations(categoryId, "COL", textItems);

            var products = productsForCategory.Where(x => filterTranslations.Any(y => y.Equals(x.Colour, StringComparison.OrdinalIgnoreCase))).ToList();
            return products;
        }

        public override async Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId)
        {
            var colours = await _filterService.GetColours(categoryId, categoryFilterGroupId);
            return new Dictionary<string, bool>(colours.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new KeyValuePair<string, bool>(x, false)));
        }
    }
}
