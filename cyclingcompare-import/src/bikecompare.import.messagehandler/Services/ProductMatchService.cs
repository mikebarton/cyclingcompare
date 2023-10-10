using bikecompare.import.messagehandler.Domains.Categories;
using bikecompare.import.messagehandler.Domains.Product;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Services
{
    public class ProductMatchService
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly IMemoryCache _memoryCache;
        private const string GlobalProductsCacheKey = "AllGlobalProducts_";
        private const string GlobalProductIdsForMerchantIdCacheKey = "GlobalProductIds_";
        private const string CategoryMappingsCacheKey = "CategoryMappingsCacheKey_";

        public ProductMatchService(ProductService productService, CategoryService categoryService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _categoryService = categoryService;
            _memoryCache = memoryCache;
        }

        public async Task<IList<GlobalProduct>> FindMatchingGlobalProducts(Product product)
        {
            if (!_memoryCache.TryGetValue(GlobalProductsCacheKey, out IList<GlobalProduct> globalProducts))
            {
                var retrievedGlobalProducts = await _productService.GetAllGlobalProducts();
                _memoryCache.Set(GlobalProductsCacheKey, retrievedGlobalProducts, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)));
                globalProducts = retrievedGlobalProducts;
            }

            if (!_memoryCache.TryGetValue(GlobalProductIdsForMerchantIdCacheKey + product.MerchantId, out IList<ProductMapping> productMappings))
            {
                var retrieved = await _productService.GetMappedGlobalProductIdsForMerchantId(product.MerchantId);
                _memoryCache.Set(GlobalProductIdsForMerchantIdCacheKey + product.MerchantId, retrieved, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)));
                productMappings = retrieved;
            }

            if (!_memoryCache.TryGetValue(CategoryMappingsCacheKey + product.MerchantId, out IList<CategoryMapping> categoryMappings))
            {
                var retrievedCategoryMappings = await _categoryService.GetCategoryMappings();
                _memoryCache.Set(CategoryMappingsCacheKey + product.MerchantId, retrievedCategoryMappings, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)));
                categoryMappings = retrievedCategoryMappings;
            }            

            var matchingProducts = globalProducts.Where(x => DoProductsMatch(product, x, categoryMappings, productMappings)).ToList();
            return matchingProducts.ToList();
        }

        public async Task AddToGlobalProductCache(GlobalProduct globalProduct)
        {
            if (!_memoryCache.TryGetValue(GlobalProductsCacheKey, out IList<GlobalProduct> globalProducts))
            {
                var retrievedGlobalProducts = await _productService.GetAllGlobalProducts();
                globalProducts = retrievedGlobalProducts;
            }
            globalProducts.Add(globalProduct);
            _memoryCache.Set(GlobalProductsCacheKey, globalProducts, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)));
        }

        public async Task AddProductMappingToCache(string productId, string globalProductId, string merchantId)
        {
            if (!_memoryCache.TryGetValue(GlobalProductIdsForMerchantIdCacheKey + merchantId, out IList<ProductMapping> globalProductIds))
            {
                var retrieved = await _productService.GetMappedGlobalProductIdsForMerchantId(merchantId);
                globalProductIds = retrieved;
            }

            var productMapping = globalProductIds.SingleOrDefault(x => x.ProductId == productId);
            if (productMapping != null)
                productMapping.GlobalProductId = globalProductId;
            else
            {
                productMapping = new ProductMapping { MerchantId = merchantId, GlobalProductId = globalProductId, ProductId = productId };
                globalProductIds.Add(productMapping);
            }

            _memoryCache.Set(GlobalProductIdsForMerchantIdCacheKey + merchantId, globalProductIds, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(10)));
        }

        private bool DoProductsMatch(Product product, GlobalProduct globalProduct, IList<CategoryMapping> categoryMappings, IList<ProductMapping> productMappings)
        {
            var filterChars = new char[] { ' ', ',', ':', '.', ';', '"', '[', ']', '{', '}', '|' };
            if (!product.Brand.Equals(globalProduct.Brand, StringComparison.OrdinalIgnoreCase))
                return false;

            var categoryMapping = categoryMappings.Where(x => x.ExternalCategoryName.Equals(product.Category, StringComparison.OrdinalIgnoreCase) 
                                                        && (string.IsNullOrWhiteSpace(x.ExternalSubCategoryName) && string.IsNullOrWhiteSpace(product.SubCategory) 
                                                                || x.ExternalSubCategoryName.Equals(product.SubCategory, StringComparison.OrdinalIgnoreCase)));

            if (!categoryMapping.Any(x => x.CategoryId == globalProduct.CategoryId))
                return false;

            if (productMappings.Any(x => x.GlobalProductId == globalProduct.GlobalProductId && x.MerchantId == product.MerchantId))
                return false;

            var productNameWords = product.Name.ToLower().Split(filterChars, StringSplitOptions.RemoveEmptyEntries);
            var globalProductNameWords = globalProduct.Name.ToLower().Split(filterChars, StringSplitOptions.RemoveEmptyEntries);
            var matchingProductWordCount = productNameWords.Count(x => globalProductNameWords.Any(y => y.Equals(x, StringComparison.OrdinalIgnoreCase)));
            var matchingGlobalProductWordCount = globalProductNameWords.Count(x => productNameWords.Any(y => y.Equals(x, StringComparison.OrdinalIgnoreCase)));

            if (matchingProductWordCount / globalProductNameWords.Length < 0.8 &&
                matchingGlobalProductWordCount / productNameWords.Length < 0.8)
                return false;

            if (product.Price < globalProduct.Price * 0.8m || product.Price > globalProduct.Price * 1.2m)
                return false;

            return true;
        }
    }
}
