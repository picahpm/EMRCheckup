using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Windows.Forms.DataVisualization.Charting;

namespace CheckupBO
{
    public partial class frmViewHearing : Form
    {
        public frmViewHearing()
        {
            InitializeComponent();
        }
        public int TprID { get; set; }
        public int siteitem { get; set; }
        private void LoadHistory(int tpr_id)
        {
            var objdocter_hdr = (from t1 in dbc.trn_audiometric_hdrs
                                 where t1.tpr_id == tpr_id
                                 select new
                                 {
                                     EN = t1.trn_patient_regi.tpr_en_no,
                                     ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                     CreateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tdh_create_by).Single().mut_fullname,
                                     CreateDate = t1.tdh_create_date,
                                     UpdateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tdh_create_by).Single().mut_fullname,
                                     UpdateDate = t1.tdh_create_date,
                                     Link = "Link"
                                 }).ToList();
            gridHistory.DataSource = objdocter_hdr;
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        private void frmViewHearing_Load(object sender, EventArgs e)
        {
            
            selectAudio_R_L();
            Loadfrm();
        }
        private void selectAudio_R_L()
        {
            try
            {
                var mrmid = 16;
                var currentmrm = (from t1 in dbc.mst_room_hdrs where t1.mrm_code == "HS"  select t1).FirstOrDefault();
                if (currentmrm != null)
                {
                    mrmid = currentmrm.mrm_id;
                }
                var ddlAudio = (from t1 in dbc.mst_doc_result_hdrs
                                join t2 in dbc.mst_doc_results on
                                t1.mrh_id equals t2.mrh_id
                                where t1.mrm_id == mrmid
                                       && t1.mrh_status == 'A'
                                       && t1.mrh_code == "HR"
                                select new
                                {
                                    mdr_ename = t2.mdr_ename,
                                    mdr_val = t2.mdr_id
                                }).ToList();
                cmbCommentR.DataSource = ddlAudio.Select((item, index) => new
                {
                    item.mdr_ename,
                    item.mdr_val
                }).ToList();
                cmbCommentR.DisplayMember = "mdr_ename";
                cmbCommentR.ValueMember = "mdr_val";
                cmbCommentL.DataSource = ddlAudio.Select((item, index) => new
                {
                    item.mdr_ename,
                    item.mdr_val
                }).ToList();
                cmbCommentL.DisplayMember = "mdr_ename";
                cmbCommentL.ValueMember = "mdr_val";
            }
            catch (Exception e)
            {
                MessageBox.Show("catch : " + e.Message);
            }
        }

