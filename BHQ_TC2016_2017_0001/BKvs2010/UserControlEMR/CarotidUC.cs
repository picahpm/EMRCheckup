using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.IO;
using BKvs2010.EmrClass;

namespace BKvs2010.UserControlEMR
{
    public partial class CarotidUC : UserControl
    {     
        public CarotidUC()
        {
            InitializeComponent();
            
            AutoCompleteDoctor obj = new AutoCompleteDoctor();
            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

            pictureBoxLeft.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pictureBoxLeft, new object[] { System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true });
            pictureBoxRight.GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).Invoke(pictureBoxRight, new object[] { System.Windows.Forms.ControlStyles.UserPaint | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | System.Windows.Forms.ControlStyles.DoubleBuffer, true });
            
        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_carotid_tech ct = bsCarotidTech.OfType<trn_carotid_tech>().FirstOrDefault();
                if (ct != null)
                {
                    if (e == null)
                    {
                        ct.tct_doctor_code = null;
                        ct.tct_doctor_name = null;
                        txtDocCode.Text = null;
                    }
                    else
                    {
                        ct.tct_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        ct.tct_doctor_name = ((DoctorProfile)e).CTPCP_Desc;
                        txtDocCode.Text = ((DoctorProfile)e).SSUSR_Initials;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }

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
                        trn_carotid_tech patientTCT = value.trn_carotid_teches.FirstOrDefault();
                        if (patientTCT == null)
                        {
                            patientTCT = new trn_carotid_tech();
                            value.trn_carotid_teches.Add(patientTCT);
                            txtmobilephone.Text = value.tpr_home_phone;
                        }                        
                        LoadLabResult(ref value);
                        LoadImage(ref value);
                        DoctorProfile dc = new DoctorProfile
                        {
                            SSUSR_Initials = patientTCT.tct_doctor_code != null ? patientTCT.tct_doctor_code : "",
                            CTPCP_Desc = patientTCT.tct_doctor_name != null ? patientTCT.tct_doctor_name : "",
                            CTPCP_Code = "",
                            CTCPT_Desc = "",
                            DoctorName = "",
                            CTPCP_SMCNo = ""
                        };                       
                        autoCompleteUC1.SelectedValue = dc.SSUSR_Initials;

                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;                        
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "patientRegis", ex, false);
                    }
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }

        public void EndEdit()
        {
            trn_carotid_tech obj = _PatientRegis.trn_carotid_teches.FirstOrDefault();
            if (autoCompleteUC1.SelectedItem != null)
            {
                DoctorProfile dc = (DoctorProfile)autoCompleteUC1.SelectedItem;
                obj.tct_doctor_code = dc.SSUSR_Initials;
                obj.tct_doctor_name = dc.CTPCP_Desc;
            }
            else
            {
                obj.tct_doctor_code = null;
                obj.tct_doctor_name = null;
            }           
        }

        private void rdnormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdnormal.Checked == true)
            {
                txtsumaryRemark.Enabled = true;
                txtsumaryRemark.Focus();
            }
            else
            {
                if (rdabnormal.Checked == false)
                {
                    txtsumaryRemark.Enabled = false;
                }
            }
        }
        private void rdabnormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdabnormal.Checked == true)
            {
                txtsumaryRemark.Enabled = true;
                txtsumaryRemark.Focus();
            }
            else
            {
                if (rdnormal.Checked == false)
                {
                    txtsumaryRemark.Enabled = false;
                }
            }
        }
        private void chkAdviceMec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdviceMec.Checked == true)
            {
                txtAdviceMec.Enabled = true;
                txtAdviceMec.Focus();
            }
            else
            {
                txtAdviceMec.Enabled = false;
                bsCarotidTech.OfType<trn_carotid_tech>().FirstOrDefault().tct_advice_med_rmk = null;
            }
        }
        private void chkAdviceDiet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdviceDiet.Checked == true)
            {
                txtAdviceDiet.Enabled = true;
                txtAdviceDiet.Focus();
            }
            else
            {
                txtAdviceDiet.Enabled = false;
                bsCarotidTech.OfType<trn_carotid_tech>().FirstOrDefault().tct_advice_dit_rmk = null;
            }
        }
        private void chkAdviceExercise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdviceExercise.Checked == true)
            {
                txtAdviceExercise.Enabled = true;
                txtAdviceExercise.Focus();
            }
            else
            {
                txtAdviceExercise.Enabled = false;
                bsCarotidTech.OfType<trn_carotid_tech>().FirstOrDefault().tct_advice_exr_rmk = null;
            }
        }
        private void chkConsult_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConsult.Checked == true)
            {
                txtconsult.Enabled = true;
                txtconsult.Focus();
            }
            else
            {
                txtconsult.Enabled=false;
                bsCarotidTech.OfType<trn_carotid_tech>().FirstOrDefault().tct_consult_card_rmk = null;
            }
        }
        private void chkfu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkfu.Checked == true)
            {
                txtfu.Enabled = true;
                txtfu.Focus();
            }
            else
            {
                txtfu.Enabled = false;
                bsCarotidTech.OfType<trn_carotid_tech>().FirstOrDefault().tct_fu_carotid_rmk = null;
            }
        }

        private void LoadLabResult(ref trn_patient_regi _patient_regis)
        {
            try
            {
                trn_patient_regi objrg = _patient_regis;
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    int tpr_id = objrg.tpr_id;

                    var ObjLabFbs = (from t1 in dbc.trn_patient_labs
                                     where t1.tpl_hn_no == (Program.CurrentRegis == null ? objrg.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no)
                                     && t1.tpl_en_no == (Program.CurrentPatient_queue == null ? objrg.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no)
                                     && t1.tpl_lab_no == "C0180"
                                     select t1.tpl_lab_value).FirstOrDefault();
                    var ObjLabCholes = (from t1 in dbc.trn_patient_labs
                                        where t1.tpl_hn_no == (Program.CurrentRegis == null ? objrg.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no)
                                        && t1.tpl_en_no == (Program.CurrentPatient_queue == null ? objrg.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no) //Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no
                                        && t1.tpl_lab_no == "C0130"
                                        select t1.tpl_lab_value).FirstOrDefault();
                    var ObjLabBmi = (from t1 in dbc.trn_basic_measure_dtls
                                     join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                                     where t2.tpr_id == tpr_id//tprid
                                     select t1.tbd_bmi).FirstOrDefault();
                    var ObjLabHbA = (from t1 in dbc.trn_patient_labs
                                     where t1.tpl_hn_no == (Program.CurrentRegis == null ? Program.CurrentRegis.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no)
                                        && t1.tpl_en_no == (Program.CurrentPatient_queue == null ? Program.CurrentRegis.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no) //Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no
                                        && t1.tpl_lab_no == "N0510"
                                     select t1.tpl_lab_value).FirstOrDefault();
                    var ObjLabLdl = (from t1 in dbc.trn_patient_labs
                                     where t1.tpl_hn_no == (Program.CurrentRegis == null ? Program.CurrentRegis.trn_patient.tpt_hn_no : Program.CurrentRegis.trn_patient.tpt_hn_no) //Program.CurrentRegis.trn_patient.tpt_hn_no
                                     && t1.tpl_en_no == (Program.CurrentPatient_queue == null ? Program.CurrentRegis.tpr_en_no : Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no) //Program.CurrentPatient_queue.trn_patient_regi.tpr_en_no
                                     && t1.tpl_lab_no == "C0159"
                                     select t1.tpl_lab_value).FirstOrDefault();
                    var ObjLabBp = (from t1 in dbc.trn_basic_measure_dtls
                                    join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                                    where t2.tpr_id == tpr_id
                                    select t1.tbd_systolic + "/" + t1.tbd_diastolic).FirstOrDefault();
                                        
                    //var ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1.tpr_mobile_phone).FirstOrDefault();
                    //if (ObjMobile == null || ObjMobile == "")
                    //{
                    //    ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1.tpr_office_phone).FirstOrDefault();
                    //}
                    //if (ObjMobile == null || ObjMobile == "")
                    //{
                    //    ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1.tpr_home_phone).FirstOrDefault();
                    //}

                    txtfbs.Text = ObjLabFbs;
                    txtcho.Text = ObjLabCholes;
                    txtbmi.Text = ObjLabBmi;
                    txthb.Text = ObjLabHbA;
                    txtldl.Text = ObjLabLdl;
                    txtbp.Text = ObjLabBp;
                    //txtmobilephone.Text = ObjMobile;
                }
            }
            catch 
            { }
        }
        private void LoadImage(ref trn_patient_regi _patient_regis)
        {  
            trn_carotid_tech objCarotid = _patient_regis.trn_carotid_teches.FirstOrDefault();
            if (objCarotid != null)
            {    
                if (objCarotid.tct_left_result != null)
                {
                    object objimg_r = objCarotid.tct_right_result.ToArray();
                    byte[] data1;
                    data1 = (byte[])objimg_r;
                    MemoryStream ms1 = new MemoryStream(data1);
                    pictureBoxRight.Image = Image.FromStream(ms1);
                }
                else
                { 
                    pictureBoxRight.Image = Properties.Resources.carotid_1;
                }

                if (objCarotid.tct_right_result != null)
                {
                    object objimg_l = objCarotid.tct_left_result.ToArray();
                    byte[] data2;
                    data2 = (byte[])objimg_l;
                    MemoryStream ms2 = new MemoryStream(data2);
                    pictureBoxLeft.Image = Image.FromStream(ms2);
                }
                else
                { 
                    pictureBoxLeft.Image = Properties.Resources.carotid_2;
                }
            }
            else
            {
                pictureBoxRight.Image = Properties.Resources.carotid_1;
                pictureBoxLeft.Image = Properties.Resources.carotid_2;
            }
        }

        private void rdresult1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdresult1.Checked == true)
            {
                cmblist1.Enabled = true;
            }
            else
            {
                cmblist1.Enabled = false;
                bsCarotidTech.OfType<trn_carotid_tech>().FirstOrDefault().tct_appoint_depart = null;
            }
        }

        private void chkappoint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkappoint.Checked == true)
            {
                dateTimeAppoint.Enabled = true;
            }
            else
            {
                dateTimeAppoint.Enabled = false;
            }
        }

    }
}
