using TestApp.FileExplorer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TestApp.FileExplorer.ImageLoading
{
    internal class ControlRequestState
    {
        private readonly ImageLoader Loader;

        private object _source;

        private int _foregroundPriority;

        private int _backgroundPriority;

        private int _decodePixelWidth = -1;

        private int _decodePixelHeight = -1;

        private TimeSpan _requestInitialDelay; 

        private ImageLoadingRequest _activeRequest;

        private static int _instanceCounter;

        private int _id;

        private bool _canUsePersistentCache;

        internal ControlRequestState(FrameworkElement element, ImageLoader instance)
        {
            _instanceCounter++;
            _id = _instanceCounter;

            Loader = instance;
            UnifiedControl = Loader.CreateUnifiedControl(element);
            UnifiedControl.LifecyclePhaseChanged += OnStatusChanged;
            UpdateCurrentPriority();
        }

        private UnifiedControl UnifiedControl { get; set; }

        public object Source
        {
            get => _source;
            set
            {
                if (!Object.Equals(_source, value))
                {
                    var oldValue = _source;
                    _source = value;
                    OnSourceChanged(oldValue, value);
                }
            }
        }

        public int ForegroundPriority
        {
            get => _foregroundPriority;
            set
            {
                if (_foregroundPriority != value)
                {
                    _foregroundPriority = value;
                    UpdateCurrentPriority();
                }
            }
        }

        public int BackgroundPriority
        {
            get => _backgroundPriority;
            set
            {
                if (_backgroundPriority != value)
                {
                    _backgroundPriority = value;
                    UpdateCurrentPriority();
                }
            }
        }

        private int CurrentPriority { get; set; }

        private void OnStatusChanged(object sender, EventArgs e)
        {
            UpdateCurrentPriority();
        }

        private void UpdateCurrentPriority()
        {
            switch (UnifiedControl.LifecyclePhase)
            {
                case ControlLifecyclePhase.Loaded:
                    CurrentPriority = ForegroundPriority;
                    break;

                case ControlLifecyclePhase.Unloaded:
                    CurrentPriority = BackgroundPriority;
                    break;

                case ControlLifecyclePhase.Finalized:
                    CancelRequest();
                    break;
            }

            if (_activeRequest != null)
            {
                _activeRequest.Priority = CurrentPriority;
            }
        }

        private static BitmapImage _placeholder;

        public static BitmapImage Placeholder
        {
            get
            {
                // TODO: add customization for placeholder.
                if (_placeholder == null)
                {
                    var currentAssembly = typeof(ControlRequestState).Assembly;
                    using (var stream = currentAssembly.GetManifestResourceStream("TestApp.FileExplorer.ImageLoading.Properties.Placeholder.jpg"))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.StreamSource = stream;
                        bitmap.EndInit();
                        bitmap.Freeze();

                        _placeholder = bitmap;
                    }
                }

                return _placeholder;
            }
        }

        private void OnSourceChanged(object oldSource, object newSource)
        {
            //TODO: dispose old source?
            UnifiedControl.SetSource(Placeholder);
            if (_activeRequest != null)
            {
                CancelRequest();
            }

            var control = UnifiedControl.Element;
            if (control == null)
            {
                return;
            }

            //Give chanse to setup all other properties.
            control.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (newSource != null && 
                    newSource == _source)
                {
                    _activeRequest = new ImageLoadingRequest
                    {
                        Source = newSource,
                        Priority = CurrentPriority,
                        Control = UnifiedControl,
                        DecodePixelHeight = DecodePixelHeight,
                        DecodePixelWidth = DecodePixelWidth,
                        InitialDelay = InitialDelay,
                        CahceOptions = new CacheOptions(CanUsePersistentCache),
                    };

                    Loader.Load(_activeRequest);
                }
            }));
        }

        private void CancelRequest()
        {
            if (_activeRequest != null)
            {
                _activeRequest.Cancel();
                _activeRequest = null;
            }
        }

        // TODO: add support for decoding options.
        public int DecodePixelWidth
        {
            get => _decodePixelWidth;
            set
            {
                if (_decodePixelWidth != value)
                {
                    _decodePixelWidth = value;
                    if (_activeRequest != null)
                    {
                        _activeRequest.DecodePixelWidth = value;
                    }
                }

            }
        }

        public int DecodePixelHeight
        {
            get => _decodePixelHeight;
            set
            {
                if (_decodePixelHeight != value)
                {
                    _decodePixelHeight = value;
                    if (_activeRequest != null)
                    {
                        _activeRequest.DecodePixelHeight = value;
                    }
                }
            }
        }

        public TimeSpan InitialDelay
        {
            get => _requestInitialDelay;
            set
            {
                if (_requestInitialDelay != value)
                {
                    _requestInitialDelay = value;
                    if (_activeRequest != null)
                    {
                        _activeRequest.InitialDelay = value;
                    }
                }
            }
        }

        public bool CanUsePersistentCache
        {
            get => _canUsePersistentCache;
            set
            {
                if (_canUsePersistentCache != value)
                {
                    _canUsePersistentCache = value;
                    if (_activeRequest != null)
                    {
                        _activeRequest.CahceOptions = new CacheOptions(CanUsePersistentCache);
                    }
                }
            }

        }

    }
}
