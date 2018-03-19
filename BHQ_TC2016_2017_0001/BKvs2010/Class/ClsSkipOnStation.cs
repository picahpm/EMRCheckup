using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Data.Common;
using System.Data.Linq;

namespace BKvs2010.Class
{
    class ClsSkipOnStation
    {
        public StatusTransaction skipOnWaitingSendManaul(int tpr_id, int tps_id, int next_mrm_id, int next_mvt_id)
        {
            try
            {
                InhCheckupDataContext cdc = new InhCheckupDataContext();

                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();

                if (tps != null && tps.tps_status == "NS" && tps.tps_ns_status == "QL")
                {
                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    updatePlanForSkip(ref tpr, tps_id);
                    skipReserveCurrentQueue(ref tpr, tps_id);
                    func.deleteCurrentQueue(ref cdc, ref tpr, tps_id);
                    func.insertNextQueue(ref tpr, next_mrm_id, next_mvt_id);
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
                Program.MessageError("ClsSkipOnStation", "skipOnWaitingSendManaul", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction skipOnStationSendManaul(int tpr_id, int tps_id, int next_mrm_id, int next_mvt_id)
        {
            try
            {
                InhCheckupDataContext cdc = new InhCheckupDataContext();
                
                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();

                Class.FunctionDataCls func = new Class.FunctionDataCls();
                updatePlanForSkip(ref tpr, tps_id);
                skipReserveCurrentQueue(ref tpr, tps_id);
                func.deleteCurrentQueue(ref cdc, ref tpr, tps_id);
                func.insertNextQueue(ref tpr, next_mrm_id, next_mvt_id);
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
                Program.MessageError("ClsSkipOnStation", "skipOnStationSendManaul", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction skipOnStationSendManaul(int next_mrm_id, int next_mvt_id)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int mvt_id = (int)Program.CurrentPatient_queue.mvt_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                int mhs_id = Program.CurrentSite.mhs_id;
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return skipOnStationSendManaul(tpr_id, tps_id, next_mrm_id, next_mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsSkipOnStation", "skipOnStationSendManaul", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction skipOnStationSendAuto(int tpr_id, int mrm_id, int tps_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
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
                                    process_type = "S",
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
                        StatusTransaction result = CallQueue.PSendAutoAllRoom(Class.SendType.Skip, mrm_id, list_mvt_id, tps_id);
                        if (result == StatusTransaction.SendCheckB)
                        {
                            if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                            {
                                return StatusTransaction.ReSendManualSite2;
                            }
                            else
                            {
                                string messege = "";
                                StatusTransaction returnB = new Class.SendQueue().ReturnToCheckB(Class.SendType.Skip, tpr_id, mrm_id, tps_id, list_mvt_id, ref messege);
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
                    catch (Exception ex)
                    {
                        Program.MessageError("ClsSkipOnStation", "skipOnStationSendAuto", ex, false);
                        return StatusTransaction.Error;
                    }
                }

                //if (!CallQueue.PSendAutoAllRoom(true))
                //{
                //    return false;
                //}
                //trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                //Class.FunctionDataCls func = new Class.FunctionDataCls();
                //updatePlanForSkip(ref tpr, tps_id);
                //func.deleteCurrentQueue(ref cdc, ref tpr, tps_id);
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
            catch
            {
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction skipOnStationSendAuto()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;
            int mrm_id = Program.CurrentRoom.mrm_id;
            return skipOnStationSendAuto(tpr_id, mrm_id, tps_id);
        }

        public void updatePlanForSkip(ref trn_patient_regi tpr, int tps_id)
        {
            trn_patient_queue currentQueue = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            List<mst_room_event> mvt = mst.GetMstRoomEventByMrm((int)currentQueue.mrm_id);
            List<trn_patient_plan> currentPlan = tpr.trn_patient_plans.Where(x => mvt.Select(y => y.mvt_id).Contains(x.mvt_id)).ToList();
            currentPlan.ForEach(x =>
            {
                x.tpl_skip = true;
                x.tpl_skip_seq = x.tpl_skip_seq == null ? 1 : x.tpl_skip_seq + 1;
            });
        }

        public void updatePlanForSkipByMvt(ref trn_patient_regi tpr, List<int> list_mvt_id)
        {
            List<trn_patient_plan> currentPlan = tpr.trn_patient_plans.Where(x => list_mvt_id.Contains(x.mvt_id)).ToList();
            currentPlan.ForEach(x =>
            {
                x.tpl_skip = true;
                x.tpl_skip_seq = x.tpl_skip_seq == null ? 1 : x.tpl_skip_seq + 1;
            });
        }

        public void updatePlanForSkipByMvt(ref trn_patient_regi tpr, int mvt_id)
        {
            trn_patient_plan currentPlan = tpr.trn_patient_plans.Where(x => x.mvt_id == mvt_id).FirstOrDefault();
            currentPlan.tpl_skip = true;
            currentPlan.tpl_skip_seq = currentPlan.tpl_skip_seq == null ? 1 : currentPlan.tpl_skip_seq + 1;
        }

        private void skipReserveCurrentQueue(ref trn_patient_regi tpr, int tps_id)
        {
            trn_patient_skip_queue PatientSkipQueue = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id &&
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
                tpr.trn_patient_skip_queues.Add(PatientSkipQueue);
            }
        }
    }
}
