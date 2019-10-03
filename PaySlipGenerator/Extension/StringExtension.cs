using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaySlipGenerator.Extension
{
    public static class StringExtension
    {
        public static DateTime ToDate(this string str)
        {
            var month = str
                        .Split(' ')
                        .Skip(1)
                        .First();

            return DateTime.Parse($"01 {month} 2018");
        }
    }
}
