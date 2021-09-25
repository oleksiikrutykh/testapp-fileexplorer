using TestApp.FileExplorer.Core.Services;
using  TestApp.FileExplorer.Localization;
using TestApp.FileExplorer.Utility;
using TestApp.FileExplorer.ViewModels.Common;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels
{
    class FoldersListViewModel : DataPresenterViewModel<List<FolderItem>>
    {
        private readonly IFileBrowserService FileBrowserService;

        private readonly IRegionManager RegionManager;

        private FolderItem _selectedFolder;

        public FoldersListViewModel(IFileBrowserService fileService,
                                    IRegionManager regionManager)
        {
            RegionManager = regionManager;
            FileBrowserService = fileService;
            EmptyState.Message = LocalizedStrings.Folders_EmptyMessage;

            //TODO: move to navigation?
            if (!DesignModeService.IsInDesignMode)
            {
                var ignoredTask = LoadData();
            }
        }

        public FolderItem SelectedFolder
        {
            get => _selectedFolder;
            set
            {
                var oldValue = _selectedFolder;
                if (oldValue != null && value == null)
                {
                    RaisePropertyChanged(nameof(SelectedFolder));
                    return;
                }

                if (SetProperty(ref _selectedFolder, value))
                {
                    UpdateSelectedFolder();
                }
            }
        }

        protected override async Task<List<FolderItem>> LoadDataImpl()
        {
            var folders = await FileBrowserService.GetFolders().ConfigureAwait(false);
            var viewModels = folders.Select(f => new FolderItem(f)).ToList();
            return viewModels;
        }

        protected override void OnDataChanged(List<FolderItem> oldValue, List<FolderItem> newValue)
        {
            base.OnDataChanged(oldValue, newValue);
            SelectedFolder = newValue?.FirstOrDefault();
        }

        private void UpdateSelectedFolder()
        {
            if (_selectedFolder == null)
            {
                //TODO: navigate to some placeholder? 
                return;
            }

            var parameters = new NavigationParameters();
            parameters.Add("folder", _selectedFolder.Folder);
            RegionManager.RequestNavigate(Regions.MainViewRightTopContent, Regions.FilesList, parameters);
        }
    }
}
