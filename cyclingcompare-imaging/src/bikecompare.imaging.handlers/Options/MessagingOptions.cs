using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Options
{
    public class MessagingOptions
    {
        public TopicNames Topics { get; set; }
    }

    public class TopicNames
    {
        public string ProductSummaryResized { get; set; }
        public string OriginalImageStored { get; set; }
        public string ImageHashCalculated { get; set; }
    }
}
