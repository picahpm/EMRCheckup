using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Globalization;

namespace BKvs2010
{
    public partial class frmNextAnnualCheckup : Form
    {

        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private string selhn { get; set; }

        public frmNextAnnualCheckup()
        {
            InitializeComponent();
        }

        private void frmNextAnnualCheckup_Load(object sender, EventArgs e)
        {
            this.Text = Program.GetRoomName();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            //Load Data
            this.LoadPatient30Day();

            GvQuestionaire.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            GvQuestionaire.ColumnHeadersHeight = GvQuestionaire.ColumnHeadersHeight * 2;
            GvQuestionaire.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            GvPatient30Day.ClearSelection();

            frmbg.Close();
        }

        private void dataGridViewSetHeader_Paint(object sender, PaintEventArgs e)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            //GvQuestionaire
            for (int i = 0; i < GvQuestionaire.Columns.Count; i++)
            {
                if (i == 2 || i == 6)
                {
                    Rectangle GvQuestion = GvQuestionaire.GetCellDisplayRectangle(i, -1, true);
                    int widthH3 = GvQuestionaire.Columns[i].Width;
                    GvQuestion.Width = (GvQuestion.Width + widthH3) - 2;
                    GvQuestion.Height = GvQuestion.Height / 2 - 2;
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), GvQuestion);
                    e.Graphics.DrawString(i == 2 ? "Questionnaire" : "Print By", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, GvQuestion, format);
                    i++;
                }
                else
                {
                    Rectangle GvQuestion = GvQuestionaire.GetCellDisplayRectangle(i, -1, true);
                    GvQuestion.Width = GvQuestion.Width - 2;
                    GvQuestion.Height = GvQuestion.Height / 2 - 2;
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), GvQuestion);
                    e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, GvQuestion, format);
                }
            }
        }

        private void ClearData()
        {
            if (GvHistoryCheckup.Rows.Count > 0) { GvHistoryCheckup.Rows.Clear(); }
            if (GvAppointmentSub.Rows.Count > 0) { GvAppointmentSub.Rows.Clear(); }
            if (GvQuestionaire.Rows.Count > 0) { GvQuestionaire.Rows.Clear(); }
            if (GvDiagnosis.Rows.Count > 0) { GvDiagnosis.Rows.Clear(); }
            if (GvLab.Rows.Count > 0) { GvLab.DataSource = null; } //GvLab.Rows.Clear(); GvLab.Columns.Clear(); }

            displaychart1.Series["BMI"].Points.Clear();
            displaychart1.Series["BP1(mmHg)"].Points.Clear();
            displaychart1.Series["BP2(mmHg)"].Points.Clear();
            displaychart1.Series["BW(kg)"].Points.Clear();
            displaychart1.Series["RR(/min)"].Points.Clear();

        }

        private void LoadLabAll(string hn)
        {
            int idata = 0;

            try
            {

                if (hn != String.Empty)
                {
                    DataTable dt = new DataTable();
                    dt.TableName = "lab";
                    dt.Columns.Add("no");
                    dt.Columns.Add("labno");
                    dt.Columns.Add("labname");
                    var objCol = (from t1 in dbc.sp_top5appointment_lab(hn) select t1).ToList();
                    for (int i = 0; i < objCol.Count; i++)
                    {
                        dt.Columns.Add(String.Format("{0:dd/MM/yyyy}", objCol[i].labdate));
                    }

                    var objRow = (from t1 in dbc.sp_hrow_appoint_lab(hn) select t1).ToList();
                    for (int j = 0; j < objRow.Count; j++)
                    {
                        dt.Rows.Add(j + 1, objRow[j].tpl_lab_no, objRow[j].tpl_lab_name);
                    }


                    int row = 0;
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k > 2)
                        {

                            var objdata = (from t1 in dbc.sp_detail_appoint_lab(objCol[row++].labdate, hn) select t1).ToList();
                            for (int q = 0; q < objdata.Count; q++)
                            {

                                DataRow[] rowdata = dt.Select("labno = '" + objdata[q].tpl_lab_no + "'");

                                foreach (DataRow data in rowdata)
                                {
                                    data[k] = objdata[q].tpl_lab_value;
                                }

                                idata = q;
                            }
                        }
                    }

                    dt.AcceptChanges();
                    GvLab.DataSource = dt;

                    GvLab.Columns[0].Width = 50;
                    GvLab.Columns[1].Width = 50;
                    GvLab.Columns[2].Width = 150;

                    GvLab.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    GvLab.Columns[1].Visible = false;
                    GvLab.Columns[2].DefaultCellStyle.BackColor = Color.LavenderBlush;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Error >>> iRow at : " + idata);
            }
        }

        //private void LoadLabAll(string hn)
        //{
        //    try
        //    {
        //        if (hn != String.Empty)
        //        {
        //            //H Col
        //            var objCol = (from t1 in dbc.trn_patient_labs where t1.tpl_hn_no == hn select t1).OrderByDescending(c => c.tpl_lab_date).Take(5).GroupBy(x => x.tpl_lab_date).ToList();
        //            for (int i = 0; i < objCol.Count; i++)
        //            {
        //                //Create Column Header
        //                DataGridViewColumn gCol = new DataGridViewColumn();
        //                DataGridViewCell gCell = new DataGridViewTextBoxCell();
        //                gCol.Name = "Col" + i;
        //                gCol.HeaderText = String.Format("{0:dd/MM/yyyy}", objCol[i].Key);
        //                gCol.Width = 80;
        //                gCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //                gCol.CellTemplate = gCell;
        //                GvLab.Columns.Add(gCol);

        //                //H Row
        //                var objRow = (from t1 in dbc.trn_patient_labs where t1.tpl_hn_no == hn && t1.tpl_lab_date == objCol[i].Key.Value select t1).OrderByDescending(c => c.tpl_lab_date).Take(5).ToList();
        //                for (int j = 0; j < objRow.Count; j++)
        //                {
        //                    //Create Row Header
        //                    DataGridViewRow gRow = new DataGridViewRow();
        //                    gRow.HeaderCell.Value = objRow[j].tpl_lab_name;
        //                    GvLab.RowHeadersWidth = 150;

        //                    GvLab.Rows.AddRange(gRow);

        //                    //put values
        //                    GvLab.Rows[j].Cells[i].Value = objRow[j].tpl_lab_value;
        //                }

        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void LoadHistoryCheckup(string hn)
        {
            if (hn != String.Empty)
            {
                //check patei
                var objpatei = (from t1 in dbc.trn_patients where t1.tpt_hn_no == hn select new { t1.tpt_id }).FirstOrDefault();
                if (objpatei != null)
                {
                    //check regis
                    var objregis = (from t1 in dbc.trn_patient_regis where t1.tpt_id == objpatei.tpt_id select t1);
                    if (objregis != null)
                    {
                        int irow = 1;
          
                        foreach (var data in objregis)
                        {
                            var objHistory = (from t1 in dbc.trn_patient_regis
                                              join t2 in dbc.mst_health_checkups on t1.mhc_id equals t2.mhc_id into t2_j
                                              from t3 in t2_j.DefaultIfEmpty()
                                              where t1.tpr_id == data.tpr_id
                                              select new
                                              {
                                                  Date = t1.tpr_arrive_date,
                                                  HCP = t3.mhc_ename == null ? "-" : t3.mhc_ename,
                                                  EN = t1.tpr_en_no,
                                                  DocName = dbc.mst_user_types.Where(x => x.mut_username == dbc.trn_doctor_hdrs.Where(c => c.tpr_id == data.tpr_id).Single().trh_create_by).Single().mut_fullname
                                              }).ToList();
                           
                            for (int i = 0; i < objHistory.Count; i++)
                            {

                                GvHistoryCheckup.Rows.Add(irow++, objHistory[i].Date, objHistory[i].HCP, objHistory[i].EN, objHistory[i].DocName == null ? "-" : objHistory[i].DocName.ToString());
                            }
                        }

                        var objQuestion = (from t1 in dbc.trn_patient_regis
                                           join t2 in dbc.trn_ques_patients on t1.tpr_id equals t2.tpr_id into t2_i
                                           from t3 in t2_i.DefaultIfEmpty()
                                           where t1.tpt_id == objpatei.tpt_id
                                           select new
                                           {
                                               Date = t1.tpr_arrive_date,
                                               QStatusYes = GetImage(t3.tqp_id == null ? false : true),
                                               QStatusNo = GetImage(t3.tqp_id == null ? true : false),
                                               Status = "-",
                                               Print = "-",
                                               Usr = "-",
                                               DTime = "-"
                                           }).ToList();

                        for (int i = 0; i < objQuestion.Count; i++)
                        {
                            GvQuestionaire.Rows.Add(i + 1, objQuestion[i].Date, objQuestion[i].QStatusYes, objQuestion[i].QStatusNo, objQuestion[i].Status, objQuestion[i].Print, objQuestion[i].Usr, objQuestion[i].DTime);
                        }

                    }
                }

            }
        }

        private void LoadAppointment(string hn)
        {
            if (hn != String.Empty)
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    //string HN_No = dbc.trn_patients.Where(c => c.tpt_id == dbc.trn_patients.Where(c => c.tpt_hn_no == hn).Single().tpt_id).Single().tpt_hn_no;
                    DataTable Dt = ws.GetAppointmentByHN(hn, "N");
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        GvAppointmentSub.Rows.Add(i + 1, String.Format("{0:dd/MM/yyyy} {1}", Dt.Rows[i]["AS_Date"], Dt.Rows[i]["AS_SessStartTime"]), Dt.Rows[i]["CTLOC_Desc"].ToString(), Dt.Rows[i]["SER_Desc"].ToString(), Dt.Rows[i]["RES_Desc"].ToString());
                    }
                }
            }
        }

        private void LoadQuestionaire(string hn)
        {
            if (hn != String.Empty)
            {
                var objQuestion = (from t1 in dbc.trn_patient_regis
                                   join t2 in dbc.trn_ques_patients on t1.tpr_id equals t2.tpr_id into t2_i
                                   from t3 in t2_i.DefaultIfEmpty()
                                   where t1.tpt_id == dbc.trn_patients.Where(c => c.tpt_hn_no == hn).Single().tpt_id
                                   select new
                                   {
                                       Date = t1.tpr_arrive_date,
                                       QStatusYes = GetImage(t3.tqp_id == null ? false : true),
                                       QStatusNo = GetImage(t3.tqp_id == null ? true : false),
                                       Status = "-",
                                       Print = "-",
                                       Usr = "-",
                                       DTime = "-"
                                   }).ToList();

                GvQuestionaire.DataSource = objQuestion;
                for (int i = 0; i < objQuestion.Count; i++)
                {
                    GvQuestionaire.Rows[i].Cells[0].Value = (i + 1);//, objQuestion[i].Date, GetImage(objQuestion[i].QStatusYes), GetImage(objQuestion[i].QStatusNo) , objQuestion[i].Print, objQuestion[i].Usr, objQuestion[i].DTime); 
                }
            }
        }

        private Image GetImage(bool chk)
        {
            Image img = null;
            switch (chk)
            {
                case true:
                    img = imageList1.Images[0];
                    break;
                default:
                    img = imageList1.Images[1];
                    break;
            }
            return img;
        }

        private void LoadGraphBasicMeasurement(string hn)
        {
            if (hn != String.Empty)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[12] { new DataColumn("ObservationItem"), new DataColumn("BodyWeight"), new DataColumn("Bmi"), new DataColumn("Diastolic"), new DataColumn("Systolic"), new DataColumn("RR"), new DataColumn("Height"), new DataColumn("Pluse"), new DataColumn("Waist"), new DataColumn("Temp"), new DataColumn("Vision_lt"), new DataColumn("Vision_rt") });
                string HN_No = hn;
                var BindDataAll = dbc.pw_Get_Basic_measureTopFive(HN_No).ToList();
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

                string str_value1, str_value2, str_value3, str_value4, str_value5, str_value6, str_value7, str_value8, str_value9, str_value10, str_value11;
                List<pw_Get_Basic_measureTopFiveResult> getBasic = new List<pw_Get_Basic_measureTopFiveResult>();
                getBasic = dbc.pw_Get_Basic_measureTopFive(HN_No).ToList();
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

                    if (str_value3 == "True")
                    {

                        displaychart1.Series["BMI"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_bmi) ? 0 : Convert.ToDouble(getBasic[i].tbd_bmi));
                    }
                    if (str_value4 == "True")
                    {

                        displaychart1.Series["BP1(mmHg)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_diastolic) ? 0 : Convert.ToDouble(getBasic[i].tbd_diastolic));
                    }
                    if (str_value5 == "True")
                    {

                        displaychart1.Series["BP2(mmHg)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_systolic) ? 0 : Convert.ToDouble(getBasic[i].tbd_systolic));
                    }

                    if (str_value8 == "True")
                    {

                        displaychart1.Series["BW(kg)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_weight) ? 0 : Convert.ToDouble(getBasic[i].tbd_weight));
                    }
                    if (str_value9 == "True")
                    {

                        displaychart1.Series["RR(/min)"].Points.AddXY(Convert.ToDateTime(getBasic[i].tbd_date).ToString("dd/MM/yyy"), string.IsNullOrEmpty(getBasic[i].tbd_rr) ? 0 : Convert.ToDouble(getBasic[i].tbd_rr));
                    }
                }


            }
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

        private void LoadDiagnosis(string hn)
        {
            if (hn != String.Empty)
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    string HN_No = hn;

                    DataTable Dt = ws.GetTop5Diagnosis(HN_No);
                    var objDiagnosis = (from DataRow t2 in Dt.Rows select t2).OrderByDescending(c => c.Field<DateTime>("MRDIA_Date")).ToList();
                    for (int i = 0; i < objDiagnosis.Count; i++)
                    {

                        GvDiagnosis.Rows.Add(i + 1, objDiagnosis[i]["MRDIA_Date"], objDiagnosis[i]["MRCID_Code"].ToString(), objDiagnosis[i]["MRCID_Desc"].ToString(), objDiagnosis[i]["DTYP_Desc"].ToString(), objDiagnosis[i]["SSUSR_Name"].ToString());
                    }
                }
            }
        }

        private void LoadPatient30Day()
        {
            try
            {
                //test date;

                //DateTime dtTest = ddlAppointDate.Value;
                //
                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();

                var objPatient = (from t1 in dbc.trn_patient_regis
                                  where t1.tpr_arrive_date.Value.Date == Program.GetServerDateTime().Date
                                  //where System.Data.Linq.SqlClient.SqlMethods.DateDiffDay(t1.tpr_arrive_date, Program.GetServerDateTime()) > 335
                                  select t1).ToList();

                int seq = 1;
                foreach (var data in objPatient)
                {
                    var pt = data.trn_patient;
                    GvPatient30Day.Rows.Add(seq, pt.tpt_hn_no, pt.tpt_othername, data.tpr_arrive_date, data.tpr_mobile_phone);
                    seq++;
                }
               
                label2.Text = String.Format("Next Annual Checkup 30 Day {0} Records.", GvPatient30Day.RowCount);
                frmbg.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDiagnosisMore_Click(object sender, EventArgs e)
        {
            if (selhn != null)
            {
                frmMoreDetail_Diagnosis frm = new frmMoreDetail_Diagnosis();

                frm.refhn = selhn;

                frm.ShowDialog();
            }
        }

        private void GvPatient30Day_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }

            string hn = GvPatient30Day.Rows[e.RowIndex].Cells["Colhn"].Value.ToString();
            selhn = hn;

            string DateAppoint = GvPatient30Day.Rows[e.RowIndex].Cells["Colarrivedate"].Value.ToString();

            this.ClearData();

            this.LoadHistoryCheckup(hn); //<-----LoadQuestionaire
            this.LoadAppointment(hn);
            //this.LoadQuestionaire(hn);
            this.LoadDiagnosis(hn);
            this.LoadGraphBasicMeasurement(hn);
            this.LoadLabAll(hn);


        }
    }
}
