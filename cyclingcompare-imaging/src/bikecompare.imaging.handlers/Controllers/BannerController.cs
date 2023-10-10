using AutoMapper;
using bikecompare.imaging.handlers.Domain.Banner;
using bikecompare.imaging.handlers.Options;
using bikecompare.imaging.handlers.Services;
using bikecompare.messages;
using infrastructure.messaging;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Controllers
{
    [Route("Banner")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleStorageService _googleStorageService;
        private readonly ImageService _imageService;
        private readonly StorageOptions _storageOptions;
        private readonly IMessagePublisher _publisher;
        private readonly BannerService _bannerService;
        private readonly IMapper _mapper;

        public BannerController(HttpClient httpClient, GoogleStorageService storageService, ImageService imageService, IOptions<StorageOptions> storageOptions, IMessagePublisher publisher, BannerService bannerService, IMapper mapper)
        {
            _httpClient = httpClient;
            _googleStorageService = storageService;
            _imageService = imageService;
            _storageOptions = storageOptions.Value;
            _publisher = publisher;
            _bannerService = bannerService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("HandleAddBanner")]
        public async Task HandlePublishProduct(GcpMessage<AddBannerMessage> wrappedMessage)
        {
            var innerMessage = wrappedMessage.GetNestedMessage();
            using var response = await _httpClient.GetAsync(innerMessage.ImageUrl);
            response.EnsureSuccessStatusCode();
            using var imageStream = await response.Content.ReadAsStreamAsync();

            var bannerEntity = _mapper.Map<Banner>(innerMessage);
            bannerEntity.ImageUrl = await _googleStorageService.WriteStorageObject(_storageOptions.Buckets.BannerBucket, $"banners/{innerMessage.BannerId}", imageStream);

            var existingBanner = await _bannerService.GetBannerById(innerMessage.BannerId);
            if(existingBanner == null)
            {
                await _bannerService.InsertBanner(bannerEntity);
            }
            else
            {
                await _bannerService.UpdateBanner(bannerEntity);
            }            
        }

        [HttpPost]
        [Route("HandleDeleteBanner")]
        public async Task HandleDeleteBanner(GcpMessage<RemoveBannerMessage> gcpMessage)
        {
            var innerMessage = gcpMessage.GetNestedMessage();
            await _bannerService.DeleteBannerById(innerMessage.BannerId);
        }
    }
}
