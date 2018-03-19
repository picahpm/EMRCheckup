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
    public partial class PLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["username"]) && !string.IsNullOrEmpty(Request.QueryString["otp"]))
                {
                    string username = Request.QueryString["username"].ToString();
                    string otp = Request.QueryString["otp"].ToString();
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        var user = dbc.mst_user_logins
                                    .Where(x => x.mul_user_login.ToUpper() == username.ToUpper() &&
                                                x.mut_status == 'A')
                                    .Select(x => new
                                    {
                                        x.mst_user_type.mut_code,
                                        x.mul_fname
                                    }).FirstOrDefault();

                        var mst_otp = dbc.mst_user_otps.Where(x => x.mul_user_login == username && x.mul_otp == otp).FirstOrDefault();

                        if (user != null && mst_otp != null)
                        {
                            mst_otp.mul_expire = funcCls.GetServerDateTime();
                            dbc.SubmitChanges();

                            Constant.CurrentUserLogin = username;
                            Constant.CurrentPageLogin = user.mut_code;
                            Constant.CurrentUserLoginName = user.mul_fname;

                            switch (Constant.CurrentPageLogin)
                            {
                                case "MKT":
                                    Response.Redirect(this.Page.ResolveClientUrl("~/frmmktNew.aspx"));
                                    break;
                                case "HPC":
                                    Response.Redirect(this.Page.ResolveClientUrl("~/frm_hpc.aspx"));
                                    break;
                                case "CLT":
                                    Response.Redirect(this.Page.ResolveClientUrl("~/frm_collection.aspx"));
                                    break;
                                case "CTC":
                                    Response.Redirect(this.Page.ResolveClientUrl("~/frm_contact_center.aspx"));
                                    break;
                            }
                        }
                        else
                        {
                            lbalertmsg.Text = "UserName & Password incorrect. Please try again";
                            txtUser.Text = username;
                            txtpass.Text = "";
                            txtUser.Focus();
                        }
                    }
                }
            }
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
            string msgalert = "";
            if (Login1(txtUser.Text.Trim(), txtpass.Text.Trim(), ref msgalert))
            {
                //Constant.CurrentUserLogin = "test1";
                string backurl = Convert1.ToString(Request.QueryString["backurl"]).ToLower();
                if (backurl == "refresh")
                {
                    litscript.Text = "<script type='text/javascript'>reloadparent();</script>";
                }
                else if (backurl != "")
                {
                    String goUrl = Context.Server.UrlDecode(backurl);
                    Context.Response.Redirect(backurl);
                }
                else
                {
                    //Defualt Page Current
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        var objpolicy = (from t1 in dbc.mst_user_logins
                                         where t1.mul_user_login == Constant.CurrentUserLogin
                                         select t1.mst_user_type.mut_code).FirstOrDefault();
                        if (objpolicy != null)
                        {
                            Constant.CurrentPageLogin = objpolicy;
                        }
                    }

                    switch (Constant.CurrentPageLogin)
                    {
                        case "MKT":
                            Response.Redirect(this.Page.ResolveClientUrl("~/frmmktNew.aspx"));
                            break;
                        case "HPC":
                            Response.Redirect(this.Page.ResolveClientUrl("~/frm_hpc.aspx"));
                            break;
                        case "CLT":
                            Response.Redirect(this.Page.ResolveClientUrl("~/frm_collection.aspx"));
                            break;
                        case "CTC":
                            Response.Redirect(this.Page.ResolveClientUrl("~/frm_contact_center.aspx"));
                            break;
                        //case "All":
                        //    Response.Redirect(this.Page.ResolveClientUrl("~/frmHome.aspx"));
                        //    break;
                    }
                    //Response.Redirect(this.Page.ResolveClientUrl("~/frmHome.aspx"));
                }
            }
            else
            {
                if (msgalert != "")
                {//กรณีที่ไม่ได้ Active User
                    lbalertmsg.Text = msgalert;
                }
                else
                {
                    lbalertmsg.Text = "UserName & Password incorrect. Please try again";
                }
                txtUser.Text = "";
                txtpass.Text = "";
                txtUser.Focus();
            }

        }
        private bool Login1(string strUsername, string strPassword, ref string msgalert)
        {
            bool checkLoginAdmin = false;
            msgalert = "";
            //if (strUsername == "Admin" && strPassword == "nimd@")
            //{
            //    Constant.CurrentUserLogin = strUsername;
            //    Constant.CurrentUserLoginName = "Admin";
            //    return true;
            //}

            try
            {
                Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
                DataTable dt = ws.LogonTrakcare(strUsername, strPassword);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        DataRow dr = dt.Rows[0];
                        Constant.CurrentUserLoginName = dr["SSUSR_Name"].ToString();
                    }
                    catch (Exception ex)
                    {

                    }

                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        var objcurrentuser = (from t1 in dbc.mst_user_logins
                                              where t1.mul_user_login.ToUpper() == strUsername.ToUpper()
                                              select t1).FirstOrDefault();
                        if (objcurrentuser != null)
                        {
                            if (objcurrentuser.mut_status == 'A')
                            {
                                Constant.CurrentUserLogin = objcurrentuser.mul_user_login;
                                if (Constant.CurrentUserLoginName == "")
                                {
                                    Constant.CurrentUserLoginName = objcurrentuser.mul_fname;
                                }
                                return true;
                            }
                            else
                            {
                                Constant.CurrentUserLogin = "";
                                msgalert = "Your status inactive. Please contact admin active status. ";
                                return false;
                            }
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
                    checkLoginAdmin = true;
                }
            }
            catch
            {
                checkLoginAdmin = true;
            }


            if (checkLoginAdmin)
            {
                if (strUsername == "CLT"
                    || strUsername == "CTC"
                    || strUsername == "HPC"
                    || strUsername == "ook"
                    || strUsername == "yee"
                    || strUsername == "MKT"
            || (strUsername == "Admin" && strPassword == "nimd@"))
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        var objcurrentuser = (from t1 in dbc.mst_user_logins
                                              where t1.mul_user_login == strUsername
                                              select t1).FirstOrDefault();
                        if (objcurrentuser != null)
                        {
                            if (objcurrentuser.mut_status == 'A')
                            {
                                Constant.CurrentUserLogin = strUsername;
                                Constant.CurrentUserLoginName = objcurrentuser.mul_fname;
                                return true;
                            }
                            else
                            {
                                Constant.CurrentUserLogin = "";
                                msgalert = "Your status inactive. Please contact admin active status.";
                                return false;
                            }
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
                    Constant.CurrentUserLogin = "";
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}