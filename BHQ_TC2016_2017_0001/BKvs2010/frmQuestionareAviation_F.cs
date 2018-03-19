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
    public partial class frmQuestionareAviation_F : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        public frmQuestionareAviation_F()
        {
            InitializeComponent();
        }

        private void frmQuestionareAviation_F_Load(object sender, EventArgs e)
        {
            this.Text = Program.GetRoomName("[Aviation]");
            this.uiProfileHorizontal1.Loaddata();
            this.LoadTransaction();
            this.timer1.Enabled = false;
        }

        public void loadfrm()
        {
            this.Text = PrePareData.StaticDataCls.ProjectName + " [Aviation]";
            timer1.Enabled = false;
            this.LoadTransaction();
        }

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

                        //tab 1
                        Program.SetValueRadioGroupBox(grbgender, CurrentQuestion.tqa_sex);
                        Program.SetValueRadioGroup(pnlaviatype, CurrentQuestion.tqa_avia_type);
                        Program.SetValueRadioGroup(pnluse_med, CurrentQuestion.tqa_use_medicine);
                        txtmed_amt.Text = CurrentQuestion.tqa_med_amount;
                        txtmed_reason.Text = CurrentQuestion.tqa_med_reason;
                        txtmed_name.Text = CurrentQuestion.tqa_med_name;
                        txtavia_oth.Text = CurrentQuestion.tqa_avia_oths;
                        txtaircraft_name.Text = CurrentQuestion.tqa_aircraft_name;
                        Program.SetValueRadioGroup(pnlchkaddr, CurrentQuestion.tqa_chge_address);
                        //tab 2
                        Program.SetValueRadioGroup(pnlchis_freq, CurrentQuestion.tqa_chis_freq);
                        Program.SetValueRadioGroup(pnlchis_dizz, CurrentQuestion.tqa_chis_dizz);
                        Program.SetValueRadioGroup(pnlchis_unco, CurrentQuestion.tqa_chis_unco);
                        Program.SetValueRadioGroup(pnlchis_eyes, CurrentQuestion.tqa_chis_eyet);
                        Program.SetValueRadioGroup(pnlchis_hayf, CurrentQuestion.tqa_chis_hayf);
                        Program.SetValueRadioGroup(pnlchis_lung, CurrentQuestion.tqa_chis_lung);
                        Program.SetValueRadioGroup(pnlchis_heart, CurrentQuestion.tqa_chis_hert);
                        Program.SetValueRadioGroup(pnlchis_high, CurrentQuestion.tqa_chis_high);
                        Program.SetValueRadioGroup(pnlchis_stomach, CurrentQuestion.tqa_chis_stom);
                        Program.SetValueRadioGroup(pnlchis_kind, CurrentQuestion.tqa_chis_kidn);
                        Program.SetValueRadioGroup(pnlchis_gyna, CurrentQuestion.tqa_chis_gyna);
                        Program.SetValueRadioGroup(pnlchis_nurv, CurrentQuestion.tqa_chis_nurv);
                        Program.SetValueRadioGroup(pnlchis_moti, CurrentQuestion.tqa_chis_moti);
                        Program.SetValueRadioGroup(pnlchis_mental, CurrentQuestion.tqa_chis_ment);
                        Program.SetValueRadioGroup(pnlchis_suic, CurrentQuestion.tqa_chis_suic);
                        Program.SetValueRadioGroup(pnlchis_alco, CurrentQuestion.tqa_chis_alco);
                        Program.SetValueRadioGroup(pnlchis_drug, CurrentQuestion.tqa_chis_drug);
                        Program.SetValueRadioGroup(pnlchis_adm, CurrentQuestion.tqa_chis_adms);
                        Program.SetValueRadioGroup(pnlchis_avia, CurrentQuestion.tqa_chis_avia);
                        Program.SetValueRadioGroup(pnlchis_otha, CurrentQuestion.tqa_chis_otha);
                        Program.SetValueRadioGroup(pnlconviction, CurrentQuestion.tqa_chis_conviction);
                        Program.SetValueRadioGroup(pnlchis_othi, CurrentQuestion.tqa_chis_othi);
                        Program.SetValueRadioGroup(pnlprev_exam_decla, CurrentQuestion.tqa_prev_exam_deca);


                        textBox12.Text = CurrentQuestion.tqa_tot_fling_time.ToString();
                        textBox13.Text = CurrentQuestion.tqa_last_six_time.ToString();

                        txtaddress.Text = CurrentQuestion.tqa_th_address;
                        txtplace.Text = CurrentQuestion.tqa_place_exam;
                        txtname_th.Text = CurrentQuestion.tqa_th_fullname;
                        txtname_en.Text = CurrentQuestion.tqa_en_fullname;
                        txtnation_th.Text = CurrentQuestion.tqa_th_nation;
                        txtnation_en.Text = CurrentQuestion.tqa_en_nation;


                        
                         if (objHN1.t2.tpt_nation_code == "TH")
                         {
                             lblpdate_dob.Text = Program.ConvertDateTimeToThai((DateTime)objHN1.t2.tpt_dob);
                         }
                         else
                         {
                             lblpdate_dob.Text = CurrentQuestion.tqa_dob == null ? null : String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob);
                         }


                        

                        txtage.Text = CurrentQuestion.tqa_age_yrs.ToString();
                        txtmonth.Text = CurrentQuestion.tqa_age_month.ToString();

                        //if (((DateTime)CurrentQuestion.tqa_update_date).Date < DateTime.Now.Date)
                        //{
                            //trnNewquesavaitionBindingSource.DataSource = dbc.trn_ques_aviations;
                            //trnNewquesavaitionBindingSource.AddNew();
                        //}
                    }
                    else
                    {
                        //Load Default Data
                        this.LoadNewDefaultData();
                        trnquesavaitionBindingSource.DataSource =(from t1 in dbc.trn_ques_aviations select t1).Take(0);
                        trnquesavaitionBindingSource.AddNew();
                    }
                }
                else
                {
                    //Load Default Data
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
                txtnation_en.Text = objHN1.t2.tpt_nation_desc;
                Program.SetValueRadioGroupBox(grbgender, objHN1.t2.tpt_gender);

                lblpdate_dob.Text = Program.ConvertDateTimeToThai((DateTime)objHN1.t2.tpt_dob);
                
                //lblDOB_th.Text = Program.ConvertDateTimeToThai((DateTime)objHN1.t2.tpt_dob);
                //lblDOB_en.Text = String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob);

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


                txtaddress.Text = address;
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
                txtnation_en.Text = objHN1.t2.tpt_nation_desc;
                Program.SetValueRadioGroupBox(grbgender, objHN1.t2.tpt_gender);
                //lblDOB_th.Text = Program.ConvertDateTimeToThai((DateTime)objHN1.t2.tpt_dob);
                //lblDOB_en.Text = String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob);

                lblpdate_dob.Text = String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob);

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

                txtaddress.Text = address;
            }

            txtage.Text = this.CalculateAgeYear((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);
            txtmonth.Text = this.CalculateAgeMonth((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);

        }

        //private void LoadNewDefaultData()
        //{
        //    var objHN1 = (from t1 in dbc.trn_patient_regis join t2 in dbc.trn_patients on t1.tpt_id equals t2.tpt_id where t1.tpt_id == Program.CurrentRegis.tpt_id select new { t1, t2 }).OrderByDescending(c => c.t1.tpr_create_date).FirstOrDefault();

        //    if (objHN1.t2.tpt_nation_code == "TH")
        //    {
        //        txtname_th.Text = objHN1.t2.tpt_first_name + " " + objHN1.t2.tpt_last_name;
        //        txtnation_th.Text = objHN1.t2.tpt_nation_desc;
        //        lblpdate_dob.Text = String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob);
        //        txtaddress.Text = objHN1.t1.tpr_other_address + " แขวง/ตำบล " + objHN1.t1.tpr_other_tumbon + " เขต/อำเภอ " + objHN1.t1.tpr_other_amphur + " จังหวัด " + objHN1.t1.tpr_other_province;
        //    }
        //    else
        //    {
        //        txtname_en.Text = objHN1.t2.tpt_first_name + " " + objHN1.t2.tpt_last_name;
        //        txtnation_en.Text = objHN1.t2.tpt_nation_desc;
        //        lblpdate_dob.Text = String.Format("{0:dd/MM/yyyy}", objHN1.t2.tpt_dob;
        //        txtaddress.Text = objHN1.t1.tpr_other_address + "," + objHN1.t1.tpr_other_tumbon + "," + objHN1.t1.tpr_other_amphur + "," + objHN1.t1.tpr_other_province + ".";
        //    }

        //    txtage.Text = this.CalculateAgeYear((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);
        //    txtmonth.Text = this.CalculateAgeMonth((DateTime)objHN1.t2.tpt_dob, Program.GetServerDateTime().Date);
        //}
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
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                txtaddress.Enabled = true;
            }
            else
            {
                txtaddress.Enabled = false;
                
            }
        }
        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked == true)
            {
                txtavia_oth.Enabled = true;
            }
            else
            {
                txtavia_oth.Enabled = false;
                txtavia_oth.DataBindings.Clear();
                txtavia_oth.Clear();
            }
        }
        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton12.Checked == true)
            {
                txtmed_name.Enabled = true;
                txtmed_amt.Enabled = true;
                txtmed_reason.Enabled = true;
            }
            else
            {
                txtmed_name.Enabled = false;
                txtmed_amt.Enabled = false;
                txtmed_reason.Enabled = false;
                txtmed_name.DataBindings.Clear();
                
                txtmed_name.Clear();
                txtmed_amt.Clear();
                txtmed_reason.Clear();
            }
        }
        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            label24.Visible = false;
            label2.Visible = false;

            if (textBox13.Text != String.Empty || textBox12.Text != String.Empty)
            {
                string strNum = textBox13.Text.Trim();
                double Num;
                bool isNum = double.TryParse(strNum, out Num);
                if (isNum == false) { label2.Visible = true; return; } else { label2.Visible = false; }


                string strNum2 = textBox12.Text.Trim();
                double Num2;
                bool isNum2 = double.TryParse(strNum2, out Num2);
                if (isNum2 == false) { label24.Visible = true; return; } else { label24.Visible = false; }

            }

            int tpr_id = 0;
            if (Program.CurrentRegis != null)
            {
                tpr_id = Program.CurrentRegis.tpr_id;
                this.Save('D');
                lblMsg.Text = "Save as Draft Complete";
                lblMsg.Visible = true;
                timer1.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            label24.Visible = false;
            label2.Visible = false;

            if (textBox13.Text != String.Empty || textBox12.Text != String.Empty)
            {
                string strNum = textBox13.Text.Trim();
                double Num;
                bool isNum = double.TryParse(strNum, out Num);
                if (isNum == false) { label2.Visible = true; return; } else { label2.Visible = false; }


                string strNum2 = textBox12.Text.Trim();
                double Num2;
                bool isNum2 = double.TryParse(strNum2, out Num2);
                if (isNum2 == false) { label24.Visible = true; return; } else { label24.Visible = false; }

            }


            this.Save('N');
            lblMsg.Text = "Save Complete";
            lblMsg.Visible = true;
            timer1.Enabled = true;
        }
        private void SaveNew(char doctype)
        {
            trn_ques_aviation currentquest = (trn_ques_aviation)trnquesavaitionBindingSource.Current;
            trn_ques_aviation Newcurrentquest = (trn_ques_aviation)trnNewquesavaitionBindingSource.Current;

            Newcurrentquest.tpr_id = Program.CurrentHDR.tpr_id;
            Newcurrentquest.tqa_place_exam = txtplace.Text;
            Newcurrentquest.tqa_type = doctype;
            Newcurrentquest.tqa_doc_type = Program.CurrentRegis.tpr_aviation_type;
            Newcurrentquest.tqa_update_by = Program.CurrentUser.mut_username;
            Newcurrentquest.tqa_update_date = Program.GetServerDateTime();

            Newcurrentquest.tqa_place_exam = currentquest.tqa_place_exam;
            Newcurrentquest.tqa_th_fullname = txtname_th.Text;
            Newcurrentquest.tqa_en_fullname = txtname_en.Text;
            Newcurrentquest.tqa_th_nation = txtnation_th.Text;
            Newcurrentquest.tqa_en_nation = txtnation_en.Text;

            if (doctype == 'N')
            {
                Newcurrentquest.tqa_confirm_doctor = Program.CurrentUser.mut_username;
                Newcurrentquest.tqa_confirm_date = Program.GetServerDateTime();
            }

            //find dob
            DateTime dob = (DateTime)(from t1 in dbc.trn_patients where t1.tpt_id == Program.CurrentRegis.tpt_id select t1.tpt_dob).FirstOrDefault();

            Newcurrentquest.tqa_dob = dob;
            //Newcurrentquest.tqa_dob =Program.CurrentRegis.trn_patient.tpt_dob;
            Newcurrentquest.tqa_age_yrs = Convert.ToDouble(txtage.Text);
            Newcurrentquest.tqa_age_month = Convert.ToDouble(txtmonth.Text);
            Newcurrentquest.tqa_avia_type = Program.GetValueRadioTochar(pnlaviatype);
            Newcurrentquest.tqa_avia_oths = currentquest.tqa_avia_oths;
            Newcurrentquest.tqa_license_no = currentquest.tqa_license_no;
            Newcurrentquest.tqa_prev_exam_deca = Program.GetValueRadioTochar(pnlprev_exam_decla);

            //Newcurrentquest.tqa_chge_address = txtaddress.Text;

            Newcurrentquest.tqa_prev_exam_loc = currentquest.tqa_prev_exam_loc;

            Newcurrentquest.tqa_use_medicine = Program.GetValueRadioTochar(pnluse_med);
            Newcurrentquest.tqa_med_name = currentquest.tqa_med_name;

            //tab 1
            Newcurrentquest.tqa_sex = Program.GetValueGroupBox(grbgender);
 
            Newcurrentquest.tqa_prev_exam_deca = Program.GetValueRadioTochar(pnlprev_exam_decla);

            Newcurrentquest.tqa_med_reason = txtmed_reason.Text;
            Newcurrentquest.tqa_med_amount = txtmed_amt.Text;
            Newcurrentquest.tqa_med_name = txtmed_name.Text;
            Newcurrentquest.tqa_avia_oths = txtavia_oth.Text;

            //tab 2
            Newcurrentquest.tqa_chis_freq = Program.GetValueRadioTochar(pnlchis_freq);
            Newcurrentquest.tqa_chis_dizz = Program.GetValueRadioTochar(pnlchis_dizz);
            Newcurrentquest.tqa_chis_unco = Program.GetValueRadioTochar(pnlchis_unco);
            Newcurrentquest.tqa_chis_eyet = Program.GetValueRadioTochar(pnlchis_eyes);
            Newcurrentquest.tqa_chis_hayf = Program.GetValueRadioTochar(pnlchis_hayf);
            Newcurrentquest.tqa_chis_hert = Program.GetValueRadioTochar(pnlchis_heart);
            Newcurrentquest.tqa_chis_lung = Program.GetValueRadioTochar(pnlchis_lung);
            Newcurrentquest.tqa_chis_high = Program.GetValueRadioTochar(pnlchis_high);
            Newcurrentquest.tqa_chis_stom = Program.GetValueRadioTochar(pnlchis_stomach);
            Newcurrentquest.tqa_chis_alco = Program.GetValueRadioTochar(pnlchis_alco);
            Newcurrentquest.tqa_chis_nurv = Program.GetValueRadioTochar(pnlchis_nurv);
            Newcurrentquest.tqa_chis_drug = Program.GetValueRadioTochar(pnlchis_drug);
            Newcurrentquest.tqa_chis_suic = Program.GetValueRadioTochar(pnlchis_suic);
            Newcurrentquest.tqa_chis_moti = Program.GetValueRadioTochar(pnlchis_moti);
            Newcurrentquest.tqa_chis_adms = Program.GetValueRadioTochar(pnlchis_adm);
            Newcurrentquest.tqa_chis_avia = Program.GetValueRadioTochar(pnlchis_avia);
            Newcurrentquest.tqa_chis_otha = Program.GetValueRadioTochar(pnlchis_otha);
            Newcurrentquest.tqa_chis_gyna = Program.GetValueRadioTochar(pnlchis_gyna);
            Newcurrentquest.tqa_chis_othi = Program.GetValueRadioTochar(pnlchis_othi);
            Newcurrentquest.tqa_chis_conviction = Program.GetValueRadioTochar(pnlconviction);

            Newcurrentquest.tqa_chis_freq_rmk = currentquest.tqa_chis_freq_rmk;
            Newcurrentquest.tqa_chis_dizz_rmk = currentquest.tqa_chis_dizz_rmk;
            Newcurrentquest.tqa_chis_unco_rmk = currentquest.tqa_chis_unco_rmk;
            Newcurrentquest.tqa_chis_eyet_rmk = currentquest.tqa_chis_eyet_rmk;
            Newcurrentquest.tqa_chis_lung_rmk = currentquest.tqa_chis_lung_rmk;
            Newcurrentquest.tqa_chis_alco_rmk = currentquest.tqa_chis_alco_rmk;
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
            Newcurrentquest.tqa_chge_address = Program.GetValueRadioTochar(pnlchkaddr);

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

            Newcurrentquest.tqa_tot_fling_time = textBox12.Text == String.Empty ? 0 : Convert.ToDouble(textBox12.Text);
            Newcurrentquest.tqa_last_six_time = textBox13.Text == String.Empty ? 0 : Convert.ToDouble(textBox13.Text);

            trnNewquesavaitionBindingSource.EndEdit();
            dbc.SubmitChanges();
        }
        private void Update(char doctype)
        {
            trn_ques_aviation currentquest = (trn_ques_aviation)trnquesavaitionBindingSource.Current;

            currentquest.tpr_id = Program.CurrentRegis.tpr_id;
            currentquest.tqa_type = doctype;
            currentquest.tqa_doc_type = Program.CurrentRegis.tpr_aviation_type;
            currentquest.tqa_update_by = Program.CurrentUser.mut_username;
            currentquest.tqa_update_date = Program.GetServerDateTime();

            if (doctype == 'N')
            {
                currentquest.tqa_confirm_doctor = Program.CurrentUser.mut_username;
                currentquest.tqa_confirm_date = Program.GetServerDateTime();
            }

            currentquest.tqa_place_exam = txtplace.Text;
            currentquest.tqa_th_fullname = txtname_th.Text;
            currentquest.tqa_en_fullname = txtname_en.Text;
            currentquest.tqa_th_nation = txtnation_th.Text;
            currentquest.tqa_en_nation = txtnation_en.Text;
            currentquest.tqa_age_yrs = Convert.ToDouble(txtage.Text);
            currentquest.tqa_age_month = Convert.ToDouble(txtmonth.Text);
            currentquest.tqa_th_address = txtaddress.Text;


            //find dob
            DateTime dob = (DateTime)(from t1 in dbc.trn_patients where t1.tpt_id == Program.CurrentRegis.tpt_id select t1.tpt_dob).FirstOrDefault();

            currentquest.tqa_dob = dob;
            //currentquest.tqa_dob = Program.CurrentRegis.trn_patient.tpt_dob;

            //tab 1
            currentquest.tqa_sex = Program.GetValueGroupBox(grbgender);
            currentquest.tqa_avia_type = Program.GetValueRadioTochar(pnlaviatype);

            currentquest.tqa_prev_exam_deca = Program.GetValueRadioTochar(pnlprev_exam_decla);
            currentquest.tqa_use_medicine = Program.GetValueRadioTochar(pnluse_med);
            currentquest.tqa_med_amount = txtmed_amt.Text;
            currentquest.tqa_med_reason = txtmed_reason.Text;
            currentquest.tqa_med_name = txtmed_name.Text;
            currentquest.tqa_avia_oths = txtavia_oth.Text;
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
            currentquest.tqa_chis_hayf_rmk = txtalle.Text;
            currentquest.tqa_chis_lung = Program.GetValueRadioTochar(pnlchis_lung);
            currentquest.tqa_chis_lung_rmk = txtlung.Text;
            currentquest.tqa_chis_kidn = Program.GetValueRadioTochar(pnlchis_kind);
            currentquest.tqa_chis_kidn_rmk = txtkind.Text;
            currentquest.tqa_chis_ment = Program.GetValueRadioTochar(pnlchis_mental);
            currentquest.tqa_chis_ment_rmk = txtmental.Text;
            currentquest.tqa_chis_alco = Program.GetValueRadioTochar(pnlchis_alco);
            currentquest.tqa_chis_alco_rmk = txtalco.Text;
            currentquest.tqa_chis_hert = Program.GetValueRadioTochar(pnlchis_heart);
            currentquest.tqa_chis_hert_rmk = txtheart.Text;
            currentquest.tqa_chis_high = Program.GetValueRadioTochar(pnlchis_high);
            currentquest.tqa_chis_high_rmk = txthigh.Text;
            currentquest.tqa_chis_stom = Program.GetValueRadioTochar(pnlchis_stomach);
            currentquest.tqa_chis_stom_rmk = txtstomach.Text;
            currentquest.tqa_chis_nurv = Program.GetValueRadioTochar(pnlchis_nurv);
            currentquest.tqa_chis_nurv_rmk = txtneur.Text;
            currentquest.tqa_chis_drug = Program.GetValueRadioTochar(pnlchis_drug);
            currentquest.tqa_chis_drug_rmk = txtdrug.Text;
            currentquest.tqa_chis_suic = Program.GetValueRadioTochar(pnlchis_suic);
            currentquest.tqa_chis_suic_rmk = txtsuic.Text;
            currentquest.tqa_chis_moti = Program.GetValueRadioTochar(pnlchis_moti);
            currentquest.tqa_chis_moti_rmk = txtmotion.Text;
            currentquest.tqa_chis_adms = Program.GetValueRadioTochar(pnlchis_adm);
            currentquest.tqa_chis_adms_rmk = txtadm.Text;
            currentquest.tqa_chis_avia = Program.GetValueRadioTochar(pnlchis_avia);
            currentquest.tqa_chis_avia_rmk = txtavia.Text;
            currentquest.tqa_chis_otha = Program.GetValueRadioTochar(pnlchis_otha);
            currentquest.tqa_chis_otha_rmk = txtotha.Text;
            currentquest.tqa_chis_gyna = Program.GetValueRadioTochar(pnlchis_gyna);
            currentquest.tqa_chis_gyna_rmk = txtgyne.Text;
            currentquest.tqa_chis_othi = Program.GetValueRadioTochar(pnlchis_othi);
            currentquest.tqa_chis_othi_rmk = txtothi.Text;
            currentquest.tqa_chis_conviction = Program.GetValueRadioTochar(pnlconviction);
            currentquest.tqa_chis_conv_rmk = txtconvic.Text;
            currentquest.tqa_chge_address = Program.GetValueRadioTochar(pnlchkaddr);

            currentquest.tqa_avia_oths = txtavia_oth.Text;


            currentquest.tqa_tot_fling_time = textBox12.Text == String.Empty ? 0 : Convert.ToDouble(textBox12.Text);
            currentquest.tqa_last_six_time = textBox13.Text == String.Empty ? 0 : Convert.ToDouble(textBox13.Text);


            trnquesavaitionBindingSource.EndEdit();
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
        private void Save(char docType)
        {

            if (Program.CurrentRegis != null)
            {

                 //find HN
                var objHN1 = (from t1 in dbc.trn_patient_regis join t2 in dbc.trn_patients on t1.tpt_id equals t2.tpt_id where t1.tpt_id == Program.CurrentRegis.tpt_id select new { t1,t2 }).OrderByDescending(c => c.t1.tpr_create_date).FirstOrDefault();

                //EN No Lastest
                string objHN2 = Program.CurrentRegis.tpr_en_no;


                if (objHN1.t1.tpr_en_no == objHN2)
                {
                    this.Update(docType);
                }
                else
                {
                    this.SaveNew(docType);
                }

                //trn_ques_aviation currentquest = (trn_ques_aviation)trnquesavaitionBindingSource.Current;

                //if (currentquest.tqa_update_date != null)
                //{
                //    if (((DateTime)currentquest.tqa_update_date).Date < Program.GetServerDateTime().Date)
                //    {
                //        this.SaveNew(docType);
                //    }
                //    else
                //    {
                //        this.Update(docType);
                //    }

                //}
                //else
                //{
                //    this.Update(docType);
                //}

                
                timer1.Enabled = true;
                lblMsg.Visible = true;
                this.LoadTransaction();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                if (i == 10)
                {
                    lblMsg.Visible = false;
                    timer1.Enabled = false;
                }
            }
        }

        private void radioButton40_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton40.Checked == true)
            {
                txtfreq.Enabled = true;
            }
            else
            {
                txtfreq.Enabled = false;
                txtfreq.Text = String.Empty;
                txtfreq.DataBindings.Clear();
            }
        }
        private void radioButton43_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton43.Checked == true)
            {
                txtunco.Enabled = true;
            }
            else
            {
                txtunco.Enabled = false;
                txtunco.Text = String.Empty;
                txtunco.DataBindings.Clear();
            }
        }
        private void radioButton45_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton45.Checked == true)
            {
                txteye.Enabled = true;
            }
            else
            {
                txteye.Enabled = false;
                txteye.Text = String.Empty;
                txteye.DataBindings.Clear();
            }
        }
        private void radioButton47_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton47.Checked == true)
            {
                txtalle.Enabled = true;
            }
            else
            {
                txtalle.Enabled = false;
                txtalle.Text = String.Empty;
                txtalle.DataBindings.Clear();
            }
        }
        private void radioButton49_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton49.Checked == true)
            {
                txtlung.Enabled = true;
            }
            else
            {
                txtlung.Enabled = false;
                txtlung.Text = String.Empty;
                txtlung.DataBindings.Clear();
            }
        }
        private void radioButton51_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton51.Checked == true)
            {
                txtheart.Enabled = true;
            }
            else
            {
                txtheart.Enabled = false;
                txtheart.Text = String.Empty;
                txtheart.DataBindings.Clear();
            }
        }
        private void radioButton53_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton53.Checked == true)
            {
                txthigh.Enabled = true;
            }
            else
            {
                txthigh.Enabled = false;
                txthigh.Text = String.Empty;
                txthigh.DataBindings.Clear();
            }
        }
        private void radioButton55_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton55.Checked == true)
            {
                txtstomach.Enabled = true;
            }
            else
            {
                txtstomach.Enabled = false;
                txtstomach.Text = String.Empty;
                txtstomach.DataBindings.Clear();
            }
        }
        private void radioButton57_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton57.Checked == true)
            {
                txtkind.Enabled = true;
            }
            else
            {
                txtkind.Enabled = false;
                txtkind.Text = String.Empty;
                txtkind.DataBindings.Clear();
            }
        }
        private void radioButton59_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton59.Checked == true)
            {
                txtgyne.Enabled = true;
            }
            else
            {
                txtgyne.Enabled = false;
                txtgyne.Text = String.Empty;
                txtgyne.DataBindings.Clear();
            }
        }
        private void radioButton61_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton61.Checked == true)
            {
                txtneur.Enabled = true;
            }
            else
            {
                txtneur.Enabled = false;
                txtneur.Text = String.Empty;
                txtneur.DataBindings.Clear();
            }
        }
        private void radioButton63_CheckedChanged(object sender, EventArgs e)
        {
                if (radioButton63.Checked == true)
                {
                    txtmotion.Enabled = true;
                }
                else
                {
                    txtmotion.Enabled = false;
                    txtmotion.Text = String.Empty;
                   //trnquesavaitionBindingSource.Current 
                }
        }
        private void radioButton65_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton65.Checked == true)
            {
                txtmental.Enabled = true;
            }
            else
            {
                txtmental.Enabled = false;
                txtmental.Text = String.Empty;
                txtmental.DataBindings.Clear();
            }
        }
        private void radioButton67_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton67.Checked == true)
            {
                txtsuic.Enabled = true;
            }
            else
            {
                txtsuic.Enabled = false;
                txtsuic.Text = String.Empty;
                txtsuic.DataBindings.Clear();
            }
        }
        private void radioButton69_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton69.Checked == true)
            {
                txtalco.Enabled = true;
            }
            else
            {
                txtalco.Enabled = false;
                txtalco.Text = String.Empty;
                txtalco.DataBindings.Clear();
            }
        }
        private void radioButton71_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton71.Checked == true)
            {
                txtdrug.Enabled = true;
            }
            else
            {
                txtdrug.Enabled = false;
                txtdrug.Text = String.Empty;
                txtdrug.DataBindings.Clear();
            }
        }
        private void radioButton73_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton73.Checked == true)
            {
                txtadm.Enabled = true;
            }
            else
            {
                txtadm.Enabled = false;
                txtadm.Text = String.Empty;
                txtadm.DataBindings.Clear();
            }
        }
        private void radioButton75_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton75.Checked == true)
            {
                txtavia.Enabled = true;
            }
            else
            {
                txtavia.Enabled = false;
                txtavia.Text = String.Empty;
                txtavia.DataBindings.Clear();
            }
        }
        private void radioButton77_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton77.Checked == true)
            {
                txtotha.Enabled = true;
            }
            else
            {
                txtotha.Enabled = false;
                txtotha.Text = String.Empty;
                txtotha.DataBindings.Clear();
            }
        }
        private void radioButton79_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton79.Checked == true)
            {
                txtconvic.Enabled = true;
            }
            else
            {
                txtconvic.Enabled = false;
                txtconvic.Text = String.Empty;
                txtconvic.DataBindings.Clear();
            }
        }
        private void radioButton81_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton81.Checked == true)
            {
                txtothi.Enabled = true;
            }
            else
            {
                txtothi.Enabled = false;
                txtothi.Text = String.Empty;
                txtothi.DataBindings.Clear();
            }
        }
        private void radioButton40_CheckedChanged_1(object sender, EventArgs e)
        {
            if (radioButton40.Checked == true)
            {
                txtfreq.Enabled = true;
            }
            else
            {
                txtfreq.Enabled = false;
                txtfreq.Text = String.Empty;
                txtfreq.DataBindings.Clear();
            }
        }
        private void radioButton41_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton41.Checked == true)
            {
                txtdizz.Enabled = true;
            }
            else
            {
                txtdizz.Enabled = false;
                txtdizz.Text = String.Empty;
                txtdizz.DataBindings.Clear();
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string> { "QA102" };
            int tprID = 0;
            if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.previewReport();
            //ClsReport.previewRpt(new List<string> { "QA102" });
        }

        private void uiProfileHorizontal1_Load(object sender, EventArgs e)
        {
            uiProfileHorizontal1.Loaddata();
        }

        //private void textBox12_TextChanged(object sender, EventArgs e)
        //{
        //    string strNum = textBox12.Text.Trim();
        //    double Num;
        //    bool isNum = double.TryParse(strNum, out Num);
        //    if (isNum == false) { label24.Visible = true; return; } else { label24.Visible = false; }
        //}

        //private void textBox13_TextChanged(object sender, EventArgs e)
        //{
        //    string strNum = textBox13.Text.Trim();
        //    double Num;
        //    bool isNum = double.TryParse(strNum, out Num);
        //    if (isNum == false) { label2.Visible = true; return; } else { label2.Visible = false; }
        //}

    }
}
