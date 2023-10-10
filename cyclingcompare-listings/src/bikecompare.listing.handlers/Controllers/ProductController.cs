using AutoMapper;
using bikecompare.imaging.messages;
using bikecompare.listing.handlers.Domain.Listing;
using bikecompare.listing.handlers.Domain.Merchant;
using bikecompare.listing.handlers.Domain.MerchantListing;
using bikecompare.messages;
using infrastructure.messaging.messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Controllers
{
    [Route("Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMapper _mapper;
        private ListingService _listingService;
        private MerchantListingService _merchantListingService;
        public ProductController(IMapper mapper, ListingService listingService, MerchantListingService merchantListingService)
        {
            _mapper = mapper;
            _listingService = listingService;
            _merchantListingService = merchantListingService;
        }

        [HttpPost]
        [Route("HandleAddProduct")]
        public async Task HandleAddProduct(GcpMessage<AddProductMessage> wrappedMessage)
        {
            var message = wrappedMessage.GetNestedMessage();

            var listing = _mapper.Map<Listing>(message);
            var existing = await _listingService.GetListingById(message.GlobalProductId);
            if (existing != null)
                await _listingService.UpdateListing(listing);
            else
                await _listingService.InsertListing(listing);            
        }

        [HttpPost]
        [Route("HandleRemoveProduct")]
        public async Task HandleRemoveProduct(GcpMessage<RemoveProductMessage> wrappedMessage)
        {
            var message = wrappedMessage.GetNestedMessage();

            await _listingService.DeleteListing(message.GlobalProductId);
            await _merchantListingService.DeleteAllMerchantListingsForProduct(message.GlobalProductId);
            
        }

        [HttpPost]
        [Route("HandleAddMerchantListing")]
        public async Task HandleAddMerchantListing(GcpMessage<AddMerchantProductMessage> wrappedMessage)
        {
            var message = wrappedMessage.GetNestedMessage();

            var existingListing = await _listingService.GetListingById(message.GlobalProductId);
            if (existingListing == null)
                throw new Exception("cannot add merchantlisting for listing that does not yet exist - " + message.GlobalProductId);

            var merchantListing = _mapper.Map<MerchantListing>(message);
            merchantListing.MerchantListingId = Guid.NewGuid().ToString("N");

            

            var existing = await _merchantListingService.GetMerchantListing(message.GlobalProductId, message.MerchantId, message.ProductId);

            if(existing != null)
            {
                await _merchantListingService.UpdateMerchantListing(merchantListing);
            }
            else
            {
                await _merchantListingService.InsertMerchantListing(merchantListing);
            }
        }

        [HttpPost]
        [Route("HandleRemoveMerchantListing")]
        public async Task HandleRemoveMerchantListing(GcpMessage<RemoveMerchantProductMessage> wrappedMessage)
        {
            var message = wrappedMessage.GetNestedMessage();

            await _merchantListingService.DeleteMerchantListing(message.GlobalProductId, message.MerchantId, message.ProductId);

            var merchantListings = await _merchantListingService.GetMerchantListingsForProduct(message.GlobalProductId);

            if(merchantListings == null || !merchantListings.Any())
            {
                await _listingService.DeleteListing(message.GlobalProductId);
            }
        }

        [HttpPost]
        [Route("HandleOriginalImageStored")]
        public async Task  HandleOriginalImageStored(GcpMessage<OriginalImageStored> wrappedMessage)
        {
            var message = wrappedMessage.GetNestedMessage();
            var listing = await _listingService.GetListingById(message.ProductId);
            if (listing == null)
                throw new InvalidDataException($"Listing with id: {message.ProductId} does not exist. Perhaps we just need to wait for the add-product message to be processed by this service");

            await _listingService.UpdateImageUrl(message.ProductId, message.ImageUrl);
        }
    }
}
