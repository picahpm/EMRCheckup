using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.Class;
using System.Globalization;
namespace BKvs2010
{
    public partial class frmPHM : Form
    {
        PopupUltrasoundLower.ResultPopupUltrasoundLower resultUltrasound = PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater;

        public frmPHM()
        {
            InitializeComponent();
            uiAllLeft1.OnRefreshStatusED += new Usercontrols.UIAllLeft.RefreshStatusED(uiAllLeft1_OnRefreshStatusED);
        }
        public int SetTprID { get; set; }
        public int siteitem { get; set; }
        public string HNno = "";
        DateTime dtCurrent = Program.GetServerDateTime();
        private void uiAllLeft1_OnRefreshStatusED()
        {
            StatusWaitCallQueue();
        }
        private void OnUCFooterClicked(int tprid, int mhs_id)
        {
            // Handle event Footer from here
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            siteitem = mhs_id;
            SetTprID = tprid;
            trn_patient_queue objQtxp = (from t1 in dbc.trn_patient_queues
                                         where //t1.mst_room_hdr.mhs_id == mhs_id &&
                                              (t1.mst_room_hdr.mrm_code == "SC" || t1.mst_room_hdr.mrm_code == "RG")
                                         && t1.tpr_id == tprid
                                         select t1).FirstOrDefault();
            if (objQtxp != null)
            {

                loadPHMList("");

                //Load PHM Data
                var objphm = (from t1 in dbc.trn_phm_hdrs where t1.tpr_id == tprid select t1).FirstOrDefault();
                if (objphm != null)
                {
                    PHMhdrbindingSource1.DataSource = objphm;
                    CalPatient(tprid);
                    CalTab2Sec12(tprid);
                    CalTab2Sec3(tprid);
                    CalTab2Sec4(tprid);
                    CalTab3(tprid);
                    CalTab4(tprid);
                    CalTab5(tprid);
                    CalTab6();
                    ShowChat(tprid);
                }


                btnReady.Enabled = false;
                btnCallQueue.Enabled = false;
                btnHold.Enabled = false;
                //btnCancel.Enabled = false;
                btnSave.Enabled = false;
                btnSaveDraft.Enabled = false;
                //btnSaveandSendauto.Enabled = false;
                uiAllLeft1.LoadDataAll(tprid);
                var currentPHM = (from t1 in dbc.trn_phm_hdrs where t1.tpr_id == tprid select t1).FirstOrDefault();
                if (currentPHM.tph_create_by == Program.CurrentUser.mut_username)
                {
                    btnSaveDraft.Enabled = true;
                }
                else
                {
                    btnSaveDraft.Enabled = false;
                }
            }

            frmbg.Close();
        }

        private void LoadUI()
        {
            uiAllLeft1.LoadDataAll();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private string strTrue = "ใช่(Yes)";
        private string strFalse = "ไม่ใช่(No)";
        private void StatusWaitCallQueue()
        {
            btnReady.Enabled = false;
            btnCallQueue.Enabled = true;
            btnHold.Enabled = false;
            btnSave.Enabled = false;
            btnSaveDraft.Enabled = false;

            this.LoadUI();
        }
        private void StatusCallQueue()
        {
            lbAlertMsg.Text = "";
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            //btnCancel.Enabled = true;

            btnSave.Enabled = true;
            btnSaveDraft.Enabled = true;
            //btnSaveandSendauto.Enabled = true;
        }
        private void StatusSaveData()
        {
            lbAlertMsg.Text = "";
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            //btnCancel.Enabled = true;

            btnSave.Enabled = true;
            btnSaveDraft.Enabled = true;
            //btnSaveandSendauto.Enabled = true;
        }

        private bool IsLoadData = true;
        private void frmPHM_Load(object sender, EventArgs e)
        {
            LoadHandlerCountDown();
            this.Text = Program.GetRoomName();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();
            
            //Load Data
            //Race
            //var objRaceGroup = (from t1 in dbc.mst_race_grps
            //                    orderby t1.mag_ename
            //                    select new DropdownData
            //                    {
            //                        Code = t1.mag_id,
            //                        Name = t1.mag_ename
            //                    }).ToList();
            //DDRaceGroup.ValueMember = "Code";
            //DDRaceGroup.DisplayMember = "Name";
            //DDRaceGroup.DataSource = objRaceGroup;

            var objRaceGroup = (from t1 in dbc.mst_race_grps
                                orderby t1.mag_ename
                                select new DropdownData
                                {
                                    Code = t1.mag_id,
                                    Name = t1.mag_ename
                                }).ToList();
            DropdownData newselect = new DropdownData();
            newselect.Name = "";
            objRaceGroup.Insert(0, newselect);
            DDRaceGroup.ValueMember = "Code";
            DDRaceGroup.DisplayMember = "Name";
            DDRaceGroup.DataSource = objRaceGroup;

            List<ComboboxItem> newbb=new List<ComboboxItem>();
            newbb.Add(new ComboboxItem("", ""));
            newbb.Add(new ComboboxItem("Make Appointment at Heart Clinic", "MH"));
            newbb.Add(new ComboboxItem("Other hospital", "O"));
            newbb.Add(new ComboboxItem("Private doctor", "P"));
            txt2sec3_StatusRefertoClinic.DataSource = newbb;
            txt2sec3_StatusRefertoClinic.DisplayMember = "Text";
            txt2sec3_StatusRefertoClinic.ValueMember = "Value";
            CBsec3_RefertoClinic.Items.Add(new ComboboxItem("", null));
            CBsec3_RefertoClinic.Items.Add(new ComboboxItem("Cardiology", "Cardiology"));
            CBsec3_RefertoClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("", null));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));

