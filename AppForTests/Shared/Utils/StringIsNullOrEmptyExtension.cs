using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Utils
{
    public static class StringIsNullOrEmptyExtension
    {
        public static bool IsNullOrEmpty(this string self)
            =>  string.IsNullOrEmpty(self);

        
    }
}
