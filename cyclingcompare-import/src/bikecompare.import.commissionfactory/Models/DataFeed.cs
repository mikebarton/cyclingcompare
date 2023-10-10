using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Models
{
    public class DataFeed
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantAvatarUrl { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int ItemsCount { get; set; }
        public List<DataFeedItem> Items { get; set; }
    }
}
