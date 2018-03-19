using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web;
using System.Web.Services;
using System.Data;
using DBToDoList;

namespace CheckUpToDoList
{
    /// <summary>
    /// Summary description for WSCheckUpToDoList
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WSCheckUpToDoList : System.Web.Services.WebService
    {
        [WebMethod]
        public List<string> AutoCompleteCompanyCode(string txt)
        {
            InhToDoListDataContext tdc = new InhToDoListDataContext();
            List<string> payor = (from msc  in tdc.mst_companies
                                  where msc.mco_code.Contains(txt)
                                  select msc.mco_code).ToList();
            return payor;
        }

        [WebMethod]
        public List<string> AutoCompletePayor(string txt)
        {
            InhToDoListDataContext tdc = new InhToDoListDataContext();
            List<string> payor = (from msp in tdc.mst_payors
                                    where msp.msp_name.ToLower().Contains(txt.ToLower())
                                    select msp.msp_name).ToList();
            return payor;
        }

        [WebMethod]
        public List<string> AutoCompleteReqDoc(string txt)
        {
            Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
            List<string> result = (from t in ws.GetCareprovider(txt).AsEnumerable()
                                   where t.Field<string>("DoctorName").Contains(txt)
                                   select t.Field<string>("DoctorName")).ToList();
            return result;
        }
    }
}
