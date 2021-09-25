using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading
{

    public interface ICustomImageLoader
    {
        string GetUniqueId(object imageSource);

        bool CanLoad(ImageLoadingRequest request);

        Task<Stream> Load(ImageLoadingRequest request);
    }
}
