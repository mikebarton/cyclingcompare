using AutoMapper;
using bikecompare.import.impact.Extensions;
using bikecompare.import.impact.Models.Impact;
using bikecompare.import.messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Mapper
{
    public class ChainReactionMapperProfile : Profile
    {
        public ChainReactionMapperProfile()
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
                .ForMember(dest => dest.Description, opt => opt.MapFrom((impactProduct, product) =>
                {
                    if (impactProduct == null || string.IsNullOrEmpty(impactProduct.Product_Description))
                        return null;

                    return ParseDescription(impactProduct.Product_Description);
                }))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom((impactProduct, product) =>
                {
                    if (impactProduct == null || string.IsNullOrEmpty(impactProduct.Product_Description))
                        return null;

                    return ParseSpec(impactProduct.Product_Description);
                }))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Image_URL))
                .ForMember(dest => dest.ModelNumber, opt => opt.MapFrom(src => src.MPN))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product_Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Current_Price))
                .ForMember(dest => dest.PriceRrp, opt => opt.MapFrom(src => src.Original_Price))
                .ForMember(dest => dest.PromoText, opt => opt.Ignore())
                .ForMember(dest => dest.Sku, opt => opt.MapFrom(src => src.Unique_Merchant_SKU))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.StockLevel, opt => opt.Ignore())
                .ForMember(dest => dest.HasStock, opt => opt.MapFrom(src => string.Equals(src.Stock_Availability, "Y", StringComparison.OrdinalIgnoreCase)))
                .ForMember(dest => dest.TargetUrl, opt => opt.MapFrom(src => src.Product_URL))
                .ForMember(dest => dest.TrackingCode, opt => opt.Ignore())
                .ForMember(dest => dest.TrackingUrl, opt => opt.MapFrom(src=>src.Product_URL))
                .ForMember(dest => dest.AgeRange, opt => opt.MapFrom(src => src.Age_Range))
                .ForMember(dest => dest.ProductWeight, opt => opt.MapFrom(src => src.Product_Weight))
                .ForMember(dest => dest.WeightUnitOfMeasurement, opt => opt.MapFrom(src => src.Weight_Unit_of_Measure))
                .ForMember(dest => dest.EAN, opt => opt.MapFrom(src => src.EAN))
                .ForMember(dest => dest.UPC, opt => opt.MapFrom(src => src.UPC));
        }

        private string ParseDescription(string description)
        {
            try
            {
                var index = description.IndexOf("Features:", StringComparison.OrdinalIgnoreCase);
                if (index > 0)
                {
                    var preFeatures = index >= 0 ? description.Substring(0, index) : description;
                    var featuresOn = description.Substring(index + "Features:".Length);
                    var matches = Regex.Matches(featuresOn, @"(?<preslash>[A-Z][a-z]+/)*(?<prespace>[A-Z][a-z]+\s)*[A-Z][a-z]*:");

                    if (matches.Count > 0)
                    {
                        var lastMatch = matches[matches.Count - 1];
                        var remainder = featuresOn.Substring(lastMatch.Index + lastMatch.Length);
                        var lastSpecMatch = Regex.Match(remainder, ".+[a-z](?=[A-Z])");
                        var postFeatures = remainder.Substring(lastSpecMatch.Index + lastSpecMatch.Length);
                        var edittedDescription = preFeatures + postFeatures;
                        return string.Join(". ", Regex.Split(edittedDescription, "\\.(?=[A-Z])"));
                    }
                }
                return string.Join(". ", Regex.Split(description, "\\.(?=[A-Z])"));
            }
            catch (Exception e)
            {
                Console.WriteLine(description);
                throw;
            }
        }

        // Define other methods and classes here
        private Dictionary<string, string> ParseSpec(string description)
        {
            try
            {
                var index = description.IndexOf("Features:", StringComparison.OrdinalIgnoreCase);
                if (index > 0)
                {
                    description = description.Replace("&nbsp;", "");
                    description = description.Replace("&quot;", "\"");
                    var preFeatures = index >= 0 ? description.Substring(0, index) : description;
                    var featuresOn = description.Substring(index + "Features:".Length);
                    var matches = Regex.Matches(featuresOn, @"(?<preslash>[A-Z][a-z]+/)*(?<prespace>[A-Z][a-z]+\s)*[A-Z][a-z]*:");
                    if (matches.Count > 0)
                    {
                        var lastMatch = matches[matches.Count - 1];
                        var remainder = featuresOn.Substring(lastMatch.Index + lastMatch.Length);
                        var matchList = matches.Cast<Match>();

                        var specDict = matchList.Select((item, i) => new { Match = item, NextMatch = matchList.Count() > i + 1 ? matchList.ToList()[i + 1] : null, })
                        .Select(l => new { Header = l.Match.Value, Value = l.NextMatch == null ? Regex.Match(featuresOn.Substring(l.Match.Index + l.Match.Length), @"[a-zA-Z0-9.\s]+([a-z][A-Z])*")?.Value : featuresOn.Substring(l.Match.Index + l.Match.Length, l.NextMatch.Index - (l.Match.Index + l.Match.Length)) }).ToSafeDictionary(x=>x.Header, x=>x.Value);
                        return specDict;
                        
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(description);
                throw;
            }
        }
    }
}
