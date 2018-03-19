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
    public partial class frmSetupUser : Form
    {
        private bool _mstContentClick = false;
        private bool _mstHeaderClick = false;

        public frmSetupUser()
        {
            InitializeComponent();
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void frmSetupUser_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            
            AddCheckHeaderColumn();
            LoadUserdata("");
            LoadSite();


        }

        private void AddCheckHeaderColumn()
        {

            CheckBox ckBox = new CheckBox();
            //Get the column header cell bounds
            Rectangle rect = this.gvMstRoom.GetCellDisplayRectangle(0, -1, true);
            ckBox.Size = new Size(13, 13);
            //Change the location of the CheckBox to make it stay on the header
            ckBox.Location = new Point(19, 6);
            ckBox.CheckedChanged += new EventHandler(ckBox_CellClick);
            //Add the CheckBox into the DataGridView
            this.gvMstRoom.Controls.Add(ckBox);
            
        }

        private void ckBox_CellClick(object sender, EventArgs e)
        {
            if (_mstContentClick)
                _mstContentClick = false;
            else
            {
                CheckBox chk = (CheckBox)sender;
                for (int j = 0; j < this.gvMstRoom.RowCount; j++)
                {
                    _mstHeaderClick = true;
                    this.gvMstRoom[0, j].Value = chk.Checked;
                }
                this.gvMstRoom.EndEdit();
            }
        }

        private void LoadSite()
        {
            try
            {
                ddlMstSite.ValueMember = "mhs_id";
                ddlMstSite.DisplayMember = "mhs_ename";
                ddlMstSite.DataSource = (from sitelist in dbc.mst_hpc_sites
                                         where sitelist.mhs_type == 'P'
                                         select new
                                         {
                                             mhs_id = sitelist.mhs_id ,
                                             mhs_ename = sitelist.mhs_ename
                                         }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("Error Load Site : " + ex.Message);
            }
        }

        private void LoadUserdata(string strSeach)
        {
            var mstusertypelist = (from t1 in dbc.mst_user_types
                                   select t1);
                                   //.DefaultIfEmpty(DateTime.Now);
            if (strSeach.Length > 0)
            {
                mstusertypelist = mstusertypelist.Where(x => x.mut_fullname.Contains(strSeach)
                                                            //|| x.mut_e_fname.Contains(strSeach)
                                                            //|| x.mut_e_lname.Contains(strSeach)
                                                            || x.mut_username.Contains(strSeach)
                                                            || x.mut_carevider_code.Contains(strSeach)
                                                      );

            }
            mstUserTypebindingSource1.DataSource = mstusertypelist;
            GridUserSetting.Columns["mutidDataGridViewTextBoxColumn"].Visible = false;
        }

        private void mstUserTypebindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            lbmsgalert.Text = "";

            //char? admintype = Program.GetValueRadioTochar(pnlAdminType);
            //if(admintype==
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveData();
                }
                else
                {
                    btnClaear_cancel_Click(null, null);

                    return;
                }
            }

            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            gvUserRoom.DataBindings.Clear();
            if (currentmut != null)
            {
                //start date
                if (currentmut.mut_effective_date != null)
                    txtEffectDate.Value = Convert.ToDateTime(currentmut.mut_effective_date);
                else
                    txtEffectDate.Value = DateTime.Now;
                //end date
                if (currentmut.mut_expire_date != null)
                {                    
                    rdoExpireDt.Checked = true;
                    txtExpireDate.Enabled = true;
                    txtExpireDate.Value = Convert.ToDateTime(currentmut.mut_expire_date);
                }
                else
                {
                    rdoNonExpire.Checked = true;
                    txtExpireDate.Enabled = false;
                    txtExpireDate.Value = DateTime.Now;
                }

                //status
                if (currentmut.mut_status == 'A')
                    ChStatus.Checked = true;
                else
                    ChStatus.Checked = false;
                //Gender
                Program.SetValueRadioGroup(pnlGender, currentmut.mut_gender);
                //mutType
                Program.SetValueRadioGroup(pnlmut_type, currentmut.mut_type);

                //Admin Type
                if (currentmut.mut_admin == true)
                {
                    pnlAdminType.Enabled = true;
                    Program.SetValueRadioGroup(pnlAdminType, currentmut.mut_admin_type);
                }
                else
                {
                    pnlAdminType.Enabled = false;
                    RDAdmin_UserAdmin.Checked = true;
                    RDAdmin_UserAdmin.Checked = false;
                }
                //
                Clearbutton();

                LoadRoom( ddlMstSite.SelectedValue == null? 1 : (int)ddlMstSite.SelectedValue, currentmut.mut_id);
                LoadUserRoom(currentmut.mut_id);

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUserdata(txtUserNameSearch.Text);
        }
        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtUserNameSearch.Text = "";
            LoadUserdata("");
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            SetbuttongSave();
            mstUserTypebindingSource1.AddNew();
            DateTime dtnow = Program.GetServerDateTime();
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            currentmut.mut_login_status = '0';//0 =not Login ,1=login
            currentmut.mut_update_by = Program.CurrentUser.mut_username;
            currentmut.mut_update_date = dtnow;
            currentmut.mut_create_by = Program.CurrentUser.mut_username;
            currentmut.mut_create_date = dtnow;
            currentmut.mut_admin = true;
            currentmut.mut_type = 'N';
            currentmut.mut_CanSendQueue = true;
            currentmut.mut_out_checkup = false;
            currentmut.mut_gender = 'F';
            RDGenderF.Checked = true;
            RDuserType_N.Select();
            gvUserRoom.Rows.Clear();
            btnEditUser_Click(sender, e);
        }
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            SetbuttongSave();
            
            
        }
        private void btnClaear_cancel_Click(object sender, EventArgs e)
        {
            dbc.Dispose();
            dbc = new InhCheckupDataContext();
            LoadUserdata("");
            Clearbutton();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Clearbutton();
            SaveData();

        }
        private void SaveData()
        {
            try
            {
                mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
                if (currentmut != null)
                {
                    //start date
                    currentmut.mut_effective_date = txtEffectDate.Value;
                    //end date
                    if (rdoExpireDt.Checked)
                        currentmut.mut_expire_date = txtExpireDate.Value;

                    //status
                    if (ChStatus.Checked == true)
                        currentmut.mut_status = 'A';
                    else
                        currentmut.mut_status = 'I';
                    //Gender

                    currentmut.mut_gender = Program.GetValueRadioTochar(pnlGender);
                    //mutType
                    currentmut.mut_type = Program.GetValueRadioTochar(pnlmut_type);

                    //Admin Type
                    if (currentmut.mut_admin == true)
                    {
                        currentmut.mut_admin_type = Program.GetValueRadioTochar(pnlAdminType);
                    }
                    else
                    {
                        currentmut.mut_admin_type = null;
                    }

                }
                mstUserTypebindingSource1.EndEdit();
                dbc.SubmitChanges();

                currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
                for (int i = 0; i < gvUserRoom.Rows.Count; i++)
                {
                    if (gvUserRoom["userRoomID", i].Value == null)
                    {
                        mst_user_room newRoom = new mst_user_room();
                        newRoom.mut_id = currentmut.mut_id;
                        newRoom.mrm_id = (int)gvUserRoom["userMrmID", i].Value;
                        newRoom.mut_username = currentmut.mut_username;
                        dbc.mst_user_rooms.InsertOnSubmit(newRoom);
                    }
                    else if (!gvUserRoom.Rows[i].Visible)
                    {
                        int removeRowID = (int)gvUserRoom["userRoomID", i].Value;
                        var objUserRoom = (from row in dbc.mst_user_rooms
                                           where row.row_id == removeRowID
                                           select row).FirstOrDefault();
                        dbc.mst_user_rooms.DeleteOnSubmit(objUserRoom);
                    }

                }
                dbc.SubmitChanges();

                lbmsgalert.Text = "Save data completed.";
            }
            catch (Exception ex)
            {
                //Program.MessageError(ex.Message);
                lbmsgalert.Text = "Error:" + ex.Message;
            }
        }
        private void SetbuttongSave()
        {
            GBUserAddEdit.Enabled = true;//เปิดให้แก้ไขได้
            btnAddNewUser.Enabled = false;
            btnEditUser.Enabled = false;
            btnSave.Enabled = true;
            gbRoom.Enabled = true; 
        }
        private void Clearbutton()
        {
            GBUserAddEdit.Enabled = false;
            btnAddNewUser.Enabled = true;
            btnEditUser.Enabled = true;
            btnSave.Enabled = false;
            //gvMstRoom.Columns[3].btn
            //gbRoom.Enabled = false;
        }

        private void GridUserSetting_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridUserSetting.SetRuningNumber("colNo");
        }

        private void frmSetupUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dbc.GetChangeSet().Updates.Count > 0 || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveData();
                }
                else
                {
                    dbc.Dispose();
                }
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPasswordConfirm_Leave(null, null);
        }
        private void txtPasswordConfirm_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                lbmsgalert.Text = "Password and Confirm Password not match.";
                //txtPassword.Text = "";
                txtPasswordConfirm.Text = "";
            }
            else
                lbmsgalert.Text = "";
        }


        private void ch_isAdmin_MouseClick(object sender, MouseEventArgs e)
        {
            if (ch_isAdmin.Checked)
            {
                pnlAdminType.Enabled = true;
            }
            else
            {
                pnlAdminType.Enabled = false;
                //RDAdmin_UserAdmin.Checked = true;
                RDAdmin_UserAdmin.Checked = false;
            }
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            if (currentmut != null)
            {
                var data = (ch_isAdmin.Checked == true) ? true : false;
                if (data != currentmut.mut_admin)
                {
                    currentmut.mut_admin = data;
                }
            }
        }
        private void RDAdmin_UserAdmin_MouseClick(object sender, MouseEventArgs e)
        {
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            if (currentmut != null)
            {
                var datach = Program.GetValueRadioTochar(pnlAdminType);
                if (datach != currentmut.mut_admin_type)
                {
                    currentmut.mut_admin_type = datach;
                }
            }
        }
        private void RDuserType_O_MouseClick(object sender, MouseEventArgs e)
        {
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            if (currentmut != null)
            {
                var datach = Program.GetValueRadioTochar(pnlmut_type);
                if (datach != currentmut.mut_type)
                {
                    currentmut.mut_type = datach;
                }
            }
        }
        private void RDGender_M_MouseClick(object sender, MouseEventArgs e)
        {
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            if (currentmut != null)
            {
                var data = Program.GetValueRadioTochar(pnlGender);
                if (data != currentmut.mut_gender)
                {
                    currentmut.mut_gender = data;
                }
            }
        }
        private void ChStatus_MouseClick(object sender, MouseEventArgs e)
        {
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            if (currentmut != null)
            {
                var data = (ChStatus.Checked == true) ? 'A' : 'I';
                if (data != currentmut.mut_status)
                {
                    currentmut.mut_status = data;
                }
            }
        }

        private void rdoExpireDt_CheckedChanged(object sender, EventArgs e)
        {
            txtExpireDate.Enabled = rdoExpireDt.Checked;
        }

        private void ddlMstSite_SelectedIndexChanged(object sender, EventArgs e)
        {

            int site = (int)ddlMstSite.SelectedValue;
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            LoadRoom(site, currentmut.mut_id);
            CheckBox chkHeader = (CheckBox)gvMstRoom.Controls[2];
            chkHeader.Checked = false;


        }

        private void LoadRoom(int siteID, int mutID)
        {
            try
            {
                if (mutID != null)
                {
                    bool status = false;
                    if (!gbRoom.Enabled)
                    {
                        gbRoom.Enabled = true;
                        status = true;
                    }

                    var objRoom = (from mstRoom in ( from roomInStie in dbc.mst_room_hdrs where roomInStie.mhs_id == siteID select roomInStie)
                                   join userHaveRoom in (from userRoom in dbc.mst_user_rooms where userRoom.mut_id == mutID select userRoom) 
                                        on mstRoom.mrm_id equals userHaveRoom.mrm_id into all
                                   from userHaveRoom in all.DefaultIfEmpty()
                                   where userHaveRoom == null
                                   select new
                                   {
                                       roomName = mstRoom.mrm_ename,
                                       mrm_id = mstRoom.mrm_id
                                   }).ToList();

                    List<int> addedRoom = new List<int>();
                    for(int i= 0; i< gvUserRoom.Rows.Count; i++)
                    {
                        if (gvUserRoom.Rows[i].Cells["userRoomID"].Value == null)
                            addedRoom.Add((int)gvUserRoom.Rows[i].Cells["userMrmID"].Value);
                    }

                    gvMstRoom.Rows.Clear();
                    foreach (var data in objRoom)
                    {
                        bool found = false;
                        for (int i = 0; i < addedRoom.Count  && !found; i++ )
                        {
                            if (addedRoom[i] == data.mrm_id)
                            {
                                found = true;
                                addedRoom.RemoveAt(i);
                            }
                        }
                        if(!found)
                            gvMstRoom.Rows.Add(false, data.roomName, data.mrm_id);
                    }
                    if (status)
                        gbRoom.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception ("Error Load room : " + ex.Message.ToString());
            }
            
        }


        private void LoadUserRoom(int mutID)
        {
            try
            {
                if (mutID != 0)
                {
                    bool status = false;
                    if (!gbRoom.Enabled)
                    {
                        gbRoom.Enabled = true;
                        status = true;
                    }
                    var objUserRoom = (from userroom in dbc.mst_user_rooms
                                       join roomhdr in dbc.mst_room_hdrs
                                       on userroom.mrm_id equals roomhdr.mrm_id
                                       join site in dbc.mst_hpc_sites
                                       on roomhdr.mhs_id equals site.mhs_id
                                       where userroom.mut_id == mutID
                                       select new
                                       {
                                           userRoomName = roomhdr.mrm_ename,
                                           userRoomSite = site.mhs_ename,
                                           userRoomDel = "delete",
                                           userRoomID = userroom.row_id,
                                           userMrmID = userroom.mrm_id,
                                           userSiteID = site.mhs_id
                                       }).ToList();

                    gvUserRoom.Rows.Clear();
                    int count = 1;
                    foreach (var data in objUserRoom)
                    {
                        gvUserRoom.Rows.Add(count, data.userRoomName, data.userRoomSite, data.userRoomDel, data.userRoomID, data.userMrmID, userSiteID);
                        count++;
                    }
                    if(status)
                        gbRoom.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error Load user room : " + ex.Message.ToString());
            }

        }

        private void gvUserRoom_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridUserSetting.SetRuningNumber("no");
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            try
            {
                //mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
                //foreach (DataGridViewRow data in gvMstRoom.Rows)
                //{

                //    if ((bool)data.Cells[0].Value == true)
                //    {
                //        mst_user_room newRoom = new mst_user_room();
                //        newRoom.mut_id = currentmut.mut_id;
                //        newRoom.mrm_id = (int)data.Cells[2].Value;
                //        newRoom.mut_username = currentmut.mut_username;
                //        dbc.mst_user_rooms.InsertOnSubmit(newRoom);
                //    }
                //}
                //dbc.SubmitChanges();
                //mstUserTypebindingSource1_CurrentChanged(sender, e);
                //btnEditUser_Click(sender, e);
                //CheckBox chkHeader = (CheckBox)gvMstRoom.Controls[2];
                //chkHeader.Checked = false;

                List<int> listRemove = new List<int>();
                for (int i = 0; i < gvMstRoom.Rows.Count; i++)
                {
                    if ((bool)gvMstRoom.Rows[i].Cells[0].Value == true)
                    {
                        gvUserRoom.Rows.Add(0, gvMstRoom.Rows[i].Cells["roomName"].Value, ddlMstSite.Text, "delete", null, gvMstRoom.Rows[i].Cells["mrm_id"].Value, (int)ddlMstSite.SelectedValue);
                        listRemove.Add(i);
                    }
                }

                for (int i = listRemove.Count; i > 0; i--)
                {
                    gvMstRoom.Rows.RemoveAt(listRemove[i-1]);
                }

                CheckBox chkHeader = (CheckBox)gvMstRoom.Controls[2];
                chkHeader.Checked = false;
                gvUserRoom.SetRuningNumber("no");
                
             }
            catch (Exception ex)
            {
                throw new Exception("Error Load user room : " + ex.Message.ToString());
            }
        }

        private void gvUserRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && btnEditUser.Enabled == false)
            {
                switch (e.ColumnIndex)
                {
                    case 3:
                        if (gvUserRoom[3, e.RowIndex].Value != null)
                        {
                            if (gvUserRoom["userRoomID", e.RowIndex].Value == null)
                            {
                                gvUserRoom.Rows.RemoveAt(e.RowIndex);
                                mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
                                LoadRoom((int)ddlMstSite.SelectedValue, currentmut.mut_id);
                                CheckBox chkHeader = (CheckBox)gvMstRoom.Controls[2];
                                chkHeader.Checked = false;
                                gvUserRoom.SetRuningNumber("no");
                            }
                            else
                            {
                                gvUserRoom.Rows[e.RowIndex].Visible = false;
                                gvUserRoom.SetRuningNumber("no");
                                //int removeRowID = (int)gvUserRoom["userRoomID", e.RowIndex].Value;
                                //var objUserRoom = (from row in dbc.mst_user_rooms
                                //                   where row.row_id == removeRowID
                                //                   select row).FirstOrDefault();
                                //dbc.mst_user_rooms.DeleteOnSubmit(objUserRoom);
                                //dbc.SubmitChanges();
                                //mstUserTypebindingSource1_CurrentChanged(sender, e);
                                //btnEditUser_Click(sender, e);
                            }
                        }
                        break;
                }
            }
        }

        private void gvMstRoom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_mstHeaderClick)
            {
                _mstHeaderClick = false;
            }
            else
            {
                if (gvMstRoom.Rows.Count > 0)
                {
                    bool found = false;
                    foreach (DataGridViewRow data in gvMstRoom.Rows)
                    {
                        if ((bool)data.Cells[0].Value == false)
                        {
                            found = true;
                        }
                    }
                    
                    CheckBox chkHeader = (CheckBox)gvMstRoom.Controls[2];
                    if (found)
                    {
                        if (chkHeader.Checked)
                        {
                            _mstContentClick = true;
                            chkHeader.Checked = false;
                        }
                    }
                    else
                    {
                        _mstContentClick = true;
                        chkHeader.Checked = true;
                    }
                }
            }
        }

        private void chkOutCheckup_MouseClick(object sender, MouseEventArgs e)
        {
            mst_user_type currentmut = (mst_user_type)mstUserTypebindingSource1.Current;
            if (currentmut != null)
            {
                var data = (chkOutCheckup.Checked == true) ? true : false;
                if (data != currentmut.mut_out_checkup)
                {
                    currentmut.mut_out_checkup = data;
                }
            }
        }



        
    }

 }