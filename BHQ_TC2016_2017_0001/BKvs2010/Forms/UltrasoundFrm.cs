using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.Class;

namespace BKvs2010.Forms
{
    public partial class UltrasoundFrm : CheckupInheriteFrm
    {
        public UltrasoundFrm()
        {
            InitializeComponent();
            DisableBtnAll();
            SetBindingSource();
        }

        private class RoomEvent
        {
            public RoomEvent()
            {

            }

            public int mvt_id { get; set; }
            public string mvt_code { get; set; }
            private bool _visible;
            public bool visible
            {
                get
                {
                    return _visible;
                }
                set
                {
                    if (checkBox != null)
                    {
                        checkBox.Visible = value;
                    }
                    _visible = value;
                }
            }
            private bool _enable;
            public bool enable
            {
                get
                {
                    return _enable;
                }
                set
                {
                    if (checkBox != null)
                    {
                        checkBox.Enabled = value;
                    }
                    _enable = value;
                    _OnEnableChanged(value);
                }
            }
            private bool _autoCheck;
            public bool autoCheck
            {
                get
                {
                    return _autoCheck;
                }
                set
                {
                    if (checkBox != null)
                    {
                        checkBox.AutoCheck = value;
                    }
                    _autoCheck = value;
                }
            }
            private bool _check;
            public bool check
            {
                get { return _check; }
                set
                {
                    if (checkBox != null)
                    {
                        checkBox.Checked = value;
                    }
                    if (value != _check)
                    {
                        _check = value;
                        _OnCheckedChanged(value);
                    }
                }
            }

            public delegate void EnableChanged(object sender, bool checkedValue);
            public event EnableChanged OnEnableChanged;
            private void _OnEnableChanged(bool enableValue)
            {
                if (OnEnableChanged != null)
                {
                    OnEnableChanged(this, enableValue);
                }
            }

            public delegate void CheckedChanged(object sender, bool checkedValue);
            public event CheckedChanged OnCheckedChanged;
            private void _OnCheckedChanged(bool checkedValue)
            {
                if (OnCheckedChanged != null)
                {
                    OnCheckedChanged(this, checkedValue);
                }
            }

            private CheckBox _checkBox;
            public CheckBox checkBox
            {
                get { return _checkBox; }
                set
                {
                    value.CheckedChanged -= new EventHandler(checkBox_CheckedChanged);
                    value.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
                    _checkBox = value;
                }
            }
            private void checkBox_CheckedChanged(object sender, EventArgs e)
            {
                CheckBox chkBox = (CheckBox)sender;
                _check = chkBox.Checked;
                _OnCheckedChanged(_check);
            }
        }
        RoomEvent uu;
        RoomEvent ul;
        RoomEvent ub;
        RoomEvent uw;
        private void SetBindingSource()
        {
            try
            {
                using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                {
                    uu = contxt.mst_events.Where(x => x.mvt_code == "UU").Select(x => new RoomEvent { checkBox = chkUpper, mvt_code = x.mvt_code, mvt_id = x.mvt_id }).FirstOrDefault();

                    ul = contxt.mst_events.Where(x => x.mvt_code == "UL").Select(x => new RoomEvent { checkBox = chkLower, mvt_code = x.mvt_code, mvt_id = x.mvt_id }).FirstOrDefault();
                    ul.OnCheckedChanged += new RoomEvent.CheckedChanged(ul_OnCheckedChanged);
                    ul.OnEnableChanged += new RoomEvent.EnableChanged(ul_OnEnableChanged);

                    ub = contxt.mst_events.Where(x => x.mvt_code == "UB").Select(x => new RoomEvent { checkBox = chkBreast, mvt_code = x.mvt_code, mvt_id = x.mvt_id }).FirstOrDefault();
                    
                    uw = contxt.mst_events.Where(x => x.mvt_code == "UW").Select(x => new RoomEvent { checkBox = chkWhole, mvt_code = x.mvt_code, mvt_id = x.mvt_id }).FirstOrDefault();
                }
            }
            catch
            {

            }
        }
        private void ul_OnCheckedChanged(object sender, bool value)
        {
            if (value)
            {
                chkUltrasound.Enabled = false;
                chkUltrasound.Checked = false;
                txtMinuteLower.Enabled = false;
                label17.Enabled = false;
                txtMinuteLower.Text = "";
            }
            else
            {
                chkUltrasound.Enabled = true;
                chkUltrasound.Checked = true;
                txtMinuteLower.Enabled = true;
                label17.Enabled = true;
                txtMinuteLower.Text = "5";
                txtMinuteLower.Focus();
            }
        }
        private void ul_OnEnableChanged(object sender, bool value)
        {
            if (!value)
            {
                chkUltrasound.Enabled = false;
                chkUltrasound.Checked = false;
                txtMinuteLower.Enabled = false;
                label17.Enabled = false;
                txtMinuteLower.Text = "";
            }
        }

