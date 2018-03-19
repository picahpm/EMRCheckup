using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.IO;
namespace CheckupBO
{
    public partial class frmCarotid : Form
    {
        public frmCarotid()
        {
            InitializeComponent();
        }

        private bool Brush = true;
        ToolPaint.Shapes DrawingShapes = new ToolPaint.Shapes();
        private bool IsPainting = false;
        private bool IsEraseing = false;
        private Point LastPos = new Point(0, 0);
        private Color CurrentColour = Color.Black;
        private float CurrentWidth = 10;
        private int ShapeNum = 0;
        private Point MouseLoc = new Point(0, 0);
        private bool IsMouseing = false;
        private bool Brush2 = true;
        //LT
        ToolPaint.Shapes DrawingShapes2 = new ToolPaint.Shapes();
        private bool IsPainting2 = false;
        private bool IsEraseing2 = false;
        private Point LastPos2 = new Point(0, 0);
        private Color CurrentColour2 = Color.Black;
        private float CurrentWidth2 = 10;
        private int ShapeNum2 = 0, count;
        private Point MouseLoc2 = new Point(0, 0);
        object objimage_L;
        object objimage_R;
        string strTagID_L, strTagID_R;
        DateTime? dt_appoint;
        DateTime? dt_followup;
        private bool IsRemark_L = true;
        private bool IsRemark_R = true;
        DataTable dt = new DataTable();
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        

