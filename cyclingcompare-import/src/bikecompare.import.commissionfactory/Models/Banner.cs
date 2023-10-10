using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Models
{
    public class Banner
    {
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string GroupName { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string ImageUrl { get; set; }
        public string MerchantAvatarUrl { get; set; }
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string TargetUrl { get; set; }
        public string TrackingCode { get; set; }
        public string TrackingUrl { get; set; }
    }
}
