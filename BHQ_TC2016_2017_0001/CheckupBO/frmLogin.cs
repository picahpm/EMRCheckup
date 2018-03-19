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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.Text = this.Text + " " + Program.AssemblyVersion;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            BtnLogin.Enabled = false;
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                var objcurrentuser = (from t1 in dbc.mst_user_types
                                      where t1.mut_admin == true
                                      && t1.mut_username == txtusername.Text.Trim()
                                      && t1.mut_password == txtpassword.Text.Trim()
                                      select t1).FirstOrDefault();
                if (objcurrentuser != null)
                {
                    Program.CurrentUser = objcurrentuser;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    lbAlertMsg.Text = "Username & Password incorrect." + Environment.NewLine + "Please try again.";
                    txtusername.Text = "";
                    txtpassword.Text = "";
                    txtusername.Focus();
                    Program.CurrentUser = null;
                }
            }
            BtnLogin.Enabled = true;
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void txtpassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BtnLogin.Focus();
                BtnLogin_Click(null, null);
            }
        }
    }
}
