using AutoMapper;
using cyclingcompare.comms.api.Email.ContactUs;
using cyclingcompare.comms.api.ViewModels.ContactUs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cyclingcompare.comms.api.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<SendContactUsMessageViewModel, ContactUsViewModel>()
                .ForMember(dest=> dest.FromName, opt=> opt.MapFrom(src=>src.Name));
        }
    }
}
