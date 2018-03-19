using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DBCheckup;
using CheckupPreviewReport.Models;

namespace CheckupPreviewReport.Classes
{
    public class GetMstReport
    {
        public MstReport Get(string ReportCode)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string server = GetMasterProjectConfigCls.GetConfigFromDB("ServerReport");
                    if (GetLocalIP() == server)
                    {
                        server = @"D:\www";
                    }
                    else
                    {
                        server = @"\\" + server;
                    }
                    var result = cdc.mst_reports
                                    .Where(x => x.mrt_code == ReportCode && x.mrt_status == 'A')
                                    .Select(x => new MstReport
                                    {
                                        ReportNameTH = x.mrt_tname,
                                        ReportNameEN = x.mrt_ename,
                                        ReportFilePath = server + @"\" + x.mrt_path_file + @"\" + x.mrt_file_name
                                    }).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetLocalIP()
        {
            string _IP = null;

            // Resolves a host name or IP address to an IPHostEntry instance.
            // IPHostEntry - Provides a container class for Internet host address information. 
            System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            // IPAddress class contains the address of a computer on an IP network. 
            foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
            {
                // InterNetwork indicates that an IP version 4 address is expected 
                // when a Socket connects to an endpoint
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    _IP = _IPAddress.ToString();
                }
            }
            return _IP;
        }
    }
}