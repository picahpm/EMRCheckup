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
    public partial class frmAlertPatient : Form
    {
        public frmAlertPatient()
        {
            InitializeComponent();
        }
        public string SetHNno
        {
            get;
            set;
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void frmAlertPatient_Load(object sender, EventArgs e)
        {// Add Button in Gridview
            DataGridViewButtonColumn doppatientButton = new DataGridViewButtonColumn();
            doppatientButton.HeaderText = "";
            doppatientButton.Name = "ColDelete";
            doppatientButton.UseColumnTextForButtonValue = true;
            doppatientButton.Text = "ลบ";
            doppatientButton.Width = 40;
            GridPatientAlert.Columns.Add(doppatientButton);

            //lbdata.Text = SetPatientAlert;
            var objpatientlist = (from t1 in dbc.trn_patients
                                  where t1.tpt_hn_no == SetHNno
                                  select t1).FirstOrDefault();
            if (objpatientlist != null)
            {
                this.PatientBindingSource.DataSource = objpatientlist;
                
            }
        }

        private void txtPatientAlert_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString().Length == 2)
            {
                GridPatientAlertSearch.Visible = true;
                var objsearchData = (from t1 in dbc.trn_patient_alerts
                                     where t1.tpa_alert.ToString().ToLower().Contains(txtPatientAlert.Text.Trim().ToLower())
                                     orderby t1.tpa_alert
                                     select new { MessageAlert = t1.tpa_alert }).ToList();
                if (objsearchData.Count == 0)
                {
                    GridPatientAlertSearch.Visible = false;
                }
                GridPatientAlertSearch.DataSource = objsearchData.DistinctBy(x => x.MessageAlert).ToList();
            }
            else if (e.KeyValue.ToString().Length == 0)
            {
                GridPatientAlertSearch.Visible = false;
            }
        }

        private void GridPatientAlertSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtPatientAlert.Text = GridPatientAlertSearch[0, e.RowIndex].Value.ToString();
            }
            catch (Exception)
            {
            }
            GridPatientAlertSearch.Visible = false;
        }

        private void txtPatientAlert_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPatientAlert.Text.Length > 1)
            {
                GridPatientAlertSearch.Visible = true;
            }
            else
            {
                GridPatientAlertSearch.Visible = false;
            }
        }

        private void GridPatientAlert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                var listoption = PatientAlertBindingSource.List;
                listoption.RemoveAt(e.RowIndex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PatientAlertBindingSource.EndEdit();
                PatientBindingSource.EndEdit();
                dbc.SubmitChanges();
                this.Close();
            }
            catch (Exception ex)
            {
                Program.MessageError("frmAlertPatient", "btnSave_Click", ex, false);
            }
        }

        private void btnAddPatientAlert_Click(object sender, EventArgs e)
        {
            GridPatientAlertSearch.Visible = false;
            if (txtPatientAlert.Text.Trim().Length == 0)
            {
                return;
            }

            var objregcurrent = (trn_patient)this.PatientBindingSource.Current;
            trn_patient_alert newitem = (trn_patient_alert)this.PatientAlertBindingSource.AddNew();
            newitem.tpt_id = objregcurrent.tpt_id;
            newitem.mut_id = Program.CurrentUser.mut_id;
            newitem.tpa_alert = txtPatientAlert.Text.Trim();
            newitem.tpa_status = 'A';
            newitem.tpa_create_by = Program.CurrentUser.mut_username;
            newitem.tpa_create_date = Program.GetServerDateTime();
            newitem.tpa_update_by = Program.CurrentUser.mut_username;
            newitem.tpa_update_date = newitem.tpa_create_date;
            txtPatientAlert.Text = "";
        }

        private void GridPatientAlert_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < GridPatientAlert.Rows.Count; i++)
            {
                if (GridPatientAlert.Rows[i].Cells["tpa_create_by"].Value != null)
                {
                    string strusername = GridPatientAlert.Rows[i].Cells["tpa_create_by"].Value.ToString();
                    var currentUserupateby = (from t1 in dbc.mst_user_types
                                              where t1.mut_username == strusername
                                              select t1).FirstOrDefault();
                    GridPatientAlert.Rows[i].Cells["Colfullname"].Value = currentUserupateby.mut_fullname;
                }
            }
        }

    }
}
