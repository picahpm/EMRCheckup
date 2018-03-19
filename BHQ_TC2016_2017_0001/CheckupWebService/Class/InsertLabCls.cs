using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class InsertLabCls
    {
        public bool Insert(int tpr_id, string user)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                    DateTime dateNow = Class.globalCls.GetServerDateTime();
                    var pregis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (pregis != null)
                    {
                        var quesionnaire = pregis.trn_ques_patients.OrderByDescending(x => x.tqp_update_date)
                                                 .Select(x => new LabClass.QuestionnaireResult
                                                 {
                                                     diabetes = x.tqp_ill_med_diab == true ? true : false,
                                                     pregnancy = x.tqp_fwm_pregnancy,
                                                     smoke = x.tqp_his_smok
                                                 }).FirstOrDefault();
                        var arrived = pregis.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date;
                        var checkuplabs = new APITrakcare.GetCheckupLabCls().ByGetCheckupLab(pregis.trn_patient.tpt_hn_no, arrived.AddYears(-5), DateTime.Now);
                        var sex = checkuplabs.sex.FirstOrDefault();

                        int enrowid;
                        int.TryParse(pregis.tpr_en_rowid, out enrowid);
                        var vs = new APITrakcare.GetVitalSignCls().ByGetVitalSignByHN(pregis.trn_patient.tpt_hn_no);
                        var vs3time = new Class.VitalSignCls().Get3time(vs, enrowid);
                        var vsunits = new Class.VitalSignUnitCls().Get();

                        foreach (var plab in pregis.trn_patient_labs)
                        {
                            plab.tpl_status = 'D';
                        }
                        foreach (var es in checkuplabs.episodes)
                        {
                            foreach (var ld in es.labdates)
                            {
                                foreach (var lab in ld.labs)
                                {
                                    var plab = pregis.trn_patient_labs.Where(x => x.tpl_en_no == es.en && x.tpl_lab_no == lab.code).FirstOrDefault();
                                    if (plab == null)
                                    {
                                        plab = new trn_patient_lab
                                        {
                                            tpl_rowid = lab.rowid,
                                            tpl_hn_no = checkuplabs.hn,
                                            tpl_en_no = es.en,
                                            tpl_create_date = dateNow
                                        };
                                        pregis.trn_patient_labs.Add(plab);
                                    }
                                    plab.tpl_patient_age = lab.age;
                                    plab.tpl_patient_sex = checkuplabs.sex;
                                    plab.tpl_lab_result = lab.valuenumber;
                                    plab.tpl_lab_date = ld.labdate;
                                    plab.tpl_head_lab_no = lab.headcode;
                                    plab.tpl_head_lab = lab.headdesc;
                                    plab.tpl_lab_no = lab.code;
                                    plab.tpl_lab_name = lab.labname;
                                    plab.tpl_lab_value = lab.valuenumber;
                                    plab.tpl_lab_unit = lab.unit;
                                    plab.tpl_lab_range = lab.range;
                                    plab.tpl_lab_rsl = lab.valuestring;
                                    plab.tpl_lab_hl = lab.hl;
                                    plab.tpl_lab_frs = lab.frs;
                                    plab.tpl_status = lab.status;
                                    plab.tpl_mhs_id = lab.mhs_id;
                                    plab.tpl_mhs_tname = lab.mhs_tname;
                                    plab.tpl_mhs_ename = lab.mhs_ename;
                                    plab.tpl_update_date = dateNow;
                                    plab.tpl_lab_result_seq = lab.seq;
                                }
                            }

                            if (es.en == pregis.tpr_en_no)
                            {
                                var curvs = vs.Where(x => x.en == pregis.tpr_en_no).OrderByDescending(x => x.vsDate).FirstOrDefault();
                                var result = new LabClass.InterpretLabCls().GetAllLabResult(es, quesionnaire, curvs, checkuplabs.dob, sex);

                                var tpeg = cdc.trn_patient_ass_grps.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                                if (tpeg == null)
                                {
                                    tpeg = new trn_patient_ass_grp
                                    {
                                        tpr_id = tpr_id,
                                        tpeg_create_by = user,
                                        tpeg_create_date = dateNow
                                    };
                                    cdc.trn_patient_ass_grps.InsertOnSubmit(tpeg);
                                }
                                tpeg.tpeg_update_by = user;
                                tpeg.tpeg_update_date = dateNow;

                                foreach (var hdr in tpeg.trn_patient_ass_hdrs)
                                {
                                    foreach (var dtl in hdr.trn_patient_ass_dtls)
                                    {
                                        dtl.tped_status = 'D';
                                    }
                                }
                                foreach (var chart in pregis.trn_patient_lab_charts)
                                {
                                    chart.tplc_active = false;
                                    chart.tplc_update_date = dateNow;
                                }

                                foreach (var grp in result.groups)
                                {
                                    var tpeh = tpeg.trn_patient_ass_hdrs.Where(x => x.tpeh_order_code == grp.code).FirstOrDefault();
                                    if (tpeh == null)
                                    {
                                        tpeh = new trn_patient_ass_hdr
                                        {
                                            tpeh_order_code = grp.code,
                                            tpeh_order_name = grp.name_en,
                                            tpeh_create_by = user,
                                            tpeh_create_date = dateNow
                                        };
                                        tpeg.trn_patient_ass_hdrs.Add(tpeh);
                                    }
                                    tpeh.tpeh_summary = grp.summary;
                                    tpeh.tpeh_pat_education = grp.education;
                                    tpeh.tpeh_status = grp.status;
                                    tpeh.tpeh_update_by = user;
                                    tpeh.tpeh_update_date = dateNow;

                                    //var insVsCls = new Class.InsertLabVsCls(vsincheckup, pregis.tpr_en_no);
                                    List<string> vslabcode = new List<string>
                                    {
                                        "VS001", "PS001", "BM001", "BP001", "RP001", "TP001"
                                    };
                                    foreach (var lab in grp.labs)
                                    {
                                        if (vslabcode.Contains(lab.code))
                                        {
                                            new Class.VitalSignCls().InsertLabVs(ref pregis, lab, vs3time, vsunits, dateNow, user);
                                            //insVsCls.Insert(ref pregis, lab, dateNow, user);
                                        }
                                        else
                                        {
                                            var tped = tpeh.trn_patient_ass_dtls.Where(x => x.tped_lab_code == lab.code).FirstOrDefault();
                                            if (tped == null)
                                            {
                                                tped = new trn_patient_ass_dtl
                                                {
                                                    tped_lab_code = lab.code,
                                                    tped_lab_name = lab.name_en,
                                                    tped_create_by = user,
                                                    tped_create_date = dateNow
                                                };
                                                tpeh.trn_patient_ass_dtls.Add(tped);
                                            }
                                            tped.tped_lab_value = lab.value;
                                            tped.mlr_id = lab.mlr_id;
                                            tped.tped_summary = lab.summary;
                                            tped.tped_lab_unit = lab.unit;
                                            tped.tped_lab_nrange = lab.normalrange;
                                            tped.tped_lab_result_eng = lab.result_en;
                                            tped.tped_lab_result_thai = lab.result_th;
                                            tped.tped_status = lab.status;
                                            tped.tped_update_by = user;
                                            tped.tped_update_date = dateNow;
                                            //  lab Royallife
                                            tped.tped_result_special = lab.result_sp;
                                            tped.tped_cond_special = lab.result_sp_con;
                                            //
                                            if (lab.chartid != null)
                                            {
                                                var chart = pregis.trn_patient_lab_charts.Where(x => x.tplc_lab_no == lab.code).FirstOrDefault();
                                                if (chart == null)
                                                {
                                                    chart = new trn_patient_lab_chart
                                                    {
                                                        tplc_lab_no = lab.code,
                                                        tplc_create_date = dateNow
                                                    };
                                                    pregis.trn_patient_lab_charts.Add(chart);
                                                }
                                                chart.tplc_active = true;
                                                chart.mlch_id = lab.chartid;
                                                chart.tplc_update_date = dateNow;
                                            }
                                        }
                                    }

                                    bool hasrecord = false;
                                    foreach (var tped in tpeh.trn_patient_ass_dtls)
                                    {
                                        if (tped.tped_status == 'D')
                                        {
                                            if (tped.tped_id == 0)
                                            {
                                                tpeh.trn_patient_ass_dtls.Remove(tped);
                                            }
                                            else
                                            {
                                                cdc.trn_patient_ass_dtls.DeleteOnSubmit(tped);
                                            }
                                        }
                                        else if (!hasrecord)
                                        {
                                            hasrecord = true;
                                        }
                                    }

                                    if (!hasrecord)
                                    {
                                        if (tpeh.tpeh_id == 0)
                                        {
                                            tpeg.trn_patient_ass_hdrs.Remove(tpeh);
                                        }
                                        else
                                        {
                                            cdc.trn_patient_ass_hdrs.DeleteOnSubmit(tpeh);
                                        }
                                    }
                                }
                            }
                        }
                        cdc.SubmitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InsertLabCls", "Insert", ex.Message);
                return false;
            }
        }

        public bool RetrieveLabToEmrCheckupWithQuestionnaire(int tpr_id, LabClass.QuestionnaireResult questionnaire, string user)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Class.globalCls.GetServerDateTime();
                    var pregis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    var ques = pregis.trn_ques_patients.FirstOrDefault();
                    if (ques == null)
                    {
                        ques = new trn_ques_patient
                        {
                            tqp_create_by = user,
                            tqp_create_date = dateNow
                        };
                        pregis.trn_ques_patients.Add(ques);
                    }
                    ques.tqp_his_smok = questionnaire.smoke;
                    ques.tqp_fwm_pregnancy = questionnaire.pregnancy;
                    ques.tqp_ill_med_diab = questionnaire.diabetes;
                    ques.tqp_update_by = user;
                    ques.tqp_update_date = dateNow;
                    cdc.SubmitChanges();
                }
                return RetrieveLabToEmrCheckup(tpr_id, user);
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InsertLabCls", "RetrieveLabToEmrCheckupWithQuestionnaire", ex.Message);
                throw ex;
            }
            return false;
        }
        public bool RetrieveLabToEmrCheckup(int tpr_id, string user)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                       System.Threading.Thread.CurrentThread.CurrentCulture
                       = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

                    DateTime dateNow = Class.globalCls.GetServerDateTime();
                    var pregis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (pregis != null)
                    {
                        var visitdate = pregis.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date;
                        var checkuplabs = new APITrakcare.GetCheckupLabCls().ByGetCheckupLab(pregis.trn_patient.tpt_hn_no, visitdate.AddYears(-5), DateTime.Now, pregis.tpr_en_no);//add pregis.tpr_en_no suriya 30/06/2017
                        new Class.PatientLabCls().InsertByCheckupLabResult(ref pregis, checkuplabs, dateNow, user);
                        for (int i = 0; i < pregis.trn_patient_labs.Count; i++)
                        {
                            if (pregis.trn_patient_labs[i].tpl_status == 'D')
                            {
                                if (pregis.trn_patient_labs[i].tpl_id == 0)
                                {
                                    pregis.trn_patient_labs.Remove(pregis.trn_patient_labs[i]);
                                }
                                else
                                {
                                    cdc.trn_patient_labs.DeleteOnSubmit(pregis.trn_patient_labs[i]);
                                }
                            }
                        }

                        int enrowid;
                        int.TryParse(pregis.tpr_en_rowid, out enrowid);
                        var vsresult = new APITrakcare.GetVitalSignCls().ByGetVitalSignByHN(pregis.trn_patient.tpt_hn_no);
                        var vs3time = new Class.VitalSignCls().Get3time(vsresult, enrowid);
                        new Class.VitalSignCls().InsertByVitalSignResult(ref pregis, vs3time, dateNow, user);

                        var vshdr = pregis.trn_basic_measure_hdrs.FirstOrDefault();
                        for (int i = vs3time.Count; i < vshdr.trn_basic_measure_dtls.Count; i++)
                        {
                            if (vshdr.trn_basic_measure_dtls[i].tbd_id == 0)
                            {
                                vshdr.trn_basic_measure_dtls.Remove(vshdr.trn_basic_measure_dtls[i]);
                            }
                            else
                            {
                                cdc.trn_basic_measure_dtls.DeleteOnSubmit(vshdr.trn_basic_measure_dtls[i]);
                            }
                        }
                        cdc.SubmitChanges();

                        foreach (var ep in checkuplabs.episodes)
                        {
                            if (ep.en == pregis.tpr_en_no)
                            {
                                var labconfig = new LabClass.GetLabConfigCls().GetByPaientLab(tpr_id);
                                var sex = checkuplabs.sex.FirstOrDefault();
                                var result = new LabClass.InterpretLabCls().GetAllLabResult(ep, labconfig, checkuplabs.dob, sex);

                                var tpeg = cdc.trn_patient_ass_grps.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                                if (tpeg == null)
                                {
                                    tpeg = new trn_patient_ass_grp
                                    {
                                        tpr_id = tpr_id,
                                        tpeg_create_by = user,
                                        tpeg_create_date = dateNow
                                    };
                                    cdc.trn_patient_ass_grps.InsertOnSubmit(tpeg);
                                }
                                tpeg.tpeg_update_by = user;
                                tpeg.tpeg_update_date = dateNow;

                                foreach (var hdr in tpeg.trn_patient_ass_hdrs)
                                {
                                    foreach (var dtl in hdr.trn_patient_ass_dtls)
                                    { 
                                        if (dtl.tped_lab_AddLabEN == null)    //  //m delete   18/08/2017
                                        {  
                                        dtl.tped_status = 'D';
                                        }
                                    }
                                }
                                foreach (var chart in pregis.trn_patient_lab_charts)
                                {
                                    chart.tplc_active = false;
                                    chart.tplc_update_date = dateNow;
                                }

                                var vsunits = new Class.VitalSignUnitCls().Get();
                                foreach (var grp in result.groups)
                                {
                                    var tpeh = tpeg.trn_patient_ass_hdrs.Where(x => x.tpeh_order_code == grp.code).FirstOrDefault();
                                    if (tpeh == null)
                                    {
                                        tpeh = new trn_patient_ass_hdr
                                        {
                                            tpeh_order_code = grp.code,
                                            tpeh_order_name = grp.name_en,
                                            tpeh_create_by = user,
                                            tpeh_create_date = dateNow
                                        };
                                        tpeg.trn_patient_ass_hdrs.Add(tpeh);
                                    }
                                    tpeh.tpeh_summary = grp.summary;
                                    tpeh.tpeh_pat_education = grp.education;
                                    tpeh.tpeh_status = grp.status;
                                    tpeh.tpeh_update_by = user;
                                    tpeh.tpeh_update_date = dateNow;

                                    List<string> vslabcode = new List<string>
                                    {
                                        "VS001", "PS001", "BM001", "BP001", "RP001", "TP001"
                                    };
                                    foreach (var lab in grp.labs)
                                    {
                                        if (vslabcode.Contains(lab.code))
                                        {
                                            new Class.VitalSignCls().InsertLabVs(ref pregis, lab, vs3time, vsunits, dateNow, user);
                                        }
                                        else
                                        {
                                            var tped = tpeh.trn_patient_ass_dtls.Where(x => x.tped_lab_code == lab.code && x.tped_lab_AddLabEN == null).FirstOrDefault();
                                            if (tped == null)
                                            {
                                                tped = new trn_patient_ass_dtl
                                                {
                                                    tped_lab_code = lab.code,
                                                    tped_lab_name = lab.name_en,
                                                    tped_create_by = user,
                                                    tped_create_date = dateNow
                                                };

                                                

                                                tpeh.trn_patient_ass_dtls.Add(tped);

                                            }
                                            if (lab.status != 'D' || tped.tped_lab_AddLabEN != null)
                                            {
                                                tped.tped_lab_value = lab.value;
                                                tped.mlr_id = lab.mlr_id;
                                                tped.tped_summary = lab.summary;
                                                tped.tped_lab_unit = lab.unit;
                                                tped.tped_lab_nrange = lab.normalrange;
                                                if (tped.tped_lab_result_status == null) { 
                                                tped.tped_lab_result_eng = lab.result_en;
                                                tped.tped_lab_result_thai = lab.result_th;
                                                }
                                                tped.tped_lab_result_jp = lab.result_jp;
                                                tped.tped_status = lab.status;
                                                tped.tped_update_by = user;
                                                tped.tped_update_date = dateNow;
                                                //tped.tped_lab_result_status = null;//m delete   reset field status hidelab
                                                if (lab.chartid != null)
                                                {
                                                    var chart = pregis.trn_patient_lab_charts.Where(x => x.tplc_lab_no == lab.code).FirstOrDefault();
                                                    if (chart == null)
                                                    {
                                                        chart = new trn_patient_lab_chart
                                                        {
                                                            tplc_lab_no = lab.code,
                                                            tplc_create_date = dateNow
                                                        };
                                                        pregis.trn_patient_lab_charts.Add(chart);
                                                    }
                                                    chart.tplc_active = true;
                                                    chart.mlch_id = lab.chartid;
                                                    chart.tplc_update_date = dateNow;
                                                }
                                            }
                                        }
                                    }

                                    bool hasrecord = false;
                                    foreach (var tped in tpeh.trn_patient_ass_dtls)
                                    {
                                        if (tped.tped_status == 'D')
                                        {
                                            //if (tped.tped_lab_AddLabEN == null)    //  //m delete   18/08/2017
                                            //{                                      
                                                if (tped.tped_id == 0)
                                                {
                                                    tpeh.trn_patient_ass_dtls.Remove(tped);
                                                }
                                                else
                                                {
                                                    cdc.trn_patient_ass_dtls.DeleteOnSubmit(tped);
                                                }
                                          //  }

                                            //else if (!hasrecord)
                                            //{
                                            //    hasrecord = true;
                                            //}
                                        }
                                        else if (!hasrecord)
                                        {
                                            hasrecord = true;
                                        }
                                    }

                                    if (!hasrecord)
                                    {

                                        if (tpeh.tpeh_id == 0)
                                        {
                                            tpeg.trn_patient_ass_hdrs.Remove(tpeh);
                                        }
                                        else
                                        {
                                            cdc.trn_patient_ass_hdrs.DeleteOnSubmit(tpeh);
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        cdc.SubmitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InsertLabCls", "Insert", ex.Message);
                return false;
            }
        }
        public void RetrieveLabToEmrCheckupBackGround(int tpr_id, string user)
        {
            System.Threading.Thread currentTimeThread = new System.Threading.Thread(delegate()
            {
                RetrieveLabToEmrCheckup(tpr_id, user);
            });
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start();
        }
    }
}