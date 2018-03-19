using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.QueueClass
{
    class TransactionQueueCls
    {
        //public void SendToBasic(ref trn_patient_regi tpr, ref string messegeSend)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();

        //        int mvt_regis = mst.getMstEvent("RG").mvt_id;

        //        trn_patient_queue objQueueRegis = new trn_patient_queue();
        //        objQueueRegis.mrm_id = Program.CurrentRoom.mrm_id;  //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        objQueueRegis.mvt_id = mvt_regis;
        //        objQueueRegis.mrd_id = Program.CurrentRoom.mrd_id;  //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        objQueueRegis.tps_start_date = dateNow;
        //        objQueueRegis.tps_end_date = dateNow;
        //        objQueueRegis.tps_send_by = Program.CurrentUser.mut_username;  //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        objQueueRegis.tps_status = "ED";
        //        objQueueRegis.tps_ns_status = null;
        //        objQueueRegis.tps_create_date = dateNow;
        //        objQueueRegis.tps_create_by = Program.CurrentUser.mut_username; //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        objQueueRegis.tps_update_by = objQueueRegis.tps_create_by;
        //        objQueueRegis.tps_update_date = dateNow;
        //        tpr.trn_patient_queues.Add(objQueueRegis);

        //        mst_room_hdr mrm = mst.getMstRoomHdr("BM", Program.CurrentSite.mhs_code); //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        mst_hpc_site mhs = mst.getMstHpcSite(Program.CurrentSite.mhs_code); //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        mst_zone mze = mst.getMstZone((int)mrm.mze_id); //error
        //        mst_event mvt = mst.getMstEvent("BM");

        //        trn_patient_queue objQueueBasic = new trn_patient_queue();
        //        objQueueBasic.mrm_id = mrm.mrm_id; //error
        //        objQueueBasic.mvt_id = mvt.mvt_id; //error
        //        objQueueBasic.mrd_id = null;
        //        objQueueBasic.tps_status = "NS";
        //        objQueueBasic.tps_ns_status = "WL";
        //        objQueueBasic.tps_create_by = Program.CurrentUser.mut_username; //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        objQueueBasic.tps_create_date = dateNow;
        //        objQueueBasic.tps_update_by = Program.CurrentUser.mut_username; //ถ้า error ที่นี่ regis คนไข้ถัดไป น่าจะ error ด้วย
        //        objQueueBasic.tps_update_date = dateNow;
        //        tpr.trn_patient_queues.Add(objQueueBasic);

        //        messegeSend = string.Format(Program.MsgSend, mrm.mrm_ename, mze.mze_ename, mhs.mhs_ename, tpr.tpr_queue_no); //error
        //        return StatusTransaction.True;
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToBasic", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

        //public StatusTransaction SendToScreening(ref trn_patient_regi tpr, ref string messegeSend)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        mst_room_hdr mrm = mst.getMstRoomHdr("SC", Program.CurrentSite.mhs_code);
        //        mst_hpc_site mhs = mst.getMstHpcSite(mrm.mhs_id);
        //        mst_zone mze = mst.getMstZone((int)mrm.mze_id);
        //        mst_event mvt = mst.getMstEvent("SC");
        //        return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToScreening", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}


        //public void SendToCheckB(ref trn_patient_regi tpr, ref string messegeSend)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        mst_room_hdr mrm = mst.getMstRoomHdr("CB", Program.CurrentSite.mhs_code);
        //        mst_hpc_site mhs = mst.getMstHpcSite(mrm.mhs_id);
        //        mst_zone mze = mst.getMstZone((int)mrm.mze_id);
        //        mst_event mvt = mst.getMstEvent("CB");
        //        return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToCheckB", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

        //public void SendToRoom(ref trn_patient_regi tpr, ref string messegeSend, int mrm_id, int mvt_id)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        mst_room_hdr mrm = mst.getMstRoomHdr(mrm_id);
        //        mst_hpc_site mhs = mst.getMstHpcSite(mrm.mhs_id);
        //        mst_zone mze = mst.getMstZone((int)mrm.mze_id);
        //        mst_event mvt = mst.getMstEvent(mvt_id);
        //        trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm.mrm_id && x.mvt_id == mvt.mvt_id).FirstOrDefault();

        //        //sendRoom
        //        //trn_patient_queue updateQueue;
        //        //if (tps == null)
        //        //{
        //        //    updateQueue = new trn_patient_queue();
        //        //    updateQueue.tps_create_by = Program.CurrentUser.mut_username;
        //        //    updateQueue.tps_create_date = dateNow;
        //        //    tpr.trn_patient_queues.Add(updateQueue);
        //        //}
        //        //else
        //        //{
        //        //    updateQueue = tpr.trn_patient_queues.Where(x => x.tps_id == tps.tps_id).FirstOrDefault();
        //        //}
        //        //updateQueue.mrm_id = mrm.mrm_id;
        //        //updateQueue.mvt_id = mvt.mvt_id;
        //        //updateQueue.mrd_id = null;
        //        //updateQueue.tps_end_date = null;
        //        //updateQueue.tps_start_date = null;
        //        //updateQueue.tps_status = "NS";
        //        //updateQueue.tps_ns_status = "QL";
        //        //updateQueue.tps_update_by = Program.CurrentUser.mut_username;
        //        //updateQueue.tps_update_date = dateNow;

        //        if (tps == null)
        //        {
        //            tps = new trn_patient_queue();
        //            tps.tps_create_by = Program.CurrentUser.mut_username;
        //            tps.tps_create_date = dateNow;
        //            tpr.trn_patient_queues.Add(tps);
        //        }
        //        tps.mrm_id = mrm.mrm_id;
        //        tps.mvt_id = mvt.mvt_id;
        //        tps.mrd_id = null;
        //        tps.tps_end_date = null;
        //        tps.tps_start_date = null;
        //        tps.tps_status = "NS";
        //        tps.tps_ns_status = "QL";
        //        tps.tps_update_by = Program.CurrentUser.mut_username;
        //        tps.tps_update_date = dateNow;
        //        messegeSend = string.Format(Program.MsgSend, mrm.mrm_ename, mze.mze_ename, mhs.mhs_ename, tpr.tpr_queue_no);
        //        return StatusTransaction.True;
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToRoom", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

        //public StatusTransaction SendToDoctor(ref trn_patient_regi tpr, ref string messegeSend)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        mst_room_hdr mrm = mst.getMstRoomHdr("DC", Program.CurrentSite.mhs_code);
        //        mst_hpc_site mhs = mst.getMstHpcSite(mrm.mhs_id);
        //        mst_zone mze = mst.getMstZone((int)mrm.mze_id);
        //        mst_event mvt = mst.getMstEvent("DC");
        //        return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToDoctor", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

        //public StatusTransaction SendToCheckC(ref trn_patient_regi tpr, ref string messegeSend, bool andSendBook = false)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        mst_hpc_site mhs = mst.getMstHpcSite(tpr.mhs_id);
        //        mst_room_hdr mrm = mst.getMstRoomHdr("CC", mhs.mhs_code);
        //        mst_zone mze = mst.getMstZone((int)mrm.mze_id);
        //        mst_event mvt = mst.getMstEvent("CC");
        //        trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm.mrm_id && x.mvt_id == mvt.mvt_id).FirstOrDefault();

        //        //CheckC
        //        //trn_patient_queue updateQueue;
        //        //if (tps == null)
        //        //{
        //        //    updateQueue = new trn_patient_queue();
        //        //    updateQueue.tps_create_by = Program.CurrentUser.mut_username;
        //        //    updateQueue.tps_create_date = dateNow;
        //        //    tpr.trn_patient_queues.Add(updateQueue);
        //        //}
        //        //else
        //        //{
        //        //    updateQueue = tpr.trn_patient_queues.Where(x => x.tps_id == tps.tps_id).FirstOrDefault();
        //        //}
        //        //updateQueue.mrm_id = mrm.mrm_id;
        //        //updateQueue.mvt_id = mvt.mvt_id;
        //        //updateQueue.mrd_id = null;
        //        //updateQueue.tps_end_date = null;
        //        //updateQueue.tps_start_date = null;
        //        //if (andSendBook)
        //        //{
        //        //    updateQueue.tps_status = "ED";
        //        //    updateQueue.tps_ns_status = null;
        //        //}
        //        //else
        //        //{
        //        //    updateQueue.tps_status = "WK";
        //        //    updateQueue.tps_ns_status = null;
        //        //}
        //        //updateQueue.tps_update_by = Program.CurrentUser.mut_username;
        //        //updateQueue.tps_update_date = dateNow;

        //        if (tps == null)
        //        {
        //            tps = new trn_patient_queue();
        //            tps.tps_create_by = Program.CurrentUser.mut_username;
        //            tps.tps_create_date = dateNow;
        //            tpr.trn_patient_queues.Add(tps);
        //        }
        //        tps.mrm_id = mrm.mrm_id;
        //        tps.mvt_id = mvt.mvt_id;
        //        tps.mrd_id = null;
        //        tps.tps_end_date = null;
        //        tps.tps_start_date = null;
        //        if (andSendBook)
        //        {
        //            tps.tps_status = "ED";
        //            tps.tps_ns_status = null;
        //        }
        //        else
        //        {
        //            tps.tps_status = "WK";
        //            tps.tps_ns_status = null;
        //        }
        //        tps.tps_update_by = Program.CurrentUser.mut_username;
        //        tps.tps_update_date = dateNow;
        //        messegeSend = string.Format(Program.MsgSend, mrm.mrm_ename, mze.mze_ename, mhs.mhs_ename, tpr.tpr_queue_no);
        //        return StatusTransaction.True;
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToCheckC", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

        //public StatusTransaction SendToPHM(ref trn_patient_regi tpr, ref string messegeSend)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        mst_room_hdr mrm = mst.getMstRoomHdr("PH", Program.CurrentSite.mhs_code);
        //        //mst_hpc_site mhs = mst.getMstHpcSite(mrm.mhs_id);
        //        //mst_zone mze = mst.getMstZone((int)mrm.mze_id);
        //        mst_event mvt = mst.getMstEvent("PH");
        //        return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToPHM", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

        //public StatusTransaction SendToBook(ref trn_patient_regi tpr, ref string messegeSend)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        tpr.tpr_status = "WB";
        //        tpr.tpr_pe_status = "RS";
        //        tpr.tpr_update_by = Program.CurrentUser.mut_username;
        //        tpr.tpr_update_date = dateNow;
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        mst_hpc_site mhs = mst.getMstHpcSite(tpr.mhs_id);
        //        mst_room_hdr mrm = mst.getMstRoomHdr("BK", mhs.mhs_code);
        //        mst_event mvt = mst.getMstEvent("BK");
        //        trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm.mrm_id && x.mvt_id == mvt.mvt_id).FirstOrDefault();

        //        //book
        //        //trn_patient_queue updateQueue;
        //        //if (tps == null)
        //        //{
        //        //    updateQueue = new trn_patient_queue();
        //        //    updateQueue.tps_create_by = Program.CurrentUser.mut_username;
        //        //    updateQueue.tps_create_date = dateNow;
        //        //    tpr.trn_patient_queues.Add(updateQueue);
        //        //}
        //        //else
        //        //{
        //        //    updateQueue = tpr.trn_patient_queues.Where(x => x.tps_id == tps.tps_id).FirstOrDefault();
        //        //}
        //        //updateQueue.mrm_id = mrm.mrm_id;
        //        //updateQueue.mvt_id = mvt.mvt_id;
        //        //updateQueue.mrd_id = null;
        //        //updateQueue.tps_end_date = null;
        //        //updateQueue.tps_start_date = null;
        //        //updateQueue.tps_status = "NS";
        //        //updateQueue.tps_ns_status = "QL";
        //        //updateQueue.tps_update_by = Program.CurrentUser.mut_username;
        //        //updateQueue.tps_update_date = dateNow;

        //        if (tps == null)
        //        {
        //            tps = new trn_patient_queue();
        //            tps.tps_create_by = Program.CurrentUser.mut_username;
        //            tps.tps_create_date = dateNow;
        //            tpr.trn_patient_queues.Add(tps);
        //        }
        //        tps.mrm_id = mrm.mrm_id;
        //        tps.mvt_id = mvt.mvt_id;
        //        tps.mrd_id = null;
        //        tps.tps_end_date = null;
        //        tps.tps_start_date = null;
        //        tps.tps_status = "NS";
        //        tps.tps_ns_status = "QL";
        //        tps.tps_update_by = Program.CurrentUser.mut_username;
        //        tps.tps_update_date = dateNow;
        //        messegeSend = "Queue No. " + tpr.tpr_queue_no + " Send to Book Complete.";
        //        return StatusTransaction.True;
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "SendToBook", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

        //public StatusTransaction ReturnToCheckB(ref trn_patient_regi tpr, ref string messegeSend)
        //{
        //    try
        //    {
        //        DateTime dateNow = Program.GetServerDateTime();
        //        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //        tpr.tpr_pe_status = "RB";
        //        mst_room_hdr mrm = mst.getMstRoomHdr("CB", Program.CurrentSite.mhs_code);
        //        mst_hpc_site mhs = mst.getMstHpcSite(mrm.mhs_id);
        //        mst_zone mze = mst.getMstZone((int)mrm.mze_id);
        //        mst_event mvt = mst.getMstEvent("CB");
        //        return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("TransactionQueueCls", "ReturnToCheckB", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //}

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
                Program.MessageError("TransactionQueueCls", "CheckUseBeforePE(int mrm_id, int mvt_id)", ex, false);
                return false;
            }
        }

        public class PatientCallQueue
        {
            public enum StatusCallQueue
            {
                NoPatient,
                GotPatient,
                Error
            }
            public StatusCallQueue Status { get; set; }
            public int? tpr_id { get; set; }
            public int? tps_id { get; set; }
        }
        public PatientCallQueue CallQueue(int mrd_id, int mut_id, string username)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                List<Class.WaitingListCls.WaitingListDtl> result = new Class.WaitingListCls().getWaitingRoomDtl(mrd_id, mut_id);
                foreach (Class.WaitingListCls.WaitingListDtl row in result)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        try
                        {
                            cdc.Connection.Open();
                            DbTransaction trans = cdc.Connection.BeginTransaction();
                            cdc.Transaction = trans;

                            trn_patient_queue PatientQueue = cdc.trn_patient_queues
                                                                .Where(x => x.tps_id == row.tps_id)
                                                                .FirstOrDefault();
                            if (PatientQueue.tps_ns_status == "QL")
                            {
                                PatientQueue.mrd_id = mrd_id;
                                PatientQueue.tps_ns_status = "WR";
                                PatientQueue.tps_update_by = username;
                                PatientQueue.tps_update_date = dateNow;
                                try
                                {
                                    cdc.SubmitChanges();
                                    cdc.Transaction.Commit();
                                    Program.CurrentRegis = PatientQueue.trn_patient_regi;
                                    Program.CurrentPatient_queue = PatientQueue;
                                    return new PatientCallQueue
                                    {
                                        Status = PatientCallQueue.StatusCallQueue.GotPatient,
                                        tpr_id = PatientQueue.tpr_id,
                                        tps_id = PatientQueue.tps_id
                                    };
                                }
                                catch (System.Data.Linq.ChangeConflictException) //Win32Exception
                                {
                                    Program.CurrentRegis = null;
                                    Program.CurrentPatient_queue = null;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            cdc.Transaction.Rollback();
                            Program.MessageError("TransactionQueueCls", "CallQueue(int mrd_id, int mut_id, string username)", ex, false);
                            Program.CurrentRegis = null;
                            Program.CurrentPatient_queue = null;
                            return new PatientCallQueue
                            {
                                Status = PatientCallQueue.StatusCallQueue.Error,
                                tpr_id = null,
                                tps_id = null
                            };
                        }
                        finally
                        {
                            cdc.Connection.Close();
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "CallQueue(int mrd_id, int mut_id, string username)", ex, false);
                Program.CurrentRegis = null;
                Program.CurrentPatient_queue = null;
                return null;
            }
        }

        public class PatientReadyQueue
        {
            public enum StatusReadyQueue
            {
                Success,
                Error
            }
            public StatusReadyQueue Status { get; set; }
        }
        public PatientReadyQueue ReadyQueue(int tps_id, int mrd_id, string username)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
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

                        PatientQueue.mrd_id = mrd_id;
                        PatientQueue.tps_status = "WK";
                        PatientQueue.tps_ns_status = null;
                        PatientQueue.tps_bm_seq = null;
                        PatientQueue.tps_call_by = username;
                        PatientQueue.tps_call_date = dateNow;
                        PatientQueue.tps_start_date = dateNow;
                        PatientQueue.tps_update_by = username;
                        PatientQueue.tps_update_date = dateNow;
                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return new PatientReadyQueue
                        {
                            Status = PatientReadyQueue.StatusReadyQueue.Success
                        };
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                        return new PatientReadyQueue
                        {
                            Status = PatientReadyQueue.StatusReadyQueue.Error
                        };
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                return new PatientReadyQueue
                {
                    Status = PatientReadyQueue.StatusReadyQueue.Error
                };
            }
        }

        public class PatientHoldQueue
        {
            public enum StatusHoldQueue
            {
                Success,
                Error
            }
            public StatusHoldQueue Status { get; set; }
            public string QueueNo { get; set; }
        }
        public PatientHoldQueue HoldQueue(int tps_id, int hold_minute, string username)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
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

                        PatientQueue.tps_status = "NS";
                        PatientQueue.tps_ns_status = "QL";
                        PatientQueue.tps_call_status = "HD";
                        PatientQueue.tps_bm_seq = null;
                        PatientQueue.tps_hold_by = username;
                        PatientQueue.tps_hold_date = dateNow.AddMinutes(hold_minute);
                        PatientQueue.tps_start_date = null;
                        PatientQueue.tps_update_date = dateNow;
                        PatientQueue.tps_update_by = username;
                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return new PatientHoldQueue
                        {
                            Status = PatientHoldQueue.StatusHoldQueue.Success,
                            QueueNo = PatientQueue.trn_patient_regi.tpr_queue_no
                        };
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                        return new PatientHoldQueue
                        {
                            Status = PatientHoldQueue.StatusHoldQueue.Error,
                            QueueNo = ""
                        };
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                return new PatientHoldQueue
                {
                    Status = PatientHoldQueue.StatusHoldQueue.Error
                };
            }
        }

        public bool SendQueueDoctorResult(int tps_id, string username)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
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

                        PatientQueue.tps_send_by = username;
                        PatientQueue.tps_end_date = dateNow;
                        PatientQueue.tps_status = "ED";
                        PatientQueue.tps_ns_status = null;
                        PatientQueue.tps_update_by = username;
                        PatientQueue.tps_update_date = dateNow;
                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                        return false;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                return false;
            }
        }
        public bool SendQueueDoctorPEBeforeCheckB(int tpr_id, int tps_id, string username)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        trn_patient_regi patientRegis = cdc.trn_patient_regis
                                                           .Where(x => x.tpr_id == tpr_id)
                                                           .FirstOrDefault();

                        trn_patient_queue PatientQueue = patientRegis.trn_patient_queues
                                                                     .Where(x => x.tps_id == tps_id)
                                                                     .FirstOrDefault();

                        PatientQueue.tps_send_by = username;
                        PatientQueue.tps_end_date = dateNow;
                        PatientQueue.tps_status = "ED";
                        PatientQueue.tps_ns_status = null;
                        PatientQueue.tps_update_by = username;
                        PatientQueue.tps_update_date = dateNow;

                        mst_event eventScreening = cdc.mst_events.Where(x => x.mvt_code == "SC").FirstOrDefault();
                        if (patientRegis.trn_patient_queues.Any(x => x.mvt_id == eventScreening.mvt_id))
                        {
                            if (patientRegis.tpr_return_screening == true)
                            {
                                patientRegis.tpr_return_screening = false;
                                SendToScreening(ref patientRegis, dateNow, username);
                            }
                            else
                            {
                                SendToCheckB(ref patientRegis, dateNow, username);
                            }
                        }
                        else
                        {
                            SendToScreening(ref patientRegis, dateNow, username);
                        }

                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                        return false;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "ReadyQueue(int tps_id, int mrd_id, string username)", ex, false);
                return false;
            }
        }
    }
}
