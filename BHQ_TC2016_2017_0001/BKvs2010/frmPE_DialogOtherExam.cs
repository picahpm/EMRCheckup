using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmPE_DialogOtherExam : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        public frmPE_DialogOtherExam()
        {
            InitializeComponent();
        }

        private void frmPE_DialogOtherExam_Load(object sender, EventArgs e)
        {
            try
            {
                this.LoadOtherExam();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error >> " + ex.Message);
            }
        }

        #region  LoadData OtherExamination
        private void LoadOtherExam()
        {
            if (Program.CurrentRegis != null)
            {
                DateTime dateNow = Program.GetServerDateTime();
                var objRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                var objDoc = objRegis.trn_doctor_hdrs.FirstOrDefault();
                if (objDoc == null)
                {
                    objDoc = new trn_doctor_hdr();
                    objDoc.trh_create_by = "OtherExam";
                    objDoc.trh_create_date = dateNow;
                    objDoc.trh_update_by = "OtherExam";
                    objDoc.trh_update_date = dateNow;
                    objRegis.trn_doctor_hdrs.Add(objDoc);
                }
                var objExam = objDoc.trn_doctor_exams.FirstOrDefault();
                if (objExam == null)
                {
                    objExam = new trn_doctor_exam();
                    objExam.trxm_create_by = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                    objDoc.trn_doctor_exams.Add(objExam);
                }
                objExam.trxm_update_by = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                DataBinding_trn_doctor_exam.DataSource = objExam;
            }
            
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                DataBinding_trn_doctor_exam.EndEdit();

                var other = DataBinding_trn_doctor_exam.OfType<trn_doctor_exam>().FirstOrDefault();
                if (other.trxm_create_date == null) other.trxm_create_date = dateNow;
                other.trxm_update_date = dateNow;

                try
                {
                    trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                            .Where(x => x.tpr_id == Program.CurrentRegis.tpr_id &&
                                                                        x.tpbr_radiology == "OE")
                                                            .FirstOrDefault();
                    if (bookResult == null)
                    {
                        bookResult = new trn_patient_book_result()
                        {
                            tpr_id = Program.CurrentRegis.tpr_id,
                            tpbr_radiology = "OE",
                            tpbr_create_by = Program.CurrentUser.mut_username,
                            tpbr_create_date = dateNow
                        };
                        dbc.trn_patient_book_results.InsertOnSubmit(bookResult);
                    }
                    bookResult.tpbr_flag_saved = true;
                    bookResult.tpbr_show_sections = true;
                    bookResult.tpbr_show_summary = true;
                    bookResult.tpbr_not_show_report = false;
                    bookResult.tpbr_active = true;
                    bookResult.tpbr_update_by = Program.CurrentUser.mut_username;
                    bookResult.tpbr_update_date = dateNow;
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
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            lblMsg.Text = "Save Data Completed.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
