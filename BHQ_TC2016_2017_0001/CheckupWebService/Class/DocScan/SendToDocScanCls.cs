using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using DBCheckup;
using System.Data;
using System.Collections;

namespace CheckupWebService.Class.DocScan
{
    public partial class SendToDocScanCls
    {
        

        public SendToDocScanCls()
        {

        }
        public string Send(int tpr_id, string mrt_code, string mhs_code, string username)
        {
            try
            {
                ReportDocument rpt = Class.GetReportDocumentCls.getRptDoc(mrt_code);
                if (rpt != null)
                {
                    rpt.SetParameterValue("@tpr_id", tpr_id);
                    rpt.SetParameterValue("@print_User", username);
                    rpt.SetParameterValue("@mrt_code", mrt_code);

                    for (int i = 0; i < rpt.ParameterFields.Count; i++)
                    {
                        if (rpt.ParameterFields[i].Name == "@isDocScan")
                        {
                            rpt.SetParameterValue("@isDocScan", true);
                            break;
                        }
                    }
                }
                int pageNo = rpt.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());
                Stream streamPDF = rpt.ExportToStream(ExportFormatType.PortableDocFormat);
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = globalCls.GetServerDateTime();

                    var patient = cdc.trn_patient_regis
                                     .Where(x => x.tpr_id == tpr_id)
                                     .Select(x => new
                                     {
                                         hn = x.trn_patient.tpt_hn_no.Replace("-", ""),
                                         en = x.tpr_en_no.Replace("-", "")
                                     }).FirstOrDefault();

                    var reports = cdc.mst_report_docscans
                                     .Where(x => x.mst_report.mrt_code == mrt_code &&
                                                 x.mst_report.mrt_status == 'A' &&
                                                 dateNow >= x.mst_report.mrt_effective_date.Value &&
                                                 x.mds_seq <= pageNo &&
                                                 x.mds_seq != null &&
                                                 (x.mst_report.mrt_expire_date != null ? (dateNow <= x.mst_report.mrt_expire_date.Value) : true))
                                     .Select(x => new
                                     {
                                         x.mds_seq,
                                         x.mds_page_no,
                                         x.mds_doc_code,
                                         pageNo = x.mds_page_code + x.mds_page_no
                                     }).ToList();

                    string careproviderCode = cdc.mst_user_types
                                                 .Where(x => x.mut_username == username)
                                                 .Select(x => x.mut_carevider_code)
                                                 .FirstOrDefault();
                    
                    List<int> listPage = reports.Select(x => (int)x.mds_seq).ToList();
                    List<PdfToJpgCls.ImageBinary> images = new PdfToJpgCls().Export(streamPDF, listPage);
                    foreach (var img in images)
                    {
                        var page = reports.Where(x => x.mds_seq == img.page_no).FirstOrDefault();
                        using (Service.WS_SendToDocscanCls wsSaveDocscan = new Service.WS_SendToDocscanCls())
                        {
                            string DocscanResult = wsSaveDocscan.SaveToDocscan(img.img, patient.hn, patient.en, page.pageNo, careproviderCode, mhs_code, page.mds_doc_code, "EMRCHECKUP", "EMRCHECKUP");
                            if (!string.IsNullOrEmpty(DocscanResult))
                            {
                                return DocscanResult;
                            }
                        }
                    }
                    return images.Count.ToString() + " Document(s)" + " Sent To Docscan Completed.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
