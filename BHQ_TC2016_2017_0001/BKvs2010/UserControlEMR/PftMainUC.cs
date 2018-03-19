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
using BKvs2010.EmrClass;

namespace BKvs2010.UserControlEMR
{
    public partial class PftMainUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public PftMainUC()
        {
            InitializeComponent();
            
            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

            setDestination();
            List<CheckBox> chkBox = destination.Select(x => x.checkBox).ToList();
            chkBox.ForEach(x => x.CheckedChanged += new EventHandler(chkBox_CheckedChanged));
        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_pft pft = bsPatientPFT.OfType<trn_pft>().FirstOrDefault();
                if (pft != null)
                {
                    if (e == null)
                    {
                        txtDoctorCode.Text = "";
                        pft.tpf_doc_code = null;
                        pft.tpf_doctor_license = null;
                        pft.tpf_doctor_name_en = null;
                        pft.tpf_doctor_name_th = null;
                    }
                    else
                    {
                        txtDoctorCode.Text = ((DoctorProfile)e).SSUSR_Initials;
                        pft.tpf_doc_code = ((DoctorProfile)e).SSUSR_Initials;
                        pft.tpf_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        pft.tpf_doctor_name_en = dn.NameEN;
                        pft.tpf_doctor_name_th = dn.NameTH;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }

        private bool _isDoctorRoom = false;
        public bool isDoctorRoom
        {
            get { return _isDoctorRoom; }
            set { _isDoctorRoom = value; }
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
                        trn_ques_patient patientQuest = value.trn_ques_patients.FirstOrDefault();
                        if (patientQuest == null)
                        {
                            patientQuest = new trn_ques_patient();
                            value.trn_ques_patients.Add(patientQuest);
                        }
                        patientQuest.PropertyChanged += new PropertyChangedEventHandler(patientQuest_PropertyChanged);
                        trn_pft patientPft = value.trn_pfts.FirstOrDefault();
                        if (patientPft == null)
                        {
                            patientPft = new trn_pft();
                            value.trn_pfts.Add(patientPft);
                        }                        

                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;

                        if (_isDoctorRoom == true)
                        {
                            autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                            autoCompleteUC1.Enabled = false;
                        }
                        else
                        {
                            autoCompleteUC1.SelectedValue = patientPft.tpf_doc_code;
                        }
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
        }

        private class DestinationControl
        {
            public CheckBox checkBox { get; set; }
            public List<string> fieldName { get; set; }
        }
        private class DestinationField
        {
            public List<string> fieldName { get; set; }
        }
        private List<DestinationControl> destination;
        private void setDestination()
        {
            destination = new List<DestinationControl>
            {
                new DestinationControl 
                { 
                    checkBox = chk_none_asthma, 
                    fieldName = new List<string>
                    {
                        "tqp_ill_med_asth",
                        "tqp_fam_med_asth"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chk_none_bronchitis, 
                    fieldName = new List<string>
                    {
                        "tqp_ill_med_bron",
                        "tqp_fam_med_bron"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chk_none_allergy, 
                    fieldName = new List<string>
                    {
                        "tqp_ill_med_alle",
                        "tqp_fam_med_alle"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chk_none_cough, 
                    fieldName = new List<string>
                    {
                        "tqp_ill_med_cough",
                        "tqp_fam_med_cough"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chk_none_runnynose, 
                    fieldName = new List<string>
                    {
                        "tqp_ill_med_rhin",
                        "tqp_fam_med_rhin"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chk_none_oth, 
                    fieldName = new List<string>
                    {
                        "tqp_ill_med_oth",
                        "tqp_fam_med_oth"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chkNoneDust, 
                    fieldName = new List<string>
                    {
                        "tqp_envi_hme_dust",
                        "tqp_envi_off_dust"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chkNoneSmoke, 
                    fieldName = new List<string>
                    {
                        "tqp_envi_hme_smoke",
                        "tqp_envi_off_smoke"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chkNoneChem, 
                    fieldName = new List<string>
                    {
                        "tqp_envi_hme_chem",
                        "tqp_envi_off_chem"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chkNonePollen, 
                    fieldName = new List<string>
                    {
                        "tqp_envi_hme_pollen",
                        "tqp_envi_off_pollen"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chkNonePet, 
                    fieldName = new List<string>
                    {
                        "tqp_envi_hme_pet",
                        "tqp_envi_off_pet"
                    }
                },
                new DestinationControl 
                { 
                    checkBox = chkNoneOther, 
                    fieldName = new List<string>
                    {
                        "tqp_envi_hme_other",
                        "tqp_envi_off_other"
                    }
                }
                
                
            };
        }

        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            if (chkBox.Checked == true)
            {
                List<DestinationControl> desCtrl = destination.Where(x => x.checkBox == chkBox).ToList();
                foreach (DestinationControl des in desCtrl)
                {
                    foreach (string field in des.fieldName)
                    {
                        trn_ques_patient patient_ques = (trn_ques_patient)bsPatientQues.Current;
                        object value = TypeDescriptor.GetProperties(patient_ques)[field].GetValue(patient_ques);
                        if (value != null)
                        {
                            if (value.GetType() == typeof(bool))
                            {
                                if ((bool)value == true)
                                {
                                    TypeDescriptor.GetProperties(patient_ques)[field].SetValue(patient_ques, false);
                                }
                            }
                        }
                    }
                }
            }
        }

        List<string> propPant = new List<string> 
        { 
            "tqp_pat_per_morn", 
            "tqp_pat_per_aday",
            "tqp_pat_per_night",
            "tqp_pat_per_rarely",
            "tqp_pat_per_nsure",
            "tqp_pat_exercise",
            "tqp_pat_pros",
            "tqp_pat_still",
            "tqp_pat_nsure"
        };
        List<string> propCough = new List<string>
        {
            "tqp_cur_ill_cough",
            "tqp_cur_ill_wcough",
            "tqp_cur_ill_gcough",
            "tqp_cur_ill_bcough"
        };
        List<string> propTimeCough = new List<string>
        {
            "tqp_cou_per_morn",
            "tqp_cou_per_aday",
            "tqp_cou_per_night",
            "tqp_cou_per_rarely",
            "tqp_cou_per_nsure"
        };
        private void pft_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }
        private void patientQuest_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tqp_his_smok")
            {
                trn_ques_patient patient_ques = (trn_ques_patient)sender;
                if (patient_ques.tqp_his_smok == 'N') //tqp_his_smok N=None,O=No smoking,Q=Quit smoking,S=Smoking
                {
                    patient_ques.tqp_his_smok_amt = null;
                    patient_ques.tqp_his_smok_dur = null;

                    txt_his_smok_amt1.Enabled = false;
                    txt_his_smok_dur1.Enabled = false;
                    txt_his_smok_amt1.DataBindings.Clear();
                    txt_his_smok_dur1.DataBindings.Clear();

                    txt_his_smok_amt2.Enabled = false;
                    txt_his_smok_dur2.Enabled = false;
                    txt_his_smok_amt2.DataBindings.Clear();
                    txt_his_smok_dur2.DataBindings.Clear();

                    txtNSmokeYear.Enabled = false;
                    patient_ques.tqp_his_nsmok_yrs = null;

                    txtQsSmokeYear.Enabled = false;
                    patient_ques.tqp_his_qsmok_yrs = null;

                    txtQsSmokeRemark.Enabled = false;
                    patient_ques.tqp_his_smok_remark = null;
                }
                else if (patient_ques.tqp_his_smok == 'O')
                {
                    patient_ques.tqp_his_smok_amt = null;
                    patient_ques.tqp_his_smok_dur = null;

                    txt_his_smok_amt1.Enabled = false;
                    txt_his_smok_dur1.Enabled = false;
                    txt_his_smok_amt1.DataBindings.Clear();
                    txt_his_smok_dur1.DataBindings.Clear();

                    txt_his_smok_amt2.Enabled = false;
                    txt_his_smok_dur2.Enabled = false;
                    txt_his_smok_amt2.DataBindings.Clear();
                    txt_his_smok_dur2.DataBindings.Clear();

                    txtNSmokeYear.Enabled = true;
                    patient_ques.tqp_his_nsmok_yrs = null;

                    txtQsSmokeYear.Enabled = false;
                    patient_ques.tqp_his_qsmok_yrs = null;

                    txtQsSmokeRemark.Enabled = false;
                    patient_ques.tqp_his_smok_remark = null;
                }
                else if (patient_ques.tqp_his_smok == 'Q')
                {
                    patient_ques.tqp_his_smok_amt = null;
                    patient_ques.tqp_his_smok_dur = null;

                    txt_his_smok_amt1.DataBindings.Clear();
                    txt_his_smok_dur1.DataBindings.Clear();
                    txt_his_smok_amt1.Enabled = false;
                    txt_his_smok_dur1.Enabled = false;

                    txt_his_smok_amt2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientQues, "tqp_his_smok_amt", true));
                    txt_his_smok_dur2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientQues, "tqp_his_smok_dur", true));
                    txt_his_smok_amt2.Enabled = true;
                    txt_his_smok_dur2.Enabled = true;

                    txtNSmokeYear.Enabled = false;
                    patient_ques.tqp_his_nsmok_yrs = null;

                    txtQsSmokeYear.Enabled = true;
                    patient_ques.tqp_his_qsmok_yrs = null;

                    txtQsSmokeRemark.Enabled = true;
                    patient_ques.tqp_his_smok_remark = null;
                }
                else if (patient_ques.tqp_his_smok == 'S')
                {
                    patient_ques.tqp_his_smok_amt = null;
                    patient_ques.tqp_his_smok_dur = null;

                    txt_his_smok_amt1.Enabled = true;
                    txt_his_smok_dur1.Enabled = true;
                    txt_his_smok_amt1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientQues, "tqp_his_smok_amt", true));
                    txt_his_smok_dur1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPatientQues, "tqp_his_smok_dur", true));

                    txt_his_smok_amt2.Enabled = false;
                    txt_his_smok_dur2.Enabled = false;
                    txt_his_smok_amt2.DataBindings.Clear();
                    txt_his_smok_dur2.DataBindings.Clear();

                    txtNSmokeYear.Enabled = false;
                    patient_ques.tqp_his_nsmok_yrs = null;

                    txtQsSmokeYear.Enabled = false;
                    patient_ques.tqp_his_qsmok_yrs = null;

                    txtQsSmokeRemark.Enabled = false;
                    patient_ques.tqp_his_smok_remark = null;
                }
                else
                {
                    patient_ques.tqp_his_smok_amt = null;
                    patient_ques.tqp_his_smok_dur = null;

                    txt_his_smok_amt1.Enabled = false;
                    txt_his_smok_dur1.Enabled = false;
                    txt_his_smok_amt1.DataBindings.Clear();
                    txt_his_smok_dur1.DataBindings.Clear();

                    txt_his_smok_amt2.Enabled = false;
                    txt_his_smok_dur2.Enabled = false;
                    txt_his_smok_amt2.DataBindings.Clear();
                    txt_his_smok_dur2.DataBindings.Clear();

                    txtNSmokeYear.Enabled = false;
                    patient_ques.tqp_his_nsmok_yrs = null;

                    txtQsSmokeYear.Enabled = false;
                    patient_ques.tqp_his_qsmok_yrs = null;

                    txtQsSmokeRemark.Enabled = false;
                    patient_ques.tqp_his_smok_remark = null;
                }
            }
            else
            {
                if (e.PropertyName == "tqp_ill_med_heart")
                {
                    object value = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (value != null)
                    {
                        if (value.GetType() == typeof(bool))
                        {
                            if ((bool)value)
                            {
                                txt1_hearRemark.Enabled = true;
                            }
                            else
                            {
                                txt1_hearRemark.Enabled = false;
                                TypeDescriptor.GetProperties(sender)["tqp_ill_med_heart_txt"].SetValue(sender, null);
                            }
                        }
                    }
                }
                else if (e.PropertyName == "tqp_ill_med_oth")
                {
                    object value = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (value != null)
                    {
                        if (value.GetType() == typeof(bool))
                        {
                            if ((bool)value)
                            {
                                txt1_OtherRemark.Enabled = true;
                            }
                            else
                            {
                                txt1_OtherRemark.Enabled = false;
                                TypeDescriptor.GetProperties(sender)["tqp_ill_med_rmk_oth"].SetValue(sender, null);
                            }
                        }
                    }
                }
                else if (e.PropertyName == "tqp_cur_ill_pant")
                {
                    object value = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (value != null)
                    {
                        if (value.GetType() == typeof(char?) || value.GetType() == typeof(char))
                        {
                            if ((char)value == 'N')
                            {
                                propPant.ForEach(x => TypeDescriptor.GetProperties(sender)[x].SetValue(sender, false));
                                TypeDescriptor.GetProperties(sender)["tqp_pat_freq"].SetValue(sender, null);
                            }
                        }
                    }
                }
                else if (e.PropertyName == "tqp_pat_freq")
                {
                    object value = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (value != null)
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_cur_ill_pant"].SetValue(sender, 'Y');
                    }
                }
                else if (propPant.Contains(e.PropertyName))
                {
                    if (propPant.Any(x => (bool?)TypeDescriptor.GetProperties(sender)[x].GetValue(sender) == true))
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_cur_ill_pant"].SetValue(sender, 'Y');
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(sender)["tqp_cur_ill_pant"].SetValue(sender, 'N');
                    }
                }
                else if (propCough.Contains(e.PropertyName))
                {
                    if (propCough.All(x => (char?)TypeDescriptor.GetProperties(sender)[x].GetValue(sender) == 'N'))
                    {
                        propTimeCough.ForEach(x => TypeDescriptor.GetProperties(sender)[x].SetValue(sender, false));
                    }
                }
                else if (propTimeCough.Contains(e.PropertyName))
                {
                    if (propTimeCough.All(x => (bool?)TypeDescriptor.GetProperties(sender)[x].GetValue(sender) == false ||
                                               (bool?)TypeDescriptor.GetProperties(sender)[x].GetValue(sender) == null))
                    {
                        propCough.ForEach(x => TypeDescriptor.GetProperties(sender)[x].SetValue(sender, null));
                    }
                }

