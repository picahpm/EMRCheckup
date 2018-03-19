using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace CheckupBO.Class
{
    public static class GetDBConfigCls
    {
        public static string GetConfig(string mpc_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    return cdc.mst_project_configs.Where(x => x.mpc_code == mpc_code).Select(x => x.mpc_value).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
