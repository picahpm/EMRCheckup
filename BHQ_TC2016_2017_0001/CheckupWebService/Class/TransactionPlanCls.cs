using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public partial class TransactionPlanCls
    {
        public void EndPlan(ref trn_patient_regi PatientRegis, List<int> list_mvt_id)
        {
            try
            {
                List<trn_patient_plan> PatientPlan = PatientRegis.trn_patient_plans
                                                                 .Where(x => list_mvt_id.Contains(x.mvt_id)).ToList();
                foreach (trn_patient_plan plan in PatientPlan)
                {
                    if (plan.tpl_status == 'N')
                    {
                        plan.tpl_status = 'P';
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("TransactionPlanCls", "endPlan", ex.Message);
            }
        }
        public void SkipPlan(ref trn_patient_regi PatientRegis, List<int> list_mvt_id)
        {
            try
            {
                List<trn_patient_plan> PatientPlan = PatientRegis.trn_patient_plans
                                                                 .Where(x => list_mvt_id.Contains(x.mvt_id)).ToList();
                foreach (trn_patient_plan plan in PatientPlan)
                {
                    if (plan.tpl_status == 'N')
                    {
                        plan.tpl_skip = true;
                        plan.tpl_skip_seq = plan.tpl_skip_seq == null ? 1 : plan.tpl_skip_seq + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("TransactionPlanCls", "endPlan", ex.Message);
            }
        }
        public void CancelPlan(ref trn_patient_regi PatientRegis, List<int> list_mvt_id)
        {
            try
            {
                List<trn_patient_plan> PatientPlan = PatientRegis.trn_patient_plans
                                                                 .Where(x => list_mvt_id.Contains(x.mvt_id)).ToList();
                foreach (trn_patient_plan plan in PatientPlan)
                {
                    if (plan.tpl_status == 'N')
                    {
                        plan.tpl_status = 'C';
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("TransactionPlanCls", "endPlan", ex.Message);
            }
        }
    }
}