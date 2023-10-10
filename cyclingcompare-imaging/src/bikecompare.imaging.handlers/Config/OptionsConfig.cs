using bikecompare.imaging.handlers.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Config
{
    public static class OptionsConfig
    {
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration config)
        {
            var messagingConfig = config.GetSection("Messaging");
            services.Configure<MessagingOptions>(messagingConfig);

            var storageConfig = config.GetSection("Storage");
            services.Configure<StorageOptions>(storageConfig);

            return services;
        }
    }
}
