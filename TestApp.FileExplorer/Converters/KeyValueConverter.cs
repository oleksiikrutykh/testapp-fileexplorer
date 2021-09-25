using BaseTools.XamarinForms.UI.Common;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BaseTools.XamarinForms.UI.Converters
{
    [ContentProperty("Mappings")]
    public class KeyValueConverter : Converter
    {
        public KeyValueConverter()
        {
            this.Mappings = new List<KeyValueMapping>();
        }

        public List<KeyValueMapping> Mappings { get; private set; }

        protected override object Convert(object value, Type targetType, object parameter, string cultureName)
        {
            object result = KeyValueMappingUtility.DetermineValue(value, this.Mappings);
            return result;
        }
    }
}
