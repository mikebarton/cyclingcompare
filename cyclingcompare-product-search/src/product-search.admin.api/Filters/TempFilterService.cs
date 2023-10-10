using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using product_search.admin.api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.ProductSummaries.Filters
{
    public abstract class TempFilterService
    {
        public FilterService FilterService { get; set; }
        public IMemoryCache Cache { get; set;
        }
        public async Task PopulateExistingMappingsToOtherCategories(string filterCode)
        {
            //var filterPairs = await GetFilterPairs(filterCode);

            //foreach (var pair in filterPairs)
            //{
            //    var categoryIds = await GetUnmappedCategoryIdsForTranslation(pair.TranslationName);
            //    foreach (var catId in categoryIds)
            //    {
            //        var catFilters = await Cache.GetOrCreateAsync($"CategoryFilters_{catId}_{filterCode}", async entry=>
            //        {
            //            entry.SetSlidingExpiration(TimeSpan.FromSeconds(15));
            //            return await FilterService.GetFiltersByCategory(catId, filterCode);
            //        });

            //        var relevantFilter = catFilters.SingleOrDefault(x => x.Name.Equals(pair.FilterName));
            //        if (relevantFilter == null)
            //        {
            //            await FilterService.AddOrUpdateFilter(new CategoryFilter { CategoryId = catId, FilterTypeCode = filterCode, Name = pair.FilterName });
            //            var updatedFilters = await FilterService.GetFiltersByCategory(catId, filterCode);
            //            relevantFilter = updatedFilters.Single(x => x.Name.Equals(pair.FilterName));
            //            Cache.Set($"CategoryFilters_{catId}_{filterCode}", updatedFilters);
            //        }

            //        await FilterService.AddOrUpdateFilterTranslation(new CategoryFilterTranslation { CategoryFilterId = relevantFilter.CategoryFilterId.Value, Name = pair.TranslationName });
            //    }
            //}
        }

        [Sql(@"select distinct cf.Name as FilterName, cft.Name as TranslationName
            from CategoryFilter cf
            join CategoryFilterTranslation cft on cf.CategoryFilterId = cft.CategoryFilterId
            where cf.FilterTypeCode = @code;")]
        public abstract Task<List<FilterPair>> GetFilterPairs(string code);

        [Sql(@"select distinct pc.CategoryId
                from ProductCategory pc
                join ProductSummary ps on pc.ProductId = ps.ProductId
                where ps.Gender = @translation and pc.CategoryId not in
                (select CategoryId 
                from CategoryFilter cf
                join CategoryFilterTranslation cft on cf.CategoryFilterId = cft.CategoryFilterId
                where cft.Name = @translation) order by pc.CategoryId;")]
        public abstract Task<List<string>> GetUnmappedCategoryIdsForTranslation(string translation);
        
    }

    public class FilterPair
    {
        public string FilterName { get; set; }
        public string TranslationName { get; set; }
    }
}
