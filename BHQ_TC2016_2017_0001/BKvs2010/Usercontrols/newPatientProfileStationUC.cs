using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class newPatientProfileStationUC : UserControl
    {
        public newPatientProfileStationUC()
        {
            InitializeComponent();
            foreach (Binding bindImage in pictureBox1.DataBindings)
            {
                if (bindImage.PropertyName == "Image")
                {
                    bindImage.Format += new ConvertEventHandler(bindImage_Format);
                }
            }
            foreach (Binding bindGender in dataGender.DataBindings)
            {
                if (bindGender.PropertyName == "Text")
                {
                    bindGender.Format += new ConvertEventHandler(bindGender_Format);
                }
            }
            foreach (Binding bindSite in lblsite.DataBindings)
            {
                if (bindSite.PropertyName == "Text")
                {
                    bindSite.Format += new ConvertEventHandler(bindSite_Format);
                }
            }
        }
        private void bindImage_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.GetType() == typeof(System.Byte[]))
                {
                    System.Byte[] val = (System.Byte[])e.Value;
                    MemoryStream ms = new MemoryStream(val);
                    e.Value = Image.FromStream(ms);
                }
            }
        }
        private void bindGender_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.GetType() == typeof(char))
                {
                    char val = (char)e.Value;
                    if (val == 'M')
                    {
                        e.Value = "ชาย(Male)";
                    }
                    else if (val == 'F')
                    {
                        e.Value = "หญิง(Female)";
                    }
                }
            }
        }
        private void bindSite_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
            {
                if (e.Value.GetType() == typeof(int))
                {
                    int val = (int)e.Value;
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            mst_hpc_site HpcSite = cdc.mst_hpc_sites.Where(x => x.mhs_id == val).FirstOrDefault();
                            if (HpcSite != null)
                            {
                                e.Value = HpcSite.mhs_ename;
                            }
                            else
                            {
                                e.Value = "";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "bindSite_Format", ex, false);
                    }
                }
            }
        }

        private string hn = null;
        private int? _tpr_id = null;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value == null)
                {
                    this.Clear();
                    hn = null;
                    _tpr_id = null;
                }
                else
                {
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                            if (PatientRegis == null)
                            {
                                this.Clear();
                                hn = null;
                                _tpr_id = null;
                                btnPatientAlertView.Visible = false;
                            }
                            else
                            {
                                trn_patient Patient = new trn_patient();
                                Patient = PatientRegis.trn_patient;
                                btnPatientAlertView.Enabled = Patient.trn_patient_alerts.Count() > 0;
                                dataAge.Text = Program.CalculateAge((DateTime)Patient.tpt_dob, (DateTime)PatientRegis.tpr_arrive_date);
                                hn = Patient.tpt_hn_no;
                                this.bsPatient.DataSource = Patient;
                                this.bsPatientRegis.DataSource = PatientRegis;
                                _tpr_id = value;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "tpt_id", ex, false);
                        _tpr_id = null;
                    }
                }
            }
        }

        private void btnPatientAlertView_Click(object sender, EventArgs e)
        {
            frmAlertPatient frm = new frmAlertPatient();
            frm.SetHNno = hn;
            frm.ShowDialog();
        }

        private void lbRetrieve_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (_tpr_id != null)
            {
                bool getInfo = new APITrakcare.GetPatientInfoCls().GetInfo((int)_tpr_id);
                if (getInfo)
                {
                    tpr_id = _tpr_id;
                }
            }
            this.Cursor = Cursors.Default;
        }
        public void Clear()
        {
            dataAge.Text = "";
            bsPatient.DataSource = new trn_patient();
            bsPatientRegis.DataSource = new trn_patient_regi();
        }
    }
}
