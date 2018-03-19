using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Windows.Forms;

namespace BKvs2010.Class
{
    class SendAutoCls
    {
        private StatusTransaction SendAutoAllRoom(bool SkipType, ref int mrm_id, ref int mvt_id, ref bool ConditionPEsite2, ref bool IsCallLab, ref bool SendEyeDoc)
        {
            bool Iscompleted = false;
            Program.RefreshWaiting = false;
            int tptID = 0;
            int tpr_id = 0;
            double limittime = Program.GetLimitTime("EDT");

            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();

            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                try
                {
                    tptID = Program.CurrentRegis.tpt_id;
                    tpr_id = Program.CurrentRegis.tpr_id;

                    DateTime dateNow = Program.GetServerDateTime();


                    int mrmID = 0;
                    int mvtID = 0;
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
                    var waitDoctor = (from t1 in dbc.vw_patient_rooms
                                      where t1.tpr_id == tpr_id
                                        && t1.mvt_type_cate == 'M'
                                        && t1.mrm_code == "DC"
                                        && t1.login_flag == "Y"
                                          //&& t1.waiting_person == 0
                                        && t1.mvt_id != Program.CurrentPatient_queue.mvt_id
                                        && t1.mhs_id == SendtoSiteId
                                        && t1.site_rm == SendtoSiteId
                                      select t1).FirstOrDefault();


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
                    if (Program.CurrentSite.mhs_code == "01CHK" && waitDoctor != null && (func.checkDoctorRequest(tpr_id, SendtoSiteId, true) == true) &&
                        ((countDoctorPass == 0 && countNursePass == 0) || (countDoctorPass != 0 && countNursePass != 0)))
                    {
                        mrmID = Convert.ToInt32(waitDoctor.mrm_id);
                        mvtID = Convert.ToInt32(waitDoctor.mvt_id);
                    }
                    else
                    {
                        bool gotoEye = false;
                        if (SkipType)
                        {
                            gotoEye = false;
                        }
                        else
                        {
                            gotoEye = CheckAutoGotoEye(ref mrmID, ref mvtID, ref Iscompleted, ref isHaveNextRoom, ref IsCallLab, ref SendEyeDoc);
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
                                }

                                if (objnextroom != null)
                                {
                                    mrmID = Convert.ToInt32(objnextroom.mrm_id);
                                    mvtID = Convert.ToInt32(objnextroom.mvt_id);
                                    mvtcode = objnextroom.mvt_code;
                                }
                                else
                                {
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
                                                        }
                                                        else
                                                        {
                                                            Iscompleted = false;
                                                            Program.RefreshWaiting = true;
                                                            System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ ผู้รับบริการจะถูกส่งไป Check point B", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                                            return StatusTransaction.False;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Iscompleted = false;
                                                        Program.RefreshWaiting = true;
                                                        //System.Windows.Forms.MessageBox.Show("Can't send to next room. Please contact Admin or Send Manual", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                                        System.Windows.Forms.MessageBox.Show("ระบบไม่สามารถส่งห้องที่เหลือแบบอัตโนมัติได้ ผู้รับบริการจะถูกส่งไป Check point B", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                                        return  StatusTransaction.False;
                                                    }
                                                }
                                            }
                                        }

                                        loopSearchNextRoom = loopSearchNextRoom + 1;

