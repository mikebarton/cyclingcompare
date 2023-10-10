using bikecompare.listing.handlers.Domain.Listing;
using bikecompare.listing.handlers.Domain.Merchant;
using bikecompare.listing.handlers.Domain.MerchantListing;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection RegisterInsight(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = config.GetConnectionString("bikecompare");
            var connectionString = ConnectionStringBuilder.GetConnectionString();

            services.AddTransient<MerchantService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<MerchantService>();
                return service;
            });

            services.AddTransient<ListingService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<ListingService>();
                return service;
            });

            services.AddTransient<MerchantListingService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<MerchantListingService>();
                return service;
            });

            return services;
        }
    }
}
