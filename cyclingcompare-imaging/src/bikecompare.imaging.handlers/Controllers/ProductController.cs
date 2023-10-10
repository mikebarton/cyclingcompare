using bikecompare.imaging.handlers.Options;
using bikecompare.imaging.handlers.Services;
using bikecompare.imaging.messages;
using bikecompare.messages;
using CoenM.ImageHash;
using CoenM.ImageHash.HashAlgorithms;
using infrastructure.messaging;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleStorageService _googleStorageService;
        private readonly ImageService _imageService;
        private readonly StorageOptions _storageOptions;
        private readonly IMessagePublisher _publisher;

        public ProductController(HttpClient httpClient, GoogleStorageService storageService, ImageService imageService, IOptions<StorageOptions> storageOptions, IMessagePublisher publisher)
        {
            _httpClient = httpClient;
            _googleStorageService = storageService;
            _imageService = imageService;
            _storageOptions = storageOptions.Value;
            _publisher = publisher;
        }

        [HttpPost]
        [Route("HandlePublishProduct")]
        public async Task HandlePublishProduct(GcpMessage<AddProductMessage> wrappedMessage)
        {
            var innerMessage = wrappedMessage.GetNestedMessage();
            using var response = await _httpClient.GetAsync(innerMessage.ImageUrl);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Will not process for {innerMessage.GlobalProductId} - {innerMessage.ImageUrl}. image download got 404 not found");
                return;
            }

            using var imageStream = await response.Content.ReadAsStreamAsync();

            var originalImageUrl = await _googleStorageService.WriteStorageObject(_storageOptions.Buckets.ProductImageBucket, $"products/{innerMessage.GlobalProductId}", imageStream);
            await _publisher.SendMessage(new OriginalImageStored { ProductId = innerMessage.GlobalProductId, ImageUrl = originalImageUrl });

            imageStream.Position = 0;
            using var targetStream = new MemoryStream();
            _imageService.ResizeImage(imageStream, targetStream, 180, 180);

            targetStream.Position = 0;
            var productSummaryUrl = await _googleStorageService.WriteStorageObject(_storageOptions.Buckets.ProductImageBucket, $"products/180x180/{innerMessage.GlobalProductId}", targetStream);

            await _publisher.SendMessage(new ProductSummaryImageResized { ProductId = innerMessage.GlobalProductId, ImageUrl = productSummaryUrl });
        }

        [HttpPost]
        [Route("HandleProductImported")]
        public async Task HandleProductImported(GcpMessage<ProductImported> message)
        {
            var innerMessage = message.GetNestedMessage();
            using var response = await _httpClient.GetAsync(innerMessage.ImageUrl);
            response.EnsureSuccessStatusCode();
            using var imageStream = await response.Content.ReadAsStreamAsync();

            var hashAlgorithm = new PerceptualHash();
            ulong imageHash = hashAlgorithm.Hash(imageStream);

            await _publisher.SendMessage<ImageHashCalculated>(new ImageHashCalculated() { Hash = imageHash, ProductId = innerMessage.ProductId });
        }
    }
}
