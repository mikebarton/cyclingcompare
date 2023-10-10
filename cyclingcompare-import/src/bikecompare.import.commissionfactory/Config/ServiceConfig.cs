using bikecompare.import.commissionfactory.Controllers;
using bikecompare.import.commissionfactory.Mapper;
using bikecompare.import.commissionfactory.Options;
using bikecompare.import.commissionfactory.Services;
using bikecompare.import.services.commissionfactory;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Config
{
    public static class ServiceConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            services.AddHttpClient<CommissionFactoryService>((serviceProvider, config) =>
            {
                var cfOptions = serviceProvider.GetRequiredService<IOptions<CommissionFactoryOptions>>().Value;
                config.BaseAddress = new Uri(cfOptions.Host);
            });

            services.AddMediatR(Assembly.GetEntryAssembly());
            services.AddTransient<MapperFactory>();
            services.AddTransient<ProductFilterService>();
        }
    }
}
