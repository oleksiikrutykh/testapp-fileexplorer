using TestApp.FileExplorer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using External = FolderBrowserLib.Interfaces;
using ExternalFolder = FolderBrowserLib.Interfaces.IFolder;

namespace TestApp.FileExplorer.Core.Models
{
    class FolderWrapper : IFolder
    {
        internal FolderWrapper(ExternalFolder folder)
        {
            folder = Guard.CheckIsNotNull(folder, nameof(folder));
            FolderImplementation = folder;
        }

        internal ExternalFolder FolderImplementation { get; }

        public string Name { get => FolderImplementation.Name; }

    }
}
