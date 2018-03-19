using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Web;
using System.IO;
namespace BKvs2010
{
    public partial class frmPFT_Result : Form
    {
        public frmPFT_Result()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        private string strTitle="รายชื่อผู้รับบริการที่ยังไม่ได้พบแพทย์(ทั้งหมด {0} คน)";
        private void frmPFT_Result_Load(object sender, EventArgs e)
        {
            LoadBinding();
        }
        private void LoadBinding()
        {
            var objGridWait= (from t1 in dbc.trn_pfts
                                   from t2 in dbc.mst_user_types
                                   from t3 in dbc.mst_user_types
                                   where t1.tpf_create_by == t2.mut_username
                                   && t1.tpf_doc_code == t3.mut_username
                                   && (t1.tpf_result == false || t1.tpf_result == null)
                                   && (t1.tpf_doc_result == false || t1.tpf_doc_result == null)
                                   select new
                                   {
                                       HNno = t1.trn_patient_regi.trn_patient.tpt_hn_no,
                                       FullName = t1.trn_patient_regi.trn_patient.tpt_othername,
                                       ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                       CreateBy = t2.mut_fullname,
                                       CloseDate = t1.tpf_close_date,
                                       EN = t1.trn_patient_regi.tpr_en_no
                                   }
                                   ).ToList();
            GridResult.DataSource = objGridWait;
            GridResult.Columns["Colen"].Visible = false;
            lbTitleGrid.Text = string.Format(strTitle, objGridWait.Count());
            if (GridResult.Rows.Count == 0 || GridResult.Rows.Count >0)
            {
                btnSave.Enabled = false;
                btnClear.Enabled = false;
                btnSend.Enabled = false;
                btnPreview.Enabled = false;
            }
           
        }

