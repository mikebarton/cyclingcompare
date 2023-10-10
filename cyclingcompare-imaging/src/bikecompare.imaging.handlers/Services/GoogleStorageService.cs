using Google.Cloud.Storage.V1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.imaging.handlers.Services
{
    public class GoogleStorageService 
    {
        public async Task ReadStorageObject(string bucketName, string objectName, Stream destinationStream)
        {
            var storageClient = await StorageClient.CreateAsync();
            await storageClient.DownloadObjectAsync(bucketName, objectName, destinationStream);
        }

        public async Task<string> WriteStorageObject(string bucketName, string objectName, Stream sourceStream)
        {
            var storageClient = await StorageClient.CreateAsync();
            var obj = await storageClient.UploadObjectAsync(bucketName, objectName, "image/jpeg", sourceStream);
            return $"https://storage.googleapis.com/{bucketName}/{objectName}";
            //Firebase.Storage.FirebaseStorage firebaseStorage = new Firebase.Storage.FirebaseStorage(bucketName);
            //var nameParts = objectName.Split(new char[] { '/' });
            //if (nameParts.Length == 0)
            //    throw new InvalidDataException("Invalid file name");

            //var fileRef = firebaseStorage.Child(nameParts[0]);
            //if (nameParts.Length > 1)
            //{
            //    for (int i = 1; i < nameParts.Length; i++)
            //    {
            //        fileRef = fileRef.Child(nameParts[i]);
            //    }
            //}
            //sourceStream.Position = 0;
            //var result = await fileRef.PutAsync(sourceStream, new System.Threading.CancellationToken(), mimeType: "image/jpeg");
            //return result;
            //return obj;
        }
    }
}
