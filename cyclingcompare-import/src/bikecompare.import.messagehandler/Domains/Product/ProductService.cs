using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Product
{
    public abstract partial class ProductService
    {
        [Sql(GetAllProductIdsByMerchantStatement)]

        public abstract Task<List<ProductMapping>> GetAllProductMappingsByMerchant(string merchantId);
        
        public async Task<string> ImportProduct(Product product, string merchantId, string apiManager)
        {
            var productId = await GetProductIdByApiId(apiManager, merchantId, product.ApiIdentifier);
            using (var trans = Connection.BeginTransaction())
            {

                try
                {
                    if (string.IsNullOrEmpty(productId))
                    {
                        productId = Guid.NewGuid().ToString("N");
                        await InsertProduct(productId, merchantId, product.Brand, product.Category, product.SubCategory, product.Colour,
                                                        product.ContentRating, product.Currency, product.DateModified, product.DeliveryCost, product.DeliveryTime,
                                                        product.Description, product.Features, product.Specs, product.Gender, product.ImageUrl, product.ModelNumber,
                                                        product.Name, product.Price, product.PriceRrp, product.PromoText, product.Sku, product.Size,
                                                        product.StockLevel, product.TargetUrl, product.TrackingCode, product.TrackingUrl, product.EAN, product.UPC, product.HasStock);
                        await InsertProductMapping(Guid.NewGuid().ToString("N"), apiManager, product.ApiIdentifier, product.MerchantId, productId, null, false);
                    }

                    else
                    {
                        await UpdateProduct(productId, merchantId, product.Brand, product.Category, product.SubCategory, product.Colour,
                                                        product.ContentRating, product.Currency, product.DateModified, product.DeliveryCost, product.DeliveryTime,
                                                        product.Description, product.Features, product.Specs, product.Gender, product.ImageUrl, product.ModelNumber,
                                                        product.Name, product.Price, product.PriceRrp, product.PromoText, product.Sku, product.Size,
                                                        product.StockLevel, product.TargetUrl, product.TrackingCode, product.TrackingUrl, product.EAN, product.UPC, product.HasStock);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    trans.Rollback();
                    throw;
                }
                trans.Commit();
            }
            return productId;
        }

        private IDbConnection _connection;
        public IDbConnection Connection 
        {
            private get
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection;
            }
            set { _connection = value; }
        }

       
        [Sql(GetProductIdStatement)]
        public abstract Task<string> GetProductIdByApiId(string apiManager, string merchantId, string apiIdentifier);

        [Sql(InsertProductStatement)]
        public abstract Task InsertProduct(string @productId, string @merchantId, string brand, string category, string subCategory, string colour, string contentRating, 
                                            string currency, DateTime dateModified, decimal deliveryCost, string deliveryTime, string description, string features, string specs,
                                            string gender, string imageUrl, string modelNumber, string name, decimal price, decimal priceRrp, string promoText, string sku, 
                                            string size, int stockLevel, string targetUrl, string trackingCode, string trackingUrl, string ean, string upc, bool? hasStock);

        [Sql(InsertIntoGlobalProductStatement)]
        public abstract Task InsertGlobalProduct(string globalProductId, string brand, string colour, string contentRating, string categoryId, string currency, DateTime dateModified, decimal deliveryCost, string deliveryTime, string description, string features, string specs, string gender, string imageUrl, string modelNumber, string name, string urlSlug, decimal price, decimal priceRrp, string promoText, string size, string ean, string upc);

        [Sql(InsertIntoProductMappingStatement)]
        public abstract Task InsertProductMapping(string productMappingId, string apiManager, string apiIdentifier, string merchantId, string productId, string globalProductId, bool isMappingVerified);

        [Sql("update ProductMappings set GlobalProductId = @globalProductId, IsMappingVerified = @isMappingVerified where ProductId = @productId")]
        public abstract Task UpdateProductMapping(string productId, string globalProductId, bool isMappingVerified);
        //public abstract void Open();
        //public abstract void Rollback();
        [Sql(UpdateProductStatement)]
        public abstract Task UpdateProduct(string @productId, string @merchantId, string brand, string category, string subCategory, string colour, string contentRating,
                                            string currency, DateTime dateModified, decimal deliveryCost, string deliveryTime, string description, string features, string specs,
                                            string gender, string imageUrl, string modelNumber, string name, decimal price, decimal priceRrp, string promoText, string sku,
                                            string size, int stockLevel, string targetUrl, string trackingCode, string trackingUrl, string ean, string upc, bool? hasStock);

        [Sql(SelectAllProducts)]
        public abstract Task<List<Product>> GetAllProducts();

        
        [Sql(SelectAllGlobalProducts)]
        public abstract Task<List<GlobalProduct>> GetAllGlobalProducts();

        [Sql(SelectAllGlobalProductsByMerchantId)]
        public abstract Task<List<GlobalProduct>> GetAllGlobalProductsByMerchant(string merchantId);

        [Sql(UpdateGlobalProductStatement)]
        public abstract Task UpdateGlobalProduct(GlobalProduct product);

        [Sql(SelectAllGlobalProductsWithoutUrlSlug)]
        public abstract Task<List<GlobalProduct>> GetGlobalProductsWithEmptyUrlSlug();

        [Sql("select ProductId, MerchantId, GlobalProductId from ProductMappings where MerchantId = @merchantId")]
        public abstract Task<List<ProductMapping>> GetMappedGlobalProductIdsForMerchantId(string merchantId);

        //[Sql("update ProductMappings set GlobalProductId = null where ProductId = @productId and GlobalProductId = @globalProductId")]
        //public abstract Task RemoveGlobalProductMapping(string productId, string globalProductId);

        [Sql("update ProductMappings set GlobalProductId = null, IsMappingVerified = 0 where ProductId = @productId and MerchantId = @merchantId")]
        public abstract Task RemoveGlobalProductMapping(string productId, string merchantId);

        //[Sql("delete from ProductMappings where ProductId = @productId and MerchantId = @merchantId and ApiManager = @apiManager")]
        //public abstract Task RemoveProductMapping(string productId, string merchantId, string apiManager);

        [Sql("update Product p set p.IsDeleted = 0 where p.ProductId = @productId")]
        public abstract Task RemoveProduct(string productId);
        
        [Sql(GetProductsForMerchantStatement)]
        public abstract Task<List<Product>> GetProductsForMerchant(string merchantId);

        [Sql(GetMappedDeletedProductsStatement)]
        public abstract Task<List<ProductMapping>> GetMappedDeletedProducts();

        [Sql(DeleteOrphanedGlobalProductsStatement)]
        public abstract Task DeleteOrphanedGlobalProducts();

        [Sql(SelectOrphanedGlobalProductsStatement)]
        public abstract Task<List<string>> GetOrphanedGlobalProducts();

        [Sql("select ProductId, MerchantId, GlobalProductId from ProductMappings where ApiManager = @apiManager and MerchantId = @merchantId and ProductId = @productId")]
        public abstract Task<ProductMapping> GetProductMapping(string apiManager, string merchantId, string productId);

        [Sql("select ProductId, ImageUrl from Product where ImageHash is null")]
        public abstract Task<List<Product>> GetUnhashedProductImages();

        [Sql("update Product set ImageHash = @imageHash where ProductId = @productId")]
        public abstract Task UpdateProductImageHash(string productId, string imageHash);
    }
}
