using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Windows.Forms.VisualStyles;
using System.Data.Linq;

namespace BKvs2010
{
    public partial class frmCheckpointB2 : Form
    {
        public frmCheckpointB2()
        {
            InitializeComponent();

            uiMenuBar1.enableDashBoard();
            GridPendingList.AutoGenerateColumns = false;
            GridPendingDetail.AutoGenerateColumns = false;
        }

        private int select_tpr_id { get; set;}

        private string _statusPending = "N";
        private string statusPending
        {
            get
            {
                return _statusPending;
            }
            set
            {
                if (_statusPending != value)
                {
                    _statusPending = value;
                    if (value == "N")
                    {
                        btnRetive.Enabled = true;
                    }
                    else
                    {
                        btnRetive.Enabled = false;
                    }
                }
            }
        }
        
        private class SelectedSite
        {
            public string Description { get; set; }
            public int DefaultFor_mhs_id { get; set; }
            public List<int> SiteSelect { get; set; }
        }
        private void SetComboSeletedSite()
        {
            var ObjSite = dbc.mst_hpc_sites
                             .Where(x => x.mhs_status == 'A' &&
                                         x.mhs_type == 'P')
                             .Select(x => new
                             {
                                 x.mhs_ename,
                                 x.mhs_id,
                                 x.mhs_code
                             }).ToList();

            List<string> Site1Default = new List<string>
            {
                "01CHK",
                "01AMS",
                "01JMSCK",
                "01IMS",
                "01BLC",
                "01OTH"
            };

            SelectedSite SelectAll = new SelectedSite { Description = "Select All", SiteSelect = ObjSite.Select(x => x.mhs_id).ToList() };
            SelectedSite SelectSite1AJILong = new SelectedSite
            {
                Description = string.Join(", ", ObjSite.Where(x => Site1Default.Contains(x.mhs_code))
                                                       .Select(x => x.mhs_ename)),
                SiteSelect = ObjSite.Where(x => Site1Default.Contains(x.mhs_code))
                                    .Select(x => x.mhs_id).ToList(),
                DefaultFor_mhs_id = 1
            };
            List<SelectedSite> GetObjSite = new List<SelectedSite>
                                            {
                                                SelectAll,
                                                SelectSite1AJILong
                                            };
            GetObjSite.AddRange(ObjSite.Select(x => new SelectedSite
                                       {
                                           Description = x.mhs_ename,
                                           SiteSelect = new List<int> { x.mhs_id },
                                           DefaultFor_mhs_id = x.mhs_code == "01CHK" ? 0 : x.mhs_id
                                       }).ToList());
            DDSiteSearch.ValueMember = "SiteSelect";
            DDSiteSearch.DisplayMember = "Description";
            DDSiteSearch.DataSource = GetObjSite;

            if (Program.CurrentSite != null)
            {
                var selectValue = GetObjSite.Where(x => x.DefaultFor_mhs_id == Program.CurrentSite.mhs_id).Select(x => x.SiteSelect).FirstOrDefault();
                DDSiteSearch.SelectedValue = selectValue;
            }
        }
        private void frmCheckpointB2_Load(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            this.Text = Program.GetRoomName();
            uiFooter1.SetTitle = "All Patient HPC Site List";
            uiFooter1.RoomCode = "CB";
            //Load Data
            List<mst_hpc_site> objsite = (from t1 in dbc.mst_hpc_sites
                                          where t1.mhs_status == 'A'
                                          && t1.mhs_room_chkup == true
                                          select t1).ToList();

            DDsiteToSend.DisplayMember = "mhs_ename";
            DDsiteToSend.ValueMember = "mhs_id";
            DDsiteToSend.DataSource = objsite.ToList();
            DDsiteToSend.SelectedValue = Program.CurrentSite.mhs_id;

            if (DDsiteToSend.SelectedValue == null)
            {
                MessageBox.Show("กรุณาตั้งค่า Site","เกิดข้อผิดพลาด",MessageBoxButtons.OK,MessageBoxIcon.Information);
                frmbg.Close();
                this.Close();
                return;
            }
            //default site
            Program.CheckPointBSiteUse = (int)DDsiteToSend.SelectedValue;

            SetComboSeletedSite();

            //var getobjsite = (from t1 in dbc.mst_hpc_sites
            //               where t1.mhs_status == 'A' && t1.mhs_type == 'P'
            //               select new DropdownData { Code = t1.mhs_id, Name = t1.mhs_ename }).ToList();
            //DropdownData newselect = new DropdownData();
            //newselect.Code = 0;
            //newselect.Name = "Select All";
            //getobjsite.Add(newselect);

            //DDSiteSearch.ValueMember = "Code";
            //DDSiteSearch.DisplayMember = "Name";
            //DDSiteSearch.DataSource = getobjsite.OrderBy(x => x.Code).ToList();

            // Add Button in Gridview
            DataGridViewButtonColumn doppatientButton = new DataGridViewButtonColumn();
            doppatientButton.HeaderText = "";
            doppatientButton.Name = "ColDelete";
            doppatientButton.UseColumnTextForButtonValue = true;
            doppatientButton.Text = "ยกเลิก";
            doppatientButton.Width = 46;
            GridPendingDetail.Columns.Add(doppatientButton);
            // Add Button in Gridview
            DataGridViewButtonColumn doppatientButton2 = new DataGridViewButtonColumn();
            doppatientButton2.HeaderText = "";
            doppatientButton2.Name = "ColDelete";
            doppatientButton2.UseColumnTextForButtonValue = true;
            doppatientButton2.Text = "ยกเลิก";
            doppatientButton2.Width = 46;
            GridPendingList.Columns.Add(doppatientButton2);
            //// Add Button in Gridview
            //DataGridViewButtonColumn btnchoiceCheckPointB = new DataGridViewButtonColumn();
            //btnchoiceCheckPointB.HeaderText = "";
            //btnchoiceCheckPointB.Name = "ColRetrivePackage";
            //btnchoiceCheckPointB.UseColumnTextForButtonValue = true;
            //btnchoiceCheckPointB.Text = "RetrivePackage";
            //btnchoiceCheckPointB.Width = 120;
            //GridData.Columns.Add(btnchoiceCheckPointB);
            //((DataGridViewButtonColumn)this.GridData.Columns["ColbtnRetrive"]).DefaultCellStyle.NullValue = null;
            ((DataGridViewImageColumn)this.GridData.Columns[9]).DefaultCellStyle.NullValue = null;


            LoadData();
            //LoadPendingdata("");

            //uiFooter1.LoadData();
            timer1.Start();

            frmbg.Close();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void SetGridData(DateTime dateNow)
        {
            try
            {
                string strtxt = txtSearch.Text.Trim().ToUpper();
                SelectedSite SelectedData = (SelectedSite)DDSiteSearch.SelectedItem;
                List<int> FilterSite = SelectedData.SiteSelect;

                var CheckBData = dbc.trn_patient_queues
                                    .Where(x => x.mst_room_hdr.mrm_code == "CB" &&
                                                x.tps_status == "NS" &&
                                                x.tps_ns_status == "QL" &&
                                                (x.trn_patient_regi.tpr_pending_no_station == false || x.trn_patient_regi.tpr_pending_no_station == null) &&
                                                (x.trn_patient_regi.tpr_close_other_site == false || x.trn_patient_regi.tpr_close_other_site == null) &&
                                                FilterSite.Contains(x.trn_patient_regi.mhs_id) &&
                                                x.trn_patient_regi.tpr_arrive_date.Value.Date == dateNow.Date &&
                                                (x.trn_patient_regi.trn_patient.tpt_hn_no.ToUpper().Contains(strtxt) ||
                                                 x.trn_patient_regi.trn_patient.tpt_othername.ToUpper().Contains(strtxt) ||
                                                 x.trn_patient_regi.tpr_queue_no.Contains(strtxt) ||
                                                 x.trn_patient_regi.trn_patient.tpt_hn_no.Replace("-", "").Contains(strtxt)))
                                    .OrderBy(x => x.trn_patient_regi.mhs_id)
                                    .ThenByDescending(x => x.tps_update_date)
                                    .Select(x => new
                                    {
                                        msh_id = x.trn_patient_regi.mst_hpc_site.mhs_id,
                                        msh_name = x.trn_patient_regi.mst_hpc_site.mhs_ename,
                                        tps_id = x.tps_id,
                                        tpr_id = x.tpr_id,
                                        queue_no = x.trn_patient_regi.tpr_queue_no,
                                        hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                        fullname = x.trn_patient_regi.trn_patient.tpt_othername,
                                        tpr_pe_status = (x.trn_patient_regi.tpr_pe_status == "RB") ? "Re-Package" : "",
                                        btnretrive = GetImage("RB"),
                                        other_site = (bool)x.trn_patient_regi.mst_hpc_site.mhs_other_clinic
                                    }).ToList();

                GridData.DataSource = CheckBData;
                GridData.Columns["Coltprid"].Visible = false;
                GridData.Columns["Coltps_id"].Visible = false;
                GridData.Columns["Colsite"].Visible = false;
                GridData.Columns["colOther_site"].Visible = false;

                foreach (DataGridViewRow dr in GridData.Rows)
                {
                    if (dr.Cells["colOther_site"].Value != null)
                    {
                        if ((bool)dr.Cells["colOther_site"].Value == true)
                        {
                            dr.Cells["ColPending"] = new DataGridViewTextBoxCell();
                        }
                    }
                }

                if (CheckBData.Count() > 0)
                {
                    int index_gv = 0;
                    if (select_tpr_id != 0)
                    {
                        var tpr_id_in_gv = (from DataGridViewRow gv in GridData.Rows where (int)gv.Cells["Coltprid"].Value == select_tpr_id select gv).FirstOrDefault();
                        if (tpr_id_in_gv != null)
                        {
                            index_gv = tpr_id_in_gv.Index;
                        }
                    }

                    if (GridData["Coltprid", index_gv].Value != null)
                    {
                        int tprID = Convert.ToInt32(GridData["Coltprid", index_gv].Value);
                        Program.CurrentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprID select t1).FirstOrDefault();
                        int tpsID = Convert.ToInt32(GridData["Coltps_id", index_gv].Value);
                        Program.CurrentPatient_queue = (from t1 in dbc.trn_patient_queues where t1.tps_id == tpsID select t1).FirstOrDefault();
                        checkPointBDg_CellClick(GridData, new DataGridViewCellEventArgs(0, 0));
                        //noina
                        GridData.CurrentCell = GridData[0, index_gv];
                    }
                }
                else
                {
                    Program.CurrentRegis = null;
                    uiUserprofile1.LoadData();
                    uiMapping1.GetMapping();
                    Program.CurrentRegis = null;
                    Program.CurrentPatient_queue = null;
                    dbc.Dispose();
                    dbc = new InhCheckupDataContext();
                    loadCheckpointBSubReCheckPackage(0, 0);
                    groupBox6.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetGridData(DateTime dateNow)", ex, false);
            }
        }
        private void LoadData()
        {
            DateTime dtnow = Program.GetServerDateTime();
            SetGridData(dtnow);
            //int siteid = Convert1.ToInt32(DDSiteSearch.SelectedValue); //Convert1.ToInt32(Program.GetValueComboBoxInt(DDSiteSearch));
            //string strtxt = txtSearch.Text.Trim();
            //DateTime dtnow = Program.GetServerDateTime();
            //DateTime ResetDate = new DateTime(dtnow.Year, dtnow.Month, dtnow.Day, 0, 0, 0);
            //var cashiersData = (from cust in dbc.trn_patient_queues
            //                    where cust.mst_room_hdr.mrm_code == "CB"
            //                    && cust.tps_status == "NS"
            //                    && cust.tps_ns_status == "QL"
            //                    // Sumit 03/02/2014
            //                    && (cust.trn_patient_regi.tpr_pending_no_station == false || cust.trn_patient_regi.tpr_pending_no_station == null)
            //                    && cust.trn_patient_regi.tpr_arrive_date.Value.Date == ResetDate
            //                    // Sumit 09/05/2014
            //                    && (cust.trn_patient_regi.tpr_close_other_site == false || cust.trn_patient_regi.tpr_close_other_site == null)
            //                    orderby cust.trn_patient_regi.mst_hpc_site.mhs_id
            //                    select new orderCheckpointCdata
            //                    {
            //                        msh_id = cust.trn_patient_regi.mst_hpc_site.mhs_id,
            //                        //msh_id = cust.mst_room_hdr.mhs_id,
            //                        //msh_name = cust.mst_room_hdr.mrm_ename,
            //                        msh_name = cust.trn_patient_regi.mst_hpc_site.mhs_ename,
            //                        tps_id = cust.tps_id,
            //                        tpr_id = cust.tpr_id,
            //                        queue_no = cust.trn_patient_regi.tpr_queue_no,
            //                        hn_no = cust.trn_patient_regi.trn_patient.tpt_hn_no,
            //                        fullname = cust.trn_patient_regi.trn_patient.tpt_pre_name 
            //                                    + " " + cust.trn_patient_regi.trn_patient.tpt_first_name
            //                                    + " " + cust.trn_patient_regi.trn_patient.tpt_last_name,
            //                        tpr_pe_status = (cust.trn_patient_regi.tpr_pe_status == "RB") ? "Re-Package" : "",
            //                        btnretrive = GetImage("RB"),
            //                        other_site = (bool)cust.trn_patient_regi.mst_hpc_site.mhs_other_clinic
            //                    });

            //if (siteid > 0)
            //{
            //    cashiersData = cashiersData.Where(XmlReadMode => XmlReadMode.msh_id == siteid);    
            //}
            //if (strtxt.Length > 0)
            //{
            //    cashiersData = cashiersData.Where(x => x.hn_no.Contains(strtxt) || 
            //                                           x.fullname.Contains(strtxt) || 
            //                                           x.queue_no.Contains(strtxt) ||
            //                                           x.hn_no.Replace("-", "").Contains(strtxt));
            //}
            //GridData.DataSource = cashiersData;
            //GridData.Columns["Coltprid"].Visible = false;
            //GridData.Columns["Coltps_id"].Visible = false;
            //GridData.Columns["Colsite"].Visible = false;
            //GridData.Columns["colOther_site"].Visible = false;

            //foreach (DataGridViewRow dr in GridData.Rows)
            //{
            //    if (dr.Cells["colOther_site"].Value != null)
            //    {
            //        if ((bool)dr.Cells["colOther_site"].Value == true)
            //        {
            //            dr.Cells["ColPending"] = new DataGridViewTextBoxCell();
            //        }
            //    }
            //}

            ////GridData.Columns["Colsitename"].Visible = false;
            //if (cashiersData.Count() > 0)
            //{
            //    //noina
            //    int index_gv = 0;
            //    if (select_tpr_id != 0)
            //    {
            //        var tpr_id_in_gv = (from DataGridViewRow gv in GridData.Rows where (int)gv.Cells["Coltprid"].Value == select_tpr_id select gv).FirstOrDefault();
            //        if (tpr_id_in_gv != null)
            //        {
            //            index_gv = tpr_id_in_gv.Index;
            //        }
            //    }

            //    if (GridData["Coltprid", index_gv].Value != null)
            //    {
            //        int tprID = Convert.ToInt32(GridData["Coltprid", index_gv].Value);
            //        Program.CurrentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprID select t1).FirstOrDefault();
            //        int tpsID = Convert.ToInt32(GridData["Coltps_id", index_gv].Value);
            //        Program.CurrentPatient_queue = (from t1 in dbc.trn_patient_queues where t1.tps_id == tpsID select t1).FirstOrDefault();
            //        checkPointBDg_CellClick(GridData, new DataGridViewCellEventArgs(0, 0));
            //        //noina
            //        GridData.CurrentCell = GridData[0, index_gv];
            //    }
            //}
            //else
            //{
            //    Program.CurrentRegis = null;
            //    uiUserprofile1.LoadData();
            //    uiMapping1.GetMapping();
            //    Program.CurrentRegis = null;
            //    Program.CurrentPatient_queue = null;
            //    dbc.Dispose();
            //    dbc = new CheckupDataContext(Program.Connectionstring);
            //    loadCheckpointBSubReCheckPackage(0, 0);
            //    //btnrefresh.Enabled = false;
            //    //btnRetive.Enabled = false;
            //    groupBox6.Enabled = false;
            //}
        }

        private Image GetImage(string strstatus)
        {
           Image imgicon = null;
           //return (strstatus == "RB") ? imageList1.Images[0] : imgicon;

            if(strstatus == "RS")
                imgicon = imageList1.Images[1];
            else
                imgicon = imageList1.Images[2];
            return imgicon;
        }

        private void LoadPendingdata(string strSearch)
        {
            DateTime dtnow = Program.GetServerDateTime();
            string txtSerach = strSearch == null ? "" : strSearch.Trim().ToUpper();

            #region LoadPending
            var objPendingList = dbc.trn_patient_regis.Where(x => x.tpr_pending == true &&
                                                                  (x.tpr_arrive_date.Value.Date != dtnow.Date ||
                                                                  (x.tpr_arrive_date.Value.Date == dtnow.Date && x.tpr_pending_cancel_onday == true)) &&
                                                                  ((x.trn_patient.tpt_othername == null ? false : x.trn_patient.tpt_othername.ToUpper().Contains(txtSerach)) ||
                                                                   (x.trn_patient.tpt_fullname == null ? false : x.trn_patient.tpt_fullname.ToUpper().Contains(txtSerach)) ||
                                                                   (x.trn_patient.tpt_hn_no == null ? false : x.trn_patient.tpt_hn_no.Contains(txtSerach)) ||
                                                                   (x.trn_patient.tpt_hn_no == null ? false : x.trn_patient.tpt_hn_no.Replace("-", "").Contains(txtSerach))))
                                    .Select(x => new
                                    {
                                        HN = x.trn_patient.tpt_hn_no,
                                        FullName = x.trn_patient.tpt_othername,
                                        package = GetImage((x.tpr_PRM == true) ? "RS" : ""),
                                        pestatus = GetImage((x.tpr_pe_status == "RS") ? "" : "RS"),
                                        planct = x.trn_patient_plans.Where(y => y.tpl_status == 'N' ||
                                                                                y.tpl_status == 'H' ||
                                                                                y.tpl_status == 'D' || 
                                                                                y.tpl_status == 'C').Count() > 0
                                                 ? GetImage("RS")
                                                 : GetImage("")
                                    }).Distinct().ToList();
            //if (strSearch != "")
            //    objPendingList = objPendingList.Where(x => x.HN.Contains(strSearch) || x.FullName.Contains(strSearch) ||
            //                                               x.HN.Replace("-", "").Contains(strSearch)).ToList();
            
            GridPendingList.DataSource = objPendingList;

            if (GridPendingList.CurrentRow != null)
                GridPendingList.CurrentRow.Selected = false;
            //GridPendingList.Text = string.Format("รายชื่อผู้ป่วยที่จัดหมอแล้วแต่ยังไม่ได้พบหมอ ทั้งหมด {0} คน", objRegisList.Count().ToString());
            if (objPendingList.Count() > 0)
            {
                string hn = objPendingList.FirstOrDefault().HN;
                LoadPendingDetail(hn);
            }
            #endregion
        }
                                        
        private void StatusWaitCallQueue()
        {
            lbAlertMsg.Text = "";
            loadUI();
        }
        private void StatusCallQueue()
        {
            loadUI();
        }
        private void StatusSaveData()
        {
            loadUI();
        }
        private void loadUI()
        {
            uiUserprofile1.LoadData();
            uiMapping1.GetMapping();
        }

        private void SkipDoctorAJIL(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                {
                    List<string> site = new List<string> { "01AMS", "01JMSCK", "01IMS", "01BLC" };
                    trn_patient_regi patientRegis = contxt.trn_patient_regis.Where(x => x.tpr_id == tpr_id && site.Contains(x.mst_hpc_site.mhs_code)).FirstOrDefault();
                    if (patientRegis != null)
                    {
                        int eventPE = contxt.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                        List<trn_patient_plan> listPlan = patientRegis.trn_patient_plans.Where(x => x.mvt_id == eventPE && x.tpl_status != 'P').ToList();
                        if (listPlan.Count() > 0)
                        {
                            listPlan.ForEach(x => x.tpl_status = 'P');
                            contxt.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SkipDoctorAJIL()", ex, false);
            }
        }
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();
            DateTime startDate = DateTime.Now;

            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, Program.CurrentRoom.mrm_id);
                if (checkPatientOnCheckB == StatusTransaction.True)
                {
                    SkipDoctorAJIL(tpr_id);
                }
                if (checkPatientOnCheckB == StatusTransaction.True || statusPending == "P")
                {
                    lbAlertMsg.Text = "";
                    if (statusPending == "P")
                    {
                        var listmrm = Getmrmid();
                        if (listmrm.Count() == 0 && (Program.CurrentRegis.tpr_pending_no_station == false || Program.CurrentRegis.tpr_pending_no_station == null))
                        {
                            lbAlertMsg.Text = "Please select station for pending.";
                            timer1.Start();
                            frmbg.Close();
                            return;
                        }
                        if (ReUseTprID() == false)
                        {
                            //LoadPendingdata("");//Load Pending GridLeft
                            timer1.Start();
                            frmbg.Close();
                            return;
                        }
                        if (Program.CurrentPatient_queue != null)
                        {
                            //save use hpc site เป็น Current site ก่อน
                            SaveSiteUse();

                            var tpsid = Program.CurrentPatient_queue.tps_id;
                            StatusTransaction result;
                            if (statusPending == "P")
                            {
                                result = CallQueue.SendAutoOnPendingCheckB();
                            }
                            else
                            {
                                result = CallQueue.SendAutoOnStation();
                            }
                            if (result == StatusTransaction.True)
                            {
                                //new Class.FunctionDataCls().genOrUpdateDoctorQuota(Program.CurrentRegis.tpr_id);
                                new Class.FunctionDataCls().dispense_doctor_by_point(tpr_id);
                                if (statusPending == "P")
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAutoOnPendingCheckB,
                                                                tpr_id,
                                                                tpsid,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username,
                                                                startDate);
                                    GridPendingList.DataSource = new
                                    {
                                        HN = "",
                                        FullName = "",
                                        package = GetImage(""),
                                        pestatus = GetImage(""),
                                        planct = GetImage("")
                                    };
                                    GridPendingDetail.DataSource = new vw_Pending_Right();
                                    txtSearchPending.Text = "";
                                }
                                else
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                                tpr_id,
                                                                tpsid,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username,
                                                                startDate);
                                }

                                //string send_roomname = Program.Getmvt_Name(CallQueue.Getmvtid);
                                string QueueNo = (from t1 in dbc.trn_patient_queues
                                                  where t1.tps_id == tpsid
                                                  select t1.trn_patient_regi.tpr_queue_no).FirstOrDefault();
                                //LoadPendingdata("");//Load Pending again
                                
                                LoadData();
                                StatusWaitCallQueue();
                                var objqueue = (from t1 in dbc.trn_patient_queues
                                                where t1.tpr_id == CallQueue.Gettprid
                                                    && t1.mrm_id == CallQueue.Getmrmid
                                                    && t1.mvt_id == CallQueue.Getmvtid
                                                select t1).FirstOrDefault();
                                new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                string data = objqueue.mst_room_hdr.mrm_ename + string.Format(" [{0} {1}]", objqueue.mst_room_hdr.mst_zone.mze_ename, objqueue.mst_room_hdr.mst_hpc_site.mhs_ename);
                                lbAlertMsg.Text = "Queue No. " + QueueNo + " .Send To " + data;
                            }
                            else if (result == StatusTransaction.Error)
                            {
                                //LoadPendingdata("");//Load Pending again
                                LoadData();
                                lbAlertMsg.Text = "โปรดกด Send Auto อีกครั้ง";
                            }
                            else
                            {
                                //LoadPendingdata("");//Load Pending again
                                LoadData();
                            }

                            if (GridPendingDetail.Rows.Count > 0)
                            {
                                //noina comment ถ้าไม่กดกล่องซ้ายมือ ยังไม่น่าจะต้อง โหลด ข้อมูล รายละเอียด station
                                //string HNno = Convert1.ToString(GridPendingList["ColHNPending", 0].Value);
                                //LoadPendingDetail(HNno);
                                //LoadPendingDetail("");
                            }
                            uiUserprofile1.LoadData();
                        }
                    }
                    else
                    {
                        if (Program.CurrentPatient_queue != null)
                        {
                            int tps_id = Program.CurrentPatient_queue.tps_id;
                            if (new Class.FunctionDataCls().ChkSendAutoNewModule(Program.CurrentRegis))
                            {
                                int mhs_id = Program.CurrentSite.mhs_id;
                                string msgAlert = "";
                                bool isPopup = false;
                                QueueClass.SendAutoCls.ResultSendQueue result = new QueueClass.SendAutoCls().SendAuto(tps_id, mhs_id, Program.CurrentUser, ref msgAlert, ref isPopup, mhs_id);
                                if (result == QueueClass.SendAutoCls.ResultSendQueue.SendComplete)
                                {
                                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                                tpr_id,
                                                                tps_id,
                                                                Program.CurrentSite.mhs_id,
                                                                Program.CurrentRoom.mrd_ename,
                                                                Program.CurrentUser.mut_username,
                                                                startDate);
                                    //LoadPendingdata("");//Load Pending again
                                    LoadData();
                                    uiUserprofile1.LoadData();
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
                            else
                            {
                                StatusTransaction result = CallQueue.SendAutoOnStation();

                                if (result == StatusTransaction.True)
                                {
                                    //new Class.FunctionDataCls().genOrUpdateDoctorQuota(Program.CurrentRegis.tpr_id);
                                    new Class.FunctionDataCls().dispense_doctor_by_point(tpr_id);
                                    if (statusPending == "P")
                                    {
                                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAutoOnPendingCheckB,
                                                                    tpr_id,
                                                                    tps_id,
                                                                    Program.CurrentSite.mhs_id,
                                                                    Program.CurrentRoom.mrd_ename,
                                                                    Program.CurrentUser.mut_username,
                                                                    startDate);
                                    }
                                    else
                                    {
                                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                                    tpr_id,
                                                                    tps_id,
                                                                    Program.CurrentSite.mhs_id,
                                                                    Program.CurrentRoom.mrd_ename,
                                                                    Program.CurrentUser.mut_username,
                                                                    startDate);
                                    }

                                    //string send_roomname = Program.Getmvt_Name(CallQueue.Getmvtid);
                                    string QueueNo = (from t1 in dbc.trn_patient_queues
                                                      where t1.tps_id == tps_id
                                                      select t1.trn_patient_regi.tpr_queue_no).FirstOrDefault();
                                    //LoadPendingdata("");//Load Pending again
                                    LoadData();
                                    StatusWaitCallQueue();
                                    var objqueue = (from t1 in dbc.trn_patient_queues
                                                    where t1.tpr_id == CallQueue.Gettprid
                                                        && t1.mrm_id == CallQueue.Getmrmid
                                                        && t1.mvt_id == CallQueue.Getmvtid
                                                    select t1).FirstOrDefault();
                                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                    string data = objqueue.mst_room_hdr.mrm_ename + string.Format(" [{0} {1}]", objqueue.mst_room_hdr.mst_zone.mze_ename, objqueue.mst_room_hdr.mst_hpc_site.mhs_ename);
                                    lbAlertMsg.Text = "Queue No. " + QueueNo + " .Send To " + data;
                                }
                                else if (result == StatusTransaction.Error)
                                {
                                    //LoadPendingdata("");//Load Pending again
                                    LoadData();
                                    lbAlertMsg.Text = "โปรดกด Send Auto อีกครั้ง";
                                }
                                else
                                {
                                    //LoadPendingdata("");//Load Pending again
                                    LoadData();
                                }
                                uiUserprofile1.LoadData();
                            }
                        }
                    }
                    this.AutoScrollPosition = new Point(0, 0);

                }
                else if (checkPatientOnCheckB == StatusTransaction.False)
                {
                    lbAlertMsg.Text = "คนไข้ไม่ได้อยู่ในสถานะ ที่จะ Send Auto ได้";
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("frmCheckpointB2", "btnSendAuto_Click" + statusPending, ex, false);
            }
            frmbg.Close();
            timer1.Start();
        }
        //private void btnSendAuto_Click(object sender, EventArgs e)
        //{
        //    timer1.Stop();

        //    frmBGScreen frmbg = new frmBGScreen();
        //    frmbg.Show();
        //    Application.DoEvents();

        //    try
        //    {
        //        int tpr_id = Program.CurrentRegis.tpr_id;
        //        StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, Program.CurrentRoom.mrm_id);
        //        if (checkPatientOnCheckB == StatusTransaction.True || statusPending == "P")
        //        {

        //            this.AutoScrollPosition = new Point(0, 0);
        //            lbAlertMsg.Text = "";
        //            if (statusPending == "P")
        //            {
        //                var listmrm = Getmrmid();
        //                if (listmrm.Count() == 0 && (Program.CurrentRegis.tpr_pending_no_station == false || Program.CurrentRegis.tpr_pending_no_station == null))
        //                {
        //                    lbAlertMsg.Text = "Please select station for pending.";
        //                    timer1.Start();
        //                    frmbg.Close();
        //                    return;
        //                }
        //                if (ReUseTprID() == false)
        //                {
        //                    LoadPendingdata("");//Load Pending GridLeft
        //                    timer1.Start();
        //                    frmbg.Close();
        //                    return;
        //                }
        //            }

        //            if (Program.CurrentPatient_queue != null)
        //            {

        //                //save use hpc site เป็น Current site ก่อน
        //                SaveSiteUse();

        //                var tpsid = Program.CurrentPatient_queue.tps_id;
        //                StatusTransaction result;
        //                if (statusPending == "P")
        //                {
        //                    result = CallQueue.SendAutoOnPendingCheckB();
        //                }
        //                else
        //                {
        //                    result = CallQueue.SendAutoOnStation();
        //                }
        //                if (result == StatusTransaction.True)
        //                {
        //                    //new Class.FunctionDataCls().genOrUpdateDoctorQuota(Program.CurrentRegis.tpr_id);
        //                    new Class.FunctionDataCls().dispense_doctor_by_point(tpr_id);
        //                    if (statusPending == "P")
        //                    {
        //                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAutoOnPendingCheckB,
        //                                                    tpr_id,
        //                                                    tpsid,
        //                                                    Program.CurrentSite.mhs_id,
        //                                                    Program.CurrentRoom.mrd_ename,
        //                                                    Program.CurrentUser.mut_username);
        //                    }
        //                    else
        //                    {
        //                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
        //                                                    tpr_id,
        //                                                    tpsid,
        //                                                    Program.CurrentSite.mhs_id,
        //                                                    Program.CurrentRoom.mrd_ename,
        //                                                    Program.CurrentUser.mut_username);
        //                    }

        //                    //string send_roomname = Program.Getmvt_Name(CallQueue.Getmvtid);
        //                    string QueueNo = (from t1 in dbc.trn_patient_queues
        //                                      where t1.tps_id == tpsid
        //                                      select t1.trn_patient_regi.tpr_queue_no).FirstOrDefault();
        //                    LoadPendingdata("");//Load Pending again
        //                    LoadData();
        //                    StatusWaitCallQueue();
        //                    var objqueue = (from t1 in dbc.trn_patient_queues
        //                                    where t1.tpr_id == CallQueue.Gettprid
        //                                        && t1.mrm_id == CallQueue.Getmrmid
        //                                        && t1.mvt_id == CallQueue.Getmvtid
        //                                    select t1).FirstOrDefault();
        //                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
        //                    string data = objqueue.mst_room_hdr.mrm_ename + string.Format(" [{0} {1}]", objqueue.mst_room_hdr.mst_zone.mze_ename, objqueue.mst_room_hdr.mst_hpc_site.mhs_ename);
        //                    lbAlertMsg.Text = "Queue No. " + QueueNo + " .Send To " + data;
        //                }
        //                else if (result == StatusTransaction.Error)
        //                {
        //                    LoadPendingdata("");//Load Pending again
        //                    LoadData();
        //                    lbAlertMsg.Text = "โปรดกด Send Auto อีกครั้ง";
        //                }
        //                else
        //                {
        //                    LoadPendingdata("");//Load Pending again
        //                    LoadData();
        //                }

        //                if (GridPendingDetail.Rows.Count > 0)
        //                {
        //                    //noina comment ถ้าไม่กดกล่องซ้ายมือ ยังไม่น่าจะต้อง โหลด ข้อมูล รายละเอียด station
        //                    //string HNno = Convert1.ToString(GridPendingList["ColHNPending", 0].Value);
        //                    //LoadPendingDetail(HNno);
        //                    //LoadPendingDetail("");
        //                }
        //                uiUserprofile1.LoadData();

        //            }
        //        }
        //        else if (checkPatientOnCheckB == StatusTransaction.False)
        //        {
        //            lbAlertMsg.Text = "คนไข้ไม่ได้อยู่ในสถานะ ที่จะ Send Auto ได้";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("frmCheckpointB2", "btnSendAuto_Click" + statusPending, ex, false);
        //    }
        //    frmbg.Close();
        //    timer1.Start();
        //}
        private void btnSendM_Click(object sender, EventArgs e)
        {
            this.AutoScrollPosition = new Point(0, 0);

            int tpr_id = Program.CurrentRegis.tpr_id;

            
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            try
            {
                StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, Program.CurrentRoom.mrm_id);
                if (checkPatientOnCheckB == StatusTransaction.True || statusPending == "P")
                {
                    timer1.Stop();
                    lbAlertMsg.Text = "";

                    //Add by noina รับค่าเข้าไปเช็คในฟังชั่นว่าถ้าเป็น P ให้ทำสโตร์ re pending
                    CallQueue.GetStatusPending = statusPending;

                    if (statusPending == "P")
                    {
                        //check ว่ามีการเลือก Pendding Detail หรือไม่
                        var listmrm = Getmrmid();
                        if (listmrm.Count() == 0)
                        {
                            lbAlertMsg.Text = "Please select station for pending.";
                            timer1.Start();
                            frmbg.Close();
                            return;
                        }
                        if (ReUseTprID() == false)
                        {
                            //LoadPendingdata("");//Load Pending GridLeft
                            timer1.Start();
                            frmbg.Close();
                            return;
                        }
                    }
                    if (Program.CurrentPatient_queue != null)
                    {

                        //save use hpc site เป็น Current site ก่อน
                        SaveSiteUse();

                        var tpsid = Program.CurrentPatient_queue.tps_id;
                        if (statusPending == "P")
                        {
                            lbAlertMsg.Text = "";
                            string messegeAlert = "";
                            StatusTransaction result = new Class.SendManaulCls().SendManualOnStationPendingCheckB(ref messegeAlert);
                            if (result == StatusTransaction.True)
                            {
                                //new Class.FunctionDataCls().genOrUpdateDoctorQuota(Program.CurrentRegis.tpr_id);
                                new Class.FunctionDataCls().dispense_doctor_by_point(tpr_id);
                                new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendManualOnPendingCheckB,
                                                            tpr_id,
                                                            tpsid,
                                                            Program.CurrentSite.mhs_id,
                                                            Program.CurrentRoom.mrd_ename,
                                                            Program.CurrentUser.mut_username);

                                GridPendingList.DataSource = new
                                {
                                    HN = "",
                                    FullName = "",
                                    package = GetImage(""),
                                    pestatus = GetImage(""),
                                    planct = GetImage("")
                                };
                                GridPendingDetail.DataSource = new vw_Pending_Right();
                                txtSearchPending.Text = "";
                                LoadData();
                                StatusWaitCallQueue();
                                lbAlertMsg.Text = messegeAlert;
                            }
                            else if (result == StatusTransaction.Error)
                            {
                                //LoadPendingdata("");//Load Pending again
                                LoadData();
                                lbAlertMsg.Text = "เกิดความผิดพลาดของระบบ โปรด send manaul อีกครั้ง";
                            }
                            else
                            {
                                //LoadPendingdata("");//Load Pending again
                                LoadData();
                            }
                        }
                        else
                        {
                            lbAlertMsg.Text = "";
                            string messegeAlert = "";
                            StatusTransaction result = new Class.SendManaulCls().SendManualOnCheckB(ref messegeAlert);
                            if (result == StatusTransaction.True)
                            {
                                //new Class.FunctionDataCls().genOrUpdateDoctorQuota(Program.CurrentRegis.tpr_id);
                                new Class.FunctionDataCls().dispense_doctor_by_point(tpr_id);
                                new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendManual,
                                                            tpr_id,
                                                            tpsid,
                                                            Program.CurrentSite.mhs_id,
                                                            Program.CurrentRoom.mrd_ename,
                                                            Program.CurrentUser.mut_username);

                                //LoadPendingdata("");
                                LoadData();
                                StatusWaitCallQueue();
                                new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                                lbAlertMsg.Text = messegeAlert;
                            }
                            else if (result == StatusTransaction.Error)
                            {
                                //LoadPendingdata("");//Load Pending again
                                LoadData();
                                lbAlertMsg.Text = "โปรด send manaul อีกครั้ง";
                            }
                            else
                            {
                                //LoadPendingdata("");//Load Pending again
                                LoadData();
                            }
                        }
                        if (GridPendingDetail.Rows.Count > 0)
                        {
                            //string HNno = Convert1.ToString(GridPendingList["ColHNPending", 0].Value);
                            //LoadPendingDetail(HNno);
                        }
                        uiUserprofile1.LoadData();
                    }
                }
                else if (checkPatientOnCheckB == StatusTransaction.False)
                {
                    lbAlertMsg.Text = "คนไข้ไม่ได้อยู่ในสถานะ ที่จะ Send Manual ได้";
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSendM_Click", ex, false);
            }
            timer1.Start();
            frmbg.Close();
        }

        #region "Add PackageItem to PackageSet"
        private Boolean ReUseTprID()
        {
            if (dbc.Connection.State == ConnectionState.Closed)
            {
                dbc = new InhCheckupDataContext();
            }

            var objReg = (from t1 in dbc.trn_patient_regis
                          where t1.trn_patient.tpt_hn_no == Program.CurrentRegis.trn_patient.tpt_hn_no
                          orderby t1.tpr_arrive_date descending
                          select t1).FirstOrDefault();
            if (objReg != null)
            {
                try
                {
                    DateTime dtnow = Program.GetServerDateTime();
                    //1-Update trn_patient-Regis *********************************


                    string strqueuNo = objReg.tpr_queue_no.Substring(1, 4);
                    int countpendingct = Convert1.ToInt32(objReg.tpr_pending_ct);
                    objReg.tpr_queue_no = "8" + strqueuNo;
                    objReg.tpr_arrive_date = dtnow.Date;
                    objReg.tpr_pending_ct = countpendingct + 1;
                    objReg.tpr_update_by = Program.CurrentUser.mut_username;
                    objReg.tpr_update_date = dtnow;
                    

                    //2. insert Package Item *********************************
                    bool haveNew = false;//เชคว่ามีรายการเพิ่มใหม่หรือไม่
                    List<int> mrmselect = Getmrmid();
                    List<int> mrmfirstselect = Getmrmfirstid(); //Id ห้องใหม่ กรณีส่งข้าม Site
                    var objpendingRight = (from t1 in dbc.vw_Pending_Rights
                                           where t1.tpt_hn_no == Program.CurrentRegis.trn_patient.tpt_hn_no
                                           select t1.tpr_id).Distinct().ToList();

                    var objitempack = (from t1 in dbc.vw_Pending_Packages
                                       where objpendingRight.Contains(t1.tpr_id)
                                       && t1.mhs_id == (int)DDsiteToSend.SelectedValue
                                       && mrmselect.Contains(t1.mrm_id)
                                       select t1);

                    //var d = dbc.vw_Pending_Packages.Where(x => x.mhs_id == Program.CurrentSite.mhs_id && x.tpr_id == objReg.tpr_id);
                    var d = dbc.vw_Pending_Packages.Where(x => x.mhs_id == (int)DDsiteToSend.SelectedValue && x.tpr_id == objReg.tpr_id);

                    foreach (var item in objitempack)
                    {
                        var tos_id = 0;
                        string set_item_row_id = item.toi_item_row_id;
                        string set_row_id = item.tos_item_row_id;
                        string set_Code = item.tos_od_set_code;
                        //Check Pakcage set
                        var objset = (from t1 in dbc.trn_patient_order_sets
                                      where t1.tos_item_row_id == set_row_id
                                      && t1.tos_od_set_code == set_Code
                                      && t1.tpr_id == objReg.tpr_id
                                      select t1);
                        if (objset.Count() == 0)
                        {
                            if (set_item_row_id != "" && set_Code != "")
                            {
                                trn_patient_order_set tmppackage = new trn_patient_order_set();
                                tmppackage.tos_item_row_id = item.tos_item_row_id;
                                tmppackage.tos_od_set_code = item.tos_od_set_code;
                                tmppackage.tos_od_set_name = item.tos_od_set_name;
                                tmppackage.tos_create_by = Program.CurrentUser.mut_username;
                                tmppackage.tos_create_date = dtnow;
                                tmppackage.tos_update_by = tmppackage.tos_create_by;
                                tmppackage.tos_update_date = dtnow;
                                objReg.trn_patient_order_sets.Add(tmppackage);
                                tos_id = tmppackage.tos_id;
                                haveNew = true;
                            }
                        }
                        else
                        {
                            var currentset = objset.FirstOrDefault();
                            if (currentset != null)
                            {
                                tos_id = currentset.tos_id;
                            }
                        }     
                           
                        //if (AddPackagetItme(ref objReg, item, tos_id, set_Code))
                        if (AddPackagetItme(ref objReg, item, tos_id, set_item_row_id))
                        {
                            haveNew = true;
                        }//insert Packet Item

                        //update Patient plan กรณีที่มี package เก่าและไม่ได้เพิ่มใหม่; [trn_patient_plans] status="C" ==> status="N"
                        int mrmid = item.mrm_id;
                        List<int> mvtidList = (from t1 in dbc.mst_room_events
                                     where t1.mrm_id == mrmid
                                     select t1.mvt_id).ToList();
                        var currenteventroom = (from t1 in objReg.trn_patient_plans
                                                where mvtidList.Contains(t1.mvt_id)
                                                select t1);
                        foreach (var currentitem in currenteventroom)
                        {
                            currentitem.tpl_status = 'N';
                        }
                    }

                    //End add Package Set & item



                    //3 insert plan
                    if (haveNew == true)
                    { //check trn_paient _order_item   --- Update patient plan
                        var objlistddd = (from t1 in objReg.trn_patient_order_items select t1.toi_item_row_id);
                        var objorderitem = objReg.trn_patient_order_items.ToList();
                        if (objlistddd.Count() > 0)
                        {
                            var objorderplanList = (from t2 in dbc.mst_order_plans
                                                    where objlistddd.Contains(t2.mop_item_row_id) &&
                                                          t2.mop_status == 'A'
                                                    select new PatientOrderItemAddPlans
                                                    {
                                                        item_row_id = t2.mop_item_row_id,
                                                        mvt_id = t2.mvt_id,
                                                        patho = "",
                                                        pacSheet = ""
                                                    }).ToList().DistinctBy(p => new { p });

                            foreach (PatientOrderItemAddPlans objitem in objorderplanList)
                            {
                                var icount = (from t1 in objReg.trn_patient_plans
                                              where t1.mvt_id == objitem.mvt_id
                                              select t1).Count();
                                if (icount == 0)
                                {
                                    trn_patient_plan newtpp = new trn_patient_plan();
                                    newtpp.mvt_id = objitem.mvt_id;
                                    newtpp.tpl_status = 'N';
                                    newtpp.tpl_by = 'A';
                                    newtpp.tpl_new = false;//status =1
                                    var itemor = objorderitem.Where(x => x.toi_item_row_id == objitem.item_row_id).FirstOrDefault();
                                    if (itemor != null)
                                    {
                                        DateTime arrivedate = (objReg.tpr_arrive_date != null) ? (DateTime)objReg.tpr_arrive_date : dtnow;
                                        newtpp.tpl_patho = Program.pathoReplace(itemor.toi_patho, arrivedate);
                                        newtpp.tpl_pac_sheet = Program.pacSheetReplace(itemor.toi_pac_sheet);
                                    }
                                    newtpp.tpl_create_by = Program.CurrentUser.mut_username;
                                    newtpp.tpl_create_date = dtnow;
                                    objReg.trn_patient_plans.Add(newtpp);
                                }
                                else
                                {
                                }
                            }
                        }
                    }
                  
                    //End 3 Insert Plan

                    //Start 3.1 Insert Plan for PRM Package
                    trn_patient_regi Objdata = (from t in dbc.trn_patient_regis where t.tpr_id == objReg.tpr_id select t).FirstOrDefault();

                    //for (int i = 0; i <= GridPendingDetail.Rows.Count - 1; i++)
                    //{

                    //    if (Convert1.ToBoolean(GridPendingDetail["Colselect", i].Value) && GridPendingDetail["Colflag", i].Value.ToString() == "Y")
                    //    {
                    //        List<int> mvt = (from t in dbc.mst_room_events where t.mrm_id == Convert.ToInt32(GridPendingDetail.Rows[i].Cells["Colmrmid"].Value.ToString()) select t.mvt_id).ToList();
                    //        foreach (int objitem in mvt)
                    //        {
                    //            trn_patient_plan newdata = new trn_patient_plan();
                    //            newdata.tpr_id = objReg.tpr_id;
                    //            newdata.mvt_id = Convert1.ToInt32(objitem);
                    //            newdata.tpl_status = 'N';
                    //            newdata.tpl_by = 'A';
                    //            newdata.tpl_type = 'U';
                    //            newdata.tpl_new = false;
                    //            newdata.tpl_create_by = "System";
                    //            newdata.tpl_create_date = dtnow;
                    //            objReg.trn_patient_plans.Add(newdata);
                    //        }
                    //        if (Objdata != null) Objdata.tpr_PRM_check = true;
                    //    }
                    //    else
                    //    {
                    //        if (Objdata != null) Objdata.tpr_PRM_check = false;
                    //    }
                    //}

                    foreach (DataGridViewRow row in GridPendingDetail.Rows)
                    {
                        bool? chk = (bool?)row.Cells["Colselect"].Value;
                        if (chk == true)
                        {
                            vw_Pending_Right data = (vw_Pending_Right)row.DataBoundItem;
                            if (data.PRM_flag == "Y")
                            {
                                List<int> mvt = (from t in dbc.mst_room_events where t.mrm_id == data.mrm_id select t.mvt_id).ToList();
                                foreach (int objitem in mvt)
                                {
                                    trn_patient_plan newdata = new trn_patient_plan();
                                    newdata.tpr_id = objReg.tpr_id;
                                    newdata.mvt_id = Convert1.ToInt32(objitem);
                                    newdata.tpl_status = 'N';
                                    newdata.tpl_by = 'A';
                                    newdata.tpl_type = 'U';
                                    newdata.tpl_new = false;
                                    newdata.tpl_create_by = "System";
                                    newdata.tpl_create_date = dtnow;
                                    objReg.trn_patient_plans.Add(newdata);
                                }
                                if (Objdata != null) Objdata.tpr_PRM_check = true;
                            }
                            else
                            {
                                if (Objdata != null) Objdata.tpr_PRM_check = false;
                            }
                        }
                    }
                    //End 3.1

                    //4.Create patient Queue  CheckPoint B
                    trn_patient_queue newqueue = new trn_patient_queue();
                    var mvtobj = (from t1 in dbc.mst_room_events
                                  where t1.mrm_id == Program.CurrentRoom.mrm_id
                                  select t1).FirstOrDefault();
                    if (mvtobj != null)
                    {
                        var objqueue = (from t1 in dbc.trn_patient_queues
                                        where t1.tpr_id == objReg.tpr_id
                                            && t1.mrm_id == Program.CurrentRoom.mrm_id
                                            && t1.mvt_id == mvtobj.mvt_id
                                        select t1).FirstOrDefault();
                        if (objqueue == null)
                        {   // insert table trn_patient_Queue
                            newqueue.mrm_id = Program.CurrentRoom.mrm_id;
                            newqueue.mvt_id = mvtobj.mvt_id;
                            newqueue.mrd_id = null;
                            newqueue.tps_start_date = dtnow;
                            newqueue.tps_end_date = dtnow;
                            newqueue.tps_status = "NS";
                            newqueue.tps_ns_status = "QL";
                            newqueue.tps_create_by = Program.CurrentUser.mut_username;
                            newqueue.tps_create_date = dtnow;
                            newqueue.tps_update_by = Program.CurrentUser.mut_username;
                            newqueue.tps_update_date = dtnow;
                            objReg.trn_patient_queues.Add(newqueue);
                        }
                        else
                        { //กรณีที่เป็นคนเก่าไม่ได้ createใหม่
                            objqueue.tps_start_date = dtnow;
                            objqueue.tps_end_date = dtnow;
                            objqueue.tps_status = "NS";
                            objqueue.tps_ns_status = "QL";
                            objqueue.tps_update_by = Program.CurrentUser.mut_username;
                            objqueue.tps_update_date = dtnow;
                            newqueue = objqueue;
                        }
                    }
                    var objalltprid = (from t1 in dbc.trn_patient_regis
                                       where t1.trn_patient.tpt_hn_no == Program.CurrentRegis.trn_patient.tpt_hn_no
                                       select t1);
                    foreach (trn_patient_regi item in objalltprid)
                    {
                        var objpatientPending = (from t1 in dbc.trn_patient_pendings
                                                 where t1.tpr_id == item.tpr_id
                                                 && mrmfirstselect.Contains(t1.mrm_id)
                                                 select t1);

                        foreach (trn_patient_pending itempp in objpatientPending)
                        {
                            itempp.tpp_status = 'C';
                        }
                    }
                    //----------- end  ------------
                    dbc.SubmitChanges();


                    //5 Update trn_patient_pending
                    var objalltpridList2 = (from t1 in dbc.trn_patient_regis
                                       where t1.trn_patient.tpt_hn_no == Program.CurrentRegis.trn_patient.tpt_hn_no
                                       select t1);
                    foreach (trn_patient_regi item in objalltpridList2)
                    {
                        //เปลี่ยน tpr_status==CP กรณีที่ไม่มี Pending Detail เท่านั้น
                        var icountpendingDetail = (from t1 in dbc.vw_Pending_Rights
                                                   where t1.tpr_id == item.tpr_id
                                                   select t1).Count();
                        if (icountpendingDetail == 0)
                        {
                            //item.tpr_status = "CP";
                            item.tpr_pending = false;
                        }
                        item.tpr_pending_cancel_onday = false;
                    }
                    dbc.SubmitChanges();

                    Program.CurrentPatient_queue = newqueue;
                    Program.CurrentRegis = objReg;

                    // insert Get Text File from Trakcare
                    CallQueue.SetUpdateTextfile(Program.CurrentRegis.trn_patient.tpt_hn_no, Program.CurrentRegis.tpr_id);

                    return true;
                }
                catch (Exception ex)
                {
                    Program.MessageError("frmCheckpointB2", "ReUseTprID", ex, false);
                    return false;
                }
            }
            return false;
        }
        private bool AddPackagetItme(ref trn_patient_regi objReg,vw_Pending_Package item,int? tos_id,string SetCode)
        {
            int tprid = objReg.tpr_id;
            var icountitem = (from t1 in dbc.trn_patient_order_items
                              where t1.tpr_id == tprid
                              //&& t1.toi_od_item_code == SetCode
                              //&& t1.toi_set_row_id == SetCode
                              && t1.toi_item_row_id == SetCode
                              select t1).Count();
            if (icountitem == 0)
            {
                trn_patient_order_item tmp = new trn_patient_order_item();
                if (tos_id != 0)
                    tmp.tos_id = tos_id;
                else
                    tmp.tos_id = null;

                tmp.toi_set_row_id = item.toi_set_row_id;
                tmp.toi_item_row_id = item.toi_item_row_id;
                tmp.toi_od_item_code = item.toi_od_item_code;
                tmp.toi_od_item_name = item.toi_od_item_name;
                tmp.toi_patho = item.toi_patho;
                tmp.toi_pac_sheet = item.toi_pac_sheet;
                tmp.toi_carotid = item.toi_carotid;
                tmp.toi_create_by = objReg.tpr_update_by;
                tmp.toi_create_date = objReg.tpr_update_date;
                tmp.toi_update_by = objReg.tpr_update_by;
                tmp.toi_update_date = objReg.tpr_update_date;
                tmp.tpr_id = tprid;
                tmp.trn_patient_regi = objReg;
                objReg.trn_patient_order_items.Add(tmp);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CopyPatientRegis()
        {
            // หา Register ล่าสุดของ HNno ที่เลือก
            var objReg = (from t1 in dbc.trn_patient_regis
                          orderby t1.tpr_arrive_date descending
                          where t1.trn_patient.tpt_hn_no == Program.CurrentRegis.trn_patient.tpt_hn_no
                          select t1).FirstOrDefault();
            if (objReg != null)
            {
                try
                {
                    //dbc.Connection.Open();
                    //DbTransaction trans = dbc.Connection.BeginTransaction();
                    //dbc.Transaction = trans;

                    //1-insert trn_patient-Regis
                    DateTime dtnow = Program.GetServerDateTime();
                    trn_patient_regi objnewRegis;
                    bool isCreateReg = false;
                    if (objReg.tpr_arrive_date.Value.Date == dtnow.Date)
                    {
                        // กรณีที่วันมารับบริการ Pendding เป็นวันเดียวกับวันที่เข้ารับบริการล่าสุด
                        // จะไม่ยอมให้ทำงาน ต่อ
                        Program.MessageError("Can not use service in date now.");
                        return false;
                    }
                    else
                    {
                        isCreateReg = true;
                        objnewRegis = new trn_patient_regi();
                        objnewRegis.tpr_id = 0;
                        objnewRegis.tpt_id = objReg.tpt_id;
                        objnewRegis.mhs_id = objReg.mhs_id;
                        objnewRegis.mdc_id = objReg.mdc_id;
                        objnewRegis.mac_id = objReg.mac_id;
                        objnewRegis.mhc_id = objReg.mhc_id;
                        objnewRegis.mut_id = objReg.mut_id;
                        objnewRegis.tpr_site_use = objReg.tpr_site_use;
                        objnewRegis.tpr_main_id = objReg.tpr_id;
                        objnewRegis.tpr_queue_no = "8" + objReg.tpr_queue_no.Substring(1, 4);
                        objnewRegis.tpr_en_no = objReg.tpr_en_no;
                        objnewRegis.tpr_en_rowid = objReg.tpr_en_rowid;
                        objnewRegis.tpr_appointment_date = objReg.tpr_appointment_date;
                        objnewRegis.tpr_arrive_date = dtnow;
                        objnewRegis.tpr_arrive_type = 'W';
                        objnewRegis.tpr_appoint_type = 'T';
                        objnewRegis.tpr_status = null;
                        objnewRegis.tpr_pe_status = null;
                        objnewRegis.tpr_rtn_pe_name = objReg.tpr_rtn_pe_name;
                        objnewRegis.tpr_rtn_pe_date = objReg.tpr_rtn_pe_date;
                        objnewRegis.tpr_print_book = "N";
                        objnewRegis.tpr_vip_code = objReg.tpr_vip_code;
                        objnewRegis.tpr_vip_desc = objReg.tpr_vip_desc;
                        objnewRegis.tpr_type = 'N';
                        objnewRegis.tpr_check_pending = 'N';
                        objnewRegis.tpr_foreigner = objReg.tpr_foreigner;
                        objnewRegis.tpr_queue_type = '5';
                        objnewRegis.tpr_patient_type = objReg.tpr_patient_type;
                        objnewRegis.tpr_new_patient = objReg.tpr_new_patient;
                        objnewRegis.tpr_company_id = objReg.tpr_company_id;
                        objnewRegis.tpr_employee_no = objReg.tpr_employee_no;
                        objnewRegis.tpr_req_pe_bef_chkup = 'N';
                        objnewRegis.tpr_req_doctor = 'N';
                        objnewRegis.tpr_req_inorout_doctor = null;
                        objnewRegis.tpr_req_doc_gender = null;
                        objnewRegis.tpr_req_doc_code = null;
                        objnewRegis.tpr_req_doc_name = null;
                        objnewRegis.tpr_pe_doc = null;
                        objnewRegis.tpr_pe_doc_code = null;
                        objnewRegis.tpr_pe_doc_name = null;
                        objnewRegis.tpr_prev_pe_code = objReg.tpr_prev_pe_code;
                        objnewRegis.tpr_prev_pe_name = objReg.tpr_prev_pe_name;
                        objnewRegis.tpr_aviation_type = objReg.tpr_aviation_type;
                        objnewRegis.tpr_pe_type = objReg.tpr_pe_type;
                        objnewRegis.tpr_npo_time = objReg.tpr_npo_time;
                        objnewRegis.tpr_npo_text = objReg.tpr_npo_text;
                        objnewRegis.tpr_main_address = objReg.tpr_main_address;
                        objnewRegis.tpr_main_tumbon = objReg.tpr_main_tumbon;
                        objnewRegis.tpr_main_amphur = objReg.tpr_main_amphur;
                        objnewRegis.tpr_main_province = objReg.tpr_main_province;
                        objnewRegis.tpr_main_zip_code = objReg.tpr_main_zip_code;
                        objnewRegis.tpr_other_address = objReg.tpr_other_address;
                        objnewRegis.tpr_other_tumbon = objReg.tpr_other_tumbon;
                        objnewRegis.tpr_other_amphur = objReg.tpr_other_amphur;
                        objnewRegis.tpr_other_province = objReg.tpr_other_province;
                        objnewRegis.tpr_other_zip_code = objReg.tpr_other_zip_code;
                        objnewRegis.tpr_mobile_phone = objReg.tpr_mobile_phone;
                        objnewRegis.tpr_office_phone = objReg.tpr_office_phone;
                        objnewRegis.tpr_home_phone = objReg.tpr_home_phone;
                        objnewRegis.tpr_email = objReg.tpr_email;
                        objnewRegis.tpr_send_book = objReg.tpr_send_book;
                        objnewRegis.tpr_send_to = objReg.tpr_send_to;
                        objnewRegis.tpr_remark = objReg.tpr_remark;
                        objnewRegis.tpr_create_by = Program.CurrentUser.mut_username;
                        objnewRegis.tpr_create_date = dtnow;
                        objnewRegis.tpr_update_by = Program.CurrentUser.mut_username;
                        objnewRegis.tpr_update_date = dtnow;

                        dbc.trn_patient_regis.InsertOnSubmit(objnewRegis);
                    }

                    //2. insert Package Item
                    bool haveNew = false;
                    List<int> mrmselect = Getmrmid();
                    var objpendingRight = (from t1 in dbc.vw_Pending_Rights
                                           where t1.tpt_hn_no == Program.CurrentRegis.trn_patient.tpt_hn_no
                                           select t1.tpr_id).Distinct().ToList();
                    var objitempack = (from t1 in dbc.vw_Pending_Packages
                                       where objpendingRight.Contains(t1.tpr_id)
                                       && t1.mhs_id == Program.CurrentSite.mhs_id
                                       && mrmselect.Contains(t1.mrm_id)
                                       select t1);
                    foreach (var item in objitempack)
                    {
                        var tos_id = 0;
                        string set_item_row_id = item.toi_set_row_id;
                        string set_Code = item.tos_od_set_code;
                        //Check Pakcage set
                        var objset = (from t1 in dbc.trn_patient_order_sets
                                      where t1.tos_item_row_id == set_item_row_id
                                      && t1.tos_od_set_code == set_Code
                                      && t1.tpr_id == objnewRegis.tpr_id
                                      select t1);
                        if (objset.Count() == 0)
                        {
                            if (set_item_row_id != "" && set_Code != "")
                            {
                                trn_patient_order_set tmppackage = new trn_patient_order_set();
                                tmppackage.tos_item_row_id = item.tos_item_row_id;
                                tmppackage.tos_od_set_code = item.tos_od_set_code;
                                tmppackage.tos_od_set_name = item.tos_od_set_name;
                                tmppackage.tos_create_by = Program.CurrentUser.mut_username;
                                tmppackage.tos_create_date = dtnow;
                                tmppackage.tos_update_by = tmppackage.tos_create_by;
                                tmppackage.tos_update_date = dtnow;
                                objnewRegis.trn_patient_order_sets.Add(tmppackage);
                                tos_id = tmppackage.tos_id;
                                haveNew = true;
                            }
                        }
                        else
                        {
                            var currentset = objset.FirstOrDefault();
                            if (currentset != null)
                            {
                                tos_id = currentset.tos_id;
                            }
                        }

                        var icountitem = (from t1 in dbc.trn_patient_order_items
                                          where t1.tpr_id == objnewRegis.tpr_id
                                          && t1.toi_od_item_code == item.toi_od_item_code
                                          select t1).Count();
                        if (icountitem == 0)
                        {
                            trn_patient_order_item tmp = new trn_patient_order_item();
                            if (tos_id != 0)
                                tmp.tos_id = tos_id;
                            else
                                tmp.tos_id = null;

                            tmp.toi_set_row_id = item.toi_set_row_id;
                            tmp.toi_item_row_id = item.toi_item_row_id;
                            tmp.toi_od_item_code = item.toi_od_item_code;
                            tmp.toi_od_item_name = item.toi_od_item_name;
                            tmp.toi_patho = item.toi_patho;
                            tmp.toi_pac_sheet = item.toi_pac_sheet;
                            tmp.toi_carotid = item.toi_carotid;
                            tmp.toi_create_by = Program.CurrentUser.mut_username;
                            tmp.toi_create_date = dtnow;
                            tmp.toi_update_by = tmp.toi_create_by;
                            tmp.toi_update_date = dtnow;
                            tmp.tpr_id = objnewRegis.tpr_id;
                            tmp.trn_patient_regi = objnewRegis;
                            objnewRegis.trn_patient_order_items.Add(tmp);
                            haveNew = true;
                        }
                    }
                    //End add Package Set & item

                    //3 insert plan
                    if (haveNew == true)
                    { //check trn_paient _order_item   --- Update patient plan
                        var objlistddd = (from t1 in objnewRegis.trn_patient_order_items select t1.toi_item_row_id);
                        var objorderitem = objnewRegis.trn_patient_order_items.ToList();
                        if (objlistddd.Count() > 0)
                        {
                            var objorderplanList = (from t2 in dbc.mst_order_plans
                                                    where objlistddd.Contains(t2.mop_item_row_id) &&
                                                          t2.mop_status == 'A'
                                                    select new PatientOrderItemAddPlans
                                                    {
                                                        item_row_id = t2.mop_item_row_id,
                                                        mvt_id = t2.mvt_id,
                                                        patho = "",
                                                        pacSheet = ""
                                                    }).ToList().DistinctBy(p => new { p });

                            foreach (PatientOrderItemAddPlans objitem in objorderplanList)
                            {
                                var icount = (from t1 in objnewRegis.trn_patient_plans
                                              where t1.mvt_id == objitem.mvt_id
                                              select t1).Count();
                                if (icount == 0)
                                {
                                    trn_patient_plan newtpp = new trn_patient_plan();
                                    newtpp.mvt_id = objitem.mvt_id;
                                    newtpp.tpl_status = 'N';
                                    newtpp.tpl_by = 'A';
                                    newtpp.tpl_new = false;//status =1
                                    var itemor = objorderitem.Where(x => x.toi_item_row_id == objitem.item_row_id).FirstOrDefault();
                                    if (itemor != null)
                                    {
                                        DateTime arrivedate = (objReg.tpr_arrive_date != null) ? (DateTime)objReg.tpr_arrive_date : dtnow;
                                        newtpp.tpl_patho = Program.pathoReplace(itemor.toi_patho, arrivedate);
                                        newtpp.tpl_pac_sheet = Program.pacSheetReplace(itemor.toi_pac_sheet);
                                    }
                                    newtpp.tpl_create_by = Program.CurrentUser.mut_username;
                                    newtpp.tpl_create_date = dtnow;
                                    objnewRegis.trn_patient_plans.Add(newtpp);
                                }
                            }
                        }
                    }

                    //4 insert Table basic Measure_hdr && dtl
                    var objbasic = objReg.trn_basic_measure_hdrs.FirstOrDefault();
                    if (objbasic != null && isCreateReg == true)
                    {//insert Basic Measurement
                        trn_basic_measure_hdr newbmh = new trn_basic_measure_hdr();
                        newbmh.tbm_id = 0;
                        newbmh.mut_id = objbasic.mut_id;
                        newbmh.mdr_id = objbasic.mdr_id;
                        newbmh.tbm_type = objbasic.tbm_type;
                        newbmh.tbm_arrive = objbasic.tbm_arrive;
                        newbmh.tbm_arrive_text = objbasic.tbm_arrive_text;
                        newbmh.tbm_purpose = objbasic.tbm_purpose;
                        newbmh.tbm_purpose_text = objbasic.tbm_purpose_text;
                        newbmh.tbm_appearance = objbasic.tbm_appearance;
                        newbmh.tbm_appearance_text = objbasic.tbm_appearance_text;
                        newbmh.tbm_precaution = objbasic.tbm_precaution;
                        newbmh.tbm_triage = objbasic.tbm_triage;
                        newbmh.tbm_vision_right = objbasic.tbm_vision_right;
                        newbmh.tbm_vision_r_remark = objbasic.tbm_vision_r_remark;
                        newbmh.tbm_vision_left = objbasic.tbm_vision_left;
                        newbmh.tbm_vision_l_remark = objbasic.tbm_vision_l_remark;
                        newbmh.tbm_glass_or_contact = objbasic.tbm_glass_or_contact;
                        newbmh.tbm_color_blind = objbasic.tbm_color_blind;
                        newbmh.tbm_color_blind_text = objbasic.tbm_color_blind_text;
                        newbmh.tbm_doc_result_thai = objbasic.tbm_doc_result_thai;
                        newbmh.tbm_doc_result_eng = objbasic.tbm_doc_result_eng;
                        newbmh.tbm_create_by = Program.CurrentUser.mut_username;
                        newbmh.tbm_create_date = dtnow;
                        newbmh.tbm_update_by = Program.CurrentUser.mut_username;
                        newbmh.tbm_update_date = dtnow;


                        //insert Basic Mesurement dtl
                        var objdtl = objbasic.trn_basic_measure_dtls.FirstOrDefault();
                        if (objdtl != null)
                        {
                            trn_basic_measure_dtl newdtl = new trn_basic_measure_dtl();
                            newdtl.tbd_id = 0;
                            newdtl.tbm_id = objdtl.tbm_id;
                            newdtl.tbd_date = objdtl.tbd_date;
                            newdtl.tbd_height = objdtl.tbd_height;
                            newdtl.tbd_weight = objdtl.tbd_weight;
                            newdtl.tbd_bmi = objdtl.tbd_bmi;
                            newdtl.tbd_systolic = objdtl.tbd_systolic;
                            newdtl.tbd_diastolic = objdtl.tbd_diastolic;
                            newdtl.tbd_pulse = objdtl.tbd_pulse;
                            newdtl.tbd_waist = objdtl.tbd_waist;
                            newdtl.tbd_rr = objdtl.tbd_rr;
                            newdtl.tbd_temp = objdtl.tbd_temp;
                            newdtl.tbd_o2sat = objdtl.tbd_o2sat;
                            newdtl.tbd_inhale = objdtl.tbd_inhale;
                            newdtl.tbd_exhale = objdtl.tbd_exhale;
                            newdtl.tbd_mea_remark = objdtl.tbd_mea_remark;
                            newdtl.tbd_vision_lt = objdtl.tbd_vision_lt;
                            newdtl.tbd_vision_rt = objdtl.tbd_vision_rt;
                            newdtl.tbd_create_by = Program.CurrentUser.mut_username;
                            newdtl.tbd_create_date = dtnow;
                            newdtl.tbd_update_by = Program.CurrentUser.mut_username;
                            newdtl.tbd_update_date = dtnow;
                            newbmh.trn_basic_measure_dtls.Add(newdtl);
                        }
                        objnewRegis.trn_basic_measure_hdrs.Add(newbmh);
                    }

                    //5.Create patient Queue  CheckPoint B
                    trn_patient_queue newqueue = new trn_patient_queue();
                    var mvtobj = (from t1 in dbc.mst_room_events
                                  where t1.mrm_id == Program.CurrentRoom.mrm_id
                                  select t1).FirstOrDefault();
                    if (mvtobj != null)
                    {
                        var objqueue = (from t1 in dbc.trn_patient_queues
                                        where t1.tpr_id == objnewRegis.tpr_id
                                            && t1.mrm_id == Program.CurrentRoom.mrm_id
                                            && t1.mvt_id == mvtobj.mvt_id
                                        select t1).FirstOrDefault();
                        if (objqueue == null)
                        {   // insert table trn_patient_Queue
                            newqueue.mrm_id = Program.CurrentRoom.mrm_id;
                            newqueue.mvt_id = mvtobj.mvt_id;
                            newqueue.mrd_id = null;
                            newqueue.tps_start_date = dtnow;
                            newqueue.tps_end_date = dtnow;
                            newqueue.tps_status = "NS";
                            newqueue.tps_ns_status = "QL";
                            newqueue.tps_create_by = Program.CurrentUser.mut_username;
                            newqueue.tps_create_date = dtnow;
                            newqueue.tps_update_by = Program.CurrentUser.mut_username;
                            newqueue.tps_update_date = dtnow;
                            objnewRegis.trn_patient_queues.Add(newqueue);
                        }
                        else
                        { //กรณีที่เป็นคนเก่าไม่ได้ createใหม่
                            objqueue.tps_start_date = dtnow;
                            objqueue.tps_end_date = dtnow;
                            objqueue.tps_status = "NS";
                            objqueue.tps_ns_status = "QL";
                            objqueue.tps_update_by = Program.CurrentUser.mut_username;
                            objqueue.tps_update_date = dtnow;
                        }
                    }

                    //Update trn_patient_pending
                    var objalltprid = (from t1 in dbc.trn_patient_regis
                                       where t1.trn_patient.tpt_hn_no == Program.CurrentRegis.trn_patient.tpt_hn_no
                                       select t1);
                    foreach (trn_patient_regi item in objalltprid)
                    {
                        var objpatientPending = (from t1 in dbc.trn_patient_pendings
                                                 where t1.tpr_id == item.tpr_id
                                                 && mrmselect.Contains(t1.mrm_id)
                                                 select t1);

                        foreach (trn_patient_pending itempp in objpatientPending)
                        {
                            itempp.tpp_status = 'C';
                        }

                        //เปลี่ยน tpr_status==CP กรณีที่ไม่มี Pending Detail เท่านั้น
                        var icountpendingDetail = (from t1 in dbc.vw_Pending_Rights
                                                   where t1.tpr_id == item.tpr_id
                                                   select t1).Count();
                        if (icountpendingDetail == 0)
                        {
                            item.tpr_status = "CP";
                        }

                    }
                    //----------- end  ------------

                    dbc.SubmitChanges();
                    Program.CurrentPatient_queue = newqueue;
                    Program.CurrentRegis = objnewRegis;

                    // insert Get Text File from Trakcare
                    CallQueue.SetUpdateTextfile(Program.CurrentRegis.trn_patient.tpt_hn_no, Program.CurrentRegis.tpr_id);

                    return true;
                }
                catch (Exception ex)
                {
                    //dbc.Transaction.Rollback();
                    Program.MessageError("frmCheckpointB2", "CopyPatientRegis", ex, false);
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        #endregion

        private List<int> Getmrmfirstid()
        {
            List<int> plandata = new List<int>();
            for (int iRow = 0; iRow <= GridPendingDetail.Rows.Count - 1; iRow++)
            {
                Boolean isselect = Convert1.ToBoolean(GridPendingDetail["Colselect", iRow].Value);
                int mrmfirst_id = Convert1.ToInt32(GridPendingDetail["mrm_first", iRow].Value);

                if (isselect)
                {
                    plandata.Add(mrmfirst_id);
                }
            }
            return plandata;

        }

        private List<int> Getmrmid()
        {
            List<int> plandata = new List<int>();
            for (int iRow = 0; iRow <= GridPendingDetail.Rows.Count - 1; iRow++)
            {
                Boolean isselect = Convert1.ToBoolean(GridPendingDetail["Colselect", iRow].Value);
                int mrmid = Convert1.ToInt32(GridPendingDetail["Colmrm_id", iRow].Value);
                int mrmfirst_id = Convert1.ToInt32(GridPendingDetail["mrm_first", iRow].Value);

                if (isselect)
                {
                    plandata.Add(mrmid);
                }
            }
            return plandata;
            
        }
        private void SaveSiteUse()
        {
            var objregis = (from t1 in dbc.trn_patient_regis
                            where t1.tpr_id == Program.CurrentRegis.tpr_id
                            select t1).FirstOrDefault();
            if (objregis != null)
            {
                objregis.tpr_site_use = Convert1.ToInt32(DDsiteToSend.SelectedValue);
                try
                {
                    dbc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (ObjectChangeConflict occ in dbc.ChangeConflicts)
                    {
                        dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    dbc.SubmitChanges();
                }
                Program.CheckPointBSiteUse = Convert1.ToInt32(DDsiteToSend.SelectedValue);
            }
        }

        private void checkPointBDg_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridData.SetRuningNumber("ColNo");
        }

        private bool isShow = false;
        private void checkPointBDg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbAlertMsg.Text = "";
            GridPendingList.ClearSelection();
            if (isShow == false)
            {
                isShow = true;
               // frmBGScreen frmbg = new frmBGScreen(); frmbg.Show(); Application.DoEvents();
                timer1.Stop();
                if (e.RowIndex > -1)
                {
                    if (GridData["Coltprid", e.RowIndex].Value != null)
                    {
                        //tpr_id ที่เลือกอยู่
                        //select_tpr_id = (int)GridData["Coltprid", e.RowIndex].Value;
                        //btnRetive.Enabled = true;
                        statusPending = "N";
                        int tprID = Convert.ToInt32(GridData["Coltprid", e.RowIndex].Value);
                        Program.CurrentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprID select t1).FirstOrDefault();
                        int tpsID = Convert.ToInt32(GridData["Coltps_id", e.RowIndex].Value);
                        label3.Text = tprID.ToString();
                        Program.CurrentPatient_queue = (from t1 in dbc.trn_patient_queues where t1.tps_id == tpsID select t1).FirstOrDefault();
                        var objsendtosite = (from t1 in dbc.mst_hpc_sites
                                             where t1.mhs_room_chkup == true
                                             && t1.mhs_status == 'A'
                                             select new DropdownData
                                             {
                                                 Code = t1.mhs_id,
                                                 Name = t1.mhs_ename
                                             }).ToList();
                        DDsiteToSend.DataSource = objsendtosite;
                        DDsiteToSend.ValueMember = "Code";
                        DDsiteToSend.DisplayMember = "Name";
                        DDsiteToSend.SelectedValue = Program.CurrentSite.mhs_id;
                        uiUserprofile1.LoadData();
                        uiMapping1.GetMapping();
                        groupBox6.Enabled = true;
                        btnCancel.Enabled = true;
                        btnrefresh.Enabled = true;
                        //btnRetive.Enabled = true;
                        loadCheckpointBSubReCheckPackage(Program.CurrentRegis.tpr_id, Program.CheckPointBSiteUse);
                    }

                    else if (GridData["ColPending", e.RowIndex].Value != null)
                    {
                        //int tprID = Convert.ToInt32(GridData["Coltprid", e.RowIndex].Value);
                        string hnNo = GridData["Colhn_no", e.RowIndex].Value.ToString();
                        int countplan = (from t1 in dbc.trn_patient_plans
                                         where t1.tpr_id == Program.CurrentRegis.tpr_id
                                         && t1.tpl_status == 'N'
                                         select t1).Count();

                        if (countplan > 0)
                        {
                            System.Windows.Forms.MessageBox.Show("HN No : " + hnNo + "ไม่สามารถค้างตรวจแพทย์ได้ เนื่องจากยังตรวจ Station ไม่ครบ", "Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (System.Windows.Forms.MessageBox.Show("HN No : " + hnNo + " จะถูกค้างตรวจแพทย์ไว้ คุณต้องการดำเนินการต่อหรือไม่", "Pending Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                            {
                                trn_patient_regi objpatientregis = (from t1 in dbc.trn_patient_regis
                                                                    where t1.tpr_id == Program.CurrentRegis.tpr_id
                                                                    select t1).FirstOrDefault();
                                if (objpatientregis != null)
                                {
                                    //objpatientregis.tpr_status = "PD";
                                    objpatientregis.tpr_pending = true;
                                    objpatientregis.tpr_pending_no_station = true;
                                    dbc.SubmitChanges();
                                    LoadData();
                                }
                            }
                        }
                    }
                   

                    ////Columnindex=9 Retrive
                    if (e.ColumnIndex == 10 && e.RowIndex > -1 && GridData[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                    {
                        //GetPackageTakecare(Program.CurrentRegis.tpr_en_rowid, Program.CurrentRegis.tpr_id);
                        //frmCheckpointBSubReCheckPackage frmcpsubcheckpackage = new frmCheckpointBSubReCheckPackage();
                        //frmcpsubcheckpackage.tpr_id = Program.CurrentRegis.tpr_id;
                        //frmcpsubcheckpackage.ShowDialog();

                        //var objnewitem = (from mrm in dbc.mst_room_hdrs
                        //                  from tpl in dbc.trn_patient_plans
                        //                  from mre in dbc.mst_room_events
                        //                  from tpr in dbc.trn_patient_regis
                        //                  orderby mrm.mrm_ename
                        //                  where tpr.tpr_id == tpl.tpr_id
                        //                  && tpl.mvt_id == mre.mvt_id
                        //                  && mre.mrm_id == mrm.mrm_id
                        //                  && mrm.mhs_id == Program.CheckPointBSiteUse
                        //                  && tpl.tpr_id == select_tpr_id
                        //                  && (tpl.tpl_status == 'N' || tpl.tpl_status == 'H' || tpl.tpl_status == 'C')
                        //                  select new { RoomName = mrm.mrm_ename, tpl.tpl_new, mre.mrm_id, tpl.tpl_status }).Distinct();
                        //GridPackage.DataSource = objnewitem.ToList();
                        //GridPackage.Columns["Coltpl_new"].Visible = false;
                    }
                }
                isShow = false;
                timer1.Start();
              //  frmbg.Close();
            }
        }
        private void GetPackageTakecare(String ENRowID, int tpr_id)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            try
            {
                if (ENRowID == null || ENRowID == "") { return; }
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        tpr_id = Program.CurrentRegis.tpr_id;
                        int mhs_id = Program.CurrentSite.mhs_id;

                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        DateTime dateNow = Program.GetServerDateTime();

                        int enRowID = Convert.ToInt32(tpr.tpr_en_rowid);
                        EmrClass.GetPTPackageCls PackageCls = new EmrClass.GetPTPackageCls();
                        EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
                        PackageCls.AddPatientOrderItem(ref tpr, "System", dateNow, getPTPackage);
                        PackageCls.AddPatientOrderSet(ref tpr, "System", dateNow, getPTPackage);
                        List<MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                        PackageCls.AddPatientEvent(ref tpr, "System", dateNow, mapOrder);
                        PackageCls.AddPatientPlan(ref tpr, "System", dateNow, mapOrder);
                        PackageCls.skipReqDoctorOutDepartment(ref tpr);
                        PackageCls.CompleteEcho(ref tpr);
                        PackageCls.skipChangeEstToEcho(ref tpr, tpr.mhs_id);
                        PackageCls.checkOrderPMR(ref tpr, tpr.mhs_id);

                        List<string> listAJI = new List<string> { "01JMSCK",
                                                                  "01IMS",
                                                                  "01AMS",
                                                                  "01BLC" };
                        if (listAJI.Contains(tpr.mst_hpc_site.mhs_code))
                        {
                            int eventPE = cdc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                            List<trn_patient_plan> listPlan = tpr.trn_patient_plans.Where(x => x.mvt_id == eventPE).ToList();

                            foreach (trn_patient_plan plan in listPlan)
                            {
                                plan.tpl_status = 'P';
                            }
                        }
                        cdc.SubmitChanges();
                        PackageCls.setRelationOrderSet(ref tpr);
                        cdc.SubmitChanges();

                        Program.CurrentRegis = tpr;
                        cdc.Transaction.Commit();
                        loadCheckpointBSubReCheckPackage(Program.CurrentRegis.tpr_id, Program.CheckPointBSiteUse);

                        //EmrClass.GetDataFromWSTrakCare getWs = new EmrClass.GetDataFromWSTrakCare();
                        //StatusTransaction getPackage = getWs.GetPatientPackage(ref tpr, dateNow);

                        //if (getPackage == StatusTransaction.True)
                        //{
                        //    StatusTransaction genPlan = getWs.genPatientPlan(ref tpr, Program.CurrentSite.mhs_id, dateNow);
                        //    if (genPlan == StatusTransaction.True)
                        //    {

                        //        // Add Check PE from AMS JMS IMS
                        //        List<string> listAJI = new List<string> { "01JMSCK",
                        //                                                  "01IMS",
                        //                                                  "01AMS",
                        //                                                  "01BLC" };
                        //        if (listAJI.Contains(tpr.mst_hpc_site.mhs_code))
                        //        {
                        //            int eventPE = cdc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                        //            List<trn_patient_plan> listPlan = tpr.trn_patient_plans.Where(x => x.mvt_id == eventPE).ToList();
                                    
                        //            foreach (trn_patient_plan plan in listPlan)
                        //            {
                        //                plan.tpl_status = 'P';
                        //            }
                        //        }
                        //        cdc.SubmitChanges();
                        //        new EmrClass.GetDataFromWSTrakCare().setRelationOrderSet(ref tpr);
                        //        cdc.SubmitChanges();

                        //        Program.CurrentRegis = tpr;
                        //        cdc.Transaction.Commit();
                        //        loadCheckpointBSubReCheckPackage(Program.CurrentRegis.tpr_id, Program.CheckPointBSiteUse);
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError(this.Name, "GetPackageTakecare", ex, false);
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "GetPackageTakecare", ex, false);
            }
            frmbg.Close();
        }
        //private void GetPackageTakecare(String ENRowID, int tpr_id)
        //{
        //    if (Program.UseWebService == false) { return; }
        //    try
        //    {
        //        if (ENRowID == null || ENRowID == "") { return; }
        //        //Get Webservice
        //        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
        //        {
        //            EmrClass.GetDataFromWSTrakCare getWS = new EmrClass.GetDataFromWSTrakCare();
        //            int rowID = Convert.ToInt32(ENRowID);
        //            List<trn_patient_order_set> tpos = getWS.getPatientOrderSetByWS(rowID);
        //            List<trn_patient_order_item> tpoi = getWS.getPatientOrderItemByWS(rowID);

        //            tpr_id = Program.CurrentRegis.tpr_id;
        //            int mhs_id = Program.CurrentSite.mhs_id;

        //            trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

        //            List<string> oldOrderSet = tpr.trn_patient_order_sets.Select(x => x.tos_item_row_id).ToList();
        //            List<trn_patient_order_set> newOrderSet = tpos.Where(x => !oldOrderSet.Contains(x.tos_item_row_id)).ToList();

        //            var oldOrderItem = tpr.trn_patient_order_items.Select(x => new { x.toi_set_row_id, x.toi_item_row_id }).ToList();
        //            List<trn_patient_order_item> newOrderItem = tpoi.Where(x => !oldOrderItem.Contains(new { x.toi_set_row_id, x.toi_item_row_id })).ToList();

        //            DateTime dateNow = Program.GetServerDateTime();
        //            newOrderItem.ForEach(x =>
        //            {
        //                x.toi_create_by = Program.CurrentUser.mut_username;
        //                x.toi_create_date = dateNow;
        //                x.toi_update_by = Program.CurrentUser.mut_username;
        //                x.toi_update_date = dateNow;
        //            });

        //            if (newOrderSet != null)
        //            {
        //                if (newOrderSet.Count() > 0)
        //                {
        //                    tpr.trn_patient_order_sets.AddRange(newOrderSet);
        //                }
        //            }
        //            if (newOrderItem != null)
        //            {
        //                if (newOrderItem.Count() > 0)
        //                {
        //                    tpr.trn_patient_order_items.AddRange(newOrderItem);
        //                }
        //            }

        //            List<int> mvt_id = tpr.trn_patient_plans.Select(x => x.mvt_id).ToList();

        //            List<trn_patient_plan> newPatientPlan = getWS.getPatientPlan(newOrderItem, tpr.tpr_arrive_date).Where(x => !mvt_id.Contains(x.mvt_id)).ToList();
        //            if (newPatientPlan != null)
        //            {
        //                if (newPatientPlan.Count() > 0)
        //                {
        //                    newPatientPlan.ForEach(x =>
        //                    {
        //                        x.tpl_create_by = Program.CurrentUser.mut_username;
        //                        x.tpl_create_date = dateNow;
        //                    });
        //                    tpr.trn_patient_plans.AddRange(newPatientPlan);
        //                }
        //            }

        //            /*if (tpr.tpr_req_doctor == 'Y' && tpr.tpr_req_inorout_doctor == "UT")
        //            {
        //                int mvt_pe = dbc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
        //                List<trn_patient_plan> planPE = tpr.trn_patient_plans.Where(x => x.mvt_id == mvt_pe).ToList();
        //                planPE.ForEach(x => tpr.trn_patient_plans.Remove(x));
        //            }*/

        //            /*int mvt_ES = dbc.mst_events.Where(x => x.mvt_code == "ES").Select(x => x.mvt_id).FirstOrDefault();
        //            int mvt_EC = dbc.mst_events.Where(x => x.mvt_code == "EC").Select(x => x.mvt_id).FirstOrDefault();
        //            int ct_ESTECHO = tpr.trn_patient_plans.Where(x => (x.mvt_id == mvt_ES || x.mvt_id == mvt_EC)).Count();*/

        //            if (tpr.tpr_req_doctor == 'Y' && tpr.tpr_req_inorout_doctor == "UT") //&& ct_ESTECHO == 0)
        //            {
        //                int mvt_pe = dbc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
        //                List<trn_patient_plan> planPE = tpr.trn_patient_plans.Where(x => x.mvt_id == mvt_pe).ToList();
        //                planPE.ForEach(x => tpr.trn_patient_plans.Remove(x));
        //            }

        //            ////RunSkipEst
        //            Class.SkipOrderSetEST.RunSkipEST(mhs_id, ref tpr);
        //            ////RunSkipEst
        //            ////marry package
        //            Class.Pmr runprm = new Class.Pmr();
        //            runprm.RunPRM(mhs_id, ref tpr);
        //            ////marry package

        //            // Add Check PE from AMS JMS IMS
        //            int v_count = (from t1 in dbc.trn_patient_queues
        //                           join t2 in dbc.mst_events on t1.mvt_id equals t2.mvt_id
        //                           where t1.tpr_id == tpr_id
        //                           && t2.mvt_code == "PE"
        //                           select t1).Count();

        //            if (v_count > 0)
        //            {
        //                var objpatientplan = (from t1 in dbc.trn_patient_plans
        //                                      join t2 in dbc.mst_events on t1.mvt_id equals t2.mvt_id
        //                                      where t1.tpr_id == tpr_id
        //                                      && t2.mvt_code == "PE"
        //                                      select t1);
        //                foreach (trn_patient_plan item in objpatientplan)
        //                {
        //                    item.tpl_status = 'P';
        //                }
        //            }

        //            cdc.SubmitChanges();


        //            // for get tos_id in trn_patient_order_set update tos_id in trn_patient_order_item
        //            foreach (trn_patient_order_item toi in newOrderItem)
        //            {
        //                int? tos_id = tpr.trn_patient_order_sets.Where(x => x.tos_item_row_id == toi.toi_set_row_id)
        //                             .Select(x => x.tos_id).FirstOrDefault();
        //                toi.tos_id = tos_id;
        //            }
        //            cdc.SubmitChanges();
        //            Program.CurrentRegis = tpr;
        //            loadCheckpointBSubReCheckPackage(Program.CurrentRegis.tpr_id, Program.CheckPointBSiteUse);
        //        }

        //        //if (haveNew == true)
        //        //{ //check trn_paient _order_item   --- Update patient plan
        //        //    var currentPatientRegi = (from t1 in dbc.trn_patient_regis
        //        //                              where t1.tpr_id == Program.CurrentRegis.tpr_id
        //        //                              select t1).FirstOrDefault();
        //        //    var objlistddd = (from t1 in currentPatientRegi.trn_patient_order_items select t1.toi_item_row_id);
        //        //    var objorderitem = currentPatientRegi.trn_patient_order_items.ToList();
        //        //    if (objlistddd.Count() > 0)
        //        //    {
        //        //        var objorderplanList = (from t2 in dbc.mst_order_plans
        //        //                                where objlistddd.Contains(t2.mop_item_row_id)
        //        //                                select new PatientOrderItemAddPlans
        //        //                                {
        //        //                                    item_row_id = t2.mop_item_row_id,
        //        //                                    mvt_id = t2.mvt_id,
        //        //                                    patho = "",
        //        //                                    pacSheet = ""
        //        //                                }).ToList().DistinctBy(p => new { p });

        //        //        foreach (PatientOrderItemAddPlans objitem in objorderplanList)
        //        //        {
        //        //            var icount = (from t1 in currentPatientRegi.trn_patient_plans
        //        //                          where t1.mvt_id == objitem.mvt_id
        //        //                          select t1).Count();
        //        //            if (icount == 0)
        //        //            {
        //        //                trn_patient_plan newtpp = new trn_patient_plan();
        //        //                newtpp.mvt_id = objitem.mvt_id;
        //        //                newtpp.tpl_status = 'N';
        //        //                newtpp.tpl_by = 'A';
        //        //                newtpp.tpl_new = true;//status =1
        //        //                var itemor = objorderitem.Where(x => x.toi_item_row_id == objitem.item_row_id).FirstOrDefault();
        //        //                if (itemor != null)
        //        //                {
        //        //                    DateTime arrivedate = (currentPatientRegi.tpr_arrive_date != null) ? (DateTime)currentPatientRegi.tpr_arrive_date : dtnow;
        //        //                    newtpp.tpl_patho = Program.pathoReplace(itemor.toi_patho, arrivedate);
        //        //                    newtpp.tpl_pac_sheet = Program.pacSheetReplace(itemor.toi_pac_sheet);
        //        //                }
        //        //                newtpp.tpl_create_by = Program.CurrentUser.mut_username;
        //        //                newtpp.tpl_create_date = dtnow;
        //        //                currentPatientRegi.trn_patient_plans.Add(newtpp);
        //        //                dbc.SubmitChanges();
        //        //            }
        //        //        }
                        
        //        //    }
        //        //}
        //        //LoadData();

        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError("=>GetPackageTakecare :" + ex.Message);
        //    }
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lbAlertMsg.Text = string.Empty;
            frmBGScreen frmbg = new frmBGScreen(); 
            frmbg.Show();
            Application.DoEvents();

            int tpr_id = Program.CurrentRegis.tpr_id;
            int mhs_id = Program.CheckPointBSiteUse;
            timer1.Stop();

            StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, Program.CurrentRoom.mrm_id);
            if (checkPatientOnCheckB == StatusTransaction.True)
            {
                if (Program.CurrentPatient_queue != null && Program.CurrentRegis != null)
                {
                    if (!CallQueue.IsStatusED())
                    {
                        string QueueNo = Program.CurrentRegis.tpr_queue_no;
                        frmChoiceRoomCancel frmcancel = new frmChoiceRoomCancel();
                        frmcancel.ShowDialog();
                        //bool complete = frmcancel.complete;
                        //if (!complete)
                        //{
                        //    lbAlertMsg.Text = "ระบบเกิดข้อผิดพลาด ไม่สามารถ Pending Queue " + QueueNo + " ได้ โปรดลอง Pending อีกครั้ง";
                        //}
                        loadCheckpointBSubReCheckPackage(tpr_id, mhs_id);
                    }
                }
            }
            else if (checkPatientOnCheckB == StatusTransaction.False)
            {
                lbAlertMsg.Text = "คนไข้ไม่ได้อยู่ในสถานะ ที่จะ Pending ได้";
            }
            timer1.Start();

            frmbg.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //timer1.Stop();
            //if (statusPending == "N")
            //{
            //    LoadData();
            //}
            //timer1.Start();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen(); 
            frmbg.Show(); 
            Application.DoEvents();

            timer1.Stop();
            LoadData(); 
            timer1.Start();

            int siteid = Convert1.ToInt32(DDSiteSearch.SelectedValue);
            bool? getother = (from t in dbc.mst_hpc_sites where t.mhs_id == siteid select t.mhs_other_clinic).FirstOrDefault();
            if (getother == true)
            {
                GridData.Columns["ColPending"].Visible = false;
            }
            else
            {
                GridData.Columns["ColPending"].Visible = true;
            }

            frmbg.Close();
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                frmBGScreen frmbg = new frmBGScreen(); 
                frmbg.Show(); 
                Application.DoEvents();

                btnSearch.Focus();
                btnSearch.Enabled = false;
                timer1.Stop();
                LoadData(); 
                timer1.Start();
                btnSearch.Enabled = true;

                frmbg.Close();
            }
        }

        private void frmCheckpointB2_FormClosing(object sender, FormClosingEventArgs e)
        {
            CallQueue.GetStatusPending = null;
            timer1.Stop();
        }

        private void GridPendingList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPendingList.SetRuningNumber();
        }

        private void GridPendingList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex==1 ,btnDel column
            if (e.ColumnIndex == 1 && e.RowIndex>-1)
            {
                string HNno = Convert1.ToString(GridPendingList["ColHNPending", e.RowIndex].Value);
                if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    StatusTransaction cancelPend = new Class.FunctionDataCls().cancelPending(HNno);
                    if (cancelPend == StatusTransaction.Error)
                    {
                        lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถยกเลิกได้ กรุณากดปุ่ม ยกเลิก อีกครั้ง";
                    }
                    //var objtpt = (from t1 in dbc.trn_patients
                    //              where t1.tpt_hn_no == HNno
                    //              select t1).FirstOrDefault();

                    //var objalltprid=(from t1 in dbc.trn_patient_regis 
                    //                 where t1.trn_patient.tpt_hn_no==HNno
                    //                 select  t1.tpr_id).ToList();

                    //var objpatientPending = (from t1 in dbc.trn_patient_pendings
                    //                         where objalltprid.Contains(t1.tpr_id)
                    //                         select t1);

                    //foreach (trn_patient_pending item in objpatientPending)
                    //{
                    //    item.tpp_status = 'C';
                    //}
                    //dbc.SubmitChanges();

                    //var objalltprs = (from t1 in dbc.trn_patient_regis
                    //                   where t1.trn_patient.tpt_hn_no == HNno
                    //                   select t1);
                    //foreach (trn_patient_regi item in objalltprs)
                    //{
                    //    //item.tpr_status = "CP";
                    //    item.tpr_pending = false;
                    //}
                    //dbc.SubmitChanges();

                }
                //LoadPendingdata("");
                LoadPendingDetail("");
            }
        }
        private void GridPendingList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbAlertMsg.Text = "";
            GridData.ClearSelection();
            if (e.RowIndex > -1)
            {
                string package = Convert1.ToString(GridPendingList["Colpackage", e.RowIndex].Value);
                DateTime datenow = Program.GetServerDateTime();
                if (package != "")
                {
                    var getobj = (from t in dbc.mst_room_hdrs
                                  where t.mrm_code == "PT"
                                  && t.mhs_id == Convert.ToInt16(DDsiteToSend.SelectedValue)
                                  && datenow >= t.mrm_effective_date.Value
                                  && (t.mrm_expire_date != null ? (datenow <= t.mrm_expire_date.Value) : true)
                                  select t.mrm_id).Distinct().FirstOrDefault();
                }
                else
                {
                   
                }

                DDsiteToSend.SelectedValue = Program.CurrentSite.mhs_id;
                string HNno = Convert1.ToString(GridPendingList["ColHNPending", e.RowIndex].Value);
                try
                {
                    Program.CurrentRegis = (from t1 in dbc.trn_patient_regis
                                            orderby t1.tpr_arrive_date descending
                                            where t1.trn_patient.tpt_hn_no == HNno
                                            select t1).FirstOrDefault();
                    uiUserprofile1.LoadData();
                    uiMapping1.GetMapping();
                    statusPending = "P";
                    LoadPendingDetail(HNno);
                    groupBox6.Enabled = true;
                    btnCancel.Enabled = false;
                    btnrefresh.Enabled = true;
                    //btnRetive.Enabled = true;
                }
                catch (Exception ex)
                {
                    Program.MessageError("frmCheckpointB2", "GridPendingList_CellClick", ex, false);
                }
            }
        }

        private void LoadPendingDetail(string HNno)
        {
            var objPendingDetailList = (from t1 in dbc.vw_Pending_Rights
                                        where t1.tpt_hn_no==HNno && t1.mhs_id == Convert.ToInt16(DDsiteToSend.SelectedValue)
                                        orderby t1.s_date,t1.mrm_ename
                                        select t1);
            GridPendingDetail.DataSource = objPendingDetailList;
            GridPendingDetail.Columns["Coltpr_id"].Visible = false;
            GridPendingDetail.Columns["Colmrm_id"].Visible = false;
            GridPendingDetail.Columns["ColHNno"].Visible = false;
            if (GridPendingDetail.CurrentRow != null)
            {  GridPendingDetail.CurrentRow.Selected = false; }
        }

        private void btnSearchPending_Click(object sender, EventArgs e)
        {
            LoadPendingdata(txtSearchPending.Text.Trim());
            LoadPendingDetail(txtSearchPending.Text.Trim());
            //noina comment
            //Clear
            //var objPendingDetailList = (from t1 in dbc.trn_patient_pendings
            //                            orderby t1.trn_patient_regi.tpr_arrive_date, t1.mst_room_hdr.mrm_ename
            //                            where t1.trn_patient_regi.trn_patient.tpt_hn_no == ""
            //                                && t1.tpp_status == 'x'
            //                            select new
            //                            {
            //                                //tpp_id = t1.tpp_id,
            //                                mrm_ename = t1.mst_room_hdr.mrm_ename,
            //                                s_date = t1.trn_patient_regi.tpr_arrive_date
            //                            }).Distinct().ToList();
            //GridPendingDetail.DataSource = objPendingDetailList;
            //GridPendingDetail.Columns["Coltpr_id"].Visible = false;
            //GridPendingDetail.Columns["Colmrm_id"].Visible = false;
            //GridPendingDetail.Columns["ColHNno"].Visible = false;

            //ใช้อันนี้แทน Add by Noina

            if (GridPendingDetail.CurrentRow != null)
            {
                GridPendingDetail.CurrentRow.Selected = false;
            }
        }
        private void GridPendingDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPendingDetail.SetRuningNumber();
            //for (int i = 0; i < GridPendingDetail.Rows.Count; i++)
            //{
            //    if (GridPendingDetail["Colflag", i].Value.ToString() == "Y")
            //    {
            //        GridPendingDetail.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
            //        GridPendingDetail.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
            //    }
            //}
        }
        private void GridPendingDetail_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            vw_Pending_Right data = (vw_Pending_Right)dgv.Rows[e.RowIndex].DataBoundItem;
            switch (data.PRM_flag)
            {
                case "Y":
                    dgv.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    dgv.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.Red;
                    break;
            }
        }

        private void GridPendingDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //click Delete button
            if(e.RowIndex>-1 && e.ColumnIndex==2){
                if (MessageBox.Show("Do you want to delete?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int tpr_id = Convert1.ToInt32(GridPendingDetail["Coltpr_id", e.RowIndex].Value);
                    string mrm_ename = GridPendingDetail["Colstation", e.RowIndex].Value.ToString();
                    List<int> list_mrm_id;
                    string hn_no = "";
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        list_mrm_id = dbc.mst_room_hdrs.Where(x => x.mrm_ename == mrm_ename).Select(x => x.mrm_id).ToList();
                        hn_no = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.trn_patient.tpt_hn_no).FirstOrDefault();
                    }
                    StatusTransaction pending = new Class.FunctionDataCls().cancelPendingStation(tpr_id, list_mrm_id);
                    if (pending == StatusTransaction.Error)
                    {
                        lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถ ยกเลิก ได้ กรุณากดปุ่ม ยกเลิก อีกครั้ง";
                    }
                    LoadPendingDetail(hn_no);

                    //var objtpridlist = (from t1 in dbc.trn_patient_regis
                    //                    where t1.trn_patient.tpt_hn_no == HNno
                    //                    select t1.tpr_id).ToList();

                    //int mrm_id = Convert.ToInt32(GridPendingDetail["Colmrm_id", e.RowIndex].Value);
                    //var objtpp = (from t1 in dbc.trn_patient_pendings
                    //              where objtpridlist.Contains(t1.tpr_id)
                    //              && t1.mrm_id == mrm_id
                    //              select t1);

                    //var patientPendingObj = (from patientPending in dbc.trn_patient_pendings
                    //                         join roomHdr in dbc.mst_room_hdrs on patientPending.mrm_id equals roomHdr.mrm_id
                    //                         where roomHdr.mrm_ename == GridPendingDetail["Colstation", e.RowIndex].Value.ToString()
                    //                         && patientPending.tpr_id == tpr_id
                    //                         select patientPending).ToList();
                    //if (patientPendingObj != null)
                    //{
                    //    foreach (var itemtpp in patientPendingObj)
                    //    {
                    //        itemtpp.tpp_status = 'C';
                    //    }
                    //    dbc.SubmitChanges();
                    //    LoadPendingDetail(HNno);
                    //}

                    //var ct_Result = (from t in dbc.trn_patient_regis where t.tpr_id == tpr_id select t.tpr_pe_status).FirstOrDefault();
                    //int ct_Pending = (from t in dbc.trn_patient_pendings where t.tpr_id == tpr_id && t.tpp_status == 'P' select t).Count();

                    //if (ct_Result != "RS" && ct_Pending == 0)
                    //{
                    //    var Update_Regis = (from t in dbc.trn_patient_regis where t.tpr_id == tpr_id select t).ToList();
                    //    if (Update_Regis != null)
                    //    {
                    //        foreach (var t in Update_Regis)
                    //        {
                    //            t.tpr_pending_no_station = true;
                    //        }
                    //        dbc.SubmitChanges();
                    //LoadPendingDetail(HNno);
                    //    }
                    //}

                    //foreach (var itemtpp in objtpp)
                    //{
                    //    itemtpp.tpp_status = 'C';
                    //    dbc.SubmitChanges();
                    //    LoadPendingDetail(HNno);
                    //}
                }
            }

        }

        private void GridPendingList_KeyUp(object sender, KeyEventArgs e)
        {
            int currentrow = GridPendingList.CurrentRow.Index - 1;
            if (currentrow < 0)
                currentrow = 0;
            try
            {
                string HNno = Convert1.ToString(GridPendingList["ColHNPending", currentrow].Value);
                LoadPendingDetail(HNno);
            }
            catch (Exception ex)
            {
                Program.MessageError("frmCheckpointB2", "GridPendingList_KeyUp", ex, false);
            }
        }
        private void GridPendingList_KeyDown(object sender, KeyEventArgs e)
        {
            int currentrow = GridPendingList.CurrentRow.Index + 1;
            if (currentrow == GridPendingList.Rows.Count)
                currentrow = GridPendingList.Rows.Count - 1;
            try
            {
                string HNno = Convert1.ToString(GridPendingList["ColHNPending", currentrow].Value);
                LoadPendingDetail(HNno);
            }
            catch (Exception ex)
            {
                Program.MessageError("frmCheckpointB2", "GridPendingList_KeyDown", ex, false);
            }
        }

        private void setdata()
        {
            //foreach (screeningconfirmdata item in mvtlist)
            //{
            //    var currentSelect = objplans.Where(x => x.mvt_id == item.mvt_id);
            //    if (item.IsSelect == true && currentSelect.Count() == 0)
            //    {
            //        trn_patient_plan newtpp = new trn_patient_plan();
            //        newtpp.tpr_id = Program.CurrentRegis.tpr_id;
            //        newtpp.mvt_id = item.mvt_id;
            //        newtpp.tpl_status = 'N';
            //        newtpp.tpl_by = 'A';
            //        newtpp.tpl_new = false;
            //        var patho = objplans.Where(x => x.mvt_id == item.mvt_id).FirstOrDefault();
            //        if (patho != null)
            //        {
            //            DateTime arrivedate = (currentPatientRegi.tpr_arrive_date != null) ? (DateTime)currentPatientRegi.tpr_arrive_date : datenowvalue;
            //            newtpp.tpl_patho = Program.pathoReplace(patho.tpl_patho, arrivedate);
            //            newtpp.tpl_pac_sheet = Program.pacSheetReplace(patho.tpl_pac_sheet);
            //        }
            //        newtpp.tpl_create_by = Program.CurrentUser.mut_username;
            //        newtpp.tpl_create_date = datenowvalue;
            //        dbc.trn_patient_plans.InsertOnSubmit(newtpp);
            //    }
            //    else if (item.IsSelect == false)
            //    {
            //        dbc.trn_patient_plans.DeleteAllOnSubmit(currentSelect);
            //    }
            //}
        }

        private void GridData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (IsValidCellAddress(e.RowIndex, e.ColumnIndex))
            //{
            //    GridData.Cursor = Cursors.Hand;
            //}
        }
        private void GridData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (IsValidCellAddress(e.RowIndex, e.ColumnIndex))
            {
                GridData.Cursor = Cursors.Default;
            }
        }
        private bool IsValidCellAddress(int rowIndex, int columnIndex)
        {
            if (columnIndex == 9) { return true; }
            else
            {
                return false;
            }
        }

        private void DDsiteToSend_SelectedValueChanged(object sender, EventArgs e)
        {
            if (GridPendingList.SelectedRows.Count == 0) { return; }
            LoadPendingDetail(GridPendingList.SelectedRows[0].Cells["ColHNPending"].Value.ToString());

            Program.CheckPointBSiteUse = (int)DDsiteToSend.SelectedValue;
        }
        private void GridData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridData.Columns[e.ColumnIndex].Name == "ColPending")
            {
                int tprID = Convert.ToInt32(GridData["Coltprid", e.RowIndex].Value);
                string hnNo = GridData["Colhn_no", e.RowIndex].Value.ToString();
                int countplan = (from t1 in dbc.trn_patient_plans
                                 where t1.tpr_id == tprID
                                 && t1.tpl_status == 'N'
                                 select t1).Count();
                if (countplan > 0)
                {
                    System.Windows.Forms.MessageBox.Show("HN No : " + hnNo + "ไม่สามารถค้างตรวจแพทย์ได้ เนื่องจากยังตรวจ Station ไม่ครบ", "Alert", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show("HN No : " + hnNo + " จะถูกค้างตรวจแพทย์ไว้ คุณต้องการดำเนินการต่อหรือไม่", "Pending Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                    {
                        StatusTransaction pending = new Class.FunctionDataCls().pendingPE(tprID);
                        if (pending == StatusTransaction.Error)
                        {
                            lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถ ค้างตรวจแพทย์ได้ กรุณากดปุ่ม ค้างตรวจแพทย์ อีกครั้ง";
                        }
                        LoadData();
                        //trn_patient_regi objpatientregis = (from t1 in dbc.trn_patient_regis
                        //                                    where t1.tpr_id == tprID
                        //                                    select t1).FirstOrDefault();
                        //if (objpatientregis != null)
                        //{
                        //    //objpatientregis.tpr_status = "PD";
                        //    objpatientregis.tpr_pending = true;
                        //    objpatientregis.tpr_pending_no_station = true;
                        //    dbc.SubmitChanges();
                        //    LoadData();
                        //}
                    }
                }
            }
        }
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnRetive_Click(object sender, EventArgs e)
        {
            try
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                StatusTransaction checkPatientOnCheckB = new Class.FunctionDataCls().checkStatusPatientOnCheckPointB(tpr_id, Program.CurrentRoom.mrm_id);
                if (checkPatientOnCheckB == StatusTransaction.True)
                {
                    try
                    {
                        if (Program.CurrentRegis != null)
                        {
                            GetPackageTakecare(Program.CurrentRegis.tpr_en_rowid, tpr_id);
                            //new Class.FunctionDataCls().genOrUpdateDoctorQuota(Program.CurrentRegis.tpr_id);
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
                else if (checkPatientOnCheckB == StatusTransaction.False)
                {
                    lbAlertMsg.Text = "คนไข้ไม่ได้อยู่ในสถานะ ที่จะ Retrieve Package/Order items ได้";
                }
            }
            catch
            {
                return;

            }
        }
        private void GridPackage_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPackage.SetRuningNumber();
            for (int i = 0; i < GridPackage.Rows.Count; i++)
            {
                if (GridPackage["Coltpl_new", i].Value.ToString() == "1")
                {
                    GridPackage.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    GridPackage.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                string tplstatus = GridPackage["tpl_status", i].Value.ToString();
                if (tplstatus == "C" || tplstatus == "D")
                {
                    GridPackage.Rows[i].Cells["ColReturn"].Value = "Return";
                }
            }
        }
        private void GridPackage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GridPackage.Rows.Count != 0)
                {
                    if (GridPackage.Columns[e.ColumnIndex].Name == "ColReturn")
                    {
                        //update trn_patient_plan tpl_status = C เปน N
                        int mrmid = (int)GridPackage.Rows[e.RowIndex].Cells["mrmid"].Value;
                        int tpr_id = Program.CurrentRegis.tpr_id;
                        StatusTransaction repackPending = new Class.FunctionDataCls().repackagePending(tpr_id, mrmid);
                        if (repackPending == StatusTransaction.Error)
                        {
                            lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถ return ได้ กรุณากด return อีกครั้ง";
                        }
                        //using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        //{
                        //    List<mst_room_event> objmvt = cdc.mst_room_events.Where(x => x.mrm_id == mrmid).ToList();
                        //    List<int?> mvt_id = objmvt.Select(x => (int?)x.mvt_id).ToList();
                        //    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        //    List<trn_patient_plan> tpl = tpr.trn_patient_plans.Where(x => mvt_id.Contains(x.mvt_id)).ToList();
                        //    foreach (trn_patient_plan pp in tpl)
                        //    {
                        //        pp.tpl_status = 'N';
                        //        if (pp.tpl_status == 'D')
                        //        {
                        //            List<trn_patient_pending> tpp = tpr.trn_patient_pendings.Where(x => x.mrm_id == mrmid).ToList();
                        //            tpp.ForEach(x => x.tpp_status = 'C');
                        //        }
                        //        List<trn_patient_queue> tps = tpr.trn_patient_queues.Where(x => x.mrm_id == mrmid && mvt_id.Contains(x.mvt_id)).ToList();
                        //        cdc.trn_patient_queues.DeleteAllOnSubmit(tps);
                        //    }
                        //    cdc.SubmitChanges();
                        //}
                        loadCheckpointBSubReCheckPackage(Program.CurrentRegis.tpr_id, Program.CheckPointBSiteUse);
                        //var objmvt = (from data in dbc.mst_room_events where data.mrm_id == mrmid select data).ToList();
                        //if (objmvt != null)
                        //{
                        //    foreach (var dr in objmvt)
                        //    {
                        //        var objtpp = (from data in dbc.trn_patient_plans where data.mvt_id == dr.mvt_id && data.tpr_id == Program.CurrentRegis.tpr_id && (data.tpl_status == 'C' || data.tpl_status == 'D') select data).ToList();
                        //        if (objtpp != null)
                        //        {
                        //            foreach (var dr2 in objtpp)
                        //            {
                        //                if (dr2.tpl_status == 'D')
                        //                {
                        //                    var objPending = (from t in dbc.trn_patient_pendings where t.tpr_id == Program.CurrentRegis.tpr_id && t.mrm_id == mrmid select t).FirstOrDefault();
                        //                    if (objPending != null)
                        //                    {
                        //                        objPending.tpp_status = 'C';
                        //                    }
                        //                }

                        //                dr2.tpl_status = 'N';
                        //                //delete room cancal
                        //                var objqueue = (from data in dbc.trn_patient_queues where data.tpr_id == select_tpr_id && data.mrm_id == dr.mrm_id && data.mvt_id == dr2.mvt_id select data).FirstOrDefault();
                        //                if (objqueue != null)
                        //                {
                        //                    dbc.trn_patient_queues.DeleteOnSubmit(objqueue);
                        //                }
                        //                dbc.SubmitChanges();
                        //            }
                        //        }
                        //    }
                        //}
                        //loadCheckpointBSubReCheckPackage(Program.CurrentRegis.tpr_id, Program.CheckPointBSiteUse);
                    }
                }
            }
            catch
            { return; }         
        }
        private void loadCheckpointBSubReCheckPackage(int tprid, int mhsid)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    /*var objnewitem = (from mrm in dbc.mst_room_hdrs
                                      join tpl in dbc.trn_patient_plans
                                      from mre in dbc.mst_room_events
                                      from tpr in dbc.trn_patient_regis
                                      orderby mrm.mhs_id, mrm.mrm_ename
                                      where tpr.tpr_id == tpl.tpr_id
                                      && tpl.mvt_id == mre.mvt_id
                                      && mre.mrm_id == mrm.mrm_id
                                      && mrm.mhs_id == mhsid
                                      && tpl.tpr_id == tprid
                                      && (tpl.tpl_status == 'N' || tpl.tpl_status == 'H' || tpl.tpl_status == 'C' || tpl.tpl_status == 'D')
                                      select new { RoomName = mrm.mrm_ename, tpl.tpl_new, mre.mrm_id, tpl.tpl_status }).Distinct();*/
                    var objnewitem = (from t1 in cdc.trn_patient_plans
                                      join t2 in cdc.mst_room_events on t1.mvt_id equals t2.mvt_id
                                      join t3 in cdc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                                      orderby t3.mrm_ename
                                      where t1.tpr_id == tprid
                                      && t3.mhs_id == ((t3.mrm_code == "PF") ? 2 : mhsid)
                                      //&& (t1.tpl_status == 'N' || t1.tpl_status == 'H' || t1.tpl_status == 'C' || t1.tpl_status == 'D')
                                      && (t1.tpl_status == 'N' || t1.tpl_status == 'H' || t1.tpl_status == 'C')
                                      select new
                                      {
                                          RoomName = t3.mrm_ename,
                                          t1.tpl_new,
                                          t3.mrm_id,
                                          t1.tpl_status
                                      }).Distinct();
                    GridPackage.DataSource = objnewitem.ToList();
                    GridPackage.Columns["Coltpl_new"].Visible = false;
                    foreach (DataGridViewRow row in GridPackage.Rows)
                    {
                        if (row.Cells["tpl_status"].Value == null)
                        {
                            row.Cells["ColReturn"] = new DataGridViewTextBoxCell();
                        }
                        else if (row.Cells["tpl_status"].Value.ToString() != "D" && row.Cells["tpl_status"].Value.ToString() != "C")
                        {
                            row.Cells["ColReturn"] = new DataGridViewTextBoxCell();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        private void btnprintslip_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                string queue = Program.CurrentRegis.tpr_queue_no;
                if (_statusPending == "P") queue = "8" + queue.Substring(1, queue.Length - 1);
                Report.frmPreviewReport frm = new Report.frmPreviewReport(Program.CurrentRegis.tpr_id, queue);
                //frm.previewReport();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            frmCreateConsultPatient frm = new frmCreateConsultPatient();
            string hn = frm.getHNCreateConsult();
            if (hn != null)
            {
                txtSearch.Text = hn;
                //DDSiteSearch.SelectedValue = 0;
                btnSearch_Click(null, null);
            }
        }
    }

    class orderCheckpointCdata
    {
        public int msh_id { get; set; }
        public string msh_name { get; set; }
        public int tps_id { get; set; }
        public int tpr_id{ get; set; }
        public string queue_no { get; set; }
        public string hn_no { get; set; }
        public string fullname { get; set; }
        public string tpr_pe_status { get; set; }
        public Image btnretrive { get; set; }
        public bool other_site { get; set; }
    }
    class PendingUser
    {
        public int tpr_id { get; set; }
        public string HNno { get; set; }
        public string FullName { get; set; }
    }
}
