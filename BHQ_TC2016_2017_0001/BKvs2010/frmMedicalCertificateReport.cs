using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using DBCheckup;
namespace BKvs2010
{
    public partial class Medical_Certificate : Form
    {
        public Medical_Certificate()
        {
            InitializeComponent();
        }

        #region function
        private void save(int tpt_id,string en)
        {
            
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                trn_report_med_cer _trmc = (from t1 in dbc.trn_report_med_cers where t1.tpt_id == tpt_id && t1.trmc_en == en select t1).FirstOrDefault();
                if(_trmc == null)
                {
                    trn_report_med_cer trmc = new trn_report_med_cer();
                    trmc.tpt_id = tpt_id;
                    trmc.trmc_en = en;
                    trmc.trmc_full_name = txtfullname.Text;
                    trmc.trmc_address = txtaddress.Text;
                    trmc.trmc_dob = Convert.ToDateTime(txtdob.Text);
                    trmc.trmc_nation = txtnation.Text;
                    trmc.trmc_licence_no = txtlicence.Text;
                    trmc.trmc_med_std = (string)cbmedical_standard_class.SelectedValue;
                    trmc.trmc_med_std_remark = txtlimitations.Text;
                    trmc.trmc_ame_signature = (int)cbame_signature.SelectedValue;
                    trmc.trmc_place_exam = txtplace_of_exam.Text;
                    trmc.trmc_date_exam = dtpexam.Value;
                    trmc.trmc_valid_until = dtpvalid_until.Value;
                    trmc.trmc_create_by = Program.CurrentUser.mut_username;
                    trmc.trmc_create_date = Program.GetServerDateTime();
                    dbc.trn_report_med_cers.InsertOnSubmit(trmc);
                }else
                {
                    _trmc.tpt_id = tpt_id;
                    _trmc.trmc_en = en;
                    _trmc.trmc_full_name = txtfullname.Text;
                    _trmc.trmc_address = txtaddress.Text;
                    _trmc.trmc_dob = Convert.ToDateTime(txtdob.Text);
                    _trmc.trmc_nation = txtnation.Text;
                    _trmc.trmc_licence_no = txtlicence.Text;
                    _trmc.trmc_med_std = (string)cbmedical_standard_class.SelectedValue;
                    _trmc.trmc_med_std_remark = txtlimitations.Text;
                    _trmc.trmc_ame_signature = (int)cbame_signature.SelectedValue;
                    _trmc.trmc_place_exam = txtplace_of_exam.Text;
                    _trmc.trmc_date_exam = dtpexam.Value;
                    _trmc.trmc_valid_until = dtpvalid_until.Value;
                    _trmc.trmc_create_by = Program.CurrentUser.mut_username;
                    _trmc.trmc_create_date = Program.GetServerDateTime();
                }
                dbc.SubmitChanges();
            }

            lblworning.Enabled = true;
            
        }

        private void load_trn_report_med_cer(int tpt_id, string en)
        {
            using(InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                if (Program.CurrentRegis != null)
                {
                    trn_report_med_cer trmc = (from t1 in dbc.trn_report_med_cers where t1.trmc_en == en && t1.tpt_id == tpt_id select t1).FirstOrDefault();
                    if (trmc != null)
                    {
                        txtfullname.Text = trmc.trmc_full_name;
                        txtaddress.Text = trmc.trmc_address;
                        txtdob.Text = String.Format("{0:dd/MM/yyyy}", trmc.trmc_dob);
                        txtnation.Text = trmc.trmc_nation;
                        txtlicence.Text = trmc.trmc_licence_no;
                        cbmedical_standard_class.SelectedIndex = Convert.ToInt16(trmc.trmc_med_std);
                        cbame_signature.SelectedValue = Convert.ToInt16(trmc.trmc_ame_signature);
                        dtpexam.Value = trmc.trmc_date_exam.Value.Date;
                        dtpvalid_until.Value = trmc.trmc_valid_until.Value.Date;
                    }
                    else
                    {
                        trn_patient tpt = Program.CurrentRegis.trn_patient;
                        //find aviation licence
                        trn_ques_aviation tqa = (from t1 in dbc.trn_ques_aviations where t1.tpr_id == Program.CurrentRegis.tpr_id select t1).FirstOrDefault();

                        txtfullname.Text = tpt.tpt_othername;
                        txtaddress.Text = Program.CurrentRegis.tpr_main_address;
                        txtdob.Text = tpt.tpt_dob_text; // String.Format("{0:dd/MM/yyyy}", tpt.tpt_dob);
                        txtnation.Text = tpt.tpt_nation_desc;
                        txtlicence.Text = tqa.tqa_license_no;
                        cbmedical_standard_class.SelectedIndex = 0;
                        cbame_signature.SelectedValue = 0;
                        dtpexam.Value = Program.CurrentRegis.tpr_arrive_date.Value.Date;
                    }
                }
            }
        }

        private void loadame_signature()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
               List<mst_user_type> mut = (from t1 in dbc.mst_user_types where t1.mut_type == 'D' select t1).ToList();
               if(mut.Count > 0)
               {
                   cbame_signature.DataSource = mut;
                   cbame_signature.DisplayMember = "mut_user_id";
                   cbame_signature.ValueMember = "mut_username";
               }

               cbame_signature.SelectedIndex = 0;
            }
        }

        #endregion

        private void Medical_Certificate_Load(object sender, EventArgs e)
        {
            CultureInfo cinfo = CultureInfo.CreateSpecificCulture("en-EN");
            loadame_signature();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            save(Program.CurrentRegis.trn_patient.tpt_id,Program.CurrentRegis.tpr_en_no);
        }

        private void cbmedical_standard_class_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbmedical_standard_class.Text == "OTHER")
            {
                txtremark_std.Enabled = true;
            }
            else
            {
                txtremark_std.Enabled = false;
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            //ClsReport.previewRpt(new List<string> { "AV101" });
        }

        private void btnprint_preview_Click(object sender, EventArgs e)
        {
            //ClsReport.previewRpt(new List<string> { "AV101" });
        }


    }
}
