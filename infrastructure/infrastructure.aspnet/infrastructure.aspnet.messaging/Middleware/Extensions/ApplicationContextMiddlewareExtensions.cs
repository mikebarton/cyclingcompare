using infrastructure.aspnet.messaging.Middleware.ApplicationContext;
using infrastructure.aspnet.Middleware.ApplicationContext;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.aspnet.messaging.Middleware.Extensions
{
    public static class ApplicationContextExtensions
    {
        public static IApplicationBuilder UseApplicationContext(this IApplicationBuilder appBuilder, Action<IApplicationContextConfigurator> optionsBuilder)
        {
            var options = new messaging.Middleware.ApplicationContext.ApplicationContextBuilder();
            optionsBuilder(options);
            return appBuilder.UseMiddleware<ApplicationContextMiddleware>(options);
        }
    }
}
