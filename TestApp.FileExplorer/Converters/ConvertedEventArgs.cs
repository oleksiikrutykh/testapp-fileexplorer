using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Converters
{
    public class ConvertedEventArgs : ConvertingEventArgs
    {
        public ConvertedEventArgs(object result, object value, Type targetType, object parameter, string cultureName)
            : base(value, targetType, parameter, cultureName)
        {
            this.Result = result;
        }

        public object Result { get; private set; }
    }
}
