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
    public partial class frmAppointment : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private string selhn { get; set; }

        public frmAppointment()
        {
            InitializeComponent();
        }

        private void frmAppointment_Load(object sender, EventArgs e)
        {
            // Add Button in Gridview
            //DataGridViewButtonColumn doppatientButton = new DataGridViewButtonColumn();
            //doppatientButton.HeaderText = "";
            //doppatientButton.Name = "Colcall";
            //doppatientButton.UseColumnTextForButtonValue = true;
            //doppatientButton.Text = "ลงทะเบียน";
            //doppatientButton.Width = 80;
            //dgvAppointments.Columns.Add(doppatientButton);
            timer1.Enabled = false;
            this.Text = Program.GetRoomName();

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();
            //Load Data
            this.LoadAppointmentHead();
  
            GvQuestionaire.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            GvQuestionaire.ColumnHeadersHeight = GvQuestionaire.ColumnHeadersHeight * 2;
            GvQuestionaire.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            dgvAppointments.ClearSelection();
            //this.LoadHistoryCheckup();
            //this.LoadAppointment();
            //this.LoadQuestionaire();
            //this.LoadDiagnosis();
            //this.LoadGraphBasicMeasurement();
            //this.LoadLabAll();
            frmbg.Close();
        }

        #region Function
        

        private void LoadAppointmentHead()
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();

            if (dgvAppointments.Rows.Count > 0)
            {
                dgvAppointments.Rows.Clear();
            }

            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    string SiteCode = Program.CurrentSite.mhs_code;
                    DataTable dt = ws.GetPTAppointment(SiteCode, ddlAppointDate.Value.ToString("yyyy-MM-dd"));

                    if (dt.Rows.Count != 0)
                    {

                        int i = 1;

                        var TmpDT = (from DataRow t1 in dt.Rows
                                     select new
                                     {
                                         No = i++,
                                         HN = t1.Field<string>("PAPMI_No"),
                                         Fullname = t1.Field<string>("Fullname"),
                                         Date = Program.ConvertDateFromServer(t1.Field<DateTime>("AS_Date").ToString(), t1.Field<TimeSpan>("AS_SessStartTime").ToString()),
                                         RowId = t1.Field<string>("APPT_RowId"),
                                         TelNo = t1.Field<string>("PAPER_MobPhone")
                                                + ((t1.Field<string>("PAPER_TelO") != null) ? "," + t1.Field<string>("PAPER_TelO") : "")
                                                + ((t1.Field<string>("PAPER_TelH") != null) ? "," + t1.Field<string>("PAPER_TelH") : "")
                                     }).ToList();
                        for (int a = 0; a < TmpDT.Count; a++)
                        {
                            dgvAppointments.Rows.Add(TmpDT[a].No, TmpDT[a].HN, TmpDT[a].Fullname, TmpDT[a].Date, TmpDT[a].TelNo, false, false, String.Empty, "ลงทะเบียน", TmpDT[a].RowId, 0);
                        }

                        for (int j = 0; j < dgvAppointments.Rows.Count; j++)
                        {
                            var objAp = (from ap in dbc.trn_appoints
                                         where ap.tap_row_id == dgvAppointments.Rows[j].Cells["Colrowid"].Value.ToString()
                                             && ap.tap_appoint_date == Convert.ToDateTime(dgvAppointments.Rows[j].Cells["Colarrivedate"].Value)
                                         select ap).ToList();
                            if (objAp.Count != 0)
                            {
                                dgvAppointments.Rows[j].Cells["Colsms"].Value = objAp[0].tap_contact_sms;
                                dgvAppointments.Rows[j].Cells["Coltel"].Value = objAp[0].tap_contact_tel;
                                dgvAppointments.Rows[j].Cells["ColtapId"].Value = objAp[0].tap_id;
                                dgvAppointments.Rows[j].Cells["Colremark"].Value = objAp[0].tap_remark;
                            }
                            else
                            {
                                dgvAppointments.Rows[j].Cells["Colsms"].Value = false;
                                dgvAppointments.Rows[j].Cells["Coltel"].Value = false;
                                dgvAppointments.Rows[j].Cells["ColtapId"].Value = 0;
                                dgvAppointments.Rows[j].Cells["Colremark"].Value = String.Empty;
                            }

                            dgvAppointments.Rows[j].Cells["Colcall"].Value = "ลงทะเบียน";
                        }
                    }
                }
                frmbg.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearData()
        {
            try
            {
                if (GvHistoryCheckup.Rows.Count > 0)
                {
                    GvHistoryCheckup.DataSource = null;
                }
                if (GvAppointmentSub.Rows.Count > 0) 
                { 
                    GvAppointmentSub.DataSource = null;
                }
                if (GvQuestionaire.Rows.Count > 0)
                {
                    GvQuestionaire.DataSource = null;
                }
                if (GvDiagnosis.Rows.Count > 0) 
                { 
                    GvDiagnosis.DataSource = null;
                }
                if (GvLab.Rows.Count > 0) 
                {
                    GvLab.DataSource = null; 
                } //GvLab.Rows.Clear(); GvLab.Columns.Clear();

                displaychart1.Series["BMI"].Points.Clear();
                displaychart1.Series["BP1(mmHg)"].Points.Clear();
                displaychart1.Series["BP2(mmHg)"].Points.Clear();
                displaychart1.Series["BW(kg)"].Points.Clear();
                displaychart1.Series["RR(/min)"].Points.Clear();
            }
            catch 
            {
                return;
            }
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                if (i == 10)
                {
                    lblMsg.Visible = false;
                    timer1.Enabled = false;
                    
                }
            }
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
                        dt.Rows.Add(j + 1,objRow[j].tpl_lab_no, objRow[j].tpl_lab_name);
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
                                GvHistoryCheckup.Rows.Add(i + 1, objHistory[i].Date, objHistory[i].HCP, objHistory[i].EN, objHistory[i].DocName == null ? "-" : objHistory[i].DocName.ToString());
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

                        GvQuestionaire.DataSource = objQuestion;
                        for (int i = 0; i < objQuestion.Count; i++)
                        {
                            GvQuestionaire.Rows[i].Cells[0].Value = i + 1;
  
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

        //private void LoadQuestionaire(string hn)
        //{
        //    if (hn != String.Empty)
        //    {
                
        //        var objQuestion = (from t1 in dbc.trn_patient_regis
        //                           join t2 in dbc.trn_ques_patients on t1.tpr_id equals t2.tpr_id into t2_i
        //                           from t3 in t2_i.DefaultIfEmpty()
        //                           where t1.tpt_id == dbc.trn_patients.Where(c => c.tpt_hn_no == hn).Single().tpt_id
        //                           select new
        //                           {
        //                               Date = t1.tpr_arrive_date,
        //                               QStatusYes = GetImage(t3.tqp_id == null ? false : true),
        //                               QStatusNo = GetImage(t3.tqp_id == null ? true : false),
        //                               Status = "-",
        //                               Print = "-",
        //                               Usr = "-",
        //                               DTime = "-"
        //                           }).ToList();

        //        GvQuestionaire.DataSource = objQuestion;

        //        for (int i = 0; i < objQuestion.Count; i++)
        //        {
        //            GvQuestionaire.Rows[i].Cells[0].Value = (i + 1);//, objQuestion[i].Date, GetImage(objQuestion[i].QStatusYes), GetImage(objQuestion[i].QStatusNo) , objQuestion[i].Print, objQuestion[i].Usr, objQuestion[i].DTime); 
        //        }
        //    }
        //}

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

        private bool IsFloatOrInt(string value)
        {
            int intValue;
            float floatValue;
            if (value == "0" || value == "0.0")
                return false;
            else
                return Int32.TryParse(value, out intValue) || float.TryParse(value, out floatValue);
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

        private void GvQuestionaire_Paint(object sender, PaintEventArgs e)
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
                    e.Graphics.DrawString(i == 2 ? "Questionaire" : "Print By", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, GvQuestion, format);
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

        private void btnfind_Click(object sender, EventArgs e)
        {
           this.LoadAppointmentHead();
        }

        private void dgvAppointments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }

            string hn = dgvAppointments.Rows[e.RowIndex].Cells["Colhn"].Value.ToString();
            selhn = hn;
            string Fullname = dgvAppointments.Rows[e.RowIndex].Cells["Colname"].Value.ToString();

            string DateAppoint = dgvAppointments.Rows[e.RowIndex].Cells["Colarrivedate"].Value.ToString();

            this.ClearData();

            this.LoadHistoryCheckup(hn); //<-----LoadQuestionaire
            this.LoadAppointment(hn);
            //this.LoadQuestionaire(hn);
            this.LoadDiagnosis(hn);
            this.LoadGraphBasicMeasurement(hn);
            this.LoadLabAll(hn);


            switch (dgvAppointments.Columns[e.ColumnIndex].Name)
            {
                case "Colcall":
                        frmPreRegister frmre = new frmPreRegister();
                        frmre.AppointDate = Convert.ToDateTime(DateAppoint);
                        frmre.HNno = hn;
                        frmre.FullName = Fullname;
                        frmre.ShowDialog();
                   break;
            }
           
        }

        private void btnDiagnosisMore_Click(object sender, EventArgs e)
        {
            frmMoreDetail_Diagnosis frm = new frmMoreDetail_Diagnosis();

            frm.refhn = selhn;

            frm.ShowDialog();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < dgvAppointments.Rows.Count; i++)
                {
                    var objApp = (from t1 in dbc.trn_appoints
                                  where t1.tap_id == Convert.ToInt32(dgvAppointments.Rows[i].Cells["Coltapid"].Value)
                                  && t1.tap_row_id == dgvAppointments.Rows[i].Cells["Colrowid"].Value.ToString()
                                  select t1).ToList();

                    if (objApp != null)
                    {
                        foreach (var data in objApp)
                        {
                            dbc.trn_appoints.DeleteOnSubmit(data);
                        }
                    }
                    //if (objApp.Count != 0)
                    //{
                    //    objApp[0].tap_remark = dgvAppointments.Rows[i].Cells["Remark"].Value.ToString();
                    //    objApp[0].tap_contact = (bool)dgvAppointments.Rows[i].Cells["SMS"].Value == true ? 'S' : 'T';
                    //}
                    //else
                    //{

                    bool sms = (bool)dgvAppointments.Rows[i].Cells["Colsms"].Value;
                    bool tel = (bool)dgvAppointments.Rows[i].Cells["Coltel"].Value;

                    if (sms == true || tel == true)
                    {
                        trn_appoint objNew = new trn_appoint
                        {
                            tap_row_id = dgvAppointments.Rows[i].Cells["Colrowid"].Value.ToString(),
                            tap_appoint_date = Convert.ToDateTime(dgvAppointments.Rows[i].Cells["Colarrivedate"].Value),
                            tap_contact_sms = (bool)sms ,
                            tap_contact_tel = (bool)tel,
                            tap_remark = dgvAppointments.Rows[i].Cells["Colremark"].Value == null ? "" : dgvAppointments.Rows[i].Cells["Colremark"].Value.ToString(),
                            tap_create_by = Program.CurrentUser.mut_username,
                            tap_create_date = Program.GetServerDateTime().Date
                        };
                        dbc.trn_appoints.InsertOnSubmit(objNew);
                    }
                    //}

                }
                dbc.SubmitChanges();
                lblMsg.Visible = true;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
