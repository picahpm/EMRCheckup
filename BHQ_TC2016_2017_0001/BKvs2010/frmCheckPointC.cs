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
    public partial class frmCheckPointC : Form
    {
        public frmCheckPointC()
        {
            InitializeComponent();
            gridChangeDoc.AutoGenerateColumns = false;
            GridPatientQueue.AutoGenerateColumns = false;
            //setGridChangeDoc();
        }

        #region morn coding set all grid
        private void refrechForm(string condition)
        {
            uiUserprofile1.ClearForm();
            uiMapping1.GetMapping();
            loadGridPatientQueue(condition);
            loadGridChangeDoc(condition);
            loadGVCompleted(condition);
        }

        private int? current_tpr_id
        {
            get;
            set;
        }

        private Image GetImage(string strstatus)
        {
            Image imgicon = null;
            //return (strstatus == "RB") ? imageList1.Images[0] : imgicon;

            if (strstatus == "Y")
                imgicon = imageList1.Images[1];
            else
                imgicon = imageList1.Images[2];
            return imgicon;
        }

        private void loadGridPatientQueue(string condition)
        {
            GridPatientQueue.AutoGenerateColumns = false;
            int mhs_id = Program.CurrentSite.mhs_id;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                DateTime dtnow = Program.GetServerDateTime();
                //int mrm_idCheckC = cdc.mst_room_hdrs
                //                      .Where(x => x.mhs_id == mhs_id &&
                //                                  x.mrm_code == "CC" &&
                //                                  x.mrm_status == 'A' &&
                //                                  x.mrm_effective_date.Value.Date <= dtnow.Date &&
                //                                  x.mrm_expire_date == null ? true : x.mrm_expire_date.Value.Date >= dtnow.Date)
                //                      .Select(x => x.mrm_id)
                //                      .FirstOrDefault();

                //int mvt_idCheckC = mst.getMstEvent("CC").mvt_id;

                List<WaitingPatientQueue> result = cdc.vw_pat_checkpointCs
                                                      .Where(x => x.mhs_id == mhs_id &&
                                                                  x.flag == "Y" &&
                                                                  (x.tpr_queue_no.Contains(condition) ||
                                                                   x.tpt_hn_no.Contains(condition) ||
                                                                   x.fullname.Contains(condition) ||
                                                                   x.tpt_hn_no.Replace("-", "").Contains(condition)))
                                                      .Select(x => new WaitingPatientQueue
                                                      {
                                                          tpr_id = x.tpr_id,
                                                          mhs_ename = x.mhs_ename,
                                                          QueueNo = x.tpr_queue_no,
                                                          HN = x.tpt_hn_no,
                                                          EN = x.tpr_en_no,
                                                          FullName = x.fullname,
                                                          package = (x.package == true) ? "PRM Package" : "",
                                                          flag = x.flag,
                                                          out_site = GetImage(x.out_site),
                                                          type_event = TypeEvent.RE
                                                      }).ToList();

                int mvtPE = cdc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                List<int?> list_mrm_id = cdc.mst_room_hdrs
                                            .Where(x => x.mst_hpc_site.mhs_other_clinic == true &&
                                                        x.mrm_code == "CC")
                                            .Select(x => (int?)x.mrm_id).ToList();
                DateTime dateNow = Program.GetServerDateTime();
                List<WaitingPatientQueue> resultPE = cdc.trn_patient_queues
                                                        .Where(x => x.trn_patient_regi.tpr_arrive_date.Value.Date == dateNow.Date &&
                                                                    x.tps_status == "NS" && x.tps_ns_status == "WP" &&
                                                                    x.mvt_id == mvtPE &&
                                                                    x.mst_room_hdr.mst_hpc_site.mhs_use_before_pe == true &&
                                                                    x.mst_room_hdr.mhs_id == mhs_id &&
                                                                    x.mst_room_hdr.mrm_code == "DC" &&
                                                                    (x.trn_patient_regi.tpr_queue_no.Contains(condition) ||
                                                                    x.trn_patient_regi.trn_patient.tpt_hn_no.Contains(condition) ||
                                                                    x.trn_patient_regi.trn_patient.tpt_othername.Contains(condition) ||
                                                                    x.trn_patient_regi.trn_patient.tpt_hn_no.Replace("-", "").Contains(condition)))
                                                        .Select(x => new WaitingPatientQueue
                                                        {
                                                            tpr_id = x.tpr_id,
                                                            mhs_ename = x.trn_patient_regi.mst_hpc_site.mhs_ename,
                                                            QueueNo = x.trn_patient_regi.tpr_queue_no,
                                                            HN = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                            EN = x.trn_patient_regi.tpr_en_no,
                                                            FullName = x.trn_patient_regi.trn_patient.tpt_othername,
                                                            package = x.trn_patient_regi.tpr_PRM == true ? "PRM Package" : "",
                                                            flag = "Y",
                                                            out_site = GetImage(x.trn_patient_regi.tpr_req_inorout_doctor == "UT" ? "Y" : "N"),
                                                            type_event = TypeEvent.PE,
                                                            tps_id = x.tps_id
                                                        }).ToList();

                if (resultPE.Count() > 0) result.AddRange(resultPE);

                int no = 1;
                result.ForEach(x => x.no = no++);
                GridPatientQueue.DataSource = result;

                foreach (DataGridViewRow rw in GridPatientQueue.Rows)
                {
                    WaitingPatientQueue Data = (WaitingPatientQueue)rw.DataBoundItem;
                    if (Data.flag == "N")
                    {
                        rw.Cells["Colbtn"] = new DataGridViewTextBoxCell();
                        rw.Cells["ColSendBook"] = new DataGridViewTextBoxCell();
                        rw.Cells["ColsendQueue"] = new DataGridViewTextBoxCell();
                    }
                    if (Data.type_event == TypeEvent.PE)
                    {
                        rw.Cells["Colbtn"] = new DataGridViewTextBoxCell();
                        rw.Cells["ColSendBook"] = new DataGridViewTextBoxCell();
                    }
                }

                GridPatientQueue.Columns["Coltprid"].Visible = false;
                GridPatientQueue.Columns["EN"].Visible = false;

                lbListcheckbody.Text = "3.1 รายชื่อผู้ป่วยที่มีผลการตรวจร่างกายครบทุกรายการ (Total " + result.Count().ToString() + " คน)";

                if (GridPatientQueue.Rows.Count != 0)
                {
                    btnprintslip.Enabled = true;
                    btnSendToCheckB.Enabled = true;
                    btnRetriveLab.Enabled = true;
                    btnrefresh.Enabled = true;
                    btnSendQueueAll.Enabled = true;
                    GridPatientQueue_CellClick(null, null);
                }
                else
                {
                    btnprintslip.Enabled = false;
                    btnSendToCheckB.Enabled = false;
                    btnRetriveLab.Enabled = false;
                    //btnrefresh.Enabled = false;
                    btnSendQueueAll.Enabled = false;
                }
                //GridPatientQueue_SelectionChanged(null, null);
                //GridPatientQueue_CellClick(null, null);
            }
        }
        private void loadGridLab(int? tpr_id, string en)
        {
            GridLab.AutoGenerateColumns = false;
            if (tpr_id != null)
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {

                    //var objlabdata = (from tpl in tpr.trn_patient_labs.Where(x => x.tpl_en_no == tpr.tpr_en_no).ToList()
                    //                  join mlb in mstLab on tpl.tpl_lab_no equals mlb.mlb_code into g
                    //                  from grp in g.DefaultIfEmpty()
                    //                  select new
                    //                  {
                    //                      LabName = grp == null ? "N/A" : grp.mst_lab_group.mlg_code + " - " + grp.mst_lab_group.mlg_ename,
                    //                      status = tpl.tpl_status
                    //                  }).ToList().GroupBy(x => x.LabName).OrderBy(x => x.Key)
                    //                  .Select((x, index) => new
                    //                  {
                    //                      no = index + 1,
                    //                      LabName = x.Key,
                    //                      LabStatus = (x.All(y => y.status == 'E')) ? "Executed" : ""
                    //                  })
                    //                  .ToList(); ;
                    //GridLab.DataSource = objlabdata;
                    //var objlabdata = tpr.trn_patient_labs
                    //                    .Where(x => x.tpl_en_no == tpr.tpr_en_no)
                    //                    .Select(x => new
                    //                    {
                    //                        LabName = mstLab.Where(y => y.mlb_code == x.tpl_lab_no)
                    //                                        .Select(y => y.mst_lab_group.mlg_code + " - " + y.mst_lab_group.mlg_ename)
                    //                                        .FirstOrDefault(),
                    //                        status = x.tpl_status
                    //                    }).ToList().GroupBy(x => x.LabName).OrderBy(x => x.Key)
                    //                    .Select((x, index) => new
                    //                    {
                    //                        no = index + 1,
                    //                        LabName = (string.IsNullOrEmpty(x.Key) ? "N/A" : x.Key),
                    //                        LabStatus = (x.All(y => y.status == 'E')) ? "Executed" : ""
                    //                    })
                    //                    .ToList();
                    //GridLab.DataSource = objlabdata;
                    //var objlabdata = (from tpl in cdc.trn_patient_labs
                    //                  join mlb in cdc.mst_labs
                    //                  on tpl.tpl_lab_no equals mlb.mlb_code
                    //                  join mlg in cdc.mst_lab_groups
                    //                  on mlb.mlg_id equals mlg.mlg_id
                    //                  where tpl.tpr_id == tpr_id &&
                    //                        tpl.tpl_en_no == en
                    //                  select new
                    //                  {
                    //                      groupCode = mlg.mlg_code,
                    //                      groupName = mlg.mlg_ename,
                    //                      status = tpl.tpl_status
                    //                  }).ToList();
                    //var result = objlabdata.GroupBy(x => new { x.groupCode, x.groupName })
                    //                       .Select((x, index) => new
                    //                       {
                    //                           no = index + 1,
                    //                           LabName = x.Key.groupCode + " - " + x.Key.groupName,
                    //                           LabStatus = (x.All(y => y.status == 'E')) ? "Executed" : ""
                    //                       }).ToList();
                    //GridLab.DataSource = result;

                    //trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    //List<trn_patient_lab> list_patient_lab = cdc.trn_patient_labs.Where(x => x.tpr_id == tpr_id && x.tpl_en_no == en).ToList();
                    ////List<mst_lab> mstLab = cdc.mst_labs.Where(x => list_patient_lab.Select(y => y.tpl_lab_no).ToList().Contains(x.mlb_code)).ToList();
                    var objlabdata = (from tpl in cdc.trn_patient_labs
                                      join mlb in cdc.mst_labs
                                      on tpl.tpl_lab_no equals mlb.mlb_code
                                      where tpl.tpr_id == tpr_id &&
                                            tpl.tpl_en_no == en
                                      orderby mlb.mst_lab_group.mlg_code
                                      select new
                                      {
                                          groupCode = mlb.mst_lab_group.mlg_code,
                                          groupName = mlb.mst_lab_group.mlg_ename,
                                          labCode = mlb.mlb_code,
                                          status = tpl.tpl_status
                                      }).ToList();
                    var result = objlabdata.GroupBy(x => new { x.groupCode, x.groupName })
                                           .Select((x, index) => new
                                           {
                                               no = index + 1,
                                               LabName = x.Key.groupCode + " - " + x.Key.groupName,
                                               LabStatus = (x.All(y => y.status == 'E')) ? "Executed" : ""
                                           }).ToList();
                    var otherLab = (from tpl in cdc.trn_patient_labs
                                    where tpl.tpr_id == tpr_id &&
                                          tpl.tpl_en_no == en &&
                                          !objlabdata.Select(y => y.labCode).ToList().Contains(tpl.tpl_lab_no)
                                    group tpl by tpl.tpl_status into grp
                                    select grp.Key).ToList();
                    if (otherLab != null && otherLab.Count() > 0)
                    {
                        //bool otherLab = cdc.trn_patient_labs.Where(x => !objlabdata.Select(y => y.labCode).ToList().Contains(x.tpl_lab_no)).Select(x => x.tpl_status).Distinct().All(x => x == 'E');
                        if (otherLab.All(x => x == 'E'))
                        {
                            result.Add(new { no = result.Count() + 1, LabName = "N/A", LabStatus = "Executed" });
                        }
                        else
                        {
                            result.Add(new { no = result.Count() + 1, LabName = "N/A", LabStatus = "" });
                        }
                    }
                    GridLab.DataSource = result;
                }
            }
            else
            {
                GridLab.DataSource = null;
            }
        }
        private void loadGridChangeDoc(string condition)
        {
            gridChangeDoc.AutoGenerateColumns = false;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dtnow = Program.GetServerDateTime();

                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                var result = cdc.trn_patient_queues.Where(x => x.mst_room_hdr.mrm_code == "DC" &&
                                                               x.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id &&
                                                               (x.trn_patient_regi.tpr_queue_no.Contains(condition) || 
                                                                x.trn_patient_regi.trn_patient.tpt_hn_no.Contains(condition) ||
                                                                x.trn_patient_regi.trn_patient.tpt_hn_no.Replace("-", "").Contains(condition) ||
                                                                (x.trn_patient_regi.trn_patient.tpt_othername.ToUpper()).Contains(condition.ToUpper())) &&
                                                               x.tps_status == "NS" &&
                                                               x.tps_ns_status == "QL" &&
                                                               x.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date)
                                .Select(x => new gridChangeDocProp()
                                {
                                    queue_no = x.trn_patient_regi.tpr_queue_no,
                                    hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                    en_no = x.trn_patient_regi.tpr_en_no,
                                    patient_name = x.trn_patient_regi.trn_patient.tpt_othername,
                                    type_desc = cdc.mst_events.Where(y => y.mvt_id == x.mvt_id).Select(y => y.mvt_code).FirstOrDefault() == "PE" ? "PE" : "Result",
                                    doctor_name = x.trn_patient_regi.tpr_pe_doc_name != null && x.trn_patient_regi.tpr_pe_doc_name != string.Empty
                                                  ? x.trn_patient_regi.tpr_pe_doc_name
                                                  : x.trn_patient_regi.tpr_req_doc_name,
                                    doctor_username = x.trn_patient_regi.tpr_pe_doc_name != null && x.trn_patient_regi.tpr_pe_doc_name != string.Empty
                                                    ? x.trn_patient_regi.tpr_pe_doc_code
                                                    : x.trn_patient_regi.tpr_req_doc_code,
                                    flag_update_field = x.trn_patient_regi.tpr_pe_doc_name != null && x.trn_patient_regi.tpr_pe_doc_name != string.Empty
                                                        ? update_field.update_pe
                                                        : update_field.update_req,
                                    tpr_id = x.tpr_id
                                }).ToList();
                int index = 1;
                result.ForEach(x => x.no = index++);
                gridChangeDocBS.DataSource = result;
                gridChangeDoc.DataSource = gridChangeDocBS;
            }
        }
        private void loadGVCompleted(string condition)
        {
            GVCompleted.AutoGenerateColumns = false;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                List<pw_Get_PatientCompletedResult> result = cdc.pw_Get_PatientCompleted(Program.CurrentSite.mhs_id).ToList();
                List<pw_Get_PatientCompletedResult> res = result.Where(x => x.QueueNo.Contains(condition) ||
                                                                              x.Hn.Contains(condition) ||
                                                                              x.Hn.Replace("-", "").Contains(condition) ||
                                                                              x.Name.Contains(condition)).ToList();
                GVCompleted.DataSource = res;
                lblPatientCompleted.Text = "3.3 รายชื่อผู้รับบริการที่พบหมอเรียบร้อยแล้ว (Total " + res.Count().ToString() + " คน)";
            }
        }
        #endregion

        public enum update_field
        {
            update_req,
            update_pe
        }
        private class gridChangeDocProp
        {
            public int no { get; set; }
            public string queue_no { get; set; }
            public string hn_no { get; set; }
            public string en_no { get; set; }
            public string patient_name { get; set; }
            public string type_desc { get; set; }
            public string doctor_name { get; set; }
            public string doctor_username { get; set; }
            public update_field flag_update_field { get; set; }
            public int tpr_id { get; set; }
        }
        private BindingSource gridChangeDocBS = new BindingSource();

        private void frmCheckPointC_Load(object sender, EventArgs e)
        {
            uiMenuBar1.enableDashBoard();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            this.Text = Program.GetRoomName();
            uiFooter1.RoomCode = "CC";
            //uiFooter1.LoadData();
            timer1.Start();
            refrechForm(txtSearch.Text);

            frmbg.Close();
        }

        private enum typeSendDoctorResult
        {
            noLabResult,
            inProgressType_W,
            inProgressType_N,
            excuted
        }
        private typeSendDoctorResult checkTypeSendDoctorResult(trn_patient_regi tpr)
        {
            string en = tpr.tpr_en_no;
            List<trn_patient_lab> list_patient_lab = tpr.trn_patient_labs
                                                        .Where(x => x.tpl_en_no == en)
                                                        .ToList();
            if (list_patient_lab == null || list_patient_lab.Count == 0)
            {
                return typeSendDoctorResult.noLabResult;
            }
            else if (tpr.tpr_pe_type == 'W' && list_patient_lab.Any(x => x.tpl_status == 'I'))
            {
                return typeSendDoctorResult.inProgressType_W;
            }
            else if (tpr.tpr_pe_type == 'N' && list_patient_lab.Any(x => x.tpl_status == 'I'))
            {
                DialogResult result =
                MessageBox.Show("คุณไม่สนใจ Lab result บางรายการที่ยังไม่เรียบร้อย คุณต้องการดำเนินการต่อหรือไม่", "Send Doctor Result Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.No)
                {
                    return typeSendDoctorResult.inProgressType_N;
                }
            }
            return typeSendDoctorResult.excuted;
        }
        private void sendQueueToDoctor(ref trn_patient_regi tpr)
        {
            if (tpr != null)
            {
                DateTime dtnow = Program.GetServerDateTime();
                int mrm_id = Program.CurrentRoom.mrm_id;
                int mrd_id = Program.CurrentRoom.mrd_id;

                trn_patient_queue queueCheckC = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_id).FirstOrDefault();

                queueCheckC.tps_update_by = Program.CurrentUser.mut_username;
                queueCheckC.tps_update_date = dtnow;
                queueCheckC.tps_status = "ED";

                EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                string mhs_code = mst.GetMstHpcSite(tpr.mhs_id).mhs_code;
                int doctor_mrm_id = mst.GetMstRoomHdr("DC", mhs_code).mrm_id;
                int doctor_mvt_id = mst.GetMstEvent("DC").mvt_id;

                trn_patient_queue doctor_queue = tpr.trn_patient_queues
                                                    .Where(x => x.mrm_id == doctor_mrm_id &&
                                                                x.mvt_id == doctor_mvt_id)
                                                    .FirstOrDefault();

                if (doctor_queue == null)
                {
                    doctor_queue = new trn_patient_queue();
                    doctor_queue.mrm_id = doctor_mrm_id;
                    doctor_queue.mvt_id = doctor_mvt_id;
                    doctor_queue.mrd_id = null;
                    tpr.trn_patient_queues.Add(doctor_queue);
                }
                doctor_queue.tps_status = "NS";
                doctor_queue.tps_ns_status = "QL";
                doctor_queue.mrd_id = null;
                doctor_queue.tps_start_date = dtnow;
                doctor_queue.tps_end_date = null;
                doctor_queue.tps_create_date = dtnow;
                doctor_queue.tps_create_by = Program.CurrentUser.mut_username;
                doctor_queue.tps_update_date = dtnow;
                doctor_queue.tps_update_by = Program.CurrentUser.mut_username;
            }
        }
        private void sendQueue(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    //new Class.FunctionDataCls().stampPEDoctor(tpr_id);
                    if (tpr != null)
                    {
                        typeSendDoctorResult result = checkTypeSendDoctorResult(tpr);
                        if (result == typeSendDoctorResult.noLabResult)
                        {
                            DialogResult dialog =
                            MessageBox.Show("Lab Result ยังไม่ออก คุณต้องการดำเนินการต่อหรือไม่", "Send Doctor Result Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dialog == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else if (result == typeSendDoctorResult.inProgressType_W)
                        {
                            DialogResult diglog =
                            MessageBox.Show("Lab Result บางรายการยังไม่ Execute คุณต้องการดำเนินการต่อหรือไม่", "Send Doctor Result Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (diglog == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else if (result == typeSendDoctorResult.inProgressType_N)
                        {
                            DialogResult diglog =
                            MessageBox.Show("คุณไม่สนใจ Lab result บางรายการที่ยังไม่เรียบร้อย คุณต้องการดำเนินการต่อหรือไม่", "Send Doctor Result Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (diglog == DialogResult.No)
                            {
                                return;
                            }
                        }
                    }
                }
               
                //sendQueueToDoctor(ref tpr);
                //CheckUpLabClass.InsertInto_trn_patient(tpr.tpr_id);
                //cdc.SubmitChanges();
                ////refrechForm(txtSearch.Text);
                //lbAlertMsg.Text = "Send Queue No. " + tpr.tpr_queue_no + " To Doctor(Result) Completed.";
            }
            catch
            {
                lbAlertMsg.Text = "เกิดความผิดพลาดของโปรแกรม โปรดติดต่อผู้ดูแลระบบ";
            }
        }
        private void sendQueueAll(List<int> list_tpr_id)
        {
            Program.RefreshWaiting = false;
            timer1.Stop();
            lbAlertMsg.Text = String.Empty;
            try
            {
                List<string> list_SendQueue = new List<string>();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    foreach (int tpr_id in list_tpr_id)
                    {
                        trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                        typeSendDoctorResult result = checkTypeSendDoctorResult(tpr);
                        if (result != typeSendDoctorResult.inProgressType_W)
                        {
                            list_SendQueue.Add(tpr.tpr_queue_no);
                            sendQueueToDoctor(ref tpr);
                        }
                    }
                    cdc.SubmitChanges();
                    refrechForm(txtSearch.Text);
                    lbAlertMsg.Text = "Send All Queue To Doctor(Result) Completed.";
                }
            }
            catch
            {
                lbAlertMsg.Text = "เกิดความผิดพลาดของโปรแกรม โปรดติดต่อผู้ดูแลระบบ";
            }
            Program.RefreshWaiting = true;
            timer1.Start();
        }
        private void pendingDoctor(int tpr_id)
        {
            Program.RefreshWaiting = false;
            timer1.Stop();
            lbAlertMsg.Text = String.Empty;
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (tpr != null)
                    {
                        DateTime dtnow = Program.GetServerDateTime();
                        int mrm_id = Program.CurrentRoom.mrm_id;
                        int mrd_id = Program.CurrentRoom.mrd_id;
                        trn_patient_queue queueCheckC = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_id).FirstOrDefault();

                        queueCheckC.tps_update_by = Program.CurrentUser.mut_username;
                        queueCheckC.tps_update_date = dtnow;
                        queueCheckC.tps_status = "ED";

                        tpr.tpr_pending_no_station = true;
                        cdc.SubmitChanges();
                        //refrechForm(txtSearch.Text);
                    }
                }
            }
            catch
            {
                lbAlertMsg.Text = "เกิดความผิดพลาดของโปรแกรม โปรดติดต่อผู้ดูแลระบบ";
            }
            Program.RefreshWaiting = true;
            timer1.Start();
        }

        private void setCurrentPatientQueue(trn_patient_regi tpr)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            int CheckC_mrm_id = mst.GetMstRoomHdr("CC", Program.CurrentSite.mhs_code).mrm_id;
            Program.CurrentPatient_queue = tpr.trn_patient_queues.Where(x => x.mrm_id == CheckC_mrm_id).FirstOrDefault();
        }
        private void GridPatientQueue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    WaitingPatientQueue data = (WaitingPatientQueue)GridPatientQueue.CurrentRow.DataBoundItem;
                    
                    int? tpr_id = null;
                    if (GridPatientQueue.CurrentRow != null)
                    {
                        tpr_id = Convert.ToInt32(GridPatientQueue.CurrentRow.Cells["Coltprid"].Value);
                    }
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    setCurrentPatientQueue(tpr);
                    uiUserprofile1.LoadData((int)tpr.tpr_id);
                    current_tpr_id = tpr.tpr_id;
                    if (data.type_event == TypeEvent.RE)
                    {
                        btnSendToCheckB.Enabled = true;
                    }
                    else
                    {
                        btnSendToCheckB.Enabled = false;
                    }
                }
            }
            catch
            {

            }
        }
        private void GridPatientQueue_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.RefreshWaiting = false;
            timer1.Stop();
            lbAlertMsg.Text = String.Empty;
            try
            {
                int tpr_id = Convert.ToInt32(GridPatientQueue.CurrentRow.Cells["Coltprid"].Value);
                int mrm_id = Program.CurrentRoom.mrm_id;
                string messege = "";
                switch (GridPatientQueue.Columns[e.ColumnIndex].Name)
                {
                    case "ColsendQueue": // send queue
                        //sendQueue(tpr_id);
                        WaitingPatientQueue Data = (WaitingPatientQueue)GridPatientQueue.Rows[e.RowIndex].DataBoundItem;
                        if (Data.type_event == TypeEvent.RE)
                        {
                            StatusTransaction sendResult = new Class.SendQueue().SendToResult(tpr_id, mrm_id, ref messege);
                            if (sendResult == StatusTransaction.True)
                            {
                                //new Class.FunctionDataCls().stampPEDoctor(tpr_id);
                                lbAlertMsg.Text = messege;
                            }
                            else if (sendResult == StatusTransaction.Error)
                            {
                                lbAlertMsg.Text = messege;
                            }
                        }
                        else
                        {
                            try
                            {
                                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                                {
                                    trn_patient_queue PatientQueue = cdc.trn_patient_queues
                                                                        .Where(x => x.tps_id == Data.tps_id).FirstOrDefault();
                                    PatientQueue.tps_ns_status = "QL";
                                    PatientQueue.tps_update_by = Program.CurrentUser.mut_username;
                                    PatientQueue.tps_update_date = Program.GetServerDateTime();
                                    cdc.SubmitChanges();
                                    lbAlertMsg.Text = string.Format(Program.MsgSend, 
                                                                    PatientQueue.mst_room_hdr.mrm_ename,
                                                                    PatientQueue.mst_room_hdr.mst_zone.mze_ename,
                                                                    PatientQueue.mst_room_hdr.mst_hpc_site.mhs_ename,
                                                                    PatientQueue.trn_patient_regi.tpr_queue_no);
                                }
                            }
                            catch (Exception ex)
                            {
                                Program.MessageError(this.Name, "GridPatientQueue_CellContentClick", ex, false);
                                lbAlertMsg.Text = "กรุณากด Send Queue อีกครั้ง";
                            }
                        }
                        refrechForm(txtSearch.Text);
                        break;
                    case "Colbtn": // ค้างตรวจแพทย์
                        //pendingDoctor(tpr_id);
                        StatusTransaction pendingDoc = new Class.ClsPendingOnStation().pendingDoctor(tpr_id, ref messege);
                        if (pendingDoc == StatusTransaction.True)
                        {
                            lbAlertMsg.Text = messege;
                        }
                        else if (pendingDoc == StatusTransaction.Error)
                        {
                            lbAlertMsg.Text = messege;
                        }
                        refrechForm(txtSearch.Text);
                        break;
                    case "ColSendBook": // send book
                        //Class.SendToBook.SendBook(tpr_id);
                        StatusTransaction sendBook = new Class.SendQueue().SendToBook(tpr_id, mrm_id, ref messege);
                        if (sendBook == StatusTransaction.True)
                        {
                            lbAlertMsg.Text = messege;
                        }
                        else if (sendBook == StatusTransaction.Error)
                        {
                            lbAlertMsg.Text = messege;
                        }
                        refrechForm(txtSearch.Text);
                        break;
                }
            }
            catch
            {

            }
            Program.RefreshWaiting = true;
            timer1.Start();
        }
        private void GridPatientQueue_SelectionChanged(object sender, EventArgs e)
        {
            int? tpr_id = null;
            string en = string.Empty;
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    if (GridPatientQueue.CurrentRow != null)
                    {
                        tpr_id = Convert.ToInt32(GridPatientQueue.CurrentRow.Cells["Coltprid"].Value);
                    }
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    en = tpr.tpr_en_no;
                }
            }
            catch
            {

            }
            loadGridLab(tpr_id, en);
        }

        private void gridChangeDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int? tpr_id = null;
                    gridChangeDocProp current = (gridChangeDocProp)gridChangeDocBS.Current;
                    if (GridPatientQueue.CurrentRow != null)
                    {
                        tpr_id = current.tpr_id;
                    }
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    uiUserprofile1.LoadData((int)tpr.tpr_id);
                    btnSendToCheckB.Enabled = false;
                    current_tpr_id = tpr.tpr_id;
                }
            }
            catch
            {

            }
        }
        private void gridChangeDoc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Stop();
            if (((DataGridView)sender).Columns[e.ColumnIndex].Name == "colBtnChange")
            {
                gridChangeDocProp result = (gridChangeDocProp)gridChangeDocBS.Current;

                frmSelectDoctor frm = new frmSelectDoctor(result.doctor_name);
                // add by pang 09/06/2015
                try
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        var chkDoc = (from patient in cdc.trn_patient_regis where patient.tpr_id == result.tpr_id select new { patient.mhs_id, patient.tpr_req_doctor, patient.tpr_req_doc_name }).FirstOrDefault();

                        if (chkDoc.tpr_req_doctor == 'Y')
                        {
                            frm.LabelWarningText = "*คนไข้ request คุณหมอ :\r\n" + chkDoc.tpr_req_doc_name;
                            frm.LabelWarningVisible = true;
                        }
                        else
                        {
                            frm.LabelWarningText = "";
                            frm.LabelWarningVisible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError(this.Name, "frmSelectDoctor", ex, false);
                    frm.LabelWarningText = "";
                    frm.LabelWarningVisible = false;
                }
                frm.ShowDialog();
                // end add 09/06/2015
                frmSelectDoctor.selectDocType type = frm.getSelectType;

                if (type == frmSelectDoctor.selectDocType.SelectedDoctor)
                {
                    bool success = new Class.FunctionDataCls().changeDoctor(result.tpr_id, frm.get_mut_username, "PE");
                    if (success)
                    {
                        string mut_fullname = new EmrClass.GetDataMasterCls().GetUser(frm.get_mut_username).mut_fullname;
                        result.doctor_name = mut_fullname;
                        gridChangeDoc.Refresh();
                    }
                }
                else if (type == frmSelectDoctor.selectDocType.NoRequestDoctor)
                {
                    bool success = new Class.FunctionDataCls().cancelPEDoctor(result.tpr_id);
                    if (success)
                    {
                        result.doctor_name = null;
                        gridChangeDoc.Refresh();
                    }
                }
            }
            timer1.Start();
        }
        // add by pang 09/06/2015
        private void checkDocRequest()
        {
            
        }
        // end 09/06/2015

        private void GVCompleted_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int? tpr_id = null;
                    if (GridPatientQueue.CurrentRow != null)
                    {
                        tpr_id = Convert.ToInt32(GVCompleted.CurrentRow.Cells["colComp_tpr_id"].Value);
                    }
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    setCurrentPatientQueue(tpr);
                    uiUserprofile1.LoadData((int)tpr.tpr_id);
                    current_tpr_id = tpr.tpr_id;
                    btnSendToCheckB.Enabled = false;
                }
            }
            catch
            {

            }
        }
        private void GVCompleted_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            #region SendRoom
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    int tpr_id = Convert.ToInt32(GVCompleted.CurrentRow.Cells["colComp_tpr_id"].Value);
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    mst_hpc_site mhs = mst.GetMstHpcSite(tpr.mhs_id);
                    mst_room_hdr mrmCheckC = mst.GetMstRoomHdr("CC", mhs.mhs_code);
                    int mrm_idCheckC = mrmCheckC.mrm_id;
                    mst_event mvtCheckC = mst.GetMstEvent("CC");
                    int mvt_idCheckC = mvtCheckC.mvt_id;
                    DateTime dateNow = Program.GetServerDateTime();

                    int tps_id = 0;
                    StatusTransaction get_tps_id = new Class.FunctionDataCls().getCurrent_tps_id(tpr_id, mrm_idCheckC, ref tps_id);
                    string messege = "";
                    switch (GVCompleted.Columns[e.ColumnIndex].Name)
                    {
                        case "phm":
                            if (get_tps_id == StatusTransaction.True)
                            {
                                StatusTransaction sendPHM = new Class.SendQueue().SendToPHM(tpr_id, mrm_idCheckC, tps_id, ref messege);
                                if (sendPHM == StatusTransaction.True)
                                {
                                    lbAlertMsg.Text = messege;
                                }
                                else if (sendPHM == StatusTransaction.Error)
                                {
                                    lbAlertMsg.Text = messege;
                                }
                            }
                            else
                            {
                                lbAlertMsg.Text = "โปรดกด send book อีกครั้ง";
                            }
                            //mst_room_hdr mrmPHM = mst.getMstRoomHdr("PH", mhs.mhs_code);
                            //int mrm_idPHM = mrmPHM.mrm_id;
                            //mst_event mvtPHM = mst.getMstEvent("PH");
                            //int mvt_idPHM = mvtPHM.mvt_id;

                            //trn_patient_queue tpsPHM = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_idPHM && x.mvt_id == mvt_idPHM).FirstOrDefault();
                            //if (tpsPHM == null)
                            //{
                            //    tpsPHM = new trn_patient_queue();
                            //    tpsPHM.mrm_id = mrm_idPHM;
                            //    tpsPHM.mvt_id = mvt_idPHM;
                            //    tpsPHM.tps_create_by = Program.CurrentUser.mut_username;
                            //    tpsPHM.tps_create_date = dateNow;
                            //    tpsPHM.tps_update_by = Program.CurrentUser.mut_username;
                            //    tpsPHM.tps_update_date = dateNow;
                            //    tpr.trn_patient_queues.Add(tpsPHM);
                            //}
                            //tpsPHM.mrd_id = null;
                            //tpsPHM.tps_end_date = null;
                            //tpsPHM.tps_start_date = null;
                            //tpsPHM.tps_status = "NS";
                            //tpsPHM.tps_ns_status = "QL";
                            //cdc.SubmitChanges();
                            //lbAlertMsg.Text = String.Format("Send Queue No. " + tpr.tpr_queue_no + " To PHM Completed.");
                            refrechForm(txtSearch.Text);
                            break;
                        case "bk":
                            if (get_tps_id == StatusTransaction.True)
                            {
                                StatusTransaction sendBook = new Class.SendQueue().SendToBook(tpr_id, mrm_idCheckC, tps_id, ref messege);
                                if (sendBook == StatusTransaction.True)
                                {
                                    lbAlertMsg.Text = messege;
                                }
                                else if (sendBook == StatusTransaction.Error)
                                {
                                    lbAlertMsg.Text = messege;
                                }
                            }
                            else
                            {
                                lbAlertMsg.Text = "โปรดกด send book อีกครั้ง";
                            }
                            //mst_room_hdr mrmBK = mst.getMstRoomHdr("BK", mhs.mhs_code);
                            //int mrm_idBK = mrmBK.mrm_id;
                            //mst_event mvtBK = mst.getMstEvent("BK");
                            //int mvt_idBK = mvtBK.mvt_id;

                            //tpr.tpr_status = "WB";
                            //tpr.tpr_pe_status = "RS";

                            //trn_patient_queue tpsBK = tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_idBK && x.mvt_id == mvt_idBK).FirstOrDefault();
                            //if (tpsBK == null)
                            //{
                            //    tpsBK = new trn_patient_queue();
                            //    tpsBK.mrm_id = mrm_idBK;
                            //    tpsBK.mvt_id = mvt_idBK;
                            //    tpsBK.tps_create_by = Program.CurrentUser.mut_username;
                            //    tpsBK.tps_create_date = dateNow;
                            //    tpsBK.tps_update_by = Program.CurrentUser.mut_username;
                            //    tpsBK.tps_update_date = dateNow;
                            //    tpr.trn_patient_queues.Add(tpsBK);
                            //}
                            //tpsBK.mrd_id = null;
                            //tpsBK.tps_end_date = null;
                            //tpsBK.tps_start_date = dateNow;
                            //tpsBK.tps_status = "NS";
                            //tpsBK.tps_ns_status = "QL";
                            //cdc.SubmitChanges();
                            //lbAlertMsg.Text = String.Format("Send Queue No. " + tpr.tpr_queue_no + " To Book Completed.");
                            refrechForm(txtSearch.Text);
                            break;
                    }
                }
            }
            catch
            {
                return;
            }
            #endregion
        }

        private void btnSendQueueAll_Click(object sender, EventArgs e)
        {
            Program.RefreshWaiting = false;
            timer1.Stop();
            lbAlertMsg.Text = String.Empty;
            try
            {
                List<int> list_tpr_id = new List<int>();
                foreach (DataGridViewRow row in GridPatientQueue.Rows)
                {
                    list_tpr_id.Add(Convert.ToInt32(row.Cells["Coltprid"].Value));
                }
                sendQueueAll(list_tpr_id);
            }
            catch
            {
                lbAlertMsg.Text = "เกิดความผิดพลาดของโปรแกรม โปรดติดต่อผู้ดูแลระบบ";
            }
            Program.RefreshWaiting = true;
            timer1.Start();
        }
        private void btnSendToCheckB_Click(object sender, EventArgs e)
        {
            lbAlertMsg.Text = "";

            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            int? tpr_id = uiUserprofile1.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;

            if (tpr_id != null)
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    mst_hpc_site mhsCheckC = mst.GetMstHpcSite(tpr.mhs_id);
                    mst_room_hdr mrmCheckC = mst.GetMstRoomHdr("CC", mhsCheckC.mhs_code);

                    string messege = "";
                    StatusTransaction sendToB = new Class.SendQueue().SendToCheckB((int)tpr_id, mrmCheckC.mrm_id, ref messege);
                    if (sendToB == StatusTransaction.True)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendCheckB,
                                                    (int)tpr_id,
                                                    tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);

                        refrechForm(txtSearch.Text);
                        lbAlertMsg.Text = messege;
                    }
                    else if (sendToB == StatusTransaction.Error)
                    {
                        refrechForm(txtSearch.Text);
                        lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถส่งไปยัง Checkpoint B ได้ กรุณา กดปุ่ม go to Checkpoint B อีกครั้ง";
                    }

                    //Class.FunctionDataCls func = new Class.FunctionDataCls();
                    //CallQueue.SendCheckPointBOnStation((int)tpr_id, mrmCheckC.mrm_id);
                    //refrechForm(txtSearch.Text);
                    //lbAlertMsg.Text = func.getStringGotoNextRoom((int)tpr_id);
                }
            }

            frmbg.Close();
        }
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            refrechForm(txtSearch.Text);
            GridPatientQueue_CellClick(null, null);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            btnSearch.Enabled = false;
            refrechForm(txtSearch.Text);
            btnSearch.Enabled = true;

            frmbg.Close();
        }
        private void btnprintslip_Click(object sender, EventArgs e)
        {
            try
            {
                if (current_tpr_id != null)
                {
                    List<string> rptCode = new List<string>() { "RG120" };
                    Report.frmPreviewReport frm = new Report.frmPreviewReport((int)current_tpr_id, rptCode, true, 1, true);
                    frm.previewReport();
                }
            }
            catch
            {
            }
        }
        private void btnRetriveLab_Click(object sender, EventArgs e)
        {
            Program.RefreshWaiting = false;
            timer1.Stop();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int tpr_id = Convert.ToInt32(GridPatientQueue.CurrentRow.Cells["Coltprid"].Value);
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    string hn = tpr.trn_patient.tpt_hn_no;
                    using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                    {
                        ws.getCheckUpLabResult(hn, tpr_id);
                        GridPatientQueue_SelectionChanged(null, null);
                    }
                    //CheckUpLabClass.ws_Getcheckuplab_Async(hn, tpr.tpr_id);
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnRetriveLab_Click", ex, false);
            }
            timer1.Start();
            Program.RefreshWaiting = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Program.RefreshWaiting = false;
            timer1.Stop();
            refrechForm(txtSearch.Text);
            timer1.Start();
            Program.RefreshWaiting = true;
        }
        private void frmCheckPointC_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.CurrentPatient_queue = null;
            timer1.Stop();
        }

        private class WaitingPatientQueue
        {
            public int no { get; set; }
            public int tpr_id { get; set; }
            public string mhs_ename { get; set; }
            public string QueueNo { get; set; }
            public string HN { get; set; }
            public string FullName { get; set; }
            public string package { get; set; }
            public string EN { get; set; }
            public string flag { get; set; }
            public Image out_site { get; set; }
            public TypeEvent type_event { get; set; }
            public int tps_id { get; set; } // ใช้สำหรับ type PE only
        }

        private enum TypeEvent
        {
            PE,
            RE
        }
    }

}