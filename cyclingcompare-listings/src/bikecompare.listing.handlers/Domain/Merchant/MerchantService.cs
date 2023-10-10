using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.Merchant
{
    public abstract partial class MerchantService
    {
        [Sql(GetMerchantStatement)]
        public abstract Task<Merchant> GetMerchant(string merchantId);
        [Sql(InsertMerchantStatement)]
        public abstract Task InsertMerchant(Merchant merchant);
        [Sql(UpdateMerchantStatement)]
        public abstract Task UpdateMerchant(Merchant merchant);
    }
}
