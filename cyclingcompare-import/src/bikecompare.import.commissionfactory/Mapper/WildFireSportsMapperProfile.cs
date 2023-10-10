using AutoMapper;
using bikecompare.import.commissionfactory.Models;
using bikecompare.import.messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Mapper
{
    public class WildFireSportsMapperProfile : Profile
    {
        public WildFireSportsMapperProfile()
        {
            CreateMap<DataFeedItem, Product>()
                .ForMember(dest => dest.StockLevel, opt => opt.Ignore())
                .ForMember(dest => dest.HasStock, opt => opt.MapFrom((feedItem, product) => string.Equals(feedItem.StockLevel, "in stock", StringComparison.OrdinalIgnoreCase))).ForMember(dest => dest.PriceRrp, opt => opt.MapFrom((feedItem, product) => {
                    var parsed = decimal.TryParse(feedItem.PriceRrp, out var prrp);
                    if (!parsed)
                        Console.WriteLine($"Problem PriceRrp: {feedItem.PriceRrp}");

                    return parsed ? prrp : 0;
                }));
        }        
    }
}
