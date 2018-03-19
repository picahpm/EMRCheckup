using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Drawing;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class frmMasterCompany : System.Web.UI.Page
    {
       
        public string emode {get ; set; }
        bool isSuccess = false;
        private static List< AddContact> _objaddcontact = null;
        private static List<Delcontact> _objdelcontact = null;
        InhToDoListDataContext dbc = new InhToDoListDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HiddenFieldRowContact.Value = "-1";
                HiddenField_mcoid.Value = "0";
                _objaddcontact = new List<AddContact>();
                _objdelcontact = new List<Delcontact>();
                LoadContact_Type();
                LoadType();

                TimerProgressWait.Enabled = false;

            }
        }

        private void LoadContact_Type()
        {
            var objctype = (from ctype in dbc.mst_contact_types select ctype).ToList();
            ddlcontact_type.DataSource = objctype;
            ddlcontact_type.DataTextField = "mct_name";
            ddlcontact_type.DataValueField = "mct_id";
            ddlcontact_type.DataBind();
        }

        private void LoadType()
        {
            var objctype = (from ctype in dbc.mst_types select ctype).ToList();
            ddltype.DataSource = objctype;
            ddltype.DataTextField = "mst_tname";
            ddltype.DataValueField = "mst_id";
            ddltype.DataBind();
        }

        private void LoadDataCompany()
        {
            var objcomp = (from comp in dbc.mst_companies where comp.mco_tname == txtsearch.Text select comp).FirstOrDefault();
            if (objcomp != null)
            {
                HiddenField_mcoid.Value = objcomp.mco_id.ToString();
                txtcompany_code.Text = objcomp.mco_code;
                txtname_th.Text = objcomp.mco_tname;
                txtname_en.Text = objcomp.mco_ename;
                txtlegal.Text = objcomp.mco_legal;
                rdona.Checked =  objcomp.mco_type == "N/A" ? true : false;
                rdojms.Checked = objcomp.mco_type == "JMS" ? true : false;
                txtaddress.Text = objcomp.mco_address;
                txtsubdistrict.Text = objcomp.mco_tambon;
                txtdistrict.Text = objcomp.mco_district;
                txtpostcode.Text = objcomp.mco_postcode;
                txtprovince.Text = objcomp.mco_province;
                txttelephone.Text = objcomp.mco_tel;
                txtfax.Text = objcomp.mco_fax;
                txtemail.Text = objcomp.mco_email;
                rdoactive.Checked = objcomp.mco_status == 'A' ? true : false;
                rdoinactive.Checked = objcomp.mco_status == 'I' ? true : false;

                if (objcomp.mco_status == 'I') { btnsavedetail.Visible = false; }
                if (objcomp.mco_status == 'A') { btnsavedetail.Visible = true; }

                this.LoadDataContact(objcomp.mco_id);
            }
            else
            {
                ClearContent();
            }
        }
        private void ClearContent()
        {
            txtcompany_code.Text = String.Empty;
            txtname_th.Text = String.Empty;
            txtname_en.Text = String.Empty;
            txtlegal.Text = String.Empty;
            txtaddress.Text = String.Empty;
            txtsubdistrict.Text = String.Empty;
            txtdistrict.Text = String.Empty;
            txtpostcode.Text = String.Empty;
            txtprovince.Text = String.Empty;
            txttelephone.Text = String.Empty;
            txtfax.Text = String.Empty;
            txtemail.Text = String.Empty;
            txtsearch.Text = String.Empty;
            //lblStatus.Text = "";
            //lblStatus.Visible = false;
            PanelContent.Enabled = false;
            btnedit.Enabled = false;
            _objdelcontact = null;
            _objaddcontact = null;
            _objaddcontact = new List<AddContact>();
            _objdelcontact = new List<Delcontact>();
            RepeaterContact.DataSource = _objaddcontact;
            RepeaterContact.DataBind();
        }

        
        protected void btnedit_Click(object sender, EventArgs e)
        {
            HiddenFieldStatus.Value = "Edit";
            PanelContent.Enabled = true;
        }

        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            ClearContent();
          
            lblStatus.Visible = false;
            HiddenFieldStatus.Value = "Add";
            PanelContent.Enabled = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblStatus.Visible = false;
            if (txtsearch.Text == "") { lblStatus.Text = "กรุณากรอกชื่อบริษัทที่ต้องการค้นหา."; lblStatus.Visible = true; return; }
            btnedit.Enabled = true;
            PanelContent.Enabled = false;
            LoadDataCompany();
        }

        private void LoadDataContact(int mco_id)
        {
            _objaddcontact = new List<AddContact>();

            var objcontact = (from contact in dbc.mst_contact_persons where contact.mco_id.Equals(mco_id) select contact).ToList();
                foreach(var data in objcontact)
                {
                    _objaddcontact.Add(new AddContact() { contact_type_id = (int)data.mct_id,type_id = (int)data.mst_id, 
                        type_name = LoadType((int)data.mst_id),
                        contact_type = LoadContactType((int)data.mct_id), contact_id = data.mcp_id, contact_name = data.mcp_name, 
                        contact_tel = data.mcp_tel, contact_email = data.mcp_email, contact_fax = data.mcp_fax });
                }

                RepeaterContact.DataSource = _objaddcontact;
                RepeaterContact.DataBind();
          
        }

        private string LoadType(int mst_id)
        {
            return (from data in dbc.mst_types where data.mst_id.Equals(mst_id) select data.mst_tname).FirstOrDefault();
        }


        private string LoadContactType(int mct_id) 
        {
            return (from data in dbc.mst_contact_types where data.mct_id.Equals(mct_id) select data.mct_name).FirstOrDefault();
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            this.saveType('S');
           
        }

        private void saveType(char strType)
        {



            if (txtcompany_code.Text == String.Empty)
            {
                lblStatus.Text = "กรุณากรอกรหัสบริษัท.";
                lblStatus.Visible = true;
                lblStatus.Focus();
                return;
            }

            if (txtname_th.Text == String.Empty || txtname_en.Text == String.Empty)
            {
                lblStatus.Text = "กรุณากรอกชื่อบริษัท(th/en).";
                lblStatus.Visible = true;
                lblStatus.Focus();
                return;
            }

            if (txtaddress.Text.Length > 199)
            {
                lblStatus.Text = "Address เกิน 200 ตัวอักษร.";
                lblStatus.Visible = true;
                lblStatus.Focus();
                return;
            }

            var objcomp = (from comp in dbc.mst_companies where comp.mco_id == Convert.ToInt16(HiddenField_mcoid.Value) select comp).FirstOrDefault();
            if (HiddenFieldStatus.Value == "Add") 
            {

                var objcheck = (from comp in dbc.mst_companies select comp).ToList();
                
                if (objcheck != null)
                {
                   int count_comp = objcheck.Where(x => x.mco_tname == txtname_th.Text || x.mco_ename == txtname_en.Text).Count();
                   if (count_comp > 0)
                   {

                            lblStatus.Focus();
                            lblStatus.Text = "ชื่อนี้มีการใช้งานแล้ว.";
                            lblStatus.Visible = true;
                            return;
                   }

                    int count_code = objcheck.Where(x => x.mco_code == txtcompany_code.Text).Count();
                    if (count_code > 0)
                   {
                           lblStatus.Focus();
                           lblStatus.Text = "รหัสนี้มีการใช้งานแล้ว.";
                           lblStatus.Visible = true;
                           return;
                   }
                }
            }
            if (objcomp != null)
            {
                    objcomp.mco_code = txtcompany_code.Text;
                    objcomp.mco_tname = txtname_th.Text;
                    objcomp.mco_ename = txtname_en.Text;
                    objcomp.mco_legal = txtlegal.Text;
                    objcomp.mco_type = rdona.Checked == true ? rdona.Text : rdojms.Text;
                    objcomp.mco_address = txtaddress.Text;
                    objcomp.mco_district = txtdistrict.Text;
                    objcomp.mco_tambon = txtsubdistrict.Text;
                    objcomp.mco_province = txtprovince.Text;
                    objcomp.mco_postcode = txtpostcode.Text;
                    objcomp.mco_tel = txttelephone.Text;
                    objcomp.mco_fax = txtfax.Text;
                    objcomp.mco_email = txtemail.Text;
                    objcomp.mco_update_date = funcCls.GetServerDateTime();
                    objcomp.mco_status = rdoactive.Checked == true ? 'A' : 'I';
                    isSuccess = true;
            }
            else
            {
                //Add New Transaction
                mst_company objinscomp = new mst_company {
                    mco_code = txtcompany_code.Text,
                    mco_tname = txtname_th.Text,
                    mco_ename = txtname_en.Text,
                    mco_legal = txtlegal.Text,
                    mco_type = rdona.Checked == true ? rdona.Text : rdojms.Text,
                    mco_address = txtaddress.Text,
                    mco_district = txtdistrict.Text,
                    mco_tambon = txtsubdistrict.Text,
                    mco_province = txtprovince.Text,
                    mco_postcode = txtpostcode.Text,
                    mco_tel = txttelephone.Text,
                    mco_fax = txtfax.Text,
                    mco_email = txtemail.Text,
                    mco_create_by = Constant.CurrentUserLogin,
                    mco_create_date = funcCls.GetServerDateTime(),
                    mco_status = rdoactive.Checked == true ? 'A' : 'I'
                };
                dbc.mst_companies.InsertOnSubmit(objinscomp);
                objcomp = objinscomp;
                isSuccess = true;
            }

            if (isSuccess == true)
            {
                //Save Company Success give Select tcd_id 
               
                    foreach (var deldata in _objdelcontact)
                    {
                        var objdelmcp = (from data in dbc.mst_contact_persons where data.mco_id.Equals(HiddenField_mcoid.Value) && data.mcp_id.Equals(deldata.contact_id) select data).FirstOrDefault();
                        if (objdelmcp != null)
                        {
                            dbc.mst_contact_persons.DeleteOnSubmit(objdelmcp);
                        }
                    }

                    foreach (var objdata in _objaddcontact)
                        {
                            var objmcp = (from data in dbc.mst_contact_persons where data.mco_id.Equals(HiddenField_mcoid.Value) && data.mcp_id.Equals(objdata.contact_id) select data).FirstOrDefault();

                            if (objmcp != null)
                            {
                                objmcp.mco_id = objcomp.mco_id;
                                objmcp.mco_code = txtcompany_code.Text;
                                objmcp.mcp_name = objdata.contact_name;
                                objmcp.mcp_tel = objdata.contact_tel;
                                objmcp.mcp_fax = objdata.contact_fax;
                                objmcp.mcp_email = objdata.contact_email;
                                objmcp.mct_id = objdata.contact_type_id;
                                objmcp.mst_id = objdata.type_id;
                                objmcp.mcp_status = 'A';
                                objmcp.mcp_update_date = funcCls.GetServerDateTime();
                                objmcp.mul_user_login = Constant.CurrentUserLogin;
                            }
                            else
                            {
                                mst_contact_person objins = new mst_contact_person 
                                {
                                    mco_id = objcomp.mco_id,
                                    mco_code = txtcompany_code.Text,
                                    mcp_name = objdata.contact_name,
                                    mcp_tel = objdata.contact_tel,
                                    mcp_fax = objdata.contact_fax,
                                    mcp_email = objdata.contact_email,
                                    mct_id = objdata.contact_type_id,
                                    mst_id = objdata.type_id,
                                    mcp_status = 'A',
                                    mcp_create_by = Constant.CurrentUserLogin,
                                    mcp_create_date = funcCls.GetServerDateTime(),
                                    mcp_update_date = funcCls.GetServerDateTime(),
                                    mul_user_login = Constant.CurrentUserLogin
                                };
                                objcomp.mst_contact_persons.Add(objins);
                            }
                        }

                dbc.SubmitChanges();
                lblStatus.Visible = true;
                lblStatus.Focus();
                lblStatus.Text = "Save Complete. Please wait ....";
                lblStatus.Focus();
                if (strType == 'S')
                {
                    this.ClearContent();
                }
                else if (strType == 'D')
                {
                    var tcd_id = (from t1 in dbc.trn_company_details where t1.tcd_code.Equals(txtcompany_code.Text) select t1.tcd_id).FirstOrDefault();
                    Response.Redirect("frmmktNew.aspx?id=" + tcd_id + "&code=" + txtcompany_code.Text);
                }
                
                TimerProgressWait.Enabled = true;
            }
        }
        protected void btnsavedetail_Click(object sender, EventArgs e)
        {
            this.saveType('D');
        }

        protected void btncontact_add_Click(object sender, EventArgs e)
        {
            if (HiddenFieldRowContact.Value == "-1")
            {
                _objaddcontact.Add(new AddContact() { contact_type_id = Convert.ToInt16(ddlcontact_type.SelectedValue), type_id = Convert.ToInt16(ddltype.SelectedValue), 
                        type_name = ddltype.SelectedItem.Text ,contact_type = ddlcontact_type.SelectedItem.Text, contact_id = 0, contact_name = txtcontact_person.Text,
                        contact_tel = txtcontact_person_tel.Text, contact_email = txtcontact_person_email.Text, contact_fax = txtcontact_fax.Text });
            }
            else
            {
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].type_id = Convert.ToInt16(ddltype.SelectedValue);
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].type_name = ddltype.SelectedItem.Text;
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].contact_type_id = Convert.ToInt16(ddlcontact_type.SelectedValue);
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].contact_type = ddlcontact_type.SelectedItem.Text;
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].contact_name = txtcontact_person.Text;
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].contact_tel = txtcontact_person_tel.Text;
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].contact_fax = txtcontact_fax.Text;
                _objaddcontact[Convert.ToInt16(HiddenFieldRowContact.Value)].contact_email = txtcontact_person_email.Text;
                HiddenFieldRowContact.Value = "-1";
                btncontact_add.Text = "เพิ่ม";
            }

            RepeaterContact.DataSource = _objaddcontact;
            RepeaterContact.DataBind();
            ClearContact();
        }

        private void ClearContact()
        {
            ddlcontact_type.SelectedIndex = 0;
            txtcontact_person.Text = "";
            txtcontact_person_email.Text = "";
            txtcontact_person_tel.Text = "";
            txtcontact_fax.Text = "";
        }

        protected void RepeaterContact_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    HiddenFieldRowContact.Value = e.Item.ItemIndex.ToString();
                    ddltype.SelectedValue = ((HiddenField)e.Item.FindControl("HiddenFieldType")).Value;
                    ddlcontact_type.SelectedValue = ((HiddenField)e.Item.FindControl("HiddenFieldTypeId")).Value;
                    txtcontact_person.Text = ((Label)e.Item.FindControl("lblcontact_name")).Text;
                    txtcontact_person_tel.Text = ((Label)e.Item.FindControl("lblcontact_tel")).Text;
                    txtcontact_fax.Text = ((Label)e.Item.FindControl("lblcontact_fax")).Text;
                    txtcontact_person_email.Text = ((Label)e.Item.FindControl("lblcontact_email")).Text;
                    btncontact_add.Text = "แก้ไข";
                    break;
                case "Del":
                    _objdelcontact.Add(new Delcontact { contact_id = Convert.ToInt16(((HiddenField)e.Item.FindControl("HiddenFieldContact_id")).Value) });
                    _objaddcontact.RemoveAt(e.Item.ItemIndex);
                    RepeaterContact.DataSource = _objaddcontact;
                    RepeaterContact.DataBind();
                    break;
            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text != "") { lblStatus.Visible = false; }
        }

        protected void TimerProgressWait_Tick(object sender, EventArgs e)
        {
            
            TimerProgressWait.Enabled = false;
            lblStatus.Visible = false;
        }

        protected void rdoinactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoinactive.Checked == true) { btnsavedetail.Visible = false; }
            if (rdoactive.Checked == true) { btnsavedetail.Visible = true; }
        }

        protected void rdoactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoinactive.Checked == true) { btnsavedetail.Visible = false; }
            if (rdoactive.Checked == true) { btnsavedetail.Visible = true; }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtsearch.Text = "";
        }

    }
    class DropdowData
    {
        public int code { get; set; }
        public string name { get; set; }
    }
    class DropdowDataStr
    {
        public string code { get; set; }
        public string name { get; set; }
    }
    class Delcontact
    {
        public int contact_id { get; set; }
    }
    class AddContact
    {
        public int contact_type_id { get; set; }
        public int type_id { get; set; }
        public string type_name { get; set; }
        public string contact_type { get; set; }
        public int contact_id { get; set; }
        public string contact_name { get; set; }
        public string contact_tel { get; set; }
        public string contact_email { get; set; }
        public string contact_fax { get; set; }
    }
}