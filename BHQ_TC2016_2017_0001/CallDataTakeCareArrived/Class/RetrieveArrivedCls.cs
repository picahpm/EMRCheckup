using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using DBCheckup;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace CallDataTakeCareArrived.Class
{
    public class RetrieveArrivedCls
    {
        public void processAll(string SourcePath, string LocalPath, string LocalPathSuccess, string LocalPathError, string LocalPathLog, DateTime dateNow, string LocalPathRerun, string IsRerun, string LocalPathOtherLocation,Boolean IsLoadDatafromWebservice)
        {
            //Step 1 : DTS from Flat file to SQL Server
            DateTime DTCurrent = DateTime.Now;

                char[] delimiter = { '|' };
                string formatDate = "yyyy-MM-dd";

                Service.WS_TrakcareCls ws_trak = new Service.WS_TrakcareCls();
                DataTable dtArr = ws_trak.GetPTArrivedIncaseFlatFileNotGen(dateNow.ToString(formatDate));
  
                foreach (DataRow drArr in dtArr.Rows)
                {
                    string EN = string.Empty;
                    string Location = string.Empty;
                    int res = 0;
                    try
                    {
                        Location = string.Empty;
                        EN = drArr["PAADM_ADMNo"].ToString();
                        Location = drArr["CTLOC_Code"].ToString();

                        if (EN != string.Empty)
                        {
                             res = serviceGetPTArrivedCheckupFilter(Location, EN, dateNow);
                        }
                        if (res == 1)//success
                        {
                                string PathSuccess = LocalPathSuccess + DateTime.Now.ToString(formatDate);
                                string FileSuccess = PathSuccess + "\\" + EN + "_S_" + Location + ".txt" ;
                                if (!Directory.Exists(PathSuccess))
                                    Directory.CreateDirectory(PathSuccess);
                                if (!File.Exists(FileSuccess)) 
                                   File.AppendAllText(FileSuccess, "Success : DateTime = "+  DateTime.Now.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
         
                    }
                }
        }
        //end suriya 18/03/2015

        public int serviceGetPTArrivedCheckupFilter(string SiteCode, string en, DateTime dateNow)
        {
            try
            {
                SiteCode = returnMainSite(SiteCode);//add suriya 15/08/2016
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {

                    if (cdc.tmp_getptarriveds.Any(t => t.paadm_admno == en && t.ctloc_code == SiteCode))//add suriya 24/03/2015
                        return -1;

                    Service.WS_TrakcareCls ws_trak = new Service.WS_TrakcareCls();
                    var result = ws_trak.GetPTArrivedCheckUpFilter(SiteCode, en, dateNow.ToString("yyyy-MM-dd")).AsEnumerable();
                    

                    tmp_getptarrived patient_arrive = result.Select(x => new tmp_getptarrived
                    {
                        paadm_type_of_patient_calc = x.Field<string>("PAADM_Type_of_Patient_Calc"),
                        paadm_rowid = x.Field<int>("PAADM_RowID").ToString(),
                        appt_rowid = x.Field<string>("APPT_RowId"),
                        appt_arrivaltime = x.Field<TimeSpan>("APPT_ArrivalTime").ToString(),
                        paadm_admdate = x.Field<DateTime?>("PAADM_AdmDate"),
                        allergy_eng = GetAllergyByHN(x.Field<string>("PAPMI_No")),//call webserice
                        paadm_admno = x.Field<string>("PAADM_ADMNo"),
                        papmi_no = x.Field<string>("PAPMI_No"),
                        ttl_desc = x.Field<string>("TTL_Desc"),
                        papmi_name = x.Field<string>("PAPMI_Name"),
                        papmi_name2 = x.Field<string>("PAPMI_Name2"),
                        appt_transdate = x.Field<DateTime?>("APPT_TransDate"),
                        appt_datesearch = x.Field<DateTime?>("APPT_DateSearch"),
                        paadm_admtime = x.Field<TimeSpan>("PAADM_admTime").ToString(),
                        ctloc_code = returnMainSite(x.Field<string>("CTLOC_Code")),
                        ctloc_desc = x.Field<string>("CTLOC_Desc"),
                        penstype_code = x.Field<string>("PENSTYPE_Code"),
                        penstype_desc = x.Field<string>("PENSTYPE_Desc"),
                        ser_rowid = x.Field<int>("SER_RowId"),
                        ser_desc = x.Field<string>("SER_Desc"),
                        ctnat_code = x.Field<string>("CTNAT_Code"),
                        ctnat_desc = x.Field<string>("CTNAT_Desc"),
                        ctsex_code = x.Field<string>("CTSEX_Code"),
                        ctsex_desc = x.Field<string>("CTSEX_Desc"),
                        papmi_dob = x.Field<DateTime?>("PAPMI_DOB"),
                        //paper_photo = getImagePatient(x.Field<string>("PAPER_Photo")),
                        paper_photo = new GetPatientImage().getByWebService(x.Field<string>("PAPMI_No")),
                        //paper_photo_path = x.Field<string>("PAPER_Photo"),
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

                        papmi_dob_text = x.Field<string>("papmi_dob_text")
                        
                     
                    }).FirstOrDefault();
                    if (patient_arrive != null)
                    {
                        if (!cdc.tmp_getptarriveds.Any(t => t.paadm_admno == en && t.ctloc_code == SiteCode))
                        {
                            cdc.tmp_getptarriveds.InsertOnSubmit(patient_arrive);
                            cdc.SubmitChanges();
                        }
                    
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex; //add  suriya 24/03/2015
            }
        }

        private class site
        {
            public string sub_site { get; set; }
            public string main_site { get; set; }
        }
        private List<site> mst_site = new List<site>
        {
            new site { sub_site = "01AMSCHK", main_site = "01AMS" }
        };
        private string returnMainSite(string sub_site)
        {
            site result = mst_site.Where(x => x.sub_site == sub_site).FirstOrDefault();
            if (result == null)
            {
                return sub_site;
            }
            return result.main_site;
        }
        private string GetAllergyByHN(String HN)
        {
            try
            {
                Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
                DataTable dt = ws.GetAllAllergyByHN(HN);

                List<string> allery = dt.AsEnumerable().Select(x => x.Field<string>("Allergy")).ToList();
                if (allery != null && allery.Count() > 0)
                {
                    return string.Join(Environment.NewLine, allery);
                }
                return "No Know Allergy";
            }
            catch (Exception ex)
            {
                Program.MessageError("RetrieveArrivedCls", "GetAllergyByHN", ex, false);
                return "";
            }
        }
        public byte[] getImagePatient(string fileName)
        {
            try
            {
                Image imageIn = Image.FromFile(@"\\10.1.106.230\Photos\BMC-LIVE\PatientPhotos\" + fileName);
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                try
                {
                    Program.MessageError("RetrieveArrivedCls", "imageToByteArray", ex, false);
                    Image imageIn = CallDataTakeCareArrived.Properties.Resources.no_image;
                    MemoryStream ms = new MemoryStream();
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                    throw;
                }
                catch (Exception ex2)
                {
                    Program.MessageError("RetrieveArrivedCls", "imageToByteArray", ex2, false);
                    return null;
                }
            }
        }
    }
}
