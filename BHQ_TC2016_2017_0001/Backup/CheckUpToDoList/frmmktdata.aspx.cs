using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Globalization;
using System.Web.Script.Services;
using System.IO;
using System.Data;
using System.Data.OleDb;
using DBToDoList;

namespace CheckUpToDoList
{

    [Serializable]
    public class PalliativeDocFileEntity
    {
        //public string SessionID { get; set; }
        public int RowID { get; set; }
        public string file_name { get; set; }
        public string attach_file { get; set; }
        public byte[] fileStream { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Status { get; set; }
        public string pathReal { get; set; }
        public bool TypeHPC { get; set; }
        public bool TypeMkt { get; set; }
        public bool TypeCollection { get; set; }
        public bool TypeContact { get; set; }
        // Status O=Old File in Server ,N=New File ,D=Delete File
    }
    [Serializable]
    public class PaymentGrid
    {
        public int RowID { get; set; }
        public int tpa_id { get; set; }
        public int? mst_id{ get; set; }
        public string mst_name { get; set; }
        public int? mpt_id{ get; set; }
        public string mpt_name { get; set; }
        public int? tpa_mpt_credit{ get; set; }
        public string tpa_mpt_remark{ get; set; }
        public int? mbm_id{ get; set; }
        public string mbm_name { get; set; }
        public int? mpm_id{ get; set; }
        public string mpm_name { get; set; }
        public int? mpr_id{ get; set; }
        public string mpr_name { get; set; }
        public int? mpq_id{ get; set; }
        public string mpq_name { get; set; }
        public int? tpa_mpq_credit{ get; set; }
        public int? mpn_id{ get; set; }
        public string mpn_name { get; set; }
        public int? tpa_mpn_credit{ get; set; }
        public int? mrm_id{ get; set; }
        public string mrm_name { get; set; }
        public char? tpa_coupon{ get; set; }
        public string coupon_name { get; set; }
        public string tpa_coupon_remark { get; set; }
        public string Status { get; set; }
        // Status O=Old File in Server ,N=New File ,D=Delete File
    }
    [Serializable]
    public class AddDocCate
    {
        public int mdc_id { get; set; }
        public string mdc_tname { get; set; }
    }
    [Serializable]
    public class AddSite
    {
        public int mcl_id { get; set; }
        public string mcl_tname { get; set; }
    }
    [Serializable]
    public class AddPlan
    {
        public string tpl_code { get; set; }
        public string tpl_name { get; set; }
    }
    [Serializable]
    public class DeletePersonalID
    {
        public int tnc_id { get; set; }
        public int tcd_id { get; set; }
    }
    [Serializable]
    public class PackageDetailList
    {
        public int tpd_id { get; set; }
        public int id { get; set; }
        public int Rowid { get; set; }
        public string tpd_order_code { get; set; }
        public string tpd_order_desc { get; set; }
        public string tpd_order_type{ get; set; }
        public int? mpt_id { get; set; }
        public string mpt_name { get; set; }
        public double? tpd_limit_credit { get; set; }
        public int? tpd_price { get; set; }
        public DateTime? tpd_date_from { get; set; }
        public DateTime? tpd_date_to { get; set; }
        public int? mpy_id { get; set; }
        public string mpy_name { get; set; }
        public string tpd_mpy_remark { get; set; }
        public string tpd_mpy_remark1 { get; set; }
        public string Status { get; set; }
        public string DoctorName { get; set; }
        public int DoctorCatID { get; set; }
        public string DoctorCatName { get; set; }

    }

    [Serializable]
    public class AddConatactPerson
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class AddConatactPersonBill
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class AddConatactPersonMKT
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    class ClsConatactdata
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
    [Serializable]
    class Clstrn_name_check
    {
        public int tnc_id { get; set; }
        public int tcd_id { get; set; }
        public string tnc_legal { get; set; }
        public string tnc_company_name { get; set; }
        public string tnc_emp_id { get; set; }
        public string tnc_personal_id { get; set; }
        public string tnc_hn { get; set; }
        public string tnc_title_name { get; set; }
        public string tnc_fname { get; set; }
        public string tnc_lname { get; set; }
        public char? tnc_gender { get; set; }
        public string tnc_site { get; set; }
        public string tnc_department { get; set; }
        public string tnc_position { get; set; }
        public DateTime? tnc_dob { get; set; }
        public string tnc_age { get; set; }
        public string tnc_program { get; set; }
        public string tnc_option { get; set; }
        public string tnc_appoint_date { get; set; }
        public string tnc_remark { get; set; }
        public string tnc_create_by { get; set; }
        public DateTime tnc_create_date { get; set; }
        public string mul_user_login { get; set; }
        public DateTime tnc_update_date { get; set; }


    }
    public partial class frmmktdata : System.Web.UI.Page
    {
        private List<PaymentGrid> _paymentType = null;
        private List<PackageDetailList> _packagedetail = null;
        private List<PalliativeDocFileEntity> _docfileList = null;
        private List<Clstrn_name_check> objnchk_list = null;
        private List<DeletePersonalID> _PersonalIDList = null;
        private List<AddDocCate> _objdoccate = null;
        private List<AddSite> _objsite = null;
        private List<AddPlan> _objplan = null;
        private List<AddConatactPerson> _objcontact_person = null;
        private List<AddConatactPersonBill> _objcontact_bill = null;
        private List<AddConatactPersonMKT> _objcontact_MKT = null;
        private const string _formatDate = "dd/MM/yyyy";

        public static byte[] ReadToEnd(Stream s)
        {
            using (var ms = new MemoryStream())
            {
                s.CopyTo(ms);
                return ms.ToArray();
                
            }
        }
        private void Cleartempdata()
        {
            ViewState["contact_personList"] = null;
            ViewState["contact_MKTList"] = null;
            ViewState["contact_billList"] = null;
            ViewState["AddPlanList"] = null;
            ViewState["objdoccateList"] = null;
            ViewState["objSiteList"] = null;
            ViewState["paymentType"] = null;
            ViewState["AttachFileList"] = null;
            ViewState["PackageDetailList"] = null;
            ViewState["NamecheckList"] = null;
            ViewState["PersonalIDList"] = null;
        }

        private bool IsPolicyEdit()
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                var objpolicy = (from t1 in dbc.mst_user_logins
                                 where t1.mul_user_login == Constant.CurrentUserLogin
                                 select t1).FirstOrDefault();
                if (objpolicy != null)
                {
                    //กรณที่ไม่เป็น User ที่มีสิทธิ์ใช้หน้า MKT แต่มีสิทธิ์แก้ไขได้ให้ใช้งานหน้าpage โดยไม่ผ่าน function Constant.CheckPolicy("MKT");
                    if (objpolicy.mul_permit == 'W' && objpolicy.mst_user_type.mut_code != "MKT")
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
                  return  false;
                }
            }
        }
        // Save==2 [Completed] tcd_id=0
        protected void Page_Load(object sender, EventArgs e)
        {
            ////TabContainer1.ActiveTabIndex = 0;
            //System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            //System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            //tab3RDPaymentType.Attributes.Add("OnClick", "checkradiopamenttype();");
            //RDPaymentType.Attributes.Add("OnClick", "checkRDpamenttype('" + RDPaymentType.ClientID + "', '" + txtBillAmount.ClientID + "');");
            //RDmpq_id.Attributes.Add("OnClick", "checkRDpamenttype('" + RDmpq_id.ClientID + "', '" + txtmqp_credit.ClientID + "');");
            //RDmpn_id.Attributes.Add("OnClick", "checkRDpamenttype('" + RDmpn_id.ClientID + "', '" + txtmpn_Credit.ClientID + "');");

            ////ถ้ายังไม่ได้ login ให้ ไปหน้า login
            //bool Ispermit = IsPolicyEdit();
            //if (Ispermit==false)
            //{
            //    Constant.CheckPolicy("MKT");
            //}
            

            if (!IsPostBack)
            {


                //TabContainer1.ActiveTabIndex = 0;
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                tab3RDPaymentType.Attributes.Add("OnClick", "checkradiopamenttype();");
                RDPaymentType.Attributes.Add("OnClick", "checkRDpamenttype('" + RDPaymentType.ClientID + "', '" + txtBillAmount.ClientID + "');");
                RDmpq_id.Attributes.Add("OnClick", "checkRDpamenttype('" + RDmpq_id.ClientID + "', '" + txtmqp_credit.ClientID + "');");
                RDmpn_id.Attributes.Add("OnClick", "checkRDpamenttype('" + RDmpn_id.ClientID + "', '" + txtmpn_Credit.ClientID + "');");

                //ถ้ายังไม่ได้ login ให้ ไปหน้า login
                bool Ispermit = IsPolicyEdit();
                if (Ispermit == false)
                {
                    Constant.CheckPolicy("MKT");
                }

                Cleartempdata();
                DDtype.DataValueField = "code";
                DDtype.DataTextField = "name";
                DDtype.DataSource = getMstTypeDropdown();
                DDtype.DataBind();
                if (DDtype.Items.Count > 0) DDtype.SelectedIndex = 0;
                Constant.addListSiteToControl(dlSite);
                Constant.addListDocCateToControl(dlDocCate);
                LoadBinding();
                RDtcd_send_rep_realY.Attributes.Add("onclick", "javascript:realYclick();");
                string st = Convert1.ToString( Request.QueryString["status"]);
               
                if (st == "2")
                {
                    lbmsgAlert.Text = "Save data completed.";
                    lbmsgAlert.Focus();
                    btnAddNew.Visible = false;

                    littab3script.Text = "<script type='text/javascript'>showpaymentType();</script>";
                    littab5.Text = "<script type='text/javascript'>showpaymentType2();</script>";
                }
                else if (st == "1")
                {
                    int tcd_id = Convert1.ToInt32(Request.QueryString["id"]);
                    string active = Convert1.ToString(Request.QueryString["active"]);
                    string edit = Convert1.ToString(Request.QueryString["edit"]);
                    HDedit.Value = edit;
                    HDActive.Value = active;

                    if (tcd_id > 0)
                    {   //Show Date For Edit
                        
                        ShowEditData(tcd_id);
                        Hiddencontrol();
                        SetkeyNumber();
                        //littab1script.Text = strHidbutton;
                        //littab2script.Text = strHidbutton2;
                        littab3script.Text = "<script type='text/javascript'>showpaymentType();</script>";
                        littab5.Text = "<script type='text/javascript'>showpaymentType2();</script>";
                        btnImportExcelContactCheck.Visible = false;

                        if (HDActive.Value == "A") { btnEditFrm.Visible = true; btnCopy.Visible = true; }
                        if (HDActive.Value == "I") { btnEditFrm.Visible = false; btnCopy.Visible = false; }

                        btnExport.Visible = true;
                        //btnCopy.Visible = true;
                        gnvAttachFile.Columns[7].Visible = false;
                        gnvAttachFile.Enabled = false;
                        AttachfileSite.Enabled = false;
                        FileUploadExcelContactCheck.Enabled = false;
                        ImportFileUploadCompanyDetail.Enabled = false;
                        ch_typeAttachfile.Enabled = false;
                        btnAddFile.Visible = false;
                        //btnSaveDraft.Visible = false;
                        btnSave.Visible = false;

                        btnImport.Visible = false;
                        btnAddNew.Visible = true;

                        //----- package----
                        btntab3Add.Visible = false;
                        btnAddRemarkmpy.Visible = false;

                        if (HDedit.Value == "1") { btnEditFrm_Click(null, null); }
                    }
                }
                else
                {   //Show Date For Add New
                    HDStatus.Value = "1";
                    Hiddencontrol();
                    //RunningDocumentCode();
                    SetkeyNumber();
                    gnvAttachFile.Columns[7].Visible = true;
                    gnvAttachFile.Enabled = true;
                    AttachfileSite.Enabled = true;
                    ch_typeAttachfile.Enabled = true;

                    btnAddNew.Visible = false;
                    btnExport.Visible = false;
                    btnCopy.Visible = false;

                    btnImportExcelContactCheck.Visible = true;
                    FileUploadExcelContactCheck.Enabled = true;

                    btnAddFile.Visible = true;
                    ImportFileUploadCompanyDetail.Enabled = true;
                    btnSave.Visible = true;
                    //btnSaveDraft.Visible = true;
                    //littab1script.Text = strShowbutton;
                    //littab2script.Text = strshowbutton2;
                    littab3script.Text = "<script type='text/javascript'>showpaymentType();</script>";
                    littab5.Text = "<script type='text/javascript'>showpaymentType2();</script>";
                    //----- package----
                    btntab3Add.Visible = true;
                    btnAddRemarkmpy.Visible = true;
                    btnImport.Visible = true;
                    //Load Master Company From frmMasterCompany.aspx [Noina 26/09/56]
                    string mco_code = Request.QueryString["code"];
                    if (mco_code != "") { LoadMstCompany(mco_code); }

                }
                showReadOnly("MKT");
            }
            //TimerRedirectPage.Enabled = false;
            //string msgAlert = "File should not be over 4 MB.";
            //string eventFileUpload1onChange = "try { var fileSize = 0; var mybrowser=navigator.userAgent; if (mybrowser.indexOf('MSIE') > 0) { } else { fileSize = $('#FileUpload1')[0].files[0].size; fileSize = fileSize / 1048576; } " +
            //           " if (fileSize > 4) {var fileUp = document.getElementById('FileUpload1'); fileUp.value = ''; $('#lbmsgAlert').html('" + msgAlert + "'); } else { $('#lbmsgAlert').html(''); }} catch (e) { alert('Error is :' + e); }";
            //FileUpload1.Attributes.Add("onChange", eventFileUpload1onChange);

        }

