using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DBCheckup;

namespace CheckupPreviewReport.Classes
{
    public class SetReportDatabase
    {
        public void SetDBLogonForReport(ReportDocument reportDocument)
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = GetMasterProjectConfigCls.GetConfigFromDB("ServerDataBase");
            connectionInfo.DatabaseName = GetMasterProjectConfigCls.GetConfigFromDB("DataBaseName");
            connectionInfo.UserID = GetMasterProjectConfigCls.GetConfigFromDB("DataBaseUserName");
            connectionInfo.Password = GetMasterProjectConfigCls.GetConfigFromDB("DataBasePassword");
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