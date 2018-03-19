using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace CheckupBO
{
    public partial class ManagePlanFrm : Form
    {
        public ManagePlanFrm()
        {
            InitializeComponent();
            bsPatient.CurrentChanged += new EventHandler(bsPatient_CurrentChanged);
            RefreshPatient(string.Empty);
            bsPatient.DataSource = patient;
            gridPatient.DataSource = bsPatient;
        }

        private BindingSource bsPatient = new BindingSource();
        private BindingList<Patients> patient = new BindingList<Patients>();

        private void RefreshPatient(string txtSearch)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                List<Patients> tpt = cdc.trn_patient_regis
                                        .Where(x => x.tpr_arrive_date.Value.Date == dateNow.Date &&
                                                    (x.trn_patient.tpt_othername.Contains(string.IsNullOrEmpty(txtSearch) ? string.Empty : txtSearch) ||
                                                     x.trn_patient.tpt_hn_no.Contains(string.IsNullOrEmpty(txtSearch) ? string.Empty : txtSearch) ||
                                                     x.tpr_en_no.Contains(string.IsNullOrEmpty(txtSearch) ? string.Empty : txtSearch)))
                                        .Select(x => new Patients
                                        {
                                            tpr_id = x.tpr_id,
                                            hn_no = x.trn_patient.tpt_hn_no,
                                            en_no = x.tpr_en_no,
                                            fullName = x.trn_patient.tpt_othername
                                        }).ToList();
                patient = new BindingList<Patients>(tpt);
                bsPatient.DataSource = patient;
                gridPatient.DataSource = bsPatient;
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            RefreshPatient(txtSearch.Text);
        }

        public void bsPatient_CurrentChanged(object sender, EventArgs e)
        {
            if (((BindingSource)sender).Count == 0) return;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                int tpr_id = ((Patients)((BindingSource)sender).Current).tpr_id;
                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                switch (tpr.tpr_patient_type)
                {
                    case '1':
                        radioButton1.Checked = true;
                        break;
                    case '2':
                        radioButton2.Checked = true;
                        break;
                    case '3':
                        radioButton3.Checked = true;
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (bsPatient.Count == 0) return;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                int tpr_id = ((Patients)bsPatient.Current).tpr_id;
                trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault(); 
                if (radioButton1.Checked)
                {
                    tpr.tpr_patient_type = '1';
                }
                else if (radioButton2.Checked)
                {
                    tpr.tpr_patient_type = '2';
                }
                else
                {
                    tpr.tpr_patient_type = '3';
                }
                cdc.SubmitChanges();
            }
        }
    }
}
