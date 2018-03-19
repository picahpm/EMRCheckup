using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckupBO
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Text = this.Text + " " + Program.AssemblyVersion;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LogoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            Program.Logout();

            LogoffToolStripMenuItem.Visible = false;
            LoginToolStripMenuItem.Visible = true;

            frmLogin frm = new frmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                HindMenu();

                LoginToolStripMenuItem.Visible = false;
                LogoffToolStripMenuItem.Visible = true;

                frmAllRoom frmall = new frmAllRoom();
                //frmall.MdiParent = this;
                frmall.ShowDialog();
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
                    LoginToolStripMenuItem.Visible = false;
                    LogoffToolStripMenuItem.Visible = true;
                    if (Program.CurrentUser.mut_admin == true)
                    {
                        policyEditToolStripMenuItem.Visible = true;
                    }

                    frmAllRoom frmall = new frmAllRoom();
                   // frmall.MdiParent = this;
                    frmall.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Source, ex.StackTrace, ex.Message,false);
            }
        }
        private void HindMenu()
        {
            foreach (ToolStripMenuItem mt in menuStrip.Items)
            {
                if (mt.Tag != null && mt.Tag.ToString() == "1")
                {

                }
                else
                {
                    mt.Visible = false;
                }
            }

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoginToolStripMenuItem_Click(null, null);
        }

        private void policyEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPolicyEdit frmpo = new frmPolicyEdit();
            frmpo.MdiParent = this;
            frmpo.Show();
        }

    }
}
