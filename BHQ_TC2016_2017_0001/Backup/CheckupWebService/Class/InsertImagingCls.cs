using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class InsertImagingCls
    {
        public bool Insert(int tpr_id, string user, List<APITrakcare.ImagingResult> imgResults, List<APITrakcare.PatientOrderSet> orders)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi pRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (pRegis != null)
                    {
                        DateTime dateNow = Class.globalCls.GetServerDateTime();
                        trn_patient patient = pRegis.trn_patient;

                        foreach (var item in imgResults)
                        {
                            switch (item.mvt_code)
                            {
                                case "XR":
                                    trn_patient_history_xray hxr = patient.trn_patient_history_xrays.Where(x => x.tphx_en_no == item.en_no).FirstOrDefault();
                                    if (hxr == null)
                                    {
                                        hxr = new trn_patient_history_xray();
                                        hxr.tphx_en_no = item.en_no;
                                        patient.trn_patient_history_xrays.Add(hxr);
                                    }
                                    hxr.tphx_link = item.Link;

                                    trn_chest_xray xr = pRegis.trn_chest_xrays.Where(x => x.tcx_order_code == item.ARCIMCode && x.tcx_en_no == item.en_no).FirstOrDefault();
                                    if (xr == null)
                                    {
                                        xr = new trn_chest_xray();
                                        xr.tcx_type = 'N';
                                        xr.tcx_order_code = item.ARCIMCode;
                                        xr.tcx_order_name = item.ARCIMDesc;
                                        xr.tcx_en_no = item.en_no;
                                        xr.tcx_create_date = dateNow;
                                        pRegis.trn_chest_xrays.Add(xr);
                                    }
                                    xr.tcx_order_date = item.OEORISttDat.Value.Date.Add(item.OEORITimeOrd.Value.Duration());
                                    xr.tcx_result_date = item.RESDateVerified.Value.Date.Add(item.RESTimeVerified.Value.Duration());
                                    xr.tcx_overseen_by = item.SSUSR_Name;
                                    xr.tcx_result = item.resultText == null ? null : (item.resultText.Substring(0, item.resultText.Length >= 8000 ? 8000 : item.resultText.Length));
                                    xr.tcx_path_html = item.PathHTML;
                                    xr.tcx_child_id = item.ChildID;
                                    xr.tcx_create_by = user;
                                    xr.tcx_update_by = user;
                                    xr.tcx_update_date = dateNow;
                                    break;
                                case "UU":
                                case "UB":
                                case "UL":
                                case "UW":
                                    trn_patient_history_ultrasound hus = patient.trn_patient_history_ultrasounds.Where(x => x.tphu_en_no == item.en_no && x.tphu_type == item.mvt_code).FirstOrDefault();
                                    if (hus == null)
                                    {
                                        hus = new trn_patient_history_ultrasound();
                                        hus.tphu_en_no = item.en_no;
                                        hus.tphu_type = item.mvt_code;
                                        patient.trn_patient_history_ultrasounds.Add(hus);
                                    }
                                    hus.tphu_link = item.Link;

                                    trn_ultrasound us = pRegis.trn_ultrasounds.Where(x => x.tus_order_code == item.ARCIMCode && x.tus_en_no == item.en_no && x.tus_ultra_type == item.mvt_code).FirstOrDefault();
                                    if (us == null)
                                    {
                                        us = new trn_ultrasound();
                                        us.tus_ultra_type = item.mvt_code;
                                        us.tus_type = 'N';
                                        us.tus_order_code = item.ARCIMCode;
                                        us.tus_order_name = item.ARCIMDesc;
                                        us.tus_en_no = item.en_no;
                                        us.tus_create_date = dateNow;
                                        pRegis.trn_ultrasounds.Add(us);
                                    }
                                    us.tus_order_date = item.OEORISttDat.Value.Date.Add(item.OEORITimeOrd.Value.Duration());
                                    us.tus_result_date = item.RESDateVerified.Value.Date.Add(item.RESTimeVerified.Value.Duration());
                                    us.tus_overseen_by = item.SSUSR_Name;
                                    us.tus_result = item.resultText == null ? null : (item.resultText.Substring(0, item.resultText.Length >= 8000 ? 8000 : item.resultText.Length));
                                    us.tus_update_date = dateNow;
                                    us.tus_path_html = item.PathHTML;
                                    us.tus_child_id = item.ChildID;
                                    us.tus_create_by = user;
                                    us.tus_update_by = user;
                                    break;
                                case "UG":
                                    trn_patient_history_ugi hug = patient.trn_patient_history_ugis.Where(x => x.tphu_en_no == item.en_no).FirstOrDefault();
                                    if (hug == null)
                                    {
                                        hug = new trn_patient_history_ugi();
                                        hug.tphu_en_no = item.en_no;
                                        patient.trn_patient_history_ugis.Add(hug);
                                    }
                                    hug.tphu_link = item.Link;

                                    trn_ugi_xray ug = pRegis.trn_ugi_xrays.Where(x => x.tug_order_code == item.ARCIMCode && x.tug_en_no == item.en_no).FirstOrDefault();
                                    if (ug == null)
                                    {
                                        ug = new trn_ugi_xray();
                                        ug.tug_type = 'N';
                                        ug.tug_order_code = item.ARCIMCode;
                                        ug.tug_order_name = item.ARCIMDesc;
                                        ug.tug_en_no = item.en_no;
                                        ug.tug_create_date = dateNow;
                                        pRegis.trn_ugi_xrays.Add(ug);
                                    }
                                    ug.tug_order_date = item.OEORISttDat.Value.Date.Add(item.OEORITimeOrd.Value.Duration());
                                    ug.tug_result_date = item.RESDateVerified.Value.Date.Add(item.RESTimeVerified.Value.Duration());
                                    ug.tug_overseen_by = item.SSUSR_Name;
                                    ug.tug_result = item.resultText == null ? null : (item.resultText.Substring(0, item.resultText.Length >= 8000 ? 8000 : item.resultText.Length));
                                    ug.tug_path_html = item.PathHTML;
                                    ug.tug_child_id = item.ChildID;
                                    ug.tug_create_by = user;
                                    ug.tug_update_by = user;
                                    ug.tug_update_date = dateNow;
                                    break;
                                case "DM":
                                    trn_patient_history_mammogram hdm = patient.trn_patient_history_mammograms.Where(x => x.tphm_en_no == item.en_no).FirstOrDefault();
                                    if (hdm == null)
                                    {
                                        hdm = new trn_patient_history_mammogram();
                                        hdm.tphm_en_no = item.en_no;
                                        patient.trn_patient_history_mammograms.Add(hdm);
                                    }
                                    hdm.tphm_link = item.Link;

                                    trn_mammogram dm = pRegis.trn_mammograms.Where(x => x.tmg_order_code == item.ARCIMCode && x.tmg_en_no == item.en_no).FirstOrDefault();
                                    if (dm == null)
                                    {
                                        dm = new trn_mammogram();
                                        dm.tmg_type = 'N';
                                        dm.tmg_order_code = item.ARCIMCode;
                                        dm.tmg_order_name = item.ARCIMDesc;
                                        dm.tmg_en_no = item.en_no;
                                        dm.tmg_create_date = dateNow;
                                        pRegis.trn_mammograms.Add(dm);
                                    }
                                    dm.tmg_order_date = item.OEORISttDat.Value.Date.Add(item.OEORITimeOrd.Value.Duration());
                                    dm.tmg_result_date = item.RESDateVerified.Value.Date.Add(item.RESTimeVerified.Value.Duration());
                                    dm.tmg_birads_cat = (item.biradsCate == null || item.biradsCate.Length < 1) ? "" : item.biradsCate.Substring(0, 1);
                                    dm.tmg_overseen_by = item.SSUSR_Name;
                                    dm.tmg_result = item.resultText == null ? null : (item.resultText.Substring(0, item.resultText.Length >= 8000 ? 8000 : item.resultText.Length));
                                    dm.tmg_path_html = item.PathHTML;
                                    dm.tmg_child_id = item.ChildID;
                                    dm.tmg_create_by = user;
                                    dm.tmg_update_by = user;
                                    dm.tmg_update_date = dateNow;
                                    break;
                                case "BD":
                                    trn_patient_history_bmd hbd = patient.trn_patient_history_bmds.Where(x => x.tphb_en_no == item.en_no).FirstOrDefault();
                                    if (hbd == null)
                                    {
                                        hbd = new trn_patient_history_bmd();
                                        hbd.tphb_en_no = item.en_no;
                                        patient.trn_patient_history_bmds.Add(hbd);
                                    }
                                    hbd.tphb_link = item.Link;

                                    trn_bmd bd = pRegis.trn_bmds.Where(x => x.bmd_order_code == item.ARCIMCode && x.bmd_en_no == item.en_no).FirstOrDefault();
                                    if (bd == null)
                                    {
                                        bd = new trn_bmd();
                                        bd.bmd_type = 'N';
                                        bd.bmd_order_code = item.ARCIMCode;
                                        bd.bmd_order_name = item.ARCIMDesc;
                                        bd.bmd_en_no = item.en_no;
                                        bd.bmd_create_date = dateNow;
                                        pRegis.trn_bmds.Add(bd);
                                    }
                                    bd.bmd_order_date = item.OEORISttDat.Value.Date.Add(item.OEORITimeOrd.Value.Duration());
                                    bd.bmd_result_date = item.RESDateVerified.Value.Date.Add(item.RESTimeVerified.Value.Duration());
                                    bd.bmd_overseen_by = item.SSUSR_Name;
                                    bd.bmd_result = item.resultText == null ? null : (item.resultText.Substring(0, item.resultText.Length >= 8000 ? 8000 : item.resultText.Length));
                                    bd.bmd_path_html = item.PathHTML;
                                    bd.bmd_child_id = item.ChildID;
                                    bd.bmd_create_by = user;
                                    bd.bmd_update_by = user;
                                    bd.bmd_update_date = dateNow;
                                    break;
                                case "CD":
                                    trn_other_xray ox = pRegis.trn_other_xrays.Where(x => x.tox_order_code == item.ARCIMCode && x.tox_en_no == item.en_no && x.tox_room_type == item.mvt_code).FirstOrDefault();
                                    if (ox == null)
                                    {
                                        ox = new trn_other_xray();
                                        ox.tox_room_type = item.mvt_code;
                                        ox.tox_type = 'N';
                                        ox.tox_order_code = item.ARCIMCode;
                                        ox.tox_order_name = item.ARCIMDesc;
                                        ox.tox_en_no = item.en_no;
                                        ox.tox_create_date = dateNow;
                                        pRegis.trn_other_xrays.Add(ox);
                                    }
                                    ox.tox_patient_result = item.patient_result;
                                    ox.tox_patient_comt = item.patient_comt;
                                    ox.tox_order_date = item.OEORISttDat.Value.Date.Add(item.OEORITimeOrd.Value.Duration());
                                    ox.tox_result_date = item.RESDateVerified.Value.Date.Add(item.RESTimeVerified.Value.Duration());
                                    ox.tox_overseen_by = item.SSUSR_Name;
                                    ox.tox_result = item.resultText == null ? null : (item.resultText.Substring(0, item.resultText.Length >= 8000 ? 8000 : item.resultText.Length));
                                    ox.tox_path_html = item.PathHTML;
                                    ox.tox_child_id = item.ChildID;
                                    ox.tox_create_by = user;
                                    ox.tox_update_by = user;
                                    ox.tox_update_date = dateNow;
                                    break;
                                case "EC":
                                    trn_patient_history_echo hec = patient.trn_patient_history_echos.Where(x => x.tphc_en_no == item.en_no).FirstOrDefault();
                                    if (hec == null)
                                    {
                                        hec = new trn_patient_history_echo();
                                        patient.trn_patient_history_echos.Add(hec);
                                        hec.tphc_en_no = item.en_no;
                                    }
                                    hec.tphc_link = item.Link;
                                    break;
                                case "EK":
                                    trn_patient_history_ekg ek = patient.trn_patient_history_ekgs.Where(x => x.tphk_en_no == item.en_no).FirstOrDefault();
                                    if (ek == null)
                                    {
                                        ek = new trn_patient_history_ekg();
                                        patient.trn_patient_history_ekgs.Add(ek);
                                        ek.tphk_en_no = item.en_no;
                                    }
                                    ek.tphk_link = item.Link;
                                    break;
                                case "ES":
                                    trn_patient_history_est es = patient.trn_patient_history_ests.Where(x => x.tphs_en_no == item.en_no).FirstOrDefault();
                                    if (es == null)
                                    {
                                        es = new trn_patient_history_est();
                                        patient.trn_patient_history_ests.Add(es);
                                        es.tphs_en_no = item.en_no;
                                    }
                                    es.tphs_link = item.Link;
                                    break;
                            }

                            trn_patient_retrieve ret = pRegis.trn_patient_retrieves.Where(x => x.tpr_image_type == item.mvt_code).FirstOrDefault();
                            if (ret == null)
                            {
                                ret = new trn_patient_retrieve();
                                ret.tpr_image_type = item.mvt_code;
                            }
                            ret.tpr_flag_retrieve = true;

                            if (item.en_no == pRegis.tpr_en_no)
                            {
                                List<trn_patient_queue> Queues = pRegis.trn_patient_queues.Where(x => x.mvt_id == item.mvt_id).ToList();
                                if (Queues.All(x => x.tps_status == "ED"))
                                {
                                    foreach (trn_patient_queue queue in Queues)
                                    {
                                        queue.tps_status = "LR";
                                    }
                                    List<trn_patient_plan> Plans = pRegis.trn_patient_plans.Where(x => x.mvt_id == item.mvt_id).ToList();
                                    foreach (trn_patient_plan plan in Plans)
                                    {
                                        plan.tpl_status = 'L';
                                    }
                                }
                            }
                        }

                        if (orders != null)
                        {
                            foreach (var order in orders)
                            {
                                foreach (var item in order.orderitems)
                                {
                                    switch (item.mvt_code)
                                    {
                                        case "PT":
                                            if (!string.IsNullOrEmpty(item.patho))
                                            {
                                                trn_obstetric_chief obChief = pRegis.trn_obstetric_chiefs.FirstOrDefault();
                                                if (obChief == null)
                                                {
                                                    obChief = new trn_obstetric_chief();
                                                    pRegis.trn_obstetric_chiefs.Add(obChief);
                                                }
                                                obChief.toc_patho_result = true;

                                                trn_patient_history_patho hpt = patient.trn_patient_history_pathos.Where(x => x.tphp_en_no == order.en).FirstOrDefault();
                                                if (hpt == null)
                                                {
                                                    hpt = new trn_patient_history_patho();
                                                    hpt.tphp_en_no = order.en;
                                                    patient.trn_patient_history_pathos.Add(hpt);
                                                }
                                                hpt.tphp_link = item.patho;
                                            }
                                            break;
                                        case "XR":
                                            if (!string.IsNullOrEmpty(item.pacsheet))
                                            {
                                                trn_patient_history_xray xr = patient.trn_patient_history_xrays.Where(x => x.tphx_en_no == order.en).FirstOrDefault();
                                                if (xr == null)
                                                {
                                                    xr = new trn_patient_history_xray();
                                                    xr.tphx_en_no = order.en;
                                                    patient.trn_patient_history_xrays.Add(xr);
                                                }
                                                xr.tphx_link = item.pacsheet;
                                            }
                                            break;
                                        case "UU":
                                        case "UB":
                                        case "UL":
                                        case "UW":
                                            if (!string.IsNullOrEmpty(item.pacsheet))
                                            {
                                                trn_patient_history_ultrasound hus = patient.trn_patient_history_ultrasounds.Where(x => x.tphu_en_no == order.en && x.tphu_type == item.mvt_code).FirstOrDefault();
                                                if (hus == null)
                                                {
                                                    hus = new trn_patient_history_ultrasound();
                                                    hus.tphu_en_no = order.en;
                                                    hus.tphu_type = item.mvt_code;
                                                    patient.trn_patient_history_ultrasounds.Add(hus);
                                                }
                                                hus.tphu_link = item.pacsheet;
                                            }
                                            break;
                                        case "UG":
                                            if (!string.IsNullOrEmpty(item.pacsheet))
                                            {
                                                trn_patient_history_ugi ug = patient.trn_patient_history_ugis.Where(x => x.tphu_en_no == order.en).FirstOrDefault();
                                                if (ug == null)
                                                {
                                                    ug = new trn_patient_history_ugi();
                                                    ug.tphu_en_no = order.en;
                                                    patient.trn_patient_history_ugis.Add(ug);
                                                }
                                                ug.tphu_link = item.pacsheet;
                                            }
                                            break;
                                        case "DM":
                                            if (!string.IsNullOrEmpty(item.pacsheet))
                                            {
                                                trn_patient_history_mammogram dm = patient.trn_patient_history_mammograms.Where(x => x.tphm_en_no == order.en).FirstOrDefault();
                                                if (dm == null)
                                                {
                                                    dm = new trn_patient_history_mammogram();
                                                    dm.tphm_en_no = order.en;
                                                    patient.trn_patient_history_mammograms.Add(dm);
                                                }
                                                dm.tphm_link = item.pacsheet;
                                            }
                                            break;
                                        case "BD":
                                            if (!string.IsNullOrEmpty(item.pacsheet))
                                            {
                                                trn_patient_history_bmd bd = patient.trn_patient_history_bmds.Where(x => x.tphb_en_no == order.en).FirstOrDefault();
                                                if (bd == null)
                                                {
                                                    bd = new trn_patient_history_bmd();
                                                    bd.tphb_en_no = order.en;
                                                    patient.trn_patient_history_bmds.Add(bd);
                                                }
                                                bd.tphb_link = item.pacsheet;
                                            }
                                            break;
                                    }
                                }
                            }
                            //foreach (var item in docResults)
                            //{
                            //    if (item.Excuted)
                            //    {
                            //        switch (item.mvt_code)
                            //        {
                            //            case "PT":
                            //                trn_obstetric_chief obChief = pRegis.trn_obstetric_chiefs.FirstOrDefault();
                            //                if (obChief == null)
                            //                {
                            //                    obChief = new trn_obstetric_chief();
                            //                    pRegis.trn_obstetric_chiefs.Add(obChief);
                            //                }
                            //                obChief.toc_patho_result = true;

                            //                trn_patient_history_patho hpt = patient.trn_patient_history_pathos.Where(x => x.tphp_en_no == item.en).FirstOrDefault();
                            //                if (hpt == null)
                            //                {
                            //                    hpt = new trn_patient_history_patho();
                            //                    hpt.tphp_en_no = item.en;
                            //                    patient.trn_patient_history_pathos.Add(hpt);
                            //                }
                            //                hpt.tphp_link = item.Link;
                            //                break;
                            //            case "XR":
                            //                trn_patient_history_xray xr = patient.trn_patient_history_xrays.Where(x => x.tphx_en_no == item.en).FirstOrDefault();
                            //                if (xr == null)
                            //                {
                            //                    xr = new trn_patient_history_xray();
                            //                    xr.tphx_en_no = item.en;
                            //                    patient.trn_patient_history_xrays.Add(xr);
                            //                }
                            //                xr.tphx_link = item.Link;
                            //                break;
                            //            case "UU":
                            //            case "UB":
                            //            case "UL":
                            //            case "UW":
                            //                trn_patient_history_ultrasound hus = patient.trn_patient_history_ultrasounds.Where(x => x.tphu_en_no == item.en && x.tphu_type == item.mvt_code).FirstOrDefault();
                            //                if (hus == null)
                            //                {
                            //                    hus = new trn_patient_history_ultrasound();
                            //                    hus.tphu_en_no = item.en;
                            //                    hus.tphu_type = item.mvt_code;
                            //                    patient.trn_patient_history_ultrasounds.Add(hus);
                            //                }
                            //                hus.tphu_link = item.Link;
                            //                break;
                            //            case "UG":
                            //                trn_patient_history_ugi ug = patient.trn_patient_history_ugis.Where(x => x.tphu_en_no == item.en).FirstOrDefault();
                            //                if (ug == null)
                            //                {
                            //                    ug = new trn_patient_history_ugi();
                            //                    ug.tphu_en_no = item.en;
                            //                    patient.trn_patient_history_ugis.Add(ug);
                            //                }
                            //                ug.tphu_link = item.Link;
                            //                break;
                            //            case "DM":
                            //                trn_patient_history_mammogram dm = patient.trn_patient_history_mammograms.Where(x => x.tphm_en_no == item.en).FirstOrDefault();
                            //                if (dm == null)
                            //                {
                            //                    dm = new trn_patient_history_mammogram();
                            //                    dm.tphm_en_no = item.en;
                            //                    patient.trn_patient_history_mammograms.Add(dm);
                            //                }
                            //                dm.tphm_link = item.Link;
                            //                break;
                            //            case "BD":
                            //                trn_patient_history_bmd bd = patient.trn_patient_history_bmds.Where(x => x.tphb_en_no == item.en).FirstOrDefault();
                            //                if (bd == null)
                            //                {
                            //                    bd = new trn_patient_history_bmd();
                            //                    bd.tphb_en_no = item.en;
                            //                    patient.trn_patient_history_bmds.Add(bd);
                            //                }
                            //                bd.tphb_link = item.Link;
                            //                break;
                            //        }
                            //    }
                            //}
                        }
                    }
                    cdc.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("RetrieveImagingCls", "Retrieve", ex.Message);
                return false;
            }
        }

        public bool Insert(int tpr_id, string user)
        {
            var info = new Class.GetInformationCls().Get(tpr_id);
            if (info != null)
            {
                DateTime dateNow = Class.globalCls.GetServerDateTime();
                DateTime startDate = (info.arrived != null ? info.arrived.Value.Date : dateNow).AddYears(-5);
                var imgResults = new APITrakcare.GetImagingResultCls().ByXrayResultList(info.hn, startDate, dateNow);
                int enRowID = Convert.ToInt32(info.enRowID);
                var orders = new APITrakcare.GetPatientOrderCls().ByGetPTPackage(enRowID);
                bool result = new Class.InsertImagingCls().Insert(tpr_id, user, imgResults, orders);
                return result;
            }
            return false;
        }
    }
}