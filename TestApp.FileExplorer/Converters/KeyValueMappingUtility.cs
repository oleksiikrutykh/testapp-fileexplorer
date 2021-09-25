using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTools.XamarinForms.UI.Common
{
    internal static class KeyValueMappingUtility
    {
        public static object DetermineValue(object key, IEnumerable<KeyValueMapping> mappings)
        {
            object result = null;
            bool isValueFound = false;
            foreach (KeyValueMapping mapping in mappings)
            {
                bool isKeyMatch = IsKeyMatch(mapping.Key, key);
                if (isKeyMatch)
                {
                    result = mapping.Value;
                    isValueFound = true;
                    break;
                }
            }

            if (!isValueFound)
            {
                KeyValueMapping defaultMapping = mappings.FirstOrDefault(m => m.IsDefault);
                if (defaultMapping != null)
                {
                    result = defaultMapping.Value;
                }
            }

            return result;
        }

        private static bool IsKeyMatch(object key, object value)
        {
            bool areEquals = false;
            string stringKey = key as string;
            if (stringKey != null)
            {
                areEquals = IsStringKeyMatch(stringKey, value);
            }
            else
            {
                areEquals = Object.Equals(key, value);
            }

            return areEquals;
        }

        private static bool IsStringKeyMatch(string key, object value)
        {
            string stringValue = value as string;
            if (stringValue == null)
            {
                if (value != null)
                {
                    stringValue = Convert.ToString(value, CultureInfo.InvariantCulture);
                }
            }

            return key == stringValue;
        }
    }
}
