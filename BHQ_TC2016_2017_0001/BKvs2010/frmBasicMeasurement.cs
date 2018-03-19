using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.Class;

namespace BKvs2010
{
    public partial class frmBasicMeasurement : Form
    {
        public frmBasicMeasurement()
        {
            InitializeComponent();
            uiAllLeft1.OnRefreshStatusED += new Usercontrols.UIAllLeft.RefreshStatusED(uiAllLeft1_OnRefreshStatusED);
            uiFooter1.OnFooternameClick += new Usercontrols.UIFooter.FooterNameClick(OnUCFooterClicked);
        }
        public int SetTprID { get; set; }
        public int siteitem { get; set; }
        private void OnUCFooterClicked(int tprid, int mhs_id)
        {
            // Handle event Footer from here
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            siteitem = mhs_id;
            SetTprID = tprid;

            this.LoadData(tprid);
            //LoadPatientData(tprid);
            btnReady.Enabled = false;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;
            btnSendManual.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
            btnRetrieveVitalSign.Enabled = false;
            uiAllLeft1.LoadDataAll(tprid);
            //uiFooter1.LoadData();
            // ให้สิทธิผู้สร้างมีสิทธิแก้ไข

            btnSaveDraft.Enabled = true;
            //trn_basic_measure_hdr BasicMensureCurrent = (trn_basic_measure_hdr)BStrn_basic_measure_hdr.Current;
            //if (BasicMensureCurrent.tbm_create_by == Program.CurrentUser.mut_username)
            //{
            //    btnSaveDraft.Enabled = true;
            //}
            //else
            //{
            //    btnSaveDraft.Enabled = false;
            //}

            frmbg.Close();
        }




