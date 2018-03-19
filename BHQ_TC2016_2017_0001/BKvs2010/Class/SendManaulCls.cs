using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.Linq;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Class
{
    class SendManaulCls
    {
        private StatusTransaction SendManual(ref trn_patient_regi tpr, ref string messegeSend, string currentRoom)
        {
            try
            {
                StatusTransaction remainStation = new Class.FunctionDataCls().checkRemainEvent(tpr.tpr_id);
                if (remainStation == StatusTransaction.True)
                {
                    frmChoiceRoom frm = new frmChoiceRoom();
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        int mrm_id = frm.GetMrmID;
                        int mvt_id = frm.mvtID;
                        new TransactionQueueCls().SendToRoom(ref tpr, ref messegeSend, mrm_id, mvt_id);
                        return StatusTransaction.True;
                    }
                    else
                    {
                        return StatusTransaction.False;
                    }
                }
                else if (remainStation == StatusTransaction.False)
                {
                    if (tpr.tpr_PRM_check == true && tpr.tpr_pe_status == "RS" && currentRoom == "PT")
                    {
                        frmChoicePRM frm = new frmChoicePRM();
                        var result = frm.ShowDialog();
                        if (result == DialogResult.Yes)
                        {
                            if (Program.CurrentSite.mhs_extra_pe_type == true && tpr.tpr_pe_site2 == 'N' && (tpr.tpr_pd_pe_site2 == null || tpr.tpr_pd_pe_site2 == false))
                            {
                                new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend, true);
                                new TransactionQueueCls().SendToBook(ref tpr, ref messegeSend);
                                return StatusTransaction.True;
                            }
                            else
                            {
                                new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend);
                                return StatusTransaction.True;
                            }
                        }
                        else
                        {
                            new TransactionQueueCls().SendToBook(ref tpr, ref messegeSend);
                            return StatusTransaction.True;
                        }
                    }
                    else
                    {
                        if (Program.CurrentSite.mhs_extra_pe_type == true && tpr.tpr_pe_site2 == 'N' && (tpr.tpr_pd_pe_site2 == null || tpr.tpr_pd_pe_site2 == false))
                        {
                            new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend, true);
                            new TransactionQueueCls().SendToBook(ref tpr, ref messegeSend);
                            return StatusTransaction.True;
                        }
                        else
                        {
                            new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend);
                            return StatusTransaction.True;
                        }
                    }
                }
                else
                {
                    return StatusTransaction.Error;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManual", "SendManual", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendManualOnCheckB(int tpr_id, int mrm_id, ref string messageSend)
        {
            Program.RefreshWaiting = false;
            try
            {
                StatusTransaction checkRemain = new Class.FunctionDataCls().checkRemainEvent(tpr_id);
                if (checkRemain == StatusTransaction.True)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        try
                        {
                            cdc.Connection.Open();
                            DbTransaction tran = cdc.Connection.BeginTransaction();
                            cdc.Transaction = tran;

                            string currentRoom = new EmrClass.GetDataMasterCls().GetMstRoomHdrByMrd_id(Program.CurrentRoom.mrd_id).mrm_code;
                            trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            StatusTransaction result = SendManual(ref tpr, ref messageSend, currentRoom);
                            if (result == StatusTransaction.True)
                            {
                                StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, mrm_id);
                                if (checkPatientOnCheckB == StatusTransaction.True || checkPatientOnCheckB == StatusTransaction.NoProcess)
                                {
                                    new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                                    new TransactionQueueCls().endQueue(ref tpr);
                                    cdc.SubmitChanges();
                                    cdc.Transaction.Commit();
                                    return StatusTransaction.True;
                                }
                                else if (checkPatientOnCheckB == StatusTransaction.False)
                                {
                                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะ ที่จะ Send Manaul ได้", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return StatusTransaction.False;
                                }
                                else
                                {
                                    return checkPatientOnCheckB;
                                }
                            }
                            else
                            {
                                return result;
                            }
                        }
                        catch (Exception ex)
                        {
                            cdc.Transaction.Rollback();
                            Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                            return StatusTransaction.Error;
                        }
                        finally
                        {
                            cdc.Connection.Close();
                        }
                    }
                }
                else if (checkRemain == StatusTransaction.False)
                {
                    return new SendQueue().SendToCheckC(tpr_id, mrm_id, ref messageSend);
                }
                else
                {
                    return checkRemain;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                return StatusTransaction.Error;
            }
            finally
            {
                Program.RefreshWaiting = true;
            }
        }
        public StatusTransaction SendManualOnCheckB(ref string messageSend)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                return SendManualOnCheckB(tpr_id, mrm_id, ref messageSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendManualOnDoctor(int tpr_id, int mrm_id, int mvt_id, ref string messageSend)
        {
            try
            {
                mst_event mvt = new EmrClass.GetDataMasterCls().GetMstEvent(mvt_id);
                if (mvt.mvt_code == "DC")
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        try
                        {
                            cdc.Connection.Open();
                            DbTransaction tran = cdc.Connection.BeginTransaction();
                            cdc.Transaction = tran;

                            string currentRoom = new EmrClass.GetDataMasterCls().GetMstRoomHdrByMrd_id(Program.CurrentRoom.mrd_id).mrm_code;
                            trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            new TransactionPlanCls().endPlan(ref tpr, mvt_id);
                            new TransactionQueueCls().endQueue(ref tpr, mrm_id, new List<int> { mvt_id });
                            cdc.SubmitChanges();
                            cdc.Transaction.Commit();
                            return StatusTransaction.True;
                        }
                        catch (Exception ex)
                        {
                            cdc.Transaction.Rollback();
                            Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                            return StatusTransaction.Error;
                        }
                        finally
                        {
                            cdc.Connection.Close();
                        }
                    }
                }
                else
                {
                    return SendManualOnCheckB(tpr_id, mrm_id, ref messageSend);
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnDoctor", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendManualOnDoctor(ref string messageSend)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                int mvt_id = (int)Program.CurrentPatient_queue.mvt_id;
                return SendManualOnDoctor(tpr_id, mrm_id, mvt_id, ref messageSend); 
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnDoctor", ex, false);
                return StatusTransaction.Error;
            }
        }

        private StatusTransaction SendManualPendingCheckB(ref trn_patient_regi tpr, ref string messegeSend, string currentRoom)
        {
            try
            {
                StatusTransaction remainStation = new Class.FunctionDataCls().checkRemainEvent(tpr.tpr_id);
                if (remainStation == StatusTransaction.True)
                {
                    frmChoiceRoom frm = new frmChoiceRoom();
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        int mrm_id = frm.GetMrmID;
                        int mvt_id = frm.mvtID;
                        new TransactionQueueCls().SendToRoom(ref tpr, ref messegeSend, mrm_id, mvt_id);
                        return StatusTransaction.True;
                    }
                    else
                    {
                        return StatusTransaction.False;
                    }
                }
                else if (remainStation == StatusTransaction.False)
                {
                    if (tpr.tpr_PRM_check == true && tpr.tpr_pe_status == "RS" && currentRoom == "PT")
                    {
                        frmChoicePRM frm = new frmChoicePRM();
                        var result = frm.ShowDialog();
                        if (result == DialogResult.Yes)
                        {
                            if (Program.CurrentSite.mhs_extra_pe_type == true && tpr.tpr_pe_site2 == 'N' && (tpr.tpr_pd_pe_site2 == null || tpr.tpr_pd_pe_site2 == false))
                            {
                                new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend, true);
                                new TransactionQueueCls().SendToBook(ref tpr, ref messegeSend);
                                return StatusTransaction.True;
                            }
                            else
                            {
                                new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend);
                                return StatusTransaction.True;
                            }
                        }
                        else
                        {
                            new TransactionQueueCls().SendToBook(ref tpr, ref messegeSend);
                            return StatusTransaction.True;
                        }
                    }
                    else
                    {
                        if (Program.CurrentSite.mhs_extra_pe_type == true && tpr.tpr_pe_site2 == 'N' && (tpr.tpr_pd_pe_site2 == null || tpr.tpr_pd_pe_site2 == false))
                        {
                            new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend, true);
                            new TransactionQueueCls().SendToBook(ref tpr, ref messegeSend);
                            return StatusTransaction.True;
                        }
                        else
                        {
                            new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend);
                            return StatusTransaction.True;
                        }
                    }
                }
                else
                {
                    return StatusTransaction.Error;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManual", "SendManualPendingCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendManualOnStationPendingCheckB(ref string sendMessege)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                return SendManualOnStationPendingCheckB(tpr_id, mrm_id, ref sendMessege);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnStationPendingCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendManualOnStationPendingCheckB(int tpr_id, int mrm_id, ref string messegeSend)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                try
                {
                    string currentRoom = new EmrClass.GetDataMasterCls().GetMstRoomHdrByMrd_id(Program.CurrentRoom.mrd_id).mrm_code;
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    StatusTransaction result = SendManualPendingCheckB(ref tpr, ref messegeSend, currentRoom);
                    if (result == StatusTransaction.True)
                    {
                        tpr.tpr_pending_cancel_onday = false;

                        StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, mrm_id);
                        if (checkPatientOnCheckB == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr);
                            try
                            {
                                cdc.SubmitChanges();
                            }
                            catch (System.Data.Linq.ChangeConflictException)
                            {
                                foreach (ObjectChangeConflict occ in cdc.ChangeConflicts)
                                {
                                    cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                                }
                            }
                        }
                        else if (checkPatientOnCheckB == StatusTransaction.False)
                        {
                            MessageBox.Show("สถานะของคนไข้ ไม่ได้อยู่ในสถานะที่จะ Send Manual ได้", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return StatusTransaction.False;
                        }
                        new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                        return StatusTransaction.True;
                    }
                    else if (result == StatusTransaction.False)
                    {
                        StatusTransaction rePendingCB = new Class.FunctionDataCls().rePendingCheckB(ref tpr);
                        if (rePendingCB == StatusTransaction.Error)
                        {
                            return StatusTransaction.Error;
                        }
                        try
                        {
                            cdc.SubmitChanges();
                        }
                        catch (System.Data.Linq.ChangeConflictException)
                        {
                            foreach (ObjectChangeConflict occ in cdc.ChangeConflicts)
                            {
                                cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                            }
                        }
                        return StatusTransaction.False;
                    }
                    else
                    {
                        StatusTransaction rePendingCB = new Class.FunctionDataCls().rePendingCheckB(ref tpr);
                        if (rePendingCB == StatusTransaction.Error)
                        {
                            return StatusTransaction.Error;
                        }
                        try
                        {
                            cdc.SubmitChanges();
                        }
                        catch (System.Data.Linq.ChangeConflictException)
                        {
                            foreach (ObjectChangeConflict occ in cdc.ChangeConflicts)
                            {
                                cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                            }
                        }
                        return StatusTransaction.Error;
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("SendManaulCls", "SendManualOnStationPendingCheckB", ex, false);
                    return StatusTransaction.Error;
                }
            }
        }


        private StatusTransaction get_list_tpl_id(int tpr_id, int mrm_id, ref List<int> list_tpl_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<int> list_mvt_id = new List<int>();
                    mst_room_hdr mrm = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).FirstOrDefault();
                    switch (mrm.mrm_code)
                    {
                        case "EM":
                            if (Program.CurrentRoom.mrd_type == 'D')
                            {
                                mst_event mvt = cdc.mst_events.Where(x => x.mvt_code == "EM").FirstOrDefault();
                                if (mvt != null)
                                {
                                    list_mvt_id.Add(mvt.mvt_id);
                                }
                                else
                                {
                                    return StatusTransaction.Error;
                                }
                            }
                            else if (Program.CurrentRoom.mrd_type == 'N')
                            {
                                mst_event mvt = cdc.mst_events.Where(x => x.mvt_code == "EN").FirstOrDefault();
                                if (mvt != null)
                                {
                                    list_mvt_id.Add(mvt.mvt_id);
                                }
                                else
                                {
                                    return StatusTransaction.Error;
                                }
                            }
                            break;
                        case "TE":
                            if (Program.CurrentRoom.mrd_type == 'D')
                            {
                                mst_event mvt = cdc.mst_events.Where(x => x.mvt_code == "TX").FirstOrDefault();
                                if (mvt != null)
                                {
                                    list_mvt_id.Add(mvt.mvt_id);
                                }
                                else
                                {
                                    return StatusTransaction.Error;
                                }
                            }
                            else if (Program.CurrentRoom.mrd_type == 'N')
                            {
                                mst_event mvt = cdc.mst_events.Where(x => x.mvt_code == "TE").FirstOrDefault();
                                if (mvt != null)
                                {
                                    list_mvt_id.Add(mvt.mvt_id);
                                }
                                else
                                {
                                    return StatusTransaction.Error;
                                }
                            }
                            break;
                        default:
                            List<mst_room_event> list_room_event = cdc.mst_room_events.Where(x => x.mrm_id == mrm_id).ToList();
                            if (list_room_event != null && list_room_event.Count > 0)
                            {
                                list_mvt_id = list_room_event.Select(x => x.mvt_id).ToList();
                            }
                            else
                            {
                                return StatusTransaction.Error;
                            }
                            break;
                    }

                    List<trn_patient_plan> list_patient_plan = cdc.trn_patient_plans.Where(x => x.tpr_id == tpr_id && list_mvt_id.Contains(x.mvt_id)).ToList();
                    if (list_patient_plan != null && list_patient_plan.Count() > 0)
                    {
                        list_tpl_id = list_patient_plan.Select(x => x.tpl_id).ToList();
                        return StatusTransaction.True;
                    }
                    else
                    {
                        return StatusTransaction.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "get_list_tpl_id", ex, false);
                return StatusTransaction.Error;
            }
        }
        private StatusTransaction get_send_to_tps_id(int tpr_id, int mrm_id, int mvt_id, ref int? tps_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        trn_patient_queue tps = cdc.trn_patient_queues
                                                   .Where(x => x.tpr_id == tpr_id &&
                                                               x.mrm_id == mrm_id &&
                                                               x.mvt_id == mvt_id)
                                                   .FirstOrDefault();
                        if (tps != null)
                        {
                            tps_id = tps.tps_id;
                        }
                        else
                        {
                            tps_id = null;
                        }
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("SendManaulCls", "get_send_to_tps_id", ex, false);
                        return StatusTransaction.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "get_send_to_tps_id", ex, false);
                return StatusTransaction.Error;
            }
        }
        private StatusTransaction SendToStation(int tpr_id, int? end_tps_id, int end_mrm_id, int send_mrm_id, int send_mvt_id, ref string messegeSend)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction tran = cdc.Connection.BeginTransaction();
                        cdc.Transaction = tran;

                        int? send_tps_id = null;

                        StatusTransaction get_tps = get_send_to_tps_id(tpr_id, send_mrm_id, send_mvt_id, ref send_tps_id);
                        trn_patient_queue send_patient_queue;
                        if (get_tps == StatusTransaction.True)
                        {
                            if (send_tps_id != null)
                            {
                                send_patient_queue = cdc.trn_patient_queues.Where(x => x.tps_id == send_tps_id).FirstOrDefault();
                                send_patient_queue.mrm_id = send_mrm_id;
                                send_patient_queue.mvt_id = send_mvt_id;
                                send_patient_queue.mrd_id = null;
                                send_patient_queue.tps_end_date = null;
                                send_patient_queue.tps_start_date = null;
                                send_patient_queue.tps_status = "NS";
                                send_patient_queue.tps_ns_status = "QL";
                                send_patient_queue.tps_update_by = Program.CurrentUser.mut_username;
                                send_patient_queue.tps_update_date = dateNow;
                            }
                            else
                            {
                                send_patient_queue = new trn_patient_queue();
                                send_patient_queue.tpr_id = tpr_id;
                                send_patient_queue.tps_create_by = Program.CurrentUser.mut_username;
                                send_patient_queue.tps_create_date = dateNow;
                                send_patient_queue.mrm_id = send_mrm_id;
                                send_patient_queue.mvt_id = send_mvt_id;
                                send_patient_queue.mrd_id = null;
                                send_patient_queue.tps_end_date = null;
                                send_patient_queue.tps_start_date = null;
                                send_patient_queue.tps_status = "NS";
                                send_patient_queue.tps_ns_status = "QL";
                                send_patient_queue.tps_update_by = Program.CurrentUser.mut_username;
                                send_patient_queue.tps_update_date = dateNow;
                                cdc.trn_patient_queues.InsertOnSubmit(send_patient_queue);

                            }
                        }
                        else
                        {
                            messegeSend = "เกิดความผิดพลาดของระบบ กรุณากดส่งอีกครั้ง";
                            return StatusTransaction.Error;
                        }

                        List<int> list_tpl_id = new List<int>();
                        StatusTransaction get_tpl = get_list_tpl_id(tpr_id, end_mrm_id, ref list_tpl_id);
                        if (get_tpl == StatusTransaction.True)
                        {
                            if (list_tpl_id != null && list_tpl_id.Count() > 0)
                            {
                                List<trn_patient_plan> list_patient_plan = cdc.trn_patient_plans.Where(x => list_tpl_id.Contains(x.tpl_id)).ToList();
                                list_patient_plan.ForEach(x => x.tpl_status = 'P');
                            }
                            else
                            {
                                messegeSend = "เกิดความผิดพลาดของระบบ กรุณากดส่งอีกครั้ง";
                                return StatusTransaction.Error;
                            }
                        }
                        else
                        {
                            return StatusTransaction.Error;
                        }

                        if (end_tps_id != null && end_tps_id > 0)
                        {
                            trn_patient_queue end_patient_queue = cdc.trn_patient_queues.Where(x => x.tps_id == end_tps_id).FirstOrDefault();
                            if (end_patient_queue != null)
                            {
                                end_patient_queue.mrd_id = Program.CurrentRoom.mrd_id;
                                end_patient_queue.tps_send_by = Program.CurrentUser.mut_username;
                                end_patient_queue.tps_end_date = dateNow;
                                end_patient_queue.tps_status = "ED";
                                end_patient_queue.tps_ns_status = null;
                                end_patient_queue.tps_update_by = Program.CurrentUser.mut_username;
                                end_patient_queue.tps_update_date = dateNow;
                            }
                            else
                            {
                                messegeSend = "เกิดความผิดพลาดของระบบ กรุณากดส่งอีกครั้ง";
                                return StatusTransaction.Error;
                            }
                        }
                        else
                        {
                            messegeSend = "เกิดความผิดพลาดของระบบ กรุณากดส่งอีกครั้ง";
                            return StatusTransaction.Error;
                        }

                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        messegeSend = new Class.FunctionDataCls().getStringGotoNextRoom(tpr_id);
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendManaulCls", "SendToRoom", ex, false);
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
                Program.MessageError("SendManaulCls", "SendToRoom", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendManualOnStation(ref string sendMessege)
        {
            try
            {
                frmChoiceRoom frm = new frmChoiceRoom();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int tpr_id = Program.CurrentRegis.tpr_id;
                    int end_tps_id = Program.CurrentPatient_queue.tps_id;
                    int end_mrm_id = Program.CurrentRoom.mrm_id;
                    int send_mrm_id = frm.GetMrmID;
                    int send_mvt_id = frm.mvtID;
                    return SendToStation(tpr_id, end_tps_id, end_mrm_id, send_mrm_id, send_mvt_id, ref sendMessege);
                }
                else
                {
                    return StatusTransaction.False;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendManualOnStation(List<int> list_mvt_id, ref string messageSend)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                return SendManualOnStation(tpr_id, mrm_id, list_mvt_id, ref messageSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendManualOnStation(int tpr_id, int mrm_id, List<int> list_mvt_id, ref string messegeSend)
        {
            Program.RefreshWaiting = false;
            try
            {
                StatusTransaction checkRemain = new Class.FunctionDataCls().checkRemainEvent(tpr_id);
                if (checkRemain == StatusTransaction.True)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        try
                        {
                            cdc.Connection.Open();
                            DbTransaction tran = cdc.Connection.BeginTransaction();
                            cdc.Transaction = tran;

                            string currentRoom = new EmrClass.GetDataMasterCls().GetMstRoomHdrByMrd_id(Program.CurrentRoom.mrd_id).mrm_code;
                            trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            StatusTransaction result = SendManual(ref tpr, ref messegeSend, currentRoom);
                            if (result == StatusTransaction.True)
                            {
                                new TransactionPlanCls().endPlan(ref tpr, list_mvt_id);
                                new TransactionQueueCls().endQueue(ref tpr, mrm_id, list_mvt_id);
                                cdc.SubmitChanges();
                                cdc.Transaction.Commit();
                                return StatusTransaction.True;
                            }
                            else
                            {
                                return result;
                            }
                        }
                        catch (Exception ex)
                        {
                            cdc.Transaction.Rollback();
                            Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                            return StatusTransaction.Error;
                        }
                        finally
                        {
                            cdc.Connection.Close();
                        }
                    }
                }
                else if (checkRemain == StatusTransaction.False)
                {
                    return new SendQueue().SendToCheckC(tpr_id, mrm_id, ref messegeSend);
                }
                else
                {
                    return checkRemain;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendManaulCls", "SendManualOnStation", ex, false);
                return StatusTransaction.Error;
            }
            finally
            {
                Program.RefreshWaiting = true;
            }
        }
    }
}
