using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Banner
{
    public partial class BannerService
    {
        private const string InsertBannerStatement = @"INSERT INTO Banner
                                                        (BannerId,
                                                        IsDeleted,
                                                        ApiManager,
                                                        ExternalId,
                                                        DateCreated,
                                                        DateModified,
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
                                                        @isDeleted,
                                                        @apiManager,
                                                        @externalId,
                                                        @dateCreated,
                                                        @dateModified,
                                                        @height,
                                                        @width,
                                                        @imageUrl,
                                                        @merchantId,
                                                        @merchantName,
                                                        @name,
                                                        @targetUrl,
                                                        @trackingCode,
                                                        @trackingUrl)";

        private const string GetBannerByExternalIdStatement = @"SELECT BannerId,
                                                            IsDeleted,
                                                            ApiManager,
                                                            ExternalId,
                                                            DateCreated,
                                                            DateModified,
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
                                                        WHERE ExternalId = @externalId and MerchantId = @merchantId and ApiManager = @apiManager
                                                        ";

        private const string UpdateBannerStatement = @"UPDATE Banner
                                                        SET
                                                        IsDeleted = @isDeleted,
                                                        DateCreated = @dateCreated,
                                                        DateModified = @dateModified,
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
    }
}
