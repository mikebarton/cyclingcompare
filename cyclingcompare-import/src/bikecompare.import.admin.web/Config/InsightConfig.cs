using bikecompare.import.admin.web.Domains.Products;
﻿using bikecompare.import.admin.web.Domains.Merchants;
using bikecompare.import.messagehandler.Domains.Categories;
using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace bikecompare.import.messagehandler.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection ConfigureInsight(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = config.GetConnectionString("bikecompare");
            var connectionString = ConnectionStringBuilder.GetConnectionString();

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

            services.AddTransient<MerchantService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<MerchantService>();
                //service.Connection = connection;
                return service;
            });

            return services;
        }
    }
}