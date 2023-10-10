using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikecompare.messages
{
    public class ProductImported
    {
        public string ProductId { get; set; }
        public string ImageUrl { get; set; }
    }
}
