using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace CheckupBO
{
    public partial class frmViewABI : Form
    {
        public frmViewABI()
        {
            InitializeComponent();
        }
        public int TprID { get; set; }
        public int siteitem { get; set; }

        InhCheckupDataContext dbc = new InhCheckupDataContext();

        private void txtTheBMI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTheBMI.Text = Convert.ToDouble(txtTheBMI.Text).ToString();
                lbrequireBmi.Visible = false;
            }
            catch (Exception ex)
            {
                if (TprID == 0)
                {
                    lbrequireBmi.Visible = false;
                }
                else
                {
                    if (TprID != 0)
                    { lbrequireBmi.Visible = false; }
                    else
                    {
                        txtTheBMI.Text = "0";
                        lbrequireBmi.Visible = true;
                    }
                }
            }
        }
        private void rdSinus_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdSinus.Checked == true)
            {
                txtOtherWithRate.Enabled = false;
                txtOtherWithRate.Text = "";
            }
        }
        private void rdAF_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdAF.Checked == true)
            {
                txtOtherWithRate.Enabled = false;
                txtOtherWithRate.Text = "";
            }
        }
        private void rdOther_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdOther.Checked)
            {
                txtOtherWithRate.Enabled = true;
                txtOtherWithRate.Focus();
            }
            else
            {
                txtOtherWithRate.Text = string.Empty;
                txtOtherWithRate.Enabled = false;
            }
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                txtImpressOther.Enabled = true;
            }
            else
            {
                txtImpressOther.Text = string.Empty;
                txtImpressOther.Enabled = false;
            }
        }

        private void frmViewABI_Load(object sender, EventArgs e)
        {
            LoadDrowDown();
            this.LoadData(TprID);
            LoadHistory(TprID);
        }
        private void LoadDrowDown()
        {
                try
                {
                    var ddlSuggestion = (from t1 in dbc.mst_doc_result_hdrs
                                         join t2 in dbc.mst_doc_results on
                                         t1.mrh_id equals t2.mrh_id
                                         where t1.mrh_status == 'A' && t1.mrh_code == "AB"
                                         select new
                                         {
                                             mrm_id=t1.mrm_id,
                                             mdr_ename = t2.mdr_ename,
                                             mdr_val = t2.mdr_id
                                         }).ToList();
            var objcurrentQueue = (from t1 in dbc.trn_patient_queues
                                   where t1.tpr_id == TprID 
                                   && t1.mst_room_hdr.mrm_code == "AB"
                                   select t1).FirstOrDefault();
            if (objcurrentQueue != null)
            {
                int mrmid = (objcurrentQueue.mrm_id != null) ? (int) objcurrentQueue.mrm_id : 0;
                if (mrmid > 0)
                {
                    ddlSuggestion = ddlSuggestion.Where(x => x.mrm_id == mrmid).ToList();
                }
            }
                    cmbSuggestion.DataSource = ddlSuggestion.Select((item, index) => new
                    {
                        item.mdr_ename,
                        item.mdr_val
                    }).ToList();

                    cmbSuggestion.DisplayMember = "mdr_ename";
                    cmbSuggestion.ValueMember = "mdr_val";
                }
                catch (Exception e)
                {
                    MessageBox.Show("catch : " + e.ToString());
                }
        }

        private void LoadHistory(int tpr_id)
        {
            var objdocter_hdr = (from t1 in dbc.trn_abi_hdrs
                                 where t1.tpr_id == tpr_id
                                 select new
                                 {
                                     EN = t1.trn_patient_regi.tpr_en_no,
                                     ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                     CreateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tah_create_by).Single().mut_fullname,
                                     CreateDate = t1.tah_create_date,
                                     UpdateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tah_create_by).Single().mut_fullname,
                                     UpdateDate = t1.tah_create_date,
                                     Link = "Link"
                                 }).ToList();
            gridHistory.DataSource = objdocter_hdr;

        }
        private void LoadData(int tprID)
        {

            trn_abi_hdr obj = dbc.trn_abi_hdrs.Where(c => c.tpr_id == tprID).FirstOrDefault();
            if (obj != null)
            {
                ABIbindingSource.DataSource = obj;
                if (TprID == 0)
                {
                    ABIbindingSource.DataSource = (from t1 in dbc.trn_abi_hdrs select t1);
                    ABIbindingSource.AddNew();
                }
                else
                {
                    ABIbindingSource.DataSource = obj;
                    trn_abi_hdr trnabi = (trn_abi_hdr)ABIbindingSource.Current;
                    Program.SetValueRadioGroup(pnlHearth, trnabi.tah_hearth_rhy_flag.ToString());
                    Program.SetValueRadioGroup(pnlBmi, trnabi.tah_the_bmi_flag.ToString());
                    Program.SetValueRadioGroup(pnlPressure, trnabi.tah_blood_pressure.ToString());
                    Program.SetValueRadioGroup(pnlRightCAVI, trnabi.tah_right_cavi_flag.ToString());
                    Program.SetValueRadioGroup(pnlLeftCAVI, trnabi.tah_left_cavi_flag.ToString());
                    Program.SetValueRadioGroup(pnlRightABI, trnabi.tah_right_abi_flag.ToString());
                    Program.SetValueRadioGroup(pnlLeftABI, trnabi.tah_left_abi_flag.ToString());
                    Program.SetValueRadioGroup(pnlCarotid, trnabi.tah_carotid_contour.ToString());
                    Program.SetValueRadioGroup(pnlFermoral, trnabi.tah_femoral_contour.ToString());
                    if (trnabi.tah_impress_normal == 'Y')
                        checkBox1.Checked = true;
                    if (trnabi.tah_impress_no_evidence == 'Y')
                        checkBox4.Checked = true;
                    if (trnabi.tah_impress_mild_periphera == 'Y')
                        checkBox8.Checked = true;
                    if (trnabi.tah_impress_mode_peripheral == 'Y')
                        checkBox3.Checked = true;
                    if (trnabi.tah_impress_serv_peripheal == 'Y')
                        checkBox7.Checked = true;
                    if (trnabi.tah_impress_non_compress == 'Y')
                        checkBox2.Checked = true;
                    if (trnabi.tah_impress_early_ather == 'Y')
                        checkBox6.Checked = true;
                    if (trnabi.tah_impress_others == 'Y')
                        checkBox5.Checked = true;

                    if (trnabi.mdr_id != null)
                    {
                        cmbSuggestion.SelectedValue = trnabi.mdr_id;
                    }

                    double thebmi = string.IsNullOrEmpty(txtTheBMI.Text) ? 0.0 : Convert.ToSingle(txtTheBMI.Text);
                    if (thebmi < 18.50)
                    {
                        rdUnder.Checked = true;
                    }
                    else if (thebmi <= 18.50 && thebmi >= 24.99)
                    {
                        radioNormal.Checked = true;
                    }
                    else if (thebmi >= 25 && thebmi <= 29.99)
                    {
                        radioOverWeight.Checked = true;
                    }
                    else if (thebmi > 30)
                    {
                        radioObesity.Checked = true;
                    }
                }
            }
            else
            {
                ABIbindingSource.DataSource = (from t1 in dbc.trn_abi_hdrs select t1);
                ABIbindingSource.AddNew();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //SaveData
                if (SaveData('D'))
                {
                    lblMsg.Text = "Save Data Completed.";
                    this.AutoScrollPosition = new Point(0, 0);
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("catch : " + ee.Message);
            }
        }
        public bool SaveData(char type)
        {
            Boolean saveIsCompleted = false;
            DateTime datenowvalue = Program.GetServerDateTime();
            try
            {
                var Abi = (trn_abi_hdr)this.ABIbindingSource.Current;
                //int suggesstion = string.IsNullOrEmpty(cmbSuggestion.SelectedValue) ? int.MinValue : Convert.ToInt32(cmbSuggestion.SelectedValue);
                if (cmbSuggestion.SelectedValue == null)
                {
                    Abi.mdr_id = null;
                }
                else
                {
                    Abi.mdr_id = Utility.GetInteger(cmbSuggestion.SelectedValue);
                }
                Abi.tpr_id = TprID;
                Abi.tah_type = type;
                //Abi.tah_the_bmi = txtTheBMI.Text;
                Abi.tah_the_bmi_flag = Program.GetValueRadioTochar(pnlBmi);
                Abi.tah_hearth_rhy_flag = Program.GetValueRadioTochar(pnlHearth);
                Abi.tah_blood_pressure = Program.GetValueRadioTochar(pnlPressure);
                Abi.tah_right_cavi_flag = Program.GetValueRadioTochar(pnlRightCAVI);
                Abi.tah_left_cavi_flag = Program.GetValueRadioTochar(pnlLeftCAVI);
                Abi.tah_right_abi_flag = Program.GetValueRadioTochar(pnlRightABI);
                Abi.tah_left_abi_flag = Program.GetValueRadioTochar(pnlLeftABI);
                Abi.tah_carotid_contour = Program.GetValueRadio(pnlCarotid);
                Abi.tah_femoral_contour = Program.GetValueRadio(pnlFermoral);
                Abi.tah_impress_normal = (checkBox1.Checked) ? 'Y' : 'N';
                Abi.tah_impress_no_evidence = (checkBox4.Checked) ? 'Y' : 'N';
                Abi.tah_impress_mild_periphera = (checkBox8.Checked) ? 'Y' : 'N';
                Abi.tah_impress_mode_peripheral = (checkBox3.Checked) ? 'Y' : 'N';
                Abi.tah_impress_serv_peripheal = (checkBox7.Checked) ? 'Y' : 'N';
                Abi.tah_impress_non_compress = (checkBox2.Checked) ? 'Y' : 'N';
                Abi.tah_impress_early_ather = (checkBox6.Checked) ? 'Y' : 'N';
                Abi.tah_impress_others = (checkBox5.Checked) ? 'Y' : 'N';

                Abi.tah_doc_result_eng = Abi.tah_doc_result_thai;
                if (Abi.tah_create_by == null)
                {
                    Abi.tah_create_by = Program.CurrentUser.mut_username;
                    Abi.tah_create_date = datenowvalue;
                }
                Abi.tah_update_by = Program.CurrentUser.mut_username;
                Abi.tah_update_date = datenowvalue;

                ABIbindingSource.EndEdit();
                dbc.SubmitChanges();
                saveIsCompleted = true;
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Message);
            }
            return saveIsCompleted;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
