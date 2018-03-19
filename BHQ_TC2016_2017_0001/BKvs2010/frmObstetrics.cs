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
using BKvs2010.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.IO;

namespace BKvs2010
{
    public partial class frmObstetrics : Form
    {
        PopupUltrasoundLower.ResultPopupUltrasoundLower resultUltrasound = PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater;

        public frmObstetrics()
        {
            InitializeComponent();
            uiAllLeft2.OnRefreshStatusED += new Usercontrols.UIAllLeft.RefreshStatusED(uiAllLeft1_OnRefreshStatusED);
            uiFooter2.OnFooternameClick += new Usercontrols.UIFooter.FooterNameClick(OnUCFooterClicked);
        }

        public int SetTprID { get; set; }
        public int siteitem { get; set; }


        private void OnUCFooterClicked(int tprid,int mhs_id)
        {
            // Handle event Footer from here
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            siteitem = mhs_id;
            SetTprID = tprid;
            trn_patient_queue objQtxp = (from t1 in dbc.trn_patient_queues
                                         where //t1.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id &&
                                              t1.mst_room_hdr.mrm_code == "PT"
                                         && t1.tpr_id == SetTprID
                                         select t1).FirstOrDefault();
            if (objQtxp != null)
            {
                ClickAutoRediobutton();
                LoadDrowDownList();
                var t11 = (from t1 in dbc.trn_obstetric_chiefs
                           where t1.tpr_id == SetTprID
                           select t1);
                if (t11.Count() > 0)
                {
                    bindingSource1.DataSource = t11.FirstOrDefault();
                    ViewDataFrom();
                }
                LoadInvestigation(SetTprID);
                LoadObstetricHistoryData(SetTprID);
                trn_obstetric_chief obc = (trn_obstetric_chief)bindingSource1.Current;
                if (obc.toc_create_by == Program.CurrentUser.mut_username)
                {
                    btnSaveDraft.Enabled = true;
                    panel3.Enabled = true;
                    panel2.Enabled = true;
                }
                else
                {
                    btnSaveDraft.Enabled = false;
                    panel3.Enabled = false;
                    panel2.Enabled = false;
                }

                btnReady.Enabled = false;
                btnCallQueue.Enabled = false;
                btnHold.Enabled = false;
                btnCancel.Enabled = false;
                btnSendManual.Enabled = false;
                btnSendAuto.Enabled = false;
                btnSendToCheckB.Enabled = false;
                btnPrintPreview.Enabled = true;
                btnPrint.Enabled = true;
                btnSendToDocScan.Enabled = false;
                uiAllLeft2.LoadDataAll(tprid);
            }

            //uiFooter2.LoadData();

            frmbg.Close();
        }
        private void uiAllLeft1_OnRefreshStatusED()
        {
            Clearfrm();
            StatusWaitCallQueue();
        }


        private void ClickAutoRediobutton()
        {
            rdbSingle.Checked = true;
            rdbNo_Contrac.Checked = true;
            rdbNo_Homr_Rep_Ther.Checked = true;
            rdbNo_Hyterectomy.Checked = true;
            rdbNo_Oophorectomy.Checked = true;
            ////tab2
            rdbNormal_Yolva.Checked = true;
            rdbNormal_Urethar.Checked = true;
            rdbNormal_Bartholin.Checked = true;
            rdbNormal_Yag_muc.Checked = true;
            rdbNormal_Yag_dis.Checked = true;
            rdbNormal_Cervix.Checked = true;
            rdbNormal_Ulterus.Checked = true;
            rdbNormal_Left_Adexa.Checked = true;
            rdbNormal_Right_Adexa.Checked = true;
            rdbNormal_Cul_de_sac.Checked = true;
        }

        public void LoadUi() 
        {
            uiAllLeft2.LoadDataAll();
            uiMenuBar1.LoadData();
        }
        private void LoadHistoryCallLast()
        {
            if (SetTprID == 0)
            {
                CallQueue.P_CallHistoryQueue();

            }
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void LoadObstetricHistoryData(int tprid)
        {
            try
            {
                var currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1).FirstOrDefault();
                if (currentRegis != null)
                {
                    trn_patient currentPT = dbc.trn_patients.Where(x => x.tpt_id == currentRegis.tpt_id).FirstOrDefault();

                    var objdocter_hdr = (from t1 in dbc.trn_obstetric_chiefs
                                         where t1.trn_patient_regi.trn_patient.tpt_hn_no == currentPT.tpt_hn_no
                                         select new
                                         {
                                             EN = t1.trn_patient_regi.tpr_en_no,
                                             ArriveDate = t1.trn_patient_regi.tpr_arrive_date,
                                             CreateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.toc_create_by).Single().mut_fullname,
                                             CreateDate = t1.toc_create_date,
                                             UpdateBy = dbc.mst_user_types.Where(c => c.mut_username == t1.toc_update_by).Single().mut_fullname,
                                             UpdateDate = t1.toc_update_date,
                                             sts = (t1.toc_doc_scan == true) ? "Completed" : "Not send to Docscan",
                                             Link = (t1.toc_doc_scan == true) ? "View" : "Send"
                                         }).ToList();
                    gvRecHistory.DataSource = objdocter_hdr;
                }     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CreateHeader()
        {
            this.gvRecHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing; //ยืดได้หดได้
            this.gvRecHistory.ColumnHeadersHeight = this.gvRecHistory.ColumnHeadersHeight * 2; //ขยายความยาวเพื่อเตรียมสำหรับการ Merge
            this.gvRecHistory.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter; //ให้ตัวหนังสืออยู่ ล่าง,กลาง
            //this.gvRecHistory.ColumnWidthChanged += new DataGridViewColumnEventHandler(gvRecHistory_ColumnWidthChanged);
        }
        private void gvRecHistory_Paint(object sender, PaintEventArgs e)
        {

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            Rectangle recA = this.gvRecHistory.GetCellDisplayRectangle(0, -1, true);
            recA.Width = recA.Width - 2;
            recA.Height = recA.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recA); //ทำการวาดพื้นลงไป ปรับแต่งค่าต่าง ๆ อยากได้สีอะไรเลือกเอาตามใจชอบนะครับ
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recA, format); //วาดชื่อ column ที่ทำการ Merge ปรับแต่งค่าต่าง ๆ (font,สี)

