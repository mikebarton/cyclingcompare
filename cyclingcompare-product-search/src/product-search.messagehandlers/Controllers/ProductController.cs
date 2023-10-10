using AutoMapper;
using bikecompare.imaging.messages;
using bikecompare.messages;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Mvc;
using product_search.messagehandlers.Domains.Product;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace product_search.messagehandlers.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("HandlePublishProduct")]
        public async Task HandlePublishProduct(GcpMessage<AddProductMessage> gcpMessage)
        {
            var message = gcpMessage.GetNestedMessage();
            var product = _mapper.Map<Product>(message);
            var existingProduct = await _productService.GetProductById(product.ProductId);
            if (existingProduct == null)
            {
                await _productService.SaveProduct(product);
                await _productService.SaveProductCategory(Guid.NewGuid().ToString("N"), product.ProductId, message.CategoryId);
            }
            else
            {
                await _productService.UpdateProduct(product);
                await _productService.UpdateProductCategory(product.ProductId, message.CategoryId);
            }

        }

        [HttpPost]
        [Route("ProductSummaryImageResized")]
        public async Task HandleProductSummaryImageResized(GcpMessage<ProductSummaryImageResized> gcpMessage)
        {
            var message = gcpMessage.GetNestedMessage();
            var existingProduct = await _productService.GetProductById(message.ProductId);
            if (existingProduct == null)
                throw new Exception("Product does not exist in this service. Let's retry again in a little while");

            await _productService.UpdateProductImageUrl(message.ProductId, message.ImageUrl);
        }

        [HttpPost]
        [Route("HandleAddMerchantProduct")]
        public async Task HandleAddMerchantProduct(GcpMessage<AddMerchantProductMessage> gcpMessage)
        {
            var message = gcpMessage.GetNestedMessage();
            var existingProduct = await _productService.GetProductById(message.GlobalProductId);
            if (existingProduct == null)
                throw new InvalidOperationException("cannot save merchant product for product that doesn't exist - " + message.GlobalProductId);

            await _productService.DeleteMerchantProduct(message.GlobalProductId, message.MerchantId, message.ProductId);
            await _productService.InsertMerchantProduct(message.GlobalProductId, message.MerchantId, message.Price, message.ProductId, message.PriceRrp, message.Size, message.Colour, message.Gender, message.HasStock, message.StockLevel);

            if(existingProduct.MinPrice > message.Price)
            {
                existingProduct.MinPrice = (int)message.Price;                
                await _productService.UpdateProduct(existingProduct);
            }
        }

        [HttpPost]
        [Route("HandleRemoveMerchantProduct")]
        public async Task HandleRemoveMerchantProduct(GcpMessage<RemoveMerchantProductMessage> gcpMessage)
        {
            var message = gcpMessage.GetNestedMessage();
            await _productService.DeleteMerchantProduct(message.GlobalProductId, message.MerchantId, message.ProductId);

            var merchantProducts = await _productService.GetMerchantProductsForProduct(message.GlobalProductId);
            if (merchantProducts == null || !merchantProducts.Any())
            {
                await _productService.DeleteProductSummary(message.GlobalProductId);
                await _productService.DeleteProductCategory(message.GlobalProductId);
            }
            else
            {
                var existingProduct = await _productService.GetProductById(message.GlobalProductId);
                if (existingProduct != null)
                {
                    existingProduct.MinPrice = merchantProducts != null && merchantProducts.Count > 0 ? (int)merchantProducts.Min(x => x.Price) : 9999;
                    await _productService.UpdateProduct(existingProduct);
                }
            }
        }

        [HttpPost]
        [Route("HandleRemoveProduct")]
        public async Task HandleRemoveProduct(GcpMessage<RemoveProductMessage> gcpMessage)
        {
            var message = gcpMessage.GetNestedMessage();
            await _productService.DeleteProductSummary(message.GlobalProductId);
            await _productService.DeleteProductCategory(message.GlobalProductId);
            await _productService.DeleteAllMerchantProductsForProductId(message.GlobalProductId);
        }
    }
}
