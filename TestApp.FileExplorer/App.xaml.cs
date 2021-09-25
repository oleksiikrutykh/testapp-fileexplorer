using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestApp.FileExplorer.Bootstrapping;
using TestApp.FileExplorer.Core.Bootstrapping;
using TestApp.FileExplorer.Views;
using Prism.Unity;
using Prism.Ioc;
using Prism.Modularity;
using TestApp.FileExplorer.ImageLoading;
using TestApp.FileExplorer.Utility.Bootstrapping;
using TestApp.FileExplorer.Utility;
using TestApp.FileExplorer.ImageLoading.Bootstrapping;

namespace TestApp.FileExplorer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<UtilityModule>();
            moduleCatalog.AddModule<ServicesModule>();
            moduleCatalog.AddModule<ImageLoaderModule>();
            moduleCatalog.AddModule<ViewModelsModule>();
            moduleCatalog.AddModule<UIModule>();
            moduleCatalog.AddModule<RegionsNavigationModule>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var logger = Container.Resolve<ILogger>();
            logger.Enabled = false;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
