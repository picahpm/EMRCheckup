using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public partial class GetPTPackageCls
    {
        public GetPTPackageCls()
        {

        }

        public EnumerableRowCollection<DataRow> GetPTPackage(int enRowID)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    return ws.GetPTPackage(enRowID).AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetPTPackageCls", "GetPTPackage(int enRowID)", ex, false);
                return null;
            }
        }
        public bool AddPatientOrderItem(ref trn_patient_regi PatientRegis, string username, DateTime dateNow, EnumerableRowCollection<DataRow> PTPackage)
        {
            try
            {
                foreach (var row in PTPackage)
                {
                    trn_patient_order_item patientOrderItem = PatientRegis.trn_patient_order_items
                                                                          .Where(x => x.toi_key == row.Field<string>("OEORI_RowId"))
                                                                          .FirstOrDefault();
                    if (patientOrderItem == null)
                    {
                        patientOrderItem = new trn_patient_order_item();
                        patientOrderItem.toi_key = row.Field<string>("OEORI_RowId");
                        patientOrderItem.toi_create_date = dateNow;
                        patientOrderItem.toi_create_by = username;
                        PatientRegis.trn_patient_order_items.Add(patientOrderItem);
                    }
                    patientOrderItem.toi_set_row_id = row.Field<string>("ARCOS_RowId");
                    patientOrderItem.toi_item_row_id = row.Field<string>("ARCIM_RowId");
                    patientOrderItem.toi_od_item_code = row.Field<string>("ARCIM_Code");
                    patientOrderItem.toi_od_item_name = row.Field<string>("ARCIM_Desc");
                    patientOrderItem.toi_patho = row.Field<string>("OEORI_LabTestSetRow");
                    patientOrderItem.toi_pac_sheet = row.Field<string>("OEORI_AccessionNumber");
                    patientOrderItem.toi_use_pac = row.Field<string>("ARCIM_Text1") != null;
                    patientOrderItem.toi_status = (row.Field<string>("OSTAT_Code") == "V");
                    patientOrderItem.toi_type = string.IsNullOrEmpty(row.Field<string>("ARCOS_RowId")) ? 'I' : 'S';
                    patientOrderItem.toi_update_date = dateNow;
                    patientOrderItem.toi_update_by = username;
                    patientOrderItem.toi_trakcare_status = row.Field<string>("OSTAT_Code");
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError("GetPTPackageCls", "AddPatientOrderItem", ex, false);
                return false;
            }
        }
        public bool AddPatientOrderSet(ref trn_patient_regi PatientRegis, string username, DateTime dateNow, EnumerableRowCollection<DataRow> PTPackage)
        {
            try
            {
                var grpPackage = PTPackage.Where(x => x.Field<string>("ARCOS_RowId") != null).GroupBy(x => x.Field<string>("ARCOS_RowId"));
                foreach (var grp in grpPackage)
                {
                    trn_patient_order_set PatientOrderSet = PatientRegis.trn_patient_order_sets.Where(x => x.tos_item_row_id == grp.Key).FirstOrDefault();
                    if (PatientOrderSet == null)
                    {
                        PatientOrderSet = new trn_patient_order_set();
                        PatientOrderSet.tos_item_row_id = grp.Key;
                        PatientOrderSet.tos_create_date = dateNow;
                        PatientOrderSet.tos_create_by = username;
                        PatientRegis.trn_patient_order_sets.Add(PatientOrderSet);
                    }
                    PatientOrderSet.tos_od_set_code = grp.FirstOrDefault().Field<string>("ARCOS_Code");
                    PatientOrderSet.tos_od_set_name = grp.FirstOrDefault().Field<string>("ARCOS_Desc");
                    PatientOrderSet.tos_status = grp.All(y => y.Field<string>("OSTAT_Code") == "D") ? false : true;
                    PatientOrderSet.tos_update_date = dateNow;
                    PatientOrderSet.tos_update_by = username;
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError("GetPTPackageCls", "AddPatientOrderSet", ex, false);
                return false;
            }
        }
        public List<MapOrderEvent> MapEvent(EnumerableRowCollection<DataRow> PTPackage)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var mapEvent = (from pack in PTPackage
                                    join mop in cdc.mst_order_plans
                                    on pack.Field<string>("ARCIM_RowId") equals mop.mop_item_row_id
                                    where mop.mop_status == 'A'
                                    select new MapOrderEvent
                                    {
                                        mop_id = mop.mop_id,
                                        mvt_id = mop.mvt_id,
                                        status = pack.Field<string>("OSTAT_Code"),
                                        use_pac = pack.Field<string>("ARCIM_Text1") != null ? true : false,
                                        pac_sheet = pack.Field<string>("OEORI_AccessionNumber"),
                                        patho = pack.Field<string>("OEORI_LabTestSetRow"),
                                        tk_orderitem_row_id = pack.Field<string>("OEORI_RowId"),
                                        excute_date = pack.Field<DateTime?>("ExecuteDate")
                                    }).ToList();
                    return mapEvent;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetPTPackageCls", "MapEvent", ex, false);
                return null;
            }
        }
        public bool AddPatientEvent(ref trn_patient_regi PatientRegis, string username, DateTime dateNow, List<MapOrderEvent> mapEvent)
        {
            try
            {
                if (mapEvent != null)
                {
                    var grp = mapEvent.GroupBy(x => x.mvt_id)
                                      .Select(x => x.OrderBy(y => y.status == "E" ? 0 :
                                                                  y.status == "V" ? 1 : 2)
                                      .FirstOrDefault());
                    foreach (var g in grp)
                    {
                        trn_patient_event patientEvent = PatientRegis.trn_patient_events.Where(x => x.mvt_id == g.mvt_id).FirstOrDefault();
                        switch (g.status)
                        {
                            case "D":
                                if (patientEvent != null)
                                {
                                    PatientRegis.trn_patient_events.Remove(patientEvent);
                                }
                                break;
                            case "E":
                                if (patientEvent == null)
                                {
                                    patientEvent = new trn_patient_event()
                                    {
                                        mvt_id = g.mvt_id,
                                        tpl_status = 'P',
                                        tpl_create_by = username,
                                        tpl_create_date = dateNow
                                    };
                                    PatientRegis.trn_patient_events.Add(patientEvent);
                                }
                                patientEvent.tpl_status = 'P';
                                patientEvent.tpl_update_by = username;
                                patientEvent.tpl_update_date = dateNow;
                                break;
                            case "V":
                                if (patientEvent == null)
                                {
                                    patientEvent = new trn_patient_event()
                                    {
                                        mvt_id = g.mvt_id,
                                        tpl_status = 'N',
                                        tpl_create_by = username,
                                        tpl_create_date = dateNow
                                    };
                                    PatientRegis.trn_patient_events.Add(patientEvent);
                                }
                                patientEvent.tpl_status = 'N';
                                patientEvent.tpl_update_by = username;
                                patientEvent.tpl_update_date = dateNow;
                                break;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError("GetPTPackageCls", "AddPatientEvent", ex, false);
                return false;
            }
        }
        public bool AddPatientPlan(ref trn_patient_regi PatientRegis, string username, DateTime dateNow, List<MapOrderEvent> mapEvent)
        {
            try
            {
                if (mapEvent != null)
                {
                    foreach (var map in mapEvent)
                    {
                        List<trn_patient_plan> listPlan = PatientRegis.trn_patient_plans
                                                                      .Where(x => x.tpl_key == map.tk_orderitem_row_id &&
                                                                                  x.mop_id == map.mop_id)
                                                                      .ToList();
                        switch (map.status)
                        {
                            case "D":
                                {
                                    foreach (trn_patient_plan plan in listPlan)
                                    {
                                        PatientRegis.trn_patient_plans.Remove(plan);
                                    }
                                }
                                break;
                            case "V":
                                {
                                    if (listPlan.Count() > 0)
                                    {
                                        foreach (trn_patient_plan PatientPlan in listPlan)
                                        {
                                            PatientPlan.mvt_id = map.mvt_id;
                                        }
                                    }
                                    else
                                    {
                                        trn_patient_plan patientPlan = PatientRegis.trn_patient_plans
                                                                                   .Where(x => x.mvt_id == map.mvt_id)
                                                                                   .FirstOrDefault();
                                        if (patientPlan == null)
                                        {
                                            trn_patient_queue patientQueue = PatientRegis.trn_patient_queues
                                                                                .Where(x => x.mvt_id == map.mvt_id &&
                                                                                            x.tps_status == "ED" && x.tps_ns_status == null)
                                                                                .FirstOrDefault();
                                            if (patientQueue == null)
                                            {
                                                PatientRegis.trn_patient_plans.Add(new trn_patient_plan
                                                {
                                                    mvt_id = map.mvt_id,
                                                    tpl_status = 'N',
                                                    tpl_by = 'A',
                                                    tpl_new = false,
                                                    tpl_use_pac = map.use_pac,
                                                    tpl_patho = pathoReplace(map.patho, map.excute_date),
                                                    tpl_pac_sheet = map.pac_sheet,
                                                    tpl_create_by = username,
                                                    tpl_create_date = dateNow,
                                                    tpl_key = map.tk_orderitem_row_id,
                                                    mop_id = map.mop_id
                                                });
                                            }
                                            else
                                            {
                                                PatientRegis.trn_patient_plans.Add(new trn_patient_plan
                                                {
                                                    mvt_id = map.mvt_id,
                                                    tpl_status = 'P',
                                                    tpl_by = 'A',
                                                    tpl_new = false,
                                                    tpl_use_pac = map.use_pac,
                                                    tpl_patho = pathoReplace(map.patho, map.excute_date),
                                                    tpl_pac_sheet = map.pac_sheet,
                                                    tpl_create_by = username,
                                                    tpl_create_date = dateNow,
                                                    tpl_key = map.tk_orderitem_row_id,
                                                    mop_id = map.mop_id
                                                });
                                            }
                                        }
                                        else
                                        {
                                            PatientRegis.trn_patient_plans.Add(new trn_patient_plan
                                            {
                                                mvt_id = map.mvt_id,
                                                tpl_status = patientPlan.tpl_status,
                                                tpl_by = patientPlan.tpl_by,
                                                tpl_new = false,
                                                tpl_use_pac = map.use_pac,
                                                tpl_patho = pathoReplace(map.patho, map.excute_date),
                                                tpl_pac_sheet = map.pac_sheet,
                                                tpl_create_by = username,
                                                tpl_create_date = dateNow,
                                                tpl_key = map.tk_orderitem_row_id,
                                                tpl_skip = patientPlan.tpl_skip,
                                                tpl_skip_seq = patientPlan.tpl_skip_seq,
                                                mop_id = map.mop_id
                                            });
                                        }
                                    }
                                }
                                break;
                            case "E":
                                {
                                    //if (listPlan.Count() > 0)
                                    //{
                                    //    foreach (trn_patient_plan PatientPlan in listPlan)
                                    //    {
                                    //        PatientPlan.mvt_id = map.mvt_id;
                                    //        PatientPlan.tpl_status = 'P';
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    PatientRegis.trn_patient_plans.Add(new trn_patient_plan
                                    //    {
                                    //        mvt_id = map.mvt_id,
                                    //        tpl_status = 'P',
                                    //        tpl_by = 'A',
                                    //        tpl_new = false,
                                    //        tpl_use_pac = map.use_pac,
                                    //        tpl_patho = pathoReplace(map.patho, tmpArriveDate),
                                    //        tpl_pac_sheet = map.pac_sheet,
                                    //        tpl_create_by = username,
                                    //        tpl_create_date = dateNow,
                                    //        tpl_key = map.tk_orderitem_row_id,
                                    //        mop_id = map.mop_id
                                    //    });
                                    //}
                                }
                                break;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError("GetPTPackageCls", "AddPatientPlan", ex, false);
                return false;
            }
        }

        public string pathoReplace(string strpatho, DateTime? excute_date)
        {
            if (string.IsNullOrEmpty(strpatho) || excute_date == null) return strpatho;
            if (strpatho.Trim().Length == 0) { return ""; }
            if (strpatho.IndexOf("||") > -1)
            {
                var strdata = excute_date.Value.ToString("yyyyMM") + @"\" + strpatho.Replace("||", "_") + ".doc";
                return strdata;
            }
            else
            {
                return strpatho;
            }
        }
        public bool skipReqDoctorOutDepartment(ref trn_patient_regi tpr)
        {
            try
            {
                if (tpr.tpr_req_doctor == 'Y' && tpr.tpr_req_inorout_doctor == "UT")
                {
                    int pe_mvt_id = new EmrClass.GetDataMasterCls().GetMstEvent("PE").mvt_id;
                    List<trn_patient_plan> planPE = tpr.trn_patient_plans.Where(x => x.mvt_id == pe_mvt_id).ToList();
                    foreach (trn_patient_plan patient_plan in planPE)
                    {
                        trn_patient_plan remove_plan = patient_plan;
                        if (patient_plan.tpl_id != 0)
                        {
                            remove_plan = tpr.trn_patient_plans.Where(x => x.tpl_id == patient_plan.tpl_id).FirstOrDefault();
                        }
                        tpr.trn_patient_plans.Remove(remove_plan);
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetDataFromWSTrakCare", "skipReqDoctorOutDepartment", ex, false);
                return false;
            }
        }
        public bool CompleteEcho(ref trn_patient_regi tpr)
        {
            try
            {
                EmrClass.GetDataMasterCls getMaster = new EmrClass.GetDataMasterCls();
                int echo_mvt_id = getMaster.GetMstEvent("EC").mvt_id;
                foreach (trn_patient_plan plan in tpr.trn_patient_plans)
                {
                    if (plan.mvt_id == echo_mvt_id)
                    {
                        plan.tpl_status = 'P';
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError("GetDataFromWSTrakCare", "CompleteEcho(ref trn_patient_regi tpr, int mhs_id)", ex, false);
                return false;
            }
        }
        public bool skipChangeEstToEcho(ref trn_patient_regi tpr, int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    List<string> set_row_id_skip = cdc.mst_config_dtls
                                                      .Where(x => x.mst_config_hdr.mfh_code == "ECHO" &&
                                                                  x.mst_config_hdr.mhs_id == mhs_id &&
                                                                  x.mst_config_hdr.mfh_status == 'A' &&
                                                                  (x.mst_config_hdr.mfh_effective_date == null ? true : dateNow >= x.mst_config_hdr.mfh_effective_date.Value.Date) &&
                                                                  (x.mst_config_hdr.mfh_expire_date == null ? true : dateNow <= x.mst_config_hdr.mfh_expire_date.Value))
                                                      .Select(x => x.mfd_text).ToList();
                    trn_patient_order_set order_skip_est_to_echo = tpr.trn_patient_order_sets
                                                                      .Where(x => set_row_id_skip.Contains(x.tos_od_set_code) &&
                                                                                  x.tos_status == true).FirstOrDefault();
                    if (order_skip_est_to_echo != null)
                    {
                        EmrClass.GetDataMasterCls getMaster = new EmrClass.GetDataMasterCls();
                        int est_mvt_id = getMaster.GetMstEvent("ES").mvt_id;
                        int echo_mvt_id = getMaster.GetMstEvent("EC").mvt_id;
                        List<trn_patient_plan> planEST = tpr.trn_patient_plans.Where(x => x.mvt_id == est_mvt_id).ToList();
                        foreach (trn_patient_plan PatientPlan in planEST)
                        {
                            PatientPlan.mvt_id = echo_mvt_id;
                            PatientPlan.tpl_status = 'P';
                        }
                        int pe_mvt_id = getMaster.GetMstEvent("PE").mvt_id;
                        List<trn_patient_plan> planPE = tpr.trn_patient_plans
                                                                .Where(x => x.mvt_id == pe_mvt_id).ToList();
                        foreach (var plan in planPE)
                        {
                            tpr.trn_patient_plans.Remove(plan);
                        }
                        try
                        {
                            List<trn_patient_event> eventEST = tpr.trn_patient_events.Where(x => x.mvt_id == est_mvt_id).ToList();
                            foreach (var ev in eventEST)
                            {
                                tpr.trn_patient_events.Remove(ev);
                            }
                        }
                        catch
                        {

                        }
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetDataFromWSTrakCare", "skipChangeEstToEcho", ex, false);
                return false;
            }
        }
        public bool checkOrderPMR(ref trn_patient_regi tpr, int mhs_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    List<string> set_row_id_pmr = cdc.mst_config_dtls
                                                     .Where(x => x.mst_config_hdr.mfh_code == "EPMR" &&
                                                                 x.mst_config_hdr.mhs_id == mhs_id &&
                                                                 x.mst_config_hdr.mfh_status == 'A' &&
                                                                 (x.mst_config_hdr.mfh_effective_date == null ? true : dateNow >= x.mst_config_hdr.mfh_effective_date.Value.Date) &&
                                                                 (x.mst_config_hdr.mfh_expire_date == null ? true : dateNow <= x.mst_config_hdr.mfh_expire_date.Value))
                                                     .Select(x => x.mfd_text).ToList();
                    trn_patient_order_set set_pmr = tpr.trn_patient_order_sets.Where(x => set_row_id_pmr.Contains(x.tos_od_set_code)).FirstOrDefault();
                    if (set_pmr != null)
                    {
                        tpr.tpr_PRM = true;
                        tpr.tpr_PRM_doctor = false;
                        return true;
                    }
                    else
                    {
                        tpr.tpr_PRM = false;
                        tpr.tpr_PRM_doctor = false;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetDataFromWSTrakCare", "checkOrderPMR", ex, false);
                return false;
            }
        }
        public bool setRelationOrderSet(ref trn_patient_regi tpr)
        {
            try
            {
                List<trn_patient_order_set> list_order_set = tpr.trn_patient_order_sets.ToList();
                foreach (trn_patient_order_set order_set in list_order_set)
                {
                    if (order_set.tos_id != 0)
                    {
                        List<int> list_order_item_id = tpr.trn_patient_order_items
                                                          .Where(x => x.toi_set_row_id == order_set.tos_item_row_id)
                                                          .Select(x => x.toi_id).ToList();
                        tpr.trn_patient_order_items.Where(x => list_order_item_id.Contains(x.toi_id)).ToList()
                           .ForEach(x => x.tos_id = order_set.tos_id);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.MessageError("GetDataFromWSTrakCare", "setRelationOrderSet", ex, false);
                return false;
            }
        }
    }
}
