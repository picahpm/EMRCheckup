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
    public partial class EyeExamOccUC : UserControl
    {
        public EyeExamOccUC()
        {
            InitializeComponent();
            //setCheckBoxUpdateSourceEvent();
        }
        //private void setCheckBoxUpdateSourceEvent()
        //{
        //    CheckBox7.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    CheckBox8.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    CheckBox9.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    CheckBox10.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    CheckBox11.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    CheckBox12.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);

        //    chkDistant_Glassess.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    chkDistant_ContactLens.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    checkBox5.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    checkBox6.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    checkBox13.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //    checkBox14.CheckedChanged += new EventHandler(chkBoxUpdateSource_CheckedChanged);
        //}
        //private void chkBoxUpdateSource_CheckedChanged(object sender, EventArgs e)
        //{
        //    CheckBox chkBox = (CheckBox)sender;
        //    foreach (Binding bind in chkBox.DataBindings)
        //    {
        //        bind.WriteValue();
        //    }
        //}

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
                        trn_eye_occ_med eye_occ_med = patient_eyes.trn_eye_occ_meds.FirstOrDefault();
                        if (eye_occ_med == null)
                        {
                            eye_occ_med = new trn_eye_occ_med();
                            patient_eyes.trn_eye_occ_meds.Add(eye_occ_med);
                        }

                        load_occ_med(ref eye_occ_med);

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
                    bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault().teo_dvis_right = (string)val;
                }
                if (e.PropertyName == "teh_vision_out_le")
                {
                    bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault().teo_dvis_left = (string)val;
                }
                if (e.PropertyName == "teh_vision_re")
                {
                    bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault().teo_dvis_rcorrect = (string)val;
                }
                if (e.PropertyName == "teh_vision_le")
                {
                    bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault().teo_dvis_lcorrect = (string)val;
                }
                if (e.PropertyName == "teh_color_vision")
                {
                    if (val.ToString() == "N")
                    {
                        bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault().teo_color_vis = 'P';
                    }
                    else if (val.ToString() == "R")
                    {
                        bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault().teo_color_vis = 'F';
                    }
                    else
                    {
                        bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault().teo_color_vis = null;
                    }
                }
            }
        }
        private void load_occ_med(ref trn_eye_occ_med patient_occ_med)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(patient_occ_med))
            {
                eye_occ_PropertyChanged(patient_occ_med, new PropertyChangedEventArgs(prop.Name));
            }

            patient_occ_med.PropertyChanged -= new PropertyChangedEventHandler(eye_occ_PropertyChanged);
            patient_occ_med.PropertyChanged += new PropertyChangedEventHandler(eye_occ_PropertyChanged);
        }
        private void eye_occ_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "teo_color_vis")
            {
                var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                if (val == null || val == DBNull.Value)
                {
                    TypeDescriptor.GetProperties(sender)["teo_fw_lantern"].SetValue(sender, null);
                    panelRadioBindingSoure18.Enabled = false;
                }
                else
                {
                    if (val.ToString() == "F")
                    {
                        panelRadioBindingSoure18.Enabled = true;
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(sender)["teo_fw_lantern"].SetValue(sender, null);
                        panelRadioBindingSoure18.Enabled = false;
                    }
                }
            }
        }
        private void btnDefault_Click(object sender, EventArgs e)
        {
            trn_eye_occ_med eye_occ_med = bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault();
            eye_occ_med.teo_heter_eso_status = 'N';
            eye_occ_med.teo_heter_exo_status = 'N';
            eye_occ_med.teo_heter_rhyper_status = 'N';
            eye_occ_med.teo_heter_lhyper_status = 'N';
            eye_occ_med.teo_dvis_right_status = 'N';
            eye_occ_med.teo_dvis_rcorrect_status = 'N';
            eye_occ_med.teo_dvis_left_status = 'N';
            eye_occ_med.teo_dvis_lcorrect_status = 'N';
            eye_occ_med.teo_dvis_both_status = 'N';
            eye_occ_med.teo_dvis_bcorrect_status = 'N';
            eye_occ_med.teo_nvis_right_status = 'N';
            eye_occ_med.teo_nvis_rcorrect_status = 'N';
            eye_occ_med.teo_nvis_left_status = 'N';
            eye_occ_med.teo_nvis_lcorrect_status = 'N';
            eye_occ_med.teo_nvis_both_status = 'N';
            eye_occ_med.teo_nvis_bcorrect_status = 'N';
            eye_occ_med.teo_hori_vis_left_status = 'N';
            eye_occ_med.teo_hori_vis_right_status = 'N';
            eye_occ_med.teo_ster_dep_far_status = 'N';
            eye_occ_med.teo_ster_dep_near_status = 'N';
        }

        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;

                trn_eye_occ_med eyes_occ = bsEyeOccMed.OfType<trn_eye_occ_med>().FirstOrDefault();
                if (eyes_occ.teo_create_by == null)
                {
                    eyes_occ.teo_create_by = user_name;
                    eyes_occ.teo_create_date = dateNow;
                }
                eyes_occ.teo_update_by = user_name;
                eyes_occ.teo_update_date = dateNow;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }
    }
}
