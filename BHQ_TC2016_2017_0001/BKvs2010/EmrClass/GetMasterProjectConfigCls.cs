using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class GetMasterProjectConfigCls
    {
        public string GetConfigFromDB(string configCode)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    return cdc.mst_project_configs.Where(x => x.mpc_code == configCode).Select(x => x.mpc_value).FirstOrDefault();
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
