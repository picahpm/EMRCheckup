using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.Usercontrols;

namespace BKvs2010.UserControlEMR
{
    public partial class TabObstetricsCKUC : UserControl
    {
        public TabObstetricsCKUC()
        {
            InitializeComponent();           
        }
       
        private bool _isDoctor = false;
        public bool isDoctor
        {
            get { return _isDoctor; }
            set { _isDoctor = value; }
        }

        private mst_user_type _user;
        public mst_user_type user
        {
            get { return _user; }
            set
            {
                if (value != user)
                {
                    obstetricsResultUC1.user = value;
                    _user = value;
                }
            }
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
                        obstetricsResultUC1.PatientRegis = value;

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
            obstetricsResultUC1.Clear();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                obsteticsChiefComplainUC1.EndEdit();
                obstetricsPhysicalExamUC1.EndEdit();
                obstetricsResultUC1.EndEdit();
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            TabSelected = tabControl1.SelectedIndex;
       
        }
        private void btnPatho_Click(object sender, EventArgs e)
        {
            using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
            {
                ws.InsertDBEmrCheckupResultXray(_PatientRegis.trn_patient.tpt_hn_no, _PatientRegis.tpr_en_no, DateTime.Now.AddYears(-5), DateTime.Now, false);
            }
            using (InhCheckupDataContext contxt = new InhCheckupDataContext())
            {
                string pathPatho = contxt.trn_patient_history_pathos.Where(x => x.tpt_id == _PatientRegis.tpt_id && x.tphp_en_no == _PatientRegis.tpr_en_no).Select(x => x.tphp_link).FirstOrDefault();
                if (string.IsNullOrEmpty(pathPatho))
                {
                    MessageBox.Show("Pathology's result is not found.", "Pathology.");
                }
                else
                {
                    System.Diagnostics.Process.Start("IExplore.exe", pathPatho);
                }
            }
        }
    }
}
