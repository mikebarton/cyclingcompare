using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.messagehandlers.Domains.Product
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string UrlSlug { get; set; }
        public string Description { get; set; }
        public short Rating { get; set; }
        public int MinPrice { get; set; }
        public string PreviewImageUrl { get; set; }
        public bool IsOnSale { get; set; }
        public string Size { get; set; }
        public string Gender { get; set; }
        public string Colour { get; set; }
        public string CurrencyCode { get; set; }
    }
}
