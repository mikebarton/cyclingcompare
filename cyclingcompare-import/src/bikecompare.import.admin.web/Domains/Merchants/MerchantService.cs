using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Domains.Merchants
{
    public abstract partial class MerchantService
    {
        [Sql(GetMerchantDataStatement)]
        public abstract Task<List<Merchant>> GetMerchants();
    }
}
