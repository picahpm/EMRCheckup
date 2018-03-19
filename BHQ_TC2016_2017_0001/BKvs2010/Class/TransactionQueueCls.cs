using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.Class
{
    class TransactionQueueCls
    {
        public StatusTransaction SendToBasic(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();

                int mvt_regis = mst.GetMstEvent("RG").mvt_id;

                trn_patient_queue objQueueRegis = new trn_patient_queue();
                objQueueRegis.mrm_id = Program.CurrentRoom.mrm_id;
                objQueueRegis.mvt_id = mvt_regis;
                objQueueRegis.mrd_id = Program.CurrentRoom.mrd_id;
                objQueueRegis.tps_start_date = dateNow;
                objQueueRegis.tps_end_date = dateNow;
                objQueueRegis.tps_send_by = Program.CurrentUser.mut_username;
                objQueueRegis.tps_status = "ED";
                objQueueRegis.tps_ns_status = null;
                objQueueRegis.tps_create_date = dateNow;
                objQueueRegis.tps_create_by = Program.CurrentUser.mut_username;
                objQueueRegis.tps_update_by = objQueueRegis.tps_create_by;
                objQueueRegis.tps_update_date = dateNow;
                tpr.trn_patient_queues.Add(objQueueRegis);

                mst_room_hdr mrm = mst.GetMstRoomHdr("BM", Program.CurrentSite.mhs_code);
                mst_hpc_site mhs = mst.GetMstHpcSite(Program.CurrentSite.mhs_code);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("BM");

                trn_patient_queue objQueueBasic = new trn_patient_queue();
                objQueueBasic.mrm_id = mrm.mrm_id;
                objQueueBasic.mvt_id = mvt.mvt_id;
                objQueueBasic.mrd_id = null;
                objQueueBasic.tps_status = "NS";
                objQueueBasic.tps_ns_status = "WL";
                objQueueBasic.tps_create_by = Program.CurrentUser.mut_username;
                objQueueBasic.tps_create_date = dateNow;
                objQueueBasic.tps_update_by = Program.CurrentUser.mut_username;
                objQueueBasic.tps_update_date = dateNow;
                tpr.trn_patient_queues.Add(objQueueBasic);

                messegeSend = string.Format(Program.MsgSend, mrm.mrm_ename, mze.mze_ename, mhs.mhs_ename, tpr.tpr_queue_no);
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToBasic", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToScreening(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr("SC", Program.CurrentSite.mhs_code);
                mst_hpc_site mhs = mst.GetMstHpcSite(mrm.mhs_id);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("SC");
                return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToScreening", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToPE(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr("DC", Program.CurrentSite.mhs_code);
                mst_hpc_site mhs = mst.GetMstHpcSite(mrm.mhs_id);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("PE");
                return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToPE", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToCheckCBeforePE(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr("DC", Program.CurrentSite.mhs_code);
                mst_hpc_site mhs = mst.GetMstHpcSite(mrm.mhs_id);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("PE");
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm.mrm_id && x.mvt_id == mvt.mvt_id).FirstOrDefault();
                if (tps == null)
                {
                    tps = new trn_patient_queue();
                    tps.tps_create_by = Program.CurrentUser.mut_username;
                    tps.tps_create_date = dateNow;
                    tpr.trn_patient_queues.Add(tps);
                }
                tps.mrm_id = mrm.mrm_id;
                tps.mvt_id = mvt.mvt_id;
                tps.mrd_id = null;
                tps.tps_end_date = null;
                tps.tps_start_date = null;
                tps.tps_status = "NS";
                tps.tps_ns_status = "WP";
                tps.tps_update_by = Program.CurrentUser.mut_username;
                tps.tps_update_date = dateNow;
                messegeSend = string.Format(Program.MsgSend, mrm.mrm_ename, mze.mze_ename, mhs.mhs_ename, tpr.tpr_queue_no);
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToPE", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToCheckB(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr("CB", Program.CurrentSite.mhs_code);
                mst_hpc_site mhs = mst.GetMstHpcSite(mrm.mhs_id);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("CB");
                return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToRoom(ref trn_patient_regi tpr, ref string messegeSend, int mrm_id, int mvt_id)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_id);
                mst_hpc_site mhs = mst.GetMstHpcSite(mrm.mhs_id);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent(mvt_id);
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm.mrm_id && x.mvt_id == mvt.mvt_id).FirstOrDefault();
                if (tps == null)
                {
                    tps = new trn_patient_queue();
                    tps.tps_create_by = Program.CurrentUser.mut_username;
                    tps.tps_create_date = dateNow;
                    tpr.trn_patient_queues.Add(tps);
                }
                tps.mrm_id = mrm.mrm_id;
                tps.mvt_id = mvt.mvt_id;
                tps.mrd_id = null;
                tps.tps_end_date = null;
                tps.tps_start_date = null;
                tps.tps_status = "NS";
                tps.tps_ns_status = "QL";
                tps.tps_update_by = Program.CurrentUser.mut_username;
                tps.tps_update_date = dateNow;
                messegeSend = string.Format(Program.MsgSend, mrm.mrm_ename, mze.mze_ename, mhs.mhs_ename, tpr.tpr_queue_no);
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToRoom", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToDoctor(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr("DC", Program.CurrentSite.mhs_code);
                mst_hpc_site mhs = mst.GetMstHpcSite(mrm.mhs_id);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("DC");
                return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToDoctor", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToCheckC(ref trn_patient_regi tpr, ref string messegeSend, bool andSendBook = false)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_hpc_site mhs = mst.GetMstHpcSite(tpr.mhs_id);
                mst_room_hdr mrm = mst.GetMstRoomHdr("CC", mhs.mhs_code);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("CC");
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm.mrm_id && x.mvt_id == mvt.mvt_id).FirstOrDefault();

                //CheckC
                //trn_patient_queue updateQueue;
                //if (tps == null)
                //{
                //    updateQueue = new trn_patient_queue();
                //    updateQueue.tps_create_by = Program.CurrentUser.mut_username;
                //    updateQueue.tps_create_date = dateNow;
                //    tpr.trn_patient_queues.Add(updateQueue);
                //}
                //else
                //{
                //    updateQueue = tpr.trn_patient_queues.Where(x => x.tps_id == tps.tps_id).FirstOrDefault();
                //}
                //updateQueue.mrm_id = mrm.mrm_id;
                //updateQueue.mvt_id = mvt.mvt_id;
                //updateQueue.mrd_id = null;
                //updateQueue.tps_end_date = null;
                //updateQueue.tps_start_date = null;
                //if (andSendBook)
                //{
                //    updateQueue.tps_status = "ED";
                //    updateQueue.tps_ns_status = null;
                //}
                //else
                //{
                //    updateQueue.tps_status = "WK";
                //    updateQueue.tps_ns_status = null;
                //}
                //updateQueue.tps_update_by = Program.CurrentUser.mut_username;
                //updateQueue.tps_update_date = dateNow;

                if (tps == null)
                {
                    tps = new trn_patient_queue();
                    tps.tps_create_by = Program.CurrentUser.mut_username;
                    tps.tps_create_date = dateNow;
                    tpr.trn_patient_queues.Add(tps);
                }
                tps.mrm_id = mrm.mrm_id;
                tps.mvt_id = mvt.mvt_id;
                tps.mrd_id = null;
                tps.tps_end_date = null;
                tps.tps_start_date = null;
                if (andSendBook)
                {
                    tps.tps_status = "ED";
                    tps.tps_ns_status = null;
                }
                else
                {
                    tps.tps_status = "WK";
                    tps.tps_ns_status = null;
                }
                tps.tps_update_by = Program.CurrentUser.mut_username;
                tps.tps_update_date = dateNow;
                messegeSend = string.Format(Program.MsgSend, mrm.mrm_ename, mze.mze_ename, mhs.mhs_ename, tpr.tpr_queue_no);
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToCheckC", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToPHM(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr("PH", Program.CurrentSite.mhs_code);
                //mst_hpc_site mhs = mst.getMstHpcSite(mrm.mhs_id);
                //mst_zone mze = mst.getMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("PH");
                return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToPHM", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToBook(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                tpr.tpr_status = "WB";
                tpr.tpr_pe_status = "RS";
                tpr.tpr_update_by = Program.CurrentUser.mut_username;
                tpr.tpr_update_date = dateNow;
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_hpc_site mhs = mst.GetMstHpcSite(tpr.mhs_id);
                mst_room_hdr mrm = mst.GetMstRoomHdr("BK", mhs.mhs_code);
                mst_event mvt = mst.GetMstEvent("BK");
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm.mrm_id && x.mvt_id == mvt.mvt_id).FirstOrDefault();

                //book
                //trn_patient_queue updateQueue;
                //if (tps == null)
                //{
                //    updateQueue = new trn_patient_queue();
                //    updateQueue.tps_create_by = Program.CurrentUser.mut_username;
                //    updateQueue.tps_create_date = dateNow;
                //    tpr.trn_patient_queues.Add(updateQueue);
                //}
                //else
                //{
                //    updateQueue = tpr.trn_patient_queues.Where(x => x.tps_id == tps.tps_id).FirstOrDefault();
                //}
                //updateQueue.mrm_id = mrm.mrm_id;
                //updateQueue.mvt_id = mvt.mvt_id;
                //updateQueue.mrd_id = null;
                //updateQueue.tps_end_date = null;
                //updateQueue.tps_start_date = null;
                //updateQueue.tps_status = "NS";
                //updateQueue.tps_ns_status = "QL";
                //updateQueue.tps_update_by = Program.CurrentUser.mut_username;
                //updateQueue.tps_update_date = dateNow;

                if (tps == null)
                {
                    tps = new trn_patient_queue();
                    tps.tps_create_by = Program.CurrentUser.mut_username;
                    tps.tps_create_date = dateNow;
                    tpr.trn_patient_queues.Add(tps);
                }
                tps.mrm_id = mrm.mrm_id;
                tps.mvt_id = mvt.mvt_id;
                tps.mrd_id = null;
                tps.tps_end_date = null;
                tps.tps_start_date = null;
                tps.tps_status = "NS";
                tps.tps_ns_status = "QL";
                tps.tps_update_by = Program.CurrentUser.mut_username;
                tps.tps_update_date = dateNow;
                messegeSend = "Queue No. " + tpr.tpr_queue_no + " Send to Book Complete.";
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "SendToBook", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction ReturnToCheckB(ref trn_patient_regi tpr, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                tpr.tpr_pe_status = "RB";
                mst_room_hdr mrm = mst.GetMstRoomHdr("CB", Program.CurrentSite.mhs_code);
                mst_hpc_site mhs = mst.GetMstHpcSite(mrm.mhs_id);
                mst_zone mze = mst.GetMstZone((int)mrm.mze_id);
                mst_event mvt = mst.GetMstEvent("CB");
                return SendToRoom(ref tpr, ref messegeSend, mrm.mrm_id, mvt.mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "ReturnToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction endQueue(ref trn_patient_regi tpr)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return endQueue(ref tpr, tps_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "endQueue", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction endQueue(ref trn_patient_regi tpr, int tps_id)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                List<trn_patient_queue> tps = tpr.trn_patient_queues
                                                 .Where(x => x.tps_id == tps_id).ToList();
                tps.ForEach(x =>
                {
                    x.mrd_id = Program.CurrentRoom.mrd_id;
                    x.tps_send_by = Program.CurrentUser.mut_username;
                    x.tps_end_date = dateNow;
                    x.tps_status = "ED";
                    x.tps_ns_status = null;
                    x.tps_update_by = Program.CurrentUser.mut_username;
                    x.tps_update_date = dateNow;
                });
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "endQueue", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction endQueue(ref trn_patient_regi tpr, int mrm_id, List<int> list_mvt_id)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                endQueue(ref tpr, tps_id);
                DateTime dateNow = Program.GetServerDateTime();
                List<trn_patient_queue> tps = tpr.trn_patient_queues
                                                 .Where(x => x.tps_id == tps_id).ToList();
                tps.ForEach(x =>
                {
                    x.mvt_id = list_mvt_id.Min();
                });
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "endQueue", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction endQueue(ref trn_patient_regi tpr, string mrm_code)
        {
            try
            {
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_code);
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return endQueue(ref tpr, tps_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionQueueCls", "endQueue", ex, false);
                return StatusTransaction.Error;
            }
        }
    }
}
