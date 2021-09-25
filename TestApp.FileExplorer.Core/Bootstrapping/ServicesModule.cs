using TestApp.FileExplorer.Core.Services;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Core.Bootstrapping
{
    public class ServicesModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(FolderBrowserLib.FolderBrowserLib.Instance);
            containerRegistry.RegisterSingleton<IFileBrowserService, FileBrowserService>();
            containerRegistry.RegisterSingleton<IFileImageService, FileImageService>();
        }
    }
}