        private int maxCountLoadPatient = 5;
        private int countLoadPatient = 0;

        private void UltrasoundFrm_tpridChanged(object sender, int? e, CheckupInheriteFrm.tpr_idStatus? status)
        {
            if (e == null)
            {
                if (cdc != null) cdc.Dispose();
                this.tps_id = null;
                newPatientProfileStationUC1.tpr_id = null;
                newPatientMappingUC1.tpr_id = null;
                ultrasoundUC1.PatientRegis = null;
                countLoadPatient = 0;
                StatusEmptyRoom();
            }
            else
            {
                try
                {
                    cdc = new InhCheckupDataContext();
                    trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == e).FirstOrDefault();
                    newPatientProfileStationUC1.tpr_id = e;
                    newPatientMappingUC1.tpr_id = e;
                    ultrasoundUC1.PatientRegis = PatientRegis;
                    if (this.FormStatus == formStatus.isFooter)
                    {
                        panalMVT.Visible = true;
                        panalMVT.Enabled = false;
                        chkUltrasound.Checked = PatientRegis.tpr_miss_lower == true ? true : false;
                        txtMinuteLower.Text = PatientRegis.tpr_call_lower_time == null ? "" : PatientRegis.tpr_call_lower_time.ToString();
                        var PatientEvent = (from pl in PatientRegis.trn_patient_plans.Where(x => x.tpl_status == 'L' || x.tpl_status == 'P')
                                            join mv in cdc.mst_events.Where(x => new List<string> { "UU", "UL", "UB", "UW" }.Contains(x.mvt_code))
                                            on pl.mvt_id equals mv.mvt_id
                                            select mv.mvt_code
                                            ).ToList();

                        foreach (var ev in PatientEvent)
                        {
                            switch (ev)
                            {
                                case "UU":
                                    uu.check = true;
                                    break;
                                case "UL":
                                    ul.check = true;
                                    break;
                                case "UB":
                                    ub.check = true;
                                    break;
                                case "UW":
                                    uw.check = true;
                                    break;
                            }
                            break;
                        }
                        StatusFooter();
                    }
                    else
                    {
                        panalMVT.Visible = true;
                        panalMVT.Enabled = true;
                        var PatientEvent = (from pl in PatientRegis.trn_patient_plans
                                            join mv in cdc.mst_events.Where(x => new List<string> { "UU", "UL", "UB", "UW" }.Contains(x.mvt_code))
                                            on pl.mvt_id equals mv.mvt_id
                                            select new
                                            {
                                                mvt_code = mv.mvt_code,
                                                tpl_status = pl.tpl_status
                                            }).GroupBy(x => x.mvt_code)
                                            .Select(x => new 
                                            {
                                                mvt_code = x.Key,
                                                tpl_status = x.OrderBy(y => y.tpl_status == 'L' ? 0 :
                                                                            y.tpl_status == 'P' ? 1 :
                                                                            y.tpl_status == 'N' ? 2 : 3)
                                                              .Select(y => y.tpl_status)
                                                              .FirstOrDefault()
                                            }).ToList();

                        uu.enable = false;
                        ul.enable = false;
                        ub.enable = false;
                        uw.enable = false;
                        foreach (var ev in PatientEvent)
                        {
                            switch(ev.tpl_status)
                            {
                                case 'L':
                                case 'P':
                                    switch (ev.mvt_code)
                                    {
                                        case "UU":
                                            uu.enable = true;
                                            uu.autoCheck = false;
                                            uu.check = true;
                                            break;
                                        case "UL":
                                            ul.enable = true;
                                            ul.autoCheck = false;
                                            ul.check = true;
                                            break;
                                        case "UB":
                                            ub.enable = true;
                                            ub.autoCheck = false;
                                            ub.check = true;
                                            break;
                                        case "UW":
                                            uw.enable = true;
                                            uw.autoCheck = false;
                                            uw.check = true;
                                            break;
                                    }
                                    break;
                                case 'N':
                                    switch (ev.mvt_code)
                                    {
                                        case "UU":
                                            uu.enable = true;
                                            uu.autoCheck = true;
                                            uu.check = true;
                                            break;
                                        case "UL":
                                            ul.enable = true;
                                            ul.autoCheck = true;
                                            ul.check = true;
                                            break;
                                        case "UB":
                                            ub.enable = true;
                                            ub.autoCheck = true;
                                            ub.check = true;
                                            break;
                                        case "UW":
                                            uw.enable = true;
                                            uw.autoCheck = true;
                                            uw.check = true;
                                            break;
                                    }
                                    break;
                                default:
                                    switch (ev.mvt_code)
                                    {
                                        case "UU":
                                            uu.enable = false;
                                            uu.autoCheck = false;
                                            uu.check = false;
                                            break;
                                        case "UL":
                                            ul.enable = false;
                                            ul.autoCheck = false;
                                            ul.check = false;
                                            break;
                                        case "UB":
                                            ub.enable = false;
                                            ub.autoCheck = false;
                                            ub.check = false;
                                            break;
                                        case "UW":
                                            uw.enable = false;
                                            uw.autoCheck = false;
                                            uw.check = false;
                                            break;
                                    }
                                    break;
                            }
                        }
                                                                       
                        switch (status)
                        {
                            case tpr_idStatus.NSWR:
                                StatusNSWR();
                                break;
                            case tpr_idStatus.WK:
                                StatusWK();
                                break;
                        }
                    }

                    //if (PatientRegis == null)
                    //{
                    //    ultrasoundUC1.StatusUC = UserControlEMR.UltrasoundUC.statusUC.isStation;
                    //    newPatientProfileStationUC1.tpr_id = null;
                    //    newPatientMappingUC1.tpr_id = null;
                    //    ultrasoundUC1.PatientRegis = null;
                    //    StatusWK();
                    //}
                    //else
                    //{
                    //    if (Program.CurrentPatient_queue != null)
                    //    {
                    //        this.tps_id = Program.CurrentPatient_queue.tps_id;
                    //        this.queue_mvt_id = Program.CurrentPatient_queue.mvt_id;
                    //    }
                    //    newPatientProfileStationUC1.tpr_id = e;
                    //    newPatientMappingUC1.tpr_id = e;
                    //    ultrasoundUC1.PatientRegis = PatientRegis;
                    //    if (this.FormStatus == formStatus.isStation)
                    //    {
                    //        ultrasoundUC1.StatusUC = UserControlEMR.UltrasoundUC.statusUC.isStation;
                    //        switch (status)
                    //        {
                    //            case tpr_idStatus.NSWR:
                    //                StatusNSWR();
                    //                break;
                    //            case tpr_idStatus.WK:
                    //                StatusWK();
                    //                break;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ultrasoundUC1.StatusUC = UserControlEMR.UltrasoundUC.statusUC.isFooter;
                    //        StatusFooter();
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    countLoadPatient++;
                    Program.MessageError(this.Name, "UltrasoundFrm", ex, false);
                    if (countLoadPatient <= maxCountLoadPatient)
                    {
                        UltrasoundFrm_tpridChanged(sender, e, status);
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }
        private void UltrasoundFrm_mrdidChanged(object sender, int? e)
        {
            if (this.FormStatus == formStatus.isStation)
            {
                if (e == null)
                {
                    newWaitingListStationUC1.mrd_id = null;
                    ultrasoundUC1.mrd_id = null;
                    newWaitingListStationUC1.mut_id = this.user.mut_id;
                }
                else
                {
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            uu.visible = false;
                            ul.visible = false;
                            ub.visible = false;
                            uw.visible = false;
                            var roomEvent = cdc.mst_room_events
                                               .Where(x => x.mst_room_hdr.mst_room_dtls.Any(y => y.mrd_id == e))
                                               .Select(x => new
                                               {
                                                   x.mvt_id,
                                                   x.mst_event.mvt_code
                                               }).ToList();
                            foreach (var evt in roomEvent)
                            {
                                switch (evt.mvt_code)
                                {
                                    case "UU":
                                        uu.visible = true;
                                        break;
                                    case "UL":
                                        ul.visible = true;
                                        break;
                                    case "UB":
                                        ub.visible = true;
                                        break;
                                    case "UW":
                                        uw.visible = true;
                                        break;
                                }
                            }
                        }
                        newWaitingListStationUC1.mrd_id = e;
                        ultrasoundUC1.mrd_id = e;
                        newWaitingListStationUC1.mut_id = this.user.mut_id;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "UltrasoundFrm_mrdidChanged", ex, false);
                    }
                }
            }
        }

