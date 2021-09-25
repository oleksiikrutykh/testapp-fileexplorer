using TestApp.FileExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestApp.FileExplorer.Core.Services
{
    public interface IFileImageService
    {
        Task<BitmapSource> RenderThumbnail(IFile file);

        Task<BitmapSource> RenderPreview(IFile file);
    }
}
