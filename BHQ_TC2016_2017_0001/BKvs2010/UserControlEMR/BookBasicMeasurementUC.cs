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
    public partial class BookBasicMeasurementUC : UserControl
    {
        public BookBasicMeasurementUC()
        {
            InitializeComponent();
        }

        private InhCheckupDataContext cdc;
        private trn_patient_regi PatientRegis;
        private int? _tpr_id = null;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                _tpr_id = value;
                if (_tpr_id == null)
                {
                    if (cdc != null) cdc.Dispose();
                    PatientRegisBS.DataSource = new trn_patient_regi();
                    ClearBinding();
                    panel1.Enabled = false;
                }
                else
                {
                    cdc = new InhCheckupDataContext();
                    ClearBinding();
                    PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                    if (PatientRegis == null)
                    {
                        PatientRegisBS.DataSource = new trn_patient_regi();
                    }
                    else
                    {
                        PatientRegisBS.DataSource = PatientRegis;
                    }
                    panel1.Enabled = true;
                }
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            tpr_id = _tpr_id;
        }

        private void ClearBinding()
        {
            txtWeight1.Text = "";
            txtHieght1.Text = "";
            txtBMI1.Text = "";
            txtBP1.Text = "";
            txtPulseRate1.Text = "";
            txtRespirationRate1.Text = "";
            txtTemperature1.Text = "";
            txtWeight2.Text = "";
            txtHieght2.Text = "";
            txtBMI2.Text = "";
            txtBP2.Text = "";
            txtPulseRate2.Text = "";
            txtRespirationRate2.Text = "";
            txtTemperature2.Text = "";
            txtWeight3.Text = "";
            txtHieght3.Text = "";
            txtBMI3.Text = "";
            txtBP3.Text = "";
            txtPulseRate3.Text = "";
            txtRespirationRate3.Text = "";
            txtTemperature3.Text = "";
            lbDate1.Text = "";
            lbDate2.Text = "";
            lbDate3.Text = "";
        }
        private void BasicBS_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            BindingSource source = sender as BindingSource;
            if (source != null)
            {
                trn_basic_measure_hdr hdr = source.OfType<trn_basic_measure_hdr>().FirstOrDefault();
                if (hdr != null)
                {
                    if (hdr.tbm_color_blind == null)
                    {
                        radioButton1.Checked = true;
                    }

                    List<trn_basic_measure_dtl> listDtl = hdr.trn_basic_measure_dtls.OrderByDescending(x => x.tbd_date).ToList();
                    if (listDtl.Count() > 0)
                    {
                        var basic_dtl = listDtl[0];
                        if (basic_dtl.tbd_date == null)
                        {
                            lbDate1.Text = "1st";
                        }
                        else
                        {
                            lbDate1.Text = basic_dtl.tbd_date.Value.ToString("dd/MM/yyyy");
                        }
                        txtWeight1.Text = (basic_dtl.tbd_weight == null) ? "" : basic_dtl.tbd_weight.ToString();
                        txtHieght1.Text = (basic_dtl.tbd_height == null) ? "" : basic_dtl.tbd_height.ToString();
                        txtBMI1.Text = (basic_dtl.tbd_bmi == null) ? "" : basic_dtl.tbd_bmi.ToString();
                        string txtBP_dia = (basic_dtl.tbd_diastolic == null)
                            ? ""
                            : basic_dtl.tbd_diastolic.ToString();
                        string txtBP_sys = (basic_dtl.tbd_systolic == null)
                            ? ""
                            : basic_dtl.tbd_systolic.ToString();
                        txtBP1.Text = txtBP_sys + '/' + txtBP_dia;
                        txtPulseRate1.Text = (basic_dtl.tbd_pulse == null) ? "" : basic_dtl.tbd_pulse.ToString();
                        txtRespirationRate1.Text = (basic_dtl.tbd_rr == null) ? "" : basic_dtl.tbd_rr.ToString();
                        txtTemperature1.Text = (basic_dtl.tbd_temp == null) ? "" : basic_dtl.tbd_temp.ToString();
                    }
                    if (listDtl.Count() > 1)
                    {
                        var basic_dtl = listDtl[1];
                        if (basic_dtl.tbd_date == null)
                        {
                            lbDate2.Text = "2nd";
                        }
                        else
                        {
                            lbDate2.Text = basic_dtl.tbd_date.Value.ToString("dd/MM/yyyy");
                        }
                        txtWeight2.Text = (basic_dtl.tbd_weight == null) ? "" : basic_dtl.tbd_weight.ToString();
                        txtHieght2.Text = (basic_dtl.tbd_height == null) ? "" : basic_dtl.tbd_height.ToString();
                        txtBMI2.Text = (basic_dtl.tbd_bmi == null) ? "" : basic_dtl.tbd_bmi.ToString();
                        string txtBP_dia = (basic_dtl.tbd_diastolic == null)
                            ? ""
                            : basic_dtl.tbd_diastolic.ToString();
                        string txtBP_sys = (basic_dtl.tbd_systolic == null)
                            ? ""
                            : basic_dtl.tbd_systolic.ToString();
                        txtBP2.Text = txtBP_sys + '/' + txtBP_dia;
                        txtPulseRate2.Text = (basic_dtl.tbd_pulse == null) ? "" : basic_dtl.tbd_pulse.ToString();
                        txtRespirationRate2.Text = (basic_dtl.tbd_rr == null) ? "" : basic_dtl.tbd_rr.ToString();
                        txtTemperature2.Text = (basic_dtl.tbd_temp == null) ? "" : basic_dtl.tbd_temp.ToString();
                    }
                    if (listDtl.Count() > 2)
                    {
                        var basic_dtl = listDtl[2];
                        if (basic_dtl.tbd_date == null)
                        {
                            lbDate3.Text = "3rd";
                        }
                        else
                        {
                            lbDate3.Text = basic_dtl.tbd_date.Value.ToString("dd/MM/yyyy");
                        }
                        txtWeight3.Text = (basic_dtl.tbd_weight == null) ? "" : basic_dtl.tbd_weight.ToString();
                        txtHieght3.Text = (basic_dtl.tbd_height == null) ? "" : basic_dtl.tbd_height.ToString();
                        txtBMI3.Text = (basic_dtl.tbd_bmi == null) ? "" : basic_dtl.tbd_bmi.ToString();
                        string txtBP_dia = (basic_dtl.tbd_diastolic == null)
                            ? ""
                            : basic_dtl.tbd_diastolic.ToString();
                        string txtBP_sys = (basic_dtl.tbd_systolic == null)
                            ? ""
                            : basic_dtl.tbd_systolic.ToString();
                        txtBP3.Text = txtBP_sys + '/' + txtBP_dia;
                        txtPulseRate3.Text = (basic_dtl.tbd_pulse == null) ? "" : basic_dtl.tbd_pulse.ToString();
                        txtRespirationRate3.Text = (basic_dtl.tbd_rr == null) ? "" : basic_dtl.tbd_rr.ToString();
                        txtTemperature3.Text = (basic_dtl.tbd_temp == null) ? "" : basic_dtl.tbd_temp.ToString();
                    }
                }
            }
        }
    }
}
