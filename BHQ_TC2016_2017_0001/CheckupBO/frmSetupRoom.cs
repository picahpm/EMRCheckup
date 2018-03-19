using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace CheckupBO
{
    public partial class frmSetupRoom : Form
    {
        public frmSetupRoom()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        private void frmSetupRoom_Load(object sender, EventArgs e)
        {
            //room
            LoadDD();//ทำครั้งเดียว

            Loaddata(0);//tab1
            LoadDataEvent();//tab2
            LoadRoomEvent(0);//tab3
        }

        private void LoadDD()
        {
            //Set dropdown Site
            var objsitelist = (from t1 in dbc.mst_hpc_sites
                                select new DropdownData
                                {
                                    Code = t1.mhs_id,
                                    Name = t1.mhs_ename
                                }).ToList();
            DropdownData newitem = new DropdownData();
            newitem.Code = 0;
            newitem.Name = "Select All";
            objsitelist.Insert(0, newitem);
            DDsite.ValueMember = "Code";
            DDsite.DisplayMember = "Name";
            DDsite.DataSource = objsitelist;
            DDsite.SelectedIndex = 0;
            
           
            

            //Roomhdr
            var objsitesearch = (from t1 in dbc.mst_hpc_sites
                               select new DropdownData
                               {
                                   Code = t1.mhs_id,
                                   Name = t1.mhs_ename
                               }).ToList();
            
            DDsite_roomhdr.ValueMember = "Code";
            DDsite_roomhdr.DisplayMember = "Name";
            DDsite_roomhdr.DataSource = objsitesearch;
            DDsite_roomhdr.SelectedIndex = 0;

            DDsite_Roomhdr_replaceRoom.ValueMember = "Code";
            DDsite_Roomhdr_replaceRoom.DisplayMember = "Name";
            DDsite_Roomhdr_replaceRoom.DataSource = objsitesearch;
            DDsite_Roomhdr_replaceRoom.SelectedIndex = 0;

            DateTime dtnow=Program.GetServerDateTime();
            var objzone = (from t1 in dbc.mst_zones
                           select new DropdownData
                           {
                               Code = t1.mze_id,
                               Name = t1.mze_ename
                           }).ToList();

            DDzone.ValueMember = "Code";
            DDzone.DisplayMember = "Name";
            DDzone.DataSource = objzone;
            DDzone.SelectedIndex = 0;
            //

            //set dropdown Room
            var objroomlist = (from t1 in dbc.mst_room_hdrs
                               select new DropdownData
                               {
                                   Code = t1.mrm_id,
                                   Name = t1.mrm_ename
                               }).ToList();
            DDRoom_station_replaceRoom.ValueMember = "Code";
            DDRoom_station_replaceRoom.DisplayMember = "Name";
            DDRoom_station_replaceRoom.DataSource = objroomlist;
            DDRoom_station_replaceRoom.SelectedIndex = 0;
            //Add ComboboxColumns Site and hiddin ColumnSite
            var objsite = (from t1 in dbc.mst_hpc_sites
                           select new
                           {
                               Value = t1.mhs_id,
                               Text = t1.mhs_ename
                           }).ToList();
            DataGridViewComboBoxColumn dgvCmbForums = new DataGridViewComboBoxColumn();
            {
                dgvCmbForums.HeaderText = "Site";// Hearder name of column
                dgvCmbForums.ValueMember = "Value";// Add items into Combobox
                dgvCmbForums.DisplayMember = "Text";
                dgvCmbForums.DataPropertyName = "mhs_id";
                dgvCmbForums.DataSource = objsite;
                dgvCmbForums.DisplayIndex = 1;
                dgvCmbForums.Width = 80;
                dgvCmbForums.DisplayStyleForCurrentCellOnly = true;
                dgvCmbForums.FlatStyle = FlatStyle.Flat;
            }
            GridRoomHDR.Columns["Colmhs_id"].Visible = false;
            GridRoomHDR.Columns.Add(dgvCmbForums);

            //Add ComboboxColumns Zone and hiddin Columnzone
            var objZone = (from t1 in dbc.mst_zones
                           select new
                           {
                               Value = t1.mze_id,
                               Text = t1.mze_ename
                           }).ToList();
            DataGridViewComboBoxColumn dgvCmbzone = new DataGridViewComboBoxColumn();
            {
                dgvCmbzone.HeaderText = "Zone";// Hearder name of column
                dgvCmbzone.ValueMember = "Value";// Add items into Combobox
                dgvCmbzone.DisplayMember = "Text";
                dgvCmbzone.DataPropertyName = "mze_id";
                dgvCmbzone.DataSource = objZone;
                dgvCmbzone.DisplayIndex = 2;
                dgvCmbzone.Width = 70;
                dgvCmbzone.DisplayStyleForCurrentCellOnly = true;
                dgvCmbzone.FlatStyle = FlatStyle.Flat;
            }
            GridRoomHDR.Columns["Colmze_id"].Visible = false;
            GridRoomHDR.Columns.Add(dgvCmbzone);

            //tabEvent ***********************************
            List<DropdownDataChar> mainsublist = new List<DropdownDataChar>();
            DropdownDataChar item1 = new DropdownDataChar();
            item1.Code = 'M'; item1.Name = "Main";
            mainsublist.Add(item1);
            DropdownDataChar item2 = new DropdownDataChar();
            item2.Code = 'S'; item2.Name = "Sub";
            mainsublist.Add(item2);
            //DropdownDataChar item0 = new DropdownDataChar();
            //item0.Code = null; item0.Name = "";
            //mainsublist.Add(item0);

            DataGridViewComboBoxColumn dgvCmbtype_cate = new DataGridViewComboBoxColumn();
            {
                // Hearder name of column
                dgvCmbtype_cate.HeaderText = "Type";

                // Add items into Combobox
                dgvCmbtype_cate.ValueMember = "Code";
                dgvCmbtype_cate.DisplayMember = "Name";
                dgvCmbtype_cate.DataPropertyName = "mvt_type_cate";
                dgvCmbtype_cate.DataSource = mainsublist;

                dgvCmbtype_cate.Width = 80;
                dgvCmbtype_cate.DisplayStyleForCurrentCellOnly = true;
                dgvCmbtype_cate.FlatStyle = FlatStyle.Flat;
            }
            dataGridView3.Columns.Add(dgvCmbtype_cate);
            
            dataGridView3.Columns["Colmvt_type_cate"].Visible = false;


            //Tab Room & Event *****************************
            var objsiteRoomEventlist = (from t1 in dbc.mst_hpc_sites
                                        select new DropdownData
                                        {
                                            Code = t1.mhs_id,
                                            Name = t1.mhs_ename
                                        }).ToList();
            //Room && Event
            objsiteRoomEventlist.Insert(0, newitem);
            DDSiteRoomEvent_Search.ValueMember = "Code";
            DDSiteRoomEvent_Search.DisplayMember = "Name";
            DDSiteRoomEvent_Search.DataSource = objsiteRoomEventlist;
            DDSiteRoomEvent_Search.SelectedIndex = 0;
            //***********************************
        }
        private void CancelAllTab()
        {
            dbc.Dispose();
            dbc = new InhCheckupDataContext();
            Loaddata(0);
            LoadDataEvent();
            LoadRoomEvent(0);
        }

        //Tab Room************************
        #region "Room"

        private void Loaddata(int siteid)
        {
            var objroom = (from t1 in dbc.mst_room_hdrs
                           select t1);
            if (siteid != 0)
            {
                objroom = objroom.Where(x => x.mhs_id == siteid);
            }
            RoomHDRbindingSource1.DataSource = objroom;
        }

        private void GridRoomHDR_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridRoomHDR.SetRuningNumber("Column1");
        }
        private void GridRoomDTL_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridRoomDTL.SetRuningNumber("Column7");
        }

        private void ch_Active_roomhdr_MouseClick(object sender, MouseEventArgs e)
        {
            mst_room_hdr currenthdr = (mst_room_hdr)RoomHDRbindingSource1.Current;
            if (currenthdr != null)
            {
                var data = (ch_Active_roomhdr.Checked == true) ? 'A' : 'I';
                if (data != currenthdr.mrm_status)
                {
                    currenthdr.mrm_status = data;
                }
            }
        }
        private void chRoomActive_MouseClick(object sender, MouseEventArgs e)
        {
            mst_room_dtl currentdtl = (mst_room_dtl)mstroomdtlsBindingSource.Current;
            if (currentdtl != null)
            {
                var data = (chRoomActive.Checked == true) ? 'A' : 'I';
                if (data != currentdtl.mrd_status)
                {
                    currentdtl.mrd_status = data;
                }
            }
        }
        private void RD_Type_Doctor_MouseClick(object sender, MouseEventArgs e)
        {
            mst_room_dtl currentdtl = (mst_room_dtl)mstroomdtlsBindingSource.Current;
            if (currentdtl != null)
            {
                var datach = Program.GetValueGroupBox(GBRoomType);
                if (datach != currentdtl.mrd_type)
                {
                    currentdtl.mrd_type = datach;
                }
            }
        }
        private void btnNewRoomhdr_Click(object sender, EventArgs e)
        {
            RoomHDRbindingSource1.AddNew();
            DateTime dtnow=Program.GetServerDateTime();
             mst_room_hdr currenthdr = (mst_room_hdr)RoomHDRbindingSource1.Current;
             if (currenthdr != null)
             {
                 string username = Program.CurrentUser.mut_username;
                 int mshid = (from t1 in dbc.mst_hpc_sites select t1.mhs_id).FirstOrDefault();
                 int mzeid = (from t1 in dbc.mst_zones select t1.mze_id).FirstOrDefault();
                 currenthdr.mhs_id = mshid;
                 currenthdr.mze_id = mzeid;
                 currenthdr.mrm_create_by = username;
                 currenthdr.mrm_create_date = dtnow;
                 currenthdr.mrm_update_by = username;
                 currenthdr.mrm_update_date = dtnow;
             }
        }
        private void btnNewRoomdtl_Click(object sender, EventArgs e)
        {
            mstroomdtlsBindingSource.AddNew();
            DateTime dtnow=Program.GetServerDateTime();
            mst_room_dtl currentdtl = (mst_room_dtl)mstroomdtlsBindingSource.Current;
            if (currentdtl != null)
            {
                string username = Program.CurrentUser.mut_username;
                currentdtl.mrd_create_by = username;
                currentdtl.mrd_create_date = dtnow;
                currentdtl.mrd_update_by = username;
                currentdtl.mrd_update_date = dtnow;
            }
        }
        //-------------search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int siteid = Utility.GetInteger(DDsite.SelectedValue);
            Loaddata(siteid);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DDsite.SelectedIndex = 0;
            Loaddata(0);
        }

        //-------bindingSource
        private void RoomHDRbindingSource1_CurrentChanged1(object sender, EventArgs e)
        {
            lbmsgAlert.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnSaveStationRoom_Click(null, null);
                }
                else
                {
                    dbc.Dispose();
                    dbc = new InhCheckupDataContext();
                    Loaddata(0);
                    return;
                }
            }
        }
        private void RoomHDRbindingSource1_CurrentItemChanged1(object sender, EventArgs e)
        {
            mst_room_hdr currenthdr = (mst_room_hdr)RoomHDRbindingSource1.Current;
            if (currenthdr != null)
            {
                //status
                if (currenthdr.mrm_status == 'A')
                    ch_Active_roomhdr.Checked = true;
                else
                    ch_Active_roomhdr.Checked = false;
            }
        }
        private void mstroomdtlsBindingSource_CurrentChanged1(object sender, EventArgs e)
        {
            lbmsgAlert.Text = "";

            //char? admintype = Program.GetValueRadioTochar(pnlAdminType);
            //if(admintype==
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnSaveStationRoom_Click(null, null);
                }
                else
                {
                    dbc.Dispose();
                    dbc = new InhCheckupDataContext();
                    Loaddata(0);

                    return;
                }
            }


        }
        private void mstroomdtlsBindingSource_CurrentItemChanged1(object sender, EventArgs e)
        {
            mst_room_dtl currentdtl = (mst_room_dtl)mstroomdtlsBindingSource.Current;
            if (currentdtl != null)
            {
                //status
                if (currentdtl.mrd_status == 'A')
                    chRoomActive.Checked = true;
                else
                    chRoomActive.Checked = false;

                //Type
                Program.SetValueRadioGroupBox(GBRoomType, Convert1.ToString(currentdtl.mrd_type));
                //
            }
        }

        //------------Save && Cancel
        private void btnSaveStationRoom_Click(object sender, EventArgs e)
        {
            lbmsgAlert.Text = "";
            mstroomdtlsBindingSource.EndEdit();
            RoomHDRbindingSource1.EndEdit();

            dbc.SubmitChanges();
            lbmsgAlert.Text = "Save data completed.";
            LoadRoomEvent(0);
        }
        private void btnCancelStationRoom_Click(object sender, EventArgs e)
        {
            CancelAllTab();
        }
        #endregion

        //tab Event************************
        #region "Event"
        private void LoadDataEvent()
        {
            var objEventList = (from t1 in dbc.mst_events 
                                select t1);
            EventbindingSource1.DataSource = objEventList;
        }
        private void btnEventAddNew_Click(object sender, EventArgs e)
        {
            EventbindingSource1.AddNew();
            mst_event newitem = (mst_event)EventbindingSource1.Current;
            string createBy=Program.CurrentUser.mut_username;
            DateTime dtnow=Program.GetServerDateTime();
            newitem.mvt_create_by =  createBy;
            newitem.mvt_create_date = dtnow;
            newitem.mvt_update_by = createBy;
            newitem.mvt_update_date = dtnow;

        }
        private void btnSaveEvent_Click(object sender, EventArgs e)
        {
            lbEventMsgAlert.Text = "";
            EventbindingSource1.EndEdit();
            dbc.SubmitChanges();
            lbEventMsgAlert.Text = "Save data completed.";

            //LoadRoomEvent
            LoadRoomEvent(0);
        }

        private void btnEventCancel_Click(object sender, EventArgs e)
        {
            CancelAllTab();
        }
        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView3.SetRuningNumber();
        }

        private void EventbindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            lbEventMsgAlert.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnSaveEvent_Click(null, null);
                }
                else
                {
                    dbc.Dispose();
                    dbc = new InhCheckupDataContext();
                    LoadDataEvent();
                    return;
                }
            }
           
        }
        private void EventbindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
             mst_event newitem = (mst_event)EventbindingSource1.Current;
            if (newitem != null)
            {
                if (newitem.mvt_status == 'A')
                {
                    chEventActive.Checked = true;
                }
                else
                {
                    chEventActive.Checked = false;
                }

                Program.SetValueRadioGroup(pnlMainSub, newitem.mvt_type_cate);
            }
        }

        private void RDEvent_Type_Main_MouseClick(object sender, MouseEventArgs e)
        {
            mst_event currentevent = (mst_event)EventbindingSource1.Current;
            if (currentevent != null)
            {
                var data = Program.GetValueRadioTochar(pnlMainSub);
                if (data != currentevent.mvt_type_cate)
                {
                    currentevent.mvt_type_cate = data;
                }
            }
        }
        private void chEventActive_MouseClick(object sender, MouseEventArgs e)
        {
            mst_event currentevent = (mst_event)EventbindingSource1.Current;
            if (currentevent != null)
            {
                var data = (chEventActive.Checked == true) ? 'A' : 'I';
                if (data != currentevent.mvt_status)
                {
                    currentevent.mvt_status = data;
                }
            }
        }
        #endregion

        //Room & Event************************
        #region "RoomEvent
        private void LoadRoomEvent(int siteid)
        {
            var RoomList = (from t1 in dbc.mst_room_hdrs
                            select new
                            {   t1.mst_hpc_site.mhs_ename,
                                t1.mhs_id,
                                t1.mrm_id,
                                t1.mrm_code,
                                t1.mrm_ename
                            }).ToList();
            if (siteid > 0)
            {
                RoomList = RoomList.Where(x => x.mhs_id == siteid).ToList();
            }
            GridRoomEvent_Station.DataSource = RoomList;
            GridRoomEvent_Station.Columns["Colmhsid"].Visible = false;
            GridRoomEvent_Station.Columns["Colmrmid"].Visible = false;
            GridRoomEvent_Station.Select();
            GridRoomEvent_Station.ClearSelection();

            var EventAllList = (from t1 in dbc.mst_events
                                where t1.mvt_status == 'A'
                                select new { t1.mvt_id,t1.mvt_code,t1.mvt_ename }).ToList();
            GridRoomEvent_Event.DataSource = EventAllList;
            GridRoomEvent_Station.ClearSelection();
            GridRoomEvent_Event.ClearSelection();
            ClearSelectEvent();
        }

        private void btnRoomEvent_Search_Click(object sender, EventArgs e)
        {
            int siteid = Utility.GetInteger( DDSiteRoomEvent_Search.SelectedValue);
            LoadRoomEvent(siteid);
        }
        private void btnRoomEvent_Clear_Click(object sender, EventArgs e)
        {
            LoadRoomEvent(0);
        }

        private void ClearSelectEvent()
        {
            foreach (DataGridViewRow row in GridRoomEvent_Event.Rows)
            {
                row.Cells["ColRoomEvent_EventSelect"].Value = false;
            }
        }
        private List<int> GetSelectEvent_mvtid()
        {
            List<int> mvtidlist = new List<int>();
            for (int iRow = 0; iRow <= GridRoomEvent_Event.Rows.Count - 1; iRow++)
            {
                Boolean isselect = Convert1.ToBoolean(GridRoomEvent_Event["ColRoomEvent_EventSelect", iRow].Value);
                int mvtid = Utility.GetInteger(GridRoomEvent_Event["ColRoomEvent_Event_mvtid", iRow].Value);
                if (isselect == true)
                {
                    mvtidlist.Add(mvtid);
                }
            }
            return mvtidlist;
            //ColRoomEvent_Event_mvtid
        }
        private void SetSelectEvent(List<int> mvtidList)
        {
            //for (int iRow = 0; iRow <= GridRoomEvent_Event.Rows.Count - 1; iRow++)
            //{
            foreach (DataGridViewRow row in GridRoomEvent_Event.Rows)
            {
                //GridRoomEvent_Event[0, iRow].Value=
                int mvt_id = Utility.GetInteger(row.Cells["ColRoomEvent_Event_mvtid"].Value);
                var icount = (from t1 in mvtidList where t1 == mvt_id select t1).Count();

                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["ColRoomEvent_EventSelect"];
                if (icount > 0)
                {
                    chk.Value = true;
                    chk.Selected = true;
                }
                else
                {
                    chk.Value = false;
                    chk.Selected = false;
                }
            }
        }
        private void GridRoomEvent_Station_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lbRoomEvent_lbMsgAlert.Text = "";
            if (e.RowIndex > -1)
            {
                int mrmid = Utility.GetInteger(GridRoomEvent_Station["Colmrmid", e.RowIndex].Value);

                var objeventlist = (from t1 in dbc.mst_room_events
                                    where t1.mrm_id == mrmid
                                    select t1.mvt_id).ToList();
                SetSelectEvent(objeventlist);
            }
        }

        private void btnRoomEvent_Import_Click(object sender, EventArgs e)
        {

        }
        private void btnsaveRoomEvent_Click(object sender, EventArgs e)
        {
            lbRoomEvent_lbMsgAlert.Text = "";
            List<int> mvtselectItem = GetSelectEvent_mvtid();
            
            if (GridRoomEvent_Station.CurrentRow != null)
            {
                var objmrm = GridRoomEvent_Station.CurrentRow;
                int mrmid = Utility.GetInteger(objmrm.Cells["Colmrmid"].Value);
                var objdel=(from t1 in dbc.mst_room_events where t1.mrm_id ==mrmid select t1).ToList();
                if (objdel.Count() > 0)
                {
                    dbc.mst_room_events.DeleteAllOnSubmit(objdel);
                }

                foreach(int mvtid in mvtselectItem){
                    mst_room_event newitem = new mst_room_event();
                    newitem.mrm_id = mrmid;
                    newitem.mvt_id = mvtid;
                    dbc.mst_room_events.InsertOnSubmit(newitem);
                }
            }
            dbc.SubmitChanges();
            lbRoomEvent_lbMsgAlert.Text = "Save data Completed.";
        }

        private void GridRoomEvent_Station_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridRoomEvent_Station.SetRuningNumber();
        }
        private void GridRoomEvent_Event_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridRoomEvent_Event.SetRuningNumber();
        }

        private void btnRoomEventCancel_Click(object sender, EventArgs e)
        {
            CancelAllTab();
        }
        #endregion

       
    }
}
