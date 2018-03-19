using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using BKvs2010.Forms;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class UIMenuBar : UserControl
    {
        public UIMenuBar()
        {
            InitializeComponent();
            try
            {
                testMenuToolStripMenuItem.Visible = Program.CurrentUser == null ? false : Program.CurrentUser.mut_username.StartsWith("TEST");
            }
            catch
            {
                testMenuToolStripMenuItem.Visible = false;
            }
        }

        public string RoomCode = "";
        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("yee");
        }

        private void UIMenuBar_Load(object sender, EventArgs e)
        {
            this.LoadEnableQuestionare();
        }

        public void LoadData()
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                resultToolStripMenuItem.Visible = false;
                var objroom = (from t1 in dbc.mst_room_hdrs
                               where t1.mrm_id == Program.CurrentRoom.mrm_id
                                   && (t1.mrm_code == "PT")
                               select t1).FirstOrDefault();
                if (objroom != null)
                {
                    resultToolStripMenuItem.Visible = true;
                }
            }
        }

        private void resultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis == null)
            {
                //frmObstetricsResult frmresult = new frmObstetricsResult();
                //frmresult.ShowDialog();
            }
            else
            {
                MessageBox.Show("กรุณาส่งคนไข้ออกจาก Station ก่อน", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void LoadEnableCarotidHistory()
        {
            PatientList_Menu.Visible = true;
        }

        #region "<==== NOINA CODING =====>"

        public void LoadEnableQuestionare()
        {
            if (Program.CurrentRegis != null)
            {
                if (Program.CurrentRegis.tpr_patient_type == '2')
                {
                    questionareAvationToolStripMenuItem.Visible = true;
                }
            }
            else
            {
                questionareAvationToolStripMenuItem.Visible = false;
            }
            resultToolStripMenuItem.Visible = false;


            //QuestionareMenu Disable FrmWaitting only 
            questionareToolStripMenuItem.Visible = false;
            if (Program.CurrentFrmShow != 28)
            {
                questionareToolStripMenuItem.Visible = true;
            }
            Program.CurrentFrmShow = 0;
        }

        private void questionareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                Forms.QuestionnaireFrm fqn = new Forms.QuestionnaireFrm(); //Program.CurrentRegis,Program.CurrentUser,Program.CurrentSite
                fqn.tpr_id = Program.CurrentRegis.tpr_id;
                fqn.ShowDialog();
            }
        }

        private void questionareAvationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {

                if (Program.CurrentRegis.tpr_aviation_type == 'N')
                {
                    frmQuestionareAviation_N frm = new frmQuestionareAviation_N();//Program.CurrentRegis, Program.CurrentUser, Program.CurrentSite
                    frm.loadfrm();
                    frm.ShowDialog();

                }
                else if (Program.CurrentRegis.tpr_aviation_type == 'F')
                {
                    frmQuestionareAviation_F frm = new frmQuestionareAviation_F(); //Program.CurrentRegis, Program.CurrentUser, Program.CurrentSite
                    frm.loadfrm();
                    frm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("คุณยังไม่ได้ระบุ Aviation Type", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion

        private void PatientList_Menu_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis == null)
            {
                frmCarotidHistory frmCarHistory = new frmCarotidHistory();
                frmCarHistory.ShowDialog();
            }
            else
            {
                MessageBox.Show("กรุณาส่งคนไข้ออกจาก Station ก่อน", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void viewAllWaitingPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWaiting frmw = new frmWaiting();
            frmw.ShowDialog();
        }

        private void faceSheetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                frmFaceSheet frm = new frmFaceSheet();
                frm.ShowDialog();
            }
        }

        private void changeDoctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RoomCode == "PF")
            {
                frmPFTChgDoc frmpf = new frmPFTChgDoc();
                frmpf.WindowState = FormWindowState.Minimized;
                frmpf.ShowDialog();
            }
            else if (RoomCode == "PT")
            {
                frmObstetricsChgDoc frmOb = new frmObstetricsChgDoc();
                frmOb.WindowState = FormWindowState.Minimized;
                frmOb.ShowDialog();
            }
        }

        public void ShowChangeDoctor(String mrm_code)
        {
            RoomCode = mrm_code;
            changeDoctorToolStripMenuItem.Visible = true;
        }

        private void linkToDocumentViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.OpenUserManual();
        }

        private void dashBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDashBoardcs frm = new frmDashBoardcs();
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }
        public void enableDashBoard()
        {
            dashBoardToolStripMenuItem.Visible = true;
        }

        private void checkup2DoListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.OpenToDoList();
        }

        private void QueuedetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("iexplore.exe", "http://10.88.26.55/CHECKUP_INQUIRY");
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.MessageError("UIMenuBar", "QueuedetailToolStripMenuItem_Click", ex, false);
            }
        }

        private void OccmedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("iexplore.exe", "http://dc-bex-pttep02.bdms.co.th:8000/");
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.MessageError("UIMenuBar", "OccmedToolStripMenuItem_Click", ex, false);
            }
        }

        private void statusLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("iexplore.exe", "http://10.88.26.55/EMRCheckup");
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.MessageError("UIMenuBar", "statusLoginToolStripMenuItem_Click", ex, false);
            }
        }

        private void occMedQuestionnaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                frmQuestionareOccmed fqn = new frmQuestionareOccmed(); //Program.CurrentRegis,Program.CurrentUser,Program.CurrentSite
                //Tranfer Data Database To Questionnaire
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {




                    if (fqn.ShowDialog() == DialogResult.Yes)
                    {
                        //Tranfer Data From Questionnaire To PE
                        //frmPE fpe = (frmPE)frmMDI;
                        //if (fpe != null)
                        //{
                        //    //var objq = (from t1 in dbc.trn_ques_patients where t1.tpr_id == Program.CurrentRegis.tpr_id select t1).FirstOrDefault();
                        //    //if (objq != null)
                        //    //{

                        //    var objQuest = (from t1 in dbc.trn_ques_patients where t1.tpr_id == Program.CurrentRegis.tpr_id select new { t1 }).FirstOrDefault();
                        //    if (objQuest == null) { return; }



                        //}
                    }
                }
            }

        }

        private void PHMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                DialogPHM frmPHM = new DialogPHM();
                frmPHM.tpr_id = Program.CurrentRegis.tpr_id;
                frmPHM.ShowDialog();
            }
        }

        private void testMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
