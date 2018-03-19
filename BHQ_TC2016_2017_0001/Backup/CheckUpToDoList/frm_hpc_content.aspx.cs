using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class frm_hpc_content : System.Web.UI.Page
    {
        InhToDoListDataContext dbc = new InhToDoListDataContext();
        private static  List<trn_name_check> objnchk_list = null;
        private static List<PaymentType_hpc> _paymentType = null;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            //เช็คสิทธิการเข้าใช้ by Yee
            Constant.CheckPolicy("HPC");
            if (Request.QueryString["id"] == null || Request.QueryString["id"] == String.Empty) { return; }

            

            if (!Page.IsPostBack) {
                _paymentType = new List<PaymentType_hpc>();
                objnchk_list = new List<trn_name_check>();
                this.LoadData(Convert.ToInt16(Request.QueryString["id"]));
                TabContainer1.ActiveTabIndex = 2;

                showReadOnly("HPC");
            }
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
                    if (currentuser.mul_permit == 'W')
                    {

                        btnEdit.Visible = true;

                    }
                }
                else
                {
                    if (Constant.CurrentUserLogin == "Admin") { return; }
                }
            }
        }


        private void LoadData(int compid)
        {
            var objcomp = (from cus in dbc.trn_company_details where cus.tcd_id == compid select cus).FirstOrDefault(); //where form?
            if (objcomp != null)
            {
                lblremark_compname_th.Text = objcomp.tcd_tname;
                lblremark_compname_en.Text = objcomp.tcd_ename;
                lblcompany_Code.Text = objcomp.tcd_code;
                lbldeptowner.Text = objcomp.tcd_type;
                lbDocumentNo.Text = objcomp.tcd_document_no;
                lblcompany_th.Text = objcomp.tcd_tname;
                lblcompany_en.Text = objcomp.tcd_ename;


                //สถานที่ตรวจ
                var objchklocation = (from location in dbc.trn_checkup_locations
                                      join mstlocation in dbc.mst_checkup_locations on location.mcl_id equals mstlocation.mcl_id
                                      where location.tcd_id == objcomp.tcd_id
                                      select new { tname_location = mstlocation.mcl_tname }).ToList();
                if (objchklocation.Count > 0)
                {
                    lblloc_remark.Text = objcomp.tcd_location_remark;

                    foreach (var data in objchklocation)
                    {
                        lblnolocation.Text = "";

                        switch (data.tname_location)
                        {
                            case "IMS":
                                chkims.Checked = true;
                                chkims.Visible = true;
                                break;
                            case "OBG":
                                chkbcancer.Checked = true;
                                chkbcancer.Visible = true;
                                break;
                            case "JMS":
                                chkjms.Checked = true;
                                chkjms.Visible = true;
                                break;
                            case "HPC1":
                                chkhpc1.Checked = true;
                                chkhpc1.Visible = true;
                                break;
                            case "HPC2":
                                chkhpc2.Checked = true;
                                chkhpc2.Visible = true;
                                break;
                            case "HPC3":
                                chkhpc3.Checked = true;
                                chkhpc3.Visible = true;
                                break;
                            case "Other":
                                chkOth.Checked = true;
                                chkOth.Visible = true;
                                break;
                        }

                    }
                }
                else
                {
                    lblnolocation.Text = "-";
                }

                lbldate_s_contract.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_date_from);
                lbldate_e_contract.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_date_to);

                //package and option Order set
                var objpackage = (from package in dbc.trn_package_details
                                  where package.tcd_id == objcomp.tcd_id
                                  select new
                                  {
                                      package_order = package.tpd_order_desc,
                                      package_price = package.tpd_price,
                                      package_payment = (from mpt in dbc.mst_payment_types
                                                         where mpt.mpt_id == package.mpt_id
                                                         select mpt.mpt_tname).FirstOrDefault(),
                                      package_credit = package.tpd_limit_credit,
                                      package_s_date = String.Format("{0:dd/MM/yyyy}", package.tpd_date_from),
                                      package_e_date = String.Format("{0:dd/MM/yyyy}", package.tpd_date_to),
                                      package_type = package.tpd_order_type
                                  }).ToList();
                if (objpackage != null)
                {
                    RepeaterPackage.DataSource = objpackage.Where(c => c.package_type == "Order Set");
                    RepeaterPackage.DataBind();

                    RepeaterOption.DataSource = objpackage.Where(c => c.package_type == "Option");
                    RepeaterOption.DataBind();
                }



                var old_paymentType = (from t1 in dbc.trn_payments
                                       where t1.tcd_id == objcomp.tcd_id
                                       select new
                                       {
                                           mst_id = t1.mst_id,
                                           mst_nameTh = (t1.mst_type != null) ? t1.mst_type.mst_tname : "",
                                           mst_nameEn = (t1.mst_type != null) ? t1.mst_type.mst_ename : "",
                                           mpt_id = t1.mpt_id,
                                           mpt_nameTh = (t1.mst_payment_type != null) ? t1.mst_payment_type.mpt_tname : "",
                                           mpt_nameEn = (t1.mst_payment_type != null) ? t1.mst_payment_type.mpt_ename : "",
                                           tpa_mpt_credit = t1.tpa_mpt_credit,
                                           tpa_mpt_remark = t1.tpa_mpt_remark,
                                           mbm_id = t1.mbm_id,
                                           mbm_nameTh = (t1.mst_billing_method != null) ? t1.mst_billing_method.mbm_tname : "",
                                           mbm_nameEn = (t1.mst_billing_method != null) ? t1.mst_billing_method.mbm_ename : "",
                                           mpm_id = t1.mpm_id,
                                           mpm_nameTh = (t1.mst_payment_main != null) ? t1.mst_payment_main.mpm_tname : "",
                                           mpm_nameEn = (t1.mst_payment_main != null) ? t1.mst_payment_main.mpm_ename : "",
                                           mpr_id = t1.mpr_id,
                                           mpr_nameTh = (t1.mst_payment_rate != null) ? t1.mst_payment_rate.mpr_tname : "",
                                           mpr_nameEn = (t1.mst_payment_rate != null) ? t1.mst_payment_rate.mpr_ename : "",
                                           mpq_id = t1.mpq_id,
                                           mpq_nameTh = (t1.mst_payment_quatation != null) ? t1.mst_payment_quatation.mpq_tname : "",
                                           mpq_nameEn = (t1.mst_payment_quatation != null) ? t1.mst_payment_quatation.mpq_ename : "",
                                           tpa_mpq_credit = t1.tpa_mpq_credit,
                                           mpn_id = t1.mpn_id,
                                           mpn_nameTh = (t1.mst_payment_nquatation != null) ? t1.mst_payment_nquatation.mpn_tname : "",
                                           mpn_nameEn = (t1.mst_payment_nquatation != null) ? t1.mst_payment_nquatation.mpn_ename : "",
                                           tpa_mpn_credit = t1.tpa_mpn_credit,
                                           mrm_id = t1.mrm_id,
                                           mrm_nameTh = (t1.mst_receive_medicine != null) ? t1.mst_receive_medicine.mrm_tname : "",
                                           mrm_nameEn = (t1.mst_receive_medicine != null) ? t1.mst_receive_medicine.mrm_ename : "",
                                           tpa_coupon = t1.tpa_coupon,
                                           coupon_name = (t1.tpa_coupon == 'I') ? "ให้" : "ไม่ให้",
                                           tpa_coupon_remark = t1.tpa_coupon_remark,
                                           Status = "O",
                                           tpa_id = t1.tpa_id
                                       }).ToList();
                _paymentType = (from t1 in old_paymentType
                                select new PaymentType_hpc
                                {
                                    mst_name = t1.mst_nameTh + "/" + t1.mst_nameEn,
                                    mpt_name = t1.mpt_nameTh + "/" + t1.mpt_nameEn,
                                    tpa_mpt_credit = t1.tpa_mpt_credit,
                                    tpa_mpt_remark = t1.tpa_mpt_remark,
                                    mbm_name = t1.mbm_nameTh + "/" + t1.mbm_nameEn,
                                    mpm_name = t1.mpm_nameTh + "/" + t1.mpm_nameEn,
                                    mpr_name = t1.mpr_nameTh + "/" + t1.mpr_nameEn,
                                    mpq_name = t1.mpq_nameTh + "/" + t1.mpq_nameEn,
                                    tpa_mpq_credit = t1.tpa_mpq_credit,
                                    mpn_name = t1.mpn_nameTh + "/" + t1.mpn_nameEn,
                                    tpa_mpn_credit = t1.tpa_mpn_credit,
                                    mrm_name = t1.mrm_nameTh + "/" + t1.mrm_nameEn,
                                    tpa_coupon = t1.tpa_coupon,
                                    coupon_name = (t1.tpa_coupon == 'I') ? "ให้" : "ไม่ให้",
                                    tpa_coupon_remark = t1.tpa_coupon_remark,
                                }).ToList();

                RepeaterPaymentType.DataSource = _paymentType;
                RepeaterPaymentType.DataBind();

                lblpayor.Text = objcomp.tcd_payor;

                //condition service employee
                var objmcs = (from mst_mcs in dbc.mst_condition_services
                              join trn_mcs in dbc.trn_condition_services on mst_mcs.mcs_id equals trn_mcs.mcs_id
                              where mst_mcs.mcs_status == 'A' && (trn_mcs.tcd_id == objcomp.tcd_id) && (trn_mcs.tcs_type == "EM" || trn_mcs.tcs_type == "EX")
                              select new{ 
                                  Namex=mst_mcs.mcs_tname+ "/" + mst_mcs.mcs_ename ,
                                  Typex=trn_mcs.tcs_type
                              }).ToList();
                if (objmcs.Count > 0)
                {
                    foreach (var data in objmcs)
                    {
                        if (data.Typex == "EM")
                        {
                            if (lblcondition_service.Text == "")
                            {
                                lblcondition_service.Text += data.Namex;
                            }
                            else
                            {
                                lblcondition_service.Text += "," +data.Namex;
                            }
                        }
                        if(data.Typex=="EX"){
                            if (lblcondition_serviceEmployee.Text == "")
                            {
                                lblcondition_serviceEmployee.Text += data.Namex ;
                            }
                            else
                            {
                                lblcondition_serviceEmployee.Text +=","+  data.Namex ;
                            }
                        }
                    }
                }

                //family welfa
                lblfam_welfa.Text = objcomp.tcd_family_welfare;

                //contact
                var objcontact = (from tcp in dbc.trn_contact_persons
                                  where tcp.mst_contact_type.mct_code == 'C' && tcp.tcd_id == objcomp.tcd_id
                                  select new
                                  {
                                      contact_name = tcp.tcp_name,
                                      contact_tel = tcp.tcp_tel,
                                      contact_fax = tcp.tcp_fax,
                                      contact_email = tcp.tcp_email
                                  }).ToList();
                if (objcontact.Count > 0)
                {
                    RepeaterContact.DataSource = objcontact;
                    RepeaterContact.DataBind();
                  
                }
              

                lblresult_address.Text = objcomp.tcd_address;
                lblresult_subdistrict.Text = objcomp.tcd_district;
                lblresult_district.Text = objcomp.tcd_tambon;
                lblresult_province.Text = objcomp.tcd_province;
                lblresult_zipcode.Text = objcomp.tcd_postcode;

                //List<string>
                List<string> medicalRpt = (from tmr in dbc.trn_medical_reports
                                  join mmr in dbc.mst_medical_reports
                                  on tmr.mmr_id equals mmr.mmr_id
                                  where tmr.tcd_id == compid
                                           select (mmr.mmr_tname.Trim() + "/" + mmr.mmr_ename.Trim()) + " " + tmr.tmr_rep_remark).ToList();

                lblresult.Text = string.Join(", ", medicalRpt);

                switch (objcomp.tcd_send_rep_real)
                {
                    case "Y":
                        lblsend_ref_real.Text = "รับกลับ";
                        break;
                    case "N":
                        lblsend_ref_real.Text = "ไม่รับกลับ";
                        if (objcomp.tcd_send_rep_flag == "C")
                        {
                            lblsend_ref_real.Text += "(ส่งบริษัท)";
                        }
                        else
                        {
                            lblsend_ref_real.Text += "(ส่งบ้าน)";
                        }
                        break;
                }
                switch (objcomp.tcd_send_rep_copy)
                {
                    case "H":
                        lblsend_rep_copy.Text = "ส่งบ้าน";
                        break;
                    case "N":
                        lblsend_rep_copy.Text = "ไม่ต้องการ";
                        break;
                    case "C":
                        lblsend_rep_copy.Text = "ส่งบริษัท";
                        break;
                }

                //emp mtk name
                var objtcp = (from tcp in dbc.trn_contact_persons
                              where tcp.tcd_id == objcomp.tcd_id && tcp.mst_contact_type.mct_code == 'M'
                              select new
                              {
                                  mtk_name = tcp.tcp_name,
                                  mtk_tel = tcp.tcp_tel,
                                  mtk_fax = tcp.tcp_fax,
                                  mtk_email = tcp.tcp_email
                              }).ToList();
                if (objtcp.Count > 0)
                {
                    RepeaterMTK.DataSource = objtcp;
                    RepeaterMTK.DataBind();
                  
                }
               
                lbllastupdate.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_update_date);
                lblupdatebyname.Text = objcomp.mul_user_login ;

                //attach file
                var objattach = Constant.GetObjAttachFile(objcomp.tcd_id,"HPC");//(from file in dbc.trn_attach_files where file.taf_user_type == 'H' && file.tcd_id == objcomp.tcd_id select new { file_name = file.taf_file_name, file_path = file.taf_path_name }).ToList();
                if (objattach.Count > 0)
                {
                    

                    RepeaterFile.DataSource = objattach;
                    RepeaterFile.DataBind();
                  
                }


                divremark.InnerHtml = Server.HtmlDecode(objcomp.tcd_remark);

                //search name check
                objnchk_list = (from tnc in dbc.trn_name_checks
                              where tnc.tcd_id == objcomp.tcd_id
                              select tnc).ToList();
                if (objnchk_list != null)
                {
                    RepeaterPatient.DataSource = objnchk_list;
                    RepeaterPatient.DataBind();
                }

            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (objnchk_list != null)
            {
                var objsearch = objnchk_list.Where(c => c.tnc_fname.Contains(txtsearch.Text) || c.tnc_lname.Contains(txtsearch.Text)).ToList();
                RepeaterPatient.DataSource = objsearch;
                RepeaterPatient.DataBind();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmmktdata.aspx?edit=1&status=1&id=" + Request.QueryString["id"] + "&active=" + Request.QueryString["active"]);
        }

       
    }

        public class PaymentType_hpc
    {
        public string mst_name { get; set; }
        public string mpt_name { get; set; }
        public int?  tpa_mpt_credit { get; set; }
        public string tpa_mpt_remark { get; set; }
        public string mbm_name { get; set; }
        public string mpm_name { get; set; }
        public string mpr_name { get; set; }
        public string mpq_name { get; set; }
        public int? tpa_mpq_credit { get; set; }
        public string mpn_name { get; set; }
        public int? tpa_mpn_credit { get; set; }
        public string mrm_name { get; set; }
        public char? tpa_coupon { get; set; }
        public string coupon_name { get; set; }
        public string tpa_coupon_remark { get; set; }
    }
}