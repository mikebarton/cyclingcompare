using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Domains.Products
{
    public partial class ProductService
    {
       

        public const string GetMerchantProductsStatement = @"SELECT 
                                                                p.ProductId, 
                                                                p.IsDeleted, 
                                                                p.MerchantId, 
                                                                p.Brand, 
                                                                p.Category, 
                                                                p.SubCategory, 
                                                                p.Colour, 
                                                                p.ContentRating, 
                                                                p.Currency, 
                                                                p.DateModified, 
                                                                p.DeliveryCost, 
                                                                p.DeliveryTime, 
                                                                p.Description, 
                                                                p.Features, 
                                                                p.Specs, 
                                                                p.Gender, 
                                                                p.ImageUrl, 
                                                                p.ModelNumber, 
                                                                p.Name, 
                                                                p.Price, 
                                                                p.PriceRrp, 
                                                                p.PromoText, 
                                                                p.SKU, 
                                                                p.Size, 
                                                                p.StockLevel, 
                                                                p.TargetUrl, 
                                                                p.TrackingCode, 
                                                                p.TrackingUrl,
                                                                m.Name as MerchantName,
                                                                m.Summary as MerchantSummary,
                                                                m.TrackingUrl as MerchantTrackingUrl,
                                                                pm.GlobalProductId
                                                            FROM Product p
                                                            JOIN ProductMappings pm on p.ProductId = pm.ProductId
                                                            JOIN Merchant m on p.MerchantId = m.MerchantId
                                                            WHERE pm.GlobalProductId = @globalProductId";

        public const string GetMerchantProductsByMerchantIdStatement = @"SELECT 
                                                                p.ProductId, 
                                                                p.IsDeleted, 
                                                                p.MerchantId, 
                                                                p.Brand, 
                                                                p.Category, 
                                                                p.SubCategory, 
                                                                p.Colour, 
                                                                p.ContentRating, 
                                                                p.Currency, 
                                                                p.DateModified, 
                                                                p.DeliveryCost, 
                                                                p.DeliveryTime, 
                                                                p.Description, 
                                                                p.Features, 
                                                                p.Specs, 
                                                                p.Gender, 
                                                                p.ImageUrl, 
                                                                p.ModelNumber, 
                                                                p.Name, 
                                                                p.Price, 
                                                                p.PriceRrp, 
                                                                p.PromoText, 
                                                                p.SKU, 
                                                                p.Size, 
                                                                p.StockLevel, 
                                                                p.TargetUrl, 
                                                                p.TrackingCode, 
                                                                p.TrackingUrl,
                                                                m.Name as MerchantName,
                                                                m.Summary as MerchantSummary,
                                                                m.TrackingUrl as MerchantTrackingUrl,
                                                                pm.GlobalProductId
                                                            FROM Product p
                                                            JOIN ProductMappings pm on p.ProductId = pm.ProductId
                                                            JOIN Merchant m on p.MerchantId = m.MerchantId
                                                            WHERE m.MerchantId = @merchantId and pm.GlobalProductId is not null";

        public const string GetGlobalProductsByMerchantIdStatement = @"SELECT gp.GlobalProductId, 
	                                                                        gp.Brand, 
	                                                                        gp.Colour, 
	                                                                        gp.ContentRating, 
	                                                                        gp.CategoryId, 
	                                                                        gp.Currency, 
	                                                                        gp.DateModified, 
	                                                                        gp.DeliveryCost, 
	                                                                        gp.DeliveryTime, 
	                                                                        gp.Description, 
	                                                                        gp.Features, 
	                                                                        gp.Specs,
	                                                                        gp.Gender, 
	                                                                        gp.ImageUrl, 
	                                                                        gp.ModelNumber, 
	                                                                        gp.Name, 
	                                                                        gp.UrlSlug,
	                                                                        gp.Price, 
	                                                                        gp.PriceRrp, 
	                                                                        gp.PromoText, 
	                                                                        gp.Size 
                                                                        from ProductMappings p 
                                                                        join GlobalProduct gp on p.GlobalProductId = gp.GlobalProductId
                                                                        WHERE p.MerchantId = @merchantId and 
	                                                                        p.GlobalProductId is not null";

        public const string GetGlobalProductIdsByMerchantIdStatement = @"SELECT GlobalProductId from ProductMappings p WHERE p.MerchantId = @merchantId and GlobalProductId is not null";


        public const string GetGlobalProductStatement = @"SELECT 
                                                            gp.GlobalProductId, 
                                                            gp.Brand, 
                                                            gp.Colour, 
                                                            gp.ContentRating, 
                                                            gp.CategoryId, 
                                                            gp.Currency, 
                                                            gp.DateModified, 
                                                            gp.DeliveryCost, 
                                                            gp.DeliveryTime, 
                                                            gp.Description, 
                                                            gp.Features, 
                                                            gp.Specs, 
                                                            gp.Gender, 
                                                            gp.ImageUrl, 
                                                            gp.ModelNumber, 
                                                            gp.Name, 
                                                            gp.UrlSlug, 
                                                            gp.Price, 
                                                            gp.PriceRrp, 
                                                            gp.PromoText, 
                                                            gp.Size 
                                                        FROM GlobalProduct gp
                                                        WHERE gp.GlobalProductId = @globalProductId";

        public const string UpdateGlobalProductStatement = @"UPDATE GlobalProduct
                                                                SET
                                                                Brand = @brand,
                                                                Colour = @colour,
                                                                ContentRating = @contentRating,
                                                                CategoryId = @categoryId,
                                                                Currency = @currency,
                                                                DateModified = @dateModified,
                                                                DeliveryCost = @deliveryCost,
                                                                DeliveryTime = @deliveryTime,
                                                                Description = @description,
                                                                Features = @features,
                                                                Specs = @specs,
                                                                Gender = @gender,
                                                                ImageUrl = @imageUrl,
                                                                ModelNumber = @modelNumber,
                                                                Name = @name,
                                                                Price = @price,
                                                                PriceRrp = @priceRrp,
                                                                PromoText = @promoText,
                                                                Size = @size
                                                                WHERE GlobalProductId = @globalProductId";
    }
}
