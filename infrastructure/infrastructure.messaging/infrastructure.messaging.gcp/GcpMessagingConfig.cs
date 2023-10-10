using Google.Cloud.PubSub.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.messaging.gcp
{
    public class GcpMessagingConfig
    {
        public string ProjectID { get; private set; }
        public TimeSpan TimeSpan { get; private set; }
        private Dictionary<Type, string> _typeMappings = new Dictionary<Type, string>();
        public PublisherClient.Settings ClientSettings;

        public GcpMessagingConfig()
        {
            TimeSpan = TimeSpan.FromMinutes(1);
        }

        public GcpMessagingConfig SetProjectID(string projectID)
        {
            ProjectID = projectID;
            return this;
        }

        public GcpMessagingConfig SetShutdownTimeSpan(TimeSpan span)
        {
            TimeSpan = span;
            return this;
        }

        public GcpMessagingConfig SetBatchingConfig(int messageCountThreshold, int byteCountThreshold, TimeSpan timeDelayThreshold)
        {
            ClientSettings = new PublisherClient.Settings
            {
                BatchingSettings = new Google.Api.Gax.BatchingSettings(
                elementCountThreshold: messageCountThreshold,
                byteCountThreshold: byteCountThreshold,
                delayThreshold: timeDelayThreshold)
            };
            return this;
        }

        public GcpMessagingConfig ConfigureMessageType<T>(string topicName)
        {
            if (!_typeMappings.ContainsKey(typeof(T)))
            {
                _typeMappings.Add(typeof(T), topicName);
            }
            return this;
        }

        public string GetTopicName<T>()
        {
            var typeObj = typeof(T);
            if (!_typeMappings.ContainsKey(typeObj))
                throw new InvalidOperationException($"Unable to find topic for message type {typeObj.FullName}");

            return _typeMappings[typeObj];
        }
    }
}
