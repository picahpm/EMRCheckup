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
    public partial class ChestXrayFrm : CheckupInheriteFrm
    {
        public ChestXrayFrm()
        {
            InitializeComponent();
            DisableBtnAll();
        }
        private void ChestXrayFrm_tpridChanged(object sender, int? e, tpr_idStatus? status)
        {
            if (e == null)
            {
                if (cdc != null) cdc.Dispose();
                newPatientProfileStationUC1.tpr_id = null;
                newPatientMappingUC1.tpr_id = null;
                chestXRayUC1.PatientRegis = null;
                StatusEmptyRoom();
            }
            else
            {
                try
                {
                    cdc = new InhCheckupDataContext();
                    trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == e).FirstOrDefault();
                    if (PatientRegis == null)
                    {
                        newPatientProfileStationUC1.tpr_id = null;
                        newPatientMappingUC1.tpr_id = null;
                        chestXRayUC1.PatientRegis = null;
                        StatusWK();
                    }
                    else
                    {
                        if (Program.CurrentPatient_queue != null)
                        {
                            this.tps_id = Program.CurrentPatient_queue.tps_id;
                            this.queue_mvt_id = Program.CurrentPatient_queue.mvt_id;
                        }
                        newPatientProfileStationUC1.tpr_id = e;
                        newPatientMappingUC1.tpr_id = e;
                        chestXRayUC1.PatientRegis = PatientRegis;
                        if (this.FormStatus == formStatus.isStation)
                        {
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
                        else
                        {
                            StatusFooter();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError(this.Name, "ChestXrayFrm_tpridChanged", ex, false);
                }
            }
        }
        private void ChestXrayFrm_mrdidChanged(object sender, int? e)
        {
            if (this.FormStatus == formStatus.isStation)
            {
                if (e == null)
                {
                    newWaitingListStationUC1.mrd_id = null;
                    newWaitingListStationUC1.mut_id = this.user.mut_id;
                }
                else
                {
                    try
                    {
                        newWaitingListStationUC1.mrd_id = e;
                        newWaitingListStationUC1.mut_id = this.user.mut_id;
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "ChestXrayFrm_mrdidChanged", ex, false);
                    }
                }
            }
        }

        PopupUltrasoundLower.ResultPopupUltrasoundLower resultUltrasound = PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater;

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

            StatusTransaction ready = CallQueue.P_CallQueueReady();
            if (ready == StatusTransaction.True)
            {
                StatusTransaction showUnit = new ClsTCPClient().sendCallUnitDisplay();
                if (showUnit == StatusTransaction.Error)
                {
                    //lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถแสดงผลบน unit display ได้";
                }
                Class.FunctionDataCls func = new Class.FunctionDataCls();
                resultUltrasound = func.popupUltrasoundLower();
                if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.BeforeStation)
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    List<int> mvt = mst.GetMstRoomEventByMrm(Program.CurrentRoom.mrm_id).Select(x => x.mvt_id).ToList();

                    Class.FunctionDataCls.sendQueueStatus result = func.sendQueueUltrasoundLower(resultUltrasound, mvt);
                    if (result == Class.FunctionDataCls.sendQueueStatus.error)
                    {
                        lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถส่งไป ultrasound ได้ กรุณาติดต่อผู้ดูแลระบบ";
                        AlertOutDepartment.LoadTime();
                        StatusWK();
                    }
                    else if (result == Class.FunctionDataCls.sendQueueStatus.sendSuccess)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendUltraSoundBefore,
                                                    (int)tpr_id,
                                                    (int)tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);

                        new ClsTCPClient().sendClearUnitDisplay();
                        StatusEmptyRoom();
                        lbAlertMsg.Text = func.GetStrSaveAndSend((int)tpr_id, "US", "UL");
                        this.tpr_id = null;
                    }
                }
                else if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
                {
                    AlertOutDepartment.LoadTime();

                    ReserveSkipCls reserveSkip = new ReserveSkipCls();
                    int? skipRoom = reserveSkip.CheckRoomSkip(tpr_id);
                    string alert = reserveSkip.MessegeAlertSkip(skipRoom);
                    StatusSendUltrasoundAfter();
                    lbAlertMsg.Text = alert;
                }
                else if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater)
                {
                    AlertOutDepartment.LoadTime();

                    ReserveSkipCls reserveSkip = new ReserveSkipCls();
                    int? skipRoom = reserveSkip.CheckRoomSkip(tpr_id);
                    StatusWK();
                    string alert = reserveSkip.MessegeAlertSkip(skipRoom);
                    lbAlertMsg.Text = alert;
                }
            }
            else if (ready == StatusTransaction.Error)
            {
                lbAlertMsg.Text = "กรุณากดปุ่ม Ready อีกครั้ง";
                StatusNSWR();
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
                StatusTransaction callQ = CallQueue.P_CallQueueWaitReady();
                //end noina

                if (callQ == StatusTransaction.Error)
                {
                    lbAlertMsg.Text = "กรุณากด Call Queue อีกครั้ง";
                    btnCallQueue.Enabled = true;
                }
                else
                {
                    if (Program.CurrentRegis != null)
                    {
                        this.tpr_id = Program.CurrentRegis.tpr_id;
                        clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                        StatusNSWR();
                    }
                    else
                    {
                        lbAlertMsg.Text = "No patient on queue!";
                        StatusEmptyRoom();
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
                lbAlertMsg.Focus();
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

            try
            {
                StatusTransaction result = new SendManaulCls().SendManualOnStation(ref messegeAlert);
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
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            DisableBtnAll();
            DateTime startDate = DateTime.Now;
            this.AutoScrollPosition = new Point(0, 0);

            Class.FunctionDataCls func = new Class.FunctionDataCls();
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            try
            {
                if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
                {
                    List<int> mvt = mst.GetMstRoomEventByMrm(Program.CurrentRoom.mrm_id).Select(x => x.mvt_id).ToList();

                    Class.FunctionDataCls.sendQueueStatus result = func.sendQueueUltrasoundLower(resultUltrasound, mvt);
                    if (result == Class.FunctionDataCls.sendQueueStatus.error)
                    {
                        lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถส่งไป ultrasound ได้ กรุณาติดต่อผู้ดูแลระบบ";
                        StatusWK();
                    }
                    else if (result == Class.FunctionDataCls.sendQueueStatus.sendSuccess)
                    {
                        new ClsTCPClient().sendClearUnitDisplay();
                        lbAlertMsg.Text = func.GetStrSaveAndSend((int)tpr_id, "US", "UL");
                        StatusEmptyRoom();
                        this.tpr_id = null;
                    }
                }
                else
                {
                    if (new Class.FunctionDataCls().ChkSendAutoNewModule(Program.CurrentRegis))
                    {
                        string msgAlert = "";
                        bool isPopup = false;
                        QueueClass.SendAutoCls.ResultSendQueue result = new QueueClass.SendAutoCls().SendAuto((int)tps_id, user, ref msgAlert, ref isPopup);
                        if (result == QueueClass.SendAutoCls.ResultSendQueue.Error)
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
                    }
                    else
                    {
                        StatusTransaction result = CallQueue.SendAutoOnStation();
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
            }
            catch (Exception ex)
            {
                lbAlertMsg.Text = ex.Message;
                StatusWK();
            }
            frmbg.Close();
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
            StatusTransaction sendToB = new Class.SendQueue().SendToCheckB((int)tpr_id, mrm_id, ref messege);
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
    }
}
