using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Data.Linq.SqlClient;
namespace CheckupBO
{
    public partial class frmQueueManage : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        public frmQueueManage()
        {
            InitializeComponent();
        }
        int tpr_id=0;

        #region function
        private void HPCSITE()
        {
            try
            {
                DateTime datenow = Program.GetServerDateTime();
                var GetHpc = (from t in dbc.mst_hpc_sites
                              where t.mhs_type == 'P'
                                  && t.mhs_status == 'A'
                                  && datenow >= t.mhs_effective_date.Value
                                  && (t.mhs_expire_date != null ? (datenow <= t.mhs_expire_date.Value) : true)
                              select new DropdownData
                              {
                                  Code = t.mhs_id,
                                  Name = t.mhs_ename
                              }).ToList();
                cmbSite.DataSource = GetHpc;
                cmbSite.DisplayMember = "Name";
                cmbSite.ValueMember = "Code";
            }
            catch
            {
                return;
            }
        }

        private void Pending(int tpr)
        {
            try
            {
                var getobj = (from t in dbc.trn_patient_plans
                              join t3 in dbc.mst_room_events on t.mvt_id equals t3.mvt_id
                              join t4 in dbc.mst_room_hdrs on t3.mrm_id equals t4.mrm_id
                              where (t.tpl_status.Value.ToString().Contains("N") || t.tpl_status.Value.ToString().Contains("H"))
                              && t.tpr_id == tpr
                              select new
                              {
                                  ename = t4.mrm_ename,
                                  Gid = "",
                                  mid = ""
                              }).Distinct().ToList();
                int i = 1;
                GVPending.Rows.Clear();
                foreach (var dr in getobj)
                {
                    GVPending.Rows.Add(false,dr.ename,dr.Gid,dr.mid);
                    i++;
                }
            }
            catch
            {
            }
        }

        private void DropDownSiteSendTO()
        {
            try
            {
                DateTime datenow = Program.GetServerDateTime();
                var defaultvalue = (from t in dbc.trn_patient_queues
                                    join t2 in dbc.mst_room_hdrs
                                    on t.mrm_id equals t2.mrm_id
                                    where t.tpr_id == tpr_id
                                    orderby t.tps_create_date ascending
                                    //select t2.mrm_id).FirstOrDefault();
                                    select t2.mhs_id).FirstOrDefault();

                var getobj = (from t in dbc.mst_hpc_sites
                              where t.mhs_type == 'P'
                                  && t.mhs_room_chkup == true
                                  && t.mhs_status == 'A'
                                  && datenow >= t.mhs_effective_date.Value
                                  && (t.mhs_expire_date != null ? (datenow <= t.mhs_expire_date.Value) : true)
                              select new DropdownData
                              {
                                  Code = t.mhs_id,
                                  Name = t.mhs_ename
                              }).ToList();
                cmbSend.DataSource = getobj;
                cmbSend.DisplayMember = "Name";
                cmbSend.ValueMember = "Code";
                cmbSend.SelectedValue = defaultvalue;
            }
            catch
            {
            }
        }

        private void GET_GVSendManual(int siteID)
        {
            try
            {
                string getRM = (from t1 in dbc.trn_patient_queues
                                join t2 in dbc.mst_room_hdrs on t1.mrm_id equals t2.mrm_id
                                where t1.tpr_id == tpr_id
                                && (t1.tps_status == "NS" || t1.tps_status == "WK")
                                select t2.mrm_code).FirstOrDefault();

                var getobj = (from t1 in dbc.vw_patient_rooms
                              where t1.tpr_id == tpr_id
                              && t1.mhs_id == siteID
                              orderby t1.mze_code, t1.mrm_code
                              select new
                              {
                                  mrmid = t1.mrm_id,
                                  mvtid = t1.mvt_id,
                                  ename = t1.mrm_ename,
                                  mhsename = t1.mhs_ename,
                                  waitingperson = t1.waiting_person,
                                  waitingtime = t1.waiting_time,
                                  mrmcode = t1.mrm_code,
                                  mvtcode = t1.mvt_code,
                                  mhsid = t1.mhs_id,
                                  mzeename = t1.mze_ename
                              });

                if (getRM != null)
                {
                    getobj = getobj.Where(x => x.mrmcode != getRM);
                }

                int i = 1;
                GVSendManual.Rows.Clear();
                foreach (var dr in getobj)
                {
                    GVSendManual.Rows.Add("", dr.ename, dr.mhsename, dr.mzeename, dr.waitingperson, dr.waitingtime, dr.mrmid, dr.mvtid,dr.mrmcode, dr.mvtcode, dr.mhsid);
                    i++;
                }
            } 
            catch
            {
                return;
            }
        }

