using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using DBCheckup;

namespace BKvs2010.Class
{
    class SendQueue
    {
        public StatusTransaction SendToBasic(int tpr_id, int mrm_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        StatusTransaction result = new TransactionQueueCls().SendToBasic(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
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
                        Program.MessageError("SendQueue", "SendToBasic", ex, false);
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
                Program.MessageError("SendQueue", "SendToBasic", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToScreening(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return SendToScreening(tpr_id, mrm_id, tps_id, ref messegeSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToScreening", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToScreening(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        StatusTransaction result = new TransactionQueueCls().SendToScreening(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToScreening", ex, false);
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
                Program.MessageError("SendQueue", "SendToScreening", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToCheckB(int tpr_id, List<int> mvt_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        StatusTransaction result = new TransactionQueueCls().SendToCheckB(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mvt_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToCheckB", ex, false);
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
                Program.MessageError("SendQueue", "SendToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToCheckB(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return SendToCheckB(tpr_id, mrm_id, tps_id, ref messegeSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToCheckB(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        StatusTransaction result = new TransactionQueueCls().SendToCheckB(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToCheckB", ex, false);
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
                Program.MessageError("SendQueue", "SendToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToCheckBFromScreening(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return SendToCheckBFromScreening(tpr_id, mrm_id, tps_id, ref messegeSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToCheckBFromScreening(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        List<int> list_pe_mvt_id = cdc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).ToList();
                        StatusTransaction result = new TransactionQueueCls().SendToCheckB(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            List<trn_patient_queue> queue_pe = tpr.trn_patient_queues.Where(x => list_pe_mvt_id.Contains((int)x.mvt_id)).ToList();
                            if (queue_pe != null && queue_pe.Count() > 0)
                            {
                                new TransactionPlanCls().endPlan(ref tpr, list_pe_mvt_id);
                            }
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToCheckB", ex, false);
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
                Program.MessageError("SendQueue", "SendToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction ReturnToCheckB(SendType type, int tpr_id, int mrm_id, int tps_id, List<int> list_mvt_id, ref string messegeSend)
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
                        StatusTransaction result = new TransactionQueueCls().ReturnToCheckB(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
                            if (tps != null)
                            {
                                if (type == Class.SendType.Normal)
                                {
                                    new TransactionQueueCls().endQueue(ref tpr, tps_id);
                                }
                                else if (type == Class.SendType.Skip)
                                {
                                    cdc.trn_patient_queues.DeleteOnSubmit(tps);
                                }
                                else if (type == Class.SendType.Pending)
                                {
                                    tps.tps_status = "PD";
                                }
                                else
                                {
                                    return StatusTransaction.Error;
                                }
                            }
                            else
                            {
                                return StatusTransaction.Error;
                            }
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
                        Program.MessageError("SendQueue", "ReturnToCheckB", ex, false);
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
                Program.MessageError("SendQueue", "ReturnToCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToPE(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return SendToPE(tpr_id, mrm_id, tps_id, ref messegeSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToPE", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToPE(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        int site = tpr.tpr_site_use == null ? tpr.mhs_id : (int)tpr.tpr_site_use;
                        bool? use_before_pe = (from mvt in cdc.mst_events
                                               join mpr in cdc.mst_plan_rooms
                                               on mvt.mvt_id equals mpr.mvt_id
                                               join mrm in cdc.mst_room_hdrs
                                               on mpr.mrm_id equals mrm.mrm_id
                                               where mvt.mvt_code == "PE" &&
                                                     mpr.mhs_id == site
                                               select mrm.mst_hpc_site.mhs_use_before_pe)
                                              .FirstOrDefault();
                        if (use_before_pe == true)
                        {
                            StatusTransaction result = new TransactionQueueCls().SendToCheckCBeforePE(ref tpr, ref messegeSend);
                            if (result == StatusTransaction.True)
                            {
                                new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                                new TransactionQueueCls().endQueue(ref tpr, tps_id);
                                cdc.SubmitChanges();
                                cdc.Transaction.Commit();
                                return StatusTransaction.True;
                            }
                            else
                            {
                                return result;
                            }
                        }
                        else
                        {
                            StatusTransaction result = new TransactionQueueCls().SendToPE(ref tpr, ref messegeSend);
                            if (result == StatusTransaction.True)
                            {
                                new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                                new TransactionQueueCls().endQueue(ref tpr, tps_id);
                                cdc.SubmitChanges();
                                cdc.Transaction.Commit();
                                return StatusTransaction.True;
                            }
                            else
                            {
                                return result;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("SendQueue", "SendToPE", ex, false);
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
                Program.MessageError("SendQueue", "SendToPE", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToCheckC(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                StatusTransaction sendC = SendToCheckC(tpr_id, mrm_id, tps_id, ref messegeSend);
                if (sendC == StatusTransaction.True)
                {
                    callLab(tpr_id);
                }
                return sendC;
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToCheckC", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToCheckC(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        StatusTransaction result = new TransactionQueueCls().SendToCheckC(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToCheckC", ex, false);
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
                Program.MessageError("SendQueue", "SendToCheckC", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToResult(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return SendToResult(tpr_id, mrm_id, tps_id, ref messegeSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToResult", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToResult(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        StatusTransaction result = new TransactionQueueCls().SendToDoctor(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToResult", ex, false);
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
                Program.MessageError("SendQueue", "SendToResult", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToPHM(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return SendToPHM(tpr_id, mrm_id, tps_id, ref messegeSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToResult", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToPHM(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        StatusTransaction result = new TransactionQueueCls().SendToPHM(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToPHM", ex, false);
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
                Program.MessageError("SendQueue", "SendToResult", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction SendToBook(int tpr_id, int mrm_id, ref string messegeSend)
        {
            try
            {
                int tps_id = Program.CurrentPatient_queue.tps_id;
                return SendToBook(tpr_id, mrm_id, tps_id, ref messegeSend);
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "SendToResult", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction SendToBook(int tpr_id, int mrm_id, int tps_id, ref string messegeSend)
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

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        trn_doctor_hdr doctor = tpr.trn_doctor_hdrs.FirstOrDefault();
                        if (doctor == null)
                        {
                            doctor = new trn_doctor_hdr();
                            tpr.trn_doctor_hdrs.Add(doctor);
                        }
                        StatusTransaction result = new TransactionQueueCls().SendToBook(ref tpr, ref messegeSend);
                        if (result == StatusTransaction.True)
                        {
                            new TransactionPlanCls().endPlan(ref tpr, mrm_id);
                            new TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                        Program.MessageError("SendQueue", "SendToResult", ex, false);
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
                Program.MessageError("SendQueue", "SendToResult", ex, false);
                return StatusTransaction.Error;
            }
        }

        private StatusTransaction callLab(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    int tpt_id = tpr.tpt_id;
                    CallQueue.CallLab(tpt_id, tpr_id);
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("SendQueue", "callLab", ex, false);
                return StatusTransaction.Error;
            }
        }
    }
}
