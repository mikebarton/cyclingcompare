using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikecompare.import.messages
{
    public class ImportBanners : IRequest
    {
        public ApiManager ApiManager { get; set; }
        
        public List<Banner> Banners { get; set; }   
    }

    public class Banner
    {
        public string BannerId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string ImageUrl { get; set; }
        public string MerchantId { get; set; }
        public string MerchantName { get; set; }
        public string Name { get; set; }
        public string TargetUrl { get; set; }
        public string TrackingCode { get; set; }
        public string TrackingUrl { get; set; }

    }

    
}
