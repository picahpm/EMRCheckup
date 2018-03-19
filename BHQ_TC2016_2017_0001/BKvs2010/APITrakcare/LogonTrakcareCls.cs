using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using setLogUser;

namespace BKvs2010.APITrakcare
{
    public class LogonTrakcareCls
    {
        private const int CheckupAppCode = 61;
        public bool CheckTrakcarePassword(string username, string password)
        {
            try
            {
                using (WS_Trakcare.WS_GetDataBytrakSoapClient ws = new WS_Trakcare.WS_GetDataBytrakSoapClient())
                {
                    DataTable dt = ws.LogonTrakcare(username, password);
                    if (dt.Rows.Count > 0)
                    {
                        SetLogUser sl = new SetLogUser();
                        sl.setItem(CheckupAppCode, username);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
