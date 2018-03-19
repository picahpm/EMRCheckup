using System;
using System.Linq;
using System.Data;
using DBCheckup;

namespace BKvs2010.APITrakcare
{
    public class GetPatientInfoCls
    {
        public bool GetInfo(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    string hn = patient_regis.trn_patient.tpt_hn_no;
                    using (Service.WS_TrakcareCls ws_trak = new Service.WS_TrakcareCls())
                    {
                        var info = ws_trak.GetPTInfoByHN(hn).AsEnumerable()
                                          .Select(x => new
                                          {
                                              PAPMI_No = x.Field<string>("PAPMI_No"),
                                              PAPMI_DOB = x.Field<DateTime>("PAPMI_DOB"),
                                              PAPMI_Name = x.Field<string>("PAPMI_Name"),
                                              PAPMI_Name2 = x.Field<string>("PAPMI_Name2"),
                                              PAPMI_Name3 = x.Field<string>("PAPMI_Name3"),
                                              PAPMI_Name5 = x.Table.Columns.Contains("PAPMI_Name5") ? x.Field<string>("PAPMI_Name5") : "",
                                              PAPMI_Name6 = x.Table.Columns.Contains("PAPMI_Name6") ? x.Field<string>("PAPMI_Name6") : "",
                                              PAPMI_Name7 = x.Table.Columns.Contains("PAPMI_Name7") ? x.Field<string>("PAPMI_Name7") : "",
                                              PAPER_PrefLanguage_DR = x.Field<int>("PAPER_PrefLanguage_DR"),
                                              PAPER_AgeYr = x.Field<string>("PAPER_AgeYr"),
                                              PAPER_AgeMth = x.Field<string>("PAPER_AgeMth"),
                                              PAPER_AgeDay = x.Field<string>("PAPER_AgeDay"),
                                              TTL_Desc = x.Field<string>("TTL_Desc"),
                                              CTSEX_Code = x.Field<string>("CTSEX_Code"),
                                              CTSEX_Desc = x.Field<string>("CTSEX_Desc"),
                                              PAPMI_DOB1 = x.Field<DateTime>("PAPMI_DOB1"),
                                              PAPMI_DOB_TEXT = x.Field<string>("papmi_dob_text"),
                                              PAPER_StName = x.Field<string>("PAPER_StName"),
                                              CTZIP_Code = x.Field<string>("CTZIP_Code"),
                                              CTZIP_Desc = x.Field<string>("CTZIP_Desc"),
                                              CTNAT_Desc = x.Field<string>("CTNAT_Desc"),
                                              //addby  Artist 18/01/2016
                                              Area = x.Field<string>("Area"),
                                              City = x.Field<string>("City"),
                                              Province = x.Field<string>("Province")
                                          }).FirstOrDefault();

                        string[] dobText = info.PAPMI_DOB_TEXT.Split('-');
                        string dobString = "";
                        if (dobText.Count() == 3)
                        {
                            dobString = dobText[2] + "/" + dobText[1] + "/" + dobText[0];
                        }

                        patient_regis.trn_patient.tpt_dob_text = dobString;
                        patient_regis.trn_patient.tpt_hn_no = info.PAPMI_No;
                        patient_regis.trn_patient.tpt_dob = info.PAPMI_DOB;
                        patient_regis.trn_patient.tpt_pre_name = info.TTL_Desc;
                        patient_regis.trn_patient.tpt_first_name = info.PAPMI_Name;
                        patient_regis.trn_patient.tpt_last_name = info.PAPMI_Name2;
                        patient_regis.trn_patient.tpt_gender = info.CTSEX_Code == "M" ? 'M' : 'F';
                        patient_regis.trn_patient.tpt_nation_desc = info.CTNAT_Desc;
                        //addby  Artist 18/01/2016
                        patient_regis.tpr_main_address = info.PAPER_StName;
                        patient_regis.tpr_main_tumbon = info.Area;
                        patient_regis.tpr_main_amphur = info.City;
                        patient_regis.tpr_main_province = info.Province;
                        patient_regis.tpr_other_address = info.PAPER_StName;
                        patient_regis.tpr_other_tumbon = info.Area;
                        patient_regis.tpr_other_amphur = info.City;
                        patient_regis.tpr_other_province = info.Province;
                        //
                        patient_regis.tpr_main_zip_code = info.CTZIP_Code;
                        patient_regis.tpr_other_zip_code = info.CTZIP_Code;

                        patient_regis.trn_patient.tpt_en_name1 = info.PAPMI_Name5;
                        patient_regis.trn_patient.tpt_en_name2 = info.PAPMI_Name6;
                        patient_regis.trn_patient.tpt_en_name3 = info.PAPMI_Name7;
                        //update image  add by m 08/05/2017 dd/mm/yyyy 
                        
                        patient_regis.trn_patient.tpt_image = new APITrakcare.GetPatientImageCls().GetImageByWS(info.PAPMI_No);
                        try
                        {
                            string tname = string.IsNullOrEmpty(info.TTL_Desc) ? "" : info.TTL_Desc.Trim();
                            string fname = string.IsNullOrEmpty(info.PAPMI_Name) ? "" : " " + info.PAPMI_Name.Trim();
                            string mname = string.IsNullOrEmpty(info.PAPMI_Name3) ? "" : " " + info.PAPMI_Name3.Trim();
                            string lname = string.IsNullOrEmpty(info.PAPMI_Name2) ? "" : " " + info.PAPMI_Name2.Trim();
                            string fullname = (tname + fname + mname + lname).Trim();
                            patient_regis.trn_patient.tpt_fullname = fullname;
                            patient_regis.trn_patient.tpt_othername = fullname;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                string tname = string.IsNullOrEmpty(info.TTL_Desc) ? "" : info.TTL_Desc.Trim();
                                string fname = string.IsNullOrEmpty(info.PAPMI_Name) ? "" : " " + info.PAPMI_Name.Trim();
                                string lname = string.IsNullOrEmpty(info.PAPMI_Name2) ? "" : " " + info.PAPMI_Name2.Trim();
                                string fullname = (tname + fname + lname).Trim();
                                patient_regis.trn_patient.tpt_othername = fullname;
                            }
                            catch
                            {

                            }
                            Program.MessageError("GetDataFromTrakCare", "tpt_fullname", ex, false);
                        }

                        cdc.SubmitChanges();
                    }
                }
                return true;
            }
            catch
            {

            }
            return false;
        }
    }
}
