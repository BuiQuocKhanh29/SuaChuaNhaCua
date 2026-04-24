using System;

namespace FixItNow.Api.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetVietnamTime()
        {
            var vietnamZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamZone);
        }
    }
}
