using bikecompare.import.commissionfactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.commissionfactory.Services
{
    public class ProductFilterService
    {
        public List<DataFeedItem> FilterIrrelevantProducts(List<DataFeedItem> items, string merchantId)
        {
            if (items == null || items.Count == 0)
                return items;

            switch (merchantId)
            {
                case Constants.MerchantIds.FindSports:
                    return items.Where(x => x.Category.ToLower().StartsWith("cycling")).ToList();                
                default:
                    return items;
            }
        }
    }
}
