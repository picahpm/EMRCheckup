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
    public partial class UIProfileHorizontal : UserControl
    {
        public UIProfileHorizontal()
        {
            InitializeComponent();
        }
        Image DefaultImageProfile = null;
        private void UIProfileHorizontal_Load(object sender, EventArgs e)
        {
            DefaultImageProfile = pictureBox1.Image;
            ClearForm();
        }
        public void Loaddata()
        {
            int tprid = 0;
            int siteid = 0;

            if(Program.CurrentRegis!=null){
                tprid=Program.CurrentRegis.tpr_id;
            }

            if(Program.CurrentSite!=null){
                siteid=Program.CurrentSite.mhs_id;
            }
            
            ShowProfile(tprid, siteid);
        }
        public void Loaddata(int tprID, int siteID)
        {
            ShowProfile(tprID, siteID);
        }
        private void ShowProfile(int tpr_id,int site_id)
        {
            InhCheckupDataContext dbc = new InhCheckupDataContext();
                var objprofile = (from t1 in dbc.trn_patient_regis
                                  where t1.tpr_id == tpr_id //&& t1.mhs_id == site_id
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
                                      visitedate=t1.tpr_arrive_date
                                  }).FirstOrDefault();

                if (objprofile != null)
                {
                    dataHN.Text = objprofile.tpt_hn_no;
                    dataEN.Text = objprofile.tpr_en_no;
                    dataFullName.Text = objprofile.name;
                    datavisiteDate.Text = (objprofile.visitedate==null) ? "" : objprofile.visitedate.Value.ToString("dd / MM / yyyy");

                    lblvisittime.Text = (objprofile.visitedate == null) ? "" : objprofile.visitedate.Value.ToString("HH:mm:ss");

                    dataDOB.Text = Program.GetFormattedString(objprofile.tpt_dob.Value);
                    dataAge.Text = objprofile.age;

                    dataGender.Text = objprofile.Gender;
                    dataNationality.Text = objprofile.Nation;
                    dataAllergyLebel.Text = objprofile.tpt_allergy == null ? String.Empty : objprofile.tpt_allergy.Replace(Environment.NewLine, @",");

                    if (dataAllergyLebel.Text.Trim() == "")
                    {
                        dataAllergyLebel.Text = "No Know Allergy";
                    }

                    pictureBox1.Image = Program.byteArrayToImage(objprofile.tpt_image.ToArray());

                }
                else
                {
                    ClearForm();
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
            dataGender.Text = "";
            dataNationality.Text = "";
            lblvisittime.Text = "";
            dataHN.Text = "";
            dataEN.Text = "";
            dataFullName.Text = "";
            dataDOB.Text = "";
            dataAge.Text = "";
            datavisiteDate.Text = "";
            dataAllergyLebel.Text = "No Know Allergy";
            pictureBox1.Image = DefaultImageProfile;
        }

       

//******************************
    }
}
