using AutoMapper;
using bikecompare.listing.handlers.Domain.Listing;
using bikecompare.listing.handlers.Domain.Merchant;
using bikecompare.listing.handlers.Domain.MerchantListing;
using bikecompare.messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddMerchantMessage, Merchant>()
                .ForMember(target => target.MerchantId, opt => opt.MapFrom(src => src.MerchantId))
                .ForMember(target => target.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(target => target.Summary, opt => opt.MapFrom(src => src.Summary))
                .ForMember(target => target.TargetUrl, opt => opt.MapFrom(src => src.TargetUrl))
                .ForMember(target => target.TrackingUrl, opt => opt.MapFrom(src => src.TrackingUrl));

            CreateMap<AddProductMessage, Listing>()
                .ForMember(target => target.ListingId, opt => opt.MapFrom(src => src.GlobalProductId))
                .ForMember(target => target.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(target => target.Features, opt=> opt.MapFrom(src=> src.Features == null ? null : JsonConvert.SerializeObject(src.Features)))
                .ForMember(target => target.Specs, opt=> opt.MapFrom(src=> src.Specs == null ? null : JsonConvert.SerializeObject(src.Specs)));

            CreateMap<AddMerchantProductMessage, MerchantListing>()
                .ForMember(target=> target.ProductId, opt=> opt.MapFrom(src=>src.GlobalProductId))
                .ForMember(target=>target.VariationId, opt=> opt.MapFrom(src=>src.ProductId));
                
        }
    }
}
