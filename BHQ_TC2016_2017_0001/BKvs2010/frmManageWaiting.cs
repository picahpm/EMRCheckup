using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmManageWaiting : Form
    {
        private int _tpr_id;
        private int _tps_id;
        private string _queue_no;

        public frmManageWaiting()
        {
            InitializeComponent();
        }

        private string messageAlert = "";
        private StatusTransaction transaction = StatusTransaction.NoProcess;

        public StatusTransaction isCallQueue(int tps_id, ref string message)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue tps = cdc.trn_patient_queues.Where(x => x.tps_id == tps_id && 
                                                                              x.tps_status == "NS" && 
                                                                              x.tps_ns_status == "QL").FirstOrDefault();
                    if (tps != null)
                    {
                        _tpr_id = tps.tpr_id;
                        _tps_id = tps.tps_id;

                        uiUserprofile1.LoadData(_tpr_id);

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == _tpr_id).FirstOrDefault();
                        _queue_no = tpr.tpr_queue_no;

                        if (Program.CurrentRegis == null)
                        {
                            btnCallQueue.Visible = true;
                        }
                        else
                        {
                            btnCallQueue.Visible = false;
                            lbAlertMsg.Text = "ไม่สามารถ Call Queue ได้ เนื่องจากมีผู้ใช้บริการ";
                        }
                        frmbg.Close();
                        this.ShowDialog();
                        tps_id = tps.tps_id;
                        message = messageAlert;
                        return transaction;
                    }
                    else
                    {
                        frmbg.Close();
                        message = _queue_no + " ไม่สามารถดำเนินการต่อได้ กรุณาตรวจสอบอีกครั้ง";
                        return StatusTransaction.NoProcess;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("frmManageWaiting", "isCallQueue", ex, false);
                return StatusTransaction.Error;
            }
        }


        BindingSource gridStationBindingSource = new BindingSource();
        class gridStationObj
        {
            public string mrm_ename { get; set; }
            public string mze_ename { get; set; }
            public string mhs_ename { get; set; }
            public int? waiting_person { get; set; }
            public int? waiting_time { get; set; }
            public int mhs_id { get; set; }
            public int? mrm_id { get; set; }
            public int mvt_id { get; set; }
            public int? vip { get; set; }
        }

        private bool setGridStationBindingSource(int? tpr_id, int? mrm_id, int? mhs_id)
        {
            if (tpr_id != null && mrm_id != null)
            {
                try
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                        string mrm_code = mst.GetMstRoomHdr((int)mrm_id).mrm_code;
                        List<gridStationObj> result = cdc.vw_patient_rooms
                                                         .Where(x => x.tpr_id == tpr_id &&
                                                                    (x.mhs_id != null ? x.mhs_id == mhs_id : true) &&
                                                                    (x.mrm_code == "CB" ? true : x.mrm_code != mrm_code))
                                                         .Select(x => new gridStationObj
                                                         {
                                                             mhs_ename = x.mhs_ename,
                                                             mze_ename = x.mze_ename,
                                                             mrm_ename = x.mrm_ename,
                                                             waiting_person = x.waiting_person,
                                                             waiting_time = x.waiting_time,
                                                             mhs_id = x.mhs_id,
                                                             mrm_id = x.mrm_id,
                                                             mvt_id = x.mvt_id,
                                                             vip = x.patient_vip
                                                         }).ToList();
                        gridStationBindingSource.DataSource = result;
                        if (result.Count > 0)
                        {
                            return true;
                        }
                    }
                }
                catch
                {

                }
            }
            return false;
        }

        private void btnCallQueue_Click(object sender, EventArgs e)
        {
            StatusTransaction callQ = new Class.FunctionDataCls().callQueue(_tps_id);
            if (callQ == StatusTransaction.True)
            {
                transaction = StatusTransaction.True;
            }
            else if (callQ == StatusTransaction.False)
            {
                messageAlert = _queue_no + " ไม่สามารถดำเนินการต่อได้ กรุณาตรวจสอบอีกครั้ง";
            }
            else
            {
                transaction = callQ;
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
