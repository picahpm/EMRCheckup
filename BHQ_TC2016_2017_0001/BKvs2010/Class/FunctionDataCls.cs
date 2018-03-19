using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
//using System.Transactions;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace BKvs2010.Class
{
    class FunctionDataCls
    {
        public bool checkPassedCheckPointB(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                //int mrm_id = mst.getMstRoomHdr("CB", tpr.mhs_id, tpr.tpr_site_use).mrm_id;
                int mvt_id = cdc.mst_events.Where(x => x.mvt_code == "CB").Select(x => x.mvt_id).FirstOrDefault();
                List<trn_patient_queue> tps = tpr.trn_patient_queues
                                                 .Where(x => x.tpr_id == tpr_id
                                                          && x.mvt_id == mvt_id).ToList();
                if (tps.Count() > 0) return true;
            }
            return false;
        }
        public bool checkEventDoctorResult(int mvt_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_event m_event = cdc.mst_events.Where(x => x.mvt_id == mvt_id).FirstOrDefault();
                    if (m_event == null)
                    {
                        return false;
                    }
                    else
                    {
                        if (m_event.mvt_code != "DC")
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
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "checkEventDoctorResult", ex, false);
                return false;
            }
        }

        public bool checkDoctorRequest(int tpr_id, int mhs_id, bool v_check)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (tpr.tpr_req_doctor != 'Y')
                    {
                        // Check เงื่อนไขชาวต่างชาติ เวลาเข้าพบแพทย์
                        int countmdc = (from t in cdc.trn_patient_cats
                                        where t.tpr_id == tpr_id
                                        select t.mdc_id).Count();
                        int DocCat = (from t in cdc.trn_patient_cats
                                      join s in cdc.mst_doc_categories on t.mdc_id equals s.mdc_id
                                      where t.tpr_id == tpr_id
                                      && s.mdc_code == "MD014"
                                      select s.mdc_id).Distinct().Count();

                        if (countmdc == 0)
                        {
                            /*int WaitPatientRoom = (from t1 in cdc.vw_call_doctors
                                                   join t2 in cdc.mst_user_types on t1.mut_username equals t2.mut_username
                                                   join t3 in cdc.mst_user_cats on t2.mut_id equals t3.mut_id
                                                   join t4 in cdc.trn_patient_cats on t3.mdc_id equals t4.mdc_id
                                                   where t4.tpr_id == tpr_id
                                                   && t1.waiting_patient == "N"
                                                   select t1).Count();*/
                            if (v_check == true)
                            {
                                int WaitPatientRoom = (from t1 in cdc.vw_call_doctors
                                                       where t1.waiting_patient == "N"
                                                       select t1).Count();

                                if (WaitPatientRoom > 0)
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
                                return true;
                            }
                        }
                        else
                        {
                            var mhs_code = (from t in cdc.mst_hpc_sites where t.mhs_id == mhs_id select t.mhs_code).FirstOrDefault();

                            if (DocCat == 1 && mhs_code == "01CHK")
                            {
                                int Doc_login = (from t1 in cdc.mst_room_hdrs
                                                 join t2 in cdc.mst_room_dtls on t1.mrm_id equals t2.mrm_id
                                                 join t3 in cdc.log_user_logins on t2.mrd_id equals t3.mrd_id
                                                 join t4 in cdc.mst_user_cats on t3.mut_id equals t4.mut_id
                                                 join t5 in cdc.mst_doc_categories on t4.mdc_id equals t5.mdc_id
                                                 where t1.mrm_code == "DC"
                                                 && t1.mhs_id == mhs_id
                                                 && t5.mdc_code == "MD014"
                                                 && t3.lug_end_date == null
                                                 && t3.lug_start_date.Value.Date == Program.GetServerDateTime().Date
                                                 select t1).Count();
                                if (Doc_login > 0)
                                {
                                    if (v_check == true)
                                    {
                                        int WaitPatientRoom = (from t1 in cdc.vw_call_doctors
                                                               join t2 in cdc.mst_user_types on t1.mut_username equals t2.mut_username
                                                               join t3 in cdc.mst_user_cats on t2.mut_id equals t3.mut_id
                                                               join t4 in cdc.trn_patient_cats on t3.mdc_id equals t4.mdc_id
                                                               where t4.tpr_id == tpr_id
                                                               && t1.waiting_patient == "N"
                                                               select t1).Count();
                                        if (WaitPatientRoom > 0)
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
                                        return true;
                                    }
                                    //return true;
                                }
                            }
                            // Check เงื่อนไข ของ Doc Category เข้าส่งเข้าห้องแพทย์ ในกรณีที่มีแพทย์ที่เข้าเงื่อนไขของผู้รับบริการอยู่ในห้อง
                            else
                            {
                                int DocInsure = (from t1 in cdc.mst_room_hdrs
                                                 join t2 in cdc.mst_room_dtls on t1.mrm_id equals t2.mrm_id
                                                 join t3 in cdc.log_user_logins on t2.mrd_id equals t3.mrd_id
                                                 join t4 in cdc.mst_user_cats on t3.mut_id equals t4.mut_id
                                                 join t5 in cdc.mst_doc_categories on t4.mdc_id equals t5.mdc_id
                                                 join t6 in cdc.trn_patient_cats on t5.mdc_id equals t6.mdc_id
                                                 where t1.mrm_code == "DC"
                                                 && t1.mhs_id == mhs_id
                                                 && t6.tpr_id == tpr_id
                                                     //&& t5.mdc_code != "MD014"
                                                 && t3.lug_end_date == null
                                                 && t3.lug_start_date.Value.Date == Program.GetServerDateTime().Date
                                                 select t1).Count();
                                if (DocInsure > 0)
                                {
                                    if (v_check == true)
                                    {
                                        int WaitPatientRoom = (from t1 in cdc.vw_call_doctors
                                                               join t2 in cdc.mst_user_types on t1.mut_username equals t2.mut_username
                                                               join t3 in cdc.mst_user_cats on t2.mut_id equals t3.mut_id
                                                               join t4 in cdc.trn_patient_cats on t3.mdc_id equals t4.mdc_id
                                                               where t4.tpr_id == tpr_id
                                                               && t1.waiting_patient == "N"
                                                               select t1).Count();
                                        if (WaitPatientRoom > 0)
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
                                        return true;
                                    }
                                    //return true;
                                }
                            }
                            /*int DocInsure = (from t1 in cdc.mst_room_hdrs
                                             join t2 in cdc.mst_room_dtls on t1.mrm_id equals t2.mrm_id
                                             join t3 in cdc.log_user_logins on t2.mrd_id equals t3.mrd_id
                                             join t4 in cdc.mst_user_cats on t3.mut_id equals t4.mut_id
                                             join t5 in cdc.mst_doc_categories on t4.mdc_id equals t5.mdc_id
                                             join t6 in cdc.trn_patient_cats on t5.mdc_id equals t6.mdc_id
                                             where t1.mrm_code == "DC"
                                             && t1.mhs_id == mhs_id
                                             && t6.tpr_id == tpr_id
                                             && t5.mdc_code != "MD014"
                                             && t3.lug_end_date == null
                                             && t3.lug_start_date.Value.Date == Program.GetServerDateTime().Date
                                             select t1).Count();
                            if (DocInsure > 0)
                            {
                                return true;
                            }*/
                        }
                    }
                    else
                    {
                        if ((!string.IsNullOrEmpty(tpr.tpr_req_doc_code)) || (tpr.tpr_req_doc_gender != null && tpr.tpr_req_doc_gender != ' '))
                        {
                            string mrm_code = "DC";
                            List<mst_user_type> mut = getUserLogin(mhs_id, mrm_code);
                            if (mut != null)
                            {
                                if (!string.IsNullOrEmpty(tpr.tpr_req_doc_code))
                                {
                                    List<string> username = mut.Select(x => x.mut_username).ToList();
                                    if (username.Contains(tpr.tpr_req_doc_code))
                                    {
                                        if (v_check == true)
                                        {
                                            int WaitPatientRoom = (from t1 in cdc.vw_call_doctors
                                                                   where t1.waiting_patient == "N"
                                                                   && t1.mut_username == tpr.tpr_req_doc_code
                                                                   select t1).Count();

                                            if (WaitPatientRoom > 0)
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
                                            return true;
                                        }
                                        //return true;
                                    }
                                }
                                else if (tpr.tpr_req_doc_gender != null && tpr.tpr_req_doc_gender != ' ')
                                {
                                    List<char?> gender = mut.Select(x => x.mut_gender).ToList();
                                    if (gender.Contains(tpr.tpr_req_doc_gender))
                                    {
                                        if (v_check == true)
                                        {
                                            int WaitPatientRoom = (from t1 in cdc.vw_call_doctors
                                                                   join t2 in cdc.mst_user_types on t1.mut_username equals t2.mut_username
                                                                   where t1.waiting_patient == "N"
                                                                   && t2.mut_gender == tpr.tpr_req_doc_gender
                                                                   select t1).Count();

                                            if (WaitPatientRoom > 0)
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
                                            return true;
                                        }

                                        //return true;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public List<log_user_login> getLogUserLogin(int mhs_id, string mrm_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    mst_hpc_site mhs = mst.GetMstHpcSite(mhs_id);
                    if (mhs != null)
                    {
                        string mhs_code = mhs.mhs_code;
                        List<mst_room_dtl> mrd = mst.GetListRoomDtl(mrm_code, mhs_code);
                        if (mrd != null)
                        {
                            List<int?> list_mrd_id = mrd.Select(x => (int?)x.mrd_id).ToList();
                            List<log_user_login> lug = cdc.log_user_logins.Where(x => x.lug_end_date == null
                                                                                   && x.mhs_id == mhs_id
                                                                                   && list_mrd_id.Contains(x.mrd_id)).ToList();
                            return lug;
                        }
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public List<mst_user_type> getUserLogin(int mhs_id, string mrm_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<log_user_login> lug = getLogUserLogin(mhs_id, mrm_code);
                    if (lug != null)
                    {
                        List<int> list_mut_id = lug.Select(y => y.mut_id).ToList();
                        List<mst_user_type> mut = cdc.mst_user_types.Where(x => list_mut_id.Contains(x.mut_id)).ToList();
                        return mut;
                    }
                }
            }
            catch
            {

            }
            return null;
        }

        public PopupUltrasoundLower.ResultPopupUltrasoundLower popupUltrasoundLower()
        {
            try
            {
                if (Program.CurrentRegis != null)
                {
                    int tpr_id = Program.CurrentRegis.tpr_id;
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        DateTime dateNow = Program.GetServerDateTime();
                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        bool checkDatePopup = tpr.tpr_call_lower_date == null
                                              ? false
                                              : (tpr.tpr_call_lower_date.Value.Date == dateNow.Date && tpr.tpr_call_lower_date <= dateNow);
                        if (tpr != null && tpr.tpr_miss_lower == true && checkDatePopup)
                        {
                            PopupUltrasoundLower popup = new PopupUltrasoundLower();
                            return popup.questionUltrasoundLower;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "popupUltrasoundLower()", ex, false);
            }
            return PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater;
            ////End Ultrasound Lower [Added.Akkaradech on 2014-01-27]
        }

        public trn_patient_queue getNewQueue(int mrm_id, int mvt_id)
        {
            DateTime dateNow = Program.GetServerDateTime();
            return new trn_patient_queue
            {
                mrd_id = null,
                mrm_id = mrm_id,
                mvt_id = mvt_id,
                tps_bm_seq = null,
                //tps_call_by = "",
                //tps_call_date = DateTime.Now,
                //tps_call_status = "",
                //tps_cancel_by = "",
                //tps_cancel_date = DateTime.Now,
                //tps_cancel_other = "",
                //tps_cancel_remark = "",
                //tps_end_date = DateTime.Now,
                //tps_hold_by = "",
                //tps_hold_date = DateTime.Now,
                //tps_lab_date = DateTime.Now,
                //tps_start_date = DateTime.Now,
                tps_status = "NS",
                tps_ns_status = "QL",
                tps_send_by = Program.CurrentUser.mut_username,
                tps_create_by = Program.CurrentUser.mut_username,
                tps_create_date = dateNow,
                tps_update_by = Program.CurrentUser.mut_username,
                tps_update_date = dateNow
            };
        }
        public void insertNextQueue(ref trn_patient_regi tpr, int mrm_id, int mvt_id)
        {
            tpr.trn_patient_queues.Add(getNewQueue(mrm_id, mvt_id));
            //Program.CurrentRegis = null;
            //Program.CurrentPatient_queue = null;
        }
        public void insertNextQueue(ref trn_patient_regi tpr, string mrm_code, string mvt_code)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            int next_mrm_id = mst.GetMstRoomHdr(mrm_code, tpr.mhs_id, tpr.tpr_site_use).mrm_id;
            int next_mvt_id = mst.GetMstEvent(mvt_code).mvt_id;

            insertNextQueue(ref tpr, next_mrm_id, next_mvt_id);
        }

        public void deleteCurrentQueue(ref InhCheckupDataContext cdc, ref trn_patient_regi tpr, int tps_id)
        {
            trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
            cdc.trn_patient_queues.DeleteOnSubmit(tps);
        }

        public void completeCurrentQueue(ref trn_patient_regi tpr, List<int> currentMvt, int tps_id)
        {
            trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
            tps.tps_status = "ED";
            tps.tps_ns_status = null;
            List<trn_patient_plan> tpp = tpr.trn_patient_plans.Where(x => currentMvt.Contains(x.mvt_id)).ToList();
            tpp.ForEach(x => x.tpl_status = 'P');
        }

        public bool sendQueue(List<int> currentMvt, string next_mrm_code, string next_mvt_code)
        {
            try
            {
                if (Program.CurrentRegis != null && Program.CurrentPatient_queue != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        int tpr_id = Program.CurrentRegis.tpr_id;
                        int tps_id = Program.CurrentPatient_queue.tps_id;
                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        completeCurrentQueue(ref tpr, currentMvt, tps_id);
                        insertNextQueue(ref tpr, next_mrm_code, next_mvt_code);
                        cdc.SubmitChanges();
                        return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public enum sendQueueStatus
        {
            sendSuccess,
            notSend,
            error
        }
        public sendQueueStatus sendQueueUltrasoundLower(PopupUltrasoundLower.ResultPopupUltrasoundLower result, List<int> currentMvt)
        {
            if (result == PopupUltrasoundLower.ResultPopupUltrasoundLower.BeforeStation)
            {
                try
                {
                    InhCheckupDataContext cdc = new InhCheckupDataContext();
                    int tpr_id = Program.CurrentRegis.tpr_id;
                    int tps_id = Program.CurrentPatient_queue.tps_id;
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    //tpr.tpr_miss_lower = false;
                    //tpr.tpr_miss_lower_date = null;
                    deleteCurrentQueue(ref cdc, ref tpr, tps_id);
                    insertNextQueue(ref tpr, "US", "UL");
                    try
                    {
                        cdc.SubmitChanges();
                    }
                    catch (System.Data.Linq.ChangeConflictException)
                    {
                        foreach (System.Data.Linq.ObjectChangeConflict occ in cdc.ChangeConflicts)
                        {
                            cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                        }
                    }
                    return sendQueueStatus.sendSuccess;
                }
                catch (Exception ex)
                {
                    Program.MessageError("Class.FunctionDataCls", "sendQueueUltrasoundLower()", ex, false);
                    return sendQueueStatus.error;
                }
            }
            else if (result == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
            {
                if (Program.CurrentRegis != null && Program.CurrentPatient_queue != null)
                {
                    try
                    {
                        if (sendQueue(currentMvt, "US", "UL"))
                        {
                            return sendQueueStatus.sendSuccess;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("Class.FunctionDataCls", "sendQueueUltrasoundLower()", ex, false);
                        return sendQueueStatus.error;
                    }
                }
            }
            return sendQueueStatus.notSend;
        }

        public bool checkEyeDropper(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                if (tpr != null)
                {
                    if (tpr.trn_eye_exam_hdrs != null)
                    {
                        if (tpr.trn_eye_exam_hdrs.Count > 0)
                        {
                            return tpr.trn_eye_exam_hdrs.FirstOrDefault().teh_eyedropper == null ? false : (bool)tpr.trn_eye_exam_hdrs.FirstOrDefault().teh_eyedropper;
                        }
                    }
                }
            }
            return false;
        }

        public bool checkHaveNextRoomOnSite()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int currentSite_id = Program.CurrentSite.mhs_id;
            return checkHaveNextRoomOnSite(tpr_id, currentSite_id);
        }
        public bool checkHaveNextRoomOnSite(int tpr_id, int currentSite_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    mst_hpc_site mhs = mst.GetMstHpcSite(currentSite_id);
                    if (mhs != null)
                    {
                        if (mhs.mhs_code == "01HPC2")
                        {
                            List<vw_patient_room> patientRoom = cdc.vw_patient_rooms
                                                                   .Where(x => x.tpr_id == tpr_id &&
                                                                               x.site_rm == currentSite_id).ToList();
                            if (patientRoom == null || patientRoom.Count == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return true;
        }

        public bool checkHaveNextRoom()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int currentSite_id = Program.CurrentSite.mhs_id;
            return checkHaveNextRoom(tpr_id, currentSite_id);
        }
        public bool checkHaveNextRoom(int tpr_id, int currentSite_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    mst_hpc_site mhs = mst.GetMstHpcSite(currentSite_id);
                    if (mhs != null)
                    {
                        if (mhs.mhs_code == "01HPC2")
                        {
                            List<vw_patient_room> patientRoom = cdc.vw_patient_rooms
                                                                   .Where(x => x.tpr_id == tpr_id &&
                                                                               x.mhs_id == currentSite_id).ToList();
                            if (patientRoom == null || patientRoom.Count == 0)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            return true;
        }

        public StatusTransaction checkUseDoctorQuota(int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_hpc_site mst_site = cdc.mst_hpc_sites.Where(x => x.mhs_id == mhs_id).FirstOrDefault();
                    if (mst_site.mhs_use_quota == true ? true : false)
                    {
                        return StatusTransaction.True;
                    }
                    else
                    {
                        return StatusTransaction.False;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "checkUseQuota", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction dispense_doctor_by_point(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (tpr != null)
                    {
                        StatusTransaction use_quota = checkUseDoctorQuota(tpr.mhs_id);
                        if (use_quota == StatusTransaction.True)
                        {
                            dispense_doctor_by_pointResult result = cdc.dispense_doctor_by_point(tpr_id, Program.CurrentUser.mut_username).FirstOrDefault();
                            if (result.Column1 == null)
                            {
                                MessageBox.Show("ไม่สามารถ Assign ให้แพทย์ได้ เพราะไม่ได้อยู่ภายใต้เงื่อนไขที่กำหนด", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (result.Column1 != "")
                            {
                                mst_user_type doctor = cdc.mst_user_types.Where(x => x.mut_username == result.Column1).FirstOrDefault();
                                if (doctor != null)
                                {
                                    int? room_id = cdc.log_user_logins.Where(x => x.lug_end_date == null && x.mut_id == doctor.mut_id).Select(x => x.mrd_id).FirstOrDefault();
                                    string room_name = cdc.mst_room_dtls.Where(x => x.mrd_id == room_id).Select(x => x.mrd_room_no).FirstOrDefault();
                                    room_name = string.IsNullOrEmpty(room_name) ? "แพทย์ยังไม่ Login" : room_name;
                                    //MessageBox.Show(tpr.tpr_queue_no + " Assign ให้แพทย์ " + doctor.mut_fullname + "[" + result.Column1 + "][" + room_name + "]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //MessageBox.Show(tpr.tpr_queue_no + " ได้เข้าพบแพทย์ " + doctor.mut_fullname + "[" + result.Column1 + "]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show("Queue No. : " + tpr.tpr_queue_no + Environment.NewLine + "Assign ให้แพทย์ : " + doctor.mut_fullname + Environment.NewLine + "Room No. : " + room_name, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            return StatusTransaction.True;
                        }
                        else
                        {
                            return StatusTransaction.False;
                        }
                    }
                    else
                    {
                        return StatusTransaction.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "dispense_doctor_by_point", ex, false);
                return StatusTransaction.False;
            }
        }
        public StatusTransaction dispense_doctor_by_waiting(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (tpr != null)
                    {
                        StatusTransaction use_quota = checkUseDoctorQuota(tpr.mhs_id);
                        if (use_quota == StatusTransaction.True)
                        {
                            dispense_doctor_by_waitingResult result = cdc.dispense_doctor_by_waiting(tpr_id, Program.CurrentUser.mut_username).FirstOrDefault();
                            if (result.tpr_pe_doc_code == null)
                            {
                                MessageBox.Show("ไม่สามารถ Assign ให้แพทย์ได้ เพราะไม่ได้อยู่ภายใต้เงื่อนไขที่กำหนด", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (result.tpr_pe_doc_code != "")
                            {
                                mst_user_type doctor = cdc.mst_user_types.Where(x => x.mut_username == result.tpr_pe_doc_code).FirstOrDefault();
                                if (doctor != null)
                                {
                                    int? room_id = cdc.log_user_logins.Where(x => x.lug_end_date == null && x.mut_id == doctor.mut_id).Select(x => x.mrd_id).FirstOrDefault();
                                    string room_name = cdc.mst_room_dtls.Where(x => x.mrd_id == room_id).Select(x => x.mrd_room_no).FirstOrDefault();
                                    room_name = string.IsNullOrEmpty(room_name) ? "แพทย์ยังไม่ Login" : room_name;
                                    //MessageBox.Show("Queue No. : " + tpr.tpr_queue_no + " Assign ให้แพทย์" + Environment.NewLine + doctor.mut_fullname + Environment.NewLine + "Room No. : " + room_name, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show("Queue No. : " + tpr.tpr_queue_no + Environment.NewLine + "Assign ให้แพทย์ : " + doctor.mut_fullname + Environment.NewLine + "Room No. : " + room_name, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            return StatusTransaction.True;
                        }
                        else
                        {
                            return StatusTransaction.False;
                        }
                    }
                    else
                    {
                        return StatusTransaction.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "dispense_doctor_by_waiting", ex, false);
                return StatusTransaction.False;
            }
        }

        public bool changeDoctor(int tpr_id, string mut_username, string log_doc_type)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    mst_user_type new_doctor = mst.GetUser(mut_username);

                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    mst_user_type old_doctor = mst.GetUser(tpr.tpr_pe_doc_code);

                    DateTime dateNow = Program.GetServerDateTime();
                    log_change_doctor log = new log_change_doctor();
                    log.log_tpr_id = tpr_id;
                    log.log_time = dateNow;
                    log.log_username = Program.CurrentUser.mut_username;
                    log.log_doc_code_old = tpr.tpr_pe_doc_code;
                    log.log_doc_code_new = mut_username;
                    log.log_doc_type = log_doc_type;
                    cdc.log_change_doctors.InsertOnSubmit(log);

                    tpr.tpr_pe_doc = 'Y';
                    tpr.tpr_pe_doc_code = new_doctor.mut_username;
                    tpr.tpr_pe_doc_name = new_doctor.mut_fullname;
                    cdc.SubmitChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool cancelPEDoctor(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    DateTime dateNow = Program.GetServerDateTime();
                    log_change_doctor log = new log_change_doctor();
                    log.log_tpr_id = tpr_id;
                    log.log_time = dateNow;
                    log.log_username = Program.CurrentUser.mut_username;
                    log.log_doc_code_old = tpr.tpr_pe_doc_code;
                    log.log_doc_code_new = null;
                    cdc.log_change_doctors.InsertOnSubmit(log);

                    tpr.tpr_pe_doc = 'N';
                    tpr.tpr_pe_doc_code = null;
                    tpr.tpr_pe_doc_name = null;
                    cdc.SubmitChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetStrSaveAndSend(int tpr_id, string mrm_code, string mvt_code)
        {
            try
            {
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    int mrm_id = mst.GetMstRoomHdr(mrm_code, tpr.mhs_id, tpr.tpr_site_use).mrm_id; ;
                    int mvt_id = mst.GetMstEvent(mvt_code).mvt_id;
                    return GetStrSaveAndSend(tpr_id, mrm_id, mvt_id);
                }
            }
            catch
            {

            }
            return string.Empty;
        }
        public string GetStrSaveAndSend(int tpr_id, int mrm_id, int mvt_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string siteName = "";
                    string mzmName = "";
                    string mvtName = "";
                    var objqueue = cdc.trn_patient_queues
                                      .Where(x => x.tpr_id == tpr_id
                                               && x.mrm_id == mrm_id
                                               && x.mvt_id == mvt_id)
                                      .OrderByDescending(x => x.tps_id)
                                      .FirstOrDefault();
                    if (objqueue != null)
                    {
                        siteName = objqueue.mst_room_hdr.mst_hpc_site.mhs_ename;
                        mzmName = objqueue.mst_room_hdr.mst_zone.mze_ename;
                    }

                    var objroomhdr = cdc.mst_events
                                        .Where(x => x.mvt_id == mvt_id)
                                        .FirstOrDefault();
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
                        return "mvtName(" + mvt_id.ToString() + ") == null and siteName(" + mrm_id.ToString() + ") == null";
                    }
                }
            }
            catch
            {

            }
            return "catch mvtName(" + mvt_id.ToString() + "), siteName(" + mrm_id.ToString() + ")";
        }

        public string getStringGotoNextRoom(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                trn_patient_queue tps = tpr.trn_patient_queues.OrderByDescending(x => x.tps_create_date).FirstOrDefault();
                bool chkCheckPointCSite2 = cdc.mst_events.Where(y => y.mvt_id == tps.mvt_id).Select(y => y.mvt_code).FirstOrDefault() == "CC";

                if (Program.CurrentSite.mhs_extra_pe_type == true && tpr.tpr_pe_site2 == 'N' && chkCheckPointCSite2)
                {
                    //return tpr.tpr_queue_no + " Send Book Complete.";
                    return tpr.tpr_queue_no + " Send to Check Point C.";
                }
                else
                {
                    var result = tpr.trn_patient_queues.Where(x => x.tpr_id == tpr_id &&
                                                                   (cdc.mst_events.Where(y => y.mvt_id == x.mvt_id).Select(y => y.mvt_code).FirstOrDefault() == "CC")
                                                                   ? x.tps_status == "WK"
                                                                   : ((x.tps_status == "NS" &&
                                                                       x.tps_ns_status == "QL") ||
                                                                       (x.tps_status == "NS" &&
                                                                        x.tps_ns_status == "WP")))
                                    .OrderByDescending(x => x.tps_id).FirstOrDefault();

                    if (result != null)
                    {
                        if (result.mrm_id != null && result.mvt_id != null)
                        {
                            return GetStrSaveAndSend(tpr_id, (int)result.mrm_id, (int)result.mvt_id);
                        }
                    }
                }
                return string.Empty;
            }
        }

        public bool checkPassDoctor(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (tpr != null)
                    {
                        trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mst_room_hdr.mrm_code == "DC").FirstOrDefault();
                        if (tps != null)
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        public StatusTransaction checkRemainEvent(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    cdc.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                    List<int> mvt_active = cdc.mst_room_events.Where(x => x.mst_room_hdr.mrm_status == 'A').Select(x => x.mvt_id).ToList();
                    int count = cdc.trn_patient_plans.Where(x => x.tpr_id == tpr_id && x.tpl_status == 'N' && mvt_active.Contains(x.mvt_id)).Count();
                    return count > 0 ? StatusTransaction.True : StatusTransaction.False;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsFunctionData", "checkRemainEvent()", ex, false);
                return StatusTransaction.Error;
            }
        }

        public List<int> get_mvt_id(int mrm_id)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_id);
            List<int> list_mvt_id = new List<int>();
            if (mrm.mrm_code == "EM")
            {
                if (Program.CurrentRoom.mrd_type == 'D')
                {
                    list_mvt_id.Add(mst.GetMstEvent("EM").mvt_id);
                }
                else if (Program.CurrentRoom.mrd_type == 'N')
                {
                    list_mvt_id.Add(mst.GetMstEvent("EN").mvt_id);
                }
            }
            else if (mrm.mrm_code == "TE")
            {
                if (Program.CurrentRoom.mrd_type == 'T')
                {
                    list_mvt_id.Add(mst.GetMstEvent("TX").mvt_id);
                }
                else if (Program.CurrentRoom.mrd_type == 'D')
                {
                    list_mvt_id.Add(mst.GetMstEvent("TE").mvt_id);
                }
            }
            else
            {
                list_mvt_id = mst.GetMstRoomEventByMrm(mrm_id).Select(x => x.mvt_id).ToList();
            }
            return list_mvt_id;
        }

        public StatusTransaction pendingPE(int tpr_id)
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

                        if (tpr != null)
                        {
                            tpr.tpr_pending = true;
                            tpr.tpr_pending_no_station = true;
                            cdc.SubmitChanges();
                            cdc.Transaction.Commit();
                            return StatusTransaction.True;
                        }
                        else
                        {
                            return StatusTransaction.Error;
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("Class.FunctionDataCls", "pendingPE", ex, false);
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
                Program.MessageError("Class.FunctionDataCls", "pendingPE", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction repackagePending(int tpr_id, int mrm_id)
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

                        List<mst_room_event> objmvt = cdc.mst_room_events.Where(x => x.mrm_id == mrm_id).ToList();
                        List<int?> mvt_id = objmvt.Select(x => (int?)x.mvt_id).ToList();
                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        List<trn_patient_plan> tpl = tpr.trn_patient_plans.Where(x => mvt_id.Contains(x.mvt_id)).ToList();
                        foreach (trn_patient_plan pp in tpl)
                        {
                            if (pp.tpl_status == 'C')
                            {
                                List<trn_patient_pending> tpp = tpr.trn_patient_pendings.Where(x => x.mrm_id == mrm_id).ToList();
                                tpp.ForEach(x => x.tpp_status = 'C');
                            }
                            pp.tpl_status = 'N';
                            List<trn_patient_queue> tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_id && mvt_id.Contains(x.mvt_id)).ToList();
                            cdc.trn_patient_queues.DeleteAllOnSubmit(tps);
                        }
                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("Class.FunctionDataCls", "repackagePending", ex, false);
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
                Program.MessageError("Class.FunctionDataCls", "repackagePending", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction cancelPending(string hn_no)
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

                        trn_patient tpt = cdc.trn_patients.Where(x => x.tpt_hn_no == hn_no).FirstOrDefault();
                        List<trn_patient_regi> list_regis = tpt.trn_patient_regis.ToList();
                        foreach (trn_patient_regi tpr in list_regis)
                        {
                            tpr.tpr_pending = false;
                            List<trn_patient_pending> list_pending = tpr.trn_patient_pendings.ToList();
                            foreach (trn_patient_pending tpp in list_pending)
                            {
                                tpp.tpp_status = 'C';
                            }
                        }
                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("Class.FunctionDataCls", "cancelPending", ex, false);
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
                Program.MessageError("Class.FunctionDataCls", "cancelPending", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction cancelPendingStation(int tpr_id, List<int> mrm_id)
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
                        List<trn_patient_pending> list_panding = tpr.trn_patient_pendings.Where(x => x.tpr_id == tpr_id && mrm_id.Contains(x.mrm_id)).ToList();
                        foreach (trn_patient_pending tpp in list_panding)
                        {
                            tpp.tpp_status = 'C';
                        }
                        if (tpr.tpr_pe_status == "RS" && list_panding.Count() > 0)
                        {
                            tpr.tpr_pending_no_station = true;
                        }
                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("Class.FunctionDataCls", "cancelPendingStation", ex, false);
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
                Program.MessageError("Class.FunctionDataCls", "cancelPendingStation", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction chkVipType(int tpr_id, int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    mst_hpc_site mhs = cdc.mst_hpc_sites.Where(x => x.mhs_id == mhs_id).FirstOrDefault();
                    if (tpr != null && mhs != null)
                    {
                        if (mhs.mhs_code == "01HPC3" && tpr.trn_patient.tpt_vip_hpc == true)
                        {
                            return StatusTransaction.True;
                        }
                        else
                        {
                            return StatusTransaction.False;
                        }
                    }
                    else
                    {
                        return StatusTransaction.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "chkVipType", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction rePendingCheckB(ref trn_patient_regi tpr)
        {
            try
            {
                tpr.tpr_pending_cancel_onday = true;
                tpr.tpr_pending = true;
                List<trn_patient_plan> list_tpl = tpr.trn_patient_plans.Where(x => (new List<char?> { 'N', 'H' }).Contains(x.tpl_status)).ToList();
                List<int> list_mvt_id = list_tpl.Select(x => x.mvt_id).Distinct().ToList();
                foreach (int mvt_id in list_mvt_id)
                {
                    List<int> list_mrm_id = new EmrClass.GetDataMasterCls().GetAllRoomByMvt(mvt_id);
                    foreach (int mrmID in list_mrm_id)
                    {
                        trn_patient_pending tpp = new trn_patient_pending();
                        tpp.mrm_id = mrmID;
                        tpp.tpp_status = 'P';
                        tpr.trn_patient_pendings.Add(tpp);
                    }
                }
                list_tpl.ForEach(x => x.tpl_status = 'D');
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "rePendingCheckB", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction getCurrent_tps_id(int tpr_id, int mrm_id, ref int tps_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue tps = cdc.trn_patient_queues
                                               .Where(x => x.tpr_id == tpr_id &&
                                                           x.mrm_id == mrm_id)
                                               .OrderByDescending(x => x.tps_create_date)
                                               .FirstOrDefault();
                    if (tps != null)
                    {
                        tps_id = tps.tps_id;
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
                Program.MessageError("Class.FunctionDataCls", "getCurrent_tps_id", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction checkStatusWaiting(int tpr_id, int mrm_id, ref int tps_id, ref string queueNo)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    queueNo = tpr.tpr_queue_no;
                    trn_patient_queue tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_id &&
                                                                              x.tps_status == "NS" &&
                                                                              x.tps_ns_status == "QL").FirstOrDefault();
                    if (tps != null)
                    {
                        tps_id = tps.tps_id;
                        return StatusTransaction.True;
                    }
                    else
                    {
                        return StatusTransaction.False;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "checkStatusWaiting", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction calSendBasic()
        {
            try
            {
                int mhs_id = Program.CurrentSite.mhs_id;
                return calSendBasic(mhs_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "calSendBasic", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction calSendBasic(int mhs_id)
        {
            try
            {
                using (inheriteContext cdc = new inheriteContext(inheriteContext.paramIndex.Site))
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        cdc.proc_check_VitalSign(mhs_id);

                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("Class.FunctionDataCls", "calSendBasic", ex, false);
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
                Program.MessageError("Class.FunctionDataCls", "calSendBasic", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction callQueue(int tps_id)
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

                        trn_patient_queue tps = cdc.trn_patient_queues
                                                   .Where(x => x.tps_id == tps_id &&
                                                               x.tps_status == "NS" &&
                                                               x.tps_ns_status == "QL").FirstOrDefault();
                        if (tps != null)
                        {
                            DateTime dateNow = Program.GetServerDateTime();
                            tps.mrd_id = Program.CurrentRoom.mrd_id;
                            tps.tps_ns_status = "WR";
                            tps.tps_update_by = Program.CurrentUser.mut_username;
                            tps.tps_update_date = dateNow;
                            try
                            {
                                cdc.SubmitChanges();
                                cdc.Transaction.Commit();
                                Program.CurrentRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tps.tpr_id).FirstOrDefault();
                                Program.CurrentPatient_queue = tps;
                                return StatusTransaction.True;
                            }
                            catch (System.Data.Linq.ChangeConflictException)
                            {
                                cdc.Transaction.Rollback();
                                return StatusTransaction.False;
                            }
                        }
                        else
                        {
                            return StatusTransaction.False;
                        }
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("Class.FunctionDataCls", "callQueue", ex, false);
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
                Program.MessageError("Class.FunctionDataCls", "callQueue", ex, false);
                return StatusTransaction.Error;
            }
        }

        public StatusTransaction checkStatusPatientOnCheckPointB(int tpr_id, int mrm_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<int> list_mrm_id_checkB = cdc.mst_room_hdrs.Where(x => x.mrm_code == "CB").Select(x => x.mrm_id).ToList();
                    if (list_mrm_id_checkB.Contains(mrm_id))
                    {
                        trn_patient_queue patient_queue = cdc.trn_patient_queues
                                                             .Where(x => x.tpr_id == tpr_id &&
                                                                           list_mrm_id_checkB.Contains((int)x.mrm_id) &&
                                                                           x.tps_status == "NS" && x.tps_ns_status == "QL").FirstOrDefault();
                        if (patient_queue != null)
                        {
                            return StatusTransaction.True;
                        }
                        else
                        {
                            return StatusTransaction.False;
                        }
                    }
                    else
                    {
                        return StatusTransaction.NoProcess;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "checkStatusPatientOnRoom", ex, false);
                return StatusTransaction.Error;
            }
        }

        public bool CheckPatientPackage(int tpr_id) // สำหรับ จ่าย quota ที่ register เท่านั้น
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    int CountSet = PatientRegis.trn_patient_order_sets.Count();
                    int CountItem = PatientRegis.trn_patient_order_items.Count();
                    return CountSet > 0 || CountItem > 0;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "CheckPatientPackage", ex, false);
                return false;
            }
        }


        public vw_patient_room WaitingDoctor(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    mst_event MstEventPE = cdc.mst_events.Where(x => x.mvt_code == "PE").FirstOrDefault();
                    trn_patient_plan PatientPlan = PatientRegis.trn_patient_plans.Where(x => x.mvt_id == MstEventPE.mvt_id && x.tpl_status == 'N').FirstOrDefault();
                    if (PatientPlan != null)
                    {
                        string doctor_code = PatientRegis.tpr_req_doc_code != null ? PatientRegis.tpr_req_doc_code : PatientRegis.tpr_pe_doc_code;
                        mst_user_type userType = cdc.mst_user_types.Where(x => x.mut_username == doctor_code).FirstOrDefault();
                        if (userType == null)
                        {
                            return null;
                        }
                        else
                        {
                            List<int?> mrd_idDoctor = cdc.mst_room_dtls.Where(x => x.mst_room_hdr.mhs_id == PatientRegis.mhs_id &&
                                                                                   x.mst_room_hdr.mrm_code == "DC")
                                                         .Select(x => (int?)x.mrd_id).ToList();

                            log_user_login logUser = cdc.log_user_logins.Where(x => x.mut_id == userType.mut_id && 
                                                                                    mrd_idDoctor.Contains(x.mrd_id) &&
                                                                                    x.lug_end_date == null).FirstOrDefault();

                            if (logUser == null)
                            {
                                return null;
                            }
                            else
                            {
                                List<Class.WaitingListCls.WaitingListDtl> listQueue = new Class.WaitingListCls().getWaitingRoomDtl(logUser.mrd_id, logUser.mut_id);
                                if (listQueue.Count > 0)
                                {
                                    return null;
                                }
                                else
                                {
                                    mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mst_room_dtls.Any(y => y.mrd_id == logUser.mrd_id)).FirstOrDefault();

                                    List<trn_patient_queue> listQ = cdc.trn_patient_queues.Where(x => x.mrm_id == roomHdr.mrm_id &&
                                                                                                      x.tps_create_date.Value.Date == dateNow.Date &&
                                                                                                      (x.tps_status == "NS" || x.tps_status == "WK") &&
                                                                                                      (x.tps_call_status == null || dateNow >= x.tps_hold_date)).ToList();
                                    int count_mrm = listQ.Where(x => (x.trn_patient_regi.tpr_req_doc_code != null)
                                                                     ? (x.trn_patient_regi.tpr_req_doc_code == userType.mut_username)
                                                                     : (x.trn_patient_regi.tpr_pe_doc_code == userType.mut_username)).Count();

                                    if (count_mrm > 0)
                                    {
                                        return null;
                                    }
                                    else
                                    {
                                        return new vw_patient_room { mrm_id = roomHdr.mrm_id, mvt_id = MstEventPE.mvt_id };
                                    }
                                }
                                //int count_mrd = cdc.trn_patient_queues.Where(x => x.mrd_id == logUser.mrd_id &&
                                //                                                  x.tps_create_date.Value.Date == dateNow.Date &&
                                //                                                  (x.tps_status == "NS" || x.tps_status == "WK") &&
                                //                                                  (x.tps_call_status != "HD" || dateNow >= x.tps_hold_date)).Count();
                                //if (count_mrd > 0)
                                //{
                                //    return null;
                                //}
                                //else
                                //{
                                //    mst_room_hdr roomHdr = cdc.mst_room_hdrs.Where(x => x.mst_room_dtls.Any(y => y.mrd_id == logUser.mrd_id)).FirstOrDefault();

                                //    List<trn_patient_queue> listQ = cdc.trn_patient_queues.Where(x => x.mrm_id == roomHdr.mrm_id &&
                                //                                                                      x.tps_create_date.Value.Date == dateNow.Date &&
                                //                                                                      (x.tps_status == "NS" || x.tps_status == "WK") && 
                                //                                                                      (x.tps_call_status == null || dateNow >= x.tps_hold_date)).ToList();
                                //    int count_mrm = listQ.Where(x => (x.trn_patient_regi.tpr_req_doc_code != null)
                                //                                     ? (x.trn_patient_regi.tpr_req_doc_code == userType.mut_username)
                                //                                     : (x.trn_patient_regi.tpr_pe_doc_code == userType.mut_username)).Count();

                                //    if (count_mrm > 0)
                                //    {
                                //        return null;
                                //    }
                                //    else
                                //    {
                                //        return new vw_patient_room { mrm_id = roomHdr.mrm_id, mvt_id = MstEventPE.mvt_id };
                                //    }
                                //}
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ChkSendAutoNewModule(trn_patient_regi PatientRegis)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int site = PatientRegis.tpr_site_use != null ? (int)PatientRegis.tpr_site_use : PatientRegis.mhs_id;
                    bool? checkUseNewModule = cdc.mst_hpc_sites
                                                 .Where(x => x.mhs_id == site)
                                                 .Select(x => x.mhs_acitive_new_module).FirstOrDefault();
                    if (checkUseNewModule == null)
                    {
                        return false;
                    }
                    else
                    {
                        return (bool)checkUseNewModule;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("Class.FunctionDataCls", "ChkSendAutoNewModule(trn_patient_regi PatientRegis)", ex, false);
                return false;
            }
        }
    }

    public enum SendType
    {
        Normal,
        Skip,
        Pending
    }
}

