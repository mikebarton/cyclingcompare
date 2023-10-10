using AutoMapper;
using bikecompare.import.messagehandler.Domains.Categories;
using bikecompare.import.messagehandler.Domains.Merchant;
using bikecompare.import.messagehandler.Domains.Product;
using bikecompare.import.messages;
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
            CreateMap<messages.Product, Domains.Product.Product>()
                .ForMember(dest=>dest.Features, opt=>opt.MapFrom(src=> src.Features == null ? null : JsonConvert.SerializeObject(src.Features)))
                .ForMember(dest=> dest.Specs, opt=>opt.MapFrom(src=>src.Specs == null ? null : JsonConvert.SerializeObject(src.Specs)));
            CreateMap<Domains.Product.Product, AddMerchantProductMessage>()
                .ForMember(dest=> dest.Features, opt=>opt.MapFrom(src=>!string.IsNullOrWhiteSpace(src.Features)? JsonConvert.DeserializeObject<List<string>>(src.Features) : null ))
                .ForMember(dest => dest.Specs, opt=> opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Specs) ? JsonConvert.DeserializeObject<Dictionary<string, string>>(src.Specs) : null ));
            CreateMap<ExternalCategory, ExternalCategoryModel>();
            CreateMap<CategoryMapping, CategoryMappingModel>().ReverseMap();
            CreateMap<GlobalProduct, AddProductMessage>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Features) ? JsonConvert.DeserializeObject<List<string>>(src.Features) : null))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.Specs) ? JsonConvert.DeserializeObject<Dictionary<string, string>>(src.Specs) : null));
            CreateMap<Merchant, AddMerchantMessage>();
            CreateMap<ImportMerchant, AddMerchantMessage>();
            CreateMap<import.messages.Banner, Domains.Banner.Banner>()
                .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.BannerId));
            CreateMap<Domains.Banner.Banner, AddBannerMessage>();
        }
    }
}
