using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FolderBrowserLib.Interfaces;

namespace TestApp.FileExplorer.Core.Models
{
    public interface IFile
    {
        string Name { get; }

        long Size { get; }

        DateTime CreationDate { get; }

        string Id { get; }
    }
}
