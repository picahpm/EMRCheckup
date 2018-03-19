using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Windows.Forms;

namespace BKvs2010.Class
{
    public partial class ReserveSkipCls
    {
        public int? CheckRoomSkip(int? tpr_id)
        {
            try
            {
                if (tpr_id != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        if (PatientRegis != null)
                        {
                            trn_patient_skip_queue PatientSkipQueue = PatientRegis.trn_patient_skip_queues.Where(x => x.tpsq_active == true).FirstOrDefault();
                            if (PatientSkipQueue != null)
                            {
                                return (int)PatientSkipQueue.mrm_id;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Program.MessageError("ReserveSkipCls", "ReserveSkipCls", ex, false);
                return null;
            }
        }
        public void SendAndReserve(int? tpr_id)
        {
            try
            {
                if (tpr_id != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        if (PatientRegis != null)
                        {
                            trn_patient_skip_queue PatientSkipQueue = PatientRegis.trn_patient_skip_queues.Where(x => x.tpsq_active == true).FirstOrDefault();
                            if (PatientSkipQueue != null)
                            {
                                PatientSkipQueue.tpsq_active = false;
                                if (PatientSkipQueue.mrm_id != null)
                                {
                                    trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues.Where(x => x.mrm_id == PatientSkipQueue.mrm_id).FirstOrDefault();
                                    if (PatientQueue != null)
                                    {
                                        PatientQueue.tps_reserve = true;
                                    }
                                }
                            }
                            trn_patient_skip_queue PatientNewSkipQueue = PatientRegis.trn_patient_skip_queues.Where(x => x.tpsq_active == null).FirstOrDefault();
                            if (PatientNewSkipQueue != null)
                            {
                                PatientNewSkipQueue.tpsq_active = true;
                            }
                            cdc.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ReserveSkipCls", "ReserveSkipCls", ex, false);
            }
        }

        public string MessegeAlertSkip(int? mrm_id)
        {
            try
            {
                if (mrm_id == null)
                {
                    return string.Empty;
                }
                else
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                        if (roomHdr == null)
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return "คนไข้มีสถานะ Skip มาจากห้อง " + roomHdr.mrm_ename + " ";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ReserveSkipCls", "MessegeAlertSkip", ex, false);
                return string.Empty;
            }
        }
    }
}
