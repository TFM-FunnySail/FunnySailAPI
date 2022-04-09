using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Extensions
{
    public static class Extensions
    {
        public static string ToUnixLongTimeStamp(this DateTime date)
        {
            return date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
        }

        public static string ToUnixLongTimeStamp(this DateTimeOffset date)
        {
            return date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds.ToString();
        }
    }
}
