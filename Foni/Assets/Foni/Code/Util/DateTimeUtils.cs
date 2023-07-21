using System;
using System.Globalization;

namespace Foni.Code.Util
{
    public static class DateTimeUtils
    {
        public static DateTime TimestampToDateTime(string timestamp)
        {
            return DateTime
                .Parse(timestamp, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
        }

        public static string DateTimeToFriendlyString(DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string TimeSpanToFriendlyString(TimeSpan timeSpan)
        {
            return $"{Math.Floor(timeSpan.TotalMinutes)}m {timeSpan.Seconds}s";
        }
    }
}