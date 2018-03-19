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
    public partial class AviationAirCrewThai : UserControl
    {
        public AviationAirCrewThai()
        {
            InitializeComponent();
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
                        trn_eye_exam_hdr patient_eyes = value.trn_eye_exam_hdrs.FirstOrDefault();
                        if (patient_eyes == null)
                        {
                            patient_eyes = new trn_eye_exam_hdr();
                            value.trn_eye_exam_hdrs.Add(patient_eyes);
                        }
                        trn_eye_aircrew_thai eye_aircrew_thai = patient_eyes.trn_eye_aircrew_thais.FirstOrDefault();
                        if (eye_aircrew_thai == null)
                        {
                            eye_aircrew_thai = new trn_eye_aircrew_thai();
                            patient_eyes.trn_eye_aircrew_thais.Add(eye_aircrew_thai);
                        }

                        load_aircrew_thai(ref eye_aircrew_thai);

                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;

                        patient_eyes.PropertyChanged -= new PropertyChangedEventHandler(patient_eyes_PropertyChanged);
                        patient_eyes.PropertyChanged += new PropertyChangedEventHandler(patient_eyes_PropertyChanged);
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "setProp tpr_patient_regis", ex, false);
                    }
                }
            }
        }
        private void patient_eyes_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "teh_vision_out_re" || e.PropertyName == "teh_vision_out_le" || e.PropertyName == "teh_color_vision" || e.PropertyName == "teh_vision_re" || e.PropertyName == "teh_vision_le")
            {
                var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                if (e.PropertyName == "teh_vision_out_re")
                {
                    bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault().tea_dvis_right = (string)val;
                }
                if (e.PropertyName == "teh_vision_out_le")
                {
                    bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault().tea_dvis_left = (string)val;
                }
                if (e.PropertyName == "teh_vision_re")
                {
                    bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault().tea_dvis_rcorrect = (string)val;
                }
                if (e.PropertyName == "teh_vision_le")
                {
                    bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault().tea_dvis_lcorrect = (string)val;
                }
                if (e.PropertyName == "teh_color_vision")
                {
                    if (val.ToString() == "N")
                    {
                        bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault().tea_color_vis = 'P';
                    }
                    else if (val.ToString() == "R")
                    {
                        bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault().tea_color_vis = 'F';
                    }
                    else
                    {
                        bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault().tea_color_vis = null;
                    }
                }
            }
        } 

        private void load_aircrew_thai(ref trn_eye_aircrew_thai patient_aircrew_thai)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(patient_aircrew_thai))
            {
                eye_thai_PropertyChanged(patient_aircrew_thai, new PropertyChangedEventArgs(prop.Name));
            }

            patient_aircrew_thai.PropertyChanged += new PropertyChangedEventHandler(eye_thai_PropertyChanged);
        }
        private void eye_thai_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tea_color_vis")
            {
                var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                if (val == null || val == DBNull.Value)
                {
                    TypeDescriptor.GetProperties(sender)["tea_fw_lantern"].SetValue(sender, null);
                    pnlFWThai.Enabled = false;
                }
                else
                {
                    if (val.ToString() == "F")
                    {
                        pnlFWThai.Enabled = true;
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(sender)["tea_fw_lantern"].SetValue(sender, null);
                        pnlFWThai.Enabled = false;
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
            
            trn_eye_aircrew_thai eyes_thai = bsEyeAircrewThai.OfType<trn_eye_aircrew_thai>().FirstOrDefault();
            if (eyes_thai.tea_create_by == null)
            {
                eyes_thai.tea_create_by = user_name;
                eyes_thai.tea_create_date = dateNow;
            }
            eyes_thai.tea_update_by = user_name;
            eyes_thai.tea_update_date = dateNow;
        }
    }
}
