using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Linq;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmPE_OutQueue : Form
    {
        public frmPE_OutQueue()
        {
            InitializeComponent();
        }

        List<CheckBox> chkItemList = new List<CheckBox>();
        public int SetTprID { get; set; }
        public string SetEN { get; set; }
        trn_patient_regi CurrentRegis = null;

        List<string> listdel_drug_name = new List<string>();
        List<string> listsummaryresult= new List<string>();
        List<CheckBox> chkselitem = new List<CheckBox>();


        InhCheckupDataContext dbc = new InhCheckupDataContext();
        List<trn_patient_surgery> surgeryList = new List<trn_patient_surgery>();

        private void frmPE_Load(object sender, EventArgs e)
        {
            this.Text = Program.GetRoomName();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            //Load Data
            LoadLeftGrid();
            this.Loadfrm();
            
            SetUIProfileHorizontal1();
            //End Load Data

            frmbg.Close();
        }

        private void SetUIProfileHorizontal1()
        {
            if (GV_Null_Result.RowCount > 0)
            {
                SetTprID = Convert1.ToInt32(GV_Null_Result["Coltprid", 0].Value);
                SetEN = Convert1.ToString(GV_Null_Result["EN", 0].Value);
                CurrentRegis = (from t1 in dbc.trn_patient_regis
                                where t1.tpr_id == SetTprID
                                select t1).FirstOrDefault();
                if (CurrentRegis != null)
                {
                    UIProfileHorizontal1.Loaddata(SetTprID, Program.CurrentSite.mhs_id);
                    //this.LoadData();
                    this.subjectiveUC1.PatientRegis = CurrentRegis;
                    this.objectiveUC1.PatientRegis = CurrentRegis;
                    this.tabAssessmentAndPlanUC1.PatientRegis = CurrentRegis;
                }
            }
        }

        public void Loadfrm()
        {
            if (CurrentRegis!= null)
            {
                //this.LoadData();
                this.subjectiveUC1.PatientRegis = CurrentRegis;
                this.objectiveUC1.PatientRegis = CurrentRegis;
                this.tabAssessmentAndPlanUC1.PatientRegis = CurrentRegis;
            }
        }
        private void LoadLeftGrid()
        {
            //Load left Grid
            var ObjNullResult = (from t1 in dbc.trn_patient_regis
                                 where t1.tpr_pe_status == "NR"
                                  && t1.tpr_pe_doc_code == Program.CurrentUser.mut_username
                                 orderby t1.tpr_arrive_date, t1.trn_patient.tpt_hn_no
                                 select new
                                 {
                                     HN = t1.trn_patient.tpt_hn_no,
                                     EN = t1.tpr_en_no,
                                     FullName = t1.trn_patient.tpt_othername,
                                     ArriveDate = t1.tpr_arrive_date.Value.Date,
                                     tprid = t1.tpr_id
                                 }).ToList();

            GV_Null_Result.DataSource = ObjNullResult;
            if (ObjNullResult != null)
            {
                if (ObjNullResult.Count > 0) Program.CurrentRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == ObjNullResult[0].tprid).FirstOrDefault();
            }
            GV_Null_Result.Columns["Coltprid"].Visible = false;
            lbTitleUI.Text = string.Format("รายชื่อผู้ป่วยที่มีรออ่านผล (ทั้งหมด {0} คน)", ObjNullResult.Count().ToString());
            //End load left grid
        } 
        
        private void Clearfrm()
        {
            dbc.Dispose();
            dbc = new InhCheckupDataContext();

            CurrentRegis = null;
            Program.CurrentPatient_queue = null;

            //new code//
            subjectiveUC1.Clear();
            objectiveUC1.Clear();
            tabAssessmentAndPlanUC1.Clear();

            panel1.Enabled = false;
            lbAlertMsg.Text = String.Empty;
        }  

        private void btnSaveASDraft_Click(object sender, EventArgs e)
        {
            if (Save('D'))
            {
                lbAlertMsg.Text = "Save Data Completed.";
            }
        }                
        
        private void LoadSubjective()
        { 
        
        }
        private void LoadObjective()
        {

        }
        private void LoadAssessmentAndPlan()
        { 
        
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ถ้ามีการเปลี่ยน Tab ที่ไม่ใช่ Index = 0 ให้เซฟเลย เป็น D
            if (CurrentRegis!= null)
            {
                this.Save('D');

                if (tabControl1.SelectedTab.Tag.ToString() == "1")
                {
                    this.LoadSubjective();                    

                }
                else if (tabControl1.SelectedTab.Tag.ToString() == "2")
                {
                    this.LoadObjective();
                }
                else if (tabControl1.SelectedTab.Tag.ToString() == "3")
                {
                    this.LoadAssessmentAndPlan();
                }
            }
        }
        
        private void btnSaveDraft_Click(object sender, EventArgs e)
            {
                if (Save('D'))
                {
                    lbAlertMsg.Text = "Save as Draft Complete";
                    lbAlertMsg.Focus();
                }
            }       

        private void disableBtnWhenSave()
        {
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
        }
        private void enableBtnWhenSave()
        {
            btnSaveDraft.Enabled = true;
            btnSendAuto.Enabled = true;
        }
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            if (this.Save('N'))
            {
                lbAlertMsg.Text = "Save data complete.";
                lbAlertMsg.Focus();
                enableBtnWhenSave();
            }
            else
            {
                enableBtnWhenSave();
            }
        }
        private bool Save(char strType)
        {
            try
            {
                if (strType == 'N')
                {
                    var objQuestP = (from t1 in dbc.trn_ques_patients where t1.tpr_id == SetTprID select new { t1.tqp_type }).FirstOrDefault();
                    if (objQuestP == null || objQuestP.tqp_type == 'D')
                    {
                        //lblnotcc_questionnaire.Visible = true;

                        if (MessageBox.Show("กรุณายืนยันข้อมูลแบบสอบถาม", "Message Alert", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Forms.QuestionnaireFrm fqn = new Forms.QuestionnaireFrm();
                            fqn.tpr_id = Program.CurrentRegis.tpr_id;
                            fqn.ShowDialog();
                            return false;
                        }
                        else
                        {
                            return false;
                        }
                        //return false;
                    }
                }

                try
                {
                    this.subjectiveUC1.EndEdit();
                    this.objectiveUC1.EndEdit();
                    this.tabAssessmentAndPlanUC1.EndEdit();
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

                //select set current dc hdr
                Program.CurrentHDR = (from hdr in dbc.trn_doctor_hdrs where hdr.tpr_id == SetTprID select hdr).FirstOrDefault();
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "Save", ex, false);
                return false;
            }
        }
           
        private void GV_Null_Result_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                    
                dbc.Connection.Close();
                dbc = new InhCheckupDataContext();

                Clearfrm();
                SetTprID = Convert1.ToInt32(GV_Null_Result["Coltprid", e.RowIndex].Value);
                SetEN = Convert1.ToString(GV_Null_Result["EN", e.RowIndex].Value);

                CurrentRegis = (from t1 in dbc.trn_patient_regis
                                where t1.tpr_id == SetTprID
                                select t1).FirstOrDefault();
                Program.CurrentRegis = CurrentRegis;
                if (CurrentRegis != null)
                {
                    UIProfileHorizontal1.Loaddata(SetTprID, Program.CurrentSite.mhs_id);                        
                        
                    //new code//
                    this.subjectiveUC1.PatientRegis = CurrentRegis;
                    this.objectiveUC1.PatientRegis = CurrentRegis;
                    this.tabAssessmentAndPlanUC1.PatientRegis = CurrentRegis;

                    if (tabControl1.SelectedTab.Tag.ToString() != "1") { tabControl1.SelectedIndex = 0; }
                    btnSaveDraft.Enabled = true;
                    btnSendAuto.Enabled = true;
                }
            }
        }

        private void GV_Null_Result_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GV_Null_Result.SetRuningNumber();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoadLeftGrid();
        } 
    }
}
