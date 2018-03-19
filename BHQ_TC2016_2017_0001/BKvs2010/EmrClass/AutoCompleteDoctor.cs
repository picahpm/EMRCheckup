using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class AutoCompleteDoctor
    {
        public List<DoctorProfile> GetDoctorData()
        {
            List<DoctorProfile> DocName = new List<DoctorProfile>();
            try
            {                
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    DataTable dt = ws.GetCareprovider("");
                    if (dt != null)
                    {
                        var result = dt.AsEnumerable().Where(x => x.Field<string>("SSUSR_Initials") != null && x.Field<string>("CTPCP_Desc") != null)
                        .Select(x => new DoctorProfile
                        {
                            SSUSR_Initials = x.Field<string>("SSUSR_Initials"),
                            CTPCP_Desc = x.Field<string>("CTPCP_Desc"),
                            CTPCP_Code = x.Field<string>("CTPCP_Code"),
                            CTPCP_SMCNo = x.Field<string>("CTPCP_SMCNo")
                        }).ToList();
                        result.Insert(0, new DoctorProfile { SSUSR_Initials = "", CTPCP_Desc = "", CTPCP_Code = "", CTCPT_Desc = "", DoctorName = "", CTPCP_SMCNo = ""});

                        DocName = result.ToList();                        
                    }
                }
                return DocName;
            }
            catch
            { return DocName; }
        }

        public DoctorName GetDoctorName(string CTPCP_Desc)
        {            
            try
            {
                string[] name = CTPCP_Desc.Split(new char[]{'/'}, 2);
                if (name.Count() == 0 || name.Count() == 1)
                {
                    return new DoctorName
                    {
                        NameEN = CTPCP_Desc,
                        NameTH = CTPCP_Desc
                    };
                }
                else
                {
                    return new DoctorName
                    {
                        NameEN = name[0],
                        NameTH = name[1]
                    };
                }
            }
            catch
            {
                return new DoctorName
                {
                    NameEN = CTPCP_Desc,
                    NameTH = CTPCP_Desc
                }; 
            }
            
        }
    }
}
