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
    public partial class UIGridChangeDoctor : UserControl
    {
        public event changeDoctorComplete ChangeComplete;
        public delegate void changeDoctorComplete(object sender);
        private void changeDoctor()
        {
            // Make sure someone is listening to event
            if (ChangeComplete == null) return;
            ChangeComplete(this);
        }
        public event changingDoctor ChangingDoctor;
        public delegate void changingDoctor(object sender);
        private void changing()
        {
            // Make sure someone is listening to event
            if (ChangingDoctor == null) return;
            ChangingDoctor(this);
        }


        private class dataCls
        {
            public int no { get; set; }
            public int tpr_id { get; set; }
            public string queue_no { get; set; }
            public string hn { get; set; }
            public string name { get; set; }
            public string bfRoom { get; set; }
            public DateTime? time { get; set; }
            public string type { get; set; }
            public string status { get; set; }
            public string package_code { get; set; }
            public string package_name { get; set; }

            public float? point { get; set; }
            public List<dataPackageCls> package { get; set; }

            public class dataPackageCls
            {
                public string package_code { get; set; }
                public string package_name { get; set; }
       //         public bool? package_status { get; set; }
                public float? point { get; set; }
            }
        }
        
        BindingSource bindingData = new BindingSource();
        private int _mrd_id;
        private int _mut_id;
        private int mut_id
        {
            get
            {
                return _mut_id;
            }
            set
            {
                DateTime dateNow = Program.GetServerDateTime();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_user_type mut = cdc.mst_user_types.Where(x => x.mut_id == value).FirstOrDefault();
                    lbDocName.Text = mut.mut_fullname;
                    List<int?> list_mrd_id_doctor_room = cdc.mst_room_dtls.Where(x => x.mst_room_hdr.mrm_code == "DC" && x.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id).Select(x => (int?)x.mrd_id).ToList();
                    log_user_login firstLog = cdc.log_user_logins.Where(x => x.lug_start_date.Value.Date == dateNow.Date &&
                                                                             list_mrd_id_doctor_room.Contains(x.mrd_id) &&
                                                                             x.mut_id == value)
                                                 .OrderBy(x => x.lug_start_date).FirstOrDefault();
                    if (firstLog != null)
                    {
                        
                        lbLogTime.Text = "Log in time : " + firstLog.lug_start_date.Value.ToString("HH:mm");
                        //int countPatient = cdc.trn_patient_regis.Where(x => x.tpr_pe_doc_code == mut.mut_username &&
                        //                                                    x.tpr_arrive_date.Value.Date == dateNow.Date).Count();
                        //lbComplete.Text = countPatient.ToString();

                        //List<int> list_tpr_id = cdc.trn_patient_regis.Where(x => x.tpr_req_doc_code != null ? x.tpr_req_doc_code == mut.mut_username
                        //                                                                                    : x.tpr_pe_doc_code == mut.mut_username &&
                        //                                                         x.tpr_arrive_date.Value.Date == dateNow.Date)
                        //                                             .Select(x => x.tpr_id).ToList();

                        List<dataCls> result = cdc.trn_patient_regis.Where(x => (x.tpr_req_doc_code != null ? x.tpr_req_doc_code == mut.mut_username
                                                                                                            : x.tpr_pe_doc_code == mut.mut_username) &&
                                                                                x.tpr_arrive_date.Value.Date == dateNow.Date
                                                                                && x.mhs_id == Program.CurrentSite.mhs_id) 
                        //List<dataCls> result = cdc.trn_patient_regis.Where(x => list_tpr_id.Contains(x.tpr_id))
                        .Select(x => new dataCls
                        {
                            tpr_id = x.tpr_id,
                            queue_no = x.tpr_queue_no,
                            hn = x.trn_patient.tpt_hn_no,
                            //name = x.trn_patient.tpt_pre_name + x.trn_patient.tpt_first_name + " " + x.trn_patient.tpt_last_name +
                            //       (x.tpr_req_doc_code == mut.mut_username ? " (Req)" : ""),
                           // pang update  :   add (req-s)=> (same request) 
                           /* name = x.trn_patient.tpt_pre_name + x.trn_patient.tpt_first_name + " " + x.trn_patient.tpt_last_name +
                                   (x.tpr_req_same_doc == true ? " (Req-s)" : (x.tpr_req_doc_code == mut.mut_username ? " (Req)" : "")), */
                           // M Update :  add (Req-F) / (Req-M) =>  Request  gender
                            name = x.trn_patient.tpt_othername +
                                   (x.tpr_req_same_doc == true ? " (Req-S)" : x.tpr_req_doc_gender == 'M' ? " (Req-M)" : x.tpr_req_doc_gender == 'F' ? " (Req-F)" : (x.tpr_req_doc_code == mut.mut_username ? " (Req)" : "")), 
                       
                            package = x.trn_patient_order_sets.Where(y=>y.tos_status==true ).Select(y => new dataCls.dataPackageCls
                            {
                                package_code = y.tos_od_set_code,
                                package_name = y.tos_od_set_name,
                             //   package_status = y.tos_status
                            }).ToList()
                        }).ToList(); 

                        int pe_mvt_id = cdc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                        int re_mvt_id = cdc.mst_events.Where(x => x.mvt_code == "DC").Select(x => x.mvt_id).FirstOrDefault();
                       
                        List<mst_order_point> mst_point = cdc.mst_order_points.Where(x => x.mot_status == 'A' ).ToList();
                 
                        foreach (dataCls re in result)
                        {
                            foreach (var pack in re.package)
                            {
                                if (pack.package_code != null)
                                {
                                   mst_order_point mst = mst_point.Where(x => x.mot_set_code == pack.package_code ).OrderByDescending(x=>x.mot_point).FirstOrDefault();
                                 //   mst_order_point mst = mst_point.Where(x => x.mot_set_code == pack.package_code && pack.package_status == true).OrderByDescending(x => x.mot_point).FirstOrDefault();
                                   
                                    
                                    if (mst != null)
                                    {
                                        pack.point = mst.mot_point == null ? 0 : (float?)mst.mot_point;
                                    }
                                    else
                                    {
                                        pack.point = null;
                                    }
                                }
                            }
                            dataCls.dataPackageCls packDefault = re.package.OrderByDescending(x => x.point).FirstOrDefault();
                            if (packDefault != null)
                            {
                                re.package_code = packDefault.package_code;
                                re.package_name = packDefault.package_name;
                                re.point = packDefault.point;
                            }

                            List<trn_patient_queue> ListQueue = cdc.trn_patient_queues.Where(x => x.tpr_id == re.tpr_id).OrderBy(x => x.tps_create_date).ToList();
                            trn_patient_queue QueueRe = ListQueue.Where(x => x.mvt_id == re_mvt_id && x.tps_status == "ED").FirstOrDefault();
                            if (QueueRe != null)
                            {
                                re.status = "R";
                                re.time = QueueRe.tps_create_date;
                                int indexBef = ListQueue.IndexOf(QueueRe) - 1;
                                if (indexBef >= 0)
                                {
                                    re.bfRoom = ListQueue[indexBef].mst_room_hdr.mrm_ename;
                                }
                            }
                            else
                            {
                                trn_patient_queue QueuePe = ListQueue.Where(x => x.mvt_id == pe_mvt_id && x.tps_status == "ED").FirstOrDefault();
                                if (QueuePe != null)
                                {
                                    re.status = "P";
                                    re.time = QueuePe.tps_create_date;
                                    int indexBef = ListQueue.IndexOf(QueuePe) - 1;
                                    if (indexBef >= 0)
                                    {
                                        re.bfRoom = ListQueue[indexBef].mst_room_hdr.mrm_ename;
                                    }
                                }
                                else
                                {
                                    re.status = "N";
                                }
                            }
                        }
                        result = result.OrderBy(x => x.status == "N" ? 0 : x.status == "P" ? 1 : 2).ToList();
                        int i = 1;
                        result.ForEach(x => x.no = i++);
                        //List<trn_patient_queue> data = cdc.trn_patient_queues
                        //                                  .Where(x => x.mst_room_hdr.mrm_code == "DC" &&
                        //                                              x.trn_patient_regi.tpr_pe_doc_code == mut.mut_username &&
                        //                                              x.tps_create_date.Value.Date == dateNow.Date).ToList();


                        //var complete = data.Where(x => x.tps_status == "ED" &&
                        //                               x.trn_patient_regi.tpr_arrive_date.Value.Date == dateNow.Date)
                        //                   .Select(x => new
                        //                   {
                        //                       mvt_code = cdc.mst_events.Where(y => y.mvt_id == x.mvt_id).Select(y => y.mvt_code).FirstOrDefault(),
                        //                       tpr_id = x.tpr_id
                        //                   }).Distinct().ToList();
                        //var completeRE = complete.Where(x => x.mvt_code == "DC").Select(x => x.tpr_id).ToList();
                        //var completePE = complete.Where(x => x.mvt_code == "PE" && !completeRE.Contains(x.tpr_id)).Select(x => x.tpr_id).ToList();
                        //lbCompleteRE.Text = completeRE.Count().ToString();
                        //lbCompletePE.Text = completePE.Count().ToString();

                        //// Before Station
                        //int pe_mvt_id = cdc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                        //int re_mvt_id = cdc.mst_events.Where(x => x.mvt_code == "DC").Select(x => x.mvt_id).FirstOrDefault();
                        //List<dataCls> result = data.Where(x => x.tps_status == "NS" &&
                        //                                       x.tps_ns_status == "QL").ToList()
                        //                           .Select((x, index) => new dataCls
                        //                           {
                        //                               no = index + 1,
                        //                               tpr_id = x.tpr_id,
                        //                               hn = x.trn_patient_regi.trn_patient.tpt_hn_no,
                        //                               name = x.trn_patient_regi.trn_patient.tpt_pre_name + x.trn_patient_regi.trn_patient.tpt_first_name + " " + x.trn_patient_regi.trn_patient.tpt_last_name +
                        //                                      (x.trn_patient_regi.tpr_req_doc_code == mut.mut_username ? " (Req)" : ""),
                        //                               time = x.tps_create_date,
                        //                               bfRoom = cdc.trn_patient_queues.Where(y => y.tpr_id == x.tpr_id && y.tps_create_date < x.tps_create_date).OrderByDescending(y => y.tps_create_date).Select(y => y.mst_room_hdr.mrm_ename).FirstOrDefault(),
                        //                               type = x.mvt_id == pe_mvt_id ? "PE" : x.mvt_id == re_mvt_id ? "Result" : ""
                        //                           }).ToList();
                        lbCompleteRE.Text = result.Where(x => x.status == "R").Count().ToString();
                        lbCompletePE.Text = result.Where(x => x.status == "P").Count().ToString();
                        lbWating.Text = result.Count().ToString();
                        lbPoint.Text = result.Sum(x => x.point).ToString();
                        bindingData.DataSource = result;
                        gridData.DataSource = bindingData;
                    }
                    lbLogTime.Click -= new EventHandler(lbLogTime_Click);
                    lbLogTime.Click += new EventHandler(lbLogTime_Click);
                }
                _mut_id = value;
            }
        }
        private void lbLogTime_Click(object sender, EventArgs e)
        {
            changing();
            popUpLogView log = new popUpLogView();
            log.showLog(mut_id);
            changeDoctor();
        }

        public UIGridChangeDoctor()
        {
            InitializeComponent();
            gridData.AutoGenerateColumns = false;
        }

        public void showDashBoard(int mrd_id)
        {
            _mrd_id = mrd_id;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                mst_room_dtl mrd = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                lbDesc.Text = "(" + mrd.mrd_room_no + ") " + mrd.mst_room_hdr.mst_hpc_site.mhs_ename + " : " + mrd.mrd_ename;
                log_user_login lug = cdc.log_user_logins.Where(x => x.mrd_id == mrd_id && 
                                                                    x.lug_end_date == null &&
                                                                    x.lug_start_date.Value.Date == dateNow.Date)
                                        .OrderByDescending(x => x.lug_start_date).FirstOrDefault();
                if (lug != null)
                {
                    lbDesc.BackColor = Color.Green;
                    lbDocName.BackColor = Color.RoyalBlue;
                    mut_id = lug.mut_id;
                }
                else
                {
                    lbDesc.BackColor = Color.Gray;
                    lbDocName.BackColor = Color.Gray;
                    lbDocName.Text = string.Empty;
                    lbLogTime.Text = "Log in time : --:--";
                    lbLogTime.Click -= new EventHandler(lbLogTime_Click);
                    lbCompletePE.Text = string.Empty;
                    lbCompleteRE.Text = string.Empty;
                    lbWating.Text = string.Empty;
                    gridData.DataSource = new dataCls();
                }
            }
        }

        private void UIGridChangeDoctor_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(556, 294);
        }

        private void gridData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridData.Columns[e.ColumnIndex].Name == "colChageDoc" && e.RowIndex >= 0)
            {
                changing();
                dataCls result = (dataCls)bindingData.Current;
                int tpr_id = result.tpr_id;
                bool success = true;
                if (ableChangeDoctor(tpr_id))
                {
                    frmSelectDoctor frm = new frmSelectDoctor();
                    frmSelectDoctor.selectDocType type = frm.getSelectType;
                    if (ableChangeDoctor(tpr_id))
                    {
                        if (type == frmSelectDoctor.selectDocType.SelectedDoctor)
                        {
                            success = new Class.FunctionDataCls().changeDoctor(result.tpr_id, frm.get_mut_username, "PE");
                        }
                        else if (type == frmSelectDoctor.selectDocType.NoRequestDoctor)
                        {
                            success = new Class.FunctionDataCls().cancelPEDoctor(result.tpr_id);
                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถเปลี่ยนแพทย์" + Environment.NewLine + "เนื่องจากได้มีการเรียกคนไข้เข้าห้องตรวจแล้ว", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่สามารถเปลี่ยนแพทย์" + Environment.NewLine + "เนื่องจากได้มีการเรียกคนไข้เข้าห้องตรวจแล้ว", "Alert.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                changeDoctor();
            }
        }

        private bool ableChangeDoctor(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                int mrm_id = cdc.mst_room_dtls.Where(x => x.mrd_id == _mrd_id).Select(x => x.mrm_id).FirstOrDefault();
                List<int> list_mvt_id = cdc.mst_events.Where(x => x.mvt_code == "DC" || x.mvt_code == "PE").Select(x => x.mvt_id).ToList();
                trn_patient_queue tps = cdc.trn_patient_queues.Where(x => x.tpr_id == tpr_id && x.mrm_id == mrm_id && list_mvt_id.Contains((int)x.mvt_id) && x.tps_status == "NS" && x.tps_ns_status == "QL").FirstOrDefault();
                if (tps != null)
                {
                    return true;
                }
                return false;
            }
        }

        private void getListReqDoctor(string docCode)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                cdc.trn_patient_regis.Where(x => x.tpr_req_doc_code == docCode && x.tpr_arrive_date.Value.Date == dateNow.Date).ToList();
            }
        }

        public override void Refresh()
        {
            showDashBoard(_mrd_id);
            base.Refresh();
        }

        private void gridData_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            dataCls data = (dataCls)dgv.Rows[e.RowIndex].DataBoundItem;
            switch (data.status)
            {
                case "P":
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 241, 7);
                    break;
                case "R":
                    dgv.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(41, 242, 13);
                    break;
            }
        }
    }
}
