using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Localization
{
    public class GlobalResourceManager
    {
        public static GlobalResourceManager Instance { get; private set; } = new GlobalResourceManager();

        public ResourceManager DefaultResourceManager { get; set; } = LocalizedStrings.ResourceManager;

        public void RegisterResource(string prefix, ResourceManager manager)
        {
            // TODO: add implementation for additional resource dictionaries.
            throw new NotImplementedException("Feature can be added in future");
        }

        public string GetString(string resourceKey)
        {
#if DEBUG
            return GetString(resourceKey, true);
#else
            return GetString(resourceKey, false);
#endif
        }

        public string GetString(string resourceKey, bool throwOnException)
        {
            var result = GetStringSafe(resourceKey);
            if (throwOnException)
            {
                if (result == null)
                {
                    throw new ArgumentException(
                   String.Format($"Key '{resourceKey}' was not found in resources '{DefaultResourceManager.BaseName}' for culture '{CultureInfo.CurrentUICulture}'."),
                 nameof(resourceKey));
                }
            }

            return result;
        }

        public string GetStringSafe(string resourceKey)
        {
            if (String.IsNullOrEmpty(resourceKey))
            {
                return null;
            }

            // TODO: add an ability to add several resource dictionaries, from different projects.
            string result = DefaultResourceManager.GetString(resourceKey, CultureInfo.CurrentUICulture);
            return result;
        }
    }
}
