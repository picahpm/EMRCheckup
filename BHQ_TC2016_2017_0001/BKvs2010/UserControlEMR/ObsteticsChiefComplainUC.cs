using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.EmrClass;

namespace BKvs2010.UserControlEMR
{
    public partial class ObsteticsChiefComplainUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public ObsteticsChiefComplainUC()
        {
            InitializeComponent();
            LoadDrowDownList();

            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_obstetric_chief pap = bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault();
                if (pap != null)
                {
                    if (e == null)
                    {
                        pap.toc_doc_code = null;
                        pap.toc_doc_name = null;
                        pap.toc_doctor_license = null;
                        pap.toc_doctor_name_en = null;
                        pap.toc_doctor_name_th = null;
                    }
                    else
                    {
                        pap.toc_doc_code = ((DoctorProfile)e).SSUSR_Initials;
                        pap.toc_doc_name = ((DoctorProfile)e).CTPCP_Desc;
                        pap.toc_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        pap.toc_doctor_name_en = dn.NameEN;
                        pap.toc_doctor_name_th = dn.NameTH;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }

        private bool _isDoctorRoom = false;
        public bool isDoctorRoom
        {
            get { return _isDoctorRoom; }
            set { _isDoctorRoom = value; }
        }

        private string username;
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
                        username = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                        trn_obstetric_chief patientChief = value.trn_obstetric_chiefs.FirstOrDefault();
                        if (patientChief == null)
                        {
                            patientChief = new trn_obstetric_chief();
                            patientChief.toc_create_by = username;
                            patientChief.toc_create_date = DateTime.Now;
                            value.trn_obstetric_chiefs.Add(patientChief);
                        }
                        patientChief.toc_update_by = username;
                        patientChief.toc_update_date = DateTime.Now;

                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;

                        if (_isDoctorRoom == false)
                        {
                            autoCompleteUC1.SelectedValue = patientChief.toc_doc_code;
                        }
                        else
                        {
                            autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                            autoCompleteUC1.Enabled = false;
                        }
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
            trn_obstetric_chief obc = bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault();

            if (rdbOralCon.Checked == true) 
            {
                obc.toc_contra_flag = rdbOralCon.Tag.ToString();
                obc.toc_contra_dura_mos = Convert.ToInt32(txtOral_D_Mos.Text);
                obc.toc_contra_dura_yrs = Convert.ToInt32(txtOral_D_Yrs.Text);
                obc.toc_contra_remark = txtOralCon.Text;
            }
            else if (rdbIUD.Checked == true)
            {
                obc.toc_contra_flag = rdbIUD.Tag.ToString();
                obc.toc_contra_dura_mos = Convert.ToInt32(txtIUD_D_Mos.Text);
                obc.toc_contra_dura_yrs = Convert.ToInt32(txtIUD_D_Yrs.Text);
                obc.toc_contra_remark = txtIUD.Text;
            }
            else if (rdbInjec.Checked == true)
            {
                obc.toc_contra_flag = rdbInjec.Tag.ToString();
                obc.toc_contra_dura_mos = Convert.ToInt32(txtInjec_D_Mos.Text);
                obc.toc_contra_dura_yrs = Convert.ToInt32(txtInjec_D_Yrs.Text);
                obc.toc_contra_remark = txtInjec.Text;
            }
            else if (rdbCon_Im.Checked == true)
            {
                obc.toc_contra_flag = rdbCon_Im.Tag.ToString();
                obc.toc_contra_dura_mos = Convert.ToInt32(txtCon_Im_D_Mos.Text);
                obc.toc_contra_dura_yrs = Convert.ToInt32(txtCon_Im_D_Yrs.Text);
                obc.toc_contra_remark = txtCon_Im.Text;
            }
            else if (rdbSter.Checked == true)
            {
                obc.toc_contra_flag = rdbSter.Tag.ToString();
                obc.toc_contra_dura_mos = Convert.ToInt32(txtSter_D_Mos.Text);
                obc.toc_contra_dura_yrs = Convert.ToInt32(txtSter_D_Yrs.Text);
                obc.toc_contra_remark = txtSter.Text;
            }
        }

