using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DBCheckup;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
namespace BKvs2010
{
    public partial class frmCumulative : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        public frmCumulative()
        {
            InitializeComponent();
        }
        string strID, series_chart;
        DataTable tb1 = new DataTable();
        DataTable tmp = new DataTable();
        public int ptpr_id { get; set; }
        public string pHN_no { get; set; }
        public string pHeadLabNo { get; set; }
        #region Function
        private void BindData() 
        {
            try
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                var GetLabProfile_ByHN = (from t1 in dbc.trn_patient_labs
                                          join t2 in dbc.trn_patient_ass_dtls.Where(x => x.trn_patient_ass_hdr.trn_patient_ass_grp.tpr_id == ptpr_id && x.trn_patient_ass_hdr.tpeh_order_code == pHeadLabNo)
                                          on t1.tpl_lab_no equals t2.tped_lab_code
                                          where t1.tpl_hn_no==pHN_no
                                          orderby t1.tpl_hn_no, t1.tpl_lab_date descending
                                          select new 
                                          {
                                              dEN = t1.tpl_en_no,
                                              dTestItem = t1.tpl_lab_name,
                                              dValue = t1.tpl_lab_value,
                                              dUnit = t1.tpl_lab_unit,
                                              dRef = t1.tpl_lab_range,
                                              dLabno = t1.tpl_lab_no,
                                              dDate = Convert.ToDateTime(t1.tpl_lab_date).ToString("dd/MM/yyy", new CultureInfo("en-US")),
                                          }).ToList();
                dgvData.DataSource = GetLabProfile_ByHN
                                                        .DistinctBy(pk => new { pk.dTestItem })
                                                        .Select((item, index) => new
                                                        {
                                                            No = index + 1,
                                                            EN = item.dEN,
                                                            TestItem = item.dTestItem,
                                                            Value = item.dValue,
                                                            Unit = item.dUnit,
                                                            lab = item.dLabno,
                                                            Ref=item.dRef,
                                                            DDMMYY1 = (GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Take(1).FirstOrDefault().ToString() : "",
                                                            DDMMYY2 = (GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(1).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(1).Take(1).FirstOrDefault().ToString() : "",
                                                            DDMMYY3 = (GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(2).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(2).Take(1).FirstOrDefault().ToString() : "",
                                                            DDMMYY4 = (GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(3).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(3).Take(1).FirstOrDefault().ToString() : "",
                                                            DDMMYY5 = (GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(4).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == item.dLabno).Select(s => s.dValue).Skip(4).Take(1).FirstOrDefault().ToString() : "",
                                                        }).ToList();
                dgvData.Columns["ColDDMMYY1"].HeaderText = (GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Take(1).FirstOrDefault().ToString() : "";
                dgvData.Columns["ColDDMMYY2"].HeaderText = (GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(1).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(1).Take(1).FirstOrDefault().ToString() : "";
                dgvData.Columns["ColDDMMYY3"].HeaderText = (GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(2).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(2).Take(1).FirstOrDefault().ToString() : "";
                dgvData.Columns["ColDDMMYY4"].HeaderText = (GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(3).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(3).Take(1).FirstOrDefault().ToString() : "";
                dgvData.Columns["ColDDMMYY5"].HeaderText = (GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(4).Take(1).FirstOrDefault() != null) ? GetLabProfile_ByHN.Where(a => a.dLabno == dgvData.CurrentRow.Cells[5].Value.ToString()).Select(s => s.dDate).Skip(4).Take(1).FirstOrDefault().ToString() : "";
                dgvData.Columns["ColEN"].Visible = false;
                dgvData.Columns["Collab"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
       
        private bool IsFloatOrInt(string value)
        {
            int intValue;
            float floatValue;
            if (value == "0")
                return false;
            else
                return Int32.TryParse(value, out intValue) || float.TryParse(value, out floatValue);
        }

        private bool IsFloatOrInt_2(string value) 
        {
            int intValue;
            float floatValue;
                return Int32.TryParse(value, out intValue) || float.TryParse(value, out floatValue);
        }
        private void clrControls(Panel pnl)
        {
            for (int i = 0; i <= 30; i++)
            {
                foreach (Control ctl in pnl.Controls)
                {
                    Chart ch;
                    if (ctl is Chart)
                    {
                        ch = (Chart)ctl;
                        pnl.Controls.Remove(ch);
                    }
                }
            }
        }
        #endregion
        private void frmCumulative_Load(object sender, EventArgs e)
        {
            uiProfileHorizontal1.Loaddata();
            BindData();
            var objasshdr = (from t1 in dbc.trn_patient_ass_hdrs
                          where t1.trn_patient_ass_grp.tpr_id == ptpr_id
                          && t1.tpeh_order_code== pHeadLabNo
                          select t1).FirstOrDefault();
            lblhead.Text = "Lab Profile " + objasshdr.tpeh_order_name;
            tb1.Columns.AddRange(new DataColumn[2] { new DataColumn("value"), new DataColumn("lab") });
            cmbValue.SelectedIndex = 1;
            displaychart1.Series["sr2"].IsVisibleInLegend = false;
            if (dgvData.Rows.Count > 0)
            {
                dgvData.Columns[0].Width = 40;
                dgvData.Columns[3].Visible = false;
                dgvData.Columns[5].Visible = false;
                this.dgvData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                for (int i = 0; i < dgvData.ColumnCount; i++)
                    dgvData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
           //// dgvData_CellContentClick(dgvData, new DataGridViewCellEventArgs(2, 0));
        }

        //private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //DataGridViewRow row = dgvData.Rows[e.RowIndex];
        //    frmBGScreen frmbg = new frmBGScreen();
        //    frmbg.Show();
        //    Application.DoEvents();
        //    displaychart1.Series["sr"].Points.Clear();
        //    string str_value;
        //    str_value = IsFloatOrInt(dgvData.CurrentRow.Cells[3].Value.ToString()).ToString();
        //    if (str_value == "False")
        //    {
        //        lblwarning.Visible = true;
        //        lblwarning.Text = "Cannot display value";
        //        this.AutoScrollPosition = new Point(0, 0);
        //        lblwarning.Focus();
        //    }
        //    else
        //    {
        //        series_chart = dgvData.CurrentRow.Cells[2].Value.ToString();
        //        strID = dgvData.CurrentRow.Cells[5].Value.ToString();
        //        str_value = GenGraph(str_value);
        //        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
        //    }
        //    frmbg.Close();
        //}
        private string GenGraph(string str_value)
        {
            try
            {
                clrControls(pnlContain);
                lblwarning.Text = "";
                lblwarning.Visible = true;
                displaychart1.Text = series_chart;
                displaychart1.Titles["Title1"].Text = series_chart;
                displaychart1.Series["sr"].LegendText = series_chart;
                displaychart1.Series["sr2"].LegendText = dgvData.CurrentRow.Cells[5].Value.ToString();
                tb1.Clear();
                //getlab_byHN = dbc.pw_Get_LabProfile_ByHN(pHN_no).Where(c => c.tpl_head_lab_no == pHeadLabNo && c.tpl_lab_no == strID).DistinctBy(c => c.tpl_en_no).ToList();
               var objPatientLab = (from t1 in dbc.trn_patient_labs
                                    join t2 in dbc.trn_patient_ass_dtls.Where(x => x.trn_patient_ass_hdr.trn_patient_ass_grp.tpr_id == ptpr_id && x.trn_patient_ass_hdr.tpeh_order_code == pHeadLabNo)
                                    on t1.tpl_lab_no equals t2.tped_lab_code
                                          where t1.tpl_lab_no==strID
                                          && t1.tpr_id==ptpr_id
                                          orderby t1.tpl_hn_no, t1.tpl_lab_date descending
                                        select new PatientLabCumulative
                                          {
                                              EN = t1.tpl_en_no,
                                              TestItem = t1.tpl_lab_name,
                                              Value = t1.tpl_lab_value,
                                              Unit = t1.tpl_lab_unit,
                                              Ref = t1.tpl_lab_range,
                                              Labno = t1.tpl_lab_no,
                                              Date = Convert.ToDateTime(t1.tpl_lab_date).ToString("dd/MM/yyy", new CultureInfo("en-US")),
                                          }).DistinctBy(c => c.EN).ToList();
               foreach(PatientLabCumulative item in objPatientLab)
               {
                    displaychart1.Series["sr"].Points.AddXY(item.Date, string.IsNullOrEmpty(item.Value) ? 0 : Convert.ToDouble(item.Value));
               }

                for (int j = 0; j <= dgvData.Rows.Count - 1; j++)
                {
                    tb1.Rows.Add(dgvData.Rows[j].Cells[2].Value, dgvData.Rows[j].Cells[5].Value);
                    gvtmp.DataSource = tb1;
                }
                for (int i2 = 0; i2 <= gvtmp.Rows.Count - 1; i2++)
                {
                    if (gvtmp.Rows[i2].Cells[0].Value.ToString() == series_chart)
                    {
                        gvtmp.Rows.RemoveAt(i2);
                    }
                    if (dgvData.Rows.Count > 1)
                    {
                        try
                        {
                            pnlContain.Visible = true;
                            Chart ch = new Chart();
                            ch.Name = "chart" + i2;
                            ch.Left =  5;
                            ch.Top = (200 * i2) + 5;
                            ch.Width = 250;
                            ch.Height = 160;
                            Color result = Color.FromArgb(243, 223, 193);
                            ch.BackColor = result;
                            ch.BorderlineDashStyle = ChartDashStyle.Solid;
                            ch.BorderlineColor = Color.FromArgb(181, 64, 1);
                            ch.BorderlineWidth = 2;
                            ch.BackGradientStyle = GradientStyle.TopBottom;
                            ch.Palette = ChartColorPalette.BrightPastel;
                            Title title = new Title(gvtmp.Rows[i2].Cells[0].Value.ToString(), Docking.Top, new Font("Tohoma", 12), Color.Black);
                            ch.Text = gvtmp.Rows[i2].Cells[1].Value.ToString();
                            ch.Tag = gvtmp.Rows[i2].Cells[0].Value.ToString();////Title
                            Legend l = new Legend("Test1");
                            ch.Titles.Add(title);
                            Series s = new Series("sr");
                            ch.ChartAreas.Add("Area1");
                            s.ChartArea = "Area1";
                            s.ChartType = SeriesChartType.Line;
                            s.BorderWidth = 3;
                            s.MarkerSize = 8;
                            s.ShadowColor = Color.Black;
                            s.ShadowOffset = 2;
                            s.Color = Color.FromArgb(220, 65, 140, 240);
                            s.MarkerStyle = MarkerStyle.Circle;
                            ch.MouseClick += new MouseEventHandler(chart1_MouseClick);
                            s.IsValueShownAsLabel = true;
                            ch.Series.Add(s);

                            //getlab_byHN = dbc.pw_Get_LabProfile_ByHN(pHN_no).Where(c => c.tpl_head_lab_no == pHeadLabNo && c.tpl_lab_no == gvtmp.Rows[i2].Cells[1].Value.ToString()).DistinctBy(c => c.tpl_en_no).ToList();
                            //for (int i = 0; i <= getlab_byHN.Count - 1; i++)
                            //{
                            //    str_value = IsFloatOrInt(getlab_byHN[i].tpl_lab_value.ToString()).ToString();
                            //    if (str_value == "True")
                            //    {
                            //        ch.Series["sr"].Points.AddXY(Convert.ToDateTime(getlab_byHN[i].tpl_lab_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getlab_byHN[i].tpl_lab_value) ? 0 : Convert.ToDouble(getlab_byHN[i].tpl_lab_value));
                            //    }
                            //}
                          string  strIDLabNo = gvtmp.Rows[i2].Cells[1].Value.ToString();
                          var objpatientLabSub = (from t1 in dbc.trn_patient_labs
                                                  join t2 in dbc.trn_patient_ass_dtls.Where(x => x.trn_patient_ass_hdr.trn_patient_ass_grp.tpr_id == ptpr_id && x.trn_patient_ass_hdr.tpeh_order_code == pHeadLabNo)
                                                  on t1.tpl_lab_no equals t2.tped_lab_code
                                                  where t1.tpl_lab_no == strIDLabNo
                                                  && t1.tpr_id == ptpr_id
                                                  orderby t1.tpl_hn_no, t1.tpl_lab_date descending
                                                  select new PatientLabCumulative
                                                  {
                                                      EN = t1.tpl_en_no,
                                                      TestItem = t1.tpl_lab_name,
                                                      Value = t1.tpl_lab_value,
                                                      Unit = t1.tpl_lab_unit,
                                                      Ref = t1.tpl_lab_range,
                                                      Labno = t1.tpl_lab_no,
                                                      Date = Convert.ToDateTime(t1.tpl_lab_date).ToString("dd/MM/yyy", new CultureInfo("en-US")),
                                                  }).DistinctBy(c => c.EN).ToList();
                          foreach (PatientLabCumulative item in objpatientLabSub)
                          {
                              str_value = IsFloatOrInt(item.Value).ToString();
                              if (str_value == "True")
                              {
                                  ch.Series["sr"].Points.AddXY(item.Date, string.IsNullOrEmpty(item.Value) ? 0 : Convert.ToDouble(item.Value));
                              }
                          }
                          pnlContain.Controls.Add(ch);
                        }
                        catch (Exception ex)
                        {
                           MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        pnlContain.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                displaychart1.Series["sr"].Points.Clear();
                pnlContain.Visible = false;
                MessageBox.Show("Cannot display value ! " + ex.Message);
            }
            return str_value;
        }

        private void cmbValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbValue.Text == "ShowValue")
            {
                displaychart1.Series["sr"].IsValueShownAsLabel = true;
            }
            else
            {
                displaychart1.Series["sr"].IsValueShownAsLabel = false;
            }
        }
       
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();
                Application.DoEvents();

                displaychart1.Series["sr"].Points.Clear();
                Chart itemdata = (Chart)sender;
                strID = itemdata.Text;
                series_chart = itemdata.Tag.ToString();
                string str_value;
                str_value = IsFloatOrInt(dgvData.CurrentRow.Cells[3].Value.ToString()).ToString();
                GenGraph(str_value);

                frmbg.Close();
            }
        }

        private void dgvData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvData.ClearSelection();
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dgvData.ColumnCount; i++)
                dgvData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            displaychart1.Series["sr"].Points.Clear();
            string str_value;
            str_value = IsFloatOrInt(dgvData.CurrentRow.Cells[3].Value.ToString()).ToString();
            if (str_value == "False")
            {
                lblwarning.Visible = true;
                lblwarning.Text = "Cannot display value";
                this.AutoScrollPosition = new Point(0, 0);
                lblwarning.Focus();
            }
            else
            {
                series_chart = dgvData.CurrentRow.Cells[2].Value.ToString();
                strID = dgvData.CurrentRow.Cells[5].Value.ToString();
                str_value = GenGraph(str_value);
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            }

            frmbg.Close();
        }

    }
    public class PatientLabCumulative
    {
        public String EN {get;set;}
        public String TestItem{get;set;}
        public String Value { get; set; }
        public String Unit{get;set;}
        public String Ref {get;set;}
        public String Labno{get;set;}
        public String Date { get; set; }
    }
}
