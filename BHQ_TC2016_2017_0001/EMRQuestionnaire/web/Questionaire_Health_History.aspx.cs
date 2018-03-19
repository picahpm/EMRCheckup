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

namespace EMRQuestionnaire.web
{
    public partial class Questionaire_Health_History : System.Web.UI.Page
    {
        executeDC clsEX = new executeDC();
        DataTable dtMasLabel = new DataTable();
        Utility ut = new Utility();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["HN"].ToString() == string.Empty)
            {
                Response.Redirect("~/default.aspx");
            }

            Session["LEGION"] = "EN";
            Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));
      
            loadLabel();

        }
        protected void loadLabel()
        {
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_EMR_PRESENT_HISTORY");
            lblgrpWorkingHistory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_01'"));
            lblSmoking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_02'"));
            rdoNonSmoking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_03'"));
            rdoNonSmokingButBeSecondHand.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_04'"));
            lblNonFor.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_25'"));
            lblYearNonSmoking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_07'"));
            rdoOutSmoking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_05'"));
            lblSpecify.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_06'"));
            lblSpecifyYear.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_07'"));
            rdoSmoke.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_08'"));
            lblAmount.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_09'"));
            lblAmountPerDay.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_10'"));
            lblDuration.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_11'"));
            lblDurationYear.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_07'"));

            lblAlcohol.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_12'"));
            rdoNoneAlcohol.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_13'"));
            rdoQuitAlcohol.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_14'"));
            lblQuitAlcoholYear.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_15'"));
            rdoSocialDrink.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_16'"));

            rdoSocialDrinkOnePerMonth.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_17'"));
            rdoSocialDrinkTwoPerMonth.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_18'"));
            rdoSocialDrinkThreePerMonth.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_19'"));
            rdoDrinkFourPerWeek.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_20'"));

            lblExercise.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_21'"));
            rdoNoneExercise.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_22'"));
            rdoLessThanHoursExercise.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_23'"));
            rdoRegularExercise.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'present_24'"));


            #endregion

            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_EMR_IILESS");
            lblGrpPresentIlless.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_0'"));
            lblPresentConcern.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_01'"));
            rdoAnnualCheckUp.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_02'"));
            rdoAnnualCheckUpOthers.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_03'"));

            lblPhycho.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_04'"));
            rdoNonePhycho.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_05'"));
            rdoPhychoOthers.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_06'"));

            lblMedicalHistory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_07'"));
            rdoMedicalHistoryNoDiseases.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_08'"));
            rdoMedicalDiseases.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_09'"));

            chkPerTension.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_10'"));
            chkHeartDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_11'"));
            chkDiabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_12'"));
            chkCoronary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_13'"));
            chkDysipidemia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_14'"));
            chkCperipheral.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_15'"));
            chkGout.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_16'"));
            chkAbddimal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_17'"));

            chkPulmonaryDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_18'"));
            chkParalysis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_19'"));
            chkPulmonaryTB.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_20'"));
            chkStroke.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_21'"));
            chkKidney.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_22'"));
            chkEpilesy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_23'"));
            chkRespiratory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_24'"));

            chkAsthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_25'"));
            chkEmphysema.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_26'"));
            chkChoronic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_27'"));
            chkSis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_28'"));
            chkAllergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_29'"));
            chkcancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_30'"));
            chkPeptic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_32'"));
            chkDiseaseOthers.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_06'"));


            lblCurrentMedicine.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_33'"));
            rdoCurrentMedicineNone.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_34'"));
            rdoCurrentMedicineHave.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_35'"));

            chkCurrentForDiabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_36'"));
            chkCurrentForHypertension.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_37'"));
            chkCurrentHyperlipidemia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_38'"));
            chkCurrentForcadiovascular.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_39'"));
            chkCurrentDyslip.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_40'"));
            chkCurrentHormone.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_41'"));
            chkCurrentMedicineOthers.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_42'"));

            lblAllergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_43'"));
            rdoAllergyNone.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_44'"));
            rdoAllergyNotsure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_46'"));
            rdoAllergyHave.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_45'"));

            lblPastIllness.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_47'"));
            lblPastIllessOrhospitalAdmission.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_48'"));
            rdoPastIllessOrhospitalAdmissionNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_49'"));
            rdoPastIllessOrhospitalAdmissionYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_50'"));
            lblPastSurgery.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_51'"));
            rdoPastSurgeryNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_52'"));
            rdoPastSurgeryYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_53'"));
            lblVaccinationInformation.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_54'"));
            lblHepatitisB.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_55'"));
            rdoHepatitisBYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_56'"));
            rdoHepatitisBNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_57'"));
            lblHepatitisA.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_58'"));
            rdoHepatitisAYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_59'"));
            rdoHepatitisANo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_60'"));
            lblTetanusvaccine.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_61'"));
            rdoTetanusvaccineLessthanTenYear.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_62'"));
            rdoTetanusvaccineMorethanTenYear.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_63'"));
            rdoTetanusvaccineNorecieve.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'illness_64'"));
            #endregion

            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_EMR_FAMILY");
            lblFamilyHistory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_01'"));
            lblFather.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_02'"));
            rdoFatherNodisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_03'"));
            rdoFatherDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_04'"));
            chkFatherHypertension.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_05'"));
            chkFatherDiabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_07'"));
            chkFatherDyslip.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_09'"));
            chkFatherGout.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_13'"));
            chkFatherPulmonary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_14'"));
            chkFatherTB.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_16'"));
            chkFatherPepticUlcer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_18'"));
            chkFatherAllergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_20'"));
            chkFatherOther.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_22'"));
            chkFatherHeartDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_06'"));
            chkFatherCoronary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_08'"));
            rdoFatherBeforeAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_10'"));
            rdoFatherAfterAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_11'"));
            rdoFatherNotSure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_12'"));
            chkFatherParalysis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_15'"));
            chkFatherStroke.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_17'"));
            chkFatherAsthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_19'"));
            chkFatherCancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_21'"));

            lblMother.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_23'"));
            rdoMotherNodisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_03'"));
            rdoMotherDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_04'"));
            chkMotherHypertension.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_05'"));
            chkMotherDiabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_07'"));
            chkMotherDyslip.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_09'"));
            chkMotherGout.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_13'"));
            chkMotherPulmonary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_14'"));
            chkMotherTB.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_16'"));
            chkMotherPepticUlcer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_18'"));
            chkMotherAllergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_20'"));
            chkMotherOther.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_22'"));
            chkMotherHeartDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_06'"));
            chkMotherCoronary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_08'"));
            rdoMotherBeforeAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_31'"));
            rdoMotherAfterAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_32'"));
            rdoMotherNotSure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_12'"));
            chkMotherParalysis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_15'"));
            chkMotherStroke.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_17'"));
            chkMotherAsthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_19'"));
            chkMotherCancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_21'"));


            lblRelatives.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_44'"));
            rdoRelativesNodisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_03'"));
            rdoRelativesDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_04'"));
            chkRelativesHypertension.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_05'"));
            chkRelativesDiabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_07'"));
            chkRelativesDyslip.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_09'"));
            chkRelativesGout.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_13'"));
            chkRelativesPulmonary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_14'"));
            chkRelativesTB.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_16'"));
            chkRelativesPepticUlcer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_18'"));
            chkRelativesAllergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_20'"));
            chkRelativesOther.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_22'"));
            chkRelativesHeartDisease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_06'"));
            chkRelativesCoronary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_08'"));

            chkWomenRelativesBeforeAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_52'"));
            chkWomenRelativesAfterAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_53'"));
            chkWomenRelativesNotSure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_54'"));
            chkMenRelativesBeforeAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_55'"));
            chkMenRelativesAfterAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_56'"));
            chkMenRelativesNotSure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_57'"));

            chkRelativesParalysis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_15'"));
            chkRelativesStroke.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_17'"));
            chkRelativesAsthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_19'"));
            chkRelativesCancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_21'"));
            lblFamilyOthers.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'family_68'"));

            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_EMR_WOMAN");
            lblForwoman.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_01'"));
            lblWomanFirstDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_02'"));
            chkMonoPause.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_03'"));
            rdoMonoStart.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_04'"));
            lblDateTo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_05'"));
            rdoNotsure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_06'"));

            lblCharacteristic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_07'"));
            rdoCharacteristicNormal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_08'"));
            rdoCharacteristicAbNormal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_09'"));
            lblPregnancy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_10'"));
            rdoPregnancyNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_11'"));
            rdoPregnancy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_12'"));
            rdoPregnancySuspectPregnancy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_13'"));
            lblHasDelivere.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_14'"));
            rdoHasDelivereYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_15'"));
            rdoHasDelivereNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_16'"));
            rdoHasDelivereNotSure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_17'"));

            lblNote_RowOne.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_18'"));
            lblNote_RowTwo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'women_19'"));
            #endregion

        }
        protected void imgThai_Click(object sender, ImageClickEventArgs e)
        {
            Session["LEGION"] = "TH";
            Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_thai.css") + "\" />"));
            loadLabel();
        }
        protected void imgEnglish_Click(object sender, ImageClickEventArgs e)
        {
            Session["LEGION"] = "EN";
            Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));
      
            loadLabel();
        }
        protected void saveQuestionaireHealthHistory_Click(object sender, EventArgs e)
        {
            try
            {
                clsQuestionaireHealthHistory clsQHH = new clsQuestionaireHealthHistory();
                clsQHH.P_HN = Session["HN"].ToString();
                clsQHH.P_type = '1';
                clsQHH.P_confirm_doctor = "";
                clsQHH.P_confirm_date = ut.ConvertDateToStringFormat(DateTime.Now.ToString(), "yyyy-MM-dd");
                clsQHH.P_envi_other = string.Empty;
                clsQHH.P_illness_others = string.Empty;
                clsQHH.P_address = string.Empty;
                clsQHH.P_tumbon = string.Empty;
                clsQHH.P_aumphur = string.Empty;
                clsQHH.P_mobile = string.Empty;
                clsQHH.P_province = string.Empty;
                clsQHH.P_zip_code = string.Empty;
                clsQHH.P_create_by = "SYSTEM";
                clsQHH.P_create_date = ut.ConvertDateToStringFormat(DateTime.Now.ToString(), "yyyy-MM-dd");
                clsQHH.P_update_by = "SYSTEM";
                clsQHH.P_update_date = ut.ConvertDateToStringFormat(DateTime.Now.ToString(), "yyyy-MM-dd");

                clsQHH.P_his_smok = ut.retValueToChar(Request.Form["Smoking"]);
                clsQHH.P_his_nsmok_yrs = ut.retValueToDouble(Request.Form["txtNonSmokingButBeSecondHand"]);

                clsQHH.P_his_qsmok_yrs = ut.retValueToDouble(Request.Form["txtSpecify"]);
                clsQHH.P_his_smok_amt = ut.retValueToDouble(Request.Form["txtAmount"]);
                clsQHH.P_his_smok_dur = ut.retValueToDouble(Request.Form["txtDuration"]);

                clsQHH.P_his_alcohol = ut.retValueToChar(Request.Form["Alcohol"]);
                clsQHH.P_his_alco_yrs = ut.retValueToDouble(Request.Form["txtQuitAlcohol"]);
                clsQHH.P_his_alco_social = ut.retValueToChar(Request.Form["SocialDrinkPerMonth"]);

                clsQHH.P_his_exercise = ut.retValueToChar(Request.Form["grpExercise"]);

                clsQHH.P_ill_concern = ut.retValueToChar(Request.Form["grpAnnualCheckUp"]);
                clsQHH.P_ill_conc_oth = ut.retValueToString(Request.Form["txtAnnualCheckUpOthers"]);

                clsQHH.P_ill_psycho = ut.retValueToChar(Request.Form["grpPhycho"]);
                clsQHH.P_ill_psycho_oth = ut.retValueToString(Request.Form["txtPhycho"]);

                clsQHH.P_ill_med_his = ut.retValueToChar(Request.Form["grpMedicalDiseases"]);
                clsQHH.P_ill_med_hyper = ut.retValueToBoolean(Request.Form["chkPerTension"]);
                clsQHH.P_ill_med_heart = ut.retValueToBoolean(Request.Form["chkHeartDisease"]);
                clsQHH.P_ill_med_diab = ut.retValueToBoolean(Request.Form["chkDiabetes"]);
                clsQHH.P_ill_med_coro = ut.retValueToBoolean(Request.Form["chkCoronary"]);
                clsQHH.P_ill_med_dysl = ut.retValueToBoolean(Request.Form["chkDysipidemia"]);
                clsQHH.P_ill_med_cper = ut.retValueToBoolean(Request.Form["chkCperipheral"]);
                clsQHH.P_ill_med_gout = ut.retValueToBoolean(Request.Form["chkGout"]);
                clsQHH.P_ill_med_abdd = ut.retValueToBoolean(Request.Form["chkAbddimal"]);

                clsQHH.P_ill_med_pulm = ut.retValueToBoolean(Request.Form["chkPulmonaryDisease"]);
                clsQHH.P_ill_med_para = ut.retValueToBoolean(Request.Form["chkParalysis"]);
                clsQHH.P_ill_med_putb = ut.retValueToBoolean(Request.Form["chkPulmonaryTB"]);
                clsQHH.P_ill_med_stro = ut.retValueToBoolean(Request.Form["chkStroke"]);
                clsQHH.P_ill_med_kidn = ut.retValueToBoolean(Request.Form["chkKidney"]);
                clsQHH.P_ill_med_epil = ut.retValueToBoolean(Request.Form["chkEpilesy"]);
                clsQHH.P_ill_med_resp = ut.retValueToBoolean(Request.Form["chkRespiratory"]);

                clsQHH.P_ill_med_asth = ut.retValueToBoolean(Request.Form["chkAsthma"]);
                clsQHH.P_ill_med_emph = ut.retValueToBoolean(Request.Form["chkEmphysema"]);
                clsQHH.P_ill_med_chro = ut.retValueToBoolean(Request.Form["chkChoronic"]);

                clsQHH.P_ill_med_sist = ut.retValueToBoolean(Request.Form["chkSis"]);
                clsQHH.P_ill_med_alle = ut.retValueToBoolean(Request.Form["chkAllergy"]);
                clsQHH.P_ill_med_canc = ut.retValueToBoolean(Request.Form["chkcancer"]);
                clsQHH.P_ill_med_canc_oth = ut.retValueToString(Request.Form["txtCancerType"]);
                clsQHH.P_ill_med_pept = ut.retValueToBoolean(Request.Form["chkPeptic"]);
                clsQHH.P_ill_med_oth = ut.retValueToBoolean(Request.Form["chkDiseaseOthers"]);
                clsQHH.P_ill_med_others = ut.retValueToString(Request.Form["txtOtherMedicalHistory"]);

                clsQHH.P_ill_cur_med = ut.retValueToChar(Request.Form["grpCurrentMedicine"]);
                clsQHH.P_ill_cmed_diab = ut.retValueToBoolean(Request.Form["chkCurrentForDiabetes"]);
                clsQHH.P_ill_cmed_hyper = ut.retValueToBoolean(Request.Form["chkCurrentForHypertension"]);
                clsQHH.P_ill_cmed_demia = ut.retValueToBoolean(Request.Form["chkCurrentHyperlipidemia"]);
                clsQHH.P_ill_cmed_oth = ut.retValueToBoolean(Request.Form["chkCurrentMedicineOthers"]);
                clsQHH.P_ill_cmed_others = ut.retValueToString(Request.Form["txtCurrentMedicineOthers"]);

                clsQHH.P_ill_cmed_cardi = ut.retValueToBoolean(Request.Form["chkCurrentForcadiovascular"]);
                clsQHH.P_ill_cmed_dysl = ut.retValueToBoolean(Request.Form["chkCurrentDyslip"]);
                clsQHH.P_ill_cmed_horm = ut.retValueToBoolean(Request.Form["chkCurrentHormone"]);

                clsQHH.P_ill_allergy = ut.retValueToChar(Request.Form["grpAllergy"]);
                clsQHH.P_ill_drug_or_food = ut.retValueToString(Request.Form["txtAllergyHave"]);

                clsQHH.P_pill_adm = ut.retValueToChar(Request.Form["grpPastIllessOrhospitalAdmission"]);
                clsQHH.P_pill_admission = ut.retValueToString(Request.Form["txtPastIllessOrhospitalAdmissionOthers"]);

                clsQHH.P_pill_sur = ut.retValueToChar(Request.Form["grpPastSurgery"]);
                clsQHH.P_pill_surgery = ut.retValueToString(Request.Form["txtPastSurgeryYesOthers"]);

                clsQHH.P_vinf_hepB_virus = ut.retValueToChar(Request.Form["grpHepatitisB"]);
                clsQHH.P_vinf_hepA_virus = ut.retValueToChar(Request.Form["grpHepatitisA"]);
                clsQHH.P_vinf_vaccine = ut.retValueToChar(Request.Form["grpTetanusvaccine"]);

                clsQHH.P_fhis_f_disease = ut.retValueToChar(Request.Form["grpFatherDisease"]);
                clsQHH.P_fhis_fdis_hyper = ut.retValueToBoolean(Request.Form["chkFatherHypertension"]);
                clsQHH.P_fhis_fdis_heart = ut.retValueToBoolean(Request.Form["chkFatherHeartDisease"]);
                clsQHH.P_fhis_fdis_diab = ut.retValueToBoolean(Request.Form["chkFatherDiabetes"]);
                clsQHH.P_fhis_fdis_coro = ut.retValueToBoolean(Request.Form["chkFatherCoronary"]);
                clsQHH.P_fhis_fdis_coro_cs = ut.retValueToChar(Request.Form["grpFather"]);
                clsQHH.P_fhis_fdis_dysl = ut.retValueToBoolean(Request.Form["chkFatherDyslip"]);
                clsQHH.P_fhis_fdis_gout = ut.retValueToBoolean(Request.Form["chkFatherGout"]);
                clsQHH.P_fhis_fdis_pulm = ut.retValueToBoolean(Request.Form["chkFatherPulmonary"]);
                clsQHH.P_fhis_fdis_para = ut.retValueToBoolean(Request.Form["chkFatherParalysis"]);
                clsQHH.P_fhis_fdis_putb = ut.retValueToBoolean(Request.Form["chkFatherTB"]);
                clsQHH.P_fhis_fdis_stro = ut.retValueToBoolean(Request.Form["chkFatherStroke"]);
                clsQHH.P_fhis_fdis_pepu = ut.retValueToBoolean(Request.Form["chkFatherPepticUlcer"]);
                clsQHH.P_fhis_fdis_asth = ut.retValueToBoolean(Request.Form["chkFatherAsthma"]);
                clsQHH.P_fhis_fdis_alle = ut.retValueToBoolean(Request.Form["chkFatherAllergy"]);
                clsQHH.P_fhis_fdis_canc = ut.retValueToBoolean(Request.Form["chkFatherCancer"]);
                clsQHH.P_fhis_fdis_canc_rmk = ut.retValueToString(Request.Form["txtFatherCancer"]);
                clsQHH.P_fhis_fdis_oth = ut.retValueToBoolean(Request.Form["chkFatherOther"]);
                clsQHH.P_fhis_fdis_others = ut.retValueToString(Request.Form["txtFatherOthers"]);


                clsQHH.P_fhis_m_disease = ut.retValueToChar(Request.Form["grpMotherDisease"]);
                clsQHH.P_fhis_mdis_hyper = ut.retValueToBoolean(Request.Form["chkMotherHypertension"]);
                clsQHH.P_fhis_mdis_heart = ut.retValueToBoolean(Request.Form["chkMotherHeartDisease"]);
                clsQHH.P_fhis_mdis_diab = ut.retValueToBoolean(Request.Form["chkMotherDiabetes"]);
                clsQHH.P_fhis_mdis_coro = ut.retValueToBoolean(Request.Form["chkMotherCoronary"]);
                clsQHH.P_fhis_mdis_coro_cs = ut.retValueToChar(Request.Form["grpMother"]);
                clsQHH.P_fhis_mdis_dysl = ut.retValueToBoolean(Request.Form["chkMotherDyslip"]);
                clsQHH.P_fhis_mdis_gout = ut.retValueToBoolean(Request.Form["chkMotherGout"]);
                clsQHH.P_fhis_mdis_pulm = ut.retValueToBoolean(Request.Form["chkMotherPulmonary"]);
                clsQHH.P_fhis_mdis_para = ut.retValueToBoolean(Request.Form["chkMotherParalysis"]);
                clsQHH.P_fhis_mdis_putb = ut.retValueToBoolean(Request.Form["chkMotherTB"]);
                clsQHH.P_fhis_mdis_stro = ut.retValueToBoolean(Request.Form["chkMotherStroke"]);
                clsQHH.P_fhis_mdis_pepu = ut.retValueToBoolean(Request.Form["chkMotherPepticUlcer"]);
                clsQHH.P_fhis_mdis_asth = ut.retValueToBoolean(Request.Form["chkMotherAsthma"]);
                clsQHH.P_fhis_mdis_alle = ut.retValueToBoolean(Request.Form["chkMotherAllergy"]);
                clsQHH.P_fhis_mdis_canc = ut.retValueToBoolean(Request.Form["chkMotherCancer"]);
                clsQHH.P_fhis_mdis_canc_rmk = ut.retValueToString(Request.Form["txtMotherCancer"]);
                clsQHH.P_fhis_mdis_oth = ut.retValueToBoolean(Request.Form["chkMotherOther"]);
                clsQHH.P_fhis_mdis_others = ut.retValueToString(Request.Form["txtMotherOthers"]);


                clsQHH.P_fhis_b_disease = ut.retValueToChar(Request.Form["grpRelativesDisease"]);
                clsQHH.P_fhis_bdis_hyper = ut.retValueToBoolean(Request.Form["chkRelativesHypertension"]);
                clsQHH.P_fhis_bdis_heart = ut.retValueToBoolean(Request.Form["chkRelativesHeartDisease"]);
                clsQHH.P_fhis_bdis_diab = ut.retValueToBoolean(Request.Form["chkRelativesDiabetes"]);
                clsQHH.P_fhis_bdis_coro = ut.retValueToBoolean(Request.Form["chkRelativesCoronary"]);
                clsQHH.P_fhis_bdis_coro_bfm = ut.retValueToBoolean(Request.Form["chkWomenRelativesBeforeAge"]);
                clsQHH.P_fhis_bdis_coro_afm = ut.retValueToBoolean(Request.Form["chkWomenRelativesAfterAge"]);
                clsQHH.P_fhis_bdis_coro_nfm = ut.retValueToBoolean(Request.Form["chkWomenRelativesNotSure"]);
                clsQHH.P_fhis_bdis_coro_bm = ut.retValueToBoolean(Request.Form["chkMenRelativesBeforeAge"]);
                clsQHH.P_fhis_bdis_coro_am = ut.retValueToBoolean(Request.Form["chkMenRelativesAfterAge"]);
                clsQHH.P_fhis_bdis_coro_nm = ut.retValueToBoolean(Request.Form["chkMenRelativesNotSure"]);
                clsQHH.P_fhis_bdis_dysl = ut.retValueToBoolean(Request.Form["chkRelativesDyslip"]);
                clsQHH.P_fhis_bdis_gout = ut.retValueToBoolean(Request.Form["chkRelativesGout"]);
                clsQHH.P_fhis_bdis_pulm = ut.retValueToBoolean(Request.Form["chkRelativesPulmonary"]);
                clsQHH.P_fhis_bdis_para = ut.retValueToBoolean(Request.Form["chkRelativesParalysis"]);
                clsQHH.P_fhis_bdis_putb = ut.retValueToBoolean(Request.Form["chkRelativesTB"]);
                clsQHH.P_fhis_bdis_stro = ut.retValueToBoolean(Request.Form["chkRelativesStroke"]);
                clsQHH.P_fhis_bdis_pepu = ut.retValueToBoolean(Request.Form["chkRelativesPepticUlcer"]);
                clsQHH.P_fhis_bdis_asth = ut.retValueToBoolean(Request.Form["chkRelativesAsthma"]);
                clsQHH.P_fhis_bdis_alle = ut.retValueToBoolean(Request.Form["chkRelativesAllergy"]);
                clsQHH.P_fhis_bdis_canc = ut.retValueToBoolean(Request.Form["chkRelativesCancer"]);
                clsQHH.P_fhis_bdis_canc_rmk = ut.retValueToString(Request.Form["txtRelativesCancer"]);
                clsQHH.P_fhis_bdis_oth = ut.retValueToBoolean(Request.Form["chkRelativesOther"]);
                clsQHH.P_fhis_bdis_others = ut.retValueToString(Request.Form["txtRelativesOthers"]);
                clsQHH.P_fhis_others = ut.retValueToString(Request.Form["txtFamilyOthers"]);


                clsQHH.P_fwm_menopause = ut.retValueToBoolean(Request.Form["chkMonoPause"]);
                clsQHH.P_fwm_meno_start = ut.retValueToChar(Request.Form["menStart"]);
                clsQHH.P_fwm_lst_st_mens = ut.ConvertDateToStringFormat(ut.retValueToString(Request.Form["txtDateFormHH"]), "yyyy-MM-dd");
                clsQHH.P_fwm_lst_ed_mens = ut.ConvertDateToStringFormat(ut.retValueToString(Request.Form["txtDateToHH"]), "yyyy-MM-dd");
                clsQHH.P_fwm_character = ut.retValueToChar(Request.Form["characteristic"]);
                clsQHH.P_fwm_pregnancy = ut.retValueToChar(Request.Form["Pregnancy"]);
                clsQHH.P_fwm_over_weight = ut.retValueToChar(Request.Form["HasDelivere"]);


                clsQHH.P_ill_med_rmk_oth = string.Empty;
                clsEX.insert_trn_questionaire_history(clsQHH);
                Response.Redirect("~/default.aspx");


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            DataTable dtBHQ = clsEX.get_QuestionairePatiant(Session["HN"].ToString());

            if (dtBHQ != null)
            {
                if (dtBHQ.Rows.Count > 0)
                {
                    if (dtBHQ.Rows[0]["p_his_smok"].ToString() == "1") { rdoNonSmoking.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_smok"].ToString() == "2") { rdoNonSmokingButBeSecondHand.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_smok"].ToString() == "3") { rdoSmoke.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_smok"].ToString() == "4") { rdoOutSmoking.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_qsmok_yrs"].ToString() != string.Empty) { txtSpecify.Text = dtBHQ.Rows[0]["p_his_qsmok_yrs"].ToString(); }
                    if (dtBHQ.Rows[0]["p_his_nsmok_yrs"].ToString() != string.Empty) { txtNonSmokingButBeSecondHand.Text = dtBHQ.Rows[0]["p_his_nsmok_yrs"].ToString(); }
                    if (dtBHQ.Rows[0]["p_his_smok_amt"].ToString() != string.Empty) { txtAmount.Text = dtBHQ.Rows[0]["p_his_smok_amt"].ToString(); }
                    if (dtBHQ.Rows[0]["p_his_smok_dur"].ToString() != string.Empty) { txtDuration.Text = dtBHQ.Rows[0]["p_his_smok_dur"].ToString(); }

                    if (dtBHQ.Rows[0]["p_his_alcohol"].ToString() == "1") { rdoNoneAlcohol.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_alcohol"].ToString() == "2") { rdoQuitAlcohol.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_alco_yrs"].ToString() != string.Empty) { txtQuitAlcohol.Text = dtBHQ.Rows[0]["p_his_alco_yrs"].ToString(); }
                    if (dtBHQ.Rows[0]["p_his_alcohol"].ToString() == "3") { rdoSocialDrink.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_alco_social"].ToString() == "1") { rdoSocialDrinkOnePerMonth.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_alco_social"].ToString() == "2") { rdoSocialDrinkTwoPerMonth.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_alco_social"].ToString() == "3") { rdoSocialDrinkThreePerMonth.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_alcohol"].ToString() == "4") { rdoDrinkFourPerWeek.Checked = true; }

                    if (dtBHQ.Rows[0]["p_his_exercise"].ToString() == "1") { rdoNoneExercise.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_exercise"].ToString() == "2") { rdoLessThanHoursExercise.Checked = true; }
                    if (dtBHQ.Rows[0]["p_his_exercise"].ToString() == "3") { rdoRegularExercise.Checked = true; }

                    if (dtBHQ.Rows[0]["p_ill_concern"].ToString() == "1") { rdoAnnualCheckUp.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_concern"].ToString() == "2") { rdoAnnualCheckUpOthers.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_conc_oth"].ToString() != string.Empty) { txtAnnualCheckUpOthers.Text = dtBHQ.Rows[0]["p_ill_conc_oth"].ToString(); }

                    if (dtBHQ.Rows[0]["p_ill_psycho"].ToString() == "1") { rdoNonePhycho.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_psycho"].ToString() == "2") { rdoPhychoOthers.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_psycho"].ToString() != string.Empty) { txtPhycho.Text = dtBHQ.Rows[0]["p_ill_psycho_oth"].ToString(); }



                    if (dtBHQ.Rows[0]["p_ill_med_his"].ToString() == "1") { rdoMedicalHistoryNoDiseases.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_his"].ToString() == "2") { rdoMedicalDiseases.Checked = true; }

                    if (dtBHQ.Rows[0]["p_ill_med_hyper"].ToString() == "True") { chkPerTension.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_heart"].ToString() == "True") { chkHeartDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_diab"].ToString() == "True") { chkDiabetes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_coro"].ToString() == "True") { chkCoronary.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_dysl"].ToString() == "True") { chkDysipidemia.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_cper"].ToString() == "True") { chkCperipheral.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_gout"].ToString() == "True") { chkGout.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_abdd"].ToString() == "True") { chkAbddimal.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_pulm"].ToString() == "True") { chkPulmonaryDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_para"].ToString() == "True") { chkParalysis.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_putb"].ToString() == "True") { chkPulmonaryTB.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_stro"].ToString() == "True") { chkStroke.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_kidn"].ToString() == "True") { chkKidney.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_epil"].ToString() == "True") { chkEpilesy.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_resp"].ToString() == "True") { chkRespiratory.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_asth"].ToString() == "True") { chkAsthma.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_emph"].ToString() == "True") { chkEmphysema.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_chro"].ToString() == "True") { chkChoronic.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_sist"].ToString() == "True") { chkSis.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_alle"].ToString() == "True") { chkAllergy.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_canc"].ToString() == "True") { chkcancer.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_canc_oth"].ToString() != string.Empty) { txtCancerType.Text = dtBHQ.Rows[0]["p_ill_psycho_oth"].ToString(); }
                    if (dtBHQ.Rows[0]["p_ill_med_pept"].ToString() == "True") { chkPeptic.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_oth"].ToString() == "True") { chkDiseaseOthers.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_med_others"].ToString() != string.Empty) { txtOtherMedicalHistory.Text = dtBHQ.Rows[0]["p_ill_med_others"].ToString(); }



                    if (dtBHQ.Rows[0]["p_ill_cur_med"].ToString() == "1") { rdoCurrentMedicineNone.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cur_med"].ToString() == "2") { rdoCurrentMedicineHave.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cmed_diab"].ToString() == "True") { chkCurrentForDiabetes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cmed_hyper"].ToString() == "True") { chkCurrentForHypertension.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cmed_demia"].ToString() == "True") { chkCurrentHyperlipidemia.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cmed_oth"].ToString() == "True") { chkCurrentMedicineOthers.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cmed_others"].ToString() != string.Empty) { txtCurrentMedicineOthers.Text = dtBHQ.Rows[0]["p_ill_cmed_others"].ToString(); }
                    if (dtBHQ.Rows[0]["p_ill_cmed_cardi"].ToString() == "True") { chkCurrentForcadiovascular.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cmed_dysl"].ToString() == "True") { chkCurrentDyslip.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_cmed_horm"].ToString() == "True") { chkCurrentHormone.Checked = true; }

                    if (dtBHQ.Rows[0]["p_ill_allergy"].ToString() == "1") { rdoAllergyNone.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_allergy"].ToString() == "2") { rdoAllergyNotsure.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_allergy"].ToString() == "3") { rdoAllergyHave.Checked = true; }
                    if (dtBHQ.Rows[0]["p_ill_drug_or_food"].ToString() != string.Empty) { txtAllergyHave.Text = dtBHQ.Rows[0]["p_ill_drug_or_food"].ToString(); }


                    if (dtBHQ.Rows[0]["p_pill_adm"].ToString() == "1") { rdoPastIllessOrhospitalAdmissionNo.Checked = true; }
                    if (dtBHQ.Rows[0]["p_pill_adm"].ToString() == "2") { rdoPastIllessOrhospitalAdmissionYes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_pill_admission"].ToString() != string.Empty) { txtPastIllessOrhospitalAdmissionOthers.Text = dtBHQ.Rows[0]["p_pill_admission"].ToString(); }


                    if (dtBHQ.Rows[0]["p_pill_sur"].ToString() == "1") { rdoPastSurgeryNo.Checked = true; }
                    if (dtBHQ.Rows[0]["p_pill_sur"].ToString() == "2") { rdoPastSurgeryYes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_pill_surgery"].ToString() != string.Empty) { txtPastSurgeryYesOthers.Text = dtBHQ.Rows[0]["p_pill_surgery"].ToString(); }


                    if (dtBHQ.Rows[0]["p_vinf_hepB_virus"].ToString() == "1") { rdoHepatitisBYes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_vinf_hepB_virus"].ToString() == "2") { rdoHepatitisBNo.Checked = true; }

                    if (dtBHQ.Rows[0]["p_vinf_hepA_virus"].ToString() == "1") { rdoHepatitisAYes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_vinf_hepA_virus"].ToString() == "2") { rdoHepatitisANo.Checked = true; }


                    if (dtBHQ.Rows[0]["p_vinf_vaccine"].ToString() == "1") { rdoTetanusvaccineLessthanTenYear.Checked = true; }
                    if (dtBHQ.Rows[0]["p_vinf_vaccine"].ToString() == "2") { rdoTetanusvaccineMorethanTenYear.Checked = true; }
                    if (dtBHQ.Rows[0]["p_vinf_vaccine"].ToString() == "3") { rdoTetanusvaccineNorecieve.Checked = true; }


                    if (dtBHQ.Rows[0]["p_fhis_f_disease"].ToString() == "1") { rdoFatherNodisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_f_disease"].ToString() == "2") { rdoFatherDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_hyper"].ToString() == "True") { chkFatherHypertension.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_diab"].ToString() == "True") { chkFatherDiabetes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_dysl"].ToString() == "True") { chkFatherDyslip.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_gout"].ToString() == "True") { chkFatherGout.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_pulm"].ToString() == "True") { chkFatherPulmonary.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_putb"].ToString() == "True") { chkFatherTB.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_pepu"].ToString() == "True") { chkFatherPepticUlcer.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_alle"].ToString() == "True") { chkFatherAllergy.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_heart"].ToString() == "True") { chkFatherHeartDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_oth"].ToString() == "True") { chkFatherOther.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_others"].ToString() != string.Empty) { txtFatherOthers.Text = dtBHQ.Rows[0]["p_fhis_fdis_others"].ToString(); }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_coro"].ToString() == "True") { chkFatherCoronary.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_coro_cs"].ToString() == "1") { rdoFatherBeforeAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_coro_cs"].ToString() == "2") { rdoFatherAfterAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_coro_cs"].ToString() == "3") { rdoFatherNotSure.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_para"].ToString() == "True") { chkFatherParalysis.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_stro"].ToString() == "True") { chkFatherStroke.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_asth"].ToString() == "True") { chkFatherAsthma.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_canc"].ToString() == "True") { chkFatherCancer.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_fdis_canc_rmk"].ToString() != string.Empty) { txtFatherCancer.Text = dtBHQ.Rows[0]["p_fhis_fdis_canc_rmk"].ToString(); }



                    if (dtBHQ.Rows[0]["p_fhis_m_disease"].ToString() == "1") { rdoMotherNodisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_m_disease"].ToString() == "2") { rdoMotherDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_hyper"].ToString() == "True") { chkMotherHypertension.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_diab"].ToString() == "True") { chkMotherDiabetes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_dysl"].ToString() == "True") { chkMotherDyslip.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_gout"].ToString() == "True") { chkMotherGout.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_pulm"].ToString() == "True") { chkMotherPulmonary.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_putb"].ToString() == "True") { chkMotherTB.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_pepu"].ToString() == "True") { chkMotherPepticUlcer.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_alle"].ToString() == "True") { chkMotherAllergy.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_heart"].ToString() == "True") { chkMotherHeartDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_oth"].ToString() == "True") { chkMotherOther.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_others"].ToString() != string.Empty) { txtMotherOthers.Text = dtBHQ.Rows[0]["p_fhis_mdis_others"].ToString(); }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_coro"].ToString() == "True") { chkMotherCoronary.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_coro_cs"].ToString() == "1") { rdoMotherBeforeAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_coro_cs"].ToString() == "2") { rdoMotherAfterAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_coro_cs"].ToString() == "3") { rdoMotherNotSure.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_para"].ToString() == "True") { chkMotherParalysis.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_stro"].ToString() == "True") { chkMotherStroke.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_asth"].ToString() == "True") { chkMotherAsthma.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_canc"].ToString() == "True") { chkMotherCancer.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_mdis_canc_rmk"].ToString() != string.Empty) { txtMotherCancer.Text = dtBHQ.Rows[0]["p_fhis_mdis_canc_rmk"].ToString(); }


                    if (dtBHQ.Rows[0]["p_fhis_b_disease"].ToString() == "1") { rdoRelativesNodisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_b_disease"].ToString() == "2") { rdoRelativesDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_hyper"].ToString() == "True") { chkRelativesHypertension.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_diab"].ToString() == "True") { chkRelativesDiabetes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_dysl"].ToString() == "True") { chkRelativesDyslip.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_gout"].ToString() == "True") { chkRelativesGout.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_pulm"].ToString() == "True") { chkRelativesPulmonary.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_putb"].ToString() == "True") { chkRelativesTB.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_pepu"].ToString() == "True") { chkRelativesPepticUlcer.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_alle"].ToString() == "True") { chkRelativesAllergy.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_heart"].ToString() == "True") { chkRelativesHeartDisease.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_oth"].ToString() == "True") { chkRelativesOther.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_others"].ToString() != string.Empty) { txtRelativesOthers.Text = dtBHQ.Rows[0]["p_fhis_bdis_others"].ToString(); }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_coro"].ToString() == "True") { chkRelativesCoronary.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_coro_bfm"].ToString() == "True") { chkWomenRelativesBeforeAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_coro_afm"].ToString() == "True") { chkWomenRelativesAfterAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_coro_nfm"].ToString() == "True") { chkWomenRelativesNotSure.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_coro_bm"].ToString() == "True") { chkMenRelativesBeforeAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_coro_am"].ToString() == "True") { chkMenRelativesAfterAge.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_coro_nm"].ToString() == "True") { chkMenRelativesNotSure.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_para"].ToString() == "True") { chkRelativesParalysis.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_stro"].ToString() == "True") { chkRelativesStroke.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_asth"].ToString() == "True") { chkRelativesAsthma.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_canc"].ToString() == "True") { chkRelativesCancer.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fhis_bdis_canc_rmk"].ToString() != string.Empty) { txtRelativesCancer.Text = dtBHQ.Rows[0]["p_fhis_bdis_canc_rmk"].ToString(); }
                    if (dtBHQ.Rows[0]["p_fhis_others"].ToString() != string.Empty) { txtFamilyOthers.Text = dtBHQ.Rows[0]["p_fhis_others"].ToString(); }


                    if (dtBHQ.Rows[0]["p_fwm_menopause"].ToString() == "True") { chkMonoPause.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fwm_meno_start"].ToString() == "1") { rdoMonoStart.Checked = true; }
                    if (dtBHQ.Rows[0]["tqp_fwm_lst_st_mens"].ToString() != string.Empty) { txtDateFormHH.Text = dtBHQ.Rows[0]["tqp_fwm_lst_st_mens"].ToString(); }
                    if (dtBHQ.Rows[0]["tqp_fwm_lst_ed_mens"].ToString() != string.Empty) { txtDateToHH.Text = dtBHQ.Rows[0]["tqp_fwm_lst_ed_mens"].ToString(); }
                    if (dtBHQ.Rows[0]["p_fwm_meno_start"].ToString() == "2") { rdoNotsure.Checked = true; }


                    if (dtBHQ.Rows[0]["p_fwm_character"].ToString() == "1") { rdoCharacteristicNormal.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fwm_character"].ToString() == "2") { rdoCharacteristicAbNormal.Checked = true; }

                    if (dtBHQ.Rows[0]["p_fwm_pregnancy"].ToString() == "1") { rdoPregnancyNo.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fwm_pregnancy"].ToString() == "2") { rdoPregnancy.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fwm_pregnancy"].ToString() == "3") { rdoPregnancySuspectPregnancy.Checked = true; }

                    if (dtBHQ.Rows[0]["p_fwm_over_weight"].ToString() == "1") { rdoHasDelivereYes.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fwm_over_weight"].ToString() == "2") { rdoHasDelivereNo.Checked = true; }
                    if (dtBHQ.Rows[0]["p_fwm_over_weight"].ToString() == "3") { rdoHasDelivereNotSure.Checked = true; }

                }

            }

        }

    }
}
