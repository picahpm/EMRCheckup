using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace CallDataTakeCareGetTextFile
{
    public static partial class Program
    {
        public static void MessageError(string strError)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                log_transaction objlog = new log_transaction();
                objlog.its_user_by = "";
                objlog.its_program = "";
                objlog.its_event = "";
                objlog.its_err_code = "";
                objlog.its_err_msg = strError;
                objlog.its_err_date = Program.GetServerDateTime();
                dbc.log_transactions.InsertOnSubmit(objlog);
                dbc.SubmitChanges();
            }
        }
        public static void MessageError(string strEvent, string ErrorCode, string strError)
        {
            MessageError(strEvent, ErrorCode, strError, true);
        }
        public static void MessageError(string strEvent, string ErrorCode, string strError, bool isShowMsg)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    log_transaction objlog = new log_transaction();
                    objlog.its_user_by = "";
                    objlog.its_program = "";
                    objlog.its_event = strEvent;
                    objlog.its_err_code = ErrorCode;
                    objlog.its_err_msg = strError;
                    objlog.its_err_date = Program.GetServerDateTime();
                    dbc.log_transactions.InsertOnSubmit(objlog);
                    dbc.SubmitChanges();
                }
            }
            catch (Exception)
            {

            }
        }
        public static void MessageError(string strEvent, string ErrorCode, Exception ex, bool isShowMsg)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    log_transaction objlog = new log_transaction();
                    objlog.its_user_by = "";
                    objlog.its_program = "";
                    objlog.its_tpr_id = 0;
                    objlog.its_event = strEvent;
                    objlog.its_err_code = ErrorCode;
                    objlog.its_err_msg = (ex == null) ? "" : ex.Message;
                    objlog.its_StackTrace = (ex == null) ? "" : ex.StackTrace;
                    objlog.its_err_date = Program.GetServerDateTime();
                    dbc.log_transactions.InsertOnSubmit(objlog);
                    dbc.SubmitChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public static DateTime GetServerDateTime()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                DateTime nowdatetime = Convert.ToDateTime(dbc.ExecuteQuery<DateTime>("select GetDate()").FirstOrDefault());
                return nowdatetime;
            }
        }
    }
}
