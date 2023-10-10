using bikecompare.imaging.handlers.Options;
using bikecompare.imaging.messages;
using infrastructure.messaging;
using infrastructure.messaging.gcp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Config
{
    public static class MessagingConfig
    {
        public static void ConfigureMessaging(this IServiceCollection services, IConfiguration configuration)
        {
            var messagingOptions = services.BuildServiceProvider().GetRequiredService<IOptions<MessagingOptions>>();
            var projectId = configuration.GetValue<string>("PROJECT_ID");
            var config = new GcpMessagingConfig()
                .SetProjectID(projectId)
                .ConfigureMessageType<ProductSummaryImageResized>(messagingOptions.Value.Topics.ProductSummaryResized)
                .ConfigureMessageType<OriginalImageStored>(messagingOptions.Value.Topics.OriginalImageStored)
                .ConfigureMessageType<ImageHashCalculated>(messagingOptions.Value.Topics.ImageHashCalculated);


            services.AddSingleton(config);
            services.AddTransient<IMessagePublisher, GcpMessagingService>();
        }
    }
}
