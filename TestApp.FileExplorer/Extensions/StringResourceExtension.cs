using TestApp.FileExplorer.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace TestApp.FileExplorer.Extensions
{
    [ContentProperty("ResourceKey")]
    public class StringResourceExtension : MarkupExtension
    {
        public StringResourceExtension(string resourceKey)
        {
            ResourceKey = resourceKey;
        }

        [ConstructorArgument("resourceKey")]
        public string ResourceKey { get; set; }

        public IValueConverter Converter { get; set; }

        public object ConverterParameter { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var value = GlobalResourceManager.Instance.GetString(ResourceKey);
            if (Converter != null)
            {
                var convertedValue = Converter.Convert(value, typeof(string), ConverterParameter, CultureInfo.CurrentCulture);
                value = convertedValue?.ToString();
            }

            return value;
        }
    }
}
