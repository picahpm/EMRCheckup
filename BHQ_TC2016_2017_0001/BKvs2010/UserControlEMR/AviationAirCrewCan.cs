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
    public partial class AviationAirCrewCan : UserControl
    {
        public AviationAirCrewCan()
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
                        trn_eye_aircrew_can eye_aircrew_can = patient_eyes.trn_eye_aircrew_cans.FirstOrDefault();
                        if (eye_aircrew_can == null)
                        {
                            eye_aircrew_can = new trn_eye_aircrew_can();
                            patient_eyes.trn_eye_aircrew_cans.Add(eye_aircrew_can);
                        }

                        load_aircrew_can(ref eye_aircrew_can);

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
                    bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault().tac_dvis_right = (string)val;
                }
                if (e.PropertyName == "teh_vision_out_le")
                {
                    bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault().tac_dvis_left = (string)val;
                }
                if (e.PropertyName == "teh_vision_re")
                {
                    bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault().tac_dvis_rcorrect = (string)val;
                }
                if (e.PropertyName == "teh_vision_le")
                {
                    bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault().tac_dvis_lcorrect = (string)val;
                }
                if (e.PropertyName == "teh_color_vision")
                {
                    if (val.ToString() == "N")
                    {
                        bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault().tac_color_vis = 'P';
                    }
                    else if (val.ToString() == "R")
                    {
                        bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault().tac_color_vis = 'F';
                    }
                    else
                    {
                        bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault().tac_color_vis = null;
                    }
                }
            }
        }        

        private bool _ShowAudio = true;
        public bool ShowAudio
        {
            get { return _ShowAudio; }
            set
            {
                if (value != _ShowAudio)
                {
                    if (value)
                    {
                        panel1.Height = 134;
                    }
                    else
                    {
                        panel1.Height = 0;
                    }
                    _ShowAudio = value;
                }
                int size = (value ? 134 : 0) + 395 + (_ShowLab ? 99 : 0);
                this.Size = new Size(911, size);
                tableLayoutPanel1.Size = new Size(911, size);
                panel4.AutoScrollMinSize = new Size(911, size);
                panel4.AutoScroll = true;
            }
        }
        private bool _ShowLab = true;
        public bool ShowLab
        {
            get { return _ShowLab; }
            set
            {
                if (value != _ShowLab)
                {
                    if (value)
                    {

                        tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Absolute;
                        panel3.Height = 99;
                    }
                    else
                    {
                        tableLayoutPanel1.RowStyles[1].SizeType = SizeType.Percent;
                        panel3.Height = 0;
                    }
                    _ShowLab = value;
                }
                int size = (_ShowAudio ? 134 : 0) + 395 + (value ? 99 : 0);
                this.Size = new Size(911, size);
                tableLayoutPanel1.Size = new Size(911, size);
                panel4.AutoScrollMinSize = new Size(911, size);
                panel4.AutoScroll = true;
            }
        }

        private void load_aircrew_can(ref trn_eye_aircrew_can patient_aircrew_can)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(patient_aircrew_can))
            {
                eye_can_PropertyChanged(patient_aircrew_can, new PropertyChangedEventArgs(prop.Name));
            }

            patient_aircrew_can.PropertyChanged += new PropertyChangedEventHandler(eye_can_PropertyChanged);
        }
        private void eye_can_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tac_color_vis")
            {
                var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                if (val == null || val == DBNull.Value)
                {
                    TypeDescriptor.GetProperties(sender)["tac_fw_lantern"].SetValue(sender, null);
                    pnl_FW_CN.Enabled = false;
                }
                else
                {
                    if (val.ToString() == "F")
                    {
                        pnl_FW_CN.Enabled = true;
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(sender)["tac_fw_lantern"].SetValue(sender, null);
                        pnl_FW_CN.Enabled = false;
                    }
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            txtHeight.Text = "";
            txtWeight.Text = "";
            txtSystolic.Text = "";
            txtDiastolic.Text = "";
            txtPulse.Text = "";
            txtRight500.Text = "";
            txtLeft500.Text = "";
            txtRight1000.Text = "";
            txtLeft1000.Text = "";
            txtRight2000.Text = "";
            txtLeft2000.Text = "";
            txtRight3000.Text = "";
            txtLeft3000.Text = "";
            txtRight4000.Text = "";
            txtLeft4000.Text = "";
            txtSugar.Text = "";
            txtAlbumin.Text = "";
            txtMicro.Text = "";
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            DateTime dateNow = Program.GetServerDateTime();
            string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;

            trn_eye_aircrew_can eyes_can = bsEyeAircrewCan.OfType<trn_eye_aircrew_can>().FirstOrDefault();
            if (eyes_can.tac_create_by == null)
            {
                eyes_can.tac_create_by = user_name;
                eyes_can.tac_create_date = dateNow;
            }
            eyes_can.tac_update_by = user_name;
            eyes_can.tac_update_date = dateNow;
        }

        private void getVitalSign(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_basic_measure_dtl basic = dbc.trn_basic_measure_dtls
                                                     .Where(x => x.trn_basic_measure_hdr.tpr_id == tpr_id)
                                                     .OrderByDescending(x => x.tbd_date)
                                                     .FirstOrDefault();

                    txtHeight.Text = ConvertToInch(basic.tbd_height);
                    txtWeight.Text = ConvertToPound(basic.tbd_weight);
                    txtSystolic.Text = basic.tbd_systolic;
                    txtDiastolic.Text = basic.tbd_diastolic;
                    txtPulse.Text = basic.tbd_pulse;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "getVitalSign", ex, false);
            }
        }
        private void getAudio(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_audiometric_hdr audio = dbc.trn_audiometric_hdrs
                                                   .Where(x => x.tpr_id == tpr_id)
                                                   .FirstOrDefault();

                    txtRight500.Text = ConvertToString(audio.tdh_right_level_500);
                    txtLeft500.Text = ConvertToString(audio.tdh_left_level_500);
                    txtRight1000.Text = ConvertToString(audio.tdh_right_level_1000);
                    txtLeft1000.Text = ConvertToString(audio.tdh_left_level_1000);
                    txtRight2000.Text = ConvertToString(audio.tdh_right_level_2000);
                    txtLeft2000.Text = ConvertToString(audio.tdh_left_level_2000);
                    txtRight3000.Text = ConvertToString(audio.tdh_right_level_3000);
                    txtLeft3000.Text = ConvertToString(audio.tdh_left_level_3000);
                    txtRight4000.Text = ConvertToString(audio.tdh_right_level_4000);
                    txtLeft4000.Text = ConvertToString(audio.tdh_left_level_4000);
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "getAudio", ex, false);
            }
        }
        private void getLab(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    var labResult = dbc.sp_med_exm_report(tpr_id).FirstOrDefault();
                    if (labResult.ecg_normal == null && labResult.ecg_abnormal == null)
                    {
                        pnlECG.Tag = null;
                    }
                    else if (labResult.ecg_normal.Length > 0)
                    {
                        pnlECG.Tag = 'N';
                    }
                    else if (labResult.ecg_abnormal.Length > 0)
                    {
                        pnlECG.Tag = 'A';
                    }
                    txtSugar.Text = "";
                    txtAlbumin.Text = labResult.C0060;
                    txtMicro.Text = labResult.E0020;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "getLab", ex, false);
            }
        }
        private string ConvertToInch(object value)
        {
            try
            {
                double inch = Convert.ToDouble(value) * 0.39370078;
                return inch.ToString("0.00");
            }
            catch
            {
                return null;
            }
        }
        private string ConvertToPound(object value)
        {
            try
            {
                double pound = Convert.ToDouble(value) * 2.20462262;
                return pound.ToString("0.00");
            }
            catch
            {
                return null;
            }
        }
        private string ConvertToString(object value)
        {
            try
            {
                string str = value.ToString();
                return str;
            }
            catch
            {
                return null;
            }
        }
    }
}
