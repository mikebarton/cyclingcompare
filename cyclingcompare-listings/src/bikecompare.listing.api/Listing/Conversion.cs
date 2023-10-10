using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Listing
{
    public class Conversion
    {
        public string ConversionId { get; set; }
        public string MerchantId { get; set; }
        public string ProductId { get; set; }
        public string TrackingUrl { get; set; }
        public string StockLevel { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public decimal PriceRrp { get; set; }
        public decimal DeliveryCost { get; set; }
        public string DeliveryTime { get; set; }
        public string IPAddress { get; set; }
        public string Referer { get; set; }
        public string UserAgent { get; set; }
        public string Host { get; set; }
        public string Origin { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
