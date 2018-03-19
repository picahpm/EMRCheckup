using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckupWebService.ImagingClass
{
    public class GetDocumentImagingCls
    {
        public List<DocumentResult> CovertToResult(List<APITrakcare.PatientOrderSet> PatientOrders)
        {
            List<DocumentResult> results = new List<DocumentResult>();
            //string LinkPacSheet = Class.GetDBConfigCls.GetConfig("LinkPacSheet");
            //string PathPatho = Class.GetDBConfigCls.GetConfig("PathPatho");
            try
            {
                foreach (var set in PatientOrders)
                {
                    string hnReplace = set.hn.Replace("-", "");
                    foreach (var item in set.orderitems)
                    {
                        DocumentResult rs = new DocumentResult
                        {
                            hn = set.hn,
                            en = set.en,
                            mvt_id = item.mvt_id,
                            mvt_code = item.mvt_code,
                            Excuted = false
                        };
                        switch (item.mvt_code)
                        {
                            case "PT":
                                if (!string.IsNullOrEmpty(item.patho))
                                {
                                    if (System.IO.File.Exists(item.patho))
                                    {
                                        rs.Excuted = true;
                                        rs.Link = item.patho;
                                    }
                                }
                                results.Add(rs);
                                break;
                            case "XR":
                            case "UU":
                            case "UB":
                            case "UL":
                            case "UW":
                            case "UG":
                            case "DM":
                            case "BD":
                            case "CD":
                                if (!string.IsNullOrEmpty(item.pacsheet))
                                {
                                    rs.Excuted = true;
                                    rs.Link = item.pacsheet;
                                }
                                results.Add(rs);
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetDocumentResultCls", "CovertToResult", ex.Message);
            }
            return results;
        }
    }

    public class DocumentResult
    {
        public string hn { get; set; }
        public string en { get; set; }
        public int mvt_id { get; set; }
        public string mvt_code { get; set; }
        public string Link { get; set; }
        public bool Excuted { get; set; }
    }
}