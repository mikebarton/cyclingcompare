using AutoMapper;
using bikecompare.imaging.handlers.Domain.Banner;
using bikecompare.messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AddBannerMessage, Banner>();
            
        }
    }
}
