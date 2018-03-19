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
    public partial class UIUserprofile : UserControl
    {
        private int? _tpr_id;
        public int? tpr_id
        {
            get
            {
                return _tpr_id;
            }
        }
        public UIUserprofile()
        {
            InitializeComponent();
            this.BackColor = Color.LightCyan;
        }

        Image DefaultImageProfile = null;
        private string HNno = "";
        private void UIUserprofile_Load(object sender, EventArgs e)
        {
            DefaultImageProfile = pictureBox1.Image;
        }
        public void LoadData(int tpr_id)
        {
            ShowProfile(tpr_id);
            uiBasicMeasurement1.LoadData(tpr_id);
            _tpr_id = tpr_id;
        }
        public void LoadData()
        {
            if (Program.CurrentRegis != null)
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                ShowProfile(tpr_id);
                uiBasicMeasurement1.LoadData(tpr_id);
                _tpr_id = tpr_id;
            }
            else
            {
                ClearForm();
                uiBasicMeasurement1.LoadData();
                _tpr_id = null;
            }
        }
        private void ShowProfile(int tpr_id)
        {
            try
            {
                InhCheckupDataContext dbc = new InhCheckupDataContext();

                var objprofile = (from t1 in dbc.trn_patient_regis
                                  where t1.tpr_id == tpr_id
                                  select new
                                  {
                                      t1.tpr_queue_no,
                                      t1.tpr_queue_type,
                                      Nation = t1.trn_patient.tpt_nation_desc,
                                      t1.tpr_en_no,
                                      t1.tpr_vip_desc,
                                      t1.trn_patient.tpt_image,
                                      t1.trn_patient.tpt_hn_no,
                                      name = t1.trn_patient.tpt_othername,
                                      Gender = t1.trn_patient.tpt_gender == 'M' ? "ชาย(Male)" : "หญิง(Female)",
                                      t1.trn_patient.tpt_dob,
                                      t1.trn_patient.tpt_allergy,
                                      age = Program.CalculateAge(t1.trn_patient.tpt_dob.Value, Program.GetServerDateTime()),
                                      PartientAlert = Program.GetPartiendAlert(t1.trn_patient.trn_patient_alerts.ToList()),
                                      t1.trn_patient.tpt_vip_hpc,
                                      site = (from t in dbc.mst_hpc_sites where t.mhs_id == t1.mhs_id select t.mhs_ename).FirstOrDefault(),
                                      visibleAlert = t1.trn_patient.trn_patient_alerts.Count() > 0
                                  }).FirstOrDefault();

                if (objprofile != null)
                {
                    HNno = objprofile.tpt_hn_no;
                    dataNo.Text = string.Format("No. {0}", objprofile.tpr_queue_no);
                    datavipLevel.Text = objprofile.tpr_vip_desc; //objprofile.tpr_queue_type == null ? "" : GetQueueType(objprofile.tpr_queue_type.Value.ToString());
                    dataHN.Text = objprofile.tpt_hn_no;
                    dataEN.Text = objprofile.tpr_en_no;
                    dataFullName.Text = objprofile.name;
                    dataGender.Text = objprofile.Gender;
                    dataDOB.Text = Program.GetFormattedString(objprofile.tpt_dob.Value);
                    dataAge.Text = objprofile.age;
                    lbdataAlergy.Text = objprofile.tpt_allergy;
                    lbdataNation.Text = objprofile.Nation;
                    lblsite.Text = objprofile.site;
                    if (lbdataAlergy.Text.Trim() == "")
                    {
                        lbdataAlergy.Text = "No Know Allergy";
                    }

                    btnPatientAlertView.Visible = false;
                    if (objprofile.PartientAlert.Length > 0)
                    {
                        lbdataPatientAlert.Text = objprofile.PartientAlert;
                        btnPatientAlertView.Visible = true;
                    }
                    ////Added.Akkaradech on 2013-12-24
                    if (objprofile.tpt_vip_hpc == true)
                    {
                        lblvip.Visible = true;
                    }
                    if (objprofile.tpt_vip_hpc == false)
                    {
                        lblvip.Visible = false;
                    }
                    ////EndAdded.Akkaradech on 2013-12-24
                    if (objprofile.tpt_image != null)
                    {
                        pictureBox1.Image = Program.byteArrayToImage(objprofile.tpt_image.ToArray());
                    }
                    else
                    {
                        pictureBox1.Image = DefaultImageProfile;
                    }
                    btnPatientAlertView.Visible = objprofile.visibleAlert;
                }
                else
                {
                    HNno = "";
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "ShowProfile", ex, false);
            }
        }
        private string GetQueueType(string strType)
        {
            string ret = "";
            switch (strType)
            {
                case "1": ret = "VVIP"; break;
                case "2": ret = "VIP Appointment"; break;
                case "3": ret = "VIP Walk-in"; break;
                case "4": ret = "Appointment"; break;
                case "5": ret = "Walk-in"; break;
            }
            return ret;
        }
        public void ClearForm()
        {
            dataNo.Text = string.Format("No. {0}", "");
            datavipLevel.Text = "";
            dataHN.Text = "";
            dataEN.Text = "";
            dataFullName.Text = "";
            dataGender.Text = "";
            dataDOB.Text = "";
            dataAge.Text = "";
            lbdataAlergy.Text = "";
            lbdataNation.Text = "";
            if (lbdataAlergy.Text.Trim() == "")
            {
                lbdataAlergy.Text = "No Know Allergy";
            }
            lblvip.Text = "";
            lblsite.Text = "";
            lbdataPatientAlert.Text = "";
            btnPatientAlertView.Visible = false;
            pictureBox1.Image = DefaultImageProfile;
            uiBasicMeasurement1.LoadData();
        }

        private void btnPatientAlertView_Click(object sender, EventArgs e)
        {
            frmAlertPatient frm = new frmAlertPatient();
            frm.SetHNno = this.HNno;
            frm.ShowDialog();
        }

        private void lbRetrieve_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (_tpr_id != null)
            {
                bool getInfo = new APITrakcare.GetPatientInfoCls().GetInfo((int)_tpr_id);
                if (getInfo)
                {
                    LoadData((int)_tpr_id);
                }
            }
            this.Cursor = Cursors.Default;
        }
    }

}