            Rectangle recA1 = this.gvRecHistory.GetCellDisplayRectangle(1, -1, true);
            recA1.Width = recA1.Width - 2;
            recA1.Height = recA1.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recA1); //ทำการวาดพื้นลงไป ปรับแต่งค่าต่าง ๆ อยากได้สีอะไรเลือกเอาตามใจชอบนะครับ
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recA1, format); //วาดชื่อ column ที่ทำการ Merge ปรับแต่งค่าต่าง ๆ (font,สี)

            Rectangle recA2 = this.gvRecHistory.GetCellDisplayRectangle(2, -1, true);
            recA2.Width = recA2.Width - 2;
            recA2.Height = recA2.Height / 2 - 2;
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recA2); //ทำการวาดพื้นลงไป ปรับแต่งค่าต่าง ๆ อยากได้สีอะไรเลือกเอาตามใจชอบนะครับ
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recA2, format); //วาดชื่อ column ที่ทำการ Merge ปรับแต่งค่าต่าง ๆ (font,สี)


            Rectangle recB = this.gvRecHistory.GetCellDisplayRectangle(3, -1, true);
            int widthH2 = this.gvRecHistory.Columns[1].Width;
            recB.Width = (recB.Width + widthH2) - 2; //กว้าง ครอบคลุม 2 column
            recB.Height = recB.Height / 2 - 2; //ยาว ครึ่งหนึ่ง
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recB); //ทำการวาดพื้นลงไป ปรับแต่งค่าต่าง ๆ อยากได้สีอะไรเลือกเอาตามใจชอบนะครับ
            e.Graphics.DrawString("Create", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recB, format); //วาดชื่อ column ที่ทำการ Merge ปรับแต่งค่าต่าง ๆ (font,สี)

            Rectangle recC = this.gvRecHistory.GetCellDisplayRectangle(5, -1, true);
            int widthH3 = this.gvRecHistory.Columns[1].Width;
            recC.Width = (recC.Width + widthH3) - 2; //กว้าง ครอบคลุม 2 column
            recC.Height = recC.Height / 2 - 2; //ยาว ครึ่งหนึ่ง
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recC); //ทำการวาดพื้นลงไป ปรับแต่งค่าต่าง ๆ อยากได้สีอะไรเลือกเอาตามใจชอบนะครับ
            e.Graphics.DrawString("Update", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recC, format); //วาดชื่อ column ที่ทำการ Merge ปรับแต่งค่าต่าง ๆ (font,สี)

            Rectangle recD = this.gvRecHistory.GetCellDisplayRectangle(7, -1, true);
            int widthH4 = this.gvRecHistory.Columns[1].Width;
            recD.Width = recD.Width - 2;
            recD.Height = recD.Height / 2 - 2; //ยาว ครึ่งหนึ่ง
            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), recD); //ทำการวาดพื้นลงไป ปรับแต่งค่าต่าง ๆ อยากได้สีอะไรเลือกเอาตามใจชอบนะครับ
            e.Graphics.DrawString("", new Font("tahoma", 10, FontStyle.Regular), Brushes.Black, recD, format); //วาดชื่อ column ที่ทำการ Merge ปรับแต่งค่าต่าง ๆ (font,สี)
        }

        private void frmObstetrics_Load(object sender, EventArgs e)
        {

            LoadHandlerCountDown();

            this.Text = Program.GetRoomName();
            uiFooter2.RoomCode = "PT";
            uiMenuBar1.ShowChangeDoctor("PT");
            frmBGScreen frmbg = new frmBGScreen(); frmbg.Show();
            Application.DoEvents();

            CreateHeader();
            ClickAutoRediobutton();
            LoadDrowDownList();
            loaddata(); 
            //uiFooter2.LoadData();

            frmbg.Close();
        }
        private void loaddata()
        {
            if (SetTprID > 0)
            {
                 OnUCFooterClicked(SetTprID, siteitem);
                return;
            }
            if (!Program.IsDummy)
            {
                LoadHistoryCallLast();
                //StatusCallQueueWaitingReady();
            }
            if (Program.CurrentRegis != null)
            {
                tabControl1.Enabled = true;
                var t11 = (from t1 in dbc.trn_obstetric_chiefs
                         where t1.tpr_id==Program.CurrentRegis.tpr_id  select t1);
                if (t11.Count() > 0)
                {
                    bindingSource1.DataSource = t11.FirstOrDefault();
                    ViewDataFrom();
                }
                else
                {
                    bindingSource1.DataSource = t11;
                    bindingSource1.AddNew();
                    trn_obstetric_chief obc = (trn_obstetric_chief)bindingSource1.Current;
                    if (obc.toc_followup_date == null)
                    {
                        obc.toc_followup_date = Program.GetServerDateTime().Date;
                    }
                    CallWebservice();//web service
                }
                LoadInvestigation(Program.CurrentRegis.tpr_id);
                //statusCallQueue();
                //StatusCallQueueWaitingReady();
                LoadObstetricHistoryData(Program.CurrentRegis.tpr_id);
                LoadUI();

                if (Program.CurrentPatient_queue != null)
                {
                    if (Program.CurrentPatient_queue.tps_status == "WK" && Program.CurrentPatient_queue.tps_ns_status == null)
                    {
                        StatusCallQueueReady();
                    }
                    else if (Program.CurrentPatient_queue.tps_status == "NS" && Program.CurrentPatient_queue.tps_ns_status == "WR")
                    {
                        StatusCallQueueWaitingReady();
                    }
                }

            }
            else
            {
                Clearfrm();
                StatusWaitCallQueue();
                if (Program.IsDummy)
                {
                    btnCallQueue.Enabled = false;
                }
            }
        }
        private void ViewDataFrom()
        {
            trn_obstetric_chief obc = (trn_obstetric_chief)bindingSource1.Current;
            Program.SetValueRadioGroup(pnlMarStatus, obc.toc_marry_status);
            Program.SetValueRadioGroup(pnlContraception, obc.toc_contraception);
            Program.SetValueRadioGroup(pnlHomrRepTher,obc.toc_homreplace);
            Program.SetValueRadioGroup(pnlHyster,obc.toc_hyster);
            Program.SetValueRadioGroup(pnlOopSal,obc.toc_oophorec);
            if (obc.toc_followup_date == null)
            {
                obc.toc_followup_date = Program.GetServerDateTime().Date;
            }
            //Tab1 :Para
            chkNormal.Checked=(obc.toc_tdel_normal=='Y');
            chkVacuum.Checked= (obc.toc_tdel_vacuum=='Y');
            chkFE.Checked=(obc.toc_tdel_force=='Y');
            chkCesa.Checked= (obc.toc_tdel_cesa =='Y');
            chkCSH.Checked  = (obc.toc_tdel_cesahy=='Y');
            chkOther.Checked  = (obc.toc_tdel_others=='Y');

            if (obc.toc_contraception == 'Y')
            {
                rdbOralCon.Checked=true;
                switch (obc.toc_contra_flag)
                {
                    case "OC":
                        rdbOralCon.Checked = true;
                        txtOralCon.Text = obc.toc_contra_remark;
                        txtOral_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtOral_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "ID":
                        rdbIUD.Checked = true;
                        txtIUD.Text = obc.toc_contra_remark;
                        txtIUD_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtIUD_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "IC":
                        rdbInjec.Checked = true;
                        txtInjec.Text = obc.toc_contra_remark;
                        txtInjec_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtInjec_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "CI":
                        rdbCon_Im.Checked = true;
                        txtInjec.Text = obc.toc_contra_remark;
                        txtCon_Im_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtCon_Im_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                    case "SL":
                        rdbSter.Checked = true;
                        txtSter.Text = obc.toc_contra_remark;
                        txtSter_D_Mos.Text = obc.toc_contra_dura_mos.Value.ToString();
                        txtSter_D_Yrs.Text = obc.toc_contra_dura_yrs.Value.ToString();
                        break;
                }
            }

            Program.SetValueRadioGroup(RDHyterectomypl, obc.toc_hyster_type);//Tab1  : Hysterectomy
            Program.SetValueRadioGroup(PlOophorectomy, obc.toc_oophorec_type); //Tab1  :Oophorectomy- Salpingo 
            //end Tab1

            //Tab2 Redio buttons
            Program.SetValueRadioGroup(pnlYolva,  obc.toc_vulva);
            Program.SetValueRadioGroup(pnlUrethar,obc.toc_uret);
            Program.SetValueRadioGroup(pnlBartholin,obc.toc_bart);
            Program.SetValueRadioGroup(pnYaMu,obc.toc_vamuc);
            
            Program.SetValueRadioGroup(pnlCervix,obc.toc_cervix);
            Program.SetValueRadioGroup(pnlUlterus_Size,obc.toc_uter_size);
            Program.SetValueRadioGroup(pnlUlterus_Tender,obc.toc_uter_tender);
            Program.SetValueRadioGroup(pnlAdexaLeft,obc.toc_adexa_left);
            Program.SetValueRadioGroup(pnlAdexaLeft_Tender,obc.toc_adexa_ltender);
            Program.SetValueRadioGroup(pnlAdexaLeft_Mass,obc.toc_adexa_lmass);
            Program.SetValueRadioGroup(pnlAdexaRight,obc.toc_adexa_right);
            Program.SetValueRadioGroup(pnlAdexaRight_Tender,obc.toc_adexa_rtender);
            Program.SetValueRadioGroup(pnlAdexaRight_Mass,obc.toc_adexa_rmass);
            Program.SetValueRadioGroup(pnlCul_de_sac,obc.toc_culdesac);

            ///tab2 : Yolva  
             chkVulvtis.Checked= (obc.toc_vulva_vulvitis=='Y');
            chkFollcutls.Checked=(obc.toc_vulva_folliculitis == 'Y');
            chkHerpes_simplex.Checked=(obc.toc_vulva_herpes == 'Y');
            chkCyst_Yolva.Checked=(obc.toc_vulva_cyst == 'Y');
            chkMass_Yolva.Checked=(obc.toc_vulva_mass == 'Y');
            chkOther_Yolva.Checked=(obc.toc_vulva_others == 'Y');
            //tab2 :Urethar  
            chkUrethrit.Checked=(obc.toc_uret_urethrit == 'Y');
            chkCon_ac.Checked=(obc.toc_uret_condyloma == 'Y');
            chkOther_Urethar.Checked=(obc.toc_uret_others == 'Y');
            //tab2 :Bartholin  
            chkBarthoinitis.Checked=(obc.toc_bart_barthol == 'Y');
            chkBarthoin_absc.Checked=(obc.toc_bart_barthol_absc == 'Y');
            chkBartholn_cyst.Checked=(obc.toc_bart_barthol_cyst == 'Y');
            //tab2 :Yaginal mucosa  :
            chkImflamed.Checked=(obc.toc_vamuc_imflam =='Y');
            chkThin_and_Dry.Checked=(obc.toc_vamuc_thindry =='Y');
            //tab2 : Yaginal discharge  : 
            Program.SetValueRadioGroup(pnlYaDis,obc.toc_vadis);            
            Program.SetValueRadioGroup(pnlYa_dis_Color,obc.toc_vadis_color);

            //tag2:Cervix 
            chkErosion.Checked= (obc.toc_cerv_erosion == 'Y');
           chkUceration.Checked = (obc.toc_cerv_ulceration == 'Y');
           chkMass_Cervix.Checked  = (obc.toc_cerv_mass=='Y');
           chkPolyp.Checked  = (obc.toc_cerv_polyp== 'Y');
           chkCyst_Cervix.Checked= ( obc.toc_cerv_cyst == 'Y');


        }
        private void LoadDrowDownList()
        {
            List<ComboboxItem> newitem = new List<ComboboxItem>();
            for (int i = 0; i < 21; i++)
            {
                newitem.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }
            cmbGravida.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbGravida.DisplayMember = "Display";
            cmbGravida.ValueMember = "Valuedata";

            cmbLiChil.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbLiChil.DisplayMember = "Display";
            cmbLiChil.ValueMember = "Valuedata";

            cmbPara.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbPara.DisplayMember = "Display";
            cmbPara.ValueMember = "Valuedata";


            // morn edit showitem to 10
            List<ComboboxItem> itemAbor = new List<ComboboxItem>();
            for (int i = 1; i <= 10; i++)
            {
                itemAbor.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }
            cmbAbortion.DataSource = (from t1 in itemAbor select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbAbortion.DisplayMember = "Display";
            cmbAbortion.ValueMember = "Valuedata";
            // morn edit showitem to 5
            List<ComboboxItem> itemEcto = new List<ComboboxItem>();
            for (int i = 0; i <= 5; i++)
            {
                itemEcto.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }
            cmbEctopic.DataSource = (from t1 in itemEcto select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbEctopic.DisplayMember = "Display";
            cmbEctopic.ValueMember = "Valuedata";
            // morn

            cmbLast_mens_period.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cmbLast_mens_period.DisplayMember = "Display";
            cmbLast_mens_period.ValueMember = "Valuedata";

            
        }
        private void NewInvestigation(int tprid)
        {
            var ctObstatic = (trn_obstetric_chief)bindingSource1.Current;

            var objmemograrm= (from t1 in dbc.trn_mammograms
                               where t1.tpr_id == tprid
                               orderby t1.tmg_result_date descending
                               select t1).FirstOrDefault();
            if(objmemograrm!=null){
                ctObstatic.toc_inves_mammogram = objmemograrm.tmg_result;
                //txtinvestigationMemogrm.Text=objmemograrm.tmg_result;
            }
            var objUltrasound = (from t1 in dbc.trn_ultrasounds
                                 where t1.tpr_id == tprid
                                 && t1.tus_ultra_type=="UL"
                                 orderby t1.tus_result_date descending
                                 select t1).FirstOrDefault();
            if (objUltrasound != null)
            {
                //txtInvestigationFindingUltrasoundLower.Text=objUltrasound.tus_result;
                ctObstatic.toc_inves_ultrasound = objUltrasound.tus_result;
            }
        }
        private void LoadInvestigation(int tprid)
        {
            var ctObstatic = (trn_obstetric_chief)bindingSource1.Current;

            //noina check is null
            int len_toc_inves_mammogram = 0;
            int len_toc_inves_mammogram_sum = 0;
            if (ctObstatic != null) { len_toc_inves_mammogram = (ctObstatic.toc_inves_mammogram == null ? 0 : ctObstatic.toc_inves_mammogram.Length); len_toc_inves_mammogram_sum = (ctObstatic.toc_inves_mammogram_sum == null ? 0 : ctObstatic.toc_inves_mammogram_sum.Length); }

            if (len_toc_inves_mammogram == 0 && len_toc_inves_mammogram_sum == 0)
            {
                var objmemograrm = (from t1 in dbc.trn_mammograms
                                    where t1.tpr_id == tprid
                                    orderby t1.tmg_result_date descending
                                    select t1).FirstOrDefault();
                if (objmemograrm != null)
                {
                    ctObstatic.toc_inves_mammogram = objmemograrm.tmg_result;
                    //txtinvestigationMemogrm.Text=objmemograrm.tmg_result;
                }
            }


            int len_toc_inves_ultrasound = 0;
            int len_toc_inves_ultrasound_sum = 0;

            if (ctObstatic != null) { len_toc_inves_ultrasound = (ctObstatic.toc_inves_ultrasound == null ? 0 : ctObstatic.toc_inves_ultrasound.Length); len_toc_inves_ultrasound_sum = (ctObstatic.toc_inves_ultrasound_sum == null ? 0 : ctObstatic.toc_inves_ultrasound_sum.Length); }

            if (len_toc_inves_ultrasound == 0 && len_toc_inves_ultrasound_sum == 0)
            {
                var objUltrasound = (from t1 in dbc.trn_ultrasounds
                                     where t1.tpr_id == tprid
                                     && t1.tus_ultra_type == "UL"
                                     orderby t1.tus_result_date descending
                                     select t1).FirstOrDefault();
                if (objUltrasound != null)
                {
                    //txtInvestigationFindingUltrasoundLower.Text=objUltrasound.tus_result;
                    ctObstatic.toc_inves_ultrasound = objUltrasound.tus_result;
                }
            }
        }

        private void CallWebservice()
        {
            if (Program.CurrentRegis != null)
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    int tprid = Program.CurrentRegis.tpr_id;
                    trn_patient_regi currentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1).FirstOrDefault();
                    if (currentRegis != null)
                    {
                        string HNno = currentRegis.trn_patient.tpt_hn_no;
                        string ENno = currentRegis.tpr_en_no;
                        string Doctorcode = Program.CurrentUser.mut_username;

                        var currentobsChief = (trn_obstetric_chief)bindingSource1.Current;


                        DataTable dt = ws.GetDiagnosisByDoctor("01-92-006363", "O01-12-876565", "001915668");//(HNno, ENno, Doctorcode); //
                        gvDiagnosis.AutoGenerateColumns = false;
                        foreach (DataRow dr in dt.Rows)
                        {
                            trnobstetricdiagsBindingSource.AddNew();
                            var pa = (trn_obstetric_diag)trnobstetricdiagsBindingSource.Current;
                            pa.toc_id = currentobsChief.toc_id;
                            pa.obg_diag_code = dr["MRCID_Code"].ToString();
                            pa.obg_diag_desc = dr["MRCID_Desc"].ToString();
                            pa.obg_diag_type = dr["DTYP_Desc"].ToString();
                            pa.obg_diag_date = Convert.ToDateTime(dr["MRDIA_Date"]);
                            pa.obg_create_by = Program.CurrentUser.mut_username;
                            pa.obg_update_by = pa.obg_create_by;
                            pa.obg_create_date = Program.GetServerDateTime();
                            pa.obg_update_date = pa.obg_create_date;
                        }
                        //gvDiagnosis.DataSource = dt;

                        DataTable dt2 = ws.GetMedicineByDoctor("01-92-006363", "O01-12-876565", "001915668");//(HNno, ENno, Doctorcode);//
                        gvMedicationTreatment.AutoGenerateColumns = false;
                        foreach (DataRow dr in dt2.Rows)
                        {
                            trnobstetricmedsBindingSource.AddNew();
                            var pa = (trn_obstetric_med)trnobstetricmedsBindingSource.Current;
                            pa.toc_id = currentobsChief.toc_id;
                            pa.obm_med_code = dr["ARCIM_Code"].ToString();
                            pa.obm_med_desc = dr["ARCIM_Desc"].ToString();
                            pa.obm_desc = dr["PHCFR_Desc1"].ToString();
                            pa.obm_qty = dr["OEORI_DoseQty"].ToString();
                            pa.obm_unit = dr["CTUOM_Desc2"].ToString();
                            pa.obm_eat_unit = dr["PHCIN_Desc1"].ToString();
                            pa.obm_create_by = Program.CurrentUser.mut_username;
                            pa.obm_update_by = pa.obm_create_by;
                            pa.obm_create_date = Program.GetServerDateTime();
                            pa.obm_update_date = pa.obm_create_date;
                        }
                        // gvMedicationTreatment.DataSource = dt2;
                    }
                }
            }
        }

        private void rdbYes_Contrac_CheckedChanged(object sender, EventArgs e)
        {
            grpContraception.Enabled = true;
        }
        private void rdbNo_Contrac_CheckedChanged(object sender, EventArgs e)//Contraception tab1
        {
            grpContraception.Enabled = false;     
        }
        private void rdbNormal_Yolva_CheckedChanged(object sender, EventArgs e)//frist tab2
        {
            chkVulvtis.Enabled = false;
            chkFollcutls.Enabled = false;
            chkHerpes_simplex.Enabled = false;
            chkCyst_Yolva.Enabled = false;
            txtCyst_Yolva.Enabled = false;
            chkMass_Yolva.Enabled = false;
            txtMass_Yolva.Enabled = false;
            chkOther_Yolva.Enabled = false;
            txtOther_Yolva.Enabled = false;
            lblYCyst.Enabled = false;
            lblYMass.Enabled = false;
        }
        private void rdbAbnormal_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            grpYolva.Enabled = true;
            chkVulvtis.Enabled = true;
            chkFollcutls.Enabled = true;
            chkHerpes_simplex.Enabled = true;
            chkCyst_Yolva.Enabled = true;
            txtCyst_Yolva.Enabled = true;
            chkMass_Yolva.Enabled = true;
            txtMass_Yolva.Enabled = true;
            chkOther_Yolva.Enabled = true;
            txtOther_Yolva.Enabled = true;
            lblYCyst.Enabled = true;
            lblYMass.Enabled = true;
            
        }
        private void rdbYes_Homr_Rep_Ther_CheckedChanged(object sender, EventArgs e)
        {
            txtHomr_RepTher.Enabled = true;
            label59.Enabled = true;
            label27.Enabled = true;
            label25.Enabled = true;
            txtHomr_RepTher_D_Mos.Enabled = true;
            txtHomr_RepTher_D_Yrs.Enabled = true;
        }
        private void rdbNo_Homr_Rep_Ther_CheckedChanged(object sender, EventArgs e)
        {
            txtHomr_RepTher.Enabled = false;
            label59.Enabled = false;
            label27.Enabled = false;
            label25.Enabled = false;
            txtHomr_RepTher_D_Mos.Enabled = false;
            txtHomr_RepTher_D_Yrs.Enabled = false;
        }
        private void rdbYes_Hyterectomy_CheckedChanged(object sender, EventArgs e)//tab1
        {
            grpHysterectomy.Enabled = true;
        }
        private void rdbYes_Oophorectomy_CheckedChanged(object sender, EventArgs e)//tab1
        {
            grpOop.Enabled = true;
        }
        private void rdbNo_Hyterectomy_CheckedChanged(object sender, EventArgs e)
        {
            grpHysterectomy.Enabled = false;
        }
        private void rdbNo_Oophorectomy_CheckedChanged(object sender, EventArgs e)
        { 
            grpOop.Enabled = false;
        }
        private void rdbNormal_Urethar_CheckedChanged(object sender, EventArgs e)
        {
            chkUrethrit.Enabled = false;
            chkCon_ac.Enabled = false;
            chkOther_Urethar.Enabled = false;
            txtOther_Urethar.Enabled = false;
        }
        private void rdbrdbAbnormal_Urethar_CheckedChanged(object sender, EventArgs e)//tab2
        {
            chkUrethrit.Enabled = true;
            chkCon_ac.Enabled = true;
            chkOther_Urethar.Enabled = true;
            txtOther_Urethar.Enabled = true;
        }
        private void rdbAbnormal_Bartholin_CheckedChanged(object sender, EventArgs e)//tab2 //3
        {
            chkBarthoinitis.Enabled = true;
            chkBarthoin_absc.Enabled = true;
            chkBartholn_cyst.Enabled = true;
            txtBartholin.Enabled = true;
            lblBartholin.Enabled = true;
        }
        private void rdbNormal_Bartholin_CheckedChanged(object sender, EventArgs e)
        {
            chkBarthoinitis.Enabled = false;
            chkBarthoin_absc.Enabled = false;
            chkBartholn_cyst.Enabled = false;
            txtBartholin.Enabled = false;
            lblBartholin.Enabled = false;
        }
        private void rdbAbnormal_Yag_muc_CheckedChanged(object sender, EventArgs e)//tab2/4
        {
            chkImflamed.Enabled = true;
            chkThin_and_Dry.Enabled = true;
        }
        private void rdbNormal_Yag_muc_CheckedChanged(object sender, EventArgs e)
        {
            chkImflamed.Enabled = false;
            chkThin_and_Dry.Enabled = false;
        }
        private void rdbAbnormal_Yag_dis_CheckedChanged(object sender, EventArgs e)//tab2/5
        {
            rdbWhite.Enabled = true;
            rdbYellow.Enabled = true;
            rdbBloody.Enabled = true;
            rdbCurd_like.Enabled = true;
            rdbPurulent.Enabled = true;
            rdbMucoprulent.Enabled = true;
        }
        private void rdbNormal_Yag_dis_CheckedChanged(object sender, EventArgs e)
        {
            rdbWhite.Enabled = false;
            rdbYellow.Enabled = false;
            rdbBloody.Enabled = false;
            rdbCurd_like.Enabled = false;
            rdbPurulent.Enabled = false;
            rdbMucoprulent.Enabled = false;
        }
        private void rdbAbnormal_Cervix_CheckedChanged(object sender, EventArgs e)//tab2/6
        {
            chkErosion.Enabled = true;
            chkUceration.Enabled = true;
            chkMass_Cervix.Enabled = true;
            lblMass_Cervix.Enabled = true;
            chkPolyp.Enabled = true;
            chkErosion.Enabled = true;
            label61.Enabled = true;
            chkCyst_Cervix.Enabled = true;
            lblCyst_Cervix.Enabled = true;
            txtMass_Cervix.Enabled = true;
            txtPoly.Enabled = true;
            txtCyst_Cervix.Enabled = true;
        }
        private void rdbNormal_Cervix_CheckedChanged(object sender, EventArgs e)
        {
            chkErosion.Enabled = false;
            chkUceration.Enabled = false;
            chkMass_Cervix.Enabled = false;
            lblMass_Cervix.Enabled = false;
            chkPolyp.Enabled = false;
            chkErosion.Enabled = false;
            label61.Enabled = false;
            chkCyst_Cervix.Enabled = false;
            lblCyst_Cervix.Enabled = false;
        }
        private void rdbAbnormal_Ulterus_CheckedChanged(object sender, EventArgs e)//tab2/7
        {
            if (rdbAbnormal_Ulterus.Checked)
            {
                label19.Enabled = true;
                txtEnlarged_size.Enabled = true;
                label20.Enabled = true;
            }
            else
            {
                txtEnlarged_size.Enabled = false;
                txtEnlarged_size.Text = "";
                label20.Enabled = false;
                label19.Enabled = false;
            }
        }
        private void rdbNormal_Ulterus_CheckedChanged(object sender, EventArgs e)//tab2/7
        {
            label19.Enabled = false;
            txtEnlarged_size.Enabled = false;
            label20.Enabled = false;
        }
        private void rdbAbnormal_Left_Adexa_CheckedChanged(object sender, EventArgs e)//tab2/8
        {
            grpAdexaLeftTender.Enabled = true;
            grpAdexaLeftMass.Enabled = true;
        }
        private void rdbNormal_Left_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            grpAdexaLeftTender.Enabled = false;
            grpAdexaLeftMass.Enabled = false;
        }
        private void rdbAbnormal_Right_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            grpAdexaRightTender.Enabled = true;
            grpAdexaRightMass.Enabled = true;
        }
        private void rdbNormal_Right_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            grpAdexaRightTender.Enabled = false;
            grpAdexaRightMass.Enabled = false;
        }
        private void chkMass_Cervix_CheckedChanged(object sender, EventArgs e)
        {
            if(chkMass_Cervix.Checked)
            {
                txtMass_Cervix.Visible=true;
                lblMass_Cervix.Visible=true;
            }
            else
            {
                txtMass_Cervix.Text = "";
                 txtMass_Cervix.Visible=false;
                lblMass_Cervix.Visible=false;
            }
        }

        Boolean iscompleted = false;
        private void disableBtnWhenSave()
        {
            btnSaveDraft.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendManual.Enabled = false;
            btnSendToCheckB.Enabled = false;
        }
        private void enableBtnWhenSave()
        {
            btnSaveDraft.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendManual.Enabled = true;
            btnSendToCheckB.Enabled = true;
        }
        private void btnSendAuto_Click(object sender, EventArgs e)
        {
            this.AutoScrollPosition = new Point(0, 0);
            disableBtnWhenSave();
            DateTime startDate = DateTime.Now;
            int tpr_id = Program.CurrentRegis.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;
            Class.FunctionDataCls func = new Class.FunctionDataCls();
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            SaveData('N');

            if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
            {
                List<int> mvt = mst.GetMstRoomEventByMrm(Program.CurrentRoom.mrm_id).Select(x => x.mvt_id).ToList();

                Class.FunctionDataCls.sendQueueStatus result = func.sendQueueUltrasoundLower(resultUltrasound, mvt);
                if (result == Class.FunctionDataCls.sendQueueStatus.error)
                {
                    lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถส่งไป ultrasound ได้ กรุณาติดต่อผู้ดูแลระบบ";
                    disableBtnWhenSave();
                }
                else if (result == Class.FunctionDataCls.sendQueueStatus.sendSuccess)
                {
                    new ClsTCPClient().sendClearUnitDisplay();
                    Clearfrm();
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = func.GetStrSaveAndSend(tpr_id, "US", "UL");
                }
            }
            else
            {
                if (new Class.FunctionDataCls().ChkSendAutoNewModule(Program.CurrentRegis))
                {
                    string msgAlert = "";
                    bool isPopup = false;
                    QueueClass.SendAutoCls.ResultSendQueue result = new QueueClass.SendAutoCls().SendAuto(tps_id, Program.CurrentUser, ref msgAlert, ref isPopup);
                    if (result == QueueClass.SendAutoCls.ResultSendQueue.Error)
                    {
                        lbAlertMsg.Text = msgAlert;
                        enableBtnWhenSave();
                    }
                    else
                    {
                        lbAlertMsg.Visible = true;
                        if (result == QueueClass.SendAutoCls.ResultSendQueue.SendComplete)
                        {
                            new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                        tpr_id,
                                                        tps_id,
                                                        Program.CurrentSite.mhs_id,
                                                        Program.CurrentRoom.mrd_ename,
                                                        Program.CurrentUser.mut_username,
                                                        startDate);

                            new ClsTCPClient().sendClearUnitDisplay();
                            new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                            Clearfrm();
                            StatusWaitCallQueue();
                            if (isPopup)
                            {
                                MessageBox.Show(msgAlert, "EMR Checkup.");
                            }
                            else
                            {
                                lbAlertMsg.Text = msgAlert;
                            }
                        }
                        else
                        {
                            if (isPopup)
                            {
                                MessageBox.Show(msgAlert, "EMR Checkup.");
                            }
                            else
                            {
                                lbAlertMsg.Text = msgAlert;
                            }
                        }
                    }
                }
                else
                {
                    StatusTransaction result = CallQueue.SendAutoOnStation();
                    if (result == StatusTransaction.True || result == StatusTransaction.SendCheckB)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendAuto,
                                                    tpr_id,
                                                    tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username,
                                                    startDate);
                        
                        new ClsTCPClient().sendClearUnitDisplay();
                        Clearfrm();
                        StatusWaitCallQueue();
                        new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                        lbAlertMsg.Text = func.getStringGotoNextRoom(tpr_id);
                    }
                    else if (result == StatusTransaction.Error)
                    {
                        enableBtnWhenSave();
                        lbAlertMsg.Text = "โปรดกด Send Auto อีกครั้ง";
                    }
                    else if (result == StatusTransaction.False)
                    {
                        enableBtnWhenSave();
                        lbAlertMsg.Text = "Save Data Complete.";
                    }
                }
            }
        }
        private void btnSaveDraft_Click(object sender, EventArgs e)
        {
            int tpr_id;
            if (Program.CurrentRegis != null)
            {
                tpr_id = Program.CurrentRegis.tpr_id;
            }
            else
            {
                tpr_id = SetTprID;
            }
            if (Program.chkBookComplete(tpr_id))
            {
                if (SaveData('D'))
                {
                    lbAlertMsg.Focus();
                    lbAlertMsg.Text = "Save Data Completed.";
                    btnSendToDocScan.Enabled = true;
                    LoadObstetricHistoryData(Program.CurrentRegis == null ? SetTprID : Program.CurrentRegis.tpr_id);
                }
            }
        }
        private void btnSendManual_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            lbAlertMsg.Text = "";

            this.AutoScrollPosition = new Point(0, 0);
            int tpr_id = Program.CurrentRegis.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;

            string messegeAlert = "";

            SaveData('N');
            if (iscompleted)
            {
                lbAlertMsg.Text = "";
                StatusTransaction result = new SendManaulCls().SendManualOnStation(ref messegeAlert);
                if (result == StatusTransaction.True)
                {
                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendManual,
                                                tpr_id,
                                                tps_id,
                                                Program.CurrentSite.mhs_id,
                                                Program.CurrentRoom.mrd_ename,
                                                Program.CurrentUser.mut_username);

                    new ClsTCPClient().sendClearUnitDisplay();
                    Clearfrm();
                    StatusWaitCallQueue();
                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                    lbAlertMsg.Text = messegeAlert;
                }
                else if (result == StatusTransaction.False)
                {
                    enableBtnWhenSave();
                    lbAlertMsg.Text = "Save Data Completed.";
                }
                else if (result == StatusTransaction.Error)
                {
                    enableBtnWhenSave();
                    lbAlertMsg.Text = "โปรด send manaul อีกครั้ง";
                }
            }
            else
            {
                enableBtnWhenSave();
                lbAlertMsg.Text = "เกิดความผิดพลาดของระบบ โปรด send manaul อีกครั้ง";
            }
        }
        private bool SaveData(char strType)
        {
            if (bindingSource1.DataSource != null)
            {
                DateTime datenow = Program.GetServerDateTime();
                Boolean saveIsCompleted = false;
                iscompleted = false;
                try
                {
                    var obc = (trn_obstetric_chief)this.bindingSource1.Current;
                    // set Radio
                    if (SetTprID == 0)
                    {
                        obc.tpr_id = Program.CurrentRegis.tpr_id;

                        obc.toc_advise_plan = txtadvise_plan.Text;

                        if (obc.toc_create_by == null)
                        {
                            obc.toc_create_by = Program.CurrentUser.mut_username;
                            obc.toc_create_date = Program.GetServerDateTime();

                            obc.toc_doc_code = obc.toc_create_by;
                            obc.toc_doc_name = Program.CurrentUser.mut_fullname;
                        }
                    }

                 
                    obc.toc_type = strType;
                    obc.toc_marry_status = Program.GetValueRadioTochar(pnlMarStatus);
                    obc.toc_contraception = Program.GetValueRadioTochar(pnlContraception);
                    obc.toc_homreplace = Program.GetValueRadioTochar(pnlHomrRepTher);
                    obc.toc_hyster = Program.GetValueRadioTochar(pnlHyster);
                    obc.toc_oophorec = Program.GetValueRadioTochar(pnlOopSal);
                    //Tab1 :Para
                    obc.toc_tdel_normal = (chkNormal.Checked) ? 'Y' : 'N';
                    obc.toc_tdel_vacuum = (chkVacuum.Checked) ? 'Y' : 'N';
                    obc.toc_tdel_force = (chkFE.Checked) ? 'Y' : 'N';
                    obc.toc_tdel_cesa = (chkCesa.Checked) ? 'Y' : 'N';
                    obc.toc_tdel_cesahy = (chkCSH.Checked) ? 'Y' : 'N';
                    obc.toc_tdel_others = (chkOther.Checked) ? 'Y' : 'N';
                    //Tab1 : Contraception 
                    if (rdbOralCon.Checked)
                    {
                        obc.toc_contra_flag = (string)rdbOralCon.Tag;
                        obc.toc_contra_remark = txtOralCon.Text;
                        obc.toc_contra_dura_mos = Convert1.ToInt32(txtOral_D_Mos.Text);
                        obc.toc_contra_dura_yrs = Convert1.ToInt32(txtOral_D_Yrs.Text);
                    }
                    if (rdbIUD.Checked)
                    {
                        obc.toc_contra_flag = (string)rdbIUD.Tag;
                        obc.toc_contra_remark = txtIUD.Text;
                        obc.toc_contra_dura_mos = Convert1.ToInt32(txtIUD_D_Mos.Text);
                        obc.toc_contra_dura_yrs = Convert1.ToInt32(txtIUD_D_Yrs.Text);
                    }
                    if (rdbInjec.Checked)
                    {
                        obc.toc_contra_flag = (string)rdbInjec.Tag;
                        obc.toc_contra_remark = txtInjec.Text;
                        obc.toc_contra_dura_mos = Convert1.ToInt32(txtInjec_D_Mos.Text);
                        obc.toc_contra_dura_yrs = Convert1.ToInt32(txtInjec_D_Yrs.Text);
                    }
                    if (rdbCon_Im.Checked)
                    {
                        obc.toc_contra_flag = (string)rdbCon_Im.Tag;
                        obc.toc_contra_remark = txtCon_Im.Text;
                        obc.toc_contra_dura_mos = Convert1.ToInt32(txtCon_Im_D_Mos.Text);
                        obc.toc_contra_dura_yrs = Convert1.ToInt32(txtCon_Im_D_Yrs.Text);
                    }
                    if (rdbSter.Checked)
                    {
                        obc.toc_contra_flag = (string)rdbSter.Tag;
                        obc.toc_contra_remark = txtSter.Text;
                        obc.toc_contra_dura_mos = Convert1.ToInt32(txtSter_D_Mos.Text);
                        obc.toc_contra_dura_yrs = Convert1.ToInt32(txtSter_D_Yrs.Text);
                    }

                    obc.toc_hyster_type = Program.GetValueRadio(RDHyterectomypl);//Tab1  : Hysterectomy
                    obc.toc_oophorec_type = Program.GetValueRadio(PlOophorectomy); //Tab1  :Oophorectomy- Salpingo 
                    //end Tab1

                    //Tab2 Redio buttons
                    obc.toc_vulva = Program.GetValueRadioTochar(pnlYolva);
                    obc.toc_uret = Program.GetValueRadioTochar(pnlUrethar);
                    obc.toc_bart = Program.GetValueRadioTochar(pnlBartholin);
                    obc.toc_vamuc = Program.GetValueRadioTochar(pnYaMu);
                    obc.toc_vadis = Program.GetValueRadioTochar(pnlYaDis);
                    obc.toc_cervix = Program.GetValueRadioTochar(pnlCervix);
                    obc.toc_uter_size = Program.GetValueRadioTochar(pnlUlterus_Size);
                    obc.toc_uter_tender = Program.GetValueRadioTochar(pnlUlterus_Tender);
                    obc.toc_adexa_left = Program.GetValueRadioTochar(pnlAdexaLeft);
                    obc.toc_adexa_ltender = Program.GetValueRadioTochar(pnlAdexaLeft_Tender);
                    obc.toc_adexa_lmass = Program.GetValueRadioTochar(pnlAdexaLeft_Mass);
                    obc.toc_adexa_right = Program.GetValueRadioTochar(pnlAdexaRight);
                    obc.toc_adexa_rtender = Program.GetValueRadioTochar(pnlAdexaRight_Tender);
                    obc.toc_adexa_rmass = Program.GetValueRadioTochar(pnlAdexaRight_Mass);
                    obc.toc_culdesac = Program.GetValueRadioTochar(pnlCul_de_sac);
                    ///tab2 : Yolva  
                    obc.toc_vulva_vulvitis = (chkVulvtis.Checked) ? 'Y' : 'N';
                    obc.toc_vulva_folliculitis = (chkFollcutls.Checked) ? 'Y' : 'N';
                    obc.toc_vulva_herpes = (chkHerpes_simplex.Checked) ? 'Y' : 'N';
                    obc.toc_vulva_cyst = (chkCyst_Yolva.Checked) ? 'Y' : 'N';
                    obc.toc_vulva_mass = (chkMass_Yolva.Checked) ? 'Y' : 'N';
                    obc.toc_vulva_others = (chkOther_Yolva.Checked) ? 'Y' : 'N';
                    //tab2 :Urethar  
                    obc.toc_uret_urethrit = (chkUrethrit.Checked) ? 'Y' : 'N';
                    obc.toc_uret_condyloma = (chkCon_ac.Checked) ? 'Y' : 'N';
                    obc.toc_uret_others = (chkOther_Urethar.Checked) ? 'Y' : 'N';
                    //tab2 :Bartholin  
                    obc.toc_bart_barthol = (chkBarthoinitis.Checked) ? 'Y' : 'N';
                    obc.toc_bart_barthol_absc = (chkBarthoin_absc.Checked) ? 'Y' : 'N';
                    obc.toc_bart_barthol_cyst = (chkBartholn_cyst.Checked) ? 'Y' : 'N';
                    //tab2 :Yaginal mucosa  :
                    obc.toc_vamuc_imflam = (chkImflamed.Checked) ? 'Y' : 'N';
                    obc.toc_vamuc_thindry = (chkThin_and_Dry.Checked) ? 'Y' : 'N';
                    //tab2 : Yaginal discharge  : 
                    obc.toc_vadis_color = Program.GetValueRadioTochar(pnlYa_dis_Color);
                    //tag2:Cervix 
                    obc.toc_cerv_erosion = (chkErosion.Checked) ? 'Y' : 'N';
                    obc.toc_cerv_ulceration = (chkUceration.Checked) ? 'Y' : 'N';
                    obc.toc_cerv_mass = (chkMass_Cervix.Checked) ? 'Y' : 'N';
                    obc.toc_cerv_polyp = (chkPolyp.Checked) ? 'Y' : 'N';
                    obc.toc_cerv_cyst = (chkCyst_Cervix.Checked) ? 'Y' : 'N';
                    obc.toc_update_by = Program.CurrentUser.mut_username;
                    obc.toc_update_date = Program.GetServerDateTime() ;
                    //Memo Sincege
                    obc.toc_meno_total = (obc.toc_meno_sinceage);

                    obc.toc_result = false;
                    //tab2:

                    //Investigation Finding by noina
                    obc.toc_advise_plan = txtadvise_plan.Text;
                    obc.toc_inves_mammogram_sum = txtmm_remark.Text;
                    obc.toc_inves_ultrasound_sum = txtultra_remark.Text;
                    obc.toc_diagnosis = txtdiag_remark.Text;

                    ////Update trn_patient_regis
                    var objPatientRegis = (from t in dbc.trn_patient_regis where t.tpr_id == Program.CurrentRegis.tpr_id select t).FirstOrDefault();
                    if (objPatientRegis.tpr_PRM == true)
                    {
                        objPatientRegis.tpr_pending = false;
                        objPatientRegis.tpr_PRM_doctor = true;
                        objPatientRegis.tpr_PRM_doccode = Program.CurrentUser.mut_username;
                        objPatientRegis.tpr_PRM_docname = Program.CurrentUser.mut_fullname;
                    }
                    ////EndUpdate trn_patient_regis
                    trnobstetricdiagsBindingSource.EndEdit();
                    trnobstetricmedsBindingSource.EndEdit();
                    bindingSource1.EndEdit();
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
                    saveIsCompleted = true;
                    iscompleted = true;
                }
                catch (Exception ex)
                {
                    Program.MessageError(this.Name, "SaveData", ex, false);
                }

                return saveIsCompleted;
            }
            else
            {
                return false;
            }
        }
       
        private void Clearfrm()
        {
            Program.CurrentRegis = null;
            Program.CurrentPatient_queue = null;

            chkNormal.Checked = false;
            chkCesa.Checked = false;
            chkCSH.Checked = false;
            chkOther.Checked = false;
            chkVacuum.Checked = false;
            chkCesa.Checked = false;
            chkFE.Checked = false;
            txtNormal.Text = "";
            txtVacEx.Text = "";
            txtFE.Text = "";
            txtCesaSec.Text = "";
            txtCSH.Text = "";
            txtOther.Text = "";
            rdbSubtotal_Hysterectomy.Checked = true;
            rdbUnilateral_sal_oophc.Checked = true;
            rdbSubtotal_Hysterectomy.Checked = false;
            rdbUnilateral_sal_oophc.Checked = false;

            chkVulvtis.Checked = false;
            chkCyst_Yolva.Checked = false;
            chkFollcutls.Checked = false;
            chkMass_Yolva.Checked = false;
            chkHerpes_simplex.Checked = false;
            chkOther_Yolva.Checked = false;
            txtCyst_Yolva.Text = "";
            txtMass_Yolva.Text = "";
            txtOther_Yolva.Text = "";
            chkUrethrit.Checked = false;
            chkCon_ac.Checked = false;
            chkOther_Urethar.Checked = false;
            txtOther_Urethar.Text = "";

            chkBarthoinitis.Checked = false;
            chkBarthoin_absc.Checked = false;
            chkBartholn_cyst.Checked = false;
            txtBartholin.Text = "";

            chkImflamed.Checked = false;
            chkThin_and_Dry.Checked = false;

            chkErosion.Checked = false;
            chkUceration.Checked = false;
            chkMass_Cervix.Checked = false;
            chkPolyp.Checked = false;
            chkCyst_Cervix.Checked = false;
            txtMass_Cervix.Text = "";
            txtPoly.Text = "";
            txtCyst_Cervix.Text = "";

            rdbWhite.Checked = true;
            rdbWhite.Checked = false;
            //Tender
            rdbNo_Tender_Ulterus.Checked = true;
            //Adexa
            rdbNo_Tender_Left_Adexa.Checked = true;
            rdbNo_Tender_Left_Adexa.Checked = false;
            rdbNo_Tender_Right_Adexa.Checked = true;
            rdbNo_Tender_Right_Adexa.Checked = false;
            //Mass
            rdbNo_Mass_Left_Adexa.Checked = true;
            rdbNo_Mass_Right_Adexa.Checked = true;
            rdbNo_Mass_Left_Adexa.Checked = false;
            rdbNo_Mass_Right_Adexa.Checked = false;

            rdbSingle.Checked = true;
            rdbNo_Contrac.Checked = true;
            rdbNo_Homr_Rep_Ther.Checked = true;
            rdbNo_Hyterectomy.Checked = true;
            rdbNo_Oophorectomy.Checked = true;
            rdbNormal_Yolva.Checked = true;
            rdbNormal_Urethar.Checked = true;
            rdbNormal_Bartholin.Checked = true;
            rdbNormal_Yag_muc.Checked = true;
            rdbNormal_Yag_dis.Checked = true;
            rdbNormal_Cervix.Checked = true;
            rdbNormal_Ulterus.Checked = true;
            rdbNormal_Left_Adexa.Checked = true;
            rdbNormal_Right_Adexa.Checked = true;
            rdbNormal_Cul_de_sac.Checked = true;

            rdbOralCon.Checked = false;
            rdbIUD.Checked = false;
            rdbInjec.Checked = false;
            rdbCon_Im.Checked = false;
            rdbSter.Checked = false;

            txtOralCon.Text = "";
            txtIUD.Text = "";
            txtInjec.Text = "";
            txtCon_Im.Text = "";
            txtSter.Text = "";

            txtOral_D_Mos.Text = "";
            txtIUD_D_Mos.Text = "";
            txtInjec_D_Mos.Text = "";
            txtCon_Im_D_Mos.Text = "";
            txtSter_D_Mos.Text = "";

            txtOral_D_Yrs.Text = "";
            txtIUD_D_Yrs.Text = "";
            txtInjec_D_Yrs.Text = "";
            txtCon_Im_D_Yrs.Text = "";
            txtSter_D_Yrs.Text = "";

            txtSince_Age.Text="";
            txtSince_Years.Text = "";
            txtMeno_Total.Text = "";
            bindingSource1.DataSource = new trn_obstetric_chief();

            LoadUI();
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
                    if (Program.CurrentRegis != null)
                    {
                        LoadUI();
                        clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                        try
                        {
                            trn_obstetric_chief objoc = dbc.trn_obstetric_chiefs.Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                            if (objoc != null)
                            {
                                bindingSource1.DataSource = objoc;
                                LoadInvestigation(Program.CurrentRegis.tpr_id);
                                tabControl1.Enabled = true;
                            }
                            else
                            {
                                var t11 = (from t1 in dbc.trn_obstetric_chiefs select t1);
                                bindingSource1.DataSource = t11;
                                bindingSource1.AddNew();
                                this.NewInvestigation(Program.CurrentRegis.tpr_id);
                                tabControl1.Enabled = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.MessageError(this.Name, "btnCallQueue_Click", ex, false);
                        }
                        StatusCallQueueWaitingReady();
                    }
                    else
                    {
                        Clearfrm();
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
            //if (result == StatusTransaction.True)
            //{
            //    // morn clear Unit Display
            //    new ClsTCPClient().sendClearUnitDisplay();
            //    // morn clear Unit Display

            //    Clearfrm();
            //    StatusWaitCallQueue();
            //    tabControl1.SelectedTab = tabPage1;
            //    tabControl1.Enabled = false;
            //    lbAlertMsg.Text = string.Format(Program.MsgHold, QueueNo);
            //}
            //else if (result == StatusTransaction.Error)
            //{
            //    btnHold.Enabled = true;
            //    lbAlertMsg.Text = "กรุณากด Hold Queue อีกครั้ง";
            //}
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            try
            {
                int? tpr_id = Program.CurrentRegis != null ? (int?)Program.CurrentRegis.tpr_id : null;
                int? mvt_id = Program.CurrentPatient_queue != null ? (int?)Program.CurrentPatient_queue.mvt_id : null;
                frmCancelQueue frmCancelQueue = new frmCancelQueue(Program.CurrentRegis.tpr_id, mvt_id);
                if (frmCancelQueue.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    new ClsTCPClient().sendClearUnitDisplay();
                    Clearfrm();
                    StatusWaitCallQueue();
                    Class.FunctionDataCls func = new Class.FunctionDataCls();
                    lbAlertMsg.Text = func.getStringGotoNextRoom((int)tpr_id);
                    new Class.ReserveSkipCls().SendAndReserve(tpr_id);
                    return;
                }
            }
            catch
            {
                lbAlertMsg.Text = "กรุณากด Cancel อีกครั้ง";
            }
            btnCancel.Enabled = true;
        }

        private void StatusWaitCallQueue()//first oepn if no patian
        {
            btnReady.Enabled = false;
            btnCallQueue.Enabled = true;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;

            tabControl1.Enabled = false;

            btnSendManual.Enabled = false;
            btnSaveDraft.Enabled = false;
            btnPrintPreview.Enabled = false;
            btnPrint.Enabled = false;
            btnSendToDocScan.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendToCheckB.Enabled = false;
            bindingSource1.DataSource = tmpnull;
        }
        public void statusCallQueue()
        {
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            btnCancel.Enabled = true;

            btnSaveDraft.Enabled = true;
            btnSendManual.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendToCheckB.Enabled = true;
            btnPrintPreview.Enabled = true;
            btnPrint.Enabled = true;
            btnSendToDocScan.Enabled = false;

            //string HNno=(from t1 in dbc.trn_patient_regis 
            //             where t1.tpr_id==Program.CurrentRegis.tpr_id 
            //             select t1.trn_patient.tpt_hn_no).FirstOrDefault();

            //var objHistory = (from t1 in dbc.trn_obstetric_chiefs
            //                  where t1.trn_patient_regi.trn_patient.tpt_hn_no == HNno
            //                  select new dataHistory {
            //                      HN=t1.trn_patient_regi.trn_patient.tpt_hn_no,
            //                      CreateDate=t1.toc_create_date,
            //                      CreateBy = t1.toc_create_by,
            //                      UpdateBy = t1.toc_update_by,
            //                      UpdateDate = t1.toc_update_date,
            //                      LinkDocview="Link",
            //                      Site=t1.trn_patient_regi.mst_hpc_site.mhs_ename,
            //                      tpr_id=t1.tpr_id
            //                  }).ToList();

            //GridHistoryOBG.DataSource = objHistory;
            //Coltpr_id.Visible = false;
        }
        public void statusSaveandSaveAsDraft()//Save and SaveAsDraft 
        {
            btnSaveDraft.Enabled = false;
            btnSendManual.Enabled = true;
            btnPrintPreview.Enabled = true;
            btnPrint.Enabled = true;
            btnSendToDocScan.Enabled = true;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;
        }
        trn_obstetric_chief tmpnull = new trn_obstetric_chief();
        private void LoadUI()
        {
            uiAllLeft2.LoadDataAll();
            //uiFooter2.LoadData();
            uiMenuBar1.LoadEnableQuestionare();
            uiMenuBar1.LoadData();
        }
        private void FunctionVisibleControl(CheckBox objcheck, TextBox objtxt)
        {
            if (objcheck.Checked)
            {
                objtxt.Enabled = true;
            }
            else
            {
                objtxt.Text = "";
                objtxt.Enabled = false;
            }
        }
        private void FunctionVisibleControl(RadioButton objcheck, TextBox objtxt)
        {
            if (objcheck.Checked)
            {
                objtxt.Enabled = true;
            }
            else
            {
                objtxt.Text = "";
                objtxt.Enabled = false;
            }
        }

        private void chkNormal_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkNormal, txtNormal);
        }
        private void chkVacuum_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkVacuum, txtVacEx);
        }
        private void chkFE_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkFE, txtFE);
        }
        private void chkCesa_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkCesa, txtCesaSec);
        }
        private void chkCSH_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkCSH, txtCSH);
        }
        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkOther, txtOther);
        }

        private void chkOther_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkOther_Yolva, txtOther_Yolva);
            if (chkOther_Yolva.Checked)
            {
                txtOther_Yolva.Visible = true;
            }
            else
            {
                txtOther_Yolva.Text = "";
                txtOther_Yolva.Visible = false;
            }
        }
        private void chkOther_Urethar_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(chkOther_Urethar, txtOther_Urethar);
            if (chkOther_Urethar.Checked)
            {
                txtOther_Urethar.Visible = true;
            }
            else
            {
                txtOther_Urethar.Text = "";
                txtOther_Urethar.Visible = false;
            }
        }
        private void chkPolyp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPolyp.Checked)
            {
                txtPoly.Visible = true;
            }
            else
            {
                txtPoly.Text = "";
                txtPoly.Visible = false;
            }
        }
        
        private void chkCyst_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            if(chkCyst_Yolva.Checked)
            {
                txtCyst_Yolva.Visible=true;
                lblYCyst.Visible=true;
            }
            else
            {
                txtCyst_Yolva.Text = "";
                txtCyst_Yolva.Visible = false;
                lblYCyst.Visible = false;
            }
        }
        private void chkMass_Yolva_CheckedChanged(object sender, EventArgs e)
        {
            if(chkMass_Yolva.Checked)
            {
                txtMass_Yolva.Visible = true;
                lblYMass.Visible = true;
            }
            else
            {
                txtMass_Yolva.Text = "";
                txtMass_Yolva.Visible = false;
                lblYMass.Visible = false;
            }
        }
        private void chkCyst_Cervix_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCyst_Cervix.Checked)
            {
                txtCyst_Cervix.Visible = true;
                lblCyst_Cervix.Visible = true;
            }
            else
            {
                txtCyst_Cervix.Text = "";
                txtCyst_Cervix.Visible = false;
                lblCyst_Cervix.Visible = false;
            }
        }
        private void chkBartholn_cyst_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBartholn_cyst.Checked)
            {
                txtBartholin.Visible = true;
                lblBartholin.Visible = true;
            }
            else
            {
                txtBartholin.Text = "";
                txtBartholin.Visible = false;
                lblBartholin.Visible = false;
            }
        }
        private void rdbOralCon_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbOralCon.Checked)
            {
                txtOralCon.Enabled = true;
                txtOral_D_Mos.Enabled = true;
                txtOral_D_Yrs.Enabled = true;
            }
            else
            {
                txtOralCon.Enabled = false;
                txtOral_D_Mos.Enabled = false;
                txtOral_D_Yrs.Enabled = false;
                txtOral_D_Mos.Text = "";
                txtOral_D_Yrs.Text = "";
            }
        }
        private void rdbIUD_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbIUD.Checked)
            {
                txtIUD.Enabled = true;

                txtIUD_D_Mos.Enabled = true;
                txtIUD_D_Yrs.Enabled = true;
            }
            else
            {
                txtIUD.Enabled = false;
                txtIUD_D_Mos.Enabled = false;
                txtIUD_D_Yrs.Enabled = false;
                txtIUD_D_Mos.Text = "";
                txtIUD_D_Yrs.Text = "";
            }
        }
        private void rdbInjec_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbInjec.Checked)
            {
                txtInjec.Enabled = true;
                txtInjec_D_Mos.Enabled = true;
                txtInjec_D_Yrs.Enabled = true;
            }
            else
            {
                txtInjec.Enabled = false;
                txtInjec_D_Mos.Enabled = false;
                txtInjec_D_Yrs.Enabled = false;
                txtInjec_D_Mos.Text = "";
                txtInjec_D_Yrs.Text = "";
            }
        }
        private void rdbCon_Im_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCon_Im.Checked)
            {
                txtCon_Im.Enabled = true;
                txtCon_Im_D_Mos.Enabled = true;
                txtCon_Im_D_Yrs.Enabled = true;
            }
            else
            {
                txtCon_Im.Enabled = false;
                txtCon_Im_D_Mos.Enabled = false;
                txtCon_Im_D_Yrs.Enabled = false;
                txtCon_Im_D_Mos.Text = "";
                txtCon_Im_D_Yrs.Text = "";
            }
        }
        private void rdbSter_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSter.Checked)
            {
                txtSter.Enabled = true;
                txtSter_D_Mos.Enabled = true;
                txtSter_D_Yrs.Enabled = true;
            }
            else
            {
                txtSter.Enabled = false;
                txtSter_D_Mos.Enabled = false;
                txtSter_D_Yrs.Enabled = false;
                txtSter_D_Mos.Text = "";
                txtSter_D_Yrs.Text = "";
            }
        }

        private void rdbMarried_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(rdbMarried, txtMarried);
        }
        private void rdbSize_Mass_Left_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(rdbSize_Mass_Left_Adexa, txtSize_Mass_Left_Adexa);
        }
        private void rdbSize_Mass_Right_Adexa_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(rdbSize_Mass_Right_Adexa, txtSize_Mass_Right_Adexa);
        }

        private void btntest_Click(object sender, EventArgs e)
        {
            Clearfrm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clearfrm();
        }

        private void txtSince_Age_TextChanged(object sender, EventArgs e)
        {
            //txtMeno_Total.Text = txtSince_Age.Text;
        }

        private void txtSince_Years_TextChanged(object sender, EventArgs e)
        {
            //txtMeno_Total.Text = txtSince_Years.Text;
        }

        private void rdbOther_Cul_de_sac_CheckedChanged(object sender, EventArgs e)
        {
            FunctionVisibleControl(rdbOther_Cul_de_sac, txtOther_Cul_de_sac);
            if (rdbOther_Cul_de_sac.Checked)
            {
                txtOther_Cul_de_sac.Visible = true;
            }
            else
            {
                txtOther_Cul_de_sac.Text = "";
                txtOther_Cul_de_sac.Visible = false;
            }
        }

        private void pnlMarStatus_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

    

        private void grbUlterus_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private string getSite(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                string site = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.mst_hpc_site.mhs_code).FirstOrDefault();
                return site;
            }
        }

        private const string rptCodePepENG = "PT102";
        private const string rptCodePepJMS = "PT103";
        private const string rptCodePepAMS = "PT104";

        private string getRptCodeBySite(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                string site = getSite(tpr_id);
                if (site == "01JMSCK")
                {
                    return rptCodePepJMS;
                }
                else if (site == "01AMS")
                {
                    return rptCodePepAMS;
                }
                else
                {
                    return rptCodePepENG;
                }
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string> { "PT101" };
            int tprID = 0;
            if (SetTprID != 0)
            {
                tprID = SetTprID;
            }
            else
            {
                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
            }
            Report.ClsReportDocument cls = new Report.ClsReportDocument();
            string otherRptCode = cls.getRptCodeBySite(tprID);
            if (!string.IsNullOrEmpty(otherRptCode)) rptCode.Add(cls.getRptCodeBySite(tprID));
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.previewReport();
            //ClsReport.previewRpt(new List<string> { "PT101" });
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string> { "PT101" };
            int tprID = 0;
            if (SetTprID != 0)
            {
                tprID = SetTprID;
            }
            else
            {
                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
            }
            Report.ClsReportDocument cls = new Report.ClsReportDocument();
            string otherRptCode = cls.getRptCodeBySite(tprID);
            if (!string.IsNullOrEmpty(otherRptCode)) rptCode.Add(cls.getRptCodeBySite(tprID));
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.printReport();
            //ClsReport.printRpt(new List<string> { "PT101" });
        }

        private void dateLast_mens_period_ValueChanged(object sender, EventArgs e)
        {

            //trn_obstetric_chief temp = (from t1 in dbc.trn_obstetric_chiefs
            //                            where t1.tpr_id == Program.CurrentRegis.tpr_id
            //                            select t1).FirstOrDefault();
            if (((DateTimePicker)sender).Value != null)
            {
                ((DateTimePicker)sender).CustomFormat = "dd/MM/yyyy";
            }
        }

        private void btnSendToDocScan_Click(object sender, EventArgs e)
        {
            try
            {
                string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "PT101", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                lbAlertMsg.Text = result;

                //if (docscan.SendtoDocscan("PT101", Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                //{
                //    lbAlertMsg.Text = HistoryData.savestatus;
                //    LoadObstetricHistoryData(Program.CurrentRegis.tpr_id);
                //}
                //else
                //{
                //    lbAlertMsg.Text = "Failed!Cannot send to docscan";
                //}
            }
            catch 
            {
                return;
            }
        }

        DocScan docscan = new DocScan();
        private void gvRecHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvRecHistory.Rows.Count > 0 && gvRecHistory.Columns[e.ColumnIndex].Name == "Column13")
                {
                    string Envalue;
                    Envalue = gvRecHistory.Rows[e.RowIndex].Cells["colen"].Value.ToString();
                    int tprid = (from t in dbc.trn_patient_regis where t.tpr_en_no == Envalue select t.tpr_id).FirstOrDefault();////Use for Update docscan
                    var coutItem = dbc.pw_Get_DocscanStatus(tprid, "PT101", Envalue).FirstOrDefault();
                    if (coutItem.Column1 == 0)////when docscan status = 0 and it not send to docscan
                    {
                        string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "PT101", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                        lbAlertMsg.Text = result;

                        //if (docscan.SendtoDocscan("PT101", tprid, Envalue, Program.getCurrentCareProvider))
                        //{
                        //    lbAlertMsg.Text = HistoryData.savestatus;
                        //    HistoryData.showform = 'Y';
                        //    docscan.GetHistory("PT101", Envalue, tprid);
                        //    LoadObstetricHistoryData(Program.CurrentRegis.tpr_id);
                        //}
                        //else
                        //{
                        //    lbAlertMsg.Text = "Failed!";
                        //}
                    }
                    else////when it send to docscan completed
                    {
                        docscan.GetHistory("PT101", Envalue, tprid);

                        #region Unofficial
                        ////Other docscan (Unofficial)
                        //string site = getSite(tprid);
                        //string rptCode = string.Empty;
                        //switch (site)
                        //{
                        //    case "JMS":
                        //        rptCode = "PT104";
                        //        break;
                        //    case "AMS":
                        //        rptCode = "PT103";
                        //        break;
                        //    default:
                        //        rptCode = "PT102";
                        //        break;
                        //}
                        //var coutItemDocscan = dbc.pw_Get_DocscanStatus(tprid, rptCode, Envalue).FirstOrDefault();
                        //if (coutItemDocscan.Column1 != 0)
                        //    docscan.GetHistory(rptCode, Envalue, tprid);
                       
                        #endregion
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void btnSendToCheckB_Click(object sender, EventArgs e)
        {
            disableBtnWhenSave();
            lbAlertMsg.Text = "";

            this.AutoScrollPosition = new Point(0, 0);

            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            if (SaveData('N'))
            {
                int tpr_id = Program.CurrentRegis.tpr_id;
                int tps_id = Program.CurrentPatient_queue.tps_id;
                int mrm_id = Program.CurrentRoom.mrm_id;
                string messege = "";
                StatusTransaction sendToB = new Class.SendQueue().SendToCheckB(tpr_id, mrm_id, ref messege);
                if (sendToB == StatusTransaction.True)
                {
                    new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendCheckB,
                                                tpr_id,
                                                tps_id,
                                                Program.CurrentSite.mhs_id,
                                                Program.CurrentRoom.mrd_ename,
                                                Program.CurrentUser.mut_username);

                    new ClsTCPClient().sendClearUnitDisplay();
                    Clearfrm();
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = messege;
                }
                else if (sendToB == StatusTransaction.Error)
                {
                    enableBtnWhenSave();
                    lbAlertMsg.Text = "ระบบเกิดความผิดพลาดไม่สามารถส่งไปยัง Checkpoint B ได้ กรุณา กดปุ่ม go to Checkpoint B อีกครั้ง";
                }
            }
            else
            {
                enableBtnWhenSave();
            }

            frmbg.Close();
        }
      
        private void gvRecHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i <= gvRecHistory.Rows.Count - 1; i++)
            {
                gvRecHistory.Rows[i].Cells["Column14"].Value = indexrow;
                indexrow = indexrow + 1;
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

            int tpr_id = Program.CurrentRegis.tpr_id;
            int tps_id = Program.CurrentPatient_queue.tps_id;

            StatusTransaction ready = CallQueue.P_CallQueueReady();
            if (ready == StatusTransaction.True)
            {
                StatusTransaction showUnit = new ClsTCPClient().sendCallUnitDisplay();
                if (showUnit == StatusTransaction.Error)
                {
                    //lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถแสดงผลบน unit display ได้";
                }
                StatusCallQueueReady();
                Class.FunctionDataCls func = new Class.FunctionDataCls();
                resultUltrasound = func.popupUltrasoundLower();
                if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.BeforeStation)
                {
                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    List<int> mvt = mst.GetMstRoomEventByMrm(Program.CurrentRoom.mrm_id).Select(x => x.mvt_id).ToList();

                    Class.FunctionDataCls.sendQueueStatus result = func.sendQueueUltrasoundLower(resultUltrasound, mvt);
                    if (result == Class.FunctionDataCls.sendQueueStatus.error)
                    {
                        lbAlertMsg.Text = "เกิดความผิดพลาดทางเทคนิค ไม่สามารถส่งไป ultrasound ได้ กรุณาติดต่อผู้ดูแลระบบ";
                        AlertOutDepartment.LoadTime();
                    }
                    else if (result == Class.FunctionDataCls.sendQueueStatus.sendSuccess)
                    {
                        new Class.logPatientFlowCls(Class.logPatientFlowCls.sendType.SendUltraSoundBefore,
                                                    tpr_id,
                                                    tps_id,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);

                        new ClsTCPClient().sendClearUnitDisplay();
                        Clearfrm();
                        StatusWaitCallQueue();
                        lbAlertMsg.Text = func.GetStrSaveAndSend(tpr_id, "US", "UL");
                    }
                }
                else if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AfterStation)
                {
                    btnSendManual.Enabled = false;
                    btnSendToCheckB.Enabled = false;
                    AlertOutDepartment.LoadTime();

                    ReserveSkipCls reserveSkip = new ReserveSkipCls();
                    int? skipRoom = reserveSkip.CheckRoomSkip(tpr_id);
                    string alert = reserveSkip.MessegeAlertSkip(skipRoom);
                    lbAlertMsg.Text = alert;
                }
                else if (resultUltrasound == PopupUltrasoundLower.ResultPopupUltrasoundLower.AskMeLater)
                {
                    AlertOutDepartment.LoadTime();

                    ReserveSkipCls reserveSkip = new ReserveSkipCls();
                    int? skipRoom = reserveSkip.CheckRoomSkip(tpr_id);
                    string alert = reserveSkip.MessegeAlertSkip(skipRoom);
                    lbAlertMsg.Text = alert;
                }
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
            clsCountDown.finishCountDown();
        }

        private void StatusCallQueueReady()
        {
            groupQueue.Text = "Queue";

            btnReady.Enabled = false;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = true;
            btnCancel.Enabled = true;

            btnSaveDraft.Enabled = true;
            btnSendManual.Enabled = true;
            btnSendAuto.Enabled = true;
            btnSendToCheckB.Enabled = true;
            btnPrintPreview.Enabled = true;
            btnPrint.Enabled = true;
            btnSendToDocScan.Enabled = false;
        }

        private void StatusCallQueueWaitingReady()
        {
            btnReady.Enabled = true;
            btnCallQueue.Enabled = false;
            btnHold.Enabled = false;
            btnCancel.Enabled = false;

            btnSaveDraft.Enabled = false;
            btnSendManual.Enabled = false;
            btnSendAuto.Enabled = false;
            btnSendToCheckB.Enabled = false;
            btnPrintPreview.Enabled = false;
            btnPrint.Enabled = false;
            btnSendToDocScan.Enabled = false;
        }

        #endregion

        private void frmObstetrics_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsCountDown.cancelCountDown();
        }

        private void uiAllLeft2_OnWaitingSuccessProcess(object sender, StatusTransaction isCallQueue, string e)
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

                if (Program.CurrentRegis != null)
                {
                    LoadUI();
                    clsCountDown.startCountDown(0, clsCountDown.GetTimeCountDown());
                    try
                    {
                        trn_obstetric_chief objoc = dbc.trn_obstetric_chiefs.Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                        if (objoc != null)
                        {
                            bindingSource1.DataSource = objoc;
                            LoadInvestigation(Program.CurrentRegis.tpr_id);
                            tabControl1.Enabled = true;
                        }
                        else
                        {
                            var t11 = (from t1 in dbc.trn_obstetric_chiefs select t1);
                            bindingSource1.DataSource = t11;
                            bindingSource1.AddNew();
                            this.NewInvestigation(Program.CurrentRegis.tpr_id);
                            tabControl1.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "uiAllLeft1_OnWaitingSuccessProcess", ex, false);
                    }
                    StatusCallQueueWaitingReady();
                }
                else
                {
                    Clearfrm();
                    StatusWaitCallQueue();
                    lbAlertMsg.Text = "No patient on queue!";
                    btnCallQueue.Enabled = true;
                }

                frmbg.Close();
            }
        }
    }

    class dataHistory{
        public string EN { get; set; }
        public DateTime? ArriveDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public string LinkDocview{ get; set; }
        public int tpr_id { get; set; }
        public string sts { get; set; }

    }
}
