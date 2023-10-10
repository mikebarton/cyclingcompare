using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Domains.Merchants
{
    public class Merchant
    {
        public string MerchantId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string TermsAndConditions { get; set; }
        public int ProductCount { get; set; }
        public int ApiIdentifier { get; set; }
    }
}
