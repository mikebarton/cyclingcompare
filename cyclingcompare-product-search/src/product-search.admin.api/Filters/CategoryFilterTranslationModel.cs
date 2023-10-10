using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Filters
{
    public class CategoryFilterTranslationModel
    {
        public int CategoryFilterTranslationId { get; set; }
        public int CategoryFilterId { get; set; }
        public string Name { get; set; }
    }
}
