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
    public partial class SubjectiveUC : UserControl
    {
        AddSurgeryFrm frmSur;
        AddAdmitionFrm frmAdm;
        public SubjectiveUC()
        {
            InitializeComponent();
            GridSurgery.AutoGenerateColumns = false;
            GridSurgery.DataSource = bsSurgery;
            GridAdmit.AutoGenerateColumns = false;
            GridAdmit.DataSource = bsAdmit;
            frmSur = new AddSurgeryFrm();
            frmAdm = new AddAdmitionFrm();
        }

        public string username { get; set; }
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
                        trn_ques_patient quesPatient = value.trn_ques_patients.FirstOrDefault();
                        if (quesPatient == null)
                        {
                            quesPatient = new trn_ques_patient();
                            value.trn_ques_patients.Add(quesPatient);
                        }
                        quesPatient.PropertyChanged += new PropertyChangedEventHandler(quesPatient_PropertyChanged);
                        quesPatient_PropertyChanged(quesPatient, new PropertyChangedEventArgs("tqp_ill_med_canc"));
                        quesPatient_PropertyChanged(quesPatient, new PropertyChangedEventArgs("tqp_ill_med_oth"));
                        trn_doctor_hdr doctorHdr = value.trn_doctor_hdrs.FirstOrDefault();
                        if (doctorHdr == null)
                        {
                            doctorHdr = new trn_doctor_hdr();
                            value.trn_doctor_hdrs.Add(doctorHdr);
                            LoadQuestionare(ref value);
                        }
                        doctorHdr.PropertyChanged += new PropertyChangedEventHandler(doctorHdr_PropertyChanged);
                        doctorHdr_PropertyChanged(doctorHdr, new PropertyChangedEventArgs("trh_hypertension"));
                        doctorHdr_PropertyChanged(doctorHdr, new PropertyChangedEventArgs("trh_heart_disease"));
                        doctorHdr_PropertyChanged(doctorHdr, new PropertyChangedEventArgs("trh_diabetes"));
                        doctorHdr_PropertyChanged(doctorHdr, new PropertyChangedEventArgs("trh_stroke"));
                        doctorHdr_PropertyChanged(doctorHdr, new PropertyChangedEventArgs("trh_seizure"));
                        doctorHdr_PropertyChanged(doctorHdr, new PropertyChangedEventArgs("trh_cancer"));
                        doctorHdr_PropertyChanged(doctorHdr, new PropertyChangedEventArgs("trh_other"));
                        bsPatient.DataSource = value.trn_patient;
                        _PatientRegis = value;
                        bsPatientRegis.DataSource = value;
                        GridSurgery.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        GridAdmit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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
        private void quesPatient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "tqp_ill_med_canc":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_cancer.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["tqp_ill_med_canc_oth"].SetValue(sender, null);
                        }
                    }
                    break;
                case "tqp_ill_med_oth":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_other.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["tqp_ill_med_others"].SetValue(sender, null);
                        }
                    }
                    break;
            }
        }
        private void doctorHdr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "trh_hypertension":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_cancer.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["trh_hypertension_txt"].SetValue(sender, null);
                        }
                    }
                    break;
                case "trh_heart_disease":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_other.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["trh_heart_disease_txt"].SetValue(sender, null);
                        }
                    }
                    break;
                case "trh_diabetes":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_cancer.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["trh_diabetes_txt"].SetValue(sender, null);
                        }
                    }
                    break;
                case "trh_stroke":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_other.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["trh_stroke_txt"].SetValue(sender, null);
                        }
                    }
                    break;
                case "trh_seizure":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_cancer.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["trh_seizure_txt"].SetValue(sender, null);
                        }
                    }
                    break;
                case "trh_cancer":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_other.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["trh_cancer_txt"].SetValue(sender, null);
                        }
                    }
                    break;
                case "trh_other":
                    {
                        var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                        bool flag = (val == null ? false : (bool)val);
                        txt_med_cancer.Enabled = flag;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["trh_other_txt"].SetValue(sender, null);
                        }
                    }
                    break;
            }
        }

        private void LoadQuestionare(ref trn_patient_regi _patientRegis)
        {
            trn_ques_patient quesPatient = _patientRegis.trn_ques_patients.FirstOrDefault();
            if (quesPatient != null)
            {
                trn_doctor_hdr doctorHdr = _patientRegis.trn_doctor_hdrs.FirstOrDefault();
               
                if (quesPatient.tqp_ill_concern == 'A')
                {
                    doctorHdr.trh_chief_complaint = "Annaul Check-up";
                }
                else if (quesPatient.tqp_ill_concern == 'O')
                {
                    if (quesPatient.tqp_ill_conc_oth != null && quesPatient.tqp_ill_conc_oth != "")
                    {
                        doctorHdr.trh_chief_complaint = "Other { " + quesPatient.tqp_ill_conc_oth + " }";
                    }
                    else
                    {
                        doctorHdr.trh_chief_complaint = "Other";
                    }
                }
                else
                {
                    doctorHdr.trh_chief_complaint = "N/A";
                }

                List<string> remarkHyper = new List<string>();
                List<string> remarkHeart = new List<string>();
                List<string> remarkDiab = new List<string>();
                List<string> remarkStroke = new List<string>();
                List<string> remarkSeizure = new List<string>();
                List<string> remarkCancer = new List<string>();
                List<string> remarkOther = new List<string>();
                List<string> CurrentMedicine = new List<string>();


                if (quesPatient.tqp_ill_cur_med == 'H')
                {
                    if (quesPatient.tqp_ill_cmed_diab == true) { CurrentMedicine.Add("ยารักษาเบาหวาน"); }
                    if (quesPatient.tqp_ill_cmed_demia == true) { CurrentMedicine.Add("ยารักษาโรคไขมันในเลือดสูง"); }
                    if (quesPatient.tqp_ill_cmed_diab == true) { CurrentMedicine.Add("ไขมันในเลือดผิดปกติ"); }
                    if (quesPatient.tqp_ill_cmed_hyper == true) { CurrentMedicine.Add("ยารักษาโรคความดันโลหิตสูง"); }
                    if (quesPatient.tqp_ill_cmed_cardi == true) { CurrentMedicine.Add("ยารักษาโรคหัวใจและหลอดเลือด"); }
                    if (quesPatient.tqp_ill_cmed_horm == true) { CurrentMedicine.Add("ฮอร์โมน"); }
                    if (quesPatient.tqp_ill_cmed_oth == true) { CurrentMedicine.Add("ยาอื่นๆ(" + quesPatient.tqp_ill_cmed_others + ")"); }
                }

                if (quesPatient.tqp_fhis_f_disease == 'D')
                {

                    if (quesPatient.tqp_fhis_fdis_hyper == true) { remarkHyper.Add("บิดา"); chHypertension.Checked = true; }
                    if (quesPatient.tqp_fhis_fdis_heart == true) { remarkHeart.Add("บิดา"); chHeartDisease.Checked = true; }
                    if (quesPatient.tqp_fhis_fdis_diab == true) { remarkDiab.Add("บิดา"); chDiabetes.Checked = true; }
                    if (quesPatient.tqp_fhis_fdis_stro == true) { remarkStroke.Add("บิดา"); chStroke.Checked = true; }
                    if (quesPatient.tqp_fhis_fdis_canc == true) { remarkCancer.Add("บิดา"); chCancer.Checked = true; }
                    if (quesPatient.tqp_fhis_fdis_oth == true) { remarkOther.Add("(บิดา) : " + quesPatient.tqp_fhis_fdis_others); chOther.Checked = true; }

                }

                if (quesPatient.tqp_fhis_m_disease == 'D')
                {

                    if (quesPatient.tqp_fhis_mdis_hyper == true) { remarkHyper.Add("มารดา"); chHypertension.Checked = true; }
                    if (quesPatient.tqp_fhis_mdis_heart == true) { remarkHeart.Add("มารดา"); chHeartDisease.Checked = true; }
                    if (quesPatient.tqp_fhis_mdis_diab == true) { remarkDiab.Add("มารดา"); chDiabetes.Checked = true; }
                    if (quesPatient.tqp_fhis_mdis_stro == true) { remarkStroke.Add("มารดา"); chStroke.Checked = true; }
                    if (quesPatient.tqp_fhis_mdis_canc == true) { remarkCancer.Add("มารดา"); chCancer.Checked = true; }
                    if (quesPatient.tqp_fhis_mdis_oth == true) { remarkOther.Add("(มารดา) : " + quesPatient.tqp_fhis_mdis_others); chOther.Checked = true; }
                }

                if (quesPatient.tqp_fhis_b_disease == 'D')
                {
                    if (quesPatient.tqp_fhis_bdis_hyper == true) { remarkHyper.Add("พี่น้อง"); chHypertension.Checked = true; }
                    if (quesPatient.tqp_fhis_bdis_heart == true) { remarkHeart.Add("พี่น้อง"); chHeartDisease.Checked = true; }
                    if (quesPatient.tqp_fhis_bdis_diab == true) { remarkDiab.Add("พี่น้อง"); chDiabetes.Checked = true; }
                    if (quesPatient.tqp_fhis_bdis_stro == true) { remarkStroke.Add("พี่น้อง"); chStroke.Checked = true; }
                    if (quesPatient.tqp_fhis_bdis_canc == true) { remarkCancer.Add("พี่น้อง"); chCancer.Checked = true; }
                    if (quesPatient.tqp_fhis_bdis_oth == true) { remarkOther.Add("(พี่น้อง) : " + quesPatient.tqp_fhis_bdis_others); chOther.Checked = true; }
                }

                doctorHdr.trh_hypertension_txt = string.Join(", ", remarkHyper);
                doctorHdr.trh_heart_disease_txt = string.Join(", ", remarkDiab);
                doctorHdr.trh_stroke_txt = string.Join(", ", remarkStroke);
                doctorHdr.trh_cancer_txt = string.Join(", ", remarkCancer);
                doctorHdr.trh_other_txt = string.Join(", ", remarkOther);

                if (CurrentMedicine.Count != 0)
                {
                    gv_cur_med.Rows.Clear();
                    for (int i = 0; i < CurrentMedicine.Count; i++)
                    {
                        gv_cur_med.Rows.Add(i + 1, CurrentMedicine[i], "Delete", "O");
                    }

                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            _PatientRegis = null;
        }
        public void EndEdit()
        {

        }

        private void btnAddSurgery_Click(object sender, EventArgs e)
        {
            frmSur.username = username;
            frmSur.Patient = this.bsPatient.OfType<trn_patient>().FirstOrDefault();
            frmSur.ShowDialog();
            GridSurgery.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void btnAddHAdmit_Click(object sender, EventArgs e)
        {
            frmAdm.username = username;
            frmAdm.Patient = this.bsPatient.OfType<trn_patient>().FirstOrDefault();
            frmAdm.ShowDialog();
            GridAdmit.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public void RefreshBindings()
        {
            PatientRegis = _PatientRegis;
        }
    }
}
