using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class OffShoreAircrewExamUC : UserControl
    {
        public OffShoreAircrewExamUC()
        {
            InitializeComponent();
        }

        private trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
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
                        trn_doctor_hdr DoctorHdr = value.trn_doctor_hdrs.FirstOrDefault();
                        if (DoctorHdr == null)
                        {
                            DoctorHdr = new trn_doctor_hdr();
                            value.trn_doctor_hdrs.Add(DoctorHdr);
                        }
                        trn_doctor_off_shore DoctorOffShore = DoctorHdr.trn_doctor_off_shores.FirstOrDefault();
                        if (DoctorOffShore == null)
                        {
                            DoctorOffShore = new trn_doctor_off_shore();
                            DoctorHdr.trn_doctor_off_shores.Add(DoctorOffShore);
                        }
                        DoctorOffShore.PropertyChanged -= new PropertyChangedEventHandler(DoctorOffShore_PropertyChanged);
                        DoctorOffShore.PropertyChanged += new PropertyChangedEventHandler(DoctorOffShore_PropertyChanged);
                        _PatientRegis = value;
                        bsPatientRegis.DataSource = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
        }
        private void DoctorOffShore_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tros_particular_id")
            {
                TypeDescriptor.GetProperties(sender)["tros_followup"].SetValue(sender, null);
                var val = TypeDescriptor.GetProperties(sender)["tros_particular_id"].GetValue(sender);
                string particular = val == null ? null : val.ToString();
                if (particular == "FI")
                {
                    txtWeekMonth.Enabled = true;
                }
                else
                {
                    txtWeekMonth.Enabled = false;
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            _PatientRegis = null;
            bsPatientRegis.DataSource = new trn_patient_regi();
        }
        public void EndEdit()
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                
                trn_doctor_off_shore DoctorOffShore = bsDoctorOffShore.OfType<trn_doctor_off_shore>().FirstOrDefault();
                if (DoctorOffShore.tros_create_by == null)
                {
                    DoctorOffShore.tros_create_by = user_name;
                    DoctorOffShore.tros_create_date = dateNow;
                }
                DoctorOffShore.tros_update_by = user_name;
                DoctorOffShore.tros_update_date = dateNow;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
