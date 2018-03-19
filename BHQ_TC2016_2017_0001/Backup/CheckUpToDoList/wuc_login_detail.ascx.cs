using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class wuc_login_detail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                var objcurrentuser = (from t1 in dbc.mst_user_logins
                                      where t1.mul_user_login == Constant.CurrentUserLogin
                                      select t1).FirstOrDefault();
                if (objcurrentuser != null)
                {
                   //Convert1.ToString(objcurrentuser.mul_lname)
                    lbllogin_detail.Text = String.Format( Convert1.ToString( objcurrentuser.mul_fname)+
                                                            " " + Constant.CurrentUserLoginName + 
                                                            " : เข้าสู่ระบบเมื่อ {0:dd/MM/yyyy HH:mm}", DateTime.Now);
                }
             }
       }
    }
}