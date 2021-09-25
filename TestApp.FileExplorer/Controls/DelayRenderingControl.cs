using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestApp.FileExplorer.Controls
{
    public class DelayRenderingControl : Decorator
    {
        private CancellationTokenSource _tokenSource;

        public DelayRenderingControl()
        {
            _tokenSource = new CancellationTokenSource();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (Child != null)
            {
                return;
            }

            _tokenSource.Cancel();
            _tokenSource = new CancellationTokenSource();
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Child != null)
            {
                return;
            }

            var token = _tokenSource.Token;
            await Task.Delay(RenderingDelay);
            if (token.IsCancellationRequested)
            {
                return;
            }

            if (ContentTemplate != null)
            {
                Child = (UIElement)ContentTemplate.LoadContent();
            }
        }

        public int RenderingDelay { get; set; }

        public DataTemplate ContentTemplate { get; set; }


    }
}
