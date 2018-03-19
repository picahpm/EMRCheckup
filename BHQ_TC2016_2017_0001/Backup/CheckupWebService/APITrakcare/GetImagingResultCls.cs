using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using DBCheckup;

namespace CheckupWebService.APITrakcare
{
    public class GetImagingResultCls
    {
        public List<ImagingResult> ByXrayResultList(string hn, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                    {
                        var RawMateWS = ws.GetXrayResultList_XrayResult(hn, dateStart.ToString("yyyy-MM-dd"), dateEnd.ToString("yyyy-MM-dd"), "").AsEnumerable();
                        var RawMateFirstOnGrp = RawMateWS.GroupBy(x => new { en = x.Field<string>("PAADMADMNo"), order = x.Field<string>("ARCIMRowId") })
                                                         .Select(x => x.OrderByDescending(y => y.Field<string>("ChildID")).FirstOrDefault())
                                                         .ToList();

                        List<ImagingResult> results = (from mate in RawMateFirstOnGrp
                                                       join mst in contxt.mst_order_plans
                                                       on mate.Field<string>("ARCIMRowId") equals mst.mop_item_row_id
                                                       select new ImagingResult
                                                       {
                                                           hn_no = mate.Field<string>("PAPMINo"),
                                                           en_no = mate.Field<string>("PAADMADMNo"),
                                                           mvt_id = mst.mvt_id,
                                                           mvt_code = mst.mst_event.mvt_code,
                                                           OEORISttDat = mate.Field<DateTime?>("OEORISttDat"),
                                                           OEORITimeOrd = mate.Field<TimeSpan?>("OEORITimeOrd"),
                                                           ARCIMCode = mate.Field<string>("ARCIMCode"),
                                                           ARCIMDesc = mate.Field<string>("ARCIMDesc"),
                                                           ARCIMRowId = mate.Field<string>("ARCIMRowId"),
                                                           ChildID = mate.Field<string>("ChildID"),
                                                           PathHTML = mate.Field<string>("PathHTML"),
                                                           ReportBy = mate.Field<string>("ReportBy"),
                                                           ReportDate = mate.Field<string>("ReportDate"),
                                                           RequestDate = mate.Field<string>("RequestDate"),
                                                           RESDateVerified = mate.Field<DateTime?>("RESDateVerified"),
                                                           RESTimeVerified = mate.Field<TimeSpan?>("RESTimeVerified"),
                                                           RESFileName = mate.Field<string>("RESFileName"),
                                                           Result1 = mate.Field<string>("Result1"),
                                                           Result2 = mate.Field<string>("Result2"),
                                                           SSUSR_Name = mate.Field<string>("SSUSRName"),
                                                           XrayType = mate.Field<string>("XrayType"),
                                                           LicenseNo = mate.Field<string>("LicenseNo"),
                                                           OEORIAccessionNumber = mate.Field<string>("OEORIAccessionNumber")
                                                       }).ToList();

                        if (results.Count > 0)
                        {
                            string DefaultFileType = Class.GetDBConfigCls.GetConfig("DefaultFileTypeXray");
                            string PathFileXray = Class.GetDBConfigCls.GetConfig("PathFileXray");
                            string LinkPacSheet = Class.GetDBConfigCls.GetConfig("LinkPacSheet");
                            string LinkPDF = Class.GetDBConfigCls.GetConfig("LinkPDF");

                            var grpEvent = results.GroupBy(x => x.mvt_code).ToList();
                            foreach (var grp in grpEvent)
                            {
                                var grpList = grp.ToList();
                                switch (grp.Key)
                                {
                                    case "XR":
                                        SetDataXray(ref grpList, LinkPacSheet, DefaultFileType, PathFileXray);
                                        break;
                                    case "UU":
                                    case "UL":
                                    case "UB":
                                    case "UW":
                                        SetDataUltrasound(ref grpList, LinkPacSheet, DefaultFileType, PathFileXray);
                                        break;
                                    case "UG":
                                        SetDataUGI(ref grpList, LinkPacSheet, DefaultFileType, PathFileXray);
                                        break;
                                    case "DM":
                                        SetDataMammogram(ref grpList, LinkPacSheet, DefaultFileType, PathFileXray);
                                        break;
                                    case "BD":
                                        SetDataBMD(ref grpList, LinkPacSheet, DefaultFileType, PathFileXray);
                                        break;
                                    case "CD":
                                        SetDataCarotid(ref grpList, DefaultFileType, PathFileXray);
                                        break;
                                    case "ES":
                                    case "EC":
                                    case "EK":
                                        SetLink(ref grpList, LinkPDF);
                                        break;
                                }
                            }
                        }
                        return results;
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("Webservice", "GetTextFileCls.GetResultXray", ex.Message);
                return new List<ImagingResult>();
            }
        }

        private void SetDataUGI(ref List<ImagingResult> results, string LinkPacSheet, string DefaultFileType, string PathFileXray)
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
        private void SetDataXray(ref List<ImagingResult> results, string LinkPacSheet, string DefaultFileType, string PathFileXray)
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
        private void SetDataBMD(ref List<ImagingResult> results, string LinkPacSheet, string DefaultFileType, string PathFileXray)
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
        private void SetDataMammogram(ref List<ImagingResult> results, string LinkPacSheet, string DefaultFileType, string PathFileXray)
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
        private void SetDataUltrasound(ref List<ImagingResult> results, string LinkPacSheet, string DefaultFileType, string PathFileXray)
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
        private void SetDataCarotid(ref List<ImagingResult> results, string DefaultFileType, string PathFileXray)
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

        private void SetLink(ref List<ImagingResult> results, string LinkPDF)
        {
            foreach (var rs in results)
            {
                rs.Link = LinkPDF + rs.RESFileName.Replace(@"\", @"/");
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
                    List<string> txtSplit = str.Split(new string[] { Environment.NewLine, @"\n" }, StringSplitOptions.None).Select(x => x.Trim()).ToList();
                    return txtSplit;
                }
            }
            catch
            {
                return new List<string>();
            }
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
                Class.globalCls.MessageError("Webservice", "GetTextFileCls.getBirad", ex.Message);
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
                Class.globalCls.MessageError("Webservice", "GetTextFileCls.getPatientResult", ex.Message);
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
                Class.globalCls.MessageError("Webservice", "GetTextFileCls.getPatientComt", ex.Message);
                return string.Empty;
            }
        }
    }

    public class ImagingResult
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