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
using QuestionnaireWebSite.clsExecuteSQL;
using QuestionnaireWebSite.clsUtility;
using EMRQuestionnaire.clsQuestionaire;
using DBCheckup;

namespace EMRQuestionnaire.web
{
    public partial class Aviation_Initial : System.Web.UI.Page
    {
        executeDC clsEX = new executeDC();
        DataTable dtMasLabel = new DataTable();
        Utility ut = new Utility();
        DataTable dtHistory = new DataTable();

        private static InhCheckupDataContext cdc;
        private DateTime currentDateTime;
        private string tmpRemark = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Request.QueryString["sHn"]) == true)
            {
                if (Session["HN"].ToString() == string.Empty)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    if (String.IsNullOrEmpty(Request.QueryString["sAllergy"]) == true)
                    {
                        Session["Allergy"] = "";
                    }
                }
            }
            else
            {
                Session["LEGION"] = "EN";
                Session["HN"] = Request.QueryString["sHn"];
                Session["Name"] = Request.QueryString["sName"];
                Session["Allergy"] = Request.QueryString["sAllergy"];
            }
            if (Session["LEGION"] == "TH")
            {
                System.Web.UI.HtmlControls.HtmlGenericControl mystyles;
                mystyles = new System.Web.UI.HtmlControls.HtmlGenericControl();
                mystyles.TagName = "style";
                string sampleCSS = "body { font-family: 'thaisans_neueregular'; }";
                mystyles.InnerText = sampleCSS;
                Page.Header.Controls.Add(mystyles);
            }
            else
            {
                System.Web.UI.HtmlControls.HtmlGenericControl mystyles;
                mystyles = new System.Web.UI.HtmlControls.HtmlGenericControl();
                mystyles.TagName = "style";
                string sampleCSS = "body { font-family:  sans-serif; }";
                mystyles.InnerText = sampleCSS;
                //Page.Header.Controls.Add(mystyles);
                Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));
            }
            txtHN.Text = Session["HN"].ToString();
            txtName.Text = Session["Name"].ToString();
            txtAllergies.Text = Session["Allergy"].ToString();

            if (!Page.IsPostBack)
            {
                Session["LEGION"] = "EN";
                //Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));

                loadGeneralInfo(Session["HN"].ToString());
                loadLabel();
                loadData(Session["HN"].ToString());
            }
        }

        private void loadGeneralInfo(string hn_no)
        {
            try
            {
                dtHistory = clsEX.get_history_patients(hn_no);
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    txtHN.Text = dtHistory.Rows[0]["HN"].ToString().Trim();
                    txtName.Text = dtHistory.Rows[0]["FULL_NAME"].ToString().Trim();
                    txtRoom.Text = dtHistory.Rows[0]["ROOM"].ToString().Trim();
                    txtPhysician.Text = dtHistory.Rows[0]["PHYSICIAN"].ToString().Trim();
                    txtVisitDate.Text = dtHistory.Rows[0]["VISIT_DATE"].ToString().Trim();
                    txtDepartment.Text = dtHistory.Rows[0]["DEPARTMENT"].ToString().Trim();
                    txtBirthDate.Text = dtHistory.Rows[0]["BIRTH_DATE"].ToString().Trim();
                    txtAge.Text = dtHistory.Rows[0]["AGE"].ToString().Trim();
                    txtSex.Text = dtHistory.Rows[0]["SEX"].ToString().Trim();
                    txtAllergies.Text = dtHistory.Rows[0]["ALLERGIES"].ToString().Trim();
                    Session["KEY_GEN"] = dtHistory.Rows[0]["KEY_GEN"].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }        
        private void loadLabel()
        {
            #region GeneralInformation
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PATIENTS");
            Lblperson.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'person'"));
            lblName.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'name'"));
            lblRoom.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'room'"));
            lblHN.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'hn'"));
            lblPhysician.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'phy'"));
            lblVisitDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'visit_date'"));
            lblDepartment.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'department'"));
            lblBirthDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'birth_date'"));
            lblAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'age'"));
            lblSex.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'sex'"));
            lblAllergies.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'allergies'"));
            #endregion            

            #region RecommendationHeader
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_AVIATIONS_RECOMMEND");
            lblRecommendation.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'lblRecommendation'"));
            lblRecommendationRule.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'lblRecommendationNew1'")) + "<br/>" +
                                        ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'lblRecommendationNew2'")) + "<br/>" +
                                        ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'lblRecommendationNew3'"));
            #endregion

            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_AVIATION_NEW");
            #region Recommendation
            lblPlace.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL1'"));
            lblName_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL2NAME'"));
            lblNationality_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL2NATION'"));
            lblGender.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL2SEX'"));
            rdGender_M.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL2MALE'"));
            rdGender_F.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL2FEMALE'"));
            lblDOB_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3DOB'"));
            lblAge_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3AGE'"));
            lblAgeYear_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3YEAR'"));
            lblAgeMonth_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3MONTH'"));
            lblMarital.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3MARITAL'"));
            rdMarital_S.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3SINGLE'"));
            rdMarital_M.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3MARRIED'"));
            rdMarital_D.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3DIVORCED'"));
            rdMarital_P.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3SEPERATED'"));
            rdMarital_W.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3WIDOWED'"));
            lblTOL.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4TOL'"));
            rdTOL_N.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4INTITIAL'"));
            rdTOL_R.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4RENEW'"));
            rdoATPL.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4ATPL'"));
            rdoPPL.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4PPL'"));
            rdoATC.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4ATC'"));
            rdoCPL.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4CPL'"));
            rdoPE.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4PE'"));
            rdoStudent.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4ST'"));
            rdoOther.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4OTHER'"));
            lblLicenseNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4LICENSE'"));
            lblAddress_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL5ADDR'"));
            lblTel_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL5TEL'"));
            lblOccupation_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL6OCC'"));
            lblCompany_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL6COMPANY'"));
            lblComAddress_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL7ADDR'"));
            lblComTel_th.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL7TEL'"));
            lblContactAdd.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL8ADDR'"));
            rdContactAdd_R.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL8RESIDENCE'"));
            rdContactAdd_C.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL8COMPANY'"));
            lblContactPerson.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL8CONTACTPERSON'"));
            lblContactTel.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL8TEL'"));
            lblHaveBeenExam.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9MAINQUESTION'"));
            rdHaveBeenExam_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9NO'"));
            rdHaveBeenExam_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9YES'"));
            lblIfYesExam.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9IFYES'"));
            lblDateHaveExam.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9DATE'"));
            lblWereYouDeclared.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9SUBQUESTION'"));
            rdWereYouDeclared_F.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9FIT'"));
            rdWereYouDeclared_U.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9UNFIT'"));
            lblHasMedical.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL10MAINQUESTION'"));
            rdHasMedical_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL10NO'"));
            rdHasMedical_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL10YES'"));
            lblIfYesMedical.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL10IFYES'"));
            lblFlyTime.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL11TFT'"));
            lblLast6Month.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL11LASTSIXMONTHS'"));
            lblForceInputNum1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELFORCENUM'"));
            lblForceInputNum2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELFORCENUM'"));
            lblAircraftPresently.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12AISRCARFT'"));
            chkJet.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12JET'"));
            chkTurboProp.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12TURBOPROP'"));
            chkHelicopter.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12HELICOPTER'"));
            chkPiston.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12PISTON'"));
            chkOther.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12OTHER'"));
            lblPresentlyFlyingStatus.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12SUBQUESTION'"));
            chkSinglePilot.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12SINGLE'"));
            chkMultiPilot.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12MULTI'"));
            lblSmoke.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13MAINQUESTION'"));
            rdoSmokeNever.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13NEVER'"));
            rdoSmokeStopSince.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13STOPPED'"));
            rdoSmokeYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13YES'"));
            lblSmokeTobaccoType.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13STATETYPE'"));
            lblSmokeAmount.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13AMOUNT'"));
            lblCurrentlyMedication.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14MAINQUESTION'"));
            rdMedicationNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14NO'"));
            rdMedicationYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14YES'"));
            lblMedicationIfYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14IFYES'"));
            lblMedicationQuantity.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14QUANTITY'"));
            lblMedicationStartDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14STARTDATE'"));
            lblMedicationReason.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14REASON'"));
            lblAlcohal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL15MAINQUESTION'"));
            lblExercise.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL16MAINQUESTION'"));
            rdExercise_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14NO'"));
            rdExercise_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14YES'"));
            #endregion
            #region MedicalHistory
            lblMedicalHistory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELMEDHISTORY'"));
            lblMedicalHistoryInfo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELMEDHISTORYDETAILS'"));
            lblMainQuestion17.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17MAINQUESTION'"));
            rdMainQuestion17_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion17_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion18.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL18MAINQUESTION'"));
            rdMainQuestion18_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion18_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion19.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL19MAINQUESTION'"));
            rdMainQuestion19_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion19_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion20.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL20MAINQUESTION'"));
            rdMainQuestion20_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion20_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion21.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL21MAINQUESTION'"));
            rdMainQuestion21_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion21_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion22.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL22MAINQUESTION'"));
            rdMainQuestion22_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion22_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion23.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL23MAINQUESTION'"));
            rdMainQuestion23_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion23_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion24.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL24MAINQUESTION'"));
            rdMainQuestion24_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion24_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion25.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL25MAINQUESTION'"));
            rdMainQuestion25_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion25_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion26.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL26MAINQUESTION'"));
            rdMainQuestion26_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion26_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion27.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL27MAINQUESTION'"));
            rdMainQuestion27_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion27_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion28.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL28MAINQUESTION'"));
            rdMainQuestion28_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion28_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion29.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL29MAINQUESTION'"));
            rdMainQuestion29_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion29_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion30.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL30MAINQUESTION'"));
            rdMainQuestion30_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion30_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion31.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL31MAINQUESTION'"));
            rdMainQuestion31_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion31_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion32.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL32MAINQUESTION'"));
            rdMainQuestion32_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion32_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion33.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL33MAINQUESTION'"));
            rdMainQuestion33_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion33_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion34.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL34MAINQUESTION'"));
            rdMainQuestion34_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion34_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion35.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35MAINQUESTION'"));
            rdMainQuestion35_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion35_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion36.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL36MAINQUESTION'"));
            rdMainQuestion36_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion36_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion37.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL37MAINQUESTION'"));
            rdMainQuestion37_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion37_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion38.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL38MAINQUESTION'"));
            rdMainQuestion38_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion38_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion39.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL39MAINQUESTION'"));
            rdMainQuestion39_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion39_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion40.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL40MAINQUESTION'"));
            rdMainQuestion40_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion40_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion41.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL41MAINQUESTION'"));
            rdMainQuestion41_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion41_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion42.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL42MAINQUESTION'"));
            rdMainQuestion42_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion42_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblMainQuestion43.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL43MAINQUESTION'"));
            chkMainQuestion43Diabete.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL43DIABETES'"));
            chkMainQuestion43Cardiovascular.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL43CARDIOVASCULAR'"));
            chkMainQuestion43Mental.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL43MENTAL'"));
            lblMainQuestion44.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL44MAINQUESTION'"));
            rdMainQuestion44_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSNO'"));
            rdMainQuestion44_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17TO42ANSYES'"));
            lblRemark.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELREMARK'"));
            #endregion
        }
        private void loadData(string hn_no)
        {
            try
            {
                int[] bd;
                cdc = new InhCheckupDataContext();
                trn_ques_aviation queAvia = cdc.trn_ques_aviations.Where(x => x.trn_patient_regi.trn_patient.tpt_hn_no.Replace("-", "") == hn_no.Replace("-", ""))
                    .OrderByDescending(x => x.tpr_id).FirstOrDefault();
                if (queAvia == null)
                {
                    queAvia = new trn_ques_aviation();
                    trn_patient_regi tpt = cdc.trn_patient_regis.Where(x => x.trn_patient.tpt_hn_no.Replace("-", "") == hn_no.Replace("-", "")).FirstOrDefault();
                    if (tpt != null)
                    {
                        queAvia.tpr_id = tpt.tpr_id;
                        queAvia.tqa_place_exam = "BANGKOK CIVIL AEROMEDICAL CENTER";                        
                        queAvia.tqa_th_fullname = tpt.trn_patient.tpt_pre_name + " " + tpt.trn_patient.tpt_first_name + " " + tpt.trn_patient.tpt_last_name;
                        queAvia.tqa_th_nation = tpt.trn_patient.tpt_nation_desc;
                        queAvia.tqa_sex = tpt.trn_patient.tpt_gender;
                        queAvia.tqa_dob = tpt.trn_patient.tpt_dob;
                        if (tpt.trn_patient.tpt_dob != null)
                        {
                            bd = CalculateAge(Convert.ToDateTime(tpt.trn_patient.tpt_dob), DateTime.Now);
                            queAvia.tqa_age_yrs = bd[0];
                            queAvia.tqa_age_month = bd[1];
                        }
                        else
                        {
                            queAvia.tqa_age_yrs = null;
                            queAvia.tqa_age_month = null;
                        }
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript' language='javascript'>alert(" + "ไม่พบข้อมูล HN: "+ hn_no +" ใน EMR" + ");</script>");
                    }
                    queAvia.tqa_license_type = 'N';

                    cdc.trn_ques_aviations.InsertOnSubmit(queAvia);
                }

                Session["tpr_id"] = queAvia.tpr_id;
                
                #region Recommendation
                txtplace.Text = queAvia.tqa_place_exam;
                txtname_th.Text = queAvia.tqa_th_fullname;
                txtnation_th.Text = queAvia.tqa_th_nation;
                //Gender
                switch (queAvia.tqa_sex.ToString())
                {
                    case "M": rdGender_M.Checked = true; break;
                    case "F": rdGender_F.Checked = true; break;
                    default:
                        rdGender_M.Checked = false;
                        rdGender_F.Checked = false;
                        break;
                }                
                txtDOB.Text = string.Format("{0:dd/MM/yyyy}", (DateTime?)(queAvia.tqa_dob));
                //Birthday,Age
                if (queAvia.tqa_dob != null)
                {
                    bd = CalculateAge(Convert.ToDateTime(queAvia.tqa_dob), DateTime.Now);
                    txtAgeYear.Text = bd[0].ToString();
                    txtAgeMonth.Text = bd[1].ToString();
                }
                //Marital                
                switch (queAvia.tqa_marital.ToString())
                {
                    case "S": rdMarital_S.Checked = true; break;
                    case "M": rdMarital_M.Checked = true; break;
                    case "D": rdMarital_D.Checked = true; break;
                    case "P": rdMarital_P.Checked = true; break;
                    case "W": rdMarital_W.Checked = true; break;
                    default:
                        rdMarital_S.Checked = false;
                        rdMarital_M.Checked = false;
                        rdMarital_D.Checked = false;
                        rdMarital_P.Checked = false;
                        rdMarital_W.Checked = false;
                        break;
                }                
                //Aviation Type
                switch (queAvia.tqa_license_type.ToString())
                {
                    case "N": rdTOL_N.Checked = true; break;
                    case "R": rdTOL_R.Checked = true; break;
                    default:
                        rdTOL_N.Checked = false;
                        rdTOL_R.Checked = false;
                        break;
                }
                switch (queAvia.tqa_avia_type.ToString())
                {
                    case "A": rdoATPL.Checked = true; break;
                    case "P": rdoPPL.Checked = true; break;
                    case "T": rdoATC.Checked = true; break;
                    case "C": rdoCPL.Checked = true; break;
                    case "E": rdoPE.Checked = true; break;
                    case "S": rdoStudent.Checked = true; break;
                    case "O": rdoOther.Checked = true; break;
                    default:
                        {
                            rdoATPL.Checked = false;
                            rdoPPL.Checked = false;
                            rdoATC.Checked = false;
                            rdoCPL.Checked = false;
                            rdoPE.Checked = false;
                            rdoStudent.Checked = false;
                            rdoOther.Checked = false;
                            break;
                        }
                }
                txtOtherLicenseType.Text = queAvia.tqa_avia_oths;
                //
                txtLicenseNo.Text = queAvia.tqa_license_no;
                txtAddress_th.Text = queAvia.tqa_th_address;
                txtTel_th.Text = queAvia.tqa_th_moblie;
                txtOccupation_th.Text = queAvia.tqa_th_occupa;
                txtCompany_th.Text = queAvia.tqa_th_comp;
                txtComAddress_th.Text = queAvia.tqa_th_office;
                txtComTel_th.Text = queAvia.tqa_th_of_mobile;                
                //Contact Address
                switch (queAvia.tqa_cont_address.ToString())
                {
                    case "R": rdContactAdd_R.Checked = true; break;
                    case "C": rdContactAdd_C.Checked = true; break;
                    default:
                        rdContactAdd_R.Checked = false;
                        rdContactAdd_C.Checked = false;
                        break;
                }
                txtContactPerson.Text = queAvia.tqa_person_emer;
                txtContactTel.Text = queAvia.tqa_telep_emer;
                //Have been examined
                switch (queAvia.tqa_prev_examined.ToString())
                {
                    case "N": rdHaveBeenExam_No.Checked = true; break;
                    case "Y": rdHaveBeenExam_Yes.Checked = true; break;
                    default:
                        rdHaveBeenExam_No.Checked = false;
                        rdHaveBeenExam_Yes.Checked = false;
                        break;
                }
                if (rdHaveBeenExam_Yes.Checked)
                {
                    txtExamIfYes.Text = queAvia.tqa_prev_exam_loc;
                    txtDateHaveExam.Text = string.Format("{0:dd/MM/yyyy}", (DateTime?)(queAvia.tqa_prev_exam_date));
                }
                else
                {
                    txtExamIfYes.Text = string.Empty;
                    txtDateHaveExam.Text = string.Empty;
                }
                //Were You Declared
                switch (queAvia.tqa_prev_exam_deca.ToString())
                {
                    case "F": rdWereYouDeclared_F.Checked = true; break;
                    case "U": rdWereYouDeclared_U.Checked = true; break;
                    default:
                        rdWereYouDeclared_F.Checked = false;
                        rdWereYouDeclared_U.Checked = false;
                        break;
                }
                //Has Medical
                switch (queAvia.tqa_med_waiver.ToString())
                {
                    case "N": rdHasMedical_No.Checked = true; break;
                    case "Y": rdHasMedical_Yes.Checked = true; break;
                    default:
                        rdHasMedical_No.Checked = false;
                        rdHasMedical_Yes.Checked = false;
                        break;
                }
                if (rdHasMedical_Yes.Checked)
                {
                    txtIfYesMedical.Text = queAvia.tqa_waiver_spec;
                }
                else
                {
                    txtIfYesMedical.Text = string.Empty;
                }
                txtFlyTime.Text = queAvia.tqa_tot_fling_time.ToString();
                txtLast6Month.Text = queAvia.tqa_last_six_time.ToString();
                txtAircraftName.Text = queAvia.tqa_aircraft_name;
                //Aircraft Type
                if (Convert.ToBoolean(queAvia.tqa_aircraft_jet)) { chkJet.Checked = true; } else { chkJet.Checked = false; }
                if (Convert.ToBoolean(queAvia.tqa_aircraft_turbo)) { chkTurboProp.Checked = true; } else { chkTurboProp.Checked = false; }
                if (Convert.ToBoolean(queAvia.tqa_aircraft_heli)) { chkHelicopter.Checked = true; } else { chkHelicopter.Checked = false; }
                if (Convert.ToBoolean(queAvia.tqa_aircraft_piston)) { chkPiston.Checked = true; } else { chkPiston.Checked = false; }
                if (Convert.ToBoolean(queAvia.tqa_aircraft_other)) { chkOther.Checked = true; txtOtherAircraft.Text = queAvia.tqa_aircraft_oth; } else { chkOther.Checked = false; txtOtherAircraft.Text = string.Empty; }
                if (Convert.ToBoolean(queAvia.tqa_single_pilot)) { chkSinglePilot.Checked = true; } else { chkSinglePilot.Checked = false; }
                if (Convert.ToBoolean(queAvia.tqa_muti_pilot)) { chkMultiPilot.Checked = true; } else { chkMultiPilot.Checked = false; }
                //Smoking
                switch (queAvia.tqa_tot_fling_time.ToString())
                {
                    case "N": rdoSmokeNever.Checked = true; break;
                    case "S": rdoSmokeStopSince.Checked = true; break;
                    case "Y": rdoSmokeYes.Checked = true; break;
                    default:
                        {
                            rdoSmokeNever.Checked = false;
                            rdoSmokeStopSince.Checked = false;
                            rdoSmokeYes.Checked = false;
                            break;
                        }
                }
                txtSmokeStopSince.Text = queAvia.tqa_smoking_since;
                txtSmokeTobaccoType.Text = queAvia.tqa_smoking_type;
                txtSmokeAmount.Text = queAvia.tqa_smoking_amt;
                //Medication
                switch (queAvia.tqa_use_medicine.ToString())
                {
                    case "N": rdMedicationNo.Checked = true; break;
                    case "Y": rdMedicationYes.Checked = true; break;
                    default:
                        {
                            rdMedicationNo.Checked = false;
                            rdMedicationYes.Checked = false;
                            break;
                        }
                }
                txtMedicationIfYes.Text = queAvia.tqa_med_name;
                txtMedicationQuantity.Text = queAvia.tqa_med_amount;
                txtMedicationReason.Text = queAvia.tqa_med_reason;
                txtMedicationStartDate.Text = string.Format("{0:dd/MM/yyyy}", (DateTime?)queAvia.tqa_med_startdate);
                txtAlcohal.Text = queAvia.tqa_avg_alcohal;
                //Exercise
                switch (queAvia.tqa_m20_exercise.ToString())
                {
                    case "N": rdExercise_No.Checked = true; break;
                    case "Y": rdExercise_Yes.Checked = true; break;
                    default:
                        rdExercise_No.Checked = false;
                        rdExercise_Yes.Checked = false;
                        break;
                }
                #endregion
                #region MedicalHistory
                //MainQuestion17
                switch (queAvia.tqa_chis_freq.ToString())
                {
                    case "N": rdMainQuestion17_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion17_yes.Checked = true;
                            txtQuestion17Yes.Text = queAvia.tqa_chis_freq_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion17.Text + " ; " + txtQuestion17Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion17_no.Checked = false;
                            rdMainQuestion17_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion18
                switch (queAvia.tqa_chis_dizz.ToString())
                {
                    case "N": rdMainQuestion18_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion18_yes.Checked = true;
                            txtQuestion18Yes.Text = queAvia.tqa_chis_dizz_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion18.Text + " ; " + txtQuestion18Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion18_no.Checked = false;
                            rdMainQuestion18_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion19
                switch (queAvia.tqa_chis_unco.ToString())
                {
                    case "N": rdMainQuestion19_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion19_yes.Checked = true;
                            txtQuestion19Yes.Text = queAvia.tqa_chis_unco_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion19.Text + " ; " + txtQuestion19Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion19_no.Checked = false;
                            rdMainQuestion19_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion20
                switch (queAvia.tqa_chis_eyet.ToString())
                {
                    case "N": rdMainQuestion20_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion20_yes.Checked = true;
                            txtQuestion20Yes.Text = queAvia.tqa_chis_eyet_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion20.Text + " ; " + txtQuestion20Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion20_no.Checked = false;
                            rdMainQuestion20_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion21
                switch (queAvia.tqa_chis_hayf.ToString())
                {
                    case "N": rdMainQuestion21_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion21_yes.Checked = true;
                            txtQuestion21Yes.Text = queAvia.tqa_chis_hayf_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion21.Text + " ; " + txtQuestion21Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion21_no.Checked = false;
                            rdMainQuestion21_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion22
                switch (queAvia.tqa_chis_hert.ToString())
                {
                    case "N": rdMainQuestion22_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion22_yes.Checked = true;
                            txtQuestion22Yes.Text = queAvia.tqa_chis_hert_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion22.Text + " ; " + txtQuestion22Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion22_no.Checked = false;
                            rdMainQuestion22_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion23
                switch (queAvia.tqa_chis_chst.ToString())
                {
                    case "N": rdMainQuestion23_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion23_yes.Checked = true;
                            txtQuestion23Yes.Text = queAvia.tqa_chis_chst_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion23.Text + " ; " + txtQuestion23Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion23_no.Checked = false;
                            rdMainQuestion23_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion24
                switch (queAvia.tqa_chis_high.ToString())
                {
                    case "N": rdMainQuestion24_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion24_yes.Checked = true;
                            txtQuestion24Yes.Text = queAvia.tqa_chis_high_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion24.Text + " ; " + txtQuestion24Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion24_no.Checked = false;
                            rdMainQuestion24_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion25
                switch (queAvia.tqa_chis_stom.ToString())
                {
                    case "N": rdMainQuestion25_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion25_yes.Checked = true;
                            txtQuestion25Yes.Text = queAvia.tqa_chis_stom_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion25.Text + " ; " + txtQuestion25Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion25_no.Checked = false;
                            rdMainQuestion25_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion26
                switch (queAvia.tqa_chis_jaun.ToString())
                {
                    case "N": rdMainQuestion26_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion26_yes.Checked = true;
                            txtQuestion26Yes.Text = queAvia.tqa_chis_jaun_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion26.Text + " ; " + txtQuestion26Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion26_no.Checked = false;
                            rdMainQuestion26_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion27
                switch (queAvia.tqa_chis_kidn.ToString())
                {
                    case "N": rdMainQuestion27_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion27_yes.Checked = true;
                            txtQuestion27Yes.Text = queAvia.tqa_chis_kidn_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion27.Text + " ; " + txtQuestion27Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion27_no.Checked = false;
                            rdMainQuestion27_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion28
                switch (queAvia.tqa_chis_suga.ToString())
                {
                    case "N": rdMainQuestion28_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion28_yes.Checked = true;
                            txtQuestion28Yes.Text = queAvia.tqa_chis_suga_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion28.Text + " ; " + txtQuestion28Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion28_no.Checked = false;
                            rdMainQuestion28_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion29
                switch (queAvia.tqa_chis_epil.ToString())
                {
                    case "N": rdMainQuestion29_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion29_yes.Checked = true;
                            txtQuestion29Yes.Text = queAvia.tqa_chis_epil_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion29.Text + " ; " + txtQuestion29Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion29_no.Checked = false;
                            rdMainQuestion29_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion30
                switch (queAvia.tqa_chis_nurv.ToString())
                {
                    case "N": rdMainQuestion30_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion30_yes.Checked = true;
                            txtQuestion30Yes.Text = queAvia.tqa_chis_nurv_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion30.Text + " ; " + txtQuestion30Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion30_no.Checked = false;
                            rdMainQuestion30_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion31
                switch (queAvia.tqa_chis_temp.ToString())
                {
                    case "N": rdMainQuestion31_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion31_yes.Checked = true;
                            txtQuestion31Yes.Text = queAvia.tqa_chis_temp_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion31.Text + " ; " + txtQuestion31Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion31_no.Checked = false;
                            rdMainQuestion31_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion32
                switch (queAvia.tqa_chis_drug.ToString())
                {
                    case "N": rdMainQuestion32_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion32_yes.Checked = true;
                            txtQuestion32Yes.Text = queAvia.tqa_chis_drug_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion32.Text + " ; " + txtQuestion32Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion32_no.Checked = false;
                            rdMainQuestion32_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion33
                switch (queAvia.tqa_chis_suic.ToString())
                {
                    case "N": rdMainQuestion33_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion33_yes.Checked = true;
                            txtQuestion33Yes.Text = queAvia.tqa_chis_suic_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion33.Text + " ; " + txtQuestion33Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion33_no.Checked = false;
                            rdMainQuestion33_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion34
                switch (queAvia.tqa_chis_losw.ToString())
                {
                    case "N": rdMainQuestion34_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion34_yes.Checked = true;
                            txtQuestion34Yes.Text = queAvia.tqa_chis_losw_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion34.Text + " ; " + txtQuestion34Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion34_no.Checked = false;
                            rdMainQuestion34_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion35
                switch (queAvia.tqa_chis_moti.ToString())
                {
                    case "N": rdMainQuestion35_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion35_yes.Checked = true;
                            txtQuestion35Yes.Text = queAvia.tqa_chis_moti_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion35.Text + " ; " + txtQuestion35Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion35_no.Checked = false;
                            rdMainQuestion35_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion36
                switch (queAvia.tqa_chis_reje.ToString())
                {
                    case "N": rdMainQuestion36_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion36_yes.Checked = true;
                            txtQuestion36Yes.Text = queAvia.tqa_chis_reje_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion36.Text + " ; " + txtQuestion36Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion36_no.Checked = false;
                            rdMainQuestion36_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion37
                switch (queAvia.tqa_chis_adms.ToString())
                {
                    case "N": rdMainQuestion37_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion37_yes.Checked = true;
                            txtQuestion37Yes.Text = queAvia.tqa_chis_adms_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion37.Text + " ; " + txtQuestion37Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion37_no.Checked = false;
                            rdMainQuestion37_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion38
                switch (queAvia.tqa_chis_avia.ToString())
                {
                    case "N": rdMainQuestion38_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion38_yes.Checked = true;
                            txtQuestion38Yes.Text = queAvia.tqa_chis_avia_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion38.Text + " ; " + txtQuestion38Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion38_no.Checked = false;
                            rdMainQuestion38_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion39
                switch (queAvia.tqa_chis_otha.ToString())
                {
                    case "N": rdMainQuestion39_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion39_yes.Checked = true;
                            txtQuestion39Yes.Text = queAvia.tqa_chis_otha_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion39.Text + " ; " + txtQuestion39Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion39_no.Checked = false;
                            rdMainQuestion39_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion40
                switch (queAvia.tqa_chis_gyna.ToString())
                {
                    case "N": rdMainQuestion40_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion40_yes.Checked = true;
                            txtQuestion21Yes.Text = queAvia.tqa_chis_gyna_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion40.Text + " ; " + txtQuestion40Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion40_no.Checked = false;
                            rdMainQuestion40_yes.Checked = false;
                            break;
                        }
                }                
                //MainQuestion41
                switch (queAvia.tqa_chis_othi.ToString())
                {
                    case "N": rdMainQuestion41_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion41_yes.Checked = true;
                            txtQuestion41Yes.Text = queAvia.tqa_chis_othi_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion41.Text + " ; " + txtQuestion41Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion41_no.Checked = false;
                            rdMainQuestion41_yes.Checked = false;
                            break;
                        }
                }
                
                //MainQuestion42
                switch (queAvia.tqa_chis_heth.ToString())
                {
                    case "N": rdMainQuestion42_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion42_yes.Checked = true;
                            txtQuestion42Yes.Text = queAvia.tqa_chis_heth_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion42.Text + " ; " + txtQuestion42Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion42_no.Checked = false;
                            rdMainQuestion42_yes.Checked = false;
                            break;
                        }
                }                
                //43
                if (Convert.ToBoolean(queAvia.tqa_chis_fam_diab)) { chkMainQuestion43Diabete.Checked = true; } else { chkMainQuestion43Diabete.Checked = false; }
                if (Convert.ToBoolean(queAvia.tqa_chis_fam_card)) { chkMainQuestion43Cardiovascular.Checked = true; } else { chkMainQuestion43Cardiovascular.Checked = false; }
                if (Convert.ToBoolean(queAvia.tqa_chis_fam_ment)) { chkMainQuestion43Mental.Checked = true; } else { chkMainQuestion43Mental.Checked = false; }
                if ((chkMainQuestion43Diabete.Checked) || (chkMainQuestion43Cardiovascular.Checked) || (chkMainQuestion43Mental.Checked))
                {
                    tmpRemark += "\r\n" + lblMainQuestion43.Text + " ; ";
                    if (chkMainQuestion43Diabete.Checked) { tmpRemark += chkMainQuestion43Diabete.Text + ", "; }
                    if (chkMainQuestion43Cardiovascular.Checked) { tmpRemark += chkMainQuestion43Cardiovascular.Text + ", "; }
                    if (chkMainQuestion43Mental.Checked) { tmpRemark += chkMainQuestion43Mental.Text + ", "; }
                    tmpRemark = tmpRemark.Remove(tmpRemark.Count()-2,2);
                }
                //MainQuestion44
                switch (queAvia.tqa_chis_conviction.ToString())
                {
                    case "N": rdMainQuestion44_no.Checked = true; break;
                    case "Y":
                        { 
                            rdMainQuestion44_yes.Checked = true;
                            txtQuestion44Yes.Text = queAvia.tqa_chis_conv_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion44.Text + " ; " + txtQuestion44Yes.Text;
                            break; 
                        }
                    default:
                        {
                            rdMainQuestion44_no.Checked = false;
                            rdMainQuestion44_yes.Checked = false;
                            break;
                        }
                }                
                #endregion                
                Session["remark"] = tmpRemark;
                txtRemark.Text = queAvia.tqa_remark + Session["remark"].ToString();

                if (rdGender_M.Checked)
                {
                    lblMainQuestion40.Enabled = false;
                    rdMainQuestion40_no.Enabled = false;
                    rdMainQuestion40_yes.Enabled = false;
                    txtQuestion40Yes.Enabled = false;
                }
                else
                {
                    lblMainQuestion40.Enabled = true;
                    rdMainQuestion40_no.Enabled = true;
                    rdMainQuestion40_yes.Enabled = true;
                    txtQuestion40Yes.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void Save(char doctype)
        {
            try
            {
                currentDateTime = DateTime.Now;
                cdc = new InhCheckupDataContext();
                trn_ques_aviation queAvia = cdc.trn_ques_aviations.Where(x => x.tpr_id == Convert.ToInt32(Session["tpr_id"])).FirstOrDefault();

                if (queAvia == null)
                {
                    queAvia = new trn_ques_aviation();
                    queAvia.tqa_create_by = "System";
                    queAvia.tqa_create_date = currentDateTime;

                    cdc.trn_ques_aviations.InsertOnSubmit(queAvia);
                }

                queAvia.tpr_id = Convert.ToInt32(Session["tpr_id"]);

                #region Recommendation
                queAvia.tqa_type = doctype;                
                //License
                bool chkLicense = false;
                if (rdoATPL.Checked) { queAvia.tqa_avia_type = 'A'; chkLicense = true; }
                if (rdoPPL.Checked) { queAvia.tqa_avia_type = 'P'; chkLicense = true; }
                if (rdoATC.Checked) { queAvia.tqa_avia_type = 'T'; chkLicense = true; }
                if (rdoCPL.Checked) { queAvia.tqa_avia_type = 'C'; chkLicense = true; }
                if (rdoPE.Checked) { queAvia.tqa_avia_type = 'E'; chkLicense = true; }
                if (rdoStudent.Checked) { queAvia.tqa_avia_type = 'S'; chkLicense = true; }
                if (rdoOther.Checked) { queAvia.tqa_avia_type = 'O'; chkLicense = true; }
                if (!chkLicense) { queAvia.tqa_avia_type = (char?)null; }
                queAvia.tqa_avia_oths = string.IsNullOrEmpty(txtOtherLicenseType.Text) ? null : txtOtherLicenseType.Text;
                queAvia.tqa_place_exam = string.IsNullOrEmpty(txtplace.Text) ? null : txtplace.Text;
                queAvia.tqa_th_fullname = string.IsNullOrEmpty(txtname_th.Text) ? null : txtname_th.Text;
                queAvia.tqa_th_nation = string.IsNullOrEmpty(txtnation_th.Text) ? null : txtnation_th.Text;
                if (rdGender_M.Checked) { queAvia.tqa_sex = 'M'; } else if (rdGender_F.Checked) { queAvia.tqa_sex = 'F'; } else { queAvia.tqa_sex = (char?)null; }
                queAvia.tqa_dob = string.IsNullOrEmpty(txtDOB.Text) ? (DateTime?)null : DateTime.Parse(ut.ConvertDateToStringFormat(txtDOB.Text.Trim(),"yyyy-MM-dd"));
                queAvia.tqa_age_yrs = string.IsNullOrEmpty(txtAgeYear.Text) ? (float?)null : float.Parse(txtAgeYear.Text);
                queAvia.tqa_age_month = string.IsNullOrEmpty(txtAgeMonth.Text) ? (float?)null : float.Parse(txtAgeMonth.Text);
                //Marital
                bool chkMarital = false;
                if (rdMarital_S.Checked) { queAvia.tqa_marital = 'S'; chkMarital = true; }
                if (rdMarital_M.Checked) { queAvia.tqa_marital = 'M'; chkMarital = true; }
                if (rdMarital_D.Checked) { queAvia.tqa_marital = 'D'; chkMarital = true; }
                if (rdMarital_P.Checked) { queAvia.tqa_marital = 'P'; chkMarital = true; }
                if (rdMarital_W.Checked) { queAvia.tqa_marital = 'W'; chkMarital = true; }
                if (!chkMarital) { queAvia.tqa_marital = (char?)null; }
                //TOL
                if (rdTOL_N.Checked) { queAvia.tqa_license_type = 'N'; } else if (rdTOL_R.Checked) { queAvia.tqa_license_type = 'R'; } else { queAvia.tqa_license_type = (char?)null; }
                queAvia.tqa_license_no = string.IsNullOrEmpty(txtLicenseNo.Text) ? null : txtLicenseNo.Text;
                queAvia.tqa_th_address = string.IsNullOrEmpty(txtAddress_th.Text) ? null : txtAddress_th.Text;
                queAvia.tqa_th_moblie = string.IsNullOrEmpty(txtTel_th.Text) ? null : txtTel_th.Text;
                queAvia.tqa_th_occupa = string.IsNullOrEmpty(txtOccupation_th.Text) ? null : txtOccupation_th.Text;
                queAvia.tqa_th_comp = string.IsNullOrEmpty(txtCompany_th.Text) ? null : txtCompany_th.Text;
                queAvia.tqa_th_office = string.IsNullOrEmpty(txtComAddress_th.Text) ? null : txtComAddress_th.Text;
                queAvia.tqa_th_of_mobile = string.IsNullOrEmpty(txtComTel_th.Text) ? null : txtComTel_th.Text;
                //Contact Address
                if (rdContactAdd_R.Checked) { queAvia.tqa_cont_address = 'R'; } else if (rdContactAdd_C.Checked) { queAvia.tqa_cont_address = 'C'; } else { queAvia.tqa_cont_address = (char?)null; }
                queAvia.tqa_person_emer = string.IsNullOrEmpty(txtContactPerson.Text) ? null : txtContactPerson.Text;
                queAvia.tqa_telep_emer = string.IsNullOrEmpty(txtContactTel.Text) ? null : txtContactTel.Text;
                //Have been examined
                if (rdHaveBeenExam_No.Checked) { queAvia.tqa_prev_examined = 'N'; } else if (rdHaveBeenExam_Yes.Checked) { queAvia.tqa_prev_examined = 'Y'; } else { queAvia.tqa_prev_examined = (char?)null; }
                queAvia.tqa_prev_exam_loc = string.IsNullOrEmpty(txtExamIfYes.Text) ? null : txtExamIfYes.Text;
                queAvia.tqa_prev_exam_date = string.IsNullOrEmpty(txtDateHaveExam.Text) ? (DateTime?)null : DateTime.Parse(ut.ConvertDateToStringFormat(txtDateHaveExam.Text.Trim(), "yyyy-MM-dd"));
                //Were you declared
                if (rdWereYouDeclared_F.Checked) { queAvia.tqa_prev_exam_deca = 'F'; } else if (rdWereYouDeclared_U.Checked) { queAvia.tqa_prev_exam_deca = 'U'; } else { queAvia.tqa_prev_exam_deca = (char?)null; }
                //Has medical
                if (rdHasMedical_No.Checked) { queAvia.tqa_med_waiver = 'N'; } else if (rdHasMedical_Yes.Checked) { queAvia.tqa_med_waiver = 'Y'; } else { queAvia.tqa_med_waiver = (char?)null; }
                queAvia.tqa_waiver_spec = string.IsNullOrEmpty(txtIfYesMedical.Text) ? null : txtIfYesMedical.Text;
                queAvia.tqa_tot_fling_time = string.IsNullOrEmpty(txtFlyTime.Text) ? (float?)null : float.Parse(txtFlyTime.Text);
                queAvia.tqa_last_six_time = string.IsNullOrEmpty(txtLast6Month.Text) ? (float?)null : float.Parse(txtLast6Month.Text);
                queAvia.tqa_aircraft_name = string.IsNullOrEmpty(txtAircraftName.Text) ? null : txtAircraftName.Text;
                if (chkJet.Checked) { queAvia.tqa_aircraft_jet = true; } else { queAvia.tqa_aircraft_jet = false; }
                if (chkTurboProp.Checked) { queAvia.tqa_aircraft_turbo = true; } else { queAvia.tqa_aircraft_turbo = false; }
                if (chkHelicopter.Checked) { queAvia.tqa_aircraft_heli = true; } else { queAvia.tqa_aircraft_heli = false; }
                if (chkPiston.Checked) { queAvia.tqa_aircraft_piston = true; } else { queAvia.tqa_aircraft_piston = false; }
                if (chkOther.Checked) { queAvia.tqa_aircraft_other = true; } else { queAvia.tqa_aircraft_other = false; }
                queAvia.tqa_aircraft_oth = string.IsNullOrEmpty(txtOtherAircraft.Text) ? null : txtOtherAircraft.Text;
                queAvia.tqa_flying_status = null;
                if (chkSinglePilot.Checked) { queAvia.tqa_single_pilot = true; } else { queAvia.tqa_single_pilot = false; }
                if (chkMultiPilot.Checked) { queAvia.tqa_muti_pilot = true; } else { queAvia.tqa_muti_pilot = false; }
                bool chkSmoke = false;
                if (rdoSmokeNever.Checked) { queAvia.tqa_smoking = 'N'; chkSmoke = true; }
                if (rdoSmokeStopSince.Checked) { queAvia.tqa_smoking = 'S'; chkSmoke = true; }
                if (rdoSmokeYes.Checked) { queAvia.tqa_smoking = 'Y'; chkSmoke = true; }
                if (!chkSmoke) { queAvia.tqa_smoking = (char?)null; }
                queAvia.tqa_smoking_since = string.IsNullOrEmpty(txtSmokeStopSince.Text) ? null : txtSmokeStopSince.Text;
                queAvia.tqa_smoking_type = string.IsNullOrEmpty(txtSmokeTobaccoType.Text) ? null : txtSmokeTobaccoType.Text;
                queAvia.tqa_smoking_amt = string.IsNullOrEmpty(txtSmokeAmount.Text) ? null : txtSmokeAmount.Text;
                bool chkMedicine = false;
                if (rdMedicationNo.Checked) { queAvia.tqa_use_medicine = 'N'; chkMedicine = true; }
                if (rdMedicationYes.Checked) { queAvia.tqa_use_medicine = 'Y'; chkMedicine = true; }
                if (!chkMedicine) { queAvia.tqa_use_medicine = (char?)null; }
                queAvia.tqa_med_name = string.IsNullOrEmpty(txtMedicationIfYes.Text) ? null : txtMedicationIfYes.Text;
                queAvia.tqa_med_amount = string.IsNullOrEmpty(txtMedicationQuantity.Text) ? null : txtMedicationQuantity.Text;
                queAvia.tqa_med_startdate = string.IsNullOrEmpty(txtMedicationStartDate.Text) ? (DateTime?)null : DateTime.Parse(ut.ConvertDateToStringFormat(txtMedicationStartDate.Text.Trim(), "yyyy-MM-dd"));
                queAvia.tqa_med_reason = string.IsNullOrEmpty(txtMedicationReason.Text) ? null : txtMedicationReason.Text;
                queAvia.tqa_avg_alcohal = string.IsNullOrEmpty(txtAlcohal.Text) ? null : txtAlcohal.Text;
                //Exercise
                if (rdExercise_No.Checked) { queAvia.tqa_m20_exercise = 'N'; } else if (rdExercise_Yes.Checked) { queAvia.tqa_m20_exercise = 'Y'; } else { queAvia.tqa_m20_exercise = (char?)null; }
                #endregion
                #region MedicalHistory
                if (rdMainQuestion17_no.Checked) { queAvia.tqa_chis_freq = 'N'; } else if (rdMainQuestion17_yes.Checked) { queAvia.tqa_chis_freq = 'Y'; tmpRemark += "\r\n" + lblMainQuestion17.Text + " ; " + txtQuestion17Yes.Text; } else { queAvia.tqa_chis_freq = (char?)null; } //17
                queAvia.tqa_chis_freq_rmk = txtQuestion17Yes.Text;
                if (rdMainQuestion18_no.Checked) { queAvia.tqa_chis_dizz = 'N'; } else if (rdMainQuestion18_yes.Checked) { queAvia.tqa_chis_dizz = 'Y'; tmpRemark += "\r\n" + lblMainQuestion18.Text + " ; " + txtQuestion18Yes.Text; } else { queAvia.tqa_chis_dizz = (char?)null; } //18
                queAvia.tqa_chis_dizz_rmk = txtQuestion18Yes.Text;
                if (rdMainQuestion19_no.Checked) { queAvia.tqa_chis_unco = 'N'; } else if (rdMainQuestion19_yes.Checked) { queAvia.tqa_chis_unco = 'Y'; tmpRemark += "\r\n" + lblMainQuestion19.Text + " ; " + txtQuestion19Yes.Text; } else { queAvia.tqa_chis_unco = (char?)null; } //19
                queAvia.tqa_chis_unco_rmk = txtQuestion19Yes.Text;
                if (rdMainQuestion20_no.Checked) { queAvia.tqa_chis_eyet = 'N'; } else if (rdMainQuestion20_yes.Checked) { queAvia.tqa_chis_eyet = 'Y'; tmpRemark += "\r\n" + lblMainQuestion20.Text + " ; " + txtQuestion20Yes.Text; } else { queAvia.tqa_chis_eyet = (char?)null; } //20
                queAvia.tqa_chis_eyet_rmk = txtQuestion20Yes.Text;
                if (rdMainQuestion21_no.Checked) { queAvia.tqa_chis_hayf = 'N'; } else if (rdMainQuestion21_yes.Checked) { queAvia.tqa_chis_hayf = 'Y'; tmpRemark += "\r\n" + lblMainQuestion21.Text + " ; " + txtQuestion21Yes.Text; } else { queAvia.tqa_chis_hayf = (char?)null; } //21
                queAvia.tqa_chis_hayf_rmk = txtQuestion21Yes.Text;
                if (rdMainQuestion22_no.Checked) { queAvia.tqa_chis_hert = 'N'; } else if (rdMainQuestion22_yes.Checked) { queAvia.tqa_chis_hert = 'Y'; tmpRemark += "\r\n" + lblMainQuestion22.Text + " ; " + txtQuestion22Yes.Text; } else { queAvia.tqa_chis_hert = (char?)null; } //22
                queAvia.tqa_chis_hert_rmk = txtQuestion22Yes.Text;
                if (rdMainQuestion23_no.Checked) { queAvia.tqa_chis_chst = 'N'; } else if (rdMainQuestion23_yes.Checked) { queAvia.tqa_chis_chst = 'Y'; tmpRemark += "\r\n" + lblMainQuestion23.Text + " ; " + txtQuestion23Yes.Text; } else { queAvia.tqa_chis_chst = (char?)null; } //23
                queAvia.tqa_chis_chst_rmk = txtQuestion23Yes.Text;
                if (rdMainQuestion24_no.Checked) { queAvia.tqa_chis_high = 'N'; } else if (rdMainQuestion24_yes.Checked) { queAvia.tqa_chis_high = 'Y'; tmpRemark += "\r\n" + lblMainQuestion24.Text + " ; " + txtQuestion24Yes.Text; } else { queAvia.tqa_chis_high = (char?)null; } //24
                queAvia.tqa_chis_high_rmk = txtQuestion24Yes.Text;
                if (rdMainQuestion25_no.Checked) { queAvia.tqa_chis_stom = 'N'; } else if (rdMainQuestion25_yes.Checked) { queAvia.tqa_chis_stom = 'Y'; tmpRemark += "\r\n" + lblMainQuestion25.Text + " ; " + txtQuestion25Yes.Text; } else { queAvia.tqa_chis_stom = (char?)null; } //25
                queAvia.tqa_chis_stom_rmk = txtQuestion25Yes.Text;
                if (rdMainQuestion26_no.Checked) { queAvia.tqa_chis_jaun = 'N'; } else if (rdMainQuestion26_yes.Checked) { queAvia.tqa_chis_jaun = 'Y'; tmpRemark += "\r\n" + lblMainQuestion26.Text + " ; " + txtQuestion26Yes.Text; } else { queAvia.tqa_chis_jaun = (char?)null; } //26
                queAvia.tqa_chis_jaun_rmk = txtQuestion26Yes.Text;
                if (rdMainQuestion27_no.Checked) { queAvia.tqa_chis_kidn = 'N'; } else if (rdMainQuestion27_yes.Checked) { queAvia.tqa_chis_kidn = 'Y'; tmpRemark += "\r\n" + lblMainQuestion27.Text + " ; " + txtQuestion27Yes.Text; } else { queAvia.tqa_chis_kidn = (char?)null; } //27
                queAvia.tqa_chis_kidn_rmk = txtQuestion27Yes.Text;
                if (rdMainQuestion28_no.Checked) { queAvia.tqa_chis_suga = 'N'; } else if (rdMainQuestion28_yes.Checked) { queAvia.tqa_chis_suga = 'Y'; tmpRemark += "\r\n" + lblMainQuestion28.Text + " ; " + txtQuestion28Yes.Text; } else { queAvia.tqa_chis_suga = (char?)null; } //28
                queAvia.tqa_chis_suga_rmk = txtQuestion28Yes.Text;
                if (rdMainQuestion29_no.Checked) { queAvia.tqa_chis_epil = 'N'; } else if (rdMainQuestion29_yes.Checked) { queAvia.tqa_chis_epil = 'Y'; tmpRemark += "\r\n" + lblMainQuestion29.Text + " ; " + txtQuestion29Yes.Text; } else { queAvia.tqa_chis_epil = (char?)null; } //29
                queAvia.tqa_chis_epil_rmk = txtQuestion29Yes.Text;
                if (rdMainQuestion30_no.Checked) { queAvia.tqa_chis_nurv = 'N'; } else if (rdMainQuestion30_yes.Checked) { queAvia.tqa_chis_nurv = 'Y'; tmpRemark += "\r\n" + lblMainQuestion30.Text + " ; " + txtQuestion30Yes.Text; } else { queAvia.tqa_chis_nurv = (char?)null; } //30
                queAvia.tqa_chis_nurv_rmk = txtQuestion30Yes.Text;
                if (rdMainQuestion31_no.Checked) { queAvia.tqa_chis_temp = 'N'; } else if (rdMainQuestion31_yes.Checked) { queAvia.tqa_chis_temp = 'Y'; tmpRemark += "\r\n" + lblMainQuestion31.Text + " ; " + txtQuestion31Yes.Text; } else { queAvia.tqa_chis_temp = (char?)null; } //31
                queAvia.tqa_chis_temp_rmk = txtQuestion31Yes.Text;
                if (rdMainQuestion32_no.Checked) { queAvia.tqa_chis_drug = 'N'; } else if (rdMainQuestion32_yes.Checked) { queAvia.tqa_chis_drug = 'Y'; tmpRemark += "\r\n" + lblMainQuestion32.Text + " ; " + txtQuestion32Yes.Text; } else { queAvia.tqa_chis_drug = (char?)null; } //32
                queAvia.tqa_chis_drug_rmk = txtQuestion32Yes.Text;
                if (rdMainQuestion33_no.Checked) { queAvia.tqa_chis_suic = 'N'; } else if (rdMainQuestion33_yes.Checked) { queAvia.tqa_chis_suic = 'Y'; tmpRemark += "\r\n" + lblMainQuestion33.Text + " ; " + txtQuestion33Yes.Text; } else { queAvia.tqa_chis_suic = (char?)null; } //33
                queAvia.tqa_chis_suic_rmk = txtQuestion33Yes.Text;
                if (rdMainQuestion34_no.Checked) { queAvia.tqa_chis_losw = 'N'; } else if (rdMainQuestion34_yes.Checked) { queAvia.tqa_chis_losw = 'Y'; tmpRemark += "\r\n" + lblMainQuestion34.Text + " ; " + txtQuestion34Yes.Text; } else { queAvia.tqa_chis_losw = (char?)null; } //34
                queAvia.tqa_chis_losw_rmk = txtQuestion34Yes.Text;
                if (rdMainQuestion35_no.Checked) { queAvia.tqa_chis_moti = 'N'; } else if (rdMainQuestion35_yes.Checked) { queAvia.tqa_chis_moti = 'Y'; tmpRemark += "\r\n" + lblMainQuestion35.Text + " ; " + txtQuestion35Yes.Text; } else { queAvia.tqa_chis_moti = (char?)null; } //35
                queAvia.tqa_chis_moti_rmk = txtQuestion35Yes.Text;
                if (rdMainQuestion36_no.Checked) { queAvia.tqa_chis_reje = 'N'; } else if (rdMainQuestion36_yes.Checked) { queAvia.tqa_chis_reje = 'Y'; tmpRemark += "\r\n" + lblMainQuestion36.Text + " ; " + txtQuestion36Yes.Text; } else { queAvia.tqa_chis_reje = (char?)null; } //36
                queAvia.tqa_chis_reje_rmk = txtQuestion36Yes.Text;
                if (rdMainQuestion37_no.Checked) { queAvia.tqa_chis_adms = 'N'; } else if (rdMainQuestion37_yes.Checked) { queAvia.tqa_chis_adms = 'Y'; tmpRemark += "\r\n" + lblMainQuestion37.Text + " ; " + txtQuestion37Yes.Text; } else { queAvia.tqa_chis_adms = (char?)null; } //37
                queAvia.tqa_chis_adms_rmk = txtQuestion37Yes.Text;
                if (rdMainQuestion38_no.Checked) { queAvia.tqa_chis_avia = 'N'; } else if (rdMainQuestion38_yes.Checked) { queAvia.tqa_chis_avia = 'Y'; tmpRemark += "\r\n" + lblMainQuestion38.Text + " ; " + txtQuestion38Yes.Text; } else { queAvia.tqa_chis_avia = (char?)null; } //38
                queAvia.tqa_chis_avia_rmk = txtQuestion38Yes.Text;
                if (rdMainQuestion39_no.Checked) { queAvia.tqa_chis_otha = 'N'; } else if (rdMainQuestion39_yes.Checked) { queAvia.tqa_chis_otha = 'Y'; tmpRemark += "\r\n" + lblMainQuestion39.Text + " ; " + txtQuestion39Yes.Text; } else { queAvia.tqa_chis_otha = (char?)null; } //39
                queAvia.tqa_chis_otha_rmk = txtQuestion39Yes.Text;
                if (rdMainQuestion40_no.Checked) { queAvia.tqa_chis_gyna = 'N'; } else if (rdMainQuestion40_yes.Checked) { queAvia.tqa_chis_gyna = 'Y'; tmpRemark += "\r\n" + lblMainQuestion40.Text + " ; " + txtQuestion40Yes.Text; } else { queAvia.tqa_chis_gyna = (char?)null; } //40
                queAvia.tqa_chis_gyna_rmk = txtQuestion40Yes.Text;
                if (rdMainQuestion41_no.Checked) { queAvia.tqa_chis_othi = 'N'; } else if (rdMainQuestion41_yes.Checked) { queAvia.tqa_chis_othi = 'Y'; tmpRemark += "\r\n" + lblMainQuestion41.Text + " ; " + txtQuestion41Yes.Text; } else { queAvia.tqa_chis_othi = (char?)null; } //41
                queAvia.tqa_chis_othi_rmk = txtQuestion41Yes.Text;
                if (rdMainQuestion42_no.Checked) { queAvia.tqa_chis_heth = 'N'; } else if (rdMainQuestion42_yes.Checked) { queAvia.tqa_chis_heth = 'Y'; tmpRemark += "\r\n" + lblMainQuestion42.Text + " ; " + txtQuestion42Yes.Text; } else { queAvia.tqa_chis_heth = (char?)null; } //42
                queAvia.tqa_chis_heth_rmk = txtQuestion42Yes.Text;
                if ((chkMainQuestion43Diabete.Checked) || (chkMainQuestion43Cardiovascular.Checked) || (chkMainQuestion43Mental.Checked))
                {
                    tmpRemark += "\r\n" + lblMainQuestion43.Text + " ; ";
                    if (chkMainQuestion43Diabete.Checked) { queAvia.tqa_chis_fam_diab = true; tmpRemark += chkMainQuestion43Diabete.Text + ", "; } else { queAvia.tqa_chis_fam_diab = false; }
                    if (chkMainQuestion43Cardiovascular.Checked) { queAvia.tqa_chis_fam_card = true; tmpRemark += chkMainQuestion43Cardiovascular.Text + ", "; } else { queAvia.tqa_chis_fam_card = false; }
                    if (chkMainQuestion43Mental.Checked) { queAvia.tqa_chis_fam_ment = true; tmpRemark += chkMainQuestion43Mental.Text + ", "; } else { queAvia.tqa_chis_fam_ment = false; }
                    tmpRemark = tmpRemark.Remove(tmpRemark.Count()-2,2);
                }
                if (rdMainQuestion44_no.Checked) { queAvia.tqa_chis_conviction = 'N'; } else if (rdMainQuestion44_yes.Checked) { queAvia.tqa_chis_conviction = 'Y'; tmpRemark += "\r\n" + lblMainQuestion44.Text + " ; " + txtQuestion44Yes.Text; } else { queAvia.tqa_chis_conviction = (char?)null; } //44
                queAvia.tqa_chis_conv_rmk = txtQuestion44Yes.Text;
                #endregion      
                if (txtRemark.Text.Count() > 0) { txtRemark.Text = txtRemark.Text.Replace(Session["remark"].ToString(), ""); }
                queAvia.tqa_remark = txtRemark.Text;
                Session["remark"] = tmpRemark;

                queAvia.tqa_update_by = "System";
                queAvia.tqa_update_date = DateTime.Now;
                try
                {
                    cdc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in cdc.ChangeConflicts)
                    {
                        cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    cdc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            loadData(Session["HN"].ToString());
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

        private int[] CalculateAge(DateTime startDate, DateTime endDate)
        {
            int[] birthday = { 0, 0 };
            if (startDate == null || endDate == null)
            {
                return birthday;
            }
            if (startDate.Date > endDate.Date)
            {
                throw new ArgumentException("startDate cannot be higher then endDate", "startDate");
            }
            int years = endDate.Year - startDate.Year;
            int months = 0;
            int days = 0;

            // Check if the last year, was a full year.
            if (endDate < startDate.AddYears(years) && years != 0)
            { years--; }

            // Calculate the number of months.
            startDate = startDate.AddYears(years);
            if (startDate.Year == endDate.Year)
            { months = endDate.Month - startDate.Month; }
            else
            { months = (12 - startDate.Month) + endDate.Month; }

            // Check if last month was a complete month.
            if (endDate < startDate.AddMonths(months) && months != 0)
            { months--; }

            // Calculate the number of days.
            startDate = startDate.AddMonths(months);
            days = (endDate - startDate).Days;

            birthday[0] = years;
            birthday[1] = months;
            return birthday;
        }

        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            Save('D');
            txtRemark.Text += Session["remark"].ToString();
            //Response.Write("<script language='javascript'>alert(Save Draft Completed.);</script>");
            //ClientScript.RegisterOnSubmitStatement(this.GetType(), "Save Data", "<script language='javascript'>alert(Save Draft Completed.);</script>");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save('N');
            //Response.Write("<script language='javascript'>alert(Save Completed.);</script>");
            Response.Redirect("~/web/frm_verify_result.aspx");
        }
    }
}
