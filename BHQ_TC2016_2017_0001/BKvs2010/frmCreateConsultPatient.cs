using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmCreateConsultPatient : Form
    {
        string returnHN = null;
        string _hn_no = null;
        string hn_no
        {
            get
            {
                return _hn_no;
            }
            set
            {
                if (value != null)
                {
                    tmp_getptarrived tga = getArrivedByHn(value);
                    picPatient.Image = Program.byteArrayToImage(tga.paper_photo);
                    lbHN.Text = tga.papmi_no;
                    lbGender.Text = tga.ctsex_code == "M" ? "Male" : "Female";
                    lbEN.Text = tga.paadm_admno;
                    lbdataAlergy.Text = tga.allergy_eng;
                    datavipLevel.Text = tga.penstype_code == null ? null : "V.I.P.";
                    try
                    {
                        DateTime dob = Convert.ToDateTime(tga.papmi_dob);
                        lbDOB.Text = dob.ToString("dd/MM/yyyy");
                        lbAge.Text = Program.CalculateAge(dob, DateTime.Today);
                    }
                    catch
                    {
                        lbDOB.Text = null;
                    }
                    lbName.Text = tga.ttl_desc + tga.papmi_name + " " + tga.papmi_name2;
                    lbNation.Text = tga.ctnat_desc;
                    btnContinue.Enabled = true;
                }
                else
                {
                    picPatient.Image = null;
                    lbHN.Text = null;
                    lbGender.Text = null;
                    lbEN.Text = null;
                    lbdataAlergy.Text = null;
                    datavipLevel.Text = null;
                    lbDOB.Text = null;
                    lbAge.Text = null;
                    lbName.Text = null;
                    lbNation.Text = null;
                    btnContinue.Enabled = false;
                }
                _hn_no = value;
            }
        }

        tmp_getptarrived _currentGetArrived;
        tmp_getptarrived currentGetArrived
        {
            get
            {
                return _currentGetArrived;
            }
            set
            {
                if (value != null)
                {
                    picPatient.Image = Program.byteArrayToImage(value.paper_photo);
                    lbHN.Text = value.papmi_no;
                    lbGender.Text = value.ctsex_code == "M" ? "Male" : "Female";
                    lbEN.Text = value.paadm_admno;
                    lbdataAlergy.Text = value.allergy_eng;
                    datavipLevel.Text = value.penstype_code == null ? null : "V.I.P.";
                    try
                    {
                        DateTime dob = Convert.ToDateTime(value.papmi_dob);
                        lbDOB.Text = dob.ToString("dd/MM/yyyy");
                        lbAge.Text = Program.CalculateAge(dob, DateTime.Today);
                    }
                    catch
                    {
                        lbDOB.Text = null;
                    }
                    lbName.Text = value.ttl_desc + value.papmi_name + " " + value.papmi_name2;
                    lbNation.Text = value.ctnat_desc;
                    btnContinue.Enabled = true;
                }
                else
                {
                    picPatient.Image = null;
                    lbHN.Text = null;
                    lbGender.Text = null;
                    lbEN.Text = null;
                    lbdataAlergy.Text = null;
                    datavipLevel.Text = null;
                    lbDOB.Text = null;
                    lbAge.Text = null;
                    lbName.Text = null;
                    lbNation.Text = null;
                    btnContinue.Enabled = false;
                }
                _currentGetArrived = value;
            }
        }

        public frmCreateConsultPatient()
        {
            InitializeComponent();
        }

        public string getHNCreateConsult()
        {
            this.ShowDialog();
            return returnHN;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            try
            {
                btnContinue.Enabled = false;
                btnSearch.Enabled = false;
                btnCancel.Enabled = false;
                lbAlert.Text = "Processing...";
                Application.DoEvents();

                //tmp_getptarrived result = getArrivedByHn(hn_no);
                if (currentGetArrived != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        //tmp_getptarrived tga = cdc.tmp_getptarriveds.Where(x => x.row_id == result.row_id).FirstOrDefault();
                        //tga.flag_success = 'Y';
                        DateTime dateNow = Program.GetServerDateTime();

                        List<tmp_getptarrived> tmpArrived = cdc.tmp_getptarriveds.Where(x => x.papmi_no == currentGetArrived.papmi_no).ToList(); // && x.paadm_admdate.Value.Date == dateNow.Date
                        if (tmpArrived.Where(x => x.flag_success == 'Y').Count() == 0)
                        {
                            tmpArrived.ForEach(x => x.flag_success = 'Y');
                            cdc.tmp_getptarriveds.InsertOnSubmit(currentGetArrived);
                            EmrClass.GetDataFromWSTrakCare cls = new EmrClass.GetDataFromWSTrakCare();
                            trn_patient tpt = cdc.trn_patients.Where(x => x.tpt_hn_no == currentGetArrived.papmi_no).FirstOrDefault();
                            bool flagNewPatient = true;
                            if (tpt != null) flagNewPatient = false;
                            cls.otherClinicSkipToCheckB(currentGetArrived, ref tpt);
                            if (flagNewPatient == true) cdc.trn_patients.InsertOnSubmit(tpt);
                            cdc.SubmitChanges();
                            trn_patient_regi tpr = tpt.trn_patient_regis.OrderByDescending(x => x.tpr_create_date).FirstOrDefault();
                            trn_RefreshLabHistory refreshHis = new trn_RefreshLabHistory()
                            {
                                tpr_id = tpr.tpr_id,
                                CreateDate = dateNow,
                                HN_no = tpt.tpt_hn_no,
                                status = false
                            };
                            cdc.trn_RefreshLabHistories.InsertOnSubmit(refreshHis);
                            cdc.SubmitChanges();
                            new EmrClass.GetPTPackageCls().setRelationOrderSet(ref tpr);
                            cdc.SubmitChanges();
                            btnContinue.Enabled = true;
                            btnSearch.Enabled = true;
                            btnCancel.Enabled = true;
                            returnHN = currentGetArrived.papmi_no;
                            this.Close();
                        }
                        else
                        {

                        }
                    }
                }
            }
            catch
            {

            }
        }

        private tmp_getptarrived getArrivedByHn(string hn)
        {
            lbAlert.Text = "Searching...";
            btnCancel.Enabled = false;
            btnContinue.Enabled = false;
            btnSearch.Enabled = false;
            Application.DoEvents();

            string tmpHN = hn.Replace("-", "");

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            //using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            //{
            //    DateTime dateNow = Program.GetServerDateTime();
            //    List<vw_tmp_arrive> result = cdc.vw_tmp_arrives
            //                                    .Where(x => (x.papmi_no == tmpHN || x.papmi_no.Replace("-", "") == tmpHN) && 
            //                                                x.paadm_admdate.Value.Date == dateNow.Date).ToList();
            //    if (result.Count > 0)
            //    {
            //        result = result.Where(x => x.flag_success == 'N' || x.flag_success == null).ToList();
            //        if (result.Count > 0)
            //        {
            //            int row_id = result.Select(x => x.row_id).FirstOrDefault();
            //            return get_tgaByRowID(row_id);
            //        }
            //        else
            //        {
            //            lbAlert.Text = "This HN No. is processing on EMR-checkup.";
            //        }
            //    }
            //    else
            //    {
            //        lbAlert.Text = "This HN No. is not found.";
            //    }
            //    return null;
            //}
            try
            {
                string currentSite = null;
                string currentSiteName = null;
                if (Program.CurrentSite != null)
                {
                    currentSite = Program.CurrentSite.mhs_code;
                    mst_hpc_site mhs = Program.getMstHpcSiteByMshCode(currentSite);
                    currentSiteName = mhs != null ? mhs.mhs_ename : null;
                }
                if (currentSite != null)
                {
                    DateTime dateTime = Program.GetServerDateTime();
                    string hn_pos1 = tmpHN.Length >= 2 ? tmpHN.Substring(0, 2) : "";
                    string hn_pos2 = tmpHN.Length >= 4 ? tmpHN.Substring(2, 2) : "";
                    string hn_pos3 = tmpHN.Length >= 5 ? tmpHN.Substring(4) : "";

                    string realHN = hn_pos1 + "-" + hn_pos2 + "-" + hn_pos3;

                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        List<tmp_getptarrived> tmpArrived = cdc.tmp_getptarriveds.Where(x => x.papmi_no == realHN && x.paadm_admdate.Value.Date == dateTime.Date)
                                                               .ToList();
                        if (tmpArrived.Count > 0)
                        {
                            if (tmpArrived.Where(x => x.flag_success == 'Y').Count() != 0)
                            {
                                lbAlert.Text = "This HN No. is processing on EMR-checkup.";
                                return null;
                            }
                        }
                        EmrClass.GetDataFromWSTrakCare cls = new EmrClass.GetDataFromWSTrakCare();
                        using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                        {
                            string dateNow = Program.GetServerDateString();
                            string timeNow = string.Format(DateTime.Now.ToString("{0}HH{1}mm{2}ss{3}"), "PT", "H", "M", "S");
                            tmp_getptarrived tga = ws.GetPTOrderByLocDateHn(currentSite, dateNow, realHN).AsEnumerable()
                                                     .Select(x => new tmp_getptarrived
                                                     {
                                                         paadm_type_of_patient_calc = x.Field<string>("PAADM_Type_of_Patient_Calc"),
                                                         paadm_rowid = x.Field<int?>("PAADM_RowID").ToString(),
                                                         appt_rowid = x.Field<string>("APPT_RowId"),
                                                         appt_arrivaltime = timeNow,
                                                         paadm_admdate = x.Field<DateTime?>("PAADM_AdmDate"),
                                                         allergy_eng = new APITrakcare.GetAllergyCls().GetByHN(x.Field<string>("PAPMI_No")),
                                                         paadm_admno = x.Field<string>("PAADM_ADMNo"),
                                                         papmi_no = x.Field<string>("PAPMI_No"),
                                                         ttl_desc = x.Field<string>("TTL_Desc"),
                                                         papmi_name = x.Field<string>("PAPMI_Name"),
                                                         papmi_name2 = x.Field<string>("PAPMI_Name2"),
                                                         appt_transdate = x.Field<DateTime?>("APPT_TransDate"),
                                                         appt_datesearch = x.Field<DateTime?>("APPT_DateSearch"),
                                                         paadm_admtime = x.Field<TimeSpan?>("PAADM_admTime").ToString(),
                                                         ctloc_code = cls.returnMainSite(currentSite),
                                                         ctloc_desc = currentSiteName,
                                                         penstype_code = x.Field<string>("PENSTYPE_Code"),
                                                         penstype_desc = x.Field<string>("PENSTYPE_Desc"),
                                                         ser_rowid = x.Field<int?>("SER_RowId"),
                                                         ser_desc = x.Field<string>("SER_Desc"),
                                                         ctnat_code = x.Field<string>("CTNAT_Code"),
                                                         ctnat_desc = x.Field<string>("CTNAT_Desc"),
                                                         ctsex_code = x.Field<string>("CTSEX_Code"),
                                                         ctsex_desc = x.Field<string>("CTSEX_Desc"),
                                                         papmi_dob = x.Field<DateTime?>("PAPMI_DOB"),
                                                         paper_photo = new EmrClass.GetPatientImage().getByPath(x.Field<string>("PAPER_Photo")),
                                                         paper_ageyr = x.Field<string>("PAPER_AgeYr"),
                                                         paper_agemth = x.Field<string>("PAPER_AgeMth"),
                                                         paper_ageday = x.Field<string>("PAPER_AgeDay"),
                                                         paper_stname = x.Field<string>("PAPER_StName"),
                                                         citarea_desc = x.Field<string>("CITAREA_Desc"),
                                                         prov_desc = x.Field<string>("PROV_Desc"),
                                                         ctcit_desc = x.Field<string>("CTCIT_Desc"),
                                                         ctzip_code = x.Field<string>("CTZIP_Code"),
                                                         paper_id = x.Field<string>("PAPER_ID"),
                                                         paper_telo = x.Field<string>("PAPER_TelO"),
                                                         paper_telh = x.Field<string>("PAPER_TelH"),
                                                         paper_mobphone = x.Field<string>("PAPER_MobPhone"),
                                                         paper_email = x.Field<string>("PAPER_Email"),

                                                         ctmar_desc = x.Field<string>("CTMAR_Desc"),
                                                         paper_name5 = x.Field<string>("PAPER_Name5"),
                                                         paper_name6 = x.Field<string>("PAPER_Name6"),
                                                         paper_name7 = x.Field<string>("PAPER_Name7"),
                                                         flag_success = 'Y',

                                                         LocWhenOrdOther = cls.returnMainSite(x.Field<string>("LocWhenOrdOther")),
                                                         //LocWhenOrdCheckup = x.Field<string>("LocWhenOrdCheckup"),
                                                         //LocWhenOrdConsult = cls.returnMainSite(x.Field<string>("LocWhenOrdConsult")),
                                                         LocWhenOrdOtherDesc = x.Field<string>("LocWhenOrdOtherDesc"),
                                                     }).FirstOrDefault();

                            //btnCancel.Enabled = true;
                            //btnContinue.Enabled = true;
                            //btnSearch.Enabled = true;
                            if (tga == null)
                            {
                                lbAlert.Text = "This HN No. is not found.";
                                return null;
                            }
                            else
                            {
                                //if (tga.paper_photo == null)
                                //{
                                //    MemoryStream ms = new MemoryStream();
                                //    BKvs2010.Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                //    tga.paper_photo = ms.ToArray();
                                //}
                                //tmpArrived.ForEach(x => x.flag_success = 'Y');
                                //cdc.SubmitChanges();
                                lbAlert.Text = "";
                                return tga;
                            }
                        }
                    }
                }
            }
            catch
            {
                lbAlert.Text = "This process is Error.";
            }
            finally
            {
                btnCancel.Enabled = true;
                btnContinue.Enabled = true;
                btnSearch.Enabled = true;
            }
            return null;
        }

        private tmp_getptarrived get_tgaByRowID(int row_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                tmp_getptarrived result = cdc.tmp_getptarriveds.Where(x => x.row_id == row_id).FirstOrDefault();
                return result;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string hn = null;
            if (txtSearch.Text != null)
            {
                hn = txtSearch.Text.Trim();
            }
            currentGetArrived = getArrivedByHn(hn);
            //if (currentGetArrived != null)
            //{
            //    hn_no = currentGetArrived.papmi_no;
            //}
            //else
            //{
            //    hn_no = null;
            //}
        }
    }
}
