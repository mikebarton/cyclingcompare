using bikecompare.import.impact.Models.Impact;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace bikecompare.import.impact.Services
{
    public class ImpactFileProcessor
    {
        private ILogger _logger;

        public ImpactFileProcessor(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ImpactFileProcessor>();
        }

        public List<ImpactProduct> ReadProducts(Stream fileStream)
        {
            using (fileStream)
            {
                var serializer = new XmlSerializer(typeof(ImpactProducts));
                var data = serializer.Deserialize(fileStream) as ImpactProducts;
                return data?.Products.ToList();
                //var size = data.Products.Select(x => x.Size).Distinct().ToList();
                //var colour = data.Products.Select(x => x.Color).Distinct().ToList();
                //var avail = data.Products.Select(x => x.Stock_Availability).Distinct().ToList();
                //var condition = data.Products.Select(x => x.Condition).Distinct().ToList();
                //var gender = data.Products.Select(x => x.Gender).Distinct().ToList();
                //var ageRange = data.Products.Select(x => x.Age_Range).Distinct().ToList();
                //var curr = data.Products.Select(x => x.Currency).Distinct().ToList();
                //var region = data.Products.Select(x => x.Shipping_Region).Distinct().ToList();
            }            
        }
    }
}
