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
    public partial class frmCancelQueue : Form
    {
        public enum useform
        {
            onWaiting,
            onStation
        }
        private useform _useform;
        private int? _tpr_id;
        private int? _mhs_id;
        private int? _mrm_id;
        private int? _mvt_id;
        private int? _tps_id;

        public frmCancelQueue()
        {
            InitializeComponent();
        }
        public frmCancelQueue(int? tpr_id, int? mvt_id, bool ultrasound = false, bool? checkLower = false, string timeLower = "", useform Useform = useform.onStation)
        {
            InitializeComponent();
            _tpr_id = tpr_id;
            _mvt_id = mvt_id;
            _mrm_id = Program.CurrentRoom.mrm_id;
            _mhs_id = Program.CurrentSite.mhs_id;
            _tps_id = Program.CurrentPatient_queue.tps_id;
            _useform = Useform;

            //ultrasound
            _ultrasound = ultrasound;
            _checkLower = checkLower;
            _timeLower = timeLower;
            //

            setDropDownSite();
            cmbSite.SelectedValue = _mhs_id;
            int? mhs_id = (int?)cmbSite.SelectedValue;
            if (setGridStationBindingSource(_tpr_id, _mrm_id, mhs_id))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                gridStation.DataSource = gridStationBindingSource;

                if (Program.CurrentSite.mhs_extra_pe_type != true)
                {
                    gridStation.Columns["colVIP"].Visible = false;
                    gridStation.Width = gridStation.Width - gridStation.Columns["colVIP"].Width;
                    this.Width = this.Width - gridStation.Columns["colVIP"].Width;
                }
                this.ShowDialog();
            }
            else
            {
                rdSkip.Enabled = false;
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                gridStation.DataSource = gridStationBindingSource;

                if (Program.CurrentSite.mhs_extra_pe_type != true)
                {
                    gridStation.Columns["colVIP"].Visible = false;
                    gridStation.Width = gridStation.Width - gridStation.Columns["colVIP"].Width;
                    this.Width = this.Width - gridStation.Columns["colVIP"].Width;
                }
                this.ShowDialog();
            }
        }
        public frmCancelQueue(int? tpr_id, int? mvt_id, int? mrm_id, int? mhs_id, int tps_id, bool visibleButtonSendAuto = true, useform Useform = useform.onStation)
        {
            InitializeComponent();
            btnSendAuto.Visible = visibleButtonSendAuto;
            _tpr_id = tpr_id;
            _mvt_id = mvt_id;
            _mrm_id = mrm_id;
            _mhs_id = mhs_id;
            _tps_id = tps_id;
            _useform = Useform;
            setDropDownSite();
            cmbSite.SelectedValue = _mhs_id;
            int? tmp_mhs_id = (int?)cmbSite.SelectedValue;
            if (setGridStationBindingSource(_tpr_id, _mrm_id, tmp_mhs_id))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                gridStation.DataSource = gridStationBindingSource;
                this.ShowDialog();
            }
            else
            {
                rdSkip.Enabled = false;
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                gridStation.DataSource = gridStationBindingSource;
                this.ShowDialog();
            }
        }

        private void setDropDownSite()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var result = cdc.mst_hpc_sites.Where(x => x.mhs_status == 'A' && 
                                                          x.mhs_type == 'P' && 
                                                          x.mhs_room_chkup == true)
                                              .OrderBy(x => x.mhs_id).Select(x => new 
                {
                    val = x.mhs_id,
                    dis = x.mhs_ename,
                }).ToList();
                cmbSite.ValueMember = "val";
                cmbSite.DisplayMember = "dis";
                cmbSite.DataSource = result;
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
                                                         //.GroupBy(x => x.mrm_id)
                                                         //.Select(x => x.OrderBy(y => y.mvt_id).FirstOrDefault())
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

        private void gridStation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gridStation.Columns[e.ColumnIndex].Name == "colBtn")
                {
                    StatusTransaction updateLower = UpdatedLower();
                    if (updateLower == StatusTransaction.True)
                    {
                        gridStationObj gso = (gridStationObj)gridStationBindingSource.Current;
                        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                        mst_event mvt = mst.GetMstEvent(gso.mvt_id);
                        if (mvt.mvt_code == "EM")
                        {
                            List<gridStationObj> obj = (List<gridStationObj>)gridStationBindingSource.DataSource;
                            mst_event eyeNurseEvent = mst.GetMstEvent("EN");
                            var incMvtCode = obj.Select(x => mst.GetMstEvent(x.mvt_id).mvt_code).ToList();
                            if (incMvtCode.Contains(eyeNurseEvent.mvt_code))
                            {
                                MessageBox.Show("Can not send to " + mvt.mvt_ename + " before " + eyeNurseEvent.mvt_ename + "." + Environment.NewLine +
                                                "Please select another room.", "Send Queue Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        bool success = false;
                        gridStationObj result = (gridStationObj)gridStationBindingSource.Current;
                        if (rdSkip.Checked)
                        {
                            Class.ClsSkipOnStation skip = new Class.ClsSkipOnStation();
                            if (_useform == useform.onStation)
                            {
                                //success = skip.skipOnStationSendManaul((int)result.mrm_id, result.mvt_id);
                                StatusTransaction skipStation = skip.skipOnStationSendManaul((int)_tpr_id, (int)_tps_id, (int)result.mrm_id, result.mvt_id);
                                if (skipStation == StatusTransaction.True)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SkipManual,
                                                                (int)_tpr_id,
                                                                (int)_tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username);

                                    success = true;
                                    //if (new EmrClass.GetDataMasterCls().GetMstRoomHdr((int)result.mrm_id).mrm_code == "DC")
                                    //{
                                        //new Class.FunctionDataCls().stampPEDoctor((int)_tpr_id);
                                    //}
                                }
                                else if (skipStation == StatusTransaction.NoProcess)
                                {
                                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะดำเนินการ skip ต่อได้", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (skipStation == StatusTransaction.Error)
                                {
                                    MessageBox.Show("กรุณา skip อีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (_useform == useform.onWaiting)
                            {
                                //success = skip.skipOnStationSendManaul((int)result.mrm_id, result.mvt_id);
                                StatusTransaction skipStation = skip.skipOnWaitingSendManaul((int)_tpr_id, (int)_tps_id, (int)result.mrm_id, result.mvt_id);
                                if (skipStation == StatusTransaction.True)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SkipOnWaitingManual,
                                                                (int)_tpr_id,
                                                                (int)_tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                "WaitingList",
                                                                Program.CurrentUser.mut_username);

                                    success = true;
                                    //if (new EmrClass.GetDataMasterCls().GetMstRoomHdr((int)result.mrm_id).mrm_code == "DC")
                                    //{
                                        //new Class.FunctionDataCls().stampPEDoctor((int)_tpr_id);
                                    //}
                                }
                                else if (skipStation == StatusTransaction.NoProcess)
                                {
                                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะดำเนินการ skip ต่อได้", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (skipStation == StatusTransaction.Error)
                                {
                                    MessageBox.Show("กรุณา skip อีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else if (rdPending.Checked)
                        {
                            Class.ClsPendingOnStation pend = new Class.ClsPendingOnStation();
                            if (_useform == useform.onStation)
                            {
                                //success = pend.pendingOnStationSendManaul((int)result.mrm_id, result.mvt_id);
                                StatusTransaction pendingStation = pend.pendingOnStationSendManaul((int)_tpr_id, (int)_mvt_id, (int)_mrm_id, (int)_mhs_id, (int)result.mrm_id, result.mvt_id, (int)_tps_id);
                                if (pendingStation == StatusTransaction.True)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.PendingManual,
                                                                (int)_tpr_id,
                                                                (int)_tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username);

                                    success = true;
                                    //if (new EmrClass.GetDataMasterCls().GetMstRoomHdr((int)result.mrm_id).mrm_code == "DC")
                                    //{
                                        //new Class.FunctionDataCls().stampPEDoctor((int)_tpr_id);
                                    //}
                                }
                                else if (pendingStation == StatusTransaction.NoProcess)
                                {
                                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะดำเนินการ pending ต่อได้", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (pendingStation == StatusTransaction.Error)
                                {
                                    MessageBox.Show("กรุณา pending อีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (_useform == useform.onWaiting)
                            {
                                //success = pend.pendingOnStationSendManaul((int)result.mrm_id, result.mvt_id);
                                StatusTransaction pendingStation = pend.pendingOnWaitingSendManaul((int)_tpr_id, (int)_mvt_id, (int)_mrm_id, (int)_mhs_id, (int)result.mrm_id, result.mvt_id, (int)_tps_id);
                                if (pendingStation == StatusTransaction.True)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.PendingOnWaitingManual,
                                                                (int)_tpr_id,
                                                                (int)_tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                "WaitingList",
                                                                Program.CurrentUser.mut_username);

                                    success = true;
                                    //if (new EmrClass.GetDataMasterCls().GetMstRoomHdr((int)result.mrm_id).mrm_code == "DC")
                                    //{
                                        //new Class.FunctionDataCls().stampPEDoctor((int)_tpr_id);
                                    //}
                                }
                                else if (pendingStation == StatusTransaction.NoProcess)
                                {
                                    MessageBox.Show("คนไข้ไม่ได้อยู่ในสถานะดำเนินการ pending ต่อได้", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (pendingStation == StatusTransaction.Error)
                                {
                                    MessageBox.Show("กรุณากด pending อีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        if (success)
                        {
                            Program.CurrentRegis = null;
                            Program.CurrentPatient_queue = null;
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                    else if (updateLower == StatusTransaction.Error)
                    {
                        MessageBox.Show("กรุณากด send manual อีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("กรุณากด send manual อีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Program.MessageError("frmCancelQueue", "gridStation_CellContentClick", ex, false);
            }
            finally
            {
                this.Close();
            }
        }

        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            try
            {
                StatusTransaction updateLower = UpdatedLower();
                if (updateLower == StatusTransaction.True)
                {
                    StatusTransaction success = StatusTransaction.NoProcess;
                    if (rdSkip.Checked)
                    {
                        Class.ClsSkipOnStation skip = new Class.ClsSkipOnStation();
                        //success = skip.skipOnStationSendAuto();
                        success = skip.skipOnStationSendAuto((int)_tpr_id, (int)_mrm_id, (int)_tps_id);
                    }
                    else if (rdPending.Checked)
                    {
                        Class.ClsPendingOnStation pend = new Class.ClsPendingOnStation();
                        //success = pend.pendingOnStationSendAuto();
                        success = pend.pendingOnStationSendAuto((int)_tpr_id, (int)_mvt_id, (int)_mrm_id, (int)_mhs_id, (int)_tps_id);
                    }

                    if (success == StatusTransaction.True)
                    {
                        if (rdSkip.Checked)
                        {
                            new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SkipAuto,
                                                        (int)_tpr_id,
                                                        (int)_tps_id,
                                                        Program.CurrentSite.mhs_id,
                                                        Program.CurrentRoom.mrd_ename,
                                                        Program.CurrentUser.mut_username);
                        }
                        else if (rdPending.Checked)
                        {
                            new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.PendingAuto,
                                                        (int)_tpr_id,
                                                        (int)_tps_id,
                                                        Program.CurrentSite.mhs_id,
                                                        Program.CurrentRoom.mrd_ename,
                                                        Program.CurrentUser.mut_username);
                        }

                        Program.CurrentRegis = null;
                        Program.CurrentPatient_queue = null;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else if (success == StatusTransaction.ReSendManualSite2)
                    {
                        MessageBox.Show("ไม่สามารถดำเนินการ send auto ได้ กรุณาเลือกห้องที่จะส่งอีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("กรุณาดำเนินการอีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = System.Windows.Forms.DialogResult.Retry;
                        //return error
                    }
                }
                else if (updateLower == StatusTransaction.Error)
                {
                    MessageBox.Show("กรุณา send auto อีกครั้ง", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {

            }
        }

        private void cmbSite_SelectedValueChanged(object sender, EventArgs e)
        {
            int? mhs_id = (int?)cmbSite.SelectedValue;
            setGridStationBindingSource(_tpr_id, _mrm_id, mhs_id);
        }

        private void rdNA_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel2.Enabled = !rdNA.Checked;
        }


        private bool _ultrasound = false;
        private bool? _checkLower = false;
        private string _timeLower = "";
        private StatusTransaction UpdatedLower()
        {
            try
            {
                try
                {
                    _timeLower = Convert.ToInt32(_timeLower).ToString();
                }
                catch
                {
                    _timeLower = "0";
                }

                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {

                    DateTime datenow = Program.GetServerDateTime();
                    trn_patient_regi objpatientregis = (from t1 in cdc.trn_patient_regis
                                                        where t1.tpr_id == _tpr_id
                                                        select t1).FirstOrDefault();
                    objpatientregis.tpr_miss_lower = (_checkLower == true) ? true : false;
                    objpatientregis.tpr_miss_lower_date = (_checkLower == true) ? (DateTime?)datenow : null;
                    objpatientregis.tpr_call_lower_time = Convert.ToInt32(_timeLower);
                    objpatientregis.tpr_call_lower_date = Program.GetServerDateTime().AddMinutes((double)objpatientregis.tpr_call_lower_time);
                    cdc.SubmitChanges();
                    return StatusTransaction.True;
                }
                //End Set column [tus_miss_lower]
            }
            catch (Exception ex)
            {
                Program.MessageError("frmUltrasound2", "UpdatedLower", ex, false);
                return StatusTransaction.Error;
            }
        }
    }
}
