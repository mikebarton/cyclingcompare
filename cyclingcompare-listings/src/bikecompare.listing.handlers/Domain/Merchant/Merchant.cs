using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.Merchant
{
    public class Merchant
    {
        public string MerchantId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string TargetUrl { get; set; }
        public string TrackingUrl { get; set; }
    }
}
