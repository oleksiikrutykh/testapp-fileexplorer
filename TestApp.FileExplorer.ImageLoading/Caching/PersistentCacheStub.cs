using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading.Caching
{
    class PersistentCacheStub : IPersistentCache
    {
        public void Clear()
        {
        }

        public bool Contains(string cacheKey)
        {
            return false;
        }

        public Stream LoadImage(string cacheKey)
        {
            return null;
        }

        public void SaveImage(string cacheKey, Stream imageStream)
        {
        }
    }
}
