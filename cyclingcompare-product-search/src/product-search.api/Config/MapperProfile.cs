using AutoMapper;
using product_search.api.Categories;
using product_search.api.ProductSummaries;
using product_search.api.ProductSummaries.Filters;

namespace product_search.api.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<ProductSummary, ProductSummaryModel>();
            CreateMap<Filter, FilterViewModel>();
        }
    }
}
