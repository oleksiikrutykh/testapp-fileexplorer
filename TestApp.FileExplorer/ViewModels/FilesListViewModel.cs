using TestApp.FileExplorer.Core.Models;
using TestApp.FileExplorer.Core.Services;
using TestApp.FileExplorer.Localization;
using TestApp.FileExplorer.ViewModels.Common;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels
{
    class FilesListViewModel : DataPresenterViewModel<List<FileItem>>, INavigationAware, IRegionMemberLifetime
    {
        private readonly IFileBrowserService FileBroserService;

        private readonly IRegionManager RegionManager;

        private IFolder _parentFolder;

        private FileItem _selectedFile;

        public FilesListViewModel(IFileBrowserService fileBrowser,
                                  IRegionManager regionManager)
        {
            FileBroserService = fileBrowser;
            RegionManager = regionManager;

            EmptyState.Message = LocalizedStrings.Files_EmptyMessage;
        }

        public FileItem SelectedFile
        {
            get => _selectedFile;
            set
            {
                if (SetProperty(ref _selectedFile, value))
                {
                    ChangeFileDetails();
                }
            }
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            SelectedFile = null;
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _parentFolder = navigationContext.Parameters["folder"] as IFolder;
            Reset();
            await LoadData();
            ChangeFileDetails();
        }

        protected override async Task<List<FileItem>> LoadDataImpl()
        {
            var files = await FileBroserService.GetFiles(_parentFolder);
            var viewModels = files.Select(f => new FileItem(f)).ToList();
            return viewModels;
        }

        private void ChangeFileDetails()
        {
            var parameters = new NavigationParameters();
            if (SelectedFile != null)
            {
                parameters.Add("file", SelectedFile.File);
            }
            RegionManager.RequestNavigate(Regions.MainViewRightBottomContent, Regions.FileDetails, parameters);
        }
    }
}
