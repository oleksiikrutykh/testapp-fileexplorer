using TestApp.FileExplorer.ImageLoading.Caching;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading.Bootstrapping
{
    public class ImageLoaderModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            ImageLoader.Instance = containerProvider.Resolve<ImageLoader>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IPersistentCache, FileBasedCache>();
            containerRegistry.RegisterSingleton<ImageLoader>();
        }
    }
}
