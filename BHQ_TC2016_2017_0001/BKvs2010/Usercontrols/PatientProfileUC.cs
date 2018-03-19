using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class PatientProfileUC : UserControl
    {
        private string hn;

        private int? _tpr_id = null;
        public int? tpr_id
        {
            get
            {
                return _tpr_id;
            }
            set
            {
                _row_id = null;
                ClearProfile();
                if (value != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                        if (patient_regis != null)
                        {
                            trn_patient patient = patient_regis.trn_patient;
                            if (patient != null)
                            {
                                btnPatientAlertView.Visible = patient.trn_patient_alerts.Count() > 0;
                                hn = patient.tpt_hn_no;
                                pictureBox1.Image = patient.tpt_image != null ? Program.byteArrayToImage(patient.tpt_image) : BKvs2010.Properties.Resources.no_image;
                                dataNo.Text = patient_regis.tpr_queue_no;
                                datavipLevel.Text = patient_regis.tpr_vip_desc;
                                lblsite.Text = patient_regis.mst_hpc_site.mhs_ename;
                                //lblsite.Text = cdc.mst_hpc_sites.Where(x => x.mhs_id == patient_regis.mhs_id).Select(x => x.mhs_ename).FirstOrDefault();
                                lblvip.Visible = patient.tpt_vip_hpc == true ? true : false;

                                dataHN.Text = patient.tpt_hn_no;
                                dataEN.Text = patient_regis.tpr_en_no;
                                dataFullName.Text = patient.tpt_othername;
                                dataGender.Text = patient.tpt_gender == 'M' ? "ชาย(Male)" : "หญิง(Female)";
                                dataDOB.Text = patient.tpt_dob_text; // Program.GetFormattedString(patient.tpt_dob.Value);
                                dataAge.Text = Program.CalculateAge(patient.tpt_dob.Value.Date, Program.GetServerDateTime().Date);
                                lbdataNation.Text = patient.tpt_nation_desc;
                                lbdataAlergy.Text = string.IsNullOrEmpty(patient.tpt_allergy) ? "No Know Allergy" : patient.tpt_allergy;

                                //if (patient.trn_patient_alerts != null && patient.trn_patient_alerts.Count() > 0)
                                //{
                                //    //string patient_alert = Program.GetPartiendAlert(patient.trn_patient_alerts.ToList());
                                //    //lbdataPatientAlert.Text = patient_alert;
                                //    btnPatientAlertView.Visible = true;
                                //}
                                //else
                                //{
                                //    btnPatientAlertView.Visible = false;
                                //}
                            }
                        }
                        else
                        {
                            btnPatientAlertView.Visible = false;
                        }
                    }
                }
                _tpr_id = value;
            }
        }

        private int? _row_id = null;
        public int? row_id
        {
            get
            {
                return _row_id;
            }
            set
            {
                _tpr_id = null;
                ClearProfile();
                if (value != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        tmp_getptarrived tmp_arrived = cdc.tmp_getptarriveds.Where(x => x.row_id == value).FirstOrDefault();
                        if (tmp_arrived != null)
                        {
                            hn = tmp_arrived.papmi_no;
                            pictureBox1.Image = tmp_arrived.paper_photo != null ? Program.byteArrayToImage(tmp_arrived.paper_photo) : BKvs2010.Properties.Resources.no_image;
                            dataNo.Text = "";
                            //datavipLevel.Text = patient_regis.tpr_vip_desc;
                            mst_hpc_site hpc_site = new EmrClass.GetDataMasterCls().GetMstHpcSite(tmp_arrived.ctloc_code);
                            lblvip.Visible = false;
                            if (hpc_site != null)
                            {
                                lblsite.Text = hpc_site.mhs_ename;
                                if (hpc_site.mhs_patient_vip == true)
                                {
                                    lblvip.Visible = true;
                                }
                            }
                            dataHN.Text = tmp_arrived.papmi_no;
                            dataEN.Text = tmp_arrived.paadm_admno;
                            dataFullName.Text = tmp_arrived.ttl_desc + tmp_arrived.papmi_name + " " + tmp_arrived.papmi_name2;
                            char gender = Convert.ToChar(tmp_arrived.ctsex_code);
                            dataGender.Text = gender == 'M' ? "ชาย(Male)" : "หญิง(Female)";
                            dataDOB.Text = Program.GetFormattedString(tmp_arrived.papmi_dob.Value);
                            dataAge.Text = Program.CalculateAge(tmp_arrived.papmi_dob.Value.Date, Program.GetServerDateTime().Date);
                            lbdataNation.Text = tmp_arrived.ctnat_desc;
                            lbdataAlergy.Text = string.IsNullOrEmpty(tmp_arrived.allergy_eng) ? "No Know Allergy" : tmp_arrived.allergy_eng;
                            btnPatientAlertView.Visible = false;
                        }
                    }
                }
                _row_id = value;
            }
        }

        public PatientProfileUC()
        {
            InitializeComponent();
        }

        private void btnPatientAlertView_Click(object sender, EventArgs e)
        {
            //frmAlertPatient frm = new frmAlertPatient();
            //frm.SetHNno = hn;
            //frm.ShowDialog();
        }

        private void lbRetrieve_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (_tpr_id != null)
            {
                bool getInfo = new APITrakcare.GetPatientInfoCls().GetInfo((int)_tpr_id);
                if (getInfo)
                {
                    tpr_id = _tpr_id;
                }
            }
            this.Cursor = Cursors.Default;
        }

        public void Clear()
        {
            tpr_id = null;
            row_id = null;
        }

        private void ClearProfile()
        {
            hn = "";
            pictureBox1.Image = BKvs2010.Properties.Resources.no_image;
            dataNo.Text = "";
            datavipLevel.Text = "";
            lblsite.Text = "";
            lblvip.Visible = false;
            dataHN.Text = "";
            dataEN.Text = "";
            dataFullName.Text = "";
            dataGender.Text = "";
            dataDOB.Text = "";
            dataAge.Text = "";
            lbdataNation.Text = "";
            lbdataAlergy.Text = "";
            btnPatientAlertView.Visible = false;
        }
    }
}
