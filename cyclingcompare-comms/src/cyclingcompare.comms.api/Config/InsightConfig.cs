using cyclingcompare.comms.api.Config;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;


namespace product_search.api.Config
{
    public static class InsightConfig
    {
        public static IServiceCollection ConfigureInsight(this IServiceCollection services, IConfiguration config)
        {
            //var connectionString = ConnectionStringBuilder.GetConnectionString(config);
            ////var connectionString = config.GetConnectionString("bikecompare");

            //services.AddTransient<CategoryService>(serviceProvider =>
            //{
            //    var connection = new MySqlConnection(connectionString);
            //    var service = connection.As<CategoryService>();
            //    return service;
            //});

            



            return services;
        }
    }
}
