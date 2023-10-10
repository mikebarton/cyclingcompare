using bikecompare.import.commissionfactory.Options;
using bikecompare.import.messages;
using infrastructure.messaging;
using infrastructure.messaging.gcp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Config
{
    public static class MessagingConfig
    {
        public static void ConfigureMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var messagingOptions = services.BuildServiceProvider().GetRequiredService<IOptions<MessagingOptions>>();
            var projectId = configuration.GetValue<string>("PROJECT_ID");

            var config = new GcpMessagingConfig()
                .SetProjectID(projectId)
                .ConfigureMessageType<ImportMerchant>(messagingOptions.Value.Topics.ImportMerchantTopicName)
                .ConfigureMessageType<ImportProducts>(messagingOptions.Value.Topics.ImportProductsTopicName);

            services.AddSingleton(config);
            services.AddTransient<IMessagePublisher, GcpMessagingService>();
        }
    }
}
