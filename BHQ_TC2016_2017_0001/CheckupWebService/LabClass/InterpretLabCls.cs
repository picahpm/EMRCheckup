using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.LabClass
{
    public class InterpretLabCls
    {
        public InterpretResult GetResult(MapLab lab, LabConfigResult labconfig, double ages, char sex)
        {
            try
            {
                InterpretResult result = new InterpretResult();
                if (lab.id != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        var mla = cdc.mst_lab_ages
                                     .Where(x => x.mlb_id == lab.id &&
                                                 x.mla_max_age.Value + (x.mla_max_day.Value / 365) >= ages &&
                                                 x.mla_min_age.Value + (x.mla_min_day.Value / 365) <= ages &&
                                                 x.mla_sex == sex &&
                                                 x.mla_status == 'A')
                                     .FirstOrDefault();

                        if (mla != null)
                        {
                            result.unit = mla.mla_vstand_unit;
                            if (!new List<string> { "N7007", "N0390" }.Contains(lab.code)) //CEA
                            {
                                result.normalrange = mla.mla_vstand_nrange;
                            }
                            else
                            {
                                var smoke = labconfig.labconfigs.Where(x => x.code == "|SMOKE|").Select(x => x.value).FirstOrDefault();
                                if (smoke == @"""True""")
                                {
                                    result.normalrange = "Smoke(0-5.5)";
                                }
                                else
                                {
                                    result.normalrange = mla.mla_vstand_nrange;
                                }
                            }

                            if (lab.usechart)
                            {
                                result.displayvalue = lab.valuedisplay;
                                result.chartmin = mla.mla_value_min;
                                result.chartmax = mla.mla_value_max;
                                result.normalmin = mla.mla_vstand_min;
                                result.normalmax = mla.mla_vstand_max;

                                if (!new List<string> { "N7007", "N0390" }.Contains(lab.code)) //CEA
                                {
                                    result.normalrange = mla.mla_vstand_nrange;
                                }
                                else
                                {
                                    var smoke = labconfig.labconfigs.Where(x => x.code == "|SMOKE|").Select(x => x.value).FirstOrDefault();
                                    if (smoke == @"""True""")
                                    {
                                        result.normalmin = mla.mla_vstand_min;
                                        result.normalmax = mla.mla_vstand_max;
                                    }
                                    else
                                    {
                                        result.normalmin = 0;
                                        result.normalmax = 3.8;
                                    }
                                }


                                double o;
                                if (double.TryParse(lab.valueinterpret, out o))
                                {
                                    result.interpretvalue = o;
                                }
                                else {
                                    if (lab.valuedisplay.Trim().Substring(0, 1) == "<") { result.interpretvalue = Convert.ToDouble(lab.valuedisplay.Replace("<", "0")); }
                                    if (lab.valuedisplay.Trim().Substring(0, 1) == ">") { result.interpretvalue = Convert.ToDouble(lab.valuedisplay.Replace(">", "0")); }
                                   
                                    //lab.Field<string>("TST_DTA").Trim().Substring(0, 1) == "."
                                //                           //              ? "0" + lab.Field<string>("TST_DTA").Trim()
                                }
                            }

                            var mlps = mla.mst_lab_results.Where(x => x.mlp_status == 'A' && x.mlp_flag_special==false).OrderBy(x => x.mlp_cond_seq).ToList();
                            foreach (var mlp in mlps)
                            {
                                string condition = mlp.mlp_condition;
                                foreach (var config in labconfig.labconfigs)
                                {
                                    condition = condition.Replace(config.code, config.value);
                                }
                                if (!string.IsNullOrEmpty(lab.valueinterpret))
                                {
                                    condition = condition = condition.Replace("?", lab.valueinterpret);
                                }
                                condition = condition = condition.Replace("\"", "'");
                                //bool val = CompilerStringCls.CheckCondition(condition);
                                bool? val = cdc.emrCalculateCondition(condition).Select(x => x.rs).FirstOrDefault();
                                if ((bool)val)
                                {
                                    result.mlr_id = mlp.mlr_id;
                                    result.summary = mlp.mlp_summary;
                                    if (result.mlr_id != null)
                                    {
                                        var recom = cdc.mst_lab_recoms
                                                       .Where(x => x.mlr_id == result.mlr_id)
                                                       .Select(x => new
                                                       {
                                                           en = x.mlr_en_name,
                                                           th = x.mlr_th_name,
                                                           jp = x.mlr_jp_name
                                                       }).FirstOrDefault();
                                        if (recom != null)
                                        {
                                            result.result_en = recom.en;
                                            result.result_th = recom.th;
                                            result.result_jp = recom.jp;
                                        }
                                    }
                                    if (new List<string> { "S0723", "N0390" }.Contains(lab.code)) //CEA
                                    {
                                        result.normalrange = mla.mla_vstand_nrange;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InterpretLabCls", "GetResults", ex.Message);
                throw ex;
            }
        }
        // getlab old
    public InterpretResult GetResult_old(MapLab lab, LabConfigResult labconfig, double ages, char sex)
        {
            try
            {
                InterpretResult result = new InterpretResult();
                if (lab.id != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        var mla = cdc.z_mst_lab_ages
                                     .Where(x => x.mlb_id == lab.id &&
                                                 x.mla_max_age.Value + (x.mla_max_day.Value / 365) >= ages &&
                                                 x.mla_min_age.Value + (x.mla_min_day.Value / 365) <= ages &&
                                                 x.mla_sex == sex &&
                                                 x.mla_status == 'A')
                                     .FirstOrDefault();

                        if (mla != null)
                        {
                            result.unit = mla.mla_vstand_unit;
                            if (!new List<string> { "N7007", "N0390" }.Contains(lab.code)) //CEA
                            {
                                result.normalrange = mla.mla_vstand_nrange;
                            }
                           
                            else
                            {
                                var smoke = labconfig.labconfigs.Where(x => x.code == "|SMOKE|").Select(x => x.value).FirstOrDefault();
                                if (smoke == @"""True""")
                                {
                                    result.normalrange = "Smoke(0-5.5)";
                                }
                                else
                                {
                                    result.normalrange = mla.mla_vstand_nrange;
                                }
                            }

                            if (lab.usechart)
                            {
                                result.displayvalue = lab.valuedisplay;
                                result.chartmin = mla.mla_value_min;
                                result.chartmax = mla.mla_value_max;
                                result.normalmin = mla.mla_vstand_min;
                                result.normalmax = mla.mla_vstand_max;

                                if (!new List<string> { "N7007", "N0390" }.Contains(lab.code)) //CEA
                                {
                                    result.normalrange = mla.mla_vstand_nrange;
                                }
                                else
                                {
                                    var smoke = labconfig.labconfigs.Where(x => x.code == "|SMOKE|").Select(x => x.value).FirstOrDefault();
                                    if (smoke == @"""True""")
                                    {
                                        result.normalmin = mla.mla_vstand_min;
                                        result.normalmax = mla.mla_vstand_max;
                                    }
                                    else
                                    {
                                        result.normalmin = 0;
                                        result.normalmax = 3.8;
                                    }
                                }


                                double o;
                                if (double.TryParse(lab.valueinterpret, out o))
                                {
                                    result.interpretvalue = o;
                                }
                                else
                                {
                                    if (lab.valuedisplay.Trim().Substring(0, 1) == "<") { result.interpretvalue = Convert.ToDouble(lab.valuedisplay.Replace("<", "0")); }
                                    if (lab.valuedisplay.Trim().Substring(0, 1) == ">") { result.interpretvalue = Convert.ToDouble(lab.valuedisplay.Replace(">", "0")); }
                                    //lab.Field<string>("TST_DTA").Trim().Substring(0, 1) == "."
                                    //                           //              ? "0" + lab.Field<string>("TST_DTA").Trim()
                                }
                            }

                            var mlps = mla.z_mst_lab_results.Where(x => x.mlp_status == 'A').OrderBy(x => x.mlp_cond_seq).ToList();
                            foreach (var mlp in mlps)
                            {
                                string condition = mlp.mlp_condition;
                                foreach (var config in labconfig.labconfigs)
                                {
                                    condition = condition.Replace(config.code, config.value);
                                }
                                if (!string.IsNullOrEmpty(lab.valueinterpret))
                                {
                                    condition = condition = condition.Replace("?", lab.valueinterpret);
                                }
                                condition = condition = condition.Replace("\"", "'");
                                //bool val = CompilerStringCls.CheckCondition(condition);
                                bool? val = cdc.emrCalculateCondition(condition).Select(x => x.rs).FirstOrDefault();
                                if ((bool)val)
                                {
                                    result.mlr_id = mlp.mlr_id;
                                    result.summary = mlp.mlp_summary;
                                    if (result.mlr_id != null)
                                    {
                                        var recom = cdc.z_mst_lab_recoms
                                                       .Where(x => x.mlr_id == result.mlr_id)
                                                       .Select(x => new
                                                       {
                                                           en = x.mlr_en_name,
                                                           th = x.mlr_th_name,
                                                           jp = x.mlr_jp_name
                                                       }).FirstOrDefault();
                                        if (recom != null)
                                        {
                                            result.result_en = recom.en;
                                            result.result_th = recom.th;
                                            result.result_jp = recom.jp;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InterpretLabCls", "GetResults", ex.Message);
                throw ex;
            }
        }

    //Get labSpecail  //  lab Royallife
        public InterpretResult GetResult_Special(MapLab lab, LabConfigResult labconfig, double ages, char sex)
        {
            try
            {
                InterpretResult result = new InterpretResult();
                if (lab.id != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        var mla = cdc.mst_lab_ages
                                     .Where(x => x.mlb_id == lab.id &&
                                                 x.mla_max_age.Value + (x.mla_max_day.Value / 365) >= ages &&
                                                 x.mla_min_age.Value + (x.mla_min_day.Value / 365) <= ages &&
                                                 x.mla_sex == sex &&
                                                 x.mla_status == 'A')
                                     .FirstOrDefault();

                        if (mla != null)
                        {
                            result.unit = mla.mla_vstand_unit;
                            

                            if (!new List<string> { "N7007", "N0390" }.Contains(lab.code)) //CEA
                            {
                                result.normalrange = mla.mla_vstand_nrange;
                            }
                            else
                            {
                                var smoke = labconfig.labconfigs.Where(x => x.code == "|SMOKE|").Select(x => x.value).FirstOrDefault();
                                if (smoke == @"""True""")
                                {
                                    result.normalrange = "Smoke(0-5.5)";
                                }
                                else
                                {
                                    result.normalrange = mla.mla_vstand_nrange;
                                }
                            }

                            if (lab.usechart)
                            {
                                result.displayvalue = lab.valuedisplay;
                                result.chartmin = mla.mla_value_min;
                                result.chartmax = mla.mla_value_max;
                                result.normalmin = mla.mla_vstand_min;
                                result.normalmax = mla.mla_vstand_max;
                                
                                if (!new List<string> { "N7007", "N0390" }.Contains(lab.code)) //CEA
                                {
                                    result.normalrange = mla.mla_vstand_nrange;
                                }
                                else
                                {
                                    var smoke = labconfig.labconfigs.Where(x => x.code == "|SMOKE|").Select(x => x.value).FirstOrDefault();
                                    if (smoke == @"""True""")
                                    {
                                        result.normalmin = mla.mla_vstand_min;
                                        result.normalmax = mla.mla_vstand_max;
                                    }
                                    else
                                    {
                                        result.normalmin = 0;
                                        result.normalmax = 3.8;
                                    }
                                }


                                double o;
                                if (double.TryParse(lab.valueinterpret, out o))
                                {
                                    result.interpretvalue = o;
                                }
                                else
                                {
                                    if (lab.valuedisplay.Trim().Substring(0, 1) == "<") { result.interpretvalue = Convert.ToDouble(lab.valuedisplay.Replace("<", "0")); }
                                    if (lab.valuedisplay.Trim().Substring(0, 1) == ">") { result.interpretvalue = Convert.ToDouble(lab.valuedisplay.Replace(">", "0")); }

                                }
                            }
                            // Get Result lab EMR
                            var mlps = mla.mst_lab_results.Where(x => x.mlp_status == 'A' && x.mlp_flag_special == false).OrderBy(x => x.mlp_cond_seq).ToList();
                            foreach (var mlp in mlps)
                            {
                                string condition = mlp.mlp_condition;
                                foreach (var config in labconfig.labconfigs)
                                {
                                    condition = condition.Replace(config.code, config.value);
                                }
                                if (!string.IsNullOrEmpty(lab.valueinterpret))
                                {
                                    condition = condition = condition.Replace("?", lab.valueinterpret);
                                }
                                condition = condition = condition.Replace("\"", "'");
                                //bool val = CompilerStringCls.CheckCondition(condition);
                                bool? val = cdc.emrCalculateCondition(condition).Select(x => x.rs).FirstOrDefault();
                                if ((bool)val)
                                {
                                    result.mlr_id = mlp.mlr_id;
                                    result.summary = mlp.mlp_summary;
                                    if (result.mlr_id != null)
                                    {
                                        var recom = cdc.mst_lab_recoms
                                                       .Where(x => x.mlr_id == result.mlr_id)
                                                       .Select(x => new
                                                       {
                                                           en = x.mlr_en_name,
                                                           th = x.mlr_th_name,
                                                           jp = x.mlr_jp_name,
                                                           //sp = x.
                                                       }).FirstOrDefault();
                                        if (recom != null)
                                        {
                                            result.result_en = recom.en;
                                            result.result_th = recom.th;
                                            result.result_jp = recom.jp;

                                        }
                                    }
                                    break;
                                }
                            }// end Get Result lab EMR

                            // Get Result lab Royallife
                            List<string> labcode_royallife = new List<string>
                                    {
                                        "S0721","S0723","S0725","S0727","S0729","S0731","S0733","S0735","S0420","S0380","S0472","S0551","C0280","N0140","N0150","N0130"
                                       };
                            if (labcode_royallife.Contains(lab.code))
                            {

                                var mlpsp = mla.mst_lab_results.Where(x => x.mlp_status == 'A' && x.mlp_flag_special == true).OrderBy(x => x.mlp_cond_seq).ToList();
                                foreach (var mlp in mlpsp)
                                {
                                    string condition = mlp.mlp_condition;
                                    foreach (var config in labconfig.labconfigs)
                                    {
                                        condition = condition.Replace(config.code, config.value);
                                    }
                                    if (!string.IsNullOrEmpty(lab.valueinterpret))
                                    {
                                        condition = condition = condition.Replace("?", lab.valueinterpret);
                                    }
                                    condition = condition = condition.Replace("\"", "'");
                                    //bool val = CompilerStringCls.CheckCondition(condition);
                                    bool? val = cdc.emrCalculateCondition(condition).Select(x => x.rs).FirstOrDefault();
                                    if ((bool)val)
                                    {
                                        result.mlr_id = mlp.mlr_id;
                                        result.summary = mlp.mlp_summary;
                                        if (result.mlr_id != null)
                                        {
                                            var recom = cdc.mst_lab_recoms
                                                           .Where(x => x.mlr_id == result.mlr_id)
                                                           .Select(x => new
                                                           {
                                                               //en = x.mlr_en_name,
                                                               //th = x.mlr_th_name,
                                                               //jp = x.mlr_jp_name,
                                                              sp = x.mlr_en_name
                                                           }).FirstOrDefault();
                                            if (recom != null)
                                            {

                                                result.result_sp = recom.sp;
                                                result.result_sp_con = mlp.mlp_cond_special ;

                                            }
                                        }
                                        break;
                                    }
                                }

                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InterpretLabCls", "GetResults", ex.Message);
                throw ex;
            }
        }
        //
        public InterpretAllLabResult GetAllLabResult(APITrakcare.Episode episode, LabClass.QuestionnaireResult questionaire, APITrakcare.VitalSignResult vsresult, DateTime dob, char sex)
        {
            try
            {
                var maplab = new LabClass.MapLabEmrCheckupCls().Mapping(episode);
                var labconfig = new LabClass.GetLabConfigCls().Get(questionaire, maplab, vsresult);
                var visitdate = episode.labdates.Select(x => x.labdate).FirstOrDefault();

                return GetAllLabResult(visitdate, maplab, labconfig, dob, sex);
               
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InterpretLabCls", "GetAllLabResult", ex.Message);
                throw ex;
            }
        }
       
        public InterpretAllLabResult GetAllLabResult(APITrakcare.Episode episode, LabConfigResult labconfig, DateTime dob, char sex)
        {
            try
            {
                var maplab = new LabClass.MapLabEmrCheckupCls().Mapping(episode);
                var visitdate = episode.labdates.Select(x => x.labdate).FirstOrDefault();

                return GetAllLabResult(visitdate, maplab, labconfig, dob, sex);
               
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InterpretLabCls", "GetAllLabResult", ex.Message);
                throw ex;
            }
        }
        public InterpretAllLabResult GetAllLabResult(DateTime visitdate, List<LabClass.MapLabEmrCheckupResult> maplab, LabConfigResult labconfig, DateTime dob, char sex)
        {
            try
            {
                InterpretAllLabResult result = new InterpretAllLabResult
                {
                    groups = new List<InterpretGroupLab>()
                };

                foreach (var date in maplab)
                {
                    var ages = calAge(dob, date.labdate);
                    foreach (var grp in date.labgroups)
                    {
                        var rsgrp = result.groups.Where(x => x.code == grp.code).LastOrDefault();
                        if (rsgrp == null)
                        {
                            rsgrp = new InterpretGroupLab
                            {
                                code = grp.code,
                                name_en = grp.nameen,
                                name_th = grp.nameth,
                                labs = new List<InterpretLab>()
                            };
                            result.groups.Add(rsgrp);
                        }
                        foreach (var lab in grp.labs)
                        {
                            var rslab = new InterpretLab();
                           // if (lab.status == 'E')
                             if (lab.status != 'D')
                            {
                                    var lab_date_nrange = new DateTime(2017,11,01);
                                    if (visitdate < lab_date_nrange) // use get lab old
                                    {

                                       
                                        var rs = GetResult_old(lab, labconfig, ages, sex);
                                        rslab.setcode = lab.setcode;
                                        rslab.code = lab.code;
                                        rslab.name_en = lab.nameen;
                                        rslab.name_th = lab.nameth;
                                        rslab.seq = lab.seq;
                                        rslab.value = lab.valuedisplay;
                                        rslab.mlr_id = rs.mlr_id;
                                        rslab.summary = rs.summary;
                                        rslab.result_en = rs.result_en;
                                        rslab.result_th = rs.result_th;
                                        rslab.result_jp = rs.result_jp;
                                        rslab.normalrange = rs.normalrange;
                                        rslab.unit = rs.unit;
                                        rslab.status = lab.status;
                                        if (lab.usechart)
                                        {
                                            var chartrs = new LabClass.GetChartCls().GetID(lab.code, rs.chartmin, rs.chartmax, rs.normalmin, rs.normalmax, rs.interpretvalue, rs.displayvalue, (rs.summary == null ? "" : rs.summary.ToString()));
                                            if (chartrs != null)
                                            {
                                                rslab.chartid = chartrs.chartid;
                                                rslab.chartpath = chartrs.chartpath;
                                            }
                                        }
                                    }
                                    else {

                                       // var rs = GetResult(lab, labconfig, ages, sex); // use get lab old
                                        var rs = GetResult_Special(lab, labconfig, ages, sex); // use test get lab test  //  lab Royallife
                                        rslab.setcode = lab.setcode;
                                        rslab.code = lab.code;
                                        rslab.name_en = lab.nameen;
                                        rslab.name_th = lab.nameth;
                                        rslab.seq = lab.seq;
                                        rslab.value = lab.valuedisplay;
                                        rslab.mlr_id = rs.mlr_id;
                                        rslab.summary = rs.summary;
                                        rslab.result_en = rs.result_en;
                                        rslab.result_th = rs.result_th;
                                        rslab.result_jp = rs.result_jp;
                                        rslab.normalrange = rs.normalrange;
                                        rslab.unit = rs.unit;
                                        rslab.status = lab.status;

                                        rslab.result_sp = rs.result_sp;
                                        rslab.result_sp_con = rs.result_sp_con;
                                      
                                        if (lab.usechart)
                                        {
                                            var chartrs = new LabClass.GetChartCls().GetID(lab.code, rs.chartmin, rs.chartmax, rs.normalmin, rs.normalmax, rs.interpretvalue, rs.displayvalue, (rs.summary == null ? "" : rs.summary.ToString()));
                                            if (chartrs != null)
                                            {
                                                rslab.chartid = chartrs.chartid;
                                                rslab.chartpath = chartrs.chartpath;
                                            }
                                        }


                                    }
                              
                            }
                            else
                            {
                                rslab.setcode = lab.setcode;
                                rslab.code = lab.code;
                                rslab.name_en = lab.nameen;
                                rslab.name_th = lab.nameth;
                                rslab.seq = lab.seq;
                                rslab.value = lab.valuedisplay;
                                rslab.status = lab.status; //  m Add 18/08/2017
                            }
                            rsgrp.labs.Add(rslab);
                        }
                    }
                }

                //using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                //{
                //    var ages = calAge(dob, visitdate);
                //    var labspecials = cdc.mst_labs.Where(x => (x.mlb_type == 'S' || x.mlb_type == 'C') && x.mlb_status == 'A').ToList();
                //    foreach (var lab in labspecials)
                //    {
                //        var rsgrp = result.groups.Where(x => x.code == lab.mst_lab_group.mlg_code).FirstOrDefault();
                //        if (rsgrp == null)
                //        {
                //            rsgrp = new InterpretGroupLab
                //            {
                //                code = lab.mst_lab_group.mlg_code,
                //                name_en = lab.mst_lab_group.mlg_ename,
                //                name_th = lab.mst_lab_group.mlg_tname,
                //                labs = new List<InterpretLab>()
                //            };
                //            result.groups.Add(rsgrp);
                //        }
                //        var maplabspecial = new MapLab
                //        {
                //            code = lab.mlb_code,
                //            id = lab.mlb_id,
                //            nameen = lab.mlb_ename,
                //            nameth = lab.mlb_tname,
                //            seq = lab.mlb_chart_seq,
                //            setcode = lab.mlb_lab_set,
                //            usechart = lab.mlb_use_chart == true ? true : false,
                //            valuetype = lab.mlb_value_type,
                //            status = 'E'
                //        };
                //        //var rs = GetResult(maplabspecial, labconfig, ages, sex); // use get lab old

                //         var lab_date_nrange = new DateTime(2017,11,01);
                //         if (visitdate < lab_date_nrange) // use get lab old
                //         {
                //             var rs = GetResult_old(maplabspecial, labconfig, ages, sex);
                //             var rslab = new InterpretLab();
                //             if (lab.mlb_type == 'C' || rs.mlr_id != null)
                //             {
                //                 rslab.setcode = lab.mlb_lab_set;
                //                 rslab.code = lab.mlb_code;
                //                 rslab.name_en = lab.mlb_ename;
                //                 rslab.name_th = lab.mlb_tname;
                //                 rslab.seq = lab.mlb_chart_seq;
                //                 rslab.mlr_id = rs.mlr_id;
                //                 rslab.summary = rs.summary;
                //                 rslab.result_en = rs.result_en;
                //                 rslab.result_th = rs.result_th;
                //                 rslab.result_jp = rs.result_jp;
                //                 rslab.normalrange = rs.normalrange;
                //                 rslab.unit = rs.unit;
                //                 rslab.status = rs.status;

                //                 if (lab.mlb_use_chart == true)
                //                 {
                //                     var chartrs = new LabClass.GetChartCls().GetID(lab.mlb_code, rs.chartmin, rs.chartmax, rs.normalmin, rs.normalmax, rs.interpretvalue, rs.displayvalue, (rs.summary == null ? "" : rs.summary.ToString()));
                //                     if (chartrs != null)
                //                     {
                //                         rslab.chartid = chartrs.chartid;
                //                         rslab.chartpath = chartrs.chartpath;
                //                     }
                //                 }
                //                 rsgrp.labs.Add(rslab);

                //             }
                //         }
                //         else {

                //             var rs = GetResult(maplabspecial, labconfig, ages, sex);
                //             var rslab = new InterpretLab();
                //             if (lab.mlb_type == 'C' || rs.mlr_id != null)
                //             {
                //                 rslab.setcode = lab.mlb_lab_set;
                //                 rslab.code = lab.mlb_code;
                //                 rslab.name_en = lab.mlb_ename;
                //                 rslab.name_th = lab.mlb_tname;
                //                 rslab.seq = lab.mlb_chart_seq;
                //                 rslab.mlr_id = rs.mlr_id;
                //                 rslab.summary = rs.summary;
                //                 rslab.result_en = rs.result_en;
                //                 rslab.result_th = rs.result_th;
                //                 rslab.result_jp = rs.result_jp;
                //                 rslab.normalrange = rs.normalrange;
                //                 rslab.unit = rs.unit;
                //                 rslab.status = rs.status;

                //                 if (lab.mlb_use_chart == true)
                //                 {
                //                     var chartrs = new LabClass.GetChartCls().GetID(lab.mlb_code, rs.chartmin, rs.chartmax, rs.normalmin, rs.normalmax, rs.interpretvalue, rs.displayvalue, (rs.summary == null ? "" : rs.summary.ToString()));
                //                     if (chartrs != null)
                //                     {
                //                         rslab.chartid = chartrs.chartid;
                //                         rslab.chartpath = chartrs.chartpath;
                //                     }
                //                 }
                //                 rsgrp.labs.Add(rslab);

                //             }

                //             // Royal
                //             if (lab.mlb_code == "N0140")
                //             {
                //                 var rsRoyal = GetResult_Special(maplabspecial, labconfig, ages, sex);
                //                 var rslabRoyal = new InterpretLab();
                //                 if (lab.mlb_type == 'C' || rs.mlr_id != null)
                //                 {
                //                     rslabRoyal.setcode = lab.mlb_lab_set;
                //                     rslabRoyal.code = lab.mlb_code;
                //                     rslabRoyal.name_en = lab.mlb_ename;
                //                     rslabRoyal.name_th = lab.mlb_tname;
                //                     rslabRoyal.seq = lab.mlb_chart_seq;
                //                     rslabRoyal.mlr_id = rs.mlr_id;
                //                     rslabRoyal.summary = rs.summary;
                //                     rslabRoyal.result_en = rs.result_en;
                //                     rslabRoyal.result_th = rs.result_th;
                //                     rslabRoyal.result_jp = rs.result_jp;
                //                     rslabRoyal.normalrange = rs.normalrange;
                //                     rslabRoyal.unit = rs.unit;
                //                     rslabRoyal.status = rs.status;

                //                     if (lab.mlb_use_chart == true)
                //                     {
                //                         //var chartrs = new LabClass.GetChartCls().GetID_Royal(lab.mlb_code, rs.chartmin, rs.chartmax, rs.normalmin, rs.normalmax, rs.interpretvalue, rs.displayvalue, (rs.summary == null ? "" : rs.summary.ToString()));
                //                         var chartrs = new LabClass.GetChartCls().GetID(lab.mlb_code, rs.chartmin, rs.chartmax, rs.normalmin, rs.normalmax, rs.interpretvalue, rs.displayvalue, (rs.summary == null ? "" : rs.summary.ToString()));
                                      
                //                         if (chartrs != null)
                //                         {
                //                             rslab.chartid = chartrs.chartid;
                //                             rslab.chartpath = chartrs.chartpath;
                //                         }
                //                     }
                //                     rsgrp.labs.Add(rslab);

                //                 }
                //             }
                //         }
                //    }
                //}


                foreach (var grp in result.groups)
                {
                    grp.labs = grp.labs.OrderBy(x => x.seq).ToList();
                    grp.status = grp.labs.Any(x => x.status == 'I') ? 'I' : 'E';
                    grp.summary = grp.labs.Any(x => x.summary == 'A') ? 'A' : 'N';
                    var edu = grp.labs.Where(x => x.summary == 'A' && !string.IsNullOrEmpty(x.result_th)).Select(x => x.result_th).ToList();
                    grp.education = string.Join(", ", edu);
                }
                return result;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InterpretLabCls", "GetAllLabResult", ex.Message);
                throw ex;
            }
        }

        public double calAge(DateTime StartDate, DateTime EndDate)
        {
            int years = 0;
            if (StartDate == null || EndDate == null)
            {
                return 0;
            }
            years = EndDate.Year - StartDate.Year;
            if (EndDate < StartDate.AddYears(years) && years != 0) years--;
            StartDate = StartDate.AddYears(years);
            double days = Convert.ToDouble((EndDate - StartDate).Days) / Convert.ToDouble(365);
            double ages = years + days;
            return ages;
        }
    }
    public class InterpretResult
    {
        public char? status { get; set; }
        public int? mlr_id { get; set; }
        public char? summary { get; set; }
        public string unit { get; set; }
        public string normalrange { get; set; }
        public string result_en { get; set; }
        public string result_th { get; set; }
        public string result_jp { get; set; }

        public double? chartmin { get; set; }
        public double? chartmax { get; set; }
        public double? normalmin { get; set; }
        public double? normalmax { get; set; }
        public string displayvalue { get; set; }
        public double? interpretvalue { get; set; }
        public string conditionspecial { get; set; }
        public string result_sp { get; set; }
        public string result_sp_con { get; set; }
    }

    public class InterpretAllLabResult
    {
        public List<InterpretGroupLab> groups { get; set; }
    }
    public class InterpretGroupLab
    {
        public string code { get; set; }
        public string name_en { get; set; }
        public string name_th { get; set; }
        public char? summary { get; set; }
        public char? status { get; set; }
        public string education { get; set; }

        public List<InterpretLab> labs { get; set; }
    }
    public class InterpretLab
    {
        public string setcode { get; set; }
        public string code { get; set; }
        public string name_en { get; set; }
        public string name_th { get; set; }
        public string value { get; set; }
        public int? mlr_id { get; set; }
        public char? summary { get; set; }
        public string unit { get; set; }
        public string normalrange { get; set; }
        public string result_en { get; set; }
        public string result_th { get; set; }
        public string result_jp { get; set; }
        public int? seq { get; set; }
        public int? chartid { get; set; }
        public string chartpath { get; set; }
        public char? status { get; set; }

        public string result_sp { get; set; }
        public string result_sp_con { get; set; }

    }
}