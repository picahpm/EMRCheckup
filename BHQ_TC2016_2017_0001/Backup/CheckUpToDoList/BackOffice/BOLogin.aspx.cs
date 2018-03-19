using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class BOLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private Boolean chkPassWord(string user, string pass)
        {

            Boolean chk = true;
            return chk;
        }
        protected void btnsubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (txtUser.Text == "" || txtpass.Text == "")
            {
                lbalertmsg.Text = "input UserName & Password try again";
                txtUser.Focus();
                return;
            }
            if (Login1(txtUser.Text.Trim(), txtpass.Text.Trim()))
            {
                //Defualt Page Current
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    var objpolicy = (from t1 in dbc.mst_user_logins
                                        where t1.mul_user_login == Constant.CurrentUserLogin
                                        select t1).FirstOrDefault();
                    if (objpolicy != null)
                    {
                        Constant.CurrentPageLogin = objpolicy.mul_user_login;
                    }
                }

                switch (Constant.CurrentPageLogin)
                {
                    case "Admin":
                        Response.Redirect(this.Page.ResolveClientUrl("~/BackOffice/AdminControlPage.aspx"));
                        break;
                }
            }
            else
            {
                lbalertmsg.Text = "UserName & Password incorrect. Please try again";
                txtUser.Text = "";
                txtpass.Text = "";
                txtUser.Focus();
            }

        }
        private bool Login1(string strUsername, string strPassword)
        {
            Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
            DataTable dt = ws.LogonTrakcare(strUsername, strPassword);
            if (dt.Rows.Count > 0)
            {
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    var objcurrentuser = (from t1 in dbc.mst_user_logins
                                          where t1.mul_user_login == strUsername
                                          select t1).FirstOrDefault();
                    if (objcurrentuser != null && (objcurrentuser.mul_user_login == "Admin" && strPassword == "nimd@"))
                    {
                        Constant.CurrentUserLogin = strUsername;
                        return true;
                    }
                    else
                    {
                        Constant.CurrentUserLogin = "";
                        return false;
                    }
                }
            }
            else
            {
                if (strUsername == "Admin" && strPassword=="nimd@" )
                {
                    Constant.CurrentUserLogin = strUsername;
                    return true;
                }
                else
                {
                    Constant.CurrentUserLogin = "";
                    return false;
                }
            }
        }
    }
}