using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DBCheckup;
using System.Globalization;
using System.Threading.Tasks;

namespace BKvs2010
{
    public partial class frmBookResultSettingByHN : Form
    {
        public int ptpr_id { get; set; }
        public string pHN_no { get; set; }
        public string pEN_no { get; set; }
        public string pFullName { get; set; }
        public string pDOB { get; set; }
        public string pPaAge { get; set; }
        public string pGender { get; set; }
        public string pPackage { get; set; }
        public string prtL { get; set; } // languaue parameter

        #region classObj
        private class ComulativeSource
        {
            public bool Equals(ComulativeSource obj)
            {
                //if (this.index != obj.index) return false;
                if (this.enNo != obj.enNo) return false;
                if (this.labMhsID != obj.labMhsID) return false;
                if (this.labMhsName != obj.labMhsName) return false;
                if (this.labCode != obj.labCode) return false;
                if (this.labDate != obj.labDate) return false;
                if (this.labName != obj.labName) return false;
                if (this.labRange != obj.labRange) return false;
                if (this.labRemark != obj.labRemark) return false;
                if (this.labResult != obj.labResult) return false;
                if (this.labSummary != obj.labSummary) return false;
                if (this.labSuppress != obj.labSuppress) return false;
                if (this.labUnit != obj.labUnit) return false;
                if (this.labValue != obj.labValue) return false;
                return true;
            }
            public int index { get; set; }
            public string enNo { get; set; }
            public string labCode { get; set; }
            public string labName { get; set; }
            public string labValue { get; set; }
            public string labRange { get; set; }
            public string labUnit { get; set; }
            public string labResult { get; set; }
            public char? labSummary { get; set; }
            public DateTime? labDate { get; set; }
            public string labRemark { get; set; }
            public char? labSuppress { get; set; }
            public int? labMhsID { get; set; }
            public string labMhsName { get; set; }
        }
        private Boolean compareListComulative(List<ComulativeSource> a, List<ComulativeSource> b)
        {
            if ((a == null && b == null) || (a.Count == 0 && b.Count == 0)) return true;
            if (a.Count != b.Count) return false;
            List<ComulativeSource> tempA = a.OrderBy(x => x.labCode).ToList();
            List<ComulativeSource> tempB = b.OrderBy(x => x.labCode).ToList();
            for (int i = 0; i < tempA.Count; i++)
            {
                if (!tempA[i].Equals(tempB[i])) return false;
            }
            return true;
        }
        private class memberGvSelectEN
        {
            public string cEN { get; set; }
            public DateTime? cENdate { get; set; }
            public string cLocation { get; set; }
            public Boolean cSelectx { get; set; }
        }
        private class gvSuppressLabSource
        {
            public int index { get; set; }
            public string labCode { get; set; }
            public string testItem { get; set; }
        }
        #endregion

