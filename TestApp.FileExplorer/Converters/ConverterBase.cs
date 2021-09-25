using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestApp.FileExplorer.Converters
{
    public class ConverterBase : IValueConverter
    {
        public event EventHandler<ConvertingEventArgs> Converting;

        public event EventHandler<ConvertedEventArgs> Converted;

        public event EventHandler<ConvertingEventArgs> ConvertingBack;

        public event EventHandler<ConvertedEventArgs> ConvertedBack;

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cultureName = culture.Name;
            EventHandler<ConvertingEventArgs> convertingHandler = this.Converting;
            if (convertingHandler != null)
            {
                var args = new ConvertingEventArgs(value, targetType, parameter, cultureName);
                convertingHandler.Invoke(this, args);
            }

            object result = this.Convert(value, targetType, parameter, cultureName);

            EventHandler<ConvertedEventArgs> convertedHandler = this.Converted;
            if (convertedHandler != null)
            {
                var args = new ConvertedEventArgs(result, value, targetType, parameter, cultureName);
                convertedHandler.Invoke(this, args);
            }

            return result;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string cultureName = culture.Name;
            EventHandler<ConvertingEventArgs> convertingHandler = this.ConvertingBack;
            if (convertingHandler != null)
            {
                var args = new ConvertingEventArgs(value, targetType, parameter, cultureName);
                convertingHandler.Invoke(this, args);
            }

            object result = this.ConvertBack(value, targetType, parameter, culture.Name);

            EventHandler<ConvertedEventArgs> convertedHandler = this.ConvertedBack;
            if (convertedHandler != null)
            {
                var args = new ConvertedEventArgs(result, value, targetType, parameter, cultureName);
                convertedHandler.Invoke(this, args);
            }

            return result;
        }

        protected virtual object Convert(object value, Type targetType, object parameter, string cultureName)
        {
            throw new NotImplementedException("Converting isn't implemented");
        }

        protected virtual object ConvertBack(object value, Type targetType, object parameter, string cultureName)
        {
            throw new NotSupportedException("Converting back isn't supported");
        }
    }
}
