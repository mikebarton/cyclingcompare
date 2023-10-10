using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public class CachedFilterService : IFilterService
    {
        private readonly IMemoryCache _cache;
        private readonly FilterService _filterService;
        public CachedFilterService(IMemoryCache cache, FilterService filterService)
        {
            _cache = cache;
            _filterService = filterService;
        }

        public Task<List<string>> GetBrands(string categoryId)
        {
            return _filterService.GetBrands(categoryId);
        }

        public Task<List<string>> GetColours(string categoryId, int categoryFilterGroupId)
        {
            return _filterService.GetColours(categoryId,  categoryFilterGroupId);
        }

        public Task<List<Filter>> GetFilterData(string categoryId)
        {
            return _cache.GetOrCreateAsync($"FILTERS_BY_CATEGORY_{categoryId}",
                        entry =>
                        {
                            entry.SetSlidingExpiration(TimeSpan.FromSeconds(10));
                            return _filterService.GetFilterData(categoryId);
                        });
        }       

        public Task<IList<string>> GetFilterTranslations(string categoryId, string filterTypeCode, List<string> filters)
        {
            return _filterService.GetFilterTranslations(categoryId, filterTypeCode, filters);
        }

        public Task<List<string>> GetGenders(string categoryId, int categoryFilterGroupId)
        {
            return _filterService.GetGenders(categoryId,  categoryFilterGroupId);
        }

        public Task<List<string>> GetMerchants(string categoryId)
        {
            return _filterService.GetMerchants(categoryId);
        }

        public Task<List<string>> GetSizes(string categoryId, int categoryFilterGroupId)
        {
            return _filterService.GetSizes(categoryId,  categoryFilterGroupId);
        }
    }
}
