using TestApp.FileExplorer.Core.Models;
using TestApp.FileExplorer.ViewModels;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.DesignData
{
    class DesignFoldersListViewModel : FoldersListViewModel
    {
        public DesignFoldersListViewModel() :
            base(null, new RegionManager())
        {
            Data = new List<FolderItem>
            {
                new FolderItem(new Folder { Name = "Folder1" }),
                new FolderItem(new Folder { Name = "Folder with the long name - sometimes it can cause design issues" }),
                new FolderItem(new Folder { Name = "One more folder" }),
            };
        }

        private class Folder : IFolder
        {
            public string Name { get; set; }
        }
    }
}
