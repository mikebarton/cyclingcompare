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
    public class FindSportsMapperProfile : Profile
    {
        public FindSportsMapperProfile()
        {
            CreateMap<DataFeedItem, Product>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom((feedItem, product) =>
                {
                    if (feedItem == null || string.IsNullOrEmpty(feedItem.Description))
                        return null;

                    var index = feedItem.Description.IndexOf("Features: ", StringComparison.OrdinalIgnoreCase);
                    var textWithoutFeatures = index >= 0 ? feedItem.Description.Substring(0, index) : feedItem.Description;
                    var indexOfSpec = textWithoutFeatures.IndexOf("Specifications: ", StringComparison.OrdinalIgnoreCase);
                    return indexOfSpec >= 0 ? textWithoutFeatures.Substring(0, indexOfSpec) : textWithoutFeatures;
                }))
                .ForMember(dest => dest.Features, opt => opt.MapFrom((feedItem, product) =>
                {
                    if (feedItem == null || string.IsNullOrEmpty(feedItem.Description))
                        return null;

                    var index = feedItem.Description.IndexOf("Features: ", StringComparison.OrdinalIgnoreCase);
                    var afterDescriptionString = index >= 0 ? feedItem.Description.Substring(index + "Features: ".Length) : string.Empty;
                    var indexOfSpec = afterDescriptionString.IndexOf("Specifications: ", StringComparison.OrdinalIgnoreCase);

                    var featureString = indexOfSpec >= 0 ? afterDescriptionString.Substring(0, indexOfSpec) : afterDescriptionString;
                    var featureItems = ParseList(featureString).ToList();
                    return featureItems;
                }))
                .ForMember(dest => dest.Specs, opt => opt.MapFrom((feedItem, product) =>
                {
                    if (feedItem == null || string.IsNullOrEmpty(feedItem.Description))
                        return null;

                    var indexOfSpec = feedItem.Description.IndexOf("Specifications: ", StringComparison.OrdinalIgnoreCase);
                    var specString = indexOfSpec >= 0 ? feedItem.Description.Substring(indexOfSpec + "Specifications: ".Length) : string.Empty;

                    var phrases = ParseList(specString).ToList();

                    var specs = BuildSpecs(phrases);
                    return specs;
                }));


        }

        Dictionary<string, string> BuildSpecs(List<string> lines)
        {
            var result = new Dictionary<string, string>();
            string pendingPhrase = null;

            Action<string> addToResult = line =>
            {
                var parts = line.Split(':');
                if (parts.Length != 2) return;
                result.Add(parts[0], parts[1]);
            };

            foreach (var p in lines.Select(p => p.Trim()))
            {
                if (p.Split(' ').Length < 2)
                    pendingPhrase = p;
                else
                {
                    if (pendingPhrase != null)
                    {
                        addToResult((pendingPhrase + " " + p));
                        pendingPhrase = null;
                    }
                    else
                        addToResult(p);
                }
            }
            return result;
        }

        IEnumerable<string> ParseList(string source)
        {
            var text = source;
            var uppercaseRegex = new Regex("[A-Z]");
            var numeralRegex = new Regex("[0-9]");
            var latestIndex = 0;

            while (text.Length > 1)
            {
                var phrase = text.TakeWhile((letter, index) =>
                {
                    latestIndex = index;
                    if (index == 0) return true;
                    if (!uppercaseRegex.IsMatch(letter.ToString()))
                        return true;

                    if (uppercaseRegex.IsMatch(letter.ToString()))
                    {
                        if (uppercaseRegex.IsMatch(text[index - 1].ToString()))
                            return true;
                        if (numeralRegex.IsMatch(text[index - 1].ToString()))
                            return true;
                        if (text[index - 1] != ' ')
                            return true;
                        if (text[index - 1] == ' ' && (index - 2) > 0 && text[index - 2] == ',')
                            return true;
                        if (text[index - 1] == ' ' && (index - 2) > 0 && text[index - 2] == ':')
                            return true;
                        if (text[index - 1] == ' ' && (index - 2) > 0 && text[index - 2] == '-')
                            return true;
                        if (text[index - 1] == ' ' && (index - 2) > 0 && uppercaseRegex.IsMatch(text[index - 2].ToString()))
                            return true;

                    }

                    return false;
                }).ToList();

                text = text.Substring(latestIndex);
                yield return new string(phrase.ToArray()); ;
            }
        }
    }
}
