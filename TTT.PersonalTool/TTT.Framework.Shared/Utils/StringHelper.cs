using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT.Framework.Utils
{
    public static class StringHelper
    {
        public static bool IsString(this object value)
        {
            return value is string
                    || value is char;
        }
    }
}
