using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using product_search.api.Categories;
using product_search.api.ProductSummaries;
using product_search.api.ProductSummaries.Filters;

namespace product_search.api.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection ConfigureInsight(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = ConnectionStringBuilder.GetConnectionString();
            //var connectionString = config.GetConnectionString("bikecompare");

            services.AddTransient<CategoryService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<CategoryService>();
                return service;
            });

            services.AddTransient<ProductSummaryService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<ProductSummaryService>();
                service.Connection = connection;
                return service;
            });

            services.AddTransient<FilterService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<FilterService>();
                return service;
            });

            

            return services;
        }
    }
}
