using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Domains.Merchants
{
    public partial class MerchantService
    {
        public const string GetMerchantDataStatement = @"select m.MerchantId, 
																m.Name, 
																m.Summary, 
																m.TermsAndConditions, 
																cnt.ProductCount,
																mm.ApiIdentifier
														from Merchant m
														join MerchantMapping mm on m.MerchantId = mm.MerchantId
														left join (select MerchantId, count(*) as ProductCount from Product p group by MerchantId) cnt on cnt.MerchantId = m.MerchantId";
    }
}