        private void uiAllLeft1_OnRefreshStatusED()
        {
            StatusWaitCallQueue();
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();

        private void frmBasicMeasurement_Load(object sender, EventArgs e)
        {
            RDstatusonArrivalWalk.Focus();
            this.Text = Program.GetRoomName();
            uiFooter1.RoomCode = "BM";
            Loadfrm();
            //uiFooter1.LoadData();
        }
        public void Loadfrm()
        {
            if (SetTprID > 0)
            {
                OnUCFooterClicked(SetTprID, siteitem); return;
            }

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();
            LoadHandlerCountDown();

            if (!Program.IsDummy)
            {
                LoadHistoryCallLast();
                //StatusCallQueueWaitingReady();
                LoadUI();
            }

            if (Program.CurrentRegis != null)
            {
                this.LoadData(Program.CurrentRegis.tpr_id);
                //LoadPatientData(Program.CurrentRegis.tpr_id);
                btnRetrieveVitalSign_Click(null, null);
            }
            else
            {
                if (Program.IsDummy)
                {
                    btnCallQueue.Enabled = false;
                    btnHold.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSendManual.Enabled = false;
                    btnSaveDraft.Enabled = false;
                    btnSendAuto.Enabled = false;
                    btnRetrieveVitalSign.Enabled = false;
                    this.LoadUI();
                }
                else
                {
                    StatusWaitCallQueue();
                }
            }
            frmbg.Close();
        }
        private void LoadHistoryCallLast()
        {
            if (SetTprID != 0)
            {
                //trn_patient_queue objQtxp = (from t1 in dbc.trn_patient_queues
                //                             where t1.mrm_id == Program.CurrentRoom.mrm_id
                //                             && t1.tpr_id == SetTprID
                //                             select t1).FirstOrDefault();
                //Program.CurrentPatient_queue = objQtxp;
                //Program.CurrentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == SetTprID select t1).FirstOrDefault();
            }
            else
            {
                DateTime dtnow = Program.GetServerDateTime();
                //trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
                //                          where t1.mrd_id == Program.CurrentRoom.mrd_id
                //                                && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                //                               && t1.tps_status == "WK" || (t1.tps_ns_status == "NS" && t1.tps_status == "WR")
                //                               && t1.tps_ns_status == null

                //                          orderby t1.tps_create_date ascending
                //                          select t1).FirstOrDefault();
                trn_patient_queue objQ = (from t1 in dbc.trn_patient_queues
                                          where t1.mrd_id == Program.CurrentRoom.mrd_id
                                                && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                               && t1.tps_status == "WK"
                                               && t1.tps_ns_status == null
                                          orderby t1.tps_create_date ascending
                                          select t1).FirstOrDefault();
                if (objQ != null)
                {
                    Program.CurrentPatient_queue = objQ;
                    Program.CurrentRegis = objQ.trn_patient_regi;
                    uiMenuBar1.LoadEnableQuestionare();
                    //btnReady.Enabled = false;
                    StatusCallQueueReady();
                }
                else
                {
                    trn_patient_queue objReady = (from t1 in dbc.trn_patient_queues
                                                  where t1.mrd_id == Program.CurrentRoom.mrd_id
                                                        && t1.trn_patient_regi.tpr_arrive_date.Value.Date == dtnow.Date
                                                       && t1.tps_status == "NS"
                                                       && t1.tps_ns_status == "WR"
                                                  orderby t1.tps_create_date ascending
                                                  select t1).FirstOrDefault();
                    if (objReady != null)
                    {
                        Program.CurrentPatient_queue = objReady;
                        Program.CurrentRegis = objReady.trn_patient_regi;
                        uiMenuBar1.LoadEnableQuestionare();
                        //btnReady.Enabled = true;
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        Program.CurrentPatient_queue = null;
                        Program.CurrentRegis = null;
                    }
                }
            }
        }

        private void LoadPatientData(int tpr_id)
        {
            trn_patient_regi patient_regis = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
            if (patient_regis != null)
            {
                trn_basic_measure_hdr patient_basic_hdr = patient_regis.trn_basic_measure_hdrs.OrderByDescending(x => x.tbm_create_date).FirstOrDefault();
                if (patient_basic_hdr == null)
                {
                    patient_basic_hdr = new trn_basic_measure_hdr();
                    patient_regis.trn_basic_measure_hdrs.Add(patient_basic_hdr);
                }
                List<trn_basic_measure_dtl> patient_basic_dtl = patient_basic_hdr.trn_basic_measure_dtls.ToList();
                if (patient_basic_dtl.Count() == 0)
                {
                    patient_basic_dtl = new List<trn_basic_measure_dtl> { new trn_basic_measure_dtl() };
                    patient_basic_hdr.trn_basic_measure_dtls.Add(patient_basic_dtl.FirstOrDefault());
                }
                BStrn_basic_measure_hdr.DataSource = patient_basic_hdr;
            }
        }
        private void LoadData(int tpr_id)
        {
            trn_patient_regi patient_regis = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
            if (patient_regis != null)
            {
                trn_basic_measure_hdr patient_basic_hdr = patient_regis.trn_basic_measure_hdrs.FirstOrDefault();
                if (patient_basic_hdr == null)
                {
                    patient_basic_hdr = new trn_basic_measure_hdr();
                    patient_regis.trn_basic_measure_hdrs.Add(patient_basic_hdr);
                }
                else
                {
                    Program.SetValueRadioGroup(GBStatusOnArrival, patient_basic_hdr.tbm_arrive);
                    Program.SetValueRadioGroup(GBPurpose, patient_basic_hdr.tbm_purpose.ToString());
                    Program.SetValueRadioGroup(GBGeneralAppearance, patient_basic_hdr.tbm_appearance);
                    Program.SetValueRadioGroup(GBFallPrecaution, patient_basic_hdr.tbm_precaution);
                    Program.SetValueRadioGroup(GBEyeGlasseslen, patient_basic_hdr.tbm_glass_or_contact);
                    Program.SetValueRadioGroup(GBColorBindness, patient_basic_hdr.tbm_color_blind);
                    Program.SetValueRadioGroup(pnlTriage, patient_basic_hdr.tbm_triage);
                    Program.SetValueRadioGroup(pnl_color_vision_isihara_result, patient_basic_hdr.tbm_color_vision_ishihara_result);
                }
                trn_basic_measure_dtl patient_basic_dtl = patient_basic_hdr.trn_basic_measure_dtls.FirstOrDefault();
                if (patient_basic_dtl == null)
                {
                    patient_basic_dtl = new trn_basic_measure_dtl();
                    patient_basic_hdr.trn_basic_measure_dtls.Add(patient_basic_dtl);
                }
                BStrn_basic_measure_hdr.DataSource = patient_basic_hdr;
                BStrn_basic_measure_dtl.DataSource = patient_basic_dtl;
            }

            //trn_basic_measure_hdr objhdr = dbc.trn_basic_measure_hdrs.Where(c => c.tpr_id ==tpr_id).OrderByDescending(x => x.tbm_create_date).FirstOrDefault();
            ////trn_basic_measure_dtl objdtl = dbc.trn_basic_measure_dtls.Where(c => c.tbm_id == objhdr.tbm_id).FirstOrDefault();
            //if (objhdr != null)
            //{
            //    BStrn_basic_measure_hdr.DataSource = objhdr;
            //    trn_basic_measure_hdr BasicMensureCurrent = (trn_basic_measure_hdr)BStrn_basic_measure_hdr.Current;
            //    // set Radio

            //    Program.SetValueRadioGroup(GBStatusOnArrival, BasicMensureCurrent.tbm_arrive.ToString());
            //    Program.SetValueRadioGroup(GBPurpose, BasicMensureCurrent.tbm_purpose.ToString());
            //    Program.SetValueRadioGroup(GBGeneralAppearance, BasicMensureCurrent.tbm_appearance.ToString());
            //    Program.SetValueRadioGroup(GBFallPrecaution, BasicMensureCurrent.tbm_precaution.ToString());
            //    Program.SetValueRadioGroup(GBEyeGlasseslen, BasicMensureCurrent.tbm_glass_or_contact.ToString());
            //    Program.SetValueRadioGroup(GBColorBindness, BasicMensureCurrent.tbm_color_blind.ToString());
            //    Program.SetValueRadioGroup(pnlTriage, BasicMensureCurrent.tbm_triage.ToString());

            //    //Noina Add  [Commented code.Akkaradech on 2014-01-29]
            //    //txt_vision_rvisual_out_lens.Text = BasicMensureCurrent.tbm_vision_rvisual_out_lens;
            //    //txt_vision_lvisual_out_lens.Text = BasicMensureCurrent.tbm_vision_lvisual_out_lens;
            //    //txt_vision_rvisual_with_lens.Text = BasicMensureCurrent.tbm_vision_rvisual_with_lens;
            //    //txt_vision_lvisual_with_lens.Text = BasicMensureCurrent.tbm_vision_lvisual_with_lens;

            //    txt_vision_binocular_out_lens.Text = BasicMensureCurrent.tbm_vision_binocular_out_lens;
            //    txt_vision_binocular_with_lens.Text = BasicMensureCurrent.tbm_vision_binocular_with_lens;

            //    Program.SetValueRadioGroup(pnl_color_vision_isihara_result, BasicMensureCurrent.tbm_color_vision_ishihara_result);
            //    txt_color_vision_isihara_score.Text = BasicMensureCurrent.tbm_color_vision_ishihara_score;
            //    //end

            //    if (BasicMensureCurrent.trn_basic_measure_dtls.Count > 0)
            //    {
            //        BStrn_basic_measure_dtl.DataSource = BasicMensureCurrent.trn_basic_measure_dtls;

            //    }
            //    else
            //    {
            //        BStrn_basic_measure_dtl.AddNew();
            //    }
            //    //StatusCallQueue();
            //}
            //else
            //{
            //    //BStrn_basic_measure_hdr.DataSource = (from t1 in dbc.trn_basic_measure_hdrs select t1).Take(0);
            //    BStrn_basic_measure_hdr.AddNew();
            //    BStrn_basic_measure_dtl.AddNew();
            //    //StatusCallQueue();
            //}
        }
        private void LoadUI()
        {
            uiAllLeft1.LoadDataAll();
            //uiFooter1.LoadData();
        }

        private void disableBtnWhenSave()
        {
            btnSendAuto.Enabled = false;
            btnSendManual.Enabled = false;
        }
        private void enableBtnWhenSave()
        {
            btnSendAuto.Enabled = true;
            btnSendManual.Enabled = true;
        }
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            if (!Checkfrm())
            {
                enableBtnWhenSave();
                return;
            }

            if (Convert1.ToInt32(txtBP1.Text) >= 180 || Convert1.ToInt32(txtBP2.Text) >= 110)
            {
                if (System.Windows.Forms.MessageBox.Show("ความดันโลหิตผู้รับบริการมีค่าตั้งแต่ 180/110 ขึ้นไป ยินยันจะส่งพบแพทย์ หรือไม่?", "BP Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                {
                    enableBtnWhenSave();
                    frmbg.Close();
                    return;
                }
                //if (System.Windows.Forms.MessageBox.Show("ค่า BP เกินค่ามาตรฐาน คุณยืนยันที่จะทำงานต่อหรือไม่", "BP Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                //{
                //    frmbg.Close();
                //    return;
                //}
            }

            btnSendManual.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSaveDraft.Enabled = false;

            this.savedata('N');
            SendAuto_Click(null, null);
            //StatusWaitCallQueue();

            try
            {
                //string gotoRoom = Program.Getmvt_Name(Getmvtid);
                //lbAlertMsg.Text = CallQueue.GetStrSaveAndSend();
            }
            catch (Exception ex)
            {
                enableBtnWhenSave();
                Program.MessageError("frmBasicMeasurement", "btnSendAuto_Click", ex, false);
            }
            frmbg.Close();
        }
        private void btnSendManual_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            if (!Checkfrm())
            {
                enableBtnWhenSave();
                return;
            }

            if (Convert1.ToInt32(txtBP1.Text) >= 180 || Convert1.ToInt32(txtBP2.Text) >= 110)
            {
                if (System.Windows.Forms.MessageBox.Show("ค่า BP เกินค่ามาตรฐาน คุณยืนยันที่จะทำงานต่อหรือไม่", "BP Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                {
                    enableBtnWhenSave();
                    frmbg.Close();
                    return;
                }
            }

            btnSendManual.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSaveDraft.Enabled = false;

            this.savedata('N');
            if (btnSendManual_Click())
            {
                btnSendManual.Enabled = false;
                try
                {
                    // morn clear Unit Display
                    //ClsTCPClient clsTCP = new ClsTCPClient();
                    //clsTCP.SendClearUnitDisplay();
                    // morn clear Unit Display


                    //string gotoRoom = CallQueue.GetStrSaveAndSend();
                    //if (Getmvtid == 0)
                    //{
                    //    lbAlertMsg.Text = "Save data completed.";
                    //}
                    //else
                    //{
                    //    lbAlertMsg.Text = gotoRoom;
                    //}
                }
                catch (Exception)
                {
                    enableBtnWhenSave();
                }
            }
            else
            {
                enableBtnWhenSave();
            }
            frmbg.Close();
        }
        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            int tpr_id;
            if (Program.CurrentRegis != null)
            {
                tpr_id = Program.CurrentRegis.tpr_id;
            }
            else
            {
                tpr_id = SetTprID;
            }
            if (!Checkfrm())
            {
                return;
            }
            if (Program.chkBookComplete(tpr_id))
            {
                if (Convert1.ToInt32(txtBP1.Text) >= 180 || Convert1.ToInt32(txtBP2.Text) >= 110)
                {
                    if (System.Windows.Forms.MessageBox.Show("ค่า BP เกินค่ามาตรฐาน คุณยืนยันที่จะทำงานต่อหรือไม่", "BP Alert", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information) == DialogResult.No)
                    {
                        frmbg.Close();
                        return;
                    }
                }
                this.savedata('D');
                //เช็คว่าเป็นหมวดแก้ไขหรือไม่ ถ้าใช่ siteitem> 0
                if (siteitem == 0)
                {
                    StatusSaveData();
                }
            }
            lbAlertMsg.Text = "Save data completed.";

            frmbg.Close();
        }
        private bool Checkfrm()
        {
            bool isok = true;
            //errorProvider1.Clear();
            //if (txtHeight.Text.Trim().Length == 0)
            //{
            //    isok = false;
            //    errorProvider1.SetError(txtHeight, "Require Field");
            //}
            //if (txtWeight.Text.Trim().Length == 0)
            //{
            //    isok = false;
            //    errorProvider1.SetError(txtWeight, "Require Field");
            //}
            return isok;
        }
        private void savedata(char SaveType)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                trn_basic_measure_hdr patient_basic_hdr = (trn_basic_measure_hdr)BStrn_basic_measure_hdr.Current;
                if (string.IsNullOrEmpty(patient_basic_hdr.tbm_create_by))
                {
                    patient_basic_hdr.mut_id = Program.CurrentUser.mut_id;
                    patient_basic_hdr.tbm_create_by = Program.CurrentUser.mut_username;
                    patient_basic_hdr.tbm_create_date = dateNow;
                }
                patient_basic_hdr.tbm_update_by = Program.CurrentUser.mut_username;
                patient_basic_hdr.tbm_update_date = dateNow;
                patient_basic_hdr.tbm_type = SaveType;
                patient_basic_hdr.tbm_arrive = Program.GetValueRadio(GBStatusOnArrival);
                patient_basic_hdr.tbm_purpose = Program.GetValueRadioTochar(GBPurpose);
                patient_basic_hdr.tbm_appearance = Program.GetValueRadio(GBGeneralAppearance);
                patient_basic_hdr.tbm_precaution = Program.GetValueRadio(GBFallPrecaution);
                patient_basic_hdr.tbm_color_vision_ishihara_result = Program.GetValueRadioTochar(pnl_color_vision_isihara_result);
                if (radioButton2.Checked != true)
                {
                    patient_basic_hdr.tbm_color_blind = Program.GetValueRadioTochar(GBColorBindness);
                }
                patient_basic_hdr.tbm_triage = Program.GetValueRadioTochar(pnlTriage);
                if (patient_basic_hdr.tbm_color_blind == 'N')
                {
                    patient_basic_hdr.tbm_color_blind_text = "";
                }
                BStrn_basic_measure_dtl.EndEdit();
                BStrn_basic_measure_hdr.EndEdit();
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

