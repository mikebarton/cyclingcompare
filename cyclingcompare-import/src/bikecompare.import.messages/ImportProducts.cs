using MediatR;
using System;
using System.Collections.Generic;

namespace bikecompare.import.messages
{
    public class ImportProducts : IRequest
    {       
        public DateTime LastModified { get; set; }
        public ApiManager ApiManager { get; set; }
        public string MerchantApiIdentifier { get; set; }
        public List<Product> Products { get; set; }

    }

    public class Product
    {
        public string ApiIdentifier { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Colour { get; set; }
        public string ContentRating { get; set; }
        public string Currency { get; set; }
        public DateTime? DateModified { get; set; }
        public decimal DeliveryCost { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public List<string> Features { get; set; }
        public Dictionary<string, string> Specs { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string ModelNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceRrp { get; set; }
        public string PromoText { get; set; }
        public string Sku { get; set; }
        public string Size { get; set; }
        public int StockLevel { get; set; }
        public bool? HasStock { get; set; }
        public string TargetUrl { get; set; }
        public string TrackingCode { get; set; }
        public string TrackingUrl { get; set; }
        public string AgeRange { get; set; }
        public float ProductWeight { get; set; }
        public string WeightUnitOfMeasurement { get; set; }
        public string EAN { get; set; }
        public string UPC { get; set; }
    }
}
