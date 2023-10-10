using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.Merchant
{
    public partial class MerchantService
    {
        public const string GetMerchantStatement = @"select MerchantId, Name, Summary, TargetUrl, TrackingUrl from Merchant where MerchantId = @merchantId";
        public const string UpdateMerchantStatement = @"update Merchant set Name = @name, Summary = @summary, TargetUrl = @targetUrl, TrackingUrl = @trackingUrl where MerchantId = @merchantId";
        public const string InsertMerchantStatement = @"insert into Merchant (MerchantId, Name, Summary, TargetUrl, TrackingUrl) values (@merchantId, @name, @summary, @targetUrl, @trackingUrl)";
    }
}
