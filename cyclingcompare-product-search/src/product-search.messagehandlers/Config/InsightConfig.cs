using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using product_search.api.Config;
using product_search.messagehandlers.Domains.Product;
using product_search.messagehandlers.MerchantSummaries;

namespace product_search.messagehandlers.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection ConfigureInsight(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = config.GetConnectionString("bikecompare");
            var connectionString = ConnectionStringBuilder.GetConnectionString();

            services.AddTransient<ProductService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<ProductService>();
                return service;
            });

            services.AddTransient<MerchantService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<MerchantService>();
                return service;
            });

            return services;
        }
    }
}
