using OrchardCore.FileStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikecompare.cms.media.google.FileStorage
{
    public class GoogleDirectory : IFileStoreEntry
    {
        private readonly string _path;
        private readonly DateTime _lastModifiedUtc;
        private readonly string _name;
        private readonly string _directoryPath;

        public GoogleDirectory(string path, DateTime lastModifiedUtc)
        {
            _path = path.TrimEnd('/');
            _path = NormalizePath(_path);
            _lastModifiedUtc = lastModifiedUtc;
            // Use GetFileName rather than GetDirectoryName as GetDirectoryName requires a delimiter
            _name = System.IO.Path.GetFileName(_path);
            _directoryPath = _path.Length > _name.Length ? _path.Substring(0, _path.Length - _name.Length - 1) : "";
            NormalizePath(_directoryPath);
        }

        private string NormalizePath(string path)
        {
            if (path.StartsWith("/"))
                return path;

            if (string.IsNullOrEmpty(path))
                return path;

            return "/" + path;
        }

        public string Path => _path;

        public string Name => _name;

        public string DirectoryPath => _directoryPath;

        public long Length => 0;

        public DateTime LastModifiedUtc => _lastModifiedUtc;

        public bool IsDirectory => true;
    }
}
