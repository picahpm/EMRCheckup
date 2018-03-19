using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CheckUpToDoList
{
    public partial class frmmktNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Constant.CheckPolicy("MKT");

            //noina 26/09/56
           string tcd_id = Request.QueryString["id"];
           string mco_code = Request.QueryString["code"];

           frm2.Attributes.Add("src", "frmmktdata.aspx?id="  + tcd_id + "&code=" + mco_code);
        }
        
    }
}