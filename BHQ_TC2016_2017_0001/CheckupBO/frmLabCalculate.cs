using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using CheckupBO.Properties;

namespace CheckupBO
{
    public partial class frmLabCalculate : Form
    {
        public frmLabCalculate()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void frmLabCalculateTemp_Load(object sender, EventArgs e)
        {
            Tab1LoadData();
            Tab2LoadData();
            Tab3LoadData();
        }

        #region "Tab1"
        private void Tab1LoadData()
        {
            mode_frm_tab1 = modeFrm.unable_edit;
            DateTime datenow = Program.GetServerDateTime().Date;
            var objGroupLab = (from t1 in dbc.mst_lab_groups
                               orderby t1.mlg_code
                               where t1.mlg_status == 'A'
                               && (t1.mlg_effective_date == null ||
                                                  (t1.mlg_effective_date.Value.Date <= datenow
                                                  && (t1.mlg_expire_date == null ||
                                                      t1.mlg_expire_date.Value.Date >= datenow))
                                              )
                               select new DropdownData
                               {
                                   Code = t1.mlg_id,
                                   Name = t1.mlg_ename
                               }).ToList();
            var newitem = new DropdownData();
            newitem.Code = 0;
            newitem.Name = "Select All";
            objGroupLab.Insert(0, newitem);

            DDLabGroup.DisplayMember = "Name";
            DDLabGroup.ValueMember = "Code";
            DDLabGroup.DataSource = objGroupLab;
            DDLabGroup.SelectedIndex = 0;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime datenow = Program.GetServerDateTime().Date;
            var objGridLab = (from t1 in dbc.mst_labs
                              orderby t1.mst_lab_group.mlg_code, t1.mlb_code
                              where t1.mst_lab_group.mlg_status == 'A' &&
                              t1.mlb_code.Contains(txtLanItem.Text)
                              && (t1.mst_lab_group.mlg_effective_date == null ||
                                                      (t1.mst_lab_group.mlg_effective_date.Value.Date <= datenow
                                                      && (t1.mst_lab_group.mlg_expire_date == null ||
                                                          t1.mst_lab_group.mlg_expire_date.Value.Date >= datenow))
                                                  )
                              && (t1.mlb_effective_date == null ||
                                                      (t1.mlb_effective_date.Value.Date <= datenow
                                                      && (t1.mlb_expire_date == null ||
                                                          t1.mlb_expire_date.Value.Date >= datenow))
                                                  )
                              select new clGridlab
                              {
                                  GroupID = t1.mst_lab_group.mlg_id,
                                  GroupItem = t1.mst_lab_group.mlg_ename,
                                  ItemCode = t1.mlb_code,
                                  ItemName = t1.mlb_ename,
                                  ItemStatus = t1.mlb_status,
                                  mlb_ID = t1.mlb_id,

                              }).ToList();
            txtLabCode.Text = dbc.mst_labs.Single(x => x.mlb_id == objGridLab.FirstOrDefault().mlb_ID).mlb_code;
            int? grouplab = Program.GetValueComboBoxInt(DDLabGroup);
            if (grouplab != null && grouplab != 0)
            {
                objGridLab = objGridLab.Where(x => x.GroupID == grouplab).ToList();
            }
            if (ch_Active.Checked == true)
            {
                objGridLab = objGridLab.Where(x => x.ItemStatus == 'A').ToList();
            }

            GridLabData.DataSource = new SortableBindingList<clGridlab>(objGridLab);
            GridLabData.Columns["ColItemID"].Visible = false;
            GridLabData.Columns["ColGroupID"].Visible = false;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            DDLabGroup.SelectedIndex = 0;
            txtLanItem.Text = "";
        }


        List<clGridResult> gr = new List<clGridResult>();
        List<clGridCondition> gc = new List<clGridCondition>();
        List<clGridAge> ga = new List<clGridAge>();

