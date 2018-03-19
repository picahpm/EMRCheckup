using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DBCheckup;


namespace BKvs2010.Usercontrols
{
    public partial class UIFooter : UserControl
    {
        public UIFooter()
        {
            InitializeComponent();
            try
            {
                SetComboSeletedSite();
            }
            catch
            {

            }
        }
        private int? _mhs_id = null;
        public int? mhs_id
        {
            get { return _mhs_id; }
            set
            {
                if (value != _mhs_id)
                {
                    if (value == null)
                    {
                        Clear();
                    }
                    else
                    {
                        SetComboSeletedSite();
                    }
                    _mhs_id = value;
                }
            }
        }
        public void Clear()
        {

        }

        private bool _isEditEMR = false;
        public bool IsEditEMR
        {
            get
            {
                return _isEditEMR;
            }
            set
            {
                _isEditEMR = value;
            }
        }
        public string RoomCode { get; set; }
        public string StrSearch { get; set; }
        public delegate void FooterNameClick(int tprid, int siteid);
        public event FooterNameClick OnFooternameClick;

        string _title = "All Patient HPC Site";
        public string SetTitle
        {
            set
            {
                _title = value;
            }
        }
        private void UIFooter_Load(object sender, EventArgs e)
        {
            for (var i = 7; i < this.dataGridView1.ColumnCount; i++)
            {
                ((DataGridViewImageColumn)this.dataGridView1.Columns[i]).DefaultCellStyle.NullValue = null;
            }
            GC.Collect();
        }
        public override void Refresh()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            showdata(txtSearch.Text.Trim());
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private class SelectedSite
        {
            public string Description { get; set; }
            public int DefaultFor_mhs_id { get; set; }
            public List<int> SiteSelect { get; set; }
        }
        private void SetComboSeletedSite()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var ObjSite = cdc.mst_hpc_sites
                                 .Where(x => x.mhs_status == 'A' &&
                                             x.mhs_type == 'P')
                                 .Select(x => new
                                 {
                                     x.mhs_ename,
                                     x.mhs_id,
                                     x.mhs_code
                                 }).ToList();

                List<string> Site1Default = new List<string>
                {
                    "01CHK",
                    "01AMS",
                    "01JMSCK",
                    "01IMS",
                    "01BLC",
                    "01OTH"
                };

                SelectedSite SelectAll = new SelectedSite { Description = "Select All", SiteSelect = ObjSite.Select(x => x.mhs_id).ToList() };
                SelectedSite SelectSite1AJILong = new SelectedSite
                {
                    Description = string.Join(", ", ObjSite.Where(x => Site1Default.Contains(x.mhs_code))
                                                           .Select(x => x.mhs_ename)),
                    SiteSelect = ObjSite.Where(x => Site1Default.Contains(x.mhs_code))
                                        .Select(x => x.mhs_id).ToList(),
                    DefaultFor_mhs_id = 1
                };
                List<SelectedSite> GetObjSite = new List<SelectedSite>
                                            {
                                                SelectAll,
                                                SelectSite1AJILong
                                            };
                GetObjSite.AddRange(ObjSite.Select(x => new SelectedSite
                {
                    Description = x.mhs_ename,
                    SiteSelect = new List<int> { x.mhs_id },
                    DefaultFor_mhs_id = x.mhs_code == "01CHK" ? 0 : x.mhs_id
                }).ToList());
                DDSite.ValueMember = "SiteSelect";
                DDSite.DisplayMember = "Description";
                DDSite.DataSource = GetObjSite;

