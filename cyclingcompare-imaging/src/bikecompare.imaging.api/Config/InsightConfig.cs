using bikecompare.imaging.api.Banners;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.api.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection ConfigureInsight(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = ConnectionStringBuilder.GetConnectionString();
            //var connectionString = config.GetConnectionString("bikecompare");

            services.AddTransient<BannerService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<BannerService>();
                return service;
            });

            //services.AddTransient<ProductSummaryService>(serviceProvider =>
            //{
            //    var connection = new MySqlConnection(connectionString);
            //    var service = connection.As<ProductSummaryService>();
            //    return service;
            //});

            //services.AddTransient<FilterService>(serviceProvider =>
            //{
            //    var connection = new MySqlConnection(connectionString);
            //    var service = connection.As<FilterService>();
            //    return service;
            //});



            return services;
        }
    }
}
