using Insight.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Merchant
{
    public abstract partial class MerchantService
    {

        public IDbConnection Connection { private get; set; }

        public IDbTransaction BeginTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            return Connection.BeginTransaction();
        }

        [Sql(GetMerchantIdStatement)]
        public abstract Task<string> GetMerchantByApiId(string apiManager, string apiIdentifier);

        [Sql(GetAllMerchantsStatement)]
        public abstract Task<List<Merchant>> GetAllMerchants();

        [Sql(InsertMerchantStatement)]
        public abstract Task InsertMerchantRecord(string merchantId, decimal commissionMax, decimal commissionMin, decimal commissionRate, int cookieDurationHours, DateTime dateModified, string status, string name, string summary, string targetMarket, string targetUrl, string termsAndConditions, string trackingCode, string trackingUrl, int validationPeriod);

        [Sql(InsertMerchantMappingStatement)]
        public abstract Task InsertMerchantMapping(string merchantMappingId, string apiManager, string apiIdentifier, string merchantId);

        [Sql(UpdateMerchantStatement)]
        public abstract Task UpdateMerchant(string merchantId, decimal commissionMax, decimal commissionMin, decimal commissionRate, int cookieDurationHours, DateTime dateModified, string status, string name, string summary, string targetMarket, string targetUrl, string termsAndConditions, string trackingCode, string trackingUrl, int validationPeriod);
    }
}
