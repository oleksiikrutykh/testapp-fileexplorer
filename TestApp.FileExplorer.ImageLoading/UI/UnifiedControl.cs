using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TestApp.FileExplorer.ImageLoading
{
    public abstract class UnifiedControl
    {
        private ControlLifecyclePhase _phase;

        protected UnifiedControl(FrameworkElement owner)
        {
            if (owner != null)
            {
                ControlLink = new WeakReference(owner);
                owner.Unloaded += OnElementUnloaded;
                owner.Loaded += OnElementLoaded;

                if (!owner.IsLoaded)
                {
                    LifecyclePhase = ControlLifecyclePhase.Unloaded;
                }
            }
            else
            {
                LifecyclePhase = ControlLifecyclePhase.Loaded;
            }
        }

        public WeakReference ControlLink { get; private set; }

        public FrameworkElement Element => ControlLink?.Target as FrameworkElement;

        public Dispatcher Dispatcher => Element?.Dispatcher;

        public event EventHandler LifecyclePhaseChanged;

        private void OnElementLoaded(object sender, RoutedEventArgs e)
        {
            LifecyclePhase = ControlLifecyclePhase.Loaded;
        }

        private void OnElementUnloaded(object sender, RoutedEventArgs e)
        {
            LifecyclePhase = ControlLifecyclePhase.Unloaded;
        }

        internal ControlLifecyclePhase LifecyclePhase
        {
            get { return _phase; }
            set
            {
                if (_phase != value)
                {
                    _phase = value;
                    LifecyclePhaseChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        internal void UpdateIsAlive()
        {
            if (ControlLink == null)
            {
                return;
            }

            if (!ControlLink.IsAlive)
            {
                LifecyclePhase = ControlLifecyclePhase.Finalized;
            }
        }

        public abstract void SetSource(ImageSource source);

       
    }
}
