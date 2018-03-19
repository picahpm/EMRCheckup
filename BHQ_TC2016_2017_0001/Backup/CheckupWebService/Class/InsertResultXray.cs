using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DBCheckup;

namespace CheckupWebService.Class
{
    public partial class InsertResultXray
    {
        private class pathoFile
        {
            public string file { get; set; }
            public DateTime? resultDate { get; set; }
        }

        public void retrieveResultXray(string hn_no, string en_no, DateTime StartDate, DateTime EndDate, bool RetrieveOrder)
        {
            try
            {
                using (InhCheckupDataContext pdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = globalCls.GetServerDateTime();
                    Class.GetTextFileCls cls = new GetTextFileCls();
                    List<Class.GetTextFileCls.ResultXray> result = cls.GetResultXray(hn_no, StartDate, EndDate);
                    if (result.Count() > 0)
                    {
                        trn_patient patient = pdc.trn_patients.Where(x => x.tpt_hn_no == hn_no).FirstOrDefault();
                        if (patient != null)
                        {
                            List<mst_event> mstEvents = pdc.mst_events
                                                           .Where(x => x.mvt_status == 'A')
                                                           .ToList();
                            List<mst_order_plan> mstOrders = pdc.mst_order_plans
                                                                .Where(x => x.mop_status == 'A')
                                                                .ToList();
                            int idXrays = mstEvents.Where(x => x.mvt_code == "XR").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeXray = mstOrders.Where(x => x.mvt_id == idXrays).Select(x => x.mop_item_row_id).ToList();
                            string hnReplace = patient.tpt_hn_no.Replace("-", "");
                            string LinkPacSheet = pdc.mst_project_configs.Where(x => x.mpc_code == "LinkPacSheet").Select(x => x.mpc_value).FirstOrDefault();
                            string LinkPDF = pdc.mst_project_configs.Where(x => x.mpc_code == "LinkPDF").Select(x => x.mpc_value).FirstOrDefault();

                            int idPT = mstEvents.Where(x => x.mvt_code == "PT").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codePT = mstOrders.Where(x => x.mvt_id == idPT).Select(x => x.mop_item_row_id).ToList();
                            string PathPatho = pdc.mst_project_configs.Where(x => x.mpc_code == "PathPatho").Select(x => x.mpc_value).FirstOrDefault();

                            var grpResult = result.GroupBy(x => x.en_no).OrderByDescending(x => x.Key).ToList();

                            int idUU = mstEvents.Where(x => x.mvt_code == "UU").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeUU = mstOrders.Where(x => x.mvt_id == idUU).Select(x => x.mop_item_row_id).ToList();

                            int idUB = mstEvents.Where(x => x.mvt_code == "UB").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeUB = mstOrders.Where(x => x.mvt_id == idUB).Select(x => x.mop_item_row_id).ToList();

                            int idUL = mstEvents.Where(x => x.mvt_code == "UL").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeUL = mstOrders.Where(x => x.mvt_id == idUL).Select(x => x.mop_item_row_id).ToList();

                            int idUW = mstEvents.Where(x => x.mvt_code == "UW").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeUW = mstOrders.Where(x => x.mvt_id == idUW).Select(x => x.mop_item_row_id).ToList();

                            int idBD = mstEvents.Where(x => x.mvt_code == "BD").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeBD = mstOrders.Where(x => x.mvt_id == idBD).Select(x => x.mop_item_row_id).ToList();

                            int idUG = mstEvents.Where(x => x.mvt_code == "UG").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeUG = mstOrders.Where(x => x.mvt_id == idUG).Select(x => x.mop_item_row_id).ToList();

                            int idDM = mstEvents.Where(x => x.mvt_code == "DM").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeDM = mstOrders.Where(x => x.mvt_id == idDM).Select(x => x.mop_item_row_id).ToList();

                            int idES = mstEvents.Where(x => x.mvt_code == "ES").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeES = mstOrders.Where(x => x.mvt_id == idES).Select(x => x.mop_item_row_id).ToList();

                            int idEC = mstEvents.Where(x => x.mvt_code == "EC").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeEC = mstOrders.Where(x => x.mvt_id == idEC).Select(x => x.mop_item_row_id).ToList();

                            int idEK = mstEvents.Where(x => x.mvt_code == "EK").Select(x => x.mvt_id).FirstOrDefault();
                            List<string> codeEK = mstOrders.Where(x => x.mvt_id == idEK).Select(x => x.mop_item_row_id).ToList();

                            trn_patient_regi patientRegis = patient.trn_patient_regis.Where(x => x.tpr_en_no == en_no).FirstOrDefault();
                            if (patientRegis != null)
                            {
                                try
                                {
                                    using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                                    {
                                        foreach (trn_patient_regi pr in patient.trn_patient_regis)
                                        {
                                            int rowid = Convert.ToInt32(patientRegis.tpr_en_rowid);
                                            DataTable dt = ws.GetPTPackage(rowid);
                                            var ptPackage = dt.AsEnumerable()
                                                              .Where(x => x.Field<string>("OSTAT_Code") == "E")
                                                              .ToList();

                                            if (RetrieveOrder)
                                            {
                                                GetPTPackageCls PackageCls = new GetPTPackageCls();
                                                EnumerableRowCollection<DataRow> getPTPackage = dt.AsEnumerable();
                                                List<GetPTPackageCls.MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                                                PackageCls.AddPatientOrderItem(ref patientRegis, "SysXray", dateNow, getPTPackage);
                                                PackageCls.AddPatientOrderSet(ref patientRegis, "SysXray", dateNow, getPTPackage);
                                                PackageCls.AddPatientEvent(ref patientRegis, "SysXray", dateNow, mapOrder);
                                                PackageCls.AddPatientPlan(ref patientRegis, "SysXray", dateNow);
                                                PackageCls.skipReqDoctorOutDepartment(ref patientRegis);
                                                PackageCls.CompleteEcho(ref patientRegis);
                                                PackageCls.skipChangeEstToEcho(ref patientRegis, patientRegis.mhs_id);
                                                PackageCls.checkOrderPMR(ref patientRegis, patientRegis.mhs_id);
                                            }

                                            trn_patient_history_xray xr = patient.trn_patient_history_xrays.Where(x => x.tphx_en_no == pr.tpr_en_no).FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || xr == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeXray.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (xr == null)
                                                    {
                                                        xr = new trn_patient_history_xray();
                                                        xr.tphx_en_no = en_no;
                                                        patient.trn_patient_history_xrays.Add(xr);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    xr.tphx_link = LinkXray;
                                                }
                                            }

                                            trn_patient_history_ultrasound uu = patient.trn_patient_history_ultrasounds
                                                                                       .Where(x => x.tphu_en_no == pr.tpr_en_no &&
                                                                                                   x.tphu_type == "UU")
                                                                                       .FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || uu == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeUU.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (uu == null)
                                                    {
                                                        uu = new trn_patient_history_ultrasound();
                                                        uu.tphu_en_no = en_no;
                                                        uu.tphu_type = "UU";
                                                        patient.trn_patient_history_ultrasounds.Add(uu);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    uu.tphu_link = LinkXray;
                                                }
                                            }

                                            trn_patient_history_ultrasound ub = patient.trn_patient_history_ultrasounds
                                                                                       .Where(x => x.tphu_en_no == pr.tpr_en_no &&
                                                                                                   x.tphu_type == "UB")
                                                                                       .FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || ub == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeUB.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (ub == null)
                                                    {
                                                        ub = new trn_patient_history_ultrasound();
                                                        ub.tphu_en_no = en_no;
                                                        ub.tphu_type = "UB";
                                                        patient.trn_patient_history_ultrasounds.Add(ub);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    ub.tphu_link = LinkXray;
                                                }
                                            }

                                            trn_patient_history_ultrasound ul = patient.trn_patient_history_ultrasounds
                                                                                       .Where(x => x.tphu_en_no == pr.tpr_en_no &&
                                                                                                   x.tphu_type == "UL")
                                                                                       .FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || ul == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeUL.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (ul == null)
                                                    {
                                                        ul = new trn_patient_history_ultrasound();
                                                        ul.tphu_en_no = en_no;
                                                        ul.tphu_type = "UL";
                                                        patient.trn_patient_history_ultrasounds.Add(ul);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    ul.tphu_link = LinkXray;
                                                }
                                            }

                                            trn_patient_history_ultrasound uw = patient.trn_patient_history_ultrasounds
                                                                                   .Where(x => x.tphu_en_no == pr.tpr_en_no &&
                                                                                               x.tphu_type == "UW")
                                                                                   .FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || uw == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeUW.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (uw == null)
                                                    {
                                                        uw = new trn_patient_history_ultrasound();
                                                        uw.tphu_en_no = en_no;
                                                        uw.tphu_type = "UW";
                                                        patient.trn_patient_history_ultrasounds.Add(uw);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    uw.tphu_link = LinkXray;
                                                }
                                            }

                                            trn_patient_history_ugi ug = patient.trn_patient_history_ugis
                                                                                .Where(x => x.tphu_en_no == pr.tpr_en_no)
                                                                                .FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || ug == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeUG.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (ug == null)
                                                    {
                                                        ug = new trn_patient_history_ugi();
                                                        ug.tphu_en_no = en_no;
                                                        patient.trn_patient_history_ugis.Add(ug);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    ug.tphu_link = LinkXray;
                                                }
                                            }

                                            trn_patient_history_mammogram dm = patient.trn_patient_history_mammograms
                                                                                      .Where(x => x.tphm_en_no == pr.tpr_en_no)
                                                                                      .FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || dm == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeDM.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (dm == null)
                                                    {
                                                        dm = new trn_patient_history_mammogram();
                                                        dm.tphm_en_no = en_no;
                                                        patient.trn_patient_history_mammograms.Add(dm);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    dm.tphm_link = LinkXray;
                                                }
                                            }

                                            trn_patient_history_bmd bd = patient.trn_patient_history_bmds
                                                                                .Where(x => x.tphb_en_no == pr.tpr_en_no)
                                                                                .FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || bd == null)
                                            {
                                                string pacSheet = ptPackage.Where(x => codeBD.Contains(x.Field<string>("ARCIM_RowId")))
                                                                           .Select(x => x.Field<string>("OEORI_AccessionNumber")).FirstOrDefault();
                                                if (!string.IsNullOrEmpty(pacSheet))
                                                {
                                                    if (bd == null)
                                                    {
                                                        bd = new trn_patient_history_bmd();
                                                        bd.tphb_en_no = en_no;
                                                        patient.trn_patient_history_bmds.Add(bd);
                                                    }
                                                    string LinkXray = string.Format(LinkPacSheet, pacSheet.Replace("/", "-"), hnReplace);
                                                    bd.tphb_link = LinkXray;
                                                }
                                            }

                                            pathoFile filePatho = ptPackage.Where(x => codePT.Contains(x.Field<string>("ARCIM_RowId")) &&
                                                                                       (x.Field<string>("OEORI_LabTestSetRow") != null && x.Field<string>("OEORI_LabTestSetRow") != "") &&
                                                                                       x.Field<DateTime?>("ExecuteDate") != null)
                                                                           .Select(x => new pathoFile
                                                                           {
                                                                               file = x.Field<string>("OEORI_LabTestSetRow"),
                                                                               resultDate = x.Field<DateTime?>("ExecuteDate")
                                                                           }).FirstOrDefault();
                                            if (filePatho != null)
                                            {
                                                if (!string.IsNullOrEmpty(filePatho.file) && filePatho.resultDate != null)
                                                {
                                                    string LinkPatho = "";
                                                    if (filePatho.resultDate != null)
                                                    {
                                                        LinkPatho = PathPatho + filePatho.resultDate.Value.Date.ToString("yyyyMM") + @"\" + filePatho.file.Replace("||", "_") + ".doc";
                                                    }
                                                    if (LinkPatho != "")
                                                    {
                                                        trn_obstetric_chief obChief = pr.trn_obstetric_chiefs.FirstOrDefault();
                                                        if (obChief != null)
                                                        {
                                                            if (obChief.toc_patho_result == null || obChief.toc_patho_result == false)
                                                            {
                                                                obChief.toc_patho_result = true;
                                                            }
                                                        }

                                                        trn_patient_history_patho pt = patient.trn_patient_history_pathos.Where(x => x.tphp_en_no == pr.tpr_en_no).FirstOrDefault();
                                                        if (pt == null)
                                                        {
                                                            pt = new trn_patient_history_patho();
                                                            pt.tphp_en_no = en_no;
                                                            patient.trn_patient_history_pathos.Add(pt);
                                                        }
                                                        pt.tphp_link = LinkPatho;
                                                    }
                                                }
                                            }

                                            trn_patient_history_est es = patient.trn_patient_history_ests.Where(x => x.tphs_en_no == pr.tpr_en_no).FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || es == null)
                                            {
                                                int countEST = ptPackage.Where(x => codeES.Contains(x.Field<string>("ARCIM_RowId")) &&
                                                                                    x.Field<string>("OSTAT_Code") == "E")
                                                                        .Count();
                                                if (countEST > 0)
                                                {
                                                    if (es == null)
                                                    {
                                                        es = new trn_patient_history_est();
                                                        patient.trn_patient_history_ests.Add(es);
                                                        es.tphs_en_no = pr.tpr_en_no;
                                                    }
                                                }
                                            }

