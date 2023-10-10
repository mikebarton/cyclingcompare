using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Options
{
    public class MessagingOptions
    {
        public TopicNames Topics { get; set; }
    }

    public class TopicNames
    {
        public string ImportMerchantTopicName { get; set; }
        public string ImportProductsTopicName { get; set; }
    }
}
