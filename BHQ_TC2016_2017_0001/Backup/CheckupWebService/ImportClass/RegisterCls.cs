using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.ImportClass
{
    public class RegisterCls
    {
        public void Regis(ref trn_patient_regi pregis, APITrakcare.GetPTArrivedResult arrived)
        {
            try
            {
                pregis.tpr_appoint_type = islate(arrived.appointdate, arrived.arrivaldate) ? 'L' : 'T';
                pregis.tpr_appointment_date = arrived.appointdate;
                pregis.tpr_arrive_date = arrived.arrivaldate;
                pregis.tpr_arrive_type = arrived.appointdate != null ? 'A' : 'W';
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("RegisterCls", "Regis", ex.Message);
                throw ex;
            }
        }
        private bool islate(DateTime? appoint, DateTime? arrived)
        {
            if (appoint == null)
            {
                return true;
            }
            else
            {
                int limit = getlimitlatein();
                if (appoint.Value.Subtract(arrived.Value).TotalMinutes <= limit)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }
        private int getlimitlatein()
        {
            return 1440;
        }
    }
}