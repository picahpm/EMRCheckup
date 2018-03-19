using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.Class
{
    class TransactionPlanCls
    {
        public StatusTransaction endPlan(ref trn_patient_regi tpr, List<int> list_mvt_id)
        {
            try
            {
                List<trn_patient_plan> tpl = tpr.trn_patient_plans
                                                .Where(x => list_mvt_id.Contains(x.mvt_id)).ToList();

                //List<trn_patient_plan> update_plan = tpr.trn_patient_plans.Where(x => tpl.Select(y => y.tpl_id).Contains(x.tpl_id)).ToList();
                //update_plan.ForEach(x => x.tpl_status = 'P');

                tpl.ForEach(x => x.tpl_status = 'P');
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionPlanCls", "endPlan", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction endPlan(ref trn_patient_regi tpr, string mrm_code)
        {
            try
            {
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_code);
                return endPlan(ref tpr, mrm.mrm_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionPlanCls", "endPlan", ex, false);
                return StatusTransaction.Error;
            }
        }
        public StatusTransaction endPlan(ref trn_patient_regi tpr, int mrm_id)
        {
            try
            {
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                mst_room_hdr mrm = mst.GetMstRoomHdr(mrm_id);
                List<int> list_mvt_id = new List<int>();
                if (mrm.mrm_code == "EM")
                {
                    if (Program.CurrentRoom.mrd_type == 'D')
                    {
                        list_mvt_id.Add(mst.GetMstEvent("EM").mvt_id);
                    }
                    else if (Program.CurrentRoom.mrd_type == 'N')
                    {
                        list_mvt_id.Add(mst.GetMstEvent("EN").mvt_id);
                    }
                }
                else if (mrm.mrm_code == "TE")
                {
                    if (Program.CurrentRoom.mrd_type == 'T')
                    {
                        list_mvt_id.Add(mst.GetMstEvent("TX").mvt_id);
                    }
                    else if (Program.CurrentRoom.mrd_type == 'D')
                    {
                        list_mvt_id.Add(mst.GetMstEvent("TE").mvt_id);
                    }
                }
                else
                {
                    list_mvt_id = mst.GetMstRoomEventByMrm(mrm_id).Select(x => x.mvt_id).ToList();
                }
                //List<trn_patient_plan> tpl = tpr.trn_patient_plans
                //                                .Where(x => list_mvt_id.Contains(x.mvt_id)).ToList();
                //tpl.ForEach(x => x.tpl_status = 'P');
                return endPlan(ref tpr, list_mvt_id);
            }
            catch (Exception ex)
            {
                Program.MessageError("TransactionPlanCls", "endPlan", ex, false);
                return StatusTransaction.Error;
            }
        }
    }
}