        DocScan docscan = new DocScan();
        //Grid  Event
        private void GridResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    string HNno = Convert1.ToString(GridResult["ColHN", e.RowIndex].Value);
                    string En = Convert1.ToString(GridResult["Colen", e.RowIndex].Value);
                    if (HNno != "")
                    {
                        btnSave.Enabled = true;
                        btnClear.Enabled = true;
                        btnPreview.Enabled = true;

                        var objCurrentHN = (from t1 in dbc.trn_pfts
                                            where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
                                            select t1).FirstOrDefault();
                        PFTbindingSource1.DataSource = objCurrentHN;
                        if (objCurrentHN != null)
                        {
                            int tpr_id = objCurrentHN.tpr_id;
                            UIProfileHorizontal1.Loaddata(tpr_id, objCurrentHN.trn_patient_regi.mhs_id);// Load Profile
                            var objcurrentQuestionPatient = (from t1 in dbc.trn_ques_patients
                                                             where t1.tpr_id == tpr_id
                                                             select t1).FirstOrDefault();
                            QestionPatientbindingSource1.DataSource = objcurrentQuestionPatient;

                            #region WaitingConfirm
                            ////Load image
                            HistoryData.showform = 'N';
                            //docscan.GetHistory("LR102", "", 4434);
                            docscan.GetHistory("LR102", En, tpr_id);
                            pictureBox_result.SizeMode = PictureBoxSizeMode.AutoSize;
                            pictureBox_result.Image = docscan.resizeImage(HistoryData.newImage, new Size(450, 500));

                            if (pictureBox_result.Image == null)
                            {
                                btnSend.Enabled = false;
                                btnPreview.Enabled = false;
                            }
                            else
                            {
                                btnSend.Enabled = true;
                                btnPreview.Enabled = true;
                            }
                            ////EndLoad image

                           // TestGetDocscan.ServiceSoapClient wsSaveDocscan = new TestGetDocscan.ServiceSoapClient();
                            //DataSet ds = new DataSet();
                            //ds = wsSaveDocscan.getDocumentList("0111006591", "O0113450939", "", "VVF", "A1");
                            //pictureBox_result.SizeMode = PictureBoxSizeMode.AutoSize;
                            //pictureBox_result.Image = docscan.resizeImage(HistoryData.newImage, new Size(450, 500));
                            #endregion
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }
        private void GridResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < GridResult.Rows.Count; i++)
            {
                GridResult["ColNo", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData())
                {
                    lbAlertMsg.Text = "Save Data Completed.";
                }
                else
                {
                    lbAlertMsg.Text = "Failed !";
                }
            }
            catch (Exception ex)
            {
                lbAlertMsg.Text = ex.Message;
            }
            lbAlertMsg.Focus();
        }
        private bool SaveData()
        {
            bool isCompleted = false;
            try
            {
                DateTime dtnow = Program.GetServerDateTime();
                trn_pft objcurrentpft = (trn_pft)PFTbindingSource1.Current;
                if (objcurrentpft.tpr_id == 0)
                {
                    objcurrentpft.tpr_id = Program.CurrentRegis.tpr_id;
                }
                if (objcurrentpft.tpf_create_by == null)
                {
                    objcurrentpft.tpf_create_by = Program.CurrentUser.mut_username;
                    objcurrentpft.tpf_create_date = dtnow;
                }
                objcurrentpft.tpf_update_by = Program.CurrentUser.mut_username;
                objcurrentpft.tpf_update_date = dtnow;
                objcurrentpft.tpf_result = true;

                PictureBox p1 = new PictureBox();
                using (Bitmap bitmap = new Bitmap(pictureBox_result.ClientSize.Width, pictureBox_result.ClientSize.Height))
                {
                    pictureBox_result.DrawToBitmap(bitmap, pictureBox_result.ClientRectangle);
                    Bitmap bmp = new Bitmap(bitmap);
                    p1.SizeMode = PictureBoxSizeMode.StretchImage;
                    p1.Image = (Image)bmp;
                }
                MemoryStream stream = new MemoryStream();
                p1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] img1 = stream.ToArray();
                objcurrentpft.tpf_docscan_img = img1;

                trn_ques_patient objqp = (trn_ques_patient)QestionPatientbindingSource1.Current;
                //load เคยมีอาการหรือปัญหาเหล่านี้หรือไม่
                if (objqp.tpr_id == 0)
                {
                    objqp.tpr_id = Program.CurrentRegis.tpr_id;
                }

                objqp.tqp_symp_faint = Program.GetValueRadioTochar(pl_symp_faint);
                objqp.tqp_symp_shake = Program.GetValueRadioTochar(pl_symp_shake);
                objqp.tqp_symp_wind = Program.GetValueRadioTochar(pl_symp_wind);
                objqp.tqp_symp_breath = Program.GetValueRadioTochar(pl_symp_breath);
                objqp.tqp_symp_vein = Program.GetValueRadioTochar(pl_symp_vein);
                objqp.tqp_symp_paralysis = Program.GetValueRadioTochar(pl_symp_paralysis);

                // Load 5
                objqp.tqp_his_smok = Program.GetValueGroupBox(gb_his_smok);
                if (objqp.tqp_his_smok == 'S')
                {
                    objqp.tqp_his_smok_amt = Convert1.ToDouble(txt_his_smok_amt1.Text);
                    objqp.tqp_his_smok_dur = Convert1.ToDouble(txt_his_smok_dur1.Text);
                }
                else if (objqp.tqp_his_smok == 'Q')
                {
                    objqp.tqp_his_smok_amt = Convert1.ToDouble(txt_his_smok_amt2.Text);
                    objqp.tqp_his_smok_dur = Convert1.ToDouble(txt_his_smok_dur2.Text);
                }

                //load 6.1 ไอ
                objqp.tqp_cur_ill_cough = Program.GetValueRadioTochar(pl2_cur_ill_cough);
                objqp.tqp_cur_ill_wcough = Program.GetValueRadioTochar(pl2_cur_ill_wcough);
                objqp.tqp_cur_ill_gcough = Program.GetValueRadioTochar(pl2_cur_ill_gcough);
                objqp.tqp_cur_ill_bcough = Program.GetValueRadioTochar(pl2_cur_ill_bcough);

                //6.2 เหนื่อยหอบ
                objqp.tqp_cur_ill_pant = Program.GetValueRadioTochar(pl3_cur_ill_pant);

                //ความถี่ของการเกิดอาการ
                objqp.tqp_pat_freq = Program.GetValueRadioTochar(pl4_pat_freq);
                if (objqp.tqp_create_by == null)
                {
                    objqp.tqp_create_by = Program.CurrentUser.mut_username;
                    objqp.tqp_create_date = dtnow;
                }
                objqp.tqp_update_by = Program.CurrentUser.mut_username;
                objqp.tqp_update_date = dtnow;
                
                PFTbindingSource1.EndEdit();
                QestionPatientbindingSource1.EndEdit();
                try
                {
                    dbc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                    {
                        dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    dbc.SubmitChanges();
                }
                isCompleted = true;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SaveData", ex, false);
            }
            return isCompleted;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            LoadBinding();
            timer1.Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (SaveData())
                {
                    ////Code Not Confirm
                    trn_pft objcurrentpft = (trn_pft)PFTbindingSource1.Current;
                    objcurrentpft.tpf_type = 'N';
                    objcurrentpft.tpf_doc_result = true;
                    PictureBox p1 = new PictureBox();
                    using (Bitmap bitmap = new Bitmap(pictureBox_result.ClientSize.Width, pictureBox_result.ClientSize.Height))
                    {
                        pictureBox_result.DrawToBitmap(bitmap, pictureBox_result.ClientRectangle);
                        Bitmap bmp = new Bitmap(bitmap);
                        p1.SizeMode = PictureBoxSizeMode.StretchImage;
                        p1.Image = (Image)bmp;
                    }
                    MemoryStream stream = new MemoryStream();
                    p1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] img1 = stream.ToArray(); 
                    objcurrentpft.tpf_docscan_img = img1;
                    dbc.SubmitChanges();
                    lbAlertMsg.Text = "Save Data Completed.";
                }

                string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "LR102", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                //lbAlertMsg.Text = result;

                //if (docscan.SendtoDocscan("LR102", Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                //{
                //    lbAlertMsg.Text = "Save Data Completed.";
                //}
                //else
                //{
                   
                //}
            }
            catch
            {
                return;
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string> { "LR102" };
            int tprID = 0;
            
            if (Program.CurrentRegis != null) 
                tprID = Program.CurrentRegis.tpr_id;

            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.previewReport();
        }

    }
}
