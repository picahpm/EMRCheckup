using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Script.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DBCheckup;
using CheckupWebService.Class;

namespace CheckupWebService
{
    /// <summary>
    /// Summary description for WS_GetDataFromPathWay
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]

    public class WS_GetDataFromPathWay : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_result> getLabresult(string Lab_Item, string Lab_Sex, string Lab_date, string Lab_dob, string Lab_value, string Lab_Value_Type,
            string vision_left, string vision_right, string pulse, string bmi, string pregnancy, string systolic,
            string diastolic, string smoke, string HBsAg, string Anti_HBc, string HBsAb, string GlucoseF,
            string GlucoseU, string Diabete, string AST, string ALT, string PSA, string AcidPH, string Eosino,
            string Parasite, string MicroalbuminU, string Microalbumin, string MicroalbuminM, string RBCMorpho,
            string Hemoglobin, string HbA2, string MCV, string ProteinU, string ErythroU, string wbcU, string rbcU)
        {

            DateTime? ldate = Lab_date.convParamGetResultToDate();
            DateTime? ldob = Lab_dob.convParamGetResultToDate();

            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                float? cVisLeft = vision_left.convParamGetResultToFloat();
                float? cVisRight = vision_right.convParamGetResultToFloat();
                float? cPulse = pulse.convParamGetResultToFloat();
                float? cBmi = bmi.convParamGetResultToFloat();
                string cPregnancy = pregnancy.convParamGetResultToString();
                float? cSystolic = systolic.convParamGetResultToFloat();
                float? cDiastolic = diastolic.convParamGetResultToFloat();
                string cSmoke = smoke.convParamGetResultToString();
                string cHBsAg = HBsAg.convParamGetResultToString();
                string cAnti_HBc = Anti_HBc.convParamGetResultToString();
                float? cHBsAb = HBsAb.convParamGetResultToFloat();
                string cGlucoseF = GlucoseF.convParamGetResultToString();
                string cGlucoseU = GlucoseU.convParamGetResultToString();
                string cDiabete = diastolic.convParamGetResultToString();
                float? cAST = AST.convParamGetResultToFloat();
                float? cALT = ALT.convParamGetResultToFloat();
                float? cPSA = PSA.convParamGetResultToFloat();
                float? cAcidPH = AcidPH.convParamGetResultToFloat();
                float? cEosino = Eosino.convParamGetResultToFloat();
                string cParasite = Parasite.convParamGetResultToString();
                float? cMicroalbuminU = MicroalbuminU.convParamGetResultToFloat();
                float? cMicroalbumin = Microalbumin.convParamGetResultToFloat();
                float? cMicroalbuminM = MicroalbuminM.convParamGetResultToFloat();
                string cRBCMorpho = RBCMorpho.convParamGetResultToString();
                float? cHemoglobin = Hemoglobin.convParamGetResultToFloat();
                float? cHbA2 = HbA2.convParamGetResultToFloat();
                float? cMCV = MCV.convParamGetResultToFloat();
                string cProteinU = ProteinU.convParamGetResultToString();
                string cErythroU = ErythroU.convParamGetResultToString();
                string cWbcU = wbcU.convParamGetResultToString();
                string cRbcU = rbcU.convParamGetResultToString();


                var result = dbc.ws_return_lab_result(Lab_Item, Lab_Sex, ldate, ldob, Lab_value, Lab_Value_Type,
                             cVisLeft, cVisRight, cPulse, cBmi, cPregnancy, cSystolic, cDiastolic, cSmoke,
                             cHBsAg, cAnti_HBc, cHBsAb, cGlucoseF, cGlucoseU, cDiabete, cAST, cALT, cPSA, cAcidPH, cEosino,
                             cParasite, cMicroalbuminU, cMicroalbumin, cMicroalbuminM, cRBCMorpho, cHemoglobin, cHbA2,
                             cMCV, cProteinU, ErythroU, cWbcU, cRbcU);

                if (result != null)
                {
                    var re = (from o in result
                              select new output_result
                              {
                                  Lab_Code = o.lab_code.convParamGetResultToString(),
                                  Lab_TName = o.lab_tname.convParamGetResultToString(),
                                  Lab_EName = o.lab_ename.convParamGetResultToString(),
                                  Lab_Standard_Value_Max = o.lab_svalue_max.ToString().convParamGetResultToString(),
                                  Lab_Standard_Value_Min = o.lab_svalue_min.ToString().convParamGetResultToString(),
                                  Lab_Standard_Unit = o.lab_sunit.convParamGetResultToString(),
                                  Lab_Summary = o.lab_summary.convParamGetResultToString(),
                                  Lab_Result_Thai = o.lab_result_thai.convParamGetResultToString(),
                                  Lab_Result_Eng = o.lab_result_eng.convParamGetResultToString(),
                                  mlr_id = o.mlr_id
                              }).ToList();
                    if (re.Count() > 0) return re;
                }

                // return null obj when record = 0
                var a = new List<output_result> {
                                new output_result {
                                    Lab_Code = null,
                                    Lab_TName = null,
                                    Lab_EName = null,
                                    Lab_Standard_Value_Max = null,
                                    Lab_Standard_Value_Min = null,
                                    Lab_Standard_Unit = null,
                                    Lab_Result_Thai = null,
                                    Lab_Result_Eng = null,
                                    mlr_id = null
                                }
                            };
                var b = a.Count();
                return a;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string getLabLanguage(int mlr_id, string language)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                var result = (from re in dbc.mst_lab_recoms
                              where re.mlr_id == mlr_id
                              select re).FirstOrDefault();

                string lab_result = "";
                if (result != null)
                {
                    if (language.ToUpper() == "TH")
                    {
                        lab_result = result.mlr_th_name;
                    }
                    else if (language.ToUpper() == "EN")
                    {
                        lab_result = result.mlr_en_name;
                    }
                    else if (language.ToUpper() == "JP")
                    {
                        lab_result = result.mlr_jp_name;
                    }
                    else if (language.ToUpper() == "CH")
                    {
                        lab_result = result.mlr_ch_name;
                    }
                    else if (language.ToUpper() == "KR")
                    {
                        lab_result = result.mlr_kr_name;
                    }
                    else if (language.ToUpper() == "AR")
                    {
                        lab_result = result.mlr_ar_name;
                    }
                    else if (language.ToUpper() == "MR")
                    {
                        lab_result = result.mlr_mr_name;
                    }
                    else if (language.ToUpper() == "FT1")
                    {
                        lab_result = result.mlr_ftxt_name1;
                    }
                    else if (language.ToUpper() == "FT2")
                    {
                        lab_result = result.mlr_ftxt_name2;
                    }
                }
                return lab_result;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<Class.GetTextFileCls.ResultXray> GetResultXray(string hn_no, DateTime StartDate, DateTime EndDate)
        {
            GetTextFileCls cls = new GetTextFileCls();
            return cls.GetResultXray(hn_no, StartDate, EndDate);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool RetrieveImaging(int tpr_id, string username)
        {
            return new Class.InsertImagingCls().Insert(tpr_id, username);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<double> InsertLab(string hn, int tpr_id)
        {
            var rs = new List<double>();
            DateTime date = DateTime.Now;
            //new Class.InsertLabCls().RetrieveLabToEmrCheckup(tpr_id, "newins");
            //rs.Add((DateTime.Now - date).TotalSeconds);
            //date = DateTime.Now;
            new Class.InsertLabCls().Insert(tpr_id, "newins");
            rs.Add((DateTime.Now - date).TotalSeconds);
            return rs;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InsertDBEmrCheckupResultXray(string hn_no, string en_no, DateTime StartDate, DateTime EndDate, bool RetrieveOrder)
        {
            new InsertResultXray().retrieveResultXray(hn_no, en_no, StartDate, EndDate, RetrieveOrder);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void InsertDBEmrCheckupResultXrayBackground(string hn_no, string en_no, DateTime StartDate, DateTime EndDate, bool RetrieveOrder)
        {
            new InsertResultXray().retrieveResultXrayBackground(hn_no, en_no, StartDate, EndDate, RetrieveOrder);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_room_queue> getWaitingQueue(int hpc_site, string zone)
        {
            List<output_room_queue> result = new List<output_room_queue>();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var rooms = cdc.mst_room_screens
                                   .Where(x => x.mze_code == zone &&
                                               x.mhs_id == hpc_site &&
                                               x.mrs_status == 'A')
                                   .GroupBy(x => x.mrm_code)
                                   .Select(x => new
                                   {
                                       mrm_code = x.Key,
                                       mvt_code = x.Select(y => y.mvt_code).ToList()
                                   }).Distinct().ToList();
                    foreach (var room in rooms)
                    {
                        var list_mrm_id = cdc.mst_room_hdrs
                                             .Where(x => x.mrm_code == room.mrm_code &&
                                                         x.mhs_id == hpc_site)
                                             .Select(x => x.mrm_id).ToList();
                        foreach (int mrm_id in list_mrm_id)
                        {
                            List<int?> list_mvt_id = cdc.mst_events.Where(x => room.mvt_code.Contains(x.mvt_code)).Select(x => (int?)x.mvt_id).ToList();
                            List<sp_get_waiting_room_hdrResult> spResult = cdc.sp_get_waiting_room_hdr(mrm_id).OrderBy(x => x.priority).ToList();
                            List<sp_get_waiting_room_hdrResult> filterSpResult = spResult.Where(x => list_mvt_id.Contains(x.mvt_id) && x.tpt_vip_hpc != true && x.holded != true)
                                                                                         .Take(10).ToList();
                            foreach (var re in filterSpResult)
                            {
                                result.Add(new output_room_queue { queue_no = re.tpr_queue_no, room_code = room.mrm_code });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WS_GetDataFromPathWay.asmx", "getWaitingQueue(int hpc_site, string zone)", ex.Message);
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public CareProviderCode getDoctorLicence(string username)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    CareProviderCode result = ws.GetCareproviderCode(username).AsEnumerable()
                                                .Select(x => new CareProviderCode
                                                {
                                                    SSUSR_Initials = x.Field<string>("SSUSR_Initials"),
                                                    CTPCP_Code = x.Field<string>("CTPCP_Code"),
                                                    CTPCP_Desc = x.Field<string>("CTPCP_Desc"),
                                                    CTPCP_SMCNo = x.Field<string>("CTPCP_SMCNo")
                                                }).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WS_GetDataFromPathWay.asmx", "getDoctorLicence(string username)", ex.Message);
                return new CareProviderCode();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_queue_serving> getServingQueue(int hpc_site, string zone)
        {
            using (InhCheckupDataContext pdc = new InhCheckupDataContext())
            {
                return (from tpr in pdc.trn_patient_regis.Where(x => x.tpr_arrive_date.Value.Date == globalCls.GetServerDateTime().Date)
                        join tps in pdc.trn_patient_queues.Where(x => x.tps_status == "WK")
                        on tpr.tpr_id equals tps.tpr_id
                        join mrm in pdc.mst_room_hdrs.Where(x => x.mhs_id == hpc_site)
                        on tps.mrm_id equals mrm.mrm_id
                        join mrd in pdc.mst_room_dtls
                        on tps.mrd_id equals mrd.mrd_id
                        join mrs in pdc.mst_room_screens.Where(x => x.mhs_id == hpc_site && x.mze_code == zone && x.mrs_status == 'A')
                        on mrm.mrm_code equals mrs.mrm_code
                        join mvt in pdc.mst_events
                        on mrs.mvt_code equals mvt.mvt_code
                        where tps.mvt_id == mvt.mvt_id
                        orderby mrs.mrs_seq, tps.tps_start_date
                        select new
                        {
                            mrd_room_no = mrd.mrd_room_no,
                            mrm_scrn_thai = mrm.mrm_scrn_thai,
                            tpr_queue_no = tpr.tpr_queue_no,
                            tpt_vip_hpc = tpr.trn_patient.tpt_vip_hpc
                        }).Where(x => x.tpt_vip_hpc != true)
                        .Select(x => new output_queue_serving
                        {
                            room_no = x.mrd_room_no,
                            //room_code = mrm.mrm_scrn_thai,
                            room_code = x.mrm_scrn_thai,
                            queue_no = x.tpr_queue_no
                        }).ToList();
            }
        }

        private List<string> get_mrm_code(int hpc_site, string zone)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                var mrm_code = (from mrs in dbc.mst_room_screens
                                where mrs.mhs_id == hpc_site &&
                                mrs.mze_code == zone &&
                                mrs.mrs_status == 'A' &&
                                globalCls.GetServerDateTime().Date >= mrs.mrs_effective_date.Value.Date &&
                                globalCls.GetServerDateTime().Date <= (mrs.mrs_expire_date != null ? mrs.mrs_expire_date.Value.Date : globalCls.GetServerDateTime().Date)
                                select mrs.mrm_code).ToList();
                return mrm_code;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_room_queue> getHoldingQueue(int hpc_site, string zone)
        {
            using (InhCheckupDataContext pdc = new InhCheckupDataContext())
            {
                return (from tpr in pdc.trn_patient_regis.Where(x => x.tpr_arrive_date.Value.Date == globalCls.GetServerDateTime().Date)
                        join tps in pdc.trn_patient_queues.Where(x => x.tps_status == "NS" && x.tps_ns_status == "QL" && x.tps_call_status == "HD")
                        on tpr.tpr_id equals tps.tpr_id
                        join mrm in pdc.mst_room_hdrs.Where(x => x.mhs_id == hpc_site)
                        on tps.mrm_id equals mrm.mrm_id
                        join mvt in pdc.mst_events
                        on tps.mvt_id equals mvt.mvt_id
                        join mrs in pdc.mst_room_screens.Where(x => x.mhs_id == hpc_site && x.mze_code == zone && x.mrs_status == 'A'
                        && globalCls.GetServerDateTime().Date >= (x.mrs_effective_date == null ? globalCls.GetServerDateTime().Date : x.mrs_effective_date.Value.Date)
                        && globalCls.GetServerDateTime().Date <= (x.mrs_expire_date == null ? globalCls.GetServerDateTime().Date : x.mrs_expire_date.Value.Date))
                        on mrm.mrm_code equals mrs.mrm_code
                        where mvt.mvt_code == mrs.mvt_code
                        orderby mrs.mrs_seq, tps.tps_hold_date
                        group new { tpr, tps, mrm, mrs } by mrm.mrm_code into g
                        select new output_room_queue
                        {
                            room_code = g.Key,
                            queue_no = string.Join("   ", g.Select(x => x.tpr.tpr_queue_no).ToArray())
                        }).ToList();
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_room_queue> getWaitingDoctorRoom(int hpc_site, string zone)
        {
            List<output_room_queue> result = new List<output_room_queue>();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var mvt = cdc.mst_events
                                 .Where(x => x.mvt_code == "PE" || x.mvt_code == "DC")
                                 .Select(x => new
                                 {
                                     x.mvt_id,
                                     x.mvt_code
                                 }).ToList();

                    var rooms = cdc.mst_room_screens
                                   .Where(x => x.mze_code == zone &&
                                               x.mhs_id == hpc_site &&
                                               x.mrs_status == 'A')
                                   .Select(x => new
                                   {
                                       x.mrm_code
                                   }).Distinct().ToList();
                    foreach (var room in rooms)
                    {
                        var list_mrm_id = cdc.mst_room_hdrs
                                             .Where(x => x.mrm_code == room.mrm_code &&
                                                         x.mhs_id == hpc_site)
                                             .Select(x => x.mrm_id).ToList();
                        foreach (int mrm_id in list_mrm_id)
                        {
                            List<sp_get_waiting_room_hdrResult> spResult = cdc.sp_get_waiting_room_hdr(mrm_id).OrderBy(x => x.priority).ToList();
                            List<sp_get_waiting_room_hdrResult> filterSpResult = spResult.Where(x => x.tpt_vip_hpc != true && x.holded != true)
                                                                                         .Take(6).ToList();
                            foreach (var re in filterSpResult)
                            {
                                string eventname = "";
                                string mvt_code = mvt.Where(x => x.mvt_id == re.mvt_id).Select(x => x.mvt_code).FirstOrDefault();
                                if (mvt_code == "PE")
                                {
                                    eventname = "(PE)";
                                }
                                else if (mvt_code == "DC")
                                {
                                    eventname = "(Result)";
                                }
                                result.Add(new output_room_queue { queue_no = re.tpr_queue_no + " " + eventname, room_code = room.mrm_code });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WS_GetDataFromPathWay.asmx", "getWaitingDoctorRoom(int hpc_site, string zone)", ex.Message);
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_room_queue> getWithin1HourQueue(int hpc_site, string zone)
        {
            List<output_room_queue> result = new List<output_room_queue>();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var mvt = cdc.mst_events
                                 .Where(x => x.mvt_code == "PE" || x.mvt_code == "DC")
                                 .Select(x => new
                                 {
                                     x.mvt_id,
                                     x.mvt_code
                                 }).ToList();

                    var rooms = cdc.mst_room_screens
                                   .Where(x => x.mze_code == zone &&
                                               x.mhs_id == hpc_site &&
                                               x.mrs_status == 'A')
                                   .Select(x => new
                                   {
                                       x.mrm_code
                                   }).Distinct().ToList();
                    foreach (var room in rooms)
                    {
                        var list_mrm_id = cdc.mst_room_hdrs
                                             .Where(x => x.mrm_code == room.mrm_code &&
                                                         x.mhs_id == hpc_site)
                                             .Select(x => x.mrm_id).ToList();
                        foreach (int mrm_id in list_mrm_id)
                        {
                            List<sp_get_waiting_room_hdrResult> spResult = cdc.sp_get_waiting_room_hdr(mrm_id).OrderBy(x => x.priority).ToList();
                            List<sp_get_waiting_room_hdrResult> filterSpResult = spResult.Where(x => x.tpt_vip_hpc != true && x.holded != true)
                                                                                         .Skip(6).Take(10).ToList();
                            foreach (var re in filterSpResult)
                            {
                                string eventname = "";
                                string mvt_code = mvt.Where(x => x.mvt_id == re.mvt_id).Select(x => x.mvt_code).FirstOrDefault();
                                if (mvt_code == "PE")
                                {
                                    eventname = "(PE)";
                                }
                                else if (mvt_code == "DC")
                                {
                                    eventname = "(Result)";
                                }
                                result.Add(new output_room_queue { queue_no = re.tpr_queue_no + " " + eventname, room_code = room.mrm_code });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WS_GetDataFromPathWay.asmx", "getWithin1HourQueue(int hpc_site, string zone)", ex.Message);
            }
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_lang_call_queue> getLanguageCallQueue(int hpc_site, string zone)
        {
            using (InhCheckupDataContext pdc = new InhCheckupDataContext())
            {
                List<output_lang_call_queue> result = (from tpr in pdc.trn_patient_regis
                                                       join tpt in pdc.trn_patients
                                                       on tpr.tpt_id equals tpt.tpt_id
                                                       join tps in pdc.trn_patient_queues
                                                       on tpr.tpr_id equals tps.tpr_id
                                                       join mrm in pdc.mst_room_hdrs
                                                       on tps.mrm_id equals mrm.mrm_id
                                                       join mrd in pdc.mst_room_dtls
                                                       on tps.mrd_id equals mrd.mrd_id
                                                       where tps.tps_status == "NS" &&
                                                       tpr.tpr_arrive_date.Value.Date == globalCls.GetServerDateTime().Date &&
                                                       get_mrm_code(hpc_site, zone).Contains(mrm.mrm_code)
                                                       select new output_lang_call_queue
                                                       {
                                                           nation = tpt.tpt_nation_code,
                                                           room_no = mrd.mrd_room_no,
                                                           queue_no = tpr.tpr_queue_no,
                                                           hpc_site = tpr.mhs_id.ToString()
                                                       }).ToList();
                return result;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<out_put_display_profile> getDisplayProfile(string p_first_name, string p_last_name, string p_hn_no, string p_dob_date)
        {
            string first_name = string.IsNullOrEmpty(p_first_name.Trim()) ? null : p_first_name.Trim().ToUpper();
            string last_name = string.IsNullOrEmpty(p_last_name.Trim()) ? null : p_last_name.Trim().ToUpper();
            string hn_no = string.IsNullOrEmpty(p_hn_no.Trim()) ? null : p_hn_no.Trim().ToUpper();
            DateTime? dob = getOnlyDate(p_dob_date.convParamGetResultToDate());

            using (InhCheckupDataContext pdc = new InhCheckupDataContext())
            {
                List<out_put_display_profile> result = new List<out_put_display_profile>();

                result = null;
                if (first_name != null || last_name != null || hn_no != null || dob != null)
                {
                    result = (from tpt in pdc.trn_patients
                              where tpt.tpt_first_name.ToUpper() == (first_name != null ? first_name : tpt.tpt_first_name.ToUpper()) &&
                                    tpt.tpt_last_name.ToUpper() == (last_name != null ? last_name : tpt.tpt_last_name.ToUpper()) &&
                                    tpt.tpt_hn_no.ToUpper() == (hn_no != null ? hn_no : tpt.tpt_hn_no.ToUpper()) &&
                                    tpt.tpt_dob == (dob.HasValue ? dob : tpt.tpt_dob)
                              select new out_put_display_profile
                              {
                                  p_hn_no = tpt.tpt_hn_no,
                                  p_en_no = tpt.trn_patient_regis.OrderByDescending(x => x.tpr_arrive_date).FirstOrDefault().tpr_en_no,
                                  p_name_th = tpt.tpt_othername,
                                  p_name_en = tpt.tpt_othername,//tpt.tpt_en_name1 + " " + tpt.tpt_en_name2 + " " + tpt.tpt_en_name3,
                                  p_gender = tpt.tpt_gender.ToString(),
                                  p_dob_date = getOnlyDate(tpt.tpt_dob),
                                  p_visit_date = tpt.trn_patient_regis.OrderByDescending(x => x.tpr_arrive_date).FirstOrDefault().tpr_arrive_date,
                                  p_age_en = pdc.func_get_age(tpt.tpt_dob, DateTime.Today).FirstOrDefault().age_ymd_long,
                                  p_allergy = pdc.func_get_patient_information(tpt.trn_patient_regis.OrderByDescending(x => x.tpr_arrive_date).FirstOrDefault().tpr_id).FirstOrDefault().tpt_allergy,
                                  p_image = (byte[])tpt.tpt_image.ToArray(),
                                  p_avia_flag = tpt.trn_patient_regis.OrderByDescending(x => x.tpr_arrive_date).FirstOrDefault().tpr_patient_type == '2' ? (byte)1 : (byte)0,
                                  p_married = tpt.tpt_married.ToString(),
                                  p_age_th = pdc.func_get_age(tpt.tpt_dob, DateTime.Today).FirstOrDefault().age_ymd_thai,
                                  p_address = pdc.func_get_patient_information(tpt.trn_patient_regis.OrderByDescending(x => x.tpr_arrive_date).FirstOrDefault().tpr_id).FirstOrDefault().tpt_address,
                                  p_telephone = tpt.trn_patient_regis.OrderByDescending(x => x.tpr_arrive_date).FirstOrDefault().tpr_mobile_phone,
                                  p_nation = tpt.tpt_nation_desc,
                                  p_regis_type = true
                              }).Take(100).ToList();

                    if (result.Count == 0)
                    {
                        result = (from tga in pdc.tmp_getptarriveds
                                  where tga.papmi_name.ToUpper() == (first_name != null ? first_name : tga.papmi_name.ToUpper()) &&
                                        tga.papmi_name2.ToUpper() == (last_name != null ? last_name : tga.papmi_name2.ToUpper()) &&
                                        tga.papmi_no.ToUpper() == (hn_no != null ? hn_no : tga.papmi_no.ToUpper()) &&
                                        tga.papmi_dob == (dob.HasValue ? dob : tga.papmi_dob)
                                  group tga by tga.papmi_no into grp
                                  select grp.OrderByDescending(g => g.appt_transdate).First()).ToList()
                                  .Select(x => new out_put_display_profile
                                  {
                                      p_hn_no = x.papmi_no,
                                      p_en_no = x.paadm_admno,
                                      p_name_th = x.ttl_desc + x.papmi_name + " " + x.papmi_name2,
                                      p_name_en = convFullName(x.paper_name5, x.paper_name6, x.paper_name7),
                                      p_gender = x.ctsex_code.ToString(),
                                      p_dob_date = getOnlyDate(x.papmi_dob),
                                      p_visit_date = convToDateTime(x.appt_transdate, x.appt_arrivaltime),
                                      p_age_en = pdc.func_get_age(x.papmi_dob, DateTime.Today).FirstOrDefault().age_ymd_long,
                                      p_allergy = ((string.IsNullOrEmpty(x.allergy_eng)) ? "No Known Allergy" : x.allergy_eng),
                                      p_image = (byte[])x.paper_photo.ToArray(),
                                      p_avia_flag = 0,
                                      p_married = convMarried(x.ctmar_desc),
                                      p_age_th = pdc.func_get_age(x.papmi_dob, DateTime.Today).FirstOrDefault().age_ymd_thai,
                                      p_address = convAddress(x.paper_stname, x.citarea_desc, x.ctcit_desc, x.prov_desc, x.ctzip_code),
                                      p_telephone = x.paper_mobphone,
                                      p_nation = x.ctnat_desc,
                                      p_regis_type = false
                                  }).ToList();
                    }
                }
                return result;
            }
        }
        private string convFullName(string title, string name, string lastname)
        {
            string[] temp = new string[] { title, name, lastname };
            string fullname = string.Join(" ", temp.Where(x => !string.IsNullOrEmpty(x)).ToArray());
            return string.IsNullOrEmpty(fullname) ? null : fullname;
        }
        private string convAddress(string stname, string area, string city, string prov, string zip)
        {
            string address = "";
            if (!string.IsNullOrEmpty(stname.Trim()))
            {
                address = address + stname.Trim();
            }
            if (!string.IsNullOrEmpty(area.Trim()))
            {
                address = address + " ต." + area.Trim();
            }
            if (!string.IsNullOrEmpty(city.Trim()))
            {
                address = address + " อ." + city.Trim();
            }
            if (!string.IsNullOrEmpty(prov.Trim()))
            {
                address = address + " จ." + prov.Trim();
            }
            if (!string.IsNullOrEmpty(zip.Trim()))
            {
                address = address + " " + zip.Trim();
            }
            return string.IsNullOrEmpty(address.Trim()) ? null : address.Trim();
        }
        private DateTime? convToDateTime(DateTime? date, string time)
        {
            try
            {
                DateTime _date = Convert.ToDateTime(date);
                string dateTime = String.Format("{0:dd/MM/yyyy}", _date.Date) + " " + time;
                return Convert.ToDateTime(dateTime);
            }
            catch
            {

            }
            return null;
        }
        private string convMarried(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                int startindex = value.IndexOf('(') == 0 ? 0 : value.IndexOf('(') + 1;
                int length = value.IndexOf(')') == 0 ? 0 : value.IndexOf(')') - startindex;
                string status = value.Substring(startindex, length);
                switch (status.ToUpper())
                {
                    case "SINGLE": return "S";
                    case "MARRIED": return "M";
                    case "WIDOWED": return "W";
                    case "DIVORCED": return "D";
                    case "UNKNOWN": return "U";
                    default: return null;
                };
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public out_put_status insertQuestionairePatiant(questionaire_patiant input_questionaire_patiant)
        {
            using (InhCheckupDataContext pdc = new InhCheckupDataContext())
            {
                out_put_status result = new out_put_status();

                result.success_flag = "Y";
                result.error_name = "SUCCESS";
                try
                {
                    int? tpr_id = return_tpr_id_by_hn(input_questionaire_patiant.p_hn_no);
                    if (tpr_id != null)
                    {
                        trn_ques_patient tqp = pdc.trn_ques_patients.Where(x => x.tpr_id == tpr_id).OrderByDescending(x => x.tqp_update_date).FirstOrDefault();
                        Boolean newRecord = false;
                        if (tqp == null)
                        {
                            tqp = new trn_ques_patient();
                            newRecord = true;
                        }
                        tqp.tpr_id = Convert.ToInt32(tpr_id);
                        tqp.tqp_type = input_questionaire_patiant.p_type;
                        tqp.tqp_his_smok = input_questionaire_patiant.p_his_smok;
                        tqp.tqp_his_nsmok_yrs = input_questionaire_patiant.p_his_nsmok_yrs;
                        tqp.tqp_his_qsmok_yrs = input_questionaire_patiant.p_his_qsmok_yrs;
                        tqp.tqp_his_smok_amt = input_questionaire_patiant.p_his_smok_amt;
                        tqp.tqp_his_smok_dur = input_questionaire_patiant.p_his_smok_dur;
                        tqp.tqp_his_alcohol = input_questionaire_patiant.p_his_alcohol;
                        tqp.tqp_his_alco_yrs = input_questionaire_patiant.p_his_alco_yrs;
                        tqp.tqp_his_alco_social = input_questionaire_patiant.p_his_alco_social;
                        tqp.tqp_his_exercise = input_questionaire_patiant.p_his_exercise;
                        tqp.tqp_ill_concern = input_questionaire_patiant.p_ill_concern;
                        tqp.tqp_ill_conc_oth = input_questionaire_patiant.p_ill_conc_oth;
                        tqp.tqp_ill_psycho = input_questionaire_patiant.p_ill_psycho;
                        tqp.tqp_ill_psycho_oth = input_questionaire_patiant.p_ill_psycho_oth;
                        tqp.tqp_ill_med_his = input_questionaire_patiant.p_ill_med_his;
                        tqp.tqp_ill_med_hyper = input_questionaire_patiant.p_ill_med_hyper;
                        tqp.tqp_ill_med_heart = input_questionaire_patiant.p_ill_med_heart;
                        tqp.tqp_ill_med_diab = input_questionaire_patiant.p_ill_med_diab;
                        tqp.tqp_ill_med_coro = input_questionaire_patiant.p_ill_med_coro;
                        tqp.tqp_ill_med_dysl = input_questionaire_patiant.p_ill_med_dysl;
                        tqp.tqp_ill_med_cper = input_questionaire_patiant.p_ill_med_cper;
                        tqp.tqp_ill_med_gout = input_questionaire_patiant.p_ill_med_gout;
                        tqp.tqp_ill_med_abdd = input_questionaire_patiant.p_ill_med_abdd;
                        tqp.tqp_ill_med_pulm = input_questionaire_patiant.p_ill_med_pulm;
                        tqp.tqp_ill_med_para = input_questionaire_patiant.p_ill_med_para;
                        tqp.tqp_ill_med_stro = input_questionaire_patiant.p_ill_med_stro;
                        tqp.tqp_ill_med_putb = input_questionaire_patiant.p_ill_med_putb;
                        tqp.tqp_ill_med_sist = input_questionaire_patiant.p_ill_med_sist;
                        tqp.tqp_ill_med_kidn = input_questionaire_patiant.p_ill_med_kidn;
                        tqp.tqp_ill_med_epil = input_questionaire_patiant.p_ill_med_epil;
                        tqp.tqp_ill_med_resp = input_questionaire_patiant.p_ill_med_resp;
                        tqp.tqp_ill_med_asth = input_questionaire_patiant.p_ill_med_asth;
                        tqp.tqp_ill_med_emph = input_questionaire_patiant.p_ill_med_emph;
                        tqp.tqp_ill_med_chro = input_questionaire_patiant.p_ill_med_chro;
                        tqp.tqp_ill_med_canc = input_questionaire_patiant.p_ill_med_canc;
                        tqp.tqp_ill_med_canc_oth = input_questionaire_patiant.p_ill_med_canc_oth;
                        tqp.tqp_ill_med_alle = input_questionaire_patiant.p_ill_med_alle;
                        tqp.tqp_ill_med_pept = input_questionaire_patiant.p_ill_med_pept;
                        tqp.tqp_ill_med_oth = input_questionaire_patiant.p_ill_med_oth;
                        tqp.tqp_ill_med_others = input_questionaire_patiant.p_ill_med_others;
                        tqp.tqp_ill_cur_med = input_questionaire_patiant.p_ill_cur_med;
                        tqp.tqp_ill_cmed_diab = input_questionaire_patiant.p_ill_cmed_diab;
                        tqp.tqp_ill_cmed_hyper = input_questionaire_patiant.p_ill_cmed_hyper;
                        tqp.tqp_ill_cmed_demia = input_questionaire_patiant.p_ill_cmed_demia;
                        tqp.tqp_ill_cmed_cardi = input_questionaire_patiant.p_ill_cmed_cardi;
                        tqp.tqp_ill_cmed_dysl = input_questionaire_patiant.p_ill_cmed_dysl;
                        tqp.tqp_ill_cmed_horm = input_questionaire_patiant.p_ill_cmed_horm;
                        tqp.tqp_ill_cmed_oth = input_questionaire_patiant.p_ill_cmed_oth;
                        tqp.tqp_ill_cmed_others = input_questionaire_patiant.p_ill_cmed_others;
                        tqp.tqp_ill_allergy = input_questionaire_patiant.p_ill_allergy;
                        tqp.tqp_ill_drug_or_food = input_questionaire_patiant.p_ill_drug_or_food;
                        tqp.tqp_pill_adm = input_questionaire_patiant.p_pill_adm;
                        tqp.tqp_pill_admission = input_questionaire_patiant.p_pill_admission;
                        tqp.tqp_pill_sur = input_questionaire_patiant.p_pill_sur;
                        tqp.tqp_pill_surgery = input_questionaire_patiant.p_pill_surgery;
                        tqp.tqp_vinf_hepB_virus = input_questionaire_patiant.p_vinf_hepB_virus;
                        tqp.tqp_vinf_hepA_virus = input_questionaire_patiant.p_vinf_hepA_virus;
                        tqp.tqp_vinf_vaccine = input_questionaire_patiant.p_vinf_vaccine;
                        tqp.tqp_fhis_f_disease = input_questionaire_patiant.p_fhis_f_disease;
                        tqp.tqp_fhis_fdis_hyper = input_questionaire_patiant.p_fhis_fdis_hyper;
                        tqp.tqp_fhis_fdis_heart = input_questionaire_patiant.p_fhis_fdis_heart;
                        tqp.tqp_fhis_fdis_diab = input_questionaire_patiant.p_fhis_fdis_diab;
                        tqp.tqp_fhis_fdis_coro = input_questionaire_patiant.p_fhis_fdis_coro;
                        tqp.tqp_fhis_fdis_coro_cs = input_questionaire_patiant.p_fhis_fdis_coro_cs;
                        tqp.tqp_fhis_fdis_dysl = input_questionaire_patiant.p_fhis_fdis_dysl;
                        tqp.tqp_fhis_fdis_gout = input_questionaire_patiant.p_fhis_fdis_gout;
                        tqp.tqp_fhis_fdis_pulm = input_questionaire_patiant.p_fhis_fdis_pulm;
                        tqp.tqp_fhis_fdis_para = input_questionaire_patiant.p_fhis_fdis_para;
                        tqp.tqp_fhis_fdis_putb = input_questionaire_patiant.p_fhis_fdis_putb;
                        tqp.tqp_fhis_fdis_stro = input_questionaire_patiant.p_fhis_fdis_stro;
                        tqp.tqp_fhis_fdis_pepu = input_questionaire_patiant.p_fhis_fdis_pepu;
                        tqp.tqp_fhis_fdis_asth = input_questionaire_patiant.p_fhis_fdis_asth;
                        tqp.tqp_fhis_fdis_alle = input_questionaire_patiant.p_fhis_fdis_alle;
                        tqp.tqp_fhis_fdis_canc = input_questionaire_patiant.p_fhis_fdis_canc;
                        tqp.tqp_fhis_fdis_canc_rmk = input_questionaire_patiant.p_fhis_fdis_canc_rmk;
                        tqp.tqp_fhis_fdis_oth = input_questionaire_patiant.p_fhis_fdis_oth;
                        tqp.tqp_fhis_fdis_others = input_questionaire_patiant.p_fhis_fdis_others;
                        tqp.tqp_fhis_m_disease = input_questionaire_patiant.p_fhis_m_disease;
                        tqp.tqp_fhis_mdis_hyper = input_questionaire_patiant.p_fhis_mdis_hyper;
                        tqp.tqp_fhis_mdis_heart = input_questionaire_patiant.p_fhis_mdis_heart;
                        tqp.tqp_fhis_mdis_diab = input_questionaire_patiant.p_fhis_mdis_diab;
                        tqp.tqp_fhis_mdis_coro = input_questionaire_patiant.p_fhis_mdis_coro;
                        tqp.tqp_fhis_mdis_coro_cs = input_questionaire_patiant.p_fhis_mdis_coro_cs;
                        tqp.tqp_fhis_mdis_dysl = input_questionaire_patiant.p_fhis_mdis_dysl;
                        tqp.tqp_fhis_mdis_pulm = input_questionaire_patiant.p_fhis_mdis_pulm;
                        tqp.tqp_fhis_mdis_gout = input_questionaire_patiant.p_fhis_mdis_gout;
                        tqp.tqp_fhis_mdis_para = input_questionaire_patiant.p_fhis_mdis_para;
                        tqp.tqp_fhis_mdis_putb = input_questionaire_patiant.p_fhis_mdis_putb;
                        tqp.tqp_fhis_mdis_stro = input_questionaire_patiant.p_fhis_mdis_stro;
                        tqp.tqp_fhis_mdis_pepu = input_questionaire_patiant.p_fhis_mdis_pepu;
                        tqp.tqp_fhis_mdis_asth = input_questionaire_patiant.p_fhis_mdis_asth;
                        tqp.tqp_fhis_mdis_alle = input_questionaire_patiant.p_fhis_mdis_alle;
                        tqp.tqp_fhis_mdis_canc = input_questionaire_patiant.p_fhis_mdis_canc;
                        tqp.tqp_fhis_mdis_canc_rmk = input_questionaire_patiant.p_fhis_mdis_canc_rmk;
                        tqp.tqp_fhis_mdis_oth = input_questionaire_patiant.p_fhis_mdis_oth;
                        tqp.tqp_fhis_mdis_others = input_questionaire_patiant.p_fhis_mdis_others;
                        tqp.tqp_fhis_b_disease = input_questionaire_patiant.p_fhis_b_disease;
                        tqp.tqp_fhis_bdis_hyper = input_questionaire_patiant.p_fhis_bdis_hyper;
                        tqp.tqp_fhis_bdis_heart = input_questionaire_patiant.p_fhis_bdis_heart;
                        tqp.tqp_fhis_bdis_diab = input_questionaire_patiant.p_fhis_bdis_diab;
                        tqp.tqp_fhis_bdis_coro = input_questionaire_patiant.p_fhis_bdis_coro;
                        tqp.tqp_fhis_bdis_coro_bfm = input_questionaire_patiant.p_fhis_bdis_coro_bfm;
                        tqp.tqp_fhis_bdis_coro_afm = input_questionaire_patiant.p_fhis_bdis_coro_afm;
                        tqp.tqp_fhis_bdis_coro_nfm = input_questionaire_patiant.p_fhis_bdis_coro_nfm;
                        tqp.tqp_fhis_bdis_coro_bm = input_questionaire_patiant.p_fhis_bdis_coro_bm;
                        tqp.tqp_fhis_bdis_coro_am = input_questionaire_patiant.p_fhis_bdis_coro_am;
                        tqp.tqp_fhis_bdis_coro_nm = input_questionaire_patiant.p_fhis_bdis_coro_nm;
                        tqp.tqp_fhis_bdis_dysl = input_questionaire_patiant.p_fhis_bdis_dysl;
                        tqp.tqp_fhis_bdis_gout = input_questionaire_patiant.p_fhis_bdis_gout;
                        tqp.tqp_fhis_bdis_pulm = input_questionaire_patiant.p_fhis_bdis_pulm;
                        tqp.tqp_fhis_bdis_para = input_questionaire_patiant.p_fhis_bdis_para;
                        tqp.tqp_fhis_bdis_putb = input_questionaire_patiant.p_fhis_bdis_putb;
                        tqp.tqp_fhis_bdis_stro = input_questionaire_patiant.p_fhis_bdis_stro;
                        tqp.tqp_fhis_bdis_pepu = input_questionaire_patiant.p_fhis_bdis_pepu;
                        tqp.tqp_fhis_bdis_asth = input_questionaire_patiant.p_fhis_bdis_asth;
                        tqp.tqp_fhis_bdis_alle = input_questionaire_patiant.p_fhis_bdis_alle;
                        tqp.tqp_fhis_bdis_canc = input_questionaire_patiant.p_fhis_bdis_canc;
                        tqp.tqp_fhis_bdis_canc_rmk = input_questionaire_patiant.p_fhis_bdis_canc_rmk;
                        tqp.tqp_fhis_bdis_oth = input_questionaire_patiant.p_fhis_bdis_oth;
                        tqp.tqp_fhis_bdis_others = input_questionaire_patiant.p_fhis_bdis_others;
                        tqp.tqp_fhis_others = input_questionaire_patiant.p_fhis_others;
                        tqp.tqp_fwm_menopause = input_questionaire_patiant.p_fwm_menopause;
                        tqp.tqp_fwm_meno_start = input_questionaire_patiant.p_fwm_meno_start;
                        tqp.tqp_fwm_lst_st_mens = input_questionaire_patiant.p_fwm_lst_st_mens;
                        tqp.tqp_fwm_lst_ed_mens = input_questionaire_patiant.p_fwm_lst_ed_mens;
                        tqp.tqp_fwm_character = input_questionaire_patiant.p_fwm_character;
                        tqp.tqp_fwm_pregnancy = input_questionaire_patiant.p_fwm_pregnancy;
                        tqp.tqp_fwm_over_weight = input_questionaire_patiant.p_fwm_over_weight;

                        if (input_questionaire_patiant.p_license != null)
                        {
                            tqp.tqp_signature = MakeMetafileStream(input_questionaire_patiant.p_license).ToArray();
                        }

                        tqp.tqp_update_date = globalCls.GetServerDateTime();
                        tqp.tqp_update_by = "System";
                        if (newRecord == true)
                        {
                            tqp.tqp_create_date = globalCls.GetServerDateTime();
                            tqp.tqp_create_by = "System";
                            pdc.trn_ques_patients.InsertOnSubmit(tqp);
                        }
                        pdc.SubmitChanges();
                    }
                    else
                    {
                        result.success_flag = "N";
                        result.error_name = "Not Find HN No. (" + input_questionaire_patiant.p_hn_no + ")";
                    }
                }
                catch (Exception ex)
                {
                    result.success_flag = "N";
                    result.error_name = ex.Message;
                    globalCls.MessageError("WS_Checkup", "insertQuestionairePatient", ex.Message);
                }
                return result;
            }
        }
        private MemoryStream MakeMetafileStream(byte[] image)
        {
            Graphics graphics = null;
            Metafile metafile = null;
            MemoryStream ms = new MemoryStream(image);
            Image returnImage = Image.FromStream(ms);
            var stream = new MemoryStream();
            try
            {
                using (graphics = Graphics.FromImage(returnImage))
                {
                    var hdc = graphics.GetHdc();
                    metafile = new Metafile(stream, hdc);
                    graphics.ReleaseHdc(hdc);
                }
                using (graphics = Graphics.FromImage(metafile))
                { graphics.DrawImage(returnImage, 0, 0); }
            }
            finally
            {
                if (graphics != null)
                { graphics.Dispose(); }
                if (metafile != null)
                { metafile.Dispose(); }
            }
            return stream;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public out_put_status insertQuestionaireAviation(questionaire_aviation input_questionaire_aviation)
        {
            using (InhCheckupDataContext pdc = new InhCheckupDataContext())
            {
                out_put_status result = new out_put_status();

                result.success_flag = "Y";
                result.error_name = "SUCCESS";
                try
                {
                    int? tpr_id = return_tpr_id_by_hn(input_questionaire_aviation.p_hn_no);
                    if (tpr_id != null)
                    {
                        trn_patient tpt = pdc.trn_patient_regis.Where(x => x.tpr_id == Convert.ToInt32(tpr_id)).FirstOrDefault().trn_patient;
                        string th_fullname = tpt.tpt_first_name + " " + tpt.tpt_last_name;
                        string nation = tpt.tpt_nation_desc;
                        double age_year = Convert.ToDouble(pdc.func_get_age(tpt.tpt_dob, DateTime.Today).FirstOrDefault().age_num_y);
                        double age_month = Convert.ToDouble(pdc.func_get_age(tpt.tpt_dob, DateTime.Today).FirstOrDefault().age_num_m);
                        trn_ques_aviation tqa = pdc.trn_ques_aviations.Where(x => x.tpr_id == tpr_id).OrderByDescending(x => x.tqa_update_date).FirstOrDefault();
                        Boolean newRecord = false;
                        if (tqa == null)
                        {
                            tqa = new trn_ques_aviation();
                            newRecord = true;
                        }
                        tqa.tpr_id = Convert.ToInt32(tpr_id);
                        tqa.tqa_type = input_questionaire_aviation.p_type;
                        tqa.tqa_doc_type = input_questionaire_aviation.p_doc_type;
                        tqa.tqa_en_fullname = input_questionaire_aviation.p_nameEN;
                        tqa.tqa_marital = input_questionaire_aviation.p_marital;
                        tqa.tqa_place_exam = input_questionaire_aviation.p_place_exam;
                        tqa.tqa_th_fullname = th_fullname;
                        tqa.tqa_th_nation = nation;
                        tqa.tqa_license_type = input_questionaire_aviation.p_license_type;
                        tqa.tqa_en_nation = nation;
                        tqa.tqa_sex = tpt.tpt_gender;
                        tqa.tqa_dob = tpt.tpt_dob;
                        tqa.tqa_age_yrs = age_year;
                        tqa.tqa_age_month = age_month;
                        tqa.tqa_avia_type = input_questionaire_aviation.p_avia_type;
                        tqa.tqa_avia_oths = input_questionaire_aviation.p_avia_oths;
                        tqa.tqa_license_no = input_questionaire_aviation.p_license_no;
                        tqa.tqa_chge_address = input_questionaire_aviation.p_chge_address;
                        tqa.tqa_th_address = input_questionaire_aviation.p_th_address;
                        tqa.tqa_th_moblie = input_questionaire_aviation.p_th_mobile;
                        tqa.tqa_en_address = input_questionaire_aviation.p_en_address;
                        tqa.tqa_en_mobile = input_questionaire_aviation.p_en_mobile;
                        tqa.tqa_th_occupa = input_questionaire_aviation.p_th_occupa;
                        tqa.tqa_th_comp = input_questionaire_aviation.p_th_comp;
                        tqa.tqa_th_office = input_questionaire_aviation.p_th_office;
                        tqa.tqa_th_of_mobile = input_questionaire_aviation.p_th_of_mobile;
                        tqa.tqa_cont_address = input_questionaire_aviation.p_cont_address;
                        tqa.tqa_person_emer = input_questionaire_aviation.p_person_emer;
                        tqa.tqa_telep_emer = input_questionaire_aviation.p_telep_emer;
                        tqa.tqa_prev_examined = input_questionaire_aviation.p_prev_examined;
                        tqa.tqa_prev_exam_loc = input_questionaire_aviation.p_prev_exam_loc;
                        tqa.tqa_prev_exam_date = input_questionaire_aviation.p_prev_exam_date;
                        tqa.tqa_prev_exam_deca = input_questionaire_aviation.p_prev_exam_deca;
                        tqa.tqa_med_waiver = input_questionaire_aviation.p_med_waiver;
                        tqa.tqa_waiver_spec = input_questionaire_aviation.p_waiver_spec;
                        tqa.tqa_tot_fling_time = input_questionaire_aviation.p_tot_fling_time;
                        tqa.tqa_last_six_time = input_questionaire_aviation.p_last_six_time;
                        tqa.tqa_pres_aircraft = input_questionaire_aviation.p_pres_aircraft;
                        tqa.tqa_aircraft_type = input_questionaire_aviation.p_aircraft_type;
                        tqa.tqa_aircraft_name = input_questionaire_aviation.p_aircraft_name;
                        tqa.tqa_aircraft_jet = input_questionaire_aviation.p_aircraft_jet;
                        tqa.tqa_aircraft_turbo = input_questionaire_aviation.p_aircraft_turbo;
                        tqa.tqa_aircraft_heli = input_questionaire_aviation.p_aircraft_heli;
                        tqa.tqa_aircraft_piston = input_questionaire_aviation.p_aircraft_piston;
                        tqa.tqa_aircraft_other = input_questionaire_aviation.p_aircraft_other;
                        tqa.tqa_aircraft_oth = input_questionaire_aviation.p_aircraft_oth;
                        tqa.tqa_flying_status = input_questionaire_aviation.p_flying_status;
                        tqa.tqa_single_pilot = input_questionaire_aviation.p_single_pilot;
                        tqa.tqa_muti_pilot = input_questionaire_aviation.p_muti_pilot;
                        tqa.tqa_smoking = input_questionaire_aviation.p_smoking;
                        tqa.tqa_smoking_since = input_questionaire_aviation.p_smoking_since;
                        tqa.tqa_smoking_type = input_questionaire_aviation.p_smoking_type;
                        tqa.tqa_smoking_amt = input_questionaire_aviation.p_smoking_amt;
                        tqa.tqa_use_medicine = input_questionaire_aviation.p_use_medicine;
                        tqa.tqa_med_name = input_questionaire_aviation.tqa_med_name;
                        tqa.tqa_med_amount = input_questionaire_aviation.tqa_med_amount;
                        tqa.tqa_med_startdate = input_questionaire_aviation.tqa_med_startdate;
                        tqa.tqa_med_reason = input_questionaire_aviation.tqa_med_reason;
                        // med 2
                        tqa.tqa_med_name2 = input_questionaire_aviation.tqa_med_name2;
                        tqa.tqa_med_amount2 = input_questionaire_aviation.tqa_med_amount2;
                        tqa.tqa_med_startdate2 = input_questionaire_aviation.tqa_med_startdate2;
                        tqa.tqa_med_reason2 = input_questionaire_aviation.tqa_med_reason2;
                        // med 3
                        tqa.tqa_med_name3 = input_questionaire_aviation.tqa_med_name3;
                        tqa.tqa_med_amount3 = input_questionaire_aviation.tqa_med_amount3;
                        tqa.tqa_med_startdate3 = input_questionaire_aviation.tqa_med_startdate3;
                        tqa.tqa_med_reason3 = input_questionaire_aviation.tqa_med_reason3;

                        tqa.tqa_avg_alcohal = input_questionaire_aviation.p_avg_alcohal;
                        tqa.tqa_m20_exercise = input_questionaire_aviation.p_m20_exercise;
                        tqa.tqa_chis_freq = input_questionaire_aviation.p_chis_freq;
                        tqa.tqa_chis_freq_rmk = input_questionaire_aviation.p_chis_freq_rmk;
                        tqa.tqa_chis_dizz = input_questionaire_aviation.p_chis_dizz;
                        tqa.tqa_chis_dizz_rmk = input_questionaire_aviation.p_chis_dizz_rmk;
                        tqa.tqa_chis_unco = input_questionaire_aviation.p_chis_unco;
                        tqa.tqa_chis_unco_rmk = input_questionaire_aviation.p_chis_unco_rmk;
                        tqa.tqa_chis_eyet = input_questionaire_aviation.p_chis_eyet;
                        tqa.tqa_chis_eyet_rmk = input_questionaire_aviation.p_chis_eyet_rmk;
                        tqa.tqa_chis_hayf = input_questionaire_aviation.p_chis_hayf;
                        tqa.tqa_chis_hayf_rmk = input_questionaire_aviation.p_chis_hayf_rmk;
                        tqa.tqa_chis_hert = input_questionaire_aviation.p_chis_hert;
                        tqa.tqa_chis_hert_rmk = input_questionaire_aviation.p_chis_hert_rmk;
                        tqa.tqa_chis_chst = input_questionaire_aviation.p_chis_chst;
                        tqa.tqa_chis_chst_rmk = input_questionaire_aviation.p_chis_chst_rmk;
                        tqa.tqa_chis_high = input_questionaire_aviation.p_chis_high;
                        tqa.tqa_chis_high_rmk = input_questionaire_aviation.p_chis_high_rmk;
                        tqa.tqa_chis_stom = input_questionaire_aviation.p_chis_stom;
                        tqa.tqa_chis_stom_rmk = input_questionaire_aviation.p_chis_stom_rmk;
                        tqa.tqa_chis_jaun = input_questionaire_aviation.p_chis_jaun;
                        tqa.tqa_chis_jaun_rmk = input_questionaire_aviation.p_chis_jaun_rmk;
                        tqa.tqa_chis_kidn = input_questionaire_aviation.p_chis_kidn;
                        tqa.tqa_chis_kidn_rmk = input_questionaire_aviation.p_chis_kidn_rmk;
                        tqa.tqa_chis_suga = input_questionaire_aviation.p_chis_suga;
                        tqa.tqa_chis_suga_rmk = input_questionaire_aviation.p_chis_suga_rmk;
                        tqa.tqa_chis_epil = input_questionaire_aviation.p_chis_epil;
                        tqa.tqa_chis_epil_rmk = input_questionaire_aviation.p_chis_epil_rmk;
                        tqa.tqa_chis_nurv = input_questionaire_aviation.p_chis_nurv;
                        tqa.tqa_chis_nurv_rmk = input_questionaire_aviation.p_chis_nurv_rmk;
                        tqa.tqa_chis_temp = input_questionaire_aviation.p_chis_temp;
                        tqa.tqa_chis_temp_rmk = input_questionaire_aviation.p_chis_temp_rmk;
                        tqa.tqa_chis_drug = input_questionaire_aviation.p_chis_drug;
                        tqa.tqa_chis_drug_rmk = input_questionaire_aviation.p_chis_drug_rmk;
                        tqa.tqa_chis_suic = input_questionaire_aviation.p_chis_suic;
                        tqa.tqa_chis_suic_rmk = input_questionaire_aviation.p_chis_suic_rmk;
                        tqa.tqa_chis_losw = input_questionaire_aviation.p_chis_losw;
                        tqa.tqa_chis_losw_rmk = input_questionaire_aviation.p_chis_losw_rmk;
                        tqa.tqa_chis_moti = input_questionaire_aviation.p_chis_moti;
                        tqa.tqa_chis_moti_rmk = input_questionaire_aviation.p_chis_moti_rmk;
                        tqa.tqa_chis_reje = input_questionaire_aviation.p_chis_reje;
                        tqa.tqa_chis_reje_rmk = input_questionaire_aviation.p_chis_reje_rmk;
                        tqa.tqa_chis_adms = input_questionaire_aviation.p_chis_adms;
                        tqa.tqa_chis_adms_rmk = input_questionaire_aviation.p_chis_adms_rmk;
                        tqa.tqa_chis_avia = input_questionaire_aviation.p_chis_avia;
                        tqa.tqa_chis_avia_rmk = input_questionaire_aviation.p_chis_avia_rmk;
                        tqa.tqa_chis_otha = input_questionaire_aviation.p_chis_otha;
                        tqa.tqa_chis_otha_rmk = input_questionaire_aviation.p_chis_otha_rmk;
                        tqa.tqa_chis_gyna = input_questionaire_aviation.p_chis_gyna;
                        tqa.tqa_chis_gyna_rmk = input_questionaire_aviation.p_chis_gyna_rmk;
                        tqa.tqa_chis_othi = input_questionaire_aviation.p_chis_othi;
                        tqa.tqa_chis_othi_rmk = input_questionaire_aviation.p_chis_othi_rmk;
                        tqa.tqa_chis_heth = input_questionaire_aviation.p_chis_heth;
                        tqa.tqa_chis_heth_rmk = input_questionaire_aviation.p_chis_heth_rmk;
                        tqa.tqa_chis_lung = input_questionaire_aviation.p_chis_lung;
                        tqa.tqa_chis_lung_rmk = input_questionaire_aviation.p_chis_lung_rmk;
                        tqa.tqa_chis_alco = input_questionaire_aviation.p_chis_alco;
                        tqa.tqa_chis_alco_rmk = input_questionaire_aviation.p_chis_alco_rmk;
                        tqa.tqa_chis_ment = input_questionaire_aviation.p_chis_ment;
                        tqa.tqa_chis_ment_rmk = input_questionaire_aviation.p_chis_ment_rmk;
                        tqa.tqa_chis_fam_diab = input_questionaire_aviation.p_chis_fam_diab;
                        tqa.tqa_chis_fam_card = input_questionaire_aviation.p_chis_fam_card;
                        tqa.tqa_chis_fam_ment = input_questionaire_aviation.p_chis_fam_ment;
                        tqa.tqa_chis_conviction = input_questionaire_aviation.p_chis_conviction;
                        tqa.tqa_chis_conv_rmk = input_questionaire_aviation.p_chis_conv_rmk;
                        tqa.tqa_remark = input_questionaire_aviation.p_remark;
                        //tqa. = input_questionaire_aviation.p_license;
                        tqa.tqa_update_by = "System";
                        tqa.tqa_update_date = globalCls.GetServerDateTime();
                        if (newRecord == true)
                        {
                            tqa.tqa_create_by = "System";
                            tqa.tqa_create_date = globalCls.GetServerDateTime();
                            pdc.trn_ques_aviations.InsertOnSubmit(tqa);
                        }
                        pdc.SubmitChanges();
                    }
                    else
                    {
                        result.success_flag = "N";
                        result.error_name = "ERROR";
                    }
                }
                catch
                {
                    result.success_flag = "N";
                    result.error_name = "ERROR";
                }
                return result;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public questionaire_patiant displayQuestionairePatiant(string hn_no)
        {
            questionaire_patiant result = (from tqp in new InhCheckupDataContext().trn_ques_patients
                                           where tqp.trn_patient_regi.trn_patient.tpt_hn_no == hn_no
                                           orderby tqp.tqp_update_date descending
                                           select new questionaire_patiant
                                           {
                                               p_hn_no = hn_no,
                                               p_his_smok = tqp.tqp_his_smok,
                                               p_his_nsmok_yrs = tqp.tqp_his_nsmok_yrs,
                                               p_his_qsmok_yrs = tqp.tqp_his_qsmok_yrs,
                                               p_his_smok_amt = tqp.tqp_his_smok_amt,
                                               p_his_smok_dur = tqp.tqp_his_smok_dur,
                                               p_his_alcohol = tqp.tqp_his_alcohol,
                                               p_his_alco_yrs = tqp.tqp_his_alco_yrs,
                                               p_his_alco_social = tqp.tqp_his_alco_social,
                                               p_his_exercise = tqp.tqp_his_exercise,
                                               p_ill_concern = tqp.tqp_ill_concern,
                                               p_ill_conc_oth = tqp.tqp_ill_conc_oth,
                                               p_ill_psycho = tqp.tqp_ill_psycho,
                                               p_ill_psycho_oth = tqp.tqp_ill_psycho_oth,
                                               p_ill_med_his = tqp.tqp_ill_med_his,
                                               p_ill_med_hyper = tqp.tqp_ill_med_hyper,
                                               p_ill_med_heart = tqp.tqp_ill_med_heart,
                                               p_ill_med_diab = tqp.tqp_ill_med_diab,
                                               p_ill_med_coro = tqp.tqp_ill_med_coro,
                                               p_ill_med_dysl = tqp.tqp_ill_med_dysl,
                                               p_ill_med_cper = tqp.tqp_ill_med_cper,
                                               p_ill_med_gout = tqp.tqp_ill_med_gout,
                                               p_ill_med_abdd = tqp.tqp_ill_med_abdd,
                                               p_ill_med_pulm = tqp.tqp_ill_med_pulm,
                                               p_ill_med_para = tqp.tqp_ill_med_para,
                                               p_ill_med_stro = tqp.tqp_ill_med_stro,
                                               p_ill_med_putb = tqp.tqp_ill_med_putb,
                                               p_ill_med_sist = tqp.tqp_ill_med_sist,
                                               p_ill_med_kidn = tqp.tqp_ill_med_kidn,
                                               p_ill_med_epil = tqp.tqp_ill_med_epil,
                                               p_ill_med_resp = tqp.tqp_ill_med_resp,
                                               p_ill_med_asth = tqp.tqp_ill_med_asth,
                                               p_ill_med_emph = tqp.tqp_ill_med_emph,
                                               p_ill_med_chro = tqp.tqp_ill_med_chro,
                                               p_ill_med_canc = tqp.tqp_ill_med_canc,
                                               p_ill_med_canc_oth = tqp.tqp_ill_med_canc_oth,
                                               p_ill_med_alle = tqp.tqp_ill_med_alle,
                                               p_ill_med_pept = tqp.tqp_ill_med_pept,
                                               p_ill_med_oth = tqp.tqp_ill_med_oth,
                                               p_ill_med_others = tqp.tqp_ill_med_others,
                                               p_ill_cur_med = tqp.tqp_ill_cur_med,
                                               p_ill_cmed_diab = tqp.tqp_ill_cmed_diab,
                                               p_ill_cmed_hyper = tqp.tqp_ill_cmed_hyper,
                                               p_ill_cmed_demia = tqp.tqp_ill_cmed_demia,
                                               p_ill_cmed_cardi = tqp.tqp_ill_cmed_cardi,
                                               p_ill_cmed_dysl = tqp.tqp_ill_cmed_dysl,
                                               p_ill_cmed_horm = tqp.tqp_ill_cmed_horm,
                                               p_ill_cmed_oth = tqp.tqp_ill_cmed_oth,
                                               p_ill_cmed_others = tqp.tqp_ill_cmed_others,
                                               p_ill_allergy = tqp.tqp_ill_allergy,
                                               p_ill_drug_or_food = tqp.tqp_ill_drug_or_food,
                                               p_pill_adm = tqp.tqp_pill_adm,
                                               p_pill_admission = tqp.tqp_pill_admission,
                                               p_pill_sur = tqp.tqp_pill_sur,
                                               p_pill_surgery = tqp.tqp_pill_surgery,
                                               p_vinf_hepB_virus = tqp.tqp_vinf_hepB_virus,
                                               p_vinf_hepA_virus = tqp.tqp_vinf_hepA_virus,
                                               p_vinf_vaccine = tqp.tqp_vinf_vaccine,
                                               p_fhis_f_disease = tqp.tqp_fhis_f_disease,
                                               p_fhis_fdis_hyper = tqp.tqp_fhis_fdis_hyper,
                                               p_fhis_fdis_heart = tqp.tqp_fhis_fdis_heart,
                                               p_fhis_fdis_diab = tqp.tqp_fhis_fdis_diab,
                                               p_fhis_fdis_coro = tqp.tqp_fhis_fdis_coro,
                                               p_fhis_fdis_coro_cs = tqp.tqp_fhis_fdis_coro_cs,
                                               p_fhis_fdis_dysl = tqp.tqp_fhis_fdis_dysl,
                                               p_fhis_fdis_gout = tqp.tqp_fhis_fdis_gout,
                                               p_fhis_fdis_pulm = tqp.tqp_fhis_fdis_pulm,
                                               p_fhis_fdis_para = tqp.tqp_fhis_fdis_para,
                                               p_fhis_fdis_putb = tqp.tqp_fhis_fdis_putb,
                                               p_fhis_fdis_stro = tqp.tqp_fhis_fdis_stro,
                                               p_fhis_fdis_pepu = tqp.tqp_fhis_fdis_pepu,
                                               p_fhis_fdis_asth = tqp.tqp_fhis_fdis_asth,
                                               p_fhis_fdis_alle = tqp.tqp_fhis_fdis_alle,
                                               p_fhis_fdis_canc = tqp.tqp_fhis_fdis_canc,
                                               p_fhis_fdis_canc_rmk = tqp.tqp_fhis_fdis_canc_rmk,
                                               p_fhis_fdis_oth = tqp.tqp_fhis_fdis_oth,
                                               p_fhis_fdis_others = tqp.tqp_fhis_fdis_others,
                                               p_fhis_m_disease = tqp.tqp_fhis_m_disease,
                                               p_fhis_mdis_hyper = tqp.tqp_fhis_mdis_hyper,
                                               p_fhis_mdis_heart = tqp.tqp_fhis_mdis_heart,
                                               p_fhis_mdis_diab = tqp.tqp_fhis_mdis_diab,
                                               p_fhis_mdis_coro = tqp.tqp_fhis_mdis_coro,
                                               p_fhis_mdis_coro_cs = tqp.tqp_fhis_mdis_coro_cs,
                                               p_fhis_mdis_dysl = tqp.tqp_fhis_mdis_dysl,
                                               p_fhis_mdis_gout = tqp.tqp_fhis_mdis_gout,
                                               p_fhis_mdis_pulm = tqp.tqp_fhis_mdis_pulm,
                                               p_fhis_mdis_para = tqp.tqp_fhis_mdis_para,
                                               p_fhis_mdis_putb = tqp.tqp_fhis_mdis_putb,
                                               p_fhis_mdis_stro = tqp.tqp_fhis_mdis_stro,
                                               p_fhis_mdis_pepu = tqp.tqp_fhis_mdis_pepu,
                                               p_fhis_mdis_asth = tqp.tqp_fhis_mdis_asth,
                                               p_fhis_mdis_alle = tqp.tqp_fhis_mdis_alle,
                                               p_fhis_mdis_canc = tqp.tqp_fhis_mdis_canc,
                                               p_fhis_mdis_canc_rmk = tqp.tqp_fhis_mdis_canc_rmk,
                                               p_fhis_mdis_oth = tqp.tqp_fhis_mdis_oth,
                                               p_fhis_mdis_others = tqp.tqp_fhis_mdis_others,
                                               p_fhis_b_disease = tqp.tqp_fhis_b_disease,
                                               p_fhis_bdis_hyper = tqp.tqp_fhis_bdis_hyper,
                                               p_fhis_bdis_heart = tqp.tqp_fhis_bdis_heart,
                                               p_fhis_bdis_diab = tqp.tqp_fhis_bdis_diab,
                                               p_fhis_bdis_coro = tqp.tqp_fhis_bdis_coro,
                                               p_fhis_bdis_coro_bfm = tqp.tqp_fhis_bdis_coro_bfm,
                                               p_fhis_bdis_coro_afm = tqp.tqp_fhis_bdis_coro_afm,
                                               p_fhis_bdis_coro_nfm = tqp.tqp_fhis_bdis_coro_nfm,
                                               p_fhis_bdis_coro_bm = tqp.tqp_fhis_bdis_coro_bm,
                                               p_fhis_bdis_coro_am = tqp.tqp_fhis_bdis_coro_am,
                                               p_fhis_bdis_coro_nm = tqp.tqp_fhis_bdis_coro_nm,
                                               p_fhis_bdis_dysl = tqp.tqp_fhis_bdis_dysl,
                                               p_fhis_bdis_gout = tqp.tqp_fhis_bdis_gout,
                                               p_fhis_bdis_pulm = tqp.tqp_fhis_bdis_pulm,
                                               p_fhis_bdis_para = tqp.tqp_fhis_bdis_para,
                                               p_fhis_bdis_putb = tqp.tqp_fhis_bdis_putb,
                                               p_fhis_bdis_stro = tqp.tqp_fhis_bdis_stro,
                                               p_fhis_bdis_pepu = tqp.tqp_fhis_bdis_pepu,
                                               p_fhis_bdis_asth = tqp.tqp_fhis_bdis_asth,
                                               p_fhis_bdis_alle = tqp.tqp_fhis_bdis_alle,
                                               p_fhis_bdis_canc = tqp.tqp_fhis_bdis_canc,
                                               p_fhis_bdis_canc_rmk = tqp.tqp_fhis_bdis_canc_rmk,
                                               p_fhis_bdis_oth = tqp.tqp_fhis_bdis_oth,
                                               p_fhis_bdis_others = tqp.tqp_fhis_bdis_others,
                                               p_fhis_others = tqp.tqp_fhis_others,
                                               p_fwm_menopause = tqp.tqp_fwm_menopause,
                                               p_fwm_meno_start = tqp.tqp_fwm_meno_start,
                                               p_fwm_lst_st_mens = tqp.tqp_fwm_lst_st_mens,
                                               p_fwm_lst_ed_mens = tqp.tqp_fwm_lst_ed_mens,
                                               p_fwm_character = tqp.tqp_fwm_character,
                                               p_fwm_pregnancy = tqp.tqp_fwm_pregnancy,
                                               p_fwm_over_weight = tqp.tqp_fwm_over_weight
                                           }).FirstOrDefault();
            if (result == null)
            {
                result = getQuesFromTrak(hn_no);
            }
            return result;
        }

        private questionaire_patiant getQuesFromTrak(string hn_no)
        {
            using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
            {
                DataTable dt = ws.GetQuestionnaireHPC(hn_no);
                if (dt.Rows.Count > 0)
                {
                    questionaire_patiant que = new questionaire_patiant();
                    que.p_hn_no = hn_no;
                    #region smoke
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q4"].ToString())
                        {
                            case "1": result = 'N'; break;
                            case "2": result = 'Q'; break;
                            case "3": result = 'S'; break;
                        };
                        que.p_his_smok = result;
                    }
                    #endregion
                    #region quit smoke yrs
                    que.p_his_qsmok_yrs = calYear(dt.Rows[0]["Q5"], dt.Rows[0]["Q171"].ToString());
                    #endregion
                    #region smoke amt
                    que.p_his_smok_amt = convToDouble(dt.Rows[0]["Q6"]);
                    #endregion
                    #region smoke_dur
                    que.p_his_smok_dur = calYear(dt.Rows[0]["Q7"], dt.Rows[0]["Q168"].ToString());
                    #endregion
                    #region his alcohol
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q8"].ToString())
                        {
                            case "1": result = 'N'; break;
                            case "2": result = 'S'; break;
                            case "3": result = 'R'; break;
                        };
                        que.p_his_alcohol = result;
                    }
                    #endregion
                    #region his_alco_yrs
                    que.p_his_alco_yrs = calYear(dt.Rows[0]["Q165"], dt.Rows[0]["Q174"].ToString());
                    #endregion
                    #region his alco social
                    {
                        char? result = null;
                        if (dt.Rows[0]["Q81"].ToString() == "1")
                        {
                            result = 'O';
                        }
                        else if (dt.Rows[0]["Q82"].ToString() == "1")
                        {
                            result = 'T';
                        }
                        else if (dt.Rows[0]["Q83"].ToString() == "1")
                        {
                            result = 'W';
                        }
                        que.p_his_alco_social = result;
                    }
                    #endregion
                    #region his_exercise
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q10"].ToString())
                        {
                            case "1": result = 'N'; break;
                            case "2": result = 'O'; break;
                            case "3": result = 'R'; break;
                        };
                        que.p_his_exercise = result;
                    }
                    #endregion
                    #region ill_concern
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q11"].ToString())
                        {
                            case "A": result = 'A'; break;
                            case "O": result = 'O'; break;
                        };
                        que.p_ill_concern = result;
                    }
                    #endregion
                    #region ill_conc_oth
                    que.p_ill_conc_oth = isnull(dt.Rows[0]["Q12"].ToString());
                    #endregion
                    #region ill_psycho
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q13"].ToString())
                        {
                            case "N": result = 'N'; break;
                            case "O": result = 'O'; break;
                        };
                        que.p_ill_psycho = result;
                    }
                    #endregion
                    #region ill_psycho_oth
                    que.p_ill_psycho_oth = isnull(dt.Rows[0]["Q14"].ToString());
                    #endregion
                    #region ill_med_his
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q15"].ToString())
                        {
                            case "1": result = 'N'; break;
                            case "0": result = 'D'; break;
                        };
                        que.p_ill_med_his = result;
                    }
                    #endregion
                    #region ill_med_hyper
                    que.p_ill_med_hyper = convBool(dt.Rows[0]["Q28"]);
                    #endregion
                    #region ill_med_heart
                    que.p_ill_med_heart = convBool(dt.Rows[0]["Q29"]);
                    #endregion
                    #region ill_med_diab
                    que.p_ill_med_diab = convBool(dt.Rows[0]["Q26"]);
                    #endregion
                    #region ill_med_coro
                    que.p_ill_med_coro = convBool(dt.Rows[0]["Q134"]);
                    #endregion
                    #region ill_med_dysl
                    que.p_ill_med_dysl = convBool(dt.Rows[0]["Q27"]);
                    #endregion
                    #region ill_med_cper
                    que.p_ill_med_cper = convBool(dt.Rows[0]["Q136"]);
                    #endregion
                    #region ill_med_gout
                    que.p_ill_med_gout = convBool(dt.Rows[0]["Q32"]);
                    #endregion
                    #region ill_med_abdd
                    que.p_ill_med_abdd = convBool(dt.Rows[0]["Q138"]);
                    #endregion
                    #region ill_med_pulm
                    que.p_ill_med_pulm = convBool(dt.Rows[0]["Q34"]);
                    #endregion
                    #region ill_med_para
                    que.p_ill_med_para = convBool(dt.Rows[0]["Q84"]);
                    #endregion
                    #region ill_med_stro
                    que.p_ill_med_stro = convBool(dt.Rows[0]["Q142"]);
                    #endregion
                    #region ill_med_putb
                    que.p_ill_med_putb = convBool(dt.Rows[0]["Q35"]);
                    #endregion
                    #region ill_med_sist
                    que.p_ill_med_sist = convBool(dt.Rows[0]["Q143"]);
                    #endregion
                    #region ill_med_asth
                    que.p_ill_med_asth = convBool(dt.Rows[0]["Q31"]);
                    #endregion
                    #region ill_med_canc
                    que.p_ill_med_canc = convBool(dt.Rows[0]["Q85"]);
                    #endregion
                    #region ill_med_canc_oth
                    que.p_ill_med_canc_oth = isnull(dt.Rows[0]["Q158"].ToString());
                    #endregion
                    #region ill_med_alle
                    que.p_ill_med_alle = convBool(dt.Rows[0]["Q31"]);
                    #endregion
                    #region ill_med_pept
                    que.p_ill_med_pept = convBool(dt.Rows[0]["Q33"]);
                    #endregion
                    #region ill_med_oth
                    if (isnull(dt.Rows[0]["Q16"].ToString()) != null) que.p_ill_med_oth = true;
                    #endregion
                    #region ill_med_others
                    que.p_ill_med_others = isnull(dt.Rows[0]["Q16"].ToString());
                    #endregion
                    #region ill_cur_med
                    {
                        char? result = null;
                        switch (convBool(dt.Rows[0]["Q72"]))
                        {
                            case true: result = 'N'; break;
                            case false: result = 'H'; break;
                        }
                        que.p_ill_cur_med = result;
                    }
                    #endregion
                    #region ill_cmed_diab
                    que.p_ill_cmed_diab = convBool(dt.Rows[0]["Q17"]);
                    #endregion
                    #region ill_cmed_hyper
                    que.p_ill_cmed_hyper = convBool(dt.Rows[0]["Q59"]);
                    #endregion
                    #region ill_cmed_demia
                    que.p_ill_cmed_demia = convBool(dt.Rows[0]["Q58"]);
                    #endregion
                    #region ill_cmed_cardi
                    que.p_ill_cmed_cardi = convBool(dt.Rows[0]["Q86"]);
                    #endregion
                    #region ill_cmed_oth
                    if (isnull(dt.Rows[0]["Q67"].ToString()) != null) que.p_ill_cmed_oth = true;
                    #endregion
                    #region ill_cmed_others
                    que.p_ill_cmed_others = isnull(dt.Rows[0]["Q67"].ToString());
                    #endregion
                    #region ill_allergy
                    if (isnull(dt.Rows[0]["Q67"].ToString()) != null) que.p_ill_allergy = 'A';
                    #endregion
                    #region ill_drug_or_food
                    que.p_ill_drug_or_food = isnull(dt.Rows[0]["Q18"].ToString());
                    #endregion
                    #region pill_adm
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q19"].ToString())
                        {
                            case "1": result = 'Y'; break;
                            case "2": result = 'N'; break;
                        }
                        que.p_pill_adm = result;
                    }
                    #endregion
                    #region pill_admission
                    que.p_pill_admission = isnull(dt.Rows[0]["Q63"].ToString());
                    #endregion
                    #region pill_sur
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q20"].ToString())
                        {
                            case "1": result = 'Y'; break;
                            case "2": result = 'N'; break;
                        }
                        que.p_pill_sur = result;
                    }
                    #endregion
                    #region pill_surgery
                    que.p_pill_surgery = isnull(dt.Rows[0]["Q64"].ToString());
                    #endregion
                    #region vinf_hepB_virus
                    {
                        char? result = null;
                        if (convBool(dt.Rows[0]["Q98"]) == true)
                        {
                            result = 'Y';
                        }
                        else if (convBool(dt.Rows[0]["Q99"]) == true)
                        {
                            result = 'N';
                        }
                        que.p_vinf_hepB_virus = result;
                    }
                    #endregion
                    #region vinf_hepA_virus
                    {
                        char? result = null;
                        if (convBool(dt.Rows[0]["Q101"]) == true)
                        {
                            result = 'Y';
                        }
                        else if (convBool(dt.Rows[0]["Q102"]) == true)
                        {
                            result = 'N';
                        }
                        que.p_vinf_hepA_virus = result;
                    }
                    #endregion
                    #region vinf_vaccine
                    {
                        char? result = null;
                        if (convBool(dt.Rows[0]["Q104"]) == true)
                        {
                            result = 'L';
                        }
                        else if (convBool(dt.Rows[0]["Q105"]) == true)
                        {
                            result = 'M';
                        }
                        else if (convBool(dt.Rows[0]["Q107"]) == true)
                        {
                            result = 'N';
                        }
                        que.p_vinf_vaccine = result;
                    }
                    #endregion
                    #region fhis_f_disease
                    {
                        Boolean? temp = convBool(dt.Rows[0]["Q20"]);
                        char? result = null;
                        switch (convBool(dt.Rows[0]["Q20"]))
                        {
                            case true: result = 'N'; break;
                            case false: result = 'D'; break;
                        }
                        que.p_fhis_f_disease = result;
                    }
                    #endregion
                    #region fhis_fdis_hyper
                    que.p_fhis_fdis_hyper = convBool(dt.Rows[0]["Q42"]);
                    #endregion
                    #region fhis_fdis_heart
                    que.p_fhis_fdis_heart = convBool(dt.Rows[0]["Q44"]);
                    #endregion
                    #region fhis_fdis_diab
                    que.p_fhis_fdis_diab = convBool(dt.Rows[0]["Q38"]);
                    #endregion
                    #region fhis_fdis_coro
                    que.p_fhis_fdis_coro = convBool(dt.Rows[0]["Q144"]);
                    #endregion
                    #region fhis_fdis_coro_cs
                    {
                        char? result = null;
                        if (convBool(dt.Rows[0]["Q145"]) == true)
                        {
                            result = 'B';
                        }
                        else if (convBool(dt.Rows[0]["Q146"]) == true)
                        {
                            result = 'N';
                        }
                        que.p_fhis_fdis_coro_cs = result;
                    }
                    #endregion
                    #region fhis_fdis_dysl
                    que.p_fhis_fdis_dysl = convBool(dt.Rows[0]["Q40"]);
                    #endregion
                    #region fhis_fdis_gout
                    que.p_fhis_fdis_gout = convBool(dt.Rows[0]["Q50"]);
                    #endregion
                    #region fhis_fdis_pulm
                    que.p_fhis_fdis_pulm = convBool(dt.Rows[0]["Q54"]);
                    #endregion
                    #region fhis_fdis_para
                    que.p_fhis_fdis_para = convBool(dt.Rows[0]["Q109"]);
                    #endregion
                    #region fhis_fdis_putb
                    que.p_fhis_fdis_putb = convBool(dt.Rows[0]["Q56"]);
                    #endregion
                    #region fhis_fdis_stro
                    que.p_fhis_fdis_stro = convBool(dt.Rows[0]["Q162"]);
                    #endregion
                    #region fhis_fdis_pepu
                    que.p_fhis_fdis_pepu = convBool(dt.Rows[0]["Q52"]);
                    #endregion
                    #region fhis_fdis_asth
                    que.p_fhis_fdis_asth = convBool(dt.Rows[0]["Q48"]);
                    #endregion
                    #region fhis_fdis_alle
                    que.p_fhis_fdis_alle = convBool(dt.Rows[0]["Q46"]);
                    #endregion
                    #region fhis_fdis_canc
                    que.p_fhis_fdis_canc = convBool(dt.Rows[0]["Q110"]);
                    #endregion
                    #region fhis_fdis_canc_rmk
                    que.p_fhis_fdis_canc_rmk = isnull(dt.Rows[0]["Q159"].ToString());
                    #endregion
                    #region fhis_fdis_oth
                    if (isnull(dt.Rows[0]["Q22"].ToString()) != null) que.p_fhis_fdis_oth = true;
                    #endregion
                    #region fhis_fdis_others
                    que.p_fhis_fdis_others = isnull(dt.Rows[0]["Q22"].ToString());
                    #endregion
                    #region fhis_m_disease
                    {
                        char? result = null;
                        switch (convBool(dt.Rows[0]["Q37"]))
                        {
                            case true: result = 'N'; break;
                            case false: result = 'D'; break;
                        }
                        que.p_fhis_m_disease = result;
                    }
                    #endregion
                    #region fhis_mdis_hyper
                    que.p_fhis_mdis_hyper = convBool(dt.Rows[0]["Q43"]);
                    #endregion
                    #region fhis_mdis_heart
                    que.p_fhis_mdis_heart = convBool(dt.Rows[0]["Q45"]);
                    #endregion
                    #region fhis_mdis_diab
                    que.p_fhis_mdis_diab = convBool(dt.Rows[0]["Q39"]);
                    #endregion
                    #region fhis_mdis_coro
                    que.p_fhis_mdis_coro = convBool(dt.Rows[0]["Q147"]);
                    #endregion
                    #region fhis_mdis_coro_cs
                    {
                        char? result = null;
                        if (convBool(dt.Rows[0]["Q148"]) == true)
                        {
                            result = 'B';
                        }
                        else if (convBool(dt.Rows[0]["Q149"]) == true)
                        {
                            result = 'N';
                        }
                        que.p_fhis_mdis_coro_cs = result;
                    }
                    #endregion
                    #region fhis_mdis_dysl
                    que.p_fhis_mdis_dysl = convBool(dt.Rows[0]["Q41"]);
                    #endregion
                    #region fhis_mdis_gout
                    que.p_fhis_mdis_gout = convBool(dt.Rows[0]["Q51"]);
                    #endregion
                    #region fhis_mdis_pulm
                    que.p_fhis_mdis_pulm = convBool(dt.Rows[0]["Q55"]);
                    #endregion
                    #region fhis_mdis_para
                    que.p_fhis_mdis_para = convBool(dt.Rows[0]["Q115"]);
                    #endregion
                    #region fhis_mdis_putb
                    que.p_fhis_mdis_putb = convBool(dt.Rows[0]["Q57"]);
                    #endregion
                    #region fhis_mdis_stro
                    que.p_fhis_mdis_stro = convBool(dt.Rows[0]["Q163"]);
                    #endregion
                    #region fhis_mdis_pepu
                    que.p_fhis_mdis_pepu = convBool(dt.Rows[0]["Q53"]);
                    #endregion
                    #region fhis_mdis_asth
                    que.p_fhis_mdis_asth = convBool(dt.Rows[0]["Q49"]);
                    #endregion
                    #region fhis_mdis_alle
                    que.p_fhis_mdis_alle = convBool(dt.Rows[0]["Q47"]);
                    #endregion
                    #region fhis_mdis_canc
                    que.p_fhis_mdis_canc = convBool(dt.Rows[0]["Q116"]);
                    #endregion
                    #region fhis_mdis_canc_rmk
                    que.p_fhis_mdis_canc_rmk = isnull(dt.Rows[0]["Q160"].ToString());
                    #endregion
                    #region fhis_mdis_oth
                    if (isnull(dt.Rows[0]["Q24"].ToString()) != null) que.p_fhis_mdis_oth = true;
                    #endregion
                    #region fhis_mdis_others
                    que.p_fhis_mdis_others = isnull(dt.Rows[0]["Q24"].ToString());
                    #endregion
                    #region fhis_b_disease
                    {
                        char? result = null;
                        switch (convBool(dt.Rows[0]["Q119"]))
                        {
                            case true: result = 'N'; break;
                            case false: result = 'D'; break;
                        }
                        que.p_fhis_b_disease = result;
                    }
                    #endregion
                    #region fhis_bdis_hyper
                    que.p_fhis_bdis_hyper = convBool(dt.Rows[0]["Q122"]);
                    #endregion
                    #region fhis_bdis_heart
                    que.p_fhis_bdis_heart = convBool(dt.Rows[0]["Q120"]);
                    #endregion
                    #region fhis_bdis_diab
                    que.p_fhis_bdis_diab = convBool(dt.Rows[0]["Q121"]);
                    #endregion
                    #region fhis_bdis_coro
                    que.p_fhis_bdis_coro = convBool(dt.Rows[0]["Q150"]);
                    #endregion
                    #region fhis_bdis_coro_bfm
                    que.p_fhis_bdis_coro_bfm = convBool(dt.Rows[0]["Q152"]);
                    #endregion
                    #region fhis_bdis_coro_nfm
                    que.p_fhis_bdis_coro_nfm = convBool(dt.Rows[0]["Q153"]);
                    #endregion
                    #region fhis_bdis_coro_bm
                    que.p_fhis_bdis_coro_bm = convBool(dt.Rows[0]["Q151"]);
                    #endregion
                    #region fhis_bdis_coro_nm
                    que.p_fhis_bdis_coro_nm = convBool(dt.Rows[0]["Q153"]);
                    #endregion
                    #region fhis_bdis_dysl
                    que.p_fhis_bdis_dysl = convBool(dt.Rows[0]["Q125"]);
                    #endregion
                    #region fhis_bdis_gout
                    que.p_fhis_bdis_gout = convBool(dt.Rows[0]["Q126"]);
                    #endregion
                    #region fhis_bdis_pulm
                    que.p_fhis_bdis_pulm = convBool(dt.Rows[0]["Q128"]);
                    #endregion
                    #region fhis_bdis_para
                    que.p_fhis_bdis_para = convBool(dt.Rows[0]["Q123"]);
                    #endregion
                    #region fhis_bdis_putb
                    que.p_fhis_bdis_putb = convBool(dt.Rows[0]["Q129"]);
                    #endregion
                    #region fhis_bdis_stro
                    que.p_fhis_bdis_stro = convBool(dt.Rows[0]["Q164"]);
                    #endregion
                    #region fhis_bdis_pepu
                    que.p_fhis_bdis_pepu = convBool(dt.Rows[0]["Q127"]);
                    #endregion
                    #region fhis_bdis_asth
                    que.p_fhis_bdis_asth = convBool(dt.Rows[0]["Q130"]);
                    #endregion
                    #region fhis_bdis_alle
                    que.p_fhis_bdis_alle = convBool(dt.Rows[0]["Q131"]);
                    #endregion
                    #region fhis_bdis_canc
                    que.p_fhis_bdis_canc = convBool(dt.Rows[0]["Q124"]);
                    #endregion
                    #region fhis_bdis_canc_rmk
                    que.p_fhis_bdis_canc_rmk = isnull(dt.Rows[0]["Q161"].ToString());
                    #endregion
                    #region fhis_bdis_oth
                    if (isnull(dt.Rows[0]["Q132"].ToString()) != null) que.p_fhis_bdis_oth = true;
                    #endregion
                    #region fhis_bdis_others
                    que.p_fhis_bdis_others = isnull(dt.Rows[0]["Q132"].ToString());
                    #endregion
                    #region fhis_others
                    que.p_fhis_others = isnull(dt.Rows[0]["Q25"].ToString());
                    #endregion
                    #region fwm_menopause
                    if (isnull(dt.Rows[0]["Q173"].ToString()) != null) que.p_fwm_menopause = true;
                    #endregion
                    #region fwm_meno_start
                    {
                        char? result = null;
                        if (isnull(dt.Rows[0]["Q173"].ToString()) == null && isnull(dt.Rows[0]["Q133"].ToString()) == null)
                        {
                            result = 'N';
                        }
                        else if (isnull(dt.Rows[0]["Q173"].ToString()) == null && isnull(dt.Rows[0]["Q133"].ToString()) != null)
                        {
                            result = 'Y';
                        }
                        que.p_fwm_meno_start = result;
                    }
                    #endregion
                    #region fwm_lst_st_mens
                    que.p_fwm_lst_st_mens = convDate(dt.Rows[0]["Q133"]);
                    #endregion
                    #region fwm_pregnancy
                    {
                        char? result = null;
                        switch (dt.Rows[0]["Q88"].ToString())
                        {
                            case "N": result = 'N'; break;
                            case "U": result = 'S'; break;
                            case "Y": result = 'P'; break;

                        };
                        que.p_fwm_pregnancy = result;
                    }
                    #endregion
                    #region fwm_over_weight
                    {
                        char? result = null;
                        if (convBool(dt.Rows[0]["Q88"]) == true)
                        {
                            result = 'Y';
                        }
                        else if (convBool(dt.Rows[0]["Q89"]) == true)
                        {
                            result = 'N';
                        }
                        que.p_fwm_over_weight = result;
                    }
                    #endregion
                    return que;
                }
                return null;
            }
        }
        private double? calYear(object value, string unit)
        {
            double? val = convToDouble(value);
            if (val != null)
            {
                switch (unit)
                {
                    case "01": return val;
                    case "02": return Convert.ToDouble(val / 12);
                    case "03": return Math.Ceiling(Convert.ToDouble(val / 52));
                    case "04": return Math.Ceiling(Convert.ToDouble(val / 365));
                };
            }
            return null;
        }
        private string isnull(string str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }
        private Boolean? convBool(object obj)
        {
            if (obj != null)
            {
                try
                {
                    return Convert.ToBoolean(obj);
                }
                catch
                {

                }
            }
            return null;
        }
        private DateTime? convDate(object obj)
        {
            if (obj != null)
            {
                try
                {
                    return Convert.ToDateTime(obj);
                }
                catch
                {

                }
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public questionaire_aviation displayQuestionaireAviation(string hn_no, string p_doc_type)
        {
            questionaire_aviation result = (from tqa in new InhCheckupDataContext().trn_ques_aviations
                                            where tqa.trn_patient_regi.trn_patient.tpt_hn_no == hn_no &&
                                            tqa.tqa_doc_type == convStrToChar(p_doc_type)
                                            orderby tqa.tqa_update_date descending
                                            select new questionaire_aviation
                                            {
                                                p_hn_no = tqa.trn_patient_regi.trn_patient.tpt_hn_no,
                                                p_nameEN = tqa.tqa_en_fullname,
                                                p_marital = tqa.tqa_marital,
                                                p_place_exam = tqa.tqa_place_exam,
                                                p_license_type = tqa.tqa_license_type,
                                                p_avia_type = tqa.tqa_avia_type,
                                                p_avia_oths = tqa.tqa_avia_oths,
                                                p_license_no = tqa.tqa_license_no,
                                                p_chge_address = tqa.tqa_chge_address,
                                                p_th_address = tqa.tqa_th_address,
                                                p_th_mobile = tqa.tqa_th_moblie,
                                                p_en_address = tqa.tqa_en_address,
                                                p_en_mobile = tqa.tqa_en_mobile,
                                                p_th_occupa = tqa.tqa_th_occupa,
                                                p_th_comp = tqa.tqa_th_comp,
                                                p_th_office = tqa.tqa_th_office,
                                                p_th_of_mobile = tqa.tqa_th_of_mobile,
                                                p_cont_address = tqa.tqa_cont_address,
                                                p_person_emer = tqa.tqa_person_emer,
                                                p_telep_emer = tqa.tqa_telep_emer,
                                                p_prev_examined = tqa.tqa_prev_examined,
                                                p_prev_exam_loc = tqa.tqa_prev_exam_loc,
                                                p_prev_exam_date = tqa.tqa_prev_exam_date,
                                                p_prev_exam_deca = tqa.tqa_prev_exam_deca,
                                                p_med_waiver = tqa.tqa_med_waiver,
                                                p_waiver_spec = tqa.tqa_waiver_spec,
                                                p_tot_fling_time = tqa.tqa_tot_fling_time,
                                                p_last_six_time = tqa.tqa_last_six_time,
                                                p_pres_aircraft = tqa.tqa_pres_aircraft,
                                                p_aircraft_type = tqa.tqa_aircraft_type,
                                                p_aircraft_name = tqa.tqa_aircraft_name,
                                                p_aircraft_jet = tqa.tqa_aircraft_jet,
                                                p_aircraft_turbo = tqa.tqa_aircraft_turbo,
                                                p_aircraft_heli = tqa.tqa_aircraft_heli,
                                                p_aircraft_piston = tqa.tqa_aircraft_piston,
                                                p_aircraft_other = tqa.tqa_aircraft_other,
                                                p_aircraft_oth = tqa.tqa_aircraft_oth,
                                                p_flying_status = tqa.tqa_flying_status,
                                                p_smoking = tqa.tqa_smoking,
                                                p_smoking_since = tqa.tqa_smoking_since,
                                                p_smoking_type = tqa.tqa_smoking_type,
                                                p_smoking_amt = tqa.tqa_smoking_amt,
                                                p_use_medicine = tqa.tqa_use_medicine,
                                                tqa_med_name = tqa.tqa_med_name,
                                                tqa_med_amount = tqa.tqa_med_amount,
                                                tqa_med_startdate = tqa.tqa_med_startdate,
                                                tqa_med_reason = tqa.tqa_med_reason,
                                                tqa_med_name2 = tqa.tqa_med_name2,
                                                tqa_med_amount2 = tqa.tqa_med_amount2,
                                                tqa_med_startdate2 = tqa.tqa_med_startdate2,
                                                tqa_med_reason2 = tqa.tqa_med_reason2,
                                                tqa_med_name3 = tqa.tqa_med_name3,
                                                tqa_med_amount3 = tqa.tqa_med_amount3,
                                                tqa_med_startdate3 = tqa.tqa_med_startdate3,
                                                tqa_med_reason3 = tqa.tqa_med_reason3,
                                                p_avg_alcohal = tqa.tqa_avg_alcohal,
                                                p_m20_exercise = tqa.tqa_m20_exercise,
                                                p_chis_freq = tqa.tqa_chis_freq,
                                                p_chis_freq_rmk = tqa.tqa_chis_freq_rmk,
                                                p_chis_dizz = tqa.tqa_chis_dizz,
                                                p_chis_dizz_rmk = tqa.tqa_chis_dizz_rmk,
                                                p_chis_unco = tqa.tqa_chis_unco,
                                                p_chis_unco_rmk = tqa.tqa_chis_unco_rmk,
                                                p_chis_eyet = tqa.tqa_chis_eyet,
                                                p_chis_eyet_rmk = tqa.tqa_chis_eyet_rmk,
                                                p_chis_hayf = tqa.tqa_chis_hayf,
                                                p_chis_hayf_rmk = tqa.tqa_chis_hayf_rmk,
                                                p_chis_hert = tqa.tqa_chis_hert,
                                                p_chis_hert_rmk = tqa.tqa_chis_hert_rmk,
                                                p_chis_chst = tqa.tqa_chis_chst,
                                                p_chis_chst_rmk = tqa.tqa_chis_chst_rmk,
                                                p_chis_high = tqa.tqa_chis_high,
                                                p_chis_high_rmk = tqa.tqa_chis_high_rmk,
                                                p_chis_stom = tqa.tqa_chis_stom,
                                                p_chis_stom_rmk = tqa.tqa_chis_stom_rmk,
                                                p_chis_jaun = tqa.tqa_chis_jaun,
                                                p_chis_jaun_rmk = tqa.tqa_chis_jaun_rmk,
                                                p_chis_kidn = tqa.tqa_chis_kidn,
                                                p_chis_kidn_rmk = tqa.tqa_chis_kidn_rmk,
                                                p_chis_suga = tqa.tqa_chis_suga,
                                                p_chis_suga_rmk = tqa.tqa_chis_suga_rmk,
                                                p_chis_epil = tqa.tqa_chis_epil,
                                                p_chis_epil_rmk = tqa.tqa_chis_epil_rmk,
                                                p_chis_nurv = tqa.tqa_chis_nurv,
                                                p_chis_nurv_rmk = tqa.tqa_chis_nurv_rmk,
                                                p_chis_temp = tqa.tqa_chis_temp,
                                                p_chis_temp_rmk = tqa.tqa_chis_temp_rmk,
                                                p_chis_drug = tqa.tqa_chis_drug,
                                                p_chis_drug_rmk = tqa.tqa_chis_drug_rmk,
                                                p_chis_suic = tqa.tqa_chis_suic,
                                                p_chis_suic_rmk = tqa.tqa_chis_suic_rmk,
                                                p_chis_losw = tqa.tqa_chis_losw,
                                                p_chis_losw_rmk = tqa.tqa_chis_losw_rmk,
                                                p_chis_moti = tqa.tqa_chis_moti,
                                                p_chis_moti_rmk = tqa.tqa_chis_moti_rmk,
                                                p_chis_reje = tqa.tqa_chis_reje,
                                                p_chis_reje_rmk = tqa.tqa_chis_reje_rmk,
                                                p_chis_adms = tqa.tqa_chis_adms,
                                                p_chis_adms_rmk = tqa.tqa_chis_adms_rmk,
                                                p_chis_avia = tqa.tqa_chis_avia,
                                                p_chis_avia_rmk = tqa.tqa_chis_avia_rmk,
                                                p_chis_otha = tqa.tqa_chis_otha,
                                                p_chis_otha_rmk = tqa.tqa_chis_otha_rmk,
                                                p_chis_gyna = tqa.tqa_chis_gyna,
                                                p_chis_gyna_rmk = tqa.tqa_chis_gyna_rmk,
                                                p_chis_othi = tqa.tqa_chis_othi,
                                                p_chis_othi_rmk = tqa.tqa_chis_othi_rmk,
                                                p_chis_heth = tqa.tqa_chis_heth,
                                                p_chis_heth_rmk = tqa.tqa_chis_heth_rmk,
                                                p_chis_lung = tqa.tqa_chis_lung,
                                                p_chis_lung_rmk = tqa.tqa_chis_lung_rmk,
                                                p_chis_alco = tqa.tqa_chis_alco,
                                                p_chis_alco_rmk = tqa.tqa_chis_alco_rmk,
                                                p_chis_ment = tqa.tqa_chis_ment,
                                                p_chis_ment_rmk = tqa.tqa_chis_ment_rmk,
                                                p_chis_fam_diab = tqa.tqa_chis_fam_diab,
                                                p_chis_fam_card = tqa.tqa_chis_fam_card,
                                                p_chis_fam_ment = tqa.tqa_chis_fam_ment,
                                                p_chis_conviction = tqa.tqa_chis_conviction,
                                                p_chis_conv_rmk = tqa.tqa_chis_conv_rmk,
                                                p_remark = tqa.tqa_remark
                                            }).FirstOrDefault();
            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<output_screen_language> getScreenLanguage()
        {
            List<output_screen_language> result = (from mrm in new InhCheckupDataContext().mst_room_hdrs
                                                   select new output_screen_language
                                                   {
                                                       mrm_code = mrm.mrm_code,
                                                       mrm_scrn_eng = mrm.mrm_scrn_eng,
                                                       mrm_scrn_thai = mrm.mrm_scrn_thai,
                                                       mrm_scrn_arabic = mrm.mrm_scrn_arabic,
                                                       mrm_scrn_myan = mrm.mrm_scrn_myan,
                                                       mrm_scrn_japan = mrm.mrm_scrn_japan
                                                   }).Distinct().ToList();

            return result;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public byte[] getImagePatient(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    Image imageIn = Image.FromFile(@"D:\www\Report\" + fileName);
                    MemoryStream ms = new MemoryStream();
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
                catch (Exception)
                {

                }
            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void getCheckUpLabResult(string HN_NO, int tpr_id)
        {

            new Class.InsertLabCls().RetrieveLabToEmrCheckup(tpr_id, "System");
            //new Class.GetLabResultCls().Insert_trn_patient_ass(tpr_id);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void getCheckUpLabResultBackground(int tpr_id)
        {
            new Class.InsertLabCls().RetrieveLabToEmrCheckupBackGround(tpr_id, "System");
            //new Class.GetLabResultCls().Insert_trn_patient_assBackground(tpr_id);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RetrieveCheckupLabResult(string hn, string en, char pregnancy, char smoke, bool diabetes)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int tpr_id = cdc.trn_patient_regis
                                    .Where(x => x.trn_patient.tpt_hn_no == hn &&
                                                x.tpr_en_no == en)
                                    .OrderByDescending(x => x.tpr_create_date)
                                    .Select(x => x.tpr_id)
                                    .FirstOrDefault();
                    if (tpr_id != 0)
                    {
                        var questionnaire = new LabClass.QuestionnaireResult { diabetes = diabetes, pregnancy = pregnancy, smoke = smoke };
                        new Class.InsertLabCls().RetrieveLabToEmrCheckupWithQuestionnaire(tpr_id, questionnaire, "EBook");
                        //Class.GetLabResultCls cls = new GetLabResultCls();
                        //cls.Insert_trn_ques_patient(tpr_id, "EBook", pregnancy, smoke, diabetes);
                        //cls.Insert_trn_patient_ass(tpr_id, "EBook");
                    }
                }
            }
            catch
            {

            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void retrieveLabToPatientLab(int tpr_id)
        {
            new Class.PatientLabCls().InsertByCheckupLabResult(tpr_id, "System");
            //new Class.GetLabResultCls().Insert_trn_patient_lab(tpr_id);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void retrieveLabToPatientLabBackground(int tpr_id)
        {
            new Class.PatientLabCls().InsertByCheckupLabResultBackground(tpr_id, "System");
            //new Class.GetLabResultCls().Insert_trn_patient_labBackground(tpr_id);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void retrieveVitalSign(int tpr_id, string username)
        {
            new Class.VitalSignCls().InsertByVitalSignResult(tpr_id, "TESTVS");
            //new Class.GetVitalSignCLs().GetVistalSign(tpr_id, username);
            //new Class.GetLabResultCls().InsertLabVitalSign(tpr_id, username);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void interpretEyeEx(int tpr_id, string username)
        {
            new Class.GetVitalSignCLs().GetEyeExam(tpr_id, "TESTVS");
            //new Class.GetVitalSignCLs().GetVistalSign(tpr_id, username);
            //new Class.GetLabResultCls().InsertLabVitalSign(tpr_id, username);
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void retrieveVitalSignBackground(int tpr_id)
        {
            new Class.GetVitalSignCLs().GetVistalSignBackground(tpr_id);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool SendToBook(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string url = cdc.mst_project_configs
                                    .Where(x => x.mpc_code == "WSCallDataTrakCare")
                                    .Select(x => x.mpc_value).FirstOrDefault();
                    if (tpr_id != null)
                    {
                        int countError = 3;
                        for (int i = 1; i <= countError; i++)
                        {
                            Class.SendToBookCls.StatusSendBook sendBook = new Class.SendToBookCls().SendToBook(tpr_id, url);
                            switch (sendBook)
                            {
                                case SendToBookCls.StatusSendBook.SendBook:
                                    return true;
                                case SendToBookCls.StatusSendBook.NotSendBook:
                                    return false;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WS_GetDataFromPathway", "SendToBook(int? tpr_id)", ex.Message);
                return false;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ResetDoctor(string HN, string EN)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_en_no == EN && x.trn_patient.tpt_hn_no == HN).FirstOrDefault();
                    if (patientRegis != null)
                    {
                        DateTime dateNow = DateTime.Now;
                        string oldDoctor = string.IsNullOrEmpty(patientRegis.tpr_req_doc_code) ? patientRegis.tpr_pe_doc_code : patientRegis.tpr_req_doc_code;
                        log_change_doctor log = new log_change_doctor
                        {
                            log_doc_code_old = oldDoctor,
                            log_doc_code_new = null,
                            log_time = dateNow,
                            log_username = "SysWS"
                        };
                        cdc.log_change_doctors.InsertOnSubmit(log);

                        patientRegis.tpr_pe_doc = null;
                        patientRegis.tpr_pe_doc_code = null;
                        patientRegis.tpr_pe_doc_name = null;
                        patientRegis.tpr_req_inorout_doctor = null;
                        patientRegis.tpr_req_doctor = null;
                        patientRegis.tpr_req_doc_gender = null;
                        patientRegis.tpr_req_doc_code = null;
                        patientRegis.tpr_req_doc_name = null;
                        patientRegis.tpr_update_by = "SysWS";
                        patientRegis.tpr_update_date = dateNow;

                        cdc.SubmitChanges();
                        return patientRegis.trn_patient.tpt_othername + " Reset Doctor Completed.";
                    }
                    else
                    {
                        return "ไม่พบคนไข้ที่ระบุ";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddPatientPackage(string HN, string EN, float point)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_en_no == EN && x.trn_patient.tpt_hn_no == HN).FirstOrDefault();
                    if (patientRegis != null)
                    {
                        mst_order_point mstOrderPoint = cdc.mst_order_points
                                                           .Where(x => x.mot_set_code.StartsWith("GHOSTPACK") &&
                                                                       x.mot_point == point &&
                                                                       x.mot_status == 'A').FirstOrDefault();
                        if (mstOrderPoint != null)
                        {
                            DateTime dateNow = DateTime.Now;
                            trn_patient_order_set orderSet = patientRegis.trn_patient_order_sets
                                                                         .Where(x => x.tos_od_set_code.StartsWith("GHOSTPACK"))
                                                                         .FirstOrDefault();
                            if (orderSet == null)
                            {
                                orderSet = new trn_patient_order_set();
                                patientRegis.trn_patient_order_sets.Add(orderSet);
                                orderSet.tos_create_by = "SysWS";
                                orderSet.tos_create_date = dateNow;
                            }
                            orderSet.tos_od_set_code = mstOrderPoint.mot_set_code;
                            orderSet.tos_od_set_name = mstOrderPoint.mot_set_name;
                            orderSet.tos_status = true;
                            orderSet.tos_update_by = "SysWS";
                            orderSet.tos_update_date = dateNow;
                            cdc.SubmitChanges();
                            return patientRegis.trn_patient.tpt_othername + " Add Package Completed.";
                        }
                        else
                        {
                            return "ไม่พบ Point ที่ระบุ";
                        }
                    }
                    else
                    {
                        return "ไม่พบคนไข้ที่ระบุ";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        public class patient_order_set
        {
            public string tos_od_set_code { get; set; }
            public string tos_od_set_name { get; set; }
            public bool? tos_status { get; set; }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<patient_order_set> GetPatientPackage(string HN, string queue_no)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_queue_no == queue_no && x.trn_patient.tpt_hn_no == HN).FirstOrDefault();
                    if (patientRegis != null)
                    {
                        return patientRegis.trn_patient_order_sets
                                           .Select(x => new patient_order_set
                                           {
                                               tos_od_set_code = x.tos_od_set_code,
                                               tos_od_set_name = x.tos_od_set_name,
                                               tos_status = x.tos_status
                                           }).ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ReCancelPatientToCheckB(string HN, string EN)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_en_no == EN && x.trn_patient.tpt_hn_no == HN).FirstOrDefault();
                    if (patientRegis != null)
                    {
                        DateTime dateNow = DateTime.Now;
                        trn_patient_queue patientQueueBK = patientRegis.trn_patient_queues.Where(x => x.mst_room_hdr.mrm_code == "BK").FirstOrDefault();
                        trn_patient_queue patientQueueCB = patientRegis.trn_patient_queues.Where(x => x.mst_room_hdr.mrm_code == "CB").FirstOrDefault();

                        //กรณี Sent to Book แล้วแต่ต้องการนำกลับมาเข้าระบบ Queue
                        if (patientRegis.tpr_status == "WB" && patientRegis.tpr_pe_status == "RS" && patientQueueBK != null && patientQueueCB != null)
                        {
                            cdc.trn_patient_queues.DeleteOnSubmit(patientQueueBK);
                            patientRegis.tpr_status = null;
                            patientRegis.tpr_pe_status = "NR";
                            patientRegis.tpr_update_by = "SysWS";
                            patientRegis.tpr_update_date = dateNow;
                        }
                        else
                        {
                            //กรณีกดค้างตรวจแพทย์ที่ Check C แต่ต้องการนำกลับมาเข้าระบบ Queue
                            //กรณีกดค้างตรวจแพทย์ที่ Check B แต่ต้องการนำกลับมาเข้าระบบ Queue
                            if (patientRegis.tpr_pending == true && patientRegis.tpr_pending_no_station == true && patientQueueBK == null && patientQueueCB != null)
                            {
                                patientRegis.tpr_status = null;
                                patientRegis.tpr_pe_status = null;
                                patientRegis.tpr_pending = null;
                                patientRegis.tpr_pending_no_station = null;
                                patientRegis.tpr_update_by = "SysWS";
                                patientRegis.tpr_update_date = dateNow;
                            }
                            else
                            {
                                //กรณีกด Cancel ก่อน Check B แต่ต้องการนำกลับมาเข้าระบบ Queue
                                if (patientQueueCB == null)
                                {
                                    mst_room_hdr mstRoom = cdc.mst_room_hdrs.Where(x => x.mhs_id == patientRegis.mhs_id && x.mrm_code == "CB").FirstOrDefault();
                                    mst_event mstEvent = cdc.mst_events.Where(x => x.mvt_code == "CB").FirstOrDefault();
                                    patientQueueCB = new trn_patient_queue()
                                    {
                                        mrm_id = mstRoom.mrm_id,
                                        mvt_id = mstEvent.mvt_id,
                                        tps_create_by = "SysWS",
                                        tps_create_date = dateNow
                                    };
                                    patientRegis.trn_patient_queues.Add(patientQueueCB);

                                    patientRegis.tpr_pending = null;
                                    patientRegis.tpr_update_by = "SysWS";
                                    patientRegis.tpr_update_date = dateNow;
                                }
                                else
                                {
                                    return patientRegis.trn_patient.tpt_othername + " ไม่ได้อยู่เงื่อนไขทีสามารถ ReCancel To Check point B ได้";
                                }
                            }
                        }
                        patientQueueCB.tps_status = "NS";
                        patientQueueCB.tps_ns_status = "QL";
                        patientQueueCB.tps_update_by = "SysWS";
                        patientQueueCB.tps_update_date = dateNow;
                        cdc.SubmitChanges();
                        return patientRegis.trn_patient.tpt_othername + " ReCancel To Check point B Completed.";
                    }
                    else
                    {
                        return "ไม่พบคนไข้ที่ระบุ";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string ReAssignDoctor(string HN, string EN)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_en_no == EN && x.trn_patient.tpt_hn_no == HN).FirstOrDefault();
                    if (patientRegis != null)
                    {
                        patientRegis.tpr_pe_doc = null;
                        patientRegis.tpr_pe_doc_code = null;
                        patientRegis.tpr_pe_doc_name = null;
                        patientRegis.tpr_req_inorout_doctor = null;
                        patientRegis.tpr_req_doc_code = null;
                        patientRegis.tpr_req_doc_name = null;
                        cdc.SubmitChanges();

                        var result = cdc.ReAssignDoctor(patientRegis.tpr_id, "SysWS").FirstOrDefault();
                        var username = result.Column1;
                        if (string.IsNullOrEmpty(username))
                        {
                            return patientRegis.trn_patient.tpt_othername + " ไม่สามารถ Assign Doctor ได้ เพราะไม่ได้อยู่ภายใต้เงื่อนไขที่กำหนด";
                        }
                        else
                        {
                            mst_user_type mut = cdc.mst_user_types.Where(x => x.mut_username == username).FirstOrDefault();
                            return patientRegis.trn_patient.tpt_othername + " ReAssign Doctor to " + mut.mut_fullname + " Completed.";
                        }
                    }
                    else
                    {
                        return "ไม่พบคนไข้ที่ระบุ";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string AddMasterPointPackage(string PackageRowID, float point)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string _package = PackageRowID.Trim();
                    DateTime dateNow = globalCls.GetServerDateTime();
                    trn_patient_order_set orderSet = cdc.trn_patient_order_sets
                                                        .Where(x => x.tos_od_set_code == _package)
                                                        .FirstOrDefault();
                    if (orderSet != null)
                    {
                        mst_order_point orderPoint = cdc.mst_order_points
                                                        .Where(x => x.mot_set_code == orderSet.tos_od_set_name)
                                                        .FirstOrDefault();
                        if (orderPoint == null)
                        {
                            orderPoint = new mst_order_point();
                            orderPoint.mot_set_code = orderSet.tos_od_set_code;
                            orderPoint.mot_set_name = orderSet.tos_od_set_name;
                            orderPoint.mot_create_by = "SysWS";
                            orderPoint.mot_create_date = dateNow;
                            cdc.mst_order_points.InsertOnSubmit(orderPoint);
                        }
                        orderPoint.mot_status = 'A';
                        orderPoint.mot_point = point;
                        orderPoint.mot_update_by = "SysWS";
                        orderPoint.mot_update_date = dateNow;
                        orderPoint.mhs_id = 1;
                        cdc.SubmitChanges();
                        return "Package Row ID. : " + orderPoint.mot_set_code + Environment.NewLine +
                               "Package Name : " + orderPoint.mot_set_name + Environment.NewLine +
                               "Site : HPC Site 1" + Environment.NewLine +
                               "Point : " + point.ToString("0.0");
                    }
                    else
                    {
                        return "Package นี้ ไม่พอข้อมูลใน Database(Order Set ของคนไข้).";
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Class.SendAutoCls.SendAutoResult SendAuto(int tps_id, List<int> ListCompleteEvent, string username, int? current_site)
        {
            return new Class.SendAutoCls().SendAuto(tps_id, ListCompleteEvent, username, current_site);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void retrievePackage(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    DateTime dateNow = globalCls.GetServerDateTime();
                    try
                    {
                        int enRowID = Convert.ToInt32(patientRegis.tpr_en_rowid);
                        GetPTPackageCls PackageCls = new GetPTPackageCls();
                        EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
                        PackageCls.AddPatientOrderItem(ref patientRegis, "SysRPack", dateNow, getPTPackage);
                        PackageCls.AddPatientOrderSet(ref patientRegis, "SysRPack", dateNow, getPTPackage);
                        List<GetPTPackageCls.MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                        PackageCls.AddPatientEvent(ref patientRegis, "SysRPack", dateNow, mapOrder);
                        PackageCls.AddPatientPlan(ref patientRegis, "SysRPack", dateNow);
                        PackageCls.skipReqDoctorOutDepartment(ref patientRegis);
                        PackageCls.CompleteEcho(ref patientRegis);
                        PackageCls.skipChangeEstToEcho(ref patientRegis, patientRegis.mhs_id);
                        PackageCls.checkOrderPMR(ref patientRegis, patientRegis.mhs_id);
                        cdc.SubmitChanges();
                    }
                    catch (Exception ex)
                    {
                        globalCls.MessageError("WSCheckUp", "retrievePackage(int tpr_id)", ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WSCheckUp", "retrievePackage(int tpr_id)", ex.Message);
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool? SendDoctorApprove(int tpr_id, string rptCode, string username)
        {
            bool? success = new SendDoctorApproveCls().SendToDoctorApprove(tpr_id, rptCode, username);
            return success;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool? RecallApprove(int tpr_id, string username)
        {
            bool? success = new SendDoctorApproveCls().CallBackToBook(tpr_id, username);
            return success;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool? testChart(int tpr_id)
        {
            var chartrs = "xx";
            //var chartrs = new LabClass.GetChartCls().GetID_Royal("C0180", 40.0, 500.00, 70.0, 99.00, 87.00, "87", "N");
            var success = new bool();
            if (chartrs != null)
            {
                success = true;
            }
            else { success = false; }


            return success;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SendToDocScan(int tpr_id, string mrt_code, string mhs_code, string username)
        {
            return new Class.DocScan.SendToDocScanCls().Send(tpr_id, mrt_code, mhs_code, username);
        }

        private trn_patient_regi GetTranPatientRegis(string HNno)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                var objregis = (from t1 in dbc.trn_patient_regis
                                where t1.trn_patient.tpt_hn_no == HNno
                                orderby t1.tpr_id descending
                                select t1).ToList().FirstOrDefault();
                return objregis;
            }
        }
        private int? return_tpr_id_by_hn(string hn_no)
        {
            int tpr_id = new InhCheckupDataContext().trn_patient_regis.Where(x => x.trn_patient.tpt_hn_no == hn_no)
                                                    .OrderByDescending(x => x.tpr_id)
                                                    .Select(x => x.tpr_id).FirstOrDefault();
            return tpr_id;
        }
        private char? convStrToChar(string value)
        {
            char? chr = null;
            if (!string.IsNullOrEmpty(value))
            {
                chr = Convert.ToChar(value);
            }
            return chr;
        }
        private DateTime? getOnlyDate(DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.Date;
            }
            return null;
        }
        private double? convToDouble(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            else
            {
                try
                {
                    double val = Convert.ToDouble(obj);
                    return val;
                }
                catch
                {
                    return null;
                }
            }
        }
    }

    public static class paramWSGetResult
    {
        public static string convParamGetResultToString(this string str)
        {
            if (str == "NULL" || string.IsNullOrEmpty(str))
            {
                return null;
            }
            return str;
        }
        public static float? convParamGetResultToFloat(this string str)
        {
            if (str == "NULL" || string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                try
                {
                    float i;
                    return float.TryParse(str, out i) ? (float?)i : null;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static DateTime? convParamGetResultToDate(this string str)
        {
            if (str == "NULL" || string.IsNullOrEmpty(str))
            {
                return null;
            }
            else
            {
                try
                {
                    return DateTime.ParseExact(str, "yyyy-MM-dd", new System.Globalization.CultureInfo("en-US"));

                }
                catch
                {
                    return null;
                }
            }
        }

    }
}
