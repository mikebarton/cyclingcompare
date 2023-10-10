using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Options
{
    public class StorageOptions
    {
        public Buckets Buckets { get; set; }
    }

    public class Buckets
    {
        public string ProductImageBucket { get; set; }
        public string BannerBucket { get; set; }
    }
}
