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
using System.Diagnostics;
using BKvs2010.EmrClass;
using BKvs2010.Usercontrols;

namespace BKvs2010
{
    public partial class frmReportChestXRay : Form
    {
        public int favariterunid;
        private string gPageTag;
        public string PageTag { get { return gPageTag; } set { gPageTag = value; /*set favoritetextbox.order*/ } }
        InhCheckupDataContext dbc;
        public frmReportChestXRay()
        {
            InitializeComponent();
            dbc = new InhCheckupDataContext();
            favoriteTextBox1.RightClickDropDown += favoriteTextBox1_DeleteFavorite;
        }
        private void favoriteTextBox1_DeleteFavorite(object sender, string e)
        {
            string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new FavoriteCls().removeFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, e, user_name);
                txtBox.ListFabvorite.Remove(e);
            }
        }

        private void frmReportChestXRay_Load(object sender, EventArgs e)
        {
            switch (PageTag)
            {
                case "XR":
                    favoriteTextBox1.FavoriteOrder = "XR";
                    favariterunid = 7;
                    break;

                case "UU":
                    favoriteTextBox1.FavoriteOrder = "UU";
                    favariterunid = 8;
                    break;

                case "UL":
                    favoriteTextBox1.FavoriteOrder = "UL";
                    favariterunid = 9;
                    break;

                case "UB":
                    favoriteTextBox1.FavoriteOrder = "UB";
                    favariterunid = 10;
                    break;

                case "UW":
                    favoriteTextBox1.FavoriteOrder = "UW";
                    favariterunid = 11;
                    break;

                case "DM":
                    favoriteTextBox1.FavoriteOrder = "DM";
                    favariterunid = 12;
                    break;

                case "BD":
                    favoriteTextBox1.FavoriteOrder = "BD";
                    favariterunid = 13;
                    break;
            }

            List<mst_conclusion_favorite_dtl> mst = new FavoriteCls().getFavorite(favoriteTextBox1.FavoriteOrder, favoriteTextBox1.FavoriteType);
            favoriteTextBox1.AutoCompleteListThList = mst.Select(x => x.mcfd_description).ToList();




            timer1.Enabled = false;
            this.LoadLink();
            this.LoadConclusion();
            this.LoadChestXRay();
        }

        private void LoadLink()
        {
            txtPacURL.Text = LoadURL(PageTag);
        }

        private void LoadChestXRay()
        {
            //check hdr
            //var objhdr = (from hdr in dbc.trn_doctor_hdrs where hdr.trh_id == Program.CurrentHDR.trh_id && ((DateTime)hdr.trh_create_date).Date == Program.GetServerDateTime().Date select hdr).FirstOrDefault();
            var objhdr = (from hdr in dbc.trn_doctor_hdrs where hdr.trh_id == Program.CurrentHDR.trh_id select hdr).FirstOrDefault();
            if (objhdr != null)
            {
                var objtdx = (from tdx in dbc.trn_doctor_xrays
                              join hdr in dbc.trn_doctor_hdrs on tdx.trh_id equals hdr.trh_id
                              where hdr.trh_id == objhdr.trh_id && tdx.trxr_type == PageTag
                              orderby tdx.trxr_result_date descending
                              select tdx).Take(1).FirstOrDefault();


                if (objtdx != null)
                {
                    txtConsult.Text = objtdx.trxr_result;
                    Program.SetValueRadioGroup(pnsummary, objtdx.trxr_summary);
                    chknotshowrpt.Checked = (objtdx.trxr_not_show_rpt == 'N' ? false : true);
                    //cboconclusion.SelectedValue = objtdx.mdr_id;
                    //if (objtdx.mdr_id == null)
                    //{
                    //    cboconclusion.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    cboconclusion.SelectedValue = objtdx.mdr_id;
                    //}
                    favoriteTextBox1.Text = objtdx.trxr_doc_result_thai;
                    //txtRemark.Text = objtdx.trxr_doc_result_thai;

                    var objimg = (from img in dbc.trn_doctor_xray_images select img).Where(c => c.trxr_id == objtdx.trxr_id).ToList();
                    for (int i = 0; i < objimg.Count; i++)
                    {
                        System.Data.Linq.Binary binary = objimg[i].trxm_image;
                        byte[] buffer = binary.ToArray();
                        MemoryStream ms = new MemoryStream(buffer);
                        Image _img = Image.FromStream(ms);
                        imageList1.Images.Add(_img);
                    }

                    this.listViewImgChest.View = View.LargeIcon;
                    this.imageList1.ImageSize = new Size(120, 120);
                    this.listViewImgChest.LargeImageList = this.imageList1;

                    for (int j = 0; j < this.imageList1.Images.Count; j++)
                    {
                        ListViewItem item = new ListViewItem();
                        item.ImageIndex = j;
                        item.Text = "Select Image";
                        item.Checked = (objimg[j].trxm_sel_image == 'S' ? true : false);
                        this.listViewImgChest.Items.Add(item);
                    }
                }
                else
                {
                    this.LoadConsult();
                }
            }
            //var objtdx = (from tdx in dbc.trn_doctor_xrays where ((DateTime)tdx.trxr_result_date).Date ==  ((DateTime)Program.CurrentRegis.tpr_arrive_date).Date && tdx.trxr_type == PageTag && Program.CurrentHDR.trh_id == Program.CurrentHDR.trh_id orderby tdx.trxr_result_date descending select tdx).FirstOrDefault();



        }

        private void LoadConclusion()
        {
            string v_type;
            //mst_room_hdr objmrm = dbc.mst_room_hdrs.Where(c => c.mrm_code == "DC" && c.mhs_id == Program.CurrentSite.mhs_id).FirstOrDefault(); //(from mrm in db.mst_room_hdrs where mrm.mrm_code == "EC" && mrm.mhs_id == Program.CurrentSite.mhs_id select new { mrm.mrm_id }).FirstOrDefault();

            //if (objmrm != null)
            {
                if (PageTag == "XR")
                {
                    v_type = "DCCX";
                }
                else if (PageTag == "DM")
                {
                    v_type = "DCDM";
                }
                else if (PageTag == "UW")
                {
                    v_type = "DCUW";
                }
                else if (PageTag == "UU")
                {
                    v_type = "DCUU";
                }
                else if (PageTag == "UL")
                {
                    v_type = "DCUL";
                }
                else if (PageTag == "UB")
                {
                    v_type = "DCUB";
                }
                else if (PageTag == "BD")
                {
                    v_type = "DCBD";
                }
                else if (PageTag == "UG")
                {
                    v_type = "DCUG";
                }
                else
                {
                    v_type = "DCOX";
                }

                var objconclusion = new EmrClass.FunctionDataCls().getDoctorResult(1, "DC", v_type)
                    .Select(x => new structDropdownIDCls { id = x.mdr_id, name = (x.mdr_ename == null) ? x.mdr_tname : x.mdr_ename }).ToList();
                //var objconclusion = (from mrh in dbc.mst_doc_result_hdrs
                //                     join mdr in dbc.mst_doc_results on mrh.mrh_id equals mdr.mrh_id
                //                     //where mrh.mrh_code == "DCUU" && mrh.mrh_status == 'A' && mdr.mdr_status == 'A'
                //                     where mrh.mrh_code == v_type
                //                     && mrh.mrh_status == 'A'
                //                     && mdr.mdr_status == 'A'
                //                    && mrh.mrm_id == objmrm.mrm_id
                //                    && (Program.GetServerDateTime().Date >= mrh.mrh_effective_date)
                //                    && Program.GetServerDateTime().Date <= (mrh.mrh_expire_date == null ? Program.GetServerDateTime().Date : mrh.mrh_expire_date)
                //                     //select new { mdrid = mdr.mdr_id, mdrtname = mdr.mdr_tname }).ToList();
                //                     select new Class.structDropdownIDCls { id = mdr.mdr_id, name = (mdr.mdr_ename == null) ? mdr.mdr_tname : mdr.mdr_ename }).ToList();
                objconclusion.Insert(0, new structDropdownIDCls { id = null, name = "" });
                //cboconclusion.DataSource = objconclusion;
                //cboconclusion.DisplayMember = "name";
                //cboconclusion.ValueMember = "id";
            }
        }

        private void LoadConsult()
        {
            string en = Program.CurrentRegis.tpr_en_no;
            switch (PageTag)
            {
                case "XR":
                    var objtcx = dbc.trn_chest_xrays
                                    .Where(x => x.tcx_en_no == en)
                                    .OrderByDescending(x => x.tcx_result_date)
                                    .FirstOrDefault();
                    //var objtcx = (from tcx in dbc.trn_chest_xrays where tcx.tpr_id == Program.CurrentRegis.tpr_id orderby tcx.tcx_result_date descending select new { tcx.tcx_result, tcx.tcx_text_report_by, tcx.tcx_text_report_date, tcx.tpr_id }).Take(1).FirstOrDefault(); // add report by report date suriya 02/06/2015 
                    if (objtcx != null)
                    {
                        txtConsult.Text = objtcx.tcx_result + Environment.NewLine + objtcx.tcx_text_report_by + Environment.NewLine + objtcx.tcx_text_report_date;
                    }

                    //this.LoadConclusion("DCCX");
                    break;
                case "DM":
                    var objtmg = dbc.trn_mammograms
                                    .Where(x => x.tmg_en_no == en)
                                    .OrderByDescending(x => x.tmg_result_date)
                                    .FirstOrDefault();
                    //var objtmg = (from mam in dbc.trn_mammograms select new { mam.tmg_result, mam.tpr_id, mam.tmg_text_report_by, mam.tmg_text_report_date }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    if (objtmg != null)
                    {
                        txtConsult.Text = objtmg.tmg_result + Environment.NewLine + objtmg.tmg_text_report_by + Environment.NewLine + objtmg.tmg_text_report_date;
                    }
                    //this.LoadConclusion("DCDM");
                    break;
                case "UW":
                    var objuw = dbc.trn_ultrasounds
                                   .Where(x => x.tus_en_no == en &&
                                               x.tus_ultra_type == "UW")
                                   .OrderByDescending(x => x.tus_result_date)
                                   .FirstOrDefault();
                    //var objuw = (from tu in dbc.trn_ultrasounds
                    //             where (tu.tus_ultra_type == "UW")
                    //             select new { tu.tpr_id, tu.tus_result, tu.tus_ultra_type, tu.tus_text_report_by, tu.tus_text_report_date }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    if (objuw != null)
                    {
                        txtConsult.Text = objuw.tus_result + Environment.NewLine + objuw.tus_text_report_by + Environment.NewLine + objuw.tus_text_report_date;
                    }

                    //this.LoadConclusion("DCUW");
                    break;
                case "UU":
                    var objuu = dbc.trn_ultrasounds
                                   .Where(x => x.tus_en_no == en &&
                                               x.tus_ultra_type == "UU")
                                   .OrderByDescending(x => x.tus_result_date)
                                   .FirstOrDefault();
                    //var objuu = (from tu in dbc.trn_ultrasounds
                    //             where (tu.tus_ultra_type == "UU")
                    //             select new { tu.tpr_id, tu.tus_result, tu.tus_ultra_type, tu.tus_text_report_by, tu.tus_text_report_date }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    if (objuu != null)
                    {
                        txtConsult.Text = objuu.tus_result + Environment.NewLine + objuu.tus_text_report_by + Environment.NewLine + objuu.tus_text_report_date;
                    }

                    ///this.LoadConclusion("DCUU");
                    break;
                case "UL":
                    var objul = dbc.trn_ultrasounds
                                   .Where(x => x.tus_en_no == en &&
                                               x.tus_ultra_type == "UL")
                                   .OrderByDescending(x => x.tus_result_date)
                                   .FirstOrDefault();
                    //var objul = (from tu in dbc.trn_ultrasounds
                    //             where (tu.tus_ultra_type == "UL")
                    //             select new { tu.tpr_id, tu.tus_result, tu.tus_ultra_type, tu.tus_text_report_by, tu.tus_text_report_date }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    if (objul != null)
                    {
                        txtConsult.Text = objul.tus_result + Environment.NewLine + objul.tus_text_report_by + Environment.NewLine + objul.tus_text_report_date;
                    }

                    //this.LoadConclusion("DCUL");
                    break;
                case "UB":
                    var objUB = dbc.trn_ultrasounds
                                   .Where(x => x.tus_en_no == en &&
                                               x.tus_ultra_type == "UB")
                                   .OrderByDescending(x => x.tus_result_date)
                                   .FirstOrDefault();
                    //var objUB = dbc.trn_ultrasounds
                    //               .Where(x => x.tpr_id == Program.CurrentRegis.tpr_id && x.tus_ultra_type == "UB")
                    //               .OrderByDescending(x => x.tus_result_date)
                    //               .Select(x => new
                    //               {
                    //                   x.tpr_id,
                    //                   x.tus_result,
                    //                   x.tus_ultra_type,
                    //                   x.tus_text_report_by,
                    //                   x.tus_text_report_date
                    //               }).FirstOrDefault();
                    if (objUB != null)
                    {
                        txtConsult.Text = objUB.tus_result + Environment.NewLine + objUB.tus_text_report_by + Environment.NewLine + objUB.tus_text_report_date;
                    }
                    //var objub = (from tu in dbc.trn_ultrasounds
                    //         where (tu.tus_ultra_type == "UB")
                    //                select new { tu.tpr_id, tu.tus_result, tu.tus_ultra_type }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    //if (objub != null)
                    // {
                    //     txtConsult.Text = objub.tus_result;
                    // }

                    //this.LoadConclusion("DCUB");
                    break;
                case "BD":
                    var objbmd = dbc.trn_bmds
                                    .Where(x => x.bmd_en_no == en)
                                    .OrderByDescending(x => x.bmd_result_date)
                                    .FirstOrDefault();
                    //var objbmd = (from bmd in dbc.trn_bmds select new { bmd.tpr_id, bmd.bmd_result, bmd.bmd_text_report_by, bmd.bmd_text_report_date }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    if (objbmd != null)
                    {
                        txtConsult.Text = objbmd.bmd_result + Environment.NewLine + objbmd.bmd_text_report_by + Environment.NewLine + objbmd.bmd_text_report_date;
                    }

                    //this.LoadConclusion("DCBD");
                    break;
                case "UG":
                    var objug = dbc.trn_ugi_xrays
                                   .Where(x => x.tug_en_no == en)
                                   .OrderByDescending(x => x.tug_result_date)
                                   .FirstOrDefault();
                    //var objug = (from ug in dbc.trn_ugi_xrays select new { ug.tpr_id, ug.tug_result, ug.tug_text_report_by, ug.tug_text_report_date }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    if (objug != null)
                    {
                        txtConsult.Text = objug.tug_result + Environment.NewLine + objug.tug_text_report_by + Environment.NewLine + objug.tug_text_report_date;
                    }

                    //this.LoadConclusion("DCUG");
                    break;
                default: //Other
                    var objtox = dbc.trn_other_xrays
                                    .Where(x => x.tox_en_no == en)
                                    .OrderByDescending(x => x.tox_result_date)
                                    .FirstOrDefault();
                    //var objtox = (from tox in dbc.trn_other_xrays select new { tox.tpr_id, tox.tox_result, tox.tox_text_report_by, tox.tox_text_report_date }).Where(c => c.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                    if (objtox != null)
                    {
                        txtConsult.Text = objtox.tox_result + Environment.NewLine + objtox.tox_text_report_by + Environment.NewLine + objtox.tox_text_report_date;
                    }

                    //this.LoadConclusion("DCOX");

                    break;

            }
        }

        private void pb_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox picbox = (PictureBox)sender;
            picbox.SizeMode = PictureBoxSizeMode.StretchImage;
            Graphics g = picbox.CreateGraphics();
            g.DrawImage((Image)e.Data.GetData(DataFormats.Bitmap), new Point(0, 0));
        }

        private void pb_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        //private void button1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    Button btnPic = (Button)sender;
        //    btnPic.DoDragDrop(btnPic.Image, DragDropEffects.Copy);
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "Save Complete";
            this.Save();
        }
        private bool CheckTransaction()
        {
            //if (cboconclusion.SelectedIndex == -1)
            //{
            //    lblMsg.Visible = true;
            //    lblMsg.Text = "Not conclusion data.";
            //    return false;
            //}
            return true;
        }
        private void Save()
        {
            if (Program.CurrentHDR != null)
            {
                DateTime dateNow = Program.GetServerDateTime();
                trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                        .Where(x => x.tpr_id == Program.CurrentRegis.tpr_id &&
                                                                    x.tpbr_radiology == PageTag)
                                                        .FirstOrDefault();
                if (bookResult == null)
                {
                    bookResult = new trn_patient_book_result()
                    {
                        tpr_id = Program.CurrentRegis.tpr_id,
                        tpbr_radiology = PageTag,
                        tpbr_create_by = Program.CurrentUser.mut_username,
                        tpbr_create_date = dateNow
                    };
                    dbc.trn_patient_book_results.InsertOnSubmit(bookResult);
                }
                bookResult.tpbr_flag_saved = true;
                bookResult.tpbr_show_sections = true;
                bookResult.tpbr_show_summary = true;
                bookResult.tpbr_not_show_report = false;
                bookResult.tpbr_active = true;
                bookResult.tpbr_update_by = Program.CurrentUser.mut_username;
                bookResult.tpbr_update_date = dateNow;


                var objtdx = (from tdx in dbc.trn_doctor_xrays
                              join hdr in dbc.trn_doctor_hdrs on tdx.trh_id equals hdr.trh_id
                              where hdr.trh_id == Program.CurrentHDR.trh_id && hdr.tpr_id == Program.CurrentRegis.tpr_id && tdx.trxr_type == PageTag
                              orderby tdx.trxr_result_date descending
                              select tdx).FirstOrDefault();

                if (objtdx == null)
                {
                    //objtdx.trxr_result = txtConsult.Text;
                    //objtdx.trxr_summary = (chknotshowrpt.Checked == true ? 'N' : 'A');
                    //objtdx.trxr_doc_result_thai = txtRemark.Text;
                    //objtdx.trxr_doc_result_eng = txtRemark.Text;
                    //objtdx.trxr_update_by = Program.CurrentUser.mut_username;
                    //objtdx.trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N');
                    //objtdx.trxr_update_date = Program.GetServerDateTime();


                    switch (PageTag)
                    {
                        case "XR":
                            if (objtdx == null)
                            {
                                var objtcx = (from tcx in dbc.trn_chest_xrays where tcx.tpr_id == Program.CurrentRegis.tpr_id orderby tcx.tcx_result_date descending select new { tcx }).FirstOrDefault();
                                if (objtcx != null)
                                {

                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (Int32)cboconclusion.SelectedValue,
                                        //mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "XR",
                                        trxr_order_code = objtcx.tcx.tcx_order_code,
                                        trxr_order_name = objtcx.tcx.tcx_order_name,
                                        trxr_order_date = objtcx.tcx.tcx_order_date,
                                        trxr_result_date = objtcx.tcx.tcx_result_date,
                                        trxr_overseen_by = objtcx.tcx.tcx_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }
                            break;
                        case "DM":
                            if (objtdx == null)
                            {
                                var objtmg = (from mam in dbc.trn_mammograms where mam.tpr_id == Program.CurrentRegis.tpr_id orderby mam.tmg_result_date descending select new { mam }).Take(1).FirstOrDefault();
                                if (objtmg != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (Int32)cboconclusion.SelectedValue,
                                        //mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "DM",
                                        trxr_order_code = objtmg.mam.tmg_order_code,
                                        trxr_order_name = objtmg.mam.tmg_order_name,
                                        trxr_order_date = objtmg.mam.tmg_order_date,
                                        trxr_result_date = objtmg.mam.tmg_result_date,
                                        trxr_overseen_by = objtmg.mam.tmg_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }

                            break;
                        case "UW":
                            if (objtdx == null)
                            {
                                var objuw = (from tu in dbc.trn_ultrasounds
                                             where (tu.tus_ultra_type == "UW") && tu.tpr_id == Program.CurrentRegis.tpr_id
                                             orderby tu.tus_result_date descending
                                             select new { tu }).Take(1).FirstOrDefault();
                                if (objuw != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (int)cboconclusion.SelectedValue,
                                        // mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "UW",
                                        trxr_order_code = objuw.tu.tus_order_code,
                                        trxr_order_name = objuw.tu.tus_order_name,
                                        trxr_order_date = objuw.tu.tus_order_date,
                                        trxr_result_date = objuw.tu.tus_result_date,
                                        trxr_overseen_by = objuw.tu.tus_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }

                            break;
                        case "UU":
                            if (objtdx == null)
                            {
                                var objuu = (from tu in dbc.trn_ultrasounds
                                             where (tu.tus_ultra_type == "UU") && tu.tpr_id == Program.CurrentRegis.tpr_id
                                             orderby tu.tus_result_date descending
                                             select new { tu }).Take(1).FirstOrDefault();
                                if (objuu != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (int)cboconclusion.SelectedValue,
                                        // mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "UU",
                                        trxr_order_code = objuu.tu.tus_order_code,
                                        trxr_order_name = objuu.tu.tus_order_name,
                                        trxr_order_date = objuu.tu.tus_order_date,
                                        trxr_result_date = objuu.tu.tus_result_date,
                                        trxr_overseen_by = objuu.tu.tus_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }

                            break;
                        case "UL":
                            if (objtdx == null)
                            {
                                var objul = (from tu in dbc.trn_ultrasounds
                                             where (tu.tus_ultra_type == "UL") && tu.tpr_id == Program.CurrentRegis.tpr_id
                                             orderby tu.tus_result_date descending
                                             select new { tu }).Take(1).FirstOrDefault();
                                if (objul != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (int)cboconclusion.SelectedValue,
                                        // mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "UL",
                                        trxr_order_code = objul.tu.tus_order_code,
                                        trxr_order_name = objul.tu.tus_order_name,
                                        trxr_order_date = objul.tu.tus_order_date,
                                        trxr_result_date = objul.tu.tus_result_date,
                                        trxr_overseen_by = objul.tu.tus_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }

                            break;
                        case "UB":
                            if (objtdx == null)
                            {
                                var objub = (from tu in dbc.trn_ultrasounds
                                             where (tu.tus_ultra_type == "UB") && tu.tpr_id == Program.CurrentRegis.tpr_id
                                             orderby tu.tus_result_date descending
                                             select new { tu }).Take(1).FirstOrDefault();
                                if (objub != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (int)cboconclusion.SelectedValue,
                                        // mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "UB",
                                        trxr_order_code = objub.tu.tus_order_code,
                                        trxr_order_name = objub.tu.tus_order_name,
                                        trxr_order_date = objub.tu.tus_order_date,
                                        trxr_result_date = objub.tu.tus_result_date,
                                        trxr_overseen_by = objub.tu.tus_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }

                            break;
                        case "BD":
                            if (objtdx == null)
                            {
                                var objbmd = (from bmd in dbc.trn_bmds where bmd.tpr_id == Program.CurrentRegis.tpr_id orderby bmd.bmd_result_date descending select new { bmd }).Take(1).FirstOrDefault();
                                if (objbmd != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (int)cboconclusion.SelectedValue,
                                        //mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "BD",
                                        trxr_order_code = objbmd.bmd.bmd_order_code,
                                        trxr_order_name = objbmd.bmd.bmd_order_name,
                                        trxr_order_date = objbmd.bmd.bmd_order_date,
                                        trxr_result_date = objbmd.bmd.bmd_result_date,
                                        trxr_overseen_by = objbmd.bmd.bmd_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }

                            break;
                        case "UG":
                            if (objtdx == null)
                            {
                                var objug = (from ug in dbc.trn_ugi_xrays where ug.tpr_id == Program.CurrentRegis.tpr_id orderby ug.tug_result_date descending select new { ug }).Take(1).FirstOrDefault();
                                if (objug != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (int)cboconclusion.SelectedValue,
                                        // mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = "UG",
                                        trxr_order_code = objug.ug.tug_order_code,
                                        trxr_order_name = objug.ug.tug_order_name,
                                        trxr_order_date = objug.ug.tug_order_date == null ? Program.GetServerDateTime() : objug.ug.tug_order_date,
                                        trxr_result_date = objug.ug.tug_result_date == null ? Program.GetServerDateTime() : objug.ug.tug_result_date,
                                        trxr_overseen_by = objug.ug.tug_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }

                            break;
                        default: //Other
                            if (objtdx == null)
                            {
                                var objtox = (from tox in dbc.trn_other_xrays where tox.tpr_id == Program.CurrentRegis.tpr_id orderby tox.tox_result_date descending select new { tox }).Take(1).FirstOrDefault();
                                if (objtox != null)
                                {
                                    trn_doctor_xray objnew = new trn_doctor_xray
                                    {
                                        trh_id = Program.CurrentHDR.trh_id,
                                        //mdr_id = (Int32)cboconclusion.SelectedValue,
                                        // mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue,
                                        trxr_type = PageTag,
                                        trxr_order_code = objtox.tox.tox_order_code,
                                        trxr_order_name = objtox.tox.tox_order_name,
                                        trxr_order_date = objtox.tox.tox_order_date,
                                        trxr_result_date = objtox.tox.tox_result_date,
                                        trxr_overseen_by = objtox.tox.tox_overseen_by,
                                        trxr_result = txtConsult.Text,
                                        trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N'),
                                        trxr_summary = Program.GetValueRadioTochar(pnsummary),
                                        trxr_doc_result_thai = favoriteTextBox1.Text,
                                        trxr_doc_result_eng = favoriteTextBox1.Text,
                                        trxr_update_by = Program.CurrentUser.mut_username,
                                        trxr_update_date = Program.GetServerDateTime()
                                    };

                                    dbc.trn_doctor_xrays.InsertOnSubmit(objnew);
                                }
                            }
                            break;
                    }

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
                    lblMsg.Visible = true;
                    //  timer1.Enabled = true;
                }
                else
                {
                    //objtdx.trh_id = Program.CurrentHDR.trh_id,
                    //objtdx.mdr_id = (Int32)cboconclusion.SelectedValue;
                    // objtdx.mdr_id = cboconclusion.SelectedValue == null ? (int?)null : (int)cboconclusion.SelectedValue;
                    //objtdx.trxr_type = PageTag;
                    //objtdx.trxr_order_code = objtcx.tcx.tcx_order_code;
                    //objtdx.trxr_order_name = objtcx.tcx.tcx_order_name;
                    //objtdx.trxr_order_date = objtcx.tcx.tcx_order_date;
                    //objtdx.trxr_result_date = objtcx.tcx.tcx_result_date;
                    //objtdx.trxr_overseen_by = objtcx.tcx.tcx_overseen_by;
                    objtdx.trxr_result = txtConsult.Text;
                    objtdx.trxr_not_show_rpt = (chknotshowrpt.Checked == true ? 'Y' : 'N');
                    objtdx.trxr_summary = Program.GetValueRadioTochar(pnsummary);
                    objtdx.trxr_doc_result_thai = favoriteTextBox1.Text;
                    objtdx.trxr_doc_result_eng = favoriteTextBox1.Text;
                    objtdx.trxr_update_by = Program.CurrentUser.mut_username;
                    objtdx.trxr_update_date = Program.GetServerDateTime();

                    dbc.SubmitChanges();
                    lblMsg.Visible = true;
                    //  timer1.Enabled = true;
                }

                //Save Image Select
                var objtrx = (from trx in dbc.trn_doctor_xrays where trx.trxr_type == PageTag select trx).Where(c => c.trh_id == Program.CurrentHDR.trh_id).FirstOrDefault();

                if (objtrx != null)
                {
                    var objimg = (from img in dbc.trn_doctor_xray_images select img).Where(c => c.trxr_id == objtrx.trxr_id).ToList();
                    for (int i = 0; i < objimg.Count; i++)
                    {
                        dbc.trn_doctor_xray_images.DeleteOnSubmit(objimg[i]); //ลบก่อน
                    }
                    dbc.SubmitChanges();

                    for (int i = 0; i < imageList1.Images.Count; i++)
                    {
                        Image _Img = imageList1.Images[i];
                        MemoryStream ms = new MemoryStream();
                        _Img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        trn_doctor_xray_image objnew = new trn_doctor_xray_image
                        {
                            trxr_id = objtrx.trxr_id,
                            trxm_image = ms.ToArray(),
                            trxm_sel_image = listViewImgChest.Items[i].Checked == true ? 'S' : 'N',
                            trxm_status = 'A',
                            trxm_update_by = Program.CurrentUser.mut_username,
                            trxm_update_date = Program.GetServerDateTime()
                        };

                        dbc.trn_doctor_xray_images.InsertOnSubmit(objnew);
                    }
                }
                else
                {
                    for (int i = 0; i < imageList1.Images.Count; i++)
                    {
                        Image _Img = imageList1.Images[i];
                        MemoryStream ms = new MemoryStream();
                        _Img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        trn_doctor_xray_image objnew = new trn_doctor_xray_image
                        {
                            trxr_id = objtrx.trxr_id,
                            trxm_image = ms.ToArray(),
                            trxm_sel_image = listViewImgChest.Items[i].Checked == true ? 'S' : 'N',
                            trxm_status = 'A',
                            trxm_update_by = Program.CurrentUser.mut_username,
                            trxm_update_date = Program.GetServerDateTime()
                        };

                        dbc.trn_doctor_xray_images.InsertOnSubmit(objnew);
                    }
                }
                dbc.SubmitChanges();
            }
            else
            {

                lblMsg.Visible = true;
                lblMsg.Text = "Not save";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (pb.Image == null)
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Not Chest Image.";
                return;
            }

            //เช็คว่าไอเทมไหนถูกเลือก
            int index_sel = 0;
            for (int i = 0; i < listViewImgChest.Items.Count; i++)
            {
                if (listViewImgChest.Items[i].Checked == true)
                {
                    index_sel = listViewImgChest.Items[i].Index;
                }
            }

            listViewImgChest.Items.Clear(); //Clear
            imageList1.Images.Add(pb.Image);

            this.listViewImgChest.View = View.LargeIcon;
            this.imageList1.ImageSize = new Size(120, 120);
            this.listViewImgChest.LargeImageList = this.imageList1;



            for (int j = 0; j < this.imageList1.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = j;
                item.Text = "Select Image";
                item.Checked = (index_sel == j ? true : false);
                this.listViewImgChest.Items.Add(item);
            }

            //Clear Pic
            pb.Image = null;

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

        private void listViewImgChest_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;
            listViewImgChest.Items[index].BackColor = Color.Yellow;
            for (int i = 0; i < listViewImgChest.Items.Count; i++)
            {
                if (i != index)
                {
                    listViewImgChest.Items[i].Checked = false;
                    listViewImgChest.Items[index].BackColor = Color.Empty;
                }
            }
        }

        private void btndelimg_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listViewImgChest.Items.Count; i++)
            {
                if (listViewImgChest.Items[i].Checked == true)
                {
                    listViewImgChest.Items.RemoveAt(i);
                    imageList1.Images.RemoveAt(i);
                }
            }

            int index_sel = 0;
            for (int i = 0; i < listViewImgChest.Items.Count; i++)
            {
                if (listViewImgChest.Items[i].Checked == true)
                {
                    index_sel = listViewImgChest.Items[i].Index;
                }
            }

            listViewImgChest.Items.Clear(); //Clear

            this.listViewImgChest.View = View.LargeIcon;
            this.imageList1.ImageSize = new Size(120, 120);
            this.listViewImgChest.LargeImageList = this.imageList1;



            for (int j = 0; j < this.imageList1.Images.Count; j++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = j;
                item.Text = "Select Image";
                item.Checked = false;
                this.listViewImgChest.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pb.Image = null;
        }

        private string LoadURL(string Typefrm)
        {
            //dbc = new CheckupDataContext(Program.Connectionstring);

            //var datenow = Program.GetServerDateTime().Date;
            //var objmvt = (from mvt in dbc.mst_events where mvt.mvt_code == Typefrm select new { mvt.mvt_id }).FirstOrDefault();
            //if (objmvt != null)
            //{
            //    var pacsheetcode = (from ptplan in dbc.trn_patient_plans
            //                        where ptplan.mvt_id == objmvt.mvt_id
            //                        && ptplan.tpr_id == Program.CurrentRegis.tpr_id
            //                        && ptplan.tpl_use_pac == true
            //                        select ptplan).FirstOrDefault();
            //    if (pacsheetcode != null)
            //    {
            //        if (pacsheetcode.tpl_pac_sheet == null) { return String.Empty; }
            //        var hn = (from t1 in dbc.trn_patients where t1.tpt_id == Program.CurrentRegis.tpt_id select t1.tpt_hn_no).FirstOrDefault();
            //        var objpathodata = (from t1 in dbc.mst_config_dtls
            //                            where t1.mst_config_hdr.mhs_id == Program.CurrentSite.mhs_id
            //                            && t1.mst_config_hdr.mfh_code == "PAC"
            //                            && (t1.mst_config_hdr.mfh_status == 'A'
            //                               && (t1.mst_config_hdr.mfh_effective_date == null ||
            //                                       (t1.mst_config_hdr.mfh_effective_date.Value.Date <= datenow
            //                                       && (t1.mst_config_hdr.mfh_expire_date == null ||
            //                                           t1.mst_config_hdr.mfh_expire_date.Value.Date >= datenow))
            //                                   )
            //                                )
            //                            && (t1.mfd_status == 'A'
            //                               && (t1.mfd_effective_date == null ||
            //                                       (t1.mfd_effective_date.Value.Date <= datenow
            //                                       && (t1.mfd_expire_date == null ||
            //                                           t1.mfd_expire_date.Value.Date >= datenow)
            //                                        )
            //                                   )
            //                                   )
            //                            select new
            //                            {
            //                                link = t1.mfd_text.Replace("[PAC]", pacsheetcode.tpl_pac_sheet.Replace("/", "-")).Replace("[HN]",
            //                                hn.Replace("-", String.Empty))
            //                            }).FirstOrDefault();

            //        return objpathodata.link.ToString();
            //    }
            //}

            //return String.Empty;

            string link = "";
            using (InhCheckupDataContext contxt = new InhCheckupDataContext())
            {
                switch (Typefrm)
                {
                    case "UU":
                        link = contxt.trn_patient_history_ultrasounds
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphu_en_no == Program.CurrentRegis.tpr_en_no &&
                                                 x.tphu_type == Typefrm)
                                     .Select(x => x.tphu_link).FirstOrDefault();
                        break;
                    case "UB":
                        link = contxt.trn_patient_history_ultrasounds
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphu_en_no == Program.CurrentRegis.tpr_en_no &&
                                                 x.tphu_type == Typefrm)
                                     .Select(x => x.tphu_link).FirstOrDefault();
                        break;
                    case "UL":
                        link = contxt.trn_patient_history_ultrasounds
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphu_en_no == Program.CurrentRegis.tpr_en_no &&
                                                 x.tphu_type == Typefrm)
                                     .Select(x => x.tphu_link).FirstOrDefault();
                        break;
                    case "UW":
                        link = contxt.trn_patient_history_ultrasounds
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphu_en_no == Program.CurrentRegis.tpr_en_no &&
                                                 x.tphu_type == Typefrm)
                                     .Select(x => x.tphu_link).FirstOrDefault();
                        break;
                    case "BD":
                        link = contxt.trn_patient_history_bmds
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphb_en_no == Program.CurrentRegis.tpr_en_no)
                                     .Select(x => x.tphb_link).FirstOrDefault();
                        break;
                    case "UG":
                        link = contxt.trn_patient_history_ugis
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphu_en_no == Program.CurrentRegis.tpr_en_no)
                                     .Select(x => x.tphu_link).FirstOrDefault();
                        break;
                    case "XR":
                        link = contxt.trn_patient_history_xrays
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphx_en_no == Program.CurrentRegis.tpr_en_no)
                                     .Select(x => x.tphx_link).FirstOrDefault();
                        break;
                    case "DM":
                        link = contxt.trn_patient_history_mammograms
                                     .Where(x => x.tpt_id == Program.CurrentRegis.tpt_id &&
                                                 x.tphm_en_no == Program.CurrentRegis.tpr_en_no)
                                     .Select(x => x.tphm_link).FirstOrDefault();
                        break;

                }
                return link;
            }
        }

        private void linkLabelURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("IExplore.exe", LoadURL(PageTag));
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pb.Image = Clipboard.GetImage();
        }


        private void TextBox_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listViewImgChest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //private void cboconclusion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboconclusion.SelectedIndex != -1 && cboconclusion.SelectedIndex != 0)
        //        txtRemark.Text = ((structDropdownIDCls)cboconclusion.SelectedItem).name;
        //    else
        //        txtRemark.Text = "";
        //}

        private void favoriteTextBox1_btnFavoriteClick(object sender, EventArgs e)
        {
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null)
            {
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                bool savefav = new FavoriteCls().saveFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, txtBox.Text, user_name);
                if (savefav)
                {
                    txtBox.AutoCompleteListThList.Add(txtBox.Text);
                    MessageBox.Show("Add '" + txtBox.Text + "' to favorite Complete.");
                }
            }
        }

    }
}
