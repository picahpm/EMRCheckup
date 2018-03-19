using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.QueueClass
{
    public partial class SendAutoCls
    {
        public ResultSendQueue SendAuto(int tps_id, int mhs_id, mst_user_type user, ref string msgAlert, ref bool isPopup, int? current_site = null)
        {
            try
            {
                List<int?> complete_mvt_id = new List<int?>();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue patientQueue = cdc.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
                    trn_patient_regi patientRegis = patientQueue.trn_patient_regi;
                    patientRegis.tpr_site_use = mhs_id;
                    cdc.SubmitChanges();
                }
                return SendAuto(tps_id, user, ref msgAlert, ref isPopup, current_site);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SendAuto(int tps_id, int mhs_id, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        public ResultSendQueue SendAuto(int tps_id, mst_user_type user, ref string msgAlert, ref bool isPopup, int? current_site = null)
        {
            try
            {
                List<int> complete_mvt_id = new List<int>();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue patientQueue = cdc.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
                    if (patientQueue.mrd_id != null)
                    {
                        int mrd_id = (int)patientQueue.mrd_id;
                        mst_room_dtl roomDtl = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                        complete_mvt_id = cdc.mst_room_events
                                             .Where(x => x.mrm_id == roomDtl.mrm_id)
                                             .Select(x => x.mvt_id).ToList();
                    }
                    else
                    {
                        complete_mvt_id = cdc.mst_room_events
                                             .Where(x => x.mrm_id == patientQueue.mrm_id)
                                             .Select(x => x.mvt_id).ToList();
                    }
                }
                if (complete_mvt_id.Count() == 0)
                {
                    isPopup = false;
                    msgAlert = "กรุณากดส่งอีกครั้ง";
                    return ResultSendQueue.Error;
                }
                else
                {
                    return SendAuto(tps_id, complete_mvt_id, user, ref msgAlert, ref isPopup, current_site);
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "(int tps_id, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        public ResultSendQueue SendAuto(int tps_id, List<int> complete_mvt_id, mst_user_type user, ref string msgAlert, ref bool isPopup, int? current_site = null)
        {
            try
            {
                string username = user == null ? "" : user.mut_username;
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        trn_patient_queue PatientQueue = cdc.trn_patient_queues
                                                            .Where(x => x.tps_id == tps_id)
                                                            .FirstOrDefault();

                        trn_patient_regi PatientRegis = PatientQueue.trn_patient_regi;

                        int tpr_id = PatientRegis.tpr_id;
                        string queue_no = PatientRegis.tpr_queue_no;
                        int mrm_id = (int)PatientQueue.mrm_id;
                        int? mrd_id = PatientQueue.mrd_id;
                        int? mhs_id = PatientRegis.tpr_site_use == null ? PatientRegis.mhs_id : PatientRegis.tpr_site_use;
                        int _current_site = current_site != null ? (int)current_site : PatientQueue.mst_room_hdr.mhs_id;

                        List<int> room_event = new List<int>();

                        mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                        if (PatientQueue.mrd_id != null)
                        {
                            mst_room_dtl roomDtl = roomHdr.mst_room_dtls.Where(x => x.mrd_id == PatientQueue.mrd_id).FirstOrDefault();
                            if (roomDtl != null)
                            {
                                if (roomDtl.mst_room_hdr.mrm_code == "EM")
                                {
                                    if (roomDtl.mrd_type == 'D')
                                    {
                                        mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "EM" && x.mvt_status == 'A').FirstOrDefault();
                                        room_event.Add(mstEvent.mvt_id);
                                    }
                                    else
                                    {
                                        mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "EN" && x.mvt_status == 'A').FirstOrDefault();
                                        room_event.Add(mstEvent.mvt_id);
                                    }
                                }
                                else
                                {
                                    room_event = cdc.mst_room_events
                                                    .Where(x => x.mrm_id == mrm_id)
                                                    .Select(x => x.mvt_id).ToList();
                                }
                            }
                        }
                        else
                        {
                            room_event = cdc.mst_room_events
                                            .Where(x => x.mrm_id == mrm_id)
                                            .Select(x => x.mvt_id).ToList();
                        }

                        DateTime dateNow = Program.GetServerDateTime();
                        int? mze_id = null;
                        if (roomHdr.mrm_code != "CB")
                        {
                            mze_id = roomHdr.mze_id;
                        }

                        int? next_mrm_id = null;
                        int? next_mvt_id = null;

                        List<sp_get_patient_roomResult> patientRoom = cdc.sp_get_patient_room(tpr_id, mhs_id).ToList();
                        List<sp_get_patient_roomResult> patientEventComplete = patientRoom.Where(x => complete_mvt_id.Contains(x.mvt_id)).ToList();
                        foreach (sp_get_patient_roomResult pr in patientEventComplete)
                        {
                            patientRoom.Remove(pr);
                        }
                        List<inx_sp_get_patient_roomResult> inxPatientRoom = new List<inx_sp_get_patient_roomResult>();
                        ResultSendQueue result = filterPatientRoom(patientRoom, ref inxPatientRoom, room_event, _current_site, mze_id, 0);

                        if (result == ResultSendQueue.Error)
                        {
                            isPopup = false;
                            msgAlert = "กรุณากดส่งอีกครั้ง";
                            return ResultSendQueue.Error;
                        }
                        else if (result == ResultSendQueue.OldRoom)
                        {
                            isPopup = false;
                            msgAlert = "ไม่สามารถส่ง Queue ได้ เนื่องจาก Order Item ของคนไข้ที่เหลืออยู่มีแค่ใน Station นี้เท่านั้น กรุณาตรวจให้ครบ หรือ Hold Queue.";
                            return ResultSendQueue.OldRoom;
                        }
                        else
                        {
                            new TransactionPlanCls().EndPlan(ref PatientRegis, complete_mvt_id);
                            new TransactionQueueCls().EndQueue(ref PatientRegis, tps_id, dateNow, username);
                            if (result == ResultSendQueue.SendCheckC)
                            {
                                new TransactionQueueCls().SendToCheckC(ref PatientRegis, dateNow, username);
                                try
                                {
                                    using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                                    {
                                        ws.retrieveVitalSignBackground(tpr_id);
                                        ws.getCheckUpLabResultBackground(tpr_id);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Program.MessageError("SendAutoCls", "WS->retrieveBackground", ex, false);
                                }
                                var roomChkC = cdc.mst_room_hdrs
                                                  .Where(x => x.mrm_code == "CC" && x.mhs_id == PatientRegis.mhs_id)
                                                  .Select(x => new
                                                  {
                                                      x.mrm_ename,
                                                      x.mst_hpc_site.mhs_ename,
                                                      x.mst_zone.mze_ename
                                                  }).FirstOrDefault();
                                if (roomChkC == null)
                                {
                                    msgAlert = "Send Queue no. " + queue_no + " To Check Point C.";
                                }
                                else
                                {
                                    msgAlert = string.Format(Program.MsgSend, roomChkC.mrm_ename, roomChkC.mhs_ename, roomChkC.mze_ename, queue_no);
                                }
                                isPopup = false;
                            }
                            else if (result == ResultSendQueue.SendCheckB)
                            {
                                new TransactionQueueCls().SendToCheckB(ref PatientRegis, dateNow, username);
                                var chkB = PatientRegis.trn_patient_queues
                                                       .Where(x => x.tps_status == "NS" && x.tps_ns_status == "QL")
                                                       .Select(x => new
                                                       {
                                                           x.mrm_id,
                                                       }).FirstOrDefault();
                                var room = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                                msgAlert = string.Format(Program.MsgSend, room.mrm_ename, room.mst_hpc_site.mhs_ename, room.mst_zone.mze_ename, queue_no);
                                isPopup = false;
                            }
                            else
                            {
                                bool patient_eye_dropper = PatientRegis.trn_eye_exam_hdrs.Count() != 0 ?
                                                           (PatientRegis.trn_eye_exam_hdrs.FirstOrDefault().teh_eyedropper == null ?
                                                            false :
                                                            (bool)PatientRegis.trn_eye_exam_hdrs.FirstOrDefault().teh_eyedropper) :
                                                            false;

                                bool checkEyeDoc = CheckEyeDoctor(tpr_id, mrd_id, patient_eye_dropper, inxPatientRoom, ref next_mrm_id, ref next_mvt_id);
                                if (checkEyeDoc && next_mrm_id != null && next_mvt_id != null)
                                {
                                    new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                    var nextRoom = inxPatientRoom.Where(x => x.mrm_id == next_mrm_id && x.mvt_id == next_mvt_id)
                                                         .Select(x => new
                                                         {
                                                             x.mrm_ename,
                                                             x.mhs_ename,
                                                             x.mze_ename
                                                         }).FirstOrDefault();
                                    msgAlert = string.Format(Program.MsgSend, nextRoom.mrm_ename, nextRoom.mhs_ename, nextRoom.mze_ename, queue_no);
                                    isPopup = false;
                                }
                                else
                                {
                                    string doctor_code = (PatientRegis.tpr_req_doc_code == null || PatientRegis.tpr_req_doc_code == "") ? PatientRegis.tpr_pe_doc_code : PatientRegis.tpr_req_doc_code;
                                    bool checkPE = CheckPE(tpr_id, doctor_code, inxPatientRoom, dateNow, ref next_mrm_id, ref next_mvt_id);
                                    if (checkPE)
                                    {
                                        new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                        var nextRoom = inxPatientRoom.Where(x => x.mrm_id == next_mrm_id && x.mvt_id == next_mvt_id)
                                                             .Select(x => new
                                                             {
                                                                 x.mrm_ename,
                                                                 x.mhs_ename,
                                                                 x.mze_ename
                                                             }).FirstOrDefault();
                                        msgAlert = string.Format(Program.MsgSend, nextRoom.mrm_ename, nextRoom.mhs_ename, nextRoom.mze_ename, queue_no);
                                        isPopup = false;
                                    }
                                    else
                                    {
                                        inx_sp_get_patient_roomResult firstPatientRoom = inxPatientRoom.OrderBy(x => x.priority).FirstOrDefault();
                                        next_mrm_id = firstPatientRoom.mrm_id;
                                        next_mvt_id = firstPatientRoom.mvt_id;
                                        new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                        msgAlert = string.Format(Program.MsgSend, firstPatientRoom.mrm_ename, firstPatientRoom.mhs_ename, firstPatientRoom.mze_ename, queue_no);
                                        isPopup = false;
                                    }
                                }
                            }
                            cdc.SubmitChanges();
                            if (inxPatientRoom.Count() > 0)
                            {
                                trn_patient_queue next_queue = PatientRegis.trn_patient_queues
                                                                           .Where(x => (x.tps_status == "NS" && x.tps_ns_status == "QL") ||
                                                                                       (x.tps_status == "NS" && x.tps_ns_status == "WP")).FirstOrDefault();
                                if (next_queue != null)
                                {
                                    List<log_send_auto> log = GetLogSendAuto(inxPatientRoom, next_queue.tps_id, tps_id, queue_no);
                                    cdc.log_send_autos.InsertAllOnSubmit(log);
                                }
                            }
                            cdc.SubmitChanges();
                            cdc.Transaction.Commit();
                            return ResultSendQueue.SendComplete;
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendAutoCls", "SendAuto(int tpr_id, int mrd_id, List<int?> complete_mvt_id, ref string msgAlert, ref bool isPopup)", ex, false);
                        isPopup = false;
                        msgAlert = "กรุณากดส่งอีกครั้ง";
                        return ResultSendQueue.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SendAuto(int tpr_id, int mrd_id, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        private ResultSendQueue filterPatientRoom(List<sp_get_patient_roomResult> input,
                                                  ref List<inx_sp_get_patient_roomResult> output,
                                                  List<int> room_mvt_id,
                                                  int current_site,
                                                  int? mze_id,
                                                  int waiting_time_out_zone)
        {
            try
            {
                List<sp_get_patient_roomResult> patientRoomWithOutCancel = input.Where(x => x.tpl_status != "C").ToList();
                if (patientRoomWithOutCancel.Count == 0)
                {
                    output = new List<inx_sp_get_patient_roomResult>();
                    return ResultSendQueue.SendCheckC;
                }
                else
                {
                    List<sp_get_patient_roomResult> patientRoomWithOutRoomEvent = patientRoomWithOutCancel.Where(x => !room_mvt_id.Contains(x.mvt_id)).ToList();
                    if (patientRoomWithOutRoomEvent.Count() == 0)
                    {
                        output = new List<inx_sp_get_patient_roomResult>();
                        return ResultSendQueue.OldRoom;
                    }
                    else
                    {
                        List<sp_get_patient_roomResult> listInput = input.Select(x => new sp_get_patient_roomResult
                                                                    {
                                                                        count_doctor_ns = x.count_doctor_ns,
                                                                        count_doctor_wk = x.count_doctor_wk,
                                                                        login_flag = x.login_flag,
                                                                        mhs_code = x.mhs_code,
                                                                        mhs_ename = x.mhs_ename,
                                                                        mhs_id = x.mhs_id,
                                                                        mrm_code = x.mrm_code,
                                                                        mrm_ename = x.mrm_ename,
                                                                        mrm_id = x.mrm_id,
                                                                        mrm_seq_room = x.mrm_seq_room,
                                                                        mvt_code = x.mvt_code,
                                                                        mvt_id = x.mvt_id,
                                                                        mvt_type_cate = x.mvt_type_cate,
                                                                        mze_code = x.mze_code,
                                                                        mze_ename = x.mze_ename,
                                                                        mze_id = x.mze_id,
                                                                        patient_vip = x.patient_vip,
                                                                        send_type = x.send_type,
                                                                        site_rm = x.site_rm,
                                                                        skip_seq = x.skip_seq,
                                                                        skip_type = x.skip_type,
                                                                        tpl_status = x.tpl_status,
                                                                        tpr_id = x.tpr_id,
                                                                        waiting_person = x.waiting_person,
                                                                        waiting_time = x.waiting_time
                                                                    }).ToList();
                        CheckRoomBef(ref listInput);
                        CheckEventCate(ref listInput);
                        List<sp_get_patient_roomResult> listInputFilter = listInput.Where(x => x.tpl_status != "C" && !room_mvt_id.Contains(x.mvt_id)).ToList();
                        output = insertSeqPatientRoom(listInputFilter, current_site, mze_id, waiting_time_out_zone);
                        return ResultSendQueue.SendComplete;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "filterPatientRoom", ex, false);
                return ResultSendQueue.Error;
            }
        }

        public ResultSendQueue SkipSendAuto(int tps_id, List<int> complete_mvt_id, string username, ref string msgAlert, ref bool isPopup)
        {
            try
            {
                List<int> skip_mvt_id = new List<int>();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue patientQueue = cdc.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
                    if (patientQueue.mrd_id != null)
                    {
                        int mrd_id = (int)patientQueue.mrd_id;
                        mst_room_dtl roomDtl = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                        skip_mvt_id = cdc.mst_room_events
                                         .Where(x => x.mrm_id == roomDtl.mrm_id)
                                         .Select(x => x.mvt_id).ToList();
                    }
                    else
                    {
                        skip_mvt_id = cdc.mst_room_events
                                         .Where(x => x.mrm_id == patientQueue.mrm_id)
                                         .Select(x => x.mvt_id).ToList();
                    }
                }
                if (skip_mvt_id.Count() == 0)
                {
                    isPopup = false;
                    msgAlert = "กรุณากดส่งอีกครั้ง";
                    return ResultSendQueue.Error;
                }
                else
                {
                    return SkipSendAuto(tps_id, new List<int>(), skip_mvt_id, username, ref msgAlert, ref isPopup);
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SkipSendAuto(int tps_id, string username, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        public ResultSendQueue SkipSendAuto(int tps_id, List<int> complete_mvt_id, List<int> skip_mvt_id, string username, ref string msgAlert, ref bool isPopup)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        trn_patient_queue PatientQueue = cdc.trn_patient_queues
                                                            .Where(x => x.tps_id == tps_id)
                                                            .FirstOrDefault();

                        trn_patient_regi PatientRegis = PatientQueue.trn_patient_regi;

                        int tpr_id = PatientRegis.tpr_id;
                        string queue_no = PatientRegis.tpr_queue_no;
                        int mrm_id = (int)PatientQueue.mrm_id;
                        int? mrd_id = PatientQueue.mrd_id;
                        int? mhs_id = PatientRegis.tpr_site_use == null ? PatientRegis.mhs_id : PatientRegis.tpr_site_use;
                        int current_site = PatientQueue.mst_room_hdr.mhs_id;

                        List<int> room_event = new List<int>();

                        mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                        if (PatientQueue.mrd_id != null)
                        {
                            mst_room_dtl roomDtl = roomHdr.mst_room_dtls.Where(x => x.mrd_id == PatientQueue.mrd_id).FirstOrDefault();
                            if (roomDtl.mst_room_hdr.mrm_code == "EM")
                            {
                                if (roomDtl.mrd_type == 'D')
                                {
                                    mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "EM" && x.mvt_status == 'A').FirstOrDefault();
                                    room_event.Add(mstEvent.mvt_id);
                                }
                                else
                                {
                                    room_event.AddRange(roomHdr.mst_room_events.Select(x => x.mvt_id));
                                }
                            }
                            else
                            {
                                room_event = cdc.mst_room_events
                                                .Where(x => x.mrm_id == mrm_id)
                                                .Select(x => x.mvt_id).ToList();
                            }
                        }
                        else
                        {
                            room_event = cdc.mst_room_events
                                            .Where(x => x.mrm_id == mrm_id)
                                            .Select(x => x.mvt_id).ToList();
                        }

                        DateTime dateNow = Program.GetServerDateTime();

                        int? mze_id = roomHdr.mze_id;

                        int? next_mrm_id = null;
                        int? next_mvt_id = null;

                        List<sp_get_patient_roomResult> patientRoom = cdc.sp_get_patient_room(tpr_id, mhs_id).ToList();
                        List<sp_get_patient_roomResult> patientEventComplete = patientRoom.Where(x => complete_mvt_id.Contains(x.mvt_id)).ToList();
                        foreach (sp_get_patient_roomResult pr in patientEventComplete)
                        {
                            patientRoom.Remove(pr);
                        }
                        List<inx_sp_get_patient_roomResult> inxPatientRoom = new List<inx_sp_get_patient_roomResult>();
                        ResultSendQueue result = filterPatientRoomOnSkip(patientRoom, ref inxPatientRoom, skip_mvt_id, current_site, mze_id, 0);

                        if (result == ResultSendQueue.Error)
                        {
                            isPopup = false;
                            msgAlert = "กรุณากดส่งอีกครั้ง";
                            return ResultSendQueue.Error;
                        }
                        else if (result == ResultSendQueue.OldRoom)
                        {
                            isPopup = false;
                            msgAlert = "";
                            return ResultSendQueue.OldRoom;
                        }
                        else
                        {
                            new TransactionPlanCls().EndPlan(ref PatientRegis, complete_mvt_id);
                            if (roomHdr.mrm_code == "EM")
                            {
                                new TransactionPlanCls().SkipPlan(ref PatientRegis, room_event);
                            }
                            else
                            {
                                new TransactionPlanCls().SkipPlan(ref PatientRegis, skip_mvt_id);
                            }
                            new TransactionQueueCls().ReserveCurrentQueue(ref PatientRegis, tps_id);
                            cdc.trn_patient_queues.DeleteOnSubmit(PatientQueue);
                            if (result == ResultSendQueue.SendCheckC)
                            {
                                new TransactionQueueCls().SendToCheckC(ref PatientRegis, dateNow, username);
                                isPopup = false;
                            }
                            else if (result == ResultSendQueue.SendCheckB)
                            {
                                new TransactionQueueCls().SendToCheckB(ref PatientRegis, dateNow, username);
                                isPopup = false;
                            }
                            else
                            {
                                string doctor_code = (PatientRegis.tpr_req_doc_code == null || PatientRegis.tpr_req_doc_code == "") ? PatientRegis.tpr_pe_doc_code : PatientRegis.tpr_req_doc_code;
                                bool checkPE = CheckPE(tpr_id, doctor_code, inxPatientRoom, dateNow, ref next_mrm_id, ref next_mvt_id);
                                if (checkPE)
                                {
                                    new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                    isPopup = false;
                                }
                                else
                                {
                                    inx_sp_get_patient_roomResult firstPatientRoom = inxPatientRoom.OrderBy(x => x.priority).FirstOrDefault();
                                    next_mrm_id = firstPatientRoom.mrm_id;
                                    next_mvt_id = firstPatientRoom.mvt_id;
                                    new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                    isPopup = false;
                                }
                            }

                            inx_sp_get_patient_roomResult nextPatientRoom = inxPatientRoom.Where(x => x.mrm_id == next_mrm_id && x.mvt_id == next_mvt_id).FirstOrDefault();
                            msgAlert = string.Format(Program.MsgSend, nextPatientRoom.mrm_ename, nextPatientRoom.mhs_ename, nextPatientRoom.mze_ename, queue_no);
                            cdc.SubmitChanges();
                            if (inxPatientRoom.Count() > 0)
                            {
                                trn_patient_queue next_queue = PatientRegis.trn_patient_queues.Where(x => x.tps_status == "NS" && x.tps_ns_status == "QL").FirstOrDefault();
                                if (next_queue != null)
                                {
                                    List<log_send_auto> log = GetLogSendAuto(inxPatientRoom, next_queue.tps_id, tps_id, queue_no);
                                    cdc.log_send_autos.InsertAllOnSubmit(log);
                                }
                            }
                            cdc.SubmitChanges();
                            cdc.Transaction.Commit();
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendAutoCls", "SkipSendAuto(int tps_id, List<int> skip_mvt_id, string username, ref string msgAlert, ref bool isPopup)", ex, false);
                        isPopup = false;
                        msgAlert = "กรุณากดส่งอีกครั้ง";
                        return ResultSendQueue.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SkipSendAuto(int tps_id, List<int> skip_mvt_id, string username, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        public ResultSendQueue SkipOnWaitingSendAuto(int tpr_id, int tps_id, List<int> skip_mvt_id, string username, ref string msgAlert, ref bool isPopup)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        string queue_no = PatientRegis.tpr_queue_no;

                        trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues
                                                                     .Where(x => x.tps_id == tps_id)
                                                                     .FirstOrDefault();

                        int current_site = PatientQueue.mst_room_hdr.mhs_id;

                        if (PatientQueue == null)
                        {
                            msgAlert = "ไม่สามารถ Skip Q." + PatientRegis.tpr_queue_no + " ได้ เนื่องจากสถานะคนไข้ไม่ได้อยู่ใน Waiting List.";
                            isPopup = true;
                            return ResultSendQueue.OldRoom;
                        }
                        else
                        {
                            if (PatientQueue.tps_status != "NS" || PatientQueue.tps_ns_status != "QL")
                            {
                                msgAlert = "ไม่สามารถ Skip Q." + PatientRegis.tpr_queue_no + " ได้ เนื่องจากสถานะคนไข้ไม่ได้อยู่ใน Waiting List.";
                                isPopup = true;
                                return ResultSendQueue.OldRoom;
                            }
                            else
                            {
                                int mrm_id = (int)PatientQueue.mrm_id;
                                int? mrd_id = PatientQueue.mrd_id;
                                int? mhs_id = PatientRegis.tpr_site_use == null ? PatientRegis.mhs_id : PatientRegis.tpr_site_use;

                                List<int> room_event = new List<int>();

                                mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                                if (PatientQueue.mrd_id != null)
                                {
                                    mst_room_dtl roomDtl = roomHdr.mst_room_dtls.Where(x => x.mrd_id == PatientQueue.mrd_id).FirstOrDefault();
                                    if (roomDtl.mst_room_hdr.mrm_code == "EM")
                                    {
                                        if (roomDtl.mrd_type == 'D')
                                        {
                                            mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "EM" && x.mvt_status == 'A').FirstOrDefault();
                                            room_event.Add(mstEvent.mvt_id);
                                        }
                                        else
                                        {
                                            room_event.AddRange(roomHdr.mst_room_events.Select(x => x.mvt_id));
                                        }
                                    }
                                    else
                                    {
                                        room_event = cdc.mst_room_events
                                                        .Where(x => x.mrm_id == mrm_id)
                                                        .Select(x => x.mvt_id).ToList();
                                    }
                                }
                                else
                                {
                                    room_event = cdc.mst_room_events
                                                    .Where(x => x.mrm_id == mrm_id)
                                                    .Select(x => x.mvt_id).ToList();
                                }

                                DateTime dateNow = Program.GetServerDateTime();

                                int? mze_id = null;
                                mze_id = roomHdr.mze_id;

                                int? next_mrm_id = null;
                                int? next_mvt_id = null;

                                List<sp_get_patient_roomResult> patientRoom = cdc.sp_get_patient_room(tpr_id, mhs_id).ToList();
                                List<inx_sp_get_patient_roomResult> inxPatientRoom = new List<inx_sp_get_patient_roomResult>();
                                ResultSendQueue result = filterPatientRoomOnSkip(patientRoom, ref inxPatientRoom, room_event, current_site, mze_id, 0);

                                if (result == ResultSendQueue.Error)
                                {
                                    isPopup = false;
                                    msgAlert = "กรุณากดส่งอีกครั้ง";
                                    return ResultSendQueue.Error;
                                }
                                else if (result == ResultSendQueue.OldRoom)
                                {
                                    isPopup = false;
                                    msgAlert = "";
                                    return ResultSendQueue.OldRoom;
                                }
                                else
                                {
                                    if (roomHdr.mrm_code == "EM")
                                    {
                                        new TransactionPlanCls().SkipPlan(ref PatientRegis, room_event);
                                    }
                                    else
                                    {
                                        new TransactionPlanCls().SkipPlan(ref PatientRegis, skip_mvt_id);
                                    }
                                    new TransactionQueueCls().ReserveCurrentQueue(ref PatientRegis, tps_id);
                                    cdc.trn_patient_queues.DeleteOnSubmit(PatientQueue);

                                    if (result == ResultSendQueue.SendCheckC)
                                    {
                                        new TransactionQueueCls().SendToCheckC(ref PatientRegis, dateNow, username);
                                        isPopup = false;
                                    }
                                    else if (result == ResultSendQueue.SendCheckB)
                                    {
                                        new TransactionQueueCls().SendToCheckB(ref PatientRegis, dateNow, username);
                                        isPopup = false;
                                    }
                                    else
                                    {
                                        string doctor_code = (PatientRegis.tpr_req_doc_code == null || PatientRegis.tpr_req_doc_code == "") ? PatientRegis.tpr_pe_doc_code : PatientRegis.tpr_req_doc_code;
                                        bool checkPE = CheckPE(tpr_id, doctor_code, inxPatientRoom, dateNow, ref next_mrm_id, ref next_mvt_id);
                                        if (checkPE)
                                        {
                                            new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                            isPopup = false;
                                        }
                                        else
                                        {
                                            inx_sp_get_patient_roomResult firstPatientRoom = inxPatientRoom.OrderBy(x => x.priority).FirstOrDefault();
                                            next_mrm_id = firstPatientRoom.mrm_id;
                                            next_mvt_id = firstPatientRoom.mvt_id;
                                            new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                            isPopup = false;
                                        }
                                    }

                                    inx_sp_get_patient_roomResult nextPatientRoom = inxPatientRoom.Where(x => x.mrm_id == next_mrm_id && x.mvt_id == next_mvt_id).FirstOrDefault();
                                    msgAlert = string.Format(Program.MsgSend, nextPatientRoom.mrm_ename, nextPatientRoom.mhs_ename, nextPatientRoom.mze_ename, queue_no);

                                    try
                                    {
                                        cdc.SubmitChanges();
                                        if (inxPatientRoom.Count() > 0)
                                        {
                                            trn_patient_queue next_queue = PatientRegis.trn_patient_queues.Where(x => x.tps_status == "NS" && x.tps_ns_status == "QL").FirstOrDefault();
                                            if (next_queue != null)
                                            {
                                                List<log_send_auto> log = GetLogSendAuto(inxPatientRoom, next_queue.tps_id, tps_id, queue_no);
                                                cdc.log_send_autos.InsertAllOnSubmit(log);
                                            }
                                        }
                                        cdc.SubmitChanges();
                                        cdc.Transaction.Commit();
                                        return result;
                                    }
                                    catch (ChangeConflictException)
                                    {
                                        cdc.Transaction.Rollback();
                                        msgAlert = "ไม่สามารถ Skip Q." + PatientRegis.tpr_queue_no + " ได้ เนื่องจากสถานะคนไข้ไม่ได้อยู่ใน Waiting List.";
                                        isPopup = true;
                                        return ResultSendQueue.OldRoom;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendAutoCls", "SendAuto(int tpr_id, int mrd_id, List<int?> complete_mvt_id, ref string msgAlert, ref bool isPopup)", ex, false);
                        isPopup = false;
                        msgAlert = "กรุณากดส่งอีกครั้ง";
                        return ResultSendQueue.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SendAuto(int tpr_id, int mrd_id, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        private ResultSendQueue filterPatientRoomOnSkip(List<sp_get_patient_roomResult> input,
                                                        ref List<inx_sp_get_patient_roomResult> output,
                                                        List<int> Skip_mvt_id,
                                                        int current_site,
                                                        int? mze_id,
                                                        int waiting_time_out_zone)
        {
            try
            {
                List<sp_get_patient_roomResult> patientRoomWithOutCancel = input.Where(x => x.tpl_status != "C").ToList();
                if (patientRoomWithOutCancel.Count == 0)
                {
                    output = new List<inx_sp_get_patient_roomResult>();
                    return ResultSendQueue.OldRoom;
                }
                else
                {
                    List<sp_get_patient_roomResult> patientRoomWithOutRoomEvent = patientRoomWithOutCancel.Where(x => !Skip_mvt_id.Contains(x.mvt_id)).ToList();
                    if (patientRoomWithOutRoomEvent.Count() == 0)
                    {
                        output = new List<inx_sp_get_patient_roomResult>();
                        return ResultSendQueue.OldRoom;
                    }
                    else
                    {
                        List<sp_get_patient_roomResult> listInput = input.Select(x => new sp_get_patient_roomResult
                                                                    {
                                                                        count_doctor_ns = x.count_doctor_ns,
                                                                        count_doctor_wk = x.count_doctor_wk,
                                                                        login_flag = x.login_flag,
                                                                        mhs_code = x.mhs_code,
                                                                        mhs_ename = x.mhs_ename,
                                                                        mhs_id = x.mhs_id,
                                                                        mrm_code = x.mrm_code,
                                                                        mrm_ename = x.mrm_ename,
                                                                        mrm_id = x.mrm_id,
                                                                        mrm_seq_room = x.mrm_seq_room,
                                                                        mvt_code = x.mvt_code,
                                                                        mvt_id = x.mvt_id,
                                                                        mvt_type_cate = x.mvt_type_cate,
                                                                        mze_code = x.mze_code,
                                                                        mze_ename = x.mze_ename,
                                                                        mze_id = x.mze_id,
                                                                        patient_vip = x.patient_vip,
                                                                        send_type = x.send_type,
                                                                        site_rm = x.site_rm,
                                                                        skip_seq = x.skip_seq,
                                                                        skip_type = x.skip_type,
                                                                        tpl_status = x.tpl_status,
                                                                        tpr_id = x.tpr_id,
                                                                        waiting_person = x.waiting_person,
                                                                        waiting_time = x.waiting_time
                                                                    }).ToList();
                        CheckRoomBef(ref listInput);
                        CheckEventCate(ref listInput);
                        List<sp_get_patient_roomResult> listInputFilter = listInput.Where(x => x.tpl_status != "C" && !Skip_mvt_id.Contains(x.mvt_id)).ToList();
                        output = insertSeqPatientRoom(listInputFilter, current_site, mze_id, waiting_time_out_zone);
                        return ResultSendQueue.SendComplete;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "filterPatientRoomOnSkip", ex, false);
                return ResultSendQueue.Error;
            }
        }

        public ResultSendQueue CancelSendAuto(int tps_id, List<int> complete_mvt_id, string username, ref string msgAlert, ref bool isPopup)
        {
            try
            {
                List<int> pending_mvt_id = new List<int>();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue patientQueue = cdc.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
                    if (patientQueue.mrd_id != null)
                    {
                        int mrd_id = (int)patientQueue.mrd_id;
                        mst_room_dtl roomDtl = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                        pending_mvt_id = cdc.mst_room_events
                                         .Where(x => x.mrm_id == roomDtl.mrm_id)
                                         .Select(x => x.mvt_id).ToList();
                    }
                    else
                    {
                        pending_mvt_id = cdc.mst_room_events
                                         .Where(x => x.mrm_id == patientQueue.mrm_id)
                                         .Select(x => x.mvt_id).ToList();
                    }
                }
                if (pending_mvt_id.Count() == 0)
                {
                    isPopup = false;
                    msgAlert = "กรุณากดส่งอีกครั้ง";
                    return ResultSendQueue.Error;
                }
                else
                {
                    return CancelSendAuto(tps_id, complete_mvt_id, pending_mvt_id, username, ref msgAlert, ref isPopup);
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SkipSendAuto(int tps_id, string username, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        public ResultSendQueue CancelSendAuto(int tps_id, List<int> complete_mvt_id, List<int> cancel_mvt_id, string username, ref string msgAlert, ref bool isPopup)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        trn_patient_queue PatientQueue = cdc.trn_patient_queues
                                                            .Where(x => x.tps_id == tps_id)
                                                            .FirstOrDefault();

                        trn_patient_regi PatientRegis = PatientQueue.trn_patient_regi;

                        int tpr_id = PatientRegis.tpr_id;
                        string queue_no = PatientRegis.tpr_queue_no;
                        int mrm_id = (int)PatientQueue.mrm_id;
                        int? mrd_id = PatientQueue.mrd_id;
                        int? mhs_id = PatientRegis.tpr_site_use == null ? PatientRegis.mhs_id : PatientRegis.tpr_site_use;
                        int current_site = PatientQueue.mst_room_hdr.mhs_id;

                        List<int> room_event = new List<int>();

                        mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                        if (PatientQueue.mrd_id != null)
                        {
                            mst_room_dtl roomDtl = roomHdr.mst_room_dtls.Where(x => x.mrd_id == PatientQueue.mrd_id).FirstOrDefault();
                            if (roomDtl.mst_room_hdr.mrm_code == "EM")
                            {
                                if (roomDtl.mrd_type == 'D')
                                {
                                    mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "EM" && x.mvt_status == 'A').FirstOrDefault();
                                    room_event.Add(mstEvent.mvt_id);
                                }
                                else
                                {
                                    room_event.AddRange(roomHdr.mst_room_events.Select(x => x.mvt_id));
                                }
                            }
                            else
                            {
                                room_event = cdc.mst_room_events
                                                .Where(x => x.mrm_id == mrm_id)
                                                .Select(x => x.mvt_id).ToList();
                            }
                        }
                        else
                        {
                            room_event = cdc.mst_room_events
                                            .Where(x => x.mrm_id == mrm_id)
                                            .Select(x => x.mvt_id).ToList();
                        }

                        DateTime dateNow = Program.GetServerDateTime();

                        int? mze_id = roomHdr.mze_id;

                        int? next_mrm_id = null;
                        int? next_mvt_id = null;

                        List<sp_get_patient_roomResult> patientRoom = cdc.sp_get_patient_room(tpr_id, mhs_id).ToList();
                        List<sp_get_patient_roomResult> patientEventPendingComplete = patientRoom.Where(x => complete_mvt_id.Contains(x.mvt_id)).ToList();
                        foreach (sp_get_patient_roomResult pr in patientEventPendingComplete)
                        {
                            patientRoom.Remove(pr);
                        }
                        List<inx_sp_get_patient_roomResult> inxPatientRoom = new List<inx_sp_get_patient_roomResult>();
                        ResultSendQueue result = filterPatientRoomOnCancel(patientRoom, ref inxPatientRoom, cancel_mvt_id, current_site, mze_id, 0);

                        if (result == ResultSendQueue.Error)
                        {
                            isPopup = false;
                            msgAlert = "กรุณากดส่งอีกครั้ง";
                            return ResultSendQueue.Error;
                        }
                        else if (result == ResultSendQueue.OldRoom)
                        {
                            isPopup = false;
                            msgAlert = "";
                            return ResultSendQueue.OldRoom;
                        }
                        else
                        {
                            new TransactionPlanCls().EndPlan(ref PatientRegis, complete_mvt_id);
                            if (roomHdr.mrm_code == "EM")
                            {
                                new TransactionPlanCls().CancelPlan(ref PatientRegis, room_event);
                            }
                            else
                            {
                                new TransactionPlanCls().CancelPlan(ref PatientRegis, cancel_mvt_id);
                            }
                            new TransactionQueueCls().ReserveCurrentQueue(ref PatientRegis, tps_id);
                            cdc.trn_patient_queues.DeleteOnSubmit(PatientQueue);

                            if (result == ResultSendQueue.SendCheckC)
                            {
                                new TransactionQueueCls().SendToCheckC(ref PatientRegis, dateNow, username);
                                isPopup = false;
                            }
                            else if (result == ResultSendQueue.SendCheckB)
                            {
                                new TransactionQueueCls().SendToCheckB(ref PatientRegis, dateNow, username);
                                isPopup = false;
                            }
                            else
                            {
                                string doctor_code = (PatientRegis.tpr_req_doc_code == null || PatientRegis.tpr_req_doc_code == "") ? PatientRegis.tpr_pe_doc_code : PatientRegis.tpr_req_doc_code;
                                bool checkPE = CheckPE(tpr_id, doctor_code, inxPatientRoom, dateNow, ref next_mrm_id, ref next_mvt_id);
                                if (checkPE)
                                {
                                    new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                    isPopup = false;
                                }
                                else
                                {
                                    inx_sp_get_patient_roomResult firstPatientRoom = inxPatientRoom.OrderBy(x => x.priority).FirstOrDefault();
                                    next_mrm_id = firstPatientRoom.mrm_id;
                                    next_mvt_id = firstPatientRoom.mvt_id;
                                    new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                    isPopup = false;
                                }
                            }

                            inx_sp_get_patient_roomResult nextPatientRoom = inxPatientRoom.Where(x => x.mrm_id == next_mrm_id && x.mvt_id == next_mvt_id).FirstOrDefault();
                            msgAlert = string.Format(Program.MsgSend, nextPatientRoom.mrm_ename, nextPatientRoom.mhs_ename, nextPatientRoom.mze_ename, queue_no);
                            cdc.SubmitChanges();
                            if (inxPatientRoom.Count() > 0)
                            {
                                trn_patient_queue next_queue = PatientRegis.trn_patient_queues.Where(x => x.tps_status == "NS" && x.tps_ns_status == "QL").FirstOrDefault();
                                if (next_queue != null)
                                {
                                    List<log_send_auto> log = GetLogSendAuto(inxPatientRoom, next_queue.tps_id, tps_id, queue_no);
                                    cdc.log_send_autos.InsertAllOnSubmit(log);
                                }
                            }
                            cdc.SubmitChanges();
                            cdc.Transaction.Commit();
                            return result;
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendAutoCls", "SkipSendAuto(int tps_id, List<int> skip_mvt_id, string username, ref string msgAlert, ref bool isPopup)", ex, false);
                        isPopup = false;
                        msgAlert = "กรุณากดส่งอีกครั้ง";
                        return ResultSendQueue.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SkipSendAuto(int tps_id, List<int> skip_mvt_id, string username, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        public ResultSendQueue CancelOnWaitingSendAuto(int tpr_id, int tps_id, List<int> cancel_mvt_id, string username, ref string msgAlert, ref bool isPopup)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        string queue_no = PatientRegis.tpr_queue_no;

                        trn_patient_queue PatientQueue = PatientRegis.trn_patient_queues
                                                                     .Where(x => x.tps_id == tps_id)
                                                                     .FirstOrDefault();

                        int current_site = PatientQueue.mst_room_hdr.mhs_id;

                        if (PatientQueue == null)
                        {
                            msgAlert = "ไม่สามารถ Skip Q." + PatientRegis.tpr_queue_no + " ได้ เนื่องจากสถานะคนไข้ไม่ได้อยู่ใน Waiting List.";
                            isPopup = true;
                            return ResultSendQueue.OldRoom;
                        }
                        else
                        {
                            if (PatientQueue.tps_status != "NS" || PatientQueue.tps_ns_status != "QL")
                            {
                                msgAlert = "ไม่สามารถ Skip Q." + PatientRegis.tpr_queue_no + " ได้ เนื่องจากสถานะคนไข้ไม่ได้อยู่ใน Waiting List.";
                                isPopup = true;
                                return ResultSendQueue.OldRoom;
                            }
                            else
                            {
                                int mrm_id = (int)PatientQueue.mrm_id;
                                int? mrd_id = PatientQueue.mrd_id;
                                int? mhs_id = PatientRegis.tpr_site_use == null ? PatientRegis.mhs_id : PatientRegis.tpr_site_use;

                                List<int> room_event = new List<int>();

                                mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                                if (PatientQueue.mrd_id != null)
                                {
                                    mst_room_dtl roomDtl = roomHdr.mst_room_dtls.Where(x => x.mrd_id == PatientQueue.mrd_id).FirstOrDefault();
                                    if (roomDtl.mst_room_hdr.mrm_code == "EM")
                                    {
                                        if (roomDtl.mrd_type == 'D')
                                        {
                                            mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "EM" && x.mvt_status == 'A').FirstOrDefault();
                                            room_event.Add(mstEvent.mvt_id);
                                        }
                                        else
                                        {
                                            mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "EN" && x.mvt_status == 'A').FirstOrDefault();
                                            room_event.Add(mstEvent.mvt_id);
                                        }
                                    }
                                    else
                                    {
                                        room_event = cdc.mst_room_events
                                                        .Where(x => x.mrm_id == mrm_id)
                                                        .Select(x => x.mvt_id).ToList();
                                    }
                                }
                                else
                                {
                                    room_event = cdc.mst_room_events
                                                    .Where(x => x.mrm_id == mrm_id)
                                                    .Select(x => x.mvt_id).ToList();
                                }

                                DateTime dateNow = Program.GetServerDateTime();

                                int? mze_id = null;
                                mze_id = roomHdr.mze_id;

                                int? next_mrm_id = null;
                                int? next_mvt_id = null;

                                List<sp_get_patient_roomResult> patientRoom = cdc.sp_get_patient_room(tpr_id, mhs_id).ToList();
                                List<inx_sp_get_patient_roomResult> inxPatientRoom = new List<inx_sp_get_patient_roomResult>();
                                ResultSendQueue result = filterPatientRoomOnCancel(patientRoom, ref inxPatientRoom, room_event, current_site, mze_id, 0);

                                if (result == ResultSendQueue.Error)
                                {
                                    isPopup = false;
                                    msgAlert = "กรุณากดส่งอีกครั้ง";
                                    return ResultSendQueue.Error;
                                }
                                else if (result == ResultSendQueue.OldRoom)
                                {
                                    isPopup = false;
                                    msgAlert = "";
                                    return ResultSendQueue.OldRoom;
                                }
                                else
                                {
                                    if (roomHdr.mrm_code == "EM")
                                    {
                                        new TransactionPlanCls().CancelPlan(ref PatientRegis, room_event);
                                    }
                                    else
                                    {
                                        new TransactionPlanCls().CancelPlan(ref PatientRegis, cancel_mvt_id);
                                    }
                                    new TransactionQueueCls().ReserveCurrentQueue(ref PatientRegis, tps_id);
                                    cdc.trn_patient_queues.DeleteOnSubmit(PatientQueue);

                                    if (result == ResultSendQueue.SendCheckC)
                                    {
                                        new TransactionQueueCls().SendToCheckC(ref PatientRegis, dateNow, username);
                                        isPopup = false;
                                    }
                                    else if (result == ResultSendQueue.SendCheckB)
                                    {
                                        new TransactionQueueCls().SendToCheckB(ref PatientRegis, dateNow, username);
                                        isPopup = false;
                                    }
                                    else
                                    {
                                        string doctor_code = (PatientRegis.tpr_req_doc_code == null || PatientRegis.tpr_req_doc_code == "") ? PatientRegis.tpr_pe_doc_code : PatientRegis.tpr_req_doc_code;
                                        bool checkPE = CheckPE(tpr_id, doctor_code, inxPatientRoom, dateNow, ref next_mrm_id, ref next_mvt_id);
                                        if (checkPE)
                                        {
                                            new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                            isPopup = false;
                                        }
                                        else
                                        {
                                            inx_sp_get_patient_roomResult firstPatientRoom = inxPatientRoom.OrderBy(x => x.priority).FirstOrDefault();
                                            next_mrm_id = firstPatientRoom.mrm_id;
                                            next_mvt_id = firstPatientRoom.mvt_id;
                                            new TransactionQueueCls().SendToRoom(ref PatientRegis, (int)next_mrm_id, (int)next_mvt_id, dateNow, username);
                                            isPopup = false;
                                        }
                                    }

                                    inx_sp_get_patient_roomResult nextPatientRoom = inxPatientRoom.Where(x => x.mrm_id == next_mrm_id && x.mvt_id == next_mvt_id).FirstOrDefault();
                                    msgAlert = string.Format(Program.MsgSend, nextPatientRoom.mrm_ename, nextPatientRoom.mhs_ename, nextPatientRoom.mze_ename, queue_no);

                                    try
                                    {
                                        cdc.SubmitChanges();
                                        if (inxPatientRoom.Count() > 0)
                                        {
                                            trn_patient_queue next_queue = PatientRegis.trn_patient_queues.Where(x => x.tps_status == "NS" && x.tps_ns_status == "QL").FirstOrDefault();
                                            if (next_queue != null)
                                            {
                                                List<log_send_auto> log = GetLogSendAuto(inxPatientRoom, next_queue.tps_id, tps_id, queue_no);
                                                cdc.log_send_autos.InsertAllOnSubmit(log);
                                            }
                                        }
                                        cdc.SubmitChanges();
                                        cdc.Transaction.Commit();
                                        return result;
                                    }
                                    catch (ChangeConflictException)
                                    {
                                        cdc.Transaction.Rollback();
                                        msgAlert = "ไม่สามารถ Skip Q." + PatientRegis.tpr_queue_no + " ได้ เนื่องจากสถานะคนไข้ไม่ได้อยู่ใน Waiting List.";
                                        isPopup = true;
                                        return ResultSendQueue.OldRoom;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendAutoCls", "SendAuto(int tpr_id, int mrd_id, List<int?> complete_mvt_id, ref string msgAlert, ref bool isPopup)", ex, false);
                        isPopup = false;
                        msgAlert = "กรุณากดส่งอีกครั้ง";
                        return ResultSendQueue.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SendAuto(int tpr_id, int mrd_id, ref string msgAlert, ref bool isPopup)", ex, false);
                isPopup = false;
                msgAlert = "กรุณากดส่งอีกครั้ง";
                return ResultSendQueue.Error;
            }
        }
        private ResultSendQueue filterPatientRoomOnCancel(List<sp_get_patient_roomResult> input,
                                                          ref List<inx_sp_get_patient_roomResult> output,
                                                          List<int> Cancel_mvt_id,
                                                          int current_site,
                                                          int? mze_id,
                                                          int waiting_time_out_zone)
        {
            try
            {
                List<sp_get_patient_roomResult> patientRoomWithOutCancel = input.Where(x => x.tpl_status != "C").ToList();
                if (patientRoomWithOutCancel.Count == 0)
                {
                    output = new List<inx_sp_get_patient_roomResult>();
                    return ResultSendQueue.SendCheckC;
                }
                else
                {
                    List<sp_get_patient_roomResult> patientRoomWithOutRoomEvent = patientRoomWithOutCancel.Where(x => !Cancel_mvt_id.Contains(x.mvt_id)).ToList();
                    if (patientRoomWithOutRoomEvent.Count() == 0)
                    {
                        output = new List<inx_sp_get_patient_roomResult>();
                        return ResultSendQueue.SendCheckC;
                    }
                    else
                    {
                        List<sp_get_patient_roomResult> listInput = input.Select(x => new sp_get_patient_roomResult
                                                                    {
                                                                        count_doctor_ns = x.count_doctor_ns,
                                                                        count_doctor_wk = x.count_doctor_wk,
                                                                        login_flag = x.login_flag,
                                                                        mhs_code = x.mhs_code,
                                                                        mhs_ename = x.mhs_ename,
                                                                        mhs_id = x.mhs_id,
                                                                        mrm_code = x.mrm_code,
                                                                        mrm_ename = x.mrm_ename,
                                                                        mrm_id = x.mrm_id,
                                                                        mrm_seq_room = x.mrm_seq_room,
                                                                        mvt_code = x.mvt_code,
                                                                        mvt_id = x.mvt_id,
                                                                        mvt_type_cate = x.mvt_type_cate,
                                                                        mze_code = x.mze_code,
                                                                        mze_ename = x.mze_ename,
                                                                        mze_id = x.mze_id,
                                                                        patient_vip = x.patient_vip,
                                                                        send_type = x.send_type,
                                                                        site_rm = x.site_rm,
                                                                        skip_seq = x.skip_seq,
                                                                        skip_type = x.skip_type,
                                                                        tpl_status = x.tpl_status,
                                                                        tpr_id = x.tpr_id,
                                                                        waiting_person = x.waiting_person,
                                                                        waiting_time = x.waiting_time
                                                                    }).ToList();
                        CheckRoomBef(ref listInput);
                        CheckEventCate(ref listInput);
                        List<sp_get_patient_roomResult> listInputFilter = listInput.Where(x => x.tpl_status != "C" && !Cancel_mvt_id.Contains(x.mvt_id)).ToList();
                        output = insertSeqPatientRoom(listInputFilter, current_site, mze_id, waiting_time_out_zone);
                        return ResultSendQueue.SendComplete;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "filterPatientRoomOnSkip", ex, false);
                return ResultSendQueue.Error;
            }
        }


        private List<log_send_auto> GetLogSendAuto(List<inx_sp_get_patient_roomResult> input, int tps_id, int process_tps_id, string queue_no)
        {
            try
            {
                List<log_send_auto> log = input.OrderBy(x => x.priority).ToList()
                                               .Select((x, inx) => new log_send_auto
                                               {
                                                   seq_id = inx + 1,
                                                   login_flag = x.login_flag,
                                                   mhs_code = x.mhs_code,
                                                   mhs_ename = x.mhs_ename,
                                                   mrm_id = x.mrm_id,
                                                   mrm_ename = x.mrm_ename,
                                                   mrm_seq_room = x.mrm_seq_room,
                                                   mvt_code = x.mvt_code,
                                                   mvt_id = x.mvt_id,
                                                   mvt_type_cate = x.mvt_type_cate,
                                                   mze_code = x.mze_code,
                                                   mze_ename = x.mze_ename,
                                                   mze_id = x.mze_id,
                                                   patient_vip = x.patient_vip,
                                                   process_tps_id = process_tps_id,
                                                   process_type = "A",
                                                   send_type = x.send_type,
                                                   site_rm = x.site_rm,
                                                   skip_seq = x.skip_seq,
                                                   skip_type = x.skip_type,
                                                   tpr_id = x.tpr_id,
                                                   waiting_person = x.waiting_person,
                                                   waiting_time = x.waiting_time,
                                                   tps_id = tps_id,
                                                   tpr_queue_no = queue_no
                                               }).ToList();
                return log;
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "LogSendAuto", ex, false);
                return new List<log_send_auto>();
            }
        }

        private class inx_sp_get_patient_roomResult : sp_get_patient_roomResult
        {
            public int priority { get; set; }
        }
        private List<inx_sp_get_patient_roomResult> insertSeqPatientRoom(List<sp_get_patient_roomResult> input, int mhs_id, int? mze_id, int waiting_time_out_zone)
        {
            try
            {
                if (mze_id != null)
                {
                    List<inx_sp_get_patient_roomResult> output = input.OrderBy(x => x.tpl_status == "N" ? 0 : 1)
                                                                      .ThenBy(x => x.site_rm == mhs_id ? 0 : x.site_rm)
                                                                      .ThenBy(x => x.send_type == true ? 0 : 1)
                                                                      .ThenBy(x => x.skip_seq)
                                                                      .ThenBy(x => x.login_flag == "Y" ? 0 : 1)
                                                                      .ThenBy(x => x.mze_id == mze_id && x.waiting_time <= waiting_time_out_zone ? 0 : 1)
                                                                      .ThenBy(x => x.mze_id != mze_id && x.waiting_time <= waiting_time_out_zone ? 0 : 1)
                                                                      .ThenBy(x => x.mze_id == mze_id ? 0 : 1)
                                                                      .ThenBy(x => x.waiting_time)
                                                                      .ThenBy(x => x.mze_id)
                                                                      .ThenBy(x => x.mrm_seq_room)
                                                                      .ToList()
                                                                      .Select((x, inx) => new inx_sp_get_patient_roomResult
                                                                      {
                                                                          priority = inx + 1,
                                                                          login_flag = x.login_flag,
                                                                          mhs_code = x.mhs_code,
                                                                          mhs_ename = x.mhs_ename,
                                                                          mrm_id = x.mrm_id,
                                                                          mrm_ename = x.mrm_ename,
                                                                          mrm_seq_room = x.mrm_seq_room,
                                                                          mvt_code = x.mvt_code,
                                                                          mvt_id = x.mvt_id,
                                                                          mvt_type_cate = x.mvt_type_cate,
                                                                          mze_code = x.mze_code,
                                                                          mze_ename = x.mze_ename,
                                                                          mze_id = x.mze_id,
                                                                          patient_vip = x.patient_vip,
                                                                          send_type = x.send_type,
                                                                          site_rm = x.site_rm,
                                                                          skip_seq = x.skip_seq,
                                                                          skip_type = x.skip_type,
                                                                          tpr_id = x.tpr_id,
                                                                          waiting_person = x.waiting_person,
                                                                          waiting_time = x.waiting_time
                                                                      }).ToList();
                    return output;
                }
                else
                {
                    List<inx_sp_get_patient_roomResult> output = input.OrderBy(x => x.tpl_status == "N" ? 0 : 1)
                                                                      .ThenBy(x => x.site_rm == mhs_id ? 0 : x.site_rm)
                                                                      .ThenBy(x => x.site_rm)
                                                                      .ThenBy(x => x.send_type == true ? 0 : 1)
                                                                      .ThenBy(x => x.skip_seq)
                                                                      .ThenBy(x => x.login_flag == "Y" ? 0 : 1)
                                                                      .ThenBy(x => x.waiting_person)
                                                                      .ThenBy(x => x.waiting_time)
                                                                      .ThenBy(x => x.mze_id)
                                                                      .ThenBy(x => x.mrm_seq_room)
                                                                      .ToList()
                                                                      .Select((x, inx) => new inx_sp_get_patient_roomResult
                                                                      {
                                                                          priority = inx + 1,
                                                                          login_flag = x.login_flag,
                                                                          mhs_code = x.mhs_code,
                                                                          mhs_ename = x.mhs_ename,
                                                                          mrm_id = x.mrm_id,
                                                                          mrm_ename = x.mrm_ename,
                                                                          mrm_seq_room = x.mrm_seq_room,
                                                                          mvt_code = x.mvt_code,
                                                                          mvt_id = x.mvt_id,
                                                                          mvt_type_cate = x.mvt_type_cate,
                                                                          mze_code = x.mze_code,
                                                                          mze_ename = x.mze_ename,
                                                                          mze_id = x.mze_id,
                                                                          patient_vip = x.patient_vip,
                                                                          send_type = x.send_type,
                                                                          site_rm = x.site_rm,
                                                                          skip_seq = x.skip_seq,
                                                                          skip_type = x.skip_type,
                                                                          tpr_id = x.tpr_id,
                                                                          waiting_person = x.waiting_person,
                                                                          waiting_time = x.waiting_time
                                                                      }).ToList();
                    return output;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "insetSeqPatientRoom", ex, false);
                return new List<inx_sp_get_patient_roomResult>();
            }
        }

        public enum ResultSendQueue
        {
            SendComplete,
            OldRoom,
            SendCheckB,
            SendCheckC,
            Error
        }
        private ResultSendQueue CheckRoomBef(ref List<sp_get_patient_roomResult> patientRoom)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    patientRoom.ForEach(x => x.send_type = true);

                    var mstRoomBef = (from vpr in patientRoom
                                      join mrb in cdc.mst_room_befores
                                      on vpr.mhs_id equals mrb.mhs_id
                                      join mvt_bef in cdc.mst_events
                                      on mrb.mrm_before equals mvt_bef.mvt_code
                                      join mvt_aft in cdc.mst_events
                                      on mrb.mrm_after equals mvt_aft.mvt_code
                                      where mvt_aft.mvt_id == vpr.mvt_id &&
                                            mrb.mrb_status == 'A' &&
                                            mvt_aft.mvt_status == 'A' &&
                                            mvt_bef.mvt_status == 'A'
                                      select new
                                      {
                                          mvt_after = mvt_aft.mvt_id,
                                          mvt_before = mvt_bef.mvt_id
                                      }).ToList();

                    foreach (var mst in mstRoomBef)
                    {
                        List<sp_get_patient_roomResult> pr = (from vpr in patientRoom
                                                              join mre in cdc.mst_room_events
                                                              on vpr.mrm_id equals mre.mrm_id
                                                              where vpr.mvt_id == mst.mvt_after
                                                              select vpr).ToList();
                        foreach (var re in pr)
                        {
                            re.send_type = false;
                        }
                    }
                    return ResultSendQueue.SendComplete;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "CheckRoomBef(ref List<sp_get_patient_roomResult> patientRoom)", ex, false);
                return ResultSendQueue.Error;
            }
        }
        private ResultSendQueue CheckEventCate(ref List<sp_get_patient_roomResult> patientRoom)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_event eventDoctor = cdc.mst_events.Where(x => x.mvt_code == "EM" && x.mvt_status == 'A').FirstOrDefault();
                    mst_event eventNurse = cdc.mst_events.Where(x => x.mvt_code == "EN" && x.mvt_status == 'A').FirstOrDefault();
                    if (eventDoctor != null && eventNurse != null)
                    {
                        foreach (sp_get_patient_roomResult re in patientRoom)
                        {
                            if (re.mvt_id == eventDoctor.mvt_id)
                            {
                                if (patientRoom.Any(x => x.mvt_id == eventNurse.mvt_id))
                                {
                                    re.mvt_type_cate = "S";
                                }
                                else
                                {
                                    re.mvt_type_cate = "M";
                                }
                            }
                        }
                    }
                    return ResultSendQueue.SendComplete;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "CheckEventCate(ref List<sp_get_patient_roomResult> patientRoom)", ex, false);
                return ResultSendQueue.Error;
            }
        }

        private bool CheckEyeDoctor(int tpr_id, int? mrd_id, bool eye_dropper, List<inx_sp_get_patient_roomResult> patientRoom, ref int? next_mrm_id, ref int? next_mvt_id)
        {
            try
            {
                if (mrd_id == null)
                {
                    return false;
                }
                else
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        mst_room_dtl roomDtl = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                        if (roomDtl.mst_room_hdr.mrm_code == "EM")
                        {
                            if (roomDtl.mrd_type == 'D')
                            {
                                return false;
                            }
                            else
                            {
                                sp_get_patient_roomResult patientRoomEye = patientRoom.Where(x => x.mvt_code == "EM").FirstOrDefault();
                                if (patientRoomEye == null)
                                {
                                    return false;
                                }
                                else
                                {
                                    if (eye_dropper == true)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        next_mrm_id = patientRoomEye.mrm_id;
                                        next_mvt_id = patientRoomEye.mvt_id;
                                        return true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "CheckEyeDoctor(int mrm_id)", ex, false);
                return false;
            }
        }
        private bool CheckPE(int tpr_id, string doctor_code, List<inx_sp_get_patient_roomResult> patientRoom, DateTime dateNow, ref int? next_mrm_id, ref int? next_mvt_id)
        {
            try
            {
                sp_get_patient_roomResult patientRoomDoctor = patientRoom.Where(x => x.mvt_code == "PE").FirstOrDefault();
                if (patientRoomDoctor == null)
                {
                    return false;
                }
                else
                {
                    if (doctor_code == "" || doctor_code == null)
                    {
                        if (patientRoomDoctor.waiting_person != 0)
                        {
                            return false;
                        }
                        else
                        {
                            next_mrm_id = patientRoomDoctor.mrm_id;
                            next_mvt_id = patientRoomDoctor.mvt_id;
                            return true;
                        }
                    }
                    else
                    {
                        if (patientRoomDoctor.login_flag != "Y")
                        {
                            return false;
                        }
                        else
                        {
                            if (patientRoomDoctor.count_doctor_ns > 0 || patientRoomDoctor.count_doctor_wk > 0)
                            {

                                return false;
                            }
                            else
                            {
                                next_mrm_id = patientRoomDoctor.mrm_id;
                                next_mvt_id = patientRoomDoctor.mvt_id;
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "CheckEyeDoctor(int mrm_id)", ex, false);
                return false;
            }
        }
    }
}
