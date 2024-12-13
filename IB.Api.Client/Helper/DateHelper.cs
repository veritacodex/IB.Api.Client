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
            return Convert.ToInt64((date - DateTime.UnixEpoch).TotalSeconds * multiplier) * 1000;
        }
        public static DateTime ApiToDate(string date)
        {
            return DateTime.ParseExact(SearchString().Replace(date, " "), AmericanDateFormat, CultureInfo.InvariantCulture);
        }

        [GeneratedRegex(@"\s+")]
        private static partial Regex SearchString();
    }
}
