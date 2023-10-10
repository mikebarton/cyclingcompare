using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Options
{
    public class CommissionFactoryOptions
    {
        public string Host { get; set; }
        public string ApiKey { get; set; }
        public string GetMerchantsPath { get; set; }
        public string GetDataFeedsPath { get; set; }
        public string GetDataFeedPath { get; set; }        
        public string GetAllBannersPath { get; set; }
    }
}
