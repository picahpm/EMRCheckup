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
    public partial class frmChoiceRoom : Form
    {
        public frmChoiceRoom()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        //public CheckupDataContext SetDBC
        //{
        //    set
        //    {
        //        dbc = value;
        //    }
        //}
        int select_mrmID = 0;
        public int GetMrmID
        {
            get
            {
                return select_mrmID;
            }
        }
        public int mvtID { get; set; }
        int WaitingTime = 0;
        private void frmChoiceRoom_Load(object sender, EventArgs e)
        {

            var objsite = (from t1 in dbc.mst_hpc_sites
                           where t1.mhs_status == 'A'
                           && t1.mhs_room_chkup == true
                           select new DropdownData
                           {
                               Code = t1.mhs_id,
                               Name = t1.mhs_ename
                           }).ToList();

            DDsiteToSend.DataSource = objsite;
            DDsiteToSend.DisplayMember = "Name";
            DDsiteToSend.ValueMember = "Code";
            if (Program.CheckPointBSiteUse == 0)
            {
                DDsiteToSend.SelectedValue = Program.CurrentSite.mhs_id;
            }
            else
            {
                DDsiteToSend.SelectedValue = Program.CheckPointBSiteUse; //Program.CurrentSite.mhs_id;
            }

            int SendtoSiteId = 0;
            if (Program.CheckPointBSiteUse != 0)
            {
                SendtoSiteId = Program.CheckPointBSiteUse;
            }
            else
            {
                SendtoSiteId = Program.CurrentSite.mhs_id;
            }
            this.Text = Program.GetRoomName();

            int? mvt_id = Program.CurrentPatient_queue.mvt_id;
            string mvt_code = dbc.mst_events.Where(x => x.mvt_id == mvt_id).Select(x => x.mvt_code).FirstOrDefault();
            int? mrm_id = Program.CurrentPatient_queue.mrm_id;
            string mrm_code = dbc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).Select(x => x.mrm_code).FirstOrDefault();

            // Add Button in Gridview
            var objview = (from t1 in dbc.vw_patient_rooms
                           where t1.mhs_id == SendtoSiteId
                           && t1.tpr_id == Program.CurrentRegis.tpr_id
                               //&& (mrm_code == "CB" ? true : (mrm_code == "TE") ? true : t1.mrm_code != mrm_code)
                           && (mrm_code == "CB" ? true : 
                               mrm_code == "US" ? t1.mrm_code != mrm_code 
                               : t1.mvt_code != mvt_code)
                           select t1)
                           .OrderBy(x => x.mhs_id)
                           .ThenBy(x => x.mze_code)
                           .ThenBy(x => x.mrm_seq_room)
                           .Select(t1 => new ChoiceRoomGrid
                               {
                                   mvt_id = t1.mvt_id,
                                   mrm_id = t1.mrm_id,
                                   mrm_code = t1.mrm_code,
                                   mhs_ename = t1.mhs_ename,
                                   mze_ename = t1.mze_ename,
                                   mrm_ename = t1.mrm_ename,
                                   waiting_person = t1.waiting_person_show,
                                   waiting_time = t1.waiting_time,
                                   mvt_code = t1.mvt_code,
                                   vip = t1.patient_vip
                               }).ToList();
            dataGridView2.DataSource = new SortableBindingList<ChoiceRoomGrid>(objview.ToList());
            if (objview.Count() > 0)
            {
                select_mrmID = Convert.ToInt32(dataGridView2["colRoomid", 0].Value);
                mvtID = Convert.ToInt32(dataGridView2["colmvtid", 0].Value);
                WaitingTime = Convert1.ToInt32(dataGridView2["ColWaitingTime", 0].Value);
            }
            if (Program.CurrentSite.mhs_extra_pe_type != true)
            {
                dataGridView2.Columns["colVIP"].Visible = false;
                dataGridView2.Width = dataGridView2.Width - dataGridView2.Columns["colVIP"].Width;
                this.Width = this.Width - dataGridView2.Columns["colVIP"].Width;
            }
        }
        private void selectedRoom()
        {
            DataGridViewRow row = dataGridView2.CurrentRow;
            int select_mrm_id = Convert.ToInt32(row.Cells["colRoomid"].Value.ToString());
            //if (new EmrClass.GetDataMasterCls().GetMstRoomHdr((int)select_mrm_id).mrm_code == "DC")
            //{
                //new Class.FunctionDataCls().stampPEDoctor(Program.CurrentRegis.tpr_id);
            //}
            int select_mvt_id = Convert.ToInt32(row.Cells["colmvtid"].Value.ToString());
            int select_waiting_time = Convert1.ToInt32(row.Cells["ColWaitingTime"].Value);
            if (ProcessSelectRoom(select_mvt_id, select_mrm_id, select_waiting_time))
            {
                select_mrmID = select_mrm_id;
                mvtID = select_mvt_id;
                WaitingTime = select_waiting_time;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                //this.DialogResult = DialogResult.Cancel;
            }
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectedRoom();
            //var gridRow = dataGridView2.CurrentRow;
            //string mrmName = Convert1.ToString(gridRow.Cells["ColName"]);
            //var objroomhdr = (from t1 in dbc.mst_room_hdrs
            //                  where t1.mrm_id == Program.CurrentRoom.mrm_id
            //                  select t1).FirstOrDefault();
            //if (objroomhdr != null && objroomhdr.mrm_code == "EM")
            //{
            //    var timelimit = Program.GetLimitTime("EDT");
            //    if (WaitingTime <= timelimit)
            //    {
            //        this.DialogResult = DialogResult.OK;
            //        //kook add waiting test 
            //        Program.CurrentSite.mhs_id = Convert1.ToInt32(DDsiteToSend.SelectedValue);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Can not send to " + mrmName + " of " + colsite);
            //    }
            //}
            //else
            //{
            //    // send
            //    if (select_mrmID != 0)
            //    {

            //        this.DialogResult = DialogResult.OK;
            //    }
            //    else
            //    {
            //        this.DialogResult = DialogResult.Cancel;
            //    }
            //}
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1)
            //{
            //    select_mrmID = Convert.ToInt32(dataGridView2["colRoomid", e.RowIndex].Value.ToString());
            //    mvtID = Convert.ToInt32(dataGridView2["colmvtid", e.RowIndex].Value.ToString());
            //    WaitingTime = Convert1.ToInt32(dataGridView2["ColWaitingTime", e.RowIndex].Value);
            //}
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                selectedRoom();

                //string mrmName = dataGridView2["ColName", e.RowIndex].Value.ToString();
                //string mrmcode = dataGridView2["ColmrmCode", e.RowIndex].Value.ToString();
                //string mvtcode = dataGridView2["colmvtcode", e.RowIndex].Value.ToString();
                //select_mrmID = Convert.ToInt32(dataGridView2["colRoomid", e.RowIndex].Value.ToString());
                //mvtID = Convert.ToInt32(dataGridView2["colmvtid", e.RowIndex].Value.ToString());
                //WaitingTime = Convert1.ToInt32(dataGridView2["ColWaitingTime", e.RowIndex].Value);

                ////ถ้าเป็น Eye==> Send Manual
                //var objroomhdr = (from t1 in dbc.mst_room_hdrs
                //                  where t1.mrm_id == Program.CurrentRoom.mrm_id
                //                  select t1).FirstOrDefault();

                //bool? EyeDrop = (from t1 in dbc.trn_eye_exam_hdrs
                //                 where t1.tpr_id == Program.CurrentRegis.tpr_id
                //                 select t1.teh_eyedropper).FirstOrDefault();

                //if (objroomhdr != null && objroomhdr.mrm_code != "EN" && mvtcode == "EM" && EyeDrop == true)
                //{//ไม่สามารถส่งไปยัง Station Eye Doctor ได้
                //    //Sumit 03/02/2014
                //    var timelimit = Program.GetLimitTime("EDT");
                //    if (WaitingTime > timelimit)
                //    {
                //        //MessageBox.Show("Can not send to " + mrmName + ", Please Send Eye Nurse Station");
                //        if (System.Windows.Forms.MessageBox.Show("Can not send to " + mrmName + ". Your confirmation will be send or not?", "Send Queue Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.Yes)
                //        {
                //            this.DialogResult = DialogResult.OK;
                //        }
                //        else
                //        {
                //            this.DialogResult = DialogResult.Cancel;
                //        }
                //    }
                //    else
                //    {
                //        this.DialogResult = DialogResult.OK;
                //    }
                //}
                //else
                //{
                //    //ถ้าเป็น CheckPoint B ไม่ต้องเลือก Site ตอน Send Manual

                //    if (objroomhdr != null && objroomhdr.mrm_code == "CB")
                //    {
                //        if (select_mrmID != 0)
                //        {
                //            this.DialogResult = DialogResult.OK;
                //        }
                //        else
                //        {
                //            this.DialogResult = DialogResult.Cancel;
                //        }
                //        return;
                //    }
                //    else if (objroomhdr != null && objroomhdr.mrm_code == "EM")
                //    {
                //        var timelimit = Program.GetLimitTime("EDT");
                //        if (WaitingTime <= timelimit)
                //        {
                //            this.DialogResult = DialogResult.OK;
                //        }
                //        else
                //        {
                //            MessageBox.Show("Can not send to " + mrmName + " of " + colsite);
                //        }
                //    }

                //    //การณี ส่งไปห้องที่ต้องการ แต่ เปลียน Siteอื่น
                //    //frmChoiceSite frmsite = new frmChoiceSite();
                //    //if (frmsite.ShowDialog() == DialogResult.OK)
                //    //{
                //    int siteid = Convert1.ToInt32(DDsiteToSend.SelectedValue);
                //    var objrooms = (from t1 in dbc.mst_room_dtls
                //                    where t1.mst_room_hdr.mhs_id == siteid
                //                    && t1.mst_room_hdr.mrm_code == mrmcode
                //                    select t1).FirstOrDefault();
                //    if (objrooms != null)
                //    {
                //        select_mrmID = objrooms.mrm_id;
                //        if (select_mrmID != 0)
                //        {
                //            this.DialogResult = DialogResult.OK;
                //        }
                //        else
                //        {
                //            this.DialogResult = DialogResult.Cancel;
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("Can not send to " + mrmName + " of " + colsite + ", Please Change HPC Site.");
                //    }
                //    // }
                //}
            }
        }

        private int siteid = 0;
        private void DDsiteToSend_SelectedValueChanged(object sender, EventArgs e)
        {
            if (siteid != Convert1.ToInt32(DDsiteToSend.SelectedValue))
            {
                siteid = Convert1.ToInt32(DDsiteToSend.SelectedValue);
                try
                {
                    int? mvt_id = Program.CurrentPatient_queue.mvt_id;
                    string mvt_code = dbc.mst_events.Where(x => x.mvt_id == mvt_id).Select(x => x.mvt_code).FirstOrDefault();
                    int? mrm_id = Program.CurrentPatient_queue.mrm_id;
                    string mrm_code = dbc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).Select(x => x.mrm_code).FirstOrDefault();

                    // Add Button in Gridview
                    var objview = (from t1 in dbc.vw_patient_rooms
                                   where t1.mhs_id == siteid
                                   && t1.tpr_id == Program.CurrentRegis.tpr_id
                                   && (mrm_code == "CB" ? true : t1.mvt_code != mvt_code)
                                   select new ChoiceRoomGrid
                                   {
                                       mvt_id = t1.mvt_id,
                                       mhs_ename = t1.mhs_ename,
                                       mze_ename = t1.mze_ename,
                                       mrm_ename = t1.mrm_ename,
                                       mrm_id = t1.mrm_id,
                                       mrm_code = t1.mrm_code,
                                       mvt_code = t1.mvt_code,
                                       waiting_person = t1.waiting_person,
                                       waiting_time = t1.waiting_time,
                                       vip = t1.patient_vip
                                   });
                    dataGridView2.DataSource = new SortableBindingList<ChoiceRoomGrid>(objview.ToList());
                }
                catch (Exception ex)
                {
                    Program.MessageError("frmChoiceRoom", "DDsiteToSend_SelectedValueChanged", ex, false);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                if (Program.CurrentUser != null)
                {
                    ////bool IsLogin = Program.CheckSessionLoginRoom(Program.CurrentUser.mut_id);
                    //if (IsLogin == false)
                    //{// return false =แสดงว่ามีการเข้าใช้งานเครื่องอื่นแล้ว : return true=แสดงว่าเป็นเครื่องที่ใช้งานอยู่
                    //    this.Close();
                    //    return;
                    //}

                    bool IsKicking = Class.ClsManageUserLogin.checkKickCurrentUser();
                    if (IsKicking)
                    {
                        this.Close();
                        return;
                    }
                }
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Timmer All Room Fail : " + ex.Message);
            }
        }

        private bool ProcessSelectRoom(int select_mvt_id, int select_mrm_id, int waitingTime)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            Class.FunctionDataCls func = new Class.FunctionDataCls();
            int tpr_id = Program.CurrentRegis.tpr_id;
            int mrm_id = Program.CurrentRoom.mrm_id;
            mst_room_hdr currentRoom = mst.GetMstRoomHdr(mrm_id);
            if (currentRoom != null && currentRoom.mrm_code == "EM" && Program.CurrentRoom.mrd_type == 'N')
            {
                mst_event selectRoom = mst.GetMstEvent(select_mvt_id);
                if (func.checkEyeDropper(tpr_id))
                {
                    if (selectRoom != null)
                    {
                        if (selectRoom.mvt_code == "EM")
                        {
                            if (MessageBox.Show("มีการเลือก Eye Dropper ซึ่งมีการรอ 15 นาที" + Environment.NewLine + "คุณต้องการดำเนินการต่อหรือไม่?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            var timelimit = Program.GetLimitTime("EDT");
                            if (waitingTime > timelimit)
                            {
                                if (MessageBox.Show("ห้องที่คุณเลือก มีเวลารอเกิน " + timelimit.ToString() + " นาที" + Environment.NewLine + "คุณต้องการดำเนินการต่อหรือไม่?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    if (selectRoom != null)
                    {
                        if (selectRoom.mvt_code != "EM")
                        {
                            if (MessageBox.Show("ส่งห้องที่ไม่ใช่ Eye Doctor คุณต้องการจะดำเนินการต่อหรือไม่?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

    }




    //if (result != null)
    //                    {
    //                        int mrm_id = Program.CurrentRoom.mrm_id;
    //                        mst_room_hdr currentRoom = mst.getMstRoomHdr(mrm_id);
    //                        if (currentRoom != null && currentRoom.mrm_code == "EM" && Program.CurrentRoom.mrd_type == 'N')
    //                        {
    //                            mst_event selectRoom = mst.getMstEvent(result.mvt_id);
    //                            if (func.checkEyeDropper(tpr_id))
    //                            {
    //                                if (selectRoom != null)
    //                                {
    //                                    if (selectRoom.mvt_code == "EM")
    //                                    {
    //                                        if (MessageBox.Show("มีการเลือก Eye Dropper ซึ่งมีการรอ 15 นาที" + Environment.NewLine + "คุณต้องการดำเนินการต่อหรือไม่?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
    //                                        {
    //                                            return result;
    //                                        }
    //                                        else
    //                                        {
    //                                            var timelimit = Program.GetLimitTime("EDT");
    //                                            if (result.waitingTime > timelimit)
    //                                            {
    //                                                if (MessageBox.Show("ห้องที่คุณเลือก มีเวลารอเกิน " + timelimit.ToString() + " นาที" + Environment.NewLine + "คุณต้องการดำเนินการต่อหรือไม่?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
    //                                                {
    //                                                    return result;
    //                                                }
    //                                                else
    //                                                {
    //                                                    goto reSend;
    //                                                }
    //                                            }
    //                                            else
    //                                            {
    //                                                return result;
    //                                            }
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                            else
    //                            {
    //                                if (selectRoom != null)
    //                                {
    //                                    if (selectRoom.mvt_code != "EM")
    //                                    {
    //                                        if (MessageBox.Show("ส่งห้องที่ไม่ใช่ Eye Doctor คุณต้องการจะดำเนินการต่อหรือไม่?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
    //                                        {
    //                                            return result;
    //                                        }
    //                                        else
    //                                        {
    //                                            goto reSend;
    //                                        }
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            return result;
    //                        }
    //                    }
    class ChoiceRoomGrid
    {
        public string mrm_ename { get; set; }
        public string mze_ename { get; set; }
        public int mvt_id { get; set; }
        public int? mrm_id { get; set; }
        public int? waiting_person { get; set; }
        public int? waiting_time { get; set; }
        public string mrm_code { get; set; }
        public string mvt_code { get; set; }
        public string mhs_ename { get; set; }
        public int? vip { get; set; }
    }
}
