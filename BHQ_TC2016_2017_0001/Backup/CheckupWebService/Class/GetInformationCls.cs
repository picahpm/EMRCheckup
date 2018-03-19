using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class GetInformationCls
    {
        public Informations Get(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    return cdc.trn_patient_regis
                              .Where(x => x.tpr_id == tpr_id)
                              .Select(x => new Informations
                              {
                                  hn = x.trn_patient.tpt_hn_no,
                                  enRowID = x.tpr_en_rowid,
                                  arrived = x.trn_patient_regis_detail != null && x.trn_patient_regis_detail.tpr_real_arrived_date != null
                                            ? x.trn_patient_regis_detail.tpr_real_arrived_date
                                            : x.tpr_arrive_date
                              }).FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }
    }

    public class Informations
    {
        public string hn { get; set; }
        public string enRowID { get; set; }
        public DateTime? arrived { get; set; }
    }
}