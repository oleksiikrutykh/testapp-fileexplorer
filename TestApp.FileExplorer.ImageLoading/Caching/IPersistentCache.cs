using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading.Caching
{
    public interface IPersistentCache
    {
        bool Contains(string cacheKey);

        Stream LoadImage(string cacheKey);

        void SaveImage(string cacheKey, Stream imageStream);

        void Clear();
    }
}
