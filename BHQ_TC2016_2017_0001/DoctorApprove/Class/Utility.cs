using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace DoctorApprove
{
    public class Utility
    {
        public static DateTime GetServerDateTime()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                return cdc.ExecuteQuery<DateTime>("select getdate()").FirstOrDefault();
            }
        }
    }
}