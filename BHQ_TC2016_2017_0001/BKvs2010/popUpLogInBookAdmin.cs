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
    public partial class popUpLogInBookAdmin : Form
    {
        private int _tpr_id;

        public popUpLogInBookAdmin()
        {
            InitializeComponent();
        }

        private class userAdmin 
        {
            public string user { get; set; }
            public string password { get; set; }
        }

        private List<userAdmin> admin = new List<userAdmin>
        {
          // new userAdmin { user = "BHN", password = "1234" }
            new userAdmin { user = "hpc1", password = "1234" }
         //   new userAdmin { user = "BHN", password = "1234" }
        };

        private bool loginSuccess = false;

        public bool LogIn(int tpr_id)
        {
            try
            {
                _tpr_id = tpr_id;
                this.ShowDialog();
                return loginSuccess;
            }
            catch
            {

            }
            return loginSuccess;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    userAdmin user = admin.Where(x => x.user.ToUpper() == txtusername.Text.ToUpper()).FirstOrDefault();
                    if (user != null && user.password == txtpassword.Text)
                    {
                        trn_patient_doctor_approve docApprove = cdc.trn_patient_doctor_approves
                                                                   .Where(x => x.tpr_id == _tpr_id)
                                                                   .FirstOrDefault();
                        docApprove.tpda_status = "CBB";
                        docApprove.tpda_process_by = user.user;
                        docApprove.tpda_process_date = DateTime.Now;
                        cdc.SubmitChanges();
                        loginSuccess = true;
                        this.Close();
                    }
                    else
                    {
                        label3.Visible = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("เกิดความผิดพลาด กรุณาลองอีกครั้ง", "Fail.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtusername_Leave(object sender, EventArgs e)
        {
            txtusername.Text = txtusername.Text.Trim();
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            label3.Visible = false;
        }
    }
}
