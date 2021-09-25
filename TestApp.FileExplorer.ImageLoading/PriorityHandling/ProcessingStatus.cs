using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading
{
    enum ExecutionStatus
    {
        Enqueued,
        InProgress,
        Cancelled,
        Faulted,
        Completed,
    }
}
