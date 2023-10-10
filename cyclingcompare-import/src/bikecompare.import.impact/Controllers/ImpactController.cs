using AutoMapper;
using bikecompare.import.impact.Mapper;
using bikecompare.import.impact.Options;
using bikecompare.import.impact.Services;
using bikecompare.import.messages;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Controllers
{
    [Route("Impact")]
    [ApiController]
    public class ImpactController : ControllerBase
    {
        private readonly ImpactFileProcessor _impactService;
        private readonly CloudStorageFileRetriever _fileRetriever;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private ImpactOptions _config;
        private readonly IMediator _mediator;
        private readonly MapperFactory _mapperFactory;


        public ImpactController(ImpactFileProcessor impactService, CloudStorageFileRetriever cloudStorageFileRetriever, ILoggerFactory loggerFactory, IMapper mapper, IMediator mediator, IOptions<ImpactOptions> options, MapperFactory mapperFactory)
        {
            _logger = loggerFactory.CreateLogger<ImpactController>();
            _mapper = mapper;
            _impactService = impactService;
            _fileRetriever = cloudStorageFileRetriever;
            _mediator = mediator;
            _config = options.Value;
            _mapperFactory = mapperFactory;
        }

        [HttpGet]
        [Route("ImportMerchants")]
        public async Task ImportMerchants()
        {
            var merchants = new List<ImportMerchant>()
            {
                new ImportMerchant(){ ApiManager = ApiManager.Impact, MappingId = Constants.MerchantIds.Wiggle, Name = "Wiggle Australia", CommissionMax = 3.5m, CommissionMin = 3.5m, CommissionRate = 3.5m, Status = "Joined", TrackingUrl = @"https://wiggle.akum7z.net/rnEy7j", TargetMarket="Australia" },
                new ImportMerchant(){ ApiManager = ApiManager.Impact, MappingId = Constants.MerchantIds.ChainReaction, Name = "Chain Reaction Cycles", CommissionMax = 3.5m, CommissionMin = 3.5m, CommissionRate = 3.5m, Status = "Joined", TrackingUrl = @"https://chainreactioncycles.d2lsjo.net/LP01VV", TargetMarket="Australia" }
            };

            foreach (var merchant in merchants)
            {
                await _mediator.Send(merchant);
            }
        }

        [HttpGet]
        [Route("ImportWiggle")]
        public async Task ImportWiggle()
        {
            await ImportProductsFromFile(_config.StorageBucket, _config.WiggleFilePath, Constants.MerchantIds.Wiggle);
        }

        [HttpGet]
        [Route("ImportChainReaction")]
        public async Task ImportChainReaction()
        {
            await ImportProductsFromFile(_config.StorageBucket, _config.ChainReactionFilePath, Constants.MerchantIds.ChainReaction);
        }

        private async Task ImportProductsFromFile(string bucket, string filePath, string merchantId)
        {
            var fileStream = await _fileRetriever.GetFileStream(bucket, filePath);
            var products = _impactService.ReadProducts(fileStream);

            var importProductsMessage = new ImportProducts
            {
                ApiManager = ApiManager.Impact,
                LastModified = DateTime.UtcNow,
                MerchantApiIdentifier = merchantId,
                Products = products.Select(x =>
                {
                    var product = _mapperFactory.CreateMapper(merchantId).Map<Product>(x);
                    return product;
                }).ToList()
            };

            Console.WriteLine($"Read {importProductsMessage.Products.Count} products from the api");
            await _mediator.Send(importProductsMessage);
        }
    }
}
