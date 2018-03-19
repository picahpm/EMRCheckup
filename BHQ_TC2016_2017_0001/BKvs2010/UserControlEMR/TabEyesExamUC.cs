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
    public partial class TabEyesExamUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public TabEyesExamUC()
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
                trn_eye_exam_hdr eye = _PatientRegis.trn_eye_exam_hdrs.FirstOrDefault();
                if (eye != null)
                {
                    if (e == null)
                    {
                        eye.teh_doctor_code = null;
                        eye.teh_doctor_license = null;
                        eye.teh_doctor_name_en = null;
                        eye.teh_doctor_name_th = null;
                    }
                    else
                    {
                        eye.teh_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        eye.teh_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        eye.teh_doctor_name_en = dn.NameEN;
                        eye.teh_doctor_name_th = dn.NameTH;
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
                        eyeExamOccUC1.PatientRegis = value;
                        eyeExamRecordUC1.PatientRegis = value;
                        eyeExamAirCrewThai1.PatientRegis = value;
                        eyeExamAirCrewFAA1.PatientRegis = value;
                        eyeExamAirCrewCan1.PatientRegis = value;
                        eyeExamAircrewAus1.PatientRegis = value;
                        eyeExamRecordUC_test1.PatientRegis = value;

                        trn_eye_exam_hdr eye = value.trn_eye_exam_hdrs.FirstOrDefault();
                        
                        if (_isDoctorRoom == false)
                        {
                            autoCompleteUC1.SelectedValue = eye.teh_doctor_code;                            
                        }
                        else
                        {
                            autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                            autoCompleteUC1.Enabled = false;
                        }

                        _PatientRegis = value;
                        this.Enabled = true;                                              
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
            eyeExamOccUC1.Clear();
            eyeExamRecordUC1.Clear();
            eyeExamAirCrewThai1.Clear();
            eyeExamAirCrewFAA1.Clear();
            eyeExamAirCrewCan1.Clear();
            eyeExamAircrewAus1.Clear();
            eyeExamRecordUC_test1.Clear();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                eyeExamOccUC1.EndEdit();
                eyeExamRecordUC1.EndEdit();
                eyeExamAirCrewThai1.EndEdit();
                eyeExamAirCrewFAA1.EndEdit();
                eyeExamAirCrewCan1.EndEdit();
                eyeExamAircrewAus1.EndEdit();
                eyeExamRecordUC_test1.EndEdit();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }

        private void eyeExamRecordUC_test1_Load(object sender, EventArgs e)
        {

        }
    }
}
