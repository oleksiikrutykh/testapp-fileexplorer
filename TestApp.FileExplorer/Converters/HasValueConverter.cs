using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;
using System.Windows;

namespace TestApp.FileExplorer.Converters
{
    public class HasValueConverter : ConverterBase
    {
        private const string InvertParameter = "invert";

        public object TrueValue { get; set; } = Visibility.Visible;

        public object FalseValue { get; set; } = Visibility.Collapsed;

        protected override object Convert(object value, Type targetType, object parameter, string cultureName)
        {
            var hasValue = HasValue(value);
            hasValue = InvertIfRequired(hasValue, parameter);
            var data = ConvertToData(hasValue);
            return data;
        }

        private bool HasValue(object data)
        {
            if (data == null)
            {
                return false;
            }

            if (data is bool)
            {
                return (bool)data;
            }

            if (data is int)
            {
                int valueInt = (int)data;
                return valueInt != 0;
            }

            if (data is double)
            {
                var valueDouble = (double)data;
                return !Double.IsNaN(valueDouble) &&
                       Math.Abs(valueDouble) > double.Epsilon;
            }

            if (data is string)
            {
                return !String.IsNullOrEmpty((string)data);
            }

            if (data is ICollection collection)
            {
                return collection.Count > 0;
            }

            if (data is ValueType valueType)
            {
                var type = data.GetType();
                var defaultValue = Activator.CreateInstance(type);
                return !Object.Equals(defaultValue, defaultValue);
            }

            return true;
        }

        private object ConvertToData(bool value)
        {
            if (value)
            {
                return TrueValue;
            }
            else
            {
                return FalseValue;
            }
        }


        private bool InvertIfRequired(bool value, object parameter)
        {
            var result = value;
            if (parameter != null)
            {
                var stringParameter = (string)parameter;
                if (stringParameter == InvertParameter)
                {
                    result = !result;
                }
            }

            return result;
        }
    }

}
