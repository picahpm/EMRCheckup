using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using QuestionnaireWebSite.clsUtility;
using QuestionnaireWebSite.clsExecuteSQL;

namespace EMRQuestionnaire.web
{
    public partial class frm_History : System.Web.UI.Page
    {
        DataTable dtMasLabel = new DataTable();
        Utility ut = new Utility();
        executeDC clsEX = new executeDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["HN"].ToString() == string.Empty)
            //{
            //    Response.Redirect("~/default.aspx");
            //}
        }
    }
}
