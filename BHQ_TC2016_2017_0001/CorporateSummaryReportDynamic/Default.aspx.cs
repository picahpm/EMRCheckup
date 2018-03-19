using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConfigEMRCheckUp;
using System.Data;
using EMR_Library;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using DBCheckup;
using DBToDoList;
using System.Reflection;
using System.IO;

namespace CorporateSummaryReportDynamic
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                startdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                enddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ComboBoxBinding();
            }
        }

        protected void ComboBoxBinding()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                List<mst_corp_report> mstRpt = cdc.mst_corp_reports
                                               .Where(x => x.mcr_active == true && x.mcr_type == 'D').ToList();

                list = mstRpt.OrderBy(x => x.mcr_seq)
                                        .Select(x => new SelectListItem
                                        {
                                            Selected = false,
                                            Value = x.mcr_id.ToString(),
                                            Text = x.mcr_report_name
                                        }).ToList();

                DataTable dt = new DataTable();
                dt = ToDataTable(list);
                dt.Columns.Add("No", typeof(int));
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    i = i + 1;
                    dr["No"] = i;
                }
                ddlSelectReport.DataSource = dt;

                ddlSelectReport.DataBind();

                if (ddlSelectReport.DataMember.Count() > 0)
                    ddlSelectReport.SelectedIndex = 0;
            }
        }

        protected void GenerateDynamicGridView()
        {
            gvDynamicReport.DataSource = null;
            gvDynamicReport.Columns.Clear();

            string CompanyName = VNull.IsNull(txtCompanyName.Text) ? null : txtCompanyName.Text;
            DateTime startDate = VNull.IsNull(startdate.Text) ? VNull.NullDateTime : Convert.ToDateTime(startdate.Text + " 00:00:00");
            DateTime endDate = VNull.IsNull(enddate.Text) ? VNull.NullDateTime : Convert.ToDateTime(enddate.Text + " 00:00:00");
            int reportId = Convert.ToInt32(ddlSelectReport.SelectedValue.ToString());
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigEMRCheckUp.ConfigCls.PathwayConnString))
            {
                con.Open();
                string storeName = "dbo.CorpRpt_DynamicLabGroup";

                if (reportId == 4)
                    storeName = "CorpRpt_DynamicLabGroup_Urine_Stool";

                SqlCommand cmd = new SqlCommand(storeName, con);
                cmd.Parameters.AddWithValue("@companyname", CompanyName);
                cmd.Parameters.AddWithValue("@startdate", startDate);
                cmd.Parameters.AddWithValue("@enddate", endDate);
                cmd.Parameters.AddWithValue("@labgroup", reportId);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                con.Close();
                con.Dispose();
            }

            foreach (DataColumn dc in dt.Columns)
            {
                BoundField bField = new BoundField();
                bField.HeaderText = dc.ColumnName;
                bField.DataField = dc.ColumnName;
                if (dc.ColumnName.Contains("RBC Morphology"))
                {
                    bField.HeaderStyle.Width = Unit.Pixel(220);
                    bField.ItemStyle.Width = Unit.Pixel(220);
                }
                gvDynamicReport.Columns.Add(bField);
            }

            gvDynamicReport.DataSource = dt;
            gvDynamicReport.DataBind();
        }

        private string strWithEnter(string input)
        {
            string res = Regex.Replace(input, @"\r\n?|\n", Environment.NewLine);
            return res;
        }

        protected void btnShowPatients_Click(object sender, EventArgs e)
        {
            gvDynamicReport.Visible = false;
            gvDynamicReport.DataSource = null;
            GeneratePatientsGridView();
            btnExcel.Visible = false;
            btnPdf.Visible = false;
            gvShowPatients.Visible = true;
        }

        protected void GeneratePatientsGridView()
        {
            gvShowPatients.DataSource = null;
            string total = "0";
            string patients = "0";
            string companyName = VNull.IsNull(txtCompanyName.Text) ? "" : txtCompanyName.Text;
            DateTime startDate = VNull.IsNull(startdate.Text) ? VNull.NullDateTime : Convert.ToDateTime(startdate.Text + " 00:00:00");
            DateTime endDate = VNull.IsNull(enddate.Text) ? VNull.NullDateTime : Convert.ToDateTime(enddate.Text + " 00:00:00");

            DataTable dt = new DataTable();
            List<patient> pt = null;
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                total = cdc.vw_patient_corporates
                                  .Where(x => (x.companyname == null ? "" : x.companyname) == companyName &&
                                               x.arrived_date.Value.Date >= startDate &&
                                               x.arrived_date.Value.Date <= endDate)
                                  .Count().ToString();

                using (InhToDoListDataContext itc = new InhToDoListDataContext())
                {
                    string sub_com = "";
                    if (!string.IsNullOrEmpty(companyName))
                    {
                        var doc_no = (from itd in itc.index_trn_company_details
                                      join tcd in itc.trn_company_details
                                      on itd.tcd_id equals tcd.tcd_id
                                      where tcd.tcd_tname == companyName
                                      select itd.tcd_document_no).FirstOrDefault();

                        sub_com = (from tcd in itc.trn_company_details
                                   join itd in itc.index_trn_company_details
                                   on tcd.tcd_id equals itd.tcd_id
                                   where tcd.tcd_document_no == doc_no
                                   select tcd.tcd_legal).FirstOrDefault();

                        txtSubCompanyName.Text = sub_com;

                        if (!string.IsNullOrEmpty(doc_no))
                        {
                            pt = cdc.trn_patient_book_covers
                                                  .Where(x => x.tcd_document_no == doc_no && x.trn_patient_regi.tpr_arrive_date.Value.Date >= startDate.Date &&
                                                     x.trn_patient_regi.tpr_arrive_date.Value.Date <= endDate.Date)
                                                  .Select(x => new patient
                                                  {
                                                      HN = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                      ID = x.trn_patient_regi.trn_patient_book_cover.tpbc_emp_id,
                                                      Name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                      Arrived = x.trn_patient_regi.trn_patient_regis_detail.tpr_real_arrived_date.Value
                                                  }).OrderBy(x => x.Arrived).ToList();

                            patients = pt.Count().ToString();

                        }
                    }
                }
            }

            dt = ToDataTable(pt);
            dt.Columns.Add("No", typeof(int));
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                i = i + 1;
                dr["No"] = i;
            }

            txtTotalPatients.Text = total;
            txtArrivedPatients.Text = patients;

            gvShowPatients.DataSource = dt;
            gvShowPatients.DataBind();

        }

        protected void btnConfirmCriterias_Click(object sender, EventArgs e)
        {
            gvShowPatients.Visible = false;
            gvShowPatients.DataSource = null;

            btnExcel.Visible = true;
            btnPdf.Visible = true;
            GenerateDynamicGridView();
            gvDynamicReport.Visible = true;
        }

        protected void gvDynamicReport_OnRowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label txtCountry = new Label();
                txtCountry.ID = "Name";
                txtCountry.Text = (e.Row.DataItem as DataRowView).Row["ชื่อ"].ToString().Replace("\r\n", System.Environment.NewLine);
                e.Row.Cells[2].Controls.Add(txtCountry);
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ExportGridToExcel();
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "CorporateSummaryReportDynamic" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvDynamicReport.GridLines = GridLines.Both;
            gvDynamicReport.HeaderStyle.Font.Bold = true;
            gvDynamicReport.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
    }

    public class patient
    {
        public string HN { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime Arrived { get; set; }
    }

    // Summary:
    //     Represents the selected item in an instance of the System.Web.Mvc.SelectList
    //     class.
    public class SelectListItem
    {
        // Summary:
        //     Gets or sets a value that indicates whether this System.Web.Mvc.SelectListItem
        //     is selected.
        //
        // Returns:
        //     true if the item is selected; otherwise, false.
        public bool Selected { get; set; }
        //
        // Summary:
        //     Gets or sets the text of the selected item.
        //
        // Returns:
        //     The text.
        public string Text { get; set; }
        //
        // Summary:
        //     Gets or sets the value of the selected item.
        //
        // Returns:
        //     The value.
        public string Value { get; set; }
    }
}
