using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Data;
using System.Windows.Forms;

namespace BKvs2010.Class
{
    class SkipOrderSetEST
    {
        #region RunSkip
        public static void RunSkipEST(int siteid, ref trn_patient_regi tpr)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    DateTime datenow = Program.GetServerDateTime();
                    var gettxtcode = (from t1 in dbc.mst_config_hdrs
                                      join t2 in dbc.mst_config_dtls
                                      on t1.mfh_id equals t2.mfh_id
                                      where t1.mfh_code == "ECHO"
                                      && t1.mhs_id == siteid
                                      && t1.mfh_status == 'A'
                                      && datenow >= t1.mfh_effective_date.Value
                                      && (t1.mfh_expire_date != null ? (datenow <= t1.mfh_expire_date.Value) : true)
                                      select t2.mfd_text).ToList();
                    if (gettxtcode.Count() != 0)
                    {
                        List<trn_patient_order_set> tpos = tpr.trn_patient_order_sets.Where(x => gettxtcode.Contains(x.tos_od_set_code)).ToList();
                        if (tpos != null)
                        {
                            bool chkEst = tpos.Count > 0;
                            if (chkEst)
                            {
                                int get_mvtEcho = (from t1 in dbc.mst_events
                                                   where t1.mvt_code == "EC"
                                                   && t1.mvt_status == 'A'
                                                   && datenow >= t1.mvt_effective_date.Value
                                                   && (t1.mvt_expire_date != null ? (datenow <= t1.mvt_expire_date.Value) : true)
                                                   select t1.mvt_id).FirstOrDefault();

                                /*var get_mvtid = (from t1 in dbc.mst_events
                                                 join t2 in dbc.mst_room_events on t1.mvt_id equals t2.mvt_id
                                                 join t3 in dbc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                 where t3.mhs_id == siteid
                                                 //&& (t3.mrm_code == "ES" || t3.mrm_code == "DC")
                                                 && t3.mrm_code == "ES"
                                                 select t1.mvt_id).Distinct().ToList();*/

                                int get_mvtid = (from t1 in dbc.mst_events
                                                   where t1.mvt_code == "ES"
                                                   && t1.mvt_status == 'A'
                                                   && datenow >= t1.mvt_effective_date.Value
                                                   && (t1.mvt_expire_date != null ? (datenow <= t1.mvt_expire_date.Value) : true)
                                                   select t1.mvt_id).FirstOrDefault();

                                List<trn_patient_plan> tpp = tpr.trn_patient_plans.Where(x => x.tpl_status != 'P' && x.mvt_id == get_mvtid).ToList();
                                foreach (trn_patient_plan t in tpp)
                                {
                                    ////tpr.trn_patient_plans.Remove(t);
                                    //t.mvt_id = get_mvtEcho;
                                    //t.tpl_status = 'P';
                                    trn_patient_plan patient_plan = tpr.trn_patient_plans.Where(x => x.tpl_id == t.tpl_id).FirstOrDefault();
                                    patient_plan.mvt_id = get_mvtEcho;
                                    patient_plan.tpl_status = 'P';
                                }

                                var get_mvtid_dc = (from t1 in dbc.mst_events
                                                    join t2 in dbc.mst_room_events on t1.mvt_id equals t2.mvt_id
                                                    join t3 in dbc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                                    where t3.mhs_id == siteid
                                                    //&& (t3.mrm_code == "ES" || t3.mrm_code == "DC")
                                                    && t3.mrm_code == "DC"
                                                    select t1.mvt_id).Distinct().ToList();
                                List<trn_patient_plan> tpp_1 = tpr.trn_patient_plans.Where(x => x.tpl_status != 'P' && get_mvtid_dc.Contains(x.mvt_id)).ToList();
                                foreach (trn_patient_plan t in tpp_1)
                                {
                                    //tpr.trn_patient_plans.Remove(t);
                                    ////t.tpl_status = 'P';
                                    trn_patient_plan patient_plan = tpr.trn_patient_plans.Where(x => x.tpl_id == t.tpl_id).FirstOrDefault();
                                    tpr.trn_patient_plans.Remove(patient_plan);
                                }

                                //var delobj = (from t in dbc.trn_patient_plans
                                //              where t.tpr_id == tprid
                                //              && t.tpl_status != 'P'
                                //              && get_mvtid.Contains(t.mvt_id)
                                //              select t).ToList();
                                //if (delobj.Count() > 0)
                                //{
                                //    dbc.trn_patient_plans.DeleteAllOnSubmit(delobj);
                                //    dbc.SubmitChanges();
                                //}
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}