                if (SetTprID != 0)
                {
                    uiAllLeft1.LoadDataAll(SetTprID);
                }
                else
                {
                    uiAllLeft1.LoadDataAll();
                }

                //// set Radio

                //trn_basic_measure_hdr patient_basic_hdr = (trn_basic_measure_hdr)BStrn_basic_measure_hdr.Current;
                //if (SetTprID == 0)
                //{
                //    patient_basic_hdr.mut_id = Program.CurrentUser.mut_id;
                //    if (patient_basic_hdr.tbm_create_by == null)
                //    {
                //        patient_basic_hdr.tbm_create_date = Program.GetServerDateTime();
                //        patient_basic_hdr.tbm_create_by = Program.CurrentUser.mut_username;
                //    }
                //    patient_basic_hdr.tpr_id = Program.CurrentRegis.tpr_id;
                //}

                //patient_basic_hdr.tbm_update_by = Program.CurrentUser.mut_username;
                //patient_basic_hdr.tbm_update_date = Program.GetServerDateTime();
                //patient_basic_hdr.tbm_type = SaveType;
                //patient_basic_hdr.tbm_arrive = Program.GetValueRadio(GBStatusOnArrival);
                //patient_basic_hdr.tbm_purpose = Program.GetValueRadioTochar(GBPurpose);
                //patient_basic_hdr.tbm_appearance = Program.GetValueRadio(GBGeneralAppearance);

                //patient_basic_hdr.tbm_precaution = Program.GetValueRadio(GBFallPrecaution);
                ////BasicMensureCurrent.tbm_glass_or_contact = Program.GetValueRadioTochar(GBEyeGlasseslen);

                //if (radioButton2.Checked != true) { patient_basic_hdr.tbm_color_blind = Program.GetValueRadioTochar(GBColorBindness); }

                //patient_basic_hdr.tbm_triage = Program.GetValueRadioTochar(pnlTriage);
                //if (patient_basic_hdr.tbm_color_blind == 'N')
                //{
                //    patient_basic_hdr.tbm_color_blind_text = "";
                //}

