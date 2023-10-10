using AutoMapper;
using bikecompare.import.impact.Config;
using bikecompare.import.impact.Mapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

[assembly: HostingStartup(typeof(bikecompare.import.impact.HostedStartup))]
namespace bikecompare.import.impact
{
    public class HostedStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {   
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var bucket = context.HostingEnvironment.IsDevelopment()  ? "cyclingcompare-dev-data-import" : "cyclingcompare-data-import";

                var dict = new Dictionary<string, string>
                {
                    {"Impact:StorageBucket", bucket },
                    {"Impact:WiggleFilePath", "impact/Wiggle_IR.xml" },
                    {"Impact:ChainReactionFilePath", "impact/Chain-Reaction-Cycles-AU_IR.xml" },                    
                };

                config.AddInMemoryCollection(dict);
            });
            
            builder.ConfigureServices((context, services) =>
            {
                services.RegisterOptions(context.Configuration);
                services.AddAutoMapper(typeof(DefaultMapperProfile));
                services.RegisterServices();
            });
        }
    }
}
