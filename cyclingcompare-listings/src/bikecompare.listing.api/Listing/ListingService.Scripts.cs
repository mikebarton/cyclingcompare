using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Listing
{
    public partial class ListingService
    {
        public const string GetListingStatement = @"select ProductId, Name, Brand, Currency, DateModified, DeliveryCost, DeliveryTime, Description, Features, Specs, Gender, ImageUrl, ModelNumber from Listing where ProductId = @listingId";

        public const string GetMerchantListingsStatement = @"select mld.MerchantListingId, 
		                                                    mld.MerchantId, 
                                                            mld.ProductId, 
                                                            mld.DateCreated, 
                                                            mld.TrackingUrl, 
                                                            mld.StockLevel, 
                                                            mld.Sku, 
                                                            mld.Price, 
                                                            mld.PriceRrp, 
                                                            mld.PromoText, 
                                                            mld.DeliveryCost, 
                                                            mld.DeliveryTime,
                                                            m.Name as MerchantName,
                                                            m.Summary as MerchantSummary,
                                                            m.TrackingUrl as MerchantTrackingUrl,
                                                            mld.Name,
                                                            mld.Size,
                                                            mld.Gender,
                                                            mld.Colour
                                                    from MerchantListingDetails mld
                                                    join Merchant m on mld.MerchantId = m.MerchantId
                                                    where ProductId = @listingId";

        public const string InsertConversionStatement = @"INSERT INTO ListingConversion
                                                                    (ConversionId,
                                                                    MerchantId,
                                                                    ProductId,
                                                                    TrackingUrl,
                                                                    StockLevel,
                                                                    SKU,
                                                                    Price,
                                                                    PriceRrp,
                                                                    DeliveryCost,
                                                                    DeliveryTime,
                                                                    IPAddress,
                                                                    Referer,
                                                                    UserAgent,
                                                                    Host,
                                                                    Origin,
                                                                    DateCreated)
                                                                    VALUES
                                                                    (@conversionId,
                                                                    @merchantId,
                                                                    @productId,
                                                                    @trackingUrl,
                                                                    @stockLevel,
                                                                    @sku,
                                                                    @price,
                                                                    @priceRrp,
                                                                    @deliveryCost,
                                                                    @deliveryTime,
                                                                    @ipAddress,
                                                                    @referer,
                                                                    @userAgent,
                                                                    @host,
                                                                    @origin,
                                                                    @dateCreated);
                                                                    ";
    }
}
