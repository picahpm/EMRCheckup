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
using System.Diagnostics;
using BKvs2010.EmrClass;
using System.Data.Linq;

namespace BKvs2010
{
    public partial class DialogAllExam : Form
    {
        InhCheckupDataContext dbc;
        List<LabStatusModel> list = new List<LabStatusModel>();
        List<ChkHideInpStatusModel> chkStatusList = new List<ChkHideInpStatusModel>();
        public DialogAllExam()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
            gvCurrentLab.AutoGenerateColumns = false;
        }

        public int tpr_id { get; set; }

        private void frmPE_DialogOtherExam_Load(object sender, EventArgs e)
        {
            try
            {
                //this.LoadTransaction();
                this.Text = Program.GetRoomName();
                uiProfileHorizontal1.Loaddata();
                ShowLabResult();
            }
            catch (Exception ex)
            {
                throw new Exception("Load DialogAllExam Fail : " + ex.Message);
            }
        }

        private void SaveLAB()
        {
            try
            {
                try
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                            .Where(x => x.tpr_id == Program.CurrentRegis.tpr_id &&
                                                                        x.tpbr_radiology == "LB")
                                                            .FirstOrDefault();
                    if (bookResult == null)
                    {
                        bookResult = new trn_patient_book_result()
                        {
                            tpr_id = Program.CurrentRegis.tpr_id,
                            tpbr_radiology = "LB",
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
                    lbAlertMsg.Text = "Save Data Complete.";
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
            catch (ChangeConflictException)
            {
                foreach (ObjectChangeConflict occ in dbc.ChangeConflicts)
                {
                    dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                }
                dbc.SubmitChanges();
            }
        }

        private class LabProfileResultsort
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Result { get; set; }
            public string Cumulative { get; set; }
        }

        private class GridLabResult
        {
            public int Id { get; set; }
            public int No { get; set; }
            public string ItemSet { get; set; }
            public string ItemCode { get; set; }
            public string ItemName { get; set; }
            public string Result { get; set; }
            public string NRange { get; set; }
            public string Units { get; set; }
            public string LabResult { get; set; }
            public string InterpretationTH { get; set; }
            public string InterpretationEN { get; set; }
            public char? Summary { get; set; }
            public DataGridViewComboBoxColumn Action { get; set; }
            public int? MlrID { get; set; }
        }

        private class LabStatusModel
        {
            public int Id { get; set; }
            public string Status { get; set; }
        }

        private class ChkHideInpStatusModel
        {
            public int Id { get; set; }
            public bool Checked { get; set; }
        }

        private class ActionModel
        {
            public string Value { get; set; }
            public string DisplayText { get; set; }
        }

        #region Comment out Old Code
        //private void LoadLAB()
        //{
        //    dbc = new InhCheckupDataContext();
        //    var objregis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();

        //    if (objregis != null)
        //    {
        //        var SelecttrnBYHN = (from t1 in dbc.trn_patient_ass_hdrs
        //                             where t1.trn_patient_ass_grp.tpr_id == objregis.tpr_id
        //                             select new LabProfileResultsort
        //                             {
        //                                 Code = t1.tpeh_order_code,
        //                                 Name = t1.tpeh_order_name,
        //                                 Status = (t1.tpeh_status == 'E') ? "Complete" : "In Progress",
        //                                 Result = (t1.tpeh_status == 'E') ? "Results" : "",
        //                                 Cumulative = (t1.tpeh_status == 'E') ? "C" : "",
        //                             }).ToList();
        //        if (SelecttrnBYHN.Count > 0)
        //        {
        //            gvCurrentLab.DataSource = SelecttrnBYHN;
        //        }
        //        //gvCurrentLab.Columns["colOrderStatus"].Visible = false;
        //        //this.gvCurrentLab.ClearSelection();
        //        if (SelecttrnBYHN.Count > 0)
        //        {
        //            DataGridViewCellEventArgs e = new DataGridViewCellEventArgs(0, 0);
        //            gvCurrentLab_CellClick(new object(), e);
        //        }

        //    }
        //}

        //private object selectValue(int row, string cell)
        //{
        //    var returnString = dataGridView1.Rows[row].Cells[cell].Value;

        //    return returnString;
        //}
        #endregion

        private int countAsummary = 0;
        private string LabResultSummay = "";

        private void ShowLabResult()
        {
            dbc = new InhCheckupDataContext();
            var tpgrps = dbc.trn_patient_ass_grps.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

            if (tpgrps != null)
            {

                List<ActionModel> actionList = new List<ActionModel>
                {
                    new ActionModel { Value = "DF", DisplayText = "" },
                    new ActionModel { Value = "HL", DisplayText = "Hide Lab" },
                    new ActionModel { Value = "HI", DisplayText = "Hide Interpretation" },
                    new ActionModel { Value = "EI", DisplayText = "Edit Interpretation" }
                };

                var cboActions = (DataGridViewComboBoxColumn)Actions;
                cboActions.DataSource = actionList;
                cboActions.ValueMember = "Value";
                cboActions.DisplayMember = "DisplayText";

                // --------------------------------------


                // --------------------------------------

                trn_ass_group.DataSource = tpgrps;
            }

        }

        private void OpenDialogInterpretation(int Index)
        {
            DialogInterpretation dlgInterpretation = new DialogInterpretation();
            trn_patient_ass_dtl dtl = (trn_patient_ass_dtl)dataGridView1.Rows[Index].DataBoundItem;
            dlgInterpretation.dtl = dtl;
            dlgInterpretation.oldStatus = list.Where(x => x.Id == Index).FirstOrDefault().Status;
            dlgInterpretation.ShowDialog();
            dataGridView1.Refresh();
        }

        private void RowPrepaint()
        {
            txtpatientEduction.Text = string.Empty;
            List<string> ptEducation = new List<string>();
            list.Clear();
            for (int row = 0; row < dataGridView1.Rows.Count; row++)
            {
                CellActionsPrepaint(row);
                CellResultPrepaint(row);
                if (!string.IsNullOrEmpty(NewPatientEducation(row)))
                    ptEducation.Add(NewPatientEducation(row));

                LabStatusModel dt = new LabStatusModel();
                dt.Id = row;
                dt.Status = dataGridView1.Rows[row].Cells["Actions"].Value == null ? "DF" : dataGridView1.Rows[row].Cells["Actions"].Value.ToString();
                list.Add(dt);
            }

            txtpatientEduction.Text = string.Join(", ", ptEducation);
        }

        private void CellResultPrepaint(int rowIndex)
        {
            if (dataGridView1.Rows[rowIndex].Cells["tped_summary"].Value != null)
            {
                char val = (char)dataGridView1.Rows[rowIndex].Cells["tped_summary"].Value;
                if (val == 'A')
                {
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
        }

        private void CellActionsPrepaint(int rowIndex)
        {
            string val = dataGridView1.Rows[rowIndex].Cells["Actions"].Value == null ? "DF" : dataGridView1.Rows[rowIndex].Cells["Actions"].Value.ToString();
            dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Empty;
            dataGridView1.Rows[rowIndex].Cells["tped_lab_result_eng"].Style.BackColor = Color.Empty;
            dataGridView1.Rows[rowIndex].Cells["tped_lab_result_thai"].Style.BackColor = Color.Empty;
            // add lab jp 1-11-17
            dataGridView1.Rows[rowIndex].Cells["tped_lab_result_jp"].Style.BackColor = Color.Empty;
            switch (val)
            {
                case "EI":
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_thai"].Style.BackColor = Color.Yellow;
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_eng"].Style.BackColor = Color.Yellow;
                    // add lab jp 1-11-17
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_jp"].Style.BackColor = Color.Yellow;
                    break;
                case "HL":
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                    break;
                case "HI":
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_thai"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_eng"].Style.BackColor = Color.LightGray;
                    // add lab jp 1-11-17
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_jp"].Style.BackColor = Color.LightGray;
                    break;
                default:
                    dataGridView1.Rows[rowIndex].Cells["Actions"].Value = val;
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Empty;
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_eng"].Style.BackColor = Color.Empty;
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_thai"].Style.BackColor = Color.Empty;
                    // add lab jp 1-11-17
                    dataGridView1.Rows[rowIndex].Cells["tped_lab_result_jp"].Style.BackColor = Color.Empty;
                    break;
            }

        }

        private string NewPatientEducation(int rowIndex)
        {
            string actionVal = dataGridView1.Rows[rowIndex].Cells["Actions"].Value == null ? "DF" : dataGridView1.Rows[rowIndex].Cells["Actions"].Value.ToString();
            string summarry = dataGridView1.Rows[rowIndex].Cells["tped_summary"].Value == null ? null : dataGridView1.Rows[rowIndex].Cells["tped_summary"].Value.ToString();
            string res = "";
            if (!string.IsNullOrEmpty(summarry) && !string.IsNullOrEmpty(actionVal))
            {
                if ((summarry == "A" && actionVal == "DF") || (summarry == "A" && actionVal == "EI"))
                    res = dataGridView1.Rows[rowIndex].Cells["tped_lab_result_thai"].Value == null ? null : dataGridView1.Rows[rowIndex].Cells["tped_lab_result_thai"].Value.ToString();
            }

            return res;
        }

        private void gvCurrentLab_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gvCurrentLab.SetRuningNumber();
            for (int row = 0; row < gvCurrentLab.Rows.Count; row++)
            {
                ChkHideInpStatusModel item = new ChkHideInpStatusModel();
                item.Id = row;
                item.Checked = false;

                chkStatusList.Add(item);
            }

            ConvertExcuteStatus();
            dataGridView1.SetRuningNumber();
            RowPrepaint();
        }

        private void gvCurrentLab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            gvCurrentLab.SetRuningNumber();
            ConvertExcuteStatus();
            dataGridView1.SetRuningNumber();
            RowPrepaint();

            #region Keep Checkbox State
            chkHideAllLab.Checked = chkStatusList.Where(x => x.Id == e.RowIndex).FirstOrDefault().Checked;
            #endregion

            lbAlertMsg.Text = string.Empty;

            #region Old Code
            //if (e.RowIndex > -1)
            //{
            //    int ptpr_id = this.tpr_id; //71;
            //    string pHN_no = (from t1 in dbc.trn_patients
            //                     where t1.trn_patient_regis.Any(x => x.tpr_id == this.tpr_id)
            //                     select t1.tpt_hn_no).FirstOrDefault();//"01-07-028951";
            //    string pHeadLabNo = gvCurrentLab["LabCode", e.RowIndex].Value.ToString();
            //    ShowLabResult();
            //    //MessageBox.Show(e.ColumnIndex.ToString());
            //    if (e.ColumnIndex == 5 && gvCurrentLab.CurrentCell.Value.ToString() == "C")
            //    {
            //        frmCumulative frm = new frmCumulative();
            //        frm.ptpr_id = ptpr_id;
            //        frm.pHN_no = pHN_no;
            //        frm.pHeadLabNo = pHeadLabNo;
            //        frm.ShowDialog();
            //    }

            //    if (e.ColumnIndex == 4 && gvCurrentLab.CurrentCell.Value.ToString() == "Results")
            //    {
            //        frmLabProfile flabprofile = new frmLabProfile();
            //        flabprofile.ptpr_id = ptpr_id;
            //        flabprofile.LabNo = pHeadLabNo;
            //        flabprofile.ShowDialog();
            //    }
            //}
            #endregion
        }

        private void ConvertExcuteStatus()
        {
            for (int row = 0; row < gvCurrentLab.Rows.Count; row++)
            {
                if (gvCurrentLab.Rows[row].Cells["tpeh_status"].Value.ToString() == "E" || gvCurrentLab.Rows[row].Cells["tpeh_status"].Value.ToString() == "I")
                    gvCurrentLab.Rows[row].Cells["tpeh_status"].Value = "C";

                if (gvCurrentLab.Rows[row].Cells["tpeh_status"].Value.ToString() != "C" && gvCurrentLab.Rows[row].Cells["tpeh_status"].Value.ToString() != "E")
                    gvCurrentLab.Rows[row].Cells["tpeh_status"].Value = "";
            }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Row.Cells["Actions"].Value.ToString()))
            {
                e.Row.Cells["Actions"].Value = "DF";
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Actions")
            {
                string val = dataGridView1.Rows[e.RowIndex].Cells["Actions"].Value == null ? "DF" : dataGridView1.Rows[e.RowIndex].Cells["Actions"].Value.ToString();

                if (val == "EI")
                    OpenDialogInterpretation(e.RowIndex);

                RowPrepaint();
            }
        }

