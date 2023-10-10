using Insight.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Domains.Products
{
    public abstract partial class ProductService
    {
        public IDbConnection Connection { private get; set; }

        [Sql("SELECT gp.GlobalProductId, Brand, CategoryId, ImageUrl, Name, IsReviewed FROM GlobalProduct gp where IsDeleted = 0")]
        public abstract Task<List<ProductSummary>> GetAllProductSummaries();

        [Sql(GetMerchantProductsStatement)]
        public abstract Task<List<MerchantProduct>> GetMerchantProductsByGlobalId(string globalProductId);

        [Sql(GetMerchantProductsByMerchantIdStatement)]
        public abstract Task<List<MerchantProduct>> GetMerchantProductsByMerchantId(string merchantId);

        [Sql(GetGlobalProductIdsByMerchantIdStatement)]
        public abstract Task<List<string>> GetGlobalProductIdsByMerchantId(string merchantId);

        [Sql(GetGlobalProductsByMerchantIdStatement)]
        public abstract Task<List<Product>> GetGlobalProductsByMerchantId(string merchantId);

        [Sql(GetGlobalProductStatement)]
        public abstract Task<Product> GetGlobalProduct(string globalProductId);

        [Sql(UpdateGlobalProductStatement)]
        public abstract Task UpdateGlobalProduct(Product product);

        [Sql("update GlobalProduct set IsReviewed = @isReviewed where GlobalProductId = @globalProductId")]
        public abstract Task SetGlobalProductReviewStatus(string globalProductId, bool isReviewed);

        [Sql("select MerchantId, ProductId, GlobalProductId from ProductMappings where MerchantId = @merchantId")]
        public abstract Task<IList<ProductMapping>> GetProductMappingsByMerchant(string merchantId);
    }
}
