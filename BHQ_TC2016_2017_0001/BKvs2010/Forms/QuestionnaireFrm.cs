using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Forms
{
    public partial class QuestionnaireFrm : Form
    {
        public QuestionnaireFrm()
        {
            InitializeComponent();
        }

        InhCheckupDataContext dbc;

        bool IsSaveComplete = false;

        private string username;
        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value != _tpr_id)
                {
                    if (value == null)
                    {
                        Clear();
                    }
                    else
                    {
                        dbc = new InhCheckupDataContext();
                        trn_patient_regi PatientRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();                                                      
                        
                       try
                        {
                            //dbc = new InhCheckupDataContext();
                            username = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                            trn_ques_patient quePatient = PatientRegis.trn_ques_patients.FirstOrDefault();
                            
                            
                            if (quePatient == null) //ถ้าคนนี้ยังไม่มี Question 
                            {
                                //pali add  for  get last questionare  27/2/2018
                                var tpt_id = dbc.trn_patient_regis.Where(x => x.tpr_id == value).Select(x => x.tpt_id).FirstOrDefault(); // หา tpt 
                                trn_patient_regi PatientRegis_ques = dbc.trn_patient_regis.Where(x => x.tpr_id != value).Where(x => x.tpt_id == tpt_id).OrderByDescending(x => x.tpr_create_date).FirstOrDefault(); //ดูว่าเคยมาตรวจมั้ย
                                if (PatientRegis_ques != null) //ถ้าเคยมาตรวจ
                                {
                                    trn_ques_patient quePatient_ques = PatientRegis_ques.trn_ques_patients.FirstOrDefault(); //หา queation ของที่เคยมาตรวจ
                                    

                                    if (quePatient_ques != null) //มี Question เก่า
                                    {
                                        quePatient = new trn_ques_patient() ;
                                       
                                        quePatient.tpr_id = (int)value;
                                        quePatient.tqp_type = quePatient_ques.tqp_type;
                                        quePatient.tqp_confirm_doctor = quePatient_ques.tqp_confirm_doctor;
                                        quePatient.tqp_confirm_date = quePatient_ques.tqp_confirm_date;
                                        quePatient.tqp_his_smok = quePatient_ques.tqp_his_smok;
                                        quePatient.tqp_his_nsmok_yrs = quePatient_ques.tqp_his_nsmok_yrs;
                                        quePatient.tqp_his_qsmok_yrs = quePatient_ques.tqp_his_qsmok_yrs;
                                        quePatient.tqp_his_smok_amt = quePatient_ques.tqp_his_smok_amt;
                                        quePatient.tqp_his_smok_dur = quePatient_ques.tqp_his_smok_dur;
                                        quePatient.tqp_his_smok_remark = quePatient_ques.tqp_his_smok_remark;
                                        quePatient.tqp_his_alcohol = quePatient_ques.tqp_his_alcohol;
                                        quePatient.tqp_his_alco_yrs = quePatient_ques.tqp_his_alco_yrs;
                                        quePatient.tqp_his_alco_social = quePatient_ques.tqp_his_alco_social;
                                        quePatient.tqp_his_exercise = quePatient_ques.tqp_his_exercise;
                                        quePatient.tqp_ill_concern = quePatient_ques.tqp_ill_concern;
                                        quePatient.tqp_ill_conc_oth = quePatient_ques.tqp_ill_conc_oth;
                                        quePatient.tqp_ill_chkup = quePatient_ques.tqp_ill_chkup;
                                        quePatient.tqp_ill_psycho = quePatient_ques.tqp_ill_psycho;
                                        quePatient.tqp_ill_psycho_oth = quePatient_ques.tqp_ill_psycho_oth;
                                        quePatient.tqp_ill_med_his = quePatient_ques.tqp_ill_med_his;
                                        quePatient.tqp_ill_med_hyper = quePatient_ques.tqp_ill_med_hyper;
                                        quePatient.tqp_ill_med_heart = quePatient_ques.tqp_ill_med_heart;
                                        quePatient.tqp_ill_med_heart_txt = quePatient_ques.tqp_ill_med_heart_txt;
                                        quePatient.tqp_ill_med_diab = quePatient_ques.tqp_ill_med_diab;
                                        quePatient.tqp_ill_med_coro = quePatient_ques.tqp_ill_med_coro;
                                        quePatient.tqp_ill_med_dysl = quePatient_ques.tqp_ill_med_dysl;
                                        quePatient.tqp_ill_med_cper = quePatient_ques.tqp_ill_med_cper;
                                        quePatient.tqp_ill_med_gout = quePatient_ques.tqp_ill_med_gout;
                                        quePatient.tqp_ill_med_abdd = quePatient_ques.tqp_ill_med_abdd;
                                        quePatient.tqp_ill_med_pulm = quePatient_ques.tqp_ill_med_pulm;
                                        quePatient.tqp_ill_med_para = quePatient_ques.tqp_ill_med_para;
                                        quePatient.tqp_ill_med_stro = quePatient_ques.tqp_ill_med_stro;
                                        quePatient.tqp_ill_med_putb = quePatient_ques.tqp_ill_med_putb;
                                        quePatient.tqp_ill_med_sist = quePatient_ques.tqp_ill_med_sist;
                                        quePatient.tqp_ill_med_kidn = quePatient_ques.tqp_ill_med_kidn;
                                        quePatient.tqp_ill_med_epil = quePatient_ques.tqp_ill_med_epil;
                                        quePatient.tqp_ill_med_hepa = quePatient_ques.tqp_ill_med_hepa;
                                        quePatient.tqp_ill_med_resp = quePatient_ques.tqp_ill_med_resp;
                                        quePatient.tqp_ill_med_asth = quePatient_ques.tqp_ill_med_asth;
                                        quePatient.tqp_ill_med_emph = quePatient_ques.tqp_ill_med_emph;
                                        quePatient.tqp_ill_med_chro = quePatient_ques.tqp_ill_med_chro;
                                        quePatient.tqp_ill_med_bron = quePatient_ques.tqp_ill_med_bron;
                                        quePatient.tqp_ill_med_cough = quePatient_ques.tqp_ill_med_cough;
                                        quePatient.tqp_ill_med_rhin = quePatient_ques.tqp_ill_med_rhin;
                                        quePatient.tqp_ill_med_canc = quePatient_ques.tqp_ill_med_canc;
                                        quePatient.tqp_ill_med_canc_oth = quePatient_ques.tqp_ill_med_canc_oth;
                                        quePatient.tqp_ill_med_alle = quePatient_ques.tqp_ill_med_alle;
                                        quePatient.tqp_ill_med_pept = quePatient_ques.tqp_ill_med_pept;
                                        quePatient.tqp_ill_med_oth = quePatient_ques.tqp_ill_med_oth;
                                        quePatient.tqp_ill_med_others = quePatient_ques.tqp_ill_med_others;
                                        quePatient.tqp_ill_med_rmk = quePatient_ques.tqp_ill_med_rmk;
                                        quePatient.tqp_ill_med_rmk_oth = quePatient_ques.tqp_ill_med_rmk_oth;
                                        quePatient.tqp_fam_med_asth = quePatient_ques.tqp_fam_med_asth;
                                        quePatient.tqp_fam_med_bron = quePatient_ques.tqp_fam_med_bron;
                                        quePatient.tqp_fam_med_alle = quePatient_ques.tqp_fam_med_alle;
                                        quePatient.tqp_fam_med_cough = quePatient_ques.tqp_fam_med_cough;
                                        quePatient.tqp_fam_med_rhin = quePatient_ques.tqp_fam_med_rhin;
                                        quePatient.tqp_fam_med_oth = quePatient_ques.tqp_fam_med_oth;
                                        quePatient.tqp_envi_hme_dust = quePatient_ques.tqp_envi_hme_dust;
                                        quePatient.tqp_envi_hme_smoke = quePatient_ques.tqp_envi_hme_smoke;
                                        quePatient.tqp_envi_hme_chem = quePatient_ques.tqp_envi_hme_chem;
                                        quePatient.tqp_envi_hme_pollen = quePatient_ques.tqp_envi_hme_pollen;
                                        quePatient.tqp_envi_hme_pet = quePatient_ques.tqp_envi_hme_pet;
                                        quePatient.tqp_envi_other = quePatient_ques.tqp_envi_other;
                                        quePatient.tqp_envi_hme_other = quePatient_ques.tqp_envi_hme_other;
                                        quePatient.tqp_envi_off_dust = quePatient_ques.tqp_envi_off_dust;
                                        quePatient.tqp_envi_off_smoke = quePatient_ques.tqp_envi_off_smoke;
                                        quePatient.tqp_envi_off_chem = quePatient_ques.tqp_envi_off_chem;
                                        quePatient.tqp_envi_off_pollen = quePatient_ques.tqp_envi_off_pollen;
                                        quePatient.tqp_envi_off_pet = quePatient_ques.tqp_envi_off_pet;
                                        quePatient.tqp_envi_off_other = quePatient_ques.tqp_envi_off_other;
                                        quePatient.tqp_envi_dur = quePatient_ques.tqp_envi_dur;
                                        quePatient.tqp_envi_yrs = quePatient_ques.tqp_envi_yrs;
                                        quePatient.tqp_cur_ill_cough = quePatient_ques.tqp_cur_ill_cough;
                                        quePatient.tqp_cur_ill_wcough = quePatient_ques.tqp_cur_ill_wcough;
                                        quePatient.tqp_cur_ill_gcough = quePatient_ques.tqp_cur_ill_gcough;
                                        quePatient.tqp_cur_ill_bcough = quePatient_ques.tqp_cur_ill_bcough;
                                        quePatient.tqp_cou_per_morn = quePatient_ques.tqp_cou_per_morn;
                                        quePatient.tqp_cou_per_aday = quePatient_ques.tqp_cou_per_aday;
                                        quePatient.tqp_cou_per_night = quePatient_ques.tqp_cou_per_night;
                                        quePatient.tqp_cou_per_rarely = quePatient_ques.tqp_cou_per_rarely;
                                        quePatient.tqp_cou_per_nsure = quePatient_ques.tqp_cou_per_nsure;
                                        quePatient.tqp_cur_ill_pant = quePatient_ques.tqp_cur_ill_pant;
                                        quePatient.tqp_pat_per_morn = quePatient_ques.tqp_pat_per_morn;
                                        quePatient.tqp_pat_per_aday = quePatient_ques.tqp_pat_per_aday;
                                        quePatient.tqp_pat_per_night = quePatient_ques.tqp_pat_per_night;
                                        quePatient.tqp_pat_per_rarely = quePatient_ques.tqp_pat_per_rarely;
                                        quePatient.tqp_pat_per_nsure = quePatient_ques.tqp_pat_per_nsure;
                                        quePatient.tqp_pat_freq = quePatient_ques.tqp_pat_freq;
                                        quePatient.tqp_pat_exercise = quePatient_ques.tqp_pat_exercise;
                                        quePatient.tqp_pat_still = quePatient_ques.tqp_pat_still;
                                        quePatient.tqp_pat_pros = quePatient_ques.tqp_pat_pros;
                                        quePatient.tqp_pat_nsure = quePatient_ques.tqp_pat_nsure;
                                        quePatient.tqp_illness_others = quePatient_ques.tqp_illness_others;
                                        quePatient.tqp_ill_cur_med = quePatient_ques.tqp_ill_cur_med;
                                        quePatient.tqp_ill_cmed_diab = quePatient_ques.tqp_ill_cmed_diab;
                                        quePatient.tqp_ill_cmed_hyper = quePatient_ques.tqp_ill_cmed_hyper;
                                        quePatient.tqp_ill_cmed_demia = quePatient_ques.tqp_ill_cmed_demia;
                                        quePatient.tqp_ill_cmed_cardi = quePatient_ques.tqp_ill_cmed_cardi;
                                        quePatient.tqp_ill_cmed_dysl = quePatient_ques.tqp_ill_cmed_dysl;
                                        quePatient.tqp_ill_cmed_horm = quePatient_ques.tqp_ill_cmed_horm;
                                        quePatient.tqp_ill_cmed_oth = quePatient_ques.tqp_ill_cmed_oth;
                                        quePatient.tqp_ill_cmed_others = quePatient_ques.tqp_ill_cmed_others;
                                        quePatient.tqp_ill_allergy = quePatient_ques.tqp_ill_allergy;
                                        quePatient.tqp_ill_drug_or_food = quePatient_ques.tqp_ill_drug_or_food;
                                        quePatient.tqp_pill_adm = quePatient_ques.tqp_pill_adm;
                                        quePatient.tqp_pill_admission = quePatient_ques.tqp_pill_admission;
                                        quePatient.tqp_pill_sur = quePatient_ques.tqp_pill_sur;
                                        quePatient.tqp_pill_surgery = quePatient_ques.tqp_pill_surgery;
                                        quePatient.tqp_vinf_hepB_virus = quePatient_ques.tqp_vinf_hepB_virus;
                                        quePatient.tqp_vinf_hepA_virus = quePatient_ques.tqp_vinf_hepA_virus;
                                        quePatient.tqp_vinf_vaccine = quePatient_ques.tqp_vinf_vaccine;
                                        quePatient.tqp_fhis_f_disease = quePatient_ques.tqp_fhis_f_disease;
                                        quePatient.tqp_fhis_fdis_hyper = quePatient_ques.tqp_fhis_fdis_hyper;
                                        quePatient.tqp_fhis_fdis_heart = quePatient_ques.tqp_fhis_fdis_heart;
                                        quePatient.tqp_fhis_fdis_diab = quePatient_ques.tqp_fhis_fdis_diab;
                                        quePatient.tqp_fhis_fdis_coro = quePatient_ques.tqp_fhis_fdis_coro;
                                        quePatient.tqp_fhis_fdis_coro_cs = quePatient_ques.tqp_fhis_fdis_coro_cs;
                                        quePatient.tqp_fhis_fdis_dysl = quePatient_ques.tqp_fhis_fdis_dysl;
                                        quePatient.tqp_fhis_fdis_gout = quePatient_ques.tqp_fhis_fdis_gout;
                                        quePatient.tqp_fhis_fdis_pulm = quePatient_ques.tqp_fhis_fdis_pulm;
                                        quePatient.tqp_fhis_fdis_para = quePatient_ques.tqp_fhis_fdis_para;
                                        quePatient.tqp_fhis_fdis_putb = quePatient_ques.tqp_fhis_fdis_putb;
                                        quePatient.tqp_fhis_fdis_stro = quePatient_ques.tqp_fhis_fdis_stro;
                                        quePatient.tqp_fhis_fdis_pepu = quePatient_ques.tqp_fhis_fdis_pepu;
                                        quePatient.tqp_fhis_fdis_asth = quePatient_ques.tqp_fhis_fdis_asth;
                                        quePatient.tqp_fhis_fdis_alle = quePatient_ques.tqp_fhis_fdis_alle;
                                        quePatient.tqp_fhis_fdis_canc = quePatient_ques.tqp_fhis_fdis_canc;
                                        quePatient.tqp_fhis_fdis_canc_rmk = quePatient_ques.tqp_fhis_fdis_canc_rmk;
                                        quePatient.tqp_fhis_fdis_oth = quePatient_ques.tqp_fhis_fdis_oth;
                                        quePatient.tqp_fhis_fdis_others = quePatient_ques.tqp_fhis_fdis_others;
                                        quePatient.tqp_fhis_m_disease = quePatient_ques.tqp_fhis_m_disease;
                                        quePatient.tqp_fhis_mdis_hyper = quePatient_ques.tqp_fhis_mdis_hyper;
                                        quePatient.tqp_fhis_mdis_heart = quePatient_ques.tqp_fhis_mdis_heart;
                                        quePatient.tqp_fhis_mdis_diab = quePatient_ques.tqp_fhis_mdis_diab;
                                        quePatient.tqp_fhis_mdis_coro = quePatient_ques.tqp_fhis_mdis_coro;
                                        quePatient.tqp_fhis_mdis_coro_cs = quePatient_ques.tqp_fhis_mdis_coro_cs;
                                        quePatient.tqp_fhis_mdis_dysl = quePatient_ques.tqp_fhis_mdis_dysl;
                                        quePatient.tqp_fhis_mdis_gout = quePatient_ques.tqp_fhis_mdis_gout;
                                        quePatient.tqp_fhis_mdis_pulm = quePatient_ques.tqp_fhis_mdis_pulm;
                                        quePatient.tqp_fhis_mdis_para = quePatient_ques.tqp_fhis_mdis_para;
                                        quePatient.tqp_fhis_mdis_putb = quePatient_ques.tqp_fhis_mdis_putb;
                                        quePatient.tqp_fhis_mdis_stro = quePatient_ques.tqp_fhis_mdis_stro;
                                        quePatient.tqp_fhis_mdis_pepu = quePatient_ques.tqp_fhis_mdis_pepu;
                                        quePatient.tqp_fhis_mdis_asth = quePatient_ques.tqp_fhis_mdis_asth;
                                        quePatient.tqp_fhis_mdis_alle = quePatient_ques.tqp_fhis_mdis_alle;
                                        quePatient.tqp_fhis_mdis_canc = quePatient_ques.tqp_fhis_mdis_canc;
                                        quePatient.tqp_fhis_mdis_canc_rmk = quePatient_ques.tqp_fhis_mdis_canc_rmk;
                                        quePatient.tqp_fhis_mdis_oth = quePatient_ques.tqp_fhis_mdis_oth;
                                        quePatient.tqp_fhis_mdis_others = quePatient_ques.tqp_fhis_mdis_others;
                                        quePatient.tqp_fhis_b_disease = quePatient_ques.tqp_fhis_b_disease;
                                        quePatient.tqp_fhis_bdis_hyper = quePatient_ques.tqp_fhis_bdis_hyper;
                                        quePatient.tqp_fhis_bdis_heart = quePatient_ques.tqp_fhis_bdis_heart;
                                        quePatient.tqp_fhis_bdis_diab = quePatient_ques.tqp_fhis_bdis_diab;
                                        quePatient.tqp_fhis_bdis_coro = quePatient_ques.tqp_fhis_bdis_coro;
                                        quePatient.tqp_fhis_bdis_coro_cs = quePatient_ques.tqp_fhis_bdis_coro_cs;
                                        quePatient.tqp_fhis_bdis_coro_bfm = quePatient_ques.tqp_fhis_bdis_coro_bfm;
                                        quePatient.tqp_fhis_bdis_coro_afm = quePatient_ques.tqp_fhis_bdis_coro_afm;
                                        quePatient.tqp_fhis_bdis_coro_nfm = quePatient_ques.tqp_fhis_bdis_coro_nfm;
                                        quePatient.tqp_fhis_bdis_coro_bm = quePatient_ques.tqp_fhis_bdis_coro_bm;
                                        quePatient.tqp_fhis_bdis_coro_am = quePatient_ques.tqp_fhis_bdis_coro_am;
                                        quePatient.tqp_fhis_bdis_coro_nm = quePatient_ques.tqp_fhis_bdis_coro_nm;
                                        quePatient.tqp_fhis_bdis_dysl = quePatient_ques.tqp_fhis_bdis_dysl;
                                        quePatient.tqp_fhis_bdis_gout = quePatient_ques.tqp_fhis_bdis_gout;
                                        quePatient.tqp_fhis_bdis_pulm = quePatient_ques.tqp_fhis_bdis_pulm;
                                        quePatient.tqp_fhis_bdis_para = quePatient_ques.tqp_fhis_bdis_para;
                                        quePatient.tqp_fhis_bdis_putb = quePatient_ques.tqp_fhis_bdis_putb;
                                        quePatient.tqp_fhis_bdis_stro = quePatient_ques.tqp_fhis_bdis_stro;
                                        quePatient.tqp_fhis_bdis_pepu = quePatient_ques.tqp_fhis_bdis_pepu;
                                        quePatient.tqp_fhis_bdis_asth = quePatient_ques.tqp_fhis_bdis_asth;
                                        quePatient.tqp_fhis_bdis_alle = quePatient_ques.tqp_fhis_bdis_alle;
                                        quePatient.tqp_fhis_bdis_canc = quePatient_ques.tqp_fhis_bdis_canc;
                                        quePatient.tqp_fhis_bdis_canc_rmk = quePatient_ques.tqp_fhis_bdis_canc_rmk;
                                        quePatient.tqp_fhis_bdis_oth = quePatient_ques.tqp_fhis_bdis_oth;
                                        quePatient.tqp_fhis_bdis_others = quePatient_ques.tqp_fhis_bdis_others;
                                        quePatient.tqp_fhis_others = quePatient_ques.tqp_fhis_others;
                                        quePatient.tqp_fwm_menopause = quePatient_ques.tqp_fwm_menopause;
                                        quePatient.tqp_fwm_meno_start = quePatient_ques.tqp_fwm_meno_start;
                                        quePatient.tqp_fwm_lst_st_mens = quePatient_ques.tqp_fwm_lst_st_mens;
                                        quePatient.tqp_fwm_lst_ed_mens = quePatient_ques.tqp_fwm_lst_ed_mens;
                                        quePatient.tqp_fwm_character = quePatient_ques.tqp_fwm_character;
                                        quePatient.tqp_fwm_pregnancy = quePatient_ques.tqp_fwm_pregnancy;
                                        quePatient.tqp_fwm_over_weight = quePatient_ques.tqp_fwm_over_weight;
                                        quePatient.tqp_symp_faint = quePatient_ques.tqp_symp_faint;
                                        quePatient.tqp_symp_shake = quePatient_ques.tqp_symp_shake;
                                        quePatient.tqp_symp_wind = quePatient_ques.tqp_symp_wind;
                                        quePatient.tqp_symp_breath = quePatient_ques.tqp_symp_breath;
                                        quePatient.tqp_symp_vein = quePatient_ques.tqp_symp_vein;
                                        quePatient.tqp_symp_paralysis = quePatient_ques.tqp_symp_paralysis;
                                        quePatient.tqp_signature = quePatient_ques.tqp_signature;
                                        quePatient.tqp_vinf_hepA_immu = quePatient_ques.tqp_vinf_hepA_immu;
                                        quePatient.tqp_vinf_hepA_vac = quePatient_ques.tqp_vinf_hepA_vac;
                                        quePatient.tqp_vinf_hepB_immu = quePatient_ques.tqp_vinf_hepB_immu;
                                        quePatient.tqp_vinf_hepB_vac = quePatient_ques.tqp_vinf_hepB_vac;
                                        quePatient.tqp_his_nsmok_yrs_text = quePatient_ques.tqp_his_nsmok_yrs_text;
                                        quePatient.tqp_his_qsmok_yrs_text = quePatient_ques.tqp_his_qsmok_yrs_text;
                                        quePatient.tqp_his_smok_amt_text = quePatient_ques.tqp_his_smok_amt_text;
                                        quePatient.tqp_his_smok_dur_text = quePatient_ques.tqp_his_smok_dur_text;
                                        quePatient.tqp_his_alco_yrs_text = quePatient_ques.tqp_his_alco_yrs_text;   
                                        quePatient.tqp_create_by = username;
                                        quePatient.tqp_create_date = Program.GetServerDateTime();
                                        quePatient.tqp_update_date = Program.GetServerDateTime();
                                        quePatient.tqp_address = PatientRegis.tpr_other_address;
                                        quePatient.tqp_tumbon = PatientRegis.tpr_other_tumbon;
                                        quePatient.tqp_aumphur = PatientRegis.tpr_other_amphur;
                                        quePatient.tqp_province = PatientRegis.tpr_other_province;
                                        quePatient.tqp_zip_code = PatientRegis.tpr_other_zip_code;
                                        quePatient.tqp_mobile = PatientRegis.tpr_mobile_phone;
                                        PatientRegis.trn_ques_patients.Add(quePatient);
                                       
                                      
                                    }
                                    else
                                    {
                                        quePatient = new trn_ques_patient(); 
                                        quePatient.tqp_create_by = username;
                                        quePatient.tqp_create_date = Program.GetServerDateTime();
                                        quePatient.tqp_address = PatientRegis.tpr_other_address;
                                        quePatient.tqp_tumbon = PatientRegis.tpr_other_tumbon;
                                        quePatient.tqp_aumphur = PatientRegis.tpr_other_amphur;
                                        quePatient.tqp_province = PatientRegis.tpr_other_province;
                                        quePatient.tqp_zip_code = PatientRegis.tpr_other_zip_code;
                                        quePatient.tqp_mobile = PatientRegis.tpr_mobile_phone;
                                        PatientRegis.trn_ques_patients.Add(quePatient);
                                        // dbc.SubmitChanges();
                                    }
                                   

                                }
                                else
                                {
                                    quePatient = new trn_ques_patient(); 
                                    quePatient.tqp_create_by = username;
                                    quePatient.tqp_create_date = Program.GetServerDateTime();
                                    quePatient.tqp_address = PatientRegis.tpr_other_address;
                                    quePatient.tqp_tumbon = PatientRegis.tpr_other_tumbon;
                                    quePatient.tqp_aumphur = PatientRegis.tpr_other_amphur;
                                    quePatient.tqp_province = PatientRegis.tpr_other_province;
                                    quePatient.tqp_zip_code = PatientRegis.tpr_other_zip_code;
                                    quePatient.tqp_mobile = PatientRegis.tpr_mobile_phone;
                                    PatientRegis.trn_ques_patients.Add(quePatient);

                                }
                                //---> end


                            }
                           
                            
                            
                            //quePatient.PropertyChanged += OnPropertyChanged;
                            quePatient.tqp_update_by = username;
                            quePatient.tqp_update_date = Program.GetServerDateTime();

                            if (PatientRegis.trn_patient.tpt_gender == 'M')
                            {
                                panelWomen.Enabled = false;
                            }

                            bsPatientRegis.DataSource = PatientRegis;
                            quePatient.PropertyChanged += OnPropertyChanged;
                            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(quePatient))
                            {
                                OnPropertyChanged(quePatient, new PropertyChangedEventArgs(property.Name));
                            }
                            this.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            Clear();
                            Program.MessageError(this.Name, "setProp tpr_patient_regis", ex, false);
                        }
                    }
                    patientProfileHorizontalUC1.tpr_id = value;
                    _tpr_id = value;
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            patientProfileHorizontalUC1.tpr_id = null;
            _tpr_id = null;
        }

        private void QuestionnaireFrm_Load(object sender, EventArgs e)
        {
            this.Text = PrePareData.StaticDataCls.ProjectName + "- Questionnaire";
        }
        private void QuestionnaireFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsSaveComplete == true) { DialogResult = System.Windows.Forms.DialogResult.Yes; } else { DialogResult = System.Windows.Forms.DialogResult.No; }
        }

        private void Nextab_Click(object sender, EventArgs e)
        {
            Button btnNextTab = (Button)sender;

            switch ((string)btnNextTab.Tag)
            {
                case "1":

                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                    break;
                case "2":

                    tabControl1.SelectedTab = tabControl1.TabPages[2];
                    break;
                case "3":

                    tabControl1.SelectedTab = tabControl1.TabPages[3];
                    break;
                case "4":

                    tabControl1.SelectedTab = tabControl1.TabPages[4];
                    break;
                case "5":

                    tabControl1.SelectedTab = tabControl1.TabPages[5];
                    break;
            }
        }
        private void Pretab_Click(object sender, EventArgs e)
        {
            Button btnPreTab = (Button)sender;
            switch ((string)btnPreTab.Tag)
            {
                case "1":
                    tabControl1.SelectedTab = tabControl1.TabPages[0];
                    break;
                case "2":
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                    break;
                case "3":
                    tabControl1.SelectedTab = tabControl1.TabPages[2];
                    break;
                case "4":
                    tabControl1.SelectedTab = tabControl1.TabPages[3];
                    break;
                case "5":
                    tabControl1.SelectedTab = tabControl1.TabPages[4];
                    break;
            }
        }
        private void btnNextKeyDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Nextab_Click(sender, e);
            }
        }
        private void btnPreKeyDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Pretab_Click(sender, e);
            }
        }
        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            if (CheckDate() == false)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "วันที่สิ้นสุดน้อยกว่าวันที่เริ่มต้น กรุณาแก้ไขใหม่.";
                return;
            }

            int tpr_id = 0;
            if (Program.CurrentRegis != null)
            {
                tpr_id = Program.CurrentRegis.tpr_id;
                if (Program.chkBookComplete(tpr_id))
                {
                    this.Save('D');
                    lblMsg.Text = "Save as Draft Complete";
                    lblMsg.Visible = true;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckDate() == false)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "วันที่เริ่มต้นต้องน้อยกว่าวันที่สิ้นสุด กรุณาแก้ไขใหม่.";
                return;
            }

            this.Save('N');
            lblMsg.Text = "Save Complete";
            lblMsg.Visible = true;
        }
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            if (radThai.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA201" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.previewReport();
            }
            else if (radEng.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA202" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.previewReport();
            }
            else
            {
                List<string> rptCode = new List<string> { "QA201" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.printReport();
                //ClsReport.printRptEye(1);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (radThai.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA201" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.printReport();
                //ClsReport.printRptEye(1);
            }
            else if (radEng.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA202" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.printReport();
                //ClsReport.printRptEye(1);
            }
            else
            {
                List<string> rptCode = new List<string> { "QA201" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.printReport();
                //ClsReport.printRptEye(1);
            }
        }
        private void btnSendDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.CurrentUser.mut_type.ToString() == "D")
                {
                    string code = "QA201";
                    if (radThai.Checked)
                    {
                        code = "QA201";
                    }
                    else if (radEng.Checked)
                    {
                        code = "QA202";
                    }
                    else
                    {
                        code = "QA201";
                    }

                    string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, code, Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                    lblMsg.Visible = true;
                    lblMsg.Text = result;

                    //if (docscan.SendtoDocscan(code, Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                    //{
                    //    lblMsg.Visible = true;
                    //    lblMsg.Text = "Send To Docscan Completed";

                    //    return;   

                    //}
                    //else
                    //    lblMsg.Text = "Cannot send to docsan user authentication failed";
                }
                else
                    lblMsg.Text = "Cannot send to docscan";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void Save(char docType)
        {
            if (docType == 'N')
            {
                bsQuesPatient.OfType<trn_ques_patient>().FirstOrDefault().tqp_confirm_doctor = Program.CurrentUser.mut_username;
                bsQuesPatient.OfType<trn_ques_patient>().FirstOrDefault().tqp_confirm_date = Program.GetServerDateTime();
                var saw = bsQuesPatient.OfType<trn_ques_patient>().FirstOrDefault().tqp_id; 
            }

            try
            {
                dbc.SubmitChanges();
            }
            catch (System.Data.Linq.ChangeConflictException)
            {
                foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                {
                    dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                }
                dbc.SubmitChanges();
            }
        }

        private bool CheckDate()
        {
            try
            {
                DateTime ds = (DateTime)dtpMemnopauseSt.Value.Date;
                DateTime de = (DateTime)dtpMemnopauseEnd.Value.Date;

                if (ds <= de)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail Load Datetime : " + ex.Message);
            }
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var value = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
            switch (e.PropertyName)
            {
                case "tqp_his_smok":
                    if ((char?)value != 'O') TypeDescriptor.GetProperties(sender)["tqp_his_nsmok_yrs"].SetValue(sender, 0.0);
                    if ((char?)value != 'Q') TypeDescriptor.GetProperties(sender)["tqp_his_qsmok_yrs"].SetValue(sender, 0.0);
                    if ((char?)value != 'S')
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_his_smok_amt"].SetValue(sender, 0.0);
                        TypeDescriptor.GetProperties(sender)["tqp_his_smok_dur"].SetValue(sender, 0.0);
                    }
                    txtNSmoke.Enabled = (char?)value == 'O';
                    txtQuitSmok.Enabled = (char?)value == 'Q';
                    txtSmokAmount.Enabled = (char?)value == 'S';
                    txtSmokDuration.Enabled = (char?)value == 'S';
                    break;
                case "tqp_his_alcohol":
                    if ((char?)value != 'Q') TypeDescriptor.GetProperties(sender)["tqp_his_alco_yrs"].SetValue(sender, 0.0);
                    if ((char?)value != 'S') TypeDescriptor.GetProperties(sender)["tqp_his_alco_social"].SetValue(sender, null);
                    txtQuitdrink.Enabled = (char?)value == 'Q';
                    gbSocialDrink.Enabled = (char?)value == 'S';
                    break;
                case "tqp_ill_concern":
                    if ((char?)value != 'O') TypeDescriptor.GetProperties(sender)["tqp_ill_conc_oth"].SetValue(sender, null);
                    txtPersentOther.Enabled = (char?)value == 'O';
                    break;
                case "tqp_ill_psycho":
                    if ((char?)value != 'O') TypeDescriptor.GetProperties(sender)["tqp_ill_psycho_oth"].SetValue(sender, null);
                    txtPsycOther.Enabled = (char?)value == 'O';
                    break;
                case "tqp_ill_med_his":
                    if ((char?)value != 'D')
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_hyper"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_heart"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_diab"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_coro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_dysl"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_cper"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_gout"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_abdd"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_pulm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_para"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_putb"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_stro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_kidn"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_epil"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_resp"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_sist"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_alle"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_oth"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_sist"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_pept"].SetValue(sender, false);
                    }
                    grbMedicalHis.Enabled = (char?)value == 'D';
                    break;
                case "tqp_ill_med_resp":
                    if ((bool?)value != true)
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_asth"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_emph"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_med_chro"].SetValue(sender, false);
                    }
                    groupBoxRespiratorytractdisease.Enabled = (bool?)value == true;
                    break;
                case "tqp_ill_med_canc":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_ill_med_canc_oth"].SetValue(sender, null);
                    txtRemarkCancer.Enabled = (bool?)value == true;
                    break;
                case "tqp_ill_med_oth":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_ill_med_others"].SetValue(sender, null);
                    txt_med_other.Enabled = (bool?)value == true;
                    break;
                case "tqp_ill_cur_med":
                    if ((char?)value != 'H')
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_diab"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_hyper"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_demia"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_cardi"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_dysl"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_horm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_oth"].SetValue(sender, false);
                    }
                    grbCurrentMedic.Enabled = (char?)value == 'H';
                    break;
                case "tqp_ill_cmed_oth":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_ill_cmed_others"].SetValue(sender, null);
                    txt_cur_other.Enabled = (bool?)value == true;
                    break;
                case "tqp_ill_allergy":
                    if ((char?)value != 'A') TypeDescriptor.GetProperties(sender)["tqp_ill_drug_or_food"].SetValue(sender, null);
                    txtremarkdrugorfood.Enabled = (char?)value == 'A';
                    break;
                case "tqp_pill_adm":
                    if ((char?)value != 'Y') TypeDescriptor.GetProperties(sender)["tqp_pill_admission"].SetValue(sender, null);
                    txtadmis.Enabled = (char?)value == 'Y';
                    break;
                case "tqp_pill_sur":
                    if ((char?)value != 'Y') TypeDescriptor.GetProperties(sender)["tqp_pill_surgery"].SetValue(sender, null);
                    txtsurgary.Enabled = (char?)value == 'Y';
                    break;
                case "tqp_fhis_f_disease":
                    if ((char?)value != 'D')
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_hyper"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_heart"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_diab"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_coro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_dysl"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_gout"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_pulm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_para"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_putb"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_stro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_pepu"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_asth"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_alle"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_canc"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_oth"].SetValue(sender, false);
                    }
                    grbFDisease.Enabled = (char?)value == 'D';
                    break;
                case "tqp_fhis_fdis_coro":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_coro_cs"].SetValue(sender, null);
                    pnl_fdis_coro.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_fdis_canc":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_canc_rmk"].SetValue(sender, null);
                    txt_f_canc.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_fdis_oth":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_fdis_others"].SetValue(sender, null);
                    txt_f_other.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_m_disease":
                    if ((char?)value != 'D')
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_hyper"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_heart"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_diab"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_coro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_dysl"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_gout"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_pulm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_para"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_putb"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_stro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_pepu"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_asth"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_alle"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_canc"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_oth"].SetValue(sender, false);
                    }
                    grbMDisease.Enabled = (char?)value == 'D';
                    break;
                case "tqp_fhis_mdis_coro":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_coro_cs"].SetValue(sender, null);
                    pnl_mdis_coro.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_mdis_canc":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_canc_rmk"].SetValue(sender, null);
                    txt_m_canc.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_mdis_oth":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_mdis_others"].SetValue(sender, null);
                    txt_m_other.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_b_disease":
                    if ((char?)value != 'D')
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_hyper"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_heart"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_diab"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_coro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_dysl"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_gout"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_pulm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_para"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_putb"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_stro"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_pepu"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_asth"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_alle"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_canc"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_oth"].SetValue(sender, false);
                    }
                    grbBDisease.Enabled = (char?)value == 'D';
                    break;
                case "tqp_fhis_bdis_coro":
                    if ((bool?)value != true)
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_coro_bfm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_coro_afm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_coro_nfm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_coro_bm"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_coro_am"].SetValue(sender, false);
                        TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_coro_nm"].SetValue(sender, false);
                    }
                    pnlBCHD.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_bdis_canc":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_canc_rmk"].SetValue(sender, null);
                    txt_b_canc.Enabled = (bool?)value == true;
                    break;
                case "tqp_fhis_bdis_oth":
                    if ((bool?)value != true) TypeDescriptor.GetProperties(sender)["tqp_fhis_bdis_others"].SetValue(sender, null);
                    txt_b_other.Enabled = (bool?)value == true;
                    break;
                case "tqp_fwm_menopause":
                    if ((bool?)value == true)
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_fwm_meno_start"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tqp_fwm_character"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tqp_fwm_pregnancy"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tqp_fwm_over_weight"].SetValue(sender, null);
                        pnlMenopause.Enabled = false;
                        pnlCharacteristic.Enabled = false;
                        pnlPregnancy.Enabled = false;
                        pnlweightover.Enabled = false;
                    }
                    else
                    {
                        pnlMenopause.Enabled = true;
                        pnlCharacteristic.Enabled = true;
                        pnlPregnancy.Enabled = true;
                        pnlweightover.Enabled = true;
                    }
                    break;
                case "tqp_fwm_meno_start":
                    dtpMemnopauseSt.Enabled = (char?)value == 'Y';
                    dtpMemnopauseEnd.Enabled = (char?)value == 'Y';
                    break;
            }
        }
    }
}

