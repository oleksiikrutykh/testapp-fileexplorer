using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading
{
    public enum PriorityLevel
    {
        Cancelled = 0,
        BackgroundLoading = 50,
        ForegroundLoading = 100,
    }
}
