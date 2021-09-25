using TestApp.FileExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels
{
    class FileImage
    {
        public IFile File { get; set; }

        public FileImageKind Kind { get; set; }

        public override string ToString()
        {
            return $"File image: {File?.Id} - {Kind}"; 
        }
    }

   
}
