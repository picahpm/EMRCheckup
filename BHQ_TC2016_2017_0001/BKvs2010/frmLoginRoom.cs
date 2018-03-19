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
    public partial class frmLoginRoom : Form
    {
        public frmLoginRoom()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        
        public string GetmrmCode { get; set; }
        public string GetTypeQueue { get; set; }
        private void frmLoginRoom_Load(object sender, EventArgs e)
        {
            //&& (t1.mrd_rm_status=='E' || (t1.mrd_rm_status!='E' && countRoomLogin.Contains(t1.mrd_id)))
            var objroomlist = (from t1 in dbc.mst_room_dtls
                               where t1.mst_room_hdr.mst_user_rooms.Where(x => x.mut_username == Program.CurrentUser.mut_username 
                                    && x.mst_room_hdr.mrm_code==GetmrmCode).Count() > 0
                                    && t1.mst_room_hdr.mhs_id==Program.CurrentSite.mhs_id
                                    && t1.mrd_status == 'A'
                               orderby t1.mrd_dummy_room,t1.mrd_id
                               select new Roomselect { EName = (t1.mrd_room_no != null) ? " (" + t1.mrd_room_no + ") " + t1.mrd_ename : t1.mrd_ename, ID = t1.mrd_id }).ToList();
            var dumyroom = (from t1 in dbc.mst_room_dtls 
                            where t1.mst_room_hdr.mrm_code == GetmrmCode 
                            && t1.mrd_dummy_room=='Y' 
                            && t1.mrd_status == 'A'
                            select t1);
            if (dumyroom.Count() > 0 && objroomlist.Where(x => x.ID==dumyroom.FirstOrDefault().mrd_id).Count() == 0)
            {
                mst_room_dtl dmroom = dumyroom.FirstOrDefault();
                Roomselect newroom = new Roomselect();
                newroom.EName = dmroom.mrd_ename;
                newroom.ID = dmroom.mrd_id;
                objroomlist.Add(newroom);
            }
            CBRoom.DataSource = objroomlist;
            CBRoom.DisplayMember = "EName";
            CBRoom.ValueMember = "ID";
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (RDOnQueue.Checked)
            {
                GetTypeQueue = "N";
                if (CBRoom.SelectedValue != null)
                {
                    //OldLogin();
                    NewLogin();
                }
                else
                {
                    //this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
            }
            else
            {
                GetTypeQueue = "O";
                log_user_login newlog = new log_user_login();
                newlog.mut_id = Program.CurrentUser.mut_id;
                newlog.mhs_id = Program.CurrentSite.mhs_id;
                newlog.lug_ip_address = Program.GetLocalIP();
                newlog.lug_start_date = Program.GetServerDateTime();
                dbc.log_user_logins.InsertOnSubmit(newlog);
                dbc.SubmitChanges();
                Class.ClsManageUserLogin.current_log = newlog;
                Program.CurrentLogin = newlog;
                mst_user_type CurrentUserlogin = (from t1 in dbc.mst_user_types where t1.mut_id == Program.CurrentUser.mut_id select t1).FirstOrDefault();
                CurrentUserlogin.mut_login_status = '1';
                dbc.SubmitChanges();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            //if (CBRoom.SelectedValue != null)
            //{
            //    //OldLogin();
            //    NewLogin();
            //}
            //else
            //{
            //    //this.DialogResult = System.Windows.Forms.DialogResult.No;
            //}
        }
        private void OldLogin()
         {
            if (Program.Login(Program.CurrentUser.mut_username, Program.CurrentSite.mhs_id, Convert.ToInt32(CBRoom.SelectedValue)))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
         }
        private void NewLogin()
         {
            Program.IsDummy = false;
            var objroom = (from t1 in dbc.mst_room_dtls 
                        where t1.mst_room_hdr.mrm_code == GetmrmCode
                        && t1.mst_room_hdr.mhs_id==Program.CurrentSite.mhs_id
                        && t1.mrd_id == Convert.ToInt32(CBRoom.SelectedValue)
                        && t1.mrd_dummy_room == 'Y' 
                        select t1).FirstOrDefault();

            if (CBRoom.SelectedValue.ToString() != "0" && objroom==null)
            {
                if (Program.Login(Program.CurrentUser.mut_username, Program.CurrentSite.mhs_id, Convert.ToInt32(CBRoom.SelectedValue)))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    //this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
            }
            else if (objroom != null)
            {//Dummy Room
                Program.IsDummy = true;
                Program.CurrentRoom = objroom;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    
        public void SetVisiblePQueue(){
            PQueue.Visible = true;
            label4.Top = 47;
            CBRoom.Top = 44;
        }

        private void frmLoginRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }// end public

    class Roomselect
    {
        public  int ID { get; set; }
        public string EName { get; set; }
    }
}
