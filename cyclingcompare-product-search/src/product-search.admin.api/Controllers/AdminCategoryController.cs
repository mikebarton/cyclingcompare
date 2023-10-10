using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_search.api.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.Controllers
{
    [Authorize]
    [Route("AdminCategory")]
    [ApiController]
    public class AdminCategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<AdminCategoryController> _logger;
        private readonly IMapper _mapper;

        public AdminCategoryController(CategoryService categoryService, ILogger<AdminCategoryController> logger, IMapper mapper)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetFlatList")]
        public async Task<IList<CategoryModel>> GetFlattenedList()
        {
            var cats = await _categoryService.GetAllCategoriesAsync();
            var orderedCats = cats.OrderBy(x => x.Position).OrderBy(x => x.ParentId).ToList();
            var models = orderedCats.Select(x => _mapper.Map<CategoryModel>(x)).ToList();
            return models;
        }

        [HttpGet]
        [Route("GetHierarchy")]
        public async Task<IList<CategoryModel>> GetHierarchy()
        {
            var cats = await _categoryService.GetAllCategoriesAsync();
            var orderedCats = cats.OrderBy(x => x.Position).OrderBy(x => x.ParentId).ToList();
            var nestedCats = new List<CategoryModel>();
            foreach (var cat in orderedCats)
            {
                var model = _mapper.Map<CategoryModel>(cat);
                if (model.ParentId == "0")
                    nestedCats.Add(model);
                else
                {
                    foreach (var rootItem in nestedCats)
                    {
                        rootItem.TryAddChild(model);
                    }
                }
            }
            return nestedCats;
        }

        [HttpPost]
        [Route("UpdateCategory")]
        public async Task UpdateCategory(CategoryModel category)
        {
            if (!string.IsNullOrEmpty(category.CategoryId))
            {
                await _categoryService.UpdateCategory(category.CategoryId, category.Title, category.ParentId, category.Position, category.Description, category.IsEnabled, category.UrlSlug);
            }
        }

        [HttpDelete]
        [Route("DeleteCategory/{categoryId}")]
        public async Task DeleteCategory(string categoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                await _categoryService.DeleteCategory(categoryId);
                await _categoryService.DeleteProductCategoryMapping(categoryId);
            }
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<CategoryModel> CreateCategory(CategoryModel category)
        {
            if (category == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            if (string.IsNullOrEmpty(category.ParentId))
                category.ParentId = "0";
            category.CategoryId = Guid.NewGuid().ToString("N");
            await _categoryService.InsertCategory(category.CategoryId, category.Title, category.ParentId, category.Position, category.Description, true, category.UrlSlug, category.CategoryBannerImage);
            return category;
        }

    }
}
