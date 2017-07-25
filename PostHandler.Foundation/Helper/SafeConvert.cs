namespace PostHandler.Foundation.Helper
{
    using System;

    public static class SafeConvert
    {
        public static string ToString(int? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToString(Guid? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToString(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString("d");
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToString(DateTime? value, bool showTime)
        {
            if (value.HasValue)
            {
                return showTime ? value.Value.ToString("g") : value.Value.ToString("d");
            }
            else
            {
                return string.Empty;
            }
        }

        public static DateTime? ToDateTime(object value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch { return null; }
            }
        }

        public static DateTime? ToDatePickerTime(object value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(value.ToString())) return null;
                    return Convert.ToDateTime(value);
                }
                catch { return null; }
            }
        }

        public static Guid? ToGuid(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return new Guid(value);
            }
            else
            {
                return null;
            }
        }

        public static long? ToInt64(object value)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch { return null; }
        }

        public static long? ToInt64(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            try
            {
                return Convert.ToInt64(value);
            }
            catch { return null; }
        }

        public static Int32? ToInt32(object value)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch { return null; }
        }

        public static Int32 ToInt(object value)
        {
            Int32 result = 0;
            try
            {
                result = Convert.ToInt32(value);
                return result;
            }
            catch { return result; }
        }

        public static Int16? ToInt16(object value)
        {
            try
            {
                return Convert.ToInt16(value);
            }
            catch { return null; }
        }

        public static bool? ToBoolean(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch { return null; }
        }

        public static decimal? ToDecimal(object value)
        {
            try
            {
                return Convert.ToDecimal(value);
            }
            catch { return null; }
        }

        public static double? ToDouble(object value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch { return null; }
        }

        public static string ToString(decimal? value)
        {
            if (value.HasValue)
            {
                return string.Format("{0:0.00}", value.Value);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToString(double? value)
        {
            if (value.HasValue)
            {
                return string.Format("{0:0.00}", value.Value);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            else
            {
                return value.ToString();
            }
        }

        public static DateTime AssignDate(DateTime oldDate, DateTime newDate)
        {
            if (oldDate == default(DateTime) && newDate != default(DateTime))
            {
                return newDate.ToUniversalTime();
            }
            else if (oldDate != default(DateTime) && newDate != default(DateTime))
            {
                if (oldDate.Date != newDate.ToUniversalTime().Date)
                {
                    return newDate.ToUniversalTime();
                }
                else return oldDate;
            }
            else return default(DateTime);
        }

        public static DateTime? AssignDate(DateTime? oldDate, DateTime? newDate)
        {
            if (!newDate.HasValue)
            {
                return null;
            }
            else if (oldDate == null && newDate != null)
            {
                return newDate.Value.ToUniversalTime();
            }
            else if (oldDate != null && newDate != null)
            {
                if (oldDate.Value.Date != newDate.Value.ToUniversalTime().Date)
                {
                    return newDate.Value.ToUniversalTime();
                }
                else return oldDate;
            }
            else return null;
        }

    }
}