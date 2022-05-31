using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Domain
{
    public static class DateTimeFormatter
    {
        public static string FromSecondsToLettersString(int seconds)
        {
            var hours = seconds / 3600;
            var minutes = seconds / 60;
            var minseconds = seconds % 60;

            return "" +
                (hours > 0 ? hours + "ч " : "")
                +
                (minutes > 0 ? minutes + "м " : "")
                +
                minseconds + "c";
        }

        public static string FromSecondsToTimeString(int seconds)
        {
            var hours = seconds / 3600;
            var minutes = seconds / 60;
            var minseconds = seconds % 60;

            return $"{hours}:{minutes}:{minseconds}";
        }
    }
}
