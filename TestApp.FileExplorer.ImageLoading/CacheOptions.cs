using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading
{
    public class CacheOptions
    {
        public CacheOptions()
        {
        }

        public CacheOptions(bool canUsePersistentCache)
        {
            CanSaveToCache = canUsePersistentCache;
            CanReadFromCache = canUsePersistentCache;
        }

        public bool CanSaveToCache { get; set; }

        public bool CanReadFromCache { get; set; }

        //TODO: add memory cache to store bitmaps which are used often.
        public bool CanSaveToMemoryCache { get; set; } = false;
    }
}
