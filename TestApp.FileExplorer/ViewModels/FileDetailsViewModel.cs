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
    class FileDetailsViewModel : DataPresenterViewModel<IFile>, INavigationAware, IRegionMemberLifetime
    {
        private IFile _file;

        public FileDetailsViewModel()
        {
            EmptyState.Message = LocalizedStrings.FileDetails_EmptyMessage;
        }

        public long Size => Data?.Size ?? 0;

        public DateTime CreationDate => Data?.CreationDate ?? new DateTime();

        public string Name => Data?.Name;

        public FileImage Image
        {
            get
            {
                if (Data == null)
                {
                    return null;
                }

                return new FileImage { File = Data, Kind = FileImageKind.FullPreview };
            }
        }

        public bool KeepAlive => true;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _file = navigationContext.Parameters["file"] as IFile;
            await LoadData();
        }

        protected override Task<IFile> LoadDataImpl()
        {
            return Task.FromResult(_file);
        }

        protected override void OnDataChanged(IFile oldValue, IFile newValue)
        {
            base.OnDataChanged(oldValue, newValue);
            RaisePropertyChanged(nameof(Size));
            RaisePropertyChanged(nameof(CreationDate));
            RaisePropertyChanged(nameof(Name));
            RaisePropertyChanged(nameof(Image));
        }

        
    }
}
