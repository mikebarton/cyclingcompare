using infrastructure.aspnet.Middleware.ApplicationContext;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace infrastructure.aspnet.messaging.Middleware.ApplicationContext
{
    public interface IApplicationContextConfigurator : IApplicationContextBuilder
    {
        void ConfigureAccountId<TMessage>(Expression<Func<TMessage, string>> expression);
    }
}
