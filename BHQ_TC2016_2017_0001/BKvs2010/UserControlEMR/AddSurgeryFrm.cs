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
    public partial class AddSurgeryFrm : Form
    {
        public AddSurgeryFrm()
        {
            InitializeComponent();
            GridData.AutoGenerateColumns = false;
            GridData.DataSource = bsSurgery;
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
                            trn_patient_surgery patientSur = new trn_patient_surgery
                            {
                                tpg_chk_date = chkDate,
                                tpg_desc = txtDes,
                                tpg_location = txtLoc,
                                tpg_send_type = 'C',
                                tpg_create_by = username,
                                tpg_update_by = username
                            };
                            bsSurgery.Insert(0, patientSur);
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
            bsSurgery.SuspendBinding();
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
            bsSurgery.ResumeBinding();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            GridData.ClearSelection();
            bsSurgery.SuspendBinding();
            foreach (DataGridViewRow dvr in GridData.Rows)
            {
                dvr.Visible = true;
            }
            bsSurgery.ResumeBinding();
        }

        private void Clear()
        {
            txtDescription.Clear();
            txtLocation.Clear();
            txttime.Text = String.Format("{0:HH:mm:ss}", DateTime.Now);
        }
    }
}
