using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels.Common
{
    class EmptyStateViewModel<TData> : PlaceholderViewModel
    {
        private string _message;

        private object _trackedObject;

        private bool _valueLoaded;

        private bool _isEmpty;

        public EmptyStateViewModel()
        {
            DetermineIsEmpty();
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private bool ValueLoaded
        {
            get => _valueLoaded;
            set
            {
                if (SetProperty(ref _valueLoaded, value))
                {
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }
        public bool IsEmpty
        {
            get => _isEmpty;
            private set
            {
                bool changed = SetProperty(ref _isEmpty, value);
                if (changed)
                {
                    RaisePropertyChanged(nameof(IsActive));
                }
            }
        }

        public object TrackedObject
        {
            get => _trackedObject;
            private set
            {
                if (!Object.Equals(_trackedObject, value))
                {
                    var oldCollection = _trackedObject as INotifyCollectionChanged;
                    if (oldCollection != null)
                    {
                        oldCollection.CollectionChanged += OnCollectionChanged;
                    }

                    _trackedObject = value;

                    var newCollection = _trackedObject as INotifyCollectionChanged;
                    if (newCollection != null)
                    {
                        newCollection.CollectionChanged += OnCollectionChanged;
                    }

                    DetermineIsEmpty();
                }
            }
        }

        public override bool IsActive => ValueLoaded && IsEmpty;

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DetermineIsEmpty();
        }

        private void DetermineIsEmpty()
        {
            if (Object.Equals(_trackedObject, default(TData)))
            {
                IsEmpty = true;
                return;
            }

            if (_trackedObject is ICollection collection)
            {
                IsEmpty = collection.Count == 0;
                return;
            }

            IsEmpty = false;
        }

        public void UpdateTrackedData(TData data)
        {
            ValueLoaded = true;
            TrackedObject = data;
        }

        public void Reset()
        {
            ValueLoaded = false;
            TrackedObject = default(TData);
        }
    }
}
