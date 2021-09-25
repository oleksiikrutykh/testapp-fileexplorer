using TestApp.FileExplorer.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Bootstrapping
{
    public class ViewModelsModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<FoldersListViewModel>();
            containerRegistry.Register<FilesListViewModel>();
        }
    }
}