        countDownCls clsCountDown = new countDownCls();
        private void LoadHandlerCountDown()
        {
            clsCountDown.successCountDown += new countDownCls.SuccessHandler(cls_successCountDown);
            clsCountDown.countDownTick += new countDownCls.TickHandler(cls_countDownTick);
        }
        private void cls_countDownTick(object sender, timeArgs e)
        {
            groupQueue.Text = "Queue " + e.countDownTime.timeString;
        }
        private void cls_successCountDown(object sender, successTypeArgs e)
        {
            DisableBtnAll();
            QueueClass.TransactionQueueCls.PatientReadyQueue ready = new QueueClass.TransactionQueueCls().ReadyQueue((int)this.tps_id, (int)this.mrd_id, this.user.mut_username);
            if (ready.Status == QueueClass.TransactionQueueCls.PatientReadyQueue.StatusReadyQueue.Error)
            {
                lbAlertMsg.Text = "กรุณากดปุ่ม Ready อีกครั้ง";
                StatusNSWR();
            }
            else
            {
                StatusTransaction showUnit = new ClsTCPClient().sendCallUnitDisplay();
                if (showUnit == StatusTransaction.Error)
                {
                    //lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถแสดงผลบน unit display ได้";
                }
                AlertOutDepartment.LoadTime();
                ReserveSkipCls reserveSkip = new ReserveSkipCls();
                int? skipRoom = reserveSkip.CheckRoomSkip(this.tpr_id);
                StatusWK();
                string alert = reserveSkip.MessegeAlertSkip(skipRoom);
                lbAlertMsg.Text = alert;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            LoadHandlerCountDown();
            this.Text = PrePareData.StaticDataCls.RoomName(this.Text);
            if (FormStatus == formStatus.isStation)
            {
                //uiFooter1.LoadData();
                refreshTool();
            }
            frmbg.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            clsCountDown.cancelCountDown();
            this.Dispose();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (cdc != null) cdc.Dispose();
        }

        private void refreshTool()
        {
            //BackgroundWorker bw = new BackgroundWorker();
            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //bw.RunWorkerAsync();
            newWaitingListStationUC1.LoadData();
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            newWaitingListStationUC1.LoadData();
            uiFooter1.LoadData();
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 5000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            timer.Dispose();
            if (!this.IsDisposed)
            {
                refreshTool();
            }
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            clsCountDown.finishCountDown();
        }
        private void btnCallQueue_Click(object sender, EventArgs e)
        {
            DisableBtnAll();
            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            try
            {
                //noina
                if (this.mrd_id != null && this.user != null)
                {
                    QueueClass.TransactionQueueCls.PatientCallQueue CallQueue = new QueueClass.TransactionQueueCls().CallQueue((int)this.mrd_id, this.user.mut_id, this.user.mut_username);
                    if (CallQueue.Status == QueueClass.TransactionQueueCls.PatientCallQueue.StatusCallQueue.Error)
                    {
                        lbAlertMsg.Text = "กรุณากด Call Queue อีกครั้ง";
                        StatusEmptyRoom();
                    }
                    else if (CallQueue.Status == QueueClass.TransactionQueueCls.PatientCallQueue.StatusCallQueue.NoPatient)
                    {
                        lbAlertMsg.Text = "No patient on queue!";
                        StatusEmptyRoom();
                    }
                    else
                    {
                        this.tpr_id = CallQueue.tpr_id;
                        this.tps_id = CallQueue.tps_id;
                        clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                        StatusNSWR();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnCallQueue_Click", ex, false);
            }
            finally
            {
                frmbg.Close();
            }
        }
        private void btnHold_Click(object sender, EventArgs e)
        {
            DisableBtnAll();
            try
            {
                int holdTime = PrePareData.StaticDataCls.HoldTime.Where(x => x.mhs_id == this.mhs_id).Select(x => x.holdTime).FirstOrDefault();
                QueueClass.TransactionQueueCls.PatientHoldQueue hold = new QueueClass.TransactionQueueCls().HoldQueue((int)this.tps_id, holdTime, this.user.mut_username);
                if (hold.Status == QueueClass.TransactionQueueCls.PatientHoldQueue.StatusHoldQueue.Error)
                {
                    lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
                    StatusWK();
                }
                else
                {
                    StatusEmptyRoom();
                    lbAlertMsg.Text = string.Format(Program.MsgHold, hold.QueueNo);
                    this.tpr_id = null;
                }
                //if (Program.CurrentRegis != null)
                //{


                //    string QueueNo = Program.CurrentRegis.tpr_queue_no;
                //    StatusTransaction result = CallQueue.P_CallHold();
                //    if (result == StatusTransaction.True)
                //    {
                //        // morn clear Unit Display
                //        new ClsTCPClient().sendClearUnitDisplay();
                //        // morn clear Unit Display

                //        StatusEmptyRoom();
                //        lbAlertMsg.Text = string.Format(Program.MsgHold, QueueNo);
                //        this.tpr_id = null;
                //    }
                //    else if (result == StatusTransaction.Error)
                //    {
                //        lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
                //        StatusWK();
                //    }
                //}
                //else
                //{
                //    lbAlertMsg.Focus();
                //    lbAlertMsg.Text = "tpr_id is null please try again and close this page !";
                //    StatusWK();
                //}
            }
            catch
            {
                lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
                StatusWK();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisableBtnAll();
            try
            {
                int? mvt_id = Program.CurrentPatient_queue != null ? (int?)Program.CurrentPatient_queue.mvt_id : null;
                frmCancelQueue frmCancelQueue = new frmCancelQueue(tpr_id, mvt_id);
                if (frmCancelQueue.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    new ClsTCPClient().sendClearUnitDisplay();
                    StatusEmptyRoom();
                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    lbAlertMsg.Text = func.getStringGotoNextRoom((int)tpr_id);
                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                    this.tpr_id = null;
                    return;
                }
                else
                {
                    StatusWK();
                }
            }
            catch
            {
                lbAlertMsg.Text = "กรุณากด Cancel อีกครั้ง";
                StatusWK();
            }
        }
        private void btnSendManual_Click(object sender, EventArgs e)
        {
            DisableBtnAll();
            this.AutoScrollPosition = new Point(0, 0);
            string messegeAlert = "";
            List<int> mvt_id = new List<int>();
            if (uu.enable && uu.visible && uu.check && uu.autoCheck) mvt_id.Add(uu.mvt_id);
            if (ul.enable && ul.visible && ul.check && ul.autoCheck) mvt_id.Add(ul.mvt_id);
            if (ub.enable && ub.visible && ub.check && ub.autoCheck) mvt_id.Add(ub.mvt_id);
            if (uw.enable && uw.visible && uw.check && uw.autoCheck) mvt_id.Add(uw.mvt_id);
            if (mvt_id.Count() == 0)
            {
                lbAlertMsg.Text = "กรุณาเลือก Order ที่ต้องการตรวจเพิ่มอย่างน้อย 1 รายการ";
                StatusWK();
            }
            else
            {
                if (CheckMinLower())
                {
                    if (SkipLower())
                    {
                        try
                        {
                            StatusTransaction result = new SendManaulCls().SendManualOnStation(mvt_id, ref messegeAlert);
                            if (result == StatusTransaction.True)
                            {
                                new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendManual,
                                                            (int)tpr_id,
                                                            (int)tps_id,
                                                            Program.CurrentSite.mhs_id,
                                                            Program.CurrentRoom.mrd_ename,
                                                            Program.CurrentUser.mut_username);

                                new ClsTCPClient().sendClearUnitDisplay();
                                new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                lbAlertMsg.Text = messegeAlert;
                                StatusEmptyRoom();
                                this.tpr_id = null;
                            }
                            else if (result == StatusTransaction.False)
                            {
                                StatusWK();
                            }
                            else if (result == StatusTransaction.Error)
                            {
                                lbAlertMsg.Text = "โปรด send manaul อีกครั้ง";
                                StatusWK();
                            }
                        }
                        catch (Exception ex)
                        {
                            lbAlertMsg.Text = "เกิดความผิดพลาดของระบบ โปรด send manaul อีกครั้ง";
                            Program.MessageError(this.Name, "btnSendManual_Click", ex, false);
                            StatusWK();
                        }
                    }
                    else
                    {
                        StatusWK();
                    }
                }
                else
                {
                    StatusWK();
                }
            }
        }
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            DisableBtnAll();
            DateTime startDate = DateTime.Now;
            this.AutoScrollPosition = new Point(0, 0);
            List<int> mvt_id = new List<int>();
            if (uu.enable && uu.visible && uu.check && uu.autoCheck) mvt_id.Add(uu.mvt_id);
            if (ul.enable && ul.visible && ul.check && ul.autoCheck) mvt_id.Add(ul.mvt_id);
            if (ub.enable && ub.visible && ub.check && ub.autoCheck) mvt_id.Add(ub.mvt_id);
            if (uw.enable && uw.visible && uw.check && uw.autoCheck) mvt_id.Add(uw.mvt_id);
            if (mvt_id.Count() == 0)
            {
                lbAlertMsg.Text = "กรุณาเลือก Order ที่ต้องการตรวจเพิ่มอย่างน้อย 1 รายการ";
                StatusWK();
            }
            else
            {
                if (CheckMinLower())
                {
                    if (SkipLower())
                    {
                        Class.FunctionDataCls func = new Class.FunctionDataCls();
                        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                        frmBGScreen frmbg = new frmBGScreen();
                        frmbg.Show();
                        Application.DoEvents();
                        try
                        {
                            if (new Class.FunctionDataCls().ChkSendAutoNewModule(Program.CurrentRegis))
                            {
                                string msgAlert = "";
                                bool isPopup = false;
                                QueueClass.SendAutoCls.ResultSendQueue result = new QueueClass.SendAutoCls().SendAuto((int)tps_id, mvt_id, user, ref msgAlert, ref isPopup);
                                if (result == QueueClass.SendAutoCls.ResultSendQueue.Error)
                                {
                                    lbAlertMsg.Text = msgAlert;
                                    StatusWK();
                                }
                                else if (result == QueueClass.SendAutoCls.ResultSendQueue.OldRoom)
                                {
                                    lbAlertMsg.Text = msgAlert;
                                    StatusWK();
                                }
                                else
                                {
                                    lbAlertMsg.Visible = true;
                                    if (result == QueueClass.SendAutoCls.ResultSendQueue.SendComplete)
                                    {
                                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                                    (int)tpr_id,
                                                                    (int)tps_id,
                                                                    Program.CurrentSite.mhs_id,
                                                                    Program.CurrentRoom.mrd_ename,
                                                                    Program.CurrentUser.mut_username,
                                                                    startDate);

                                        new ClsTCPClient().sendClearUnitDisplay();
                                        new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                        StatusEmptyRoom();
                                        this.tpr_id = null;
                                        if (isPopup)
                                        {
                                            MessageBox.Show(msgAlert, "EMR Checkup.");
                                        }
                                        else
                                        {
                                            lbAlertMsg.Text = msgAlert;
                                        }
                                    }
                                    else
                                    {
                                        if (isPopup)
                                        {
                                            MessageBox.Show(msgAlert, "EMR Checkup.");
                                        }
                                        else
                                        {
                                            lbAlertMsg.Text = msgAlert;
                                        }
                                    }
                                }
                                //using (Service.WS_Checkup ws = new Service.WS_Checkup())
                                //{
                                //    WS_PathWay.SendAutoResult result = ws.SendAuto((int)tps_id, mvt_id.ToArray(), user.mut_username, Program.CurrentSite.mhs_id);
                                //    if (result.SendResult == WS_PathWay.Result.Error)
                                //    {
                                //        lbAlertMsg.Text = result.AlertMsg;
                                //        StatusWK();
                                //    }
                                //    else
                                //    {
                                //        if (result.SendResult == WS_PathWay.Result.Complete)
                                //        {
                                //            new ClsTCPClient().sendClearUnitDisplay();
                                //            new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                //            StatusEmptyRoom();
                                //            this.tpr_id = null;
                                //            lbAlertMsg.Text = result.AlertMsg;
                                //        }
                                //        else
                                //        {
                                //            lbAlertMsg.Text = result.AlertMsg;
                                //            StatusWK();
                                //        }
                                //    }
                                //}
                            }
                            else
                            {
                                StatusTransaction result = CallQueue.SendAutoOnStation(mvt_id);
                                if (result == StatusTransaction.True || result == StatusTransaction.SendCheckB)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                                (int)tpr_id,
                                                                (int)tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username,
                                                                startDate);
                                    
                                    new ClsTCPClient().sendClearUnitDisplay();
                                    lbAlertMsg.Visible = true;
                                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                    lbAlertMsg.Text = new Class.FunctionDataCls().getStringGotoNextRoom((int)tpr_id);
                                    StatusEmptyRoom();
                                    this.tpr_id = null;
                                }
                                else if (result == StatusTransaction.Error)
                                {
                                    lbAlertMsg.Text = "โปรดกด Send Auto อีกครั้ง";
                                    StatusWK();
                                }
                                else if (result == StatusTransaction.False)
                                {
                                    lbAlertMsg.Text = "Save Data Complete.";
                                    StatusWK();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lbAlertMsg.Text = ex.Message;
                            StatusWK();
                        }
                        finally
                        {
                            frmbg.Close();
                        }
                    }
                    else
                    {
                        StatusWK();
                    }
                }
                else
                {
                    StatusWK();
                }
            }
        }
        private void btnSendToCheckB_Click(object sender, EventArgs e)
        {
            DisableBtnAll();

            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            int mrm_id = Program.CurrentRoom.mrm_id;
            string messege = "";
            List<int> mvt_id = new List<int>();
            if (uu.enable && uu.visible && uu.check) mvt_id.Add(uu.mvt_id);
            if (ul.enable && ul.visible && ul.check) mvt_id.Add(ul.mvt_id);
            if (ub.enable && ub.visible && ub.check) mvt_id.Add(ub.mvt_id);
            if (uw.enable && uw.visible && uw.check) mvt_id.Add(uw.mvt_id);
            if (mvt_id.Count() == 0)
            {
                lbAlertMsg.Text = "กรุณาเลือก Order ที่ต้องการตรวจเพิ่มอย่างน้อย 1 รายการ";
                StatusWK();
            }
            else
            {
                StatusTransaction sendToB = new Class.SendQueue().SendToCheckB((int)tpr_id, mvt_id, (int)this.tps_id, ref messege);
                if (sendToB == StatusTransaction.True)
                {
                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendCheckB,
                                                (int)tpr_id,
                                                (int)tps_id,
                                                Program.CurrentSite.mhs_id,
                                                Program.CurrentRoom.mrd_ename,
                                                Program.CurrentUser.mut_username);

                    new ClsTCPClient().sendClearUnitDisplay();
                    lbAlertMsg.Text = messege;
                    StatusEmptyRoom();
                    this.tpr_id = null;
                }
                else if (sendToB == StatusTransaction.Error)
                {
                    lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถส่งไปยัง Checkpoint B ได้ กรุณา กดปุ่ม go to Checkpoint B อีกครั้ง";
                    StatusWK();
                }
            }
            frmbg.Close();
        }
        private void newWaitingListStationUC1_OnWaitingSuccessProcess(object sender, StatusTransaction isCallQueue, string e)
        {
            this.AutoScrollPosition = new Point(0, 0);
            lbAlertMsg.Text = e;
            if (isCallQueue == StatusTransaction.True)
            {
                DisableBtnAll();
                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();
                Application.DoEvents();

                if (Program.CurrentRegis != null)
                {
                    this.tpr_id = Program.CurrentRegis.tpr_id;
                    this.tps_id = Program.CurrentPatient_queue.tps_id;
                    clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                    StatusNSWR();
                }
                else
                {
                    lbAlertMsg.Text = "No patient on queue!";
                    StatusEmptyRoom();
                }
                frmbg.Close();
            }
            else
            {
                if (this.tpr_id == null)
                {
                    StatusEmptyRoom();
                }
                else
                {
                    StatusWK();
                }
            }
        }

