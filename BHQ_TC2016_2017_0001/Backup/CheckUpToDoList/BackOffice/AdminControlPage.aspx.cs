using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBToDoList;

namespace CheckUpToDoList.BackOffice
{
    public partial class AdminControlPage : System.Web.UI.Page
    {
        private static List<Viewuserlogin> _UserList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IsLoginBackOffice();

                _UserList = new List<Viewuserlogin>();
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    //แสดง file ที่มีการ upload ไปแล้ว
                    _UserList = (from t1 in dbc.mst_user_logins
                                 where t1.mul_user_login!="Admin"
                                 select new Viewuserlogin
                                 {
                                     Userlogin = t1.mul_user_login,
                                     FirstName = t1.mul_fname,
                                     Lastname = t1.mul_lname,
                                     Market = (t1.mut_id == 1) ? true : false,
                                     HPC = (t1.mut_id == 2) ? true : false,
                                     Collection = (t1.mut_id == 3) ? true : false,
                                     ContactCenter = (t1.mut_id == 4) ? true : false,
                                     IsEdit=t1.mul_permit.ToString().Trim(),
                                     IsActive = (t1.mut_status != null && t1.mut_status == 'A') ? true : false,
                                      Status="O"
                                 }).ToList();
                    showGridUser();
                }
            }
        }
        private void IsLoginBackOffice()
        {
            if (Constant.CurrentUserLogin != "")
            {//ถ้ายังไม่ได้ login ให้ ไปหน้า login
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    var objpolicy = (from t1 in dbc.mst_user_logins
                                     where t1.mul_user_login == Constant.CurrentUserLogin
                                     select t1).Count();
                    if (objpolicy ==1 && Constant.CurrentUserLogin=="Admin")
                    {
                        return;
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("BOLogin.aspx");
                    }
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect("BOLogin.aspx");
            }
        }

        protected void chkMarket_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender,"1","R");
        }
        protected void chkMarketWrite_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender,"1","W");
        }

        protected void chkHPC_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender, "2", "R");
        }
        protected void chkHPCWrite_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender, "2", "W");
        }

        protected void chkCollection_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender, "3","R");
        }
        protected void chkCollectionWrite_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender, "3","W");
        }

        protected void chkContactCenter_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender, "4", "R");
        }
        protected void chkContactCenterWrite_CheckedChanged(object sender, EventArgs e)
        {
            SetDepartmant(sender, "4", "W");
        }

        private void SetDepartmant(object sender, string StrType,string IsReadWrite)
        {
            RadioButton itemRD = (RadioButton)sender;
            if (itemRD.Checked == true)
            {
                HiddenField IdHiddenField = (HiddenField)itemRD.Parent.FindControl("mpf_idHiddenField_" + StrType);

                string  RowID = Convert1.ToString(IdHiddenField.Value);

                var currentselect = _UserList.Where(x => x.Userlogin == RowID).FirstOrDefault();
                currentselect.Market = false;
                currentselect.HPC = false;
                currentselect.Collection = false;
                currentselect.ContactCenter = false;
                currentselect.IsEdit = IsReadWrite;
                currentselect.Status = "E";
                switch (StrType){
                    case "1": currentselect.Market = true; break;
                    case "2": currentselect.HPC = true; break;
                    case "3": currentselect.Collection = true; break;
                    case "4": currentselect.ContactCenter = true; break;
                }
            }
        }
        protected void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox itemRD = (CheckBox)sender;
            HiddenField IdHiddenField = (HiddenField)itemRD.Parent.FindControl("mpf_idHiddenField_Active" );
            string RowID = Convert1.ToString(IdHiddenField.Value);
            var currentselect = _UserList.Where(x => x.Userlogin == RowID).FirstOrDefault();
            currentselect.IsActive = itemRD.Checked;
            currentselect.Status = "E";
        }
        //Add Userlogin
        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            lbmsgAlertAdd.Text = "";
            if (txtUserLogin.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "")
            {
                lbmsgAlertAdd.Text = "Please input Username, FirstName or LastName.";
                return;
            }

            string userlo = txtUserLogin.Text.Trim();
            string fname = txtFirstName.Text.Trim();
            string lname = txtLastName.Text.Trim();
            if (btnAddnew.Text == "Add")
            {
                //Check UserLogin ซ้ำในtemp
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    var objuser = (from t1 in dbc.mst_user_logins
                                   where t1.mul_user_login.ToLower() == userlo.ToLower()
                                   select t1).Count();
                    if (objuser > 0)
                    {
                        lbmsgAlertAdd.Text = "User login had in system. please change user login.";
                        txtUserLogin.Text = "";
                        return;
                    }
                }

                Viewuserlogin newitem = new Viewuserlogin();
                newitem.Userlogin = txtUserLogin.Text;
                newitem.FirstName = txtFirstName.Text;
                newitem.Lastname = txtLastName.Text;
                newitem.IsActive = chActive.Checked;
                newitem.Market = false;
                newitem.HPC = false;
                newitem.Collection = false;
                newitem.ContactCenter = false;
                newitem.Status = "N";
                if (RDDepartment.SelectedValue != null)
                {
                    var departmentvalue = RDDepartment.SelectedValue;
                    if (departmentvalue == "1")
                        newitem.Market = true;

                    if (departmentvalue == "2")
                        newitem.HPC = true;

                    if (departmentvalue == "3")
                        newitem.Collection = true;

                    if (departmentvalue == "4")
                        newitem.ContactCenter = true;
                }
                newitem.IsEdit= RDPolicyEdit.SelectedValue;
                _UserList.Add(newitem);
            }
                
            else if (btnAddnew.Text == "SAVE")
            {
                var objcurrent = (from t1 in _UserList
                                  where t1.Userlogin.ToLower() == txtUserLogin.Text.ToLower()
                                  select t1).FirstOrDefault();

                if (objcurrent != null)
                {
                    objcurrent.FirstName = txtFirstName.Text;
                    objcurrent.Lastname = txtLastName.Text;
                    objcurrent.IsActive = chActive.Checked;
                    objcurrent.Market = false;
                    objcurrent.HPC = false;
                    objcurrent.Collection = false;
                    objcurrent.ContactCenter = false;
                    objcurrent.IsEdit = RDPolicyEdit.SelectedValue;
                    string st = objcurrent.Status;
                    if (st == "N")
                    { //แก้รายการที่เพิ่ง insert ไป แต่ ยังไม่ได้ Save
                        objcurrent.Status = "N";
                    }
                    else
                    {
                        objcurrent.Status = "E";
                    }
                    if (RDDepartment.SelectedValue != null)
                    {
                        var departmentvalue = RDDepartment.SelectedValue;
                        if (departmentvalue == "1")
                            objcurrent.Market = true;

                        if (departmentvalue == "2")
                            objcurrent.HPC = true;

                        if (departmentvalue == "3")
                            objcurrent.Collection = true;

                        if (departmentvalue == "4")
                            objcurrent.ContactCenter = true;
                    }
                    txtUserLogin.Enabled = true;
                    btnAddnew.Text = "Add";
                }
             
            }
            showGridUser();

            //clear text
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtUserLogin.Text = "";
            RDDepartment.ClearSelection();
            RDPolicyEdit.ClearSelection();
            chActive.Checked = false;
        }

        //save final
        protected void btnSave_Click(object sender, EventArgs e)
        {
            lbmsgAlertAdd.Text = "";
            DateTime dtnow = funcCls.GetServerDateTime();
            for (int i = _UserList.Count - 1; i >= 0; i--)
            {
                if (_UserList[i].Status == "N")
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        mst_user_login newitem = new mst_user_login();
                        newitem.mul_user_login = _UserList[i].Userlogin;
                        newitem.mul_fname = _UserList[i].FirstName;
                        newitem.mul_lname = _UserList[i].Lastname;
                        if (_UserList[i].Market == true)
                            newitem.mut_id = 1;

                        if (_UserList[i].HPC == true)
                            newitem.mut_id = 2;

                        if (_UserList[i].Collection == true)
                            newitem.mut_id = 3;

                        if (_UserList[i].ContactCenter == true)
                            newitem.mut_id = 4;

                        newitem.mul_permit = Convert1.ToChar(_UserList[i].IsEdit);
                        newitem.mut_status = (_UserList[i].IsActive == true) ? 'A' : 'I';
                        newitem.mul_create_by = Constant.CurrentUserLogin;
                        newitem.mul_create_date = dtnow;
                        newitem.mul_user_login_update = Constant.CurrentUserLogin;
                        newitem.mul_update_date = dtnow;
                        dbc.mst_user_logins.InsertOnSubmit(newitem);
                        dbc.SubmitChanges();
                        _UserList[i].Status = "O";
                    }
                }
                else if (_UserList[i].Status == "D")
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        var currentUser = (from t1 in dbc.mst_user_logins
                                           where t1.mul_user_login == _UserList[i].Userlogin
                                           select t1).FirstOrDefault();
                        if (currentUser != null)
                        {
                            dbc.mst_user_logins.DeleteOnSubmit(currentUser);
                        }
                        dbc.SubmitChanges();
                    }
                }
                else
                {
                    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                    {
                        var currentUser = (from t1 in dbc.mst_user_logins
                                           where t1.mul_user_login == _UserList[i].Userlogin
                                           select t1).FirstOrDefault();
                        if (currentUser != null)
                        {
                            currentUser.mul_permit = Convert1.ToChar(_UserList[i].IsEdit);
                            currentUser.mul_user_login = _UserList[i].Userlogin;
                            currentUser.mul_fname = _UserList[i].FirstName;
                            currentUser.mul_lname = _UserList[i].Lastname;
                            if (_UserList[i].Market == true)
                                currentUser.mut_id = 1;

                            if (_UserList[i].HPC == true)
                                currentUser.mut_id = 2;

                            if (_UserList[i].Collection == true)
                                currentUser.mut_id = 3;

                            if (_UserList[i].ContactCenter == true)
                                currentUser.mut_id = 4;

                            currentUser.mut_status = (_UserList[i].IsActive == true) ? 'A' : 'I';
                            currentUser.mul_user_login_update = Constant.CurrentUserLogin;
                            currentUser.mul_update_date = dtnow;
                            dbc.SubmitChanges();
                        }
                    }
                }
            }//end for each

            lbmsgAlert.Text = "Save data completed.";
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string userlog = Convert.ToString(e.CommandArgument);
            switch (e.CommandName)
            {
                case "Edituser":
                    if (userlog!="")
                    {
                        var objfile = (from f in _UserList where f.Userlogin.Equals(userlog) select f).FirstOrDefault();
                        if (objfile != null)
                        {
                            btnAddnew.Text = "SAVE";
                            txtFirstName.Text =objfile.FirstName;
                            txtLastName.Text = objfile.Lastname;
                            txtUserLogin.Text = objfile.Userlogin;
                            txtUserLogin.Enabled = false;
                            var mut_id=0;
                            if (objfile.Market == true)
                            mut_id = 1;

                            if (objfile.HPC == true)
                                mut_id = 2;

                            if (objfile.Collection == true)
                                mut_id = 3;

                            if (objfile.ContactCenter == true)
                                mut_id = 4;

                            RDPolicyEdit.SelectedValue = objfile.IsEdit.Trim();
                            RDDepartment.SelectedValue = mut_id.ToString();
                            chActive.Checked = objfile.IsActive;
                            showGridUser();
                        }
                    }
                    break;
                case "DeleteUser":
                    foreach (Viewuserlogin file in _UserList)
                    {
                        if (file.Userlogin.Equals(userlog))
                        {
                            var objfile = (from f in _UserList where f.Userlogin.Equals(userlog) select f).FirstOrDefault();
                            if (objfile != null)
                            {
                                if (objfile!=null)
                                {
                                    objfile.Status = "D";
                                }
                                showGridUser();
                            }
                            break;
                        }
                    }
                    showGridUser();
                    break;
            }
        }

        //Update Delete button in gridview
        private void showGridUser()
        {
            GridView1.DataSource = _UserList.Where(x=>x.Status=="N" || x.Status=="O" || x.Status=="E");
            GridView1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = txtSearch.Text.Trim();
            txtUserLogin.Enabled = true;
            string strsearch = txtSearch.Text.ToLower();
            //_UserList = new List<Viewuserlogin>();
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                //แสดง file ที่มีการ upload ไปแล้ว
                _UserList = (from t1 in dbc.mst_user_logins
                             where t1.mul_user_login != "Admin"
                             && (t1.mul_user_login.ToLower().Contains(strsearch)
                                || t1.mul_fname.ToLower().Contains(strsearch) 
                                || t1.mul_lname.ToLower().Contains(strsearch)
                                || (t1.mul_fname + " " + t1.mul_lname).ToLower().Contains(strsearch)
                             )
                             select new Viewuserlogin
                             {
                                 Userlogin = t1.mul_user_login,
                                 FirstName = t1.mul_fname,
                                 Lastname = t1.mul_lname,
                                 FullName=Constant.MargeString(t1.mul_fname,t1.mul_lname," "),
                                 Market = (t1.mut_id == 1) ? true : false,
                                 HPC = (t1.mut_id == 2) ? true : false,
                                 Collection = (t1.mut_id == 3) ? true : false,
                                 ContactCenter = (t1.mut_id == 4) ? true : false,
                                 IsEdit = t1.mul_permit.ToString().Trim(),
                                 IsActive = (t1.mut_status != null && t1.mut_status == 'A') ? true : false,
                                 Status = "O"
                             }).ToList();
                //if (strsearch != null)
                //{
                //    _UserList = _UserList.Where(x => x.Userlogin.ToLower().Contains(strsearch) 
                //                || x.FirstName.ToLower().Contains(strsearch) 
                //                || x.Lastname.ToLower().Contains(strsearch)
                //                || x.FullName.ToLower().Contains(strsearch)).ToList();
                //}
                showGridUser();
            }
            lbmsgAlert.Text = "";
            lbmsgAlertAdd.Text = "";
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            lbAlert.Text = "";
            txtUserLogin.Text = txtUserLogin.Text.Trim();
            if (txtUserLogin.Text.Length > 0)
            {
                Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
                DataTable dt = ws.getUser(txtUserLogin.Text);
                if (dt.Rows.Count > 0)
                {
                    txtUserLogin.Text = dt.Rows[0]["SSUSR_Initials"].ToString();
                    string name = dt.Rows[0]["SSUSR_Name"].ToString();
                    if (name.Length > 0)
                    {
                        var sName = name.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                        txtFirstName.Text = sName.FirstOrDefault();
                        txtLastName.Text = sName.LastOrDefault();

                        //txtFirstName.Text = name.Substring(0, name.IndexOf(" ")).Trim();
                        //txtLastName.Text = name.Substring(name.IndexOf(" ")).Trim();
                    }
                    else
                    {
                        lbAlert.Text = "Unknown User's Name.";
                        txtFirstName.Text = "";
                        txtLastName.Text = "";
                    }
                    chActive.Checked = true;
                }
                else
                {
                    lbAlert.Text = "User Not Found.";
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    chActive.Checked = false;
                }
            }
            else
            {
                lbAlert.Text = "Please Insert Username.";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                chActive.Checked = false;
            }
        }
    }

     class Viewuserlogin{
        public string Userlogin{get;set;}
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string FullName { get; set; }
        public bool Market { get; set; }
        public bool HPC { get; set; }
        public bool Collection { get; set; }
        public bool ContactCenter { get; set; }
        public bool IsActive { get; set; }
        public string IsEdit { get; set; }
        public string Status { get; set; }
    }
}