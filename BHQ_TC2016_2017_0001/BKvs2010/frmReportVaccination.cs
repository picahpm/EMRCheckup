using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DBCheckup;

namespace BKvs2010
{

    public partial class frmReportVaccination : Form
    {
        InhCheckupDataContext cdc = new InhCheckupDataContext();

        private int? _tpt_id;
        private int? tpt_id
        {
            get { return _tpt_id; }
            set
            {
                if (value != null)
                {
                    string name = cdc.trn_patients.Where(x => x.tpt_id == value)
                                     .Select(x => x.tpt_othername)
                                     .FirstOrDefault();
                    txtPatientName.Text = name;
                    txtPatientName.SelectionStart = 0;
                    txtPatientName.SelectionLength = txtPatientName.Text.Length;
                }
                else
                {
                    txtPatientName.Text = string.Empty;
                }
                _tpt_id = value;
            }
        }

        private int? _mut_id;
        private int? mut_id
        {
            get { return _mut_id; }
            set
            {
                if (value != null)
                {
                    string name = cdc.mst_user_types.Where(x => x.mut_id == value)
                                     .Select(x => x.mut_fullname)
                                     .FirstOrDefault();
                    txtDoctor.Text = name;
                    txtDoctor.SelectionStart = 0;
                    txtDoctor.SelectionLength = txtDoctor.Text.Length;
                }
                else
                {
                    txtDoctor.Text = string.Empty;
                }
                _mut_id = value;
            }
        }

        public frmReportVaccination()
        {
            InitializeComponent();
            gridDtl.AutoGenerateColumns = false;
            bsVaccineHdr.AddNew();
            addDataCombobox();
            //tpt_id = null;
        }

        private void addDataCombobox()
        {
            addType();
            addTime();
        }

        private class data
        {
            public string val { get; set; }
            public string display { get; set; }
        }

        List<data> mstTime = new List<data>()
        {
            new data { val = null, display = "" },
            new data { val = "BO", display = "Booster : date" },
            new data { val = "IV", display = "Initial Vac :" },
            new data { val = "2V", display = "2nd Vac : date" },
            new data { val = "3V", display = "3rd Vac : date" }
        };

        List<data> mstType = new List<data>()
        {
            new data { val = null, display = "" },
            new data { val = "HA", display = "Hepatitis A" },
            new data { val = "HB", display = "Hepatitis B" },
            new data { val = "VA", display = "Varicella" },
            new data { val = "ME", display = "Measles" }
        };

        private void addType()
        {
            cbType.ValueMember = "val";
            cbType.DisplayMember = "display";
            cbType.DataSource = mstType;
        }

        private void addTime()
        {
            cbTime.ValueMember = "val";
            cbTime.DisplayMember = "display";
            cbTime.DataSource = mstTime;
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = ((CheckBox)sender);
            panel1.Enabled = cb.Checked;
            trn_report_vaccine_dtl dtl = (trn_report_vaccine_dtl)bsVaccineDtl.Current;
            if (dtl != null)
            {
                dtl.trvd_flag_show_report = ((CheckBox)sender).Checked;
            }
            if (cb.Checked == false)
            {
                dtl.trvd_time = null;
                dtl.trvd_date_on = null;
            }
            else
            {
                dtl.trvd_date_on = dpDateOn.Value;
            }
        }

        private void panel1_EnabledChanged(object sender, EventArgs e)
        {
            if (!((Panel)sender).Enabled)
            {
                cbTime.SelectedItem = "";
                dpDateOn.Value = DateTime.Now;
            }
        }

        private void chkSero_CheckedChanged(object sender, EventArgs e)
        {
            txtSerology.Enabled = chkSero.Checked;
            trn_report_vaccine_dtl dtl = (trn_report_vaccine_dtl)bsVaccineDtl.Current;
            if (dtl != null)
            {
                dtl.trvd_chk_serology = ((CheckBox)sender).Checked;
            }
            if (!chkSero.Checked)
            {
                dtl.trvd_serology = "";
            }
        }

