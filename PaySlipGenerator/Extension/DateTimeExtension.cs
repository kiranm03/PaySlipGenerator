using System;
using System.Collections.Generic;
using System.Text;

namespace PaySlipGenerator.Extension
{
    public static class DateTimeExtension
    {
        public static string ToPayslipDateText(this DateTime date)
        {
            var month = date.ToString("MMMM");
            var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
            return 
                $"01 {month} - {lastDayOfMonth} {month}";
        }
    }
}