                ////Noina Add
                //patient_basic_hdr.tbm_vision_rvisual_out_lens = txt_vision_rvisual_out_lens.Text;
                //patient_basic_hdr.tbm_vision_lvisual_out_lens = txt_vision_lvisual_out_lens.Text;

                //patient_basic_hdr.tbm_vision_rvisual_with_lens = txt_vision_rvisual_with_lens.Text;
                //patient_basic_hdr.tbm_vision_lvisual_with_lens = txt_vision_lvisual_with_lens.Text;

                //patient_basic_hdr.tbm_vision_binocular_out_lens = txt_vision_binocular_out_lens.Text;
                //patient_basic_hdr.tbm_vision_binocular_with_lens = txt_vision_binocular_with_lens.Text;

                //patient_basic_hdr.tbm_color_vision_ishihara_result = Program.GetValueRadioTochar(pnl_color_vision_isihara_result);
                //patient_basic_hdr.tbm_color_vision_ishihara_score = txt_color_vision_isihara_score.Text;
                ////end

                ////BasicMensureCurrent.trn_patient_regi = (from t1 in dbc.trn_patient_regis where t1.tpr_id == Program.CurrentRegis.tpr_id select t1).FirstOrDefault();

                ////((trn_basic_measure_dtl)BasicMeasurementDTLbindingSource1.Current).tbm_id = BasicMensureCurrent.tbm_id;
                //BStrn_basic_measure_dtl.EndEdit();
                //BStrn_basic_measure_hdr.EndEdit();
                //dbc.SubmitChanges();
                //if (SetTprID != 0)
                //{
                //    uiAllLeft1.LoadDataAll(SetTprID);
                //}
                //else
                //{
                //    uiAllLeft1.LoadDataAll();
                //}
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "savedata", ex, false);
                //throw;
            }
        }

        private void btnCallQueue_Click(object sender, EventArgs e)
        {
            btnCallQueue.Enabled = false;
            lbAlertMsg.Text = "";
            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            try
            {
                //noina
                StatusTransaction callQ = CallQueue.P_CallQueueWaitReady();
                //end noina

                if (callQ == StatusTransaction.Error)
                {
                    lbAlertMsg.Text = "กรุณากด Call Queue อีกครั้ง";
                    btnCallQueue.Enabled = true;
                }
                else
                {
                    StatusTransaction calBasic = new Class.FunctionDataCls().calSendBasic();
                    if (calBasic == StatusTransaction.Error)
                    {

                    }
                    if (Program.CurrentRegis != null)
                    {
                        LoadUI();
                        clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                        try
                        {
                            this.LoadData(Program.CurrentRegis.tpr_id);
                            //LoadPatientData(Program.CurrentRegis.tpr_id);
                            btnRetrieveVitalSign_Click(null, null);
                            uiMenuBar1.LoadEnableQuestionare();
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError("frmBasicMeasurement", "btnCallQueue_Click", ex, false);
                        }
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = "No patient on queue!";
                        btnCallQueue.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnCallQueue_Click", ex, false);
            }
            finally
            {
                frmbg.Close();
            }
        }
        private void btnHold_Click(object sender, EventArgs e)
        {
            btnHold.Enabled = false;

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            if (Program.CurrentRegis == null) { return; }
            string QueueNo = Program.CurrentRegis.tpr_queue_no;

            int holdTime = PrePareData.StaticDataCls.HoldTime.Where(x => x.mhs_id == Program.CurrentSite.mhs_id).Select(x => x.holdTime).FirstOrDefault();
            QueueClass.TransactionQueueCls.PatientHoldQueue hold = new QueueClass.TransactionQueueCls().HoldQueue(Program.CurrentPatient_queue.tps_id, holdTime, Program.CurrentUser.mut_username);
            if (hold.Status == QueueClass.TransactionQueueCls.PatientHoldQueue.StatusHoldQueue.Error)
            {
                lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
                btnHold.Enabled = true;
            }
            else
            {
                StatusWaitCallQueue();
                lbAlertMsg.Text = string.Format(Program.MsgHold, hold.QueueNo);
            }
            //StatusTransaction result = CallQueue.P_CallHold();
            //if (result == StatusTransaction.True)
            //{
            //    // morn clear Unit Display
            //    new ClsTCPClient().sendClearUnitDisplay();
            //    // morn clear Unit Display

            //    Program.CurrentPatient_queue = null;
            //    Program.CurrentRegis = null;
            //    uiMenuBar1.LoadEnableQuestionare();
            //    StatusWaitCallQueue();
            //    lbAlertMsg.Text = string.Format(Program.MsgHold, QueueNo);
            //}
            //else if (result == StatusTransaction.Error)
            //{
            //    btnHold.Enabled = true;
            //    lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
            //}
            frmbg.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันยกเลิกการตรวจ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    btnCancel.Enabled = false;
                    savedata('D');
                    string QueueNo = Program.CurrentRegis.tpr_queue_no;
                    //morn clear Unit Display
                    new ClsTCPClient().sendClearUnitDisplay();
                    // morn clear Unit Display
                    Class.ClsPendingOnStation pend = new ClsPendingOnStation();
                    if (pend.pendingPatientBeforeCheckpointB())
                    {
                        lbAlertMsg.Text = string.Format(Program.MsgCancel, QueueNo);
                        StatusWaitCallQueue();
                        return;
                    }
                }
                catch
                {

                }
                lbAlertMsg.Text = "เกิดความผิดพลาดของโปรแกรม โปรด Cancel อีกครั้ง";
            }
            //frmBGScreen frmbg = new frmBGScreen(); frmbg.Show(); Application.DoEvents();

            //if (Program.CurrentRegis == null) { return; }
            //FrmReason frm = new FrmReason();
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    // morn clear Unit Display
            //    ClsTCPClient clsTCP = new ClsTCPClient();
            //    clsTCP.SendClearUnitDisplay();
            //    // morn clear Unit Display

            //    BasicMeasurementDTLbindingSource1.CancelEdit();
            //    BasicMeasurementbindingSource1.CancelEdit();
            //    string QueueNo = Program.CurrentRegis.tpr_queue_no;
            //    CallQueue.P_CallCancel( frm.strReason, frm.strReasonOther);

            //    //noina 29/01/2557 
            //    //clsCountDown.cancelCountDown();
            //    //end noina

            //    uiMenuBar1.LoadEnableQuestionare();
            //    StatusWaitCallQueue();
            //    if (Program.CurrentRegis == null)
            //    {
            //        lbAlertMsg.Text = string.Format(Program.MsgCancel, QueueNo);
            //    }

            //}
            //frmbg.Close();
        }
        private void SendAuto_Click(object sender, EventArgs e)
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;

            trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
            //EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            //if (CallQueue.IsStatusED()) { StatusWaitCallQueue(); return; }
            //Boolean saveIsCompleted = false;
            ////Update Queue
            //var CurrentQueue = (from t1 in dbc.trn_patient_queues
            //                                where t1.tps_id == Program.CurrentPatient_queue.tps_id
            //                                select t1).FirstOrDefault();
            //CurrentQueue.mrm_id = Program.CurrentRoom.mrm_id;
            //CurrentQueue.mrd_id = Program.CurrentRoom.mrd_id;
            //CurrentQueue.tps_send_by = Program.CurrentUser.mut_username;
            //CurrentQueue.tps_end_date = Program.GetServerDateTime();
            //CurrentQueue.tps_status = "ED";
            //CurrentQueue.tps_update_by = Program.CurrentUser.mut_username;
            //CurrentQueue.tps_update_date = Program.GetServerDateTime();
            //dbc.SubmitChanges();

            //int mrmid = 0;
            //int mvtid = 0;

            DateTime dateNow = Program.GetServerDateTime();
            int ct_PE = (from t1 in dbc.log_user_logins
                         join t2 in dbc.mst_room_dtls on t1.mrd_id equals t2.mrd_id
                         join t3 in dbc.mst_room_hdrs on t2.mrm_id equals t3.mrm_id
                         where t1.lug_start_date.Value.Date == dateNow.Date
                         && t1.lug_end_date == null
                         && t3.mrm_code == "DC"
                         && t3.mhs_id == Program.CurrentSite.mhs_id
                         select t1).Count();

            if (Program.CurrentSite.mhs_extra_pe_type == true)
            {
                //Sumit Edit 27/05/2014
                if (tpr.tpr_req_pe_bef_chkup == 'Y'
                || Convert1.ToInt32(txtBP1.Text) >= 180
                || Convert1.ToInt32(txtBP2.Text) >= 110)
                {
                    // Add Send Start Check B
                    Class.FunctionDataCls func = new Class.FunctionDataCls();

                    if ((ct_PE > 0) && (func.checkDoctorRequest(tpr_id, Program.CurrentSite.mhs_id, false) == true) && Program.CurrentRegis.tpr_req_inorout_doctor != "UT")
                    {
                        //mrmid = mst.getMstRoomHdr("DC").mrm_id;
                        //mvtid = mst.getMstEvent("PE").mvt_id;
                        string messege = "";
                        StatusTransaction result = new Class.SendQueue().SendToPE(tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                        if (result == StatusTransaction.True)
                        {
                            new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendPE,
                                                        tpr_id,
                                                        tps_id,
                                                        Program.CurrentSite.mhs_id,
                                                        Program.CurrentRoom.mrd_ename,
                                                        Program.CurrentUser.mut_username);

                            new ClsTCPClient().sendClearUnitDisplay();
                            //ClsBasicMeasurement.SaveBasicMeasurment(tpr_id, true);
                            new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                            //new Class.FunctionDataCls().genOrUpdateDoctorQuota(tpr_id);
                            new Class.FunctionDataCls().dispense_doctor_by_waiting(tpr_id);
                            StatusWaitCallQueue();
                            lbAlertMsg.Text = messege;
                        }
                    }
                    else
                    {
                        string messege = "";
                        StatusTransaction result = new Class.SendQueue().SendToScreening(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                        if (result == StatusTransaction.True)
                        {
                            new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendScreening,
                                                        tpr_id,
                                                        tps_id,
                                                        Program.CurrentSite.mhs_id,
                                                        Program.CurrentRoom.mrd_ename,
                                                        Program.CurrentUser.mut_username);

                            new ClsTCPClient().sendClearUnitDisplay();
                            //ClsBasicMeasurement.SaveBasicMeasurment(Program.CurrentRegis.tpr_id, true);
                            new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                            StatusWaitCallQueue();
                            lbAlertMsg.Text = messege;
                        }
                        //mrmid = mst.getMstRoomHdr("SC").mrm_id;
                        //mvtid = mst.getMstEvent("SC").mvt_id;
                    }
                }
                else
                {
                    string messege = "";
                    StatusTransaction result = new Class.SendQueue().SendToScreening(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                    if (result == StatusTransaction.True)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendScreening,
                                                    tpr_id,
                                                    tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);

                        new ClsTCPClient().sendClearUnitDisplay();
                        //ClsBasicMeasurement.SaveBasicMeasurment(Program.CurrentRegis.tpr_id, true);
                        new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = messege;
                    }
                    //mrmid = mst.getMstRoomHdr("SC").mrm_id;
                    //mvtid = mst.getMstEvent("SC").mvt_id;
                }
            }
            else
            {
                //Sumit Edit 27/05/2014
                if (tpr.tpr_req_pe_bef_chkup == 'Y'
                || tpr.tpr_foreigner == 'Y'
                || Convert1.ToInt32(txtBP1.Text) >= 180
                || Convert1.ToInt32(txtBP2.Text) >= 110)
                {
                    // Add Send Start Check B
                    Class.FunctionDataCls func = new Class.FunctionDataCls();

                    if ((ct_PE > 0) && (func.checkDoctorRequest(tpr_id, Program.CurrentSite.mhs_id, false) == true) && Program.CurrentRegis.tpr_req_inorout_doctor != "UT")
                    {
                        //mrmid = mst.getMstRoomHdr("DC").mrm_id;
                        //mvtid = mst.getMstEvent("PE").mvt_id;
                        string messege = "";
                        StatusTransaction result = new Class.SendQueue().SendToPE(tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                        lbAlertMsg.Text = messege;
                        if (result == StatusTransaction.True)
                        {
                            new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendScreening,
                                                        tpr_id,
                                                        tps_id,
                                                        Program.CurrentSite.mhs_id,
                                                        Program.CurrentRoom.mrd_ename,
                                                        Program.CurrentUser.mut_username);

                            new ClsTCPClient().sendClearUnitDisplay();
                            //ClsBasicMeasurement.SaveBasicMeasurment(tpr_id, true);
                            new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                            //new Class.FunctionDataCls().genOrUpdateDoctorQuota(tpr_id);
                            new Class.FunctionDataCls().dispense_doctor_by_waiting(tpr_id);
                            StatusWaitCallQueue();
                            lbAlertMsg.Text = messege;
                        }
                    }
                    else
                    {
                        string messege = "";
                        StatusTransaction result = new Class.SendQueue().SendToScreening(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                        lbAlertMsg.Text = messege;
                        if (result == StatusTransaction.True)
                        {
                            new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendScreening,
                                                        tpr_id,
                                                        tps_id,
                                                        Program.CurrentSite.mhs_id,
                                                        Program.CurrentRoom.mrd_ename,
                                                        Program.CurrentUser.mut_username);

                            new ClsTCPClient().sendClearUnitDisplay();
                            //ClsBasicMeasurement.SaveBasicMeasurment(Program.CurrentRegis.tpr_id, true);
                            new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                            StatusWaitCallQueue();
                            lbAlertMsg.Text = messege;
                        }
                        //mrmid = mst.getMstRoomHdr("SC").mrm_id;
                        //mvtid = mst.getMstEvent("SC").mvt_id;
                    }
                }
                else
                {
                    //mrmid = mst.getMstRoomHdr("SC").mrm_id;
                    //mvtid = mst.getMstEvent("SC").mvt_id;
                    string messege = "";
                    StatusTransaction result = new Class.SendQueue().SendToScreening(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                    lbAlertMsg.Text = messege;
                    if (result == StatusTransaction.True)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendScreening,
                                                    tpr_id,
                                                    tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);

                        new ClsTCPClient().sendClearUnitDisplay();
                        //ClsBasicMeasurement.SaveBasicMeasurment(Program.CurrentRegis.tpr_id, true);
                        new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = messege;
                    }
                }
            }
            //Getmvtid = mvtid;

            //CallQueue.Getmvtid = mvtid;
            //CallQueue.Gettprid = Program.CurrentRegis.tpr_id;
            //CallQueue.Getmrmid = mrmid;

            //var objqueue = (from t1 in dbc.trn_patient_queues
            //                where t1.tpr_id == Program.CurrentRegis.tpr_id
            //                && t1.mrm_id == mrmid
            //                && t1.mvt_id == mvtid
            //                select t1).FirstOrDefault();
            //if (objqueue == null)
            //{
            //    trn_patient_queue newqueue = new trn_patient_queue();
            //    newqueue.tpr_id = Program.CurrentRegis.tpr_id;
            //    newqueue.mrm_id = mrmid;
            //    newqueue.mrd_id = null;
            //    newqueue.mvt_id = mvtid;
            //    newqueue.tps_start_date = null;
            //    newqueue.tps_end_date = null;
            //    newqueue.tps_status = "NS";
            //    newqueue.tps_ns_status = "QL";
            //    newqueue.tps_create_by = Program.CurrentUser.mut_username;
            //    newqueue.tps_create_date = Program.GetServerDateTime();
            //    newqueue.tps_update_by = Program.CurrentUser.mut_username;
            //    newqueue.tps_update_date = newqueue.tps_create_date;
            //    dbc.trn_patient_queues.InsertOnSubmit(newqueue);
            //}
            //else
            //{
            //    objqueue.tps_create_date = Program.GetServerDateTime();
            //    objqueue.tps_create_by = Program.CurrentUser.mut_username;
            //    objqueue.tps_update_by = objqueue.tps_create_by;
            //    objqueue.tps_update_date = objqueue.tps_create_date;
            //}
            //dbc.SubmitChanges();
            //saveIsCompleted = true;
            //if (saveIsCompleted)
            //{
            //    lbAlertMsg.Text = "Send Auto Is Completed.";
            //    //MessageBox.Show("Send Auto Is Completed.", "Result Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //    // morn clear Unit Display
            //    ClsTCPClient clsTCP = new ClsTCPClient();
            //    clsTCP.SendClearUnitDisplay();
            //    // morn clear Unit Display
            //}
            //ClsBasicMeasurement.SaveBasicMeasurment(Program.CurrentRegis.tpr_id, true);
        }
        private bool btnSendManual_Click()
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;

            //EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            //if (CallQueue.IsStatusED()) { StatusWaitCallQueue(); return false; }
            //int mrm_id = 0;
            //int mvtid = 0;
            frmChoicePopUp frm = new frmChoicePopUp();
            var result = frm.ShowDialog();
            if (result == DialogResult.Yes)
            {
                //mrm_id = mst.getMstRoomHdr("DC").mrm_id;
                //mvtid = mst.getMstEvent("PE").mvt_id;
                string messege = "";
                StatusTransaction sendQueue = new Class.SendQueue().SendToPE(tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                lbAlertMsg.Text = messege;
                if (sendQueue == StatusTransaction.True)
                {
                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendPE,
                                                tpr_id,
                                                tps_id,
                                                Program.CurrentSite.mhs_id,
                                                Program.CurrentRoom.mrd_ename,
                                                Program.CurrentUser.mut_username);

                    new ClsTCPClient().sendClearUnitDisplay();
                    //ClsBasicMeasurement.SaveBasicMeasurment(tpr_id, true);
                    new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                    //new Class.FunctionDataCls().genOrUpdateDoctorQuota(Program.CurrentRegis.tpr_id);
                    new Class.FunctionDataCls().dispense_doctor_by_waiting(tpr_id);
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = messege;
                }
            }
            else if (result == DialogResult.No)
            {
                string messege = "";
                StatusTransaction sendQueue = new Class.SendQueue().SendToScreening(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                if (sendQueue == StatusTransaction.True)
                {
                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendScreening,
                                                tpr_id,
                                                tps_id,
                                                Program.CurrentSite.mhs_id,
                                                Program.CurrentRoom.mrd_ename,
                                                Program.CurrentUser.mut_username);

                    new ClsTCPClient().sendClearUnitDisplay();
                    //ClsBasicMeasurement.SaveBasicMeasurment(Program.CurrentRegis.tpr_id, true);
                    new Class.VistalSignCls().RetrieveVistalSignBackground(tpr_id);
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = messege;
                }
                //mrm_id = mst.getMstRoomHdr("SC").mrm_id;
                //mvtid = mst.getMstEvent("SC").mvt_id;
            }
            else
            {
                return false;
            }
            //Getmvtid = mvtid;

            //CallQueue.Getmvtid = mvtid;
            //CallQueue.Gettprid=Program.CurrentRegis.tpr_id;
            //CallQueue.Getmrmid = mrm_id;

            //Boolean saveIsCompleted = false;
            //DateTime dtnow = Program.GetServerDateTime();
            //trn_patient_queue currentqueue = (from t1 in dbc.trn_patient_queues
            //                                    where t1.tps_id == Program.CurrentPatient_queue.tps_id
            //                                    select t1).FirstOrDefault();
            //if (currentqueue != null)
            //{
            //    currentqueue.tpr_id = Program.CurrentRegis.tpr_id;
            //    currentqueue.mrm_id = Program.CurrentRoom.mrm_id;
            //    currentqueue.mrd_id = Program.CurrentRoom.mrd_id;
            //    currentqueue.tps_send_by = Program.CurrentUser.mut_username;
            //    currentqueue.tps_end_date = Program.GetServerDateTime();
            //    currentqueue.tps_status = "ED";
            //    currentqueue.tps_update_by = Program.CurrentUser.mut_username;
            //    currentqueue.tps_update_date = currentqueue.tps_end_date;
            //    dbc.SubmitChanges();

            //    var objqueue = (from t1 in dbc.trn_patient_queues
            //                    where t1.tpr_id == Program.CurrentRegis.tpr_id
            //                        && t1.mrm_id == mrm_id
            //                        && t1.mvt_id == mvtid
            //                    select t1).FirstOrDefault();
            //    if (objqueue == null)
            //    {
            //        trn_patient_queue newitem = new trn_patient_queue();
            //        newitem.tpr_id = Program.CurrentRegis.tpr_id;
            //        newitem.mrm_id = mrm_id;
            //        newitem.mvt_id = mvtid;
            //        newitem.mrd_id = null;
            //        newitem.tps_end_date = null;
            //        newitem.tps_start_date = null;
            //        newitem.tps_status = "NS";
            //        newitem.tps_ns_status = "QL";
            //        newitem.tps_update_by = Program.CurrentUser.mut_username;
            //        newitem.tps_update_date = currentqueue.tps_end_date;
            //        newitem.tps_create_by = Program.CurrentUser.mut_username;
            //        newitem.tps_create_date = currentqueue.tps_end_date;
            //        dbc.trn_patient_queues.InsertOnSubmit(newitem);
            //        dbc.SubmitChanges();
            //    }
            //    else
            //    {
            //        objqueue.tps_create_date = dtnow;
            //        objqueue.tps_create_by = Program.CurrentUser.mut_username;
            //        objqueue.tps_update_by = objqueue.tps_create_by;
            //        objqueue.tps_update_date = dtnow;
            //        dbc.SubmitChanges();
            //    }

            //    saveIsCompleted = true;
            //}
            //if (saveIsCompleted)
            //{
            //    lbAlertMsg.Text = "Send manual is completed.";
            //}

            return true;
        }
        private void StatusWaitCallQueue()
        {
            groupQueue.Text = "Queue";
            btnReady.Enabled = false;
            btnCallQueue.Enabled = true;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;

            btnSendManual.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
            btnRetrieveVitalSign.Enabled = false;

            RDstatusonArrivalWalk.Checked = true;
            RDfallprecautionSP.Checked = true;
            RDGeneralGood.Checked = true;
            RDEyeYes.Checked = true;
            //RDcolorBindnessNormal.Checked = true;
            rdoSuperRed.Checked = true;
            rdoSuperRed.Checked = false;

            txt_color_vision_isihara_score.Text = "";
            txt_vision_binocular_out_lens.Text = "";
            txt_vision_binocular_with_lens.Text = "";
            txt_vision_lvisual_out_lens.Text = "";
            txt_vision_lvisual_with_lens.Text = "";
            txt_vision_rvisual_out_lens.Text = "";
            txt_vision_rvisual_with_lens.Text = "";

            Program.CurrentRegis = null;
            Program.CurrentPatient_queue = null;

            dbc.Dispose();
            dbc = new InhCheckupDataContext();

            trn_basic_measure_hdr objhdr = new trn_basic_measure_hdr();
            BStrn_basic_measure_hdr.DataSource = objhdr;
            trn_basic_measure_dtl objdtl = new trn_basic_measure_dtl();
            BStrn_basic_measure_dtl.DataSource = objdtl;

            this.LoadUI();

        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if (RDcolorBindnessNormal.Checked)
            {
                txtColorBindness.Text = "";
                txtColorBindness.Enabled = false;
            }
            else
            {
                txtColorBindness.Enabled = true;

            }
        }
        private void btnRetrieveVitalSign_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis == null)
            {
                return;
            }
            btnRetrieveVitalSign.Enabled = false;
            string withlens = "";




            trn_basic_measure_dtl curretnBMDTL = (trn_basic_measure_dtl)BStrn_basic_measure_dtl.Current;
            if (curretnBMDTL != null)
            {
                //Get from webservice
                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();
                Application.DoEvents();

                try
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        DateTime dateNow = Program.GetServerDateTime();
                        trn_patient patient = cdc.trn_patient_regis.Where(x => x.tpr_id == Program.CurrentRegis.tpr_id).Select(x => x.trn_patient).FirstOrDefault();
                        List<trn_basic_measure_dtl> list_basic_dtl = new EmrClass.GetDataFromWSTrakCare().getVitalSignByHN(patient.tpt_hn_no);
                        if (list_basic_dtl != null && list_basic_dtl.Count() > 0)
                        {
                            trn_basic_measure_dtl basic_dtl = list_basic_dtl.Where(x => x.tbd_date.Value.Date == dateNow.Date)
                                                                            .OrderByDescending(x => x.tbd_date)
                                                                            .FirstOrDefault();

                            if (basic_dtl != null)
                            {
                                trn_basic_measure_hdr currentHdrBinding = (trn_basic_measure_hdr)BStrn_basic_measure_hdr.Current;

                                trn_basic_measure_dtl currentDtlBinding = (trn_basic_measure_dtl)BStrn_basic_measure_dtl.Current;
                                currentDtlBinding.tbd_bmi = basic_dtl.tbd_bmi;
                                currentDtlBinding.tbd_date = basic_dtl.tbd_date;
                                currentDtlBinding.tbd_diastolic = basic_dtl.tbd_diastolic;
                                currentDtlBinding.tbd_exhale = basic_dtl.tbd_exhale;
                                currentDtlBinding.tbd_height = basic_dtl.tbd_height;
                                currentDtlBinding.tbd_inhale = basic_dtl.tbd_inhale;
                                currentDtlBinding.tbd_mea_remark = basic_dtl.tbd_mea_remark;
                                currentDtlBinding.tbd_o2sat = basic_dtl.tbd_o2sat;
                                currentDtlBinding.tbd_pulse = basic_dtl.tbd_pulse;
                                currentDtlBinding.tbd_rr = basic_dtl.tbd_rr;
                                currentDtlBinding.tbd_systolic = basic_dtl.tbd_systolic;
                                currentDtlBinding.tbd_temp = basic_dtl.tbd_temp;
                                currentDtlBinding.tbd_vision_lt = basic_dtl.tbd_vision_lt;
                                currentDtlBinding.tbd_vision_rt = basic_dtl.tbd_vision_rt;
                                currentDtlBinding.tbd_waist = basic_dtl.tbd_waist;
                                currentDtlBinding.tbd_weight = basic_dtl.tbd_weight;
                                currentHdrBinding.tbm_vision_right = Convert1.ToInt32(basic_dtl.tbd_vision_rt);
                                currentHdrBinding.tbm_vision_left = Convert1.ToInt32(basic_dtl.tbd_vision_lt);
                                currentDtlBinding.tbd_vision_with_lens = basic_dtl.tbd_vision_with_lens;

                                withlens = currentDtlBinding.tbd_vision_with_lens == true ? "Y" : "N";
                                if (currentDtlBinding.tbd_vision_with_lens == true)
                                {
                                    currentHdrBinding.tbm_vision_rvisual_with_lens = basic_dtl.tbd_vision_rt;
                                    currentHdrBinding.tbm_vision_lvisual_with_lens = basic_dtl.tbd_vision_lt;
                                    currentHdrBinding.tbm_vision_rvisual_out_lens = "";
                                    currentHdrBinding.tbm_vision_lvisual_out_lens = "";
                                }
                                else
                                {
                                    currentHdrBinding.tbm_vision_rvisual_with_lens = "";
                                    currentHdrBinding.tbm_vision_lvisual_with_lens = "";
                                    currentHdrBinding.tbm_vision_rvisual_out_lens = basic_dtl.tbd_vision_rt;
                                    currentHdrBinding.tbm_vision_lvisual_out_lens = basic_dtl.tbd_vision_lt;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("frmBasicMeasurement", "btnRetrieveVitalSign_Click", ex, false);
                }
                frmbg.Close();
            }
            btnRetrieveVitalSign.Enabled = true;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                txtOtherStatusOnArrival.Enabled = true;
            }
            else
            {
                txtOtherStatusOnArrival.Text = "";
                txtOtherStatusOnArrival.Enabled = false;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                txtGeneralAppearance.Enabled = true;
            }
            else
            {
                txtGeneralAppearance.Text = "";
                txtGeneralAppearance.Enabled = false;
            }
        }

        private void StatusCallQueue()
        {
            if (SetTprID == 0)
            {
                lbAlertMsg.Text = "";
                btnCallQueue.Enabled = false;
                btnHold.Enabled = true;
                btnCancel.Enabled = true;

                btnSendManual.Enabled = true;
                btnSaveDraft.Enabled = true;
                btnSendAuto.Enabled = true;
                btnRetrieveVitalSign.Enabled = true;
                LoadUI();
            }
            else
            {
                lbAlertMsg.Text = "";
                btnCallQueue.Enabled = false;
                btnHold.Enabled = false;
                btnCancel.Enabled = false;

                btnSendManual.Enabled = false;
                btnSaveDraft.Enabled = false;
                btnSendAuto.Enabled = false;
                btnRetrieveVitalSign.Enabled = false;
            }
        }


        private void StatusSaveData()
        {
            lbAlertMsg.Text = "";
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            btnCancel.Enabled = true;

            btnSendManual.Enabled = true;
            btnSaveDraft.Enabled = true;
            btnSendAuto.Enabled = true;
            btnRetrieveVitalSign.Enabled = true;
        }

        private void RDpurposeOther_CheckedChanged(object sender, EventArgs e)
        {
            if (RDpurposeOther.Checked)
            {
                txtPurposeOther.Enabled = true;
            }
            else
            {
                txtPurposeOther.Text = "";
                txtPurposeOther.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                txtColorBindness.Text = "";
                txtColorBindness.Enabled = false;
            }
            else
            {
                txtColorBindness.Enabled = true;

            }
        }
        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (RDcolorBindnessNormal.Checked)
            {
                txtColorBindness.Text = "";
                txtColorBindness.Enabled = false;
            }
            else
            {
                txtColorBindness.Enabled = true;

            }
        }

        #region Unit Display

        countDownCls clsCountDown = new countDownCls();

        private void LoadHandlerCountDown()
        {
            clsCountDown.successCountDown += new countDownCls.SuccessHandler(cls_successCountDown);
            clsCountDown.countDownTick += new countDownCls.TickHandler(cls_countDownTick);
        }

        private void cls_countDownTick(object sender, timeArgs e)
        {
            groupQueue.Text = "Queue " + e.countDownTime.timeString;
        }

        private void cls_successCountDown(object sender, successTypeArgs e)
        {
            lbAlertMsg.Text = "";
            StatusTransaction ready = CallQueue.P_CallQueueReady();
            if (ready == StatusTransaction.True)
            {
                AlertOutDepartment.LoadTime();
                StatusTransaction showUnit = new ClsTCPClient().sendCallUnitDisplay();
                if (showUnit == StatusTransaction.Error)
                {
                    //lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถแสดงผลบน unit display ได้";
                }
                StatusCallQueueReady();
            }
            else if (ready == StatusTransaction.Error)
            {
                lbAlertMsg.Text = "กรุณากดปุ่ม Ready อีกครั้ง";
                btnReady.Enabled = true;
            }
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            btnReady.Enabled = false;
            //clsCountDown = new countDownCls();
            clsCountDown.finishCountDown();
            //CallQueue.P_CallQueueReady();
            //clsTCPClients = new ClsTCPClient();
            //ClsTCPClient.IPAddress();
            //StatusCallQueueReady();
        }

        private void StatusCallQueueReady()
        {
            groupQueue.Text = "Queue";
            btnReady.Enabled = false;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            btnCancel.Enabled = true;

            btnSendManual.Enabled = true;
            btnSaveDraft.Enabled = true;
            btnSendAuto.Enabled = true;
            btnRetrieveVitalSign.Enabled = true;
        }

        private void StatusCallQueueWaitingReady()
        {
            btnReady.Enabled = true;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;

            btnSendManual.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
            btnRetrieveVitalSign.Enabled = false;
        }

        #endregion

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmBasicMeasurement_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsCountDown.cancelCountDown();
            dbc.Dispose();
        }

        private void uiAllLeft1_OnWaitingSuccessProcess(object sender, StatusTransaction isCallQueue, string e)
        {
            lbAlertMsg.Text = "";
            this.AutoScrollPosition = new Point(0, 0);

            lbAlertMsg.Text = e;
            if (isCallQueue == StatusTransaction.True)
            {
                btnCallQueue.Enabled = false;

                frmBGScreen frmbg = new frmBGScreen();
                frmbg.Show();
                Application.DoEvents();

                StatusTransaction calBasic = new Class.FunctionDataCls().calSendBasic();
                if (calBasic == StatusTransaction.Error)
                {

                }
                if (Program.CurrentRegis != null)
                {
                    LoadUI();
                    clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                    try
                    {
                        this.LoadData(Program.CurrentRegis.tpr_id);
                        //LoadPatientData(Program.CurrentRegis.tpr_id);
                        btnRetrieveVitalSign_Click(null, null);
                        uiMenuBar1.LoadEnableQuestionare();
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError("frmBasicMeasurement", "uiAllLeft1_OnWaitingSuccessProcess", ex, false);
                    }
                    StatusCallQueueWaitingReady();
                }
                else
                {
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = "No patient on queue!";
                    btnCallQueue.Enabled = true;
                }

                frmbg.Close();
            }
        }
    }
}
