using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.cms.media.google.FileStorage
{
    public class GoogleCloudFileStoreOptions
    {
        public string BucketName { get; set; }
        public string? BasePath { get; set; }
    }
}
