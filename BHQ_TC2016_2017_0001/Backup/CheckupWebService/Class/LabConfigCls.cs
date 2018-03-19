using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class LabConfigCls
    {
        public List<LabConfig> Get(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string en = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.tpr_en_no).FirstOrDefault();
                    List<LabConfig> labConfig = new List<LabConfig>();
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
                            labConfig.Add(new LabConfig
                            {
                                code = config.mlc_code,
                                value = config.mlc_type == 'N' ? res : @"""" + res + @""""
                            });
                        }
                    }
                    return labConfig;
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("LabConfigCls", "Get(int tpr_id)", ex.Message);
                return new List<LabConfig>();
            }
        }
    }

    public class LabConfig
    {
        public string code { get; set; }
        public string value { get; set; }
    }
}