using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Converters
{
    public class EmptyConverter : ConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter, string cultureName)
        {
            return value;
        }

        protected override object ConvertBack(object value, Type targetType, object parameter, string cultureName)
        {
            return value;
        }
    }
}