        private void rdbYes_Contrac_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbYes_Contrac.Checked == true)
            {
                grpContraception.Enabled = true;
                trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();

                //rdbOralCon.Checked = true;
                switch (obc.toc_contra_flag)
                {
                    case "OC":
                        rdbOralCon.Checked = true;
                        txtOralCon.Text = obc.toc_contra_remark;
                        txtOral_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtOral_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "ID":
                        rdbIUD.Checked = true;
                        txtIUD.Text = obc.toc_contra_remark;
                        txtIUD_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtIUD_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "IC":
                        rdbInjec.Checked = true;
                        txtInjec.Text = obc.toc_contra_remark;
                        txtInjec_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtInjec_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "CI":
                        rdbCon_Im.Checked = true;
                        txtInjec.Text = obc.toc_contra_remark;
                        txtCon_Im_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtCon_Im_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "SL":
                        rdbSter.Checked = true;
                        txtSter.Text = obc.toc_contra_remark;
                        txtSter_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtSter_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                }
            }
            else
            {
                grpContraception.Enabled = false;
                clearContraception();
            }
        }
        private void rdbNo_Contrac_CheckedChanged(object sender, EventArgs e)
        {
            grpContraception.Enabled = false;
            clearContraception();
        }
        private void clearContraception()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();
            obc.toc_contra_flag = null;
            obc.toc_contra_remark = null;
            obc.toc_contra_dura_mos = null;
            obc.toc_contra_dura_yrs = null;

            rdbOralCon.Checked = false;
            rdbIUD.Checked = false;
            rdbInjec.Checked = false;
            rdbCon_Im.Checked = false;
            rdbSter.Checked = false;
        }

        private void rdbOralCon_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbOralCon.Checked == true)
            {
                txtOral_D_Mos.Focus();
                txtOral_D_Mos.Enabled = true;
                txtOral_D_Yrs.Enabled = true;
                txtOralCon.Enabled = true;

                txtIUD_D_Mos.Enabled = false;
                txtIUD_D_Yrs.Enabled = false;
                txtIUD.Enabled = false;
                txtInjec_D_Mos.Enabled = false;
                txtInjec_D_Yrs.Enabled = false;
                txtInjec.Enabled = false;
                txtCon_Im_D_Mos.Enabled = false;
                txtCon_Im_D_Yrs.Enabled = false;
                txtCon_Im.Enabled = false;
                txtSter_D_Mos.Enabled = false;
                txtSter_D_Yrs.Enabled = false;
                txtSter.Enabled = false;
            }
            else
            {
                txtOral_D_Mos.Enabled = false;
                txtOral_D_Yrs.Enabled = false;
                txtOralCon.Enabled = false;
                txtOral_D_Mos.Text = null;
                txtOral_D_Yrs.Text = null;
                txtOralCon.Text = null;
            }
        }
        private void rdbIUD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbIUD.Checked == true)
            {
                txtIUD_D_Mos.Focus();
                txtIUD_D_Mos.Enabled = true;
                txtIUD_D_Yrs.Enabled = true;
                txtIUD.Enabled = true;

                txtOral_D_Mos.Enabled = false;
                txtOral_D_Yrs.Enabled = false;
                txtOralCon.Enabled = false;
                txtInjec_D_Mos.Enabled = false;
                txtInjec_D_Yrs.Enabled = false;
                txtInjec.Enabled = false;
                txtCon_Im_D_Mos.Enabled = false;
                txtCon_Im_D_Yrs.Enabled = false;
                txtCon_Im.Enabled = false;
                txtSter_D_Mos.Enabled = false;
                txtSter_D_Yrs.Enabled = false;
                txtSter.Enabled = false;
            }
            else
            {
                txtIUD_D_Mos.Enabled = false;
                txtIUD_D_Yrs.Enabled = false;
                txtIUD.Enabled = false;
                txtIUD_D_Mos.Text = null;
                txtIUD_D_Yrs.Text = null;
                txtIUD.Text = null;
            }
        }
        private void rdbInjec_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbInjec.Checked == true)
            {
                txtInjec_D_Mos.Focus();
                txtInjec_D_Mos.Enabled = true;
                txtInjec_D_Yrs.Enabled = true;
                txtInjec.Enabled = true;

                txtOral_D_Mos.Enabled = false;
                txtOral_D_Yrs.Enabled = false;
                txtOralCon.Enabled = false;
                txtIUD_D_Mos.Enabled = false;
                txtIUD_D_Yrs.Enabled = false;
                txtIUD.Enabled = false;
                txtCon_Im_D_Mos.Enabled = false;
                txtCon_Im_D_Yrs.Enabled = false;
                txtCon_Im.Enabled = false;
                txtSter_D_Mos.Enabled = false;
                txtSter_D_Yrs.Enabled = false;
                txtSter.Enabled = false;
            }
            else
            {
                txtInjec_D_Mos.Enabled = false;
                txtInjec_D_Yrs.Enabled = false;
                txtInjec.Enabled = false;
                txtInjec_D_Mos.Text = null;
                txtInjec_D_Yrs.Text = null;
                txtInjec.Text = null;
            }
        }
        private void rdbCon_Im_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCon_Im.Checked == true)
            {
                txtCon_Im_D_Mos.Focus();
                txtCon_Im_D_Mos.Enabled = true;
                txtCon_Im_D_Yrs.Enabled = true;
                txtCon_Im.Enabled = true;

                txtOral_D_Mos.Enabled = false;
                txtOral_D_Yrs.Enabled = false;
                txtOralCon.Enabled = false;
                txtIUD_D_Mos.Enabled = false;
                txtIUD_D_Yrs.Enabled = false;
                txtIUD.Enabled = false;
                txtInjec_D_Mos.Enabled = false;
                txtInjec_D_Yrs.Enabled = false;
                txtInjec.Enabled = false;
                txtSter_D_Mos.Enabled = false;
                txtSter_D_Yrs.Enabled = false;
                txtSter.Enabled = false;
            }
            else
            {
                txtCon_Im_D_Mos.Enabled = false;
                txtCon_Im_D_Yrs.Enabled = false;
                txtCon_Im.Enabled = false;
                txtCon_Im_D_Mos.Text = null;
                txtCon_Im_D_Yrs.Text = null;
                txtCon_Im.Text = null;
            }
        }
        private void rdbSter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSter.Checked == true)
            {
                txtSter_D_Mos.Focus();
                txtSter_D_Mos.Enabled = true;
                txtSter_D_Yrs.Enabled = true;
                txtSter.Enabled = true;

                txtOral_D_Mos.Enabled = false;
                txtOral_D_Yrs.Enabled = false;
                txtOralCon.Enabled = false;
                txtIUD_D_Mos.Enabled = false;
                txtIUD_D_Yrs.Enabled = false;
                txtIUD.Enabled = false;
                txtInjec_D_Mos.Enabled = false;
                txtInjec_D_Yrs.Enabled = false;
                txtInjec.Enabled = false;
                txtCon_Im_D_Mos.Enabled = false;
                txtCon_Im_D_Yrs.Enabled = false;
                txtCon_Im.Enabled = false;
            }
            else
            {
                txtSter_D_Mos.Enabled = false;
                txtSter_D_Yrs.Enabled = false;
                txtSter.Enabled = false;
                txtSter_D_Mos.Text = null;
                txtSter_D_Yrs.Text = null;
                txtSter.Text = null;
            }
        }

        private void rdbYes_Hyterectomy_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbYes_Hyterectomy.Checked != true)
            {
                grpHysterectomy.Enabled = false;
                clearHyterectomy();
            }
            else { grpHysterectomy.Enabled = true; }
        }
        private void rdbNo_Hyterectomy_CheckedChanged(object sender, EventArgs e)
        {
            grpHysterectomy.Enabled = false;
            clearHyterectomy(); 
        }
        private void clearHyterectomy()
        {
            trn_obstetric_chief obc=_PatientRegis.trn_obstetric_chiefs.FirstOrDefault();
            obc.toc_hyster_type = null;
            obc.toc_hyster_remark = null;
        }

        private void rdbYes_Oophorectomy_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbYes_Oophorectomy.Checked != true)
            {
                grpOop.Enabled = false;
                clearOophorectomy();
            }
            else { grpOop.Enabled = true; }
        }
        private void rdbNo_Oophorectomy_CheckedChanged(object sender, EventArgs e)
        {
            clearOophorectomy();
        }
        private void clearOophorectomy()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();
            obc.toc_oophorec_type = null;
            obc.toc_oophorec_remark = null;
        }        

        private void LoadDrowDownList()
        {
            List<ComboboxItem> newitem = new List<ComboboxItem>();
            for (int i = 0; i < 21; i++)
            {
                newitem.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }
            cmbGravida.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbGravida.DisplayMember = "Display";
            cmbGravida.ValueMember = "Valuedata";

            cmbLiChil.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbLiChil.DisplayMember = "Display";
            cmbLiChil.ValueMember = "Valuedata";

            cmbPara.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbPara.DisplayMember = "Display";
            cmbPara.ValueMember = "Valuedata";


            // morn edit showitem to 10
            List<ComboboxItem> itemAbor = new List<ComboboxItem>();
            for (int i = 1; i <= 10; i++)
            {
                itemAbor.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }
            cmbAbortion.DataSource = (from t1 in itemAbor select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbAbortion.DisplayMember = "Display";
            cmbAbortion.ValueMember = "Valuedata";
            // morn edit showitem to 5
            List<ComboboxItem> itemEcto = new List<ComboboxItem>();
            for (int i = 0; i <= 5; i++)
            {
                itemEcto.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }
            cmbEctopic.DataSource = (from t1 in itemEcto select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbEctopic.DisplayMember = "Display";
            cmbEctopic.ValueMember = "Valuedata";
            // morn

            cmbLast_mens_period.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbLast_mens_period.DisplayMember = "Display";
            cmbLast_mens_period.ValueMember = "Valuedata";


        }

        private void rdbNo_Homr_Rep_Ther_CheckedChanged(object sender, EventArgs e)
        {
            txtHomr_RepTher.Enabled = false;            
            txtHomr_RepTher_D_Mos.Enabled = false;
            txtHomr_RepTher_D_Yrs.Enabled = false;

            bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_homreplace_txt = null;
            bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_homreplace_dura_mos = null;
            bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_homreplace_dura_yrs = null;
        }
        private void rdbYes_Homr_Rep_Ther_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbYes_Homr_Rep_Ther.Checked == true)
            {
                txtHomr_RepTher.Enabled = true;
                txtHomr_RepTher_D_Mos.Enabled = true;
                txtHomr_RepTher_D_Yrs.Enabled = true;
            }
            else
            {
                txtHomr_RepTher.Enabled = false;
                txtHomr_RepTher_D_Mos.Enabled = false;
                txtHomr_RepTher_D_Yrs.Enabled = false;

                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_homreplace_txt = null;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_homreplace_dura_mos = null;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_homreplace_dura_yrs = null;
            }
        }

        private void rdbMarried_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMarried.Checked == true)
            { 
                txtMarried.Enabled = true;
                txtMarried.Focus();
            }
            else { txtMarried.Enabled = false; }
        }

        private void chkNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNormal.Checked == true)
            {
                txtNormal.Enabled = true;
                txtNormal.Focus();
            }
            else 
            { 
                txtNormal.Enabled = false;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_tdel_normal_txt = null;
            }
        }
        private void chkVacuum_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVacuum.Checked == true)
            {
                txtVacEx.Enabled = true;
                txtVacEx.Focus();
            }
            else 
            { 
                txtVacEx.Enabled = false;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_tdel_vacuum_txt = null;
            }
        }
        private void chkFE_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFE.Checked == true)
            {
                txtFE.Enabled = true;
                txtFE.Focus();
            }
            else 
            { 
                txtFE.Enabled = false;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_tdel_force_txt = null;
            }
        }
        private void chkCesa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCesa.Checked == true)
            {
                txtCesaSec.Enabled = true;
                txtCesaSec.Focus();
            }
            else 
            { 
                txtCesaSec.Enabled = false;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_tdel_cesa_txt = null;
            }
        }
        private void chkCSH_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCSH.Checked == true)
            {
                txtCSH.Enabled = true;
                txtCSH.Focus();
            }
            else 
            { 
                txtCSH.Enabled = false;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_tdel_cesahy_txt = null;
            }
        }
        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOther.Checked == true)
            {
                txtOther.Enabled = true;
                txtOther.Focus();
            }
            else 
            { 
                txtOther.Enabled = false;
                bsObstetricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_tdel_others_txt = null;
            }
        }

    }
}
