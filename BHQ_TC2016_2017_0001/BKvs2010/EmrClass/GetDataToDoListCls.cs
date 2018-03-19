using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using DBToDoList;

namespace BKvs2010.EmrClass
{
    public class GetDataToDoListCls
    {
        public List<ObjCompany> getListCompany()
        {
            using (InhToDoListDataContext tdc = new InhToDoListDataContext())
            {
                List<ObjCompany> obj = tdc.sp_get_company()
                                          .Select(x => new ObjCompany
                                          {
                                              code = x.tcd_code,
                                              name = x.tcd_tname
                                          }).ToList();
                obj.Insert(0, new ObjCompany { code = "", name = "" });
                return obj;
            }
        }

        public trn_name_check getNameCheck(string hn_no)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                trn_patient tpt = cdc.trn_patients.Where(x => x.tpt_hn_no == hn_no).FirstOrDefault();
                if (tpt != null)
                {
                    using (InhToDoListDataContext tdc = new InhToDoListDataContext())
                    {
                        trn_name_check tnc = tdc.trn_name_checks.Where(x => x.tnc_fname == tpt.tpt_first_name && x.tnc_lname == tpt.tpt_last_name).OrderByDescending(x => x.tnc_create_date).FirstOrDefault();
                        return tnc;
                    }
                }
            }
            return null;
        }

        public List<string> getListDep(string code)
        {
            using (InhToDoListDataContext tdc = new InhToDoListDataContext())
            {
                int tcd_id = tdc.trn_company_details
                             .Where(x => x.tcd_code == code)
                             .OrderByDescending(x => x.tcd_create_date)
                             .Select(x => x.tcd_id).FirstOrDefault();
                List<string> tmp = tdc.trn_name_checks
                                   .Where(x => x.tcd_id == tcd_id)
                                   .OrderBy(x => x.tnc_department)
                                   .Select(x => x.tnc_department).Distinct().ToList();
                tmp.Insert(0, "");
                return tmp;
            }
        }

        public trn_company_detail getCompanyDetail(string code)
        {
            using (InhToDoListDataContext tdc = new InhToDoListDataContext())
            {
                trn_company_detail tcd = tdc.trn_company_details
                                         .Where(x => x.tcd_code == code)
                                         .OrderByDescending(x => x.tcd_create_date).FirstOrDefault();
                return tcd;
            }
        }
    }
}
