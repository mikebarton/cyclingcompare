using AutoMapper;
using bikecompare.import.commissionfactory.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(bikecompare.import.commissionfactory.HostedStartup))]
namespace bikecompare.import.commissionfactory
{
    public class HostedStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            var dict = new Dictionary<string, string>
            {
                {"CommissionFactory:Host", "https://api.commissionfactory.com"},
                {"CommissionFactory:GetMerchantsPath",  "/V1/Affiliate/Merchants?apiKey={0}&status=joined"},
                {"CommissionFactory:GetDatafeedsPath", "/V1/Affiliate/DataFeeds?apiKey={0}&merchantId={1}" },
                {"CommissionFactory:GetDataFeedPath",  "/V1/Affiliate/DataFeeds/{0}?apiKey={1}"},
                {"CommissionFactory:GetAllBannersPath", "/V1/Affiliate/Banners?apiKey={0}" },
                {"Messaging:Topics:ImportMerchantTopicName", "import-merchant"},
                {"Messaging:Topics:ImportProductsTopicName", "import-products"}
            };

            builder.ConfigureAppConfiguration(config =>
            {
                config.AddInMemoryCollection(dict);
            });
            
            builder.ConfigureServices((context, services) =>
            {
                services.RegisterOptions(context.Configuration);
                //services.ConfigureMessaging(context.Configuration);
                services.RegisterServices();
            });
        }
    }
}
