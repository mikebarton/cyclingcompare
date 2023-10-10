using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.api.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //services.AddTransient<ProductFilterFactory>();

            return services;
        }
    }
}
