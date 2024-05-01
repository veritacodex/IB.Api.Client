using System.Globalization;
using System;
using System.Text.RegularExpressions;

namespace IB.Api.Client.Helper
{
    public static partial class DateHelper
    {
        public const string EuropeanDateFormat = "dd/MM/yyyy HH:mm:ss";
        public const string AmericanDateFormat = "yyyyMMdd HH:mm:ss";
        public static string ConvertToApiDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd HH:mm:ss");
        }
        public static DateTime UnixTimeStampToDateTime(long time)
        {
            var converted = DateTimeOffset.FromUnixTimeSeconds(time);
            return converted.DateTime;
        }
        public static long DateToEpoch(DateTime date, long multiplier = 1)
        {
            return Convert.ToInt64((date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds * multiplier);
        }
        public static DateTime ApiToDate(string date)
        {
            return DateTime.ParseExact(SearchString().Replace(date, " "), AmericanDateFormat, CultureInfo.InvariantCulture);
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex SearchString();
    }
}
