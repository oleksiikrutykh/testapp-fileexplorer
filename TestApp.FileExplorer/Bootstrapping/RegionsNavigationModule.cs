using TestApp.FileExplorer.ViewModels;
using TestApp.FileExplorer.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Bootstrapping
{
    class RegionsNavigationModule : IModule
    {
        
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //var builder = containerRegistry.GetBuilder();
            containerRegistry.RegisterForNavigation<FoldersListView>(Regions.FolderList);
            containerRegistry.RegisterForNavigation<FilesListView>(Regions.FilesList);
            containerRegistry.RegisterForNavigation<FileDetailsView>(Regions.FileDetails);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion(Regions.MainViewLeftRegion, typeof(FoldersListView)); //  RegisterViewWithRegion("ContentRegion", typeof(FoldersListView));
        }

        

        //public void OnInitialized(IContainerProvider containerProvider)
        //{
        //    var regionManager = containerProvider.Resolve<IRegionManager>();
        //    regionManager.RegisterViewWithRegion("ContentRegion", typeof(PersonList));
        //}

        //public void RegisterTypes(IContainerRegistry containerRegistry)
        //{
        //    containerRegistry.RegisterForNavigation<PersonDetail>();
        //}
    }
}
