using bikecompare.imaging.handlers.Domain.Banner;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection ConfigureInsight(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = config.GetConnectionString("bikecompare");
            var connectionString = ConnectionStringBuilder.GetConnectionString();

            services.AddTransient<BannerService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<BannerService>();                
                return service;
            });

            return services;
        }
    }
}
