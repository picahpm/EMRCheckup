using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class RetrievePackageCls
    {
        public void Retrieve(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    int en_rowid = Convert.ToInt32(regis.tpr_en_rowid);
                }
            }
            catch
            {

            }
        }
    }
}