        private void showReadOnly(string StatusPage)
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                var objpolicy = (from t1 in dbc.mst_user_logins
                                 where t1.mul_user_login == Constant.CurrentUserLogin
                                 && t1.mst_user_type.mut_code == StatusPage
                                 select t1);
                if (objpolicy.Count() > 0)
                {
                    var currentuser = objpolicy.FirstOrDefault();
                    if (currentuser.mul_permit == 'R')
                    {
                        btnSave.Visible=false;
                        btnAddNew.Visible=false;
                        btnEditFrm.Visible=false;
                        //btnExport.Visible=false;
                        btnCopy.Visible = false;
                    }
                }
                else
                {
                    if (Constant.CurrentUserLogin == "Admin") { return; }
                }
            }
        }
        private string RunningDocumentCode()
        {
            string doccode = "";
            int year = DateTime.Now.Year;
            string _year = "";
            if (year > 2500) { _year = (year - 543).ToString(); } else { _year = (DateTime.Now.Year).ToString(); }

            doccode = String.Format("{0}{1}-{2}", _year.Substring(2), DateTime.Now.Month.ToString("00"), GetRunningNo());

            return doccode;
        }

        private string GetRunningNo()
        {
            string no = "";
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                //find last tcd_id in now //data.tcd_code == txt_company_code.Text && 
                var objdata = (from data in dbc.trn_company_details where (data.tcd_create_date.Year == DateTime.Now.Year && data.tcd_create_date.Month == DateTime.Now.Month) select data).OrderByDescending(c => c.tcd_document_no).FirstOrDefault();
                if (objdata != null)
                {
                    string oldDocCode = objdata.tcd_document_no;
                    int pos = oldDocCode.IndexOf("-") + 1;
                    string str_docno = oldDocCode.Substring(pos, oldDocCode.Length - pos);
                    int int_docno = Convert.ToInt16(str_docno);
                    no = String.Format("{0:000}", int_docno + 1);
                    //format document code Ex. 1311-001
                }
                else
                {
                    no = "001";
                }
            }
            return no;
        }
        private void autoAddAttributes(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                autoAddAttributes(ctrl);
                if (ctrl.GetType() == typeof(TextBox))
                {
                    ((TextBox)ctrl).Attributes.Add("onKeyup", "parent.calcHeight()");
                }
            }
        }

        private void LoadMstCompany(string mco_code) //[Noina 26/09/56]
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                var objcomp = (from comp in dbc.mst_companies where (comp.mco_code == mco_code) select comp).FirstOrDefault();
                if (objcomp != null)
                {
                    //var objdetail = (from detail in dbc.trn_company_details where detail.tcd_code.Equals(objcomp.mco_code) select detail).FirstOrDefault();
                    //if (objdetail != null) {

                    //    ShowEditData(objdetail.tcd_id);
                    //    HDStatus.Value = "0";
                    //    Hiddencontrol();
                    //    SetkeyNumber();
                    //    //littab1script.Text = strHidbutton;
                    //    //littab2script.Text = strHidbutton2;
                    //    littab3script.Text = "<script type='text/javascript'>showpaymentType();</script>";
                    //    littab5.Text = "<script type='text/javascript'>showpaymentType2();</script>";
                    //    btnImportExcelContactCheck.Visible = false;
                    //    btnEditFrm.Visible = true;
                    //    btnExport.Visible = true;
                    //    btnCopy.Visible = true;
                    //    gnvAttachFile.Columns[7].Visible = false;
                    //    gnvAttachFile.Enabled = false;
                    //    AttachfileSite.Enabled = false;
                    //    FileUploadExcelContactCheck.Enabled = false;
                    //    FileUpload1.Enabled = false;
                    //    ch_typeAttachfile.Enabled = false;
                    //    btnAddFile.Visible = false;
                    //    //btnSaveDraft.Visible = false;
                    //    btnSave.Visible = false;


                    //    btnAddNew.Visible = true;

                    //    //----- package----
                    //    btntab3Add.Visible = false;
                    //    btnAddRemarkmpy.Visible = false;

                    //    LoadContact(dbc, mco_code);

                    //    return;
                    //}

                    HDIsCopy.Value = "1";

                    txt_company_code.Text = objcomp.mco_code;
                    txtComNameTH.Text = objcomp.mco_tname;
                    txtComNameEng.Text = objcomp.mco_ename;
                    txtlegal.Text = objcomp.mco_legal;
                    rdona.Checked = objcomp.mco_type == "N/A" ? true : false;
                    rdojms.Checked = objcomp.mco_type == "JMS" ? true : false;
                    txtComAddress.Text = objcomp.mco_address;
                    txtComTumbon.Text = objcomp.mco_tambon;
                    txtComAumphur.Text = objcomp.mco_district;
                    txtComPostCode.Text = objcomp.mco_postcode;
                    txtComProvince.Text = objcomp.mco_province;
                    txtComTel.Text = objcomp.mco_tel;
                    txtComFax.Text = objcomp.mco_fax;
                    txtComEmail.Text = objcomp.mco_email;

                    txtbillComNameth.Text = objcomp.mco_tname;
                    txtbillAddress.Text = objcomp.mco_address;
                    txtbillTumbon.Text = objcomp.mco_tambon;
                    txtbillAumphur.Text = objcomp.mco_district;
                    txtbillPostCode.Text = objcomp.mco_postcode;
                    txtbillProvince.Text = objcomp.mco_province;
                    txtbillTel.Text = objcomp.mco_tel;
                    txtbillFax.Text = objcomp.mco_fax;

                    LoadContact(dbc, mco_code);
                       //btnSave.Visible = true;
                }
            }

            //RunningDocumentCode();
        }

        private void LoadContact(InhToDoListDataContext dbc,string mco_code)
        {
            if (ViewState["contact_personList"] == null)
                _objcontact_person = new List<AddConatactPerson>();
            else
                _objcontact_person = (List<AddConatactPerson>)ViewState["contact_personList"];

            var datacontactperson = (from t1 in dbc.mst_contact_persons
                                     where t1.mst_contact_type.mct_code == 'C' && t1.mco_code == mco_code
                                     select t1).ToList();
            foreach (var ddata in datacontactperson)
            {
                _objcontact_person.Add(new AddConatactPerson { Name = ddata.mcp_name, Tel = ddata.mcp_tel, Fax = ddata.mcp_fax, Email = ddata.mcp_email });
            }

            ViewState["contact_personList"] = _objcontact_person;
            RepeaterContacrPerson.DataSource = _objcontact_person;
            RepeaterContacrPerson.DataBind();

            var datacontactpersonbill = (from t1 in dbc.mst_contact_persons
                                         where t1.mst_contact_type.mct_code == 'B' && t1.mco_code == mco_code
                                         select t1).ToList();
            if (ViewState["contact_billList"] == null)
                _objcontact_bill = new List<AddConatactPersonBill>();
            else
                _objcontact_bill = (List<AddConatactPersonBill>)ViewState["contact_billList"];

            foreach (var ddata in datacontactpersonbill)
            {
                _objcontact_bill.Add(new AddConatactPersonBill { Name = ddata.mcp_name, Tel = ddata.mcp_tel, Fax = ddata.mcp_fax, Email = ddata.mcp_email });
            }

            ViewState["contact_billList"] = _objcontact_bill;
            RepeaterContactBill.DataSource = _objcontact_bill;
            RepeaterContactBill.DataBind();

            var datacontactpersonmkt = (from t1 in dbc.mst_contact_persons
                                        where t1.mst_contact_type.mct_code == 'M' && t1.mco_code == mco_code
                                        select t1).ToList();
            if (ViewState["contact_MKTList"] == null)
                _objcontact_MKT = new List<AddConatactPersonMKT>();
            else
                _objcontact_MKT = (List<AddConatactPersonMKT>)ViewState["contact_MKTList"];

            foreach (var ddata in datacontactpersonmkt)
            {
                _objcontact_MKT.Add(new AddConatactPersonMKT { Name = ddata.mcp_name, Tel = ddata.mcp_tel, Fax = ddata.mcp_fax, Email = ddata.mcp_email });
            }

            ViewState["contact_MKTList"] = _objcontact_MKT;
            RepeaterContactMKT.DataSource = _objcontact_MKT;
            RepeaterContactMKT.DataBind();
                 
        }
        private void LoadMstCompanySearchByName(string mco_name) //[Noina 26/09/56]
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                var objcomp = (from comp in dbc.mst_companies where (comp.mco_tname == mco_name) || (comp.mco_ename == mco_name) select comp).FirstOrDefault();
                if (objcomp != null)
                {
                    txt_company_code.Text = objcomp.mco_code;
                    txtComNameTH.Text = objcomp.mco_tname;
                    txtComNameEng.Text = objcomp.mco_ename;
                    txtlegal.Text = objcomp.mco_legal;

                    rdona.Checked = objcomp.mco_type == "N/A" ? true : false;
                    rdojms.Checked = objcomp.mco_type == "JMS" ? true : false;

                    txtComAddress.Text = objcomp.mco_address;
                    txtComTumbon.Text = objcomp.mco_tambon;
                    txtComAumphur.Text = objcomp.mco_district;
                    txtComPostCode.Text = objcomp.mco_postcode;
                    txtComProvince.Text = objcomp.mco_province;
                    txtComTel.Text = objcomp.mco_tel;
                    txtComFax.Text = objcomp.mco_fax;
                    txtComEmail.Text = objcomp.mco_email;

                    txtbillComNameth.Text = objcomp.mco_tname;
                    txtbillAddress.Text = objcomp.mco_address;
                    txtbillTumbon.Text = objcomp.mco_tambon;
                    txtbillAumphur.Text = objcomp.mco_district;
                    txtbillPostCode.Text = objcomp.mco_postcode;
                    txtbillProvince.Text = objcomp.mco_province;
                    txtbillTel.Text = objcomp.mco_tel;
                    txtbillFax.Text = objcomp.mco_fax;

                    this.LoadContact(dbc,objcomp.mco_code);

                    //btnSave.Visible = true;
                }
            }

        }

        

        private void LoadBinding()
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                //Payment Type
                var typeWorkList = (from t1 in dbc.mst_payment_types
                                    where t1.mpt_status == 'A'
                                    select new
                                    { Code=t1.mpt_id,
                                      Text =MargeString( t1.mpt_tname , t1.mpt_ename)
                                    });
                RDPaymentType.DataSource = typeWorkList.ToList();
                RDPaymentType.DataValueField = "Code";
                RDPaymentType.DataTextField = "Text";
                RDPaymentType.DataBind();
                var typeWorkList2 = (from t1 in dbc.mst_payment_types
                                    where t1.mpt_status == 'A'
                                    select new
                                    { Code=t1.mpt_id,
                                      Text =MargeString( t1.mpt_tname , t1.mpt_ename)
                                    });
                tab3RDPaymentType.DataSource = typeWorkList2.ToList();
                tab3RDPaymentType.DataValueField = "Code";
                tab3RDPaymentType.DataTextField = "Text";
                tab3RDPaymentType.DataBind();

                //Billing Method 
                var billMethodList = (from t1 in dbc.mst_billing_methods
                                      where t1.mbm_status == 'A'
                                      select new
                                      {
                                          Code=t1.mbm_id,
                                          Text =MargeString( t1.mbm_tname , t1.mbm_ename)
                                      });
                RDMbm_id.DataSource = billMethodList;
                RDMbm_id.DataValueField = "Code";
                RDMbm_id.DataTextField = "Text";
                RDMbm_id.DataBind();

                //การชำระเงินชุดตรวจหลัก / term of payment : Main program
                var paymentMainList = (from t1 in dbc.mst_payment_mains
                                      where t1.mpm_status == 'A'
                                      select new
                                      {
                                          Code = t1.mpm_id,
                                          Text =MargeString( t1.mpm_tname, t1.mpm_ename)
                                      });
                RDMpm_id.DataSource = paymentMainList;
                RDMpm_id.DataValueField = "Code";
                RDMpm_id.DataTextField = "Text";
                RDMpm_id.DataBind();
                //อัตราค่าบริการ / Check-up rate
                var payment_rateList = (from t1 in dbc.mst_payment_rates
                                        where t1.mpr_status== 'A'
                                        select new
                                        {
                                            Code = t1.mpr_id,
                                            Text = MargeString( t1.mpr_tname ,t1.mpr_ename)
                                        });
                RDmpr_id.DataSource = payment_rateList;
                RDmpr_id.DataValueField = "Code";
                RDmpr_id.DataTextField = "Text";
                RDmpr_id.DataBind();
                //ตรวจเพิ่มตามใบเสนอราคา / Term of payment : Options items as montioned in quatation
                var payment_QuatationList = (from t1 in dbc.mst_payment_quatations
                                        where t1.mpq_status == 'A'
                                        select new
                                        {
                                            Code = t1.mpq_id,
                                            Text =MargeString( t1.mpq_tname,t1.mpq_ename)
                                        });
                RDmpq_id.DataSource = payment_QuatationList;
                RDmpq_id.DataValueField = "Code";
                RDmpq_id.DataTextField = "Text";
                RDmpq_id.DataBind();
                //ตรวจเพิ่มนอกใบเสนอราคา / Term of payment : Options items montioned not in quatation 
                var payment_NQuatationList = (from t1 in dbc.mst_payment_nquatations
                                             where t1.mpn_status == 'A'
                                             select new
                                             {
                                                 Code = t1.mpn_id,
                                                 Text =MargeString( t1.mpn_tname,t1.mpn_ename)
                                             });
                RDmpn_id.DataSource = payment_NQuatationList;
                RDmpn_id.DataValueField = "Code";
                RDmpn_id.DataTextField = "Text";
                RDmpn_id.DataBind();
                //รับยาจากการตรวจสุขภาพ / Term of Receiving Medicine
                var Recrive_MedicinList = (from t1 in dbc.mst_receive_medicines
                                              where t1.mrm_status == 'A'
                                              select new
                                              {
                                                  Code = t1.mrm_id,
                                                  Text =MargeString( t1.mrm_tname,t1.mrm_ename)
                                              });
                RDmrm_id.DataSource = Recrive_MedicinList;
                RDmrm_id.DataValueField = "Code";
                RDmrm_id.DataTextField = "Text";
                RDmrm_id.DataBind();
                //คูปองอาหาร / Meal Coupon

                //**************** ข้อมูลการตรวจสุขภาพ ******************
                //Request Doctor Cat
                var DoctorCatList = (from t1 in dbc.mst_doctor_cats
                                        orderby t1.mdc_sort 
                                           where t1.mdc_status == 'A'
                                           select new
                                           {
                                               Code = t1.mdc_id,
                                               Text =MargeString( t1.mdc_tname ,t1.mdc_ename)
                                           });
                
                dlDocCate.DataSource = DoctorCatList;
                dlDocCate.DataValueField = "Code";
                dlDocCate.DataTextField = "Text";
                dlDocCate.DataBind();
                dlDocCate.Items.Insert(0, String.Empty);
                dlDocCate.SelectedIndex = 0;

                var doccatlist2 = (from t1 in dbc.mst_doctor_cats
                                    orderby t1.mdc_sort 
                                    where t1.mdc_status == 'A'
                                    select new
                                    {
                                        Code = t1.mdc_id,
                                        Text =MargeString( t1.mdc_tname ,t1.mdc_ename)
                                    });
                dlDocCate2.DataSource = DoctorCatList;
                dlDocCate2.DataValueField = "Code";
                dlDocCate2.DataTextField = "Text";
                dlDocCate2.DataBind();
                dlDocCate2.SelectedIndex = 0;

                //Check-up Location
                //var CheckUpLocationList = (from t1 in dbc.mst_checkup_locations
                //                     where t1.mcl_status == 'A'
                //                     select new
                //                     {
                //                         Code = t1.mcl_id,
                //                         Text =MargeString(t1.mcl_tname , t1.mcl_ename)
                //                     });
                //dlSite.DataSource = CheckUpLocationList;
                //dlSite.DataValueField = "Code";
                //dlSite.DataTextField = "Text";
                //dlSite.DataBind();
                Constant.addListSiteToControl(dlSite);

                //Medical Reports
                var medicalReportsList = (from t1 in dbc.mst_medical_reports
                                           select new
                                           {
                                               Code = t1.mmr_id,
                                               Text =MargeString( t1.mmr_tname ,t1.mmr_ename)
                                           });
                CHMmmr_id.DataSource = medicalReportsList;
                CHMmmr_id.DataValueField = "Code";
                CHMmmr_id.DataTextField = "Text";
                CHMmmr_id.DataBind();


                //เงื่อนไขการเข้ารับบริการ
                var conditionReportsList = (from t1 in dbc.mst_condition_services
                                            select new
                                            {
                                                Code = t1.mcs_id,
                                                Text =MargeString( t1.mcs_tname ,t1.mcs_ename)
                                            });
                CHmcs_id_Employee.DataSource = conditionReportsList;
                CHmcs_id_Employee.DataValueField = "Code";
                CHmcs_id_Employee.DataTextField = "Text";
                CHmcs_id_Employee.DataBind();
                
                CHmcs_id_Boss.DataSource = conditionReportsList;
                CHmcs_id_Boss.DataValueField = "Code";
                CHmcs_id_Boss.DataTextField = "Text";
                CHmcs_id_Boss.DataBind();

                //mpy data
                refreshMPY(dbc);

                //AttachFile Type in CheckList
                var objtypeAttach = (from t1 in dbc.mst_user_types
                                     where t1.mut_status == 'A'
                                     select new DropdowDataStr { code = t1.mut_code, name = t1.mut_ename }).ToList();
                ch_typeAttachfile.DataSource = objtypeAttach;
                ch_typeAttachfile.DataValueField = "code";
                ch_typeAttachfile.DataTextField = "name";
                ch_typeAttachfile.DataBind();


            }
            //company Year
            List<DropdowData> ddlist = new List<DropdowData>();
            var startAt = DateTime.Now.Year-3;
            var endAt = DateTime.Now.Year + 4;
            for (int dd = startAt; dd < endAt; dd++)
            {
                DropdowData newitem = new DropdowData();
                newitem.code = dd;
                newitem.name = dd.ToString();
                ddlist.Add(newitem);
            }
            DDCompanyYear.DataSource = ddlist;
            DDCompanyYear.DataValueField = "code";
            DDCompanyYear.DataTextField = "name";
            DDCompanyYear.DataBind();
            DDCompanyYear.SelectedValue = Convert1.ToString( DateTime.Now.Year);
        }
        private void ShowEditData(int tcd_id)
        {
            try
            {
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    var tcd = (from t1 in dbc.trn_company_details where t1.tcd_id == tcd_id select t1).FirstOrDefault();
                    if (tcd != null)
                    {   //tcd.tcd_id="";
                        DDCompanyYear.SelectedValue =Convert1.ToString(tcd.tcd_year);
                        txt_company_code.Text = tcd.tcd_code;
                        txt_company_code.Enabled = false;
                        txtdoccode.Text = tcd.tcd_document_no;
                        txtComNameTH.Text = tcd.tcd_tname;
                        txtComNameEng.Text = tcd.tcd_ename;

                        lbCompanyNameTH.Text = tcd.tcd_tname;
                        lbCompanyNameEng.Text = tcd.tcd_ename;
                        txtlegal.Text = tcd.tcd_legal;
                        rdona.Checked = tcd.tcd_type == "N/A" ? true : false;
                        rdojms.Checked = tcd.tcd_type == "JMS" ? true : false;

                        txtComAddress.Text = tcd.tcd_address;
                        txtComTumbon.Text = tcd.tcd_tambon;
                        txtComAumphur.Text = tcd.tcd_district;
                        txtComProvince.Text = tcd.tcd_province;
                        txtComPostCode.Text = tcd.tcd_postcode;
                        txtComTel.Text = tcd.tcd_tel;
                        //tcd.tcd_mobile="";
                        txtComFax.Text = tcd.tcd_fax;
                        txtComEmail.Text = tcd.tcd_email;
                        if (tcd.tcd_date_from != null)
                        {
                            txtconDatefrom.Text = tcd.tcd_date_from.Value.ToString("dd/MM/yyyy");
                        }
                        if (tcd.tcd_date_to != null)
                        {
                            txtConDateto.Text = tcd.tcd_date_to.Value.ToString("dd/MM/yyyy");
                        }

                        txtbillComNameth.Text = tcd.tcd_bill_company;
                        txtbillAddress.Text = tcd.tcd_bill_address;
                        txtbillTumbon.Text = tcd.tcd_bill_tambon;
                        txtbillAumphur.Text = tcd.tcd_bill_district;
                        txtbillProvince.Text = tcd.tcd_bill_province;
                        txtbillPostCode.Text = tcd.tcd_bill_postcode;
                        txtbillTel.Text = tcd.tcd_bill_tel;
                        //tcd.tcd_bill_mobile="";
                        txtbillFax.Text = tcd.tcd_bill_fax;
                        txtFamilyWelfar.Text = tcd.tcd_family_welfare;

                        txt_payor.Text = tcd.tcd_payor;

                        //tcd.tcd_doc_code="";
                        //tcd.tcd_doc_fname="";
                        //tcd.tcd_doc_tname="";
                        txttcdLocation_remark.Text = tcd.tcd_location_remark;
                        txtResultAddress.Text = tcd.tcd_result_address;
                        txtResultTumbon.Text = tcd.tcd_result_tambon;
                        txtResultAumphur.Text = tcd.tcd_result_district;
                        txtResultProvince.Text = tcd.tcd_result_province;
                        txtResultPostCode.Text = tcd.tcd_result_postcode;
                        if (tcd.tcd_send_rep_real == "Y")
                        {
                            RDtcd_send_rep_realY.Checked = true;
                        }
                        else if (tcd.tcd_send_rep_real == "N")
                        {
                            RDtcd_send_rep_realN.Checked = true;
                        }
                        if (tcd.tcd_send_rep_flag == "H")
                        {
                            RDtcd_send_rep_flagH.Checked = true;
                        }
                        else if (tcd.tcd_send_rep_flag == "C")
                        {
                            RDtcd_send_rep_flagC.Checked = true;
                        }

                        RDtcd_send_rep_copy.SelectedValue = tcd.tcd_send_rep_copy;

                        EditorReamrk.Content = Server.HtmlDecode(tcd.tcd_remark);

                        //company Request Doctor
                        if (tcd.trn_company_request_doctors.Count() > 0)
                        {
                            var objcurrentcompanyRequestDoc=tcd.trn_company_request_doctors.FirstOrDefault();
                            txt_req_doc.Text = objcurrentcompanyRequestDoc.tcr_doc_code + "/" + objcurrentcompanyRequestDoc.tcr_doc_tname + "/" + objcurrentcompanyRequestDoc.tcr_doc_ename;
                        }

                        //Medical report
                        var objmr = (from t1 in tcd.trn_medical_reports
                                                    select t1).ToList();
                        foreach (trn_medical_report itemmr in objmr)
                        {
                            foreach (ListItem item in CHMmmr_id.Items)
                            {
                                if (item.Value.Trim() == itemmr.mmr_id.ToString())//text or value (item.Value.Trim())
                                {
                                    item.Selected = true;
                                }
                            }
                            if (itemmr.tmr_rep_remark.Trim() != "")
                            {
                                txttcd_rep_remark.Text = itemmr.tmr_rep_remark;
                            }
                        }

                        // save Contact B=Bill, C=Person, M=Markeing,P=Payor
                        var datacontactperson = (from t1 in tcd.trn_contact_persons
                                                 where t1.mst_contact_type.mct_code == 'C'
                                                 select t1).ToList();
                        if (ViewState["contact_personList"] == null)
                            _objcontact_person = new List<AddConatactPerson>();
                        else
                            _objcontact_person = (List<AddConatactPerson>)ViewState["contact_personList"];

                        foreach (var ddata in datacontactperson)
                        {
                            _objcontact_person.Add(new AddConatactPerson { Name = ddata.tcp_name, Tel = ddata.tcp_tel,Fax = ddata.tcp_fax,Email = ddata.tcp_email });
                        }

                        ViewState["contact_personList"] = _objcontact_person;
                        RepeaterContacrPerson.DataSource = _objcontact_person;
                        RepeaterContacrPerson.DataBind();

                        var datacontactpersonbill = tcd.trn_contact_persons.Where(x => x.mct_id == 2).ToList() ;

                        if (ViewState["contact_billList"] == null)
                            _objcontact_bill = new List<AddConatactPersonBill>();
                        else
                            _objcontact_bill = (List<AddConatactPersonBill>)ViewState["contact_billList"];

                        foreach (var ddata in datacontactpersonbill)
                        {
                            _objcontact_bill.Add(new AddConatactPersonBill { Name = ddata.tcp_name, Tel = ddata.tcp_tel, Fax = ddata.tcp_fax, Email = ddata.tcp_email });
                        }

                        ViewState["contact_billList"] = _objcontact_bill;
                        RepeaterContactBill.DataSource = _objcontact_bill;
                        RepeaterContactBill.DataBind();



                        //Maketing
                        var datacontactpersonmkt = (from t1 in tcd.trn_contact_persons
                                                     where t1.mst_contact_type.mct_code == 'M'
                                                     select t1).ToList();
                        if (ViewState["contact_MKTList"] == null)
                            _objcontact_MKT = new List<AddConatactPersonMKT>();
                        else
                            _objcontact_MKT = (List<AddConatactPersonMKT>)ViewState["contact_MKTList"];

                        foreach (var ddata in datacontactpersonmkt)
                        {
                            _objcontact_MKT.Add(new AddConatactPersonMKT { Name = ddata.tcp_name, Tel = ddata.tcp_tel, Fax = ddata.tcp_fax, Email = ddata.tcp_email });
                        }

                        ViewState["contact_MKTList"] = _objcontact_MKT;
                        RepeaterContactMKT.DataSource = _objcontact_MKT;
                        RepeaterContactMKT.DataBind();


                        //trn_plan
                        if (ViewState["AddPlanList"] == null)
                            _objplan = new List<AddPlan>();
                        else
                            _objplan = (List<AddPlan>)ViewState["AddPlanList"];

                        var dataplan = (from t1 in tcd.trn_plans
                                        select new { tpl_code = t1.tpl_code, tpl_name = t1.tpl_name }).ToList();

                        foreach (var ddata in dataplan)
                        {
                            _objplan.Add(new AddPlan { tpl_code = ddata.tpl_code , tpl_name = ddata.tpl_name });
                        }

                        ViewState["AddPlanList"] = _objplan;
                        RepeaterPlan.DataSource = _objplan;
                        RepeaterPlan.DataBind();

                        //company Request Doctor Cat
                        if (ViewState["objdoccateList"] == null)
                            _objdoccate = new List<AddDocCate>();
                        else
                            _objdoccate = (List<AddDocCate>)ViewState["objdoccateList"];

                        var dataReqDocCat = (from t1 in tcd.trn_company_doctor_cats
                                             select new { mdc_id = t1.mdc_id, mdc_tname = GetNameDocCate(dbc,t1.mdc_id) }).ToList();
                        foreach (var ddata in dataReqDocCat)
                        {
                            _objdoccate.Add(new AddDocCate { mdc_id = ddata.mdc_id, mdc_tname = ddata.mdc_tname });
                        }
                        ViewState["objdoccateList"] = _objdoccate;
                        RepeaterDocCate.DataSource = _objdoccate;
                        RepeaterDocCate.DataBind();

                        //txtHiddenDocCate.Value = strdata;

                        //CheckUp Loacation
                        if (ViewState["objSiteList"] == null)
                            _objsite = new List<AddSite>();
                        else
                            _objsite = (List<AddSite>)ViewState["objSiteList"];

                        var dataLocation = (from t1 in dbc.trn_checkup_locations
                                            where t1.tcd_id == tcd_id
                                            select new { mcl_id = t1.mcl_id, mcl_tname = GetNameSite(dbc, t1.mcl_id) }).ToList();
                        foreach (var ddata in dataLocation)
                        {
                            _objsite.Add(new AddSite { mcl_id = ddata.mcl_id, mcl_tname = ddata.mcl_tname});
                        }
                        ViewState["objSiteList"] = _objsite;

                        RepeaterSite.DataSource = _objsite;
                        RepeaterSite.DataBind();

                        //strdata = ""
                        //var dataLocation = (from t1 in dbc.trn_checkup_locations
                        //                    where t1.tcd_id == tcd_id
                        //                    select t1.mcl_id).ToList();
                        //var dtmstLocation = (from t2 in dbc.mst_checkup_locations
                        //                     where dataLocation.Contains(t2.mcl_id)
                        //                     select t2).ToList();
                        //foreach (mst_checkup_location ddata in dtmstLocation)
                        //{
                        //    string NameAll = MargeString(ddata.mcl_tname, ddata.mcl_ename);
                        //    if (strdata == "")
                        //    {
                        //        strdata = ddata.mcl_id.ToString() + "," + NameAll;
                        //    }
                        //    else
                        //    {
                        //        strdata += "|*|" + ddata.mcl_id.ToString() + "," + NameAll;
                        //    }
                        //}
                        //txtHiddenSite.Value = strdata;

                        //แสดง Payment Type
                       var  old_paymentType = (from t1 in dbc.trn_payments
                                        where t1.tcd_id == tcd_id
                                        select new
                                        {
                                             mst_id=t1.mst_id,
                                             mst_nameTh = (t1.mst_type != null) ? t1.mst_type.mst_tname : "",
                                             mst_nameEn = (t1.mst_type != null) ? t1.mst_type.mst_ename : "",
                                             mpt_id=t1.mpt_id,
                                             mpt_nameTh = (t1.mst_payment_type != null) ? t1.mst_payment_type.mpt_tname : "",
                                             mpt_nameEn = (t1.mst_payment_type != null) ? t1.mst_payment_type.mpt_ename : "",
                                             tpa_mpt_credit=t1.tpa_mpt_credit,
                                             tpa_mpt_remark=t1.tpa_mpt_remark,
                                             mbm_id=t1.mbm_id,
                                             mbm_nameTh = (t1.mst_billing_method != null) ? t1.mst_billing_method.mbm_tname : "",
                                             mbm_nameEn = (t1.mst_billing_method != null) ? t1.mst_billing_method.mbm_ename : "",
                                             mpm_id=t1.mpm_id,
                                             mpm_nameTh = (t1.mst_payment_main != null) ?t1.mst_payment_main.mpm_tname : "",
                                             mpm_nameEn = (t1.mst_payment_main != null) ?t1.mst_payment_main.mpm_ename : "",
                                             mpr_id=t1.mpr_id,
                                             mpr_nameTh=(t1.mst_payment_rate!=null)?t1.mst_payment_rate.mpr_tname:"",
                                             mpr_nameEn=(t1.mst_payment_rate!=null)?t1.mst_payment_rate.mpr_ename:"",
                                             mpq_id=t1.mpq_id,
                                             mpq_nameTh=(t1.mst_payment_quatation!=null)?t1.mst_payment_quatation.mpq_tname:"",
                                             mpq_nameEn = (t1.mst_payment_quatation != null) ? t1.mst_payment_quatation.mpq_ename : "",
                                             tpa_mpq_credit=t1.tpa_mpq_credit,
                                             mpn_id=t1.mpn_id,
                                             mpn_nameTh = (t1.mst_payment_nquatation != null) ? t1.mst_payment_nquatation.mpn_tname : "",
                                             mpn_nameEn = (t1.mst_payment_nquatation != null) ? t1.mst_payment_nquatation.mpn_ename : "",
                                             tpa_mpn_credit = t1.tpa_mpn_credit,
                                             mrm_id= t1.mrm_id,
                                             mrm_nameTh = (t1.mst_receive_medicine != null) ? t1.mst_receive_medicine.mrm_tname : "",
                                             mrm_nameEn = (t1.mst_receive_medicine != null) ? t1.mst_receive_medicine.mrm_ename : "",
                                             tpa_coupon= t1.tpa_coupon,
                                             coupon_name = (t1.tpa_coupon=='I')?"ให้":"ไม่ให้",
                                             tpa_coupon_remark=t1.tpa_coupon_remark,
                                             Status="O", 
                                             tpa_id=t1.tpa_id
                                        }).ToList();
                       _paymentType = new List<PaymentGrid>();
                       _paymentType = (from t1 in old_paymentType
                                        select new PaymentGrid
                                        {
                                            mst_id = t1.mst_id,
                                            mst_name = t1.mst_nameTh + "/" + t1.mst_nameEn,
                                            mpt_id = t1.mpt_id,
                                            mpt_name = t1.mpt_nameTh + "/" + t1.mpt_nameEn,
                                            tpa_mpt_credit = t1.tpa_mpt_credit,
                                            tpa_mpt_remark = t1.tpa_mpt_remark,
                                            mbm_id = t1.mbm_id,
                                            mbm_name = t1.mbm_nameTh + "/" + t1.mbm_nameEn,
                                            mpm_id = t1.mpm_id,
                                            mpm_name = t1.mpm_nameTh + "/" + t1.mpm_nameEn,
                                            mpr_id = t1.mpr_id,
                                            mpr_name = t1.mpr_nameTh + "/" + t1.mpr_nameEn,
                                            mpq_id = t1.mpq_id,
                                            mpq_name = t1.mpq_nameTh + "/" + t1.mpq_nameEn,
                                            tpa_mpq_credit = t1.tpa_mpq_credit,
                                            mpn_id = t1.mpn_id,
                                            mpn_name = t1.mpn_nameTh + "/" + t1.mpn_nameEn,
                                            tpa_mpn_credit = t1.tpa_mpn_credit,
                                            mrm_id = t1.mrm_id,
                                            mrm_name = t1.mrm_nameTh + "/" + t1.mrm_nameEn,
                                            tpa_coupon = t1.tpa_coupon,
                                            coupon_name = (t1.tpa_coupon == 'I') ? "ให้" : "ไม่ให้",
                                            tpa_coupon_remark = t1.tpa_coupon_remark,
                                            Status = "N",
                                            tpa_id = t1.tpa_id
                                        }).ToList();
                       int rowcount = 0;
                        foreach (PaymentGrid item in _paymentType)
                        {
                            item.RowID = rowcount;
                            rowcount++;
                        }
                        HiddenPaymentRunning.Value = rowcount.ToString();
                        ViewState["paymentType"] = _paymentType;

                        ShowGridPaymentType();

                        //แสดง file ที่มีการ upload ไปแล้ว
                        //_docfileList = new List<PalliativeDocFileEntity>();
                        //foreach (PalliativeDocFileEntity item in _docfileList)
                        //{
                        //    if (item.SessionID == Session.SessionID)
                        //    {
                        //        _docfileList.Remove(item);
                        //    }
                        //}
                        if (ViewState["AttachFileList"] == null)
                            _docfileList = new List<PalliativeDocFileEntity>();
                        else
                            _docfileList = (List<PalliativeDocFileEntity>)ViewState["AttachFileList"];

                       _docfileList = (from t1 in dbc.mst_path_files
                                             where t1.tcd_id == tcd_id
                                             select new PalliativeDocFileEntity
                                             {   
                                                 RowID=t1.mpf_id,
                                                 file_name =t1.mpf_file_name,
                                                 attach_file = Constant.HttpUrl + t1.mpf_path_name,
                                                 pathReal=t1.mpf_path_name,
                                                 fileStream = null,
                                                 UpdateDate = t1.mpf_update_date,
                                                 UpdateBy = t1.mul_user_login, 
                                                 Status="N",
                                                 TypeHPC = t1.trn_attach_files.Where(x=>x.mst_user_type.mut_code=="HPC").Count()>0,
                                                 TypeCollection = t1.trn_attach_files.Where(x => x.mst_user_type.mut_code == "CLT").Count() > 0,
                                                 TypeContact = t1.trn_attach_files.Where(x => x.mst_user_type.mut_code == "CTC").Count() > 0,
                                                 TypeMkt = t1.trn_attach_files.Where(x => x.mst_user_type.mut_code == "MKT").Count() > 0,
                                                    
                                             }).ToList();
                        // _docfileList.AddRange(_docfileListxx);
                        gnvAttachFile.DataSource = _docfileList;
                        gnvAttachFile.DataBind();
                        ViewState["AttachFileList"] = _docfileList;

                        //แสดง package ที่มีการเลือกไว้แล้ว
                        if (ViewState["PackageDetailList"] == null)
                            _packagedetail = new List<PackageDetailList>();
                        else
                            _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];


                        _packagedetail = (from t1 in dbc.trn_package_details
                                          where t1.tcd_id == tcd_id
                                          select new PackageDetailList
                                          {
                                             id = 0,
                                             tpd_id=t1.tpd_id,
                                             Rowid=t1.tpd_id,
                                             tpd_order_code= t1.tpd_order_code,
                                             tpd_order_desc=t1.tpd_order_desc,
                                             tpd_order_type=t1.tpd_order_type,
                                             mpt_id=t1.mpt_id,
                                             mpt_name=t1.tpd_mpt_name,
                                             tpd_mpy_remark=t1.tpd_mpy_remark,
                                             tpd_mpy_remark1=t1.tpd_mpy_remark1,
                                             mpy_id =t1.mpy_id,
                                             mpy_name=t1.tpd_mpy_name,
                                             Status="N",
                                             tpd_date_from=t1.tpd_date_from,
                                             tpd_date_to=t1.tpd_date_to,
                                             tpd_limit_credit=t1.tpd_limit_credit,
                                             tpd_price=t1.tpd_price,
                                             DoctorName=t1.trn_package_request_doctors.FirstOrDefault().trd_doc_tname,
                                             DoctorCatID=t1.trn_package_doctor_cats.FirstOrDefault().mdc_id
                                          }).ToList();

                        int rowcountpackage = 0;
                        foreach (PackageDetailList itemdd in _packagedetail)
                        {
                            itemdd.id = rowcountpackage;
                            rowcountpackage++;
                        }
                        HiddenCountRowPackage.Value = rowcountpackage.ToString();
                        ViewState["PackageDetailList"] = _packagedetail;
                        BindPackage(_packagedetail);

                        //Condition service
                        var objconditionsservice = (from t1 in tcd.trn_condition_services
                                                    where t1.tcs_type == "EM"
                                                    select t1).ToList();
                        foreach (trn_condition_service itemcondition in objconditionsservice)
                        {
                            foreach (ListItem item in CHmcs_id_Employee.Items)
                            {
                                if (item.Value.Trim() == itemcondition.mcs_id.ToString())//text or value (item.Value.Trim())
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                        var objconditionsserviceX = (from t1 in tcd.trn_condition_services
                                                    where t1.tcs_type == "EX"
                                                    select t1).ToList();
                        foreach (trn_condition_service itemcondition in objconditionsserviceX)
                        {
                            foreach (ListItem item in CHmcs_id_Boss.Items)
                            {
                                if (item.Value.Trim() == itemcondition.mcs_id.ToString())//text or value (item.Value.Trim())
                                {
                                    item.Selected = true;
                                }
                            }
                        }

                        //search name check
                        var objtnc = (from tnc in dbc.trn_name_checks
                                      where tnc.tcd_id == tcd.tcd_id
                                      select new Clstrn_name_check
                                      {
                                           tcd_id=tnc.tcd_id,
                                            mul_user_login=tnc.mul_user_login,
                                             tnc_age=tnc.tnc_age,
                                              tnc_appoint_date=tnc.tnc_appoint_date,
                                               tnc_company_name=tnc.tnc_company_name,
                                                tnc_create_by=tnc.tnc_create_by,
                                                 tnc_create_date=tnc.tnc_create_date,
                                                  tnc_department=tnc.tnc_department,
                                                   tnc_dob=tnc.tnc_dob,
                                                    tnc_emp_id=tnc.tnc_emp_id,
                                                    tnc_fname=tnc.tnc_fname,
                                                    tnc_gender=tnc.tnc_gender,
                                                    tnc_hn=tnc.tnc_hn,
                                                    tnc_id=tnc.tcd_id,
                                                    tnc_legal=tnc.tnc_legal,
                                                     tnc_lname=tnc.tnc_lname,
                                                     tnc_option=tnc.tnc_option,
                                                     tnc_personal_id=tnc.tnc_personal_id,
                                                     tnc_position=tnc.tnc_position,
                                                     tnc_program=tnc.tnc_program,
                                                     tnc_remark=tnc.tnc_remark,
                                                     tnc_site=tnc.tnc_site,
                                                     tnc_title_name=tnc.tnc_title_name,
                                                     tnc_update_date=tcd.tcd_update_date
                                      }).ToList();

                        lblnamechk_compname_th.Text = (lblnamechk_compname_th.Text == "-" ? txtComNameTH.Text : lblnamechk_compname_th.Text);
                        lblnamechk_compname_en.Text = (lblnamechk_compname_en.Text == "-" ? txtComNameEng.Text : lblnamechk_compname_en.Text);
                        txtnamechk_legal.Text = (txtnamechk_legal.Text == String.Empty ? txtlegal.Text : txtnamechk_legal.Text);
                        txtnamechk_compname.Text = (txtnamechk_compname.Text == String.Empty ? txtComNameTH.Text : txtnamechk_compname.Text);

                        if (ViewState["NamecheckList"] == null)
                            objnchk_list = new List<Clstrn_name_check>();
                        else
                            objnchk_list = (List<Clstrn_name_check>)ViewState["NamecheckList"];

                        if (objtnc.Count != 0)
                        {
                            objnchk_list = objtnc;
                            ViewState["NamecheckList"] = objnchk_list;
                            RepeaterPatient.DataSource = objnchk_list;
                            RepeaterPatient.DataBind();
                            //this.ListNameCheck();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                lbmsgAlert.Text = ex.Message;
            }
        }

        private string GetNameSite(InhToDoListDataContext dbc, int mcl_id)
        {
            string siteName = (from t2 in dbc.mst_checkup_locations
                               where t2.mcl_id == mcl_id select t2.mcl_tname + " / " + t2.mcl_ename).FirstOrDefault();
            return siteName;
        }

        private void refreshMPY(InhToDoListDataContext dbc)
        {   //mpy data
            var mpyList =(from t1 in dbc.mst_prepare_yourselfs
                            where t1.mpy_status=='A'
                            select new DropdowData{
                             code=t1.mpy_id,
                             name=t1.mpy_desc
                            }).ToList();
            DropdowData newitem = new DropdowData();
            newitem.code = 0;
            newitem.name = "Select Item";
            mpyList.Insert(0, newitem);
            DDmpy.DataSource = mpyList.ToList();
            DDmpy.DataValueField = "code";
            DDmpy.DataTextField = "name";
            DDmpy.DataBind();
            DDmpy.SelectedIndex=0;
        }
        private void Hiddencontrol()
        {   //สิทธิการ แกไข้ข้อมูล
            bool IsEdit = false;
            if (HDStatus.Value == "1") { IsEdit = true; }

            //if (HDedit.Value == "1") { IsEdit = true; }

            PanelCheckupData.Enabled = IsEdit;
            PanelCompanyData.Enabled = IsEdit;
            PanelPayment.Enabled = IsEdit;
            PanelPackage.Enabled = IsEdit;
            PanelNameCheckup.Enabled = IsEdit;

            PanelRemark.Enabled = IsEdit;

            btnImportExcelContactCheck.Enabled = IsEdit;
            //foreach (Control vdata in TabContainer1.Controls)
            //{
            //    foreach (Control tabpanel in vdata.Controls)
            //    {

            //        foreach (Control item in tabpanel.Controls)
            //        {
            //            if (tabpanel is Panel) { ((Panel)tabpanel).Enabled = false; }
            //            //SetEnable(item, IsEdit);
            //        }
            //    }
            //}



            // tab payment
            DDtype.Enabled=IsEdit;
            RDPaymentType.Enabled=IsEdit;
            txtBillAmount.Enabled=IsEdit;
            txtBillRemark.Enabled=IsEdit;
            RDMbm_id.Enabled=IsEdit;
            RDMpm_id.Enabled=IsEdit;
            RDmpr_id.Enabled=IsEdit;
            RDmpq_id.Enabled=IsEdit;
            txtmqp_credit.Enabled=IsEdit;
            RDmpn_id.Enabled=IsEdit;
            txtmpn_Credit.Enabled=IsEdit;
            RDmrm_id.Enabled=IsEdit;
            RDCoupon.Enabled=IsEdit;
            txtCouponRemark.Enabled=IsEdit;
            btnAddPaymentType.Visible=IsEdit;
            GridPaymentType.Enabled = IsEdit;

            //tab รายชื่อผู้ตรวจ
            btnAddNewData.Visible = IsEdit;
            //foreach (RepeaterItem item in RepeaterPatient.Items)
            //{
            //    item.FindControl("ImgEdit").Visible = IsEdit;
            //    item.FindControl("ImgDel").Visible = IsEdit;
            //}
        }
        private void SetEnable(Control item, bool IsEdit)
        {
            if (item is TextBox)
            {
                TextBox txtitem = (TextBox)item;
                txtitem.Enabled = IsEdit;
            }
            else if (item is CheckBoxList)
            {
                CheckBoxList chitem = (CheckBoxList)item;
                chitem.Enabled = IsEdit;
            }
            else if (item is RadioButtonList)
            {
                RadioButtonList Rditem = (RadioButtonList)item;
                Rditem.Enabled = IsEdit;
            }
            else if (item is Button)
            {
                Button btitem = (Button)item;
                btitem.Visible = IsEdit;
            }else if(item is DropDownList){
                DropDownList Dritem = (DropDownList)item;
                Dritem.Enabled = IsEdit;
            }
            else if (item is RadioButton)
            {
                RadioButton Rditem = (RadioButton)item;
                Rditem.Enabled = IsEdit;
            }
            else if (item is FileUpload)
            {
                FileUpload FUitem = (FileUpload)item;
                FUitem.Visible = IsEdit;
            }
            else if (item is Panel)
            {
                Panel FUitem = (Panel)item;
                FUitem.Visible = IsEdit;
            }
        }
        private void SetkeyNumber()
        {
            Constant.SetTextKeyNumber(ref txtBillAmount); 
            Constant.SetTextKeyNumber(ref txtmqp_credit);
            Constant.SetTextKeyNumber(ref txtmpn_Credit);
            Constant.SetTextKeyNumber(ref txtLimitCredit); 
            Constant.SetTextKeyNumber(ref txtPricevalue);
        }

        protected void Tab1_Click(object sender, EventArgs e)
        {
            //Tab1.CssClass = "ui-state-default ui-corner-top ui-tabs-active ui-state-active";
            //Tab2.CssClass = "ui-state-default ui-corner-top";
            //Tab3.CssClass = "ui-state-default ui-corner-top";
            //Tab4.CssClass = "ui-state-default ui-corner-top";
            //MainView.ActiveViewIndex = 0; 
        }
        protected void Tab2_Click(object sender, EventArgs e)
        {
            //Tab1.CssClass = "ui-state-default ui-corner-top";
            //Tab2.CssClass = "ui-state-default ui-corner-top ui-tabs-active ui-state-active";
            //Tab3.CssClass = "ui-state-default ui-corner-top";
            //Tab4.CssClass = "ui-state-default ui-corner-top";
            //MainView.ActiveViewIndex = 1; 
        }
        protected void Tab3_Click(object sender, EventArgs e)
        {
            //Tab1.CssClass = "ui-state-default ui-corner-top";
            //Tab2.CssClass = "ui-state-default ui-corner-top";
            //Tab3.CssClass = "ui-state-default ui-corner-top ui-tabs-active ui-state-active";
            //Tab4.CssClass = "ui-state-default ui-corner-top";
            //MainView.ActiveViewIndex = 2;
            //lbCompanyNameEng.Text = txtComNameEng.Text;
            //lbCompanyNameTH.Text = txtComNameTH.Text;
            //bool IsEdit = false;
            //if (HDStatus.Value == "1") { IsEdit = true; }

            //btntab3Add.Visible = IsEdit;
            //btnAddRemarkmpy.Visible = IsEdit;

        }
        protected void Tab4_Click(object sender, EventArgs e)
        {
            //Tab1.CssClass = "ui-state-default ui-corner-top";
            //Tab2.CssClass = "ui-state-default ui-corner-top";
            //Tab3.CssClass = "ui-state-default ui-corner-top";
            //Tab4.CssClass = "ui-state-default ui-corner-top ui-tabs-active ui-state-active";
            //MainView.ActiveViewIndex = 3; 
        }
        protected void Tab5_Click(object sender, EventArgs e)
        {
            //MainView.ActiveViewIndex = 4; 
        }

        //Import Excel
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (ImportFileUploadCompanyDetail.HasFile == true)
            {

                string ContentType = Path.GetExtension(ImportFileUploadCompanyDetail.FileName);
                if (ContentType == ".xlsx" || ContentType == ".xls")
                {
                    lbmsgAlert.Text = "";


                    //check file size
                    decimal kb_size = ImportFileUploadCompanyDetail.PostedFile.ContentLength;
                    decimal mb_size = kb_size / 1048576;
                    if (mb_size > 4) { lbmsgAlert.Text = "OverSize Limit 4 MB. Uploaded File Size is " + String.Format("{0:N2}", mb_size) + " MB."; return; }


                    lbmsgAlert.Text = string.Empty;
                    string FileName = Path.GetFileName(ImportFileUploadCompanyDetail.PostedFile.FileName);

                    string Extension = Path.GetExtension(ImportFileUploadCompanyDetail.PostedFile.FileName);

                    string FolderPath = Constant.pathTempUpload; //"/ToDoList/tempUpload/";

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    ImportFileUploadCompanyDetail.SaveAs(FilePath);
                    //lbmsgAlert.Text = "begin Import path file:" + FilePath;
                    try
                    {
                        ReadExcel(FilePath);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "External table is not in the expected format.")
                        {
                            lbmsgAlert.Text = "Please select valid excel sheet";
                        }
                        else lbmsgAlert.Text = ex.Message;
                    }
                    File.Delete(FilePath);
                    Constant.RemoveOldFiles();
                }
                else
                {
                    lbmsgAlert.Text = "Please attach excel sheet, then click Import file";
          
                }
            }
            else
            {
                lbmsgAlert.Text = "Please attach excel sheet, then click Import file";
            }
        }
        private void ReadExcel(string filePath)
        {
            try
            {
                DataTable table = new DataTable();
                string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"", filePath);
                using (OleDbConnection dbConnection = new OleDbConnection(strConn))
                {
                    using (OleDbDataAdapter dbAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", dbConnection)) //rename sheet if required!
                        dbAdapter.Fill(table);
                    int rows = table.Rows.Count;
                    Boolean icount = false;

                    var strCode = "xxx";
                    List<ClsConatactdata> contactdata = new List<ClsConatactdata>();
                    foreach (DataRow dr in table.Rows)
                    {
                        //1Code	2Thai	3Eng	4Address	5Tambon	6District	7Province	8Postcode	9Tel.	10Fax.	
                        //11E-mail 12Address	Name	13Tel.	14Fax.	15Email	16Form	17To	18Company Name(Eng)   19Address	20Tambon
                        //21District	22Province	23Postcode	24Tel.	25Fax.	26E-mail Address	27Type	28Payment Type 29วงเงิน(บาท)	30Remark
                        //31Billing Method	32Check-up Rate	33Main Program	34Options items as mentioned in quotation 	35วงเงิน(บาท)	36Options items as mentioned not in quotation 	37วงเงิน(บาท)	
                        //38Term of receiving Medicine	39Meal Coupon	40Family's welfare	41Name	42Doctor Cat.	43Name	44Remark	
                        //45Address 46sub-district 47District	48Province	49Postcode	
                        //50Name	51อื่นๆ	52ฉบับจริง	53กรณีไม่รับกลับ	54สำเนา	55พนักงาน	56ผู้บริหาร	    57Name	58Tel.	59Fax.	
                        //60Email Address	61Remark

                        if (icount == true)
                        {
                            var dat1No = dr[0].ToString();

                            if (strCode != dat1No && strCode == "xxx")
                            {
                                strCode = dat1No;
                                txt_company_code.Text = dat1No;

                                var dat2Thai = dr[1].ToString(); txtComNameTH.Text = dat2Thai;
                                var dat3Eng = dr[2].ToString(); txtComNameEng.Text = dat3Eng;
                                var dat4Address = dr[3].ToString(); txtComAddress.Text = dat4Address;
                                var dat5Tambon = dr[4].ToString(); txtComTumbon.Text = dat5Tambon;
                                var dat6District = dr[5].ToString(); txtComAumphur.Text = dat6District;
                                var dat7Province = dr[6].ToString(); txtComProvince.Text = dat7Province;
                                var dat8Postcode = dr[7].ToString(); txtComPostCode.Text = dat8Postcode;
                                var dat9Tel = dr[8].ToString(); txtComTel.Text = dat9Tel;
                                var dat10Fax = dr[9].ToString(); txtComFax.Text = dat10Fax;
                                var dat11E_mail = dr[10].ToString(); txtComEmail.Text = dat11E_mail;

                                var dat12AddressName = dr[11].ToString();

                                //var dat13Tel = dr[12].ToString(); txtConTel.Text = dat13Tel;
                                //var dat14Fax = dr[13].ToString(); txtConFax.Text = dat14Fax;
                                //var dat15Email = dr[14].ToString(); txtConEmail.Text = dat15Email;
                                var dat16Form = dr[15].ToString(); txtconDatefrom.Text = dat16Form;
                                var dat17To = dr[16].ToString(); txtConDateto.Text = dat17To;

                                var dat18CompanyName = dr[17].ToString(); txtbillComNameth.Text = dat18CompanyName;
                                var dat19Address = dr[18].ToString(); txtbillAddress.Text = dat19Address;
                                var dat20Tambon = dr[19].ToString(); txtbillTumbon.Text = dat20Tambon;
                                var dat21District = dr[20].ToString(); txtbillAumphur.Text = dat20Tambon;
                                var dat22Province = dr[21].ToString(); txtbillProvince.Text = dat22Province;
                                var dat23Postcode = dr[22].ToString(); txtbillPostCode.Text = dat23Postcode;
                                var dat24Tel = dr[23].ToString(); txtbillTel.Text = dat24Tel;
                                var dat25Fax = dr[24].ToString(); txtbillFax.Text = dat25Fax;
                                //var dat26EmailAddress = dr[25].ToString(); txtConBillEmail.Text = dat26EmailAddress;

                                var dat27Type = dr[26].ToString(); GetCodeText(dat27Type, DDtype);
                                var dat28PaymentType = dr[27].ToString(); GetCodeText(dat28PaymentType, RDPaymentType);
                                var dat29Amount = dr[28].ToString(); txtBillAmount.Text = dat29Amount;
                                var dat30Remark = dr[29].ToString(); txtBillRemark.Text = dat30Remark;

                                var dat31BillingMethod = dr[30].ToString(); GetCodeText(dat31BillingMethod, RDMbm_id);//****
                                var dat32CheckupRate = dr[31].ToString(); GetCodeText(dat32CheckupRate, RDmpr_id);
                                var dat33MainProgram = dr[32].ToString(); GetCodeText(dat33MainProgram, RDMpm_id);
                                var dat34OptionsItemsAsMentionedInQuotation = dr[33].ToString(); GetCodeText(dat34OptionsItemsAsMentionedInQuotation, RDmpq_id);
                                var dat35AmountInQuotation = dr[34].ToString(); txtmqp_credit.Text = dat35AmountInQuotation;
                                var dat36OptionsItemsAsMentionedNotInQuotation = dr[35].ToString(); GetCodeText(dat36OptionsItemsAsMentionedNotInQuotation, RDmpn_id);
                                var dat37AmountNotInQuotation = dr[36].ToString(); txtmpn_Credit.Text = dat37AmountNotInQuotation;
                                var dat38TermOfReceivingMedicine = dr[37].ToString(); GetCodeText(dat38TermOfReceivingMedicine, RDmrm_id);

                                var dat39MealCoupon = dr[38].ToString(); GetCodeText(dat39MealCoupon, RDCoupon);
                                var dat40Familyswelfare = dr[39].ToString(); txtFamilyWelfar.Text = dat40Familyswelfare;

                                var dat41Name = dr[40].ToString(); txt_req_doc.Text = dat41Name;


                                txtResultAddress.Text = dr[44].ToString();
                                txtResultTumbon.Text = dr[45].ToString();
                                txtResultAumphur.Text = dr[46].ToString();
                                txtResultProvince.Text = dr[47].ToString();
                                txtResultPostCode.Text = dr[48].ToString();
                            }

                            if (strCode == dat1No)
                            {
                                // Location
                                var countrow = (from t1 in contactdata
                                                where t1.Type == "L"
                                                && t1.Name == dr[42].ToString()
                                                select t1).Count();
                                if (countrow == 0)
                                {
                                    ClsConatactdata cdataLocation = new ClsConatactdata();
                                    cdataLocation.Type = "L";
                                    cdataLocation.Name = dr[42].ToString();
                                    string[] dta = dr[42].ToString().Split('=');
                                    int mcl_id = Convert1.ToInt32(dta[0].Trim());
                                    InhToDoListDataContext dbcx = new InhToDoListDataContext();
                                    var objlocation = (from t1 in dbcx.mst_checkup_locations
                                                       where t1.mcl_id == mcl_id
                                                       select t1).FirstOrDefault();
                                    if (objlocation != null)
                                    {
                                        cdataLocation.Tel = MargeString(objlocation.mcl_tname, objlocation.mcl_ename);
                                    }
                                    else
                                    {
                                        cdataLocation.Tel = "";
                                    }
                                    dbcx.Dispose();
                                    contactdata.Add(cdataLocation);
                                }
                                var dat44Remark = dr[43].ToString();
                                if (txttcdLocation_remark.Text == "")
                                {
                                    txttcdLocation_remark.Text = dat44Remark;
                                }
                                else
                                {
                                    txttcdLocation_remark.Text += "," + dat44Remark;
                                }

                                // Payor plan
                                //txtHiddenPayAgree //ไม่มีข้อมูลใน Excel

                                // Contact Bill
                                countrow = (from t1 in contactdata
                                            where t1.Type == "B"
                                            && t1.Name == dr[17].ToString()
                                            && t1.Tel == dr[23].ToString()
                                            && t1.Fax == dr[24].ToString()
                                            && t1.Email == dr[25].ToString()
                                            select t1).Count();
                                if (countrow == 0)
                                {
                                    if (dr[17].ToString() != "")
                                    {
                                        ClsConatactdata cdataBill = SetContact("B", dr[17].ToString(), dr[23].ToString(), dr[24].ToString(), dr[25].ToString());
                                        contactdata.Add(cdataBill);
                                    }
                                }
                                //Contact Mkt
                                countrow = (from t1 in contactdata
                                            where t1.Type == "M"
                                            && t1.Name == dr[56].ToString()
                                            && t1.Tel == dr[57].ToString()
                                            && t1.Fax == dr[58].ToString()
                                            && t1.Email == dr[59].ToString()
                                            select t1).Count();
                                if (countrow == 0)
                                {
                                    if (dr[56].ToString() != "")
                                    {
                                        ClsConatactdata cdataMkt = SetContact("M", dr[56].ToString(), dr[57].ToString(), dr[58].ToString(), dr[59].ToString());
                                        contactdata.Add(cdataMkt);
                                    }
                                }
                                //Contact person
                                countrow = (from t1 in contactdata
                                            where t1.Type == "C"
                                            && t1.Name == dr[11].ToString()
                                            && t1.Tel == dr[12].ToString()
                                            && t1.Fax == dr[13].ToString()
                                            && t1.Email == dr[14].ToString()
                                            select t1).Count();
                                if (countrow == 0)
                                {
                                    if (dr[11].ToString() != "")
                                    {
                                        ClsConatactdata cdataperson = SetContact("C", dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString());
                                        contactdata.Add(cdataperson);
                                    }
                                }
                                //Request doctor
                                var dat41Doctor_Cat = dr[41].ToString();
                                var dadoc = dat41Doctor_Cat.Split('=');
                                dat41Doctor_Cat = dadoc[0].Trim();

                                ClsConatactdata cdatadoccat = new ClsConatactdata();
                                cdatadoccat.Type = "D";
                                cdatadoccat.Name = dat41Doctor_Cat;
                                InhToDoListDataContext dbcx2 = new InhToDoListDataContext();
                                var objreqdoctor = (from t1 in dbcx2.mst_doctor_cats
                                                    where t1.mdc_id == Convert1.ToInt32(dat41Doctor_Cat)
                                                    select t1).FirstOrDefault();

                                if (objreqdoctor != null)
                                {
                                    cdatadoccat.Tel = MargeString(objreqdoctor.mdc_tname, objreqdoctor.mdc_ename);
                                }
                                else
                                {
                                    cdatadoccat.Tel = "";
                                }
                                dbcx2.Dispose();
                                contactdata.Add(cdatadoccat);

                                //**********************************************************************************************************************
                                //Medical Reports
                                var dat49MedicalReport = dr[49].ToString();
                                string[] da1 = dat49MedicalReport.Split('=');
                                dat49MedicalReport = da1[0].Trim();
                                foreach (ListItem item in CHMmmr_id.Items)
                                {
                                    if (item.Value.Trim() == dat49MedicalReport)//text or value (item.Value.Trim())
                                    {
                                        item.Selected = true;
                                    }
                                }
                                //send Report Method[ฉบับจริง]
                                var dat51SendReportMethod = dr[51].ToString();
                                da1 = dat51SendReportMethod.Split('=');
                                dat51SendReportMethod = da1[0].Trim();
                                if (dat51SendReportMethod == "N")
                                {
                                    RDtcd_send_rep_realN.Checked = true;
                                }
                                if (dat51SendReportMethod == "Y")
                                {
                                    RDtcd_send_rep_realY.Checked = true;
                                }
                                //กรณีไม่รับกลับ
                                var dat52send_rep_flag = dr[52].ToString();
                                da1 = dat52send_rep_flag.Split('=');
                                dat52send_rep_flag = da1[0].Trim();
                                if (dat52send_rep_flag == "H")
                                {
                                    RDtcd_send_rep_flagH.Checked = true;
                                }
                                if (dat52send_rep_flag == "C")
                                {
                                    RDtcd_send_rep_flagC.Checked = true;
                                }
                                //สำเนา
                                var dat53send_rep_Copy = dr[53].ToString();
                                da1 = dat53send_rep_Copy.Split('=');
                                dat53send_rep_Copy = da1[0].Trim();
                                GetCodeText(dat53send_rep_Copy, RDtcd_send_rep_copy);

                                //เงื่อนไขการเข้ารับบริการ
                                var dat54mcs = dr[54].ToString();
                                da1 = dat54mcs.Split('=');
                                dat54mcs = da1[0].Trim();
                                foreach (ListItem item in CHmcs_id_Employee.Items)
                                {
                                    if (item.Value.Trim() == dat54mcs)//text or value (item.Value.Trim())
                                    {
                                        item.Selected = true;
                                    }
                                }
                                var dat55mcs = dr[55].ToString();
                                da1 = dat55mcs.Split('=');
                                dat55mcs = da1[0].Trim();
                                foreach (ListItem item in CHmcs_id_Boss.Items)
                                {
                                    if (item.Value.Trim() == dat55mcs)//text or value (item.Value.Trim())
                                    {
                                        item.Selected = true;
                                    }
                                }


                            }
                            else
                            {
                                ShowContact(contactdata);
                                return;
                            }
                        }

                        if (dr[0].ToString() == "Code")
                        {
                            icount = true;
                        }

                    }
                    //Response.Write(rows.ToString());
                    if (icount == false) { lbmsgAlert.Text = "External table is not in the expected format."; return; }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "External table is not in the expected format.")
                {
                    lbmsgAlert.Text = "Please select valid excel sheet";
                }
                else lbmsgAlert.Text = "External table is not in the expected format";
            }

        }
        private ClsConatactdata SetContact(string Type,string Name, string tel, string Fax, string Email)
        {
            ClsConatactdata cdataperson = new ClsConatactdata();
            cdataperson.Type = Type;
            cdataperson.Name = Name;
            cdataperson.Tel = tel;
            cdataperson.Fax = Fax;
            cdataperson.Email = Email;
            return cdataperson;
        }
        private void ShowContact(List<ClsConatactdata> contactdata)
        {
            txtHiddenContact.Value = "";
            txtHiddenContactMkt.Value = "";
            txtHiddenContactBill.Value = "";
            txtHiddenSite.Value = "";
            txtHiddenDocCate.Value = "";
            if (ViewState["contact_personList"] == null)
                _objcontact_person = new List<AddConatactPerson>();
            else
                _objcontact_person = (List<AddConatactPerson>)ViewState["contact_personList"];
            if (ViewState["contact_billList"] == null)
                _objcontact_bill = new List<AddConatactPersonBill>();
            else
                _objcontact_bill = (List<AddConatactPersonBill>)ViewState["contact_billList"];
            if (ViewState["contact_MKTList"] == null)
                _objcontact_MKT = new List<AddConatactPersonMKT>();
            else
                _objcontact_MKT = (List<AddConatactPersonMKT>)ViewState["contact_MKTList"];

            foreach (ClsConatactdata item in contactdata)
            {
                if (item.Type == "D")
                {
                    //List Location
                    var strlocation = "{0},{1}";
                    if (txtHiddenDocCate.Value != "")
                    {
                        txtHiddenDocCate.Value += "|*|";
                    }
                    string[] locationName = item.Name.Split('=');
                    string idLocation = locationName[0].Trim();
                    txtHiddenDocCate.Value += string.Format(strlocation, idLocation, item.Tel);
                }
                if (item.Type == "L")
                {
                    //List Location
                    var strlocation = "{0},{1}";
                    if (txtHiddenSite.Value != "")
                    {
                        txtHiddenSite.Value += "|*|";
                    }
                    string[] locationName = item.Name.Split('=');
                    string idLocation = locationName[0].Trim();
                    txtHiddenSite.Value += string.Format(strlocation, idLocation, item.Tel);
                }
                if (item.Type == "C")
                {

                    _objcontact_person.Add(new AddConatactPerson { Name = item.Name, Tel = item.Tel, Fax = item.Fax, Email = item.Email });

                     //List Contact Person
                     var strcontactperson = "{0},{1},{2},{3}";
                     if (txtHiddenContact.Value != "")
                     {
                         txtHiddenContact.Value += "|*|";
                     }
                     txtHiddenContact.Value += string.Format(strcontactperson, item.Name, item.Tel, item.Fax, item.Email);
                }
                else if (item.Type == "M")
                {
                    _objcontact_MKT.Add(new AddConatactPersonMKT { Name = item.Name, Tel = item.Tel, Fax = item.Fax, Email = item.Email });
                    //List Contact MKT
                    var strcontactMkt = "{0},{1},{2},{3}";
                    if (txtHiddenContactMkt.Value != "")
                    {
                        txtHiddenContactMkt.Value += "|*|";
                    }
                    txtHiddenContactMkt.Value += string.Format(strcontactMkt, item.Name, item.Tel, item.Fax, item.Email);
                }
                else if (item.Type == "B")
                {
                    _objcontact_bill.Add(new AddConatactPersonBill { Name = item.Name, Tel = item.Tel, Fax = item.Fax, Email = item.Email });

                    //List Contact Bill
                    var strcontactBill = "{0},{1},{2},{3}";
                    if (txtHiddenContactBill.Value != "")
                    {
                        txtHiddenContactBill.Value += "|*|";
                    }
                    txtHiddenContactBill.Value += string.Format(strcontactBill, item.Name, item.Tel, item.Fax, item.Email);
                }

            }
            ViewState["contact_personList"] = _objcontact_person;
            ViewState["contact_billList"] = _objcontact_bill;
            ViewState["contact_MKTList"] = _objcontact_MKT;

            RepeaterContacrPerson.DataSource = _objcontact_person;
            RepeaterContacrPerson.DataBind();
            RepeaterContactMKT.DataSource = _objcontact_MKT;
            RepeaterContactMKT.DataBind();
            RepeaterContactBill.DataSource = _objcontact_bill;
            RepeaterContactBill.DataBind();


        }
        private void GetCodeText(string strdata, DropDownList DD)
        {
            string[] da1 = strdata.Split('=');
            try 
	        {
                //DD.Items.FindByText(strdata).Selected = true;
		        DD.SelectedValue= da1[0].Trim();
	        }
	        catch (Exception ex)
	        {
		        
	        }
        }
        private void GetCodeText(string strdata, RadioButtonList RD)
        {
            string[] da1 = strdata.Split('=');
            try
            {
                //RD.Items.FindByText(strdata).Selected = true;
                RD.SelectedValue= da1[0].Trim();
            }
            catch (Exception ex)
            {
                
            }
        }

        private string GetNameDocCate(InhToDoListDataContext dbc,int mdc_id)
        {
            string currentName = (from t1 in dbc.mst_doctor_cats
                                    where t1.mdc_id == mdc_id
                                    select t1.mdc_tname + " / " + t1.mdc_ename).FirstOrDefault();

            return currentName;
        }
        //Import รายชื่อผู้ตรวจ
        protected void btnImportExcelContactCheck_Click(object sender, EventArgs e)
        {
            if (FileUploadExcelContactCheck.PostedFile.FileName == String.Empty) { lblcheckfile.Text = "please select excel file."; lblcheckfile.Visible = true; return; } else { lblcheckfile.Visible = false; }
            
            if (FileUploadExcelContactCheck.HasFile == true)
            {
                string ContentType = Path.GetExtension(FileUploadExcelContactCheck.FileName);
                if (ContentType == ".xlsx" || ContentType == ".xls")
                {
                    TempUpload objTemp = new TempUpload(FileUploadExcelContactCheck);
                    string filePath = objTemp.SaveFile();
                    DataTable dt = new DataTable();
                    string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;TypeGuessRows=0;ImportMixedTypes=Text\"", Server.MapPath(filePath));
                    using (OleDbConnection dbConnection = new OleDbConnection(strConn))
                    {
                        using (OleDbDataAdapter dbAdapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", dbConnection)) //rename sheet if required!
                            dbAdapter.Fill(dt);
                        if (dt.Columns.Count != 19) 
                        {
                            lblcheckfile.Visible = true;
                            lblcheckfile.Text = "column not macth template excel file.";
                            return; 
                        }
                        bool isStart = false;
                        if (ViewState["NamecheckList"] == null)
                            objnchk_list = new List<Clstrn_name_check>();
                        else
                            objnchk_list = (List<Clstrn_name_check>)ViewState["NamecheckList"];

                        foreach (DataRow dr in dt.Rows)
                        {


                            if (isStart && dr[0].ToString().Trim() != "" && dr[3].ToString().Trim() != "" && dr[4].ToString().Trim() != "")
                            {   //ส่วนที่เป็นข้อมูล
                                string tnc_emp_id = dr[1].ToString();
                                string tnc_title_name = dr[2].ToString();
                                string tnc_fname = dr[3].ToString().Trim();
                                string tnc_lname = dr[4].ToString().Trim();
                                string tnc_program = dr[5].ToString();
                                string tnc_option = dr[6].ToString();
                                string tnc_remark = dr[7].ToString();
                                char? tnc_gender = Convert1.ToChar(dr[8]);

                                string tnc_legal = dr[9].ToString();
                                string tnc_company_name = dr[10].ToString();
                                string tnc_site = dr[11].ToString();
                                string tnc_department = dr[12].ToString();
                                string tnc_position = dr[13].ToString();
                                string tnc_personal_id = dr[14].ToString();
                                string tnc_hn = dr[15].ToString();

                                DateTime? tnc_dob = null;
                                if (dr[16].ToString() != null) { Constant.ConvertStringToDate(dr[16].ToString()); }

                                string tnc_age = dr[17].ToString();
                                string tnc_appoint_date = dr[18].ToString().Replace('.', ':');

                                var objcurrent = (from t1 in objnchk_list
                                                  where t1.tnc_fname == tnc_fname
                                                  && t1.tnc_lname==tnc_lname
                                                  select t1).FirstOrDefault();
                                if (objcurrent != null)
                                { //กรณีที่ tnc_personal_id มีอยู่แล้ว
                                    objcurrent.tnc_emp_id = tnc_emp_id;
                                    objcurrent.tnc_title_name = tnc_title_name;
                                    objcurrent.tnc_fname = tnc_fname;
                                    objcurrent.tnc_lname = tnc_lname;
                                    objcurrent.tnc_program = tnc_program;
                                    objcurrent.tnc_option = tnc_option;
                                    objcurrent.tnc_remark = tnc_remark;
                                    objcurrent.tnc_gender = tnc_gender;

                                    objcurrent.tnc_legal = tnc_legal;
                                    objcurrent.tnc_company_name = tnc_company_name;
                                    objcurrent.tnc_site = tnc_site;
                                    objcurrent.tnc_department = tnc_department;
                                    objcurrent.tnc_position = tnc_position;
                                    objcurrent.tnc_personal_id = tnc_personal_id;
                                    objcurrent.tnc_hn = tnc_hn;
                                    objcurrent.tnc_dob = tnc_dob;
                                    objcurrent.tnc_age = tnc_age;
                                    objcurrent.tnc_appoint_date = tnc_appoint_date;
                                }
                                else
                                {
                                    Clstrn_name_check objnchk = new Clstrn_name_check();
                                    objnchk.tnc_emp_id = tnc_emp_id;
                                    objnchk.tnc_title_name = tnc_title_name;
                                    objnchk.tnc_fname = tnc_fname;
                                    objnchk.tnc_lname = tnc_lname;
                                    objnchk.tnc_program = tnc_program;
                                    objnchk.tnc_option = tnc_option;
                                    objnchk.tnc_remark = tnc_remark;
                                    objnchk.tnc_gender = tnc_gender;

                                    objnchk.tnc_legal = tnc_legal;
                                    objnchk.tnc_company_name = tnc_company_name;
                                    objnchk.tnc_site = tnc_site;
                                    objnchk.tnc_department = tnc_department;
                                    objnchk.tnc_position = tnc_position;
                                    objnchk.tnc_personal_id = tnc_personal_id;
                                    objnchk.tnc_hn = tnc_hn;
                                    objnchk.tnc_dob = tnc_dob;
                                    objnchk.tnc_age = tnc_age;
                                    objnchk.tnc_appoint_date = tnc_appoint_date;
                                    objnchk_list.Add(objnchk);
                                }
                            }
                            if (dr[0].ToString().ToLower() == "no.")
                            {
                                isStart = true;
                            }
                            if (dr[0].ToString().Trim() == "" || (dr[3].ToString().Trim() == "" && dr[4].ToString().Trim() == ""))
                            {
                                isStart = false;
                            }
                        }
                        ViewState["NamecheckList"] = objnchk_list;

                        //dt.Rows.RemoveAt(0);
                        RepeaterPatient.DataSource = objnchk_list.ToList();
                        RepeaterPatient.DataBind();
                        //this.ListNameCheck();
                    }
                }
                else
                {
                    lblcheckfile.Visible = true;
                    lblcheckfile.Text = "please select file .xlsx, .xls only.";
                    return;
                }
                
            }
        }

        //Upload File
        protected void btnAddFile_Click(object sender, EventArgs e)
        {
            

            lbmsgAlert.Text = "";
            lbmsgbox.InnerHtml = "";

            if (txt_company_code.Text.Trim() == "")
            {
                lbmsgAlert.Text = "Please input company code.";
                TabContainer1.ActiveTabIndex = 0;
                txt_company_code.Focus();
                return;
            }
            try
            {
                //if (!fileUpload.HasFile)
                //{
                //    lbmsgbox.InnerHtml = "Please select file.";
                //    return;
                //};

                //check file size
                decimal kb_size = AttachfileSite.PostedFile.ContentLength;
                decimal mb_size = kb_size / 1048576;
                if (mb_size > 3) { lbmsgbox.InnerHtml = "OverSize Limit 3MB. Uploaded File Size is " + String.Format("{0:N2}",mb_size) + "MB."; return; }

                //สร้าง Temp เพื่อเก็บไฟล์ก่อนการ บันทึกจริงๆ
                TempUpload objTemp = new TempUpload(AttachfileSite);
                string filePath = objTemp.SaveFile();
                string strDateTime = DateTime.Now.ToString("yyMMddHHmmss");
                string fileName = string.Format("{0}{1}", strDateTime, AttachfileSite.FileName);
                string showFileName = AttachfileSite.FileName;
                string PathTypeFile = txt_company_code.Text.Trim().Replace(" ","") ;

                var  CreateFolder=string.Format(Constant.pathserverUpload, PathTypeFile);
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(String.Format("{0}", CreateFolder))))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(String.Format("{0}", CreateFolder)));
                }

                string attachFile = string.Format(Constant.pathserverUpload, PathTypeFile + "/" + fileName.Replace("+","_"));//path file รวมfolder Type แล้ว
                Stream stream = AttachfileSite.PostedFile.InputStream;
                
                string typefilename = PathTypeFile.Replace("/", "");
                bool chHPC = false;
                bool chMKT = false;
                bool chCLT = false;
                bool chCTC = false;
                foreach (ListItem item in ch_typeAttachfile.Items)
                {
                    if (item.Selected == true)//text or value (item.Value.Trim())
                    {
                        if (item.Value == "MKT")
                        {
                            chMKT = true;
                        }
                        if (item.Value == "HPC")
                        {
                            chHPC = true;
                        }
                        if (item.Value == "CLT")
                        {
                            chCLT = true;
                        }
                        if (item.Value == "CTC")
                        {
                            chCTC = true;
                        }
                    }
                }


                if (ViewState["AttachFileList"] == null)
                    _docfileList = new List<PalliativeDocFileEntity>();
                else
                    _docfileList = (List<PalliativeDocFileEntity>)ViewState["AttachFileList"];

                byte[] itemarry=ReadToEnd(stream);
                _docfileList.Add(new PalliativeDocFileEntity()
                {
                    RowID = _docfileList.Count + 1,
                    TypeMkt = chMKT,
                    TypeContact = chCTC,
                    TypeCollection = chCLT,
                    TypeHPC = chHPC,
                    file_name = showFileName,
                    attach_file = filePath,

                    fileStream = itemarry,
                    UpdateDate = DateTime.Now,
                    UpdateBy = Constant.CurrentUserLogin,
                    Status = "N",
                    pathReal = attachFile
                });
                ViewState["AttachFileList"] = _docfileList;
                BindUpload(_docfileList);
                //ClearAddNewNameCheck ch_typeAttachfile
                foreach (ListItem item in ch_typeAttachfile.Items)
                {
                    item.Selected = false;
                }
            }
            catch (Exception ex)
            {
                lbmsgAlert.Text = ex.Message;
            }
        }
      
        protected void BindUpload(List<PalliativeDocFileEntity> datalist)
        {
            gnvAttachFile.DataSource = datalist.Where(x=>(x.Status=="O" || x.Status=="N"));
            gnvAttachFile.DataBind();
        }
        private bool WriteFile(string filePath, Stream stream)
        {
            string serverpath = Server.MapPath(filePath);
            try
            {
                using (FileStream fileStream = System.IO.File.Create(serverpath, (int)stream.Length))
                {
                    // Fill the bytes[] array with the stream data
                    byte[] bytesInStream = new byte[stream.Length];
                    stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                    // Use FileStream object to write to the specified file
                    fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
      //**************
        protected void btnSave_Click(object sender, EventArgs e)
        {
            

            int tcd_id = Convert1.ToInt32(Request.QueryString["id"]);

            //--- HDIsCopy -เป็นการกดปุ่ม Copy หรือไม่
            //if (tcd_id > 0 && HDIsCopy.Value=="0")
            //{
            //    if(saveEditData(tcd_id))
            //    {
            //        Response.Redirect("frmmktdata.aspx?status=2");
            //    }

            //}else
            //{
            //    if (txt_company_code.Text.Trim() == "")
            //    {
            //        lbmsgAlert.Text = "Company Code is Require!";
            //        Tab1_Click(null, null);
            //        txt_company_code.Focus();
            //        return;
            //    }

            //    using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            //    {
            //        var isCountcurrent = (from t1 in dbc.trn_company_details
            //                              where t1.tcd_code == txt_company_code.Text.Trim() && t1.tcd_document_no == txtdoccode.Text.Trim()
            //                              select t1).Count();
            //        if (isCountcurrent > 0) { lbmsgAlert.Text = "Can't save this document no."; return; }
            //    }
                if (saveData(tcd_id))
                {
                    lbmsgAlert.Text = "Save data completed.";
                    lbmsgAlert.Focus();

                    ClientScript.RegisterStartupScript(typeof(Page), "SymbolError", "<script type='text/javascript'>parent.frm1.location.reload();</script>");
                }
           // }
       }
        private bool saveData(int tcdid)
        {
            try
            {
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    DateTime dtnow = funcCls.GetServerDateTime();
                    trn_company_detail tcd = new trn_company_detail();

                    //tcd.tcd_id= tcdid;
                    tcd.tcd_year = Convert1.ToInt32( DDCompanyYear.SelectedValue);
                    tcd.tcd_code=txt_company_code.Text;

                    if (txtdoccode.Text == "") { tcd.tcd_document_no = RunningDocumentCode(); } else { tcd.tcd_document_no = txtdoccode.Text; }

                    tcd.tcd_tname=txtComNameTH.Text;
                    tcd.tcd_ename=txtComNameEng.Text;
                    tcd.tcd_legal = txtlegal.Text;
                    tcd.tcd_type = rdona.Checked == true ? rdona.Text : rdojms.Text;
                    tcd.tcd_address = txtComAddress.Text;
                    tcd.tcd_tambon=txtComTumbon.Text;
                    tcd.tcd_district=txtComAumphur.Text;
                    tcd.tcd_province=txtComProvince.Text;
                    tcd.tcd_postcode=txtComPostCode.Text;
                    tcd.tcd_tel=txtComTel.Text;
                    tcd.tcd_payor = txt_payor.Text;

                    //tcd.tcd_mobile="";
                    tcd.tcd_fax=txtComFax.Text;
                    tcd.tcd_email=txtComEmail.Text;

                    tcd.tcd_date_from = Constant.ConvertStringToDate(txtconDatefrom.Text);
                    tcd.tcd_date_to = Constant.ConvertStringToDate(txtConDateto.Text);

                    tcd.tcd_bill_company=txtbillComNameth.Text;
                    tcd.tcd_bill_address=txtbillAddress.Text;
                    tcd.tcd_bill_tambon=txtbillTumbon.Text;
                    tcd.tcd_bill_district=txtbillAumphur.Text;
                    tcd.tcd_bill_province=txtbillProvince.Text;
                    tcd.tcd_bill_postcode=txtbillPostCode.Text;
                    tcd.tcd_bill_tel=txtbillTel.Text;
                    //tcd.tcd_bill_mobile="";
                    tcd.tcd_bill_fax=txtbillFax.Text;

                    tcd.tcd_family_welfare = txtFamilyWelfar.Text;
                    //tcd.tcd_doc_code="";
                    //tcd.tcd_doc_fname="";
                    //tcd.tcd_doc_tname="";
                    tcd.tcd_location_remark = txttcdLocation_remark.Text;
                    tcd.tcd_result_address = txtResultAddress.Text;
                    tcd.tcd_result_tambon = txtResultTumbon.Text;
                    tcd.tcd_result_district = txtResultAumphur.Text;
                    tcd.tcd_result_province = txtResultProvince.Text;
                    tcd.tcd_result_postcode = txtResultPostCode.Text;
                    tcd.tcd_send_rep_real = (RDtcd_send_rep_realY.Checked) ? "Y" :( (RDtcd_send_rep_realN.Checked)?"N":"");
                    tcd.tcd_send_rep_flag=(RDtcd_send_rep_flagH.Checked)?"H":((RDtcd_send_rep_flagC.Checked)?"C":"");
                    tcd.tcd_send_rep_copy = RDtcd_send_rep_copy.SelectedValue;
                    
                    //Medical Report
                    int i = 0;
                    tcd.trn_medical_reports.Clear();
                    foreach (ListItem item in CHMmmr_id.Items)
                    {
                        if (item.Selected == true)//text or value (item.Value.Trim())
                        {
                            int mmr_id = Convert1.ToInt32(item.Value.Trim());
                            trn_medical_report newitem = new trn_medical_report();
                            newitem.mmr_id = mmr_id;
                            newitem.mul_user_login = Constant.CurrentUserLogin;
                            newitem.tmr_create_by = Constant.CurrentUserLogin;
                            newitem.tmr_create_date = funcCls.GetServerDateTime();
                            //newitem.tmr_id = 0;
                            if (i == CHMmmr_id.Items.Count - 1)
                            {
                                newitem.tmr_rep_remark = txttcd_rep_remark.Text;
                            }
                            else
                            {
                                newitem.tmr_rep_remark = "";
                            }
                            newitem.tmr_update_date = funcCls.GetServerDateTime();
                            //newitem.tcd_id = 0;
                            tcd.trn_medical_reports.Add(newitem);
                        }
                        i = i+1;
                    }
                    
                    //tcd.mcs_id=0;
                    tcd.tcd_remark = Server.HtmlEncode(EditorReamrk.Content); //txtRemark.Text;
                    tcd.tcd_create_by = Constant.CurrentUserLogin;
                    tcd.tcd_create_date = dtnow;
                    tcd.mul_user_login = Constant.CurrentUserLogin;
                    tcd.tcd_update_date = dtnow;
                   

                    // save Contact B=Bill, C=Person, M=Markeing,P=Payor
                    //txtHiddenContact

                    //string[] strSeparatorsRow = new string[] { "|*|" };//row
                    tcd.trn_contact_persons.Clear();
                    if (ViewState["contact_personList"] == null)
                        _objcontact_person = new List<AddConatactPerson>();
                    else
                        _objcontact_person = (List<AddConatactPerson>)ViewState["contact_personList"];

                    //string[] dr = txtHiddenContact.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var strItem in _objcontact_person)
                    {
                        //string[] datacol = strItem.Split(',');//col
                        trn_contact_person contactnew = setContact(strItem.Name, strItem.Tel, strItem.Fax, strItem.Email, 'C');
                        tcd.trn_contact_persons.Add(contactnew);
                    }

                    //ContactBill
                    if (ViewState["contact_billList"] == null)
                        _objcontact_bill = new List<AddConatactPersonBill>();
                    else
                        _objcontact_bill = (List<AddConatactPersonBill>)ViewState["contact_billList"];

                    foreach (var strItem in _objcontact_bill)
                    {
                        //string[] datacol = strItem.Split(',');//col
                        trn_contact_person contactnew = setContact(strItem.Name, strItem.Tel, strItem.Fax, strItem.Email, 'B');
                        tcd.trn_contact_persons.Add(contactnew);
                    }

                    //Maketing
                    if (ViewState["contact_MKTList"] == null)
                        _objcontact_MKT = new List<AddConatactPersonMKT>();
                    else
                        _objcontact_MKT = (List<AddConatactPersonMKT>)ViewState["contact_MKTList"];

                    foreach (var strItem in _objcontact_MKT)
                    {
                        //string[] datacol = strItem.Split(',');//col
                        trn_contact_person contactnew = setContact(strItem.Name, strItem.Tel, strItem.Fax, strItem.Email, 'M');
                        tcd.trn_contact_persons.Add(contactnew);
                    }
                    
                    //trn_plan
                    //dr = txtHiddenPayAgree.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //tcd.trn_plans.Clear();
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_plan planNew = new trn_plan();
                    //    planNew.tpl_code = datacol[0].Replace("|x|", ",");
                    //    planNew.tpl_name = datacol[1].Replace("|x|", ",");
                    //    planNew.tpl_create_by = Constant.CurrentUserLogin;
                    //    planNew.tpl_create_date = funcCls.GetServerDateTime();
                    //    planNew.mul_user_login = Constant.CurrentUserLogin;
                    //    planNew.tpl_update_date = funcCls.GetServerDateTime();
                    //    tcd.trn_plans.Add(planNew);
                    //}

                    dbc.trn_plans.DeleteAllOnSubmit(tcd.trn_plans);
                    if (ViewState["AddPlanList"] == null)
                        _objplan = new List<AddPlan>();
                    else
                        _objplan = (List<AddPlan>)ViewState["AddPlanList"];

                    foreach (var strItem in _objplan)
                    {
                        trn_plan planNew = new trn_plan();
                        planNew.tpl_code = strItem.tpl_code;
                        planNew.tpl_name = strItem.tpl_name;
                        planNew.tpl_create_by = Constant.CurrentUserLogin;
                        planNew.tpl_create_date = funcCls.GetServerDateTime();
                        planNew.mul_user_login = Constant.CurrentUserLogin;
                        planNew.tpl_update_date = funcCls.GetServerDateTime();
                        tcd.trn_plans.Add(planNew);
                    }




                    //Company Request Doctor
                    dbc.trn_company_request_doctors.DeleteAllOnSubmit(tcd.trn_company_request_doctors);
                    if (txt_req_doc.Text.Trim() != "")
                    {
                        string[] companyDoctor = txt_req_doc.Text.Split('/');
                        trn_company_request_doctor nitem = new trn_company_request_doctor();
                        nitem.tcd_id = tcd.tcd_id;
                        
                        if (companyDoctor.Count() > 0) nitem.tcr_doc_code = companyDoctor[0];
                        if (companyDoctor.Count() > 1) nitem.tcr_doc_ename = companyDoctor[2];
                        if (companyDoctor.Count() > 2) nitem.tcr_doc_tname = companyDoctor[1];
                        nitem.tcr_create_by = Constant.CurrentUserLogin;
                        nitem.tcr_create_date = funcCls.GetServerDateTime();
                        nitem.mul_user_login = Constant.CurrentUserLogin;
                        nitem.tcr_update_date = funcCls.GetServerDateTime();
                        tcd.trn_company_request_doctors.Add(nitem);
                    }
                    //company request doctor Cats
                    //dr = txtHiddenDocCate.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //dbc.trn_company_doctor_cats.DeleteAllOnSubmit(tcd.trn_company_doctor_cats);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_company_doctor_cat docCatNew = new trn_company_doctor_cat();
                    //    docCatNew.mdc_id = Convert1.ToInt32(datacol[0]);
                    //    docCatNew.tcdc_create_by = Constant.CurrentUserLogin;
                    //    docCatNew.tcdc_create_date = funcCls.GetServerDateTime();
                    //    docCatNew.mul_user_login = Constant.CurrentUserLogin;
                    //    docCatNew.tcdc_update_date = funcCls.GetServerDateTime();
                    //    tcd.trn_company_doctor_cats.Add(docCatNew);
                    //}


                    dbc.trn_company_doctor_cats.DeleteAllOnSubmit(tcd.trn_company_doctor_cats);
                    if (ViewState["objdoccateList"] == null)
                        _objdoccate = new List<AddDocCate>();
                    else
                        _objdoccate = (List<AddDocCate>)ViewState["objdoccateList"];

                    foreach (var strItem in _objdoccate)
                    {
                        trn_company_doctor_cat docCatNew = new trn_company_doctor_cat();
                        docCatNew.mdc_id = Convert1.ToInt32(strItem.mdc_id);
                        docCatNew.tcdc_create_by = Constant.CurrentUserLogin;
                        docCatNew.tcdc_create_date = funcCls.GetServerDateTime();
                        docCatNew.mul_user_login = Constant.CurrentUserLogin;
                        docCatNew.tcdc_update_date = funcCls.GetServerDateTime();
                        tcd.trn_company_doctor_cats.Add(docCatNew);
                    }



                    //CheckUp Loacation
                    dbc.trn_checkup_locations.DeleteAllOnSubmit(tcd.trn_checkup_locations);
                    if (ViewState["objSiteList"] == null)
                        _objsite = new List<AddSite>();
                    else
                        _objsite = (List<AddSite>)ViewState["objSiteList"];

                    foreach (var strItem in _objsite)
                    {
                        trn_checkup_location locationNew = new trn_checkup_location();
                        locationNew.mcl_id = Convert1.ToInt32(strItem.mcl_id);
                        locationNew.tcl_create_by = Constant.CurrentUserLogin;
                        locationNew.tcl_create_date = funcCls.GetServerDateTime();
                        locationNew.mul_user_login = Constant.CurrentUserLogin;
                        locationNew.tcl_update_date = funcCls.GetServerDateTime();
                        tcd.trn_checkup_locations.Add(locationNew);
                    }

                    //dr = txtHiddenSite.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //dbc.trn_checkup_locations.DeleteAllOnSubmit(tcd.trn_checkup_locations);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_checkup_location locationNew = new trn_checkup_location();
                    //    locationNew.mcl_id = Convert1.ToInt32(datacol[0]);
                    //    locationNew.tcl_create_by = Constant.CurrentUserLogin;
                    //    locationNew.tcl_create_date = funcCls.GetServerDateTime();
                    //    locationNew.mul_user_login = Constant.CurrentUserLogin;
                    //    locationNew.tcl_update_date=funcCls.GetServerDateTime();
                    //    tcd.trn_checkup_locations.Add(locationNew);
                    //}

                    //***********  payment Type  **************************************************************
                    SetPaymentTypeSave(ref tcd, dbc);

                    //Save Upload File *****************************
                    if (ViewState["AttachFileList"] == null)
                        _docfileList = new List<PalliativeDocFileEntity>();
                    else
                        _docfileList = (List<PalliativeDocFileEntity>)ViewState["AttachFileList"];
                    if (_docfileList != null)
                    {
                        if (this.SaveUploadfile(dbc,ref tcd)==false)
                        {
                            return false;
                        }
                    }

                    //Condition service เงื่อนไขการเข้ารับบริการ
                    foreach (ListItem item in CHmcs_id_Employee.Items)
                    {
                        if (item.Selected == true)//text or value (item.Value.Trim())
                        {
                            trn_condition_service connew = new trn_condition_service();
                            connew.mcs_id=Convert1.ToInt32(item.Value.Trim());
                            connew.tcs_type = "EM";
                            connew.tcs_create_by = Constant.CurrentUserLogin;
                            connew.tcs_create_date = funcCls.GetServerDateTime();
                            connew.mul_user_login = Constant.CurrentUserLogin;
                            connew.tcs_update_date = funcCls.GetServerDateTime();
                            tcd.trn_condition_services.Add(connew);
                        }
                    }
                    //Condition service เงื่อนไขการเข้ารับบริการ
                    foreach (ListItem item in CHmcs_id_Boss.Items)
                    {
                        if (item.Selected == true)//text or value (item.Value.Trim())
                        {
                            trn_condition_service connew = new trn_condition_service();
                            connew.mcs_id = Convert1.ToInt32(item.Value.Trim());
                            connew.tcs_type = "EX";
                            connew.tcs_create_by = Constant.CurrentUserLogin;
                            connew.tcs_create_date = funcCls.GetServerDateTime();
                            connew.mul_user_login = Constant.CurrentUserLogin;
                            connew.tcs_update_date = funcCls.GetServerDateTime();
                            tcd.trn_condition_services.Add(connew);
                        }
                    }

                    //Package add

                    if (ViewState["PackageDetailList"] == null)
                        _packagedetail = new List<PackageDetailList>();
                    else
                        _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];

                    if (_packagedetail != null)
                    {
                        if (SavePackageItem(dbc, ref tcd) == false)
                        {
                            return false;
                        }
                        
                    }
                     //tab4 รายชื่อผู้ตรวจ
                    InsertAndUpdateNameCheck(dbc,ref tcd);

                    dbc.trn_company_details.InsertOnSubmit(tcd);

                    txtdoccode.Text = tcd.tcd_document_no;

                    dbc.SubmitChanges();

                    

                    return true;
                }
            }
            catch(Exception ex)
            {
                lbmsgAlert.Text = ex.Message;
                return false;
            }
        }
        private bool saveEditData(int tcd_id)
        {
            try
            {
                using (InhToDoListDataContext dbc = new InhToDoListDataContext())
                {
                    DateTime dtnow = funcCls.GetServerDateTime();

                    trn_company_detail tcd = (from t1 in dbc.trn_company_details
                                              where t1.tcd_id == tcd_id
                                              select t1).FirstOrDefault();
                    //tcd.tcd_id="";
                    tcd.tcd_year = Convert1.ToInt32(DDCompanyYear.SelectedValue);
                    tcd.tcd_code = txt_company_code.Text;
                    tcd.tcd_tname = txtComNameTH.Text;
                    tcd.tcd_ename = txtComNameEng.Text;
                    tcd.tcd_legal = txtlegal.Text;
                    tcd.tcd_address = txtComAddress.Text;
                    tcd.tcd_tambon = txtComTumbon.Text;
                    tcd.tcd_district = txtComAumphur.Text;
                    tcd.tcd_province = txtComProvince.Text;
                    tcd.tcd_postcode = txtComPostCode.Text;
                    tcd.tcd_tel = txtComTel.Text;
                    //tcd.tcd_mobile="";
                    tcd.tcd_fax = txtComFax.Text;
                    tcd.tcd_email = txtComEmail.Text;

                    tcd.tcd_date_from = Constant.ConvertStringToDate(txtconDatefrom.Text);
                    tcd.tcd_date_to = Constant.ConvertStringToDate(txtConDateto.Text);

                    tcd.tcd_bill_company = txtbillComNameth.Text;
                    tcd.tcd_bill_address = txtbillAddress.Text;
                    tcd.tcd_bill_tambon = txtbillTumbon.Text;
                    tcd.tcd_bill_district = txtbillAumphur.Text;
                    tcd.tcd_bill_province = txtbillProvince.Text;
                    tcd.tcd_bill_postcode = txtbillPostCode.Text;
                    tcd.tcd_bill_tel = txtbillTel.Text;
                    //tcd.tcd_bill_mobile="";
                    tcd.tcd_bill_fax = txtbillFax.Text;
                    
                    tcd.tcd_family_welfare = txtFamilyWelfar.Text;
                    //tcd.tcd_doc_code="";
                    //tcd.tcd_doc_fname="";
                    //tcd.tcd_doc_tname="";

                    tcd.tcd_payor = txt_payor.Text;

                    tcd.tcd_location_remark = txttcdLocation_remark.Text;
                    tcd.tcd_result_address = txtResultAddress.Text;
                    tcd.tcd_result_tambon = txtResultTumbon.Text;
                    tcd.tcd_result_district = txtResultAumphur.Text;
                    tcd.tcd_result_province = txtResultProvince.Text;
                    tcd.tcd_result_postcode = txtResultPostCode.Text;
                    tcd.tcd_send_rep_real = (RDtcd_send_rep_realY.Checked) ? "Y" : ((RDtcd_send_rep_realN.Checked) ? "N" : "");
                    tcd.tcd_send_rep_flag = (RDtcd_send_rep_flagH.Checked) ? "H" : ((RDtcd_send_rep_flagC.Checked) ? "C" : "");
                    tcd.tcd_send_rep_copy = RDtcd_send_rep_copy.SelectedValue;

                    //tcd.mmr_id = Convert1.ToInt32(CHMmmr_id.SelectedValue);
                    //tcd.tcd_rep_remark = txttcd_rep_remark.Text;
                    //Medical Report
                    int i = 0;
                    dbc.trn_medical_reports.DeleteAllOnSubmit(tcd.trn_medical_reports);
                    foreach (ListItem item in CHMmmr_id.Items)
                    {
                        if (item.Selected == true)//text or value (item.Value.Trim())
                        {
                            int mmr_id = Convert1.ToInt32(item.Value.Trim());
                            trn_medical_report newitem = new trn_medical_report();
                            newitem.mmr_id = mmr_id;
                            newitem.mul_user_login = Constant.CurrentUserLogin;
                            newitem.tmr_create_by = Constant.CurrentUserLogin;
                            newitem.tmr_create_date = funcCls.GetServerDateTime();
                            //newitem.tmr_id = 0;
                            if (i == CHMmmr_id.Items.Count - 1)
                            {
                                newitem.tmr_rep_remark = txttcd_rep_remark.Text;
                            }
                            else
                            {
                                newitem.tmr_rep_remark = "";
                            }
                            newitem.tmr_update_date = funcCls.GetServerDateTime();
                            //newitem.tcd_id = 0;
                            tcd.trn_medical_reports.Add(newitem);
                        }
                        i = i + 1;
                    }

                    //tcd.mcs_id=0;
                    tcd.tcd_remark = Server.HtmlEncode(EditorReamrk.Content);
                    tcd.tcd_create_by = Constant.CurrentUserLogin;
                    tcd.tcd_create_date = dtnow;
                    tcd.mul_user_login = Constant.CurrentUserLogin;
                    tcd.tcd_update_date = dtnow;

                    //// save Contact B=Bill, C=Person, M=Markeing,P=Payor
                    ////txtHiddenContact
                    //string[] strSeparatorsRow = new string[] { "|*|" };//row
                    //var contactpersons = (from t1 in dbc.trn_contact_persons
                    //                      where t1.tcd_id == tcd.tcd_id
                    //                      select t1);
                    //dbc.trn_contact_persons.DeleteAllOnSubmit(contactpersons);
                    //string[] dr = txtHiddenContact.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_contact_person contactnew = setContact(datacol[0], datacol[1], datacol[2], datacol[3], 'C');
                    //    tcd.trn_contact_persons.Add(contactnew);
                    //}
                    ////ContactBill
                    //dr = txtHiddenContactBill.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_contact_person contactnew = setContact(datacol[0], datacol[1], datacol[2], datacol[3], 'B');
                    //    tcd.trn_contact_persons.Add(contactnew);
                    //}
                    ////Maketing
                    //dr = txtHiddenContactMkt.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_contact_person contactnew = setContact(datacol[0], datacol[1], datacol[2], datacol[3], 'M');
                    //    tcd.trn_contact_persons.Add(contactnew);
                    //}

                    var contactpersons = (from t1 in dbc.trn_contact_persons
                                          where t1.tcd_id == tcd.tcd_id
                                          select t1);
                    dbc.trn_contact_persons.DeleteAllOnSubmit(contactpersons);
                    if (ViewState["contact_personList"] == null)
                        _objcontact_person = new List<AddConatactPerson>();
                    else
                        _objcontact_person = (List<AddConatactPerson>)ViewState["contact_personList"];

                    //string[] dr = txtHiddenContact.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var strItem in _objcontact_person)
                    {
                        //string[] datacol = strItem.Split(',');//col
                        trn_contact_person contactnew = setContact(strItem.Name, strItem.Tel, strItem.Fax, strItem.Email, 'C');
                        tcd.trn_contact_persons.Add(contactnew);
                    }

                    //ContactBill
                    if (ViewState["contact_billList"] == null)
                        _objcontact_bill = new List<AddConatactPersonBill>();
                    else
                        _objcontact_bill = (List<AddConatactPersonBill>)ViewState["contact_billList"];

                    foreach (var strItem in _objcontact_bill)
                    {
                        //string[] datacol = strItem.Split(',');//col
                        trn_contact_person contactnew = setContact(strItem.Name,strItem.Tel, strItem.Fax,  strItem.Email, 'B');
                        tcd.trn_contact_persons.Add(contactnew);
                    }

                    //Maketing
                    if (ViewState["contact_MKTList"] == null)
                        _objcontact_MKT = new List<AddConatactPersonMKT>();
                    else
                        _objcontact_MKT = (List<AddConatactPersonMKT>)ViewState["contact_MKTList"];

                    foreach (var strItem in _objcontact_MKT)
                    {
                        //string[] datacol = strItem.Split(',');//col
                        trn_contact_person contactnew = setContact(strItem.Name,strItem.Tel, strItem.Fax,  strItem.Email, 'M');
                        tcd.trn_contact_persons.Add(contactnew);
                    }




                    //trn_plan
                    //dr = txtHiddenPayAgree.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //dbc.trn_plans.DeleteAllOnSubmit(tcd.trn_plans);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_plan planNew = new trn_plan();
                    //    planNew.tpl_code = datacol[0].Replace("|x|", ",");
                    //    planNew.tpl_name = datacol[1].Replace("|x|", ",");
                    //    planNew.tpl_create_by = Constant.CurrentUserLogin;
                    //    planNew.tpl_create_date = funcCls.GetServerDateTime();
                    //    planNew.mul_user_login = Constant.CurrentUserLogin;
                    //    planNew.tpl_update_date = funcCls.GetServerDateTime();
                    //    tcd.trn_plans.Add(planNew);
                    //}

                    dbc.trn_plans.DeleteAllOnSubmit(tcd.trn_plans);
                    if (ViewState["AddPlanList"] == null)
                        _objplan = new List<AddPlan>();
                    else
                        _objplan = (List<AddPlan>)ViewState["AddPlanList"];

                    foreach (var strItem in _objplan)
                    {
                        trn_plan planNew = new trn_plan();
                        planNew.tpl_code = strItem.tpl_code;
                        planNew.tpl_name = strItem.tpl_name;
                        planNew.tpl_create_by = Constant.CurrentUserLogin;
                        planNew.tpl_create_date = funcCls.GetServerDateTime();
                        planNew.mul_user_login = Constant.CurrentUserLogin;
                        planNew.tpl_update_date = funcCls.GetServerDateTime();
                        tcd.trn_plans.Add(planNew);
                    }

                    //Company Request Doctor
                    dbc.trn_company_request_doctors.DeleteAllOnSubmit(tcd.trn_company_request_doctors);
                    if (txt_req_doc.Text.Trim() != "")
                    {
                        string[] companyDoctor = txt_req_doc.Text.Split('/');
                        trn_company_request_doctor nitem = new trn_company_request_doctor();
                        if (companyDoctor.Count() > 0) nitem.tcr_doc_code = companyDoctor[0];
                        if (companyDoctor.Count() > 1) nitem.tcr_doc_tname = companyDoctor[1];
                        if (companyDoctor.Count() > 2) nitem.tcr_doc_ename = companyDoctor[2];
                        nitem.tcr_create_by = Constant.CurrentUserLogin;
                        nitem.tcr_create_date = funcCls.GetServerDateTime();
                        nitem.mul_user_login = Constant.CurrentUserLogin;
                        nitem.tcr_update_date = funcCls.GetServerDateTime();
                        tcd.trn_company_request_doctors.Add(nitem);
                    }

                    //company request doctor Cats
                    //dr = txtHiddenDocCate.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //dbc.trn_company_doctor_cats.DeleteAllOnSubmit(tcd.trn_company_doctor_cats);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_company_doctor_cat docCatNew = new trn_company_doctor_cat();
                    //    docCatNew.mdc_id = Convert1.ToInt32(datacol[0]);
                    //    docCatNew.tcdc_create_by = Constant.CurrentUserLogin;
                    //    docCatNew.tcdc_create_date = funcCls.GetServerDateTime();
                    //    docCatNew.mul_user_login = Constant.CurrentUserLogin;
                    //    docCatNew.tcdc_update_date = funcCls.GetServerDateTime();
                    //    tcd.trn_company_doctor_cats.Add(docCatNew);
                    //}

                    dbc.trn_company_doctor_cats.DeleteAllOnSubmit(tcd.trn_company_doctor_cats);
                    if (ViewState["objdoccateList"] == null)
                        _objdoccate = new List<AddDocCate>();
                    else
                        _objdoccate = (List<AddDocCate>)ViewState["objdoccateList"];

                    foreach (var strItem in _objdoccate)
                    {
                        trn_company_doctor_cat docCatNew = new trn_company_doctor_cat();
                        docCatNew.mdc_id = Convert1.ToInt32(strItem.mdc_id);
                        docCatNew.tcdc_create_by = Constant.CurrentUserLogin;
                        docCatNew.tcdc_create_date = funcCls.GetServerDateTime();
                        docCatNew.mul_user_login = Constant.CurrentUserLogin;
                        docCatNew.tcdc_update_date = funcCls.GetServerDateTime();
                        tcd.trn_company_doctor_cats.Add(docCatNew);
                    }


                    //CheckUp Loacation
                    //dr = txtHiddenSite.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
                    //dbc.trn_checkup_locations.DeleteAllOnSubmit(tcd.trn_checkup_locations);
                    //foreach (string strItem in dr)
                    //{
                    //    string[] datacol = strItem.Split(',');//col
                    //    trn_checkup_location locationNew = new trn_checkup_location();
                    //    locationNew.mcl_id = Convert1.ToInt32(datacol[0]);
                    //    locationNew.tcl_create_by = Constant.CurrentUserLogin;
                    //    locationNew.tcl_create_date = funcCls.GetServerDateTime();
                    //    locationNew.mul_user_login = Constant.CurrentUserLogin;
                    //    locationNew.tcl_update_date = funcCls.GetServerDateTime();
                    //    tcd.trn_checkup_locations.Add(locationNew);
                    //}
                    dbc.trn_checkup_locations.DeleteAllOnSubmit(tcd.trn_checkup_locations);
                    if (ViewState["objSiteList"] == null)
                        _objsite = new List<AddSite>();
                    else
                        _objsite = (List<AddSite>)ViewState["objSiteList"];

                    foreach (var strItem in _objsite)
                    {
                        trn_checkup_location locationNew = new trn_checkup_location();
                        locationNew.mcl_id = Convert1.ToInt32(strItem.mcl_id);
                        locationNew.tcl_create_by = Constant.CurrentUserLogin;
                        locationNew.tcl_create_date = funcCls.GetServerDateTime();
                        locationNew.mul_user_login = Constant.CurrentUserLogin;
                        locationNew.tcl_update_date = funcCls.GetServerDateTime();
                        tcd.trn_checkup_locations.Add(locationNew);
                    }




                    //***********  payment Type  **************************************************************
                    SetPaymentTypeSave(ref tcd,dbc);


                    //Save Upload File *****************************
                    if (ViewState["AttachFileList"] == null)
                        _docfileList = new List<PalliativeDocFileEntity>();
                    else
                        _docfileList = (List<PalliativeDocFileEntity>)ViewState["AttachFileList"];
                    if (_docfileList != null)
                    {
                        if (this.SaveUploadfile(dbc, ref tcd) == false)
                        {
                            return false;
                        }
                    }

                    //Condition service เงื่อนไขการเข้ารับบริการ
                    dbc.trn_condition_services.DeleteAllOnSubmit(tcd.trn_condition_services);
                    foreach (ListItem item in CHmcs_id_Employee.Items)
                    {
                        if (item.Selected == true)//text or value (item.Value.Trim())
                        {
                            trn_condition_service connew = new trn_condition_service();
                            connew.mcs_id = Convert1.ToInt32(item.Value.Trim());
                            connew.tcs_type = "EM";
                            connew.tcs_create_by = Constant.CurrentUserLogin;
                            connew.tcs_create_date = funcCls.GetServerDateTime();
                            connew.mul_user_login = Constant.CurrentUserLogin;
                            connew.tcs_update_date = funcCls.GetServerDateTime();
                            tcd.trn_condition_services.Add(connew);
                        }
                    }

                    //Condition service เงื่อนไขการเข้ารับบริการ
                    foreach (ListItem item in CHmcs_id_Boss.Items)
                    {
                        if (item.Selected == true)//text or value (item.Value.Trim())
                        {
                            trn_condition_service connew = new trn_condition_service();
                            connew.mcs_id = Convert1.ToInt32(item.Value.Trim());
                            connew.tcs_type = "EX";
                            connew.tcs_create_by = Constant.CurrentUserLogin;
                            connew.tcs_create_date = funcCls.GetServerDateTime();
                            connew.mul_user_login = Constant.CurrentUserLogin;
                            connew.tcs_update_date = funcCls.GetServerDateTime();
                            tcd.trn_condition_services.Add(connew);
                        }
                    }

                    //Save Package

                    if (ViewState["PackageDetailList"] == null)
                        _packagedetail = new List<PackageDetailList>();
                    else
                        _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];

                    if (_packagedetail != null)
                    {
                        if (SavePackageItem(dbc, ref tcd) == false)
                        {
                            return false;
                        }
                    }

                    //tab4 รายชื่อผู้ตรวจ
                    InsertAndUpdateNameCheck(dbc,ref tcd);

                    dbc.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                lbmsgAlert.Text = ex.Message;
                return false;
            }
        }
        private void SetPaymentTypeSave(ref trn_company_detail tcd, InhToDoListDataContext dbc)
        {
            if (ViewState["paymentType"] == null)
                _paymentType = new List<PaymentGrid>();
            else
                _paymentType = (List<PaymentGrid>)ViewState["paymentType"];

            if (_paymentType != null)
            {
                foreach (PaymentGrid item in _paymentType)
                {
                    if (item.Status == "N")
                    {
                        trn_payment newitem = new trn_payment();
                        newitem.tpa_id=0;
                        newitem.mst_id=item.mst_id;
                        newitem.mpt_id=item.mpt_id;
                        newitem.tpa_mpt_credit=item.tpa_mpt_credit;
                        newitem.tpa_mpt_remark=item.tpa_mpt_remark;
                        newitem.mbm_id= item.mbm_id == 0 ? null : item.mbm_id;
                        newitem.mpm_id=item.mpm_id;
                        newitem.mpr_id=item.mpr_id;
                        newitem.mpq_id=item.mpq_id;
                        newitem.tpa_mpq_credit=item.tpa_mpq_credit;
                        newitem.mpn_id=item.mpn_id;
                        newitem.tpa_mpn_credit=item.tpa_mpn_credit;
                        newitem.mrm_id=item.mrm_id;
                        newitem.tpa_coupon=item.tpa_coupon;
                        newitem.tpa_coupon_remark=item.tpa_coupon_remark;
                        newitem.tpa_create_by = Constant.CurrentUserLogin; 
                        newitem.tpa_create_date = funcCls.GetServerDateTime();
                        newitem.mul_user_login = Constant.CurrentUserLogin; 
                        newitem.tpa_update_date = funcCls.GetServerDateTime();
                        tcd.trn_payments.Add(newitem);
                    }
                    else if (item.Status == "D")
                    {
                        trn_payment currentpayment = tcd.trn_payments.Where(x => x.tpa_id == item.tpa_id).FirstOrDefault();
                        if (currentpayment != null)
                        {
                            try
                            {
                                //Delete Recode in trn_payments
                                dbc.trn_payments.DeleteOnSubmit(currentpayment);
                            }
                            catch (Exception)
                            {
                                
                            }
                        }
                    }
                    else
                    {
                        //update
                        trn_payment currentpayment = tcd.trn_payments.Where(x => x.tpa_id == item.tpa_id).FirstOrDefault();
                        if (currentpayment != null)
                        {
                            currentpayment.mst_id = item.mst_id;
                            currentpayment.mpt_id = item.mpt_id;
                            currentpayment.tpa_mpt_credit = item.tpa_mpt_credit;
                            currentpayment.tpa_mpt_remark = item.tpa_mpt_remark;
                            currentpayment.mbm_id = item.mbm_id;
                            currentpayment.mpm_id = item.mpm_id;
                            currentpayment.mpr_id = item.mpr_id;
                            currentpayment.mpq_id = item.mpq_id;
                            currentpayment.tpa_mpq_credit = item.tpa_mpq_credit;
                            currentpayment.mpn_id = item.mpn_id;
                            currentpayment.tpa_mpn_credit = item.tpa_mpn_credit;
                            currentpayment.mrm_id = item.mrm_id;
                            currentpayment.tpa_coupon = item.tpa_coupon;
                            currentpayment.tpa_coupon_remark = item.tpa_coupon_remark;
                            //currentpayment.tpa_create_by = Constant.CurrentUserLogin;
                            //currentpayment.tpa_create_date = funcCls.GetServerDateTime();
                            currentpayment.mul_user_login = Constant.CurrentUserLogin;
                            currentpayment.tpa_update_date = funcCls.GetServerDateTime();
                        }
                    }
                }
            }
            
        }
        private bool SaveUploadfile(InhToDoListDataContext dbc, ref trn_company_detail tcd)
        {
                funcCls.ErrorLog("Start Upload file");
                DateTime dtnow = funcCls.GetServerDateTime();
                if (ViewState["AttachFileList"] == null)
                    _docfileList = new List<PalliativeDocFileEntity>();
                else
                    _docfileList = (List<PalliativeDocFileEntity>)ViewState["AttachFileList"];
                foreach (PalliativeDocFileEntity item in _docfileList)
                {
                    if (item.Status == "N")
                    {
                        if (item.fileStream != null)
                        {
                            funcCls.ErrorLog("file :" + item.file_name + " status :" + item.Status);
                            Stream filestream = new MemoryStream(item.fileStream);
                            if (WriteFile(item.pathReal, filestream))
                            {
                                funcCls.ErrorLog("Upload file completed.");
                            }
                            else
                            {
                                if (File.Exists(Server.MapPath(item.pathReal)))
                                {
                                    File.Delete(Server.MapPath(item.pathReal));
                                    funcCls.ErrorLog("Delete file completed.");
                                }
                                funcCls.ErrorLog("Upload file type Copy." + Server.MapPath(item.attach_file) + ":" + Server.MapPath(item.pathReal));
                                File.Copy(Server.MapPath(item.attach_file), Server.MapPath(item.pathReal));
                                funcCls.ErrorLog("Copy completed.");
                            }
                            mst_path_file newpathfile = new mst_path_file();

                            // newpathfile.taf_user_type = Convert.ToChar(item.TypeFile);
                            newpathfile.mpf_file_name = item.file_name;
                            newpathfile.mpf_path_name = item.pathReal;
                            newpathfile.mpf_create_by = Constant.CurrentUserLogin;
                            newpathfile.mpf_create_date = dtnow;
                            newpathfile.mul_user_login = Constant.CurrentUserLogin;
                            newpathfile.mpf_update_date = dtnow;

                            //insert Attach file
                            if (item.TypeMkt)
                            {
                                trn_attach_file newattach = SetAttachfile("MKT", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeHPC)
                            {
                                trn_attach_file newattach = SetAttachfile("HPC", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeCollection)
                            {
                                trn_attach_file newattach = SetAttachfile("CLT", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeContact)
                            {
                                trn_attach_file newattach = SetAttachfile("CTC", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            //********************

                            tcd.mst_path_files.Add(newpathfile);
                            funcCls.ErrorLog("Save in trn_attach_file New record completed.");
                        }
                        else
                        {
                            mst_path_file newpathfile = new mst_path_file();
                            newpathfile.mpf_file_name = item.file_name;
                            newpathfile.mpf_path_name = item.pathReal;
                            newpathfile.mpf_create_by = Constant.CurrentUserLogin;
                            newpathfile.mpf_create_date = dtnow;
                            newpathfile.mul_user_login = Constant.CurrentUserLogin;
                            newpathfile.mpf_update_date = dtnow;

                            //insert Attach file
                            if (item.TypeMkt)
                            {
                                trn_attach_file newattach = SetAttachfile("MKT", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeHPC)
                            {
                                trn_attach_file newattach = SetAttachfile("HPC", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeCollection)
                            {
                                trn_attach_file newattach = SetAttachfile("CLT", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeContact)
                            {
                                trn_attach_file newattach = SetAttachfile("CTC", dtnow, dbc);
                                newpathfile.trn_attach_files.Add(newattach);
                            }
                            //********************

                            tcd.mst_path_files.Add(newpathfile);
                            funcCls.ErrorLog("Save in trn_attach_file New record completed.");
                        }
                    }
                    else if (item.Status == "D")
                    {
                        mst_path_file currentattachfile = tcd.mst_path_files.Where(x => x.mpf_id == item.RowID).FirstOrDefault();
                        if (currentattachfile != null)
                        {
                            try
                            {
                                funcCls.ErrorLog("file :" + item.file_name + " status :" + item.Status);
                                //delete file in folder
                                string fileName = currentattachfile.mpf_path_name;
                                string pathfile = string.Format(Constant.pathserverUpload, fileName);
                                string Serverpath = Server.MapPath(pathfile);
                                if (File.Exists(Serverpath))
                                {
                                    File.Delete(Serverpath);
                                    funcCls.ErrorLog("Delete file completed.");
                                }

                                //Delete Recode in trn_attach_file
                                dbc.trn_attach_files.DeleteAllOnSubmit(currentattachfile.trn_attach_files);
                                dbc.mst_path_files.DeleteOnSubmit(currentattachfile);
                                funcCls.ErrorLog("Delete recode in trn_attach_files completed.");
                            }
                            catch (Exception ex)
                            {
                                funcCls.ErrorLog("Delete recode error :" + ex.Message);
                                lbmsgAlert.Text = "ไม่สามารถทำการ ลบ file:" + currentattachfile.mpf_path_name;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //กรณีที่มีการเปลื่ยน Type 
                        mst_path_file currentattachfile = tcd.mst_path_files.Where(x => x.mpf_id == item.RowID).FirstOrDefault();
                        if (currentattachfile != null)
                        {
                            dbc.trn_attach_files.DeleteAllOnSubmit(currentattachfile.trn_attach_files);

                            //insert Attach file
                            if (item.TypeMkt)
                            {
                                trn_attach_file newattach = SetAttachfile("MKT", dtnow, dbc);
                                currentattachfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeHPC)
                            {
                                trn_attach_file newattach = SetAttachfile("HPC", dtnow, dbc);
                                currentattachfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeCollection)
                            {
                                trn_attach_file newattach = SetAttachfile("CLT", dtnow, dbc);
                                currentattachfile.trn_attach_files.Add(newattach);
                            }
                            if (item.TypeContact)
                            {
                                trn_attach_file newattach = SetAttachfile("CTC", dtnow, dbc);
                                currentattachfile.trn_attach_files.Add(newattach);
                            }
                            //********************
                        }

                    }
                }
                funcCls.ErrorLog("End Upload.");
                return true;
        }
        private bool SavePackageItem(InhToDoListDataContext dbc, ref trn_company_detail tcd)
        {
            DateTime dtnow = funcCls.GetServerDateTime();
            string userlogin = Constant.CurrentUserLogin;

            if (ViewState["PackageDetailList"] == null)
                _packagedetail = new List<PackageDetailList>();
            else
                _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];

            foreach (PackageDetailList item in _packagedetail)
            {
                if (item.Status == "N")
                {
                    trn_package_detail newitem = new trn_package_detail();
                    newitem.tcd_id = tcd.tcd_id;
                    newitem.tpd_order_code = item.tpd_order_code;
                    newitem.tpd_order_desc = item.tpd_order_desc;
                    newitem.tpd_order_type = item.tpd_order_type;
                    newitem.mpt_id = item.mpt_id;
                    newitem.tpd_mpt_name = item.mpt_name;
                    newitem.tpd_limit_credit = item.tpd_limit_credit;
                    newitem.tpd_price = item.tpd_price;
                    newitem.tpd_date_from = item.tpd_date_from;
                    newitem.tpd_date_to = item.tpd_date_to;
                    newitem.mpy_id = item.mpy_id;
                    newitem.tpd_mpy_name = item.mpy_name;
                    newitem.tpd_mpy_remark = item.tpd_mpy_remark;
                    newitem.tpd_mpy_remark1 = item.tpd_mpy_remark1;
                    newitem.tpd_create_by = userlogin;
                    newitem.tpd_create_date = dtnow;
                    newitem.mul_user_login = userlogin;
                    newitem.tpd_update_date = dtnow;

                    trn_package_request_doctor dtnew = new trn_package_request_doctor();
                    dtnew.trd_doc_code = "";
                    dtnew.trd_doc_ename = "";
                    dtnew.trd_doc_tname = item.DoctorName;
                    dtnew.trd_create_by = userlogin;
                    dtnew.trd_create_date = dtnow;
                    dtnew.mul_user_login = userlogin;
                    dtnew.trd_update_date = dtnow;
                    newitem.trn_package_request_doctors.Add(dtnew);

                    trn_package_doctor_cat dtcat = new trn_package_doctor_cat();
                    dtcat.mdc_id = item.DoctorCatID;
                    dtcat.tdc_create_by = userlogin;
                    dtcat.tdc_create_date = dtnow;
                    dtcat.mul_user_login = userlogin;
                    dtcat.tdc_update_date = dtnow;
                    newitem.trn_package_doctor_cats.Add(dtcat);

                    tcd.trn_package_details.Add(newitem);
                }
                else if (item.Status == "D")
                {
                    trn_package_detail currentpackage = tcd.trn_package_details.Where(x => x.tpd_id == item.Rowid).FirstOrDefault();
                    if (currentpackage != null)
                    {
                        try
                        {
                            //Delete Recode in trn_attach_file
                            dbc.trn_package_request_doctors.DeleteAllOnSubmit(currentpackage.trn_package_request_doctors);
                            dbc.trn_package_doctor_cats.DeleteAllOnSubmit(currentpackage.trn_package_doctor_cats);
                            dbc.trn_package_details.DeleteOnSubmit(currentpackage);
                        }
                        catch (Exception)
                        {
                            lbmsgAlert.Text = "ไม่สามารถทำการ ลบ file:" + currentpackage.tpd_order_desc;
                            return false;
                        }
                    }
                }
                else
                {
                    if (item.tpd_id > 0)
                    {
                        var currentp_detail = (from t1 in tcd.trn_package_details
                                               where t1.tpd_id == item.tpd_id
                                               select t1).FirstOrDefault();
                        if (currentp_detail != null)
                        {
                            currentp_detail.tpd_order_code = item.tpd_order_code;
                            currentp_detail.tpd_order_desc = item.tpd_order_desc;
                            currentp_detail.tpd_order_type = item.tpd_order_type;
                            currentp_detail.mpt_id = item.mpt_id;
                            currentp_detail.tpd_mpt_name = item.mpt_name;
                            currentp_detail.tpd_limit_credit = item.tpd_limit_credit;
                            currentp_detail.tpd_price = item.tpd_price;
                            currentp_detail.tpd_date_from = item.tpd_date_from;
                            currentp_detail.tpd_date_to = item.tpd_date_to;
                            currentp_detail.mpy_id = item.mpy_id;
                            currentp_detail.tpd_mpy_name = item.mpy_name;
                            currentp_detail.tpd_mpy_remark = item.tpd_mpy_remark;
                            currentp_detail.tpd_mpy_remark1 = item.tpd_mpy_remark1;
                            currentp_detail.mul_user_login = userlogin;
                            currentp_detail.tpd_update_date = dtnow;

                            var currentP_R = (from t1 in currentp_detail.trn_package_request_doctors
                                              select t1).FirstOrDefault();
                            if (currentP_R != null)
                            {
                                currentP_R.trd_doc_code = "";
                                currentP_R.trd_doc_ename = "";
                                currentP_R.trd_doc_tname = item.DoctorName;
                                currentP_R.mul_user_login = userlogin;
                                currentP_R.trd_update_date = dtnow;
                            }
                            else
                            {
                                trn_package_request_doctor newrd = new trn_package_request_doctor();
                                newrd.trd_doc_code = "";
                                newrd.trd_doc_ename = "";
                                newrd.trd_doc_tname = item.DoctorName;
                                newrd.trd_create_by = Constant.CurrentUserLogin;
                                newrd.trd_create_date = funcCls.GetServerDateTime();
                                newrd.mul_user_login = Constant.CurrentUserLogin;
                                newrd.trd_update_date = funcCls.GetServerDateTime();
                                currentp_detail.trn_package_request_doctors.Add(newrd);

                            }

                            var currentC_D = (from t1 in currentp_detail.trn_package_doctor_cats
                                              select t1).FirstOrDefault();
                            if (currentC_D != null)
                            {
                                currentC_D.mdc_id = item.DoctorCatID;
                                currentC_D.mul_user_login = Constant.CurrentUserLogin;
                                currentC_D.tdc_update_date = funcCls.GetServerDateTime();
                            }
                            else
                            {
                                trn_package_doctor_cat dtcat = new trn_package_doctor_cat();
                                dtcat.mdc_id = item.DoctorCatID;
                                dtcat.tdc_create_by = Constant.CurrentUserLogin;
                                dtcat.tdc_create_date = funcCls.GetServerDateTime();
                                dtcat.mul_user_login = Constant.CurrentUserLogin;
                                dtcat.tdc_update_date = funcCls.GetServerDateTime();
                                currentp_detail.trn_package_doctor_cats.Add(dtcat);
                            }
                        }
                    }
                }            

            }
            ViewState["PackageDetailList"] = _packagedetail;
            return true;
        }

        private void InsertAndUpdateNameCheck(InhToDoListDataContext dbc, ref trn_company_detail tcd)
        {
            //save data name check
            DateTime dtnow = funcCls.GetServerDateTime();
            string Username=Constant.CurrentUserLogin;
            if (ViewState["NamecheckList"] == null)
                objnchk_list = new List<Clstrn_name_check>();
            else
                objnchk_list = (List<Clstrn_name_check>)ViewState["NamecheckList"];

            foreach (var item in objnchk_list)
            {
                //var objname = (from tnc in dbc.trn_name_checks
                //               where tnc.tnc_id == item.tnc_id
                //               select tnc).FirstOrDefault();
                //if (objname != null) //update
                //{
                //    objname.tnc_legal = item.tnc_legal;
                //    objname.tnc_company_name = item.tnc_company_name;
                //    objname.tnc_emp_id = item.tnc_emp_id;
                //    objname.tnc_personal_id = item.tnc_personal_id;
                //    objname.tnc_hn = item.tnc_hn;
                //    objname.tnc_title_name = item.tnc_title_name;
                //    objname.tnc_fname = item.tnc_fname;
                //    objname.tnc_lname = item.tnc_lname;
                //    objname.tnc_gender = item.tnc_gender;
                //    objname.tnc_site = item.tnc_site;
                //    objname.tnc_department = item.tnc_department;
                //    objname.tnc_position = item.tnc_position;
                //    objname.tnc_dob = item.tnc_dob;
                //    objname.tnc_age = item.tnc_age;
                //    objname.tnc_program = item.tnc_program;
                //    objname.tnc_option = item.tnc_option;
                //    objname.tnc_appoint_date = item.tnc_appoint_date;
                //    objname.tnc_remark = item.tnc_remark;
                //    objname.mul_user_login = Username;
                //    objname.tnc_update_date = dtnow;
                //}
                //else //insert
                //{
                    trn_name_check newitem = new trn_name_check();
                    newitem.tnc_legal = item.tnc_legal;
                    newitem.tnc_company_name = item.tnc_company_name;
                    newitem.tnc_emp_id = item.tnc_emp_id;
                    newitem.tnc_personal_id = item.tnc_personal_id;
                    newitem.tnc_hn = item.tnc_hn;
                    newitem.tnc_title_name = item.tnc_title_name;
                    newitem.tnc_fname = item.tnc_fname;
                    newitem.tnc_lname = item.tnc_lname;
                    newitem.tnc_gender = item.tnc_gender;
                    newitem.tnc_site = item.tnc_site;
                    newitem.tnc_department = item.tnc_department;
                    newitem.tnc_position = item.tnc_position;
                    newitem.tnc_dob = item.tnc_dob;

                    newitem.tnc_age = item.tnc_age;
                    newitem.tnc_program = item.tnc_program;
                    newitem.tnc_option = item.tnc_option;
                    newitem.tnc_appoint_date = item.tnc_appoint_date;
                    newitem.tnc_remark = item.tnc_remark;
                    newitem.tnc_create_by = Username;
                    newitem.tnc_create_date = dtnow;
                    newitem.mul_user_login = Username;
                    newitem.tnc_update_date = dtnow;

                    tcd.trn_name_checks.Add(newitem);
                    
                //}//else
            }
            ViewState["NamecheckList"] = objnchk_list;

            if (ViewState["PersonalIDList"] == null)
                _PersonalIDList = new List<DeletePersonalID>();
            else
                _PersonalIDList = (List<DeletePersonalID>)ViewState["PersonalIDList"];

                if (_PersonalIDList.Count != 0)
                {
                    foreach (var item in _PersonalIDList)
                    {
                        var objDel = (from tcn in tcd.trn_name_checks 
                                      where tcn.tnc_id == item.tnc_id 
                                      select tcn);
                        
                        //tcd.trn_name_checks.Remove(objDel);
                        dbc.trn_name_checks.DeleteAllOnSubmit(objDel);
                    }

                }

                //dbc.SubmitChanges();

        }
        private trn_contact_person setContact(string person,string Tel,string Fax,string Email,char strType)
        {
            trn_contact_person contact_Personnew = new trn_contact_person();
            contact_Personnew.tcp_name = person;//.Replace("|x|",",");
            //contact_Personnew.tcp_mname = "";
            //contact_Personnew.tcp_lname = "";
            contact_Personnew.tcp_tel = Tel;//.Replace("|x|", ",");
            contact_Personnew.tcp_mobile = "";
            contact_Personnew.tcp_fax = Fax;//.Replace("|x|", ",");
            contact_Personnew.tcp_email = Email;//.Replace("|x|", ",");
            contact_Personnew.mct_id = Getmct_id(strType);//'C'=Persion B=Bill M=Marketing,P=Payor
            contact_Personnew.tcp_status = 'A';
            contact_Personnew.tcp_create_by = Constant.CurrentUserLogin;
            contact_Personnew.tcp_create_date = funcCls.GetServerDateTime();
            contact_Personnew.mul_user_login = Constant.CurrentUserLogin;
            contact_Personnew.tcp_update_date = funcCls.GetServerDateTime();
            return contact_Personnew;
        }
        private trn_attach_file SetAttachfile(string mut_code,DateTime dtnow, InhToDoListDataContext dbc)
        {
            trn_attach_file newattach = new trn_attach_file();
            newattach.mut_id = (from t1 in dbc.mst_user_types
                                where t1.mut_code == mut_code
                                select t1.mut_id).FirstOrDefault();
            newattach.taf_create_by = Constant.CurrentUserLogin;
            newattach.taf_create_date = dtnow;
            newattach.mul_user_login = Constant.CurrentUserLogin;
            newattach.taf_update_date = dtnow;
            return newattach;
        }

        private int Getmct_id(char mct_code)
        {
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                var objcurrentmct = (from t1 in dbc.mst_contact_types
                                     where t1.mct_code == mct_code
                                     select t1).FirstOrDefault();
                if (objcurrentmct != null)
                {
                    return objcurrentmct.mct_id;
                }
                else
                {
                    return 0;
                }
            }
        }
        private string MargeString(object str1, object str2)
        {
            string nameTH = Convert1.ToString(str1);
            string nameEN = Convert1.ToString(str2);
            string NameAll = "";
            if (nameTH == "" || nameEN == "")
            {
                if (nameTH != "")
                {
                    NameAll = nameTH;
                }
                if (nameEN != "")
                {
                    NameAll = nameEN;
                }
            }
            else
            {
                NameAll = nameTH + " / " + nameEN;
            }
            return NameAll;
        }

        protected void btnEditFrm_Click(object sender, EventArgs e)
        {
            HDStatus.Value = "1";
            Hiddencontrol();

            gnvAttachFile.Columns[7].Visible = true;
            gnvAttachFile.Enabled = true;
            AttachfileSite.Enabled = true;
            ImportFileUploadCompanyDetail.Enabled = true;
            ch_typeAttachfile.Enabled = true;
            btnAddFile.Visible = true;
            btnImport.Visible = false;
            FileUploadExcelContactCheck.Enabled = true;
            btnImportExcelContactCheck.Visible = true;
            btnSave.Visible = true;
            //littab1script.Text = strShowbutton;
            //littab2script.Text = strshowbutton2;
            littab3script.Text = "<script type='text/javascript'>showpaymentType();</script>";
            littab5.Text = "<script type='text/javascript'>showpaymentType2();</script>";
            //----- package----
            btntab3Add.Visible = true;
            btnAddRemarkmpy.Visible = true;

            btnAddNew.Visible = true;
            btnCopy.Visible = false;
            btnExport.Visible = false;
            btnEditFrm.Visible = false;
            txt_company_code.Enabled = true;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }
        private void ExportToExcel()
        {
            string TemplatePath =Constant.pathServer+ @"App_Class\tmpExcel.html";
            string thumb = Constant.ParseTemplate(TemplatePath);
            string datarow = datafrm(); 

            string Logo = Constant.URLServer + "images/LogobankokHospital.jpg";//image Logo
            string tm = string.Format(thumb, datarow, Logo);
            Response.ClearContent();
            Response.ContentType = "Application/vnd.ms-excel";
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment; filename=Print.xls");
            Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            Response.Write("<head>");
            Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            Response.Write("<!--[if gte mso 9]><xml>");
            Response.Write("<x:ExcelWorkbook>");
            Response.Write("<x:ExcelWorksheets>");
            Response.Write("<x:ExcelWorksheet>");
            Response.Write("<x:Name>Report Data</x:Name>");
            Response.Write("<x:WorksheetOptions>");
            Response.Write("<x:Print>");
            Response.Write("<x:ValidPrinterInfo/>");
            Response.Write("</x:Print>");
            Response.Write("</x:WorksheetOptions>");
            Response.Write("</x:ExcelWorksheet>");
            Response.Write("</x:ExcelWorksheets>");
            Response.Write("</x:ExcelWorkbook>");
            Response.Write("</xml>");
            Response.Write("<![endif]--> ");
            Response.Write(tm); // give ur html string here
            Response.Write("</head>");
            Response.Flush();
            Response.End();
        }
        private string datafrm()
        {
            string Output = "<tr>";
            for (int i = 0; i < 60; i++)
            {
                Output += "<td>{" + i.ToString() + "}</td>";
            }
            Output += "</tr>";

            string dataAll = "";

            var dat1No=txt_company_code.Text;
            var dat2Thai = txtComNameTH.Text;
            var dat3Eng = txtComNameEng.Text;
            var dat4Address = txtComAddress.Text ;
            var dat5Tambon =  txtComTumbon.Text ;
            var dat6District =txtComAumphur.Text ;
            var dat7Province =  txtComProvince.Text ;
            var dat8Postcode =  txtComPostCode.Text ;
            var dat9Tel = txtComTel.Text;
            var dat10Fax =  txtComFax.Text;
            var dat11E_mail = txtComEmail.Text;
            var dat12Contact_Personname ="";
            var dat13Contact_Persontel = "";
            var dat14Contact_Personfax = "";
            var dat15Contact_PersonEmail = "";
            var dat16Form = txtconDatefrom.Text;
            var dat17To = txtConDateto.Text;
            var dat18Contact_BillCompanyName = txtbillComNameth.Text;
            var dat19Contact_BillAddress = txtbillAddress.Text;
            var dat20Contact_BillTambon = txtbillTumbon.Text;
            var dat21Contact_BillDistrict = txtbillAumphur.Text;
            var dat22Contact_BillProvince = txtbillProvince.Text;
            var dat23Contact_BillPostcode = txtbillPostCode.Text;
            var dat24Contact_BillTel = txtbillTel.Text;
            var dat25Contact_BillFax = txtbillFax.Text;
            var dat26Contact_BillEmail = "";
            var dat27Type = (DDtype.SelectedItem != null) ? DDtype.SelectedValue + "=" + DDtype.SelectedItem.Text : "";
            var dat28PaymentType = (RDPaymentType.SelectedItem != null) ? RDPaymentType.SelectedValue + "=" + RDPaymentType.SelectedItem.Text:"";
            var dat29Amount = txtBillAmount.Text ;
            var dat30Remark =  txtBillRemark.Text ;
            var dat31BillingMethod = (RDMbm_id.SelectedItem != null) ? RDMbm_id.SelectedValue + "=" + RDMbm_id.SelectedItem.Text : "";//****
            var dat32CheckupRate =(RDmpr_id.SelectedItem != null) ? RDmpr_id.SelectedValue + "=" + RDmpr_id.SelectedItem.Text:"";
            var dat33MainProgram = (RDMpm_id.SelectedItem != null) ? RDMpm_id.SelectedValue + "=" + RDMpm_id.SelectedItem.Text:"";
            var dat34OptionsItemsAsMentionedInQuotation = (RDmpq_id.SelectedItem != null) ? RDmpq_id.SelectedValue + "=" + RDmpq_id.SelectedItem.Text:"";
            var dat35AmountInQuotation = txtmqp_credit.Text;
            var dat36OptionsItemsAsMentionedNotInQuotation = (RDmpn_id.SelectedItem != null) ?  RDmpn_id.SelectedValue + "=" + RDmpn_id.SelectedItem.Text:"";
            var dat37AmountNotInQuotation =txtmpn_Credit.Text;
            var dat38TermOfReceivingMedicine = (RDmrm_id.SelectedItem != null) ? RDmrm_id.SelectedValue + "=" + RDmrm_id.SelectedItem.Text : "";
            var dat39MealCoupon =(RDCoupon.SelectedItem != null) ? RDCoupon.SelectedValue + "=" + RDCoupon.SelectedItem.Text:"";
            var dat40Familyswelfare = txtFamilyWelfar.Text;
            var dat41Name = txt_req_doc.Text ;
            var dat42Doctor_Cat = (dlDocCate.SelectedItem != null) ? dlDocCate.SelectedValue + "=" + dlDocCate.SelectedItem.Text : ""; 
            var dat43SiteName = "";//Lists
            var dat44SiteRemark = txttcdLocation_remark.Text;
            var dat45ResultAddress= txtResultAddress.Text;
            var dat46ResultTumbon=txtResultTumbon.Text;
            var dat47ResultAumphur=txtResultAumphur.Text;
            var dat48ResultProvince=txtResultProvince.Text;
            var dat49ResultPostCode=txtResultPostCode.Text;

            var dat50MedicalReportsName = "";
            var dat51MedicalReportsOther = txttcd_rep_remark.Text;

            var dat52SendReportMOriginal = "";
            var dat53SendReportMNotGetBack= "";
            var dat54SendReportMCopy = "";

            var dat55conditionPermittedEmployee = "";
            var dat56conditionPermittedBoss = "";

            var dat57MarketingName = "";
            var dat58MarketingTel = "";
            var dat59MarketingFax = "";
            var dat60MarketingEmail = "";
            var dat61Remark = "";

            //Medical Report 
            string strdatammr="";
            int imr = 0;
            foreach (ListItem item in CHMmmr_id.Items)
            {
                if (item.Selected == true)
                {
                   string mmrid= item.Value.Trim();
                   string mmrdesc = item.Text;
                   if (strdatammr == "")
                   {
                       strdatammr = mmrid + "," + mmrdesc.Replace(",", "|x|")+",";
                       if (imr == CHMmmr_id.Items.Count - 1)
                       {
                           strdatammr += dat51MedicalReportsOther;
                       }
                   }
                   else
                   {
                       strdatammr += "|*|" + mmrid + "," + mmrdesc.Replace(",", "|x|") + ",";
                       if (imr == CHMmmr_id.Items.Count - 1)
                       {
                           strdatammr += dat51MedicalReportsOther;
                       }
                   }
                }
                imr = imr + 1;
            }

            //Send Report Method dat52-dat54
            if (RDtcd_send_rep_realY.Checked)
            {
                dat52SendReportMOriginal = RDtcd_send_rep_realY.Text;
            }else if (RDtcd_send_rep_realN.Checked)
            {
                dat52SendReportMOriginal = RDtcd_send_rep_realN.Text;
                if (RDtcd_send_rep_flagH.Checked)
                {
                    dat53SendReportMNotGetBack = RDtcd_send_rep_flagH.Text;
                }
                else if (RDtcd_send_rep_flagC.Checked)
                {
                    dat53SendReportMNotGetBack = RDtcd_send_rep_flagC.Text;
                }
                
            }
            if (RDtcd_send_rep_copy.SelectedItem != null)
            {
                dat54SendReportMCopy = RDtcd_send_rep_copy.SelectedItem.Text;
            }
            //condition พนักงาน
            string strdataCD = "";
            foreach (ListItem item in CHmcs_id_Employee.Items)
            {
                if (item.Selected == true)
                {
                    string mcsid = item.Value.Trim();
                    string mcsdesc = item.Text;
                    if (strdataCD == "")
                    {
                        strdataCD = mcsid + "," + mcsdesc.Replace(",", "|x|");
                    }
                    else
                    {
                        strdataCD += "|*|" + mcsid + "," + mcsdesc.Replace(",", "|x|");
                    }
                }
            }
            //condition ผู้บริหาร
            string strdataCDbos = "";
            foreach (ListItem item in CHmcs_id_Boss.Items)
            {
                if (item.Selected == true)
                {
                    string mcsid = item.Value.Trim();
                    string mcsdesc = item.Text;
                    if (strdataCDbos == "")
                    {
                        strdataCDbos = mcsid + "," + mcsdesc.Replace(",", "|x|");
                    }
                    else
                    {
                        strdataCDbos += "|*|" + mcsid + "," + mcsdesc.Replace(",", "|x|");
                    }
                }
            }

            // save Contact B=Bill, C=Person, M=Markeing,P=Payor
            //txtHiddenContact
            string[] strSeparatorsRow = new string[] { "|*|" };//row
            string[] drC = txtHiddenContact.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
            
            //ContactBill
            string[] drB = txtHiddenContactBill.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
            
            //Maketing
            string[] drM = txtHiddenContactMkt.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
            //Medical Report
            string[] drMR = strdatammr.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
            //Condition
            string[] drCondition = strdataCD.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);
            string[] drConditionBos = strdataCDbos.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);

            int icount = drC.Length;
            if (drB.Length > icount)
                icount = drB.Length;
            if (drM.Length > icount)
                icount = drM.Length;
            if (drMR.Length > icount)
                icount = drMR.Length;
            if (drCondition.Length > icount)
                icount = drCondition.Length;
            if (drConditionBos.Length > icount)
                icount = drConditionBos.Length;

            //  trn_plan ไม่มี column ใน Excel

            ////CheckUp Loacation
            string[] drLocation = txtHiddenSite.Value.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries);

            for (int ai = 0; ai < icount; ai++)
            {
                //Contact Person
                if (ai < drC.Length)
                {
                    string[] datacol = drC[ai].Split(',');
                    dat12Contact_Personname = datacol[0];
                    dat13Contact_Persontel = datacol[1];
                    dat14Contact_Personfax = datacol[2];
                    dat15Contact_PersonEmail = datacol[3];
                }//Contact Bill
                if (ai < drB.Length)
                {
                    string[] datacol = drB[ai].Split(',');
                    dat18Contact_BillCompanyName = datacol[0];
                    dat24Contact_BillTel = datacol[1];
                    dat25Contact_BillFax = datacol[2];
                    dat26Contact_BillEmail = datacol[3];
                }//Contact Market
                if (ai < drM.Length)
                {
                    string[] datacol = drM[ai].Split(',');
                    dat57MarketingName = datacol[0];
                    dat58MarketingTel = datacol[1];
                    dat59MarketingFax = datacol[2];
                    dat60MarketingEmail = datacol[3];
                }//Location
                if (ai < drLocation.Length)
                {   string[] datacol = drLocation[ai].Split(',');
                    dat43SiteName = datacol[0] + " = " + datacol[1];
                }//Location Remark
                string[] drLocationRemark = txttcdLocation_remark.Text.Split(',');
                if (ai < drLocationRemark.Length)
                {
                    dat44SiteRemark = drLocationRemark[ai];
                }//Medical Report
                if (ai < drMR.Length)
                {
                    string[] datacol = drMR[ai].Split(',');
                    dat50MedicalReportsName = datacol[0] + " = " + datacol[1].Replace("|x|", ",");
                    dat51MedicalReportsOther = datacol[2];
                }//Condition
                if (ai < drCondition.Length)
                {
                    string[] datacol =  drCondition[ai].Split(',');
                    dat55conditionPermittedEmployee = datacol[0] + " = " + datacol[1].Replace("|x|", ",");
                }
                if (ai < drConditionBos.Length)
                {
                    string[] datacol = drConditionBos[ai].Split(',');
                    dat56conditionPermittedBoss = datacol[0] + " = " + datacol[1].Replace("|x|", ",");
                }

                string[] datavalue = new string[]{dat1No,dat2Thai,dat3Eng,dat4Address,dat5Tambon,dat6District,dat7Province,dat8Postcode,dat9Tel,dat10Fax,
                                        dat11E_mail,dat12Contact_Personname,dat13Contact_Persontel,dat14Contact_Personfax,dat15Contact_PersonEmail,dat16Form,
                                        dat17To,dat18Contact_BillCompanyName,dat19Contact_BillAddress,dat20Contact_BillTambon,dat21Contact_BillDistrict,
                dat22Contact_BillProvince,dat23Contact_BillPostcode,dat24Contact_BillTel,dat25Contact_BillFax,dat26Contact_BillEmail,dat27Type,dat28PaymentType,
                dat29Amount,dat30Remark,dat31BillingMethod,dat32CheckupRate,dat33MainProgram,dat34OptionsItemsAsMentionedInQuotation,dat35AmountInQuotation,dat36OptionsItemsAsMentionedNotInQuotation,
                dat37AmountNotInQuotation,dat38TermOfReceivingMedicine,dat39MealCoupon,dat40Familyswelfare,dat41Name,dat42Doctor_Cat,dat43SiteName,dat44SiteRemark,dat45ResultAddress,dat46ResultTumbon,dat47ResultAumphur,dat48ResultProvince,
                dat49ResultPostCode,dat50MedicalReportsName,dat51MedicalReportsOther,dat52SendReportMOriginal,dat53SendReportMNotGetBack,dat54SendReportMCopy,dat55conditionPermittedEmployee,
                dat56conditionPermittedBoss,dat57MarketingName,dat58MarketingTel,dat59MarketingFax,dat60MarketingEmail,dat61Remark};

                dataAll += string.Format(Output, datavalue);
            }

            return dataAll;
        }

        //---- tab Package ---
        protected void btnAddRemarkmpy_Click(object sender, EventArgs e)
        {
            if (txtprepareCheck.Text.Trim() != "")
            {

            }
            using (InhToDoListDataContext dbc = new InhToDoListDataContext())
            {
                var icount = (from t1 in dbc.mst_prepare_yourselfs
                              where t1.mpy_desc == txtprepareCheck.Text.Trim()
                              select t1).Count();
                if (icount == 0)
                {
                    mst_prepare_yourself newitem = new mst_prepare_yourself();
                    newitem.mpy_desc = txtprepareCheck.Text.Trim();
                    newitem.mpy_status = 'A';
                    newitem.mpy_create_date = funcCls.GetServerDateTime();
                    newitem.mpy_create_by = Constant.CurrentUserLogin;
                    newitem.mul_user_login = Constant.CurrentUserLogin;
                    newitem.mpy_update_date = funcCls.GetServerDateTime();
                    dbc.mst_prepare_yourselfs.InsertOnSubmit(newitem);
                    dbc.SubmitChanges();
                    refreshMPY(dbc);
                    DDmpy.SelectedValue = newitem.mpy_id.ToString();
                    txtprepareCheck.Text = "";
                }
                else
                {
                    lbmsgRemarktab3.Text = "Remark had in system.";
                }
            }
        }
        protected void btntab3Add_Click(object sender, EventArgs e)
        {
            lbmsgAlertPackage.Text = "";
            try
            {
                string[] datespilt = txtDateFrom.Text.Split('-');
                DateTime? dnowFrom=null;
                DateTime? dnowto=null;
                if (txtDateFrom.Text.Trim() != "")
                {
                    dnowFrom = Constant.ConvertStringToDate(txtDateFrom.Text);
                }
                if (txtDateTo.Text.Trim() != "")
                {
                    dnowto = Constant.ConvertStringToDate(txtDateTo.Text);
                }
                string[] strSeparatorsRow = new string[] { "|,|" };//row txtordersetOrOption
                string orderdecode = HttpContext.Current.Server.HtmlDecode(txtordersetOrOption.Text);
                string[] orderdata = orderdecode.Split(strSeparatorsRow, StringSplitOptions.RemoveEmptyEntries); ;
                string orderCode = orderdata[0].ToString();
                string ordername = orderdata[1].ToString();
                string ordertype = tab3RDOrderType.SelectedValue.ToString(); //orderdata[2].ToString();//จาก Order Type 
                int? mpt_id = 0;
                int? mpy_id = 0;
                if (tab3RDPaymentType.SelectedValue != null)
                {
                    mpt_id = Convert1.ToInt32(tab3RDPaymentType.SelectedValue);
                }
                else
                {
                    mpt_id = null;
                    lbmsgAlertPackage.Text = "Please select payment Type!";
                    return;
                }
                if (DDmpy.SelectedValue != "0")
                {
                    mpy_id = Convert1.ToInt32(DDmpy.SelectedValue);
                }
                else
                {
                    mpy_id = null;
                }
                string strmpt_name = (tab3RDPaymentType.SelectedItem != null) ? tab3RDPaymentType.SelectedItem.Text : "";
                string strmpy_name = (DDmpy.SelectedValue != "0") ? DDmpy.SelectedItem.Text : "";


                if (ViewState["PackageDetailList"] == null)
                    _packagedetail = new List<PackageDetailList>();
                else
                    _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];

                if (btntab3Add.Text == "Save")
                {
                    //
                    int id = Convert.ToInt32(HDpackageid.Value);
                    var objpackage = _packagedetail.Where(x => x.id == id).FirstOrDefault();
                    if (objpackage != null)
                    {
                        objpackage.tpd_order_code = orderCode;
                        objpackage.tpd_order_desc = ordername;
                        objpackage.tpd_order_type = ordertype;
                        objpackage.mpt_id = mpt_id;
                        objpackage.mpt_name = strmpt_name;
                        objpackage.tpd_limit_credit = Convert1.ToInt32(txtLimitCredit.Text);
                        objpackage.tpd_price = Convert1.ToInt32(txtPricevalue.Text);
                        objpackage.tpd_date_from = dnowFrom;
                        objpackage.tpd_date_to = dnowto;
                        objpackage.mpy_id = mpy_id;
                        objpackage.mpy_name = strmpy_name;
                        string ststa = objpackage.Status;
                        //if (objpackage.Status != "N")
                        //{
                            objpackage.Status = "N";
                        //}
                        objpackage.tpd_mpy_remark = txtprepareCheck.Text;
                        objpackage.DoctorCatID = Convert1.ToInt32(dlDocCate2.SelectedValue);
                        objpackage.DoctorCatName = dlDocCate2.SelectedItem.Text;
                        objpackage.DoctorName = txtDoctorName.Text;
                    }
                }
                else
                {
                    int icountx=_packagedetail.Where(x=>x.tpd_order_code==orderCode).Count();
                    if(icountx==0){
                       var iCountPackage = Convert1.ToInt32(HiddenCountRowPackage.Value);
                        _packagedetail.Add(new PackageDetailList()
                        {
                            id= iCountPackage,
                            tpd_id=0,
                            tpd_order_code = orderCode,
                            tpd_order_desc = ordername,
                            tpd_order_type = ordertype,
                            mpt_id = mpt_id,
                            mpt_name = strmpt_name,
                            tpd_limit_credit = Convert1.ToInt32(txtLimitCredit.Text),
                            tpd_price = Convert1.ToInt32(txtPricevalue.Text),
                            tpd_date_from = dnowFrom,
                            tpd_date_to = dnowto,
                            mpy_id = mpy_id,
                            mpy_name = strmpy_name,
                            tpd_mpy_remark = txtprepareCheck.Text,
                            tpd_mpy_remark1 = "",
                            Status = "N",
                            Rowid = 0,
                            DoctorCatID = Convert1.ToInt32(dlDocCate2.SelectedValue),
                            DoctorCatName = dlDocCate2.SelectedItem.Text,
                            DoctorName = txtDoctorName.Text
                        });
                        HiddenCountRowPackage.Value = (iCountPackage + 1).ToString();
                    }else{
                        var objpackage = _packagedetail.Where(x => x.tpd_order_code == orderCode).FirstOrDefault();
                        objpackage.tpd_order_code = orderCode;
                        objpackage.tpd_order_desc = ordername;
                        objpackage.tpd_order_type = ordertype;
                        objpackage.mpt_id = mpt_id;
                        objpackage.mpt_name = strmpt_name;
                        objpackage.tpd_limit_credit = Convert1.ToInt32(txtLimitCredit.Text);
                        objpackage.tpd_price = Convert1.ToInt32(txtPricevalue.Text);
                        objpackage.tpd_date_from = dnowFrom;
                        objpackage.tpd_date_to = dnowto;
                        objpackage.mpy_id = mpy_id;
                        objpackage.mpy_name = strmpy_name;
                        objpackage.Status = "N";
                        objpackage.tpd_mpy_remark = txtprepareCheck.Text;
                        objpackage.DoctorCatID = Convert1.ToInt32(dlDocCate2.SelectedValue);
                        objpackage.DoctorCatName = dlDocCate2.SelectedItem.Text;
                        objpackage.DoctorName = txtDoctorName.Text;
                        //lbmsgAlertPackage.Text="Pacakge had in select.";
                        //return;
                    }
                }
                ViewState["PackageDetailList"] = _packagedetail;
                BindPackage(_packagedetail);

                txtDateFrom.Text = "";
                txtDateTo.Text = "";
                txtordersetOrOption.Text = "";
                txtPricevalue.Text = "";
                txtDoctorName.Text = "";
                txtLimitCredit.Text = "";
                tab3RDPaymentType.SelectedIndex = 0;
                tab3RDPaymentType.SelectedItem.Selected = false;
                dlDocCate2.SelectedIndex = 0;
                DDmpy.SelectedIndex = 0;
                btntab3Add.Text = "Add";

            }
            catch (Exception ex)
            {
                lbmsgAlertPackage.Text = "ข้อมูลวันที่ไม่ถูกต้อง! กรุณากรอกใหม่";
            }

        }
        protected void BindPackage(List<PackageDetailList> datalist)
        {
            Gridtab3packagePlan.DataSource = new SortableBindingList<PackageDetailList>(datalist.Where(x => (x.Status == "A" || x.Status == "N" || x.Status == "E") & x.tpd_order_type == "Order Set").Select((item, index) => new PackageDetailList
                                    {
                                        id=item.id,
                                        Rowid = index + 1,
                                        tpd_order_code=item.tpd_order_code,
                                        tpd_order_desc = item.tpd_order_desc,
                                        tpd_order_type=item.tpd_order_type,
                                        mpt_id=item.mpt_id,
                                        mpt_name=item.mpt_name,
                                        tpd_limit_credit=item.tpd_limit_credit,
                                        tpd_price=item.tpd_price,
                                        tpd_date_from=item.tpd_date_from,
                                        tpd_date_to=item.tpd_date_to,
                                        mpy_id=item.mpy_id,
                                        mpy_name=item.mpy_name,
                                        tpd_mpy_remark=item.tpd_mpy_remark,
                                        tpd_mpy_remark1=item.tpd_mpy_remark1,
                                        Status=item.Status,
                                    }).ToList());
            Gridtab3packagePlan.DataBind();

            GridView3PackagePlanItem.DataSource = new SortableBindingList<PackageDetailList>(datalist.Where(x => (x.Status == "A" || x.Status == "N" || x.Status == "E") & x.tpd_order_type == "Option").Select((item, index) => new PackageDetailList
            {
                id = item.id,
                Rowid = index + 1,
                tpd_order_code = item.tpd_order_code,
                tpd_order_desc = item.tpd_order_desc,
                tpd_order_type = item.tpd_order_type,
                mpt_id = item.mpt_id,
                mpt_name = item.mpt_name,
                tpd_limit_credit = item.tpd_limit_credit,
                tpd_price = item.tpd_price,
                tpd_date_from = item.tpd_date_from,
                tpd_date_to = item.tpd_date_to,
                mpy_id = item.mpy_id,
                mpy_name = item.mpy_name,
                tpd_mpy_remark = item.tpd_mpy_remark,
                tpd_mpy_remark1 = item.tpd_mpy_remark1,
                Status = item.Status,
            }).ToList());
            GridView3PackagePlanItem.DataBind();
        }
        protected void btnDelPackage_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["PackageDetailList"] == null)
                _packagedetail = new List<PackageDetailList>();
            else
                _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];

            if (_packagedetail != null)
            {
                ImageButton btnEdit = (ImageButton)sender;
                GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
                //Delete Old File && New File

                int rowid = Grow.RowIndex;
                PackageDetailList currentselect = _packagedetail[rowid];
                if (currentselect != null)
                {
                    currentselect.Status = "D";
                    ViewState["PackageDetailList"] = _packagedetail;
                    BindPackage(_packagedetail);
                }
            }
        }
        protected void btnEditPackage_Click(object sender, ImageClickEventArgs e)
        {
            if (ViewState["PackageDetailList"] == null)
                _packagedetail = new List<PackageDetailList>();
            else
                _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];

            if (_packagedetail != null)
            {
                ImageButton btnEdit = (ImageButton)sender;
                GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
                //Delete Old File && New File

                int rowid = Grow.RowIndex;
                PackageDetailList currentselect = _packagedetail[rowid];
                if (currentselect != null)
                {
                    HDpackageid.Value = currentselect.id.ToString();
                    txtordersetOrOption.Text = currentselect.tpd_order_code + "|,|" + currentselect.tpd_order_desc + "|,|" + currentselect.tpd_order_type;
                    tab3RDOrderType.SelectedValue = (currentselect.tpd_order_type != null && currentselect.tpd_order_type == "Order Set") ? "Order Set" : "Order Item";
                    txtPricevalue.Text = currentselect.tpd_price.ToString();
                    txtDateFrom.Text = currentselect.tpd_date_from.Value.ToString("dd/MM/yyyy");
                    txtDateTo.Text = currentselect.tpd_date_to.Value.ToString("dd/MM/yyyy");
                    txtDoctorName.Text = currentselect.DoctorName;
                    dlDocCate2.SelectedValue = currentselect.DoctorCatID.ToString();
                    DDmpy.SelectedValue = (currentselect.mpy_id == null) ? "0" : currentselect.mpy_id.ToString();
                    txtprepareCheck.Text = currentselect.tpd_mpy_remark;
                    tab3RDPaymentType.SelectedValue = currentselect.mpt_id.ToString();
                    txtLimitCredit.Text = currentselect.tpd_limit_credit.ToString();

                    btntab3Add.Text = "Save";
                }
            }
        }
        protected void Gridtab3packagePlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (ViewState["PackageDetailList"] == null)
                _packagedetail = new List<PackageDetailList>();
            else
                _packagedetail = (List<PackageDetailList>)ViewState["PackageDetailList"];

            switch (e.CommandName)
            {
                case "DeleteFile":
                    var objcurrentrow = (from f in _packagedetail where f.id.Equals(Convert.ToInt32(e.CommandArgument)) select f).FirstOrDefault();
                    if (objcurrentrow != null)
                    {
                        if (objcurrentrow.Rowid > 0)
                        {
                            objcurrentrow.Status = "D";
                        }
                        else
                        {
                            _packagedetail.Remove(objcurrentrow);
                        }
                        BindPackage(_packagedetail);
                        ViewState["PackageDetailList"] = _packagedetail;
                    }
                    break;
                case "EditFile":
                    var currentselect = (from f in _packagedetail where f.id.Equals(Convert.ToInt32(e.CommandArgument)) select f).FirstOrDefault();
                    if (currentselect != null)
                    {
                        HDpackageid.Value = currentselect.id.ToString();
                        txtordersetOrOption.Text = currentselect.tpd_order_code + "|,|" + currentselect.tpd_order_desc + "|,|" + currentselect.tpd_order_type;
                        txtPricevalue.Text = currentselect.tpd_price.ToString();
                        txtDateFrom.Text = (currentselect.tpd_date_from!=null)?currentselect.tpd_date_from.Value.ToString("dd/MM/yyyy"):"";
                        txtDateTo.Text = (currentselect.tpd_date_to!=null)?currentselect.tpd_date_to.Value.ToString("dd/MM/yyyy"):"";
                        txtDoctorName.Text = currentselect.DoctorName;
                        dlDocCate2.SelectedValue = currentselect.DoctorCatID.ToString();
                        
                        DDmpy.SelectedValue = (currentselect.mpy_id == null) ? "0" : currentselect.mpy_id.ToString();
                        txtprepareCheck.Text = currentselect.tpd_mpy_remark;
                        tab3RDOrderType.SelectedValue = currentselect.tpd_order_type;
                        tab3RDPaymentType.SelectedValue = currentselect.mpt_id.ToString();
                        txtLimitCredit.Text = currentselect.tpd_limit_credit.ToString();
                        btntab3Add.Text = "Save";
                    }
                    break;
            }
        }
        //--------- -------

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            HDStatus.Value = "1";
            Hiddencontrol();

            gnvAttachFile.Columns[7].Visible = true;
            gnvAttachFile.Enabled = true;
            AttachfileSite.Enabled = true;
            ImportFileUploadCompanyDetail.Enabled = true;
            ch_typeAttachfile.Enabled = true;
            btnAddFile.Visible = true;
            btnImport.Visible = true;
            btnSave.Visible = true;
            //btnSaveDraft.Visible = true;
            btnEditFrm.Visible = true;
            btnImportExcelContactCheck.Visible = true;
            txtdoccode.Text = "";
            txt_company_code.Enabled = false;
            //littab1script.Text = strShowbutton;
            //littab2script.Text = strshowbutton2;
            littab3script.Text = "<script type='text/javascript'>showpaymentType();</script>";
            //----- package----
            btntab3Add.Visible = true;
            btnAddRemarkmpy.Visible = true;
            HDIsCopy.Value = "1";

            btnAddNew.Visible = true;
            btnCopy.Visible = false;
            btnExport.Visible = false;
            btnEditFrm.Visible = false;
            txt_company_code.Enabled = true;

            //RunningDocumentCode();

        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmmktdata.aspx");
        }

        protected void gnvAttachFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (ViewState["AttachFileList"] == null)
                _docfileList = new List<PalliativeDocFileEntity>();
            else
                _docfileList = (List<PalliativeDocFileEntity>)ViewState["AttachFileList"];

            switch (e.CommandName)
            {
                case "DeleteFile":
                    string cmdArgu = e.CommandArgument.ToString();
                    
                    int rowid = Convert1.ToInt32(cmdArgu);
                    foreach (PalliativeDocFileEntity file in _docfileList.Where(x => x.RowID == rowid))
                    {
                        if (file.RowID.Equals(rowid))
                        {
                            var objfile = (from f in _docfileList where f.RowID.Equals(rowid) select f).FirstOrDefault();
                            if (objfile != null)
                            {
                                if (objfile.RowID > 0)
                                {
                                    objfile.Status = "D";
                                }
                                else
                                {
                                    _docfileList.Remove(objfile);
                                }
                                ViewState["AttachFileList"] = _docfileList;
                                BindUpload(_docfileList);
                            }
                            break;
                        }
                    }
                    BindUpload(_docfileList);
                    break;
            }
        }


        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txtAppointTime.Text == "__:__")
            {
                txtAppointTime.Text = "";
            }

            if (ViewState["NamecheckList"] == null)
                objnchk_list = new List<Clstrn_name_check>();
            else
                objnchk_list = (List<Clstrn_name_check>)ViewState["NamecheckList"];

            if (HiddenFieldStatus.Value == "edit")
            {
                var objupdate = objnchk_list[int.Parse(HiddenField_index.Value.ToString())];
                if (objupdate != null)
                {
                    objupdate.tnc_legal = txtnamechk_legal.Text;
                    objupdate.tnc_company_name = txtnamechk_compname.Text;
                    objupdate.tnc_emp_id = txtempid.Text;
                    objupdate.tnc_personal_id = txtpersonalid.Text;
                    objupdate.tnc_hn = txthn.Text;
                    objupdate.tnc_title_name = txttitlename.Text;
                    objupdate.tnc_fname = txtfname.Text;
                    objupdate.tnc_lname = txtlname.Text;
                    objupdate.tnc_gender = rdoF.Checked == true ? 'F' : 'M';
                    objupdate.tnc_site = txtsite.Text;
                    objupdate.tnc_department = txtdept.Text;
                    objupdate.tnc_position = txtposition.Text;
                    if (txtDOB.Text != String.Empty) { objupdate.tnc_dob = Constant.ConvertStringToDate(txtDOB.Text); }
                    if (txtDOB.Text == String.Empty) { objupdate.tnc_dob = null; }
                    objupdate.tnc_age = txtage.Text;
                    objupdate.tnc_program = txtprogram.Text;
                    objupdate.tnc_option = txtoption.Text;
                    objupdate.tnc_appoint_date = txtAppointDate.Text +" "+ txtAppointTime.Text ;
                    objupdate.tnc_remark = txtaddremark.Text;

                    HiddenFieldStatus.Value = "add";
                }
            }
            else if (HiddenFieldStatus.Value == "add")
            {
                Clstrn_name_check objnchk = new Clstrn_name_check();
                HiddenField_index.Value = "0";
                objnchk.tnc_legal = txtnamechk_legal.Text;
                objnchk.tnc_company_name = txtnamechk_compname.Text;
                objnchk.tnc_emp_id = txtempid.Text;
                objnchk.tnc_personal_id = txtpersonalid.Text;
                objnchk.tnc_hn = txthn.Text;
                objnchk.tnc_title_name = txttitlename.Text;
                objnchk.tnc_fname = txtfname.Text;
                objnchk.tnc_lname = txtlname.Text;
                objnchk.tnc_gender = rdoF.Checked == true ? 'F' : 'M';
                objnchk.tnc_site = txtsite.Text;
                objnchk.tnc_department = txtdept.Text;
                objnchk.tnc_position = txtposition.Text;
                if (txtDOB.Text != String.Empty) { objnchk.tnc_dob = Constant.ConvertStringToDate(txtDOB.Text); }
                if (txtDOB.Text == String.Empty) { objnchk.tnc_dob = null; }
                objnchk.tnc_age = txtage.Text;
                objnchk.tnc_program = txtprogram.Text;
                objnchk.tnc_option = txtoption.Text;
                objnchk.tnc_appoint_date = txtAppointDate.Text + " " + txtAppointTime.Text;
                objnchk.tnc_remark = txtaddremark.Text;
                objnchk_list.Add(objnchk);
            }
            ViewState["NamecheckList"] = objnchk_list;
            RepeaterPatient.DataSource = objnchk_list;
            RepeaterPatient.DataBind();

            ClearAddNewNameCheck();
        }

        private void ClearAddNewNameCheck()
        {
            txtempid.Text = txtpersonalid.Text = txthn.Text = txttitlename.Text = txtfname.Text =
            txtlname.Text = txtposition.Text = txtDOB.Text = txtage.Text = txtprogram.Text = txtoption.Text = 
            txtAppointDate.Text=txtAppointTime.Text = txtaddremark.Text = txtsite.Text = txtdept.Text = String.Empty;
        }
        protected void btnAddNewData_Click(object sender, EventArgs e)
        {
            if (HiddenFieldCountClick.Value == "show") 
            {
                btnAddNewData.Text = "Cancel";
                HiddenFieldCountClick.Value = "close";
                PanelAddNewNameCheck.Visible = true;
                ClearAddNewNameCheck();
            }
            else if (HiddenFieldCountClick.Value == "close")
            {
                btnAddNewData.Text = "Add New";
                HiddenFieldCountClick.Value = "show";
                PanelAddNewNameCheck.Visible = false;
            }
        }

        protected void RepeaterPatient_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (ViewState["NamecheckList"] == null)
                objnchk_list = new List<Clstrn_name_check>();
            else
                objnchk_list = (List<Clstrn_name_check>)ViewState["NamecheckList"];

            switch (e.CommandName)
            {
                case "edit":
                    HiddenFieldStatus.Value = "edit";
                   
                    var objedit = objnchk_list[e.Item.ItemIndex]; 

                    if (objedit != null)
                    {
                        HiddenField_index.Value = e.Item.ItemIndex.ToString();
                        txtnamechk_compname.Text = objedit.tnc_company_name;
                        txtnamechk_legal.Text = objedit.tnc_legal;
                        txtempid.Text = objedit.tnc_emp_id;
                        txtpersonalid.Text = objedit.tnc_personal_id;
                        txthn.Text = objedit.tnc_hn;
                        txttitlename.Text = objedit.tnc_title_name;
                        txtfname.Text = objedit.tnc_fname;
                        txtlname.Text = objedit.tnc_lname;
                        if (objedit.tnc_gender == 'M') { rdoM.Checked = true; } else { rdoF.Checked = true; }
                        txtposition.Text = objedit.tnc_position;
                        txtsite.Text = objedit.tnc_site;
                        txtdept.Text = objedit.tnc_department;
                        txtDOB.Text = String.Format("{0:dd/MM/yyyy}", objedit.tnc_dob);
                        txtage.Text = objedit.tnc_age;
                        txtprogram.Text = objedit.tnc_program;
                        txtoption.Text = objedit.tnc_option;
                        string[] strdate = objedit.tnc_appoint_date.Split(' ');
                        if (strdate.Length > 1)
                        {
                            txtAppointDate.Text = strdate[0];// objedit.tnc_appoint_date;
                            txtAppointTime.Text = strdate[1].Replace('.', ':');
                        }
                        else if (strdate.Length == 1)
                        {
                            txtAppointDate.Text = strdate[0];
                        }
                        txtaddremark.Text = objedit.tnc_remark;
                    }
                    
                    btnAddNewData.Text = "Cancel";
                    HiddenFieldCountClick.Value = "close";
                    PanelAddNewNameCheck.Visible = true;

                    break;
                case "del":
                            int tcd_id = Convert1.ToInt32(Request.QueryString["id"]);
                            if (ViewState["PersonalIDList"] == null)
                                    _PersonalIDList = new List<DeletePersonalID>();
                                else
                                    _PersonalIDList = (List<DeletePersonalID>)ViewState["PersonalIDList"] ;

                            _PersonalIDList.Add(new DeletePersonalID() { tcd_id = tcd_id, tnc_id = int.Parse(e.CommandArgument.ToString()) });
                            objnchk_list.RemoveAt(e.Item.ItemIndex);

                            RepeaterPatient.DataSource = objnchk_list;
                            RepeaterPatient.DataBind();
                            break;
            }
            ViewState["NamecheckList"] = objnchk_list;
        }

        private List<DropdowData> getMstTypeDropdown()
        {
            InhToDoListDataContext tdc = new InhToDoListDataContext();
            List<DropdowData> mstType = (from mst in tdc.mst_types
                                         where mst.mst_status == 'A'
                                         select new DropdowData
                                         {
                                             code = mst.mst_id,
                                             name = mst.mst_tname + "/" + mst.mst_ename
                                         }).ToList();
            return mstType;
        }

        protected void btnfindnameth_Click(object sender, EventArgs e)
        {
            if (txtComNameTH.Text != "")
            {
                LoadMstCompanySearchByName(txtComNameTH.Text);
            }
            else
            {
                LoadMstCompanySearchByName(txtComNameEng.Text);
            }

            //RunningDocumentCode();
        }

        /* ***** function autoclick attach file type click */
        protected void chkHPC_CheckedChanged(object sender, EventArgs e)
        {
            SetStatusAttachFile(sender, "HPC");
        }
        protected void chkMKT_CheckedChanged(object sender, EventArgs e)
        {
            SetStatusAttachFile(sender, "MKT");
        }
        protected void chkCollection_CheckedChanged(object sender, EventArgs e)
        {
            SetStatusAttachFile(sender, "Collection");
        }
        protected void chkcontact_CheckedChanged(object sender, EventArgs e)
        {
            SetStatusAttachFile(sender, "contact");
        }
        private void SetStatusAttachFile(object sender,string StrType)
        {
            CheckBox HPCCheckBox = (CheckBox)sender;
            HiddenField IdHiddenField = (HiddenField)HPCCheckBox.Parent.FindControl("mpf_idHiddenField");

            string[] cmdArgu = IdHiddenField.Value.ToString().Split(',');
            int RowID = Convert1.ToInt32(cmdArgu[0]);

            if (ViewState["AttachFileList"] == null)
                _docfileList = new List<PalliativeDocFileEntity>();
            else
                _docfileList = (List<PalliativeDocFileEntity>)ViewState["AttachFileList"];

            var currentselect = _docfileList.Where(x => x.RowID == RowID).FirstOrDefault();
            if (currentselect != null)
            {
                switch (StrType)
                {
                    case "HPC":
                        currentselect.TypeHPC = HPCCheckBox.Checked;
                        break;
                    case "MKT":
                        currentselect.TypeMkt = HPCCheckBox.Checked;
                        break;
                    case "Collection":
                        currentselect.TypeCollection = HPCCheckBox.Checked;
                        break;
                    case "contact":
                        currentselect.TypeContact = HPCCheckBox.Checked;
                        break;
                }
                ViewState["AttachFileList"] = _docfileList;
            }
        }

        /*******************/
        //Add Payment Type
        protected void btnAddPaymentType_Click(object sender, EventArgs e)
        {
            int mst_id = Convert1.ToInt32(DDtype.SelectedValue);//tcd.mst_id = Convert1.ToInt32(DDtype.SelectedValue);
            string mst_name = (DDtype.SelectedItem != null) ? DDtype.SelectedItem.Text : ""; 
            int mpt_id = Convert1.ToInt32(RDPaymentType.SelectedValue);
            string mpt_name = (RDPaymentType.SelectedItem != null) ? RDPaymentType.SelectedItem.Text : "";
            int? tpa_mpt_credit = null;
            if (txtBillAmount.Text.Trim() != "") {tpa_mpt_credit= Convert1.ToInt32(txtBillAmount.Text); }
            string strtpa_mpt_remark = txtBillRemark.Text;

             int  mbm_id  = Convert1.ToInt32(RDMbm_id.SelectedValue);
             string  mbm_name = (RDMbm_id.SelectedItem != null) ? RDMbm_id.SelectedItem.Text : "";


            int mpm_id = Convert1.ToInt32(RDMpm_id.SelectedValue);
            string mpm_name = (RDMpm_id.SelectedItem != null) ? RDMpm_id.SelectedItem.Text : "";

            int mpr_id = Convert1.ToInt32(RDmpr_id.SelectedValue);
            string mpr_name = (RDmpr_id.SelectedItem != null) ? RDmpr_id.SelectedItem.Text : "";

            int mpq_id = Convert1.ToInt32(RDmpq_id.SelectedValue);
            string mpq_name = (RDmpq_id.SelectedItem != null) ? RDmpq_id.SelectedItem.Text : "";

            int? tpa_mpq_credit = null;
            if (txtmqp_credit.Text.Trim() != "") { tpa_mpq_credit = Convert1.ToInt32(txtmqp_credit.Text); }
            int mpn_id = Convert1.ToInt32(RDmpn_id.SelectedValue);
            string mpn_name = (RDmpn_id.SelectedItem != null) ? RDmpn_id.SelectedItem.Text : "";

            int? tpa_mpn_credit = null;
            if (txtmpn_Credit.Text.Trim() != "") { tpa_mpn_credit = Convert1.ToInt32(txtmpn_Credit.Text); }
            int mrm_id = Convert1.ToInt32(RDmrm_id.SelectedValue);
            string mrm_name = (RDmrm_id.SelectedItem != null) ? RDmrm_id.SelectedItem.Text : "";

            char? chtpa_coupon =Convert1.ToChar( RDCoupon.SelectedValue);
            string coupon_name = (RDCoupon.SelectedItem != null) ? RDCoupon.SelectedItem.Text : "";
            string tpa_coupon_remark = txtCouponRemark.Text;

            if (ViewState["paymentType"] == null)
                _paymentType = new List<PaymentGrid>();
            else
                _paymentType = (List<PaymentGrid>)ViewState["paymentType"];
            
            if (btnAddPaymentType.Text == "SAVE")
            {int RowID = Convert1.ToInt32(HiddenPaymentRowID.Value);
                //Save Edit
                PaymentGrid objcurrentPayment = (from t1 in _paymentType
                                                 where t1.RowID == RowID
                                                 select t1).FirstOrDefault();
                if (objcurrentPayment != null)
                {
                    objcurrentPayment.mst_id = mst_id;
                    objcurrentPayment.mst_name = mst_name;
                    objcurrentPayment.mpt_id = mpt_id;
                    objcurrentPayment.mpt_name = mpt_name;
                    objcurrentPayment.tpa_mpt_credit = tpa_mpt_credit;
                    objcurrentPayment.tpa_mpt_remark = strtpa_mpt_remark;
                    objcurrentPayment.mbm_id = mbm_id;
                    objcurrentPayment.mbm_name = mbm_name;
                    objcurrentPayment.mpm_id = mpm_id;
                    objcurrentPayment.mpm_name = mpm_name;
                    objcurrentPayment.mpr_id = mpr_id;
                    objcurrentPayment.mpr_name = mpr_name;
                    objcurrentPayment.mpq_id = mpq_id;
                    objcurrentPayment.mpq_name = mpq_name;
                    objcurrentPayment.tpa_mpq_credit = tpa_mpq_credit;
                    objcurrentPayment.mpn_id = mpn_id;
                    objcurrentPayment.mpn_name = mpn_name;
                    objcurrentPayment.tpa_mpn_credit = tpa_mpn_credit;
                    objcurrentPayment.mrm_id = mrm_id;
                    objcurrentPayment.mrm_name = mrm_name;
                    objcurrentPayment.tpa_coupon = chtpa_coupon;
                    objcurrentPayment.coupon_name = coupon_name;
                    objcurrentPayment.tpa_coupon_remark = tpa_coupon_remark;
                    objcurrentPayment.Status = "E";
                }
                btnAddPaymentType.Text = "Add";
                HiddenPaymentRowID.Value = "";
            }else{
                //ADD
                int rowcount =Convert1.ToInt32( HiddenPaymentRunning.Value);
                _paymentType.Add(new PaymentGrid()
                {
                    RowID = rowcount,
                    tpa_id = 0,
                    mst_id = mst_id,
                    mst_name = mst_name,
                    mpt_id = mpt_id,
                    mpt_name = mpt_name,
                    tpa_mpt_credit = tpa_mpt_credit,
                    tpa_mpt_remark = strtpa_mpt_remark,
                    mbm_id = mbm_id,
                    mbm_name = mbm_name,
                    mpm_id = mpm_id,
                    mpm_name = mpm_name,
                    mpr_id = mpr_id,
                    mpr_name = mpr_name,
                    mpq_id = mpq_id,
                    mpq_name = mpq_name,
                    tpa_mpq_credit = tpa_mpq_credit,
                    mpn_id = mpn_id,
                    mpn_name = mpn_name,
                    tpa_mpn_credit = tpa_mpn_credit,
                    mrm_id = mrm_id,
                    mrm_name = mrm_name,
                    tpa_coupon = chtpa_coupon,
                    coupon_name = coupon_name,
                    tpa_coupon_remark = tpa_coupon_remark,
                    Status = "N"
                });
                rowcount++;
                HiddenPaymentRunning.Value = rowcount.ToString();
            }
            ViewState["paymentType"] = _paymentType;
            ShowGridPaymentType();
            ClearfrmpaymentType();

        }
        private void ClearfrmpaymentType()
        {
            DDtype.SelectedIndex = 0;
            RDPaymentType.ClearSelection();
            txtBillAmount.Text = ""; txtBillAmount.Enabled = false;
            txtmqp_credit.Text = ""; txtmqp_credit.Enabled = false;
            txtBillRemark.Text = ""; 
            txtmpn_Credit.Text = ""; txtmpn_Credit.Enabled = false;
            RDMbm_id.ClearSelection();
            RDMpm_id.ClearSelection();
            RDmpr_id.ClearSelection();
            RDmpq_id.ClearSelection();
            RDmpn_id.ClearSelection();
            RDmrm_id.ClearSelection();
            RDCoupon.ClearSelection();
            txtCouponRemark.Text = "";
            littab5.Text = "<script type='text/javascript'>showpaymentType2();</script>";
        }
        protected void btnDelPaymentType_Click(object sender, EventArgs e)
        {
            if (_paymentType != null)
            {
                ImageButton btnEdit = (ImageButton)sender;
                GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;
                //Delete Old File && New File

                int rowid = Grow.RowIndex;
                PaymentGrid currentselect = _paymentType[rowid];
                if (currentselect != null)
                {
                    if (currentselect.tpa_id > 0)
                    {
                        currentselect.Status = "D";
                        ShowGridPaymentType();
                    }
                }
            }
        }
        protected void GridPaymentType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (ViewState["paymentType"] == null)
                _paymentType = new List<PaymentGrid>();
            else
                _paymentType = (List<PaymentGrid>)ViewState["paymentType"];

            switch (e.CommandName)
            {
                case "DeleteFile":
                    var objcurrentrow = (from f in _paymentType where f.RowID.Equals(Convert.ToInt32(e.CommandArgument)) select f).FirstOrDefault();
                    if (objcurrentrow != null)
                    {
                        if (objcurrentrow.tpa_id > 0)
                        {
                            objcurrentrow.Status = "D";
                        }
                        else
                        {
                            _paymentType.Remove(objcurrentrow);
                        }
                    }
                    break;
                case "EditFile":
                    var objcurrentrowEdit = (from f in _paymentType where f.RowID.Equals(Convert.ToInt32(e.CommandArgument)) select f).FirstOrDefault();
                    if (objcurrentrowEdit != null)
                    {
                        btnAddPaymentType.Text = "SAVE";
                        HiddenPaymentRowID.Value = e.CommandArgument.ToString();
                        DDtype.SelectedValue = objcurrentrowEdit.mst_id.ToString();
                        RDPaymentType.SelectedValue = objcurrentrowEdit.mpt_id.ToString();
                        txtBillAmount.Text = Convert1.ToString(objcurrentrowEdit.tpa_mpt_credit);
                        if (txtBillAmount.Text != "") { txtBillAmount.Enabled = true; } else { txtBillAmount.Enabled = false; }
                        txtBillRemark.Text = objcurrentrowEdit.tpa_mpt_remark;

                        if (objcurrentrowEdit.mbm_id != null)
                        {
                            RDMbm_id.SelectedValue = Convert1.ToString(objcurrentrowEdit.mbm_id);
                        }

                        RDMpm_id.SelectedValue = Convert1.ToString(objcurrentrowEdit.mpm_id);
                        RDmpr_id.SelectedValue = Convert1.ToString(objcurrentrowEdit.mpr_id);
                        RDmpq_id.SelectedValue = Convert1.ToString(objcurrentrowEdit.mpq_id);
                        txtmqp_credit.Text = Convert1.ToString(objcurrentrowEdit.tpa_mpq_credit);
                        if (txtmqp_credit.Text != "") { txtmqp_credit.Enabled = true; } else { txtmqp_credit.Enabled = false; }
                        
                        RDmpn_id.SelectedValue = Convert1.ToString(objcurrentrowEdit.mpn_id);
                        txtmpn_Credit.Text = Convert1.ToString(objcurrentrowEdit.tpa_mpn_credit);
                        if (txtmpn_Credit.Text != "") { txtmpn_Credit.Enabled = true; } else { txtmpn_Credit.Enabled = false; }
                        RDmrm_id.SelectedValue = Convert1.ToString(objcurrentrowEdit.mrm_id);
                        RDCoupon.SelectedValue = Convert1.ToString(objcurrentrowEdit.tpa_coupon);
                        txtCouponRemark.Text = objcurrentrowEdit.tpa_coupon_remark;

                        ClientScript.RegisterStartupScript(typeof(Page), "SymbolError","<script type='text/javascript'>setTimeout(function(){showpaymentType2();},3000)</script>");

                        //littab5.Text = "<script type='text/javascript'>alert('Hello');setTimeout(function(){showpaymentType2();},3000);</script>";
                    }
                break;
            }
            ShowGridPaymentType();
            ViewState["paymentType"] = _paymentType;
       }
        private void ShowGridPaymentType()
        {
            GridPaymentType.DataSource = _paymentType.Where(x => x.Status == "O" || x.Status == "N" || x.Status == "E");
            GridPaymentType.DataBind();
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 2) //tab ที่ 3 index ที่ 2 
            {
                lbCompanyNameEng.Text = txtComNameEng.Text;
                lbCompanyNameTH.Text = txtComNameTH.Text;
                bool IsEdit = false;
                if (HDStatus.Value == "1") { IsEdit = true; }

                btntab3Add.Visible = IsEdit;
                btnAddRemarkmpy.Visible = IsEdit;
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (ViewState["NamecheckList"] == null)
                objnchk_list = new List<Clstrn_name_check>();
            else
                objnchk_list = (List<Clstrn_name_check>)ViewState["NamecheckList"];

            var objsearch = objnchk_list.Where(c => c.tnc_fname.Contains(txtsearch.Text) || c.tnc_lname.Contains(txtsearch.Text)).ToList();
            RepeaterPatient.DataSource = objsearch;
            RepeaterPatient.DataBind();
        }

        //protected void TimerRedirectPage_Tick(object sender, EventArgs e)
        //{
        //    TimerRedirectPage.Enabled = false;
        //    lbmsgAlert.Text = ""; //parent.location.href = parent.location.href;
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "script", "<script type=text/javascript> window.top.location.href = 'frmmktNew.aspx'</script>", false);
        //    //ScriptManager.RegisterStartupScript(this, typeof(string), "script", "<script type=text/javascript> window.location.href = 'frmmktNew.aspx';</script>", false);
        
        //}

        protected void btnDoccate_Click(object sender, EventArgs e)
        {
            if (ViewState["objdoccateList"] == null)
                _objdoccate = new List<AddDocCate>();
            else
                _objdoccate = (List<AddDocCate>)ViewState["objdoccateList"];

            if (dlDocCate.SelectedIndex == 0) { return; }

            var chkdata = _objdoccate.Where(c => c.mdc_tname.Equals(dlDocCate.SelectedItem.Text)).FirstOrDefault();
            if (chkdata != null) { return; }

            _objdoccate.Add(new AddDocCate {mdc_id = int.Parse(dlDocCate.SelectedValue) ,mdc_tname = dlDocCate.SelectedItem.Text });

            ViewState["objdoccateList"] = _objdoccate;
            RepeaterDocCate.DataSource = _objdoccate;
            RepeaterDocCate.DataBind();

        }
        protected void RepeaterDocCate_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                if (ViewState["objdoccateList"] == null)
                    _objdoccate = new List<AddDocCate>();
                else
                    _objdoccate = (List<AddDocCate>)ViewState["objdoccateList"];

                _objdoccate.RemoveAt(e.Item.ItemIndex);

                ViewState["objdoccateList"] = _objdoccate;
                RepeaterDocCate.DataSource = _objdoccate;
                RepeaterDocCate.DataBind();
            }
        }

        protected void btnsite_Click(object sender, EventArgs e)
        {
            if (ViewState["objSiteList"] == null)
                _objsite = new List<AddSite>();
            else
                _objsite = (List<AddSite>)ViewState["objSiteList"];

            if (dlSite.SelectedIndex == 0) { return; }
            var chkdata = _objsite.Where(c => c.mcl_tname.Equals(dlSite.SelectedItem.Text)).FirstOrDefault();
            if (chkdata != null) { return; }
            _objsite.Add(new AddSite { mcl_id = int.Parse(dlSite.SelectedValue), mcl_tname = dlSite.SelectedItem.Text });

            ViewState["objSiteList"] = _objsite;
            RepeaterSite.DataSource = _objsite;
            RepeaterSite.DataBind();

        }
        protected void RepeaterSite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                if (ViewState["objSiteList"] == null)
                    _objsite = new List<AddSite>();
                else
                    _objsite = (List<AddSite>)ViewState["objSiteList"];

                _objsite.RemoveAt(e.Item.ItemIndex);

                ViewState["objSiteList"] = _objsite;
                RepeaterSite.DataSource = _objsite;
                RepeaterSite.DataBind();
            }
        }

        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            if (ViewState["AddPlanList"] == null)
                _objplan = new List<AddPlan>();
            else
                _objplan = (List<AddPlan>)ViewState["AddPlanList"];

            GetDataTrakCare ws = new GetDataTrakCare();
            DataTable dt =   ws.genTableListPaymentAgreement(txt_payor.Text);
            _objplan = new List<AddPlan>();
            foreach(DataRow dr in dt.Rows)
            {
                _objplan.Add(new AddPlan { tpl_code = dr["AUXIT_Code"].ToString(), tpl_name = dr["AUXIT_Desc"].ToString() });
            }
            ViewState["AddPlanList"] = _objplan;
            RepeaterPlan.DataSource = _objplan;
            RepeaterPlan.DataBind();
        }
        protected void RepeaterPlan_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "Del")
            {
                if (ViewState["AddPlanList"] == null)
                    _objplan = new List<AddPlan>();
                else
                    _objplan = (List<AddPlan>)ViewState["AddPlanList"];

                _objplan.RemoveAt(e.Item.ItemIndex);
                ViewState["AddPlanList"] = _objplan;
                RepeaterPlan.DataSource = _objplan;
                RepeaterPlan.DataBind();
            }
        }

        //Contact Person
        protected void btnaddContact_Click(object sender, EventArgs e)
        {
            if (ViewState["contact_personList"] == null)
                _objcontact_person = new List<AddConatactPerson>();
            else
                _objcontact_person = (List<AddConatactPerson>)ViewState["contact_personList"];

            _objcontact_person.Add(new AddConatactPerson { Name = txtConPer.Text, Tel = txtConTel.Text, Fax = txtConFax.Text, Email = txtConEmail.Text });

            ViewState["contact_personList"] = _objcontact_person;
            RepeaterContacrPerson.DataSource = _objcontact_person;
            RepeaterContacrPerson.DataBind();
            txtConPer.Text = "";
            txtConTel.Text = "";
            txtConFax.Text = "";
            txtConEmail.Text = "";
        }
        protected void RepeaterContacrPerson_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                if (ViewState["contact_personList"] == null)
                    _objcontact_person = new List<AddConatactPerson>();
                else
                    _objcontact_person = (List<AddConatactPerson>)ViewState["contact_personList"];

                _objcontact_person.RemoveAt(e.Item.ItemIndex);
                ViewState["contact_personList"] = _objcontact_person;
                RepeaterContacrPerson.DataSource = _objcontact_person;
                RepeaterContacrPerson.DataBind();
            }
        }

        protected void btnaddbill_Click(object sender, EventArgs e)
        {
            if (ViewState["contact_billList"] == null)
                _objcontact_bill = new List<AddConatactPersonBill>();
            else
                _objcontact_bill = (List<AddConatactPersonBill>)ViewState["contact_billList"];

            _objcontact_bill.Add(new AddConatactPersonBill { Name = txtConBill.Text, Tel = txtConBillTel.Text, Fax = txtConBillFax.Text, Email = txtConBillEmail.Text });

            ViewState["contact_billList"] = _objcontact_bill;
            RepeaterContactBill.DataSource = _objcontact_bill;
            RepeaterContactBill.DataBind();
            txtConBill.Text = "";
            txtConBillTel.Text = "";
            txtConBillFax.Text = "";
            txtConBillEmail.Text = "";
        }
        protected void RepeaterContactBill_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                if (ViewState["contact_billList"] == null)
                    _objcontact_bill = new List<AddConatactPersonBill>();
                else
                    _objcontact_bill = (List<AddConatactPersonBill>)ViewState["contact_billList"];

                _objcontact_bill.RemoveAt(e.Item.ItemIndex);
                ViewState["contact_billList"] = _objcontact_bill;
                RepeaterContactBill.DataSource = _objcontact_bill;
                RepeaterContactBill.DataBind();
            }
        }

        protected void btnmarket_Click(object sender, EventArgs e)
        {
            if (ViewState["contact_MKTList"] == null)
                _objcontact_MKT = new List<AddConatactPersonMKT>();
            else
                _objcontact_MKT = (List<AddConatactPersonMKT>)ViewState["contact_MKTList"];

            _objcontact_MKT.Add(new AddConatactPersonMKT { Name = txtMktName.Text, Tel = txtMkt_Tel.Text, Fax = txtMkt_Fax.Text, Email = txtMktEmail.Text });

            ViewState["contact_MKTList"] = _objcontact_MKT;
            RepeaterContactMKT.DataSource = _objcontact_MKT;
            RepeaterContactMKT.DataBind();
            txtMktName.Text = "";
            txtMkt_Tel.Text = "";
            txtMkt_Fax.Text = "";
            txtMktEmail.Text = "";
        }
        protected void RepeaterContactMKT_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                if (ViewState["contact_MKTList"] == null)
                    _objcontact_MKT = new List<AddConatactPersonMKT>();
                else
                    _objcontact_MKT = (List<AddConatactPersonMKT>)ViewState["contact_MKTList"];

                _objcontact_MKT.RemoveAt(e.Item.ItemIndex);
                ViewState["contact_MKTList"] = _objcontact_MKT;
                RepeaterContactMKT.DataSource = _objcontact_MKT;
                RepeaterContactMKT.DataBind();
            }
        }

        //end Contact Person
    }
    
}

