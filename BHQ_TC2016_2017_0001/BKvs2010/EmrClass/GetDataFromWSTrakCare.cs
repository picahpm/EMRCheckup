using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class GetDataFromWSTrakCare
    {
        public StatusTransaction WS_GetPTPackageAllStation(ref List<StationStatus> list_StationStatus, int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    int en_rowid = Convert.ToInt32(patient_regis.tpr_en_rowid);
                    using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                    {
                        var result_datatable = ws.GetPTPackage(en_rowid).AsEnumerable();
                        var result = result_datatable
                                     .Where(x => (x.Field<string>("OSTAT_Code") == "V" ||
                                                  x.Field<string>("OSTAT_Code") == "E") &&
                                                  x.Field<string>("ARCOS_RowId") != null)
                                     .Select(x => new EntityGetPTPackage
                                     {
                                         ARCOS_RowId = x.Field<string>("ARCOS_RowId"),
                                         ARCOS_Code = x.Field<string>("ARCOS_Code"),
                                         ARCOS_Desc = x.Field<string>("ARCOS_Desc"),

                                         ARCIM_RowId = x.Field<string>("ARCIM_RowId"),
                                         ARCIM_Code = x.Field<string>("ARCIM_Code"),
                                         ARCIM_Desc = x.Field<string>("ARCIM_Desc"),
                                         OEORI_LabTestSetRow = x.Field<string>("OEORI_LabTestSetRow"),
                                         OEORI_AccessionNumber = x.Field<string>("OEORI_AccessionNumber"),
                                         ARCIM_Text1 = x.Field<string>("ARCIM_Text1"),
                                         OSTAT_Code = x.Field<string>("OSTAT_Code")
                                     }).ToList();
                        var grpStatus = result.GroupBy(x => x.OSTAT_Code);
                        List<StationStatus> list_station = new List<StationStatus>();
                        foreach (var grp in grpStatus)
                        {
                            foreach (var item in grp)
                            {
                                List<int> _mvt_id = cdc.mst_order_plans.Where(x => x.mop_item_row_id == item.ARCIM_RowId).Select(x => x.mvt_id).ToList();
                                foreach (int val in _mvt_id)
                                {
                                    list_station.Add(new StationStatus { mvt_id = val, status = grp.Key });
                                }
                            }
                        }
                        list_StationStatus = list_station.GroupBy(x => x.mvt_id).Select(x => new StationStatus
                        {
                            mvt_id = x.Key,
                            status = x.Any(y => y.status == "E") ? "E" : "V"
                        }).ToList();
                        return StatusTransaction.True;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetDataFromWSTrakCare", "GetPatientAllStation", ex, false);
                return StatusTransaction.Error;
            }
        }
        public void otherClinicSkipToCheckB(tmp_getptarrived tga, ref trn_patient objpatient, int SiteCode = -999)//add SiteCode suriya 03/04/2015
        {
            if (tga != null)
            {
                DateTime dateNow = DateTime.Now; //Program.GetServerDateTime();
                if (objpatient == null)
                {
                    objpatient = new trn_patient();
                    objpatient.tpt_create_date = dateNow;
                }
                objpatient.tpt_update_date = dateNow;
                objpatient.tpt_hn_no = tga.papmi_no;
                objpatient.tpt_gender = Convert.ToChar(tga.ctsex_code);

                objpatient.tpt_pre_name = tga.ttl_desc;
                objpatient.tpt_first_name = tga.papmi_name;
                objpatient.tpt_last_name = tga.papmi_name2;
                objpatient.tpt_image = tga.paper_photo;

                objpatient.tpt_dob = tga.papmi_dob;
                objpatient.tpt_nation_code = tga.ctnat_code;
                objpatient.tpt_nation_desc = tga.ctnat_desc;
                objpatient.tpt_update_date = dateNow;
                objpatient.tpt_id_card = tga.paper_id;

                objpatient.tpt_allergy = tga.allergy_eng;
                objpatient.tpt_en_name1 = tga.paper_name5;
                objpatient.tpt_en_name2 = tga.paper_name6;
                objpatient.tpt_en_name3 = tga.paper_name7;

                objpatient.tpt_dob_text = tga.papmi_dob_text;
                try
                {
                    string tname = string.IsNullOrEmpty(tga.ttl_desc) ? "" : tga.ttl_desc.Trim();
                    string fname = string.IsNullOrEmpty(tga.papmi_name) ? "" : " " + tga.papmi_name.Trim();
                    string mname = string.IsNullOrEmpty(tga.paper_name7) ? "" : " " + tga.paper_name7.Trim();
                    string lname = string.IsNullOrEmpty(tga.papmi_name2) ? "" : " " + tga.papmi_name2.Trim();
                    string fullname = (tname + fname + mname + lname).Trim();
                    objpatient.tpt_fullname = fullname;
                    objpatient.tpt_othername = fullname;
                }
                catch (Exception ex)
                {
                    try
                    {
                        string tname = string.IsNullOrEmpty(tga.ttl_desc) ? "" : tga.ttl_desc.Trim();
                        string fname = string.IsNullOrEmpty(tga.papmi_name) ? "" : " " + tga.papmi_name.Trim();
                        string lname = string.IsNullOrEmpty(tga.papmi_name2) ? "" : " " + tga.papmi_name2.Trim();
                        string fullname = (tname + fname + lname).Trim();
                        objpatient.tpt_fullname = fullname;
                        objpatient.tpt_othername = fullname;
                    }
                    catch
                    {

                    }
                    Program.MessageError("GetDataFromTrakCare", "tpt_fullname", ex, false);
                }

                if (!string.IsNullOrEmpty(tga.ctmar_desc))
                {
                    married mar = mst_married.Where(x => x.desc.Any(y => tga.ctmar_desc.Contains(y))).FirstOrDefault();
                    if (mar != null)
                    {
                        objpatient.tpt_married = mar.status;
                        objpatient.tpt_married_desc = mar.desc;
                    }
                }
                objpatient.trn_patient_regis.Add(getDefaultPatientRegis(tga, SiteCode));//add SiteCode suriya 03/04/2015
            }
        }
        public trn_patient_regi getDefaultPatientRegis(tmp_getptarrived tga, int SiteCode = -999)//add SiteCode suriya 03/04/2015
        {
            mst_hpc_site mhs_clinic = getSiteClinic();
            DateTime dateNow = DateTime.Now; //Program.GetServerDateTime();

            trn_patient_regi tpr = new trn_patient_regi
            {
                mac_id = null, //
                mdc_id = null, //
                mhc_id = null, //
                //mhs_id = mhs_clinic.mhs_id,//del  suriya 03/04/2015
                mhs_id = SiteCode == -999 ? mhs_clinic.mhs_id : Program.CurrentSite.mhs_id,//add  suriya 03/04/2015
                mut_id = null, //
                tcd_id = null, //
                tnc_id = null, //
                tpr_appoint_type = 'T',
                //tpr_appointment_date = dateNow,//del  suriya 03/04/2015
                //tpr_arrive_date = dateNow,//del  suriya 03/04/2015
                tpr_appointment_date = SiteCode == -999 ? dateNow : tga.paadm_admdate,//add  suriya 03/04/2015
                tpr_arrive_date = SiteCode == -999 ? dateNow : tga.paadm_admdate,//add  suriya 03/04/2015
                tpr_arrive_type = tga.ser_rowid == 2808 ? 'W' : 'A',
                tpr_aviation_type = null, //
                tpr_check_pending = 'N',
                tpr_comp_dep = null, //
                tpr_comp_dep_edesc = null, //
                tpr_comp_dep_tdesc = null, //
                tpr_comp_edesc = null, //
                tpr_comp_tdesc = null, //
                tpr_company_code = null, //
                tpr_company_id = null, //
                tpr_create_by = "CONSULT",
                tpr_create_date = dateNow,
                tpr_email = tga.paper_email,
                tpr_employee_no = null, //
                tpr_en_no = tga.paadm_admno,
                tpr_en_rowid = tga.paadm_rowid,
                tpr_foreigner = tga.ctnat_code == "TH" ? 'N' : 'Y', // 'TH' ? 'N' : 'Y'
                tpr_home_phone = tga.paper_telh,
                tpr_main_address = tga.paper_stname,
                tpr_main_amphur = tga.ctcit_desc,
                tpr_main_id = null, // reinsert when submit
                tpr_main_province = tga.prov_desc,
                tpr_main_tumbon = tga.citarea_desc,
                tpr_main_zip_code = tga.ctzip_code,
                //tpr_mhc_ename = tpos.Select(x => x.tos_od_set_name).FirstOrDefault(),
                tpr_miss_lower = false,
                tpr_mobile_phone = tga.paper_mobphone,
                tpr_new_patient = getNewPatientType(tga),
                tpr_npo_text = null, //
                tpr_npo_time = null, //
                tpr_office_phone = tga.paper_telo,
                tpr_other_address = tga.paper_stname,
                tpr_other_amphur = tga.ctcit_desc,
                tpr_other_province = tga.prov_desc,
                tpr_other_tumbon = tga.citarea_desc,
                tpr_other_zip_code = tga.ctzip_code,
                tpr_patient_type = '1',
                tpr_pe_doc = 'N',
                tpr_pe_doc_code = null, //
                tpr_pe_doc_name = null, //
                tpr_pe_status = null, //
                tpr_pe_type = 'W',
                tpr_pending = false,
                tpr_pending_cancel_onday = false,
                tpr_pending_ct = 0,
                tpr_pending_no_station = null, //
                tpr_prev_pe_code = null, //
                tpr_prev_pe_name = null, //
                tpr_print_book = "N",
                tpr_PRM = null,
                tpr_PRM_check = null,
                tpr_PRM_doccode = null,
                tpr_PRM_docname = null,
                tpr_PRM_doctor = null,
                tpr_queue_no = getQueueOtherClinic(tga.paadm_admno),
                tpr_queue_type = getTprQueueType(tga),
                tpr_remark = null, //
                tpr_req_doc_code = null, //
                tpr_req_doc_gender = null, //
                tpr_req_doc_name = null, //
                tpr_req_doctor = 'N',
                tpr_req_inorout_doctor = null, //
                tpr_req_pe_bef_chkup = null, //
                tpr_rtn_pe_date = null, //
                tpr_rtn_pe_name = null, //
                tpr_send_book = null, //
                tpr_send_to = null, //
                tpr_site_use = null, //
                tpr_status = null, //
                tpr_type = null, //
                tpr_update_by = "CONSULT",
                tpr_update_date = dateNow,
                tpr_vip_code = tga.penstype_code,
                tpr_vip_desc = tga.penstype_desc,
                tpr_arrive_site = tga.LocWhenOrdOther,
                tpr_arrive_site_desc = tga.LocWhenOrdOtherDesc
            };

            tpr.trn_basic_measure_hdrs.Add(getBasicHdr(tga.papmi_no, tga.paadm_admno));


            //GetPatientPackage(ref tpr, dateNow);
            //genPatientPlan(ref tpr, Program.CurrentSite.mhs_id, dateNow);
            int enRowID = Convert.ToInt32(tpr.tpr_en_rowid);
            GetPTPackageCls PackageCls = new GetPTPackageCls();
            EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
            PackageCls.AddPatientOrderItem(ref tpr, "System", dateNow, getPTPackage);
            PackageCls.AddPatientOrderSet(ref tpr, "System", dateNow, getPTPackage);
            List<MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
            PackageCls.AddPatientEvent(ref tpr, "System", dateNow, mapOrder);
            PackageCls.AddPatientPlan(ref tpr, "System", dateNow, mapOrder);
            PackageCls.skipReqDoctorOutDepartment(ref tpr);
            PackageCls.CompleteEcho(ref tpr);
            PackageCls.skipChangeEstToEcho(ref tpr, tpr.mhs_id);
            PackageCls.checkOrderPMR(ref tpr, tpr.mhs_id);


            trn_patient_queue tpq = getPatientQueue("CB", tpr);
            if (tpq != null) tpr.trn_patient_queues.Add(tpq);

            List<trn_out_department> tod = getOutDepartment(tga.papmi_no, tga.ctloc_code, DateTime.Now);
            if (tod != null) tpr.trn_out_departments.AddRange(tod);

            trn_patient_cat tpc = getForeignerPatient(tga.ctnat_code);
            if (tpc != null) tpr.trn_patient_cats.Add(tpc);

            return tpr;
        }
        public trn_patient_cat getForeignerPatient(string nationCode)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                int mdc_id = cdc.mst_doc_categories.Where(x => x.mdc_code == "MD014"
                                                          && x.mdc_status == 'A'
                                                          && dateNow.Date >= x.mdc_effective_date.Value.Date
                                                          && dateNow.Date <= (x.mdc_expire_date == null ? dateNow.Date : x.mdc_expire_date.Value.Date))
                             .Select(x => x.mdc_id).FirstOrDefault();
                if (nationCode == "TH")
                {
                    return new trn_patient_cat
                    {
                        mdc_id = mdc_id
                    };
                }
                else
                {
                    return null;
                }
            }
        }
        public trn_basic_measure_hdr getBasicHdr(string hn, string en)
        {
            string rvisual_out_lens = null;
            string lvisual_out_lens = null;
            string rvisual_with_lens = null;
            string lvisual_with_lens = null;
            DateTime dateNow = Program.GetServerDateTime();
            using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
            {
                DataSet ds = ws.GetvitalSign(en);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        string WithContactLens = ds.Tables[0].Rows[0]["WithContactLens"] == null ? "" : ds.Tables[0].Rows[0]["WithContactLens"].ToString();
                        if (WithContactLens == "N")
                        {
                            rvisual_out_lens = ds.Tables[0].Rows[0]["VisionRt"].ToString();
                            lvisual_out_lens = ds.Tables[0].Rows[0]["VisionLt"].ToString();
                        }
                        else if (WithContactLens == "Y")
                        {
                            rvisual_with_lens = ds.Tables[0].Rows[0]["VisionRt"].ToString();
                            lvisual_with_lens = ds.Tables[0].Rows[0]["VisionLt"].ToString();
                        }
                    }
                }
            }
            trn_basic_measure_hdr tbm = new trn_basic_measure_hdr
            {
                tbm_appearance = "GD",
                tbm_arrive = "WK",
                tbm_create_by = "System",
                tbm_create_date = dateNow,
                tbm_precaution = "SDP",
                tbm_purpose = 'W',
                tbm_triage = '5',
                tbm_type = 'N',
                tbm_update_by = "System",
                tbm_update_date = dateNow,
                tbm_vision_lvisual_out_lens = lvisual_out_lens,
                tbm_vision_lvisual_with_lens = lvisual_with_lens,
                tbm_vision_rvisual_out_lens = rvisual_out_lens,
                tbm_vision_rvisual_with_lens = rvisual_with_lens
            };
            tbm.trn_basic_measure_dtls.AddRange(getVitalSign(hn, en));
            return tbm;
        }
        public List<trn_basic_measure_dtl> getVitalSign(string hn, string en)
        {
            using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
            {
                var result = getVitalSignByHN(hn);
                if (result != null)
                {
                    trn_basic_measure_dtl dtl = result.OrderByDescending(x => x.tbd_date).FirstOrDefault();
                    return new List<trn_basic_measure_dtl> { dtl };
                }
                return new List<trn_basic_measure_dtl>();
            }
        }

        public List<trn_basic_measure_dtl> getVitalSignByHN(string hn)
        {
            using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
            {
                DateTime dateNow = Program.GetServerDateTime();
                DataTable dt = ws.GetVitalSignByHN(hn);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<trn_basic_measure_dtl> bmDtl = new List<trn_basic_measure_dtl>();
                        var result = dt.AsEnumerable()
                                       .GroupBy(x => x.Field<DateTime>("OBS_Date").Add(x.Field<TimeSpan>("OBS_Time")))
                                       .OrderByDescending(x => x.Key)
                                       .Take(5);
                        foreach (var re in result)
                        {
                            trn_basic_measure_dtl dtl = new trn_basic_measure_dtl();
                            foreach (var item in re)
                            {
                                int itemNo = item.Field<int>("OBS_Item_DR");
                                switch (itemNo)
                                {
                                    case 230:
                                        dtl.tbd_weight = item.Field<string>("OBS_Value");
                                        break;
                                    case 231:
                                        dtl.tbd_height = item.Field<string>("OBS_Value");
                                        break;
                                    case 11:
                                        dtl.tbd_temp = item.Field<string>("OBS_Value");
                                        break;
                                    case 129:
                                        dtl.tbd_systolic = item.Field<string>("OBS_Value");
                                        break;
                                    case 128:
                                        dtl.tbd_diastolic = item.Field<string>("OBS_Value");
                                        break;
                                    case 9:
                                        dtl.tbd_pulse = item.Field<string>("OBS_Value");
                                        break;
                                    case 10:
                                        dtl.tbd_rr = item.Field<string>("OBS_Value");
                                        break;
                                    case 134:
                                        dtl.tbd_bmi = item.Field<string>("OBS_Value");
                                        break;
                                    case 176:
                                        dtl.tbd_waist = item.Field<string>("OBS_Value");
                                        break;
                                    case 173:
                                        dtl.tbd_vision_lt = item.Field<string>("OBS_Value");
                                        break;
                                    case 174:
                                        dtl.tbd_vision_rt = item.Field<string>("OBS_Value");
                                        break;
                                    case 281:
                                        dtl.tbd_vision_with_lens = (item.Field<string>("OBS_Value") == "Y" || item.Field<string>("OBS_Value") == "y") ? true : false;
                                        break;
                                }
                            }
                            dtl.tbd_date = re.Key;
                            dtl.tbd_create_by = Program.CurrentUser.mut_username;
                            dtl.tbd_create_date = dateNow;
                            dtl.tbd_update_by = Program.CurrentUser.mut_username;
                            dtl.tbd_update_date = dateNow;
                            bmDtl.Add(dtl);
                        }
                        return bmDtl;
                    }
                }
            }
            return null;
        }
        public trn_patient_queue getPatientQueue(string roomCode, trn_patient_regi tpr)
        {
            DateTime dateNow = Program.GetServerDateTime();
            return new trn_patient_queue
            {
                mvt_id = getMvtId(roomCode),
                mrm_id = getMrmId(roomCode, getSiteCodeForMrm(roomCode, tpr)),
                mrd_id = null,
                tps_start_date = null,
                tps_end_date = null,
                tps_status = "NS",
                tps_ns_status = "QL",
                tps_create_by = "System",
                tps_create_date = dateNow,
                tps_update_by = "System",
                tps_update_date = dateNow
            };
        }
        public int? getSiteCodeForMrm(string roomCode, trn_patient_regi tpr)
        {
            if (roomCode == "CC")
            {
                return tpr.mhs_id;
            }
            else
            {
                return (tpr.tpr_site_use != null ? tpr.tpr_site_use : tpr.mhs_id);
            }
        }
        public int getMvtId(string mvt_code)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                int mvt_id = cdc.mst_events.Where(x => x.mvt_code == mvt_code && x.mvt_status == 'A'
                                                 && dateNow.Date >= x.mvt_effective_date.Value.Date
                                                 && dateNow.Date <= (x.mvt_expire_date == null ? dateNow : x.mvt_expire_date.Value.Date))
                             .Select(x => x.mvt_id).FirstOrDefault();
                return mvt_id;
            }
        }
        public int getMrmId(string mrm_code, int? mhs_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                int mrm_id = cdc.mst_room_hdrs.Where(x => x.mrm_code == mrm_code && x.mrm_status == 'A'
                                                 && x.mhs_id == mhs_id
                                                 && dateNow.Date >= x.mrm_effective_date.Value.Date
                                                 && dateNow.Date <= (x.mrm_expire_date == null ? dateNow : x.mrm_expire_date.Value.Date))
                             .Select(x => x.mrm_id).FirstOrDefault();
                return mrm_id;
            }
        }
        public List<trn_out_department> getOutDepartment(string hn, string siteCode, DateTime date)
        {
            DateTime dateNow = Program.GetServerDateTime();
            using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
            {
                var result = ws.getApptByHN(hn, siteCode, date.ToString("yyyy-MM-dd")).AsEnumerable();
                List<trn_out_department> tod = result.Select(x => new trn_out_department
                {
                    tod_desc = x.Field<string>("SER_Desc"),
                    tod_location = x.Field<string>("CTLOC_Desc"),
                    tod_start_date = convToDateTime(x.Field<TimeSpan>("AS_SessStartTime"), x.Field<DateTime>("AS_Date")),
                    tod_create_by = null,
                    tod_create_date = dateNow,
                    tod_update_by = null,
                    tod_update_date = dateNow
                }).ToList();
                return tod;
            }
        }
        public DateTime? convToDateTime(TimeSpan? time, DateTime? date)
        {
            if (time != null && date != null)
            {
                return new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, time.Value.Hours, time.Value.Minutes, 0);
            }
            return null;
        }
        public mst_hpc_site getSiteClinic()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                mst_hpc_site mhs = cdc.mst_hpc_sites.Where(x => x.mhs_other_clinic == true && x.mhs_status == 'A'
                                                           && dateNow.Date >= x.mhs_effective_date.Value.Date
                                                           && dateNow.Date <= (x.mhs_expire_date == null ? dateNow : x.mhs_expire_date.Value.Date))
                                   .FirstOrDefault();
                return mhs;
            }
        }
        public char? getTprQueueType(tmp_getptarrived tga)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                int site = cdc.mst_hpc_sites.Where(x => x.mhs_code == tga.ctloc_code).Select(x => x.mhs_id).FirstOrDefault();

                tmp_getptappointment tap = cdc.tmp_getptappointments.Where(x => x.papmi_no == tga.papmi_no).FirstOrDefault();
                TimeSpan tarrivaltime;
                TimeSpan.TryParse(tga.appt_arrivaltime.Replace("PT", "").Replace("H", ":").Replace("M", ":").Replace("S", ""), out tarrivaltime);
                TimeSpan tAppointment;
                TimeSpan.TryParse(tga.paadm_admtime.Replace("PT", "").Replace("H", ":").Replace("M", ""), out tAppointment);
                var litTime = (from t1 in cdc.mst_config_hdrs
                               where t1.mhs_id == site && t1.mfh_code == "WTM"
                               select t1.mst_config_dtls.FirstOrDefault()).FirstOrDefault();
                double litValueTime = 1440;
                if (litTime != null)
                {
                    litValueTime = (double)litTime.mfd_value;
                }

                TimeSpan ta = tarrivaltime.Subtract(tAppointment);
                List<string> patientVVIP = cdc.mst_check_vvips.Select(x => x.mvp_hn_no).ToList();
                if (patientVVIP.Contains(tga.papmi_no))
                {
                    return '1'; //VVIP
                }
                else if (!string.IsNullOrEmpty(tga.penstype_code))
                {
                    if (tap == null)
                    {
                        return '3'; //VIP WalkIn
                    }


                    if (ta.TotalMinutes <= litValueTime && tga.ser_rowid != 2808)
                        return '2'; //VIP Appointment
                    else
                        return '3'; //VIP WalkIn
                }
                else
                {
                    if (tap == null)
                    {
                        return '5'; //WalkIn
                    }
                    if (ta.TotalMinutes <= litValueTime && tga.ser_rowid != 2808)
                        return '4'; //Appointment
                    else
                        return '5'; //WalkIn
                }
            }
        }
        public char? getNewPatientType(tmp_getptarrived tga)
        {
            return (tga.paadm_type_of_patient_calc == "1" || tga.paadm_type_of_patient_calc == "2" ? 'Y' : 'N');
        }
        public string getQueueOtherClinic(string en)
        {
            try
            {
                return "5" + en.Substring(en.Length - 4, 4);
            }
            catch
            {
                return null;
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
        public string returnMainSite(string sub_site)
        {
            site result = mst_site.Where(x => x.sub_site == sub_site).FirstOrDefault();
            if (result == null)
            {
                return sub_site;
            }
            return result.main_site;
        }
        List<married> mst_married = new List<married>
        {
            new married { status = 'S', desc = "Single" },
            new married { status = 'M', desc = "Married" },
            new married { status = 'W', desc = "Widowed" },
            new married { status = 'D', desc = "Divorced" },
            new married { status = 'U', desc = "Unknown" }
        };
    }
}
