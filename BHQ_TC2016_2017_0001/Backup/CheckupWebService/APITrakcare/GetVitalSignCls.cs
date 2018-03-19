using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CheckupWebService.APITrakcare
{
    public class GetVitalSignCls
    {
        public List<VitalSignResult> ByGetVitalSignByHN(string hn)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var RawMateWS = ws.GetVitalSignByHN(hn).AsEnumerable();
                    var RawMateGroup = RawMateWS.GroupBy(x => new
                                       {
                                           en = x.Field<string>("PAADM_ADMNo"),
                                           enrowid = x.Field<int>("PAADM_RowID"),
                                           rowid = x.Field<int>("OBS_ParRef"),
                                           vsDate = x.Field<DateTime?>("OBS_Date") == null
                                                    ? null
                                                    : x.Field<TimeSpan?>("OBS_Time") == null
                                                    ? x.Field<DateTime?>("OBS_Date")
                                                    : x.Field<DateTime?>("OBS_Date").Value.Date.Add(x.Field<TimeSpan?>("OBS_Time").Value.Duration()),
                                           location = x.Field<string>("CTLOC_Code")
                                       }).ToList();
                    List<VitalSignResult> results = new List<VitalSignResult>();
                    foreach (var grp in RawMateGroup)
                    {
                        VitalSignResult rs = new VitalSignResult
                        {
                            en = grp.Key.en,
                            enrowid = grp.Key.enrowid,
                            rowid = grp.Key.rowid,
                            location = grp.Key.location,
                            vsDate = grp.Key.vsDate
                        };
                        foreach (var vs in grp)
                        {
                            switch (vs.Field<int>("OBS_Item_DR"))
                            {
                                case 230:
                                    rs.weight = vs.Field<string>("OBS_Value");
                                    break;
                                case 231:
                                    rs.height = vs.Field<string>("OBS_Value");
                                    break;
                                case 11:
                                    rs.temperature = vs.Field<string>("OBS_Value");
                                    break;
                                case 129:
                                    rs.systolic = vs.Field<string>("OBS_Value");
                                    break;
                                case 128:
                                    rs.diastolic = vs.Field<string>("OBS_Value");
                                    break;
                                case 9:
                                    rs.pulse = vs.Field<string>("OBS_Value");
                                    break;
                                case 10:
                                    rs.respirationrate = vs.Field<string>("OBS_Value");
                                    break;
                                case 134:
                                    rs.bmi = vs.Field<string>("OBS_Value");
                                    break;
                                case 176:
                                    rs.Waist = vs.Field<string>("OBS_Value");
                                    break;
                                case 173:
                                    rs.visionLeft = vs.Field<string>("OBS_Value");
                                    break;
                                case 174:
                                    rs.visionRight = vs.Field<string>("OBS_Value");
                                    break;
                                case 281:
                                    rs.withlen = vs.Field<string>("OBS_Value") == null ? false : vs.Field<string>("OBS_Value").ToUpper() == "Y";
                                    break;
                                case 179:
                                    rs.colorblind = vs.Field<string>("OBS_Value");
                                    break;
                            }
                        }
                        results.Add(rs);
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetVitalSignCls", "ByGetVitalSignByHN", ex.Message);
                throw ex;
            }
        }
    }

    public class VitalSignResult
    {
        public string en { get; set; }
        public int enrowid { get; set; }
        public int rowid { get; set; }
        public DateTime? vsDate { get; set; }
        public string location { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public string temperature { get; set; }
        public string systolic { get; set; }
        public string diastolic { get; set; }
        public string pulse { get; set; }
        public string respirationrate { get; set; }
        public string bmi { get; set; }
        public string Waist { get; set; }
        public string visionLeft { get; set; }
        public string visionRight { get; set; }
        public string colorblind { get; set; }
        public bool withlen { get; set; }
    }
}