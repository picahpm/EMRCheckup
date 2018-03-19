using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.LabClass
{
    public class GetLabConfigCls
    {
        public LabClass.LabConfigResult GetByPaientLab(int tpr_id)
        {
            try
            {
                LabClass.LabConfigResult result = new LabClass.LabConfigResult
                {
                    labconfigs = new List<LabClass.LabConfig>()
                };
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string en = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.tpr_en_no).FirstOrDefault();
                    var mstLabConfig = cdc.mst_lab_configs.Where(x => x.mlc_status == 'A')
                                          .Select(x => new
                                          {
                                              x.mlc_code,
                                              x.mlc_type,
                                              x.mlc_sql
                                          }).ToList();
                    foreach (var config in mstLabConfig)
                    {
                        string sql = config.mlc_sql.Replace(@"|tpr_id|", tpr_id.ToString()).Replace(@"|en_no|", "'" + en + "'");
                        string res = cdc.ExecuteQuery<string>(sql).FirstOrDefault();
                        if (!string.IsNullOrEmpty(res))
                        {
                            result.labconfigs.Add(new LabClass.LabConfig
                            {
                                code = config.mlc_code,
                                value = config.mlc_type == 'S'
                                        ? @"""" + res + @""""
                                        : castnumber(res)
                            });
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetLabConfigCls", "GetByPaientLab", ex.Message);
                throw ex;
            }
        }
        private string castnumber(string val)
        {
            double o;
            if (val.StartsWith("<"))
            {
                if (double.TryParse(val.Replace("<", ""), out o))
                {
                    return (o - 0.001).ToString();
                }
            }
            else if (val.StartsWith(">"))
            {
                if (double.TryParse(val.Replace(">", ""), out o))
                {
                    return (o + 0.001).ToString();
                }
            }
            return val;
        }

        public LabConfigResult Get(QuestionnaireResult questionnaire, List<MapLabEmrCheckupResult> maplabs, APITrakcare.VitalSignResult vsresult)
        {
            try
            {
                LabConfigResult result = new LabConfigResult();
                result.labconfigs = new List<LabConfig>();

                result.labconfigs.Add(new LabConfig { code = "|PREG|", value = questionnaire.pregnancy == 'P' ? @"""True""" : @"""False""" });
                result.labconfigs.Add(new LabConfig { code = "|SMOKE|", value = questionnaire.pregnancy == 'S' ? @"""True""" : @"""False""" });
                result.labconfigs.Add(new LabConfig { code = "|DM|", value = questionnaire.diabetes == true ? @"""True""" : @"""False""" });

                result.labconfigs.Add(new LabConfig { code = "|VLEFT|", value = vsresult.visionLeft });
                result.labconfigs.Add(new LabConfig { code = "|VRIGHT|", value = vsresult.visionRight });
                result.labconfigs.Add(new LabConfig { code = "|PULSE|", value = vsresult.pulse });
                result.labconfigs.Add(new LabConfig { code = "|BMI|", value = vsresult.bmi });
                result.labconfigs.Add(new LabConfig { code = "|SYS|", value = vsresult.systolic });
                result.labconfigs.Add(new LabConfig { code = "|DIA|", value = vsresult.diastolic });
                result.labconfigs.Add(new LabConfig { code = "|RP|", value = vsresult.respirationrate });
                result.labconfigs.Add(new LabConfig { code = "|TP|", value = vsresult.temperature });

                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var mst = cdc.mst_lab_configs
                                 .Where(x => x.mlc_status == 'A' &&
                                             x.mlc_table.Trim() == "trn_patient_lab")
                                 .Select(x => new
                                 {
                                     mlc_code = x.mlc_code,
                                     mlc_lab_code = x.mlc_lab_code
                                 }).ToList();
                    foreach (var lc in mst)
                    {
                        var codes = lc.mlc_lab_code.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (var code in codes)
                        {
                            foreach (var maplab in maplabs)
                            {
                                foreach (var grp in maplab.labgroups)
                                {
                                    var lab = grp.labs.Where(x => x.code == code.Trim() && x.status == 'E').FirstOrDefault();
                                    if (lab != null)
                                    {
                                        result.labconfigs.Add(new LabConfig { code = lab.code, value = lab.valueinterpret });
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
                Class.globalCls.MessageError("GetLabConfigCls", "Get", ex.Message);
                throw ex;
            }
        }

        //public LabConfigResult Get(QuestionnaireResult questionnaire, APITrakcare.Visit labresults, APITrakcare.VitalSignResult vsresult)
        //{
        //    try
        //    {
        //        LabConfigResult result = new LabConfigResult();
        //        result.labconfigs = new List<LabConfig>();

        //        result.labconfigs.Add(new LabConfig { code = "|PREG|", value = questionnaire.pregnancy == 'P' ? @"""True""" : @"""False""" });
        //        result.labconfigs.Add(new LabConfig { code = "|SMOKE|", value = questionnaire.pregnancy == 'S' ? @"""True""" : @"""False""" });
        //        result.labconfigs.Add(new LabConfig { code = "|DM|", value = questionnaire.diabetes == true ? @"""True""" : @"""False""" });

        //        result.labconfigs.Add(new LabConfig { code = "|VLEFT|", value = vsresult.visionLeft });
        //        result.labconfigs.Add(new LabConfig { code = "|VRIGHT|", value = vsresult.visionRight });
        //        result.labconfigs.Add(new LabConfig { code = "|PULSE|", value = vsresult.pulse });
        //        result.labconfigs.Add(new LabConfig { code = "|BMI|", value = vsresult.bmi });
        //        result.labconfigs.Add(new LabConfig { code = "|SYS|", value = vsresult.systolic });
        //        result.labconfigs.Add(new LabConfig { code = "|DIA|", value = vsresult.diastolic });
        //        result.labconfigs.Add(new LabConfig { code = "|RP|", value = vsresult.respirationrate });
        //        result.labconfigs.Add(new LabConfig { code = "|TP|", value = vsresult.temperature });

        //        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
        //        {
        //            var mst = cdc.mst_lab_configs
        //                         .Where(x => x.mlc_status == 'A' &&
        //                                     x.mlc_table.Trim() == "trn_patient_lab")
        //                         .Select(x => new
        //                         {
        //                             mlc_code = x.mlc_code,
        //                             mlc_lab_code = x.mlc_lab_code
        //                         }).ToList();
        //            foreach (var lc in mst)
        //            {
        //                var codes = lc.mlc_lab_code.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //                foreach (var code in codes)
        //                {
        //                    foreach (var grp in labresults.labgroups)
        //                    {
        //                        var lab = grp.labs.Where(x => x.labcode == code.Trim()).FirstOrDefault();
        //                        if (lab != null)
        //                        {
        //                            if (lab.lab_value_type == 'N')
        //                            {
        //                                if (lab.lab_value_number.Contains(">"))
        //                                {
        //                                    double o;
        //                                    if (double.TryParse(lab.lab_value_number.Replace(">", ""), out o))
        //                                    {
        //                                        result.labconfigs.Add(new LabConfig { code = lc.mlc_code, value = (o + 0.1).ToString() });
        //                                    }
        //                                }
        //                                else if (lab.lab_value_number.Contains("<"))
        //                                {
        //                                    double o;
        //                                    if (double.TryParse(lab.lab_value_number.Replace("<", ""), out o))
        //                                    {
        //                                        result.labconfigs.Add(new LabConfig { code = lc.mlc_code, value = (o - 0.1).ToString() });
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    result.labconfigs.Add(new LabConfig { code = lc.mlc_code, value = lab.lab_value_number });
        //                                }
        //                            }
        //                            else
        //                            {
        //                                result.labconfigs.Add(new LabConfig { code = lc.mlc_code, value = @"""" + lab.lab_value_string + @"""" });
        //                            }
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Class.globalCls.MessageError("GetLabConfigCls", "Get", ex.Message);
        //        throw ex;
        //    }
        //}

        public LabConfigResult Get(QuestionnaireResult questionnaire, CheckupLabs checkuplabs, APITrakcare.VitalSignResult vsresult)
        {
            try
            {
                LabConfigResult result = new LabConfigResult();
                result.labconfigs = new List<LabConfig>();

                result.labconfigs.Add(new LabConfig { code = "|PREG|", value = questionnaire.pregnancy == 'P' ? @"""True""" : @"""False""" });
                result.labconfigs.Add(new LabConfig { code = "|SMOKE|", value = questionnaire.pregnancy == 'S' ? @"""True""" : @"""False""" });
                result.labconfigs.Add(new LabConfig { code = "|DM|", value = questionnaire.diabetes == true ? @"""True""" : @"""False""" });

                result.labconfigs.Add(new LabConfig { code = "|VLEFT|", value = vsresult.visionLeft });
                result.labconfigs.Add(new LabConfig { code = "|VRIGHT|", value = vsresult.visionRight });
                result.labconfigs.Add(new LabConfig { code = "|PULSE|", value = vsresult.pulse });
                result.labconfigs.Add(new LabConfig { code = "|BMI|", value = vsresult.bmi });
                result.labconfigs.Add(new LabConfig { code = "|SYS|", value = vsresult.systolic });
                result.labconfigs.Add(new LabConfig { code = "|DIA|", value = vsresult.diastolic });
                result.labconfigs.Add(new LabConfig { code = "|RP|", value = vsresult.respirationrate });
                result.labconfigs.Add(new LabConfig { code = "|TP|", value = vsresult.temperature });

                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var mst = cdc.mst_lab_configs
                                 .Where(x => x.mlc_status == 'A' &&
                                             x.mlc_table.Trim() == "trn_patient_lab")
                                 .Select(x => new
                                 {
                                     mlc_code = x.mlc_code,
                                     mlc_lab_code = x.mlc_lab_code
                                 }).ToList();
                    foreach (var lc in mst)
                    {
                        var codes = lc.mlc_lab_code.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                        foreach (var code in codes)
                        {
                            var lab = checkuplabs.labs.Where(x => x.labcode == code).FirstOrDefault();
                            if (lab != null)
                            {
                                //if (lab.labvaluetype == 'N')
                                //{
                                //    result.labconfigs.Add(new LabConfig { code = lc.mlc_code, value = lab.labvalue });
                                //}
                                //else
                                //{
                                //    result.labconfigs.Add(new LabConfig { code = lc.mlc_code, value = @"""" + lab.labvalue + @"""" });
                                //}
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetLabConfigCls", "Get", ex.Message);
                throw ex;
            }
        }
    }

    public class QuestionnaireResult
    {
        public char? pregnancy { get; set; }
        public char? smoke { get; set; }
        public bool? diabetes { get; set; }
    }

    public class LabConfigResult
    {
        public List<LabConfig> labconfigs { get; set; }
    }

    public class LabConfig
    {
        public string code { get; set; }
        public string value { get; set; }
    }

    public class CheckupLabs
    {
        public List<Lab> labs { get; set; }
    }
    public class Lab
    {
        public string labcode { get; set; }
        public string labvalue { get; set; }
    }
}
