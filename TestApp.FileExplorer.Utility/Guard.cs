using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.Utility
{
    public static class Guard
    {
        public static TTarget Cast<TOrigin, TTarget>(this TOrigin origin, string parameterName) where TTarget : TOrigin
        {
            try
            {
                return (TTarget)origin;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException(parameterName);
            }
        }

        public static T CheckIsNotNull<T>(T value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }
    }
}
