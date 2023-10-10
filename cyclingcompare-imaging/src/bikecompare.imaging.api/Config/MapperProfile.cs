using AutoMapper;
using bikecompare.imaging.api.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.api.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Banner, BannerModel>();
            //CreateMap<Category, CategoryModel>();
            //CreateMap<ProductSummary, ProductSummaryModel>();
        }
    }
}
