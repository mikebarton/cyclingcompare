using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace product_search.api.ProductSummaries.Filters
{
    public class ProductFilterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProductFilterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProductFilterProvider Create(FilterViewModel filter)
        {
            switch (filter.FilterId)
            {
                case Constants.FilterIds.PriceFilter:
                    return _serviceProvider.GetRequiredService<PriceFilterProvider>();
                case Constants.FilterIds.BrandFilter:
                    return _serviceProvider.GetRequiredService<BrandFilterProvider>();
                case Constants.FilterIds.MerchantFilter:
                    return _serviceProvider.GetRequiredService<MerchantFilterProvider>();
                case Constants.FilterIds.SizeFilter:
                    return _serviceProvider.GetRequiredService<SizeFilterProvider>();
                case Constants.FilterIds.GenderFilter:
                    return _serviceProvider.GetRequiredService<GenderFilterProvider>();
                case Constants.FilterIds.ColourFilter:
                    return _serviceProvider.GetRequiredService<ColourFilterProvider>();
                default:
                    throw new ArgumentOutOfRangeException($"No FilterProvider for FilterId: {filter.FilterId}");
            }
        }
    }
}