                if (Program.CurrentSite != null)
                {
                    var selectValue = GetObjSite.Where(x => x.DefaultFor_mhs_id == Program.CurrentSite.mhs_id).Select(x => x.SiteSelect).FirstOrDefault();
                    DDSite.SelectedValue = selectValue;
                }
            }
        }

        private class footerclass
        {
            public int no { get; set; }
            public int tprID { get; set; }
            public int mhs_id { get; set; }
            public string Dept { get; set; }
            public string queue_no { get; set; }
            public string hn_no { get; set; }
            public string name { get; set; }
            public Image RG { get; set; }
            public Image BM { get; set; }
            public Image SC { get; set; }
            public Image CB { get; set; }
            public Image PE { get; set; }
            public Image CD { get; set; }
            public Image XR { get; set; }
            public Image US1 { get; set; }
            public Image US2 { get; set; }
            public Image US3 { get; set; }
            public Image US4 { get; set; }
            public Image DM { get; set; }
            public Image BD { get; set; }
            public Image EN { get; set; }
            public Image EM { get; set; }
            public Image HS { get; set; }
            public Image EK { get; set; }
            public Image AB { get; set; }
            public Image ES { get; set; }
            public Image PT { get; set; }
            public Image TX { get; set; }
            public Image TE { get; set; }
            public Image UG { get; set; }
            public Image PF { get; set; }
            public Image CC { get; set; }
            public Image DC { get; set; }
            public Image PH { get; set; }
            public Image BK { get; set; }
            public Image FN { get; set; }
        }
        public void LoadData()
        {
            try
            {
                //if (isFirstload)
                //{
                //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                //    {
                //        var objsite = (from t1 in dbc.mst_hpc_sites
                //                       where t1.mhs_status == 'A' && t1.mhs_type == 'P'
                //                       select new DropdownData { Code = t1.mhs_id, Name = t1.mhs_ename }).ToList();
                //        DropdownData newselect = new DropdownData();
                //        newselect.Code = 0;
                //        newselect.Name = "Select All";
                //        objsite.Add(newselect);

                //        DDSite.ValueMember = "Code";
                //        DDSite.DisplayMember = "Name";
                //        DDSite.DataSource = objsite.OrderBy(x => x.Code).ToList();
                //        DDSite.SelectedValue = Program.CurrentSite.mhs_id;
                //        isFirstload = false;
                //    }
                //}
                showdata(txtSearch.Text.Trim());
                GC.Collect();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadData()", ex, false);
            }
        }
        public void threaShow(string searchtext)
        {
            showdata(searchtext.Trim());
            Application.DoEvents();
        }

        private void showdata(string searchdata)
        {
            try
            {
                SelectedSite selectedData = (SelectedSite)DDSite.SelectedItem;
                List<int> selectedSite = selectedData.SiteSelect;
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string txtSearch = searchdata.Trim().ToLower();
                    DateTime dateNow = Program.GetServerDateTime();

                    var datanewx = cdc.vw_pat_status.Where(x => selectedSite.Contains(x.mhs_id) &&
                                                                x.tpr_arrive_date.Value.Date == dateNow.Date &&
                                                                (searchdata.Length == 0 ? true :
                                                                (x.tpr_queue_no.ToLower().Contains(searchdata) ||
                                                                x.name.ToLower().Contains(searchdata) ||
                                                                x.tpt_hn_no.ToLower().Contains(searchdata) ||
                                                                x.mhs_ename.ToLower().Contains(searchdata) ||
                                                                x.tpt_hn_no.ToLower().Replace("-", "").Contains(searchdata))))
                                                    .OrderBy(x => x.tpt_hn_no)
                                                    .ToList()
                                                    .Select((x, inx) => new footerclass
                                                    {
                                                        no = inx + 1,
                                                        tprID = x.tpr_id,
                                                        mhs_id = x.mhs_id,
                                                        Dept = x.mhs_ename,
                                                        queue_no = x.tpr_queue_no,
                                                        hn_no = x.tpt_hn_no,
                                                        name = x.name,
                                                        RG = GetImage(x.RG),
                                                        BM = GetImage(x.BM),
                                                        SC = GetImage(x.SC),
                                                        CB = GetImage(x.CB),
                                                        PE = GetImage(x.PE),
                                                        CD = GetImage(x.CD),
                                                        XR = GetImage(x.XR),
                                                        US1 = GetImage(x.UU),
                                                        US2 = GetImage(x.UL),
                                                        US3 = GetImage(x.UB),
                                                        US4 = GetImage(x.UW),
                                                        DM = GetImage(x.DM),
                                                        BD = GetImage(x.BD),
                                                        EN = GetImage(x.EN),
                                                        EM = GetImage(x.EM),
                                                        HS = GetImage(x.HS),
                                                        EK = GetImage(x.EK),
                                                        AB = GetImage(x.AB),
                                                        ES = GetImage(x.ES),
                                                        PT = GetImage(x.PT),
                                                        TX = GetImage(x.TX),
                                                        TE = GetImage(x.TE),
                                                        UG = GetImage(x.UG),
                                                        PF = GetImage(x.PF),
                                                        CC = GetImage(x.CC),
                                                        DC = GetImage(x.DC),
                                                        PH = GetImage(x.PH),
                                                        BK = GetImage(x.BK),
                                                        FN = GetImage(x.FN)
                                                    })
                                                    .ToList();
                    
                    dataGridView1.DataSource = datanewx;

                    dataGridView1.Columns["ColsiteID"].Visible = false;
                    lbdataPatientHPCSITE.Text = string.Format("{0} (Total {1} คน)", selectedData.Description, datanewx.Count());
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "showdata(string searchdata)", ex, false);
            }

            //DateTime serverdate = Program.GetServerDateTime();
            //DateTime dnow = new DateTime(serverdate.Year, serverdate.Month, serverdate.Day, 0, 0, 0);

            //using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            //{

            //    int? mhs_id = null;
            //    if (DDSite.SelectedValue != null && DDSite.SelectedValue.ToString() != "0")
            //    {
            //        mhs_id = Convert.ToInt32(DDSite.SelectedValue);
            //    }
            //    var datanewx = cdc.vw_pat_status.Where(x => (mhs_id == null ? true : x.mhs_id == mhs_id) &&
            //                                                x.tpr_arrive_date.Value.Date == dnow.Date &&
            //        //x.RG != null &&
            //                                                (searchdata.Length == 0 ? true :
            //                                                (x.tpr_queue_no.ToLower().Contains(searchdata) ||
            //                                                 x.name.ToLower().Contains(searchdata) ||
            //                                                 x.tpt_hn_no.ToLower().Contains(searchdata) ||
            //                                                 x.mhs_ename.ToLower().Contains(searchdata) ||
            //                                                 x.tpt_hn_no.ToLower().Replace("-", "").Contains(searchdata))))
            //                                    .Select(x => new footerclass16
            //                                    {
            //                                        tprID = x.tpr_id,
            //                                        mhs_id = x.mhs_id,
            //                                        Dept = x.mhs_ename,
            //                                        queue_no = x.tpr_queue_no,
            //                                        hn_no = x.tpt_hn_no,
            //                                        name = x.name,
            //                                        RG = GetImage(x.RG),
            //                                        BM = GetImage(x.BM),
            //                                        SC = GetImage(x.SC),
            //                                        CB = GetImage(x.CB),
            //                                        PE = GetImage(x.PE),
            //                                        CD = GetImage(x.CD),
            //                                        XR = GetImage(x.XR),
            //                                        US1 = GetImage(x.UU),
            //                                        US2 = GetImage(x.UL),
            //                                        US3 = GetImage(x.UB),
            //                                        US4 = GetImage(x.UW),
            //                                        DM = GetImage(x.DM),
            //                                        BD = GetImage(x.BD),
            //                                        EN = GetImage(x.EN),
            //                                        EM = GetImage(x.EM),
            //                                        HS = GetImage(x.HS),
            //                                        EK = GetImage(x.EK),
            //                                        AB = GetImage(x.AB),
            //                                        ES = GetImage(x.ES),
            //                                        PT = GetImage(x.PT),
            //                                        TX = GetImage(x.TX),
            //                                        TE = GetImage(x.TE),
            //                                        UG = GetImage(x.UG),
            //                                        PF = GetImage(x.PF),
            //                                        CC = GetImage(x.CC),
            //                                        DC = GetImage(x.DC),
            //                                        PH = GetImage(x.PH),
            //                                        BK = GetImage(x.BK),
            //                                        FN = GetImage(x.FN)
            //                                    }).OrderBy(x => x.hn_no)
            //                                    .ToList();

            //    dataGridView1.DataSource = datanewx;

            //    dataGridView1.Columns["ColsiteID"].Visible = false;
            //    lbdataPatientHPCSITE.Text = string.Format("{0} (Total {1} คน)", _title, datanewx.Count());
            //}
            //InhCheckupDataContext dbc = new InhCheckupDataContext();

            ////var objmrmlist = (from t1 in dbc.vw_pat_status where t1.tpr_arrive_date.Value.Date == serverdate.Date && t1.RG != null select t1);
            //var objmrmlist = (from t1 in dbc.vw_pat_status where t1.tpr_arrive_date.Value.Date == serverdate.Date select t1);
            //if (DDSite.SelectedValue != null && DDSite.SelectedValue.ToString() != "0")
            //{
            //    objmrmlist = objmrmlist.Where(x => x.mhs_id == Convert.ToInt32(DDSite.SelectedValue));
            //}
            //var datanewx = from t1 in objmrmlist
            //               select new footerclass16
            //               {
            //                   tprID = t1.tpr_id,
            //                   mhs_id = t1.mhs_id,
            //                   Dept = t1.mhs_ename,
            //                   queue_no = t1.tpr_queue_no,
            //                   hn_no = t1.tpt_hn_no,
            //                   name = t1.name,
            //                   RG = GetImage(t1.RG),
            //                   BM = GetImage(t1.BM),
            //                   SC = GetImage(t1.SC),
            //                   CB = GetImage(t1.CB),
            //                   PE = GetImage(t1.PE),
            //                   CD = GetImage(t1.CD),
            //                   XR = GetImage(t1.XR),
            //                   US1 = GetImage(t1.UU),
            //                   US2 = GetImage(t1.UL),
            //                   US3 = GetImage(t1.UB),
            //                   US4 = GetImage(t1.UW),
            //                   DM = GetImage(t1.DM),
            //                   BD = GetImage(t1.BD),
            //                   EN = GetImage(t1.EN),
            //                   EM = GetImage(t1.EM),
            //                   HS = GetImage(t1.HS),
            //                   EK = GetImage(t1.EK),
            //                   AB = GetImage(t1.AB),
            //                   ES = GetImage(t1.ES),
            //                   PT = GetImage(t1.PT),
            //                   TX = GetImage(t1.TX),
            //                   TE = GetImage(t1.TE),
            //                   UG = GetImage(t1.UG),
            //                   PF = GetImage(t1.PF),
            //                   CC = GetImage(t1.CC),
            //                   DC = GetImage(t1.DC),
            //                   PH = GetImage(t1.PH),
            //                   BK = GetImage(t1.BK),
            //                   FN = GetImage(t1.FN)
            //               };

            //if (searchdata != "")
            //{//footerclass16
            //    searchdata = searchdata.ToLower();
            //    dataGridView1.DataSource = datanewx.Where(x => x.queue_no.ToLower().Contains(searchdata) ||
            //                                                   x.name.ToLower().Contains(searchdata) ||
            //                                                   x.hn_no.ToLower().Contains(searchdata) ||
            //                                                   x.Dept.ToLower().Contains(searchdata) ||
            //                                                   x.hn_no.ToLower().Replace("-", "").Contains(searchdata)).OrderBy(y => y.hn_no);
            //}
            //else
            //{
            //    //var objdata = new SortableBindingList<footerclass16>(datanewx.OrderBy(y => y.hn_no).ToList());
            //    dataGridView1.DataSource = datanewx.OrderBy(y => y.hn_no);
            //}

            //dataGridView1.Columns["ColsiteID"].Visible = false;
            //lbdataPatientHPCSITE.Text = string.Format("{0} (Total {1} คน)", _title, datanewx.Count());
        }

        private Image GetImage(string strstatus)
        {
            Image imgicon = null;
            switch (strstatus)
            {
                case "NS": imgicon = imageList1.Images[0]; break;
                case "WK": imgicon = imageList1.Images[1]; break;
                case "ED": imgicon = imageList1.Images[2]; break;
                case "LR": imgicon = imageList1.Images[3]; break;
                case "CL": imgicon = imageList1.Images[4]; break;
                case "WT": imgicon = imageList1.Images[6]; break;
            }
            return imgicon;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            showdata(txtSearch.Text.Trim());
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                this.showdata("");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    if (Program.CurrentRoom != null)
                    {
                        mst_room_dtl mrd = cdc.mst_room_dtls.Where(x => x.mrd_id == Program.CurrentRoom.mrd_id).FirstOrDefault();
                        if (mrd.mst_room_hdr.mrm_code == "CC" || mrd.mst_room_hdr.mrm_code == "CB")
                        {
                            return;
                        }
                    }
                }
            }
            catch
            {

            }

            if (e.RowIndex == -1) { return; }
            if (Program.FooterIsclick == false)
            {
                return;
            }
            try
            {
                if (BKvs2010.Program.CurrentRegis != null) return;
                int tprid = Convert.ToInt32(dataGridView1["coltprid", e.RowIndex].Value);
                int mhs_id = Convert1.ToInt32(dataGridView1["ColsiteID", e.RowIndex].Value);
                string colRC = "";
                /* case 3:
                    case 5:
                    case 4: OnFooternameClick(tprid); break;*/
                switch (e.ColumnIndex)
                {
                    case 7: colRC = "RG"; break;
                    case 8: colRC = "BM"; break;
                    case 9: colRC = "SC"; break;
                    //  M Edit  เปิด comment  ไม่สามารถ เปิดจาก Book เขียว ได้ครับ
                    //case 10: colRC = "CB"; break;
                    ////case 11: colRC = "PE"; break; ไม่รันหน้าหมอ เปลี่ยนเป็น DC
                    //case 11: colRC = "DC"; break;
                    case 12: colRC = "CD"; break;

                    //case 13: colRC = "XR"; break;
                    //case 14: colRC = "US"; break;
                    //case 15: colRC = "US"; break;
                    //case 16: colRC = "US"; break;
                    //case 17: colRC = "US"; break;

                    //case 18: colRC = "DM"; break;
                    //case 19: colRC = "BD"; break;
                    case 20: colRC = "EM"; break;
                    case 21: colRC = "EM"; break;
                    case 22: colRC = "HS"; break;
                    //case 23: colRC = "EK"; break;
                    case 24: colRC = "AB"; break;
                    //case 25: colRC = "ES"; break;
                    case 26: colRC = "OB"; break;//paptest 
                    case 27: colRC = "TE"; break;
                    case 28: colRC = "TE"; break;
                    //case 29: colRC = "UG"; break;
                    case 30: colRC = "PF"; break;
                    //case 31: colRC = "CC"; break;
                    //case 32: colRC = "DC"; break;
                    //case 33: colRC = "PH"; break;
                    //case 34: colRC = "BK"; break;
                }
                if (colRC != "")
                {
                    if (!_isEditEMR)
                    {
                        if (CompareImages((Bitmap)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, (Bitmap)imageList1.Images[2])
                            || CompareImages((Bitmap)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, (Bitmap)imageList1.Images[3]))
                        {
                            Program.IsViewHistory = true;
                            //ตรวจสอบว่าอยู่หน้านั้นหรือไม่ ?
                            if (RoomCode != colRC)
                            {
                                showfrm(colRC, tprid, mhs_id);
                            }
                            else
                            {
                                //OnFooternameClick(tprid, mhs_id);
                            }
                        }
                    }
                    else
                    {
                        Program.IsViewHistory = true;
                        showfrm(colRC, tprid, mhs_id);
                    }
                }
                else 
                { //  M  Add  16/10/2015 เพื่อเตือนว่าเปิด book เขียวห้องไหนได้บ้าง
                    if (e.ColumnIndex > 9)
                    {
                        //MessageBox.Show("สามารถใช้ได้งานได้ เฉพาะห้อง Registration, Vital Signs และ Screening ได้เท่านั้นขอบคุณค่ะ");
                    }
                }
            }
            catch (Exception)
            {

            }
            GC.Collect();
        }
        private static bool CompareImages(Bitmap image1, Bitmap image2)
        {
            if (image1.Width == image2.Width && image1.Height == image2.Height)
            {
                for (int i = 0; i < image1.Width; i++)
                {
                    for (int j = 0; j < image1.Height; j++)
                    {
                        if (image1.GetPixel(i, j) != image2.GetPixel(i, j))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private void openForm(string frmName, int tpr_id)
        {
            var type = Type.GetType(frmName);
            var form = Activator.CreateInstance(type) as Forms.CheckupInheriteFrm;
            form.FormStatus = Forms.CheckupInheriteFrm.formStatus.isFooter;
            form.WindowState = FormWindowState.Maximized;
         //   form.user = Program.CurrentUser;
            form.user = Program.CurrentUser;
            form.mrd_id = Program.CurrentRoom.mrd_id;
            form.tpr_id = tpr_id;
            if (Class.ClsManageUserLogin.current_log != null)
            {
                form.lug_id = Class.ClsManageUserLogin.current_log.lug_id;
            }
            form.ShowDialog();
        }

        //private void showfrm(string pagecode, int tprID, int mhs_id)
        //{
        //    Program.FooterIsclick = false;

        //    InhCheckupDataContext dbc = new InhCheckupDataContext();
        //    mst_room_dtl currentmrd = new mst_room_dtl();
        //    var currentRoomdtl = (from t1 in dbc.mst_room_dtls
        //                          where t1.mst_room_hdr.mrm_code == pagecode
        //                          select t1).FirstOrDefault();
        //    if (currentRoomdtl != null)
        //    {
        //        currentmrd = Program.CurrentRoom;
        //        //Program.CurrentRoom = null;
        //        Program.CurrentRoom = currentRoomdtl;
        //    }
        //    switch (pagecode)
        //    {
        //        case "RG": frmScreeningPage frmRG = new frmScreeningPage();
        //            frmRG.SetTprID = tprID;
        //            frmRG.siteitem = mhs_id;
        //            frmRG.WindowState = FormWindowState.Maximized;
        //            frmRG.ShowDialog();
        //            break;
        //        case "BM": frmBasicMeasurement frmBM = new frmBasicMeasurement();
        //            frmBM.SetTprID = tprID;
        //            frmBM.siteitem = mhs_id;
        //            frmBM.ShowDialog();
        //            break;
        //        case "AB":
        //            //frmABI2 frmAB = new frmABI2();
        //            //frmAB.SetTprID = tprID;
        //            //frmAB.siteitem = mhs_id;
        //            //frmAB.ShowDialog();
        //            openForm("BKvs2010.Forms.ABIFrm", tprID);
        //            break;
        //        case "HS":
        //            //frmHearing frmHS = new frmHearing();
        //            //frmHS.SetTprID = tprID;
        //            //frmHS.siteitem = mhs_id;
        //            //frmHS.ShowDialog();
        //            openForm("BKvs2010.Forms.HearingFrm", tprID);
        //            break;
        //        case "EN":
        //        case "EM":
        //            //frmEye1 frmEM = new frmEye1();
        //            //frmEM.SetTprID = tprID;
        //            //frmEM.siteitem = mhs_id;
        //            //frmEM.ShowDialog();
        //            openForm("BKvs2010.Forms.EyesFrm", tprID);
        //            break;
        //        case "SC": frmScreeningPage frmSC = new frmScreeningPage();
        //            frmSC.SetTprID = tprID; frmSC.siteitem = mhs_id;
        //            frmSC.ShowDialog();
        //            break;
        //        case "PT": frmObstetrics frmPT = new frmObstetrics();
        //            frmPT.SetTprID = tprID; frmPT.siteitem = mhs_id;
        //            frmPT.WindowState = FormWindowState.Maximized;
        //            frmPT.ShowDialog();
        //            break;
        //        case "ES": frmEST frmES = new frmEST();
        //            frmES.SetTprID = tprID; frmES.siteitem = mhs_id;
        //            frmES.WindowState = FormWindowState.Maximized;
        //            frmES.ShowDialog();
        //            break;
        //        case "EK": frmEKG frmEK = new frmEKG();
        //            frmEK.SetTprID = tprID; frmEK.siteitem = mhs_id;
        //            frmEK.ShowDialog();
        //            break;
        //        case "CD": frmCarotid_2 frmCD = new frmCarotid_2();
        //            frmCD.SetTprID = tprID;
        //            frmCD.siteitem = mhs_id;
        //            frmCD.ShowDialog(); break;
        //        case "TE": frmTeeth frmTE = new frmTeeth();
        //            //frmTE.SetTprID = tprID; frmTE.siteitem = mhs_id;
        //            //frmTE.ShowDialog();
        //            openForm("BKvs2010.Forms.DentalFrm", tprID);
        //            break;
        //        case "DM": frmMammogramPage frmDM = new frmMammogramPage();
        //            frmDM.SetTprID = tprID; frmDM.siteitem = mhs_id;
        //            frmDM.WindowState = FormWindowState.Maximized;
        //            frmDM.ShowDialog();
        //            break;
        //        case "DC":
        //            //break;
        //            if (!_isEditEMR)
        //            {
        //                //frmPE frmPE = new frmPE();
        //                //frmPE.SetTprID = tprID;frmPE.siteitem = mhs_id;
        //                //frmPE.WindowState = FormWindowState.Maximized;
        //                //frmPE.ShowDialog();
        //                //break;
        //            }
        //            else
        //            {
        //                frmPE_OutQueue frmPEOut = new frmPE_OutQueue();
        //                frmPEOut.SetTprID = tprID;
        //                frmPEOut.WindowState = FormWindowState.Maximized;
        //                frmPEOut.ShowDialog();
        //            }
        //            break;
        //        case "CC": /*frmCheckPointC frmCC = new frmCheckPointC(); frmCC.ShowDialog();*/ break;
        //        case "XR": frmChestXRay frmXR = new frmChestXRay();
        //            frmXR.SetTprID = tprID; frmXR.siteitem = mhs_id;
        //            frmXR.WindowState = FormWindowState.Maximized;
        //            frmXR.ShowDialog();
        //            break;
        //        case "UG": frmUgiXRay frmUGI = new frmUgiXRay();
        //            frmUGI.SetTprID = tprID;
        //            frmUGI.siteitem = mhs_id;
        //            frmUGI.WindowState = FormWindowState.Maximized;
        //            frmUGI.ShowDialog();
        //            break;
        //        case "PF": frmPFT frmPFT = new frmPFT();
        //            frmPFT.SetTprID = tprID;
        //            frmPFT.siteitem = mhs_id;
        //            frmPFT.WindowState = FormWindowState.Maximized;
        //            frmPFT.ShowDialog();
        //            break;
        //        case "OB":
        //            frmObstetrics frmobg = new frmObstetrics();
        //            frmobg.SetTprID = tprID;
        //            frmobg.siteitem = mhs_id;
        //            frmobg.ShowDialog(); break;
        //        case "BD": frmBMD frmbd = new frmBMD();
        //            frmbd.SetTprID = tprID;
        //            frmbd.siteitem = mhs_id;
        //            frmbd.ShowDialog(); break;
        //        case "PH": frmPHM frmPH = new frmPHM();
        //            frmPH.SetTprID = tprID;
        //            frmPH.siteitem = mhs_id;
        //            frmPH.ShowDialog(); break;
        //        case "CB": /*frmCheckpointB2 frmCB = new frmCheckpointB2(); frmCB.ShowDialog();*/ break;
        //        case "US": frmUltrasound2 frmus = new frmUltrasound2();
        //            frmus.SetTprID = tprID;
        //            frmus.siteitem = mhs_id;
        //            frmus.ShowDialog(); break;
        //        case "BK": break;
        //    }

        //    Program.CurrentRoom = currentmrd;
        //    Program.FooterIsclick = true;
        //    GC.Collect();
        //}
        private void showfrm(string pagecode, int tprID, int mhs_id)
        {
            Program.FooterIsclick = false;

            InhCheckupDataContext dbc = new InhCheckupDataContext();
            mst_room_dtl currentmrd = new mst_room_dtl();
            var currentRoomdtl = (from t1 in dbc.mst_room_dtls
                                  where t1.mst_room_hdr.mrm_code == pagecode
                                  select t1).FirstOrDefault();
            var patientDetail = dbc.trn_patient_regis
                                   .Where(x => x.tpr_id == tprID)
                                   .Select(x => new
                                   {
                                       x.trn_patient.tpt_hn_no,
                                       x.trn_patient.tpt_othername
                                   }).FirstOrDefault();

            if (currentRoomdtl != null)
            {
                currentmrd = Program.CurrentRoom;
                //Program.CurrentRoom = null;
                Program.CurrentRoom = currentRoomdtl;
            }
            switch (pagecode)
            {
                case "RG": frmScreeningPage frmRG = new frmScreeningPage();
                    frmRG.SetTprID = tprID;
                    frmRG.siteitem = mhs_id;
                    frmRG.WindowState = FormWindowState.Maximized;
                    frmRG.ShowDialog();
                    break;
                case "BM": frmBasicMeasurement frmBM = new frmBasicMeasurement();
                    frmBM.SetTprID = tprID;
                    frmBM.siteitem = mhs_id;
                    frmBM.ShowDialog();
                    break;
                case "AB":
                    //openForm("BKvs2010.Forms.ABIFrm", tprID);
                    using (DialogABI abi = new DialogABI())
                    {
                        abi.Text = abi.Text + " HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        abi.tpr_id = tprID;
                        abi.ShowDialog();
                    }
                    break;
                case "HS":
                    //openForm("BKvs2010.Forms.HearingFrm", tprID);
                    using (DialogAudio audio = new DialogAudio())
                    {
                        audio.Text = audio.Text + " HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        audio.tpr_id = tprID;
                        audio.ShowDialog();
                    }
                    break;
                case "EN":
                case "EM":
                    //openForm("BKvs2010.Forms.EyesFrm", tprID);
                    using (DialogEyes eyes = new DialogEyes())
                    {
                        eyes.Text = eyes.Text + " HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        eyes.tpr_id = tprID;
                        eyes.ShowDialog();
                    }
                    break;
                case "SC": frmScreeningPage frmSC = new frmScreeningPage();
                    frmSC.SetTprID = tprID; frmSC.siteitem = mhs_id;
                    frmSC.ShowDialog();
                    break;
                case "PT": 
                    //frmObstetrics frmPT = new frmObstetrics();
                    //frmPT.SetTprID = tprID; frmPT.siteitem = mhs_id;
                    //frmPT.WindowState = FormWindowState.Maximized;
                    //frmPT.ShowDialog();
                    //openForm("BKvs2010.Forms.GYNFrm", tprID);
                    using (DialogPAP pap = new DialogPAP())
                    {
                        pap.Text = pap.Text + " HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        pap.tpr_id = tprID;
                        pap.ShowDialog();
                    }
                    break;
                case "ES": 
                    //frmEST frmES = new frmEST();
                    //frmES.SetTprID = tprID; frmES.siteitem = mhs_id;
                    //frmES.WindowState = FormWindowState.Maximized;
                    //frmES.ShowDialog();
                    //openForm("BKvs2010.Forms.ESTFrm", tprID);
                    break;
                case "EK": 
                    //frmEKG frmEK = new frmEKG();
                    //frmEK.SetTprID = tprID; frmEK.siteitem = mhs_id;
                    //frmEK.ShowDialog();'
                    //openForm("BKvs2010.Forms.EKGFrm", tprID);
                    break;
                case "CD": 
                    //frmCarotid_2 frmCD = new frmCarotid_2();
                    //frmCD.SetTprID = tprID;
                    //frmCD.siteitem = mhs_id;
                    //frmCD.ShowDialog();
                    using (DialogCarotid carotid = new DialogCarotid())
                    {
                        carotid.Text = carotid.Text + " HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        carotid.tpr_id = tprID;
                        carotid.ShowDialog();
                    }
                    break;
                case "TE":
                    //openForm("BKvs2010.Forms.DentalFrm", tprID);
                    using (DialogDental dental = new DialogDental())
                    {
                        dental.Text = dental.Text + " HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        dental.tpr_id = tprID;
                        dental.ShowDialog();
                    }
                    break;
                case "DM": 
                    //frmMammogramPage frmDM = new frmMammogramPage();
                    //frmDM.SetTprID = tprID; frmDM.siteitem = mhs_id;
                    //frmDM.WindowState = FormWindowState.Maximized;
                    //frmDM.ShowDialog();
                    //openForm("BKvs2010.Forms.MammogramFrm", tprID);
                    break;
                case "DC":
                    ////break;
                    //if (!_isEditEMR)
                    //{
                    //    //frmPE frmPE = new frmPE();
                    //    //frmPE.SetTprID = tprID;frmPE.siteitem = mhs_id;
                    //    //frmPE.WindowState = FormWindowState.Maximized;
                    //    //frmPE.ShowDialog();
                    //    //break;
                    //}
                    //else
                    //{
                    //    frmPE_OutQueue frmPEOut = new frmPE_OutQueue();
                    //    frmPEOut.SetTprID = tprID;
                    //    frmPEOut.WindowState = FormWindowState.Maximized;
                    //    frmPEOut.ShowDialog();
                    //}
                    break;
                case "XR":
                    //openForm("BKvs2010.Forms.ChestXrayFrm", tprID);
                    //frmChestXRay frmXR = new frmChestXRay();
                    //frmXR.SetTprID = tprID; frmXR.siteitem = mhs_id;
                    //frmXR.WindowState = FormWindowState.Maximized;
                    //frmXR.ShowDialog();
                    break;
                case "UG":
                    //openForm("BKvs2010.Forms.UGIFrm", tprID);
                    //frmUgiXRay frmUGI = new frmUgiXRay();
                    //frmUGI.SetTprID = tprID;
                    //frmUGI.siteitem = mhs_id;
                    //frmUGI.WindowState = FormWindowState.Maximized;
                    //frmUGI.ShowDialog();
                    break;
                case "PF":
                    //openForm("BKvs2010.Forms.PFTFrm", tprID);
                    //frmPFT frmPFT = new frmPFT();
                    //frmPFT.SetTprID = tprID;
                    //frmPFT.siteitem = mhs_id;
                    //frmPFT.WindowState = FormWindowState.Maximized;
                    //frmPFT.ShowDialog();
                    using (DialogPFT pft = new DialogPFT())
                    {
                        pft.Text = pft.Text +" HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        pft.tpr_id = tprID;
                        pft.ShowDialog();
                    }
                    break;
                case "OB":
                    //frmObstetrics frmobg = new frmObstetrics();
                    //frmobg.SetTprID = tprID;
                    //frmobg.siteitem = mhs_id;
                    //frmobg.ShowDialog();
                    using (DialogPAP pap = new DialogPAP())
                    {
                        pap.Text = pap.Text + " HN : " + patientDetail.tpt_hn_no + " Name : " + patientDetail.tpt_othername;
                        pap.tpr_id = tprID;
                        pap.ShowDialog();
                    }
                    break;
                case "BD":
                    //openForm("BKvs2010.Forms.BMDFrm", tprID);
                    //frmBMD frmbd = new frmBMD();
                    //frmbd.SetTprID = tprID;
                    //frmbd.siteitem = mhs_id;
                    //frmbd.ShowDialog(); 
                    break;
                case "PH": 
                    //frmPHM frmPH = new frmPHM();
                    //frmPH.SetTprID = tprID;
                    //frmPH.siteitem = mhs_id;
                    //frmPH.ShowDialog(); 
                    //break;
                case "US":
                    //openForm("BKvs2010.Forms.UltrasoundFrm", tprID);
                    //frmUltrasound2 frmus = new frmUltrasound2();
                    //frmus.SetTprID = tprID;
                    //frmus.siteitem = mhs_id;
                    //frmus.ShowDialog(); 
                    break;
                case "BK":
                case "CB":
                case "CC":
                    break;
            }

            Program.CurrentRoom = currentmrd;
            Program.FooterIsclick = true;
            GC.Collect();
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            string tooltipName = "Name : ";
                            string tooltipDoctor = "Doctor Name : ";
                            string tooltipSite = "Current Site : ";
                            string tooltipRoom = "Current Room : ";
                            int tpr_id = Convert.ToInt32(dataGridView1["coltprid", e.RowIndex].Value);
                            trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                            if (patient_regis != null)
                            {
                                tooltipName += patient_regis.trn_patient.tpt_othername;
                                if (!string.IsNullOrEmpty(patient_regis.tpr_pe_doc_code))
                                {
                                    mst_user_type user = new EmrClass.GetDataMasterCls().GetUser(patient_regis.tpr_pe_doc_code);
                                    if (user != null)
                                    {
                                        tooltipDoctor += user.mut_fullname;
                                    }
                                }
                                trn_patient_queue tps = patient_regis.trn_patient_queues.Where(x => x.tps_status == "NS" || x.tps_status == "WK").FirstOrDefault();
                                if (tps != null)
                                {
                                    mst_room_hdr mrm = new EmrClass.GetDataMasterCls().GetMstRoomHdr((int)tps.mrm_id);
                                    if (mrm != null)
                                    {
                                        mst_hpc_site mhs = new EmrClass.GetDataMasterCls().GetMstHpcSite(mrm.mhs_id);
                                        if (mhs != null)
                                        {
                                            tooltipSite += mhs.mhs_ename;
                                        }
                                        tooltipSite += ", " + mrm.mrm_ename;
                                    }
                                    if (tps.mrd_id != null)
                                    {
                                        mst_room_dtl room_dtl = cdc.mst_room_dtls.Where(x => x.mrd_id == tps.mrd_id).FirstOrDefault();
                                        if (room_dtl != null)
                                        {
                                            tooltipRoom += room_dtl.mrd_ename;
                                        }
                                    }
                                }
                            }
                            dataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = tooltipName + Environment.NewLine +
                                                                                   tooltipDoctor + Environment.NewLine +
                                                                                   tooltipSite + Environment.NewLine +
                                                                                   tooltipRoom;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("UIFooter", "dataGridView1_CellMouseMove", ex.Message, false);
            }
        }
    }

}