using AutoMapper;
using bikecompare.listing.handlers.Domain.Merchant;
using bikecompare.messages;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Controllers
{
    [Route("Merchant")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private IMapper _mapper;
        private MerchantService _merchantService;
        public MerchantController(IMapper mapper, MerchantService merchantService)
        {
            _mapper = mapper;
            _merchantService = merchantService;
        }

        [HttpPost]
        [Route("HandlePublishMerchant")]
        public async Task HandlePublishMerchant(GcpMessage<AddMerchantMessage> wrappedMessage)
        {
            var message = wrappedMessage.GetNestedMessage();

            var merchant = _mapper.Map<Merchant>(message);
            var existing = await _merchantService.GetMerchant(message.MerchantId);
            if (existing != null)
                await _merchantService.UpdateMerchant(merchant);
            else
                await _merchantService.InsertMerchant(merchant);
        }
    }
}
