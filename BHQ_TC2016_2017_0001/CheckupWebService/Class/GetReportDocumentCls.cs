using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DBCheckup;

namespace CheckupWebService.Class
{
    public static class GetReportDocumentCls
    {
        public static ReportDocument getRptDoc(string rptCode)
        {
            ReportDocument cryRpt = new ReportDocument();
            cryRpt = null;
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                var result = (from row in dbc.mst_reports
                              where row.mrt_code == rptCode
                              select row).FirstOrDefault();
                if (result != null)
                {
                    string ServerReport = GetDBConfigCls.GetConfig("ServerReport");
                    try
                    {
                        //using (Class.NetworkShareAccesser.Access(Program.serverIP, domainConnServer, usernameConnServer, passwordConnServer))
                        //using (Class.NetworkShareAccesser.Access(Program.serverIP, usernameConnServer, passwordConnServer))
                        //{
                        string pathRpt = @"\\" + ServerReport + @"\" + result.mrt_path_file + @"\" + result.mrt_file_name;
                        if (pathRpt != string.Empty)
                        {
                            if (File.Exists(pathRpt))
                            {
                                cryRpt = new ReportDocument();
                                cryRpt.Load(pathRpt);
                                SetDBLogonForReport(cryRpt);
                            }
                            else
                            {
                                cryRpt = null;
                            }
                        }
                        else
                        {
                            cryRpt = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        globalCls.MessageError("approve", "approve", ex.Message);
                    }

                }
            }
            //clearConnection();
            if (cryRpt != null)
            {
                try
                {
                    cryRpt.Refresh();
                    cryRpt.VerifyDatabase();

                }
                catch (Exception ex)
                {
                    globalCls.MessageError("approve", "approve", ex.Message);

                }
            }
            return cryRpt;
        }
        //public static ReportDocument getRptDocApp(string rptCode)
        //{
        //    ReportDocument cryRpt = new ReportDocument();
        //    cryRpt = null;
        //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
        //    {
        //        var result = (from row in dbc.mst_reports
        //                      where row.mrt_code == rptCode
        //                      select row).FirstOrDefault();
        //        if (result != null)
        //        {
        //            string ServerReport = GetDBConfigCls.GetConfig("ServerReport");
        //            try
        //            {
        //                //using (Class.NetworkShareAccesser.Access(Program.serverIP, domainConnServer, usernameConnServer, passwordConnServer))
        //                //using (Class.NetworkShareAccesser.Access(Program.serverIP, usernameConnServer, passwordConnServer))
        //                //{
        //                string pathRpt = @"\\" + ServerReport + @"\" + result.mrt_path_file + @"\" + result.mrt_file_name;
        //                if (pathRpt != string.Empty)
        //                {
        //                    if (File.Exists(pathRpt))
        //                    {
        //                        cryRpt = new ReportDocument();
        //                        cryRpt.Load(pathRpt);
        //                        SetDBLogonForReport(cryRpt);
        //                    }
        //                    else
        //                    {
        //                        cryRpt = null;
        //                    }
        //                }
        //                else
        //                {
        //                    cryRpt = null;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                globalCls.MessageError("approve", "approve", ex.Message);
        //            }

        //        }
        //    }
        //    //clearConnection();
        //    if (cryRpt != null)
        //    {
        //        try
        //        {
        //            cryRpt.Refresh();
        //            cryRpt.VerifyDatabase();

        //        }
        //        catch (Exception ex)
        //        {
        //            globalCls.MessageError("approve", "approve", ex.Message);

        //        }
        //    }
        //    return cryRpt;
        //}


        public static void SetDBLogonForReport(ReportDocument reportDocument)
        {
            string ServerDataBase = GetDBConfigCls.GetConfig("ServerDataBase");
            string DataBaseName = GetDBConfigCls.GetConfig("DataBaseName");
            string DataBaseUserName = GetDBConfigCls.GetConfig("DataBaseUserName");
            string DataBasePassword = GetDBConfigCls.GetConfig("DataBasePassword"); 

            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = ServerDataBase;

            connectionInfo.DatabaseName = DataBaseName;
            connectionInfo.UserID = DataBaseUserName;
            connectionInfo.Password = DataBasePassword;
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }
    }
}