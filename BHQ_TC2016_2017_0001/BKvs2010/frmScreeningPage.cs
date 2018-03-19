using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Windows.Forms;
using System.Collections;
using BKvs2010.Class;
using DBCheckup;
using DBToDoList;

namespace BKvs2010
{
    public partial class frmScreeningPage : Form
    {
        public frmScreeningPage()
        {
            InitializeComponent();
            GridPackage.AutoGenerateColumns = false;
            if (Program.CurrentSite.mhs_extra_pe_type == true)
            {
                chkReturnScreening.Visible = true;
            }
            else
            {
                chkReturnScreening.Visible = false;
            }
            this.WindowState = FormWindowState.Maximized;

            uiAllLeft2.OnRefreshStatusED += new Usercontrols.UIAllLeft.RefreshStatusED(uiAllLeft1_OnRefreshStatusED);
            uiFooter1.OnFooternameClick += new Usercontrols.UIFooter.FooterNameClick(OnUCFooterClicked);
            setRadioPEonSite2();
            GridPackage.AutoGenerateColumns = false;
        }
        private void setRadioPEonSite2()
        {
            string mhs_code = Program.CurrentSite.mhs_code;
            tbPnPESite.Height = 30;
            if (Program.CurrentSite.mhs_extra_pe_type == true)
            {
                tbPnPESite.RowStyles[0].Height = 0;
                tbPnPESite.RowStyles[1].Height = 26;
            }
            else
            {
                tbPnPESite.RowStyles[0].Height = 26;
                tbPnPESite.RowStyles[1].Height = 0;
            }
            int? tpr_id = null;
            if (SetTprID == 0)
            {
                if (Program.CurrentRegis != null)
                {
                    tpr_id = Program.CurrentRegis.tpr_id;
                }
            }
            else
            {
                tpr_id = SetTprID;
            }
            if (tpr_id != null)
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string cur_regis_site = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.mst_hpc_site.mhs_code).FirstOrDefault();
                    mst_hpc_site patient_site = cdc.mst_hpc_sites.Where(x => x.mhs_code == cur_regis_site).FirstOrDefault();
                    if (cur_regis_site != null)
                    {
                        if (patient_site.mhs_extra_pe_type == true)
                        {
                            tbPnPESite.RowStyles[0].Height = 0;
                            tbPnPESite.RowStyles[1].Height = 26;
                        }
                        else
                        {
                            tbPnPESite.RowStyles[0].Height = 26;
                            tbPnPESite.RowStyles[1].Height = 0;
                        }
                    }
                }
            }
        }

        string strSelected;
        int mhcid,oldmhcid;
        public int SetTprID{ get;set;}
        public int siteitem { get; set; }
        private static List<pw_Get_AviationTypeResult> getAviationType;

        private void OnUCFooterClicked(int tprid,int mhs_id)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            btnloadPackageTK.Enabled = false;

            siteitem = mhs_id;
            SetTprID = tprid;
            setRadioPEonSite2();

            trn_patient_queue objQtxp = (from t1 in dbc.trn_patient_queues
                                         where (t1.mst_room_hdr.mrm_code == "SC" || t1.mst_room_hdr.mrm_code == "RG")
                                         && t1.tpr_id == tprid
                                         select t1).FirstOrDefault();
            if (objQtxp != null)
            {
                EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();

                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string hn = cdc.trn_patient_regis.Where(x => x.tpr_id == tprid).Select(x => x.trn_patient.tpt_hn_no).FirstOrDefault();
                    if (hn != null)
                    {
                        trn_name_check tnc = cls.getNameCheck(hn);
                        if (tnc != null)
                        {
                            DDcompany.SelectedValue = tnc.trn_company_detail.tcd_code;
                            txtEmployeeID.Text = tnc.tnc_emp_id;
                        }
                    }
                }
                this.LoadData(tprid);
                GetAviation(tprid);
                CheckConsenFrom(tprid);
                uiAllLeft2.LoadDataAll(tprid);
                //uiFooter1.LoadData();

                StatusEnableControlClickFooter(tprid);
            }
            frmbg.Close();
        }
        private void uiAllLeft1_OnRefreshStatusED()
        {
            StatusWaitCallQueue();
        }

        private void StatusEnableControlClickFooter(int tpr_id)
        {
            btnReady.Enabled = false;
            btnCOFrom.Enabled = false;
            btnprintslip.Enabled = true;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;
            btnSendManual.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSaveDraft.Enabled = true;
            panel2.Enabled = true;
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();

        // Button Call Queue
        private void btnSendA_Click(object sender, EventArgs e)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int tps_id = Program.CurrentPatient_queue.tps_id;

                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        var countDoctorPE = (from t1 in cdc.trn_patient_queues
                                             where t1.tpr_id == Program.CurrentRegis.tpr_id
                                             && t1.mst_room_hdr.mrm_code == "DC"
                                             && t1.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id
                                             select t1).Count();
                        if (countDoctorPE > 0)
                        { //ถ้าเคยเข้าตรวจแล้ว

                            string messege = "";
                            StatusTransaction result = new Class.SendQueue().SendToCheckBFromScreening(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                            if (result == StatusTransaction.True)
                            {
                                new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendCheckB,
                                                            tpr_id,
                                                            tps_id,
                                                            Program.CurrentSite.mhs_id,
                                                            Program.CurrentRoom.mrd_ename,
                                                            Program.CurrentUser.mut_username);

                                new Class.FunctionDataCls().dispense_doctor_by_point(tpr_id);
                                new ClsTCPClient().sendClearUnitDisplay();
                                StatusWaitCallQueue();
                                lbAlertMsg.Text = messege;
                            }
                        }
                        else
                        {
                            if (RDReuestPEBeforYES.Checked == true && Program.CurrentRegis.tpr_req_inorout_doctor != "UT")
                            {
                                string messege = "";
                                StatusTransaction result = new Class.SendQueue().SendToPE(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                                if (result == StatusTransaction.True)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendPE,
                                                                tpr_id,
                                                                tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username);

                                    new ClsTCPClient().sendClearUnitDisplay();
                                    //new Class.FunctionDataCls().stampPEDoctor(Program.CurrentRegis.tpr_id);
                                    new Class.FunctionDataCls().dispense_doctor_by_waiting(tpr_id);
                                    StatusWaitCallQueue();
                                    lbAlertMsg.Text = messege;
                                }
                            }
                            else
                            {
                                string messege = "";
                                StatusTransaction result = new Class.SendQueue().SendToCheckBFromScreening(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                                if (result == StatusTransaction.True)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendCheckB,
                                                                tpr_id,
                                                                tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username);

                                    new ClsTCPClient().sendClearUnitDisplay();
                                    new Class.FunctionDataCls().dispense_doctor_by_point(tpr_id);
                                    StatusWaitCallQueue();
                                    lbAlertMsg.Text = messege;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("frmScreeningPage", "btnSendA_Click", ex, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("frmScreeningPage", "btnSendA_Click", ex, false);
            }
        }
        private void btnCallQ_Click(object sender, EventArgs e)
        {
            btnCallQueue.Enabled = false;
            lbAlertMsg.Text = "";
            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

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

                        //noina add 21/01/2557
                        LoadUI();
                        clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                        try
                        {
                            this.LoadData(Program.CurrentRegis.tpr_id);
                            RDNormal_CheckedChanged(null, null);
                            GetAviation(Program.CurrentRegis.tpr_id);
                            uiMenuBar1.LoadEnableQuestionare();
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError(this.Name, "btnCallQueue_Click", ex, false);
                        }
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = "No patient on queue!";
                        btnCallQueue.Enabled = true;
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
            btnHold.Enabled = false;

            if (CallQueue.IsStatusED()) { StatusWaitCallQueue(); return; }
            string QueueNo = Program.CurrentRegis.tpr_queue_no;
            int holdTime = PrePareData.StaticDataCls.HoldTime.Where(x => x.mhs_id == Program.CurrentSite.mhs_id).Select(x => x.holdTime).FirstOrDefault();
            QueueClass.TransactionQueueCls.PatientHoldQueue hold = new QueueClass.TransactionQueueCls().HoldQueue(Program.CurrentPatient_queue.tps_id, holdTime, Program.CurrentUser.mut_username);
            if (hold.Status == QueueClass.TransactionQueueCls.PatientHoldQueue.StatusHoldQueue.Error)
            {
                lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
                btnHold.Enabled = true;
            }
            else
            {
                StatusWaitCallQueue();
                lbAlertMsg.Text = string.Format(Program.MsgHold, hold.QueueNo);
            }
            //noina 29/01/2557 
            //clsCountDown.cancelCountDown();
            //end noina
            
            //StatusTransaction result = CallQueue.P_CallHold();
            //if (result == StatusTransaction.True)
            //{
            //    // morn clear Unit Display
            //    new ClsTCPClient().sendClearUnitDisplay();
            //    // morn clear Unit Display

            //    Program.CurrentPatient_queue = null;
            //    Program.CurrentRegis = null;
            //    StatusWaitCallQueue();
            //    btnClear_Click(null, null);
            //    CheckListAviation.Items.Clear();
            //    lbAlertMsg.Text = string.Format(Program.MsgHold, QueueNo);
            //}
            //else if (result == StatusTransaction.Error)
            //{
            //    btnHold.Enabled = true;
            //    lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
            //}
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันยกเลิกการตรวจ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    btnCancel.Enabled = false;
                    SaveData('D');
                    //morn clear Unit Display
                    new ClsTCPClient().sendClearUnitDisplay();
                    // morn clear Unit Display
                    string QueueNo = Program.CurrentRegis.tpr_queue_no;
                    Class.ClsPendingOnStation pend = new ClsPendingOnStation();
                    if (pend.pendingPatientBeforeCheckpointB())
                    {
                        lbAlertMsg.Text = string.Format(Program.MsgCancel, QueueNo);
                        StatusWaitCallQueue();
                        return;
                    }
                }
                catch
                {

                }
                lbAlertMsg.Text = "เกิดความผิดพลาดของโปรแกรม โปรด Cancel อีกครั้ง";
            }
        }
        private void addLocation()
        {
            try
            {
                DateTime datenow = Program.GetServerDateTime();

                var objlocation = (from t1 in dbc.mst_locations
                                   where datenow.Date >= t1.mlc_effective_date.Value.Date
                                   && (t1.mlc_expire_date == null ||
                                       (t1.mlc_expire_date != null && datenow.Date <= t1.mlc_expire_date.Value.Date))
                                   select new LocationRegis { strlocation = t1.mlc_ename }).ToList();
                LocationRegis newselect = new LocationRegis();
                newselect.strlocation = "- Select -";
                objlocation.Insert(0, newselect);
                DDLocation.DataSource = objlocation;
                DDLocation.DisplayMember = "strlocation";
                DDLocation.ValueMember = "strlocation";
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "addLocation", ex, false);
            }
        }
        private void frmScreeningPage_Load(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            try
            {
                uiMenuBar1.enableDashBoard();
                LoadHandlerCountDown();
                addLocation();
                EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();
                List<ObjCompany> objCom = cls.getListCompany();
                DDcompany.DataSource = objCom;
                DDcompany.ValueMember = "code";
                DDcompany.DisplayMember = "name";

                this.Text = Program.GetRoomName();
                DataGridViewButtonColumn patientAlertButton = new DataGridViewButtonColumn();
                patientAlertButton.HeaderText = "";
                patientAlertButton.Name = "btndel";
                patientAlertButton.UseColumnTextForButtonValue = true;
                patientAlertButton.Text = "ลบ";
                patientAlertButton.Width = 40;
                GridPatientAlert.Columns.Add(patientAlertButton);

                uiFooter1.RoomCode = "SC";


                this.SetComboBoxControl();
                this.Loadfrm();
                //uiFooter1.LoadData();

                if (Program.CurrentRegis != null)
                {
                    GetAviation(Program.CurrentRegis.tpr_id);
                }

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "";
                btn.Name = "Coldel";
                btn.UseColumnTextForButtonValue = true;
                btn.Text = "Delete";
                btn.Width = 70;
                Gridout_department.Columns.Add(btn);

            }
            catch 
            {
                return;
            }

            frmbg.Close();
        }
        public void Loadfrm()
        {
            if (SetTprID > 0)
            {
                 OnUCFooterClicked(SetTprID, siteitem);
                return;
            }
            if (!Program.IsDummy)
            {
                LoadHistoryCallLast();
                LoadUI();
                //Noina 
                uiMenuBar1.LoadEnableQuestionare();
            }
            if (Program.CurrentRegis != null)
            {
                    this.LoadData(Program.CurrentRegis.tpr_id);
                    RDNormal_CheckedChanged(null, null);

                //Noina
                    uiMenuBar1.LoadEnableQuestionare();
            }
            else
            {
                if (Program.IsDummy)
                {
                        btnCallQueue.Enabled = false;
                        btnHold.Enabled = false;
                        btnCancel.Enabled = false;
                        btnSendManual.Enabled = false;
                        btnSendAuto.Enabled = false;
                        btnSaveDraft.Enabled = false;

                        panel2.Enabled = false;
                        RDNormal_CheckedChanged(null, null);
                       
                        this.LoadUI();
                }
                else
                {
                    StatusWaitCallQueue();
                    btnSendManual.Enabled = false;
                    btnSendAuto.Enabled = false;
                    btnSaveDraft.Enabled = false;
                }
            }
        }
        private void LoadHistoryCallLast()
        {
            if (SetTprID != 0)
            {
                trn_patient_queue objQtxp = (from t1 in dbc.trn_patient_queues
                                             where t1.mrd_id == Program.CurrentRoom.mrd_id
                                             && t1.tpr_id == SetTprID
                                             select t1).FirstOrDefault();
                Program.CurrentPatient_queue = objQtxp;
                Program.CurrentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == SetTprID select t1).FirstOrDefault();
                if (Program.CurrentRegis != null)
                {
                    EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();

                    trn_name_check tnc = cls.getNameCheck(Program.CurrentRegis.trn_patient.tpt_hn_no);
                    if (tnc != null)
                    {
                        DDcompany.SelectedValue = tnc.trn_company_detail.tcd_code;
                        txtEmployeeID.Text = tnc.tnc_emp_id;
                    }
                }
                AlertOutDepartment.LoadTime();
            }
            else
            {
                DateTime dtnow = Program.GetServerDateTime();
                trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
                                          where t1.mrd_id == Program.CurrentRoom.mrd_id
                                                && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                                && t1.tps_status == "WK" 
                                                && t1.tps_ns_status == null
                                          orderby t1.tps_create_date ascending
                                          select t1).FirstOrDefault();
                if (objQ != null)
                {
                    Program.CurrentPatient_queue = objQ;
                    Program.CurrentRegis = objQ.trn_patient_regi;
                    uiMenuBar1.LoadEnableQuestionare();
                    StatusCallQueueReady();
                }
                else
                {
                    trn_patient_queue objReady = (from t1 in dbc.trn_patient_queues
                                              where t1.mrd_id == Program.CurrentRoom.mrd_id
                                                    && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                                   && t1.tps_status == "NS"
                                                   && t1.tps_ns_status == "WR"
                                              orderby t1.tps_create_date ascending
                                              select t1).FirstOrDefault();
                    if (objReady != null)
                    {
                        Program.CurrentPatient_queue = objReady;
                        Program.CurrentRegis = objReady.trn_patient_regi;
                        uiMenuBar1.LoadEnableQuestionare();
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        Program.CurrentPatient_queue = null;
                        Program.CurrentRegis = null;
                    }
                }
            }
         }

        private void GetAviation(int tpr_id)
        {
            try
            {
                int counttprID = (from t in dbc.trn_patient_aviations where t.tpr_id == tpr_id select t.tpr_id).Count();
                int countpatientcate = (from t in dbc.trn_patient_cats where t.tpr_id == tpr_id select t.tpr_id).Count(); //Count data in table trn_patient_cat
                DateTime datenow = Program.GetServerDateTime();

                #region PatientAviation
                if (counttprID != 0)
                {

                    var getdata = (from t1 in dbc.trn_patient_aviations
                                   join t2 in dbc.mst_avia_cat_types on t1.mav_id equals t2.mav_id
                                   join t3 in dbc.mst_aviation_types on t2.mat_id equals t3.mat_id
                                   where t1.tpr_id == tpr_id
                                   select t2).ToList();

                    if (getdata.Count > 0)
                    {
                        #region GetMacID_DisplayInCheckListAviation
                        DataTable dt = new DataTable(); DataSet ds = new DataSet();
                        dt.Columns.Add("macid");
                        ds.Tables.Add(dt);
                        foreach (var element in getdata)
                        {
                            var row = dt.NewRow();
                            row["macid"] = element.mac_id;
                            dt.Rows.Add(row);
                        }
                        for (int j = 0; j <= dt.Rows.Count - 1; j++)
                        {
                            if (CheckListAviation.Items.Count > 0)
                            {
                                switch (Convert.ToInt32(dt.Rows[j][0]))
                                {
                                    case 1: CheckListAviation.SetItemChecked(0, true); break;
                                    case 2: CheckListAviation.SetItemChecked(1, true); break;
                                    case 3: CheckListAviation.SetItemChecked(2, true); break;
                                    case 4: CheckListAviation.SetItemChecked(3, true); break;
                                    case 5: CheckListAviation.SetItemChecked(4, true); break;
                                }
                            }
                        }
                        #endregion GetMacID_DisplayInCheckListAviation

                        #region DisplayInListAviationSelect
                        ArrayList SelectedItems = new ArrayList(CheckListAviation.CheckedItems);
                        DataTable results = new DataTable(); DataSet ds2 = new DataSet();
                        for (int i2 = 0; i2 <= SelectedItems.Count - 1; i2++)
                        {
                            DataTable dt2 = new DataTable();
                            string strNameSelect = SelectedItems[i2].ToString();
                            int macid = (from t1 in dbc.mst_aviation_categories where t1.mac_ename == strNameSelect select t1.mac_id).FirstOrDefault();
                            string mactype = (from t1 in dbc.mst_aviation_categories where t1.mac_ename == strNameSelect select t1.mac_avia_type).FirstOrDefault();
                            string initial = "";

                            switch (mactype)
                            {
                                case "TH":
                                    initial = "(Thai)";
                                    break;
                                case "CN":
                                    initial = "(Canada)";
                                    break;
                                case "FA":
                                    initial = "(FAA)";
                                    break;
                                case "AS":
                                    initial = "(Australia)";
                                    break;
                                case "FD":
                                    initial = "(Flight Attendant)";
                                    break;
                            }
                            getAviationType = dbc.pw_Get_AviationType(macid).ToList();
                            dt2.Columns.Add("ID");
                            dt2.Columns.Add("Name");
                            ds2.Tables.Add(dt2);
                            foreach (var element in getAviationType)
                            {
                                var row = dt2.NewRow();
                                row["ID"] = element.mav_id;
                                row["Name"] = element.mat_ename;
                                dt2.Rows.Add(row);
                            }
                            //results = dt2.AsEnumerable().Distinct(DataRowComparer.Default).CopyToDataTable();
                            for (int j = 0; j <= dt2.Rows.Count - 1; j++)
                            {
                                ListAviationSelect.Items.Add(initial + "~" + dt2.Rows[j][1].ToString());
                                g1.Rows.Add(initial + "~" + dt2.Rows[j][1].ToString(), dt2.Rows[j][0].ToString());
                            }
                        }
                        #endregion DisplayInListAviationSelect

                        #region DisplayInListAviationFinalSelect
                        DataTable dt3 = new DataTable(); DataSet ds3 = new DataSet();
                        dt3.Columns.Add("matid");
                        dt3.Columns.Add("ename");
                        dt3.Columns.Add("mav");
                        ds3.Tables.Add(dt3);
                        foreach (var element in getdata)
                        {
                            var row = dt3.NewRow();
                            row["matid"] = element.mat_id;
                            row["ename"] = element.mst_aviation_type.mat_ename;
                            row["mav"] = element.mav_id;
                            dt3.Rows.Add(row);
                        }
                        if (dt3.Rows.Count > 0)
                        {
                            for (int cdt3 = 0; cdt3 <= dt3.Rows.Count - 1; cdt3++)
                            {
                                ListAviationFinalSelect.Items.Add(dt3.Rows[cdt3]["ename"].ToString());
                                for (int i = 0; i <= g1.Rows.Count - 1; i++)
                                {
                                    if (g1.Rows[i].Cells[1].Value.ToString() == dt3.Rows[cdt3]["mav"].ToString())
                                    {
                                        g2.Rows.Add(g1.Rows[i].Cells[0].Value.ToString(), g1.Rows[i].Cells[1].Value.ToString());
                                    }
                                }
                                for (int i = 0; i < g2.Rows.Count; i++)
                                {
                                    for (int j = 0; j < g1.Rows.Count; j++)
                                    {
                                        if (g1.Rows[j].Cells[0].Value.ToString() == g2.Rows[i].Cells[0].Value.ToString())
                                        {
                                            g1.Rows.RemoveAt(j);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion DisplayInListAviationFinalSelect
                    }
                }
                else
                {
                    if (ListAviationFinalSelect.Items.Count > 0)
                        ListAviationFinalSelect.Items.Clear();

                    if (ListAviationSelect.Items.Count > 0)
                        ListAviationSelect.Items.Clear();
                }
                #endregion

                #region DoctorCategory

                // Doc Category Master
                listBoxMasterCate.Items.Clear();
                var listPatientCate = (from t in dbc.trn_patient_cats where t.tpr_id == tpr_id select t.mdc_id).ToList();

                var listdoctorCate = (from t1 in dbc.mst_doc_categories
                                      where t1.mdc_status == 'A'
                                      && !listPatientCate.Contains(t1.mdc_id)
                                      && datenow >= t1.mdc_effective_date.Value
                                                && (t1.mdc_expire_date != null ? (datenow <= t1.mdc_expire_date.Value) : true)
                                      orderby t1.mdc_ename ascending
                                      select new { Code = t1.mdc_code, Name = t1.mdc_ename }).ToList();
                DataTable Tmptable = new DataTable(); DataSet ds_hd = new DataSet("dss");
                Tmptable.Columns.Add("ID"); Tmptable.Columns.Add("Name");
                ds_hd.Tables.Add(Tmptable);
                var item = listBoxMasterCate.Items;
                foreach (var element in listdoctorCate)
                {
                    var row = Tmptable.NewRow();
                    row["ID"] = element.Code;
                    row["Name"] = element.Name;
                    Tmptable.Rows.Add(row);
                }
                for (int i = 0; i <= Tmptable.Rows.Count - 1; i++)
                {
                    item.Add(Tmptable.Rows[i]["Name"].ToString());
                }

                if (countpatientcate != 0)
                {
                    // Doc Category Detail
                    var getdata = (from t1 in dbc.trn_patient_cats
                                   join t2 in dbc.mst_doc_categories on t1.mdc_id equals t2.mdc_id
                                   where t1.tpr_id == tpr_id
                                   select t2).ToList();
                    if (getdata.Count > 0)
                    {
                        DataTable dt = new DataTable(); DataSet ds = new DataSet();
                        dt.Columns.Add("mdcid");
                        dt.Columns.Add("ename");
                        ds.Tables.Add(dt);
                        foreach (var element in getdata)
                        {
                            var row = dt.NewRow();
                            row["mdcid"] = element.mdc_id;
                            row["ename"] = element.mdc_ename;
                            dt.Rows.Add(row);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                listBoxCateSelected.Items.Add(dt.Rows[i]["ename"].ToString());
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "GetAviation", ex, false);
            }
        }

        private void LoadUI()
        {
            uiAllLeft2.LoadDataAll();
        }
        private void LoadData(int tprid)
        {
            try
            {
                if (dbc.Connection.State == ConnectionState.Closed)
                {
                    dbc = new InhCheckupDataContext();
                }
                trn_patient_regi objpt = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1).FirstOrDefault();
                if (objpt != null)
                {
                    this.PatientBindingSource.DataSource = objpt.trn_patient;

                    GridPackage.DataSource = objpt.trn_patient_order_sets.Where(x => x.tos_status == true).Select(x => new { packName = x.tos_od_set_name }).ToList();
                    int pos = objpt.trn_patient.trn_patient_regis.IndexOf(objpt);
                    this.PatientRegisBindingSource.Position = pos;
                    Program.SetValueRadioGroup(GBQueueType, objpt.tpr_queue_type);
                    Program.SetValueRadioGroup(GBPatienttype, objpt.tpr_patient_type);

                    Program.SetValueRadioGroup(pnSite2, objpt.tpr_pe_site2);

                    string docCode = objpt.tpr_req_doc_code;
                    string docName = objpt.tpr_req_doc_name;
                    Program.SetValueRadioGroup(GBRequestPEBefore, objpt.tpr_req_pe_bef_chkup);
                    Program.SetValueRadioGroup(GBRequestPE, objpt.tpr_req_doctor);
                    if (objpt.tpr_req_doctor != 'N')
                    {
                        if (objpt.tpr_req_doc_gender == null)
                        {
                            Program.SetValueRadioGroup(GBDoctorGender, "N");
                        }
                        else
                        {
                            Program.SetValueRadioGroup(GBDoctorGender, objpt.tpr_req_doc_gender);
                        }
                        Program.SetValueRadioGroup(GBRequestDoctor, objpt.tpr_req_inorout_doctor);
                        DoctorCompleteBox.SelectedValue = docCode;
                    }

                    if (objpt.tpr_aviation_type != null)
                    {
                        GBAviationType.Enabled = true;
                        Program.SetValueRadioGroup(GBAviationType, objpt.tpr_aviation_type);
                    }
                    else
                    {
                        GBAviationType.Enabled = false;
                    }

                    Program.SetValueRadioGroup(GBPEType, objpt.tpr_pe_type);
                    Program.SetValueRadioGroup(GBNPOTime, objpt.tpr_npo_time);
                    Program.SetValueRadioGroup(GBBook, objpt.tpr_send_book);


                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    if (func.checkPassDoctor(tprid))
                    {
                        RDReuestPEBeforYES.AutoCheck = false;
                        RDReuestPEBeforNO.AutoCheck = false;
                        chkReturnScreening.AutoCheck = false;
                    }
                    else
                    {
                        RDReuestPEBeforYES.AutoCheck = true;
                        RDReuestPEBeforNO.AutoCheck = true;
                        chkReturnScreening.AutoCheck = true;
                    }
                    Program.SetValueRadioGroup(pnSite2, objpt.tpr_pe_site2);
                    chkReturnScreening.Checked = objpt.tpr_return_screening == null ? false : (bool)objpt.tpr_return_screening;

                    var objdataCBHealthCheckUPProgram = (from t1 in dbc.mst_health_checkups
                                                         where t1.mhc_id == objpt.mhc_id
                                                         select new DropdownData { Code = t1.mhc_id, Name = t1.mhc_ename }).FirstOrDefault();
                    if (objdataCBHealthCheckUPProgram != null)
                    {
                        CBHealthCheckUPProgram.SelectedIndex = CBHealthCheckUPProgram.FindString(objdataCBHealthCheckUPProgram.Name);
                    }
                    else
                    {
                        CBHealthCheckUPProgram.SelectedIndex = 0;
                    }

                    txtMainAddress.Text = objpt.tpr_main_address + " แขวง " + objpt.tpr_main_tumbon + " เขต " + objpt.tpr_main_amphur + " จังหวัด " + objpt.tpr_main_province + " " + objpt.tpr_main_zip_code;
                    var objpatient = (trn_patient)this.PatientBindingSource.Current;
                    if (objpatient.tpt_vip_hpc == true)
                        chkviphpc.Checked = true;
                    if (objpatient.tpt_vip_hpc == false)
                        chkviphpc.Checked = false;

                    ////Add health checkup program // Program.CurrentRegis.tpr_id
                    trn_patient_regi objgethealthcheckup = (from t in dbc.trn_patient_regis where t.tpr_id == tprid select t).FirstOrDefault();
                    if (objgethealthcheckup != null)
                    {
                        //txtHealthCheckup.Text = objgethealthcheckup.tpr_mhc_ename;
                        trn_patient_regi PRegis = PatientRegisBindingSource.OfType<trn_patient_regi>().FirstOrDefault();
                        if (PRegis != null)
                        {
                            string setName = PRegis.tpr_mhc_ename;
                        }
                        else
                        {
                            txtHealthCheckup.Text = "";
                        }
                        oldmhcid = Convert.ToInt32(objgethealthcheckup.mhc_id);
                       
                    }
                    else
                    {
                        //if (this.GridPackage.Rows.Count > 0)
                        //{
                        //    txtHealthCheckup.Text = GridPackage.Rows[0].Cells[0].Value.ToString();
                        //}
                        trn_patient_regi PRegis = PatientRegisBindingSource.OfType<trn_patient_regi>().FirstOrDefault();
                        if (PRegis != null)
                        {
                            string setName = PRegis.tpr_mhc_ename;
                        }
                        else
                        {
                            txtHealthCheckup.Text = "";
                        }
                    }
                    ////EndAdd health checkup program


                    //morn Company
                    if (objgethealthcheckup != null)
                    {
                        if (!string.IsNullOrEmpty(objgethealthcheckup.tpr_company_code))
                        {
                            DDcompany.SelectedValue = objgethealthcheckup.tpr_company_code;
                        }
                        else if (!string.IsNullOrEmpty(objgethealthcheckup.tpr_comp_tdesc))
                        {
                            DDcompany.Text = objgethealthcheckup.tpr_comp_tdesc;
                        }
                        else if (!string.IsNullOrEmpty(objgethealthcheckup.tpr_comp_edesc))
                        {
                            DDcompany.Text = objgethealthcheckup.tpr_comp_edesc;
                        }
                        else
                        {
                            EmrClass.GetDataToDoListCls tCls = new EmrClass.GetDataToDoListCls();
                            trn_name_check tnc = tCls.getNameCheck(objgethealthcheckup.trn_patient.tpt_hn_no);
                            if (tnc != null)
                            {
                                DDcompany.SelectedValue = tnc.trn_company_detail.tcd_code;
                                txtEmployeeID.Text = tnc.tnc_emp_id;
                            }
                        }


                        if (!string.IsNullOrEmpty(objgethealthcheckup.tpr_comp_dep_tdesc))
                        {
                            DDDepartment.Text = objgethealthcheckup.tpr_comp_dep_tdesc;
                        }

                        if (objgethealthcheckup.tpr_req_same_doc == true)
                        {
                            chkReqSameDR.Checked = true;
                        }
                        else
                        {
                            chkReqSameDR.Checked = false;
                        }
                    }

                }

                if (PastedPERoom(tprid))
                {
                    GBRequestPEBefore.Enabled = false;
                }   
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadData", ex, false);
            }
        }
        private string getstring(object bx)
        {
            if (bx != null)
            {
                return bx.ToString();
            }
            else
            {
                return "";
            }
        }
        private void SetComboBoxControl()
        {
            try
            {
                DateTime datenow = Program.GetServerDateTime();
                int mdcid = (from t in dbc.trn_patient_cats where t.tpr_id == SetTprID select t.mdc_id).FirstOrDefault();
                var mreport = (from t1 in dbc.mst_reports
                               where t1.mrt_status == 'A'
                               && (from t2 in t1.mst_report_matches where t2.mst_report_grp.mrm_id == Program.CurrentRoom.mrm_id select t2).Count() > 0
                               && datenow >= t1.mrt_effective_date.Value
                               && (t1.mrt_expire_date != null ? (datenow <= t1.mrt_expire_date.Value) : true)
                               orderby t1.mrt_report_seq
                               select new {t1.mrt_code , t1.mrt_ename}).ToList();

                CheckConsentForm.DataSource = mreport;
                CheckConsentForm.DisplayMember = "mrt_ename";
                CheckConsentForm.ValueMember = "mrt_code";
               
                var objmst_health_checkup = (from t1 in dbc.mst_health_checkups
                                             where t1.mhc_status == 'A'
                                             && datenow >= t1.mhc_effective_date.Value
                                             && (t1.mhc_expire_date != null ? (datenow <= t1.mhc_expire_date.Value) : true)
                                             select new DropdownData { Code = t1.mhc_id, Name = t1.mhc_ename }).ToList();
                DropdownData newselect = new DropdownData();
                newselect.Code = null;
                newselect.Name = "- Select -";
                objmst_health_checkup.Insert(0, newselect);
                CBHealthCheckUPProgram.DataSource = objmst_health_checkup;
                CBHealthCheckUPProgram.ValueMember = "Code";
                CBHealthCheckUPProgram.DisplayMember = "Name";
                if (objmst_health_checkup.Count > 0)
                {
                    CBHealthCheckUPProgram.SelectedIndex = 0;
                }

                
                var listdoctorCate = (from t1 in dbc.mst_doc_categories
                                     where t1.mdc_status == 'A'
                                     //&& !listPatientCate.Contains(t1.mdc_id)
                                     && datenow >= t1.mdc_effective_date.Value
                                               && (t1.mdc_expire_date != null ? (datenow <= t1.mdc_expire_date.Value) : true)
                                     orderby t1.mdc_ename ascending
                                     select new  { Code = t1.mdc_code, Name = t1.mdc_ename }).ToList();
                DataTable Tmptable = new DataTable(); DataSet ds = new DataSet("dss");
                Tmptable.Columns.Add("ID"); Tmptable.Columns.Add("Name");
                ds.Tables.Add(Tmptable);
                var item = listBoxMasterCate.Items;
                foreach (var element in listdoctorCate)
                {
                    var row = Tmptable.NewRow();
                    row["ID"] = element.Code;
                    row["Name"] = element.Name;
                    Tmptable.Rows.Add(row);
                }
                for (int i = 0; i <= Tmptable.Rows.Count - 1; i++)
                {

                    item.Add(Tmptable.Rows[i]["Name"].ToString());
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetComboBoxControl", ex, false);
            }

        }
        private void CheckPendding()
        {
            if (Program.CurrentRegis == null)
            {
                return;
            }
            try
            {
                var objpendding = (from t1 in dbc.trn_patient_pendings
                                   where t1.tpr_id == Program.CurrentRegis.tpr_id
                                   && t1.trn_patient_regi.tpr_status == "PD"
                                   select t1).Count();
                if (objpendding > 0)
                {
                    ChPendingData.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "CheckPendding", ex, false);
                throw;
            }
        }

        private void btnOutDepartment_Click(object sender, EventArgs e)
        {
            if (!checkAddOutDepartment())
            {
                return;
            }
            try
            {
                trnoutdepartmentsBindingSource.AddNew();
                trn_out_department newitem = (trn_out_department)trnoutdepartmentsBindingSource.Current;
                if (SetTprID == 0)
                {
                    newitem.tpr_id = Program.CurrentRegis.tpr_id;
                }
                else
                {
                    newitem.tpr_id = SetTprID;
                }
                newitem.tod_desc = txtDescription.Text.Trim();
                newitem.tod_location = DDLocation.Text; //txtLocation.Text.Trim();
                try
                {
                    newitem.tod_start_date = Convert.ToDateTime(txtTime.Text.Trim());
                }
                catch (Exception)
                {
                    newitem.tod_start_date = null;
                    throw;
                }
                newitem.tod_create_by = Program.CurrentUser.mut_id;
                newitem.tod_create_date = Program.GetServerDateTime();
                newitem.tod_update_by = newitem.tod_create_by;
                newitem.tod_update_date = newitem.tod_create_date;
                txtDescription.Text = "";
                DDLocation.Text = "";
                txtTime.Text = "";
                dbc.trn_out_departments.InsertOnSubmit(newitem);
                dbc.SubmitChanges();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnOutDepartment_Click", ex, false);
                throw;
            }
        }
        private bool checkAddOutDepartment()
        {
            DateTime dnow;
            bool istime = false;
            try
            {
                dnow = Convert.ToDateTime(txtTime.Text.Trim());
                istime = true;
            }
            catch (Exception)
            {
            }
            lbAlertMsg.Text = "";
            if (txtDescription.Text.Trim() == "" || DDLocation.Text == "")
            {
                lbAlertMsg.Text = "Please input data require.";
                return false;
            }
            if (istime == false)
            {
                lbAlertMsg.Text = "Input time : Incorrect Ex. 13:30";
                return false;
            }
            return true;
        }

        private void RDrequestPE_CheckedChanged(object sender, EventArgs e)
        {
            if (RDrequestPE.Checked)
            {
                GBRequestDoctor.Enabled = false;
                GBDoctorGender.Enabled = false;
                RDRequestDoctorin.Checked = false;
                RDDoctorOutlet.Checked = false;
                RDNA.Checked = false;
                RDDoctorIsMen.Checked = false;
                RDdoctorfemale.Checked = false;
                DoctorCompleteBox.Enabled = false;
                DoctorCompleteBox.SelectedValue = null;

            }
            else
            {
                GBRequestDoctor.Enabled = true;
                RDRequestDoctorin.Checked = true;
                RDNA.Checked = true;
                GBDoctorGender.Enabled = true;
                DoctorCompleteBox.Enabled = true;
               
            }
        }
        private void RDPatientType_Corporate_CheckedChanged(object sender, EventArgs e)
        {

        }
        

        private void GridPatientAlert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                DataGridViewRow dr = GridPatientAlert.CurrentRow;
                GridPatientAlert.Rows.Remove(dr);
            }
        }
        private void GridPackage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                DataGridViewRow dr = GridPackage.CurrentRow;
                GridPackage.Rows.Remove(dr);
            }
        }
        private void btnAddPatientAlert_Click(object sender, EventArgs e)
        {
                 GridPatientAlertSearch.Visible = false;
            if (txtPatientAlert.Text.Trim().Length == 0)
            {
               return;
            }

            var objregcurrent = (trn_patient_regi)this.PatientRegisBindingSource.Current;
            if (objregcurrent != null)
            {
                trn_patient_alert newitem = (trn_patient_alert)this.PatientAlertBindingSource.AddNew();
                newitem.tpt_id = objregcurrent.tpt_id;
                newitem.mut_id = Program.CurrentUser.mut_id;
                newitem.tpa_alert = txtPatientAlert.Text.Trim();
                newitem.tpa_status = 'A';
                newitem.tpa_create_by = Program.CurrentUser.mut_username;
                newitem.tpa_create_date = Program.GetServerDateTime();
                newitem.tpa_update_by = Program.CurrentUser.mut_username;
                newitem.tpa_update_date = newitem.tpa_create_date;
                //newitem.trn_patient = objregcurrent.trn_patient;
                txtPatientAlert.Text = "";
                GridPatientAlertSearch.Visible = false;
            }
        }

        private void StatusWaitCallQueue()
        {
            //เลือกรายการแรก
            btnReady.Enabled = false;
            RDReuestPEBeforNO.Checked = true;
            RDPatientType_General.Checked = true;
            RDqueueTypeVIPAppoint.Checked = true;
            RDAviationCategoryNewcase.Checked = true;
            RDrequestPE.Checked = true;
            RDRequestDoctorin.Checked = true;
            //RDDoctorIsMen.Checked = true;
            RDNA.Checked = true;
            rdWaitPE.Checked = true;
            RDNPOTimeNo.Checked = true;
            RDBookInday.Checked = true;
            //ไม่เลือก
            ChPendingData.Checked = false;
            RDaviationTypeFollowup.Checked = false;
            panel2.Enabled = false;
            txtMainAddress.Text = "";
            txtHealthCheckup.Text = "";
            GridPackage.DataSource = null;

            chkReturnScreening.Enabled = false;
            chkReturnScreening.Checked = false;
            chsameMainAddress.Checked = false;

            Program.CurrentRegis = null;
            Program.CurrentPatient_queue = null;

            dbc.Dispose();
            dbc = new InhCheckupDataContext();

            trn_patient objhdr = new trn_patient();
            PatientBindingSource.DataSource = objhdr;

            btnCallQueue.Enabled = true;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;
            btnSendManual.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnprintslip.Enabled = false;
            ////Added:Akkaradech
            btnselectFinal.Enabled = false;
            btnClear.Enabled = false;
            btnmove.Enabled = false;
            if (ListAviationFinalSelect.Items.Count > 0)
                ListAviationFinalSelect.Items.Clear();

            if (ListAviationSelect.Items.Count > 0)
                ListAviationSelect.Items.Clear();

            this.LoadUI();
           
        }
        private void StatusCallQueue()
        {//เช็คว่าเป็นการเปิดดูรายการอย่างเดียวหรือไม่ SetTprID != null คือ ดูรายการอย่างเดียวแก้ไขไม่ได้
            if (SetTprID == 0)
            {
                btnCallQueue.Enabled = false;
                btnHold.Enabled = true;
                btnCancel.Enabled = true;

                if (PastedPERoom(Program.CurrentRegis.tpr_id))
                {
                    btnSendManual.Enabled = false;
                }
                else
                {
                    btnSendManual.Enabled = true;
                }

                btnSendAuto.Enabled = true;
                btnSaveDraft.Enabled = true;
                btnprintslip.Enabled = true;
                panel2.Enabled = true;
                this.LoadUI();
            }
            else
            {
                btnCallQueue.Enabled = false;
                btnHold.Enabled = false;
                btnCancel.Enabled = false;
                btnSendManual.Enabled = false;
                btnSendAuto.Enabled = false;
                btnSaveDraft.Enabled = false;
                btnprintslip.Enabled = false;
                panel2.Enabled = false;
            }
        }
        private void StatusSaveData()
        {
            btnReady.Enabled = false;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            btnCancel.Enabled = true;
            btnSendManual.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSaveDraft.Enabled = true;
            btnprintslip.Enabled = true;
            this.LoadUI();
            ////Added:Akkaradech
            btnselectFinal.Enabled = false;
            btnClear.Enabled = false;
            btnmove.Enabled = false;
        }

        private List<TmpOrderItem> itemPackageList = new List<TmpOrderItem>();
        //Save Data
        private void disableBtnWhenSave()
        {
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendManual.Enabled = false;
        }
        private void enableBtnWhenSave()
        {
            btnSaveDraft.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendManual.Enabled = true;
        }
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            frmBGScreen frmbg = new frmBGScreen(); 
            frmbg.Show(); 
            Application.DoEvents();

            btnSendAuto.Enabled = false;
            if (SaveData('N'))
            {
                // morn clear Unit Display
                new ClsTCPClient().sendClearUnitDisplay();
                // morn clear Unit Display
                btnSendA_Click(null, null);
                btnClear_Click(null, null);
                CheckListAviation.Items.Clear();
                listBoxCateSelected.Items.Clear();
            }
            else
            {
                enableBtnWhenSave();
            }
            frmbg.Close();


        }
        private void btnSaveDraft_Click(object sender, EventArgs e)
        {

            frmBGScreen frmbg = new frmBGScreen(); 
            frmbg.Show(); 
            Application.DoEvents();

            btnSaveDraft.Enabled = false;
          
            int tpr_id = 0;
            if (Program.CurrentRegis != null)
            {
                tpr_id = Program.CurrentRegis.tpr_id;
            }
            else
            {
                tpr_id = SetTprID;
            }
            if (Program.chkBookComplete(tpr_id))
            {
                if (SaveData('D') && siteitem == 0)
                {

                }
                btnSaveDraft.Enabled = true;
                lbAlertMsg.Focus();
            }
            frmbg.Close();

        }
        private Boolean btnSendM_Click()
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            if (CallQueue.IsStatusED()) { StatusWaitCallQueue(); return false; }
            int mrmid = 0;//ห้องที่ต้องการส่งต่อไป
            int mvtid = 0;
            Boolean saveIsCompleted = false;
            try
            {
                var mrm_id = mst.GetMstRoomHdr("DC").mrm_id;
                var mvt_id = mst.GetMstEvent("PE").mvt_id;
                var objqueues = (from t1 in dbc.trn_patient_queues
                                 where t1.mvt_id == mvt_id
                                     && t1.mrm_id == mrm_id
                                     && t1.tpr_id==Program.CurrentRegis.tpr_id
                                 select t1).Count();
                if (objqueues == 0)
                {
                    mrmid = mst.GetMstRoomHdr("CB").mrm_id;
                    mvtid = mst.GetMstEvent("CB").mvt_id;
                    
                }
                else
                {
                    mrmid = mst.GetMstRoomHdr("CB").mrm_id;
                    mvtid = mst.GetMstEvent("CB").mvt_id;
                }
                CallQueue.Getmrmid = mrmid;
                CallQueue.Getmvtid = mvtid;
                CallQueue.Gettprid = Program.CurrentRegis.tpr_id;

                DateTime dtnow = Program.GetServerDateTime();
                trn_patient_queue CurrentQueue = (from t1 in dbc.trn_patient_queues
                                                  where t1.tps_id == Program.CurrentPatient_queue.tps_id
                                                  select t1).FirstOrDefault();

                CurrentQueue.mrd_id = Program.CurrentRoom.mrd_id;
                CurrentQueue.tps_end_date = dtnow;
                CurrentQueue.tps_send_by = Program.CurrentUser.mut_username;
                CurrentQueue.tps_update_date = dtnow;
                CurrentQueue.tps_status = "ED";

                trn_patient_queue newitem = new trn_patient_queue();
                newitem.tpr_id = Program.CurrentRegis.tpr_id;
                newitem.mvt_id = mvtid;
                newitem.mst_room_hdr = dbc.mst_room_hdrs.Where(x => x.mrm_id == mrmid).FirstOrDefault();
                newitem.mrm_id = mrmid;
                newitem.mrd_id = null;
                newitem.tps_start_date = null;
                newitem.tps_end_date = null;
                newitem.tps_status = "NS";
                newitem.tps_ns_status = "QL";
                newitem.tps_create_by = Program.CurrentUser.mut_username;
                newitem.tps_create_date = dtnow;
                newitem.tps_update_by = newitem.tps_create_by;
                newitem.tps_update_date = newitem.tps_create_date;
                dbc.trn_patient_queues.InsertOnSubmit(newitem);

                dbc.SubmitChanges();
                saveIsCompleted = true;
             
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSendM_Click", ex, false);
            }
            if (saveIsCompleted)
            {
                StatusWaitCallQueue();
                lbAlertMsg.Text = CallQueue.GetStrSaveAndSend();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnSendManual_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            frmBGScreen frmbg = new frmBGScreen(); 
            frmbg.Show(); 
            Application.DoEvents();

            btnSendManual.Enabled = false; Application.DoEvents();

            if (SaveData('N'))
            {
                DateTime datenowvalue = Program.GetServerDateTime();
                if (btnSendM_Click() == false)
                {
                    enableBtnWhenSave();
                    btnSendManual.Enabled = true;
                    frmbg.Close();
                    return;
                }
                else
                {
                    enableBtnWhenSave();
                    frmbg.Close();
                    return;
                }
            }
            else
            {
                enableBtnWhenSave();
            }
            frmbg.Close();
        }
        private bool SaveData(char chTypecode)
        {
            label18.ForeColor = Color.Black;
            if (Program.GetValueRadioTochar(GBPatienttype) == '2' || Program.GetValueRadioTochar(GBPatienttype) == '4')
            {
                if (RDPatientType_Aviation.Checked == true)
                {
                    if (ListAviationFinalSelect.Items.Count == 0)
                    {
                        lbAlertMsg.Focus();
                        lbAlertMsg.Text = "Please select Aviation";
                    }
                }
            }

            TimeSpan interval;
            bool isconvert = true;
            try
            {
                if (txtnpotimeRemark.Text.Trim() == ":")
                {
                    interval = TimeSpan.Parse("00:00");
                }
                else
                {
                    interval = TimeSpan.Parse(txtnpotimeRemark.Text.Trim());
                }
            }
            catch (FormatException)
            {
                isconvert = true;
            }
            catch (OverflowException)
            {
                isconvert = false;
            }

            if (RDnpotimeYes.Checked && (txtnpotimeRemark.Text.Trim().Length < 5 || isconvert==false))
            {
                lbAlertMsg.Text = "NPO Time incorrect.";
                lbAlertMsg.Focus();
                return false ;
            }
            if (RDPatientType_Aviation.Checked == true && g2.Rows.Count == 0)
            {
                lbAlertMsg.Text = "Please select Aviation Category";
                lbAlertMsg.Focus();
                return false;
            }
            if (Program.CurrentSite.mhs_extra_pe_type == true)
            {
                if (!rdPEWaiting.Checked && !rdPENotWaiting.Checked)
                {
                    lbAlertMsg.Text = "Please select PE Type.";
                    lbAlertMsg.Focus();
                    return false;
                }
            }

            if (DrequestPE_yes.Checked == true) //ไม่ได้เลือก No แสดงว่าเลือก Yes อยู่
            {
                if (RDDoctorOutlet.Checked)
                {
                    if (DoctorCompleteBox.SelectedValue == null)
                    {
                        lbAlertMsg.Text = "Please required specify a doctor name.";
                        lbAlertMsg.Focus();
                        return false;
                    }
                }
                else if (RDRequestDoctorin.Checked)
                {
                    if (DoctorCompleteBox.SelectedValue == null && !RDDoctorIsMen.Checked && !RDdoctorfemale.Checked)
                    {
                        lbAlertMsg.Text = "Please required specify a doctor's name or gender of doctor.";
                        lbAlertMsg.Focus();
                        return false;
                    }
                }
            }


            DateTime datenowvalue = Program.GetServerDateTime();

            try
            {
                Boolean saveIsCompleted = false;
                var objregis = (trn_patient_regi)this.PatientRegisBindingSource.Current;
                var objpatient = (trn_patient)this.PatientBindingSource.Current;
                foreach (var source in this.PatientAlertBindingSource)
                {
                    trn_patient_alert alert = (trn_patient_alert)source;
                    objpatient.trn_patient_alerts.Add(alert);
                }
                var QueueTypevalue = Program.GetValueRadio(GBQueueType);

                if (objregis != null)
                {
                    objregis.tpr_type = chTypecode;
                    objregis.tpr_arrive_type = this.GetArriveType();

                    if (CBAviationCategory.Enabled == true && CBAviationCategory.SelectedValue != null)
                    {
                        objregis.mac_id = Program.GetValueComboBoxInt(CBAviationCategory);// Convert1.ToInt32(CBAviationCategory.SelectedValue);
                    }
                    else
                    {
                        objregis.mac_id = null;
                    }
                    if (CBDoctorCategory.Enabled == true && CBDoctorCategory.SelectedValue != null)
                    {
                        objregis.mdc_id = Program.GetValueComboBoxInt(CBDoctorCategory); //Convert1.ToInt32(CBDoctorCategory.SelectedValue);
                    }
                    else
                    {
                        objregis.mdc_id = null;
                    }
                    if (mhcid != 0)
                    {
                        objregis.mhc_id = mhcid;
                        objregis.tpr_mhc_ename = txtHealthCheckup.Text;
                    }
                    if (mhcid == 0)
                    {
                        objregis.mhc_id = null;
                        objregis.tpr_mhc_ename = txtHealthCheckup.Text;
                    }
                    objregis.tpr_queue_type = Program.GetValueRadioTochar(GBQueueType);
                    objregis.tpr_patient_type = Program.GetValueRadioTochar(GBPatienttype);
                    objregis.tpr_req_pe_bef_chkup = Program.GetValueRadioTochar(GBRequestPEBefore);
                    objregis.tpr_req_doctor = Program.GetValueRadioTochar(GBRequestPE);

                    if (chTypecode == 'N')
                    {
                        objregis.tpr_return_screening = chkReturnScreening.Checked;
                        objregis.tpr_nurse_code = Program.CurrentUser.mut_username;
                        objregis.tpr_nurse_name = Program.CurrentUser.mut_fullname;
                    }

                    //if (SetTprID != 0) // Check Click from 1.6 morn
                    //{
                    //    string old_doc_code = objregis.tpr_pe_doc_code;
                    //    string new_doc_code = DoctorCompleteBox.SelectedValue == null ? null : DoctorCompleteBox.SelectedValue.ToString();
                    //    StatusTransaction passPE = new Class.FunctionDataCls().checkPassPE(SetTprID);
                    //    if (passPE == StatusTransaction.False)
                    //    {
                    //        objregis.tpr_pe_doc_code = new_doc_code;
                    //        objregis.tpr_pe_doc_name = DoctorCompleteBox.SelectedValue == null ? null : DoctorCompleteBox.Text;
                    //    }
                    //}
                    objregis.tpr_req_doc_code = DoctorCompleteBox.SelectedValue == null ? null : DoctorCompleteBox.SelectedValue.ToString();
                    objregis.tpr_req_doc_name = DoctorCompleteBox.SelectedValue == null ? null : DoctorCompleteBox.Text;
                    if (objregis.tpr_req_doctor == 'N')
                    {
                        objregis.tpr_req_doc_gender = null;
                        objregis.tpr_req_inorout_doctor = string.Empty;
                        objregis.tpr_req_doc_name = string.Empty;
                    }
                    else
                    {
                        if (Program.GetValueRadioTochar(GBDoctorGender) != null)
                            // new code  for fix  could not use check box N/A  by artist '3/9/2015
                            
                            if (Program.GetValueRadioTochar(GBDoctorGender) != 'N')
                            {
                                objregis.tpr_req_doc_gender = Program.GetValueRadioTochar(GBDoctorGender);
                            }
                            else {
                                objregis.tpr_req_doc_gender = null;
                            }

                        //if (Program.GetValueRadioTochar(GBDoctorGender) != 'N')     // old code
                        // objregis.tpr_req_doc_gender = Program.GetValueRadioTochar(GBDoctorGender); // old code
                     
                        //objregis.tpr_req_doc_gender = Program.GetValueRadioTochar(GBDoctorGender);
                        objregis.tpr_req_inorout_doctor = Program.GetValueRadio(GBRequestDoctor);
                    }


                    objregis.tpr_pe_type = Program.GetValueRadioTochar(GBPEType);
                    objregis.tpr_pe_site2 = Program.GetValueRadioTochar(pnSite2);
                    objregis.tpr_npo_time = Program.GetValueRadioTochar(GBNPOTime);
                    objregis.tpr_send_book = Program.GetValueRadioTochar(GBBook);
                    objregis.tpr_send_to = Program.GetValueRadioTochar(panelBookSendTo);
                    if (objregis.tpr_create_by == null)
                    {
                        objregis.tpr_create_date = datenowvalue;
                        objregis.tpr_create_by = Program.CurrentUser.mut_username;
                    }
                    objregis.tpr_update_date = objregis.tpr_create_date;
                    objregis.tpr_update_by = Program.CurrentUser.mut_username;
                    objregis.tpr_check_pending = (ChPendingData.Checked) ? 'Y' : 'N';// check Pending

                    objregis.tpr_req_same_doc = chkReqSameDR.Checked;

                    //morn new field for company
                    EmrClass.GetDataToDoListCls tCls = new EmrClass.GetDataToDoListCls();
                    if (DDcompany.SelectedValue != null)
                    {
                        trn_company_detail tcd = tCls.getCompanyDetail(DDcompany.SelectedValue.ToString());
                        if (tcd != null)
                        {
                            objregis.tcd_id = tcd.tcd_id;
                            objregis.tpr_company_code = tcd.tcd_code;
                            objregis.tpr_comp_tdesc = tcd.tcd_tname;
                            objregis.tpr_comp_edesc = tcd.tcd_ename;
                            objregis.tpr_comp_dep_tdesc = DDDepartment.Text;
                        }
                    }
                    //end morn

                }
                if(objpatient != null)
                    objpatient.tpt_vip_hpc = (chkviphpc.Checked) ? true : false;//Added.Akkaradech on 2013-12-24{viphpc}

                StatusTransaction getAviaCate = getAviationCate(ref objregis);
                StatusTransaction getPTCate = getPatientCate(ref objregis);

                // For HPC Site 2
                if (Program.CurrentSite.mhs_extra_pe_type == true)
                {
                    if (objregis.tpr_pe_site2 == 'P')
                    {
                        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                        mst_event mvt = mst.GetMstEvent("ES");
                        List<trn_patient_plan> planEST = objregis.trn_patient_plans.Where(x => x.mvt_id == mvt.mvt_id).ToList();
                        if (planEST == null || planEST.Count == 0)
                        {
                            int mvt_pe = dbc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                            List<trn_patient_plan> planPE = objregis.trn_patient_plans.Where(x => x.mvt_id == mvt_pe).ToList();
                            planPE.ForEach(x => objregis.trn_patient_plans.Remove(x));
                        }
                    }
                    else if (objregis.tpr_pe_site2 == 'N')
                    {
                        EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                        mst_event mvt = mst.GetMstEvent("PE");
                        List<trn_patient_plan> planPE = objregis.trn_patient_plans.Where(x => x.mvt_id == mvt.mvt_id).ToList();
                        if (planPE == null || planPE.Count == 0)
                        {
                            trn_patient_plan newPlanPE = new trn_patient_plan
                            {
                                mvt_id = mvt.mvt_id,
                                tpl_use_pac = false,
                                tpl_status = 'N',
                                tpl_by = 'A',
                                tpl_new = false
                            };
                            objregis.trn_patient_plans.Add(newPlanPE);
                        }
                    }
                }

                EmrClass.GetPTPackageCls PackageCls = new EmrClass.GetPTPackageCls();
                PackageCls.skipReqDoctorOutDepartment(ref objregis);
                PackageCls.CompleteEcho(ref objregis);
                PackageCls.skipChangeEstToEcho(ref objregis, objregis.mhs_id);
                PackageCls.checkOrderPMR(ref objregis, objregis.mhs_id);

                try
                {
                    PatientBindingSource.EndEdit();
                    try
                    {
                        dbc.SubmitChanges();
                    }
                    catch (System.Data.Linq.ChangeConflictException)
                    {
                        foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                        {
                            dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                        }
                        dbc.SubmitChanges();
                    }
                    bool setRelationOrder = new EmrClass.GetPTPackageCls().setRelationOrderSet(ref objregis);
                    if (setRelationOrder)
                    {
                        dbc.SubmitChanges();
                    }
                    saveIsCompleted = true;

                }
                catch (Exception ex)
                {
                    Program.MessageError("frmScreeningPage", "SaveData", ex, false);
                }
                if (saveIsCompleted == true)
                {
                    lbAlertMsg.Text = "Save data completed.";
                    uiMenuBar1.LoadEnableQuestionare();
                }
                lbAlertMsg.Focus();
                return saveIsCompleted;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SaveData", ex, false);
                lbAlertMsg.Focus();
                return false;
            }
        }

        private char GetArriveType()
        {
            string strtype = Program.GetValueRadio(GBQueueType);
            if (strtype == "2" && strtype == "4")
            {
                return 'A';
            }
            else
            {
                return 'W';
            }
            
        }

        private void txtOtherTumbon_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtOtherAumpher.Top-1;
            GridOtherAddress.Left = txtOtherAddress.Left;
            GridOtherAddress.Width = GridPatientAlert.Width;
            GridOtherAddress.Height = 165;
            SearchOtherAddress(txtOtherTumbon.Text.Trim());
        }
        private void txtOtherAumpher_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtmobilePhone.Top-1;
            GridOtherAddress.Left = txtmobilePhone.Left;
            GridOtherAddress.Width = GridPatientAlert.Width;
            GridOtherAddress.Height = 165;
            SearchOtherAddress(txtOtherAumpher.Text.Trim());
        }
        private void txtOtherProvice_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtmobilePhone.Top - 1;
            GridOtherAddress.Left = txtmobilePhone.Left;
            GridOtherAddress.Width = GridPatientAlert.Width;
            GridOtherAddress.Height = 165;
            SearchOtherAddress(txtOtherProvice.Text.Trim());
        }
        private void txtOtherPostCode_KeyUp(object sender, KeyEventArgs e)
        {

        }

       private void txtPatientAlert_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString().Length >1)
            {
                GridPatientAlertSearch.Top = GridPatientAlert.Top-1;
                GridPatientAlertSearch.Left = GridPatientAlert.Left;
                GridPatientAlertSearch.Width = GridPatientAlert.Width;
                GridPatientAlertSearch.Height = 97;
                GridPatientAlertSearch.Visible = true;
                var objsearchData = (from t1 in dbc.trn_patient_alerts
                                     where t1.tpa_alert.Contains(txtPatientAlert.Text.Trim())
                                     orderby t1.tpa_alert
                                     select new { MessageAlert = t1.tpa_alert }).ToList();
                if (objsearchData.Count > 0)
                {
                    GridPatientAlertSearch.DataSource = objsearchData.DistinctBy(x => x.MessageAlert).ToList();
                }
                else
                {
                    GridPatientAlertSearch.Visible = false;
                }
            }
            else if (e.KeyValue.ToString().Length <2)
            {
                GridPatientAlertSearch.Visible = false;
            }
        }

        private void SearchOtherAddress(string strtext)
        {
            if (strtext.Length > 1)
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    DataTable dt = ws.ListAddress(strtext);
                    GridOtherAddress.DataSource = dt;
                    if (GridOtherAddress.Rows.Count > 0)
                    {
                        GridOtherAddress.Visible = true;
                    }
                    else
                    {
                        GridOtherAddress.Visible = false;
                    }
                }
            }
        }

        private void Gridout_department_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < Gridout_department.Rows.Count; i++)
            {
                Gridout_department["colNo",i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void GridOtherAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridOtherAddress.CurrentRow != null)
            {
                DataGridViewRow dr = GridOtherAddress.CurrentRow;
                trn_patient_regi pr = (trn_patient_regi)PatientRegisBindingSource.Current;
                pr.tpr_other_tumbon = dr.Cells[4].Value.ToString();
                pr.tpr_other_amphur = dr.Cells[3].Value.ToString();
                pr.tpr_other_province = dr.Cells[1].Value.ToString();
                pr.tpr_other_zip_code = dr.Cells[0].Value.ToString();
                GridOtherAddress.Visible = false;
                txtOtherAumpher.Focus();
            }
        }
        private void GridPatientAlertSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPatientAlert.Text = GridPatientAlertSearch[0, e.RowIndex].Value.ToString();
            GridPatientAlertSearch.Visible = false;
            GridPatientAlert.Focus();
        }

        private void btnloadPackageTK_Click(object sender, EventArgs e)
        {
            try
            {
                trn_patient_regi tpr = (trn_patient_regi)PatientRegisBindingSource.Current;
                DateTime dateNow = Program.GetServerDateTime();

                int enRowID = Convert.ToInt32(tpr.tpr_en_rowid);
                EmrClass.GetPTPackageCls PackageCls = new EmrClass.GetPTPackageCls();
                EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
                PackageCls.AddPatientOrderItem(ref tpr, "System", dateNow, getPTPackage);
                PackageCls.AddPatientOrderSet(ref tpr, "System", dateNow, getPTPackage);
                List<MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                PackageCls.AddPatientEvent(ref tpr, "System", dateNow, mapOrder);
                PackageCls.AddPatientPlan(ref tpr, "System", dateNow, mapOrder);

                //EmrClass.GetDataFromWSTrakCare getWs = new EmrClass.GetDataFromWSTrakCare();
                //StatusTransaction getPackage = getWs.GetPatientPackage(ref tpr, dateNow);
                //GridPackage.DataSource = tpr.trn_patient_order_sets.Where(x => x.tos_status == true).ToList();
                //if (getPackage == StatusTransaction.True)
                //{
                //    StatusTransaction genPlan = getWs.genPatientPlan(ref tpr, Program.CurrentSite.mhs_id, dateNow);
                //    if (genPlan == StatusTransaction.True)
                //    {

                //    }
                //}
                //else
                //{

                //}

                GridPackage.AutoGenerateColumns = false;
                GridPackage.DataSource = tpr.trn_patient_order_sets.Where(x => x.tos_status == true).Select(x => new { tos_od_set_name = x.tos_od_set_name }).ToList();
                if (tpr.trn_patient_order_sets.Count > 0)
                {
                    txtHealthCheckup.Text = tpr.trn_patient_order_sets.FirstOrDefault().tos_od_set_name;
                }
                trn_patient_regi PRegis = PatientRegisBindingSource.OfType<trn_patient_regi>().FirstOrDefault();
                if (PRegis != null)
                {
                    string setName = PRegis.tpr_mhc_ename;
                }
                else
                {
                    txtHealthCheckup.Text = "";
                }
            }
            catch
            {

            }
        }
        private void txtOtherAddress_Click(object sender, EventArgs e)
        {
            if (GridOtherAddress.Visible == true) { GridOtherAddress.Visible = false; }
        }

        private void RDPatientType_walkin_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void RDBookwantResult_CheckedChanged(object sender, EventArgs e)
        {
            if (RDPatientType_Corporate.Checked)
            {
                panelBookSendTo.Enabled = true;
                if (RDBookwantResult.Checked)
                {
                    panelBookSendTo.Visible = true;
                    RDBookSendToPN.Checked = true;
                }
                else
                {
                    panelBookSendTo.Visible = false;
                }
            }
            else
            {
                panelBookSendTo.Enabled = false;
                if (RDBookwantResult.Checked)
                {
                    panelBookSendTo.Visible = true;
                    RDBookSendToPN.Checked = true;
                }
                else
                {
                    panelBookSendTo.Visible = false;
                }
            }
        }
        private void RDDoctorOutlet_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void RDnpotimeYes_CheckedChanged(object sender, EventArgs e)
        {
            if (RDnpotimeYes.Checked)
            {
                txtnpotimeRemark.Enabled = true;
            }
            else
            {
                txtnpotimeRemark.Text = "";
                txtnpotimeRemark.Enabled = false;
            }
        }
        private void RDNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                CheckConsenFrom(Program.CurrentRegis.tpr_id);
            }
            else
            {
                CheckConsenFrom(SetTprID);
            }
        }
        private void CheckConsenFrom(int tprid)
        {
            if (tprid != 0)
            {
                var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1).FirstOrDefault();
                if (currentRegis != null)
                {
                    trn_patient objtrn_patient = (from t1 in dbc.trn_patients where t1.tpt_id == currentRegis.tpt_id select t1).FirstOrDefault();

                    int index = 0;
                    if (RDNormal.Checked)
                    {
                        for (index = 0; index < CheckConsentForm.Items.Count; index++)
                        {
                            string valuecheckbox = Program.GetCheckedListBoxValue(index, CheckConsentForm).ToString();
                            if (objtrn_patient.tpt_nation_code == "TH")
                            {
                                if (valuecheckbox == "RG103")
                                { CheckConsentForm.SetItemChecked(index, true); }
                                else
                                {
                                    CheckConsentForm.SetItemChecked(index, false);
                                }
                            }
                            else
                            {
                                if (valuecheckbox == "RG104")
                                { CheckConsentForm.SetItemChecked(index, true); }
                                else
                                {
                                    CheckConsentForm.SetItemChecked(index, false);
                                }
                            }
                        }
                    }
                    else if (RDBig.Checked)
                    {
                        for (index = 0; index < CheckConsentForm.Items.Count; index++)
                        {
                            string valuecheckbox = Program.GetCheckedListBoxValue(index, CheckConsentForm).ToString();
                            if (objtrn_patient.tpt_nation_code == "TH")
                            {
                                if (valuecheckbox == "RG101" || valuecheckbox == "RG103")
                                { CheckConsentForm.SetItemChecked(index, true); }
                                else
                                {
                                    CheckConsentForm.SetItemChecked(index, false);
                                }

                            }
                            else
                            {
                                if (valuecheckbox == "RG102" || valuecheckbox == "RG104")
                                { CheckConsentForm.SetItemChecked(index, true); }
                                else
                                {
                                    CheckConsentForm.SetItemChecked(index, false);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnPrintConsenform_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> arraystr = new List<string>();
                for (int index = 0; index < CheckConsentForm.Items.Count; index++)
                {
                    if (CheckConsentForm.GetItemChecked(index))
                    {
                        string valuecheckbox = Program.GetCheckedListBoxValue(index, CheckConsentForm).ToString();
                        arraystr.Add(valuecheckbox);
                    }
                }
                if (arraystr.Count() != 0)
                {
                    int tprID = 0;
                    if (SetTprID != 0)
                    {
                        tprID = SetTprID;
                    }
                    else
                    {
                        if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    }
                    Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, arraystr);
                    frm.previewReport();
                    //ClsReport.previewRpt(arraystr);
                }
                else
                {
                    lbAlertMsg.Text = "Please select Consent Form report.";
                    lbAlertMsg.Focus();
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnprintslip_Click(object sender, EventArgs e)
        {
            //if (Program.CurrentRegis != null)
            //{
            //    ClsReport.printWristband();
            //}
            int tprID = 0;
            if (SetTprID != 0)
            {
                tprID = SetTprID;
            }
            else
            {
                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
            }
            List<string> rptCode = new List<string>() { "RG120" };
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode, true, 1, true);
            frm.previewReport();
        }

        private void chsameMainAddress_CheckedChanged(object sender, EventArgs e)
        {
            trn_patient_regi objregis = (trn_patient_regi)PatientRegisBindingSource.Current;
            if (chsameMainAddress.Checked)
            {
                objregis.tpr_other_name = objregis.trn_patient.tpt_othername;
                objregis.tpr_other_address = objregis.tpr_main_address;
                objregis.tpr_other_province = objregis.tpr_main_province;
                objregis.tpr_other_amphur = objregis.tpr_main_amphur;
                objregis.tpr_other_tumbon = objregis.tpr_main_tumbon;
                objregis.tpr_other_zip_code = objregis.tpr_main_zip_code;
                chOfficeAddress.Checked = false;
            }
            else
            {
                objregis.tpr_other_name = "";
                objregis.tpr_other_address ="";
                objregis.tpr_other_province = "";
                objregis.tpr_other_amphur = "";
                objregis.tpr_other_tumbon = "";
                objregis.tpr_other_zip_code = "";
            }
        }
        private void chOfficeAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chOfficeAddress.Checked)
            {
                trn_patient_regi objregis = (trn_patient_regi)PatientRegisBindingSource.Current;
                objregis.tpr_other_address = "";
                objregis.tpr_other_province = "";
                objregis.tpr_other_amphur = "";
                objregis.tpr_other_tumbon = "";
                objregis.tpr_other_zip_code = "";
                chsameMainAddress.Checked = false;
            }
        }

        private void btnCOFrom_Click(object sender, EventArgs e)
        {
            try
            {//แสดง Report
                int tprID = 0;
                if (SetTprID != 0)
                {
                    tprID = SetTprID;
                }
                else
                {
                    if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                }
                List<string> rptCode = new List<string>() { "RG115" };
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.previewReport();
                //ClsReport.previewRpt(new List<string> { "RG115" });
            }
            catch (Exception)
            {

            }
        }

        private void txtnpotimeRemark_KeyUp(object sender, KeyEventArgs e)
        {
            Timecheck(txtnpotimeRemark, e);
        }

        private void txtTime_KeyUp(object sender, KeyEventArgs e)
        {
            Timecheck(txtTime, e);
        }
        private void Timecheck(MaskedTextBox txt, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete) { return; }
            string[] timekey = txt.Text.ToString().Split(':');
            int time1Houre = Convert1.ToInt32(timekey[0].Trim());
            if (timekey[0].Trim().Length == 2)
            {
                if (time1Houre > 23)
                {
                    time1Houre = 23;
                    txt.Text = time1Houre.ToString() + ":" + timekey[1];
                }
            }
            int time2Houre = Convert1.ToInt32(timekey[1].Trim());
            if (timekey[1].Trim().Length == 2)
            {
                if (time2Houre > 59)
                {
                    time2Houre = 59;
                    txt.Text = time1Houre.ToString("00") + ":" + time2Houre.ToString();
                }
            }
        }

        private void btn_pac_addition_item_Click(object sender, EventArgs e)
        {
            frmPackageAdditionItem frm = new frmPackageAdditionItem();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

        }

        private void RDNPOTimeNo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CBHealthCheckUPProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void CheckListAviation_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void CheckListAviation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CheckListAviation.SelectedIndex == -1)
                return;
            else
            {
                ArrayList SelectedItems = new ArrayList(CheckListAviation.CheckedItems);
                DataTable results = new DataTable(); DataSet ds = new DataSet();
                DateTime datenow = Program.GetServerDateTime();
                if (SelectedItems.Count != 0)
                {
                    for (int i = 0; i <= SelectedItems.Count - 1; i++)
                    {
                        DataTable dt = new DataTable();
                        string strNameSelect = SelectedItems[i].ToString();
                        int macid = (from t1 in dbc.mst_aviation_categories where t1.mac_ename == strNameSelect select t1.mac_id).FirstOrDefault();
                        string mactype = (from t1 in dbc.mst_aviation_categories where t1.mac_ename == strNameSelect select t1.mac_avia_type).FirstOrDefault();
                        string initial = "";

                        switch (mactype)
                        {
                            case "TH":
                                initial = "(Thai)";
                                break;
                            case "CN":
                                initial = "(Canada)";
                                break;
                            case "FA":
                                initial = "(FAA)";
                                break;
                            case "AS":
                                initial = "(Australia)";
                                break;
                            case "FD":
                                initial = "(Flight Attendant)";
                                break;
                        }
                        getAviationType = dbc.pw_Get_AviationType(macid).ToList();
                        dt.Columns.Add("ID");
                        dt.Columns.Add("Name");
                        ds.Tables.Add(dt);
                        foreach (var items in getAviationType)
                        {
                            var row = dt.NewRow();
                            row["ID"] = items.mav_id;
                            row["Name"] = initial + "~" + items.mat_ename;
                            dt.Rows.Add(row);
                        }
                        results = dt.AsEnumerable().Distinct(DataRowComparer.Default).CopyToDataTable();
                        for (int j = 0; j <= results.Rows.Count - 1; j++)
                        {
                            //////Check duplicate data
                            bool chk = true;
                            int count = g1.Rows.Count;
                            List<string> newList = new List<string>();
                            string[] temp = new string[count];
                            for (int c = 0; c < count; c++)
                            {
                                //temp[c] = ListAviationSelect.Items[c].ToString();initial + "~" + dt.Rows[j][1].ToString()
                                temp[c] = g1.Rows[c].Cells[0].Value.ToString();
                                if (temp[c] == dt.Rows[j][1].ToString())
                                {
                                    chk = false;
                                    break;
                                }
                            }
                            if (chk == true)
                            {
                                // ListAviationSelect.Items.Add(initial + "~" + dt.Rows[j][1].ToString());
                                g1.Rows.Add(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString());
                            }
                        }
                    }
                }
            }
        }

        private void ListAviationSelect_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indexSelect = this.ListAviationSelect.SelectedIndex;
            for (int i = this.ListAviationSelect.SelectedIndices.Count - 1; i >= 0; i--)
            {
                this.ListAviationSelect.Items.RemoveAt(this.ListAviationSelect.SelectedIndices[i]);
            }
        }

        private void btnselectFinal_Click(object sender, EventArgs e)
        {
            #region Selected
            try
            {
                for (int i = 0; i <= g1.Rows.Count - 1; i++)
                {
                    if (g1.Rows[i].Cells[0].Selected == true)
                    {
                        bool chk = true;
                        int count = g2.Rows.Count;
                        List<string> newList = new List<string>();
                        string[] temp = new string[count];
                        string str = g1.Rows[i].Cells[0].Value.ToString();
                        for (int c = 0; c <= count - 1; c++)
                        {
                            //temp[c] = ListAviationSelect.Items[c].ToString();initial + "~" + dt.Rows[j][1].ToString()
                            temp[c] = g2.Rows[c].Cells[0].Value.ToString();
                            if (temp[c] == str)
                            {
                                chk = false;
                                break;
                            }
                        }
                        if (chk == true)
                        {
                            g2.Rows.Add(g1.Rows[i].Cells[0].Value.ToString(), g1.Rows[i].Cells[1].Value.ToString());
                        }               
                    }
                }

                for (int i = 0; i < g2.Rows.Count; i++)
                {
                    for (int j = 0; j < g1.Rows.Count; j++)
                    {
                        if (g1.Rows[j].Cells[0].Value.ToString() == g2.Rows[i].Cells[0].Value.ToString())
                        {
                            g1.Rows.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            catch
            {
                return;
            }
            #endregion
        }

        private void ListAviationFinalSelect_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indexSelect = this.ListAviationFinalSelect.SelectedIndex;
            for (int i = this.ListAviationFinalSelect.SelectedIndices.Count - 1; i >= 0; i--)
                this.ListAviationFinalSelect.Items.RemoveAt(this.ListAviationFinalSelect.SelectedIndices[i]);
        }

        #region SavePatientAviation
        private StatusTransaction getAviationCate(ref trn_patient_regi tpr)
        {
            try
            {
                List<int> old_patient_aviation_id = tpr.trn_patient_cats.Select(x => x.tpc_id).ToList();
                foreach (int aviation_id in old_patient_aviation_id)
                {
                    trn_patient_aviation patient_aviation = tpr.trn_patient_aviations.Where(x => x.tpv_id == aviation_id).FirstOrDefault();
                    tpr.trn_patient_aviations.Remove(patient_aviation);
                }
                foreach (DataGridViewRow row in g2.Rows)
                {
                    int get_mav_id = Convert.ToInt32(row.Cells[1].Value.ToString());
                    tpr.trn_patient_aviations.Add(new trn_patient_aviation
                    {
                        mav_id = get_mav_id
                    });
                }
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("frmRegister", "getPatientCate", ex, false);
                return StatusTransaction.Error;
            }
        }
        private void SaveAivation(int tprid)
        {
            try
            {
                #region SaveAviation
                if (g2.Rows.Count > 0)
                {
                    int count = (from t in dbc.trn_patient_aviations where t.tpr_id == tprid select t.tpr_id).Count();
                    if (count != 0)
                    {
                        //dbc.pw_UpdateAviation(tprid);
                        //dbc.SubmitChanges();
                        var delobj = (from t in dbc.trn_patient_aviations where t.tpr_id == tprid select t).ToList();
                        dbc.trn_patient_aviations.DeleteAllOnSubmit(delobj);
                        dbc.SubmitChanges();
                    }
                    for (int i = 0; i <= g2.Rows.Count - 1; i++)
                    {
                        trn_patient_aviation newobj = new trn_patient_aviation
                        {
                            tpr_id = tprid,
                            mav_id = Convert.ToInt32(g2.Rows[i].Cells[1].Value.ToString())
                        };
                        dbc.SubmitChanges();
                    }
                }
                #endregion
            }
            catch 
            {
                return;
            }
        }
        #endregion 

        #region SaveDoctorCategory
        private StatusTransaction getPatientCate(ref trn_patient_regi tpr)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<int> old_patient_cate_id = tpr.trn_patient_cats.Select(x => x.tpc_id).ToList();
                    foreach (int cate_id in old_patient_cate_id)
                    {
                        trn_patient_cat patient_cat = tpr.trn_patient_cats.Where(x => x.tpc_id == cate_id).FirstOrDefault();
                        tpr.trn_patient_cats.Remove(patient_cat);
                    }
                    foreach (var item in listBoxCateSelected.Items)
                    {
                        int mdc_id = cdc.mst_doc_categories.Where(x => x.mdc_ename == item.ToString()).Select(x => x.mdc_id).FirstOrDefault();
                        tpr.trn_patient_cats.Add(new trn_patient_cat
                        {
                            mdc_id = mdc_id
                        });
                    }
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("frmRegister", "getPatientCate", ex, false);
                return StatusTransaction.Error;
            }
        }
        private void SaveDoctorCategory(int tprid)
        {
            try
            {
                if (listBoxCateSelected.Items.Count > 0)
                {
                    int count = (from t in dbc.trn_patient_cats where t.tpr_id == tprid select t.tpr_id).Count();
                    if (count != 0)
                    {
                        ////Delete data exist
                        var delobj = (from t in dbc.trn_patient_cats where t.tpr_id == tprid select t);
                        dbc.trn_patient_cats.DeleteAllOnSubmit(delobj);
                        dbc.SubmitChanges();
                    }
                    for (int i = 0; i <= listBoxCateSelected.Items.Count - 1; i++)
                    {
                        this.listBoxCateSelected.SelectedIndex = i; 
                    }
                    ArrayList SelectedItems = new ArrayList(listBoxCateSelected.SelectedItems);//Listbox
                    for (int i = 0; i <= SelectedItems.Count - 1; i++)
                    {
                        string strNameSelect = SelectedItems[i].ToString();
                        var getmdcid = (from t1 in dbc.mst_doc_categories
                                        where t1.mdc_ename == strNameSelect
                                        select t1).ToList();
                        foreach (var item in getmdcid)
                        {
                            trn_patient_cat newobj = new trn_patient_cat
                            {
                                tpr_id = tprid,
                                mdc_id = Convert.ToInt32(item.mdc_id)
                            };
                            dbc.trn_patient_cats.InsertOnSubmit(newobj);
                            dbc.SubmitChanges();
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAviationType();
        }

        private void btnGetAviation_Click(object sender, EventArgs e)
        {
            //GetAviation();
        }

        private void btnmove_Click(object sender, EventArgs e)
        {
            try
            {
                #region RemoveItems
                for (int i = 0; i <= g2.Rows.Count - 1; i++)
                {
                    if (g2.Rows[i].Cells[0].Selected == true)
                    {
                        g1.Rows.Add(g2.Rows[i].Cells[0].Value.ToString(), g2.Rows[i].Cells[1].Value.ToString());
                    }
                }
                for (int i = 0; i < g1.Rows.Count; i++)
                {
                    for (int j = 0; j < g2.Rows.Count; j++)
                    {
                        if (g2.Rows[j].Cells[0].Value.ToString() == g1.Rows[i].Cells[0].Value.ToString())
                        {
                            g2.Rows.RemoveAt(j);
                            break;
                        }
                    }
                }
                #endregion
            }
            catch
            {
                return;
            }
        }

        private void Gridout_department_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
              
                if (Gridout_department.Columns[e.ColumnIndex].Name == "Coldel")
                {
                    var deletedepartment = (from t in dbc.trn_out_departments where t.tod_id == Convert.ToInt32(Gridout_department.Rows[e.RowIndex].Cells["coltod"].Value) select t).ToList();
                    if (deletedepartment.Count != 0)
                    {
                        dbc.trn_out_departments.DeleteAllOnSubmit(deletedepartment);
                        dbc.SubmitChanges();
                    }
                    trnoutdepartmentsBindingSource.DataSource = (from t1 in dbc.trn_out_departments where t1.tpr_id == Program.CurrentRegis.tpr_id select t1);
                }
            }
            catch
            {
                return;
            }
        }

        private void LoadOutDepartment()
        {
            ////Added.Akkaradech on 2014-01-07
            var objdata = (from t in dbc.trn_out_departments
                           where t.tpr_id == Program.CurrentRegis.tpr_id
                           select new
                           {
                               Description = t.tod_desc,
                               Location = t.tod_location,
                               Time = t.tod_start_date,
                               todid = t.tod_id
                           }).ToList();
            Gridout_department.DataSource = objdata;
            Gridout_department.Columns["coltod"].Visible = false;
        }

        private void btnmovecate_Click(object sender, EventArgs e)
        {
            if (listBoxMasterCate.SelectedItems.Count == 0)
            {
                return;
            }
            else
            {
                string strName = listBoxMasterCate.SelectedItem.ToString();
                ArrayList SelectedItems = new ArrayList(listBoxMasterCate.SelectedItems);
                DataTable results = new DataTable(); DataSet ds = new DataSet();
                DateTime datenow = Program.GetServerDateTime();
                foreach (string item in SelectedItems)
                {
                    if (listBoxCateSelected.FindStringExact(item) == -1)
                        listBoxCateSelected.Items.Add(item);

                    while (listBoxMasterCate.SelectedIndex != -1)
                        listBoxMasterCate.Items.RemoveAt(listBoxMasterCate.SelectedIndex);
                }
            }
        }

        private void btnprecate_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxCateSelected.Items.Count > 0)
                {
                    string strName = listBoxCateSelected.SelectedItem.ToString();
                    ArrayList SelectedItems = new ArrayList(listBoxCateSelected.SelectedItems);

                    foreach (string item in SelectedItems)
                        if (listBoxMasterCate.FindStringExact(item) == -1)
                            listBoxMasterCate.Items.Add(item);

                    while (listBoxCateSelected.SelectedIndex != -1)
                        listBoxCateSelected.Items.RemoveAt(listBoxCateSelected.SelectedIndex);
                }
            }
            catch
            {
                return;
            }
        }

        private void btnClearAllCate_Click(object sender, EventArgs e)
        {
            if (listBoxCateSelected.Items.Count != 0)
            {
                listBoxCateSelected.Items.Clear();
                listBoxMasterCate.Items.Clear();
                SetComboBoxControl();
            }
            else
                return;
        }

        private bool PastedPERoom(int tpr_id)
        {
            bool PastedPE = false;

            var mvtCodePEQuery = (from mstEvent in dbc.mst_events
                                  where mstEvent.mvt_code == "PE"
                                  select mstEvent.mvt_id).SingleOrDefault();
            if (mvtCodePEQuery != 0)
            {
                int mvtCode = mvtCodePEQuery;
                int doctorRoomQuery = (from trnPatientQ in dbc.trn_patient_queues
                                       where trnPatientQ.tpr_id == tpr_id && trnPatientQ.mvt_id == mvtCode
                                       select trnPatientQ.tps_id).Count();
                if (doctorRoomQuery > 0)
                    PastedPE = true;

            }
            return PastedPE;
        }

        private void btnAddHealth_Click(object sender, EventArgs e)
        {
            frmHealthSelect newfrm = new frmHealthSelect();
            Oldtext.oldtxt = "";
            Oldtext.oldtxt = txtHealthCheckup.Text;
            Oldtext.oldID = oldmhcid;
            newfrm.ShowDialog();
            txtHealthCheckup.Text = newfrm.strSelectedName;
            mhcid = newfrm.SelectedID;
            strSelected = newfrm.strSelectedName;
        }

      

        #region Unit Display

        countDownCls clsCountDown = new countDownCls();

        private void btnReady_Click(object sender, EventArgs e)
        {
            btnReady.Enabled = false;
            clsCountDown.finishCountDown();
        }

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
            lbAlertMsg.Text = "";
            StatusTransaction ready = CallQueue.P_CallQueueReady();
            if (ready == StatusTransaction.True)
            {
                AlertOutDepartment.LoadTime();
                StatusTransaction showUnit = new ClsTCPClient().sendCallUnitDisplay();
                if (showUnit == StatusTransaction.Error)
                {
                    //lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถแสดงผลบน unit display ได้";
                }
                StatusCallQueueReady();
            }
            else if (ready == StatusTransaction.Error)
            {
                lbAlertMsg.Text = "กรุณากดปุ่ม Ready อีกครั้ง";
                btnReady.Enabled = true;
            }
        }

        private void StatusCallQueueReady()
        {
            groupQueue.Text = "Queue";
            btnReady.Enabled = false;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            btnCancel.Enabled = true;

            btnSendAuto.Enabled = true;
            btnSaveDraft.Enabled = true;
            btnCOFrom.Enabled = true;
            btnprintslip.Enabled = true;
            panel2.Enabled = true;
        }

        private void StatusCallQueueWaitingReady()
        {
            btnReady.Enabled = true;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;

            btnSendAuto.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnCOFrom.Enabled = false;
            btnprintslip.Enabled = false;
            btnSendManual.Enabled = false;
            panel2.Enabled = false;
            rdPEWaiting.Checked = false;
            rdPENotWaiting.Checked = false;
        }

        #endregion

        private void frmScreeningPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsCountDown.cancelCountDown();
        }

        private void DDcompany_SelectedValueChanged(object sender, EventArgs e)
        {
            EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();
            if (DDcompany.SelectedValue == null)
            {
                DDDepartment.DataSource = null;
            }
            else
            {
                List<string> dep = cls.getListDep(DDcompany.SelectedValue.ToString());
                DDDepartment.DataSource = dep;
            }
        }

        private void DDcompany_Leave(object sender, EventArgs e)
        {
            if (DDcompany.SelectedItem == null)
            {
                DDcompany.SelectedIndex = 0;
            }
        }

        private void gpCompany_EnabledChanged(object sender, EventArgs e)
        {
            if (((GroupBox)sender).Enabled == false)
            {
                DDcompany.SelectedItem = 0;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void clearAviationType()
        {
            ListAviationFinalSelect.Items.Clear();
            ListAviationSelect.Items.Clear();
            g1.Rows.Clear();
            g2.Rows.Clear();
            foreach (int i in CheckListAviation.CheckedIndices)
            {
                CheckListAviation.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
        private void RDPatientType_Aviation_CheckedChanged(object sender, EventArgs e)
        {
            if (RDPatientType_Aviation.Checked || RDPatientType_AviationAircrew.Checked)
            {
                if (RDPatientType_Aviation.Checked)
                {
                    rdWaitPE.Checked = false; // morn : default pe type is null for checked Corporate
                    rdNotWaitPE.Checked = false;

                    chOfficeAddress.Enabled = true;
                    gpCompany.Enabled = true;

                }
                else
                {
                    chOfficeAddress.Checked = false;
                    chOfficeAddress.Enabled = false;
                    gpCompany.Enabled = false;
                }

                CBAviationCategory.Enabled = true;
                RDAviationCategoryNewcase.Enabled = true;
                RDaviationTypeFollowup.Enabled = true;


                CheckListAviation.Enabled = true;
                ListAviationSelect.Enabled = true;
                btnselectFinal.Enabled = true;
                ListAviationFinalSelect.Enabled = true;
                btnClear.Enabled = true;
                btnmove.Enabled = true;
                DateTime datenow = Program.GetServerDateTime();
                var listAviationCat = (from t1 in dbc.mst_aviation_categories
                                       where t1.mac_status == 'A'
                                       && datenow >= t1.mac_effective_date.Value
                                                   && (t1.mac_expire_date != null ? (datenow <= t1.mac_expire_date.Value) : true)
                                       select t1).ToList();
                DataTable Tmptable = new DataTable(); DataSet ds = new DataSet("dss");

                Tmptable.Columns.Add("ID"); Tmptable.Columns.Add("Name");
                ds.Tables.Add(Tmptable);
                var item = CheckListAviation.Items;
                foreach (var element in listAviationCat)
                {
                    var row = Tmptable.NewRow();
                    row["ID"] = element.mac_id;
                    row["Name"] = element.mac_ename;
                    Tmptable.Rows.Add(row);
                }
                for (int i = 0; i <= Tmptable.Rows.Count - 1; i++)
                {
                    item.Add(Tmptable.Rows[i]["Name"].ToString());
                }
            }
            else
            {
                RDAviationCategoryNewcase.Checked = false;
                RDaviationTypeFollowup.Checked = false;
                RDAviationCategoryNewcase.Enabled = false;
                RDaviationTypeFollowup.Enabled = false;
                CBAviationCategory.Enabled = false;
                CheckListAviation.Enabled = false;
                ListAviationSelect.Enabled = false;
                btnselectFinal.Enabled = false;
                ListAviationFinalSelect.Enabled = false;
                btnClear.Enabled = false;
                btnmove.Enabled = false;
                CheckListAviation.Items.Clear();
                clearAviationType();

                if (RDPatientType_General.Checked)
                {
                    gpCompany.Enabled = false;
                    chOfficeAddress.Checked = false;
                    chOfficeAddress.Enabled = false;
                }
                else if (RDPatientType_Corporate.Checked)
                {
                    chOfficeAddress.Enabled = true;
                    rdWaitPE.Checked = false; // morn : default pe type is null for checked Corporate
                    rdNotWaitPE.Checked = false;
                    gpCompany.Enabled = true;
                }
            }
        }

        private void RDReuestPEBeforNO_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.CurrentSite.mhs_extra_pe_type == true)
            {
                if (RDReuestPEBeforNO.Checked)
                {
                    chkReturnScreening.Enabled = false;
                    chkReturnScreening.Checked = false;
                }
                else
                {
                    chkReturnScreening.Enabled = true;
                    chkReturnScreening.Checked = true;
                }
            }
        }

        private void RDRequestDoctorin_CheckedChanged(object sender, EventArgs e)
        {
            GBRequestPEBefore.Enabled = !RDDoctorOutlet.Checked;
            DoctorCompleteBox.SelectedValue = null;
            if (RDDoctorOutlet.Checked)
            {
                RDNA.Checked = true;
                RDNA.AutoCheck = false;
                RDDoctorIsMen.AutoCheck = false;
                RDdoctorfemale.AutoCheck = false;
            }
            else
            {
                RDNA.AutoCheck = true;
                RDDoctorIsMen.AutoCheck = true;
                RDdoctorfemale.AutoCheck = true;
            }
            setDataSourceDoctor();
        }

        private void setDataSourceDoctor()
        {
            if (RDRequestDoctorin.Checked == true)
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var result = cdc.mst_user_types.Where(x => x.mut_fullname.Contains(DoctorCompleteBox.Text.Trim()) && x.mut_username != null && x.mut_fullname != null && x.mut_status =='A')
                    .OrderBy(x => x.mut_fullname)
                    .Select(x => new
                    {
                        val = x.mut_username,
                        dis = x.mut_fullname
                    }).ToList();
                    DoctorCompleteBox.DataSource = result;
                    DoctorCompleteBox.ValueMember = "val";
                    DoctorCompleteBox.DisplayMember = "dis";
                }
            }
            else
            {
                List<DoctorProfile> result = new EmrClass.AutoCompleteDoctor().GetDoctorData();
                if (result.Count > 0)
                {
                    DoctorCompleteBox.DataSource = result;
                    DoctorCompleteBox.ValueMember = "SSUSR_Initials";
                    DoctorCompleteBox.DisplayMember = "CTPCP_Desc";
                }
                else
                {
                    DoctorCompleteBox.DataSource = null;
                }
            }
        }

        private void DoctorCompleteBox_SelectedValueChanged(object sender, object e)
        {
            trn_patient_regi objcurrenttpr = (trn_patient_regi)PatientRegisBindingSource.Current;
            if (e != null)
            {
                DoctorProfile doc = e as DoctorProfile;
                if (doc != null)
                {
                    objcurrenttpr.tpr_req_doc_code = doc.SSUSR_Initials;
                    objcurrenttpr.tpr_req_doc_name = doc.CTPCP_Desc;
                }
                else
                {
                    objcurrenttpr.tpr_req_doc_code = null;
                    objcurrenttpr.tpr_req_doc_name = null;
                }
            }
            else
            {
                objcurrenttpr.tpr_req_doc_code = null;
                objcurrenttpr.tpr_req_doc_name = null;
            }
        }

        private void uiAllLeft2_OnWaitingSuccessProcess(object sender, StatusTransaction isCallQueue, string e)
        {
            lbAlertMsg.Text = "";
            this.AutoScrollPosition = new Point(0, 0);

            lbAlertMsg.Text = e;
            if (isCallQueue == StatusTransaction.True)
            {
                btnCallQueue.Enabled = false;

                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();
                Application.DoEvents();

                if (Program.CurrentRegis != null)
                {

                    //noina add 21/01/2557
                    LoadUI();
                    clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                    try
                    {
                        this.LoadData(Program.CurrentRegis.tpr_id);
                        RDNormal_CheckedChanged(null, null);
                        GetAviation(Program.CurrentRegis.tpr_id);
                        uiMenuBar1.LoadEnableQuestionare();
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "uiAllLeft1_OnWaitingSuccessProcess", ex, false);
                    }
                    StatusCallQueueWaitingReady();
                }
                else
                {
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = "No patient on queue!";
                    btnCallQueue.Enabled = true;
                }

                frmbg.Close();
            }
        }
    }
}
