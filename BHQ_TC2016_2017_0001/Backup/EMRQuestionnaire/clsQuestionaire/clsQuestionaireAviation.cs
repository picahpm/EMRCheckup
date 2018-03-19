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
    public class clsQuestionaireAviation
    {
        private string _p_HN;
        public string P_HN
        {
            get { return _p_HN; }
            set { _p_HN = value; }
        }
        private char? _tqa_type;
        public char? tqa_type
        {
            get { return _tqa_type; }
            set { _tqa_type = value; }
        }
        
        private char? _tqa_doc_type;
        public char? tqa_doc_type
        {
            get { return _tqa_doc_type; }
            set { _tqa_doc_type = value; }
        }

        private string _tqa_confirm_doctor;
        public string tqa_confirm_doctor
        {
            get { return _tqa_confirm_doctor; }
            set { _tqa_confirm_doctor = value; }
        }

        private char? _tqa_avia_type;
        public char? tqa_avia_type
        {
            get { return _tqa_avia_type; }
            set { _tqa_avia_type = value; }
        }

        private string _tqa_avia_oths;
        public string tqa_avia_oths
        {
            get { return _tqa_avia_oths; }
            set { _tqa_avia_oths = value;}
        }

        private string _tqa_place_exam;
        public string tqa_place_exam
        {
            get { return _tqa_place_exam; }
            set { _tqa_place_exam = value; }
        }

        private string _tqa_th_fullname;
        public string tqa_th_fullname
        {
            get { return _tqa_th_fullname; }
            set { _tqa_th_fullname = value; }
        }

        private string _tqa_th_nation;
        public string tqa_th_nation
        {
            get { return _tqa_th_nation; }
            set { _tqa_th_nation = value; }
        }

        private string _tqa_en_fullname;
        public string tqa_en_fullname
        {
            get { return _tqa_en_fullname; }
            set { _tqa_en_fullname = value; }
        }

        private string _tqa_en_nation;
        public string tqa_en_nation
        {
            get { return _tqa_en_nation; }
            set { _tqa_en_nation = value; }
        }
        
        private char? _tqa_sex;
        public char? tqa_sex
        {
            get { return _tqa_sex; }
            set { _tqa_sex = value; }
        }
      public string tqa_dob {get; set;}
      public float? tqa_age_yrs {get; set;}
      public float? tqa_age_month {get; set;}
      public char? tqa_marital {get; set;}
      public char? tqa_license_type {get; set;}
      public string  tqa_license_no {get; set;}
      public char? tqa_chge_address {get; set;}
      public string tqa_th_address {get; set;}
      public string tqa_th_moblie {get; set;}
      public string tqa_en_address {get; set;}
      public string tqa_en_mobile {get; set;}
      public string tqa_th_occupa {get; set;}
      public string tqa_th_comp {get; set;}
      public string tqa_en_occupa {get; set;}
      public string tqa_en_comp {get; set;}
      public string tqa_th_office {get; set;}
      public string tqa_th_of_mobile {get; set;}
      public string tqa_en_office {get; set;}
      public string tqa_en_of_mobile {get; set;}
      public char? tqa_cont_address {get; set;}
      public string tqa_person_emer {get; set;}
      public string tqa_telep_emer {get; set;}
      public char? tqa_prev_examined {get; set;}
      public string tqa_prev_exam_loc {get; set;}
      public string tqa_prev_exam_date {get; set;}
      public char? tqa_prev_exam_deca {get; set;}
      public char? tqa_med_waiver {get; set;}
      public string tqa_waiver_spec {get; set;}
      public float? tqa_tot_fling_time {get; set;}
      public float? tqa_last_six_time {get; set;}
      public string tqa_pres_aircraft {get; set;}
      public char? tqa_aircraft_type {get; set;}
      public string tqa_aircraft_name {get; set;}
      public bool? tqa_aircraft_jet {get; set;}
      public bool? tqa_aircraft_turbo {get; set;}
      public bool? tqa_aircraft_heli {get; set;}
      public bool? tqa_aircraft_piston {get; set;}
      public bool? tqa_aircraft_other {get; set;}
      public string tqa_aircraft_oth {get; set;}
      public char? tqa_flying_status {get; set;}
      public bool? tqa_single_pilot {get; set;}
      public bool? tqa_muti_pilot {get; set;}
      public char? tqa_smoking {get; set;}
      public string tqa_smoking_since {get; set;}
      public string tqa_smoking_type {get; set;}
      public string tqa_smoking_amt {get; set;}
      public char? tqa_use_medicine {get; set;}
      public string tqa_med_name {get; set;}
      public string tqa_med_amount {get; set;}
      public string tqa_med_startdate {get; set;}
      public string tqa_med_reason {get; set;}      
      public string tqa_avg_alcohal {get; set;}
      public char? tqa_m20_exercise {get; set;}
      public char? tqa_chis_freq {get; set;}
      public string tqa_chis_freq_rmk {get; set;}
      public char? tqa_chis_dizz {get; set;}
      public string tqa_chis_dizz_rmk {get; set;}
      public char? tqa_chis_unco {get; set;}
      public string tqa_chis_unco_rmk {get; set;}
      public char? tqa_chis_eyet {get; set;}
      public string tqa_chis_eyet_rmk {get; set;} 
      public char? tqa_chis_hayf {get; set;}
      public string tqa_chis_hayf_rmk {get; set;} 
      public char? tqa_chis_hert {get; set;}
      public string tqa_chis_hert_rmk {get; set;} 
      public char? tqa_chis_chst {get; set;}
      public string tqa_chis_chst_rmk {get; set;} 
      public char? tqa_chis_high {get; set;}
      public string tqa_chis_high_rmk {get; set;} 
      public char? tqa_chis_stom {get; set;}
      public string tqa_chis_stom_rmk {get; set;} 
      public char? tqa_chis_jaun {get; set;}
      public string tqa_chis_jaun_rmk {get; set;} 
      public char? tqa_chis_kidn {get; set;}
      public string tqa_chis_kidn_rmk {get; set;} 
      public char? tqa_chis_suga {get; set;}
      public string tqa_chis_suga_rmk {get; set;} 
      public char? tqa_chis_epil {get; set;}
      public string tqa_chis_epil_rmk {get; set;} 
      public char? tqa_chis_nurv {get; set;}
      public string tqa_chis_nurv_rmk {get; set;} 
      public char? tqa_chis_temp {get; set;}
      public string tqa_chis_temp_rmk {get; set;} 
      public char? tqa_chis_drug {get; set;}
      public string tqa_chis_drug_rmk {get; set;} 
      public char? tqa_chis_suic {get; set;}
      public string tqa_chis_suic_rmk {get; set;} 
      public char? tqa_chis_losw {get; set;}
      public string tqa_chis_losw_rmk {get; set;} 
      public char? tqa_chis_moti {get; set;}
      public string tqa_chis_moti_rmk {get; set;} 
      public char? tqa_chis_reje {get; set;}
      public string tqa_chis_reje_rmk {get; set;} 
      public char? tqa_chis_adms {get; set;}
      public string tqa_chis_adms_rmk {get; set;} 
      public char? tqa_chis_avia {get; set;}
      public string tqa_chis_avia_rmk {get; set;} 
      public char? tqa_chis_otha {get; set;}
      public string tqa_chis_otha_rmk {get; set;} 
      public char? tqa_chis_gyna {get; set;}
      public string tqa_chis_gyna_rmk {get; set;} 
      public char? tqa_chis_othi {get; set;}
      public string tqa_chis_othi_rmk {get; set;} 
      public char? tqa_chis_heth {get; set;}
      public string tqa_chis_heth_rmk {get; set;} 
      public char? tqa_chis_lung {get; set;}
      public string tqa_chis_lung_rmk {get; set;} 
      public char? tqa_chis_alco {get; set;}
      public string tqa_chis_alco_rmk {get; set;} 
      public char? tqa_chis_ment {get; set;}
      public string tqa_chis_ment_rmk {get; set;} 
      public char? tqa_chis_fam_his {get; set;}
      public bool? tqa_chis_fam_diab {get; set;}
      public bool? tqa_chis_fam_card {get; set;}
      public bool? tqa_chis_fam_ment {get; set;}
      public char? tqa_chis_conviction {get; set;}
      public string tqa_chis_conv_rmk {get; set;}
      public string tqa_remark {get; set;}
      //public string tqa_create_by {get; set;}
      //public DateTime tqa_create_date {get; set;}
      //public string tqa_update_by {get; set;}
      //public DateTime tqa_update_date {get; set;}
    }
}
