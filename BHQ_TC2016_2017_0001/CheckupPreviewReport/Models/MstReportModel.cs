using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckupPreviewReport.Models
{
    public class MstReportModel
    {
        public MstReportModel()
        {
            ListMstReport = new List<MstReport>();
        }

        List<MstReport> ListMstReport { get; set; }
    }


    public class MstReport
    {
        public string ReportNameTH { get; set; }
        public string ReportNameEN { get; set; }
        public string ReportFilePath { get; set; }
    }
}