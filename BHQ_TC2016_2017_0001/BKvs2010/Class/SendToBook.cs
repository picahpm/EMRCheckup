using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Data;
using System.Windows.Forms;
namespace BKvs2010.Class
{
    /// <summary>
    /// Send to Book 
    /// </summary>
    /// 

    class SendToBook
    {
        #region RunSendBook
        public static void SendBook(int tprid)
        {
            try
            {
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    DateTime datenow = Program.GetServerDateTime();
                    int mrmID = 0;
                    int mvtID = 0;
                    string roomname = String.Empty;

                    trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tprid).FirstOrDefault();

                    //Update trn_patient_queue
                    int tps_ID = (from t in dbc.trn_patient_queues
                                  join t2 in dbc.mst_room_hdrs on t.mrm_id equals t2.mrm_id
                                  where t.tpr_id == tprid && t2.mrm_code == "CC" && t.tps_status == "WK"
                                  select t.tps_id).FirstOrDefault();

                    var objQueue = (from t1 in dbc.trn_patient_queues
                                    where t1.tps_id == tps_ID
                                    select t1).FirstOrDefault();
                    if (objQueue != null)
                    {
                        objQueue.tps_status = "ED";
                        objQueue.mrd_id = Program.CurrentRoom.mrd_id;
                        objQueue.tps_send_by = Program.CurrentUser.mut_username;
                        objQueue.tps_end_date = datenow;
                        objQueue.tps_update_by = Program.CurrentUser.mut_username;
                        objQueue.tps_update_date = datenow;
                    }
                    // End.
                    mst_hpc_site mhs = mst.GetMstHpcSite(tpr.mhs_id);
                    mrmID = mst.GetMstRoomHdr("BK", mhs.mhs_code).mrm_id;
                    mvtID = mst.GetMstEvent("BK").mvt_id;
                    //mrmID = Program.Getmrm_id(dbc, "BK");
                    //mvtID = Program.Getmvt_id(dbc, "BK");
                    var getHnEn = (from t in dbc.trn_patient_regis join t2 in dbc.trn_patients on t.tpt_id equals t2.tpt_id where t.tpr_id == tprid select new { t, t2 }).ToList();
                    var objevent = (from t1 in dbc.mst_events where t1.mvt_id == mvtID select t1).ToList();

                    if (objevent.Count != 0)
                        roomname = objevent[0].mvt_ename;

                    var objqueueBK = (from t1 in dbc.trn_patient_queues
                                      where t1.trn_patient_regi.trn_patient.tpt_hn_no == getHnEn.Select(x => x.t2.tpt_hn_no).FirstOrDefault()
                                       && t1.trn_patient_regi.tpr_en_no == getHnEn.Select(x => x.t.tpr_en_no).FirstOrDefault()
                                       && t1.mrm_id == mrmID
                                       && t1.mvt_id == mvtID
                                      select t1).FirstOrDefault();

                    var objRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1).FirstOrDefault();
                    if (objRegis != null)
                    {
                        objRegis.tpr_status = "WB";
                        objRegis.tpr_pe_status = "RS";
                    }
                    if (objqueueBK == null && mrmID != 0)
                    {
                        trn_patient_queue newitem = new trn_patient_queue();
                        newitem.tpr_id = tprid;
                        newitem.mrm_id = mrmID;
                        newitem.mvt_id = mvtID;
                        newitem.mrd_id = null;
                        newitem.tps_end_date = null;
                        newitem.tps_start_date = null;
                        newitem.tps_status = "NS";
                        newitem.tps_ns_status = "QL";
                        newitem.tps_create_by = Program.CurrentUser.mut_username;
                        newitem.tps_create_date = datenow;
                        newitem.tps_update_by = Program.CurrentUser.mut_username;
                        newitem.tps_update_date = datenow;
                        dbc.trn_patient_queues.InsertOnSubmit(newitem);
                        MessageBox.Show("Send Completed. Send To" + roomname, "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (objqueueBK != null && mrmID != 0)
                    {
                        objqueueBK.mrd_id = null;
                        objqueueBK.tps_status = "NS";
                        objqueueBK.tps_ns_status = "QL";
                        objqueueBK.tps_create_date = datenow;
                        objqueueBK.tps_create_by = Program.CurrentUser.mut_username;
                        objqueueBK.tps_update_by = objqueueBK.tps_create_by;
                        objqueueBK.tps_update_date = datenow;
                        MessageBox.Show("Send Completed. Sent To" + roomname, "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (mvtID == mst.GetMstEvent("BK").mvt_id)
                    {
                        MessageBox.Show("Checkup Process Completed", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dbc.SubmitChanges();
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
