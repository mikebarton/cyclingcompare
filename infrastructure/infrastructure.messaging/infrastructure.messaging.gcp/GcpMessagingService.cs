using Google.Api.Gax.Grpc;
using Google.Cloud.PubSub.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using infrastructure.messaging;

namespace infrastructure.messaging.gcp
{
    public class GcpMessagingService : IMessagePublisher
    {
        private Dictionary<Type, PublisherClient> _publisherClients;
        private GcpMessagingConfig _config;

        public GcpMessagingService(GcpMessagingConfig config)
        {
            _publisherClients = new Dictionary<Type, PublisherClient>();

            if (config == null)
                throw new InvalidProgramException("No GcpMessagingConfig. Register an instance with the service provider that contains required config data");

            _config = config;            
        }

        public Task ShutDown()
        {
            var tasks = new List<Task>();
            foreach (var client in _publisherClients.Values)
            {
                var task = client.ShutdownAsync(_config.TimeSpan);
                tasks.Add(task);
            }

            return Task.WhenAll(tasks);
        }

        private async Task<PublisherClient> EnsurePublisher<T>()
        {
            if (_publisherClients.ContainsKey(typeof(T)))
                return _publisherClients[typeof(T)];

            var topicName = _config.GetTopicName<T>();
            var publisherClient = await PublisherClient.CreateAsync(new TopicName(_config.ProjectID, topicName), settings: _config.ClientSettings ?? new PublisherClient.Settings());
            _publisherClients.Add(typeof(T), publisherClient);
            return publisherClient;
        }

        public async Task SendMessage<T>(T message) where T : class
        {
            var publisherClient = await EnsurePublisher<T>();
            var text = JsonConvert.SerializeObject(message);
            await publisherClient.PublishAsync(text);
        }        

        
    }
}
