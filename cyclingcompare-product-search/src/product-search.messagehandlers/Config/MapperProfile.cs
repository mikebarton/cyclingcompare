using AutoMapper;
using bikecompare.messages;
using product_search.messagehandlers.Domains.Product;
using product_search.messagehandlers.MerchantSummaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.messagehandlers.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddProductMessage, Product>()
                .ForMember(target => target.ProductId, opt => opt.MapFrom(src => src.GlobalProductId))
                .ForMember(target => target.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(target => target.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(target => target.IsOnSale,
                    opt => opt.MapFrom(
                        src => src.PriceRrp != 0 && src.Price != src.PriceRrp && src.PriceRrp < src.Price))
                .ForMember(target => target.MinPrice, opt => opt.MapFrom(src => src.Price))
                // .ForMember(target => target.PreviewImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(target => target.Rating,
                    opt => opt.MapFrom((src, target) =>
                        short.TryParse(src.ContentRating, out var parsed) ? parsed : 0));
            CreateMap<AddMerchantMessage, MerchantSummary>()
                .ForMember(target => target.MerchantName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
