using TestApp.FileExplorer.Extensions;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Bootstrapping
{
    public class UIModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<FileImageCustomLoader>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var imageLoader = containerProvider.Resolve<FileImageCustomLoader>();
            ImageLoading.ImageLoader.Instance.RegisterLoader(imageLoader);
        }
    }
}