        private void chkVaccine_CheckedChanged(object sender, EventArgs e)
        {
            txtVaccination.Enabled = chkVaccine.Checked;
            trn_report_vaccine_dtl dtl = (trn_report_vaccine_dtl)bsVaccineDtl.Current;
            dtl.trvd_chk_vaccination_rec = ((CheckBox)sender).Checked;
            if (!chkVaccine.Checked)
            {
                dtl.trvd_vaccination_record = "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            trn_report_vaccine_dtl dtl = new trn_report_vaccine_dtl();
            dtl.trvd_chk_serology = true;
            dtl.trvd_chk_vaccination_rec = true;
            dtl.trvd_flag_attached = true;
            dtl.trvd_flag_show_report = true;
            dtl.trvd_date_on = DateTime.Now.Date;
            bsVaccineDtl.Add(dtl);
            bsVaccineDtl.MoveLast();
        }

        private void chkSee_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            trn_report_vaccine_dtl dtl = (trn_report_vaccine_dtl)bsVaccineDtl.Current;
            if (dtl != null)
            {
                dtl.trvd_flag_attached = cb.Checked;
            }
        }

        private void bsVaccineDtl_CurrentChanged(object sender, EventArgs e)
        {
            trn_report_vaccine_dtl dtl = (trn_report_vaccine_dtl)bsVaccineDtl.Current;
            if (dtl != null)
            {
                pnDetail.Enabled = true;
                chkSero.Checked = dtl.trvd_chk_serology == true ? true : false;
                chkVaccine.Checked = dtl.trvd_chk_vaccination_rec == true ? true : false;
                chkSee.Checked = dtl.trvd_flag_attached == true ? true : false;
                chkShow.Checked = dtl.trvd_flag_show_report == true ? true : false;
                dpDateOn.Value = dtl.trvd_date_on == null ? DateTime.Now : dtl.trvd_date_on.Value.Date;
                btnDelete.Enabled = true;
            }
            else
            {
                pnDetail.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void dpDateOn_ValueChanged(object sender, EventArgs e)
        {
            trn_report_vaccine_dtl dtl = (trn_report_vaccine_dtl)bsVaccineDtl.Current;
            if (dtl != null)
            {
                dtl.trvd_date_on = ((DateTimePicker)sender).Value;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            trn_report_vaccine_dtl dtl = (trn_report_vaccine_dtl)bsVaccineDtl.Current;
            bsVaccineDtl.Remove(dtl);
        }

        private void btnClareAll_Click(object sender, EventArgs e)
        {
            bsVaccineDtl.Clear();
        }

        private void txtPatientName_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string txtSearch = tb.Text.ToUpper();
            if (ListAutoComplete.Focused != true) ListAutoComplete.Visible = false;
            int tmp_id = cdc.trn_patients.Where(x => (x.tpt_othername).ToUpper() == txtSearch).Select(x => x.tpt_id).FirstOrDefault();
            if (tmp_id != 0) tpt_id = tmp_id;
            //tpt_id = cdc.trn_patients.Where(x => (x.tpt_pre_name + x.tpt_first_name + " " + x.tpt_last_name).ToUpper() == txtSearch).Select(x => x.tpt_id).FirstOrDefault();
        }

        private void txtPatientName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = false;
                if (ListAutoComplete.Items.Count > 0)
                {
                    tpt_id = (int)ListAutoComplete.SelectedValue;
                }
                ListAutoComplete.Visible = false;
                txtPatientName.Focus();
            }
            else if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
            {
                if (txtPatientName.Text.Length >= 2)
                {
                    if (ListAutoComplete.Visible == false)
                    {
                        ListAutoComplete.Visible = true;
                    }

                    TextBox tb = (TextBox)sender;
                    string txtSearch = tb.Text.ToUpper();
                    var result = cdc.trn_patients.Where(x => x.tpt_othername.ToUpper().Contains(txtSearch))
                                    .OrderBy(x => x.tpt_othername)
                                    .Select(x => new
                                    {
                                        id = x.tpt_id,
                                        name = x.tpt_pre_name + x.tpt_first_name + " " + x.tpt_last_name
                                    }).ToList();
                    ListAutoComplete.DataSource = result;
                    ListAutoComplete.DisplayMember = "name";
                    ListAutoComplete.ValueMember = "id";
                }
                else if (txtPatientName.Text.Length < 2)
                {
                    ListAutoComplete.Visible = false;
                }
            }
        }

        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                if (ListAutoComplete.Items.Count > 0)
                {
                    if (ListAutoComplete.SelectedIndex < ListAutoComplete.Items.Count - 1)
                    {
                        ListAutoComplete.SelectedIndex = ListAutoComplete.SelectedIndex + 1;
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                if (ListAutoComplete.Items.Count > 0)
                {
                    if (ListAutoComplete.SelectedIndex > 0)
                    {
                        ListAutoComplete.SelectedIndex = ListAutoComplete.SelectedIndex - 1;
                    }
                }
            }
        }

