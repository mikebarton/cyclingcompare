using bikecompare.listing.api.Listing;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection RegisterInsight(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = config.GetConnectionString("bikecompare");
            var connectionString = ConnectionStringBuilder.GetConnectionString();

            services.AddTransient<ListingService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<ListingService>();
                return service;
            });

            return services;
        }
    }
}
