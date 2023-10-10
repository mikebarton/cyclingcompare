using Insight.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.ProductSummaries.Filters
{
    public abstract partial class FilterService : IFilterService
    {        

        public async Task<IList<string>> GetFilterTranslations(string categoryId, string filterTypeCode, List<string> filters)
        {
            using var connection = await GetConnection().OpenConnectionAsync();
            await connection.ExecuteSqlAsync(@"CREATE TEMPORARY TABLE FILTERS_TO_USE(                                                
                                                    Name text
                                                )");

            foreach (var filter in filters)
            {
                await connection.ExecuteAsync("insert into FILTERS_TO_USE (Name) values (@filter)", new { filter }, CommandType.Text);
            }
            
            var translations = await connection.QueryAsync<string>(@"select cft.Name
                                                    from CategoryFilter cf
                                                    join FILTERS_TO_USE ftu on cf.Name = ftu.Name
                                                    join CategoryFilterTranslation cft on cf.CategoryFilterId = cft.CategoryFilterId
                                                    where cf.CategoryId = @categoryId and cf.FilterTypeCode = @filterTypeCode",
                                                    new { categoryId, filterTypeCode }, CommandType.Text);
            await connection.ExecuteAsync("DROP TEMPORARY TABLE FILTERS_TO_USE", commandType: CommandType.Text);

            return translations;
        }        

        [Sql(GetBrandsStatement)]
        public abstract Task<List<string>> GetBrands(string categoryId);

        [Sql(GetMerchantsStatement)]
        public abstract Task<List<string>> GetMerchants(string categoryId);

        [Sql(GetSizesStatement)]
        public abstract Task<List<string>> GetSizes(string categoryId, int categoryFilterGroupId);

        [Sql(GetGendersStatement)]
        public abstract Task<List<string>> GetGenders(string categoryId, int categoryFilterGroupId);

        [Sql(GetColoursStatement)]
        public abstract Task<List<string>> GetColours(string categoryId, int categoryFilterGroupId);

        [Sql(@"select CategoryFilterGroupId, FilterCode as FilterId, FilterType, `Name`, `Order`, MinLabel, MaxLabel from CategoryFilterGroup cfg where cfg.CategoryId = @categoryId order by `Order`")]
        public abstract Task<List<Filter>> GetFilterData(string categoryId);

        protected abstract IDbConnection GetConnection();
    }
}
