using TestApp.FileExplorer.ImageLoading.Caching;
using TestApp.FileExplorer.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TestApp.FileExplorer.ImageLoading
{

    public partial class ImageLoader
    {
        #region AttachedProperties

        #region Source property

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached("Source",
                                                 typeof(object),
                                                 typeof(ImageLoader),
                                                 new PropertyMetadata(null, OnSourceChanged));

        public static object GetSource(DependencyObject obj)
        {
            return obj.GetValue(SourceProperty);
        }

        public static void SetSource(DependencyObject obj, object value)
        {
            obj.SetValue(SourceProperty, value);
        }

        private static void OnSourceChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)owner;
            var state = GetImageLoadingState(element);
            state.Source = args.NewValue;
        }

        #endregion

        #region ForegroundPriority property

        public static int GetForegroundPriority(DependencyObject obj)
        {
            return (int)obj.GetValue(ForegroundPriorityProperty);
        }

        public static void SetForegroundPriority(DependencyObject obj, int value)
        {
            obj.SetValue(ForegroundPriorityProperty, value);
        }

        // Using a DependencyProperty as the backing store for ForegroundPriority.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundPriorityProperty =
            DependencyProperty.RegisterAttached("ForegroundPriority",
                                                 typeof(int),
                                                 typeof(ImageLoader),
                                                 new PropertyMetadata((int)PriorityLevel.ForegroundLoading, OnForegroundPriorityChanged));


        private static void OnForegroundPriorityChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)owner;
            var state = GetImageLoadingState(element);
            state.ForegroundPriority = (int)args.NewValue;
        }

        #endregion

        #region BackgroundPriority property

        public static int GetBackgroundPriority(DependencyObject obj)
        {
            return (int)obj.GetValue(BackgroundPriorityProperty);
        }

        public static void SetBackgroundPriority(DependencyObject obj, int value)
        {
            obj.SetValue(BackgroundPriorityProperty, value);
        }

        // Using a DependencyProperty as the backing store for BackgroundPriority.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundPriorityProperty =
            DependencyProperty.RegisterAttached("BackgroundPriority",
                                                 typeof(int),
                                                 typeof(ImageLoader),
                                                 new PropertyMetadata((int)PriorityLevel.BackgroundLoading, OnBackgroundPriorityChanged));


        private static void OnBackgroundPriorityChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)owner;
            var state = GetImageLoadingState(element);
            state.BackgroundPriority = (int)args.NewValue;
        }

        #endregion

        #region DecodePixelWodth property

        public static int GetDecodePixelWidth(DependencyObject obj)
        {
            return (int)obj.GetValue(DecodePixelWidthProperty);
        }

        public static void SetDecodePixelWidth(DependencyObject obj, int value)
        {
            obj.SetValue(DecodePixelWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for DecodePixelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelWidthProperty =
            DependencyProperty.RegisterAttached("DecodePixelWidth", typeof(int), typeof(ImageLoader), new PropertyMetadata(-1, OnDecodePixelWidthChanged));

        private static void OnDecodePixelWidthChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)owner;
            var state = GetImageLoadingState(element);
            state.DecodePixelWidth = (int)args.NewValue;
        }

        #endregion

        #region DecodePixelHeight property

        public static int GetDecodePixelHeight(DependencyObject obj)
        {
            return (int)obj.GetValue(DecodePixelHeightProperty);
        }

        public static void SetDecodePixelHeight(DependencyObject obj, int value)
        {
            obj.SetValue(DecodePixelHeightProperty, value);
        }

        public static readonly DependencyProperty DecodePixelHeightProperty =
            DependencyProperty.RegisterAttached("DecodePixelHeight", typeof(int), typeof(ImageLoader), new PropertyMetadata(-1, OnDecodePixelHeightChanged));

        private static void OnDecodePixelHeightChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)owner;
            var state = GetImageLoadingState(element);
            state.DecodePixelHeight = (int)args.NewValue;
        }

        #endregion

        #region InitialDelay property


        public static int GetInitialDelay(DependencyObject obj)
        {
            return (int)obj.GetValue(InitialDelayProperty);
        }

        public static void SetInitialDelay(DependencyObject obj, int value)
        {
            obj.SetValue(InitialDelayProperty, value);
        }

        // Using a DependencyProperty as the backing store for InitialDelay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialDelayProperty =
            DependencyProperty.RegisterAttached("InitialDelay", typeof(int), typeof(ImageLoader), new PropertyMetadata(0, OnInitialDelayChanged));


        private static void OnInitialDelayChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)owner;
            var state = GetImageLoadingState(element);
            state.InitialDelay = TimeSpan.FromMilliseconds((int)args.NewValue);
        }

        #endregion

        #region CacheOptions

        public static bool GetCanUsePersistentCache(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanUsePersistentCacheProperty);
        }

        public static void SetCanUsePersistentCache(DependencyObject obj, bool value)
        {
            obj.SetValue(CanUsePersistentCacheProperty, value);
        }

        public static readonly DependencyProperty CanUsePersistentCacheProperty =
            DependencyProperty.RegisterAttached("CanUsePersistentCache", typeof(bool), typeof(ImageLoader), new PropertyMetadata(false, OnCanUsePersistentCacheChanged));


        private static void OnCanUsePersistentCacheChanged(DependencyObject owner, DependencyPropertyChangedEventArgs args)
        {
            var element = (FrameworkElement)owner;
            var state = GetImageLoadingState(element);
            state.CanUsePersistentCache = (bool)args.NewValue;
        }

        #endregion

        #endregion

        #region Control extensibility

        public List<Func<FrameworkElement, UnifiedControl>> ControlTypes { get; }
            = new List<Func<FrameworkElement, UnifiedControl>>
            {
                ImageUnifiedControl.TryCreateWrapper,
            };

        public void RegisterControlType(Func<FrameworkElement, UnifiedControl> control)
        {
            ControlTypes.Insert(0, control);
        }

        public UnifiedControl CreateUnifiedControl(FrameworkElement element)
        {
            foreach (var factoryMethod in ControlTypes)
            {
                var wrapper = factoryMethod.Invoke(element);
                if (wrapper != null)
                {
                    return wrapper;
                }
            }

            throw new NotSupportedException($"Control {element?.GetType()} is not supported. Please register control wrapper in ImageLoader");
        }

        #endregion

        #region Loading formats extensibility

        public List<ICustomImageLoader> RegisteredLoaders { get; private set; } = new List<ICustomImageLoader>();

        public void RegisterLoader(ICustomImageLoader loader)
        {
            RegisteredLoaders.Insert(0, loader);
        }

        #endregion

        #region Control state support

        private static readonly DependencyProperty ImageLoadingRequestProperty =
            DependencyProperty.RegisterAttached("ImageLoadingRequest", typeof(ControlRequestState), typeof(ImageLoader), new PropertyMetadata(null));

        private static ControlRequestState GetImageLoadingState(FrameworkElement obj)
        {
            var request = (ControlRequestState)obj.GetValue(ImageLoadingRequestProperty);
            if (request == null)
            {
                request = new ControlRequestState(obj, Instance);
                obj.SetValue(ImageLoadingRequestProperty, request); 
            }

            return request;
        }

        #endregion


       
    }
}
