using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Mapper
{
    public class MapperFactory
    {        
        public IMapper CreateMapper(string merchantId = null)
        {
            switch (merchantId)
            {
                case Constants.MerchantIds.ChainReaction:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {                            
                            cfg.AddProfile<ChainReactionMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }                
                case Constants.MerchantIds.Wiggle:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<WiggleMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
                default:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {                            
                            cfg.AddProfile<DefaultMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
            }
        }
    }
}
