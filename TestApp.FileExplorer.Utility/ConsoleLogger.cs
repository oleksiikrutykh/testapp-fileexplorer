using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Utility
{
    class ConsoleLogger : ILogger
    {
        public bool Enabled { get; set; } = false;

        public void Log(string message)
        {
            if (Enabled)
            {
                Debug.WriteLine(message);
            }
        }

        public void Log(Exception exception)
        {
            if (Enabled)
            {
                Debug.WriteLine(exception.ToString());
            }
        }
    }
}
