using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Utils
{
    public static class LetExstension
    {
        public static R Let<T,R>(this T self, Func<T,R> converter)
            => converter(self);

        public static void Let<T>(this T self, Action<T> action)
            => action(self);
    }
}
