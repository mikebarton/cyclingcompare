using AutoMapper;
using bikecompare.import.commissionfactory.Models;
using bikecompare.import.messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Mapper
{
    public class CommonMapperProfile : Profile
    {
        public CommonMapperProfile()
        {
            CreateMap<Merchant, ImportMerchant>();
            CreateMap<Models.Banner, bikecompare.import.messages.Banner>()
                .ForMember(dest => dest.BannerId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
