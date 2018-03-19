using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.Usercontrols;
using BKvs2010.EmrClass;

namespace BKvs2010.UserControlEMR
{
    public partial class TabAssessmentAndPlanUC : UserControl
    {
        public TabAssessmentAndPlanUC()
        {
            InitializeComponent();
            gridMasterRecom.AutoGenerateColumns = false;
            gridSelRecom.AutoGenerateColumns = false;
            gvsummary_pos_finding.AutoGenerateColumns = false;
            var data_cmbfollow_up_month = new List<structCombo>();
            data_cmbfollow_up_month.Add(new structCombo { val = "Day", dis = "วัน" });
            data_cmbfollow_up_month.Add(new structCombo { val = "Month", dis = "เดือน" });
            data_cmbfollow_up_month.Add(new structCombo { val = "Year", dis = "ปี" });
            cmbday_month_year.ValueMember = "val";
            cmbday_month_year.DisplayMember = "dis";
            cmbday_month_year.DataSource = data_cmbfollow_up_month;
            setMapField();
            //cmbday_month_year.DataSource = Enum.GetValues(typeof(follows));
            txtAddRecom.BtnFavoriteClick += favoriteTextBox1_btnFavoriteClick;
            txtAddRecom.RightClickDropDown += favoriteTextBox1_DeleteFavorite;
        }
        private void favoriteTextBox1_btnFavoriteClick(object sender, string e)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                FavoriteDoctorTextBox txtBox = sender as FavoriteDoctorTextBox;
                if (txtBox != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        int mut_id = cdc.mst_user_types.Where(x => x.mut_username == _Username)
                            .Select(x => x.mut_id)
                            .FirstOrDefault();

                        mst_autocomplete_physical_exam mst = cdc.mst_autocomplete_physical_exams
                            .Where(x => x.mape_type == txtBox.AutoCompleteType &&
                                        x.mut_id == mut_id &&
                                        x.mape_description == e)
                            .FirstOrDefault();
                        if (mst == null)
                        {
                            mst = new mst_autocomplete_physical_exam();
                            mst.mape_type = txtBox.AutoCompleteType;
                            mst.mut_id = mut_id;
                            mst.mape_active = true;
                            mst.mape_description = e;
                            mst.mape_create_date = dateNow;
                            cdc.mst_autocomplete_physical_exams.InsertOnSubmit(mst);
                            cdc.SubmitChanges();
                        }
                        List<string> tmp = txtBox.AutoCompleteListThList;
                        tmp.Add(e);
                        txtBox.AutoCompleteListThList = tmp;
                        MessageBox.Show("Add '" + txtBox.Text + "' to favorite Complete.");
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("TabAssessmentAndPlanUC", "favoriteTextBox1_btnFavoriteClick", ex, false);
            }
        }
        private void favoriteTextBox1_DeleteFavorite(object sender, string e)
        {
            try
            {
                FavoriteDoctorTextBox txtBox = sender as FavoriteDoctorTextBox;
                if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        mst_autocomplete_physical_exam mst = cdc.mst_autocomplete_physical_exams
                            .Where(x => x.mape_type == txtBox.AutoCompleteType &&
                                        x.mst_user_type.mut_username == _Username &&
                                        x.mape_description == e)
                            .FirstOrDefault();
                        if (mst != null)
                        {
                            cdc.mst_autocomplete_physical_exams.DeleteOnSubmit(mst);
                            cdc.SubmitChanges();
                        }
                        List<string> tmp = txtBox.AutoCompleteListThList;
                        tmp.Remove(e);
                        txtBox.AutoCompleteListThList = tmp;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("TabAssessmentAndPlanUC", "favoriteTextBox1_btnFavoriteClick", ex, false);
            }
        }


        private class structCombo
        {
            public string val { get; set; }
            public string dis { get; set; }
        }

