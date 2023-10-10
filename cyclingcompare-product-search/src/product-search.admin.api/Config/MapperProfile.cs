using AutoMapper;
using product_search.admin.api.Filters;
using product_search.api.Categories;

namespace product_search.api.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryFilterTranslation, CategoryFilterTranslationModel>().ReverseMap();
            CreateMap<CategoryFilter, CategoryFilterModel>()
                .ForMember(dest => dest.FilterTypeCode, opt => opt.MapFrom((filter, filterModel) =>
                  {
                      switch (filter.FilterTypeCode)
                      {
                          case "SIZ": return FilterType.Size;
                          case "GEN": return FilterType.Gender;
                          case "COL": return FilterType.Colour;
                          default:
                              throw new System.Exception($"Invalid FilterTypeCode - {filter.FilterTypeCode}");
                      }
                  }))
                .ReverseMap()
                  .ForMember(dest => dest.FilterTypeCode, opt => opt.MapFrom((filterModel, filter) =>
                     {
                         switch (filterModel.FilterTypeCode)
                         {
                             case FilterType.Colour: return "COL";
                             case FilterType.Gender: return "GEN";
                             case FilterType.Size: return "SIZ";
                             default:
                                 throw new System.Exception($"Invalid FilterType - {filterModel.FilterTypeCode.ToString()}");
                         }
                     }));
        }
    }
}
