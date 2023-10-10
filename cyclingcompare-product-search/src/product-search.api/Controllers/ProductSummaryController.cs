using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using product_search.api.Extensions;
using product_search.api.ProductSummaries;
using product_search.api.ProductSummaries.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.api.Controllers
{
    [Route("ProductSummary")]
    [ApiController]
    public class ProductSummaryController : ControllerBase
    {
        private readonly ILogger<ProductSummaryController> _logger;
        private readonly IMapper _mapper;
        private readonly ProductSummaryService _productSummaryService;
        private readonly IFilterService _filterService;
        private readonly ProductFilterFactory _filterFactory;
        private readonly IMemoryCache _memoryCache;

        public ProductSummaryController(ProductSummaryService productSummaryService, ILogger<ProductSummaryController> logger, IMapper mapper, IFilterService filterService, ProductFilterFactory filterFactory, IMemoryCache cache)
        {
            _productSummaryService = productSummaryService;
            _logger = logger;
            _mapper = mapper;
            _filterService = filterService;
            _filterFactory = filterFactory;
            _memoryCache = cache;
        }

        [HttpGet]
        [Route("GetTopDealsByCategory/{categoryId}")]
        public async Task<IList<ProductSummaryModel>> GetTopDealsByCategory(int categoryId)
        {
            var products = await _productSummaryService.GetTopDealsByCategoryId(categoryId);
            var productModels = _mapper.Map<List<ProductSummaryModel>>(products);
            return productModels;
        }
        
        
        [HttpGet]
        [Route("GetPagedByCategoryId/ps/{pageSize}/pn/{pageNum}/so/{sortOrder}/cid/{categoryId}")]
        public async Task<dynamic> GetPagedProductsByCategory(int pageSize, int pageNum, Constants.SortOrder sortOrder, string categoryId)
        {
            var productCount = await _productSummaryService.GetProductCountByCategory(categoryId);
            var products = await _productSummaryService.GetPagedByCategory(pageSize, pageNum, sortOrder, categoryId);
            var productModels = _mapper.Map<List<ProductSummaryModel>>(products);
            return new { TotalCount = productCount, Products = productModels, PageSize = pageSize, PageNum = pageNum, SortOrder = (int)sortOrder };
        }

        [HttpGet]
        [Route("Search/{keywords}")]
        [EnableCors("AllowAll")]
        public async Task<IList<ProductSummaryModel>> GetByKeyword(string keywords)
        {
            var likedKeyword = $"%{keywords}%";
            var results = await _productSummaryService.GetByKeywords(likedKeyword);
            var productModels = _mapper.Map<List<ProductSummaryModel>>(results);
            return productModels;
        }

        [HttpGet]
        [Route("GetFilters")]
        public async Task<IList<FilterViewModel>> GetFilters(string categoryId)
        {            
            var filters = await _filterService.GetFilterData(categoryId);
            var vmTasks = filters.Select(async x =>
            {
                var viewModel = _mapper.Map<FilterViewModel>(x);
                var filterProvider = _filterFactory.Create(viewModel);
                viewModel.FilterOptions = await filterProvider.GetFilterOptions(categoryId, x.CategoryFilterGroupId);
                return viewModel;
            });

            var vms = await Task.WhenAll(vmTasks);
            
            return vms;
        }
        
        [HttpPost]
        [Route("FilterProducts/ps/{pageSize}/pn/{pageNum}/so/{sortOrder}/cid/{categoryId}")]
        public async Task<dynamic> FilterProducts([FromBody]List<FilterViewModel> filters, [FromRoute]int pageSize, [FromRoute]int pageNum, [FromRoute]Constants.SortOrder sortOrder, [FromRoute]string categoryId)
        {
            var filteredProductTasks = filters?.Select(x =>
            {
                var filterProvider = _filterFactory.Create(x);
                var filteredProducts = filterProvider.GetFilteredProducts(categoryId, x);
                return filteredProducts;
            });

            var taskResults = await Task.WhenAll(filteredProductTasks);
            var filteredProducts = taskResults.Select(x => x.ToList())
            .Aggregate((a, b) => a.Intersect(b).ToList())
            .DistinctBy(x => x.ProductId)
            .ToList();

            var totalCount = filteredProducts.Count();
            List<ProductSummary> ordered = null;
            switch (sortOrder)
            {
                case Constants.SortOrder.NameAsc:
                    ordered = filteredProducts.OrderBy(x => x.ProductName).ToList();
                    break;
                case Constants.SortOrder.NameDesc:
                    ordered = filteredProducts.OrderByDescending(x => x.ProductName).ToList();
                    break;
                case Constants.SortOrder.PriceAsc:
                    ordered = filteredProducts.OrderBy(x => x.MinPrice).ToList();
                    break;
                case Constants.SortOrder.PriceDesc:
                    ordered = filteredProducts.OrderByDescending(x => x.MinPrice).ToList();
                    break;
                default:
                    break;
            }

            if (pageSize > 90)
                pageSize = 90;

            if (pageNum < 0)
                pageNum = 0;

            if (pageSize * (pageNum + 1) >= ordered.Count + pageSize)
                pageNum = Math.Max((ordered.Count / pageSize) - 1, 0);

            return new { TotalCount = totalCount, Products = ordered.Skip(pageNum * pageSize).Take(pageSize).ToList(), PageSize = pageSize, PageNum = pageNum, SortOrder = (int)sortOrder };
        }
        
    }    
}
