using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Listing
{
    public class Listing
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Currency { get; set; }
        public string DateModified { get; set; }
        public decimal DeliveryCost { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string Specs { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string ModelNumber { get; set; }
    }
}