        List<fieldDestinationCls> mapField = new List<fieldDestinationCls>();
        private BindingList<structureGridHdr> sourceGridHdr = new BindingList<structureGridHdr>();
        private BindingSource bsHdr = new BindingSource();
        private BindingList<structureGridDtl> sourceGridDtlTH = new BindingList<structureGridDtl>();
        private BindingList<structureGridDtl> sourceGridDtlEN = new BindingList<structureGridDtl>();
        private BindingSource bsDtl = new BindingSource();
        private BindingList<structureGridFinding> gridFinding = new BindingList<structureGridFinding>();
        private BindingSource bsGridFinding = new BindingSource();

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
                    try
                    {
                        LoadFindingProblem(ref value);
                        loadDoctorHdr(ref value);
                        loadRecommend(ref value);
                        //loadLab(ref value);
                        loadFreeText(ref value);
                        bsHdr.DataSource = sourceGridHdr;
                        gridMasterRecom.DataSource = bsHdr;
                        gridSelRecom.DataSource = bsDtl;
                        setGrid("TH");
                        bsGridFinding.DataSource = gridFinding;
                        gvsummary_pos_finding.DataSource = bsGridFinding;
                        
                        _PatientRegis = value;
                        bsPatientRegis.DataSource = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "", ex, false);
                        Clear();
                    }
                }
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                if (value != _Username)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        txtAddRecom.AutoCompleteListThList = new List<string>();
                    }
                    else
                    {
                        try
                        {
                            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                            {
                                txtAddRecom.AutoCompleteListThList = cdc.mst_autocomplete_physical_exams
                                    .Where(x => x.mst_user_type.mut_username == value &&
                                                x.mape_active == true &&
                                                x.mape_type == txtAddRecom.AutoCompleteType)
                                    .Select(x => x.mape_description)
                                    .ToList();
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError("ExamCheckupUC", "GetListAutoComplete", ex, false);
                            txtAddRecom.AutoCompleteListThList = new List<string>();
                        }
                    }
                    _Username = value;
                }
            }
        }

        private void LoadQuestionnaire(ref trn_patient_regi _patient_regis)
        {
            trn_ques_patient objQuestionnaire = _patient_regis.trn_ques_patients.FirstOrDefault();
            if (objQuestionnaire != null)
            {
                trn_finding_problem findingProblem = _patient_regis.trn_finding_problems.FirstOrDefault();
                if (objQuestionnaire.tqp_ill_med_his == 'D')
                {
                    List<string> ill_med = new List<string>();
                    if (objQuestionnaire.tqp_ill_med_hyper == true) { ill_med.Add("ความดันโลหิตสูง(Hypertension)"); }
                    if (objQuestionnaire.tqp_ill_med_heart == true) { ill_med.Add("โรคหัวใจ(Heart Disease)"); }
                    if (objQuestionnaire.tqp_ill_med_diab == true) { ill_med.Add("โรคเบาหวาน(Diabetes mellitus DM)"); }
                    if (objQuestionnaire.tqp_ill_med_coro == true) { ill_med.Add("โรคหลอดเลือดหัวใจ(Coronary Heart Disease)"); }
                    if (objQuestionnaire.tqp_ill_med_dysl == true) { ill_med.Add("โรคไขมันเลือดผิดปกติ(Dyslipidemia)"); }
                    if (objQuestionnaire.tqp_ill_med_cper == true) { ill_med.Add("โรคหลอดเลือดแดงส่วนปลาย(CPeripheral Artery Disease)"); }
                    if (objQuestionnaire.tqp_ill_med_gout == true) { ill_med.Add("โรคเก๊า(Gout)"); }
                    if (objQuestionnaire.tqp_ill_med_abdd == true) { ill_med.Add("โรคหลอดเลือดแดงใหญ่โป่งพอง(Abddminal Aortic Aneurysm)"); }
                    if (objQuestionnaire.tqp_ill_med_pulm == true) { ill_med.Add("โรคปอด(Pulmonary Disease)"); }
                    if (objQuestionnaire.tqp_ill_med_para == true) { ill_med.Add("โรคอัมพฤกษ์(Paralysis)"); }
                    if (objQuestionnaire.tqp_ill_med_putb == true) { ill_med.Add("วัณโรคปอด(Pulmonary TB)"); }
                    if (objQuestionnaire.tqp_ill_med_stro == true) { ill_med.Add("โรคอัมพาต(Stroke)"); }
                    if (objQuestionnaire.tqp_ill_med_kidn == true) { ill_med.Add("โรคไต(Kidney disease)"); }
                    if (objQuestionnaire.tqp_ill_med_epil == true) { ill_med.Add("โรคลมชัก(Epilepsy)"); }
                    if (objQuestionnaire.tqp_ill_med_resp == true) { ill_med.Add("โรคทางเดินหายใจ(Respiratory Tract disease)"); }
                    if (objQuestionnaire.tqp_ill_med_asth == true) { ill_med.Add("หอบหืด(Asthma)"); }
                    if (objQuestionnaire.tqp_ill_med_emph == true) { ill_med.Add("ถุงลมโป่งพอง(Emphysema)"); }
                    if (objQuestionnaire.tqp_ill_med_chro == true) { ill_med.Add("ปอดอุดกั้นเรื้อรัง(Chronic Obstructive Pulmonary Disease)"); }
                    if (objQuestionnaire.tqp_ill_med_sist == true) { ill_med.Add("โรคหลอดเลือดสมอง(Sis Transient Ischemic Attacks)"); }
                    if (objQuestionnaire.tqp_ill_med_alle == true) { ill_med.Add("โรคภูมิแพ้(Allergy)"); }
                    if (objQuestionnaire.tqp_ill_med_canc == true) { ill_med.Add("โรคมะเร็ง(Cancer)" + "Remark : " + objQuestionnaire.tqp_ill_med_canc_oth); }
                    if (objQuestionnaire.tqp_ill_med_pept == true) { ill_med.Add("โรคกระเพราะอาหาร(Peptic Ulcer)"); }
                    if (objQuestionnaire.tqp_ill_med_oth == true) { ill_med.Add("อื่นๆ(Other) Remark : " + objQuestionnaire.tqp_ill_med_others); }

                    findingProblem.tfp_med_history_text = string.Join(Environment.NewLine, ill_med);                    
                }

                if (objQuestionnaire.tqp_fhis_f_disease == 'D')
                {
                    List<string> fatherHis = new List<string>();
                    string coro_radio = String.Empty;
                    if (objQuestionnaire.tqp_fhis_fdis_hyper == true) { fatherHis.Add("ความดันโลหิตสูง(Hypertension)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_heart == true) { fatherHis.Add("โรคหัวใจ(Heart Disease)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_diab == true) { fatherHis.Add("โรคเบาหวาน(Diabetes mellitus DM)"); }

                    switch (objQuestionnaire.tqp_fhis_fdis_coro_cs)
                    {
                        case 'B':
                            coro_radio = "(เริ่มเป็นก่อนอายุ 55 ปี(Diagnosed before age 55 year old in male))";
                            break;
                        case 'A':
                            coro_radio = "(เริ่มเป็นหลังอายุ 55 ปี(Diagnosed after age 55 year old in male))";
                            break;
                        case 'N':
                            coro_radio = "(จำอายุที่เริ่มเป็นไม่ได้(Not sure / Unknown))";
                            break;
                    }
                    if (objQuestionnaire.tqp_fhis_fdis_coro == true) { fatherHis.Add("โรคหลอดเลือดหัวใจ(Coronary Heart Disease)" + coro_radio); }
                    if (objQuestionnaire.tqp_fhis_fdis_dysl == true) { fatherHis.Add("โรคไขมันเลือดผิดปกติ(Dyslipidemia)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_gout == true) { fatherHis.Add("โรคเก๊า(Gout)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_pulm == true) { fatherHis.Add("โรคปอด(Pulmonary Disease)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_para == true) { fatherHis.Add("โรคอัมพฤกษ์(Paralysis)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_putb == true) { fatherHis.Add("วัณโรคปอด(Pulmonary TB)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_stro == true) { fatherHis.Add("โรคอัมพาต(Stroke)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_asth == true) { fatherHis.Add("หอบหืด(Asthma)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_alle == true) { fatherHis.Add("โรคภูมิแพ้(Allergy)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_canc == true) { fatherHis.Add("โรคมะเร็ง(Cancer)" + "Remark : " + objQuestionnaire.tqp_fhis_fdis_canc_rmk); }
                    if (objQuestionnaire.tqp_fhis_fdis_pepu == true) { fatherHis.Add("โรคกระเพราะอาหาร(Peptic Ulcer)"); }
                    if (objQuestionnaire.tqp_fhis_fdis_oth == true) { fatherHis.Add("อื่นๆ(Other) Remark : " + objQuestionnaire.tqp_fhis_fdis_others); }

                    findingProblem.tfp_med_father_history_text = string.Join(Environment.NewLine, fatherHis);                    
                }

                if (objQuestionnaire.tqp_fhis_m_disease == 'D')
                {
                    List<string> motherHis = new List<string>();
                    string coro_radio = String.Empty;
                    if (objQuestionnaire.tqp_fhis_mdis_hyper == true) { motherHis.Add("ความดันโลหิตสูง(Hypertension)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_heart == true) { motherHis.Add("โรคหัวใจ(Heart Disease)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_diab == true) { motherHis.Add("โรคเบาหวาน(Diabetes mellitus DM)"); }

                    switch (objQuestionnaire.tqp_fhis_mdis_coro_cs)
                    {
                        case 'B':
                            coro_radio = "(เริ่มเป็นก่อนอายุ 55 ปี(Diagnosed before age 55 year old in male))";
                            break;
                        case 'A':
                            coro_radio = "(เริ่มเป็นหลังอายุ 55 ปี(Diagnosed after age 55 year old in male))";
                            break;
                        case 'N':
                            coro_radio = "(จำอายุที่เริ่มเป็นไม่ได้(Not sure / Unknown))";
                            break;
                    }
                    if (objQuestionnaire.tqp_fhis_mdis_coro == true) { motherHis.Add("โรคหลอดเลือดหัวใจ(Coronary Heart Disease)" + coro_radio); }
                    if (objQuestionnaire.tqp_fhis_mdis_dysl == true) { motherHis.Add("โรคไขมันเลือดผิดปกติ(Dyslipidemia)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_gout == true) { motherHis.Add("โรคเก๊า(Gout)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_pulm == true) { motherHis.Add("โรคปอด(Pulmonary Disease)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_para == true) { motherHis.Add("โรคอัมพฤกษ์(Paralysis)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_putb == true) { motherHis.Add("วัณโรคปอด(Pulmonary TB)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_stro == true) { motherHis.Add("โรคอัมพาต(Stroke)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_asth == true) { motherHis.Add("หอบหืด(Asthma)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_alle == true) { motherHis.Add("โรคภูมิแพ้(Allergy)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_canc == true) { motherHis.Add("โรคมะเร็ง(Cancer)" + "Remark : " + objQuestionnaire.tqp_fhis_mdis_canc_rmk); }
                    if (objQuestionnaire.tqp_fhis_mdis_pepu == true) { motherHis.Add("โรคกระเพราะอาหาร(Peptic Ulcer)"); }
                    if (objQuestionnaire.tqp_fhis_mdis_oth == true) { motherHis.Add("อื่นๆ(Other) Remark : " + objQuestionnaire.tqp_fhis_mdis_others); }

                    findingProblem.tfp_med_mother_history_text = string.Join(Environment.NewLine, motherHis);                    
                }

                if (objQuestionnaire.tqp_fhis_b_disease == 'D')
                {
                    List<string> familyHis = new List<string>();
                    string coro_radio = String.Empty;
                    if (objQuestionnaire.tqp_fhis_bdis_hyper == true) { familyHis.Add("ความดันโลหิตสูง(Hypertension)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_heart == true) { familyHis.Add("โรคหัวใจ(Heart Disease)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_diab == true) { familyHis.Add("โรคเบาหวาน(Diabetes mellitus DM)"); }

                    switch (objQuestionnaire.tqp_fhis_bdis_coro_cs)
                    {
                        case 'B':
                            coro_radio = "(เริ่มเป็นก่อนอายุ 55 ปี(Diagnosed before age 55 year old in male))";
                            break;
                        case 'A':
                            coro_radio = "(เริ่มเป็นหลังอายุ 55 ปี(Diagnosed after age 55 year old in male))";
                            break;
                        case 'N':
                            coro_radio = "(จำอายุที่เริ่มเป็นไม่ได้(Not sure / Unknown))";
                            break;
                    }
                    if (objQuestionnaire.tqp_fhis_bdis_coro == true) { familyHis.Add("โรคหลอดเลือดหัวใจ(Coronary Heart Disease)" + coro_radio); }
                    if (objQuestionnaire.tqp_fhis_bdis_dysl == true) { familyHis.Add("โรคไขมันเลือดผิดปกติ(Dyslipidemia)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_gout == true) { familyHis.Add("โรคเก๊า(Gout)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_pulm == true) { familyHis.Add("โรคปอด(Pulmonary Disease)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_para == true) { familyHis.Add("โรคอัมพฤกษ์(Paralysis)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_putb == true) { familyHis.Add("วัณโรคปอด(Pulmonary TB)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_stro == true) { familyHis.Add("โรคอัมพาต(Stroke)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_asth == true) { familyHis.Add("หอบหืด(Asthma)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_alle == true) { familyHis.Add("โรคภูมิแพ้(Allergy)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_canc == true) { familyHis.Add("โรคมะเร็ง(Cancer)" + "Remark : " + objQuestionnaire.tqp_fhis_bdis_canc_rmk); }
                    if (objQuestionnaire.tqp_fhis_bdis_pepu == true) { familyHis.Add("โรคกระเพราะอาหาร(Peptic Ulcer)"); }
                    if (objQuestionnaire.tqp_fhis_bdis_oth == true) { familyHis.Add("อื่นๆ(Other) Remark : " + objQuestionnaire.tqp_fhis_bdis_others); }

                    findingProblem.tfp_med_brother_history_text = string.Join(Environment.NewLine, familyHis);                    
                }
                if (findingProblem.tfp_med_history_text == null || findingProblem.tfp_med_history_text == string.Empty || findingProblem.tfp_med_history_text == "") { chk_med_his.Enabled = false; } else { chk_med_his.Enabled = true; }
                if (findingProblem.tfp_med_father_history_text == null || findingProblem.tfp_med_father_history_text == string.Empty || findingProblem.tfp_med_father_history_text == "") { chk_med_f_his.Enabled = false; } else { chk_med_f_his.Enabled = true; }
                if (findingProblem.tfp_med_mother_history_text == null || findingProblem.tfp_med_mother_history_text == string.Empty || findingProblem.tfp_med_mother_history_text == "") { chk_med_m_his.Enabled = false; } else { chk_med_m_his.Enabled = true; }
                if (findingProblem.tfp_med_brother_history_text == null || findingProblem.tfp_med_brother_history_text == string.Empty || findingProblem.tfp_med_brother_history_text == "") { chk_med_b_his.Enabled = false; } else { chk_med_b_his.Enabled = true; }
            }
        }
        private void LoadDoctorCheckup(ref trn_patient_regi _patient_regis)
        {
            trn_doctor_hdr doctorHdr = _patient_regis.trn_doctor_hdrs.FirstOrDefault();
            if (doctorHdr == null)
            {
                doctorHdr = new trn_doctor_hdr();
                _patient_regis.trn_doctor_hdrs.Add(doctorHdr);
            }
            trn_doctor_checkup doctorCheckup = doctorHdr.trn_doctor_checkups.FirstOrDefault();
            if (doctorCheckup != null)
            {
                trn_finding_problem findingProblem = _patient_regis.trn_finding_problems.FirstOrDefault();
                List<string> phyEx = new List<string>();

                if (doctorCheckup.trcp_ga == 'A') { phyEx.Add("GA-" + doctorCheckup.trcp_ga_remark); }
                if (doctorCheckup.trcp_heent == 'A') { phyEx.Add("HEENT-" + doctorCheckup.trcp_heent_remark); }
                if (doctorCheckup.trcp_chest == 'A') { phyEx.Add("CHEST-" + doctorCheckup.trcp_chest_remark); }
                if (doctorCheckup.trcp_cvs == 'A') { phyEx.Add("CSV-" + doctorCheckup.trcp_cvs_remark); }
                if (doctorCheckup.trcp_abdomen == 'A') { phyEx.Add("Abdomen-" + doctorCheckup.trcp_abdomen_remark); }
                if (doctorCheckup.trcp_extremities == 'A') { phyEx.Add("Extremities-" + doctorCheckup.trcp_ext_remark); }
                if (doctorCheckup.trcp_neuro == 'A') { phyEx.Add("Neuro-" + doctorCheckup.trcp_neuro_remark); }
                if (doctorCheckup.trcp_musculos == 'A') { phyEx.Add("Musculoskeleton system-" + doctorCheckup.trcp_musculos_remark); }
                if (doctorCheckup.trcp_other == 'A') { phyEx.Add("Other-" + doctorCheckup.trcp_other_remark); }

                findingProblem.tfp_physical_exam_text = string.Join(Environment.NewLine, phyEx);
                if (findingProblem.tfp_physical_exam_text == null || findingProblem.tfp_physical_exam_text == string.Empty || findingProblem.tfp_physical_exam_text == "") { chkphysicalEx.Enabled = false; } else { chkphysicalEx.Enabled = true; }
            }
        }
        private void LoadLaboatory(ref trn_patient_regi _patient_regis)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_finding_problem findingProblem = _patient_regis.trn_finding_problems.FirstOrDefault();
                    int tpr_id = _patient_regis.tpr_id;

                    List<string> lab = cdc.trn_patient_ass_dtls
                                          .Where(x => x.tped_summary == 'A' &&
                                                      x.trn_patient_ass_hdr.tpeh_summary == 'A' &&
                                                      x.trn_patient_ass_hdr.trn_patient_ass_grp.tpr_id == tpr_id)
                                          .Select(x => x.tped_lab_name + ' ' + ((x.tped_lab_value == null) ? "" : x.tped_lab_value) + ' ' + ((x.tped_lab_unit == null) ? "" : x.tped_lab_unit)).ToList();

                    findingProblem.tfp_laboatory_text = string.Join(Environment.NewLine, lab);
                    if (findingProblem.tfp_laboatory_text == null || findingProblem.tfp_laboatory_text == string.Empty || findingProblem.tfp_laboatory_text == "" ) { chklab.Enabled = false; } else { chklab.Enabled = true; }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadLaboatory(ref trn_patient_regi _patient_regis)", ex, false);
            }
        }
        private void LoadFindingProblem(ref trn_patient_regi _patient_regis)
        {
            try
            {
                trn_finding_problem findingProblem = _patient_regis.trn_finding_problems.FirstOrDefault();
                if (findingProblem == null)
                {
                    findingProblem = new trn_finding_problem();
                    _patient_regis.trn_finding_problems.Add(findingProblem);
                }
                LoadQuestionnaire(ref _patient_regis);
                LoadDoctorCheckup(ref _patient_regis);
                LoadLaboatory(ref _patient_regis);

                if (findingProblem.tfp_all_exam_text == null || findingProblem.tfp_all_exam_text == "")
                {
                    chkallexam.Enabled = false;
                }
                else
                {
                    chkallexam.Enabled = true;
                }

                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(findingProblem))
                {
                    findingProblem_PropertyChanged(findingProblem, new PropertyChangedEventArgs(prop.Name));
                }
                findingProblem.PropertyChanged += new PropertyChangedEventHandler(findingProblem_PropertyChanged);
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadAssetmentAndPlan", ex, false);
                return;
            }
        }        
        private void loadDoctorHdr(ref trn_patient_regi _patient_regis)
        {
            trn_doctor_hdr doctor_hdr = _patient_regis.trn_doctor_hdrs.FirstOrDefault();
            if (doctor_hdr == null)
            {
                doctor_hdr = new trn_doctor_hdr();
                _patient_regis.trn_doctor_hdrs.Add(doctor_hdr);
            }
            txtconsult_to.DataBindings.Clear();
            txtconsult_to.DataBindings.Add(new Binding("Text", doctor_hdr, "trh_consult_to", true));
            txtfollow_up.DataBindings.Clear();
            txtfollow_up.DataBindings.Add(new Binding("Text", doctor_hdr, "trh_follow_up", true));
            txtfollowup.DataBindings.Clear();
            txtfollowup.DataBindings.Add(new Binding("Text", doctor_hdr, "trh_follow_up_month", true));
            cmbday_month_year.DataBindings.Clear();
            cmbday_month_year.DataBindings.Add(new Binding("SelectedValue", doctor_hdr, "trh_day_month_year", true));
            txt_follow_up_remark.DataBindings.Clear();
            txt_follow_up_remark.DataBindings.Add(new Binding("Text", doctor_hdr, "trh_follow_remark", true));
        }
        private void loadLab(ref trn_patient_regi _patient_regis)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    int tpr_id = _patient_regis.tpr_id;
                    trn_assplan_hdr patient_assplan_hdr = _patient_regis.trn_assplan_hdrs.FirstOrDefault();
                    if (patient_assplan_hdr == null)
                    {
                        patient_assplan_hdr = new trn_assplan_hdr();
                        _patient_regis.trn_assplan_hdrs.Add(patient_assplan_hdr);
                    }
                    List<trn_patient_ass_dtl> patient_ass_dtl = dbc.trn_patient_ass_dtls
                                                                   .Where(x => x.trn_patient_ass_hdr.tpeh_summary == 'A' &&
                                                                               x.tped_summary == 'A' &&
                                                                               x.trn_patient_ass_hdr.trn_patient_ass_grp.tpr_id == tpr_id).ToList();

                    var lab_dtl = (from assDtl in patient_ass_dtl
                                   join mstLab in dbc.mst_labs 
                                   on assDtl.tped_lab_code equals mstLab.mlb_code
                                   join mstRec in dbc.mst_lab_recoms
                                   on assDtl.mlr_id equals mstRec.mlr_id
                                   where assDtl.mlr_id != null &&
                                         mstRec.mlr_th_name != null && mstRec.mlr_th_name.Trim().Length > 0 &&
                                         mstRec.mlr_en_name != null && mstRec.mlr_en_name.Trim().Length > 0
                                   select new
                                   {
                                       mstLab.mlb_id,
                                       assDtl.mlr_id,
                                       mstRec.mlr_th_name,
                                       mstRec.mlr_en_name
                                   }).ToList();

                    List<trn_assplan_lab_hdr> patient_lab_hdr = patient_assplan_hdr.trn_assplan_lab_hdrs.ToList();
                    List<int> list_mlb_id = lab_dtl.Select(x => x.mlb_id).ToList();

                    var new_lab_dtl = lab_dtl.Where(x => !patient_lab_hdr.Select(y => y.mlb_id).Contains(x.mlb_id)).ToList();

                    if (new_lab_dtl.Count() > 0)
                    {
                        List<trn_assplan_lab_hdr> new_assplan = new_lab_dtl
                                                                .Select(x => new trn_assplan_lab_hdr
                                                                {
                                                                    mlb_id = x.mlb_id,
                                                                    mlr_id = x.mlr_id,
                                                                    talh_active = true
                                                                }).ToList();

                        foreach (trn_assplan_lab_hdr hdr in new_assplan)
                        {
                            hdr.trn_assplan_lab_dtls.AddRange(new_lab_dtl
                                                              .Where(x => x.mlb_id == hdr.mlb_id)
                                                              .Select(x => new trn_assplan_lab_dtl
                                                              {
                                                                  tald_lang = "TH",
                                                                  tald_free_text = x.mlr_th_name
                                                              }));

                            hdr.trn_assplan_lab_dtls.AddRange(new_lab_dtl
                                                              .Where(x => x.mlb_id == hdr.mlb_id)
                                                              .Select(x => new trn_assplan_lab_dtl
                                                              {
                                                                  tald_lang = "EN",
                                                                  tald_free_text = x.mlr_en_name
                                                              }));
                        }
                        patient_assplan_hdr.trn_assplan_lab_hdrs.AddRange(new_assplan);
                    }

                    foreach (trn_assplan_lab_hdr hdr in patient_assplan_hdr.trn_assplan_lab_hdrs)
                    {
                        mst_lab_recom lab_recom = dbc.mst_lab_recoms.Where(x => x.mlr_id == hdr.mlr_id).FirstOrDefault();
                        structureGridHdr sgh = new structureGridHdr();
                        sgh.sourceHdr = hdr;
                        sgh.active = hdr.talh_active == true ? true : false;
                        sgh.ActiveChanged += new structureGridHdr.activeChanged(sourceGrid_ActiveChanged);
                        foreach (trn_assplan_lab_dtl dtl in hdr.trn_assplan_lab_dtls)
                        {
                            structureGridDtl sgd = new structureGridDtl
                            {
                                sourceHdr = hdr,
                                active = sgh.active,
                                lang = dtl.tald_lang,
                                defluatText = dtl.tald_lang == "TH" ? lab_recom.mlr_th_name : lab_recom.mlr_en_name,
                                recommend = dtl.tald_free_text
                            };
                            sgd.RecommendChanged += new structureGridDtl.recommendChanged(sourceGrid_RecommendChanged);
                            if (dtl.tald_lang == "TH")
                            {
                                sourceGridDtlTH.Add(sgd);
                            }
                            else
                            {
                                sourceGridDtlEN.Add(sgd);
                            }
                        }
                        sourceGridHdr.Add(sgh);
                    }
                }
            }
            catch
            {

            }
        }
        private void loadRecommend(ref trn_patient_regi _patient_regis)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_doctor_hdr doctor_hdr = _patient_regis.trn_doctor_hdrs.FirstOrDefault();
                    trn_assplan_hdr patient_assplan_hdr = _patient_regis.trn_assplan_hdrs.FirstOrDefault();
                    if (patient_assplan_hdr == null)
                    {
                        patient_assplan_hdr = new trn_assplan_hdr();
                        _patient_regis.trn_assplan_hdrs.Add(patient_assplan_hdr);
                    }

                    var grpRecom = dbc.mst_deflaut_recom_dtls
                                      .Where(x => x.mst_deflaut_recom_hdr.mdrh_status == 'A' &&
                                                  x.mst_deflaut_recom_hdr.mdrh_type == "PERC" &&
                                                  x.mdrd_status == 'A')
                                      .GroupBy(x => new { x.mst_deflaut_recom_hdr.mdrh_type, x.mdrd_type }).ToList();

                    List<trn_assplan_recom_hdr> patient_recom_hdr = patient_assplan_hdr.trn_assplan_recom_hdrs.ToList();

                    if (doctor_hdr != null && patient_recom_hdr.Count() == 0)
                    {
                        foreach (var recom_dtl in grpRecom)
                        {
                            trn_assplan_recom_hdr hdr = new trn_assplan_recom_hdr();
                            hdr.mdrh_type = recom_dtl.Key.mdrh_type;
                            hdr.mdrd_type = recom_dtl.Key.mdrd_type;
                            switch (recom_dtl.Key.mdrd_type)
                            {
                                case "AC":
                                    hdr.tarh_active = doctor_hdr.trh_annual_checkup == true ? true : false;
                                    break;
                                case "DC":
                                    hdr.tarh_active = doctor_hdr.trh_diet_control == true ? true : false;
                                    break;
                                case "RA":
                                    hdr.tarh_active = doctor_hdr.trh_reg_aerobic_exercise == true ? true : false;
                                    break;
                                case "PS":
                                    hdr.tarh_active = doctor_hdr.trh_pap_smear_yearly == true ? true : false;
                                    break;
                                case "MG":
                                    hdr.tarh_active = doctor_hdr.trh_mm_yearly == true ? true : false;
                                    break;
                                case "MB":
                                    hdr.tarh_active = doctor_hdr.trh_monthly_breast == true ? true : false;
                                    break;
                                default:
                                    hdr.tarh_active = true;
                                    break;
                            }
                            if (recom_dtl.Select(x => x.mdrd_default_active).FirstOrDefault() == true)
                            {
                                hdr.tarh_active = true;
                            }
                            foreach (mst_deflaut_recom_dtl mst_dtl in recom_dtl)
                            {
                                trn_assplan_recom_dtl dtl = new trn_assplan_recom_dtl();
                                dtl.tard_lang = mst_dtl.mdrd_lang;
                                dtl.tard_free_text = mst_dtl.mdrd_recom;
                                hdr.trn_assplan_recom_dtls.Add(dtl);
                            }
                            patient_assplan_hdr.trn_assplan_recom_hdrs.Add(hdr);
                        }
                    }
                    else if (doctor_hdr == null && patient_recom_hdr.Count() == 0)
                    {
                        var key_recom_hdr = patient_recom_hdr.Select(x => new
                        {
                            x.mdrh_type,
                            x.mdrd_type
                        }).ToList();
                        var newGrpRecom = grpRecom.Where(x => !key_recom_hdr.Contains(x.Key));
                        foreach (var recom_dtl in newGrpRecom)
                        {
                            trn_assplan_recom_hdr hdr = new trn_assplan_recom_hdr();
                            hdr.mdrh_type = recom_dtl.Key.mdrh_type;
                            hdr.mdrd_type = recom_dtl.Key.mdrd_type;
                            hdr.tarh_active = recom_dtl.Select(x => x.mdrd_default_active).FirstOrDefault() == true ? true : false;
                            foreach (mst_deflaut_recom_dtl mst_dtl in recom_dtl)
                            {
                                trn_assplan_recom_dtl dtl = new trn_assplan_recom_dtl();
                                dtl.tard_lang = mst_dtl.mdrd_lang;
                                dtl.tard_free_text = mst_dtl.mdrd_recom;
                                hdr.trn_assplan_recom_dtls.Add(dtl);
                            }
                            patient_assplan_hdr.trn_assplan_recom_hdrs.Add(hdr);
                        }
                    }

                    foreach (trn_assplan_recom_hdr hdr in patient_assplan_hdr.trn_assplan_recom_hdrs)
                    {
                        var mst_dtl = grpRecom.Where(x => x.Key.mdrh_type == hdr.mdrh_type &&
                                                          x.Key.mdrd_type == hdr.mdrd_type).FirstOrDefault();
                        structureGridHdr sgh = new structureGridHdr();
                        sgh.sourceHdr = hdr;
                        sgh.active = hdr.tarh_active == true ? true : false;
                        sgh.ActiveChanged += new structureGridHdr.activeChanged(sourceGrid_ActiveChanged);
                        foreach (trn_assplan_recom_dtl dtl in hdr.trn_assplan_recom_dtls)
                        {
                            structureGridDtl sgd = new structureGridDtl
                            {
                                sourceHdr = hdr,
                                active = sgh.active,
                                lang = dtl.tard_lang,
                                defluatText = mst_dtl.Where(x => x.mdrd_lang == dtl.tard_lang)
                                                     .Select(x => x.mdrd_recom).FirstOrDefault(),
                                recommend = dtl.tard_free_text
                            };
                            sgd.RecommendChanged += new structureGridDtl.recommendChanged(sourceGrid_RecommendChanged);
                            if (dtl.tard_lang == "TH")
                            {
                                sourceGridDtlTH.Add(sgd);
                            }
                            else
                            {
                                sourceGridDtlEN.Add(sgd);
                            }
                        }
                        sourceGridHdr.Add(sgh);
                    }
                }
            }
            catch
            {

            }
        }
        private void loadFreeText(ref trn_patient_regi _patient_regis)
        {
            trn_assplan_hdr patient_assplan_hdr = _patient_regis.trn_assplan_hdrs.FirstOrDefault();
            foreach (trn_assplan_freetext_hdr hdr in patient_assplan_hdr.trn_assplan_freetext_hdrs)
            {
                structureGridHdr sgh = new structureGridHdr();
                sgh.sourceHdr = hdr;
                sgh.active = hdr.tafh_active == true ? true : false;
                sgh.ActiveChanged += new structureGridHdr.activeChanged(sourceGrid_ActiveChanged);
                foreach (trn_assplan_freetext_dtl dtl in hdr.trn_assplan_freetext_dtls)
                {
                    structureGridDtl sgd = new structureGridDtl
                    {
                        sourceHdr = hdr,
                        active = sgh.active,
                        lang = dtl.tafd_lang,
                        defluatText = dtl.tafd_free_text,
                        recommend = dtl.tafd_free_text
                    };
                    sgd.RecommendChanged += new structureGridDtl.recommendChanged(sourceGrid_RecommendChanged);
                    if (dtl.tafd_lang == "TH")
                    {
                        sourceGridDtlTH.Add(sgd);
                    }
                    else
                    {
                        sourceGridDtlEN.Add(sgd);
                    }
                }
                sourceGridHdr.Add(sgh);
            }
        }
        private void insertFreeText(string text)
        {
            trn_assplan_hdr patient_assplan_hdr = _PatientRegis.trn_assplan_hdrs.FirstOrDefault();
            trn_assplan_freetext_hdr hdr = new trn_assplan_freetext_hdr
            {
                tafh_active = true
            };
            trn_assplan_freetext_dtl dtl_th = new trn_assplan_freetext_dtl
            {
                tafd_lang = "TH",
                tafd_free_text = text
            };
            hdr.trn_assplan_freetext_dtls.Add(dtl_th);
            trn_assplan_freetext_dtl dtl_en = new trn_assplan_freetext_dtl
            {
                tafd_lang = "EN",
                tafd_free_text = text
            };
            hdr.trn_assplan_freetext_dtls.Add(dtl_en);
            patient_assplan_hdr.trn_assplan_freetext_hdrs.Add(hdr);

            structureGridHdr sgh = new structureGridHdr();
            sgh.sourceHdr = hdr;
            sgh.active = false;
            sgh.ActiveChanged += new structureGridHdr.activeChanged(sourceGrid_ActiveChanged);
            structureGridDtl sgdTH = new structureGridDtl
            {
                sourceHdr = hdr,
                active = false,
                lang = "TH",
                defluatText = text,
                recommend = text
            };
            sgdTH.RecommendChanged += new structureGridDtl.recommendChanged(sourceGrid_RecommendChanged);
            sourceGridDtlTH.Add(sgdTH);
            structureGridDtl sgdEN = new structureGridDtl
            {
                sourceHdr = hdr,
                active = false,
                lang = "EN",
                defluatText = text,
                recommend = text
            };
            sgdEN.RecommendChanged += new structureGridDtl.recommendChanged(sourceGrid_RecommendChanged);
            sourceGridDtlEN.Add(sgdEN);
            sourceGridHdr.Add(sgh);
            sgh.active = true;
            setGrid(rdTH.Checked ? "TH" : "EN");
        }
        private void setGrid(string lang)
        {
            if (lang == "TH")
            {
                foreach (structureGridHdr hdr in sourceGridHdr)
                {
                    hdr.defluatText = sourceGridDtlTH.Where(x => x.sourceHdr == hdr.sourceHdr && x.lang == lang).Select(x => x.defluatText).FirstOrDefault();
                }
                bsDtl.DataSource = sourceGridDtlTH.Where(x => x.active == true);
            }
            else
            {
                foreach (structureGridHdr hdr in sourceGridHdr)
                {
                    hdr.defluatText = sourceGridDtlEN.Where(x => x.sourceHdr == hdr.sourceHdr && x.lang == lang).Select(x => x.defluatText).FirstOrDefault();
                }
                bsDtl.DataSource = sourceGridDtlEN.Where(x => x.active == true);
            }
            gridMasterRecom.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridMasterRecom.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridMasterRecom.Refresh();
            gridSelRecom.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            gridSelRecom.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridSelRecom.Refresh();
        }

        private void sourceGrid_ActiveChanged(object source, bool value)
        {
            trn_doctor_hdr doctor_hdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            trn_assplan_hdr assplan_hdr = _PatientRegis.trn_assplan_hdrs.FirstOrDefault();
            if (source.GetType() == typeof(trn_assplan_lab_hdr))
            {
                trn_assplan_lab_hdr lab = source as trn_assplan_lab_hdr;
                trn_assplan_lab_hdr lab_hdr = assplan_hdr.trn_assplan_lab_hdrs[assplan_hdr.trn_assplan_lab_hdrs.IndexOf(lab)];
                lab_hdr.talh_active = value;
                List<structureGridDtl> sgdTH = sourceGridDtlTH.Where(x => x.sourceHdr == lab).ToList();
                foreach (structureGridDtl dtl in sgdTH)
                {
                    dtl.active = value;
                }
                List<structureGridDtl> sgdEN = sourceGridDtlEN.Where(x => x.sourceHdr == lab).ToList();
                foreach (structureGridDtl dtl in sgdEN)
                {
                    dtl.active = value;
                }
            }
            else if (source.GetType() == typeof(trn_assplan_recom_hdr))
            {
                trn_assplan_recom_hdr recom = source as trn_assplan_recom_hdr;
                trn_assplan_recom_hdr recom_hdr = assplan_hdr.trn_assplan_recom_hdrs[assplan_hdr.trn_assplan_recom_hdrs.IndexOf(recom)];
                if (doctor_hdr != null)
                {
                    switch (recom_hdr.mdrd_type)
                    {
                        case "AC":
                            doctor_hdr.trh_annual_checkup = value;
                            break;
                        case "DC":
                            doctor_hdr.trh_diet_control = value;
                            break;
                        case "RA":
                            doctor_hdr.trh_reg_aerobic_exercise = value;
                            break;
                        case "PS":
                            doctor_hdr.trh_pap_smear_yearly = value;
                            break;
                        case "MG":
                            doctor_hdr.trh_mm_yearly = value;
                            break;
                        case "MB":
                            doctor_hdr.trh_monthly_breast = value;
                            break;
                    }
                }
                recom_hdr.tarh_active = value;
                List<structureGridDtl> sgdTH = sourceGridDtlTH.Where(x => x.sourceHdr == recom).ToList();
                foreach (structureGridDtl dtl in sgdTH)
                {
                    dtl.active = value;
                }
                List<structureGridDtl> sgdEN = sourceGridDtlEN.Where(x => x.sourceHdr == recom).ToList();
                foreach (structureGridDtl dtl in sgdEN)
                {
                    dtl.active = value;
                }
            }
            else if (source.GetType() == typeof(trn_assplan_freetext_hdr))
            {
                trn_assplan_freetext_hdr freetext = source as trn_assplan_freetext_hdr;
                trn_assplan_freetext_hdr freetext_hdr = assplan_hdr.trn_assplan_freetext_hdrs[assplan_hdr.trn_assplan_freetext_hdrs.IndexOf(freetext)];
                freetext_hdr.tafh_active = value;
                List<structureGridDtl> sgdTH = sourceGridDtlTH.Where(x => x.sourceHdr == freetext).ToList();
                foreach (structureGridDtl dtl in sgdTH)
                {
                    dtl.active = value;
                }
                List<structureGridDtl> sgdEN = sourceGridDtlEN.Where(x => x.sourceHdr == freetext).ToList();
                foreach (structureGridDtl dtl in sgdEN)
                {
                    dtl.active = value;
                }
            }
            List<string> list_th = sourceGridDtlTH.Where(x => x.active == true)
                                                      .Select(x => x.recommend).ToList();
            if (list_th.Count() > 0)
            {
                string sum_th = string.Join("|", list_th);
                doctor_hdr.trh_recomm_summary = sum_th;
            }
            else
            {
                doctor_hdr.trh_recomm_summary = null;
            }
            List<string> list_en = sourceGridDtlEN.Where(x => x.active == true)
                                                      .Select(x => x.recommend).ToList();
            if (list_en.Count() > 0)
            {
                string sum_en = string.Join("|", list_en);
                doctor_hdr.trh_recomm_summary_en = sum_en;
            }
            else
            {
                doctor_hdr.trh_recomm_summary_en = null;
            }
            
            setGrid(rdTH.Checked ? "TH" : "EN");
        }
        private void sourceGrid_RecommendChanged(object source, string lang, string value)
        {
            trn_assplan_hdr assplan_hdr = _PatientRegis.trn_assplan_hdrs.FirstOrDefault();
            if (source.GetType() == typeof(trn_assplan_lab_hdr))
            {
                trn_assplan_lab_hdr lab = source as trn_assplan_lab_hdr;
                trn_assplan_lab_hdr lab_hdr = assplan_hdr.trn_assplan_lab_hdrs[assplan_hdr.trn_assplan_lab_hdrs.IndexOf(lab)];
                trn_assplan_lab_dtl lab_dtl = lab_hdr.trn_assplan_lab_dtls
                                                     .Where(x => x.tald_lang == lang)
                                                     .FirstOrDefault();
                lab_dtl.tald_free_text = value;
            }
            else if (source.GetType() == typeof(trn_assplan_recom_hdr))
            {
                trn_assplan_recom_hdr recom = source as trn_assplan_recom_hdr;
                trn_assplan_recom_hdr recom_hdr = assplan_hdr.trn_assplan_recom_hdrs[assplan_hdr.trn_assplan_recom_hdrs.IndexOf(recom)];
                trn_assplan_recom_dtl recom_dtl = recom_hdr.trn_assplan_recom_dtls
                                                           .Where(x => x.tard_lang == lang)
                                                           .FirstOrDefault();
                recom_dtl.tard_free_text = value;
            }
            else if (source.GetType() == typeof(trn_assplan_freetext_hdr))
            {
                trn_assplan_freetext_hdr freetext = source as trn_assplan_freetext_hdr;
                trn_assplan_freetext_hdr freetext_hdr = assplan_hdr.trn_assplan_freetext_hdrs[assplan_hdr.trn_assplan_freetext_hdrs.IndexOf(freetext)];
                trn_assplan_freetext_dtl freetext_dtl = freetext_hdr.trn_assplan_freetext_dtls
                                                                    .Where(x => x.tafd_lang == lang)
                                                                    .FirstOrDefault();
                freetext_dtl.tafd_free_text = value;
            }

            trn_doctor_hdr doctor_hdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            if (lang == "TH")
            {
                List<string> list_th = sourceGridDtlTH.Where(x => x.active == true)
                                                      .Select(x => x.recommend).ToList();
                if (list_th.Count() > 0)
                {
                    string sum_th = string.Join("|", list_th);
                    doctor_hdr.trh_recomm_summary = sum_th;
                }
                else
                {
                    doctor_hdr.trh_recomm_summary = null;
                }
            }
            else if (lang == "EN")
            {
                List<string> list_en = sourceGridDtlEN.Where(x => x.active == true)
                                                      .Select(x => x.recommend).ToList();
                if (list_en.Count() > 0)
                {
                    string sum_en = string.Join("|", list_en);
                    doctor_hdr.trh_recomm_summary_en = sum_en;
                }
                else
                {
                    doctor_hdr.trh_recomm_summary_en = null;
                }
            }
            List<string> list_finding = sourceGridDtlEN.Where(x => x.active == true)
                                                      .Select(x => x.recommend).ToList();
            if (list_finding.Count() > 0)
            {
                string sum_finding = string.Join("|", list_finding);
                doctor_hdr.trh_positive_finding = sum_finding;
            }
            else
            {
                doctor_hdr.trh_positive_finding = null;
            }
        }
        private void sourceGrid_TextChanged(CheckBox chkBox, string value)
        {
            trn_finding_problem objFinding = _PatientRegis.trn_finding_problems.FirstOrDefault();
            string txtEdit = gridFinding.Where(x => x.header == chkBox.Text).Select(x => x.textEdit).FirstOrDefault();
            switch (Convert.ToInt32(chkBox.Tag))
            {
                case 1:
                    objFinding.tfp_med_history_text_edit = txtEdit;
                    break;
                case 2 :
                    objFinding.tfp_med_father_history_text_edit = txtEdit;
                    break;
                case 3:
                    objFinding.tfp_med_mother_history_text_edit = txtEdit;
                    break;
                case 4:
                    objFinding.tfp_med_brother_history_text_edit = txtEdit;
                    break;
                case 5:
                    objFinding.tfp_physical_exam_text_edit = txtEdit;
                    break;
                case 6:
                    objFinding.tfp_laboatory_text_edit = txtEdit;
                    break;
                case 7:
                    objFinding.tfp_all_exam_text_edit = txtEdit;
                    break;
            }            
        }

        private void rdTH_CheckedChanged(object sender, EventArgs e)
        {
            if (rdTH.Checked)
            {
                setGrid("TH");
            }
        }
        private void rdEN_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEN.Checked)
            {
                setGrid("EN");
            }
        }
        private void gridMasterRecom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView gv = (DataGridView)sender;
                if (gv.Columns[e.ColumnIndex].Name == "ColChk")
                {
                    if (gv["ColChk", e.RowIndex] != null && (bool)gv["ColChk", e.RowIndex].Value == true)
                    {
                        gv["ColChk", e.RowIndex].Value = false;
                    }
                    else
                    {
                        gv["ColChk", e.RowIndex].Value = true;
                    }
                }
            }
            catch
            {

            }
        }
        private void gridMasterRecom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView gv = (DataGridView)sender;
                if (gv.Columns[e.ColumnIndex].Name == "defluatText")
                {
                    if (gv["ColChk", e.RowIndex] != null && (bool)gv["ColChk", e.RowIndex].Value == true)
                    {
                        gv["ColChk", e.RowIndex].Value = false;
                    }
                    else
                    {
                        gv["ColChk", e.RowIndex].Value = true;
                    }
                }
            }
            catch
            {

            }
        }
        private void txtAddRecom_Enter(object sender, EventArgs e)
        {
            alertRqRecom.Visible = false;
        }
        private void txtAddRecom_Leave(object sender, EventArgs e)
        {
            txtAddRecom.Text = txtAddRecom.Text.Trim();
            alertRqRecom.Visible = false;
        }
        private void btnRecomAdd_Click(object sender, EventArgs e)
        {
            if (txtAddRecom.Text.Length > 0)
            {
                insertFreeText(txtAddRecom.Text);
                txtAddRecom.Text = "";
                alertRqRecom.Visible = false;
            }
            else
            {
                txtAddRecom.Focus();
                alertRqRecom.Visible = true;
            }
        }

        private class structureGridHdr
        {
            public delegate void activeChanged(object sender, bool value);
            public event activeChanged ActiveChanged;
            private void _activeChanged(bool value)
            {
                if (ActiveChanged == null) return;
                ActiveChanged(sourceHdr, value);
            }

            public object sourceHdr { get; set; }
            public string defluatText { get; set; }
            private bool _active;
            public bool active
            {
                get
                {
                    return _active;
                }
                set
                {
                    _active = value;
                    _activeChanged(value);
                }
            }
        }
        private class structureGridDtl
        {
            public delegate void recommendChanged(object sender, string lang, string value);
            public event recommendChanged RecommendChanged;
            private void _recommendChanged(string value)
            {
                if (RecommendChanged == null) return;
                RecommendChanged(sourceHdr, lang, value);
            }

            public string lang { get; set; }
            public object sourceHdr { get; set; }
            public string defluatText { get; set; }
            public bool active { get; set; }
            private string _recommend;
            public string recommend
            {
                get
                {
                    return _recommend;
                }
                set
                {
                    _recommend = value;
                    _recommendChanged(value);
                }
            }
        }
        private class structureGridFinding
        {
            public delegate void textChanged(CheckBox chkBox, string value);
            public event textChanged TextChanged;
            private void _textChanged(string value)
            {
                if (TextChanged == null) return;
                TextChanged(chkBox, value);
            }
            public string header { get; set; }            
            public string defaultText { get; set; }
            private string _textEdit;
            public string textEdit
            {
                get
                {
                    return _textEdit;
                }
                set
                {
                    _textEdit = value;
                    _textChanged(value);
                }
            }
            public CheckBox chkBox { get; set; }
        }

        public void Clear()
        {
            this.Enabled = false;
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            trn_doctor_hdr doctor_hdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();

            if (gridFinding.Count > 0)
            {
                string med = "";
                foreach (structureGridFinding item in gridFinding)
                {
                    med += item.header + item.textEdit + "|";
                }
                doctor_hdr.trh_positive_finding = med;
            }
            else
            {
                doctor_hdr.trh_positive_finding = null;
            }
        }

        private void SummaryTextResult(ListBox lb, int tagid, CheckBox ch)
        {
            trn_doctor_hdr doctor_hdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            StringBuilder sb = new StringBuilder();
            string sum_finding = "";
            //if (gvsummary_pos_finding.Rows.Count == 1)
            {
                if (lb.Items.Count > 0)
                {
                    for (int i = 0; i < lb.Items.Count; i++)
                    {
                        //gvsummary_pos_finding.Rows.Add(sb.ToString(), tagid);                    
                        gvsummary_pos_finding.Rows.Add(ch.Text + lb.Items[i].ToString(), tagid);
                    }
                    for (int j = 0; j < gvsummary_pos_finding.Rows.Count; j++)
                    {
                        if (gvsummary_pos_finding.Rows[j].Cells["Column6"].Value != null)
                        {
                            sum_finding = sum_finding + "|" + gvsummary_pos_finding.Rows[j].Cells["Column6"].Value.ToString();
                        }
                    }
                }
                doctor_hdr.trh_positive_finding = sum_finding;
            }
        }
        private void RemoveTextResult(int row)
        {
            trn_doctor_hdr doctor_hdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            //int iRowIndex = -1;
            int iRowIndex = 0;
            List<int> removeList = new List<int>();
            string sum_finding = "";
            foreach (DataGridViewRow iRow in gvsummary_pos_finding.Rows)
            {
                if (iRow.Cells[1].Value != null)
                    if ((int)iRow.Cells[1].Value == row)
                    {
                        //iRowIndex += 1;
                        //gvsummary_pos_finding.Rows.RemoveAt(iRowIndex);
                        //break;
                        removeList.Add(iRowIndex);
                    }
                iRowIndex += 1;
            }
            for (int i = removeList.Count(); i > 0; i--)
            {
                gvsummary_pos_finding.Rows.RemoveAt(removeList[i - 1]);                
            }
            for (int j = 0; j < gvsummary_pos_finding.Rows.Count; j++)
            {
                if (gvsummary_pos_finding.Rows[j].Cells["Column6"].Value != null)
                {
                    sum_finding = sum_finding + "|" + gvsummary_pos_finding.Rows[j].Cells["Column6"].Value.ToString();
                }
            }
            doctor_hdr.trh_positive_finding = sum_finding;
        }

        private void chk_med_m_his_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_med_m_his.Checked == true)
            {
                structureGridFinding gf = new structureGridFinding
                {
                    header = chk_med_m_his.Text,
                    defaultText = txt_med_mother_history.Text,
                    textEdit = txt_med_mother_history.Text,
                    chkBox=chk_med_m_his
                };
                gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                gridFinding.Add(gf);
            }
            else
            {
                structureGridFinding gf = gridFinding.Where(x => x.header == chk_med_m_his.Text).FirstOrDefault();
                gridFinding.Remove(gf);
            }
        }
        private void chk_med_f_his_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_med_f_his.Checked == true)
            {
                structureGridFinding gf = new structureGridFinding
                {
                    header = chk_med_f_his.Text,
                    defaultText = txt_med_father_history.Text,
                    textEdit = txt_med_father_history.Text,
                    chkBox=chk_med_f_his
                };
                gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                gridFinding.Add(gf);
            }
            else
            {
                structureGridFinding gf = gridFinding.Where(x => x.header == chk_med_f_his.Text).FirstOrDefault();
                gridFinding.Remove(gf);
            }
        }      
        private void chkallexam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkallexam.Checked == true)
            {
                structureGridFinding gf = new structureGridFinding
                {
                    header = chkallexam.Text,
                    defaultText = txt_all_exam.Text,
                    textEdit = txt_all_exam.Text,
                    chkBox=chkallexam
                };
                gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                gridFinding.Add(gf);
            }
            else
            {
                structureGridFinding gf = gridFinding.Where(x => x.header == chkallexam.Text).FirstOrDefault();
                gridFinding.Remove(gf);
            }
        }
        private void chklab_CheckedChanged(object sender, EventArgs e)
        {
            if (chklab.Checked == true)
            {
                structureGridFinding gf = new structureGridFinding
                {
                    header = chklab.Text,
                    defaultText = txt_laboatory.Text,
                    textEdit = txt_laboatory.Text,
                    chkBox=chklab
                };
                gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                gridFinding.Add(gf);
            }
            else
            {
                structureGridFinding gf = gridFinding.Where(x => x.header == chklab.Text).FirstOrDefault();
                gridFinding.Remove(gf);
            }
        }
        private void chk_med_b_his_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_med_b_his.Checked == true)
            {
                structureGridFinding gf = new structureGridFinding
                {
                    header = chk_med_b_his.Text,
                    defaultText = txt_med_brother_history.Text,
                    textEdit = txt_med_brother_history.Text,
                    chkBox=chk_med_b_his
                };
                gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                gridFinding.Add(gf);
            }
            else
            {
                structureGridFinding gf = gridFinding.Where(x => x.header == chk_med_b_his.Text).FirstOrDefault();
                gridFinding.Remove(gf);
            }
        }
        private void chkphysicalEx_CheckedChanged(object sender, EventArgs e)
        {
            if (chkphysicalEx.Checked == true)
            {
                structureGridFinding gf = new structureGridFinding
                {
                    header = chkphysicalEx.Text,
                    defaultText = txt_physical_exam.Text,
                    textEdit = txt_physical_exam.Text,
                    chkBox=chkphysicalEx
                };
                gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                gridFinding.Add(gf);
            }
            else
            {
                structureGridFinding gf = gridFinding.Where(x => x.header == chkphysicalEx.Text).FirstOrDefault();
                gridFinding.Remove(gf);
            }
        }
        private void chk_med_his_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_med_his.Checked == true)
            {
                structureGridFinding gf = new structureGridFinding
                {
                    header = chk_med_his.Text,
                    defaultText = text_med_history.Text,
                    textEdit = text_med_history.Text,
                    chkBox=chk_med_his
                };
                gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                gridFinding.Add(gf);
            }
            else
            {
                structureGridFinding gf = gridFinding.Where(x => x.header == chk_med_his.Text).FirstOrDefault();
                gridFinding.Remove(gf);
            }
        }

        private class fieldDestinationCls
        {
            public string fieldCheck { get; set; }
            public string fieldDestination1 { get; set; }
            public string fieldDestination2 { get; set; }
            public CheckBox checkBox { get; set; }
        }        
        private void setMapField()
        {
            mapField = new List<fieldDestinationCls>
            {
                new fieldDestinationCls
                {                    
                    fieldCheck = "tfp_med_history",
                    fieldDestination1 = "tfp_med_history_text",
                    fieldDestination2 = "tfp_med_history_text_edit",
                    checkBox = chk_med_his
                },

                new fieldDestinationCls
                {
                    fieldCheck = "tfp_med_father_history",
                    fieldDestination1 = "tfp_med_father_history_text",
                    fieldDestination2 = "tfp_med_father_history_text_edit",
                    checkBox = chk_med_f_his
                },

                new fieldDestinationCls
                {
                    fieldCheck = "tfp_med_mother_history",
                    fieldDestination1 = "tfp_med_mother_history_text",
                    fieldDestination2 = "tfp_med_mother_history_text_edit",
                    checkBox = chk_med_m_his
                },

                new fieldDestinationCls
                {
                    fieldCheck = "tfp_med_brother_history",
                    fieldDestination1 = "tfp_med_brother_history_text",
                    fieldDestination2 = "tfp_med_brother_history_text_edit",
                    checkBox = chk_med_b_his
                },

                new fieldDestinationCls
                {
                    fieldCheck = "tfp_physical_exam",
                    fieldDestination1 = "tfp_physical_exam_text",
                    fieldDestination2 = "tfp_physical_exam_text_edit",
                    checkBox = chkphysicalEx
                },

                new fieldDestinationCls
                {
                    fieldCheck = "tfp_laboatory",
                    fieldDestination1 = "tfp_laboatory_text",
                    fieldDestination2 = "tfp_laboatory_text_edit",
                    checkBox = chklab
                },

                new fieldDestinationCls
                {
                    fieldCheck = "tfp_all_exam",
                    fieldDestination1 = "tfp_all_exam_text",
                    fieldDestination2 = "tfp_all_exam_text_edit",
                    checkBox = chkallexam
                }
            };
        }
        private void findingProblem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            fieldDestinationCls chkField = mapField.Where(x => x.fieldCheck == e.PropertyName).FirstOrDefault();
            if (chkField != null)
            {
                var valflag = TypeDescriptor.GetProperties(sender)[chkField.fieldCheck].GetValue(sender);                
                if ((bool?)valflag != null)
                {
                    if ((bool?)valflag == true)
                    {
                        chkField.checkBox.Checked = true;
                        var valDefault = TypeDescriptor.GetProperties(sender)[chkField.fieldDestination1].GetValue(sender);
                        var valEdit = TypeDescriptor.GetProperties(sender)[chkField.fieldDestination2].GetValue(sender);
                        structureGridFinding gf = new structureGridFinding
                        {
                            header = chkField.checkBox.Text,
                            defaultText = valDefault.ToString(),
                            //textEdit = (string)valEdit,
                            chkBox = chkField.checkBox
                        };
                        if ((string)valEdit == null || (string)valEdit == string.Empty)
                        {
                            gf.textEdit = (string)valDefault;
                        }
                        else
                        {
                            gf.textEdit = (string)valEdit;
                        }
                        gf.TextChanged += new structureGridFinding.textChanged(sourceGrid_TextChanged);
                        gridFinding.Add(gf);
                    }
                    else if ((bool?)valflag == false)
                    {
                        chkField.checkBox.Checked = false;
                        structureGridFinding gf = gridFinding.Where(x => x.header == chkField.checkBox.Text).FirstOrDefault();
                        gridFinding.Remove(gf);
                    }
                }
                else
                {
                    chkField.checkBox.Checked = false;
                }
            }
            //else
            //{
            //    fieldDestinationCls desField = mapField.Where(x => x.fieldDestination2 == e.PropertyName).FirstOrDefault();
            //    if (desField != null)
            //    {
            //        var val = TypeDescriptor.GetProperties(sender)[desField.fieldDestination2].GetValue(sender);
            //        var valflag = TypeDescriptor.GetProperties(sender)[desField.fieldCheck].GetValue(sender);
            //        structureGridFinding result = gridFinding.OfType<structureGridFinding>().Where(x => x.textEdit == desField.fieldDestination2).FirstOrDefault();
            //        if (valflag != null)
            //        {
            //            if (valflag.GetType() == typeof(string) || valflag.GetType() == typeof(char))
            //            {
            //                if (string.IsNullOrEmpty(valflag.ToString()) || valflag.ToString() == "N")
            //                {
            //                    if (result != null)
            //                    {
            //                        gridFinding.Remove(result);
            //                    }
            //                }
            //                else
            //                {
            //                    //if (result != null)
            //                    //{
            //                    //    result.val = val.ToString();
            //                    //}
            //                    //else
            //                    //{
            //                    //    sourceDiag.Add(new clsSourceDiagnosis
            //                    //    {
            //                    //        fieldName = desField.fieldDestination,
            //                    //        side = desField.fieldSide,
            //                    //        name = desField.fieldDesciption1,
            //                    //        name_desc = desField.fieldDesciption2,
            //                    //        val = val.ToString()
            //                    //    });
            //                    //}
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (result != null)
            //            {
            //                //sourceDiag.Remove(result);
            //            }
            //        }
            //        //dgvDiagnosis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //        //dgvDiagnosis.Refresh();
            //    }
            //}
        }
    }
}
