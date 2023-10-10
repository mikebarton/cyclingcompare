using Microsoft.Extensions.DependencyInjection;
using product_search.api.ProductSummaries.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.Config
{
    public static class ServiceConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<ProductFilterFactory>();
            services.AddTransient<IFilterService, CachedFilterService>();
            services.AddTransient<ColourFilterProvider>();
            services.AddTransient<BrandFilterProvider>();
            services.AddTransient<GenderFilterProvider>();
            services.AddTransient<MerchantFilterProvider>();
            services.AddTransient<PriceFilterProvider>();
            services.AddTransient<SizeFilterProvider>();

            return services;
        }
    }
}
