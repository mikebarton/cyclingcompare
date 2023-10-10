using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace infrastructure.messaging.messages
{
    public class GcpMessage<TMessage>
    {
        public MessageData Message { get; set; }
        public string Subscription { get; set; }

        public TMessage GetNestedMessage()
        {
            if (string.IsNullOrEmpty(Message.Data))
                return default(TMessage);

            var bytes = Convert.FromBase64String(Message.Data);
            var text = Encoding.UTF8.GetString(bytes);
            var message = JsonConvert.DeserializeObject<TMessage>(text);
            return message;
        }
    }

    public class MessageData
    {
        public Dictionary<string, string> Attributes { get; set; }
        public string Data { get; set; }
        public string MessageID { get; set; }
    }
}
