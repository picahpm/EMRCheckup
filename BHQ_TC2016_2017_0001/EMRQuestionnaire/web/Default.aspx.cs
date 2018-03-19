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
    public partial class Default : System.Web.UI.Page
    {
        Utility ut = new Utility();
        DataTable dtMasLabel = new DataTable();
        executeDC clsEX = new executeDC();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session["LEGION"] = "EN";
            Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));
      
            loadLabel();
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            Service.WS_TrakcareCls wsVerify = new Service.WS_TrakcareCls();
            DataTable dtVerify = wsVerify.GetAllAllergyByHN_NameDob(txtHN.Text.Trim(), txtName.Text.Trim(), txtBirthDate.Text.Trim());
            DataTable dtHistory = clsEX.get_history_verify(txtHN.Text.Trim());
            if (dtVerify != null)
            {
                if (!(dtVerify.Rows.Count).Equals(0))
                {

                    if (dtHistory.Rows.Count >= 1)
                    {
                        Session["HN"] = txtHN.Text.Trim();
                        Session["Name"] = dtVerify.Rows[0]["TTL_Desc"].ToString() + " " + dtVerify.Rows[0]["PAPMI_Name"].ToString() + " " + dtVerify.Rows[0]["PAPMI_Name2"].ToString();
                        
                        ////Session["Allergy"] = ut.resultAllergy(dtVerify);
                        Response.Redirect("~/web/frm_verify_result.aspx");

                    }
                    else
                    {
                        lblWarningVerify.Text = "No Verfify Patient";
                    }
                }
                else
                {
                    lblWarningVerify.Text = "No Verfify Patient";
                }
            }

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

            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_MEDICAL_CHECKUP");
            LabelMEDMain.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelMEDMain'"));
            LabelMED01.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelMED01'"));
            LabelMED02.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelMED02'"));
            LabelMED03.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelMED03'"));
            btnVerify.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LabelMED04'"));
            
            //end of Label
        }

    }
}
