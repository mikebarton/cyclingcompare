using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.aspnet.Middleware.ApplicationContext
{
    public class ApplicationContext : IApplicationContext
    {
        private HttpContext _context;
        private const string AccountIdKey = "AccountId";
        public async Task Initialise(HttpContext context, IApplicationContextBuilder builder)
        {
            _context = context;
            var accountId = await builder.GetAccountId(context);
            context.Items[AccountIdKey] = accountId;
        }

        public string AccountId
        {
            get
            {
                if (_context.Items.ContainsKey(AccountIdKey))
                    return _context.Items[AccountIdKey].ToString();

                return null;
            }
        }
    }
}
