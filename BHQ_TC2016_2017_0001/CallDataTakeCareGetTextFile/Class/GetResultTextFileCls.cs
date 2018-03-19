using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using DBCheckup;

namespace CallDataTakeCareGetTextFile.Class
{
    public class GetResultTextFileCls
    {
        public void retrieveHistory()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<trn_RefreshLabHistory> list_refresh = cdc.trn_RefreshLabHistories.Where(x => x.status == false).ToList();
                    foreach (trn_RefreshLabHistory trl in list_refresh)
                    {
                        retrieveHistory(trl);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetResultTextFileCls", "retrieveHistory", ex, false);
            }
        }
        public void retrieveHistory(trn_RefreshLabHistory trl)
        {
            try
            {
                using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                {
                    ws.RetrieveImaging((int)trl.tpr_id, "JobImage");
                }
                //using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                //{
                //    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                //    var patient = cdc.trn_patient_regis
                //                     .Where(x => x.tpr_id == trl.tpr_id)
                //                     .Select(x => new
                //                     {
                //                         hn = x.trn_patient.tpt_hn_no,
                //                         en = x.tpr_en_no,
                //                         arrived = x.trn_patient_regis_detail == null ? x.tpr_arrive_date.Value.Date : x.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date
                //                     }).FirstOrDefault();
                //    using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                //    {
                //        ws.InsertDBEmrCheckupResultXray(patient.hn, patient.en, patient.arrived.AddYears(-5), patient.arrived, false);
                //    }
                //}
            }
            catch (Exception ex)
            {
                Program.MessageError("GetResultTextFileCls", "retrieveHistory", ex, false);
            }
        }
    }
}
