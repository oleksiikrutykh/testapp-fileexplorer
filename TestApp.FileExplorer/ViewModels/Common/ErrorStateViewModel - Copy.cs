using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dxo.FileExplorer.ViewModels.Common
{
    class ErrorStateViewModel : PlaceholderViewModel
    {
        private Lazy<ErrorMessagePresenter> _errorMessagePresenter = new Lazy<ErrorMessagePresenter>();

        private string _errorMessage;

        public ErrorMessagePresenter ErrorConverter { get => _errorMessagePresenter.Value; }

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
            }

            ErrorMessage = ErrorConverter.ConvertError(ex);
        }

        public void Reset()
        {
            ErrorMessage = null;
        }
    }
}
