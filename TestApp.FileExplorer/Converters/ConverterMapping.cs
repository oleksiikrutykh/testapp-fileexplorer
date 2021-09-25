using BaseTools.XamarinForms.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dxo.FileExplorer.Converters
{
    public class ConverterMapping : KeyValueMapping
    {
        public new object Value
        {
            get
            {
                return base.Value;
            }

            set
            {
                base.Value = value;
            }
        }
    }
}
