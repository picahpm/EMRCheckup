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
    public partial class frmMoreDetail_Diagnosis : Form
    {
      
        InhCheckupDataContext dbc = new InhCheckupDataContext();
       public string refhn{get; set;}

        public frmMoreDetail_Diagnosis()
        {
            InitializeComponent();
        }

        private void frmMoreDetail_Diagnosis_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            this.Text = PrePareData.StaticDataCls.ProjectName + "[Diagnosis More Detail]";
            //string id = dbc.trn_patients.Where(c => c.tpt_id == Program.CurrentRegis.tpt_id).Single().tpt_hn_no;
            this.LoadDiagnosisAll(dtpDateS.Value.Date.ToString("yyyy-MM-dd"), dtpDateE.Value.Date.ToString("yyyy-MM-dd"), refhn == null ? dbc.trn_patients.Where(c => c.tpt_id == Program.CurrentRegis.tpt_id).Single().tpt_hn_no : refhn);
        }

        private void LoadDiagnosisAll(string Date_S,string Date_E,string HN_No)
        {
            lblMsgAlert.Text = String.Empty;
            if (Program.CurrentRegis != null || refhn != null)
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    DataTable Dt = ws.GetDiagnosisByDate(HN_No, Date_S, Date_E);

                    if (Dt.Rows.Count == 0) { lblMsgAlert.Text = "No Data."; }
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {

                        GvDiagnosis.Rows.Add(i + 1, String.Format("{0:dd/MM/yyyy}", Dt.Rows[i]["MRDIA_Date"]), Dt.Rows[i]["MRCID_Code"].ToString(), Dt.Rows[i]["MRCID_Desc"].ToString(), Dt.Rows[i]["DTYP_Desc"].ToString(), Dt.Rows[i]["SSUSR_Name"].ToString());
                    }
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                GvDiagnosis.Rows.Clear();

                this.LoadDiagnosisAll(dtpDateS.Value.Date.ToString("yyyy-MM-dd"), dtpDateE.Value.Date.ToString("yyyy-MM-dd"), dbc.trn_patients.Where(c => c.tpt_id == Program.CurrentRegis.tpt_id).Single().tpt_hn_no);
            }
            else if(refhn != String.Empty)
            {
                //Click from Appointment
                GvDiagnosis.Rows.Clear();

                this.LoadDiagnosisAll(dtpDateS.Value.Date.ToString("yyyy-MM-dd"), dtpDateE.Value.Date.ToString("yyyy-MM-dd"), refhn);
            }
        }
    }
}
