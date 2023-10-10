using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public abstract class ProductFilterProviderBase : IProductFilterProvider
    {
        private readonly ProductSummaryService _productSummaryService;
        private readonly IMemoryCache _cache;

        public ProductFilterProviderBase(ProductSummaryService productSummaryService, IMemoryCache cache)
        {
            _productSummaryService = productSummaryService;
            _cache = cache;
        }

        public abstract Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter);

        public abstract Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId);
        

        protected Task<List<ProductSummary>> GetProductSummariesByCategory(string categoryId)
        {
            return _cache.GetOrCreateAsync($"PRODUCTS_BY_CATEGORY_{categoryId}",
                        entry =>
                        {
                            entry.SetSlidingExpiration(TimeSpan.FromSeconds(30));
                            return _productSummaryService.GetByCategoryId(categoryId);
                        });
        }
    }
}
