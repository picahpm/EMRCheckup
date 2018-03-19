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
    public partial class frmPFTChgDoc : Form
    {
        public frmPFTChgDoc()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private string StrTitleGrid1 = "รายชื่อผู้ป่วยที่ส่งตรวจแต่ผลยังไม่ออก ทั้งหมด {0} คน";
        private string StrTitleReport = "รายชื่อผู้ป่วยที่รออ่านผล ทั้งหมด {0} คน";
        private trn_pft CurrentPFT = null;
        private void frmPFTChgDoc_Load(object sender, EventArgs e)
        {
            LoadBinding();
        }

        private void LoadBinding()
        {
            var objdata = (from t1 in dbc.trn_pfts
                           from t2 in dbc.mst_user_types
                           where t1.tpf_create_by == Program.CurrentUser.mut_username
                           && t1.tpf_result == false
                           && t1.tpf_doc_result == false
                           && t1.tpf_create_by == t2.mut_username
                           select new
                           {
                               HNno = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                               FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                               ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                               CreateBy = t2.mut_fullname
                           }).ToList();
            GridWaitResult.DataSource = objdata;
            lbTitleWaitResult.Text = string.Format(StrTitleGrid1, objdata.Count());

            var objResultReport = (from t1 in dbc.trn_pfts
                                   from t2 in dbc.mst_user_types
                                   from t3 in dbc.mst_user_types
                                   where t1.tpf_create_by == t2.mut_username
                                   && t1.tpf_doc_code == t3.mut_username
                                   && t1.tpf_result == false
                                   && t1.tpf_doc_result == true
                                   select new
                                   {
                                       HNno = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                       FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                       ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                       CreateBy = t2.mut_fullname,
                                       CloseDate = t1.tpf_close_date,
                                       CloseRemark = t1.tpf_close_remark
                                   }
                                   ).ToList();
            GridReportResult.DataSource = objResultReport;
            lbTitleResult.Text = string.Format(StrTitleReport, objResultReport.Count());

        }

        private void GridWaitResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string HNno = GridWaitResult["ColHN1", e.RowIndex].Value.ToString();
                txtHN.Text = HNno;
                CurrentPFT = (from t1 in dbc.trn_pfts
                                  where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
                                  orderby t1.tpr_id descending
                                  select t1).FirstOrDefault();
                var objcurrentRegis = (from t1 in dbc.trn_pfts
                                       where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
                                       orderby t1.tpr_id descending
                                       select new
                                       {
                                           HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                           EN = t1.trn_patient_regi.tpr_en_no,
                                           FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                           Arrivedate = t1.trn_patient_regi.tpr_arrive_date,
                                           CheckDocName = t1.tpf_doc_name
                                       }).FirstOrDefault();
                if (objcurrentRegis != null)
                {
                    txtENno.Text = objcurrentRegis.EN;
                    txtName.Text = objcurrentRegis.FullName;
                    txtArriveDate.Text = objcurrentRegis.Arrivedate.Value.ToString("dd/MM/yyyy");
                    txtDoctorCheck.Text = objcurrentRegis.CheckDocName;

                    ch_ResultAlert.Enabled = false;
                    DateTimeAlert.Enabled = false;
                    txtRemark.Enabled = false;
                }
            }
        }
        private void GridReportResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string HNno = GridReportResult["ColHN1", e.RowIndex].Value.ToString();
                txtHN.Text = HNno;
                 CurrentPFT = (from t1 in dbc.trn_pfts
                                  where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
                                  orderby t1.tpr_id descending
                                  select t1).FirstOrDefault();
                var objcurrentRegis = (from t1 in dbc.trn_pfts
                                       where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
                                       orderby t1.tpr_id descending
                                       select new
                                       {
                                           HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                           EN = t1.trn_patient_regi.tpr_en_no,
                                           FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                           Arrivedate = t1.trn_patient_regi.tpr_arrive_date,
                                           CheckDocName = t1.tpf_doc_name
                                       }).FirstOrDefault();
                if (objcurrentRegis != null)
                {
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentPFT != null)
                {
                    var objcurrentpft = (from t1 in dbc.trn_pfts
                                         where t1.tpf_id == CurrentPFT.tpf_id
                                         select t1).FirstOrDefault();
                    if (objcurrentpft != null)
                    {
                        objcurrentpft.tpf_doc_code = txtDocCode.Text;
                        objcurrentpft.tpf_doc_name = txtDocName.Text;
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

        private void txtDocName_Click(object sender, EventArgs e)
        {
            if (GridDoctorName.Visible == true) { GridDoctorName.Visible = false; }
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

        private void GridReportResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridReportResult.Rows.Count; i++)
            {
                GridReportResult["ColNo1", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
        private void GridWaitResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridWaitResult.Rows.Count; i++)
            {
                GridWaitResult["ColNo2", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }


    }
}
