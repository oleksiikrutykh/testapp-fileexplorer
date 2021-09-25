using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Utility
{
    public interface ILogger
    {
        bool Enabled { get; set; }

        void Log(string message);

        void Log(Exception exception);
    }
}
