using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class ImportPatientCls
    {
        public int? RegisPatient(string location, string en, DateTime arrived, string user)
        {
            using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
            {
                var result = ws.GetPTArrivedCheckUpFilter(location, en, arrived.ToString("yyyy-MM-dd")).AsEnumerable().FirstOrDefault();
                if (result != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        DateTime dateNow = globalCls.GetServerDateTime();
                        var patient = cdc.trn_patients.Where(x => x.tpt_hn_no == result.Field<string>("PAPMI_No")).FirstOrDefault();
                        if (patient == null)
                        {
                            patient = new trn_patient
                            {
                                tpt_hn_no = result.Field<string>("PAPMI_No"),
                                tpt_create_date = dateNow
                            };
                            cdc.trn_patients.InsertOnSubmit(patient);
                        }
                        patient.tpt_pre_name = result.Field<string>("TTL_Desc") == null ? "" : result.Field<string>("TTL_Desc").Trim();
                        patient.tpt_first_name = result.Field<string>("PAPMI_Name") == null ? "" : result.Field<string>("PAPMI_Name").Trim();
                        patient.tpt_last_name = result.Field<string>("PAPMI_Name2") == null ? "" : result.Field<string>("PAPMI_Name2").Trim();
                        patient.tpt_en_name1 = result.Field<string>("PAPER_Name5") == null ? "" : result.Field<string>("PAPER_Name5").Trim();
                        patient.tpt_en_name2 = result.Field<string>("PAPER_Name6") == null ? "" : result.Field<string>("PAPER_Name6").Trim();
                        patient.tpt_en_name3 = result.Field<string>("PAPMI_Name7") == null ? "" : result.Field<string>("PAPMI_Name7").Trim();
                        List<string> name = new List<string>();
                        if (patient.tpt_pre_name != "") name.Add(patient.tpt_pre_name);
                        if (patient.tpt_first_name != "") name.Add(patient.tpt_first_name);
                        if (patient.tpt_en_name3 != "") name.Add(patient.tpt_en_name3);
                        if (patient.tpt_last_name != "") name.Add(patient.tpt_last_name);
                        patient.tpt_fullname = string.Join(" ", name);
                        patient.tpt_othername = string.Join(" ", name);
                        patient.tpt_id_card = result.Field<string>("PAPER_ID") == null ? "" : result.Field<string>("PAPER_ID").Trim();
                        patient.tpt_dob = result.Field<DateTime?>("PAPMI_DOB");
                        patient.tpt_dob_text = result.Field<string>("papmi_dob_text") == null ? "" : result.Field<string>("papmi_dob_text").Trim();
                        patient.tpt_gender = result.Field<string>("CTSEX_Code") == null || result.Field<string>("CTSEX_Code").Trim().Length == 0 ? (char?)null : result.Field<string>("CTSEX_Code").Trim()[0];
                        Marrie mar = new Marrie(result.Field<string>("CTMAR_Desc"));
                        patient.tpt_married = mar.code;
                        patient.tpt_married_desc = mar.desc;
                        patient.tpt_nation_code = result.Field<string>("CTNAT_Code") == null ? "" : result.Field<string>("CTNAT_Code").Trim();
                        patient.tpt_nation_desc = result.Field<string>("CTNAT_Desc") == null ? "" : result.Field<string>("CTNAT_Desc").Trim();
                        patient.tpt_allergy = new APITrakcare.GetAllergyCls().ByGetAllergyByHN(result.Field<string>("PAPMI_No"));
                        patient.tpt_image = GetImage(result.Field<string>("PAPMI_No"));
                        patient.tpt_vip_hpc = location == "01HPC3" ? true : false;
                        patient.tpt_update_date = dateNow;

                        int mhs_id = cdc.mst_hpc_sites.Where(x => x.mhs_code == location).Select(x => x.mhs_id).FirstOrDefault();
                        var regis = patient.trn_patient_regis.Where(x => x.tpr_en_no == result.Field<string>("PAADM_ADMNo") && x.mhs_id == mhs_id).FirstOrDefault();
                        if (regis == null)
                        {
                            DateTime? PAADM_AdmDate = result.Field<DateTime?>("PAADM_AdmDate");
                            TimeSpan? PAADM_AdmTime = result.Field<TimeSpan?>("PAADM_AdmTime");
                            TimeSpan? APPT_ArrivalTime = result.Field<TimeSpan?>("APPT_ArrivalTime");
                            DateTime? app = null;
                            DateTime? arr = null;
                            if (PAADM_AdmDate != null)
                            {
                                app = PAADM_AdmDate.Value;
                                arrived = PAADM_AdmDate.Value;
                                if (APPT_ArrivalTime != null)
                                {
                                    app = app.Value.Add(APPT_ArrivalTime.Value);
                                }
                                if (PAADM_AdmTime != null)
                                {
                                    arr = arr.Value.Add(PAADM_AdmTime.Value);
                                }
                            }
                            var pattype = '1';
                            var apptype = GetAppointType(GetLimitAppoint(mhs_id), app, arr);
                            var arrtype = app == null ? 'W' : 'A';
                            var queueType = GetQueueType(result.Field<int>("SER_RowId"), !string.IsNullOrEmpty(result.Field<string>("PENSTYPE_Code")), apptype);
                            regis = new trn_patient_regi
                            {
                                mhs_id = mhs_id,

                                tpr_en_no = result.Field<string>("PAADM_ADMNo"),
                                tpr_vip_code = result.Field<string>("PENSTYPE_Code") == null ? null : result.Field<string>("PENSTYPE_Code").Trim(),
                                tpr_vip_desc = result.Field<string>("PENSTYPE_Desc") == null ? null : result.Field<string>("PENSTYPE_Desc").Trim(),
                                tpr_email = result.Field<string>("PAPER_Email") == null ? null : result.Field<string>("PAPER_Email").Trim(),
                                tpr_en_rowid = result.Field<int?>("PAADM_RowID") == null ? null : result.Field<int?>("PAADM_RowID").ToString(),
                                tpr_foreigner = result.Field<string>("CTNAT_Code") == null ? (char?)null : (result.Field<string>("CTNAT_Code") == "TH" ? 'N' : 'Y'),
                                tpr_home_phone = result.Field<string>("PAPER_TelH") == null ? null : result.Field<string>("PAPER_TelH").Trim(),
                                tpr_main_address = result.Field<string>("PAPER_StName") == null ? null : result.Field<string>("PAPER_StName").Trim(),
                                tpr_main_amphur = result.Field<string>("CTCIT_Desc") == null ? null : result.Field<string>("CTCIT_Desc").Trim(),
                                tpr_main_province = result.Field<string>("PROV_Desc") == null ? null : result.Field<string>("PROV_Desc").Trim(),
                                tpr_main_tumbon = result.Field<string>("CITAREA_Desc") == null ? null : result.Field<string>("CITAREA_Desc").Trim(),
                                tpr_main_zip_code = result.Field<string>("CTZIP_Code") == null ? null : result.Field<string>("CTZIP_Code").Trim(),
                                tpr_office_phone = result.Field<string>("PAPER_TelO") == null ? null : result.Field<string>("PAPER_TelO"),
                                tpr_mobile_phone = result.Field<string>("PAPER_MobPhone") == null ? null : result.Field<string>("PAPER_MobPhone").Trim(),
                                tpr_new_patient = result.Field<string>("PAADM_Type_of_Patient_Calc") == null ? (char?)null : (new List<string> { "1", "2" }.Contains(result.Field<string>("PAADM_Type_of_Patient_Calc")) ? 'Y' : 'N'),

                                tpr_appoint_type = apptype,
                                tpr_appointment_date = app,
                                tpr_arrive_date = arr,
                                tpr_arrive_type = arrtype,

                                tpr_patient_type = pattype,

                                tpr_req_doc_code = null,
                                tpr_req_doc_gender = null,
                                tpr_req_doc_name = null,
                                tpr_req_doctor = null,
                                tpr_req_inorout_doctor = null,
                                tpr_req_pe_bef_chkup = null,
                                tpr_req_same_doc = null,
                                tpr_pe_doc = null, // queue
                                tpr_pe_doc_code = null, // queue
                                tpr_pe_doc_name = null, // queue
                                tpr_pe_site2 = 'P', // site 2
                                tpr_pe_type = 'W', // site 1
                                tpr_pending_no_station = null,
                                tpr_check_pending = 'N', //pending
                                tpr_type = 'D',
                                tpr_return_screening = null,

                                tpr_miss_lower = null, //ultrasound
                                tpr_miss_lower_date = null, //ultrasound
                                tpr_call_lower_date = null, //ultrasound
                                tpr_call_lower_time = null, //ultrasound

                                tpr_pd_pe_site2 = null, // queue
                                tpr_pending = null,
                                tpr_pending_cancel_onday = null,
                                tpr_pending_ct = 0,
                                tpr_site_use = null, // stamp on checkpoint B

                                tpr_status = "WB", // status = Wait Book, for CK Report
                                tpr_pe_status = "RS", // status = Result, for CK Report

                                tpr_mhc_ename = null, //package

                                tpr_other_address = null, // trn_patient_regis last espisode
                                tpr_other_amphur = null, // trn_patient_regis last espisode
                                tpr_other_province = null, // trn_patient_regis last espisode
                                tpr_other_tumbon = null, // trn_patient_regis last espisode
                                tpr_other_zip_code = null, // trn_patient_regis last espisode
                                tpr_other_name = null,

                                tpr_PRM = null, // package PMR
                                tpr_PRM_check = null, // package PMR
                                tpr_PRM_doccode = null, // package PMR
                                tpr_PRM_docname = null, // package PMR
                                tpr_PRM_doctor = null, // package PMR

                                tpr_queue_no = GetQueueNo(result.Field<string>("PAADM_ADMNo"), queueType, CheckOtherSite(mhs_id)),
                                tpr_queue_type = CheckVIP(patient.tpt_hn_no) ? '1' : queueType,

                                tpr_remark = null,
                                tpr_send_book = null,
                                tpr_send_to = null, // trn_patient_regis last espisode

                                tnc_document_no = null, // company
                                tnc_emp_id = null, // company
                                tnc_id = null, // company
                                tnc_position = null, // company
                                tpr_employee_no = null,
                                tcd_id = null,
                                tpr_comp_dep = null,
                                tpr_comp_dep_edesc = null,
                                tpr_comp_dep_tdesc = null,
                                tpr_comp_edesc = null,
                                tpr_comp_tdesc = null,
                                tpr_company_code = null,
                                tpr_company_id = null,
                                tnc_department = null,

                                tpr_create_by = user,
                                tpr_create_date = dateNow,
                                tpr_update_by = user,
                                tpr_update_date = dateNow,

                                tpr_sys_assign_doctor = null, // not use
                                mut_id = null, // not use
                                mac_id = null, // not use
                                mdc_id = null, // not use
                                mhc_id = null, // not use
                                tpr_arrive_site = "", // not use
                                tpr_arrive_site_desc = "", // not use
                                tpr_close_other_site = null, // not use
                                tpr_npo_text = null, // tmp_patient_regis
                                tpr_npo_time = null, // tmp_patient_regis
                                tpr_aviation_type = null, //select on register
                                tpr_nurse_code = null,
                                tpr_nurse_name = null,
                                tpr_prev_pe_code = null,
                                tpr_prev_pe_name = null,
                                tpr_rtn_pe_date = null,
                                tpr_print_book = "N",
                                tpr_rtn_pe_name = null
                            };
                            patient.trn_patient_regis.Add(regis);
                        }
                        cdc.SubmitChanges();
                        return regis.tpr_id;
                    }
                }
            }
            return null;
        }

        private bool CheckVIP(string hn)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var vip = cdc.mst_check_vvips.Where(x => x.mvp_hn_no == hn).Count();
                    if (vip > 0)
                    {
                        return true;
                    }
                }
            }
            catch
            {

            }
            return false;
        }
        private bool CheckOtherSite(int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var otherSite = cdc.mst_hpc_sites.Where(x => x.mhs_id == mhs_id && x.mhs_other_clinic == true).Count();
                    if (otherSite == 0)
                    {
                        return false;
                    }
                }
            }
            catch
            {

            }
            return true;
        }
        private char? GetAppointType(int LimitAppointTime, DateTime? appoint, DateTime? arrive)
        {
            if (appoint == null)
            {
                return 'L';
            }
            else
            {
                if (appoint.Value.Subtract(arrive.Value).TotalMinutes <= LimitAppointTime)
                {
                    return 'T';
                }
                else
                {
                    return 'L';
                }
            }
        }
        private char? GetQueueType(int ser_rowid, bool isVIPCode, char? appointType)
        {
            if (isVIPCode)
            {
                if (appointType == 'L')
                {
                    return '3';
                }
                else if (ser_rowid != 2808)
                {
                    return '2';
                }
                else
                {
                    return '3';
                }
            }
            else
            {
                if (appointType == 'L')
                {
                    return '5';
                }
                else if (ser_rowid != 2808)
                {
                    return '4';
                }
                else
                {
                    return '5';
                }
            }
        }
        private string GetQueueNo(string en, char? QueueType, bool isOtherSite)
        {
            string QueueNo = en.Substring(en.Length - 4);
            if (isOtherSite)
            {
                return "5" + QueueNo;
            }
            switch (QueueType)
            {
                case '1':
                case '2':
                case '4':
                    return "1" + QueueNo;
                case '3':
                case '5':
                    return "9" + QueueNo;
                default:
                    return "9" + QueueNo;;
            }
        }
        private int GetLimitAppoint(int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var config = cdc.mst_config_dtls.Where(x => x.mst_config_hdr.mhs_id == mhs_id && x.mfd_code == "WTM").FirstOrDefault();
                    if (config != null)
                    {
                        if (config.mfd_value != null)
                        {
                            return Convert.ToInt32(config.mfd_value);
                        }
                    }
                }
            }
            catch
            {

            }
            return 1440;
        }
        private byte[] GetImage(string hn)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var img = ws.GetPTImageByHN(hn).AsEnumerable().Select(x => x.Field<byte[]>("docData")).FirstOrDefault();
                    if (img == null)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                    return img;
                }
            }
            catch
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        private class Marrie
        {
            public Marrie()
            {

            }
            public Marrie(string CTMAR_Desc)
            {
                Marrie m = Get(CTMAR_Desc);
                desc = m.desc;
                code = m.code;
            }

            public Marrie Get(string CTMAR_Desc)
            {
                if (CTMAR_Desc.Contains("Single") == true)
                {
                    desc = "Single";
                    code = 'S';
                }
                else if (CTMAR_Desc.Contains("Married") == true)
                {
                    desc = "Married";
                    code = 'M';
                }
                else if (CTMAR_Desc.Contains("Widowed") == true)
                {
                    desc = "Widowed";
                    code = 'W';
                }
                else if (CTMAR_Desc.Contains("Divorced") == true)
                {
                    desc = "Divorced";
                    code = 'D';
                }
                else if (CTMAR_Desc.Contains("Unknown") == true)
                {
                    desc = "Unknown";
                    code = 'U';
                }
                else
                {
                    desc = "";
                    code = (char?)null;
                }
                return this;
            }

            public string desc { get; set; }
            public char? code { get; set; }
        }
    }
}