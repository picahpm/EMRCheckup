using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CheckUpToDoList
{
    /// <summary>
    /// Summary description for GetDataTrakCare
    /// </summary>
    public class GetDataTrakCare : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "no-cache");

            string type = context.Request["type"];
            if (type == "payor")
            {
                string payor = context.Request["param1"];
                context.Response.Write(genTableListPaymentAgreement(payor));
            }
            else if (type == "req_doc")
            {
                string docName = context.Request["param1"];
                context.Response.Write(AutoCompleteReqDoc(docName));
            }
            else if (type == "ordersetall")
            {
                string docName = context.Request["input"];
                context.Response.Write(AutoCompleteGetAllOrderSet(docName));
            }
        }
        public string AutoCompleteGetAllOrderSet(string docName)
        {
            Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
            var result = (from dn in ws.GetAllOrderSet(docName).AsEnumerable()
                          where dn.Field<string>("ARCOS_Desc").ToLower().Contains(docName.ToLower()) ||
                          dn.Field<string>("ARCOS_Code").ToLower().Contains(docName.ToLower())
                          select new { desc = dn.Field<string>("ARCOS_Code") + "|,|" + dn.Field<string>("ARCOS_Desc") + "|,|" + dn.Field<string>("ARCIC_Desc") }
                            
                          ).ToList();
            string tmpStr = string.Empty;
            string tmpConj = "<,>";
           var result2 = (from t1 in result
                      select t1.desc).ToList();
           foreach (object st in result2)
            {
                tmpStr = tmpStr + HttpContext.Current.Server.HtmlEncode(st.ToString()) + tmpConj;
                //tmpStr = tmpStr + HttpContext.Current.Server.HtmlDecode(st.ToString()) + tmpConj;
            }
            if (!string.IsNullOrEmpty(tmpStr)) tmpStr.Substring(1, tmpStr.Length - tmpConj.Length);
            return  tmpStr;
        }
        public string AutoCompleteReqDoc(string docName)
        {
            docName = docName.ToLower();
            Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
            var a = from dn in ws.GetCareprovider("").AsEnumerable() select dn;
            var result = (from dn in ws.GetCareprovider("").AsEnumerable()
                          where dn.Field<string>("DoctorName").ToLower().Contains(docName) ||
                                dn.Field<string>("CTPCP_Desc").ToLower().Contains(docName) ||
                                dn.Field<string>("CTPCP_Code").ToLower().Contains(docName)
                          select new docdata
                          {
                              DocNameE = dn.Field<string>("CTPCP_Desc"),
                              DocNameT = dn.Field<string>("DoctorName"),
                              DocCode = dn.Field<string>("CTPCP_Code")
                          }).ToList();
            string tmpStr = string.Empty;
            string tmpConj = "<,>";
            foreach (docdata st in result)
            {
                string[] temp = st.DocNameE.Split('/');
                tmpStr += st.DocCode + "/" + st.DocNameT + "/" + (temp != null ? temp[0] : "") + tmpConj; //+ st.DocCode+"," 
            }
            if (!string.IsNullOrEmpty(tmpStr)) tmpStr.Substring(1, tmpStr.Length - tmpConj.Length);
            return tmpStr;
        }

        public DataTable genTableListPaymentAgreement(string payor)
        {
            Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
            DataTable dt = ws.ListPaymentAgreement(payor);
            
           // string tmpStr = "<td>No.</td><td>Code</td><td>Desc</td><td>...</td>";
            //string tmpStr = "";
            ////int i = 0;
            //string strsplit = "";
            //foreach (DataRow dr in dt.Rows)
            //{
            //    tmpStr +=strsplit + dr["AUXIT_Code"].ToString() +","+ dr["AUXIT_Desc"].ToString().Replace(",","|x|");
            //    strsplit = "|*|";
            //    //tmpStr += "<tr><td>" + (i + 1).ToString() + "</td><td>" + dr["AUXIT_Code"].ToString() + "</td>";
            //    //tmpStr += "<td>" + dr["AUXIT_Desc"].ToString() + "</td>";
            //    //tmpStr += "<td><button type='button' id='btnTest' onclick='delRowPayAgree(" + i.ToString() + ")'>X</button></td></tr>";
            //    //i++;
            //}
            return dt;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    class docdata
    {
        public string DocCode { get; set; }
        public string DocNameT { get; set; }
        public string DocNameE { get; set; }
    }

    //class AddPayor
    //{
    //    public string payor_code { get; set; }
    //    public string payor_name { get; set; }
    //}
}