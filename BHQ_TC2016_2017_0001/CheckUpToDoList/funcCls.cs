using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.IO;
using System.Text;
using DBToDoList;

namespace CheckUpToDoList
{
    public static class funcCls
    {
        public static System.Nullable<int> convStrToInt(string value)
        {
            System.Nullable<int> num = null;
            try
            {
                num = Convert.ToInt32(value);
            }
            catch
            {

            }
            return num;
        }
        public static System.Nullable<Double> convStrToDouble(string value)
        {
            System.Nullable<Double> num = null;
            try
            {
                num = Convert.ToDouble(value);
            }
            catch
            {

            }
            return num;
        }
        public static DateTime GetServerDateTime()
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                //var EnCulture = CultureInfo.CreateSpecificCulture("en-US");
                DateTime nowdatetime = dbc.ExecuteQuery<DateTime>("select GetDate()").FirstOrDefault();//.ToString(EnCulture));
                return nowdatetime;
            }
        }

        private static string sLogFormat;
        private static string sErrorTime;
       

        public static void ErrorLog(string sErrMsg)
        {
            if ( Constant.IsWriteLog == "0")
            {
                return;
            }
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";

            //this variable used to create log filename format "
            //for example filename : ErrorLogYYYYMMDD
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            sErrorTime = sYear + sMonth + sDay;
            string serverpath = HttpContext.Current.Server.MapPath("Logs/");
            StreamWriter sw = new StreamWriter(serverpath + sErrorTime+".txt", true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }
    }
}