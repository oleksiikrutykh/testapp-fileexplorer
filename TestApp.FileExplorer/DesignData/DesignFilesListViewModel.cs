using TestApp.FileExplorer.Core.Models;
using TestApp.FileExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.DesignData
{
    class DesignFilesListViewModel : FilesListViewModel
    {
        public DesignFilesListViewModel()
            : base(null, null)
        {
            Data = new List<FileItem>
            {
                new FileItem(new File { Name = "Test 1"}),
                new FileItem(new File { Name = "Test 2"}),
                new FileItem(new File { Name = "Test 3"}),
                new FileItem(new File { Name = "Test file with the long name - sometimes it can cause design issues"}),
            };
        }

        private class File : IFile
        {
            public string Name { get; set; }

            public long Size { get; set; }

            public DateTime CreationDate { get; set; }

            public string Id { get; set; }
        }
    }
}
