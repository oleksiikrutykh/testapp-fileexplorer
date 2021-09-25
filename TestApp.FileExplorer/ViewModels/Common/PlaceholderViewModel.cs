using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels.Common
{
    abstract class PlaceholderViewModel : BindableBase
    {
        public abstract bool IsActive { get; }

    }
}
