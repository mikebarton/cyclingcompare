using AutoMapper;
using bikecompare.import.commissionfactory.Models;
using bikecompare.import.messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Mapper
{
    public class DefaultDataFeedMapperProfile : Profile
    {
        public DefaultDataFeedMapperProfile()
        {
            CreateMap<DataFeedItem, Product>()
                .ForMember(dest => dest.PriceRrp, opt => opt.MapFrom((feedItem, product) => {
                    var parsed = decimal.TryParse(feedItem.PriceRrp, out var prrp);
                    if (!parsed)
                        Console.WriteLine($"Problem PriceRrp: {feedItem.PriceRrp}");

                    return parsed ? prrp : 0;
                }));
        }
    }
}
