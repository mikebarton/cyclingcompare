using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.messagehandlers.Domains.Product
{
    public class ProductCategory
    {
        public string ProductCategoryId { get; set; }
        public string ProductId { get; set; }
        public string CategoryId { get; set; }
    }
}
