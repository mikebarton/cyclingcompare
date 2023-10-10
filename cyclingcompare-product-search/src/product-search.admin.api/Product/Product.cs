using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Product
{
    public class Product
    {
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
        public string Size { get; set; }
        public string Colour { get; set; }
        public string Gender { get; set; }
        public string PreviewImageUrl { get; set; }

    }
}