            txt6sec1_StatusRefertoClinic.DataSource = newbb;
            txt6sec1_StatusRefertoClinic.DisplayMember = "Text";
            txt6sec1_StatusRefertoClinic.ValueMember = "Value";
            txt6sec1_RefertoClinic.Items.Add(new ComboboxItem("", null));
            txt6sec1_RefertoClinic.Items.Add(new ComboboxItem("Cardiology", "Cardiology"));
            txt6sec1_RefertoClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("", null));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));

            List<ComboboxItem> newbb3 = new List<ComboboxItem>();
            newbb3.Add(new ComboboxItem("", ""));
            newbb3.Add(new ComboboxItem("Make Appointment at Heart Clinic", "MD"));
            newbb3.Add(new ComboboxItem("Other hospital", "O"));
            newbb3.Add(new ComboboxItem("Private doctor", "P"));
            txt3sec3_ReasonFornotRefer.DataSource = newbb3;
            txt3sec3_ReasonFornotRefer.DisplayMember = "Text";
            txt3sec3_ReasonFornotRefer.ValueMember = "Value";
            txt3sec3_ReferToClinic.Items.Add(new ComboboxItem("", null));
            txt3sec3_ReferToClinic.Items.Add(new ComboboxItem("Diabetic Center", "Diabetic Center"));
            txt3sec3_ReferToClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("", null));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));
            
            txt6sec2_ReasonFornotRefer.DataSource = newbb3;
            txt6sec2_ReasonFornotRefer.DisplayMember = "Text";
            txt6sec2_ReasonFornotRefer.ValueMember = "Value";
            txt6sec2_ReferToClinic.Items.Add(new ComboboxItem("", null));
            txt6sec2_ReferToClinic.Items.Add(new ComboboxItem("Diabetic Center", "Diabetic Center"));
            txt6sec2_ReferToClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("", null));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));

            SetDDReasonforNotRefer(txt4sec1_StatusReferClinic);
            SetDDReasonforNotRefer(txt5sec1_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt5sec2_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt5sec3_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec1_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec2_ReasonFornotRefer);
            SetDDReasonforNotRefer(txt6sec3_StatusReferClinic);
            SetDDReasonforNotRefer(txt6sec4_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec5_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec6_StatusRefertoClinic);

            if (SetTprID > 0)
            {
                OnUCFooterClicked(SetTprID, siteitem);
            }
            else
            {
                LoadData(SetTprID);
               
            }
            loadPHMList("");
            IsLoadData = false;
            txt2sec3_StatusRefertoClinic_SelectedValueChanged(null, null);
            txt3sec3_ReasonFornotRefer_SelectedValueChanged(null, null);

           //// ChangeFontColor(panel3PersonalHealthManagment);
           // //ChangeFontColor(groupBox3);
           // //ChangeFontColor(groupBox4);
           // //ChangeFontColor(groupBox5);
           // //ChangeFontColor(groupBox6);
           // //ChangeFontColor(panel6);
           // //ChangeFontColor(panel7);
           // //ChangeFontColor(panel11);
           // //ChangeFontColor(panel12);
           // //ChangeFontColor(panel13);
           // //ChangeFontColor(panel4Diabetes);
           // ChangeFontColor(groupBox11);
           // ChangeFontColor(groupBox12);
           
            frmbg.Close();
        }
        private void SetDDReasonforNotRefer(ComboBox cb)
        {
            List<ComboboxItem> newbb1 = new List<ComboboxItem>();
            newbb1.Add(new ComboboxItem("", ""));
            newbb1.Add(new ComboboxItem("Make Appointment", "M"));
            newbb1.Add(new ComboboxItem("Other hospital", "O"));
            newbb1.Add(new ComboboxItem("Private doctor", "P"));
            cb.DataSource = newbb1;
            cb.DisplayMember = "Text";
            cb.ValueMember = "Value";
        }
        private void SetCombobox(ComboBox cb)
        {
            cb.Items.Add(new ComboboxItem("1.Refer to breast clinic", "1.Refer to breast clinic"));
            cb.Items.Add(new ComboboxItem("2.make appoiinment and brest clinic", "2.make appoiinment and brest clinic"));
            cb.Items.Add(new ComboboxItem("3.not refer to brest clinic(other hospital)", "3.not refer to brest clinic(other hospital)"));
            cb.Items.Add(new ComboboxItem("4.not refer to brest clinic(private doctor)", "4.not refer to brest clinic(private doctor)"));
        }
        private void LoadData(int tpr_id)
        {
            try
            {
                if (!Program.IsDummy)
                {
                    CallQueue.P_CallHistoryQueue();
                }
                if (Program.CurrentRegis != null)
                {
                    //ChangeFontColor(panel3PersonalHealthManagment);
                    //ChangeFontColor(groupBox3);
                    //ChangeFontColor(groupBox4);
                    //ChangeFontColor(groupBox5);
                    //ChangeFontColor(groupBox6);
                    //ChangeFontColor(panel6);
                    //ChangeFontColor(panel7);
                    //ChangeFontColor(panel11);
                    //ChangeFontColor(panel12);
                    //ChangeFontColor(panel13);
                    //ChangeFontColor(panel4Diabetes);
                    //ChangeFontColor(groupBox11);
                    //ChangeFontColor(groupBox12);
                   
                    tpr_id = Program.CurrentRegis.tpr_id;
                    loadPHMList("");
                    var getnation = (from t in dbc.trn_patient_regis
                                     join t2 in dbc.trn_patients
                                     on t.tpt_id equals t2.tpt_id
                                     where t.tpr_id == tpr_id
                                     select t2.tpt_nation_code).FirstOrDefault();
                    if (getnation != null)
                    {
                        if (getnation == "TH")
                        {
                            DDRaceGroup.SelectedValue = 4;//"ASIAN";
                            DDRaceGroup.Text = "ASIAN";
                            int ddd = Convert.ToInt32(DDRaceGroup.SelectedValue);
                            var objRace = (from t1 in dbc.mst_races
                                           where t1.mag_id == ddd
                                           orderby t1.mae_ename
                                           select new DropdownData
                                           {
                                               Code = t1.mae_id,
                                               Name = t1.mae_ename
                                           }).ToList();
                            DDRace.ValueMember = "Code";
                            DDRace.DisplayMember = "Name";
                            DDRace.DataSource = objRace;
                            DDRace.Text = "THAI";
                        }
                        else
                        {
                            DDRaceGroup.DataSource = null;
                            var objRaceGroup = (from t1 in dbc.mst_race_grps
                                                orderby t1.mag_ename
                                                select new DropdownData
                                                {
                                                    Code = t1.mag_id,
                                                    Name = t1.mag_ename
                                                }).ToList();
                            DropdownData newselect = new DropdownData();
                            newselect.Name = "";
                            objRaceGroup.Insert(0, newselect);
                            DDRaceGroup.ValueMember = "Code";
                            DDRaceGroup.DisplayMember = "Name";
                            DDRaceGroup.DataSource = objRaceGroup;
                            DDRace.DataSource = null;

                            //int ddd = Convert.ToInt32(DDRaceGroup.SelectedValue);
                            //var objRace = (from t1 in dbc.mst_races
                            //               where t1.mag_id == ddd
                            //               orderby t1.mae_ename
                            //               select new DropdownData
                            //               {
                            //                   Code = t1.mae_id,
                            //                   Name = t1.mae_ename
                            //               }).ToList();
                            //DropdownData newselect2 = new DropdownData();
                            //newselect2.Name = "";
                            //objRace.Insert(0, newselect2);
                            //DDRace.ValueMember = "Code";
                            //DDRace.DisplayMember = "Name";
                            //DDRace.DataSource = objRaceGroup;
                        }
                    }
                    ////Load ReferToClinic
                    GetReferToClinic(CBsec3_RefertoClinic);
                    GetReferToClinic(txt3sec3_ReferToClinic);
                    
                    GetReferToClinic(txt4sec1_RefertoClinic);
                    GetReferToClinic(txt5sec1_RefertoClinic);
                    GetReferToClinic(txt5sec2_ReferToClinic);
                    GetReferToClinic(txt5sec3_ReferToClinic);
                    GetReferToClinic(txt6sec1_RefertoClinic);
                    GetReferToClinic(txt6sec2_ReferToClinic);
                    GetReferToClinic(txt6sec3_RefertoClinic);
                    GetReferToClinic(txt6sec4_RefertoClinic);
                    GetReferToClinic(txt6sec5_ReferToClinic);
                    GetReferToClinic(txt6sec6_ReferToClinic);
                    ////EndLoad ReferToClinic

                    //Load PHM Data 
                    var objphm = (from t1 in dbc.trn_phm_hdrs where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                    if (objphm != null)
                    {
                        PHMhdrbindingSource1.DataSource = objphm;

                        // Set ค่า Group Race
                        var objdataCBHealthCheckUPProgram = (from t1 in dbc.mst_race_grps
                                                             where t1.mag_tname == objphm.tph_race_grp
                                                             select new DropdownData
                                                             {
                                                                 Code = t1.mag_id,
                                                                 Name = t1.mag_ename
                                                             }).FirstOrDefault();
                        if (objdataCBHealthCheckUPProgram != null)
                        {
                            DDRaceGroup.SelectedIndex = DDRaceGroup.FindString(objdataCBHealthCheckUPProgram.Name);
                            //set ค่า Race
                            int ddd = Convert.ToInt32(DDRaceGroup.SelectedValue);
                            
                            var objRace = (from t1 in dbc.mst_races
                                           where t1.mag_id == ddd
                                           select new DropdownData
                                           {
                                               Code = t1.mae_id,
                                               Name = t1.mae_ename
                                           }).ToList();
                            DDRace.ValueMember = "Code";
                            DDRace.DisplayMember = "Name";
                            DDRace.DataSource = objRace;
                            var objrace = (from t1 in dbc.mst_races
                                           where t1.mae_ename == objphm.tph_race
                                           select new DropdownData
                                           {
                                               Code = t1.mag_id,
                                               Name = t1.mae_ename
                                           }).FirstOrDefault();
                            if (objrace != null)
                            {
                                DDRace.SelectedIndex = DDRace.FindString(objrace.Name);
                            }
                        }
                        else
                        {
                            DDRaceGroup.SelectedIndex = 0;
                        }

                    }
                    else
                    {
                        PHMhdrbindingSource1.DataSource = (from t1 in dbc.trn_phm_hdrs select t1).Take(0);
                        PHMhdrbindingSource1.AddNew();
                    }

                    CalPatient(tpr_id);
                    CalTab2Sec12(tpr_id);
                    CalTab2Sec3(tpr_id);
                    CalTab2Sec4(tpr_id);
                    CalTab3(tpr_id);
                    CalTab4(tpr_id);
                    CalTab5(tpr_id);
                    CalTab6();

                    ShowChat(tpr_id);
                    this.LoadUI();
                    StatusCallQueue();

                    string asian;
                    if (DDRaceGroup.Text == "ASIAN")
                    {
                        asian = "A";
                    }
                    else
                    {
                        asian = "N";
                    }
                    GetBMI(asian);
                    //ChangeForeColor(txt2sec1_SmokePoint);
                    //ChangeForeColor(txt2sec1_HighBloodPoint);
                    //ChangeForeColor(txt2sec1_familyhistoryPoint);
                    //ChangeForeColor(txt2sec1_AgePoint);
                    //ChangeForeColor(txt2sec1_HDLPoint);

                    if (Program.CurrentPatient_queue.tps_status == "WK" && Program.CurrentPatient_queue.tps_ns_status == null)
                    {
                        StatusCallQueueReady();
                    }
                    else if (Program.CurrentPatient_queue.tps_status == "NS" && Program.CurrentPatient_queue.tps_ns_status == "WR")
                    {
                        StatusCallQueueWaitingReady();
                    }
                    button6_Click(null, null);
                }
                else
                {
                    if (Program.IsDummy)
                    {
                        btnCallQueue.Enabled = true;
                        btnHold.Enabled = false;
                        //btnCancel.Enabled = false;
                        btnSave.Enabled = false;
                        btnSaveDraft.Enabled = false;
                        //btnSaveandSendauto.Enabled = false;
                        LoadUI();
                    }
                    else
                    {
                        StatusWaitCallQueue();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CalPatient(int tpr_id)
        {   //Tab1
            if (tpr_id != 0)
            {
                var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                if (currentRegis != null)
                {
                    this.HNno = currentRegis.trn_patient.tpt_hn_no;
                    txtHn.Text = this.HNno;
                    txtTitle.Text = currentRegis.trn_patient.tpt_pre_name;
                    txtFirstName.Text = currentRegis.trn_patient.tpt_first_name;
                    txtLastName.Text = currentRegis.trn_patient.tpt_last_name;
                   
                    txtGender.Text = (currentRegis.trn_patient.tpt_gender == 'F') ? "Female" : ((currentRegis.trn_patient.tpt_gender == 'M') ? "Male" : "");
                    var dobdate = new DateTime();
                    try
                    {
                        dobdate = Convert.ToDateTime(currentRegis.trn_patient.tpt_dob);
                    }
                    catch (Exception)
                    {
                    }
                    trn_phm_hdr objcurrentphm = (trn_phm_hdr)PHMhdrbindingSource1.Current;
                    objcurrentphm.tph_age=(Program.GetServerDateTime().Year - dobdate.Year);
                    //txtAge.Text = .ToString();
                    txtNation.Text = currentRegis.trn_patient.tpt_nation_desc;
                    //if (currentRegis.trn_patient.tpt_nation_code == "TH")
                    //{
                    //    DDRaceGroup.SelectedValue = 4;
                    //    DDRaceGroup.SelectedText = "ASIAN";

                    //    DDRace.SelectedText = "THAI";
                    //}

                    trn_basic_measure_dtl objbmdtl = (from t1 in dbc.trn_basic_measure_dtls
                                                      where t1.trn_basic_measure_hdr.tpr_id == tpr_id
                                                      select t1).FirstOrDefault();
                  
                    if (objbmdtl != null)
                    {
                        txtBMI.Text = objbmdtl.tbd_bmi;
                        txtWeight.Text = objbmdtl.tbd_weight;
                        txtHeight.Text = objbmdtl.tbd_height;
                        txtWaistSize.Text = objbmdtl.tbd_waist;
                        txtSystolicBP.Text = objbmdtl.tbd_systolic;
                        txtDiastolicBP.Text = objbmdtl.tbd_diastolic;

                        // Add Sumit 17/01/2014
                    }
                    else
                    {
                        txtBMI.Text = "";
                        txtWeight.Text = "";
                        txtHeight.Text = "";
                        txtWaistSize.Text = "";
                        txtSystolicBP.Text = "";
                        txtDiastolicBP.Text = "";
                    }

                }
            }
            //ตาราง trn_ques_patient
            var objqp = (from t1 in dbc.trn_ques_patients where t1.tpr_id == tpr_id select t1).FirstOrDefault();
            string strTrue = "ใช่(Yes)";
            string strFalse = "ไม่ใช่(No)";
            if (objqp != null)
            {
                txtSec2_1.Text = (objqp.tqp_ill_cmed_hyper == true) ? strTrue : strFalse;
                txt2Sec_2.Text = (objqp.tqp_his_exercise == 'O' || objqp.tqp_his_exercise == 'N') ? strTrue : strFalse;
                txt2Sec_3.Text = (objqp.tqp_fwm_over_weight == 'Y') ? strTrue : strFalse;
                txt2Sec_4.Text = (objqp.tqp_his_smok == 'S' && (objqp.tqp_his_smok_dur <= 1 || objqp.tqp_his_smok_amt > 0)) ? strTrue : strFalse;
                txt2Sec_5.Text = (objqp.tqp_vinf_hepB_virus == 'Y') ? strTrue : strFalse;
                txt2Sec_6.Text = (objqp.tqp_vinf_hepA_virus == 'Y') ? strTrue : strFalse;
                txt2Sec_7.Text = (objqp.tqp_his_alcohol == 'R') ? strTrue : strFalse;
                txt2Sec_8.Text = (objqp.tqp_vinf_vaccine == 'L') ? strTrue : strFalse;

                txt91Coronary.Text = (objqp.tqp_fhis_fdis_coro == true && (objqp.tqp_fhis_fdis_coro_cs == 'B' || objqp.tqp_fhis_fdis_coro_cs == 'N')) ? strTrue : strFalse;
                txt91Diabetes.Text = (objqp.tqp_fhis_fdis_diab == true) ? strTrue : strFalse;
                txt91Stroke.Text = (objqp.tqp_fhis_fdis_stro == true || objqp.tqp_fhis_fdis_para == true) ? strTrue : strFalse;
                txt91Hypertension.Text = (objqp.tqp_fhis_fdis_hyper == true) ? strTrue : strFalse;
                txt91Cancer.Text = (objqp.tqp_fhis_fdis_canc == true) ? strTrue : strFalse;
                txt91Dyslipidemia.Text = (objqp.tqp_fhis_fdis_dysl == true) ? strTrue : strFalse;

                txt92Coronary.Text = (objqp.tqp_fhis_mdis_coro == true && (objqp.tqp_fhis_mdis_coro_cs == 'B' || objqp.tqp_fhis_mdis_coro_cs == 'N')) ? strTrue : strFalse;
                txt92Diabetes.Text = (objqp.tqp_fhis_mdis_diab == true) ? strTrue : strFalse;
                txt92Stroke.Text = (objqp.tqp_fhis_mdis_stro == true || objqp.tqp_fhis_mdis_para == true) ? strTrue : strFalse;
                txt92Hypertension.Text = (objqp.tqp_fhis_mdis_hyper == true) ? strTrue : strFalse;
                txt92Cancer.Text = (objqp.tqp_fhis_mdis_canc == true) ? strTrue : strFalse;
                txt92Dyslipidemia.Text = (objqp.tqp_fhis_mdis_dysl == true) ? strTrue : strFalse;

                txt93Coronary.Text = (objqp.tqp_fhis_bdis_coro == true && (objqp.tqp_fhis_bdis_coro_bfm == true || objqp.tqp_fhis_bdis_coro_nfm == true || objqp.tqp_fhis_bdis_coro_bm == true || objqp.tqp_fhis_bdis_coro_nm == true)) ? strTrue : strFalse;
                txt93Diabetes.Text = (objqp.tqp_fhis_bdis_diab == true) ? strTrue : strFalse;
                txt93Stroke.Text = (objqp.tqp_fhis_bdis_stro == true || objqp.tqp_fhis_bdis_para == true) ? strTrue : strFalse;
                txt93Hypertension.Text = (objqp.tqp_fhis_bdis_hyper == true) ? strTrue : strFalse;
                txt93Cancer.Text = (objqp.tqp_fhis_bdis_canc == true) ? strTrue : strFalse;
                txt93Dyslipidemia.Text = (objqp.tqp_fhis_bdis_dysl == true) ? strTrue : strFalse;

                txt10Coronary.Text = (objqp.tqp_ill_med_coro == true) ? strTrue : strFalse;
                txt10Abdominal.Text = (objqp.tqp_ill_med_abdd == true) ? strTrue : strFalse;
                txt10Periperal.Text=(objqp.tqp_ill_med_cper == true) ? strTrue : strFalse;
                txt10Transient.Text = (objqp.tqp_ill_med_sist == true || objqp.tqp_ill_med_para == true || objqp.tqp_ill_med_stro==true ) ? strTrue : strFalse;
                txt10Diabetes.Text = (objqp.tqp_ill_med_diab == true) ? strTrue : strFalse;
                txt10Cancer.Text= (objqp.tqp_ill_med_canc == true) ? strTrue : strFalse;
                txt10Hypertention.Text = (objqp.tqp_ill_med_hyper == true) ? strTrue : strFalse;
                txt10Dyslipidemia.Text = (objqp.tqp_ill_med_dysl == true) ? strTrue : strFalse;

            }
        }
        private void CalTab2Sec12(int tpr_id)
        {   // tab1
            var objqp = (from t1 in dbc.trn_ques_patients where t1.tpr_id == tpr_id select t1).FirstOrDefault();
            trn_phm_hdr currentPHM = (trn_phm_hdr)PHMhdrbindingSource1.Current;
            var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();
            if (currentRegis != null)
            {
                txtHn.Text = this.HNno;
                txtGender.Text = (currentRegis.trn_patient.tpt_gender == 'F') ? "Female" : ((currentRegis.trn_patient.tpt_gender == 'M') ? "Male" : "");
                txt2sec2_Gender.Text = txtGender.Text;
                var dobdate = new DateTime();
                try
                {
                    dobdate = Convert.ToDateTime(currentRegis.trn_patient.tpt_dob);
                }
                catch (Exception)
                {
                }
                txt2sec1_Age.Text = (Program.GetServerDateTime().Year - dobdate.Year).ToString();
                txt2sec2_Age.Text = txt2sec1_Age.Text;

                txt2sec1_Gender.Text = txtGender.Text;
            }
            string strTrue = "ใช่(Yes)";
            string strFalse = "ไม่ใช่(No)";
            if (objqp != null)
            {
                txt2sec1_Smoke.Text =(objqp.tqp_his_smok == 'S') ? strTrue : strFalse;
                txt2sec1_SmokePoint.Text = (txt2sec1_Smoke.Text == strTrue) ? "1" : "0";
                //currentPHM.tph_smok_pt = (objqp.tqp_his_smok == 'S') ? 1 : 0;

                //txt2sec1_SmokePoint.Text = (objqp.tqp_his_smok == 'S') ? "1":"0";
                var objdata = ((objqp.tqp_fhis_fdis_coro == true && (objqp.tqp_fhis_fdis_coro_cs == 'B' || objqp.tqp_fhis_fdis_coro_cs == 'N')) 
                    || (objqp.tqp_fhis_mdis_coro==true && (objqp.tqp_fhis_mdis_coro_cs == 'B' || objqp.tqp_fhis_mdis_coro_cs == 'N'))
                    //|| (objqp.tqp_fhis_bdis_coro == true && (objqp.tqp_fhis_bdis_coro_cs == 'B')) ? "1" : "0";
                    || (objqp.tqp_fhis_bdis_coro == true && (objqp.tqp_fhis_bdis_coro_bfm == true || objqp.tqp_fhis_bdis_coro_nfm == true || objqp.tqp_fhis_bdis_coro_bm == true || objqp.tqp_fhis_bdis_coro_nm == true))) ? "1" : "0";

               // txt2sec1_familyhistoryPoint.Text = objdata;
                txt2sec1_FamilyHistory.Text = (objdata == "1") ? strTrue : strFalse;
                txt2sec1_familyhistoryPoint.Text = objdata;

                currentPHM.tph_fhis_CHD_pt = Convert1.ToInt32(objdata);
              
                //if (Convert1.ToInt32(txt2sec1_SmokePoint.Text) >= 1)
                //{
                //    ChangeForeColor(txt2sec1_Smoke);
                //    ChangeForeColor(txt2sec1_SmokePoint);
                //}
                //HDL Point
                

                //txt2sec1_PreCardioCount.Text = "";
                
                txt2sec1_DrugforhighBlood.Text=(objqp.tqp_ill_cmed_hyper == true) ? strTrue : strFalse;
                txt2sec2_DrugforhighBlood.Text = txt2sec1_DrugforhighBlood.Text;

                //sec2
                txt2sec2_Smoke.Text = (objqp.tqp_his_smok=='S')?strTrue:strFalse;
            }

            int SysBP = 0;
            trn_basic_measure_dtl objbmdtl = (from t1 in dbc.trn_basic_measure_dtls
                                                where t1.trn_basic_measure_hdr.tpr_id == tpr_id
                                                select t1).FirstOrDefault();
            if (objbmdtl != null)
            {
                txt2sec1_SystolicBP.Text = objbmdtl.tbd_systolic;
                txt2sec1_DiastolicBP.Text = objbmdtl.tbd_diastolic;
                txt2sec2_SystolicBP.Text = txt2sec1_SystolicBP.Text;
                txt2sec2_DiastolicBP.Text = txt2sec1_DiastolicBP.Text;
                SysBP = Utility.GetInteger(objbmdtl.tbd_systolic);

                txt2sec1_HighBloodPoint.Text = ((Utility.GetInteger(objbmdtl.tbd_systolic) >= 140
                                                || Utility.GetInteger(objbmdtl.tbd_diastolic) >= 90)
                                                || (objqp != null && objqp.tqp_ill_cmed_hyper == true)) ? "1" : "0";

                if (Convert1.ToInt32(txt2sec1_HighBloodPoint.Text) >0)
                {
                    ChangeForeColor(txt2sec1_SystolicBP);
                    ChangeForeColor(txt2sec1_DiastolicBP);
                    ChangeForeColor(txt2sec1_HighBloodPoint);
                }
            }

            txt2sec1_AgePoint.Text = ((currentRegis.trn_patient.tpt_gender == 'M' && Utility.GetInteger(txt2sec1_Age.Text) > 45)
                                        || (currentRegis.trn_patient.tpt_gender == 'F' && Utility.GetInteger(txt2sec1_Age.Text) > 55)) ? "1" : "0";

            currentPHM.tph_age_by_gender_pt = ((currentRegis.trn_patient.tpt_gender == 'M' && Utility.GetInteger(txt2sec1_Age.Text) >= 45)
                                    || (currentRegis.trn_patient.tpt_gender == 'F' && Utility.GetInteger(txt2sec1_Age.Text) >= 55)) ? 1 : 0;

            var objlab = (from t1 in dbc.trn_patient_labs
                          where t1.tpl_lab_no == "C0150" && t1.tpl_hn_no == this.HNno && t1.tpl_en_no == (Program.CurrentRegis == null ? currentRegis.tpr_en_no : Program.CurrentRegis.tpr_en_no)
                          //&& ((Program.GetServerDateTime().Date - t1.tpl_lab_date.Value.Date).Days <= 365)
                          orderby t1.tpl_lab_date descending
                          select t1.tpl_lab_value).FirstOrDefault();
            if (objlab != null)
            {//HDL Cholesterol Sec1
                txt2sec1_HDLCholesterol.Text = objlab;
                int HDLvalue = Utility.GetInteger(txt2sec1_HDLCholesterol.Text.Trim());
                if (HDLvalue < 40)
                {
                    txt2sec1_HDLPoint.Text = "1";
                    //currentPHM.tph_hdl_pt = 1;
                }
                else if (40 <= HDLvalue && HDLvalue < 60)
                {
                    txt2sec1_HDLPoint.Text = "0";
                    //currentPHM.tph_hdl_pt = 0;
                }
                else if (HDLvalue >= 60)
                {
                    txt2sec1_HDLPoint.Text = "-1";
                    //currentPHM.tph_hdl_pt = -1;
                }
            }
            else
            {
                txt2sec1_HDLCholesterol.Text = "";
                txt2sec1_HDLPoint.Text = "0";
                //currentPHM.tph_hdl_pt = 0;
            }

            int v_pre_sum = Convert1.ToInt32(txt2sec1_SmokePoint.Text) + Convert1.ToInt32(txt2sec1_HighBloodPoint.Text) +
                    Convert1.ToInt32(txt2sec1_familyhistoryPoint.Text) + Convert1.ToInt32(txt2sec1_AgePoint.Text) + Convert1.ToInt32(txt2sec1_HDLPoint.Text);
            txt2sec1_PreCardioCount.Text = Convert1.ToString(v_pre_sum);

            //HDL Cholesterol Sec2
            int icount = 0;
            var objlab2 = Getlab("C0150", this.HNno);
            foreach (trn_patient_lab item in objlab2)
            {
                if (icount < 2)
                {
                    if (Convert.ToDateTime(item.tpl_lab_date).Date == dtCurrent.Date)
                    {
                        txt2sec2_HDLCholesterol.Text = item.tpl_lab_value.ToString();
                        txt2sec2_HDLCholesterolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        icount = icount + 1;
                    }
                    else if (Convert.ToDateTime(item.tpl_lab_date).Date != dtCurrent.Date)
                    {
                        txt2sec2_PreviousHDLCholesterol.Text = item.tpl_lab_value.ToString();
                        txt2sec2_PreviousHDLCholesterolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        icount = icount + 2;
                    }
                }
            }
            //Total Cholesterol Sec2
            var objtotalcholesterol = Getlab("C0130", this.HNno);
            int icounttotal = 0;
            foreach (trn_patient_lab item in objtotalcholesterol)
            {
                if (icounttotal < 2)
                {
                    if (Convert.ToDateTime(item.tpl_lab_date).Date == dtCurrent.Date)
                    {
                        txt2sec2_TotalCholesterol.Text = item.tpl_lab_value.ToString();
                        txt2sec2_TotalCholesterolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        icounttotal = icounttotal + 1;
                    }
                    else if (Convert.ToDateTime(item.tpl_lab_date).Date != dtCurrent.Date)
                    {
                        txt2sec2_PreTotalCholesterol.Text = item.tpl_lab_value.ToString();
                        txt2sec2_PreTotalcholestorolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        icounttotal = icounttotal + 2;
                    }
                }
            }

            //Risk Score
            double riskscore = 0;
            int HDL = 0;
            if (txt2sec1_HDLCholesterol.Text == "")
            {
                HDL = -9999;
            }
            else
            {
                HDL = Utility.GetInteger(txt2sec1_HDLCholesterol.Text.Trim());
            }
            string gendervalue = (currentRegis.trn_patient.tpt_gender==null)?"M":currentRegis.trn_patient.tpt_gender.ToString();
            var totalcholestoral = Utility.GetInteger(txt2sec2_TotalCholesterol.Text);
            var agevalue= Utility.GetInteger(txt2sec1_Age.Text);
            var objriskscore = (from t1 in dbc.mst_phm_cfg_dtls
                                where t1.mst_phm_cfg_hdr.mph_code == "FH01"
                                && t1.mpd_str_1 == gendervalue
                                && agevalue >= t1.mpd_min_num1
                                && agevalue <= t1.mpd_max_num1
                                select t1).FirstOrDefault();
            if (objriskscore != null)
            {
                currentPHM.tph_cardio_age_pt = Convert1.ToDouble(objriskscore.mpd_num_value);
                riskscore = Convert1.ToDouble(objriskscore.mpd_num_value);
            }
            var objriskscore2 = (from t1 in dbc.mst_phm_cfg_dtls
                                where t1.mst_phm_cfg_hdr.mph_code == "FH02"
                                && t1.mpd_str_1 == gendervalue
                                && agevalue >= t1.mpd_min_num1
                                && agevalue <= t1.mpd_max_num1
                                && totalcholestoral >= t1.mpd_min_num2
                                && totalcholestoral <= t1.mpd_max_num2
                                select t1).FirstOrDefault();
            if (objriskscore2 != null)
            {
                currentPHM.tph_cardio_totcholes_pt = Convert1.ToDouble(objriskscore2.mpd_num_value);
                riskscore += Convert1.ToDouble(objriskscore2.mpd_num_value);
            }
            if (objqp != null)
            {
                string issmoke = (objqp.tqp_his_smok == 'S') ? "S" : "N";
                var objriskscore3 = (from t1 in dbc.mst_phm_cfg_dtls
                                     where t1.mst_phm_cfg_hdr.mph_code == "FH03"
                                     && t1.mpd_str_1 == gendervalue
                                     && agevalue >= t1.mpd_min_num1
                                     && agevalue <= t1.mpd_max_num1
                                     && t1.mpd_str_2 == issmoke
                                     select t1).FirstOrDefault();
                if (objriskscore3 != null)
                {
                    currentPHM.tph_cardio_smoke_pt = Convert1.ToDouble(objriskscore3.mpd_num_value);
                    riskscore += Convert1.ToDouble(objriskscore3.mpd_num_value);
                }
            }
            var objriskscore4 = (from t1 in dbc.mst_phm_cfg_dtls
                                 where t1.mst_phm_cfg_hdr.mph_code == "FH04"
                                 && t1.mpd_str_1 == gendervalue
                                 && HDL>=t1.mpd_min_num1
                                 && HDL<=t1.mpd_max_num1 
                                 select t1).FirstOrDefault();
            if (objriskscore4 != null)
            {
                currentPHM.tph_cardio_choles_pt = Convert1.ToDouble(objriskscore4.mpd_num_value);
                riskscore += Convert1.ToDouble(objriskscore4.mpd_num_value);
            }
            if (objqp != null)
            {
                string istreat = (objqp.tqp_pill_adm == 'Y' || objqp.tqp_pill_sur == 'Y') ? "T" : "U";
                var objriskscore5 = (from t1 in dbc.mst_phm_cfg_dtls
                                     where t1.mst_phm_cfg_hdr.mph_code == "FH05"
                                     && t1.mpd_str_1 == gendervalue
                                     && t1.mpd_str_2 == istreat
                                     && SysBP >= t1.mpd_min_num1
                                     && SysBP <= t1.mpd_max_num1
                                     select t1).FirstOrDefault();
                if (objriskscore5 != null)
                {
                    currentPHM.tph_cardio_bp_pt = Convert1.ToDouble(objriskscore5.mpd_num_value);
                    riskscore += Convert1.ToDouble(objriskscore5.mpd_num_value);
                }
            }
            txt2sec2_TotalRiskScore.Text = riskscore.ToString();
            currentPHM.tph_total_risk_pt = riskscore;

            var objcompare=(from t1 in dbc.mst_phm_cfg_dtls 
                           where t1.mst_phm_cfg_hdr.mph_code=="FH06"
                           && t1.mpd_str_1==gendervalue
                           && riskscore >= t1.mpd_min_num1
                           && riskscore <= t1.mpd_max_num1
                           select t1).FirstOrDefault();
            if (objcompare != null)
            {
                currentPHM.tph_10y_risk_score = objcompare.mpd_str_value1;
                txt2sec2_10yearRiskScore.Text = objcompare.mpd_str_value1;
            }
        }
        private void CalTab2Sec3(int tpr_id)
        {
            //LDL
            var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();
            var objlab = Getlab("C0159", this.HNno);
            int icount = 0;
            foreach (trn_patient_lab item in objlab)
            {
                if (icount < 2)
                {
                    if (Convert.ToDateTime(item.tpl_lab_date).Date == dtCurrent.Date)
                    {
                        txt2sec3_LDL.Text = item.tpl_lab_value.ToString();
                        txt2sec3_LDLDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        icount = icount + 1;
                    }
                    else if (Convert.ToDateTime(item.tpl_lab_date).Date != dtCurrent.Date)
                    {
                        txt2sec3_PreLDL.Text = item.tpl_lab_value.ToString();
                        txt2sec3_PreLDLDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        icount = icount + 2;
                    }
                }
                //if (icount == 0)
                //{
                //    var obj = objlab.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                //    foreach (trn_patient_lab itemPrev in obj)
                //    {
                //        txt2sec3_LDL.Text = item.tpl_lab_value.ToString();
                //        txt2sec3_LDLDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                //    }
                //    icount = icount + 1;
                //}
                //else if (icount == 1)
                //{
                //    //txt2sec3_PreLDL.Text = item.tpl_lab_value.ToString();
                //    //txt2sec3_PreLDLDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");

                //    var obj = objlab.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                //    foreach (trn_patient_lab itemPrev in obj)
                //    {
                //        txt2sec3_PreLDL.Text = itemPrev.tpl_lab_value.ToString();
                //        txt2sec3_PreLDLDate.Text = itemPrev.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                //    }
                //    icount = icount + 1;
                //}
            }
            //** *** ***
            //Risk category
            var riskType = "";
             var objqp = (from t1 in dbc.trn_ques_patients where t1.tpr_id == tpr_id select t1).FirstOrDefault();
             trn_phm_hdr currentPHM = (trn_phm_hdr)PHMhdrbindingSource1.Current;
             int fhisCHDpt = Convert1.ToInt32(currentPHM.tph_fhis_CHD_pt);
             int totalRiskPt = Convert1.ToInt32(currentPHM.tph_total_risk_pt);
             //if (objqp !=null && objqp.tqp_ill_med_coro == true || fhisCHDpt == 1 || currentPHM.tph_pre_cardio_pt >= 2 || totalRiskPt >= 20)
             if ((objqp != null && 
                 (objqp.tqp_ill_med_coro == true || objqp.tqp_ill_med_cper == true || objqp.tqp_ill_med_abdd == true || objqp.tqp_ill_med_sist == true ||
                  objqp.tqp_ill_med_para == true || objqp.tqp_ill_med_stro == true || objqp.tqp_ill_med_diab == true)) || 
                 currentPHM.tph_pre_cardio_pt >= 2 || totalRiskPt > 20)
             {
                 riskType = "H";//"High Risk (H)";
             }
             else if (currentPHM.tph_pre_cardio_pt >= 2 && totalRiskPt >= 10 && totalRiskPt <= 20)
             {
                 riskType = "D";//"Moderate High Risk (D)";
             }
             else if (currentPHM.tph_pre_cardio_pt >= 2 && totalRiskPt < 10)
             {
                 riskType ="M";// "Moderate Risk(M)";
             }
             else if (currentPHM.tph_pre_cardio_pt == 0 || currentPHM.tph_pre_cardio_pt == 1)
             {
                 riskType = "L";//"Lower Risk (L)";
             }

                  var objdatatypeC = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'C').FirstOrDefault();
                  if (objdatatypeC == null)
                  {
                      var objreferclinic = (from t1 in dbc.mst_phm_cfg_dtls
                                            where t1.mst_phm_cfg_hdr.mph_code == "DB11"
                                            && t1.mpd_str_1 == riskType
                                            select t1).ToList();
                      if (objreferclinic.Count > 0)
                      {
                          txt2sec3_RiskCategory.Text = objreferclinic.FirstOrDefault().mpd_str_value1;
                          txt2sec3_Recommend.Text = objreferclinic.FirstOrDefault().mpd_str_value2;
                          CBsec3_RefertoClinic.Text = objreferclinic.FirstOrDefault().mpd_str_value3;
                          txt2sec3_Protocol.Text = objreferclinic.FirstOrDefault().mpd_str_value3;
                          txt2sec3_Recommendation.SelectedText = objreferclinic.FirstOrDefault().mpd_str_value4;
                      }
                  }
                  else
                  {//แสดงข้อมูลทีเคยบันทึก
                        txt2sec3_RiskCategory.Text =objdatatypeC.tpd_category ;
                        txt2sec3_Recommend.Text =objdatatypeC.tpd_clinic_recommend;
                        CBsec3_RefertoClinic.Text= objdatatypeC.tpd_refer_to_clinic ;
                        txt2sec3_Protocol.Text=  objdatatypeC.tpd_protocal ;
                        txt2sec3_StatusRefertoClinic.SelectedValue = objdatatypeC.tpd_status ;
                        txt2sec3_ReasonfornotReferRemark.Text = objdatatypeC.tpd_status_other ;
                        txt2sec3_Recommendation.Text = objdatatypeC.tpd_recomment ;
                        txt2sec3_ConcernPointForTLC.Text = objdatatypeC.tpd_concern ;
                        txt2sec3_Note.Text = objdatatypeC.tpd_note ;
                  }
             txt2sec3_Note.Text = "";
        }
        private void CalTab2Sec4(int tpr_id)
        {
            var objqp = (from t1 in dbc.trn_ques_patients where t1.tpr_id == tpr_id select t1).FirstOrDefault();
            trn_phm_hdr currentphm =(trn_phm_hdr) PHMhdrbindingSource1.Current;
            
            txt2sec4_Smoke.Text = txt2sec1_Smoke.Text;
            txt2sec4_SmokeRisk.Text=txt2sec1_Smoke.Text;
            if (objqp != null)
            {
                currentphm.tph_rk_smoking = (objqp.tqp_his_smok == 'S') ? true : false;//+
            }
            //txt2sec4_HighBloodPressure.Text = (txt2sec1_HighBloodPoint.Text== "1")?strTrue:strFalse;
            //if (Convert1.ToInt32(txt2sec1_SystolicBP.Text) >= 140 || Convert1.ToInt32(txt2sec1_DiastolicBP.Text) >= 90) {
            //    txt2sec4_HighBloodPressure.Text = strTrue;
            //}else{
            //    txt2sec4_HighBloodPressure.Text = strFalse;
            //}

            txt2sec4_HighBloodPressure.Text = (Convert1.ToInt32(txt2sec1_SystolicBP.Text) >= 140 || Convert1.ToInt32(txt2sec1_DiastolicBP.Text) >= 90) ? strTrue:strFalse;
            txt2sec4_HighBloodPressureRisk.Text=(txt2sec1_HighBloodPoint.Text == "1") ? strTrue : strFalse;
            currentphm.tph_high_blood_pt = Convert1.ToInt32(txt2sec1_HighBloodPoint.Text);
            currentphm.tph_rk_high_blood = (txt2sec1_HighBloodPoint.Text == "1") ? true : false;

            txt2sec4_Systolic.Text = txt2sec1_SystolicBP.Text;
            txt2sec4_Diastolic.Text= txt2sec1_DiastolicBP.Text;

            txt2sec4_BloodPerssureMedication.Text = txt2sec1_DrugforhighBlood.Text;

            if (txt2sec1_HDLCholesterol.Text == "")
            {
                txt2sec4_HDLcholesterol.Text = "";
                txt2sec4_HDLcholesterolRisk.Text = "";
            }
            else
            {
                txt2sec4_HDLcholesterol.Text = (txt2sec1_HDLPoint.Text == "1") ? strTrue : strFalse;
                txt2sec4_HDLcholesterolRisk.Text = (txt2sec4_HDLcholesterol.Text == strTrue) ? strTrue : strFalse;
            }
            //txt2sec1_HDLPoint       //txt2sec4_HDLcholesterol
            
            currentphm.tph_hdl_pt = Convert1.ToInt32(txt2sec4_HDLcholesterol.Text);
            currentphm.tph_rk_hdl = (txt2sec4_HDLcholesterol.Text == "1") ? true : false;
            
            txt2sec42_familyCHD.Text = txt2sec1_FamilyHistory.Text;
            txt2sec42_familyCHDRisk.Text = txt2sec42_familyCHD.Text;
            currentphm.tph_fhis_CHD_pt = Convert1.ToInt32(txt2sec1_familyhistoryPoint.Text);
            currentphm.tph_rk_CHD = (currentphm.tph_fhis_CHD_pt == 1) ? true : false;

            txt2sec42_Agemale45.Text = (txt2sec1_AgePoint.Text == "1") ? strTrue : strFalse;
            txt2sec42_Age45Risk.Text = (txt2sec42_Agemale45.Text == strTrue) ? strTrue : strFalse;
            currentphm.tph_age_by_gender_pt = Convert1.ToInt32(txt2sec42_Agemale45.Text);
            currentphm.tph_rk_age = (currentphm.tph_age_by_gender_pt == 1) ? true : false;

            txt2sec42_Gender.Text = txt2sec1_Gender.Text;
            txt2sec42_Age.Text = txt2sec1_Age.Text;

            txt2sec43_Coro.Text = txt10Coronary.Text;
            txt2sec43_Peripheral.Text = txt10Periperal.Text;
            txt2sec43_Abdominal.Text = txt10Abdominal.Text;
            txt2sec43_Transient.Text = txt10Transient.Text;
            txt2sec43_Diabetes.Text = txt10Diabetes.Text;

            txt2sec44_TotalCholesterol.Text = txt2sec2_TotalCholesterol.Text;
            txt2sec44_LDLChesterol.Text = txt2sec3_LDL.Text ;

        }
        private void CalTab3(int tpr_id)
        {
            try
            {
                trn_phm_hdr currentphm = (trn_phm_hdr)PHMhdrbindingSource1.Current;

                var objqp = (from t1 in dbc.trn_ques_patients where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                string Gender = currentRegis.trn_patient.tpt_gender.ToString();

                txt3sec1_BMIResule.Text = txtBMI.Text;
                double bmivalue = Convert1.ToDouble(txt3sec1_BMIResule.Text);
                string raceGroup = "";
                if (DDRaceGroup.SelectedValue != null)
                {
                    raceGroup = (DDRaceGroup.Text == "ASIAN") ? "A" : "N";
                }
                var objdata = GetphmCfgvalue("DB01", raceGroup).Where(x => x.mpd_min_num1 <= bmivalue && bmivalue <= x.mpd_max_num1).FirstOrDefault();
                if (objdata != null)
                {
                    //currentphm.tph_bmi_pt = Convert1.ToInt32(objdata.mpd_num_value);
                    txt3sec1_BMIPoint.Text = objdata.mpd_num_value.ToString();
                }
                else
                {
                    //currentphm.tph_bmi_pt = 0;
                    txt3sec1_BMIPoint.Text = "0";
                }

                if (DDRaceGroup.Text == "ASIAN")
                {
                    mst_race_grpsmst_race_grps.Text = "ASIAN";
                }
                else if (DDRaceGroup.Text == "")
                {
                    mst_race_grpsmst_race_grps.Text = "";
                }
                else
                {
                    mst_race_grpsmst_race_grps.Text = "NonASIAN";
                }
                //mst_race_grpsmst_race_grps.Text = (DDRaceGroup.Text == "ASIAN") ? "ASIAN" : "NonASIAN";

                txt3sec1_Waistsize.Text = txtWaistSize.Text;
                double waistvalue = Convert1.ToDouble(txt3sec1_Waistsize.Text);
                var objwaistvalue = GetphmCfgvalue("DB02", raceGroup).Where(x => x.mpd_str_2 == Gender && x.mpd_min_num1 <= waistvalue && waistvalue <= x.mpd_max_num1).FirstOrDefault();
                if (objwaistvalue != null)
                {
                    txt3sec1_WaistByGenderPoint.Text = objwaistvalue.mpd_num_value.ToString();
                    //currentphm.tph_waist_pt = Convert1.ToInt32(objwaistvalue.mpd_num_value);
                }
                else
                {
                    txt3sec1_WaistByGenderPoint.Text = "0";
                    //currentphm.tph_waist_pt = 0;
                }

                txt3sec1_Age.Text = txtAge.Text;
                double Agevalue = Convert1.ToDouble(txtAge.Text);
                if (objqp != null)
                {
                    txt3sec1_Exercise.Text = (objqp.tqp_his_exercise == 'O' || objqp.tqp_his_exercise == 'N') ? strTrue : strFalse;
                    string IsExercise = (objqp.tqp_his_exercise == 'O' || objqp.tqp_his_exercise == 'N') ? "Y" : "N";
                    var objExercisevalue = GetphmCfgvalue("DB06", raceGroup).Where(x => x.mpd_str_2 == IsExercise && x.mpd_min_num1 <= Agevalue && Agevalue < x.mpd_max_num1).FirstOrDefault();
                    if (objExercisevalue != null)
                    {
                        txt3sec1_ExercisePoint.Text = objExercisevalue.mpd_num_value.ToString();
                        //currentphm.tph_exercise_pt = Convert1.ToInt32(objExercisevalue.mpd_num_value);
                    }
                    else
                    {
                        txt3sec1_ExercisePoint.Text = "0";
                        //currentphm.tph_exercise_pt = 0;
                    }
                }

                txt3sec1_Gender.Text = txtGender.Text;
                var objGendervalue = GetphmCfgvalue("DB03", raceGroup).Where(x => x.mpd_str_2 == Gender).FirstOrDefault();
                if (objGendervalue != null)
                {
                    txt3sec1_MaleGenderPoint.Text = objGendervalue.mpd_num_value.ToString();
                    //currentphm.tph_gender_pt = Convert1.ToInt32(objGendervalue.mpd_num_value);
                }
                else
                {
                    //currentphm.tph_gender_pt = 0;
                    txt3sec1_MaleGenderPoint.Text = "0";
                }

                if (objqp != null)
                {
                    txt3sec1_OverWeightInfant.Text = (objqp.tqp_fwm_over_weight == 'Y') ? strTrue : strFalse;
                    string overweightvalue = (objqp.tqp_fwm_over_weight == 'Y') ? "Y" : "N";
                    var objOverWeightvalue = GetphmCfgvalue("DB07", raceGroup).Where(x => x.mpd_str_2 == overweightvalue).FirstOrDefault();
                    if (objOverWeightvalue != null)
                    {
                        txt3sec1_OverWeightInfantPoint.Text = objOverWeightvalue.mpd_num_value.ToString();
                        //currentphm.tph_overweight_pt = Convert1.ToInt32(objOverWeightvalue.mpd_num_value);
                    }
                    else
                    {
                        txt3sec1_OverWeightInfantPoint.Text = "0";
                        //currentphm.tph_overweight_pt = 0;
                    }
                }

                //txt3sec1_Age.Text = txtAge.Text;
                //double Agevalue = Convert1.ToDouble(txtAge.Text);
                var objAgevalue = GetphmCfgvalue("DB04", raceGroup).Where(x => x.mpd_min_num1 <= Agevalue && Agevalue <= x.mpd_max_num1).FirstOrDefault();
                if (objAgevalue != null)
                {
                    txt3sec1_AgePoint.Text = objAgevalue.mpd_num_value.ToString();
                    //currentphm.tph_age_pt = Convert1.ToInt32(objAgevalue.mpd_num_value);
                }
                else
                {
                    txt3sec1_AgePoint.Text = "0";
                    //currentphm.tph_age_pt = 0;
                }
                if (objqp != null)
                {
                    string Familyvalue = ((objqp.tqp_fhis_fdis_diab == true)
                            || (objqp.tqp_fhis_mdis_diab == true)
                            || (objqp.tqp_fhis_bdis_diab == true)) ? "1" : "0";
                    txt3sec1_FamilyDiabete.Text = (Familyvalue == "1") ? strTrue : strFalse;
                    var objFamilyvalue = GetphmCfgvalue("DB05", raceGroup).Where(x => x.mpd_str_2 == ((Familyvalue == "1") ? "Y" : "N")).FirstOrDefault();
                    if (objFamilyvalue != null)
                    {
                        txt3sec1_FamilyHistoryPoint.Text = objFamilyvalue.mpd_num_value.ToString();
                        //currentphm.tph_fam_his_pt = Convert1.ToInt32(objFamilyvalue.mpd_num_value);
                    }
                    else
                    {
                        txt3sec1_FamilyHistoryPoint.Text = "0";
                        //currentphm.tph_fam_his_pt = 0;
                    }
                }

                txt3sec1_systoricBP.Text = txt2sec1_SystolicBP.Text;
                int intsystorcBP = Convert1.ToInt32(txt2sec1_SystolicBP.Text);
                if (intsystorcBP >= 140)
                {
                    txt3sec1_HighBlood.Text = "2";
                }
                else
                {
                    txt3sec1_HighBlood.Text = "0";
                }

                var TotalPoint = Convert1.ToInt32(txt3sec1_BMIPoint.Text);
                TotalPoint += Convert1.ToInt32(txt3sec1_WaistByGenderPoint.Text);
                TotalPoint += Convert1.ToInt32(txt3sec1_MaleGenderPoint.Text);
                TotalPoint += Convert1.ToInt32(txt3sec1_AgePoint.Text);
                TotalPoint += Convert1.ToInt32(txt3sec1_FamilyHistoryPoint.Text);
                TotalPoint += Convert1.ToInt32(txt3sec1_ExercisePoint.Text);
                TotalPoint += Convert1.ToInt32(txt3sec1_OverWeightInfantPoint.Text);
                TotalPoint += Convert1.ToInt32(txt3sec1_HighBlood.Text);//เพิ่ม HighBlood Text
                currentphm.tph_diabetes_tot_pt = TotalPoint;

                var objTotalvalue = GetphmCfgvalue("DB08", raceGroup).Where(x => x.mpd_min_num1 <= TotalPoint && TotalPoint <= x.mpd_max_num1).FirstOrDefault();
                if (objTotalvalue != null)
                {
                    txt3sec1_RelativeRisk.Text = objTotalvalue.mpd_str_value1.ToString();
                    //currentphm.tph_relative_risk = objTotalvalue.mpd_str_value1.ToString();
                }
                else
                {
                    txt3sec1_RelativeRisk.Text = "";
                    //currentphm.tph_relative_risk = "";
                }
                // ******************  sec 2************************************
                var objfpg = GetLabNo("C0180", "I0001", this.HNno);
                int icount = 0;
                foreach (trn_patient_lab item in objfpg)
                {
                    if (icount == 0)
                    {
                        currentphm.tph_fpg = Convert1.ToDouble(item.tpl_lab_value);
                        currentphm.tph_fpg_date = item.tpl_lab_date;
                        icount = icount + 1;
                    }
                    else
                    {
                        currentphm.tph_prev_fpg = Convert1.ToDouble(item.tpl_lab_value);
                        currentphm.tph_prev_fpg_date = item.tpl_lab_date;

                      
                    }
                }
                var objhba = GetLabNo("N0510", "I0950", this.HNno);
                icount = 0;
                foreach (trn_patient_lab item in objfpg)
                {
                    if (icount == 0)
                    {
                        currentphm.tph_hba1c = Convert1.ToDouble(item.tpl_lab_value);
                        currentphm.tph_hba1c_date = item.tpl_lab_date;
                        icount = icount + 1;
                    }
                    else
                    {
                        currentphm.tph_prev_hba1c = Convert1.ToDouble(item.tpl_lab_value);
                        currentphm.tph_prev_hba1c_date = item.tpl_lab_date;
                    }
                }

                if (currentphm.tph_prev_fpg_date != null && Program.GetServerDateTime().Date <= currentphm.tph_prev_fpg_date.Value.AddDays(365))
                {
                    var objfpgdiagnosis = (from t1 in dbc.mst_phm_cfg_dtls
                                           where t1.mst_phm_cfg_hdr.mph_code == "DB09"
                                           && t1.mpd_min_num1 >= Convert1.ToDouble(currentphm.tph_fpg)
                                           && t1.mpd_max_num1 <= Convert1.ToDouble(currentphm.tph_fpg)
                                           select t1).FirstOrDefault();
                    if (objfpgdiagnosis != null)
                    {
                        currentphm.tph_fpg_diagnosis = objfpgdiagnosis.mpd_str_value1;
                    }
                    else
                    {
                        currentphm.tph_fpg_diagnosis = string.Empty;
                    }
                }
                else
                {
                    currentphm.tph_fpg_diagnosis = string.Empty;
                }
                if (currentphm.tph_prev_hba1c_date != null && Program.GetServerDateTime().Date <= currentphm.tph_prev_hba1c_date.Value.AddDays(365))
                {
                    var objhba1cdiagnosis = (from t1 in dbc.mst_phm_cfg_dtls
                                             where t1.mst_phm_cfg_hdr.mph_code == "DB10"
                                             && t1.mpd_min_num1 >= Convert1.ToDouble(currentphm.tph_hba1c)
                                             && t1.mpd_max_num1 <= Convert1.ToDouble(currentphm.tph_hba1c)
                                             select t1).FirstOrDefault();
                    if (objhba1cdiagnosis != null)
                    {
                        currentphm.tph_hba1c_diagnosis = objhba1cdiagnosis.mpd_str_value1;
                    }
                    else
                    {
                        currentphm.tph_hba1c_diagnosis = string.Empty;
                    }
                }
                else
                {
                    currentphm.tph_hba1c_diagnosis = string.Empty;
                }


                //*******************  sec 3  ***************************************************
                //Risk category
                var objlab2 = Getlab("C0180", "I0001", this.HNno);
                icount = 0;
                DateTime? vHbA1c_Curr;
                DateTime? vHbA1c_Prev;
                DateTime? vFPG_Curr;
                DateTime? vFPG_Prev;
                foreach (trn_patient_lab item in objlab2)
                {
                    if (icount < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == dtCurrent.Date)
                        {
                            txt3sec2_FPG.Text = item.tpl_lab_value.ToString();
                            txt3sec2_FPGDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            vFPG_Curr = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != dtCurrent.Date)
                        {
                            txt3sec2_PreviousFPG.Text = item.tpl_lab_value.ToString();
                            txt3sec2_PreviousFPGDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            vFPG_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 2;
                        }
                    }
                    //if (icount == 0)
                    //{
                    //    var obj = objlab2.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                    //    foreach (trn_patient_lab itemPrev in obj)
                    //    {
                    //        txt3sec2_FPG.Text = item.tpl_lab_value.ToString();
                    //        txt3sec2_FPGDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //        vFPG_Curr = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                    //    }
                    //    icount = icount + 1;
                    //}
                    //else if (icount == 1)
                    //{
                    //    //txt3sec2_PreviousFPG.Text = item.tpl_lab_value.ToString();
                    //    //txt3sec2_PreviousFPGDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //    //vFPG_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());

                    //    var obj = objlab2.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                    //    foreach (trn_patient_lab itemPrev in obj)
                    //    {
                    //        txt3sec2_PreviousFPG.Text = itemPrev.tpl_lab_value.ToString();
                    //        txt3sec2_PreviousFPGDate.Text = itemPrev.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //        vFPG_Prev = Convert.ToDateTime(itemPrev.tpl_lab_date.Value.ToShortDateString());
                    //    }
                    //    icount = icount + 1;
                    //}
                }
                var objlab3 = Getlab("N0510", "I0950", this.HNno);
                icount = 0;
                foreach (trn_patient_lab item in objlab3)
                {
                    if (icount < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == dtCurrent.Date)
                        {
                            txt3sec2_HBA1C.Text = item.tpl_lab_value.ToString();
                            txt3sec2_HbA1cDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            vHbA1c_Curr = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != dtCurrent.Date)
                        {
                            txt3sec2_PreHbA1c.Text = item.tpl_lab_value.ToString();
                            txt3sec2_PreHbA1cDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            vHbA1c_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 2;
                        }
                    }
                    //if (icount == 0)
                    //{
                    //    var obj = objlab3.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                    //    foreach (trn_patient_lab itemPrev in obj)
                    //    {
                    //        txt3sec2_HBA1C.Text = item.tpl_lab_value.ToString();
                    //        txt3sec2_HbA1cDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //        vHbA1c_Curr = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                    //    }
                    //    icount = icount + 1;
                    //}
                    //else if (icount == 1)
                    //{
                    //    //txt3sec2_PreHbA1c.Text = item.tpl_lab_value.ToString();
                    //    //txt3sec2_PreHbA1cDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //    //vHbA1c_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                    //    var obj = objlab3.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                    //    foreach (trn_patient_lab itemPrev in obj)
                    //    {
                    //        txt3sec2_PreHbA1c.Text = itemPrev.tpl_lab_value.ToString();
                    //        txt3sec2_PreHbA1cDate.Text = itemPrev.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //        vHbA1c_Prev = Convert.ToDateTime(itemPrev.tpl_lab_date.Value.ToShortDateString());
                    //    }
                    //    icount = icount + 1;
                    //}
                }
                var objlabfpg = GetPHMDiagnosis("DB09", txt3sec2_FPG.Text);
                txt3sec2_FPGdiagnosis.Text = objlabfpg;

                var objlabHba1c = GetPHMDiagnosis("DB10", txt3sec2_HBA1C.Text);
                txt3sec2_HbA1cDiagnosis.Text = objlabHba1c;

                int riskcategory = 0;
                //Compare Edit Sumit 17/01/2014
                double hba1cvalue = 0;
                double fpgvalue = 0;

                if (txt3sec2_HbA1cDate.Text != "" && txt3sec2_FPGDate.Text != "")
                {
                    hba1cvalue = Convert1.ToDouble(txt3sec2_HBA1C.Text);
                    fpgvalue = 0;
                }
                else if (txt3sec2_HbA1cDate.Text == "" && txt3sec2_FPGDate.Text != "")
                {
                    hba1cvalue = 0;
                    fpgvalue = Convert1.ToDouble(txt3sec2_FPG.Text);
                }
                else if (txt3sec2_HbA1cDate.Text != "" && txt3sec2_FPGDate.Text == "")
                {
                    hba1cvalue = Convert1.ToDouble(txt3sec2_HBA1C.Text);
                    fpgvalue = 0;
                }
                else
                {
                    if (txt3sec2_PreHbA1c.Text != "" && txt3sec2_PreviousFPG.Text != "")
                    {
                        DateTime dtnow = Program.GetServerDateTime();
                        var objlabValue = (from t1 in dbc.trn_patient_labs
                                           where (t1.tpl_lab_no == "C0180" || t1.tpl_lab_no == "I0001" || t1.tpl_lab_no == "N0510" || t1.tpl_lab_no == "I0950")
                                            && t1.tpl_lab_date != dtnow.Date
                                           orderby t1.tpl_lab_date descending
                                           select t1).FirstOrDefault();
                        if (objlabValue.tpl_lab_no == "C0180" || objlabValue.tpl_lab_no == "I0001")
                        {
                            hba1cvalue = 0;
                            fpgvalue = Convert1.ToDouble(txt3sec2_PreviousFPG.Text);
                        }
                        else
                        {
                            hba1cvalue = Convert1.ToDouble(txt3sec2_PreHbA1c.Text);
                            fpgvalue = 0; 
                        }
                    }
                    else if (txt3sec2_PreHbA1c.Text != "" && txt3sec2_PreviousFPG.Text == "")
                    {
                        hba1cvalue = Convert1.ToDouble(txt3sec2_PreHbA1c.Text);
                        fpgvalue = 0;
                    }
                    else if (txt3sec2_PreHbA1c.Text == "" && txt3sec2_PreviousFPG.Text != "")
                    {
                        hba1cvalue = 0;
                        fpgvalue = Convert1.ToDouble(txt3sec2_PreviousFPG.Text);
                    }
                    else
                    {
                        hba1cvalue = 0;
                        fpgvalue = 0;
                    }
                }


                 //hba1cvalue = Convert1.ToDouble(txt3sec2_HBA1C.Text);
                 //fpgvalue = Convert1.ToDouble(txt3sec2_FPG.Text);
                string isAsian = (DDRaceGroup.Text == "ASIAN") ? "A" : "N";
                string typerisk = "";
                var objcfgphm = (from t1 in dbc.mst_phm_cfg_dtls
                                 where t1.mph_id == 14
                                 && t1.mpd_str_1 == isAsian
                                 && hba1cvalue >= t1.mpd_min_num1
                                 && hba1cvalue <= t1.mpd_max_num1
                                 select t1).FirstOrDefault();
                if (objcfgphm != null)
                {
                    typerisk = objcfgphm.mpd_str_value2;
                }
                //if (hba1cvalue !=0 && hba1cvalue <= 6.4 && hba1cvalue >= 5.7)
                if (hba1cvalue != 0 && hba1cvalue >= 6.5)
                {
                    riskcategory = 1;
                }
                //else if (hba1cvalue != 0 &&  hba1cvalue >= 6.5)
                else if (hba1cvalue != 0 && hba1cvalue <= 6.4 && hba1cvalue >= 5.7)
                {
                    riskcategory = 2;
                }
                else if (hba1cvalue != 0 && typerisk == "H" && hba1cvalue < 5.7)
                {
                    riskcategory = 3;
                }
                else if (hba1cvalue != 0 && typerisk == "L" && hba1cvalue < 5.7)
                {
                    riskcategory = 4;
                }
                else if (fpgvalue != 0 && fpgvalue < 100)
                {
                    riskcategory = 5;
                }
                else if (fpgvalue != 0 && fpgvalue >= 100)
                {
                    riskcategory = 6;
                }
                //      D
                var objdatatypeD = currentphm.trn_phm_dtls.Where(x => x.tpd_type == 'D').FirstOrDefault();
                if (objdatatypeD != null)
                {
                    txt3sec3_RiskCategory.Text = objdatatypeD.tpd_category;

                    txt3sec3_Recommend.Text = objdatatypeD.tpd_clinic_recommend;

                    txt3sec3_ReferToClinic.Text = objdatatypeD.tpd_refer_to_clinic;
                    txt3sec3_Protocol.Text = objdatatypeD.tpd_protocal;
                    txt3sec3_ReasonFornotRefer.SelectedValue = objdatatypeD.tpd_status;
                    txt3sec3_ReasonfornotReferRemark.Text = objdatatypeD.tpd_status_other;
                    txt3sec3_Recommendation.Text = objdatatypeD.tpd_recomment;
                    txt3sec3_ConcernPointsForTLC.Text = objdatatypeD.tpd_concern;
                    txt3sec3_txtFollowupPoint.Text = objdatatypeD.tpd_note;
                }
                else
                {
                    var objcfgvalue = (from t1 in dbc.mst_phm_cfg_dtls
                                       where t1.mst_phm_cfg_hdr.mph_code == "DB12"
                                       && t1.mpd_num_1 == riskcategory
                                       select t1).FirstOrDefault();
                    if (objcfgvalue != null)
                    {
                        txt3sec3_RiskCategory.Text = objcfgvalue.mpd_str_value1;
                        txt3sec3_Recommend.Text = objcfgvalue.mpd_str_value2;
                        txt3sec3_Protocol.Text = objcfgvalue.mpd_str_value3;
                        txt3sec3_Recommendation.Text = objcfgvalue.mpd_str_value4;
                    }
                }


                var objlab = Getlab("C0159", this.HNno);
                icount = 0;
                foreach (trn_patient_lab item in objlab)
                {
                    if (icount < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == dtCurrent.Date)
                        {
                            txt3sec3_LDLCholesterol.Text = item.tpl_lab_value.ToString();
                            txt3sec3_LDLChDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            icount = icount + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != dtCurrent.Date)
                        {
                            txt3sec3_PreviousLDLCholesterol.Text = item.tpl_lab_value.ToString();
                            txt3sec3_PrevoiusLDLChoDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            vHbA1c_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 2;
                        }
                    }
                    //if (icount == 0)
                    //{
                    //    var obj = objlab.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                    //    foreach (trn_patient_lab itemPrev in obj)
                    //    {
                    //        txt3sec3_LDLCholesterol.Text = item.tpl_lab_value.ToString();
                    //        txt3sec3_LDLChDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //    }
                    //    icount = icount + 1;
                    //}
                    //else if (icount == 1)
                    //{
                    //    //txt3sec3_PreviousLDLCholesterol.Text = item.tpl_lab_value.ToString();
                    //    //txt3sec3_PrevoiusLDLChoDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");

                    //    var obj = objlab.Where(x => x.tpl_lab_date != dtCurrent).ToList();
                    //    foreach (trn_patient_lab itemPrev in obj)
                    //    {
                    //        txt3sec3_PreviousLDLCholesterol.Text = itemPrev.tpl_lab_value.ToString();
                    //        txt3sec3_PrevoiusLDLChoDate.Text = itemPrev.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    //        vHbA1c_Prev = Convert.ToDateTime(itemPrev.tpl_lab_date.Value.ToShortDateString());
                    //    }
                    //    icount = icount + 1;
                    //}
                }
                //********************************* sec  4  ************************
                txt3sec4_RaceGroup.Text = mst_race_grpsmst_race_grps.Text;

                //txt3sec4_BMI.Text = (currentphm.tph_bmi_pt > 0) ? strTrue : strFalse;
                txt3sec4_BMI.Text = (Convert1.ToInt32(txt3sec1_BMIPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_BMIRisk.Text = (txt3sec4_BMI.Text == strTrue) ? strTrue : strFalse;
                //txt3sec4_BMIRisk.Text = txt3sec4_BMI.Text;
                //txt3sec4_Waist.Text = (currentphm.tph_waist_pt > 0) ? strTrue : strFalse;
                txt3sec4_Waist.Text = (Convert1.ToInt32(txt3sec1_WaistByGenderPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_WaistRisk.Text = (txt3sec4_Waist.Text == strTrue) ? strTrue : strFalse;
                //txt3sec4_WaistRisk.Text = txt3sec4_Waist.Text;
                //txt3sec4_Exercise.Text = (currentphm.tph_exercise_pt > 0) ? strTrue : strFalse;
                txt3sec4_Exercise.Text = (Convert1.ToInt32(txt3sec1_ExercisePoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_ExerciseRisk.Text = (txt3sec4_Exercise.Text == strTrue) ? strTrue : strFalse;
                //txt3sec4_ExerciseRisk.Text = txt3sec4_Exercise.Text;

                txt3sec4_Gender.Text = (Convert1.ToInt32(txt3sec1_MaleGenderPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_GenderRisk.Text = (txt3sec4_Gender.Text == strTrue) ? strTrue : strFalse;

                //txt3sec4_Age.Text = (currentphm.tph_age_pt > 0) ? strTrue : strFalse;
                //txt3sec4_AgeRisk.Text = txt3sec4_Age.Text;

                txt3sec4_Age.Text = (Convert1.ToInt32(txt3sec1_AgePoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_AgeRisk.Text = (txt3sec4_Age.Text == strTrue) ? strTrue : strFalse;

                //txt3sec4_Gender.Text = (currentphm.tph_gender_pt > 0) ? strTrue : strFalse;
                //txt3sec4_GenderRisk.Text = txt3sec4_Gender.Text;
                //txt3sec4_FamilyHistory.Text = (currentphm.tph_fam_his_pt > 0) ? strTrue : strFalse;
                //txt3sec4_FamilyHistoryRisk.Text = txt3sec4_FamilyHistory.Text;

                txt3sec4_FamilyHistory.Text = (Convert1.ToInt32(txt3sec1_FamilyHistoryPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_FamilyHistoryRisk.Text = (txt3sec4_FamilyHistory.Text == strTrue) ? strTrue : strFalse;

                //txt3sec4_Womanweight.Text = (currentphm.tph_overweight_pt > 0) ? strTrue : strFalse;
                //txt3sec4_WomanweightRisk.Text = txt3sec4_Womanweight.Text;

                txt3sec4_Womanweight.Text = (Convert1.ToInt32(txt3sec1_OverWeightInfantPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_WomanweightRisk.Text = (txt3sec4_Womanweight.Text == strTrue) ? strTrue : strFalse;

                //txt3sec4_HbA1C.Text =Convert1.ToDouble( currentphm.tph_hba1c).ToString();
                //txt3sec4_FPG.Text = Convert1.ToDouble(currentphm.tph_fpg).ToString();
                if (objqp != null)
                {
                    txt3sec4_FatherDiabetes.Text = (objqp.tqp_fhis_fdis_diab == true || objqp.tqp_fhis_mdis_diab == true) ? strTrue : strFalse;
                    txt3sec4_SiblingDiaetes.Text = (objqp.tqp_fhis_bdis_diab == true) ? strTrue : strFalse; 
                    //txt3sec4_Diabetes.Text = (objqp.tqp_ill_med_diab == true) ? strTrue : strFalse;
                }
                button6_Click(null, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private void CalTab4(int tpr_id)
        {
            trn_phm_hdr currentPHM = (trn_phm_hdr)PHMhdrbindingSource1.Current;
            //Diabetes Save PHM dtl Type="M"
            var objdatatypeM = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'M').FirstOrDefault();
            if (objdatatypeM != null)
            {


                txt4sec1_BiradsCategory.Text = objdatatypeM.tpd_category;
                txt4sec1_Protocal.Text = objdatatypeM.tpd_protocal;
                txt4sec1_RefertoClinic.Text=  objdatatypeM.tpd_refer_to_clinic ;
                txt4sec1_StatusReferClinic.SelectedValue=  objdatatypeM.tpd_status .ToString();
                txt4sec1_ReasonfornotReferRemark.Text =objdatatypeM.tpd_status_other ;
                txt4sec1_Diagnosis.Text = objdatatypeM.tpd_diagnosis;
                txt4sec1_Remark.Text= objdatatypeM.tpd_note ;

                int idcategory = Convert1.ToInt32(objdatatypeM.tpd_category);
                if (idcategory < 3)
                {
                    txt4sec1_Recommend.Text = "NONE";
                    txt4sec1_Protocal.Text = "Care plan";
                }
                else if (idcategory < 7)
                {
                    txt4sec1_Recommend.Text = "Breast Clinic";
                }
                if (idcategory > 3)
                {
                    txt4sec1_Protocal.Text = "Consult to";
                }

                //Diagnosis && Remark
                int tpt_id = 0;
                if (SetTprID > 0) { tpt_id = (from t1 in dbc.trn_patient_regis where t1.tpr_id == SetTprID select t1.tpt_id).FirstOrDefault(); }

                double categoryid=Convert1.ToInt32(objdatatypeM.tpd_category);
                var currentpatient =(from t1 in dbc.trn_patients 
                                  where t1.tpt_id== (Program.CurrentRegis == null ? tpt_id : Program.CurrentRegis.tpt_id)
                                  select t1).FirstOrDefault();
                string strNation = "T";
                if (currentpatient != null && currentpatient.tpt_nation_code != null)
                {
                    strNation = (currentpatient.tpt_nation_code == "TH") ? "T" : "E";
                }
                else
                {
                    strNation = "E";
                }

                if (txt4sec1_Diagnosis.Text == "" || txt4sec1_Remark.Text == "")
                {
                    var objhpmcfghdr = (from t1 in dbc.mst_phm_cfg_dtls
                                        where t1.mst_phm_cfg_hdr.mph_code == "DM01"
                                        && t1.mpd_str_1 == strNation
                                        && t1.mpd_num_1 == categoryid
                                        select t1).FirstOrDefault();
                    if (objhpmcfghdr != null)
                    {
                        txt4sec1_Diagnosis.Text = objhpmcfghdr.mpd_str_value1;
                        txt4sec1_Remark.Text = objhpmcfghdr.mpd_str_value3;
                    }
                }
            }
           
        }
        private void CalTab5(int tpr_id)
        {
            trn_phm_hdr currentPHM = (trn_phm_hdr)PHMhdrbindingSource1.Current;
            //AFP
            var objdatatypeA = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'A').FirstOrDefault();
            if (objdatatypeA != null)
            {

                txt5sec1_tumorMarkevafp.Text = objdatatypeA.tpd_category ;
                txt5sec1_Recommend.Text = objdatatypeA.tpd_clinic_recommend ;
                txt5sec1_RefertoClinic.Text = objdatatypeA.tpd_refer_to_clinic ;
                txt5sec1_Protocal.Text = objdatatypeA.tpd_protocal ;
                txt5sec1_StatusRefertoClinic.SelectedValue=  objdatatypeA.tpd_status;
                txt5sec1_ReasonfornotReferRemark.Text= objdatatypeA.tpd_status_other ;
                txt5sec1_Remark.Text  =objdatatypeA.tpd_note ;
            }
            else
            {
                var objAFP = (from t1 in dbc.trn_patient_labs
                              where (t1.tpl_lab_no == "N0380" || t1.tpl_lab_no == "N7006")
                              && t1.tpl_hn_no == HNno
                              orderby t1.tpl_lab_date descending
                              select new { t1.tpl_lab_value, t1.tpl_lab_date }).FirstOrDefault();
                if (objAFP != null)
                {
                    txt5sec1_tumorMarkevafp.Text = objAFP.tpl_lab_value;
                    //txt5sec1_tumorMarkevafpDate.Text = Convert.ToDateTime(objAFP.tpl_lab_date).ToShortDateString();
                    txt5sec1_tumorMarkevafpDate.Text = objAFP.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    if (Convert1.ToInt32(txt5sec1_tumorMarkevafp.Text) >= 5.4)
                    {
                        txt5sec1_Protocal.Text = "ส่งพบผู้เชี่ยวชาญด้านมะเร็ง";
                    }
                    else
                    {
                        txt5sec1_Protocal.Text = "ให้คำแนะนำทั่วไป";
                    }
                }
            }

            //CEA
            var objdatatypeE = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'E').FirstOrDefault();
            if (objdatatypeE != null)
            {
                txt5sec2_tumormarkevCFA.Text = objdatatypeE.tpd_category ;
                txt5sec2_Recommend.Text = objdatatypeE.tpd_clinic_recommend ;
                txt5sec2_ReferToClinic.Text = objdatatypeE.tpd_refer_to_clinic ;
                txt5sec2_Protocal.Text = objdatatypeE.tpd_protocal ;
                txt5sec2_StatusRefertoClinic.SelectedValue = objdatatypeE.tpd_status;
                txt5sec2_ReasonfornotReferRemark.Text = objdatatypeE.tpd_status_other ;
                txt5sec2_Remark.Text = objdatatypeE.tpd_note ;
            }
            else
            {
                var objCEA = (from t1 in dbc.trn_patient_labs
                              where (t1.tpl_lab_no == "N0390" || t1.tpl_lab_no == "N7007")
                              && t1.tpl_hn_no == HNno
                              orderby t1.tpl_lab_date descending
                              select new { t1.tpl_lab_value, t1.tpl_lab_date }).FirstOrDefault();
                if (objCEA != null)
                {
                    txt5sec2_tumormarkevCFA.Text = objCEA.tpl_lab_value;
                    //txt5sec2_tumormarkevCFADate.Text = Convert.ToDateTime(objCEA.tpl_lab_date).ToShortDateString();
                    txt5sec2_tumormarkevCFADate.Text = objCEA.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    if (Convert1.ToInt32(txt5sec2_tumormarkevCFA.Text) >= 2.5)
                    {
                        txt5sec2_Protocal.Text = "ส่งพบผู้เชี่ยวชาญด้านมะเร็ง";
                    }
                    else
                    {
                        txt5sec2_Protocal.Text = "ให้คำแนะนำทั่วไป";
                    }
                }
            }
            //PSA
            //PSA Type="P"
            var objdatatypeP = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'P').FirstOrDefault();
            if (objdatatypeP != null)
            {
                txt5sec3_TumorMarkevPSA.Text=objdatatypeP.tpd_category ;
                txt5sec3_Recommend.Text= objdatatypeP.tpd_clinic_recommend ;
                txt5sec3_ReferToClinic.Text= objdatatypeP.tpd_refer_to_clinic ;
                txt5sec3_Protocal.Text= objdatatypeP.tpd_protocal ;
                txt5sec3_StatusRefertoClinic.SelectedValue =objdatatypeP.tpd_status;
                txt5sec3_ReasonfornotReferRemark.Text= objdatatypeP.tpd_status_other ;
                txt5sec3_Remark.Text= objdatatypeP.tpd_note ;
            }
            else
            {
                var objPSA = (from t1 in dbc.trn_patient_labs
                              where (t1.tpl_lab_no == "N0050")
                              && t1.tpl_hn_no == HNno
                              orderby t1.tpl_lab_date descending
                              select new { t1.tpl_lab_value, t1.tpl_lab_date }).FirstOrDefault();
                if (objPSA != null)
                {
                    txt5sec3_TumorMarkevPSA.Text = objPSA.tpl_lab_value;
                    //txt5sec3_TumorMarkevPSADate.Text = Convert.ToDateTime(objPSA.tpl_lab_date).ToShortDateString();
                    txt5sec3_TumorMarkevPSADate.Text = objPSA.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                    if (Convert1.ToInt32(txt5sec3_TumorMarkevPSA.Text) >= 4)
                    {
                        txt5sec3_Protocal.Text = "ส่งพบผู้เชี่ยวชาญด้านมะเร็ง";
                    }
                    else
                    {
                        txt5sec3_Protocal.Text = "ให้คำแนะนำทั่วไป";
                    }
                }
            }
        }
        private void CalTab6()
        {
            txt6sec1_RiskCategory.Text = txt2sec3_RiskCategory.Text;
            txt6sec1_Recommend.Text = txt2sec3_Recommend.Text;
            txt6sec1_RefertoClinic.Text = CBsec3_RefertoClinic.Text;
            txt6sec1_Protocol.Text = txt2sec3_Protocol.Text;
            txt6sec1_StatusRefertoClinic.Text = txt2sec3_StatusRefertoClinic.Text;
            txt6sec1_ReasonfornotReferRemark.Text = txt2sec3_ReasonfornotReferRemark.Text;
           txt6sec1_Recommendation.Text=txt2sec3_Recommendation.Text;
           txt6sec1_ConcernPointForTLC.Text = txt2sec3_ConcernPointForTLC.Text;
           txt6sec1_Note.Text = txt2sec3_Note.Text;

           txt6sec2_RiskCategory.Text = txt3sec3_RiskCategory.Text;
           txt6sec2_Recommend.Text = txt3sec3_Recommend.Text;
           txt6sec2_ReferToClinic.Text = txt3sec3_ReferToClinic.Text;
           txt6sec2_Protocol.Text = txt3sec3_Protocol.Text;
           txt6sec2_ReasonFornotRefer.Text = txt3sec3_ReasonFornotRefer.Text;
           txt6sec2_ReasonfornotReferRemark.Text = txt3sec3_ReasonfornotReferRemark.Text;
           txt6sec2_Recommendation.Text = txt3sec3_Recommendation.Text;
           txt6sec2_ConcernPointsForTLC.Text = txt3sec3_ConcernPointsForTLC.Text;
           txt6sec2_txtFollowupPoint.Text = txt3sec3_txtFollowupPoint.Text;

           txt6sec3_BiradsCategory.Text = txt4sec1_BiradsCategory.Text;
           txt6sec3_Diagnosis.Text = txt4sec1_Diagnosis.Text;
           txt6sec3_Recommend.Text = txt4sec1_Recommend.Text;
           txt6sec3_RefertoClinic.Text = txt4sec1_RefertoClinic.Text;
           txt6sec3_Protocal.Text = txt4sec1_Protocal.Text;
           txt6sec3_StatusReferClinic.Text = txt4sec1_StatusReferClinic.Text;
           txt6sec3_ReasonfornotReferRemark.Text = txt4sec1_ReasonfornotReferRemark.Text;
           txt6sec3_Remark.Text = txt4sec1_Remark.Text;

           txt6sec4_tumorMarkevafp.Text = txt5sec1_tumorMarkevafp.Text;
           txt6sec4_Recommend.Text = txt5sec1_Recommend.Text;
           txt6sec4_RefertoClinic.Text = txt5sec1_RefertoClinic.Text;
           txt6sec4_Protocal.Text = txt5sec1_Protocal.Text;
           txt6sec4_StatusRefertoClinic.Text = txt5sec1_StatusRefertoClinic.Text;
           txt6sec4_ReasonfornotReferRemark.Text = txt5sec1_ReasonfornotReferRemark.Text;
           txt6sec4_Remark.Text = txt5sec1_Remark.Text;

           txt6sec5_tumormarkevCFA.Text = txt5sec2_tumormarkevCFA.Text;
           txt6sec5_Recommend.Text = txt5sec2_Recommend.Text;
           txt6sec5_ReferToClinic.Text = txt5sec2_ReferToClinic.Text;
           txt6sec5_Protocal.Text = txt5sec2_Protocal.Text;
           txt6sec5_StatusRefertoClinic.Text = txt5sec2_StatusRefertoClinic.Text;
           txt6sec5_ReasonfornotReferRemark.Text=txt5sec2_ReasonfornotReferRemark.Text;
           txt6sec5_Remark.Text = txt5sec2_Remark.Text;

           txt6sec6_TumorMarkevPSA.Text = txt5sec3_TumorMarkevPSA.Text;
           txt6sec6_Recommend.Text = txt5sec3_Recommend.Text ;
           txt6sec6_ReferToClinic.Text = txt5sec3_ReferToClinic.Text;
           txt6sec6_Protocal.Text = txt5sec3_Protocal.Text;
           txt6sec6_StatusRefertoClinic.Text = txt5sec3_StatusRefertoClinic.Text;
           txt6sec6_ReasonfornotReferRemark.Text = txt5sec3_ReasonfornotReferRemark.Text;
           txt6sec6_Remark.Text = txt5sec3_Remark.Text;
    
        }

        private void ClearFrm()
        {
            Program.CurrentRegis = null;
            Program.CurrentPatient_queue = null;

            txtAge.Text = "";
            dbc.Dispose();
            dbc = new InhCheckupDataContext();
            List<PHMList> datax = new List<PHMList>();

            GridPHMList.DataSource = datax.ToList();
            Clearcontrol(panel3PersonalHealthManagment);
            Clearcontrol(panel9Cardiovascular);
            Clearcontrol(panel4Diabetes);
            Clearcontrol(tabPage4_Mammogram);
            Clearcontrol(tabPage5_TumorMarker);
            Clearcontrol(panel5RiskFactorSummary);

            LoadUI();
        }
        private void Clearcontrol(Control ControlP)
        {
            foreach (Control item in ControlP.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                else if(item is GroupBox)
                {
                    Clearcontrol(item);
                }
                else if (item is Panel)
                {
                    Clearcontrol(item);
                }
                
            }
        }
        private List<mst_phm_cfg_dtl> GetphmCfgvalue(string mphCode,string RaceGroup)
        {
            List<mst_phm_cfg_dtl> objvalue = (from t1 in dbc.mst_phm_cfg_dtls
                                              where t1.mst_phm_cfg_hdr.mph_code == mphCode
                                                    && t1.mpd_str_1 == RaceGroup
                            select t1).ToList();
            if (objvalue != null)
            {
                return objvalue;
            }
            else
            {
                return null;
            }
        }
        private List<trn_patient_lab> GetLabNo(string Labno1, string Labno2,string HNno)
        {
            List<trn_patient_lab> objitem = (from t1 in dbc.trn_patient_labs
                                             where (t1.tpl_lab_no == Labno1 || t1.tpl_lab_no == Labno2)
                                                     && t1.tpl_hn_no == HNno
                                             orderby t1.tpl_lab_date descending
                                             select t1).Take(2).ToList();
            return objitem;
        }
        private List<trn_patient_lab> Getlab(string Labno,string HNno)
        {
            List<trn_patient_lab> objitem = (from t1 in dbc.trn_patient_labs
                                where t1.tpl_lab_no == Labno
                                 && t1.tpl_hn_no == HNno
                                 orderby t1.tpl_lab_date descending
                             select t1).ToList();
            //&& ((Program.GetServerDateTime().Date - t1.tpl_lab_date.Value.Date).Days <= 365)

            return objitem;
        }

        private List<trn_patient_lab> GetlabPrev(string Labno, string HNno)
        {
            DateTime dtCurrent = Program.GetServerDateTime();
            List<trn_patient_lab> objitem = (from t1 in dbc.trn_patient_labs
                                             where t1.tpl_lab_no == Labno
                                              && t1.tpl_hn_no == HNno
                                              && t1.tpl_lab_date == dtCurrent
                                             orderby t1.tpl_lab_date descending
                                             select t1).Take(2).ToList();
            return objitem;
        }

        private List<trn_patient_lab> Getlab(string Labno1,string Labno2, string HNno)
        {
            List<trn_patient_lab> objitem = (from t1 in dbc.trn_patient_labs
                                             where (t1.tpl_lab_no == Labno1 || t1.tpl_lab_no==Labno2)
                                              && t1.tpl_hn_no == HNno
                                             orderby t1.tpl_lab_date descending
                                             select t1).ToList();
            //&& ((Program.GetServerDateTime().Date - t1.tpl_lab_date.Value.Date).Days <= 365)
            return objitem;
        }
        private string GetPHMDiagnosis(string Code, string LabValue)
        {
            if (LabValue == "") { return ""; }
            float valuenum = Convert1.ToFloat(LabValue);
            var objdata = (from t1 in dbc.mst_phm_cfg_dtls
                           where t1.mst_phm_cfg_hdr.mph_code == Code
                           && t1.mpd_min_num1 <= valuenum
                           && t1.mpd_max_num1 >= valuenum
                           select t1).FirstOrDefault();
            if (objdata != null)
                return objdata.mpd_str_value1;
            else
                return "";
        }

        private Boolean isFirstload = true;
        private void loadPHMList(string searchdata)
        {
            if (isFirstload)
            {
                List<mst_hpc_site> objsite = (from t1 in dbc.mst_hpc_sites
                                                where t1.mhs_status == 'A' && t1.mhs_type == 'P'
                                                select t1).ToList();
                mst_hpc_site newselect = new mst_hpc_site();
                newselect.mhs_id = 0;
                newselect.mhs_ename = "Select All";
                objsite.Add(newselect);

                DDSite.DataSource = objsite.OrderBy(x => x.mhs_id).ToList();
                DDSite.DisplayMember = "mhs_ename";
                DDSite.ValueMember = "mhs_id";
                DDSite.SelectedValue = Program.CurrentSite.mhs_id;
                isFirstload = false;
            }


            var objPHMList = (from t1 in dbc.trn_patient_queues
                              where t1.mrm_id == Program.CurrentRoom.mrm_id
                              && t1.trn_patient_regi.tpr_arrive_date.Value.Date == Program.GetServerDateTime().Date
                              select new PHMList
                              {
                                  QueueNo=t1.trn_patient_regi.tpr_queue_no,
                                  HnNo=t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                  ArriveDate=t1.trn_patient_regi.tpr_arrive_date,
                                  FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                  mhc_Name = t1.trn_patient_regi.tpr_mhc_ename,
                                  SiteId=t1.mst_room_hdr.mhs_id,
                                  Status = Getstatus(t1.tps_status),
                                  DoctorName = t1.trn_patient_regi.tpr_pe_doc_name
                              }).ToList();

            if (Convert.ToInt32(DDSite.SelectedValue) > 0)
            {
               objPHMList= objPHMList.Where(x => x.SiteId == Convert.ToInt32(DDSite.SelectedValue)).ToList();
            }
            if (searchdata != "")
            {
                GridPHMList.DataSource = new SortableBindingList<PHMList>(objPHMList.Where(x =>(x.QueueNo!=null && x.QueueNo.ToLower().Contains(searchdata))
                                                                                            || (x.HnNo != null && x.HnNo.ToLower().Contains(searchdata))
                                                                                            || (x.mhc_Name != null && x.mhc_Name.ToLower().Contains(searchdata))
                                                                                            || (x.FullName != null && x.FullName.ToLower().Contains(searchdata))
                                                                                            || (x.Status != null && x.Status.ToLower().Contains(searchdata))).ToList());
            }
            else
            {
                GridPHMList.DataSource =  new SortableBindingList<PHMList>(objPHMList);
            }
            GridPHMList.Columns["ColSiteId"].Visible = false;
        }
        private string Getstatus(string datastatus)
        {
            string strStatus = "";
            switch (datastatus)
            {
                case "NS":
                    strStatus = "ยังไม่ตรวจ";
                    break;
                case "WK":
                    strStatus = "กำลังตรวจ";
                    break;
                case "ED":
                    strStatus = "ตรวจเสร็จแล้ว";
                    break;
                default:
                    strStatus = "";
                    break;
            }
            return strStatus;
        }
        private void GridPHMList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridPHMList.Rows.Count; i++)
            {
                GridPHMList["colNo", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadPHMList(txtSearch.Text.Trim());
        }

      

        private void btnCallQueue_Click(object sender, EventArgs e)
        {
            btnCallQueue.Enabled = false;
            lbAlertMsg.Text = "";
            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            try
            {
                //noina
                StatusTransaction callQ = CallQueue.P_CallQueueWaitReady();
                //end noina

                if (callQ == StatusTransaction.Error)
                {
                    lbAlertMsg.Text = "กรุณากด Call Queue อีกครั้ง";
                    btnCallQueue.Enabled = true;
                }
                else
                {
                    if (Program.CurrentRegis != null)
                    {
                        LoadUI();
                        clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                        try
                        {
                            LoadData(Program.CurrentRegis.tpr_id);

                            var objRaceGroup = (from t1 in dbc.mst_race_grps
                                                orderby t1.mag_ename
                                                select new DropdownData
                                                {
                                                    Code = t1.mag_id,
                                                    Name = t1.mag_ename
                                                }).ToList();
                            DropdownData newselect = new DropdownData();
                            newselect.Name = "";
                            objRaceGroup.Insert(0, newselect);
                            DDRaceGroup.ValueMember = "Code";
                            DDRaceGroup.DisplayMember = "Name";
                            DDRaceGroup.DataSource = objRaceGroup;
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError(this.Name, "btnCallQueue_Click", ex, false);
                        }
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = "No patient on queue!";
                        btnCallQueue.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnCallQueue_Click", ex, false);
            }
            finally
            {
                frmbg.Close();
            }
        }
        private void btnHold_Click(object sender, EventArgs e)
        {
            btnHold.Enabled = false;

            string QueueNo = Program.CurrentRegis.tpr_queue_no;
            int holdTime = PrePareData.StaticDataCls.HoldTime.Where(x => x.mhs_id == Program.CurrentSite.mhs_id).Select(x => x.holdTime).FirstOrDefault();
            QueueClass.TransactionQueueCls.PatientHoldQueue hold = new QueueClass.TransactionQueueCls().HoldQueue(Program.CurrentPatient_queue.tps_id, holdTime, Program.CurrentUser.mut_username);
            if (hold.Status == QueueClass.TransactionQueueCls.PatientHoldQueue.StatusHoldQueue.Error)
            {
                lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
                btnHold.Enabled = true;
            }
            else
            {
                StatusWaitCallQueue();
                lbAlertMsg.Text = string.Format(Program.MsgHold, hold.QueueNo);
            }
            //StatusTransaction result = CallQueue.P_CallHold();
            //if (result == StatusTransaction.True)
            //{
            //    // morn clear Unit Display
            //    new ClsTCPClient().sendClearUnitDisplay();
            //    // morn clear Unit Display

            //    Program.CurrentPatient_queue = null;
            //    Program.CurrentRegis = null;
            //    StatusWaitCallQueue();
            //    lbAlertMsg.Text = string.Format(Program.MsgHold, QueueNo);
            //}
            //else if (result == StatusTransaction.Error)
            //{
            //    btnHold.Enabled = true;
            //    lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
            //}
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            try
            {
                int? tpr_id = Program.CurrentRegis != null ? (int?)Program.CurrentRegis.tpr_id : null;
                int? mvt_id = Program.CurrentPatient_queue != null ? (int?)Program.CurrentPatient_queue.mvt_id : null;
                frmCancelQueue frmCancelQueue = new frmCancelQueue(Program.CurrentRegis.tpr_id, mvt_id);
                if (frmCancelQueue.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    new ClsTCPClient().sendClearUnitDisplay();
                    //ClearFrm();
                    StatusWaitCallQueue();
                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    lbAlertMsg.Text = func.getStringGotoNextRoom((int)tpr_id);
                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                    return;
                }
            }
            catch
            {
                lbAlertMsg.Text = "กรุณากด Cancel อีกครั้ง";
            }
            btnCancel.Enabled = true;
        }
        
        private bool Save(char Type)
        {
            try
            {
                //Update Regis Status
                var objRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == (Program.CurrentRegis == null ? SetTprID : Program.CurrentRegis.tpr_id) select t1).FirstOrDefault();
                if (objRegis != null)
                {
                    objRegis.tpr_status = "WB";
                }

                DateTime dtnow = Program.GetServerDateTime();
                trn_phm_hdr currentPHM = (trn_phm_hdr)PHMhdrbindingSource1.Current;

                currentPHM.trn_patient_regi = (from t1 in dbc.trn_patient_regis where t1.tpr_id == (Program.CurrentRegis == null ? SetTprID : Program.CurrentRegis.tpr_id) select t1).FirstOrDefault();
                currentPHM.tph_type = Type;
                if (currentPHM.tpr_id == 0)
                {
                    currentPHM.tpr_id = (Program.CurrentRegis == null ? SetTprID : Program.CurrentRegis.tpr_id);
                }
                if (currentPHM.tph_create_by == null)
                {
                    currentPHM.tph_create_by = Program.CurrentUser.mut_username;
                    currentPHM.tph_create_date = dtnow;
                }
                currentPHM.tph_race_grp = DDRaceGroup.Text;
                currentPHM.tph_race = DDRace.Text;

                currentPHM.tph_update_by = Program.CurrentUser.mut_username;
                currentPHM.tph_update_date = dtnow;
                currentPHM.tph_high_blood_pt = Convert1.ToInt32(txt2sec1_HighBloodPoint.Text);

                //Sumit 05/02/2014
                currentPHM.tph_smok_pt = Convert1.ToInt32(txt2sec1_SmokePoint.Text);
                currentPHM.tph_fhis_CHD_pt = Convert1.ToInt32(txt2sec1_familyhistoryPoint.Text);
                currentPHM.tph_age_by_gender_pt = Convert1.ToInt32(txt2sec1_AgePoint.Text);
                currentPHM.tph_hdl_pt = Convert1.ToInt32(txt2sec1_HDLPoint.Text);

                currentPHM.tph_pre_cardio_pt = Convert1.ToInt32(txt2sec1_PreCardioCount.Text);

                currentPHM.tph_bmi_pt = Convert1.ToInt32(txt3sec1_BMIPoint.Text);
                currentPHM.tph_waist_pt = Convert1.ToInt32(txt3sec1_WaistByGenderPoint.Text);
                currentPHM.tph_exercise_pt = Convert1.ToInt32(txt3sec1_ExercisePoint.Text);
                currentPHM.tph_gender_pt = Convert1.ToInt32(txt3sec1_MaleGenderPoint.Text);
                currentPHM.tph_overweight_pt = Convert1.ToInt32(txt3sec1_OverWeightInfantPoint.Text);
                currentPHM.tph_age_pt = Convert1.ToInt32(txt3sec1_AgePoint.Text);
                currentPHM.tph_fam_his_pt = Convert1.ToInt32(txt3sec1_FamilyHistoryPoint.Text);

                currentPHM.tph_relative_risk = txt3sec1_RelativeRisk.Text;
                currentPHM.tph_risk_score = Convert1.ToInt32(txt3sec1_TotalPoint.Text);

                currentPHM.tph_rk_bmi = (txt3sec4_BMIRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_waist = (txt3sec4_WaistRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_exercise = (txt3sec4_ExerciseRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_diab_age = (txt3sec4_AgeRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_gender = (txt3sec4_GenderRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_diab_family = (txt3sec4_FamilyHistoryRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_infant_more = (txt3sec4_WomanweightRisk.Text == strTrue) ? true : false;


                //Cardiovascura
                if (txt2sec2_HDLCholesterol.Text != "")
                {
                    currentPHM.tph_hdl_choles = Convert1.ToFloat(txt2sec2_HDLCholesterol.Text);
                    currentPHM.tph_hdl_date = DateTime.ParseExact(txt2sec2_HDLCholesterolDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                }

                if (txt2sec2_PreviousHDLCholesterol.Text != "")
                {
                    currentPHM.tph_prev_hdl = Convert1.ToFloat(txt2sec2_PreviousHDLCholesterol.Text);
                    currentPHM.tph_prev_hdl_date = DateTime.ParseExact(txt2sec2_PreviousHDLCholesterolDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture); 
                }

                if (txt2sec2_TotalCholesterol.Text != "")
                {
                    currentPHM.tph_tot_choles = Convert1.ToFloat(txt2sec2_TotalCholesterol.Text);
                    currentPHM.tph_choles_date = DateTime.ParseExact(txt2sec2_TotalCholesterolDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture); 
                }

                if (txt2sec2_PreTotalCholesterol.Text != "")
                {
                    currentPHM.tph_prev_choles = Convert1.ToFloat(txt2sec2_PreTotalCholesterol.Text);
                    currentPHM.tph_prev_choles_date = DateTime.ParseExact(txt2sec2_PreTotalcholestorolDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                }

                if (txt2sec3_LDL.Text != "")
                {
                    currentPHM.tph_ldl_choles = Convert1.ToFloat(txt2sec3_LDL.Text);
                    currentPHM.tph_ldl_date = DateTime.ParseExact(txt2sec3_LDLDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture); 
                }

                if (txt2sec3_PreLDL.Text != "")
                {
                    currentPHM.tph_prev_ldl = Convert1.ToFloat(txt2sec3_PreLDL.Text);
                    currentPHM.tph_prev_ldl_date = DateTime.ParseExact(txt2sec3_PreLDLDate.Text, "dd/MM/yyyy", CultureInfo.CurrentCulture); 
                }

                currentPHM.tph_fpg_diagnosis = txt3sec2_FPGdiagnosis.Text;
                currentPHM.tph_hba1c_diagnosis = txt3sec2_HbA1cDiagnosis.Text;

                //Cadiovascura Save PHM dtl Type ="C"
                var objdatatypeC = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'C').FirstOrDefault();
                if (objdatatypeC != null)
                {
                    objdatatypeC.tpd_type = 'C';
                    objdatatypeC.tpd_category = txt2sec3_RiskCategory.Text;
                    objdatatypeC.tpd_clinic_recommend = txt2sec3_Recommend.Text;
                    objdatatypeC.tpd_refer_to_clinic = CBsec3_RefertoClinic.Text;
                    objdatatypeC.tpd_protocal = txt2sec3_Protocol.Text;
                    objdatatypeC.tpd_status = txt2sec3_StatusRefertoClinic.SelectedValue.ToString();
                    objdatatypeC.tpd_status_other = txt2sec3_ReasonfornotReferRemark.Text;
                    objdatatypeC.tpd_recomment = txt2sec3_Recommendation.Text;
                    objdatatypeC.tpd_concern = txt2sec3_ConcernPointForTLC.Text;
                    objdatatypeC.tpd_note = txt2sec3_Note.Text;
                    objdatatypeC.tpd_diagnosis = "";
                    objdatatypeC.tpd_create_by = Program.CurrentUser.mut_username;
                    objdatatypeC.tpd_create_date = Program.GetServerDateTime();
                    objdatatypeC.tpd_update_by = objdatatypeC.tpd_create_by;
                    objdatatypeC.tpd_update_date = objdatatypeC.tpd_create_date;
                }
                else
                {
                    trn_phm_dtl newitem = new trn_phm_dtl();
                    newitem.tpd_type = 'C';
                    newitem.tpd_category = txt2sec3_RiskCategory.Text;
                    newitem.tpd_clinic_recommend = txt2sec3_Recommend.Text;
                    newitem.tpd_refer_to_clinic = CBsec3_RefertoClinic.Text;
                    newitem.tpd_protocal = txt2sec3_Protocol.Text;
                    newitem.tpd_status = txt2sec3_StatusRefertoClinic.SelectedValue.ToString();
                    newitem.tpd_status_other = txt2sec3_ReasonfornotReferRemark.Text;
                    newitem.tpd_recomment = txt2sec3_Recommendation.Text;
                    newitem.tpd_concern = txt2sec3_ConcernPointForTLC.Text;
                    newitem.tpd_note = txt2sec3_Note.Text;
                    newitem.tpd_diagnosis = "";
                    newitem.tpd_create_by = Program.CurrentUser.mut_username;
                    newitem.tpd_create_date = Program.GetServerDateTime();
                    newitem.tpd_update_by = newitem.tpd_create_by;
                    newitem.tpd_update_date = newitem.tpd_create_date;
                    currentPHM.trn_phm_dtls.Add(newitem);
                }

                //Diabetes Save PHM dtl Type="D"
                var objdatatypeD = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'D').FirstOrDefault();
                if (objdatatypeD != null)
                {
                    objdatatypeD.tpd_type = 'D';
                    objdatatypeD.tpd_category = txt3sec3_RiskCategory.Text;
                    objdatatypeD.tpd_clinic_recommend = txt3sec3_Recommend.Text;
                    objdatatypeD.tpd_refer_to_clinic = txt3sec3_ReferToClinic.Text;
                    objdatatypeD.tpd_protocal = txt3sec3_Protocol.Text;
                    objdatatypeD.tpd_status = txt3sec3_ReasonFornotRefer.SelectedValue.ToString();
                    objdatatypeD.tpd_status_other = txt3sec3_ReasonfornotReferRemark.Text;
                    objdatatypeD.tpd_recomment = txt3sec3_Recommendation.Text;
                    objdatatypeD.tpd_concern = txt3sec3_ConcernPointsForTLC.Text;
                    objdatatypeD.tpd_note = txt3sec3_txtFollowupPoint.Text;
                    objdatatypeD.tpd_diagnosis = "";
                    objdatatypeD.tpd_create_by = Program.CurrentUser.mut_username;
                    objdatatypeD.tpd_create_date = Program.GetServerDateTime();
                    objdatatypeD.tpd_update_by = objdatatypeD.tpd_create_by;
                    objdatatypeD.tpd_update_date = objdatatypeD.tpd_create_date;
                }
                else
                {
                    trn_phm_dtl newitem = new trn_phm_dtl();
                    newitem.tpd_type = 'D';
                    newitem.tpd_category = txt3sec3_RiskCategory.Text;
                    newitem.tpd_clinic_recommend = txt3sec3_Recommend.Text;
                    newitem.tpd_refer_to_clinic = txt3sec3_ReferToClinic.Text;
                    newitem.tpd_protocal = txt3sec3_Protocol.Text;
                    newitem.tpd_status = txt3sec3_ReasonFornotRefer.SelectedValue.ToString();
                    newitem.tpd_status_other = txt3sec3_ReasonfornotReferRemark.Text;
                    newitem.tpd_recomment = txt3sec3_Recommendation.Text;
                    newitem.tpd_concern = txt3sec3_ConcernPointsForTLC.Text;
                    newitem.tpd_note = txt3sec3_txtFollowupPoint.Text;
                    newitem.tpd_diagnosis = "";
                    newitem.tpd_create_by = Program.CurrentUser.mut_username;
                    newitem.tpd_create_date = Program.GetServerDateTime();
                    newitem.tpd_update_by = newitem.tpd_create_by;
                    newitem.tpd_update_date = newitem.tpd_create_date;
                    currentPHM.trn_phm_dtls.Add(newitem);
                }

                //MeMograme Save PHM dtl Type="M"
                var objdatatypeM = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'M').FirstOrDefault();
                if (objdatatypeM != null)
                {
                    objdatatypeM.tpd_type = 'M';
                    objdatatypeM.tpd_category = txt4sec1_BiradsCategory.Text;
                    objdatatypeM.tpd_diagnosis = txt4sec1_Diagnosis.Text;
                    objdatatypeM.tpd_clinic_recommend = txt4sec1_Recommend.Text;
                    objdatatypeM.tpd_refer_to_clinic = txt4sec1_RefertoClinic.Text;
                    objdatatypeM.tpd_protocal = txt4sec1_Protocal.Text;
                    objdatatypeM.tpd_status = txt4sec1_StatusReferClinic.SelectedValue.ToString();
                    objdatatypeM.tpd_status_other = txt4sec1_ReasonfornotReferRemark.Text;
                    objdatatypeM.tpd_recomment = string.Empty;
                    objdatatypeM.tpd_concern = string.Empty;
                    objdatatypeM.tpd_note = txt4sec1_Remark.Text;
                    objdatatypeM.tpd_create_by = Program.CurrentUser.mut_username;
                    objdatatypeM.tpd_create_date = Program.GetServerDateTime();
                    objdatatypeM.tpd_update_by = objdatatypeM.tpd_create_by;
                    objdatatypeM.tpd_update_date = objdatatypeM.tpd_create_date;
                }
                else
                {
                    trn_phm_dtl newitem = new trn_phm_dtl();
                    newitem.tpd_type = 'M';
                    newitem.tpd_category = txt4sec1_BiradsCategory.Text;
                    newitem.tpd_diagnosis = txt4sec1_Diagnosis.Text;
                    newitem.tpd_clinic_recommend = txt4sec1_Recommend.Text;
                    newitem.tpd_refer_to_clinic = txt4sec1_RefertoClinic.Text;
                    newitem.tpd_protocal = txt4sec1_Protocal.Text;
                    newitem.tpd_status = txt4sec1_StatusReferClinic.SelectedValue.ToString();
                    newitem.tpd_status_other = txt4sec1_ReasonfornotReferRemark.Text;
                    newitem.tpd_recomment = string.Empty;
                    newitem.tpd_concern = string.Empty;
                    newitem.tpd_note = txt4sec1_Remark.Text;
                    newitem.tpd_create_by = Program.CurrentUser.mut_username;
                    newitem.tpd_create_date = Program.GetServerDateTime();
                    newitem.tpd_update_by = newitem.tpd_create_by;
                    newitem.tpd_update_date = newitem.tpd_create_date;
                    currentPHM.trn_phm_dtls.Add(newitem);
                }

                //AFP Type="A"
                var objdatatypeA = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'A').FirstOrDefault();
                if (objdatatypeA != null)
                {
                    objdatatypeA.tpd_type = 'A';
                    objdatatypeA.tpd_category = txt5sec1_tumorMarkevafp.Text;
                    objdatatypeA.tpd_diagnosis =string.Empty;
                    objdatatypeA.tpd_clinic_recommend = txt5sec1_Recommend.Text;
                    objdatatypeA.tpd_refer_to_clinic = txt5sec1_RefertoClinic.Text;
                    objdatatypeA.tpd_protocal = txt5sec1_Protocal.Text;
                    objdatatypeA.tpd_status = txt5sec1_StatusRefertoClinic.SelectedValue.ToString();
                    objdatatypeA.tpd_status_other = txt5sec1_ReasonfornotReferRemark.Text;
                    objdatatypeA.tpd_recomment = string.Empty;
                    objdatatypeA.tpd_concern = string.Empty;
                    objdatatypeA.tpd_note = txt5sec1_Remark.Text;
                    objdatatypeA.tpd_create_by = Program.CurrentUser.mut_username;
                    objdatatypeA.tpd_create_date = Program.GetServerDateTime();
                    objdatatypeA.tpd_update_by = objdatatypeA.tpd_create_by;
                    objdatatypeA.tpd_update_date = objdatatypeA.tpd_create_date;
                }
                else
                {
                    trn_phm_dtl newitem = new trn_phm_dtl();
                    newitem.tpd_type = 'A';
                    newitem.tpd_category = txt5sec1_tumorMarkevafp.Text;
                    newitem.tpd_diagnosis = string.Empty;
                    newitem.tpd_clinic_recommend = txt5sec1_Recommend.Text;
                    newitem.tpd_refer_to_clinic = txt5sec1_RefertoClinic.Text;
                    newitem.tpd_protocal = txt5sec1_Protocal.Text;
                    newitem.tpd_status = txt5sec1_StatusRefertoClinic.SelectedValue.ToString();
                    newitem.tpd_status_other = txt5sec1_ReasonfornotReferRemark.Text;
                    newitem.tpd_recomment = string.Empty;
                    newitem.tpd_concern = string.Empty;
                    newitem.tpd_note = txt5sec1_Remark.Text;
                    newitem.tpd_create_by = Program.CurrentUser.mut_username;
                    newitem.tpd_create_date = Program.GetServerDateTime();
                    newitem.tpd_update_by = newitem.tpd_create_by;
                    newitem.tpd_update_date = newitem.tpd_create_date;
                    currentPHM.trn_phm_dtls.Add(newitem);
                }

                //CEA Type="E"
                var objdatatypeE = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'E').FirstOrDefault();
                if (objdatatypeE != null)
                {
                    objdatatypeE.tpd_type = 'E';
                    objdatatypeE.tpd_category = txt5sec2_tumormarkevCFA.Text;
                    objdatatypeE.tpd_diagnosis = string.Empty;
                    objdatatypeE.tpd_clinic_recommend = txt5sec2_Recommend.Text;
                    objdatatypeE.tpd_refer_to_clinic = txt5sec2_ReferToClinic.Text;
                    objdatatypeE.tpd_protocal = txt5sec2_Protocal.Text;
                    objdatatypeE.tpd_status = txt5sec2_StatusRefertoClinic.SelectedValue.ToString();
                    objdatatypeE.tpd_status_other = txt5sec2_ReasonfornotReferRemark.Text;
                    objdatatypeE.tpd_recomment = string.Empty;
                    objdatatypeE.tpd_concern = string.Empty;
                    objdatatypeE.tpd_note = txt5sec2_Remark.Text;
                    objdatatypeE.tpd_create_by = Program.CurrentUser.mut_username;
                    objdatatypeE.tpd_create_date = Program.GetServerDateTime();
                    objdatatypeE.tpd_update_by = objdatatypeE.tpd_create_by;
                    objdatatypeE.tpd_update_date = objdatatypeE.tpd_create_date;
                }
                else
                {
                    trn_phm_dtl newitem = new trn_phm_dtl();
                    newitem.tpd_type = 'E';
                    newitem.tpd_category = txt5sec2_tumormarkevCFA.Text;
                    newitem.tpd_diagnosis = string.Empty;
                    newitem.tpd_clinic_recommend = txt5sec2_Recommend.Text;
                    newitem.tpd_refer_to_clinic = txt5sec2_ReferToClinic.Text;
                    newitem.tpd_protocal = txt5sec2_Protocal.Text;
                    newitem.tpd_status = txt5sec2_StatusRefertoClinic.SelectedValue.ToString();
                    newitem.tpd_status_other = txt5sec2_ReasonfornotReferRemark.Text;
                    newitem.tpd_recomment = string.Empty;
                    newitem.tpd_concern = string.Empty;
                    newitem.tpd_note = txt5sec2_Remark.Text;
                    newitem.tpd_create_by = Program.CurrentUser.mut_username;
                    newitem.tpd_create_date = Program.GetServerDateTime();
                    newitem.tpd_update_by = newitem.tpd_create_by;
                    newitem.tpd_update_date = newitem.tpd_create_date;
                    currentPHM.trn_phm_dtls.Add(newitem);
                }

                //PSA Type="P"
                var objdatatypeP = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'P').FirstOrDefault();
                if (objdatatypeP != null)
                {
                    objdatatypeP.tpd_type = 'P';
                    objdatatypeP.tpd_category = txt5sec3_TumorMarkevPSA.Text;
                    objdatatypeP.tpd_diagnosis = string.Empty;
                    objdatatypeP.tpd_clinic_recommend = txt5sec3_Recommend.Text;
                    objdatatypeP.tpd_refer_to_clinic = txt5sec3_ReferToClinic.Text;
                    objdatatypeP.tpd_protocal = txt5sec3_Protocal.Text;
                    objdatatypeP.tpd_status = txt5sec3_StatusRefertoClinic.SelectedValue.ToString();
                    objdatatypeP.tpd_status_other = txt5sec3_ReasonfornotReferRemark.Text;
                    objdatatypeP.tpd_recomment = string.Empty;
                    objdatatypeP.tpd_concern = string.Empty;
                    objdatatypeP.tpd_note = txt5sec3_Remark.Text;
                    objdatatypeP.tpd_create_by = Program.CurrentUser.mut_username;
                    objdatatypeP.tpd_create_date = Program.GetServerDateTime();
                    objdatatypeP.tpd_update_by = objdatatypeP.tpd_create_by;
                    objdatatypeP.tpd_update_date = objdatatypeP.tpd_create_date;
                }
                else
                {
                    trn_phm_dtl newitem = new trn_phm_dtl();
                    newitem.tpd_type = 'P';
                    newitem.tpd_category = txt5sec3_TumorMarkevPSA.Text;
                    newitem.tpd_diagnosis = string.Empty;
                    newitem.tpd_clinic_recommend = txt5sec3_Recommend.Text;
                    newitem.tpd_refer_to_clinic = txt5sec3_ReferToClinic.Text;
                    newitem.tpd_protocal = txt5sec3_Protocal.Text;
                    newitem.tpd_status = txt5sec3_StatusRefertoClinic.SelectedValue.ToString();
                    newitem.tpd_status_other = txt5sec3_ReasonfornotReferRemark.Text;
                    newitem.tpd_recomment = string.Empty;
                    newitem.tpd_concern = string.Empty;
                    newitem.tpd_note = txt5sec3_Remark.Text;
                    newitem.tpd_create_by = Program.CurrentUser.mut_username;
                    newitem.tpd_create_date = Program.GetServerDateTime();
                    newitem.tpd_update_by = newitem.tpd_create_by;
                    newitem.tpd_update_date = newitem.tpd_create_date;
                    currentPHM.trn_phm_dtls.Add(newitem);
                }

                PHMhdrbindingSource1.EndEdit();
                try
                {
                    dbc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                    {
                        dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    dbc.SubmitChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "Save", ex, false);
                return false;
            }
        }
        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            int tpr_id;
            if (Program.CurrentRegis != null)
            {
                tpr_id = Program.CurrentRegis.tpr_id;
            }
            else
            {
                tpr_id = SetTprID;
            }
            if (Program.chkBookComplete(tpr_id))
            {
                if (Save('D'))
                {
                    lbAlertMsg.Focus();
                    lbAlertMsg.Text = "Save data completed.";
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Save('N'))
            {
                try
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                //CallQueue.PSendManualAllRoom();
                DateTime dtnow = Program.GetServerDateTime();
                trn_patient_queue CurrentQueue = (from t1 in dbc.trn_patient_queues
                                                  where t1.tps_id == Program.CurrentPatient_queue.tps_id
                                                  select t1).FirstOrDefault();
                CurrentQueue.tpr_id = Program.CurrentRegis.tpr_id;
                CurrentQueue.mrm_id = Program.CurrentRoom.mrm_id;
                CurrentQueue.mrd_id = Program.CurrentRoom.mrd_id;
                CurrentQueue.tps_end_date = dtnow;
                CurrentQueue.tps_update_date = dtnow;
                CurrentQueue.tps_status = "ED";

                    //สงไป Book อย่างเดียว
                int mrmid = mst.GetMstRoomHdr("BK").mrm_id;
                int mvtid = mst.GetMstEvent("BK").mvt_id;
                   var objqueue = (from t1 in dbc.trn_patient_queues
                                   where t1.tpr_id == Program.CurrentRegis.tpr_id
                                       && t1.mrm_id == mrmid
                                       && t1.mvt_id == mvtid
                                   select t1).FirstOrDefault();
                   if (objqueue == null && mrmid != 0)
                   {
                       trn_patient_queue newitem = new trn_patient_queue();
                       newitem.tpr_id = Program.CurrentRegis.tpr_id;
                       newitem.mrm_id = mrmid;
                       newitem.mvt_id = mvtid;
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
                   }
                   else if (objqueue != null && mrmid != 0)
                   {
                       objqueue.mrd_id = null;
                       objqueue.tps_status = "NS";
                       objqueue.tps_ns_status = "QL";
                       objqueue.tps_create_date = dtnow;
                       objqueue.tps_create_by = Program.CurrentUser.mut_username;
                       objqueue.tps_update_by = objqueue.tps_create_by;
                       objqueue.tps_update_date = dtnow;
                   }
                   dbc.SubmitChanges();
                    StatusWaitCallQueue();
                    //string roomName = Program.Getmvt_Name(mvtid);
                    lbAlertMsg.Text = "Checkup Process Completed"; //CallQueue.GetStrSaveAndSend();
                }
                catch (Exception ex)
                {
                    lbAlertMsg.Text = "Error :" + ex.Message;
                }

            }
            lbAlertMsg.Focus();
        }
        private void btnSaveandSendauto_Click(object sender, EventArgs e)
        {
            if (Save('N'))
            {
                try
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    //CallQueue.PSendManualAllRoom();
                    DateTime dtnow = Program.GetServerDateTime();
                    trn_patient_queue CurrentQueue = (from t1 in dbc.trn_patient_queues
                                                      where t1.tps_id == Program.CurrentPatient_queue.tps_id
                                                      select t1).FirstOrDefault();
                    CurrentQueue.tpr_id = Program.CurrentRegis.tpr_id;
                    CurrentQueue.mrm_id = Program.CurrentRoom.mrm_id;
                    CurrentQueue.mrd_id = Program.CurrentRoom.mrd_id;
                    CurrentQueue.tps_end_date = dtnow;
                    CurrentQueue.tps_update_date = dtnow;
                    CurrentQueue.tps_status = "ED";

                    //สงไป Book อย่างเดียว
                    int mrmid = mst.GetMstRoomHdr("BK").mrm_id;
                    int mvtid = mst.GetMstEvent("BK").mvt_id;
                    var objqueue = (from t1 in dbc.trn_patient_queues
                                    where t1.tpr_id == Program.CurrentRegis.tpr_id
                                        && t1.mrm_id == mrmid
                                        && t1.mvt_id == mvtid
                                    select t1).FirstOrDefault();
                    if (objqueue == null && mrmid != 0)
                    {
                        trn_patient_queue newitem = new trn_patient_queue();
                        newitem.tpr_id = Program.CurrentRegis.tpr_id;
                        newitem.mrm_id = mrmid;
                        newitem.mvt_id = mvtid;
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
                    }
                    else if (objqueue != null && mrmid != 0)
                    {
                        objqueue.mrd_id = null;
                        objqueue.tps_status = "NS";
                        objqueue.tps_ns_status = "QL";
                        objqueue.tps_create_date = dtnow;
                        objqueue.tps_create_by = Program.CurrentUser.mut_username;
                        objqueue.tps_update_by = objqueue.tps_create_by;
                        objqueue.tps_update_date = dtnow;
                    }
                    dbc.SubmitChanges();
                    StatusWaitCallQueue();
                   // string roomName = Program.Getmvt_Name(mvtid);
                    lbAlertMsg.Text ="Checkup Process Completed";
                }
                catch (Exception ex)
                {
                    lbAlertMsg.Text = "Error :" + ex.Message;
                }
            }
            lbAlertMsg.Focus();
        }
        
        private void Sumpoint2sec1()
        {
            var Smokepoint = Utility.GetInteger(txt2sec1_SmokePoint.Text.Trim());
            var familyhistoryPoint = Utility.GetInteger(txt2sec1_familyhistoryPoint.Text.Trim());
            var HighBloodPoint = Utility.GetInteger(txt2sec1_HighBloodPoint.Text.Trim());
            var AgePoint = Utility.GetInteger(txt2sec1_AgePoint.Text.Trim());
            var HDLPoint = Utility.GetInteger(txt2sec1_HDLPoint.Text.Trim());

            trn_phm_hdr currentPHM = (trn_phm_hdr)PHMhdrbindingSource1.Current;
            currentPHM.tph_pre_cardio_pt = Smokepoint + familyhistoryPoint + HighBloodPoint + HDLPoint;

        }
        private void txt2sec1_Point_TextChanged(object sender, EventArgs e)
        {
            Sumpoint2sec1();
        }

        private void ShowChat(int tpr_id)
        {
            var objcurrentRegis = (from t1 in dbc.trn_patient_regis
                                   where t1.tpr_id == tpr_id
                                   select t1).FirstOrDefault();
            string HNno = (from t1 in dbc.trn_patients where t1.tpt_id == objcurrentRegis.tpt_id select t1.tpt_hn_no).FirstOrDefault();
            var objAFP_List = (from t1 in dbc.trn_patient_labs
                               where (t1.tpl_lab_no == "N0380"
                               || t1.tpl_lab_no == "N7006")
                               && t1.tpl_hn_no == HNno
                               orderby t1.tpl_lab_date descending
                               select new { t1.tpl_lab_date.Value.Date, Value = convertdata(t1.tpl_lab_value) }).Take(5).ToList();
            ChartAFP.DataSource = objAFP_List.Where(x => x.Value > 0);

            var objCEA_List = (from t1 in dbc.trn_patient_labs
                               where (t1.tpl_lab_no == "N0390"
                               || t1.tpl_lab_no == "N7007")
                               && t1.tpl_hn_no == HNno
                               orderby t1.tpl_lab_date descending
                               select new { t1.tpl_lab_date.Value.Date, Value = convertdata(t1.tpl_lab_value) }).Take(5).ToList();
            ChartCEA.DataSource = objCEA_List.Where(x=>x.Value>0);


            var objPSA_List = (from t1 in dbc.trn_patient_labs
                               where (t1.tpl_lab_no == "N0050")
                               && t1.tpl_hn_no == HNno
                               orderby t1.tpl_lab_date descending
                               select new { t1.tpl_lab_date.Value.Date, Value = convertdata(t1.tpl_lab_value) }).Take(5).ToList();
            ChartPSA.DataSource = objPSA_List.Where(x => x.Value > 0);

        }
        private double convertdata(string strvalue)
        {
            return Convert1.ToDouble(strvalue);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadPHMList(txtSearch.Text.Trim());
        }

        private void txt2sec3_StatusRefertoClinic_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txt2sec3_StatusRefertoClinic.Text == "Make Appointment at Heart Clinic" || txt2sec3_StatusRefertoClinic.Text=="")
            {
                txt2sec3_ReasonfornotReferRemark.Enabled = false;
            }
            else
            {
                txt2sec3_ReasonfornotReferRemark.Enabled = true;
            }
        }

        private void txt3sec3_ReasonFornotRefer_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txt3sec3_ReasonFornotRefer.Text == "Make Appointment at Heart Clinic" || txt3sec3_ReasonFornotRefer.Text == "")
            {
                txt3sec3_ReasonfornotReferRemark.Enabled = false;
            }
            else
            {
                txt3sec3_ReasonfornotReferRemark.Enabled = true;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalTab6();
            if (SetTprID > 0)
            {
                CalTab3(SetTprID);
            }
            else
            {
                if (Program.CurrentRegis != null)
                {
                    CalTab3(Program.CurrentRegis.tpr_id);
                }
            }
            if (Convert1.ToInt32(txt2sec1_HighBloodPoint.Text) > 0)
            {
                ChangeForeColor(txt2sec1_SystolicBP);
                ChangeForeColor(txt2sec1_DiastolicBP);
                txt2sec1_DrugforhighBlood.ForeColor = Color.Red;
            }
            if (Convert1.ToInt32(txt2sec1_SmokePoint.Text) > 0)
            {
                ChangeForeColor(txt2sec1_SmokePoint);
                ChangeForeColor(txt2sec1_Smoke);
                txt2sec1_Smoke.ForeColor = Color.Red;
            }
            if (Convert1.ToInt32(txt2sec1_familyhistoryPoint.Text) > 0)
            {
                txt2sec1_FamilyHistory.ForeColor = Color.Red;
            }

            if (Convert1.ToInt32(txt2sec1_familyhistoryPoint.Text) > 0)
            {
                txt2sec1_FamilyHistory.ForeColor = Color.Red;
            }
            if (Convert1.ToInt32(txt2sec1_AgePoint.Text) > 0)
            {
                txt2sec1_Age.ForeColor = Color.Red;
                txt2sec1_Gender.ForeColor = Color.Red;
            }
            if (Convert1.ToInt32(txt2sec1_HDLPoint.Text) > 0)
            {
                txt2sec1_HDLCholesterol.ForeColor = Color.Red;
            }
            
        }

        private void DDRaceGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            if (DDRaceGroup.SelectedValue != null && IsLoadData==false)
            {
                int ddd = Convert.ToInt32(DDRaceGroup.SelectedValue);
                var objRace = (from t1 in dbc.mst_races
                               where t1.mag_id == ddd
                               select new DropdownData
                               {
                                   Code = t1.mae_id,
                                   Name = t1.mae_ename
                               }).ToList();
                DDRace.ValueMember = "Code";
                DDRace.DisplayMember = "Name";
                DDRace.DataSource = objRace;

                string asian;
                if (DDRaceGroup.Text == "ASIAN")
                    asian = "A";
                else
                    asian = "N";

                GetBMI(asian); 
            }
        }

        private void GetBMI(string status)
        {
            ////Added.Akkaradech on 2014-01-21
            try
            {
                if (Program.CurrentRegis != null)
                {
                    var getObj = dbc.pw_Get_BMI(Program.CurrentRegis.tpr_id, status, "textbox", txtBMI.Text).FirstOrDefault();
                    switch (getObj.mpd_str_value1.ToString())
                    {
                        case "U":
                            txtBMI.ForeColor = Color.Orange;
                            break;
                        case "N":
                            txtBMI.ForeColor = Color.Black;
                            break;
                        case "O":
                            txtBMI.ForeColor = Color.Orange;
                            break;
                        case "B":
                            txtBMI.ForeColor = Color.Red;
                            break;
                    }
                }
                else
                    return;
            }
            catch
            {
                return;
            }
        }

        private void ChangeForeColor(TextBox txt)
        {
            ////Added.Akkaradech on 2014-01-21
            try
            {
                if (Convert1.ToInt32(txt.Text) > 0 && txt.Text !="" )
                    txt.ForeColor = Color.Red;
                else
                    txt.ForeColor = Color.Black;
            }
            catch
            {
                return;
            }
        }

        private void GetReferToClinic(ComboBox cb)
        {
            ////Added.Akkaradech on 2014-01-21
            try
            {
                var obj = (from t in dbc.mst_locations
                           where t.mlc_status == 'A'
                           && dtCurrent >= t.mlc_effective_date.Value
                           && (t.mlc_expire_date != null ? (dtCurrent <= t.mlc_expire_date.Value) : true)
                           orderby t.mlc_ename
                           select new DropdownData { Code = t.mlc_id, Name = t.mlc_ename }).ToList();

                DropdownData newselect = new DropdownData();
                newselect.Code = null;
                newselect.Name = "- Select -";
                obj.Insert(0, newselect);
                cb.DataSource = obj;
                cb.DisplayMember = "Name";
                cb.ValueMember = "Code";
                if (obj.Count > 0)
                {
                    cb.SelectedIndex = 0;
                }
            }
            catch
            {
                return;
            }
        }

        private void ChangeFontColor(Control control)
        {
            ////Change font color when it display 'Yes'
            foreach (Control ctl in control.Controls)
            {
                TextBox txt;
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.ForeColor = Color.Empty;
                    if (txt.Text == "ใช่(Yes)")
                    {
                        txt.ForeColor = Color.Red;
                    }
                    if (txt.Text == "ไม่ใช่(No)")
                    {
                        txt.ForeColor = Color.Black;
                    }
                }
            }
        }

        #region Unit Display

        countDownCls clsCountDown = new countDownCls();

        private void LoadHandlerCountDown()
        {
            clsCountDown.successCountDown += new countDownCls.SuccessHandler(cls_successCountDown);
            clsCountDown.countDownTick += new countDownCls.TickHandler(cls_countDownTick);
        }

        private void cls_countDownTick(object sender, timeArgs e)
        {
            groupQueue.Text = "Queue " + e.countDownTime.timeString;
        }

        private void cls_successCountDown(object sender, successTypeArgs e)
        {
            lbAlertMsg.Text = "";

            int tpr_id = Program.CurrentRegis.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;

            StatusTransaction ready = CallQueue.P_CallQueueReady();
            if (ready == StatusTransaction.True)
            {
                StatusTransaction showUnit = new ClsTCPClient().sendCallUnitDisplay();
                if (showUnit == StatusTransaction.Error)
                {
                    //lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถแสดงผลบน unit display ได้";
                }
                StatusCallQueueReady();
                Class.FunctionDataCls func = new Class.FunctionDataCls();
                resultUltrasound = func.popupUltrasoundLower();
                if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.BeforeStation)
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    List<int> mvt = mst.GetMstRoomEventByMrm(Program.CurrentRoom.mrm_id).Select(x => x.mvt_id).ToList();

                    Class.FunctionDataCls.sendQueueStatus result = func.sendQueueUltrasoundLower(resultUltrasound, mvt);
                    if (result == Class.FunctionDataCls.sendQueueStatus.error)
                    {
                        lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถส่งไป ultrasound ได้ กรุณาติดต่อผู้ดูแลระบบ";
                        AlertOutDepartment.LoadTime();
                    }
                    else if (result == Class.FunctionDataCls.sendQueueStatus.sendSuccess)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendUltraSoundBefore,
                                                    tpr_id,
                                                    tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);

                        new ClsTCPClient().sendClearUnitDisplay();
                        ClearFrm();
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = func.GetStrSaveAndSend(tpr_id, "US", "UL");
                    }
                }
                else if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
                {
                    //btnSendManual.Enabled = false;
                    //btnSendToCheckB.Enabled = false;
                    AlertOutDepartment.LoadTime();

                    ReserveSkipCls reserveSkip = new ReserveSkipCls();
                    int? skipRoom = reserveSkip.CheckRoomSkip(tpr_id);
                    string alert = reserveSkip.MessegeAlertSkip(skipRoom);
                    lbAlertMsg.Text = alert;
                }
                else if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater)
                {
                    AlertOutDepartment.LoadTime();

                    ReserveSkipCls reserveSkip = new ReserveSkipCls();
                    int? skipRoom = reserveSkip.CheckRoomSkip(tpr_id);
                    string alert = reserveSkip.MessegeAlertSkip(skipRoom);
                    lbAlertMsg.Text = alert;
                }
            }
            else if (ready == StatusTransaction.Error)
            {
                lbAlertMsg.Text = "กรุณากดปุ่ม Ready อีกครั้ง";
                btnReady.Enabled = true;
            }
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            btnReady.Enabled = false;
            clsCountDown.finishCountDown();
        }

        private void StatusCallQueueReady()
        {
            groupQueue.Text = "Queue";
            btnReady.Enabled = false;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            //btnCancel.Enabled = true;

            btnSave.Enabled = true;
            btnSaveDraft.Enabled = true;
        }

        private void StatusCallQueueWaitingReady()
        {
            btnReady.Enabled = true;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;

            btnSave.Enabled = false;
            btnSaveDraft.Enabled = false;
        }

        #endregion

        private void label73_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeFontColor(panel3PersonalHealthManagment);
            ChangeFontColor(groupBox3);
            ChangeFontColor(groupBox4);
            ChangeFontColor(groupBox5);
            ChangeFontColor(groupBox6);
            ChangeFontColor(groupBox11);
            ChangeFontColor(groupBox12);
            ChangeFontColor(panel6);
            ChangeFontColor(panel7);
            ChangeFontColor(panel11);
            ChangeFontColor(panel12);
            ChangeFontColor(panel13);
            ChangeFontColor(panel4Diabetes);
            ChangeFontColor(groupBox11);
            ChangeFontColor(groupBox12);
            ChangeForeColor(txt2sec1_SmokePoint);
            ChangeForeColor(txt2sec1_HighBloodPoint);
            ChangeForeColor(txt2sec1_familyhistoryPoint);
            ChangeForeColor(txt2sec1_AgePoint);
            ChangeForeColor(txt2sec1_HDLPoint);
            ChangeForeColor(txt2sec1_PreCardioCount);
            if (Convert1.ToInt32(txt3sec1_FamilyHistoryPoint.Text) > 0)
                txt3sec1_FamilyHistoryPoint.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_TotalPoint.Text) > 0)
                ChangeForeColor(txt3sec1_TotalPoint);
            if (Convert1.ToInt32(txt3sec1_ExercisePoint.Text) > 0)
                txt3sec1_ExercisePoint.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_BMIPoint.Text) > 0)
                txt3sec1_BMIPoint.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_OverWeightInfantPoint.Text) > 0)
                txt3sec1_OverWeightInfantPoint.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_HighBlood.Text) > 0)
                txt3sec1_HighBlood.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_systoricBP.Text) > 0)
                txt3sec1_systoricBP.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_OverWeightInfant.Text) > 0)
                txt3sec1_OverWeightInfant.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_AgePoint.Text) > 0)
                txt3sec1_AgePoint.ForeColor = Color.Red;
            if (Convert1.ToInt32(txt3sec1_MaleGenderPoint.Text) > 0)
                txt3sec1_MaleGenderPoint.ForeColor = Color.Red; 
        }

        private void frmPHM_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsCountDown.cancelCountDown();
        }

        private void uiAllLeft1_OnWaitingSuccessProcess(object sender, StatusTransaction isCallQueue, string e)
        {
            lbAlertMsg.Text = "";
            this.AutoScrollPosition = new Point(0, 0);

            lbAlertMsg.Text = e;
            if (isCallQueue == StatusTransaction.True)
            {
                btnCallQueue.Enabled = false;

                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();
                Application.DoEvents();

                if (Program.CurrentRegis != null)
                {
                    LoadUI();
                    clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                    try
                    {
                        LoadData(Program.CurrentRegis.tpr_id);

                        var objRaceGroup = (from t1 in dbc.mst_race_grps
                                            orderby t1.mag_ename
                                            select new DropdownData
                                            {
                                                Code = t1.mag_id,
                                                Name = t1.mag_ename
                                            }).ToList();
                        DropdownData newselect = new DropdownData();
                        newselect.Name = "";
                        objRaceGroup.Insert(0, newselect);
                        DDRaceGroup.ValueMember = "Code";
                        DDRaceGroup.DisplayMember = "Name";
                        DDRaceGroup.DataSource = objRaceGroup;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "uiAllLeft1_OnWaitingSuccessProcess", ex, false);
                    }
                    StatusCallQueueWaitingReady();
                }
                else
                {
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = "No patient on queue!";
                    btnCallQueue.Enabled = true;
                }

                frmbg.Close();
            }
        }

    }
    class PHMList
    {
        public string QueueNo { get; set; }
        public string HnNo { get; set; }
        public DateTime? ArriveDate { get; set; }
        public string FullName { get; set; }
        public int SiteId { get; set; }
        public string mhc_Name { get; set; }
        public string Status { get; set; }
        public string DoctorName { get; set; }
    }
}
