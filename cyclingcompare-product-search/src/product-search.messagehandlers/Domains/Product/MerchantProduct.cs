using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.messagehandlers.Domains.Product
{
    public class MerchantProduct
    {
        public string ProductId { get; set; }
        public string MerchantId { get; set; }
        public decimal Price { get; set; }
        public string VariationId { get; set; }
        public decimal PriceRrp { get; set; }
        public string Size { get; set; }
        public string Colour { get; set; }
        public string Gender { get; set; }
        public bool HasStock { get; set; }
        public int StockLevel { get; set; }
    }
}