        private void DisableBtnAll()
        {
            lbAlertMsg.Text = "";
            btnReady.Enabled = false;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;
            btnSendManual.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendToCheckB.Enabled = false;
        }
        private void StatusEmptyRoom()
        {
            btnCallQueue.Enabled = true;
            panalMVT.Enabled = false;
            uu.check = false;
            ul.check = false;
            ub.check = false;
            uw.check = false;
        }
        private void StatusNSWR()
        {
            btnReady.Enabled = true;
        }
        private void StatusWK()
        {
            groupQueue.Text = "";
            btnHold.Enabled = true;
            btnCancel.Enabled = true;
            btnSendManual.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendToCheckB.Enabled = true;
        }
        private void StatusFooter()
        {

        }
        private void StatusSendUltrasoundAfter()
        {
            btnHold.Enabled = true;
            btnCancel.Enabled = true;
            btnSendAuto.Enabled = true;
        }

        private bool CheckMinLower()
        {
            try
            {
                if (!chkUltrasound.Checked) return true;
                int min = Convert.ToInt32(txtMinuteLower.Text);
                if (min == 0)
                {
                    lbAlertMsg.Text = "กรุณาระบุเวลาที่ต้องการตรวจ U/S Lower อีกครั้ง";
                    return false;
                }
                return true;
            }
            catch
            {
                lbAlertMsg.Text = "กรุณาระบุเวลาที่ต้องการตรวจ U/S Lower อีกครั้ง";
                txtMinuteLower.Text = "0";
                txtMinuteLower.Focus();
                return false;
            }
        }
        private bool SkipLower()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_regi PatientRegis = cdc.trn_patient_regis
                                                       .Where(x => x.tpr_id == tpr_id)
                                                       .FirstOrDefault();
                    PatientRegis.tpr_miss_lower = chkUltrasound.Checked;
                    if (chkUltrasound.Checked)
                    {
                        PatientRegis.tpr_miss_lower_date = dateNow;
                        PatientRegis.tpr_call_lower_time = Convert.ToInt32(txtMinuteLower.Text);
                        PatientRegis.tpr_call_lower_date = dateNow.AddMinutes(Convert.ToInt32(txtMinuteLower.Text));
                    }
                    else
                    {
                        PatientRegis.tpr_miss_lower_date = null;
                        PatientRegis.tpr_call_lower_time = null;
                        PatientRegis.tpr_call_lower_date = null;
                    }
                    cdc.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SkipLower()", ex, false);
                return false;
            }
        }
    }
}