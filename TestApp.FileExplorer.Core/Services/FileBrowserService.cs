using TestApp.FileExplorer.Core.Models;
using External = FolderBrowserLib.Interfaces;
using ExternalFolder = FolderBrowserLib.Interfaces.IFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.FileExplorer.Utility;

namespace TestApp.FileExplorer.Core.Services
{
    class FileBrowserService : IFileBrowserService
    {
        private External.IFolderBrowserLib _folderBrowserLib;

        public FileBrowserService(External.IFolderBrowserLib folderBrowser)
        {
            _folderBrowserLib = FolderBrowserLib.FolderBrowserLib.Instance;
        }

        public async Task<List<IFolder>> GetFolders()
        {
            return await Task.Run(() =>
            {
                return _folderBrowserLib.GetFolders()
                                        .Select(f => new FolderWrapper(f))
                                        .Cast<IFolder>()
                                        .ToList();
            });
        }

        public async Task<List<IFile>> GetFiles(IFolder folder)
        {
            var folderWrapper = Guard.Cast<IFolder, FolderWrapper>(folder, nameof(folder));
            var files = await folderWrapper.FolderImplementation.GetFilesAsync().ConfigureAwait(false);
            return files.Select(f => new FileWrapper(f))
                        .Cast<IFile>()
                        .ToList();
        }
    }
}
