using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_search.admin.api.Filters;
using product_search.admin.api.Product;
using product_search.admin.api.ProductSummaries.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace product_search.admin.api.Controllers
{
    [Authorize]
    [Route("AdminFilter")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly ILogger<FilterController> _logger;
        private readonly IMapper _mapper;
        private readonly FilterService _filterService;
        private readonly ProductService _productService;
        private readonly TempFilterService _tempFilterService;

        public FilterController(ILogger<FilterController> logger, IMapper mapper, FilterService filterService, ProductService productService, TempFilterService tempFilterService)
        {
            _logger = logger;
            _mapper = mapper;
            _filterService = filterService;
            _productService = productService;
            _tempFilterService = tempFilterService;
        }

        
        [HttpGet]
        [Route("GetFilterGroups/{categoryId}")]
        public async Task<List<CategoryFilterGroup>> GetFilterGroups(string categoryId)
        {
            var groups = await _filterService.GetFilterGroupsByCategoryId(categoryId);
            return groups;
        }

        [HttpDelete]
        [Route("DeleteFilterGroup/{groupId}")]
        public async Task DeleteFilterGroup(int groupId)
        {
            await _filterService.DeleteCategoryFilterGroup(groupId);
        }

        [HttpPost]
        [Route("AddOrUpdateFilterGroup")]
        public async Task AddOrUpdateFilterGroup(CategoryFilterGroup group)
        {
            group.FilterCode = FilterIds.SizeFilter;
            group.FilterType = FilterSelectionType.SelectMany;
            var existing = await _filterService.GetFilterGroupById(group.CategoryFilterGroupId);
            if (existing != null)
                await _filterService.UpdateCategoryFilterGroup(group);
            else
                await _filterService.AddCategoryFilterGroup(group);
        }

        [HttpPost]
        [Route("RelocateFilter/{filterId}/{targetGroup}")]
        public async Task RelocateFilter(int filterId, int targetGroup)
        {
            var existing = await _filterService.GetFilterById(filterId);
            if(existing != null)
            {
                var existingGroup = await _filterService.GetFilterGroupById(targetGroup);
                if(existingGroup != null)
                {
                    existing.CategoryFilterGroupId = targetGroup;
                    await _filterService.UpdateFilter(existing);
                }                
            }
        }

        [HttpGet]
        [Route("GetFilters/{categoryId}")]
        public async Task<List<CategoryFilterModel>> GetFilters([FromRoute]string categoryId, [FromQuery]int filterGroupId)
        {            
            var filters = await _filterService.GetFiltersByCategoryAndGroup(categoryId, filterGroupId);
            var models = filters.Select(x => _mapper.Map<CategoryFilterModel>(x)).ToList();
            return models;
        }


        [HttpPost]
        [Route("AddOrUpdateFilter")]
        public async Task AddOrUpdateFilter(CategoryFilterModel model)
        {
            var filter = _mapper.Map<CategoryFilter>(model);
            await _filterService.AddOrUpdateFilter(filter);
        }

        [HttpDelete]
        [Route("DeleteFilter")]
        public async Task DeleteFilter(int filterId)
        {
            await _filterService.DeleteFilter(filterId);
            await _filterService.DeleteTranslationsByFilterId(filterId);
        }

        [HttpGet]
        [Route("GetTranslationsByFilterId/{CategoryFilterId}")]
        public async Task<List<CategoryFilterTranslationModel>> GetFilterTranslations([FromRoute] int categoryFilterId)
        {
            var filterTranslations = await _filterService.GetCategoryFilterTranslationsByCategoryFilterId(categoryFilterId);
            var models = filterTranslations.Select<CategoryFilterTranslation, CategoryFilterTranslationModel>(x => _mapper.Map<CategoryFilterTranslationModel>(x)).ToList();
            return models;
        }        

        [HttpPost]
        [Route("AddOrUpdateTranslation")]
        public async Task AddOrUpdateTranslation(CategoryFilterTranslationModel model)
        {
            try
            {
                Regex.IsMatch("", model.Name);                
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"REGEX ISSUE: cannot save invalid translation {model.Name}");
                throw;
            }
            var translation = _mapper.Map<CategoryFilterTranslation>(model);
            await _filterService.AddOrUpdateFilterTranslation(translation);
        }

        [HttpDelete]
        [Route("DeleteTranslation/{categoryFilterTranslationId}")]
        public async Task DeleteTranslation(int categoryFilterTranslationId)
        {
            await _filterService.DeleteTranslationsById(categoryFilterTranslationId);
        }

        [HttpGet]
        [Route("GetTranslations/{filterGroupId}")]
        public async Task<List<TranslationViewModel>> GetTranslations(int filterGroupId, [FromQuery]bool unmapped = true)
        {
            var currentGroup = await _filterService.GetFilterGroupById(filterGroupId);
            if (currentGroup == null)
                return await Task.FromResult(new List<TranslationViewModel>());

            var products = await _productService.GetProductsByCategory(currentGroup.CategoryId);
            

            List<string> productVariants = new List<string>();
            switch (currentGroup.FilterCode)
            {
                case FilterIds.SizeFilter:
                    productVariants =  await _filterService.GetAllSizeFilters(currentGroup.CategoryId, filterGroupId);
                    break;
                case FilterIds.ColourFilter:
                    productVariants =  await _filterService.GetAllColourFilters(currentGroup.CategoryId, filterGroupId);
                    break;
                case FilterIds.GenderFilter:
                    productVariants =  await _filterService.GetAllGenderFilters(currentGroup.CategoryId, filterGroupId);
                    break;                
                default:
                    break;
            }

            
            var translations = new List<string>();
            switch (currentGroup.FilterCode)
            {
                case FilterIds.SizeFilter:
                    translations = await _filterService.GetMappedTranslationsByCategoryIdAndType(currentGroup.CategoryId, filterGroupId);
                    break;
                case FilterIds.ColourFilter:
                    translations = await _filterService.GetMappedTranslationsByCategoryIdAndType(currentGroup.CategoryId, filterGroupId);
                    break;
                case FilterIds.GenderFilter:
                    translations = await _filterService.GetMappedTranslationsByCategoryIdAndType(currentGroup.CategoryId, filterGroupId);
                    break;                    
                default:
                    break;
            }

            var variantNames = new List<string>();
            if(unmapped)
            {
                switch (currentGroup.FilterCode)
                {
                    case FilterIds.SizeFilter:
                        variantNames = products.Where(x=> !string.IsNullOrWhiteSpace(x.Size)).Where(x => translations.All(y => !Regex.IsMatch(x.Size, y))).Select(x => x.Size).ToList();
                        break;
                    case FilterIds.ColourFilter:
                        variantNames = products.Where(x => !string.IsNullOrWhiteSpace(x.Colour)).Where(x => translations.All(y => !Regex.IsMatch(x.Colour, y))).Select(x => x.Colour).ToList();
                        break;
                    case FilterIds.GenderFilter:
                        variantNames = products.Where(x => !string.IsNullOrWhiteSpace(x.Gender)).Where(x => translations.All(y => !Regex.IsMatch(x.Gender, y))).Select(x => x.Gender).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (currentGroup.FilterCode)
                {
                    case FilterIds.SizeFilter:
                        variantNames = products.Where(x => !string.IsNullOrWhiteSpace(x.Size)).Where(x => translations.Any(y => Regex.IsMatch(x.Size, y))).Select(x => x.Size).ToList();
                        break;
                    case FilterIds.ColourFilter:
                        variantNames = products.Where(x => !string.IsNullOrWhiteSpace(x.Colour)).Where(x => translations.Any(y => Regex.IsMatch(x.Colour, y))).Select(x => x.Colour).ToList();
                        break;
                    case FilterIds.GenderFilter:
                        variantNames = products.Where(x => !string.IsNullOrWhiteSpace(x.Gender)).Where(x => translations.Any(y => Regex.IsMatch(x.Gender, y))).Select(x => x.Gender).ToList();
                        break;
                    default:
                        break;
                }
            }
            

            Func<Product.Product, string, bool> prodFilter = (prod, keyword) =>
            {
                if (prod.PreviewImageUrl == null)
                    return false;

                switch (currentGroup.FilterCode)
                {
                    case FilterIds.SizeFilter:
                        return prod.Size != null && prod.Size.Equals(keyword);
                    case FilterIds.ColourFilter:
                        return prod.Colour != null && prod.Colour.Equals(keyword);
                    case FilterIds.GenderFilter:
                        return prod.Gender != null && prod.Gender.Equals(keyword);
                    default:
                        return false;
                }
            };

            var results = variantNames.Distinct().Select(x => new TranslationViewModel() { TranslationName = x, ImageUrls = products.Where(y => prodFilter(y, x)).Select(y => y.PreviewImageUrl).ToList() }).ToList();
            return results;
        }        
    }
}