        public void Loadfrm()
        {
            if (TprID != 0)
            {
                tabControl1.Enabled = true;
                this.LoadData(TprID);
                LoadHistory();
            }
            else
            {
                    trnaudiometrichdrBindingSource.DataSource = (from t1 in dbc.trn_audiometric_hdrs select t1);
                    trnaudiometrichdrBindingSource.AddNew();
                    tabControl1.Enabled = false;
            }
        }
        private void LoadHistory()
        {
            var objdocter_hdr = (from t1 in dbc.trn_audiometric_hdrs
                                 where t1.tpr_id == TprID
                                 select new
                                 {
                                     EN = t1.trn_patient_regi.tpr_en_no,
                                     ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                     CreateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tdh_create_by).Single().mut_fullname,
                                     CreateDate = t1.tdh_create_date,
                                     UpdateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tdh_create_by).Single().mut_fullname,
                                     UpdateDate = t1.tdh_create_date,
                                     Link = "Link"
                                 }).ToList();
            gridHistory.DataSource = objdocter_hdr;
        }

        private void LoadData(int tprid)
        {
            var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1).FirstOrDefault();
            trn_audiometric_hdr obj = dbc.trn_audiometric_hdrs.Where(c => c.tpr_id == tprid).FirstOrDefault();
            if (obj != null)
            {
                trnaudiometrichdrBindingSource.DataSource = obj;
                if (currentRegis == null)
                {
                    trnaudiometrichdrBindingSource.DataSource = (from t1 in dbc.trn_audiometric_hdrs select t1);
                    trnaudiometrichdrBindingSource.AddNew();
                }
                else
                {
                    trnaudiometrichdrBindingSource.DataSource = obj;
                    trn_audiometric_hdr trnaud = (trn_audiometric_hdr)trnaudiometrichdrBindingSource.Current;
                    Program.SetValueRadioGroup(panel3, trnaud.tdh_hearing_machine.ToString());
                    Program.SetValueRadioGroup(pnlBelive, trnaud.tdh_believable.ToString());
                    txtAirRt.Text = trnaud.tdh_right_air_pt_ave.ToString();
                    txtAirLt.Text = trnaud.tdh_left_air_pt_ave.ToString();
                    txtBoneRt.Text = trnaud.tdh_right_bone_pt_ave.ToString();
                    txtBoneLt.Text = trnaud.tdh_left_bone_pt_ave.ToString();
                    cmbCommentL.SelectedValue = Convert.ToInt32(trnaud.mdr_id_left);
                    cmbCommentR.SelectedValue = Convert.ToInt32(trnaud.mdr_id_right);
                    txtRt250.Text = trnaud.tdh_right_level_250.ToString();
                    txtLt250.Text = trnaud.tdh_left_level_250.ToString();
                    txtRt500.Text = trnaud.tdh_right_level_500.ToString();
                    txtLt500.Text = trnaud.tdh_left_level_500.ToString();
                    txtRt750.Text = trnaud.tdh_right_level_750.ToString();
                    txtLt750.Text = trnaud.tdh_left_level_750.ToString();
                    txtRt1000.Text = trnaud.tdh_right_level_1000.ToString();
                    txtLt1000.Text = trnaud.tdh_left_level_1000.ToString();
                    txtRt1500.Text = trnaud.tdh_right_level_1500.ToString();
                    txtLt1500.Text = trnaud.tdh_left_level_1500.ToString();
                    txtRt2000.Text = trnaud.tdh_right_level_2000.ToString();
                    txtLt2000.Text = trnaud.tdh_left_level_2000.ToString();
                    txtRt3000.Text = trnaud.tdh_right_level_3000.ToString();
                    txtLt3000.Text = trnaud.tdh_left_level_3000.ToString();
                    txtRt4000.Text = trnaud.tdh_right_level_4000.ToString();
                    txtLt4000.Text = trnaud.tdh_left_level_4000.ToString();
                    txtRt5000.Text = trnaud.tdh_right_level_6000.ToString();
                    txtLt5000.Text = trnaud.tdh_left_level_6000.ToString();
                    txtRt6000.Text = trnaud.tdh_right_level_8000.ToString();
                    txtLt6000.Text = trnaud.tdh_left_level_8000.ToString();
                    if (txtRt250.Text == "0")
                        txtRt250.Text = "";
                    if (txtLt250.Text == "0")
                        txtLt250.Text = "";
                    if (txtRt500.Text == "0")
                        txtRt500.Text = "";
                    if (txtLt500.Text == "0")
                        txtLt500.Text = "";
                    if (txtRt750.Text == "0")
                        txtRt750.Text = "";
                    if (txtLt750.Text == "0")
                        txtLt750.Text = "";
                    if (txtRt1000.Text == "0")
                        txtRt1000.Text = "";
                    if (txtLt1000.Text == "0")
                        txtLt1000.Text = "";
                    if (txtRt1500.Text == "0")
                        txtRt1500.Text = "";
                    if (txtLt1500.Text == "0")
                        txtLt1500.Text = "";
                    if (txtRt2000.Text == "0")
                        txtRt2000.Text = "";
                    if (txtLt2000.Text == "0")
                        txtLt2000.Text = "";
                    if (txtRt3000.Text == "0")
                        txtRt3000.Text = "";
                    if (txtLt3000.Text == "0")
                        txtLt3000.Text = "";
                    if (txtRt4000.Text == "0")
                        txtRt4000.Text = "";
                    if (txtLt4000.Text == "0")
                        txtLt4000.Text = "";
                    if (txtRt5000.Text == "0")
                        txtRt5000.Text = "";
                    if (txtLt5000.Text == "0")
                        txtLt5000.Text = "";
                    if (txtRt6000.Text == "0")
                        txtRt6000.Text = "";
                    if (txtLt6000.Text == "0")
                        txtLt6000.Text = "";
                    if (trnaud.tdh_right_air_pt_ave.ToString() == "0")
                        txtAirRt.Text = "";
                    if (trnaud.tdh_left_air_pt_ave.ToString() == "0")
                        txtAirLt.Text = "";
                    if (trnaud.tdh_right_bone_pt_ave.ToString() == "0")
                        txtBoneRt.Text = "";
                    if (trnaud.tdh_left_bone_pt_ave.ToString() == "0")
                        txtBoneLt.Text = "";
                }
                btnGraph_Click(null, null);
            }
            else
            {
                trnaudiometrichdrBindingSource.DataSource = (from t1 in dbc.trn_audiometric_hdrs select t1);
                trnaudiometrichdrBindingSource.AddNew();
            }
        }
        private void Clearfrm()
        {
            trn_audiometric_hdr obj = new trn_audiometric_hdr();
            trnaudiometrichdrBindingSource.DataSource = obj;
            dbc.Dispose();
            dbc = new InhCheckupDataContext();
            txtRight_thai.Text = null;
            txtLeft_thai.Text = null;
            clrpanel(panel3);
            clrpanel(pnlBelive);
            clrGroupbox(groupBox5);
            cmbCommentL.SelectedIndex = 0;
            cmbCommentR.SelectedIndex = 0;
        }
        private void clrpanel(Panel pnl)
        {
            foreach (Control ctl in pnl.Controls)
            {
                RadioButton rb;
                if (ctl is RadioButton)
                {
                    rb = (RadioButton)ctl;
                    rb.Checked = false;
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
                    txt.Text = null;
                }
            }
        }
        public bool SaveData(char type)
        {
            trn_audiometric_hdr Audiometric = (trn_audiometric_hdr)trnaudiometrichdrBindingSource.Current;
            Boolean saveIsCompleted = false;
            DateTime datenowvalue = Program.GetServerDateTime();
            int AudioR = Utility.GetInteger(cmbCommentR.SelectedValue);
            int AudioL = Utility.GetInteger(cmbCommentL.SelectedValue);
            int sum1 = Utility.GetInteger(txtRt250.Text);
            int sum2 = Utility.GetInteger(txtRt500.Text);
            int sum3 = Utility.GetInteger(txtRt750.Text);
            int sum4 = Utility.GetInteger(txtRt1000.Text);
            int sum5 = Utility.GetInteger(txtRt1500.Text);
            int sum6 = Utility.GetInteger(txtRt2000.Text);
            int sum7 = Utility.GetInteger(txtRt3000.Text);
            int sum8 = Utility.GetInteger(txtRt4000.Text);
            int sum9 = Utility.GetInteger(txtRt5000.Text);
            int sum10 = Utility.GetInteger(txtRt6000.Text);
            ////Left
            int sum11 = Utility.GetInteger(txtLt250.Text);
            int sum12 = Utility.GetInteger(txtLt500.Text);
            int sum13 = Utility.GetInteger(txtLt750.Text);
            int sum14 = Utility.GetInteger(txtLt1000.Text);
            int sum15 = Utility.GetInteger(txtLt1500.Text);
            int sum16 = Utility.GetInteger(txtLt2000.Text);
            int sum17 = Utility.GetInteger(txtLt3000.Text);
            int sum18 = Utility.GetInteger(txtLt4000.Text);
            int sum19 = Utility.GetInteger(txtLt5000.Text);
            int sum20 = Utility.GetInteger(txtLt6000.Text);
            int R_air_pt = Utility.GetInteger(txtAirRt.Text);
            int L_air_pt = Utility.GetInteger(txtAirLt.Text);
            int R_bone_pt = Utility.GetInteger(txtBoneRt.Text);
            int L_bone_pt = Utility.GetInteger(txtBoneLt.Text);
            Audiometric.mdr_id_left = AudioL;
            Audiometric.mdr_id_right = AudioR;
            Audiometric.tdh_type = type;
            Audiometric.tdh_hearing_machine = Program.GetValueRadioTochar(panel3);
            Audiometric.tdh_right_level_250 = sum1;
            Audiometric.tdh_left_level_250 = sum11;
            Audiometric.tdh_right_level_500 = sum2;
            Audiometric.tdh_left_level_500 = sum12;
            Audiometric.tdh_right_level_750 = sum3;
            Audiometric.tdh_left_level_750 = sum13;
            Audiometric.tdh_right_level_1000 = sum4;
            Audiometric.tdh_left_level_1000 = sum14;
            Audiometric.tdh_right_level_1500 = sum5;
            Audiometric.tdh_left_level_1500 = sum5;
            Audiometric.tdh_right_level_2000 = sum6;
            Audiometric.tdh_left_level_2000 = sum16;
            Audiometric.tdh_right_level_3000 = sum7;
            Audiometric.tdh_left_level_3000 = sum17;
            Audiometric.tdh_right_level_4000 = sum8;
            Audiometric.tdh_left_level_4000 = sum18;
            Audiometric.tdh_right_level_6000 = sum9;
            Audiometric.tdh_left_level_6000 = sum19;
            Audiometric.tdh_right_level_8000 = sum10;
            Audiometric.tdh_left_level_8000 = sum20;
            if (siteitem == 0)
            {
                Audiometric.tpr_id =TprID;
            }
            Audiometric.tdh_believable = Program.GetValueRadioTochar(pnlBelive);
            Audiometric.tdh_right_air_pt_ave = R_air_pt;
            Audiometric.tdh_left_air_pt_ave = L_air_pt;
            Audiometric.tdh_right_bone_pt_ave = R_bone_pt;
            Audiometric.tdh_left_bone_pt_ave = L_bone_pt;
            if (Audiometric.tdh_create_by == null)
            {
                Audiometric.tdh_create_by = Program.CurrentUser.mut_username;
                Audiometric.tdh_create_date = datenowvalue;
            }
            Audiometric.tdh_update_by = Program.CurrentUser.mut_username;
            Audiometric.tdh_update_date = datenowvalue;
            trnaudiometrichdrBindingSource.EndEdit();
            dbc.SubmitChanges();
            saveIsCompleted = true;
            if (saveIsCompleted == true)
            {
                lblMsg.Text = "Save Data Completed.";
            }
            return saveIsCompleted;
        }

        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData('D'))
                {
                    
                    lblMsg.Text = "Save Data Completed.";
                    this.AutoScrollPosition = new Point(0, 0);
                    lblMsg.Focus();
                    btnG_hst_Click(null, null);
                }
                else
                {
                    lblMsg.Text = "Failed !";
                    lblMsg.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Focus();
                lblMsg.Text = ex.Message;
            }
        }

        private void btnCleargraph_Click(object sender, EventArgs e)
        {
            chart1.Series["Lt"].Points.Clear();
            chart1.Series["Rt"].Points.Clear();
            txtRt250.Text = string.Empty;
            txtRt500.Text = string.Empty;
            txtRt750.Text = string.Empty;
            txtRt1000.Text = string.Empty;
            txtRt1500.Text = string.Empty;
            txtRt2000.Text = string.Empty;
            txtRt3000.Text = string.Empty;
            txtRt4000.Text = string.Empty;
            txtRt5000.Text = string.Empty;
            txtRt6000.Text = string.Empty;
            txtLt250.Text = string.Empty;
            txtLt500.Text = string.Empty;
            txtLt500.Text = string.Empty;
            txtLt750.Text = string.Empty;
            txtLt1000.Text = string.Empty;
            txtLt1500.Text = string.Empty;
            txtLt1000.Text = string.Empty;
            txtLt2000.Text = string.Empty;
            txtLt3000.Text = string.Empty;
            txtLt4000.Text = string.Empty;
            txtLt5000.Text = string.Empty;
            txtLt6000.Text = string.Empty;
        }
        private void btnGraph_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRt250.Text) && string.IsNullOrEmpty(txtRt500.Text) && string.IsNullOrEmpty(txtRt750.Text) && string.IsNullOrEmpty(txtRt1000.Text)
                && string.IsNullOrEmpty(txtRt1500.Text) && string.IsNullOrEmpty(txtRt2000.Text) && string.IsNullOrEmpty(txtRt3000.Text) && string.IsNullOrEmpty(txtRt4000.Text)
                 && string.IsNullOrEmpty(txtRt5000.Text) && string.IsNullOrEmpty(txtRt6000.Text) && string.IsNullOrEmpty(txtLt250.Text) && string.IsNullOrEmpty(txtLt500.Text)
                && string.IsNullOrEmpty(txtLt750.Text) && string.IsNullOrEmpty(txtLt1000.Text) && string.IsNullOrEmpty(txtLt1500.Text) && string.IsNullOrEmpty(txtLt2000.Text)
                && string.IsNullOrEmpty(txtLt3000.Text) && string.IsNullOrEmpty(txtLt4000.Text) && string.IsNullOrEmpty(txtLt5000.Text) && string.IsNullOrEmpty(txtLt6000.Text))
                {
                    chart1.Series["Lt"].Points.Clear();
                    chart1.Series["Rt"].Points.Clear();
                }
                else
                {
                    if (string.IsNullOrEmpty(txtRt250.Text) && string.IsNullOrEmpty(txtRt500.Text) && string.IsNullOrEmpty(txtRt750.Text) && string.IsNullOrEmpty(txtRt1000.Text)
                        && string.IsNullOrEmpty(txtRt1500.Text) && string.IsNullOrEmpty(txtRt2000.Text) && string.IsNullOrEmpty(txtRt3000.Text) && string.IsNullOrEmpty(txtRt4000.Text)
                        && string.IsNullOrEmpty(txtRt5000.Text) && string.IsNullOrEmpty(txtRt6000.Text))
                    {
                        chart1.Series["Rt"].Points.Clear();
                    }
                    if (string.IsNullOrEmpty(txtLt250.Text) && string.IsNullOrEmpty(txtLt500.Text) && string.IsNullOrEmpty(txtLt750.Text) && string.IsNullOrEmpty(txtLt1000.Text)
                         && string.IsNullOrEmpty(txtLt1500.Text) && string.IsNullOrEmpty(txtLt2000.Text) && string.IsNullOrEmpty(txtLt3000.Text) && string.IsNullOrEmpty(txtLt4000.Text)
                        && string.IsNullOrEmpty(txtLt5000.Text) && string.IsNullOrEmpty(txtLt6000.Text))
                    {
                        chart1.Series["Lt"].Points.Clear();
                    }
                    int sum1 = string.IsNullOrEmpty(txtRt250.Text) ? 0 : Convert.ToInt32(txtRt250.Text);
                    int sum2 = string.IsNullOrEmpty(txtRt500.Text) ? 0 : Convert.ToInt32(txtRt500.Text);
                    int sum3 = string.IsNullOrEmpty(txtRt750.Text) ? 0 : Convert.ToInt32(txtRt750.Text);
                    int sum4 = string.IsNullOrEmpty(txtRt1000.Text) ? 0 : Convert.ToInt32(txtRt1000.Text);
                    int sum5 = string.IsNullOrEmpty(txtRt1500.Text) ? 0 : Convert.ToInt32(txtRt1500.Text);
                    int sum6 = string.IsNullOrEmpty(txtRt2000.Text) ? 0 : Convert.ToInt32(txtRt2000.Text);
                    int sum7 = string.IsNullOrEmpty(txtRt3000.Text) ? 0 : Convert.ToInt32(txtRt3000.Text);
                    int sum8 = string.IsNullOrEmpty(txtRt4000.Text) ? 0 : Convert.ToInt32(txtRt4000.Text);
                    int sum9 = string.IsNullOrEmpty(txtRt5000.Text) ? 0 : Convert.ToInt32(txtRt5000.Text);
                    int sum10 = string.IsNullOrEmpty(txtRt6000.Text) ? 0 : Convert.ToInt32(txtRt6000.Text);
                    int sum11 = string.IsNullOrEmpty(txtLt250.Text) ? 0 : Convert.ToInt32(txtLt250.Text);
                    int sum12 = string.IsNullOrEmpty(txtLt500.Text) ? 0 : Convert.ToInt32(txtLt500.Text);
                    int sum13 = string.IsNullOrEmpty(txtLt750.Text) ? 0 : Convert.ToInt32(txtLt750.Text);
                    int sum14 = string.IsNullOrEmpty(txtLt1000.Text) ? 0 : Convert.ToInt32(txtLt1000.Text);
                    int sum15 = string.IsNullOrEmpty(txtLt1500.Text) ? 0 : Convert.ToInt32(txtLt1500.Text);
                    int sum16 = string.IsNullOrEmpty(txtLt2000.Text) ? 0 : Convert.ToInt32(txtLt2000.Text);
                    int sum17 = string.IsNullOrEmpty(txtLt3000.Text) ? 0 : Convert.ToInt32(txtLt3000.Text);
                    int sum18 = string.IsNullOrEmpty(txtLt4000.Text) ? 0 : Convert.ToInt32(txtLt4000.Text);
                    int sum19 = string.IsNullOrEmpty(txtLt5000.Text) ? 0 : Convert.ToInt32(txtLt5000.Text);
                    int sum20 = string.IsNullOrEmpty(txtLt6000.Text) ? 0 : Convert.ToInt32(txtLt6000.Text);
                    if (sum1 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        chart1.Series["Rt"].Points.AddXY(250, sum1);
                    }
                    if (sum2 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                        }
                        if (sum2 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                        }
                        else
                        {
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                        }
                    }
                    if (sum3 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                        }
                    }
                    if (sum4 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0 && sum3 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            if (sum3 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(750, sum3);
                            }
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                        }
                    }
                    if (sum5 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0 && sum3 != 0 && sum4 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            chart1.Series["Rt"].Points.AddXY(1500, sum5);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            if (sum3 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(750, sum3);
                            }
                            if (sum4 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            }
                            chart1.Series["Rt"].Points.AddXY(1500, sum5);
                        }
                    }
                    if (sum6 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0 && sum3 != 0 && sum4 != 0 && sum5 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            chart1.Series["Rt"].Points.AddXY(2000, sum6);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            if (sum3 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(750, sum3);
                            }
                            if (sum4 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            }
                            if (sum5 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            }
                            chart1.Series["Rt"].Points.AddXY(2000, sum6);
                        }
                    }
                    if (sum7 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0 && sum3 != 0 && sum4 != 0 && sum5 != 0 && sum6 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            chart1.Series["Rt"].Points.AddXY(3000, sum7);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            if (sum3 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(750, sum3);
                            }
                            if (sum4 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            }
                            if (sum5 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            }
                            if (sum6 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            }
                            chart1.Series["Rt"].Points.AddXY(3000, sum7);
                        }
                    }
                    if (sum8 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0 && sum3 != 0 && sum4 != 0 && sum5 != 0 && sum6 != 0 && sum7 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            chart1.Series["Rt"].Points.AddXY(3000, sum7);
                            chart1.Series["Rt"].Points.AddXY(4000, sum8);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            if (sum3 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(750, sum3);
                            }
                            if (sum4 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            }
                            if (sum5 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            }
                            if (sum6 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            }
                            if (sum7 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(3000, sum7);
                            }
                            chart1.Series["Rt"].Points.AddXY(4000, sum8);
                        }
                    }
                    if (sum9 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0 && sum3 != 0 && sum4 != 0 && sum5 != 0 && sum6 != 0 && sum7 != 0 && sum8 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            chart1.Series["Rt"].Points.AddXY(3000, sum7);
                            chart1.Series["Rt"].Points.AddXY(4000, sum8);
                            chart1.Series["Rt"].Points.AddXY(6000, sum9);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            if (sum3 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(750, sum3);
                            }
                            if (sum4 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            }
                            if (sum5 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            }
                            if (sum6 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            }
                            if (sum7 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(3000, sum7);
                            }
                            if (sum8 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(4000, sum8);
                            }
                            chart1.Series["Rt"].Points.AddXY(6000, sum9);
                        }
                    }
                    if (sum10 != 0)
                    {
                        chart1.Series["Rt"].Points.Clear();
                        if (sum1 != 0 && sum2 != 0 && sum3 != 0 && sum4 != 0 && sum5 != 0 && sum6 != 0 && sum7 != 0 && sum8 != 0 && sum9 != 0)
                        {
                            chart1.Series["Rt"].Points.AddXY(250, sum1);
                            chart1.Series["Rt"].Points.AddXY(500, sum2);
                            chart1.Series["Rt"].Points.AddXY(750, sum3);
                            chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            chart1.Series["Rt"].Points.AddXY(3000, sum7);
                            chart1.Series["Rt"].Points.AddXY(4000, sum8);
                            chart1.Series["Rt"].Points.AddXY(6000, sum9);
                            chart1.Series["Rt"].Points.AddXY(8000, sum10);
                        }
                        else
                        {
                            if (sum1 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(250, sum1);
                            }
                            if (sum2 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(500, sum2);
                            }
                            if (sum3 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(750, sum3);
                            }
                            if (sum4 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1000, sum4);
                            }
                            if (sum5 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(1500, sum5);
                            }
                            if (sum6 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(2000, sum6);
                            }
                            if (sum7 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(3000, sum7);
                            }
                            if (sum8 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(4000, sum8);
                            }
                            if (sum9 != 0)
                            {
                                chart1.Series["Rt"].Points.AddXY(6000, sum9);
                            }
                            chart1.Series["Rt"].Points.AddXY(8000, sum10);
                        }
                    }
                    if (sum11 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        chart1.Series["Lt"].Points.AddXY(250, sum11);
                    }
                    if (sum12 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        chart1.Series["Lt"].Points.AddXY(500, sum12);
                        if (sum11 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                        }
                    }
                    if (sum13 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                        }
                    }
                    if (sum14 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0 && sum13 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            if (sum13 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(750, sum13);
                            }
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                        }
                    }
                    if (sum15 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0 && sum13 != 0 && sum14 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            chart1.Series["Lt"].Points.AddXY(1500, sum15);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            if (sum13 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(750, sum13);
                            }
                            if (sum14 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            }
                            chart1.Series["Lt"].Points.AddXY(1500, sum15);
                        }
                    }
                    if (sum16 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0 && sum13 != 0 && sum14 != 0 && sum15 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            chart1.Series["Lt"].Points.AddXY(2000, sum16);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            if (sum13 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(750, sum13);
                            }
                            if (sum14 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            }
                            if (sum15 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            }
                            chart1.Series["Lt"].Points.AddXY(2000, sum16);
                        }
                    }
                    if (sum17 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0 && sum13 != 0 && sum14 != 0 && sum15 != 0 && sum16 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            chart1.Series["Lt"].Points.AddXY(3000, sum17);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            if (sum13 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(750, sum13);
                            }
                            if (sum14 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            }
                            if (sum15 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            }
                            if (sum16 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            }
                            chart1.Series["Lt"].Points.AddXY(3000, sum17);
                        }
                    }
                    if (sum18 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0 && sum13 != 0 && sum14 != 0 && sum15 != 0 && sum16 != 0 && sum17 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            chart1.Series["Lt"].Points.AddXY(3000, sum17);
                            chart1.Series["Lt"].Points.AddXY(4000, sum18);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            if (sum13 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(750, sum13);
                            }
                            if (sum14 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            }
                            if (sum15 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            }
                            if (sum16 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            }
                            if (sum17 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(3000, sum17);
                            }
                            chart1.Series["Lt"].Points.AddXY(4000, sum18);
                        }
                    }
                    if (sum19 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0 && sum13 != 0 && sum14 != 0 && sum15 != 0 && sum16 != 0 && sum17 != 0 && sum18 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            chart1.Series["Lt"].Points.AddXY(3000, sum17);
                            chart1.Series["Lt"].Points.AddXY(4000, sum18);
                            chart1.Series["Lt"].Points.AddXY(6000, sum19);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            if (sum13 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(750, sum13);
                            }
                            if (sum14 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            }
                            if (sum15 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            }
                            if (sum16 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            }
                            if (sum17 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(3000, sum17);
                            }
                            if (sum18 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(4000, sum18);
                            }
                            chart1.Series["Lt"].Points.AddXY(6000, sum19);
                        }
                    }
                    if (sum20 != 0)
                    {
                        chart1.Series["Lt"].Points.Clear();
                        if (sum11 != 0 && sum12 != 0 && sum13 != 0 && sum14 != 0 && sum15 != 0 && sum16 != 0 && sum17 != 0 && sum18 != 0 && sum19 != 0)
                        {
                            chart1.Series["Lt"].Points.AddXY(250, sum11);
                            chart1.Series["Lt"].Points.AddXY(500, sum12);
                            chart1.Series["Lt"].Points.AddXY(750, sum13);
                            chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            chart1.Series["Lt"].Points.AddXY(3000, sum17);
                            chart1.Series["Lt"].Points.AddXY(4000, sum18);
                            chart1.Series["Lt"].Points.AddXY(6000, sum19);
                            chart1.Series["Lt"].Points.AddXY(8000, sum20);
                        }
                        else
                        {
                            if (sum11 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(250, sum11);
                            }
                            if (sum12 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(500, sum12);
                            }
                            if (sum13 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(750, sum13);
                            }
                            if (sum14 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1000, sum14);
                            }
                            if (sum15 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(1500, sum15);
                            }
                            if (sum16 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(2000, sum16);
                            }
                            if (sum17 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(3000, sum17);
                            }
                            if (sum18 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(4000, sum18);
                            }
                            if (sum19 != 0)
                            {
                                chart1.Series["Lt"].Points.AddXY(6000, sum19);
                            }
                            chart1.Series["Lt"].Points.AddXY(8000, sum20);
                        }
                    }
                    chart1.Series["Rt"].ChartType = SeriesChartType.Line;
                    chart1.Series["Lt"].ChartType = SeriesChartType.Line;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot generate :" + ex.Message);
            }
        }


        private void txtRt250_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnG_hst_Click(object sender, EventArgs e)
        {
            LoadHistory(TprID);
        }

        private void gridHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i <= gridHistory.Rows.Count - 1; i++)
            {
                gridHistory.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
