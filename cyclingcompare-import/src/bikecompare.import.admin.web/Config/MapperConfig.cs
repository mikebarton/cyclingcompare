using AutoMapper;
using bikecompare.import.admin.web.Domains.Products;
using bikecompare.import.messagehandler.Domains.Categories;
using bikecompare.messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ExternalCategory, ExternalCategoryModel>();
            CreateMap<CategoryMapping, CategoryMappingModel>().ReverseMap();
            CreateMap<ProductSummary, ProductSummaryViewModel>();
            CreateMap<Product, ProductDetailsViewModel>().ReverseMap();
            CreateMap<MerchantProduct, MerchantProductViewModel>();
            CreateMap<Product, AddProductMessage>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Features) ? JsonConvert.DeserializeObject<List<string>>(src.Features) : null))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Specs) ? JsonConvert.DeserializeObject<Dictionary<string, string>>(src.Specs) : null));
            CreateMap<MerchantProduct, AddMerchantProductMessage>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Features) ? JsonConvert.DeserializeObject<List<string>>(src.Features) : null))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Specs) ? JsonConvert.DeserializeObject<Dictionary<string, string>>(src.Specs) : null));
        }
    }
}
