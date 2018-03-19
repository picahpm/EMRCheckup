using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.APITrakcare
{
    public class GetPatientOrderCls
    {
        public List<PatientOrderSet> ByGetPTPackage(int enRowID)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                    {
                        var LinkPacSheet = Class.GetDBConfigCls.GetConfig("LinkPacSheet");
                        var PathPatho = Class.GetDBConfigCls.GetConfig("PathPatho");
                        var RawMateWS = ws.GetPTPackage(enRowID).AsEnumerable();
                        var MappingOrder = (from mate in RawMateWS
                                            join mst in cdc.mst_order_plans
                                            on mate.Field<string>("ARCIM_RowId") equals mst.mop_item_row_id
                                            where mst.mop_status == 'A'
                                            group new { mate, mst } 
                                            by new 
                                            {
                                                hn = mate.Field<string>("PAPMI_No"),
                                                en = mate.Field<string>("PAADM_ADMNo"),
                                                rowid = mate.Field<string>("ARCOS_RowId"),
                                                code = mate.Field<string>("ARCOS_Code"), 
                                                name = mate.Field<string>("ARCOS_Desc") 
                                            } into set
                                            select new PatientOrderSet
                                            {
                                                hn = set.Key.hn,
                                                en = set.Key.en,
                                                orderset_rowid = set.Key.rowid,
                                                orderset_code = set.Key.code,
                                                orderset_name = set.Key.name,
                                                orderset_status = set.All(x => x.mate.Field<string>("OSTAT_Code") == "D") ? false : true,
                                                orderitems = set.Select(x => new PatientOrderItem
                                                {
                                                    rowid = x.mate.Field<string>("OEORI_RowId"),
                                                    mop_id = x.mst.mop_id,
                                                    mvt_id = x.mst.mvt_id,
                                                    mvt_code = x.mst.mst_event.mvt_code,
                                                    orderitem_rowid = x.mate.Field<string>("ARCIM_RowId"),
                                                    orderitem_code = x.mate.Field<string>("ARCIM_Code"),
                                                    orderitem_name = x.mate.Field<string>("ARCIM_Desc"),
                                                    orderitem_status = x.mate.Field<string>("OSTAT_Code"),
                                                    use_pacsheet = x.mate.Field<string>("ARCIM_Text1") != null ? true : false,
                                                    pacsheet = x.mate.Field<string>("OEORI_AccessionNumber") != null
                                                               ? string.Format(LinkPacSheet, x.mate.Field<string>("OEORI_AccessionNumber").Replace("/", "-"), set.Key.hn.Replace("-", ""))
                                                               : null,
                                                    patho = x.mst.mst_event.mvt_code == "PT" && x.mate.Field<DateTime?>("ExecuteDate") != null && x.mate.Field<string>("OEORI_LabTestSetRow") != null
                                                            ? PathPatho + x.mate.Field<DateTime?>("ExecuteDate").Value.Date.ToString("yyyyMM") + @"\" + x.mate.Field<string>("OEORI_LabTestSetRow").Replace("||", "_") + ".doc"
                                                            : null,
                                                    excute_date = x.mate.Field<DateTime?>("ExecuteDate")
                                                }).ToList()
                                            }).ToList();
                        return MappingOrder;
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetPatientOrderCls", "ByGetPTPackage", ex.Message);
                throw ex;
            }
        }
    }

    public class PatientOrderSet
    {
        public string hn { get; set; }
        public string en { get; set; }
        public string orderset_rowid { get; set; }
        public string orderset_code { get; set; }
        public string orderset_name { get; set; }
        public bool orderset_status { get; set; }

        public List<PatientOrderItem> orderitems { get; set; }
    }
    public class PatientOrderItem
    {
        public string rowid { get; set; }
        public int mop_id { get; set; }
        public int mvt_id { get; set; }
        public string mvt_code { get; set; }
        public string orderitem_rowid { get; set; }
        public string orderitem_code { get; set; }
        public string orderitem_name { get; set; }
        public string orderitem_status { get; set; }
        public bool use_pacsheet { get; set; }
        public string pacsheet { get; set; }
        public string patho { get; set; }
        public DateTime? excute_date { get; set; }
    }
}