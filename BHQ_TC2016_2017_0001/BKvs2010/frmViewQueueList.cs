using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace BKvs2010
{
    public partial class frmViewQueueList : Form
    {
        public frmViewQueueList()
        {
            InitializeComponent();
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();

        private void frmRegister2_Load(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen(); 
            frmbg.Show();
            Application.DoEvents();

            try
            {
                uiFooter1.SetTitle = "1.6 All Patient HPC Site";
                this.WindowState = FormWindowState.Maximized;
                if (!Program.IsDummy)
                {
                    // Add Button in Gridview
                    DataGridViewButtonColumn dgvButton = new DataGridViewButtonColumn();
                    dgvButton.HeaderText = "Send";
                    dgvButton.Name = "btnSend";
                    dgvButton.UseColumnTextForButtonValue = true;
                    dgvButton.Text = "Send";
                    dgvButton.Width = 40;
                    GridWaitingListSendBMS.Columns.Add(dgvButton);
                }
                LoadDataAll();
                //uiFooter1.LoadData();
                timer1.Start();
            }
            catch
            {

            }
            frmbg.Close();
        }
        public String GetDateTime(string strdate)
        {
            string[] dt = strdate.Split(':');
            string ap = (Convert.ToInt32(dt[0]) > 11) ? " PM" : " AM";
            return dt[0] + ":" + dt[1] + ap;
        }
        
        private string GetTypeTopCenter(string typedata, string time1, string time2, string ServerRowID, double litTime)
        {
            if (time2 == null || time2 == "")
            {
                if (typedata != null && typedata.Trim() != "")
                {
                    return "VIPนัดไม่ตรงเวลา";
                }
                else
                { 
                    return "Walk-in";
                }
            }
            TimeSpan arrivedtime = TimeSpan.Parse(time1);
            TimeSpan AppointTime = TimeSpan.Parse(time2);

            string strresult = "";
            
            double timeDelay = litTime;
            TimeSpan ta = arrivedtime.Subtract(AppointTime);
            TimeSpan ta2 = AppointTime.Subtract(arrivedtime);

            if (typedata != null && typedata.Trim() !="")
            {
                if (ta.TotalMinutes > timeDelay)
                {//arrivedtime - AppointTime > timeDelay
                    strresult = "VIPนัดไม่ตรงเวลา";
                }
                else if (ta2.TotalMinutes > timeDelay)
                {
                    strresult = "VIPนัดไม่ตรงเวลา";
                }
                else
                {
                    strresult = "VIPนัดตรงเวลา";
                }
            }
            else
            {
                List<string> ListServiceWalkin = new List<string>
                {
                    "3035", //Walk in (Non requested Doctor) AMS
                    "3036", //Walk in (Requested Doctor) AMS
                    "3514", //Walk in (Non requested Doctor) AMS Checkup
                    "3515", //Walk in (Requested Doctor) AMS Checkup
                    "5063", //Walk in (Non requested Doctor) Bangkok Longevity Center
                    "5064", //Walk in (Requested Doctor) Bangkok Longevity Center
                    "3109", //Walk in (Non requested Doctor) HPC Site 1
                    "3110", //Walk in (Requested Doctor) HPC Site 1
                    "3111", //Walk in (Non requested Doctor) HPC Site 2
                    "3112", //Walk in (Requested Doctor) HPC Site 2
                    "4455", //Walk in (Non requested Doctor) HPC Site 3
                    "4456", //Walk in (Requested Doctor) HPC Site 3
                    "3119", //Walk in (Non requested Doctor) IMS 
                    "3120", //Walk in (Requested Doctor) IMS 
                    "3123", //Walk in (Non requested Doctor) JMS Checkup
                    "3124", //Walk in (Requested Doctor) JMS Checkup
                    "2808"
                };
                if (ListServiceWalkin.Contains(ServerRowID))
                {
                    strresult= "Walk-in";
                }
                else
                {
                    //if (Program.CurrentSite.mhs_code == "" || Program.CurrentSite.mhs_code == "")
                    //{
                    //    return "01HPC2";
                    //}
                    if (ta.TotalMinutes > timeDelay)
                    {//arrivedtime - AppointTime > timeDelay
                        strresult = "นัดไม่ตรงเวลา";
                    }
                    else if (ta2.TotalMinutes > timeDelay)
                    {
                        strresult = "นัดไม่ตรงเวลา";
                    }
                    else
                    {
                        strresult = "นัดตรงเวลา";
                    }
                }
            }
            return strresult;
        }
        private string GetTypeWaitingList(trn_patient_regi t0)
        {
            if (t0.tpr_vip_code != null && t0.tpr_vip_code!="" && t0.tpr_appoint_type == 'L')
            {
                return "VIP นัดไม่ตรงเวลา";
            }
            else if (t0.tpr_vip_code != null && t0.tpr_vip_code != "" && t0.tpr_appoint_type == 'T')
            {
                return "VIP นัดตรงเวลา";
            }

            if (t0.tpr_arrive_type == 'W') { return "Walk-in"; }
            if (t0.tpr_arrive_type == 'A' && t0.tpr_appoint_type == 'L')
            { return "นัดไม่ตรงเวลา"; }
            else if (t0.tpr_arrive_type == 'A' && t0.tpr_appoint_type == 'T')
            {
                return "นัดตรงเวลา";
            }
            return "";
        }
        private Image GetImage(string strstatus)
        {
            Image imgicon = null;
            switch (strstatus)
            {
                case "A": imgicon = imageList1.Images[1]; break;
            }
            return imgicon;
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void LoadDataAll()
        {
            try
            {
                ((DataGridViewImageColumn)this.GridPatrentAppointment.Columns[4]).DefaultCellStyle.NullValue = null;
                this.ShowPatientAppointment();
                Application.DoEvents();
                this.showAllPendingList();
                Application.DoEvents();
                this.ShowWaitingListSendBMS();
                Application.DoEvents();
                this.showGridQueueBasicMeasureme();
                Application.DoEvents();
                this.ShowAllArrivedListWaitingtoRegister();
                Application.DoEvents();
            }
            catch
            {
                return;
               // MessageBox.Show(ex.Message);
            }
        }
//1.1 PatientAppointment
        private void ShowPatientAppointment()
        {
            GridPatrentAppointment.AutoGenerateColumns = false;
            DateTime dtnow = Program.GetServerDateTime();
                var objpatrentAppointment = (from t1 in dbc.tmp_getptappointments
                                             where t1.ctloc_code == Program.CurrentSite.mhs_code
                                             && t1.as_date.Value.Date == dtnow.Date
                                             orderby t1.as_sessstarttime ascending
                                             select new PatientAppoint11
                                             {
                                                 HN = t1.papmi_no,
                                                 Fullname = t1.fullname,
                                                 AppointTime = t1.as_sessstarttime,
                                                 status = t1.appt_status,
                                                 Arrived = GetImage(t1.appt_status)
                                             });
                GridPatrentAppointment.DataSource = objpatrentAppointment;
                //GridPatrentAppointment.DataSource = objpatrentAppointment; new SortableBindingList<PatientAppoint11>(objpatrentAppointment.Select((item, index) => new PatientAppoint11
                //{
                //    HN = item.HN,
                //    Fullname = item.Fullname,
                //    AppointTime = item.AppointTime,
                //    Arrived = item.Arrived
                //}).ToList());
                lbtitle11.Text = string.Format("1.1 Patient Appointment Today (Total {0} คน)", objpatrentAppointment.Count());
        }
        private void GridPatrentAppointment_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            PatientAppoint11 data = (PatientAppoint11)dgv.Rows[e.RowIndex].DataBoundItem;
            switch (data.status)
            {
                case "A":
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(200, 254, 200);
                    break;
            }
        }
//1.2 All Arrived List waiting to register (Total xx คน)

        private void ShowAllArrivedListWaitingtoRegister(string txtSearch = "")
        {
            double litTime = Program.GetLimitTime("WTM");
            DateTime dtnow = Program.GetServerDateTime();
            DateTime ResetDate = new DateTime(dtnow.Year, dtnow.Month, dtnow.Day, 0, 0, 0);
            //1.2 All Arrived List waiting to register (Total xx คน)

            var objwaitregist = (from t1 in dbc.vw_tmp_arrives
                                 join t2 in dbc.tmp_getptappointments on t1.appt_rowid equals t2.appt_rowid into appt_rowid
                                 from t2 in appt_rowid.DefaultIfEmpty()
                                 where ((txtSearch == null || txtSearch == "") ? true :
                                         (t1.papmi_no.Replace("-", "").Contains(txtSearch) ||
                                          t1.papmi_no.Contains(txtSearch) ||
                                          ((t1.fullname == null) ? t1.ttl_desc + t1.papmi_name + " " + t1.papmi_name2 : t1.fullname).Contains(txtSearch)))
                                 && (t1.flag_success == null || t1.flag_success == 'N')
                                 && t1.ctloc_code == Program.CurrentSite.mhs_code
                                     //&& t2.ctloc_code == Program.CurrentSite.mhs_code
                                 && t1.paadm_admdate.Value.Date == dtnow.Date
                                 //&& t2.as_date.Value.Date == dtnow.Date
                                 orderby t1.appt_arrivaltime
                                 select new Allarrive12
                                 {
                                     rowid = t1.row_id,
                                     No = 1,
                                     HN = t1.papmi_no,
                                     Name = (t1.fullname == null) ? t1.ttl_desc + t1.papmi_name + " " + t1.papmi_name2 : t1.fullname,
                                     ArriveTime = t1.appt_arrivaltime,
                                     //typedata = GetTypeTopCenter(t1.penstype_code, t1.appt_arrivaltime, t1.paadm_admtime, t1.ser_rowid.ToString(), litTime)
                                     typedata = GetTypeTopCenter(t1.penstype_code, t1.appt_arrivaltime, t2.as_sessstarttime == null ? null : t2.as_sessstarttime, t1.ser_rowid.ToString(), litTime)
                                     //typedata = GetTypeTopCenter(t1.penstype_code, t1.appt_arrivaltime, null, t1.ser_rowid.ToString(), litTime)
                                 }).Distinct().ToList();
            GridAllArrivedListWaitingtoRegister.DataSource = objwaitregist;
            //GridAllArrivedListWaitingtoRegister.DataSource = new SortableBindingList<Allarrive12>(objwaitregist.Select((item, index) => new Allarrive12
            //                        {
            //                            rowid = item.row_id,
            //                            No = index + 1,
            //                            HN = item.papmi_no,
            //                            Name = (item.fullname == null) ? item.ttl_desc + item.papmi_name + " " + item.papmi_name2 : item.fullname,
            //                            ArriveTime = item.appt_arrivaltime,
            //                            typedata = GetTypeTopCenter(item.penstype_code,item.appt_arrivaltime,item.paadm_admtime,item.ser_rowid.ToString(),litTime)
            //                        }).ToList());
            lbdataWaitingtoRegister.Text = string.Format("1.2 All Arrived List waiting to register (Total {0} คน)", GridAllArrivedListWaitingtoRegister.RowCount.ToString());
        }
// 1.3 AllPendingList
        private void showAllPendingList(string txtSearch = "")
        {
            DateTime dtnow = Program.GetServerDateTime().Date;

            var objtpr = (from t1 in dbc.trn_patient_regis
                          where ((txtSearch == null || txtSearch == "") ? true :
                                  (t1.trn_patient.tpt_hn_no.Contains(txtSearch) ||
                                  (t1.trn_patient.tpt_hn_no.Replace("-", "").Contains(txtSearch) ||
                                  ((t1.trn_patient.tpt_othername).Contains(txtSearch)))))
                          && t1.tpr_pending == true
                              //t1.tpr_status=="PD"
                          && t1.mhs_id == Program.CurrentSite.mhs_id
                          select new
                          {
                              hn = t1.trn_patient.tpt_hn_no,
                              name = t1.trn_patient.tpt_othername,
                              s_date = GetFormattedString(t1.tpr_arrive_date.Value)
                          }).ToList();


                lbdataAllPendingList.Text = string.Format("1.3 All Pending List (Total {0} คน)", objtpr.Count());

                GridPenddingList.DataSource = new SortableBindingList<Pendding13>(objtpr.Select((player, index) => new Pendding13
                                {
                                    hn = player.hn,
                                    name = player.name,
                                    arrive_date = player.s_date,
                                    no = index + 1
                                }).ToList());

        } 
//1.4 Waiting List send to Basic Measurement Station        
        private void ShowWaitingListSendBMS()
        {
            DateTime dtnow = Program.GetServerDateTime();
            var objpatientRegister = (from t1 in dbc.trn_patient_queues
                                        where t1.tps_status == "NS"
                                            && t1.tps_ns_status == "WL"
                                        && t1.trn_patient_regi.mhs_id==Program.CurrentSite.mhs_id
                                        && t1.mst_room_hdr.mrm_code=="BM"
                                        && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date 
                                        orderby t1.trn_patient_regi.tpr_arrive_date
                                        select new
                                        {
                                            tpsID=t1.tps_id,
                                            QueueNo = t1.trn_patient_regi.tpr_queue_no,
                                            HNno = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                            Name = t1.trn_patient_regi.trn_patient.tpt_othername,
                                            ArriveTime = t1.trn_patient_regi.tpr_arrive_date,
                                            ArriveType = GetTypeWaitingList(t1.trn_patient_regi)
                                        }).ToList();

            lbtitle14.Text = string.Format("1.4 Waiting List send to Basic Measurement Station (Total {0} คน)", objpatientRegister.Count().ToString());
            GridWaitingListSendBMS.DataSource = new SortableBindingList<WaitinglistSendBMS14>(objpatientRegister.Select((item, index) => new WaitinglistSendBMS14
                                    {
                                        No = index + 1,
                                        tpsID=item.tpsID,
                                        QueueNo = item.QueueNo,
                                        HN = item.HNno,
                                        Name = item.Name,
                                        ArrivedTime =(item.ArriveTime!=null)? item.ArriveTime.Value.ToString("hh:mm:ss") : "",
                                        ArrivedType = item.ArriveType
                                    }).ToList());

        } 
        private void GridWaitingListSendBMS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >-1)
            {
                if (e.ColumnIndex == 0)
                {
                    DataGridViewRow dr = GridWaitingListSendBMS.CurrentRow;
                    P_Send_manual_Ql_bm(dr);
                }
            }
        }
//1.5 Display Queue Basic Measurement
        private void showGridQueueBasicMeasureme()
        {
            DateTime dtnow = Program.GetServerDateTime();
                DateTime ResetDate = new DateTime(dtnow.Year, dtnow.Month, dtnow.Day, 0, 0, 0);
            var objdata = (from t1 in dbc.trn_patient_queues
                           where t1.tps_status == "NS"
                                && t1.tps_ns_status == "QL"
                                && t1.mst_room_hdr.mhs_id==Program.CurrentSite.mhs_id
                                && t1.mst_room_hdr.mrm_code=="BM"
                                && t1.trn_patient_regi.tpr_arrive_date.Value.Date == ResetDate
                                orderby t1.tps_bm_seq
                           select new
                           {
                               Callstatus = t1.tps_call_status,
                               QueueNo = t1.trn_patient_regi.tpr_queue_no,
                               HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                               Name = t1.trn_patient_regi.trn_patient.tpt_othername
                           }).ToList();

            var xxx = objdata.Select((xx, index) => new QueueBasicMeasurement15
            {
                no = index + 1,
                Callstatus = xx.Callstatus,
                QueueNo = xx.QueueNo,
                HN = xx.HN,
                Name = xx.Name
            });
            GridQueueBasicMeasureme.DataSource = new SortableBindingList<QueueBasicMeasurement15>(xxx.ToList());
            lbdataDisplayQueueBasic.Text = string.Format("1.5 Display Queue Basic Measurement(Total{0} คน)", objdata.Count().ToString());
        } 
      
        private static string GetFormattedString(DateTime objdate)
        {
            return objdate.ToString("dd/MM/yyyy");
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lbAlertMsg.Text = "";
            this.LoadDataAll();
            timer1.Start();
        }

        private void P_Send_manual_Ql_bm(DataGridViewRow dr)
        {
            DateTime dtnow = Program.GetServerDateTime();
            var objdata = (from t1 in dbc.trn_patient_queues
                           where t1.tps_status == "NS"
                                && t1.tps_call_status==null
                                && t1.tps_ns_status == "QL"
                                && t1.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id
                                && t1.mst_room_hdr.mrm_code == "BM"
                                && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                           orderby t1.tps_bm_seq
                           select new
                           {
                               Callstatus = t1.tps_call_status,
                               QueueNo = t1.trn_patient_regi.tpr_queue_no,
                               HN = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                               Name = t1.trn_patient_regi.trn_patient.tpt_othername
                           }).ToList();
            if (objdata.Count() > 4)
            {
                lbAlertMsg.Text = "Not Send Queue. Because Max Limit!";
                return;
            }
            string strtpsid = dr.Cells["coltpsID"].Value.ToString();
            string Qno = dr.Cells["colQueueno"].Value.ToString();

            frmSelectQueue frm = new frmSelectQueue();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int setqueue = frm.Getvalue;
                if (objdata.Count() < 5)
                {// ถ้าคนwไข้ที่รอ Queue มี<5
                    int tpsID = Convert.ToInt32(strtpsid);
                    //ส่งQueue ไป
                    
                    //ใส่ ตำแหน่ง ของ Queue ใหม่
                    var QueueBasicMeasurements = (from t1 in dbc.trn_patient_queues
                                                      where t1.tps_status == "NS"
                                                           && t1.tps_ns_status == "QL"
                                                           && t1.tps_call_status==null
                                                           && t1.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id
                                                           && t1.mst_room_hdr.mrm_code == "BM"
                                                           && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                                  orderby t1.tps_bm_seq ascending
                                                      select t1);
                    int icount = 1;
                    if (setqueue <= QueueBasicMeasurements.Count())
                    {
                        foreach (trn_patient_queue item in QueueBasicMeasurements)
                        {
                            if (icount == setqueue)
                            {
                                trn_patient_queue objqueue = (from t1 in dbc.trn_patient_queues
                                                              where t1.tps_id == tpsID
                                                              select t1).FirstOrDefault();
                                objqueue.tps_bm_seq = icount;
                                objqueue.tps_ns_status = "QL";
                                objqueue.tps_update_by = Program.CurrentUser.mut_username;
                                objqueue.tps_update_date = dtnow;
                                dbc.SubmitChanges();

                                lbAlertMsg.Text = "Send Queue " + Qno + " Completed";
                                icount = icount + 1;
                            }
                            item.tps_bm_seq = icount;
                            item.tps_ns_status = "QL";
                            item.tps_update_by = Program.CurrentUser.mut_username;
                            item.tps_update_date = dtnow;
                            dbc.SubmitChanges();
                            icount = icount + 1;
                        }
                    }
                    else
                    {

                        trn_patient_queue objqueue = (from t1 in dbc.trn_patient_queues
                                                      where t1.tps_id == tpsID
                                                      select t1).FirstOrDefault();
                        objqueue.tps_bm_seq = QueueBasicMeasurements.Count()+1;
                        objqueue.tps_ns_status = "QL";
                        objqueue.tps_update_by = Program.CurrentUser.mut_username;
                        objqueue.tps_update_date = dtnow;
                        dbc.SubmitChanges();

                        lbAlertMsg.Text = "Send Queue " + Qno + " Completed";
                        icount = icount + 1;
                    }
                    this.LoadDataAll();
                }
                else
                {
                    lbAlertMsg.Text = "Not Send Queue. Because Max Limit!";
                }
            }
        }

        //ย้ายการ send Auto to BasicMeasurement ไปเก็บไว้ใน Ticker Sql server Database แล้วคับ ซึ่งจะ Auto ทำงานเอง 
        //private void P_Send_Auto_QL_BM()
        //{
        //    try
        //    {
        //        List<trn_patient_queue> QueueList = new List<trn_patient_queue>();
        //        QueueList = CallQueue.QueueList(ref dbc);
        //        foreach (trn_patient_queue item in QueueList)
        //        {
        //            trn_patient_queue objqueue = (from t1 in dbc.trn_patient_queues
        //                                          where t1.tps_id == item.tps_id
        //                                          select t1).FirstOrDefault();
        //            objqueue.tps_ns_status = "QL";
        //            //objqueue.tps_update_by = Program.CurrentUser.mut_id;
        //            objqueue.tps_update_date = Program.GetServerDateTime();
        //            dbc.SubmitChanges();
        //        }
        //        // MessageBox.Show("ทำการส่งไปยัง Display Basic Measuremnet เรียบร้อยแล้ว", "Send Manual", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError(ex.Message);
        //    }
        //}
        //private void timeAutoSendBasicMeasurementStation_Tick(object sender, EventArgs e)
        //{
        //    timeAutoSendBasicMeasurementStation.Stop();
        //    var objIcountQL = (from t1 in dbc.trn_patient_queues
        //                       where t1.tps_status == "NS"
        //                            && t1.trn_patient_regi.mhs_id == Program.CurrentSite.mhs_id
        //                            && t1.mst_room_hdr.mrm_code == "BM"
        //                            && t1.tps_ns_status == "QL"
        //                       orderby t1.trn_patient_regi.tpr_queue_no
        //                       select t1).Count();
        //    if (objIcountQL == 0)
        //    {
        //        P_Send_Auto_QL_BM();
        //    }
        //    timeAutoSendBasicMeasurementStation.Start();
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            lbAlertMsg.Text = "";
            this.LoadDataAll();
            timer1.Start();
        }

        private void GridPatrentAppointment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Stop();
        }
        private void GridWaitingListSendBMS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Stop();
        }

        private void GridAllArrivedListWaitingtoRegister_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Stop();
        }
        private void GridAllArrivedListWaitingtoRegister_DoubleClick(object sender, EventArgs e)
        {
            if (!Program.IsDummy)
            {
                timer1.Stop();
                try
                {
                    if (GridAllArrivedListWaitingtoRegister.CurrentRow == null) { return; }
                    //int rowid = Convert.ToInt32(GridAllArrivedListWaitingtoRegister.CurrentRow.Cells[0].Value.ToString());////Commented.Akkaradech on 2013-12-25
                    int rowid = Convert.ToInt32(GridAllArrivedListWaitingtoRegister.CurrentRow.Cells["Column13"].Value.ToString());////Updated.Akkaradech on 2013-12-25
                  
                    var currentGetArride=(from t1 in dbc.tmp_getptarriveds where t1.row_id == rowid select t1).FirstOrDefault();


                    var currentGetAppoint = (from t1 in dbc.tmp_getptappointments where t1.appt_rowid == currentGetArride.appt_rowid select t1).FirstOrDefault();


                    if (currentGetArride != null)
                    {
                        conflict:
                        if (currentGetArride.flag_success == null || currentGetArride.flag_success == 'N')
                        {
                            currentGetArride.flag_success = 'Y';

                            try
                            {
                                dbc.SubmitChanges();
                            }
                            catch (System.Data.Linq.ChangeConflictException)
                            {
                                goto conflict;
                            }

                            Program.Tmp_GetPtarrived = currentGetArride;
                            if (currentGetAppoint != null)
                            {
                                Program.Tmp_GetAppointment = currentGetAppoint;
                            }
                            frmRegister frm = new frmRegister();
                            try
                            {
                                frm.patient_type = returnPatientType(GridAllArrivedListWaitingtoRegister.CurrentRow.Cells["Column12"].Value.ToString());
                            }
                            catch
                            {
                                frm.patient_type = "5";
                            }
                            frm.IsAddnew = true;
                            var iscompleted = frm.ShowDialog();
                            if (frm.Iscompleted != true)
                            {
                                currentGetArride.flag_success = 'N';
                                dbc.SubmitChanges();
                            }

                            timer1.Stop();
                            lbAlertMsg.Text = "";
                            this.LoadDataAll();
                            //uiFooter1.LoadData();
                            timer1.Start();
                        }
                        else
                        {
                            var strfullname = Program.Tmp_GetPtarrived.ttl_desc + Program.Tmp_GetPtarrived.papmi_name + " " + Program.Tmp_GetPtarrived.papmi_name2;
                            lbAlertMsg.Text = string.Format("ผู้รับบริการ {0} ถูกลงทะเบียนเรียบร้อยแล้ว", strfullname);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError(this.Name, "GridAllArrivedListWaitingtoRegister_DoubleClick", ex, false);
                }
            }
        }
        private string returnPatientType(string desc)
        {
            switch (desc)
            {
                case "VIPนัดตรงเวลา":
                    return "2";
                case "VIPนัดไม่ตรงเวลา":
                    return "3";
                case "นัดตรงเวลา":
                    return "4";
                default:
                    return "5";
            }
        }

        private void GridQueueBasicMeasureme_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Stop();
        }

        private void uiFooter1_Click(object sender, EventArgs e)
        {
        }
        private void uiFooter1_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void GridQueueBasicMeasureme_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < GridQueueBasicMeasureme.Rows.Count; i++)
            {
                if (GridQueueBasicMeasureme.Rows[i].Cells["Colstatus"].Value != null && GridQueueBasicMeasureme.Rows[i].Cells["Colstatus"].Value.ToString() == "HD")
                {
                    GridQueueBasicMeasureme.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    GridQueueBasicMeasureme.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
            }
        }
        private void GridAllArrivedListWaitingtoRegister_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridAllArrivedListWaitingtoRegister.SetRuningNumber("Column8");
        }
        private void GridPatrentAppointment_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridPatrentAppointment.SetRuningNumber("colNo");
        }

        private void frmViewQueueList_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            this.Dispose();
        }

        private void btnRetrieveRegister_Click(object sender, EventArgs e)
        {
            try
            {
                var tmpGetptarrivedObj = from ptarriveToday in
                                             (from ptarrive in dbc.tmp_getptarriveds
                                              where ptarrive.flag_success == 'Y'
                                                 && ptarrive.paadm_admdate == DateTime.Today
                                              select ptarrive)
                                         join patient in dbc.trn_patients
                                            on ptarriveToday.papmi_no equals patient.tpt_hn_no into joined
                                         from fillter in joined.DefaultIfEmpty()
                                         select new
                                         {
                                             ptarriveToday.row_id,
                                             tpt_hn_no = fillter == null ? null : fillter.tpt_hn_no                        
                                         };

                foreach (var oneTmpGetptarrive in tmpGetptarrivedObj)
                {
                    if (oneTmpGetptarrive.tpt_hn_no == null)
                    {
                        var UpdateTmpGetptarrivedObj = (from ptarrive in dbc.tmp_getptarriveds
                                                        where ptarrive.row_id == oneTmpGetptarrive.row_id
                                                        select ptarrive).FirstOrDefault();
                        if (UpdateTmpGetptarrivedObj != null)
                        {
                            UpdateTmpGetptarrivedObj.flag_success = null;
                            dbc.SubmitChanges();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnRetrieveRegister_Click", ex, false);
            }
        }

        private void GridAllArrivedListWaitingtoRegister_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ////Added.Akkaradech on 2013-12-25 [Cancel Patient]
            try
            {
                if (GridAllArrivedListWaitingtoRegister.Rows.Count > 0 && e.ColumnIndex == GridAllArrivedListWaitingtoRegister.Columns["Column6"].Index)
                {
                    int rowid = Convert.ToInt32(GridAllArrivedListWaitingtoRegister.CurrentRow.Cells["Column13"].Value.ToString());////Updated.Akkaradech on 2013-12-25
                    var currentGetArride = (from t1 in dbc.tmp_getptarriveds where t1.row_id == rowid select t1).FirstOrDefault();
                    if (currentGetArride != null)
                    {
                        if (currentGetArride.flag_success == null || currentGetArride.flag_success == 'N')
                        {
                            currentGetArride.flag_success = 'C';
                            dbc.SubmitChanges();
                            btnRefresh_Click(null, null);
                        }
                        else
                        {
                            var strfullname = Program.Tmp_GetPtarrived.ttl_desc + Program.Tmp_GetPtarrived.papmi_name + " " + Program.Tmp_GetPtarrived.papmi_name2;
                            lbAlertMsg.Text = string.Format("ผู้รับบริการ {0} ถูกลงทะเบียนเรียบร้อยแล้ว", strfullname);
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            frmDashBoardcs frm = new frmDashBoardcs();
            frm.WindowState = FormWindowState.Maximized;
            frm.ShowDialog();
        }

        private void txtSearchRegis_TextChanged(object sender, EventArgs e)
        {
            string txtSearch = ((TextBox)sender).Text;
            if (txtSearch.Length >= 2)
            {
                timer1.Stop();
                ShowAllArrivedListWaitingtoRegister(txtSearch);
            }
            else
            {
                timer1.Start();
            }
        }

        private void txtSearchPending_TextChanged(object sender, EventArgs e)
        {
            string txtSearch = ((TextBox)sender).Text;
            if (txtSearch.Length >= 2)
            {
                timer1.Stop();
                showAllPendingList(txtSearch);
            }
            else
            {
                timer1.Start();
            }
        }
    }

    class PatientAppoint11
    {
        public string HN { get; set; }
        public string Fullname { get; set; }
        public string AppointTime { get; set; }
        public Image Arrived { get; set; }
        public string status { get; set; }
    }
    class Allarrive12 {
        public int rowid { get; set; }
        public int No { get; set; }
        public string HN { get; set; }
        public string Name { get; set; }
        public string ArriveTime { get; set; }
        public string typedata { get; set; }
    }
    class Pendding13
    {
        public string hn { get; set; }
        public string name { get; set; }
        public string arrive_date { get; set; }
        public int no { get; set; }
    }
    class WaitinglistSendBMS14
    {
        public int No { get; set; }
        public int tpsID { get; set; }
        public string QueueNo { get; set; }
        public string HN { get; set; }
        public string Name { get; set; }
        public string ArrivedTime { get; set; }
        public string ArrivedType { get; set; }
    }
    class QueueBasicMeasurement15
    {
        public int no { get; set; }
        public string Callstatus { get; set; }
        public string QueueNo { get; set; }
        public string HN { get; set; }
        public string Name { get; set; }
    }
}
