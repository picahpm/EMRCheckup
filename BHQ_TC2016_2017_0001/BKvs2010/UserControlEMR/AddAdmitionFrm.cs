using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class AddAdmitionFrm : Form
    {
        public AddAdmitionFrm()
        {
            InitializeComponent();
            GridData.AutoGenerateColumns = false;
            GridData.DataSource = bsAdmition;
        }

        public string username { get; set; }

        private trn_patient _Patient;
        public trn_patient Patient
        {
            get { return _Patient; }
            set
            {
                if (value == null)
                {
                    Clear();
                }
                else
                {
                    try
                    {
                        _Patient = value;
                        bsPatient.DataSource = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "", ex, false);
                        Clear();
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string txtLoc = txtLocation.Text.Trim();
                string txtDes = txtDescription.Text.Trim();
                if (txtLoc.Length == 0)
                {

                }
                else
                {
                    if (txtDes.Length == 0)
                    {

                    }
                    else
                    {
                        try
                        {
                            DateTime chkDate = Convert.ToDateTime(String.Format("{0:yyyy/MM/dd}", dtpdate.Value) + " " + txttime.Text);
                            trn_patient_admit patientSur = new trn_patient_admit
                            {
                                tpd_chk_date = chkDate,
                                tpd_icd10 = txtDes,
                                tpd_location = txtLoc,
                                tpd_send_type = 'C',
                                tpd_create_by = username,
                                tpd_update_by = username
                            };
                            bsAdmition.Insert(0, patientSur);
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnAdd_Click", ex, false);
            }
        }
        private void btnfind_Click(object sender, EventArgs e)
        {
            GridData.ClearSelection();
            bsAdmition.SuspendBinding();
            string txtLoc = txtLocation.Text.Trim();
            string txtDes = txtDescription.Text.Trim();
            DateTime chkDate = Convert.ToDateTime(String.Format("{0:yyyy/MM/dd}", dtpdate.Value) + " " + txttime.Text);
            foreach (DataGridViewRow dvr in GridData.Rows)
            {
                trn_patient_surgery sur = (trn_patient_surgery)dvr.DataBoundItem;
                if (sur.tpg_location.Contains(txtLoc) && sur.tpg_desc.Contains(txtDes) && sur.tpg_chk_date.Value.Date == chkDate.Date)
                {
                    dvr.Visible = true;
                }
                else
                {
                    dvr.Visible = false;
                }
            }
            bsAdmition.ResumeBinding();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            GridData.ClearSelection();
            bsAdmition.SuspendBinding();
            foreach (DataGridViewRow dvr in GridData.Rows)
            {
                dvr.Visible = true;
            }
            bsAdmition.ResumeBinding();
        }

        private void Clear()
        {
            txtDescription.Clear();
            txtLocation.Clear();
            txttime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
        }
    }
}
