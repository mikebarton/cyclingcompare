using AutoMapper;
using bikecompare.import.impact.Models.Impact;
using bikecompare.import.messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Mapper
{
    public class DefaultMapperProfile : Profile
    {
        public DefaultMapperProfile()
        {
            CreateMap<ImpactProduct, Product>()
                .ForMember(dest => dest.ApiIdentifier, opt => opt.MapFrom(src => src.Unique_Merchant_SKU))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Manufacturer))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.Product_Type))
                .ForMember(dest => dest.Colour, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.ContentRating, opt => opt.Ignore())
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.DeliveryCost, opt => opt.MapFrom(src => src.Shipping_Rate))
                .ForMember(dest => dest.DeliveryTime, opt => opt.Ignore())
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src=>src.Product_Description))                
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image_URL))
                .ForMember(dest => dest.ModelNumber, opt => opt.MapFrom(src => src.MPN))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product_Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Current_Price))
                .ForMember(dest => dest.PriceRrp, opt => opt.MapFrom(src => src.Original_Price))
                .ForMember(dest => dest.PromoText, opt => opt.Ignore())
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Unique_Merchant_SKU))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.StockLevel, opt => opt.MapFrom(src => src.Stock_Availability))
                .ForMember(dest => dest.TargetUrl, opt => opt.MapFrom(src => src.Product_URL))
                .ForMember(dest => dest.TrackingCode, opt => opt.Ignore())
                .ForMember(dest => dest.TrackingUrl, opt => opt.MapFrom(src=>src.Product_URL))
                .ForMember(dest => dest.AgeRange, opt => opt.MapFrom(src => src.Age_Range))
                .ForMember(dest => dest.ProductWeight, opt => opt.MapFrom(src => src.Product_Weight))
                .ForMember(dest => dest.WeightUnitOfMeasurement, opt => opt.MapFrom(src => src.Weight_Unit_of_Measure))
                .ForMember(dest => dest.EAN, opt => opt.MapFrom(src => src.EAN))
                .ForMember(dest => dest.UPC, opt => opt.MapFrom(src => src.UPC));
        }

       
    }
}
