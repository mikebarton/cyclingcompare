using AutoMapper;
using bikecompare.listing.api.Listing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Listing.Listing, ListingViewModel>()
                .ForMember(dest => dest.Features, opt=> opt.MapFrom(src=>string.IsNullOrWhiteSpace(src.Features) ? null : JsonConvert.DeserializeObject<List<string>>(src.Features)))
                .ForMember(dest => dest.Specs, opt=> opt.MapFrom(src=> string.IsNullOrWhiteSpace(src.Specs) ? null : JsonConvert.DeserializeObject<Dictionary<string, string>>(src.Specs)));
            CreateMap<MerchantListing, MerchantListingViewModel>();
            CreateMap<MerchantListing, Conversion>();
        }
    }
}
