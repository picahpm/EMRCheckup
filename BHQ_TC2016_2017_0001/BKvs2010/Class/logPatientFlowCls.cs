using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using DBCheckup;

namespace BKvs2010.Class
{
    class logPatientFlowCls
    {
        public enum sendType
        {
            Regis,
            CallQueue,
            CallQueueManual,
            SendAuto,
            SendAutoOnPendingCheckB,
            SendManualOnPendingCheckB,
            SendManual,
            SendCheckB,
            SendScreening,
            SendPE,
            SkipAuto,
            SkipManual,
            SkipOnWaitingManual,
            PendingAuto,
            PendingManual,
            PendingOnWaitingManual,
            SendUltraSoundBefore,
            SendUltraSoundAfter
        }
        public logPatientFlowCls(sendType type, int tpr_id, int tps_id, int mhs_id, string mrd_ename, string mut_username, DateTime? StartProcressTime = null)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_queue newPatientQueue = cdc.trn_patient_queues.Where(x => x.tpr_id == tpr_id &&
                                                                                        ((x.tps_status == "NS" && x.tps_ns_status == "QL") ||
                                                                                         (x.tps_status == "NS" && x.tps_ns_status == "WL") ||
                                                                                          x.tps_status == "WK"))
                                                                            .OrderByDescending(x => x.tps_id).FirstOrDefault();
                    int? nextTpsID = null;
                    if (newPatientQueue != null)
                    {
                        nextTpsID = newPatientQueue.tps_id;
                    }
                    int countFlow = cdc.log_patient_flows.Where(x => x.tpr_id == tpr_id).Count();

                    log_patient_flow log = new log_patient_flow()
                    {
                        log_time = dateNow,
                        mhs_id = mhs_id,
                        mrd_ename = mrd_ename,
                        mut_username = mut_username,
                        send_type = type.ToString(),
                        seq_id = countFlow + 1,
                        tpr_id = tpr_id,
                        tps_id = tps_id,
                        next_tps_id = nextTpsID,
                        process_time = StartProcressTime == null ? 0 : Convert.ToInt64((DateTime.Now - StartProcressTime.Value).TotalMilliseconds)
                        // nextTpsID
                    };
                    cdc.log_patient_flows.InsertOnSubmit(log);
                    cdc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("logPatientFlowCls", "logPatientFlowCls", ex, false);
            }
        }
    }
}
