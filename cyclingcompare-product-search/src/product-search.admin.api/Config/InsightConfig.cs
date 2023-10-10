using Insight.Database;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using product_search.admin.api.Filters;
using product_search.admin.api.Product;
using product_search.admin.api.ProductSummaries.Filters;
using product_search.api.Categories;


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

            services.AddTransient<FilterService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<FilterService>();
                return service;
            });

            services.AddTransient<ProductService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<ProductService>();
                return service;
            });

            services.AddTransient<TempFilterService>(serviceProvider =>
            {
                var connection = new MySqlConnection(connectionString);
                var service = connection.As<TempFilterService>();
                service.FilterService = serviceProvider.GetRequiredService<FilterService>();
                service.Cache = serviceProvider.GetRequiredService<IMemoryCache>();
                return service;
            });

            return services;
        }
    }
}
