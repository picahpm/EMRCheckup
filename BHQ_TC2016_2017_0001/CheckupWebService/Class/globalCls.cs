using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DBCheckup;

namespace CheckupWebService.Class
{
	public class globalCls
	{
        public static DateTime GetServerDateTime()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                DateTime nowdatetime = Convert.ToDateTime(dbc.ExecuteQuery<DateTime>("select GetDate()").FirstOrDefault());
                return nowdatetime;
            }
        }
        public static void MessageError(String strEvent, String ErrorCode, String strError)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    log_transaction objlog = new log_transaction();
                    objlog.its_user_by = "System";
                    objlog.its_program = "Web Service";
                    objlog.its_event = strEvent;
                    objlog.its_err_code = ErrorCode;
                    objlog.its_err_msg = strError;
                    objlog.its_err_date = DateTime.Now;
                    dbc.log_transactions.InsertOnSubmit(objlog);
                    dbc.SubmitChanges();
                }
            }
            catch (Exception)
            {

            }
        }
        public static DateTime ConvertDateFromServer(string strdate, string strTime)
        {
            DateTime dateResult;
            try
            {
                DateTime dtdata = Convert.ToDateTime(strdate);
                TimeSpan tarrivaltime;
                if (strTime.IndexOf("S") != -1)
                {
                    tarrivaltime = TimeSpan.Parse(strTime.Replace("PT", "").Replace("H", ":").Replace("M", ":").Replace("S", ""));
                }
                else
                {
                    tarrivaltime = TimeSpan.Parse(strTime.Replace("PT", "").Replace("H", ":").Replace("M", ""));
                }
                dateResult = new DateTime(dtdata.Year, dtdata.Month, dtdata.Day, tarrivaltime.Hours, tarrivaltime.Minutes, tarrivaltime.Seconds);
                return dateResult;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
	}
}
