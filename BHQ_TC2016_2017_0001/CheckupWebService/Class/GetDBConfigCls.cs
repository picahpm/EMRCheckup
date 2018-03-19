using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace CheckupWebService.Class
{
    public static class GetDBConfigCls
    {
        public static string GetConfig(string mpc_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string cs = cdc.mst_project_configs.Where(x => x.mpc_code == mpc_code).Select(x => x.mpc_value).FirstOrDefault();
                    return cs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