        #region Comment out Old Code
        //private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dataGridView1.Columns[e.ColumnIndex].Name == "Actions")
        //    {
        //        if (((trn_patient_ass_dtl)dataGridView1.Rows[e.RowIndex].DataBoundItem).tped_lab_result_status == "EI")
        //        {
        //            OpenDialogInterpretation(e.RowIndex);
        //        }
        //    }
        //}
        #endregion

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    OpenDialogInterpretation(e.RowIndex);
                    RowPrepaint();
                }
            }
            catch
            {
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            //int indexrow = 1;
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    if (dataGridView1.Rows[i].Cells["colSummary"].Value != null && dataGridView1.Rows[i].Cells["colSummary"].Value.ToString() == "A")
            //    {
            //        dataGridView1.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            //        dataGridView1.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
            //    }
            //    dataGridView1.Rows[i].Cells[0].Value = indexrow;
            //    indexrow = indexrow + 1;
            //}
            try
            {
                gvCurrentLab.SetRuningNumber();
                dataGridView1.SetRuningNumber();
                RowPrepaint();
            }
            catch
            {
            }
        }

        private void btnrefreshLabResult_Click(object sender, EventArgs e)
        {

        }

        private void chkHideAllLab_Click(object sender, EventArgs e)
        {
            #region Keep Checkbox State
            int labGrpId = gvCurrentLab.CurrentCell.RowIndex;
            var obj = chkStatusList.FirstOrDefault(x => x.Id == labGrpId);
            if (obj != null)
                obj.Checked = chkHideAllLab.Checked;
            #endregion

            if (chkHideAllLab.Checked)
            {

                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {
                    string val = dataGridView1.Rows[row].Cells["Actions"].Value == null ? "DF" : dataGridView1.Rows[row].Cells["Actions"].Value.ToString();
                    if (val == "DF")
                    {
                        dataGridView1.Rows[row].Cells["Actions"].Value = "HI";
                    }
                }
            }
            else
            {
                for (int row = 0; row < dataGridView1.Rows.Count; row++)
                {

                    string val = dataGridView1.Rows[row].Cells["Actions"].Value == null ? "DF" : dataGridView1.Rows[row].Cells["Actions"].Value.ToString();
                    if (val == "HI")
                    {
                        dataGridView1.Rows[row].Cells["Actions"].Value = "DF";
                    }
                }
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveLAB();

        }

        private void btnAddLab_Click(object sender, EventArgs e)//add suriya 29/06/2017
        {
            dbc = new InhCheckupDataContext();
            string pHN_no = (from t1 in dbc.trn_patients
                             where t1.trn_patient_regis.Any(x => x.tpr_id == this.tpr_id)
                             select t1.tpt_hn_no).FirstOrDefault();
            PopupAddLab popLab = new PopupAddLab( pHN_no, this.tpr_id);
            popLab.ShowDialog();
            ShowLabResult();
        }

        private void gvCurrentLab_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int ptpr_id = this.tpr_id; //71;
                string pHN_no = (from t1 in dbc.trn_patients
                                 where t1.trn_patient_regis.Any(x => x.tpr_id == this.tpr_id)
                                 select t1.tpt_hn_no).FirstOrDefault();
                string pHeadLabNo = gvCurrentLab.Rows[e.RowIndex].Cells["tpeh_order_code"].Value.ToString();

                if (gvCurrentLab.CurrentCell.Value.ToString() == "C")
                {
                    frmCumulative frm = new frmCumulative();
                    frm.ptpr_id = ptpr_id;
                    frm.pHN_no = pHN_no;
                    frm.pHeadLabNo = pHeadLabNo;
                    frm.ShowDialog();
                }

                if (gvCurrentLab.CurrentCell.Value.ToString() == "Results")
                {
                    frmLabProfile flabprofile = new frmLabProfile();
                    flabprofile.ptpr_id = ptpr_id;
                    flabprofile.LabNo = pHeadLabNo;
                    flabprofile.ShowDialog();
                }
            }
        }
    }
}