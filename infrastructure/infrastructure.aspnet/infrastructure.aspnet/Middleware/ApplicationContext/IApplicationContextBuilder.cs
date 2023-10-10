using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.aspnet.Middleware.ApplicationContext
{
    public interface IApplicationContextBuilder
    {
        Task<string> GetAccountId(HttpContext context);
    }
}
