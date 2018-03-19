using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckUpToDoList
{
    public partial class frmCompanyMstBody : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //เช็คสิทธิการเข้าใช้ by Yee
            Constant.CheckPolicy("MKT");
        }
    }
}