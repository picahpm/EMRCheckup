using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Data.Common;
using System.Windows.Forms;
using System.Data.Linq;

namespace BKvs2010
{
    public class CallQueue
    {
        public static void P_CallHistoryQueue()
        {
            try
            {
                DateTime dtnow = Program.GetServerDateTime();
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
                                              where t1.mrd_id == Program.CurrentRoom.mrd_id
                                              && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                              && t1.tps_status == "WK"
                                              && t1.tps_ns_status == null
                                              orderby t1.tps_create_date ascending
                                              select t1).FirstOrDefault();
                    if (objQ != null)
                    {
                        Program.CurrentPatient_queue = objQ;
                        Program.CurrentRegis = objQ.trn_patient_regi;
                        AlertOutDepartment.LoadTime();
                    }
                    else
                    {
                        trn_patient_queue objReady = (from t1 in dbc.trn_patient_queues
                                                      where t1.mrd_id == Program.CurrentRoom.mrd_id
                                                            && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                                           && t1.tps_status == "NS"
                                                           && t1.tps_ns_status == "WR"
                                                      orderby t1.tps_create_date ascending
                                                      select t1).FirstOrDefault();
                        if (objReady != null)
                        {
                            Program.CurrentPatient_queue = objReady;
                            Program.CurrentRegis = objReady.trn_patient_regi;
                            //AlertOutDepartment.LoadTime();
                        }
                        else
                        {
                            Program.CurrentPatient_queue = null;
                            Program.CurrentRegis = null;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void P_CallHistoryQueue2()
        {
            try
            {
                DateTime dtnow = Program.GetServerDateTime();
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
                                              where t1.mrd_id == Program.CurrentRoom.mrd_id
                                              && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                              && t1.tps_status == "WK"
                                              && t1.tps_ns_status == null
                                              orderby t1.tps_create_date ascending
                                              select t1).FirstOrDefault();
                    if (objQ != null)
                    {
                        Program.CurrentPatient_queue = objQ;
                        Program.CurrentRegis = objQ.trn_patient_regi;
                        AlertOutDepartment.LoadTime();
                    }
                    else
                    {
                        trn_patient_queue objReady = (from t1 in dbc.trn_patient_queues
                                                      where t1.mrd_id == Program.CurrentRoom.mrd_id
                                                            && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                                           && t1.tps_status == "NS"
                                                           && t1.tps_ns_status == "WR"
                                                      orderby t1.tps_create_date ascending
                                                      select t1).FirstOrDefault();
                        if (objReady != null)
                        {
                            Program.CurrentPatient_queue = objReady;
                            Program.CurrentRegis = objReady.trn_patient_regi;
                            //AlertOutDepartment.LoadTime();
                        }
                        else
                        {
                            Program.CurrentPatient_queue = null;
                            Program.CurrentRegis = null;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static StatusTransaction P_CallQueueWaitReady()
        {
            Program.RefreshWaiting = false;
            Program.CurrentRegis = null;

            try
            {
                int mrd_id = Program.CurrentRoom.mrd_id;
                int mut_id = Program.CurrentUser.mut_id;
                string mut_username = Program.CurrentUser.mut_username;
                DateTime dateNow = Program.GetServerDateTime();
                List<Class.WaitingListCls.WaitingListDtl> result = new Class.WaitingListCls().getWaitingRoomDtl(mrd_id, mut_id);
                foreach (Class.WaitingListCls.WaitingListDtl row in result.OrderBy(x => x.index))
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        try
                        {
                            cdc.Connection.Open();
                            DbTransaction trans = cdc.Connection.BeginTransaction();
                            cdc.Transaction = trans;

                            trn_patient_queue PatientQueue = cdc.trn_patient_queues
                                                                .Where(x => x.tps_id == row.tps_id &&
                                                                            x.tps_status == "NS" &&
                                                                            x.tps_ns_status == "QL")
                                                                .FirstOrDefault();
                            if (PatientQueue != null)
                            {
                                PatientQueue.mrd_id = mrd_id;
                                PatientQueue.tps_ns_status = "WR";
                                PatientQueue.tps_update_by = mut_username;
                                PatientQueue.tps_update_date = dateNow;
                                try
                                {
                                    cdc.SubmitChanges();
                                    cdc.Transaction.Commit();
                                    Program.CurrentPatient_queue = PatientQueue;
                                    Program.CurrentRegis = PatientQueue.trn_patient_regi;
                                    return StatusTransaction.True;
                                }
                                catch (System.Data.Linq.ChangeConflictException)
                                {
                                    cdc.Transaction.Rollback();
                                    Program.CurrentPatient_queue = null;
                                    Program.CurrentRegis = null;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            cdc.Transaction.Rollback();
                            Program.CurrentPatient_queue = null;
                            Program.CurrentRegis = null;
                            Program.MessageError("CallQueue", "P_CallQueueWaitReady", ex, false);
                            return StatusTransaction.Error;
                        }
                        finally
                        {
                            cdc.Connection.Close();
                        }
                    }
                }
                return StatusTransaction.False;
            }
            catch (Exception ex)
            {
                Program.MessageError("CallQueue", "P_CallQueueWaitReady", ex, false);
                return StatusTransaction.Error;
            }
            finally
            {
                Program.RefreshWaiting = true;
            }
        }

        //public static StatusTransaction P_CallQueueWaitReady()
        //{
        //    Program.RefreshWaiting = false;
        //    Program.CurrentRegis = null;

        //    try
        //    {
        //    reQuery:
        //        using (InhCheckupDataContext dbc = new InhCheckupDataContext())
        //        {
        //            try
        //            {
        //                dbc.Connection.Open();
        //                DbTransaction trans = dbc.Connection.BeginTransaction();
        //                dbc.Transaction = trans;

        //                DateTime datenow = Program.GetServerDateTime();
        //                DateTime dtyesterday = datenow.Date;// new DateTime(2013, 3, 5);
        //                TimeSpan timenow = datenow.TimeOfDay;
        //                double limittime = Program.GetLimitTime("HD");

        //                // แพทย์สามารถเรียก ผู้รับบริการได้หรือไม่ โดย คนจำนวนน้อยที่สุดหรือไม่
        //                /*var call_flag = (from t in dbc.vw_call_doctors 
        //                                 where t.tpr_pe_doc_code == Program.CurrentUser.mut_username 
        //                                 select t.call_flag).FirstOrDefault();*/
        //                // แพทย์สามารถเรียก ผู้รับบริการได้หรือไม่
        //                /*var call_can = (from t in dbc.vw_call_doctors 
        //                                where t.tpr_pe_doc_code == Program.CurrentUser.mut_username 
        //                                select t.call_can).FirstOrDefault();*/

        //                //bool? doc_call = dbc.func_get_doctor_call(Program.CurrentUser.mut_username);

        //                var objselect = (from t1 in dbc.trn_patient_queues
        //                                 where t1.mrm_id == Program.CurrentRoom.mrm_id
        //                                 && t1.tps_status == "NS"
        //                                 && t1.tps_ns_status == "QL" //|| t1.tps_ns_status == "WR"
        //                                 && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtyesterday.Date
        //                                 //&& GetpatientInPatientCate.Contains(t1.trn_patient_regi.tpr_id)
        //                                 orderby t1.tps_bm_seq, t1.tps_call_date, t1.tps_create_date
        //                                 select new
        //                                 {
        //                                     mdc_id = t1.trn_patient_regi.mdc_id,
        //                                     mvtID = t1.mvt_id,
        //                                     ReqDoc = t1.trn_patient_regi.tpr_req_doctor,
        //                                     ReqGender = (t1.trn_patient_regi.tpr_req_doc_gender == null) ? 'X' : t1.trn_patient_regi.tpr_req_doc_gender,
        //                                     DocCode = t1.trn_patient_regi.tpr_req_doc_code,
        //                                     //PEDoc = string.IsNullOrEmpty(Convert.ToString(t1.trn_patient_regi.tpr_pe_doc)) ? 'N' : t1.trn_patient_regi.tpr_pe_doc,
        //                                     PEDoc = (t1.trn_patient_regi.tpr_pe_doc == null) ? 'N' : t1.trn_patient_regi.tpr_pe_doc,
        //                                     PEDocCode = t1.trn_patient_regi.tpr_pe_doc_code,
        //                                     NurseCode = t1.trn_patient_regi.tpr_nurse_code,

        //                                     RTN_Nurse = t1.trn_patient_regi.tpr_return_screening,

        //                                     OutDocCode = string.IsNullOrEmpty(t1.trn_patient_regi.tpr_req_inorout_doctor) ? false :
        //                                                  t1.trn_patient_regi.tpr_req_inorout_doctor != "UT" ? false : true, //(t1.trn_patient_regi.tpr_req_inorout_doctor == "UT") ? true : false,

        //                                     t1.tps_id,
        //                                     t1.tps_status,
        //                                     t1.tps_ns_status,
        //                                     t1.tps_call_status,
        //                                     tps_bm_seq = (t1.tps_bm_seq != null) ? t1.tps_bm_seq : 99,
        //                                     t1.mrm_id,
        //                                     t1.tps_create_date,
        //                                     IsPirot = t1.trn_patient_regi.tpr_patient_type,
        //                                     st_date2 = (t1.tps_call_status == "HD") ? t1.tps_hold_date : datenow,
        //                                     st_date = (t1.tps_call_status == "HD") ? t1.tps_hold_date : t1.tps_create_date,
        //                                     hold_flag = (t1.tps_call_status == "HD" && (timenow.Subtract(t1.tps_hold_date.Value.TimeOfDay)).TotalMinutes >= 0) ? "Y" : "N",
        //                                     tprid = t1.trn_patient_regi.tpr_id,
        //                                     countmdc = (from t in dbc.trn_patient_cats where t.tpr_id == t1.trn_patient_regi.tpr_id select t.mdc_id).Count(),

        //                                     countInsu = (from t in dbc.trn_patient_cats 
        //                                                  join s in dbc.mst_doc_categories on t.mdc_id equals s.mdc_id
        //                                                  where t.tpr_id == t1.trn_patient_regi.tpr_id
        //                                                  && s.mdc_pre_insure == true
        //                                                  select t.mdc_id).Count(),

        //                                     DocCat = (from t in dbc.trn_patient_cats join s in dbc.mst_doc_categories on t.mdc_id equals s.mdc_id where t.tpr_id == t1.trn_patient_regi.tpr_id && s.mdc_code == "MD014" select s.mdc_id).Distinct().Count(),

        //                                     mdc_flag = (from mut in dbc.mst_user_types
        //                                                 join muc in dbc.mst_user_cats on mut.mut_id equals muc.mut_id
        //                                                 join tpc1 in dbc.trn_patient_cats on muc.mdc_id equals tpc1.mdc_id
        //                                                 join tpq in dbc.trn_patient_queues on tpc1.tpr_id equals tpq.tpr_id
        //                                                 where tpq.mrm_id == Program.CurrentRoom.mrm_id
        //                                                 && tpq.tpr_id == t1.trn_patient_regi.tpr_id
        //                                                 && tpq.tps_status == "NS"
        //                                                 && tpq.tps_ns_status == "QL"
        //                                                 && tpq.trn_patient_regi.tpr_arrive_date.Value.Date == dtyesterday.Date
        //                                                 && mut.mut_id == Program.CurrentUser.mut_id
        //                                                 && mut.mut_gender == ((tpq.trn_patient_regi.tpr_req_doc_gender == null) ? mut.mut_gender : tpq.trn_patient_regi.tpr_req_doc_gender)
        //                                                 select tpc1.tpr_id).Count(),
        //                                     vip_hpc = (t1.trn_patient_regi.trn_patient.tpt_vip_hpc == true) ? "Y" : "N",
        //                                     type_PE = ((from t in dbc.mst_events where t.mvt_id == t1.mvt_id select t.mvt_code).FirstOrDefault() == "PE") ? "Y" : "N",
        //                                     type_Lower = t1.trn_patient_regi.tpr_miss_lower
        //                                 }).ToList();

        //                objselect = (from t1 in objselect
        //                             orderby t1.hold_flag descending, t1.tps_bm_seq, t1.st_date
        //                             select t1).ToList();

        //                if (objselect.Count() > 0)
        //                {
        //                    //var v_tprid = objselect.Select(x => x.tprid).ToList();
        //                    //var countmdc = (from t in dbc.trn_patient_cats where t.tpr_id == Convert1.ToInt32(v_tprid) select t.mdc_id).Count();
        //                    //*********** เช็คว่าเป็น Pirot **
        //                    var CurrentmrmCode = (from t1 in dbc.mst_room_hdrs where t1.mrm_id == Program.CurrentRoom.mrm_id select t1).FirstOrDefault();
        //                    if (CurrentmrmCode != null)
        //                    {
        //                        if (Program.CurrentSite.mhs_code == "01HPC3")
        //                        {
        //                            //objselect = objselect.OrderBy(x => x.tps_bm_seq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.st_date).ToList();
        //                            objselect = objselect.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.tps_bm_seq).ThenBy(x => x.st_date).ToList();
        //                        }
        //                        else if (Program.CurrentSite.mhs_code == "01HPC2")
        //                        {
        //                            //objselect = objselect.OrderBy(x => x.tps_bm_seq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.st_date).ToList();
        //                            objselect = objselect.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.tps_bm_seq).ThenBy(x => x.st_date).ToList();
        //                        }

        //                        if (CurrentmrmCode.mrm_code == "SC")
        //                        {
        //                            if (Program.CurrentRoom.mrd_avation == true && Program.CurrentRoom.mrd_pre_insure == true)
        //                            {
        //                                //objselect = objselect.Where(x => (x.IsPirot == '2' || x.IsPirot == '4') && x.countmdc > 0).ToList();
        //                                //objselect = objselect.Where(x => (x.IsPirot == '2' || x.IsPirot == '4') || x.countInsu > 0).ToList();
        //                            }
        //                            else if (Program.CurrentRoom.mrd_avation == true && Program.CurrentRoom.mrd_pre_insure == false)
        //                            {
        //                                objselect = objselect.Where(x => (x.IsPirot == '2' || x.IsPirot == '4') && x.countInsu == 0).ToList();
        //                            }
        //                            else if (Program.CurrentRoom.mrd_avation == false && Program.CurrentRoom.mrd_pre_insure == true)
        //                            {
        //                                //objselect = objselect.Where(x => x.countmdc > 0 || ((x.IsPirot != '2' && x.IsPirot != '4') && (x.countmdc == 0 || (x.countmdc > 0 && x.DocCat == 1)))).ToList();
        //                                //objselect = objselect.Where(x => countmdc > 0).ToList();
        //                                objselect = objselect.Where(x => (x.IsPirot != '2' && x.IsPirot != '4') || x.countInsu > 0).ToList();
        //                            }
        //                            else
        //                            {
        //                                //objselect = objselect.Where(x => (x.IsPirot != '2' && x.IsPirot != '4') && (x.countmdc == 0 || (x.countmdc > 0 && x.DocCat == 1))).ToList();
        //                                objselect = objselect.Where(x => (x.IsPirot != '2' && x.IsPirot != '4') && x.countInsu == 0).ToList();
        //                            }

        //                            // เพิ่มเงื่อนไขในการเข้าห้องเดิมของ HPC Site 2
        //                            if (Program.CurrentSite.mhs_code == "01HPC2")
        //                            {
        //                                objselect = objselect.Where(x => (x.NurseCode == null || (x.NurseCode != null && x.NurseCode == Program.CurrentUser.mut_username))).ToList();

        //                                objselect = objselect.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.RTN_Nurse).ThenByDescending(x => x.hold_flag).ThenBy(x => x.tps_bm_seq).ThenBy(x => x.st_date).ToList();
        //                            }
        //                            else if (Program.CurrentSite.mhs_code == "01HPC3")
        //                            {
        //                                objselect = objselect.Where(x => (x.NurseCode == null || (x.NurseCode != null && x.NurseCode == Program.CurrentUser.mut_username))).ToList();

        //                                objselect = objselect.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.RTN_Nurse).ThenByDescending(x => x.hold_flag).ThenBy(x => x.tps_bm_seq).ThenBy(x => x.st_date).ToList();
        //                            }

        //                            //    if (countmdc == 0)
        //                            //    {
        //                            //        objselect = objselect.Where(x => x.IsPirot != '2' && x.IsPirot != '4').ToList();
        //                            //    }
        //                            //}
        //                            //if (Program.CurrentRoom.mrd_pre_insure == true)
        //                            //{
        //                            //    //objselect = objselect.Where(x => x.mdc_id != null || x.mdc_id == null).ToList();
        //                            //    objselect = objselect.Where(x => countmdc > 0).ToList();
        //                            //}
        //                            //else
        //                            //{
        //                            //    // objselect = objselect.Where(x => x.mdc_id == null).ToList();
        //                            //    objselect = objselect.Where(x => countmdc == 0).ToList();
        //                            //}
        //                        }
        //                        else if (CurrentmrmCode.mrm_code == "DC")
        //                        {
        //                            //{//ถ้า Require Doctor
        //                            char datagender = (Program.CurrentUser.mut_gender == null) ? 'Z' : Convert.ToChar(Program.CurrentUser.mut_gender);

        //                            //objselect = objselect.Where(x => ((x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                            //                                    (x.PEDoc == null && ((x.countmdc > 0 && x.tprid == Convert1.ToInt32(GetpatientInPatientCate.Contains(x.tprid))) || (x.countmdc == 0)) ||
        //                            //                                    (x.PEDoc == 'N' && ((x.countmdc > 0 && x.tprid == Convert1.ToInt32(GetpatientInPatientCate.Contains(x.tprid))) || (x.countmdc == 0))))
        //                            //                                    && (x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                            //                                    (x.ReqDoc == 'Y' && x.ReqGender == datagender) ||
        //                            //                                    (x.ReqDoc == 'N' && ((x.countmdc > 0 && x.tprid == Convert1.ToInt32(GetpatientInPatientCate.Contains(x.tprid))) || (x.countmdc == 0)))
        //                            //                                )).ToList();

        //                            if (Program.CurrentSite.mhs_code == "01CHK")
        //                            {
        //                                /*if (Program.CurrentRegis.tpr_req_inorout_doctor == "UT")
        //                                {
        //                                    objselect = objselect.Where(x => ((x.type_PE == "Y") ||
        //                                                                      (x.type_PE == "N" && x.ReqDoc == 'Y' && && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                                      (x.type_PE == "N" && x.ReqDoc == 'Y' && && x.DocCode == null && x.ReqGender == datagender)
        //                                                                )).*/
        //                                /*if (doc_call == true)
        //                                {
        //                                    //objselect = objselect.Where(x => 1 == 1).ToList();

        //                                    objselect = objselect.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                                                               (x.OutDocCode == false && x.PEDoc == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)) ||
        //                                                               (x.OutDocCode == false && x.PEDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))))
        //                                                               && ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                               ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
        //                                                               ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) || 
        //                                                               (x.OutDocCode == true && x.type_PE == "Y") ||
        //                                                               (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                               (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
        //                                                           )).ToList();
        //                                }
        //                                else
        //                                {
        //                                    objselect = objselect.Where(x => (
        //                                                                (x.PEDocCode != null && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                                                                (x.DocCode != null && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                                (x.countmdc > 0 && x.mdc_flag > 0)
        //                                                                )).ToList();
        //                                }*/
        //                                objselect = objselect.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                                                           ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                           ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
        //                                                           (x.OutDocCode == true && x.type_PE == "Y") ||
        //                                                           (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                           (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
        //                                                            )).ToList();
        //                            }
        //                            else if (Program.CurrentSite.mhs_code == "01HPC2")
        //                            {
        //                                /*objselect = objselect.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                                                                (x.OutDocCode == false && x.PEDoc == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)) ||
        //                                                                (x.OutDocCode == false && x.PEDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))))
        //                                                                && ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                                ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
        //                                                                ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
        //                                                                (x.OutDocCode == true && x.type_PE == "Y") ||
        //                                                                (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                                (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
        //                                                           )).ToList();*/

        //                                objselect = objselect.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                                                                  (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) || 
        //                                                                  (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) || 
        //                                                                  (x.OutDocCode == false && x.PEDoc == 'N' && x.DocCode == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) || 
        //                                                                  (x.OutDocCode == true && x.type_PE == "Y") ||
        //                                                                  (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                                  (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
        //                                                           )).ToList();

        //                                objselect = objselect.OrderBy(x => x.tps_bm_seq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.type_PE).ThenBy(x => x.st_date).ToList();
        //                            }
        //                            else
        //                            {
        //                                objselect = objselect.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                                                                  (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                                  (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
        //                                                                  (x.OutDocCode == false && x.PEDoc == 'N' && x.DocCode == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
        //                                                                  (x.OutDocCode == true && x.type_PE == "Y") ||
        //                                                                  (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                                  (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)
        //                                                           )).ToList();

        //                                objselect = objselect.OrderBy(x => x.tps_bm_seq).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.type_PE).ThenBy(x => x.st_date).ToList();
        //                            }

        //                            /*objselect = objselect.Where(x => ((x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
        //                                                               (x.PEDoc == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)) ||
        //                                                               (x.PEDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))))
        //                                                               && (x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
        //                                                               (x.ReqDoc == 'Y' && x.ReqGender == datagender) ||
        //                                                               (x.ReqDoc == 'N' && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0)))
        //                                                           )).ToList();*/
        //                        }
        //                        else if (CurrentmrmCode.mrm_code == "EM")
        //                        {//ถ้า เป็น Eye เข้าใช้งานผ่าน Type D=Doctor[mvt_code=EM] N=Nurse[mvt_code=EN]
        //                            string strCode = "";
        //                            if (Program.CurrentRoom.mrd_type == 'D')
        //                            {
        //                                strCode = "EM";
        //                            }
        //                            else if (Program.CurrentRoom.mrd_type == 'N')
        //                            {
        //                                strCode = "EN";
        //                            }

        //                            int mvtid = 0;
        //                            var currentEvent = (from t1 in dbc.mst_events
        //                                                where t1.mvt_code == strCode
        //                                                select t1).FirstOrDefault();
        //                            if (currentEvent != null)
        //                            {
        //                                mvtid = currentEvent.mvt_id;
        //                                objselect = objselect.Where(x => x.mvtID == mvtid).ToList();
        //                            }
        //                        }
        //                        else if (CurrentmrmCode.mrm_code == "TE")
        //                        {//กรณีที่ หน้า Teeth เข้าใช้งานผ่าน T=Technical[mvt_code=TX] D=Doctor[mvt_code=TE]
        //                            string strCode = "";
        //                            if (Program.CurrentRoom.mrd_type == 'T')
        //                            {
        //                                strCode = "TX";
        //                            }
        //                            else if (Program.CurrentRoom.mrd_type == 'D')
        //                            {
        //                                strCode = "TE";
        //                            }

        //                            int mvtid = 0;
        //                            var currentEvent = (from t1 in dbc.mst_events
        //                                                where t1.mvt_code == strCode
        //                                                select t1).FirstOrDefault();
        //                            if (currentEvent != null)
        //                            {
        //                                mvtid = currentEvent.mvt_id;
        //                                objselect = objselect.Where(x => x.mvtID == mvtid).ToList();
        //                            }
        //                        }
        //                        else if (CurrentmrmCode.mrm_code == "UU")
        //                        {
        //                            if (Program.CurrentSite.mhs_code == "01CHK")
        //                            {
        //                                objselect = objselect.OrderBy(x => x.tps_bm_seq).ThenByDescending(x => x.type_Lower).ThenByDescending(x => x.hold_flag).ThenBy(x => x.st_date).ToList();
        //                            }
        //                            else
        //                            {
        //                                objselect = objselect.OrderBy(x => x.tps_bm_seq).ThenByDescending(x => x.type_Lower).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.st_date).ToList();
        //                            }

        //                        }
        //                    }

        //                    //**************************** 
        //                    var objselectTime = (from t1 in objselect
        //                                         /*where (t1.st_date != null) && (timenow.Subtract(t1.st_date.Value.TimeOfDay)).TotalMinutes > limittime
        //                                         || timenow.Subtract(t1.st_date.Value.TimeOfDay).TotalMinutes == 0*/
        //                                         //where (t1.st_date != null) && timenow.Subtract(t1.st_date.Value.TimeOfDay).TotalMinutes == 0
        //                                         //orderby t1.hold_flag descending, t1.tps_bm_seq, t1.st_date
        //                                         select t1).FirstOrDefault();
        //                    if (objselectTime == null)
        //                    {
        //                        objselectTime = objselect.FirstOrDefault();
        //                    }
        //                    if (objselectTime != null)
        //                    {
        //                        trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues where t1.tps_id == objselectTime.tps_id select t1).FirstOrDefault();
        //                        if (objQ != null)
        //                        {
        //                            if (objQ.tps_ns_status == "QL")
        //                            {
        //                                objQ.mrd_id = Program.CurrentRoom.mrd_id;
        //                                objQ.tps_ns_status = "WR";
        //                                objQ.tps_update_by = Program.CurrentUser.mut_username;
        //                                objQ.tps_update_date = datenow;
        //                                try
        //                                {
        //                                    Program.CurrentPatient_queue = objQ;
        //                                    Program.CurrentRegis = objQ.trn_patient_regi;
        //                                    dbc.SubmitChanges();
        //                                    dbc.Transaction.Commit();
        //                                    return StatusTransaction.True;
        //                                }
        //                                catch (System.Data.Linq.ChangeConflictException) //Win32Exception
        //                                {
        //                                    goto reQuery;
        //                                }
        //                                //AlertOutDepartment.LoadTime();
        //                            }
        //                            else if (objQ.tps_status == "WR")
        //                            {
        //                                goto reQuery;
        //                            }
        //                        }
        //                    }

        //                }
        //                return StatusTransaction.False;
        //            }
        //            catch (Exception ex)
        //            {
        //                dbc.Transaction.Rollback();
        //                Program.CurrentPatient_queue = null;
        //                Program.CurrentRegis = null;
        //                Program.MessageError("CallQueue", "P_CallQueueWaitReady", ex, false);
        //                return StatusTransaction.Error;
        //            }
        //            finally
        //            {
        //                dbc.Connection.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("CallQueue", "P_CallQueueWaitReady", ex, false);
        //        return StatusTransaction.Error;
        //    }
        //    finally
        //    {
        //        Program.RefreshWaiting = true;
        //    }
        //}

        public static StatusTransaction P_CallQueueReady()
        {
            Program.RefreshWaiting = false;
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    try
                    {
                        dbc.Connection.Open();
                        DbTransaction tran = dbc.Connection.BeginTransaction();
                        dbc.Transaction = tran;

                        if (Program.CurrentPatient_queue != null && Program.CurrentRoom != null)
                        {
                            trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues where t1.tps_id == Program.CurrentPatient_queue.tps_id select t1).FirstOrDefault();
                            if (objQ != null)
                            {
                                objQ.mrd_id = Program.CurrentRoom.mrd_id;
                                objQ.tps_status = "WK";
                                objQ.tps_ns_status = null;
                                objQ.tps_bm_seq = null;
                                objQ.tps_call_by = Program.CurrentUser.mut_username;
                                objQ.tps_call_date = Program.GetServerDateTime();
                                objQ.tps_start_date = Program.GetServerDateTime();
                                objQ.tps_update_by = Program.CurrentUser.mut_username;
                                objQ.tps_update_date = Program.GetServerDateTime();
                                dbc.SubmitChanges();
                                dbc.Transaction.Commit();
                            }
                        }
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        dbc.Transaction.Rollback();
                        Program.MessageError("CallQueue", "P_CallQueueReady", ex, false);
                        return StatusTransaction.Error;
                    }
                    finally
                    {
                        dbc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("CallQueue", "P_CallQueueRedy", ex, false);
                return StatusTransaction.Error;
            }
            finally
            {
                Program.RefreshWaiting = true;
            }
        }


        //public static void P_CallCancel(CheckupDataContext dbc,string remarkReson,string remarkOtherReson)
        //{
        //    if (CallQueue.IsStatusED()) { return; }
        //    Program.RefreshWaiting = false;
        //    int tpr_id = Program.CurrentRegis.tpr_id;
        //    int mrm_id = Program.CurrentRoom.mrm_id;
        //    trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
        //                              where t1.tpr_id == tpr_id
        //                              && t1.mrm_id == mrm_id
        //                              && t1.tps_status == "WK"
        //                              select t1).FirstOrDefault();
        //    if (objQ != null)
        //    {
        //        objQ.tps_status = "CL";
        //        objQ.tps_ns_status = null;
        //        objQ.tps_start_date = null;
        //        objQ.tps_end_date = null;
        //        objQ.tps_cancel_by = Program.CurrentUser.mut_username;
        //        objQ.tps_cancel_date = Program.GetServerDateTime();
        //        objQ.tps_cancel_remark = remarkReson;
        //        objQ.tps_cancel_other = remarkOtherReson;
        //        objQ.tps_update_by = Program.CurrentUser.mut_username;
        //        objQ.tps_update_date = objQ.tps_cancel_date;
        //        dbc.SubmitChanges();
        //        PSendAutoAllRoomByStation(true);//Added.Akkaradech on 2013-12-20
        //        Program.CurrentPatient_queue = null;
        //        Program.CurrentRegis = null;
        //        //AlertOutDepartment.StopTime();
        //    }
        //    Program.RefreshWaiting = true;
        //}

        #region CancelQueueAndSendByStation Added.Akkaradech on 2013-12-19
        //public static void CancelByStation(CheckupDataContext dbc, string remarkReson, string remarkOtherReson)
        //{
        //    try
        //    {
        //        if (CallQueue.IsStatusED()) { return; }
        //        Program.RefreshWaiting = false;
        //        int tpr_id = Program.CurrentRegis.tpr_id;
        //        int mrm_id = Program.CurrentRoom.mrm_id;
        //        trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
        //                                  where t1.tpr_id == tpr_id
        //                                  && t1.mrm_id == mrm_id
        //                                  && t1.tps_status == "WK"
        //                                  select t1).FirstOrDefault();
        //        if (objQ != null)
        //        {
        //            objQ.tps_status = "CL";
        //            objQ.tps_ns_status = null;
        //            objQ.tps_start_date = null;
        //            objQ.tps_end_date = null;
        //            objQ.tps_cancel_by = Program.CurrentUser.mut_username;
        //            objQ.tps_cancel_date = Program.GetServerDateTime();
        //            objQ.tps_cancel_remark = remarkReson;
        //            objQ.tps_cancel_other = remarkOtherReson;
        //            objQ.tps_update_by = Program.CurrentUser.mut_username;
        //            objQ.tps_update_date = objQ.tps_cancel_date;
        //            dbc.SubmitChanges();
        //           // PSendAutoAllRoom();
        //            PSendAutoAllRoomByStation(true);
        //            //Program.CurrentPatient_queue = null;
        //            //Program.CurrentRegis = null;
        //            AlertOutDepartment.StopTime();
        //        }
        //        Program.RefreshWaiting = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        //public static bool PSendAutoAllRoomByStation(bool iscancelroom)
        //{
        //    bool IsCallLab = false;
        //    if (CallQueue.IsStatusED()) { return true; }
        //    bool Iscompleted = false;
        //    Program.RefreshWaiting = false;
        //    int tptID = 0;
        //    int tpr_id = 0;

        //    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();

        //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
        //    {
        //        try
        //        {

        //            dbc.Connection.Open();
        //            DbTransaction trans = dbc.Connection.BeginTransaction();
        //            dbc.Transaction = trans;


        //            manage_queue_transaction mqt = dbc.manage_queue_transactions.Where(x => x.mqt_id == 1).FirstOrDefault();
        //            string user_name = null;
        //            if (Program.CurrentUser != null)
        //            {
        //                user_name = Program.CurrentUser.mut_username;
        //            }
        //            mqt.mqt_tpr_id = tpr_id;
        //            mqt.mqt_update_date = Program.GetServerDateTime();
        //            mqt.mqt_flag = false;
        //            mqt.mqt_user_name = user_name;
        //            try
        //            {
        //                dbc.SubmitChanges();
        //            }
        //            catch
        //            {
        //                dbc.Transaction.Rollback();
        //                dbc.Connection.Close();
        //                return false;
        //            }



        //            tptID = Program.CurrentRegis.tpt_id;
        //            tpr_id = Program.CurrentRegis.tpr_id;
        //            Gettprid = tpr_id;
        //            // update Patient_Plans
        //            if (iscancelroom == true)
        //            {
        //                P_CancelPatientPlan(dbc, Program.CurrentRoom.mrm_id, (int)Program.CurrentPatient_queue.mvt_id);
        //            }
        //            else
        //            {
        //                SetPatientPlanStatus(dbc, Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id);
        //            }

        //            // update Patient Queue
        //            trn_patient_queue CurrentQueue = (from t1 in dbc.trn_patient_queues
        //                                              where t1.tps_id == Program.CurrentPatient_queue.tps_id
        //                                              select t1).FirstOrDefault();
        //            CurrentQueue.tpr_id = Program.CurrentRegis.tpr_id;
        //            CurrentQueue.mrm_id = Program.CurrentRoom.mrm_id;
        //            CurrentQueue.mrd_id = Program.CurrentRoom.mrd_id;
        //            CurrentQueue.tps_send_by = Program.CurrentUser.mut_username;
        //            CurrentQueue.tps_end_date = Program.GetServerDateTime();
        //            CurrentQueue.tps_status = "ED";
        //            CurrentQueue.tps_ns_status = null;

        //            dbc.SubmitChanges();

        //            int mrmID = 0;
        //            int mvtID = 0;
        //            bool isHaveNextRoom = false;
        //            GetIsHaveNextRoom = false;
        //            if (CheckAutoGotoEye(ref mrmID, ref mvtID, ref Iscompleted, ref isHaveNextRoom, ref IsCallLab) == false)
        //            {

        //                //ส่งคิวตามปกติ
        //                string mvtcode = "";
        //                int SendtoSiteId = 0;
        //                if (Program.CheckPointBSiteUse != 0)
        //                {
        //                    SendtoSiteId = Program.CheckPointBSiteUse;
        //                }
        //                else
        //                {
        //                    SendtoSiteId = Program.CurrentSite.mhs_id;
        //                }
        //                //เช็คว่ามีห้องตรวจถัดไปหรือไม่

        //                var objEventroom = (from t1 in dbc.mst_room_events
        //                                    where t1.mrm_id == Program.CurrentRoom.mrm_id
        //                                    select t1.mvt_id).ToList();
        //                var icountnextRoom = (from t1 in dbc.trn_patient_plans
        //                                      where t1.tpr_id == Program.CurrentRegis.tpr_id
        //                                      && t1.tpl_status == 'N'
        //                                      && !objEventroom.Contains(t1.mvt_id)
        //                                      select t1
        //                                   ).Count();

        //                if (icountnextRoom > 0)
        //                {
        //                    //count LoopOver =3
        //                    int loopSearchNextRoom = 0;
        //                    //เวลาที่ต้องรอในห้องในต้องไม่เกิน Limitvalue
        //                    double Limitvalue = (from t1 in dbc.mst_config_dtls
        //                                         where t1.mst_config_hdr.mfh_code == "SQ"
        //                                         && t1.mst_config_hdr.mhs_id == Program.CurrentSite.mhs_id
        //                                         && t1.mfd_status == 'A'
        //                                         select t1.mfd_value).FirstOrDefault();
        //                CheckAgain: var objnextroom = (from t1 in dbc.vw_patient_rooms
        //                                               where t1.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
        //                                               && t1.tpr_id == Program.CurrentRegis.tpr_id
        //                                               && t1.mhs_id == SendtoSiteId
        //                                               && t1.site_rm == SendtoSiteId
        //                                               && t1.waiting_time <= Limitvalue
        //                                               orderby t1.waiting_time
        //                                               select t1).FirstOrDefault();

        //                    if (objnextroom != null)
        //                    {
        //                        mrmID = Convert.ToInt32(objnextroom.mrm_id);
        //                        mvtID = Convert.ToInt32(objnextroom.mvt_id);
        //                        mvtcode = objnextroom.mvt_code;
        //                        GetIsHaveNextRoom = true;

        //                    }
        //                    else
        //                    {
        //                        var objnextSiteroom = (from t1 in dbc.vw_patient_rooms
        //                                               where t1.mhs_id == SendtoSiteId
        //                                               && t1.tpr_id == Program.CurrentRegis.tpr_id
        //                                               && t1.site_rm == SendtoSiteId
        //                                               && t1.waiting_time <= Limitvalue
        //                                               orderby t1.waiting_time
        //                                               select t1).FirstOrDefault();

        //                        if (objnextSiteroom != null)
        //                        {
        //                            mrmID = Convert.ToInt32(objnextSiteroom.mrm_id);
        //                            mvtID = Convert.ToInt32(objnextSiteroom.mvt_id);
        //                            mvtcode = objnextSiteroom.mvt_code;
        //                            GetIsHaveNextRoom = true;
        //                        }
        //                        else
        //                        {
        //                            if (loopSearchNextRoom >= 3)
        //                            {
        //                                if (Program.CurrentRoom.mst_room_hdr.mrm_code == "CB")
        //                                {
        //                                    dbc.Transaction.Rollback();
        //                                    Iscompleted = false;
        //                                    Program.RefreshWaiting = true;
        //                                    Program.MessageError("Can't sent into HPC Site " + SendtoSiteId + ". Please change HPC Site.");
        //                                    return Iscompleted;
        //                                }
        //                                else
        //                                {
        //                                    objnextSiteroom = (from t1 in dbc.vw_patient_rooms
        //                                                       where t1.mhs_id == SendtoSiteId
        //                                                       && t1.tpr_id == Program.CurrentRegis.tpr_id
        //                                                       orderby t1.waiting_time
        //                                                       select t1).FirstOrDefault();
        //                                    if (objnextSiteroom != null)
        //                                    {
        //                                        mrmID = Convert.ToInt32(objnextSiteroom.mrm_id);
        //                                        mvtID = Convert.ToInt32(objnextSiteroom.mvt_id);
        //                                        mvtcode = objnextSiteroom.mvt_code;
        //                                        GetIsHaveNextRoom = true;
        //                                    }
        //                                    else
        //                                    {
        //                                        dbc.Transaction.Rollback();
        //                                        Iscompleted = false;
        //                                        Program.RefreshWaiting = true;
        //                                        Program.MessageError("Can't sent to next room. Please contact Admin.");
        //                                        return Iscompleted;
        //                                    }
        //                                }
        //                            }

        //                            loopSearchNextRoom = loopSearchNextRoom + 1;
        //                            Limitvalue = 1440;
        //                            goto CheckAgain;
        //                        }
        //                    }
        //                    // เช็คลำดับขั้นการเข้าห้อง
        //                    CheckNextRoom(ref mrmID, ref mvtID, mvtcode);
        //                }
        //                else
        //                {
        //                    //ถ้าไม่ห้องถัดไปให้ไปที่ checkpoint CC
        //                    int mhs_id = Program.CurrentSite.mhs_id;
        //                    int? tpr_site_use = Program.CurrentRegis.tpr_site_use;
        //                    mrmID = mst.getMstRoomHdr("CC").mrm_id;
        //                    mvtID = mst.getMstEvent("CC").mvt_id;
        //                    //mrmID = Program.Getmrm_id(dbc, "CC");
        //                    //mvtID = Program.Getmvt_id(dbc, "CC");
        //                    mvtcode = "CC";
        //                    isHaveNextRoom = true;
        //                    GetIsHaveNextRoom = true;
        //                    IsCallLab = true;
        //                }
        //            }
        //            Getmvtid = mvtID;
        //            Getmrmid = mrmID;

        //            //เช้คว่ามี Record ซ้ำหรือไม่ ถ้ามีแล้วไม่ต้องทำการสร้างใหม่ เพราะจะทำให้ Queue มีหลายQueue ใน Waitinglist
        //            var objqueue = (from t1 in dbc.trn_patient_queues
        //                            where t1.tpr_id == Program.CurrentRegis.tpr_id
        //                                && t1.mrm_id == mrmID
        //                                && t1.mvt_id == mvtID
        //                            select t1).FirstOrDefault();
        //            if (objqueue == null)
        //            {
        //                // insert table trn_patient_Queue
        //                trn_patient_queue newqueue = new trn_patient_queue();
        //                newqueue.tpr_id = Program.CurrentRegis.tpr_id;
        //                newqueue.mrm_id = mrmID;
        //                newqueue.mvt_id = mvtID;
        //                newqueue.mrd_id = null;
        //                newqueue.tps_start_date = Program.GetServerDateTime();
        //                newqueue.tps_end_date = Program.GetServerDateTime();
        //                if (isHaveNextRoom)
        //                {
        //                    newqueue.tps_status = "WK";
        //                    newqueue.tps_ns_status = null;
        //                }
        //                else
        //                {
        //                    newqueue.tps_status = "NS";
        //                    newqueue.tps_ns_status = "QL";
        //                }
        //                newqueue.tps_create_by = Program.CurrentUser.mut_username;
        //                newqueue.tps_create_date = Program.GetServerDateTime();
        //                newqueue.tps_update_by = Program.CurrentUser.mut_username;
        //                newqueue.tps_update_date = newqueue.tps_create_date;
        //                dbc.trn_patient_queues.InsertOnSubmit(newqueue);
        //            }
        //            else
        //            {
        //                objqueue.tps_create_date = Program.GetServerDateTime();
        //                objqueue.tps_create_by = Program.CurrentUser.mut_username;
        //                objqueue.tps_update_by = objqueue.tps_create_by;
        //                objqueue.tps_update_date = Program.GetServerDateTime();
        //            }
        //            dbc.SubmitChanges();
        //            ////Use update status in table [trn_patient_queue] to cancel
        //            trn_patient_queue CurrentQueue2 = (from t1 in dbc.trn_patient_queues
        //                                               where t1.tps_id == Program.CurrentPatient_queue.tps_id
        //                                               select t1).FirstOrDefault();
        //            CurrentQueue2.tpr_id = Program.CurrentRegis.tpr_id;
        //            CurrentQueue2.mrm_id = Program.CurrentRoom.mrm_id;
        //            CurrentQueue2.mrd_id = Program.CurrentRoom.mrd_id;
        //            CurrentQueue2.tps_send_by = Program.CurrentUser.mut_username;
        //            CurrentQueue2.tps_end_date = Program.GetServerDateTime();
        //            CurrentQueue2.tps_status = "CL";
        //            CurrentQueue2.tps_ns_status = null;


        //            mqt.mqt_tpr_id = null;
        //            mqt.mqt_update_date = null;
        //            mqt.mqt_flag = true;
        //            mqt.mqt_user_name = null;


        //            dbc.SubmitChanges();
        //            ////End Use update status in table [trn_patient_queue] to cancel
        //            dbc.Transaction.Commit();
        //            //Program.CurrentRegis = null;
        //            //Program.CurrentPatient_queue = null;
        //            AlertOutDepartment.StopTime();
        //            Iscompleted = true;
        //        }
        //        catch (Exception)
        //        {
        //            dbc.Transaction.Rollback();
        //            Iscompleted = false;
        //        }
        //        finally
        //        {
        //            dbc.Connection.Close();
        //        }
        //    }
        //    Program.RefreshWaiting = true;

        //    ClsBasicMeasurement.SaveBasicMeasurment(Program.CurrentRegis.tpr_id, IsCallLab);

        //    if (IsCallLab)
        //    {
        //        try
        //        {
        //            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
        //            {
        //                var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tptID).FirstOrDefault();
        //                CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no, Program.CurrentRegis.tpr_id);


        //                //Update LR สูติอย่างเดียวเนื่องจากไม่มีใน callDataTakeCare Event Code สูติ= PT
        //                SetLR_Obstaticresult(dbc, tpr_id);
        //            }

        //        }
        //        catch (Exception)
        //        {
        //        }

        //        try
        //        {
        //            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
        //            {
        //                var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tptID).FirstOrDefault();
        //                CheckUpLabClass.ws_Getcheckuplab_Async(currentPatient.tpt_hn_no, tpr_id);
        //                dbc.Connection.Close();
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            Program.MessageError("ws => getcheckuplab :" + ex.Message);
        //        }
        //    }

        //    //Program.RefreshWaiting = true;
        //    return Iscompleted;
        //}

        #endregion
        public static bool GetUltrasound_Type(int tprid, int mhsid)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    int ultra_upper = (from t1 in dbc.vw_patient_rooms
                                       where t1.tpr_id == tprid
                                       && t1.mhs_id == mhsid
                                       && t1.mrm_code == "US"
                                       && t1.mvt_code == "UU"
                                       select t1.mvt_code).Count();

                    int ultra_type = (from t1 in dbc.vw_patient_rooms
                                      where t1.tpr_id == tprid
                                      && t1.mhs_id == mhsid
                                      && t1.mrm_code == "US"
                                      && t1.mvt_code != "UU"
                                      select t1.mvt_code).Count();

                    if (ultra_upper > 0)
                    {
                        return true;
                    }
                    else
                    {
                        if (ultra_type > 0)
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
            catch
            { }
            return true;
        }

        public static bool CheckExcept_Lower(int tprid, int mhsid)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    int ct_station = (from t1 in dbc.vw_patient_rooms
                                      where t1.tpr_id == tprid
                                      && t1.mhs_id == mhsid
                                          //&& t1.mvt_type_cate == 'M'
                                      && t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                      && t1.mvt_code != "UL"
                                      && t1.send_type == true
                                      select t1).Count();

                    int ct_lower = (from t in dbc.trn_patient_regis
                                    where t.tpr_id == tprid
                                    && t.tpr_miss_lower == true
                                    select t).Count();

                    if (ct_station > 0)
                    {
                        if (ct_lower > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch
            { }
            return true;
        }

        public static StatusTransaction PSendAutoAllRoom(Class.SendType type, int mrm_id, int tps_id)
        {
            return PSendAutoAllRoom(type, mrm_id, null, tps_id);
        }
        public static StatusTransaction PSendAutoAllRoom(Class.SendType type, int mrm_id, List<int> list_mvt_id, int tps_id)
        {
            StatusTransaction statusTran = StatusTransaction.False;

            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                bool IsCallLab = false;
                //if (CallQueue.IsStatusED()) { return true; }
                bool Iscompleted = false;
                bool SendEyeDoc = false;
                Program.RefreshWaiting = false;
                int tpt_id = Program.CurrentRegis.tpt_id;
                int tpr_id = Program.CurrentRegis.tpr_id;
                double limittime = Program.GetLimitTime("EDT");
                int mrmID = 0;
                int mvtID = 0;

                bool PassCheckPointB = false;
                if (type == Class.SendType.Pending)
                {
                    PassCheckPointB = new Class.FunctionDataCls().checkPassedCheckPointB(tpr_id);
                }

                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        List<trn_patient_plan> list_plan = cdc.trn_patient_plans.Where(x => x.tpr_id == tpr_id && list_mvt_id.Contains(x.mvt_id)).ToList();
                        if (list_mvt_id != null && list_mvt_id.Count > 0)
                        {
                            if (type == Class.SendType.Normal)
                            {
                                list_plan.ForEach(x => x.tpl_status = 'P');
                                cdc.SubmitChanges();
                                cdc.Transaction.Commit();
                            }
                            else if (type == Class.SendType.Skip)
                            {
                                list_plan.ForEach(x =>
                                {
                                    x.tpl_skip = true;
                                    x.tpl_skip_seq = x.tpl_skip_seq == null ? 1 : x.tpl_skip_seq + 1;
                                });
                                cdc.SubmitChanges();
                                cdc.Transaction.Commit();
                            }
                            else if (type == Class.SendType.Pending)
                            {
                                list_plan.ForEach(x => x.tpl_status = 'C');

                                List<trn_patient_pending> tpp = cdc.trn_patient_pendings
                                                                   .Where(x => x.tpr_id == tpr_id &&
                                                                               x.tpp_status == 'P' &&
                                                                               x.mrm_id == mrm_id).ToList();
                                if (tpp.Count() == 0)
                                {
                                    trn_patient_pending new_tpp = new trn_patient_pending
                                    {
                                        tpr_id = tpr_id,
                                        mrm_id = mrm_id,
                                        tpp_status = 'P',
                                        tpp_create_date = dateNow,
                                        tpp_update_date = dateNow
                                    };
                                    cdc.trn_patient_pendings.InsertOnSubmit(new_tpp);
                                    cdc.SubmitChanges();
                                    cdc.Transaction.Commit();
                                }
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
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Iscompleted = false;
                        Program.MessageError("CallQueue", "PSendAutoAllRoom(Class.SendType type, int mrm_id, List<int> list_mvt_id, int tps_id)", ex, false);
                        return StatusTransaction.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }

                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();


                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    try
                    {
                        dbc.Connection.Open();
                        DbTransaction trans = dbc.Connection.BeginTransaction();
                        dbc.Transaction = trans;

                        Gettprid = tpr_id;

                        //var objnextroom

                        // update Patient_Plans
                        //SetPatientPlanStatus(dbc, Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id);


                        // update Patient Queue
                        //trn_patient_queue CurrentQueue = (from t1 in dbc.trn_patient_queues
                        //                                  where t1.tps_id == Program.CurrentPatient_queue.tps_id
                        //                                  select t1).FirstOrDefault();
                        //CurrentQueue.tpr_id = Program.CurrentRegis.tpr_id;
                        //CurrentQueue.mrm_id = Program.CurrentRoom.mrm_id;
                        //CurrentQueue.mrd_id = Program.CurrentRoom.mrd_id;
                        //CurrentQueue.tps_send_by = Program.CurrentUser.mut_username;
                        //CurrentQueue.tps_end_date = Program.GetServerDateTime();
                        //CurrentQueue.tps_status = "ED";
                        //CurrentQueue.tps_ns_status = null;

                        //dbc.SubmitChanges();

                        bool isHaveNextRoom = false;

                        //ส่งคิวตามปกติ
                        string mvtcode = "";
                        int SendtoSiteId = 0;
                        if (Program.CheckPointBSiteUse != 0)
                        {
                            SendtoSiteId = Program.CheckPointBSiteUse;
                        }
                        else
                        {
                            SendtoSiteId = Program.CurrentSite.mhs_id;
                        }

                        var objnextroom = new vw_patient_room();
                        var objnextSiteroom = new vw_patient_room();
                        var objLower = new vw_patient_room();

                        //Check waiting Person for Doctor Room



                        //case เดิม ส่งถ้า Waiting Time น้อยกว่าที่กำหนด
                        //var waitDoctor = (from t1 in dbc.vw_patient_rooms
                        //                  where t1.tpr_id == tpr_id
                        //                    && t1.mvt_type_cate == 'M'
                        //                    && t1.mrm_code == "DC"
                        //                    && t1.login_flag == "Y"
                        //                      //&& t1.waiting_person == 0
                        //                    && t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                        //                    && t1.mhs_id == SendtoSiteId
                        //                    && t1.site_rm == SendtoSiteId
                        //                  select t1).FirstOrDefault();


                        //**** morn ***** Edit Send Doctor เฉพาะ กรณี แพทย์ ไม่ Service และ ไม่มี Q รอที่ Waiting
                        //จาก Case เดิม จะส่งถ้า Waiting Time น้อยกว่าที่กำหนด

                        var waitDoctor = new Class.FunctionDataCls().WaitingDoctor(tpr_id);
                        // ให้เท่ากับ new vw_patient_room() เพราะ function เก่า ใช้ตัวแปร Object vw_patient_room || null
                        // new vw_patient_room() จะไม่เท่ากับ null ส่ง Doctor
                        // null ไม่ส่ง Doctor
                        //**** morn *****



                        /*int waitDocRoom = (from t1 in dbc.trn_patient_queues
                                           join t2 in dbc.mst_room_hdrs on t1.mrm_id equals t2.mrm_id
                                           where t2.mrm_code == "DC"
                                           && t2.mhs_id == SendtoSiteId
                                           && t1.tps_status == "NS"
                                           && t1.tps_ns_status == "QL"
                                           && t1.tps_call_status == null
                                           && t1.tps_create_date.Value.Date == dateNow.Date
                                           select t1).Count();*/

                        var chkEye_Package = (from t1 in dbc.trn_patient_plans
                                              join t2 in dbc.mst_events on t1.mvt_id equals t2.mvt_id
                                              where t1.tpr_id == tpr_id
                                              && t2.mvt_code == "EM"
                                              select t1).Count();

                        var countDoctorPass = (from t1 in dbc.trn_patient_queues
                                               join t2 in dbc.mst_events on t1.mvt_id equals t2.mvt_id
                                               where t1.tpr_id == tpr_id
                                               && t2.mvt_code == "EM"
                                               select t1).Count();

                        var countNursePass = (from t1 in dbc.trn_patient_queues
                                              join t2 in dbc.mst_events on t1.mvt_id equals t2.mvt_id
                                              where t1.tpr_id == tpr_id
                                              && t2.mvt_code == "EN"
                                              select t1).Count();

                        // Add Send Start Check B
                        Class.FunctionDataCls func = new Class.FunctionDataCls();

                        //&& waitDocRoom == 0
                        //if (Program.CurrentSite.mhs_code == "01CHK" && waitDoctor != null && (func.checkDoctorRequest(tpr_id, SendtoSiteId, true) == true) &&
                        //    ((countDoctorPass == 0 && countNursePass == 0) || (countDoctorPass != 0 && countNursePass != 0)))
                        //if (Program.CurrentSite.mhs_code == "01CHK"  && (func.checkDoctorRequest(tpr_id, SendtoSiteId, true) == true) &&
                        //    ((countDoctorPass == 0 && countNursePass == 0) || (countDoctorPass != 0 && countNursePass != 0)))
                        if (Program.CurrentSite.mhs_code == "01CHK" && waitDoctor != null &&
                            ((countDoctorPass == 0 && countNursePass == 0) || (countDoctorPass != 0 && countNursePass != 0)))
                        {
                            mrmID = Convert.ToInt32(waitDoctor.mrm_id);
                            mvtID = Convert.ToInt32(waitDoctor.mvt_id);
                        }
                        else
                        {
                            bool gotoEye = false;
                            if (type == Class.SendType.Skip || type == Class.SendType.Pending)
                            {
                                gotoEye = false;
                            }
                            else
                            {
                                gotoEye = CheckAutoGotoEye(ref mrmID, ref mvtID, ref Iscompleted, ref isHaveNextRoom, ref IsCallLab, ref SendEyeDoc);
                                if (Iscompleted)
                                {
                                    statusTran = StatusTransaction.True;
                                }
                                else
                                {
                                    statusTran = StatusTransaction.False;
                                }
                            }
                            if (gotoEye == false)
                            {
                                // ห้องที่ส่งได้
                                var objPatientRoom = (from t1 in dbc.vw_patient_rooms
                                                      where t1.mhs_id == SendtoSiteId
                                                      && t1.tpr_id == tpr_id
                                                      && t1.send_type == true
                                                      select t1.mvt_id).ToList();

                                //เช็คว่ามีห้องตรวจถัดไปหรือไม่
                                var objEventroom = (from t1 in dbc.mst_room_events
                                                    where t1.mrm_id == Program.CurrentRoom.mrm_id
                                                    //&& t1.mvt_id == ((Program.CurrentRoom.mst_room_hdr.mrm_code == "TE" || Program.CurrentRoom.mst_room_hdr.mrm_code == "EM") ? Program.CurrentPatient_queue.mvt_id : t1.mvt_id)
                                                    select t1.mvt_id).ToList();

                                //เช็คห้องเฉพาะที่มีการ Active เท่านั้น
                                var objRoomActive = (from t1 in dbc.mst_room_events
                                                     join t2 in dbc.mst_room_hdrs on t1.mrm_id equals t2.mrm_id
                                                     where t2.mrm_status == 'A'
                                                     && t2.mhs_id == SendtoSiteId
                                                     select t1.mvt_id).ToList();

                                //Check ห้องเฉพาะ ที่มีการ Login เท่านั้น
                                var objRoomLogin = (from t1 in dbc.log_user_logins
                                                    join t2 in dbc.mst_room_dtls on t1.mrd_id equals t2.mrd_id
                                                    join t3 in dbc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                    join t4 in dbc.mst_room_events on t3.mrm_id equals t4.mrm_id
                                                    join t5 in dbc.mst_events on t4.mvt_id equals t5.mvt_id
                                                    where t1.lug_end_date == null
                                                    && t1.lug_start_date.Value.Date == dateNow.Date
                                                    && t3.mhs_id == Program.CurrentSite.mhs_id
                                                    select t5.mvt_id).Distinct().ToList();

                                //จำนวน Station ที่ Active งานจาก Station ของผู้รับบริการที่เข้ามาตรวจ โดยไม่รวม Station ปัจจุบันที่ตรวจอยู่
                                var icountnextRoom = (from t1 in dbc.trn_patient_plans
                                                      where t1.tpr_id == tpr_id
                                                      && t1.tpl_status == 'N'
                                                      && !objEventroom.Contains(t1.mvt_id)
                                                          //Edit Sumit
                                                      && objRoomActive.Contains(t1.mvt_id)
                                                      select t1).Count();

                                //จำนวน Station ที่เปิดใช้งานจาก Station ของผู้รับบริการที่เข้ามาตรวจ โดยไม่รวม Station ปัจจุบันที่ตรวจอยู่
                                var icountActiveRoom = (from t1 in dbc.trn_patient_plans
                                                        where t1.tpr_id == tpr_id
                                                        && t1.tpl_status == 'N'
                                                        && !objEventroom.Contains(t1.mvt_id)
                                                            //Edit Sumit
                                                        && objRoomActive.Contains(t1.mvt_id)
                                                        && objRoomLogin.Contains(t1.mvt_id)
                                                        select t1).Count();

                                //Check Station ที่มีการ Skip
                                var icountRoomNoSkiponZone = (from t1 in dbc.trn_patient_plans
                                                              join t2 in dbc.mst_room_events on t1.mvt_id equals t2.mvt_id
                                                              join t3 in dbc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                              where t1.tpr_id == tpr_id
                                                              && t3.mhs_id == SendtoSiteId
                                                              && t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                              && t1.tpl_status == 'N'
                                                              && (t1.tpl_skip == false || t1.tpl_skip == null)
                                                              && t3.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
                                                              && !objEventroom.Contains(t1.mvt_id)
                                                              && objRoomActive.Contains(t1.mvt_id)
                                                              && objRoomLogin.Contains(t1.mvt_id)
                                                              && objPatientRoom.Contains(t1.mvt_id)
                                                              select t1).Count();

                                var icountRoomNoSkiponSite = (from t1 in dbc.trn_patient_plans
                                                              join t2 in dbc.mst_room_events on t1.mvt_id equals t2.mvt_id
                                                              join t3 in dbc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                              where t1.tpr_id == tpr_id
                                                              && t3.mhs_id == SendtoSiteId
                                                                  //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                              && t1.tpl_status == 'N'
                                                              && (t1.tpl_skip == false || t1.tpl_skip == null)
                                                                  //&& t3.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
                                                              && !objEventroom.Contains(t1.mvt_id)
                                                              && objRoomActive.Contains(t1.mvt_id)
                                                              && objRoomLogin.Contains(t1.mvt_id)
                                                              && objPatientRoom.Contains(t1.mvt_id)
                                                              select t1).Count();

                                if (icountnextRoom > 0)
                                {
                                    //count LoopOver =3
                                    int loopSearchNextRoom = 0;
                                    //เวลาที่ต้องรอในห้องในต้องไม่เกิน Limitvalue
                                    double Limitvalue = (from t1 in dbc.mst_config_dtls
                                                         where t1.mst_config_hdr.mfh_code == "SQ"
                                                         && t1.mst_config_hdr.mhs_id == Program.CurrentSite.mhs_id
                                                         && t1.mfd_status == 'A'
                                                         select (double)t1.mfd_value).FirstOrDefault();
                                CheckAgain:
                                    // Add Condition Send from Checkpoint B
                                    if (Program.CurrentRoom.mst_room_hdr.mrm_code == "CB")
                                    {
                                        objnextroom = (from t1 in dbc.vw_patient_rooms
                                                       where t1.tpr_id == tpr_id
                                                           //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                       && t1.mrm_id != Program.CurrentRoom.mrm_id
                                                           //&& t1.mvt_type_cate == 'M'
                                                       && t1.mvt_type_cate ==
                                                       (GetUltrasound_Type(tpr_id, SendtoSiteId) == true ||
                                                       (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                                                           //&& t1.mvt_code != ((from t in dbc.trn_patient_regis where t.tpr_id == t1.tpr_id && t.tpr_miss_lower == true select t).Count() > 0 ? "UL" : "ZZ")
                                                       && t1.mvt_code != (CheckExcept_Lower(tpr_id, SendtoSiteId) == true ? "UL" : "ZZ")
                                                       && t1.mhs_id == SendtoSiteId
                                                       && t1.site_rm == SendtoSiteId
                                                       && t1.waiting_person == 0
                                                       && t1.login_flag == ((icountActiveRoom > 0) ? "Y" : "N")
                                                       && t1.send_type == true
                                                       orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                                       select t1).FirstOrDefault();
                                    }
                                    else
                                    {
                                        if (icountRoomNoSkiponZone > 0)
                                        {
                                            objnextroom = (from t1 in dbc.vw_patient_rooms
                                                           where t1.tpr_id == tpr_id
                                                           && t1.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
                                                               //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                           && t1.mrm_id != Program.CurrentRoom.mrm_id
                                                               //&& t1.mvt_type_cate == 'M'
                                                           && t1.mvt_type_cate ==
                                                           (GetUltrasound_Type(tpr_id, SendtoSiteId) == true ||
                                                           (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                                                               //&& t1.mvt_code != ((from t in dbc.trn_patient_regis where t.tpr_id == t1.tpr_id && t.tpr_miss_lower == true select t).Count() > 0 ? "UL" : "ZZ")
                                                           && t1.mvt_code != (CheckExcept_Lower(tpr_id, SendtoSiteId) == true ? "UL" : "ZZ")
                                                           && t1.mhs_id == SendtoSiteId
                                                           && t1.site_rm == SendtoSiteId
                                                           && t1.waiting_time <= Limitvalue
                                                               // Add Sumit
                                                           && t1.login_flag == ((icountActiveRoom > 0) ? "Y" : "N")
                                                           && t1.skip_type == false
                                                           && t1.send_type == true
                                                           orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                                           select t1).FirstOrDefault();
                                        }
                                        else if (icountRoomNoSkiponSite == 0)
                                        {
                                            objnextroom = (from t1 in dbc.vw_patient_rooms
                                                           where t1.tpr_id == tpr_id
                                                           && t1.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
                                                               //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                           && t1.mrm_id != Program.CurrentRoom.mrm_id
                                                               //&& t1.mvt_type_cate == 'M'
                                                           && t1.mvt_type_cate ==
                                                           (GetUltrasound_Type(tpr_id, SendtoSiteId) == true ||
                                                           (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                                                               //&& t1.mvt_code != ((from t in dbc.trn_patient_regis where t.tpr_id == t1.tpr_id && t.tpr_miss_lower == true select t).Count() > 0 ? "UL" : "ZZ")
                                                           && t1.mvt_code != (CheckExcept_Lower(tpr_id, SendtoSiteId) == true ? "UL" : "ZZ")
                                                           && t1.mhs_id == SendtoSiteId
                                                           && t1.site_rm == SendtoSiteId
                                                           && t1.waiting_time <= Limitvalue
                                                               // Add Sumit
                                                           && t1.login_flag == ((icountActiveRoom > 0) ? "Y" : "N")
                                                               //&& t1.skip_type == false
                                                           && t1.send_type == true
                                                           orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                                           select t1).FirstOrDefault();
                                        }
                                        else
                                        {
                                            objnextroom = null;
                                        }
                                        //select t1).ToList();
                                    }

                                    /*if (icountActiveRoom > 0)
                                    {
                                        objnextroom = objnextroom.Where(x => x.login_flag == "Y").ToList().FirstOrDefault();
                                    }*/

                                    if (objnextroom != null)
                                    {
                                        mrmID = Convert.ToInt32(objnextroom.mrm_id);
                                        mvtID = Convert.ToInt32(objnextroom.mvt_id);
                                        mvtcode = objnextroom.mvt_code;
                                    }
                                    else
                                    {
                                        //var objnextSiteroom = (from t1 in dbc.vw_patient_rooms
                                        //                       where t1.mhs_id == SendtoSiteId
                                        //                       && t1.tpr_id == Program.CurrentRegis.tpr_id
                                        //                       && t1.site_rm == Program.CurrentSite.mhs_id
                                        //                       && t1.waiting_time <= Limitvalue
                                        //                       orderby t1.waiting_time
                                        //                       select t1).FirstOrDefault();
                                        //
                                        if (icountRoomNoSkiponSite > 0)
                                        {
                                            objnextSiteroom = (from t1 in dbc.vw_patient_rooms
                                                               where t1.mhs_id == SendtoSiteId
                                                               && t1.tpr_id == tpr_id
                                                                   //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                               && t1.mrm_id != Program.CurrentRoom.mrm_id
                                                                   //&& t1.mvt_type_cate == 'M'
                                                               && t1.mvt_type_cate ==
                                                               (GetUltrasound_Type(tpr_id, SendtoSiteId) == true ||
                                                               (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                                                               && t1.mvt_code != (CheckExcept_Lower(tpr_id, SendtoSiteId) == true ? "UL" : "ZZ")
                                                               && t1.site_rm == SendtoSiteId
                                                               && t1.waiting_time <= Limitvalue
                                                                   //Add Sumit  
                                                               && t1.login_flag == ((icountActiveRoom > 0) ? "Y" : "N")
                                                               && t1.skip_type == false
                                                               && t1.send_type == true
                                                               orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                                               select t1).FirstOrDefault();

                                            //var tempResult = dbc.vw_patient_rooms.Where(x => x.mhs_id == SendtoSiteId &&
                                            //                                                 x.tpr_id == tpr_id &&
                                            //                                                 x.mrm_id != Program.CurrentRoom.mrm_id &&
                                            //                                                 x.mvt_type_cate == (GetUltrasound_Type(tpr_id, SendtoSiteId) == true ||
                                            //                                                 (x.mvt_code != "UL" && x.mvt_code != "UB" && x.mvt_code != "UW") ? 'M' : x.mvt_type_cate) &&
                                            //                                                 x.mvt_code != (CheckExcept_Lower(tpr_id, SendtoSiteId) == true ? "UL" : "ZZ") &&
                                            //                                                 x.site_rm == SendtoSiteId &&
                                            //                                                 x.login_flag == ((icountActiveRoom > 0) ? "Y" : "N") &&
                                            //                                                 x.skip_type == false &&
                                            //                                                 x.send_type == true).ToList();

                                            //var Result = tempResult.Where(x => x.waiting_time <= Limitvalue && x.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id).ToList();
                                            //if (Result.Count() > 0)
                                            //{
                                            //    objnextSiteroom = Result.OrderBy(x => new { }).FirstOrDefault();
                                            //}
                                            //else
                                            //{

                                            //}

                                        }
                                        else
                                        {
                                            objnextSiteroom = (from t1 in dbc.vw_patient_rooms
                                                               where t1.mhs_id == SendtoSiteId
                                                               && t1.tpr_id == tpr_id
                                                                   //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                               && t1.mrm_id != Program.CurrentRoom.mrm_id
                                                                   //&& t1.mvt_type_cate == 'M'
                                                               && t1.mvt_type_cate ==
                                                               (GetUltrasound_Type(tpr_id, SendtoSiteId) == true ||
                                                               (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                                                               && t1.mvt_code != (CheckExcept_Lower(tpr_id, SendtoSiteId) == true ? "UL" : "ZZ")
                                                               && t1.site_rm == SendtoSiteId
                                                               && t1.waiting_time <= Limitvalue
                                                                   //Add Sumit  
                                                               && t1.login_flag == ((icountActiveRoom > 0) ? "Y" : "N")
                                                                   //&& t1.skip_type == false
                                                               && t1.send_type == true
                                                               orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                                               select t1).FirstOrDefault();
                                        }

                                        if (objnextSiteroom != null)
                                        {
                                            mrmID = Convert.ToInt32(objnextSiteroom.mrm_id);
                                            mvtID = Convert.ToInt32(objnextSiteroom.mvt_id);
                                            mvtcode = objnextSiteroom.mvt_code;
                                        }
                                        else
                                        {
                                            if (loopSearchNextRoom >= 3)
                                            {
                                                if (Program.CurrentRoom.mst_room_hdr.mrm_code == "CB")
                                                {
                                                    //dbc.Transaction.Rollback();
                                                    Iscompleted = false;
                                                    Program.RefreshWaiting = true;
                                                    System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งแบบอัตโนมัติได้ กรุณากดปุ่ม Send Manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                                    //return Iscompleted;
                                                    return StatusTransaction.False;
                                                }
                                                else
                                                {
                                                    objnextSiteroom = (from t1 in dbc.vw_patient_rooms
                                                                       where t1.mhs_id == SendtoSiteId
                                                                       && t1.tpr_id == tpr_id
                                                                           //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                                       && t1.mrm_id != Program.CurrentRoom.mrm_id
                                                                           //&& t1.mvt_type_cate == 'M'
                                                                       && t1.mvt_type_cate ==
                                                                        (GetUltrasound_Type(tpr_id, SendtoSiteId) == true ||
                                                                        (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                                                                       && t1.mvt_code != (CheckExcept_Lower(tpr_id, SendtoSiteId) == true ? "UL" : "ZZ")
                                                                           //Add Sumit 
                                                                       && t1.login_flag == ((icountActiveRoom > 0) ? "Y" : "N")
                                                                       && t1.send_type == true
                                                                       orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                                                       select t1).FirstOrDefault();
                                                    if (objnextSiteroom != null)
                                                    {
                                                        mrmID = Convert.ToInt32(objnextSiteroom.mrm_id);
                                                        mvtID = Convert.ToInt32(objnextSiteroom.mvt_id);
                                                        mvtcode = objnextSiteroom.mvt_code;
                                                    }
                                                    else
                                                    {
                                                        if (Program.CurrentRegis.tpr_miss_lower == true)
                                                        {
                                                            objLower = (from t1 in dbc.vw_patient_rooms
                                                                        where t1.mhs_id == SendtoSiteId
                                                                        && t1.tpr_id == tpr_id
                                                                        && t1.mrm_id != Program.CurrentRoom.mrm_id
                                                                        && t1.mrm_code == "US"
                                                                        && t1.mvt_code == "UL"
                                                                        orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                                                        select t1).FirstOrDefault();
                                                            if (objLower != null)
                                                            {
                                                                System.Windows.Forms.MessageBox.Show("ระบบเหลือห้องที่ไม่สามารถข้ามได้แล้ว ระบบจะส่งไปห้อง Ultrasound Lower อัตโนมัติ", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                                                mrmID = Convert.ToInt32(objLower.mrm_id);
                                                                mvtID = Convert.ToInt32(objLower.mvt_id);
                                                                mvtcode = objLower.mvt_code;
                                                                statusTran = StatusTransaction.True;
                                                            }
                                                            else
                                                            {
                                                                Iscompleted = false;
                                                                Program.RefreshWaiting = true;
                                                                //return Iscompleted;
                                                                return StatusTransaction.SendCheckB;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Iscompleted = false;
                                                            Program.RefreshWaiting = true;
                                                            //System.Windows.Forms.MessageBox.Show("Can't send to next room. Please contact Admin or Send Manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                                            //return Iscompleted;
                                                            return StatusTransaction.SendCheckB;
                                                        }
                                                    }
                                                }
                                            }

                                            loopSearchNextRoom = loopSearchNextRoom + 1;

                                            //ถ้าไม่มีข้นตอนก็ทำขั้นตอนถัดไป
                                            //var objothSiteroom = (from t1 in dbc.vw_patient_rooms
                                            //                      where t1.mhs_id == Program.CurrentSite.mhs_id
                                            //                      && t1.tpr_id == Program.CurrentRegis.tpr_id
                                            //                      && t1.site_rm != Program.CurrentSite.mhs_id
                                            //                      && t1.waiting_time <= Limitvalue
                                            //                      orderby t1.waiting_time
                                            //                      select t1).FirstOrDefault();
                                            //if (objothSiteroom != null)
                                            //{
                                            //    mrmID = Convert.ToInt32(objnextSiteroom.mrm_id);
                                            //    mvtID = Convert.ToInt32(objnextSiteroom.mvt_id);
                                            //    mvtcode = objnextSiteroom.mvt_code;
                                            //}
                                            //else
                                            //{
                                            //    Limitvalue = 1440;
                                            //    goto CheckAgain;
                                            //}
                                            if (mrmID == 0)
                                            {
                                                Limitvalue = 1440;
                                                goto CheckAgain;
                                            }

                                        }
                                    }

                                    /*var mvt_Code = (from t in dbc.mst_events where t.mvt_id == mvtID select t.mvt_code).FirstOrDefault();

                                    if (mvt_Code == "PE")
                                    {
                                        if (System.Windows.Forms.MessageBox.Show("ห้องที่จะส่งถัดไปเป็น PE Station คุณยืนยันที่จะส่งไปหรือไม่", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                                        {
                                            SetPatientPlanStatus(dbc, Program.CurrentRegis.tpr_id, mrmID);
                                            loopSearchNextRoom = 2;
                                            goto CheckAgain;
                                        }
                                    }*/
                                    // เช็คลำดับขั้นการเข้าห้อง
                                    //CheckNextRoom(ref mrmID, ref mvtID, mvtcode);
                                }
                                else
                                {
                                    try
                                    {
                                        if (Program.CurrentRegis.tpr_PRM_check == true && Program.CurrentRegis.tpr_pe_status == "RS" && Program.CurrentRoom.mst_room_hdr.mrm_code == "PT")
                                        {
                                            frmChoicePRM frm = new frmChoicePRM();
                                            var result = frm.ShowDialog();
                                            if (result == DialogResult.Yes)
                                            {
                                                mrmID = mst.GetMstRoomHdr("CC").mrm_id;
                                                mvtID = mst.GetMstEvent("CC").mvt_id;
                                                //mrmID = Program.Getmrm_id(dbc, "CC");
                                                //mvtID = Program.Getmvt_id(dbc, "CC");
                                                mvtcode = "CC";
                                                isHaveNextRoom = true;
                                                IsCallLab = true;

                                                if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                                                {

                                                }
                                            }
                                            else if (result == DialogResult.No)
                                            {
                                                DateTime dtnow = Program.GetServerDateTime();
                                                string roomname = String.Empty;
                                                mrmID = mst.GetMstRoomHdr("BK").mrm_id;
                                                mvtID = mst.GetMstEvent("BK").mvt_id;
                                                //mrmID = Program.Getmrm_id(dbc, "BK");
                                                //mvtID = Program.Getmvt_id(dbc, "BK");
                                                /*var getHnEn = (from t in dbc.trn_patient_regis join t2 in dbc.trn_patients on t.tpt_id equals t2.tpt_id where t.tpr_id == tpr_id select new { t, t2 }).ToList();
                                                var objevent = (from t1 in dbc.mst_events where t1.mvt_id == mvtID select t1).ToList();
                                                if (objevent.Count != 0)
                                                {
                                                    roomname = objevent[0].mvt_ename;
                                                }*/
                                                /*var objqueueBK = (from t1 in dbc.trn_patient_queues
                                                                  where t1.trn_patient_regi.trn_patient.tpt_hn_no == getHnEn.Select(x => x.t2.tpt_hn_no).FirstOrDefault()
                                                                   && t1.trn_patient_regi.tpr_en_no == getHnEn.Select(x => x.t.tpr_en_no).FirstOrDefault()
                                                                   && t1.mrm_id == mrmID
                                                                   && t1.mvt_id == mvtID
                                                                  select t1).FirstOrDefault();*/

                                                var objRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                                                if (objRegis != null)
                                                {
                                                    objRegis.tpr_status = "WB";
                                                    objRegis.tpr_pe_status = "RS";
                                                }

                                                /*if (objqueueBK == null && mrmID != 0)
                                                {
                                                    trn_patient_queue newitem = new trn_patient_queue();
                                                    newitem.tpr_id = tpr_id;
                                                    newitem.mrm_id = mrmID;
                                                    newitem.mvt_id = mvtID;
                                                    newitem.mrd_id = null;
                                                    newitem.tps_end_date = null;
                                                    newitem.tps_start_date = null;
                                                    newitem.tps_status = "NS";
                                                    newitem.tps_ns_status = "QL";
                                                    newitem.tps_create_by = Program.CurrentUser.mut_username;
                                                    newitem.tps_create_date = dtnow;
                                                    newitem.tps_update_by = Program.CurrentUser.mut_username;
                                                    newitem.tps_update_date = dtnow;
                                                    dbc.trn_patient_queues.InsertOnSubmit(newitem);
                                                    dbc.SubmitChanges();
                                                    //MessageBox.Show("Send Completed. Send To" + roomname, "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else if (objqueueBK != null && mrmID != 0)
                                                {
                                                    objqueueBK.mrd_id = null;
                                                    objqueueBK.tps_status = "NS";
                                                    objqueueBK.tps_ns_status = "QL";
                                                    objqueueBK.tps_create_date = dtnow;
                                                    objqueueBK.tps_create_by = Program.CurrentUser.mut_username;
                                                    objqueueBK.tps_update_by = objqueueBK.tps_create_by;
                                                    objqueueBK.tps_update_date = dtnow;
                                                    dbc.SubmitChanges();
                                                    //MessageBox.Show("Send Completed. Sent To" + roomname, "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }*/
                                                /*if (mrmID == Program.Getmrm_id(dbc, "BK"))
                                                {
                                                    MessageBox.Show("Checkup Process Completed", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }*/
                                                MessageBox.Show("Checkup Process Completed", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                mvtcode = "BK";
                                                isHaveNextRoom = false;
                                                IsCallLab = true;
                                                //dbc.Transaction.Commit();
                                                /*Program.CurrentRegis = null;
                                                Program.CurrentPatient_queue = null;
                                                AlertOutDepartment.StopTime();
                                                Iscompleted = true;
                                                return true; */

                                                //Update
                                                //mrmID = 
                                            }
                                        }
                                        else
                                        {
                                            //ถ้าไม่ห้องถัดไปให้ไปที่ checkpoint CC
                                            mrmID = mst.GetMstRoomHdr("CC").mrm_id;
                                            mvtID = mst.GetMstEvent("CC").mvt_id;
                                            //mrmID = Program.Getmrm_id(dbc, "CC");
                                            //mvtID = Program.Getmvt_id(dbc, "CC");
                                            mvtcode = "CC";
                                            isHaveNextRoom = true;
                                            IsCallLab = true;

                                            //เพิ่มเงื่อนไข 
                                            if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                                            {

                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception(ex.Message);
                                    }
                                }
                            }
                            /*else
                            {
                                CheckNextRoom(ref mrmID, ref mvtID, mvtcode);
                            }*/
                        }

                        // Eye Station Edit 13/06/2014
                        /*var chk_EyeXRay_mrm = (from t in dbc.mst_room_hdrs where t.mrm_id == mrmID select t.mrm_code).FirstOrDefault();
                        var EyeXRay_mvt = (from t in dbc.mst_events where t.mvt_code == "TX" select t.mvt_id).FirstOrDefault();
                        var EyeDoc_mvt = (from t in dbc.mst_events where t.mvt_code == "TE" select t.mvt_id).FirstOrDefault();
                        int ct_EyeXRay = (from t in dbc.trn_patient_plans where t.tpr_id == tpr_id && t.mvt_id == EyeXRay_mvt && t.tpl_status == 'N' select t).Count();
                        int ct_EyeDoc = (from t in dbc.trn_patient_plans where t.tpr_id == tpr_id && t.mvt_id == EyeDoc_mvt && t.tpl_status == 'N' select t).Count();

                        if (Program.CurrentPatient_queue.mvt_id == EyeXRay_mvt)
                        {
                            if (chk_EyeXRay_mrm == "TE" && ct_EyeDoc > 0)
                            {
                                mvtID = EyeDoc_mvt;
                            }
                        }
                        else if (Program.CurrentPatient_queue.mvt_id == EyeDoc_mvt)
                        {
                            if (chk_EyeXRay_mrm == "TE" && ct_EyeXRay > 0)
                            {
                                mvtID = EyeXRay_mvt;
                            }
                        }*/

                        Getmvtid = mvtID;
                        Getmrmid = mrmID;


                        ////เช้คว่ามี Record ซ้ำหรือไม่ ถ้ามีแล้วไม่ต้องทำการสร้างใหม่ เพราะจะทำให้ Queue มีหลายQueue ใน Waitinglist
                        //var objqueue = (from t1 in dbc.trn_patient_queues
                        //           where t1.tpr_id == tpr_id
                        //               && t1.mrm_id == mrmID
                        //               && t1.mvt_id == mvtID
                        //           select t1).FirstOrDefault();
                        //if (objqueue == null)
                        //{
                        //    // insert table trn_patient_Queue
                        //    trn_patient_queue newqueue = new trn_patient_queue();
                        //    newqueue.tpr_id = Program.CurrentRegis.tpr_id;
                        //    newqueue.mrm_id = mrmID;
                        //    newqueue.mvt_id = mvtID;
                        //    newqueue.mrd_id = null;
                        //    newqueue.tps_start_date = Program.GetServerDateTime();
                        //    newqueue.tps_end_date = Program.GetServerDateTime();

                        //    /*var chk_CC = (from t1 in dbc.mst_room_hdrs
                        //                  where t1.mrm_id == mrmID
                        //                  select t1.mrm_code).FirstOrDefault();

                        //    if (chk_CC == "CC")
                        //    {
                        //        newqueue.tps_status = "WK";
                        //        newqueue.tps_ns_status = null;
                        //    }
                        //    else
                        //    {
                        //        newqueue.tps_status = "NS";
                        //        newqueue.tps_ns_status = "QL";
                        //    }*/
                        //    if (isHaveNextRoom)
                        //    {
                        //        newqueue.tps_status = "WK";
                        //        newqueue.tps_ns_status = null;
                        //    }
                        //    else
                        //    {
                        //        newqueue.tps_status = "NS";
                        //        newqueue.tps_ns_status = "QL";
                        //    }
                        //    newqueue.tps_create_by = Program.CurrentUser.mut_username;
                        //    newqueue.tps_create_date = Program.GetServerDateTime();
                        //    newqueue.tps_update_by = Program.CurrentUser.mut_username;
                        //    newqueue.tps_update_date = newqueue.tps_create_date;
                        //    dbc.trn_patient_queues.InsertOnSubmit(newqueue);



                        //}
                        //else
                        //{
                        //    //Edit
                        //    if (isHaveNextRoom)
                        //    {
                        //        objqueue.tps_status = "WK";
                        //        objqueue.tps_ns_status = null;
                        //    }
                        //    else
                        //    {
                        //        objqueue.tps_status = "NS";
                        //        objqueue.tps_ns_status = "QL";
                        //    }
                        //    objqueue.mrd_id = null;
                        //    objqueue.tps_start_date = Program.GetServerDateTime();

                        //    objqueue.tps_create_date = Program.GetServerDateTime();
                        //    objqueue.tps_create_by = Program.CurrentUser.mut_username;
                        //    objqueue.tps_update_by = objqueue.tps_create_by;
                        //    objqueue.tps_update_date = Program.GetServerDateTime();


                        //}

                        //if (ConditionPEsite2)
                        //{
                        //    //if (objqueue == null) objqueue = new trn_patient_queue();
                        //    //// ส่งเข้า Book Page
                        //    trn_patient_queue tps = new trn_patient_queue()
                        //    {
                        //       tps_status = "ED",
                        //       tps_ns_status = null,
                        //       tps_end_date = dateNow,
                        //       tps_send_by = Program.CurrentUser.mut_username,
                        //       tps_update_by = Program.CurrentUser.mut_username,
                        //       tps_update_date = dateNow

                        //    };

                        //    //// ส่งเข้า Book Page
                        //    trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        //    tpr.trn_patient_queues.Add(tps);
                        //    tpr.tpr_pe_status = "RS";
                        //    tpr.tpr_status = "WB";
                        //}

                        trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        trn_patient_queue objQueue = tpr.trn_patient_queues.Where(x => x.mrm_id == mrmID &&
                                                                                       x.mvt_id == mvtID).FirstOrDefault();
                        if (objQueue == null)
                        {
                            objQueue = new trn_patient_queue();
                            //dbc.trn_patient_queues.InsertOnSubmit(objQueue);
                            tpr.trn_patient_queues.Add(objQueue);
                            objQueue.mrm_id = mrmID;
                            objQueue.mvt_id = mvtID;
                        }
                        objQueue.mrd_id = null;
                        //objQueue.tps_start_date = dateNow;
                        //objQueue.tps_end_date = Program.GetServerDateTime();
                        if (isHaveNextRoom)
                        {
                            objQueue.tps_status = "WK";
                            objQueue.tps_ns_status = null;
                        }
                        else
                        {
                            objQueue.tps_status = "NS";
                            objQueue.tps_ns_status = "QL";
                        }

                        if (SendEyeDoc)
                        {
                            objQueue.tps_bm_seq = null;
                            objQueue.tps_call_status = "HD";
                            objQueue.tps_hold_by = Program.CurrentUser.mut_username;
                            objQueue.tps_hold_date = dateNow.AddMinutes(limittime);
                        }

                        objQueue.tps_create_by = Program.CurrentUser.mut_username;
                        objQueue.tps_create_date = dateNow;
                        objQueue.tps_update_by = Program.CurrentUser.mut_username;
                        objQueue.tps_update_date = dateNow;

                        /*if (ConditionPEsite2)
                        {
                            objQueue.tps_status = "ED";
                            objQueue.tps_ns_status = null;
                            objQueue.tps_end_date = dateNow;
                            objQueue.tps_send_by = Program.CurrentUser.mut_username;
                            objQueue.tps_update_by = Program.CurrentUser.mut_username;
                            objQueue.tps_update_date = dateNow;

                            //// ส่งเข้า Book Page
                            tpr.tpr_pe_status = "RS";
                            tpr.tpr_status = "WB";
                        }*/

                        trn_patient_queue tps = dbc.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
                        if (tps != null)
                        {
                            if (type == Class.SendType.Normal)
                            {
                                tps.mrd_id = Program.CurrentRoom.mrd_id;
                                tps.tps_send_by = Program.CurrentUser.mut_username;
                                tps.tps_end_date = dateNow;
                                tps.tps_status = "ED";
                                tps.tps_ns_status = null;
                                tps.tps_update_by = Program.CurrentUser.mut_username;
                                tps.tps_update_date = dateNow;
                            }
                            else if (type == Class.SendType.Skip)
                            {
                                dbc.trn_patient_queues.DeleteOnSubmit(tps);
                            }
                            else if (type == Class.SendType.Pending)
                            {
                                if (PassCheckPointB)
                                {
                                    dbc.trn_patient_queues.DeleteOnSubmit(tps);
                                }
                                else
                                {
                                    tps.tps_status = "PD";
                                }
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

                        dbc.SubmitChanges();
                        dbc.Transaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbc.Transaction.Rollback();
                        Iscompleted = false;
                        statusTran = StatusTransaction.Error;
                    }
                    finally
                    {
                        dbc.Connection.Close();
                    }
                }

                //if (new EmrClass.GetDataMasterCls().GetMstRoomHdr(mrmID).mrm_code == "DC")
                //{
                //new Class.FunctionDataCls().stampPEDoctor(Program.CurrentRegis.tpr_id);
                //}

                // morn clear Unit Display
                //new ClsTCPClient().sendClearUnitDisplay();

                //Program.CurrentRegis = null;
                //Program.CurrentPatient_queue = null;
                AlertOutDepartment.StopTime();
                Iscompleted = true;
                statusTran = StatusTransaction.True;

                Program.RefreshWaiting = true;

                ////noina cr.

                if (IsCallLab)
                {
                    CallLab(tpt_id, tpr_id);
                    //try
                    //{
                    //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    //    {
                    //        var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tptID).FirstOrDefault();
                    //        //CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no,Program.CurrentRegis.tpr_id);
                    //        CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no, tpr_id);
                    //        ClsBasicMeasurement.SaveBasicMeasurment(tpr_id, IsCallLab);

                    //        //Update LR สูติอย่างเดียวเนื่องจากไม่มีใน callDataTakeCare Event Code สูติ= PT
                    //        SetLR_Obstaticresult(dbc, tpr_id);
                    //    }

                    //}
                    //catch (Exception)
                    //{
                    //}

                    //try
                    //{
                    //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    //    {
                    //        var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tptID).FirstOrDefault();
                    //        CheckUpLabClass.ws_Getcheckuplab_Async(currentPatient.tpt_hn_no, tpr_id);
                    //        //dbc.Connection.Close();
                    //    }

                    //}
                    //catch (Exception ex)
                    //{
                    //    Program.MessageError("ws => getcheckuplab :" + ex.Message);
                    //}
                }
                return statusTran;
            }
            catch (Exception ex)
            {
                Program.MessageError("CallQueue", "PSendAutoAllRoom", ex, false);
                return StatusTransaction.Error;
            }
            finally
            {
                Program.RefreshWaiting = true;
            }
        }
        public static void CallLab(int tpt_id, int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tpt_id).FirstOrDefault();
                    string en = currentPatient.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.tpr_en_no).FirstOrDefault();
                    CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no, tpr_id);
                    using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                    {
                        ws.InsertDBEmrCheckupResultXrayBackground(currentPatient.tpt_hn_no, en, dateNow.AddYears(-5), dateNow, true);
                        ws.retrieveVitalSignBackground(tpr_id);
                        ws.getCheckUpLabResultBackground(tpr_id);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("CallQueue", "CallLab(int tpt_id, int tpr_id)", ex, false);
            }

            //try
            //{
            //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            //    {
            //        var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tpt_id).FirstOrDefault();
            //        //CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no,Program.CurrentRegis.tpr_id);
            //        CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no, tpr_id);
            //        ClsBasicMeasurement.SaveBasicMeasurment(tpr_id, true);

            //        //Update LR สูติอย่างเดียวเนื่องจากไม่มีใน callDataTakeCare Event Code สูติ= PT
            //        SetLR_Obstaticresult(dbc, tpr_id);
            //    }

            //}
            //catch (Exception)
            //{
            //}

            //try
            //{
            //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            //    {
            //        var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tpt_id).FirstOrDefault();
            //        CheckUpLabClass.ws_Getcheckuplab_Async(currentPatient.tpt_hn_no, tpr_id);
            //        //dbc.Connection.Close();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Program.MessageError("CallQueue", "CallLab", ex, false);
            //}
        }

        public static bool CheckAutoGotoEye(ref int mrmID, ref int mvtID, ref bool IsCompleted, ref bool isHaveNextRoom, ref bool IsCallLab, ref bool SendEyeDoc)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                DateTime dt = Program.GetServerDateTime();
                var currentRoom = (from t1 in cdc.mst_room_hdrs where t1.mrm_id == Program.CurrentRoom.mrm_id select t1).FirstOrDefault();
                if (currentRoom != null && currentRoom.mrm_code == "EM" && Program.CurrentRoom.mrd_type == 'N')
                {
                    // ทำกรณีที่เป็น Nurse เท่านั้น
                    var tehid = (from t1 in cdc.trn_eye_exam_hdrs where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                    if (tehid != null)
                    {
                        if (tehid.teh_eyedropper == true)
                        {
                            var timelimit = Program.GetLimitTime("EDT");

                            // ห้องที่ส่งได้
                            var objPatientRoom = (from t1 in cdc.vw_patient_rooms
                                                  where t1.mhs_id == Program.CurrentSite.mhs_id
                                                  && t1.tpr_id == Program.CurrentRegis.tpr_id
                                                  && t1.send_type == true
                                                  select t1.mvt_id).ToList();

                            //Edit Sumit
                            var objEventroom = (from t1 in cdc.mst_room_events
                                                where t1.mrm_id == Program.CurrentRoom.mrm_id
                                                select t1.mvt_id).ToList();

                            //เช็คห้องเฉพาะที่มีการ Active เท่านั้น
                            var objRoomActive = (from t1 in cdc.mst_room_events
                                                 join t2 in cdc.mst_room_hdrs on t1.mrm_id equals t2.mrm_id
                                                 where t2.mrm_status == 'A'
                                                 && t2.mhs_id == Program.CurrentSite.mhs_id
                                                 select t1.mvt_id).ToList();

                            //Check ห้องเฉพาะ ที่มีการ Login เท่านั้น
                            var objRoomLogin = (from t1 in cdc.log_user_logins
                                                join t2 in cdc.mst_room_dtls on t1.mrd_id equals t2.mrd_id
                                                join t3 in cdc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                join t4 in cdc.mst_room_events on t3.mrm_id equals t4.mrm_id
                                                join t5 in cdc.mst_events on t4.mvt_id equals t5.mvt_id
                                                where t1.lug_end_date == null
                                                && t1.lug_start_date.Value.Date == dt.Date
                                                && t3.mhs_id == Program.CurrentSite.mhs_id
                                                select t5.mvt_id).ToList();

                            //จำนวน Station ที่เปิดใช้งานจาก Station ของผู้รับบริการที่เข้ามาตรวจ โดยไม่รวม Station ปัจจุบันที่ตรวจอยู่
                            var icountActiveRoom = (from t1 in cdc.trn_patient_plans
                                                    where t1.tpr_id == Program.CurrentRegis.tpr_id
                                                    && t1.tpl_status == 'N'
                                                    && !objEventroom.Contains(t1.mvt_id)
                                                    && objRoomActive.Contains(t1.mvt_id)
                                                    && objRoomLogin.Contains(t1.mvt_id)
                                                    select t1).Count();

                            //Check Station ที่มีการ Skip
                            /*var icountRoomNoSkiponZone = (from t1 in cdc.trn_patient_plans
                                                          join t2 in cdc.mst_room_events on t1.mvt_id equals t2.mvt_id
                                                          join t3 in cdc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                          where t1.tpr_id == tpr_id
                                                          && t3.mhs_id == Program.CurrentSite.mhs_id
                                                          && t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                          && t1.tpl_status == 'N'
                                                          && (t1.tpl_skip == false || t1.tpl_skip == null)
                                                          && t3.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
                                                          && !objEventroom.Contains(t1.mvt_id)
                                                              //Edit Sumit
                                                            && objRoomActive.Contains(t1.mvt_id)
                                                            && objRoomLogin.Contains(t1.mvt_id)
                                                          select t1).Count();*/

                            var icountRoomNoSkiponSite = (from t1 in cdc.trn_patient_plans
                                                          join t2 in cdc.mst_room_events on t1.mvt_id equals t2.mvt_id
                                                          join t3 in cdc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                          where t1.tpr_id == tpr_id
                                                          && t3.mhs_id == Program.CurrentSite.mhs_id
                                                              //&& t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                                          && t1.tpl_status == 'N'
                                                          && (t1.tpl_skip == false || t1.tpl_skip == null)
                                                              //&& t3.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
                                                          && !objEventroom.Contains(t1.mvt_id)
                                                          && objRoomActive.Contains(t1.mvt_id)
                                                          && objRoomLogin.Contains(t1.mvt_id)
                                                          && objPatientRoom.Contains(t1.mvt_id)
                                                          select t1).Count();

                            var icountRoomInZone = (from t1 in cdc.vw_patient_rooms
                                                    where t1.tpr_id == tpr_id
                                                    && t1.mhs_id == Program.CurrentSite.mhs_id
                                                    && t1.mze_id == Program.CurrentRoom.mst_room_hdr.mze_id
                                                    && t1.send_type == true
                                                    && t1.skip_type == (icountRoomNoSkiponSite > 0 ? false : true)
                                                    && t1.waiting_time <= timelimit
                                                    && !objEventroom.Contains(t1.mvt_id)
                                                    && objRoomActive.Contains(t1.mvt_id)
                                                    && objRoomLogin.Contains(t1.mvt_id)
                                                    select t1).Count();

                            var queueWait = (from t1 in cdc.vw_patient_rooms
                                             //orderby t1.waiting_time, t1.mze_code
                                             where t1.tpr_id == tpr_id
                                             && t1.mhs_id == Program.CurrentSite.mhs_id
                                             && t1.mze_id == (icountRoomInZone > 0 ? Program.CurrentRoom.mst_room_hdr.mze_id : t1.mze_id)
                                                 //&& t1.waiting_time <= timelimit
                                             && !objEventroom.Contains(t1.mvt_id)
                                             && objRoomActive.Contains(t1.mvt_id)
                                             && t1.login_flag == ((icountActiveRoom > 0) ? "Y" : "N")
                                             && t1.mrm_code != "EM"
                                             && t1.mvt_code != (CheckExcept_Lower(tpr_id, Program.CurrentSite.mhs_id) == true ? "UL" : "ZZ")
                                                 //&& t1.mvt_type_cate == 'M'
                                             && t1.mvt_type_cate ==
                                            (GetUltrasound_Type(tpr_id, Program.CurrentSite.mhs_id) == true ||
                                            (t1.mvt_code != "UL" && t1.mvt_code != "UB" && t1.mvt_code != "UW") ? 'M' : t1.mvt_type_cate)
                                            && t1.send_type == true
                                             orderby t1.skip_type, t1.skip_seq, t1.waiting_time, t1.mze_code, t1.mrm_seq_room
                                             select t1).FirstOrDefault();

                            DateTime datenow = Program.GetServerDateTime();
                            if (queueWait != null)
                            {
                                //if (icountRoomNoSkiponSite == 0)
                                if (queueWait.waiting_time <= timelimit)
                                {
                                    // ส่ง คิวไปหาคนที่เจอ
                                    mrmID = Convert1.ToInt32(queueWait.mrm_id);
                                    mvtID = queueWait.mvt_id;
                                    //CheckNextRoom(ref mrmID, ref mvtID, queueWait.mvt_code);
                                }
                                else
                                {
                                    var objwaitRoom = (from t1 in cdc.mst_room_events
                                                       from mvt in cdc.mst_events
                                                       from mrm in cdc.mst_room_hdrs
                                                       where t1.mrm_id == mrm.mrm_id
                                                       && t1.mvt_id == mvt.mvt_id
                                                       && mrm.mrm_code == "EM"
                                                       && mvt.mvt_code == "EM"
                                                       && mrm.mhs_id == Program.CurrentSite.mhs_id
                                                       && mrm.mrm_status == 'A'
                                                           && (mrm.mrm_effective_date == null ||
                                                                   (mrm.mrm_effective_date.Value.Date <= datenow
                                                                   && (mrm.mrm_expire_date == null ||
                                                                       mrm.mrm_expire_date.Value.Date >= datenow))
                                                               )
                                                               && mvt.mvt_status == 'A'
                                                           && (mvt.mvt_effective_date == null ||
                                                                   (mvt.mvt_effective_date.Value.Date <= datenow
                                                                   && (mvt.mvt_expire_date == null ||
                                                                       mvt.mvt_expire_date.Value.Date >= datenow))
                                                               )
                                                       select new
                                                       {
                                                           mrm.mrm_id,
                                                           mvt.mvt_id
                                                       }).FirstOrDefault();
                                    if (objwaitRoom != null)
                                    {
                                        mrmID = objwaitRoom.mrm_id;
                                        mvtID = objwaitRoom.mvt_id;

                                        //Add เงื่อนไขถ้าส่งเข้าแพทย์เลยในกรณี Dialate
                                        SendEyeDoc = true;
                                    }
                                    else
                                    {
                                        IsCompleted = false;
                                    }
                                }
                            }
                            else
                            {//ข้อ 11

                                var objwaitRoom = (from t1 in cdc.mst_room_events
                                                   from mvt in cdc.mst_events
                                                   from mrm in cdc.mst_room_hdrs
                                                   where t1.mrm_id == mrm.mrm_id
                                                   && t1.mvt_id == mvt.mvt_id
                                                   && mrm.mrm_code == "EM"
                                                   && mvt.mvt_code == "EM"
                                                   && mrm.mhs_id == Program.CurrentSite.mhs_id
                                                   && mrm.mrm_status == 'A'
                                                       && (mrm.mrm_effective_date == null ||
                                                               (mrm.mrm_effective_date.Value.Date <= datenow
                                                               && (mrm.mrm_expire_date == null ||
                                                                   mrm.mrm_expire_date.Value.Date >= datenow))
                                                           )
                                                           && mvt.mvt_status == 'A'
                                                       && (mvt.mvt_effective_date == null ||
                                                               (mvt.mvt_effective_date.Value.Date <= datenow
                                                               && (mvt.mvt_expire_date == null ||
                                                                   mvt.mvt_expire_date.Value.Date >= datenow))
                                                           )
                                                   select new
                                                   {
                                                       mrm.mrm_id,
                                                       mvt.mvt_id
                                                   }).FirstOrDefault();
                                if (objwaitRoom != null)
                                {
                                    mrmID = objwaitRoom.mrm_id;
                                    mvtID = objwaitRoom.mvt_id;

                                    //Add เงื่อนไขถ้าส่งเข้าแพทย์เลยในกรณี Dialate
                                    SendEyeDoc = true;
                                }
                                else
                                {
                                    IsCompleted = false;
                                }
                            }
                        }
                        else
                        {//ข้อ 11
                            DateTime datenow = Program.GetServerDateTime();
                            var objwaitRoom = (from t1 in cdc.mst_room_events
                                               from mvt in cdc.mst_events
                                               from mrm in cdc.mst_room_hdrs
                                               where t1.mrm_id == mrm.mrm_id
                                               && t1.mvt_id == mvt.mvt_id
                                               && mrm.mrm_code == "EM"
                                               && mvt.mvt_code == "EM"
                                               && mrm.mhs_id == Program.CurrentSite.mhs_id
                                               && mrm.mrm_status == 'A'
                                                   && (mrm.mrm_effective_date == null ||
                                                           (mrm.mrm_effective_date.Value.Date <= datenow
                                                           && (mrm.mrm_expire_date == null ||
                                                               mrm.mrm_expire_date.Value.Date >= datenow))
                                                       )
                                                       && mvt.mvt_status == 'A'
                                                   && (mvt.mvt_effective_date == null ||
                                                           (mvt.mvt_effective_date.Value.Date <= datenow
                                                           && (mvt.mvt_expire_date == null ||
                                                               mvt.mvt_expire_date.Value.Date >= datenow))
                                                       )
                                               select new
                                               {
                                                   mrm.mrm_id,
                                                   mvt.mvt_id
                                               }).FirstOrDefault();
                            if (objwaitRoom != null)
                            {
                                mrmID = objwaitRoom.mrm_id;
                                mvtID = objwaitRoom.mvt_id;
                            }
                            else
                            {
                                IsCompleted = false;
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    var tehid = (from t1 in cdc.trn_eye_exam_hdrs where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                    if (tehid != null)
                    {
                        if (tehid.teh_eyedropper == true)
                        {
                            // กรณที่มีการหยอดตาแล้ว==> ถูกส่งไปห้องอื่น==> ให้ส่งไปห้องตาอีกรอบ
                            var chkEye_Package = (from t1 in cdc.trn_patient_plans
                                                  join t2 in cdc.mst_events on t1.mvt_id equals t2.mvt_id
                                                  where t1.tpr_id == tpr_id
                                                  && t2.mvt_code == "EM"
                                                  select t1).Count();

                            var countDoctorPass = (from t1 in cdc.trn_patient_queues
                                                   join t2 in cdc.mst_events on t1.mvt_id equals t2.mvt_id
                                                   where t1.tpr_id == tpr_id
                                                   && t2.mvt_code == "EM"
                                                   select t1).Count();

                            var countNursePass = (from t1 in cdc.trn_patient_queues
                                                  join t2 in cdc.mst_events on t1.mvt_id equals t2.mvt_id
                                                  where t1.tpr_id == tpr_id
                                                  && t2.mvt_code == "EN"
                                                  select t1).Count();
                            if (chkEye_Package == 0)
                            {
                                return false;
                            }
                            else
                            {
                                if (countNursePass > 0 && countDoctorPass == 0)
                                {
                                    DateTime datenow = Program.GetServerDateTime();
                                    var objwaitRoom = (from t1 in cdc.mst_room_events
                                                       from mvt in cdc.mst_events
                                                       from mrm in cdc.mst_room_hdrs
                                                       where t1.mrm_id == mrm.mrm_id
                                                       && t1.mvt_id == mvt.mvt_id
                                                       && mrm.mrm_code == "EM"
                                                       && mvt.mvt_code == "EM"
                                                       && mrm.mhs_id == Program.CurrentSite.mhs_id
                                                       && mrm.mrm_status == 'A'
                                                           && (mrm.mrm_effective_date == null ||
                                                                   (mrm.mrm_effective_date.Value.Date <= datenow
                                                                   && (mrm.mrm_expire_date == null ||
                                                                       mrm.mrm_expire_date.Value.Date >= datenow))
                                                               )
                                                               && mvt.mvt_status == 'A'
                                                           && (mvt.mvt_effective_date == null ||
                                                                   (mvt.mvt_effective_date.Value.Date <= datenow
                                                                   && (mvt.mvt_expire_date == null ||
                                                                       mvt.mvt_expire_date.Value.Date >= datenow))
                                                               )
                                                       select new
                                                       {
                                                           mrm.mrm_id,
                                                           mvt.mvt_id
                                                       }).FirstOrDefault();
                                    if (objwaitRoom != null)
                                    {
                                        mrmID = objwaitRoom.mrm_id;
                                        mvtID = objwaitRoom.mvt_id;
                                    }
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static int Getmvtid { get; set; }
        public static int Getmrmid { get; set; }
        public static int Gettprid { get; set; }
        public static bool GetIsHaveNextRoom { get; set; }
        public static string GetStatusPending { get; set; }

        public static string GetStrSaveAndSend()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                string siteName = "";
                string mzmName = "";
                string mvtName = "";
                var objqueue = (from t1 in dbc.trn_patient_queues
                                where t1.tpr_id == Gettprid
                                    && t1.mrm_id == Getmrmid
                                    && t1.mvt_id == Getmvtid
                                select t1).FirstOrDefault();
                if (objqueue != null)
                {
                    siteName = objqueue.mst_room_hdr.mst_hpc_site.mhs_ename;
                    mzmName = objqueue.mst_room_hdr.mst_zone.mze_ename;
                }

                var objroomhdr = (from t1 in dbc.mst_events
                                  where t1.mvt_id == Getmvtid
                                  select t1).FirstOrDefault();
                if (objroomhdr != null)
                {
                    mvtName = objroomhdr.mvt_ename;
                }
                if (mvtName != "" && siteName != "")
                {

                    //"Save data completed. Send to {0} [ Site: {2} Zone: {1}]"
                    return string.Format(Program.MsgSend, mvtName, siteName, mzmName, objqueue.trn_patient_regi.tpr_queue_no);
                }
                else
                {
                    return "";
                }
            }
        }

        public static void SetLR_Obstaticresult(CheckupDataContext dbc, int tpr_id)
        {
            //Update LR สูติอย่างเดียวเนื่องจากไม่มีใน callDataTakeCare Event Code สูติ= PT
            var currentPT = (from t1 in dbc.mst_events
                             where t1.mvt_code == "PT"
                             select t1).FirstOrDefault();
            if (currentPT != null)
            {
                //Check Plan มี file patho
                var currentPlan = (from t1 in dbc.trn_patient_plans
                                   where t1.mvt_id == currentPT.mvt_id
                                   && t1.tpr_id == tpr_id
                                   select t1).ToList();
                if (currentPlan.Count() > 0)
                {
                    foreach (trn_patient_plan item in currentPlan)
                    {
                        if (item.tpl_patho != null || item.tpl_patho != "")
                        {
                            var pathLoadFileName = GetPathPatho(dbc, tpr_id, currentPT.mvt_id);//@"\\10.1.106.230\Results\BMC-LIVE\Lab\Results\201307\014385176_R354_1.doc";
                            if (System.IO.File.Exists(pathLoadFileName))
                            {
                                var objcurrentQueue = (from t1 in dbc.trn_patient_queues
                                                       where t1.mvt_id == currentPT.mvt_id
                                                         && t1.tpr_id == tpr_id
                                                       select t1).FirstOrDefault();
                                if (objcurrentQueue != null)
                                {
                                    objcurrentQueue.tps_status = "LR";
                                }
                                dbc.SubmitChanges();
                            }

                        }
                    }
                }
            }
        }

        public static string GetPathPatho(CheckupDataContext dbc, int tpr_id, int mvt_id)
        {
            //วิธีการหา Patho
            /*
             select mfd.mfd_text
             from mst_config_hdr mfh,
                    mst_config_dtl mfd
             where mfh.mfh_id = mfd.mfh_id
                    and mfh.mhs_id = 1
                    and mfh.mfh_code = 'PAT'
                    and mfh.mfh_status = 'A'
                    and convert(date,getdate(),103) between
                        convert(date,isnull(mfh.mfh_effective_date,getdate()),103)
                        and CONVERT(date,isnull(mfh.mfh_expire_date,getdate()),103)
                    and mfd.mfd_status = 'A'
                    and convert(date,getdate(),103) between
                        convert(date,isnull(mfd.mfd_effective_date,getdate()),103)
                        and CONVERT(date,isnull(mfd.mfd_expire_date,getdate()),103)
             */
            try
            {
                var datenow = Program.GetServerDateTime().Date;
                int mhs_id = 0;
                var objcurrentRegis = (from t1 in dbc.trn_patient_regis
                                       where
                                           t1.tpr_id == tpr_id
                                       select t1).FirstOrDefault();
                if (objcurrentRegis != null)
                {
                    mhs_id = objcurrentRegis.mhs_id;
                }
                var objpathodata = (from t1 in dbc.mst_config_dtls
                                    where t1.mst_config_hdr.mhs_id == mhs_id
                                    && t1.mst_config_hdr.mfh_code == "PAT"
                                    && (t1.mst_config_hdr.mfh_status == 'A'
                                       && (t1.mst_config_hdr.mfh_effective_date == null ||
                                               (t1.mst_config_hdr.mfh_effective_date.Value.Date <= datenow
                                               && (t1.mst_config_hdr.mfh_expire_date == null ||
                                                   t1.mst_config_hdr.mfh_expire_date.Value.Date >= datenow))
                                           )
                                        )
                                    && (t1.mfd_status == 'A'
                                       && (t1.mfd_effective_date == null ||
                                               (t1.mfd_effective_date.Value.Date <= datenow
                                               && (t1.mfd_expire_date == null ||
                                                   t1.mfd_expire_date.Value.Date >= datenow)
                                                )
                                           )
                                           )
                                    select t1).FirstOrDefault();
                if (objpathodata != null)
                {
                    var pathlink = objpathodata.mfd_text;//Path file

                    var objplan = (from t1 in objcurrentRegis.trn_patient_plans
                                   where t1.tpr_id == tpr_id
                                       && t1.mvt_id == mvt_id
                                   select t1).FirstOrDefault();
                    if (objplan != null)
                    {
                        var fileName = objplan.tpl_patho;//file Name
                        if (objplan.tpl_patho != null && objplan.tpl_patho != "")
                        {
                            var pathLoadFileName = pathlink + fileName;
                            if (System.IO.File.Exists(pathLoadFileName))
                            {
                                return pathLoadFileName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("CallQueue", "GetPathPatho", ex, false);
            }
            return "";

        }

        public static bool IsStatusED()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                if (Program.CurrentPatient_queue != null)
                {
                    trn_patient_queue objCurrentQueue = (from t1 in dbc.trn_patient_queues
                                                         where t1.tps_id == Program.CurrentPatient_queue.tps_id
                                                         select t1).FirstOrDefault();
                    if (objCurrentQueue != null)
                    {
                        if (objCurrentQueue.tps_status == "ED")
                        {
                            return true;
                            //Program.CurrentPatient_queue = null;
                        }
                    }
                }
            }
            return false;
        }

        public static void SetUpdateTextfile(string HN_no, int tpr_id)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                var objdata = (from t1 in dbc.trn_RefreshLabHistories where t1.HN_no == HN_no && t1.tpr_id == tpr_id select t1).FirstOrDefault();
                if (objdata != null)
                {
                    objdata.status = false;
                }
                else
                {
                    trn_RefreshLabHistory trnRLH = new trn_RefreshLabHistory();
                    trnRLH.tpr_id = tpr_id;
                    trnRLH.HN_no = HN_no;
                    trnRLH.status = false;
                    trnRLH.CreateDate = Program.GetServerDateTime();
                    dbc.trn_RefreshLabHistories.InsertOnSubmit(trnRLH);
                }
                dbc.SubmitChanges();
            }
        }

        //public static void UpdateStatusLR_Queue11(int mvt_id, int tpr_id)
        //{
        //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
        //    {
        //        var objcurrentQueue = (from t1 in dbc.trn_patient_queues
        //                               where t1.mvt_id == mvt_id
        //                                 && t1.tpr_id == tpr_id
        //                               select t1).FirstOrDefault();
        //        if (objcurrentQueue != null)
        //        {
        //            objcurrentQueue.tps_status = "LR";
        //        }
        //        dbc.SubmitChanges();
        //    }
        //}
        #region morn coding 19-06-2014

        #region function Send Auto
        public static StatusTransaction SendAutoOnStation(int tpr_id, int mrm_id, int tps_id)
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
                                process_type = "A",
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

                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    bool haveNextRoom = func.checkHaveNextRoom();
                    bool haveNextRoomOnSite = func.checkHaveNextRoomOnSite();
                    bool SendAuto = false;
                    if (!haveNextRoom)
                    {
                        SendAuto = true;
                    }
                    else
                    {
                        if (haveNextRoomOnSite)
                        {
                            SendAuto = true;
                        }
                    }

                    if (SendAuto)
                    {
                        List<int> list_mvt_id = new Class.FunctionDataCls().get_mvt_id(mrm_id);
                        StatusTransaction result = PSendAutoAllRoom(Class.SendType.Normal, mrm_id, list_mvt_id, tps_id);
                        if (result == StatusTransaction.SendCheckB)
                        {
                            if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                            {
                                string messege = "";
                                System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ กรุณาเลือก send manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                StatusTransaction sendManual = new Class.SendManaulCls().SendManualOnStation(list_mvt_id, ref messege);
                                if (result == StatusTransaction.True)
                                {
                                    //new ClsTCPClient().sendClearUnitDisplay();
                                    //Program.CurrentRegis = null;
                                    //Program.CurrentPatient_queue = null;
                                }
                                return sendManual;
                            }
                            else
                            {
                                string messege = "";
                                System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ ผู้รับบริการจะถูกส่งไป Check point B", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                StatusTransaction returnB = new Class.SendQueue().ReturnToCheckB(Class.SendType.Normal, tpr_id, mrm_id, tps_id, list_mvt_id, ref messege);
                                if (returnB == StatusTransaction.True)
                                {
                                    //new ClsTCPClient().sendClearUnitDisplay();
                                    //Program.CurrentRegis = null;
                                    //Program.CurrentPatient_queue = null;
                                    //new Class.FunctionDataCls().SendToCheckPointB(tpr_id, tps_id);
                                }
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
                                    int index = 1;
                                    int? log_tps_id = logCls.get_tps_id(tpr_id);
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
                    else
                    {
                        if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                        {
                            string messege = "";

                            System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ กรุณาเลือก send manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            StatusTransaction sendManual = new Class.SendManaulCls().SendManualOnStation(ref messege);
                            if (sendManual == StatusTransaction.True)
                            {
                                //new ClsTCPClient().sendClearUnitDisplay();
                                //Program.CurrentRegis = null;
                                //Program.CurrentPatient_queue = null;
                            }
                            return sendManual;
                        }
                        else
                        {
                            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                            mst_hpc_site mhs = mst.GetMstHpcSite(Program.CurrentSite.mhs_id);
                            MessageBox.Show("ไม่สามารถส่งคนไข้ไปยัง Site " + mhs.mhs_ename + " ได้ กรุณาเปลี่ยน Site", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return StatusTransaction.False;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("CallQueue", "SendAutoOnStation", ex, false);
                    return StatusTransaction.Error;
                }
            }
        }
        public static StatusTransaction SendAutoOnStation()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int mrm_id = Program.CurrentRoom.mrm_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;
            return SendAutoOnStation(tpr_id, mrm_id, tps_id);
        }

        public static StatusTransaction SendAutoOnStation(int tpr_id, int mrm_id, List<int> list_mvt_id, int tps_id)
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
                                process_type = "A",
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

                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    if (func.checkHaveNextRoomOnSite())
                    {
                        StatusTransaction result = PSendAutoAllRoom(Class.SendType.Normal, mrm_id, list_mvt_id, tps_id);

                        if (result == StatusTransaction.SendCheckB)
                        {
                            if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                            {
                                string messege = "";

                                System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ กรุณาเลือก send manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                StatusTransaction sendManual = new Class.SendManaulCls().SendManualOnStation(list_mvt_id, ref messege);
                                if (sendManual == StatusTransaction.True)
                                {
                                    //new ClsTCPClient().sendClearUnitDisplay();
                                    //Program.CurrentRegis = null;
                                    //Program.CurrentPatient_queue = null;
                                }
                                return sendManual;
                            }
                            else
                            {
                                string messege = "";
                                System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ ผู้รับบริการจะถูกส่งไป Check point B", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                StatusTransaction returnB = new Class.SendQueue().ReturnToCheckB(Class.SendType.Normal, tpr_id, mrm_id, tps_id, list_mvt_id, ref messege);
                                if (returnB == StatusTransaction.True)
                                {
                                    //new ClsTCPClient().sendClearUnitDisplay();
                                    //Program.CurrentRegis = null;
                                    //Program.CurrentPatient_queue = null;
                                }
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
                                    int index = 1;
                                    int? log_tps_id = logCls.get_tps_id(tpr_id);
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
                    else
                    {
                        if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                        {
                            string messege = "";

                            System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ กรุณาเลือก send manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            StatusTransaction sendManual = new Class.SendManaulCls().SendManualOnStation(list_mvt_id, ref messege);
                            if (sendManual == StatusTransaction.True)
                            {
                                //new ClsTCPClient().sendClearUnitDisplay();
                                //Program.CurrentRegis = null;
                                //Program.CurrentPatient_queue = null;
                            }
                            else if (sendManual == StatusTransaction.False)
                            {

                            }
                            return sendManual;
                        }
                        else
                        {
                            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                            mst_hpc_site mhs = mst.GetMstHpcSite(Program.CurrentSite.mhs_id);
                            MessageBox.Show("ไม่สามารถส่งคนไข้ไปยัง Site " + mhs.mhs_ename + " ได้ กรุณาเปลี่ยน Site", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return StatusTransaction.False;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("CallQueue", "SendAutoOnStation", ex, false);
                    return StatusTransaction.Error;
                }
            }
        }
        public static StatusTransaction SendAutoOnStation(List<int> list_mvt_id)
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int mrm_id = Program.CurrentRoom.mrm_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;
            return SendAutoOnStation(tpr_id, mrm_id, list_mvt_id, tps_id);
        }

        public static StatusTransaction SendAutoOnPendingCheckB(int tpr_id, int mrm_id, int tps_id)
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
                                process_type = "A",
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

                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    bool haveNextRoom = func.checkHaveNextRoom();
                    bool haveNextRoomOnSite = func.checkHaveNextRoomOnSite();
                    bool SendAuto = false;
                    if (!haveNextRoom)
                    {
                        SendAuto = true;
                    }
                    else
                    {
                        if (haveNextRoomOnSite)
                        {
                            SendAuto = true;
                        }
                    }

                    if (SendAuto)
                    {
                        List<int> list_mvt_id = new Class.FunctionDataCls().get_mvt_id(mrm_id);
                        StatusTransaction result = PSendAutoAllRoom(Class.SendType.Normal, mrm_id, list_mvt_id, tps_id);
                        if (result == StatusTransaction.SendCheckB)
                        {
                            if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                            {
                                string messege = "";
                                System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ กรุณาเลือก send manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                                StatusTransaction sendManual = new Class.SendManaulCls().SendManualOnStation(list_mvt_id, ref messege);
                                if (result == StatusTransaction.True)
                                {
                                    //new ClsTCPClient().sendClearUnitDisplay();
                                    //Program.CurrentRegis = null;
                                    //Program.CurrentPatient_queue = null;
                                }
                                else
                                {
                                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                                    new Class.TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                                }
                                return sendManual;
                            }
                            else
                            {
                                string messege = "";
                                System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ ผู้รับบริการจะถูกส่งไป Check point B", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                StatusTransaction returnB = new Class.SendQueue().ReturnToCheckB(Class.SendType.Normal, tpr_id, mrm_id, tps_id, list_mvt_id, ref messege);
                                if (returnB == StatusTransaction.True)
                                {
                                    //new ClsTCPClient().sendClearUnitDisplay();
                                    //Program.CurrentRegis = null;
                                    //Program.CurrentPatient_queue = null;
                                    //new Class.FunctionDataCls().SendToCheckPointB(tpr_id, tps_id);
                                }
                                else if (returnB == StatusTransaction.Error)
                                {
                                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                                    new Class.TransactionQueueCls().endQueue(ref tpr, tps_id);
                                    cdc.SubmitChanges();
                                }
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
                                    int index = 1;
                                    int? log_tps_id = logCls.get_tps_id(tpr_id);
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
                    else
                    {
                        if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                        {
                            string messege = "";

                            System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ กรุณาเลือก send manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                            StatusTransaction sendManual = new Class.SendManaulCls().SendManualOnStationPendingCheckB(ref messege);
                            if (sendManual == StatusTransaction.True)
                            {
                                //new ClsTCPClient().sendClearUnitDisplay();
                                //Program.CurrentRegis = null;
                                //Program.CurrentPatient_queue = null;
                            }
                            else
                            {
                                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                                new Class.TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                            }
                            return sendManual;
                        }
                        else
                        {
                            trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            new Class.TransactionQueueCls().endQueue(ref tpr, tps_id);
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
                                cdc.SubmitChanges();
                            }
                            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                            mst_hpc_site mhs = mst.GetMstHpcSite(Program.CurrentSite.mhs_id);
                            MessageBox.Show("ไม่สามารถส่งคนไข้ไปยัง Site " + mhs.mhs_ename + " ได้ กรุณาเปลี่ยน Site", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return StatusTransaction.False;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("CallQueue", "SendAutoOnStation", ex, false);
                    return StatusTransaction.Error;
                }
            }
        }
        public static StatusTransaction SendAutoOnPendingCheckB()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int mrm_id = Program.CurrentRoom.mrm_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;
            return SendAutoOnPendingCheckB(tpr_id, mrm_id, tps_id);
        }
        #endregion

        #endregion

        #region ยกเลิกการใช้งาน
        public static StatusTransaction P_CallHold()
        {//
            Program.RefreshWaiting = false;
            //if (CallQueue.IsStatusED()) { return; }
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    try
                    {
                        dbc.Connection.Open();
                        DbTransaction trans = dbc.Connection.BeginTransaction();
                        dbc.Transaction = trans;

                        DateTime dateNow = Program.GetServerDateTime();
                        int tpr_id = Program.CurrentRegis.tpr_id;
                        int mrm_id = Program.CurrentRoom.mrm_id;
                        double limittime = Program.GetLimitTime("HD");
                        int mvt_id = Convert1.ToInt32(Program.CurrentPatient_queue.mvt_id);
                        trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
                                                  where t1.tpr_id == tpr_id
                                                  && t1.mrm_id == mrm_id
                                                  && t1.mvt_id == mvt_id
                                                  select t1).FirstOrDefault();
                        if (objQ != null)
                        {
                            objQ.tps_status = "NS";
                            objQ.tps_ns_status = "QL";
                            objQ.tps_call_status = "HD";
                            objQ.tps_bm_seq = null;
                            objQ.tps_hold_by = Program.CurrentUser.mut_username;
                            objQ.tps_hold_date = dateNow.AddMinutes(limittime);
                            objQ.tps_start_date = null;
                            objQ.tps_update_date = dateNow;
                            objQ.tps_update_by = Program.CurrentUser.mut_username;
                            dbc.SubmitChanges();
                            dbc.Transaction.Commit();
                            //AlertOutDepartment.StopTime();
                        }
                    }
                    catch (Exception ex)
                    {
                        dbc.Transaction.Rollback();
                        Program.MessageError("CallQueue", "P_CallHold", ex, false);
                        return StatusTransaction.Error;
                    }
                    finally
                    {
                        dbc.Connection.Close();
                    }
                }
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("CallQueue", "P_CallHold", ex, false);
                return StatusTransaction.Error;
            }
            finally
            {
                Program.RefreshWaiting = true;
            }
        }
        #endregion
    }
}
