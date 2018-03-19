using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010
{
    public partial class PatientBookAddressFrm : Form
    {
        public PatientBookAddressFrm()
        {
            InitializeComponent();
        }

        public string PatientNameTH { get; set; }
        public string PatientNameEN { get; set; }
        public string ContactPersonName { get; set; }
        public string PatientAddress { get; set; }
        public string CompanyAddress { get; set; }
        public string OtherAddress { get; set; }
        public string FlagName { get; set; }
        public string FlagAddress { get; set; }

        private InhCheckupDataContext cdc;
        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value == null)
                {
                    if (cdc != null) cdc.Dispose();
                    _tpr_id = null;
                }
                else
                {
                    try
                    {
                        cdc = new InhCheckupDataContext();
                        trn_patient_regi patientRegis = cdc.trn_patient_regis
                                                           .Where(x => x.tpr_id == value)
                                                           .FirstOrDefault();
                        if (patientRegis.trn_patient_book_address == null)
                        {
                            patientRegis.trn_patient_book_address = new trn_patient_book_address();
                            pnName.Tag = FlagName;
                            patientRegis.trn_patient_book_address.tpba_flag_name = FlagName;
                            switch (FlagName)
                            {
                                case "TH":
                                    patientRegis.trn_patient_book_address.tpba_receiver = PatientNameTH;
                                    //rdTH.Checked = true;
                                    break;
                                case "EN":
                                    patientRegis.trn_patient_book_address.tpba_receiver = PatientNameEN;
                                    //rdEN.Checked = true;
                                    break;
                                case "CP":
                                    patientRegis.trn_patient_book_address.tpba_receiver = ContactPersonName;
                                    break;
                            }
                            pnAddress.Tag = FlagAddress;
                            patientRegis.trn_patient_book_address.tpba_flag_address = FlagAddress;
                            switch (FlagAddress)
                            {
                                case "H":
                                    patientRegis.trn_patient_book_address.tpba_send_address = PatientAddress;
                                    //rdHome.Checked = true;
                                    txtAddress.ReadOnly = true;
                                    break;
                                case "C":
                                    patientRegis.trn_patient_book_address.tpba_send_address = CompanyAddress;
                                    txtAddress.ReadOnly = true;
                                    //rdCom.Checked = true;
                                    break;
                                case "O":
                                    patientRegis.trn_patient_book_address.tpba_send_address = OtherAddress;
                                    txtAddress.ReadOnly = false;
                                    //rdOT.Checked = true;
                                    break;
                            }

                            //patientRegis.trn_patient_book_address.tpba_receiver = patientRegis.tpr_other_name;
                            //patientRegis.trn_patient_book_address.tpba_send_address = patientRegis.tpr_other_address +
                            //                                                          " ต." + patientRegis.tpr_other_tumbon +
                            //                                                          " อ." + patientRegis.tpr_other_amphur +
                            //                                                          " จ." + patientRegis.tpr_other_province +
                            //                                                          patientRegis.tpr_other_zip_code;
                            //patientRegis.trn_patient_book_address.tpba_send_address = patientRegis.trn_patient_book_address.tpba_send_address.Replace(Environment.NewLine, " ");
                        }
                        else
                        {
                            var flagName = patientRegis.trn_patient_book_address.tpba_flag_name;
                            pnName.Tag = flagName;
                            //switch (flagName)
                            //{
                            //    case "TH":
                            //        rdTH.Checked = true;
                            //        break;
                            //    case "EN":
                            //        rdEN.Checked = true;
                            //        break;
                            //}
                            var flagAdd = patientRegis.trn_patient_book_address.tpba_flag_address;
                            pnAddress.Tag = flagAdd;
                            switch (flagAdd)
                            {
                                case "H":
                                    txtAddress.ReadOnly = true;
                                    break;
                                case "C":
                                    txtAddress.ReadOnly = true;
                                    break;
                                case "O":
                                    txtAddress.ReadOnly = false;
                                    break;
                            }
                        }
                        PatientBookAddress = patientRegis.trn_patient_book_address;
                        rdTH.CheckedChanged += Name_RadioChange;
                        rdEN.CheckedChanged += Name_RadioChange;
                        rdPerson.CheckedChanged += Name_RadioChange;
                        rdHome.CheckedChanged += Address_RadioChange;
                        rdCom.CheckedChanged += Address_RadioChange;
                        rdOT.CheckedChanged += Address_RadioChange;
                        _tpr_id = value;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "tpr_id", ex, false);
                    }
                }
            }
        }
        public string username { get; set; }

        BindingSource bsPatientBookAddress;
        private trn_patient_book_address _patientBookAddress;
        private trn_patient_book_address PatientBookAddress
        {
            get { return _patientBookAddress; }
            set
            {
                if (value == null)
                {
                    bsPatientBookAddress.DataSource = new trn_patient_book_address();
                    _patientBookAddress = null;
                }
                else
                {
                    try
                    {
                        bsPatientBookAddress = new BindingSource();
                        bsPatientBookAddress.DataSource = value;
                        TxtReceiver.DataBindings.Clear();
                        TxtReceiver.DataBindings.Add(new Binding("Text", bsPatientBookAddress, "tpba_receiver"));
                        txtAddress.DataBindings.Clear();
                        txtAddress.DataBindings.Add(new Binding("Text", bsPatientBookAddress, "tpba_send_address"));
                        _patientBookAddress = value;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "PatientBookAddress", ex, false);
                    }
                }
            }
        }

        private void Name_RadioChange(object sender, EventArgs e)
        {
            var rd = sender as RadioButton;
            if (rd != null)
            {
                if (rd.Checked)
                {
                    switch (rd.Tag.ToString())
                    {
                        case "TH":
                            _patientBookAddress.tpba_flag_name = "TH";
                            _patientBookAddress.tpba_receiver = PatientNameTH;
                            break;
                        case "EN":
                            _patientBookAddress.tpba_flag_name = "EN";
                            _patientBookAddress.tpba_receiver = PatientNameEN;
                            break;
                        case "CP":
                            _patientBookAddress.tpba_flag_name = "CP";
                            _patientBookAddress.tpba_receiver = ContactPersonName;
                            break;
                    }
                }
            }
        }
        private void Address_RadioChange(object sender, EventArgs e)
        {
            var rd = sender as RadioButton;
            if (rd != null)
            {
                if (rd.Checked)
                {
                    switch (rd.Tag.ToString())
                    {
                        case "H":
                            _patientBookAddress.tpba_flag_address = "H";
                            _patientBookAddress.tpba_send_address = PatientAddress;
                            txtAddress.ReadOnly = true;
                            break;
                        case "C":
                            _patientBookAddress.tpba_flag_address = "C";
                            _patientBookAddress.tpba_send_address = CompanyAddress;
                            txtAddress.ReadOnly = true;
                            break;
                        case "O":
                            _patientBookAddress.tpba_flag_address = "O";
                            txtAddress.ReadOnly = false;
                            break;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                if (_patientBookAddress.tpba_create_date == null)
                {
                    _patientBookAddress.tpba_create_by = username;
                    _patientBookAddress.tpba_create_date = dateNow;
                }
                _patientBookAddress.tpba_update_by = username;
                _patientBookAddress.tpba_update_date = dateNow;
                cdc.SubmitChanges();
                lbAlertMsg.Text = "Save Data Complete.";
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSave_Click", ex, false);
                lbAlertMsg.Text = "Please Try Again.";
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> rptCode = new List<string> { "BK401" };
                //Report.frmPreviewReport frm = new Report.frmPreviewReport((int)_tpr_id, rptCode, false, 1, true);
                Report.frmPreviewReport frm = new Report.frmPreviewReport((int)_tpr_id, rptCode);
                frm.previewReport();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnPrint_Click", ex, false);
                lbAlertMsg.Text = "Please Try Again.";
            }
        }
    }
}
