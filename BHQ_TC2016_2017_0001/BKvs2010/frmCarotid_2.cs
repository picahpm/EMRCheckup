using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using BKvs2010.EmrClass;
using BKvs2010.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmCarotid_2 : Form
    {
        PopupUltrasoundLower.ResultPopupUltrasoundLower resultUltrasound = PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater;

        private bool Brush = true;
        Usercontrols.ToolPaint.Shapes DrawingShapes = new Usercontrols.ToolPaint.Shapes();
        private bool IsPainting = false;
        private bool IsEraseing = false;
        private Point LastPos = new Point(0, 0);
        private Color CurrentColour = Color.Black;
        private float CurrentWidth = 10;
        private int ShapeNum = 0;
        private Point MouseLoc = new Point(0, 0);
        private bool IsMouseing = false;
        private bool Brush2 = true;
        //LT
        Usercontrols.ToolPaint.Shapes DrawingShapes2 = new Usercontrols.ToolPaint.Shapes();
        private bool IsPainting2 = false;
        private bool IsEraseing2 = false;
        private Point LastPos2 = new Point(0, 0);
        private Color CurrentColour2 = Color.Black;
        private float CurrentWidth2 = 10;
        private int ShapeNum2 = 0,count;
        private Point MouseLoc2 = new Point(0, 0);
        object objimage_L;
        object objimage_R;
        string strTagID_L, strTagID_R;
        private bool IsRemark_L = true;
        private bool IsRemark_R = true;
        DataTable dt = new DataTable();
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        public string HN ;
        public string EN ;
        public string page ;
        public string doctorCode ;
        public string locationCode ;
        public string documentCode ;
        public string item;
        public byte[] img;
        AutoCompleteDoctor obj = new AutoCompleteDoctor();

        public frmCarotid_2()
        {
            InitializeComponent();
            pictureBoxLeft.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pictureBoxLeft, new object[] { System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true });
            pictureBoxRight.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pictureBoxRight, new object[] { System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true });
            //dateTimePicker1.MinDate = DateTime.Today; dateTimeFollowup.MinDate = DateTime.Today;
            uiAllLeft1.OnRefreshStatusED += new Usercontrols.UIAllLeft.RefreshStatusED(uiAllLeft1_OnRefreshStatusED);
            uiFooter1.OnFooternameClick += new Usercontrols.UIFooter.FooterNameClick(OnUCFooterClicked);

            autoCompleteUC1.DataSource = GetData();
            autoCompleteUC1.DisplayMember = "fullname";
            autoCompleteUC1.ValueMember = "username";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

            autoCompleteUC2.DataSource = obj.GetDoctorData();
            autoCompleteUC2.ValueMember = "SSUSR_Initials";
            autoCompleteUC2.DisplayMember = "CTPCP_Desc";
            autoCompleteUC2.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC2_SelectedValueChanged);

        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_carotid_tech carotid = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                if (carotid != null)
                {
                    if (e == null)
                    {
                        carotid.tct_technician_code = null;
                        carotid.tct_technician_name = null;
                    }
                    else
                    {
                        carotid.tct_technician_code = ((DoctorProfile)e).SSUSR_Initials;
                        carotid.tct_technician_name = ((DoctorProfile)e).CTPCP_Desc;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }
        private void autoCompleteUC2_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_carotid_tech objcurrenttpr = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                if (objcurrenttpr != null)
                {
                    if (e == null)
                    {
                        txtDoctorCode.Text = "";
                        objcurrenttpr.tct_doctor_code = null;
                        objcurrenttpr.tct_doctor_name = null;                        
                    }
                    else
                    {
                        txtDoctorCode.Text = ((DoctorProfile)e).SSUSR_Initials;
                        objcurrenttpr.tct_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        objcurrenttpr.tct_doctor_name = ((DoctorProfile)e).CTPCP_Desc;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC2_SelectedValueChanged", ex, false);
            }
        }
        public int SetTprID { get; set; }
        public int siteitem { get; set; }

        class Technician
        {
            public string username { get; set; }
            public string fullname { get; set; }
        }
        private List<Technician> GetData()
        {
            List<Technician> lstName=new List<Technician>();
            try
            {
                using(InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    var result = dbc.mst_user_types.Select(x => new Technician
                    {
                        username = x.mut_username,
                        fullname = x.mut_fullname
                    }).ToList();
                    result.Insert(0, new Technician { username = "", fullname = "" });

                    lstName = result.ToList();
                }
                return lstName;
            }
            catch
            { return lstName; }
        }

        private void OnUCFooterClicked(int iprid, int mhs_id)
        {
            // Handle event Footer from here
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            siteitem = mhs_id;
            SetTprID = iprid;
            trn_patient_queue objQtxp = (from t1 in dbc.trn_patient_queues
                                         where 
                                         //t1.mst_room_hdr.mhs_id == mhs_id && 
                                         t1.mst_room_hdr.mrm_code == "CD"
                                         && t1.tpr_id == iprid
                                         select t1).FirstOrDefault();
            if (objQtxp != null)
            {
                //Program.CurrentPatient_queue = objQtxp;
                //Program.CurrentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == iprid select t1).FirstOrDefault();
                this.LoadData(iprid);
                LoadHistory(iprid);
                LoadLabResult(iprid);

                btnReady.Enabled = false;
                btnCallQueue.Enabled = false;
                btnHold.Enabled = false;
                btnCancel.Enabled = false;
                btnSendManual.Enabled = false;
                btnSendAuto.Enabled = false;
                btnSendToCheckB.Enabled = false;
                btnSendtoDocScan.Enabled = false;
                btnPrintPreview.Enabled = true;
                btnPrint.Enabled = true;
                uiAllLeft1.LoadDataAll(iprid);
                //uiFooter1.LoadData();
                //Enable
                trn_carotid_tech Carotid_Tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                if (Carotid_Tech.tct_create_by == Program.CurrentUser.mut_username)
                {
                    GBLeft.Enabled = true;
                    GBRight.Enabled = true;
                    GBpaint.Enabled = true;
                    pictureBoxRight.Enabled = true;
                    pictureBoxLeft.Enabled = true;
                    btnSaveDraft.Enabled = true;
                }
                else
                {
                    GBLeft.Enabled = false;
                    GBRight.Enabled = false;
                    GBpaint.Enabled = false;
                    pictureBoxRight.Enabled = false;
                    pictureBoxLeft.Enabled = false;
                    btnSaveDraft.Enabled = false;
                }
            }
            frmbg.Close();
        }
        private void uiAllLeft1_OnRefreshStatusED()
        {
            Clearfrm();
            StatusWaitCallQueue();
        }

        #region Functions
        private void LoadHistory(int tpr_id)
        {
            try
            {
                CheckUpLabClass.retrieveLabToPatientLab(new List<string> { "C0180", "C0150", "N0510", "C0159" }, tpr_id);
                string HNno = (from t1 in dbc.trn_patient_regis
                               where t1.tpr_id == Program.CurrentRegis.tpr_id
                               select t1.trn_patient.tpt_hn_no).FirstOrDefault();
                string EN = (from t in dbc.trn_patient_regis where t.tpr_id == Program.CurrentRegis.tpr_id select t.tpr_en_no).FirstOrDefault();
                var ObjectHistory = (from t1 in dbc.trn_carotid_teches
                                     where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno  //t1.tpr_id == tpr_id
                                     select new
                                     {
                                         EN = t1.trn_patient_regi.tpr_en_no,
                                         ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                         CreateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tct_create_by).Single().mut_fullname,
                                         CreateDate = t1.tct_create_date,
                                         UpdateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tct_create_by).Single().mut_fullname,
                                         UpdateDate = t1.tct_create_date,
                                         sts = (t1.tct_doc_scan == true) ? "Completed" : "Not send to Docscan",
                                         Link = (t1.tct_doc_scan == true) ? "View" : "Send"
                                     }).ToList();
                Gv_History.DataSource = ObjectHistory;
            }
            catch
            { return; }
        }

        private void clrPicture(PictureBox pic)
        {
            foreach (Control ctl in pic.Controls)
            {
                Label lbl;
                if (ctl is Label)
                {
                    lbl = (Label)ctl;
                    lbl.Visible = false;
                }
            }
        }
        private void clrGroupbox(GroupBox gb)
        {
            foreach (Control ctl in gb.Controls)
            { 
                TextBox txt;
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.Text = "";
                }
            }
        }
        private void Loadfrm()
        {
            if (SetTprID > 0)
            {
                OnUCFooterClicked(SetTprID, siteitem); return;
            }
            if (!Program.IsDummy)
            {
                this.Text = Program.GetRoomName();
                LoadHistoryCallLast();
            }
            if (Program.CurrentRegis != null)
            {
                this.LoadData(Program.CurrentRegis.tpr_id);
                //btnSaveAsDraft.Enabled = true;
                /*btnCarotidReport.Enabled = (Program.CurrentUser.mut_type == 'D');*/
                LoadHistory(Program.CurrentRegis.tpr_id);
                LoadLabResult(Program.CurrentRegis.tpr_id);
                tabControl1.Enabled = true;                
            }
            else
            {
                if (Program.IsDummy)
                {
                    btnSendAuto.Enabled = false;
                    btnSendToCheckB.Enabled = false;
                    btnSendManual.Enabled = false;
                    btnSaveDraft.Enabled = false;
                    /*btnCarotidReport.Enabled = false;*/
                }
                else
                {
                    Clearfrm();
                    StatusWaitCallQueue();
                }
                tabControl1.Enabled = false;                
            }
        }
        private void LoadHistoryCallLast()
        {
            try
            {

                DateTime dtnow = Program.GetServerDateTime();
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
                    uiMenuBar1.LoadEnableQuestionare();
                    LoadUI();
                    StatusCallQueueReady();
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
                        uiMenuBar1.LoadEnableQuestionare();
                        LoadUI();
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        Program.CurrentPatient_queue = null;
                        Program.CurrentRegis = null;
                    }
                }

            }
            catch
            {
                lbAlertMsg.Text = "Not found patient";
            }


        }
        private void LoadLabResult(int tpr_id)
        {
            //Add by Noina
            trn_patient_regi objrg = new trn_patient_regi();
            if (Program.CurrentPatient_queue == null)
            {
                objrg = (from rg in dbc.trn_patient_regis where rg.tpr_id == tpr_id select rg).FirstOrDefault();
                
            }
            var ObjLabFbs = (from t1 in dbc.trn_patient_labs
                             where t1.tpl_hn_no == (Program.CurrentRegis == null ? objrg.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no)
                             && t1.tpl_en_no == (Program.CurrentPatient_queue == null  ? objrg.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no)
                             && t1.tpl_lab_no == "C0180"
                             select t1.tpl_lab_value).FirstOrDefault(); 
            var ObjLabCholes = (from t1 in dbc.trn_patient_labs
                             where t1.tpl_hn_no == (Program.CurrentRegis == null ? objrg.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no)
                             && t1.tpl_en_no == (Program.CurrentPatient_queue == null ? objrg.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no) //Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no
                             && t1.tpl_lab_no == "C0130"
                             select t1.tpl_lab_value).FirstOrDefault(); 
            var ObjLabBmi = (from t1 in dbc.trn_basic_measure_dtls
                             join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                             where t2.tpr_id == tpr_id//tprid
                             select t1.tbd_bmi).FirstOrDefault();
            var ObjLabHbA = (from t1 in dbc.trn_patient_labs
                             where t1.tpl_hn_no == (Program.CurrentRegis == null ? objrg.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no)
                                && t1.tpl_en_no == (Program.CurrentPatient_queue == null ? objrg.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no) //Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no
                                && t1.tpl_lab_no == "N0510"
                             select t1.tpl_lab_value).FirstOrDefault(); 
            var ObjLabLdl = (from t1 in dbc.trn_patient_labs
                             where t1.tpl_hn_no == (Program.CurrentRegis == null ? objrg.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no) //Program.CurrentRegis.trn_patient.tpt_hn_no
                             && t1.tpl_en_no == (Program.CurrentPatient_queue == null ? objrg.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no) //Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no
                             && t1.tpl_lab_no == "C0159"
                             select t1.tpl_lab_value).FirstOrDefault(); 
            var ObjLabBp = (from t1 in dbc.trn_basic_measure_dtls
                            join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                            where t2.tpr_id == tpr_id
                            select t1.tbd_systolic + "/" + t1.tbd_diastolic).FirstOrDefault();

            var ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1.tpr_mobile_phone).FirstOrDefault();
            if (ObjMobile == null || ObjMobile == "")
            {
                ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1.tpr_office_phone).FirstOrDefault();
            }
            if (ObjMobile == null || ObjMobile == "")
            {
                ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1.tpr_home_phone).FirstOrDefault();
            }

            txtfbs.Text = ObjLabFbs;
            txtcho.Text = ObjLabCholes;
            txtbmi.Text = ObjLabBmi;
            txthb.Text = ObjLabHbA;
            txtldl.Text = ObjLabLdl;
            txtbp.Text = ObjLabBp;
            txtmobilephone.Text = ObjMobile;
        }

        private void LoadData(int tpr_id)
        {
            trn_carotid_tech obj = dbc.trn_carotid_teches.Where(c => c.tpr_id == tpr_id).FirstOrDefault();
            if (obj != null)
            {
                bindingSourceTrn_carotid_Tech.DataSource = obj;

                trn_carotid_tech trnCarotid_tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                bool? isClose = trnCarotid_tech.tct_close;
                if (isClose == null)
                {
                    trnCarotid_tech.tct_close = false;
                    trnCarotid_tech.tct_close_rmk = "";
                }                
                //objimage_L = trnCarotid_tech.tct_left_result.ToArray();
                //objimage_R = trnCarotid_tech.tct_right_result.ToArray();
                //byte[] data;
                //byte[] data2;
                //data = (byte[])objimage_L;
                //data2 = (byte[])objimage_R;
                //MemoryStream ms = new MemoryStream(data);
                //MemoryStream ms2 = new MemoryStream(data2);
                //pictureBoxLeft.Image = Image.FromStream(ms);
                //pictureBoxRight.Image = Image.FromStream(ms2);
                if (trnCarotid_tech.tct_left_result != null)
                {
                    objimage_R = trnCarotid_tech.tct_right_result.ToArray();
                    byte[] data1;
                    data1 = (byte[])objimage_R;
                    MemoryStream ms1 = new MemoryStream(data1);
                    pictureBoxRight.Image = Image.FromStream(ms1);
                }
                else
                {
                    pictureBoxRight.Image = Properties.Resources.carotid_1;
                }
                if (trnCarotid_tech.tct_right_result != null)
                {
                    objimage_L = trnCarotid_tech.tct_left_result.ToArray();
                    byte[] data2;
                    data2 = (byte[])objimage_L;
                    MemoryStream ms2 = new MemoryStream(data2);
                    pictureBoxLeft.Image = Image.FromStream(ms2);
                }
                else
                {
                    pictureBoxLeft.Image = Properties.Resources.carotid_2;
                }
                Program.SetValueRadioGroup(pnlDuplex, trnCarotid_tech.tct_finding_ca_duplex.ToString());
                cmblist1.SelectedItem = trnCarotid_tech.tct_appoint_depart;
                Program.SetValueRadioGroup(pnlStatusCall, trnCarotid_tech.tct_call_status.ToString());
                Program.SetValueRadioGroup(pnlsummary, trnCarotid_tech.tct_summary.ToString());
                
                //noina comment
                string Str_Appoint_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_appoint_doctor_date.ToString()) ? "" : Convert.ToDateTime(trnCarotid_tech.tct_appoint_doctor_date).ToString();
                string Str_Followup_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_follow_up.ToString()) ? "" : Convert.ToDateTime(trnCarotid_tech.tct_follow_up).ToString();
                //dateTimePicker1.Text = Str_Appoint_Date;
                //dateTimeFollowup.Text = Str_Followup_Date;
                
                if (trnCarotid_tech.tct_appoint_doctor_date == null)
                {
                    chkappoint.Checked = false;
                    dateTimePicker1.Enabled = false;
                }
                else
                {
                    chkappoint.Checked = true;
                    dateTimePicker1.Value = (DateTime)trnCarotid_tech.tct_appoint_doctor_date;
                }

                if (trnCarotid_tech.tct_follow_up == null)
                {
                    chk_prn.Checked = false;
             //       dateTimeFollowup.Enabled = false;
                }
                else
                {
                    chk_prn.Checked = true;
                    dateTimeFollowup.Value = (DateTime)trnCarotid_tech.tct_follow_up;
                }
                txtsumaryRemark.Text = trnCarotid_tech.tct_summary_remark;
                if (trnCarotid_tech.tct_appoint_doctor == true)
                {
                    chkappoint.Checked = true;
                }
                if (trnCarotid_tech.tct_advice_med == true)
                {
                    chkAdviceMec.Checked = true;
                    txtAdviceMec.Text = trnCarotid_tech.tct_advice_med_rmk;
                }
                if (trnCarotid_tech.tct_advice_dit == true)
                {
                    chkAdviceDiet.Checked = true;
                    txtAdviceDiet.Text = trnCarotid_tech.tct_advice_dit_rmk;
                }
                if (trnCarotid_tech.tct_advice_exr == true)
                {
                    chkAdviceExercise.Checked = true;
                    txtAdviceExercise.Text = trnCarotid_tech.tct_advice_exr_rmk;
                }
                if (trnCarotid_tech.tct_consult_card == true)
                {
                    chkConsult.Checked = true;
                    txtconsult.Text = trnCarotid_tech.tct_consult_card_rmk;
                }
                if (trnCarotid_tech.tct_fu_carotid == true)
                {
                    chkfu.Checked = true;
                    txtfu.Text = trnCarotid_tech.tct_fu_carotid_rmk;
                }
                if (trnCarotid_tech.tct_prn == true)
                {
                    chk_prn.Checked = true;
                }
                txtremark.Text = trnCarotid_tech.tct_other_remark;
                Program.SetValueRadioGroup(pnlAppointStatus, trnCarotid_tech.tct_appoint_status.ToString());
                if (obj.tct_technician_code != null)
                {
                    autoCompleteUC1.SelectedValue = obj.tct_technician_code;
                }
                else
                {
                    autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                    autoCompleteUC1.Enabled = false;
                }
            }
            else
            {
                bindingSourceTrn_carotid_Tech.DataSource = (from t1 in dbc.trn_carotid_teches select t1);
                bindingSourceTrn_carotid_Tech.AddNew();
                //StatusCallQueue();
                pictureBoxLeft.Image = Properties.Resources.carotid_2;
                pictureBoxRight.Image = Properties.Resources.carotid_1;
                autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                autoCompleteUC1.Enabled = false;
            }
            

            // morn not sure
            //if (Program.CurrentPatient_queue != null)
            //{
            //    if (Program.CurrentPatient_queue.tps_status == "WK" && Program.CurrentPatient_queue.tps_ns_status == null)
            //    {
            //        StatusCallQueueReady();
            //    }
            //    else if (Program.CurrentPatient_queue.tps_status == "NS" && Program.CurrentPatient_queue.tps_ns_status == "WR")
            //    {
            //        StatusCallQueueWaitingReady();
            //    }
            //}
        }
        private void StatusWaitCallQueue()
        {
            btnReady.Enabled = false;
            btnCallQueue.Enabled = true;
            btnSendManual.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendToCheckB.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnPrintPreview.Enabled = false;
            btnPrint.Enabled = false;
            btnSendtoDocScan.Enabled = false;
        }
        private void StatusCallQueue()
        {
            if (SetTprID == 0)
            {
                btnCallQueue.Enabled = false;
                lbAlertMsg.Text = "";
                btnHold.Enabled = true;
                btnCancel.Enabled = true;
                btnSendAuto.Enabled = true;
                btnSendToCheckB.Enabled = true;
                btnSaveDraft.Enabled = true;
                btnSendManual.Enabled = true;
            }
             else
             {
                 lbAlertMsg.Text = "";
                 btnCallQueue.Enabled = false;
                 btnHold.Enabled = false;
                 btnCancel.Enabled = false;
                 btnSendManual.Enabled = false;
                 btnSaveDraft.Enabled = false;
                 btnSendAuto.Enabled = false;
                 btnSendToCheckB.Enabled = false;
             }
            btnSendtoDocScan.Enabled = false;
            btnPrintPreview.Enabled = true;
            btnPrint.Enabled = true;
        }
        private void StatusSaveData()
        {
            btnCallQueue.Enabled = false;
            lbAlertMsg.Text = "";
            btnHold.Enabled = true;
            btnCancel.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendToCheckB.Enabled = true;
            btnSaveDraft.Enabled = true;
            btnSendManual.Enabled = true;
            btnPrintPreview.Enabled = false;
            btnPrint.Enabled = false;
            btnSendtoDocScan.Enabled = false;
        }
        private void LoadUI()
        {
            uiAllLeft1.LoadDataAll();
            //uiFooter1.LoadData();
        }
        private void Clearfrm()
        {
            Program.CurrentRegis = null;
            Program.CurrentPatient_queue = null;

            trn_carotid_tech obj = new trn_carotid_tech();
            bindingSourceTrn_carotid_Tech.DataSource = obj;
            clrpanel(pnlAppointStatus);
            clrpanel(pnlDuplex);
            clrpanel(pnlStatusCall);
            clrpanel(pnlsummary);
            clrpanel(pnlAdviceMec);
            clrpanel(pnlAdviceDiet);
            clrpanel(pnlAdviceExercise);
            clrpanel(pnlConsult);
            clrpanel(pnlFu);
            clrPicture(pictureBoxLeft);
            clrPicture(pictureBoxRight);
            autoCompleteUC2.SelectedValue = null;
            txtmobilephone.Text = null;
            txtsumaryRemark.Text = "";
            txtremark.Text = "";
            chkappoint.Checked = false;
            DrawingShapes = new Usercontrols.ToolPaint.Shapes();
            DrawingShapes2 = new Usercontrols.ToolPaint.Shapes();
            pictureBoxRight.Refresh();
            pictureBoxLeft.Refresh();
            pictureBoxLeft.Image = Properties.Resources.carotid_2;
            pictureBoxRight.Image = Properties.Resources.carotid_1;
            dbc.Dispose();
            dbc = new InhCheckupDataContext();
            clrGroupbox(GBlab);
            chk_prn.Checked = false;
            chkappoint.Checked = false;
            dateTimeFollowup.Value = DateTime.Now;

            LoadUI();
        }
        private void clrpanel(Panel pnl)
        {
            foreach (Control ctl in pnl.Controls)
            {
                RadioButton rb;
                CheckBox chk;
                if (ctl is RadioButton)
                {
                    rb = (RadioButton)ctl;
                    rb.Checked = false;
                }
                if (ctl is CheckBox)
                {
                    chk = (CheckBox)ctl;
                    chk.Checked = false;
                }
            }
        }

        private bool SaveData(char type)
        {
            trn_carotid_tech Carotid_Tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
            Boolean saveIsCompleted = false;
            DateTime datenowvalue = Program.GetServerDateTime();
            if (SetTprID == 0)
            {
                Carotid_Tech.tpr_id = Program.CurrentRegis.tpr_id;
            }
            Carotid_Tech.tct_type = type;
            Carotid_Tech.tct_result = false;
            PictureBox p1 = new PictureBox();
            PictureBox p2 = new PictureBox();
            using (Bitmap bitmap = new Bitmap(pictureBoxLeft.ClientSize.Width, pictureBoxLeft.ClientSize.Height))
            {
                pictureBoxLeft.DrawToBitmap(bitmap, pictureBoxLeft.ClientRectangle);
                Bitmap bmp = new Bitmap(bitmap);
                p1.SizeMode = PictureBoxSizeMode.StretchImage;
                p1.Image = (Image)bmp;
            }
            using (Bitmap bitmap = new Bitmap(pictureBoxRight.ClientSize.Width, pictureBoxRight.ClientSize.Height))
            {
                pictureBoxRight.DrawToBitmap(bitmap, pictureBoxRight.ClientRectangle);
                Bitmap bmp = new Bitmap(bitmap);
                p2.SizeMode = PictureBoxSizeMode.StretchImage;
                p2.Image = (Image)bmp;
            }
            MemoryStream stream_L = new MemoryStream();
            MemoryStream stream_R = new MemoryStream();
            p1.Image.Save(stream_L, System.Drawing.Imaging.ImageFormat.Jpeg);
            p2.Image.Save(stream_R, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic_L = stream_L.ToArray();
            byte[] pic_R = stream_R.ToArray();
            Carotid_Tech.tct_left_result = pic_L;
            Carotid_Tech.tct_right_result = pic_R;
            Carotid_Tech.tct_finding_ca_duplex = Program.GetValueRadio(pnlDuplex);
            if (rdresult1.Checked == true)
            {
                Carotid_Tech.tct_appoint_depart = cmblist1.SelectedItem.ToString();
            }
            else
            {
                Carotid_Tech.tct_appoint_depart = DBNull.Value.ToString();
            }
            Carotid_Tech.tct_call_status = Program.GetValueRadioTochar(pnlStatusCall);
            Carotid_Tech.tct_appoint_doctor = (chkappoint.Checked) ? true : false;

            if (chkappoint.Checked == true) { Carotid_Tech.tct_appoint_doctor_date = dateTimePicker1.Value; }
             //dt_appoint;
                        
            Carotid_Tech.tct_appoint_status = Program.GetValueRadio(pnlAppointStatus);            
            Carotid_Tech.tct_summary = Program.GetValueRadioTochar(pnlsummary);
            Carotid_Tech.tct_summary_remark = txtsumaryRemark.Text;
            Carotid_Tech.tct_advice_med = (chkAdviceMec.Checked) ? true : false;
            Carotid_Tech.tct_advice_med_rmk = txtAdviceMec.Text;
            Carotid_Tech.tct_advice_dit = (chkAdviceDiet.Checked) ? true : false;
            Carotid_Tech.tct_advice_dit_rmk = txtAdviceDiet.Text;
            Carotid_Tech.tct_advice_exr = (chkAdviceExercise.Checked) ? true : false;
            Carotid_Tech.tct_advice_exr_rmk = txtAdviceExercise.Text;
            Carotid_Tech.tct_consult_card = (chkConsult.Checked) ? true : false;
            Carotid_Tech.tct_consult_card_rmk = txtconsult.Text;
            Carotid_Tech.tct_fu_carotid = (chkfu.Checked) ? true : false;
            Carotid_Tech.tct_fu_carotid_rmk = txtfu.Text;
            Carotid_Tech.tct_other_remark = txtremark.Text;
            Carotid_Tech.tct_follow_up = dateTimeFollowup.Value;
          //  if (chk_prn.Checked == true) { Carotid_Tech.tct_follow_up = dateTimeFollowup.Value; }
             //dt_followup; 

            Carotid_Tech.tct_prn = (chk_prn.Checked) ? true : false;

            Carotid_Tech.tct_telephone = txtmobilephone.Text; //noina

            string createby = Carotid_Tech.tct_create_by;
            if (createby == null)
            {
                Carotid_Tech.tct_create_by = Program.CurrentUser.mut_username;
                Carotid_Tech.tct_create_date = datenowvalue;
            }
            Carotid_Tech.tct_update_by =  Program.CurrentUser.mut_username;
            Carotid_Tech.tct_update_date = datenowvalue;
            bindingSourceTrn_carotid_Tech.EndEdit();
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
            saveIsCompleted = true;
            if (saveIsCompleted == true)
            {
                var tctid = (from q in dbc.trn_carotid_teches select q.tct_id).Max();
            }
            return saveIsCompleted;
        }

        private void RemarkOnImgage(PictureBox pb, MouseEventArgs e , string side)
        {
            count++;
            frmPopupRemark frm = new frmPopupRemark();
            frm.ShowDialog();
            Label lbl = new Label();
            lbl = new Label();
            lbl.Name = "l1" + count;
            lbl.BackColor = Color.MintCream;
            lbl.Left = Convert.ToInt32(e.X);
            lbl.Top = Convert.ToInt32(e.Y);
            lbl.AutoSize = true;
            lbl.Cursor = Cursors.Hand;
            lbl.Width = 20;
            lbl.Height = 10;
            lbl.Text = frm.strTextValue;
            lbl.Tag = lbl.Name;
            pb.Controls.Add(lbl);
            if (side == "L")
                lbl.MouseClick += new MouseEventHandler(lblremark_L_Click);
            if (side == "R")
                lbl.MouseClick += new MouseEventHandler(lblremark_R_Click);
        }

        private void ReverseIt()
        {
            lbAlertMsg.Focus();
            lbAlertMsg.Text = "";
            if (Program.CurrentRegis != null)
            {
                LoadData(Program.CurrentRegis.tpr_id);
            }
        }
        #endregion

        private void frmCarotid_2_Load(object sender, EventArgs e)
        {
            LoadHandlerCountDown();

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            Loadfrm();
            this.Text = Program.GetRoomName();
            uiMenuBar1.LoadEnableCarotidHistory();//แสดงปุ่ม Patient List Menubar
            //dateTimePicker1.MinDate = DateTime.Today; dateTimeFollowup.MinDate = DateTime.Today;
            CreateHeader();

            frmbg.Close();
        }

        private void btnBrushErase_Click(object sender, EventArgs e)
        {
            Brush = !Brush;
            Brush2= !Brush2;
            if (Brush == true & Brush2 == true)
                lblstatusPaint.Text = "Brush";
            else
                lblstatusPaint.Text = "Erase";
        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                CurrentColour = colorDialog1.Color;
                CurrentColour2 = colorDialog1.Color;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CurrentWidth = Convert.ToSingle(numericUpDown1.Value);
            CurrentWidth2 = Convert.ToSingle(numericUpDown1.Value);
        }

        private void btnClearall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DrawingShapes = new Usercontrols.ToolPaint.Shapes();
                DrawingShapes2 = new Usercontrols.ToolPaint.Shapes();
                pictureBoxRight.Refresh();
                pictureBoxLeft.Refresh();
                pictureBoxLeft.Image = Properties.Resources.carotid_2;
                pictureBoxRight.Image = Properties.Resources.carotid_1;
                clrPicture(pictureBoxLeft);
                clrPicture(pictureBoxRight);
            }
        }

        private void pnlpictureBoxLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (Brush)
            {
                IsPainting = true;
                ShapeNum++;
                LastPos = new Point(0, 0);
            }
            else
                IsEraseing = true;
        }
        
        private void pictureBoxRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (Brush2)
            {
                IsPainting2 = true;
                ShapeNum2++;
                LastPos2 = new Point(0, 0);
            }
            else
                IsEraseing2 = true;
        }

        private void pictureBoxRight_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLoc2 = e.Location;
            if (IsPainting2)
            {
                if (LastPos2 != e.Location)
                {
                    LastPos2 = e.Location;
                    DrawingShapes2.NewShape(LastPos2, CurrentWidth2, CurrentColour2, ShapeNum2);
                }
            }
            else if (IsEraseing2)
            {
                DrawingShapes2.RemoveShape(e.Location, 10);
            }
            pictureBoxRight.Refresh();
        }

        private void pictureBoxRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsPainting2)
                IsPainting2 = false;
            if (IsEraseing2)
                IsEraseing2 = false;
        }

        private void pictureBoxRight_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < DrawingShapes2.NumberOfShapes() - 1; i++)
            {
                Usercontrols.ToolPaint.Shape T = DrawingShapes2.GetShape(i);
                Usercontrols.ToolPaint.Shape T1 = DrawingShapes2.GetShape(i + 1); 
                if (T.ShapeNumber == T1.ShapeNumber)
                {
                    Pen p = new Pen(T.Colour, T.Width);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(p, T.Location, T1.Location);
                    p.Dispose();
                }
            }
            if (IsMouseing)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 0.5f), MouseLoc2.X - (CurrentWidth2 / 2), MouseLoc2.Y - (CurrentWidth2 / 2), CurrentWidth2, CurrentWidth2);
            }
        }

        private void pictureBoxRight_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
            IsMouseing = true;
        }

        private void pictureBoxRight_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            IsMouseing = false;
            pictureBoxRight.Refresh();
        }

        private void pictureBoxLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (Brush)
            {
                IsPainting = true;
                ShapeNum++;
                LastPos = new Point(0, 0);
            }
            else
                IsEraseing = true;
        }

        private void pictureBoxLeft_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
            IsMouseing = true;
        }

        private void pictureBoxLeft_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            IsMouseing = false;
            pictureBoxLeft.Refresh();
        }

        private void pictureBoxLeft_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLoc = e.Location;
            if (IsPainting)
            {
                if (LastPos != e.Location)
                {
                    LastPos = e.Location;
                    DrawingShapes.NewShape(LastPos, CurrentWidth, CurrentColour, ShapeNum);
                }
            }
            if (IsEraseing)
            {
                DrawingShapes.RemoveShape(e.Location, 10);
            }
            pictureBoxLeft.Refresh();
        }

        private void pictureBoxLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsPainting)
                IsPainting = false;
            if (IsEraseing)
                IsEraseing = false;
        }

        private void pictureBoxLeft_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < DrawingShapes.NumberOfShapes() - 1; i++)
            {
                Usercontrols.ToolPaint.Shape T = DrawingShapes.GetShape(i);
                Usercontrols.ToolPaint.Shape T1 = DrawingShapes.GetShape(i + 1); 
                if (T.ShapeNumber == T1.ShapeNumber)
                {
                    Pen p = new Pen(T.Colour, T.Width);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(p, T.Location, T1.Location);
                    p.Dispose();
                }
            }
            if (IsMouseing)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 0.5f), MouseLoc.X - (CurrentWidth / 2), MouseLoc.Y - (CurrentWidth / 2), CurrentWidth, CurrentWidth);
            }
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
                        uiAllLeft1.LoadDataAll();
                        clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                        try
                        {
                            //uiFooter1.LoadData();
                            LoadData(Program.CurrentRegis.tpr_id);
                            LoadHistory(Program.CurrentRegis.tpr_id);
                            tabControl1.Enabled = true;
                            Loadfrm();
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError("frmCarotid_2", "btnCallQueue_Click", ex, false);
                        }
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        Clearfrm();
                        StatusWaitCallQueue();
                        //bindingSourceTrn_carotid_Tech.DataSource = (from t1 in dbc.trn_carotid_teches select t1);
                        //bindingSourceTrn_carotid_Tech.AddNew();
                        lbAlertMsg.Text = "No patient on queue!";
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

            lbAlertMsg.Focus();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

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

            //    Clearfrm();
            //    StatusWaitCallQueue();
            //    tabControl1.SelectedTab = t1;
            //    tabControl1.Enabled = false;
            //    lbAlertMsg.Text = string.Format(Program.MsgHold, QueueNo);
            //}
            //else if (result == StatusTransaction.Error)
            //{
            //    btnHold.Enabled = true;
            //    lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
            //}
            frmbg.Close();
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
                    Clearfrm();
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
            
            //lblAlert.Focus();
            //FrmReason frm = new FrmReason();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    // morn clear Unit Display
            //    ClsTCPClient clsTCP = new ClsTCPClient();
            //    clsTCP.SendClearUnitDisplay();
            //    // morn clear Unit Display

            //    string QueueNo = Program.CurrentRegis.tpr_queue_no;
            //    //ทำการ cancel queue และ ส่งไปยังห้องถัดไป (หากมีข้อมูล)
            //    CallQueue.P_CallCancel(frm.strReason, frm.strReasonOther);
            //    //ทำการอัพเดต patient plan ให้มีสถาะนะเป็น C
            //    //เช็คว่ามีห้องถัดไปหรือไม่
            //    if (CallQueue.GetIsHaveNextRoom == true)
            //    {
            //        string msgAlert = "";
            //        msgAlert = CallQueue.GetStrCancelAndSend();
            //        lblAlert.Text = msgAlert;
            //        StatusWaitCallQueue();
            //    }
            //    else
            //    {
            //        StatusWaitCallQueue();
            //        if (Program.CurrentRegis == null)
            //        {
            //            lblAlert.Text = string.Format(Program.MsgCancel, QueueNo);
            //        }
            //    }
            //}
        }

        private void rdresult1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdresult1.Checked == true)
            {
                cmblist1.Enabled = true;
                cmblist1.SelectedIndex = 0;
            }
            else
            {
                cmblist1.Enabled = false;
                cmblist1.SelectedIndex = -1;
            }
        }

        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            try
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
                    if (SaveData('D'))
                    {
                        lbAlertMsg.Focus();
                        lbAlertMsg.Text = "Save Data Completed.";
                        if (siteitem == 0)
                        {
                            if (Program.CurrentRegis != null)
                            {
                                LoadHistory(Program.CurrentRegis.tpr_id);
                                btnGet_hst_Click(null, null);
                            }
                        }
                        btnSendtoDocScan.Enabled = true;
                    }
                    else
                    {
                        lbAlertMsg.Focus();
                        lbAlertMsg.Text = "Save Data Not Completed.";
                    }
                }
            }
            catch (Exception ex)
            {
                lbAlertMsg.Focus();
                lbAlertMsg.Text = ex.Message;
            }
        }

        private void btnSendManual_Click(object sender, EventArgs e)
        {
            try
            {
                disableBtnWhenSave();
                lbAlertMsg.Text = "";

                this.AutoScrollPosition = new Point(0, 0);
                int tpr_id = Program.CurrentRegis.tpr_id;
                int tps_id = Program.CurrentPatient_queue.tps_id;

                string messegeAlert = "";

                if (SaveData('N'))
                {
                    StatusTransaction result = new SendManaulCls().SendManualOnStation(ref messegeAlert);
                    if (result == StatusTransaction.True)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendManual,
                                                    tpr_id,
                                                    tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);

                        new ClsTCPClient().sendClearUnitDisplay();
                        Clearfrm();
                        StatusWaitCallQueue();
                        tabControl1.Enabled = false;
                        new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                        lbAlertMsg.Text = messegeAlert;
                    }
                    else if (result == StatusTransaction.False)
                    {
                        enableBtnWhenSave();
                        //ReverseIt();
                    }
                    else if (result == StatusTransaction.Error)
                    {
                        enableBtnWhenSave();
                        lbAlertMsg.Text = "กรุณา send manual อีกครั้ง";
                    }
                }
                else
                {
                    enableBtnWhenSave();
                }
            }
            catch (Exception ex)
            {
                enableBtnWhenSave();
                lbAlertMsg.Text = ex.Message;
            }
        }

        private void disableBtnWhenSave()
        {
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendManual.Enabled = false;
            btnSendToCheckB.Enabled = false;
        }
        private void enableBtnWhenSave()
        {
            btnSaveDraft.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendManual.Enabled = true;
            btnSendToCheckB.Enabled = true;
        }
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            try
            {
                this.AutoScrollPosition = new Point(0, 0);
                disableBtnWhenSave();
                DateTime startDate = DateTime.Now;

                Class.FunctionDataCls func = new Class.FunctionDataCls();
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                int tpr_id = Program.CurrentRegis.tpr_id;
                int tps_id = Program.CurrentPatient_queue.tps_id;
                if (SaveData('N'))
                {
                    if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
                    {
                        List<int> mvt = mst.GetMstRoomEventByMrm(Program.CurrentRoom.mrm_id).Select(x => x.mvt_id).ToList();

                        Class.FunctionDataCls.sendQueueStatus result = func.sendQueueUltrasoundLower(resultUltrasound, mvt);
                        if (result == Class.FunctionDataCls.sendQueueStatus.error)
                        {
                            lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถส่งไป ultrasound ได้ กรุณาติดต่อผู้ดูแลระบบ";
                            enableBtnWhenSave();
                        }
                        else if (result == Class.FunctionDataCls.sendQueueStatus.sendSuccess)
                        {
                            new ClsTCPClient().sendClearUnitDisplay();
                            Clearfrm();
                            StatusWaitCallQueue();
                            lbAlertMsg.Text = func.GetStrSaveAndSend(tpr_id, "US", "UL");
                        }
                    }
                    else
                    {
                        if (new Class.FunctionDataCls().ChkSendAutoNewModule(Program.CurrentRegis))
                        {
                            string msgAlert = "";
                            bool isPopup = false;
                            QueueClass.SendAutoCls.ResultSendQueue result = new QueueClass.SendAutoCls().SendAuto(tps_id, Program.CurrentUser, ref msgAlert, ref isPopup);
                            if (result == QueueClass.SendAutoCls.ResultSendQueue.Error)
                            {
                                lbAlertMsg.Text = msgAlert;
                                enableBtnWhenSave();
                            }
                            else
                            {
                                lbAlertMsg.Visible = true;
                                if (result == QueueClass.SendAutoCls.ResultSendQueue.SendComplete)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                                tpr_id,
                                                                tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username,
                                                                startDate);

                                    new ClsTCPClient().sendClearUnitDisplay();
                                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                    Clearfrm();
                                    StatusWaitCallQueue();
                                    if (isPopup)
                                    {
                                        MessageBox.Show(msgAlert, "EMR Checkup.");
                                    }
                                    else
                                    {
                                        lbAlertMsg.Text = msgAlert;
                                    }
                                }
                                else
                                {
                                    if (isPopup)
                                    {
                                        MessageBox.Show(msgAlert, "EMR Checkup.");
                                    }
                                    else
                                    {
                                        lbAlertMsg.Text = msgAlert;
                                    }
                                }
                            }
                        }
                        else
                        {
                            StatusTransaction result = CallQueue.SendAutoOnStation();
                            if (result == StatusTransaction.True || result == StatusTransaction.SendCheckB)
                            {
                                new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                            tpr_id,
                                                            tps_id,
                                                            Program.CurrentSite.mhs_id,
                                                            Program.CurrentRoom.mrd_ename,
                                                            Program.CurrentUser.mut_username,
                                                            startDate);
                                
                                new ClsTCPClient().sendClearUnitDisplay();
                                Clearfrm();
                                StatusWaitCallQueue();
                                new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                lbAlertMsg.Text = func.getStringGotoNextRoom((int)tpr_id);
                            }
                            else if (result == StatusTransaction.Error)
                            {
                                enableBtnWhenSave();
                                lbAlertMsg.Text = "โปรดกด Send Auto อีกครั้ง";
                            }
                            else if (result == StatusTransaction.False)
                            {
                                enableBtnWhenSave();
                                lbAlertMsg.Text = "Save Data Complete.";
                            }
                        }
                    }
                }
                else
                {
                    lbAlertMsg.Focus();
                    lbAlertMsg.Text = "Save Data Not Completed";
                    enableBtnWhenSave();
                }
            }
            catch (Exception ex) 
            { 
                lbAlertMsg.Focus();
                lbAlertMsg.Text = ex.Message;
                enableBtnWhenSave();
            }
        }

        private void rdnormal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdnormal.Checked == true)
            {
                txtsumaryRemark.Enabled = true;
                txtsumaryRemark.Focus();
            }
        }

        private void rdabnormal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdabnormal.Checked == true)
            {
                txtsumaryRemark.Enabled = true;
                txtsumaryRemark.Focus();
            }
        }

        private void chkAdviceMec_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdviceMec.Checked == true)
            {
                txtAdviceMec.Enabled = true;
                txtAdviceMec.Focus();
            }
            else
            {
                txtAdviceMec.Enabled = false;
                txtAdviceMec.Text = null;
            }
        }

        private void chkAdviceDiet_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdviceDiet.Checked == true)
            {
                txtAdviceDiet.Enabled = true;
                txtAdviceDiet.Focus();
            }
            else
            {
                txtAdviceDiet.Enabled = false;
                txtAdviceDiet.Text = null;
            }
        }

        private void chkAdviceExercise_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdviceExercise.Checked == true)
            {
                txtAdviceExercise.Enabled = true;
                txtAdviceExercise.Focus();
            }
            else
            {
                txtAdviceExercise.Enabled = false;
                txtAdviceExercise.Text = null;
            }
        }

        private void chkConsult_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkConsult.Checked == true)
            {
                txtconsult.Enabled = true;
                txtconsult.Focus();
            }
            else
            {
                txtconsult.Enabled = false;
                txtconsult.Text = null;
            }
        }

        private void chkfu_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkfu.Checked == true)
            {
                txtfu.Enabled = true;
                txtfu.Focus();
            }
            else
            {
                txtfu.Enabled = false;
                txtfu.Text = null;
            }
        }

        private void pictureBoxLeft_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsRemark_L == true && Brush == true)
                RemarkOnImgage(this.pictureBoxLeft, e, "L");
        }

        private void pictureBoxRight_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsRemark_R == true && Brush2 == true)
                RemarkOnImgage(this.pictureBoxRight, e, "R");
        }

        private void lblremark_L_Click(object sender, EventArgs e)
        {
            Label itemdata = (Label)sender;
            strTagID_L = itemdata.Tag.ToString();
            pictureBoxLeft.Controls.RemoveByKey(strTagID_L);
        }

        private void lblremark_R_Click(object sender, EventArgs e)
        {
            Label itemdata = (Label)sender;
            strTagID_R = itemdata.Tag.ToString();
            pictureBoxRight.Controls.RemoveByKey(strTagID_R);
        }

        private void Gv_History_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Gv_History.ClearSelection();
            int indexrow = 1;
            for (int i = 0; i <= Gv_History.Rows.Count - 1; i++)
            {
                Gv_History.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void chkappoint_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkappoint.Checked == true)
            {
             //   dateTimePicker1.Enabled = true;
                //dateTimePicker1.Format = DateTimePickerFormat.Custom;
                //dateTimePicker1.CustomFormat = "dd/MM/yyyy";
                //dt_appoint = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            }
            else
            {
               // dateTimePicker1.Enabled = false;
                //dateTimePicker1.Format = DateTimePickerFormat.Custom;
                //dateTimePicker1.CustomFormat = " ";
                //dt_appoint = null;
            }
        }

        private void chk_prn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_prn.Checked == true)
            {
              //  dateTimeFollowup.Enabled = true;
                //dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                //dateTimeFollowup.CustomFormat = "dd/MM/yyyy";
                //dt_followup = Convert.ToDateTime(dateTimeFollowup.Value.ToShortDateString());
            }
            else
            {
              //  dateTimeFollowup.Enabled = false;
                //dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                //dateTimeFollowup.CustomFormat = " ";
                //dt_followup = null;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dt_appoint = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
        }

        private void dateTimeFollowup_ValueChanged(object sender, EventArgs e)
        {
            //dt_followup = Convert.ToDateTime(dateTimeFollowup.Value.ToShortDateString());
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string> { "CD101" };
            int tprID = 0;
            if (SetTprID != 0)
            {
                tprID = SetTprID;
            }
            else
            {
                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
            }
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.previewReport();
            //ClsReport.previewRpt(new List<string> { "CD101" });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string> { "CD101" };
            int tprID = 0;
            if (SetTprID != 0)
            {
                tprID = SetTprID;
            }
            else
            {
                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
            }
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.printReport();
            //ClsReport.printRpt(new List<string> { "CD101" });
        }

        private void btnremark_Click(object sender, EventArgs e)
        {
            IsRemark_L = !IsRemark_L;
            IsRemark_R = !IsRemark_R;
            if (IsRemark_L == true && IsRemark_R == true)
                lblS_remark.Text = "Remark";
            else
                lblS_remark.Text = "Null";
        }

        private void btnGet_hst_Click(object sender, EventArgs e)
        {
            LoadHistory(Program.CurrentRegis.tpr_id);
        }

        private void btnCarotidReport_Click(object sender, EventArgs e)
        {

        }

        private void btnSendtoDocScan_Click(object sender, EventArgs e)
        {
            try
            {
                string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "CD101", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                lbAlertMsg.Text = result;

                //if (docscan.SendtoDocscan("CD101", Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                //{
                //    lbAlertMsg.Text = HistoryData.savestatus;
                //    LoadHistory(Program.CurrentRegis.tpr_id);
                //}
                //else
                //{
                //    lbAlertMsg.Text = "Cannot send to docscan user authentication failed";
                //}
            }
            catch 
            {
                return;
            }
        }

        DocScan docscan = new DocScan();
        private void Gv_History_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Gv_History.Rows.Count > 0 && Gv_History.Columns[e.ColumnIndex].Name == "Column13")
                {
                    string Envalue;
                    Envalue = Gv_History.Rows[e.RowIndex].Cells["ColEn"].Value.ToString();
                    int tprid = (from t in dbc.trn_patient_regis where t.tpr_en_no == Envalue select t.tpr_id).FirstOrDefault();
                    var coutItem = dbc.pw_Get_DocscanStatus(tprid, "CD101", Envalue).FirstOrDefault();
                    if (coutItem.Column1 == 0)
                    {
                        string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "CD101", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                        lbAlertMsg.Text = result;

                        //if (docscan.SendtoDocscan("CD101", tprid, Envalue, Program.getCurrentCareProvider))
                        //{
                        //    lbAlertMsg.Text = HistoryData.savestatus;
                        //    HistoryData.showform = 'Y';
                        //    docscan.GetHistory("CD101", Envalue, tprid);
                        //    LoadHistory(Program.CurrentRegis.tpr_id);
                        //}
                        //else
                        //{
                        //    lbAlertMsg.Text = "Cannot send to docscan user authentication failed";
                        //}
                    }
                    else////when it send to docscan completed
                    {
                        HistoryData.showform = 'Y';
                        docscan.GetHistory("CD101", Envalue, tprid);
                        //////Other docscan (Unofficial)
                        ////var coutItemDocscan = dbc.pw_Get_DocscanStatus(tprid, "CD102", Envalue).FirstOrDefault();
                        ////if(coutItemDocscan.Column1 !=0)
                        ////    docscan.GetHistory("CD102", Envalue, tprid);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void Gv_History_Paint(object sender, PaintEventArgs e)
        {
            Usercontrols.ToolPaint paint = new Usercontrols.ToolPaint();
            paint.PaintOnGridview(e, this.Gv_History);
        }

        private void CreateHeader()
        {
            this.Gv_History.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.Gv_History.ColumnHeadersHeight = this.Gv_History.ColumnHeadersHeight * 2;
            this.Gv_History.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
        }

        private void btnSendToCheckB_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            lbAlertMsg.Text = "";

            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            if (SaveData('N'))
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int tps_id = Program.CurrentPatient_queue.tps_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                string messege = "";
                StatusTransaction sendToB = new Class.SendQueue().SendToCheckB(tpr_id, mrm_id, ref messege);
                if (sendToB == StatusTransaction.True)
                {
                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendCheckB,
                                                tpr_id,
                                                tps_id,
                                                Program.CurrentSite.mhs_id,
                                                Program.CurrentRoom.mrd_ename,
                                                Program.CurrentUser.mut_username);

                    new ClsTCPClient().sendClearUnitDisplay();
                    Clearfrm();
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = messege;
                }
                else if (sendToB == StatusTransaction.Error)
                {
                    enableBtnWhenSave();
                    lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถส่งไปยัง Checkpoint B ได้ กรุณา กดปุ่ม go to Checkpoint B อีกครั้ง";
                }
            }
            else
            {
                enableBtnWhenSave();
            }

            frmbg.Close();
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
                        Clearfrm();
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = func.GetStrSaveAndSend(tpr_id, "US", "UL");
                    }
                }
                else if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
                {
                    btnSendManual.Enabled = false;
                    btnSendToCheckB.Enabled = false;
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
            btnCancel.Enabled = true;
            btnSendManual.Enabled = true;
            btnSaveDraft.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendToCheckB.Enabled = true;
            btnSendtoDocScan.Enabled = false;
            btnPrintPreview.Enabled = true;
            btnPrint.Enabled = true;
        }

        private void StatusCallQueueWaitingReady()
        {
            btnReady.Enabled = true;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;
            btnSendManual.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendToCheckB.Enabled = false;
            btnSendtoDocScan.Enabled = false;
            btnPrintPreview.Enabled = false;
            btnPrint.Enabled = false;
        }

        #endregion

        private void frmCarotid_2_FormClosed(object sender, FormClosedEventArgs e)
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
                    uiAllLeft1.LoadDataAll();
                    clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                    try
                    {
                        //uiFooter1.LoadData();
                        LoadData(Program.CurrentRegis.tpr_id);
                        LoadHistory(Program.CurrentRegis.tpr_id);
                        tabControl1.Enabled = true;
                        Loadfrm();
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("frmCarotid_2", "uiAllLeft1_OnWaitingSuccessProcess", ex, false);
                    }
                    StatusCallQueueWaitingReady();
                }
                else
                {
                    Clearfrm();
                    StatusWaitCallQueue();
                    //bindingSourceTrn_carotid_Tech.DataSource = (from t1 in dbc.trn_carotid_teches select t1);
                    //bindingSourceTrn_carotid_Tech.AddNew();
                    lbAlertMsg.Text = "No patient on queue!";
                }

                frmbg.Close();
            }
        }

    }
}