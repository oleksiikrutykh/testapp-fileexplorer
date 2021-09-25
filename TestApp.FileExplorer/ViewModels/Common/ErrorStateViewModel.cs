using TestApp.FileExplorer.Localization;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp.FileExplorer.ViewModels.Common
{
    class ErrorStateViewModel : PlaceholderViewModel
    {
        private string _errorMessage;

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (SetProperty(ref _errorMessage, value))
                {
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }

        public override bool IsActive => !String.IsNullOrEmpty(_errorMessage);

        public ICommand ReloadCommand { get; set; }

        public void ShowError(Exception ex)
        { 
            if (ex == null)
            {
                ErrorMessage = null;
                return;
            }

            //TODO: add different messages for varios errors.
            ErrorMessage = LocalizedStrings.Error_GenericError;
        }

        public void Reset()
        {
            ErrorMessage = null;
        }
    }
}
