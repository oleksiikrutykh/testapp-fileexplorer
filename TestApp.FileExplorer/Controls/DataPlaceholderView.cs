using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestApp.FileExplorer.Controls
{
    class DataPlaceholderView : Control
    {
        public DataPlaceholderView()
        {
            DefaultStyleKey = typeof(DataPlaceholderView);
        }

        public static readonly DependencyProperty PlaceholderDataSourceProperty =
            DependencyProperty.Register("PlaceholderDataSource",
                                         typeof(object),
                                         typeof(DataPlaceholderView), 
                                         new PropertyMetadata(null));

        public object PlaceholderDataSource
        {
            get { return (object)GetValue(PlaceholderDataSourceProperty); }
            set { SetValue(PlaceholderDataSourceProperty, value); }
        }

        public static readonly DependencyProperty LoadingStateTemplateProperty =
           DependencyProperty.Register("LoadingStateTemplate",
                                        typeof(DataTemplate),
                                        typeof(DataPlaceholderView),
                                        new PropertyMetadata(null));

        public DataTemplate LoadingStateTemplate
        {
            get { return (DataTemplate)GetValue(LoadingStateTemplateProperty); }
            set { SetValue(LoadingStateTemplateProperty, value); }
        }

        public static readonly DependencyProperty EmptyStateTemplateProperty =
            DependencyProperty.Register("EmptyStateTemplate", 
                                         typeof(DataTemplate),
                                         typeof(DataPlaceholderView),
                                         new PropertyMetadata(null));

        public DataTemplate EmptyStateTemplate
        {
            get { return (DataTemplate)GetValue(EmptyStateTemplateProperty); }
            set { SetValue(EmptyStateTemplateProperty, value); }
        }

        public static readonly DependencyProperty ErrorStateTemplateProperty =
           DependencyProperty.Register("ErrorStateTemplate", 
                                       typeof(DataTemplate), 
                                       typeof(DataPlaceholderView), 
                                       new PropertyMetadata(null));

        public DataTemplate ErrorStateTemplate
        {
            get { return (DataTemplate)GetValue(ErrorStateTemplateProperty); }
            set { SetValue(ErrorStateTemplateProperty, value); }
        }
    }
}
