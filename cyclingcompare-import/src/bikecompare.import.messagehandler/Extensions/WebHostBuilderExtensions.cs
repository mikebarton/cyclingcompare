using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseHostingStartups(this IWebHostBuilder builder)
        {
            var assemblies = Assembly.GetEntryAssembly()
                                    .GetReferencedAssemblies()
                                    .Where(x=>!x.Name.StartsWith("Microsoft"))
                                    .Select(x=>Assembly.Load(x));
            
            assemblies = assemblies.Where(a => a.GetTypes()
                                   .Any(t => typeof(IHostingStartup).IsAssignableFrom(t))).ToArray();

            var settingValue = string.Join(';', assemblies.Select(a => a.FullName));
            builder.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, settingValue);
            
            return builder;
        }
    }
}
