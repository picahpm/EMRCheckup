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

namespace BKvs2010.UserControlEMR
{
    public partial class TabCarotidUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public TabCarotidUC()
        {
            InitializeComponent();
            
            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_carotid_hdr carotid = _PatientRegis.trn_carotid_hdrs.FirstOrDefault();                
                if (carotid != null)
                {
                    if (e == null)
                    {
                        carotid.tch_doctor_code = null;
                        carotid.tch_doctor_license = null;
                        carotid.tch_doctor_name_en = null;
                        carotid.tch_doctor_name_th = null;
                    }
                    else
                    {
                        carotid.tch_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        carotid.tch_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        carotid.tch_doctor_name_en = dn.NameEN;
                        carotid.tch_doctor_name_th = dn.NameTH;
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
                        carotidUC1.PatientRegis = value;
                        carotidDuplexReport1.PatientRegis = value;

                        trn_carotid_hdr carotid = value.trn_carotid_hdrs.FirstOrDefault();

                        _PatientRegis = value;
                        this.Enabled = true;

                        if (_isDoctorRoom == false)
                        {
                            autoCompleteUC1.SelectedValue = carotid.tch_doctor_code;
                        }
                        else
                        {
                            autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                            autoCompleteUC1.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "setProp tpr_patient_regis", ex, false);
                    }
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            carotidUC1.Clear();
            carotidDuplexReport1.Clear();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                carotidUC1.EndEdit();
                carotidDuplexReport1.EndEdit();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }

        public int? TabSelected { get; set; }
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TabSelected = tabControl1.SelectedIndex;
        }        
    }
}
