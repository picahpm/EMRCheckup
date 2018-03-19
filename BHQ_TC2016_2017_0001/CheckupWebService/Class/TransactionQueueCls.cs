using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public partial class TransactionQueueCls
    {
        public void SendToRoom(ref trn_patient_regi PatientRegis, int mrm_id, int mvt_id, DateTime dateNow, string username)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    bool checkBefPE = CheckUseBeforePE(mrm_id, mvt_id);
                    trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues.Where(x => x.mrm_id == mrm_id && x.mvt_id == mvt_id).FirstOrDefault();
                    if (PatientQueue == null)
                    {
                        PatientQueue = new trn_patient_queue();
                        PatientQueue.mrm_id = mrm_id;
                        PatientQueue.mvt_id = mvt_id;
                        PatientQueue.tps_create_by = username;
                        PatientQueue.tps_create_date = dateNow;
                        PatientRegis.trn_patient_queues.Add(PatientQueue);
                    }
                    if (checkBefPE)
                    {
                        PatientQueue.tps_ns_status = "WP";
                    }
                    else
                    {
                        PatientQueue.tps_ns_status = "QL";
                    }
                    PatientQueue.tps_status = "NS";
                    PatientQueue.mrd_id = null;
                    PatientQueue.tps_end_date = null;
                    PatientQueue.tps_start_date = null;
                    PatientQueue.tps_update_by = username;
                    PatientQueue.tps_update_date = dateNow;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SendToScreening(ref trn_patient_regi PatientRegis, DateTime dateNow, string username)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int mhs_id = PatientRegis.mhs_id;
                    mst_room_hdr mstRoomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_code == "SC" && x.mhs_id == mhs_id).FirstOrDefault();
                    mst_event mstEvent = cdc.mst_events
                                            .Where(x => x.mvt_code == "SC")
                                            .FirstOrDefault();

                    trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues.Where(x => x.mrm_id == mstRoomHdr.mrm_id && x.mvt_id == mstEvent.mvt_id).FirstOrDefault();

                    if (PatientQueue == null)
                    {
                        PatientQueue = new trn_patient_queue();
                        PatientQueue.mrm_id = mstRoomHdr.mrm_id;
                        PatientQueue.mvt_id = mstEvent.mvt_id;
                        PatientQueue.tps_create_by = username;
                        PatientQueue.tps_create_date = dateNow;
                        PatientRegis.trn_patient_queues.Add(PatientQueue);
                    }
                    PatientQueue.mrd_id = null;
                    PatientQueue.tps_end_date = null;
                    PatientQueue.tps_start_date = null;
                    PatientQueue.tps_status = "NS";
                    PatientQueue.tps_ns_status = "QL";
                    PatientQueue.tps_update_by = username;
                    PatientQueue.tps_update_date = dateNow;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SendToCheckB(ref trn_patient_regi PatientRegis, DateTime dateNow, string username)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int mhs_id = (PatientRegis.tpr_site_use == null || PatientRegis.tpr_site_use == 0) ? (int)PatientRegis.mhs_id : (int)PatientRegis.tpr_site_use;

                    mst_room_hdr mstRoomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_code == "CB" && x.mhs_id == mhs_id).FirstOrDefault();
                    mst_event mstEvent = cdc.mst_events
                                            .Where(x => x.mvt_code == "CB")
                                            .FirstOrDefault();
                    trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues
                                                                 .Where(x => x.mst_room_hdr.mrm_code == "CB" && x.mvt_id == mstEvent.mvt_id)
                                                                 .FirstOrDefault();
                    if (PatientQueue != null)
                    {
                        PatientRegis.tpr_pe_status = "RB";
                        PatientQueue.mrm_id = mstRoomHdr.mrm_id;
                        PatientQueue.tps_status = "NS";
                        PatientQueue.tps_ns_status = "QL";
                        PatientQueue.mrd_id = null;
                        PatientQueue.tps_end_date = null;
                        PatientQueue.tps_start_date = null;
                        PatientQueue.tps_update_by = username;
                        PatientQueue.tps_update_date = dateNow;
                    }
                    else
                    {
                        SendToRoom(ref PatientRegis, mstRoomHdr.mrm_id, mstEvent.mvt_id, dateNow, username);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SendToCheckC(ref trn_patient_regi PatientRegis, DateTime dateNow, string username, bool andSendBook = false)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int mhs_id = (PatientRegis.tpr_site_use == null || PatientRegis.tpr_site_use == 0) ? (int)PatientRegis.mhs_id : (int)PatientRegis.tpr_site_use;
                    mst_room_hdr mstRoomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_code == "CC" && x.mhs_id == mhs_id).FirstOrDefault();
                    mst_event mstEvent = cdc.mst_events
                                            .Where(x => x.mvt_code == "CC")
                                            .FirstOrDefault();
                    trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues.Where(x => x.mrm_id == mstRoomHdr.mrm_id && x.mvt_id == mstEvent.mvt_id).FirstOrDefault();

                    if (PatientQueue == null)
                    {
                        PatientQueue = new trn_patient_queue();
                        PatientQueue.mrm_id = mstRoomHdr.mrm_id;
                        PatientQueue.mvt_id = mstEvent.mvt_id;
                        PatientQueue.tps_create_by = username;
                        PatientQueue.tps_create_date = dateNow;
                        PatientRegis.trn_patient_queues.Add(PatientQueue);
                    }
                    PatientQueue.mrd_id = null;
                    PatientQueue.tps_end_date = null;
                    PatientQueue.tps_start_date = null;
                    if (andSendBook)
                    {
                        PatientQueue.tps_status = "ED";
                    }
                    else
                    {
                        PatientQueue.tps_status = "WK";
                    }
                    PatientQueue.tps_ns_status = null;
                    PatientQueue.tps_update_by = username;
                    PatientQueue.tps_update_date = dateNow;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendToPE(ref trn_patient_regi PatientRegis, DateTime dateNow, string username)  // use for PE Before Check B
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int mhs_id = (PatientRegis.tpr_site_use == null || PatientRegis.tpr_site_use == 0) ? (int)PatientRegis.mhs_id : (int)PatientRegis.tpr_site_use;

                    mst_room_hdr mstRoomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_code == "DC" && x.mhs_id == mhs_id).FirstOrDefault();
                    mst_event mstEvent = cdc.mst_events
                                            .Where(x => x.mvt_code == "PE")
                                            .FirstOrDefault();
                    trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues
                                                                 .Where(x => x.mrm_id == mstRoomHdr.mrm_id && x.mvt_id == mstEvent.mvt_id)
                                                                 .FirstOrDefault();
                    SendToRoom(ref PatientRegis, mstRoomHdr.mrm_id, mstEvent.mvt_id, dateNow, username);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EndQueue(ref trn_patient_regi PatientRegis, int tps_id, DateTime dateNow, string username)
        {
            try
            {
                List<trn_patient_queue> tps = PatientRegis.trn_patient_queues
                                                 .Where(x => x.tps_id == tps_id).ToList();
                tps.ForEach(x =>
                {
                    x.tps_send_by = username;
                    x.tps_end_date = dateNow;
                    x.tps_status = "ED";
                    x.tps_ns_status = null;
                    x.tps_update_by = username;
                    x.tps_update_date = dateNow;
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReserveCurrentQueue(ref trn_patient_regi PatientRegis, int tps_id)
        {
            trn_patient_skip_queue PatientSkipQueue = PatientRegis.trn_patient_queues
                                                                  .Where(x => x.tps_id == tps_id &&
                                                                              x.mst_room_hdr.mrm_skip_reserve == true)
                                                                  .Select(x => new trn_patient_skip_queue
                                                                  {
                                                                      tps_id = x.tps_id,
                                                                      mrm_id = x.mrm_id,
                                                                      mrd_id = x.mrd_id,
                                                                      mvt_id = x.mvt_id,
                                                                      tps_bm_seq = x.tps_bm_seq,
                                                                      tps_call_by = x.tps_call_by,
                                                                      tps_call_date = x.tps_call_date,
                                                                      tps_call_status = x.tps_call_status,
                                                                      tps_cancel_by = x.tps_cancel_by,
                                                                      tps_cancel_date = x.tps_cancel_date,
                                                                      tps_cancel_other = x.tps_cancel_other,
                                                                      tps_cancel_remark = x.tps_cancel_remark,
                                                                      tps_create_by = x.tps_create_by,
                                                                      tps_create_date = x.tps_create_date,
                                                                      tps_end_date = x.tps_end_date,
                                                                      tps_hold_by = x.tps_hold_by,
                                                                      tps_hold_date = x.tps_hold_date,
                                                                      tps_lab_date = x.tps_lab_date,
                                                                      tps_ns_status = x.tps_ns_status,
                                                                      tps_reserve = x.tps_reserve,
                                                                      tps_send_by = x.tps_send_by,
                                                                      tps_start_date = x.tps_start_date,
                                                                      tps_status = x.tps_status,
                                                                      tps_update_by = x.tps_update_by,
                                                                      tps_update_date = x.tps_update_date,
                                                                      tpsq_active = null
                                                                  }).FirstOrDefault();
            if (PatientSkipQueue != null)
            {
                PatientRegis.trn_patient_skip_queues.Add(PatientSkipQueue);
            }
        }

        public bool CheckUseBeforePE(int mrm_id, int mvt_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_id == mvt_id).FirstOrDefault();
                    if (mstEvent.mvt_code != "PE")
                    {
                        return false;
                    }
                    else
                    {
                        mst_room_hdr mstRoomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                        if (mstRoomHdr.mrm_code != "DC")
                        {
                            return false;
                        }
                        else
                        {
                            if (mstRoomHdr.mst_hpc_site.mhs_use_before_pe == null || mstRoomHdr.mst_hpc_site.mhs_use_before_pe == false)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("get_lab_result.Class.TransactionQueueCls", "CheckUseBeforePE(int mrm_id, int mvt_id)", ex.Message);
                return false;
            }
        }
    }
}