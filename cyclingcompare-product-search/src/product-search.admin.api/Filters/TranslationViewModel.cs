using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.admin.api.Filters
{
    

    public record TranslationViewModel
    {
        public string TranslationName { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
