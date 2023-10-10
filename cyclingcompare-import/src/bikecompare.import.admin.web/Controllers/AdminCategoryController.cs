using AutoMapper;
using bikecompare.import.messagehandler.Domains.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Controllers
{
    [Route("Category")]
    [ApiController]
    [Authorize]
    public class AdminCategoryController : ControllerBase
    {
        private CategoryService _categoryService;
        private IMapper _mapper;
        public AdminCategoryController(CategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetExternalCategories")]
        public async Task<IList<ExternalCategoryModel>> GetExternalCategories()
        {
            var externalCats = await _categoryService.GetAllExternalCategoryNames();
            var models = externalCats.Select(x=> _mapper.Map<ExternalCategoryModel>(x)).ToList();
            return models;
        }

        [HttpGet]
        [Route("GetMappings")]
        public async Task<IList<CategoryMappingModel>> GetCategoryMappings()
        {
            var mappings = await _categoryService.GetCategoryMappings();
            var models = mappings.Select(x => _mapper.Map<CategoryMappingModel>(x)).ToList();
            return models;
        }

        [HttpPost]
        [Route("UpdateMapping")]
        public async Task UpdateCategoryMapping(CategoryMappingModel model)
        {
            await _categoryService.DeleteMapping(model.ExternalCategoryName, model.ExternalSubCategoryName, model.MerchantId);
            await _categoryService.AddMapping(Guid.NewGuid().ToString("N"), model.ExternalCategoryName, model.ExternalSubCategoryName, model.MerchantId, model.CategoryId);
        }
    }
}
