using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TestApp.FileExplorer.ImageLoading
{
    public class ImageUnifiedControl : UnifiedControl
    {
        public ImageUnifiedControl(Image image)
            : base(image)
        {
        }

        public override void SetSource(ImageSource source)
        {
            var image = ControlLink.Target as Image;
            if (image != null)
            {
                image.Source = source;
            }
        }

        public static UnifiedControl TryCreateWrapper(DependencyObject element)
        {
            var image = element as Image;
            if (image == null)
            {
                return null;
            }

            return new ImageUnifiedControl(image);
        }
    }
}
