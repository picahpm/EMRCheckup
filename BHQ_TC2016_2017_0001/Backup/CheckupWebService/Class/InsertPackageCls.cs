using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class InsertPackageCls
    {
        public bool Insert(int tpr_id, List<APITrakcare.PatientOrderSet> OrderSets, string user, DateTime dateNow)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    foreach (var set in OrderSets)
                    {
                        var pRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        int? tos_id = null;
                        if (set.orderset_rowid != null)
                        {
                            var pOrderset = pRegis.trn_patient_order_sets.Where(x => x.tos_item_row_id == set.orderset_rowid).FirstOrDefault();
                            if (pOrderset == null)
                            {
                                pOrderset = new trn_patient_order_set
                                {
                                    tos_item_row_id = set.orderset_rowid,
                                    tos_create_by = user,
                                    tos_create_date = dateNow
                                };
                                pRegis.trn_patient_order_sets.Add(pOrderset);
                            }
                            pOrderset.tos_od_set_code = set.orderset_code;
                            pOrderset.tos_od_set_name = set.orderset_name;
                            pOrderset.tos_status = set.orderitems.All(x => x.orderitem_status == "D") ? false : true;
                            pOrderset.tos_update_by = user;
                            pOrderset.tos_update_date = dateNow;
                            cdc.SubmitChanges();
                            tos_id = pOrderset.tos_id;
                        }

                        foreach (var item in set.orderitems)
                        {
                            var pOrderitem = pRegis.trn_patient_order_items.Where(x => x.toi_item_row_id == item.rowid).FirstOrDefault();
                            if (pOrderitem == null)
                            {
                                pOrderitem = new trn_patient_order_item
                                {
                                    toi_key = item.rowid,
                                    toi_create_by = user,
                                    toi_create_date = dateNow
                                };
                                pRegis.trn_patient_order_items.Add(pOrderitem);
                            }
                            pOrderitem.tos_id = tos_id;
                            pOrderitem.toi_set_row_id = set.orderset_rowid;
                            pOrderitem.toi_item_row_id = item.orderitem_rowid;
                            pOrderitem.toi_od_item_code = item.orderitem_code;
                            pOrderitem.toi_od_item_name = item.orderitem_name;
                            //pOrderitem.toi_patho = item.patho;
                            //pOrderitem.toi_pac_sheet = item.use_pacsheet;
                            pOrderitem.toi_use_pac = item.use_pacsheet;
                            pOrderitem.toi_status = item.orderitem_status == "V" ? true : false;
                            pOrderitem.toi_type = string.IsNullOrEmpty(set.orderset_rowid) ? 'I' : 'S';
                            pOrderitem.toi_update_by = user;
                            pOrderitem.toi_update_date = dateNow;
                            pOrderitem.toi_trakcare_status = item.orderitem_status;
                        }
                        cdc.SubmitChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("InsertPackageCls", "Insert", ex.Message);
                return false;
            }
        }
    }
}