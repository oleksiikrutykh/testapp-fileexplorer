using TestApp.FileExplorer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExternalFile = FolderBrowserLib.Interfaces.IImageFile;

namespace TestApp.FileExplorer.Core.Models
{
    class FileWrapper : IFile
    {
        internal FileWrapper(ExternalFile file)
        {
            file = Guard.CheckIsNotNull(file, nameof(file));
            File = file;
        }

        internal ExternalFile File { get; }

        public string Name { get => File.Name; }

        public long Size { get => File.FileSize; }

        public DateTime CreationDate { get => File.CreationDate; }

        public string Id { get => File.Id.ToString(); }

    }
}
