using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DBCheckup;

namespace CheckupPreviewReport.Classes
{
    public class GetReportDocument
    {
        public ReportDocument Get(string ReportCode)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var file = cdc.mst_reports
                                  .Where(x => x.mrt_code == ReportCode && x.mrt_status == 'A')
                                  .Select(x => new
                                  {
                                      x.mrt_path_file,
                                      x.mrt_file_name
                                  }).FirstOrDefault();
                    var pathFile = @"\\" + GetMasterProjectConfigCls.GetConfigFromDB("ServerReport") + @"\" + file.mrt_path_file + @"\" + file.mrt_file_name;
                    if (!File.Exists(pathFile))
                    {
                        throw new Exception("File Report not Found.");
                    }
                    else
                    {
                        ReportDocument rpt = new ReportDocument();
                        rpt.Load(pathFile);
                        new SetReportDatabase().SetDBLogonForReport(rpt);
                        rpt.Refresh();
                        rpt.VerifyDatabase();
                        return rpt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}