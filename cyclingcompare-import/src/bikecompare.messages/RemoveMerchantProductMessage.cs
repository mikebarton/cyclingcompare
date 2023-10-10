using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikecompare.messages
{
    public class RemoveMerchantProductMessage
    {
        public string GlobalProductId { get; set; }
        public string ProductId { get; set; }
        public string MerchantId { get; set; }
    }
}
