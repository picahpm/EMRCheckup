using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DBToDoList;

namespace CheckUpToDoList
{
    public partial class frm_contact_center_search : System.Web.UI.Page
    {
        InhToDoListDataContext dbc = new InhToDoListDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            if (!Page.IsPostBack)
            {
                this.LoadType();
                this.LoadLacation();
                this.LoadCompany();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCompany();
        }
        private List<int> LoadLocationId(int locid)
        {
            List<int> tcdid = (from loc in dbc.trn_checkup_locations where loc.mcl_id == locid select loc.tcd_id).ToList();
            return tcdid;
        }

        private void LoadCompany()
        {
            //var objcomp = (from comp in dbc.sp_search_company()
            //               where ChSearch.Checked == true ? (funcCls.GetServerDateTime().Date >= comp.tcd_date_from.Value.Date && funcCls.GetServerDateTime().Date <= comp.tcd_date_to.Value.Date) : (comp.tcd_date_from.Value.Date > funcCls.GetServerDateTime().Date && comp.tcd_date_to.Value.Date > funcCls.GetServerDateTime().Date) || (comp.tcd_date_to.Value.Date < funcCls.GetServerDateTime().Date)
            //               select comp).ToList();

            //if (Convert.ToInt16(DDLocation.SelectedIndex) != 0)
            //{
            //    List<int> tcdid = LoadLocationId(Convert.ToInt16(DDLocation.SelectedValue));
            //    objcomp = (from data in objcomp where tcdid.Contains(data.tcd_id) select data).ToList();
            //}

            //if (txtsearch.Text != String.Empty)
            //{
            //    objcomp = objcomp.Where(c => c.tcd_tname.Contains(txtsearch.Text)).ToList();
            //}

            //if (Convert.ToInt16(ddltypeofcomp.SelectedIndex) != 0)
            //{
            //    objcomp = objcomp.Where(c => c.tcd_type == ddltypeofcomp.Text).ToList();
            //}

            //if (txtlegal.Text != String.Empty)
            //{
            //    objcomp = objcomp.Where(c => c.tcd_legal == txtlegal.Text).ToList();
            //}

            //if (Convert.ToInt16(DDType.SelectedIndex) != 0)
            //{
            //    using (InhToDoListDataContext tdc = new InhToDoListDataContext())
            //    {
            //        List<int?> new_tcd_id = (from tpm in tdc.trn_payments
            //                                 where tpm.mst_id == Convert.ToInt32(DDType.SelectedValue)
            //                                 select tpm.tcd_id).ToList();
            //        objcomp = objcomp.Where(x => new_tcd_id.Contains(x.tcd_id)).ToList();
            //    }
            //    //objcomp = objcomp.Where(c => c.mst_id == Convert.ToInt16(DDType.SelectedValue)).ToList();
            //}

            //if (txtstart.Text != String.Empty && txtEndDate.Text != String.Empty)
            //{
            //    objcomp = objcomp.Where(c => (c.tcd_date_from >= Constant.ConvertStringToDate(txtstart.Text)) && c.tcd_date_to <= Constant.ConvertStringToDate(txtEndDate.Text)).ToList();
            //}


            //var result = (from comp in objcomp
            //              select new
            //              {
            //                  tcd_id = comp.tcd_id,
            //                  tcd_tname = comp.tcd_tname,
            //                  tcd_document_no = comp.tcd_document_no,
            //                  status = LoadCheckCompanyMaster(comp.tcd_code)
            //              });

            //RepeaterCompany.DataSource = result;
            //RepeaterCompany.DataBind();

            try
            {
                string strSearch = (string.IsNullOrEmpty(txtsearch.Text) ? "" : txtsearch.Text).Trim().ToLower();
                bool? active = ChSearch.Checked == true ? true : false;
                int mcl_id = Convert.ToInt32(DDLocation.SelectedValue);
                int mst_id = Convert.ToInt32(DDType.SelectedValue);
                DateTime? fromDate = string.IsNullOrEmpty(txtstart.Text) ? (DateTime?)null : Constant.ConvertStringToDate(txtstart.Text);
                DateTime? endDate = string.IsNullOrEmpty(txtEndDate.Text) ? (DateTime?)null : Constant.ConvertStringToDate(txtEndDate.Text);
                string strTypeComp = ddltypeofcomp.Text;
                string strLegal = txtlegal.Text;

                using (InhToDoListDataContext context = new InhToDoListDataContext())
                {
                    var result = context.sp_search_company2(strSearch, active, mcl_id, mst_id, fromDate, endDate, strTypeComp, strLegal)
                                        .Select(x => new
                                        {
                                            tcd_id = x.tcd_id,
                                            tcd_tname = x.tcd_tname,
                                            tcd_document_no = x.tcd_document_no,
                                            status = LoadCheckCompanyMaster(x.tcd_code)
                                        }).ToList();

                    RepeaterCompany.DataSource = result;
                    RepeaterCompany.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private string LoadCheckCompanyMaster(string code)
        {
            string status = String.Empty;
            status = (from data in dbc.mst_companies where data.mco_code == code select Convert.ToString(data.mco_status)).FirstOrDefault();
            if (status == null) { status = "A"; }
            return status;
        }

        private class Location
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        private void LoadLacation()
        {
            try
            {
                List<Location> result = new List<Location> { new Location { id = 0, name = "" } };
                DDLocation.DataTextField = "name";
                DDLocation.DataValueField = "id";
                DDLocation.DataSource = result;
                using (InhToDoListDataContext context = new InhToDoListDataContext())
                {
                    result.AddRange(context.mst_checkup_locations
                                           .Where(x => x.mcl_status == 'A')
                                           .Select(x => new Location
                                           {
                                               id = x.mcl_id,
                                               name = x.mcl_ename
                                           }).ToList());
                }
                DDLocation.DataBind();
            }
            catch (Exception ex)
            {

            }
            //var objloc = (from loc in dbc.mst_checkup_locations select new { id = loc.mcl_id, name = loc.mcl_tname + "/" + loc.mcl_ename }).ToList();
            //DDLocation.DataTextField = "name";
            //DDLocation.DataValueField = "id";
            //DDLocation.DataSource = objloc;
            //DDLocation.DataBind();
            //DDLocation.Items.Insert(0, String.Empty);
            //DDLocation.SelectedIndex = 0;
        }
        private class CompType
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        private void LoadType()
        {
            try
            {
                List<CompType> result = new List<CompType> { new CompType { id = 0, name = "" } };
                DDType.DataTextField = "name";
                DDType.DataValueField = "id";
                DDType.DataSource = result;
                using (InhToDoListDataContext context = new InhToDoListDataContext())
                {
                    result.AddRange(context.mst_types
                                           .Where(x => x.mst_status == 'A')
                                           .Select(x => new CompType
                                           {
                                               id = x.mst_id,
                                               name = x.mst_tname
                                           }).ToList());
                }
                DDType.DataBind();
            }
            catch (Exception ex)
            {

            }
            //var objmstid = (from tid in dbc.mst_types select new { id = tid.mst_id, name = tid.mst_tname }).ToList();
            //DDType.DataTextField = "name";
            //DDType.DataValueField = "id";
            //DDType.DataSource = objmstid;
            //DDType.DataBind();
            //DDType.Items.Insert(0, String.Empty);
            //DDType.SelectedIndex = 0;
        }
    }
}