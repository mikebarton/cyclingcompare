using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Config
{
    public static class ConfigureAuthentication
    {
        public static IServiceCollection ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var projectId = configuration.GetValue<string>("PROJECT_ID");
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //       .AddJwtBearer(options =>
            //       {
            //           options.Audience = projectId;
            //           options.Authority = $"https://securetoken.google.com/{projectId}";
            //       });

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            return services;
        }
    }
}
