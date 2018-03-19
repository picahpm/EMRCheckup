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
    public partial class frm_verify_result : System.Web.UI.Page
    {
        Utility ut = new Utility();
        executeDC clsEX = new executeDC();
        DataTable dtDraft = new DataTable();
        DataTable dtMasLabel = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["HN"].ToString() == string.Empty)
            {
                Response.Redirect("~/default.aspx");
            }
            Session["LEGION"] = "EN";
            Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));
            plAviationType.Visible = false;

            loadLabel();
        }
        protected void btnMainPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/web/MainPage.aspx");
        }
        protected void btnHealthHistory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/web/questionaire_Health_History.aspx");
        }
        protected void imgThai_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["LEGION"] = "TH";
                Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_thai.css") + "\" />"));
      
                loadLabel();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void imgEnglish_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["LEGION"] = "EN";
                Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));
      
                loadLabel();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void loadLabel()
        {

            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_MENU_QUESTIONARE");
            LabelQuesMain.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelQuesMain'"));
            btnMainPage.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelQues01'"));
            btnHealthHistory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelQues02'"));
            btnAviation.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelQues03'"));

            //end of Label
        }

        protected void btnAviation_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/web/Aviation_Renew.aspx");
            plAviationType.Visible = true;
        }

        protected void AviationTypeLink_Click(object sender, BulletedListEventArgs e)
        {
            ListItem url = AviationTypeLink.Items[e.Index];
            Response.Redirect(url.Value);
        }        
    }
}
