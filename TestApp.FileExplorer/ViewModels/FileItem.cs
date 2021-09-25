using TestApp.FileExplorer.Core.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels
{
    class FileItem : BindableBase
    {
        public FileItem(IFile file)
            : this(file, FileImageKind.Thumbnail)
        {
        }

        public FileItem(IFile file, FileImageKind imageKind)
        {
            File = file;
            Name = file.Name;
            ImageSource = new FileImage
            {
                File = file,
                Kind = imageKind,
            };
        }

        public IFile File { get; } 

        public string Name { get; }

        public FileImage ImageSource { get; }
    }
}
