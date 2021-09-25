using TestApp.FileExplorer.Core.Services;
using TestApp.FileExplorer.ImageLoading;
using TestApp.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestApp.FileExplorer.Extensions
{
    class FileImageCustomLoader : ICustomImageLoader
    {
        private readonly IFileImageService ImageService;

        public FileImageCustomLoader(IFileImageService imageService)
        {
            ImageService = imageService;
        }

        public bool CanLoad(ImageLoadingRequest request)
        {
            return request.Source is FileImage;
        }

        public string GetUniqueId(object imageSource)
        {
            var image = (FileImage)imageSource;
            var id = $"{image.File.Id}_{image.Kind}";
            return id;
        }

        public async Task<Stream> Load(ImageLoadingRequest request)
        {
            var imageItem = (FileImage)request.Source;
            BitmapSource source = null;
            switch (imageItem.Kind)
            {
                case FileImageKind.Thumbnail:
                    source = await ImageService.RenderThumbnail(imageItem.File);
                    break;

                case FileImageKind.FullPreview:
                    source = await ImageService.RenderPreview(imageItem.File);
                    break;
            }

            //TODO: remove after investigations?
            //if (!source.IsFrozen)
            //{
            //    source.Freeze();
            //}

            //TODO: try in background thread!
            var memoryStream = new MemoryStream();
            try
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
            }
            catch
            {
                memoryStream.Dispose();
                throw;
            }

            return memoryStream;
        }
    }
}
