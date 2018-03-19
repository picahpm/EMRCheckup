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
    public partial class frmObstetricsChgDoc : Form
    {
        public frmObstetricsChgDoc()
        {
            InitializeComponent();
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private string StrTitleGrid1 = "รายชื่อผู้ป่วยที่ส่งตรวจแต่ผลยังไม่ออก ทั้งหมด {0} คน";
        private string StrTitleReport = "รายชื่อผู้ป่วยที่รออ่านผล ทั้งหมด {0} คน";
        private trn_obstetric_chief currentObste = null;
        private void frmObstetricsChgDoc_Load(object sender, EventArgs e)
        {
            LoadBinding();
        }
        private void LoadBinding()
        {
            var objdata = (from toc in dbc.trn_obstetric_chiefs
                           from mut in dbc.mst_user_types
                           from tpt in dbc.trn_patients
                           from tpr in dbc.trn_patient_regis
                           where tpt.tpt_id==tpr.tpt_id
                           && tpr.tpr_id==toc.tpr_id
                           && toc.toc_create_by == mut.mut_username
                           && toc.toc_result == false
                           && (toc.toc_patho_result == false || toc.toc_patho_result == null)
                           select new
                           {
                               HNno = toc.trn_patient_regi.trn_patient.tpt_hn_no,
                               FullName = toc.trn_patient_regi.trn_patient.tpt_othername,
                               ArriveDate = toc.trn_patient_regi.tpr_arrive_date,
                               CreateBy = mut.mut_fullname
                           }).ToList();
            GridWaitResult.DataSource = objdata;
            lbTitlteGridWait.Text = string.Format(StrTitleGrid1, objdata.Count());

            var objResultReport = (from t1 in dbc.vw_obg_chang_docs
                                   orderby t1.p_date descending
                                   select t1);
            GridReportResult.DataSource = objResultReport;
            lbTitleResult.Text = string.Format(StrTitleReport, objResultReport.Count());

        }

        private void txtDocName_KeyUp(object sender, KeyEventArgs e)
        {
            SearchGetDoctor(txtDocName.Text.Trim());
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
        private void txtDocName_Click(object sender, EventArgs e)
        {
            if (GridDoctorName.Visible == true) { GridDoctorName.Visible = false; }
        }
        private void GridDoctorName_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var selectedItems = GridDoctorName.CurrentRow;
                txtDocCode.Text = selectedItems.Cells[1].Value.ToString();
                txtDocName.Text = selectedItems.Cells[0].Value.ToString();
            }
            catch (Exception)
            {
            }
            GridDoctorName.Visible = false;
        }
        private void GridDoctorName_Leave(object sender, EventArgs e)
        {
            if (GridDoctorName.Visible == true) { GridDoctorName.Visible = false; }
        }

        private void GridReportResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbMsgAlert.Text = "";
            if (e.RowIndex != -1)
            {
                int toc_id = Convert1.ToInt32(GridReportResult["Coltoc_id", e.RowIndex].Value);
                string HNno = GridReportResult["ColHN2", e.RowIndex].Value.ToString();
                currentObste = (from t1 in dbc.trn_obstetric_chiefs
                                  where t1.toc_id == toc_id
                                  orderby t1.tpr_id descending
                                  select t1).FirstOrDefault();
                var objcurrentRegis = (from t1 in dbc.trn_obstetric_chiefs
                                       where t1.toc_id == toc_id
                                       orderby t1.tpr_id descending
                                       select new
                                       {
                                           HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                           EN = t1.trn_patient_regi.tpr_en_no,
                                           FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                           Arrivedate = t1.trn_patient_regi.tpr_arrive_date,
                                           CheckDocName = t1.toc_doc_name
                                       }).FirstOrDefault();
                if (objcurrentRegis != null)
                {
                    txtHNno.Text = HNno;
                    txtENno.Text = objcurrentRegis.EN;
                    txtName.Text = objcurrentRegis.FullName;
                    txtArriveDate.Text = objcurrentRegis.Arrivedate.Value.ToString("dd/MM/yyyy");
                    txtDoctorCheck.Text = objcurrentRegis.CheckDocName;

                    ch_ResultAlert.Enabled = true;
                    DateTimeAlert.Enabled = true;
                    txtRemark.Enabled = true;
                }
            }
        }
        private void GridWaitResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbMsgAlert.Text = "";
            if (e.RowIndex != -1)
            {
                string HNno = GridWaitResult["ColHNno", e.RowIndex].Value.ToString();
                txtHNno.Text = HNno;
                 currentObste = (from t1 in dbc.trn_obstetric_chiefs
                                  where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
                                  orderby t1.tpr_id descending
                                  select t1).FirstOrDefault();
                var objcurrentobstetric = (from t1 in dbc.trn_obstetric_chiefs
                                       where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
                                       orderby t1.tpr_id descending
                                       select new
                                       {
                                           HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                           EN = t1.trn_patient_regi.tpr_en_no,
                                           FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                           Arrivedate = t1.trn_patient_regi.tpr_arrive_date,
                                           CheckDocName = t1.toc_doc_name
                                       }).FirstOrDefault();
                if (objcurrentobstetric != null)
                {
                    txtENno.Text = objcurrentobstetric.EN;
                    txtName.Text = objcurrentobstetric.FullName;
                    txtArriveDate.Text = objcurrentobstetric.Arrivedate.Value.ToString("dd/MM/yyyy");
                    txtDoctorCheck.Text = objcurrentobstetric.CheckDocName;

                    ch_ResultAlert.Enabled = false;
                    DateTimeAlert.Enabled = false;
                    txtRemark.Enabled = false;
                }
            }
        }

        private void GridWaitResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridWaitResult.Rows.Count; i++)
            {
                GridWaitResult["ColNo1", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
        private void GridReportResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridReportResult.Rows.Count; i++)
            {
                GridReportResult["ColNo2", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentObste != null)
                {
                    var objcurrentpft = (from t1 in dbc.trn_obstetric_chiefs
                                         where t1.toc_id == currentObste.toc_id
                                         select t1).FirstOrDefault();
                    if (objcurrentpft != null)
                    {
                        //if (ch_ResultAlert.Enabled == true)
                        //{
                            
                        //}
                        objcurrentpft.toc_doc_code = txtDocCode.Text;
                        objcurrentpft.toc_doc_name = txtDocName.Text;
                        dbc.SubmitChanges();
                    }
                    lbMsgAlert.Text = "Save data completed.";
                    txtDocCode.Text = "";
                    txtDocName.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
