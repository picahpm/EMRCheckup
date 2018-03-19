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
    public partial class AviationExamUC : UserControl
    {
        public AviationExamUC()
        {
            InitializeComponent();

            txtfitcls.TextChanged += new EventHandler(rec_remark_TextChanged);
            txtunitcls.TextChanged += new EventHandler(rec_remark_TextChanged);
            txtdeferred.TextChanged += new EventHandler(rec_remark_TextChanged);
            txtfitcls.EnabledChanged += new EventHandler(rec_remark_EnabledChanged);
            txtunitcls.EnabledChanged += new EventHandler(rec_remark_EnabledChanged);
            txtdeferred.EnabledChanged += new EventHandler(rec_remark_EnabledChanged);

            desFieldAviation = new List<DestinationField>
            {
                new DestinationField { fieldName = "trav_head", fieldRemarkName = "trav_head_remark", txtBox = txthead },
                new DestinationField { fieldName = "trav_nose", fieldRemarkName = "trav_nose_remark", txtBox = txtnose },
                new DestinationField { fieldName = "trav_sinuses", fieldRemarkName = "trav_sinuses_remark", txtBox = txtsinuses },
                new DestinationField { fieldName = "trav_mouth", fieldRemarkName = "trav_mouth_remark", txtBox = txtmouth },
                new DestinationField { fieldName = "trav_ears", fieldRemarkName = "trav_ears_remark", txtBox = txtears },
                new DestinationField { fieldName = "trav_drums", fieldRemarkName = "trav_drums_remark", txtBox = txtdrums },
                new DestinationField { fieldName = "trav_eyes", fieldRemarkName = "trav_eyes_remark", txtBox = txteyes },
                new DestinationField { fieldName = "trav_opht", fieldRemarkName = "trav_opht_remark", txtBox = txtopht },
                new DestinationField { fieldName = "trav_pupils", fieldRemarkName = "trav_pupils_remark", txtBox = txtpupe },
                new DestinationField { fieldName = "trav_ocular", fieldRemarkName = "trav_ocular_remark", txtBox = txtocular },
                new DestinationField { fieldName = "trav_lungs", fieldRemarkName = "trav_lungs_remark", txtBox = txtlungs },
                new DestinationField { fieldName = "trav_heart", fieldRemarkName = "trav_heart_remark", txtBox = txtheart },
                new DestinationField { fieldName = "trav_vascular", fieldRemarkName = "trav_vascular_remark", txtBox = txtvascular },
                new DestinationField { fieldName = "trav_abdomen", fieldRemarkName = "trav_abdomen_remark", txtBox = txtabdomen2 },
                new DestinationField { fieldName = "trav_anus", fieldRemarkName = "trav_anus_remark", txtBox = txtanus },
                new DestinationField { fieldName = "trav_endo", fieldRemarkName = "trav_endo_remark", txtBox = txtendoc },
                new DestinationField { fieldName = "trav_gu", fieldRemarkName = "trav_gu_remark", txtBox = txtgu },
                new DestinationField { fieldName = "trav_upper", fieldRemarkName = "trav_upper_remark", txtBox = txtupper },
                new DestinationField { fieldName = "trav_spine", fieldRemarkName = "trav_spine_remark", txtBox = txtspine },
                new DestinationField { fieldName = "trav_body", fieldRemarkName = "trav_body_remark", txtBox = txtbody },
                new DestinationField { fieldName = "trav_skin", fieldRemarkName = "trav_skin_remark", txtBox = txtskin },
                new DestinationField { fieldName = "trav_neuro", fieldRemarkName = "trav_neuro_remark", txtBox = txtneuro2 },
                new DestinationField { fieldName = "trav_psych", fieldRemarkName = "trav_psych_remark", txtBox = txtpsyc },
                new DestinationField { fieldName = "trav_gene", fieldRemarkName = "trav_gene_remark", txtBox = txtgeneral }
            };

        }
        private void rec_remark_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            trn_doctor_aviation DoctorAviation = bsDoctorAviation.OfType<trn_doctor_aviation>().FirstOrDefault();
            if (DoctorAviation != null)
            {
                DoctorAviation.trav_rec_remark = txtBox.Text;
            }
        }
        private void rec_remark_EnabledChanged(object sender, EventArgs e)
        {
            if (_PatientRegis != null)
            {
                TextBox txtBox = (TextBox)sender;
                if (txtBox.Enabled)
                {
                    txtBox.DataBindings.Add(new Binding("Text", bsDoctorAviation, "trav_rec_remark", true));
                }
                else
                {
                    txtBox.DataBindings.Clear();
                }
            }
        }
        private class DestinationField
        {
            public string fieldName { get; set; }
            public string fieldRemarkName { get; set; }
            public TextBox txtBox { get; set; }
        }
        private List<DestinationField> desFieldAviation;
        private class clsSourceMaster
        {
            public int val { get; set; }
            public string dis { get; set; }
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
                        var docResult = new EmrClass.FunctionDataCls().getDoctorResult(value.mhs_id, "DC", "PE")
                                              .Select(x => new clsSourceMaster
                                              {
                                                  val = x.mdr_id,
                                                  dis = x.mdr_tname
                                              }).ToList();

                        cmbOthertest.DataSource = docResult;
                        cmbOthertest.DisplayMember = "dis";
                        cmbOthertest.ValueMember = "dis";

                        trn_basic_measure_hdr BasicHdr = value.trn_basic_measure_hdrs.FirstOrDefault();
                        if (bsBasicHdr == null)
                        {
                            BasicHdr = new trn_basic_measure_hdr();
                            value.trn_basic_measure_hdrs.Add(BasicHdr);
                        }
                        List<trn_basic_measure_dtl> BasicDtl = BasicHdr.trn_basic_measure_dtls.ToList();
                        if (BasicDtl.Count() == 0)
                        {
                            BasicDtl = new List<trn_basic_measure_dtl>
                        {
                            new trn_basic_measure_dtl()
                        };
                            BasicHdr.trn_basic_measure_dtls.AddRange(BasicDtl);
                        }
                        trn_doctor_hdr DoctorHdr = value.trn_doctor_hdrs.FirstOrDefault();
                        if (DoctorHdr == null)
                        {
                            DoctorHdr = new trn_doctor_hdr();
                            value.trn_doctor_hdrs.Add(DoctorHdr);
                        }
                        trn_doctor_aviation DoctorAviation = DoctorHdr.trn_doctor_aviations.FirstOrDefault();
                        if (DoctorAviation == null)
                        {
                            DoctorAviation = new trn_doctor_aviation();
                            DoctorHdr.trn_doctor_aviations.Add(DoctorAviation);
                        }
                        DoctorAviation.PropertyChanged -= new PropertyChangedEventHandler(DoctorAviation_PropertyChanged);
                        DoctorAviation.PropertyChanged += new PropertyChangedEventHandler(DoctorAviation_PropertyChanged);

                        trn_audiometric_hdr PatientAudio = value.trn_audiometric_hdrs.FirstOrDefault();
                        if (PatientAudio != null)
                        {
                            DoctorAviation.trav_rear_500 = ConvertToInt(PatientAudio.tdh_right_level_500);
                            DoctorAviation.trav_lear_500 = ConvertToInt(PatientAudio.tdh_left_level_500);
                            DoctorAviation.trav_rear_1000 = ConvertToInt(PatientAudio.tdh_right_level_1000);
                            DoctorAviation.trav_lear_1000 = ConvertToInt(PatientAudio.tdh_left_level_1000);
                            DoctorAviation.trav_rear_2000 = ConvertToInt(PatientAudio.tdh_right_level_2000);
                            DoctorAviation.trav_lear_2000 = ConvertToInt(PatientAudio.tdh_left_level_2000);
                            DoctorAviation.trav_rear_3000 = ConvertToInt(PatientAudio.tdh_right_level_3000);
                            DoctorAviation.trav_lear_3000 = ConvertToInt(PatientAudio.tdh_left_level_3000);
                            DoctorAviation.trav_rear_4000 = ConvertToInt(PatientAudio.tdh_right_level_4000);
                            DoctorAviation.trav_lear_4000 = ConvertToInt(PatientAudio.tdh_left_level_4000);
                        }
                        ShowLabResult(value.tpr_id);

                        _PatientRegis = value;
                        bsPatientRegis.DataSource = value;
                        bsBasicDtl.Position = BasicDtl.IndexOf(BasicDtl.OrderByDescending(x => x.tbd_date).FirstOrDefault());
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
        }

        private void DoctorAviation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            DestinationField desField = desFieldAviation.Where(x => x.fieldName == e.PropertyName).FirstOrDefault();
            var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
            if (desField != null)
            {
                char? valChar = ConvertToChar(val);
                if (valChar == 'A')
                {
                    desField.txtBox.Enabled = true;
                }
                else
                {
                    TypeDescriptor.GetProperties(sender)[desField.fieldRemarkName].SetValue(sender, null);
                    desField.txtBox.Enabled = false;
                }
            }
            else if (e.PropertyName == "trav_recomment")
            {
                char? valChar = ConvertToChar(val);
                txtfitcls.DataBindings.Clear();
                txtunitcls.DataBindings.Clear();
                txtdeferred.DataBindings.Clear();
                txtfitcls.Text = "";
                txtunitcls.Text = "";
                txtdeferred.Text = "";
                if (valChar == 'F')
                {
                    txtfitcls.Enabled = true;
                    txtunitcls.Enabled = false;
                    txtdeferred.Enabled = false;
                }
                else if (valChar == 'U')
                {
                    txtfitcls.Enabled = false;
                    txtunitcls.Enabled = true;
                    txtdeferred.Enabled = false;
                }
                else if (valChar == 'D')
                {
                    txtfitcls.Enabled = false;
                    txtunitcls.Enabled = false;
                    txtdeferred.Enabled = true;
                }
                else
                {
                    txtfitcls.Enabled = false;
                    txtunitcls.Enabled = false;
                    txtdeferred.Enabled = false;
                }
            }
            else
            {
                trn_audiometric_hdr PatientAudio = bsPatientAudio.OfType<trn_audiometric_hdr>().FirstOrDefault();
                if (PatientAudio != null)
                {
                    int? valInt = val == null ? null : ConvertToInt(val);
                    switch (e.PropertyName)
                    {
                        case "trav_rear_500":
                            PatientAudio.tdh_right_level_500 = valInt;
                            break;
                        case "trav_lear_500":
                            PatientAudio.tdh_left_level_500 = valInt;
                            break;
                        case "trav_rear_1000":
                            PatientAudio.tdh_right_level_1000 = valInt;
                            break;
                        case "trav_lear_1000":
                            PatientAudio.tdh_left_level_1000 = valInt;
                            break;
                        case "trav_rear_2000":
                            PatientAudio.tdh_right_level_2000 = valInt;
                            break;
                        case "trav_lear_2000":
                            PatientAudio.tdh_left_level_2000 = valInt;
                            break;
                        case "trav_rear_3000":
                            PatientAudio.tdh_right_level_3000 = valInt;
                            break;
                        case "trav_lear_3000":
                            PatientAudio.tdh_left_level_3000 = valInt;
                            break;
                        case "trav_rear_4000":
                            PatientAudio.tdh_right_level_4000 = valInt;
                            break;
                        case "trav_lear_4000":
                            PatientAudio.tdh_left_level_4000 = valInt;
                            break;
                    }
                }
            }
        }
        private void ShowLabResult(int? tpr_id)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_doctor_aviation DoctorAviation = bsDoctorAviation.OfType<trn_doctor_aviation>().FirstOrDefault();
                    var labResult = dbc.sp_med_exm_report(tpr_id).FirstOrDefault();
                    if (labResult != null)
                    {
                        txtBlgr.Text = labResult.A0200;
                        txtRh.Text = labResult.L0030;
                        txtHb.Text = labResult.A0001;
                        txtVpc.Text = "";
                        txtFBS.Text = labResult.C0180;
                        txtChol.Text = labResult.C0130;
                        txtTrig.Text = labResult.C0140;
                        txtHDL.Text = labResult.C0150;
                        txtLDL.Text = labResult.C0159;
                        txtSGOT.Text = labResult.C0040;
                        txtSGPT.Text = labResult.C0030;
                        txtAlk.Text = labResult.C0010;
                        txtBun.Text = labResult.C0080;
                        txtCreat.Text = labResult.C0070;
                        txtUric.Text = labResult.C0320;
                        txtVDRL.Text = labResult.P1015;
                        //txtHIV.Text = labResult.X0012 == null ? "Not Test" : "Tested";
                        txtHIV.Text = labResult.X0012;
                        txtHBsAg.Text = labResult.N7014;
                        txtSugar.Text = labResult.D1105;
                        txtAlbumin.Text = labResult.C0060;
                        txtMicro.Text = labResult.E0020;

                        if (labResult.ecg_normal == null && labResult.ecg_abnormal == null)
                        {
                            radioButton78.Checked = false;
                            radioButton79.Checked = false;
                        }
                        else if (labResult.ecg_normal.Length != 0)
                        {
                            radioButton78.Checked = true;
                        }
                        else if (labResult.ecg_normal.Length != 0)
                        {
                            radioButton79.Checked = true;
                        }

                        if (labResult.chest_x_normal == null && labResult.chest_x_abnormal == null)
                        {
                            radioButton80.Checked = false;
                            radioButton81.Checked = false;
                        }
                        else if (labResult.chest_x_normal.Length != 0)
                        {
                            radioButton80.Checked = true;
                        }
                        else if (labResult.chest_x_normal.Length != 0)
                        {
                            radioButton81.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "ShowLabResult", ex, false); 
            }
        }

        public void EndEdit()
        {

        }
        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }

        private double? ConvertToDouble(object value)
        {
            try
            {
                double result = Convert.ToDouble(value);
                return result;
            }
            catch
            {
                return null;
            }
        }
        private int? ConvertToInt(object value)
        {
            try
            {
                int result = Convert.ToInt32(value);
                return result;
            }
            catch
            {
                return null;
            }
        }
        private char? ConvertToChar(object value)
        {
            try
            {
                char result = Convert.ToChar(value);
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