        public int TprID;
        private void frmCarotid_Load(object sender, EventArgs e)
        {
            this.LoadData(TprID);
            LoadLabResult(TprID);
        }
        private void LoadData(int tpr_id)
        {
            trn_carotid_tech obj = dbc.trn_carotid_teches.Where(c => c.tpr_id == tpr_id).FirstOrDefault();
            if (obj != null)
            {
                bindingSourceTrn_carotid_Tech.DataSource = obj;
                trn_carotid_tech trnCarotid_tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                objimage_L = trnCarotid_tech.tct_left_result.ToArray();
                objimage_R = trnCarotid_tech.tct_right_result.ToArray();
                byte[] data;
                byte[] data2;
                data = (byte[])objimage_L;
                data2 = (byte[])objimage_R;
                MemoryStream ms = new MemoryStream(data);
                MemoryStream ms2 = new MemoryStream(data2);
                pictureBoxLeft.Image = Image.FromStream(ms);
                pictureBoxRight.Image = Image.FromStream(ms2);
                Program.SetValueRadioGroup(pnlDuplex, trnCarotid_tech.tct_finding_ca_duplex.ToString());
                cmblist1.SelectedItem = trnCarotid_tech.tct_appoint_depart;
                Program.SetValueRadioGroup(pnlStatusCall, trnCarotid_tech.tct_call_status.ToString());
                Program.SetValueRadioGroup(pnlsummary, trnCarotid_tech.tct_summary.ToString());
                txtDoctorCode.Text = trnCarotid_tech.tct_doctor_code;
                txtDoctorName.Text = trnCarotid_tech.tct_doctor_name;
                string Str_Appoint_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_appoint_doctor_date.ToString()) ? "" : Convert.ToDateTime(trnCarotid_tech.tct_appoint_doctor_date).ToString();
                string Str_Followup_Date = string.IsNullOrEmpty(trnCarotid_tech.tct_follow_up.ToString()) ? "" : Convert.ToDateTime(trnCarotid_tech.tct_follow_up).ToString();
                dateTimePicker1.Text = Str_Appoint_Date;
                dateTimeFollowup.Text = Str_Followup_Date;
                txtsumaryRemark.Text = trnCarotid_tech.tct_summary_remark;
                if (trnCarotid_tech.tct_appoint_doctor == true)
                {
                    chkappoint.Checked = true;
                }
                if (trnCarotid_tech.tct_advice_med == true)
                {
                    chkAdviceMec.Checked = true;
                    txtAdviceMec.Text = trnCarotid_tech.tct_advice_med_rmk;
                }
                if (trnCarotid_tech.tct_advice_dit == true)
                {
                    chkAdviceDiet.Checked = true;
                    txtAdviceDiet.Text = trnCarotid_tech.tct_advice_dit_rmk;
                }
                if (trnCarotid_tech.tct_advice_exr == true)
                {
                    chkAdviceExercise.Checked = true;
                    txtAdviceExercise.Text = trnCarotid_tech.tct_advice_exr_rmk;
                }
                if (trnCarotid_tech.tct_consult_card == true)
                {
                    chkConsult.Checked = true;
                    txtconsult.Text = trnCarotid_tech.tct_consult_card_rmk;
                }
                if (trnCarotid_tech.tct_fu_carotid == true)
                {
                    chkfu.Checked = true;
                    txtfu.Text = trnCarotid_tech.tct_fu_carotid_rmk;
                }
                if (trnCarotid_tech.tct_prn == true)
                {
                    chk_prn.Checked = true;
                }
                txtremark.Text = trnCarotid_tech.tct_other_remark;
                Program.SetValueRadioGroup(pnlAppointStatus, trnCarotid_tech.tct_appoint_status.ToString());
            }
            else
            {
                bindingSourceTrn_carotid_Tech.DataSource = (from t1 in dbc.trn_carotid_teches select t1);
                bindingSourceTrn_carotid_Tech.AddNew();
                pictureBoxLeft.Image = Properties.Resources.carotid_2;
                pictureBoxRight.Image = Properties.Resources.carotid_1;
            }
        }
        private void LoadLabResult(int tpr_id)
        {
            var ObjLabFbs = (from t1 in dbc.trn_patient_ass_dtls
                             join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                             join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                             where t1.tped_lab_code == "C0180" && t3.tpr_id == tpr_id//tprid
                             select t1.tped_lab_value).FirstOrDefault();

            var ObjLabCholes = (from t1 in dbc.trn_patient_ass_dtls
                                join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                                join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                                where t1.tped_lab_code == "C0150" && t3.tpr_id == tpr_id//tprid
                                select t1.tped_lab_value).FirstOrDefault();

            var ObjLabBmi = (from t1 in dbc.trn_basic_measure_dtls
                             join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                             where t2.tpr_id == tpr_id//tprid
                             select t1.tbd_bmi).FirstOrDefault();

            var ObjLabHbA = (from t1 in dbc.trn_patient_ass_dtls
                             join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                             join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                             where t1.tped_lab_code == "N0510" && t3.tpr_id == tpr_id
                             select t1.tped_lab_value).FirstOrDefault();

            var ObjLabLdl = (from t1 in dbc.trn_patient_ass_dtls
                             join t2 in dbc.trn_patient_ass_hdrs on t1.tpeh_id equals t2.tpeh_id
                             join t3 in dbc.trn_patient_ass_grps on t2.tpeg_id equals t3.tpeg_id
                             where t1.tped_lab_code == "C0159" && t3.tpr_id == tpr_id
                             select t1.tped_lab_value).FirstOrDefault();

            var ObjLabBp = (from t1 in dbc.trn_basic_measure_dtls
                            join t2 in dbc.trn_basic_measure_hdrs on t1.tbm_id equals t2.tbm_id
                            where t2.tpr_id == tpr_id
                            select t1.tbd_systolic + "/" + t1.tbd_diastolic).FirstOrDefault();

            var ObjMobile = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tpr_id select t1.tpr_mobile_phone).FirstOrDefault();
            txtfbs.Text = ObjLabFbs;
            txtcho.Text = ObjLabCholes;
            txtbmi.Text = ObjLabBmi;
            txthb.Text = ObjLabHbA;
            txtldl.Text = ObjLabLdl;
            txtbp.Text = ObjLabBp;
            txtmobilephone.Text = ObjMobile;
        }
        private bool Save(char type)
        {
            trn_carotid_tech Carotid_Tech = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
            Boolean saveIsCompleted = false;
            DateTime datenowvalue = Program.GetServerDateTime();
            Carotid_Tech.tpr_id = TprID;
            Carotid_Tech.tct_type = type;
            Carotid_Tech.tct_result = false;
            PictureBox p1 = new PictureBox();
            PictureBox p2 = new PictureBox();
            using (Bitmap bitmap = new Bitmap(pictureBoxLeft.ClientSize.Width, pictureBoxLeft.ClientSize.Height))
            {
                pictureBoxLeft.DrawToBitmap(bitmap, pictureBoxLeft.ClientRectangle);
                Bitmap bmp = new Bitmap(bitmap);
                p1.SizeMode = PictureBoxSizeMode.StretchImage;
                p1.Image = (Image)bmp;
            }
            using (Bitmap bitmap = new Bitmap(pictureBoxRight.ClientSize.Width, pictureBoxRight.ClientSize.Height))
            {
                pictureBoxRight.DrawToBitmap(bitmap, pictureBoxRight.ClientRectangle);
                Bitmap bmp = new Bitmap(bitmap);
                p2.SizeMode = PictureBoxSizeMode.StretchImage;
                p2.Image = (Image)bmp;
            }
            MemoryStream stream_L = new MemoryStream();
            MemoryStream stream_R = new MemoryStream();
            p1.Image.Save(stream_L, System.Drawing.Imaging.ImageFormat.Jpeg);
            p2.Image.Save(stream_R, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] pic_L = stream_L.ToArray();
            byte[] pic_R = stream_R.ToArray();
            Carotid_Tech.tct_left_result = pic_L;
            Carotid_Tech.tct_right_result = pic_R;
            Carotid_Tech.tct_finding_ca_duplex = Program.GetValueRadio(pnlDuplex);
            if (rdresult1.Checked == true)
            {
                Carotid_Tech.tct_appoint_depart = cmblist1.SelectedItem.ToString();
            }
            else
            {
                Carotid_Tech.tct_appoint_depart = DBNull.Value.ToString();
            }
            Carotid_Tech.tct_call_status = Program.GetValueRadioTochar(pnlStatusCall);
            Carotid_Tech.tct_appoint_doctor = (chkappoint.Checked) ? true : false;
            Carotid_Tech.tct_appoint_doctor_date = dt_appoint;
            Carotid_Tech.tct_doctor_code = txtDoctorCode.Text;
            Carotid_Tech.tct_doctor_name = txtDoctorName.Text;
            Carotid_Tech.tct_appoint_status = Program.GetValueRadio(pnlAppointStatus);
            Carotid_Tech.tct_doctor_code = txtDoctorCode.Text;
            Carotid_Tech.tct_doctor_name = txtDoctorName.Text;
            Carotid_Tech.tct_summary = Program.GetValueRadioTochar(pnlsummary);
            Carotid_Tech.tct_summary_remark = txtsumaryRemark.Text;
            Carotid_Tech.tct_advice_med = (chkAdviceMec.Checked) ? true : false;
            Carotid_Tech.tct_advice_med_rmk = txtAdviceMec.Text;
            Carotid_Tech.tct_advice_dit = (chkAdviceDiet.Checked) ? true : false;
            Carotid_Tech.tct_advice_dit_rmk = txtAdviceDiet.Text;
            Carotid_Tech.tct_advice_exr = (chkAdviceExercise.Checked) ? true : false;
            Carotid_Tech.tct_advice_exr_rmk = txtAdviceExercise.Text;
            Carotid_Tech.tct_consult_card = (chkConsult.Checked) ? true : false;
            Carotid_Tech.tct_consult_card_rmk = txtconsult.Text;
            Carotid_Tech.tct_fu_carotid = (chkfu.Checked) ? true : false;
            Carotid_Tech.tct_fu_carotid_rmk = txtfu.Text;
            Carotid_Tech.tct_other_remark = txtremark.Text;
            Carotid_Tech.tct_follow_up = dt_followup;
            Carotid_Tech.tct_prn = (chk_prn.Checked) ? true : false;
            if (Carotid_Tech.tct_create_by == null)
            {
                Carotid_Tech.tct_create_by = Program.CurrentUser.mut_username;
                Carotid_Tech.tct_create_date = datenowvalue;
            }
            Carotid_Tech.tct_update_by = Program.CurrentUser.mut_username;
            Carotid_Tech.tct_update_date = Carotid_Tech.tct_create_date;
            bindingSourceTrn_carotid_Tech.EndEdit();
            dbc.SubmitChanges();
            saveIsCompleted = true;
            if (saveIsCompleted == true)
            {
                var tctid = (from q in dbc.trn_carotid_teches select q.tct_id).Max();
            }
            return saveIsCompleted;
        }

        private void chkappoint_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkappoint.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd/MM/yyyy";
                dt_appoint = Convert.ToDateTime(dateTimePicker1.Value.ToShortDateString());
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
                dt_appoint = null;
            }
        }
        private void pictureBoxRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (Brush2)
            {
                IsPainting2 = true;
                ShapeNum2++;
                LastPos2 = new Point(0, 0);
            }
            else
            {
                IsEraseing2 = true;
            }
        }

        private void pictureBoxRight_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLoc2 = e.Location;
            if (IsPainting2)
            {
                if (LastPos2 != e.Location)
                {
                    LastPos2 = e.Location;
                    DrawingShapes2.NewShape(LastPos2, CurrentWidth2, CurrentColour2, ShapeNum2);
                }
            }
            if (IsEraseing2)
            {
                DrawingShapes2.RemoveShape(e.Location, 10);
            }
            pictureBoxRight.Refresh();
        }

        private void pictureBoxRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsPainting2)
            {
                IsPainting2 = false;
            }
            if (IsEraseing2)
            {
                IsEraseing2 = false;
            }
        }

        private void pictureBoxRight_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < DrawingShapes2.NumberOfShapes() - 1; i++)
            {
                ToolPaint.Shape T = DrawingShapes2.GetShape(i);
                ToolPaint.Shape T1 = DrawingShapes2.GetShape(i + 1);
                if (T.ShapeNumber == T1.ShapeNumber)
                {
                    Pen p = new Pen(T.Colour, T.Width);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(p, T.Location, T1.Location);
                    p.Dispose();
                }
            }
            if (IsMouseing)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 0.5f), MouseLoc2.X - (CurrentWidth2 / 2), MouseLoc2.Y - (CurrentWidth2 / 2), CurrentWidth2, CurrentWidth2);
            }
        }

        private void pictureBoxRight_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
            IsMouseing = true;
        }

        private void pictureBoxRight_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            IsMouseing = false;
            pictureBoxRight.Refresh();
        }

        private void pictureBoxLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (Brush)
            {
                IsPainting = true;
                ShapeNum++;
                LastPos = new Point(0, 0);
            }
            else
            {
                IsEraseing = true;
            }
        }

        private void pictureBoxLeft_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
            IsMouseing = true;
        }

        private void pictureBoxLeft_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            IsMouseing = false;
            pictureBoxLeft.Refresh();
        }

        private void pictureBoxLeft_MouseMove(object sender, MouseEventArgs e)
        {
            MouseLoc = e.Location;
            if (IsPainting)
            {
                if (LastPos != e.Location)
                {
                    LastPos = e.Location;
                    DrawingShapes.NewShape(LastPos, CurrentWidth, CurrentColour, ShapeNum);
                }
            }
            if (IsEraseing)
            {
                DrawingShapes.RemoveShape(e.Location, 10);
            }
            pictureBoxLeft.Refresh();
        }

        private void pictureBoxLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsPainting)
            {
                IsPainting = false;
            }
            if (IsEraseing)
            {
                IsEraseing = false;
            }
        }

        private void pictureBoxLeft_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < DrawingShapes.NumberOfShapes() - 1; i++)
            {
                ToolPaint.Shape T = DrawingShapes.GetShape(i);
                ToolPaint.Shape T1 = DrawingShapes.GetShape(i + 1);
                if (T.ShapeNumber == T1.ShapeNumber)
                {
                    Pen p = new Pen(T.Colour, T.Width);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(p, T.Location, T1.Location);
                    p.Dispose();
                }
            }
            if (IsMouseing)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 0.5f), MouseLoc.X - (CurrentWidth / 2), MouseLoc.Y - (CurrentWidth / 2), CurrentWidth, CurrentWidth);
            }
        }
        private void chk_prn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_prn.Checked == true)
            {
                dateTimeFollowup.Enabled = true;
                dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                dateTimeFollowup.CustomFormat = "dd/MM/yyyy";
                dt_followup = Convert.ToDateTime(dateTimeFollowup.Value.ToShortDateString());
            }
            else
            {
                dateTimeFollowup.Enabled = false;
                dateTimeFollowup.Format = DateTimePickerFormat.Custom;
                dateTimeFollowup.CustomFormat = " ";
                dt_followup = null;
            }
        }

        private void btnSaveAsDraft_Click(object sender, EventArgs e)
        {
            try
            {
                if (Save('D'))
                {
                    lbAlertMsg.Text = "Save data completed.";
                }
            }
            catch(Exception ex)
            {
                Program.MessageError(ex.Message);
            }
        }

        private void btnBrushErase_Click(object sender, EventArgs e)
        {
            Brush = !Brush;
            Brush2 = !Brush2;
            if (Brush == true & Brush2 == true)
            {
                lblstatusPaint.Text = "Brush";
            }
            else
            {
                lblstatusPaint.Text = "Erase";
            }
        }

        private void btncolor_Click(object sender, EventArgs e)
        {
            DialogResult D = colorDialog1.ShowDialog();
            if (D == DialogResult.OK)
            {
                CurrentColour = colorDialog1.Color;
                CurrentColour2 = colorDialog1.Color;
            }
        }

        private void btnClearall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DrawingShapes = new ToolPaint.Shapes();
                DrawingShapes2 = new ToolPaint.Shapes();
                pictureBoxRight.Refresh();
                pictureBoxLeft.Refresh();
                pictureBoxLeft.Image = Properties.Resources.carotid_2;
                pictureBoxRight.Image = Properties.Resources.carotid_1;
                clrPicture(pictureBoxLeft);
                clrPicture(pictureBoxRight);
            }
        }
        private void clrPicture(PictureBox pic)
        {
            foreach (Control ctl in pic.Controls)
            {
                Label lbl;
                if (ctl is Label)
                {
                    lbl = (Label)ctl;
                    lbl.Visible = false;
                }
            }
        }
        private void clrGroupbox(GroupBox gb)
        {
            foreach (Control ctl in gb.Controls)
            {
                TextBox txt;
                if (ctl is TextBox)
                {
                    txt = (TextBox)ctl;
                    txt.Text = "";
                }
            }
        }

        private void btnremark_Click(object sender, EventArgs e)
        {
            IsRemark_L = !IsRemark_L;
            IsRemark_R = !IsRemark_R;
            if (IsRemark_L == true && IsRemark_R == true)
            {
                lblS_remark.Text = "Remark";
            }
            else
            {
                lblS_remark.Text = "Null";
            }
        }

        private void btnGet_hst_Click(object sender, EventArgs e)
        {
            LoadHistory(TprID);
        }
        private void LoadHistory(int tpr_id)
        {
            var ObjectHistory = (from t1 in dbc.trn_carotid_teches
                                 where t1.tpr_id == tpr_id
                                 select new
                                 {
                                     EN = t1.trn_patient_regi.tpr_en_no,
                                     ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                     CreateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tct_create_by).Single().mut_fullname,
                                     CreateDate = t1.tct_create_date,
                                     UpdateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.tct_create_by).Single().mut_fullname,
                                     UpdateDate = t1.tct_create_date,
                                     Link = "Link"
                                 }).ToList();
            Gv_History.DataSource = ObjectHistory;
        }

        private void rdresult1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdresult1.Checked == true)
            {
                cmblist1.Enabled = true;
                cmblist1.SelectedIndex = 0;
            }
            else
            {
                cmblist1.Enabled = false;
                cmblist1.SelectedIndex = -1;
            }
        }

        private void txtDoctorName_KeyUp(object sender, KeyEventArgs e)
        {
            GridDoctorName.Location = new Point(132, 277);
            //GridDoctorName. = 126;
            SearchGetDoctor(txtDoctorName.Text.Trim());
        }
        private void SearchGetDoctor(string strSearch)
        {
            //if (Program.UseWebService == false) { return; }
            try
            {
                if (strSearch.Length >= 2)
                {
                    GridDoctorName.Visible = true;
                    Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
                    DataTable dt = ws.GetCareprovider(strSearch);
                    GridDoctorName.DataSource = dt;
                    GridDoctorName.Columns[0].Visible = false;
                    GridDoctorName.Columns[1].Visible = false;
                    GridDoctorName.Columns[2].Visible = false;
                    GridDoctorName.Columns[3].Visible = false;
                    GridDoctorName.Columns[4].Visible = true;
                    GridDoctorName.Columns[4].HeaderText = "Doctor Name";
                    GridDoctorName.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    GridDoctorName.Visible = true;
                }
                else
                {
                    GridDoctorName.Visible = false;
                }
            }
            catch (Exception)
            {
                // Program.MessageError("=>SearchGetDoctor :" + ex.Message);
            }
        }

        private void txtDoctorName_Leave(object sender, EventArgs e)
        {
            GridDoctorName.Visible = false;
            if (txtDoctorName.Text == "")
            {
                txtDoctorCode.Text = "";
            }
        }

        private void rdnormal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdnormal.Checked == true)
            {
                txtsumaryRemark.Enabled = true;
                txtsumaryRemark.Focus();
            }
        }

        private void rdabnormal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdabnormal.Checked == true)
            {
                txtsumaryRemark.Enabled = true;
                txtsumaryRemark.Focus();
            }
        }

        private void chkAdviceMec_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdviceMec.Checked == true)
            {
                txtAdviceMec.Enabled = true;
                txtAdviceMec.Focus();
            }
            else
            {
                txtAdviceMec.Enabled = false;
                txtAdviceMec.Text = null;
            }
        }

        private void chkAdviceDiet_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdviceDiet.Checked == true)
            {
                txtAdviceDiet.Enabled = true;
                txtAdviceDiet.Focus();
            }
            else
            {
                txtAdviceDiet.Enabled = false;
                txtAdviceDiet.Text = null;
            }
        }

        private void chkAdviceExercise_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdviceExercise.Checked == true)
            {
                txtAdviceExercise.Enabled = true;
                txtAdviceExercise.Focus();
            }
            else
            {
                txtAdviceExercise.Enabled = false;
                txtAdviceExercise.Text = null;
            }
        }

        private void chkConsult_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkConsult.Checked == true)
            {
                txtconsult.Enabled = true;
                txtconsult.Focus();
            }
            else
            {
                txtconsult.Enabled = false;
                txtconsult.Text = null;
            }
        }

        private void chkfu_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkfu.Checked == true)
            {
                txtfu.Enabled = true;
                txtfu.Focus();
            }
            else
            {
                txtfu.Enabled = false;
                txtfu.Text = null;
            }
        }

        private void dateTimeFollowup_ValueChanged(object sender, EventArgs e)
        {
            dt_followup = Convert.ToDateTime(dateTimeFollowup.Value.ToShortDateString());
        }

        private void pictureBoxLeft_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsRemark_L == true && Brush == true)
            {
                RemarkOnImgage(this.pictureBoxLeft, e, "L");
            }
        }

        private void pictureBoxRight_MouseClick(object sender, MouseEventArgs e)
        {
            if (IsRemark_R == true && Brush2 == true)
            {
                RemarkOnImgage(this.pictureBoxRight, e, "R");
            }
        }
        private void RemarkOnImgage(PictureBox pb, MouseEventArgs e, string side)
        {
            count++;
            frmPopupRemark frm = new frmPopupRemark();
            frm.ShowDialog();
            Label lbl = new Label();
            lbl = new Label();
            lbl.Name = "l1" + count;
            lbl.BackColor = Color.MintCream;
            lbl.Left = Convert.ToInt32(e.X);
            lbl.Top = Convert.ToInt32(e.Y);
            lbl.AutoSize = true;
            lbl.Cursor = Cursors.Hand;
            lbl.Width = 20;
            lbl.Height = 10;
            lbl.Text = frm.strTextValue;
            lbl.Tag = lbl.Name;
            pb.Controls.Add(lbl);
            if (side == "L")
            {
                lbl.MouseClick += new MouseEventHandler(lblremark_L_Click);
            }
            if (side == "R")
            {
                lbl.MouseClick += new MouseEventHandler(lblremark_R_Click);
            }
        }
        private void lblremark_L_Click(object sender, EventArgs e)
        {
            Label itemdata = (Label)sender;
            strTagID_L = itemdata.Tag.ToString();
            pictureBoxLeft.Controls.RemoveByKey(strTagID_L);
        }

        private void lblremark_R_Click(object sender, EventArgs e)
        {
            Label itemdata = (Label)sender;
            strTagID_R = itemdata.Tag.ToString();
            pictureBoxRight.Controls.RemoveByKey(strTagID_R);
        }

        private void pictureBoxRight_MouseLeave_1(object sender, EventArgs e)
        {
            Cursor.Show();
            IsMouseing = false;
            pictureBoxRight.Refresh();
        }

        private void pictureBoxRight_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < DrawingShapes2.NumberOfShapes() - 1; i++)
            {
                ToolPaint.Shape T = DrawingShapes2.GetShape(i);
                ToolPaint.Shape T1 = DrawingShapes2.GetShape(i + 1);
                if (T.ShapeNumber == T1.ShapeNumber)
                {
                    Pen p = new Pen(T.Colour, T.Width);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(p, T.Location, T1.Location);
                    p.Dispose();
                }
            }
            if (IsMouseing)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 0.5f), MouseLoc2.X - (CurrentWidth2 / 2), MouseLoc2.Y - (CurrentWidth2 / 2), CurrentWidth2, CurrentWidth2);
            }
        }

        private void pictureBoxLeft_Paint_1(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < DrawingShapes.NumberOfShapes() - 1; i++)
            {
                ToolPaint.Shape T = DrawingShapes.GetShape(i);
                ToolPaint.Shape T1 = DrawingShapes.GetShape(i + 1);
                if (T.ShapeNumber == T1.ShapeNumber)
                {
                    Pen p = new Pen(T.Colour, T.Width);
                    p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                    p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                    e.Graphics.DrawLine(p, T.Location, T1.Location);
                    p.Dispose();
                }
            }
            if (IsMouseing)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Black, 0.5f), MouseLoc.X - (CurrentWidth / 2), MouseLoc.Y - (CurrentWidth / 2), CurrentWidth, CurrentWidth);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            CurrentWidth = Convert.ToSingle(numericUpDown1.Value);
            CurrentWidth2 = Convert.ToSingle(numericUpDown1.Value);
        }

        private void GridDoctorName_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var selectedItems = GridDoctorName.CurrentRow;
                trn_carotid_tech objcurrenttpr = (trn_carotid_tech)bindingSourceTrn_carotid_Tech.Current;
                objcurrenttpr.tct_doctor_code = selectedItems.Cells[1].Value.ToString();
                objcurrenttpr.tct_doctor_name = selectedItems.Cells[0].Value.ToString();
                txtDoctorName.Text = objcurrenttpr.tct_doctor_name;
                txtDoctorCode.Text = objcurrenttpr.tct_doctor_code;
                txtDoctorName.Focus();
            }
            catch (Exception)
            {
            }
            GridDoctorName.Visible = false;
        }

        private void Gv_History_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < Gv_History.Rows.Count; i++)
            {
                Gv_History.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }


    }
}
