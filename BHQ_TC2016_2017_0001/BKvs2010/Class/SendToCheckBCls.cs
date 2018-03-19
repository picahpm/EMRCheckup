using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Data.Common;

namespace BKvs2010.Class
{
    class SendToCheckBCls
    {
        public StatusTransaction SendToCheckBOnWaiting(int tpr_id, int mrm_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction tran = cdc.Connection.BeginTransaction();
                        cdc.Transaction = tran;

                        DateTime dateNow = Program.GetServerDateTime();
                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                        if (tps != null && tps.tps_status == "NS" && tps.tps_ns_status == "QL")
                        {
                            patient_regisToCheckB(ref tpr);
                            cdc.trn_patient_queues.DeleteOnSubmit(tps); // delete tps_id ห้องปัจจุบัน
                            queueToCheckB(ref tpr, dateNow);
                            cdc.SubmitChanges();
                            cdc.Transaction.Commit();
                            return StatusTransaction.True;
                        }
                        else
                        {
                            return StatusTransaction.NoProcess;
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendToCheckBCls", "SendToCheckBOnWaiting", ex, false);
                        return StatusTransaction.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendToCheckBCls", "SendToCheckBOnWaiting", ex, false);
                return StatusTransaction.Error;
            }
        }

        public bool SendToCheckBOnStation(int tpr_id, int tps_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

                    patient_regisToCheckB(ref tpr);
                    queueToCheckB(ref tpr, dateNow);

                    trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault(); // update status ห้องปัจจุบัน
                    tps.tps_send_by = Program.CurrentUser.mut_username;
                    tps.tps_end_date = dateNow;
                    tps.tps_status = "ED";
                    tps.tps_ns_status = null;
                    tps.tps_update_by = Program.CurrentUser.mut_username;
                    tps.tps_update_date = dateNow;
                    cdc.SubmitChanges();
                }
                return true;
            }
            catch
            {

            }
            return false;
        }

        private void patient_regisToCheckB(ref trn_patient_regi tpr)
        {
            tpr.tpr_pe_status = "RB";
        }

        private void queueToCheckB(ref trn_patient_regi tpr, DateTime dateNow)
        {
            // update tps checkpointB "NS", "QL", start_date = null, end_date = null, update_date = dateNow

            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            int mrm_checkB = mst.GetMstRoomHdr("CB", tpr.mhs_id, tpr.tpr_site_use).mrm_id;
            int mvt_checkB = mst.GetMstEvent("CB").mvt_id;
            trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mvt_id == mvt_checkB).FirstOrDefault();
            if (tps == null) // case imposible
            {
                tps = new trn_patient_queue()
                {
                    mrm_id = mrm_checkB,
                    mvt_id = mvt_checkB,
                    tps_create_by = Program.CurrentUser.mut_username,
                    tps_create_date = dateNow
                };
                tpr.trn_patient_queues.Add(tps);
            }
            tps.tps_status = "NS";
            tps.tps_ns_status = "QL";
            tps.tps_update_by = Program.CurrentUser.mut_username;
            tps.tps_update_date = dateNow;

            tps.mrd_id = null;
            tps.tps_bm_seq = null;
            tps.tps_call_by = null;
            tps.tps_call_date = null;
            tps.tps_call_status = null; // Hold = "HD"
            tps.tps_cancel_by = null;
            tps.tps_cancel_date = null;
            tps.tps_cancel_other = null;
            tps.tps_cancel_remark = null;
            tps.tps_end_date = null;
            tps.tps_hold_by = null;
            tps.tps_hold_date = null;
            tps.tps_lab_date = null;
            tps.tps_send_by = null;
            tps.tps_start_date = null;
        }
    }
}
