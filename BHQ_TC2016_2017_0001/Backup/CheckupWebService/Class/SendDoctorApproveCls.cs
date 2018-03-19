using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

using CrystalDecisions.CrystalReports.Engine;
using DBCheckup;

using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;

namespace CheckupWebService.Class
{
    public partial class SendDoctorApproveCls
    {
        public bool? SendToDoctorApprove(int tpr_id, string rptCode, string username)
        {
            try
            {
                DateTime dateNow = DateTime.Now;
                List<string> bookColorRptCode = new List<string> { "BK306", "BK307" };
                List<string> bookRptCode = new List<string> { "BK301", "BK304" };
                List<string> bookOnePageRptCode = new List<string> { "BK302", "BK303" };
                if (bookColorRptCode.Contains(rptCode) || bookRptCode.Contains(rptCode) || bookOnePageRptCode.Contains(rptCode))
                {

                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        trn_patient_regi patientRegis = cdc.trn_patient_regis
                                                           .Where(x => x.tpr_id == tpr_id)
                                                           .FirstOrDefault();
                        trn_patient_doctor_approve doctorApprove = patientRegis.trn_patient_doctor_approve;
                        if (doctorApprove != null && doctorApprove.tpda_status != "CBB" && doctorApprove.tpda_status != "NAP")
                        {
                            return false;
                        }

                        ReportDocument rpt = GetReportDocumentCls.getRptDoc(rptCode);
                        if (rpt != null)
                        {
                            GetReportDocumentCls.SetDBLogonForReport(rpt);
                            rpt.SetParameterValue("@tpr_id", tpr_id);
                            string pdfPath = HttpContext.Current.Request.MapPath(@".\PdfTemp\");
                            string pdfFileName = tpr_id.ToString("0000000000") + DateTime.Now.ToString("ssmmHHyyyyMMdd") + ".pdf";
                            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfPath + pdfFileName);
                            string BookImgForApprove = GetDBConfigCls.GetConfig("BookImgForApprove");
                            Directory.CreateDirectory(BookImgForApprove + tpr_id.ToString("0000000000"));
                            foreach (var file in Directory.GetFiles(BookImgForApprove + tpr_id.ToString("0000000000")))
                            {
                                File.Delete(file);
                            }
                            PdfToJpg(Path.GetFullPath(pdfPath + pdfFileName), BookImgForApprove + tpr_id.ToString("0000000000"));

                            rpt.Dispose();
                            File.Delete(Path.GetFullPath(pdfPath + pdfFileName));

                            if (doctorApprove == null)
                            {
                                doctorApprove = new trn_patient_doctor_approve();
                                doctorApprove.tpr_id = tpr_id;
                                cdc.trn_patient_doctor_approves.InsertOnSubmit(doctorApprove);
                            }
                            doctorApprove.tpda_status = "WFA";
                            doctorApprove.tpda_path_image = tpr_id.ToString("0000000000");
                            doctorApprove.tpda_process_by = username;
                            doctorApprove.tpda_process_date = dateNow;
                            cdc.SubmitChanges();
                            if (patientRegis.tpr_req_inorout_doctor == "UT")
                            {

                            }
                            return true;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                globalCls.MessageError("approve", "approve", ex.Message);
                return false;
            }
        }

        public bool? CallBackToBook(int tpr_id, string username)
        {
            try
            {
                DateTime dateNow = DateTime.Now;
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis
                                                        .Where(x => x.tpr_id == tpr_id)
                                                        .FirstOrDefault();

                    trn_patient_doctor_approve doctorApprove = patientRegis.trn_patient_doctor_approve;

                    if (doctorApprove.tpda_status == "API")
                    {
                        return null;
                    }
                    doctorApprove.tpda_status = "CBB";
                    doctorApprove.tpda_process_by = username;
                    doctorApprove.tpda_process_date = dateNow;
                    cdc.SubmitChanges();
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }

        public void SendToDoctorApproveBackGround()
        {
            try
            {

            }
            catch
            {

            }
        }

        private void PdfToJpg(string inputPDFFile, string outputImagesPath)
        {
            try
            {
                GhostscriptVersionInfo _lastInstalledVersion = null;
                GhostscriptRasterizer _rasterizer = null;
                int desired_x_dpi = 96;
                int desired_y_dpi = 96;

                _lastInstalledVersion =
                    GhostscriptVersionInfo.GetLastInstalledVersion(
                            GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                            GhostscriptLicense.GPL);

                _rasterizer = new GhostscriptRasterizer();

                _rasterizer.Open(inputPDFFile, _lastInstalledVersion, false);

                for (int pageNumber = 1; pageNumber <= _rasterizer.PageCount; pageNumber++)
                {
                    string pageFilePath = Path.Combine(outputImagesPath, "Page-" + pageNumber.ToString("00") + ".jpg");

                    Image img = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                    img.Save(pageFilePath, ImageFormat.Jpeg);
                }
                _rasterizer.Dispose();
            }
            catch
            {

            }
        }

        private string GenerateContentEmail(string doctorcode)
        {
            return "";
        }
    }
}