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

    public partial class frmAviationMedCerReport : Form
    {
        InhCheckupDataContext cdc = new InhCheckupDataContext();

        private int? _tram_id;
        private int? tram_id
        {
            get
            {
                return _tram_id;
            }
            set
            {
                if (value != null)
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }
                _tram_id = value;
            }
        }

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

        public frmAviationMedCerReport()
        {
            InitializeComponent();
        }

        private void frmAviationMedCerReport_Load(object sender, EventArgs e)
        {
            var result = cdc.mst_report_types.Where(x => x.mrt_status == 'A')
                            .Select(x => new
                            {
                                id = x.mrt_id,
                                desc = x.mrt_name
                            }).ToList();
            cmbRptType.DataSource = result;
            cmbRptType.DisplayMember = "desc";
            cmbRptType.ValueMember = "id";
            bsAviaMedCer.DataSource = new trn_report_aviation_medical();
        }

        private void txtPatientName_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            string txtSearch = tb.Text.ToUpper();
            if (ListAutoComplete.Focused != true) ListAutoComplete.Visible = false;
            int tmp_id = cdc.trn_patients.Where(x => (x.tpt_othername).ToUpper() == txtSearch).Select(x => x.tpt_id).FirstOrDefault();
            if (tmp_id != 0) tpt_id = tmp_id;
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
                                        name = x.tpt_othername
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

        private void rdOther_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            if (rd.Checked == true)
            {
                txtAsRemark.Enabled = true;
            }
            else
            {
                txtAsRemark.Enabled = false;
            }
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
                trn_report_aviation_medical tram = (trn_report_aviation_medical)bsAviaMedCer.Current;
                tram.mut_id = mut_id;
                tram.tram_date_print = dateTimePicker1.Value.Date;
                tram.tram_date_exam = dateTimePicker2.Value.Date;
                tram.tram_purpose_physical = Program.GetValueRadioTochar(pnPurpose);
                tram.tram_assessment_aircrew = Program.GetValueRadio(pnAirMedCer);
                tram.tram_assessment_as = Program.GetValueRadio(pnAS);
                tram.tram_ass_as_remark = txtAsRemark.Text;
                tram.tram_update_by = Program.CurrentUser.mut_username;
                tram.tram_update_date = dateNow;
                trn_patient tpt = cdc.trn_patients.Where(x => x.tpt_id == tpt_id).FirstOrDefault();
                if (tram.tram_id == 0)
                {
                    tram.tram_create_by = Program.CurrentUser.mut_username;
                    tram.tram_create_date = dateNow;
                    tpt.trn_report_aviation_medicals.Add(tram);
                }
                else
                {
                    bsAviaMedCer.EndEdit();
                }
                cdc.SubmitChanges();
                tram_id = tram.tram_id;
                btnPrint.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            trn_report_aviation_medical tram = (trn_report_aviation_medical)bsAviaMedCer.Current;
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tram.tram_id, "AV103", Program.CurrentSite.mhs_ename);
        }
    }
}