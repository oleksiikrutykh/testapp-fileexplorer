using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Converters
{
    public class ConvertingEventArgs : EventArgs
    {
        public ConvertingEventArgs(object value, Type targetType, object parameter, string cultureName)
        {
            this.Value = value;
            this.TargetType = targetType;
            this.Parameter = parameter;
            this.CultureName = cultureName;
        }

        public object Value { get; private set; }
        public Type TargetType { get; private set; }

        public object Parameter { get; private set; }

        public string CultureName { get; private set; }
    }
}
