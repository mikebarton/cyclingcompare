using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.messagehandlers.Domains.Product
{
    public abstract class ProductService
    {
        [Sql("insert into ProductSummary (ProductId, ProductName, Brand, UrlSlug, Description, Rating, MinPrice, PreviewImageUrl, IsOnSale, Size, Gender, Colour, CurrencyCode) values (@productId, @productName, @brand, @urlSlug, @description, @rating, @minPrice, @previewImageUrl, @isOnSale, @size, @gender, @colour, @currencyCode)")]
        public abstract Task SaveProduct(Product product);

        [Sql("select ps.ProductId, ps.ProductName, ps.Brand, ps.UrlSlug, ps.Description, ps.Rating, ps.MinPrice, ps.PreviewImageUrl, ps.IsOnSale, ps.Size, ps.Gender, ps.Colour, ps.CurrencyCode from ProductSummary ps where ProductId = @productId")]
        public abstract Task<Product> GetProductById(string productId);

        [Sql("update ProductSummary set ProductName = @productName, Brand = @brand, UrlSlug = @urlSlug, Description = @description, Rating = @rating, MinPrice = @minPrice, IsOnSale = @isOnSale, Size = @size, Gender = @gender, Colour = @colour, CurrencyCode = @currencyCode where ProductId = @productId")]
        public abstract Task UpdateProduct(Product product);

        [Sql("insert into ProductCategory (ProductCategoryId, ProductId, CategoryId) values (@productCategoryId, @productId, @categoryId)")]
        public abstract Task SaveProductCategory(string productCategoryId, string productId, string categoryId);

        [Sql("update ProductCategory set CategoryId = @categoryId where ProductId = @productId")]
        public abstract Task UpdateProductCategory(string productId, string categoryId);

        [Sql("update ProductSummary set PreviewImageUrl = @imageUrl where ProductId = @productId")]
        public abstract Task UpdateProductImageUrl(string productId, string imageUrl);

        [Sql("insert into MerchantProductSummary (ProductId, MerchantId, Price, VariationId, PriceRrp, Size, Colour, Gender, HasStock, StockLevel) values (@globalProductId, @merchantId, @price, @productId, @priceRrp, @size, @colour, @gender, @hasStock, @stockLevel)")]
        public abstract Task InsertMerchantProduct(string globalProductId, string merchantId, decimal price, string productId, decimal priceRrp, string size, string colour, string gender, bool? hasStock, int stockLevel);
        
        [Sql("delete from MerchantProductSummary where ProductId = @globalProductId and MerchantId = @merchantId and VariationId = @productId")]
        public abstract Task DeleteMerchantProduct(string globalProductId, string merchantId, string productId);        

        [Sql("select ProductId, MerchantId, Price, VariationId, PriceRrp from MerchantProductSummary where ProductId = @productId")]
        public abstract Task<List<MerchantProduct>> GetMerchantProductsForProduct(string productId);

        [Sql("delete from ProductSummary where ProductId = @productId")]
        public abstract Task DeleteProductSummary(string productId);

        [Sql(" delete from ProductCategory where ProductId = @productId")]
        public abstract Task DeleteProductCategory(string productId);

        [Sql("delete from MerchantProductSummary where productId = @productId")]
        public abstract Task DeleteAllMerchantProductsForProductId(string productId);
    }
}
