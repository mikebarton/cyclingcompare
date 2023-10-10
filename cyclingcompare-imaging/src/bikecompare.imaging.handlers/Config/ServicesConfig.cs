using bikecompare.imaging.handlers.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Config
{
    public static class ServicesConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<GoogleStorageService>();
            services.AddTransient<ImageService>();
            services.AddHttpClient();
            return services;
        }
    }
}
