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
    public partial class newWaitingListUC : UserControl
    {
        public newWaitingListUC()
        {
            InitializeComponent();
            DGV_Waiting.AutoGenerateColumns = false;
        }

        private string mhs_code = null;
        private int? _mhs_id = null;
        public int? mhs_id
        { 
            get { return _mhs_id; }
            set
            {
                if (_mhs_id != value)
                {
                    if (value == null)
                    {
                        mhs_code = null;
                    }
                    else
                    {
                        try
                        {
                            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                            {
                                mst_hpc_site hpc_site = cdc.mst_hpc_sites
                                                           .Where(x => x.mhs_id == value)
                                                           .FirstOrDefault();
                                if (hpc_site == null)
                                {
                                    mhs_code = null;
                                    _mhs_id = null;
                                }
                                else
                                {
                                    mst_room_hdr room_hdr = hpc_site.mst_room_hdrs.Where(x => x.mrm_code == mrm_code).FirstOrDefault();
                                    if (room_hdr == null)
                                    {
                                        mrm_id = null;
                                    }
                                    else
                                    {
                                        mrm_id = room_hdr.mrm_id;
                                    }
                                    mhs_code = hpc_site.mhs_code;
                                    _mhs_id = value;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError(this.Name, "mhs_id", ex, false);
                        }
                    }
                }
            }
        }
        public string mrm_code { get; set; }
        private int? mrm_id { get; set; }

        public string condition { get; set; }

        public void LoadData()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    if (mrm_code == "RG")
                    {
                        DateTime dateNow = Program.GetServerDateTime();
                        List<sp_get_waiting_room_hdrResult> result = cdc.tmp_getptarriveds
                                                                        .Where(x => (x.flag_success == null || x.flag_success == 'N') && 
                                                                                     x.ctloc_code == mhs_code && 
                                                                                     x.paadm_admdate.Value.Date == dateNow.Date)
                                                                        .OrderBy(x => x.appt_arrivaltime)
                                                                        .Select(x => new sp_get_waiting_room_hdrResult
                                                                        {
                                                                            tpt_hn_no = x.papmi_no,
                                                                            tpt_othername = x.ttl_desc + x.papmi_name + " " + x.papmi_name2
                                                                        }).ToList();
                        int inx = 1;
                        result.ForEach(x => x.priority = inx++);
                        DGV_Waiting.Columns["colBtnCancelQueue"].Visible = false;
                        DGV_Waiting.Columns["colSendToCheckB"].Visible = false;
                        DGV_Waiting.DataSource = result;
                    }
                    else
                    {
                        mst_room_hdr room_hdr = cdc.mst_room_hdrs
                                                   .Where(x => x.mhs_id == mhs_id &&
                                                               x.mrm_code == mrm_code)
                                                   .FirstOrDefault();
                        if (room_hdr != null)
                        {
                            List<sp_get_waiting_room_hdrResult> result = cdc.sp_get_waiting_room_hdr(room_hdr.mrm_id).ToList();
                            if (result.Count() == 0)
                            {
                                DGV_Waiting.DataSource = new List<sp_get_waiting_room_hdrResult>();
                            }
                            else
                            {
                                if (mrm_code == "BM" || mrm_code == "SC" || mrm_code == "PH")
                                {
                                    DGV_Waiting.Columns["colBtnCancelQueue"].Visible = false;
                                    DGV_Waiting.Columns["colSendToCheckB"].Visible = false;
                                    DGV_Waiting.DataSource = result;
                                }
                                else if (mrm_code == "EM")
                                {
                                    int? eye_nurse_mvt_id = cdc.mst_events.Where(x => x.mvt_code == "EN").Select(x => x.mvt_id).FirstOrDefault();
                                    result = result.OrderBy(x => eye_nurse_mvt_id == x.mvt_id ? 0 : 1)
                                             .Select((x, inx) => new sp_get_waiting_room_hdrResult
                                             {
                                                 priority = inx + 1,
                                                 tpr_id = x.tpr_id,
                                                 tps_id = x.tps_id,
                                                 mvt_id = x.mvt_id,
                                                 tpr_queue_no = x.tpr_queue_no,
                                                 tpt_hn_no = x.tpt_hn_no,
                                                 tpt_othername = x.tpt_othername,
                                                 holded = x.holded,
                                                 reserved = x.reserved
                                             }).ToList();
                                    DGV_Waiting.DataSource = result;
                                }
                                else
                                {
                                    DGV_Waiting.DataSource = result;
                                }
                            }
                            lbtitle1.Text = string.Format(room_hdr.mrm_ename + "(Total {0})", DGV_Waiting.Rows.Count.ToString());
                        }
                        else
                        {
                            lbtitle1.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadData", ex, false);
                DGV_Waiting.DataSource = new List<sp_get_waiting_room_hdrResult>();
            }
        }

        private void DGV_Waiting_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            sp_get_waiting_room_hdrResult data = (sp_get_waiting_room_hdrResult)dgv.Rows[e.RowIndex].DataBoundItem;
            switch (data.holded)
            {
                case true:
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
                    break;
            }
            if (mrm_code == "DC")
            {
                bool passCheckB = new Class.FunctionDataCls().checkPassedCheckPointB((int)data.tpr_id);
                bool isResult = new Class.FunctionDataCls().checkEventDoctorResult((int)data.mvt_id);
                if (!passCheckB || isResult)
                {
                    dgv.Rows[e.RowIndex].Cells["colBtnCancelQueue"] = new DataGridViewTextBoxCell();
                    dgv.Rows[e.RowIndex].Cells["colSendToCheckB"] = new DataGridViewTextBoxCell();
                }
            }
        }

        public delegate void CompleteAction(object sender, bool Canceled);
        public event CompleteAction OnCompleteAction;
        private void _OnCompleteAction(bool Canceled)
        {
            if (OnCompleteAction != null)
            {
                OnCompleteAction(this, Canceled);
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
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex].Name == "colBtnCancelQueue")
            {
                sp_get_waiting_room_hdrResult data = (sp_get_waiting_room_hdrResult)dgv.Rows[e.RowIndex].DataBoundItem;
                if (checkStatusCanSend((int)data.tps_id))
                {
                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    if (func.checkPassedCheckPointB((int)data.tpr_id))
                    {
                        frmCancelQueue frmCancelQueue = new frmCancelQueue((int)data.tpr_id, (int)data.mvt_id, mrm_id, mhs_id, (int)data.tps_id, false, frmCancelQueue.useform.onWaiting);

                        if (frmCancelQueue.DialogResult == System.Windows.Forms.DialogResult.OK)
                        {
                            string alert = func.getStringGotoNextRoom((int)data.tpr_id);
                            MessageBox.Show(alert);
                            _OnCompleteAction(false);
                            new Class.ReserveSkipCls().SendAndReserve((int)data.tpr_id);
                            return;
                        }
                        else if (frmCancelQueue.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        {
                            _OnCompleteAction(true);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะที่ดำเนินการ skip ได้", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _OnCompleteAction(true);
                }
            }
            else if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "colSendToCheckB")
            {
                sp_get_waiting_room_hdrResult data = (sp_get_waiting_room_hdrResult)dgv.Rows[e.RowIndex].DataBoundItem;
                if (checkStatusCanSend((int)data.tps_id))
                {
                    DialogResult result = MessageBox.Show("คุณต้องการส่ง คนไข้ไปยัง Check Point B หรือไม่?", "Alert.", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        Class.SendToCheckBCls sendB = new Class.SendToCheckBCls();

                        StatusTransaction complete = sendB.SendToCheckBOnWaiting((int)data.tpr_id, (int)mrm_id);
                        if (complete == StatusTransaction.True)
                        {
                            MessageBox.Show("Sent To Checkpoint B Complete.", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _OnCompleteAction(false);
                        }
                        else if (complete == StatusTransaction.NoProcess)
                        {
                            MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะที่จะส่งไปยัง checkpoint B ได้", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _OnCompleteAction(true);
                        }
                        else if (complete == StatusTransaction.Error)
                        {
                            MessageBox.Show("ระบบเกิดความผิดพลาดไม่สามารถส่งไปยัง checkpoin B ได้ กรุณาทำอีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _OnCompleteAction(true);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะที่จะส่งไปยัง checkpoint B ได้", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _OnCompleteAction(true);
                }
            }
        }
    }
}
