using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Text = PrePareData.StaticDataCls.ProjectName;
        }
        
        private void frmMain_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            string ver = Program.AssemblyVersion;

            stripVersion.Text = "Version : " + ver;
            LoginToolStripMenuItem_Click(null, null);
            try
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    piorityToolStripMenuItem.Visible = true;
                }
            }
            catch
            {

            }
        }
        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmLogin frm = new frmLogin();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    HindMenu();
                    ShowTooStatus();
                    LoginToolStripMenuItem.Visible = false;
                    LogoffToolStripMenuItem.Visible = true;

                    frmAllRoom frmAR= new frmAllRoom();
                    if (frmAR.ShowDialog() == DialogResult.Yes)
                    {
                        LoginToolStripMenuItem.Visible = true;
                        LogoffToolStripMenuItem.Visible = false;
                        LoginToolStripMenuItem_Click(null, null);
                    }

                }
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Source, ex.StackTrace, ex.Message);
            }
        }
        private void HindMenu()
        {
            foreach (ToolStripMenuItem mt in menuStrip.Items)
            {
                if (mt.Tag !=null && mt.Tag.ToString() == "1")
                {

                }
                else
                {
                    mt.Visible = false;
                }
            }

        }

        private void ShowTooStatus()
        {//แสดง status footer Bar หลัง Login
            toolstatusIPaddress.Text =string.Format("Computer IP Address : {0}", Program.GetLocalIP());
            toolStatusUserLogin.Text = string.Format("User Log in :[{0}] {1}",Program.CurrentUser.mut_username.ToString() ,Program.CurrentUser.mut_fullname);
            if (Program.CurrentRoom != null)
            {
                toolstatusLocation.Text = string.Format("Location : {0} Counter :{1}", Program.CurrentSite.mhs_ename, Program.CurrentRoom.mrd_ename);
            }
            else
            {
                toolstatusLocation.Text = string.Format("Location : {0} ", Program.CurrentSite.mhs_ename);
            }
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            if (this.HaveForm("frmViewQueueList") == false)
            {
                frmViewQueueList childForm = new frmViewQueueList();
                //childForm.MdiParent = this;
                this.WindowState = FormWindowState.Maximized;
                childForm.ShowDialog();
            }
        }
      
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
          this.Close();
        }
        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("คุณต้องการออกจากโปรแกรมหรือไม่", "ยืนยันการออกจากโปรแกรม", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (Class.ClsManageUserLogin.current_log != null)
                {
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            log_user_login log = cdc.log_user_logins.Where(x => x.lug_id == Class.ClsManageUserLogin.current_log.lug_id).FirstOrDefault();
                            if (log.lug_end_date == null)
                            {
                                log.lug_end_date = Program.GetServerDateTime();
                                cdc.SubmitChanges();
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                Program.Logout();
            }
        }

        private void LogoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            ClearShowTooStatus();
            Program.Logout();

            LogoffToolStripMenuItem.Visible = false;
            LoginToolStripMenuItem.Visible = true;

            frmLogin frm = new frmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                HindMenu();
                ShowTooStatus();
                frmAllRoom frmAR = new frmAllRoom();
                if (frmAR.ShowDialog() == DialogResult.Yes)
                {
                    LoginToolStripMenuItem.Visible = true;
                    LogoffToolStripMenuItem.Visible = false;
                    LoginToolStripMenuItem_Click(null, null);
                }
                else
                {
                    LoginToolStripMenuItem.Visible = false;
                    LogoffToolStripMenuItem.Visible = true;
                }

            }
        }
        private void ClearShowTooStatus()
        {//แสดง status footer Bar หลัง Login
            toolstatusIPaddress.Text = string.Format("Computer IP Address : {0}", "");
            toolStatusUserLogin.Text = string.Format("User Log in :[{0}] {1}","", "");
            toolstatusLocation.Text = string.Format("Location : {0} Counter :{1}", "", "");
        }
        
        private Boolean HaveForm(string FormName)
        {
            foreach (Form mdiform in this.MdiChildren)
            {
                if (mdiform.Name.ToLower() == FormName.ToLower())
                {
                    mdiform.Focus();
                    return true;
                }
            }
            return false;
        }

        private void vitalSignsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.HaveForm("frmCumulativeVitalSigns") == false)
            {
                frmCumulativeVitalSigns frm = new frmCumulativeVitalSigns();
                frm.MdiParent = this;
                frm.Show();
                frm.WindowState = FormWindowState.Maximized;
            }
        }

        private void waitingAllRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWaiting frmwtt = new frmWaiting();
            frmwtt.WindowState = FormWindowState.Maximized;
            frmwtt.ShowDialog();
        }

        private void comulativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.HaveForm("frmCumulative") == false)
            {
                frmCarotid_2 frm = new frmCarotid_2();
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            }
        }
    }
}
