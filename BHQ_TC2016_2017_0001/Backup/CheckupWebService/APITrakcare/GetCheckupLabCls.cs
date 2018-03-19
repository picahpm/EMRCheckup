using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.APITrakcare
{
    public class GetCheckupLabCls
    {
        public List<CheckupLab> Get(string hn, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var result = ws.GetCheckupLab(hn, dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd")).AsEnumerable();
                    return result.Select(x => new CheckupLab
                    {
                        rowid = x.Field<string>("EPVISVisitNumber"),
                        hn = x.Field<string>("DBT_CDE"),
                        en = x.Field<string>("EPI_NO"),
                        age = Convert.ToInt32(x.Field<string>("EPVIS_AGE")),
                        sex = x.Field<string>("EPVIS_SEX") == "Male" ? "M" : x.Field<string>("EPVIS_SEX") == "Female" ? "F" : "",
                        lab_date = new DateTime(
                                        Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(0, 4)),
                                        Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(5, 2)),
                                        Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(8, 2)),
                                        (int)(Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) / 60),
                                        (int)(Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) % 60),
                                        0
                                        ),
                        head_lab_code = x.Field<string>("CTTS_CDE"),
                        head_lab_desc = x.Field<string>("CTTS_NME"),
                        lab_code = x.Field<string>("CTTC_CDE"),
                        lab_desc = x.Field<string>("CTTC_DES"),
                        lab_value_number = x.Field<string>("CTTC_RSL_FMT").Contains("N") && x.Field<string>("TST_DTA").Trim().Length > 0 ?
                                            x.Field<string>("TST_DTA").Trim().Substring(0, 1) == "." ? "0" + x.Field<string>("TST_DTA")
                                            : x.Field<string>("TST_DTA")
                                        : x.Field<string>("TST_DTA"),
                        lab_value_string = x.Field<string>("RSL") == null || x.Field<string>("RSL").Trim() == "" ? null : x.Field<string>("RSL").Trim(),
                        lab_unit = x.Field<string>("CTTC_UNT"),
                        lab_range = x.Field<string>("NML"),
                        lab_hl = x.Field<string>("HL") == "H" ? 1 : x.Field<string>("HL") == "L" ? -1 : 0,
                        lab_frs = x.Field<string>("CTTC_RSL_FMT").Contains("N") ? 1 :
                                    x.Field<string>("CTTC_RSL_FMT") == "S" ? 2 :
                                    x.Field<string>("CTTC_RSL_FMT") == "X" ? 0 :
                                    -1,
                        status = x.Field<string>("Status") == "Executed" ? 'E' : 'I',
                        site_code = x.Field<string>("CTHOS_CDE"),
                        lab_seq = (x.Field<string>("Sequen") == null || x.Field<string>("Sequen") == "") ? (int?)null :
                                            !IsNumeric(x.Field<string>("Sequen")) ? (int?)null :
                                            Convert.ToInt32(x.Field<string>("Sequen"))
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CheckupLabResult ByGetCheckupLab(string hn, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var RawMateWS = ws.GetCheckupLab(hn, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")).AsEnumerable();
                    var RawMateGroups = RawMateWS.Where(x => x.Field<string>("EPI_NO").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Count() > 0)
                                                 .GroupBy(x => x.Field<string>("EPI_NO").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0]).ToList();

                    var result = ws.GetPTInfoByHN(hn).AsEnumerable()
                                   .Select(x => new CheckupLabResult
                                   {
                                       hn = hn,
                                       sex = x.Field<string>("CTSEX_Code"),
                                       dob = x.Field<DateTime>("PAPMI_DOB")
                                   }).FirstOrDefault();
                    if (result != null)
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            result.episodes = RawMateGroups.Select(x => new Episode
                            {
                                en = x.Key,
                                labdates = x.GroupBy(y => new DateTime(
                                                        Convert.ToInt32(y.Field<string>("EPIVISDateOfCollection").Substring(0, 4)),
                                                        Convert.ToInt32(y.Field<string>("EPIVISDateOfCollection").Substring(5, 2)),
                                                        Convert.ToInt32(y.Field<string>("EPIVISDateOfCollection").Substring(8, 2))
                                                        )
                                           ).Select(y => new LabDate
                                           {
                                               labdate = y.Key,
                                               labs = (from lab in y
                                                       join mhs in new LabClass.MappingLocationCls().Mapping()
                                                       on lab.Field<string>("CTHOS_CDE") equals mhs.subcode into l
                                                       from m in l.DefaultIfEmpty()
                                                       select new Lab
                                                       {
                                                           rowid = lab.Field<string>("EPVISVisitNumber"),
                                                           age = lab.Field<string>("EPVIS_AGE") == null
                                                                 ? (int?)null
                                                                 : !IsNumeric(lab.Field<string>("EPVIS_AGE"))
                                                                 ? (int?)null
                                                                 : Convert.ToInt32(lab.Field<string>("EPVIS_AGE")),
                                                           headcode = lab.Field<string>("CTTS_CDE"),
                                                           headdesc = lab.Field<string>("CTTS_NME"),
                                                           code = lab.Field<string>("CTTC_CDE"),
                                                           labname = lab.Field<string>("CTTC_DES"),
                                                           valuenumber = lab.Field<string>("TST_DTA") == null
                                                                         ? null
                                                                         : lab.Field<string>("TST_DTA").Trim().Substring(0, 1) == "."
                                                                         ? "0" + lab.Field<string>("TST_DTA").Trim()
                                                                         : lab.Field<string>("TST_DTA").Trim(),
                                                           valuestring = lab.Field<string>("RSL") == null
                                                                         ? null
                                                                         : lab.Field<string>("RSL").Trim(),
                                                           unit = lab.Field<string>("CTTC_UNT"),
                                                           range = lab.Field<string>("NML"),
                                                           hl = lab.Field<string>("HL") == "H" ? 1 : lab.Field<string>("HL") == "L" ? -1 : 0,
                                                           frs = lab.Field<string>("CTTC_RSL_FMT") == null ? -1
                                                                 : lab.Field<string>("CTTC_RSL_FMT").Contains("N") ? 1
                                                                 : lab.Field<string>("CTTC_RSL_FMT") == "S" ? 2
                                                                 : lab.Field<string>("CTTC_RSL_FMT") == "X" ? 0
                                                                 : -1,
                                                           seq = lab.Field<string>("Sequen") == null || lab.Field<string>("Sequen") == ""
                                                                 ? (int?)null
                                                                 : !IsNumeric(lab.Field<string>("Sequen")) ? (int?)null
                                                                 : Convert.ToInt32(lab.Field<string>("Sequen")),
                                                           location = lab.Field<string>("CTHOS_CDE"),
                                                           status = lab.Field<string>("Status") == "Executed" ? 'E'
                                                                    : lab.Field<string>("Status") == "D/C (Discontinued)" ? 'D'
                                                                    : 'I',
                                                           mhs_id = m == null ? (int?)null : m.mainid,
                                                           mhs_ename = m == null ? null : m.maindesc,
                                                           mhs_tname = m == null ? null : m.maindesc
                                                       }).ToList()
                                           }).ToList()
                            }).ToList();
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetCheckupLabCls", "ByGetCheckupLab", ex.Message);
                throw ex;
            }
        }

        //public CheckupLabResult ByGetCheckupLab(string hn, DateTime startDate, DateTime endDate)
        //{
        //    try
        //    {
        //        using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
        //        {
        //            var RawMateWS = ws.GetCheckupLab(hn, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")).AsEnumerable();
        //            var RawMateGroups = RawMateWS.GroupBy(x => new
        //                                {
        //                                    en = x.Field<string>("EPI_NO"),
        //                                    labdate = new DateTime(
        //                                                Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(0, 4)),
        //                                                Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(5, 2)),
        //                                                Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(8, 2))
        //                                                )
        //                                }).ToList();

        //            var result = ws.GetPTInfoByHN(hn).AsEnumerable()
        //                           .Select(x => new CheckupLabResult
        //                           {
        //                               hn = hn,
        //                               sex = x.Field<string>("CTSEX_Code"),
        //                               dob = x.Field<DateTime>("PAPMI_DOB")
        //                           }).FirstOrDefault();
        //            if (result != null)
        //            {
        //                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
        //                {
        //                    result.visits = RawMateGroups.Select(x => new Visit
        //                    {
        //                        en = x.Key.en,
        //                        labdate = x.Key.labdate,
        //                        labgroups = (from lab in x
        //                                     join mst in cdc.mst_labs.Where(ml => ml.mlb_status == 'A' && ml.mst_lab_group.mlg_status == 'A')
        //                                     on lab.Field<string>("CTTC_CDE") equals mst.mlb_code into m
        //                                     from mlb in m.DefaultIfEmpty()
        //                                     group new { mlb, lab } by new 
        //                                     {
        //                                         id = mlb == null ? (int?)null : mlb.mst_lab_group.mlg_id, 
        //                                         code = mlb == null ? null : mlb.mst_lab_group.mlg_code,
        //                                         th = mlb == null ? null : mlb.mst_lab_group.mlg_tname,
        //                                         en = mlb == null ? null : mlb.mst_lab_group.mlg_ename
        //                                     } into grp
        //                                     select new LabGroup
        //                                     {
        //                                         groupid = grp.Key.id,
        //                                         group_en_name = grp.Key.en,
        //                                         group_th_name = grp.Key.th,
        //                                         groupcode = grp.Key.code,
        //                                         labs = grp.Select(y => new Lab
        //                                         {
        //                                             rowid = y.lab.Field<string>("EPVISVisitNumber"),
        //                                             labcode = y.lab.Field<string>("CTTC_CDE"),
        //                                             labid = y.mlb == null ? (int?)null : y.mlb.mlb_id,
        //                                             lab_th_name = y.mlb == null ? y.lab.Field<string>("CTTC_DES") : y.mlb.mlb_tname,
        //                                             lab_en_name = y.mlb == null ? y.lab.Field<string>("CTTC_DES") : y.mlb.mlb_ename,
        //                                             lab_value_type = y.mlb != null && y.mlb.mlb_value_type != null
        //                                                              ? y.mlb.mlb_value_type
        //                                                              : y.lab.Field<string>("CTTC_RSL_FMT").Contains("N")
        //                                                              ? 'N'
        //                                                              : 'S',
        //                                             lab_value_number = y.lab.Field<string>("TST_DTA") == null
        //                                                                ? null
        //                                                                : y.mlb == null || y.mlb.mlb_value_type == null || y.mlb.mlb_value_type == 'S'
        //                                                                ? y.lab.Field<string>("TST_DTA").Trim()
        //                                                                : y.lab.Field<string>("TST_DTA").Trim().Substring(0, 1) == "."
        //                                                                ? "0" + y.lab.Field<string>("TST_DTA").Trim()
        //                                                                : y.lab.Field<string>("TST_DTA").Trim(),
        //                                             lab_value_string = y.lab.Field<string>("RSL") == null
        //                                                                ? null
        //                                                                : y.lab.Field<string>("RSL").Trim(),
        //                                             lab_unit = y.lab.Field<string>("CTTC_UNT"),
        //                                             lab_range = y.lab.Field<string>("NML"),
        //                                             lab_hl = y.lab.Field<string>("HL") == "H" ? 1 : y.lab.Field<string>("HL") == "L" ? -1 : 0,
        //                                             lab_frs = y.lab.Field<string>("CTTC_RSL_FMT") == null ? -1
        //                                                       : y.lab.Field<string>("CTTC_RSL_FMT").Contains("N") ? 1
        //                                                       : y.lab.Field<string>("CTTC_RSL_FMT") == "S" ? 2
        //                                                       : y.lab.Field<string>("CTTC_RSL_FMT") == "X" ? 0
        //                                                       : -1,
        //                                             status = y.lab.Field<string>("Status") == "Executed" ? 'E' : 'I',
        //                                             lab_seq = y.lab.Field<string>("Sequen") == null || y.lab.Field<string>("Sequen") == "" 
        //                                                       ? (int?)null
        //                                                       : !IsNumeric(y.lab.Field<string>("Sequen")) ? (int?)null 
        //                                                       : Convert.ToInt32(y.lab.Field<string>("Sequen")),
        //                                             locationcode = y.lab.Field<string>("CTHOS_CDE"),
        //                                             usechart = y.mlb == null ? false : y.mlb.mlb_use_chart == true ? true : false,

        //                                             head_lab_code = y.lab.Field<string>("CTTS_CDE"),
        //                                             head_lab_desc = y.lab.Field<string>("CTTS_NME")
        //                                         }).ToList()
        //                                     }).ToList()
        //                    }).ToList();
        //                }
        //            }
        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Class.globalCls.MessageError("GetCheckupLabCls", "ByGetCheckupLab", ex.Message);
        //        throw ex;
        //    }
        //}

        private bool IsNumeric(string value)
        {
            int num;
            return Int32.TryParse(value, out num);
        }
    }

    public class CheckupLab
    {
        public string rowid { get; set; }
        public string hn { get; set; }
        public string en { get; set; }
        public int? age { get; set; }
        public string sex { get; set; }
        public DateTime lab_date { get; set; }
        public string head_lab_code { get; set; }
        public string head_lab_desc { get; set; }
        public string lab_code { get; set; }
        public string lab_desc { get; set; }
        public string lab_value_number { get; set; }
        public string lab_value_string { get; set; }
        public string lab_unit { get; set; }
        public string lab_range { get; set; }
        public int? lab_hl { get; set; }
        public int? lab_frs { get; set; }
        public char? status { get; set; }
        public string site_code { get; set; }
        public int? lab_seq { get; set; }
    }

    public class CheckupLabResult
    {
        public string hn { get; set; }
        public string sex { get; set; }
        public DateTime dob { get; set; }
        public List<Episode> episodes { get; set; }
    }
    public class Episode
    {
        public string en { get; set; }
        public List<LabDate> labdates { get; set; }
    }
    public class LabDate
    {
        public DateTime labdate { get; set; }
        public List<Lab> labs { get; set; }
    }
    public class Lab
    {
        public string rowid { get; set; }
        public int? age { get; set; }
        public string headcode { get; set; }
        public string headdesc { get; set; }
        public string code { get; set; }
        public string labname { get; set; }
        public string valuenumber { get; set; }
        public string valuestring { get; set; }
        public string unit { get; set; }
        public string range { get; set; }
        public int? hl { get; set; }
        public int? frs { get; set; }
        public char? status { get; set; }
        public int? seq { get; set; }
        public string location { get; set; }
        public int? mhs_id { get; set; }
        public string mhs_ename { get; set; }
        public string mhs_tname { get; set; }
    }

    //public class CheckupLabResult
    //{
    //    public string hn { get; set; }
    //    public string sex { get; set; }
    //    public DateTime dob { get; set; }
    //    public List<Visit> visits { get; set; }
    //}
    //public class Visit
    //{
    //    public string en { get; set; }
    //    public DateTime labdate { get; set; }
    //    public List<LabGroup> labgroups { get; set; }
    //}
    //public class LabGroup
    //{
    //    public int? groupid { get; set; }
    //    public string groupcode { get; set; }
    //    public string group_th_name { get; set; }
    //    public string group_en_name { get; set; }
    //    public List<Lab> labs { get; set; }
    //}
    //public class Lab
    //{
    //    public string rowid { get; set; }
    //    public string labcode { get; set; }
    //    public int? labid { get; set; }
    //    public string lab_th_name { get; set; }
    //    public string lab_en_name { get; set; }
    //    public char? lab_value_type { get; set; }
    //    public string lab_value_number { get; set; }
    //    public string lab_value_string { get; set; }
    //    public string lab_unit { get; set; }
    //    public string lab_range { get; set; }
    //    public int? lab_hl { get; set; }
    //    public int? lab_frs { get; set; }
    //    public char? status { get; set; }
    //    public int? lab_seq { get; set; }
    //    public string locationcode { get; set; }
    //    public bool usechart { get; set; }

    //    public string head_lab_code { get; set; }
    //    public string head_lab_desc { get; set; }
    //}
}