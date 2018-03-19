using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using DBCheckup;

namespace CheckupWebService.Class
{
    public partial class GetTextFileCls
    {
        string DefaultFileType = "";
        string PathFileXray = "";

        public GetTextFileCls()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DefaultFileType = cdc.mst_project_configs
                                     .Where(x => x.mpc_code == "DefaultFileTypeXray")
                                     .Select(x => x.mpc_value)
                                     .FirstOrDefault();

                PathFileXray = cdc.mst_project_configs
                                  .Where(x => x.mpc_code == "PathFileXray")
                                  .Select(x => x.mpc_value)
                                  .FirstOrDefault();
            }
        }
        public List<ResultXray> GetResultXray(string hn_no, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                    {
                        System.Threading.Thread.CurrentThread.CurrentCulture
                   = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");//add  suriya 30/06/2017
                        var tmpRes = ws.GetXrayResultList_XrayResult(hn_no, dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"), "").AsEnumerable()
                                       .GroupBy(x => new { en = x.Field<string>("PAADMADMNo"), order = x.Field<string>("ARCIMRowId") })
                                       .Select(x => x.OrderByDescending(y => y.Field<string>("ChildID")).FirstOrDefault())
                                       .ToList();

                        List<ResultXray> results = (from re in tmpRes
                                                    join mst in contxt.mst_order_plans
                                                    on re.Field<string>("ARCIMRowId") equals mst.mop_item_row_id
                                                    select new ResultXray
                                                    {
                                                        hn_no = re.Field<string>("PAPMINo"),
                                                        en_no = re.Field<string>("PAADMADMNo"),
                                                        mvt_id = mst.mvt_id,
                                                        mvt_code = mst.mst_event.mvt_code,
                                                        OEORISttDat = re.Field<DateTime?>("OEORISttDat"),
                                                        OEORITimeOrd = re.Field<TimeSpan?>("OEORITimeOrd"),
                                                        ARCIMCode = re.Field<string>("ARCIMCode"),
                                                        ARCIMDesc = re.Field<string>("ARCIMDesc"),
                                                        ARCIMRowId = re.Field<string>("ARCIMRowId"),
                                                        ChildID = re.Field<string>("ChildID"),
                                                        PathHTML = re.Field<string>("PathHTML"),
                                                        ReportBy = re.Field<string>("ReportBy"),
                                                        ReportDate = re.Field<string>("ReportDate"),
                                                        RequestDate = re.Field<string>("RequestDate"),
                                                        RESDateVerified = re.Field<DateTime?>("RESDateVerified"),
                                                        RESTimeVerified = re.Field<TimeSpan?>("RESTimeVerified"),
                                                        RESFileName = re.Field<string>("RESFileName"),
                                                        Result1 = re.Field<string>("Result1"),
                                                        Result2 = re.Field<string>("Result2"),
                                                        SSUSR_Name = re.Field<string>("SSUSRName"),
                                                        XrayType = re.Field<string>("XrayType"),
                                                        LicenseNo = re.Field<string>("LicenseNo"),
                                                        OEORIAccessionNumber = re.Field<string>("OEORIAccessionNumber")
                                                    }).ToList();
                        
                        //System.Data.DataTable dataTable = ws.GetXrayResultList_XrayResult(hn_no, dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"), "");
                        //List<ResultXray> results = (from re in dataTable.AsEnumerable()
                        //                            join mst in contxt.mst_order_plans
                        //                            on re.Field<string>("ARCIMRowId") equals mst.mop_item_row_id
                        //                            select new ResultXray
                        //                            {
                        //                                hn_no = re.Field<string>("PAPMINo"),
                        //                                en_no = re.Field<string>("PAADMADMNo"),
                        //                                mvt_id = mst.mvt_id,
                        //                                mvt_code = mst.mst_event.mvt_code,
                        //                                OEORISttDat = re.Field<DateTime>("OEORISttDat"),
                        //                                OEORITimeOrd = re.Field<TimeSpan>("OEORITimeOrd"),
                        //                                ARCIMCode = re.Field<string>("ARCIMCode"),
                        //                                ARCIMDesc = re.Field<string>("ARCIMDesc"),
                        //                                ARCIMRowId = re.Field<string>("ARCIMRowId"),
                        //                                ChildID = re.Field<string>("ChildID"),
                        //                                PathHTML = re.Field<string>("PathHTML"),
                        //                                ReportBy = re.Field<string>("ReportBy"),
                        //                                ReportDate = re.Field<string>("ReportDate"),
                        //                                RequestDate = re.Field<string>("RequestDate"),
                        //                                RESDateVerified = re.Field<DateTime>("RESDateVerified"),
                        //                                RESTimeVerified = re.Field<TimeSpan>("RESTimeVerified"),
                        //                                RESFileName = re.Field<string>("RESFileName"),
                        //                                Result1 = re.Field<string>("Result1"),
                        //                                Result2 = re.Field<string>("Result2"),
                        //                                SSUSR_Name = re.Field<string>("SSUSRName")
                        //                            }).ToList();

                        if (results.Count > 0)
                        {
                            string LinkPacSheet = GetDBConfigCls.GetConfig("LinkPacSheet");
                            string LinkPDF = GetDBConfigCls.GetConfig("LinkPDF");

                            var grpEvent = results.GroupBy(x => x.mvt_code).ToList();
                            foreach (var grp in grpEvent)
                            {
                                var grpList = grp.ToList();
                                switch (grp.Key)
                                {
                                    case "XR":
                                        SetDataXray(ref grpList, LinkPacSheet);
                                        break;
                                    case "UU":
                                    case "UL":
                                    case "UB":
                                    case "UW":
                                        SetDataUltrasound(ref grpList, LinkPacSheet);
                                        break;
                                    case "UG":
                                        SetDataUGI(ref grpList, LinkPacSheet);
                                        break;
                                    case "DM":
                                        SetDataMammogram(ref grpList, LinkPacSheet);
                                        break;
                                    case "BD":
                                        SetDataBMD(ref grpList, LinkPacSheet);
                                        break;
                                    case "CD":
                                        SetDataCarotid(ref grpList, LinkPacSheet);
                                        break;
                                }
                            }
                        }

                        //SetDataUGI(ref results);
                        //SetDataXray(ref results);
                        //SetDataBMD(ref results);
                        //SetDataMammogram(ref results);
                        //SetDataUltrasound(ref results);
                        //SetDataCarotid(ref results);
                        return results;
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "GetTextFileCls.GetResultXray", ex.Message);
                return new List<ResultXray>();
            }
        }

        private void SetDataUGI(ref List<ResultXray> results, string LinkPacSheet)
        {
            foreach (var rs in results)
            {
                List<string> TextSplit = new List<string>();
                if (rs.XrayType == "1")
                {
                    if (!string.IsNullOrEmpty(rs.RESFileName) && rs.RESFileName.ToLower().Contains(DefaultFileType))
                    {
                        string url;
                        rs.RESFileName = rs.RESFileName.Replace(@"\", @"/");
                        if (PathFileXray.EndsWith(@"/") && rs.RESFileName.StartsWith(@"/"))
                        {
                            url = PathFileXray + rs.RESFileName.Substring(1);
                        }
                        else
                        {
                            url = PathFileXray + rs.RESFileName;
                        }
                        TextSplit = ReadTextFromUrl(url);
                    }
                }
                else if (rs.XrayType == "2")
                {
                    TextSplit = rs.Result1.SplitLine();
                }
                else if (rs.XrayType == "3")
                {
                    TextSplit = UtilityCls.HtmlToPlainText(rs.Result1).SplitLine();
                }
                string content = GetContentResult(TextSplit, "Patient Order : " + rs.ARCIMDesc, rs.ReportBy, rs.ReportDate, rs.LicenseNo);
                rs.resultText = content;
                string hnReplace = rs.hn_no.Replace("-", "");
                rs.Link = string.Format(LinkPacSheet, rs.OEORIAccessionNumber.Replace("/", "-"), hnReplace);
            }
        }
        private void SetDataUGI(ref List<ResultXray> results)
        {
            int i = results.Count;
            while (i > 0)
            {
                i--;
                if (results[i].mvt_code == "UG")
                {
                    if (results[i].XrayType == "1")
                    {
                        if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                        {
                            string url;
                            results[i].RESFileName = results[i].RESFileName.Replace(@"\", @"/");
                            if (PathFileXray.EndsWith(@"/") && results[i].RESFileName.StartsWith(@"/"))
                            {
                                url = PathFileXray + results[i].RESFileName.Substring(1);
                            }
                            else
                            {
                                url = PathFileXray + results[i].RESFileName;
                            }
                            string content = GetContentResult(ReadTextFromUrl(url), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);

                            //string path;
                            //if (PathFileXray.EndsWith(@"\") && results[i].RESFileName.StartsWith(@"\"))
                            //{
                            //    path = PathFileXray + results[i].RESFileName.Substring(1);
                            //}
                            //else
                            //{
                            //    path = PathFileXray + results[i].RESFileName;
                            //}
                            //string content = GetContentResult(GetLinesTextFile(path), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                            results[i].resultText = content;
                        }
                    }
                    else if (results[i].XrayType == "2")
                    {
                        string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }
                    else if (results[i].XrayType == "3")
                    {
                        string result = UtilityCls.HtmlToPlainText(results[i].Result1);
                        string content = GetContentResult(result.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }

                    //if (!string.IsNullOrEmpty(results[i].Result1) && !string.IsNullOrEmpty(results[i].PathHTML))
                    //{
                    //    string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                    //    results[i].resultText = content;
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                    //    {
                    //        string path;
                    //        if (PathFileXray.EndsWith(@"\") && results[i].RESFileName.StartsWith(@"\"))
                    //        {
                    //            path = PathFileXray + results[i].RESFileName.Substring(1);
                    //        }
                    //        else
                    //        {
                    //            path = PathFileXray + results[i].RESFileName;
                    //        }
                    //        string content = GetContentResult(GetLinesTextFile(path), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                    //        results[i].resultText = content;
                    //    }
                    //}
                }
            }
        }

        private void SetDataXray(ref List<ResultXray> results, string LinkPacSheet)
        {
            foreach (var rs in results)
            {
                List<string> TextSplit = new List<string>();
                if (rs.XrayType == "1")
                {
                    if (!string.IsNullOrEmpty(rs.RESFileName) && rs.RESFileName.ToLower().Contains(DefaultFileType))
                    {
                        string url;
                        rs.RESFileName = rs.RESFileName.Replace(@"\", @"/");
                        if (PathFileXray.EndsWith(@"/") && rs.RESFileName.StartsWith(@"/"))
                        {
                            url = PathFileXray + rs.RESFileName.Substring(1);
                        }
                        else
                        {
                            url = PathFileXray + rs.RESFileName;
                        }
                        TextSplit = ReadTextFromUrl(url);
                    }
                }
                else if (rs.XrayType == "2")
                {
                    TextSplit = rs.Result1.SplitLine();
                }
                else if (rs.XrayType == "3")
                {
                    TextSplit = UtilityCls.HtmlToPlainText(rs.Result1).SplitLine();
                }
                string content = GetContentResult(TextSplit, "Patient Order : " + rs.ARCIMDesc, rs.ReportBy, rs.ReportDate, rs.LicenseNo);
                rs.resultText = content;
                string hnReplace = rs.hn_no.Replace("-", "");
                rs.Link = string.Format(LinkPacSheet, rs.OEORIAccessionNumber.Replace("/", "-"), hnReplace);
            }
        }
        private void SetDataXray(ref List<ResultXray> results)
        {
            int i = results.Count;
            while (i > 0)
            {
                i--;
                if (results[i].mvt_code == "XR")
                {
                    if (results[i].XrayType == "1")
                    {
                        if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                        {
                            string url;
                            results[i].RESFileName = results[i].RESFileName.Replace(@"\", @"/");
                            if (PathFileXray.EndsWith(@"/") && results[i].RESFileName.StartsWith(@"/"))
                            {
                                url = PathFileXray + results[i].RESFileName.Substring(1);
                            }
                            else
                            {
                                url = PathFileXray + results[i].RESFileName;
                            }
                            string content = GetContentResult(ReadTextFromUrl(url), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                            results[i].resultText = content;
                        }
                    }
                    else if (results[i].XrayType == "2")
                    {
                        string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }
                    else if (results[i].XrayType == "3")
                    {
                        string result = UtilityCls.HtmlToPlainText(results[i].Result1);
                        string content = GetContentResult(result.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }
                }
            }
        }

        private void SetDataBMD(ref List<ResultXray> results, string LinkPacSheet)
        {
            foreach (var rs in results)
            {
                List<string> TextSplit = new List<string>();
                if (rs.XrayType == "1")
                {
                    if (!string.IsNullOrEmpty(rs.RESFileName) && rs.RESFileName.ToLower().Contains(DefaultFileType))
                    {
                        string url;
                        rs.RESFileName = rs.RESFileName.Replace(@"\", @"/");
                        if (PathFileXray.EndsWith(@"/") && rs.RESFileName.StartsWith(@"/"))
                        {
                            url = PathFileXray + rs.RESFileName.Substring(1);
                        }
                        else
                        {
                            url = PathFileXray + rs.RESFileName;
                        }
                        TextSplit = ReadTextFromUrl(url);
                    }
                }
                else if (rs.XrayType == "2")
                {
                    TextSplit = rs.Result1.SplitLine();
                }
                else if (rs.XrayType == "3")
                {
                    TextSplit = UtilityCls.HtmlToPlainText(rs.Result1).SplitLine();
                }
                string content = GetContentResult(TextSplit, "Patient Order : " + rs.ARCIMDesc, rs.ReportBy, rs.ReportDate, rs.LicenseNo);
                rs.resultText = content;
                string hnReplace = rs.hn_no.Replace("-", "");
                rs.Link = string.Format(LinkPacSheet, rs.OEORIAccessionNumber.Replace("/", "-"), hnReplace);
            }
        }
        private void SetDataBMD(ref List<ResultXray> results)
        {
            int i = results.Count;
            while (i > 0)
            {
                i--;
                if (results[i].mvt_code == "BD")
                {
                    if (results[i].XrayType == "1")
                    {
                        if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                        {
                            string url;
                            results[i].RESFileName = results[i].RESFileName.Replace(@"\", @"/");
                            if (PathFileXray.EndsWith(@"/") && results[i].RESFileName.StartsWith(@"/"))
                            {
                                url = PathFileXray + results[i].RESFileName.Substring(1);
                            }
                            else
                            {
                                url = PathFileXray + results[i].RESFileName;
                            }
                            string content = GetContentResult(ReadTextFromUrl(url), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);

                            //string path;
                            //if (PathFileXray.EndsWith(@"\") && results[i].RESFileName.StartsWith(@"\"))
                            //{
                            //    path = PathFileXray + results[i].RESFileName.Substring(1);
                            //}
                            //else
                            //{
                            //    path = PathFileXray + results[i].RESFileName;
                            //}
                            //string content = GetContentResult(GetLinesTextFile(path), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                            results[i].resultText = content;
                        }
                    }
                    else if (results[i].XrayType == "2")
                    {
                        string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }
                    else if (results[i].XrayType == "3")
                    {
                        string result = UtilityCls.HtmlToPlainText(results[i].Result1);
                        string content = GetContentResult(result.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }
                    //if (!string.IsNullOrEmpty(results[i].Result1) && !string.IsNullOrEmpty(results[i].PathHTML))
                    //{
                    //    string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                    //    results[i].resultText = content;
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                    //    {
                    //        string path;
                    //        if (PathFileXray.EndsWith(@"\") && results[i].RESFileName.StartsWith(@"\"))
                    //        {
                    //            path = PathFileXray + results[i].RESFileName.Substring(1);
                    //        }
                    //        else
                    //        {
                    //            path = PathFileXray + results[i].RESFileName;
                    //        }
                    //        string content = GetContentResult(GetLinesTextFile(path), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                    //        results[i].resultText = content;
                    //    }
                    //}
                }
            }
        }

        private void SetDataMammogram(ref List<ResultXray> results, string LinkPacSheet)
        {
            foreach (var rs in results)
            {
                List<string> TextSplit = new List<string>();
                if (rs.XrayType == "1")
                {
                    if (!string.IsNullOrEmpty(rs.RESFileName) && rs.RESFileName.ToLower().Contains(DefaultFileType))
                    {
                        string url;
                        rs.RESFileName = rs.RESFileName.Replace(@"\", @"/");
                        if (PathFileXray.EndsWith(@"/") && rs.RESFileName.StartsWith(@"/"))
                        {
                            url = PathFileXray + rs.RESFileName.Substring(1);
                        }
                        else
                        {
                            url = PathFileXray + rs.RESFileName;
                        }
                        TextSplit = ReadTextFromUrl(url);
                    }
                }
                else if (rs.XrayType == "2")
                {
                    TextSplit = rs.Result1.SplitLine();
                }
                else if (rs.XrayType == "3")
                {
                    TextSplit = UtilityCls.HtmlToPlainText(rs.Result1).SplitLine();
                }
                string content = GetContentResult(TextSplit, "Patient Order : " + rs.ARCIMDesc, rs.ReportBy, rs.ReportDate, rs.LicenseNo);
                rs.resultText = content;
                rs.biradsCate = getBirad(content);
                string hnReplace = rs.hn_no.Replace("-", "");
                rs.Link = string.Format(LinkPacSheet, rs.OEORIAccessionNumber.Replace("/", "-"), hnReplace);
            }
        }
        private void SetDataMammogram(ref List<ResultXray> results)
        {
            int i = results.Count;
            while (i > 0)
            {
                i--;
                if (results[i].mvt_code == "DM")
                {
                    if (results[i].XrayType == "1")
                    {
                        if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                        {
                            string url;
                            results[i].RESFileName = results[i].RESFileName.Replace(@"\", @"/");
                            if (PathFileXray.EndsWith(@"/") && results[i].RESFileName.StartsWith(@"/"))
                            {
                                url = PathFileXray + results[i].RESFileName.Substring(1);
                            }
                            else
                            {
                                url = PathFileXray + results[i].RESFileName;
                            }
                            string content = GetContentResult(ReadTextFromUrl(url), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);

                            //string path;
                            //if (PathFileXray.EndsWith(@"\") && results[i].RESFileName.StartsWith(@"\"))
                            //{
                            //    path = PathFileXray + results[i].RESFileName.Substring(1);
                            //}
                            //else
                            //{
                            //    path = PathFileXray + results[i].RESFileName;
                            //}
                            //string content = GetContentResult(GetLinesTextFile(path), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                            results[i].resultText = content;
                            results[i].biradsCate = getBirad(content);
                        }
                    }
                    else if (results[i].XrayType == "2")
                    {
                        string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                        results[i].biradsCate = getBirad(content);
                    }
                    else if (results[i].XrayType == "3")
                    {
                        string result = UtilityCls.HtmlToPlainText(results[i].Result1);
                        string content = GetContentResult(result.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                        results[i].biradsCate = getBirad(content);
                    }
                    //if (!string.IsNullOrEmpty(results[i].Result1) && !string.IsNullOrEmpty(results[i].PathHTML))
                    //{
                    //    string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                    //    results[i].resultText = content;
                    //    results[i].biradsCate = getBirad(results[i].Result1);
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                    //    {
                    //        string path;
                    //        if (PathFileXray.EndsWith(@"\") && results[i].RESFileName.StartsWith(@"\"))
                    //        {
                    //            path = PathFileXray + results[i].RESFileName.Substring(1);
                    //        }
                    //        else
                    //        {
                    //            path = PathFileXray + results[i].RESFileName;
                    //        }
                    //        string content = GetContentResult(GetLinesTextFile(path), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate);
                    //        results[i].resultText = content;
                    //        results[i].biradsCate = getBirad(content);
                    //    }
                    //}
                }
            }
        }

        private void SetDataUltrasound(ref List<ResultXray> results, string LinkPacSheet)
        {
            foreach (var rs in results)
            {
                List<string> TextSplit = new List<string>();
                if (rs.XrayType == "1")
                {
                    if (!string.IsNullOrEmpty(rs.RESFileName) && rs.RESFileName.ToLower().Contains(DefaultFileType))
                    {
                        string url;
                        rs.RESFileName = rs.RESFileName.Replace(@"\", @"/");
                        if (PathFileXray.EndsWith(@"/") && rs.RESFileName.StartsWith(@"/"))
                        {
                            url = PathFileXray + rs.RESFileName.Substring(1);
                        }
                        else
                        {
                            url = PathFileXray + rs.RESFileName;
                        }
                        TextSplit = ReadTextFromUrl(url);
                    }
                }
                else if (rs.XrayType == "2")
                {
                    TextSplit = rs.Result1.SplitLine();
                }
                else if (rs.XrayType == "3")
                {
                    TextSplit = UtilityCls.HtmlToPlainText(rs.Result1).SplitLine();
                }
                string content = GetContentResult(TextSplit, "Patient Order : " + rs.ARCIMDesc, rs.ReportBy, rs.ReportDate, rs.LicenseNo);
                rs.resultText = content;
                string hnReplace = rs.hn_no.Replace("-", "");
                rs.Link = string.Format(LinkPacSheet, rs.OEORIAccessionNumber.Replace("/", "-"), hnReplace);
            }
        }
        private void SetDataUltrasound(ref List<ResultXray> results)
        {
            int i = results.Count;
            while (i > 0)
            {
                i--;
                if (new List<string> { "UU", "UL", "UB", "UW" }.Contains(results[i].mvt_code))
                {
                    if (results[i].XrayType == "1")
                    {
                        if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                        {
                            string url;
                            results[i].RESFileName = results[i].RESFileName.Replace(@"\", @"/");
                            if (PathFileXray.EndsWith(@"/") && results[i].RESFileName.StartsWith(@"/"))
                            {
                                url = PathFileXray + results[i].RESFileName.Substring(1);
                            }
                            else
                            {
                                url = PathFileXray + results[i].RESFileName;
                            }
                            string content = GetContentResult(ReadTextFromUrl(url), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                            results[i].resultText = content;
                        }
                    }
                    else if (results[i].XrayType == "2")
                    {
                        string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }
                    else if (results[i].XrayType == "3")
                    {
                        string result = UtilityCls.HtmlToPlainText(results[i].Result1);
                        string content = GetContentResult(result.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                    }
                }
            }
        }

        private class LineTextFile
        {
            public int index { get; set; }
            public string text { get; set; }
        }
        private string GetContentResult(List<string> lines, string pateintOrder, string reportBy, string reportDate, string license)
        {
            if (lines.Count > 0)
            {
                lines = lines.StartWithAndRemoveFirstOrDefault(x => x.StartsWith("Patient Order")).ToList();
                lines = lines.EndWithLastOrDefault(x => x.StartsWith("Report Date") || x.StartsWith("Request Date")).ToList();
                lines = lines.RemoveFirstEmpty();
                lines = lines.RemoveLastEmpty();
                if (!lines.Any(x => x.StartsWith("Report By"))) lines.AddRange(new string[] { "", reportBy });
                if (!string.IsNullOrEmpty(license) && license.Trim().Length > 0)
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        if (lines[i].StartsWith("Report By"))
                        {
                            if (!lines[i].Contains(license))
                            {
                                lines[i] = lines[i].Trim() + " (" + license + ")";
                            }
                        }
                    }
                }
                if (!lines.Any(x => x.StartsWith("Report Date"))) lines.Add(reportDate);
                lines = lines.SetFormat();
                return string.Join(Environment.NewLine, lines);
            }
            return "";
        }
        private string GetContentResult(List<string> lines, string pateintOrder)
        {
            if (lines.Count > 0)
            {
                lines = lines.StartWithAndRemoveFirstOrDefault(x => x.StartsWith("Patient Order")).ToList();
                lines = lines.EndWithLastOrDefault(x => x.StartsWith("Report Date")).ToList();
                lines = lines.RemoveFirstEmpty();
                lines = lines.RemoveLastEmpty();
                lines = lines.SetFormat();
                return string.Join(Environment.NewLine, lines);
            }
            return "";
        }
        private List<string> GetLinesTextFile(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    return System.IO.File.ReadAllLines(path, Encoding.GetEncoding("windows-874")).Select(x => x.Trim()).ToList();
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }
        private string ReadTextFile(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {

                    return "";

                    //List<LineTextFile> lines = System.IO.File.ReadAllLines(path, Encoding.GetEncoding("windows-874"))
                    //                           .ToList()
                    //                           .Select((x, idx) => new LineTextFile
                    //                           {
                    //                               index = idx,
                    //                               text = x.Trim()
                    //                           }).ToList();
                    //if (lines.Count() > 0)
                    //{
                    //    int idxReportByFirst = lines.Where(x => x.text.Contains("Report By")).Select(x => x.index).FirstOrDefault();
                    //    if (idxReportByFirst > 0)
                    //    {
                    //        int idxReportBySecond = lines.Where(x => x.text.Contains("Report By")).Skip(1).Select(x => x.index).FirstOrDefault();
                    //        string[] result = lines.Where(x => x.index > idxReportByFirst && x.index < (idxReportBySecond > 0 ? idxReportBySecond : lines.Count()))
                    //                               .Select(x => x.text).ToArray();

                    //        string res = string.Join(Environment.NewLine, result);
                    //        return res;
                    //    }
                    //    else
                    //    {
                    //        return string.Empty;
                    //    }
                    //}
                    //else
                    //{
                    //    return string.Empty;
                    //}
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "GetTextFileCls", ex.Message);
                return string.Empty;
            }
        }
        private string getResult(string txt)
        {
            try
            {
                if (txt == null || txt == "")
                {
                    return string.Empty;
                }
                else
                {
                    string txtReplace = txt.Replace("\r\n", "\n").Replace("\n\n", "\n");
                    string[] txtSplit = txtReplace.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                    string[] txtFilter = txtSplit.Where(x => x.Trim() != "").ToArray();
                    return string.Join(Environment.NewLine, txtFilter);
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "GetTextFileCls", ex.Message);
                return string.Empty;
            }
        }

        private string getBirad(string txt)
        {
            try
            {
                string txtReplace = txt.Replace("\r\n", "\n").Replace("\n\n", "\n");
                string[] txtSplit = txtReplace.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] txtFilter = txtSplit.Where(x => x.Trim() != "").ToArray();
                string lineBirad = txtFilter.Where(x => x.Contains("BI-RADS") || x.Contains("BIRADS category") || x.Contains("BIRADS")).FirstOrDefault();
                int inx = lineBirad.IndexOf("BI-RADS") & lineBirad.IndexOf("BIRADS category") & lineBirad.IndexOf("BIRADS");
                if (inx >= 1)
                {
                    lineBirad = lineBirad.Substring(inx - 1);
                }
                string[] birad = lineBirad.Split(new string[] { "BI-RADS", "BIRADS category", "BIRADS", ":", " " }, StringSplitOptions.RemoveEmptyEntries);
                string biradCate = birad.Count() <= 1 ? "" : birad[0].Trim();
                return biradCate;
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "GetTextFileCls.getBirad", ex.Message);
                return string.Empty;
            }
        }

        private class TextFileCarotid
        {
            public string result { get; set; }
            public string patient_result { get; set; }
            public string patient_comt { get; set; }
        }

        private void SetDataCarotid(ref List<ResultXray> results, string LinkPacSheet)
        {
            foreach (var rs in results)
            {
                List<string> TextSplit = new List<string>();
                if (rs.XrayType == "1")
                {
                    if (!string.IsNullOrEmpty(rs.RESFileName) && rs.RESFileName.ToLower().Contains(DefaultFileType))
                    {
                        string url;
                        rs.RESFileName = rs.RESFileName.Replace(@"\", @"/");
                        if (PathFileXray.EndsWith(@"/") && rs.RESFileName.StartsWith(@"/"))
                        {
                            url = PathFileXray + rs.RESFileName.Substring(1);
                        }
                        else
                        {
                            url = PathFileXray + rs.RESFileName;
                        }
                        TextSplit = ReadTextFromUrl(url);
                    }
                }
                else if (rs.XrayType == "2")
                {
                    TextSplit = rs.Result1.SplitLine();
                }
                else if (rs.XrayType == "3")
                {
                    TextSplit = UtilityCls.HtmlToPlainText(rs.Result1).SplitLine();
                }
                string content = GetContentResult(TextSplit, "Patient Order : " + rs.ARCIMDesc);
                rs.resultText = content;
                rs.patient_result = getPatientResult(content);
                rs.patient_comt = getPatientComt(content);
            }
        }
        private void SetDataCarotid(ref List<ResultXray> results)
        {
            int i = results.Count;
            while (i > 0)
            {
                i--;
                if (results[i].mvt_code == "CD")
                {
                    if (results[i].XrayType == "1")
                    {
                        if (!string.IsNullOrEmpty(results[i].RESFileName) && results[i].RESFileName.ToLower().Contains(DefaultFileType))
                        {
                            string url;
                            results[i].RESFileName = results[i].RESFileName.Replace(@"\", @"/");
                            if (PathFileXray.EndsWith(@"/") && results[i].RESFileName.StartsWith(@"/"))
                            {
                                url = PathFileXray + results[i].RESFileName.Substring(1);
                            }
                            else
                            {
                                url = PathFileXray + results[i].RESFileName;
                            }
                            string content = GetContentResult(ReadTextFromUrl(url), "Patient Order : " + results[i].ARCIMDesc);
                            results[i].resultText = content;
                            results[i].patient_result = getPatientResult(content);
                            results[i].patient_comt = getPatientComt(content);
                        }
                    }
                    else if (results[i].XrayType == "2")
                    {
                        string content = GetContentResult(results[i].Result1.SplitLine(), "Patient Order : " + results[i].ARCIMDesc);
                        results[i].resultText = content;
                        results[i].patient_result = getPatientResult(content);
                        results[i].patient_comt = getPatientComt(content);
                    }
                    else if (results[i].XrayType == "3")
                    {
                        string result = UtilityCls.HtmlToPlainText(results[i].Result1);
                        string content = GetContentResult(result.SplitLine(), "Patient Order : " + results[i].ARCIMDesc, results[i].ReportBy, results[i].ReportDate, results[i].LicenseNo);
                        results[i].resultText = content;
                        results[i].patient_result = getPatientResult(content);
                        results[i].patient_comt = getPatientComt(content);
                    }
                }
            }
        }
        private string ReadTextFileCarotid(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    List<LineTextFile> lines = System.IO.File.ReadAllLines(path, Encoding.GetEncoding("windows-874"))
                                               .ToList()
                                               .Select((x, idx) => new LineTextFile
                                               {
                                                   index = idx,
                                                   text = x.Trim()
                                               }).ToList();
                    if (lines.Count() > 0)
                    {
                        int idxReportByFirst = lines.Where(x => x.text.Contains("Report Doctor :")).Select(x => x.index).FirstOrDefault();
                        if (idxReportByFirst > 0)
                        {
                            int idxReportBySecond = lines.Where(x => x.text.Contains("Request Date:")).Skip(1).Select(x => x.index).FirstOrDefault();
                            string[] result = lines.Where(x => x.index > idxReportByFirst && x.index < (idxReportBySecond > 0 ? idxReportBySecond : lines.Count()))
                                                   .Select(x => x.text).ToArray();

                            string res = string.Join(Environment.NewLine, result);
                            return res;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "GetTextFileCls", ex.Message);
                return string.Empty;
            }
        }
        private string getPatientResult(string txt)
        {
            try
            {
                string txtReplace = txt.Replace("\r\n", "\n").Replace("\n\n", "\n");
                string[] txtSplit = txtReplace.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] txtFilter = txtSplit.Where(x => x.Trim() != "").ToArray();
                string patientResult = txtFilter.Where(x => x.Contains("Patient Order Carotid (Result) :"))
                                                .Select(x => x == null ? null : x.Replace("Patient Order Carotid (Result) :", "").Trim())
                                                .FirstOrDefault();
                return patientResult;
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "GetTextFileCls.getPatientResult", ex.Message);
                return string.Empty;
            }
        }
        private string getPatientComt(string txt)
        {
            try
            {
                string txtReplace = txt.Replace("\r\n", "\n").Replace("\n\n", "\n");
                string[] txtSplit = txtReplace.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] txtFilter = txtSplit.Where(x => x.Trim() != "").ToArray();
                string PatientComt = txtFilter.Where(x => x.StartsWith("Comment:"))
                                              .Select(x => x == null ? null : x.Replace("Comment:", "").Trim())
                                              .FirstOrDefault();
                return PatientComt;
            }
            catch (Exception ex)
            {
                globalCls.MessageError("Webservice", "GetTextFileCls.getPatientComt", ex.Message);
                return string.Empty;
            }
        }

        private List<string> ReadTextFromUrl(string url)
        {
            try
            {
                using (System.Net.WebClient req = new System.Net.WebClient())
                {
                    req.Credentials = new System.Net.NetworkCredential("anonymous", "anonymous@example.com");
                    byte[] newFileData = req.DownloadData(url);
                    string str = Encoding.GetEncoding("windows-874").GetString(newFileData);
                    return str.Split(new string[] { Environment.NewLine, @"\n" }, StringSplitOptions.None).Select(x => x.Trim()).ToList();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

        public class ResultXray
        {
            public string hn_no { get; set; }
            public string en_no { get; set; }
            public int mvt_id { get; set; }
            public string mvt_code { get; set; }
            public DateTime? OEORISttDat { get; set; }
            public TimeSpan? OEORITimeOrd { get; set; }
            public string ARCIMRowId { get; set; }
            public string ARCIMCode { get; set; }
            public string ARCIMDesc { get; set; }
            public string RESFileName { get; set; }
            public string SSUSR_Name { get; set; }
            public string Result1 { get; set; }
            public string Result2 { get; set; }
            public TimeSpan? RESTimeVerified { get; set; }
            public DateTime? RESDateVerified { get; set; }
            public string ChildID { get; set; }
            public string ReportBy { get; set; }
            public string ReportDate { get; set; }
            public string RequestDate { get; set; }
            public string PathHTML { get; set; }
            public string resultText { get; set; }
            public string biradsCate { get; set; }
            public string patient_result { get; set; }
            public string patient_comt { get; set; }
            public string XrayType { get; set; }
            public string LicenseNo { get; set; }
            public string OEORIAccessionNumber { get; set; }
            public string Link { get; set; }
        }
    }
}


public static class UtilityXray
{
    public static List<string> SplitLine(this string str)
    {
        string tmp = str;
        List<string> result = new List<string>();
        while (tmp.IndexOf("\n") >= 0)
        {
            int inx = tmp.IndexOf("\n");
            if (tmp.Substring(0, inx + 1).Trim() != result.LastOrDefault())
            {
                result.Add(tmp.Substring(0, inx + 1).Trim());
            }
            tmp = tmp.Substring(inx + 1);
        }
        if (tmp.Trim().Length > 0) result.Add(tmp.Trim());
        return result;
    }
    public static List<string> RemoveFirstEmpty(this List<string> str)
    {
        while (str.FirstOrDefault() == string.Empty)
        {
            str.RemoveAt(0);
        }
        return str;
    }
    public static List<string> RemoveLastEmpty(this List<string> str)
    {
        while (str.LastOrDefault() == string.Empty)
        {
            str.RemoveAt(str.Count - 1);
        }
        return str;
    }
    public static List<string> SetFormat(this List<string> str)
    {
        List<string> result = new List<string>();
        foreach (string s in str)
        {
            if (s.ToUpper().StartsWith("PATIENT ORDER"))
            {
                result.Add(s);
                result.Add("");
            }
            else if (s.ToUpper().StartsWith("REPORT BY"))
            {
                result.Add("");
                result.Add(s);
            }
            else if (s != "")
            {
                result.Add(s);
            }
        }
        return result;
    }

    public static List<T> StartWithFirstOrDefault<T>(this List<T> items, Func<T, bool> predicate)
    {
        if (items == null) throw new ArgumentNullException("items");
        if (predicate == null) throw new ArgumentNullException("predicate");

        int inxOf = -1;
        for (int i = 0; i <= items.Count - 1; i++)
        {
            if (predicate(items[i]))
            {
                inxOf = i;
                break;
            }
        }
        if (inxOf > 0)
        {
            items.RemoveRange(0, inxOf);
        }
        return items;
    }
    public static List<T> StartWithAndRemoveFirstOrDefault<T>(this List<T> items, Func<T, bool> predicate)
    {
        if (items == null) throw new ArgumentNullException("items");
        if (predicate == null) throw new ArgumentNullException("predicate");

        int inxOf = -1;
        for (int i = 0; i <= items.Count - 1; i++)
        {
            if (predicate(items[i]))
            {
                inxOf = i + 1;
                break;
            }
        }
        if (inxOf > 0)
        {
            items.RemoveRange(0, inxOf);
        }
        return items;
    }
    public static List<T> EndWithLastOrDefault<T>(this List<T> items, Func<T, bool> predicate)
    {
        if (items == null) throw new ArgumentNullException("items");
        if (predicate == null) throw new ArgumentNullException("predicate");

        int inxOf = -1;
        for (int i = items.Count - 1; i > 0; i--)
        {
            if (predicate(items[i]))
            {
                inxOf = i;
                break;
            }
        }
        if (inxOf >= 0)
        {
            items.RemoveRange(inxOf + 1, items.Count - (inxOf + 1));
        }
        return items;
    }
}