using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class PersonalHealthManagmentUC : UserControl
    {
        public PersonalHealthManagmentUC()
        {
            InitializeComponent();            
        }
        string strTrue = "ใช่(Yes)";
        string strFalse = "ไม่ใช่(No)";
        DateTime currentDatetime;
        InhCheckupDataContext dbc;
        private string username;
        private trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                if (value == null)
                {
                    Clear();
                }
                else
                {
                    dbc = new InhCheckupDataContext();
                    mapComboBox();
                    try
                    {
                        username = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                        currentDatetime = Program.GetServerDateTime();

                        LoadPHMData(ref value);
                        
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "setProp tpr_patient_regis", ex, false);
                    }
                }
            }
        }

        public void EndEdit()
        {
            try
            {
                trn_phm_hdr objPHM = bsPHM.OfType<trn_phm_hdr>().FirstOrDefault();

                objPHM.tph_update_by = username;
                objPHM.tph_update_date = currentDatetime;

                //Cadiovascura Save PHM dtl Type ="C"
                var objdatatypeC = objPHM.trn_phm_dtls.Where(x => x.tpd_type == 'C').FirstOrDefault();
                if (objdatatypeC == null)
                {
                    objdatatypeC = new trn_phm_dtl();
                    objdatatypeC.tpd_type = 'C';
                    objdatatypeC.tpd_create_by = username;
                    objdatatypeC.tpd_create_date = currentDatetime;
                }
                objdatatypeC.tpd_category = txt2sec3_RiskCategory.Text;
                objdatatypeC.tpd_clinic_recommend = txt2sec3_Recommend.Text;
                objdatatypeC.tpd_refer_to_clinic = CBsec3_RefertoClinic.Text;
                objdatatypeC.tpd_protocal = txt2sec3_Protocol.Text;
                objdatatypeC.tpd_status = txt2sec3_StatusRefertoClinic.SelectedValue.ToString();
                objdatatypeC.tpd_status_other = txt2sec3_ReasonfornotReferRemark.Text;
                objdatatypeC.tpd_recomment = txt2sec3_Recommendation.Text;
                objdatatypeC.tpd_concern = txt2sec3_ConcernPointForTLC.Text;
                objdatatypeC.tpd_note = txt2sec3_Note.Text;
                objdatatypeC.tpd_diagnosis = "";

                objdatatypeC.tpd_update_by = username;
                objdatatypeC.tpd_update_date = currentDatetime;
                objPHM.trn_phm_dtls.Add(objdatatypeC);

                //Diabetes Save PHM dtl Type="D"
                var objdatatypeD = objPHM.trn_phm_dtls.Where(x => x.tpd_type == 'D').FirstOrDefault();
                if (objdatatypeD == null)
                {
                    objdatatypeD = new trn_phm_dtl();
                    objdatatypeD.tpd_type = 'D';
                    objdatatypeD.tpd_create_by = username;
                    objdatatypeD.tpd_create_date = currentDatetime;
                }
                objdatatypeD.tpd_category = txt3sec3_RiskCategory.Text;
                objdatatypeD.tpd_clinic_recommend = txt3sec3_Recommend.Text;
                objdatatypeD.tpd_refer_to_clinic = txt3sec3_ReferToClinic.Text;
                objdatatypeD.tpd_protocal = txt3sec3_Protocol.Text;
                objdatatypeD.tpd_status = txt3sec3_ReasonFornotRefer.SelectedValue.ToString();
                objdatatypeD.tpd_status_other = txt3sec3_ReasonfornotReferRemark.Text;
                objdatatypeD.tpd_recomment = txt3sec3_Recommendation.Text;
                objdatatypeD.tpd_concern = txt3sec3_ConcernPointsForTLC.Text;
                objdatatypeD.tpd_note = txt3sec3_txtFollowupPoint.Text;
                objdatatypeD.tpd_diagnosis = "";

                objdatatypeD.tpd_update_by = username;
                objdatatypeD.tpd_update_date = currentDatetime;
                objPHM.trn_phm_dtls.Add(objdatatypeD);

                //MeMograme Save PHM dtl Type="M"
                var objdatatypeM = objPHM.trn_phm_dtls.Where(x => x.tpd_type == 'M').FirstOrDefault();
                if (objdatatypeM == null)
                {
                    objdatatypeM = new trn_phm_dtl();
                    objdatatypeM.tpd_type = 'M';
                    objdatatypeM.tpd_create_by = username;
                    objdatatypeM.tpd_create_date = currentDatetime;
                }
                objdatatypeM.tpd_category = txt4sec1_BiradsCategory.Text;
                objdatatypeM.tpd_diagnosis = txt4sec1_Diagnosis.Text;
                objdatatypeM.tpd_clinic_recommend = txt4sec1_Recommend.Text;
                objdatatypeM.tpd_refer_to_clinic = txt4sec1_RefertoClinic.Text;
                objdatatypeM.tpd_protocal = txt4sec1_Protocal.Text;
                objdatatypeM.tpd_status = txt4sec1_StatusReferClinic.SelectedValue.ToString();
                objdatatypeM.tpd_status_other = txt4sec1_ReasonfornotReferRemark.Text;
                objdatatypeM.tpd_recomment = string.Empty;
                objdatatypeM.tpd_concern = string.Empty;
                objdatatypeM.tpd_note = txt4sec1_Remark.Text;

                objdatatypeM.tpd_update_by = username;
                objdatatypeM.tpd_update_date = currentDatetime;
                objPHM.trn_phm_dtls.Add(objdatatypeM);

                //AFP Type="A"
                var objdatatypeA = objPHM.trn_phm_dtls.Where(x => x.tpd_type == 'A').FirstOrDefault();
                if (objdatatypeA == null)
                {
                    objdatatypeA = new trn_phm_dtl();
                    objdatatypeA.tpd_type = 'A';
                    objdatatypeA.tpd_create_by = username;
                    objdatatypeA.tpd_create_date = currentDatetime;
                }
                objdatatypeA.tpd_category = txt5sec1_tumorMarkevafp.Text;
                objdatatypeA.tpd_diagnosis = string.Empty;
                objdatatypeA.tpd_clinic_recommend = txt5sec1_Recommend.Text;
                objdatatypeA.tpd_refer_to_clinic = txt5sec1_RefertoClinic.Text;
                objdatatypeA.tpd_protocal = txt5sec1_Protocal.Text;
                objdatatypeA.tpd_status = txt5sec1_StatusRefertoClinic.SelectedValue.ToString();
                objdatatypeA.tpd_status_other = txt5sec1_ReasonfornotReferRemark.Text;
                objdatatypeA.tpd_recomment = string.Empty;
                objdatatypeA.tpd_concern = string.Empty;
                objdatatypeA.tpd_note = txt5sec1_Remark.Text;

                objdatatypeA.tpd_update_by = username;
                objdatatypeA.tpd_update_date = currentDatetime;
                objPHM.trn_phm_dtls.Add(objdatatypeA);

                //CEA Type="E"
                var objdatatypeE = objPHM.trn_phm_dtls.Where(x => x.tpd_type == 'E').FirstOrDefault();
                if (objdatatypeE == null)
                {
                    objdatatypeE = new trn_phm_dtl();
                    objdatatypeE.tpd_type = 'E';
                    objdatatypeE.tpd_create_by = username;
                    objdatatypeE.tpd_create_date = currentDatetime;
                }
                objdatatypeE.tpd_category = txt5sec2_tumormarkevCFA.Text;
                objdatatypeE.tpd_diagnosis = string.Empty;
                objdatatypeE.tpd_clinic_recommend = txt5sec2_Recommend.Text;
                objdatatypeE.tpd_refer_to_clinic = txt5sec2_ReferToClinic.Text;
                objdatatypeE.tpd_protocal = txt5sec2_Protocal.Text;
                objdatatypeE.tpd_status = txt5sec2_StatusRefertoClinic.SelectedValue.ToString();
                objdatatypeE.tpd_status_other = txt5sec2_ReasonfornotReferRemark.Text;
                objdatatypeE.tpd_recomment = string.Empty;
                objdatatypeE.tpd_concern = string.Empty;
                objdatatypeE.tpd_note = txt5sec2_Remark.Text;

                objdatatypeE.tpd_update_by = username;
                objdatatypeE.tpd_update_date = currentDatetime;
                objPHM.trn_phm_dtls.Add(objdatatypeE);

                //PSA Type="P"
                var objdatatypeP = objPHM.trn_phm_dtls.Where(x => x.tpd_type == 'P').FirstOrDefault();
                if (objdatatypeP == null)
                {
                    objdatatypeP = new trn_phm_dtl();
                    objdatatypeP.tpd_type = 'P';
                    objdatatypeP.tpd_create_by = username;
                    objdatatypeP.tpd_create_date = currentDatetime;
                }
                objdatatypeP.tpd_category = txt5sec3_TumorMarkevPSA.Text;
                objdatatypeP.tpd_diagnosis = string.Empty;
                objdatatypeP.tpd_clinic_recommend = txt5sec3_Recommend.Text;
                objdatatypeP.tpd_refer_to_clinic = txt5sec3_ReferToClinic.Text;
                objdatatypeP.tpd_protocal = txt5sec3_Protocal.Text;
                objdatatypeP.tpd_status = txt5sec3_StatusRefertoClinic.SelectedValue.ToString();
                objdatatypeP.tpd_status_other = txt5sec3_ReasonfornotReferRemark.Text;
                objdatatypeP.tpd_recomment = string.Empty;
                objdatatypeP.tpd_concern = string.Empty;
                objdatatypeP.tpd_note = txt5sec3_Remark.Text;

                objdatatypeP.tpd_update_by = username;
                objdatatypeP.tpd_update_date = currentDatetime;
                objPHM.trn_phm_dtls.Add(objdatatypeP);
            }
            catch(Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit PHM", ex, false);
            }
        }
        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;            
        }
        private void ClearControl()
        {
            ClearPHMSec1();
            ClearPHMSec2();
            ClearPHMSec2_9();
            ClearCardioData();
            ClearDiabeteData();
            ClearMammoData();
            ClearTumorData();
            ClearRiskData();
        }

        private void mapComboBox()
        {
            var objRaceGroup = (from t1 in dbc.mst_race_grps
                                orderby t1.mag_ename
                                select new DropdownData
                                {
                                    Code = t1.mag_id,
                                    Name = t1.mag_ename
                                }).ToList();
            DropdownData newselect = new DropdownData();
            newselect.Name = "";
            objRaceGroup.Insert(0, newselect);
            DDRaceGroup.ValueMember = "Code";
            DDRaceGroup.DisplayMember = "Name";
            DDRaceGroup.DataSource = objRaceGroup;

            List<ComboboxItem> newbb = new List<ComboboxItem>();
            newbb.Add(new ComboboxItem("", ""));
            newbb.Add(new ComboboxItem("Make Appointment at Heart Clinic", "MH"));
            newbb.Add(new ComboboxItem("Other hospital", "O"));
            newbb.Add(new ComboboxItem("Private doctor", "P"));
            txt2sec3_StatusRefertoClinic.DataSource = newbb;
            txt2sec3_StatusRefertoClinic.DisplayMember = "Text";
            txt2sec3_StatusRefertoClinic.ValueMember = "Value";
            CBsec3_RefertoClinic.Items.Add(new ComboboxItem("", null));
            CBsec3_RefertoClinic.Items.Add(new ComboboxItem("Cardiology", "Cardiology"));
            CBsec3_RefertoClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("", null));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt2sec3_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));

            txt6sec1_StatusRefertoClinic.DataSource = newbb;
            txt6sec1_StatusRefertoClinic.DisplayMember = "Text";
            txt6sec1_StatusRefertoClinic.ValueMember = "Value";
            txt6sec1_RefertoClinic.Items.Add(new ComboboxItem("", null));
            txt6sec1_RefertoClinic.Items.Add(new ComboboxItem("Cardiology", "Cardiology"));
            txt6sec1_RefertoClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("", null));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt6sec1_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));

            List<ComboboxItem> newbb3 = new List<ComboboxItem>();
            newbb3.Add(new ComboboxItem("", ""));
            newbb3.Add(new ComboboxItem("Make Appointment at Heart Clinic", "MD"));
            newbb3.Add(new ComboboxItem("Other hospital", "O"));
            newbb3.Add(new ComboboxItem("Private doctor", "P"));
            txt3sec3_ReasonFornotRefer.DataSource = newbb3;
            txt3sec3_ReasonFornotRefer.DisplayMember = "Text";
            txt3sec3_ReasonFornotRefer.ValueMember = "Value";
            txt3sec3_ReferToClinic.Items.Add(new ComboboxItem("", null));
            txt3sec3_ReferToClinic.Items.Add(new ComboboxItem("Diabetic Center", "Diabetic Center"));
            txt3sec3_ReferToClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("", null));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt3sec3_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));

            txt6sec2_ReasonFornotRefer.DataSource = newbb3;
            txt6sec2_ReasonFornotRefer.DisplayMember = "Text";
            txt6sec2_ReasonFornotRefer.ValueMember = "Value";
            txt6sec2_ReferToClinic.Items.Add(new ComboboxItem("", null));
            txt6sec2_ReferToClinic.Items.Add(new ComboboxItem("Diabetic Center", "Diabetic Center"));
            txt6sec2_ReferToClinic.Items.Add(new ComboboxItem("PHM Center", "PHM Center"));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("", null));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("CPG", "CPG"));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("CPG / Care Plan", "CPG / Care Plan"));
            txt6sec2_Protocol.Items.Add(new ComboboxItem("Care Plan", "Care Plan"));

            SetDDReasonforNotRefer(txt4sec1_StatusReferClinic);
            SetDDReasonforNotRefer(txt5sec1_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt5sec2_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt5sec3_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec1_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec2_ReasonFornotRefer);
            SetDDReasonforNotRefer(txt6sec3_StatusReferClinic);
            SetDDReasonforNotRefer(txt6sec4_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec5_StatusRefertoClinic);
            SetDDReasonforNotRefer(txt6sec6_StatusRefertoClinic);
        }

        private void LoadPHMData(ref trn_patient_regi objPatientRegis)
        {
            int tpr_id = objPatientRegis.tpr_id;
            trn_patient objPatient = objPatientRegis.trn_patient;
            if (objPatient != null)
            {
                txtHn.Text = objPatient.tpt_hn_no;
                txtTitle.Text = objPatient.tpt_pre_name;
                txtFirstName.Text = objPatient.tpt_first_name;
                txtLastName.Text = objPatient.tpt_last_name;
                txtGender.Text = (objPatient.tpt_gender == 'F') ? "Female" : ((objPatient.tpt_gender == 'M') ? "Male" : "");
                var dobdate = new DateTime();
                try
                {
                    dobdate = Convert.ToDateTime(objPatient.tpt_dob);
                }
                catch (Exception)
                {
                }
                trn_phm_hdr objphm = objPatientRegis.trn_phm_hdrs.FirstOrDefault();
                if (objphm == null)
                {
                    objphm = new trn_phm_hdr();
                    objPatientRegis.trn_phm_hdrs.Add(objphm);                    
                }                
                txtAge.Text = (currentDatetime.Year - dobdate.Year).ToString();
                objphm.tph_age = (currentDatetime.Year - dobdate.Year);
                txtNation.Text = objPatient.tpt_nation_desc;
                //basic measure
                
                trn_basic_measure_dtl objbmdtl = (from t1 in dbc.trn_basic_measure_dtls
                                                  where t1.trn_basic_measure_hdr.tpr_id == tpr_id
                                                  select t1).FirstOrDefault();

                if (objbmdtl != null)
                {
                    txtBMI.Text = objbmdtl.tbd_bmi;
                    txtWeight.Text = objbmdtl.tbd_weight;
                    txtHeight.Text = objbmdtl.tbd_height;
                    txtWaistSize.Text = objbmdtl.tbd_waist;
                    txtSystolicBP.Text = objbmdtl.tbd_systolic;
                    txtDiastolicBP.Text = objbmdtl.tbd_diastolic;
                }                
                
                //ตาราง trn_ques_patient
                trn_ques_patient objqp = objPatientRegis.trn_ques_patients.FirstOrDefault();                
                if (objqp != null)
                {
                    txtSec2_1.Text = (objqp.tqp_ill_cmed_hyper == true) ? strTrue : strFalse;
                    txt2Sec_2.Text = (objqp.tqp_his_exercise == 'O' || objqp.tqp_his_exercise == 'N') ? strTrue : strFalse;
                    txt2Sec_3.Text = (objqp.tqp_fwm_over_weight == 'Y') ? strTrue : strFalse;
                    txt2Sec_4.Text = (objqp.tqp_his_smok == 'S' && (objqp.tqp_his_smok_dur <= 1 || objqp.tqp_his_smok_amt > 0)) ? strTrue : strFalse;
                    txt2Sec_5.Text = (objqp.tqp_vinf_hepB_virus == 'Y') ? strTrue : strFalse;
                    txt2Sec_6.Text = (objqp.tqp_vinf_hepA_virus == 'Y') ? strTrue : strFalse;
                    txt2Sec_7.Text = (objqp.tqp_his_alcohol == 'R') ? strTrue : strFalse;
                    txt2Sec_8.Text = (objqp.tqp_vinf_vaccine == 'L') ? strTrue : strFalse;

                    txt91Coronary.Text = (objqp.tqp_fhis_fdis_coro == true && (objqp.tqp_fhis_fdis_coro_cs == 'B' || objqp.tqp_fhis_fdis_coro_cs == 'N')) ? strTrue : strFalse;
                    txt91Diabetes.Text = (objqp.tqp_fhis_fdis_diab == true) ? strTrue : strFalse;
                    txt91Stroke.Text = (objqp.tqp_fhis_fdis_stro == true || objqp.tqp_fhis_fdis_para == true) ? strTrue : strFalse;
                    txt91Hypertension.Text = (objqp.tqp_fhis_fdis_hyper == true) ? strTrue : strFalse;
                    txt91Cancer.Text = (objqp.tqp_fhis_fdis_canc == true) ? strTrue : strFalse;
                    txt91Dyslipidemia.Text = (objqp.tqp_fhis_fdis_dysl == true) ? strTrue : strFalse;

                    txt92Coronary.Text = (objqp.tqp_fhis_mdis_coro == true && (objqp.tqp_fhis_mdis_coro_cs == 'B' || objqp.tqp_fhis_mdis_coro_cs == 'N')) ? strTrue : strFalse;
                    txt92Diabetes.Text = (objqp.tqp_fhis_mdis_diab == true) ? strTrue : strFalse;
                    txt92Stroke.Text = (objqp.tqp_fhis_mdis_stro == true || objqp.tqp_fhis_mdis_para == true) ? strTrue : strFalse;
                    txt92Hypertension.Text = (objqp.tqp_fhis_mdis_hyper == true) ? strTrue : strFalse;
                    txt92Cancer.Text = (objqp.tqp_fhis_mdis_canc == true) ? strTrue : strFalse;
                    txt92Dyslipidemia.Text = (objqp.tqp_fhis_mdis_dysl == true) ? strTrue : strFalse;

                    txt93Coronary.Text = (objqp.tqp_fhis_bdis_coro == true && (objqp.tqp_fhis_bdis_coro_bfm == true || objqp.tqp_fhis_bdis_coro_nfm == true || objqp.tqp_fhis_bdis_coro_bm == true || objqp.tqp_fhis_bdis_coro_nm == true)) ? strTrue : strFalse;
                    txt93Diabetes.Text = (objqp.tqp_fhis_bdis_diab == true) ? strTrue : strFalse;
                    txt93Stroke.Text = (objqp.tqp_fhis_bdis_stro == true || objqp.tqp_fhis_bdis_para == true) ? strTrue : strFalse;
                    txt93Hypertension.Text = (objqp.tqp_fhis_bdis_hyper == true) ? strTrue : strFalse;
                    txt93Cancer.Text = (objqp.tqp_fhis_bdis_canc == true) ? strTrue : strFalse;
                    txt93Dyslipidemia.Text = (objqp.tqp_fhis_bdis_dysl == true) ? strTrue : strFalse;

                    txt10Coronary.Text = (objqp.tqp_ill_med_coro == true) ? strTrue : strFalse;
                    txt10Abdominal.Text = (objqp.tqp_ill_med_abdd == true) ? strTrue : strFalse;
                    txt10Periperal.Text = (objqp.tqp_ill_med_cper == true) ? strTrue : strFalse;
                    txt10Transient.Text = (objqp.tqp_ill_med_sist == true || objqp.tqp_ill_med_para == true || objqp.tqp_ill_med_stro == true) ? strTrue : strFalse;
                    txt10Diabetes.Text = (objqp.tqp_ill_med_diab == true) ? strTrue : strFalse;
                    txt10Cancer.Text = (objqp.tqp_ill_med_canc == true) ? strTrue : strFalse;
                    txt10Hypertention.Text = (objqp.tqp_ill_med_hyper == true) ? strTrue : strFalse;
                    txt10Dyslipidemia.Text = (objqp.tqp_ill_med_dysl == true) ? strTrue : strFalse;
                }
                
                LoadCardiovascular(ref objPatientRegis);
                LoadDiabetes(ref objPatientRegis);
                LoadMammogram(ref objPatientRegis);
                LoadTumorMarker(ref objPatientRegis);
                LoadRiskSummary();
            }
            else
            {
                ClearControl();
            }
        }
        private void ClearPHMSec1()
        {
            txtHn.Text = null;
            txtTitle.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            txtGender.Text = null;
            bsPHM.OfType<trn_phm_hdr>().FirstOrDefault().tph_age = null;
            txtNation.Text = null;
            txtBMI.Text = null;
            txtWeight.Text = null;
            txtHeight.Text = null;
            txtWaistSize.Text = null;
            txtSystolicBP.Text = null;
            txtDiastolicBP.Text = null;
            DDRaceGroup.SelectedIndex = 0;
            DDRace.SelectedIndex = 0;            
        }
        private void ClearPHMSec2()
        {
            txtSec2_1.Text = null;
            txt2Sec_2.Text = null;
            txt2Sec_3.Text = null;
            txt2Sec_4.Text = null;
            txt2Sec_5.Text = null;
            txt2Sec_6.Text = null;
            txt2Sec_7.Text = null;
            txt2Sec_8.Text = null;            
        }
        private void ClearPHMSec2_9()
        {
            txt91Coronary.Text = null;
            txt91Diabetes.Text = null;
            txt91Stroke.Text = null;
            txt91Hypertension.Text = null;
            txt91Cancer.Text = null;
            txt91Dyslipidemia.Text = null;

            txt92Coronary.Text = null;
            txt92Diabetes.Text = null;
            txt92Stroke.Text = null;
            txt92Hypertension.Text = null;
            txt92Cancer.Text = null;
            txt92Dyslipidemia.Text = null;

            txt93Coronary.Text = null;
            txt93Diabetes.Text = null;
            txt93Stroke.Text = null;
            txt93Hypertension.Text = null;
            txt93Cancer.Text = null;
            txt93Dyslipidemia.Text = null;

            txt10Coronary.Text = null;
            txt10Abdominal.Text = null;
            txt10Periperal.Text = null;
            txt10Transient.Text = null;
            txt10Diabetes.Text = null;
            txt10Cancer.Text = null;
            txt10Hypertention.Text = null;
            txt10Dyslipidemia.Text = null;

            bsPHM.OfType<trn_phm_hdr>().FirstOrDefault().tph_remark = null;
        }

        private void LoadCardiovascular(ref trn_patient_regi objPatientRegis)
        {
            int tpr_id = objPatientRegis.tpr_id;
            string tpt_hn_no = objPatientRegis.trn_patient.tpt_hn_no;
            string tpr_en_no = objPatientRegis.tpr_en_no;
            trn_ques_patient objqp = objPatientRegis.trn_ques_patients.FirstOrDefault();
            trn_phm_hdr currentPHM = objPatientRegis.trn_phm_hdrs.FirstOrDefault();
            if (objqp != null)
            {
                #region CardiovascularSec1
                //Smoke
                txt2sec1_Smoke.Text = (objqp.tqp_his_smok == 'S') ? strTrue : strFalse;
                txt2sec1_SmokePoint.Text = (txt2sec1_Smoke.Text == strTrue) ? "1" : "0";
                currentPHM.tph_smok_pt = Utility.GetInteger(txt2sec1_SmokePoint.Text);
                //Family CHD
                var objdata = ((objqp.tqp_fhis_fdis_coro == true && (objqp.tqp_fhis_fdis_coro_cs == 'B' || objqp.tqp_fhis_fdis_coro_cs == 'N'))
                    || (objqp.tqp_fhis_mdis_coro == true && (objqp.tqp_fhis_mdis_coro_cs == 'B' || objqp.tqp_fhis_mdis_coro_cs == 'N'))
                    || (objqp.tqp_fhis_bdis_coro == true && (objqp.tqp_fhis_bdis_coro_bfm == true || objqp.tqp_fhis_bdis_coro_nfm == true || objqp.tqp_fhis_bdis_coro_bm == true || objqp.tqp_fhis_bdis_coro_nm == true))) ? "1" : "0";
                txt2sec1_FamilyHistory.Text = (objdata == "1") ? strTrue : strFalse;
                txt2sec1_familyhistoryPoint.Text = objdata;
                currentPHM.tph_fhis_CHD_pt = Utility.GetInteger(txt2sec1_familyhistoryPoint.Text);
                //BP
                txt2sec1_SystolicBP.Text = txtSystolicBP.Text;
                txt2sec1_DiastolicBP.Text = txtDiastolicBP.Text;
                int SysBP = Utility.GetInteger(txt2sec1_SystolicBP.Text);
                trn_basic_measure_dtl objbmdtl = (from t1 in dbc.trn_basic_measure_dtls
                                                  where t1.trn_basic_measure_hdr.tpr_id == tpr_id
                                                  select t1).FirstOrDefault();
                txt2sec1_HighBloodPoint.Text = ((Utility.GetInteger(txt2sec1_SystolicBP.Text) >= 140
                                                || Utility.GetInteger(txt2sec1_DiastolicBP.Text) >= 90)
                                                || (objqp != null && objqp.tqp_ill_cmed_hyper == true)) ? "1" : "0";
                if (Convert.ToInt32(txt2sec1_HighBloodPoint.Text) > 0)
                {
                    ChangeForeColor(txt2sec1_SystolicBP);
                    ChangeForeColor(txt2sec1_DiastolicBP);
                    ChangeForeColor(txt2sec1_HighBloodPoint);
                }
                currentPHM.tph_high_blood_pt = Utility.GetInteger(txt2sec1_HighBloodPoint.Text);
                txt2sec1_DrugforhighBlood.Text = (objqp.tqp_ill_cmed_hyper == true) ? strTrue : strFalse;
                //Age & Gender
                txt2sec1_Age.Text = txtAge.Text;
                txt2sec1_Gender.Text = txtGender.Text;
                txt2sec1_AgePoint.Text = ((objPatientRegis.trn_patient.tpt_gender == 'M' && Utility.GetInteger(txt2sec1_Age.Text) > 45)
                                        || (objPatientRegis.trn_patient.tpt_gender == 'F' && Utility.GetInteger(txt2sec1_Age.Text) > 55)) ? "1" : "0";
                currentPHM.tph_age_by_gender_pt = Utility.GetInteger(txt2sec1_AgePoint.Text);
                //Cholesteral
                var objLab = (from t1 in dbc.trn_patient_labs
                              where t1.tpl_lab_no == "C0150" && t1.tpl_hn_no == tpt_hn_no && t1.tpl_en_no == tpr_en_no
                              //&& ((Program.GetServerDateTime().Date - t1.tpl_lab_date.Value.Date).Days <= 365)
                              orderby t1.tpl_lab_date descending
                              select t1.tpl_lab_value).FirstOrDefault();
                if (objLab != null)
                {//HDL Cholesterol Sec1
                    txt2sec1_HDLCholesterol.Text = objLab;
                    int HDLvalue = Utility.GetInteger(txt2sec1_HDLCholesterol.Text.Trim());
                    if (HDLvalue < 40)
                    {
                        txt2sec1_HDLPoint.Text = "1";
                        //currentPHM.tph_hdl_pt = 1;
                    }
                    else if (40 <= HDLvalue && HDLvalue < 60)
                    {
                        txt2sec1_HDLPoint.Text = "0";
                        //currentPHM.tph_hdl_pt = 0;
                    }
                    else if (HDLvalue >= 60)
                    {
                        txt2sec1_HDLPoint.Text = "-1";
                        //currentPHM.tph_hdl_pt = -1;
                    }
                }
                else
                {
                    txt2sec1_HDLCholesterol.Text = "";
                    txt2sec1_HDLPoint.Text = "0";
                    //currentPHM.tph_hdl_pt = 0;
                }
                currentPHM.tph_hdl_pt = Utility.GetInteger(txt2sec1_HDLPoint.Text);
                //Pre-Cardio Result
                int v_pre_sum = Convert.ToInt32(txt2sec1_SmokePoint.Text) + Convert.ToInt32(txt2sec1_HighBloodPoint.Text) +
                    Convert.ToInt32(txt2sec1_familyhistoryPoint.Text) + Convert.ToInt32(txt2sec1_AgePoint.Text) + Convert.ToInt32(txt2sec1_HDLPoint.Text);
                txt2sec1_PreCardioCount.Text = Convert.ToString(v_pre_sum);
                currentPHM.tph_pre_cardio_pt = Utility.GetInteger(txt2sec1_PreCardioCount.Text);
                #endregion
                #region CardiovascularSec2
                //Gender,Age,BP,Smoke
                txt2sec2_Gender.Text = txt2sec1_Gender.Text;
                txt2sec2_Age.Text = txt2sec1_Age.Text;
                txt2sec2_SystolicBP.Text = txt2sec1_SystolicBP.Text;
                txt2sec2_DiastolicBP.Text = txt2sec1_DiastolicBP.Text;
                txt2sec2_DrugforhighBlood.Text = txt2sec1_DrugforhighBlood.Text;
                txt2sec2_Smoke.Text = txt2sec1_Smoke.Text;
                //Risk Score
                double riskscore = 0;
                int HDL = 0;
                if (txt2sec1_HDLCholesterol.Text == "")
                {
                    HDL = -9999;
                }
                else
                {
                    HDL = Utility.GetInteger(txt2sec1_HDLCholesterol.Text.Trim());
                }
                string gendervalue = (objPatientRegis.trn_patient.tpt_gender == null) ? "M" : objPatientRegis.trn_patient.tpt_gender.ToString();
                var totalcholestoral = Utility.GetInteger(txt2sec2_TotalCholesterol.Text);
                var agevalue = Utility.GetInteger(txt2sec1_Age.Text);
                var objriskscore = (from t1 in dbc.mst_phm_cfg_dtls
                                    where t1.mst_phm_cfg_hdr.mph_code == "FH01"
                                    && t1.mpd_str_1 == gendervalue
                                    && agevalue >= t1.mpd_min_num1
                                    && agevalue <= t1.mpd_max_num1
                                    select t1).FirstOrDefault();
                if (objriskscore != null)
                {
                    currentPHM.tph_cardio_age_pt = Convert.ToDouble(objriskscore.mpd_num_value);
                    riskscore = Convert.ToDouble(objriskscore.mpd_num_value);
                }
                var objriskscore2 = (from t1 in dbc.mst_phm_cfg_dtls
                                     where t1.mst_phm_cfg_hdr.mph_code == "FH02"
                                     && t1.mpd_str_1 == gendervalue
                                     && agevalue >= t1.mpd_min_num1
                                     && agevalue <= t1.mpd_max_num1
                                     && totalcholestoral >= t1.mpd_min_num2
                                     && totalcholestoral <= t1.mpd_max_num2
                                     select t1).FirstOrDefault();
                if (objriskscore2 != null)
                {
                    currentPHM.tph_cardio_totcholes_pt = Convert.ToDouble(objriskscore2.mpd_num_value);
                    riskscore += Convert.ToDouble(objriskscore2.mpd_num_value);
                }
                if (objqp != null)
                {
                    string issmoke = (objqp.tqp_his_smok == 'S') ? "S" : "N";
                    var objriskscore3 = (from t1 in dbc.mst_phm_cfg_dtls
                                         where t1.mst_phm_cfg_hdr.mph_code == "FH03"
                                         && t1.mpd_str_1 == gendervalue
                                         && agevalue >= t1.mpd_min_num1
                                         && agevalue <= t1.mpd_max_num1
                                         && t1.mpd_str_2 == issmoke
                                         select t1).FirstOrDefault();
                    if (objriskscore3 != null)
                    {
                        currentPHM.tph_cardio_smoke_pt = Convert.ToDouble(objriskscore3.mpd_num_value);
                        riskscore += Convert.ToDouble(objriskscore3.mpd_num_value);
                    }
                }
                var objriskscore4 = (from t1 in dbc.mst_phm_cfg_dtls
                                     where t1.mst_phm_cfg_hdr.mph_code == "FH04"
                                     && t1.mpd_str_1 == gendervalue
                                     && HDL >= t1.mpd_min_num1
                                     && HDL <= t1.mpd_max_num1
                                     select t1).FirstOrDefault();
                if (objriskscore4 != null)
                {
                    currentPHM.tph_cardio_choles_pt = Convert.ToDouble(objriskscore4.mpd_num_value);
                    riskscore += Convert.ToDouble(objriskscore4.mpd_num_value);
                }
                if (objqp != null)
                {
                    string istreat = (objqp.tqp_pill_adm == 'Y' || objqp.tqp_pill_sur == 'Y') ? "T" : "U";
                    var objriskscore5 = (from t1 in dbc.mst_phm_cfg_dtls
                                         where t1.mst_phm_cfg_hdr.mph_code == "FH05"
                                         && t1.mpd_str_1 == gendervalue
                                         && t1.mpd_str_2 == istreat
                                         && SysBP >= t1.mpd_min_num1
                                         && SysBP <= t1.mpd_max_num1
                                         select t1).FirstOrDefault();
                    if (objriskscore5 != null)
                    {
                        currentPHM.tph_cardio_bp_pt = Convert.ToDouble(objriskscore5.mpd_num_value);
                        riskscore += Convert.ToDouble(objriskscore5.mpd_num_value);
                    }
                }
                txt2sec2_TotalRiskScore.Text = riskscore.ToString();
                currentPHM.tph_total_risk_pt = Convert.ToDouble(txt2sec2_TotalRiskScore.Text);
                var objcompare = (from t1 in dbc.mst_phm_cfg_dtls
                                  where t1.mst_phm_cfg_hdr.mph_code == "FH06"
                                  && t1.mpd_str_1 == gendervalue
                                  && riskscore >= t1.mpd_min_num1
                                  && riskscore <= t1.mpd_max_num1
                                  select t1).FirstOrDefault();
                if (objcompare != null)
                {                    
                    txt2sec2_10yearRiskScore.Text = objcompare.mpd_str_value1;
                    currentPHM.tph_10y_risk_score = txt2sec2_10yearRiskScore.Text;
                }
                //HDL Cholesterol  
                int icount = 0;
                var objlab2 = Getlab("C0150", tpt_hn_no);
                foreach (trn_patient_lab item in objlab2)
                {
                    if (icount < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == currentDatetime.Date)
                        {
                            txt2sec2_HDLCholesterol.Text = item.tpl_lab_value.ToString();
                            txt2sec2_HDLCholesterolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_hdl_choles = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_hdl_date = item.tpl_lab_date.Value;
                            icount = icount + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != currentDatetime.Date)
                        {
                            txt2sec2_PreviousHDLCholesterol.Text = item.tpl_lab_value.ToString();
                            txt2sec2_PreviousHDLCholesterolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_prev_hdl = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_prev_hdl_date = item.tpl_lab_date.Value;
                            icount = icount + 2;
                        }
                    }
                } 
                //Total Cholesterol Sec2
                int icounttotal = 0;
                var objtotalcholesterol = Getlab("C0130", tpt_hn_no);
                foreach (trn_patient_lab item in objtotalcholesterol)
                {
                    if (icounttotal < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == currentDatetime.Date)
                        {
                            txt2sec2_TotalCholesterol.Text = item.tpl_lab_value.ToString();
                            txt2sec2_TotalCholesterolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_tot_choles = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_choles_date = item.tpl_lab_date.Value;
                            icounttotal = icounttotal + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != currentDatetime.Date)
                        {
                            txt2sec2_PreTotalCholesterol.Text = item.tpl_lab_value.ToString();
                            txt2sec2_PreTotalcholestorolDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_prev_choles = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_prev_choles_date = item.tpl_lab_date.Value;
                            icounttotal = icounttotal + 2;
                        }
                    }
                } 
                #endregion
                #region CardiovascularSec3
                //LDL
                int count = 0;
                var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1).FirstOrDefault();
                var objLDLSec3 = Getlab("C0159", tpt_hn_no);
                foreach (trn_patient_lab item in objLDLSec3)
                {
                    if (count < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == currentDatetime.Date)
                        {
                            txt2sec3_LDL.Text = item.tpl_lab_value.ToString();
                            txt2sec3_LDLDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_ldl_choles = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_ldl_date = item.tpl_lab_date.Value;
                            count = count + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != currentDatetime.Date)
                        {
                            txt2sec3_PreLDL.Text = item.tpl_lab_value.ToString();
                            txt2sec3_PreLDLDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_prev_ldl = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_prev_ldl_date = item.tpl_lab_date.Value;
                            count = count + 2;
                        }
                    }
                }
                //Risk category
                var riskType = "";
                int fhisCHDpt = Convert.ToInt32(currentPHM.tph_fhis_CHD_pt);
                int totalRiskPt = Convert.ToInt32(currentPHM.tph_total_risk_pt);
                if ((objqp != null &&
                 (objqp.tqp_ill_med_coro == true || objqp.tqp_ill_med_cper == true || objqp.tqp_ill_med_abdd == true || objqp.tqp_ill_med_sist == true ||
                  objqp.tqp_ill_med_para == true || objqp.tqp_ill_med_stro == true || objqp.tqp_ill_med_diab == true)) ||
                 currentPHM.tph_pre_cardio_pt >= 2 || totalRiskPt > 20)
                {
                    riskType = "H";//"High Risk (H)";
                }
                else if (currentPHM.tph_pre_cardio_pt >= 2 && totalRiskPt >= 10 && totalRiskPt <= 20)
                {
                    riskType = "D";//"Moderate High Risk (D)";
                }
                else if (currentPHM.tph_pre_cardio_pt >= 2 && totalRiskPt < 10)
                {
                    riskType = "M";// "Moderate Risk(M)";
                }
                else if (currentPHM.tph_pre_cardio_pt == 0 || currentPHM.tph_pre_cardio_pt == 1)
                {
                    riskType = "L";//"Lower Risk (L)";
                }
                var objdatatypeC = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'C').FirstOrDefault();
                if (objdatatypeC == null)
                {
                    var objreferclinic = (from t1 in dbc.mst_phm_cfg_dtls
                                          where t1.mst_phm_cfg_hdr.mph_code == "DB11"
                                          && t1.mpd_str_1 == riskType
                                          select t1).ToList();
                    if (objreferclinic.Count > 0)
                    {
                        txt2sec3_RiskCategory.Text = objreferclinic.FirstOrDefault().mpd_str_value1;
                        txt2sec3_Recommend.Text = objreferclinic.FirstOrDefault().mpd_str_value2;
                        CBsec3_RefertoClinic.Text = objreferclinic.FirstOrDefault().mpd_str_value3;
                        txt2sec3_Protocol.Text = objreferclinic.FirstOrDefault().mpd_str_value3;
                        txt2sec3_Recommendation.SelectedText = objreferclinic.FirstOrDefault().mpd_str_value4;
                    }
                }
                else
                {//แสดงข้อมูลทีเคยบันทึก
                    txt2sec3_RiskCategory.Text = objdatatypeC.tpd_category;
                    txt2sec3_Recommend.Text = objdatatypeC.tpd_clinic_recommend;
                    CBsec3_RefertoClinic.Text = objdatatypeC.tpd_refer_to_clinic;
                    txt2sec3_Protocol.Text = objdatatypeC.tpd_protocal;
                    txt2sec3_StatusRefertoClinic.SelectedValue = objdatatypeC.tpd_status;
                    txt2sec3_ReasonfornotReferRemark.Text = objdatatypeC.tpd_status_other;
                    txt2sec3_Recommendation.Text = objdatatypeC.tpd_recomment;
                    txt2sec3_ConcernPointForTLC.Text = objdatatypeC.tpd_concern;
                    txt2sec3_Note.Text = objdatatypeC.tpd_note;
                }
                #endregion
                #region CardiovascularSec4
                //Menstural History
                txt2sec4_Smoke.Text = txt2sec1_Smoke.Text;
                txt2sec4_SmokeRisk.Text = txt2sec1_Smoke.Text;
                currentPHM.tph_rk_smoking = (objqp.tqp_his_smok == 'S') ? true : false;

                txt2sec4_HighBloodPressure.Text = (Convert.ToInt32(txt2sec1_SystolicBP.Text) >= 140 || Convert.ToInt32(txt2sec1_DiastolicBP.Text) >= 90) ? strTrue : strFalse;
                txt2sec4_HighBloodPressureRisk.Text = (txt2sec1_HighBloodPoint.Text == "1") ? strTrue : strFalse;
                ////currentPHM.tph_high_blood_pt = Convert.ToInt32(txt2sec1_HighBloodPoint.Text);
                currentPHM.tph_rk_high_blood = (txt2sec1_HighBloodPoint.Text == "1") ? true : false;
                txt2sec4_Systolic.Text = txt2sec1_SystolicBP.Text;
                txt2sec4_Diastolic.Text = txt2sec1_DiastolicBP.Text;
                txt2sec4_BloodPerssureMedication.Text = txt2sec1_DrugforhighBlood.Text;

                if (txt2sec1_HDLCholesterol.Text == "")
                {
                    txt2sec4_HDLcholesterol.Text = "";
                    txt2sec4_HDLcholesterolRisk.Text = "";
                }
                else
                {
                    txt2sec4_HDLcholesterol.Text = (txt2sec1_HDLPoint.Text == "1") ? strTrue : strFalse;
                    txt2sec4_HDLcholesterolRisk.Text = (txt2sec4_HDLcholesterol.Text == strTrue) ? strTrue : strFalse;
                }
                currentPHM.tph_hdl_pt = Convert.ToInt32(txt2sec4_HDLcholesterol.Text);
                currentPHM.tph_rk_hdl = (txt2sec4_HDLcholesterol.Text == "1") ? true : false;
                //Un-Modifiable Risk Factor
                txt2sec42_familyCHD.Text = txt2sec1_FamilyHistory.Text;
                txt2sec42_familyCHDRisk.Text = txt2sec42_familyCHD.Text;
                ////currentPHM.tph_fhis_CHD_pt = Convert.ToInt32(txt2sec1_familyhistoryPoint.Text);
                currentPHM.tph_rk_CHD = (currentPHM.tph_fhis_CHD_pt == 1) ? true : false;

                txt2sec42_Agemale45.Text = (txt2sec1_AgePoint.Text == "1") ? strTrue : strFalse;
                txt2sec42_Age45Risk.Text = (txt2sec42_Agemale45.Text == strTrue) ? strTrue : strFalse;
                ////currentPHM.tph_age_by_gender_pt = Convert.ToInt32(txt2sec42_Agemale45.Text);
                currentPHM.tph_rk_age = (txt2sec1_AgePoint.Text == "1") ? true : false;

                txt2sec42_Gender.Text = txt2sec1_Gender.Text;
                txt2sec42_Age.Text = txt2sec1_Age.Text;
                //Risk of medical history
                txt2sec43_Coro.Text = txt10Coronary.Text;
                txt2sec43_Peripheral.Text = txt10Periperal.Text;
                txt2sec43_Abdominal.Text = txt10Abdominal.Text;
                txt2sec43_Transient.Text = txt10Transient.Text;
                txt2sec43_Diabetes.Text = txt10Diabetes.Text;
                //Additional Follow up Items
                txt2sec44_TotalCholesterol.Text = txt2sec2_TotalCholesterol.Text;
                txt2sec44_LDLChesterol.Text = txt2sec3_LDL.Text;
                #endregion
            }            
        }
        private void ClearCardioData()
        {
            trn_phm_hdr objPHM = bsPHM.OfType<trn_phm_hdr>().FirstOrDefault();
            //Sec1
            txt2sec1_Smoke.Text = null;
            objPHM.tph_smok_pt = null;
            txt2sec1_FamilyHistory.Text = null;
            objPHM.tph_fhis_CHD_pt = null;
            txt2sec1_SystolicBP.Text = null;
            txt2sec1_DiastolicBP.Text = null;
            objPHM.tph_high_blood_pt = null;
            txt2sec1_DrugforhighBlood.Text = null;
            txt2sec1_Age.Text = null;
            txt2sec1_Gender.Text = null;
            objPHM.tph_age_by_gender_pt = null;
            txt2sec1_HDLCholesterol.Text = null;
            objPHM.tph_hdl_pt = null;
            objPHM.tph_pre_cardio_pt = null;
            //Sec2
            txt2sec2_Gender.Text = null;
            txt2sec2_Age.Text = null;
            txt2sec2_SystolicBP.Text = null;
            txt2sec2_DiastolicBP.Text = null;
            txt2sec2_DrugforhighBlood.Text = null;
            txt2sec2_Smoke.Text = null;
            objPHM.tph_total_risk_pt = null;
            objPHM.tph_10y_risk_score = null;
            objPHM.tph_hdl_choles = null;
            objPHM.tph_hdl_date = null;
            objPHM.tph_prev_hdl = null;
            objPHM.tph_prev_hdl_date = null;
            objPHM.tph_tot_choles = null;
            objPHM.tph_choles_date = null;
            objPHM.tph_prev_choles = null;
            objPHM.tph_prev_choles_date = null;
            //Sec3
            objPHM.tph_ldl_choles = null;
            objPHM.tph_ldl_date = null;
            objPHM.tph_prev_ldl = null;
            objPHM.tph_prev_ldl_date = null;
            txt2sec3_RiskCategory.Text = null; ;
            txt2sec3_Recommend.Text = null;
            CBsec3_RefertoClinic.SelectedIndex = 0;
            txt2sec3_Protocol.SelectedIndex = 0;
            txt2sec3_StatusRefertoClinic.SelectedIndex = 0;
            txt2sec3_ReasonfornotReferRemark.Text = null;
            txt2sec3_Recommendation.Text = null;
            txt2sec3_ConcernPointForTLC.Text = null;
            txt2sec3_Note.Text = null;
            //Sec4
            txt2sec4_Smoke.Text = null;
            txt2sec4_SmokeRisk.Text = null;
            txt2sec4_HighBloodPressure.Text = null;
            txt2sec4_HighBloodPressureRisk.Text = null;
            txt2sec4_Systolic.Text = null;
            txt2sec4_Diastolic.Text = null;
            txt2sec4_BloodPerssureMedication.Text = null;
            txt2sec4_HDLcholesterol.Text = null;
            txt2sec4_HDLcholesterolRisk.Text = null;
            txt2sec42_familyCHD.Text = null;
            txt2sec42_familyCHDRisk.Text = null;
            txt2sec42_Agemale45.Text = null;
            txt2sec42_Age45Risk.Text = null;
            txt2sec42_Gender.Text = null;
            txt2sec42_Age.Text = null;
            txt2sec43_Coro.Text = null;
            txt2sec43_Peripheral.Text = null;
            txt2sec43_Abdominal.Text = null;
            txt2sec43_Transient.Text = null;
            txt2sec43_Diabetes.Text = null;
            txt2sec44_TotalCholesterol.Text = null;
            txt2sec44_LDLChesterol.Text = null;
        }

        private void LoadDiabetes(ref trn_patient_regi objPatientRegis)
        { 
            int tpr_id = objPatientRegis.tpr_id;
            string tpt_hn_no = objPatientRegis.trn_patient.tpt_hn_no;
            string tpr_en_no = objPatientRegis.tpr_en_no;
            string Gender = objPatientRegis.trn_patient.tpt_gender.ToString();
            trn_ques_patient objqp = objPatientRegis.trn_ques_patients.FirstOrDefault();
            trn_phm_hdr currentPHM = objPatientRegis.trn_phm_hdrs.FirstOrDefault();
            if (objqp != null)
            {
                #region DiabetesSec1
                //BMI
                txt3sec1_BMIResule.Text = txtBMI.Text;
                double bmivalue = Convert.ToDouble(txt3sec1_BMIResule.Text);
                string raceGroup = "";
                if (DDRaceGroup.SelectedValue != null)
                {
                    raceGroup = (DDRaceGroup.Text == "ASIAN") ? "A" : "N";
                }
                var objdata = GetphmCfgvalue("DB01", raceGroup).Where(x => x.mpd_min_num1 <= bmivalue && bmivalue <= x.mpd_max_num1).FirstOrDefault();
                if (objdata != null)
                {
                    //currentphm.tph_bmi_pt = Convert.ToInt32(objdata.mpd_num_value);
                    txt3sec1_BMIPoint.Text = objdata.mpd_num_value.ToString();
                }
                else
                {
                    //currentphm.tph_bmi_pt = 0;
                    txt3sec1_BMIPoint.Text = "0";
                }
                currentPHM.tph_bmi_pt = Utility.GetInteger(txt3sec1_BMIPoint.Text);
                //Race Group
                if (DDRaceGroup.Text == "ASIAN")
                {
                    mst_race_grpsmst_race_grps.Text = "ASIAN";
                }
                else if (DDRaceGroup.Text == "")
                {
                    mst_race_grpsmst_race_grps.Text = "";
                }
                else
                {
                    mst_race_grpsmst_race_grps.Text = "NonASIAN";
                }
                //Waist size
                txt3sec1_Waistsize.Text = txtWaistSize.Text;
                double waistvalue = Convert.ToDouble(txt3sec1_Waistsize.Text);
                var objwaistvalue = GetphmCfgvalue("DB02", raceGroup).Where(x => x.mpd_str_2 == Gender && x.mpd_min_num1 <= waistvalue && waistvalue <= x.mpd_max_num1).FirstOrDefault();
                if (objwaistvalue != null)
                {
                    txt3sec1_WaistByGenderPoint.Text = objwaistvalue.mpd_num_value.ToString();
                    //currentphm.tph_waist_pt = Convert.ToInt32(objwaistvalue.mpd_num_value);
                }
                else
                {
                    txt3sec1_WaistByGenderPoint.Text = "0";
                    //currentphm.tph_waist_pt = 0;
                }
                currentPHM.tph_waist_pt = Utility.GetInteger(txt3sec1_WaistByGenderPoint.Text);
                //Age & Exercise
                double Agevalue = Convert.ToDouble(txtAge.Text);
                txt3sec1_Age.Text = txtAge.Text;
                var objAgevalue = GetphmCfgvalue("DB04", raceGroup).Where(x => x.mpd_min_num1 <= Agevalue && Agevalue <= x.mpd_max_num1).FirstOrDefault();
                if (objAgevalue != null)
                {
                    txt3sec1_AgePoint.Text = objAgevalue.mpd_num_value.ToString();
                    //currentphm.tph_age_pt = Convert.ToInt32(objAgevalue.mpd_num_value);
                }
                else
                {
                    txt3sec1_AgePoint.Text = "0";
                    //currentphm.tph_age_pt = 0;
                }
                currentPHM.tph_age_pt = Utility.GetInteger(txt3sec1_AgePoint.Text);
                txt3sec1_Exercise.Text = (objqp.tqp_his_exercise == 'O' || objqp.tqp_his_exercise == 'N') ? strTrue : strFalse;
                string IsExercise = (objqp.tqp_his_exercise == 'O' || objqp.tqp_his_exercise == 'N') ? "Y" : "N";
                var objExercisevalue = GetphmCfgvalue("DB06", raceGroup).Where(x => x.mpd_str_2 == IsExercise && x.mpd_min_num1 <= Agevalue && Agevalue < x.mpd_max_num1).FirstOrDefault();
                if (objExercisevalue != null)
                {
                    txt3sec1_ExercisePoint.Text = objExercisevalue.mpd_num_value.ToString();
                    //currentphm.tph_exercise_pt = Convert.ToInt32(objExercisevalue.mpd_num_value);
                }
                else
                {
                    txt3sec1_ExercisePoint.Text = "0";
                    //currentphm.tph_exercise_pt = 0;
                }
                currentPHM.tph_exercise_pt = Utility.GetInteger(txt3sec1_ExercisePoint.Text);
                //Gender
                txt3sec1_Gender.Text = txtGender.Text;
                var objGendervalue = GetphmCfgvalue("DB03", raceGroup).Where(x => x.mpd_str_2 == Gender).FirstOrDefault();
                if (objGendervalue != null)
                {
                    txt3sec1_MaleGenderPoint.Text = objGendervalue.mpd_num_value.ToString();
                    //currentphm.tph_gender_pt = Convert.ToInt32(objGendervalue.mpd_num_value);
                }
                else
                {
                    //currentphm.tph_gender_pt = 0;
                    txt3sec1_MaleGenderPoint.Text = "0";
                }
                currentPHM.tph_gender_pt = Utility.GetInteger(txt3sec1_MaleGenderPoint.Text);
                //Over Weight
                txt3sec1_OverWeightInfant.Text = (objqp.tqp_fwm_over_weight == 'Y') ? strTrue : strFalse;
                string overweightvalue = (objqp.tqp_fwm_over_weight == 'Y') ? "Y" : "N";
                var objOverWeightvalue = GetphmCfgvalue("DB07", raceGroup).Where(x => x.mpd_str_2 == overweightvalue).FirstOrDefault();
                if (objOverWeightvalue != null)
                {
                    txt3sec1_OverWeightInfantPoint.Text = objOverWeightvalue.mpd_num_value.ToString();
                    //currentphm.tph_overweight_pt = Convert.ToInt32(objOverWeightvalue.mpd_num_value);
                }
                else
                {
                    txt3sec1_OverWeightInfantPoint.Text = "0";
                    //currentphm.tph_overweight_pt = 0;
                }
                currentPHM.tph_overweight_pt = Utility.GetInteger(txt3sec1_OverWeightInfantPoint.Text);
                //BP
                txt3sec1_systoricBP.Text = txt2sec1_SystolicBP.Text;
                int intsystorcBP = Convert.ToInt32(txt3sec1_systoricBP.Text);
                if (intsystorcBP >= 140)
                {
                    txt3sec1_HighBlood.Text = "2";
                }
                else
                {
                    txt3sec1_HighBlood.Text = "0";
                }
                //Family Diabetes
                string Familyvalue = ((objqp.tqp_fhis_fdis_diab == true)
                            || (objqp.tqp_fhis_mdis_diab == true)
                            || (objqp.tqp_fhis_bdis_diab == true)) ? "1" : "0";
                txt3sec1_FamilyDiabete.Text = (Familyvalue == "1") ? strTrue : strFalse;
                var objFamilyvalue = GetphmCfgvalue("DB05", raceGroup).Where(x => x.mpd_str_2 == ((Familyvalue == "1") ? "Y" : "N")).FirstOrDefault();
                if (objFamilyvalue != null)
                {
                    txt3sec1_FamilyHistoryPoint.Text = objFamilyvalue.mpd_num_value.ToString();
                    //currentphm.tph_fam_his_pt = Convert.ToInt32(objFamilyvalue.mpd_num_value);
                }
                else
                {
                    txt3sec1_FamilyHistoryPoint.Text = "0";
                    //currentphm.tph_fam_his_pt = 0;
                }
                currentPHM.tph_fam_his_pt = Utility.GetInteger(txt3sec1_FamilyHistoryPoint.Text);
                //Diabetes Total Point
                var TotalPoint = Convert.ToInt32(txt3sec1_BMIPoint.Text);
                TotalPoint += Convert.ToInt32(txt3sec1_WaistByGenderPoint.Text);
                TotalPoint += Convert.ToInt32(txt3sec1_MaleGenderPoint.Text);
                TotalPoint += Convert.ToInt32(txt3sec1_AgePoint.Text);
                TotalPoint += Convert.ToInt32(txt3sec1_FamilyHistoryPoint.Text);
                TotalPoint += Convert.ToInt32(txt3sec1_ExercisePoint.Text);
                TotalPoint += Convert.ToInt32(txt3sec1_OverWeightInfantPoint.Text);
                TotalPoint += Convert.ToInt32(txt3sec1_HighBlood.Text);//เพิ่ม HighBlood Text
                currentPHM.tph_diabetes_tot_pt = TotalPoint;
                txt3sec1_TotalPoint.Text = TotalPoint.ToString();
                //Reletive Risk
                var objTotalvalue = GetphmCfgvalue("DB08", raceGroup).Where(x => x.mpd_min_num1 <= TotalPoint && TotalPoint <= x.mpd_max_num1).FirstOrDefault();
                if (objTotalvalue != null)
                {
                    txt3sec1_RelativeRisk.Text = objTotalvalue.mpd_str_value1.ToString();
                    //currentphm.tph_relative_risk = objTotalvalue.mpd_str_value1.ToString();
                }
                else
                {
                    txt3sec1_RelativeRisk.Text = "";
                    //currentphm.tph_relative_risk = "";
                }
                currentPHM.tph_relative_risk = txt3sec1_RelativeRisk.Text;
                #endregion
                #region DiabetesSec2
                //FPG
                var objfpg = GetLabNo("C0180", "I0001", tpt_hn_no);
                int icount = 0;
                DateTime? vHbA1c_Curr;
                DateTime? vHbA1c_Prev;
                DateTime? vFPG_Curr;
                DateTime? vFPG_Prev;
                foreach (trn_patient_lab item in objfpg)
                {
                    if (icount < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == currentDatetime.Date)
                        {
                            txt3sec2_FPG.Text = item.tpl_lab_value.ToString();
                            txt3sec2_FPGDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_fpg = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_fpg_date = item.tpl_lab_date.Value;
                            vFPG_Curr = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != currentDatetime.Date)
                        {
                            txt3sec2_PreviousFPG.Text = item.tpl_lab_value.ToString();
                            txt3sec2_PreviousFPGDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_prev_fpg = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_prev_fpg_date = item.tpl_lab_date.Value;
                            vFPG_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 2;
                        }
                    }
                }
                var objlabfpg = GetPHMDiagnosis("DB09", txt3sec2_FPG.Text);
                txt3sec2_FPGdiagnosis.Text = objlabfpg;
                currentPHM.tph_fpg_diagnosis = txt3sec2_FPGdiagnosis.Text;
                //HbA1c
                var objhba = GetLabNo("N0510", "I0950", tpt_hn_no);
                icount = 0;
                foreach (trn_patient_lab item in objfpg)
                {
                    if (icount < 2)
                    {
                        if (Convert.ToDateTime(item.tpl_lab_date).Date == currentDatetime.Date)
                        {
                            txt3sec2_HBA1C.Text = item.tpl_lab_value.ToString();
                            txt3sec2_HbA1cDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_hba1c = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_hba1c_date = item.tpl_lab_date.Value;
                            vHbA1c_Curr = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 1;
                        }
                        else if (Convert.ToDateTime(item.tpl_lab_date).Date != currentDatetime.Date)
                        {
                            txt3sec2_PreHbA1c.Text = item.tpl_lab_value.ToString();
                            txt3sec2_PreHbA1cDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                            currentPHM.tph_prev_hba1c = Convert.ToDouble(item.tpl_lab_value);
                            currentPHM.tph_prev_hba1c_date = item.tpl_lab_date.Value;
                            vHbA1c_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                            icount = icount + 2;
                        }
                    }
                }
                var objlabHba1c = GetPHMDiagnosis("DB10", txt3sec2_HBA1C.Text);
                txt3sec2_HbA1cDiagnosis.Text = objlabHba1c;
                currentPHM.tph_hba1c_diagnosis = txt3sec2_HbA1cDiagnosis.Text;
                #endregion
                #region DiabetesSec3
                //Risk category
                int riskcategory = 0;
                //Compare Edit Sumit 17/01/2014
                double hba1cvalue = 0;
                double fpgvalue = 0;

                if (txt3sec2_HbA1cDate.Text != "" && txt3sec2_FPGDate.Text != "")
                {
                    hba1cvalue = Convert.ToDouble(txt3sec2_HBA1C.Text);
                    fpgvalue = 0;
                }
                else if (txt3sec2_HbA1cDate.Text == "" && txt3sec2_FPGDate.Text != "")
                {
                    hba1cvalue = 0;
                    fpgvalue = Convert.ToDouble(txt3sec2_FPG.Text);
                }
                else if (txt3sec2_HbA1cDate.Text != "" && txt3sec2_FPGDate.Text == "")
                {
                    hba1cvalue = Convert.ToDouble(txt3sec2_HBA1C.Text);
                    fpgvalue = 0;
                }
                else
                {
                    if (txt3sec2_PreHbA1c.Text != "" && txt3sec2_PreviousFPG.Text != "")
                    {
                        var objlabValue = (from t1 in dbc.trn_patient_labs
                                           where (t1.tpl_lab_no == "C0180" || t1.tpl_lab_no == "I0001" || t1.tpl_lab_no == "N0510" || t1.tpl_lab_no == "I0950")
                                            && t1.tpl_lab_date != currentDatetime.Date
                                           orderby t1.tpl_lab_date descending
                                           select t1).FirstOrDefault();
                        if (objlabValue.tpl_lab_no == "C0180" || objlabValue.tpl_lab_no == "I0001")
                        {
                            hba1cvalue = 0;
                            fpgvalue = Convert.ToDouble(txt3sec2_PreviousFPG.Text);
                        }
                        else
                        {
                            hba1cvalue = Convert.ToDouble(txt3sec2_PreHbA1c.Text);
                            fpgvalue = 0;
                        }
                    }
                    else if (txt3sec2_PreHbA1c.Text != "" && txt3sec2_PreviousFPG.Text == "")
                    {
                        hba1cvalue = Convert.ToDouble(txt3sec2_PreHbA1c.Text);
                        fpgvalue = 0;
                    }
                    else if (txt3sec2_PreHbA1c.Text == "" && txt3sec2_PreviousFPG.Text != "")
                    {
                        hba1cvalue = 0;
                        fpgvalue = Convert.ToDouble(txt3sec2_PreviousFPG.Text);
                    }
                    else
                    {
                        hba1cvalue = 0;
                        fpgvalue = 0;
                    }
                }
                string isAsian = (DDRaceGroup.Text == "ASIAN") ? "A" : "N";
                string typerisk = "";
                var objcfgphm = (from t1 in dbc.mst_phm_cfg_dtls
                                 where t1.mph_id == 14
                                 && t1.mpd_str_1 == isAsian
                                 && hba1cvalue >= t1.mpd_min_num1
                                 && hba1cvalue <= t1.mpd_max_num1
                                 select t1).FirstOrDefault();
                if (objcfgphm != null)
                {
                    typerisk = objcfgphm.mpd_str_value2;
                }
                //if (hba1cvalue !=0 && hba1cvalue <= 6.4 && hba1cvalue >= 5.7)
                if (hba1cvalue != 0 && hba1cvalue >= 6.5)
                {
                    riskcategory = 1;
                }
                //else if (hba1cvalue != 0 &&  hba1cvalue >= 6.5)
                else if (hba1cvalue != 0 && hba1cvalue <= 6.4 && hba1cvalue >= 5.7)
                {
                    riskcategory = 2;
                }
                else if (hba1cvalue != 0 && typerisk == "H" && hba1cvalue < 5.7)
                {
                    riskcategory = 3;
                }
                else if (hba1cvalue != 0 && typerisk == "L" && hba1cvalue < 5.7)
                {
                    riskcategory = 4;
                }
                else if (fpgvalue != 0 && fpgvalue < 100)
                {
                    riskcategory = 5;
                }
                else if (fpgvalue != 0 && fpgvalue >= 100)
                {
                    riskcategory = 6;
                }
                var objdatatypeD = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'D').FirstOrDefault();
                if (objdatatypeD != null)
                {
                    txt3sec3_RiskCategory.Text = objdatatypeD.tpd_category;

                    txt3sec3_Recommend.Text = objdatatypeD.tpd_clinic_recommend;

                    txt3sec3_ReferToClinic.Text = objdatatypeD.tpd_refer_to_clinic;
                    txt3sec3_Protocol.Text = objdatatypeD.tpd_protocal;
                    txt3sec3_ReasonFornotRefer.SelectedValue = objdatatypeD.tpd_status;
                    txt3sec3_ReasonfornotReferRemark.Text = objdatatypeD.tpd_status_other;
                    txt3sec3_Recommendation.Text = objdatatypeD.tpd_recomment;
                    txt3sec3_ConcernPointsForTLC.Text = objdatatypeD.tpd_concern;
                    txt3sec3_txtFollowupPoint.Text = objdatatypeD.tpd_note;
                }
                else
                {
                    var objcfgvalue = (from t1 in dbc.mst_phm_cfg_dtls
                                       where t1.mst_phm_cfg_hdr.mph_code == "DB12"
                                       && t1.mpd_num_1 == riskcategory
                                       select t1).FirstOrDefault();
                    if (objcfgvalue != null)
                    {
                        txt3sec3_RiskCategory.Text = objcfgvalue.mpd_str_value1;
                        txt3sec3_Recommend.Text = objcfgvalue.mpd_str_value2;
                        txt3sec3_Protocol.Text = objcfgvalue.mpd_str_value3;
                        txt3sec3_Recommendation.Text = objcfgvalue.mpd_str_value4;
                    }
                }
                //LDL Cholesteral
                txt3sec3_LDLCholesterol.Text = txt2sec3_LDL.Text;
                txt3sec3_LDLChDate.Text = txt2sec3_LDLDate.Text;
                txt3sec3_PreviousLDLCholesterol.Text = txt2sec3_PreLDL.Text;
                txt3sec3_PrevoiusLDLChoDate.Text = txt2sec3_PreLDLDate.Text;
                //var objlab = Getlab("C0159", tpt_hn_no);
                //icount = 0;
                //foreach (trn_patient_lab item in objlab)
                //{
                //    if (icount < 2)
                //    {
                //        if (Convert.ToDateTime(item.tpl_lab_date).Date == Program.GetServerDateTime().Date)
                //        {
                //            txt3sec3_LDLCholesterol.Text = item.tpl_lab_value.ToString();
                //            txt3sec3_LDLChDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                //            icount = icount + 1;
                //        }
                //        else if (Convert.ToDateTime(item.tpl_lab_date).Date != Program.GetServerDateTime().Date)
                //        {
                //            txt3sec3_PreviousLDLCholesterol.Text = item.tpl_lab_value.ToString();
                //            txt3sec3_PrevoiusLDLChoDate.Text = item.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                //            vHbA1c_Prev = Convert.ToDateTime(item.tpl_lab_date.Value.ToShortDateString());
                //            icount = icount + 2;
                //        }
                //    }
                //}
                #endregion
                #region DiabetesSec4
                txt3sec4_RaceGroup.Text = mst_race_grpsmst_race_grps.Text;
                txt3sec4_BMI.Text = (Convert.ToInt32(txt3sec1_BMIPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_BMIRisk.Text = (txt3sec4_BMI.Text == strTrue) ? strTrue : strFalse;
                txt3sec4_Waist.Text = (Convert.ToInt32(txt3sec1_WaistByGenderPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_WaistRisk.Text = (txt3sec4_Waist.Text == strTrue) ? strTrue : strFalse;
                txt3sec4_Exercise.Text = (Convert.ToInt32(txt3sec1_ExercisePoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_ExerciseRisk.Text = (txt3sec4_Exercise.Text == strTrue) ? strTrue : strFalse;
                txt3sec4_Gender.Text = (Convert.ToInt32(txt3sec1_MaleGenderPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_GenderRisk.Text = (txt3sec4_Gender.Text == strTrue) ? strTrue : strFalse;
                txt3sec4_Age.Text = (Convert.ToInt32(txt3sec1_AgePoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_AgeRisk.Text = (txt3sec4_Age.Text == strTrue) ? strTrue : strFalse;
                txt3sec4_FamilyHistory.Text = (Convert.ToInt32(txt3sec1_FamilyHistoryPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_FamilyHistoryRisk.Text = (txt3sec4_FamilyHistory.Text == strTrue) ? strTrue : strFalse;
                txt3sec4_Womanweight.Text = (Convert.ToInt32(txt3sec1_OverWeightInfantPoint.Text) > 0) ? strTrue : strFalse;
                txt3sec4_WomanweightRisk.Text = (txt3sec4_Womanweight.Text == strTrue) ? strTrue : strFalse;
                txt3sec4_FatherDiabetes.Text = (objqp.tqp_fhis_fdis_diab == true || objqp.tqp_fhis_mdis_diab == true) ? strTrue : strFalse;
                txt3sec4_SiblingDiaetes.Text = (objqp.tqp_fhis_bdis_diab == true) ? strTrue : strFalse;

                button6_Click(null, null);
                //for save data
                currentPHM.tph_rk_bmi = (txt3sec4_BMIRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_waist = (txt3sec4_WaistRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_exercise = (txt3sec4_ExerciseRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_diab_age = (txt3sec4_AgeRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_gender = (txt3sec4_GenderRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_diab_family = (txt3sec4_FamilyHistoryRisk.Text == strTrue) ? true : false;
                currentPHM.tph_rk_infant_more = (txt3sec4_WomanweightRisk.Text == strTrue) ? true : false;

                //txt3sec4_BMI.Text = (currentphm.tph_bmi_pt > 0) ? strTrue : strFalse;
                //txt3sec4_BMIRisk.Text = txt3sec4_BMI.Text;
                //txt3sec4_Waist.Text = (currentphm.tph_waist_pt > 0) ? strTrue : strFalse;
                //txt3sec4_WaistRisk.Text = txt3sec4_Waist.Text;
                //txt3sec4_Exercise.Text = (currentphm.tph_exercise_pt > 0) ? strTrue : strFalse;
                //txt3sec4_ExerciseRisk.Text = txt3sec4_Exercise.Text;
                //txt3sec4_Gender.Text = (currentphm.tph_gender_pt > 0) ? strTrue : strFalse;
                //txt3sec4_GenderRisk.Text = txt3sec4_Gender.Text;
                //txt3sec4_Age.Text = (currentphm.tph_age_pt > 0) ? strTrue : strFalse;
                //txt3sec4_AgeRisk.Text = txt3sec4_Age.Text;
                //txt3sec4_FamilyHistory.Text = (currentphm.tph_fam_his_pt > 0) ? strTrue : strFalse;
                //txt3sec4_FamilyHistoryRisk.Text = txt3sec4_FamilyHistory.Text;
                //txt3sec4_Womanweight.Text = (currentphm.tph_overweight_pt > 0) ? strTrue : strFalse;
                //txt3sec4_WomanweightRisk.Text = txt3sec4_Womanweight.Text;
                //txt3sec4_HbA1C.Text =Convert.ToDouble( currentphm.tph_hba1c).ToString();
                //txt3sec4_FPG.Text = Convert.ToDouble(currentphm.tph_fpg).ToString();
                //txt3sec4_Diabetes.Text = (objqp.tqp_ill_med_diab == true) ? strTrue : strFalse;
                #endregion
            }
        }
        private void ClearDiabeteData()
        {
            trn_phm_hdr objPHM = bsPHM.OfType<trn_phm_hdr>().FirstOrDefault();
            //Sec1
            txt3sec1_BMIResule.Text = null;
            objPHM.tph_bmi_pt = null;
            mst_race_grpsmst_race_grps.Text = null;
            txt3sec1_Waistsize.Text = null;
            objPHM.tph_waist_pt = null;
            txt3sec1_Age.Text = null;
            objPHM.tph_age_pt = null;
            txt3sec1_Exercise.Text = null;
            objPHM.tph_exercise_pt = null;
            txt3sec1_Gender.Text = null;
            objPHM.tph_gender_pt = null;
            txt3sec1_OverWeightInfant.Text = null;
            objPHM.tph_overweight_pt = null;
            txt3sec1_systoricBP.Text = null;
            txt3sec1_HighBlood.Text = null;
            txt3sec1_FamilyDiabete.Text = null;
            objPHM.tph_fam_his_pt = null;
            objPHM.tph_risk_score = null;
            objPHM.tph_relative_risk = null;
            //Sec2
            objPHM.tph_fpg = null;
            objPHM.tph_fpg_date = null;
            objPHM.tph_prev_fpg = null;
            objPHM.tph_prev_fpg_date = null;
            objPHM.tph_fpg_diagnosis = null;
            objPHM.tph_hba1c = null;
            objPHM.tph_hba1c_date = null;
            objPHM.tph_prev_hba1c = null;
            objPHM.tph_prev_hba1c_date = null;
            objPHM.tph_hba1c_diagnosis = null;
            //Sec3
            txt3sec3_RiskCategory.Text = null;
            txt3sec3_Recommend.Text = null;
            txt3sec3_ReferToClinic.SelectedIndex = 0;
            txt3sec3_Protocol.SelectedIndex = 0;
            txt3sec3_ReasonFornotRefer.SelectedIndex = 0;
            txt3sec3_ReasonfornotReferRemark.Text = null;
            txt3sec3_Recommendation.Text = null;
            txt3sec3_ConcernPointsForTLC.Text = null;
            txt3sec3_txtFollowupPoint.Text = null;
            txt3sec3_LDLCholesterol.Text = null;
            txt3sec3_LDLChDate.Text = null;
            txt3sec3_PreviousLDLCholesterol.Text = null;
            txt3sec3_PrevoiusLDLChoDate.Text = null;
            //Sec4
            txt3sec4_RaceGroup.Text = null;
            txt3sec4_BMI.Text = null;
            objPHM.tph_rk_bmi = null;
            txt3sec4_Waist.Text = null;
            objPHM.tph_rk_waist = null;
            txt3sec4_Exercise.Text = null;
            objPHM.tph_rk_exercise = null;
            txt3sec4_Gender.Text = null;
            objPHM.tph_rk_gender = null;
            txt3sec4_Age.Text = null;
            objPHM.tph_rk_diab_age = null;
            txt3sec4_FamilyHistory.Text = null;
            objPHM.tph_rk_diab_family = null;
            txt3sec4_Womanweight.Text = null;
            objPHM.tph_rk_infant_more = null;
            txt3sec4_FatherDiabetes.Text = null;
            txt3sec4_SiblingDiaetes.Text = null;
        }

        private void LoadMammogram(ref trn_patient_regi objPatientRegis)
        {
            int tpr_id = objPatientRegis.tpr_id;
            trn_patient objPatient = objPatientRegis.trn_patient;
            trn_phm_hdr currentPHM = objPatientRegis.trn_phm_hdrs.FirstOrDefault();
            if (currentPHM != null)
            {
                //Diabetes Save PHM dtl Type="M"
                var objdatatypeM = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'M').FirstOrDefault();
                if (objdatatypeM != null)
                {
                    txt4sec1_BiradsCategory.Text = objdatatypeM.tpd_category;
                    txt4sec1_Protocal.Text = objdatatypeM.tpd_protocal;
                    txt4sec1_RefertoClinic.Text = objdatatypeM.tpd_refer_to_clinic;
                    txt4sec1_StatusReferClinic.SelectedValue = objdatatypeM.tpd_status.ToString();
                    txt4sec1_ReasonfornotReferRemark.Text = objdatatypeM.tpd_status_other;
                    txt4sec1_Diagnosis.Text = objdatatypeM.tpd_diagnosis;
                    txt4sec1_Remark.Text = objdatatypeM.tpd_note;

                    if (objdatatypeM.tpd_category != "")
                    {
                        int idcategory = Convert.ToInt32(objdatatypeM.tpd_category);
                        if (idcategory < 3)
                        {
                            txt4sec1_Recommend.Text = "NONE";
                            txt4sec1_Protocal.Text = "Care plan";
                        }
                        else if (idcategory < 7)
                        {
                            txt4sec1_Recommend.Text = "Breast Clinic";
                        }
                        if (idcategory > 3)
                        {
                            txt4sec1_Protocal.Text = "Consult to";
                        }
                    }

                    //Diagnosis && Remark                
                    double categoryid;
                    if (objdatatypeM.tpd_category == "")
                    {
                        categoryid = Convert.ToDouble(0);
                    }
                    else
                    {
                        categoryid = Convert.ToInt32(objdatatypeM.tpd_category);
                    }
                    string strNation = "T";
                    if (objPatient != null && objPatient.tpt_nation_code != null)
                    {
                        strNation = (objPatient.tpt_nation_code == "TH") ? "T" : "E";
                    }
                    else
                    {
                        strNation = "E";
                    }

                    if (txt4sec1_Diagnosis.Text == "" || txt4sec1_Remark.Text == "")
                    {
                        var objhpmcfghdr = (from t1 in dbc.mst_phm_cfg_dtls
                                            where t1.mst_phm_cfg_hdr.mph_code == "DM01"
                                            && t1.mpd_str_1 == strNation
                                            && t1.mpd_num_1 == categoryid
                                            select t1).FirstOrDefault();
                        if (objhpmcfghdr != null)
                        {
                            txt4sec1_Diagnosis.Text = objhpmcfghdr.mpd_str_value1;
                            txt4sec1_Remark.Text = objhpmcfghdr.mpd_str_value3;
                        }
                    }
                }
            }
        }
        private void ClearMammoData()
        {
            txt4sec1_BiradsCategory.Text = null;
            txt4sec1_Protocal.Text = null;
            txt4sec1_RefertoClinic.Text = null;
            txt4sec1_StatusReferClinic.SelectedIndex = 0;
            txt4sec1_ReasonfornotReferRemark.Text = null;
            txt4sec1_Diagnosis.Text = null;
            txt4sec1_Remark.Text = null;
            txt4sec1_Recommend.Text = null;            
        }

        private void LoadTumorMarker(ref trn_patient_regi objPatientRegis)
        {
            string tpt_hn_no = objPatientRegis.trn_patient.tpt_hn_no;
            trn_phm_hdr currentPHM = objPatientRegis.trn_phm_hdrs.FirstOrDefault();
            if (currentPHM != null)
            {
                #region AFP
                var objdatatypeA = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'A').FirstOrDefault();
                if (objdatatypeA != null)
                {

                    txt5sec1_tumorMarkevafp.Text = objdatatypeA.tpd_category;
                    txt5sec1_Recommend.Text = objdatatypeA.tpd_clinic_recommend;
                    txt5sec1_RefertoClinic.Text = objdatatypeA.tpd_refer_to_clinic;
                    txt5sec1_Protocal.Text = objdatatypeA.tpd_protocal;
                    txt5sec1_StatusRefertoClinic.SelectedValue = objdatatypeA.tpd_status;
                    txt5sec1_ReasonfornotReferRemark.Text = objdatatypeA.tpd_status_other;
                    txt5sec1_Remark.Text = objdatatypeA.tpd_note;
                }
                else
                {
                    var objAFP = (from t1 in dbc.trn_patient_labs
                                  where (t1.tpl_lab_no == "N0380" || t1.tpl_lab_no == "N7006")
                                  && t1.tpl_hn_no == tpt_hn_no
                                  orderby t1.tpl_lab_date descending
                                  select new { t1.tpl_lab_value, t1.tpl_lab_date }).FirstOrDefault();
                    if (objAFP != null)
                    {
                        txt5sec1_tumorMarkevafp.Text = objAFP.tpl_lab_value;
                        //txt5sec1_tumorMarkevafpDate.Text = Convert.ToDateTime(objAFP.tpl_lab_date).ToShortDateString();
                        txt5sec1_tumorMarkevafpDate.Text = objAFP.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        if (Convert.ToInt32(txt5sec1_tumorMarkevafp.Text) >= 5.4)
                        {
                            txt5sec1_Protocal.Text = "ส่งพบผู้เชี่ยวชาญด้านมะเร็ง";
                        }
                        else
                        {
                            txt5sec1_Protocal.Text = "ให้คำแนะนำทั่วไป";
                        }
                    }
                }
                #endregion
                #region CEA
                var objdatatypeE = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'E').FirstOrDefault();
                if (objdatatypeE != null)
                {
                    txt5sec2_tumormarkevCFA.Text = objdatatypeE.tpd_category;
                    txt5sec2_Recommend.Text = objdatatypeE.tpd_clinic_recommend;
                    txt5sec2_ReferToClinic.Text = objdatatypeE.tpd_refer_to_clinic;
                    txt5sec2_Protocal.Text = objdatatypeE.tpd_protocal;
                    txt5sec2_StatusRefertoClinic.SelectedValue = objdatatypeE.tpd_status;
                    txt5sec2_ReasonfornotReferRemark.Text = objdatatypeE.tpd_status_other;
                    txt5sec2_Remark.Text = objdatatypeE.tpd_note;
                }
                else
                {
                    var objCEA = (from t1 in dbc.trn_patient_labs
                                  where (t1.tpl_lab_no == "N0390" || t1.tpl_lab_no == "N7007")
                                  && t1.tpl_hn_no == tpt_hn_no
                                  orderby t1.tpl_lab_date descending
                                  select new { t1.tpl_lab_value, t1.tpl_lab_date }).FirstOrDefault();
                    if (objCEA != null)
                    {
                        txt5sec2_tumormarkevCFA.Text = objCEA.tpl_lab_value;
                        //txt5sec2_tumormarkevCFADate.Text = Convert.ToDateTime(objCEA.tpl_lab_date).ToShortDateString();
                        txt5sec2_tumormarkevCFADate.Text = objCEA.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        if (Convert.ToInt32(txt5sec2_tumormarkevCFA.Text) >= 2.5)
                        {
                            txt5sec2_Protocal.Text = "ส่งพบผู้เชี่ยวชาญด้านมะเร็ง";
                        }
                        else
                        {
                            txt5sec2_Protocal.Text = "ให้คำแนะนำทั่วไป";
                        }
                    }
                }
                #endregion
                #region PSA
                //PSA Type="P"
                var objdatatypeP = currentPHM.trn_phm_dtls.Where(x => x.tpd_type == 'P').FirstOrDefault();
                if (objdatatypeP != null)
                {
                    txt5sec3_TumorMarkevPSA.Text = objdatatypeP.tpd_category;
                    txt5sec3_Recommend.Text = objdatatypeP.tpd_clinic_recommend;
                    txt5sec3_ReferToClinic.Text = objdatatypeP.tpd_refer_to_clinic;
                    txt5sec3_Protocal.Text = objdatatypeP.tpd_protocal;
                    txt5sec3_StatusRefertoClinic.SelectedValue = objdatatypeP.tpd_status;
                    txt5sec3_ReasonfornotReferRemark.Text = objdatatypeP.tpd_status_other;
                    txt5sec3_Remark.Text = objdatatypeP.tpd_note;
                }
                else
                {
                    var objPSA = (from t1 in dbc.trn_patient_labs
                                  where (t1.tpl_lab_no == "N0050")
                                  && t1.tpl_hn_no == tpt_hn_no
                                  orderby t1.tpl_lab_date descending
                                  select new { t1.tpl_lab_value, t1.tpl_lab_date }).FirstOrDefault();
                    if (objPSA != null)
                    {
                        txt5sec3_TumorMarkevPSA.Text = objPSA.tpl_lab_value;
                        //txt5sec3_TumorMarkevPSADate.Text = Convert.ToDateTime(objPSA.tpl_lab_date).ToShortDateString();
                        txt5sec3_TumorMarkevPSADate.Text = objPSA.tpl_lab_date.Value.ToString("dd/MM/yyyy");
                        if (Convert.ToInt32(txt5sec3_TumorMarkevPSA.Text) >= 4)
                        {
                            txt5sec3_Protocal.Text = "ส่งพบผู้เชี่ยวชาญด้านมะเร็ง";
                        }
                        else
                        {
                            txt5sec3_Protocal.Text = "ให้คำแนะนำทั่วไป";
                        }
                    }
                }
                #endregion
            }
        }
        private void ClearTumorData()
        { 
            //Sec1
            txt5sec1_tumorMarkevafp.Text = null;
            txt5sec1_Recommend.Text = null;
            txt5sec1_RefertoClinic.SelectedIndex = 0;
            txt5sec1_Protocal.Text = null;
            txt5sec1_StatusRefertoClinic.SelectedIndex = 0;
            txt5sec1_ReasonfornotReferRemark.Text = null;
            txt5sec1_Remark.Text = null;
            txt5sec1_tumorMarkevafpDate.Text = null;
            //Sec2
            txt5sec2_tumormarkevCFA.Text = null;
            txt5sec2_Recommend.Text = null;
            txt5sec2_ReferToClinic.SelectedIndex = 0;
            txt5sec2_Protocal.Text = null;
            txt5sec2_StatusRefertoClinic.SelectedIndex = 0;
            txt5sec2_ReasonfornotReferRemark.Text = null;
            txt5sec2_Remark.Text = null;
            txt5sec2_tumormarkevCFADate.Text = null;
            //Sec3
            txt5sec3_TumorMarkevPSA.Text = null;
            txt5sec3_Recommend.Text = null;
            txt5sec3_ReferToClinic.SelectedIndex = 0;
            txt5sec3_Protocal.Text = null;
            txt5sec3_StatusRefertoClinic.SelectedIndex = 0;
            txt5sec3_ReasonfornotReferRemark.Text = null;
            txt5sec3_Remark.Text = null;
            txt5sec3_TumorMarkevPSADate.Text = null;
        }

        private void LoadRiskSummary()
        {
            #region Cardiovascular
            txt6sec1_RiskCategory.Text = txt2sec3_RiskCategory.Text;
            txt6sec1_Recommend.Text = txt2sec3_Recommend.Text;
            txt6sec1_RefertoClinic.Text = CBsec3_RefertoClinic.Text;
            txt6sec1_Protocol.Text = txt2sec3_Protocol.Text;
            txt6sec1_StatusRefertoClinic.Text = txt2sec3_StatusRefertoClinic.Text;
            txt6sec1_ReasonfornotReferRemark.Text = txt2sec3_ReasonfornotReferRemark.Text;
            txt6sec1_Recommendation.Text = txt2sec3_Recommendation.Text;
            txt6sec1_ConcernPointForTLC.Text = txt2sec3_ConcernPointForTLC.Text;
            txt6sec1_Note.Text = txt2sec3_Note.Text;
            #endregion
            #region Diabetes
            txt6sec2_RiskCategory.Text = txt3sec3_RiskCategory.Text;
            txt6sec2_Recommend.Text = txt3sec3_Recommend.Text;
            txt6sec2_ReferToClinic.Text = txt3sec3_ReferToClinic.Text;
            txt6sec2_Protocol.Text = txt3sec3_Protocol.Text;
            txt6sec2_ReasonFornotRefer.Text = txt3sec3_ReasonFornotRefer.Text;
            txt6sec2_ReasonfornotReferRemark.Text = txt3sec3_ReasonfornotReferRemark.Text;
            txt6sec2_Recommendation.Text = txt3sec3_Recommendation.Text;
            txt6sec2_ConcernPointsForTLC.Text = txt3sec3_ConcernPointsForTLC.Text;
            txt6sec2_txtFollowupPoint.Text = txt3sec3_txtFollowupPoint.Text;
            #endregion
            #region Mammogram
            txt6sec3_BiradsCategory.Text = txt4sec1_BiradsCategory.Text;
            txt6sec3_Diagnosis.Text = txt4sec1_Diagnosis.Text;
            txt6sec3_Recommend.Text = txt4sec1_Recommend.Text;
            txt6sec3_RefertoClinic.Text = txt4sec1_RefertoClinic.Text;
            txt6sec3_Protocal.Text = txt4sec1_Protocal.Text;
            txt6sec3_StatusReferClinic.Text = txt4sec1_StatusReferClinic.Text;
            txt6sec3_ReasonfornotReferRemark.Text = txt4sec1_ReasonfornotReferRemark.Text;
            txt6sec3_Remark.Text = txt4sec1_Remark.Text;
            #endregion
            #region TumorSec1
            txt6sec4_tumorMarkevafp.Text = txt5sec1_tumorMarkevafp.Text;
            txt6sec4_Recommend.Text = txt5sec1_Recommend.Text;
            txt6sec4_RefertoClinic.Text = txt5sec1_RefertoClinic.Text;
            txt6sec4_Protocal.Text = txt5sec1_Protocal.Text;
            txt6sec4_StatusRefertoClinic.Text = txt5sec1_StatusRefertoClinic.Text;
            txt6sec4_ReasonfornotReferRemark.Text = txt5sec1_ReasonfornotReferRemark.Text;
            txt6sec4_Remark.Text = txt5sec1_Remark.Text;
            #endregion
            #region TumorSec2
            txt6sec5_tumormarkevCFA.Text = txt5sec2_tumormarkevCFA.Text;
            txt6sec5_Recommend.Text = txt5sec2_Recommend.Text;
            txt6sec5_ReferToClinic.Text = txt5sec2_ReferToClinic.Text;
            txt6sec5_Protocal.Text = txt5sec2_Protocal.Text;
            txt6sec5_StatusRefertoClinic.Text = txt5sec2_StatusRefertoClinic.Text;
            txt6sec5_ReasonfornotReferRemark.Text = txt5sec2_ReasonfornotReferRemark.Text;
            txt6sec5_Remark.Text = txt5sec2_Remark.Text;
            #endregion
            #region TumorSec3
            txt6sec6_TumorMarkevPSA.Text = txt5sec3_TumorMarkevPSA.Text;
            txt6sec6_Recommend.Text = txt5sec3_Recommend.Text;
            txt6sec6_ReferToClinic.Text = txt5sec3_ReferToClinic.Text;
            txt6sec6_Protocal.Text = txt5sec3_Protocal.Text;
            txt6sec6_StatusRefertoClinic.Text = txt5sec3_StatusRefertoClinic.Text;
            txt6sec6_ReasonfornotReferRemark.Text = txt5sec3_ReasonfornotReferRemark.Text;
            txt6sec6_Remark.Text = txt5sec3_Remark.Text;
            #endregion
        }
        private void ClearRiskData()
        {
            txt6sec1_RiskCategory.Text = null;
            txt6sec1_Recommend.Text = null;
            txt6sec1_RefertoClinic.SelectedIndex = 0;
            txt6sec1_Protocol.SelectedIndex = 0;
            txt6sec1_StatusRefertoClinic.SelectedIndex = 0;
            txt6sec1_ReasonfornotReferRemark.Text = null;
            txt6sec1_Recommendation.Text = null;
            txt6sec1_ConcernPointForTLC.Text = null;
            txt6sec1_Note.Text = null;

            txt6sec2_RiskCategory.Text = null;
            txt6sec2_Recommend.Text = null;
            txt6sec2_ReferToClinic.SelectedIndex = 0;
            txt6sec2_Protocol.SelectedIndex = 0;
            txt6sec2_ReasonFornotRefer.SelectedIndex = 0;
            txt6sec2_ReasonfornotReferRemark.Text = null;
            txt6sec2_Recommendation.Text = null;
            txt6sec2_ConcernPointsForTLC.Text = null;
            txt6sec2_txtFollowupPoint.Text = null;

            txt6sec3_BiradsCategory.Text = null;
            txt6sec3_Diagnosis.Text = null;
            txt6sec3_Recommend.Text = null;
            txt6sec3_RefertoClinic.SelectedIndex = 0;
            txt6sec3_Protocal.Text = null;
            txt6sec3_StatusReferClinic.SelectedIndex = 0;
            txt6sec3_ReasonfornotReferRemark.Text = null;
            txt6sec3_Remark.Text = null;

            txt6sec4_tumorMarkevafp.Text = null;
            txt6sec4_Recommend.Text = null;
            txt6sec4_RefertoClinic.SelectedIndex = 0;
            txt6sec4_Protocal.Text = null;
            txt6sec4_StatusRefertoClinic.SelectedIndex = 0;
            txt6sec4_ReasonfornotReferRemark.Text = null;
            txt6sec4_Remark.Text = null;

            txt6sec5_tumormarkevCFA.Text = null;
            txt6sec5_Recommend.Text = null;
            txt6sec5_ReferToClinic.SelectedIndex = 0;
            txt6sec5_Protocal.Text = null;
            txt6sec5_StatusRefertoClinic.SelectedIndex = 0;
            txt6sec5_ReasonfornotReferRemark.Text = null;
            txt6sec5_Remark.Text = null;

            txt6sec6_TumorMarkevPSA.Text = null;
            txt6sec6_Recommend.Text = null;
            txt6sec6_ReferToClinic.SelectedIndex = 0;
            txt6sec6_Protocal.Text = null;
            txt6sec6_StatusRefertoClinic.SelectedIndex = 0;
            txt6sec6_ReasonfornotReferRemark.Text = null;
            txt6sec6_Remark.Text = null;
        }

        private void ChangeForeColor(TextBox txt)
        {
            ////Added.Akkaradech on 2014-01-21
            try
            {
                if (Convert.ToInt32(txt.Text) > 0 && txt.Text != "")
                    txt.ForeColor = Color.Red;
                else
                    txt.ForeColor = Color.Black;
            }
            catch
            {
                return;
            }
        }
        private List<trn_patient_lab> Getlab(string Labno, string HNno)
        {
            List<trn_patient_lab> objitem = (from t1 in dbc.trn_patient_labs
                                             where t1.tpl_lab_no == Labno
                                              && t1.tpl_hn_no == HNno
                                             orderby t1.tpl_lab_date descending
                                             select t1).ToList();
            //&& ((Program.GetServerDateTime().Date - t1.tpl_lab_date.Value.Date).Days <= 365)

            return objitem;
        }
        private List<mst_phm_cfg_dtl> GetphmCfgvalue(string mphCode, string RaceGroup)
        {
            List<mst_phm_cfg_dtl> objvalue = (from t1 in dbc.mst_phm_cfg_dtls
                                              where t1.mst_phm_cfg_hdr.mph_code == mphCode
                                                    && t1.mpd_str_1 == RaceGroup
                                              select t1).ToList();
            if (objvalue != null)
            {
                return objvalue;
            }
            else
            {
                return null;
            }
        }
        private List<trn_patient_lab> GetLabNo(string Labno1, string Labno2, string HNno)
        {
            List<trn_patient_lab> objitem = (from t1 in dbc.trn_patient_labs
                                             where (t1.tpl_lab_no == Labno1 || t1.tpl_lab_no == Labno2)
                                                     && t1.tpl_hn_no == HNno
                                             orderby t1.tpl_lab_date descending
                                             select t1).Take(2).ToList();
            return objitem;
        }
        private List<trn_patient_lab> Getlab(string Labno1, string Labno2, string HNno)
        {
            List<trn_patient_lab> objitem = (from t1 in dbc.trn_patient_labs
                                             where (t1.tpl_lab_no == Labno1 || t1.tpl_lab_no == Labno2)
                                              && t1.tpl_hn_no == HNno
                                             orderby t1.tpl_lab_date descending
                                             select t1).ToList();
            //&& ((Program.GetServerDateTime().Date - t1.tpl_lab_date.Value.Date).Days <= 365)
            return objitem;
        }
        private string GetPHMDiagnosis(string Code, string LabValue)
        {
            if (LabValue == "") { return ""; }
            float valuenum = Utility.ToFloat(LabValue);
            var objdata = (from t1 in dbc.mst_phm_cfg_dtls
                           where t1.mst_phm_cfg_hdr.mph_code == Code
                           && t1.mpd_min_num1 <= valuenum
                           && t1.mpd_max_num1 >= valuenum
                           select t1).FirstOrDefault();
            if (objdata != null)
                return objdata.mpd_str_value1;
            else
                return "";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            ChangeFontColor(plPersonalHealthManagment);
            ChangeFontColor(groupBox3);
            ChangeFontColor(groupBox4);
            ChangeFontColor(groupBox5);
            ChangeFontColor(groupBox6);
            ChangeFontColor(groupBox11);
            ChangeFontColor(groupBox12);
            ChangeFontColor(panel6);
            ChangeFontColor(panel7);
            ChangeFontColor(panel11);
            ChangeFontColor(panel12);
            ChangeFontColor(panel13);
            ChangeFontColor(plDiabetes);
            ChangeFontColor(groupBox11);
            ChangeFontColor(groupBox12);
            ChangeForeColor(txt2sec1_SmokePoint);
            ChangeForeColor(txt2sec1_HighBloodPoint);
            ChangeForeColor(txt2sec1_familyhistoryPoint);
            ChangeForeColor(txt2sec1_AgePoint);
            ChangeForeColor(txt2sec1_HDLPoint);
            ChangeForeColor(txt2sec1_PreCardioCount);
            if (Convert.ToInt32(txt3sec1_FamilyHistoryPoint.Text) > 0)
                txt3sec1_FamilyHistoryPoint.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_TotalPoint.Text) > 0)
                ChangeForeColor(txt3sec1_TotalPoint);
            if (Convert.ToInt32(txt3sec1_ExercisePoint.Text) > 0)
                txt3sec1_ExercisePoint.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_BMIPoint.Text) > 0)
                txt3sec1_BMIPoint.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_OverWeightInfantPoint.Text) > 0)
                txt3sec1_OverWeightInfantPoint.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_HighBlood.Text) > 0)
                txt3sec1_HighBlood.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_systoricBP.Text) > 0)
                txt3sec1_systoricBP.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_OverWeightInfant.Text) > 0)
                txt3sec1_OverWeightInfant.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_AgePoint.Text) > 0)
                txt3sec1_AgePoint.ForeColor = Color.Red;
            if (Convert.ToInt32(txt3sec1_MaleGenderPoint.Text) > 0)
                txt3sec1_MaleGenderPoint.ForeColor = Color.Red;
        }
        private void ChangeFontColor(Control control)
        {
            ////Change font color when it display 'Yes'
            foreach (Control ctl in control.Controls)
            {
                TextBox txt;
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.ForeColor = Color.Empty;
                    if (txt.Text == "ใช่(Yes)")
                    {
                        txt.ForeColor = Color.Red;
                    }
                    if (txt.Text == "ไม่ใช่(No)")
                    {
                        txt.ForeColor = Color.Black;
                    }
                }
            }
        }
        private void SetDDReasonforNotRefer(ComboBox cb)
        {
            List<ComboboxItem> newbb1 = new List<ComboboxItem>();
            newbb1.Add(new ComboboxItem("", ""));
            newbb1.Add(new ComboboxItem("Make Appointment", "M"));
            newbb1.Add(new ComboboxItem("Other hospital", "O"));
            newbb1.Add(new ComboboxItem("Private doctor", "P"));
            cb.DataSource = newbb1;
            cb.DisplayMember = "Text";
            cb.ValueMember = "Value";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 5)
            {
                LoadRiskSummary();
            }
        }        
    }
}
