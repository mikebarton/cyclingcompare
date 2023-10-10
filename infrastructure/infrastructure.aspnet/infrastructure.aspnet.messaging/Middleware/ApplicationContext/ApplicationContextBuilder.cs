using infrastructure.aspnet.Middleware.ApplicationContext;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.aspnet.messaging.Middleware.ApplicationContext
{
    public class ApplicationContextBuilder : IApplicationContextConfigurator
    {
        private Dictionary<Type, Delegate> _accountIdGetters = new Dictionary<Type, Delegate>();
        public void ConfigureAccountId<TMessage>(Expression<Func<TMessage, string>> expression)
        {
            Delegate getter = expression.Compile();
            _accountIdGetters.Add(typeof(TMessage), getter);
        }

        public async Task<string> GetAccountId(HttpContext context)
        {
            var messageAndType = await GetInnerGcpMessageAndType(context);
            if (messageAndType.success)
            {
                var getter = GetAccountIdGetter(messageAndType.messageType);
                var accountId = getter(messageAndType.message);
                return accountId;
            }
            return null;
        }

        private async Task<(bool success, object message, Type messageType)> GetInnerGcpMessageAndType(HttpContext context)
        {
            var firstActionParameterType = GetGcpMessageActionParameterType(context);
            if (firstActionParameterType != null && firstActionParameterType.IsConstructedGenericType)
            {
                if (typeof(GcpMessage<>) == firstActionParameterType.GetGenericTypeDefinition())
                {
                    var messageType = firstActionParameterType.GenericTypeArguments.Single();
                    var requestObject = await GetRequestBody(firstActionParameterType, context);
                    var methodInfo = firstActionParameterType.GetMethod("GetNestedMessage");
                    var innerMessage = methodInfo.Invoke(requestObject, new dynamic[] { }) as dynamic;
                    return (true, innerMessage, messageType);
                }
            }
            return (false, null, null);
        }

        private Type GetGcpMessageActionParameterType(HttpContext context)
        {
            var endpointFeature = context.Features[typeof(Microsoft.AspNetCore.Http.Features.IEndpointFeature)]
                                            as Microsoft.AspNetCore.Http.Features.IEndpointFeature;

            Microsoft.AspNetCore.Http.Endpoint endpoint = endpointFeature?.Endpoint;
            var types = endpoint.Metadata.Select(x => x.GetType()).ToList();
            var descriptor = endpoint.Metadata.OfType<ControllerActionDescriptor>().SingleOrDefault();
            return descriptor.Parameters.FirstOrDefault()?.ParameterType;
        }

        private async Task<dynamic> GetRequestBody(Type messageType, HttpContext context)
        {            
            context.Request.EnableRewind();
            
            var stream = new MemoryStream();
            await context.Request.Body.CopyToAsync(stream);

            context.Request.Body.Seek(0, SeekOrigin.Begin);
            stream.Position = 0;

            using (var reader = new StreamReader(context.Request.Body))
            {
                var text = await reader.ReadToEndAsync();
                var obj = JsonConvert.DeserializeObject(text, messageType);
                context.Request.Body = stream;
                return obj;
            }
        }

        private Func<dynamic, string> GetAccountIdGetter(Type messageType)
        {
            if (_accountIdGetters.TryGetValue(messageType, out var dele))
            {
                var func = dele;
                var converted = ConvertFunc(func);
                return converted;
            }
            return null;
        }

        private Func<dynamic, string> ConvertFunc(dynamic func)
        {
            return new Func<dynamic, string>(t => func(t));
        }

    
    }
}
