using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Utility
{
    public static class Logger
    {
        public static ILogger Instance { get; internal set; } = new ConsoleLogger();
    }
}
