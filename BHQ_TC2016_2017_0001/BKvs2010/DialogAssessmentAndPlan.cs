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
    public partial class DialogAssessmentAndPlan : Form
    {
        public DialogAssessmentAndPlan()
        {
            InitializeComponent();
        }

        private InhCheckupDataContext dbc;
        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value == null)
                {
                    btnSave.Enabled = false;
                    this.tabAssessmentAndPlanUC1.PatientRegis = null;
                    _tpr_id = null;
                }
                else
                {
                    try
                    {
                        dbc = new InhCheckupDataContext();
                        trn_patient_regi patient_regis = dbc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                        if (patient_regis == null)
                        {
                            btnSave.Enabled = false;
                            this.tabAssessmentAndPlanUC1.PatientRegis = null;
                            _tpr_id = null;
                        }
                        else
                        {
                            tabAssessmentAndPlanUC1.PatientRegis = patient_regis;
                            _tpr_id = value;
                            btnSave.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSave.Enabled = false;
                        this.tabAssessmentAndPlanUC1.PatientRegis = null;
                        _tpr_id = null;
                        Program.MessageError(this.Name, "tpr_id", ex, false);
                    }
                }
            }
        }

        public string Username
        {
            get { return tabAssessmentAndPlanUC1.Username; }
            set { tabAssessmentAndPlanUC1.Username = value; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tabAssessmentAndPlanUC1.EndEdit();
                try
                {
                    dbc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                    {
                        dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    dbc.SubmitChanges();
                }
                lbAlertMsg.Text = "Save Data Complete.";
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSave_Click", ex, false);
                lbAlertMsg.Text = "โปรดลองอีกครั้ง";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
