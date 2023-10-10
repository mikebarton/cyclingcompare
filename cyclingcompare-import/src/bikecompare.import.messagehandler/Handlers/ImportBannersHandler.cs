using AutoMapper;
using bikecompare.import.messagehandler.Domains.Banner;
using bikecompare.import.messagehandler.Domains.Merchant;
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
    public class ImportBannersHandler : AsyncRequestHandler<ImportBanners>
    {
        private IMapper _mapper;
        private IMessagePublisher _publisher;
        private BannerService _bannerService;
        private MerchantService _merchantService;

        public ImportBannersHandler(BannerService bannerService, IMapper mapper, IMessagePublisher publisher, MerchantService merchantService)
        {
            _mapper = mapper;
            _publisher = publisher;
            _bannerService = bannerService;
            _merchantService = merchantService;
        }

        protected override async Task Handle(ImportBanners request, CancellationToken cancellationToken)
        {
            Dictionary<string, string> merchantIdMappings = new Dictionary<string, string>();

            var storedIds = new List<string>();
            foreach (var banner in request.Banners)
            {
                if(!merchantIdMappings.TryGetValue(banner.MerchantId, out var merchantId))
                {
                    merchantId = await _merchantService.GetMerchantByApiId(request.ApiManager.ToString(), banner.MerchantId);
                    merchantIdMappings[banner.MerchantId] = merchantId;
                }

                var bannerData = _mapper.Map<Domains.Banner.Banner>(banner);
                bannerData.MerchantId = merchantId;
                bannerData.ApiManager = request.ApiManager.ToString();
                bannerData.BannerId = Guid.NewGuid().ToString("N");

                var existingBanner = await _bannerService.GetBannerByExternalId(bannerData.ExternalId, bannerData.MerchantId, bannerData.ApiManager);
                if (existingBanner == null)
                    await _bannerService.InsertBanner(bannerData);
                else
                {
                    bannerData.BannerId = existingBanner.BannerId;
                    await _bannerService.UpdateBanner(bannerData);
                }

                var message = _mapper.Map<AddBannerMessage>(bannerData);
                await _publisher.SendMessage(message);
                storedIds.Add(bannerData.BannerId);
            }

            var allBannerIds = await _bannerService.GetBannerIds();
            foreach(var toDelete in allBannerIds.Where(x=> !storedIds.Contains(x)))
            {
                var deleteMessage = new RemoveBannerMessage() { BannerId = toDelete };
                await _publisher.SendMessage(deleteMessage);
                await _bannerService.DeleteBanner(toDelete);
            }
        }
    }
}
