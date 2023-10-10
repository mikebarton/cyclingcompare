using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.api.Listing
{
    public abstract partial class ListingService
    {
        [Sql(GetListingStatement)]
        public abstract Task<Listing> GetListing(string listingId);

        [Sql(GetMerchantListingsStatement)]
        public abstract Task<List<MerchantListing>> GetMerchantListings(string listingId);

        [Sql(InsertConversionStatement)]
        public abstract Task InsertConversion(Conversion conversion);
    }
}
