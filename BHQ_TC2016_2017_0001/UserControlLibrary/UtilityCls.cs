using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserControlLibrary
{
    internal static class UtilityCls
    {
        public static double ConvertToDouble(this object val)
        {
            try
            {
                return Convert.ToDouble(val);
            }
            catch
            {
                return 0;
            }
        }

        public static string ConvertToStringDouble(this object val)
        {
            try
            {
                return (Convert.ToDouble(val)).ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string ConvertToStringInteger(this object val)
        {
            try
            {
                return (Convert.ToInt64(val)).ToString();
            }
            catch
            {
                return "";
            }
        }

        public static string ConvertToString(this object val)
        {
            try
            {
                return val.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
