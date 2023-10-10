using AutoMapper;
using bikecompare.import.admin.web.Domains.Products;
using bikecompare.messages;
using infrastructure.messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.admin.web.Controllers
{
    [Route("GlobalProduct")]
    [ApiController]
    [Authorize]
    public class AdminGlobalProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;
        private readonly IMessagePublisher _publisher;

        public AdminGlobalProductController(ProductService productService, IMapper mapper, IMessagePublisher publisher)
        {
            _productService = productService;
            _mapper = mapper;
            _publisher = publisher;
        }

        [HttpGet]
        [Route("GetAllProductSummaries")]
        public async Task<List<ProductSummaryViewModel>> GetAllProductSummaries()
        {
            var products = await _productService.GetAllProductSummaries();
            var viewmodels = products.Select(x => _mapper.Map<ProductSummaryViewModel>(x)).ToList();
            return viewmodels;
        }

        [HttpGet]
        [Route("GetProductDetails/{globalProductId}")]
        public async Task<ProductDetailsViewModel> GetProductDetails(string globalProductId)
        {
            var product = await _productService.GetGlobalProduct(globalProductId);
            var merchantProducts = await _productService.GetMerchantProductsByGlobalId(globalProductId);
            var productVm = _mapper.Map<ProductDetailsViewModel>(product);
            var merchantProductVms = merchantProducts.Select(x => _mapper.Map<MerchantProductViewModel>(x)).ToList();
            productVm.MerchantProducts = merchantProductVms;
            return productVm;
        }

        [HttpPost]
        [Route("UpdateGlobalProduct")]
        public async Task UpdateGlobalProduct(ProductDetailsViewModel viewmodel)
        {
            var product = _mapper.Map<Product>(viewmodel);
            await _productService.UpdateGlobalProduct(product);
        }

        [HttpPost]
        [Route("PublishGlobalProducts")]
        public async Task PublishGlobalProduct(List<string> globalProductIds)
        {
            foreach (var globalProductId in globalProductIds)
            {
                var product = await _productService.GetGlobalProduct(globalProductId);
                var message = _mapper.Map<AddProductMessage>(product);
                await _publisher.SendMessage(message);
                var merchantProducts = await _productService.GetMerchantProductsByGlobalId(globalProductId);
                foreach (var mp in merchantProducts)
                {
                    var mpMessage = _mapper.Map<AddMerchantProductMessage>(mp);
                    mpMessage.GlobalProductId = globalProductId;
                    await _publisher.SendMessage(mpMessage);
                }
            }
        }        

        [HttpPost]
        [Route("UnpublishGlobalProducts")]
        public async Task UnpublishGlobalProduct(List<string> globalProductIds)
        {
            foreach (var globalProductId in globalProductIds)
            {
                await _publisher.SendMessage(new RemoveProductMessage { GlobalProductId = globalProductId });
            }
        }

        [HttpGet]
        [Route("UnpublishGlobalProductByMerchantId/{merchantId}")]
        public async Task UnpublishGlobalProductByMerchantId(string merchantId)
        {
            var mappingsByMerchant = await _productService.GetProductMappingsByMerchant(merchantId);
            foreach (var prodId in mappingsByMerchant)
            {
                await _publisher.SendMessage(new RemoveMerchantProductMessage { GlobalProductId = prodId.GlobalProductId, MerchantId = merchantId, ProductId = prodId.ProductId });
            }
        }

        [HttpGet]
        [Route("PublishGlobalProductByMerchantId/{merchantId}")]
        public async Task PublishGlobalProductByMerchantId(string merchantId)
        {
            var products = await _productService.GetGlobalProductsByMerchantId(merchantId);
            var merchantProducts = await _productService.GetMerchantProductsByMerchantId(merchantId);
            foreach (var prod in products)
            {
                var gpMessage = _mapper.Map<AddProductMessage>(prod);
                await _publisher.SendMessage(gpMessage);
                var relevantMerchantProducts = merchantProducts.Where(x => x.GlobalProductId == prod.GlobalProductId);
                foreach (var mp in relevantMerchantProducts)
                {
                    var mpMessage = _mapper.Map<AddMerchantProductMessage>(mp);
                    mpMessage.GlobalProductId = prod.GlobalProductId;
                    await _publisher.SendMessage(mpMessage);
                }                
            }
        }

        [HttpPost]
        [Route("SetReviewed/{globalProductId}")]
        public async Task SetReviewed(string globalProductId)
        {
            await _productService.SetGlobalProductReviewStatus(globalProductId, true);
        }

        [HttpPost]
        [Route("SetNotReviewed/{globalProductId}")]
        public async Task SetNotReviewed(string globalProductId)
        {
            await _productService.SetGlobalProductReviewStatus(globalProductId, false);
        }
    }
}
