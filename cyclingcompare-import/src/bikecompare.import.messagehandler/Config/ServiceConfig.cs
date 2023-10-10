using bikecompare.import.messagehandler.Domains.Product;
using bikecompare.import.messagehandler.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ProductMatchService>();
            services.AddTransient<ProductMappingService>();
            services.AddTransient<ImageHashService>();
            //services.AddHttpClient();
            
            return services;
        }
    }
}

