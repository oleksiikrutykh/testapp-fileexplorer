using TestApp.FileExplorer.ImageLoading.Caching;
using TestApp.FileExplorer.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TestApp.FileExplorer.ImageLoading
{
    public partial class ImageLoader
    {
        public static ImageLoader Instance { get; internal set; } = new ImageLoader(null);

        private readonly IPersistentCache PersistentCache;

        private readonly PriorityQueue<ImageLoadingRequest, object> PriorityQueue;

        private readonly int ThreadsCount = 2;

        public ImageLoader(IPersistentCache persistentCache)
        {
            PersistentCache = persistentCache ?? new PersistentCacheStub();
            PriorityQueue = new PriorityQueue<ImageLoadingRequest, object>(HandleRequestInQueue, ThreadsCount);
        }

        public void Load(ImageLoadingRequest request)
        {
            if (DesignModeService.IsInDesignMode)
            {
                return;
            }

            request.CustomLoader = RegisteredLoaders.FirstOrDefault(l => l.CanLoad(request));
            if (request.CustomLoader == null)
            {
                var message = $"Image can't be loaded. Please register specific custom loader to support '{ request.Source }' image";
                throw new InvalidOperationException(message);
            }

            if (request.InitialDelay > TimeSpan.Zero)
            {
                request.InitialDelayTask = Task.Delay(request.InitialDelay);
            }

            Logger.Instance.Log($"Image loader - Add item to queue: {request.Source}");
            PriorityQueue.Enqueue(request.QueueItem);
        }

        private async Task<object> HandleRequestInQueue(ImageLoadingRequest request)
        {
            Logger.Instance.Log($"Image loader - start operation {request.Source}");
            BitmapImage bitmap = null;
            try
            {
                bitmap = await LoadInBackground(request);
            }
            catch (Exception ex)
            {
                // TODO: send exception to external code.
                Logger.Instance.Log("Image loading error: " + Environment.NewLine + ex.ToString());
            }


            Logger.Instance.Log($"Image loader - request {request.Source} completed with result { bitmap != null }");

            var dispatcher = request.Control.Dispatcher;
            if (dispatcher == null)
            {
                return null;
            }

            var asyncOperation = dispatcher.BeginInvoke(new Action(() =>
            {
                if (!request.Cancelled &&
                    bitmap != null)
                {
                    request.Control.SetSource(bitmap);
                }
            }));

            return null;
        }

        private async Task<BitmapImage> LoadInBackground(ImageLoadingRequest request)
        {
            if (request.Cancelled)
            {
                return null;
            }

            if (request.InitialDelayTask != null)
            {
                await request.InitialDelayTask;
                if (request.Cancelled)
                {
                    return null;
                }
            }

            var cachedBitmap = TryReadBitmapFromCache(request);
            if (cachedBitmap != null)
            {
                return cachedBitmap;
            }

            if (request.Cancelled)
            {
                return null;
            }

            using (var stream = await LoadViaCustomLoader(request))
            {
                if (stream == null)
                {
                    return null;
                }

                Stream seekableStream = stream;
                if (request.CahceOptions.CanSaveToCache)
                {
                    if (!stream.CanSeek)
                    {
                        seekableStream = new MemoryStream();
                        try
                        {
                            stream.CopyTo(seekableStream);
                            seekableStream.Seek(0, SeekOrigin.Begin);
                        }
                        catch
                        {
                            seekableStream.Dispose();
                            throw;
                        }
                    }
                }

                using (seekableStream)
                {
                    if (request.CahceOptions.CanSaveToCache)
                    {
                        PersistentCache.SaveImage(request.Id, seekableStream);
                        seekableStream.Seek(0, SeekOrigin.Begin);
                    }

                    if (request.Cancelled)
                    {
                        return null;
                    }

                    var loadedBitmap = CreateBitmap(seekableStream, request);
                    return loadedBitmap;
                }
            }
        }

        private BitmapImage TryReadBitmapFromCache(ImageLoadingRequest request)
        {
            if (!request.CahceOptions.CanReadFromCache)
            {
                return null;
            }

            try
            {
                if (!PersistentCache.Contains(request.Id))
                {
                    return null;
                }

                using (var cacheStream = PersistentCache.LoadImage(request.Id))
                {
                    if (cacheStream == null)
                    {
                        return null;
                    }

                    if (request.Cancelled)
                    {
                        return null;
                    }

                    return CreateBitmap(cacheStream, request);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                // Cached bitmap is not valid, try to execute real request.
                return null;
            }
        }

        private BitmapImage CreateBitmap(Stream stream, ImageLoadingRequest request)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            if (request.DecodePixelWidth > 0)
            {
                bitmap.DecodePixelWidth = request.DecodePixelWidth;
            }

            if (request.DecodePixelHeight > 0)
            {
                bitmap.DecodePixelHeight = request.DecodePixelHeight;
            }

            bitmap.StreamSource = stream;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        private async Task<Stream> LoadViaCustomLoader(ImageLoadingRequest request)
        {
            var loader = RegisteredLoaders.FirstOrDefault(l => l.CanLoad(request));
            if (loader == null)
            {
                throw new InvalidOperationException("Image can't be loaded. Please register custom loader to support requested image source.");
            }

            var stream = await loader.Load(request);
            return stream;
        }
    }
}
