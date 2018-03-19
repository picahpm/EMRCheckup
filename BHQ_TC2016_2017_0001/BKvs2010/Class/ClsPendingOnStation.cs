using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Data.Linq;
using System.Data.Common;

namespace BKvs2010.Class
{
    class ClsPendingOnStation
    {
        public void updatePendingPatientPlan(ref trn_patient_regi tpr, int mvt_id, int mhs_id)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_room_hdr mrm = mst.GetMstRoomHdrByMvt(mvt_id, mhs_id);
            updatePendingPatientPlan(ref tpr, mrm.mrm_id, mvt_id, mhs_id);
        }
        public void updatePendingPatientPlan(ref trn_patient_regi tpr, string mvt_code, int mhs_id)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_event mvt = mst.GetMstEvent(mvt_code);
            mst_room_hdr mrm = mst.GetMstRoomHdrByMvt(mvt.mvt_id, mhs_id);
            updatePendingPatientPlan(ref tpr, mrm.mrm_id, mvt.mvt_id, mhs_id);
        }
        public void updatePendingPatientPlan(ref trn_patient_regi tpr, string mvt_code, int mrm_id, int mhs_id)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_event mvt = mst.GetMstEvent(mvt_code);
            updatePendingPatientPlan(ref tpr, mrm_id, mvt.mvt_id, mhs_id);
        }
        public void updatePendingPatientPlan(ref trn_patient_regi tpr, string mvt_code, string mrm_code, int mhs_id)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_event mvt = mst.GetMstEvent(mvt_code);
            mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_code);
            updatePendingPatientPlan(ref tpr, mrm.mrm_id, mvt.mvt_id, mhs_id);
        }
        public void updatePendingPatientPlan(ref trn_patient_regi tpr, int mvt_id, int mrm_id, int mhs_id)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_id);
            List<int> list_mvt_id;
            if (mrm.mrm_code == "TE")
            {
                list_mvt_id = new List<int> { mvt_id };
            }
            else
            {
                list_mvt_id = mst.GetMstRoomEventByMrm(mrm_id).Select(x => x.mvt_id).ToList();
            }
            List<trn_patient_plan> tpl = tpr.trn_patient_plans
                                            .Where(x => list_mvt_id.Contains(x.mvt_id)).ToList();
            tpl.ForEach(x => x.tpl_status = 'C');
        }

        public void updatePendingPatientQueue(ref InhCheckupDataContext cdc, ref trn_patient_regi tpr, string mrm_code)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_code);
            updatePendingPatientQueue(ref cdc, ref tpr, mrm.mrm_id);
        }
        public void updatePendingPatientQueue(ref InhCheckupDataContext cdc, ref trn_patient_regi tpr, int mrm_id)
        {
            List<trn_patient_queue> tps = tpr.trn_patient_queues
                                             .Where(x => x.mrm_id == mrm_id).ToList();
            Class.FunctionDataCls func = new Class.FunctionDataCls();
            if (func.checkPassedCheckPointB(tpr.tpr_id))
            {
                cdc.trn_patient_queues.DeleteAllOnSubmit(tps);
                //tps.ForEach(x => x.tps_status = "CL");
            }
            else
            {
                tps.ForEach(x => x.tps_status = "PD");
            }
        }

        public void insertPatientPending(ref trn_patient_regi tpr, int mrm_id)
        {
            DateTime dateNow = Program.GetServerDateTime();
            List<trn_patient_pending> tpp = tpr.trn_patient_pendings
                                               .Where(x => x.tpp_status == 'P'
                                                        && x.mrm_id == mrm_id).ToList();
            if (tpp.Count() == 0)
            {
                trn_patient_pending tps = new trn_patient_pending
                {
                    mrm_id = mrm_id,
                    tpp_status = 'P',
                    tpp_create_date = dateNow,
                    tpp_update_date = dateNow
                };
                tpr.trn_patient_pendings.Add(tps);
            }
        }
        public void insertPatientPending(ref trn_patient_regi tpr, string mrm_code)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_code);
            insertPatientPending(ref tpr, mrm.mrm_id);
        }

        public StatusTransaction pendingOnStationSendManaul(int tpr_id, int mvt_id, int mrm_id, int mhs_id, int next_mrm_id, int next_mvt_id, int tps_id)
        {
            try
            {
                InhCheckupDataContext cdc = new InhCheckupDataContext();

                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();

                tpr.tpr_pending = true;
                updatePendingPatientPlan(ref tpr, mvt_id, mrm_id, mhs_id);
                updatePendingPatientQueue(ref cdc, ref tpr, mrm_id);
                insertPatientPending(ref tpr, mrm_id);
                Class.FunctionDataCls func = new Class.FunctionDataCls();
                func.insertNextQueue(ref tpr, next_mrm_id, next_mvt_id);
                // alert
                try
                {
                    cdc.SubmitChanges();
                    return StatusTransaction.True;
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (ObjectChangeConflict occ in cdc.ChangeConflicts)
                    {
                        cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    cdc.SubmitChanges();
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsPendingOnStation", "pendingOnStationSendManaul", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction pendingOnStationSendManaul(int next_mrm_id, int next_mvt_id)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int mvt_id = (int)Program.CurrentPatient_queue.mvt_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                int mhs_id = Program.CurrentSite.mhs_id;
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return pendingOnStationSendManaul(tpr_id, mvt_id, mrm_id, mhs_id, next_mrm_id, next_mvt_id, tps_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsPendingOnStation", "pendingOnStationSendManaul", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction pendingOnWaitingSendManaul(int tpr_id, int mvt_id, int mrm_id, int mhs_id, int next_mrm_id, int next_mvt_id, int tps_id)
        {
            try
            {
                InhCheckupDataContext cdc = new InhCheckupDataContext();

                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();

                if (tps != null && tps.tps_status == "NS" && tps.tps_ns_status == "QL")
                {
                    tpr.tpr_pending = true;
                    updatePendingPatientPlan(ref tpr, mvt_id, mrm_id, mhs_id);
                    updatePendingPatientQueue(ref cdc, ref tpr, mrm_id);
                    insertPatientPending(ref tpr, mrm_id);
                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    func.insertNextQueue(ref tpr, next_mrm_id, next_mvt_id);
                    // alert
                    try
                    {
                        cdc.SubmitChanges();
                        return StatusTransaction.True;
                    }
                    catch (System.Data.Linq.ChangeConflictException)
                    {
                        return StatusTransaction.NoProcess;
                    }
                }
                else
                {
                    return StatusTransaction.NoProcess;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsPendingOnStation", "pendingOnWaitingSendManaul", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction pendingOnStationSendAuto(int tpr_id, int mvt_id, int mrm_id, int mhs_id, int tps_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    Class.clsLogSendAuto logCls = new Class.clsLogSendAuto();
                    List<log_send_auto> log_auto = null;
                    try
                    {
                        List<vw_patient_room> vw_pa_room = logCls.getLogSendAuto(tpr_id);
                        if (vw_pa_room != null)
                        {
                            log_auto = vw_pa_room.Select(x => new log_send_auto
                            {
                                tpr_id = x.tpr_id,
                                process_type = "P",
                                process_tps_id = tps_id,
                                login_flag = x.login_flag,
                                mhs_code = x.mhs_code,
                                mhs_ename = x.mhs_ename,
                                mrm_ename = x.mrm_ename,
                                mrm_id = x.mrm_id,
                                mrm_seq_room = x.mrm_seq_room,
                                mvt_code = x.mvt_code,
                                mvt_id = x.mvt_id,
                                mvt_type_cate = x.mvt_type_cate.ToString(),
                                mze_code = x.mze_code,
                                mze_ename = x.mze_ename,
                                mze_id = x.mze_id,
                                patient_vip = x.patient_vip,
                                send_type = x.send_type,
                                site_rm = x.site_rm,
                                skip_seq = x.skip_seq,
                                skip_type = x.skip_type,
                                waiting_person = x.waiting_person,
                                waiting_time = x.waiting_time
                            }).ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("log_send_auto", "", ex, false);
                    }

                    List<int> list_mvt_id = new Class.FunctionDataCls().get_mvt_id(mrm_id);
                    StatusTransaction result = CallQueue.PSendAutoAllRoom(Class.SendType.Pending, mrm_id, list_mvt_id, tps_id);
                    if (result == StatusTransaction.SendCheckB)
                    {
                        if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                        {
                            return StatusTransaction.ReSendManualSite2;
                        }
                        else
                        {
                            string messege = "";
                            StatusTransaction returnB = new Class.SendQueue().ReturnToCheckB(Class.SendType.Pending, tpr_id, mrm_id, tps_id, list_mvt_id, ref messege);
                            if (returnB == StatusTransaction.True)
                            {
                                //new ClsTCPClient().sendClearUnitDisplay();
                                //new Class.FunctionDataCls().SendToCheckPointB(tpr_id, tps_id);
                            }
                            System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ ผู้รับบริการจะถูกส่งไป Check point B", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return returnB;
                        }
                    }
                    else if (result == StatusTransaction.True)
                    {
                        try
                        {
                            if (log_auto != null)
                            {
                                string queue_no = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.tpr_queue_no).FirstOrDefault();
                                int? log_tps_id = logCls.get_tps_id(tpr_id);
                                int index = 1;
                                log_auto.ForEach(x => { x.tps_id = log_tps_id; x.seq_id = index++; x.tpr_queue_no = queue_no; });
                                cdc.log_send_autos.InsertAllOnSubmit(log_auto);
                                cdc.SubmitChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError("log_send_auto", "", ex, false);
                        }
                        return result;
                    }
                    else
                    {
                        return result;
                    }
                }
                //InhCheckupDataContext cdc = new InhCheckupDataContext();

                //trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                //tpr.tpr_pending = true;
                //updatePendingPatientPlan(ref tpr, mvt_id, mrm_id, mhs_id);
                //cdc.SubmitChanges();
                //updatePendingPatientQueue(ref cdc, ref tpr, mrm_id);
                //insertPatientPending(ref tpr, mrm_id);
                //CallQueue.PSendAutoAllRoom(true);
                //// alert
                //try
                //{
                //    cdc.SubmitChanges();
                //}
                //catch (System.Data.Linq.ChangeConflictException)
                //{
                //    foreach (ObjectChangeConflict occ in cdc.ChangeConflicts)
                //    {
                //        cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                //    }
                //    cdc.SubmitChanges();
                //}
                //return true;
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsPendingOnStation", "pendingOnStationSendAuto", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction pendingOnStationSendAuto()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int mvt_id = (int)Program.CurrentPatient_queue.mvt_id;
            int mrm_id = Program.CurrentRoom.mrm_id;
            int mhs_id = Program.CurrentSite.mhs_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;
            return pendingOnStationSendAuto(tpr_id, mvt_id, mrm_id, mhs_id, tps_id);
        }

        //public bool pendingPatientCheckpointB(int tpr_id, int mhs_id, List<int> list_mvt_id)
        //{
        //    try
        //    {
        //        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
        //        {
        //            trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
        //            tpr.tpr_pending = true;
        //            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
        //            foreach (int mvt_id in list_mvt_id)
        //            {
        //                mst_room_hdr mrm = mst.getMstRoomHdrByMvt(mvt_id, mhs_id);
        //                updatePendingPatientPlan(ref tpr, mvt_id, mrm.mrm_id);
        //                insertPatientPending(ref tpr, mrm.mrm_id);
        //            }
        //            cdc.SubmitChanges();
        //            return true;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public StatusTransaction pendingPatientCheckpointB(List<int> list_mvt_id)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int mhs_id = Program.CurrentSite.mhs_id;
                return pendingPatientCheckpointB(tpr_id, mhs_id, list_mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsPendingOnStation", "pendingPatientCheckpointB", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction pendingPatientCheckpointB(int tpr_id, int mhs_id, List<int> list_mvt_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    tpr.tpr_pending = true;
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    foreach (int mvt_id in list_mvt_id)
                    {
                        mst_room_hdr mrm = mst.GetMstRoomHdrByMvt(mvt_id, mhs_id);
                        updatePendingPatientPlan(ref tpr, mvt_id, mrm.mrm_id, mhs_id);
                        insertPatientPending(ref tpr, mrm.mrm_id);
                    }
                    cdc.SubmitChanges();
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsPendingOnStation", "pendingPatientCheckpointB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public bool pendingPatientBeforeCheckpointB(int tpr_id, int mrm_id)
        {
            try
            {
                InhCheckupDataContext cdc = new InhCheckupDataContext();
                    
                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                tpr.tpr_pending = true;
                updatePendingPatientQueue(ref cdc, ref tpr, mrm_id);
                // alert cancel
                cdc.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool pendingPatientBeforeCheckpointB()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int mrm_id = Program.CurrentRoom.mrm_id;
            return pendingPatientBeforeCheckpointB(tpr_id, mrm_id);
        }

        public StatusTransaction pendingDoctor(int tpr_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        if (tpr != null)
                        {
                            DateTime dtnow = Program.GetServerDateTime();
                            int mrm_id = Program.CurrentRoom.mrm_id;
                            int mrd_id = Program.CurrentRoom.mrd_id;
                            trn_patient_queue queueCheckC = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_id).FirstOrDefault();

                            queueCheckC.tps_update_by = Program.CurrentUser.mut_username;
                            queueCheckC.tps_update_date = dtnow;
                            queueCheckC.tps_status = "ED";

                            tpr.tpr_pending = true;
                            tpr.tpr_pending_no_station = true;
                            cdc.SubmitChanges();
                            cdc.Transaction.Commit();
                            messegeSend = "Queue " + tpr.tpr_queue_no + " ค้างตรวจแพทย์เสร็จสิ้น";
                            return StatusTransaction.True;
                        }
                        else
                        {
                            messegeSend = "เกิดความผิดพลาดของระบบ ไม่สามารถค้างตรวจแพทย์ได้ กรุณาดำเนินการอีกครั้ง";
                            return StatusTransaction.Error;
                        }
                    }
                    catch (Exception ex)
                    {
                        messegeSend = "เกิดความผิดพลาดของระบบ ไม่สามารถค้างตรวจแพทย์ได้ กรุณาดำเนินการอีกครั้ง";
                        cdc.Transaction.Rollback();
                        Program.MessageError("ClsPendingOnStation", "pendingDoctor", ex, false);
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
                messegeSend = "เกิดความผิดพลาดของระบบ ไม่สามารถค้างตรวจแพทย์ได้ กรุณาดำเนินการอีกครั้ง";
                Program.MessageError("ClsPendingOnStation", "pendingDoctor", ex, false);
                return StatusTransaction.Error;
            }
        }
    }
}
