using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using DBCheckup;

namespace BKvs2010.Forms
{
    public partial class PatternCheckupFrm : Form
    {
        public PatternCheckupFrm()
        {
            InitializeComponent();
        }

        protected InhCheckupDataContext cdc;

        private int? _tpr_id = null;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value != _tpr_id)
                {
                    if (value == null)
                    {
                        Program.CurrentRegis = null;
                        Program.CurrentPatient_queue = null;
                    }
                    _tpr_id = value;
                    OnTpridChanged(value, null);
                }
            }
        }

        private int? _tps_id = null;
        public int? tps_id
        {
            get
            {
                return _tps_id;
            }
            set
            {
                if (_tps_id != value)
                {
                    _tps_id = value;
                }
            }
        }

        private int? _queue_mvt_id = null;
        public int? queue_mvt_id
        {
            get
            {
                return _queue_mvt_id;
            }
            set
            {
                if (_queue_mvt_id != value)
                {
                    _queue_mvt_id = value;
                }
            }
        }

        private int? _mvt_id = null;
        public int? mvt_id
        {
            get { return _mvt_id; }
            set
            {
                if (_mvt_id != value)
                {
                    _mvt_id = value;
                    OnMvtidChanged(value);
                }
            }
        }
        public string username { get; set; }

        public enum tpr_idStatus
        {
            NSWR,
            WK,
            Nothing
        }
        public enum formStatus
        {
            isFooter,
            isStation
        }

        private formStatus _FormStatus = formStatus.isFooter;

        public formStatus FormStatus
        {
            get { return _FormStatus; }
            set
            {
                if (value != _FormStatus)
                {
                    _FormStatus = value;
                }
            }
        }

        public mst_user_type user { get; set; }

        private log_user_login _userLogin;
        public log_user_login userLogin
        {
            get { return _userLogin; }
            set
            {
                if (value != _userLogin)
                {
                    if (value == null)
                    {

                    }
                    else
                    {

                    }
                    _userLogin = value;
                }
            }
        }
        private void CheckKickedUser()
        {
            try
            {
                using (InhCheckupDataContext context = new InhCheckupDataContext())
                {
                    log_user_login lug = context.log_user_logins
                                                .Where(x => x.lug_id == _lug_id)
                                                .FirstOrDefault();
                    if (lug.lug_end_date != null)
                    {
                        this.Close();
                        OnKickedUser(false);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "CheckKickedUser", ex, false);
            }
            Timer timerCheckKickUser = new Timer();
            timerCheckKickUser.Enabled = true;
            timerCheckKickUser.Interval = 5000;
            timerCheckKickUser.Tick += new EventHandler(timerCheckKickUser_Tick);
            timerCheckKickUser.Start();
        }

        private void timerCheckKickUser_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            timer.Dispose();
            if (!this.IsDisposed && _lug_id != null)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(CheckKickedUser));
                thread.IsBackground = true;
                thread.Start();
            }
        }



        public int? mut_id { get; set; }
        private int? _lug_id = null;
        public int? lug_id
        {
            get { return _lug_id; }
            set
            {
                if (_lug_id != value)
                {
                    _lug_id = value;
                    if (value != null)
                    {
                        CheckKickedUser();
                    }
                }
            }
        }

        public event TpridChanged tpridChanged;
        public delegate void TpridChanged(object sender, int? e, tpr_idStatus? status);
        private void OnTpridChanged(int? e, tpr_idStatus? status)
        {
            if (tpridChanged == null) return;
            tpridChanged(this, e, status);
        }

        private int? _mrd_id = null;
        public int? mrd_id
        {
            get { return _mrd_id; }
            set
            {
                if (value != _mrd_id)
                {
                    if (value != null)
                    {
                        if (this.FormStatus == formStatus.isStation)
                        {
                            using (InhCheckupDataContext context = new InhCheckupDataContext())
                            {
                                DateTime dateNow = Program.GetServerDateTime();
                                trn_patient_queue PatientQueue = context.trn_patient_queues
                                                                        .Where(x => x.mrd_id == value &&
                                                                                    x.trn_patient_regi.tpr_arrive_date.Value.Date == dateNow.Date &&
                                                                                    ((x.tps_status == "NS" && x.tps_ns_status == "WR") || (x.tps_status == "WK")))
                                                                        .FirstOrDefault();

                                if (PatientQueue != null)
                                {
                                    this._tpr_id = PatientQueue.tpr_id;
                                    this._tps_id = PatientQueue.tps_id;

                                    Program.CurrentRegis = PatientQueue.trn_patient_regi;
                                    Program.CurrentPatient_queue = PatientQueue;
                                    if (PatientQueue.tps_status == "NS" && PatientQueue.tps_ns_status == "WR")
                                    {
                                        OnTpridChanged(PatientQueue.tpr_id, tpr_idStatus.NSWR);
                                    }
                                    else
                                    {
                                        OnTpridChanged(PatientQueue.tpr_id, tpr_idStatus.WK);
                                    }
                                }
                                else
                                {
                                    this._tps_id = null;
                                    this._queue_mvt_id = null;
                                    OnTpridChanged(null, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        _FormStatus = formStatus.isFooter;
                    }
                    _mrd_id = value;
                    OnmrdidChanged(value);
                }
            }
        }

        public event MrdidChanged mrdidChanged;
        public delegate void MrdidChanged(object sender, int? e);
        private void OnmrdidChanged(int? e)
        {
            if (mrdidChanged == null) return;
            mrdidChanged(this, e);
        }

        public event MvtidChanged mvtidChanged;
        public delegate void MvtidChanged(object sender, int? e);
        private void OnMvtidChanged(int? e)
        {
            if (mvtidChanged == null) return;
            mvtidChanged(this, e);
        }

        public event KickedUser kickedUser;
        public delegate void KickedUser(object sender, bool e);
        private void OnKickedUser(bool e)
        {
            if (kickedUser == null) return;
            kickedUser(this, e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.Dispose();
        }

        protected MenuStrip menuStrip1;
        protected ToolStripMenuItem todolistMnu;
        protected ToolStripMenuItem qustionnareToolStripMenuItem;
        protected ToolStripMenuItem patientsQustionnareToolStripMenuItem;
        protected ToolStripMenuItem qustionnareAviationToolStripMenuItem;
        protected ToolStripMenuItem questionnaireOccMedToolStripMenuItem;
        protected ToolStripMenuItem userManualMnu;
        protected ToolStripMenuItem dashBoardToolStripMenuItem;
        protected ToolStripMenuItem queueDetailToolStripMenuItem;
        protected ToolStripMenuItem statusLoginToolStripMenuItem;
        protected ToolStripMenuItem questionnaireAviationNewToolStripMenuItem;
        protected ToolStripMenuItem applicationForMedialCertificateToolStripMenuItem;
        protected ToolStripMenuItem applicationForRenewalMedialCertificateToolStripMenuItem;
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.userManualMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.todolistMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.qustionnareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientsQustionnareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionnaireAviationNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationForMedialCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationForRenewalMedialCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qustionnareAviationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionnaireOccMedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashBoardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queueDetailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userManualMnu,
            this.todolistMnu,
            this.qustionnareToolStripMenuItem,
            this.dashBoardToolStripMenuItem,
            this.queueDetailToolStripMenuItem,
            this.statusLoginToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(693, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // userManualMnu
            // 
            this.userManualMnu.Name = "userManualMnu";
            this.userManualMnu.Size = new System.Drawing.Size(85, 20);
            this.userManualMnu.Text = "User Manual";
            this.userManualMnu.Click += new System.EventHandler(this.userManualMnu_Click);
            // 
            // todolistMnu
            // 
            this.todolistMnu.Name = "todolistMnu";
            this.todolistMnu.Size = new System.Drawing.Size(117, 20);
            this.todolistMnu.Text = "Check-up Todolist";
            this.todolistMnu.Click += new System.EventHandler(this.todolistMnu_Click);
            // 
            // qustionnareToolStripMenuItem
            // 
            this.qustionnareToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patientsQustionnareToolStripMenuItem,
            this.questionnaireAviationNewToolStripMenuItem,
            this.qustionnareAviationToolStripMenuItem,
            this.questionnaireOccMedToolStripMenuItem});
            this.qustionnareToolStripMenuItem.Name = "qustionnareToolStripMenuItem";
            this.qustionnareToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.qustionnareToolStripMenuItem.Text = "Questionnaire";
            // 
            // patientsQustionnareToolStripMenuItem
            // 
            this.patientsQustionnareToolStripMenuItem.Name = "patientsQustionnareToolStripMenuItem";
            this.patientsQustionnareToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.patientsQustionnareToolStripMenuItem.Text = "Questionnaire Patient";
            this.patientsQustionnareToolStripMenuItem.Click += new System.EventHandler(this.patientsQustionnareToolStripMenuItem_Click);
            // 
            // questionnaireAviationNewToolStripMenuItem
            // 
            this.questionnaireAviationNewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationForMedialCertificateToolStripMenuItem,
            this.applicationForRenewalMedialCertificateToolStripMenuItem});
            this.questionnaireAviationNewToolStripMenuItem.Name = "questionnaireAviationNewToolStripMenuItem";
            this.questionnaireAviationNewToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.questionnaireAviationNewToolStripMenuItem.Text = "Questionnaire Aviation";
            // 
            // applicationForMedialCertificateToolStripMenuItem
            // 
            this.applicationForMedialCertificateToolStripMenuItem.Name = "applicationForMedialCertificateToolStripMenuItem";
            this.applicationForMedialCertificateToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.applicationForMedialCertificateToolStripMenuItem.Text = "Application for Medial Certificate";
            this.applicationForMedialCertificateToolStripMenuItem.Click += new System.EventHandler(this.applicationForMedialCertificateToolStripMenuItem_Click);
            // 
            // applicationForRenewalMedialCertificateToolStripMenuItem
            // 
            this.applicationForRenewalMedialCertificateToolStripMenuItem.Name = "applicationForRenewalMedialCertificateToolStripMenuItem";
            this.applicationForRenewalMedialCertificateToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.applicationForRenewalMedialCertificateToolStripMenuItem.Text = "Application for Renewal Medial Certificate";
            this.applicationForRenewalMedialCertificateToolStripMenuItem.Click += new System.EventHandler(this.applicationForRenewalMedialCertificateToolStripMenuItem_Click);
            // 
            // qustionnareAviationToolStripMenuItem
            // 
            this.qustionnareAviationToolStripMenuItem.Name = "qustionnareAviationToolStripMenuItem";
            this.qustionnareAviationToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.qustionnareAviationToolStripMenuItem.Text = "Questionnaire Aviation";
            // 
            // questionnaireOccMedToolStripMenuItem
            // 
            this.questionnaireOccMedToolStripMenuItem.Name = "questionnaireOccMedToolStripMenuItem";
            this.questionnaireOccMedToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.questionnaireOccMedToolStripMenuItem.Text = "Questionnaire Occ med.";
            // 
            // dashBoardToolStripMenuItem
            // 
            this.dashBoardToolStripMenuItem.Name = "dashBoardToolStripMenuItem";
            this.dashBoardToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.dashBoardToolStripMenuItem.Text = "Dash Board";
            this.dashBoardToolStripMenuItem.Click += new System.EventHandler(this.dashBoardToolStripMenuItem_Click);
            // 
            // queueDetailToolStripMenuItem
            // 
            this.queueDetailToolStripMenuItem.Name = "queueDetailToolStripMenuItem";
            this.queueDetailToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.queueDetailToolStripMenuItem.Text = "Queue Detail";
            this.queueDetailToolStripMenuItem.Click += new System.EventHandler(this.queueDetailToolStripMenuItem_Click);
            // 
            // statusLoginToolStripMenuItem
            // 
            this.statusLoginToolStripMenuItem.Name = "statusLoginToolStripMenuItem";
            this.statusLoginToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.statusLoginToolStripMenuItem.Text = "Status Login";
            this.statusLoginToolStripMenuItem.Click += new System.EventHandler(this.statusLoginToolStripMenuItem_Click);
            // 
            // CheckupInheriteFrm
            // 
            this.ClientSize = new System.Drawing.Size(693, 262);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CheckupInheriteFrm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void userManualMnu_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string filePath = PrePareData.StaticDataCls.UserManualPath;
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                catch
                {

                }
            }
            Cursor = Cursors.Arrow;
        }
        private void todolistMnu_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("firefox.exe", PrePareData.StaticDataCls.ToDoListUrl);
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.MessageError("UIMenuBar", "todolistMnu_Click", ex, false);
            }
        }
        private void patientsQustionnareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                QuestionnaireFrm fqn = new QuestionnaireFrm();
                fqn.tpr_id = Program.CurrentRegis.tpr_id;
                fqn.ShowDialog();
                trn_ques_patient q = cdc.trn_ques_patients.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                cdc.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, q);
            }
        }
        private void applicationForMedialCertificateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmQuestionareAviation_N frm = new frmQuestionareAviation_N())
            {
                frm.loadfrm();
                frm.ShowDialog();
            }
        }
        private void applicationForRenewalMedialCertificateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmQuestionareAviation_F frm = new frmQuestionareAviation_F())
            {
                frm.loadfrm();
                frm.ShowDialog();
            }
        }
        private void dashBoardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmDashBoardcs frm = new frmDashBoardcs())
            {
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            }
        }
        private void statusLoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("firefox.exe", PrePareData.StaticDataCls.StatusLoginUrl);
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.MessageError("UIMenuBar", "todolistMnu_Click", ex, false);
            }
        }
        private void queueDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("firefox.exe", PrePareData.StaticDataCls.QueueDetailUrl);
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.MessageError("UIMenuBar", "todolistMnu_Click", ex, false);
            }
        }
    }
}