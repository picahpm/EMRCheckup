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
using BKvs2010.EmrClass;

namespace BKvs2010
{
    /// <summary>
    ///  view history of patient carotid artery
    /// </summary>
    public partial class frmCarotidHistory : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        int tprid = 0,tpr_id = 0,gv_rst;
        int count;
        string mdrid;
        object objimage_L;
        object objimage_R;
        DateTime? dt_appoint;
        DateTime? dt_followup;
        Usercontrols.ToolPaint.Shapes DrawingShapes = new Usercontrols.ToolPaint.Shapes();
        Usercontrols.ToolPaint.Shapes DrawingShapes2 = new Usercontrols.ToolPaint.Shapes();
        private bool IsPainting = false;
        private bool IsEraseing = false;
        private Point LastPos = new Point(0, 0);
        private bool IsPainting2 = false;
        private bool IsEraseing2 = false;
        private Point LastPos2 = new Point(0, 0);
        private float CurrentWidth2 = 10;
        private float CurrentWidth = 10;
        private Color CurrentColour = Color.Black;
        private Color CurrentColour2 = Color.Black;
        private bool IsMouseing = false;
        private Point MouseLoc = new Point(0, 0);
        private Point MouseLoc2 = new Point(0, 0);
        private bool Brush2 = true;
        private bool Brush = true;
        private int ShapeNum = 0;
        private int ShapeNum2 = 0;
        private bool IsRemark_L = true;
        private bool IsRemark_R = true;
        string strTagID_L, strTagID_R;

        public frmCarotidHistory()
        {
            InitializeComponent();
        }

