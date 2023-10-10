using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Domains.Categories
{
    public class CategoryMappingModel
    {
        public string CategoryMappingId { get; set; }
        public string ExternalCategoryName { get; set; }
        public string ExternalSubCategoryName { get; set; }
        public string MerchantId { get; set; }
        public string CategoryId { get; set; }
    }
}
