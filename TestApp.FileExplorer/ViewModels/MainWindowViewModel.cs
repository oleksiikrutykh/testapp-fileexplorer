using TestApp.FileExplorer.Core.Services;
using TestApp.FileExplorer.ImageLoading.Caching;
using TestApp.FileExplorer.ViewModels.Common;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp.FileExplorer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRegionManager RegionManager;

        private readonly Lazy<IPersistentCache> ImageCache;

        public MainWindowViewModel(IRegionManager regionManager, Lazy<IPersistentCache> imageCache)
        {
            RegionManager = regionManager;
            ImageCache = imageCache;

            ClearCacheCommand = new DelegateCommand(ClearCache, () => !_isClearing);
        }

        private bool _isClearing;

        public DelegateCommand ClearCacheCommand { get; private set; }

        private async void ClearCache()
        {
            _isClearing = true;
            ClearCacheCommand.RaiseCanExecuteChanged();
            try
            {
                await Task.Run(() =>
                {
                    ImageCache.Value.Clear();
                });
            }
            catch
            {
                // TODO: show error message?
            }
            finally
            {
                _isClearing = false;
                ClearCacheCommand.RaiseCanExecuteChanged();
            } 
        }
    }
}
