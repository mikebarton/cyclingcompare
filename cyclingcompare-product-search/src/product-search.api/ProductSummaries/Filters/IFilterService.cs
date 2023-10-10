using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public interface IFilterService
    {
        //Task<List<CategoryFilter>> GetCategoryFilters(string categoryId, string filterTypeCode);
        Task<IList<string>> GetFilterTranslations(string categoryId, string filterTypeCode, List<string> filters);
        Task<List<string>> GetBrands(string categoryId);
        Task<List<string>> GetMerchants(string categoryId);
        Task<List<string>> GetSizes(string categoryId, int categoryFilterGroupId);
        Task<List<string>> GetGenders(string categoryId, int categoryFilterGroupId);
        Task<List<string>> GetColours(string categoryId, int categoryFilterGroupId);
        Task<List<Filter>> GetFilterData(string categoryId);
    }
}
