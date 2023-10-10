using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.aspnet.Middleware.ApplicationContext
{
    public interface IApplicationContext
    {
        Task Initialise(HttpContext context, IApplicationContextBuilder builder);
        string AccountId { get; }
    }
}