        #region properties
        private List<pw_Get_PatientBook_LabCumulativeResult> tempResult;
        private List<pw_Get_PatientBook_LabCumulativeResult> getLabResult_ByHN
        {
            get
            {
                try
                {
                    string HN_no = Program.CurrentRegis.trn_patient.tpt_hn_no;
                    return new InhCheckupDataContext().pw_Get_PatientBook_LabCumulative(HN_no, null, null, null).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private ComulativeSource _currentComulative = new ComulativeSource();
        private ComulativeSource currentComulative
        {
            get { return _currentComulative; }
            set
            {
                _currentComulative = value;
                if (value == null)
                {
                    ckSaveSuppressLab.Checked = false;
                    ckSaveSuppressLab.Enabled = false;
                    rdNormal.Checked = false;
                    rdNormal.Enabled = false;
                    rdAbNormal.Checked = false;
                    rdAbNormal.Enabled = false;
                    txtLabRemark.Text = "";
                    txtLabRemark.Enabled = false;
                }
                else
                {
                    ckSaveSuppressLab.Checked = value.labSuppress == 'Y' ? true : false;
                    ckSaveSuppressLab.Enabled = true;
                    rdNormal.Checked = value.labSummary == 'N';
                    rdNormal.Enabled = true;
                    rdAbNormal.Checked = value.labSummary == 'A';
                    rdAbNormal.Enabled = true;
                    txtLabRemark.Text = value.labRemark;
                    txtLabRemark.Enabled = true;
                }
            }
        }
        private List<ComulativeSource> checkPointData = new List<ComulativeSource>();
        private List<ComulativeSource> _currentData = new List<ComulativeSource>();
        private List<ComulativeSource> currentData
        {
            get { return _currentData; }
            set
            {
                _currentData = value;
            }
        }
        private List<ComulativeSource> LoadCheckPointData
        {
            get
            {
                try
                {
                    return Program.CurrentRegis.trn_book_hdrs.FirstOrDefault().trn_book_labs.Select((x, _index) => new ComulativeSource
                    {
                        index = _index,
                        enNo = x.tklb_en_no,
                        labCode = x.tklb_lab_no,
                        labName = x.tklb_lab_name,
                        labRange = x.tklb_lab_range,
                        labResult = x.tklb_lab_result_thai,
                        labSummary = x.tklb_summary,
                        labUnit = x.tklb_lab_unit,
                        labValue = x.tklb_lab_value,
                        labDate = x.tklb_lab_date,
                        labRemark = x.tklb_remark,
                        labSuppress = x.tklb_suppress,
                        labMhsID = x.mhs_id,
                        labMhsName = x.mst_hpc_site.mhs_tname
                    }).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private List<int?> getHpcSite
        {
            get
            {
                try
                {
                    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    {
                        List<int?> mhs_id = null;
                        if ((int?)cbLocation.SelectedValue == -1)
                        {
                            mhs_id = dbc.mst_hpc_sites.Where(x => x.mhs_status == 'A').Select(x => (int?)x.mhs_id).ToList();
                        }
                        else if ((int?)cbLocation.SelectedValue == -2)
                        {
                            mhs_id = dbc.mst_hpc_sites.Where(x => x.mhs_status == 'A' && x.mhs_type == 'P').Select(x => (int?)x.mhs_id).ToList();
                        }
                        else
                        {
                            mhs_id = new List<int?> { (int?)cbLocation.SelectedValue };
                        }
                        return mhs_id;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private BindingList<memberGvSelectEN> GvSelectENSource = new BindingList<memberGvSelectEN>();
        private BindingList<memberGvSelectEN> getGvSelectENSource(DateTime? startDate, DateTime? endDate, List<int?> mhs_id)
        {
            try
            {
                return new BindingList<memberGvSelectEN>(tempResult.GroupBy(x => x.tpl_en_no)
                        .Select(x => x.OrderByDescending(y => y.tpl_lab_date).First())
                        .Where(x => x.tpl_lab_date.Value.Date >= startDate.Value.Date && x.tpl_lab_date.Value.Date <= endDate.Value.Date
                            && mhs_id.Contains(x.mhs_id))
                        .Select(x => new memberGvSelectEN
                        {
                            cEN = x.tpl_en_no,
                            cENdate = x.tpl_lab_date,
                            cLocation = x.mhs_ename,
                            cSelectx = true
                        }).ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private char? convToCharNull(object obj)
        {
            try
            {
                if (obj != null)
                {
                    return Convert.ToChar(obj);
                }
            }
            catch
            {

            }
            return null;
        }
        private List<ComulativeSource> getRetriveToRealData
        {
            get
            {
                try
                {
                    List<string> listEN = GvSelectENSource.Where(y => y.cSelectx == true).Select(y => y.cEN).ToList();
                    return tempResult.Where(x => listEN.Contains(x.tpl_en_no))
                                     .GroupBy(x => x.mlb_id)
                                     .Select(x => x.OrderByDescending(y => y.tpl_lab_date).First()).ToList()
                                     .Select((x, _index) => new ComulativeSource
                                     {
                                         index = _index,
                                         enNo = x.tpl_en_no,
                                         labMhsID = x.mhs_id,
                                         labMhsName = x.mhs_tname,
                                         labCode = x.tpl_lab_no,
                                         labName = x.tpl_lab_name,
                                         labValue = x.tpl_lab_value,
                                         labRange = x.tpl_lab_range,
                                         labUnit = x.tpl_lab_unit,
                                         labResult = x.mlp_tname,
                                         labSummary = convToCharNull(x.mlp_summary),
                                         labDate = x.tpl_lab_date,
                                         labRemark = null,
                                         labSuppress = 'N'
                                     }).ToList(); 
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        private List<string> getListMlbCode(string labGroupCode)
        {
            try
            {
                return new InhCheckupDataContext().mst_labs.Where(x => x.mst_lab_group.mlg_code == labGroupCode).Select(x => x.mlb_code).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<ComulativeSource> getComulativeByGroup(string labGroupCode)
        {
            try
            {
                List<string> ListMlbCode = getListMlbCode(labGroupCode);
                return currentData.Where(x => ListMlbCode.Contains(x.labCode)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private ComulativeSource getCurrentComulative(DataGridView dgv)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                if (dgv == gvLabCumulative1)
                {
                    gvLabCumulative2.ClearSelection();
                    gvLabCumulative3.ClearSelection();
                    gvLabCumulative4.ClearSelection();
                    gvLabCumulative5.ClearSelection();
                    gvLabCumulative6.ClearSelection();
                }
                else if (dgv == gvLabCumulative2)
                {
                    gvLabCumulative1.ClearSelection();
                    gvLabCumulative3.ClearSelection();
                    gvLabCumulative4.ClearSelection();
                    gvLabCumulative5.ClearSelection();
                    gvLabCumulative6.ClearSelection();
                }
                else if (dgv == gvLabCumulative3)
                {
                    gvLabCumulative1.ClearSelection();
                    gvLabCumulative2.ClearSelection();
                    gvLabCumulative4.ClearSelection();
                    gvLabCumulative5.ClearSelection();
                    gvLabCumulative6.ClearSelection();
                }
                else if (dgv == gvLabCumulative4)
                {
                    gvLabCumulative1.ClearSelection();
                    gvLabCumulative2.ClearSelection();
                    gvLabCumulative3.ClearSelection();
                    gvLabCumulative5.ClearSelection();
                    gvLabCumulative6.ClearSelection();
                }
                else if (dgv == gvLabCumulative5)
                {
                    gvLabCumulative1.ClearSelection();
                    gvLabCumulative2.ClearSelection();
                    gvLabCumulative3.ClearSelection();
                    gvLabCumulative4.ClearSelection();
                    gvLabCumulative6.ClearSelection();
                }
                else if (dgv == gvLabCumulative6)
                {
                    gvLabCumulative1.ClearSelection();
                    gvLabCumulative2.ClearSelection();
                    gvLabCumulative3.ClearSelection();
                    gvLabCumulative4.ClearSelection();
                    gvLabCumulative5.ClearSelection();
                }
                string labCode = dgv.SelectedRows[0].Cells[1].Value.ToString();
                return currentData.Where(x => x.labCode == labCode).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
        private BindingList<gvSuppressLabSource> suppressSource = new BindingList<gvSuppressLabSource>();
        private char? ckPointHIV;
        private char? ckPointChart;
        private char? ckPointSuppress;
        private Boolean chk_trn_book_hdr
        {
            get
            {
                try
                {
                    trn_book_hdr tkh = Program.CurrentRegis.trn_book_hdrs.FirstOrDefault();
                    if (tkh.tkh_report_HIV != (ckReportHIV.Checked ? 'Y' : 'N')) return false;
                    if (tkh.tkh_show_chart != (ckShowDefaultChart.Checked ? 'Y' : 'N')) return false;
                    if (tkh.tkh_suppress_lab != (ckSuppressLab.Checked ? 'Y' : 'N')) return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return true;
            }
        }
        private Boolean chkCheckPointHdr
        {
            get
            {
                try
                {
                    if (ckPointHIV != (ckReportHIV.Checked ? 'Y' : 'N')) return false;
                    if (ckPointChart != (ckShowDefaultChart.Checked ? 'Y' : 'N')) return false;
                    if (ckPointSuppress != (ckSuppressLab.Checked ? 'Y' : 'N')) return false;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return true;
            }
        }

        #endregion

        private void Load_PatientProfile()
        {
            try
            {
                trn_patient_regi tpr = Program.CurrentRegis;
                trn_patient tpt = tpr.trn_patient;
                trn_book_hdr tkh = tpr.trn_book_hdrs.FirstOrDefault();
                lblHN.Text = tpt.tpt_hn_no;
                lblEN.Text = tpr.tpr_en_no;
                lblFullName.Text = tpt.tpt_othername;
                lblDOB.Text = tpt.tpt_dob_text; // Program.GetFormattedString(tpt.tpt_dob.Value.Date);
                lblAge.Text = Program.CalculateAge(tpt.tpt_dob.Value, Program.GetServerDateTime()).ToString();
                lblGender.Text = returnGender(tpt.tpt_gender);
                lblPackage.Text = tpr.mhc_id == null ? "no package" : tpr.mst_health_checkup.mhc_tname;
                pictureBox1.Image = Program.byteArrayToImage(tpt.tpt_image.ToArray());
                ckReportHIV.Checked = tkh.tkh_report_HIV == 'Y';
                ckShowDefaultChart.Checked = tkh.tkh_show_chart == 'Y';
                ckSuppressLab.Checked = tkh.tkh_suppress_lab == 'Y';
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private string returnGender(char? gender)
        {
            if (gender == 'M')
            {
                return "Male";
            }
            else if (gender == 'F')
            {
                return "Female";
            }
            return null;
        }
        private void BindData_CbLocation()
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    if (rdAllLocation.Checked == true)
                    {
                        var source = dbc.mst_hpc_sites.Where(x => x.mhs_status == 'A').Select(x => new
                        {
                            code = x.mhs_id,
                            name = x.mhs_tname
                        }).ToList();
                        source.Insert(0, new { code = -1, name = "All Location" });
                        cbLocation.DataSource = source;
                        cbLocation.ValueMember = "code";
                        cbLocation.DisplayMember = "name";
                    }
                    else if (rdHpcOnly.Checked == true)
                    {
                        var source = dbc.mst_hpc_sites.Where(x => x.mhs_status == 'A' && x.mhs_type == 'P').Select(x => new
                        {
                            code = x.mhs_id,
                            name = x.mhs_ename
                        }).ToList();
                        source.Insert(0, new { code = -2, name = "All HPC Site" });
                        cbLocation.DataSource = source;
                        cbLocation.ValueMember = "code";
                        cbLocation.DisplayMember = "name";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void selectPanel(Panel panel)
        {
            gvLabCumulative1.ClearSelection();
            gvLabCumulative2.ClearSelection();
            gvLabCumulative3.ClearSelection();
            gvLabCumulative4.ClearSelection();
            gvLabCumulative5.ClearSelection();
            gvLabCumulative6.ClearSelection();

            Panel1.Height = 25;
            Panel2.Height = 25;
            Panel3.Height = 25;
            Panel4.Height = 25;
            Panel5.Height = 25;
            Panel6.Height = 25;
            panel.Height = 490;
        }

        public frmBookResultSettingByHN()
        {
            InitializeComponent();
        }
        private void frmBookResultSettingByHN_Load(object sender, EventArgs e)
        {
            Program.CurrentRegis = new InhCheckupDataContext().trn_patient_regis.Where(x => x.tpr_id == ptpr_id).FirstOrDefault();
            txtVDateFrom.MinDate = DateTime.Today.AddYears(-5);
            System.Threading.Thread.CurrentThread.CurrentCulture
                = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            gvLabCumulative1.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;
            gvLabCumulative2.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;
            gvLabCumulative3.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;
            gvLabCumulative4.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;
            gvLabCumulative5.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;
            gvLabCumulative6.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;
            gvSelectEN.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;

            gvLabCumulative1.AutoGenerateColumns = false;
            gvLabCumulative2.AutoGenerateColumns = false;
            gvLabCumulative3.AutoGenerateColumns = false;
            gvLabCumulative4.AutoGenerateColumns = false;
            gvLabCumulative5.AutoGenerateColumns = false;
            gvLabCumulative6.AutoGenerateColumns = false;

            tempResult = getLabResult_ByHN;
            gvSelectEN.AutoGenerateColumns = false;
            selectPanel(Panel1);
            Load_PatientProfile();
            BindData_CbLocation();
            checkPointData = LoadCheckPointData;
            currentData = LoadCheckPointData;
            insertCommulativeGrid();
            GvSelectENSource = new BindingList<memberGvSelectEN>(
                currentData.GroupBy(x => x.enNo).Select(x => x.OrderByDescending(y => y.labDate).First()).Select(x => new memberGvSelectEN
                {
                    cEN = x.enNo,
                    cENdate = x.labDate,
                    cLocation = x.labMhsName,
                    cSelectx = true
                }).ToList());
            gvSelectEN.DataSource = GvSelectENSource;
            if (gvSelectEN.Rows.Count > 0) gvSelectEN.Rows[0].ReadOnly = true;
            suppressSource = new BindingList<gvSuppressLabSource>(
                currentData.Where(x => x.labSuppress == 'Y').Select((x, _index) => new gvSuppressLabSource
                {
                    index = _index + 1,
                    labCode = x.labCode,
                    testItem = x.labName
                }).ToList());
            gvSuppressLab.DataSource = suppressSource;
            ckSaveSuppressLab.Checked = false;
            ckSaveSuppressLab.Enabled = false;
            rdNormal.Checked = false;
            rdNormal.Enabled = false;
            rdAbNormal.Checked = false;
            rdAbNormal.Enabled = false;
            txtLabRemark.Text = "";
            txtLabRemark.Enabled = false;
        }
        private void rdAllLocation_CheckedChanged(object sender, EventArgs e)
        {
            BindData_CbLocation();
        }
        private void lblView1_Click(object sender, EventArgs e)
        {
            selectPanel(Panel1);
            gvLabCumulative1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void lblView2_Click(object sender, EventArgs e)
        {
            selectPanel(Panel2);
            gvLabCumulative2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void lblView3_Click(object sender, EventArgs e)
        {
            selectPanel(Panel3);
            gvLabCumulative3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void lblView4_Click(object sender, EventArgs e)
        {
            selectPanel(Panel4);
            gvLabCumulative4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void lblView5_Click(object sender, EventArgs e)
        {
            selectPanel(Panel5);
            gvLabCumulative5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void lblView6_Click(object sender, EventArgs e)
        {
            selectPanel(Panel6);
            gvLabCumulative6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private void gvSuppressLab_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if ((gvSuppressLab.RowCount > 0) && (gvSuppressLab != null))
                {
                    gvSuppressLab.Rows[0].Cells["cLabCode"].Selected = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GvSelectENSource = getGvSelectENSource(txtVDateFrom.Value, txtVDateTo.Value, getHpcSite);
            gvSelectEN.DataSource = GvSelectENSource;
            gvSelectEN.Refresh();
            if (gvSelectEN.Rows.Count > 0) gvSelectEN.Rows[0].ReadOnly = true;
            currentData = new List<ComulativeSource>();
            gvLabCumulative1.DataBindings.Clear();
            gvLabCumulative2.DataBindings.Clear();
            gvLabCumulative3.DataBindings.Clear();
            gvLabCumulative4.DataBindings.Clear();
            gvLabCumulative5.DataBindings.Clear();
            gvLabCumulative6.DataBindings.Clear();
            gvLabCumulative1.DataSource = null;
            gvLabCumulative2.DataSource = null;
            gvLabCumulative3.DataSource = null;
            gvLabCumulative4.DataSource = null;
            gvLabCumulative5.DataSource = null;
            gvLabCumulative6.DataSource = null;
            suppressSource.Clear();
        }
        private void insertCommulativeGrid()
        {
            gvLabCumulative1.SelectionChanged -= new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative2.SelectionChanged -= new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative3.SelectionChanged -= new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative4.SelectionChanged -= new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative5.SelectionChanged -= new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative6.SelectionChanged -= new EventHandler(gvComulative_SelectionChanged);

            gvLabCumulative1.DataSource = getComulativeByGroup("LB01");
            gvLabCumulative2.DataSource = getComulativeByGroup("LB02");
            gvLabCumulative3.DataSource = getComulativeByGroup("LB03");
            gvLabCumulative4.DataSource = getComulativeByGroup("LB04");
            gvLabCumulative5.DataSource = getComulativeByGroup("LB05");
            gvLabCumulative6.DataSource = getComulativeByGroup("LB06");

            /*gvLabCumulative1.ClearSelection();
            gvLabCumulative2.ClearSelection();
            gvLabCumulative3.ClearSelection();
            gvLabCumulative4.ClearSelection();
            gvLabCumulative5.ClearSelection();
            gvLabCumulative6.ClearSelection();*/

            gvLabCumulative1.SelectionChanged += new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative2.SelectionChanged += new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative3.SelectionChanged += new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative4.SelectionChanged += new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative5.SelectionChanged += new EventHandler(gvComulative_SelectionChanged);
            gvLabCumulative6.SelectionChanged += new EventHandler(gvComulative_SelectionChanged);
        }
        private void btnRetriveLab_Click(object sender, EventArgs e)
        {
            gvSelectEN.EndEdit();
            currentData = getRetriveToRealData;
            insertCommulativeGrid();
            suppressSource.Clear();
        }
        private void gvComulative_SelectionChanged(object sender, EventArgs e)
        {
            ComulativeSource temp = getCurrentComulative((DataGridView)sender);
            if (temp != null)
            {
                currentComulative = null;
                currentComulative = temp;
            }
            else
            {
                currentComulative = null;
            }
        }
        private void ckSaveSuppressLab_CheckedChanged(object sender, EventArgs e)
        {
            if (currentComulative != null)
            {
                if (currentComulative.labSuppress != ((ckSaveSuppressLab.Checked == true) ? 'Y' : 'N'))
                {
                    currentComulative.labSuppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
                    if (currentComulative.labSuppress == 'Y')
                    {
                        suppressSource.Add(new gvSuppressLabSource
                        {
                            index = suppressSource.Count + 1,
                            labCode = currentComulative.labCode,
                            testItem = currentComulative.labName
                        });
                    }
                    else
                    {
                        suppressSource.Remove(suppressSource.Where(x => x.labCode == currentComulative.labCode).FirstOrDefault());
                        suppressSource = new BindingList<gvSuppressLabSource>(suppressSource.Select((x, _index) => new gvSuppressLabSource
                        {
                            index = _index + 1,
                            labCode = x.labCode,
                            testItem = x.testItem
                        }).ToList());
                    }
                }
            }
        }
        private void rdNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (currentComulative != null && rdNormal.Checked == true) currentComulative.labSummary = 'N';
        }
        private void rdAbNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (currentComulative != null && rdAbNormal.Checked == true) currentComulative.labSummary = 'A';
        }
        private void txtLabRemark_TextChanged(object sender, EventArgs e)
        {
            if (currentComulative != null) currentComulative.labRemark = txtLabRemark.Text;
        }
        private void labsUpdateAll_Click(object sender, EventArgs e)
        {
            checkPointData = currentData;
            ckPointHIV = (ckReportHIV.Checked ? 'Y' : 'N');
            ckPointChart = (ckShowDefaultChart.Checked ? 'Y' : 'N');
            ckPointSuppress = (ckSuppressLab.Checked ? 'Y' : 'N');
        }
        private void frmBookResultSettingByHN_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (compareListComulative(currentData, LoadCheckPointData) && chk_trn_book_hdr) return;
                if ((!compareListComulative(currentData, checkPointData)) || (!chkCheckPointHdr))
                {
                    DialogResult result = MessageBox.Show("You don't Press button Update after Last Edit.\r\nDo you want to save last Edit?", "Warning", MessageBoxButtons.YesNoCancel);
                    switch (result)
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            checkPointData = currentData;
                            break;
                        case System.Windows.Forms.DialogResult.No:
                            if (checkPointData.Count == 0) return;
                            break;
                        case System.Windows.Forms.DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }
                else
                {
                    return;
                }

                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    trn_book_hdr tbh = dbc.trn_book_hdrs.Where(x => x.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    tbh.tkh_show_chart = (ckShowDefaultChart.Checked) ? 'Y' : 'N';
                    tbh.tkh_report_HIV = (ckReportHIV.Checked) ? 'Y' : 'N';
                    tbh.tkh_suppress_lab = (ckSaveSuppressLab.Checked) ? 'Y' : 'N';

                    string create_by = Program.CurrentUser.mut_username;
                    DateTime create_date = Program.GetServerDateTime();
                    if (tbh.trn_book_labs.Count > 0)
                    {
                        tbh.trn_book_labs.Select(x => new
                        {
                            create_by = x.tklb_create_by,
                            create_date = x.tklb_create_date
                        }).FirstOrDefault();
                        dbc.trn_book_labs.DeleteAllOnSubmit(tbh.trn_book_labs);
                    }
                    List<trn_book_lab> tbl = checkPointData.Select(x => new trn_book_lab
                    {
                        mhs_id = x.labMhsID,
                        tklb_en_no = x.enNo,
                        tklb_lab_date = x.labDate,
                        tklb_lab_name = x.labName,
                        tklb_lab_no = x.labCode,
                        tklb_lab_range = x.labRange,
                        tklb_lab_unit = x.labUnit,
                        tklb_lab_value = x.labValue,
                        tklb_remark = x.labRemark,
                        tklb_summary = x.labSummary,
                        tklb_suppress = x.labSuppress,
                        tklb_lab_result_thai = x.labResult,
                        tklb_create_by = create_by,
                        tklb_create_date = create_date,
                        tklb_update_by = Program.CurrentUser.mut_username,
                        tklb_update_date = Program.GetServerDateTime()
                    }).ToList();
                    tbh.trn_book_labs.AddRange(tbl);
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                            .Where(x => x.tpr_id == Program.CurrentRegis.tpr_id &&
                                                                        x.tpbr_radiology == "LB")
                                                            .FirstOrDefault();
                    if (bookResult == null)
                    {
                        bookResult = new trn_patient_book_result()
                        {
                            tpr_id = Program.CurrentRegis.tpr_id,
                            tpbr_radiology = "LB",
                            tpbr_create_by = Program.CurrentUser.mut_username,
                            tpbr_create_date = dateNow
                        };
                        dbc.trn_patient_book_results.InsertOnSubmit(bookResult);
                    }
                    bookResult.tpbr_flag_saved = true;
                    bookResult.tpbr_show_sections = true;
                    bookResult.tpbr_show_summary = true;
                    bookResult.tpbr_not_show_report = false;
                    bookResult.tpbr_active = true;
                    bookResult.tpbr_update_by = Program.CurrentUser.mut_username;
                    bookResult.tpbr_update_date = dateNow;
                    dbc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void gvLabCumulative_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                ComulativeSource current = (ComulativeSource)dgv.Rows[e.RowIndex].DataBoundItem;
                if (current != null)
                {
                    if (current.labSummary == 'N')
                    {
                        foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
                        {
                            cell.Style = new DataGridViewCellStyle()
                            {
                                ForeColor = Color.Black
                            };
                        }
                    }
                    else
                    {
                        foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
                        {
                            cell.Style = new DataGridViewCellStyle()
                            {
                                ForeColor = Color.Red
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "gvLabCumulative_CellFormatting", ex, false);
            }
        }







        




        #region old code not use

        //private static int? RefID;
        //private static string HN;
        //private static int? svLocationID;
        //private static DateTime? svDateFrom;
        //private static DateTime? svDateTo;

        //private static int? RefBookID_tkh_id;



        //private static List<pw_Get_PatientBook_LabCumulativeResult> MainData_LabResult_ByHN;

        //private static List<pw_Get_PatientBook_LabCumulativeResult> iDataCumulative1;
        //private static List<pw_Get_PatientBook_LabCumulativeResult> objCumulative1;
        //private static List<mst_lab> objMsLab1;
        //private static List<trn_book_lab> objBkLab1;

        //private static List<pw_Get_PatientBook_LabCumulativeResult> iDataCumulative2;
        //private static List<pw_Get_PatientBook_LabCumulativeResult> objCumulative2;
        //private static List<mst_lab> objMsLab2;
        //private static List<trn_book_lab> objBkLab2;

        //private static List<pw_Get_PatientBook_LabCumulativeResult> iDataCumulative3;
        //private static List<pw_Get_PatientBook_LabCumulativeResult> objCumulative3;
        //private static List<mst_lab> objMsLab3;
        //private static List<trn_book_lab> objBkLab3;

        //private static List<pw_Get_PatientBook_LabCumulativeResult> iDataCumulative4;
        //private static List<pw_Get_PatientBook_LabCumulativeResult> objCumulative4;
        //private static List<mst_lab> objMsLab4;
        //private static List<trn_book_lab> objBkLab4;

        //private static List<pw_Get_PatientBook_LabCumulativeResult> iDataCumulative5;
        //private static List<pw_Get_PatientBook_LabCumulativeResult> objCumulative5;
        //private static List<mst_lab> objMsLab5;
        //private static List<trn_book_lab> objBkLab5;

        //private static List<pw_Get_PatientBook_LabCumulativeResult> iDataCumulative6;
        //private static List<pw_Get_PatientBook_LabCumulativeResult> objCumulative6;
        //private static List<mst_lab> objMsLab6;
        //private static List<trn_book_lab> objBkLab6;

        //private static int CurrentPanel = 1;
        //private static string valueTextRemark;
        //private static int? chkEvent_txtremark = 0;


        //private void Load_ViewPanel()
        //{
        //    Panel1.Height = 490;
        //    Panel2.Height = 25;
        //    Panel3.Height = 25;
        //    Panel4.Height = 25;
        //    Panel5.Height = 25;
        //    Panel6.Height = 25;
        //}

        //private static InhCheckupDataContext dbc = new InhCheckupDataContext();

        //private static void LoadData_LabResult_ByHN()
        //{
            
        //    try
        //    {
        //        int cRefBookID_tkh_id = dbc.trn_book_hdrs.Count(c => c.tpr_id == RefID);

        //        if (cRefBookID_tkh_id == 0)
        //        {
        //            InsData_trnBookHdr_ByRefIDs();
        //        }

        //        RefBookID_tkh_id = null;
        //        RefBookID_tkh_id = dbc.trn_book_hdrs.Where(w => w.tpr_id == RefID).Select(s => s.tkh_id).FirstOrDefault();

        //        MainData_LabResult_ByHN = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        MainData_LabResult_ByHN = dbc.pw_Get_PatientBook_LabCumulative(HN, null, null, null).ToList(); //error

        //        List<mst_lab>
        //        MainData_MstLab = dbc.mst_labs.ToList();

        //        List<trn_book_lab>
        //        MainData_BkLab = dbc.trn_book_labs.Where(b => b.tkh_id == RefBookID_tkh_id).ToList();

        //        #region iDataCumulative1

        //        objBkLab1 = new List<trn_book_lab>();
        //        objBkLab1 = MainData_BkLab.Where(l => l.tkh_id == RefBookID_tkh_id).ToList();

        //        objMsLab1 = new List<mst_lab>();
        //        objMsLab1 = MainData_MstLab.Where(m => m.mlg_id == 1).OrderBy(o => o.mlb_code).ToList();

        //        objCumulative1 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulative1 = MainData_LabResult_ByHN.Where(c => c.mlg_id == 1).OrderByDescending(o => o.tpl_en_no).ThenBy(o => o.tpl_lab_no).ToList();

        //        var objCumulativeCurrent1 = objCumulative1.Where(c => c.tpl_lab_date != null && c.tpl_lab_date.Value.Date == Program.GetServerDateTime().Date).ToList();

        //        iDataCumulative1 = new List<pw_Get_PatientBook_LabCumulativeResult>();

        //        if (objCumulativeCurrent1.Count > 0)
        //        {
        //            foreach (var c1 in objCumulativeCurrent1)
        //            {
        //                pw_Get_PatientBook_LabCumulativeResult
        //                dtCumulative1 = new pw_Get_PatientBook_LabCumulativeResult();
        //                dtCumulative1.tpl_en_no = (c1.tpl_en_no != null) ? c1.tpl_en_no : null;
        //                dtCumulative1.tpl_lab_date = c1.tpl_lab_date;
        //                dtCumulative1.mhs_id = (c1.mhs_id != null) ? c1.mhs_id : null;
        //                dtCumulative1.mhs_ename = (c1.mhs_ename != null) ? c1.mhs_ename : null;
        //                dtCumulative1.tpl_lab_no = (c1.tpl_lab_no != null) ? c1.tpl_lab_no : null;
        //                dtCumulative1.tpl_lab_name = (c1.tpl_lab_name != null) ? c1.tpl_lab_name : null;
        //                dtCumulative1.tpl_lab_value = (c1.tpl_lab_value != null) ? c1.tpl_lab_value : null;
        //                dtCumulative1.tpl_lab_range = (c1.tpl_lab_range != null) ? c1.tpl_lab_range : null;
        //                dtCumulative1.tpl_lab_unit = (c1.tpl_lab_unit != null) ? c1.tpl_lab_unit : null;
        //                dtCumulative1.mlp_tname = (c1.mlp_tname != null) ? c1.mlp_tname : null;
        //                dtCumulative1.mlp_ename = (c1.mlp_ename != null) ? c1.mlp_ename : null;
        //                dtCumulative1.vLab_Result = (c1.vLab_Result != null) ? c1.vLab_Result : null;
        //                dtCumulative1.mlp_summary = (c1.mlp_summary != null) ? c1.mlp_summary.ToString() : null;
        //                iDataCumulative1.Add(dtCumulative1);
        //            }
        //        }

        //        #endregion

        //        #region iDataCumulative2

        //        objBkLab2 = new List<trn_book_lab>();
        //        objBkLab2 = MainData_BkLab.Where(l => l.tkh_id == RefBookID_tkh_id).ToList();

        //        objMsLab2 = new List<mst_lab>();
        //        objMsLab2 = MainData_MstLab.Where(m => m.mlg_id == 2).OrderBy(o => o.mlb_code).ToList();

        //        objCumulative2 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulative2 = MainData_LabResult_ByHN.Where(c => c.mlg_id == 2).OrderByDescending(o => o.tpl_en_no).ThenBy(o => o.tpl_lab_no).ToList();

        //        List<pw_Get_PatientBook_LabCumulativeResult>
        //        objCumulativeCurrent2 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulativeCurrent2 = objCumulative2.Where(c => c.tpl_lab_date != null && c.tpl_lab_date.Value.Date == Program.GetServerDateTime().Date).ToList();

        //        iDataCumulative2 = new List<pw_Get_PatientBook_LabCumulativeResult>();

        //        if (objCumulativeCurrent2.Count > 0)
        //        {
        //            foreach (var c2 in objCumulativeCurrent2)
        //            {
        //                pw_Get_PatientBook_LabCumulativeResult
        //                dtCumulative2 = new pw_Get_PatientBook_LabCumulativeResult();
        //                dtCumulative2.tpl_en_no = (c2.tpl_en_no != null) ? c2.tpl_en_no : null;
        //                dtCumulative2.tpl_lab_date = c2.tpl_lab_date;
        //                dtCumulative2.mhs_id = (c2.mhs_id != null) ? c2.mhs_id : null;
        //                dtCumulative2.mhs_ename = (c2.mhs_ename != null) ? c2.mhs_ename : null;
        //                dtCumulative2.tpl_lab_no = (c2.tpl_lab_no != null) ? c2.tpl_lab_no : null;
        //                dtCumulative2.tpl_lab_name = (c2.tpl_lab_name != null) ? c2.tpl_lab_name : null;
        //                dtCumulative2.tpl_lab_value = (c2.tpl_lab_value != null) ? c2.tpl_lab_value : null;
        //                dtCumulative2.tpl_lab_range = (c2.tpl_lab_range != null) ? c2.tpl_lab_range : null;
        //                dtCumulative2.tpl_lab_unit = (c2.tpl_lab_unit != null) ? c2.tpl_lab_unit : null;
        //                dtCumulative2.mlp_tname = (c2.mlp_tname != null) ? c2.mlp_tname : null;
        //                dtCumulative2.mlp_ename = (c2.mlp_ename != null) ? c2.mlp_ename : null;
        //                dtCumulative2.vLab_Result = (c2.vLab_Result != null) ? c2.vLab_Result : null;
        //                dtCumulative2.mlp_summary = (c2.mlp_summary != null) ? c2.mlp_summary.ToString() : null;
        //                iDataCumulative2.Add(dtCumulative2);
        //            }
        //        }

        //        #endregion

        //        #region iDataCumulative3

        //        objBkLab3 = new List<trn_book_lab>();
        //        objBkLab3 = MainData_BkLab.Where(l => l.tkh_id == RefBookID_tkh_id).ToList();

        //        objMsLab3 = new List<mst_lab>();
        //        objMsLab3 = MainData_MstLab.Where(m => m.mlg_id == 3).OrderBy(o => o.mlb_code).ToList();

        //        objCumulative3 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulative3 = MainData_LabResult_ByHN.Where(c => c.mlg_id == 3).OrderByDescending(o => o.tpl_en_no).ThenBy(o => o.tpl_lab_no).ToList();

        //        List<pw_Get_PatientBook_LabCumulativeResult>
        //        objCumulativeCurrent3 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulativeCurrent3 = objCumulative3.Where(c => c.tpl_lab_date != null && c.tpl_lab_date.Value.Date == Program.GetServerDateTime().Date).ToList();

        //        iDataCumulative3 = new List<pw_Get_PatientBook_LabCumulativeResult>();

        //        if (objCumulativeCurrent3.Count > 0)
        //        {
        //            foreach (var c3 in objCumulativeCurrent3)
        //            {
        //                pw_Get_PatientBook_LabCumulativeResult
        //                dtCumulative3 = new pw_Get_PatientBook_LabCumulativeResult();
        //                dtCumulative3.tpl_en_no = (c3.tpl_en_no != null) ? c3.tpl_en_no : null;
        //                dtCumulative3.tpl_lab_date = c3.tpl_lab_date;
        //                dtCumulative3.mhs_id = (c3.mhs_id != null) ? c3.mhs_id : null;
        //                dtCumulative3.mhs_ename = (c3.mhs_ename != null) ? c3.mhs_ename : null;
        //                dtCumulative3.tpl_lab_no = (c3.tpl_lab_no != null) ? c3.tpl_lab_no : null;
        //                dtCumulative3.tpl_lab_name = (c3.tpl_lab_name != null) ? c3.tpl_lab_name : null;
        //                dtCumulative3.tpl_lab_value = (c3.tpl_lab_value != null) ? c3.tpl_lab_value : null;
        //                dtCumulative3.tpl_lab_range = (c3.tpl_lab_range != null) ? c3.tpl_lab_range : null;
        //                dtCumulative3.tpl_lab_unit = (c3.tpl_lab_unit != null) ? c3.tpl_lab_unit : null;
        //                dtCumulative3.mlp_tname = (c3.mlp_tname != null) ? c3.mlp_tname : null;
        //                dtCumulative3.mlp_ename = (c3.mlp_ename != null) ? c3.mlp_ename : null;
        //                dtCumulative3.vLab_Result = (c3.vLab_Result != null) ? c3.vLab_Result : null;
        //                dtCumulative3.mlp_summary = (c3.mlp_summary != null) ? c3.mlp_summary.ToString() : null;
        //                iDataCumulative3.Add(dtCumulative3);
        //            }
        //        }

        //        #endregion

        //        #region iDataCumulative4

        //        objBkLab4 = new List<trn_book_lab>();
        //        objBkLab4 = MainData_BkLab.Where(l => l.tkh_id == RefBookID_tkh_id).ToList();

        //        objMsLab4 = new List<mst_lab>();
        //        objMsLab4 = MainData_MstLab.Where(m => m.mlg_id == 4).OrderBy(o => o.mlb_code).ToList();

        //        objCumulative4 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulative4 = MainData_LabResult_ByHN.Where(c => c.mlg_id == 4).OrderByDescending(o => o.tpl_en_no).ThenBy(o => o.tpl_lab_no).ToList();

        //        List<pw_Get_PatientBook_LabCumulativeResult>
        //        objCumulativeCurrent4 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulativeCurrent4 = objCumulative4.Where(c => c.tpl_lab_date != null && c.tpl_lab_date.Value.Date == Program.GetServerDateTime().Date).ToList();

        //        iDataCumulative4 = new List<pw_Get_PatientBook_LabCumulativeResult>();

        //        if (objCumulativeCurrent4.Count > 0)
        //        {
        //            foreach (var c4 in objCumulativeCurrent4)
        //            {
        //                pw_Get_PatientBook_LabCumulativeResult
        //                dtCumulative4 = new pw_Get_PatientBook_LabCumulativeResult();
        //                dtCumulative4.tpl_en_no = (c4.tpl_en_no != null) ? c4.tpl_en_no : null;
        //                dtCumulative4.tpl_lab_date = c4.tpl_lab_date;
        //                dtCumulative4.mhs_id = (c4.mhs_id != null) ? c4.mhs_id : null;
        //                dtCumulative4.mhs_ename = (c4.mhs_ename != null) ? c4.mhs_ename : null;
        //                dtCumulative4.tpl_lab_no = (c4.tpl_lab_no != null) ? c4.tpl_lab_no : null;
        //                dtCumulative4.tpl_lab_name = (c4.tpl_lab_name != null) ? c4.tpl_lab_name : null;
        //                dtCumulative4.tpl_lab_value = (c4.tpl_lab_value != null) ? c4.tpl_lab_value : null;
        //                dtCumulative4.tpl_lab_range = (c4.tpl_lab_range != null) ? c4.tpl_lab_range : null;
        //                dtCumulative4.tpl_lab_unit = (c4.tpl_lab_unit != null) ? c4.tpl_lab_unit : null;
        //                dtCumulative4.mlp_tname = (c4.mlp_tname != null) ? c4.mlp_tname : null;
        //                dtCumulative4.mlp_ename = (c4.mlp_ename != null) ? c4.mlp_ename : null;
        //                dtCumulative4.vLab_Result = (c4.vLab_Result != null) ? c4.vLab_Result : null;
        //                dtCumulative4.mlp_summary = (c4.mlp_summary != null) ? c4.mlp_summary.ToString() : null;
        //                iDataCumulative4.Add(dtCumulative4);
        //            }
        //        }

        //        #endregion

        //        #region iDataCumulative5

        //        objBkLab5 = new List<trn_book_lab>();
        //        objBkLab5 = MainData_BkLab.Where(l => l.tkh_id == RefBookID_tkh_id).ToList();

        //        objMsLab5 = new List<mst_lab>();
        //        objMsLab5 = MainData_MstLab.Where(m => m.mlg_id == 5).OrderBy(o => o.mlb_code).ToList();

        //        objCumulative5 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulative5 = MainData_LabResult_ByHN.Where(c => c.mlg_id == 5).OrderBy(o => o.tpl_lab_no).OrderByDescending(o => o.tpl_en_no).ToList();

        //        List<pw_Get_PatientBook_LabCumulativeResult>
        //        objCumulativeCurrent5 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulativeCurrent5 = objCumulative5.Where(c => c.tpl_lab_date != null && c.tpl_lab_date.Value.Date == Program.GetServerDateTime().Date).OrderBy(o => o.tpl_lab_no).OrderByDescending(o => o.tpl_en_no).ToList();

        //        iDataCumulative5 = new List<pw_Get_PatientBook_LabCumulativeResult>();

        //        if (objCumulativeCurrent5.Count > 0)
        //        {
        //            foreach (var c5 in objCumulativeCurrent5)
        //            {
        //                pw_Get_PatientBook_LabCumulativeResult
        //                dtCumulative5 = new pw_Get_PatientBook_LabCumulativeResult();
        //                dtCumulative5.tpl_en_no = (c5.tpl_en_no != null) ? c5.tpl_en_no : null;
        //                dtCumulative5.tpl_lab_date = c5.tpl_lab_date;
        //                dtCumulative5.mhs_id = (c5.mhs_id != null) ? c5.mhs_id : null;
        //                dtCumulative5.mhs_ename = (c5.mhs_ename != null) ? c5.mhs_ename : null;
        //                dtCumulative5.tpl_lab_no = (c5.tpl_lab_no != null) ? c5.tpl_lab_no : null;
        //                dtCumulative5.tpl_lab_name = (c5.tpl_lab_name != null) ? c5.tpl_lab_name : null;
        //                dtCumulative5.tpl_lab_value = (c5.tpl_lab_value != null) ? c5.tpl_lab_value : null;
        //                dtCumulative5.tpl_lab_range = (c5.tpl_lab_range != null) ? c5.tpl_lab_range : null;
        //                dtCumulative5.tpl_lab_unit = (c5.tpl_lab_unit != null) ? c5.tpl_lab_unit : null;
        //                dtCumulative5.mlp_tname = (c5.mlp_tname != null) ? c5.mlp_tname : null;
        //                dtCumulative5.mlp_ename = (c5.mlp_ename != null) ? c5.mlp_ename : null;
        //                dtCumulative5.vLab_Result = (c5.vLab_Result != null) ? c5.vLab_Result : null;
        //                dtCumulative5.mlp_summary = (c5.mlp_summary != null) ? c5.mlp_summary.ToString() : null;
        //                iDataCumulative5.Add(dtCumulative5);
        //            }
        //        }

        //        #endregion

        //        #region iDataCumulative6

        //        objBkLab6 = new List<trn_book_lab>();
        //        objBkLab6 = MainData_BkLab.Where(l => l.tkh_id == RefBookID_tkh_id).ToList();

        //        objMsLab6 = new List<mst_lab>();
        //        objMsLab6 = MainData_MstLab.Where(m => m.mlg_id == 6).OrderBy(o => o.mlb_code).ToList();

        //        objCumulative6 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulative6 = MainData_LabResult_ByHN.Where(c => c.mlg_id == 6).OrderBy(o => o.tpl_lab_no).OrderByDescending(o => o.tpl_en_no).ToList();

        //        List<pw_Get_PatientBook_LabCumulativeResult>
        //        objCumulativeCurrent6 = new List<pw_Get_PatientBook_LabCumulativeResult>();
        //        objCumulativeCurrent6 = objCumulative6.Where(c => c.tpl_lab_date != null && c.tpl_lab_date.Value.Date == Program.GetServerDateTime().Date).OrderBy(o => o.tpl_lab_no).OrderByDescending(o => o.tpl_en_no).ToList();

        //        iDataCumulative6 = new List<pw_Get_PatientBook_LabCumulativeResult>();

        //        if (objCumulativeCurrent6.Count > 0)
        //        {
        //            foreach (var c6 in objCumulativeCurrent6)
        //            {
        //                pw_Get_PatientBook_LabCumulativeResult
        //                dtCumulative6 = new pw_Get_PatientBook_LabCumulativeResult();
        //                dtCumulative6.tpl_en_no = (c6.tpl_en_no != null) ? c6.tpl_en_no : null;
        //                dtCumulative6.tpl_lab_date = c6.tpl_lab_date;
        //                dtCumulative6.mhs_id = (c6.mhs_id != null) ? c6.mhs_id : null;
        //                dtCumulative6.mhs_ename = (c6.mhs_ename != null) ? c6.mhs_ename : null;
        //                dtCumulative6.tpl_lab_no = (c6.tpl_lab_no != null) ? c6.tpl_lab_no : null;
        //                dtCumulative6.tpl_lab_name = (c6.tpl_lab_name != null) ? c6.tpl_lab_name : null;
        //                dtCumulative6.tpl_lab_value = (c6.tpl_lab_value != null) ? c6.tpl_lab_value : null;
        //                dtCumulative6.tpl_lab_range = (c6.tpl_lab_range != null) ? c6.tpl_lab_range : null;
        //                dtCumulative6.tpl_lab_unit = (c6.tpl_lab_unit != null) ? c6.tpl_lab_unit : null;
        //                dtCumulative6.mlp_tname = (c6.mlp_tname != null) ? c6.mlp_tname : null;
        //                dtCumulative6.mlp_ename = (c6.mlp_ename != null) ? c6.mlp_ename : null;
        //                dtCumulative6.vLab_Result = (c6.vLab_Result != null) ? c6.vLab_Result : null;
        //                dtCumulative6.mlp_summary = (c6.mlp_summary != null) ? c6.mlp_summary.ToString() : null;
        //                iDataCumulative6.Add(dtCumulative6);
        //            }
        //        }

        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    #region backup code

        //    //Mst_lab insert into object

        //    //foreach (var l1 in objMsLab1)
        //    //{
        //    //    pw_Get_PatientBook_LabCumulativeResult
        //    //    dtMsLab1 = new pw_Get_PatientBook_LabCumulativeResult();
        //    //    dtMsLab1.tpl_lab_no = l1.mlb_code;
        //    //    dtMsLab1.tpl_lab_name = l1.mlb_ename;
        //    //    iDataCumulative1.Add(dtMsLab1);
        //    //}

        //    //Parallel.ForEach(objMsLab2, (l2) =>
        //    //{
        //    //    pw_Get_PatientBook_LabCumulativeResult
        //    //    dtMsLab2 = new pw_Get_PatientBook_LabCumulativeResult();
        //    //    dtMsLab2.tpl_lab_no = l2.mlb_code;
        //    //    dtMsLab2.tpl_lab_name = l2.mlb_ename;
        //    //    iDataCumulative2.Add(dtMsLab2);
        //    //});

        //    #endregion
        //}

        //private void Check_objCumulativeLab()
        //{
        //    try
        //    {
        //        //if (objCumulative1 != null && objCumulative2 != null && objCumulative3 != null && objCumulative4 != null && objCumulative5 != null && objCumulative6 != null)
        //        //{
        //            LoadBindData_gvSelectEN();
        //            LoadBindData_RetriveLab();
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvLabCumulative(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    BindData_gvLabCumulative1(HN, svLocationID, svDateFrom, svDateTo);
        //    BindData_gvLabCumulative2(HN, svLocationID, svDateFrom, svDateTo);
        //    BindData_gvLabCumulative3(HN, svLocationID, svDateFrom, svDateTo);
        //    BindData_gvLabCumulative4(HN, svLocationID, svDateFrom, svDateTo);
        //    BindData_gvLabCumulative5(HN, svLocationID, svDateFrom, svDateTo);
        //    BindData_gvLabCumulative6(HN, svLocationID, svDateFrom, svDateTo);
        //}

        //#region BindData

        

        //private void BindData_SuppressLab()
        //{
        //    try
        //    {
        //        var SuppressLab
        //            = objBkLab1.Union(objBkLab2);
        //            SuppressLab
        //            = SuppressLab.Union(objBkLab3);
        //            SuppressLab
        //            = SuppressLab.Union(objBkLab4);
        //            SuppressLab
        //            = SuppressLab.Union(objBkLab5);
        //            SuppressLab
        //            = SuppressLab.Union(objBkLab6);

        //        var BindgvSuppressLab = SuppressLab.Where(c => c.tklb_suppress == 'Y')
        //                                .Select(c => new
        //                                {
        //                                    dEN = (c.tklb_en_no != null) ? c.tklb_en_no.ToString() : null,
        //                                    dLabCode = (c.tklb_lab_no != null) ? c.tklb_lab_no.ToString() : null,
        //                                    dTestItem = (c.tklb_lab_name != null) ? c.tklb_lab_name.ToString() : null,
        //                                }).ToList();

        //        gvSuppressLab.DataSource = BindgvSuppressLab
        //                                    .OrderByDescending(o => o.dEN)
        //                                    .DistinctBy(d => new { d.dLabCode })
        //                                    .Select((item, index) => new
        //                                    {
        //                                        cTestItemNo = index + 1,
        //                                        cLabCode = item.dLabCode,
        //                                        cTestItem = item.dTestItem,
        //                                    }).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvSelectEN(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    try
        //    {
        //        if (svLocationID == 0 || svLocationID == null)
        //        {
        //            var BindgvSelectEN = MainData_LabResult_ByHN
        //                                .Where(m =>
        //                                        m.tpl_lab_date.Value.Date >= svDateFrom.Value.Date &&
        //                                        m.tpl_lab_date.Value.Date <= svDateTo.Value.Date &&
        //                                        m.tpl_en_no != null
        //                                        )
        //                                        .Select(t1 => new
        //                                        {
        //                                            dEN = t1.tpl_en_no,
        //                                            dEN_Date = (t1.tpl_lab_date != null) ? t1.tpl_lab_date : null,
        //                                            dEN_Location = t1.mhs_ename,
        //                                            dEN_Select = (t1.tpl_lab_date.Value.Date.ToString() == Program.GetServerDateTime().Date.ToString()) ? 1 : 0
        //                                        }
        //                                        ).ToList();

        //            gvSelectEN.DataSource = BindgvSelectEN
        //                                    .OrderByDescending(o => o.dEN)
        //                                    .ThenBy(o => o.dEN_Date)
        //                                    .DistinctBy(d => new { d.dEN })
        //                                    .Select((item, index) => new
        //                                    {
        //                                        cEN = item.dEN,
        //                                        cENdate = item.dEN_Date,
        //                                        cLocation = item.dEN_Location,
        //                                        cSelectx = item.dEN_Select
        //                                    }).ToList();
        //        }
        //        else
        //        {
        //            var BindgvSelectEN = MainData_LabResult_ByHN
        //                                .Where(m =>
        //                                        m.mhs_id == svLocationID &&
        //                                        m.tpl_lab_date.Value.Date >= svDateFrom.Value.Date &&
        //                                        m.tpl_lab_date.Value.Date <= svDateTo.Value.Date &&
        //                                        m.tpl_en_no != null
        //                                        )
        //                                        .Select(t1 => new
        //                                        {
        //                                            dEN = t1.tpl_en_no,
        //                                            dEN_Date = (t1.tpl_lab_date != null) ? t1.tpl_lab_date : null,
        //                                            dEN_Location = t1.mhs_ename,
        //                                            dEN_Select = (t1.tpl_lab_date.Value.Date.ToString() == Program.GetServerDateTime().Date.ToString()) ? 1 : 0
        //                                        }
        //                                        ).ToList();

        //            gvSelectEN.DataSource = BindgvSelectEN
        //                                    .OrderByDescending(o => o.dEN)
        //                                    .ThenBy(o => o.dEN_Date)
        //                                    .DistinctBy(d => new { d.dEN })
        //                                    .Select((item, index) => new
        //                                    {
        //                                        cEN = item.dEN,
        //                                        cENdate = item.dEN_Date,
        //                                        cLocation = item.dEN_Location,
        //                                        cSelectx = item.dEN_Select
        //                                    }).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvLabCumulative1(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    try
        //    {
        //        //DataSource_gvLabCumulative1
        //        var BindgvLabCumulative1 = iDataCumulative1.Where(c => c.tpl_lab_no != null)
        //                                    .Select(c => new
        //                                    {
        //                                        dEN = (c.tpl_en_no != null) ? c.tpl_en_no.ToString() : null,
        //                                        dEN_Date = c.tpl_lab_date,
        //                                        dEN_Location = (c.mhs_id != 0 && c.mhs_id != null) ? c.mhs_ename.ToString() : null,
        //                                        dLabCode = (c.tpl_lab_no != null) ? c.tpl_lab_no.ToString() : null,
        //                                        dTestItem = (c.tpl_lab_name != null) ? c.tpl_lab_name.ToString() : null,
        //                                        dResultValue = (c.tpl_lab_value != null) ? c.tpl_lab_value.ToString() : null,
        //                                        dRefRange = (c.tpl_lab_range != null) ? c.tpl_lab_range.ToString() : null,
        //                                        dUnit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit.ToString() : null,
        //                                        dLabResult_th = (c.mlp_tname != null) ? c.mlp_tname.ToString() : null,
        //                                        dLabResult_en = (c.mlp_ename != null) ? c.mlp_ename.ToString() : null,
        //                                        dLabResultChk = (c.vLab_Result != null) ? c.vLab_Result.ToString() : null,
        //                                        dLabABNormal = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null
        //                                    }).ToList();

        //        //BindData_gvCumuLablative1
        //        switch (UL)
        //        {
        //            case "TH":
        //                gvLabCumulative1.DataSource = BindgvLabCumulative1
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu1_EN = item.dEN,
        //                                                    cLabCumu1_LabCode = item.dLabCode,
        //                                                    cLabCumu1_TestItem = (item.dLabCode != null) ? objMsLab1.Where(w => w.mlb_code == item.dLabCode).FirstOrDefault().mlb_tname.ToString() : "",
        //                                                    cLabCumu1_Result = item.dResultValue,
        //                                                    cLabCumu1_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu1_Units = item.dUnit,
        //                                                    cLabCumu1_Translate = item.dLabResult_th,
        //                                                    cLabCumu1_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;

        //            case "EN":
        //                gvLabCumulative1.DataSource = BindgvLabCumulative1
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu1_EN = item.dEN,
        //                                                    cLabCumu1_LabCode = item.dLabCode,
        //                                                    cLabCumu1_TestItem = item.dTestItem,
        //                                                    cLabCumu1_Result = item.dResultValue,
        //                                                    cLabCumu1_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu1_Units = item.dUnit,
        //                                                    cLabCumu1_Translate = item.dLabResult_en,
        //                                                    cLabCumu1_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvLabCumulative2(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    try
        //    {
        //        //DataSource_gvLabCumulative2
        //        var BindgvLabCumulative2 = iDataCumulative2.Where(c => c.tpl_lab_no != null)
        //                                    .Select(c => new
        //                                    {
        //                                        dEN = (c.tpl_en_no != null) ? c.tpl_en_no.ToString() : null,
        //                                        dEN_Date = c.tpl_lab_date,
        //                                        dEN_Location = (c.mhs_id != 0 && c.mhs_id != null) ? c.mhs_ename.ToString() : null,
        //                                        dLabCode = (c.tpl_lab_no != null) ? c.tpl_lab_no.ToString() : null,
        //                                        dTestItem = (c.tpl_lab_name != null) ? c.tpl_lab_name.ToString() : null,
        //                                        dResultValue = (c.tpl_lab_value != null) ? c.tpl_lab_value.ToString() : null,
        //                                        dRefRange = (c.tpl_lab_range != null) ? c.tpl_lab_range.ToString() : null,
        //                                        dUnit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit.ToString() : null,
        //                                        dLabResult_th = (c.mlp_tname != null) ? c.mlp_tname.ToString() : null,
        //                                        dLabResult_en = (c.mlp_ename != null) ? c.mlp_ename.ToString() : null,
        //                                        dLabResultChk = (c.vLab_Result != null) ? c.vLab_Result.ToString() : null,
        //                                        dLabABNormal = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null
        //                                    }).ToList();

        //        //BindData_gvCumuLablative2
        //        switch (UL)
        //        {
        //            case "TH":
        //                gvLabCumulative2.DataSource = BindgvLabCumulative2
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu2_EN = item.dEN,
        //                                                    cLabCumu2_LabCode = item.dLabCode,
        //                                                    cLabCumu2_TestItem = (item.dLabCode != null) ? objMsLab2.Where(w => w.mlb_code == item.dLabCode).FirstOrDefault().mlb_tname.ToString() : "",
        //                                                    cLabCumu2_Result = item.dResultValue,
        //                                                    cLabCumu2_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu2_Units = item.dUnit,
        //                                                    cLabCumu2_Translate = item.dLabResult_th,
        //                                                    cLabCumu2_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;

        //            case "EN":
        //                gvLabCumulative2.DataSource = BindgvLabCumulative2
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu2_EN = item.dEN,
        //                                                    cLabCumu2_LabCode = item.dLabCode,
        //                                                    cLabCumu2_TestItem = item.dTestItem,
        //                                                    cLabCumu2_Result = item.dResultValue,
        //                                                    cLabCumu2_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu2_Units = item.dUnit,
        //                                                    cLabCumu2_Translate = item.dLabResult_en,
        //                                                    cLabCumu2_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvLabCumulative3(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    try
        //    {
        //        //DataSource_gvLabCumulative3
        //        var BindgvLabCumulative3 = iDataCumulative3.Where(c => c.tpl_lab_no != null)
        //                                    .Select(c => new
        //                                    {
        //                                        dEN = (c.tpl_en_no != null) ? c.tpl_en_no.ToString() : null,
        //                                        dEN_Date = c.tpl_lab_date,
        //                                        dEN_Location = (c.mhs_id != 0 && c.mhs_id != null) ? c.mhs_ename.ToString() : null,
        //                                        dLabCode = (c.tpl_lab_no != null) ? c.tpl_lab_no.ToString() : null,
        //                                        dTestItem = (c.tpl_lab_name != null) ? c.tpl_lab_name.ToString() : null,
        //                                        dResultValue = (c.tpl_lab_value != null) ? c.tpl_lab_value.ToString() : null,
        //                                        dRefRange = (c.tpl_lab_range != null) ? c.tpl_lab_range.ToString() : null,
        //                                        dUnit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit.ToString() : null,
        //                                        dLabResult_th = (c.mlp_tname != null) ? c.mlp_tname.ToString() : null,
        //                                        dLabResult_en = (c.mlp_ename != null) ? c.mlp_ename.ToString() : null,
        //                                        dLabResultChk = (c.vLab_Result != null) ? c.vLab_Result.ToString() : null,
        //                                        dLabABNormal = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null
        //                                    }).ToList();

        //        //BindData_gvCumuLablative3
        //        switch (UL)
        //        {
        //            case "TH":
        //                gvLabCumulative3.DataSource = BindgvLabCumulative3
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu3_EN = item.dEN,
        //                                                    cLabCumu3_LabCode = item.dLabCode,
        //                                                    cLabCumu3_TestItem = (item.dLabCode != null) ? objMsLab3.Where(w => w.mlb_code == item.dLabCode).FirstOrDefault().mlb_tname.ToString() : "",
        //                                                    cLabCumu3_Result = item.dResultValue,
        //                                                    cLabCumu3_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu3_Units = item.dUnit,
        //                                                    cLabCumu3_Translate = item.dLabResult_th,
        //                                                    cLabCumu3_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;

        //            case "EN":
        //                gvLabCumulative3.DataSource = BindgvLabCumulative3
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu3_EN = item.dEN,
        //                                                    cLabCumu3_LabCode = item.dLabCode,
        //                                                    cLabCumu3_TestItem = item.dTestItem,
        //                                                    cLabCumu3_Result = item.dResultValue,
        //                                                    cLabCumu3_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu3_Units = item.dUnit,
        //                                                    cLabCumu3_Translate = item.dLabResult_en,
        //                                                    cLabCumu3_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvLabCumulative4(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    try
        //    {
        //        //DataSource_gvLabCumulative4
        //        var BindgvLabCumulative4 = iDataCumulative4.Where(c => c.tpl_lab_no != null)
        //                                    .Select(c => new
        //                                    {
        //                                        dEN = (c.tpl_en_no != null) ? c.tpl_en_no.ToString() : null,
        //                                        dEN_Date = c.tpl_lab_date,
        //                                        dEN_Location = (c.mhs_id != 0 && c.mhs_id != null) ? c.mhs_ename.ToString() : null,
        //                                        dLabCode = (c.tpl_lab_no != null) ? c.tpl_lab_no.ToString() : null,
        //                                        dTestItem = (c.tpl_lab_name != null) ? c.tpl_lab_name.ToString() : null,
        //                                        dResultValue = (c.tpl_lab_value != null) ? c.tpl_lab_value.ToString() : null,
        //                                        dRefRange = (c.tpl_lab_range != null) ? c.tpl_lab_range.ToString() : null,
        //                                        dUnit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit.ToString() : null,
        //                                        dLabResult_th = (c.mlp_tname != null) ? c.mlp_tname.ToString() : null,
        //                                        dLabResult_en = (c.mlp_ename != null) ? c.mlp_ename.ToString() : null,
        //                                        dLabResultChk = (c.vLab_Result != null) ? c.vLab_Result.ToString() : null,
        //                                        dLabABNormal = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null
        //                                    }).ToList();

        //        //BindData_gvCumuLablative4
        //        switch (UL)
        //        {
        //            case "TH":
        //                gvLabCumulative4.DataSource = BindgvLabCumulative4
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu4_EN = item.dEN,
        //                                                    cLabCumu4_LabCode = item.dLabCode,
        //                                                    cLabCumu4_TestItem = (item.dLabCode != null) ? objMsLab4.Where(w => w.mlb_code == item.dLabCode).FirstOrDefault().mlb_tname.ToString() : "",
        //                                                    cLabCumu4_Result = item.dResultValue,
        //                                                    cLabCumu4_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu4_Units = item.dUnit,
        //                                                    cLabCumu4_Translate = item.dLabResult_th,
        //                                                    cLabCumu4_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;

        //            case "EN":
        //                gvLabCumulative4.DataSource = BindgvLabCumulative4
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu4_EN = item.dEN,
        //                                                    cLabCumu4_LabCode = item.dLabCode,
        //                                                    cLabCumu4_TestItem = item.dTestItem,
        //                                                    cLabCumu4_Result = item.dResultValue,
        //                                                    cLabCumu4_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu4_Units = item.dUnit,
        //                                                    cLabCumu4_Translate = item.dLabResult_en,
        //                                                    cLabCumu4_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvLabCumulative5(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    try
        //    {
        //        //DataSource_gvLabCumulative5
        //        var BindgvLabCumulative5 = iDataCumulative5.Where(c => c.tpl_lab_no != null)
        //                                    .Select(c => new
        //                                    {
        //                                        dEN = (c.tpl_en_no != null) ? c.tpl_en_no.ToString() : null,
        //                                        dEN_Date = c.tpl_lab_date,
        //                                        dEN_Location = (c.mhs_id != 0 && c.mhs_id != null) ? c.mhs_ename.ToString() : null,
        //                                        dLabCode = (c.tpl_lab_no != null) ? c.tpl_lab_no.ToString() : null,
        //                                        dTestItem = (c.tpl_lab_name != null) ? c.tpl_lab_name.ToString() : null,
        //                                        dResultValue = (c.tpl_lab_value != null) ? c.tpl_lab_value.ToString() : null,
        //                                        dRefRange = (c.tpl_lab_range != null) ? c.tpl_lab_range.ToString() : null,
        //                                        dUnit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit.ToString() : null,
        //                                        dLabResult_th = (c.mlp_tname != null) ? c.mlp_tname.ToString() : null,
        //                                        dLabResult_en = (c.mlp_ename != null) ? c.mlp_ename.ToString() : null,
        //                                        dLabResultChk = (c.vLab_Result != null) ? c.vLab_Result.ToString() : null,
        //                                        dLabABNormal = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null
        //                                    }).ToList();

        //        //BindData_gvCumuLablative5
        //        switch (UL)
        //        {
        //            case "TH":
        //                gvLabCumulative5.DataSource = BindgvLabCumulative5
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu5_EN = item.dEN,
        //                                                    cLabCumu5_LabCode = item.dLabCode,
        //                                                    cLabCumu5_TestItem = (item.dLabCode != null) ? objMsLab5.Where(w => w.mlb_code == item.dLabCode).FirstOrDefault().mlb_tname.ToString() : "",
        //                                                    cLabCumu5_Result = item.dResultValue,
        //                                                    cLabCumu5_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu5_Units = item.dUnit,
        //                                                    cLabCumu5_Translate = item.dLabResult_th,
        //                                                    cLabCumu5_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;

        //            case "EN":
        //                gvLabCumulative5.DataSource = BindgvLabCumulative5
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu5_EN = item.dEN,
        //                                                    cLabCumu5_LabCode = item.dLabCode,
        //                                                    cLabCumu5_TestItem = item.dTestItem,
        //                                                    cLabCumu5_Result = item.dResultValue,
        //                                                    cLabCumu5_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu5_Units = item.dUnit,
        //                                                    cLabCumu5_Translate = item.dLabResult_en,
        //                                                    cLabCumu5_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void BindData_gvLabCumulative6(string HN, int? svLocationID, DateTime? svDateFrom, DateTime? svDateTo)
        //{
        //    try
        //    {
        //        //DataSource_gvLabCumulative6
        //        var BindgvLabCumulative6 = iDataCumulative6.Where(c => c.tpl_lab_no != null)
        //                                    .Select(c => new
        //                                    {
        //                                        dEN = (c.tpl_en_no != null) ? c.tpl_en_no.ToString() : null,
        //                                        dEN_Date = c.tpl_lab_date,
        //                                        dEN_Location = (c.mhs_id != 0 && c.mhs_id != null) ? c.mhs_ename.ToString() : null,
        //                                        dLabCode = (c.tpl_lab_no != null) ? c.tpl_lab_no.ToString() : null,
        //                                        dTestItem = (c.tpl_lab_name != null) ? c.tpl_lab_name.ToString() : null,
        //                                        dResultValue = (c.tpl_lab_value != null) ? c.tpl_lab_value.ToString() : null,
        //                                        dRefRange = (c.tpl_lab_range != null) ? c.tpl_lab_range.ToString() : null,
        //                                        dUnit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit.ToString() : null,
        //                                        dLabResult_th = (c.mlp_tname != null) ? c.mlp_tname.ToString() : null,
        //                                        dLabResult_en = (c.mlp_ename != null) ? c.mlp_ename.ToString() : null,
        //                                        dLabResultChk = (c.vLab_Result != null) ? c.vLab_Result.ToString() : null,
        //                                        dLabABNormal = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null
        //                                    }).ToList();

        //        //BindData_gvCumuLablative6
        //        switch (UL)
        //        {
        //            case "TH":
        //                gvLabCumulative6.DataSource = BindgvLabCumulative6
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu6_EN = item.dEN,
        //                                                    cLabCumu6_LabCode = item.dLabCode,
        //                                                    cLabCumu6_TestItem = (item.dLabCode != null) ? objMsLab6.Where(w => w.mlb_code == item.dLabCode).FirstOrDefault().mlb_tname.ToString() : "",
        //                                                    cLabCumu6_Result = item.dResultValue,
        //                                                    cLabCumu6_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu6_Units = item.dUnit,
        //                                                    cLabCumu6_Translate = item.dLabResult_th,
        //                                                    cLabCumu6_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;

        //            case "EN":
        //                gvLabCumulative6.DataSource = BindgvLabCumulative6
        //                                                .OrderByDescending(o => o.dEN)
        //                                                .DistinctBy(d => new { d.dLabCode })
        //                                                .Select((item, index) => new
        //                                                {
        //                                                    cLabCumu6_EN = item.dEN,
        //                                                    cLabCumu6_LabCode = item.dLabCode,
        //                                                    cLabCumu6_TestItem = item.dTestItem,
        //                                                    cLabCumu6_Result = item.dResultValue,
        //                                                    cLabCumu6_ReferenceRange = item.dRefRange,
        //                                                    cLabCumu6_Units = item.dUnit,
        //                                                    cLabCumu6_Translate = item.dLabResult_en,
        //                                                    cLabCumu6_LabABNormal = (item.dLabABNormal == null) ? "" : item.dLabABNormal
        //                                                }).ToList();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //#endregion

        //#region Action Control

        //private void LoadBindData_gvSelectEN()
        //{
        //    try
        //    {
        //        HN = string.IsNullOrEmpty(lblHN.Text) ? null : lblHN.Text;
        //        svLocationID = ChkLocation();
        //        svDateFrom = txtVDateFrom.Value;
        //        svDateTo = txtVDateTo.Value;
                
        //        BindData_gvSelectEN(HN, svLocationID, svDateFrom, svDateTo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void LoadBindData_RetriveLab()
        //{
        //    try
        //    {
        //        HN = string.IsNullOrEmpty(lblHN.Text) ? null : lblHN.Text;
        //        svLocationID = ChkLocation();
        //        svDateFrom = txtVDateFrom.Value;
        //        svDateTo = txtVDateTo.Value;

        //        BindData_gvLabCumulative(HN, svLocationID, svDateFrom, svDateTo);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void LoadData_CheckboxHdr()
        //{
        //    try
        //    {
        //        var ReportHIVck = (Convert.ToChar(dbc.trn_book_hdrs.Where(w => w.tpr_id == RefID).Select(s => s.tkh_report_HIV).FirstOrDefault()) != 'Y') ? ckReportHIV.Checked = false : ckReportHIV.Checked = true;
        //        var ShowDefaultChartck = (Convert.ToChar(dbc.trn_book_hdrs.Where(w => w.tpr_id == RefID).Select(s => s.tkh_show_chart).FirstOrDefault()) == 'N') ? ckShowDefaultChart.Checked = false : ckShowDefaultChart.Checked = true;
        //        var SuppressLabck = (Convert.ToChar(dbc.trn_book_hdrs.Where(w => w.tpr_id == RefID).Select(s => s.tkh_suppress_lab).FirstOrDefault()) == 'N') ? ckSuppressLab.Checked = false : ckSuppressLab.Checked = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        

        

        //private int? ChkLocation()
        //{
        //    try
        //    {
        //        int? chkLocation;
        //        if (rdAllLocation.Checked != true)
        //        {
        //            chkLocation = (rdHpcOnly.Checked == true) ? Convert.ToInt32(cbLocation.SelectedValue) : 0;
        //        }
        //        else
        //        {
        //            chkLocation = Convert.ToInt32(cbLocation.SelectedValue); 
        //        }
        //        return chkLocation;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void clearData()
        //{
        //    rdAbNormal.Checked = false;
        //    rdNormal.Checked = false;
        //    ckSaveSuppressLab.Checked = false;
        //    txtLabRemark.Text = "";
        //}

        //private static void InsData_trnBookHdr_ByRefIDs()
        //{
        //    try
        //    {
        //        if (RefID != null)
        //        {
        //            CheckupDataContext 
        //            dbcInsBookhr = new CheckupDataContext(Program.Connectionstring);
        //            dbcInsBookhr.Connection.Open();

        //            trn_book_hdr iBook_hdr = new trn_book_hdr();
        //            iBook_hdr.tpr_id = Convert.ToInt32(RefID);
        //            iBook_hdr.tkh_create_by = Program.CurrentUser.mut_username;
        //            iBook_hdr.tkh_create_date = Program.GetServerDateTime();
        //            iBook_hdr.tkh_update_by = Program.CurrentUser.mut_username;
        //            iBook_hdr.tkh_update_date = Program.GetServerDateTime();

        //            dbcInsBookhr.trn_book_hdrs.InsertOnSubmit(iBook_hdr);
        //            dbcInsBookhr.SubmitChanges();
        //            dbcInsBookhr.Connection.Close();

        //            MessageBox.Show("InsData_trnBookHdr_ByRefID() is Completed : trn_book_hdr.Insert(ins => ins new { tpr_id = RefIDs(" + RefID + ") })");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private static void foreach_objCumulative1(List<pw_Get_PatientBook_LabCumulativeResult> objCumulative1)
        //{
        //    try
        //    {
        //        foreach (var c in objCumulative1)
        //        {
        //            pw_Get_PatientBook_LabCumulativeResult
        //            dtCumulative1 = new pw_Get_PatientBook_LabCumulativeResult();
        //            dtCumulative1.tpl_en_no = (c.tpl_en_no != null) ? c.tpl_en_no : null;
        //            dtCumulative1.tpl_lab_date = c.tpl_lab_date;
        //            dtCumulative1.mhs_id = (c.mhs_id != null) ? c.mhs_id : null;
        //            dtCumulative1.mhs_ename = (c.mhs_ename != null) ? c.mhs_ename : null;
        //            dtCumulative1.tpl_lab_no = (c.tpl_lab_no != null) ? c.tpl_lab_no : null;
        //            dtCumulative1.tpl_lab_name = (c.tpl_lab_name != null) ? c.tpl_lab_name : null;
        //            dtCumulative1.tpl_lab_value = (c.tpl_lab_value != null) ? c.tpl_lab_value : null;
        //            dtCumulative1.tpl_lab_range = (c.tpl_lab_range != null) ? c.tpl_lab_range : null;
        //            dtCumulative1.tpl_lab_unit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit : null;
        //            dtCumulative1.mlp_tname = (c.mlp_tname != null) ? c.mlp_tname : null;
        //            dtCumulative1.mlp_ename = (c.mlp_ename != null) ? c.mlp_ename : null;
        //            dtCumulative1.vLab_Result = (c.vLab_Result != null) ? c.vLab_Result : null;
        //            dtCumulative1.mlp_summary = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null;
        //            iDataCumulative1.Add(dtCumulative1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private static void foreach_objCumulative2(List<pw_Get_PatientBook_LabCumulativeResult> objCumulative2)
        //{
        //    try
        //    {
        //        foreach (var c in objCumulative2)
        //        {
        //            pw_Get_PatientBook_LabCumulativeResult
        //            dtCumulative2 = new pw_Get_PatientBook_LabCumulativeResult();
        //            dtCumulative2.tpl_en_no = (c.tpl_en_no != null) ? c.tpl_en_no : null;
        //            dtCumulative2.tpl_lab_date = c.tpl_lab_date;
        //            dtCumulative2.mhs_id = (c.mhs_id != null) ? c.mhs_id : null;
        //            dtCumulative2.mhs_ename = (c.mhs_ename != null) ? c.mhs_ename : null;
        //            dtCumulative2.tpl_lab_no = (c.tpl_lab_no != null) ? c.tpl_lab_no : null;
        //            dtCumulative2.tpl_lab_name = (c.tpl_lab_name != null) ? c.tpl_lab_name : null;
        //            dtCumulative2.tpl_lab_value = (c.tpl_lab_value != null) ? c.tpl_lab_value : null;
        //            dtCumulative2.tpl_lab_range = (c.tpl_lab_range != null) ? c.tpl_lab_range : null;
        //            dtCumulative2.tpl_lab_unit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit : null;
        //            dtCumulative2.mlp_tname = (c.mlp_tname != null) ? c.mlp_tname : null;
        //            dtCumulative2.mlp_ename = (c.mlp_ename != null) ? c.mlp_ename : null;
        //            dtCumulative2.vLab_Result = (c.vLab_Result != null) ? c.vLab_Result : null;
        //            dtCumulative2.mlp_summary = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null;
        //            iDataCumulative2.Add(dtCumulative2);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private static void foreach_objCumulative3(List<pw_Get_PatientBook_LabCumulativeResult> objCumulative3)
        //{
        //    try
        //    {
        //        foreach (var c in objCumulative3)
        //        {
        //            pw_Get_PatientBook_LabCumulativeResult
        //            dtCumulative3 = new pw_Get_PatientBook_LabCumulativeResult();
        //            dtCumulative3.tpl_en_no = (c.tpl_en_no != null) ? c.tpl_en_no : null;
        //            dtCumulative3.tpl_lab_date = c.tpl_lab_date;
        //            dtCumulative3.mhs_id = (c.mhs_id != null) ? c.mhs_id : null;
        //            dtCumulative3.mhs_ename = (c.mhs_ename != null) ? c.mhs_ename : null;
        //            dtCumulative3.tpl_lab_no = (c.tpl_lab_no != null) ? c.tpl_lab_no : null;
        //            dtCumulative3.tpl_lab_name = (c.tpl_lab_name != null) ? c.tpl_lab_name : null;
        //            dtCumulative3.tpl_lab_value = (c.tpl_lab_value != null) ? c.tpl_lab_value : null;
        //            dtCumulative3.tpl_lab_range = (c.tpl_lab_range != null) ? c.tpl_lab_range : null;
        //            dtCumulative3.tpl_lab_unit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit : null;
        //            dtCumulative3.mlp_tname = (c.mlp_tname != null) ? c.mlp_tname : null;
        //            dtCumulative3.mlp_ename = (c.mlp_ename != null) ? c.mlp_ename : null;
        //            dtCumulative3.vLab_Result = (c.vLab_Result != null) ? c.vLab_Result : null;
        //            dtCumulative3.mlp_summary = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null;
        //            iDataCumulative3.Add(dtCumulative3);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private static void foreach_objCumulative4(List<pw_Get_PatientBook_LabCumulativeResult> objCumulative4)
        //{
        //    try
        //    {
        //        foreach (var c in objCumulative4)
        //        {
        //            pw_Get_PatientBook_LabCumulativeResult
        //            dtCumulative4 = new pw_Get_PatientBook_LabCumulativeResult();
        //            dtCumulative4.tpl_en_no = (c.tpl_en_no != null) ? c.tpl_en_no : null;
        //            dtCumulative4.tpl_lab_date = c.tpl_lab_date;
        //            dtCumulative4.mhs_id = (c.mhs_id != null) ? c.mhs_id : null;
        //            dtCumulative4.mhs_ename = (c.mhs_ename != null) ? c.mhs_ename : null;
        //            dtCumulative4.tpl_lab_no = (c.tpl_lab_no != null) ? c.tpl_lab_no : null;
        //            dtCumulative4.tpl_lab_name = (c.tpl_lab_name != null) ? c.tpl_lab_name : null;
        //            dtCumulative4.tpl_lab_value = (c.tpl_lab_value != null) ? c.tpl_lab_value : null;
        //            dtCumulative4.tpl_lab_range = (c.tpl_lab_range != null) ? c.tpl_lab_range : null;
        //            dtCumulative4.tpl_lab_unit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit : null;
        //            dtCumulative4.mlp_tname = (c.mlp_tname != null) ? c.mlp_tname : null;
        //            dtCumulative4.mlp_ename = (c.mlp_ename != null) ? c.mlp_ename : null;
        //            dtCumulative4.vLab_Result = (c.vLab_Result != null) ? c.vLab_Result : null;
        //            dtCumulative4.mlp_summary = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null;
        //            iDataCumulative4.Add(dtCumulative4);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private static void foreach_objCumulative5(List<pw_Get_PatientBook_LabCumulativeResult> objCumulative5)
        //{
        //    try
        //    {
        //        foreach (var c in objCumulative5)
        //        {
        //            pw_Get_PatientBook_LabCumulativeResult
        //            dtCumulative5 = new pw_Get_PatientBook_LabCumulativeResult();
        //            dtCumulative5.tpl_en_no = (c.tpl_en_no != null) ? c.tpl_en_no : null;
        //            dtCumulative5.tpl_lab_date = c.tpl_lab_date;
        //            dtCumulative5.mhs_id = (c.mhs_id != null) ? c.mhs_id : null;
        //            dtCumulative5.mhs_ename = (c.mhs_ename != null) ? c.mhs_ename : null;
        //            dtCumulative5.tpl_lab_no = (c.tpl_lab_no != null) ? c.tpl_lab_no : null;
        //            dtCumulative5.tpl_lab_name = (c.tpl_lab_name != null) ? c.tpl_lab_name : null;
        //            dtCumulative5.tpl_lab_value = (c.tpl_lab_value != null) ? c.tpl_lab_value : null;
        //            dtCumulative5.tpl_lab_range = (c.tpl_lab_range != null) ? c.tpl_lab_range : null;
        //            dtCumulative5.tpl_lab_unit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit : null;
        //            dtCumulative5.mlp_tname = (c.mlp_tname != null) ? c.mlp_tname : null;
        //            dtCumulative5.mlp_ename = (c.mlp_ename != null) ? c.mlp_ename : null;
        //            dtCumulative5.vLab_Result = (c.vLab_Result != null) ? c.vLab_Result : null;
        //            dtCumulative5.mlp_summary = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null;
        //            iDataCumulative5.Add(dtCumulative5);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private static void foreach_objCumulative6(List<pw_Get_PatientBook_LabCumulativeResult> objCumulative6)
        //{
        //    try
        //    {
        //        foreach (var c in objCumulative6)
        //        {
        //            pw_Get_PatientBook_LabCumulativeResult
        //            dtCumulative6 = new pw_Get_PatientBook_LabCumulativeResult();
        //            dtCumulative6.tpl_en_no = (c.tpl_en_no != null) ? c.tpl_en_no : null;
        //            dtCumulative6.tpl_lab_date = c.tpl_lab_date;
        //            dtCumulative6.mhs_id = (c.mhs_id != null) ? c.mhs_id : null;
        //            dtCumulative6.mhs_ename = (c.mhs_ename != null) ? c.mhs_ename : null;
        //            dtCumulative6.tpl_lab_no = (c.tpl_lab_no != null) ? c.tpl_lab_no : null;
        //            dtCumulative6.tpl_lab_name = (c.tpl_lab_name != null) ? c.tpl_lab_name : null;
        //            dtCumulative6.tpl_lab_value = (c.tpl_lab_value != null) ? c.tpl_lab_value : null;
        //            dtCumulative6.tpl_lab_range = (c.tpl_lab_range != null) ? c.tpl_lab_range : null;
        //            dtCumulative6.tpl_lab_unit = (c.tpl_lab_unit != null) ? c.tpl_lab_unit : null;
        //            dtCumulative6.mlp_tname = (c.mlp_tname != null) ? c.mlp_tname : null;
        //            dtCumulative6.mlp_ename = (c.mlp_ename != null) ? c.mlp_ename : null;
        //            dtCumulative6.vLab_Result = (c.vLab_Result != null) ? c.vLab_Result : null;
        //            dtCumulative6.mlp_summary = (c.mlp_summary != null) ? c.mlp_summary.ToString() : null;
        //            iDataCumulative6.Add(dtCumulative6);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    try
        //    {
        //        clearData();

        //        if ((gvLabCumulative1.RowCount > 0) && (gvLabCumulative1 != null))
        //        {
        //            gvLabCumulative1.Rows[0].Cells["cLabCumu1_TestItem"].Selected = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    try
        //    {
        //        if ((gvLabCumulative2.RowCount > 0) && (gvLabCumulative2 != null))
        //        {
        //            gvLabCumulative2.Rows[0].Cells["cLabCumu2_TestItem"].Selected = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    try
        //    {
        //        if ((gvLabCumulative3.RowCount > 0) && (gvLabCumulative3 != null))
        //        {
        //            gvLabCumulative3.Rows[0].Cells["cLabCumu3_TestItem"].Selected = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative4_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    try
        //    {
        //        if ((gvLabCumulative4.RowCount > 0) && (gvLabCumulative4 != null))
        //        {
        //            gvLabCumulative4.Rows[0].Cells["cLabCumu4_TestItem"].Selected = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative5_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    try
        //    {
        //        if ((gvLabCumulative5.RowCount > 0) && (gvLabCumulative5 != null))
        //        {
        //            gvLabCumulative5.Rows[0].Cells["cLabCumu5_TestItem"].Selected = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative6_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    try
        //    {
        //        if ((gvLabCumulative6.RowCount > 0) && (gvLabCumulative6 != null))
        //        {
        //            gvLabCumulative6.Rows[0].Cells["cLabCumu6_TestItem"].Selected = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvSelectEN_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    try
        //    {
        //        if ((gvSelectEN.RowCount > 0) && (gvSelectEN != null))
        //        {
        //            DateTime Row1date = Convert.ToDateTime(gvSelectEN.Rows[0].Cells["cENdate"].Value).Date;
        //            DateTime DateNow = DateTime.Now.Date;
        //            if (Row1date.Equals(DateNow))
        //            {
        //                gvSelectEN.Rows[0].Cells["cEN"].Selected = true;
        //                gvSelectEN.CurrentRow.Cells["cSelect"].Value = 1;
        //            }
        //            else
        //            {
        //                gvSelectEN.Rows[0].Cells["cEN"].Selected = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        

        //#region ViewPanel
        
        //private void lblView1_Click(object sender, EventArgs e)
        //{
        //    Panel1.Height = 490;
        //    Panel2.Height = 25;
        //    Panel3.Height = 25;
        //    Panel4.Height = 25;
        //    Panel5.Height = 25;
        //    Panel6.Height = 25;
        //    CurrentPanel = 1;
        //}
        //private void lblView2_Click(object sender, EventArgs e)
        //{
        //    Panel1.Height = 25;
        //    Panel2.Height = 490;
        //    Panel3.Height = 25;
        //    Panel4.Height = 25;
        //    Panel5.Height = 25;
        //    Panel6.Height = 25;
        //    CurrentPanel = 2;
        //}
        //private void lblView3_Click(object sender, EventArgs e)
        //{
        //    Panel1.Height = 25;
        //    Panel2.Height = 25;
        //    Panel3.Height = 490;
        //    Panel4.Height = 25;
        //    Panel5.Height = 25;
        //    Panel6.Height = 25;
        //    CurrentPanel = 3;
        //}
        //private void lblView4_Click(object sender, EventArgs e)
        //{
        //    Panel1.Height = 25;
        //    Panel2.Height = 25;
        //    Panel3.Height = 25;
        //    Panel4.Height = 490;
        //    Panel5.Height = 25;
        //    Panel6.Height = 25;
        //    CurrentPanel = 4;
        //}
        //private void lblView5_Click(object sender, EventArgs e)
        //{
        //    Panel1.Height = 25;
        //    Panel2.Height = 25;
        //    Panel3.Height = 25;
        //    Panel4.Height = 25;
        //    Panel5.Height = 490;
        //    Panel6.Height = 25;
        //    CurrentPanel = 5;
        //}
        //private void lblView6_Click(object sender, EventArgs e)
        //{
        //    Panel1.Height = 25;
        //    Panel2.Height = 25;
        //    Panel3.Height = 25;
        //    Panel4.Height = 25;
        //    Panel5.Height = 25;
        //    Panel6.Height = 490;
        //    CurrentPanel = 6;
        //}
        //#endregion

        //#endregion

        //#region Event Lab Control

        //private void gvSelectEN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        gvSelectEN.CurrentRow.Cells["cSelect"].ReadOnly = false;

        //        if (e.ColumnIndex == 0 && e.RowIndex > -1)
        //        {
        //            string SelectEN = gvSelectEN.CurrentRow.Cells["cEN"].Value.ToString();
        //            bool SelectValue = Convert.ToBoolean((gvSelectEN.CurrentRow.Cells["cSelect"] as DataGridViewCheckBoxCell).EditedFormattedValue);

        //            switch (SelectValue)
        //            {
        //                case true:

        //                    var AddData_SelectENs1 = objCumulative1.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Except(iDataCumulative1.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    if ((AddData_SelectENs1 != null) && (AddData_SelectENs1.Count != 0))
        //                    {
        //                        foreach_objCumulative1(objCumulative1.Where(c => c.tpl_en_no == SelectEN).ToList());
        //                        BindData_gvLabCumulative1(HN, svLocationID, svDateFrom, svDateTo);
        //                    }

        //                    var AddData_SelectENs2 = objCumulative2.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Except(iDataCumulative2.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    if ((AddData_SelectENs2 != null) && (AddData_SelectENs2.Count != 0))
        //                    {
        //                        foreach_objCumulative2(objCumulative2.Where(c => c.tpl_en_no == SelectEN).ToList());
        //                        BindData_gvLabCumulative2(HN, svLocationID, svDateFrom, svDateTo);
        //                    }

        //                    var AddData_SelectENs3 = objCumulative3.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Except(iDataCumulative3.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    if ((AddData_SelectENs3 != null) && (AddData_SelectENs3.Count != 0))
        //                    {
        //                        foreach_objCumulative3(objCumulative3.Where(c => c.tpl_en_no == SelectEN).ToList());
        //                        BindData_gvLabCumulative3(HN, svLocationID, svDateFrom, svDateTo);
        //                    }

        //                    var AddData_SelectENs4 = objCumulative4.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Except(iDataCumulative4.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    if ((AddData_SelectENs4 != null) && (AddData_SelectENs4.Count != 0))
        //                    {
        //                        foreach_objCumulative4(objCumulative4.Where(c => c.tpl_en_no == SelectEN).ToList());
        //                        BindData_gvLabCumulative4(HN, svLocationID, svDateFrom, svDateTo);
        //                    }

        //                    var AddData_SelectENs5 = objCumulative5.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Except(iDataCumulative5.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    if ((AddData_SelectENs5 != null) && (AddData_SelectENs5.Count != 0))
        //                    {
        //                        foreach_objCumulative5(objCumulative5.Where(c => c.tpl_en_no == SelectEN).ToList());
        //                        BindData_gvLabCumulative5(HN, svLocationID, svDateFrom, svDateTo);
        //                    }

        //                    var AddData_SelectENs6 = objCumulative6.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Except(iDataCumulative6.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    if ((AddData_SelectENs6 != null) && (AddData_SelectENs6.Count != 0))
        //                    {
        //                        foreach_objCumulative6(objCumulative6.Where(c => c.tpl_en_no == SelectEN).ToList());
        //                        BindData_gvLabCumulative6(HN, svLocationID, svDateFrom, svDateTo);
        //                    }

        //                    break;

        //                case false:

        //                    var DelData_SelectENs1 = objCumulative1.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Intersect(iDataCumulative1.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    iDataCumulative1.RemoveAll(d => d.tpl_en_no == SelectEN);
        //                    BindData_gvLabCumulative1(HN, svLocationID, svDateFrom, svDateTo);

        //                    var DelData_SelectENs2 = objCumulative2.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Intersect(iDataCumulative2.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    iDataCumulative2.RemoveAll(d => d.tpl_en_no == SelectEN);
        //                    BindData_gvLabCumulative2(HN, svLocationID, svDateFrom, svDateTo);

        //                    var DelData_SelectENs3 = objCumulative3.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Intersect(iDataCumulative3.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    iDataCumulative3.RemoveAll(d => d.tpl_en_no == SelectEN);
        //                    BindData_gvLabCumulative3(HN, svLocationID, svDateFrom, svDateTo);

        //                    var DelData_SelectENs4 = objCumulative4.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Intersect(iDataCumulative4.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    iDataCumulative4.RemoveAll(d => d.tpl_en_no == SelectEN);
        //                    BindData_gvLabCumulative4(HN, svLocationID, svDateFrom, svDateTo);

        //                    var DelData_SelectENs5 = objCumulative5.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Intersect(iDataCumulative5.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    iDataCumulative5.RemoveAll(d => d.tpl_en_no == SelectEN);
        //                    BindData_gvLabCumulative5(HN, svLocationID, svDateFrom, svDateTo);

        //                    var DelData_SelectENs6 = objCumulative6.Where(c => c.tpl_en_no == SelectEN).Select(s => new { s.tpl_en_no, s.tpl_lab_no }).Intersect(iDataCumulative6.Where(c => c.tpl_en_no != null).Select(s => new { s.tpl_en_no, s.tpl_lab_no })).ToList();

        //                    iDataCumulative6.RemoveAll(d => d.tpl_en_no == SelectEN);
        //                    BindData_gvLabCumulative6(HN, svLocationID, svDateFrom, svDateTo);

        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        clearData();
        //        int cMainData_BkLab = (gvLabCumulative1.RowCount != 0) ? gvLabCumulative1.RowCount : 0;

        //        if (cMainData_BkLab != 0)
        //        {
        //            var sEN = gvLabCumulative1.CurrentRow.Cells["cLabCumu1_EN"].Value;

        //            if (sEN != null && objBkLab1 != null)
        //            {
        //                string ENcS = sEN.ToString();
        //                string labNo = gvLabCumulative1.CurrentRow.Cells["cLabCumu1_LabCode"].Value.ToString();

        //                var sBookSuppressRemark = objBkLab1
        //                                            .Where(w => 
        //                                                    w.tkh_id == RefBookID_tkh_id && 
        //                                                    w.tklb_en_no == ENcS && 
        //                                                    w.tklb_lab_no == labNo
        //                                                    )
        //                                                    .Select((item, index) => new 
        //                                                    {
        //                                                        labSuppress = (item.tklb_suppress == 'Y') ? ckSaveSuppressLab.Checked = true : ckSaveSuppressLab.Checked = false,
        //                                                        labRemark = (item.tklb_remark != null) ? txtLabRemark.Text = item.tklb_remark : null
        //                                                    }
        //                                                    ).ToList();
        //            }

        //            string ABvalue = string.IsNullOrEmpty(gvLabCumulative1.CurrentRow.Cells["cLabCumu1_LabABNormal"].Value.ToString()) ? "" : gvLabCumulative1.CurrentRow.Cells["cLabCumu1_LabABNormal"].Value.ToString();
        //            switch (ABvalue)
        //            {
        //                case "A": rdAbNormal.Checked = true; break;
        //                case "N": rdNormal.Checked = true; break;
        //                case "": rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //                case null: rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        clearData();
        //        int cMainData_BkLab = (gvLabCumulative2.RowCount != 0) ? gvLabCumulative2.RowCount : 0;

        //        if (cMainData_BkLab != 0)
        //        {
        //            var sEN = gvLabCumulative2.CurrentRow.Cells["cLabCumu2_EN"].Value;

        //            if (sEN != null && objBkLab2 != null)
        //            {
        //                string ENcS = sEN.ToString();
        //                string labNo = gvLabCumulative2.CurrentRow.Cells["cLabCumu2_LabCode"].Value.ToString();

        //                var sBookSuppressRemark = objBkLab2
        //                                            .Where(w =>
        //                                                    w.tkh_id == RefBookID_tkh_id &&
        //                                                    w.tklb_en_no == ENcS &&
        //                                                    w.tklb_lab_no == labNo
        //                                                    )
        //                                                    .Select((item, index) => new
        //                                                    {
        //                                                        labSuppress = (item.tklb_suppress == 'Y') ? ckSaveSuppressLab.Checked = true : ckSaveSuppressLab.Checked = false,
        //                                                        labRemark = (item.tklb_remark != null) ? txtLabRemark.Text = item.tklb_remark : null
        //                                                    }
        //                                                    ).ToList();
        //            }

        //            string ABvalue = string.IsNullOrEmpty(gvLabCumulative2.CurrentRow.Cells["cLabCumu2_LabABNormal"].Value.ToString()) ? "" : gvLabCumulative2.CurrentRow.Cells["cLabCumu2_LabABNormal"].Value.ToString();
        //            switch (ABvalue)
        //            {
        //                case "A": rdAbNormal.Checked = true; break;
        //                case "N": rdNormal.Checked = true; break;
        //                case "": rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //                case null: rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative3_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        clearData();
        //        int cMainData_BkLab = (gvLabCumulative3.RowCount != 0) ? gvLabCumulative3.RowCount : 0;

        //        if (cMainData_BkLab != 0)
        //        {
        //            var sEN = gvLabCumulative3.CurrentRow.Cells["cLabCumu3_EN"].Value;

        //            if (sEN != null && objBkLab3 != null)
        //            {
        //                string ENcS = sEN.ToString();
        //                string labNo = gvLabCumulative3.CurrentRow.Cells["cLabCumu3_LabCode"].Value.ToString();

        //                var sBookSuppressRemark = objBkLab3
        //                                            .Where(w =>
        //                                                    w.tkh_id == RefBookID_tkh_id &&
        //                                                    w.tklb_en_no == ENcS &&
        //                                                    w.tklb_lab_no == labNo
        //                                                    )
        //                                                    .Select((item, index) => new
        //                                                    {
        //                                                        labSuppress = (item.tklb_suppress == 'Y') ? ckSaveSuppressLab.Checked = true : ckSaveSuppressLab.Checked = false,
        //                                                        labRemark = (item.tklb_remark != null) ? txtLabRemark.Text = item.tklb_remark : null
        //                                                    }
        //                                                    ).ToList();
        //            }

        //            string ABvalue = string.IsNullOrEmpty(gvLabCumulative3.CurrentRow.Cells["cLabCumu3_LabABNormal"].Value.ToString()) ? "" : gvLabCumulative3.CurrentRow.Cells["cLabCumu3_LabABNormal"].Value.ToString();
        //            switch (ABvalue)
        //            {
        //                case "A": rdAbNormal.Checked = true; break;
        //                case "N": rdNormal.Checked = true; break;
        //                case "": rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //                case null: rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative4_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        clearData();
        //        int cMainData_BkLab = (gvLabCumulative4.RowCount != 0) ? gvLabCumulative4.RowCount : 0;

        //        if (cMainData_BkLab != 0)
        //        {
        //            var sEN = gvLabCumulative4.CurrentRow.Cells["cLabCumu4_EN"].Value;

        //            if (sEN != null && objBkLab4 != null)
        //            {
        //                string ENcS = sEN.ToString();
        //                string labNo = gvLabCumulative4.CurrentRow.Cells["cLabCumu4_LabCode"].Value.ToString();

        //                var sBookSuppressRemark = objBkLab4
        //                                            .Where(w =>
        //                                                    w.tkh_id == RefBookID_tkh_id &&
        //                                                    w.tklb_en_no == ENcS &&
        //                                                    w.tklb_lab_no == labNo
        //                                                    )
        //                                                    .Select((item, index) => new
        //                                                    {
        //                                                        labSuppress = (item.tklb_suppress == 'Y') ? ckSaveSuppressLab.Checked = true : ckSaveSuppressLab.Checked = false,
        //                                                        labRemark = (item.tklb_remark != null) ? txtLabRemark.Text = item.tklb_remark : null
        //                                                    }
        //                                                    ).ToList();
        //            }

        //            string ABvalue = string.IsNullOrEmpty(gvLabCumulative4.CurrentRow.Cells["cLabCumu4_LabABNormal"].Value.ToString()) ? "" : gvLabCumulative4.CurrentRow.Cells["cLabCumu4_LabABNormal"].Value.ToString();
        //            switch (ABvalue)
        //            {
        //                case "A": rdAbNormal.Checked = true; break;
        //                case "N": rdNormal.Checked = true; break;
        //                case "": rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //                case null: rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative5_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        clearData();
        //        int cMainData_BkLab = (gvLabCumulative5.RowCount != 0) ? gvLabCumulative5.RowCount : 0;

        //        if (cMainData_BkLab != 0)
        //        {
        //            var sEN = gvLabCumulative5.CurrentRow.Cells["cLabCumu5_EN"].Value;

        //            if (sEN != null && objBkLab5 != null)
        //            {
        //                string ENcS = sEN.ToString();
        //                string labNo = gvLabCumulative5.CurrentRow.Cells["cLabCumu5_LabCode"].Value.ToString();

        //                var sBookSuppressRemark = objBkLab5
        //                                            .Where(w =>
        //                                                    w.tkh_id == RefBookID_tkh_id &&
        //                                                    w.tklb_en_no == ENcS &&
        //                                                    w.tklb_lab_no == labNo
        //                                                    )
        //                                                    .Select((item, index) => new
        //                                                    {
        //                                                        labSuppress = (item.tklb_suppress == 'Y') ? ckSaveSuppressLab.Checked = true : ckSaveSuppressLab.Checked = false,
        //                                                        labRemark = (item.tklb_remark != null) ? txtLabRemark.Text = item.tklb_remark : null
        //                                                    }
        //                                                    ).ToList();
        //            }

        //            string ABvalue = string.IsNullOrEmpty(gvLabCumulative5.CurrentRow.Cells["cLabCumu5_LabABNormal"].Value.ToString()) ? "" : gvLabCumulative5.CurrentRow.Cells["cLabCumu5_LabABNormal"].Value.ToString();
        //            switch (ABvalue)
        //            {
        //                case "A": rdAbNormal.Checked = true; break;
        //                case "N": rdNormal.Checked = true; break;
        //                case "": rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //                case null: rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void gvLabCumulative6_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        clearData();
        //        int cMainData_BkLab = (gvLabCumulative6.RowCount != 0) ? gvLabCumulative6.RowCount : 0;

        //        if (cMainData_BkLab != 0)
        //        {
        //            //string sEN = string.IsNullOrEmpty(gvLabCumulative6.CurrentRow.Cells["cLabCumu6_EN"].Value.ToString()) ? "" : gvLabCumulative6.CurrentRow.Cells["cLabCumu6_EN"].Value.ToString();
        //            var sEN = gvLabCumulative6.CurrentRow.Cells["cLabCumu6_EN"].Value;

        //            if (sEN != null && objBkLab6 != null)
        //            {
        //                string ENcS = sEN.ToString();
        //                string labNo = gvLabCumulative6.CurrentRow.Cells["cLabCumu6_LabCode"].Value.ToString();

        //                var sBookSuppressRemark = objBkLab6
        //                                            .Where(w =>
        //                                                    w.tkh_id == RefBookID_tkh_id &&
        //                                                    w.tklb_en_no == ENcS &&
        //                                                    w.tklb_lab_no == labNo
        //                                                    )
        //                                                    .Select((item, index) => new
        //                                                    {
        //                                                        labSuppress = (item.tklb_suppress == 'Y') ? ckSaveSuppressLab.Checked = true : ckSaveSuppressLab.Checked = false,
        //                                                        labRemark = (item.tklb_remark != null) ? txtLabRemark.Text = item.tklb_remark : null
        //                                                    }
        //                                                    ).ToList();
        //            }

        //            string ABvalue = string.IsNullOrEmpty(gvLabCumulative6.CurrentRow.Cells["cLabCumu6_LabABNormal"].Value.ToString()) ? "" : gvLabCumulative6.CurrentRow.Cells["cLabCumu6_LabABNormal"].Value.ToString();
        //            switch (ABvalue)
        //            {
        //                case "A": rdAbNormal.Checked = true; break;
        //                case "N": rdNormal.Checked = true; break;
        //                case "": rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //                case null: rdAbNormal.Checked = false; rdNormal.Checked = false; break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void labsUpdateAll_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (RefBookID_tkh_id != null)
        //        {
        //            //iDataCumulative
        //            var updData1 = iDataCumulative1.Where(w => w.tpl_en_no != null && w.tpl_lab_no != null).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).ToList();
        //            var updData2 = iDataCumulative2.Where(w => w.tpl_en_no != null && w.tpl_lab_no != null).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).ToList();
        //            var updData3 = iDataCumulative3.Where(w => w.tpl_en_no != null && w.tpl_lab_no != null).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).ToList();
        //            var updData4 = iDataCumulative4.Where(w => w.tpl_en_no != null && w.tpl_lab_no != null).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).ToList();
        //            var updData5 = iDataCumulative5.Where(w => w.tpl_en_no != null && w.tpl_lab_no != null).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).ToList();
        //            var updData6 = iDataCumulative6.Where(w => w.tpl_en_no != null && w.tpl_lab_no != null).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).ToList();

        //            var iDataCumulative = updData1.Union(updData2);
        //                iDataCumulative = iDataCumulative.Union(updData3);
        //                iDataCumulative = iDataCumulative.Union(updData4);
        //                iDataCumulative = iDataCumulative.Union(updData5);
        //                iDataCumulative = iDataCumulative.Union(updData6);

        //            var tbDel = dbc.trn_book_labs.Where(d => d.tkh_id == RefBookID_tkh_id).ToList();

        //            if ((tbDel.Count > 0) && (tbDel != null))
        //            {
        //                dbc.trn_book_labs.DeleteAllOnSubmit(tbDel);
        //                dbc.SubmitChanges();
        //                tbDel.Clear();
        //            }

        //            var objBkLab = objBkLab1.Union(objBkLab2);
        //                objBkLab = objBkLab.Union(objBkLab3);
        //                objBkLab = objBkLab.Union(objBkLab4);
        //                objBkLab = objBkLab.Union(objBkLab5);
        //                objBkLab = objBkLab.Union(objBkLab6);

        //            List<trn_book_lab> updBooklabs = new List<trn_book_lab>();
        //            foreach (var item in iDataCumulative)
        //            {
        //                trn_book_lab upd = new trn_book_lab();
        //                upd.tkh_id = RefBookID_tkh_id;
        //                upd.tklb_en_no = item.tpl_en_no;
        //                upd.tklb_lab_no = item.tpl_lab_no;
        //                upd.tklb_lab_name = item.tpl_lab_name;
        //                upd.tklb_lab_date = (item.tpl_lab_date != null) ? item.tpl_lab_date : null;
        //                upd.tklb_lab_value = (item.tpl_lab_value != null) ? item.tpl_lab_value : null;
        //                upd.tklb_lab_unit = (item.tpl_lab_unit != null) ? item.tpl_lab_unit : null;
        //                upd.tklb_lab_range = (item.tpl_lab_range != null) ? item.tpl_lab_range : null;
        //                upd.tklb_lab_result_thai = (item.mlp_tname != null) ? item.mlp_tname : null;
        //                upd.tklb_lab_result_eng = (item.mlp_ename != null) ? item.mlp_ename : null;
        //                upd.tklb_summary = (item.mlp_summary != null) ? Convert.ToChar(item.mlp_summary) : ' ';
        //                upd.tklb_suppress = objBkLab.Where(w => w.tklb_en_no != null && w.tklb_lab_no == item.tpl_lab_no).Select(s => s.tklb_suppress).FirstOrDefault(); //(item.sp_suppress != null) ? Convert.ToChar(item.sp_suppress) : 'N';
        //                upd.tklb_remark = objBkLab.Where(w => w.tklb_en_no != null && w.tklb_lab_no == item.tpl_lab_no).Select(s => s.tklb_remark).FirstOrDefault(); //item.sp_remark;
        //                upd.tklb_create_by = Program.CurrentUser.mut_username;
        //                upd.tklb_update_by = Program.CurrentUser.mut_username;
        //                updBooklabs.Add(upd);
        //            }

        //            if (updBooklabs != null)
        //            {
        //                CheckupDataContext 
        //                dbcUpdBookLabs = new CheckupDataContext(Program.Connectionstring);
        //                dbcUpdBookLabs.Connection.Open();
        //                dbcUpdBookLabs.trn_book_labs.InsertAllOnSubmit(updBooklabs);
        //                dbcUpdBookLabs.SubmitChanges();
        //                dbcUpdBookLabs.Connection.Close();

        //                //MessageBox.Show("Ins into trn_book_labs : tkh_id => " + RefBookID_tkh_id);
        //                MessageBox.Show("tkh_id => (" + RefBookID_tkh_id + ")" + ": Update data into trn_book_lab is Completed.");
        //            }

        //            var InsCkReportHIV = (ckReportHIV.Checked != true) ? 'N' : 'Y';
        //            var InsCkShowDefaultChart = (ckShowDefaultChart.Checked != true) ? 'N' : 'Y';
        //            var InsCkSuppressLab = (ckSuppressLab.Checked != true) ? 'N' : 'Y';

        //            CheckupDataContext 
        //            dbcUpdBookHdrs = new CheckupDataContext(Program.Connectionstring);
        //            dbcUpdBookHdrs.Connection.Open();
        //            trn_book_hdr InsCk = new trn_book_hdr();
        //            InsCk = dbcUpdBookHdrs.trn_book_hdrs.Where(i => i.tpr_id == ptpr_id).FirstOrDefault();
        //            InsCk.tkh_report_HIV = InsCkReportHIV;
        //            InsCk.tkh_show_chart = InsCkShowDefaultChart;
        //            InsCk.tkh_suppress_lab = InsCkSuppressLab;
        //            InsCk.tkh_update_by = Program.CurrentUser.mut_username;
        //            InsCk.tkh_update_date = Program.GetServerDateTime();
        //            dbcUpdBookHdrs.SubmitChanges();
        //            dbcUpdBookHdrs.Connection.Close();
        //        }
        //        else
        //        {
        //            MessageBox.Show("debug => tkh_id is not null.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void ckSaveSuppressLab_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        switch (CurrentPanel)
        //        {
        //            case 1:
        //                if (!string.IsNullOrEmpty(gvLabCumulative1.CurrentRow.Cells["cLabCumu1_LabCode"].Value.ToString()))
        //                {
        //                    if (gvLabCumulative1.RowCount > 0)
        //                    {
        //                        string ValueckSaveSuppressLab1 = gvLabCumulative1.CurrentRow.Cells["cLabCumu1_LabCode"].Value.ToString();
        //                        var updValueSelectSuppressLab1 = objBkLab1.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab1).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                        var item1 = iDataCumulative1.Where(w => w.tpl_en_no != null && w.tpl_lab_no == ValueckSaveSuppressLab1).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                        if (updValueSelectSuppressLab1 == null)
        //                        {
        //                            trn_book_lab upd1 = new trn_book_lab();
        //                            upd1.tkh_id = RefBookID_tkh_id;
        //                            upd1.tklb_en_no = item1.Select(s => s.tpl_en_no).FirstOrDefault();
        //                            upd1.tklb_lab_no = item1.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                            upd1.tklb_lab_name = item1.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                            upd1.tklb_lab_date = (item1.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                            upd1.tklb_lab_value = (item1.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                            upd1.tklb_lab_unit = (item1.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                            upd1.tklb_lab_range = (item1.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                            upd1.tklb_lab_result_thai = (item1.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item1.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                            upd1.tklb_lab_result_eng = (item1.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item1.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                            upd1.tklb_summary = (item1.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item1.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                            upd1.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                            upd1.tklb_remark = null;
        //                            upd1.tklb_create_by = Program.CurrentUser.mut_username;
        //                            upd1.tklb_update_by = Program.CurrentUser.mut_username;
        //                            objBkLab1.Add(upd1);
        //                        }
        //                        else
        //                        {
        //                            var updValueSelectSuppressLab = objBkLab1.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab1).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                            if (ckSaveSuppressLab.Checked == true)
        //                            {
        //                                updValueSelectSuppressLab.tklb_suppress = 'Y';
        //                            }
        //                            else
        //                            {
        //                                updValueSelectSuppressLab.tklb_suppress = 'N';
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 2:
        //                if (!string.IsNullOrEmpty(gvLabCumulative2.CurrentRow.Cells["cLabCumu2_LabCode"].Value.ToString()))
        //                {
        //                    if (gvLabCumulative2.RowCount > 0)
        //                    {
        //                        string ValueckSaveSuppressLab2 = gvLabCumulative2.CurrentRow.Cells["cLabCumu2_LabCode"].Value.ToString();
        //                        var updValueSelectSuppressLab2 = objBkLab2.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab2).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                        var item2 = iDataCumulative2.Where(w => w.tpl_en_no != null && w.tpl_lab_no == ValueckSaveSuppressLab2).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                        if (updValueSelectSuppressLab2 == null)
        //                        {
        //                            trn_book_lab upd2 = new trn_book_lab();
        //                            upd2.tkh_id = RefBookID_tkh_id;
        //                            upd2.tklb_en_no = item2.Select(s => s.tpl_en_no).FirstOrDefault();
        //                            upd2.tklb_lab_no = item2.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                            upd2.tklb_lab_name = item2.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                            upd2.tklb_lab_date = (item2.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                            upd2.tklb_lab_value = (item2.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                            upd2.tklb_lab_unit = (item2.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                            upd2.tklb_lab_range = (item2.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                            upd2.tklb_lab_result_thai = (item2.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item2.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                            upd2.tklb_lab_result_eng = (item2.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item2.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                            upd2.tklb_summary = (item2.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item2.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                            upd2.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                            upd2.tklb_remark = null;
        //                            upd2.tklb_create_by = Program.CurrentUser.mut_username;
        //                            upd2.tklb_update_by = Program.CurrentUser.mut_username;
        //                            objBkLab2.Add(upd2);
        //                        }
        //                        else
        //                        {
        //                            updValueSelectSuppressLab2 = objBkLab2.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab2).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                            if (ckSaveSuppressLab.Checked == true)
        //                            {
        //                                updValueSelectSuppressLab2.tklb_suppress = 'Y';
        //                            }
        //                            else
        //                            {
        //                                updValueSelectSuppressLab2.tklb_suppress = 'N';
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 3:
        //                if (!string.IsNullOrEmpty(gvLabCumulative3.CurrentRow.Cells["cLabCumu3_LabCode"].Value.ToString()))
        //                {
        //                    if (gvLabCumulative3.RowCount > 0)
        //                    {
        //                        string ValueckSaveSuppressLab3 = gvLabCumulative3.CurrentRow.Cells["cLabCumu3_LabCode"].Value.ToString();
        //                        var updValueSelectSuppressLab3 = objBkLab3.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab3).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                        var item3 = iDataCumulative3.Where(w => w.tpl_en_no != null && w.tpl_lab_no == ValueckSaveSuppressLab3).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                        if (updValueSelectSuppressLab3 == null)
        //                        {
        //                            trn_book_lab upd3 = new trn_book_lab();
        //                            upd3.tkh_id = RefBookID_tkh_id;
        //                            upd3.tklb_en_no = item3.Select(s => s.tpl_en_no).FirstOrDefault();
        //                            upd3.tklb_lab_no = item3.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                            upd3.tklb_lab_name = item3.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                            upd3.tklb_lab_date = (item3.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                            upd3.tklb_lab_value = (item3.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                            upd3.tklb_lab_unit = (item3.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                            upd3.tklb_lab_range = (item3.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                            upd3.tklb_lab_result_thai = (item3.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item3.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                            upd3.tklb_lab_result_eng = (item3.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item3.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                            upd3.tklb_summary = (item3.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item3.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                            upd3.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                            upd3.tklb_remark = null;
        //                            upd3.tklb_create_by = Program.CurrentUser.mut_username;
        //                            upd3.tklb_update_by = Program.CurrentUser.mut_username;
        //                            objBkLab3.Add(upd3);
        //                        }
        //                        else
        //                        {
        //                            updValueSelectSuppressLab3 = objBkLab3.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab3).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                            if (ckSaveSuppressLab.Checked == true)
        //                            {
        //                                updValueSelectSuppressLab3.tklb_suppress = 'Y';
        //                            }
        //                            else
        //                            {
        //                                updValueSelectSuppressLab3.tklb_suppress = 'N';
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 4:
        //                if (!string.IsNullOrEmpty(gvLabCumulative4.CurrentRow.Cells["cLabCumu4_LabCode"].Value.ToString()))
        //                {
        //                    if (gvLabCumulative4.RowCount > 0)
        //                    {
        //                        string ValueckSaveSuppressLab4 = gvLabCumulative4.CurrentRow.Cells["cLabCumu4_LabCode"].Value.ToString();
        //                        var updValueSelectSuppressLab4 = objBkLab4.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab4).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                        var item4 = iDataCumulative4.Where(w => w.tpl_en_no != null && w.tpl_lab_no == ValueckSaveSuppressLab4).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                        if (updValueSelectSuppressLab4 == null)
        //                        {
        //                            trn_book_lab upd4 = new trn_book_lab();
        //                            upd4.tkh_id = RefBookID_tkh_id;
        //                            upd4.tklb_en_no = item4.Select(s => s.tpl_en_no).FirstOrDefault();
        //                            upd4.tklb_lab_no = item4.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                            upd4.tklb_lab_name = item4.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                            upd4.tklb_lab_date = (item4.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                            upd4.tklb_lab_value = (item4.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                            upd4.tklb_lab_unit = (item4.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                            upd4.tklb_lab_range = (item4.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                            upd4.tklb_lab_result_thai = (item4.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item4.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                            upd4.tklb_lab_result_eng = (item4.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item4.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                            upd4.tklb_summary = (item4.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item4.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                            upd4.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                            upd4.tklb_remark = null;
        //                            upd4.tklb_create_by = Program.CurrentUser.mut_username;
        //                            upd4.tklb_update_by = Program.CurrentUser.mut_username;
        //                            objBkLab4.Add(upd4);
        //                        }
        //                        else
        //                        {
        //                            updValueSelectSuppressLab4 = objBkLab4.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab4).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                            if (ckSaveSuppressLab.Checked == true)
        //                            {
        //                                updValueSelectSuppressLab4.tklb_suppress = 'Y';
        //                            }
        //                            else
        //                            {
        //                                updValueSelectSuppressLab4.tklb_suppress = 'N';
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 5:
        //                if (!string.IsNullOrEmpty(gvLabCumulative5.CurrentRow.Cells["cLabCumu5_LabCode"].Value.ToString()))
        //                {
        //                    if (gvLabCumulative5.RowCount > 0)
        //                    {
        //                        string ValueckSaveSuppressLab5 = gvLabCumulative5.CurrentRow.Cells["cLabCumu5_LabCode"].Value.ToString();
        //                        var updValueSelectSuppressLab5 = objBkLab5.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab5).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                        var item5 = iDataCumulative5.Where(w => w.tpl_en_no != null && w.tpl_lab_no == ValueckSaveSuppressLab5).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                        if (updValueSelectSuppressLab5 == null)
        //                        {
        //                            trn_book_lab upd5 = new trn_book_lab();
        //                            upd5.tkh_id = RefBookID_tkh_id;
        //                            upd5.tklb_en_no = item5.Select(s => s.tpl_en_no).FirstOrDefault();
        //                            upd5.tklb_lab_no = item5.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                            upd5.tklb_lab_name = item5.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                            upd5.tklb_lab_date = (item5.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                            upd5.tklb_lab_value = (item5.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                            upd5.tklb_lab_unit = (item5.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                            upd5.tklb_lab_range = (item5.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                            upd5.tklb_lab_result_thai = (item5.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item5.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                            upd5.tklb_lab_result_eng = (item5.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item5.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                            upd5.tklb_summary = (item5.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item5.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                            upd5.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                            upd5.tklb_remark = null;
        //                            upd5.tklb_create_by = Program.CurrentUser.mut_username;
        //                            upd5.tklb_update_by = Program.CurrentUser.mut_username;
        //                            objBkLab5.Add(upd5);
        //                        }
        //                        else
        //                        {
        //                            updValueSelectSuppressLab5 = objBkLab5.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab5).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                            if (ckSaveSuppressLab.Checked == true)
        //                            {
        //                                updValueSelectSuppressLab5.tklb_suppress = 'Y';
        //                            }
        //                            else
        //                            {
        //                                updValueSelectSuppressLab5.tklb_suppress = 'N';
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 6:
        //                if (!string.IsNullOrEmpty(gvLabCumulative6.CurrentRow.Cells["cLabCumu6_LabCode"].Value.ToString()))
        //                {
        //                    if (gvLabCumulative6.RowCount > 0)
        //                    {
        //                        string ValueckSaveSuppressLab6 = gvLabCumulative6.CurrentRow.Cells["cLabCumu6_LabCode"].Value.ToString();
        //                        var updValueSelectSuppressLab6 = objBkLab6.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab6).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                        var item6 = iDataCumulative6.Where(w => w.tpl_en_no != null && w.tpl_lab_no == ValueckSaveSuppressLab6).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                        if (updValueSelectSuppressLab6 == null)
        //                        {
        //                            trn_book_lab upd6 = new trn_book_lab();
        //                            upd6.tkh_id = RefBookID_tkh_id;
        //                            upd6.tklb_en_no = item6.Select(s => s.tpl_en_no).FirstOrDefault();
        //                            upd6.tklb_lab_no = item6.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                            upd6.tklb_lab_name = item6.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                            upd6.tklb_lab_date = (item6.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                            upd6.tklb_lab_value = (item6.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                            upd6.tklb_lab_unit = (item6.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                            upd6.tklb_lab_range = (item6.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                            upd6.tklb_lab_result_thai = (item6.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item6.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                            upd6.tklb_lab_result_eng = (item6.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item6.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                            upd6.tklb_summary = (item6.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item6.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                            upd6.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                            upd6.tklb_remark = null;
        //                            upd6.tklb_create_by = Program.CurrentUser.mut_username;
        //                            upd6.tklb_update_by = Program.CurrentUser.mut_username;
        //                            objBkLab6.Add(upd6);
        //                        }
        //                        else
        //                        {
        //                            updValueSelectSuppressLab6 = objBkLab6.Where(w => w.tklb_en_no != null && w.tklb_lab_no == ValueckSaveSuppressLab6).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                            if (ckSaveSuppressLab.Checked == true)
        //                            {
        //                                updValueSelectSuppressLab6.tklb_suppress = 'Y';
        //                            }
        //                            else
        //                            {
        //                                updValueSelectSuppressLab6.tklb_suppress = 'N';
        //                            }
        //                        }
        //                    }
        //                }
        //                break;
        //        }

        //        BindData_SuppressLab();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private void txtLabRemark_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        switch (CurrentPanel)
        //        {
        //            case 1:
        //                if (!string.IsNullOrEmpty(gvLabCumulative1.CurrentRow.Cells["cLabCumu1_LabCode"].Value.ToString()))
        //                {
        //                    if (chkEvent_txtremark == 1)
        //                    {
        //                        if (valueTextRemark != txtLabRemark.Text)
        //                        {
        //                            if (gvLabCumulative1.RowCount > 0)
        //                            {
        //                                string codelabTextRemark1 = gvLabCumulative1.CurrentRow.Cells["cLabCumu1_LabCode"].Value.ToString();
        //                                var updValueTextLabRemark1 = objBkLab1.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark1).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                var item1 = iDataCumulative1.Where(w => w.tpl_en_no != null && w.tpl_lab_no == codelabTextRemark1).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                                if (updValueTextLabRemark1 == null)
        //                                {
        //                                    trn_book_lab upd1 = new trn_book_lab();
        //                                    upd1.tkh_id = RefBookID_tkh_id;
        //                                    upd1.tklb_en_no = item1.Select(s => s.tpl_en_no).FirstOrDefault();
        //                                    upd1.tklb_lab_no = item1.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                                    upd1.tklb_lab_name = item1.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                                    upd1.tklb_lab_date = (item1.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                                    upd1.tklb_lab_value = (item1.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                                    upd1.tklb_lab_unit = (item1.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                                    upd1.tklb_lab_range = (item1.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item1.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                                    upd1.tklb_lab_result_thai = (item1.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item1.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                                    upd1.tklb_lab_result_eng = (item1.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item1.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                                    upd1.tklb_summary = (item1.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item1.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                                    upd1.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                                    upd1.tklb_remark = txtLabRemark.Text;
        //                                    upd1.tklb_create_by = Program.CurrentUser.mut_username;
        //                                    upd1.tklb_update_by = Program.CurrentUser.mut_username;
        //                                    objBkLab1.Add(upd1);
        //                                }
        //                                else
        //                                {
        //                                    updValueTextLabRemark1 = objBkLab1.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark1).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                    if (valueTextRemark != txtLabRemark.Text)
        //                                    {
        //                                        updValueTextLabRemark1.tklb_remark = txtLabRemark.Text;
        //                                        chkEvent_txtremark = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 2:
        //                if (!string.IsNullOrEmpty(gvLabCumulative2.CurrentRow.Cells["cLabCumu2_LabCode"].Value.ToString()))
        //                {
        //                    if (chkEvent_txtremark == 1)
        //                    {
        //                        if (valueTextRemark != txtLabRemark.Text)
        //                        {
        //                            if (gvLabCumulative2.RowCount > 0)
        //                            {
        //                                string codelabTextRemark2 = gvLabCumulative2.CurrentRow.Cells["cLabCumu2_LabCode"].Value.ToString();
        //                                var updValueTextLabRemark2 = objBkLab2.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark2).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                var item2 = iDataCumulative2.Where(w => w.tpl_en_no != null && w.tpl_lab_no == codelabTextRemark2).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                                if (updValueTextLabRemark2 == null)
        //                                {
        //                                    trn_book_lab upd2 = new trn_book_lab();
        //                                    upd2.tkh_id = RefBookID_tkh_id;
        //                                    upd2.tklb_en_no = item2.Select(s => s.tpl_en_no).FirstOrDefault();
        //                                    upd2.tklb_lab_no = item2.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                                    upd2.tklb_lab_name = item2.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                                    upd2.tklb_lab_date = (item2.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                                    upd2.tklb_lab_value = (item2.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                                    upd2.tklb_lab_unit = (item2.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                                    upd2.tklb_lab_range = (item2.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item2.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                                    upd2.tklb_lab_result_thai = (item2.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item2.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                                    upd2.tklb_lab_result_eng = (item2.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item2.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                                    upd2.tklb_summary = (item2.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item2.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                                    upd2.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                                    upd2.tklb_remark = txtLabRemark.Text;
        //                                    upd2.tklb_create_by = Program.CurrentUser.mut_username;
        //                                    upd2.tklb_update_by = Program.CurrentUser.mut_username;
        //                                    objBkLab2.Add(upd2);
        //                                }
        //                                else
        //                                {
        //                                    updValueTextLabRemark2 = objBkLab2.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark2).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                    if (valueTextRemark != txtLabRemark.Text)
        //                                    {
        //                                        updValueTextLabRemark2.tklb_remark = txtLabRemark.Text;
        //                                        chkEvent_txtremark = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 3:
        //                if (!string.IsNullOrEmpty(gvLabCumulative3.CurrentRow.Cells["cLabCumu3_LabCode"].Value.ToString()))
        //                {
        //                    if (chkEvent_txtremark == 1)
        //                    {
        //                        if (valueTextRemark != txtLabRemark.Text)
        //                        {
        //                            if (gvLabCumulative3.RowCount > 0)
        //                            {
        //                                string codelabTextRemark3 = gvLabCumulative3.CurrentRow.Cells["cLabCumu3_LabCode"].Value.ToString();
        //                                var updValueTextLabRemark3 = objBkLab3.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark3).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                var item3 = iDataCumulative3.Where(w => w.tpl_en_no != null && w.tpl_lab_no == codelabTextRemark3).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                                if (updValueTextLabRemark3 == null)
        //                                {
        //                                    trn_book_lab upd3 = new trn_book_lab();
        //                                    upd3.tkh_id = RefBookID_tkh_id;
        //                                    upd3.tklb_en_no = item3.Select(s => s.tpl_en_no).FirstOrDefault();
        //                                    upd3.tklb_lab_no = item3.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                                    upd3.tklb_lab_name = item3.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                                    upd3.tklb_lab_date = (item3.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                                    upd3.tklb_lab_value = (item3.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                                    upd3.tklb_lab_unit = (item3.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                                    upd3.tklb_lab_range = (item3.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item3.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                                    upd3.tklb_lab_result_thai = (item3.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item3.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                                    upd3.tklb_lab_result_eng = (item3.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item3.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                                    upd3.tklb_summary = (item3.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item3.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                                    upd3.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                                    upd3.tklb_remark = txtLabRemark.Text;
        //                                    upd3.tklb_create_by = Program.CurrentUser.mut_username;
        //                                    upd3.tklb_update_by = Program.CurrentUser.mut_username;
        //                                    objBkLab3.Add(upd3);
        //                                }
        //                                else
        //                                {
        //                                    updValueTextLabRemark3 = objBkLab3.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark3).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                    if (valueTextRemark != txtLabRemark.Text)
        //                                    {
        //                                        updValueTextLabRemark3.tklb_remark = txtLabRemark.Text;
        //                                        chkEvent_txtremark = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 4:
        //                if (!string.IsNullOrEmpty(gvLabCumulative4.CurrentRow.Cells["cLabCumu4_LabCode"].Value.ToString()))
        //                {
        //                    if (chkEvent_txtremark == 1)
        //                    {
        //                        if (valueTextRemark != txtLabRemark.Text)
        //                        {
        //                            if (gvLabCumulative4.RowCount > 0)
        //                            {
        //                                string codelabTextRemark4 = gvLabCumulative4.CurrentRow.Cells["cLabCumu4_LabCode"].Value.ToString();
        //                                var updValueTextLabRemark4 = objBkLab4.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark4).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                var item4 = iDataCumulative4.Where(w => w.tpl_en_no != null && w.tpl_lab_no == codelabTextRemark4).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                                if (updValueTextLabRemark4 == null)
        //                                {
        //                                    trn_book_lab upd4 = new trn_book_lab();
        //                                    upd4.tkh_id = RefBookID_tkh_id;
        //                                    upd4.tklb_en_no = item4.Select(s => s.tpl_en_no).FirstOrDefault();
        //                                    upd4.tklb_lab_no = item4.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                                    upd4.tklb_lab_name = item4.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                                    upd4.tklb_lab_date = (item4.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                                    upd4.tklb_lab_value = (item4.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                                    upd4.tklb_lab_unit = (item4.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                                    upd4.tklb_lab_range = (item4.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item4.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                                    upd4.tklb_lab_result_thai = (item4.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item4.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                                    upd4.tklb_lab_result_eng = (item4.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item4.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                                    upd4.tklb_summary = (item4.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item4.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                                    upd4.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                                    upd4.tklb_remark = txtLabRemark.Text;
        //                                    upd4.tklb_create_by = Program.CurrentUser.mut_username;
        //                                    upd4.tklb_update_by = Program.CurrentUser.mut_username;
        //                                    objBkLab4.Add(upd4);
        //                                }
        //                                else
        //                                {
        //                                    updValueTextLabRemark4 = objBkLab4.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark4).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                    if (valueTextRemark != txtLabRemark.Text)
        //                                    {
        //                                        updValueTextLabRemark4.tklb_remark = txtLabRemark.Text;
        //                                        chkEvent_txtremark = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 5:
        //                if (!string.IsNullOrEmpty(gvLabCumulative5.CurrentRow.Cells["cLabCumu5_LabCode"].Value.ToString()))
        //                {
        //                    if (chkEvent_txtremark == 1)
        //                    {
        //                        if (valueTextRemark != txtLabRemark.Text)
        //                        {
        //                            if (gvLabCumulative5.RowCount > 0)
        //                            {
        //                                string codelabTextRemark5 = gvLabCumulative5.CurrentRow.Cells["cLabCumu5_LabCode"].Value.ToString();
        //                                var updValueTextLabRemark5 = objBkLab5.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark5).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                var item5 = iDataCumulative5.Where(w => w.tpl_en_no != null && w.tpl_lab_no == codelabTextRemark5).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
        //                                if (updValueTextLabRemark5 == null)
        //                                {
        //                                    trn_book_lab upd5 = new trn_book_lab();
        //                                    upd5.tkh_id = RefBookID_tkh_id;
        //                                    upd5.tklb_en_no = item5.Select(s => s.tpl_en_no).FirstOrDefault();
        //                                    upd5.tklb_lab_no = item5.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                                    upd5.tklb_lab_name = item5.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                                    upd5.tklb_lab_date = (item5.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                                    upd5.tklb_lab_value = (item5.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                                    upd5.tklb_lab_unit = (item5.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                                    upd5.tklb_lab_range = (item5.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item5.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                                    upd5.tklb_lab_result_thai = (item5.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item5.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                                    upd5.tklb_lab_result_eng = (item5.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item5.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                                    upd5.tklb_summary = (item5.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item5.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                                    upd5.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                                    upd5.tklb_remark = txtLabRemark.Text;
        //                                    upd5.tklb_create_by = Program.CurrentUser.mut_username;
        //                                    upd5.tklb_update_by = Program.CurrentUser.mut_username;
        //                                    objBkLab5.Add(upd5);
        //                                }
        //                                else
        //                                {
        //                                    updValueTextLabRemark5 = objBkLab5.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark5).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                    if (valueTextRemark != txtLabRemark.Text)
        //                                    {
        //                                        updValueTextLabRemark5.tklb_remark = txtLabRemark.Text;
        //                                        chkEvent_txtremark = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;

        //            case 6:
        //                if (!string.IsNullOrEmpty(gvLabCumulative6.CurrentRow.Cells["cLabCumu6_LabCode"].Value.ToString()))
        //                {
        //                    if (chkEvent_txtremark == 1)
        //                    {
        //                        if (valueTextRemark != txtLabRemark.Text)
        //                        {
        //                            if (gvLabCumulative6.RowCount > 0)
        //                            {
        //                                string codelabTextRemark6 = gvLabCumulative6.CurrentRow.Cells["cLabCumu6_LabCode"].Value.ToString();

        //                                var updValueTextLabRemark6 = objBkLab6.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark6).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                var item6 = iDataCumulative6.Where(w => w.tpl_en_no != null && w.tpl_lab_no == codelabTextRemark6).OrderByDescending(o => o.tpl_lab_date).DistinctBy(d => d.tpl_lab_no).Take(1).ToList();
                                        
        //                                if (updValueTextLabRemark6 == null)
        //                                {
        //                                    trn_book_lab upd6 = new trn_book_lab();
        //                                    upd6.tkh_id = RefBookID_tkh_id;
        //                                    upd6.tklb_en_no = item6.Select(s => s.tpl_en_no).FirstOrDefault();
        //                                    upd6.tklb_lab_no = item6.Select(s => s.tpl_lab_no).FirstOrDefault();
        //                                    upd6.tklb_lab_name = item6.Select(s => s.tpl_lab_name).FirstOrDefault();
        //                                    upd6.tklb_lab_date = (item6.Select(s => s.tpl_lab_date).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_date).FirstOrDefault() : null;
        //                                    upd6.tklb_lab_value = (item6.Select(s => s.tpl_lab_value).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_value).FirstOrDefault() : null;
        //                                    upd6.tklb_lab_unit = (item6.Select(s => s.tpl_lab_unit).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_unit).FirstOrDefault() : null;
        //                                    upd6.tklb_lab_range = (item6.Select(s => s.tpl_lab_range).FirstOrDefault() != null) ? item6.Select(s => s.tpl_lab_range).FirstOrDefault() : null;
        //                                    upd6.tklb_lab_result_thai = (item6.Select(s => s.mlp_tname).FirstOrDefault() != null) ? item6.Select(s => s.mlp_tname).FirstOrDefault() : null;
        //                                    upd6.tklb_lab_result_eng = (item6.Select(s => s.mlp_ename).FirstOrDefault() != null) ? item6.Select(s => s.mlp_ename).FirstOrDefault() : null;
        //                                    upd6.tklb_summary = (item6.Select(s => s.mlp_summary).FirstOrDefault() != null) ? Convert.ToChar(item6.Select(s => s.mlp_summary).FirstOrDefault()) : ' ';
        //                                    upd6.tklb_suppress = (ckSaveSuppressLab.Checked == true) ? 'Y' : 'N';
        //                                    upd6.tklb_remark = txtLabRemark.Text;
        //                                    upd6.tklb_create_by = Program.CurrentUser.mut_username;
        //                                    upd6.tklb_update_by = Program.CurrentUser.mut_username;
        //                                    objBkLab6.Add(upd6);
        //                                }
        //                                else
        //                                {
        //                                    updValueTextLabRemark6 = objBkLab6.Where(w => w.tklb_en_no != null && w.tklb_lab_no == codelabTextRemark6).OrderByDescending(o => o.tklb_lab_date).FirstOrDefault();
        //                                    if (valueTextRemark != txtLabRemark.Text)
        //                                    {
        //                                        updValueTextLabRemark6.tklb_remark = txtLabRemark.Text;
        //                                        chkEvent_txtremark = 0;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}


        //private void txtLabRemark_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        //{
        //    try
        //    {
        //        if (CurrentPanel > 0)
        //        {
        //            if (chkEvent_txtremark == 0)
        //            {
        //                valueTextRemark = "";
        //                valueTextRemark = txtLabRemark.Text;
        //                chkEvent_txtremark = 1;
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("LabCode is null, Please select lab form gridview.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        #endregion



        //HIV report P3050 P3005 X0012





    }     
}
