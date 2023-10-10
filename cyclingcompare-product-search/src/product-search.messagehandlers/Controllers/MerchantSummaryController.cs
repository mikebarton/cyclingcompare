using AutoMapper;
using bikecompare.messages;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using product_search.messagehandlers.MerchantSummaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_search.messagehandlers.Controllers
{
    [Route("MerchantSummary")]
    [ApiController]
    public class MerchantSummaryController : ControllerBase
    {
        private readonly ILogger<MerchantSummaryController> _logger;
        private readonly IMapper _mapper;
        private readonly MerchantService _merchantService;

        public MerchantSummaryController(MerchantService merchantService, IMapper mapper, ILogger<MerchantSummaryController> logger)
        {
            _logger = logger;
            _merchantService = merchantService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("HandlerPublishMerchant")]
        public async Task HandlePublishMerchant(GcpMessage<AddMerchantMessage> gcpMessage)
        {
            var message = gcpMessage.GetNestedMessage();
            var existing = await _merchantService.GetMerchantSummaryById(message.MerchantId);
            var merchantSummary = _mapper.Map<MerchantSummary>(message);
            if(existing == null)
            {
                await _merchantService.InsertMerchantSummary(merchantSummary);
            }
            else
            {
                await _merchantService.UpdateMerchantSummary(merchantSummary);
            }
        }
    }
}
