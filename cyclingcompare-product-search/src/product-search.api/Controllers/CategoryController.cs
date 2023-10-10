using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_search.api.Categories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;

        public CategoryController(CategoryService categoryService, ILogger<CategoryController> logger, IMapper mapper)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IList<CategoryModel>> GetAll()
        {
            _logger.LogInformation("retreiving all categories");
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
    }
}
