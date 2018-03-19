using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Windows.Forms;

namespace BKvs2010
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            txtusername.LostFocus += txt_LostFocus;
            txtpassword.LostFocus += txt_LostFocus;
            Image logo = Program.GetLogo();
            int top = (lbAlertMsg.Top / 2) - (logo.Height / 2);
            int left = (this.Width / 2) - (logo.Width / 2);
            pictureBox1.Size = logo.Size;
            pictureBox1.Location = new Point(left, top);
            pictureBox1.Image = logo;

            //Program.GetAllBrowser();
        }
        private void txt_LostFocus(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.Text = tb.Text.Trim();
            }
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                List<mst_hpc_site> listSite = cdc.mst_hpc_sites
                                                 .Where(x => x.mhs_status == 'A' &&
                                                             x.mst_room_hdrs
                                                              .Any(y => y.mst_user_rooms
                                                                         .Any(z => z.mst_user_type.mut_username.ToUpper() == txtusername.Text.Trim().ToUpper())))
                                                 .OrderBy(x => x.mhs_id)
                                                 .ToList();
                if (listSite.Count > 0)
                {
                    CBSite.DataSource = listSite;
                    CBSite.DisplayMember = "mhs_ename";
                    CBSite.ValueMember = "mhs_id";

                    
                    int? defaultsite = cdc.mst_user_types.Where(x => x.mut_username.ToUpper() == txtusername.Text.Trim().ToUpper()).Select(x => x.mut_default_hpc).FirstOrDefault();
                    if (defaultsite != null && defaultsite != 0)
                    {
                        CBSite.SelectedValue = defaultsite;
                    }
                }
                else
                {
                    CBSite.DataSource = null;
                }
            }
            //CBSite.DataSource = null;

            ////Add
            ////txtusername.Text = txtusername.Text.ToUpper();

            //mst_user_type datasite1 = (from t1 in dbc.mst_user_types
            //                           where t1.mut_username.ToUpper() == txtusername.Text.Trim().ToUpper()
            //                           && t1.mut_status == 'A'
            //                           select t1).FirstOrDefault();
            //if (datasite1 != null)
            //{
            //    var ddd = (from t1 in dbc.mst_user_rooms
            //               where t1.mut_id == datasite1.mut_id
            //               orderby t1.mst_room_hdr.mst_hpc_site.mhs_id
            //               select new
            //               {
            //                   id = t1.mst_room_hdr.mst_hpc_site.mhs_id,
            //                   name = t1.mst_room_hdr.mst_hpc_site.mhs_ename
            //               });

            //    CBSite.DisplayMember = "name";
            //    CBSite.ValueMember = "id";
            //    CBSite.DataSource = ddd.DistinctBy(p => new { p.id, p.name }).Select(g => new { g.id, g.name }).ToList();
            //}
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtusername_TextChanged(null, null);
            this.Text = BKvs2010.PrePareData.StaticDataCls.ProjectName;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Program.CurrentUser = null;
            Program.CurrentRoom = null;
            Program.CurrentLogin = null;

            Program.CurrentSite = CBSite.SelectedItem as mst_hpc_site;

            if (string.IsNullOrEmpty(txtusername.Text) || string.IsNullOrEmpty(txtpassword.Text))
            {
                lbAlertMsg.Text = "Please insert username and password.";
            }
            else if (Program.CurrentSite == null)
            {
                lbAlertMsg.Text = "Please select HPC site.";
            }
            else
            {
                mst_user_type user = CheckUserCheckup(txtusername.Text, txtpassword.Text);
                if (user != null)
                {
                    Program.CurrentUser = user;
                    Program.CurrentSite = CBSite.SelectedItem as mst_hpc_site;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            var u = cdc.mst_user_types.Where(x => x.mut_id == user.mut_id).FirstOrDefault();
                            u.mut_default_hpc = Program.CurrentSite.mhs_id;
                            cdc.SubmitChanges();
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    lbAlertMsg.Text = "Username or Password incorrect. Please try again";
                }
            }
            //if (Program.Login(txtusername.Text.Trim(), txtpassword.Text.Trim(), Convert.ToInt32(CBSite.SelectedValue)))
            //{
            //    this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //}
            //else
            //{
            //    lbAlertMsg.Text = "UserName && Password incorrect. Please try again";
            //    txtusername.Text = "";
            //    txtpassword.Text = "";
            //    txtusername.Focus();
            //}
        }
        private mst_user_type CheckUserCheckup(string username, string password)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_user_type user = cdc.mst_user_types
                                            .Where(x => x.mut_username.ToUpper() == username.ToUpper())
                                            .FirstOrDefault();
                    if (user == null)
                    {
                        return null;
                    }
                    else if (user.mut_admin_data == true && user.mut_password == password)
                    {
                        return user;
                    }
                    else
                    {
                        bool checkpass = new APITrakcare.LogonTrakcareCls().CheckTrakcarePassword(username, password);
                        if (checkpass)
                        {
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.OpenUserManual();
        }
    }
}
