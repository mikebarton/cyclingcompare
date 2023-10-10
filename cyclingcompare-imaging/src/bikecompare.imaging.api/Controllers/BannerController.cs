using AutoMapper;
using bikecompare.imaging.api.Banners;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.api.Controllers
{
    [ApiController]
    [Route("Banner")]
    public class BannerController : Controller
    {
        private readonly ILogger<BannerController> _logger;
        private readonly IMapper _mapper;
        private readonly BannerService _bannerService;

        public BannerController(ILogger<BannerController> logger, IMapper mapper, BannerService bannerService)
        {
            _logger = logger;
            _mapper = mapper;
            _bannerService = bannerService;
        }

        [HttpGet]
        [Route("GetRandomBanner")]
        [EnableCors("AllowAll")]
        public async Task<BannerModel> GetRandomBanner(int width, int height)
        {
            var banners = await _bannerService.GetBannersByDimensions(width, height);
            if (banners.Count > 0)
            {
                var models = banners.Select(x => _mapper.Map<BannerModel>(x)).ToList();
                Random rnd = new Random();
                var modelIndex = rnd.Next(0, models.Count() - 1);

                return models[modelIndex];
            }
            return null;
        }
    }
}
