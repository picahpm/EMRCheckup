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
    public partial class CarotidDuplexReport : UserControl
    {
        public CarotidDuplexReport()
        {
            InitializeComponent();            
        }

        private class clsSourceMaster
        {
            public int? val { get; set; }
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
                        var sourceSuggestion = new EmrClass.FunctionDataCls().getDoctorResult(value.mhs_id, "CD", "CD")
                                                        .Select(x => new clsSourceMaster
                                                        {
                                                            val = x.mdr_id,
                                                            dis = x.mdr_tname
                                                        }).ToList();
                        sourceSuggestion.Insert(0, new clsSourceMaster {val=null,dis=null });
                        cmb_mdrID.DataSource = sourceSuggestion;
                        cmb_mdrID.DisplayMember = "dis";
                        cmb_mdrID.ValueMember = "val";   

                        trn_carotid_hdr patientTCH = value.trn_carotid_hdrs.FirstOrDefault();
                        if (patientTCH == null)
                        {
                            patientTCH = new trn_carotid_hdr();
                            value.trn_carotid_hdrs.Add(patientTCH);
                        }
                        LoadDataLimeStone(ref patientTCH);
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        LoadLabResult(ref value);
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
            try
            {
                trn_carotid_hdr patientTCH = _PatientRegis.trn_carotid_hdrs.FirstOrDefault();
                SaveLimeStone(ref patientTCH);
            }
            catch
            { }
        }
       
        private void LoadLabResult(ref trn_patient_regi _patient_regis)
        {
            trn_patient_regi objPatient = _patient_regis;
            trn_carotid_hdr objTCH = bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault();
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                string en = objPatient.tpr_en_no;
                string hn = objPatient.trn_patient.tpt_hn_no;
                var objotherxray = (from t1 in dbc.trn_other_xrays
                                                    where t1.tox_en_no == en
                                                      && t1.trn_patient_regi.trn_patient.tpt_hn_no == hn
                                                      && t1.tox_room_type == "CD"
                                                    select t1).FirstOrDefault();
                                if (objotherxray != null)
                                {                                    
                                    txtpatientOrderResult.Text = objTCH.tch_patient_result == null ? objotherxray.tox_result : objTCH.tch_patient_result;
                                    txtComment.Text = objTCH.tch_patient_comt == null ? objotherxray.tox_patient_comt : objTCH.tch_patient_comt;
                                    txtResult.Text = objTCH.tch_result == null ? objotherxray.tox_order_name : objTCH.tch_result;
                                    txtRequestDate.Text = objTCH.tch_request_date == null ? objotherxray.tox_order_date.Value.ToShortDateString() : objTCH.tch_request_date.Value.ToShortDateString();
                                    //request_date = Convert.ToDateTime(objotherxray.tox_order_date.Value.ToShortDateString());
                                    //report_date = Convert.ToDateTime(objotherxray.tox_result_date.Value.ToShortDateString());
                                    txtReportDateTime.Text = objTCH.tch_report_date == null ? objotherxray.tox_result_date.Value.ToShortDateString() : objTCH.tch_report_date.Value.ToShortDateString();
                                }
            }
        }        

        //Combobox_DocResult
        ToolTip tooltip = new ToolTip();
        private void tooltip_DrawItem(object sender, DrawItemEventArgs e)
        {
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 500;
            tooltip.ReshowDelay = 100;
            tooltip.AutomaticDelay = 500;
            tooltip.ShowAlways = false;
            string DropDownText;
            ComboBox combo = (ComboBox)sender;
            if (e.Index == -1)
            {
                DropDownText = string.Empty;
            }
            else
            {
                DropDownText = combo.GetItemText(combo.Items[e.Index]);
            }
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(DropDownText, e.Font, br, e.Bounds);
            }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                tooltip.Show(DropDownText, combo, e.Bounds.Right, e.Bounds.Bottom);
            }
        }
        private void tooltip_DropDownClosed(object sender, EventArgs e)
        {
            tooltip.Hide((ComboBox)sender);
        }
        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            foreach (Binding binding in combo.DataBindings)
            {
                binding.WriteValue();
            }
        }              

        //WallLimeStone
        private void LoadDataLimeStone(ref trn_carotid_hdr _patientTCH)
        {
            try
            {
                SubString(_patientTCH.tch_wall_right_ica, txt_tch_wall_right_ica1, txt_tch_wall_right_ica2, txt_tch_wall_right_ica3);
                SubString(_patientTCH.tch_wall_right_eca, txt_tch_wall_right_eca1, txt_tch_wall_right_eca2, txt_tch_wall_right_eca3);
                SubString(_patientTCH.tch_wall_right_cca, txt_tch_wall_right_cca1, txt_tch_wall_right_cca2, txt_tch_wall_right_cca3);
                SubString(_patientTCH.tch_wall_right_va, txt_tch_wall_right_va1, txt_tch_wall_right_va2, txt_tch_wall_right_va3);

                SubString(_patientTCH.tch_wall_left_ica, txt_tch_wall_left_ica1, txt_tch_wall_left_ica2, txt_tch_wall_left_ica3);
                SubString(_patientTCH.tch_wall_left_eca, txt_tch_wall_left_eca1, txt_tch_wall_left_eca2, txt_tch_wall_left_eca3);
                SubString(_patientTCH.tch_wall_left_cca, txt_tch_wall_left_cca1, txt_tch_wall_left_cca2, txt_tch_wall_left_cca3);
                SubString(_patientTCH.tch_wall_left_va, txt_tch_wall_left_va1, txt_tch_wall_left_va2, txt_tch_wall_left_va3);
            }
            catch
            { }
        }
        private void SaveLimeStone(ref trn_carotid_hdr _patientTCH)
        {
            try
            {
                _patientTCH.tch_wall_right_ica = SumString(txt_tch_wall_right_ica1.Text, txt_tch_wall_right_ica2.Text, txt_tch_wall_right_ica3.Text);
                _patientTCH.tch_wall_right_eca = SumString(txt_tch_wall_right_eca1.Text, txt_tch_wall_right_eca2.Text, txt_tch_wall_right_eca3.Text);
                _patientTCH.tch_wall_right_cca = SumString(txt_tch_wall_right_cca1.Text, txt_tch_wall_right_cca2.Text, txt_tch_wall_right_cca3.Text);
                _patientTCH.tch_wall_right_va = SumString(txt_tch_wall_right_va1.Text, txt_tch_wall_right_va2.Text, txt_tch_wall_right_va3.Text);

                _patientTCH.tch_wall_left_ica = SumString(txt_tch_wall_left_ica1.Text, txt_tch_wall_left_ica2.Text, txt_tch_wall_left_ica3.Text);
                _patientTCH.tch_wall_left_eca = SumString(txt_tch_wall_left_eca1.Text, txt_tch_wall_left_eca2.Text, txt_tch_wall_left_eca3.Text);
                _patientTCH.tch_wall_left_cca = SumString(txt_tch_wall_left_cca1.Text, txt_tch_wall_left_cca2.Text, txt_tch_wall_left_cca3.Text);
                _patientTCH.tch_wall_left_va = SumString(txt_tch_wall_left_va1.Text, txt_tch_wall_left_va2.Text, txt_tch_wall_left_va3.Text);
            }
            catch
            { }
        }
        private string SumString(string str1, string str2, string str3)
        {
            return (str1 == null ? "" : str1) + "|" + (str2 == null ? "" : str2) + "|" + (str3 == null ? "" : str3);
        }
        private void SubString(string strarray, TextBox txt1, TextBox txt2, TextBox txt3)
        {
            if (strarray != null)
            {
                string[] str = strarray.Split('|');
                if (str.Length > 0)
                    txt1.Text = str[0];
                if (str.Length > 1)
                    txt2.Text = str[1];
                if (str.Length > 2)
                    txt3.Text = str[2];
            }
        }

        #region checkbox_CheckedChanged
        //private void chk_tch_consult_doc_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chk_tch_consult_doc.Checked == true)
        //    {
        //        cmb_mdrID.Enabled = true;
        //    }
        //    else
        //    {
        //        cmb_mdrID.Enabled = false;
        //        bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().mdr_id = null;
        //    }
        //}
        private void chk_tch_follow_up_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_follow_up.Checked == true)
            {
                cmb_mdrID.Enabled = true;
            }
            else
            {
                cmb_mdrID.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().mdr_id = null;
            }
        }

        private void chk_tch_stes_right_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_right_ica.Checked == true)
            {
                txt_tch_stes_right_ica_txt.Enabled = true;
                txt_tch_stes_right_ica_txt.Focus();
            }
            else
            {
                txt_tch_stes_right_ica_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_right_ica_txt = null;
            }
        }
        private void chk_tch_stes_right_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_right_eca.Checked == true)
            {
                txt_tch_stes_right_eca_txt.Enabled = true;
                txt_tch_stes_right_eca_txt.Focus();
            }
            else
            {
                txt_tch_stes_right_eca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_right_eca_txt = null;
            }
        }
        private void chk_tch_stes_right_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_right_cca.Checked == true)
            {
                txt_tch_stes_right_cca_txt.Enabled = true;
                txt_tch_stes_right_cca_txt.Focus();
            }
            else
            {
                txt_tch_stes_right_cca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_right_cca_txt = null;
            }
        }
        private void chk_tch_stes_right_va_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_right_va.Checked == true)
            {
                txt_tch_stes_right_va_txt.Enabled = true;
                txt_tch_stes_right_va_txt.Focus();
            }
            else
            {
                txt_tch_stes_right_va_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_right_va_txt = null;
            }
        }
        private void chk_tch_stes_left_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_left_ica.Checked == true)
            {
                txt_tch_stes_left_ica_txt.Enabled = true;
                txt_tch_stes_left_ica_txt.Focus();
            }
            else
            {
                txt_tch_stes_left_ica_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_left_ica_txt = null;
            }
        }
        private void chk_tch_stes_left_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_left_eca.Checked == true)
            {
                txt_tch_stes_left_eca_txt.Enabled = true;
                txt_tch_stes_left_eca_txt.Focus();
            }
            else
            {
                txt_tch_stes_left_eca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_left_eca_txt = null;
            }
        }
        private void chk_tch_stes_left_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_left_cca.Checked == true)
            {
                txt_tch_stes_left_cca_txt.Enabled = true;
                txt_tch_stes_left_cca_txt.Focus();
            }
            else
            {
                txt_tch_stes_left_cca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_left_cca_txt = null;
            }
        }
        private void chk_tch_stes_left_va_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stes_left_va.Checked == true)
            {
                txt_tch_stes_left_va_txt.Enabled = true;
                txt_tch_stes_left_va_txt.Focus();
            }
            else
            {
                txt_tch_stes_left_va_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stes_left_va_txt = null;
            }
        }

        private void chk_tch_stok_right_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_right_ica.Checked == true)
            {
                txt_tch_stok_right_ica_txt.Enabled = true;
                txt_tch_stok_right_ica_txt.Focus();
            }
            else
            {
                txt_tch_stok_right_ica_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_right_ica_txt = null;
            }
        }
        private void chk_tch_stok_right_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_right_eca.Checked == true)
            {
                txt_tch_stok_right_eca_txt.Enabled = true;
                txt_tch_stok_right_eca_txt.Focus();
            }
            else
            {
                txt_tch_stok_right_eca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_right_eca_txt = null;
            }
        }
        private void chk_tch_stok_right_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_right_cca.Checked == true)
            {
                txt_tch_stok_right_cca_txt.Enabled = true;
                txt_tch_stok_right_cca_txt.Focus();
            }
            else
            {
                txt_tch_stok_right_cca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_right_cca_txt = null;
            }
        }
        private void chk_tch_stok_right_va_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_right_va.Checked == true)
            {
                txt_tch_stok_right_va_txt.Enabled = true;
                txt_tch_stok_right_va_txt.Focus();
            }
            else
            {
                txt_tch_stok_right_va_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_right_va_txt = null;
            }
        }
        private void chk_tch_stok_left_ica_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_left_ica.Checked == true)
            {
                txt_tch_stok_left_ica_txt.Enabled = true;
                txt_tch_stok_left_ica_txt.Focus();
            }
            else
            {
                txt_tch_stok_left_ica_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_left_ica_txt = null;
            }
        }
        private void chk_tch_stok_left_eca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_left_eca.Checked == true)
            {
                txt_tch_stok_left_eca_txt.Enabled = true;
                txt_tch_stok_left_eca_txt.Focus();
            }
            else
            {
                txt_tch_stok_left_eca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_left_eca_txt = null;
            }
        }
        private void chk_tch_stok_left_cca_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_left_cca.Checked == true)
            {
                txt_tch_stok_left_cca_txt.Enabled = true;
                txt_tch_stok_left_cca_txt.Focus();
            }
            else
            {
                txt_tch_stok_left_cca_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_left_cca_txt = null;
            }
        }
        private void chk_tch_stok_left_va_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tch_stok_left_va.Checked == true)
            {
                txt_tch_stok_left_va_txt.Enabled = true;
                txt_tch_stok_left_va_txt.Focus();
            }
            else
            {
                txt_tch_stok_left_va_txt.Enabled = false;
                bsCarotidHDR.OfType<trn_carotid_hdr>().FirstOrDefault().tch_stok_left_va_txt = null;
            }
        }
        #endregion        
    }
}