                                        if (mrmID == 0)
                                        {
                                            Limitvalue = 1440;
                                            goto CheckAgain;
                                        }

                                    }
                                }
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
                                            mvtcode = "CC";
                                            isHaveNextRoom = true;
                                            IsCallLab = true;

                                            if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                                            {
                                                ConditionPEsite2 = true;
                                            }
                                        }
                                        else if (result == DialogResult.No)
                                        {
                                            DateTime dtnow = Program.GetServerDateTime();
                                            string roomname = String.Empty;
                                            mrmID = mst.GetMstRoomHdr("BK").mrm_id;
                                            mvtID = mst.GetMstEvent("BK").mvt_id;

                                            var objRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                                            if (objRegis != null)
                                            {
                                                objRegis.tpr_status = "WB";
                                                objRegis.tpr_pe_status = "RS";
                                            }
                                            MessageBox.Show("Checkup Process Completed", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            mvtcode = "BK";
                                            isHaveNextRoom = false;
                                            IsCallLab = true;
                                        }
                                    }
                                    else
                                    {
                                        //ถ้าไม่ห้องถัดไปให้ไปที่ checkpoint CC
                                        mrmID = mst.GetMstRoomHdr("CC").mrm_id;
                                        mvtID = mst.GetMstEvent("CC").mvt_id;
                                        mvtcode = "CC";
                                        isHaveNextRoom = true;
                                        IsCallLab = true;

                                        //เพิ่มเงื่อนไข 
                                        if (Program.CurrentSite.mhs_extra_pe_type == true && Program.CurrentRegis.tpr_pe_site2 == 'N' && (Program.CurrentRegis.tpr_pd_pe_site2 == null || Program.CurrentRegis.tpr_pd_pe_site2 == false))
                                        {
                                            ConditionPEsite2 = true;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Program.MessageError("SendAutoCls", "SendAutoAllRoom", ex, false);
                    return StatusTransaction.Error;
                }
            }
            Program.RefreshWaiting = true;

            ////noina cr.

            if (Iscompleted == true)
            {
                return StatusTransaction.True;
            }
            else
            {
                return StatusTransaction.False;
            }
        }

        public StatusTransaction SendAutoOnStation(int tpr_id, int mrm_id, int mvt_id, bool ConditionPEsite2, bool IsCallLab, bool isHaveNextRoom, bool SendEyeDoc)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                try
                {
                    //if (new EmrClass.GetDataMasterCls().GetMstRoomHdr(mrm_id).mrm_code == "DC")
                    //{
                    //    new Class.FunctionDataCls().stampPEDoctor(Program.CurrentRegis.tpr_id);
                    //}

                    DateTime dateNow = Program.GetServerDateTime();





                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    trn_patient_queue objQueue = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_id &&
                                                                                   x.mvt_id == mvt_id).FirstOrDefault();
                    if (objQueue == null)
                    {
                        objQueue = new trn_patient_queue();
                        //dbc.trn_patient_queues.InsertOnSubmit(objQueue);
                        tpr.trn_patient_queues.Add(objQueue);
                        objQueue.mrm_id = mrm_id;
                        objQueue.mvt_id = mvt_id;
                    }
                    objQueue.mrd_id = null;
                    objQueue.tps_start_date = dateNow;
                    objQueue.tps_end_date = Program.GetServerDateTime();
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
                        //objQueue.tps_hold_date = dateNow.AddMinutes(limittime);
                    }

                    objQueue.tps_create_by = Program.CurrentUser.mut_username;
                    objQueue.tps_create_date = dateNow;
                    objQueue.tps_update_by = Program.CurrentUser.mut_username;
                    objQueue.tps_update_date = dateNow;

                    if (ConditionPEsite2)
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
                    }


                    // morn clear Unit Display
                    //new ClsTCPClient().sendClearUnitDisplay();
                    //Program.CurrentRegis = null;
                    //Program.CurrentPatient_queue = null;
                    AlertOutDepartment.StopTime();
                    //Iscompleted = true;

                    if (IsCallLab)
                    {
                        try
                        {
                            //using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                            //{
                            //    var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tptID).FirstOrDefault();
                            //    //CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no,Program.CurrentRegis.tpr_id);
                            //    CallQueue.SetUpdateTextfile(currentPatient.tpt_hn_no, tpr_id);
                            //    ClsBasicMeasurement.SaveBasicMeasurment(tpr_id, IsCallLab);

                            //    //Update LR สูติอย่างเดียวเนื่องจากไม่มีใน callDataTakeCare Event Code สูติ= PT
                            //    SetLR_Obstaticresult(dbc, tpr_id);
                            //}

                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            //using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                            //{
                            //    var currentPatient = dbc.trn_patients.Where(x => x.tpt_id == tptID).FirstOrDefault();
                            //    CheckUpLabClass.ws_Getcheckuplab_Async(currentPatient.tpt_hn_no, tpr_id);
                            //    return StatusTransaction.True;
                            //}

                        }
                        catch (Exception ex)
                        {
                            Program.MessageError("SendAutoCls", "SendAutoOnStation", ex, false);
                            return StatusTransaction.False;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("SendAutoCls", "SendAutoOnStation", ex, false);
                    return StatusTransaction.Error;
                }
            }
            return StatusTransaction.Error;
        }

        public StatusTransaction SendAuto()
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                return SendAuto();
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SendAuto", ex, false);
                return StatusTransaction.Error;
            }
        }
        public bool CheckAutoGotoEye(ref int mrmID, ref int mvtID, ref bool IsCompleted, ref bool isHaveNextRoom, ref bool IsCallLab, ref bool SendEyeDoc)
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
                {// กรณที่มีการหยอดตาแล้ว==> ถูกส่งไปห้องอื่น==> ให้ส่งไปห้องตาอีกรอบ
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
            }
        }


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
                Program.MessageError("SendAutoCls", "GetPathPatho", ex, false);
            }
            return "";

        }

        public StatusTransaction SendAutoBySkip()
        {
            try
            {


                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("SendAutoCls", "SendAutoBySkip", ex, false);
                return StatusTransaction.Error;
            }
        }
    }
}
