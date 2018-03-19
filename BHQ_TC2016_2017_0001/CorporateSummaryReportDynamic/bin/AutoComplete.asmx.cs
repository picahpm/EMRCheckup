using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DBCheckup;
using System.Web.Script.Services;

namespace CorporateSummaryReportDynamic
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<string> GetListCorp(string prefix)
        {
            List<string> data = new List<string>();

            using (DBToDoList.InhToDoListDataContext itc = new DBToDoList.InhToDoListDataContext())
            {
                var coms = (from icd in itc.index_trn_company_details
                            join tcd in itc.trn_company_details
                            on icd.tcd_id equals tcd.tcd_id
                            where tcd.tcd_tname.Contains(prefix)
                            select tcd.tcd_tname).ToList();
                data = coms;
            }
            return data;
        }

        [WebMethod]
        public string Test(string prefix)
        {
            string data = "Received " + prefix + " markers.";
            return data;
        }
    }
}
