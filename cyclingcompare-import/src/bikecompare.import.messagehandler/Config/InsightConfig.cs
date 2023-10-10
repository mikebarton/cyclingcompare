using bikecompare.import.messagehandler.Domains.Banner;
using bikecompare.import.messagehandler.Domains.Categories;
using bikecompare.import.messagehandler.Domains.Merchant;
using bikecompare.import.messagehandler.Domains.Product;
using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;

namespace bikecompare.import.messagehandler.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection ConfigureInsight(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = config.GetConnectionString("bikecompare");
            var connectionString = ConnectionStringBuilder.GetConnectionString();

            services.AddTransient<MerchantService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<MerchantService>();
                service.Connection = connection;
                return service;
            });

            services.AddTransient<CategoryService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<CategoryService>();
                service.Connection = connection;
                return service;
            });

            services.AddTransient<ProductService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<ProductService>();
                service.Connection = connection;
                return service;
            });

            services.AddTransient<Func<ProductService>>(provider => () =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<ProductService>();
                service.Connection = connection;
                return service;
            });

            services.AddTransient<BannerService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<BannerService>();
                //service.Connection = connection;
                return service;
            });
            return services;
        }
    }
}