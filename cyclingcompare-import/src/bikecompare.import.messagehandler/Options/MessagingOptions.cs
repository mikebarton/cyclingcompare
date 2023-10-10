using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Options
{
    public class MessagingOptions
    {
        public TopicNames Topics { get; set; }
    }

    public class TopicNames
    {
        public string AddProductTopicName { get; set; }
        public string RemoveProductTopicName { get; set; }
        public string AddMerchantProductTopicName { get; set; }
        public string RemoveMerchantProductTopicName { get; set; }
        public string AddMerchantTopicName { get; set; }
        public string RemoveMerchantTopicName { get; set; }
        public string AddBannerTopicName { get; set; }
        public string DeleteBannerTopicName { get; set; }
    }
}
