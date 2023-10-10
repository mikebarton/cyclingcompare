using System;
using System.Collections.Generic;

namespace bikecompare.messages
{
    public class AddProductMessage
    {
        public string GlobalProductId { get; set; }
        public string Brand { get; set; }
        public string CategoryId { get; set; }
        public string Colour { get; set; }
        public string ContentRating { get; set; }
        public string Currency { get; set; }
        public DateTime DateModified { get; set; }
        public decimal DeliveryCost { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public List<string> Features { get; set; }
        public Dictionary<string, string> Specs { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string ModelNumber { get; set; }
        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public decimal Price { get; set; }
        public decimal PriceRrp { get; set; }
        public string PromoText { get; set; }
        public string Size { get; set; }
    }

    
}
