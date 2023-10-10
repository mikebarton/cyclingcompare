using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries
{
    public interface IProductSummaryService
    {
        Task<IList<ProductSummary>> GetPagedByCategory(int pageSize, int pageNum, api.Constants.SortOrder sortOrder, string categoryId);
        Task<long> GetProductCountByCategory(string categoryId);
        Task<List<ProductSummary>> GetTopDealsByCategoryId(int categoryId);
        Task<List<ProductSummary>> GetByCategoryId(string categoryId);
        Task<List<ProductSummary>> GetByKeywords(string keywords);
    }
}
