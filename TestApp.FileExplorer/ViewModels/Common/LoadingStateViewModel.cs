using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels.Common
{
    class LoadingStateViewModel : PlaceholderViewModel
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (SetProperty(ref _isLoading, value))
                {
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }

        public override bool IsActive => IsLoading;
    }
}
