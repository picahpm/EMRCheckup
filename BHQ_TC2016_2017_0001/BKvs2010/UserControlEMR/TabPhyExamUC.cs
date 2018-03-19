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
    public partial class TabPhyExamUC : UserControl
    {
        public TabPhyExamUC()
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
                        examCheckupUC1.PatientRegis = value;
                        examCheckupUC1.Username = Program.CurrentUser.mut_username;
                        aviationExamUC1.PatientRegis = value;
                        eyeExamAirCrewFAA1.PatientRegis = value;
                        eyeExamAirCrewCan1.PatientRegis = value;
                        eyeExamAircrewAus1.PatientRegis = value;
                        offShoreAircrewExamUC1.PatientRegis = value;
                        examOccMedUC1.PatientRegis = value;
                        fitToFlyAircrewExamUC1.PatientRegis = value;
                        _PatientRegis = value;
                        this.Enabled = true;
                        
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "setProp tpr_patient_regis", ex, false);
                    }
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            examCheckupUC1.Clear();
            aviationExamUC1.Clear();
            eyeExamAirCrewFAA1.Clear();
            eyeExamAirCrewCan1.Clear();
            eyeExamAircrewAus1.Clear();
            offShoreAircrewExamUC1.Clear();
            examOccMedUC1.Clear();
            fitToFlyAircrewExamUC1.Clear();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                examCheckupUC1.EndEdit();
                aviationExamUC1.EndEdit();
                eyeExamAirCrewFAA1.EndEdit();
                eyeExamAirCrewCan1.EndEdit();
                eyeExamAircrewAus1.EndEdit();
                offShoreAircrewExamUC1.EndEdit();
                examOccMedUC1.EndEdit();
                fitToFlyAircrewExamUC1.EndEdit();

                
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
         //   TabSelected = tabControl1.SelectedIndex.ToString();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TabSelected = tabControl1.SelectedIndex;
        }


        public int? TabSelected { get; set; }
        //DialogPhysicalExam dialogPhyEx = new DialogPhysicalExam();
        public bool IsENGPrt { get; set; }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) {IsENGPrt = false;  }        
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) { IsENGPrt = true; }
        }
    }
}
