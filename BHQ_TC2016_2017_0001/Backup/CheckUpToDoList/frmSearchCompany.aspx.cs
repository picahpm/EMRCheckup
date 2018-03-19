using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class frmSearchCompany : System.Web.UI.Page
    {
        [WebMethod]
        public static List<string> GetCompanyList(string cname)
        {
            try
            {
                string strSearch = cname.Trim().ToLower();
                if (strSearch.Length >= 2)
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        List<string> result = dbc.trn_company_details
                                                 .Where(x => x.tcd_tname.ToLower().Contains(strSearch) ||
                                                             x.tcd_ename.ToLower().Contains(strSearch))
                                                 .Select(x => x.tcd_tname).Distinct().ToList();
                        //List<string> result = new List<string>();
                        //var objcomp = (from comp in dbc.trn_company_details
                        //               where comp.tcd_tname.ToLower().Contains(cname.ToLower()) ||
                        //               comp.tcd_ename.ToLower().Contains(cname.ToLower())
                        //               select new { cname = comp.tcd_tname }).GroupBy(c => c.cname).ToList();

                        //foreach (var data in objcomp)
                        //{
                        //    result.Add(data.Key);
                        //}
                        return result;
                    }
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

        [WebMethod]
        public static List<string> GetMasterCompanyList(string cname)
        {
            try
            {
                string strSearch = cname.Trim().ToLower();
                if (strSearch.Length >= 2)
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        List<string> result = dbc.mst_companies
                                                 .Where(x => x.mco_tname.ToLower().Contains(strSearch) ||
                                                             x.mco_ename.ToLower().Contains(strSearch))
                                                 .Select(x => x.mco_tname).Distinct().ToList();
                        //List<string> result = new List<string>();
                        //var objcomp = (from comp in dbc.mst_companies
                        //               where comp.mco_tname.ToLower().Contains(cname.ToLower()) ||
                        //                  comp.mco_ename.ToLower().Contains(cname.ToLower())
                        //               select new { cname = comp.mco_tname }).ToList();

                        //foreach (var data in objcomp)
                        //{
                        //    result.Add(data.cname.ToString());
                        //}
                        return result;
                    }
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

        [WebMethod]
        public static List<string> GetMasterCompanyListEng(string cname)
        {
            try
            {
                string strSearch = cname.Trim().ToLower();
                if (strSearch.Length >= 2)
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        List<string> result = dbc.mst_companies
                                                 .Where(x => x.mco_ename.ToLower().Contains(strSearch))
                                                 .Select(x => x.mco_tname).Distinct().ToList();
                        //var objcomp = (from comp in dbc.mst_companies
                        //               where
                        //                  comp.mco_ename.ToLower().Contains(cname.ToLower()) && comp.mco_status == 'A'
                        //               select new { cname = comp.mco_ename }).ToList();
                        return result;
                    }
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

        [WebMethod]
        public static List<string> GetMasterCompanyListTh(string cname)
        {
            try
            {
                string strSearch = cname.Trim().ToLower();
                if (strSearch.Length >= 2)
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        List<string> result = dbc.mst_companies
                                                 .Where(x => x.mco_tname.ToLower().Contains(strSearch))
                                                 .Select(x => x.mco_tname).Distinct().ToList();
                        //var objcomp = (from comp in dbc.mst_companies
                        //               where comp.mco_tname.ToLower().Contains(cname.ToLower()) && comp.mco_status == 'A'
                        //               select new { cname = comp.mco_tname }).ToList();
                        return result;
                    }
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }

        [WebMethod]
        public static List<string> AutoCompletePayor(string txt)
        {
            try
            {
                string strSearch = txt.Trim().ToLower();
                if (strSearch.Length >= 2)
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        List<string> result = dbc.mst_payors
                                                 .Where(x => x.msp_name.ToLower().Contains(strSearch))
                                                 .Select(x => x.msp_name).Distinct().ToList();
                        //List<string> result = (from msp in dbc.mst_payors
                        //                       where msp.msp_name.ToLower().Contains(txt.ToLower())
                        //                       select msp.msp_name).ToList();
                        return result;
                    }
                }
                else
                {
                    return new List<string>();
                }
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}