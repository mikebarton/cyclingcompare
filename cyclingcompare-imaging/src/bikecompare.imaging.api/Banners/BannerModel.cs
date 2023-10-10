using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.api.Banners
{
    public class BannerModel
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string TrackingUrl { get; set; }
    }
}
