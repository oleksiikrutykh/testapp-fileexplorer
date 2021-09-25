using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TestApp.FileExplorer.Core.Models;
using TestApp.FileExplorer.Utility;
using External = FolderBrowserLib.Interfaces;
using ExternalFolder = FolderBrowserLib.Interfaces.IFolder;

namespace TestApp.FileExplorer.Core.Services
{
    class FileImageService : IFileImageService
    {
        private External.IFolderBrowserLib _folderLib;

        public FileImageService(External.IFolderBrowserLib folderLib)
        {
            folderLib = Guard.CheckIsNotNull(folderLib, nameof(folderLib));
            _folderLib = folderLib;
        }

        public async Task<BitmapSource> RenderPreview(IFile file)
        {
            var fileWrapper = Guard.Cast<IFile, FileWrapper>(file, nameof(file));
            var bitmapImage = await _folderLib.GetImagePreviewAsync(fileWrapper.File);
            return bitmapImage;
        }

        public Task<BitmapSource> RenderThumbnail(IFile file)
        {
            var fileWrapper = Guard.Cast<IFile, FileWrapper>(file, nameof(file));
            var bitmapImage = _folderLib.GetImageThumbnail(fileWrapper.File);

            //TODO: investigate call in non-UI thread?
            return Task.FromResult(bitmapImage);
            //return Task.Run(() =>
            //{
            //    var bitmapImage = _folderLib.GetImageThumbnail(fileWrapper.File);
            //    return Task.FromResult<object>(bitmapImage);
            //});
        }
    }
}
