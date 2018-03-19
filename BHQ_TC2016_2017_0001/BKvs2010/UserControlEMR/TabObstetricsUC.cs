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
    public partial class TabObstetricsUC : UserControl
    {        
        public TabObstetricsUC()
        {
            InitializeComponent();
        }

        private bool _isDoctor = false;
        public bool isDoctor
        {
            get { return _isDoctor; }
            set { _isDoctor = value; }
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
                        obsteticsChiefComplainUC1.isDoctorRoom = _isDoctor;
                        obsteticsChiefComplainUC1.PatientRegis = value;
                        obstetricsPhysicalExamUC1.PatientRegis = value;
                        //obstetricsResultUC1.PatientRegis = value;

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
            obsteticsChiefComplainUC1.Clear();
            obstetricsPhysicalExamUC1.Clear();
            //obstetricsResultUC1.Clear();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                obsteticsChiefComplainUC1.EndEdit();
                obstetricsPhysicalExamUC1.EndEdit();
                //obstetricsResultUC1.EndEdit();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }

        public int? TabSelected { get; set; }
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TabSelected = tabControl1.SelectedIndex;
        }        
    }
}
