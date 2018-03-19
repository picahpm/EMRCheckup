using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
	public partial class UIWaitingList: UserControl
	{
        public delegate void CancelQueueHandler(object sender, completeArgs e);
        public event CancelQueueHandler cancelQueueHandler;
        private void _cancelQueueHandler(completeArgs e)
        {
            // Make sure someone is listening to event
            if (cancelQueueHandler == null) return;
            cancelQueueHandler(this, e);
        }

        public delegate void SendToCheckpointBHandler(object sender, completeArgs e);
        public event SendToCheckpointBHandler sendTocheckBHandler;
        private void _sendTocheckBHandler(completeArgs e)
        {
            // Make sure someone is listening to event
            if (sendTocheckBHandler == null) return;
            sendTocheckBHandler(this, e);
        }

		public UIWaitingList()
		{
			InitializeComponent();
		}

        private class gridProp
        {
            public int no { get; set; }
            public int tpr_id { get; set; }
            public int tps_id { get; set; }
            public int mhs_id { get; set; }
            public int? mrm_id { get; set; }
            public int? mvt_id { get; set; }
            public string patient_name { get; set; }
            public string hn_no { get; set; }
            public string callstatus { get; set; }
            public string vip_hpc { get; set; }
            public string hold_flag { get; set; }
            public int bmSeq { get; set; }
            public DateTime ordDate { get; set; }
            public bool? RTN_Nurse { get; set; }
            public string type_EyeNurse { get; set; }
            public bool? type_Lower { get; set; }
        }
        BindingSource gridBS = new BindingSource();

        public void ShowWaiting(int no, string txtSearch, int mhs_id, string roomCode)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                Class.FunctionDataCls func = new Class.FunctionDataCls();
                string mhs_code = cdc.mst_hpc_sites.Where(x => x.mhs_id == mhs_id).Select(x => x.mhs_code).FirstOrDefault();
                DateTime dateNow = Program.GetServerDateTime();
                DateTime ResetDate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 0, 0, 0);
                TimeSpan timenow = dateNow.TimeOfDay;

                if (roomCode == "RG")
                {
                    List<gridProp> result = cdc.vw_tmp_arrives.Where(x => (x.flag_success == null || x.flag_success == 'N') &&
                                                                          x.ctloc_code == Program.CurrentSite.mhs_code &&
                                                                          x.paadm_admdate.Value.Date == dateNow.Date)
                                                                         .OrderBy(x => x.appt_arrivaltime)
                                                                         .Select(x => new gridProp
                                                                         {
                                                                             hn_no = x.papmi_no,
                                                                             patient_name = x.ttl_desc + x.papmi_name + " " + x.papmi_name2
                                                                         }).ToList();
                    int number = 1;
                    result.ForEach(x => x.no = number++);
                    gridBS.DataSource = result;
                }
                else
                {
                    using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                    {
                        mst_room_hdr mstRoom = contxt.mst_room_hdrs
                                                     .Where(x => x.mhs_id == mhs_id &&
                                                                 x.mrm_status == 'A' &&
                                                                 x.mrm_code == roomCode)
                                                     .FirstOrDefault();

                        if (mstRoom != null)
                        {
                            List<sp_get_waiting_room_hdrResult> result = contxt.sp_get_waiting_room_hdr(mstRoom.mrm_id).ToList();
                            List<gridProp> data = result.Where(x => (txtSearch == null || txtSearch == string.Empty ? true
                                                                     : (x.tpt_hn_no.Contains(txtSearch) ||
                                                                       (x.tpt_hn_no.Replace("-", "").Contains(txtSearch)) ||
                                                                       (x.tpt_othername.ToUpper()).Contains(txtSearch.ToUpper()))))
                                                        .OrderBy(x => x.priority)
                                                        .Select((x, inx) => new gridProp
                                                        {
                                                            no = inx + 1,
                                                            tpr_id = (int)x.tpr_id,
                                                            tps_id = (int)x.tps_id,
                                                            mhs_id = mhs_id,
                                                            mvt_id = x.mvt_id,
                                                            mrm_id = mstRoom.mrm_id,
                                                            hn_no = x.tpt_hn_no,
                                                            patient_name = x.tpt_othername,
                                                            callstatus = x.holded == true ? "HD" : null
                                                        }).ToList();
                            gridBS.DataSource = data;
                            DGV_Waiting.AutoGenerateColumns = false;
                            DGV_Waiting.DataSource = gridBS;

                            lbtitle1.Text = string.Format(no.ToString() + ".Waiting " + mstRoom.mrm_ename + "(Total {0})", gridBS.Count.ToString());
                            if (roomCode == "RG" || roomCode == "BM" || roomCode == "SC" || roomCode == "PH")
                            {
                                DGV_Waiting.Columns["colBtnCancelQueue"].Visible = false;
                                DGV_Waiting.Columns["colSendToCheckB"].Visible = false;
                            }
                            else if (roomCode == "DC")
                            {
                                foreach (DataGridViewRow row in DGV_Waiting.Rows)
                                {
                                    gridProp gp = (gridProp)gridBS[row.Index];
                                    bool checkDoctorResult = (cdc.mst_events.Where(x => x.mvt_id == gp.mvt_id).Select(x => x.mvt_code).FirstOrDefault() == "DC");
                                    if (!func.checkPassedCheckPointB(gp.tpr_id) || checkDoctorResult)
                                    {
                                        row.Cells["colBtnCancelQueue"] = new DataGridViewTextBoxCell();
                                        row.Cells["colSendToCheckB"] = new DataGridViewTextBoxCell();
                                    }
                                }
                            }
                        }
                    }
                    //    List<gridProp> result = cdc.trn_patient_queues.Where(x => x.tps_status == "NS" &&
                    //                                                              (mhs_id == 0
                    //                                                               ? true
                    //                                                               : x.mst_room_hdr.mhs_id == mhs_id) &&
                    //                                                               x.mst_room_hdr.mrm_code == roomCode &&
                    //                                                               x.tps_create_date.Value.Date == dateNow.Date && 
                    //                                                               (txtSearch == null || txtSearch == string.Empty
                    //                                                               ? true
                    //                                                               : (x.trn_patient_regi.trn_patient.tpt_hn_no.Contains(txtSearch) ||
                    //                                                                 (x.trn_patient_regi.trn_patient.tpt_hn_no.Replace("-", "").Contains(txtSearch)) ||
                    //                                                                 (x.trn_patient_regi.trn_patient.tpt_othername.ToUpper()).Contains(txtSearch.ToUpper()))))
                    //                                                  .GroupBy(x => x.tpr_id)
                    //                                                  .Select(x => x.OrderByDescending(y => y.tps_update_date).FirstOrDefault())
                    //                                                  .Select(x => new gridProp
                    //                                                   {
                    //                                                       tpr_id = x.tpr_id,
                    //                                                       tps_id = x.tps_id,
                    //                                                       mhs_id = x.mst_room_hdr.mhs_id,
                    //                                                       mvt_id = x.mvt_id,
                    //                                                       mrm_id = x.mrm_id,
                    //                                                       hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                    //                                                       patient_name = x.trn_patient_regi.trn_patient.tpt_othername,
                    //                                                       callstatus = x.tps_call_status == "HD" ? "HD" : null,

                    //                                                       // Add Data for Order BY
                    //                                                       vip_hpc = (x.trn_patient_regi.trn_patient.tpt_vip_hpc == true) ? "Y" : "N",
                    //                                                       hold_flag = (x.tps_call_status == "HD" && (timenow.Subtract(x.tps_hold_date.Value.TimeOfDay)).TotalMinutes >= 0) ? "Y" : "N",
                    //                                                       bmSeq = Convert.ToInt32((x.tps_bm_seq != null) ? x.tps_bm_seq : 99),
                    //                                                       ordDate = Convert.ToDateTime((x.tps_call_status == "HD") ? x.tps_hold_date : x.tps_create_date),
                    //                                                       RTN_Nurse = x.trn_patient_regi.tpr_return_screening,
                    //                                                       type_EyeNurse = ((from t in cdc.mst_events where t.mvt_id == x.mvt_id select t.mvt_code).FirstOrDefault() == "EN") ? "Y" : "N",
                    //                                                       type_Lower = x.trn_patient_regi.tpr_miss_lower

                    //                                                   }).ToList();

                    //    // Add Order each Station
                    //    if (mhs_code == "01HPC2" || mhs_code == "01HPC3")
                    //    {
                    //        if (roomCode == "SC")
                    //        {
                    //            result = result.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.RTN_Nurse).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
                    //        }
                    //        else if (roomCode == "EM")
                    //        {
                    //            result = result.OrderByDescending(x => x.type_EyeNurse).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
                    //        }
                    //        else if (roomCode == "US")
                    //        {
                    //            result = result.OrderByDescending(x => x.type_Lower).ThenByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
                    //        }
                    //        else
                    //        {
                    //            result = result.OrderByDescending(x => x.vip_hpc).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (roomCode == "EM")
                    //        {
                    //            result = result.OrderByDescending(x => x.type_EyeNurse).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
                    //        }
                    //        else if (roomCode == "US")
                    //        {
                    //            result = result.OrderByDescending(x => x.type_Lower).ThenByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
                    //        }
                    //        else
                    //        {
                    //            result = result.OrderByDescending(x => x.hold_flag).ThenBy(x => x.bmSeq).ThenBy(x => x.ordDate).ToList();
                    //        }
                    //    }

                    //    int number = 1;
                    //    result.ForEach(x => x.no = number++);
                    //    gridBS.DataSource = result;


                    //}
                    //DGV_Waiting.AutoGenerateColumns = false;
                    //DGV_Waiting.DataSource = gridBS;
                    //mst_room_hdr mrm = cdc.mst_room_hdrs.Where(x => x.mrm_code == roomCode && x.mhs_id == mhs_id).FirstOrDefault();
                    //if (mrm != null)
                    //{
                    //    lbtitle1.Text = string.Format(no.ToString() + ".Waiting " + mrm.mrm_ename + "(Total {0})", gridBS.Count.ToString());
                    //    if (roomCode == "RG" || roomCode == "BM" || roomCode == "SC" || roomCode == "PH")
                    //    {
                    //        DGV_Waiting.Columns["colBtnCancelQueue"].Visible = false;
                    //        DGV_Waiting.Columns["colSendToCheckB"].Visible = false;
                    //    }
                    //    else if (roomCode == "DC")
                    //    {
                    //        foreach (DataGridViewRow row in DGV_Waiting.Rows)
                    //        {
                    //            gridProp gp = (gridProp)gridBS[row.Index];
                    //            bool checkDoctorResult = (cdc.mst_events.Where(x => x.mvt_id == gp.mvt_id).Select(x => x.mvt_code).FirstOrDefault() == "DC");
                    //            if (!func.checkPassedCheckPointB(gp.tpr_id) || checkDoctorResult)
                    //            {
                    //                row.Cells["colBtnCancelQueue"] = new DataGridViewTextBoxCell();
                    //                row.Cells["colSendToCheckB"] = new DataGridViewTextBoxCell();
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    lbtitle1.Text = string.Empty;
                    //}
                }
            }
        }
        
        private void UIWaitingList_Load(object sender, EventArgs e)
        {
        }

        #region Function
        public void ShowWaiting(int no,string coderoom,string nroom,List<WaitQueue> objdata) 
        {
            /*
                select papmi_no, paadm_admdate, ttl_desc+papmi_name+' '+papmi_name2 name
                from tmp_getptarrived
                where paadm_admdate = CONVERT(date,GETDATE(),103)
                and (flag_success is null or flag_success = 'N')
                and ctloc_code = '01CHK'
             
            if (coderoom == "RG")
            {
                using (DBCheckup.InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    var datenow = Program.GetServerDateTime();
                    var objdataRG = (from t1 in dbc.vw_tmp_arrives
                                 where (t1.flag_success == null || t1.flag_success == 'N')
                                 && t1.ctloc_code == Program.CurrentSite.mhs_code
                                 && t1.paadm_admdate.Value.Date == datenow.Date
                                 orderby t1.appt_arrivaltime
                                 select new WaitQueue
                                 {
                                     Callstatus="",
                                     RoomName=nroom,
                                     msh_id = Program.CurrentSite.mhs_id,
                                     coderoom = "RG",
                                     QueueNo = "",
                                     HN = t1.papmi_no,
                                     Name = t1.ttl_desc + t1.papmi_name + " " + t1.papmi_name2
                                 }).ToList();
                    var data = objdataRG.Where(x => x.coderoom == coderoom).Select((d, index) => new waiting18
                    {
                        no = index + 1,
                        HN = d.HN,
                        Name = d.Name,
                        Callstatus = d.Callstatus
                    });
                    DGV_Waiting.DataSource = new SortableBindingList<waiting18>(data.ToList());
                    DGV_Waiting.Columns["Colcallstatus"].Visible = false;
                    lbtitle1.Text = string.Format(no + ".Waiting " + nroom + "(Total {0})", data.Count().ToString());
                }

            }
            else
            {*/
            
            DGV_Waiting.Columns["colBtnCancelQueue"].Visible = false;
                var data = objdata.Where(x => x.coderoom == coderoom).Select((d, index) => new waiting18
                {
                    no = index + 1,
                    HN = d.HN,
                    Name = d.Name,
                    Callstatus = d.Callstatus
                });
                DGV_Waiting.DataSource = new SortableBindingList<waiting18>(data.ToList());
                DGV_Waiting.Columns["Colcallstatus"].Visible = false;
                lbtitle1.Text = string.Format(no + ".Waiting " + nroom + "(Total {0})", data.Count().ToString());
            //}
        }
        #endregion

        private void DGV_Waiting_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < DGV_Waiting.Rows.Count; i++)
            {
                if (DGV_Waiting.Rows[i].Cells["Colcallstatus"].Value!=null && DGV_Waiting.Rows[i].Cells["Colcallstatus"].Value.ToString() == "HD")
                {
                    DGV_Waiting.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    DGV_Waiting.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
            }
        }

        private bool checkStatusCanSend(int tps_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue tps = cdc.trn_patient_queues.Where(x => x.tps_id == tps_id).FirstOrDefault();
                    if (tps != null && tps.tps_status == "NS" && tps.tps_ns_status == "QL")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("frmWaiting", "checkStatusCanSend", ex, false);
                return false;
            }
        }
        private void DGV_Waiting_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "colBtnCancelQueue")
            {
                gridProp gp = (gridProp)gridBS.Current;
                if (checkStatusCanSend(gp.tps_id))
                {
                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    if (func.checkPassedCheckPointB(gp.tpr_id))
                    {
                        frmCancelQueue frmCancelQueue = new frmCancelQueue(gp.tpr_id, gp.mvt_id, gp.mrm_id, gp.mhs_id, gp.tps_id, false, frmCancelQueue.useform.onWaiting);

                        if (frmCancelQueue.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            string alert = func.getStringGotoNextRoom(gp.tpr_id);
                            MessageBox.Show(alert);
                            _cancelQueueHandler(null);
                            new Class.ReserveSkipCls().SendAndReserve(gp.tpr_id);
                            return;
                        }
                        else if (frmCancelQueue.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะที่ดำเนินการ skip ได้", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _cancelQueueHandler(null);
                }
            }
            else if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "colSendToCheckB")
            {
                gridProp gp = (gridProp)gridBS.Current;
                if (checkStatusCanSend(gp.tps_id))
                {
                    DialogResult result = MessageBox.Show("คุณต้องการส่ง คนไข้ไปยัง Check Point B หรือไม่?", "Alert.", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        Class.SendToCheckBCls sendB = new Class.SendToCheckBCls();

                        StatusTransaction complete = sendB.SendToCheckBOnWaiting(gp.tpr_id, (int)gp.mrm_id);
                        if (complete == StatusTransaction.True)
                        {
                            try
                            {
                                new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendCheckB,
                                                            gp.tpr_id,
                                                            gp.tps_id,
                                                            (Program.CurrentSite == null ? 0 : Program.CurrentSite.mhs_id),
                                                            "WaitingList",
                                                            (Program.CurrentUser == null ? "" : Program.CurrentUser.mut_username));
                            }
                            catch (Exception ex)
                            {
                                Program.MessageError(this.Name, "logPatientFlowCls", ex, false);
                            }
                            MessageBox.Show("Sent To Checkpoint B Complete.", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _sendTocheckBHandler(null);
                        }
                        else if (complete == StatusTransaction.NoProcess)
                        {
                            MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะที่จะส่งไปยัง checkpoint B ได้", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _sendTocheckBHandler(null);
                        }
                        else if (complete == StatusTransaction.Error)
                        {
                            MessageBox.Show("ระบบเกิดความผิดพลาดไม่สามารถส่งไปยัง checkpoin B ได้ กรุณาทำอีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _sendTocheckBHandler(null);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะที่จะส่งไปยัง checkpoint B ได้", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _cancelQueueHandler(null);
                }
            }
        }
    }
    public class waiting18
    {
        public int no { get; set; }
        public string HN { get; set; }
        public string Name { get; set; }
        public string Callstatus { get; set; }
    }


    public class completeArgs : EventArgs
    {
        public object valueData { get; private set; }
        public completeArgs(object _valueData)
        {
            valueData = _valueData;
        }
    }
}
