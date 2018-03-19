using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace EMRQuestionnaire.clsQuestionaire
{
    public class clsQuestionaireHealthHistory
    {
        private string _p_HN;
        public string P_HN
        {
            get { return _p_HN; }
            set { _p_HN = value; }
        }
        private char _p_type;
        public char P_type
        {
            get { return _p_type; }
            set { _p_type = value; }
        }
        private string _p_confirm_doctor;
        public string P_confirm_doctor
        {
            get { return _p_confirm_doctor; }
            set { _p_confirm_doctor = value; }
        }
        private string _p_confirm_date;

        public string P_confirm_date
        {
            get { return _p_confirm_date; }
            set { _p_confirm_date = value; }
        }

        private char _p_his_smok;

        public char P_his_smok
        {
            get { return _p_his_smok; }
            set { _p_his_smok = value; }
        }
        private double _p_his_nsmok_yrs;

        public double P_his_nsmok_yrs
        {
            get { return _p_his_nsmok_yrs; }
            set { _p_his_nsmok_yrs = value; }
        }
        private double _p_his_qsmok_yrs;

        public double P_his_qsmok_yrs
        {
            get { return _p_his_qsmok_yrs; }
            set { _p_his_qsmok_yrs = value; }
        }
        private double _p_his_smok_amt;

        public double P_his_smok_amt
        {
            get { return _p_his_smok_amt; }
            set { _p_his_smok_amt = value; }
        }
        private double _p_his_smok_dur;

        public double P_his_smok_dur
        {
            get { return _p_his_smok_dur; }
            set { _p_his_smok_dur = value; }
        }

        private double _p_his_smok_remark;

        public double P_his_smok_remark
        {
            get { return _p_his_smok_remark; }
            set { _p_his_smok_remark = value; }
        }

        private char _p_his_alcohol;

        public char P_his_alcohol
        {
            get { return _p_his_alcohol; }
            set { _p_his_alcohol = value; }
        }
        private double _p_his_alco_yrs;

        public double P_his_alco_yrs
        {
            get { return _p_his_alco_yrs; }
            set { _p_his_alco_yrs = value; }
        }
        private char _p_his_alco_social;

        public char P_his_alco_social
        {
            get { return _p_his_alco_social; }
            set { _p_his_alco_social = value; }
        }
        private char _p_his_exercise;

        public char P_his_exercise
        {
            get { return _p_his_exercise; }
            set { _p_his_exercise = value; }
        }
        private char _p_ill_concern;

        public char P_ill_concern
        {
            get { return _p_ill_concern; }
            set { _p_ill_concern = value; }
        }
        private string _p_ill_conc_oth;

        public string P_ill_conc_oth
        {
            get { return _p_ill_conc_oth; }
            set { _p_ill_conc_oth = value; }
        }

        private bool _p_ill_chkup;

        public bool P_ill_chkup
        {
            get { return _p_ill_chkup; }
            set { _p_ill_chkup = value; }
        }

        private char _p_ill_psycho;

        public char P_ill_psycho
        {
            get { return _p_ill_psycho; }
            set { _p_ill_psycho = value; }
        }
        private string _p_ill_psycho_oth;

        public string P_ill_psycho_oth
        {
            get { return _p_ill_psycho_oth; }
            set { _p_ill_psycho_oth = value; }
        }
        private char _p_ill_med_his;

        public char P_ill_med_his
        {
            get { return _p_ill_med_his; }
            set { _p_ill_med_his = value; }
        }
        private bool _p_ill_med_hyper;

        public bool P_ill_med_hyper
        {
            get { return _p_ill_med_hyper; }
            set { _p_ill_med_hyper = value; }
        }
        private bool _p_ill_med_heart;

        public bool P_ill_med_heart
        {
            get { return _p_ill_med_heart; }
            set { _p_ill_med_heart = value; }
        }
        private bool _p_ill_med_heart_txt;

        public bool P_ill_med_heart_txt
        {
            get { return _p_ill_med_heart_txt; }
            set { _p_ill_med_heart_txt = value; }
        }
        private bool _p_ill_med_diab;

        public bool P_ill_med_diab
        {
            get { return _p_ill_med_diab; }
            set { _p_ill_med_diab = value; }
        }
        private bool _p_ill_med_coro;

        public bool P_ill_med_coro
        {
            get { return _p_ill_med_coro; }
            set { _p_ill_med_coro = value; }
        }
        private bool _p_ill_med_dysl;

        public bool P_ill_med_dysl
        {
            get { return _p_ill_med_dysl; }
            set { _p_ill_med_dysl = value; }
        }
        private bool _p_ill_med_cper;

        public bool P_ill_med_cper
        {
            get { return _p_ill_med_cper; }
            set { _p_ill_med_cper = value; }
        }
        private bool _p_ill_med_gout;

        public bool P_ill_med_gout
        {
            get { return _p_ill_med_gout; }
            set { _p_ill_med_gout = value; }
        }
        private bool _p_ill_med_abdd;

        public bool P_ill_med_abdd
        {
            get { return _p_ill_med_abdd; }
            set { _p_ill_med_abdd = value; }
        }
        private bool _p_ill_med_pulm;

        public bool P_ill_med_pulm
        {
            get { return _p_ill_med_pulm; }
            set { _p_ill_med_pulm = value; }
        }
        private bool _p_ill_med_para;

        public bool P_ill_med_para
        {
            get { return _p_ill_med_para; }
            set { _p_ill_med_para = value; }
        }
        private bool _p_ill_med_stro;

        public bool P_ill_med_stro
        {
            get { return _p_ill_med_stro; }
            set { _p_ill_med_stro = value; }
        }
        private bool _p_ill_med_putb;

        public bool P_ill_med_putb
        {
            get { return _p_ill_med_putb; }
            set { _p_ill_med_putb = value; }
        }
        private bool _p_ill_med_sist;

        public bool P_ill_med_sist
        {
            get { return _p_ill_med_sist; }
            set { _p_ill_med_sist = value; }
        }
        private bool _p_ill_med_kidn;

        public bool P_ill_med_kidn
        {
            get { return _p_ill_med_kidn; }
            set { _p_ill_med_kidn = value; }
        }
        private bool _p_ill_med_epil;

        public bool P_ill_med_epil
        {
            get { return _p_ill_med_epil; }
            set { _p_ill_med_epil = value; }
        }

        private bool _p_ill_med_hepa;

        public bool P_ill_med_hepa
        {
            get { return _p_ill_med_hepa; }
            set { _p_ill_med_hepa = value; }
        }

        private bool _p_ill_med_resp;

        public bool P_ill_med_resp
        {
            get { return _p_ill_med_resp; }
            set { _p_ill_med_resp = value; }
        }
        private bool _p_ill_med_asth;

        public bool P_ill_med_asth
        {
            get { return _p_ill_med_asth; }
            set { _p_ill_med_asth = value; }
        }
        private bool _p_ill_med_emph;

        public bool P_ill_med_emph
        {
            get { return _p_ill_med_emph; }
            set { _p_ill_med_emph = value; }
        }
        private bool _p_ill_med_chro;

        public bool P_ill_med_chro
        {
            get { return _p_ill_med_chro; }
            set { _p_ill_med_chro = value; }
        }
        private bool _p_ill_med_bron;

        public bool P_ill_med_bron
        {
            get { return _p_ill_med_bron; }
            set { _p_ill_med_bron = value; }
        }

        private bool _p_ill_med_cough;

        public bool P_ill_med_cough
        {
            get { return _p_ill_med_cough; }
            set { _p_ill_med_cough = value; }
        }

        private bool _p_ill_med_rhin;

        public bool P_ill_med_rhin
        {
            get { return _p_ill_med_rhin; }
            set { _p_ill_med_rhin = value; }
        }
        private bool _p_ill_med_canc;

        public bool P_ill_med_canc
        {
            get { return _p_ill_med_canc; }
            set { _p_ill_med_canc = value; }
        }
        private string _p_ill_med_canc_oth;

        public string P_ill_med_canc_oth
        {
            get { return _p_ill_med_canc_oth; }
            set { _p_ill_med_canc_oth = value; }
        }
        private bool _p_ill_med_alle;

        public bool P_ill_med_alle
        {
            get { return _p_ill_med_alle; }
            set { _p_ill_med_alle = value; }
        }
        private bool _p_ill_med_pept;

        public bool P_ill_med_pept
        {
            get { return _p_ill_med_pept; }
            set { _p_ill_med_pept = value; }
        }
        private bool _p_ill_med_oth;

        public bool P_ill_med_oth
        {
            get { return _p_ill_med_oth; }
            set { _p_ill_med_oth = value; }
        }
        private string _p_ill_med_others;

        public string P_ill_med_others
        {
            get { return _p_ill_med_others; }
            set { _p_ill_med_others = value; }
        }
        private bool p_ill_med_rmk;

        public bool P_ill_med_rmk
        {
            get { return p_ill_med_rmk; }
            set { p_ill_med_rmk = value; }
        }

        private string p_ill_med_rmk_oth;

        public string P_ill_med_rmk_oth
        {
            get { return p_ill_med_rmk_oth; }
            set { p_ill_med_rmk_oth = value; }
        }

        //------------------------------------
        private bool _p_fam_med_asth;

        public bool P_fam_med_asth
        {
            get { return _p_fam_med_asth; }
            set { _p_fam_med_asth = value; }
        }
        private bool _p_fam_med_bron;

        public bool P_fam_med_bron
        {
            get { return _p_fam_med_bron; }
            set { _p_fam_med_bron = value; }
        }
        private bool _p_fam_med_alle;

        public bool P_fam_med_alle
        {
            get { return _p_fam_med_alle; }
            set { _p_fam_med_alle = value; }
        }
        private bool _p_fam_med_cough;

        public bool P_fam_med_cough
        {
            get { return _p_fam_med_cough; }
            set { _p_fam_med_cough = value; }
        }
        private bool _p_fam_med_rhin;

        public bool P_fam_med_rhin
        {
            get { return _p_fam_med_rhin; }
            set { _p_fam_med_rhin = value; }
        }
        private bool _p_fam_med_oth;

        public bool P_fam_med_oth
        {
            get { return _p_fam_med_oth; }
            set { _p_fam_med_oth = value; }
        }
        private bool _p_envi_hme_dust;

        public bool P_envi_hme_dust
        {
            get { return _p_envi_hme_dust; }
            set { _p_envi_hme_dust = value; }
        }
        private bool _p_envi_hme_smoke;

        public bool P_envi_hme_smoke
        {
            get { return _p_envi_hme_smoke; }
            set { _p_envi_hme_smoke = value; }
        }
        private bool _p_envi_hme_chem;

        public bool P_envi_hme_chem
        {
            get { return _p_envi_hme_chem; }
            set { _p_envi_hme_chem = value; }
        }
        private bool _p_envi_hme_pollen;

        public bool P_envi_hme_pollen
        {
            get { return _p_envi_hme_pollen; }
            set { _p_envi_hme_pollen = value; }
        }
        private bool _p_envi_hme_pet;

        public bool P_envi_hme_pet
        {
            get { return _p_envi_hme_pet; }
            set { _p_envi_hme_pet = value; }
        }
        private string _p_envi_other;

        public string P_envi_other
        {
            get { return _p_envi_other; }
            set { _p_envi_other = value; }
        }
        private bool _p_envi_hme_other;

        public bool P_envi_hme_other
        {
            get { return _p_envi_hme_other; }
            set { _p_envi_hme_other = value; }
        }
        private bool _p_envi_off_dust;

        public bool P_envi_off_dust
        {
            get { return _p_envi_off_dust; }
            set { _p_envi_off_dust = value; }
        }
        private bool _p_envi_off_smoke;

        public bool P_envi_off_smoke
        {
            get { return _p_envi_off_smoke; }
            set { _p_envi_off_smoke = value; }
        }
        private bool _p_envi_off_chem;

        public bool P_envi_off_chem
        {
            get { return _p_envi_off_chem; }
            set { _p_envi_off_chem = value; }
        }
        private bool _p_envi_off_pollen;

        public bool P_envi_off_pollen
        {
            get { return _p_envi_off_pollen; }
            set { _p_envi_off_pollen = value; }
        }
        private bool _p_envi_off_pet;

        public bool P_envi_off_pet
        {
            get { return _p_envi_off_pet; }
            set { _p_envi_off_pet = value; }
        }
        private bool _p_envi_off_other;

        public bool P_envi_off_other
        {
            get { return _p_envi_off_other; }
            set { _p_envi_off_other = value; }
        }
        private float _p_envi_dur;

        public float P_envi_dur
        {
            get { return _p_envi_dur; }
            set { _p_envi_dur = value; }
        }
        private float _p_envi_yrs;

        public float P_envi_yrs
        {
            get { return _p_envi_yrs; }
            set { _p_envi_yrs = value; }
        }
        private bool _p_cur_ill_cough;

        public bool P_cur_ill_cough
        {
            get { return _p_cur_ill_cough; }
            set { _p_cur_ill_cough = value; }
        }
        private bool _p_cur_ill_wcough;

        public bool P_cur_ill_wcough
        {
            get { return _p_cur_ill_wcough; }
            set { _p_cur_ill_wcough = value; }
        }
        private bool _p_cur_ill_gcough;

        public bool P_cur_ill_gcough
        {
            get { return _p_cur_ill_gcough; }
            set { _p_cur_ill_gcough = value; }
        }
        private bool _p_cur_ill_bcough;

        public bool P_cur_ill_bcough
        {
            get { return _p_cur_ill_bcough; }
            set { _p_cur_ill_bcough = value; }
        }
        private bool _p_cou_per_morn;

        public bool P_cou_per_morn
        {
            get { return _p_cou_per_morn; }
            set { _p_cou_per_morn = value; }
        }
        private bool _p_cou_per_aday;

        public bool P_cou_per_aday
        {
            get { return _p_cou_per_aday; }
            set { _p_cou_per_aday = value; }
        }
        private bool _p_cou_per_night;

        public bool P_cou_per_night
        {
            get { return _p_cou_per_night; }
            set { _p_cou_per_night = value; }
        }
        private bool _p_cou_per_rarely;

        public bool P_cou_per_rarely
        {
            get { return _p_cou_per_rarely; }
            set { _p_cou_per_rarely = value; }
        }
        private bool _p_cou_per_nsure;

        public bool P_cou_per_nsure
        {
            get { return _p_cou_per_nsure; }
            set { _p_cou_per_nsure = value; }
        }
        private bool _p_cur_ill_pant;

        public bool P_cur_ill_pant
        {
            get { return _p_cur_ill_pant; }
            set { _p_cur_ill_pant = value; }
        }
        private bool _p_pat_per_morn;

        public bool P_pat_per_morn
        {
            get { return _p_pat_per_morn; }
            set { _p_pat_per_morn = value; }
        }
        private bool _p_pat_per_aday;

        public bool P_pat_per_aday
        {
            get { return _p_pat_per_aday; }
            set { _p_pat_per_aday = value; }
        }
        private bool _p_pat_per_night;

        public bool P_pat_per_night
        {
            get { return _p_pat_per_night; }
            set { _p_pat_per_night = value; }
        }
        private bool _p_pat_per_rarely;

        public bool P_pat_per_rarely
        {
            get { return _p_pat_per_rarely; }
            set { _p_pat_per_rarely = value; }
        }
        private bool _p_pat_per_nsure;

        public bool P_pat_per_nsure
        {
            get { return _p_pat_per_nsure; }
            set { _p_pat_per_nsure = value; }
        }
        private bool _p_pat_freq;

        public bool P_pat_freq
        {
            get { return _p_pat_freq; }
            set { _p_pat_freq = value; }
        }
        private bool _p_pat_exercise;

        public bool P_pat_exercise
        {
            get { return _p_pat_exercise; }
            set { _p_pat_exercise = value; }
        }
        private bool _p_pat_still;

        public bool P_pat_still
        {
            get { return _p_pat_still; }
            set { _p_pat_still = value; }
        }
        private bool _p_pat_pros;

        public bool P_pat_pros
        {
            get { return _p_pat_pros; }
            set { _p_pat_pros = value; }
        }
        private bool _p_pat_nsure;

        public bool P_pat_nsure
        {
            get { return _p_pat_nsure; }
            set { _p_pat_nsure = value; }
        }
        private string _p_illness_others;

        public string P_illness_others
        {
            get { return _p_illness_others; }
            set { _p_illness_others = value; }
        }

        //------------------------------------

        private char _p_ill_cur_med;

        public char P_ill_cur_med
        {
            get { return _p_ill_cur_med; }
            set { _p_ill_cur_med = value; }
        }
        private bool _p_ill_cmed_diab;

        public bool P_ill_cmed_diab
        {
            get { return _p_ill_cmed_diab; }
            set { _p_ill_cmed_diab = value; }
        }
        private bool _p_ill_cmed_hyper;

        public bool P_ill_cmed_hyper
        {
            get { return _p_ill_cmed_hyper; }
            set { _p_ill_cmed_hyper = value; }
        }
        private bool _p_ill_cmed_demia;

        public bool P_ill_cmed_demia
        {
            get { return _p_ill_cmed_demia; }
            set { _p_ill_cmed_demia = value; }
        }
        private bool _p_ill_cmed_cardi;

        public bool P_ill_cmed_cardi
        {
            get { return _p_ill_cmed_cardi; }
            set { _p_ill_cmed_cardi = value; }
        }
        private bool _p_ill_cmed_dysl;

        public bool P_ill_cmed_dysl
        {
            get { return _p_ill_cmed_dysl; }
            set { _p_ill_cmed_dysl = value; }
        }
        private bool _p_ill_cmed_horm;

        public bool P_ill_cmed_horm
        {
            get { return _p_ill_cmed_horm; }
            set { _p_ill_cmed_horm = value; }
        }
        private bool _p_ill_cmed_oth;

        public bool P_ill_cmed_oth
        {
            get { return _p_ill_cmed_oth; }
            set { _p_ill_cmed_oth = value; }
        }
        private string _p_ill_cmed_others;

        public string P_ill_cmed_others
        {
            get { return _p_ill_cmed_others; }
            set { _p_ill_cmed_others = value; }
        }
        private char _p_ill_allergy;

        public char P_ill_allergy
        {
            get { return _p_ill_allergy; }
            set { _p_ill_allergy = value; }
        }
        private string _p_ill_drug_or_food;

        public string P_ill_drug_or_food
        {
            get { return _p_ill_drug_or_food; }
            set { _p_ill_drug_or_food = value; }
        }
        private char _p_pill_adm;

        public char P_pill_adm
        {
            get { return _p_pill_adm; }
            set { _p_pill_adm = value; }
        }
        private string _p_pill_admission;

        public string P_pill_admission
        {
            get { return _p_pill_admission; }
            set { _p_pill_admission = value; }
        }
        private char _p_pill_sur;

        public char P_pill_sur
        {
            get { return _p_pill_sur; }
            set { _p_pill_sur = value; }
        }
        private string _p_pill_surgery;

        public string P_pill_surgery
        {
            get { return _p_pill_surgery; }
            set { _p_pill_surgery = value; }
        }
        private char _p_vinf_hepB_virus;

        public char P_vinf_hepB_virus
        {
            get { return _p_vinf_hepB_virus; }
            set { _p_vinf_hepB_virus = value; }
        }
        private char _p_vinf_hepA_virus;

        public char P_vinf_hepA_virus
        {
            get { return _p_vinf_hepA_virus; }
            set { _p_vinf_hepA_virus = value; }
        }
        private char _p_vinf_vaccine;

        public char P_vinf_vaccine
        {
            get { return _p_vinf_vaccine; }
            set { _p_vinf_vaccine = value; }
        }
        private char _p_fhis_f_disease;

        public char P_fhis_f_disease
        {
            get { return _p_fhis_f_disease; }
            set { _p_fhis_f_disease = value; }
        }
        private bool _p_fhis_fdis_hyper;

        public bool P_fhis_fdis_hyper
        {
            get { return _p_fhis_fdis_hyper; }
            set { _p_fhis_fdis_hyper = value; }
        }
        private bool _p_fhis_fdis_heart;

        public bool P_fhis_fdis_heart
        {
            get { return _p_fhis_fdis_heart; }
            set { _p_fhis_fdis_heart = value; }
        }
        private bool _p_fhis_fdis_diab;

        public bool P_fhis_fdis_diab
        {
            get { return _p_fhis_fdis_diab; }
            set { _p_fhis_fdis_diab = value; }
        }
        private bool _p_fhis_fdis_coro;

        public bool P_fhis_fdis_coro
        {
            get { return _p_fhis_fdis_coro; }
            set { _p_fhis_fdis_coro = value; }
        }
        private char _p_fhis_fdis_coro_cs;

        public char P_fhis_fdis_coro_cs
        {
            get { return _p_fhis_fdis_coro_cs; }
            set { _p_fhis_fdis_coro_cs = value; }
        }
        private bool _p_fhis_fdis_dysl;

        public bool P_fhis_fdis_dysl
        {
            get { return _p_fhis_fdis_dysl; }
            set { _p_fhis_fdis_dysl = value; }
        }
        private bool _p_fhis_fdis_gout;

        public bool P_fhis_fdis_gout
        {
            get { return _p_fhis_fdis_gout; }
            set { _p_fhis_fdis_gout = value; }
        }
        private bool _p_fhis_fdis_pulm;

        public bool P_fhis_fdis_pulm
        {
            get { return _p_fhis_fdis_pulm; }
            set { _p_fhis_fdis_pulm = value; }
        }
        private bool _p_fhis_fdis_para;

        public bool P_fhis_fdis_para
        {
            get { return _p_fhis_fdis_para; }
            set { _p_fhis_fdis_para = value; }
        }
        private bool _p_fhis_fdis_putb;

        public bool P_fhis_fdis_putb
        {
            get { return _p_fhis_fdis_putb; }
            set { _p_fhis_fdis_putb = value; }
        }
        private bool _p_fhis_fdis_stro;

        public bool P_fhis_fdis_stro
        {
            get { return _p_fhis_fdis_stro; }
            set { _p_fhis_fdis_stro = value; }
        }
        private bool _p_fhis_fdis_pepu;

        public bool P_fhis_fdis_pepu
        {
            get { return _p_fhis_fdis_pepu; }
            set { _p_fhis_fdis_pepu = value; }
        }
        private bool _p_fhis_fdis_asth;

        public bool P_fhis_fdis_asth
        {
            get { return _p_fhis_fdis_asth; }
            set { _p_fhis_fdis_asth = value; }
        }
        private bool _p_fhis_fdis_alle;

        public bool P_fhis_fdis_alle
        {
            get { return _p_fhis_fdis_alle; }
            set { _p_fhis_fdis_alle = value; }
        }
        private bool _p_fhis_fdis_canc;

        public bool P_fhis_fdis_canc
        {
            get { return _p_fhis_fdis_canc; }
            set { _p_fhis_fdis_canc = value; }
        }
        private string _p_fhis_fdis_canc_rmk;

        public string P_fhis_fdis_canc_rmk
        {
            get { return _p_fhis_fdis_canc_rmk; }
            set { _p_fhis_fdis_canc_rmk = value; }
        }
        private bool _p_fhis_fdis_oth;

        public bool P_fhis_fdis_oth
        {
            get { return _p_fhis_fdis_oth; }
            set { _p_fhis_fdis_oth = value; }
        }
        private string _p_fhis_fdis_others;

        public string P_fhis_fdis_others
        {
            get { return _p_fhis_fdis_others; }
            set { _p_fhis_fdis_others = value; }
        }
        private char _p_fhis_m_disease;

        public char P_fhis_m_disease
        {
            get { return _p_fhis_m_disease; }
            set { _p_fhis_m_disease = value; }
        }
        private bool _p_fhis_mdis_hyper;

        public bool P_fhis_mdis_hyper
        {
            get { return _p_fhis_mdis_hyper; }
            set { _p_fhis_mdis_hyper = value; }
        }
        private bool _p_fhis_mdis_heart;

        public bool P_fhis_mdis_heart
        {
            get { return _p_fhis_mdis_heart; }
            set { _p_fhis_mdis_heart = value; }
        }
        private bool _p_fhis_mdis_diab;

        public bool P_fhis_mdis_diab
        {
            get { return _p_fhis_mdis_diab; }
            set { _p_fhis_mdis_diab = value; }
        }
        private bool _p_fhis_mdis_coro;

        public bool P_fhis_mdis_coro
        {
            get { return _p_fhis_mdis_coro; }
            set { _p_fhis_mdis_coro = value; }
        }
        private char _p_fhis_mdis_coro_cs;

        public char P_fhis_mdis_coro_cs
        {
            get { return _p_fhis_mdis_coro_cs; }
            set { _p_fhis_mdis_coro_cs = value; }
        }
        private bool _p_fhis_mdis_dysl;

        public bool P_fhis_mdis_dysl
        {
            get { return _p_fhis_mdis_dysl; }
            set { _p_fhis_mdis_dysl = value; }
        }
        private bool _p_fhis_mdis_gout;

        public bool P_fhis_mdis_gout
        {
            get { return _p_fhis_mdis_gout; }
            set { _p_fhis_mdis_gout = value; }
        }
        private bool _p_fhis_mdis_pulm;

        public bool P_fhis_mdis_pulm
        {
            get { return _p_fhis_mdis_pulm; }
            set { _p_fhis_mdis_pulm = value; }
        }
        private bool _p_fhis_mdis_para;

        public bool P_fhis_mdis_para
        {
            get { return _p_fhis_mdis_para; }
            set { _p_fhis_mdis_para = value; }
        }
        private bool _p_fhis_mdis_putb;

        public bool P_fhis_mdis_putb
        {
            get { return _p_fhis_mdis_putb; }
            set { _p_fhis_mdis_putb = value; }
        }
        private bool _p_fhis_mdis_stro;

        public bool P_fhis_mdis_stro
        {
            get { return _p_fhis_mdis_stro; }
            set { _p_fhis_mdis_stro = value; }
        }
        private bool _p_fhis_mdis_pepu;

        public bool P_fhis_mdis_pepu
        {
            get { return _p_fhis_mdis_pepu; }
            set { _p_fhis_mdis_pepu = value; }
        }
        private bool _p_fhis_mdis_asth;

        public bool P_fhis_mdis_asth
        {
            get { return _p_fhis_mdis_asth; }
            set { _p_fhis_mdis_asth = value; }
        }
        private bool _p_fhis_mdis_alle;

        public bool P_fhis_mdis_alle
        {
            get { return _p_fhis_mdis_alle; }
            set { _p_fhis_mdis_alle = value; }
        }
        private bool _p_fhis_mdis_canc;

        public bool P_fhis_mdis_canc
        {
            get { return _p_fhis_mdis_canc; }
            set { _p_fhis_mdis_canc = value; }
        }
        private string _p_fhis_mdis_canc_rmk;

        public string P_fhis_mdis_canc_rmk
        {
            get { return _p_fhis_mdis_canc_rmk; }
            set { _p_fhis_mdis_canc_rmk = value; }
        }
        private bool _p_fhis_mdis_oth;

        public bool P_fhis_mdis_oth
        {
            get { return _p_fhis_mdis_oth; }
            set { _p_fhis_mdis_oth = value; }
        }
        private string _p_fhis_mdis_others;

        public string P_fhis_mdis_others
        {
            get { return _p_fhis_mdis_others; }
            set { _p_fhis_mdis_others = value; }
        }
        private char _p_fhis_b_disease;

        public char P_fhis_b_disease
        {
            get { return _p_fhis_b_disease; }
            set { _p_fhis_b_disease = value; }
        }
        private bool _p_fhis_bdis_hyper;

        public bool P_fhis_bdis_hyper
        {
            get { return _p_fhis_bdis_hyper; }
            set { _p_fhis_bdis_hyper = value; }
        }
        private bool _p_fhis_bdis_heart;

        public bool P_fhis_bdis_heart
        {
            get { return _p_fhis_bdis_heart; }
            set { _p_fhis_bdis_heart = value; }
        }
        private bool _p_fhis_bdis_diab;

        public bool P_fhis_bdis_diab
        {
            get { return _p_fhis_bdis_diab; }
            set { _p_fhis_bdis_diab = value; }
        }
        private bool _p_fhis_bdis_coro;

        public bool P_fhis_bdis_coro
        {
            get { return _p_fhis_bdis_coro; }
            set { _p_fhis_bdis_coro = value; }
        }

        private bool _p_fhis_bdis_coro_cs;

        public bool P_fhis_bdis_coro_cs
        {
            get { return _p_fhis_bdis_coro_cs; }
            set { _p_fhis_bdis_coro_cs = value; }
        }

        private bool _p_fhis_bdis_coro_bfm;

        public bool P_fhis_bdis_coro_bfm
        {
            get { return _p_fhis_bdis_coro_bfm; }
            set { _p_fhis_bdis_coro_bfm = value; }
        }
        private bool _p_fhis_bdis_coro_afm;

        public bool P_fhis_bdis_coro_afm
        {
            get { return _p_fhis_bdis_coro_afm; }
            set { _p_fhis_bdis_coro_afm = value; }
        }
        private bool _p_fhis_bdis_coro_nfm;

        public bool P_fhis_bdis_coro_nfm
        {
            get { return _p_fhis_bdis_coro_nfm; }
            set { _p_fhis_bdis_coro_nfm = value; }
        }
        private bool _p_fhis_bdis_coro_bm;

        public bool P_fhis_bdis_coro_bm
        {
            get { return _p_fhis_bdis_coro_bm; }
            set { _p_fhis_bdis_coro_bm = value; }
        }
        private bool _p_fhis_bdis_coro_am;

        public bool P_fhis_bdis_coro_am
        {
            get { return _p_fhis_bdis_coro_am; }
            set { _p_fhis_bdis_coro_am = value; }
        }
        private bool _p_fhis_bdis_coro_nm;

        public bool P_fhis_bdis_coro_nm
        {
            get { return _p_fhis_bdis_coro_nm; }
            set { _p_fhis_bdis_coro_nm = value; }
        }
        private bool _p_fhis_bdis_dysl;

        public bool P_fhis_bdis_dysl
        {
            get { return _p_fhis_bdis_dysl; }
            set { _p_fhis_bdis_dysl = value; }
        }
        private bool _p_fhis_bdis_gout;

        public bool P_fhis_bdis_gout
        {
            get { return _p_fhis_bdis_gout; }
            set { _p_fhis_bdis_gout = value; }
        }
        private bool _p_fhis_bdis_pulm;

        public bool P_fhis_bdis_pulm
        {
            get { return _p_fhis_bdis_pulm; }
            set { _p_fhis_bdis_pulm = value; }
        }
        private bool _p_fhis_bdis_para;

        public bool P_fhis_bdis_para
        {
            get { return _p_fhis_bdis_para; }
            set { _p_fhis_bdis_para = value; }
        }
        private bool _p_fhis_bdis_putb;

        public bool P_fhis_bdis_putb
        {
            get { return _p_fhis_bdis_putb; }
            set { _p_fhis_bdis_putb = value; }
        }
        private bool _p_fhis_bdis_stro;

        public bool P_fhis_bdis_stro
        {
            get { return _p_fhis_bdis_stro; }
            set { _p_fhis_bdis_stro = value; }
        }
        private bool _p_fhis_bdis_pepu;

        public bool P_fhis_bdis_pepu
        {
            get { return _p_fhis_bdis_pepu; }
            set { _p_fhis_bdis_pepu = value; }
        }
        private bool _p_fhis_bdis_asth;

        public bool P_fhis_bdis_asth
        {
            get { return _p_fhis_bdis_asth; }
            set { _p_fhis_bdis_asth = value; }
        }
        private bool _p_fhis_bdis_alle;

        public bool P_fhis_bdis_alle
        {
            get { return _p_fhis_bdis_alle; }
            set { _p_fhis_bdis_alle = value; }
        }
        private bool _p_fhis_bdis_canc;

        public bool P_fhis_bdis_canc
        {
            get { return _p_fhis_bdis_canc; }
            set { _p_fhis_bdis_canc = value; }
        }
        private string _p_fhis_bdis_canc_rmk;

        public string P_fhis_bdis_canc_rmk
        {
            get { return _p_fhis_bdis_canc_rmk; }
            set { _p_fhis_bdis_canc_rmk = value; }
        }
        private bool _p_fhis_bdis_oth;

        public bool P_fhis_bdis_oth
        {
            get { return _p_fhis_bdis_oth; }
            set { _p_fhis_bdis_oth = value; }
        }
        private string _p_fhis_bdis_others;

        public string P_fhis_bdis_others
        {
            get { return _p_fhis_bdis_others; }
            set { _p_fhis_bdis_others = value; }
        }
        private string _p_fhis_others;

        public string P_fhis_others
        {
            get { return _p_fhis_others; }
            set { _p_fhis_others = value; }
        }
        private bool _p_fwm_menopause;

        public bool P_fwm_menopause
        {
            get { return _p_fwm_menopause; }
            set { _p_fwm_menopause = value; }
        }
        private char _p_fwm_meno_start;

        public char P_fwm_meno_start
        {
            get { return _p_fwm_meno_start; }
            set { _p_fwm_meno_start = value; }
        }
        private string _p_fwm_lst_st_mens;

        public string P_fwm_lst_st_mens
        {
            get { return _p_fwm_lst_st_mens; }
            set { _p_fwm_lst_st_mens = value; }
        }
        private string _p_fwm_lst_ed_mens;

        public string P_fwm_lst_ed_mens
        {
            get { return _p_fwm_lst_ed_mens; }
            set { _p_fwm_lst_ed_mens = value; }
        }
        private bool _p_fwm_boolacter;

        public bool P_fwm_boolacter
        {
            get { return _p_fwm_boolacter; }
            set { _p_fwm_boolacter = value; }
        }
        private char _p_fwm_pregnancy;

        public char P_fwm_pregnancy
        {
            get { return _p_fwm_pregnancy; }
            set { _p_fwm_pregnancy = value; }
        }
        private char _p_fwm_over_weight;

        public char P_fwm_over_weight
        {
            get { return _p_fwm_over_weight; }
            set { _p_fwm_over_weight = value; }
        }
        private int _p_license;

        public int P_license
        {
            get { return _p_license; }
            set { _p_license = value; }
        }
        private string p_create_by;

        public string P_create_by
        {
            get { return p_create_by; }
            set { p_create_by = value; }
        }
        private string p_create_date;

        public string P_create_date
        {
            get { return p_create_date; }
            set { p_create_date = value; }
        }
        private string p_update_by;

        public string P_update_by
        {
            get { return p_update_by; }
            set { p_update_by = value; }
        }
        private string p_update_date;

        public string P_update_date
        {
            get { return p_update_date; }
            set { p_update_date = value; }
        }

        private bool _p_symp_faint;

        public bool P_symp_faint
        {
            get { return _p_symp_faint; }
            set { _p_symp_faint = value; }
        }
        private bool _p_symp_shake;

        public bool P_symp_shake
        {
            get { return _p_symp_shake; }
            set { _p_symp_shake = value; }
        }
        private bool _p_symp_wind;

        public bool P_symp_wind
        {
            get { return _p_symp_wind; }
            set { _p_symp_wind = value; }
        }
        private bool _p_symp_breath;

        public bool P_symp_breath
        {
            get { return _p_symp_breath; }
            set { _p_symp_breath = value; }
        }
        private bool _p_symp_vein;

        public bool P_symp_vein
        {
            get { return _p_symp_vein; }
            set { _p_symp_vein = value; }
        }
        private bool _p_symp_paralysis;

        public bool P_symp_paralysis
        {
            get { return _p_symp_paralysis; }
            set { _p_symp_paralysis = value; }
        }
        private string _p_address;

        public string P_address
        {
            get { return _p_address; }
            set { _p_address = value; }
        }
        private string _p_tumbon;

        public string P_tumbon
        {
            get { return _p_tumbon; }
            set { _p_tumbon = value; }
        }
        private string _p_aumphur;

        public string P_aumphur
        {
            get { return _p_aumphur; }
            set { _p_aumphur = value; }
        }
        private string _p_province;

        public string P_province
        {
            get { return _p_province; }
            set { _p_province = value; }
        }
        private string _p_zip_code;

        public string P_zip_code
        {
            get { return _p_zip_code; }
            set { _p_zip_code = value; }
        }
        private string _p_mobile;

        public string P_mobile
        {
            get { return _p_mobile; }
            set { _p_mobile = value; }
        }

        private char _p_fwm_character;

        public char P_fwm_character
        {
            get { return _p_fwm_character; }
            set { _p_fwm_character = value; }
        }
    }
}
