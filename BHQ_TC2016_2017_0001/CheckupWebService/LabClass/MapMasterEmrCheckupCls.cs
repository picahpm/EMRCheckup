using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.LabClass
{
    public class MapLabEmrCheckupCls
    {
        public List<MapLabEmrCheckupResult> Mapping(APITrakcare.Episode episode)
        {
            try
            {
                List<MapLabEmrCheckupResult> result = new List<MapLabEmrCheckupResult>();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    foreach (var labdate in episode.labdates)
                    {
                        result.Add(new MapLabEmrCheckupResult
                        {
                            labdate = labdate.labdate,
                            labgroups = (from ld in labdate.labs
                                         join ms in cdc.mst_labs.Where(x => x.mlb_status == 'A' && x.mst_lab_group.mlg_status == 'A')
                                         on ld.code equals ms.mlb_code
                                         group new { ld, ms } 
                                         by new { ms.mst_lab_group.mlg_code, ms.mst_lab_group.mlg_ename, ms.mst_lab_group.mlg_tname }
                                         into grp
                                         select new MapLabGroup
                                         {
                                             code = grp.Key.mlg_code,
                                             nameth = grp.Key.mlg_tname,
                                             nameen = grp.Key.mlg_ename,
                                             labs = grp.Select(x => new MapLab
                                             {
                                                 id = x.ms.mlb_id,
                                                 setcode = x.ms.mlb_lab_set,
                                                 code = x.ms.mlb_code,
                                                 nameth = x.ms.mlb_tname,
                                                 nameen = x.ms.mlb_ename,
                                                 usechart = x.ms.mlb_use_chart == true ? true : false,
                                                 valuetype = x.ms.mlb_value_type,
                                                 valuedisplay = x.ms.mlb_value_type == 'S'
                                                                ? x.ld.valuestring
                                                                : x.ld.valuenumber,
                                                 valueinterpret = x.ms.mlb_value_type == 'S'
                                                                  ? @"""" + x.ld.valuestring + @""""
                                                                  : x.ld.valuenumber,
                                                                 // : castnumber(x.ld.valuenumber),
                                                 seq = x.ms.mlb_chart_seq,
                                                 status = x.ld.status
                                             }).ToList()
                                         }).ToList()
                        });
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("MapLabEmrCheckupCls", "Mapping", ex.Message);
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
    }

    public class MapLabEmrCheckupResult
    {
        public DateTime labdate { get; set; }
        public List<MapLabGroup> labgroups { get; set; }
    }
    public class MapLabGroup
    {
        public string code { get; set; }
        public string nameth { get; set; }
        public string nameen { get; set; }
        public List<MapLab> labs { get; set; }
    }
    public class MapLab
    {
        public int id { get; set; }
        public string setcode { get; set; }
        public string code { get; set; }
        public string nameth { get; set; }
        public string nameen { get; set; }
        public char? valuetype { get; set; }
        public string valuedisplay { get; set; }
        public string valueinterpret { get; set; }
        public bool usechart { get; set; }
        public int? seq { get; set; }
        public char? status { get; set; }
    }
}