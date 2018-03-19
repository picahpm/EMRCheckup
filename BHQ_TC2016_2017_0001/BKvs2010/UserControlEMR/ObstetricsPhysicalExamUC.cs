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
    public partial class ObstetricsPhysicalExamUC : UserControl
    {
        public ObstetricsPhysicalExamUC()
        {
            InitializeComponent();
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

                        //CallWebservice();

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

        }

        private void clearYolva()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();

            obc.toc_vulva_vulvitis = null;
            obc.toc_vulva_folliculitis = null;
            obc.toc_vulva_herpes = null;
            obc.toc_vulva_cyst = null;
            obc.toc_vulva_cyst_unit = null;
            obc.toc_vulva_mass = null;
            obc.toc_vulva_mass_unit = null;
            obc.toc_vulva_others = null;
            obc.toc_vulva_others_remark = null;
        }
        private void clearUrethra()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();

            obc.toc_uret_urethrit = null;
            obc.toc_uret_condyloma = null;
            obc.toc_uret_others = null;
            obc.toc_uret_others_remark = null;
        }
        private void clearBartholin()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();

            obc.toc_bart_barthol = null;
            obc.toc_bart_barthol_absc = null;
            obc.toc_bart_barthol_cyst = null;
            obc.toc_bart_barthol_cyst_unit = null;
        }
        private void clearVaginalMucosa()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();

            obc.toc_vamuc_imflam = null;
            obc.toc_vamuc_thindry = null;
        }
        private void clearCervix()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();

            obc.toc_cerv_erosion = null;
            obc.toc_cerv_ulceration = null;
            obc.toc_cerv_mass = null;
            obc.toc_cerv_mass_unit = null;
            obc.toc_cerv_polyp = null;
            obc.toc_cerv_polyp_unit = null;
            obc.toc_cerv_cyst = null;
            obc.toc_cerv_cyst_unit = null;
        }
        private void clearVaginalDischarge()
        {
            trn_obstetric_chief obc = _PatientRegis.trn_obstetric_chiefs.FirstOrDefault();

            obc.toc_vadis_color = null;
        }

        private void rdbNormal_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            grpYolva.Enabled = false;
            clearYolva();
        }
        private void rdbAbnormal_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Yolva.Checked == true)
            { grpYolva.Enabled = true; }
            else
            {
                grpYolva.Enabled = false;
                clearYolva();
            }
        }

        private void rdbNormal_Urethar_CheckedChanged(object sender, EventArgs e)
        {
            grpUrethar.Enabled = false;
            clearUrethra();
        }
        private void rdbrdbAbnormal_Urethar_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbrdbAbnormal_Urethar.Checked == true)
            { grpUrethar.Enabled = true; }
            else
            {
                grpUrethar.Enabled = false;
                clearUrethra();
            }
        }

        private void rdbNormal_Bartholin_CheckedChanged(object sender, EventArgs e)
        {
            grpBartholin.Enabled = false;
            clearBartholin();
        }
        private void rdbAbnormal_Bartholin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Bartholin.Checked == true)
            { grpBartholin.Enabled = true; }
            else
            {
                grpBartholin.Enabled = false;
                clearBartholin();
            }
        }

        private void rdbNormal_Yag_muc_CheckedChanged(object sender, EventArgs e)
        {
            grpVaginalMucosa.Enabled = false;
            clearVaginalMucosa();
        }
        private void rdbAbnormal_Yag_muc_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Yag_muc.Checked == true)
            { grpVaginalMucosa.Enabled = true; }
            else
            {
                grpVaginalMucosa.Enabled = false;
                clearVaginalMucosa();
            }
        }

        private void rdbNormal_Yag_dis_CheckedChanged(object sender, EventArgs e)
        {
            pnlYa_dis_Color.Enabled = false;
            clearVaginalDischarge();
        }
        private void rdbAbnormal_Yag_dis_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Yag_dis.Checked == true)
            { pnlYa_dis_Color.Enabled = true; }
            else
            {
                pnlYa_dis_Color.Enabled = false;
                clearVaginalDischarge();
            }
        }

        private void rdbNormal_Cervix_CheckedChanged(object sender, EventArgs e)
        {
            grpCervix.Enabled = false;
            clearCervix();
        }
        private void rdbAbnormal_Cervix_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Cervix.Checked == true)
            { grpCervix.Enabled = true; }
            else
            {
                grpCervix.Enabled = false;
                clearCervix();
            }
        }

        private void CallWebservice() //grid view diagnosis
        {
            if (Program.CurrentRegis != null)
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    {
                        int tprid = Program.CurrentRegis.tpr_id;
                        trn_patient_regi currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1).FirstOrDefault();
                        if (currentRegis != null)
                        {
                            string HNno = currentRegis.trn_patient.tpt_hn_no;
                            string ENno = currentRegis.tpr_en_no;
                            string Doctorcode = Program.CurrentUser.mut_username;

                            var currentobsChief = (trn_obstetric_chief)bsObstestricChief.Current;


                            DataTable dt = ws.GetDiagnosisByDoctor("01-92-006363", "O01-12-876565", "001915668");//(HNno, ENno, Doctorcode); //
                            gvDiagnosis.AutoGenerateColumns = false;
                            foreach (DataRow dr in dt.Rows)
                            {
                                bsObstetricDiag.AddNew();
                                var pa = (trn_obstetric_diag)bsObstetricDiag.Current;
                                pa.toc_id = currentobsChief.toc_id;
                                pa.obg_diag_code = dr["MRCID_Code"].ToString();
                                pa.obg_diag_desc = dr["MRCID_Desc"].ToString();
                                pa.obg_diag_type = dr["DTYP_Desc"].ToString();
                                pa.obg_diag_date = Convert.ToDateTime(dr["MRDIA_Date"]);
                                pa.obg_create_by = Program.CurrentUser.mut_username;
                                pa.obg_update_by = pa.obg_create_by;
                                pa.obg_create_date = Program.GetServerDateTime();
                                pa.obg_update_date = pa.obg_create_date;
                            }
                            //gvDiagnosis.DataSource = dt;

                            //TakeCare 2016
                            //DataTable dt2 = ws.GetMedicineByDoctor("01-92-006363", "O01-12-876565", "001915668");//(HNno, ENno, Doctorcode);//
                            //gvMedicationTreatment.AutoGenerateColumns = false;
                            //foreach (DataRow dr in dt2.Rows)
                            //{
                            //    bsObstetricMed.AddNew();
                            //    var pa = (trn_obstetric_med)bsObstetricMed.Current;
                            //    pa.toc_id = currentobsChief.toc_id;
                            //    pa.obm_med_code = dr["ARCIM_Code"].ToString();
                            //    pa.obm_med_desc = dr["ARCIM_Desc"].ToString();
                            //    pa.obm_desc = dr["PHCFR_Desc1"].ToString();
                            //    pa.obm_qty = dr["OEORI_DoseQty"].ToString();
                            //    pa.obm_unit = dr["CTUOM_Desc2"].ToString();
                            //    pa.obm_eat_unit = dr["PHCIN_Desc1"].ToString();
                            //    pa.obm_create_by = Program.CurrentUser.mut_username;
                            //    pa.obm_update_by = pa.obm_create_by;
                            //    pa.obm_create_date = Program.GetServerDateTime();
                            //    pa.obm_update_date = pa.obm_create_date;
                            //}
                            // gvMedicationTreatment.DataSource = dt2;
                        }
                    }
                }
            }
        }

        private void chkCyst_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCyst_Yolva.Checked == true)
            {
                this.txtCyst_Yolva.Enabled = true;
                txtCyst_Yolva.Focus();
            }
            else
            {
                this.txtCyst_Yolva.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_vulva_cyst_unit = null;
            }
        }
        private void chkMass_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkMass_Yolva.Checked == true)
            {
                this.txtMass_Yolva.Enabled = true;
                txtMass_Yolva.Focus();
            }
            else
            {
                this.txtMass_Yolva.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_vulva_mass_unit = null;
            }
        }
        private void chkOther_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOther_Yolva.Checked == true)
            {
                this.txtOther_Yolva.Enabled = true;
                txtOther_Yolva.Focus();
            }
            else
            {
                this.txtOther_Yolva.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_vulva_others_remark = null;
            }
        }
        private void chkOther_Urethar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOther_Urethar.Checked == true)
            {
                this.txtOther_Urethar.Enabled = true;
                txtOther_Urethar.Focus();
            }
            else
            {
                this.txtOther_Urethar.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_uret_others_remark = null;
            }
        }
        private void chkBartholn_cyst_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBartholn_cyst.Checked == true)
            {
                this.txtBartholin.Enabled = true;
                txtBartholin.Focus();
            }
            else
            {
                this.txtBartholin.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_bart_barthol_cyst_unit = null;
            }
        }
        private void chkMass_Cervix_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkMass_Cervix.Checked == true)
            {
                this.txtMass_Cervix.Enabled = true;
                txtMass_Cervix.Focus();
            }
            else
            {
                this.txtMass_Cervix.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_cerv_mass_unit = null;
            }
        }
        private void chkPolyp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPolyp.Checked == true)
            {
                this.txtPoly.Enabled = true;
                txtPoly.Focus();
            }
            else
            {
                this.txtPoly.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_cerv_polyp_unit = null;
            }
        }
        private void chkCyst_Cervix_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCyst_Cervix.Checked == true)
            {
                this.txtCyst_Cervix.Enabled = true;
                txtCyst_Cervix.Focus();
            }
            else
            {
                this.txtCyst_Cervix.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_cerv_cyst_unit = null;
            }
        }
                
        private void rdbNormal_Left_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNo_Tender_Left_Adexa.Checked == true)
            {
                grpAdexaLeftTender.Enabled = false;
                grpAdexaLeftMass.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_ltender = null;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_lmass = null;
            }
        }
        private void rdbAbnormal_Left_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Left_Adexa.Checked == true)
            {
                grpAdexaLeftTender.Enabled = true;
                grpAdexaLeftMass.Enabled = true;
            }
            else
            {
                grpAdexaLeftTender.Enabled = false;
                grpAdexaLeftMass.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_ltender = null;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_lmass = null;
            }
        }
        private void rdbNormal_Right_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNormal_Right_Adexa.Checked == true)
            {
                grpAdexaRightTender.Enabled = false;
                grpAdexaRightMass.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_rtender = null;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_rmass = null;
            }
        }
        private void rdbAbnormal_Right_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Right_Adexa.Checked == true)
            {
                grpAdexaRightTender.Enabled = true;
                grpAdexaRightMass.Enabled = true;
            }
            else
            {
                grpAdexaRightTender.Enabled = false;
                grpAdexaRightMass.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_rtender = null;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_rmass = null;
            }
        }
        private void rdbSize_Mass_Right_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSize_Mass_Right_Adexa.Checked == true)
            {
                txtSize_Mass_Right_Adexa.Enabled = true;
                txtSize_Mass_Right_Adexa.Focus();
            }
            else
            {
                txtSize_Mass_Right_Adexa.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_rmass_unit = null;
            }
        }
        private void rdbSize_Mass_Left_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSize_Mass_Left_Adexa.Checked == true)
            {
                txtSize_Mass_Left_Adexa.Enabled = true;
                txtSize_Mass_Left_Adexa.Focus();
            }
            else
            {
                txtSize_Mass_Left_Adexa.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_adexa_lmass_unit = null;
            }
        }

        private void rdbAbnormal_Ulterus_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbAbnormal_Ulterus.Checked == true)
            {
                txtEnlarged_size.Enabled = true;
                txtEnlarged_size.Focus();
            }
            else
            {
                txtEnlarged_size.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_uter_sz_enlarge = null;
            }
        }

        private void rdbOther_Cul_de_sac_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbOther_Cul_de_sac.Checked == true)
            {
                txtOther_Cul_de_sac.Enabled = true;
                txtOther_Cul_de_sac.Focus();
            }
            else
            {
                txtOther_Cul_de_sac.Enabled = false;
                bsObstestricChief.OfType<trn_obstetric_chief>().FirstOrDefault().toc_culd_others = null;
            }
        }        
    }
}