        private void ListAutoComplete_Click(object sender, EventArgs e)
        {
            if (ListAutoComplete.Items.Count > 0)
            {
                tpt_id = (int)ListAutoComplete.SelectedValue;
            }
            txtPatientName.Focus();
            ListAutoComplete.Visible = false;
        }

        private void ListAutoComplete_MouseMove(object sender, MouseEventArgs e)
        {
            ListAutoComplete.SelectedIndex = ListAutoComplete.IndexFromPoint(e.X, e.Y);
        }

        private void txtDoctor_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string txtSearch = tb.Text.ToUpper();
            if (lbAutoCompleteDoctor.Focused != true) lbAutoCompleteDoctor.Visible = false;
            int tmp_id = cdc.mst_user_types.Where(x => x.mut_fullname.ToUpper() == txtSearch).Select(x => x.mut_id).FirstOrDefault();
            if (tmp_id != 0) mut_id = tmp_id;
            //mut_id = cdc.mst_user_types.Where(x => x.mut_fullname.ToUpper() == txtSearch).Select(x => x.mut_id).FirstOrDefault();
        }

        private void txtDoctor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                e.SuppressKeyPress = false;
                if (lbAutoCompleteDoctor.Items.Count > 0)
                {
                    mut_id = (int)lbAutoCompleteDoctor.SelectedValue;
                }
                lbAutoCompleteDoctor.Visible = false;
                txtDoctor.Focus();
            }
            else if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
            {
                if (txtDoctor.Text.Length >= 2)
                {
                    if (lbAutoCompleteDoctor.Visible == false)
                    {
                        lbAutoCompleteDoctor.Visible = true;
                    }

                    TextBox tb = (TextBox)sender;
                    string txtSearch = tb.Text.ToUpper();
                    var result = cdc.mst_user_types.Where(x => x.mut_fullname.Contains(txtSearch))
                                    .OrderBy(x => x.mut_t_fname)
                                    .Select(x => new
                                    {
                                        id = x.mut_id,
                                        name = x.mut_fullname
                                    }).ToList();
                    lbAutoCompleteDoctor.DataSource = result;
                    lbAutoCompleteDoctor.DisplayMember = "name";
                    lbAutoCompleteDoctor.ValueMember = "id";
                }
                else if (txtDoctor.Text.Length < 2)
                {
                    lbAutoCompleteDoctor.Visible = false;
                }
            }
        }

        private void txtDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                if (lbAutoCompleteDoctor.Items.Count > 0)
                {
                    if (lbAutoCompleteDoctor.SelectedIndex < lbAutoCompleteDoctor.Items.Count - 1)
                    {
                        lbAutoCompleteDoctor.SelectedIndex = lbAutoCompleteDoctor.SelectedIndex + 1;
                    }
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                if (lbAutoCompleteDoctor.Items.Count > 0)
                {
                    if (lbAutoCompleteDoctor.SelectedIndex > 0)
                    {
                        lbAutoCompleteDoctor.SelectedIndex = lbAutoCompleteDoctor.SelectedIndex - 1;
                    }
                }
            }
        }

        private void lbAutoCompleteDoctor_Click(object sender, EventArgs e)
        {
            if (lbAutoCompleteDoctor.Items.Count > 0)
            {
                mut_id = (int)lbAutoCompleteDoctor.SelectedValue;
            }
            txtDoctor.Focus();
            lbAutoCompleteDoctor.Visible = false;
        }

        private void lbAutoCompleteDoctor_MouseMove(object sender, MouseEventArgs e)
        {
            lbAutoCompleteDoctor.SelectedIndex = lbAutoCompleteDoctor.IndexFromPoint(e.X, e.Y);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tpt_id == null)
            {
                MessageBox.Show("Please Select Patient.");
                txtPatientName.Focus();
            }
            else if (mut_id == null)
            {
                MessageBox.Show("Please Select Doctor.");
                txtDoctor.Focus();
            }
            else
            {
                DateTime dateNow = Program.GetServerDateTime();
                trn_patient tpt = cdc.trn_patients.Where(x => x.tpt_id == tpt_id).FirstOrDefault();
                trn_report_vaccine_hdr trvh = (trn_report_vaccine_hdr)bsVaccineHdr.Current;
                trvh.mut_id = mut_id;
                trvh.mhs_id = Program.CurrentSite.mhs_id;
                trvh.trvh_update_by = Program.CurrentUser.mut_username;
                trvh.trvh_update_date = dateNow;
                if (trvh.trvh_id == 0)
                {
                    tpt.trn_report_vaccine_hdrs.Add(trvh);
                    trvh.trvh_create_by = Program.CurrentUser.mut_username;
                    trvh.trvh_create_date = dateNow;
                }
                else
                {
                    bsVaccineHdr.EndEdit();
                }
                cdc.SubmitChanges();
                // แก้ bug colTimeName หาย หลังจาก submit
                foreach (DataGridViewRow row in gridDtl.Rows)
                {
                    string val = "";
                    if (row.Cells["colTime"].Value != null) val = row.Cells["colTime"].Value.ToString();
                    row.Cells["colTimeName"].Value = mstTime.Where(x => x.val == val).Select(x => x.display).FirstOrDefault();
                }
                //
                btnPrint.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            trn_report_vaccine_hdr trvh = (trn_report_vaccine_hdr)bsVaccineHdr.Current;
            Report.frmPreviewReport frm = new Report.frmPreviewReport(trvh.trvh_id, "AV104", Program.CurrentSite.mhs_ename);
            //frm.previewReport();
        }

        private void gridDtl_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (gridDtl.Columns[e.ColumnIndex].Name == "colTime")
            {
                var result = gridDtl[e.ColumnIndex, e.RowIndex].Value;
                string val;
                if (result == null)
                {
                    val = null;
                }
                else
                {
                    val = result.ToString();
                }
                string display = mstType.Where(x => x.val == val).Select(x => x.display).FirstOrDefault();
                gridDtl.Rows[e.RowIndex].Cells["colTimeName"].Value = display;
            }
        }

        private void cbTime_Validated(object sender, EventArgs e)
        {
            if (gridDtl.SelectedRows.Count > 0)
            {
                int rowIndex = gridDtl.SelectedRows[0].Index;
                string val = (string)cbTime.SelectedValue;
                string display = mstTime.Where(x => x.val == val).Select(x => x.display).FirstOrDefault();
                gridDtl.Rows[rowIndex].Cells["colTimeName"].Value = display;
            }
        }
    }
}
