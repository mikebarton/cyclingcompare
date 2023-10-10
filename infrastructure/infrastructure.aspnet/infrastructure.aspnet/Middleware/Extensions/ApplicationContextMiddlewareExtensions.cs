using infrastructure.aspnet.Middleware.ApplicationContext;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.aspnet.Middleware.Extensions
{
    public static class ApplicationContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseApplicationContext(this IApplicationBuilder appBuilder, Action<IApplicationContextBuilder> optionsBuilder)
        {
            var options = new ApplicationContextBuilder();
            optionsBuilder(options);
            return appBuilder.UseMiddleware<ApplicationContextMiddleware>(options);
        }
    }
}
