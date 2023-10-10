using bikecompare.import.impact.Mapper;
using bikecompare.import.impact.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Config
{
    public static class ServiceConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            services.AddMediatR(Assembly.GetEntryAssembly());
            services.AddTransient<ImpactFileProcessor>();
            services.AddTransient<CloudStorageFileRetriever>();
            services.AddTransient<MapperFactory>();
        }
    }
}
