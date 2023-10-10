using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Domain.Banner
{
    public partial class BannerService
    {
        private const string InsertBannerStatement = @"INSERT INTO Banner
                                                        (BannerId,
                                                        Height,
                                                        Width,
                                                        ImageUrl,
                                                        MerchantId,
                                                        MerchantName,
                                                        Name,
                                                        TargetUrl,
                                                        TrackingCode,
                                                        TrackingUrl)
                                                        VALUES
                                                        (@bannerId,
                                                        @height,
                                                        @width,
                                                        @imageUrl,
                                                        @merchantId,
                                                        @merchantName,
                                                        @name,
                                                        @targetUrl,
                                                        @trackingCode,
                                                        @trackingUrl)";

        private const string UpdateBannerStatement = @"UPDATE Banner
                                                        SET
                                                        Height = @height,
                                                        Width = @width,
                                                        ImageUrl = @imageUrl,
                                                        MerchantName = @merchantName,
                                                        Name = @name,
                                                        TargetUrl = @targetUrl,
                                                        TrackingCode = @trackingCode,
                                                        TrackingUrl = @trackingUrl
                                                        WHERE BannerId = @bannerId
                                                        ";

        private const string GetBannerByIdStatement = @"SELECT BannerId,
                                                            Height,
                                                            Width,
                                                            ImageUrl,
                                                            MerchantId,
                                                            MerchantName,
                                                            Name,
                                                            TargetUrl,
                                                            TrackingCode,
                                                            TrackingUrl
                                                        FROM Banner
                                                        WHERE BannerId = @bannerId
                                                        ";

        private const string DeleteBannerByIdStatement = @"Delete From Banner where BannerId = @bannerId";
    }
}
