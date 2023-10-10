using cyclingcompare.comms.api.Email.ContactUs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace cyclingcompare.comms.api.Email
{
    public static class EmailConfigurationExtension
    {
        public static IServiceCollection UseEmail(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SendGridOptions>(options =>
            {
                options.ApiKey = config.GetValue<string>("sendgrid_apikey");
            });
            services.Configure<ContactUsOptions>(options =>
            {
                var section = config.GetSection("contactus");
                options.TargetEmail = section.GetValue<string>("targetemail");
                options.FromEmail = section.GetValue<string>("fromemail");
                options.FromName = section.GetValue<string>("fromname");
            });
            services.AddTransient<EmailService>();
            return services;
        }

        public static IServiceCollection UseRazorLight(this IServiceCollection services, IConfiguration config)
        {
            var projectFolder = config.GetValue<string>(WebHostDefaults.ContentRootKey);
            var engineBuilder = new RazorLightEngineBuilder()
                            .UseFileSystemProject(Path.Join(projectFolder, @"\Templates"))
                            .SetOperatingAssembly(Assembly.GetExecutingAssembly())
                            .UseMemoryCachingProvider();

            services.AddSingleton<IRazorLightEngine>(engineBuilder.Build());

            return services;
        }
    }
}