                List<DestinationControl> desControls = destination.Where(x => x.fieldName.Any(y => y == e.PropertyName)).ToList();
                if (desControls.Count() > 0)
                {
                    object value = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (value != null)
                    {
                        if (value.GetType() == typeof(bool))
                        {
                            if ((bool)value == true)
                            {
                                foreach (DestinationControl des in desControls)
                                {
                                    if (des.checkBox.Checked == true)
                                    {
                                        des.checkBox.Checked = false;
                                    }
                                }
                            }
                        }
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
            DateTime dateNow = Program.GetServerDateTime();
            string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;

            trn_pft patientPFT = bsPatientPFT.OfType<trn_pft>().FirstOrDefault();
            if (patientPFT.tpf_create_by == null)
            {
                patientPFT.tpf_create_by = user_name;
                patientPFT.tpf_create_date = dateNow;
            }
            patientPFT.tpf_update_by = user_name;
            patientPFT.tpf_update_date = dateNow;
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton14.Checked == true)
            {
                txtPFTAbnormal.Enabled = false;
            }
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked == true)
            {
                txtPFTAbnormal.Enabled = true;
                txtPFTAbnormal.Focus();
            }
            else
            {
                txtPFTAbnormal.Enabled = false;
                bsPatientPFT.OfType<trn_pft>().FirstOrDefault().tpf_pft_main_abnormal = null;
            }
        }
    }
}
