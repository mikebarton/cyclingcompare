using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Product
{
    public partial class ProductService
    {
        private const string GetProductIdStatement = @"SELECT ProductId FROM ProductMappings where ApiManager = @apiManager and MerchantId = @merchantId and ApiIdentifier = @apiIdentifier limit 1";

        private const string GetAllProductIdsByMerchantStatement = @"select ProductId, MerchantId, GlobalProductId from ProductMappings where MerchantId = @merchantId";

        private const string InsertProductStatement = @"INSERT INTO `Product`
                                                                            (`ProductId`,
                                                                            `MerchantId`,
                                                                            `Brand`,
                                                                            `Category`,
                                                                            `SubCategory`,
                                                                            `Colour`,
                                                                            `ContentRating`,
                                                                            `Currency`,
                                                                            `DateModified`,
                                                                            `DeliveryCost`,
                                                                            `DeliveryTime`,
                                                                            `Description`,
                                                                            `Features`,
                                                                            `Specs`,
                                                                            `Gender`,
                                                                            `ImageUrl`,
                                                                            `ModelNumber`,
                                                                            `Name`,
                                                                            `Price`,
                                                                            `PriceRrp`,
                                                                            `PromoText`,
                                                                            `SKU`,
                                                                            `Size`,
                                                                            `StockLevel`,
                                                                            `TargetUrl`,
                                                                            `TrackingCode`,
                                                                            `TrackingUrl`,
                                                                            `Ean`,
                                                                            `Upc`,
                                                                            `HasStock`)
                                                                            VALUES
                                                                            (@productId,
                                                                            @merchantId,
                                                                            @brand,
                                                                            @category,
                                                                            @subCategory,
                                                                            @colour,
                                                                            @contentRating,
                                                                            @currency,
                                                                            @dateModified,
                                                                            @deliveryCost,
                                                                            @deliveryTime,
                                                                            @description,
                                                                            @features,
                                                                            @specs,
                                                                            @gender,
                                                                            @imageUrl,
                                                                            @modelNumber,
                                                                            @name,
                                                                            @price,
                                                                            @priceRrp,
                                                                            @promoText,
                                                                            @sku,
                                                                            @size,
                                                                            @stockLevel,
                                                                            @targetUrl,
                                                                            @trackingCode,
                                                                            @trackingUrl,
                                                                            @ean,
                                                                            @upc,
                                                                            @hasStock)
                                                                            ";

        private const string InsertIntoGlobalProductStatement = @"INSERT INTO GlobalProduct
                                                                    (GlobalProductId,
                                                                    Brand,
                                                                    Colour,
                                                                    ContentRating,
                                                                    CategoryId,
                                                                    Currency,
                                                                    DateModified,
                                                                    DeliveryCost,
                                                                    DeliveryTime,
                                                                    Description,
                                                                    Features,
                                                                    Specs,
                                                                    Gender,
                                                                    ImageUrl,
                                                                    ModelNumber,
                                                                    Name,
                                                                    UrlSlug,
                                                                    Price,
                                                                    PriceRrp,
                                                                    PromoText,
                                                                    Size, Ean, Upc)
                                                                    VALUES
                                                                    (@globalProductId,
                                                                    @brand,
                                                                    @colour,
                                                                    @contentRating,
                                                                    @categoryId,
                                                                    @currency,
                                                                    @dateModified,
                                                                    @deliveryCost,
                                                                    @deliveryTime,
                                                                    @description,
                                                                    @features,
                                                                    @specs,
                                                                    @gender,
                                                                    @imageUrl,
                                                                    @modelNumber,
                                                                    @name,
                                                                    @urlSlug,
                                                                    @price,
                                                                    @priceRrp,
                                                                    @promoText,
                                                                    @size, @ean, @upc)
";

        private const string InsertIntoProductMappingStatement = @"INSERT INTO `ProductMappings`
                                                                            (`ProductMappingId`,
                                                                            `ApiManager`,
                                                                            `ApiIdentifier`,
                                                                            `MerchantId`,
                                                                            `ProductId`,
                                                                            `GlobalProductId`,
                                                                            `IsMappingVerified`)
                                                                            VALUES
                                                                            (@productMappingId,
                                                                            @apiManager,
                                                                            @apiIdentifier,
                                                                            @merchantId,
                                                                            @productId,
                                                                            @globalProductId,
                                                                            @isMappingVerified);
                                                                            ";

        private const string UpdateProductStatement = @"UPDATE `Product`
                                                                SET
                                                                `MerchantId` = @merchantId,
                                                                `Brand` = @brand,
                                                                `Category` = @category,
                                                                `SubCategory` = @subCategory,
                                                                `Colour` = @colour,
                                                                `ContentRating` = @contentRating,
                                                                `Currency` = @currency,
                                                                `DateModified` = @dateModified,
                                                                `DeliveryCost` = @deliveryCost,
                                                                `DeliveryTime` = @deliveryTime,
                                                                `Description` = @description,
                                                                `Features` = @features,
                                                                `Specs` = @specs,
                                                                `Gender` = @gender,
                                                                `ImageUrl` = @imageUrl,
                                                                `ModelNumber` = @modelNumber,
                                                                `Name` = @name,
                                                                `Price` = @price,
                                                                `PriceRrp` = @priceRrp,
                                                                `PromoText` = @promoText,
                                                                `SKU` = @sku,
                                                                `Size` = @size,
                                                                `StockLevel` = @stockLevel,
                                                                `TargetUrl` = @targetUrl,
                                                                `TrackingCode` = @trackingCode,
                                                                `TrackingUrl` = @trackingUrl,
                                                                `Ean` = @ean,
                                                                `Upc` = @,
                                                                `HasStock` = @hasStock
                                                                WHERE ProductId = @productId;
                                                                ";

        private const string UpdateGlobalProductStatement = @"UPDATE `GlobalProduct`
                                                                SET
                                                                `Brand` = @brand,
                                                                `Colour` = @colour,
                                                                `ContentRating` = @contentRating,
                                                                `Currency` = @currency,
                                                                `DateModified` = @dateModified,
                                                                `DeliveryCost` = @deliveryCost,
                                                                `DeliveryTime` = @deliveryTime,
                                                                `Description` = @description,
                                                                `Features` = @features,
                                                                `Specs` = @specs,
                                                                `Gender` = @gender,
                                                                `ImageUrl` = @imageUrl,
                                                                `ModelNumber` = @modelNumber,
                                                                `Name` = @name,
                                                                `UrlSlug` = @urlSlug,
                                                                `Price` = @price,
                                                                `PriceRrp` = @priceRrp,
                                                                `PromoText` = @promoText,
                                                                `Size` = @size,
                                                                `Ean` = @ean,
                                                                `Upc` = @upc
                                                                WHERE GlobalProductId = @globalProductId;
                                                                ";

        private const string SelectAllProducts = @"select p.ProductId, 
                                                             p.MerchantId, 
                                                            pm.ApiManager,
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
                                                            p.Ean,
                                                            p.Upc,
                                                            p.HasStock
                                                            from Product p 
                                                            join ProductMappings pm on p.ProductId = pm.ProductId where IsDeleted = 0";

        

        private const string SelectAllGlobalProducts = @"select 
                                                        GlobalProductId, 
                                                        Brand, 
                                                        Colour, 
                                                        ContentRating, 
                                                        CategoryId, 
                                                        Currency, 
                                                        DateModified, 
                                                        DeliveryCost, 
                                                        DeliveryTime, 
                                                        Description, 
                                                        Features, 
                                                        Specs,
                                                        Gender, 
                                                        ImageUrl, 
                                                        ModelNumber, 
                                                        Name, 
                                                        UrlSlug,
                                                        Price, 
                                                        PriceRrp, 
                                                        PromoText, 
                                                        Size, Ean, Upc
                                                        from GlobalProduct where IsDeleted = 0";

        private const string SelectAllGlobalProductsByMerchantId = @"select 
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
                                                        gp.Size, gp.Ean, gp.Upc
                                                        from GlobalProduct gp
                                                        join ProductMappings pm on gp.GlobalProductId = pm.GlobalProductId
                                                        where pm.MerchantId = @merchantId and gp.IsDeleted = 0";

        private const string SelectAllGlobalProductsWithoutUrlSlug = @"select 
                                                        GlobalProductId, 
                                                        Brand, 
                                                        Colour, 
                                                        ContentRating, 
                                                        CategoryId, 
                                                        Currency, 
                                                        DateModified, 
                                                        DeliveryCost, 
                                                        DeliveryTime, 
                                                        Description, 
                                                        Features, 
                                                        Specs,
                                                        Gender, 
                                                        ImageUrl, 
                                                        ModelNumber, 
                                                        Name, 
                                                        UrlSlug,
                                                        Price, 
                                                        PriceRrp, 
                                                        PromoText, 
                                                        Size, Ean, Upc 
                                                        from GlobalProduct where UrlSlug is null";

        private const string DeleteOrphanedGlobalProductsStatement = @"update GlobalProduct gp 
                                                                    left join ProductMappings pm on gp.GlobalProductId = pm.GlobalProductId
                                                                    set gp.IsDeleted = 1 
                                                                    where pm.GlobalProductId is null";

        private const string SelectOrphanedGlobalProductsStatement = @"select gpGlobalProductId as GlobalProductId
                                                                        from 
                                                                        (select gp.GlobalProductId as gpGlobalProductId, pm.GlobalProductId as pmGlobalProductId
                                                                        from GlobalProduct gp
                                                                        left join ProductMappings pm on gp.GlobalProductId = pm.GlobalProductId where gp.IsDeleted = 0) t
                                                                        where pmGlobalProductId is null";


        private const string GetProductsForMerchantStatement = @"select p.ProductId,
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
                                                            p.Ean,
                                                            p.Upc,
                                                            p.ImageHash,
                                                            p.HasStock
                                                        from Product p
                                                        where p.MerchantId = @merchantId and p.IsDeleted = 0;";



        private const string GetMappedDeletedProductsStatement = @"select p.ProductId,
                                                        p.MerchantId,
                                                        pm.GlobalProductId
                                                        from Product p
                                                        join ProductMappings pm on p.ProductId = pm.ProductId
                                                        where p.IsDeleted = 1 and pm.GlobalProductId is not null;";
    }
}
