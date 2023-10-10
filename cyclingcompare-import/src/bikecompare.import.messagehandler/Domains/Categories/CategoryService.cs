using Insight.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Categories
{
    public abstract class CategoryService
    {
        public IDbConnection Connection { private get; set; }

        [Sql("select distinct p.MerchantId, m.Name as MerchantName, Category as ExternalCategoryName, SubCategory as ExternalSubCategoryName from Product p left join Merchant m on m.MerchantId = p.MerchantId")]
        public abstract Task<IList<ExternalCategory>> GetAllExternalCategoryNames();
        [Sql("select distinct p.MerchantId, m.Name as MerchantName, Category as ExternalCategoryName, SubCategory as ExternalSubCategoryName from Product p left join Merchant m on m.MerchantId = p.MerchantId where p.MerchantId = @merchantId")]
        public abstract Task<IList<ExternalCategory>> GetExternalCategoryNames(string merchantId);

        [Sql("select MerchantId, ExternalCategoryName, ExternalSubCategoryName, MappedCategoryId as CategoryId from CategoryMapping")]
        public abstract Task<IList<CategoryMapping>> GetCategoryMappings();

        [Sql("insert into CategoryMapping (CategoryMappingId, MerchantId, ExternalCategoryName, ExternalSubCategoryName, MappedCategoryId) values (@mappingId, @merchantId, @externalCategory, @externalSubCategory, @categoryId)")]
        public abstract Task AddMapping(string mappingId, string externalCategory, string externalSubCategory, string merchantId, string categoryId);

        [Sql("delete from CategoryMapping where ExternalCategoryName = @externalCategory and ExternalSubCategoryName = @externalSubCategory and MerchantId = @merchantId")]
        public abstract Task DeleteMapping(string externalCategory, string externalSubCategory, string merchantId);
    }
}
