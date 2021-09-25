using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading.Caching
{

    class CacheRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public string CacheKey { get; set; }

        //TODO: add last access date, and other useful statistics to support automatic expiring, deleting, etc.
    }
}
