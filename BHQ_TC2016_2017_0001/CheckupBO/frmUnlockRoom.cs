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
    public partial class frmUnlockRoom : Form
    {
        public frmUnlockRoom()
        {
            InitializeComponent();
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void frmUnlockRoom_Load(object sender, EventArgs e)
        {
            LoadDD(); 
            LoadGrid(0, 0);
        }

        private void LoadDD()
        {
            //Set dropdown Site
            var objRaceGroup = (from t1 in dbc.mst_hpc_sites
                                select new DropdownData
                                {
                                   Code  = t1.mhs_id,
                                   Name  = t1.mhs_ename
                                }).ToList();
            DropdownData newitem =new DropdownData();
            newitem.Code = 0;
            newitem.Name = "Select All";

            objRaceGroup.Insert(0,newitem);
            DDSite.ValueMember = "Code";
            DDSite.DisplayMember = "Name";
            DDSite.DataSource = objRaceGroup;
            DDSite.SelectedIndex = 0;
            //set dropdown Room
            var objroomlist = (from t1 in dbc.mst_room_hdrs
                               select new DropdownData
                               {
                                   Code = t1.mrm_id,
                                   Name = t1.mrm_ename
                               }).ToList();
            objroomlist.Insert(0, newitem);
            DDRoom.ValueMember = "Code";
            DDRoom.DisplayMember = "Name";
            DDRoom.DataSource = objroomlist;
            DDRoom.SelectedIndex = 0;

        }
        private void LoadGrid(int mhs_id,int mrm_id)
        {
            var objListRoom = (from t1 in dbc.mst_room_hdrs
                               select new
                               {
                                   siteName=t1.mst_hpc_site.mhs_ename,
                                   RoomName=t1.mrm_ename,
                                   mrmId=t1.mrm_id,
                                   mhsId=t1.mhs_id
                               }).ToList();
            if (mhs_id > 0)
            {
                objListRoom = objListRoom.Where(x => x.mhsId == mhs_id).ToList();
            }
            if (mrm_id > 0)
            {
                objListRoom = objListRoom.Where(x => x.mrmId == mrm_id).ToList();
            }

            GridSiteRoom.DataSource = objListRoom;
            GridSiteRoom.Columns["ColmhsID"].Visible = false;
            GridSiteRoom.Columns["ColmrmID"].Visible = false;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            lbmsgAlert.Text = "";
            int mhsid=Convert1.ToInt32(DDSite.SelectedValue);
            int mrmid=Convert1.ToInt32(DDRoom.SelectedValue);
            LoadGrid(mhsid, mrmid);
        }

        private void GridSiteRoom_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridSiteRoom.SetRuningNumber();
            ShowRoomLock(0);
        }

        private void btnUnlockRoom_Click(object sender, EventArgs e)
        {
            lbmsgAlert.Text = "";
            string msgdisplay = "";
            DateTime dateNow = Program.GetServerDateTime();
            for (int iRow = 0; iRow <= GridRoomLock.Rows.Count - 1; iRow++)
            {
                Boolean isselect = Convert1.ToBoolean(GridRoomLock["Colselect", iRow].Value);
                int mrdid = Convert1.ToInt32(GridRoomLock["Colmrd_id", iRow].Value);
                if (isselect == true)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        cdc.log_user_logins.Where(x => x.mrd_id == mrdid && x.lug_end_date == null).ToList()
                                           .ForEach(x => x.lug_end_date = dateNow);
                        cdc.SubmitChanges();
                    }
                    msgdisplay = "Unlock completed.";
                }
            }
            btnsearch_Click(null, null);
            ShowRoomLock(0);
            lbmsgAlert.Text = msgdisplay;
        }

        private void GridSiteRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                lbmsgAlert.Text = "";
                ShowRoomLock(e.RowIndex);
            }
        }
        private void ShowRoomLock(int rowid)
        {
            int mrmid = Convert1.ToInt32(GridSiteRoom["ColmrmID", rowid].Value);
                /*  select mrd.mrd_ename, COUNT(1)
                    from mst_room_dtl mrd,
                    log_user_login lug
                    where mrd.mrd_id = lug.mrd_id
                    and lug.lug_end_date is null
                    and mrd.mrm_id = mrm_id
                    group by mrd.mrd_ename
                ]*/
            var objlistLock = (from lug in dbc.log_user_logins
                               from mrd in dbc.mst_room_dtls
                               from mut in dbc.mst_user_types
                               where mrd.mrd_id == lug.mrd_id
                               && lug.lug_end_date == null
                               && mrd.mrm_id == mrmid
                               && lug.mut_id == mut.mut_id
                               select new
                               {
                                   mrd.mrd_ename,
                                   mrd.mrd_id,
                                   mut.mut_username,
                                   name = mut.mut_t_fname + " " + mut.mut_t_lname
                               }).ToList();
            var objgroup = objlistLock.GroupBy(x => x).Select(group => new
            {
                mrd_ename = group.FirstOrDefault().mrd_ename,
                mrd_id = group.FirstOrDefault().mrd_id,
                countperson = group.Count(),
                username = group.FirstOrDefault().mut_username,
                name = group.FirstOrDefault().name
            }).OrderBy(x => x.mrd_ename);
            GridRoomLock.DataSource = objgroup.ToList();
        }
        private void GridRoomLock_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridRoomLock.SetRuningNumber();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbmsgAlert.Text = "";
            DDSite.SelectedIndex = 0;
            DDRoom.SelectedIndex = 0;
        }

        private void DDRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
