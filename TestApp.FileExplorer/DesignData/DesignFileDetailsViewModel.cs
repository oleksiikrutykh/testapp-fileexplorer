using TestApp.FileExplorer.Core.Models;
using TestApp.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.DesignData
{
    class DesignFileDetailsViewModel : FileDetailsViewModel
    {
        public DesignFileDetailsViewModel()
        {
            var file = new FileStub
            {
                CreationDate = DateTime.Now,
                Name = "Sample file",
                Size = 1200345,
            };

            SetData(file, null);
        }

        private class FileStub : IFile
        {
            public string Name { get; set; }

            public long Size { get; set; }

            public DateTime CreationDate { get; set; }

            public string Id { get; set; }
        }
    }
}
