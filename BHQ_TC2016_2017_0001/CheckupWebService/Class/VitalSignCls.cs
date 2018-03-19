using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class VitalSignCls
    {
        public bool InsertByVitalSignResult(int tpr_id, string user)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Class.globalCls.GetServerDateTime();
                    var pregis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (pregis != null)
                    {
                        int enrowid;
                        int.TryParse(pregis.tpr_en_rowid, out enrowid);
                        var vs = new APITrakcare.GetVitalSignCls().ByGetVitalSignByHN(pregis.trn_patient.tpt_hn_no);
                        var vs3time = new Class.VitalSignCls().Get3time(vs, enrowid);
                        var vsunits = new Class.VitalSignUnitCls().Get();
                        var visitdate = pregis.trn_patient_regis_detail != null && pregis.trn_patient_regis_detail.tpr_real_arrived_date != null
                                        ? pregis.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date
                                        : pregis.tpr_arrive_date != null
                                        ? pregis.tpr_arrive_date.Value.Date
                                        : dateNow;
                        InsertByVitalSignResult(ref pregis, vs3time, dateNow, user);

                        var hdr = pregis.trn_basic_measure_hdrs.FirstOrDefault();
                        for (int i = vs3time.Count; i < hdr.trn_basic_measure_dtls.Count; i++)
                        {
                            if (hdr.trn_basic_measure_dtls[i].tbd_id == 0)
                            {
                                hdr.trn_basic_measure_dtls.Remove(hdr.trn_basic_measure_dtls[i]);
                            }
                            else
                            {
                                cdc.trn_basic_measure_dtls.DeleteOnSubmit(hdr.trn_basic_measure_dtls[i]);
                            }
                        }

                        var sex = (char)pregis.trn_patient.tpt_gender;
                        var ages = new LabClass.InterpretLabCls().calAge(pregis.trn_patient.tpt_dob.Value.Date, visitdate);
                        var labvitalsigns = cdc.mst_labs.Where(x => x.mlb_type == 'C' && x.mlb_status == 'A').ToList();
                        var labconfig = new LabClass.GetLabConfigCls().GetByPaientLab(tpr_id);

                        foreach (var lab in labvitalsigns)
                        {
                            var maplabspecial = new LabClass.MapLab
                            {
                                code = lab.mlb_code,
                                id = lab.mlb_id,
                                nameen = lab.mlb_ename,
                                nameth = lab.mlb_tname,
                                seq = lab.mlb_chart_seq,
                                setcode = lab.mlb_lab_set,
                                usechart = lab.mlb_use_chart == true ? true : false,
                                valuetype = lab.mlb_value_type,
                                status = 'E'
                            };
                            var rs = new LabClass.InterpretLabCls().GetResult(maplabspecial, labconfig, ages, sex);
                            var rslab = new LabClass.InterpretLab
                            {
                                setcode = lab.mlb_lab_set,
                                code = lab.mlb_code,
                                name_en = lab.mlb_ename,
                                name_th = lab.mlb_tname,
                                seq = lab.mlb_chart_seq,
                                mlr_id = rs.mlr_id,
                                summary = rs.summary,
                                result_en = rs.result_en,
                                result_th = rs.result_th,
                                result_jp =rs.result_jp,
                                normalrange = rs.normalrange,
                                unit = rs.unit,
                                status = rs.status
                            };
                            InsertLabVs(ref pregis, rslab, vs3time, vsunits, dateNow, user);
                        }
                        cdc.SubmitChanges();

                        new Class.GetVitalSignCLs().GetEyeExam(tpr_id, "TESTVS");

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("VitalSignCls", "InsertByVitalSignResult", ex.Message);
                return false;
            }
        }

        public void InsertByVitalSignResult(ref trn_patient_regi pregis, List<APITrakcare.VitalSignResult> vsresult, DateTime dateNow, string user)
        {
            try
            {
                var hdr = pregis.trn_basic_measure_hdrs.FirstOrDefault();
                if (hdr == null)
                {
                    hdr = new trn_basic_measure_hdr();
                    hdr.tbm_create_by = user;
                    hdr.tbm_create_date = dateNow;
                    pregis.trn_basic_measure_hdrs.Add(hdr);
                }
                hdr.tbm_vision_left = null;
                hdr.tbm_vision_right = null;
                hdr.tbm_glass_or_contact = null;
                hdr.tbm_color_blind = null;
                hdr.tbm_vision_lvisual_with_lens = null;
                hdr.tbm_vision_rvisual_with_lens = null;
                hdr.tbm_vision_lvisual_out_lens = null;
                hdr.tbm_vision_rvisual_out_lens = null;
                hdr.tbm_update_by = user;
                hdr.tbm_update_date = dateNow;

                if (vsresult.Count > 0)
                {
                    hdr.tbm_vision_left = convInt(vsresult[0].visionLeft);
                    hdr.tbm_vision_right = convInt(vsresult[0].visionRight);
                    hdr.tbm_glass_or_contact = vsresult[0].withlen ? 'Y' : 'N';
                    hdr.tbm_color_blind = vsresult[0].colorblind == null
                                          ? (char?)null
                                          : vsresult[0].colorblind.ToUpper() == "NA"
                                          ? 'X'
                                          : vsresult[0].colorblind.ToUpper() == "NO"
                                          ? 'N'
                                          : vsresult[0].colorblind.ToUpper() == "AB"
                                          ? 'A'
                                          : (char?)null;
                    if (hdr.tbm_glass_or_contact == 'Y')
                    {
                        hdr.tbm_vision_lvisual_with_lens = vsresult[0].visionLeft;
                        hdr.tbm_vision_rvisual_with_lens = vsresult[0].visionRight;
                    }
                    else
                    {
                        hdr.tbm_vision_lvisual_out_lens = vsresult[0].visionLeft;
                        hdr.tbm_vision_rvisual_out_lens = vsresult[0].visionRight;
                    }

                    var dtls = hdr.trn_basic_measure_dtls.OrderBy(x => x.tbd_id).ToList();
                    int i;
                    for (i = 0; i < vsresult.Count; i++)
                    {
                        trn_basic_measure_dtl dtl;
                        if (i < dtls.Count)
                        {
                            dtl = dtls[i];
                        }
                        else
                        {
                            dtl = new trn_basic_measure_dtl
                            {
                                tbd_create_by = user,
                                tbd_create_date = dateNow
                            };
                            hdr.trn_basic_measure_dtls.Add(dtl);
                        }
                        dtl.tbd_row_id = vsresult[i].rowid;
                        dtl.tbd_en_no = vsresult[i].en;
                        dtl.tbd_date = vsresult[i].vsDate;
                        dtl.tbd_vision_lt = vsresult[i].visionLeft;
                        dtl.tbd_vision_rt = vsresult[i].visionRight;
                        dtl.tbd_bmi = vsresult[i].bmi;
                        dtl.tbd_diastolic = vsresult[i].diastolic;
                        dtl.tbd_height = vsresult[i].height;
                        dtl.tbd_pulse = vsresult[i].pulse;
                        dtl.tbd_rr = vsresult[i].respirationrate;
                        dtl.tbd_systolic = vsresult[i].systolic;
                        dtl.tbd_temp = vsresult[i].temperature;
                        if (vsresult[i].withlen)
                        {
                            dtl.tbd_vision_lvisual_out_lens = null;
                            dtl.tbd_vision_rvisual_out_lens = null;
                            dtl.tbd_vision_lvisual_with_lens = vsresult[i].visionLeft;
                            dtl.tbd_vision_rvisual_with_lens = vsresult[i].visionRight;
                        }
                        else
                        {
                            dtl.tbd_vision_lvisual_out_lens = vsresult[i].visionLeft;
                            dtl.tbd_vision_rvisual_out_lens = vsresult[i].visionRight;
                            dtl.tbd_vision_lvisual_with_lens = null;
                            dtl.tbd_vision_rvisual_with_lens = null;
                        }
                        dtl.tbd_vision_with_lens = vsresult[i].withlen;
                        dtl.tbd_waist = vsresult[i].Waist;
                        dtl.tbd_weight = vsresult[i].weight;
                        dtl.tbd_update_by = user;
                        dtl.tbd_update_date = dateNow;
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("VitalSignCls", "InsertByVitalSignResult", ex.Message);
                throw ex;
            }
        }
        public void updateByVitalSignResult(ref trn_patient_regi pregis, List<APITrakcare.VitalSignResult> vsresult, DateTime dateNow, string user)
        {
            try
            {
                var hdr = pregis.trn_basic_measure_hdrs.FirstOrDefault();
                if (hdr == null)
                {
                    hdr = new trn_basic_measure_hdr();
                    hdr.tbm_create_by = user;
                    hdr.tbm_create_date = dateNow;
                    pregis.trn_basic_measure_hdrs.Add(hdr);
                }
                hdr.tbm_vision_left = null;
                hdr.tbm_vision_right = null;
                hdr.tbm_glass_or_contact = null;
                hdr.tbm_color_blind = null;
                hdr.tbm_vision_lvisual_with_lens = null;
                hdr.tbm_vision_rvisual_with_lens = null;
                hdr.tbm_vision_lvisual_out_lens = null;
                hdr.tbm_vision_rvisual_out_lens = null;
                hdr.tbm_update_by = user;
                hdr.tbm_update_date = dateNow;

                if (vsresult.Count > 0)
                {
                    hdr.tbm_vision_left = convInt(vsresult[0].visionLeft);
                    hdr.tbm_vision_right = convInt(vsresult[0].visionRight);
                    hdr.tbm_glass_or_contact = vsresult[0].withlen ? 'Y' : 'N';
                    hdr.tbm_color_blind = vsresult[0].colorblind == null
                                          ? (char?)null
                                          : vsresult[0].colorblind.ToUpper() == "NA"
                                          ? 'X'
                                          : vsresult[0].colorblind.ToUpper() == "NO"
                                          ? 'N'
                                          : vsresult[0].colorblind.ToUpper() == "AB"
                                          ? 'A'
                                          : (char?)null;
                    if (hdr.tbm_glass_or_contact == 'Y')
                    {
                        hdr.tbm_vision_lvisual_with_lens = vsresult[0].visionLeft;
                        hdr.tbm_vision_rvisual_with_lens = vsresult[0].visionRight;
                    }
                    else
                    {
                        hdr.tbm_vision_lvisual_out_lens = vsresult[0].visionLeft;
                        hdr.tbm_vision_rvisual_out_lens = vsresult[0].visionRight;
                    }

                    var dtls = hdr.trn_basic_measure_dtls.OrderBy(x => x.tbd_id).ToList();
                    int i;
                    for (i = 0; i < vsresult.Count; i++)
                    {
                        trn_basic_measure_dtl dtl;
                        if (i < dtls.Count)
                        {
                            dtl = dtls[i];
                        }
                        else
                        {
                            dtl = new trn_basic_measure_dtl
                            {
                                tbd_create_by = user,
                                tbd_create_date = dateNow
                            };
                            hdr.trn_basic_measure_dtls.Add(dtl);
                        }
                        dtl.tbd_row_id = vsresult[i].rowid;
                        dtl.tbd_en_no = vsresult[i].en;
                        dtl.tbd_date = vsresult[i].vsDate;
                        dtl.tbd_vision_lt = vsresult[i].visionLeft;
                        dtl.tbd_vision_rt = vsresult[i].visionRight;
                        dtl.tbd_bmi = vsresult[i].bmi;
                        dtl.tbd_diastolic = vsresult[i].diastolic;
                        dtl.tbd_height = vsresult[i].height;
                        dtl.tbd_pulse = vsresult[i].pulse;
                        dtl.tbd_rr = vsresult[i].respirationrate;
                        dtl.tbd_systolic = vsresult[i].systolic;
                        dtl.tbd_temp = vsresult[i].temperature;
                        if (vsresult[i].withlen)
                        {
                            dtl.tbd_vision_lvisual_out_lens = null;
                            dtl.tbd_vision_rvisual_out_lens = null;
                            dtl.tbd_vision_lvisual_with_lens = vsresult[i].visionLeft;
                            dtl.tbd_vision_rvisual_with_lens = vsresult[i].visionRight;
                        }
                        else
                        {
                            dtl.tbd_vision_lvisual_out_lens = vsresult[i].visionLeft;
                            dtl.tbd_vision_rvisual_out_lens = vsresult[i].visionRight;
                            dtl.tbd_vision_lvisual_with_lens = null;
                            dtl.tbd_vision_rvisual_with_lens = null;
                        }
                        dtl.tbd_vision_with_lens = vsresult[i].withlen;
                        dtl.tbd_waist = vsresult[i].Waist;
                        dtl.tbd_weight = vsresult[i].weight;
                        dtl.tbd_update_by = user;
                        dtl.tbd_update_date = dateNow;
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("VitalSignCls", "InsertByVitalSignResult", ex.Message);
                throw ex;
            }
        }


        public List<APITrakcare.VitalSignResult> Get3time(List<APITrakcare.VitalSignResult> vsresult, int enrowid)
        {
            try
            {
                bool start = false;
                int count = 0;
                var locationcheckup = new LabClass.MappingLocationCls().Mapping();
                var locationcodecheckup = locationcheckup.Select(x => x.subcode).ToList();
                var vscheckup = vsresult.Where(x => locationcodecheckup.Contains(x.location)).OrderByDescending(x => x.vsDate).ToList();
                var vs3time = new List<APITrakcare.VitalSignResult>();
                foreach (var vs in vscheckup)
                {
                    if (vs.enrowid == enrowid)
                    {
                        start = true;
                    }
                    if (start)
                    {
                        vs3time.Add(vs);
                    }
                    if (count >= 3)
                    {
                        break;
                    }
                }
                return vs3time;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("VitalSignCls", "Get3time", ex.Message);
                throw ex;
            }
        }
        private int? convInt(string val)
        {
            int o;
            if (int.TryParse(val, out o))
            {
                return o;
            }
            else
            {
                return null;
            }
        }

        public void InsertLabVs(ref trn_patient_regi pregis, LabClass.InterpretLab lab, List<APITrakcare.VitalSignResult> vsresult, Class.VitalSignUnitResult vsunits, DateTime dateNow, string user)
        {
            var labvsdate = pregis.trn_patient_vitalsign_labdate;
            if (labvsdate == null)
            {
                pregis.trn_patient_vitalsign_labdate = new trn_patient_vitalsign_labdate();
            }
            pregis.trn_patient_vitalsign_labdate.tpvl_result_date_1 = vsresult.Count() == 0 ? null : vsresult[0].vsDate;
            pregis.trn_patient_vitalsign_labdate.tpvl_result_date_2 = vsresult.Count() <= 1 ? null : vsresult[1].vsDate;
            pregis.trn_patient_vitalsign_labdate.tpvl_result_date_3 = vsresult.Count() <= 2 ? null : vsresult[2].vsDate;
            
            var labvs = pregis.trn_patient_lab_vitalsigns.Where(x => x.tplv_lab_code == lab.code).FirstOrDefault();
            if (labvs == null)
            {
                labvs = new trn_patient_lab_vitalsign
                {
                    tplv_lab_code = lab.code,
                    tplv_create_by = user,
                    tplv_create_date = dateNow
                };
                pregis.trn_patient_lab_vitalsigns.Add(labvs);
            }
            switch (lab.code)
            {
                case "BP001":
                    labvs.tplv_result_1 = vsresult.Count() == 0 ? null : (string.IsNullOrEmpty(vsresult[0].systolic) ? "" : vsresult[0].systolic) + "/" +
                                                                         (string.IsNullOrEmpty(vsresult[0].diastolic) ? "" : vsresult[0].diastolic);
                    labvs.tplv_result_2 = vsresult.Count() <= 1 ? null : (string.IsNullOrEmpty(vsresult[1].systolic) ? "" : vsresult[1].systolic) + "/" +
                                                                         (string.IsNullOrEmpty(vsresult[1].diastolic) ? "" : vsresult[1].diastolic);
                    labvs.tplv_result_3 = vsresult.Count() <= 2 ? null : (string.IsNullOrEmpty(vsresult[2].systolic) ? "" : vsresult[2].systolic) + "/" +
                                                                         (string.IsNullOrEmpty(vsresult[2].diastolic) ? "" : vsresult[2].diastolic);
                    break;
                case "PS001":
                    labvs.tplv_lab_unit_th = "ครั้งต่อนาที";
                    labvs.tplv_lab_unit_en = "beat / minute";
                    labvs.tplv_result_1 = vsresult.Count() == 0 ? null : vsresult[0].pulse;
                    labvs.tplv_result_2 = vsresult.Count() <= 1 ? null : vsresult[1].pulse;
                    labvs.tplv_result_3 = vsresult.Count() <= 2 ? null : vsresult[2].pulse;
                    break;
                case "RP001":
                    labvs.tplv_lab_unit_th = "ครั้งต่อนาที";
                    labvs.tplv_lab_unit_en = "beat / minute";
                    labvs.tplv_result_1 = vsresult.Count() == 0 ? null : vsresult[0].respirationrate;
                    labvs.tplv_result_2 = vsresult.Count() <= 1 ? null : vsresult[1].respirationrate;
                    labvs.tplv_result_3 = vsresult.Count() <= 2 ? null : vsresult[2].respirationrate;
                    break;
                case "TP001":
                    labvs.tplv_lab_unit_th = "องศาเซลเซียส";
                    labvs.tplv_lab_unit_en = "°C";
                    labvs.tplv_result_1 = vsresult.Count() == 0 ? null : vsresult[0].temperature;
                    labvs.tplv_result_2 = vsresult.Count() <= 1 ? null : vsresult[1].temperature;
                    labvs.tplv_result_3 = vsresult.Count() <= 2 ? null : vsresult[2].temperature;
                    break;
            }
            var unit = vsunits.vitalsigns.Where(x => x.code == lab.code).FirstOrDefault();
            if (unit != null)
            {
                labvs.tplv_lab_unit_th = unit.unitlangs.th;
                labvs.tplv_lab_unit_en = unit.unitlangs.en;
            }
            labvs.tplv_summary = lab.summary == null ? null : lab.summary.ToString();
            labvs.tplv_lab_name_th = lab.name_th;
            labvs.tplv_lab_name_en = lab.name_en;
            labvs.tplv_lab_result_th = lab.result_th;
            labvs.tplv_lab_result_en = lab.result_en;
            labvs.tplv_lab_result_jp = lab.result_jp;
            labvs.tplv_normal_range = lab.normalrange;
            labvs.tplv_update_by = user;
            labvs.tplv_update_date = dateNow;
        }
    }
}