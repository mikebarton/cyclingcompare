using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.MerchantListing
{
    public partial class MerchantListingService
    {
        public const string GetMerchantListingStatement = @"SELECT MerchantListingId, MerchantId, ProductId, VariationId, DateCreated, TrackingUrl, StockLevel, SKU, Price, PriceRrp, PromoText, DeliveryCost, DeliveryTime FROM MerchantListingDetails where MerchantId = @merchantId and ProductId = @globalProductId and VariationId = @productId";
        public const string GetMerchantListingsForProductStatement = @"SELECT MerchantListingId, MerchantId, ProductId, VariationId, DateCreated, TrackingUrl, StockLevel, SKU, Price, PriceRrp, PromoText, DeliveryCost, DeliveryTime FROM MerchantListingDetails where ProductId = @globalProductId";
        public const string InsertMerchantListingStatement = @"INSERT INTO MerchantListingDetails
                                                                (MerchantListingId,
                                                                MerchantId,
                                                                ProductId,
                                                                VariationId,
                                                                DateCreated,
                                                                TrackingUrl,
                                                                StockLevel,
                                                                SKU,
                                                                Price,
                                                                PriceRrp,
                                                                PromoText,
                                                                DeliveryCost,
                                                                DeliveryTime,
                                                                Name,
                                                                Size,
                                                                Gender,
                                                                Colour)
                                                                VALUES
                                                                (@merchantListingId,
                                                                @merchantId,
                                                                @productId,
                                                                @variationId,
                                                                @dateCreated,
                                                                @trackingUrl,
                                                                @stockLevel,
                                                                @sku,
                                                                @price,
                                                                @priceRrp,
                                                                @promoText,
                                                                @deliveryCost,
                                                                @deliveryTime,
                                                                @name,
                                                                @size,
                                                                @gender,
                                                                @colour);
                                                                ";
        public const string UpdateMerchantListingStatement = @"UPDATE MerchantListingDetails
                                                                SET
                                                                DateCreated = @dateCreated,
                                                                TrackingUrl = @trackingUrl,
                                                                StockLevel = @stockLevel,
                                                                SKU = @sku,
                                                                Price = @price,
                                                                PriceRrp = @priceRrp,
                                                                PromoText = @promoText,
                                                                DeliveryCost = @deliveryCost,
                                                                DeliveryTime = @deliveryTime,
                                                                Name = @name,
                                                                Size = @size,
                                                                Gender = @gender,
                                                                Colour = @colour
                                                                WHERE ProductId = @productId and MerchantId = @merchantId and VariationId = @variationId;
                                                                ";

        public const string DeleteMerchantListingStatement = @"Delete 
                                                                from MerchantListingDetails
                                                                where ProductId = @globalProductId and MerchantId = @merchantId and VariationId = @productId";

        public const string DeleteAllMerchantListingsForProductStatement = @"Delete from MerchantListingDetails where ProductId = @productId";

    }
}
