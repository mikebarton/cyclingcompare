using AutoMapper;
using bikecompare.import.commissionfactory.Mapper;
using bikecompare.import.commissionfactory.Options;
using bikecompare.import.commissionfactory.Services;
using bikecompare.import.messages;
using bikecompare.import.services.commissionfactory;
using infrastructure.messaging;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Controllers
{
    [Route("CommissionFactory")]
    [ApiController]
    public class CommissionFactoryImportController : ControllerBase
    {        
        private readonly CommissionFactoryService _commissionFactoryService;
        private readonly MapperFactory _mapper;
        private readonly IMediator _mediator;
        private readonly ProductFilterService _productFilterService;


        public CommissionFactoryImportController(CommissionFactoryService commissionFactoryService, MapperFactory mapper, IMediator mediator, ProductFilterService productFilterService)
        {            
            _commissionFactoryService = commissionFactoryService;
            _mapper = mapper;
            _mediator = mediator;
            _productFilterService = productFilterService;
        }

        [HttpGet]
        [Route("ImportMerchants")]
        public async Task ImportMerchants()
        {
            var merchants = await _commissionFactoryService.GetMerchants();
            foreach (var merchant in merchants)
            {
                var importMerchantmessage = _mapper.CreateMapper().Map<ImportMerchant>(merchant);
                importMerchantmessage.MappingId = merchant.Id;
                importMerchantmessage.ApiManager = ApiManager.CommissionFactory;

                await _mediator.Send(importMerchantmessage);
            }
        }

        [HttpGet]
        [Route("ImportBanners")]
        public async Task ImportBanners()
        {
            var banners = await _commissionFactoryService.GetAllBanners();
            var mapper = _mapper.CreateMapper();
            var message = new ImportBanners() { ApiManager = ApiManager.CommissionFactory };
            message.Banners = banners.Select(x => mapper.Map<import.messages.Banner>(x)).ToList();
            await _mediator.Send(message);
        }

        [HttpGet]
        [Route("ImportProducts/{merchantId}")]
        public async Task Import(string merchantId)
        {
            if (!IsValidMerchantId(merchantId))
                return;

            var dataFeeds = await _commissionFactoryService.GetDataFeedsWithoutItems(merchantId);
            foreach (var feed in dataFeeds)
            {
                var feedWithItems = await _commissionFactoryService.GetDataFeedWithItems(feed.Id.ToString());
                var importProductsMessage = new ImportProducts
                {
                    ApiManager = ApiManager.CommissionFactory,
                    LastModified = feedWithItems.DateModified,
                    MerchantApiIdentifier = merchantId,
                    Products = _productFilterService.FilterIrrelevantProducts(feedWithItems.Items, merchantId)?.Select(x =>
                    {
                        var product = _mapper.CreateMapper(merchantId).Map<Product>(x);
                        product.ApiIdentifier = x.Id;
                        return product;
                    }).ToList()
                };
                Console.WriteLine($"Read {importProductsMessage.Products.Count} products from the api");
                await _mediator.Send(importProductsMessage);
            }
        }

        private bool IsValidMerchantId(string merchantId)
        {
            switch (merchantId)
            {
                case Constants.MerchantIds.BicyclesOnline:
                case Constants.MerchantIds.FindSports:
                case Constants.MerchantIds.FitNutrition:
                case Constants.MerchantIds.MyProteinApac:
                case Constants.MerchantIds.ProvizSports:
                case Constants.MerchantIds.SpartanSuppz:
                case Constants.MerchantIds.Sportitude:
                case Constants.MerchantIds.MyProtein:
                case Constants.MerchantIds.WildFireSports:
                case Constants.MerchantIds.ProBikeKit:
                    return true;
                default:
                    return false;
            }
        }
    }
}
