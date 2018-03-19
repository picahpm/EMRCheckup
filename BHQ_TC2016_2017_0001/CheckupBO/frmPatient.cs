using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace CheckupBO
{
    public partial class frmPatient : Form
    {
        public frmPatient()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void frmRegister_Load(object sender, EventArgs e)
        {
            LoadData("");
        }

        private void LoadData(string strSearch)
        {
            strSearch = strSearch.Trim();
            DateTime dtnow = txtDateSearch.Value.Date;
            var objpatients= (from t1 in dbc.trn_patients
                              where t1.tpt_create_date.Value.Date==dtnow.Date
                              orderby t1.tpt_create_date
                              select t1);
            if (strSearch.Length > 0)
            {
                PatientbindingSource1.DataSource  = objpatients.Where(x => x.tpt_hn_no.Contains(strSearch) 
                                                        || x.tpt_othername.ToUpper().Contains(strSearch.ToUpper()));
            }
            else
            {
                PatientbindingSource1.DataSource = objpatients;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //save Marrie pnlMarried
                PatientbindingSource1.EndEdit();
                dbc.SubmitChanges();
                lbErrormsg.Text = "Save data completed.";
            }
            catch (Exception ex)
            {
                lbErrormsg.Text = ex.Message;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearchHNName.Text);
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtHN.Text = "";
            txtDateSearch.Value = Program.GetServerDateTime().Date;
            txtHN.Focus();
        }

        private void PatientbindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (dbc.GetChangeSet().Updates.Count > 0)
                {
                    if (MessageBox.Show("คุณต้องการบันทึกการเปลี่ยนแปลงข้อมูลหรือไม่", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbc.SubmitChanges();
                    }
                    else
                    {
                        PatientbindingSource1.CancelEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Message);
            }
            // Gender
            trn_patient currentpt = (trn_patient)PatientbindingSource1.Current;
            if (currentpt != null)
            {
                Program.SetValueRadioGroup(GBGender, currentpt.tpt_gender);
                Program.SetValueRadioGroup(pnlMarried, currentpt.tpt_married);
            }
            
            //Patient Type
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            if (currentRegis != null)
            {
                Program.SetValueRadioGroup(GBPatientType, currentRegis.tpr_patient_type);
            }
        }
        private void trnpatientregisBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (dbc.GetChangeSet().Updates.Count > 0)
                {
                    if (MessageBox.Show("คุณต้องการบันทึกการเปลี่ยนแปลงข้อมูลหรือไม่", "Confirm Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbc.SubmitChanges();
                    }
                    else
                    {
                        trnpatientregisBindingSource.CancelEdit();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Message);
            }

            //Patient Type
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            if (currentRegis != null)
            {
                Program.SetValueRadioGroup(GBPatientType, currentRegis.tpr_patient_type);
                RDGeneral_CheckedChanged(null, null);
                CHSameMainAddress.Checked = false; CHOfficeAddress.Checked = false;
                txtMainAddress.Text = currentRegis.tpr_main_address + " แขวง " + currentRegis.tpr_main_tumbon + " เขต " + currentRegis.tpr_main_amphur + " จังหวัด " + currentRegis.tpr_main_province + " " + currentRegis.tpr_main_zip_code;
                if (currentRegis.trn_patient != null && currentRegis.trn_patient.tpt_image != null)
                {
                    pictureBox1.Image = Program.byteArrayToImage(currentRegis.trn_patient.tpt_image.ToArray());
                }
            }

            var objPackItemList = (from t1 in currentRegis.trn_patient_order_items
                                   select new PackageBOView
                                   {
                                       ItemSet = (from t2 in currentRegis.trn_patient_order_sets where t2.tos_id==t1.tos_id select t2.tos_item_row_id).FirstOrDefault(),
                                       ItemSetCode =(from t2 in currentRegis.trn_patient_order_sets where t2.tos_id==t1.tos_id select t2.tos_od_set_code).FirstOrDefault(),
                                       ItemSetName =(from t2 in currentRegis.trn_patient_order_sets where t2.tos_id==t1.tos_id select t2.tos_od_set_name).FirstOrDefault(),
                                       ItemID = t1.toi_item_row_id,
                                       ItemCode = t1.toi_od_item_code,
                                       ItemName = t1.toi_od_item_name
                                   }).ToList();
            GridPackage.DataSource = new SortableBindingList<PackageBOView>(objPackItemList);

            //Load Queue History
            var objQueueHistory = (from t1 in currentRegis.trn_patient_queues
                                   select new QueueHistory
                                   {
                                       RoomName = t1.mst_room_hdr.mrm_ename,
                                       EventName =(from t2 in dbc.mst_events where t2.mvt_id== t1.mvt_id select t2.mvt_ename).FirstOrDefault(),
                                       CallBy = t1.tps_call_by,
                                       CallDate = t1.tps_call_date,
                                       HoldBy = t1.tps_hold_by,
                                       HoldDate = t1.tps_hold_date,
                                       CancelBy = t1.tps_cancel_by,
                                       CencelDate = t1.tps_cancel_date,
                                       CancelRemark1 = t1.tps_cancel_remark,
                                       CancelRemark2 = t1.tps_cancel_other
                                   }).ToList();

            GridQueueHistory.DataSource = new SortableBindingList<QueueHistory>(objQueueHistory);

           //LoadBasicMeasurement
          int  SetTprID = currentRegis.tpr_id;
          trn_basic_measure_hdr objhdr = dbc.trn_basic_measure_hdrs.Where(c => c.tpr_id == SetTprID).FirstOrDefault();
            if (objhdr != null)
            {
                BasicMeasurementbindingSource1.DataSource = objhdr;
                trn_basic_measure_hdr BasicMensureCurrent = (trn_basic_measure_hdr)BasicMeasurementbindingSource1.Current;
                // set Radio

                Program.SetValueRadioGroup(GBStatusOnArrival, BasicMensureCurrent.tbm_arrive.ToString());
                Program.SetValueRadioGroup(GBPurpose, BasicMensureCurrent.tbm_purpose.ToString());
                Program.SetValueRadioGroup(GBGeneralAppearance, BasicMensureCurrent.tbm_appearance.ToString());
                Program.SetValueRadioGroup(GBFallPrecaution, BasicMensureCurrent.tbm_precaution.ToString());
                Program.SetValueRadioGroup(GBEyeGlasseslen, BasicMensureCurrent.tbm_glass_or_contact.ToString());
                Program.SetValueRadioGroup(GBColorBindness, BasicMensureCurrent.tbm_color_blind.ToString());
                Program.SetValueRadioGroup(GBTriage, BasicMensureCurrent.tbm_triage);

                var objBasicMeasurementDetail= (from t1 in dbc.trn_basic_measure_dtls
                                                         where t1.trn_basic_measure_hdr.trn_patient_regi.trn_patient.tpt_hn_no == currentRegis.trn_patient.tpt_hn_no
                                                         orderby t1.tbd_update_by descending
                                                         select new BasicMeasurementDetail
                                                         {
                                                             Height=t1.tbd_height,
                                                             Weight=t1.tbd_weight,
                                                             BMI=t1.tbd_bmi,
                                                             BP=((t1.tbd_systolic!=null)?t1.tbd_systolic:"") + " / "+ ((t1.tbd_diastolic!=null)?t1.tbd_diastolic:""),
                                                             Pulse=t1.tbd_pulse,
                                                             Waist=t1.tbd_waist,
                                                             RR=t1.tbd_rr,
                                                             Temp=t1.tbd_temp
                                                         }).Take(5);
                GridBasicMeasurementDetail.DataSource = new SortableBindingList<BasicMeasurementDetail>(objBasicMeasurementDetail.ToList());

            }
            //End LoadBasicMeasurement
        }

        private void RDmale_CheckedChanged(object sender, EventArgs e)
        {
            if (RDmale.Checked)
            {
                trn_patient currentpt = (trn_patient)PatientbindingSource1.Current;
                currentpt.tpt_gender = 'M';
            }
            else if (RDFemale.Checked)
            {
                trn_patient currentpt = (trn_patient)PatientbindingSource1.Current;
                currentpt.tpt_gender = 'F';
            }
        }
        private void RDGeneral_CheckedChanged(object sender, EventArgs e)
        {
            DDCompany.Enabled = false; txtEmployeeID.Enabled = false;
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            if (currentRegis != null)
            {
                if (RDGeneral.Checked)
                {
                    currentRegis.tpr_patient_type = Convert.ToChar(RDGeneral.Tag);
                    currentRegis.tpr_company_id = null;
                    currentRegis.tpr_employee_no = null;
                }
                else if (RDAviationPilot.Checked)
                {
                    currentRegis.tpr_patient_type = Convert.ToChar(RDAviationPilot.Tag);
                    currentRegis.tpr_company_id = null;
                    currentRegis.tpr_employee_no = null;
                }
                else if (RDcorporate.Checked)
                {
                    currentRegis.tpr_patient_type = Convert.ToChar(RDcorporate.Tag);
                    DDCompany.Enabled = true; txtEmployeeID.Enabled = true;
                }
                //else if (RDaviationAircrew.Checked)
                //{
                //    currentRegis.tpr_patient_type = Convert.ToChar(RDaviationAircrew.Tag);
                //    currentRegis.tpr_company_id = null;
                //    currentRegis.tpr_employee_no = null;
                //}
            }
        }
        private void CHSameMainAddress_CheckedChanged(object sender, EventArgs e)
        {
            trn_patient_regi objregis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            if (CHSameMainAddress.Checked)
            {
                objregis.tpr_other_address = objregis.tpr_main_address;
                objregis.tpr_other_province = objregis.tpr_main_province;
                objregis.tpr_other_amphur = objregis.tpr_main_amphur;
                objregis.tpr_other_tumbon = objregis.tpr_main_tumbon;
                objregis.tpr_other_zip_code = objregis.tpr_main_zip_code;
                CHOfficeAddress.Checked = false;
            }
            //else
            //{
            //    objregis.tpr_other_address = "";
            //    objregis.tpr_other_province = "";
            //    objregis.tpr_other_amphur = "";
            //    objregis.tpr_other_tumbon = "";
            //    objregis.tpr_other_zip_code = "";
            //}
        }
        private void CHOfficeAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (CHOfficeAddress.Checked)
            {
                trn_patient_regi objregis = (trn_patient_regi)trnpatientregisBindingSource.Current;
                objregis.tpr_other_address = "";
                objregis.tpr_other_province = "";
                objregis.tpr_other_amphur = "";
                objregis.tpr_other_tumbon = "";
                objregis.tpr_other_zip_code = "";
                CHSameMainAddress.Checked = false;
            }
        }

        private void GridPatient_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPatient.SetRuningNumber(0);
        }
        private void GridPatientRegis_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPatientRegis.SetRuningNumber(0);
        }
        private void GridPackage_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPackage.SetRuningNumber(0);
        }
        private void GridOutDepartment_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridOutDepartment.SetRuningNumber(0);
        }
        private void GridPatientAlert_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPatientAlert.SetRuningNumber(0);
        }
        private void GridQueueHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridQueueHistory.SetRuningNumber(0);
        }
        private void GridBasicMeasurementDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridBasicMeasurementDetail.SetRuningNumber(0);
        }

        private void btnViewCarotid1_Click(object sender, EventArgs e)
        {
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            frmCarotid frmca = new frmCarotid();
            frmca.TprID = currentRegis.tpr_id;
            frmca.ShowDialog();
        }
        private void btnviewEye_Click(object sender, EventArgs e)
        {
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            frmViewEyes frmeye = new frmViewEyes();
            frmeye.TprID = currentRegis.tpr_id;
            frmeye.ShowDialog();
        }
        private void btnViewHearing_Click(object sender, EventArgs e)
        {
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            frmViewHearing frmHearing = new frmViewHearing();
            frmHearing.TprID = currentRegis.tpr_id;
            frmHearing.ShowDialog();
        }
        private void btnABI_Click(object sender, EventArgs e)
        {
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            frmViewABI frmABI = new frmViewABI();
            frmABI.TprID = currentRegis.tpr_id;
            frmABI.ShowDialog();
        }
        private void btnTeeth_Click(object sender, EventArgs e)
        {
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            frmViewTeeth frmTeeth = new frmViewTeeth();
            frmTeeth.TprID = currentRegis.tpr_id;
            frmTeeth.ShowDialog();
        }
        private void btnPapTest_Click(object sender, EventArgs e)
        {
            trn_patient_regi currentRegis = (trn_patient_regi)trnpatientregisBindingSource.Current;
            frmViewObstetrics frmObs = new frmViewObstetrics();
            frmObs.TprID = currentRegis.tpr_id;
            frmObs.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
       
    }
    class PackageBOView
    {
        public String ItemSet { get; set; }
        public String ItemSetCode { get; set; }
        public String ItemSetName { get; set; }
        public String ItemID { get; set; }
        public String ItemCode { get; set; }
        public String ItemName { get; set; }
    }
    class QueueHistory
    {
        public string RoomName{get;set;}
        public string EventName{get;set;}
        public string CallBy{get;set;}
        public DateTime? CallDate{get;set;}
        public string HoldBy{get;set;}
        public DateTime? HoldDate{get;set;}
        public string CancelBy{get;set;}
        public DateTime? CencelDate{get;set;}
        public string CancelRemark1{get;set;}
        public string CancelRemark2 { get; set; }
    }
    class BasicMeasurementDetail
    {
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BMI { get; set; }
        public string BP { get; set; }
        public string Pulse { get; set; }
        public string Waist { get; set; }
        public string RR { get; set; }
        public string Temp { get; set; }
    }
}
