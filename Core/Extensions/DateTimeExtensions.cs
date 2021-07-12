using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPrettyDate(this DateTime date, CultureInfo culture)
        {
            if (culture==null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            return date.ToString("yyyyMMdd", culture);
        }
    }
}
