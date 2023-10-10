using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Filters
{
    public abstract class FilterService
    {
        public async Task AddOrUpdateFilter(CategoryFilter filter)
        {
            if (filter.CategoryFilterId.HasValue)
            {
                var existing = await GetFilterById(filter.CategoryFilterId.Value);
                if (existing != null)
                {
                    await UpdateFilter(filter);
                    return;
                }

            }
            await CreateFilter(filter);

        }

        public async Task AddOrUpdateFilterTranslation(CategoryFilterTranslation filterTranslation)
        {
            if (filterTranslation.CategoryFilterTranslationId.HasValue)
            {
                var existing = await GetCategoryFilterTranslationsById(filterTranslation.CategoryFilterTranslationId.Value);
                if(existing != null)
                {
                    await UpdateCategoryFilterTranslation(filterTranslation);
                    return;
                }

            }
            await InsertCategoryFilterTranslation(filterTranslation);

        }

        [Sql("SELECT CategoryFilterGroupId, `Name`, CategoryId, FilterType, MinLabel, MaxLabel, `Order`, FilterCode FROM CategoryFilterGroup where CategoryId = @categoryId order by `Order`")]
        public abstract Task<List<CategoryFilterGroup>> GetFilterGroupsByCategoryId(string categoryId);

        [Sql("SELECT CategoryFilterGroupId, `Name`, CategoryId, FilterType, MinLabel, MaxLabel, `Order`, FilterCode FROM CategoryFilterGroup where CategoryFilterGroupId = @categoryFilterGroupId")]
        public abstract Task<CategoryFilterGroup> GetFilterGroupById(int categoryFilterGroupId);
        
        [Sql("INSERT INTO CategoryFilterGroup (`Name`, CategoryId, FilterType, MinLabel, MaxLabel, `Order`, FilterCode) VALUES (@name, @categoryId, @filterType, @minLabel, @maxLabel, @order, @filterCode)")]
        public abstract Task AddCategoryFilterGroup(CategoryFilterGroup group);

        [Sql("update CategoryFilterGroup set `Name` = @name, CategoryId = @categoryId, FilterType = @filterType, MinLabel = @minLabel, MaxLabel = @maxLabel, `Order` = @order, FilterCode = @filterCode where CategoryFilterGroupId = @categoryFilterGroupId")]
        public abstract Task UpdateCategoryFilterGroup(CategoryFilterGroup group);

        [Sql("delete from CategoryFilterGroup where CategoryFilterGroupId = @categoryFilterGroupId")]
        public abstract Task DeleteCategoryFilterGroup(int categoryFilterGroupId);

        [Sql("select CategoryFilterId, FilterTypeCode, CategoryId, IsEnabled, Name, `Order`, CategoryFilterGroupId from CategoryFilter where CategoryId = @categoryId and CategoryFilterGroupId = @filterGroupId order by `Order`")]
        public abstract Task<List<CategoryFilter>> GetFiltersByCategoryAndGroup(string categoryId, int filterGroupId);

        [Sql("select CategoryFilterId, FilterTypeCode, CategoryId, IsEnabled, Name, `Order`, CategoryFilterGroupId from CategoryFilter where CategoryFilterId = @categoryFilterId")]
        public abstract Task<CategoryFilter> GetFilterById(int categoryFilterId);

        [Sql("insert into CategoryFilter (FilterTypeCode, CategoryId, IsEnabled, Name, `Order`, CategoryFilterGroupId) values (@filterTypeCode, @categoryId, @isEnabled, @name, @order, @categoryFilterGroupId)")]
        public abstract Task CreateFilter(CategoryFilter filter);

        [Sql("update CategoryFilter set FilterTypeCode = @filterTypeCode, CategoryId = @categoryId, IsEnabled = @isEnabled, Name = @name, `Order` = @order, CategoryFilterGroupId = @categoryFilterGroupId where CategoryFilterId = @categoryFilterId")]
        public abstract Task UpdateFilter(CategoryFilter filter);

        [Sql("delete from CategoryFilter where CategoryFilterId = @categoryFilterId")]
        public abstract Task DeleteFilter(int categoryFilterId);

        [Sql("delete from CategoryFilterTranslation where CategoryFilterId = @categoryFilterId")]
        public abstract Task DeleteTranslationsByFilterId(int categoryFilterId);

        [Sql("delete from CategoryFilterTranslation where CategoryFilterTranslationId = @categoryFilterTranslationId")]
        public abstract Task DeleteTranslationsById(int categoryFilterTranslationId);

        [Sql("select CategoryFilterTranslationId, CategoryFilterId, Name from CategoryFilterTranslation where CategoryFilterId = @categoryFilterId")]
        public abstract Task<List<CategoryFilterTranslation>> GetCategoryFilterTranslationsByCategoryFilterId(int categoryFilterId);

        [Sql("select CategoryFilterTranslationId, CategoryFilterId, Name from CategoryFilterTranslation where CategoryFilterTranslationId = @categoryFilterTranslationId")]
        public abstract Task<CategoryFilterTranslation> GetCategoryFilterTranslationsById(int categoryFilterTranslationId);

        [Sql("insert into CategoryFilterTranslation (CategoryFilterId, Name) values (@categoryFilterId, @name)")]
        public abstract Task InsertCategoryFilterTranslation(CategoryFilterTranslation translation);

        [Sql("update CategoryFilterTranslation set CategoryFilterId = @categoryFilterId, Name = @name where CategoryFilterTranslationId = @categoryFilterTranslationId")]
        public abstract Task UpdateCategoryFilterTranslation(CategoryFilterTranslation translation);

        [Sql(@"select cft.Name 
		            from CategoryFilter cf 
		            join CategoryFilterTranslation cft on cft.CategoryFilterId = cf.CategoryFilterId
		            where cf.CategoryId = @categoryId and cf.CategoryFilterGroupId = @filterGroupId")]
        public abstract Task<List<string>> GetMappedTranslationsByCategoryIdAndType(string categoryId, int filterGroupId);

        [Sql(@"select distinct mps.Size
                from ProductSummary ps 
                join MerchantProductSummary mps on ps.ProductId = mps.ProductId
                join ProductCategory pc on ps.ProductId = pc.ProductId and pc.CategoryId = @categoryId	            
                join CategoryFilter cf on cf.CategoryId = pc.CategoryId and cf.FilterTypeCode = 'SIZ' and cf.CategoryFilterGroupId = @filterGroupId
                where mps.Size is not null and mps.Size != ''                 
                order by mps.Size")]
        public abstract Task<List<string>> GetAllSizeFilters(string categoryId, int filterGroupId);

        [Sql(@"select distinct mps.Colour
                from ProductSummary ps 
                join MerchantProductSummary mps on ps.ProductId = mps.ProductId
                join ProductCategory pc on ps.ProductId = pc.ProductId and pc.CategoryId = @categoryId	            
                join CategoryFilter cf on cf.CategoryId = pc.CategoryId and cf.FilterTypeCode = 'COL' and cf.CategoryFilterGroupId = @filterGroupId
                where mps.Colour is not null and mps.Colour != ''                 
                order by mps.Colour")]
        public abstract Task<List<string>> GetAllColourFilters(string categoryId, int filterGroupId);

        [Sql(@"select distinct mps.Gender
                from ProductSummary ps 
                join MerchantProductSummary mps on ps.ProductId = mps.ProductId
                join ProductCategory pc on ps.ProductId = pc.ProductId and pc.CategoryId = @categoryId	            
                join CategoryFilter cf on cf.CategoryId = pc.CategoryId and cf.FilterTypeCode = 'GEN' and cf.CategoryFilterGroupId = @filterGroupId
                where mps.Gender is not null and mps.Gender != ''                 
                order by mps.Gender")]
        public abstract Task<List<string>> GetAllGenderFilters(string categoryId, int filterGroupId);

    }
}
