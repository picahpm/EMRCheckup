using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.PrePareData
{
    public partial class PrepareDataCls
    {
        public bool Prepared()
        {
            try
            {
                Program.AssemblyVersion = Program.GetAssemblyVersion();
                Program.LocalIP = Program.GetLocalIP();

                StaticDataCls.ProjectName = GetDataConfig("ProjectName") + Program.AssemblyVersion + " ";
                StaticDataCls.WSTrakcareUrl = GetDataConfig("WSTrakcareUrl");
                StaticDataCls.WSDocScanUrl = GetDataConfig("WSDocScanUrl");
                StaticDataCls.WSDocScanPreviewUrl = GetDataConfig("WSDocScanPreviewUrl");
                StaticDataCls.WSPathWay = GetDataConfig("WSPathWay");
                StaticDataCls.UseUnitDisplay = GetDataConfig("UseUnitDisplay") == "true" ? true : false;
                StaticDataCls.UseMenu = GetDataConfig("UseMenu") == "true" ? true : false;
                StaticDataCls.ServerDataBase = GetDataConfig("ServerDataBase");
                StaticDataCls.ServerReport = GetDataConfig("ServerReport");
                StaticDataCls.DataBaseName = GetDataConfig("DataBaseName");
                StaticDataCls.DataBaseUserName = GetDataConfig("DataBaseUserName");
                StaticDataCls.DataBasePassword = GetDataConfig("DataBasePassword");
                StaticDataCls.ToDoListUrl = GetDataConfig("ToDoListUrl");
                StaticDataCls.UserManualPath = GetDataConfig("UserManualPath");
                StaticDataCls.StatusLoginUrl = GetDataConfig("StatusLoginUrl");
                StaticDataCls.QueueDetailUrl = GetDataConfig("QueueDetailUrl");
                StaticDataCls.WSTrakcareLoginUrl = GetDataConfig("WSTrakcareLoginUrl");
                StaticDataCls.QuesOccmedUrl = GetDataConfig("QuesOccmedUrl");
                StaticDataCls.PathFileLogo = GetDataConfig("PathFileLogo");
                StaticDataCls.HoldTime = GetHoldTime();

                StaticDataCls.urlPreviewReport = GetDataConfig("urlPreviewReport");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string GetDataConfig(string code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_project_config mst = cdc.mst_project_configs
                                                .Where(x => x.mpc_code == code)
                                                .FirstOrDefault();
                    return mst.mpc_value;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("PrepareDataCls", "GetDataConfig(string code)", ex, false);
                throw ex;
            }
        }

        public class ConfigHoldTime
        {
            public int mhs_id { get; set; }
            public int holdTime { get; set; }
        }
        public List<ConfigHoldTime> GetHoldTime()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<ConfigHoldTime> mst = cdc.mst_config_dtls
                                                  .Where(x => x.mfd_code == "HD" &&
                                                              x.mst_config_hdr.mfh_code == "HD")
                                                  .Select(x => new ConfigHoldTime
                                                  {
                                                      mhs_id = x.mst_config_hdr.mhs_id,
                                                      holdTime = (int)x.mfd_value
                                                  }).ToList();
                    return mst;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("PrepareDataCls", "GetDataConfig(string code)", ex, false);
                throw ex;
            }
        }
    }
}
