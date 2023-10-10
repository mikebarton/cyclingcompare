using AutoMapper;
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
    public class ImportMerchantHandler : AsyncRequestHandler<ImportMerchant>
    {
        private readonly MerchantService _merchantService;
        private IMessagePublisher _publisher;
        private IMapper _mapper;

        public ImportMerchantHandler(MerchantService merchantService, IMessagePublisher publisher, IMapper mapper)
        {
            _merchantService = merchantService;
            _publisher = publisher;
            _mapper = mapper;
        }

        protected override async Task Handle(ImportMerchant request, CancellationToken cancellationToken)
        {
            var merchantId = await _merchantService.GetMerchantByApiId(request.ApiManager.ToString(), request.MappingId);
            using (var tran = _merchantService.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(merchantId))
                    {
                        merchantId = Guid.NewGuid().ToString("N");
                        await _merchantService.InsertMerchantRecord(merchantId, request.CommissionMax, request.CommissionMin, request.CommissionRate, request.CookieDurationHours, request.DateModified, request.Status, request.Name, request.Summary, request.TargetMarket, request.TargetUrl, request.TermsAndConditions, request.TrackingCode, request.TrackingUrl, request.ValidationPeriod);
                        await _merchantService.InsertMerchantMapping(Guid.NewGuid().ToString("N"), request.ApiManager.ToString(), request.MappingId, merchantId);
                    }
                    else
                    {
                        await _merchantService.UpdateMerchant(merchantId, request.CommissionMax, request.CommissionMin, request.CommissionRate, request.CookieDurationHours, request.DateModified, request.Status, request.Name, request.Summary, request.TargetMarket, request.TargetUrl, request.TermsAndConditions, request.TrackingCode, request.TrackingUrl, request.ValidationPeriod);
                    }
                    tran.Commit();

                    var addMerchantMessage = _mapper.Map<AddMerchantMessage>(request);
                    addMerchantMessage.MerchantId = merchantId;
                    await _publisher.SendMessage(addMerchantMessage);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