        #region Funcs
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
        private void ClsControl()
        {
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

        private void GetPatient()
        {
            var ObjNullResult = (from t1 in dbc.trn_carotid_teches
                                  where t1.tct_result == false
                                   && (t1.tct_close==false || t1.tct_close==null)
                                   orderby t1.trn_patient_regi.tpr_arrive_date 
                                  select new CarotidCustomer
                                  {
                                      tpr_id = t1.tpr_id,
                                      HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                      FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                      ArriveDate = t1.trn_patient_regi.tpr_arrive_date.Value.Date,
                                      DoctorName = t1.tct_doctor_name,
                                      cusid = t1.tpr_id
                                  }).ToList();

            GV_Null_Result.DataSource = ObjNullResult;
            GV_Null_Result.Columns["Coltprid"].Visible = false;
            GV_Null_Result.Columns[7].Visible = false;
            GV_Null_Result.Columns["tpr_id"].Visible = false;
            lbTitleUI.Text = string.Format("รายชื่อผู้ป่วยที่มีรออ่านผล (ทั้งหมด {0} คน)", ObjNullResult.Count().ToString());

            var ObjResult = (from t1 in dbc.trn_carotid_teches
                             where  t1.tct_result == true
                              && (t1.tct_close == false || t1.tct_close == null)
                             select new CarotidCustomer
                             {
                                 tpr_id = t1.tpr_id,
                                 HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                 FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                 ArriveDate = t1.trn_patient_regi.tpr_arrive_date.Value.Date,
                                 DoctorName = t1.tct_doctor_name,
                                 cusid = t1.tpr_id
                             }).ToList();
            GV_Result.DataSource = ObjResult;
            GV_Result.Columns["Coltprid2"].Visible = false;
            GV_Result.Columns[7].Visible = false;
            GV_Result.Columns["tpr_id"].Visible = false;
            lblTitle2.Text = string.Format("รายชื่อผู้ป่วยที่หมออ่านผลแล้วแต่ยังไม่ได้โทรแจ้ง (ทั้งหมด {0} คน)", ObjResult.Count().ToString());
        }

        private void LoadData()
        {
            if (Program.CurrentRegis != null)
            {
                CheckUpLabClass.retrieveLabToPatientLab(new List<string> { "C0180", "C0150", "N0510", "C0159" }, Program.CurrentRegis.tpr_id);
            }
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
                }
            }
            else
            {
                bindingSourceCarotid.DataSource = (from tbl1 in dbc.trn_carotid_hdrs select tbl1);
                bindingSourceCarotid.AddNew();
            }
        }

        private void Gv_GetDataPatiet(DataGridView gv)
        {
            var selectedItems = gv.CurrentRow;
            lblAlert.Text = string.Empty;
            if (tprid != Convert.ToInt32(selectedItems.Cells[7].Value.ToString()))
            {
                tprid = Convert.ToInt32(selectedItems.Cells[7].Value.ToString());
                label1.Text = tprid.ToString();
                Program.CurrentRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == tprid).FirstOrDefault();
                trn_carotid_tech obj = dbc.trn_carotid_teches.Where(c => c.tpr_id == tprid).FirstOrDefault();
                trn_carotid_hdr obj_carotid_hdr = dbc.trn_carotid_hdrs.Where(c => c.tpr_id == tprid).FirstOrDefault();
                if (obj != null)
                {
                    bindingSourceTrn_carotid_Tech.DataSource = obj;
                    if (Program.CurrentRegis != null)
                    {
                        bindingSourceTrn_carotid_Tech.DataSource = obj;
                        trn_carotid_tech trnCarotid_tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                        bool? isClose =trnCarotid_tech.tct_close;
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
                        txtDoctorCode.Text = trnCarotid_tech.tct_doctor_code;
                        txtDoctorName.Text = trnCarotid_tech.tct_doctor_name;
                        string Str_Appoint_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_appoint_doctor_date.ToString()) ? "": Convert.ToDateTime(trnCarotid_tech.tct_appoint_doctor_date).ToString();
                        string Str_Followup_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_follow_up.ToString()) ? "" : Convert.ToDateTime(trnCarotid_tech.tct_follow_up).ToString();
                       
                        txtsumaryRemark.Text = trnCarotid_tech.tct_summary_remark;
                        if (trnCarotid_tech.tct_appoint_doctor == true)
                        {
                            chkappoint.Checked = true;
                            //dateTimePicker1.MinDate = Convert.ToDateTime(Str_Appoint_Date);
                            dateTimePicker1.Text = Str_Appoint_Date;
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
                            //dateTimeFollowup.MinDate = Convert.ToDateTime(trnCarotid_tech.tct_follow_up); //Convert.ToDateTime(Str_Followup_Date);
                            dateTimeFollowup.Text = Convert.ToDateTime(trnCarotid_tech.tct_follow_up).ToString();//Str_Followup_Date;
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
                    }
                    else
                    { }
                }
                btnSaveAsDraft.Enabled = true;
            }
           LoadData();
        }

        private void Loadfrm()
        {
            uiProfileHorizontal1.Loaddata();
            if (Program.CurrentRegis != null)
            {
                tabControl1.Enabled = true;
                //Load data from trn_Other_xray
                var objcurrentOtherxray = (from t1 in dbc.trn_other_xrays where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                if (objcurrentOtherxray != null)
                {
                    trn_carotid_hdr currentcarotid = (trn_carotid_hdr)bindingSourceCarotid.Current;
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
            }
        }

        private void SearchGetDoctor(string strSearch)
        {
                var objlistMstUserType = (from t1 in dbc.mst_user_types
                                          where t1.mut_type == 'D'
                                          && (t1.mut_out_checkup == false || t1.mut_out_checkup == null)
                                          && t1.mut_fullname.Contains(strSearch)
                                          select new { DoctorName = t1.mut_fullname, DoctorCode = t1.mut_username }).ToList();
                GridDoctorName.DataSource = objlistMstUserType;
                GridDoctorName.Columns[1].Visible = false;
                GridDoctorName.Columns[0].HeaderText = "Doctor Name";
                GridDoctorName.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (objlistMstUserType.Count() > 0)
                {
                    GridDoctorName.Visible = true;
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

        private void Save_DB(int tb_status, Boolean tb_hdr, Boolean tb_tech)
        {
            btnSaveAsDraft.Enabled = false;
            ////[trn_carotid_hdr]
            trn_carotid_hdr Carotid_hdr = (trn_carotid_hdr)bindingSourceCarotid.Current;
            trn_carotid_hdr obj = dbc.trn_carotid_hdrs.Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
            ////[trn_carotid_tech]
            trn_carotid_tech Carotid_Tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
            trn_carotid_tech Obj_Carotid_Tech = dbc.trn_carotid_teches.Where(c => c.tpr_id == tprid).FirstOrDefault();
            bindingSourceTrn_carotid_Tech.DataSource = Obj_Carotid_Tech;
            Boolean saveIsCompleted = false;
            DateTime datenowvalue = Program.GetServerDateTime();

            if (tb_hdr == true)
            {
                Carotid_hdr.tpr_id = Program.CurrentRegis.tpr_id;
                if (chk_tch_consult_doc.Checked == true)
                {
                    Carotid_hdr.mdr_id = Convert.ToInt32(mdrid);
                }
                if (chk_tch_consult_doc.Checked == false)
                {
                    Carotid_hdr.mdr_id = null;
                }
                if (Carotid_Tech.tct_close == null)
                {

                }
                Carotid_hdr.tch_type = 'N';
                Carotid_hdr.tch_artery_wall_abnormal = Program.GetValueRadioTochar(pnlPre);
                Carotid_hdr.tch_wall_limestone = Program.GetValueRadioTochar(panel4);
                Carotid_hdr.tch_stenosis_abnormal = Program.GetValueRadioTochar(pnl_tch_stenosis_abnormal);

                //Yee add 24 /07/2013
                Carotid_hdr.tch_wall_right_ica = SumString(txt_tch_wall_right_ica1.Text, txt_tch_wall_right_ica2.Text, txt_tch_wall_right_ica3.Text);
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
                //double tch_stes_right_ica_txt = string.IsNullOrEmpty(txt_tch_stes_right_ica_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_right_ica_txt.Text);
                //double tch_stes_right_eca_txt = string.IsNullOrEmpty(txt_tch_stes_right_eca_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_right_eca_txt.Text);
                //double tch_stes_right_cca_txt = string.IsNullOrEmpty(txt_tch_stes_right_cca_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_right_cca_txt.Text);
                //double tch_stes_right_va_txt = string.IsNullOrEmpty(txt_tch_stes_right_va_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_right_va_txt.Text);
                //double tch_stes_left_ica_txt = string.IsNullOrEmpty(txt_tch_stes_left_ica_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_left_ica_txt.Text);
                //double tch_stes_left_eca_txt = string.IsNullOrEmpty(txt_tch_stes_left_eca_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_left_eca_txt.Text);
                //double tch_stes_left_cca_txt = string.IsNullOrEmpty(txt_tch_stes_left_cca_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_left_cca_txt.Text);
                //double tch_stes_left_va_txt = string.IsNullOrEmpty(txt_tch_stes_left_va_txt.Text) ? 0 : Convert.ToSingle(txt_tch_stes_left_va_txt.Text);
                //Carotid_hdr.tch_stes_right_ica_txt = tch_stes_right_ica_txt;
                //Carotid_hdr.tch_stes_right_eca_txt = tch_stes_right_eca_txt;
                //Carotid_hdr.tch_stes_right_cca_txt = tch_stes_right_cca_txt;
                //Carotid_hdr.tch_stes_right_va_txt = tch_stes_right_va_txt;
                //Carotid_hdr.tch_stes_left_ica_txt = tch_stes_left_ica_txt;
                //Carotid_hdr.tch_stes_left_eca_txt = tch_stes_left_eca_txt;
                //Carotid_hdr.tch_stes_left_cca_txt = tch_stes_left_cca_txt;
                //Carotid_hdr.tch_stes_left_va_txt = tch_stes_left_va_txt;
                Carotid_hdr.tch_create_by = Program.CurrentUser.mut_username;
                Carotid_hdr.tch_create_date = datenowvalue;
                Carotid_hdr.tch_update_by = Program.CurrentUser.mut_username;
                Carotid_hdr.tch_update_date = Carotid_hdr.tch_create_date;
                bindingSourceCarotid.EndEdit();
                dbc.SubmitChanges();
                saveIsCompleted = true;
                if (saveIsCompleted == true)
                {
                    lblAlert.Focus();
                    lblAlert.Text = "Save data completed.";
                }
            }
            if (tb_tech == true)
            {
                //////[trn_carotid_tech]
                //Carotid_Tech.tct_result = true;
                Carotid_Tech.tpr_id = Program.CurrentRegis.tpr_id;
                Carotid_Tech.tct_type = 'N';
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
                else if (chkappoint.Checked == false)
                {
                    dt_appoint = null;
                    Carotid_Tech.tct_appoint_doctor_date = dt_appoint;
                }
                Carotid_Tech.tct_doctor_code = txtDoctorCode.Text;
                Carotid_Tech.tct_doctor_name = txtDoctorName.Text;
                Carotid_Tech.tct_appoint_status = Program.GetValueRadio(pnlAppointStatus);
                Carotid_Tech.tct_doctor_code = txtDoctorCode.Text;
                Carotid_Tech.tct_doctor_name = txtDoctorName.Text;
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
                if (chk_prn.Checked == true)
                {
                    Carotid_Tech.tct_follow_up = dt_followup;
                }
                else if (chk_prn.Checked == false)
                {
                    dt_followup = null;
                    Carotid_Tech.tct_appoint_doctor_date = dt_followup;
                }
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
                    lblAlert.Focus();
                    lblAlert.Text = "Save data completed.";
                    
                }
            }
            btnSaveAsDraft.Enabled = true;
        }
        private string SumString(string str1, string str2, string str3)
        {
            return (str1 == null ? "" : str1) + "|" + (str2 == null ? "" : str2) + "|" + (str3 == null ? "" : str3);
            //string suntext = "";
            //if (str1.Length > 0)
            //{
            //    suntext = str1.Trim();
            //}
            //if (str2.Length > 0)
            //{
            //    suntext = str2.Trim();
            //}
            //else
            //{
            //    suntext = "|" + str2.Trim();
            //}
            //if (str3.Length > 0)
            //{
            //    suntext = str3.Trim();
            //}
            //else
            //{
            //    suntext = "|" + str3.Trim();
            //}
            //return suntext;
        }
        private void SubString(string strarray, ref TextBox txt1, ref TextBox txt2, ref TextBox txt3)
        {
            string[] str = strarray.Split('|');
            string[] datavalue = str;
            if (str.Length > 0)
                txt1.Text = datavalue[0];
            if (str.Length > 1)
                txt2.Text = datavalue[1];
            if (str.Length > 2)
                txt3.Text = datavalue[2];
        }

        private void ObjTechnician()
        {
            var ObjTechnician = (from tbl in dbc.trn_carotid_teches
                                 where tbl.tpr_id == Program.CurrentRegis.tpr_id
                                 join tbl2 in dbc.mst_user_types on tbl.tct_create_by equals tbl2.mut_username
                                 where tbl.tpr_id == Program.CurrentRegis.tpr_id
                                 select tbl2.mut_fullname).FirstOrDefault();
            txttechnician.Text = ObjTechnician.ToString();
        }
        #endregion

        #region Events
        private void frmCarotidHistory_Load(object sender, EventArgs e)
        {
            GetPatient();
            GetConsultDoc();
            Program.GetRoomName();
            /*uiFooter1.RoomCode = "CD";
            uiFooter1.LoadData();*/
            if (tprid == 0)
            {
                btnSaveAsDraft.Enabled = false;
                tabControl1.Enabled = false;
            }
            else
            {
                btnSaveAsDraft.Enabled = true;
            }
        }

        private void GV_Null_Result_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i <= GV_Null_Result.Rows.Count - 1; i++)
            {
                GV_Null_Result.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void GV_Result_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i <= GV_Result.Rows.Count - 1; i++)
            {
                GV_Result.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void GV_Null_Result_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            uiProfileHorizontal1.Focus();
            ClsControl();
            if (e.RowIndex >= 0)
            {
                Gv_GetDataPatiet(this.GV_Null_Result);
                gv_rst = 0;
                btnSaveAsDraft.Enabled = true;
                if (gv_rst == 0)
                {
                    panel3.Enabled = true;
                    panel5.Enabled = true;
                }
            }
            Loadfrm();
            ObjTechnician();
            tabControl1.TabPages["tabPage1"].AutoScrollPosition = new Point(0, 0);
            tabControl1.TabPages["tabPage2"].AutoScrollPosition = new Point(0, 0);
            panel1.AutoScrollPosition = new Point(0, 0);
            this.AutoScrollPosition = new Point(0, 0);

            pictureBoxLeft.Enabled = true;
            pictureBoxRight.Enabled = true;
            GBpaint.Enabled = true;
            textBox1.Enabled = true;
            txttechnician.Enabled = true;
            pbResultFromDoc.Enabled = true;
        }

        private void GV_Result_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            uiProfileHorizontal1.Focus();
            ClsControl();
            btnSaveAsDraft.Enabled = true;
            if (e.RowIndex >= 0)
            {
                Gv_GetDataPatiet(this.GV_Result);
                gv_rst = 1;
                if (gv_rst == 1)
                {
                    panel3.Enabled = false;
                    panel5.Enabled = false;
                }
            }
            Loadfrm();
            ObjTechnician();
            tabControl1.TabPages["tabPage1"].AutoScrollPosition = new Point(0, 0);
            tabControl1.TabPages["tabPage2"].AutoScrollPosition = new Point(0, 0);
            panel1.AutoScrollPosition = new Point(0, 0);
            this.AutoScrollPosition = new Point(0, 0);

            pictureBoxLeft.Enabled = false;
            pictureBoxRight.Enabled = false;
            GBpaint.Enabled = false;
            textBox1.Enabled = false;
            txttechnician.Enabled = false;
            pbResultFromDoc.Enabled = false;
        }

        private void txtDoctorName_KeyUp(object sender, KeyEventArgs e)
       {
            //GridDoctorName.Location = new Point(12, 320);
            SearchGetDoctor(txtDoctorName.Text.Trim());
        }

        private void GridDoctorName_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var selectedItems = GridDoctorName.CurrentRow;
                trn_carotid_tech objcurrenttpr = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                objcurrenttpr.tct_doctor_code = selectedItems.Cells[1].Value.ToString();
                objcurrenttpr.tct_doctor_name = selectedItems.Cells[0].Value.ToString();
                txtDoctorCode.Text = objcurrenttpr.tct_doctor_code;
                txtDoctorName.Text = objcurrenttpr.tct_doctor_name;
                txtDoctorName.Focus();
            }
            catch (Exception)
            {
            }
            GridDoctorName.Visible = false;
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

        private void btnSaveAsDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (gv_rst == 0)
                {
                    Save_DB(gv_rst, true, true);
                    GetPatient();
                    this.AutoScrollPosition = new Point(0, 0);
                }
                else if(gv_rst == 1)
                {
                    Save_DB(gv_rst, false, true);
                    GetPatient();
                    this.AutoScrollPosition = new Point(0, 0);
                }
            }
            catch (Exception ex)
            {
                lblAlert.Text = ex.Message;
            }
        }

        private void txt_tch_artery_right_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.')
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

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
                mdrid = null;
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
                dateTimeFollowup.Enabled = false;
                dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                dateTimeFollowup.CustomFormat = " ";
                dt_followup = null;
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

        private void cmb_mdrID_SelectedValueChanged(object sender, EventArgs e)
        {
            mdrid = cmb_mdrID.SelectedValue.ToString();
        }

        private void txtDoctorName_Leave(object sender, EventArgs e)
        {
            GridDoctorName.Visible = false;
            if (txtDoctorName.Text == "")
            {
                txtDoctorCode.Text = "";
            }
        }
        #endregion

        private void uiFooter1_Load(object sender, EventArgs e)
        {

        }

        private void GridDoctorName_Leave(object sender, EventArgs e)
        {
            GridDoctorName.Visible = false;
        }

        private void btnBrushErase_Click(object sender, EventArgs e)
        {
            Brush = !Brush;
            Brush2 = !Brush2;
            if (Brush == true & Brush2 == true)
            {
                lblstatusPaint.Text = "Brush";
            }
            else
            {
                lblstatusPaint.Text = "Erase";
            }
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

        private void btnremark_Click(object sender, EventArgs e)
        {
            IsRemark_L = !IsRemark_L;
            IsRemark_R = !IsRemark_R;
            if (IsRemark_L == true && IsRemark_R == true)
            {
                lblS_remark.Text = "Remark";
            }
            else
            {
                lblS_remark.Text = "Null";
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
        private void pictureBoxLeft_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsRemark_L == true && Brush == true)
            {
                RemarkOnImgage(this.pictureBoxLeft, e, "L");
            }
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
            {
                IsPainting = false;
            }
            if (IsEraseing)
            {
                IsEraseing = false;
            }
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

        private void pictureBoxRight_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsRemark_R == true && Brush2 == true)
            {
                RemarkOnImgage(this.pictureBoxRight, e, "R");
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
            {
                IsPainting2 = false;
            }
            if (IsEraseing2)
            {
                IsEraseing2 = false;
            }
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

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) & (Keys)e.KeyChar != Keys.Back & e.KeyChar != '.' &  ((TextBox)sender).Text.IndexOf(".", 0) > 0)
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void frmCarotidHistory_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.CurrentRegis = null;
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
