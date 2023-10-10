using AutoMapper;
using bikecompare.import.messagehandler.Domains.Categories;
using bikecompare.import.messagehandler.Domains.Product;
using bikecompare.messages;
using infrastructure.messaging;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Services
{
    public class ProductMappingService
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly IMemoryCache _memoryCache;
        private IMapper _mapper;
        private IMessagePublisher _publisher;

        private const string CategoryMappingsCacheKey = "CategoryMappingsCacheKey_";
        private const string GlobalProductMappingsForMerchantIdCacheKey = "GlobalProductMappings_";
        private const string GlobalProductsCacheKey = "AllGlobalProducts";
        private const string ProductsForMerchantCachesKey = "ProductsForMerchant_";

        public ProductMappingService(ProductService productService, CategoryService categoryService, IMemoryCache memoryCache, IMapper mapper, IMessagePublisher publisher)
        {
            _productService = productService;
            _categoryService = categoryService;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _publisher = publisher;
        }

        public async Task MapProducts(string merchantId)
        {
            var allProducts = await GetAllProductsForMerchant(merchantId);
            var mappings = await GetProductMappingsForMerchant(merchantId);
            var unmappedProducts = allProducts.Join(mappings, x => x.ProductId, x => x.ProductId, (x, y) => new { Product = x, Mapping = y })
                                                .Where(x => string.IsNullOrWhiteSpace(x.Mapping.GlobalProductId) && !string.IsNullOrWhiteSpace(x.Product.ImageHash))
                                                .Select(x => x.Product);

            foreach (var productToMap in unmappedProducts)
            {
                if (!string.IsNullOrWhiteSpace(productToMap.ImageHash))
                {
                    var productsWithMatchingImage = allProducts.Where(x => x.ProductId != productToMap.ProductId && x.ImageHash == productToMap.ImageHash).ToList();
                    var mappingsForProducts = productsWithMatchingImage.Select(x => mappings.First(y => y.ProductId == x.ProductId)).ToList();
                    var mappingsThatHaveGlobalProduct = mappingsForProducts.Where(x => !string.IsNullOrWhiteSpace(x.GlobalProductId)).ToList();                    

                    if (mappingsThatHaveGlobalProduct.Count > 0)
                    {
                        await MapToExistingGlobalProduct(productToMap, mappingsThatHaveGlobalProduct[0].GlobalProductId);
                        continue;
                    }
                }
                
                await TryMapProductToGlobalProduct(productToMap);
            }
        }

        

        private async Task TryMapProductToGlobalProduct(Product product)
        {
            var categoryMappings = await GetCategoryMappings();
            var categoryMapping = categoryMappings.SingleOrDefault(x => x.MerchantId == product.MerchantId && x.ExternalCategoryName.Equals(product.Category, StringComparison.OrdinalIgnoreCase) && x.ExternalSubCategoryName.Equals(product.SubCategory, StringComparison.OrdinalIgnoreCase));
            if (categoryMapping == null)
            {
                Console.WriteLine($"cannot map product {product.ProductId}. There is no Category Mapping for {product.Category} - {product.SubCategory ?? "<no-sub-cat>"}");
                return;
            }

            var productMappings = await GetProductMappingsForMerchant(product.MerchantId);
            if(productMappings.Any(x=>x.ProductId == product.ProductId && x.MerchantId == product.MerchantId && !string.IsNullOrEmpty(x.GlobalProductId)))
            {
                Console.WriteLine($"cannot map product {product.ProductId}. It is already mapped to GlobalProduct");
                return;
            }

            var globalProducts = await GetAllGlobalProducts();
            var matchingProducts = globalProducts.Where(x => DoProductsMatch(product, x, categoryMappings, productMappings)).ToList();
            if (matchingProducts == null || matchingProducts.Count() == 0)
            {
                await AddGlobalProduct(product, categoryMapping);
            }
            else if (matchingProducts.Count() == 1)
            {
                await MapToExistingGlobalProduct(product, matchingProducts[0].GlobalProductId);
            }
            else if(matchingProducts.Count() > 1)
            {
                Console.WriteLine($"Cannot map product {product.ProductId} because it maps to more than one GlobalProduct");
            }
        }

        private void SetProductSlugs(GlobalProduct globalProduct)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 ]");
            var cleaned = rgx.Replace(globalProduct.Name, "");
            var words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var slug = string.Join('-', words);
            globalProduct.UrlSlug = slug;
        }

        private async Task MapToExistingGlobalProduct(Product product, string globalProductId)
        {
            await _productService.UpdateProductMapping(product.ProductId, globalProductId, false);
            await AddProductMappingToCache(product.ProductId, globalProductId, product.MerchantId);
            var merchantProductMessage = _mapper.Map<AddMerchantProductMessage>(product);
            merchantProductMessage.GlobalProductId = globalProductId;
            await _publisher.SendMessage(merchantProductMessage);
        }

        private async Task<GlobalProduct> AddGlobalProduct(Product product, CategoryMapping categoryMapping)
        {
            var newGlobalProduct = new GlobalProduct { GlobalProductId = Guid.NewGuid().ToString("N"), ApiIdentifier = product.ApiIdentifier, Brand = product.Brand, CategoryId = categoryMapping.CategoryId, Colour = product.Colour, ContentRating = product.ContentRating, Currency = product.Currency, DateModified = product.DateModified, DeliveryCost = product.DeliveryCost, DeliveryTime = product.DeliveryTime, Description = product.Description, Features = product.Features, Specs = product.Specs, Gender = product.Gender, ImageUrl = product.ImageUrl, ModelNumber = product.ModelNumber, Name = product.Name, Price = product.Price, PriceRrp = product.PriceRrp, PromoText = product.PromoText, Size = product.Size };
            SetProductSlugs(newGlobalProduct);
            await _productService.InsertGlobalProduct(newGlobalProduct.GlobalProductId, product.Brand, product.Colour, product.ContentRating, categoryMapping.CategoryId, product.Currency, product.DateModified, product.DeliveryCost, product.DeliveryTime, product.Description, product.Features, product.Specs, product.Gender, product.ImageUrl, product.ModelNumber, product.Name, newGlobalProduct.UrlSlug, product.Price, product.PriceRrp, product.PromoText, product.Size, product.EAN, product.UPC);
            await _productService.UpdateProductMapping(product.ProductId, newGlobalProduct.GlobalProductId, true);
            await AddToGlobalProductCache(newGlobalProduct);
            await AddProductMappingToCache(product.ProductId, newGlobalProduct.GlobalProductId, product.MerchantId);
            var addProductMessage = _mapper.Map<AddProductMessage>(newGlobalProduct);
            await _publisher.SendMessage(addProductMessage);
            var merchantProductMessage = _mapper.Map<AddMerchantProductMessage>(product);
            merchantProductMessage.GlobalProductId = newGlobalProduct.GlobalProductId;
            await _publisher.SendMessage(merchantProductMessage);
            return newGlobalProduct;
        }

        private async Task<IList<Product>> GetAllProductsForMerchant(string merchantId)
        {
            if(!_memoryCache.TryGetValue(ProductsForMerchantCachesKey + merchantId, out IList<Product> products))
            {
                var retrievedProducts = await _productService.GetProductsForMerchant(merchantId);
                _memoryCache.Set(ProductsForMerchantCachesKey + merchantId, retrievedProducts, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(120)));
                products = retrievedProducts;
            }
            return products;
        }

        private async Task AddToGlobalProductCache(GlobalProduct globalProduct)
        {
            if (!_memoryCache.TryGetValue(GlobalProductsCacheKey, out IList<GlobalProduct> globalProducts))
            {
                var retrievedGlobalProducts = await _productService.GetAllGlobalProducts();
                globalProducts = retrievedGlobalProducts;
            }
            globalProducts.Add(globalProduct);
            _memoryCache.Set(GlobalProductsCacheKey, globalProducts, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(120)));
        }

        private async Task AddProductMappingToCache(string productId, string globalProductId, string merchantId)
        {
            if (!_memoryCache.TryGetValue(GlobalProductMappingsForMerchantIdCacheKey + merchantId, out IList<ProductMapping> globalProductIds))
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

            _memoryCache.Set(GlobalProductMappingsForMerchantIdCacheKey + merchantId, globalProductIds, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(120)));
        }

        private bool DoProductsMatch(Product product, GlobalProduct globalProduct, IList<CategoryMapping> categoryMappings, IList<ProductMapping> productMappings)
        {
            if (!string.IsNullOrWhiteSpace(globalProduct.EAN) && !string.IsNullOrWhiteSpace(product.EAN))
                return globalProduct.EAN.Equals(product.EAN, StringComparison.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(globalProduct.UPC) && !string.IsNullOrWhiteSpace(product.UPC))
                return globalProduct.UPC.Equals(product.UPC, StringComparison.OrdinalIgnoreCase);

            if (!product.Brand?.Equals(globalProduct.Brand, StringComparison.OrdinalIgnoreCase) ?? false)
                return false;

            var categoryMapping = categoryMappings.Where(x => x.ExternalCategoryName.Equals(product.Category, StringComparison.OrdinalIgnoreCase)
                                                        && (string.IsNullOrWhiteSpace(x.ExternalSubCategoryName) && string.IsNullOrWhiteSpace(product.SubCategory)
                                                                || x.ExternalSubCategoryName.Equals(product.SubCategory, StringComparison.OrdinalIgnoreCase)));
            
            if (productMappings.Any(x => x.GlobalProductId == globalProduct.GlobalProductId && x.MerchantId == product.MerchantId))
                return false;

            var filterChars = new char[] { ' ', ',', ':', '.', ';', '"', '[', ']', '{', '}', '|' };


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

        private async Task<IList<CategoryMapping>> GetCategoryMappings()
        {
            if (!_memoryCache.TryGetValue(CategoryMappingsCacheKey, out IList<CategoryMapping> categoryMappings))
            {
                var retrievedCategoryMappings = await _categoryService.GetCategoryMappings();
                _memoryCache.Set(CategoryMappingsCacheKey, retrievedCategoryMappings, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(120)));
                categoryMappings = retrievedCategoryMappings;
            }

            return categoryMappings;
        }

        private async Task<IList<GlobalProduct>> GetAllGlobalProducts()
        {
            if (!_memoryCache.TryGetValue(GlobalProductsCacheKey, out IList<GlobalProduct> globalProducts))
            {
                var retrievedGlobalProducts = await _productService.GetAllGlobalProducts();
                _memoryCache.Set(GlobalProductsCacheKey, retrievedGlobalProducts, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(120)));
                globalProducts = retrievedGlobalProducts;
            }

            return globalProducts;
        }

        private async Task<IList<ProductMapping>> GetProductMappingsForMerchant(string merchantId)
        {
            if (!_memoryCache.TryGetValue(GlobalProductMappingsForMerchantIdCacheKey + merchantId, out IList<ProductMapping> productMappings))
            {
                var retrieved = await _productService.GetMappedGlobalProductIdsForMerchantId(merchantId);
                _memoryCache.Set(GlobalProductMappingsForMerchantIdCacheKey + merchantId, retrieved, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(120)));
                productMappings = retrieved;
            }

            return productMappings;
        }
    }
}
