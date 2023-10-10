using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikecompare.messages
{
    public class AddBannerMessage
    {
        public string BannerId { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string ImageUrl { get; set; }
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string Name { get; set; }
        public string TargetUrl { get; set; }
        public string TrackingCode { get; set; }
        public string TrackingUrl { get; set; }
    }
}
