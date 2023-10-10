using bikecompare.import.impact.Options;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.import.impact.Services
{
    public class CloudStorageFileRetriever
    {
        public CloudStorageFileRetriever()
        {
        }

        public async Task<Stream> GetFileStream(string bucketName, string filePath)
        {
            var storageClient = await StorageClient.CreateAsync();
            
            var memoryStream = new MemoryStream();
            await storageClient.DownloadObjectAsync(bucketName, filePath, memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
