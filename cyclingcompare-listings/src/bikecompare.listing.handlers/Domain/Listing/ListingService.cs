using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.Listing
{
    public abstract partial class ListingService
    {
        [Sql(GetListingByIdStatement)]
        public abstract Task<Listing> GetListingById(string listingId);
        [Sql(UpdateListingStatement)]
        public abstract Task UpdateListing(Listing listing);
        [Sql(InsertListingStatement)]
        public abstract Task InsertListing(Listing listing);
        [Sql(DeleteListingStatement)]
        public abstract Task DeleteListing(string productId);
        [Sql(UpdateListingImage)]
        public abstract Task UpdateImageUrl(string listingId, string imageUrl);
        
    
    }
}
