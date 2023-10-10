using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace infrastructure.aspnet.Middleware.ApplicationContext
{
    public class ApplicationContextBuilder : IApplicationContextBuilder
    {
        public Task<string> GetAccountId(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}
