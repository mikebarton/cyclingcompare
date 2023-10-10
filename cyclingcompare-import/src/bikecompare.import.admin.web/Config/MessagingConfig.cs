using bikecompare.import.admin.web.Options;
using bikecompare.messages;
using infrastructure.messaging;
using infrastructure.messaging.gcp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Config
{
    public static class MessagingConfig
    {
        public static void ConfigureMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var messagingOptions = services.BuildServiceProvider().GetRequiredService<IOptions<MessagingOptions>>();
            var projectId = configuration.GetValue<string>("PROJECT_ID");

            var config = new GcpMessagingConfig()
                .SetProjectID(projectId)
                .ConfigureMessageType<AddProductMessage>(messagingOptions.Value.Topics.AddProductTopicName)
                .ConfigureMessageType<AddMerchantMessage>(messagingOptions.Value.Topics.AddMerchantTopicName)
                .ConfigureMessageType<RemoveProductMessage>(messagingOptions.Value.Topics.RemoveProductTopicName)
                .ConfigureMessageType<RemoveMerchantMessage>(messagingOptions.Value.Topics.RemoveMerchantTopicName)
                .ConfigureMessageType<AddMerchantProductMessage>(messagingOptions.Value.Topics.AddMerchantProductTopicName)
                .ConfigureMessageType<RemoveMerchantProductMessage>(messagingOptions.Value.Topics.RemoveMerchantProductTopicName);

            services.AddSingleton(config);
            services.AddTransient<IMessagePublisher, GcpMessagingService>();
        }
    }
}
