using AutoMapper;
using bikecompare.import.messagehandler.Domains.Merchant;
using bikecompare.import.messagehandler.Domains.Product;
using bikecompare.import.messagehandler.Services;
using bikecompare.import.messages;
using bikecompare.messages;
using infrastructure.messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Handlers
{
    public class ImportProductHandler : AsyncRequestHandler<ImportProducts>
    {
        private readonly ProductService _productService;
        private readonly MerchantService _merchantService;
        private readonly IMapper _mapper;
        private readonly ProductMappingService _productMappingService;
        private readonly IMessagePublisher _publisher;

        public ImportProductHandler(ProductService productService, MerchantService merchantService, IMapper mapper, ProductMappingService productMappingService, IMessagePublisher publisher)
        {
            _productService = productService;
            _merchantService = merchantService;
            _mapper = mapper;
            _productMappingService = productMappingService;
            _publisher = publisher;
        }

        protected override async Task Handle(ImportProducts request, CancellationToken cancellationToken)
        {
            var merchantId = await _merchantService.GetMerchantByApiId(request.ApiManager.ToString(), request.MerchantApiIdentifier);

            if (string.IsNullOrEmpty(merchantId))
                throw new InvalidOperationException("supplied merchant api identifier does not match any imported merchant");

            var storedProductIds = new List<string>();
            foreach (var product in request.Products)
            {
                var mappedProduct = _mapper.Map<Domains.Product.Product>(product);
                mappedProduct.MerchantId = merchantId;
                mappedProduct.ProductId = await _productService.ImportProduct(mappedProduct, merchantId, request.ApiManager.ToString());                
                storedProductIds.Add(mappedProduct.ProductId);
                //await _productMappingService.TryMapProductToGlobalProduct(mappedProduct);
                //var productMapping = await _productService.GetProductMapping(request.ApiManager.ToString(), merchantId, mappedProduct.ProductId);
                //if (!string.IsNullOrWhiteSpace(productMapping.GlobalProductId))
                //{
                //    var message = _mapper.Map<AddMerchantProductMessage>(mappedProduct);
                //    message.ProductId = productMapping.GlobalProductId;
                //    await _publisher.SendMessage(message);
                //}
            }

            var allProductMappings = await _productService.GetAllProductMappingsByMerchant(merchantId);
            var productsToDelete = allProductMappings.Where(x => !storedProductIds.Any(y => x.ProductId == y));
            foreach (var productToDelete in productsToDelete)
            {
                await _productService.RemoveProduct(productToDelete.ProductId);
                await _productService.RemoveGlobalProductMapping(productToDelete.ProductId, merchantId);
                await _publisher.SendMessage(new RemoveMerchantProductMessage() { GlobalProductId = productToDelete.GlobalProductId, MerchantId = productToDelete.MerchantId, ProductId = productToDelete.ProductId });
            }

            var orphannedGlobalProducts = await _productService.GetOrphanedGlobalProducts();
            await _productService.DeleteOrphanedGlobalProducts();
            foreach (var deletedProd in orphannedGlobalProducts)
            {
                await _publisher.SendMessage(new RemoveProductMessage { GlobalProductId = deletedProd });
            }
        }
    }
}
