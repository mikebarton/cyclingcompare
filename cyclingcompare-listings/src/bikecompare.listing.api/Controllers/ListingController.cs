using AutoMapper;
using bikecompare.listing.api.Listing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Controllers
{
    [Route("Listing")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ListingService _listingService;
        private IHttpContextAccessor _httpContextAccessor;

        public ListingController(IMapper mapper, ListingService listingService, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _listingService = listingService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetListing/{listingId}")]
        public async Task<ListingViewModel> GetListing(string listingId)
        {
            var listing = await _listingService.GetListing(listingId);
            var merchantListings = await _listingService.GetMerchantListings(listingId);

            if (listing == null)
                return null;

            var listingVm = _mapper.Map<ListingViewModel>(listing);
            listingVm.MerchantListings = merchantListings.Select(x => _mapper.Map<MerchantListingViewModel>(x)).OrderBy(x=> x.Price).ToList();

            return listingVm;
        }

        [HttpPost]
        [Route("SubmitConversion")]
        public async Task ViewProduct(ConversionViewModel model)
        {
            var referrer = _httpContextAccessor.HttpContext.Request.Headers["Referer"];
            var host = _httpContextAccessor.HttpContext.Request.Headers["Host"];
            var origin = _httpContextAccessor.HttpContext.Request.Headers["Origin"];
            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];
            var ipaddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var merchantListings = await _listingService.GetMerchantListings(model.ProductId);
            var merchantListing = merchantListings.Find(x => x.MerchantId.Equals(model.MerchantId, StringComparison.OrdinalIgnoreCase));
            var conversion = _mapper.Map<Conversion>(merchantListing);
            conversion.ConversionId = Guid.NewGuid().ToString("N");
            conversion.Referer = model.Referer;
            conversion.Host = host;
            conversion.Origin = origin;
            conversion.UserAgent = userAgent;
            conversion.IPAddress = ipaddress;
            conversion.DateCreated = DateTime.UtcNow;

            await _listingService.InsertConversion(conversion);
        }

    }
}
