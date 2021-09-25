using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTools.XamarinForms.UI.Common
{
    public class KeyValueMapping
    {
        public bool IsDefault { get; set; }

        public object Key { get; set; }

        internal object Value { get; set; }
    }
}
