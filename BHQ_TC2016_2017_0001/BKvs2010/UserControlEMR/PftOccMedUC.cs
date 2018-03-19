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

namespace BKvs2010.UserControlEMR
{
    public partial class PftOccMedUC : UserControl
    {
        public PftOccMedUC()
        {
            InitializeComponent();

            txtfvc_active.TextChanged += new EventHandler(updateBindingTextBox);
            txtfvc_target.TextChanged += new EventHandler(updateBindingTextBox);
            txtfve1_active.TextChanged += new EventHandler(updateBindingTextBox);
            txtfve1_target.TextChanged += new EventHandler(updateBindingTextBox);
            txtactive_value.TextChanged += new EventHandler(updateBindingTextBox);
            txttarget_value.TextChanged += new EventHandler(updateBindingTextBox);
            txtfef_active.TextChanged += new EventHandler(updateBindingTextBox);
            txtfef_target.TextChanged += new EventHandler(updateBindingTextBox);
        }
        private void updateBindingTextBox(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            foreach (Binding bind in txtBox.DataBindings)
            {
                bind.WriteValue();
            }
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
                        trn_pft patientPft = value.trn_pfts.FirstOrDefault();
                        if (patientPft == null)
                        {
                            patientPft = new trn_pft();
                            value.trn_pfts.Add(patientPft);
                        }
                        patientPft.PropertyChanged += new PropertyChangedEventHandler(patientPft_PropertyChanged);
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "tpr_id", ex, false);
                    }
                }
            }
        }
        private void patientPft_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            double? active = null;
            double? target = null;
            string result = null;
            switch (e.PropertyName)
            {
                case "tpffvc_active":
                case "tpffvc_target":
                    active = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpffvc_active"].GetValue(sender));
                    target = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpffvc_target"].GetValue(sender));
                    result = cal_Matchs(active, target);
                    TypeDescriptor.GetProperties(sender)["tpfmatch1_value"].SetValue(sender, result);
                    break;
                case "tpffve1_active":
                case "tpffve1_target":
                    active = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpffve1_active"].GetValue(sender));
                    target = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpffve1_target"].GetValue(sender));
                    result = cal_Matchs(active, target);
                    TypeDescriptor.GetProperties(sender)["tpfmatch2_value"].SetValue(sender, result);
                    break;
                case "tpfactive_value":
                case "tpftarget_value":
                    active = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpfactive_value"].GetValue(sender));
                    target = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpftarget_value"].GetValue(sender));
                    result = cal_Matchs(active, target);
                    TypeDescriptor.GetProperties(sender)["tpfmatch3_value"].SetValue(sender, result);
                    break;
                case "tpffef_active":
                case "tpffef_target":
                    active = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpffef_active"].GetValue(sender));
                    target = ConvertToDoubleNull(TypeDescriptor.GetProperties(sender)["tpffef_target"].GetValue(sender));
                    result = cal_Matchs(active, target);
                    TypeDescriptor.GetProperties(sender)["tpfmatch4_value"].SetValue(sender, result);
                    break;
            }
        }

        private string cal_Matchs(object num_active, object num_target)
        {
            try
            {
                if (num_active == null || num_target == null)
                {
                    return null;
                }
                else if (Convert.ToDouble(num_target) == 0)
                {
                    return null;
                }
                else
                {
                    double num = Convert.ToDouble(num_active) / Convert.ToDouble(num_target) * 100;
                    return num.ToString("0.00");
                }
            }
            catch
            {
                return null;
            }
        }
        private double? ConvertToDoubleNull(object value)
        {
            try
            {
                double num = Convert.ToDouble(value);
                return num;
            }
            catch
            {
                return null;
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                txtPFTOccMedAbnormal.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                txtPFTOccMedAbnormal.Enabled = true;
                txtPFTOccMedAbnormal.Focus();
            }
            else
            {
                txtPFTOccMedAbnormal.Enabled = false;
                bsPatientPFT.OfType<trn_pft>().FirstOrDefault().tpf_pft_occmed_abnormal = null;
            }
        }
    }
}
