using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.messagehandlers.MerchantSummaries
{
    public abstract partial class MerchantService
    {
        [Sql("insert into MerchantSummary (MerchantId, MerchantName) values (@merchantId, @merchantName)")]
        public abstract Task InsertMerchantSummary(MerchantSummary merchantSummary);

        [Sql("update MerchantSummary set MerchantName = @merchantName where MerchantId = @merchantId")]
        public abstract Task UpdateMerchantSummary(MerchantSummary merchantSummary);

        [Sql("select MerchantId, MerchantName from MerchantSummary where MerchantId = @merchantId")]
        public abstract Task<MerchantSummary> GetMerchantSummaryById(string merchantId);
    }
}