        private void PatientSearch(string strSearch) 
        {
            try
            {
                #region Commented
                //var GetPatient = (from t1 in dbc.trn_patients
                //                  join t2 in dbc.trn_patient_regis
                //                  on t1.tpt_id equals t2.tpt_id
                //                  join t3 in dbc.trn_patient_plans
                //                  on t2.tpr_id equals t3.tpr_id
                //                  where (t3.tpl_status.Value.ToString().Contains("N")
                //                   && t3.tpl_status.Value.ToString().Contains("H"))
                //                    && SqlMethods.Like(t1.tpt_first_name, "%" + txtSearch.Text + "%") 
                //                    && SqlMethods.Like(t1.tpt_pre_name, "%" + txtSearch.Text + "%") 
                //                    && SqlMethods.Like(t1.tpt_last_name, "%" + txtSearch.Text + "%") 
                //                   // && t2.mhs_id  == Convert.ToInt32(cmbSite.SelectedValue)
                //                  select new
                //                  {
                //                      tpr_id = t2.tpr_id,
                //                      hn = t2.trn_patient.tpt_hn_no,
                //                      fullname = String.Format("{0} {1}", t2.trn_patient.tpt_first_name, t2.trn_patient.tpt_last_name),
                //                      arrive_date = t2.tpr_arrive_date
                //                  }).ToList().Distinct();
                #endregion

                strSearch = strSearch.Trim();
                DateTime dtnow = dtparrivedate.Value.Date;
                var GetPatient = (from t1 in dbc.trn_patients
                                  join t2 in dbc.trn_patient_regis
                                  on t1.tpt_id equals t2.tpt_id
                                  join t3 in dbc.trn_patient_plans
                                  on t2.tpr_id equals t3.tpr_id
                                  where //t2.tpr_arrive_date.Value.Date == dtnow.Date
                                   t3.tpl_status.Value.ToString().Contains("N") || t3.tpl_status.Value.ToString().Contains("H")
                                  select new
                                  {
                                      tpr_id = t2.tpr_id,
                                      hn = t2.trn_patient.tpt_hn_no,
                                      fullname = t2.trn_patient.tpt_othername,
                                      prename = t2.trn_patient.tpt_pre_name,
                                      fname = t2.trn_patient.tpt_first_name,
                                      lname = t2.trn_patient.tpt_last_name,
                                      site = t2.mhs_id,
                                      arrive_date = t2.tpr_arrive_date,
                                      en = t2.tpr_en_no,
                                      queueno = t2.tpr_queue_no
                                  }).Distinct();

                if (dtparrivedate.Checked == true)
                {
                    GetPatient = GetPatient.Where(x => x.arrive_date.Value.Date == dtnow.Date);
                }
                if (cmbSite.SelectedValue != null && cmbSite.SelectedValue.ToString() != "0")
                {
                    GetPatient = GetPatient.Where(x => x.site == Convert.ToInt32(cmbSite.SelectedValue));
                }
                if (strSearch.Length > 0)
                {
                    GetPatient = GetPatient.Where(x => x.hn.Contains(strSearch) || x.fname.Contains(strSearch) || x.lname.Contains(strSearch) || x.en.Contains(txtSearch.Text) || x.queueno.Contains(txtSearch.Text));
                    int i = 1;
                    gvpatient.Rows.Clear();
                    foreach (var dr in GetPatient) 
                    {
                        gvpatient.Rows.Add(dr.tpr_id, i, dr.hn, dr.fullname, dr.arrive_date.Value.ToString("dd/MM/yyyy"));
                        i++;
                    }
                }
                else
                {
                    int i = 1;
                    gvpatient.Rows.Clear();
                    foreach (var dr in GetPatient)
                    {
                        gvpatient.Rows.Add(dr.tpr_id, i, dr.hn, dr.fullname, dr.arrive_date.Value.ToString("dd/MM/yyyy"));
                        i++;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private static double GetLimitTime(string mfhCode, int mhsid,InhCheckupDataContext dbc)
        {
            DateTime datenow = Program.GetServerDateTime();
            var litTime = (from t1 in dbc.mst_config_hdrs
                           where t1.mhs_id == mhsid
                                && t1.mfh_code == mfhCode
                                 && (t1.mfh_status == 'A'
                                       && (t1.mfh_effective_date == null ||
                                               (t1.mfh_effective_date.Value.Date <= datenow
                                               && (t1.mfh_expire_date == null ||t1.mfh_expire_date.Value.Date >= datenow))
                                               ))
                           select t1.mst_config_dtls.FirstOrDefault()).FirstOrDefault();
            return (double)litTime.mfd_value;
        }

        private void clrgroupbox(GroupBox gb)
        {
            foreach (Control ctl in gb.Controls)
            {
                TextBox txt; 
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.Text = string.Empty;
                }
            }
        }

        private void Queue(int tprid)
        {
            try
            {
                var objqueue = (from que in dbc.trn_patient_queues
                                join t3 in dbc.mst_room_dtls on que.mrd_id equals t3.mrd_id into joinque
                                from t3 in joinque.DefaultIfEmpty()
                                join t4 in dbc.mst_events on que.mvt_id equals t4.mvt_id
                                where que.tpr_id == tprid
                                select new
                                {
                                    station = que.mst_room_hdr.mrm_ename,
                                    room_name = t3.mrd_ename,
                                    event_name = t4.mvt_ename,
                                    status = que.tps_status,
                                    substatus = "",
                                    callstatus = que.tps_call_status,
                                    callby = que.tps_call_by,
                                    calldate = que.tps_call_date,
                                    holdby = que.tps_hold_by,
                                    holddate = que.tps_hold_date,
                                    cancelby = que.tps_cancel_by,
                                    canceldate = que.tps_cancel_date,
                                    cancelremark = que.tps_cancel_remark,
                                    StatusP = GetImage(que.tps_status),
                                }).ToList();
                int i = 1;
                gvqueue.Rows.Clear();
                foreach (var dr in objqueue)
                {
                    gvqueue.Rows.Add(dr.station, dr.room_name, dr.event_name, dr.StatusP, dr.substatus, dr.callstatus, dr.callby, dr.calldate, dr.holdby, dr.holddate, dr.cancelby, dr.canceldate, dr.cancelremark, dr.status);
                    i++;
                }
            }
            catch
            {
                return;
            }
        }

        private Image GetImage(string strstatus)
        {
            Image imgicon = null;
            switch (strstatus)
            {
                case "NS": imgicon = imageList1.Images[0]; break;
                case "WK": imgicon = imageList1.Images[1]; break;
                case "ED": imgicon = imageList1.Images[2]; break;
                case "LR": imgicon = imageList1.Images[3]; break;
                case "CL": imgicon = imageList1.Images[4]; break;
                case "WT": imgicon = imageList1.Images[6]; break;
            }
            return imgicon;
        }

        private void InsertPatient(int tprid) 
        {
            ////Without NS
            try
            {
                var SitePat = (from t1 in dbc.trn_patient_regis
                               where t1.tpr_id == tprid
                               select t1.mhs_id).FirstOrDefault();

                var mrmC = (from t1 in dbc.mst_room_hdrs
                            where t1.mrm_code == "CC"
                            && t1.mhs_id == SitePat
                            select t1.mrm_id).FirstOrDefault();

                var mvtC = (from t1 in dbc.mst_events
                            where t1.mvt_code == "CC"
                            select t1.mvt_id).FirstOrDefault();

                //Insert trn_patient_queue for Check point C
                var objqueue = (from t1 in dbc.trn_patient_queues
                                where t1.tpr_id == tprid
                                    && t1.mrm_id == mrmC
                                    && t1.mvt_id == mvtC
                                select t1).FirstOrDefault();
                if (objqueue == null)
                {
                    // insert table trn_patient_Queue
                    trn_patient_queue newqueue = new trn_patient_queue();
                    newqueue.tpr_id = tpr_id;
                    newqueue.mrm_id = mrmC;
                    newqueue.mvt_id = mvtC;
                    newqueue.mrd_id = null;
                    newqueue.tps_start_date = Program.GetServerDateTime();
                    newqueue.tps_end_date = Program.GetServerDateTime();
                    newqueue.tps_status = "WK";
                    newqueue.tps_ns_status = null;
                    newqueue.tps_create_by = Program.CurrentUser.mut_username;
                    newqueue.tps_create_date = Program.GetServerDateTime();
                    newqueue.tps_update_by = Program.CurrentUser.mut_username;
                    newqueue.tps_update_date = newqueue.tps_create_date;
                    dbc.trn_patient_queues.InsertOnSubmit(newqueue);
                }
                else
                {
                    objqueue.tps_create_date = Program.GetServerDateTime();
                    objqueue.tps_create_by = Program.CurrentUser.mut_username;
                    objqueue.tps_update_by = objqueue.tps_create_by;
                    objqueue.tps_update_date = Program.GetServerDateTime();
                }
                dbc.SubmitChanges();
            }
            catch
            {
                return;
            }
        }

        #endregion

        private void frmQueueManage_Load(object sender, EventArgs e)
        {
            HPCSITE();
            PatientSearch("");
            gvqueue.Columns["Colstatus_img"].Frozen = true;
            
            foreach (Control ctrl in groupBox2.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).ReadOnly = true;
                    ((TextBox)ctrl).BackColor = Color.White;
                }
            }
        }

        private void gvpatient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.AutoScrollPosition = new Point(0, 0);
                if (gvpatient.Rows.Count > 0)
                {
                    tpr_id = (int)gvpatient.Rows[e.RowIndex].Cells["col_pt_tpr_id"].Value;
                    label13.Text = tpr_id.ToString();

                    #region AddToTextbox

                    var objpatient = (from pt in dbc.trn_patient_regis
                                      where pt.tpr_id == tpr_id //&& pt.tpr_arrive_date.Value.Date == dtparrivedate.Value.Date
                                      select new
                                      {
                                          tpr_id = pt.tpr_id,
                                          hn = pt.trn_patient.tpt_hn_no,
                                          en = pt.tpr_en_no,
                                          pre_name = pt.trn_patient.tpt_pre_name,
                                          fname = pt.trn_patient.tpt_first_name,
                                          lname = pt.trn_patient.tpt_last_name,
                                          //fullname = String.Format("{0}{1} {2}",pt.trn_patient.tpt_pre_name, pt.trn_patient.tpt_first_name, pt.trn_patient.tpt_last_name),
                                          genger = pt.trn_patient.tpt_gender,
                                          arrive_date = pt.tpr_arrive_date,
                                          image = pt.trn_patient.tpt_image,
                                          IdCard = pt.trn_patient.tpt_id_card ?? "-",
                                          Package = pt.trn_patient.trn_patient_regis.Select(z => z.tpr_mhc_ename).FirstOrDefault(),
                                          QNo = pt.trn_patient.trn_patient_regis.Select(z => z.tpr_queue_no).FirstOrDefault()
                                      });
                    if (dtparrivedate.Checked == true)
                    {
                        objpatient = objpatient.Where(x => x.arrive_date.Value.Date == dtparrivedate.Value.Date);
                    }
                    if (objpatient != null)
                    {
                        btnPending.Enabled = true;
                        btnCancelPending.Enabled = true;
                        btnRefresh.Enabled = true;
                        txthn_no.Text = objpatient.Select(z => z.hn).FirstOrDefault();
                        txtprefix.Text = objpatient.Select(z => z.pre_name).FirstOrDefault();
                        txtfname.Text = objpatient.Select(z => z.fname).FirstOrDefault();
                        txtlname.Text = objpatient.Select(z => z.lname).FirstOrDefault();
                        txtgender.Text = objpatient.Select(z => z.genger.Value.ToString()).FirstOrDefault();

                        if (txtgender.Text == "F")
                            txtgender.Text = "Femal";
                        if (txtgender.Text == "M")
                            txtgender.Text = "Male";
                        if (txtgender.Text == "")
                            txtgender.Text = "-";

                        txtarrive_date.Text = objpatient.Select(z => z.arrive_date.Value).FirstOrDefault().ToString("dd/MM/yyyy");
                        pbpatient.Image = Program.byteArrayToImage(objpatient.Select(z => z.image.ToArray()).FirstOrDefault());
                        txten_no.Text = objpatient.Select(z => z.en).FirstOrDefault();
                        txtidcard.Text = objpatient.Select(z => z.IdCard).FirstOrDefault();
                        txtpackage.Text = objpatient.Select(z => z.Package).FirstOrDefault();
                        txtQNo.Text = objpatient.Select(z => z.QNo).FirstOrDefault();

                        #region GVQueue
                        //add data to gvqueue
                        //var RightJoin = from dept in ListOfDepartment
                        //                join employee in ListOfEmployees
                        //                on dept.ID equals employee.DeptID into joinDeptEmp
                        //                from employee in joinDeptEmp.DefaultIfEmpty()
                        //                select new
                        //                {
                        //                    EmployeeName = employee != null ? employee.Name : null,
                        //                    DepartmentName = dept.Name
                        //                };

                        var objqueue = (from que in dbc.trn_patient_queues
                                        join t3 in dbc.mst_room_dtls on que.mrd_id equals t3.mrd_id into joinque
                                        from t3 in joinque.DefaultIfEmpty()
                                        join t4 in dbc.mst_events on que.mvt_id equals t4.mvt_id
                                        where que.tpr_id == objpatient.Select(z=>z.tpr_id).FirstOrDefault()
                                        select new
                                        {
                                            station = que.mst_room_hdr.mrm_ename,
                                            //room_name = (from mrd in dbc.mst_room_dtls where mrd.mrd_id == que.mrd_id select mrd.mrd_ename).FirstOrDefault(),
                                            //event_name = (from mvt in dbc.mst_events where mvt.mvt_id == que.mvt_id select mvt.mvt_tname).FirstOrDefault(),
                                            room_name = t3.mrd_ename,
                                            event_name = t4.mvt_ename,
                                            status = que.tps_status,
                                            substatus = "",
                                            callstatus = que.tps_call_status,
                                            callby = que.tps_call_by,
                                            calldate = que.tps_call_date,
                                            holdby = que.tps_hold_by,
                                            holddate = que.tps_hold_date,
                                            cancelby = que.tps_cancel_by,
                                            canceldate = que.tps_cancel_date,
                                            cancelremark = que.tps_cancel_remark,
                                            StatusP = GetImage(que.tps_status),
                                        }).ToList();
                        int i = 1;
                        gvqueue.Rows.Clear();
                        foreach (var dr in objqueue)
                        {
                            gvqueue.Rows.Add(dr.station, dr.room_name, dr.event_name, dr.StatusP, dr.substatus, dr.callstatus, dr.callby, dr.calldate, dr.holdby, dr.holddate, dr.cancelby, dr.canceldate, dr.cancelremark, dr.status);
                            i++;
                        }
                        #endregion

                        #region CallFunction
                        Pending(tpr_id);
                        DropDownSiteSendTO();
                        int sitesend = string.IsNullOrEmpty(cmbSend.SelectedValue.ToString()) ? 1 : Convert.ToInt32(cmbSend.SelectedValue);
                        GET_GVSendManual(sitesend);
                        #endregion
                    }
                    else
                    {
                        clrgroupbox(groupBox2);
                        pbpatient.Image = null;
                        GVPending.Rows.Clear();
                        GVSendManual.Rows.Clear();
                    }
                    #endregion
                }
            }
            catch
            {
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GVPending.Rows.Clear();
            GVSendManual.Rows.Clear();
            gvqueue.Rows.Clear();
            clrgroupbox(groupBox2);
            pbpatient.Image = null;
            btnPending.Enabled = false;
            btnCancelPending.Enabled = false;
            btnRefresh.Enabled = false;
            PatientSearch(txtSearch.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtparrivedate.Value = DateTime.Today;
            btnRefresh.Enabled = false;
            PatientSearch("");
            txtSearch.Text = ""; 
            GVPending.Rows.Clear();
            GVSendManual.Rows.Clear();
            gvqueue.Rows.Clear();
            clrgroupbox(groupBox2);
            pbpatient.Image = null;
        }

        private void GVSendManual_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int mvtID, WaitingTime, mrmid, mhsid;
                string colsite;

                if (e.ColumnIndex == 0 && e.RowIndex > -1)
                {
                    string mrmName = GVSendManual["Colstation", e.RowIndex].Value.ToString();
                    string mrmcode = GVSendManual["ColmrmCode", e.RowIndex].Value.ToString();
                    string mvtcode = GVSendManual["colmvtcode", e.RowIndex].Value.ToString();
                    mrmid = Convert.ToInt32(GVSendManual["colmrmid", e.RowIndex].Value.ToString());
                    mvtID = Convert.ToInt32(GVSendManual["Colmvtid", e.RowIndex].Value.ToString());
                    WaitingTime = Utility.GetInteger(GVSendManual["ColWaitingTime", e.RowIndex].Value);
                    mhsid = Utility.GetInteger(GVSendManual["Colmhsid", e.RowIndex].Value);
                    colsite = GVSendManual["Colmhsename", e.RowIndex].Value.ToString();

                    var objroomhdr = (from t1 in dbc.mst_room_hdrs
                                      where t1.mrm_id == mrmid
                                      select t1).FirstOrDefault();

                    var ctroomeye = (from t1 in dbc.vw_patient_rooms
                                     where t1.mvt_code == "EN"
                                     && t1.tpr_id == tpr_id
                                     && t1.mhs_id == mhsid
                                     select t1).Count();

                    var ChkB = (from t1 in dbc.trn_patient_queues
                                  join t2 in dbc.mst_room_hdrs on t1.mrm_id equals t2.mrm_id
                                  join t3 in dbc.mst_events on t1.mvt_id equals t3.mvt_id
                                  where t1.tpr_id == tpr_id
                                  && t2.mrm_code == "CB"
                                  orderby t1.tps_create_date descending
                                  select t1).FirstOrDefault();

                    var Station = (from t1 in dbc.trn_patient_queues
                                   join t2 in dbc.mst_room_hdrs on t1.mrm_id equals t2.mrm_id
                                   join t3 in dbc.mst_events on t1.mvt_id equals t3.mvt_id
                                   where t1.tpr_id == tpr_id
                                   //&& t1.tps_status == "NS"
                                   orderby t1.tps_create_date descending
                                   select new
                                   {t1,t2}).FirstOrDefault();

                    if (ctroomeye > 0 && mvtcode == "EM")
                    {
                        MessageBox.Show("Can not send to " + mrmName + ", Please Send Eye Nurse Station");
                    }
                    else
                    {
                        if (ChkB == null)
                        {
                            MessageBox.Show("Can not send to " + mrmName + ", Please Wait, until Check Point B Station");
                            return;
                        }
                        else if (ChkB.tps_status == "NS")
                        {
                            if (MessageBox.Show("Stay on Check Point B, You want send to " + mrmName + ".", "Alert", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning) == DialogResult.OK)
                            {
                                //Update trn_patient_queue on Check point B
                                trn_patient_queue CurrentQueue = (from t1 in dbc.trn_patient_queues
                                                                  where t1.tps_id == ChkB.tps_id
                                                                  select t1).FirstOrDefault();
                                CurrentQueue.tps_send_by = Program.CurrentUser.mut_username;
                                CurrentQueue.tps_end_date = Program.GetServerDateTime();
                                CurrentQueue.tps_status = "ED";
                                CurrentQueue.tps_ns_status = null;
                                CurrentQueue.tps_update_by = Program.CurrentUser.mut_username;
                                CurrentQueue.tps_update_date = Program.GetServerDateTime();

                                //Insert trn_patient_queue
                                var objqueue = (from t1 in dbc.trn_patient_queues
                                                where t1.tpr_id == tpr_id
                                                    && t1.mrm_id == mrmid
                                                    && t1.mvt_id == mvtID
                                                select t1).FirstOrDefault();
                                if (objqueue == null)
                                {
                                    // insert table trn_patient_Queue
                                    trn_patient_queue newqueue = new trn_patient_queue();
                                    newqueue.tpr_id = tpr_id;
                                    newqueue.mrm_id = mrmid;
                                    newqueue.mvt_id = mvtID;
                                    newqueue.mrd_id = null;
                                    newqueue.tps_start_date = Program.GetServerDateTime();
                                    newqueue.tps_end_date = Program.GetServerDateTime();
                                    newqueue.tps_status = "NS";
                                    newqueue.tps_ns_status = "QL";
                                    newqueue.tps_create_by = Program.CurrentUser.mut_username;
                                    newqueue.tps_create_date = Program.GetServerDateTime();
                                    newqueue.tps_update_by = Program.CurrentUser.mut_username;
                                    newqueue.tps_update_date = newqueue.tps_create_date;
                                    dbc.trn_patient_queues.InsertOnSubmit(newqueue);
                                }
                                else
                                {
                                    objqueue.tps_create_date = Program.GetServerDateTime();
                                    objqueue.tps_create_by = Program.CurrentUser.mut_username;
                                    objqueue.tps_update_by = objqueue.tps_create_by;
                                    objqueue.tps_update_date = Program.GetServerDateTime();
                                }
                                dbc.SubmitChanges();
                               
                                MessageBox.Show("Send to " + mrmName + ".");
                                Pending(tpr_id);
                                GET_GVSendManual(Convert.ToInt32(cmbSend.SelectedValue.ToString()));
                                Queue(tpr_id);
                            }
                            return;
                        }
                        else
                        {
                            int siteid = Utility.GetInteger(cmbSend.SelectedValue);
                            /*var objrooms = (from t1 in dbc.mst_room_dtls
                                            where t1.mst_room_hdr.mhs_id == siteid
                                            && t1.mst_room_hdr.mrm_code == mrmcode
                                            select t1).FirstOrDefault();
                            if (objrooms != null)
                            {*/
                               // mrmid = objrooms.mrm_id;

                                if (Station == null || Station.t1.tps_status == "ED" || Station.t1.tps_status == "LR" || Station.t1.tps_status == "CL")
                                {
                                    if (MessageBox.Show("Send to " + mrmName + ".", "Important", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        #region InsertData
                                        //Insert trn_patient_queue
                                        var objqueue = (from t1 in dbc.trn_patient_queues
                                                        where t1.tpr_id == tpr_id
                                                            && t1.mrm_id == mrmid
                                                            && t1.mvt_id == mvtID
                                                        select t1).FirstOrDefault();
                                        if (objqueue == null)
                                        {
                                            // insert table trn_patient_Queue
                                            trn_patient_queue newqueue = new trn_patient_queue();
                                            newqueue.tpr_id = tpr_id;
                                            newqueue.mrm_id = mrmid;
                                            newqueue.mvt_id = mvtID;
                                            newqueue.mrd_id = null;
                                            newqueue.tps_start_date = Program.GetServerDateTime();
                                            newqueue.tps_end_date = Program.GetServerDateTime();
                                            newqueue.tps_status = "NS";
                                            newqueue.tps_ns_status = "QL";
                                            newqueue.tps_create_by = Program.CurrentUser.mut_username;
                                            newqueue.tps_create_date = Program.GetServerDateTime();
                                            newqueue.tps_update_by = Program.CurrentUser.mut_username;
                                            newqueue.tps_update_date = newqueue.tps_create_date;
                                            dbc.trn_patient_queues.InsertOnSubmit(newqueue);
                                        }
                                        else
                                        {
                                            objqueue.tps_create_date = Program.GetServerDateTime();
                                            objqueue.tps_create_by = Program.CurrentUser.mut_username;
                                            objqueue.tps_update_by = objqueue.tps_create_by;
                                            objqueue.tps_update_date = Program.GetServerDateTime();
                                        }

                                        dbc.SubmitChanges();
                                        #endregion
                                    }
                                    else
                                    {
                                        return;
                                    }

                                }
                                else if (Station.t1.tps_status == "NS")
                                {
                                    if (MessageBox.Show("Skip Station " + Station.t2.mrm_ename + ". Send to " + mrmName + ".", "Important", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        #region DeleteData
                                        if (Station != null)
                                        {
                                            var objbmd = (from t1 in dbc.trn_patient_queues
                                                          where t1.tps_id == Station.t1.tps_id
                                                          select t1).ToList();
                                            if (objbmd.Count() > 0)
                                            {
                                                dbc.trn_patient_queues.DeleteAllOnSubmit(objbmd);
                                                //dbc.SubmitChanges();
                                            }
                                        }
                                        #endregion

                                        #region InsertData
                                        //Insert trn_patient_queue
                                        var objqueue = (from t1 in dbc.trn_patient_queues
                                                        where t1.tpr_id == tpr_id
                                                            && t1.mrm_id == mrmid
                                                            && t1.mvt_id == mvtID
                                                        select t1).FirstOrDefault();
                                        if (objqueue == null)
                                        {
                                            // insert table trn_patient_Queue
                                            trn_patient_queue newqueue = new trn_patient_queue();
                                            newqueue.tpr_id = tpr_id;
                                            newqueue.mrm_id = mrmid;
                                            newqueue.mvt_id = mvtID;
                                            newqueue.mrd_id = null;
                                            newqueue.tps_start_date = Program.GetServerDateTime();
                                            newqueue.tps_end_date = Program.GetServerDateTime();
                                            newqueue.tps_status = "NS";
                                            newqueue.tps_ns_status = "QL";
                                            newqueue.tps_create_by = Program.CurrentUser.mut_username;
                                            newqueue.tps_create_date = Program.GetServerDateTime();
                                            newqueue.tps_update_by = Program.CurrentUser.mut_username;
                                            newqueue.tps_update_date = newqueue.tps_create_date;
                                            dbc.trn_patient_queues.InsertOnSubmit(newqueue);
                                        }
                                        else
                                        {
                                            objqueue.tps_create_date = Program.GetServerDateTime();
                                            objqueue.tps_create_by = Program.CurrentUser.mut_username;
                                            objqueue.tps_update_by = objqueue.tps_create_by;
                                            objqueue.tps_update_date = Program.GetServerDateTime();
                                        }

                                        dbc.SubmitChanges();
                                        #endregion
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                else if (Station.t1.tps_status == "WK")
                                {
                                    if (MessageBox.Show("Stay in " + Station.t2.mrm_ename + ". You confirm send to " + mrmName + ".", "Important", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.Yes)
                                    {
                                        #region DeleteData
                                        if (Station != null)
                                        {
                                            var objbmd = (from t1 in dbc.trn_patient_queues
                                                          where t1.tps_id == Station.t1.tps_id
                                                          select t1).ToList();
                                            if (objbmd.Count() > 0)
                                            {
                                                dbc.trn_patient_queues.DeleteAllOnSubmit(objbmd);
                                                //dbc.SubmitChanges();
                                            }
                                        }
                                        #endregion

                                        #region InsertData
                                        //Insert trn_patient_queue
                                        var objqueue = (from t1 in dbc.trn_patient_queues
                                                        where t1.tpr_id == tpr_id
                                                            && t1.mrm_id == mrmid
                                                            && t1.mvt_id == mvtID
                                                        select t1).FirstOrDefault();
                                        if (objqueue == null)
                                        {
                                            // insert table trn_patient_Queue
                                            trn_patient_queue newqueue = new trn_patient_queue();
                                            newqueue.tpr_id = tpr_id;
                                            newqueue.mrm_id = mrmid;
                                            newqueue.mvt_id = mvtID;
                                            newqueue.mrd_id = null;
                                            newqueue.tps_start_date = Program.GetServerDateTime();
                                            newqueue.tps_end_date = Program.GetServerDateTime();
                                            newqueue.tps_status = "NS";
                                            newqueue.tps_ns_status = "QL";
                                            newqueue.tps_create_by = Program.CurrentUser.mut_username;
                                            newqueue.tps_create_date = Program.GetServerDateTime();
                                            newqueue.tps_update_by = Program.CurrentUser.mut_username;
                                            newqueue.tps_update_date = newqueue.tps_create_date;
                                            dbc.trn_patient_queues.InsertOnSubmit(newqueue);
                                        }
                                        else
                                        {
                                            objqueue.tps_create_date = Program.GetServerDateTime();
                                            objqueue.tps_create_by = Program.CurrentUser.mut_username;
                                            objqueue.tps_update_by = objqueue.tps_create_by;
                                            objqueue.tps_update_date = Program.GetServerDateTime();
                                        }

                                        dbc.SubmitChanges();
                                        #endregion
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                
                                Pending(tpr_id);
                                GET_GVSendManual(Convert.ToInt32(cmbSend.SelectedValue.ToString()));
                                Queue(tpr_id);
                            /*}
                            else
                            {
                                MessageBox.Show("Cannot send to " + mrmName + " of " + colsite + ", Please Change HPC Site.");
                                return;
                            }*/
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void btnPending_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= GVPending.Rows.Count - 1; i++)
                {
                    var mrmid = (from t in dbc.mst_room_hdrs where t.mrm_ename == GVPending.Rows[i].Cells["Colname"].Value.ToString() select t.mrm_id).FirstOrDefault();
                    var mvt = (from t in dbc.mst_room_events where t.mrm_id == Convert.ToInt32(mrmid) select t.mvt_id).FirstOrDefault();
                    DateTime datenow = Program.GetServerDateTime();

                    if (Convert1.ToBoolean(GVPending[0, i].Value) == true)
                    {
                        var checkexist = (from t in dbc.trn_patient_pendings
                                          where t.tpr_id == tpr_id
                                          && t.mrm_id == mrmid
                                          && t.tpp_status == 'P'
                                          && t.tpp_create_date.Value.Date == datenow.Date
                                          select t).Count();
                        if (checkexist == 0)
                        {
                            #region Pending
                            dbc.pw_Update_PatientPending(tpr_id, Convert.ToInt32(mrmid), "pending");
                            #endregion
                        }
                        else
                        {
                            MessageBox.Show("Data Exist", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                dbc.SubmitChanges();
                Pending(tpr_id);
                GET_GVSendManual(Convert.ToInt32(cmbSend.SelectedValue.ToString()));
                Queue(tpr_id);

                var chkPlan = (from t1 in dbc.trn_patient_plans
                               where t1.tpr_id == tpr_id
                               && (t1.tpl_status == 'N' || t1.tpl_status == 'H')
                               select t1).Count();

                if (chkPlan == 0)
                {
                    MessageBox.Show("ไม่มี Station เหลืออยู่แล้ว ระบบจะทำการส่งไปห้อง Check Point C อัตโนมัติ", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    InsertPatient(tpr_id);
                }
            }
            catch
            {
                return;
            }
        }

        private void btnCancelPending_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= GVPending.Rows.Count - 1; i++)
                {
                    var mrmid = (from t in dbc.mst_room_hdrs where t.mrm_ename == GVPending.Rows[i].Cells["Colname"].Value.ToString() select t.mrm_id).FirstOrDefault();
                    var mvt = (from t in dbc.mst_room_events where t.mrm_id == Convert.ToInt32(mrmid) select t.mvt_id).FirstOrDefault();
                    if (Convert1.ToBoolean(GVPending[0, i].Value) == true)
                    {
                        dbc.pw_Update_PatientPending(tpr_id, Convert.ToInt32(mrmid), "cancel");
                    }
                }
                dbc.SubmitChanges();
                Pending(tpr_id);
                GET_GVSendManual(Convert.ToInt32(cmbSend.SelectedValue.ToString()));
                Queue(tpr_id);

                var chkPlan = (from t1 in dbc.trn_patient_plans
                               where t1.tpr_id == tpr_id
                               && (t1.tpl_status == 'N' || t1.tpl_status == 'H')
                               select t1).Count();

                if (chkPlan == 0)
                {
                    MessageBox.Show("ไม่มี Station เหลืออยู่แล้ว ระบบจะทำการส่งไปห้อง Check Point C อัตโนมัติ", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    #region Commented
                    //var SitePat = (from t1 in dbc.trn_patient_regis
                    //               where t1.tpr_id == tpr_id
                    //               select t1.mhs_id).FirstOrDefault();

                    //var mrmC = (from t1 in dbc.mst_room_hdrs
                    //            where t1.mrm_code == "CC"
                    //            && t1.mhs_id == SitePat
                    //            select t1.mrm_id).FirstOrDefault();

                    //var mvtC = (from t1 in dbc.mst_events
                    //            where t1.mvt_code == "CC"
                    //            select t1.mvt_id).FirstOrDefault();

                    ////Insert trn_patient_queue for Check point C
                    //var objqueue = (from t1 in dbc.trn_patient_queues
                    //                where t1.tpr_id == tpr_id
                    //                    && t1.mrm_id == mrmC
                    //                    && t1.mvt_id == mvtC
                    //                select t1).FirstOrDefault();
                    //if (objqueue == null)
                    //{
                    //    // insert table trn_patient_Queue
                    //    trn_patient_queue newqueue = new trn_patient_queue();
                    //    newqueue.tpr_id = tpr_id;
                    //    newqueue.mrm_id = mrmC;
                    //    newqueue.mvt_id = mvtC;
                    //    newqueue.mrd_id = null;
                    //    newqueue.tps_start_date = Program.GetServerDateTime();
                    //    newqueue.tps_end_date = Program.GetServerDateTime();
                    //    newqueue.tps_status = "WK";
                    //    newqueue.tps_ns_status = null;
                    //    newqueue.tps_create_by = Program.CurrentUser.mut_username;
                    //    newqueue.tps_create_date = Program.GetServerDateTime();
                    //    newqueue.tps_update_by = Program.CurrentUser.mut_username;
                    //    newqueue.tps_update_date = newqueue.tps_create_date;
                    //    dbc.trn_patient_queues.InsertOnSubmit(newqueue);
                    //}
                    //else
                    //{
                    //    objqueue.tps_create_date = Program.GetServerDateTime();
                    //    objqueue.tps_create_by = Program.CurrentUser.mut_username;
                    //    objqueue.tps_update_by = objqueue.tps_create_by;
                    //    objqueue.tps_update_date = Program.GetServerDateTime();
                    //}
                    //dbc.SubmitChanges();
                    #endregion

                    InsertPatient(tpr_id);
                }
            }
            catch
            {
                return;
            }
        }

        private void cmbSend_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                GET_GVSendManual(Convert.ToInt32(cmbSend.SelectedValue.ToString()));
            }
            catch
            {
                return;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (tpr_id !=0 && cmbSend.SelectedValue != null)
                {
                    Pending(tpr_id);
                    GET_GVSendManual(Convert.ToInt32(cmbSend.SelectedValue.ToString()));
                    Queue(tpr_id);
                }
            }
            catch
            {
                return;
            }
        }
    }
}
