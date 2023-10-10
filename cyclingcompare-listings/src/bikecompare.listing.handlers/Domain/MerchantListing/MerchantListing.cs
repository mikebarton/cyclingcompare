using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.MerchantListing
{
    public class MerchantListing
    {
        public string MerchantListingId { get; set; }
        public string MerchantId { get; set; }
        public string ProductId { get; set; }
        public string VariationId { get; set; }
        public DateTime DateCreated { get; set; }
        public string TrackingUrl { get; set; }
        public string StockLevel { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public decimal PriceRrp { get; set; }
        public string PromoText { get; set; }
        public decimal DeliveryCost { get; set; }
        public string DeliveryTime { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Colour { get; set; }
    }
}
