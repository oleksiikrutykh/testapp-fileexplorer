using TestApp.FileExplorer.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Converters
{
    public class FileSizeConverter : ConverterBase
    {
        private const double RangeSize = 1024;

        private static readonly string[] SizeUnits = new string[] 
        {
            LocalizedStrings.FileDetails_BytesSize,
            LocalizedStrings.FileDetails_KilobytesSize,
            LocalizedStrings.FileDetails_MegabytesSize,
            LocalizedStrings.FileDetails_GigabytesSize,
        };

        protected override object Convert(object value, Type targetType, object parameter, string cultureName)
        {
            if (value == null)
            {
                return null;
            }

            long bytesCount = System.Convert.ToInt64(value);
            return ConvertToString(bytesCount);
        }

        public static string ConvertToString(long bytesSize)
        {
            int unitIndex = 0;
            double sizeInUnits = bytesSize;
            while (sizeInUnits > RangeSize && unitIndex < SizeUnits.Length - 1)
            {
                unitIndex++;
                sizeInUnits /= RangeSize;
            }

            string label = string.Format("{0:0.#} {1}", sizeInUnits, SizeUnits[unitIndex]);
            return label;
        }
    }
}
