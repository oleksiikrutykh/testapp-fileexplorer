using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading.Caching
{
    class FileAccessSynchronizationManager
    {
        private readonly ConcurrentDictionary<string, object> _locks = new ConcurrentDictionary<string, object>();

        public object LockAccess(string fileKey)
        {
            return _locks.GetOrAdd(fileKey, (k) => new object());
        }
    }
}
