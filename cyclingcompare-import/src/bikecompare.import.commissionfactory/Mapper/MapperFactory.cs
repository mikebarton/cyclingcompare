using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Mapper
{
    public class MapperFactory
    {        
        public IMapper CreateMapper(string merchantId = null)
        {
            switch (merchantId)
            {
                case Constants.MerchantIds.BicyclesOnline:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CommonMapperProfile>();
                            cfg.AddProfile<BicyclesOnlineMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }                
                case Constants.MerchantIds.FindSports:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CommonMapperProfile>();
                            cfg.AddProfile<FindSportsMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
                case Constants.MerchantIds.WildFireSports:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CommonMapperProfile>();
                            cfg.AddProfile<WildFireSportsMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
                case Constants.MerchantIds.Sportitude:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CommonMapperProfile>();
                            cfg.AddProfile<SportitudeMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
                case Constants.MerchantIds.ProvizSports:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CommonMapperProfile>();
                            cfg.AddProfile<ProvizMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
                case Constants.MerchantIds.ProBikeKit:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CommonMapperProfile>();
                            cfg.AddProfile<ProBikeKitMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
                default:
                    {
                        var mapperConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CommonMapperProfile>();
                            cfg.AddProfile<DefaultDataFeedMapperProfile>();
                        });
                        return mapperConfig.CreateMapper();
                    }
            }
        }
    }
}
