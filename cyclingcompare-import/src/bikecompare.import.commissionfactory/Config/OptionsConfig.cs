using bikecompare.import.commissionfactory.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Config
{
    public static class OptionsConfig
    {
        public static void RegisterOptions(this IServiceCollection services, IConfiguration config)
        {
            var messagingConfig = config.GetSection("Messaging");
            services.Configure<MessagingOptions>(messagingConfig);

            var commissionFactoryConfig = config.GetSection("CommissionFactory");
            services.Configure<CommissionFactoryOptions>(commissionFactoryConfig);
        }
    }
}
