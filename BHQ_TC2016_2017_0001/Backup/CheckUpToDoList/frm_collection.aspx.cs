using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
namespace CheckUpToDoList
{
    public partial class frm_collection : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Page.ClientScript.RegisterClientScriptInclude("cal", "calendar/calendar.js");
            //Constant.SetCalendar_img(ref txtstart, txtstart.ClientID);
            //Constant.SetCalendar_img(ref txtEndDate, txtEndDate.ClientID);

            //if (!Page.IsPostBack)
            //{
            //    LoadType();
            //    LoadLacation();
            //    LoadCompany();
            //    //this.LoadData();
            //}
            
        }

        //private void LoadType()
        //{
        //    var objmstid = (from tid in dbc.mst_types select new { id = tid.mst_id, name = tid.mst_tname }).ToList();
        //    DDType.DataTextField = "name";
        //    DDType.DataValueField = "id";
        //    DDType.DataSource = objmstid;
        //    DDType.DataBind();
        //    DDType.SelectedIndex = 0;
        //}

        //private void LoadLacation()
        //{
        //    var objloc = (from loc in dbc.mst_checkup_locations select new { id = loc.mcl_id, name = loc.mcl_tname + "/" + loc.mcl_ename }).ToList();
        //    DDLocation.DataTextField = "name";
        //    DDLocation.DataValueField = "id";
        //    DDLocation.DataSource = objloc;
        //    DDLocation.DataBind();
        //}

        //private void LoadCompany()
        //{
        //    //sel company master
        //    var objmstcomp = (from comp in dbc.trn_company_details
        //                      select new { cid = comp.tcd_id, cname = comp.tcd_tname + "/" + comp.tcd_ename}).ToList();
        //    gvsearch.DataSource = objmstcomp;
        //    gvsearch.DataBind();

        //}
        //private int irow()
        //{
        //    int i = gvsearch.Rows.Count;
        //    return i;
        //}

        //private void LoadData(int compname)
        //{
        //    ,DateTime compdateform,int type
        //    sel company detail from code
        //    && (cus.tcd_date_from >= compdateform && cus.tcd_date_to <= compdateform) && cus.mst_id == type 
        //    var objcomp = (from cus in dbc.trn_company_details where cus.tcd_id == compname   
        //                   select cus).FirstOrDefault(); //where form?
        //    if (objcomp != null)
        //    {
        //        lblcompany_th.Text = objcomp.tcd_tname;
        //        lblcompany_en.Text = objcomp.tcd_ename;
        //        lblcompany_address.Text = objcomp.tcd_address;
        //        lblcompany_amphur.Text = objcomp.tcd_result_district;
        //        lblcompany_tumbon.Text = objcomp.tcd_result_tambon;
        //        lblcompany_province.Text = objcomp.tcd_province;
        //        lblcompany_postcode.Text = objcomp.tcd_postcode;

        //        contact
        //        var objcontact = (from tcp in dbc.trn_contact_persons
        //                          where tcp.tcp_type == 'C' && tcp.tcd_id == objcomp.tcd_id
        //                          select new { contact_name = tcp.tcp_fname + " " + tcp.tcp_lname,
        //                          contact_tel = tcp.tcp_tel,
        //                          contact_fax = tcp.tcp_fax,
        //                          contact_email = tcp.tcp_email}).ToList();
        //        if (objcontact.Count > 0)
        //        {
        //            gvcontact.DataSource = objcontact;
        //            gvcontact.DataBind();
        //        }
        //        else
        //        {
        //            lblsms_contact.Visible = true;
        //        }

        //        emp mtk name
        //        var objtcp = (from tcp in dbc.trn_contact_persons
        //                      where tcp.tcd_id == objcomp.tcd_id && tcp.tcp_type == 'M'
        //                      select new
        //                      {
        //                          mtk_name = tcp.tcp_fname + " " + tcp.tcp_lname,
        //                          mtk_tel = tcp.tcp_tel,
        //                          mtk_fax = tcp.tcp_fax,
        //                          mtk_email = tcp.tcp_email
        //                      }).ToList();
        //        if (objtcp.Count > 0)
        //        {
        //            gvmtkstaff.DataSource = objtcp;
        //            gvmtkstaff.DataBind();
        //        }
        //        else
        //        {
        //            lblsms_contact_mtk.Visible = true;
        //        }

        //        lblcontact_s.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_date_from);
        //        lblcontact_e.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_date_to);

        //        lblpayor.Text = objcomp.tcd_payor;


        //        plan
        //        var objplan = (from plan in dbc.trn_plans where plan.tcd_id == objcomp.tcd_id select new {plan_no = 0, plan_name = plan.tpl_name, plan_action = "..." }).ToList();
        //        if (objplan.Count > 0)
        //        {
        //            gvplan.DataSource = objplan;
        //            gvplan.DataBind();
        //        }
        //        else
        //        {
        //            lblsms_plan.Visible = true;
        //        }



        //        comp name billing
        //        lblcomp_name_bill.Text = objcomp.tcd_bill_company;

        //        comp address billing
        //        lblcomp_addr_bill.Text = objcomp.tcd_bill_address + String.Empty + objcomp.tcd_bill_tambon + String.Empty + objcomp.tcd_bill_district + String.Empty + objcomp.tcd_bill_province + String.Empty + objcomp.tcd_bill_postcode;
           
        //        contact person billing
        //        var objcontactbill = (from tcp in dbc.trn_contact_persons
        //                              where tcp.tcp_type == 'B' && tcp.tcd_id == objcomp.tcd_id
        //                              select new { contact_bill_name = tcp.tcp_fname + " " + tcp.tcp_lname,
        //                                           contact_bill_tel = tcp.tcp_tel,
        //                                           contact_bill_fax = tcp.tcp_fax,
        //                                           contact_bill_email = tcp.tcp_email
        //                              }).ToList();
        //        if (objcontact.Count > 0)
        //        {
        //            gv_contact_bill.DataSource = objcontactbill;
        //            gv_contact_bill.DataBind();
        //        }
        //        else
        //        {
        //            lblsms_contact_bill.Visible = true;
        //        }

        //        payment type
        //        var objmpt = (from mtp in dbc.mst_payment_types where mtp.mpt_id == objcomp.mpt_id && mtp.mpt_status == 'A' select mtp).FirstOrDefault();
        //        if (objmpt != null)
        //        {
        //            lblpaytype.Text = objmpt.mpt_tname + "/" + objmpt.mpt_ename;
        //        }

        //        billing medthod
        //        var objmbm = (from mbm in dbc.mst_billing_methods where mbm.mbm_id == objcomp.mbm_id && mbm.mbm_status == 'A' select mbm).FirstOrDefault();
        //        if (objmpt != null)
        //        {
        //            lblbill_medthod.Text = objmbm.mbm_tname + "/" + objmbm.mbm_ename;
        //        }

        //        type
        //        var objmstid = (from tid in dbc.mst_types where tid.mst_id == objcomp.mst_id select new { typename = tid.mst_tname + "/" + tid.mst_ename }).FirstOrDefault();
        //        if (objmstid != null)
        //        {
        //            lbltype.Text = objmstid.typename.ToString();
        //        }
                        
                   
        //        package and option
        //        var objpackage = (from package in dbc.trn_package_details
        //                          where package.tcd_id == objcomp.tcd_id
        //                          select new
        //                          {
        //                              package_no = 0,
        //                              package_order = package.tpd_order_desc,
        //                              package_price = package.tpd_price,
        //                              package_payment = (from mpt in dbc.mst_payment_types
        //                                                 where mpt.mpt_id == package.mpt_id
        //                                                 select mpt).FirstOrDefault(),
        //                              package_credit = package.tpd_limit_credit,
        //                              package_s_date = package.tpd_date_from,
        //                              package_e_date = package.tpd_date_to
        //                          }).FirstOrDefault();
        //        if (objpackage != null)
        //        {
        //            gvpackage_option.DataSource = objpackage;
        //            gvpackage_option.DataBind();
        //        }
        //        else
        //        {
        //            lblsms_package_option.Visible = true;
        //        }

        //        check up rate
        //        var objmpr = (from mpr in dbc.mst_payment_rates where mpr.mpr_status == 'A' && mpr.mpr_id == objcomp.mpr_id select mpr).FirstOrDefault();
        //        if (objmpr != null)
        //        {
        //            lblchkup_rate.Text = objmpr.mpr_tname + "/" + objmpr.mpr_ename;
        //        }

        //        payment main
        //        var objmpm = (from mpm in dbc.mst_payment_mains where mpm.mpm_status == 'A' && mpm.mpm_id == objcomp.mpm_id select mpm).FirstOrDefault();
        //        if (objmpm != null)
        //        {
        //            lblmain_program.Text = objmpm.mpm_tname + "/" + objmpm.mpm_ename;
        //        }

        //        pay is qt
        //        var objqt = (from qt in dbc.mst_payment_quatations where qt.mpq_status == 'A' && qt.mpq_id == objcomp.mpq_id select qt).FirstOrDefault();
        //        if (objqt != null)
        //        {
        //            lbloption_in_qt.Text = objqt.mpq_tname + "/" + objqt.mpq_ename;
        //        }

        //        //pay is not qt
        //        var objnqt = (from nqt in dbc.mst_payment_nquatations where nqt.mpn_status == 'A' && nqt.mpn_id == objcomp.mpn_id select nqt).FirstOrDefault();
        //        if (objnqt != null)
        //        {
        //            lbloption_in_nqt.Text = objnqt.mpn_tname + "/" + objnqt.mpn_ename;
        //        }


        //        recieve medicine
        //        var objrmed = (from mrm in dbc.mst_receive_medicines where mrm.mrm_id == objcomp.mrm_id && mrm.mrm_status == 'A' select mrm).FirstOrDefault();
        //        if (objrmed != null)
        //        {
        //            lblrecieve_med.Text = objrmed.mrm_tname + "/" + objrmed.mrm_ename;
        //        }

        //        condition service employee or executive
        //        var objmcs = (from mst_mcs in dbc.mst_condition_services
        //                      join trn_mcs in dbc.trn_condition_services on mst_mcs.mcs_id equals trn_mcs.mcs_id
        //                      where mst_mcs.mcs_status == 'A' && (trn_mcs.tcd_id == objcomp.tcd_id) && (trn_mcs.tcs_type == "EM" || trn_mcs.tcs_type == "EX")
        //                      select mst_mcs).ToList();
        //        if (objmcs.Count > 0)
        //        {
        //            foreach (var data in objmcs)
        //            {
        //                lblcondition.Text += data.mcs_tname + "/" + data.mcs_ename + ",";
        //            }
        //        }

        //        attach file
        //        var objattach = (from file in dbc.trn_attach_files where file.taf_user_type == 'H' && file.tcd_id == objcomp.tcd_id select file).ToList();
        //        if (objattach.Count > 0)
        //        {
        //            gvattach_file.DataSource = objattach;
        //        }
        //        else
        //        {
        //            lblsms_attach_file.Visible = true;
        //        }

        //        lblremark.Text = objcomp.tcd_remark;

        //        lbllastupdate.Text = String.Format("{0:dd/MM/yyyy}", objcomp.tcd_update_date);
        //        lbllastupdateby.Text = objcomp.mul_user_login;
        //    }
        //}

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    DateTime d = new DateTime();
        //    d = Convert.ToDateTime(txtstart.Text);
        //    LoadCompany();
        //    //LoadData(txtAutoComplete.Text);
        //    //var objcomp = (from comp in dbc.trn_company_details where comp.tcd_tname.Contains(txtAutoComplete.Text) || comp.tcd_ename.Contains(txtAutoComplete.Text) select new { id = comp.tcd_id, name = comp.tcd_tname + "/" + comp.tcd_ename }).ToList();
        //}

        //protected void gvsearch_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("onMouseOver", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#00EEEE';this.style.cursor='pointer'");
        //        e.Row.Attributes.Add("onMouseOut", "this.style.backgroundColor=currentcolor");
        //        //e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('btnSearch','Select${0}')", e.Row.RowIndex));

        //    }
        //}
    }
}