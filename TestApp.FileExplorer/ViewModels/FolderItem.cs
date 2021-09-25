using TestApp.FileExplorer.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels
{
    class FolderItem : BindableBase
    {

        public FolderItem(IFolder folder)
        {
            Folder = folder;
            Name = folder.Name;
        }

        public IFolder Folder { get; }

        public string Name { get; }
    }
}
