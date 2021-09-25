using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Reflection;
using SQLite;

namespace TestApp.FileExplorer.ImageLoading.Caching
{
    public class FileBasedCache : IPersistentCache, IDisposable
    {
        private const string DatabaseFileName = "Cache.db";

        private const string DefaultDatabaseDirectory = "ImageCache";

        private SQLiteConnectionWithLock _db;

        private FileAccessSynchronizationManager _fileLocker = new FileAccessSynchronizationManager();

        public FileBasedCache()
            : this(DefaultDatabaseDirectory)
        {
        }

        //TODO: extend.
        private FileBasedCache(string directoryRelativePath)
        {
            DirecotryPath = directoryRelativePath;

            string fullDirectoryPath = directoryRelativePath;
            var parentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appName = "TestApp.ImageLoading";
            var attribute = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>();
            if (attribute != null)
            {
                appName = attribute.Product;
            }

            fullDirectoryPath = Path.Combine(parentDirectory, appName, directoryRelativePath);
            FullDirectoryPath = fullDirectoryPath;

            Directory.CreateDirectory(fullDirectoryPath);
            var dbFile = Path.Combine(fullDirectoryPath, DatabaseFileName);
            var connectionString = new SQLiteConnectionString(dbFile, true, null);
            _db = new SQLiteConnectionWithLock(connectionString, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            using (_db.Lock())
            {
                _db.CreateTable<CacheRecord>();
            }

            //var allRecords = _db.Table<CacheRecord>().ToList();
            //allRecords.ToList();
            //_db.Insert(new CacheRecord { CacheKey = "sadasd" });
        }

        public string DirecotryPath { get; }

        private string FullDirectoryPath { get; }

        public bool Contains(string cacheKey)
        {
            using (_db.Lock())
            {
                var record = _db.Table<CacheRecord>().FirstOrDefault(t => t.CacheKey == cacheKey);
                return record != null;
            }
        }

        public Stream LoadImage(string cacheKey)
        {
            string filePath = null;
            using (_db.Lock())
            {
                var record = _db.Table<CacheRecord>().FirstOrDefault(t => t.CacheKey == cacheKey);
                if (record == null)
                {
                    return null;
                }

                filePath = GetFilePath(record);
            }

            lock (_fileLocker.LockAccess(cacheKey))
            {
                var memoryStream = new MemoryStream();
                using (var fileStream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    fileStream.CopyTo(memoryStream);
                }

                memoryStream.Seek(0, SeekOrigin.Begin);

                return memoryStream;
            }
        }

        public void SaveImage(string cacheKey, Stream imageStream)
        {
            string filePath = null;
            using (_db.Lock())
            {
                var record = _db.Table<CacheRecord>().FirstOrDefault(t => t.CacheKey == cacheKey);
                if (record == null)
                {
                    record = new CacheRecord { CacheKey = cacheKey };
                    _db.Insert(record);
                }

                filePath = GetFilePath(record);
            }

            lock (_fileLocker.LockAccess(cacheKey))
            {
                using (var fileStream = File.Open(filePath, FileMode.Create, FileAccess.Write))
                {
                    imageStream.CopyTo(fileStream);
                }
            }
        }

        public void Clear()
        {
            using (_db.Lock())
            {
                // Not efficient implementation, (maybe it's better to kill all directory.
                // However full clearing isn't typical usage of cache.
                // Usually cache remove only separate records which are expired, etc.
                while (true)
                {
                    var record = _db.Table<CacheRecord>().FirstOrDefault();
                    if (record == null)
                    {
                        break;
                    }

                    var filePath = GetFilePath(record);
                    lock (_fileLocker.LockAccess(record.CacheKey))
                    {
                        File.Delete(filePath);
                    }

                    _db.Delete(record);
                }
            }
        }

        private string GetFilePath(CacheRecord record)
        {
            return Path.Combine(FullDirectoryPath, $"Image_{record.Id}");
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db?.Dispose();
                    _db = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }

    
}
