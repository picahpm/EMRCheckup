using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class NewPatientProfileLandscape : UserControl
    {
        public NewPatientProfileLandscape()
        {
            InitializeComponent();
        }

        private trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                if (value != _PatientRegis)
                {
                    if (value == null)
                    {
                        ImagePatient.Image = null;
                        dataFullName.Text = "";
                        dataHN.Text = "";
                        dataEN.Text = "";
                        dataDOB.Text = "";
                        dataGender.Text = "";
                        dataNationality.Text = "";
                        dataAge.Text = "";
                        datavisiteDate.Text = "";
                        lblvisittime.Text = "";
                        dataAllergyLebel.Text = "";
                    }
                    else
                    {
                        ImagePatient.Image = Program.byteArrayToImage(value.trn_patient.tpt_image);
                        dataFullName.Text = value.trn_patient.tpt_othername;
                        dataHN.Text = value.trn_patient.tpt_hn_no;
                        dataEN.Text = value.tpr_en_no;
                        dataDOB.Text = value.trn_patient.tpt_dob_text;
                        dataGender.Text = value.trn_patient.tpt_gender == null ? "N/A" : value.trn_patient.tpt_gender == 'F' ? "Female" : "Male";
                        dataNationality.Text = value.trn_patient.tpt_nation_code;
                        dataAge.Text = Program.CalculateAge(value.trn_patient.tpt_dob.Value, value.trn_patient_regis_detail.tpr_real_arrived_date.Value);
                        datavisiteDate.Text = value.trn_patient_regis_detail.tpr_real_arrived_date.Value.ToString("dd/MM/yyyy");
                        lblvisittime.Text = value.trn_patient_regis_detail.tpr_real_arrived_date.Value.ToString("HH:mm:ss");
                        dataAllergyLebel.Text = string.IsNullOrEmpty(value.trn_patient.tpt_allergy) ? "No Allergy." : value.trn_patient.tpt_allergy;
                    }
                    _PatientRegis = value;
                }
            }
        }
    }
}
