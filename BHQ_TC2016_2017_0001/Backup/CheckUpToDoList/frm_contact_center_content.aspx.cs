using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class frm_contact_center_content : System.Web.UI.Page
    {
        InhToDoListDataContext dbc = new InhToDoListDataContext();
        private static List<trn_name_check> objnchk_list = null;
        private static List<PaymentType_ctc> _paymentType = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            
            //เช็คสิทธิการเข้าใช้ by Yee
            Constant.CheckPolicy("CTC");
            if (Request.QueryString["id"] == null || Request.QueryString["id"] == String.Empty) { return; }

            if (!Page.IsPostBack)
            {
                _paymentType = new List<PaymentType_ctc>();
                objnchk_list = new List<trn_name_check>();
                this.LoadData(Convert.ToInt16(Request.QueryString["id"]));
                showReadOnly("CTC");
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

                lbldoccode.Text = objcomp.tcd_document_no;
                lblcompcode.Text = objcomp.tcd_code;
                lblcompany_th.Text = objcomp.tcd_tname;
                lblcompany_en.Text = objcomp.tcd_ename;
                lbldeptowner.Text = objcomp.tcd_type;

                

                //check-up location
                var objmcl = (from mst_mcl in dbc.mst_checkup_locations
                              join trn_mcl in dbc.trn_checkup_locations on mst_mcl.mcl_id equals trn_mcl.mcl_id
                              where trn_mcl.tcd_id == objcomp.tcd_id
                              select new
                              {
                                  ename = mst_mcl.mcl_ename,
                                  tname = mst_mcl.mcl_tname
                              }).ToList();
                List<string> tempLocation = new List<string>();
                if (objmcl.Count > 0) tempLocation.Add(string.Join(", ", objmcl.Select(x => x.ename + "/" + x.tname)));
                if (!string.IsNullOrEmpty(objcomp.tcd_location_remark)) tempLocation.Add(objcomp.tcd_location_remark);
                lblchk_location.Text = string.Join(", ", tempLocation);
                /*
                if (objmcl != null)
                {
                    lblchk_location.Text = objmcl.mcl_tname; //+ "/" + objmcl.mcl_ename;
                }
                */

                lbldate_s_contract.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_date_from);
                lbldate_e_contract.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_date_to);

                //package and option
                //var objpackage = (from package in dbc.trn_package_details
                //                  where package.tcd_id == objcomp.tcd_id
                //                  select new
                //                  {
                //                      package_order = package.tpd_order_desc,
                //                      package_price = package.tpd_price,
                //                      package_payment = (from mpt in dbc.mst_payment_types
                //                                         where mpt.mpt_id == package.mpt_id
                //                                         select mpt.mpt_tname).FirstOrDefault(),
                //                      package_credit = package.tpd_limit_credit,
                //                      package_s_date = String.Format("{0:dd/MM/yyyy}", package.tpd_date_from),
                //                      package_e_date = String.Format("{0:dd/MM/yyyy}",package.tpd_date_to),
                //                      package_rd = (from rd in dbc.trn_package_request_doctors where rd.tpd_id == package.tpd_id select rd.trd_doc_tname).FirstOrDefault(),
                //                      package_dc = (from dc in dbc.trn_package_doctor_cats where dc.tpd_id == package.tpd_id select dc.mst_doctor_cat.mdc_tname).FirstOrDefault()
                //                  }).ToList();

                //if (objpackage != null)
                //{
                //    RepeaterPackage.DataSource = objpackage;
                //    RepeaterPackage.DataBind();
                //}

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
                                      package_rd = (from rd in dbc.trn_package_request_doctors where rd.tpd_id == package.tpd_id select rd.trd_doc_tname).FirstOrDefault(),
                                      package_dc = (from dc in dbc.trn_package_doctor_cats where dc.tpd_id == package.tpd_id select dc.mst_doctor_cat.mdc_tname).FirstOrDefault(),
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
                                select new PaymentType_ctc
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

                lblwelfare.Text = objcomp.tcd_family_welfare;


                //condition service employee, executive (edit code by morn)
                var objCond = (from mst_mcs in dbc.mst_condition_services
                               join trn_mcs in dbc.trn_condition_services on mst_mcs.mcs_id equals trn_mcs.mcs_id
                               where mst_mcs.mcs_status == 'A' && (trn_mcs.tcd_id == objcomp.tcd_id) && (trn_mcs.tcs_type == "EM" || trn_mcs.tcs_type == "EX")
                               select new
                               {
                                   type = trn_mcs.tcs_type,
                                   ename = mst_mcs.mcs_ename,
                                   tname = mst_mcs.mcs_tname
                               }).ToList();
                lblcond_service.Text = string.Join(", ", objCond.Where(x => x.type == "EM").Select(x => x.ename + '/' + x.tname));
                lblcond_service_executive.Text = string.Join(", ", objCond.Where(x => x.type == "EX").Select(x => x.ename + '/' + x.tname));

                List<trn_company_request_doctor> tcrd = dbc.trn_company_request_doctors.Where(x => x.tcd_id == objcomp.tcd_id).ToList();
                lbldoctor.Text = String.Join(", ", tcrd.Select(x => x.tcr_doc_ename + '/' + x.tcr_doc_tname));

                //doctor type (edit code by morn)
                var objmdc = (from mst_mdc in dbc.mst_doctor_cats
                              join trn_mdc in dbc.trn_package_doctor_cats on mst_mdc.mdc_id equals trn_mdc.mdc_id
                              where trn_mdc.trn_package_detail.tcd_id == objcomp.tcd_id
                              select new
                              {
                                  ename = mst_mdc.mdc_ename,
                                  tname = mst_mdc.mdc_tname
                              }).ToList();
                lbldoctor_type.Text = String.Join(", ", objmdc.Select(x => x.ename + '/' + x.tname));
                /*
                if (objmdc != null)
                {
                    lbldoctor_type.Text = objmdc.mdc_tname; // + "/" + objmdc.mdc_ename;
                }
                */

                //สถานที่ตรวจ
                var objchklocation = (from location in dbc.trn_checkup_locations
                                      join mstlocation in dbc.mst_checkup_locations on location.mcl_id equals mstlocation.mcl_id
                                      where location.tcd_id == objcomp.tcd_id
                                      select new { tname_location = mstlocation.mcl_tname }).ToList();
                if (objchklocation.Count > 0)
                {
                    foreach (var data in objchklocation)
                    {
                        lblloc_remark.Text = objcomp.tcd_location_remark;

                        switch (data.tname_location)
                        {
                            case "IMS":
                                chkims.Checked = true;
                                break;
                            case "OBG":
                                chkbcancer.Checked = true;
                                break;
                            case "JMS":
                                chkjms.Checked = true;
                                break;
                            case "HPC1":
                                chkhpc1.Checked = true;
                                break;
                            case "HPC2":
                                chkhpc2.Checked = true;
                                break;
                            case "HPC3":
                                chkhpc3.Checked = true;
                                break;
                            default :
                                chkoth.Checked = true;
                                break;
                        }
                    }
                }

                //mtk staff name
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
                lbllastupdateby.Text = objcomp.mul_user_login;

                var objattach = Constant.GetObjAttachFile(objcomp.tcd_id,"CTC");//(from file in dbc.trn_attach_files where file.taf_user_type == 'H' && file.tcd_id == objcomp.tcd_id select new { file_name = file.taf_file_name, file_path = file.taf_path_name }).ToList();
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
                if (objnchk_list!=null)
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmmktdata.aspx?edit=1&status=1&id=" + Request.QueryString["id"] + "&active=" + Request.QueryString["active"]);
        }



    }

    public class PaymentType_ctc
    {
        public string mst_name { get; set; }
        public string mpt_name { get; set; }
        public int? tpa_mpt_credit { get; set; }
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