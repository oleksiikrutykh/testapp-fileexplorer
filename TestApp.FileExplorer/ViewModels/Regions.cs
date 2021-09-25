using TestApp.FileExplorer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ViewModels
{
    public static class Regions
    {
        public static readonly string MainViewLeftRegion = "FoldersView";

        public static readonly string MainViewRightTopContent = "RightTopContent";

        public static readonly string MainViewRightBottomContent = "RightBottomContent";




        public static readonly string FolderList = nameof(FoldersListView);

        public static readonly string FilesList = nameof(FilesListView);

        public static readonly string FileDetails = nameof(FileDetailsView);
    }
}
