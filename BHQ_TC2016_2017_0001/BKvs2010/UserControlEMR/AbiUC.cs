using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.EmrClass;
using BKvs2010.Usercontrols;

namespace BKvs2010.UserControlEMR
{
    public partial class AbiUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public AbiUC()
        {
            InitializeComponent();
            
            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

            List<mst_conclusion_favorite_dtl> mst = new FavoriteCls().getFavorite(favoriteTextBox1.FavoriteOrder, favoriteTextBox1.FavoriteType);
            favoriteTextBox1.AutoCompleteListThList = mst.Select(x => x.mcfd_description).ToList();
            favoriteTextBox1.RightClickDropDown += favoriteTextBox1_DeleteFavorite;
        }
        private void favoriteTextBox1_DeleteFavorite(object sender, string e)
        {
            string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new FavoriteCls().removeFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, e, user_name);
                txtBox.AutoCompleteListThList.Remove(e);
            }
        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_abi_hdr abi = bsPatientABI.OfType<trn_abi_hdr>().FirstOrDefault();
                
                if (abi != null)
                {
                    if (e == null)
                    {
                        abi.tah_doctor_code = null;
                        abi.tah_doctor_license = null;
                        abi.tah_doctor_name_en = null;
                        abi.tah_doctor_name_th = null;
                    }
                    else
                    {                        
                        abi.tah_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        abi.tah_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        abi.tah_doctor_name_en = dn.NameEN;
                        abi.tah_doctor_name_th = dn.NameTH;
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
                        trn_abi_hdr patientAbi = value.trn_abi_hdrs.FirstOrDefault();
                        if (patientAbi == null)
                        {
                            patientAbi = new trn_abi_hdr();
                            value.trn_abi_hdrs.Add(patientAbi);
                            patientAbi.tah_the_bmi = new InhCheckupDataContext().funcGetBMIValue(value.tpr_id);                            
                        }                                            
                                                                        
                        patientAbi_PropertyChanged(patientAbi, new PropertyChangedEventArgs("tah_impress_others"));
                        patientAbi_PropertyChanged(patientAbi, new PropertyChangedEventArgs("tah_hearth_rhy_flag"));
                        patientAbi.PropertyChanged -= new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                        patientAbi.PropertyChanged += new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                        
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;
                                  
                        if (_isDoctorRoom == false)
                        {    
                            if (patientAbi.tah_doctor_code == null)
                            {
                                if (value.trn_patient_doctor == null)
                                {
                                    value.trn_patient_doctor = new trn_patient_doctor();
                                }
                                autoCompleteUC1.SelectedValue = value.trn_patient_doctor.tpd_doctor_code;
                            }
                            else
                            {
                                autoCompleteUC1.SelectedValue = patientAbi.tah_doctor_code;
                            }
                        }
                        else
                        {
                            if (value.trn_patient_doctor == null)
                            {
                                value.trn_patient_doctor = new trn_patient_doctor();
                            }

                            if (value.trn_patient_doctor.tpd_doctor_code == null)
                            {
                                autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                            }
                            else
                            {
                                autoCompleteUC1.SelectedValue = value.trn_patient_doctor.tpd_doctor_code;
                            }
                        }
                        favoriteTextBox1.Text = patientAbi.tah_doc_result_thai;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "patientRegis", ex, false);
                    }
                }
            }
        }

        private void comboUpdateSoure_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            foreach (Binding binding in combo.DataBindings)
            {
                binding.WriteValue();
            }
        }
        //char? tmpChk = null; //for set normal - abnormal
        //bool withImpress = false; //for check other with another impression
        //private void chkBoxUpdateSource_CheckedChanged(object sender, EventArgs e)
        //{
        //    trn_abi_hdr abi = bsPatientABI.OfType<trn_abi_hdr>().FirstOrDefault();
        //    CheckBox chkBox = (CheckBox)sender;
        //    foreach (Binding bind in chkBox.DataBindings)
        //    {
        //        bind.WriteValue();
                
        //        switch (chkBox.Name)
        //        {
        //            case "chkimpress_normal":
        //                {
        //                    if (chkBox.Checked == true) 
        //                    {
        //                        abi.tah_impress_mild_periphera = 'N';
        //                        abi.tah_impress_serv_peripheal = 'N';
        //                        abi.tah_impress_early_ather = 'N';
        //                        abi.tah_impress_no_evidence = 'N';
        //                        abi.tah_impress_mode_peripheral = 'N';
        //                        abi.tah_impress_non_compress = 'N';
        //                        withImpress = true;
        //                        tmpChk = 'N'; 
        //                    }
        //                    else { tmpChk = null; withImpress = false; }                                
        //                    break;
        //                }
        //            case "chkimpress_others":
        //                {
        //                    if (withImpress == true)
        //                    { }
        //                    else { tmpChk = null; }
        //                    break;
        //                }
        //            case "chkimpress_mild_periphera":
        //            case "chkimpress_serv_peripheal":
        //            case "chkimpress_early_ather":
        //            case "chkimpress_no_evidence":
        //            case "chkimpress_mode_peripheral":
        //            case "chkimpress_non_compress":
        //                {
        //                    if (chkBox.Checked == true) 
        //                    {
        //                        abi.tah_impress_normal = 'N';
        //                        withImpress = true;
        //                        tmpChk = 'A'; 
        //                    }
        //                    else 
        //                    {
        //                        if (chkimpress_mild_periphera.Checked == false && chkimpress_serv_peripheal.Checked == false
        //                            && chkimpress_early_ather.Checked == false && chkimpress_no_evidence.Checked == false
        //                            && chkimpress_mode_peripheral.Checked == false && chkimpress_non_compress.Checked == false)
        //                        { withImpress = false; tmpChk = null; }
        //                        else { withImpress = true; }
        //                    }
        //                    break;
        //                }               
        //        }                
        //    }
        //    abi.tah_summary = tmpChk;
        //}

        private class clsSourceMaster
        {
            public int val { get; set; }
            public string dis { get; set; }
        }

        private void patientAbi_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
            switch (e.PropertyName)
            {
                case "tah_impress_others":
                    if (val == null)
                    {
                        TypeDescriptor.GetProperties(sender)[e.PropertyName].SetValue(sender, 'N');
                    }
                    char NewVal = Convert.ToChar(TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender));
                    if (NewVal == 'N')
                    {
                        TypeDescriptor.GetProperties(sender)["tah_others_text"].SetValue(sender, "");
                        txtImpressOther.Enabled = false;
                    }
                    else if (NewVal == 'Y')
                    {
                        txtImpressOther.Enabled = true;
                    }
                    break;
                case "tah_hearth_rhy_flag":
                    if (val != null && val.GetType() == typeof(char))
                    {
                        char result = (char)val;
                        if (result == 'O')
                        {
                            txtOtherWithRate.Enabled = true;
                            txtOtherWithRate.Focus();
                        }
                        else
                        {
                            TypeDescriptor.GetProperties(sender)["tah_other_with_rate"].SetValue(sender, null);
                            txtOtherWithRate.Enabled = false;
                        }
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(sender)["tah_other_with_rate"].SetValue(sender, null);
                        txtOtherWithRate.Enabled = false;
                    }
                    break;
                case "tah_the_bmi":
                case "tah_asian":
                    var valBMI = TypeDescriptor.GetProperties(sender)["tah_the_bmi"].GetValue(sender);
                    var valCont = TypeDescriptor.GetProperties(sender)["tah_asian"].GetValue(sender);
                    if (valBMI != null && valCont != null)
                    {
                        string result = new InhCheckupDataContext().funcGetBMIResult(valBMI.ToString(), valCont.ToString());
                        if (result != null)
                        {
                            char flag = Convert.ToChar(result);
                            if (new List<char> { 'U', 'N', 'O', 'B' }.Contains(flag))
                            {
                                TypeDescriptor.GetProperties(sender)["tah_the_bmi_flag"].SetValue(sender, flag);
                            }
                            else
                            {
                                TypeDescriptor.GetProperties(sender)["tah_the_bmi_flag"].SetValue(sender, null);
                            }
                        }
                    }
                    break;
                case "tah_summary":
                    ((trn_abi_hdr)sender).PropertyChanged -= new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    if (val == null)
                    {
                        TypeDescriptor.GetProperties(sender)["tah_impress_normal"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_no_evidence"].SetValue(sender, 'N');

                        TypeDescriptor.GetProperties(sender)["tah_impress_non_compress"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_mild_periphera"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_serv_peripheal"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_early_ather"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_mode_peripheral"].SetValue(sender, 'N');
                    }
                    else if ((char)val == 'N')
                    {
                        var valImNo = TypeDescriptor.GetProperties(sender)["tah_impress_no_evidence"].GetValue(sender);
                        if (valImNo != null && (char)valImNo == 'N')
                        {
                            TypeDescriptor.GetProperties(sender)["tah_impress_normal"].SetValue(sender, 'Y');
                        }

                        TypeDescriptor.GetProperties(sender)["tah_impress_non_compress"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_mild_periphera"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_serv_peripheal"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_early_ather"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_mode_peripheral"].SetValue(sender, 'N');
                    }
                    else if ((char)val == 'A')
                    {
                        TypeDescriptor.GetProperties(sender)["tah_impress_normal"].SetValue(sender, 'N');
                    }
                    ((trn_abi_hdr)sender).PropertyChanged += new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    break;
                case "tah_impress_normal":
                    ((trn_abi_hdr)sender).PropertyChanged -= new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    if (val == null)
                    {

                    }
                    else if ((char)val == 'Y')
                    {
                        TypeDescriptor.GetProperties(sender)["tah_summary"].SetValue(sender, 'N');

                        TypeDescriptor.GetProperties(sender)["tah_impress_no_evidence"].SetValue(sender, 'N');

                        TypeDescriptor.GetProperties(sender)["tah_impress_non_compress"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_mild_periphera"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_serv_peripheal"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_early_ather"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tah_impress_mode_peripheral"].SetValue(sender, 'N');
                    }
                    else if ((char)val == 'N')
                    {

                    }
                    ((trn_abi_hdr)sender).PropertyChanged += new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    break;
                case "tah_impress_no_evidence":
                    ((trn_abi_hdr)sender).PropertyChanged -= new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    if (val == null)
                    {

                    }
                    else if ((char)val == 'Y')
                    {
                        //TypeDescriptor.GetProperties(sender)["tah_summary"].SetValue(sender, 'N');

                        TypeDescriptor.GetProperties(sender)["tah_impress_normal"].SetValue(sender, 'N');

                        //TypeDescriptor.GetProperties(sender)["tah_impress_non_compress"].SetValue(sender, 'N');
                        //TypeDescriptor.GetProperties(sender)["tah_impress_mild_periphera"].SetValue(sender, 'N');
                        //TypeDescriptor.GetProperties(sender)["tah_impress_serv_peripheal"].SetValue(sender, 'N');
                        //TypeDescriptor.GetProperties(sender)["tah_impress_early_ather"].SetValue(sender, 'N');
                        //TypeDescriptor.GetProperties(sender)["tah_impress_mode_peripheral"].SetValue(sender, 'N');
                    }
                    else if ((char)val == 'N')
                    {

                    }
                    ((trn_abi_hdr)sender).PropertyChanged += new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    break;
                case "tah_impress_serv_peripheal":
                case "tah_impress_early_ather":
                case "tah_impress_mode_peripheral":
                case "tah_impress_non_compress":
                    ((trn_abi_hdr)sender).PropertyChanged -= new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    if (val == null)
                    {

                    }
                    else if ((char)val == 'Y')
                    {
                        TypeDescriptor.GetProperties(sender)["tah_summary"].SetValue(sender, 'A');

                        TypeDescriptor.GetProperties(sender)["tah_impress_normal"].SetValue(sender, 'N');
                    }
                    else if ((char)val == 'N')
                    {

                    }
                    ((trn_abi_hdr)sender).PropertyChanged += new PropertyChangedEventHandler(patientAbi_PropertyChanged);
                    break;
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

            trn_abi_hdr patientAbi = bsPatientABI.OfType<trn_abi_hdr>().FirstOrDefault();
            if (patientAbi.tah_create_by == null)
            {
                patientAbi.tah_create_by = user_name;
                patientAbi.tah_create_date = dateNow;
            }
            patientAbi.tah_update_by = user_name;
            patientAbi.tah_update_date = dateNow;
            patientAbi.tah_doc_result_thai = favoriteTextBox1.Text;
            patientAbi.tah_doc_result_eng = favoriteTextBox1.Text;
        }

        private void favoriteTextBox1_btnFavoriteClick(object sender, EventArgs e)
        {
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null)
            {
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                bool savefav = new FavoriteCls().saveFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, txtBox.Text, user_name);
                if (savefav)
                {
                    txtBox.AutoCompleteListThList.Add(txtBox.Text);
                    MessageBox.Show("Add '" + txtBox.Text + "' to favorite Complete.");
                }
            }
        }
    }
}
