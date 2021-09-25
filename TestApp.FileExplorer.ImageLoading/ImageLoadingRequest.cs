using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading
{
    public class ImageLoadingRequest
    {
        private ICustomImageLoader _customLoader;

        internal ImageLoader Service { get; set; }

        internal ICustomImageLoader CustomLoader
        {
            get => _customLoader;
            set
            {
                _customLoader = value;
                if (_customLoader != null)
                {
                    Id = CustomLoader.GetUniqueId(Source);
                }
            }
        }

        internal string Id { get; private set; }

        public ImageLoadingRequest()
        {
            QueueItem = new QueueItem<ImageLoadingRequest, object>(0, this);
        }

        internal QueueItem<ImageLoadingRequest, object> QueueItem { get; }

        public object Source { get; set; }

        public int DecodePixelWidth { get; set; }

        public int DecodePixelHeight { get; set; }

        public CacheOptions CahceOptions { get; set; } = new CacheOptions();

        public TimeSpan InitialDelay { get; set; }

        internal Task InitialDelayTask { get; set; }

        public UnifiedControl Control { get; set; }

        public int Priority
        {
            get => QueueItem.Priority;
            set => QueueItem.Priority = value;
        }

        private event EventHandler CancelledStatusRequesting;

        private bool _cancelled;

        public bool Cancelled
        {
            get
            {
                if (!_cancelled)
                {
                    CancelledStatusRequesting?.Invoke(this, EventArgs.Empty);
                }

                return _cancelled;
            }

            private set
            {
                _cancelled = value;
            }
        }

        public void Cancel()
        {
            Cancelled = true;
            QueueItem.Cancel();
        }
    }
}
