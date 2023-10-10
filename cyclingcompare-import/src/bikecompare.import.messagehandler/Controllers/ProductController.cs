using AutoMapper;
using bikecompare.imaging.messages;
using bikecompare.import.messagehandler.Domains.Categories;
using bikecompare.import.messagehandler.Domains.Merchant;
using bikecompare.import.messagehandler.Domains.Product;
using bikecompare.import.messagehandler.Services;
using bikecompare.messages;
using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;
using infrastructure.messaging;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dasync.Collections;

namespace bikecompare.import.messagehandler.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductMappingService _productMappingService;
        private ProductService _productService;
        private CategoryService _categoryService;
        private IMessagePublisher _publisher;
        private IMapper _mapper;
        private MerchantService _merchantService;
        private ImageHashService _hashService;
        private Func<ProductService> _productFactory;

        public ProductController(ProductMappingService productMappingService, ProductService productService, CategoryService categoryService, IMessagePublisher publisher, IMapper mapper, MerchantService merchantService, ImageHashService hashService, Func<ProductService> productFactory )
        {
            _productMappingService = productMappingService;
            _productService = productService;
            _categoryService = categoryService;
            _publisher = publisher;
            _mapper = mapper;
            _merchantService = merchantService;
            _hashService = hashService;
            _productFactory = productFactory;
        }
        

        [HttpGet]
        [Route("MapProducts/{merchantId}")]
        public async Task MapProducts(string merchantId)
        {
            await _productMappingService.MapProducts(merchantId);                        
        }

        [HttpGet]
        [Route("PublishMerchantListings/{merchantId}")]
        public async Task PublishMerchantListings(string merchantId)
        {
            var mappings = await _productService.GetAllProductMappingsByMerchant(merchantId);
            var products = await _productService.GetProductsForMerchant(merchantId);
            var productsAndMappings = products.Join(mappings, x => x.ProductId, x => x.ProductId, (x, y) => new { Product = x, Mapping = y }).ToList();
            var mappedProducts = productsAndMappings.Where(x => !string.IsNullOrWhiteSpace(x.Mapping.GlobalProductId));
            await mappedProducts.ParallelForEachAsync(async p =>
            {
                var message = _mapper.Map<AddMerchantProductMessage>(p.Product);
                message.GlobalProductId = p.Mapping.GlobalProductId;
                await _publisher.SendMessage(message);
            }, maxDegreeOfParallelism: 10);
        }

        [HttpGet]
        [Route("CalculateImageHash")]
        public async Task CalculateImageHash()
        {
            HttpClient httpClient = new HttpClient();
            var products = await _productService.GetUnhashedProductImages();
            var hashAlgorithm = new PerceptualHash();

            await products.ParallelForEachAsync(async item =>
            //foreach(var item in products)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(item.ImageUrl))
                    {
                        try
                        {
                            var hash = await _hashService.GetHash(item.ImageUrl);
                            if (string.IsNullOrWhiteSpace(hash))
                                return;

                            var serv = _productFactory();
                            await serv.UpdateProductImageHash(item.ProductId, hash);
                            
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"could not download and hash image ({item.ImageUrl}) - {e.Message}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error generating hash - {e.Message}");
                }
            }, maxDegreeOfParallelism: 5);
            
            
            //foreach (var item in products)
            //{
            //    await _publisher.SendMessage(new ProductImported() { ProductId = item.ProductId, ImageUrl = item.ImageUrl });
            //}
        }

        //[HttpPost]
        //[Route("StoreImageHash")]
        //public async Task StoreImageData(GcpMessage<ImageHashCalculated> gcpMessage)
        //{
        //    var message = gcpMessage.GetNestedMessage();
        //    await _productService.UpdateProductImageHash(message.ProductId, message.Hash);
        //}

        [HttpGet]
        [Route("UpdateGlobalProduct/{merchantId}")]
        public async Task UpdateGlobalProduct(string merchantId)
        {
            var mappings = await _productService.GetAllProductMappingsByMerchant(merchantId);
            var mappedProducts = mappings.Where(x => !string.IsNullOrEmpty(x.GlobalProductId));
            var products = await _productService.GetProductsForMerchant(merchantId);
            
            foreach (var mapping in mappings)
            {
                var product = products.First(x => x.ProductId == mapping.ProductId);
                var globalProductToUpdate = new GlobalProduct { GlobalProductId = mapping.GlobalProductId, ApiIdentifier = product.ApiIdentifier, Brand = product.Brand, Colour = product.Colour, ContentRating = product.ContentRating, Currency = product.Currency, DateModified = product.DateModified, DeliveryCost = product.DeliveryCost, DeliveryTime = product.DeliveryTime, Description = product.Description, Features = product.Features, Specs = product.Specs, Gender = product.Gender, ImageUrl = product.ImageUrl, ModelNumber = product.ModelNumber, Name = product.Name, Price = product.Price, PriceRrp = product.PriceRrp, PromoText = product.PromoText, Size = product.Size };
                await _productService.UpdateGlobalProduct(globalProductToUpdate);
            }
        }

        [HttpGet]
        [Route("PublishAll/{merchantId}")]
        public async Task PublishAll(string merchantId)
        {
            
            var globalProducts = await _productService.GetAllGlobalProductsByMerchant(merchantId);
            var products = await _productService.GetProductsForMerchant(merchantId);
            var mappingsForMerchant = await _productService.GetAllProductMappingsByMerchant(merchantId);
            var globalProductsForMerchant = globalProducts.Join(mappingsForMerchant.Where(x => !string.IsNullOrEmpty(x.GlobalProductId)), x => x.GlobalProductId, y => y.GlobalProductId, (x, y) => new { GlobalProduct = x, Mapping = y });

            foreach (var globalProduct in globalProductsForMerchant)
            {
                var gpMessage = _mapper.Map<AddProductMessage>(globalProduct.GlobalProduct);
                await _publisher.SendMessage(gpMessage);
                var product = products.First(x => x.ProductId == globalProduct.Mapping.ProductId);               
                
                var message = _mapper.Map<AddMerchantProductMessage>(product);
                message.GlobalProductId = globalProduct.Mapping.GlobalProductId;
                await _publisher.SendMessage(message);                
            }            
        }
    }
}
