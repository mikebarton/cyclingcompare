using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikecompare.messages
{
    public class AddMerchantMessage
    {
        public string MerchantId { get; set; }
        public decimal CommissionMax { get; set; }
        public decimal CommissionMin { get; set; }
        public decimal CommissionRate { get; set; }
        public int CookieDurationHours { get; set; }
        public DateTime DateModified { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string TargetUrl { get; set; }        
        public string TrackingUrl { get; set; }
    }
}
