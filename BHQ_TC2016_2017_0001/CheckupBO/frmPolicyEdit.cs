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
    public partial class frmPolicyEdit : Form
    {
        public frmPolicyEdit()
        {
            InitializeComponent();
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void frmPolicyEdit_Load(object sender, EventArgs e)
        {
            //UserData
            var objusertype = (from t1 in dbc.mst_user_types
                               where t1.mut_status == 'A'
                               select new
                               {
                                   t1.mut_user_id,
                                   t1.mut_username,
                                   t1.mut_fullname,
                                   mut_type=(t1.mut_type=='D')?"Doctor":"Other"
                               }).ToList();
            this.GridUsertype.DataSource = objusertype.Select((item,index)=>new{
                no=index+1,
                UserID=item.mut_user_id,
                UserName=item.mut_username,
                FullName= item.mut_fullname,
                UserType= item.mut_type
            }).ToList();

            //Permision 
            var objRoom = (from t1 in dbc.mst_room_hdrs
                           select new
                           {
                               t1.mrm_id,
                               t1.mrm_ename,
                               t1.mrm_effective_date,
                               t1.mrm_expire_date,
                               t1.mst_hpc_site.mhs_ename
                           }).ToList();
           
            dataGridView1.DataSource = objRoom;
            dataGridView1.ReadOnly = true;
            GridUsertype.ClearSelection();
            dataGridView1.Columns["Column6"].ReadOnly = true;
            dataGridView1.Columns["Column7"].ReadOnly = true;
            dataGridView1.Columns["Column8"].ReadOnly = true;
            dataGridView1.Columns["Column9"].ReadOnly = true;
        }

        private void GridUsertype_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string UserName = GridUsertype.CurrentRow.Cells[2].Value.ToString();
                //// ส่งค่าให้กับสิทธิ User

                
                var listUserroom = (from t1 in dbc.mst_user_rooms
                                    where t1.mut_username == UserName
                                    select t1).ToList();
                
                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    int gridmrmid = Utility.GetInteger(dataGridView1[1, i].Value);
                    int CheckCount = listUserroom.Where(x => x.mrm_id == gridmrmid).Count();
                    if (CheckCount>0)
                    {
                        dataGridView1[0, i].Value = true;
                    }
                    else
                    {
                        dataGridView1[0, i].Value = false;
                    }

                }
                dataGridView1.ReadOnly = false;
            }
            catch (Exception)
            {
                MessageBox.Show("ทำงานผิดพลาด");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lbAlertMessage.Text = "";
            if (GridUsertype.CurrentRow != null)
            {
                string UserID = Convert1.ToString(GridUsertype.CurrentRow.Cells[1].Value);
                string UserName = GridUsertype.CurrentRow.Cells[2].Value.ToString();
                var listUserroom = (from t1 in dbc.mst_user_rooms
                                    where t1.mut_username == UserName
                                    select t1);
                dbc.mst_user_rooms.DeleteAllOnSubmit(listUserroom);

                for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
                {
                    if (Convert1.ToBoolean(dataGridView1[0, i].Value) == true)
                    {
                        int mrmid = Utility.GetInteger(dataGridView1[1, i].Value);

                        mst_user_type usermut = (from t1 in dbc.mst_user_types where t1.mut_username == UserName select t1).FirstOrDefault();
                        mst_user_room newitem = new mst_user_room();
                        newitem.mst_user_type = usermut;
                        newitem.mut_id = usermut.mut_id;
                        newitem.mut_username = UserName;
                        newitem.mrm_id = mrmid;
                        dbc.mst_user_rooms.InsertOnSubmit(newitem);
                    }
                }
                dbc.SubmitChanges();
                lbAlertMessage.Text = "Save data completed.";
            }
            else
            {
                lbAlertMessage.Text = "Please select user for edit.";
            }
       }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.ReadOnly==false)
            {
                bool ischeck = Convert1.ToBoolean(dataGridView1[0, e.RowIndex].Value);
                if (ischeck==false)
                {
                    dataGridView1[0, e.RowIndex].Value = true;
                }
                else
                {
                    dataGridView1[0, e.RowIndex].Value = false;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            //UserData
            var objusertype = (from t1 in dbc.mst_user_types
                               where t1.mut_status == 'A'
                               && (t1.mut_fullname.Contains(txtSearch.Text.Trim())
                               || t1.mut_e_fname.Contains(txtSearch.Text.Trim())
                               || t1.mut_e_lname.Contains(txtSearch.Text.Trim())
                               || t1.mut_username.Contains(txtSearch.Text.Trim()))
                               select new
                               {
                                   t1.mut_user_id,
                                   t1.mut_username,
                                   t1.mut_fullname,
                                   mut_type = (t1.mut_type == 'D') ? "Doctor" : "Other"
                               }).ToList();
            this.GridUsertype.DataSource = objusertype.Select((item, index) => new
            {
                no = index + 1,
                UserID = item.mut_user_id,
                UserName = item.mut_username,
                FullName = item.mut_fullname,
                UserType = item.mut_type
            }).ToList();
        }

    }
}
