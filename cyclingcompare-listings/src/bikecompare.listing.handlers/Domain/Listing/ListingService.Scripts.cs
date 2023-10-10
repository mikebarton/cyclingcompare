using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.listing.handlers.Domain.Listing
{
    public partial class ListingService
    {
        public const string GetListingByIdStatement = @"SELECT ProductId, Name, Brand, Currency, DateModified, DeliveryCost, DeliveryTime, Description, Features, Specs, Gender, ImageUrl, ModelNumber FROM Listing where ProductId = @listingId";


        public const string UpdateListingStatement = @"UPDATE Listing
                                                SET
                                                Name = @name,
                                                Brand = @brand,
                                                Currency = @currency,
                                                DateModified = @dateModified,
                                                DeliveryCost = @deliveryCost,
                                                DeliveryTime = @deliveryTime,
                                                Description = @description,
                                                Features = @features,
                                                Specs = @specs,
                                                Gender = @gender,
                                                ImageUrl = @imageUrl,
                                                ModelNumber = @modelNumber
                                                WHERE ProductId = @listingId;
                                                ";

        public const string InsertListingStatement = @"INSERT INTO Listing
                                            (ProductId,
                                            Name,
                                            Brand,
                                            Currency,
                                            DateModified,
                                            DeliveryCost,
                                            DeliveryTime,
                                            Description,
                                            Features,
                                            Specs,
                                            Gender,
                                            ImageUrl,
                                            ModelNumber)
                                            VALUES
                                            (@listingId,
                                            @name,
                                            @brand,
                                            @currency,
                                            @dateModified,
                                            @deliveryCost,
                                            @deliveryTime,
                                            @description,
                                            @features,
                                            @specs,
                                            @gender,
                                            @imageUrl,
                                            @modelNumber);
                                            ";

        public const string DeleteListingStatement = @"DELETE from Listing where ProductId = @productId";

        public const string UpdateListingImage = @"UPDATE Listing
                                                SET
                                                ImageUrl = @imageUrl                                                
                                                WHERE ProductId = @listingId;
                                                ";

        
    }
}