                                            trn_patient_history_echo ec = patient.trn_patient_history_echos.Where(x => x.tphc_en_no == pr.tpr_en_no).FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || ec == null)
                                            {
                                                int countEC = ptPackage.Where(x => codeEC.Contains(x.Field<string>("ARCIM_RowId")) &&
                                                                                   x.Field<string>("OSTAT_Code") == "E")
                                                                       .Count();
                                                if (countEC > 0)
                                                {
                                                    if (ec == null)
                                                    {
                                                        ec = new trn_patient_history_echo();
                                                        patient.trn_patient_history_echos.Add(ec);
                                                        ec.tphc_en_no = pr.tpr_en_no;
                                                    }
                                                }
                                            }

                                            trn_patient_history_ekg ek = patient.trn_patient_history_ekgs.Where(x => x.tphk_en_no == pr.tpr_en_no).FirstOrDefault();
                                            if (pr.tpr_en_no == en_no || ek == null)
                                            {
                                                int countEK = ptPackage.Where(x => codeEK.Contains(x.Field<string>("ARCIM_RowId")) &&
                                                                                   x.Field<string>("OSTAT_Code") == "E")
                                                                       .Count();
                                                if (countEK > 0)
                                                {
                                                    if (ek == null)
                                                    {
                                                        ek = new trn_patient_history_ekg();
                                                        patient.trn_patient_history_ekgs.Add(ek);
                                                        ek.tphk_en_no = pr.tpr_en_no;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    foreach (var re in result)
                                    {
                                        switch (re.mvt_code)
                                        {
                                            case "UG":
                                                trn_ugi_xray ug = patientRegis.trn_ugi_xrays
                                                                              .Where(x => x.tug_order_code == re.ARCIMCode &&
                                                                                          x.tug_en_no == re.en_no)
                                                                              .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || ug == null)
                                                {
                                                    if (ug == null)
                                                    {
                                                        ug = new trn_ugi_xray();
                                                        ug.tug_type = 'N';
                                                        ug.tug_order_code = re.ARCIMCode;
                                                        ug.tug_order_name = re.ARCIMDesc;
                                                        ug.tug_en_no = re.en_no;
                                                        ug.tug_create_date = dateNow;
                                                        patientRegis.trn_ugi_xrays.Add(ug);
                                                    }
                                                    ug.tug_order_date = re.OEORISttDat.Value.Date.Add(re.OEORITimeOrd.Value.Duration());
                                                    ug.tug_result_date = re.RESDateVerified.Value.Date.Add(re.RESTimeVerified.Value.Duration());
                                                    ug.tug_overseen_by = re.SSUSR_Name;
                                                    ug.tug_result = re.resultText == null ? null : (re.resultText.Substring(0, re.resultText.Length >= 8000 ? 8000 : re.resultText.Length));
                                                    ug.tug_path_html = re.PathHTML;
                                                    ug.tug_child_id = re.ChildID;
                                                    ug.tug_create_by = "System";
                                                    ug.tug_update_by = "System";
                                                    ug.tug_update_date = dateNow;
                                                }
                                                break;
                                            case "XR":
                                                trn_chest_xray xr = patientRegis.trn_chest_xrays
                                                                                .Where(x => x.tcx_order_code == re.ARCIMCode &&
                                                                                            x.tcx_en_no == re.en_no)
                                                                                .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || xr == null)
                                                {
                                                    if (xr == null)
                                                    {
                                                        xr = new trn_chest_xray();
                                                        xr.tcx_type = 'N';
                                                        xr.tcx_order_code = re.ARCIMCode;
                                                        xr.tcx_order_name = re.ARCIMDesc;
                                                        xr.tcx_en_no = re.en_no;
                                                        xr.tcx_create_date = dateNow;
                                                        patientRegis.trn_chest_xrays.Add(xr);
                                                    }
                                                    xr.tcx_order_date = re.OEORISttDat.Value.Date.Add(re.OEORITimeOrd.Value.Duration());
                                                    xr.tcx_result_date = re.RESDateVerified.Value.Date.Add(re.RESTimeVerified.Value.Duration());
                                                    xr.tcx_overseen_by = re.SSUSR_Name;
                                                    xr.tcx_result = re.resultText == null ? null : (re.resultText.Substring(0, re.resultText.Length >= 8000 ? 8000 : re.resultText.Length));
                                                    xr.tcx_path_html = re.PathHTML;
                                                    xr.tcx_child_id = re.ChildID;
                                                    xr.tcx_create_by = "System";
                                                    xr.tcx_update_by = "System";
                                                    xr.tcx_update_date = dateNow;
                                                }
                                                break;
                                            case "BD":
                                                trn_bmd bd = patientRegis.trn_bmds
                                                                         .Where(x => x.bmd_order_code == re.ARCIMCode &&
                                                                                     x.bmd_en_no == re.en_no)
                                                                         .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || bd == null)
                                                {
                                                    if (bd == null)
                                                    {
                                                        bd = new trn_bmd();
                                                        bd.bmd_type = 'N';
                                                        bd.bmd_order_code = re.ARCIMCode;
                                                        bd.bmd_order_name = re.ARCIMDesc;
                                                        bd.bmd_en_no = re.en_no;
                                                        bd.bmd_create_date = dateNow;
                                                        patientRegis.trn_bmds.Add(bd);
                                                    }
                                                    bd.bmd_order_date = re.OEORISttDat.Value.Date.Add(re.OEORITimeOrd.Value.Duration());
                                                    bd.bmd_result_date = re.RESDateVerified.Value.Date.Add(re.RESTimeVerified.Value.Duration());
                                                    bd.bmd_overseen_by = re.SSUSR_Name;
                                                    bd.bmd_result = re.resultText == null ? null : (re.resultText.Substring(0, re.resultText.Length >= 8000 ? 8000 : re.resultText.Length));
                                                    bd.bmd_path_html = re.PathHTML;
                                                    bd.bmd_child_id = re.ChildID;
                                                    bd.bmd_create_by = "System";
                                                    bd.bmd_update_by = "System";
                                                    bd.bmd_update_date = dateNow;
                                                }
                                                break;
                                            case "DM":
                                                trn_mammogram dm = patientRegis.trn_mammograms
                                                                               .Where(x => x.tmg_order_code == re.ARCIMCode &&
                                                                                           x.tmg_en_no == re.en_no)
                                                                               .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || dm == null)
                                                {
                                                    if (dm == null)
                                                    {
                                                        dm = new trn_mammogram();
                                                        dm.tmg_type = 'N';
                                                        dm.tmg_order_code = re.ARCIMCode;
                                                        dm.tmg_order_name = re.ARCIMDesc;
                                                        dm.tmg_en_no = re.en_no;
                                                        dm.tmg_create_date = dateNow;
                                                        patientRegis.trn_mammograms.Add(dm);
                                                    }
                                                    dm.tmg_order_date = re.OEORISttDat.Value.Date.Add(re.OEORITimeOrd.Value.Duration());
                                                    dm.tmg_result_date = re.RESDateVerified.Value.Date.Add(re.RESTimeVerified.Value.Duration());
                                                    dm.tmg_birads_cat = (re.biradsCate == null || re.biradsCate.Length < 1) ? "" : re.biradsCate.Substring(0, 1);
                                                    dm.tmg_overseen_by = re.SSUSR_Name;
                                                    dm.tmg_result = re.resultText == null ? null : (re.resultText.Substring(0, re.resultText.Length >= 8000 ? 8000 : re.resultText.Length));
                                                    dm.tmg_path_html = re.PathHTML;
                                                    dm.tmg_child_id = re.ChildID;
                                                    dm.tmg_create_by = "System";
                                                    dm.tmg_update_by = "System";
                                                    dm.tmg_update_date = dateNow;
                                                }
                                                break;
                                            case "UU":
                                            case "UB":
                                            case "UL":
                                            case "UW":
                                                trn_ultrasound us = patientRegis.trn_ultrasounds
                                                                                .Where(x => x.tus_order_code == re.ARCIMCode &&
                                                                                            x.tus_en_no == re.en_no &&
                                                                                            x.tus_ultra_type == re.mvt_code)
                                                                                .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || us == null)
                                                {
                                                    if (us == null)
                                                    {
                                                        us = new trn_ultrasound();
                                                        us.tus_ultra_type = re.mvt_code;
                                                        us.tus_type = 'N';
                                                        us.tus_order_code = re.ARCIMCode;
                                                        us.tus_order_name = re.ARCIMDesc;
                                                        us.tus_en_no = re.en_no;
                                                        us.tus_create_date = dateNow;
                                                        patientRegis.trn_ultrasounds.Add(us);
                                                    }
                                                    us.tus_order_date = re.OEORISttDat.Value.Date.Add(re.OEORITimeOrd.Value.Duration());
                                                    us.tus_result_date = re.RESDateVerified.Value.Date.Add(re.RESTimeVerified.Value.Duration());
                                                    us.tus_overseen_by = re.SSUSR_Name;
                                                    us.tus_result = re.resultText == null ? null : (re.resultText.Substring(0, re.resultText.Length >= 8000 ? 8000 : re.resultText.Length));
                                                    us.tus_update_date = dateNow;
                                                    us.tus_path_html = re.PathHTML;
                                                    us.tus_child_id = re.ChildID;
                                                    us.tus_create_by = "System";
                                                    us.tus_update_by = "System";
                                                }
                                                break;
                                            case "CD":
                                                trn_other_xray ox = patientRegis.trn_other_xrays
                                                                                .Where(x => x.tox_order_code == re.ARCIMCode &&
                                                                                            x.tox_en_no == re.en_no &&
                                                                                            x.tox_room_type == re.mvt_code)
                                                                                .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || ox == null)
                                                {
                                                    if (ox == null)
                                                    {
                                                        ox = new trn_other_xray();
                                                        ox.tox_room_type = re.mvt_code;
                                                        ox.tox_type = 'N';
                                                        ox.tox_order_code = re.ARCIMCode;
                                                        ox.tox_order_name = re.ARCIMDesc;
                                                        ox.tox_en_no = re.en_no;
                                                        ox.tox_create_date = dateNow;
                                                        patientRegis.trn_other_xrays.Add(ox);
                                                    }
                                                    ox.tox_patient_result = re.patient_result;
                                                    ox.tox_patient_comt = re.patient_comt;
                                                    ox.tox_order_date = re.OEORISttDat.Value.Date.Add(re.OEORITimeOrd.Value.Duration());
                                                    ox.tox_result_date = re.RESDateVerified.Value.Date.Add(re.RESTimeVerified.Value.Duration());
                                                    ox.tox_overseen_by = re.SSUSR_Name;
                                                    ox.tox_result = re.resultText == null ? null : (re.resultText.Substring(0, re.resultText.Length >= 8000 ? 8000 : re.resultText.Length));
                                                    ox.tox_path_html = re.PathHTML;
                                                    ox.tox_child_id = re.ChildID;
                                                    ox.tox_create_by = "System";
                                                    ox.tox_update_by = "System";
                                                    ox.tox_update_date = dateNow;
                                                }
                                                break;
                                            case "EC":
                                                trn_patient_history_echo ec = patient.trn_patient_history_echos
                                                                                     .Where(x => x.tphc_en_no == re.en_no)
                                                                                     .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || ec == null)
                                                {
                                                    if (ec == null)
                                                    {
                                                        ec = new trn_patient_history_echo();
                                                        patient.trn_patient_history_echos.Add(ec);
                                                        ec.tphc_en_no = re.en_no;
                                                    }
                                                    ec.tphc_link = LinkPDF + re.RESFileName.Replace(@"\", @"/");
                                                }
                                                break;
                                            case "EK":
                                                trn_patient_history_ekg ek = patient.trn_patient_history_ekgs
                                                                                    .Where(x => x.tphk_en_no == re.en_no)
                                                                                    .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || ek == null)
                                                {
                                                    if (ek == null)
                                                    {
                                                        ek = new trn_patient_history_ekg();
                                                        patient.trn_patient_history_ekgs.Add(ek);
                                                        ek.tphk_en_no = re.en_no;
                                                    }
                                                    ek.tphk_link = LinkPDF + re.RESFileName.Replace(@"\", @"/");
                                                }
                                                break;
                                            case "ES":
                                                trn_patient_history_est es = patient.trn_patient_history_ests
                                                                                    .Where(x => x.tphs_en_no == re.en_no)
                                                                                    .FirstOrDefault();
                                                if (patientRegis.tpr_en_no == re.en_no || es == null)
                                                {
                                                    if (es == null)
                                                    {
                                                        es = new trn_patient_history_est();
                                                        patient.trn_patient_history_ests.Add(es);
                                                        es.tphs_en_no = re.en_no;
                                                    }
                                                    es.tphs_link = LinkPDF + re.RESFileName.Replace(@"\", @"/");
                                                }
                                                break;
                                        }

                                        trn_patient_retrieve ret = patientRegis.trn_patient_retrieves.Where(x => x.tpr_image_type == re.mvt_code).FirstOrDefault();
                                        if (ret == null)
                                        {
                                            ret = new trn_patient_retrieve();
                                            ret.tpr_image_type = re.mvt_code;
                                        }
                                        ret.tpr_flag_retrieve = true;

                                        if (re.en_no == patientRegis.tpr_en_no)
                                        {
                                            List<trn_patient_queue> Queues = patientRegis.trn_patient_queues.Where(x => x.mvt_id == re.mvt_id).ToList();
                                            if (Queues.All(x => x.tps_status == "ED"))
                                            {
                                                foreach (trn_patient_queue queue in Queues)
                                                {
                                                    queue.tps_status = "LR";
                                                }
                                                List<trn_patient_plan> Plans = patientRegis.trn_patient_plans.Where(x => x.mvt_id == re.mvt_id).ToList();
                                                foreach (trn_patient_plan plan in Plans)
                                                {
                                                    plan.tpl_status = 'L';
                                                }
                                            }
                                        }
                                    }
                                    pdc.SubmitChanges();
                                }
                                catch (Exception ex)
                                {
                                    globalCls.MessageError("Webservice", "InsertResultXray", ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "InsertResultXray", ex.Message);
            }
        }
        public void retrieveResultXrayBackground(string hn_no, string en_no, DateTime StartDate, DateTime EndDate, bool RetrieveOrder)
        {
            System.Threading.Thread currentTimeThread = new System.Threading.Thread(delegate()
            {
                retrieveResultXray(hn_no, en_no, StartDate, EndDate, RetrieveOrder);
            });
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start();
        }
    }
}