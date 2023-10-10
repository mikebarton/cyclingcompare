using AutoMapper;
using bikecompare.import.messagehandler.Domains.Categories;
using bikecompare.import.messagehandler.Domains.Merchant;
using bikecompare.import.messagehandler.Domains.Product;
using bikecompare.import.messagehandler.Services;
using bikecompare.messages;
using infrastructure.messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.messagehandler.Controllers
{
    [Route("Merchant")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private MerchantService _merchantService; 
        private IMessagePublisher _publisher;
        private IMapper _mapper;

        public MerchantController(MerchantService merchantService, IMessagePublisher publisher, IMapper mapper)
        {
            _publisher = publisher;
            _mapper = mapper;
            _merchantService = merchantService;
        }

        [HttpGet]
        [Route("PublishAll")]
        public async Task PublishAll()
        {
            var merchants = await _merchantService.GetAllMerchants();
            foreach (var merch in merchants)
            {
                var message = _mapper.Map<AddMerchantMessage>(merch);
                await _publisher.SendMessage(message);
            }
        }
    }
}
