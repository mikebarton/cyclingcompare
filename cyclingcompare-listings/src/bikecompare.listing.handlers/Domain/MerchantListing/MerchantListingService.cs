using Insight.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.MerchantListing
{
    public abstract partial class MerchantListingService
    {
        [Sql(GetMerchantListingStatement)]
        public abstract Task<MerchantListing> GetMerchantListing(string globalProductId, string merchantId, string productId);
        [Sql(GetMerchantListingsForProductStatement)]
        public abstract Task<List<MerchantListing>> GetMerchantListingsForProduct(string globalProductId);
        [Sql(InsertMerchantListingStatement)]
        public abstract Task InsertMerchantListing(MerchantListing merchantListing);
        [Sql(UpdateMerchantListingStatement)]
        public abstract Task UpdateMerchantListing(MerchantListing merchantListing);
        [Sql(DeleteMerchantListingStatement)]
        public abstract Task DeleteMerchantListing(string globalProductId, string merchantId, string productId);
        [Sql(DeleteAllMerchantListingsForProductStatement)]
        public abstract Task DeleteAllMerchantListingsForProduct(string productId);
    }
}
