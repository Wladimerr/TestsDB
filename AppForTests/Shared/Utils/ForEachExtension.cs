using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Utils
{
    public static class ForEachExtension
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> func)
        {
            foreach (T item in self)
                func(item);
        }
    }
}
