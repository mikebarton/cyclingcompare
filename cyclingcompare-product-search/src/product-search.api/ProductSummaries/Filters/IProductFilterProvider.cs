using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public interface IProductFilterProvider
    {
        Task<IList<ProductSummary>> GetFilteredProducts(string categoryId, FilterViewModel filter);
        Task<Dictionary<string, bool>> GetFilterOptions(string categoryId, int categoryFilterGroupId);
    }
}
