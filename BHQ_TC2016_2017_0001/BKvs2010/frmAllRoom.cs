using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010
{
    public partial class frmAllRoom : Form
    {
        public frmAllRoom()
        {
            InitializeComponent();
            btnPB.Image = btnprintBook.Image;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                button1.Visible = true;
                button2.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
            }
        }
        frmViewQueueList childForm_1;
        frmBasicMeasurement frmBM_2 ;
        frmScreeningPage frmsc_3;
        frmPE_OutQueue fPE_OutQ_5;
        frmCarotidReport frmcdReport_7;
        frmCarotid_2 frmcd_8;
        //frmUltrasound2 frmus_10;
        frmObstetrics frmpt_18;
        frmPHM frmph_21;
        frmPFT_Result frmpfR_23;
        frmCheckpointB2 frmcB_25;
        frmCheckPointC frmCC_26;
        frmBookResult frmBR_27;
        frmWaiting frmwtt_28;
        frmAppointment frmAP_29;
        frmNextAnnualCheckup frmNA_30;
        frmPatientStatus frmps_31;
        frmPrintReport frmPR_32;
        frmBookPrint frmbp_33;

        int frmshow = 0;
        private void btn_Click(object sender, EventArgs e)
        {
            Button btnitem = (Button)sender;
            string typeQueue = "";
            if ((btnitem.Tag.ToString() != "CD" ||
                (btnitem.Tag.ToString() == "CD" && (Program.CurrentUser.mut_type != 'D' && Program.CurrentUser.mut_type != 'N'))) &&
                btnitem.Tag.ToString() != "CB" &&
                btnitem.Tag.ToString() != "CC" &&
                (btnitem.Tag.ToString() != "PF" ||
                 (btnitem.Tag.ToString() == "PF" && Program.CurrentUser.mut_type != 'D')) &&
                (btnitem.Tag.ToString() != "DC" ||
                 (btnitem.Tag.ToString() == "DC" && Program.CurrentUser.mut_type != 'N')) &&
                (btnitem.Tag.ToString() != "PT" ||
                 (btnitem.Tag.ToString() == "PT" && Program.CurrentUser.mut_type != 'N')))
            {
                frmLoginRoom frmlr = new frmLoginRoom();
                if (btnitem.Tag.ToString() == "DC")
                {
                    frmlr.SetVisiblePQueue();
                }
                frmlr.GetmrmCode = btnitem.Tag.ToString();
                if (frmlr.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                typeQueue = frmlr.GetTypeQueue;
            }

            Program.FooterIsclick = true;
            GC.Collect();//คำสั่ง Clear Memory ที่เคยเรียก data base มาใช้งาน 
            timer1.Enabled = true;
            try
            {
                switch (btnitem.Tag.ToString())
                {
                    case "RG":
                        frmshow = 1;
                        Program.CurrentFrmShow = 1;
                        childForm_1 = new frmViewQueueList();
                        childForm_1.WindowState = FormWindowState.Maximized;
                        childForm_1.ShowDialog();
                        break;
                    case "BM": frmshow = 2;
                        Program.CurrentFrmShow = 2;
                        frmBM_2 = new frmBasicMeasurement();
                        frmBM_2.WindowState = FormWindowState.Maximized;
                        frmBM_2.ShowDialog();
                        break;
                    case "SC":
                        frmshow = 3;
                        Program.CurrentFrmShow = 3;
                        frmsc_3 = new frmScreeningPage();
                        frmsc_3.WindowState = FormWindowState.Maximized;
                        frmsc_3.ShowDialog();
                        break;
                    case "DC":
                        if (Program.CurrentUser != null && Program.CurrentUser.mut_type == 'D')
                        {//typeQueue Radio On Queue ,Out Queue
                            if (typeQueue == "N")
                            {
                                //frmPE fPE = new frmPE();
                                frmshow = 4;
                                Program.CurrentFrmShow = 4;
                                var type = Type.GetType("BKvs2010.Forms.DoctorFrm");
                                var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                                form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                                form.WindowState = FormWindowState.Maximized;
                                form.user = Program.CurrentUser;
                                form.mrd_id = Program.CurrentRoom.mrd_id;
                                form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                                form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                                form.ShowDialog();
                                //fPEbtn_4 = new frmPE();
                                //fPEbtn_4.WindowState = FormWindowState.Maximized;
                                //fPEbtn_4.ShowDialog();
                            }
                            else if (typeQueue == "O")
                            {
                                //frmPE_OutQueue fPE = new frmPE_OutQueue();
                                frmshow = 5;
                                Program.CurrentFrmShow = 5;
                                fPE_OutQ_5 = new frmPE_OutQueue();
                                fPE_OutQ_5.WindowState = FormWindowState.Maximized;
                                fPE_OutQ_5.ShowDialog();
                            }
                        }
                        //else if (Program.CurrentUser != null && Program.CurrentUser.mut_type == 'N')
                        //{
                        //    //frmPEChgDoc fPE = new frmPEChgDoc();
                        //    frmshow = 6;
                        //    Program.CurrentFrmShow = 6;
                        //    fPECD_6 = new frmPEChgDoc();
                        //    fPECD_6.ShowDialog();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ห้องนี้กำหนดสิทธิ์ เข้าได้เฉพาะหมอเท่านั้น", "Alert.", MessageBoxButtons.OK);
                        //}
                        break;
                    case "CD":
                        if (Program.CurrentUser != null && Program.CurrentUser.mut_type == 'D')
                        {
                            //frmCarotidReport frmcd = new frmCarotidReport();
                            frmshow = 7;
                            Program.CurrentFrmShow = 7;
                            frmcdReport_7 = new frmCarotidReport();
                            frmcdReport_7.WindowState = FormWindowState.Maximized;
                            frmcdReport_7.ShowDialog();
                        }
                        else if (Program.CurrentUser != null && Program.CurrentUser.mut_type == 'T')
                        {
                            //frmCarotid_2 frmcd = new frmCarotid_2();
                            frmshow = 8;
                            Program.CurrentFrmShow = 8; 
                            frmcd_8 = new frmCarotid_2();
                            frmcd_8.WindowState = FormWindowState.Maximized;
                            frmcd_8.ShowDialog();
                        }
                        break;
                    case "XR":
                        //frmChestXRay frmxr = new frmChestXRay();
                        frmshow = 9;
                        Program.CurrentFrmShow = 9;
                        //frmxr_9=new frmChestXRay();
                        //frmxr_9.WindowState = FormWindowState.Maximized;
                        //frmxr_9.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.ChestXrayFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "US":
                        //frmUltrasound2 frmus = new frmUltrasound2();
                        frmshow = 10;
                        Program.CurrentFrmShow = 10;
                        //frmus_10=new frmUltrasound2();
                        //frmus_10.WindowState = FormWindowState.Maximized;
                        //frmus_10.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.UltrasoundFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "DM":
                        //frmMammogramPage frmDM = new frmMammogramPage();
                        frmshow = 11;
                        Program.CurrentFrmShow = 11;
                        //frmDM_11=new frmMammogramPage();
                        ////frmDM_11.WindowState = FormWindowState.Maximized;
                        //frmDM_11.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.MammogramFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "BD":
                        //frmBMD fBD = new frmBMD();
                        frmshow = 12;
                        Program.CurrentFrmShow = 12;
                        //fBD_12=new frmBMD();
                        //fBD_12.WindowState = FormWindowState.Maximized;
                        //fBD_12.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.BMDFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "EM":
                        //frmEye1 feye = new frmEye1();
                        frmshow = 13;
                        Program.CurrentFrmShow = 13;
                        //feye_13=new frmEye1();
                        //feye_13.WindowState = FormWindowState.Maximized;
                        //feye_13.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.EyesFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            if (Program.CurrentRoom.mrd_type == 'D')
                            {
                                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                                {
                                    form.mvt_id = cdc.mst_events.Where(x => x.mvt_code == "EM").Select(x => x.mvt_id).FirstOrDefault();                                    
                                }   
                            }
                            else if (Program.CurrentRoom.mrd_type == 'N')
                            {
                                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                                {
                                    form.mvt_id = cdc.mst_events.Where(x => x.mvt_code == "EN").Select(x => x.mvt_id).FirstOrDefault();
                                }
                            }
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "HS":
                        //frmHearing frmAUdE = new frmHearing();
                        frmshow = 14;
                        Program.CurrentFrmShow = 14;
                        //frmAUdE_14=new frmHearing();
                        //frmAUdE_14.WindowState = FormWindowState.Maximized;
                        //frmAUdE_14.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.HearingFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "EK":
                        frmshow = 15;
                        Program.CurrentFrmShow = 15;
                        //frmekg_15=new frmEKG();
                        //frmekg_15.WindowState = FormWindowState.Maximized;
                        //frmekg_15.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.EKGFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;

                    case "AB":
                        //frmABI2 frmab = new frmABI2();
                        frmshow = 16;
                        Program.CurrentFrmShow = 16;
                        //frmab_16=new frmABI2();
                        //frmab_16.WindowState = FormWindowState.Maximized;
                        //frmab_16.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.ABIFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "ES":
                        frmshow = 17;
                        Program.CurrentFrmShow = 17;
                        //frmest_17=new frmEST();
                        //frmest_17.WindowState = FormWindowState.Maximized;
                        //frmest_17.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.ESTFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "PT":
                        //frmObstetrics frmpt = new frmObstetrics();//สูตินารีเวช
                        frmshow = 18;
                        Program.CurrentFrmShow = 18;
                        //frmpt_18 = new frmObstetrics();
                        //frmpt_18.WindowState = FormWindowState.Maximized;
                        //frmpt_18.ShowDialog();   
                        if (Program.CurrentUser != null && Program.CurrentUser.mut_type == 'D')
                        {
                            var type = Type.GetType("BKvs2010.Forms.GYNFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        //else
                        //{
                        //    frmshow = 34;
                        //    Program.CurrentFrmShow = 34;
                        //    frmpt_34 = new frmObstetricsChgDoc();
                        //    //frmpt_18.WindowState = FormWindowState.Maximized;
                        //    frmpt_34.ShowDialog();
                        //    break;
                        //}
                        break;

                    case "BP":
                        //frmBookResult frmPB = new frmBookResult();
                        frmshow = 27;
                        Program.CurrentFrmShow = 27;
                        frmBR_27 = new frmBookResult();
                        frmBR_27.WindowState = FormWindowState.Maximized;
                        frmBR_27.ShowDialog();
                        break;

                    case "TE":
                        //frmTeeth frmth = new frmTeeth();
                        frmshow = 19;
                        Program.CurrentFrmShow = 19;
                        //frmth_19=new frmTeeth();
                        //frmth_19.WindowState = FormWindowState.Maximized;
                        //frmth_19.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.DentalFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "UG":
                        frmshow = 20;
                        Program.CurrentFrmShow = 20;
                        //frmUG_20=new frmUgiXRay();
                        ////frmUG_20.WindowState = FormWindowState.Maximized;
                        //frmUG_20.ShowDialog();
                        {
                            var type = Type.GetType("BKvs2010.Forms.UGIFrm");
                            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                            form.WindowState = FormWindowState.Maximized;
                            form.user = Program.CurrentUser;
                            form.mrd_id = Program.CurrentRoom.mrd_id;
                            form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                            form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                            form.ShowDialog();
                        }
                        break;
                    case "PH":
                        //frmPHM frmph = new frmPHM();
                        frmshow = 21;
                        Program.CurrentFrmShow = 21;
                        frmph_21=new frmPHM();
                        frmph_21.WindowState = FormWindowState.Maximized;
                        frmph_21.ShowDialog();
                        break;
                    case "BK":
                        //frmshow = 22;
                        //Program.CurrentFrmShow = 22;
                        //frmBK_22=new frmQuestionnaireAviation2();
                        //frmBK_22.WindowState = FormWindowState.Maximized;
                        //frmBK_22.ShowDialog();
                        break;
                    case "PF":
                        if (Program.CurrentUser != null && Program.CurrentUser.mut_type == 'D')
                        {
                            //frmPFT_Result frmpf = new frmPFT_Result();
                            frmshow = 23;
                            Program.CurrentFrmShow = 23; 
                            frmpfR_23 = new frmPFT_Result();
                            frmpfR_23.WindowState = FormWindowState.Maximized;
                            frmpfR_23.ShowDialog();
                        }
                        else
                        {
                            //frmPFT frmpf = new frmPFT();
                            frmshow = 24;
                            Program.CurrentFrmShow = 24; 
                            //frmpft_24 = new frmPFT();
                            //frmpft_24.WindowState = FormWindowState.Maximized;
                            //frmpft_24.ShowDialog();
                            {
                                var type = Type.GetType("BKvs2010.Forms.PFTFrm");
                                var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
                                form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isStation;
                                form.WindowState = FormWindowState.Maximized;
                                form.user = Program.CurrentUser;
                                form.mrd_id = Program.CurrentRoom.mrd_id;
                                form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
                                form.kickedUser += new Forms.CheckupInheriteFrm.KickedUser(form_kickedUser);
                                form.ShowDialog();
                            }
                        }
                        break;

                    case "CC":
                    case "CB":
                        string mrmcode = btnitem.Tag.ToString();
                        string strLogtype = Program.CurrentUser.mut_type.ToString();
                        using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                        {
                            #region SelectRoom New version [Akkaradech on 2014-01-13]
                            //var countRoomLogin = (from t1 in dbc.log_user_logins
                            //                      where t1.mhs_id == Program.CurrentSite.mhs_id
                            //                      && t1.mut_id == Program.CurrentUser.mut_id
                            //                      && t1.lug_end_date == null
                            //                      select t1.mrd_id).ToList();
                            int getmrmid = (from t1 in dbc.mst_room_hdrs 
                                            where t1.mrm_code == mrmcode
                                            && t1.mhs_id == Program.CurrentSite.mhs_id
                                            select t1.mrm_id).FirstOrDefault();

                            int countRoomLogin = (from t1 in dbc.log_user_logins
                                                  join t2 in dbc.mst_room_dtls
                                                  on t1.mrd_id equals t2.mrd_id
                                                  where t1.mhs_id == Program.CurrentSite.mhs_id
                                                  && t2.mrm_id == getmrmid
                                                  && t1.lug_end_date == null
                                                  select t1.mrd_id).Count();

                            var objroom = (from t1 in dbc.mst_room_dtls
                                           where t1.mst_room_hdr.mrm_code == mrmcode
                                           && t1.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id
                                           //&& t1.mrd_id == mrdid
                                           //&& t1.mrd_dummy_room == 'Y'
                                           select t1).FirstOrDefault();
                            Program.IsDummy = true;
                            Program.CurrentRoom = objroom;

                            if (strLogtype == "D")
                            {
                                int countdoctor = (from t in dbc.mst_room_hdrs
                                                   where t.mrm_id == getmrmid
                                                   && t.mhs_id == Program.CurrentSite.mhs_id
                                                   select t.mrm_count_doctor.Value).FirstOrDefault();

                                if (countRoomLogin < countdoctor)
                                {
                                    if (mrmcode == "CB")
                                    {
                                        //if (Program.CurrentSite.mhs_code == "01JMSCK" || Program.CurrentSite.mhs_code == "01JMS" || Program.CurrentSite.mhs_code == "01IMS" || Program.CurrentSite.mhs_code == "01AMS" || Program.CurrentSite.mhs_code == "01BLC")
                                        //{
                                        //    MessageBox.Show("ไม่สามารถเข้าใช้งานได้", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //    return;
                                        //}
                                        //else
                                        //{
                                            frmshow = 25;
                                            Program.CurrentFrmShow = 25;
                                            frmcB_25 = new frmCheckpointB2();
                                            frmcB_25.WindowState = FormWindowState.Maximized;
                                            frmcB_25.ShowDialog();
                                        //}
                                    }
                                    if (mrmcode == "CC")
                                    {
                                        frmshow = 26;
                                        Program.CurrentFrmShow = 26;
                                        frmCC_26 = new frmCheckPointC();
                                        frmCC_26.WindowState = FormWindowState.Maximized;
                                        frmCC_26.ShowDialog();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ไม่สามารถเข้าใช้งานได้", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                int countperson = (from t in dbc.mst_room_hdrs
                                                   where t.mrm_id == getmrmid
                                                   && t.mhs_id == Program.CurrentSite.mhs_id
                                                   select t.mrm_count_person.Value).FirstOrDefault();
                                if (countRoomLogin < countperson)
                                {
                                    if (mrmcode == "CB")
                                    {
                                        frmshow = 25;
                                        Program.CurrentFrmShow = 25; 
                                        frmcB_25 = new frmCheckpointB2();
                                        frmcB_25.WindowState = FormWindowState.Maximized;
                                        frmcB_25.ShowDialog();
                                    }
                                    if (mrmcode == "CC")
                                    {
                                        frmshow = 26;
                                        Program.CurrentFrmShow = 25; 
                                        frmCC_26 = new frmCheckPointC();
                                        //frmCC_26.WindowState = FormWindowState.Maximized;
                                        frmCC_26.ShowDialog();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ไม่สามารถเข้าใช้งานได้", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            #endregion

                            #region CommentedCode Old version [Akkaradech on 2014-01-13]
                            //var objroomlist = (from t1 in dbc.mst_room_dtls
                            //                   where t1.mst_room_hdr.mst_user_rooms.Where(x => x.mut_username == Program.CurrentUser.mut_username
                            //                        && x.mst_room_hdr.mrm_code == mrmcode).Count() > 0
                            //                        && t1.mst_room_hdr.mhs_id == Program.CurrentSite.mhs_id
                            //                        && (t1.mrd_rm_status == 'E' || (t1.mrd_rm_status != 'E' && countRoomLogin.Contains(t1.mrd_id)))
                            //                   orderby t1.mrd_dummy_room, t1.mrd_id
                            //                   select new Roomselect { EName = t1.mrd_ename, ID = t1.mrd_id }).ToList();
                           
                            //var dumyroom = (from t1 in dbc.mst_room_dtls
                            //                where t1.mst_room_hdr.mrm_code == mrmcode
                            //                && t1.mrd_dummy_room == 'Y'
                            //                select t1);
                            //if (dumyroom.Count() > 0 && objroomlist.Where(x => x.ID == dumyroom.FirstOrDefault().mrd_id).Count() == 0)
                            //{
                            //    mst_room_dtl dmroom = dumyroom.FirstOrDefault();
                            //    Roomselect newroom = new Roomselect();
                            //    newroom.EName = dmroom.mrd_ename;
                            //    newroom.ID = dmroom.mrd_id;
                            //    objroomlist.Add(newroom);
                            //}
                            //Program.IsDummy = false;
                            //if (objroomlist.Count() == 0)
                            //{//กรณีไม่ได้กำหนดสิทธิ์เข้าใช้ห้องCheck Point B
                            //    MessageBox.Show("ไม่สามารถเข้าใช้งานได้", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}
                            //int mrdid = objroomlist.FirstOrDefault().ID;
                            //var objroom = (from t1 in dbc.mst_room_dtls
                            //               where t1.mst_room_hdr.mrm_code == mrmcode
                            //               //&& t1.mrd_id == mrdid
                            //               //&& t1.mrd_dummy_room == 'Y'
                            //               select t1).FirstOrDefault();
                            //Program.IsDummy = true;
                            //Program.CurrentRoom = objroom;

                            //if (objroomlist.Count() > 0 && objroom == null)
                            //{
                            //    if (Program.Login(Program.CurrentUser.mut_username, Program.CurrentSite.mhs_id, mrdid))
                            //    {
                            //    }
                            //    else
                            //    {
                            //        return;
                            //    }
                            //}
                            //else if (objroom != null)
                            //{//Dummy Room
                            //    Program.IsDummy = true;
                            //    Program.CurrentRoom = objroom;
                            //}
                           
                            //if (mrmcode == "CB")
                            //{
                            //    //frmCheckpointB2 frmcB = new frmCheckpointB2();
                            //    frmshow = 25; frmcB_25 = new frmCheckpointB2();
                            //    frmcB_25.WindowState = FormWindowState.Maximized;
                            //    frmcB_25.ShowDialog();
                            //}
                            //if (mrmcode == "CC")
                            //{
                            //    //frmCheckPointC frmCC = new frmCheckPointC();
                            //    frmshow = 26; frmCC_26 = new frmCheckPointC();
                            //    frmCC_26.WindowState = FormWindowState.Maximized;
                            //    frmCC_26.ShowDialog();
                            //} 
                            #endregion
                        }
                        break;
                }
                frmshow = 0;
                timer1.Enabled = false;
                Program.ExitRoom();
            }//end try
            catch (Exception ex)
            {
                Program.MessageError(ex.Source, "Page " + btnitem.Tag.ToString(), ex.Message);
            }
            //frmshow = 0;
            //timer1.Enabled = false;
            //Program.ExitRoom();
        }
        private void form_kickedUser(object sender, bool e)
        {
            Program.get_mutLoginStatus = '1';
            this.DialogResult = DialogResult.Yes;
        }
        private void frmAllRoom_Load(object sender, EventArgs e)
        {
            this.Text = Program.GetRoomName("Waiting All Room");
        }

        private void btnwattingAllroom_Click(object sender, EventArgs e)
        {
            Program.CurrentFrmShow = 28;
            frmshow = 28; frmwtt_28 = new frmWaiting();
            frmwtt_28.WindowState = FormWindowState.Maximized;
            frmwtt_28.ShowDialog();
        }

        private void frmBK_Click(object sender, EventArgs e)
        {
        }

        private void frmPB_Click(object sender, EventArgs e)
        {
            Program.CurrentFrmShow = 27;
            frmshow = 27; frmBR_27 = new frmBookResult();
            frmBR_27.WindowState = FormWindowState.Maximized;
            frmBR_27.ShowDialog();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            Program.CurrentFrmShow = 29;
            frmshow = 29; frmAP_29 = new frmAppointment();
            frmAP_29.ShowDialog();
        }

        private void btnNextAnual_Click(object sender, EventArgs e)
        {
            Program.CurrentFrmShow = 30;
            frmshow = 30; frmNA_30 = new frmNextAnnualCheckup();
            frmNA_30.ShowDialog();
        }

        private void btnPatientStatus_Click(object sender, EventArgs e)
        {
            Program.CurrentFrmShow = 31;
            frmshow = 31; frmps_31 = new frmPatientStatus();
            frmps_31.ShowDialog();
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            Program.CurrentFrmShow = 32;
            frmshow = 32; frmPR_32 = new frmPrintReport();
            frmPR_32.ShowDialog();
        }

        private void btnprintBook_Click(object sender, EventArgs e)
        {
            Program.CurrentFrmShow = 33;
            frmshow = 33; frmbp_33 = new frmBookPrint();
            frmbp_33.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                if (Program.FooterIsclick)
                {
                    if (Program.CurrentUser != null)
                    {
                        if (Class.ClsManageUserLogin.checkKickCurrentUser())
                        {
                            CloseSubFrom();
                            //Program.ExitRoom();
                            Program.get_mutLoginStatus = '1';
                            this.DialogResult = DialogResult.Yes;
                            return;
                        }
                    }
                }
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Timmer All Room Fail : " + ex.Message);
            }
        }

        // morn 2014-02-09
        // add function chk form before close
        private void closeForm(Form frm)
        {
            if (frm != null)
            {
                frm.DialogResult = System.Windows.Forms.DialogResult.No;
            }
        }

        private void CloseSubFrom()
        {
            if (frmshow == 0) { return; }
            switch (frmshow)
            {
                case 1: closeForm(childForm_1); break;
                case 2: closeForm(frmBM_2); break;
                case 3: closeForm(frmsc_3); break;
                case 5: closeForm(fPE_OutQ_5); break;
                case 7: closeForm(frmcdReport_7); break;
                case 8: closeForm(frmcd_8); break;
                //case 10: closeForm(frmus_10); break;
                case 18: closeForm(frmpt_18); break;
                case 21: closeForm(frmph_21); break;
                case 23: closeForm(frmpfR_23); break;
                case 25: closeForm(frmcB_25); break;
                case 26: closeForm(frmCC_26); break;
                case 27: closeForm(frmBR_27); break;
                case 28: closeForm(frmwtt_28); break;
                case 29: closeForm(frmAP_29); break;
                case 30: closeForm(frmNA_30); break;
                case 31: closeForm(frmps_31); break;
                case 32: closeForm(frmPR_32); break;
                case 33: closeForm(frmbp_33); break;
            }
        }

        private void frmAllRoom_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
            GC.Collect(); 
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAviationMedCerReport frm = new frmAviationMedCerReport();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmReportVaccination frm = new frmReportVaccination();
            frm.ShowDialog();
        }
    }
}
