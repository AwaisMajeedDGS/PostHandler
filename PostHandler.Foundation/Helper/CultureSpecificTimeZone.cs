namespace PostHandler.Foundation.Helper
{
    using System;

    public static class CultureSpecificTimeZone
    {
        public static TimeZoneInfo FindByCulture(string culture)
        {
            switch (culture)
            {
                case "ur-PK": return TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                default: return TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");
            }
        }
    }
}