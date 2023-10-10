using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.aspnet.Middleware.ApplicationContext
{
    public class ApplicationContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IApplicationContextBuilder _builder;

        public ApplicationContextMiddleware(RequestDelegate next, IApplicationContextBuilder builder)
        {
            _next = next;
            _builder = builder;
        }

        public async Task InvokeAsync(HttpContext httpContext, IApplicationContext applicationContext)
        {
            await applicationContext.Initialise(httpContext, _builder);
            await _next.Invoke(httpContext);
        }
    }
}