        private int? mlb_id = null;
        private void GridLabData_SelectionChanged(object sender, EventArgs e)
        {
            gr = new List<clGridResult>();
            gc = new List<clGridCondition>();
            ga = new List<clGridAge>();
            if (GridLabData.SelectedRows.Count > 0)
            {
                int row = GridLabData.SelectedRows[0].Index;
                if (row > -1)
                {
                    mlb_id = convInt(GridLabData["ColItemID", row].Value);
                    if (mlb_id > 0)
                    {
                        gr = (from t1 in dbc.mst_lab_recoms
                              where t1.mlb_id == mlb_id
                              select new clGridResult
                              {
                                  mlr_id = t1.mlr_id,
                                  mlr_summary = t1.mlr_summary,
                                  mlr_th_name = t1.mlr_th_name,
                                  mlr_en_name = t1.mlr_en_name,
                                  mlr_jp_name = t1.mlr_jp_name,
                                  mlr_ch_name = t1.mlr_ch_name,
                                  mlr_kr_name = t1.mlr_kr_name,
                                  mlr_ar_name = t1.mlr_ar_name,
                                  mlr_mr_name = t1.mlr_mr_name,
                                  mlr_ftxt_name1 = t1.mlr_ftxt_name1,
                                  mlr_ftxt_name2 = t1.mlr_ftxt_name2,
                                  mlr_default = t1.mlr_default == true ? 'D' : ' '
                              }).ToList();
                        GridResult.DataSource = new SortableBindingList<clGridResult>(gr);

                        ga = (from t1 in dbc.mst_lab_ages
                              where t1.mlb_id == mlb_id
                              select new clGridAge
                              {
                                  mla_id = t1.mla_id,
                                  mla_sex = t1.mla_sex,
                                  mla_max_age = t1.mla_max_age,
                                  mla_max_day = t1.mla_max_day,
                                  mla_min_age = t1.mla_min_age,
                                  mla_min_day = t1.mla_min_day
                              }).ToList();
                        gridAge.DataSource = new SortableBindingList<clGridAge>(ga);

                        gc = (from t1 in dbc.mst_lab_results
                              orderby t1.mst_lab_age.mlb_id, t1.mst_lab_age.mla_id, t1.mlp_cond_seq
                              where t1.mst_lab_age.mlb_id == mlb_id
                              select new clGridCondition
                              {
                                  rowID = t1.mlp_id,
                                  mla_sex = t1.mst_lab_age.mla_sex,
                                  mla_min_age = t1.mst_lab_age.mla_min_age,
                                  mla_max_age = t1.mst_lab_age.mla_max_age,
                                  mla_min_day = t1.mst_lab_age.mla_min_day,
                                  mla_max_day = t1.mst_lab_age.mla_max_day,
                                  mla_vstand_min = t1.mst_lab_age.mla_vstand_min,
                                  mla_vstand_max = t1.mst_lab_age.mla_vstand_max,
                                  mla_vstand_nrange = t1.mst_lab_age.mla_vstand_nrange,
                                  mla_vstand_unit = t1.mst_lab_age.mla_vstand_unit,
                                  mlp_cond_seq = t1.mlp_cond_seq,
                                  mlp_condition = t1.mlp_condition,
                                  mlp_summary = t1.mlp_summary,
                                  mlp_status = t1.mlp_status,
                                  mlp_effective_date = t1.mlp_effective_date,
                                  mlp_expire_date = t1.mlp_expire_date,
                                  mla_id = t1.mla_id,
                                  mlr_id = t1.mlr_id
                              }).ToList();
                        GridCondition.DataSource = new SortableBindingList<clGridCondition>(gc);
                    }
                }
            }
        }
        private void GridCondition_SelectionChanged(object sender, EventArgs e)
        {
            mode_frm_tab1 = modeFrm.unable_edit;
            if (gc.Count > 0)
            {
                if (GridCondition.SelectedRows.Count > 0)
                {
                    int row = GridCondition.SelectedRows[0].Index;
                    int? mlp_id = convInt(GridCondition["ColrowID", row].Value);
                    clGridCondition tgc = gc.Single(x => (x.rowID == mlp_id));
                    gr.ForEach(x => x.result_lab = (x.mlr_id == tgc.mlr_id ? true : false));
                    ga.ForEach(x => x.age_lab = (x.mla_id == tgc.mla_id ? true : false));
                    GridResult.Refresh();
                    txtCondition.Text = tgc.mlp_condition;
                    Ch_viewActive.Checked = tgc.mlp_status == 'A' ? true : false;
                 //   Ch_viewActive.Checked = tgc.mlp_status == 'A' ? true : false;
                    txtDateStart.Value = (convDate(tgc.mlp_effective_date) != null ? convDateNotNull(tgc.mlp_effective_date) : DateTime.Now);
                    txtDateEnd.Value = (convDate(tgc.mlp_expire_date) != null ? convDateNotNull(tgc.mlp_expire_date) : txtDateEnd.MaxDate);
                    txtDayMin.Text = tgc.mla_min_day.ToString();
                    txtYearMin.Text = tgc.mla_min_age.ToString();
                    txtDayMax.Text = tgc.mla_max_day.ToString();
                    txtYearMax.Text = tgc.mla_max_age.ToString();
                    if (GridLabData.SelectedRows.Count > 0)
                    {
                        txtLabCode.Text = GridLabData["Column2", GridLabData.SelectedRows[0].Index].Value.ToString();
                    }
                }
            }
            else
            {
                txtCondition.Text = null;
                Ch_viewActive.Checked = false;
                txtDateStart.Value = DateTime.Now;
                txtDateEnd.Value = txtDateEnd.MaxDate;
                txtDayMin.Text = null;
                txtYearMin.Text = null;
                txtDayMax.Text = null;
                txtYearMax.Text = null;
                txtLabCode.Text = null;
            }
        }
        private void GridResult_SelectionChanged(object sender, EventArgs e)
        {
            txtThai.Text = null;
            txtEnglish.Text = null;
            txtjapan.Text = null;
            txtChinese.Text = null;
            txtKorean.Text = null;
            txtArabic.Text = null;
            txtBurmese.Text = null;
            txtOtherLanguage1.Text = null;
            txtOtherLanguage2.Text = null;
            if (gr.Count > 0)
            {
                if (GridResult.SelectedRows.Count > 0)
                {
                    int row = GridResult.SelectedRows[0].Index;
                    if (row > -1)
                    {
                        int? mlr_id = convInt(GridResult["Colmlr_id", row].Value);
                        if (mlr_id != null)
                        {
                            clGridResult tgr = gr.Single(x => x.mlr_id == mlr_id);
                            if (tgr != null)
                            {
                                txtThai.Text = tgr.mlr_th_name;
                                txtEnglish.Text = tgr.mlr_en_name;
                                txtjapan.Text = tgr.mlr_jp_name;
                                txtChinese.Text = tgr.mlr_ch_name;
                                txtKorean.Text = tgr.mlr_kr_name;
                                txtArabic.Text = tgr.mlr_ar_name;
                                txtBurmese.Text = tgr.mlr_mr_name;
                                txtOtherLanguage1.Text = tgr.mlr_ftxt_name1;
                                txtOtherLanguage2.Text = tgr.mlr_ftxt_name2;
                            }
                        }
                    }
                }
            }
        }
        private void gridAge_SelectionChanged(object sender, EventArgs e)
        {
            txtDayMin.Text = null;
            txtYearMin.Text = null;
            txtDayMax.Text = null;
            txtYearMax.Text = null;
            if (ga.Count > 0)
            {
                if (gridAge.SelectedRows.Count > 0)
                {
                    int row = gridAge.SelectedRows[0].Index;
                    if (row > -1)
                    {
                        int? mla_id = convInt(gridAge["col_mla_id", row].Value);
                        if (mla_id != null)
                        {
                            clGridAge tga = ga.Single(x => x.mla_id == mla_id);
                            if (tga != null)
                            {
                                txtDayMin.Text = tga.mla_min_day.ToString();
                                txtYearMin.Text = tga.mla_min_age.ToString();
                                txtDayMax.Text = tga.mla_max_day.ToString();
                                txtYearMax.Text = tga.mla_max_age.ToString();
                            }
                        }
                    }
                }
            }
        }
        private void GridLabData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView DGV = (DataGridView)sender;
            DGV.SetRuningNumber();
        }
        private void GridCondition_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView DGV = (DataGridView)sender;
            DGV.SetRuningNumber();
        }
        private void GridResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView DGV = (DataGridView)sender;
            DGV.SetRuningNumber("ColNo3");
        }
        private void gridAge_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView DGV = (DataGridView)sender;
            DGV.SetRuningNumber("ColNo4");
        }
        private void GridResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mode_frm_tab1 != modeFrm.unable_edit)
            {
                if (GridResult.Columns[e.ColumnIndex].Name == "Colselect")
                {
                    if (GridResult.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxColumn))
                    {
                        if (GridResult.SelectedRows.Count > 0)
                        {
                            int? mlr_id = convInt(GridResult["Colmlr_id", e.RowIndex].Value);
                            if (mlr_id != null)
                            {
                                gr.ForEach(x => x.result_lab = (x.mlr_id == mlr_id ? true : false));
                                GridResult.Refresh();
                            }

                            if (gc.Count > 0)
                            {
                                if (GridCondition.SelectedRows.Count > 0)
                                {
                                    int rowCondi = GridCondition.SelectedRows[0].Index;
                                    clGridCondition tgc = gc.Single(x => x.rowID == (convInt(GridCondition["ColrowID", rowCondi].Value)));
                                    tgc.mlr_id = mlr_id;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void gridAge_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mode_frm_tab1 != modeFrm.unable_edit)
            {
                if (gridAge.Columns[e.ColumnIndex].Name == "ColSelect2")
                {
                    if (gridAge.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxColumn))
                    {
                        if (gridAge.SelectedRows.Count > 0)
                        {
                            int? mla_id = convInt(gridAge["col_mla_id", e.RowIndex].Value);
                            if (mla_id != null)
                            {
                                ga.ForEach(x => x.age_lab = (x.mla_id == mla_id ? true : false));
                                gridAge.Refresh();
                            }

                            if (gc.Count > 0)
                            {
                                if (GridCondition.SelectedRows.Count > 0)
                                {
                                    int rowCondi = GridCondition.SelectedRows[0].Index;
                                    clGridCondition tgc = gc.Single(x => x.rowID == (convInt(GridCondition["ColrowID", rowCondi].Value)));
                                    tgc.mla_id = mla_id;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveDataTab1();
        }
        private void GridLabData_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (GridLabData.Focused == true)
            {
                saveDataTab1();
            }
        }
        private void GridCondition_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (GridCondition.Focused == true)
            {
                saveDataTab1();
            }
        }
        private void saveDataTab1()
        {
            mode_frm_tab1 = modeFrm.unable_edit;
            clGridCondition temp;
            clGridCondition tgc;
            int row;
            int? mlp_id;
            Boolean chkSame = true;
            if (GridCondition.SelectedRows.Count > 0)
            {
                row = GridCondition.SelectedRows[0].Index;
                mlp_id = convInt(GridCondition["ColrowID", row].Value);
                if (mlp_id != null)
                {
                    temp = (from mlp in dbc.mst_lab_results
                            where mlp.mlp_id == mlp_id
                            select new clGridCondition
                            {
                                rowID = mlp.mlp_id,
                                mla_id = mlp.mla_id,
                                mlr_id = mlp.mlr_id,
                                mlp_cond_seq = mlp.mlp_cond_seq,
                                mlp_summary = mlp.mlp_summary,
                                mlp_condition = mlp.mlp_condition,
                                mlp_status = mlp.mlp_status,
                                mlp_effective_date = mlp.mlp_effective_date,
                                mlp_expire_date = mlp.mlp_expire_date,
                                mla_min_day = mlp.mst_lab_age.mla_min_day,
                                mla_min_age = mlp.mst_lab_age.mla_min_age,
                                mla_max_day = mlp.mst_lab_age.mla_max_day,
                                mla_max_age = mlp.mst_lab_age.mla_max_age,
                                mla_sex = mlp.mst_lab_age.mla_sex,
                                mla_vstand_max = mlp.mst_lab_age.mla_vstand_max,
                                mla_vstand_min = mlp.mst_lab_age.mla_vstand_min,
                                mla_vstand_unit = mlp.mst_lab_age.mla_vstand_unit
                            }).FirstOrDefault();
                    tgc = gc.Single(x => x.rowID == mlp_id);
                    if (!(tgc.Equals(temp)))
                    {
                        chkSame = false;
                    }
                    if (chkSame == false)
                    {
                        DialogResult dr = MessageBox.Show("D", "", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            mst_lab_result mst_lr = (from mlp in dbc.mst_lab_results
                                                     where mlp.mlp_id == convIntNotNull(mlp_id)
                                                     select mlp).FirstOrDefault();
                            if (mst_lr != null)
                            {
                                mst_lr.mla_id = tgc.mla_id;
                                mst_lr.mlr_id = tgc.mlr_id;
                                mst_lr.mlp_cond_seq = tgc.mlp_cond_seq;
                                mst_lr.mlp_summary = tgc.mlp_summary;
                                mst_lr.mlp_condition = tgc.mlp_condition;
                                mst_lr.mlp_status = tgc.mlp_status;
                                mst_lr.mlp_effective_date = tgc.mlp_effective_date;
                                mst_lr.mlp_expire_date = tgc.mlp_expire_date;
                                mst_lr.mlp_update_by = Program.CurrentUser.mut_e_fname;
                                mst_lr.mlp_update_date = DateTime.Now;
                                dbc.SubmitChanges();
                            }
                        }
                        else
                        {
                            tgc.rowID = temp.rowID;
                            tgc.mla_id = temp.mla_id;
                            tgc.mlr_id = temp.mlr_id;
                            tgc.mla_max_age = temp.mla_max_age;
                            tgc.mla_max_day = temp.mla_max_day;
                            tgc.mla_min_age = temp.mla_min_age;
                            tgc.mla_min_day = temp.mla_min_day;
                            tgc.mla_sex = temp.mla_sex;
                            tgc.mla_vstand_max = temp.mla_vstand_max;
                            tgc.mla_vstand_min = temp.mla_vstand_min;
                            tgc.mla_vstand_unit = temp.mla_vstand_unit;
                            tgc.mlp_cond_seq = temp.mlp_cond_seq;
                            tgc.mlp_condition = temp.mlp_condition;
                            tgc.mlp_effective_date = temp.mlp_effective_date;
                            tgc.mlp_expire_date = temp.mlp_expire_date;
                            tgc.mlp_status = temp.mlp_status;
                            tgc.mlp_summary = temp.mlp_summary;
                        }
                    }
                }
                else
                {
                    tgc = gc.Single(x => x.rowID == null);
                    int? sequence;
                    var tempSeq = new DBCheckup.InhCheckupDataContext().mst_lab_results.Where(x => x.mla_id == tgc.mla_id);
                    if (tempSeq.Count() > 0)
                    {
                        sequence = tempSeq != null ? tempSeq.OrderByDescending(x => x.mlp_cond_seq).FirstOrDefault().mlp_cond_seq + 1 : 1;
                    }
                    else
                    {
                        sequence = 1;
                    }

                    DialogResult dr = MessageBox.Show("", "", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        mst_lab_result lab_result = new mst_lab_result()
                        {
                            mla_id = tgc.mla_id,
                            mlr_id = tgc.mlr_id,
                            mlp_cond_seq = sequence,
                            mlp_summary = tgc.mlp_summary,
                            mlp_condition = tgc.mlp_condition,
                            mlp_status = tgc.mlp_status,
                            mlp_effective_date = tgc.mlp_effective_date,
                            mlp_expire_date = tgc.mlp_expire_date,
                            mlp_create_by = Program.CurrentUser.mut_e_fname,
                            mlp_create_date = DateTime.Now,
                            mlp_update_by = Program.CurrentUser.mut_e_fname,
                            mlp_update_date = DateTime.Now
                        };
                        dbc.mst_lab_results.InsertOnSubmit(lab_result);
                        dbc.SubmitChanges();
                    }
                    else
                    {
                        foreach (DataGridViewRow dgRow in GridCondition.Rows)
                        {
                            if (dgRow.Cells["ColrowID"].Value == null)
                            {
                                CurrencyManager cm = (CurrencyManager)BindingContext[this.GridCondition.DataSource];

                                cm.SuspendBinding();
                                dgRow.Visible = false;
                                cm.ResumeBinding();
                            }
                        }
                        gc.Remove(tgc);
                    }
                }
            }
        }
        private void btnCon_Click(object sender, EventArgs e)
        {
            SetCondition(sender);
        }
        private void SetCondition(object sender)
        {
            Button btntag = (Button)sender;
            string strcondition = btntag.Tag.ToString();

            txtCondition.Focus();
            var pos = Cursor.Position;
            int num = 0;

            switch (strcondition)
            {
                case "(a or b)":
                    txtCondition.Text = string.Format("({0}) or ({1}) ", txtCondition.Text, " ");
                    num = txtCondition.GetCharIndexFromPosition(pos) - 2;
                    txtCondition.SelectionStart = num;
                    break;
                case @"“”":
                    txtCondition.Text = string.Format(@"{0} """"", txtCondition.Text, "");
                    num = txtCondition.GetCharIndexFromPosition(pos);
                    txtCondition.SelectionStart = num;
                    break;
                default:
                    txtCondition.Text += strcondition + " ";
                    num = txtCondition.GetCharIndexFromPosition(pos);
                    txtCondition.SelectionStart = num + 1;
                    break;
            }

            //MessageBox.Show(btntag.Tag.ToString());
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            mode_frm_tab1 = modeFrm.able_edit;
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            mode_frm_tab1 = modeFrm.add_new;
        }
        private void txtCondition_TextChanged(object sender, EventArgs e)
        {
            if (GridCondition.SelectedRows.Count > 0)
            {
                int row = GridCondition.SelectedRows[0].Index;
                int? mlp_id = convInt(GridCondition["ColrowID", row].Value);
                clGridCondition tgc = gc.Single(x => x.rowID == mlp_id);
                tgc.mlp_condition = txtCondition.Text;
            }
        }
        private void Ch_viewActive_CheckedChanged(object sender, EventArgs e)
        {
            if (GridCondition.SelectedRows.Count > 0)
            {
                int row = GridCondition.SelectedRows[0].Index;
                int? mlp_id = convInt(GridCondition["ColrowID", row].Value);
                clGridCondition tgc = gc.Single(x => x.rowID == mlp_id);
                if (Ch_viewActive.Checked == true)
                {
                    tgc.mlp_status = 'A';
                }
                else
                {
                    tgc.mlp_status = null;
                }
            }
        }
        private void txtDateStart_ValueChanged(object sender, EventArgs e)
        {
            if (GridCondition.SelectedRows.Count > 0)
            {
                int row = GridCondition.SelectedRows[0].Index;
                int? mlp_id = convInt(GridCondition["ColrowID", row].Value);
                clGridCondition tgc = gc.Single(x => x.rowID == mlp_id);
                tgc.mlp_effective_date = txtDateStart.Value;
            }
        }
        private void txtDateEnd_ValueChanged(object sender, EventArgs e)
        {
            //if (GridCondition.SelectedRows.Count > 0)
            //{
            //    int row = GridCondition.SelectedRows[0].Index;
            //    int? mlp_id = convInt(GridCondition["ColrowID", row].Value);
            //    clGridCondition tgc = gc.Single(x => x.rowID == mlp_id);
            //    tgc.mlp_effective_date = txtDateEnd.Value;
            //    GridCondition.Refresh();
            //}
        }
        private void SetFromReadOnly()
        {
            Ch_viewActive.Enabled = false;
            txtDateStart.Enabled = false;
            txtDateEnd.Enabled = false;
            btnAddNew.Enabled = true;
            btnEdit.Enabled = true;
            btnSave.Enabled = false;
            panel1.Enabled = false;
            txtCondition.Enabled = false;
        }
        private void SetFromEdit()
        {
            Ch_viewActive.Enabled = true;
            txtDateStart.Enabled = true;
            txtDateEnd.Enabled = true;
            btnAddNew.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            panel1.Enabled = true;
            txtCondition.Enabled = true;
        }
        private void btnTestLabConnection_Click(object sender, EventArgs e)
        {
            string mlb_code = txtLabCode.Text;
            string condition = txtCondition.Text;
            if (verify_condition(txtLabCode.Text, txtCondition.Text) == true)
            {
                MessageBox.Show("Condition Pass.", "Condition");
            }
            else
            {
                MessageBox.Show("Condition Error.", "Condition");
            }

        }
        private Boolean? verify_condition(string mlb_code, string condition)
        {
            Boolean? chkError = null;
            dbc.sp_verify_condition_error(mlb_code, condition, ref chkError);
            return chkError;
        }
        private modeFrm _mode_frm_tab1;
        private modeFrm mode_frm_tab1
        {
            get
            {
                return _mode_frm_tab1;
            }
            set
            {
                if (value == modeFrm.unable_edit)
                {
                    SetFromReadOnly();
                    lb1MsgAlert.Text = string.Empty;
                }
                else if (value == modeFrm.able_edit)
                {
                    SetFromEdit();
                    lb1MsgAlert.Text = "Edit Mode";
                }
                else if (value == modeFrm.add_new)
                {
                    gc.Add(new clGridCondition());
                    GridCondition.DataSource = new List<clGridCondition>();
                    GridCondition.DataSource = gc;
                    GridCondition.ClearSelection();
                    GridCondition.Rows[GridCondition.Rows.Count - 1].Selected = true;
                    GridCondition.CurrentCell = GridCondition.Rows[GridCondition.Rows.Count - 1].Cells[0];
                    GridCondition.FirstDisplayedScrollingRowIndex = GridCondition.Rows.Count - 1;
                    foreach (DataGridViewRow dgRow in gridAge.Rows)
                    {
                        dgRow.Cells["Colselect2"].Value = false;
                    } foreach (DataGridViewRow dgRow in GridResult.Rows)
                    {
                        dgRow.Cells["Colselect"].Value = false;
                    }
                    lb1MsgAlert.Text = "Add New Mode";
                    gridAge.Refresh();
                    GridResult.Refresh();
                    SetFromEdit();
                }
                _mode_frm_tab1 = value;
            }
        }
        #endregion

        #region "Tab2"
        #region "mst lab group"
        private void gridLabGroupT2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).SetRuningNumber("ColNotab21");
        }
        private void mst_lab_groupBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_lab_group mlg = (mst_lab_group)mst_lab_groupBindingSource.Current;
            if (mlg != null)
            {
                char? status = mlg.mlg_status;
                ch21_Active.Checked = (status == 'A') ? true : false;
            }
            else
            {
                ch21_Active.Checked = false;
                dtpStGroup.Value = DateTime.Today;
                dtpEndGroup.Value = dtpEndGroup.MaxDate;
            }
        }
        private void ch21_Active_CheckedChanged(object sender, EventArgs e)
        {
            mst_lab_group mlg = ((mst_lab_group)mst_lab_groupBindingSource.Current);
            if (mlg != null)
            {
                if (ch21_Active.Checked == true)
                {
                    mlg.mlg_status = 'A';
                }
                else
                {
                    mlg.mlg_status = null;
                }
            }
        }
        #endregion
        #region "mst lab"
        private void gridLabT2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).SetRuningNumber("ColNotab22");
        }
        private void mst_labsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_lab mlb = (mst_lab)mst_labsBindingSource.Current;
            if (mlb != null)
            {
                char? status = mlb.mlb_status;
                ch22_Active.Checked = (status == 'A') ? true : false;
                char? type = mlb.mlb_type;
                if (type == 'N')
                {
                    rdNorType.Checked = true;
                }
                else if (type == 'S')
                {
                    rdSpcType.Checked = true;
                }
                else
                {
                    rdNorType.Checked = false;
                    rdSpcType.Checked = false;
                }
                char? val_type = mlb.mlb_value_type;
                if (val_type == 'N')
                {
                    rdNumber.Checked = true;
                }
                else if (val_type == 'S')
                {
                    rdString.Checked = true;
                }
                else
                {
                    rdNumber.Checked = false;
                    rdString.Checked = false;
                }
            }
            else
            {
                ch22_Active.Checked = false;
                rdNorType.Checked = false;
                rdSpcType.Checked = false;
                rdNumber.Checked = false;
                rdString.Checked = false;
                dtpStItem.Value = DateTime.Today;
                dtpEndItem.Value = dtpEndGroup.MaxDate;
            }
        }
        private void rdNorType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNorType.Checked == true)
            {
                mst_lab mlb = ((mst_lab)mst_labsBindingSource.Current);
                if (mlb != null) mlb.mlb_type = 'N';
            }
        }
        private void rdSpcType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSpcType.Checked == true)
            {
                mst_lab mlb = ((mst_lab)mst_labsBindingSource.Current);
                if (mlb != null) mlb.mlb_type = 'S';
            }
        }
        private void rdNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNumber.Checked == true)
            {
                mst_lab mlb = ((mst_lab)mst_labsBindingSource.Current);
                if (mlb != null) mlb.mlb_value_type = 'N';
            }
        }
        private void rdString_CheckedChanged(object sender, EventArgs e)
        {
            if (rdString.Checked == true)
            {
                mst_lab mlb = ((mst_lab)mst_labsBindingSource.Current);
                if (mlb != null) mlb.mlb_value_type = 'S';
            }
        }
        private void ch22_Active_CheckedChanged(object sender, EventArgs e)
        {
            mst_lab mlb = ((mst_lab)mst_labsBindingSource.Current);
            if (mlb != null)
            {
                if (ch22_Active.Checked == true)
                {
                    mlb.mlb_status = 'A';
                }
                else
                {
                    mlb.mlb_status = null;
                }
            }
        }
        #endregion
        #region "mst lab recom"
        private void gridLabRecomT2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).SetRuningNumber("ColNotab24");
        }
        private void mst_lab_recomsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_lab_recom mlr = (mst_lab_recom)mst_lab_recomsBindingSource.Current;
            if (mlr != null)
            {
                char? status = mlr.mlr_status;
                ch24_Active.Checked = (status == 'A') ? true : false;
                char? summary = mlr.mlr_summary;
                if (summary == 'N')
                {
                    RDNormal.Checked = true;
                }
                else if (summary == 'A')
                {
                    RDABNormal.Checked = true;
                }
                else
                {
                    RDNormal.Checked = false;
                    RDABNormal.Checked = false;
                }
            }
            else
            {
                ch24_Active.Checked = false;
                RDNormal.Checked = false;
                RDABNormal.Checked = false;
                dtpStRecom.Value = DateTime.Today;
                dtpEndRecom.Value = dtpEndGroup.MaxDate;
            }
        }
        private void RDNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (RDNormal.Checked == true)
            {
                mst_lab_recom mlr = ((mst_lab_recom)mst_lab_recomsBindingSource.Current);
                if (mlr != null) mlr.mlr_summary = 'N';
            }
        }
        private void RDABNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (RDABNormal.Checked == true)
            {
                mst_lab_recom mlr = ((mst_lab_recom)mst_lab_recomsBindingSource.Current);
                if (mlr != null) mlr.mlr_summary = 'A';
            }
        }
        private void ch24_Active_CheckedChanged(object sender, EventArgs e)
        {
            mst_lab_recom mlr = ((mst_lab_recom)mst_lab_recomsBindingSource.Current);
            if (mlr != null)
            {
                if (ch24_Active.Checked == true)
                {
                    mlr.mlr_status = 'A';
                }
                else
                {
                    mlr.mlr_status = null;
                }
            }
        }
        #endregion
        #region "mst lab age"
        private void gridLabAgeT2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).SetRuningNumber("ColNotab23");
        }
        private void mst_lab_agesBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_lab_age mla = (mst_lab_age)mst_lab_agesBindingSource.Current;
            if (mla != null)
            {
                char? status = mla.mla_status;
                ch23_Active.Checked = (status == 'A') ? true : false;
                char? sex = mla.mla_sex;
                if (sex == 'M')
                { RD2_Male.Checked = true; }
                else if (sex == 'F')
                { RD2_Female.Checked = true; }
                else
                {
                    RD2_Male.Checked = false;
                    RD2_Female.Checked = false;
                }
            }
            else
            {
                ch23_Active.Checked = false;
                RD2_Male.Checked = false;
                RD2_Female.Checked = false;
                dtpStAge.Value = DateTime.Today;
                dtpEndAge.Value = dtpEndGroup.MaxDate;
            }
        }
        private void RD2_Male_CheckedChanged(object sender, EventArgs e)
        {
            if (RD2_Male.Checked == true)
            {
                mst_lab_age mla = ((mst_lab_age)mst_lab_agesBindingSource.Current);
                if (mla != null) mla.mla_sex = 'M';
            }
        }
        private void RD2_Female_CheckedChanged(object sender, EventArgs e)
        {
            if (RD2_Female.Checked == true)
            {
                mst_lab_age mla = ((mst_lab_age)mst_lab_agesBindingSource.Current);
                if (mla != null) mla.mla_sex = 'F';
            }
        }
        private void ch23_Active_CheckedChanged(object sender, EventArgs e)
        {
            mst_lab_age mla = ((mst_lab_age)mst_lab_agesBindingSource.Current);
            if (mla != null)
            {
                if (ch23_Active.Checked == true)
                {
                    mla.mla_status = 'A';
                }
                else
                {
                    mla.mla_status = null;
                }
            }
        }
        #endregion

        private void Tab2LoadData()
        {
            mst_lab_groupBindingSource.DataSource = dbc.mst_lab_groups;
            var objSex = new List<cbData>()
            {
                new cbData() { Code = "", Name = "All"},
                new cbData() { Code = "M", Name = "Male"},
                new cbData() { Code = "F", Name = "Female"}
            };
            cbSearchGender.DataSource = objSex;
            cbSearchGender.DisplayMember = "Name";
            cbSearchGender.ValueMember = "Code";
            cbSearchGender.SelectedIndex = 0;
            setReadOnlyTab2();
        }
        private void btnEditAll_Click(object sender, EventArgs e)
        {
            setEditTab2();
        }
        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            mst_lab_groupBindingSource.AddNew();
            setEditTab2(pnGroup);
        }
        private void btnNewItem_Click(object sender, EventArgs e)
        {
            mst_labsBindingSource.AddNew();
            setEditTab2(pnLab);
        }
        private void btnNewSex_Click(object sender, EventArgs e)
        {
            mst_lab_agesBindingSource.AddNew();
            setEditTab2(pnRecom);
        }
        private void btnNewResult_Click(object sender, EventArgs e)
        {
            mst_lab_recomsBindingSource.AddNew();
            setEditTab2(pnAge);
        }
        private void btnsaveTab2_Click(object sender, EventArgs e)
        {
            saveTab2();
            setReadOnlyTab2();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
        private void gridLabGroupT2_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Focused == true)
            {
                saveTab2();
            }
        }
        private void gridLabT2_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Focused == true)
            {
                saveTab2();
            }
        }
        private void gridLabRecomT2_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Focused == true)
            {
                saveTab2();
            }
        }
        private void gridLabAgeT2_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Focused == true)
            {
                saveTab2();
            }
        }
        private void txtSearchGroup_TextChanged(object sender, EventArgs e)
        {
            string txtSearch = txtSearchGroup.Text.ToUpper().Trim();
            int bsCount = mst_lab_groupBindingSource.Count;
            for (int i = 0; i < bsCount; i++)
            {
                mst_lab_group mlg = (mst_lab_group)mst_lab_groupBindingSource.List[i];
                if (mlg.mlg_tname.ToUpper().Contains(txtSearch) || mlg.mlg_ename.ToUpper().Contains(txtSearch))
                {
                    mst_lab_groupBindingSource.Position = i;
                    break;
                }
            }
        }
        private void txtSearchLab_TextChanged(object sender, EventArgs e)
        {
            string txtSearch = txtSearchLab.Text.ToUpper().Trim();
            int bsCount = mst_labsBindingSource.Count;
            for (int i = 0; i < bsCount; i++)
            {
                mst_lab mlb = (mst_lab)mst_labsBindingSource.List[i];
                if (mlb.mlb_code.ToUpper().Contains(txtSearch) || mlb.mlb_tname.ToUpper().Contains(txtSearch)
                    || mlb.mlb_ename.ToUpper().Contains(txtSearch))
                {
                    mst_labsBindingSource.Position = i;
                    break;
                }
            }
        }
        private void txtSearchRecom_TextChanged(object sender, EventArgs e)
        {
            string txtSearch = txtSearchRecom.Text.ToUpper().Trim();
            int bsCount = mst_lab_recomsBindingSource.Count;
            for (int i = 0; i < bsCount; i++)
            {
                mst_lab_recom mlr = (mst_lab_recom)mst_lab_recomsBindingSource.List[i];
                if (mlr.mlr_th_name.ToUpper().Contains(txtSearch) || mlr.mlr_en_name.ToUpper().Contains(txtSearch))
                {
                    mst_lab_recomsBindingSource.Position = i;
                    break;
                }
            }
        }
        private void searchAge()
        {
            string txtSearchAMin = txtSearchAgeMin.Text.ToUpper().Trim();
            string txtSearchDMin = txtSearchDayMin.Text.ToUpper().Trim();
            string txtSearchAMax = txtSearchAgeMax.Text.ToUpper().Trim();
            string txtSearchDMax = txtSearchDayMax.Text.ToUpper().Trim();
            string txtSex = cbSearchGender.SelectedValue.ToString().ToUpper();
            int bsCount = mst_lab_agesBindingSource.Count;
            for (int i = 0; i < bsCount; i++)
            {
                mst_lab_age mla = (mst_lab_age)mst_lab_agesBindingSource.List[i];
                if (mla.mla_min_age.ToString().Contains(txtSearchAMin) && mla.mla_min_day.ToString().Contains(txtSearchDMin)
                    && mla.mla_max_age.ToString().Contains(txtSearchAMax) && mla.mla_max_day.ToString().Contains(txtSearchDMax)
                    && mla.mla_sex.ToString().Contains(txtSex))
                {
                    mst_lab_agesBindingSource.Position = i;
                    break;
                }
            }
        }
        private void cbSearchGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchAge();
        }
        private void txtSearchAgeMin_TextChanged(object sender, EventArgs e)
        {
            searchAge();
        }
        private void txtSearchDayMin_TextChanged(object sender, EventArgs e)
        {
            searchAge();
        }
        private void txtSearchAgeMax_TextChanged(object sender, EventArgs e)
        {
            searchAge();
        }
        private void txtSearchDayMax_TextChanged(object sender, EventArgs e)
        {
            searchAge();
        }
        private void button46_Click(object sender, EventArgs e)
        {
            mst_lab_groupBindingSource.EndEdit();
            mst_labsBindingSource.EndEdit();
            mst_lab_agesBindingSource.EndEdit();
            mst_lab_recomsBindingSource.EndEdit();
            if (dbc.GetChangeSet().Inserts.Count > 0)
            {
                var tb = dbc.GetChangeSet().Inserts[0];
                if (tb.GetType() == typeof(mst_lab_group))
                {
                    mst_lab_groupBindingSource.Remove(tb);
                }
                else if (tb.GetType() == typeof(mst_lab))
                {
                    mst_labsBindingSource.Remove(tb);
                }
                else if (tb.GetType() == typeof(mst_lab_age))
                {
                    mst_lab_agesBindingSource.Remove(tb);
                }
                else if (tb.GetType() == typeof(mst_lab_recom))
                {
                    mst_lab_recomsBindingSource.Remove(tb);
                }
                dbc.SubmitChanges();
            }
            else if (dbc.GetChangeSet().Updates.Count > 0)
            {
                var tb = dbc.GetChangeSet().Updates[0];
                if (tb.GetType() == typeof(mst_lab_group))
                {
                    var ori = dbc.mst_lab_groups.GetOriginalEntityState((mst_lab_group)tb);
                    mst_lab_group mlg = (mst_lab_group)tb;
                    mlg.mlg_id = ori.mlg_id;
                    mlg.mlg_code = ori.mlg_code;
                    mlg.mlg_tname = ori.mlg_tname;
                    mlg.mlg_ename = ori.mlg_ename;
                    mlg.mlg_department = ori.mlg_department;
                    mlg.mlg_status = ori.mlg_status;
                    mlg.mlg_effective_date = ori.mlg_effective_date;
                    mlg.mlg_expire_date = ori.mlg_expire_date;
                    mlg.mlg_create_date = ori.mlg_create_date;
                    mlg.mlg_create_by = ori.mlg_create_by;
                    mlg.mlg_update_date = ori.mlg_update_date;
                    mlg.mlg_update_by = ori.mlg_update_by;
                }
                else if (tb.GetType() == typeof(mst_lab))
                {
                    var ori = dbc.mst_labs.GetOriginalEntityState((mst_lab)tb);
                    mst_lab mlb = (mst_lab)tb;
                    mlb.mlb_id = ori.mlb_id;
                    mlb.mlg_id = ori.mlg_id;
                    mlb.mlb_code = ori.mlb_code;
                    mlb.mlb_create_by = ori.mlb_create_by;
                    mlb.mlb_create_date = ori.mlb_create_date;
                    mlb.mlb_effective_date = ori.mlb_effective_date;
                    mlb.mlb_ename = ori.mlb_ename;
                    mlb.mlb_expire_date = ori.mlb_expire_date;
                    mlb.mlb_lab_set = ori.mlb_lab_set;
                    mlb.mlb_lab_setname = ori.mlb_lab_setname;
                    mlb.mlb_status = ori.mlb_status;
                    mlb.mlb_tname = ori.mlb_tname;
                    mlb.mlb_type = ori.mlb_type;
                    mlb.mlb_update_by = ori.mlb_update_by;
                    mlb.mlb_update_date = ori.mlb_update_date;
                    mlb.mlb_value_type = ori.mlb_value_type;
                }
                else if (tb.GetType() == typeof(mst_lab_age))
                {
                    var ori = dbc.mst_lab_ages.GetOriginalEntityState((mst_lab_age)tb);
                    mst_lab_age mla = (mst_lab_age)tb;
                    mla.mla_id = ori.mla_id;
                    mla.mlb_id = ori.mlb_id;
                    mla.mla_create_by = ori.mla_create_by;
                    mla.mla_create_date = ori.mla_create_date;
                    mla.mla_effective_date = ori.mla_effective_date;
                    mla.mla_expire_date = ori.mla_expire_date;
                    mla.mla_max_age = ori.mla_max_age;
                    mla.mla_max_day = ori.mla_max_day;
                    mla.mla_min_age = ori.mla_min_age;
                    mla.mla_min_day = ori.mla_min_day;
                    mla.mla_sex = ori.mla_sex;
                    mla.mla_status = ori.mla_status;
                    mla.mla_update_by = ori.mla_update_by;
                    mla.mla_update_date = ori.mla_update_date;
                    //mla.mla_vstand_max = ori.mla_vstand_max;
                    mla.mla_vstand_nrange = ori.mla_vstand_nrange;
                    mla.mla_vstand_min = ori.mla_vstand_min;
                    mla.mla_vstand_unit = ori.mla_vstand_unit;
                }
                else if (tb.GetType() == typeof(mst_lab_recom))
                {
                    var ori = dbc.mst_lab_recoms.GetOriginalEntityState((mst_lab_recom)tb);
                    mst_lab_recom mlr = (mst_lab_recom)tb;
                    mlr.mlr_id = ori.mlr_id;
                    mlr.mlb_id = ori.mlb_id;
                    mlr.mlr_ar_name = ori.mlr_ar_name;
                    mlr.mlr_ch_name = ori.mlr_ch_name;
                    mlr.mlr_code = ori.mlr_code;
                    mlr.mlr_create_by = ori.mlr_create_by;
                    mlr.mlr_create_date = ori.mlr_create_date;
                    mlr.mlr_default = ori.mlr_default;
                    mlr.mlr_effective_date = ori.mlr_effective_date;
                    mlr.mlr_en_name = ori.mlr_en_name;
                    mlr.mlr_expire_date = ori.mlr_expire_date;
                    mlr.mlr_ftxt_name1 = ori.mlr_ftxt_name1;
                    mlr.mlr_ftxt_name2 = ori.mlr_ftxt_name2;
                    mlr.mlr_jp_name = ori.mlr_jp_name;
                    mlr.mlr_kr_name = ori.mlr_kr_name;
                    mlr.mlr_mr_name = ori.mlr_mr_name;
                    mlr.mlr_status = ori.mlr_status;
                    mlr.mlr_summary = ori.mlr_summary;
                    mlr.mlr_th_name = ori.mlr_th_name;
                    mlr.mlr_update_by = ori.mlr_update_by;
                    mlr.mlr_update_date = ori.mlr_update_date;
                }
            }
        }
        private void setEditTab2(Panel panalEdit)
        {
            gbSearchGroup.Enabled = false;
            gbSearchLab.Enabled = false;
            gbSearchRecom.Enabled = false;
            gbSearchAge.Enabled = false;
            gridLabGroupT2.Enabled = false;
            gridLabT2.Enabled = false;
            gridLabAgeT2.Enabled = false;
            gridLabRecomT2.Enabled = false;
            pnGroup.Enabled = panalEdit == pnGroup;
            pnLab.Enabled = panalEdit == pnLab;
            pnRecom.Enabled = panalEdit == pnRecom;
            pnAge.Enabled = panalEdit == pnAge;
            btnEditAll.Enabled = false;
            btnNewGroup.Enabled = false;
            btnNewItem.Enabled = false;
            btnNewResult.Enabled = false;
            btnNewSex.Enabled = false;
            btnsaveTab2.Enabled = true;
            btnCancel.Enabled = true;
        }
        private void setEditTab2()
        {
            gbSearchGroup.Enabled = false;
            gbSearchLab.Enabled = false;
            gbSearchRecom.Enabled = false;
            gbSearchAge.Enabled = false;
            pnGroup.Enabled = true;
            pnLab.Enabled = true;
            pnRecom.Enabled = true;
            pnAge.Enabled = true;
            btnEditAll.Enabled = false;
            btnNewGroup.Enabled = false;
            btnNewItem.Enabled = false;
            btnNewResult.Enabled = false;
            btnNewSex.Enabled = false;
            btnsaveTab2.Enabled = true;
            btnCancel.Enabled = true;
        }
        private void setReadOnlyTab2()
        {
            gbSearchGroup.Enabled = true;
            gbSearchLab.Enabled = true;
            gbSearchRecom.Enabled = true;
            gbSearchAge.Enabled = true;
            gridLabGroupT2.Enabled = true;
            gridLabT2.Enabled = true;
            gridLabAgeT2.Enabled = true;
            gridLabRecomT2.Enabled = true;
            pnGroup.Enabled = false;
            pnLab.Enabled = false;
            pnRecom.Enabled = false;
            pnAge.Enabled = false;
            btnEditAll.Enabled = true;
            btnNewGroup.Enabled = true;
            btnNewItem.Enabled = true;
            btnNewResult.Enabled = true;
            btnNewSex.Enabled = true;
            btnsaveTab2.Enabled = false;
            btnCancel.Enabled = false;
        }
        private void saveTab2()
        {
            mst_lab_groupBindingSource.EndEdit();
            mst_labsBindingSource.EndEdit();
            mst_lab_agesBindingSource.EndEdit();
            mst_lab_recomsBindingSource.EndEdit();
            if (dbc.GetChangeSet().Inserts.Count > 0)
            {
                DialogResult dr = MessageBox.Show("new", "", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    var tb = dbc.GetChangeSet().Inserts[0];
                    if (tb.GetType() == typeof(mst_lab_group))
                    {
                        dbc.mst_lab_groups.InsertOnSubmit((mst_lab_group)tb);
                    }
                    else if (tb.GetType() == typeof(mst_lab))
                    {
                        dbc.mst_labs.InsertOnSubmit((mst_lab)tb);
                    }
                    else if (tb.GetType() == typeof(mst_lab_age))
                    {
                        dbc.mst_lab_ages.InsertOnSubmit((mst_lab_age)tb);
                    }
                    else if (tb.GetType() == typeof(mst_lab_recom))
                    {
                        dbc.mst_lab_recoms.InsertOnSubmit((mst_lab_recom)tb);
                    }
                    dbc.SubmitChanges();
                }
                else
                {
                    var tb = dbc.GetChangeSet().Inserts[0];
                    if (tb.GetType() == typeof(mst_lab_group))
                    {
                        mst_lab_groupBindingSource.Remove(tb);
                    }
                    else if (tb.GetType() == typeof(mst_lab))
                    {
                        mst_labsBindingSource.Remove(tb);
                    }
                    else if (tb.GetType() == typeof(mst_lab_age))
                    {
                        mst_lab_agesBindingSource.Remove(tb);
                    }
                    else if (tb.GetType() == typeof(mst_lab_recom))
                    {
                        mst_lab_recomsBindingSource.Remove(tb);
                    }
                    dbc.SubmitChanges();
                }
            }
            else if (dbc.GetChangeSet().Updates.Count > 0)
            {
                DialogResult dr = MessageBox.Show("edit", "", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    dbc.SubmitChanges();
                }
                else
                {
                    var tb = dbc.GetChangeSet().Updates[0];
                    if (tb.GetType() == typeof(mst_lab_group))
                    {
                        var ori = dbc.mst_lab_groups.GetOriginalEntityState((mst_lab_group)tb);
                        mst_lab_group mlg = (mst_lab_group)tb;
                        mlg.mlg_id = ori.mlg_id;
                        mlg.mlg_code = ori.mlg_code;
                        mlg.mlg_tname = ori.mlg_tname;
                        mlg.mlg_ename = ori.mlg_ename;
                        mlg.mlg_department = ori.mlg_department;
                        mlg.mlg_status = ori.mlg_status;
                        mlg.mlg_effective_date = ori.mlg_effective_date;
                        mlg.mlg_expire_date = ori.mlg_expire_date;
                        mlg.mlg_create_date = ori.mlg_create_date;
                        mlg.mlg_create_by = ori.mlg_create_by;
                        mlg.mlg_update_date = ori.mlg_update_date;
                        mlg.mlg_update_by = ori.mlg_update_by;
                    }
                    else if (tb.GetType() == typeof(mst_lab))
                    {
                        var ori = dbc.mst_labs.GetOriginalEntityState((mst_lab)tb);
                        mst_lab mlb = (mst_lab)tb;
                        mlb.mlb_id = ori.mlb_id;
                        mlb.mlg_id = ori.mlg_id;
                        mlb.mlb_code = ori.mlb_code;
                        mlb.mlb_create_by = ori.mlb_create_by;
                        mlb.mlb_create_date = ori.mlb_create_date;
                        mlb.mlb_effective_date = ori.mlb_effective_date;
                        mlb.mlb_ename = ori.mlb_ename;
                        mlb.mlb_expire_date = ori.mlb_expire_date;
                        mlb.mlb_lab_set = ori.mlb_lab_set;
                        mlb.mlb_lab_setname = ori.mlb_lab_setname;
                        mlb.mlb_status = ori.mlb_status;
                        mlb.mlb_tname = ori.mlb_tname;
                        mlb.mlb_type = ori.mlb_type;
                        mlb.mlb_update_by = ori.mlb_update_by;
                        mlb.mlb_update_date = ori.mlb_update_date;
                        mlb.mlb_value_type = ori.mlb_value_type;
                    }
                    else if (tb.GetType() == typeof(mst_lab_age))
                    {
                        var ori = dbc.mst_lab_ages.GetOriginalEntityState((mst_lab_age)tb);
                        mst_lab_age mla = (mst_lab_age)tb;
                        mla.mla_id = ori.mla_id;
                        mla.mlb_id = ori.mlb_id;
                        mla.mla_create_by = ori.mla_create_by;
                        mla.mla_create_date = ori.mla_create_date;
                        mla.mla_effective_date = ori.mla_effective_date;
                        mla.mla_expire_date = ori.mla_expire_date;
                        mla.mla_max_age = ori.mla_max_age;
                        mla.mla_max_day = ori.mla_max_day;
                        mla.mla_min_age = ori.mla_min_age;
                        mla.mla_min_day = ori.mla_min_day;
                        mla.mla_sex = ori.mla_sex;
                        mla.mla_status = ori.mla_status;
                        mla.mla_update_by = ori.mla_update_by;
                        mla.mla_update_date = ori.mla_update_date;
                        //mla.mla_vstand_max = ori.mla_vstand_max;
                        mla.mla_vstand_nrange = ori.mla_vstand_nrange;
                        mla.mla_vstand_min = ori.mla_vstand_min;
                        mla.mla_vstand_unit = ori.mla_vstand_unit;
                    }
                    else if (tb.GetType() == typeof(mst_lab_recom))
                    {
                        var ori = dbc.mst_lab_recoms.GetOriginalEntityState((mst_lab_recom)tb);
                        mst_lab_recom mlr = (mst_lab_recom)tb;
                        mlr.mlr_id = ori.mlr_id;
                        mlr.mlb_id = ori.mlb_id;
                        mlr.mlr_ar_name = ori.mlr_ar_name;
                        mlr.mlr_ch_name = ori.mlr_ch_name;
                        mlr.mlr_code = ori.mlr_code;
                        mlr.mlr_create_by = ori.mlr_create_by;
                        mlr.mlr_create_date = ori.mlr_create_date;
                        mlr.mlr_default = ori.mlr_default;
                        mlr.mlr_effective_date = ori.mlr_effective_date;
                        mlr.mlr_en_name = ori.mlr_en_name;
                        mlr.mlr_expire_date = ori.mlr_expire_date;
                        mlr.mlr_ftxt_name1 = ori.mlr_ftxt_name1;
                        mlr.mlr_ftxt_name2 = ori.mlr_ftxt_name2;
                        mlr.mlr_jp_name = ori.mlr_jp_name;
                        mlr.mlr_kr_name = ori.mlr_kr_name;
                        mlr.mlr_mr_name = ori.mlr_mr_name;
                        mlr.mlr_status = ori.mlr_status;
                        mlr.mlr_summary = ori.mlr_summary;
                        mlr.mlr_th_name = ori.mlr_th_name;
                        mlr.mlr_update_by = ori.mlr_update_by;
                        mlr.mlr_update_date = ori.mlr_update_date;
                    }
                }
            }
        }
        #endregion

        #region "tab 3"
        private void Tab3LoadData()
        {
            List<DropdownData> dd = new List<DropdownData>(){
                                        new DropdownData { Code = 1, Name = "Thai" },
                                        new DropdownData { Code = 2, Name = "English" },
                                        new DropdownData { Code = 3, Name = "Japan" },
                                        new DropdownData { Code = 4, Name = "China" },
                                        new DropdownData { Code = 5, Name = "Korea" },
                                        new DropdownData { Code = 6, Name = "Arabic" },
                                        new DropdownData { Code = 7, Name = "Myanmar" },
                                        new DropdownData { Code = 8, Name = "Free Text1" },
                                        new DropdownData { Code = 9, Name = "Free Text2" }
                                    };
            cbLang.DataSource = dd;
            cbLang.DisplayMember = "Name";
            cbLang.ValueMember = "Code";
        }
        private void button47_Click(object sender, EventArgs e)
        {
            string sex = null;
            if (rdMaleT3.Checked == true) sex = "M";
            else if (rdFemaleT3.Checked == true) sex = "F";

            string valueType = null;
            if (rdNumT3.Checked == true) valueType = "N";
            else if (rdStringT3.Checked == true) valueType = "S";

            string preg = null;
            if (rdPreTrue.Checked == true) preg = "True";
            else if (rdPreFalse.Checked == true) preg = "False";

            string smoke = null;
            if (rdSmokeTrueT3.Checked == true) smoke = "True";
            if (rdSmokeFalseT3.Checked == true) smoke = "False";

            string diab = null;
            if (rdDiaTrueT3.Checked == true) diab = "True";
            if (rdDiaFalseT3.Checked == true) diab = "False";

            string labDate = null;
            labDate = dtpLabT3.Value.ToString("yyyy-MM-dd");

            string dob = null;
            dob = dtpDOBT3.Value.ToString("yyyy-MM-dd");

            List<output_result> re = getLabresult(txtLabItemT3.Text, sex, labDate, dob, txtLabValueT3.Text, valueType,
                txtVLT3.Text, txtVRT3.Text, txtPulseT3.Text, txtBMIT3.Text, preg, txtSysT3.Text,
                txtDiaT3.Text, smoke, txtHBsAgT3.Text, txtAnti_HBcT3.Text, txtHBsAbT3.Text, txtGluFT3.Text,
                txtGluUT3.Text, diab, txtASTT3.Text, txtALTT3.Text, txtPSAT3.Text, txtAcidPHT3.Text, txtEosinoT3.Text,
                txtParasiteT3.Text, txtMicroalbuminUT3.Text, txtMicroalbuminT3.Text, txtMicroalbuminMT3.Text, txtRBCMorphoT3.Text,
                txtHemoglobinT3.Text, txtHbA2T3.Text, txtMCVT3.Text, txtProteinUT3.Text, txtErythroUT3.Text, txtwbcUT3.Text, txtRBCT3.Text);

            output_grid(re);

        }
        private List<output_result> getLabresult(string Lab_Item, string Lab_Sex, string Lab_date, string Lab_dob, string Lab_value, string Lab_Value_Type,
            string vision_left, string vision_right, string pulse, string bmi, string pregnancy, string systolic,
            string diastolic, string smoke, string HBsAg, string Anti_HBc, string HBsAb, string GlucoseF,
            string GlucoseU, string Diabete, string AST, string ALT, string PSA, string AcidPH, string Eosino,
            string Parasite, string MicroalbuminU, string Microalbumin, string MicroalbuminM, string RBCMorpho,
            string Hemoglobin, string HbA2, string MCV, string ProteinU, string ErythroU, string wbcU, string rbcU)
        {

            DateTime? ldate = convToDate(Lab_date);
            DateTime? ldob = convToDate(Lab_dob);

            float? cVisLeft = ParseNfloat(returnNull(vision_left));
            float? cVisRight = ParseNfloat(returnNull(vision_right));
            float? cPulse = ParseNfloat(returnNull(pulse));
            float? cBmi = ParseNfloat(returnNull(bmi));
            string cPregnancy = returnNull(pregnancy);
            float? cSystolic = ParseNfloat(returnNull(systolic));
            float? cDiastolic = ParseNfloat(returnNull(diastolic));
            string cSmoke = returnNull(smoke);
            string cHBsAg = returnNull(HBsAg);
            string cAnti_HBc = returnNull(Anti_HBc);
            float? cHBsAb = ParseNfloat(returnNull(HBsAb));
            string cGlucoseF = returnNull(GlucoseF);
            string cGlucoseU = returnNull(GlucoseU);
            string cDiabete = returnNull(diastolic);
            float? cAST = ParseNfloat(returnNull(AST));
            float? cALT = ParseNfloat(returnNull(ALT));
            float? cPSA = ParseNfloat(returnNull(PSA));
            float? cAcidPH = ParseNfloat(returnNull(AcidPH));
            float? cEosino = ParseNfloat(returnNull(Eosino));
            string cParasite = returnNull(Parasite);
            float? cMicroalbuminU = ParseNfloat(returnNull(MicroalbuminU));
            float? cMicroalbumin = ParseNfloat(returnNull(Microalbumin));
            float? cMicroalbuminM = ParseNfloat(returnNull(MicroalbuminM));
            string cRBCMorpho = returnNull(RBCMorpho);
            float? cHemoglobin = ParseNfloat(returnNull(Hemoglobin));
            float? cHbA2 = ParseNfloat(returnNull(HbA2));
            float? cMCV = ParseNfloat(returnNull(MCV));
            string cProteinU = returnNull(ProteinU);
            string cErythroU = returnNull(ErythroU);
            string cWbcU = returnNull(wbcU);
            string cRbcU = returnNull(rbcU);


            var result = dbc.ws_return_lab_result(Lab_Item, Lab_Sex, ldate, ldob, Lab_value, Lab_Value_Type,
                         cVisLeft, cVisRight, cPulse, cBmi, cPregnancy, cSystolic, cDiastolic, cSmoke,
                         cHBsAg, cAnti_HBc, cHBsAb, cGlucoseF, cGlucoseU, cDiabete, cAST, cALT, cPSA, cAcidPH, cEosino,
                         cParasite, cMicroalbuminU, cMicroalbumin, cMicroalbuminM, cRBCMorpho, cHemoglobin, cHbA2,
                         cMCV, cProteinU, ErythroU, cWbcU, cRbcU);

            if (result != null)
            {
                var re = (from o in result
                          select new output_result
                          {
                              mlg_id = o.mlg_id,
                              mlb_id = o.mlb_id,
                              mla_id = o.mla_id,
                              mlr_id = o.mlr_id
                          }).ToList();
                if (re != null) return re;
            }
            return null;
        }
        private void output_grid(List<output_result> output)
        {
            if (output != null)
            {
                var result = (from mlb in dbc.mst_labs
                              join mla in dbc.mst_lab_ages
                              on mlb.mlb_id equals mla.mlb_id
                              join mlp in dbc.mst_lab_results
                              on mla.mla_id equals mlp.mla_id
                              join mlr in dbc.mst_lab_recoms
                              on mlp.mlr_id equals mlr.mlr_id
                              where output.Select(x => x.mla_id).ToList().Contains(mla.mla_id)
                              orderby mlb.mlb_code,
                                      !(output.Select(o => o.mlr_id).ToList().Contains(mlr.mlr_id))
                              select new clGridConditionTab3
                              {
                                  result = (output.Select(o => o.mlr_id).ToList().Contains(mlr.mlr_id)) ? Resources.button_ok : Resources.cancel,
                                  mlb_code = mlb.mlb_code,
                                  mlb_name = mlb.mlb_ename,
                                  rangeAge = getAgeRange(mla.mla_min_age, mla.mla_min_day.Value, mla.mla_max_age.Value, mla.mla_max_day.Value),
                                  condition = mlp.mlp_condition,
                                  sex = mla.mla_sex.ToString(),
                                  summary = mlp.mlp_summary.ToString(),
                                  result_select_language = getLanguage(mlr),
                                  result_eng = mlr.mlr_en_name
                              }).ToList();
                gridConditionT3.DataSource = new SortableBindingList<clGridConditionTab3>(result);
            }
        }
        private string getLanguage(mst_lab_recom mlr)
        {
            if (mlr != null)
            {
                switch (Convert.ToInt32(cbLang.SelectedValue))
                {
                    case 1:
                        return mlr.mlr_th_name;
                    case 2:
                        return mlr.mlr_en_name;
                    case 3:
                        return mlr.mlr_jp_name;
                    case 4:
                        return mlr.mlr_ch_name;
                    case 5:
                        return mlr.mlr_kr_name;
                    case 6:
                        return mlr.mlr_ar_name;
                    case 7:
                        return mlr.mlr_mr_name;
                    case 8:
                        return mlr.mlr_ftxt_name1;
                    case 9:
                        return mlr.mlr_ftxt_name2;
                }
            }
            return null;
        }
        private string getAgeRange(double? agemin, double? daymin, double? agemax, double? daymax)
        {
            string str = string.Empty;
            str = convDoubleToString(agemin) + " (y) ";
            str += convDoubleToString(daymin) + " (d) - ";
            str += convDoubleToString(agemax) + " (y) ";
            str += convDoubleToString(daymax) + " (d)";
            return str;
        }
        #endregion

        private string convDoubleToString(double? num)
        {
            string str = string.Empty;
            if (num != null)
            {
                str = Convert.ToDouble(num).ToString("G");
            }
            return str;
        }
        private System.Nullable<DateTime> convDate(object value)
        {
            if (value != null)
            {
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch
                {

                }
            }
            return null;
        }
        private DateTime convDateNotNull(object value)
        {
            if (value != null)
            {
                try
                {
                    return Convert.ToDateTime(value);
                }
                catch
                {

                }
            }
            return DateTime.Now;
        }
        private System.Nullable<int> convInt(object value)
        {
            if (value != null)
            {
                try
                {
                    return Convert.ToInt32(value);
                }
                catch
                {

                }
            }
            return null;
        }
        private int convIntNotNull(object value)
        {
            if (value != null)
            {
                return Convert.ToInt32(value);
            }
            return 0;
        }
        private string returnNull(string value)
        {
            string reValue = value;
            if (value == "NULL") reValue = null;
            return reValue;
        }
        private float? ParseNfloat(string val)
        {
            float i;
            return float.TryParse(val, out i) ? (float?)i : null;
        }
        private DateTime? convToDate(string dateTime)
        {
            DateTime? temp;
            try
            {
                if (!string.IsNullOrEmpty(dateTime))
                {
                    temp = DateTime.ParseExact(dateTime, "yyyy-MM-dd", new System.Globalization.CultureInfo("en-US"));
                }
                else temp = null;
                //temp = Convert.ToDateTime(dateTime);
            }
            catch
            {
                temp = null;
            }
            return temp;
        }
        private string setNullString(string value)
        {
            return string.IsNullOrEmpty(value) ? null : value;
        }
        private char? conToNChar(string value)
        {
            char? reValue = null;
            try
            {
                reValue = Convert.ToChar(value);
            }
            catch
            {

            }
            return reValue;
        }

        private void btnColor_Click(object sender, EventArgs e)//add suriya 24/06/2015
        {
            ColorDialog col = new ColorDialog();
            DialogResult result = col.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                txtColorCode.Text = col.Color.A.ToString() + "," + col.Color.R.ToString() + "," + col.Color.G.ToString() + "," + col.Color.B.ToString();
            }

        }

        private void txtColorCode_TextChanged(object sender, EventArgs e)//add suriya 24/06/2015
        {
            if (txtColorCode.Text != "")
            {
                string[] arr = txtColorCode.Text.Split(',');
                txtColor.BackColor = Color.FromArgb(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]), int.Parse(arr[3]));

            }
            else
            {
                txtColor.BackColor = Color.White;
            }
        }
    }

    enum modeFrm { able_edit, add_new, unable_edit }
    class clGridlab
    {
        public int? GroupID { get; set; }
        public string GroupItem { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public char? ItemStatus { get; set; }
        public int? mlb_ID { get; set; }
    }
    class clGridCondition
    {
        public int? rowID { get; set; }
        public char? mla_sex { get; set; }
        public double? mla_min_age { get; set; }
        public double? mla_max_age { get; set; }
        public double? mla_min_day { get; set; }
        public double? mla_max_day { get; set; }
        public double? mla_vstand_min { get; set; }
        public double? mla_vstand_max { get; set; }
        public string mla_vstand_nrange { get; set; }
        public string mla_vstand_unit { get; set; }
        public int? mlp_cond_seq { get; set; }
        public string mlp_condition { get; set; }
        public char? mlp_summary { get; set; }
        public char? mlp_status { get; set; }
        public DateTime? mlp_effective_date { get; set; }
        public DateTime? mlp_expire_date { get; set; }
        public int? mla_id { get; set; }
        //public string mlr_th_name { get; set; }
        //public string mlr_en_name { get; set; }
        //public string mlr_jp_name { get; set; }
        //public string mlr_ch_name { get; set; }
        //public string mlr_kr_name { get; set; }
        //public string mlr_ar_name { get; set; }
        //public string mlr_mr_name { get; set; }
        //public string mlr_ftxt_name1 { get; set; }
        //public string mlr_ftxt_name2 { get; set; }
        public int? mlr_id { get; set; }
        public bool Equals(clGridCondition other)
        {
            if (this.rowID != other.rowID) return false;
            if (this.mla_sex != other.mla_sex) return false;
            if (this.rowID != other.rowID) return false;
            if (this.mla_min_age != other.mla_min_age) return false;
            if (this.mla_max_age != other.mla_max_age) return false;
            if (this.mla_min_day != other.mla_min_day) return false;
            if (this.mla_max_day != other.mla_max_day) return false;
            if (this.mla_vstand_min != other.mla_vstand_min) return false;
            if (this.mla_vstand_max != other.mla_vstand_max) return false;
            if (this.mla_vstand_unit != other.mla_vstand_unit) return false;
            if (this.mlp_cond_seq != other.mlp_cond_seq) return false;
            if (this.mlp_condition != other.mlp_condition) return false;
            if (this.mlp_summary != other.mlp_summary) return false;
            if (this.mlp_status != other.mlp_status) return false;
            if (this.mlp_effective_date != other.mlp_effective_date) return false;
            if (this.mlp_expire_date != other.mlp_expire_date) return false;
            if (this.mla_id != other.mla_id) return false;
            if (this.mlr_id != other.mlr_id) return false;
            return true;
        }
    }
    class clGridResult
    {
        public bool result_lab { get; set; }
        public char? mlr_default { get; set; }
        public int? mlr_id { get; set; }
        public char? mlr_summary { get; set; }
        public string mlr_th_name { get; set; }
        public string mlr_en_name { get; set; }
        public string mlr_jp_name { get; set; }
        public string mlr_ch_name { get; set; }
        public string mlr_kr_name { get; set; }
        public string mlr_ar_name { get; set; }
        public string mlr_mr_name { get; set; }
        public string mlr_ftxt_name1 { get; set; }
        public string mlr_ftxt_name2 { get; set; }
    }
    class clGridAge
    {
        public int? mla_id { get; set; }
        public char? mla_sex { get; set; }
        public double? mla_min_age { get; set; }
        public double? mla_max_age { get; set; }
        public double? mla_min_day { get; set; }
        public double? mla_max_day { get; set; }
        public bool age_lab { get; set; }
    }
    class output_result
    {
        public int? mlg_id;
        public int? mlb_id;
        public int? mla_id;
        public int? mlr_id;
    }
    class cbData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    class clGridConditionTab3
    {
        public Image result { get; set; }
        public string mlb_code { get; set; }
        public string mlb_name { get; set; }
        public string sex { get; set; }
        public string rangeAge { get; set; }
        public string condition { get; set; }
        public string summary { get; set; }
        public string result_select_language { get; set; }
        public string result_eng { get; set; }
    }
}
