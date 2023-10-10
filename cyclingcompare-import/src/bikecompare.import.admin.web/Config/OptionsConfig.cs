using bikecompare.import.admin.web.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Config
{
    public static class OptionsConfig
    {
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration config)
    {
        var messagingConfig = config.GetSection("Messaging");
        services.Configure<MessagingOptions>(messagingConfig);

            return services;
    }
}
}
