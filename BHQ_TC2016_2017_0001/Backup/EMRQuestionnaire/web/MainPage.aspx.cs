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
    public partial class MainPage : System.Web.UI.Page
    {
        executeDC clsEX = new executeDC();
        DataTable dtMasLabel = new DataTable();
        Utility ut = new Utility();
        DataTable dtHistory = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(Request.QueryString["sHn"]) == true)
           {
               if ( Session["HN"].ToString() == string.Empty)
               {
                   Response.Redirect("~/default.aspx");
               }
               else {

                   if (String.IsNullOrEmpty(Request.QueryString["sAllergy"]) == true) {

                       Session["Allergy"] = "";                   
                   }
               }
            }
            else {
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
            else {
                System.Web.UI.HtmlControls.HtmlGenericControl mystyles;
                mystyles = new System.Web.UI.HtmlControls.HtmlGenericControl();
                mystyles.TagName = "style";
                string sampleCSS = "body { font-family:  sans-serif; }";
                mystyles.InnerText = sampleCSS;
                Page.Header.Controls.Add(mystyles);
            }
            txtHN.Text = Session["HN"].ToString();
            txtName.Text = Session["Name"].ToString();
            txtAllergies.Text = Session["Allergy"].ToString();

            if (!Page.IsPostBack)
            {
                Session["LEGION"] = "EN";
                Page.Header.Controls.Add(new System.Web.UI.LiteralControl("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + ResolveUrl("~/css/maincss_en.css") + "\" />"));
      
                loadLabel();

            }
        }
        private void loadDataModeNew()
        {
            txtHN.Text = Session["HN"].ToString();
            txtName.Text = Session["Name"].ToString();
            txtAllergies.Text = Session["Allergy"].ToString();
        }
        private void loadData(string rowid_key)
        {
            try
            {
                dtHistory = clsEX.get_history_patients(rowid_key);
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

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "GENERAL_INFORMATION");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    txtEmploymentDate.Text = dtHistory.Rows[0]["EMPLOYMENT_DATE"].ToString().Trim();
                    txtEmployer.Text = dtHistory.Rows[0]["EMPLOYER"].ToString().Trim();
                    txtWorkLocation.Text = dtHistory.Rows[0]["WORK_LOCATION"].ToString().Trim();
                    txtEmail.Text = dtHistory.Rows[0]["EMAIL_ADDRESS"].ToString().Trim();
                    txtFunctional.Text = dtHistory.Rows[0]["FUNCTIONAL_GROUP"].ToString().Trim();
                    txtCurrent.Text = dtHistory.Rows[0]["CURRENT_POSITION"].ToString().Trim();
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "HISTORY_CLASSIFICATION_OF_EMPLOYMENT");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["OIL_NATURAL_GAS"].ToString() == "Y") { chkOil.Checked = true; }
                    if (dtHistory.Rows[0]["NON_METAL_PRODUCTS"].ToString() == "Y") { chkNon.Checked = true; }
                    if (dtHistory.Rows[0]["MANUFACTURE_OF_FOOD"].ToString() == "Y") { chkMan.Checked = true; }
                    if (dtHistory.Rows[0]["MANUFACTURE_OF_BASIC_METALS"].ToString() == "Y") { chkBasicMetals.Checked = true; }
                    if (dtHistory.Rows[0]["MANUFACTURE_OF_TEXTILES"].ToString() == "Y") { chkText.Checked = true; }
                    if (dtHistory.Rows[0]["METALS_PRODUCTS"].ToString() == "Y") { chkMetals.Checked = true; }
                    if (dtHistory.Rows[0]["FORESTRY_AND_LOGGING"].ToString() == "Y") { chkForrest.Checked = true; }
                    if (dtHistory.Rows[0]["MANUFACTURE_OF_MOTOR"].ToString() == "Y") { chkMotor.Checked = true; }
                    if (dtHistory.Rows[0]["MANUFACTURE_OF_PAPER"].ToString() == "Y") { chkPaper.Checked = true; }
                    if (dtHistory.Rows[0]["PUBLIC_UTILITY"].ToString() == "Y") { chkPublic.Checked = true; }
                    if (dtHistory.Rows[0]["MANUFACTURE_OF_CHEMICAL"].ToString() == "Y") { chkChemecal.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkOtherClassificationofEmployment.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty)
                    {
                        txtOtherClassificationofEmployment.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString();
                    }
                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_TYPE_OF_WORK");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["OFFICE_WORK"].ToString() == "Y") { rdoOffice.Checked = true; }
                    if (dtHistory.Rows[0]["ONSHORE_WORK"].ToString() == "Y") { rdoOnShore.Checked = true; }
                    if (dtHistory.Rows[0]["OFFSHORE_WORK"].ToString() == "Y") { rdoOffShore.Checked = true; }

                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_SPECIAL_ASSIGNMENT");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["FIRE_FIGHTING_STAFF"].ToString() == "Y") { chkFire.Checked = true; }
                    if (dtHistory.Rows[0]["CONFINED_SPACE_WORKER"].ToString() == "Y") { chkCon.Checked = true; }
                    if (dtHistory.Rows[0]["PROFESSIONAL_DRIVER"].ToString() == "Y") { chkProfes.Checked = true; }
                    if (dtHistory.Rows[0]["LABORATORY_TECHNICIAN"].ToString() == "Y") { chkLab.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkOtherSpecialAssignment.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtOtherSpecialAssignment.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }
                    if (dtHistory.Rows[0]["CRANE_OPERATOR"].ToString() == "Y") { chkCrane.Checked = true; }
                    if (dtHistory.Rows[0]["PAINTER"].ToString() == "Y") { chkPainter.Checked = true; }
                    if (dtHistory.Rows[0]["CATERING_AND_FOOD"].ToString() == "Y") { chkCarter.Checked = true; }

                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_PHYSICAL_HEALTH_HAZARD");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO_PHYSICAL_HEALTH_HAZARD"].ToString() == "Y") { chkNophysical.Checked = true; }
                    if (dtHistory.Rows[0]["LIGHT"].ToString() == "Y") { chkLight.Checked = true; }
                    if (dtHistory.Rows[0]["COLD"].ToString() == "Y") { chkCold.Checked = true; }
                    if (dtHistory.Rows[0]["NOISE"].ToString() == "Y") { chkNoise.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkOthersHazard.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtOthersHazard.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }
                    if (dtHistory.Rows[0]["RADIATION"].ToString() == "Y") { chkRadia.Checked = true; }
                    if (dtHistory.Rows[0]["HEAT"].ToString() == "Y") { chkHeat.Checked = true; }


                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_BIOLOGICAL_HEALTH_HAZARD");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO_BIOLOGICAL_HEALTH_HAZARD"].ToString() == "Y") { chkNobiological.Checked = true; }
                    if (dtHistory.Rows[0]["ANIMAL_CARRIERS"].ToString() == "Y") { chkAnimal.Checked = true; }
                    if (dtHistory.Rows[0]["BLOOD_OR_OTHER"].ToString() == "Y") { chkBlood.Checked = true; }
                    if (dtHistory.Rows[0]["BACTERIA"].ToString() == "Y") { chkBacteria.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkOtherBiologicalHealtHazard.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtOthersBiologicalHealtHazard.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }
                    if (dtHistory.Rows[0]["FUNGUS"].ToString() == "Y") { chkFungus.Checked = true; }
                    if (dtHistory.Rows[0]["VIRUS"].ToString() == "Y") { chkVirus.Checked = true; }


                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_CHEMICAL_HEALTH_HAZARD");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO_CHEMICAL_HEALTH_HAZARD"].ToString() == "Y") { chkNoChemecal.Checked = true; }
                    if (dtHistory.Rows[0]["ORGANIC"].ToString() == "Y") { chkOrganic.Checked = true; }
                    if (dtHistory.Rows[0]["GAS"].ToString() == "Y") { chkGas.Checked = true; }
                    if (dtHistory.Rows[0]["HEAVY_METAL"].ToString() == "Y") { chkHeavy.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkOtherChemicalhealthhazard.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtOtherChemicalhealthhazard.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }
                    if (dtHistory.Rows[0]["DUST"].ToString() == "Y") { chkDust.Checked = true; }
                    if (dtHistory.Rows[0]["ACID"].ToString() == "Y") { chkAcid.Checked = true; }
                    if (dtHistory.Rows[0]["METAL_FUME"].ToString() == "Y") { chkMetalFume.Checked = true; }
                    if (dtHistory.Rows[0]["HERBICIDE"].ToString() == "Y") { chkHerb.Checked = true; }
                    if (dtHistory.Rows[0]["METAL_POWDERS"].ToString() == "Y") { chkPowder.Checked = true; }


                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_PHYCHOLOGICAL_HEALTH_HAZARD");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoNoPsychological.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoYesPsychological.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { TxtOtherPsychological.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }
                  
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_ERGONOMIC_HEALTH_HAZARD");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO_ERGONOMIC_HEALTH_HAZARD"].ToString() == "Y") { chkNoErgonomic.Checked = true; }
                    if (dtHistory.Rows[0]["POOR_POSTURE"].ToString() == "Y") { chkPoor.Checked = true; }
                    if (dtHistory.Rows[0]["INAPPROPRIATE"].ToString() == "Y") { chkInapp.Checked = true; }
                    if (dtHistory.Rows[0]["REPEATING"].ToString() == "Y") { chkRepeat.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_PPE");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { chkNoPPE.Checked = true; }
                    if (dtHistory.Rows[0]["EARPLUG_EARMUFF"].ToString() == "Y") { chkEarplug.Checked = true; }
                    if (dtHistory.Rows[0]["SAFETY_GLASSES"].ToString() == "Y") { chkSafetyGlass.Checked = true; }
                    if (dtHistory.Rows[0]["HELMET"].ToString() == "Y") { chkHelmet.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkPPEOther.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtPPEOtherDetails.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }
                    if (dtHistory.Rows[0]["SAFETY_SHOES"].ToString() == "Y") { chkSafetyShoe.Checked = true; }
                    if (dtHistory.Rows[0]["GLOVES"].ToString() == "Y") { chkGlove.Checked = true; }
                    if (dtHistory.Rows[0]["COVERALLS"].ToString() == "Y") { chkCoverall.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_MEDICATION_REGULARLY");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoNOmedication_regularly.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoYESmedication_regularly.Checked = true; }


                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_MEDICATION_YOU_ARE_TAKING");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["HEART_DISEASE_MEDICATION"].ToString() == "Y") { chkHeartTaking.Checked = true; }
                    if (dtHistory.Rows[0]["HIGH_BLOOD_PRESSURE_MEDICATION"].ToString() == "Y") { chkHighBloodPressureTaking.Checked = true; }
                    if (dtHistory.Rows[0]["HIGH_BLOOD_LIPIDS_MEDICATION"].ToString() == "Y") { chkHighBloodLipidsTaking.Checked = true; }
                    if (dtHistory.Rows[0]["BLOOD_THINNER"].ToString() == "Y") { chkBloodThinnerTaking.Checked = true; }
                    if (dtHistory.Rows[0]["DIABETES"].ToString() == "Y") { chkDiabetesMedicationTaking.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkOtherDo_you_needTaking.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtOtherDo_you_needDetailsTaking.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }


                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_MEDICATION_MEDICINE_OR_FOOD");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoAre_you_allergic_no.Checked = true; }
                    if (dtHistory.Rows[0]["NOT_SURE"].ToString() == "Y") { rdoAre_you_allergic_not_sure.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { rdoAre_you_allergic_others.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtAre_you_allergic_othersDetails.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }


                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_IMPAIRMENT");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoHave_llness_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoHave_llness_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["DETAILS_ONE"].ToString() != string.Empty) { txtDetails_Illness_row_1.Text = dtHistory.Rows[0]["DETAILS_ONE"].ToString(); }
                    if (dtHistory.Rows[0]["DETAILS_TWO"].ToString() != string.Empty) { txtDetails_Illness_row_2.Text = dtHistory.Rows[0]["DETAILS_TWO"].ToString(); }
                    if (dtHistory.Rows[0]["YEAR_ONE"].ToString() != string.Empty) { txtDetails_Year_row_1.Text = dtHistory.Rows[0]["YEAR_ONE"].ToString(); }
                    if (dtHistory.Rows[0]["YEAR_TWO"].ToString() != string.Empty) { txtDetails_Year_row_2.Text = dtHistory.Rows[0]["YEAR_TWO"].ToString(); }
                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HAD_AN_OPERATION");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoOperationNo.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoOperationYes.Checked = true; }
                    if (dtHistory.Rows[0]["YES_DETAILS"].ToString() != string.Empty) { txtYesDetailOperation.Text = dtHistory.Rows[0]["YES_DETAILS"].ToString(); }
                }


                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_UNDERLYING_DECEASES");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {


                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoUnderlying_deceases_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoUnderlying_deceases_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["SLE"].ToString() == "Y") { chkSLE.Checked = true; }
                    if (dtHistory.Rows[0]["CANCER"].ToString() == "Y") { chkCancer.Checked = true; }
                    if (dtHistory.Rows[0]["DIABETES"].ToString() == "Y") { chkDiabets.Checked = true; }
                    if (dtHistory.Rows[0]["ASTHMA"].ToString() == "Y") { chkUnderlying_deceases_Asthma.Checked = true; }

                    if (dtHistory.Rows[0]["PEPTIC_ULCER"].ToString() == "Y") { chkPeptic_Ulcer.Checked = true; }
                    if (dtHistory.Rows[0]["EPILEPSY"].ToString() == "Y") { chkEpile.Checked = true; }
                    if (dtHistory.Rows[0]["HIGH_BLOOD_PRESSURE"].ToString() == "Y") { chkHigh_blood_pressure_Hypertension.Checked = true; }
                    if (dtHistory.Rows[0]["CHRONIC_OBSTRUCTIVE"].ToString() == "Y") { chkChronic.Checked = true; }


                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkOthers_please_specify.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtOthers_please_specify.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }


                    if (dtHistory.Rows[0]["ANEMIA"].ToString() == "Y") { chkAnemia.Checked = true; }
                    if (dtHistory.Rows[0]["LUNG_EMPHYSEMA"].ToString() == "Y") { chkLung_emphysema.Checked = true; }
                    if (dtHistory.Rows[0]["CARDIOVASCULAR"].ToString() == "Y") { chkCardiovascular.Checked = true; }
                    if (dtHistory.Rows[0]["KIDNEY_DISEASE"].ToString() == "Y") { chkKidney_disease.Checked = true; }
                    if (dtHistory.Rows[0]["HEPATITIS"].ToString() == "Y") { chkHepatitis.Checked = true; }
                    if (dtHistory.Rows[0]["HIGH_BLOOD_LIPIDS"].ToString() == "Y") { chkHigh_blood_lipids_Hyperlipidemia.Checked = true; }


                }



                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_VACCINATION_OR_IMMUNITY");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoVaccination_or_immunity_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoVaccination_or_immunity_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["JE"].ToString() == "Y") { chkJE.Checked = true; }
                    if (dtHistory.Rows[0]["CHICKENPOX"].ToString() == "Y") { chkChickenpox.Checked = true; }
                    if (dtHistory.Rows[0]["INFLUENZA"].ToString() == "Y") { chkInfluenza.Checked = true; }
                    if (dtHistory.Rows[0]["HEPATITIS_A"].ToString() == "Y") { chkHepatitisA.Checked = true; }
                    if (dtHistory.Rows[0]["YELLOW_FEVER"].ToString() == "Y") { chkYellowFever.Checked = true; }
                    if (dtHistory.Rows[0]["MENING"].ToString() == "Y") { chkMeningococcal.Checked = true; }
                    if (dtHistory.Rows[0]["HEPATITIS_B"].ToString() == "Y") { chkHepatitisB.Checked = true; }
                    if (dtHistory.Rows[0]["TETANUS"].ToString() == "Y") { chkTetanus.Checked = true; }
                    if (dtHistory.Rows[0]["TYPHOID"].ToString() == "Y") { chkTyphoid.Checked = true; }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_DO_YOU_SMOKE");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoDo_you_smoke_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoDo_you_smoke_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["YES_BUT"].ToString() == "Y") { rdoDo_you_smoke_Yes_but.Checked = true; }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_SMOKE_BEFORE_QUITTING");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["FIVE_YEAR"].ToString() == "Y") { rdoSmoke_before_quitting_0_5.Checked = true; }
                    if (dtHistory.Rows[0]["SIX_YEAR"].ToString() == "Y") { rdoSmoke_before_quitting_6_10.Checked = true; }
                    if (dtHistory.Rows[0]["OVER_TEN_YEAR"].ToString() == "Y") { rdoSmoke_before_quitting_over_10.Checked = true; }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HOW_MANY_SMOKE_BEFORE_QUITTING");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["FIVE_ROLLS"].ToString() == "Y") { rdoMany_cigarettes_less_5.Checked = true; }
                    if (dtHistory.Rows[0]["TEN_ROWS"].ToString() == "Y") { rdoMany_cigarettes_5_10.Checked = true; }
                    if (dtHistory.Rows[0]["OVER_TEN_ROLLS"].ToString() == "Y") { rdoMany_cigarettes_over_10.Checked = true; }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HOW_LONG_HAVE_YOU_BEEN_SMOKING");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["FIVE_YEAR"].ToString() == "Y") { rdoHow_long_have_you_been_smoking_0_5.Checked = true; }
                    if (dtHistory.Rows[0]["SIX_YEAR"].ToString() == "Y") { rdoHow_long_have_you_been_smoking_6_10.Checked = true; }
                    if (dtHistory.Rows[0]["OVER_TEN_YEAR"].ToString() == "Y") { rdoHow_long_have_you_been_smoking_over_10.Checked = true; }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HOW_MANY_CIGARETTES_DO_YOU_SMOKE_IN_A_DAY");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["FIVE_ROLLS"].ToString() == "Y") { rdoHow_many_cigarettes_do_you_smoke_in_a_day_less_5.Checked = true; }
                    if (dtHistory.Rows[0]["TEN_ROWS"].ToString() == "Y") { rdoHow_many_cigarettes_do_you_smoke_in_a_day_5_10.Checked = true; }
                    if (dtHistory.Rows[0]["OVER_TEN_ROLLS"].ToString() == "Y") { rdoHow_many_cigarettes_do_you_smoke_in_a_day_over_10.Checked = true; }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HAVE_YOU_EVER_THINKING_ABOUT_QUIT_SMOKING");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoHave_you_ever_thinking_about_quit_smoking_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoHave_you_ever_thinking_about_quit_smoking_Yes.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HAVE_YOU_EVER_CONSUMED_ALCOHOL");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoHave_you_ever_consumed_alcohol_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoHave_you_ever_consumed_alcohol_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["YES_BUT"].ToString() == "Y") { rdoHave_you_ever_consumed_alcohol_Yes_But.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HOW_LONG_DID_YOU_DRINK_ALCOHOL_BEFORE_STOP_DRINKING");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["FIVE_YEAR"].ToString() == "Y") { rdoHow_long_did_you_drink_alcohol_before_stop_drinking_0_5.Checked = true; }
                    if (dtHistory.Rows[0]["SIX_YEAR"].ToString() == "Y") { rdoHow_long_did_you_drink_alcohol_before_stop_drinking_6_10.Checked = true; }
                    if (dtHistory.Rows[0]["OVER_TEN_YEAR"].ToString() == "Y") { rdoHow_long_did_you_drink_alcohol_before_stop_drinking_over_10.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HOW_OFTEN_DID_YOU_DRINK_BEFORE_YOU_STOPPED");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["ONE_TIME"].ToString() == "Y") { rdoHow_often_did_you_drink_before_you_stopped_1.Checked = true; }
                    if (dtHistory.Rows[0]["TWO_TIME"].ToString() == "Y") { rdoHow_often_did_you_drink_before_you_stopped_2.Checked = true; }
                    if (dtHistory.Rows[0]["THREE_TIME"].ToString() == "Y") { rdoHow_often_did_you_drink_before_you_stopped_3.Checked = true; }

                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HOW_OFTEN_DO_YOU_CONSUME_ALCOHOL");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["LESS_THAN_ONE_TIME"].ToString() == "Y") { rdoHow_often_do_you_consume_alcohol_Less_than_1_time.Checked = true; }
                    if (dtHistory.Rows[0]["ONE_TIME"].ToString() == "Y") { rdoHow_often_do_you_consume_alcohol_1_time_week.Checked = true; }
                    if (dtHistory.Rows[0]["TWO_TIME"].ToString() == "Y") { rdoHow_often_do_you_consume_alcohol_2_3_week.Checked = true; }
                    if (dtHistory.Rows[0]["MORE_THAN_THREE_TIME"].ToString() == "Y") { rdoHow_often_do_you_consume_alcohol_more_than_3_week.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HAVE_YOU_EVER_THINK_ABOUT_STOP_DRINKING");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoHave_you_ever_think_about_stop_drinking_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoHave_you_ever_think_about_stop_drinking_Yes.Checked = true; }


                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_HAVE_YOU_USE_OR_TRIED_ANY_DRUGS");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoHave_you_use_or_tried_any_drugs_No.Checked = true; }
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoHave_you_use_or_tried_any_drugs_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["YES_BUT"].ToString() == "Y") { rdoHave_you_use_or_tried_any_drugs_Yes_But.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_IIINESS_WHAT_TYPE_OF_DRUGS_DID_YOU_USED");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["MARIJUANA"].ToString() == "Y") { rdoWhat_type_of_drugs_did_you_used_Mari.Checked = true; }
                    if (dtHistory.Rows[0]["AMPHETAMINE"].ToString() == "Y") { rdoWhat_type_of_drugs_did_you_used_Amp.Checked = true; }
                    if (dtHistory.Rows[0]["VOLATILE"].ToString() == "Y") { rdoWhat_type_of_drugs_did_you_used_Volatile.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { rdoWhat_type_of_drugs_did_you_used_Others.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtWhat_type_of_drugs_did_you_used_other.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "OTHER_HEALTH_ISSUES_FAVORITE_FOOD");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["RICE"].ToString() == "Y") { chkWhat_is_your_favorite_food_Rice.Checked = true; }
                    if (dtHistory.Rows[0]["VEGETABLE"].ToString() == "Y") { chkWhat_is_your_favorite_food_Vegetable.Checked = true; }
                    if (dtHistory.Rows[0]["DEEP_FRIED_FOOD"].ToString() == "Y") { chkWhat_is_your_favorite_food_Deep.Checked = true; }
                    if (dtHistory.Rows[0]["SNACK"].ToString() == "Y") { chkWhat_is_your_favorite_food_Snack.Checked = true; }


                    if (dtHistory.Rows[0]["OTHERS"].ToString() == "Y") { chkWhat_is_your_favorite_food_Others.Checked = true; }
                    if (dtHistory.Rows[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtWhat_is_your_favorite_food_Others_details.Text = dtHistory.Rows[0]["OTHERS_DETAILS"].ToString(); }

                    if (dtHistory.Rows[0]["FAST_FOOD"].ToString() == "Y") { chkWhat_is_your_favorite_food_Fast_Food.Checked = true; }
                    if (dtHistory.Rows[0]["FISH"].ToString() == "Y") { chkWhat_is_your_favorite_food_Fish_Lean.Checked = true; }
                    if (dtHistory.Rows[0]["INSTANT_NOODLE"].ToString() == "Y") { chkWhat_is_your_favorite_food_Instant.Checked = true; }

                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoDo_you_exercise_play_sports_No.Checked = true; }
                    if (dtHistory.Rows[0]["LESS_THAN_THREE_TIME"].ToString() == "Y") { rdoDo_you_exercise_play_sports_Less_than_3_times.Checked = true; }
                    if (dtHistory.Rows[0]["MORE_THAN_THREE_TIME"].ToString() == "Y") { rdo_you_exercise_play_sports_More_than_3_times.Checked = true; }
                }

                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "OTHER_HEALTH_ISSUES_DO_YOU_EXERCISE_DURATION");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    // if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoDo_you_exercise_play_sports_No.Checked = true; }
                    if (dtHistory.Rows[0]["LESS_THAN_30_MINUTS"].ToString() == "Y") { rdoWhat_is_your_exercise_duration_Less_than_30.Checked = true; }
                    if (dtHistory.Rows[0]["MORE_THAN_30_MINUTS"].ToString() == "Y") { rdoWhat_is_your_exercise_duration_Over_than_30.Checked = true; }
                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "OTHER_HEALTH_ISSUES_DO_YOU_WANT_TO_DECLARE_PERSONAL");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {
                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoDo_you_want_to_declare_personal_history_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["NO"].ToString() == "Y") { rdoDo_you_want_to_declare_personal_history_No.Checked = true; }

                    if (dtHistory.Rows[1]["DDMMYY"].ToString() != string.Empty) { txtDDMMYY_1.Text = dtHistory.Rows[1]["DDMMYY"].ToString(); }
                    if (dtHistory.Rows[1]["INJURY_OR_ILLNESS"].ToString() != string.Empty) { txtInjury_1.Text = dtHistory.Rows[1]["INJURY_OR_ILLNESS"].ToString(); }
                    if (dtHistory.Rows[1]["CAUSE_OF_INJURY"].ToString() != string.Empty) { txtCause_of_injury_1.Text = dtHistory.Rows[1]["CAUSE_OF_INJURY"].ToString(); }
                    if (dtHistory.Rows[1]["DISABLED"].ToString() != string.Empty) { txtDisabled_1.Text = dtHistory.Rows[1]["DISABLED"].ToString(); }
                    if (dtHistory.Rows[1]["LOSS_OF_LIMBS"].ToString() != string.Empty) { txtLimbs_1.Text = dtHistory.Rows[1]["LOSS_OF_LIMBS"].ToString(); }
                    if (dtHistory.Rows[1]["LESS_THAN_THREE"].ToString() != string.Empty) { txtLessThan_1.Text = dtHistory.Rows[1]["LESS_THAN_THREE"].ToString(); }
                    if (dtHistory.Rows[1]["MORE_THAN_THREE"].ToString() != string.Empty) { txtMoreThan_1.Text = dtHistory.Rows[1]["MORE_THAN_THREE"].ToString(); }

                    if (dtHistory.Rows[0]["DDMMYY"].ToString() != string.Empty) { txtDDMMYY_2.Text = dtHistory.Rows[0]["DDMMYY"].ToString(); }
                    if (dtHistory.Rows[0]["INJURY_OR_ILLNESS"].ToString() != string.Empty) { txtInjury_2.Text = dtHistory.Rows[0]["INJURY_OR_ILLNESS"].ToString(); }
                    if (dtHistory.Rows[0]["CAUSE_OF_INJURY"].ToString() != string.Empty) { txtCause_of_injury_2.Text = dtHistory.Rows[0]["CAUSE_OF_INJURY"].ToString(); }
                    if (dtHistory.Rows[0]["DISABLED"].ToString() != string.Empty) { txtDisabled_2.Text = dtHistory.Rows[0]["DISABLED"].ToString(); }
                    if (dtHistory.Rows[0]["LOSS_OF_LIMBS"].ToString() != string.Empty) { txtLimbs_2.Text = dtHistory.Rows[0]["LOSS_OF_LIMBS"].ToString(); }
                    if (dtHistory.Rows[0]["LESS_THAN_THREE"].ToString() != string.Empty) { txtLessThan_2.Text = dtHistory.Rows[0]["LESS_THAN_THREE"].ToString(); }
                    if (dtHistory.Rows[0]["MORE_THAN_THREE"].ToString() != string.Empty) { txtMoreThan_2.Text = dtHistory.Rows[0]["MORE_THAN_THREE"].ToString(); }

                }
                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "OTHER_HEALTH_ISSUES_DO_YOU_HAVE_MENSTRUAL_PERIODS_AT_PRESENT");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    if (dtHistory.Rows[0]["YES"].ToString() == "Y") { rdoMenstrual_periods_yes.Checked = true; }
                    if (dtHistory.Rows[0]["MENOPAUSE"].ToString() == "Y") { rdoMenstrual.Checked = true; }
                    if (dtHistory.Rows[0]["DATE_FORM"].ToString() != string.Empty) { txtMenoDateForm.Text = dtHistory.Rows[0]["DATE_FORM"].ToString(); }
                    if (dtHistory.Rows[0]["DATE_TO"].ToString() != string.Empty) { txtMenoDateTo.Text = dtHistory.Rows[0]["DATE_TO"].ToString(); }

                    if (dtHistory.Rows[0]["NORMAL"].ToString() == "Y") { rdoCharacteristicNormal.Checked = true; }
                    if (dtHistory.Rows[0]["ABNORMAL"].ToString() == "Y") { rdoCharacteristicABNormal.Checked = true; }
                    if (dtHistory.Rows[0]["PRE_NO"].ToString() == "Y") { rdoPreNo.Checked = true; }
                    if (dtHistory.Rows[0]["PRE_PREGNANCY"].ToString() == "Y") { rdoPregnacy.Checked = true; }
                    if (dtHistory.Rows[0]["PRE_SUSPECTED"].ToString() == "Y") { rdoPregSuspect.Checked = true; }
                    if (dtHistory.Rows[0]["HAS_YES"].ToString() == "Y") { rdoDelivered_Yes.Checked = true; }
                    if (dtHistory.Rows[0]["HAS_NO"].ToString() == "Y") { rdodelivered_No.Checked = true; }
                    if (dtHistory.Rows[0]["HAS_NOT_SURE"].ToString() == "Y") { rdodelivered_not_sure.Checked = true; }
                }


                dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "PERSONAL_FAMILY_IIINESS");
                if (dtHistory != null && dtHistory.Rows.Count > 0)
                {

                    DataRow[] drFather = dtHistory.Select("FAMILY_ID = '1'");

                    if (drFather[0]["NONE"].ToString() == "Y") { chkFather_None.Checked = true; }
                    if (drFather[0]["ANEMIA"].ToString() == "Y") { chkFather_Anemia.Checked = true; }
                    if (drFather[0]["CANCER"].ToString() == "Y") { chkFather_Cancer.Checked = true; }
                    if (drFather[0]["DIABETES"].ToString() == "Y") { chkFather_Diabetes.Checked = true; }
                    if (drFather[0]["ASTHMA"].ToString() == "Y") { chkFather_Asthma.Checked = true; }
                    if (drFather[0]["HIGH_BLOOD_PRESSURE"].ToString() == "Y") { chkfatherHigh_blood_pressure.Checked = true; }
                    if (drFather[0]["ALLERGY"].ToString() == "Y") { chkFather_Allergy.Checked = true; }
                    if (drFather[0]["CARDIOVASCULAR"].ToString() == "Y") { chkFatherCardiovascular.Checked = true; }
                    if (drFather[0]["TUBERCULOSIS"].ToString() == "Y") { chkFather_Tuberculosis.Checked = true; }
                    if (drFather[0]["OTHERS"].ToString() == "Y") { chkFather_Others.Checked = true; }
                    if (drFather[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtFather_other_details.Text = drFather[0]["OTHERS_DETAILS"].ToString(); }

                    DataRow[] drMother = dtHistory.Select("FAMILY_ID = '2'");

                    if (drMother[0]["NONE"].ToString() == "Y") { chkMother_None.Checked = true; }
                    if (drMother[0]["ANEMIA"].ToString() == "Y") { chkMother_Anemia.Checked = true; }
                    if (drMother[0]["CANCER"].ToString() == "Y") { chkMother_Cancer.Checked = true; }
                    if (drMother[0]["DIABETES"].ToString() == "Y") { chkMother_Diabetes.Checked = true; }
                    if (drMother[0]["ASTHMA"].ToString() == "Y") { chkMother_Asthma.Checked = true; }
                    if (drMother[0]["HIGH_BLOOD_PRESSURE"].ToString() == "Y") { chkMotherHigh_blood_pressure.Checked = true; }
                    if (drMother[0]["ALLERGY"].ToString() == "Y") { chkMother_Allergy.Checked = true; }
                    if (drMother[0]["CARDIOVASCULAR"].ToString() == "Y") { chkMotherCardiovascular.Checked = true; }
                    if (drMother[0]["TUBERCULOSIS"].ToString() == "Y") { chkMother_Tuberculosis.Checked = true; }
                    if (drMother[0]["OTHERS"].ToString() == "Y") { chkMother_Others.Checked = true; }
                    if (drMother[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtMother_other_details.Text = drMother[0]["OTHERS_DETAILS"].ToString(); }

                    DataRow[] drSiblings = dtHistory.Select("FAMILY_ID = '3'");

                    if (drSiblings[0]["NONE"].ToString() == "Y") { chkSiblings_None.Checked = true; }
                    if (drSiblings[0]["ANEMIA"].ToString() == "Y") { chkSiblings_Anemia.Checked = true; }
                    if (drSiblings[0]["CANCER"].ToString() == "Y") { chkSiblings_Cancer.Checked = true; }
                    if (drSiblings[0]["DIABETES"].ToString() == "Y") { chkSiblings_Diabetes.Checked = true; }
                    if (drSiblings[0]["ASTHMA"].ToString() == "Y") { chkSiblings_Asthma.Checked = true; }
                    if (drSiblings[0]["HIGH_BLOOD_PRESSURE"].ToString() == "Y") { chkSiblingsHigh_blood_pressure.Checked = true; }
                    if (drSiblings[0]["ALLERGY"].ToString() == "Y") { chkSiblings_Allergy.Checked = true; }
                    if (drSiblings[0]["CARDIOVASCULAR"].ToString() == "Y") { chkSiblingsCardiovascular.Checked = true; }
                    if (drSiblings[0]["TUBERCULOSIS"].ToString() == "Y") { chkSiblings_Tuberculosis.Checked = true; }
                    if (drSiblings[0]["OTHERS"].ToString() == "Y") { chkSiblings_Others.Checked = true; }
                    if (drSiblings[0]["OTHERS_DETAILS"].ToString() != string.Empty) { txtSiblings_other_details.Text = drSiblings[0]["OTHERS_DETAILS"].ToString(); }


                    dtHistory = clsEX.get_history_questionaire(Session["KEY_GEN"].ToString(), "WORKING_HISTORY_ALL");
                    if (dtHistory != null && dtHistory.Rows.Count > 0)
                    {


                        if (dtHistory.Rows[5]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString() != string.Empty) { txtCurrentEmp_2_1_row_1.Text = dtHistory.Rows[5]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString(); }
                        if (dtHistory.Rows[5]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString() != string.Empty) { txtClass_2_1_row_1.Text = dtHistory.Rows[5]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString(); }
                        if (dtHistory.Rows[5]["WORKING_HISTORY_TYPE_OF_WORK"].ToString() != string.Empty) { txtType_2_1_row_1.Text = dtHistory.Rows[5]["WORKING_HISTORY_TYPE_OF_WORK"].ToString(); }
                        if (dtHistory.Rows[5]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString() != string.Empty) { txtPeriod_2_1_row_1_1.Text = dtHistory.Rows[5]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString(); }
                        if (dtHistory.Rows[5]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString() != string.Empty) { txtPeriod_2_1_row_1_2.Text = dtHistory.Rows[5]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString(); }
                        if (dtHistory.Rows[5]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString() != string.Empty) { txtRelated_2_1_row_1.Text = dtHistory.Rows[5]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString(); }
                        if (dtHistory.Rows[5]["WORKING_HISTORYWORK_PPE"].ToString() != string.Empty) { txtDoYou_2_1_row_1.Text = dtHistory.Rows[5]["WORKING_HISTORYWORK_PPE"].ToString(); }

                        if (dtHistory.Rows[4]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString() != string.Empty) { txtCurrentEmp_2_1_row_2.Text = dtHistory.Rows[4]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString(); }
                        if (dtHistory.Rows[4]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString() != string.Empty) { txtClass_2_1_row_2.Text = dtHistory.Rows[4]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString(); }
                        if (dtHistory.Rows[4]["WORKING_HISTORY_TYPE_OF_WORK"].ToString() != string.Empty) { txtType_2_1_row_2.Text = dtHistory.Rows[4]["WORKING_HISTORY_TYPE_OF_WORK"].ToString(); }
                        if (dtHistory.Rows[4]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString() != string.Empty) { txtPeriod_2_1_row_2_1.Text = dtHistory.Rows[4]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString(); }
                        if (dtHistory.Rows[4]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString() != string.Empty) { txtPeriod_2_1_row_2_2.Text = dtHistory.Rows[4]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString(); }
                        if (dtHistory.Rows[4]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString() != string.Empty) { txtRelated_2_1_row_2.Text = dtHistory.Rows[4]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString(); }
                        if (dtHistory.Rows[4]["WORKING_HISTORYWORK_PPE"].ToString() != string.Empty) { txtDoYou_2_1_row_2.Text = dtHistory.Rows[4]["WORKING_HISTORYWORK_PPE"].ToString(); }

                        if (dtHistory.Rows[3]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString() != string.Empty) { txtCurrentEmp_2_1_row_3.Text = dtHistory.Rows[3]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString(); }
                        if (dtHistory.Rows[3]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString() != string.Empty) { txtClass_2_1_row_3.Text = dtHistory.Rows[3]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString(); }
                        if (dtHistory.Rows[3]["WORKING_HISTORY_TYPE_OF_WORK"].ToString() != string.Empty) { txtType_2_1_row_3.Text = dtHistory.Rows[3]["WORKING_HISTORY_TYPE_OF_WORK"].ToString(); }
                        if (dtHistory.Rows[3]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString() != string.Empty) { txtPeriod_2_1_row_3_1.Text = dtHistory.Rows[3]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString(); }
                        if (dtHistory.Rows[3]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString() != string.Empty) { txtPeriod_2_1_row_3_2.Text = dtHistory.Rows[3]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString(); }
                        if (dtHistory.Rows[3]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString() != string.Empty) { txtRelated_2_1_row_3.Text = dtHistory.Rows[3]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString(); }
                        if (dtHistory.Rows[3]["WORKING_HISTORYWORK_PPE"].ToString() != string.Empty) { txtDoYou_2_1_row_3.Text = dtHistory.Rows[3]["WORKING_HISTORYWORK_PPE"].ToString(); }

                        if (dtHistory.Rows[2]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString() != string.Empty) { txtCurrentEmp_2_1_row_4.Text = dtHistory.Rows[2]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString(); }
                        if (dtHistory.Rows[2]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString() != string.Empty) { txtClass_2_1_row_4.Text = dtHistory.Rows[2]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString(); }
                        if (dtHistory.Rows[2]["WORKING_HISTORY_TYPE_OF_WORK"].ToString() != string.Empty) { txtType_2_1_row_4.Text = dtHistory.Rows[2]["WORKING_HISTORY_TYPE_OF_WORK"].ToString(); }
                        if (dtHistory.Rows[2]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString() != string.Empty) { txtPeriod_2_1_row_4_1.Text = dtHistory.Rows[2]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString(); }
                        if (dtHistory.Rows[2]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString() != string.Empty) { txtPeriod_2_1_row_4_2.Text = dtHistory.Rows[2]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString(); }
                        if (dtHistory.Rows[2]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString() != string.Empty) { txtRelated_2_1_row_4.Text = dtHistory.Rows[2]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString(); }
                        if (dtHistory.Rows[2]["WORKING_HISTORYWORK_PPE"].ToString() != string.Empty) { txtDoYou_2_1_row_4.Text = dtHistory.Rows[2]["WORKING_HISTORYWORK_PPE"].ToString(); }


                        if (dtHistory.Rows[1]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString() != string.Empty) { txtCurrentEmp_2_1_row_5.Text = dtHistory.Rows[1]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString(); }
                        if (dtHistory.Rows[1]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString() != string.Empty) { txtClass_2_1_row_5.Text = dtHistory.Rows[1]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString(); }
                        if (dtHistory.Rows[1]["WORKING_HISTORY_TYPE_OF_WORK"].ToString() != string.Empty) { txtType_2_1_row_5.Text = dtHistory.Rows[1]["WORKING_HISTORY_TYPE_OF_WORK"].ToString(); }
                        if (dtHistory.Rows[1]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString() != string.Empty) { txtPeriod_2_1_row_5_1.Text = dtHistory.Rows[1]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString(); }
                        if (dtHistory.Rows[1]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString() != string.Empty) { txtPeriod_2_1_row_5_2.Text = dtHistory.Rows[1]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString(); }
                        if (dtHistory.Rows[1]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString() != string.Empty) { txtRelated_2_1_row_5.Text = dtHistory.Rows[1]["WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS"].ToString(); }
                        if (dtHistory.Rows[1]["WORKING_HISTORYWORK_PPE"].ToString() != string.Empty) { txtDoYou_2_1_row_5.Text = dtHistory.Rows[1]["WORKING_HISTORYWORK_PPE"].ToString(); }



                        if (dtHistory.Rows[0]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString() != string.Empty) { txtCurrentEmp_2_2_row_1.Text = dtHistory.Rows[0]["WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT"].ToString(); }
                        if (dtHistory.Rows[0]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString() != string.Empty) { txtClass_2_2_row_1.Text = dtHistory.Rows[0]["WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT"].ToString(); }
                        if (dtHistory.Rows[0]["WORKING_HISTORY_TYPE_OF_WORK"].ToString() != string.Empty) { txtType_2_2_row_1.Text = dtHistory.Rows[0]["WORKING_HISTORY_TYPE_OF_WORK"].ToString(); }
                        if (dtHistory.Rows[0]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString() != string.Empty) { txtPeriod_2_2_row_1_1.Text = dtHistory.Rows[0]["WORKING_HISTORY_PERIOD_DATE_FROM"].ToString(); }
                        if (dtHistory.Rows[0]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString() != string.Empty) { txtPeriod_2_2_row_1_2.Text = dtHistory.Rows[0]["WORKING_HISTORY_PERIOD_DATE_TO"].ToString(); }

                    }
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }



        }
        private void loadLabel()
        {

            #region
            //patient info
            #region
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
            //end of patient info
            //general Information
            #region

            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_GENERAL_INFORMATIONAL");
            lblGeneralInformation.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'part_1'"));
            lblEmploymentDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'employment_date'"));
            lblEmployer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'employer'"));
            lblWorkLocation.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'work_location'"));
            lblEmail.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'e_mail_address'"));
            lblFunctional.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'functional_group'"));
            lblCurrent.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'current_position'"));
            #endregion
            //end of general Information
            //working history
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORKING_HISTORY");
            lblgrpWorkingHistory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'part_2'"));
            lblHeadWorking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_1_ working_history'"));
            lblCurrentEmp_2_1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'current_employe_department'"));
            lblClass_2_1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'classification'"));
            lblType_2_1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'type_of_work'"));
            lblPeriod_2_1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'period'"));
            lblRelated_2_1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'work_related'"));
            lblDoYou_2_1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'ppe'"));

            //working history
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORKING_HISTORY");
            lblHeadWorkingCurrent.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_2_current_employer_ department'"));
            lblCurrentEmp_2_2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'current_employe_department'"));
            lblClass_2_2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'classification'"));
            lblType_2_2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'type_of_work'"));
            lblPeriod_2_2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'period'"));
            lblRelated_2_2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'work_related'"));
            lblDoYou_2_2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'ppe'"));
            #endregion
            //end of working history
            //Classification 
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_CLASSIFICATION_OF_EMPLOYMENT");
            lblClassification.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_3_classifi'"));
            chkOil.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'oil_natural'"));
            chkNon.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'non_metal'"));
            chkMan.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'manufacture_of_food'"));
            chkBasicMetals.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'basic_ metals'"));
            chkText.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'textiles'"));
            chkMetals.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'metals_products'"));
            chkForrest.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'logging'"));
            chkMotor.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'semi_trailers'"));
            chkPaper.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'paper'"));
            chkPublic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'public_utility'"));
            chkChemecal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'chemecal_petroleum'"));
            chkOtherClassificationofEmployment.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'other'"));
            #endregion
            //end of classification
            //Type of Work 
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_TYPE_OF_WORK");
            lblTypeOfWork.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_4 _type'"));
            rdoOffice.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'office_work'"));
            rdoOnShore.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'onshore_work'"));
            rdoOffShore.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'offshore_work'"));
            #endregion
            //end of type of work
            //Special assignment
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_SPECIAL_ASSIGNMENT");
            lblSpecialAssignment.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_5_special'"));
            chkFire.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'fire_fighting'"));
            chkCon.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'confined'"));
            chkProfes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'driver'"));
            chkLab.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'laboratory'"));
            chkCrane.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'crane'"));
            chkPainter.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'painter'"));
            chkCarter.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'catering'"));
            chkOtherSpecialAssignment.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'others'"));
            #endregion
            //end of Special assignment
            //Physical health hazard
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORK_RELATED_HEALTH_HAZARD");
            lblPhysicalHealthHazard.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_6_1_physical'"));
            chkNophysical.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'no_physical'"));
            chkLight.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'light'"));
            chkCold.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'cold'"));
            chkNoise.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'noise'"));
            chkRadia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'radiation'"));
            chkHeat.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'heat'"));
            chkOthersHazard.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'others_2_6_1'"));
            #endregion
            //end of Physical health hazard
            //Biological
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORK_RELATED_HEALTH_HAZARD");
            lblBiologicalHealtHazard.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_6_2_biological'"));
            chkNobiological.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'no_biological'"));
            chkAnimal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'animal'"));
            chkBlood.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'blood'"));
            chkBacteria.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'bacteria'"));
            chkFungus.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'fungus'"));
            chkVirus.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'virus'"));
            chkOtherBiologicalHealtHazard.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'others_2_6_2'"));
            #endregion
            //end of Biological
            //Chenmical health hazard
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORK_RELATED_HEALTH_HAZARD");
            lblChemicalHealthHazard.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_6_3_chemical'"));
            chkNoChemecal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'no_chemical'"));
            chkOrganic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'organic'"));
            chkGas.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'gas'"));
            chkHeavy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'heavy_metal'"));
            chkAcid.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'acid'"));
            chkMetalFume.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'metal_fume'"));
            chkHerb.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'herbicide'"));
            chkDust.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'dust'"));
            chkPowder.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'metal_powders'"));
            chkOtherChemicalhealthhazard.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'others_2_6_3'"));
            #endregion
            //end of Chemical
            //Psychological
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORK_RELATED_HEALTH_HAZARD");
            lblPsychological.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_6_4_psychological'"));
            rdoNoPsychological.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'no_2_6_4'"));
            rdoYesPsychological.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'yes_2_6_4'"));
            #endregion
            //end of lblPsychological
            //Ergonomic
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORK_RELATED_HEALTH_HAZARD");
            lblErgonomic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_6_5_ergonomic'"));
            chkNoErgonomic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'no_ergonomic'"));
            chkPoor.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'poor_posture'"));
            chkInapp.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'inappropriate'"));
            chkRepeat.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'repeating_movement'"));
            #endregion
            //end of Ergonomic
            //Do you use any Personal
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WORK_RELATED_HEALTH_HAZARD");
            lblDoYouUseAnyPersonal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_6_6_do_you'"));
            chkNoPPE.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '2_6_6_no'"));
            chkEarplug.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'earplug'"));
            chkSafetyGlass.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'safety_glasses'"));
            chkHelmet.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'helmet'"));
            chkSafetyShoe.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'safety_shoes'"));
            chkGlove.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'gloves'"));
            chkCoverall.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'coveralls'"));
            chkPPEOther.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'others_2_6_6'"));
            #endregion
            //end of Do you use any Personal

            //*****Part 3****//
            //3.1 Do you need to take any medication regularly
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblgrpPersonalIllness.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'personal_illness'"));
            lblDo_you_need_to_take_any_medication_regularly.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_1_do'"));
            rdoNOmedication_regularly.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_1_No'"));
            rdoYESmedication_regularly.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_1_yes'"));
            #endregion
            //end of Do you use any Personal
            //3.2 Please select the medication that you are taking.
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblPlease_select_the_medication.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_2_please'"));
            chkHeartTaking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'heart_disease'"));
            chkHighBloodPressureTaking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'high_blood'"));
            chkHighBloodLipidsTaking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'high_blood_lipids'"));
            chkBloodThinnerTaking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'blood_thinner'"));
            chkDiabetesMedicationTaking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'diabetes'"));
            chkOtherDo_you_needTaking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_2_others'"));

            #endregion
            //end Please select the medication that you are taking.
            //3.3 Are you allergic to any medicine or food?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblAre_you_allergic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_3_are'"));
            rdoAre_you_allergic_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_3_no'"));
            rdoAre_you_allergic_not_sure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_3_not_sure'"));
            rdoAre_you_allergic_others.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_3_other'"));
            #endregion
            //end of Are you allergic to any medicine or food?
            //3.4 Have you ever had any severe illness/ impairment?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHave_llness.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_4_have'"));
            rdoHave_llness_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_4_no'"));
            rdoHave_llness_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_4_yes'"));
            lblDetails.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'details'"));
            lblYears.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'year'"));
            #endregion
            //end of Have you ever had any severe illness/ impairment?
            //3.5 Have you ever had an operation?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHave_you_ever_had_an_operation.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_5_have'"));
            rdoOperationNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_5_no'"));
            rdoOperationYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_5_yes'"));
            #endregion
            //end of 3.5 Have you ever had an operation?
            //3.6 Do you have any underlying deceases ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblDo_you_have_any_underlying_deceases.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_6_do'"));
            rdoUnderlying_deceases_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_6_no'"));
            rdoUnderlying_deceases_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_6_yes_please'"));
            chkSLE.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'sle'"));
            chkCancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'cancer'"));
            chkDiabets.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_6_diabetes'"));
            chkUnderlying_deceases_Asthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'asthma'"));
            chkPeptic_Ulcer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'peptic'"));

            chkEpile.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'epilepsy'"));
            chkHigh_blood_pressure_Hypertension.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'high_blood_pressure'"));
            chkChronic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'copo'"));
            chkAnemia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'anemia'"));
            chkLung_emphysema.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'lung'"));

            chkCardiovascular.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'cardiovascular'"));
            chkKidney_disease.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'kidney'"));
            chkHepatitis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'hepatitis'"));
            chkHigh_blood_lipids_Hyperlipidemia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_6_high_blood_lipids'"));
            chkOthers_please_specify.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_6_yes_please'"));
            #endregion
            //end of 3.6 Do you have any underlying deceases ?
            //3.7 Have you ever had any vaccination or immunity ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHave_you_ever_had_any_vaccination_or_immunity.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_7_have'"));
            rdoVaccination_or_immunity_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_7_no'"));
            rdoVaccination_or_immunity_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_7_yes'"));
            chkJE.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'je'"));
            chkChickenpox.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'chickenpox'"));
            chkInfluenza.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'influenza'"));
            chkHepatitisA.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'hepatitis_a'"));
            chkYellowFever.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'yellow_ fever'"));
            chkMeningococcal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'meningococcal'"));
            chkHepatitisB.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'hepatitis_b'"));
            chkTetanus.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'tetanus'"));
            chkTyphoid.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'typhoid'"));
            #endregion
            //end of 3.7 Have you ever had any vaccination or immunity ?
            //3.8 Do you smoke ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblDo_you_smoke.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_8_do_you_smoke'"));
            rdoDo_you_smoke_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_8_do_you_no'"));
            rdoDo_you_smoke_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_8_do_you_yes'"));
            rdoDo_you_smoke_Yes_but.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_8_do_you_yes_but'"));
            #endregion
            //end of 3.8 Do you smoke ?
            //3.9 How long did you smoke before quitting ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblSmoke_before_quitting.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_9_head'"));
            rdoSmoke_before_quitting_0_5.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_9_0_5'"));
            rdoSmoke_before_quitting_6_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_9_0_6'"));
            rdoSmoke_before_quitting_over_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_9_0_10'"));
            #endregion
            //end of 3.9 How long did you smoke before quitting ?
            //3.10 How many cigarettes did you smoke before you quit ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblMany_cigarettes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_10_head'"));
            rdoMany_cigarettes_less_5.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_10_0_5'"));
            rdoMany_cigarettes_5_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_10_0_10'"));
            rdoMany_cigarettes_over_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_10_10'"));
            #endregion
            //end of 3.10 How many cigarettes did you smoke before you quit ?
            //3.11 How long have you been smoking ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHow_long_have_you_been_smoking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_11_head'"));
            rdoHow_long_have_you_been_smoking_0_5.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_11_5'"));
            rdoHow_long_have_you_been_smoking_6_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_11_6'"));
            rdoHow_long_have_you_been_smoking_over_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_11_10'"));
            #endregion
            //end of 3.11 How long have you been smoking ?
            //3.12 How many cigarettes do you smoke in a day?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHow_many_cigarettes_do_you_smoke_in_a_day.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_12_head'"));
            rdoHow_many_cigarettes_do_you_smoke_in_a_day_less_5.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_12_0'"));
            rdoHow_many_cigarettes_do_you_smoke_in_a_day_5_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_12_5'"));
            rdoHow_many_cigarettes_do_you_smoke_in_a_day_over_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_12_10'"));
            #endregion
            //end of 3.12 How many cigarettes do you smoke in a day?
            //3.13 Have you ever thinking about quit smoking ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHave_you_ever_thinking_about_quit_smoking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_13_head'"));
            rdoHave_you_ever_thinking_about_quit_smoking_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_13_no'"));
            rdoHave_you_ever_thinking_about_quit_smoking_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_13_yes'"));

            #endregion
            //end of 3.13 Have you ever thinking about quit smoking ?
            //3.14 Have you ever consumed alcohol ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHave_you_ever_consumed_alcohol.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_14_head'"));
            rdoHave_you_ever_consumed_alcohol_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_14_no'"));
            rdoHave_you_ever_consumed_alcohol_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_14_yes'"));
            rdoHave_you_ever_consumed_alcohol_Yes_But.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_14_but'"));
            #endregion
            //end of 3.14 Have you ever consumed alcohol ?
            //3.15 How long did you drink alcohol before stop drinking ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHow_long_did_you_drink_alcohol_before_stop_drinking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_15_head'"));
            rdoHow_long_did_you_drink_alcohol_before_stop_drinking_0_5.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_15_0'"));
            rdoHow_long_did_you_drink_alcohol_before_stop_drinking_6_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_15_6'"));
            rdoHow_long_did_you_drink_alcohol_before_stop_drinking_over_10.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_15_10'"));
            #endregion
            //end of 3.15 How long did you drink alcohol before stop drinking ?
            //3.16 How often did you drink before you stopped ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHow_often_did_you_drink_before_you_stopped.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_16_head'"));
            rdoHow_often_did_you_drink_before_you_stopped_1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_16_1'"));
            rdoHow_often_did_you_drink_before_you_stopped_2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_16_2'"));
            rdoHow_often_did_you_drink_before_you_stopped_3.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_16_3'"));
            #endregion
            //end of 3.16 How often did you drink before you stopped ?
            //3.17 How often do you consume alcohol ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHow_often_do_you_consume_alcohol.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_17_head'"));
            rdoHow_often_do_you_consume_alcohol_Less_than_1_time.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_17_1'"));
            rdoHow_often_do_you_consume_alcohol_1_time_week.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_17_2'"));
            rdoHow_often_do_you_consume_alcohol_2_3_week.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_17_3'"));
            rdoHow_often_do_you_consume_alcohol_more_than_3_week.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_17_4'"));
            #endregion
            //end of 3.17 How often do you consume alcohol ?
            //3.18 Have you ever think about stop drinking ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lbltblHave_you_ever_think_about_stop_drinking.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_18_head'"));
            rdoHave_you_ever_think_about_stop_drinking_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_18_1'"));
            rdoHave_you_ever_think_about_stop_drinking_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_18_2'"));
            #endregion
            //end of 3.18 Have you ever think about stop drinking ?
            //3.19 Have you use or tried any drugs ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblHave_you_use_or_tried_any_drugs.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_19_head'"));
            rdoHave_you_use_or_tried_any_drugs_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_19_1'"));
            rdoHave_you_use_or_tried_any_drugs_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_19_2'"));
            rdoHave_you_use_or_tried_any_drugs_Yes_But.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_19_3'"));
            #endregion
            //end of 3.19 Have you use or tried any drugs ?
            //3.20 What type of drugs did you used ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_PERSONAL_ILLNESS");
            lblWhat_type_of_drugs_did_you_used.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_20_head'"));
            rdoWhat_type_of_drugs_did_you_used_Mari.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_20_1'"));
            rdoWhat_type_of_drugs_did_you_used_Amp.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_20_2'"));
            rdoWhat_type_of_drugs_did_you_used_Volatile.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_20_3'"));
            rdoWhat_type_of_drugs_did_you_used_Others.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '3_20_4'"));
            #endregion
            //end of 3.20 What type of drugs did you used ?
            //*******End OF Part 3**************//



            //*******Part 4**************//
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_FAMILY_ILLNESS");
            lblgrpFamilyIllness.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_header'"));
            lblFather.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_1_header'"));
            chkFather_None.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_1'"));
            chkFather_Anemia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_2'"));
            chkFather_Cancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_3'"));
            chkFather_Diabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_4'"));
            chkFather_Asthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_5'"));
            chkfatherHigh_blood_pressure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_6'"));
            chkFather_Allergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_7'"));
            chkFatherCardiovascular.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_8'"));
            chkFather_Tuberculosis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_9'"));
            chkFather_Others.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_10'"));

            lblMother.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_2_header'"));
            chkMother_None.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_1'"));
            chkMother_Anemia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_2'"));
            chkMother_Cancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_3'"));
            chkMother_Diabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_4'"));
            chkMother_Asthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_5'"));
            chkMotherHigh_blood_pressure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_6'"));
            chkMother_Allergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_7'"));
            chkMotherCardiovascular.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_8'"));
            chkMother_Tuberculosis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_9'"));
            chkMother_Others.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_10'"));

            lblSiblings.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_3_header'"));
            chkSiblings_None.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_1'"));
            chkSiblings_Anemia.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_2'"));
            chkSiblings_Cancer.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_3'"));
            chkSiblings_Diabetes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_4'"));
            chkSiblings_Asthma.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_5'"));
            chkSiblingsHigh_blood_pressure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_6'"));
            chkSiblings_Allergy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_7'"));
            chkSiblingsCardiovascular.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_8'"));
            chkSiblings_Tuberculosis.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_9'"));
            chkSiblings_Others.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '4_10'"));
            #endregion
            //*******End OF Part 4**************//



            //*******Part 5**************//
            //5.1 What is your favorite food ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_OTHER_HEALTH_ISSUES");
            lblgrpOtherHealthIssues.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_header'"));
            lblWhat_is_your_favorite_food.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_header'"));
            chkWhat_is_your_favorite_food_Rice.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_1'"));
            chkWhat_is_your_favorite_food_Vegetable.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_2'"));
            chkWhat_is_your_favorite_food_Deep.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_3'"));
            chkWhat_is_your_favorite_food_Snack.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_4'"));
            chkWhat_is_your_favorite_food_Fast_Food.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_5'"));
            chkWhat_is_your_favorite_food_Fish_Lean.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_6'"));
            chkWhat_is_your_favorite_food_Instant.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_7'"));
            chkWhat_is_your_favorite_food_Others.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_1_8'"));
            #endregion
            //end of 5.1 What is your favorite food ?

            //5.2 Do you exercise/ play sports ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_OTHER_HEALTH_ISSUES");
            lblDo_you_exercise_play_sports.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_2_header'"));
            rdoDo_you_exercise_play_sports_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_2_1'"));
            rdoDo_you_exercise_play_sports_Less_than_3_times.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_2_2'"));
            rdo_you_exercise_play_sports_More_than_3_times.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_2_3'"));
            #endregion
            //end of 5.2 Do you exercise/ play sports ?

            //5.3 What is your exercise duration ?
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_OTHER_HEALTH_ISSUES");
            lblWhat_is_your_exercise_duration.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_header'"));
            rdoWhat_is_your_exercise_duration_Less_than_30.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1'"));
            rdoWhat_is_your_exercise_duration_Over_than_30.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_2'"));
            #endregion
            //end of 5.3 What is your exercise duration ?

            //5.3.1 Do you want to declare personal history
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_OTHER_HEALTH_ISSUES");
            lblDo_you_want_to_declare_personal_history.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_header'"));
            rdoDo_you_want_to_declare_personal_history_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_yes'"));
            rdoDo_you_want_to_declare_personal_history_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_no'"));

            lblDDMM.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_1'"));
            lblPartOF.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_2'"));
            lblCause.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_3'"));
            lblServerity.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_4'"));
            lblDisabled.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_5'"));
            lblLossof.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_6'"));
            lblTemporary.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_7'"));
            lblLess_than_3.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_8'"));
            lblMore_than_3.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_3_1_1_9'"));

            lblMenstrual_periods.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_4_header'"));
            rdoMenstrual_periods_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_4_yes'"));
            rdoMenstrual.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = '5_4_no'"));
            #endregion
            //end of 5.3 What is your exercise duration ?

            //5.4 Woman//
            #region
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_WOMAN");
            lblFor_women.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'wo_header'"));
            lblMenoFirstDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_1'"));
            lblMenoToDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_2'"));
            lblCharacteristic.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_3'"));
            lblCharacteristicNomal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_7'"));
            lblCharacteristicAbnomal.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_8'"));
            lblPreg.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_4'"));
            rdoPreNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_9'"));
            rdoPregnacy.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_10'"));
            rdoPregSuspect.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_11'"));

            lbldelivered.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_5'"));
            rdoDelivered_Yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_12'"));
            rdodelivered_No.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_13'"));
            rdodelivered_not_sure.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_14'"));

            lblperiod_is_over_7_days.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_6'"));
            lblperiod_is_less_7_days.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'w_15'"));

            #endregion
            // end of Woman
            //*******End of Part 5**************//

            #endregion
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Session["key_gen"] = ut.GET_USER_ID_RESULT();
                Session["CREATE_BY"] = "Admin";
                Session["UPDATE_BY"] = "Admin";
                save_patient('1');
                save_WorkHistory();
                save_personal_Illness();
                save_family_Illness();
                save_health_other_issue();

                Response.Redirect("default.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void btnSaveDraft_Click(object sender, EventArgs e)
        {
            try
            {
                Session["key_gen"] = ut.GET_USER_ID_RESULT();
                Session["CREATE_BY"] = "Admin";
                Session["UPDATE_BY"] = "Admin";
                save_patient('0');
                save_WorkHistory();
                save_personal_Illness();
                save_family_Illness();
                save_health_other_issue();

                Response.Redirect("~/default.aspx");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        private void save_patient(char draft)
        {
            try
            {

                clsPatientINFO clsPatient = new clsPatientINFO();
                clsPatient.TITLE_NAME_EN = string.Empty;
                clsPatient.TITLE_NAME_TH = string.Empty;
                clsPatient.FIRST_NAME_EN = string.Empty;
                clsPatient.FIRST_NAME_TH = string.Empty;
                clsPatient.LAST_NAME_EN = string.Empty;
                clsPatient.LAST_NAME_TH = string.Empty;
                clsPatient.FULL_NAME_EN = string.Empty;
                clsPatient.HN = Session["HN"].ToString().Trim();
                clsPatient.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                clsPatient.FULL_NAME = txtName.Text.Trim();
                clsPatient.ROOM = txtRoom.Text.Trim();
                clsPatient.PHYSICIAN = txtPhysician.Text.Trim();
                clsPatient.VISIT_DATE = ut.ConvertDateToStringFormat(txtVisitDate.Text.Trim(), "yyyy-MM-dd");
                clsPatient.DEPARTMENT = txtDepartment.Text.Trim();
                clsPatient.BIRTH_DATE = ut.ConvertDateToStringFormat(txtBirthDate.Text.Trim(), "yyyy-MM-dd");
                clsPatient.AGE = txtAge.Text.Trim();
                clsPatient.SEX = txtSex.Text.Trim();
                clsPatient.ALLERGIES = txtAllergies.Text.Trim();
                clsPatient.LANGUAGE = Session["LEGION"].ToString();
                clsPatient.DRAFT = draft;
                clsPatient.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsPatient.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_patients(clsPatient);

                clsGeneralInformation clsGeneral = new clsGeneralInformation();
                clsGeneral.HN = Session["HN"].ToString().Trim();
                clsGeneral.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                clsGeneral.EMPLOYMENT_DATE = ut.ConvertDateToStringFormat(txtEmploymentDate.Text.Trim(), "yyyy-MM-dd");
                clsGeneral.EMPLOYER = txtEmployer.Text.Trim();
                clsGeneral.WORK_LOCATION = txtWorkLocation.Text.Trim();
                clsGeneral.EMAIL_ADDRESS = txtEmail.Text.Trim();
                clsGeneral.FUNCTIONAL_GROUP = txtFunctional.Text.Trim();
                clsGeneral.CURRENT_POSITION = txtCurrent.Text.Trim();
                clsGeneral.TELEPHONE = string.Empty;
                clsGeneral.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsGeneral.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_general_informational(clsGeneral);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
        private void save_WorkHistory()
        {

            #region
            clsWorkingHistory clsWorkHistory = new clsWorkingHistory();
            clsWorkHistory.HN = Session["HN"].ToString().Trim();
            clsWorkHistory.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            clsWorkHistory.WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT = txtCurrentEmp_2_1_row_1.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT = txtClass_2_1_row_1.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_TYPE_OF_WORK = txtType_2_1_row_1.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_FROM = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_1_1.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_TO = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_1_2.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS = txtRelated_2_1_row_1.Text.Trim();
            clsWorkHistory.WORKING_HISTORYWORK_PPE = txtDoYou_2_1_row_1.Text.Trim();
            clsWorkHistory.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsWorkHistory.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_All(clsWorkHistory);

            clsWorkHistory.HN = Session["HN"].ToString().Trim();
            clsWorkHistory.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            clsWorkHistory.WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT = txtCurrentEmp_2_1_row_2.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT = txtClass_2_1_row_2.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_TYPE_OF_WORK = txtType_2_1_row_2.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_FROM = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_2_1.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_TO = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_2_2.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS = txtRelated_2_1_row_2.Text.Trim();
            clsWorkHistory.WORKING_HISTORYWORK_PPE = txtDoYou_2_1_row_2.Text.Trim();
            clsWorkHistory.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsWorkHistory.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_All(clsWorkHistory);


            clsWorkHistory.HN = Session["HN"].ToString().Trim();
            clsWorkHistory.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            clsWorkHistory.WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT = txtCurrentEmp_2_1_row_3.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT = txtClass_2_1_row_3.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_TYPE_OF_WORK = txtType_2_1_row_3.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_FROM = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_3_1.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_TO = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_3_2.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS = txtRelated_2_1_row_3.Text.Trim();
            clsWorkHistory.WORKING_HISTORYWORK_PPE = txtDoYou_2_1_row_3.Text.Trim();
            clsWorkHistory.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsWorkHistory.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_All(clsWorkHistory);

            clsWorkHistory.HN = Session["HN"].ToString().Trim();
            clsWorkHistory.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            clsWorkHistory.WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT = txtCurrentEmp_2_1_row_4.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT = txtClass_2_1_row_4.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_TYPE_OF_WORK = txtType_2_1_row_4.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_FROM = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_4_1.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_TO = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_4_2.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS = txtRelated_2_1_row_4.Text.Trim();
            clsWorkHistory.WORKING_HISTORYWORK_PPE = txtDoYou_2_1_row_4.Text.Trim();
            clsWorkHistory.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsWorkHistory.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_All(clsWorkHistory);

            clsWorkHistory.HN = Session["HN"].ToString().Trim();
            clsWorkHistory.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            clsWorkHistory.WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT = txtCurrentEmp_2_1_row_5.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT = txtClass_2_1_row_5.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_TYPE_OF_WORK = txtType_2_1_row_5.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_FROM = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_5_1.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_TO = ut.ConvertDateToStringFormat(txtPeriod_2_1_row_5_2.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS = txtRelated_2_1_row_5.Text.Trim();
            clsWorkHistory.WORKING_HISTORYWORK_PPE = txtDoYou_2_1_row_5.Text.Trim();
            clsWorkHistory.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsWorkHistory.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_All(clsWorkHistory);



            clsWorkHistory.HN = Session["HN"].ToString().Trim();
            clsWorkHistory.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            clsWorkHistory.WORKING_HISTORY_CURRENT_EMPLOYER_DEPARTMENT = txtCurrentEmp_2_2_row_1.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_CLASSIFICATION_OF_EMPLOYMENT = txtClass_2_2_row_1.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_TYPE_OF_WORK = txtType_2_2_row_1.Text.Trim();
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_FROM = ut.ConvertDateToStringFormat(txtPeriod_2_2_row_1_1.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORY_PERIOD_DATE_TO = ut.ConvertDateToStringFormat(txtPeriod_2_2_row_1_2.Text.Trim(), "yyyy-MM-dd");
            clsWorkHistory.WORKING_HISTORYWORK_RELATED_HEALTH_HAZARDS = string.Empty;
            clsWorkHistory.WORKING_HISTORYWORK_PPE = string.Empty;
            clsWorkHistory.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsWorkHistory.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_All(clsWorkHistory);

            clsWorkingHistoryClassificationOfEmployment clsClassification = new clsWorkingHistoryClassificationOfEmployment();
            clsClassification.HN = Session["HN"].ToString().Trim();
            if (chkOil.Checked) { clsClassification.OIL_NATURAL_GAS = 'Y'; } else { clsClassification.OIL_NATURAL_GAS = 'N'; }
            if (chkNon.Checked) { clsClassification.NON_METAL_PRODUCTS = 'Y'; } else { clsClassification.NON_METAL_PRODUCTS = 'N'; }
            if (chkMan.Checked) { clsClassification.MANUFACTURE_OF_FOOD = 'Y'; } else { clsClassification.MANUFACTURE_OF_FOOD = 'N'; }
            if (chkBasicMetals.Checked) { clsClassification.MANUFACTURE_OF_BASIC_METALS = 'Y'; } else { clsClassification.MANUFACTURE_OF_BASIC_METALS = 'N'; }
            if (chkText.Checked) { clsClassification.MANUFACTURE_OF_TEXTILES = 'Y'; } else { clsClassification.MANUFACTURE_OF_TEXTILES = 'N'; }
            if (chkMetals.Checked) { clsClassification.METALS_PRODUCTS = 'Y'; } else { clsClassification.METALS_PRODUCTS = 'N'; }
            if (chkForrest.Checked) { clsClassification.FORESTRY_AND_LOGGING = 'Y'; } else { clsClassification.FORESTRY_AND_LOGGING = 'N'; }
            if (chkMotor.Checked) { clsClassification.MANUFACTURE_OF_MOTOR = 'Y'; } else { clsClassification.MANUFACTURE_OF_MOTOR = 'N'; }
            if (chkPaper.Checked) { clsClassification.MANUFACTURE_OF_PAPER = 'Y'; } else { clsClassification.MANUFACTURE_OF_PAPER = 'N'; }
            if (chkPublic.Checked) { clsClassification.PUBLIC_UTILITY = 'Y'; } else { clsClassification.PUBLIC_UTILITY = 'N'; }
            if (chkChemecal.Checked) { clsClassification.MANUFACTURE_OF_CHEMICAL = 'Y'; } else { clsClassification.MANUFACTURE_OF_CHEMICAL = 'N'; }
            if (chkOtherClassificationofEmployment.Checked) { clsClassification.OTHERS = 'Y'; } else { clsClassification.OTHERS = 'N'; }
            clsClassification.OTHERS_DETAILS = txtOtherClassificationofEmployment.Text.Trim();
            clsClassification.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsClassification.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsClassification.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            clsEX.insert_trn_working_history_classification_of_employment(clsClassification);


            clsWorkingHistoryTypeOfWork clsTypeOfWork = new clsWorkingHistoryTypeOfWork();
            clsTypeOfWork.HN = Session["HN"].ToString().Trim();
            clsTypeOfWork.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (rdoOffice.Checked) { clsTypeOfWork.OFFICE_WORK = 'Y'; } else { clsTypeOfWork.OFFICE_WORK = 'N'; }
            if (rdoOffShore.Checked) { clsTypeOfWork.OFFSHORE_WORK = 'Y'; } else { clsTypeOfWork.OFFSHORE_WORK = 'N'; }
            if (rdoOnShore.Checked) { clsTypeOfWork.ONSHORE_WORK = 'Y'; } else { clsTypeOfWork.ONSHORE_WORK = 'N'; }
            clsTypeOfWork.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsTypeOfWork.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_type_of_work(clsTypeOfWork);


            clsWorkingHistorySpecialAssignment clsAssignment = new clsWorkingHistorySpecialAssignment();
            clsAssignment.HN = Session["HN"].ToString().Trim();
            clsAssignment.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (chkFire.Checked) { clsAssignment.FIRE_FIGHTING_STAFF = 'Y'; } else { clsAssignment.FIRE_FIGHTING_STAFF = 'N'; }
            if (chkCon.Checked) { clsAssignment.CONFINED_SPACE_WORKER = 'Y'; } else { clsAssignment.CONFINED_SPACE_WORKER = 'N'; }
            if (chkProfes.Checked) { clsAssignment.PROFESSIONAL_DRIVER = 'Y'; } else { clsAssignment.PROFESSIONAL_DRIVER = 'N'; }
            if (chkLab.Checked) { clsAssignment.LABORATORY_TECHNICIAN = 'Y'; } else { clsAssignment.LABORATORY_TECHNICIAN = 'N'; }
            if (chkCrane.Checked) { clsAssignment.CRANE_OPERATOR = 'Y'; } else { clsAssignment.CRANE_OPERATOR = 'N'; }
            if (chkPainter.Checked) { clsAssignment.PAINTER = 'Y'; } else { clsAssignment.PAINTER = 'N'; }
            if (chkCarter.Checked) { clsAssignment.CATERING_AND_FOOD = 'Y'; } else { clsAssignment.CATERING_AND_FOOD = 'N'; }
            if (chkOtherSpecialAssignment.Checked) { clsAssignment.OTHERS = 'Y'; } else { clsAssignment.OTHERS = 'N'; }
            clsAssignment.OTHERS_DETAILS = txtOtherSpecialAssignment.Text.Trim();
            clsAssignment.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsAssignment.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_insert_trn_working_history_special_assignment(clsAssignment);

            clsWorkingHistoryPhysicalHealthHazard clsPhysical = new clsWorkingHistoryPhysicalHealthHazard();
            clsPhysical.HN = Session["HN"].ToString().Trim();
            clsPhysical.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (chkNophysical.Checked) { clsPhysical.NO_PHYSICAL_HEALTH_HAZARD = 'Y'; } else { clsPhysical.NO_PHYSICAL_HEALTH_HAZARD = 'N'; }
            if (chkLight.Checked) { clsPhysical.LIGHT = 'Y'; } else { clsPhysical.LIGHT = 'N'; }
            if (chkCold.Checked) { clsPhysical.COLD = 'Y'; } else { clsPhysical.COLD = 'N'; }
            if (chkNoise.Checked) { clsPhysical.NOISE = 'Y'; } else { clsPhysical.NOISE = 'N'; }
            if (chkRadia.Checked) { clsPhysical.RADIATION = 'Y'; } else { clsPhysical.RADIATION = 'N'; }
            if (chkHeat.Checked) { clsPhysical.HEAT = 'Y'; } else { clsPhysical.HEAT = 'N'; }
            if (chkOthersHazard.Checked) { clsPhysical.OTHERS = 'Y'; } else { clsPhysical.OTHERS = 'N'; }
            clsPhysical.OTHERS_DETAILS = txtOthersHazard.Text.Trim();
            clsPhysical.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsPhysical.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_physical_health_hazard(clsPhysical);


            clsWorkingHistoryBiologicalHealthHazard clsBiological = new clsWorkingHistoryBiologicalHealthHazard();
            clsBiological.HN = Session["HN"].ToString().Trim();
            clsBiological.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (chkNobiological.Checked) { clsBiological.NO_BIOLOGICAL_HEALTH_HAZARD = 'Y'; } else { clsBiological.NO_BIOLOGICAL_HEALTH_HAZARD = 'N'; }
            if (chkAnimal.Checked) { clsBiological.ANIMAL_CARRIERS = 'Y'; } else { clsBiological.ANIMAL_CARRIERS = 'N'; }
            if (chkBlood.Checked) { clsBiological.BLOOD_OR_OTHER = 'Y'; } else { clsBiological.BLOOD_OR_OTHER = 'N'; }
            if (chkBacteria.Checked) { clsBiological.BACTERIA = 'Y'; } else { clsBiological.BACTERIA = 'N'; }
            if (chkFungus.Checked) { clsBiological.FUNGUS = 'Y'; } else { clsBiological.FUNGUS = 'N'; }
            if (chkVirus.Checked) { clsBiological.VIRUS = 'Y'; } else { clsBiological.VIRUS = 'N'; }
            if (chkOtherBiologicalHealtHazard.Checked) { clsBiological.OTHERS = 'Y'; } else { clsBiological.OTHERS = 'N'; }
            clsBiological.OTHERS_DETAILS = txtOthersBiologicalHealtHazard.Text.Trim();
            clsBiological.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsBiological.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_biological_health_hazard(clsBiological);

            clsWorkingHistoryChemicalHealthHazard clsChemical = new clsWorkingHistoryChemicalHealthHazard();
            clsChemical.HN = Session["HN"].ToString().Trim();
            clsChemical.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (chkNoChemecal.Checked) { clsChemical.NO_CHEMICAL_HEALTH_HAZARD = 'Y'; } else { clsChemical.NO_CHEMICAL_HEALTH_HAZARD = 'N'; }
            if (chkOrganic.Checked) { clsChemical.ORGANIC = 'Y'; } else { clsChemical.ORGANIC = 'N'; }
            if (chkGas.Checked) { clsChemical.GAS = 'Y'; } else { clsChemical.GAS = 'N'; }
            if (chkHeavy.Checked) { clsChemical.HEAVY_METAL = 'Y'; } else { clsChemical.HEAVY_METAL = 'N'; }
            if (chkAcid.Checked) { clsChemical.ACID = 'Y'; } else { clsChemical.ACID = 'N'; }
            if (chkMetalFume.Checked) { clsChemical.METAL_FUME = 'Y'; } else { clsChemical.METAL_FUME = 'N'; }
            if (chkHerb.Checked) { clsChemical.HERBICIDE = 'Y'; } else { clsChemical.HERBICIDE = 'N'; }
            if (chkDust.Checked) { clsChemical.DUST = 'Y'; } else { clsChemical.DUST = 'N'; }
            if (chkPowder.Checked) { clsChemical.METAL_POWDERS = 'Y'; } else { clsChemical.METAL_POWDERS = 'N'; }
            if (chkOtherChemicalhealthhazard.Checked) { clsChemical.OTHERS = 'Y'; } else { clsChemical.OTHERS = 'N'; }
            clsChemical.OTHERS_DETAILS = txtOtherChemicalhealthhazard.Text.Trim();
            clsChemical.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsChemical.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_chemical_health_hazard(clsChemical);


            clsWorkingHistoryPsychologicalHealthHazard clsPsycho = new clsWorkingHistoryPsychologicalHealthHazard();
            clsPsycho.HN = Session["HN"].ToString().Trim();
            clsPsycho.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (rdoNoPsychological.Checked) { clsPsycho.NO = 'Y'; } else { clsPsycho.NO = 'N'; }
            if (rdoYesPsychological.Checked) { clsPsycho.YES = 'Y'; } else { clsPsycho.YES = 'N'; }
            clsPsycho.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsPsycho.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_phychological_health_hazard(clsPsycho);

            clsWorkingHistoryErgonomicHealthHazard clsErgonomic = new clsWorkingHistoryErgonomicHealthHazard();
            clsErgonomic.HN = Session["HN"].ToString().Trim();
            clsErgonomic.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (chkNoErgonomic.Checked) { clsErgonomic.NO_ERGONOMIC_HEALTH_HAZARD = 'Y'; } else { clsErgonomic.NO_ERGONOMIC_HEALTH_HAZARD = 'N'; }
            if (chkPoor.Checked) { clsErgonomic.POOR_POSTURE = 'Y'; } else { clsErgonomic.POOR_POSTURE = 'N'; }
            if (chkInapp.Checked) { clsErgonomic.INAPPROPRIATE = 'Y'; } else { clsErgonomic.INAPPROPRIATE = 'N'; }
            if (chkRepeat.Checked) { clsErgonomic.REPEATING = 'Y'; } else { clsErgonomic.REPEATING = 'N'; }
            clsErgonomic.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsErgonomic.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_ergonomic_health_hazard(clsErgonomic);

            clsWorkingHistoryPPE clsPPE = new clsWorkingHistoryPPE();
            clsPPE.HN = Session["HN"].ToString().Trim();
            clsPPE.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
            if (chkNoPPE.Checked) { clsPPE.NO = 'Y'; } else { clsPPE.NO = 'N'; }
            if (chkEarplug.Checked) { clsPPE.EARPLUG_EARMUFF = 'Y'; } else { clsPPE.EARPLUG_EARMUFF = 'N'; }
            if (chkSafetyGlass.Checked) { clsPPE.SAFETY_GLASSES = 'Y'; } else { clsPPE.SAFETY_GLASSES = 'N'; }
            if (chkHelmet.Checked) { clsPPE.HELMET = 'Y'; } else { clsPPE.HELMET = 'N'; }
            if (chkSafetyShoe.Checked) { clsPPE.SAFETY_SHOES = 'Y'; } else { clsPPE.SAFETY_SHOES = 'N'; }
            if (chkGlove.Checked) { clsPPE.GLOVES = 'Y'; } else { clsPPE.GLOVES = 'N'; }
            if (chkCoverall.Checked) { clsPPE.COVERALLS = 'Y'; } else { clsPPE.COVERALLS = 'N'; }
            if (chkPPEOther.Checked) { clsPPE.OTHERS = 'Y'; } else { clsPPE.OTHERS = 'N'; }
            clsPPE.OTHERS_DETAILS = txtPPEOtherDetails.Text.Trim();
            clsPPE.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
            clsPPE.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
            clsEX.insert_trn_working_history_ppe(clsPPE);

            #endregion
        }
        protected void chkOtherClassificationofEmployment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkOtherClassificationofEmployment.Checked) { txtOtherClassificationofEmployment.Enabled = true; }
                else { txtOtherClassificationofEmployment.Enabled = false; txtOtherClassificationofEmployment.Text = string.Empty; }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
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
        protected void save_personal_Illness()
        {
            try
            {
                clsPersonalIllnessMedicationRegularly clsMediRegulary = new clsPersonalIllnessMedicationRegularly();
                clsMediRegulary.HN = Session["HN"].ToString().Trim();
                clsMediRegulary.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoNOmedication_regularly.Checked) { clsMediRegulary.NO = 'Y'; } else { clsMediRegulary.NO = 'N'; }
                if (rdoYESmedication_regularly.Checked) { clsMediRegulary.YES = 'Y'; } else { clsMediRegulary.YES = 'N'; }
                clsMediRegulary.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsMediRegulary.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_medication_regularly(clsMediRegulary);

                clsPersonalIllnessMedicationThatYouAreTaking clsTaking = new clsPersonalIllnessMedicationThatYouAreTaking();
                clsTaking.HN = Session["HN"].ToString().Trim();
                clsTaking.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (chkHeartTaking.Checked) { clsTaking.HEART_DISEASE_MEDICATION = 'Y'; } else { clsTaking.HEART_DISEASE_MEDICATION = 'N'; }
                if (chkHighBloodPressureTaking.Checked) { clsTaking.HIGH_BLOOD_PRESSURE_MEDICATION = 'Y'; } else { clsTaking.HIGH_BLOOD_PRESSURE_MEDICATION = 'N'; }
                if (chkHighBloodLipidsTaking.Checked) { clsTaking.HIGH_BLOOD_LIPIDS_MEDICATION = 'Y'; } else { clsTaking.HIGH_BLOOD_LIPIDS_MEDICATION = 'N'; }
                if (chkBloodThinnerTaking.Checked) { clsTaking.BLOOD_THINNER = 'Y'; } else { clsTaking.BLOOD_THINNER = 'N'; }
                if (chkDiabetesMedicationTaking.Checked) { clsTaking.DIABETES = 'Y'; } else { clsTaking.DIABETES = 'N'; }
                if (chkOtherDo_you_needTaking.Checked) { clsTaking.OTHERS = 'Y'; } else { clsTaking.OTHERS = 'N'; }
                clsTaking.OTHERS_DETAILS = txtOtherDo_you_needDetailsTaking.Text.Trim();
                clsTaking.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsTaking.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_medication_you_are_taking(clsTaking);

                clsPersonalIllnessMedicineOrFood clsMedFood = new clsPersonalIllnessMedicineOrFood();
                clsMedFood.HN = clsTaking.HN = Session["HN"].ToString().Trim();
                clsMedFood.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoAre_you_allergic_no.Checked) { clsMedFood.NO = 'Y'; } else { clsMedFood.NO = 'N'; }
                if (rdoAre_you_allergic_not_sure.Checked) { clsMedFood.YES = 'Y'; } else { clsMedFood.YES = 'N'; }
                if (rdoAre_you_allergic_others.Checked) { clsMedFood.OTHERS = 'Y'; } else { clsMedFood.OTHERS = 'N'; }
                clsMedFood.OTHERS_DETAILS = txtAre_you_allergic_othersDetails.Text.Trim();
                clsMedFood.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsMedFood.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_medication_medicine_or_food(clsMedFood);

                clsPersonalIllnessillnessImpairment clsImpairment = new clsPersonalIllnessillnessImpairment();
                clsImpairment.HN = clsTaking.HN = Session["HN"].ToString().Trim();
                clsImpairment.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHave_llness_No.Checked) { clsImpairment.NO = 'Y'; } else { clsImpairment.NO = 'N'; }
                if (rdoHave_llness_Yes.Checked) { clsImpairment.YES = 'Y'; } else { clsImpairment.YES = 'N'; }
                clsImpairment.DETAILS_ONE = txtDetails_Illness_row_1.Text.Trim();
                clsImpairment.DETAILS_TWO = txtDetails_Illness_row_2.Text.Trim();
                clsImpairment.YEAR_ONE = ut.ConvertDateToStringFormat(txtDetails_Year_row_1.Text.Trim(), "yyyy-MM-dd");
                clsImpairment.YEAR_TWO = ut.ConvertDateToStringFormat(txtDetails_Year_row_2.Text.Trim(), "yyyy-MM-dd");
                clsImpairment.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsImpairment.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_impairment(clsImpairment);

                clsPersonalIllnessHadAnOperation clsOperation = new clsPersonalIllnessHadAnOperation();
                clsOperation.HN = Session["HN"].ToString().Trim();
                clsOperation.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoOperationNo.Checked) { clsOperation.NO = 'Y'; } else { clsOperation.NO = 'N'; }
                if (rdoOperationYes.Checked) { clsOperation.YES = 'Y'; } else { clsOperation.YES = 'N'; }
                clsOperation.OTHERS_DETAILS = txtYesDetailOperation.Text.Trim();
                clsOperation.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsOperation.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_had_an_operation(clsOperation);

                clsPersonalIllnessUnderlyingDeceases clsDecease = new clsPersonalIllnessUnderlyingDeceases();
                clsDecease.HN = Session["HN"].ToString().Trim();
                clsDecease.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoUnderlying_deceases_No.Checked) { clsDecease.NO = 'Y'; } else { clsDecease.NO = 'N'; }
                if (rdoUnderlying_deceases_Yes.Checked) { clsDecease.YES = 'Y'; } else { clsDecease.YES = 'N'; }
                if (chkSLE.Checked) { clsDecease.SLE = 'Y'; } else { clsDecease.SLE = 'N'; }
                if (chkCancer.Checked) { clsDecease.CANCER = 'Y'; } else { clsDecease.CANCER = 'N'; }
                if (chkDiabets.Checked) { clsDecease.DIABETES = 'Y'; } else { clsDecease.DIABETES = 'N'; }
                if (chkUnderlying_deceases_Asthma.Checked) { clsDecease.ASTHMA = 'Y'; } else { clsDecease.ASTHMA = 'N'; }
                if (chkPeptic_Ulcer.Checked) { clsDecease.PEPTIC_ULCER = 'Y'; } else { clsDecease.PEPTIC_ULCER = 'N'; }
                if (chkEpile.Checked) { clsDecease.EPILEPSY = 'Y'; } else { clsDecease.EPILEPSY = 'N'; }
                if (chkHigh_blood_pressure_Hypertension.Checked) { clsDecease.HIGH_BLOOD_PRESSURE = 'Y'; } else { clsDecease.HIGH_BLOOD_PRESSURE = 'N'; }
                if (chkChronic.Checked) { clsDecease.CHRONIC_OBSTRUCTIVE = 'Y'; } else { clsDecease.CHRONIC_OBSTRUCTIVE = 'N'; }
                if (chkAnemia.Checked) { clsDecease.ANEMIA = 'Y'; } else { clsDecease.ANEMIA = 'N'; }
                if (chkLung_emphysema.Checked) { clsDecease.LUNG_EMPHYSEMA = 'Y'; } else { clsDecease.LUNG_EMPHYSEMA = 'N'; }
                if (chkCardiovascular.Checked) { clsDecease.CARDIOVASCULAR = 'Y'; } else { clsDecease.CARDIOVASCULAR = 'N'; }
                if (chkKidney_disease.Checked) { clsDecease.KIDNEY_DISEASE = 'Y'; } else { clsDecease.KIDNEY_DISEASE = 'N'; }
                if (chkHepatitis.Checked) { clsDecease.HEPATITIS = 'Y'; } else { clsDecease.HEPATITIS = 'N'; }
                if (chkHigh_blood_lipids_Hyperlipidemia.Checked) { clsDecease.HIGH_BLOOD_LIPIDS = 'Y'; } else { clsDecease.HIGH_BLOOD_LIPIDS = 'N'; }
                if (chkOthers_please_specify.Checked) { clsDecease.OTHERS = 'Y'; } else { clsDecease.OTHERS = 'N'; }
                clsDecease.OTHERS_DETAILS = txtOthers_please_specify.Text.Trim();
                clsDecease.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsDecease.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_underlying_deceases(clsDecease);

                clsPersonalIllnessVaccinationOrImmunity clsImmunity = new clsPersonalIllnessVaccinationOrImmunity();
                clsImmunity.HN = Session["HN"].ToString().Trim();
                clsImmunity.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoVaccination_or_immunity_No.Checked) { clsImmunity.NO = 'Y'; } else { clsImmunity.NO = 'N'; }
                if (rdoVaccination_or_immunity_Yes.Checked) { clsImmunity.YES = 'Y'; } else { clsImmunity.YES = 'N'; }
                if (chkJE.Checked) { clsImmunity.JE = 'Y'; } else { clsImmunity.JE = 'N'; }
                if (chkChickenpox.Checked) { clsImmunity.CHICKENPOX = 'Y'; } else { clsImmunity.CHICKENPOX = 'N'; }
                if (chkInfluenza.Checked) { clsImmunity.INFLUENZA = 'Y'; } else { clsImmunity.INFLUENZA = 'N'; }
                if (chkHepatitisA.Checked) { clsImmunity.HEPATITIS_A = 'Y'; } else { clsImmunity.HEPATITIS_A = 'N'; }
                if (chkHepatitisB.Checked) { clsImmunity.HEPATITIS_B = 'Y'; } else { clsImmunity.HEPATITIS_B = 'N'; }
                if (chkYellowFever.Checked) { clsImmunity.YELLOW_FEVER = 'Y'; } else { clsImmunity.YELLOW_FEVER = 'N'; }
                if (chkMeningococcal.Checked) { clsImmunity.MENING = 'Y'; } else { clsImmunity.MENING = 'N'; }
                if (chkTetanus.Checked) { clsImmunity.TETANUS = 'Y'; } else { clsImmunity.TETANUS = 'N'; }
                if (chkTyphoid.Checked) { clsImmunity.TYPHOID = 'Y'; } else { clsImmunity.TYPHOID = 'N'; }
                clsImmunity.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsImmunity.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_vaccination_or_immunity(clsImmunity);

                clsPersonalIllnessDoYouSmoke clsDoyouSmoke = new clsPersonalIllnessDoYouSmoke();
                clsDoyouSmoke.HN = Session["HN"].ToString().Trim();
                clsDoyouSmoke.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoDo_you_smoke_No.Checked) { clsDoyouSmoke.NO = 'Y'; } else { clsDoyouSmoke.NO = 'N'; }
                if (rdoDo_you_smoke_Yes.Checked) { clsDoyouSmoke.YES = 'Y'; } else { clsDoyouSmoke.YES = 'N'; }
                if (rdoDo_you_smoke_Yes_but.Checked) { clsDoyouSmoke.OTHERS = 'Y'; } else { clsDoyouSmoke.OTHERS = 'N'; }
                clsDoyouSmoke.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsDoyouSmoke.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_do_you_smoke(clsDoyouSmoke);

                clsPersonalIllnessDoYouSmoke clsSmoke_before_quitting = new clsPersonalIllnessDoYouSmoke();
                clsSmoke_before_quitting.HN = Session["HN"].ToString().Trim();
                clsSmoke_before_quitting.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoSmoke_before_quitting_0_5.Checked) { clsSmoke_before_quitting.NO = 'Y'; } else { clsSmoke_before_quitting.NO = 'N'; }
                if (rdoSmoke_before_quitting_6_10.Checked) { clsSmoke_before_quitting.YES = 'Y'; } else { clsSmoke_before_quitting.YES = 'N'; }
                if (rdoSmoke_before_quitting_over_10.Checked) { clsSmoke_before_quitting.OTHERS = 'Y'; } else { clsSmoke_before_quitting.OTHERS = 'N'; }
                clsSmoke_before_quitting.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsSmoke_before_quitting.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_smoke_before_quitting(clsSmoke_before_quitting);

                clsPersonalIllnessDoYouSmoke clsMany_cigarettes = new clsPersonalIllnessDoYouSmoke();
                clsMany_cigarettes.HN = Session["HN"].ToString().Trim();
                clsMany_cigarettes.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoMany_cigarettes_less_5.Checked) { clsMany_cigarettes.NO = 'Y'; } else { clsMany_cigarettes.NO = 'N'; }
                if (rdoMany_cigarettes_5_10.Checked) { clsMany_cigarettes.YES = 'Y'; } else { clsMany_cigarettes.YES = 'N'; }
                if (rdoMany_cigarettes_over_10.Checked) { clsMany_cigarettes.OTHERS = 'Y'; } else { clsMany_cigarettes.OTHERS = 'N'; }
                clsMany_cigarettes.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsMany_cigarettes.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_how_many_smoke_before_quitting(clsMany_cigarettes);

                clsPersonalIllnessDoYouSmoke clsHow_long_have_you_been_smoking = new clsPersonalIllnessDoYouSmoke();
                clsHow_long_have_you_been_smoking.HN = Session["HN"].ToString().Trim();
                clsHow_long_have_you_been_smoking.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHow_long_have_you_been_smoking_0_5.Checked)
                { clsHow_long_have_you_been_smoking.NO = 'Y'; }
                else { clsHow_long_have_you_been_smoking.NO = 'N'; }

                if (rdoHow_long_have_you_been_smoking_6_10.Checked) { clsHow_long_have_you_been_smoking.YES = 'Y'; }
                else { clsHow_long_have_you_been_smoking.YES = 'N'; }
                if (rdoHow_long_have_you_been_smoking_over_10.Checked) { clsHow_long_have_you_been_smoking.OTHERS = 'Y'; }
                else { clsHow_long_have_you_been_smoking.OTHERS = 'N'; }
                clsHow_long_have_you_been_smoking.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHow_long_have_you_been_smoking.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_how_long_have_you_been_smoking(clsHow_long_have_you_been_smoking);


                clsPersonalIllnessDoYouSmoke clsHow_many_cigarettes_do_you_smoke_in_a_day = new clsPersonalIllnessDoYouSmoke();
                clsHow_many_cigarettes_do_you_smoke_in_a_day.HN = Session["HN"].ToString().Trim();
                clsHow_many_cigarettes_do_you_smoke_in_a_day.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHow_many_cigarettes_do_you_smoke_in_a_day_less_5.Checked) { clsHow_many_cigarettes_do_you_smoke_in_a_day.NO = 'Y'; }
                else { clsHow_many_cigarettes_do_you_smoke_in_a_day.NO = 'N'; }
                if (rdoHow_many_cigarettes_do_you_smoke_in_a_day_5_10.Checked) { clsHow_many_cigarettes_do_you_smoke_in_a_day.YES = 'Y'; }
                else { clsHow_many_cigarettes_do_you_smoke_in_a_day.YES = 'N'; }
                if (rdoHow_many_cigarettes_do_you_smoke_in_a_day_over_10.Checked) { clsHow_many_cigarettes_do_you_smoke_in_a_day.OTHERS = 'Y'; }
                else { clsHow_many_cigarettes_do_you_smoke_in_a_day.OTHERS = 'N'; }
                clsHow_many_cigarettes_do_you_smoke_in_a_day.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHow_many_cigarettes_do_you_smoke_in_a_day.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_how_many_cigarettes_do_you_smoke_in_a_day(clsHow_many_cigarettes_do_you_smoke_in_a_day);


                clsPersonalIllnessDoYouSmoke clsHave_you_ever_thinking_about_quit_smoking = new clsPersonalIllnessDoYouSmoke();

                clsHave_you_ever_thinking_about_quit_smoking.HN = Session["HN"].ToString().Trim();
                clsHave_you_ever_thinking_about_quit_smoking.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHave_you_ever_thinking_about_quit_smoking_No.Checked) { clsHave_you_ever_thinking_about_quit_smoking.NO = 'Y'; }
                else { clsHave_you_ever_thinking_about_quit_smoking.NO = 'N'; }
                if (rdoHave_you_ever_thinking_about_quit_smoking_Yes.Checked) { clsHave_you_ever_thinking_about_quit_smoking.YES = 'Y'; }
                else { clsHave_you_ever_thinking_about_quit_smoking.YES = 'N'; }
                clsHave_you_ever_thinking_about_quit_smoking.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHave_you_ever_thinking_about_quit_smoking.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_have_you_ever_thinking_about_quit_smoking(clsHave_you_ever_thinking_about_quit_smoking);

                clsPersonalIllnessDoYouSmoke clsHave_you_ever_consumed_alcohol = new clsPersonalIllnessDoYouSmoke();
                clsHave_you_ever_consumed_alcohol.HN = Session["HN"].ToString().Trim();
                clsHave_you_ever_consumed_alcohol.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHave_you_ever_consumed_alcohol_No.Checked) { clsHave_you_ever_consumed_alcohol.NO = 'Y'; }
                else { clsHave_you_ever_consumed_alcohol.NO = 'N'; }
                if (rdoHave_you_ever_consumed_alcohol_Yes.Checked) { clsHave_you_ever_consumed_alcohol.YES = 'Y'; }
                else { clsHave_you_ever_consumed_alcohol.YES = 'N'; }
                if (rdoHave_you_ever_consumed_alcohol_Yes_But.Checked) { clsHave_you_ever_consumed_alcohol.OTHERS = 'Y'; }
                else { clsHave_you_ever_consumed_alcohol.OTHERS = 'N'; }
                clsHave_you_ever_consumed_alcohol.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHave_you_ever_consumed_alcohol.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_have_you_ever_consumed_alcohol(clsHave_you_ever_consumed_alcohol);

                clsPersonalIllnessDoYouSmoke clsHow_long_did_you_drink_alcohol_before_stop_drinking = new clsPersonalIllnessDoYouSmoke();
                clsHow_long_did_you_drink_alcohol_before_stop_drinking.HN = Session["HN"].ToString().Trim();
                clsHow_long_did_you_drink_alcohol_before_stop_drinking.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHow_long_did_you_drink_alcohol_before_stop_drinking_0_5.Checked) { clsHow_long_did_you_drink_alcohol_before_stop_drinking.NO = 'Y'; }
                else { clsHow_long_did_you_drink_alcohol_before_stop_drinking.NO = 'N'; }
                if (rdoHow_long_did_you_drink_alcohol_before_stop_drinking_6_10.Checked) { clsHow_long_did_you_drink_alcohol_before_stop_drinking.YES = 'Y'; }
                else { clsHow_long_did_you_drink_alcohol_before_stop_drinking.YES = 'N'; }
                if (rdoHow_long_did_you_drink_alcohol_before_stop_drinking_over_10.Checked) { clsHow_long_did_you_drink_alcohol_before_stop_drinking.OTHERS = 'Y'; }
                else { clsHow_long_did_you_drink_alcohol_before_stop_drinking.OTHERS = 'N'; }
                clsHow_long_did_you_drink_alcohol_before_stop_drinking.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHow_long_did_you_drink_alcohol_before_stop_drinking.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_how_long_did_you_drink_alcohol_before_stop_drinking(clsHow_long_did_you_drink_alcohol_before_stop_drinking);

                clsPersonalIllnessDoYouSmoke clsHow_often_did_you_drink_before_you_stopped = new clsPersonalIllnessDoYouSmoke();
                clsHow_often_did_you_drink_before_you_stopped.HN = Session["HN"].ToString().Trim();
                clsHow_often_did_you_drink_before_you_stopped.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHow_often_did_you_drink_before_you_stopped_1.Checked) { clsHow_often_did_you_drink_before_you_stopped.NO = 'Y'; }
                else { clsHow_often_did_you_drink_before_you_stopped.NO = 'N'; }
                if (rdoHow_often_did_you_drink_before_you_stopped_2.Checked) { clsHow_often_did_you_drink_before_you_stopped.YES = 'Y'; }
                else { clsHow_often_did_you_drink_before_you_stopped.YES = 'N'; }
                if (rdoHow_often_did_you_drink_before_you_stopped_3.Checked) { clsHow_often_did_you_drink_before_you_stopped.OTHERS = 'Y'; }
                else { clsHow_often_did_you_drink_before_you_stopped.OTHERS = 'N'; }
                clsHow_often_did_you_drink_before_you_stopped.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHow_often_did_you_drink_before_you_stopped.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_how_often_did_you_drink_before_you_stopped(clsHow_often_did_you_drink_before_you_stopped);

                clsPersonalIllnessDoYouSmoke clsHow_often_do_you_consume_alcohol = new clsPersonalIllnessDoYouSmoke();
                clsHow_often_do_you_consume_alcohol.HN = Session["HN"].ToString().Trim();
                clsHow_often_do_you_consume_alcohol.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHow_often_do_you_consume_alcohol_Less_than_1_time.Checked) { clsHow_often_do_you_consume_alcohol.NO = 'Y'; }
                else { clsHow_often_do_you_consume_alcohol.NO = 'N'; }
                if (rdoHow_often_do_you_consume_alcohol_1_time_week.Checked) { clsHow_often_do_you_consume_alcohol.YES = 'Y'; }
                else { clsHow_often_do_you_consume_alcohol.YES = 'N'; }
                if (rdoHow_often_do_you_consume_alcohol_2_3_week.Checked) { clsHow_often_do_you_consume_alcohol.OTHERS = 'Y'; }
                else { clsHow_often_do_you_consume_alcohol.OTHERS = 'N'; }
                if (rdoHow_often_do_you_consume_alcohol_more_than_3_week.Checked) { clsHow_often_do_you_consume_alcohol.JE = 'Y'; }
                else { clsHow_often_do_you_consume_alcohol.JE = 'N'; }
                clsHow_often_do_you_consume_alcohol.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHow_often_do_you_consume_alcohol.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_how_often_do_you_consume_alcohol(clsHow_often_do_you_consume_alcohol);


                clsPersonalIllnessDoYouSmoke clsHave_you_ever_think_about_stop_drinking = new clsPersonalIllnessDoYouSmoke();
                clsHave_you_ever_think_about_stop_drinking.HN = Session["HN"].ToString().Trim();
                clsHave_you_ever_think_about_stop_drinking.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHave_you_ever_think_about_stop_drinking_No.Checked) { clsHave_you_ever_think_about_stop_drinking.NO = 'Y'; }
                else { clsHave_you_ever_think_about_stop_drinking.NO = 'N'; }
                if (rdoHave_you_ever_think_about_stop_drinking_Yes.Checked) { clsHave_you_ever_think_about_stop_drinking.YES = 'Y'; }
                else { clsHave_you_ever_think_about_stop_drinking.YES = 'N'; }
                clsHave_you_ever_think_about_stop_drinking.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHave_you_ever_think_about_stop_drinking.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_have_you_ever_think_about_stop_drinking(clsHave_you_ever_think_about_stop_drinking);

                clsPersonalIllnessDoYouSmoke clsHave_you_use_or_tried_any_drugs = new clsPersonalIllnessDoYouSmoke();
                clsHave_you_use_or_tried_any_drugs.HN = Session["HN"].ToString().Trim();
                clsHave_you_use_or_tried_any_drugs.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoHave_you_use_or_tried_any_drugs_No.Checked) { clsHave_you_use_or_tried_any_drugs.NO = 'Y'; }
                else { clsHave_you_use_or_tried_any_drugs.NO = 'N'; }
                if (rdoHave_you_use_or_tried_any_drugs_Yes.Checked) { clsHave_you_use_or_tried_any_drugs.YES = 'Y'; }
                else { clsHave_you_use_or_tried_any_drugs.YES = 'N'; }
                if (rdoHave_you_use_or_tried_any_drugs_Yes_But.Checked) { clsHave_you_use_or_tried_any_drugs.OTHERS = 'Y'; }
                else { clsHave_you_use_or_tried_any_drugs.OTHERS = 'N'; }
                clsHave_you_use_or_tried_any_drugs.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsHave_you_use_or_tried_any_drugs.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_have_you_use_or_tried_any_drugs(clsHave_you_use_or_tried_any_drugs);

                clsPersonalIllnessDoYouSmoke clsWhat_type_of_drugs_did_you_used = new clsPersonalIllnessDoYouSmoke();
                clsWhat_type_of_drugs_did_you_used.HN = Session["HN"].ToString().Trim();
                clsWhat_type_of_drugs_did_you_used.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoWhat_type_of_drugs_did_you_used_Mari.Checked) { clsWhat_type_of_drugs_did_you_used.NO = 'Y'; }
                else { clsWhat_type_of_drugs_did_you_used.NO = 'N'; }
                if (rdoWhat_type_of_drugs_did_you_used_Amp.Checked) { clsWhat_type_of_drugs_did_you_used.YES = 'Y'; }
                else { clsWhat_type_of_drugs_did_you_used.YES = 'N'; }
                if (rdoWhat_type_of_drugs_did_you_used_Volatile.Checked) { clsWhat_type_of_drugs_did_you_used.JE = 'Y'; }
                else { clsWhat_type_of_drugs_did_you_used.JE = 'N'; }
                if (rdoWhat_type_of_drugs_did_you_used_Others.Checked) { clsWhat_type_of_drugs_did_you_used.OTHERS = 'Y'; }
                else { clsWhat_type_of_drugs_did_you_used.OTHERS = 'N'; }
                clsWhat_type_of_drugs_did_you_used.OTHERS_DETAILS = txtWhat_type_of_drugs_did_you_used_other.Text.Trim();
                clsWhat_type_of_drugs_did_you_used.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsWhat_type_of_drugs_did_you_used.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_iiiness_what_type_of_drugs_did_you_used(clsWhat_type_of_drugs_did_you_used);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void save_family_Illness()
        {
            try
            {
                clsFamilyIllness clsFamily = new clsFamilyIllness();
                clsFamily.HN = Session["HN"].ToString().Trim();
                clsFamily.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                clsFamily.FAMILY_ID = '1';//Father
                if (chkFather_None.Checked) { clsFamily.NONE = 'Y'; } else { clsFamily.NONE = 'N'; }
                if (chkFather_Anemia.Checked) { clsFamily.ANEMIA_FAMILY = 'Y'; } else { clsFamily.ANEMIA_FAMILY = 'N'; }
                if (chkFather_Cancer.Checked) { clsFamily.CANCER_FAMILY = 'Y'; } else { clsFamily.CANCER_FAMILY = 'N'; }
                if (chkFather_Diabetes.Checked) { clsFamily.DIABETES_FAMILY = 'Y'; } else { clsFamily.DIABETES_FAMILY = 'N'; }
                if (chkFather_Asthma.Checked) { clsFamily.ASTHMA_FAMILY = 'Y'; } else { clsFamily.ASTHMA_FAMILY = 'N'; }
                if (chkfatherHigh_blood_pressure.Checked) { clsFamily.HIGH_BLOOD_PRESSURE_FAMILY = 'Y'; } else { clsFamily.HIGH_BLOOD_PRESSURE_FAMILY = 'N'; }
                if (chkFather_Allergy.Checked) { clsFamily.ALLERGY_FAMILY = 'Y'; } else { clsFamily.ALLERGY_FAMILY = 'N'; }
                if (chkFatherCardiovascular.Checked) { clsFamily.CARDIOVASCULAR_FAMILY = 'Y'; } else { clsFamily.CARDIOVASCULAR_FAMILY = 'N'; }
                if (chkFather_Tuberculosis.Checked) { clsFamily.TUBERCULOSIS_FAMILY = 'Y'; } else { clsFamily.TUBERCULOSIS_FAMILY = 'N'; }
                if (chkFather_Others.Checked) { clsFamily.OTHERS = 'Y'; } else { clsFamily.OTHERS = 'N'; }
                clsFamily.OTHERS_DETAILS = txtFather_other_details.Text.Trim();
                clsFamily.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsFamily.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_personal_family_iiiness(clsFamily);


                clsFamily.FAMILY_ID = '2';//Mother
                if (chkMother_None.Checked) { clsFamily.NONE = 'Y'; } else { clsFamily.NONE = 'N'; }
                if (chkMother_Anemia.Checked) { clsFamily.ANEMIA_FAMILY = 'Y'; } else { clsFamily.ANEMIA_FAMILY = 'N'; }
                if (chkMother_Cancer.Checked) { clsFamily.CANCER_FAMILY = 'Y'; } else { clsFamily.CANCER_FAMILY = 'N'; }
                if (chkMother_Diabetes.Checked) { clsFamily.DIABETES_FAMILY = 'Y'; } else { clsFamily.DIABETES_FAMILY = 'N'; }
                if (chkMother_Asthma.Checked) { clsFamily.ASTHMA_FAMILY = 'Y'; } else { clsFamily.ASTHMA_FAMILY = 'N'; }
                if (chkMotherHigh_blood_pressure.Checked) { clsFamily.HIGH_BLOOD_PRESSURE_FAMILY = 'Y'; } else { clsFamily.HIGH_BLOOD_PRESSURE_FAMILY = 'N'; }
                if (chkMother_Allergy.Checked) { clsFamily.ALLERGY_FAMILY = 'Y'; } else { clsFamily.ALLERGY_FAMILY = 'N'; }
                if (chkMotherCardiovascular.Checked) { clsFamily.CARDIOVASCULAR_FAMILY = 'Y'; } else { clsFamily.CARDIOVASCULAR_FAMILY = 'N'; }
                if (chkMother_Tuberculosis.Checked) { clsFamily.TUBERCULOSIS_FAMILY = 'Y'; } else { clsFamily.TUBERCULOSIS_FAMILY = 'N'; }
                if (chkMother_Others.Checked) { clsFamily.OTHERS = 'Y'; } else { clsFamily.OTHERS = 'N'; }
                clsFamily.OTHERS_DETAILS = txtMother_other_details.Text.Trim();
                clsEX.insert_trn_personal_family_iiiness(clsFamily);


                clsFamily.FAMILY_ID = '3';//Siblings
                if (chkSiblings_None.Checked) { clsFamily.NONE = 'Y'; } else { clsFamily.NONE = 'N'; }
                if (chkSiblings_Anemia.Checked) { clsFamily.ANEMIA_FAMILY = 'Y'; } else { clsFamily.ANEMIA_FAMILY = 'N'; }
                if (chkSiblings_Cancer.Checked) { clsFamily.CANCER_FAMILY = 'Y'; } else { clsFamily.CANCER_FAMILY = 'N'; }
                if (chkSiblings_Diabetes.Checked) { clsFamily.DIABETES_FAMILY = 'Y'; } else { clsFamily.DIABETES_FAMILY = 'N'; }
                if (chkSiblings_Asthma.Checked) { clsFamily.ASTHMA_FAMILY = 'Y'; } else { clsFamily.ASTHMA_FAMILY = 'N'; }
                if (chkSiblingsHigh_blood_pressure.Checked) { clsFamily.HIGH_BLOOD_PRESSURE_FAMILY = 'Y'; } else { clsFamily.HIGH_BLOOD_PRESSURE_FAMILY = 'N'; }
                if (chkSiblings_Allergy.Checked) { clsFamily.ALLERGY_FAMILY = 'Y'; } else { clsFamily.ALLERGY_FAMILY = 'N'; }
                if (chkSiblingsCardiovascular.Checked) { clsFamily.CARDIOVASCULAR_FAMILY = 'Y'; } else { clsFamily.CARDIOVASCULAR_FAMILY = 'N'; }
                if (chkSiblings_Tuberculosis.Checked) { clsFamily.TUBERCULOSIS_FAMILY = 'Y'; } else { clsFamily.TUBERCULOSIS_FAMILY = 'N'; }
                if (chkSiblings_Others.Checked) { clsFamily.OTHERS = 'Y'; } else { clsFamily.OTHERS = 'N'; }
                clsFamily.OTHERS_DETAILS = txtSiblings_other_details.Text.Trim();
                clsEX.insert_trn_personal_family_iiiness(clsFamily);


            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        protected void save_health_other_issue()
        {
            try
            {
                clsOtherHealthIssuesfavoritefood clsFavoriteFood = new clsOtherHealthIssuesfavoritefood();
                clsFavoriteFood.HN = Session["HN"].ToString().Trim();
                clsFavoriteFood.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (chkWhat_is_your_favorite_food_Rice.Checked) { clsFavoriteFood.RICE = 'Y'; }
                else { clsFavoriteFood.RICE = 'N'; }
                if (chkWhat_is_your_favorite_food_Vegetable.Checked) { clsFavoriteFood.VEGETABLE = 'Y'; }
                else { clsFavoriteFood.VEGETABLE = 'N'; }
                if (chkWhat_is_your_favorite_food_Deep.Checked) { clsFavoriteFood.DEEP_FRIED_FOOD = 'Y'; }
                else { clsFavoriteFood.DEEP_FRIED_FOOD = 'N'; }
                if (chkWhat_is_your_favorite_food_Snack.Checked) { clsFavoriteFood.SNACK = 'Y'; }
                else { clsFavoriteFood.SNACK = 'N'; }
                if (chkWhat_is_your_favorite_food_Fast_Food.Checked) { clsFavoriteFood.FAST_FOOD = 'Y'; }
                else { clsFavoriteFood.FAST_FOOD = 'N'; }
                if (chkWhat_is_your_favorite_food_Fish_Lean.Checked) { clsFavoriteFood.FISH = 'Y'; }
                else { clsFavoriteFood.FISH = 'N'; }
                if (chkWhat_is_your_favorite_food_Instant.Checked) { clsFavoriteFood.INSTANT_NOODLE = 'Y'; }
                else { clsFavoriteFood.INSTANT_NOODLE = 'N'; }
                if (chkWhat_is_your_favorite_food_Others.Checked) { clsFavoriteFood.OTHERS = 'Y'; }
                else { clsFavoriteFood.OTHERS = 'N'; }
                clsFavoriteFood.OTHERS_DETAILS = txtWhat_is_your_favorite_food_Others_details.Text.Trim();
                clsFavoriteFood.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsFavoriteFood.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_other_health_issues_favorite_food(clsFavoriteFood);

                clsOtherHealthIssuesExerciseSport clsSport = new clsOtherHealthIssuesExerciseSport();
                clsSport.HN = Session["HN"].ToString().Trim();
                clsSport.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoDo_you_exercise_play_sports_No.Checked) { clsSport.NO = 'Y'; }
                else { clsSport.NO = 'N'; }
                if (rdoDo_you_exercise_play_sports_Less_than_3_times.Checked) { clsSport.YES = 'Y'; }
                else { clsSport.YES = 'N'; }
                if (rdo_you_exercise_play_sports_More_than_3_times.Checked) { clsSport.OTHERS = 'Y'; }
                else { clsSport.OTHERS = 'N'; }
                clsSport.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsSport.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_other_health_issues_do_you_exercise(clsSport);

                clsOtherHealthIssuesExerciseSport clsSportDuration = new clsOtherHealthIssuesExerciseSport();
                clsSportDuration.HN = Session["HN"].ToString().Trim();
                clsSportDuration.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoWhat_is_your_exercise_duration_Less_than_30.Checked) { clsSportDuration.NO = 'Y'; }
                else { clsSportDuration.NO = 'N'; }
                if (rdoWhat_is_your_exercise_duration_Over_than_30.Checked) { clsSportDuration.YES = 'Y'; }
                else { clsSportDuration.YES = 'N'; }
                clsSportDuration.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsSportDuration.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_other_health_issues_do_you_exercise_duration(clsSportDuration);


                clsOtherHealthIssuesExerciseSport clsDeclarePersonal = new clsOtherHealthIssuesExerciseSport();
                clsDeclarePersonal.HN = Session["HN"].ToString().Trim();
                clsDeclarePersonal.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoDo_you_want_to_declare_personal_history_Yes.Checked) { clsDeclarePersonal.YES = 'Y'; }
                else { clsDeclarePersonal.YES = 'N'; }
                if (rdoDo_you_want_to_declare_personal_history_No.Checked) { clsDeclarePersonal.NO = 'Y'; }
                else { clsDeclarePersonal.NO = 'N'; }
                clsDeclarePersonal.DDMMYY = ut.ConvertDateToStringFormat(txtDDMMYY_1.Text.Trim(), "yyyy-MM-dd");
                clsDeclarePersonal.INJURY_OR_ILLNESS = txtInjury_1.Text.Trim();
                clsDeclarePersonal.CAUSE_OF_INJURY = txtCause_of_injury_1.Text.Trim();
                clsDeclarePersonal.DISABLED = txtDisabled_1.Text.Trim();
                clsDeclarePersonal.LOSS_OF_LIMBS = txtLimbs_1.Text.Trim();
                clsDeclarePersonal.LESS_THAN_THREE = txtLessThan_1.Text.Trim();
                clsDeclarePersonal.MORE_THAN_THREE = txtMoreThan_1.Text.Trim();
                clsDeclarePersonal.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsDeclarePersonal.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_other_health_issues_do_you_want_to_declare_personal(clsDeclarePersonal);

                clsDeclarePersonal.DDMMYY = ut.ConvertDateToStringFormat(txtDDMMYY_2.Text.Trim(), "yyyy-MM-dd");
                clsDeclarePersonal.INJURY_OR_ILLNESS = txtInjury_2.Text.Trim();
                clsDeclarePersonal.CAUSE_OF_INJURY = txtCause_of_injury_2.Text.Trim();
                clsDeclarePersonal.DISABLED = txtDisabled_2.Text.Trim();
                clsDeclarePersonal.LOSS_OF_LIMBS = txtLimbs_2.Text.Trim();
                clsDeclarePersonal.LESS_THAN_THREE = txtLessThan_2.Text.Trim();
                clsDeclarePersonal.MORE_THAN_THREE = txtMoreThan_2.Text.Trim();
                clsDeclarePersonal.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsDeclarePersonal.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_other_health_issues_do_you_want_to_declare_personal(clsDeclarePersonal);




                clsOtherHealthMenstrualPeriodsAtPresent clsMen = new clsOtherHealthMenstrualPeriodsAtPresent();
                clsMen.HN = Session["HN"].ToString().Trim();
                clsMen.KEY_GEN = Session["KEY_GEN"].ToString().Trim();
                if (rdoMenstrual_periods_yes.Checked) { clsMen.YES = 'Y'; } else { clsMen.YES = 'N'; }
                if (rdoMenstrual.Checked) { clsMen.MENOPAUSE = 'Y'; } else { clsMen.MENOPAUSE = 'N'; }
                clsMen.DATE_FORM = ut.ConvertDateToStringFormat(txtMenoDateForm.Text.Trim(), "yyyy-MM-dd");
                clsMen.DATE_TO = ut.ConvertDateToStringFormat(txtMenoDateTo.Text.Trim(), "yyyy-MM-dd");
                if (rdoCharacteristicNormal.Checked) { clsMen.NORMAL = 'Y'; } else { clsMen.NORMAL = 'N'; }
                if (rdoCharacteristicABNormal.Checked) { clsMen.ABNORMAL = 'Y'; } else { clsMen.ABNORMAL = 'N'; }
                if (rdoPreNo.Checked) { clsMen.PRE_NO = 'Y'; } else { clsMen.PRE_NO = 'N'; }
                if (rdoPregnacy.Checked) { clsMen.PRE_PREGNANCY = 'Y'; } else { clsMen.PRE_PREGNANCY = 'N'; }
                if (rdoPregSuspect.Checked) { clsMen.PRE_SUSPECTED = 'Y'; } else { clsMen.PRE_SUSPECTED = 'N'; }
                if (rdoDelivered_Yes.Checked) { clsMen.HAS_YES = 'Y'; } else { clsMen.HAS_YES = 'N'; }
                if (rdodelivered_No.Checked) { clsMen.HAS_NO = 'Y'; } else { clsMen.HAS_NO = 'N'; }
                if (rdodelivered_not_sure.Checked) { clsMen.HAS_NOT_SURE = 'Y'; } else { clsMen.HAS_NOT_SURE = 'N'; }

                clsMen.CREATE_BY = Session["CREATE_BY"].ToString().Trim();
                clsMen.UPDATE_BY = Session["UPDATE_BY"].ToString().Trim();
                clsEX.insert_trn_other_health_issues_do_you_have_menstrual_periods_at_present(clsMen);
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

    }
}
