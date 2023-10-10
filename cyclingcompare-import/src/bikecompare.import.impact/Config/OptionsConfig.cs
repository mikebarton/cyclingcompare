using bikecompare.import.impact.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Config
{
    public static class OptionsConfig
    {
        public static void RegisterOptions(this IServiceCollection services, IConfiguration config)
        {            
            var impactConfig = config.GetSection("Impact");
            services.Configure<ImpactOptions>(impactConfig);
        }
    }
}
