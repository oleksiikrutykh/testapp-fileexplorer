using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TestApp.FileExplorer.ImageLoading.UI
{
    //TODO: add better support for image brush.
    public class ImageBrushUnifiedControl : UnifiedControl
    {
        private WeakReference _imageBrushLink;

        public ImageBrushUnifiedControl(ImageBrush brush, FrameworkElement elementOwner)
            : base(elementOwner)
        {
            _imageBrushLink = new WeakReference(brush);
        }


        public override void SetSource(ImageSource source)
        {
            var brush = _imageBrushLink.Target as ImageBrush;
            if (brush == null)
            {
                return;
            }

            brush.ImageSource = source;
        }
    }
}
