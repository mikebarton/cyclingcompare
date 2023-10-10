using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Listing
{
    public class ListingViewModel
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Currency { get; set; }
        public string DateModified { get; set; }
        public decimal DeliveryCost { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string ModelNumber { get; set; }
        public List<string> Features { get; set; }
        public Dictionary<string, string> Specs { get; set; }

        public List<MerchantListingViewModel> MerchantListings { get; set; }
    }

    public class SpecViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class MerchantListingViewModel {
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
        public decimal DeliverCost { get; set; }
        public string DeliveryTime { get; set; }
        public string MerchantName { get; set; }
        public string MerchantSummary { get; set; }
        public string MerchantTrackingUrl { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Colour { get; set; }
    }

}
