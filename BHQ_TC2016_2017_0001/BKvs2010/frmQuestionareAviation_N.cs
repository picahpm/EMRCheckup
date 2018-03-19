using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010
{

    public partial class frmQuestionareAviation_N : Form
    {

        public class ListAirCraft
        {
            public string type { get; set; }
            public string name { get; set; }
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        public frmQuestionareAviation_N()
        {
            InitializeComponent();
        }

        private List<ListAirCraft> _ListAirCraft = new List<ListAirCraft>();

        private void LoadTransaction()
        {
            if (Program.CurrentRegis != null)
            {

                 //find HN
                var objHN1 = (from t1 in dbc.trn_patient_regis join t2 in dbc.trn_patients on t1.tpt_id equals t2.tpt_id where t1.tpt_id == Program.CurrentRegis.tpt_id select new { t1,t2 }).OrderByDescending(c => c.t1.tpr_create_date).FirstOrDefault();

                //EN No Lastest
                string objHN2 = Program.CurrentRegis.tpr_en_no;


                if (objHN1.t1.tpr_en_no == objHN2)
                {
                    var objnew = (from q in dbc.trn_ques_aviations where q.tpr_id == Program.CurrentRegis.tpr_id select q).OrderByDescending(c => c.tqa_update_date).FirstOrDefault();
                    if (objnew != null)
                    {
                        trnquesavaitionBindingSource.DataSource = objnew;
                        trn_ques_aviation CurrentQuestion = (trn_ques_aviation)trnquesavaitionBindingSource.Current;

                        txtplace.Text = CurrentQuestion.tqa_place_exam;


                        //tab 1
                        Program.SetValueRadioGroupBox(grbgender, CurrentQuestion.tqa_sex);
                        Program.SetValueRadioGroup(pnllicensetype, CurrentQuestion.tqa_license_type);
                        Program.SetValueRadioGroupBox(grbaviatype, CurrentQuestion.tqa_avia_type);
                        txtaviation_oth.Text = CurrentQuestion.tqa_avia_oths;
                        Program.SetValueRadioGroup(pnlconadd, CurrentQuestion.tqa_cont_address);
                        Program.SetValueRadioGroup(pnlmarital, CurrentQuestion.tqa_marital);
                        Program.SetValueRadioGroup(pnlprevexam, CurrentQuestion.tqa_prev_examined);
                        Program.SetValueRadioGroup(pnlprev_exam_decl, CurrentQuestion.tqa_prev_exam_deca);
                        Program.SetValueRadioGroup(pnlmed_waiver, CurrentQuestion.tqa_med_waiver);
                        Program.SetValueRadioGroup(pnlaircraft_type, CurrentQuestion.tqa_aircraft_type);
                        Program.SetValueRadioGroup(pnlflying_status, CurrentQuestion.tqa_flying_status);
                        Program.SetValueRadioGroup(pnlsmoking, CurrentQuestion.tqa_smoking);
                        Program.SetValueRadioGroup(pnlexercise, CurrentQuestion.tqa_m20_exercise);
                        Program.SetValueRadioGroup(pnluse_midicine, CurrentQuestion.tqa_use_medicine);

                        if (CurrentQuestion.tqa_prev_exam_date != null)
                        {
                            dtpprev_date.Value =(DateTime) CurrentQuestion.tqa_prev_exam_date;
                        }
                        txtaircraft_name.Text = CurrentQuestion.tqa_aircraft_name;
                        chkjet.Checked = (CurrentQuestion.tqa_aircraft_jet == true) ? true : false;
                        chkturbo_prop.Checked = (CurrentQuestion.tqa_aircraft_turbo == true) ? true : false;
                        chkhelicopter.Checked = (CurrentQuestion.tqa_aircraft_heli == true) ? true : false;
                        chkpiston.Checked = (CurrentQuestion.tqa_aircraft_piston == true) ? true : false;
                        chkoth.Checked = (CurrentQuestion.tqa_aircraft_other == true) ? true : false;
                        txtoth.Text = CurrentQuestion.tqa_aircraft_oth;
                        chksingle_pilot.Checked = (CurrentQuestion.tqa_single_pilot == true) ? true : false;
                        chkmuti_pilot.Checked = (CurrentQuestion.tqa_muti_pilot == true) ? true : false;

                        txtstate_type.Text = CurrentQuestion.tqa_smoking_type;
                        txtsmok_amt.Text = CurrentQuestion.tqa_smoking_amt;
                        txtstoped_since.Text = CurrentQuestion.tqa_smoking_since;

                        txtmed_name.Text = CurrentQuestion.tqa_med_name;
                        txtmed_amt.Text = CurrentQuestion.tqa_med_amount;
                        txtmed_reason.Text = CurrentQuestion.tqa_med_reason;
                        if (CurrentQuestion.tqa_med_startdate != null)
                        {
                            dtpmed_startdate.Value = (DateTime)CurrentQuestion.tqa_med_startdate;
                        }

                        //tab 2
                        Program.SetValueRadioGroup(pnlchis_freq, CurrentQuestion.tqa_chis_freq);
                        Program.SetValueRadioGroup(pnlchis_dizz, CurrentQuestion.tqa_chis_dizz);
                        Program.SetValueRadioGroup(pnlchis_unco, CurrentQuestion.tqa_chis_unco);
                        Program.SetValueRadioGroup(pnlchis_eyes, CurrentQuestion.tqa_chis_eyet);
                        Program.SetValueRadioGroup(pnlchis_hayf, CurrentQuestion.tqa_chis_hayf);
                        Program.SetValueRadioGroup(pnlchis_heart, CurrentQuestion.tqa_chis_hert);
                        Program.SetValueRadioGroup(pnlchis_chest, CurrentQuestion.tqa_chis_chst);
                        Program.SetValueRadioGroup(pnlchis_high, CurrentQuestion.tqa_chis_high);
                        Program.SetValueRadioGroup(pnlchis_stomach, CurrentQuestion.tqa_chis_stom);
                        Program.SetValueRadioGroup(pnlchis_jaun, CurrentQuestion.tqa_chis_jaun);
                        Program.SetValueRadioGroup(pnlchis_kidn, CurrentQuestion.tqa_chis_kidn);
                        Program.SetValueRadioGroup(pnlchis_sugar, CurrentQuestion.tqa_chis_suga);
                        Program.SetValueRadioGroup(pnlepil, CurrentQuestion.tqa_chis_epil);
                        Program.SetValueRadioGroup(pnlchis_nurv, CurrentQuestion.tqa_chis_nurv);
                        Program.SetValueRadioGroup(pnlchis_temp, CurrentQuestion.tqa_chis_temp);
                        Program.SetValueRadioGroup(pnlchis_drug, CurrentQuestion.tqa_chis_drug);
                        Program.SetValueRadioGroup(pnlchis_suic, CurrentQuestion.tqa_chis_suic);
                        Program.SetValueRadioGroup(pnlchis_lows, CurrentQuestion.tqa_chis_losw);
                        Program.SetValueRadioGroup(pnlchis_moti, CurrentQuestion.tqa_chis_moti);
                        Program.SetValueRadioGroup(pnlchis_rej, CurrentQuestion.tqa_chis_reje);
                        Program.SetValueRadioGroup(pnlchis_adm, CurrentQuestion.tqa_chis_adms);
                        Program.SetValueRadioGroup(pnlchis_avia, CurrentQuestion.tqa_chis_avia);
                        Program.SetValueRadioGroup(pnlchis_otha, CurrentQuestion.tqa_chis_otha);
                        Program.SetValueRadioGroup(pnlchis_gyna, CurrentQuestion.tqa_chis_gyna);
                        Program.SetValueRadioGroup(pnlchis_othi, CurrentQuestion.tqa_chis_othi);
                        Program.SetValueRadioGroup(pnlchis_hlht, CurrentQuestion.tqa_chis_heth);
                        //Program.SetValueRadioGroup(pnlchis_fam_his, CurrentQuestion.tqa_chis_fam_his);

                        cbdiabetes.Checked = (CurrentQuestion.tqa_chis_fam_diab == null || CurrentQuestion.tqa_chis_fam_diab == false) ? false : true;
                        cbcardio.Checked = (CurrentQuestion.tqa_chis_fam_card == null || CurrentQuestion.tqa_chis_fam_card == false) ? false : true;
                        cbmental.Checked = (CurrentQuestion.tqa_chis_fam_ment == null || CurrentQuestion.tqa_chis_fam_ment == false) ? false : true;

                        Program.SetValueRadioGroup(pnlconviction, CurrentQuestion.tqa_chis_conviction);


                        txtaddress_en.Text = CurrentQuestion.tqa_en_address;
                        txtaddress_th.Text = CurrentQuestion.tqa_th_address;
                        
                        //txta

                        txtname_th.Text = CurrentQuestion.tqa_th_fullname;
                        txtname_en.Text = CurrentQuestion.tqa_en_fullname;
                        txtnation_th.Text = CurrentQuestion.tqa_th_nation;
                        txtnation_en.Text = CurrentQuestion.tqa_en_nation;

                        if (CurrentQuestion.tqa_dob != null)
                        {
                            lblDOB_th.Text = Program.ConvertDateTimeToThai((DateTime)CurrentQuestion.tqa_dob);
                            lblDOB_en.Text = String.Format("{0:dd/MM/yyyy}", CurrentQuestion.tqa_dob);
                        }
                        else
                        {
                            lblDOB_th.Text = "-";
                            lblDOB_en.Text = "-";
                        }

                        txtageyear_en.Text = CurrentQuestion.tqa_age_yrs.ToString();
                        txtageyear_th.Text = CurrentQuestion.tqa_age_yrs.ToString();
                        txtagemonth_en.Text = CurrentQuestion.tqa_age_month.ToString();
                        txtagemonth_th.Text = CurrentQuestion.tqa_age_month.ToString();



                         textBox24.Text = CurrentQuestion.tqa_tot_fling_time.ToString();
                         textBox25.Text = CurrentQuestion.tqa_last_six_time.ToString();


                    }
                    else
                    {
                        this.LoadNewDefaultData();
                        trnquesavaitionBindingSource.DataSource =(from t1 in dbc.trn_ques_aviations select t1).Take(0);
                        trnquesavaitionBindingSource.AddNew();
                    }
                }
                else
                {
                    this.LoadNewDefaultData();
                    trnquesavaitionBindingSource.DataSource = (from t1 in dbc.trn_ques_aviations select t1).Take(0);
                    trnquesavaitionBindingSource.AddNew();
                }
               
            }
        }

        private void LoadNewDefaultData()
        {
            var objHN1 = (from t1 in dbc.trn_patient_regis join t2 in dbc.trn_patients on t1.tpt_id equals t2.tpt_id where t1.tpt_id == Program.CurrentRegis.tpt_id select new { t1, t2 }).OrderByDescending(c => c.t1.tpr_create_date).FirstOrDefault();
            //Load Default Data
            if (objHN1.t2.tpt_nation_code == "TH")
            {
                string nameEn = (string.IsNullOrEmpty(objHN1.t2.tpt_en_name1) ? "" : objHN1.t2.tpt_en_name1.Trim()) +
                                   (string.IsNullOrEmpty(objHN1.t2.tpt_en_name3) ? "" : " " + objHN1.t2.tpt_en_name3.Trim()) +
                                   (string.IsNullOrEmpty(objHN1.t2.tpt_en_name2) ? "" : " " + objHN1.t2.tpt_en_name2.Trim());
                txtname_th.Text = objHN1.t2.tpt_othername;
                txtname_en.Text = nameEn;
                txtnation_th.Text = objHN1.t2.tpt_nation_desc;
                txtname_en.Text = objHN1.t2.tpt_en_name1 + " " + objHN1.t2.tpt_en_name2;
                txtnation_en.Text = objHN1.t2.tpt_nation_desc;
                Program.SetValueRadioGroupBox(grbgender, objHN1.t2.tpt_gender);
                lblDOB_th.Text = Program.ConvertDateTimeToThai((DateTime)objHN1.t2.tpt_dob);
                lblDOB_en.Text = String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob);

                string address = String.Empty;
                if (objHN1.t1.tpr_other_address == null)
                {
                    address += objHN1.t1.tpr_main_address == null ? "" : "เลขที่ " + objHN1.t1.tpr_main_address;
                    address += objHN1.t1.tpr_main_tumbon == null ? "" : " แขวง/ตำบล " + objHN1.t1.tpr_main_tumbon;
                    address += objHN1.t1.tpr_main_amphur == null ? "" : " เขต/อำเภอ " + objHN1.t1.tpr_main_amphur;
                    address += objHN1.t1.tpr_main_province == null ? "" : " จังหวัด " + objHN1.t1.tpr_main_province;
                    address += objHN1.t1.tpr_main_zip_code == null ? "" : " รหัสไปรษณีย์ " + objHN1.t1.tpr_main_zip_code;
                }
                else
                {
                    address += objHN1.t1.tpr_other_address == null ? "" : "เลขที่ " + objHN1.t1.tpr_other_address;
                    address += objHN1.t1.tpr_other_tumbon == null ? "" : " แขวง/ตำบล " + objHN1.t1.tpr_other_tumbon;
                    address += objHN1.t1.tpr_other_amphur == null ? "" : " เขต/อำเภอ " + objHN1.t1.tpr_other_amphur;
                    address += objHN1.t1.tpr_other_province == null ? "" : " จังหวัด " + objHN1.t1.tpr_other_province;
                    address += objHN1.t1.tpr_other_zip_code == null ? " รหัสไปรษณีย์ " : objHN1.t1.tpr_other_zip_code;
                }


                txtaddress_th.Text = address;
            }
            else
            {
                string nameEn = "";
                if (string.IsNullOrEmpty(objHN1.t2.tpt_othername.Trim()))
                {
                    nameEn = (string.IsNullOrEmpty(objHN1.t2.tpt_en_name1) ? "" : objHN1.t2.tpt_en_name1.Trim()) +
                             (string.IsNullOrEmpty(objHN1.t2.tpt_en_name3) ? "" : " " + objHN1.t2.tpt_en_name3.Trim()) +
                             (string.IsNullOrEmpty(objHN1.t2.tpt_en_name2) ? "" : " " + objHN1.t2.tpt_en_name2.Trim());
                }
                else
                {
                    nameEn = objHN1.t2.tpt_othername;
                }

                txtname_th.Text = objHN1.t2.tpt_othername;
                txtname_en.Text = nameEn;
                txtnation_th.Text = objHN1.t2.tpt_nation_desc;
                txtname_en.Text = objHN1.t2.tpt_en_name1 + " " + objHN1.t2.tpt_en_name2;
                txtnation_en.Text = objHN1.t2.tpt_nation_desc;
                Program.SetValueRadioGroupBox(grbgender, objHN1.t2.tpt_gender);
                lblDOB_th.Text = Program.ConvertDateTimeToThai((DateTime)objHN1.t2.tpt_dob);
                lblDOB_en.Text = String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob);

                string address = String.Empty;
                if (objHN1.t1.tpr_other_address == null)
                {
                    address += objHN1.t1.tpr_main_address == null ? "" : objHN1.t1.tpr_main_address;
                    address += objHN1.t1.tpr_main_tumbon == null ? "" : " , " + objHN1.t1.tpr_main_tumbon;
                    address += objHN1.t1.tpr_main_amphur == null ? "" : " , " + objHN1.t1.tpr_main_amphur;
                    address += objHN1.t1.tpr_main_province == null ? "" : " " + objHN1.t1.tpr_main_province;
                    address += objHN1.t1.tpr_main_zip_code == null ? "" : objHN1.t1.tpr_main_zip_code;
                }
                else
                {
                    address += objHN1.t1.tpr_other_address == null ? "" : objHN1.t1.tpr_other_address;
                    address += objHN1.t1.tpr_other_tumbon == null ? "" : " " + objHN1.t1.tpr_other_tumbon;
                    address += objHN1.t1.tpr_other_amphur == null ? "" : " " + objHN1.t1.tpr_other_amphur;
                    address += objHN1.t1.tpr_other_province == null ? "" : " " + objHN1.t1.tpr_other_province;
                    address += objHN1.t1.tpr_main_zip_code == null ? "" : objHN1.t1.tpr_main_zip_code;
                }

                txtaddress_th.Text = address;
            }

            txtageyear_th.Text = this.CalculateAgeYear((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);
            txtagemonth_th.Text = this.CalculateAgeMonth((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);
            txtageyear_en.Text = this.CalculateAgeYear((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);
            txtagemonth_en.Text = this.CalculateAgeMonth((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);
            

        }

        private string CalculateAgeMonth(DateTime startDate, DateTime endDate)
        {
            if (startDate == null || endDate == null)
            {
                return String.Empty;
            }
            if (startDate.Date > endDate.Date)
            {
                throw new ArgumentException("startDate cannot be higher then endDate", "startDate");
            }
            int years = endDate.Year - startDate.Year;
            int months = 0;

            if (endDate < startDate.AddYears(years) && years != 0)
            { years--; }

            // Calculate the number of months.
            startDate = startDate.AddYears(years);
            if (startDate.Year == endDate.Year)
            { months = endDate.Month - startDate.Month; }
            else
            { months = (12 - startDate.Month) + endDate.Month; }

            //Check if last month was a complete month.
            if (endDate < startDate.AddMonths(months) && months != 0)
            { months--; }

            return months.ToString();
        }

        

        private string CalculateAgeYear(DateTime startDate, DateTime endDate)
        {
            if (startDate == null || endDate == null)
            {
                return String.Empty;
            }
            if (startDate.Date > endDate.Date)
            {
                throw new ArgumentException("startDate cannot be higher then endDate", "startDate");
            }
            int years = endDate.Year - startDate.Year;

            //Check if the last year, was a full year.
            if (endDate < startDate.AddYears(years) && years != 0)
            { years--; }

            return years.ToString();
        }

        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            label68.Visible = false;
            label2.Visible = false;

            if (textBox24.Text != String.Empty || textBox25.Text != String.Empty)
            {
                string strNum = textBox24.Text.Trim();
                double Num;
                bool isNum = double.TryParse(strNum, out Num);
                if (isNum == false) { label2.Visible = true; return; } else { label2.Visible = false; }


                string strNum2 = textBox25.Text.Trim();
                double Num2;
                bool isNum2 = double.TryParse(strNum2, out Num2);
                if (isNum2 == false) { label68.Visible = true; return; } else { label68.Visible = false; }
            }

            int tpr_id = 0;
            if (Program.CurrentRegis != null)
            {
                tpr_id = Program.CurrentRegis.tpr_id;
                this.Save('D');
                lblMsg.Text = "Save As Draft Complete";
                lblMsg.Visible = true;
                timer1.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            label68.Visible = false;
            label2.Visible = false;

            if (textBox24.Text != String.Empty || textBox25.Text != String.Empty)
            {
                string strNum = textBox24.Text.Trim();
                double Num;
                bool isNum = double.TryParse(strNum, out Num);
                if (isNum == false) { label2.Visible = true; return; } else { label2.Visible = false; }


                string strNum2 = textBox25.Text.Trim();
                double Num2;
                bool isNum2 = double.TryParse(strNum2, out Num2);
                if (isNum2 == false) { label68.Visible = true; return; } else { label68.Visible = false; }
            }


            this.Save('N');
            lblMsg.Text = "Save Completed";
            lblMsg.Visible = true;
            timer1.Enabled = true;
        }

        private void SaveNew(char doctype)
        {
            trn_ques_aviation currentquest = (trn_ques_aviation)trnquesavaitionBindingSource.Current;
            trn_ques_aviation Newcurrentquest = (trn_ques_aviation)trnNewquesavaitionBindingSource.Current;

            Newcurrentquest.tpr_id = Program.CurrentHDR.tpr_id;
            Newcurrentquest.tqa_type = doctype;
            Newcurrentquest.tqa_doc_type = Program.CurrentRegis.tpr_aviation_type;

            Newcurrentquest.tqa_create_by = Program.CurrentUser.mut_username;
            Newcurrentquest.tqa_create_date = Program.GetServerDateTime();

            Newcurrentquest.tqa_update_by = Program.CurrentUser.mut_username;
            Newcurrentquest.tqa_update_date = Program.GetServerDateTime();

            Newcurrentquest.tqa_place_exam = txtplace.Text;
            Newcurrentquest.tqa_th_fullname = txtname_th.Text;
            Newcurrentquest.tqa_en_fullname = txtname_en.Text;
            Newcurrentquest.tqa_th_nation = txtnation_th.Text;
            Newcurrentquest.tqa_en_nation = txtnation_en.Text;

            DateTime dob = (DateTime)(from t1 in dbc.trn_patients where t1.tpt_id == Program.CurrentRegis.tpt_id select t1.tpt_dob).FirstOrDefault();

            Newcurrentquest.tqa_dob = dob;
            Newcurrentquest.tqa_age_yrs = Convert.ToDouble(txtageyear_th.Text);
            Newcurrentquest.tqa_age_month = Convert.ToDouble(txtagemonth_th.Text);
            Newcurrentquest.tqa_avia_type = Program.GetValueGroupBox(grbaviatype);
            Newcurrentquest.tqa_avia_oths = txtaviation_oth.Text;
            Newcurrentquest.tqa_license_no = currentquest.tqa_license_no;

            Newcurrentquest.tqa_th_address = txtaddress_th.Text;
            Newcurrentquest.tqa_th_moblie = currentquest.tqa_th_moblie;
            Newcurrentquest.tqa_en_address = txtaddress_en.Text;
            Newcurrentquest.tqa_en_mobile = currentquest.tqa_en_mobile;
            Newcurrentquest.tqa_th_occupa = currentquest.tqa_th_occupa;
            Newcurrentquest.tqa_en_occupa = currentquest.tqa_en_occupa;
            Newcurrentquest.tqa_th_comp = currentquest.tqa_th_comp;
            Newcurrentquest.tqa_en_comp = currentquest.tqa_en_comp;

            Newcurrentquest.tqa_th_office = currentquest.tqa_th_office;
            Newcurrentquest.tqa_en_office = currentquest.tqa_en_office;

            Newcurrentquest.tqa_th_office = currentquest.tqa_th_office;
            Newcurrentquest.tqa_en_office = currentquest.tqa_en_office;

            Newcurrentquest.tqa_cont_address = Program.GetValueRadioTochar(pnlconadd);
            Newcurrentquest.tqa_person_emer = currentquest.tqa_person_emer;
            Newcurrentquest.tqa_telep_emer = currentquest.tqa_telep_emer;

            Newcurrentquest.tqa_prev_exam_loc = currentquest.tqa_prev_exam_loc;

            Newcurrentquest.tqa_waiver_spec = currentquest.tqa_waiver_spec;
            Newcurrentquest.tqa_tot_fling_time = currentquest.tqa_tot_fling_time;
            Newcurrentquest.tqa_last_six_time = currentquest.tqa_last_six_time;

            Newcurrentquest.tqa_aircraft_oth = currentquest.tqa_aircraft_oth;
            Newcurrentquest.tqa_smoking_since = currentquest.tqa_smoking_since;
            Newcurrentquest.tqa_smoking_amt = currentquest.tqa_smoking_amt;
            Newcurrentquest.tqa_use_medicine = currentquest.tqa_use_medicine;
            Newcurrentquest.tqa_med_name = txtmed_name.Text;
            Newcurrentquest.tqa_med_startdate = Convert.ToDateTime(dtpmed_startdate.Value);
            Newcurrentquest.tqa_med_reason = txtmed_reason.Text;
           // Newcurrentquest.tqa_name_med = currentquest.tqa_name_med;

            //tab 1
            Newcurrentquest.tqa_sex = Program.GetValueGroupBox(grbgender);
            Newcurrentquest.tqa_marital = Program.GetValueRadioTochar(pnlmarital);
            Newcurrentquest.tqa_license_type = Program.GetValueRadioTochar(pnllicensetype);
            Newcurrentquest.tqa_avia_type = Program.GetValueGroupBox(grbaviatype);
            Newcurrentquest.tqa_prev_examined = Program.GetValueRadioTochar(pnlprevexam);
            Newcurrentquest.tqa_prev_exam_date = Convert.ToDateTime(dtpprev_date.Value);
            
            Newcurrentquest.tqa_prev_exam_deca = Program.GetValueRadioTochar(pnlprev_exam_decl);
            Newcurrentquest.tqa_med_waiver = Program.GetValueRadioTochar(pnlmed_waiver);
            Newcurrentquest.tqa_aircraft_type = Program.GetValueRadioTochar(pnlaircraft_type);
            Newcurrentquest.tqa_flying_status = Program.GetValueRadioTochar(pnlflying_status);
            Newcurrentquest.tqa_smoking = Program.GetValueRadioTochar(pnlsmoking);
            Newcurrentquest.tqa_smoking_since = txtstoped_since.Text;
            Newcurrentquest.tqa_smoking_type = txtstate_type.Text;
            Newcurrentquest.tqa_smoking_amt = txtsmok_amt.Text;
            Newcurrentquest.tqa_m20_exercise = Program.GetValueRadioTochar(pnlexercise);

            Newcurrentquest.tqa_aircraft_name = txtaircraft_name.Text;
            Newcurrentquest.tqa_aircraft_jet = chkjet.Checked;
            Newcurrentquest.tqa_aircraft_turbo = chkturbo_prop.Checked;
            Newcurrentquest.tqa_aircraft_heli = chkhelicopter.Checked;
            Newcurrentquest.tqa_aircraft_piston = chkpiston.Checked;
            Newcurrentquest.tqa_aircraft_other = chkoth.Checked;
            Newcurrentquest.tqa_aircraft_oth = txtoth.Text;
            Newcurrentquest.tqa_single_pilot = chksingle_pilot.Checked;
            Newcurrentquest.tqa_muti_pilot = chkmuti_pilot.Checked;

            //tab 2
            Newcurrentquest.tqa_chis_freq = Program.GetValueRadioTochar(pnlchis_freq);
            Newcurrentquest.tqa_chis_dizz = Program.GetValueRadioTochar(pnlchis_dizz);
            Newcurrentquest.tqa_chis_unco = Program.GetValueRadioTochar(pnlchis_unco);
            Newcurrentquest.tqa_chis_eyet = Program.GetValueRadioTochar(pnlchis_eyes);
            Newcurrentquest.tqa_chis_hayf = Program.GetValueRadioTochar(pnlchis_hayf);
            Newcurrentquest.tqa_chis_hert = Program.GetValueRadioTochar(pnlchis_heart);
            Newcurrentquest.tqa_chis_chst = Program.GetValueRadioTochar(pnlchis_chest);
            Newcurrentquest.tqa_chis_high = Program.GetValueRadioTochar(pnlchis_high);
            Newcurrentquest.tqa_chis_stom = Program.GetValueRadioTochar(pnlchis_stomach);
            Newcurrentquest.tqa_chis_jaun = Program.GetValueRadioTochar(pnlchis_jaun);
            Newcurrentquest.tqa_chis_kidn = Program.GetValueRadioTochar(pnlchis_kidn);
            Newcurrentquest.tqa_chis_suga = Program.GetValueRadioTochar(pnlchis_sugar);
            Newcurrentquest.tqa_chis_epil = Program.GetValueRadioTochar(pnlepil);
            Newcurrentquest.tqa_chis_nurv = Program.GetValueRadioTochar(pnlchis_nurv);
            Newcurrentquest.tqa_chis_temp = Program.GetValueRadioTochar(pnlchis_temp);
            Newcurrentquest.tqa_chis_drug = Program.GetValueRadioTochar(pnlchis_drug);
            Newcurrentquest.tqa_chis_suic = Program.GetValueRadioTochar(pnlchis_suic);
            Newcurrentquest.tqa_chis_losw = Program.GetValueRadioTochar(pnlchis_lows);
            Newcurrentquest.tqa_chis_moti = Program.GetValueRadioTochar(pnlchis_moti);
            Newcurrentquest.tqa_chis_reje = Program.GetValueRadioTochar(pnlchis_rej);
            Newcurrentquest.tqa_chis_adms = Program.GetValueRadioTochar(pnlchis_adm);
            Newcurrentquest.tqa_chis_avia = Program.GetValueRadioTochar(pnlchis_avia);
            Newcurrentquest.tqa_chis_otha = Program.GetValueRadioTochar(pnlchis_otha);
            Newcurrentquest.tqa_chis_gyna = Program.GetValueRadioTochar(pnlchis_gyna);
            Newcurrentquest.tqa_chis_othi = Program.GetValueRadioTochar(pnlchis_othi);
            Newcurrentquest.tqa_chis_heth = Program.GetValueRadioTochar(pnlchis_hlht);
            //Newcurrentquest.tqa_chis_fam_his = Program.GetValueRadioTochar(pnlchis_fam_his);

            //cbdiabetes.Checked = (currentquest.tqa_chis_fam_diab == null || currentquest.tqa_chis_fam_diab == false) ? false : true;
            //cbcardio.Checked = (currentquest.tqa_chis_fam_card == null || currentquest.tqa_chis_fam_card == false) ? false : true;
            //cbmental.Checked = (currentquest.tqa_chis_fam_ment == null || currentquest.tqa_chis_fam_ment == false) ? false : true;

            Newcurrentquest.tqa_chis_fam_diab = cbdiabetes.Checked;
            Newcurrentquest.tqa_chis_fam_card = cbcardio.Checked;
            Newcurrentquest.tqa_chis_fam_ment = cbmental.Checked; 

            Newcurrentquest.tqa_chis_conviction = Program.GetValueRadioTochar(pnlconviction);

            Newcurrentquest.tqa_chis_freq_rmk = currentquest.tqa_chis_freq_rmk;
            Newcurrentquest.tqa_chis_dizz_rmk = currentquest.tqa_chis_dizz_rmk;
            Newcurrentquest.tqa_chis_unco_rmk = currentquest.tqa_chis_unco_rmk;
            Newcurrentquest.tqa_chis_eyet_rmk = currentquest.tqa_chis_eyet_rmk;
            Newcurrentquest.tqa_chis_hayf_rmk = currentquest.tqa_chis_hayf_rmk;
            Newcurrentquest.tqa_chis_hert_rmk = currentquest.tqa_chis_hert_rmk;
            Newcurrentquest.tqa_chis_chst_rmk = currentquest.tqa_chis_chst_rmk;
            Newcurrentquest.tqa_chis_high_rmk = currentquest.tqa_chis_high_rmk;
            Newcurrentquest.tqa_chis_stom_rmk = currentquest.tqa_chis_stom_rmk;
            Newcurrentquest.tqa_chis_jaun_rmk = currentquest.tqa_chis_jaun_rmk;
            Newcurrentquest.tqa_chis_kidn_rmk = currentquest.tqa_chis_kidn_rmk;
            Newcurrentquest.tqa_chis_suga_rmk = currentquest.tqa_chis_suga_rmk;
            Newcurrentquest.tqa_chis_epil_rmk = currentquest.tqa_chis_epil_rmk;
            Newcurrentquest.tqa_chis_nurv_rmk = currentquest.tqa_chis_nurv_rmk;

            Newcurrentquest.tqa_chis_temp_rmk = currentquest.tqa_chis_temp_rmk;
            Newcurrentquest.tqa_chis_drug_rmk = currentquest.tqa_chis_drug_rmk;
            Newcurrentquest.tqa_chis_suic_rmk = currentquest.tqa_chis_suic_rmk;
            Newcurrentquest.tqa_chis_losw_rmk = currentquest.tqa_chis_losw_rmk;
            Newcurrentquest.tqa_chis_moti_rmk = currentquest.tqa_chis_moti_rmk;
            Newcurrentquest.tqa_chis_reje_rmk = currentquest.tqa_chis_reje_rmk;
            Newcurrentquest.tqa_chis_adms_rmk = currentquest.tqa_chis_adms_rmk;
            Newcurrentquest.tqa_chis_avia_rmk = currentquest.tqa_chis_avia_rmk;
            Newcurrentquest.tqa_chis_otha_rmk = currentquest.tqa_chis_otha_rmk;
            Newcurrentquest.tqa_chis_gyna_rmk = currentquest.tqa_chis_gyna_rmk;
            Newcurrentquest.tqa_chis_othi_rmk = currentquest.tqa_chis_othi_rmk;
            Newcurrentquest.tqa_chis_heth_rmk = currentquest.tqa_chis_heth_rmk;

            Newcurrentquest.tqa_tot_fling_time = textBox24.Text == String.Empty ? 0 : Convert.ToDouble(textBox24.Text);
            Newcurrentquest.tqa_last_six_time = textBox25.Text == String.Empty ? 0 : Convert.ToDouble(textBox25.Text);

            trnNewquesavaitionBindingSource.EndEdit();
        }

        private void Update(char doctype)
        {
            trn_ques_aviation currentquest = (trn_ques_aviation)trnquesavaitionBindingSource.Current;

            currentquest.tpr_id = Program.CurrentRegis.tpr_id;
            currentquest.tqa_type = doctype;
            currentquest.tqa_doc_type = Program.CurrentRegis.tpr_aviation_type;
            currentquest.tqa_update_by = Program.CurrentUser.mut_username;
            currentquest.tqa_update_date = Program.GetServerDateTime();

            currentquest.tqa_th_fullname = txtname_th.Text;
            currentquest.tqa_en_fullname = txtname_en.Text;
            currentquest.tqa_th_nation = txtnation_th.Text;
            currentquest.tqa_en_nation = txtnation_en.Text;

            currentquest.tqa_place_exam = txtplace.Text;

            currentquest.tqa_age_yrs = Convert.ToDouble(txtageyear_th.Text);
            currentquest.tqa_age_month = Convert.ToDouble(txtagemonth_th.Text);
            currentquest.tqa_th_address = txtaddress_th.Text;
            currentquest.tqa_en_address = txtaddress_en.Text;

            //find dob
            DateTime dob = (DateTime)(from t1 in dbc.trn_patients where t1.tpt_id == Program.CurrentRegis.tpt_id select t1.tpt_dob).FirstOrDefault();

            currentquest.tqa_dob = dob;
            //tab 1
            currentquest.tqa_sex = Program.GetValueGroupBox(grbgender);
            currentquest.tqa_avia_type = Program.GetValueGroupBox(grbaviatype);
            currentquest.tqa_avia_oths = txtaviation_oth.Text;
            currentquest.tqa_license_type = Program.GetValueRadioTochar(pnllicensetype);
            currentquest.tqa_marital = Program.GetValueRadioTochar(pnlmarital);
            currentquest.tqa_prev_examined = Program.GetValueRadioTochar(pnlprevexam);
            currentquest.tqa_prev_exam_deca = Program.GetValueRadioTochar(pnlprev_exam_decl);
            currentquest.tqa_med_waiver = Program.GetValueRadioTochar(pnlmed_waiver);
            currentquest.tqa_aircraft_type = Program.GetValueRadioTochar(pnlaircraft_type);
            currentquest.tqa_prev_exam_date = Convert.ToDateTime(dtpprev_date.Value);

            currentquest.tqa_flying_status = Program.GetValueRadioTochar(pnlflying_status);
            currentquest.tqa_smoking = Program.GetValueRadioTochar(pnlsmoking);
            currentquest.tqa_smoking_since = txtstoped_since.Text;
            currentquest.tqa_smoking_type = txtstate_type.Text;
            currentquest.tqa_smoking_amt = txtsmok_amt.Text;
            currentquest.tqa_m20_exercise = Program.GetValueRadioTochar(pnlexercise);
            currentquest.tqa_cont_address = Program.GetValueRadioTochar(pnlconadd);
            currentquest.tqa_use_medicine = Program.GetValueRadioTochar(pnluse_midicine);
            currentquest.tqa_med_name = txtmed_name.Text;
            currentquest.tqa_med_amount = txtmed_amt.Text;
            currentquest.tqa_med_startdate = Convert.ToDateTime(dtpmed_startdate.Value);
            currentquest.tqa_med_reason = txtmed_reason.Text;

            currentquest.tqa_aircraft_name = txtaircraft_name.Text;
            currentquest.tqa_aircraft_jet = chkjet.Checked;
            currentquest.tqa_aircraft_turbo = chkturbo_prop.Checked;
            currentquest.tqa_aircraft_heli = chkhelicopter.Checked;
            currentquest.tqa_aircraft_piston = chkpiston.Checked;
            currentquest.tqa_aircraft_other = chkoth.Checked;
            currentquest.tqa_aircraft_oth = txtoth.Text;
            currentquest.tqa_single_pilot = chksingle_pilot.Checked;

            currentquest.tqa_muti_pilot = chkmuti_pilot.Checked;
            currentquest.tqa_avia_oths = txtaviation_oth.Text;
            //tab 2
            currentquest.tqa_chis_freq = Program.GetValueRadioTochar(pnlchis_freq);
            currentquest.tqa_chis_freq_rmk = txtfreq.Text;
            currentquest.tqa_chis_dizz = Program.GetValueRadioTochar(pnlchis_dizz);
            currentquest.tqa_chis_dizz_rmk = txtdizz.Text;
            currentquest.tqa_chis_unco = Program.GetValueRadioTochar(pnlchis_unco);
            currentquest.tqa_chis_unco_rmk = txtunco.Text;
            currentquest.tqa_chis_eyet = Program.GetValueRadioTochar(pnlchis_eyes);
            currentquest.tqa_chis_eyet_rmk = txteye.Text;
            currentquest.tqa_chis_hayf = Program.GetValueRadioTochar(pnlchis_hayf);
            currentquest.tqa_chis_hayf_rmk = txthayf.Text;
            currentquest.tqa_chis_hert = Program.GetValueRadioTochar(pnlchis_heart);
            currentquest.tqa_chis_hert_rmk = txtheart.Text;
            currentquest.tqa_chis_chst = Program.GetValueRadioTochar(pnlchis_chest);
            currentquest.tqa_chis_chst_rmk = txtchest.Text;
            currentquest.tqa_chis_high = Program.GetValueRadioTochar(pnlchis_high);
            currentquest.tqa_chis_high_rmk = txthigh.Text;
            currentquest.tqa_chis_stom = Program.GetValueRadioTochar(pnlchis_stomach);
            currentquest.tqa_chis_stom_rmk = txtstomach.Text;
            currentquest.tqa_chis_jaun = Program.GetValueRadioTochar(pnlchis_jaun);
            currentquest.tqa_chis_jaun_rmk = txtjaun.Text;
            currentquest.tqa_chis_kidn = Program.GetValueRadioTochar(pnlchis_kidn);
            currentquest.tqa_chis_kidn_rmk = txtkidn.Text;
            currentquest.tqa_chis_suga = Program.GetValueRadioTochar(pnlchis_sugar);
            currentquest.tqa_chis_suga_rmk = txtchis_sugar.Text;
            currentquest.tqa_chis_epil = Program.GetValueRadioTochar(pnlepil);
            currentquest.tqa_chis_epil_rmk = txtepilfit.Text;
            currentquest.tqa_chis_nurv = Program.GetValueRadioTochar(pnlchis_nurv);
            currentquest.tqa_chis_nurv_rmk = txtnurv.Text;
            currentquest.tqa_chis_temp = Program.GetValueRadioTochar(pnlchis_temp);
            currentquest.tqa_chis_temp_rmk = txttemp.Text;
            currentquest.tqa_chis_drug = Program.GetValueRadioTochar(pnlchis_drug);
            currentquest.tqa_chis_drug_rmk = txtdrug.Text;
            currentquest.tqa_chis_suic = Program.GetValueRadioTochar(pnlchis_suic);
            currentquest.tqa_chis_suic_rmk = txtsuic.Text;
            currentquest.tqa_chis_losw = Program.GetValueRadioTochar(pnlchis_lows);
            currentquest.tqa_chis_losw_rmk = txtloss.Text;
            currentquest.tqa_chis_moti = Program.GetValueRadioTochar(pnlchis_moti);
            currentquest.tqa_chis_moti_rmk = txtmoti.Text;
            currentquest.tqa_chis_reje = Program.GetValueRadioTochar(pnlchis_rej);
            currentquest.tqa_chis_reje_rmk = txtreje.Text;
            currentquest.tqa_chis_adms = Program.GetValueRadioTochar(pnlchis_adm);
            currentquest.tqa_chis_adms_rmk = txtadm.Text;
            currentquest.tqa_chis_avia = Program.GetValueRadioTochar(pnlchis_avia);
            currentquest.tqa_chis_avia_rmk = txtavia.Text;
            currentquest.tqa_chis_otha = Program.GetValueRadioTochar(pnlchis_otha);
            currentquest.tqa_chis_otha_rmk = txtotha.Text;
            currentquest.tqa_chis_gyna = Program.GetValueRadioTochar(pnlchis_gyna);
            currentquest.tqa_chis_gyna_rmk = txtgyna.Text;
            currentquest.tqa_chis_othi = Program.GetValueRadioTochar(pnlchis_othi);
            currentquest.tqa_chis_othi_rmk = txtothi.Text;
            currentquest.tqa_chis_heth = Program.GetValueRadioTochar(pnlchis_hlht);
            currentquest.tqa_chis_heth_rmk = txtmental.Text;
            //currentquest.tqa_chis_fam_his = Program.GetValueRadioTochar(pnlchis_fam_his);

            //cbdiabetes.Checked = (currentquest.tqa_chis_fam_diab == null || currentquest.tqa_chis_fam_diab == false) ? false : true;
            //cbcardio.Checked = (currentquest.tqa_chis_fam_card == null || currentquest.tqa_chis_fam_card == false) ? false : true;
            //cbmental.Checked = (currentquest.tqa_chis_fam_ment == null || currentquest.tqa_chis_fam_ment == false) ? false : true;
            currentquest.tqa_chis_fam_diab = cbdiabetes.Checked;
            currentquest.tqa_chis_fam_card = cbcardio.Checked;
            currentquest.tqa_chis_fam_ment = cbmental.Checked;
            currentquest.tqa_chis_conviction = Program.GetValueRadioTochar(pnlconviction);

            currentquest.tqa_prev_exam_loc = txtprev_exam_loc.Text;
            currentquest.tqa_waiver_spec = textBox23.Text;
            currentquest.tqa_aircraft_oth = txtoth.Text;
            currentquest.tqa_smoking_since = txtstoped_since.Text;
            currentquest.tqa_smoking_amt = txtstate_type.Text;
            currentquest.tqa_med_name = txtmed_name.Text;

            currentquest.tqa_tot_fling_time = textBox24.Text == String.Empty ? 0 : Convert.ToDouble(textBox24.Text);
            currentquest.tqa_last_six_time = textBox25.Text == String.Empty ? 0 : Convert.ToDouble(textBox25.Text);

            trnquesavaitionBindingSource.EndEdit();
        }
        private void Save(char docType)
        {

            if (Program.CurrentRegis != null)
            {

                 //find HN
                var objHN1 = (from t1 in dbc.trn_patient_regis join t2 in dbc.trn_patients on t1.tpt_id equals t2.tpt_id where t1.tpt_id == Program.CurrentRegis.tpt_id select new { t1,t2 }).OrderByDescending(c => c.t1.tpr_create_date).FirstOrDefault();

                //EN No Lastest
                string objHN2 = Program.CurrentRegis.tpr_en_no;

                trn_ques_aviation currentquest = (trn_ques_aviation)trnquesavaitionBindingSource.Current;

                if (objHN1.t1.tpr_en_no == objHN2)
                {
                    this.Update(docType);
                }
                else
                {
                    this.SaveNew(docType);
                }
                //if (currentquest.tqa_update_date != null)
                //{

                //    //if (((DateTime)currentquest.tqa_update_date).Date < Program.GetServerDateTime().Date)
                //    //{
                //    //    this.SaveNew(docType);
                //    //}
                //    //else
                //    //{
                //    //    this.Update(docType);
                //    //}
                        
                //}
                //else
                //{
                //    //this.Update(docType);
                //}

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
                timer1.Enabled = true;
                lblMsg.Visible = true;
                this.LoadTransaction();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 20; i++)
            {
                if (i == 20)
                {
                    lblMsg.Visible = false;
                    timer1.Enabled = false;
                }
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked == true)
            {
                txtaviation_oth.Enabled = true;
                return;
            }

            txtaviation_oth.Enabled = false;
            txtaviation_oth.DataBindings.Clear();
            txtaviation_oth.Clear();
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton21.Checked == true)
            {
                txtprev_exam_loc.Enabled = true;
                dtpprev_date.Enabled = true;
                return;
            }

            txtprev_exam_loc.Enabled = false;
            dtpprev_date.Enabled = false;
            dtpprev_date.Text = null;
            txtprev_exam_loc.DataBindings.Clear();
            txtprev_exam_loc.Clear();
        }

        private void radioButton23_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton23.Checked == true)
            {
                textBox23.Enabled = true;
                return;
            }

            textBox23.Enabled = false;
            textBox23.DataBindings.Clear();
            textBox23.Clear();
        }

        private void radioButton29_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton29.Checked == true)
            {
                txtoth.Enabled = true;
                return;
            }

            txtoth.Enabled = false;
            txtoth.DataBindings.Clear();
            txtoth.Clear();
        }

        private void radioButton33_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton33.Checked == true)
            {
                txtstoped_since.Enabled = true;

                return;
            }

            txtstoped_since.Enabled = false;
            txtstoped_since.DataBindings.Clear();
            txtstoped_since.Clear();
        }

        private void radioButton34_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton34.Checked == true)
            {
                txtstate_type.Enabled = true;
                txtsmok_amt.Enabled = true;
                return;
            }

            txtstate_type.Enabled = false;
            txtsmok_amt.Enabled = false;
            txtsmok_amt.Clear();
            txtstate_type.DataBindings.Clear();
            txtstate_type.Clear();
        }

        private void radioButton38_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton38.Checked == true)
            {
                txtmed_name.Enabled = true;
                txtmed_amt.Enabled = true;
                txtmed_reason.Enabled = true;
                dtpmed_startdate.Enabled = true;
                return;
            }

            txtmed_name.Enabled = false;
            txtmed_amt.Enabled = false;
            txtmed_reason.Enabled = false;
            dtpmed_startdate.Enabled = false;
            txtmed_amt.Clear();
            txtmed_reason.Clear();
            txtmed_name.DataBindings.Clear();
            txtmed_name.Clear();
        }

        private void radioButton40_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton40.Checked == true)
            {
                txtfreq.Enabled = true;
                return;
            }

            txtfreq.Enabled = false;
            txtfreq.DataBindings.Clear();
            txtfreq.Clear();
        }

        private void radioButton41_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton41.Checked == true)
            {
                txtdizz.Enabled = true;
                return;
            }

            txtdizz.Enabled = false;
            txtdizz.DataBindings.Clear();
            txtdizz.Clear();
        }

        private void radioButton43_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton43.Checked == true)
            {
                txtunco.Enabled = true;
                return;
            }

            txtunco.Enabled = false;
            txtunco.DataBindings.Clear();
            txtunco.Clear();
        }

        private void radioButton45_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton45.Checked == true)
            {
                txteye.Enabled = true;
                return;
            }

            txteye.Enabled = false;
            txteye.DataBindings.Clear();
            txteye.Clear();
        }

        private void radioButton47_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton47.Checked == true)
            {
                txthayf.Enabled = true;
                return;
            }

            txthayf.Enabled = false;
            txthayf.DataBindings.Clear();
            txthayf.Clear();
        }

        private void radioButton49_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton49.Checked == true)
            {
                txtheart.Enabled = true;
                return;
            }

            txtheart.Enabled = false;
            txtheart.DataBindings.Clear();
            txtheart.Clear();
        }

        private void radioButton51_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton51.Checked == true)
            {
                txtchest.Enabled = true;
                return;
            }

            txtchest.Enabled = false;
            txtchest.DataBindings.Clear();
            txtchest.Clear();
        }

        private void radioButton53_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton53.Checked == true)
            {
                txthigh.Enabled = true;
                return;
            }

           txthigh.Enabled = false;
           txthigh.DataBindings.Clear();
           txthigh.Clear();
        }

        private void radioButton55_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton55.Checked == true)
            {
                txtstomach.Enabled = true;
                return;
            }

            txtstomach.Enabled = false;
            txtstomach.DataBindings.Clear();
            txtstomach.Clear();
        }

        private void radioButton57_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton57.Checked == true)
            {
                txtjaun.Enabled = true;
                return;
            }

            txtjaun.Enabled = false;
            txtjaun.DataBindings.Clear();
            txtjaun.Clear();
        }

        private void radioButton59_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton59.Checked == true)
            {
                txtkidn.Enabled = true;
                return;
            }

            txtkidn.Enabled = false;
            txtkidn.DataBindings.Clear();
            txtkidn.Clear();
        }

        private void radioButton61_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton61.Checked == true)
            {
                txtchis_sugar.Enabled = true;
                return;
            }

            txtchis_sugar.Enabled = false;
            txtchis_sugar.DataBindings.Clear();
            txtchis_sugar.Clear();
        }

        private void radioButton63_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton63.Checked == true)
            {
                txtepil.Enabled = true;
                return;
            }

            txtepil.Enabled = false;
            txtepil.DataBindings.Clear();
            txtepil.Clear();
        }

        private void radioButton65_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton65.Checked == true)
            {
                txtnurv.Enabled = true;
                return;
            }

            txtnurv.Enabled = false;
            txtnurv.DataBindings.Clear();
            txtnurv.Clear();
        }

        private void radioButton67_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton67.Checked == true)
            {
                txttemp.Enabled = true;
                return;
            }

            txttemp.Enabled = false;
            txttemp.DataBindings.Clear();
            txttemp.Clear();
        }

        private void radioButton69_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton69.Checked == true)
            {
                txtdrug.Enabled = true;
                return;
            }

            txtdrug.Enabled = false;
            txtdrug.DataBindings.Clear();
            txtdrug.Clear();
        }

        private void radioButton71_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton71.Checked == true)
            {
                txtsuic.Enabled = true;
                return;
            }

            txtsuic.Enabled = false;
            txtsuic.DataBindings.Clear();
            txtsuic.Clear();
        }

        private void radioButton73_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton73.Checked == true)
            {
                txtloss.Enabled = true;
                return;
            }

            txtloss.Enabled = false;
            txtloss.DataBindings.Clear();
            txtloss.Clear();
        }

        private void radioButton75_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton75.Checked == true)
            {
                txtmoti.Enabled = true;
                return;
            }

            txtmoti.Enabled = false;
            txtmoti.DataBindings.Clear();
            txtmoti.Clear();
        }

        private void radioButton77_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton77.Checked == true)
            {
                txtreje.Enabled = true;
                return;
            }

            txtreje.Enabled = false;
            txtreje.DataBindings.Clear();
            txtreje.Clear();
        }

        private void radioButton79_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton79.Checked == true)
            {
                txtadm.Enabled = true;
                return;
            }

            txtadm.Enabled = false;
            txtadm.DataBindings.Clear();
            txtadm.Clear();
        }

        private void radioButton81_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton81.Checked == true)
            {
                txtavia.Enabled = true;
                return;
            }

            txtavia.Enabled = false;
            txtavia.DataBindings.Clear();
            txtavia.Clear();
        }

        private void radioButton83_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton83.Checked == true)
            {
                txtotha.Enabled = true;
                return;
            }

            txtotha.Enabled = false;
            txtotha.DataBindings.Clear();
            txtotha.Clear();
        }

        private void radioButton85_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton85.Checked == true)
            {
                txtgyna.Enabled = true;
                return;
            }

            txtgyna.Enabled = false;
            txtgyna.DataBindings.Clear();
            txtgyna.Clear();
        }

        private void radioButton87_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton87.Checked == true)
            {
                txtothi.Enabled = true;
                //txtothi.DataBindings.Clear();
                return;
            }

            txtothi.Enabled = false;
            txtothi.DataBindings.Clear();
            txtothi.Clear();
        }

        private void radioButton89_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton89.Checked == true)
            {
                txtmental.Enabled = true;
                return;
            }

            txtmental.Enabled = false;
            txtmental.DataBindings.Clear();
            txtmental.Clear();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string> { "QA101" };
            int tprID = 0;
            if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.previewReport();
            //ClsReport.previewRpt(new List<string> { "QA101" });
        }

        private void radioButton91_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton91.Checked == true)
            {
                txtepilfit.Enabled = true;
                return;
            }

            txtepilfit.Enabled = false;
            txtepilfit.DataBindings.Clear();
            txtepilfit.Clear();
        }

        private void frmQuestionareAviation_N_Load(object sender, EventArgs e)
        {

            this.Text = PrePareData.StaticDataCls.ProjectName + " [Aviation]";
            timer1.Enabled = false;
            uiProfileHorizontal1.Loaddata();
            this.LoadTransaction();
        }

        public void loadfrm()
        {
            this.Text = PrePareData.StaticDataCls.ProjectName + " [Aviation]";
            timer1.Enabled = false;
            this.LoadTransaction();
        }

        private void chkoth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkoth.Checked == true)
            {
                txtoth.Enabled = true;
                this.chkSelectAirCraft_CheckedChanged(sender, e);
                return;
            }
            txtoth.Enabled = false;
            txtoth.Clear();
            this.chkSelectAirCraft_CheckedChanged(sender, e);
        }

        private void uiProfileHorizontal1_Load(object sender, EventArgs e)
        {
            uiProfileHorizontal1.Loaddata();
        }

        private void SelectAirCarft(CheckBox chk)
        {
            StringBuilder sb = new StringBuilder();
            sb.Remove(0, sb.Length);

            if (chk.Checked == true)
            {
                switch (chk.Tag.ToString())
                {
                    case "J":
                        _ListAirCraft.Add(new ListAirCraft { type = "J", name = "Jet" });
                        break;
                    case "T":
                        _ListAirCraft.Add(new ListAirCraft { type = "T", name = "Turbo Prop" });
                        break;
                    case "H":
                        _ListAirCraft.Add(new ListAirCraft { type = "H", name = "Helicopter" });
                        break;
                    case "P":
                        _ListAirCraft.Add(new ListAirCraft { type = "P", name = "Piston engine" });
                        break;
                    case "O":
                        _ListAirCraft.Add(new ListAirCraft { type = "O", name = "Other" });
                        break;
                    case "S":
                        _ListAirCraft.Add(new ListAirCraft { type = "S", name = "Single-pilot" });
                        break;
                    case "M":
                        _ListAirCraft.Add(new ListAirCraft { type = "M", name = "Multi-pilot" });
                        break;
                }
            }
            else
            {
                switch (chk.Tag.ToString())
                {
                    case "J":
                        foreach (var data in _ListAirCraft.FindAll(x => x.type == "J")) { _ListAirCraft.Remove(data); }
                        break;
                    case "T":
                        foreach (var data in _ListAirCraft.FindAll(x => x.type == "T")) { _ListAirCraft.Remove(data); }
                        break;
                    case "H":
                        foreach (var data in _ListAirCraft.FindAll(x => x.type == "H")) { _ListAirCraft.Remove(data); }
                        break;
                    case "P":
                        foreach (var data in _ListAirCraft.FindAll(x => x.type == "P")) { _ListAirCraft.Remove(data); }
                        break;
                    case "O":
                        foreach (var data in _ListAirCraft.FindAll(x => x.type == "O")) { _ListAirCraft.Remove(data); }
                        break;
                    case "S":
                        foreach (var data in _ListAirCraft.FindAll(x => x.type == "S")) { _ListAirCraft.Remove(data); }
                        break;
                    case "M":
                        foreach (var data in _ListAirCraft.FindAll(x => x.type == "M")) { _ListAirCraft.Remove(data); }
                        break;
                }
            }
            if (_ListAirCraft.Count > 0)
            {

                for (int i = 0; i < _ListAirCraft.Count; i++)
                {
                    if (i > 0 && i < _ListAirCraft.Count)
                    {
                        sb.Append(",");
                    }
                    sb.Append(_ListAirCraft[i].name);
                }

                txtaircraft_name.Text = sb.ToString();
            }
            else
            {
                txtaircraft_name.Clear();
            }
        }

        private void chkSelectAirCraft_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            this.SelectAirCarft(chk);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
