using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.IO;
using System.Runtime.InteropServices;
using BKvs2010.EmrClass;
using BKvs2010.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace BKvs2010
{
    /// <summary>
    /// view history of patient carotid artery in accordance with doctor code signin
    /// </summary>
    public partial class frmCarotidReport : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        int tprid = 0,count;
        int tpr_id = 0;
        object objimage_L;
        object objimage_R;
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
        private int ShapeNum2 = 0;
        private Point MouseLoc2 = new Point(0, 0);
        DateTime? dt_appoint;
        DateTime? dt_followup, request_date, report_date; 
        string strTagID_L, strTagID_R, mdrid, en;
        private bool IsRemark_L = true;
        private bool IsRemark_R = true;
        int countpage = 0;
        AutoCompleteDoctor obj = new AutoCompleteDoctor();

        public frmCarotidReport()
        {
            InitializeComponent();
            pictureBoxLeft.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pictureBoxLeft, new object[] { System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true });
            pictureBoxRight.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pictureBoxRight, new object[] { System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true });

            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
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
                trn_carotid_hdr carotid = (trn_carotid_hdr)bindingSourceCarotid.Current;
                if (carotid != null)
                {
                    if (e == null)
                    {
                        carotid.tch_doctor_code = null;
                        carotid.tch_doctor_license = null;
                        carotid.tch_doctor_name_en = null;
                        carotid.tch_doctor_name_th = null;
                    }
                    else
                    {
                        carotid.tch_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        carotid.tch_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        carotid.tch_doctor_name_en = dn.NameEN;
                        carotid.tch_doctor_name_th = dn.NameTH;
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

        #region Save_SendDoc
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                trn_carotid_hdr Carotid_hdr = (trn_carotid_hdr)bindingSourceCarotid.Current;
                Boolean saveIsCompleted = false;
                DateTime datenowvalue = Program.GetServerDateTime();
                Carotid_hdr.tpr_id = tprid;
                ////Report Carotid Duplex
                Carotid_hdr.tch_patient_result = txtpatientOrderResult.Text;
                Carotid_hdr.tch_patient_comt = txtComment.Text;
                Carotid_hdr.tch_result = txtResult.Text;
                Carotid_hdr.tch_request_date = request_date;
                Carotid_hdr.tch_report_date = report_date;
                ////EndReport Carotid Duplex
                if (chk_tch_consult_doc.Checked == true)
                {
                    Carotid_hdr.mdr_id = Convert.ToInt32(mdrid);
                }
                if (chk_tch_consult_doc.Checked == false)
                {
                    Carotid_hdr.mdr_id = null;
                }
                Carotid_hdr.tch_type = 'N';
                Carotid_hdr.tch_artery_wall_abnormal = Program.GetValueRadioTochar(pnlPre);
                Carotid_hdr.tch_wall_limestone = Program.GetValueRadioTochar(panel4);
                //Yee add 24 /07/2013
                Carotid_hdr.tch_wall_right_ica = SumString(txt_tch_wall_right_ica1.Text,txt_tch_wall_right_ica2.Text,txt_tch_wall_right_ica3.Text);
                Carotid_hdr.tch_wall_right_eca = SumString(txt_tch_wall_right_eca1.Text, txt_tch_wall_right_eca2.Text, txt_tch_wall_right_eca3.Text);
                Carotid_hdr.tch_wall_right_cca = SumString(txt_tch_wall_right_cca1.Text, txt_tch_wall_right_cca2.Text, txt_tch_wall_right_cca3.Text);
                Carotid_hdr.tch_wall_right_va = SumString(txt_tch_wall_right_va1.Text, txt_tch_wall_right_va2.Text, txt_tch_wall_right_va3.Text);

                Carotid_hdr.tch_wall_left_ica = SumString(txt_tch_wall_left_ica1.Text, txt_tch_wall_left_ica2.Text, txt_tch_wall_left_ica3.Text);
                Carotid_hdr.tch_wall_left_eca = SumString(txt_tch_wall_left_eca1.Text, txt_tch_wall_left_eca2.Text, txt_tch_wall_left_eca3.Text);
                Carotid_hdr.tch_wall_left_cca = SumString(txt_tch_wall_left_cca1.Text, txt_tch_wall_left_cca2.Text, txt_tch_wall_left_cca3.Text);
                Carotid_hdr.tch_wall_left_va = SumString(txt_tch_wall_left_va1.Text, txt_tch_wall_left_va2.Text, txt_tch_wall_left_va3.Text);
                
                Carotid_hdr.tch_stes_right_ica_txt = txt_tch_stes_right_ica_txt.Text;
                Carotid_hdr.tch_stes_right_eca_txt = txt_tch_stes_right_eca_txt.Text;
                Carotid_hdr.tch_stes_right_cca_txt = txt_tch_stes_right_cca_txt.Text;
                Carotid_hdr.tch_stes_right_va_txt = txt_tch_stes_right_va_txt.Text;
                Carotid_hdr.tch_stes_left_ica_txt = txt_tch_stes_left_ica_txt.Text;
                Carotid_hdr.tch_stes_left_eca_txt = txt_tch_stes_left_eca_txt.Text;
                Carotid_hdr.tch_stes_left_cca_txt = txt_tch_stes_left_cca_txt.Text;
                Carotid_hdr.tch_stes_left_va_txt = txt_tch_stes_left_va_txt.Text;
                Carotid_hdr.tch_stok_right_ica_txt = txt_tch_stok_right_ica_txt.Text;
                Carotid_hdr.tch_stok_right_eca_txt = txt_tch_stok_right_eca_txt.Text;
                Carotid_hdr.tch_stok_right_cca_txt = txt_tch_stok_right_cca_txt.Text;
                Carotid_hdr.tch_stok_right_va_txt = txt_tch_stok_right_va_txt.Text;
                Carotid_hdr.tch_stok_left_ica_txt = txt_tch_stok_left_ica_txt.Text;
                Carotid_hdr.tch_stok_left_eca_txt = txt_tch_stok_left_eca_txt.Text;
                Carotid_hdr.tch_stok_left_cca_txt = txt_tch_stok_left_cca_txt.Text;
                Carotid_hdr.tch_stok_left_va_txt = txt_tch_stok_left_va_txt.Text;

                //
                Carotid_hdr.tch_stenosis_abnormal = Program.GetValueRadioTochar(pnl_tch_stenosis_abnormal);
                Carotid_hdr.tch_stes_right_ica = (chk_tch_stes_right_ica.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stes_right_eca = (chk_tch_stes_right_eca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stes_right_cca = (chk_tch_stes_right_cca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stes_right_va = (chk_tch_stes_right_va.Checked) ? 'Y' : 'N'; 

                Carotid_hdr.tch_stes_left_ica = (chk_tch_stes_left_ica.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stes_left_eca = (chk_tch_stes_left_eca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stes_left_cca = (chk_tch_stes_left_cca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stes_left_va = (chk_tch_stes_left_va.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_storke_abnormal = Program.GetValueRadioTochar(pnl_tch_storke_abnormal);
                Carotid_hdr.tch_stok_right_ica = (chk_tch_stok_right_ica.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stok_right_eca = (chk_tch_stok_right_eca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stok_right_cca = (chk_tch_stok_right_cca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stok_right_va = (chk_tch_stok_right_va.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stok_left_ica = (chk_tch_stok_left_ica.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stok_left_eca = (chk_tch_stok_left_eca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stok_left_cca = (chk_tch_stok_left_cca.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_stok_left_va = (chk_tch_stok_left_va.Checked) ? 'Y' : 'N'; 
                Carotid_hdr.tch_summary_result = Program.GetValueRadioTochar(pnl_tch_summary_result);
                Carotid_hdr.tch_consult_doc = (chk_tch_consult_doc.Checked) ? 'Y' : 'N';

                Carotid_hdr.tch_create_by = Program.CurrentUser.mut_username;
                Carotid_hdr.tch_create_date = datenowvalue;
                Carotid_hdr.tch_update_by = Program.CurrentUser.mut_username;
                Carotid_hdr.tch_update_date = Carotid_hdr.tch_create_date; ;
                bindingSourceCarotid.EndEdit();
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
                    lblAlert.Focus();
                    lblAlert.Text = "Save data completed.";
                    ////update tct_result in table trn_carotid_tech
                    trn_carotid_tech Carotid_Tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                    trn_carotid_tech obj = dbc.trn_carotid_teches.Where(c => c.tpr_id == tprid).FirstOrDefault();
                    bindingSourceTrn_carotid_Tech.DataSource = obj;
                    Carotid_Tech.tct_result = true;
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
                    if (chkappoint.Checked == true)
                    {
                        Carotid_Tech.tct_appoint_doctor_date = dt_appoint;
                    }
                    else if(chkappoint.Checked == false)
                    {
                        dt_appoint = null;
                        Carotid_Tech.tct_appoint_doctor_date = dt_appoint;
                    }
                    //Carotid_Tech.tct_appoint_doctor_date = dt_appoint;
                    
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
                    Carotid_Tech.tct_prn = (chk_prn.Checked) ? true : false;
                    Carotid_Tech.tct_create_by = Program.CurrentUser.mut_username;
                    Carotid_Tech.tct_create_date = datenowvalue;
                    Carotid_Tech.tct_update_by = Program.CurrentUser.mut_username;
                    Carotid_Tech.tct_update_date = Carotid_Tech.tct_create_date;
                    Carotid_Tech.tct_close = ch_Close.Checked;
                    bindingSourceTrn_carotid_Tech.EndEdit();
                    dbc.SubmitChanges();
                    LoadGridCustomer();
                    //tabControl1.Enabled = false;
                    //btnSave.Enabled = false;
                    //btnSendDoc.Enabled = false;
                    tabControl1.Enabled = true;
                    btnSave.Enabled = true;
                    btnSendDoc.Enabled = true;
                    if (chk_prn.Checked == true)
                    {
                        Carotid_Tech.tct_follow_up = dt_followup;
                    }
                    else
                    {
                        dt_followup = null;
                        Carotid_Tech.tct_appoint_doctor_date = null;
                    }
                    Clearfrm();
                }
            }
            catch (Exception ex)
            {
                lblAlert.Text = ex.Message;
            }
        }
        private string SumString(string str1,string str2,string str3)
        {
            return (str1 == null ? "" : str1) + "|" + (str2 == null ? "" : str2) + "|" + (str3 == null ? "" : str3);
        }
        private void SubString(string strarray, TextBox txt1, TextBox txt2, TextBox txt3)
        {
            string[] str = strarray.Split('|');
            if(str.Length>0)
                txt1.Text = str[0];
            if(str.Length>1)
                txt2.Text = str[1];
            if(str.Length>2)
                txt3.Text = str[2];
        }
        private void btnSendDoc_Click(object sender, EventArgs e)
        {
            string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "CD101", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
            lblAlert.Text = result;

            //try
            //{
            //    if (docscan.SendtoDocscan("CD102", tprid, en, Program.getCurrentCareProvider))
            //        lblAlert.Text = HistoryData.savestatus;
            //    else
            //        lblAlert.Text = HistoryData.savestatus;
            //}
            //catch 
            //{
            //    lblAlert.Focus();
            //    lblAlert.Text = "Cannot send to docscan";
            //}
        }
        #endregion

        #region Func
        private void LoadGridCustomer()
        {
            var objcarotidlist = (from t1 in dbc.trn_carotid_teches
                                  where t1.tct_doctor_code == Program.CurrentUser.mut_username
                                  && t1.tct_result == false
                                  && t1.tct_close==false
                                  select new CarotidCustomer
                                  {
                                      tpr_id=t1.tpr_id,
                                      HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                      FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                      ArriveDate = t1.trn_patient_regi.tpr_arrive_date.Value.Date,
                                      DoctorName = t1.tct_doctor_name,
                                      cusid=t1.tpr_id,
                                      EN = t1.trn_patient_regi.tpr_en_no
                                  }).ToList();
            GridCustomer.DataSource = objcarotidlist;
            GridCustomer.Columns["Coltprid"].Visible = false;
            GridCustomer.Columns[7].Visible = false;
            GridCustomer.Columns["tpr_id"].Visible = false;
            GridCustomer.Columns["colen"].Visible = false;
            lbTitleUI.Text=string.Format("รายชื่อผู้ป่วยที่มีรออ่านผล (ทั้งหมด {0} คน)",objcarotidlist.Count().ToString());
        }
        private void Loadfrm()
        {
            UIProfileHorizontal1.Loaddata();
            if (Program.CurrentRegis != null)
            {
                tabControl1.Enabled = true;
                btnSave.Enabled = true;
                btnSendDoc.Enabled = true;
                    //Load data from trn_Other_xray
                    var objcurrentOtherxray = (from t1 in dbc.trn_other_xrays where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                    if (objcurrentOtherxray != null)
                    {
                         trn_carotid_hdr currentcarotid=(trn_carotid_hdr) bindingSourceCarotid.Current;
                         currentcarotid.tch_patient_result = objcurrentOtherxray.tox_patient_result;
                         currentcarotid.tch_patient_comt = objcurrentOtherxray.tox_patient_comt;
                         currentcarotid.tch_result = objcurrentOtherxray.tox_result;
                         currentcarotid.tch_report_doc = objcurrentOtherxray.tox_order_name;
                         currentcarotid.tch_request_date = objcurrentOtherxray.tox_order_date;
                         currentcarotid.tch_report_date = objcurrentOtherxray.tox_result_date;
                    }
            }
            else
            {
                tabControl1.Enabled = false;
                btnSave.Enabled = false;
                btnSendDoc.Enabled = false;
               
                pictureBoxLeft.Image = Properties.Resources.carotid_2;
                pictureBoxRight.Image = Properties.Resources.carotid_1;
            }
        }

        private void RemarkOnImgage(PictureBox pb, MouseEventArgs e, string side)
        {
            count++;
            frmPopupRemark frm = new frmPopupRemark();
            frm.ShowDialog();
            Label newlabel = new Label();
            newlabel = new Label();
            newlabel.Name = "l1" + count;
            newlabel.BackColor = Color.MintCream;
            newlabel.Left = Convert.ToInt32(e.X);
            newlabel.Top = Convert.ToInt32(e.Y);
            newlabel.AutoSize = true;
            newlabel.Cursor = Cursors.Hand;
            newlabel.Width = 20;
            newlabel.Height = 10;
            newlabel.Text = frm.strTextValue;
            newlabel.Tag = newlabel.Name;
            pb.Controls.Add(newlabel);
            if (side == "L")
            {
                newlabel.MouseClick += new MouseEventHandler(lblremark_L_Click);
            }
            if (side == "R")
            {
                newlabel.MouseClick += new MouseEventHandler(lblremark_R_Click);
            }
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
        private void GetConsultDoc()
        {
            int? ObjMrmID = (from a in dbc.mst_room_hdrs where a.mrm_code == "CD" && a.mhs_id == Convert.ToInt32(Program.CurrentSite.mhs_id) select a.mrm_id).FirstOrDefault();
            var ObjMdrName = (from t1 in dbc.mst_doc_result_hdrs
                              join t2 in dbc.mst_doc_results on t1.mrh_id equals t2.mrh_id
                              where t1.mrh_id == t2.mrh_id && t1.mrh_code == "CD" && t1.mrm_id == ObjMrmID
                              && t1.mrh_status == 'A' && t2.mdr_status == 'A'
                              select new
                              {
                                  mdrid = t2.mdr_id,
                                  name = t2.mdr_ename
                              }).ToList();
            cmb_mdrID.DataSource = ObjMdrName.Select((item, index) => new
            {
                item.mdrid,
                item.name
            }).ToList();
            cmb_mdrID.DisplayMember = "name";
            cmb_mdrID.ValueMember = "mdrid";
        }
        private void LoadData()
        {
            trn_carotid_hdr obj = dbc.trn_carotid_hdrs.Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
            if (obj != null)
            {
                bindingSourceCarotid.DataSource = obj;
                if (Program.CurrentRegis == null)
                {
                    bindingSourceCarotid.DataSource = (from tbl1 in dbc.trn_carotid_hdrs select tbl1);
                    bindingSourceCarotid.AddNew();
                }
                else
                {
                    bindingSourceCarotid.DataSource = obj;
                    trn_carotid_hdr Carotid_hdr = (trn_carotid_hdr)bindingSourceCarotid.Current;
                    Program.SetValueRadioGroup(pnlPre, Carotid_hdr.tch_artery_wall_abnormal.ToString());
                    Program.SetValueRadioGroup(panel4, Carotid_hdr.tch_wall_limestone.ToString());
                    Program.SetValueRadioGroup(pnl_tch_stenosis_abnormal, Carotid_hdr.tch_stenosis_abnormal.ToString());
                    Program.SetValueRadioGroup(pnl_tch_storke_abnormal, Carotid_hdr.tch_storke_abnormal.ToString());
                    if (Carotid_hdr.tch_stes_right_ica == 'Y')
                        chk_tch_stes_right_ica.Checked = true;
                    if (Carotid_hdr.tch_stes_right_eca == 'Y')
                        chk_tch_stes_right_eca.Checked = true;
                    if (Carotid_hdr.tch_stes_right_cca == 'Y')
                        chk_tch_stes_right_cca.Checked = true;
                    if (Carotid_hdr.tch_stes_right_va == 'Y')
                        chk_tch_stes_right_va.Checked = true;
                    if (Carotid_hdr.tch_stes_left_ica == 'Y')
                        chk_tch_stes_left_ica.Checked = true;
                    if (Carotid_hdr.tch_stes_left_eca == 'Y')
                        chk_tch_stes_left_eca.Checked = true;
                    if (Carotid_hdr.tch_stes_left_cca == 'Y')
                        chk_tch_stes_left_cca.Checked = true;
                    if (Carotid_hdr.tch_stes_left_va == 'Y')
                        chk_tch_stes_left_va.Checked = true;
                    if (Carotid_hdr.tch_stok_right_ica == 'Y')
                        chk_tch_stok_right_ica.Checked = true;
                    if (Carotid_hdr.tch_stok_right_eca == 'Y')
                        chk_tch_stok_right_eca.Checked = true;
                    if (Carotid_hdr.tch_stok_right_cca == 'Y')
                        chk_tch_stok_right_cca.Checked = true;
                    if (Carotid_hdr.tch_stok_right_va == 'Y')
                        chk_tch_stok_right_va.Checked = true;
                    if (Carotid_hdr.tch_stok_right_va == 'Y')
                        chk_tch_stok_right_va.Checked = true;
                    if (Carotid_hdr.tch_stok_left_ica == 'Y')
                        chk_tch_stok_left_ica.Checked = true;
                    if (Carotid_hdr.tch_stok_left_eca == 'Y')
                        chk_tch_stok_left_eca.Checked = true;
                    if (Carotid_hdr.tch_stok_left_cca == 'Y')
                        chk_tch_stok_left_cca.Checked = true;
                    if (Carotid_hdr.tch_stok_left_va == 'Y')
                        chk_tch_stok_left_va.Checked = true;
                    if (Carotid_hdr.tch_consult_doc == 'Y')
                    {
                        chk_tch_consult_doc.Checked = true;
                        int? ObjMdrID = (from a in dbc.trn_carotid_hdrs where a.tpr_id == tprid select a.mdr_id).FirstOrDefault();
                        GetConsultDoc();
                        cmb_mdrID.SelectedValue = ObjMdrID;
                    }
                    txt_tch_stes_right_ica_txt.Text = Carotid_hdr.tch_stes_right_ica_txt.ToString();
                    txt_tch_stes_right_eca_txt.Text = Carotid_hdr.tch_stes_right_eca_txt.ToString();
                    txt_tch_stes_right_cca_txt.Text = Carotid_hdr.tch_stes_right_cca_txt.ToString();
                    txt_tch_stes_right_va_txt.Text = Carotid_hdr.tch_stes_right_va_txt.ToString();
                    txt_tch_stes_left_ica_txt.Text = Carotid_hdr.tch_stes_left_ica_txt.ToString();
                    txt_tch_stes_left_eca_txt.Text = Carotid_hdr.tch_stes_left_eca_txt.ToString();
                    txt_tch_stes_left_cca_txt.Text = Carotid_hdr.tch_stes_left_cca_txt.ToString();
                    txt_tch_stes_left_va_txt.Text = Carotid_hdr.tch_stes_left_va_txt.ToString();
                    Program.SetValueRadioGroup(pnl_tch_summary_result, Carotid_hdr.tch_summary_result.ToString());

                    //Yee add 24 /07/2013
                    SubString(Carotid_hdr.tch_wall_right_ica, txt_tch_wall_right_ica1, txt_tch_wall_right_ica2, txt_tch_wall_right_ica3);
                    SubString(Carotid_hdr.tch_wall_right_eca, txt_tch_wall_right_eca1, txt_tch_wall_right_eca2, txt_tch_wall_right_eca3);
                    SubString(Carotid_hdr.tch_wall_right_cca, txt_tch_wall_right_cca1, txt_tch_wall_right_cca2, txt_tch_wall_right_cca3);
                    SubString(Carotid_hdr.tch_wall_right_va, txt_tch_wall_right_va1, txt_tch_wall_right_va2, txt_tch_wall_right_va3);

                    SubString(Carotid_hdr.tch_wall_left_ica, txt_tch_wall_left_ica1, txt_tch_wall_left_ica2, txt_tch_wall_left_ica3);
                    SubString(Carotid_hdr.tch_wall_left_eca, txt_tch_wall_left_eca1, txt_tch_wall_left_eca2, txt_tch_wall_left_eca3);
                    SubString(Carotid_hdr.tch_wall_left_cca, txt_tch_wall_left_cca1, txt_tch_wall_left_cca2, txt_tch_wall_left_cca3);
                    SubString(Carotid_hdr.tch_wall_left_va, txt_tch_wall_left_va1, txt_tch_wall_left_va2, txt_tch_wall_left_va3);

                   txt_tch_stes_right_ica_txt.Text= Carotid_hdr.tch_stes_right_ica_txt ;
                   txt_tch_stes_right_eca_txt.Text= Carotid_hdr.tch_stes_right_eca_txt ;
                   txt_tch_stes_right_cca_txt.Text=Carotid_hdr.tch_stes_right_cca_txt ;
                   txt_tch_stes_right_va_txt.Text= Carotid_hdr.tch_stes_right_va_txt ;

                   txt_tch_stes_left_ica_txt.Text= Carotid_hdr.tch_stes_left_ica_txt;
                   txt_tch_stes_left_eca_txt.Text = Carotid_hdr.tch_stes_left_eca_txt;
                   txt_tch_stes_left_cca_txt.Text= Carotid_hdr.tch_stes_left_cca_txt ;
                   txt_tch_stes_left_va_txt.Text= Carotid_hdr.tch_stes_left_va_txt ;

                   txt_tch_stok_right_ica_txt.Text= Carotid_hdr.tch_stok_right_ica_txt ;
                   txt_tch_stok_right_eca_txt.Text= Carotid_hdr.tch_stok_right_eca_txt ;
                   txt_tch_stok_right_cca_txt.Text= Carotid_hdr.tch_stok_right_cca_txt ;
                   txt_tch_stok_right_va_txt.Text= Carotid_hdr.tch_stok_right_va_txt ;
                   txt_tch_stok_left_ica_txt.Text= Carotid_hdr.tch_stok_left_ica_txt ;
                   txt_tch_stok_left_eca_txt.Text= Carotid_hdr.tch_stok_left_eca_txt ;
                   txt_tch_stok_left_cca_txt.Text= Carotid_hdr.tch_stok_left_cca_txt ;
                   txt_tch_stok_left_va_txt.Text= Carotid_hdr.tch_stok_left_va_txt ;
                    //
                   if (Carotid_hdr.tch_doctor_code != null)
                   {
                       autoCompleteUC1.SelectedValue = Carotid_hdr.tch_doctor_code;
                   }
                   else
                   {
                       autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;                       
                       autoCompleteUC1.Enabled = false;
                   }
                }                
            }
            else
            {
                bindingSourceCarotid.DataSource = (from tbl1 in dbc.trn_carotid_hdrs select tbl1);
                bindingSourceCarotid.AddNew();
                autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                autoCompleteUC1.Enabled = false;
            }
        }

        private void Clearfrm()
        {
            tabControl1.SelectedIndex = 0;
            ClsControl();
            UIProfileHorizontal1.ClearForm();
            txttechnician.Text = "";
            bindingSourceCarotid.DataSource=new trn_carotid_hdr();
            bindingSourceTrn_carotid_Tech.DataSource = new trn_carotid_tech();
            autoCompleteUC2.SelectedValue = null;
            dateTimeFollowup.Value = DateTime.Now;
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
                TextBox txt;
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.Text = string.Empty;
                }
            }
        }

        private void ClsControl()
        {
            clrpanel(pnlsummary);
            //Clear Picture Box
            DrawingShapes = new Usercontrols.ToolPaint.Shapes();
            DrawingShapes2 = new Usercontrols.ToolPaint.Shapes();
            pictureBoxRight.Refresh();
            pictureBoxLeft.Refresh();
            pictureBoxLeft.Image = Properties.Resources.carotid_2;
            pictureBoxRight.Image = Properties.Resources.carotid_1;
            clrPicture(pictureBoxLeft);
            clrPicture(pictureBoxRight);
            //end Clear Picture
            clrgroupbox(GBCarotidDuplex);
            clrpanel(pnlAdviceMec);
            clrpanel(pnlAdviceDiet);
            clrpanel(pnlAdviceExercise);
            clrpanel(pnlConsult);
            clrpanel(pnlFu);
            chk_prn.Checked = false;
            chkappoint.Checked = false;
            clrpanel(pnlDuplex);
            clrpanel(pnlStatusCall);
            clrpanel(pnlAppointStatus);
            clrgroupbox(GBlab);
            clrgroupbox(groupBox1);
            clrgroupbox(groupBox2);
            clrgroupbox(groupBox3);
            clrgroupbox(groupBox4);
            txtpatientOrderResult.Text = "";
            txtComment.Text = "";
            txtResult.Text = "";
            clrpanel(pnlPre);
            clrpanel(panel4);
            clrpanel(pnl_tch_storke_abnormal);
            clrpanel(pnl_tch_summary_result);
            checkBox22.Checked = false;
            chk_tch_consult_doc.Checked = false;
            clrgroupbox(groupBox11);
            clrgroupbox(groupBox12);
        }

        private void clrgroupbox(GroupBox gb)
        {
            foreach (Control ctl in gb.Controls)
            {
                TextBox txt;
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.Text = string.Empty;
                }
            }
        }        
        
        #endregion

        #region Tools
        private void chk_tch_consult_doc_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_consult_doc.Checked == true)
            {
                trn_carotid_hdr Carotid_hdr = (trn_carotid_hdr)bindingSourceCarotid.Current;
                cmb_mdrID.Enabled = true;
                if (Carotid_hdr.tch_consult_doc == 'Y')
                {
                    chk_tch_consult_doc.Checked = true;
                    int? ObjMdrID = (from a in dbc.trn_carotid_hdrs where a.tpr_id == tprid select a.mdr_id).FirstOrDefault();
                    GetConsultDoc();
                    cmb_mdrID.SelectedValue = ObjMdrID;
                    mdrid = cmb_mdrID.SelectedValue.ToString();
                }
                else
                {
                    GetConsultDoc();
                    mdrid = cmb_mdrID.SelectedValue.ToString();
                }
            }
            else
            {
                cmb_mdrID.Enabled = false;
                mdrid = null ;
            }
        }

        private void btnBrushErase_Click(object sender, EventArgs e)
        {
            Brush = !Brush;
            Brush2 = !Brush2;
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
            if (MessageBox.Show("Are you sure you want to clear all ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
        #endregion

        private void frmCarotidReport_Load(object sender, EventArgs e)
        {
            this.Text = Program.GetRoomName("Carotid Reports");
            LoadGridCustomer();
            Loadfrm();
            GetConsultDoc();
        }

        private void chk_tch_stes_right_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_right_ica.Checked == true)
            {
                txt_tch_stes_right_ica_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_right_ica_txt.Enabled = false;
                txt_tch_stes_right_ica_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stes_right_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_right_eca.Checked == true)
            {
                txt_tch_stes_right_eca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_right_eca_txt.Enabled = false;
                txt_tch_stes_right_eca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stes_right_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_right_cca.Checked == true)
            {
                txt_tch_stes_right_cca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_right_cca_txt.Enabled = false;
                txt_tch_stes_right_cca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stes_right_va_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_right_va.Checked == true)
            {
                txt_tch_stes_right_va_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_right_va_txt.Enabled = false;
                txt_tch_stes_right_va_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stes_left_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_left_ica.Checked == true)
            {
                txt_tch_stes_left_ica_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_left_ica_txt.Enabled = false;
                txt_tch_stes_left_ica_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stes_left_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_left_eca.Checked == true)
            {
                txt_tch_stes_left_eca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_left_eca_txt.Enabled = false;
                txt_tch_stes_left_eca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stes_left_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_left_cca.Checked == true)
            {
                txt_tch_stes_left_cca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_left_cca_txt.Enabled = false;
                txt_tch_stes_left_cca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stes_left_va_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stes_left_va.Checked == true)
            {
                txt_tch_stes_left_va_txt.Enabled = true;
            }
            else
            {
                txt_tch_stes_left_va_txt.Enabled = false;
                txt_tch_stes_left_va_txt.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmCarotidHistory frm = new frmCarotidHistory();
            frm.Show();
            frm.WindowState = FormWindowState.Maximized;
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
                txtAdviceMec.Text = string.Empty;
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
                txtAdviceDiet.Text = string.Empty;
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
                txtAdviceExercise.Text = string.Empty;
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
                txtconsult.Text = string.Empty;
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
                txtfu.Text = string.Empty;
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

        private void chkappoint_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkappoint.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd/MM/yyyy";
                dt_appoint = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
                dt_appoint = null;
            }
        }

        private void chk_prn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_prn.Checked == true)
            {
                dateTimeFollowup.Enabled = true;
                dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                dateTimeFollowup.CustomFormat = "dd/MM/yyyy";
                dt_followup = Convert.ToDateTime(dateTimeFollowup.Value.ToShortDateString());
            }
            else
            {
                //  M  fix  for  Follow up ไม่ได้ ถ้าไม่ติ๊ก PRN ต้องทำ Follow up ได้ถึงแม้ไม่ติ๊ก PRN 7/7/2015
                //dateTimeFollowup.Enabled = false;
                //dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                //dateTimeFollowup.CustomFormat = " ";
                //dt_followup = null;

                dateTimeFollowup.Enabled = true;
                dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                dateTimeFollowup.CustomFormat = "dd/MM/yyyy";
                dt_followup = Convert.ToDateTime(dateTimeFollowup.Value.ToShortDateString());
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dt_appoint = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
        }

        private void dateTimeFollowup_ValueChanged(object sender, EventArgs e)
        {
            dt_followup = Convert.ToDateTime(dateTimeFollowup.Value.ToShortDateString());
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

        private void lblremark_R_Click(object sender, EventArgs e)
        {
            Label itemdata = (Label)sender;
            strTagID_R = itemdata.Tag.ToString();
            pictureBoxRight.Controls.RemoveByKey(strTagID_R);
        }

        private void lblremark_L_Click(object sender, EventArgs e)
        {
            Label itemdata = (Label)sender;
            strTagID_L = itemdata.Tag.ToString();
            pictureBoxLeft.Controls.RemoveByKey(strTagID_L);
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

        private void cmb_mdrID_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_mdrID.SelectedValue != null)
                mdrid = cmb_mdrID.SelectedValue.ToString();
        }

        private void GridCustomer_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridCustomer.Rows.Count; i++)
            {
                GridCustomer.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
                GridCustomer.Columns["Column1"].SortMode = DataGridViewColumnSortMode.NotSortable;
                GridCustomer.Columns["Column2"].SortMode = DataGridViewColumnSortMode.NotSortable;
                GridCustomer.Columns["Column3"].SortMode = DataGridViewColumnSortMode.NotSortable;
                GridCustomer.Columns["Column6"].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        DocScan docscan = new DocScan();
        private void GridCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tabControl1.Enabled = false;
                lblAlert.Text = "";
                pbResultFromDoc.Image = null;
                lblPage.Text = "-";
                ClsControl();
                if (e.RowIndex >= 0)
                {
                    var selectedItems = GridCustomer.CurrentRow;
                    if (tprid != Convert.ToInt32(selectedItems.Cells[7].Value.ToString()) || tprid == Convert.ToInt32(selectedItems.Cells[7].Value.ToString()))
                    {
                        string hn;
                        tprid = Convert.ToInt32(selectedItems.Cells[7].Value.ToString());
                        en = selectedItems.Cells["colen"].Value.ToString();
                        hn = selectedItems.Cells["Column2"].Value.ToString();
                        Program.CurrentRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == tprid).FirstOrDefault();
                        if (Program.CurrentRegis != null)
                        {
                            CheckUpLabClass.retrieveLabToPatientLab(new List<string> { "C0180", "C0150", "N0510", "C0159" }, Program.CurrentRegis.tpr_id);
                        }
                        trn_carotid_tech obj = dbc.trn_carotid_teches.Where(c => c.tpr_id == tprid).FirstOrDefault();
                        if (obj != null)
                        {
                            tabControl1.Enabled = true;
                            bindingSourceTrn_carotid_Tech.DataSource = obj;
                            if (Program.CurrentRegis != null)
                            {
                                bindingSourceTrn_carotid_Tech.DataSource = obj;
                                trn_carotid_tech trnCarotid_tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                                objimage_L = trnCarotid_tech.tct_left_result.ToArray();
                                objimage_R = trnCarotid_tech.tct_right_result.ToArray();
                                byte[] data;
                                byte[] data2;
                                data = (byte[])objimage_L;
                                data2 = (byte[])objimage_R;
                                MemoryStream ms = new MemoryStream(data);
                                MemoryStream ms2 = new MemoryStream(data2);
                                pictureBoxLeft.Image = Image.FromStream(ms);
                                pictureBoxRight.Image = Image.FromStream(ms2);
                                Program.SetValueRadioGroup(pnlDuplex, trnCarotid_tech.tct_finding_ca_duplex.ToString());
                                cmblist1.SelectedItem = trnCarotid_tech.tct_appoint_depart;
                                Program.SetValueRadioGroup(pnlStatusCall, trnCarotid_tech.tct_call_status.ToString());
                                Program.SetValueRadioGroup(pnlsummary, trnCarotid_tech.tct_summary.ToString());
                                string Str_Appoint_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_appoint_doctor_date.ToString()) ? "" : Convert.ToDateTime(trnCarotid_tech.tct_appoint_doctor_date).ToString();
                                string Str_Followup_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_follow_up.ToString()) ? "" : Convert.ToDateTime(trnCarotid_tech.tct_follow_up).ToString();
                                txtsumaryRemark.Text = trnCarotid_tech.tct_summary_remark;
                                if (trnCarotid_tech.tct_appoint_doctor == true)
                                {
                                    chkappoint.Checked = true;
                                    // dateTimePicker1.MinDate = Convert.ToDateTime(Str_Appoint_Date); 
                                    dateTimePicker1.Text = Str_Appoint_Date;
                                    dateTimePicker1.Enabled = true;
                                }
                                if (trnCarotid_tech.tct_appoint_doctor == false)
                                {
                                    dateTimePicker1.Format = DateTimePickerFormat.Custom;
                                    dateTimePicker1.CustomFormat = " ";
                                    dt_appoint = null;
                                    dateTimePicker1.Enabled = false;
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
                                    // dateTimeFollowup.MinDate = Convert.ToDateTime(Str_Followup_Date);
                                    dateTimeFollowup.Text = Str_Followup_Date;
                                    dateTimeFollowup.Enabled = true;
                                }
                                else if (trnCarotid_tech.tct_prn == false)
                                {
                                    dateTimeFollowup.Text = string.Empty;
                                    dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                                    dateTimeFollowup.CustomFormat = " ";
                                    dt_followup = null;
                                    dateTimeFollowup.Enabled = false;
                                }
                                txtremark.Text = trnCarotid_tech.tct_other_remark;
                                Program.SetValueRadioGroup(pnlAppointStatus, trnCarotid_tech.tct_appoint_status.ToString());
                                ////GetLab
                                var ObjLabFbs = (from t1 in dbc.trn_patient_ass_dtls
                                                 join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                                                 join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                                                 where t1.tped_lab_code == "C0180" && t3.tpr_id == tprid
                                                 orderby t1.tped_update_date descending
                                                 select t1.tped_lab_value).FirstOrDefault();

                                var ObjLabCholes = (from t1 in dbc.trn_patient_ass_dtls
                                                    join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                                                    join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                                                    where t1.tped_lab_code == "C0150" && t3.tpr_id == tprid
                                                    orderby t1.tped_update_date descending
                                                    select t1.tped_lab_value).FirstOrDefault();

                                var ObjLabBmi = (from t1 in dbc.trn_basic_measure_dtls
                                                 join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                                                 where t2.tpr_id == tprid
                                                 orderby t1.tbd_update_date descending
                                                 select t1.tbd_bmi).FirstOrDefault();

                                var ObjLabHbA = (from t1 in dbc.trn_patient_ass_dtls
                                                 join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                                                 join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                                                 where t1.tped_lab_code == "N0510" && t3.tpr_id == tprid
                                                 orderby t1.tped_update_date descending
                                                 select t1.tped_lab_value).FirstOrDefault();

                                var ObjLabLdl = (from t1 in dbc.trn_patient_ass_dtls
                                                 join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                                                 join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                                                 where t1.tped_lab_code == "C0159" && t3.tpr_id == tprid
                                                 orderby t1.tped_update_date descending
                                                 select t1.tped_lab_value).FirstOrDefault();

                                var ObjLabBp = (from t1 in dbc.trn_basic_measure_dtls
                                                join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                                                where t2.tpr_id == tprid
                                                orderby t1.tbd_update_date descending
                                                select t1.tbd_systolic + "/" + t1.tbd_diastolic).FirstOrDefault();

                                var ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == Program.CurrentRegis.tpr_id select t1.tpr_mobile_phone).FirstOrDefault();
                                txtfbs.Text = ObjLabFbs;
                                txtcho.Text = ObjLabCholes;
                                txtbmi.Text = ObjLabBmi;
                                txthb.Text = ObjLabHbA;
                                txtldl.Text = ObjLabLdl;
                                txtbp.Text = ObjLabBp;
                                txtmobilephone.Text = ObjMobile;
                                //var ObjTechnician = (from tbl in dbc.trn_carotid_teches
                                //                     where tbl.tpr_id == Program.CurrentRegis.tpr_id
                                //                     join tbl2 in dbc.mst_user_types on tbl.tct_create_by equals tbl2.mut_username
                                //                     where tbl.tpr_id == Program.CurrentRegis.tpr_id
                                //                     select tbl2.mut_fullname).FirstOrDefault();
                                var ObjTechnician = (from tbl in dbc.trn_carotid_teches
                                                     where tbl.tpr_id == Program.CurrentRegis.tpr_id                                                     
                                                     select tbl.tct_technician_name).FirstOrDefault();
                                txttechnician.Text = ObjTechnician.ToString();
                                ////GetDocscan
                                var coutItem = dbc.pw_Get_DocscanStatus(tprid, "CD101", en).FirstOrDefault();
                                if (coutItem.Column1 == 0)////when docscan status = 0 and it not send to docscan
                                {
                                    lbldocscanStatus.Visible = true;
                                    btnsend_to_docscan.Visible = true;
                                    btnFirst.Enabled = false;
                                    btnNext.Enabled = false;
                                    lbldocscanStatus.Text = "No send to docscan";
                                }
                                else
                                {
                                    lbldocscanStatus.Visible = false;
                                    btnsend_to_docscan.Visible = false;
                                    lbldocscanStatus.Text = "";
                                    HistoryData.showform = 'N';
                                    docscan.GetHistory("CD101", en, tprid);
                                    pbResultFromDoc.Image = docscan.resizeImage(HistoryData.newImage, new Size(300, 320));
                                    lblPage.Text = HistoryData.count + 1 + "/" + HistoryData.filelength.ToString();
                                    //if (HistoryData.Totalpage == 1)
                                    //{
                                    //    btnFirst.Enabled = false;
                                    //    btnNext.Enabled = false;
                                    //}
                                    //else
                                    //{
                                    //    btnFirst.Enabled = true;
                                    //    btnNext.Enabled = true;
                                    //}
                                }
                                ////EndGetDocscan
                                var objotherxray = (from t1 in dbc.trn_other_xrays
                                                    where t1.tox_en_no == en
                                                      && t1.trn_patient_regi.trn_patient.tpt_hn_no == hn
                                                      && t1.tox_room_type == "CD"
                                                    select t1).FirstOrDefault();
                                if (objotherxray != null)
                                {
                                    txtpatientOrderResult.Text = objotherxray.tox_result;
                                    txtComment.Text = objotherxray.tox_patient_comt;
                                    txtResult.Text = objotherxray.tox_order_name;
                                    txtRequestDate.Text = objotherxray.tox_order_date.Value.ToShortDateString();
                                    request_date = Convert.ToDateTime(objotherxray.tox_order_date.Value.ToShortDateString());
                                    report_date = Convert.ToDateTime(objotherxray.tox_result_date.Value.ToShortDateString());
                                    txtReportDateTime.Text = objotherxray.tox_result_date.Value.ToShortDateString();
                                }
                            }
                            else
                            {
                                lblAlert.Focus();
                                lblAlert.Text = "Program.CurrentRqgis is null !";
                            }
                        }
                    }
                    LoadData();
                }
                Loadfrm();
            }
            catch
            {
                return;
            }
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

        private void pictureBoxLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (Brush)
            {
                IsPainting = true;
                ShapeNum++;
                LastPos = new Point(0, 0);
            }
            else
            {
                IsEraseing = true;
            }
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

        private void pictureBoxRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (Brush2)
            {
                IsPainting2 = true;
                ShapeNum2++;
                LastPos2 = new Point(0, 0);
            }
            else
            {
                IsEraseing2 = true;
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
            if (IsEraseing2)
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

        private void txt_tch_artery_right_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
                e.Handled = true;

            base.OnKeyPress(e);
        }

        private void chk_tch_stok_right_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_right_ica.Checked == true)
            {
                txt_tch_stok_right_ica_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_right_ica_txt.Enabled = false;
                txt_tch_stok_right_ica_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stok_right_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_right_eca.Checked == true)
            {
                txt_tch_stok_right_eca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_right_eca_txt.Enabled = false;
                txt_tch_stok_right_eca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stok_right_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_right_cca.Checked == true)
            {
                txt_tch_stok_right_cca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_right_cca_txt.Enabled = false;
                txt_tch_stok_right_cca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stok_right_va_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_right_va.Checked == true)
            {
                txt_tch_stok_right_va_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_right_va_txt.Enabled = false;
                txt_tch_stok_right_va_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stok_left_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_left_ica.Checked == true)
            {
                txt_tch_stok_left_ica_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_left_ica_txt.Enabled = false;
                txt_tch_stok_left_ica_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stok_left_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_left_eca.Checked == true)
            {
                txt_tch_stok_left_eca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_left_eca_txt.Enabled = false;
                txt_tch_stok_left_eca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stok_left_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_left_cca.Checked == true)
            {
                txt_tch_stok_left_cca_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_left_cca_txt.Enabled = false;
                txt_tch_stok_left_cca_txt.Text = string.Empty;
            }
        }

        private void chk_tch_stok_left_va_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_tch_stok_left_va.Checked == true)
            {
                txt_tch_stok_left_va_txt.Enabled = true;
            }
            else
            {
                txt_tch_stok_left_va_txt.Enabled = false;
                txt_tch_stok_left_va_txt.Text = string.Empty;
            }
        }

        private void btnsend_to_docscan_Click(object sender, EventArgs e)
        {
            try
            {
                string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "CD101", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                lblAlert.Text = result;

                //if (docscan.SendtoDocscan("CD101", tprid, en, Program.getCurrentCareProvider))
                //{
                //    lblAlert.Text = HistoryData.savestatus;
                //    docscan.GetHistory("CD101", en, tprid);
                //    pbResultFromDoc.Image = docscan.resizeImage(HistoryData.newImage, new Size(300, 320));
                //}
                //else
                //{
                //    lblAlert.Focus();
                //    lblAlert.Text = "Failed! Cannot send to docscan";
                //}
            }
            catch (Exception)
            {
            }
        }

        private void pbResultFromDoc_Click(object sender, EventArgs e)
        {
            if (this.pbResultFromDoc.Image != null)
            {
                frmViewscan newFrm = new frmViewscan();
                newFrm.Show();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (countpage < HistoryData.filelength)
            {
                countpage++;
                pbResultFromDoc.Image = docscan.resizeImage((Image)HistoryData.arrlist[countpage - 1], new Size(300, 320));
                lblPage.Text = countpage + "/" + HistoryData.filelength.ToString();
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (countpage >= 0)
            {
                countpage--;
                pbResultFromDoc.Image = docscan.resizeImage((Image)HistoryData.arrlist[countpage - 1], new Size(300, 320));
                lblPage.Text = lblPage.Text = countpage + "/" + HistoryData.filelength.ToString();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt_tch_artery_right.Text = "";
            txt_tch_artery_left.Text = "";
            txt_tch_artery_right.Enabled = false;
            txt_tch_artery_left.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txt_tch_artery_right.Enabled = true;
            txt_tch_artery_left.Enabled = true;
        }
    }
}
