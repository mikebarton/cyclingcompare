using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using OrchardCore.Modules;
using OrchardCore.FileStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace bikecompare.cms.media.google.FileStorage
{
    public class GoogleCloudFileStore : OrchardCore.FileStorage.IFileStore
    {
        private const string _directoryMarkerFileName = "OrchardCore.Media.txt";
        private readonly GoogleCloudFileStoreOptions _options;
        private readonly IContentTypeProvider _contentTypeProvider;
        private readonly IClock _clock;
        private readonly string _basePrefix = null;

        public GoogleCloudFileStore(IOptions<GoogleCloudFileStoreOptions> options, IContentTypeProvider contentTypeProvider, IClock clock)
        {
            _options = options.Value;
            _contentTypeProvider = contentTypeProvider;
            _clock = clock;

            if (!String.IsNullOrEmpty(_options.BasePath))
            {
                _basePrefix = NormalizePrefix(_options.BasePath);
            }
        }

        public async Task CopyFileAsync(string srcPath, string dstPath)
        {
            if (srcPath == dstPath)
            {
                throw new ArgumentException($"The values for {nameof(srcPath)} and {nameof(dstPath)} must not be the same.");
            }            

            var storageClient = await StorageClient.CreateAsync();                  

            if(!await FileExists(storageClient, srcPath))
            {
                throw new FileStoreException($"Cannot copy file '{srcPath}' because it does not exist.");
            }

            if (await FileExists(storageClient, dstPath))
            {
                throw new FileStoreException($"Cannot copy file '{srcPath}' because a file already exists in the new path '{dstPath}'.");
            }

            await storageClient.CopyObjectAsync(_options.BucketName, GetObjectPath(srcPath), _options.BucketName, GetObjectPath(dstPath));            
        }        

        public async Task<string> CreateFileFromStreamAsync(string path, Stream inputStream, bool overwrite = false)
        {
            var storageClient = await StorageClient.CreateAsync();
            if(!overwrite && await FileExists(storageClient, path))
            {
                throw new FileStoreException($"Cannot create file '{path}' because it already exists.");
            }

            _contentTypeProvider.TryGetContentType(path, out var contentType);

            await storageClient.UploadObjectAsync(_options.BucketName, GetObjectPath(path), contentType, inputStream);

            return path;
        }

        public IAsyncEnumerable<IFileStoreEntry> GetDirectoryContentAsync(string path = null, bool includeSubDirectories = false)
        {
            try
            {
                var storageClient = StorageClient.Create();
                
                return GetDirectoryContentAsync(storageClient, path, includeSubDirectories);
                
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot get directory content with path '{path}'.", ex);
            }
        }

        private async IAsyncEnumerable<IFileStoreEntry> GetDirectoryContentAsync(StorageClient storageClient, string path = null, bool includeSubDirectories = false)
        {
            var prefix = GetObjectPath(path);
            prefix = NormalizePrefix(prefix);
            var listOptions = new ListObjectsOptions();
            if (!includeSubDirectories)
            {
                listOptions.Delimiter = "/";
                listOptions.IncludeTrailingDelimiter = true;
            }

            var page = storageClient.ListObjectsAsync(_options.BucketName, prefix, listOptions);

            await foreach (var blob in page)
            {
                //if (string.Equals(blob.Name, prefix, StringComparison.OrdinalIgnoreCase))
                if(blob.Name.EndsWith('/') && blob.Size == 0)
                {
                    var folderPath = blob.Name;
                    if (!String.IsNullOrEmpty(_options.BasePath))
                    {
                        folderPath = folderPath.Substring(_options.BasePath.Length);
                    }

                    //folderPath = folderPath.Trim('/');
                    var dir = new GoogleDirectory(folderPath, _clock.UtcNow);
                    if (string.Equals(path, dir.Path, StringComparison.OrdinalIgnoreCase))
                        continue;

                    yield return dir;
                }
                else
                {
                    if (blob.Name.Contains(_directoryMarkerFileName, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var itemName = Path.GetFileName(WebUtility.UrlDecode(blob.Name)).Trim('/');
                    
                    // Ignore directory marker files.
                    if (itemName != _directoryMarkerFileName)
                    {
                        var itemPath = this.Combine(path?.Trim('/'), itemName);
                        
                        yield return new GoogleFile(itemPath, (long?)blob.Size, blob.Updated);
                    }
                }
            }
        }

        

        public async Task<IFileStoreEntry> GetDirectoryInfoAsync(string path)
        {
            try
            {
                var storageClient = await StorageClient.CreateAsync();

                if (path == String.Empty || await FileExists(storageClient, path, true))
                {
                    var dir = new GoogleDirectory(path, _clock.UtcNow);
                    return dir;
                }                

                return null;
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot get directory info with path '{path}'.", ex);
            }
        }

        public async Task<IFileStoreEntry> GetFileInfoAsync(string path)
        {
            try
            {
                StorageClient storageClient = await StorageClient.CreateAsync();
                if (!await FileExists(storageClient, path))
                {
                    return null;
                }

                var file = await storageClient.GetObjectAsync(_options.BucketName, GetObjectPath(path));

                return new GoogleFile(path, (long?)file.Size, file.Updated);
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot get file info with path '{path}'.", ex);
            }
        }

        public async Task<Stream> GetFileStreamAsync(string path)
        {
            try
            {
                var storageClient = await StorageClient.CreateAsync();
                if (!await FileExists(storageClient, path))
                {
                    throw new FileStoreException($"Cannot get file stream because the file '{path}' does not exist.");
                }
                var stream = new MemoryStream();
                await storageClient.DownloadObjectAsync(_options.BucketName, GetObjectPath(path), stream);
                stream.Position = 0;
                
                return stream;
            }
            catch (FileStoreException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot get file stream of the file '{path}'.", ex);
            }
        }

        public Task<Stream> GetFileStreamAsync(IFileStoreEntry fileStoreEntry)
        {
            return GetFileStreamAsync(fileStoreEntry.Path);
        }

        public async Task MoveFileAsync(string oldPath, string newPath)
        {
            try
            {
                await CopyFileAsync(oldPath, newPath);
                await TryDeleteFileAsync(oldPath);
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot move file '{oldPath}' to '{newPath}'.", ex);
            }
        }

        public async Task<bool> TryCreateDirectoryAsync(string path)
        {
            try
            {
                var storageClient = await StorageClient.CreateAsync();
                if (await FileExists(storageClient, path))
                {
                    throw new FileStoreException($"Cannot create directory because the path '{path}' already exists and is a file.");
                }
                var folderName = path.EndsWith('/') ? path : path + @"/";
                folderName = folderName.TrimStart('/');
                folderName = _options.BasePath == null ? folderName : $"{_options.BasePath}/{folderName}";
                var blobDirectory = await storageClient.UploadObjectAsync(_options.BucketName, folderName, "", new MemoryStream());                

                return true;
            }
            catch (FileStoreException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot create directory '{path}'.", ex);
            }
        }

        public async Task<bool> TryDeleteDirectoryAsync(string path)
        {
            try
            {
                if (String.IsNullOrEmpty(path))
                {
                    throw new FileStoreException("Cannot delete the root directory.");
                }

                var blobsWereDeleted = false;
                var prefix = this.Combine(_basePrefix, path);
                prefix = this.NormalizePrefix(prefix);

                var storageClient = await StorageClient.CreateAsync();
                var page = storageClient.ListObjectsAsync(_options.BucketName, prefix);
                await foreach (var blob in page)
                {
                    await storageClient.DeleteObjectAsync(_options.BucketName, blob.Name);
                    blobsWereDeleted = true;
                }

                return blobsWereDeleted;
            }
            catch (FileStoreException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot delete directory '{path}'.", ex);
            }
        }

        public async Task<bool> TryDeleteFileAsync(string path)
        {
            try
            {
                var storageClient = await StorageClient.CreateAsync();
                if(!await FileExists(storageClient, path))
                {
                    return false;
                }

                await storageClient.DeleteObjectAsync(_options.BucketName, GetObjectPath(path));
                return true;
            }
            catch (Exception ex)
            {
                throw new FileStoreException($"Cannot delete file '{path}'.", ex);
            }
        }        

        private string NormalizePrefix(string prefix)
        {
            prefix = prefix.Trim('/') + '/';
            if (prefix.Length == 1)
            {
                return String.Empty;
            }
            else
            {
                return prefix;
            }
        }

        private async Task<bool> FileExists(StorageClient client, string path, bool isDirectory = false)
        {
            var prefix= GetObjectPath(path);

            if(isDirectory)
                prefix = NormalizePrefix(prefix);

            var matchingObjects = client.ListObjectsAsync(_options.BucketName, prefix);
            return await matchingObjects.AnyAsync(x =>
            {
                return string.Equals(prefix, x.Name, StringComparison.OrdinalIgnoreCase);
            });
        }

        private string GetObjectPath(string path)
        {
            //return _options.BasePath == null ? path : $"{_options.BasePath}/{path}";
            return this.Combine(_options.BasePath, path);
        }
    }
}
