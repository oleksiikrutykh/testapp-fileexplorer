using TestApp.FileExplorer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Core.Services
{
    public interface IFileBrowserService
    {
        Task<List<IFolder>> GetFolders();

        Task<List<IFile>> GetFiles(IFolder folder);
    }
}
