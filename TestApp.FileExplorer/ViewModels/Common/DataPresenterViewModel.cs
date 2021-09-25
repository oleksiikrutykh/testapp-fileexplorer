using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels.Common
{
    abstract class DataPresenterViewModel<TData> : ViewModelBase
    {
        private TData _data;

        private CancellationTokenSource _dataLoadingCancellationSource = new CancellationTokenSource();

        public DataPresenterViewModel()
        {
            EmptyState = new EmptyStateViewModel<TData>();
            ErrorState = new ErrorStateViewModel();
            LoadingState = new LoadingStateViewModel();
        }

        public TData Data
        {
            get => _data;
            set
            {

                if (!Object.Equals(_data, value))
                {
                    var oldData = _data;
                    _data = value;
                    RaisePropertyChanged(nameof(Data));
                    OnDataChanged(_data, value);
                }
            }
        }


        public ErrorStateViewModel ErrorState { get; }

        public EmptyStateViewModel<TData> EmptyState { get; }

        public LoadingStateViewModel LoadingState { get; }


        public async Task LoadData()
        {
            Reset();
            var token = _dataLoadingCancellationSource.Token;
            LoadingState.IsLoading = true;

            TData newData = default(TData);
            Exception exception = null;

            try
            {
                newData = await LoadDataImpl();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            SetData(newData, exception);
        }

        public void SetData(TData newValue, Exception ex)
        {
            LoadingState.IsLoading = false;
            ErrorState.ShowError(ex);
            if (ex == null)
            {
                EmptyState.UpdateTrackedData(newValue);
            }
            else
            {
                EmptyState.Reset();
            }

            Data = newValue;
        }

        public void Reset()
        {
            _dataLoadingCancellationSource.Cancel();
            _dataLoadingCancellationSource = new CancellationTokenSource();
            Data = default(TData);
            EmptyState.Reset();
            ErrorState.Reset();
            LoadingState.IsLoading = false;
        }

        protected abstract Task<TData> LoadDataImpl();

        protected virtual void OnDataChanged(TData oldValue, TData newValue)
        {
        }
    }
}
