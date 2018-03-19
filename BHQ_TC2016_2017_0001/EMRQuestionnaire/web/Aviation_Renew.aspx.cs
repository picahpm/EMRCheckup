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
    public partial class Aviation_Renew : System.Web.UI.Page
    {
        executeDC clsEX = new executeDC();
        DataTable dtMasLabel = new DataTable();
        Utility ut = new Utility();
        DataTable dtHistory = new DataTable();

        private InhCheckupDataContext cdc;        
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
            Label1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'name'"));
            lblRoom.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'room'"));
            lblHN.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'hn'"));
            lblPhysician.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'phy'"));
            lblVisitDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'visit_date'"));
            lblDepartment.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'department'"));
            lblBirthDate.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'birth_date'"));
            Label2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'age'"));
            lblSex.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'sex'"));
            lblAllergies.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'allergies'"));
            #endregion

            #region RecommendationHeader
            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_AVIATIONS_RECOMMEND");
            lblRecommendation.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'lblRecommendation'"));
            lblRecommendationRule.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'lblRecommendationRenew'"));
            #endregion

            dtMasLabel = clsEX.get_mas_label("", Session["LEGION"].ToString(), "TBL_MAS_AVIATION_RENEW");
            #region Recommendation
            lblPlace.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL1'"));
            lblName.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL2NAME'"));
            lblNationality.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL3NATION'"));
            lblGender.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4SEX'"));
            rdGender_M.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4MALE'"));
            rdGender_F.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL4FEMALE'"));
            lblDOB.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL5DOB'"));
            lblAge.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL6AGE'"));
            lblAgeYear.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL6YEAR'"));
            lblAgeMonth.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL6MONTH'"));
            lblAddress.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL7ADDR'"));
            rdAddress_S.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL7ADDRSAME'"));
            rdAddress_C.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL7ADDRCHANGE'"));
            lblTel.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL8TEL'"));
            lblLicenseType.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9TOL'"));
            rdATPL.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9ATPL'"));
            rdPPL.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9PPL'"));
            rdATC.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9ATC'"));
            rdCPL.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9CPL'"));
            rdPE.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9PE'"));
            rdStudent.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9ST'"));
            rdOther.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL9OTHER'"));
            lblLicenseNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL10LICENSE'"));
            lblFlyTime.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL11TFT'"));
            lblLast6Month.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL11LASTSIXMONTHS'"));
            lblForceInputNum1.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELFORCENUM'"));
            lblForceInputNum2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELFORCENUM'"));
            lblFlyTimeHour.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL11HOURS'"));
            lblFlyTimeHou2.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL11HOURS'"));
            lblACType.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL11ACTYPE'"));
            lblLastMedicalExam.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12LASTMEDICAL'"));
            rdLastMedicalExam_F.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12FIT'"));
            rdLastMedicalExam_U.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12UNFIT'"));
            rdLastMedicalExam_L.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12LIMITATION'"));
            lblResult.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL12RESULT'"));
            lblCurrentlyMedical.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13MEDICATION'"));
            lblResultMedical.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13RESULT'"));
            rdMedicalNo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13NO'"));
            rdMedicalYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13YES'"));
            lblMedicalIfYes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13IFYES'"));
            lblQuantity.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13QUNTITY'"));
            lblReason.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL13REASON'"));
            #endregion
            #region MedicalHistory
            lblMedicalHistory.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELMEDHISTORY'"));
            lblMedicalHistoryInfo.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABELMEDHISTORYDETAILS'"));
            lblMainQuestion14.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL14MAINQUESTION'"));
            rdMainQuestion14_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion14_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion15.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL15MAINQUESTION'"));
            rdMainQuestion15_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion15_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion16.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL16MAINQUESTION'"));
            rdMainQuestion16_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion16_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion17.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL17MAINQUESTION'"));
            rdMainQuestion17_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion17_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion18.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL18MAINQUESTION'"));
            rdMainQuestion18_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion18_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion19.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL19MAINQUESTION'"));
            rdMainQuestion19_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion19_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion20.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL20MAINQUESTION'"));
            rdMainQuestion20_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion20_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion21.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL21MAINQUESTION'"));
            rdMainQuestion21_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion21_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion22.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL22MAINQUESTION'"));
            rdMainQuestion22_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion22_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion23.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL23MAINQUESTION'"));
            rdMainQuestion23_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion23_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion24.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL24MAINQUESTION'"));
            rdMainQuestion24_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion24_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion25.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL25MAINQUESTION'"));
            rdMainQuestion25_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion25_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion26.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL26MAINQUESTION'"));
            rdMainQuestion26_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion26_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion27.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL27MAINQUESTION'"));
            rdMainQuestion27_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion27_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion28.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL28MAINQUESTION'"));
            rdMainQuestion28_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion28_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion29.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL29MAINQUESTION'"));
            rdMainQuestion29_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion29_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion30.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL30MAINQUESTION'"));
            rdMainQuestion30_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion30_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion31.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL31MAINQUESTION'"));
            rdMainQuestion31_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion31_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion32.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL32MAINQUESTION'"));
            rdMainQuestion32_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion32_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion33.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL33MAINQUESTION'"));
            rdMainQuestion33_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion33_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion34.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL34MAINQUESTION'"));
            rdMainQuestion34_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion34_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
            lblMainQuestion35.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35MAINQUESTION'"));
            rdMainQuestion35_no.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35NO'"));
            rdMainQuestion35_yes.Text = ut.getLabelDt(dtMasLabel.Select("LABEL_ID = 'LABEL35YES'"));
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

                        bd = CalculateAge(Convert.ToDateTime(tpt.trn_patient.tpt_dob), DateTime.Now);
                        queAvia.tqa_age_yrs = bd[0];
                        queAvia.tqa_age_month = bd[1];
                    }
                    else
                    {
                        Response.Write("<script type='text/javascript' language='javascript'>alert(" + "ไม่พบข้อมูล HN: " + hn_no + " ใน EMR" + ");</script>");
                    }

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
                //Address
                switch (queAvia.tqa_chge_address.ToString())
                {
                    case "S": rdAddress_S.Checked = true; break;
                    case "C": rdAddress_C.Checked = true; break;
                    default:
                        rdAddress_S.Checked = false;
                        rdAddress_C.Checked = false;
                        break;
                }
                txtAddressChanged.Text = queAvia.tqa_th_address;
                txtTel.Text = queAvia.tqa_th_moblie;
                //queAviation Type
                switch (queAvia.tqa_avia_type.ToString())
                {
                    case "A": rdATPL.Checked = true; break;
                    case "P": rdPPL.Checked = true; break;
                    case "T": rdATC.Checked = true; break;
                    case "C": rdCPL.Checked = true; break;
                    case "E": rdPE.Checked = true; break;
                    case "S": rdStudent.Checked = true; break;
                    case "O": rdOther.Checked = true; break;
                    default:
                        {
                            rdATPL.Checked = false;
                            rdPPL.Checked = false;
                            rdATC.Checked = false;
                            rdCPL.Checked = false;
                            rdPE.Checked = false;
                            rdStudent.Checked = false;
                            rdOther.Checked = false;
                            break;
                        }
                }
                txtOtherLicense.Text = queAvia.tqa_avia_oths;
                txtLicenseNo.Text = queAvia.tqa_license_no;
                //Fly time                    
                txtFlyTime.Text = queAvia.tqa_tot_fling_time.ToString();
                txtLast6Month.Text = queAvia.tqa_last_six_time.ToString();
                txtACType.Text = queAvia.tqa_aircraft_name;
                //Last medical
                txtLastMedicalExam.Text = queAvia.tqa_prev_exam_loc;
                switch (queAvia.tqa_prev_exam_deca.ToString())
                {
                    case "F": rdLastMedicalExam_F.Checked = true; break;
                    case "U": rdLastMedicalExam_U.Checked = true; break;
                    case "L": rdLastMedicalExam_L.Checked = true; break;
                    default:
                        rdLastMedicalExam_F.Checked = false;
                        rdLastMedicalExam_U.Checked = false;
                        rdLastMedicalExam_L.Checked = false;
                        break;
                }
                //Medication
                switch (queAvia.tqa_use_medicine.ToString())
                {
                    case "N": rdMedicalNo.Checked = true; break;
                    case "Y": rdMedicalYes.Checked = true; break;
                    default:
                        {
                            rdMedicalNo.Checked = false;
                            rdMedicalYes.Checked = false;
                            break;
                        }
                }
                txtMedicalIfYes.Text = queAvia.tqa_med_name;
                txtMedicalQuantity.Text = queAvia.tqa_med_amount;
                txtMedicalReason.Text = queAvia.tqa_med_reason;
                #endregion
                #region MedicalHistory
                //MainQuestion14
                switch (queAvia.tqa_chis_freq.ToString())
                {
                    case "N": rdMainQuestion14_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion14_yes.Checked = true;
                            txtQuestion14Yes.Text = queAvia.tqa_chis_freq_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion14.Text + " ; " + txtQuestion14Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion14_no.Checked = false;
                            rdMainQuestion14_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion15
                switch (queAvia.tqa_chis_dizz.ToString())
                {
                    case "N": rdMainQuestion15_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion15_yes.Checked = true;
                            txtQuestion15Yes.Text = queAvia.tqa_chis_dizz_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion15.Text + " ; " + txtQuestion15Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion15_no.Checked = false;
                            rdMainQuestion15_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion16
                switch (queAvia.tqa_chis_unco.ToString())
                {
                    case "N": rdMainQuestion16_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion16_yes.Checked = true;
                            txtQuestion16Yes.Text = queAvia.tqa_chis_unco_rmk;
                            tmpRemark += "\r\n" + lblMainQuestion16.Text + " ; " + txtQuestion16Yes.Text;
                            break;
                        }
                    default:
                        {
                            rdMainQuestion16_no.Checked = false;
                            rdMainQuestion16_yes.Checked = false;
                            break;
                        }
                }
                //MainQuestion17
                switch (queAvia.tqa_chis_eyet.ToString())
                {
                    case "N": rdMainQuestion17_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion17_yes.Checked = true;
                            txtQuestion17Yes.Text = queAvia.tqa_chis_eyet_rmk;
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
                switch (queAvia.tqa_chis_hayf.ToString())
                {
                    case "N": rdMainQuestion18_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion18_yes.Checked = true;
                            txtQuestion18Yes.Text = queAvia.tqa_chis_hayf_rmk;
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
                switch (queAvia.tqa_chis_lung.ToString())
                {
                    case "N": rdMainQuestion19_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion19_yes.Checked = true;
                            txtQuestion19Yes.Text = queAvia.tqa_chis_lung_rmk;
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
                switch (queAvia.tqa_chis_hert.ToString())
                {
                    case "N": rdMainQuestion20_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion20_yes.Checked = true;
                            txtQuestion20Yes.Text = queAvia.tqa_chis_hert_rmk;
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
                switch (queAvia.tqa_chis_high.ToString())
                {
                    case "N": rdMainQuestion21_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion21_yes.Checked = true;
                            txtQuestion21Yes.Text = queAvia.tqa_chis_high_rmk;
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
                switch (queAvia.tqa_chis_stom.ToString())
                {
                    case "N": rdMainQuestion22_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion22_yes.Checked = true;
                            txtQuestion22Yes.Text = queAvia.tqa_chis_stom_rmk;
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
                switch (queAvia.tqa_chis_kidn.ToString())
                {
                    case "N": rdMainQuestion23_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion23_yes.Checked = true;
                            txtQuestion23Yes.Text = queAvia.tqa_chis_kidn_rmk;
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
                switch (queAvia.tqa_chis_gyna.ToString())
                {
                    case "N": rdMainQuestion24_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion24_yes.Checked = true;
                            txtQuestion24Yes.Text = queAvia.tqa_chis_gyna_rmk;
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
                switch (queAvia.tqa_chis_nurv.ToString())
                {
                    case "N": rdMainQuestion25_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion25_yes.Checked = true;
                            txtQuestion25Yes.Text = queAvia.tqa_chis_nurv_rmk;
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
                switch (queAvia.tqa_chis_moti.ToString())
                {
                    case "N": rdMainQuestion26_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion26_yes.Checked = true;
                            txtQuestion26Yes.Text = queAvia.tqa_chis_moti_rmk;
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
                switch (queAvia.tqa_chis_ment.ToString())
                {
                    case "N": rdMainQuestion27_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion27_yes.Checked = true;
                            txtQuestion27Yes.Text = queAvia.tqa_chis_ment_rmk;
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
                switch (queAvia.tqa_chis_suic.ToString())
                {
                    case "N": rdMainQuestion28_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion28_yes.Checked = true;
                            txtQuestion28Yes.Text = queAvia.tqa_chis_suic_rmk;
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
                switch (queAvia.tqa_chis_alco.ToString())
                {
                    case "N": rdMainQuestion29_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion29_yes.Checked = true;
                            txtQuestion29Yes.Text = queAvia.tqa_chis_alco_rmk;
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
                switch (queAvia.tqa_chis_drug.ToString())
                {
                    case "N": rdMainQuestion30_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion30_yes.Checked = true;
                            txtQuestion30Yes.Text = queAvia.tqa_chis_drug_rmk;
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
                switch (queAvia.tqa_chis_adms.ToString())
                {
                    case "N": rdMainQuestion31_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion31_yes.Checked = true;
                            txtQuestion31Yes.Text = queAvia.tqa_chis_adms_rmk;
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
                switch (queAvia.tqa_chis_avia.ToString())
                {
                    case "N": rdMainQuestion32_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion32_yes.Checked = true;
                            txtQuestion32Yes.Text = queAvia.tqa_chis_avia_rmk;
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
                switch (queAvia.tqa_chis_otha.ToString())
                {
                    case "N": rdMainQuestion33_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion33_yes.Checked = true;
                            txtQuestion33Yes.Text = queAvia.tqa_chis_otha_rmk;
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
                switch (queAvia.tqa_chis_conviction.ToString())
                {
                    case "N": rdMainQuestion34_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion34_yes.Checked = true;
                            txtQuestion34Yes.Text = queAvia.tqa_chis_conv_rmk;
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
                switch (queAvia.tqa_chis_othi.ToString())
                {
                    case "N": rdMainQuestion35_no.Checked = true; break;
                    case "Y":
                        {
                            rdMainQuestion35_yes.Checked = true;
                            txtQuestion35Yes.Text = queAvia.tqa_chis_othi_rmk;
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
                #endregion
                Session["remark"] = tmpRemark;
                txtRemark.Text = queAvia.tqa_remark + Session["remark"].ToString();

                if (rdGender_M.Checked)
                {
                    lblMainQuestion24.Enabled = false;
                    rdMainQuestion24_no.Enabled = false;
                    rdMainQuestion24_yes.Enabled = false;
                    txtQuestion24Yes.Enabled = false;
                }
                else
                {
                    lblMainQuestion24.Enabled = true;
                    rdMainQuestion24_no.Enabled = true;
                    rdMainQuestion24_yes.Enabled = true;
                    txtQuestion24Yes.Enabled = true;
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
                if (rdATPL.Checked) { queAvia.tqa_avia_type = 'A'; chkLicense = true; }
                if (rdPPL.Checked) { queAvia.tqa_avia_type = 'P'; chkLicense = true; }
                if (rdATC.Checked) { queAvia.tqa_avia_type = 'T'; chkLicense = true; }
                if (rdCPL.Checked) { queAvia.tqa_avia_type = 'C'; chkLicense = true; }
                if (rdPE.Checked) { queAvia.tqa_avia_type = 'E'; chkLicense = true; }
                if (rdStudent.Checked) { queAvia.tqa_avia_type = 'S'; chkLicense = true; }
                if (rdOther.Checked) { queAvia.tqa_avia_type = 'O'; chkLicense = true; }
                if (!chkLicense) { queAvia.tqa_avia_type = (char?)null; }
                queAvia.tqa_avia_oths = string.IsNullOrEmpty(txtOtherLicense.Text) ? null : txtOtherLicense.Text;
                queAvia.tqa_place_exam = string.IsNullOrEmpty(txtplace.Text) ? null : txtplace.Text;
                queAvia.tqa_th_fullname = string.IsNullOrEmpty(txtname_th.Text) ? null : txtname_th.Text;
                queAvia.tqa_th_nation = string.IsNullOrEmpty(txtnation_th.Text) ? null : txtnation_th.Text;
                if (rdGender_M.Checked) { queAvia.tqa_sex = 'M'; } else if (rdGender_F.Checked) { queAvia.tqa_sex = 'F'; } else { queAvia.tqa_sex = (char?)null; }
                queAvia.tqa_dob = string.IsNullOrEmpty(txtDOB.Text) ? (DateTime?)null : DateTime.Parse(ut.ConvertDateToStringFormat(txtDOB.Text.Trim(), "yyyy-MM-dd"));
                queAvia.tqa_age_yrs = string.IsNullOrEmpty(txtAgeYear.Text) ? (float?)null : float.Parse(txtAgeYear.Text);
                queAvia.tqa_age_month = string.IsNullOrEmpty(txtAgeMonth.Text) ? (float?)null : float.Parse(txtAgeMonth.Text);
                queAvia.tqa_license_no = string.IsNullOrEmpty(txtLicenseNo.Text) ? null : txtLicenseNo.Text;
                if (rdAddress_S.Checked) { queAvia.tqa_chge_address = 'S'; } else if (rdAddress_C.Checked) { queAvia.tqa_chge_address = 'C'; } else { queAvia.tqa_chge_address = (char?)null; }
                queAvia.tqa_th_address = string.IsNullOrEmpty(txtAddressChanged.Text) ? null : txtAddressChanged.Text;
                queAvia.tqa_th_moblie = string.IsNullOrEmpty(txtTel.Text) ? null : txtTel.Text;
                queAvia.tqa_prev_exam_loc = string.IsNullOrEmpty(txtLastMedicalExam.Text) ? null : txtLastMedicalExam.Text;
                //Last medical exam
                bool chkLast = false;
                if (rdLastMedicalExam_F.Checked) { queAvia.tqa_prev_exam_deca = 'F'; chkLast = true; }
                if (rdLastMedicalExam_U.Checked) { queAvia.tqa_prev_exam_deca = 'U'; chkLast = true; }
                if (rdLastMedicalExam_L.Checked) { queAvia.tqa_prev_exam_deca = 'L'; chkLast = true; }
                if (!chkLast) { queAvia.tqa_prev_exam_deca = (char?)null; }                
                queAvia.tqa_tot_fling_time = string.IsNullOrEmpty(txtFlyTime.Text) ? (float?)null : float.Parse(txtFlyTime.Text);
                queAvia.tqa_last_six_time = string.IsNullOrEmpty(txtLast6Month.Text) ? (float?)null : float.Parse(txtLast6Month.Text);
                queAvia.tqa_aircraft_name = string.IsNullOrEmpty(txtACType.Text) ? null : txtACType.Text;
                bool chkMedicine = false;
                if (rdMedicalNo.Checked) { queAvia.tqa_use_medicine = 'N'; chkMedicine = true; }
                if (rdMedicalYes.Checked) { queAvia.tqa_use_medicine = 'Y'; chkMedicine = true; }
                if (!chkMedicine) { queAvia.tqa_use_medicine = (char?)null; }
                queAvia.tqa_med_name = string.IsNullOrEmpty(txtMedicalIfYes.Text) ? null : txtMedicalIfYes.Text;
                queAvia.tqa_med_amount = string.IsNullOrEmpty(txtMedicalQuantity.Text) ? null : txtMedicalQuantity.Text;
                queAvia.tqa_med_reason = string.IsNullOrEmpty(txtMedicalReason.Text) ? null : txtMedicalReason.Text;
                #endregion
                #region MedicalHistory
                if (rdMainQuestion14_no.Checked) { queAvia.tqa_chis_freq = 'N'; } else if (rdMainQuestion14_yes.Checked) { queAvia.tqa_chis_freq = 'Y'; tmpRemark += "\r\n" + lblMainQuestion14.Text + " ; " + txtQuestion14Yes.Text; } else { queAvia.tqa_chis_freq = (char?)(char?)null; } //14
                queAvia.tqa_chis_freq_rmk = txtQuestion14Yes.Text;
                if (rdMainQuestion15_no.Checked) { queAvia.tqa_chis_dizz = 'N'; } else if (rdMainQuestion15_yes.Checked) { queAvia.tqa_chis_dizz = 'Y'; tmpRemark += "\r\n" + lblMainQuestion15.Text + " ; " + txtQuestion15Yes.Text; } else { queAvia.tqa_chis_dizz = (char?)null; } //15
                queAvia.tqa_chis_dizz_rmk = txtQuestion15Yes.Text;
                if (rdMainQuestion16_no.Checked) { queAvia.tqa_chis_unco = 'N'; } else if (rdMainQuestion16_yes.Checked) { queAvia.tqa_chis_unco = 'Y'; tmpRemark += "\r\n" + lblMainQuestion16.Text + " ; " + txtQuestion16Yes.Text; } else { queAvia.tqa_chis_unco = (char?)null; } //16
                queAvia.tqa_chis_unco_rmk = txtQuestion16Yes.Text;
                if (rdMainQuestion17_no.Checked) { queAvia.tqa_chis_eyet = 'N'; } else if (rdMainQuestion17_yes.Checked) { queAvia.tqa_chis_eyet = 'Y'; tmpRemark += "\r\n" + lblMainQuestion17.Text + " ; " + txtQuestion17Yes.Text; } else { queAvia.tqa_chis_eyet = (char?)null; } //17
                queAvia.tqa_chis_eyet_rmk = txtQuestion17Yes.Text;
                if (rdMainQuestion18_no.Checked) { queAvia.tqa_chis_hayf = 'N'; } else if (rdMainQuestion18_yes.Checked) { queAvia.tqa_chis_hayf = 'Y'; tmpRemark += "\r\n" + lblMainQuestion18.Text + " ; " + txtQuestion18Yes.Text; } else { queAvia.tqa_chis_hayf = (char?)null; } //18
                queAvia.tqa_chis_hayf_rmk = txtQuestion18Yes.Text;
                if (rdMainQuestion19_no.Checked) { queAvia.tqa_chis_lung = 'N'; } else if (rdMainQuestion19_yes.Checked) { queAvia.tqa_chis_lung = 'Y'; tmpRemark += "\r\n" + lblMainQuestion19.Text + " ; " + txtQuestion19Yes.Text; } else { queAvia.tqa_chis_lung = (char?)null; } //19
                queAvia.tqa_chis_lung_rmk = txtQuestion19Yes.Text;
                if (rdMainQuestion20_no.Checked) { queAvia.tqa_chis_hert = 'N'; } else if (rdMainQuestion20_yes.Checked) { queAvia.tqa_chis_hert = 'Y'; tmpRemark += "\r\n" + lblMainQuestion20.Text + " ; " + txtQuestion20Yes.Text; } else { queAvia.tqa_chis_hert = (char?)null; } //20
                queAvia.tqa_chis_hert_rmk = txtQuestion20Yes.Text;
                if (rdMainQuestion21_no.Checked) { queAvia.tqa_chis_high = 'N'; } else if (rdMainQuestion21_yes.Checked) { queAvia.tqa_chis_high = 'Y'; tmpRemark += "\r\n" + lblMainQuestion21.Text + " ; " + txtQuestion21Yes.Text; } else { queAvia.tqa_chis_high = (char?)null; } //21
                queAvia.tqa_chis_high_rmk = txtQuestion21Yes.Text;
                if (rdMainQuestion22_no.Checked) { queAvia.tqa_chis_stom = 'N'; } else if (rdMainQuestion22_yes.Checked) { queAvia.tqa_chis_stom = 'Y'; tmpRemark += "\r\n" + lblMainQuestion22.Text + " ; " + txtQuestion22Yes.Text; } else { queAvia.tqa_chis_stom = (char?)null; } //22
                queAvia.tqa_chis_stom_rmk = txtQuestion22Yes.Text;
                if (rdMainQuestion23_no.Checked) { queAvia.tqa_chis_kidn = 'N'; } else if (rdMainQuestion23_yes.Checked) { queAvia.tqa_chis_kidn = 'Y'; tmpRemark += "\r\n" + lblMainQuestion23.Text + " ; " + txtQuestion23Yes.Text; } else { queAvia.tqa_chis_kidn = (char?)null; } //23
                queAvia.tqa_chis_kidn_rmk = txtQuestion23Yes.Text;
                if (rdMainQuestion24_no.Checked) { queAvia.tqa_chis_gyna = 'N'; } else if (rdMainQuestion24_yes.Checked) { queAvia.tqa_chis_gyna = 'Y'; tmpRemark += "\r\n" + lblMainQuestion24.Text + " ; " + txtQuestion24Yes.Text; } else { queAvia.tqa_chis_gyna = (char?)null; } //24
                queAvia.tqa_chis_gyna_rmk = txtQuestion24Yes.Text;
                if (rdMainQuestion25_no.Checked) { queAvia.tqa_chis_nurv = 'N'; } else if (rdMainQuestion25_yes.Checked) { queAvia.tqa_chis_nurv = 'Y'; tmpRemark += "\r\n" + lblMainQuestion25.Text + " ; " + txtQuestion25Yes.Text; } else { queAvia.tqa_chis_nurv = (char?)null; } //25
                queAvia.tqa_chis_nurv_rmk = txtQuestion25Yes.Text;
                if (rdMainQuestion26_no.Checked) { queAvia.tqa_chis_moti = 'N'; } else if (rdMainQuestion26_yes.Checked) { queAvia.tqa_chis_moti = 'Y'; tmpRemark += "\r\n" + lblMainQuestion26.Text + " ; " + txtQuestion26Yes.Text; } else { queAvia.tqa_chis_moti = (char?)null; } //26
                queAvia.tqa_chis_moti_rmk = txtQuestion26Yes.Text;
                if (rdMainQuestion27_no.Checked) { queAvia.tqa_chis_ment = 'N'; } else if (rdMainQuestion27_yes.Checked) { queAvia.tqa_chis_ment = 'Y'; tmpRemark += "\r\n" + lblMainQuestion27.Text + " ; " + txtQuestion27Yes.Text; } else { queAvia.tqa_chis_ment = (char?)null; } //27
                queAvia.tqa_chis_ment_rmk = txtQuestion27Yes.Text;
                if (rdMainQuestion28_no.Checked) { queAvia.tqa_chis_suic = 'N'; } else if (rdMainQuestion28_yes.Checked) { queAvia.tqa_chis_suic = 'Y'; tmpRemark += "\r\n" + lblMainQuestion28.Text + " ; " + txtQuestion28Yes.Text; } else { queAvia.tqa_chis_suic = (char?)null; } //28
                queAvia.tqa_chis_suic_rmk = txtQuestion28Yes.Text;
                if (rdMainQuestion29_no.Checked) { queAvia.tqa_chis_alco = 'N'; } else if (rdMainQuestion29_yes.Checked) { queAvia.tqa_chis_alco = 'Y'; tmpRemark += "\r\n" + lblMainQuestion29.Text + " ; " + txtQuestion29Yes.Text; } else { queAvia.tqa_chis_alco = (char?)null; } //29
                queAvia.tqa_chis_alco_rmk = txtQuestion29Yes.Text;
                if (rdMainQuestion30_no.Checked) { queAvia.tqa_chis_drug = 'N'; } else if (rdMainQuestion30_yes.Checked) { queAvia.tqa_chis_drug = 'Y'; tmpRemark += "\r\n" + lblMainQuestion30.Text + " ; " + txtQuestion30Yes.Text; } else { queAvia.tqa_chis_drug = (char?)null; } //30
                queAvia.tqa_chis_drug_rmk = txtQuestion30Yes.Text;
                if (rdMainQuestion31_no.Checked) { queAvia.tqa_chis_adms = 'N'; } else if (rdMainQuestion31_yes.Checked) { queAvia.tqa_chis_adms = 'Y'; tmpRemark += "\r\n" + lblMainQuestion31.Text + " ; " + txtQuestion31Yes.Text; } else { queAvia.tqa_chis_adms = (char?)null; } //31
                queAvia.tqa_chis_adms_rmk = txtQuestion31Yes.Text;
                if (rdMainQuestion32_no.Checked) { queAvia.tqa_chis_avia = 'N'; } else if (rdMainQuestion32_yes.Checked) { queAvia.tqa_chis_avia = 'Y'; tmpRemark += "\r\n" + lblMainQuestion32.Text + " ; " + txtQuestion32Yes.Text; } else { queAvia.tqa_chis_avia = (char?)null; } //32
                queAvia.tqa_chis_avia_rmk = txtQuestion32Yes.Text;
                if (rdMainQuestion33_no.Checked) { queAvia.tqa_chis_otha = 'N'; } else if (rdMainQuestion33_yes.Checked) { queAvia.tqa_chis_otha = 'Y'; tmpRemark += "\r\n" + lblMainQuestion33.Text + " ; " + txtQuestion33Yes.Text; } else { queAvia.tqa_chis_otha = (char?)null; } //33
                queAvia.tqa_chis_otha_rmk = txtQuestion33Yes.Text;
                if (rdMainQuestion34_no.Checked) { queAvia.tqa_chis_conviction = 'N'; } else if (rdMainQuestion34_yes.Checked) { queAvia.tqa_chis_conviction = 'Y'; tmpRemark += "\r\n" + lblMainQuestion34.Text + " ; " + txtQuestion34Yes.Text; } else { queAvia.tqa_chis_conviction = (char?)null; } //34
                queAvia.tqa_chis_conv_rmk = txtQuestion34Yes.Text;
                if (rdMainQuestion35_no.Checked) { queAvia.tqa_chis_othi = 'N'; } else if (rdMainQuestion35_yes.Checked) { queAvia.tqa_chis_othi = 'Y'; tmpRemark += "\r\n" + lblMainQuestion35.Text + " ; " + txtQuestion35Yes.Text; } else { queAvia.tqa_chis_othi = (char?)null; } //35
                queAvia.tqa_chis_othi_rmk = txtQuestion35Yes.Text; 
                #endregion                                
                if (txtRemark.Text.Count() > 0) { txtRemark.Text = txtRemark.Text.Replace(Session["remark"].ToString(), ""); }
                queAvia.tqa_remark = txtRemark.Text;
                Session["remark"] = tmpRemark;

                queAvia.tqa_update_by = "System";
                queAvia.tqa_update_date = currentDateTime;
                
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
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save('N');
            //Response.Write("<script language='javascript'>alert(Save Completed.);</script>");
            Response.Redirect("~/web/frm_verify_result.aspx");
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
        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            loadData(Session["HN"].ToString());
        }
    }
}
