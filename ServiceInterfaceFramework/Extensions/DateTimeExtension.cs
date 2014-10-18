using System;

namespace ServiceInterfaceFramework
{
    public static class DateTimeExtension
    {
        private static DateTime m_minValue = new DateTime(1900, 1, 1);
        private static DateTime m_maxValue = new DateTime(3000, 12, 31);

        public const string SHORT_FORMAT_STRING = "yyyy-MM-dd";
        public const string LONG_FROMAT_STRING = "yyyy-MM-dd HH:mm";

        public static DateTime MinValue
        {
            get
            {
                return m_minValue;
            }
        }

        public static DateTime MaxValue
        {
            get
            {
                return m_maxValue;
            }
        }

        public static DateTime ToDateTime(this string str)
        {
            return DateTime.Parse(str);
        }
    }
}
