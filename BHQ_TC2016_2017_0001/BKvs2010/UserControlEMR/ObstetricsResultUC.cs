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
    public partial class ObstetricsResultUC : UserControl
    {
        public ObstetricsResultUC()
        {
            InitializeComponent();
            addDDsuggustion();
        }

        private class docResult
        {
            public int? mdr_id { get; set; }
            public string mdr_ename { get; set; }
        }
        private void addDDsuggustion()
        {
            try
            {
                List<mst_doc_result> doctorResult = new EmrClass.FunctionDataCls().getDoctorResult(1, "PT", "PP");
                var combo = doctorResult.Select(x => new docResult
                {
                    mdr_id = x.mdr_id,
                    mdr_ename = x.mdr_ename
                }).ToList();
                combo.Insert(0, new docResult { mdr_id = null, mdr_ename = "" });
                DDsuggustion.DataSource = combo;
                DDsuggustion.DisplayMember = "mdr_ename";
                DDsuggustion.ValueMember = "mdr_id";
            }
            catch
            {

            }
        }

        public mst_user_type user { get; set; }

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
                        trn_obstetric_doc patientDoc = value.trn_obstetric_docs.FirstOrDefault();
                        if (patientDoc == null)
                        {
                            patientDoc = new trn_obstetric_doc();
                            patientDoc.obd_create_by = user == null ? null : user.mut_username;
                            value.trn_obstetric_docs.Add(patientDoc);
                        }
                        patientDoc.obd_update_by = user == null ? null : user.mut_username;

                        bsPatientRegis.DataSource = value;
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
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            DateTime dateNow = Program.GetServerDateTime();
            trn_obstetric_doc obs = (trn_obstetric_doc)bsObstetricsDoc.Current;
            if (obs.obd_create_date == null) obs.obd_create_date = dateNow;
            obs.obd_update_date = dateNow;
        }

        private void rbCellResultN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCellResultN.Checked == true)
            {
                //txtResultAbRemark.Text = null;
                rbCellDummy.Checked = false;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_result = 'N';
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_result_ab_remark = null;
            }
        }

        private void rbCellDummy_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCellDummy.Checked == true)
            {
                rbCellResultN.Checked = false;
                Boolean chkValue = false;
                foreach (Control ctrl in pnCellResult.Controls)
                {
                    if (ctrl.GetType() == typeof(RadioButton))
                    {
                        if (((RadioButton)ctrl).Checked == true)
                        {
                            chkValue = true;
                            break;
                        }
                    }
                }
                if (chkValue == false)
                {
                    //rbCellResultA.Checked = true;
                    bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_result = 'A';
                }
            }
        }

        private void rbCellResultA_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                if (rbCellDummy.Checked == false) rbCellDummy.Checked = true;
            }
        }
        private void rbCellResultL_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                if (rbCellDummy.Checked == false) rbCellDummy.Checked = true;
            }
        }
        private void rbCellResultH_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                if (rbCellDummy.Checked == false) rbCellDummy.Checked = true;
            }
        }
        private void rbCellResultO_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == false)
            {
                //txtResultAbRemark.Text = null;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_result_ab_remark = null;
            }
            else
            {
                if (rbCellDummy.Checked == false) rbCellDummy.Checked = true;
            }
        }
        private void txtResultAbRemark_TextChanged(object sender, EventArgs e)
        {
            if (txtResultAbRemark.Text.Length > 0)
            {
                if (rbCellResultO.Checked == false) bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_result = 'O';//rbCellResultO.Checked = true;
            }
        }

        private void chkRec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRec.Checked == true)
            {
                Boolean chkValue = false;
                foreach (Control ctrl in pnRecommend.Controls)
                {
                    if (ctrl.GetType().Equals(typeof(RadioButton)))
                    {
                        if (((RadioButton)ctrl).Checked == true)
                        {
                            chkValue = true;
                        }
                    }
                }
                if (chkValue == false)
                {
                    //rbRecFR.Checked = true;
                    bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_recommend = "FR";
                }
            }
            else
            {
                foreach (Control ctrl in pnRecommend.Controls)
                {
                    if (ctrl.GetType().Equals(typeof(RadioButton)))
                    {
                        //((RadioButton)ctrl).Checked = false;
                        bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_recommend = null;
                    }
                }
            }
        }

        private void rbRecFR_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                chkRec.Checked = true;
            }
        }
        private void rbRecFU_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                chkRec.Checked = true;
            }
        }
        private void rbRecFO_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                chkRec.Checked = true;
            }
            else
            {
                //txtRecRemark.Text = null;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_recommend_remark = null;
            }
        }
        private void txtRecRemark_TextChanged(object sender, EventArgs e)
        {
            if (txtRecRemark.Text.Length > 0)
            {
                if (rbRecFO.Checked == false) bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_recommend = "FO";//rbRecFO.Checked = true;
            }
        }

        private void chkOtherAb_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtherAb.Checked == true)
            {
                Boolean chkValue = false;
                foreach (Control ctrl in pnOtherAb.Controls)
                {
                    if (ctrl.GetType().Equals(typeof(RadioButton)))
                    {
                        if (((RadioButton)ctrl).Checked == true)
                        {
                            chkValue = true;
                            break;
                        }
                    }
                }
                if (chkValue == false)
                {
                    //rbOtherAbN.Checked = true;
                    bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_other_ab = 'N';
                }
            }
            else
            {
                foreach (Control ctrl in pnOtherAb.Controls)
                {
                    if (ctrl.GetType().Equals(typeof(RadioButton)))
                    {
                        //((RadioButton)ctrl).Checked = false;
                        bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_other_ab = null;
                    }
                }
            }
        }

        private void rbOtherAbN_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                chkOtherAb.Checked = true;
            }
        }
        private void rbOtherAbM_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                chkOtherAb.Checked = true;
            }
        }
        private void rbOtherAbS_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                chkOtherAb.Checked = true;
            }
            else
            {
                foreach (Control ctrl in pnOther.Controls)
                {
                    if (ctrl.GetType().Equals(typeof(UserControlLibrary.CheckBoxBinding)))
                    {
                        ((UserControlLibrary.CheckBoxBinding)ctrl).Checked = false;
                    }
                }
            }
        }

        private void chkOtherAbFungus_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                //rbOtherAbS.Checked = true;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_other_ab = 'S';
            }
        }
        private void chkOtherAbBateria_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                //rbOtherAbS.Checked = true;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_other_ab = 'S';
            }
        }
        private void chkOtherAbOther_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                //rbOtherAbS.Checked = true;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_other_ab = 'S';
            }
            else
            {
                //txtOtherAbRemark.Text = null;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_other_ab_other_remark = null;
            }
        }
        private void txtOtherAbRemark_TextChanged(object sender, EventArgs e)
        {
            if (txtOtherAbRemark.Text.Length > 0)
            {
                if (chkOtherAbOther.Checked == false) chkOtherAbOther.Checked = true;
            }
        }

        private void rbHPVTestHR_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHPVTestHR.Checked == false)
            {
                //txtHPVRemark.Text = null;
                bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_hpv_test_remark = null;
            }
        }
        private void txtHPVRemark_TextChanged(object sender, EventArgs e)
        {
            if (txtHPVRemark.Text.Length > 0)
            {
                if (rbHPVTestHR.Checked == false) bsObstetricsDoc.OfType<trn_obstetric_doc>().FirstOrDefault().obd_hpv_test = "HR";//rbHPVTestHR.Checked = true;
            }
        }
    }
}
