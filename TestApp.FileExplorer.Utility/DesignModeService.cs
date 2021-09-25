using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestApp.FileExplorer.Utility
{
    public static class DesignModeService
    {
        private static bool? _isInDesignMode;

        public static bool IsInDesignMode
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                {
                    var metadata = DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject));
                    _isInDesignMode = (bool)metadata.DefaultValue;
                }

                return _isInDesignMode.Value;
            }
        }
    }
}
