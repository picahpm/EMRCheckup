using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Data.Common;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;
namespace BKvs2010
{
    public partial class frmCumulativeVitalSigns : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        DataTable dt = new DataTable();
        private static List<pw_Get_Basic_measureTopFiveResult> getBasic;
        public frmCumulativeVitalSigns()
        {
            InitializeComponent();
        }

        #region Generate
        private DataTable AutoNumber(DataTable SourceTable)
        {
            DataTable ResultTable = new DataTable();
            DataColumn AutoNumberColumn = new DataColumn();
            AutoNumberColumn.ColumnName = "No.";
            AutoNumberColumn.DataType = typeof(int);
            AutoNumberColumn.AutoIncrement = true;
            AutoNumberColumn.AutoIncrementSeed = 1;
            AutoNumberColumn.AutoIncrementStep = 1;
            ResultTable.Columns.Add(AutoNumberColumn);
            ResultTable.Merge(SourceTable);
            return ResultTable;
        }
        private bool IsFloatOrInt(string value)
        {
            int intValue;
            float floatValue;
            if (value == "0" || value == "0.0")
                return false;
            else
                return Int32.TryParse(value, out intValue) || float.TryParse(value, out floatValue);
        }
        #endregion
        #region pageload
        private void frmCumulativeVitalSigns_Load(object sender, EventArgs e)
        {
            this.Text = Program.GetRoomName("Cumulative Vital Signs");
            try
            {
                if (Program.CurrentRegis != null)
                {
                    panel3.Visible = true;
                    //int tpr_id = 805;//370;
                    //string str_HN = "01-10-054361";//"01-07-058653";
                    int tpr_id = string.IsNullOrEmpty(Program.CurrentRegis.tpr_id.ToString()) ? int.MinValue : Convert.ToInt32(Program.CurrentRegis.tpr_id);
                    string str_HN = dbc.trn_patients.Where(a => a.tpt_id == Program.CurrentRegis.tpt_id).Select(s => s.tpt_hn_no).FirstOrDefault();
                    uiProfileHorizontal1.Loaddata();
                    dt.Columns.AddRange(new DataColumn[12] { new DataColumn("ObservationItem"), new DataColumn("BodyWeight"), new DataColumn("Bmi"), new DataColumn("Diastolic"), new DataColumn("Systolic"), new DataColumn("RR"), new DataColumn("Height"), new DataColumn("Pluse"), new DataColumn("Waist"), new DataColumn("Temp"), new DataColumn("Vision_lt"), new DataColumn("Vision_rt") });
                    cmbValue.SelectedIndex = 1;
                    var BindDataAll = dbc.pw_Get_Basic_measureTopFive(str_HN).ToList();
                    foreach (var element in BindDataAll)
                    {
                        var row = dt.NewRow();
                        row["BodyWeight"] = element.tbd_weight;
                        row["Bmi"] = element.tbd_bmi;
                        row["Diastolic"] = element.tbd_diastolic;
                        row["Systolic"] = element.tbd_systolic;
                        row["RR"] = element.tbd_rr;
                        row["ObservationItem"] = Convert.ToDateTime(element.tbd_date).ToString("dd/MM/yyy", new CultureInfo("en-US"));
                        row["Height"] = element.tbd_height;
                        row["Pluse"] = element.tbd_pulse;
                        row["Waist"] = element.tbd_waist;
                        row["Temp"] = element.tbd_temp;
                        row["Vision_lt"] = element.tbd_vision_lt;
                        row["Vision_rt"] = element.tbd_vision_rt;
                        dt.Rows.Add(row);
                    }
                    DataTable dtReturn = GetInversedDataTable(dt, "ObservationItem");
                    dgvData.DataSource = AutoNumber(dtReturn);
                    //getBasic = dbc.pw_Get_Basic_measure("01-07-058653", 370, "all").ToList();
                    getBasic = dbc.pw_Get_Basic_measureTopFive(str_HN).ToList();
                    string str_value1, str_value2, str_value3, str_value4, str_value5, str_value6, str_value7, str_value8, str_value9, str_value10, str_value11;
                    for (int i = 0; i <= getBasic.Count - 1; i++)
                    {
                        str_value1 = IsFloatOrInt(getBasic[i].tbd_vision_lt).ToString();
                        str_value2 = IsFloatOrInt(getBasic[i].tbd_vision_rt).ToString();
                        str_value3 = IsFloatOrInt(getBasic[i].tbd_bmi).ToString();
                        str_value4 = IsFloatOrInt(getBasic[i].tbd_diastolic).ToString();
                        str_value5 = IsFloatOrInt(getBasic[i].tbd_systolic).ToString();
                        str_value6 = IsFloatOrInt(getBasic[i].tbd_temp).ToString();
                        str_value7 = IsFloatOrInt(getBasic[i].tbd_waist).ToString();
                        str_value8 = IsFloatOrInt(getBasic[i].tbd_weight).ToString();
                        str_value9 = IsFloatOrInt(getBasic[i].tbd_rr).ToString();
                        str_value10 = IsFloatOrInt(getBasic[i].tbd_pulse).ToString();
                        str_value11 = IsFloatOrInt(getBasic[i].tbd_height).ToString();
                        if (str_value1 == "True")
                        {
                            //displaychart_vision_lt.Series.Clear();
                            displaychart_vision_lt.Series["lt"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_vision_lt) ? 0 : Convert.ToDouble(getBasic[i].tbd_vision_lt));
                        }
                        if (str_value2 == "True")
                        {
                            displaychart_vision_rt.Series["rt"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_vision_rt) ? 0 : Convert.ToDouble(getBasic[i].tbd_vision_rt));
                        }
                        if (str_value3 == "True")
                        {
                            displaychart_bmi.Series["BMI"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_bmi) ? 0 : Convert.ToDouble(getBasic[i].tbd_bmi));
                            displaychart1.Series["BMI"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_bmi) ? 0 : Convert.ToDouble(getBasic[i].tbd_bmi));
                        }
                        if (str_value4 == "True")
                        {
                            displaychart_bp.Series["Bp1"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_diastolic) ? 0 : Convert.ToDouble(getBasic[i].tbd_diastolic));
                            displaychart1.Series["BP1(mmHg)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_diastolic) ? 0 : Convert.ToDouble(getBasic[i].tbd_diastolic));
                        }
                        if (str_value5 == "True")
                        {
                            displaychart_bp.Series["Bp2"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_systolic) ? 0 : Convert.ToDouble(getBasic[i].tbd_systolic));
                            displaychart1.Series["BP2(mmHg)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_systolic) ? 0 : Convert.ToDouble(getBasic[i].tbd_systolic));
                        }
                        if (str_value6 == "True")
                        {
                            displaychart_temp.Series["temp"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_temp) ? 0 : Convert.ToDouble(getBasic[i].tbd_temp));
                        }
                        if (str_value7 == "True")
                        {
                            displaychart_waist.Series["waist"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_waist) ? 0 : Convert.ToDouble(getBasic[i].tbd_waist));
                        }
                        if (str_value8 == "True")
                        {
                            displaychart_bodyweight.Series["BW(kg)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_weight) ? 0 : Convert.ToDouble(getBasic[i].tbd_weight));
                            displaychart1.Series["BW(kg)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_weight) ? 0 : Convert.ToDouble(getBasic[i].tbd_weight));
                        }
                        if (str_value9 == "True")
                        {
                            displaychart_rr.Series["RR(/min)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_rr) ? 0 : Convert.ToDouble(getBasic[i].tbd_rr));
                            displaychart1.Series["RR(/min)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_rr) ? 0 : Convert.ToDouble(getBasic[i].tbd_rr));
                        }
                        if (str_value10 == "True")
                        {
                            displaychart_pluse.Series["pluse"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_pulse) ? 0 : Convert.ToDouble(getBasic[i].tbd_pulse));
                        }
                        if (str_value11 == "True")
                        {
                            displaychart_height.Series["height"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_height) ? 0 : Convert.ToDouble(getBasic[i].tbd_height));
                        }
                    }
                }
                else
                {
                    lblwarning.Visible = true;
                    lblwarning.Text = "No Data to Display";
                    panel3.Visible = false;
                }
            }
            catch
            {
                MessageBox.Show("Cannot display value !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion
        private void cmbValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbValue.Text == "ShowValue")
            {
                displaychart1.Series["RR(/min)"].IsValueShownAsLabel = true;
                displaychart1.Series["BW(kg)"].IsValueShownAsLabel = true;
                displaychart1.Series["BP1(mmHg)"].IsValueShownAsLabel = true;
                displaychart1.Series["BP2(mmHg)"].IsValueShownAsLabel = true;
                displaychart1.Series["BMI"].IsValueShownAsLabel = true;
                displaychart_bmi.Series["BMI"].IsValueShownAsLabel = true;
                displaychart_bodyweight.Series["BW(kg)"].IsValueShownAsLabel = true;
                displaychart_rr.Series["RR(/min)"].IsValueShownAsLabel = true;
                displaychart_temp.Series["temp"].IsValueShownAsLabel = true;
                displaychart_height.Series["height"].IsValueShownAsLabel = true;
                displaychart_waist.Series["waist"].IsValueShownAsLabel = true;
                displaychart_height.Series["height"].IsValueShownAsLabel = true;
                displaychart_pluse.Series["pluse"].IsValueShownAsLabel = true;
                displaychart_vision_lt.Series["lt"].IsValueShownAsLabel = true;
                displaychart_vision_rt.Series["rt"].IsValueShownAsLabel = true;
                displaychart_bp.Series["Bp1"].IsValueShownAsLabel = true;
                displaychart_bp.Series["Bp2"].IsValueShownAsLabel = true;
            }
            else
            {
                displaychart1.Series["RR(/min)"].IsValueShownAsLabel = false;
                displaychart1.Series["BW(kg)"].IsValueShownAsLabel = false;
                displaychart1.Series["BP1(mmHg)"].IsValueShownAsLabel = false;
                displaychart1.Series["BP2(mmHg)"].IsValueShownAsLabel = false;
                displaychart1.Series["BMI"].IsValueShownAsLabel = false;
                displaychart_bmi.Series["BMI"].IsValueShownAsLabel = false;
                displaychart_bodyweight.Series["BW(kg)"].IsValueShownAsLabel = false;
                displaychart_rr.Series["RR(/min)"].IsValueShownAsLabel = false;
                displaychart_temp.Series["temp"].IsValueShownAsLabel = false;
                displaychart_height.Series["height"].IsValueShownAsLabel = false;
                displaychart_waist.Series["waist"].IsValueShownAsLabel = false;
                displaychart_height.Series["height"].IsValueShownAsLabel = false;
                displaychart_pluse.Series["pluse"].IsValueShownAsLabel = false;
                displaychart_vision_lt.Series["lt"].IsValueShownAsLabel = false;
                displaychart_vision_rt.Series["rt"].IsValueShownAsLabel = false;
                displaychart_bp.Series["Bp1"].IsValueShownAsLabel = false;
                displaychart_bp.Series["Bp2"].IsValueShownAsLabel = false;
            }
        }
        #region InversedData
        private static DataTable GetInversedDataTable(DataTable table, string columnX, string columnY, string columnZ, string nullValue, bool sumValues)
        {
            DataTable returnTable = new DataTable();
            if (columnX == "")
                columnX = table.Columns[0].ColumnName;
            returnTable.Columns.Add(columnY);
            List<string> columnXValues = new List<string>();
            foreach (DataRow dr in table.Rows)
            {
                string columnXTemp = dr[columnX].ToString();
                if (!columnXValues.Contains(columnXTemp))
                {
                    columnXValues.Add(columnXTemp);
                    returnTable.Columns.Add(columnXTemp);
                }
            }
            if (columnY != "" && columnZ != "")
            {
                List<string> columnYValues = new List<string>();

                foreach (DataRow dr in table.Rows)
                {
                    if (!columnYValues.Contains(dr[columnY].ToString()))
                        columnYValues.Add(dr[columnY].ToString());
                }

                foreach (string columnYValue in columnYValues)
                {
                    DataRow drReturn = returnTable.NewRow();
                    drReturn[0] = columnYValue;
                    DataRow[] rows = table.Select(columnY + "='" + columnYValue + "'");
                    foreach (DataRow dr in rows)
                    {
                        string rowColumnTitle = dr[columnX].ToString();
                        foreach (DataColumn dc in returnTable.Columns)
                        {
                            if (dc.ColumnName == rowColumnTitle)
                            {
                                if (sumValues)
                                {
                                    try
                                    {
                                        drReturn[rowColumnTitle] =
                                             Convert.ToDecimal(drReturn[rowColumnTitle]) +
                                             Convert.ToDecimal(dr[columnZ]);
                                    }
                                    catch
                                    {
                                        drReturn[rowColumnTitle] = dr[columnZ];
                                    }
                                }
                                else
                                {
                                    drReturn[rowColumnTitle] = dr[columnZ];
                                }
                            }
                        }
                    }
                    returnTable.Rows.Add(drReturn);
                }
            }
            else
            {
                MessageBox.Show("The columns to perform inversion are not provided", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (nullValue != "")
            {
                foreach (DataRow dr in returnTable.Rows)
                {
                    foreach (DataColumn dc in returnTable.Columns)
                    {
                        if (dr[dc.ColumnName].ToString() == "")
                            dr[dc.ColumnName] = nullValue;
                    }
                }
            }
            return returnTable;
        }

        private static DataTable GetInversedDataTable(DataTable table, string columnX,params string[] columnsToIgnore)
        {
            DataTable returnTable = new DataTable();
            if (columnX == "")
                columnX = table.Columns[0].ColumnName;
            returnTable.Columns.Add(columnX);
            List<string> columnXValues = new List<string>();
            List<string> listColumnsToIgnore = new List<string>();
            if (columnsToIgnore.Length > 0)
                listColumnsToIgnore.AddRange(columnsToIgnore);

            if (!listColumnsToIgnore.Contains(columnX))
                listColumnsToIgnore.Add(columnX);

            foreach (DataRow dr in table.Rows)
            {
                string columnXTemp = dr[columnX].ToString();
                if (!columnXValues.Contains(columnXTemp))
                {
                    columnXValues.Add(columnXTemp);
                    returnTable.Columns.Add(columnXTemp);
                }
                else
                {
                    //MessageBox.Show("The inversion used must have " +
                    //                    "unique values for column " + columnX);
                }
            }
            foreach (DataColumn dc in table.Columns)
            {
                if (!columnXValues.Contains(dc.ColumnName) &&
                    !listColumnsToIgnore.Contains(dc.ColumnName))
                {
                    DataRow dr = returnTable.NewRow();
                    dr[0] = dc.ColumnName;
                    returnTable.Rows.Add(dr);
                }
            }
            for (int i = 0; i < returnTable.Rows.Count; i++)
            {
                for (int j = 1; j < returnTable.Columns.Count; j++)
                {
                    returnTable.Rows[i][j] = table.Rows[j - 1][returnTable.Rows[i][0].ToString()].ToString();
                }
            }
            return returnTable;
        }
      
        #endregion

        private void btnGetInversedData_Click(object sender, EventArgs e)
        {
            DataTable dtReturn = GetInversedDataTable(dt, "ObservationItem");
            dgvData.DataSource = dtReturn;
        }

        private void dgvData_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for (int i = 0; i < dgvData.ColumnCount; i++)
                dgvData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

    }
}
