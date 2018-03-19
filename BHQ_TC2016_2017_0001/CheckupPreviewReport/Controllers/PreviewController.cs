using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CheckupPreviewReport.Classes;

using CheckupPreviewReport.Models;

namespace CheckupPreviewReport.Controllers
{
    public class PreviewController : Controller
    {
        [HttpGet]
        public ActionResult Report(int? tpr_id, string ReportCode, string Username)
        {
            try
            {
                if (tpr_id == null || string.IsNullOrEmpty(ReportCode))
                {
                    return View();
                }
                else
                {
                    var mstRpt = new GetMstReport().Get(ReportCode);
                    ReportDocument rpt = new ReportDocument();
                    rpt.Load(mstRpt.ReportFilePath);
                    new SetReportDatabase().SetDBLogonForReport(rpt);
                    rpt.Refresh();
                    rpt.VerifyDatabase();

                    rpt.SetParameterValue("@tpr_id", tpr_id);
                    rpt.SetParameterValue("@mrt_code", ReportCode);

                    if (!string.IsNullOrEmpty(Username))
                    {
                        rpt.SetParameterValue("@print_user", Username);
                    }

                    Stream stream = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                    rpt.Close();
                    rpt.Dispose();
                    return File(stream, "application/pdf");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View();
            }
        }
    }
}
