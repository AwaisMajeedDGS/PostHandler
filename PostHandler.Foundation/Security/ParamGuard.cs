namespace PostHandler.Foundation.Security
{
    using System;

    public static class ParamGuard
    {
        public static void ArgumentNotNull(object parameterValue, string parameterName)
        {
            if (default(object) == parameterValue)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static bool ArgumentNotNull(object parameterValue)
        {
            return (default(object) != parameterValue);
        }

        public static void ArgumentNotNullOrWhiteSpace(string parameterValue, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static Boolean ArgumentNotNullOrWhiteSpace(string parameterValue)
        {
            return (!string.IsNullOrWhiteSpace(parameterValue));
        }

        public static Boolean ArgumentIsNull(object parameter, string value)
        {
            if (parameter == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static Boolean IsReferenceEquals(object parameter)
        {
            return ReferenceEquals(parameter, null);
        }

    }
}