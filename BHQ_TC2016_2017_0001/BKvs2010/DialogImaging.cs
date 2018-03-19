using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Runtime.InteropServices;

namespace BKvs2010
{
    public partial class DialogImaging : Form
    {
        public DialogImaging()
        {
            InitializeComponent();
            favoriteTextBox1.BtnFavoriteClick += favoriteTextBox1_BtnFavoriteClick;
            favoriteTextBox1.RightClickDropDown += favoriteTextBox1_DeleteFavorite;
            txtPacURL.GotFocus += txtPacURL_GotFocus;
            pb.MouseUp += pb_MouseUp;
            listViewImgChest.ItemCheck += listViewImgChest_ItemCheck;
            listViewImgChest.MultiSelect = false;

            ListImage = new List<Image>();
            ListImageDisplay = new List<Image>();
            imageListSmall.ImageSize = new Size(100, 100);
            listViewImgChest.View = View.LargeIcon;
            listViewImgChest.LargeImageList = imageListSmall;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (clipBoard != null) clipBoard.Dispose();
        }

        List<Image> ListImage;
        List<Image> ListImageDisplay;
        private void favoriteTextBox1_BtnFavoriteClick(object sender, EventArgs e)
        {
            Usercontrols.FavoriteTextBox txtBox = sender as Usercontrols.FavoriteTextBox;
            if (txtBox != null)
            {
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                bool savefav = new EmrClass.FavoriteCls().saveFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, txtBox.Text, user_name);
                if (savefav)
                {
                    txtBox.AutoCompleteListThList.Add(txtBox.Text);
                    MessageBox.Show("Add '" + txtBox.Text + "' to favorite Complete.");
                }
            }
        }
        private void favoriteTextBox1_DeleteFavorite(object sender, string e)
        {
            string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
            Usercontrols.FavoriteTextBox txtBox = sender as Usercontrols.FavoriteTextBox;
            if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new EmrClass.FavoriteCls().removeFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, e, user_name);
                txtBox.AutoCompleteListThList.Remove(e);
            }
        }
        private void txtPacURL_GotFocus(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.SelectAll();
        }
        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                MenuItem pMenu = new MenuItem();
                pMenu.Text = "Paste";
                pMenu.Click += delegate
                {
                    Menu_Paste();
                };
                MenuItem dMenu = new MenuItem();
                dMenu.Text = "Delete";
                dMenu.Click += delegate
                {
                    Menu_Delete();
                };
                pMenu.Enabled = Clipboard.GetImage() != null;
                dMenu.Enabled = pb.Image != null;
                cm.MenuItems.Add(pMenu);
                cm.MenuItems.Add(dMenu);
                cm.Show(this, PointToClient(Cursor.Position));
            }
        }
        private void Menu_Paste()
        {
            var img = Clipboard.GetImage();
            if (img != null)
            {
                AddNewImage(img);
            }
        }
        private void Menu_Delete()
        {
            DeleteImage();
        }
        private void listViewImgChest_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                listViewImgChest.ItemCheck -= listViewImgChest_ItemCheck;
                for (int i = 0; i < listViewImgChest.Items.Count; i++)
                {
                    if (i != e.Index)
                    {
                        listViewImgChest.Items[i].Checked = false;
                    }
                    else
                    {
                        pb.Image = ListImageDisplay[e.Index];
                    }
                }
                e.NewValue = CheckState.Checked;
                listViewImgChest.ItemCheck += listViewImgChest_ItemCheck;
            }
            else
            {
                e.NewValue = CheckState.Checked;
            }
        }

        private InhCheckupDataContext cdc;
        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                _tpr_id = value;
                LoadData();
            }
        }
        private string _trxr_type;
        public string trxr_type
        {
            get { return _trxr_type; }
            set
            {
                _trxr_type = value;
                LoadData();
            }
        }

        private void LoadData()
        {
            Clear();
            if (_tpr_id != null && _trxr_type != null)
            {
                try
                {
                    favoriteTextBox1.FavoriteOrder = _trxr_type;
                    if (_trxr_type == "UU")
                    {
                        ChkSummaryResult.Text = "Abdomen Result";
                        ChkSummaryResult.Visible = true;
                    }
                    else if (_trxr_type == "DM")
                    {
                        ChkSummaryResult.Text = "Mammogram with Ultrasound Breast Result";
                        ChkSummaryResult.Visible = true;
                    }
                    else
                    {
                        ChkSummaryResult.Text = "";
                        ChkSummaryResult.Visible = false;
                    }
                    List<mst_conclusion_favorite_dtl> mst = new EmrClass.FavoriteCls().getFavorite(favoriteTextBox1.FavoriteOrder, favoriteTextBox1.FavoriteType);
                    favoriteTextBox1.AutoCompleteListThList = mst.Select(x => x.mcfd_description).ToList();

                    cdc = new InhCheckupDataContext();
                    trn_patient_regi reg = cdc.trn_patient_regis.Where(x => x.tpr_id == _tpr_id).FirstOrDefault();
                    if (reg != null)
                    {
                        bool retrieve = true;
                        var ret = reg.trn_patient_retrieves.Where(x => x.tpr_image_type == _trxr_type).FirstOrDefault();
                        if (ret != null)
                        {
                            retrieve = ret.tpr_flag_retrieve == null ? true : (bool)ret.tpr_flag_retrieve;
                        }

                        trn_doctor_hdr hdr = reg.trn_doctor_hdrs.FirstOrDefault();
                        if (hdr == null)
                        {
                            hdr = new trn_doctor_hdr();
                            reg.trn_doctor_hdrs.Add(hdr);
                        }
                        trn_doctor_xray xray = hdr.trn_doctor_xrays.Where(x => x.trxr_type == _trxr_type).FirstOrDefault();
                        if (xray == null)
                        {
                            xray = new trn_doctor_xray();
                            xray.trxr_not_show_rpt = 'N';
                            xray.trxr_type = _trxr_type;
                            hdr.trn_doctor_xrays.Add(xray);
                        }

                        if (retrieve)
                        {
                            var result = LoadResult(reg.tpr_en_no);
                            xray.trxr_order_code = result.trxr_order_code;
                            xray.trxr_order_name = result.trxr_order_name;
                            xray.trxr_order_date = result.trxr_order_date;
                            xray.trxr_result_date = result.trxr_result_date;
                            xray.trxr_overseen_by = result.trxr_overseen_by;
                            xray.trxr_result = result.trxr_result;
                        }

                        var xray_images = xray.trn_doctor_xray_images.ToList();
                        int i = 0;
                        foreach (trn_doctor_xray_image image in xray_images)
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(image.trxm_image.ToArray());
                            Image img = Image.FromStream(ms);
                            AddImage(img, image.trxm_sel_image == 'S' ? true : false);
                        }

                        txtPacURL.Text = LoadURL(reg.tpt_id, reg.tpr_en_no);
                        DoctorXrayBS.DataSource = xray;
                    }
                    panel1.Enabled = true;
                }
                catch (Exception ex)
                {
                    Program.MessageError("DialogImaging", "LoadData()", ex, false);
                    panel1.Enabled = false;
                }
            }
        }
        private string LoadURL(int? tpt_id, string en)
        {
            string link = "";
            try
            {
                using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                {
                    switch (_trxr_type)
                    {
                        case "UU":
                        case "UB":
                        case "UL":
                        case "UW":
                            link = contxt.trn_patient_history_ultrasounds
                                         .Where(x => x.tpt_id == tpt_id &&
                                                     x.tphu_en_no == en &&
                                                     x.tphu_type == _trxr_type)
                                         .Select(x => x.tphu_link).FirstOrDefault();
                            break;
                        case "BD":
                            link = contxt.trn_patient_history_bmds
                                         .Where(x => x.tpt_id == tpt_id &&
                                                     x.tphb_en_no == en)
                                         .Select(x => x.tphb_link).FirstOrDefault();
                            break;
                        case "UG":
                            link = contxt.trn_patient_history_ugis
                                         .Where(x => x.tpt_id == tpt_id &&
                                                     x.tphu_en_no == en)
                                         .Select(x => x.tphu_link).FirstOrDefault();
                            break;
                        case "XR":
                            link = contxt.trn_patient_history_xrays
                                         .Where(x => x.tpt_id == tpt_id &&
                                                     x.tphx_en_no == en)
                                         .Select(x => x.tphx_link).FirstOrDefault();
                            break;
                        case "DM":
                            link = contxt.trn_patient_history_mammograms
                                         .Where(x => x.tpt_id == tpt_id &&
                                                     x.tphm_en_no == en)
                                         .Select(x => x.tphm_link).FirstOrDefault();
                            break;
                    }
                }
            }
            catch
            {

            }
            return link;
        }
        private trn_doctor_xray LoadResult(string en)
        {
            trn_doctor_xray result = new trn_doctor_xray();
            try
            {
                using (InhCheckupDataContext con = new InhCheckupDataContext())
                {
                    switch (_trxr_type)
                    {
                        case "XR":
                            var objtcx = con.trn_chest_xrays
                                            .Where(x => x.tpr_id == _tpr_id &&
                                                        x.tcx_en_no == en)
                                            .OrderByDescending(x => x.tcx_result_date)
                                            .FirstOrDefault();
                            if (objtcx != null)
                            {
                                result.trxr_type = _trxr_type;
                                result.trxr_order_code = objtcx.tcx_order_code;
                                result.trxr_order_name = objtcx.tcx_order_name;
                                result.trxr_result_date = objtcx.tcx_result_date;
                                result.trxr_overseen_by = objtcx.tcx_overseen_by;
                                result.trxr_result = objtcx.tcx_result;
                            }
                            return result;
                        case "DM":
                            var objtmg = con.trn_mammograms
                                            .Where(x => x.tpr_id == _tpr_id &&
                                                        x.tmg_en_no == en)
                                            .OrderByDescending(x => x.tmg_result_date)
                                            .FirstOrDefault();
                            if (objtmg != null)
                            {
                                result.trxr_type = _trxr_type;
                                result.trxr_order_code = objtmg.tmg_order_code;
                                result.trxr_order_name = objtmg.tmg_order_name;
                                result.trxr_order_date = objtmg.tmg_order_date;
                                result.trxr_result_date = objtmg.tmg_result_date;
                                result.trxr_overseen_by = objtmg.tmg_overseen_by;
                                result.trxr_result = objtmg.tmg_result;
                            }
                            return result;
                        case "UW":
                        case "UU":
                        case "UL":
                        case "UB":
                            var objus = con.trn_ultrasounds
                                           .Where(x => x.tpr_id == _tpr_id &&
                                                       x.tus_en_no == en &&
                                                       x.tus_ultra_type == _trxr_type)
                                           .OrderByDescending(x => x.tus_result_date)
                                           .FirstOrDefault();
                            if (objus != null)
                            {
                                result.trxr_type = _trxr_type;
                                result.trxr_order_code = objus.tus_order_code;
                                result.trxr_order_name = objus.tus_order_name;
                                result.trxr_order_date = objus.tus_order_date;
                                result.trxr_result_date = objus.tus_result_date;
                                result.trxr_overseen_by = objus.tus_overseen_by;
                                result.trxr_result = objus.tus_result;
                            }
                            return result;
                        case "BD":
                            var objbmd = con.trn_bmds
                                            .Where(x => x.tpr_id == _tpr_id &&
                                                        x.bmd_en_no == en)
                                            .OrderByDescending(x => x.bmd_result_date)
                                            .FirstOrDefault();
                            if (objbmd != null)
                            {
                                result.trxr_type = _trxr_type;
                                result.trxr_order_code = objbmd.bmd_order_code;
                                result.trxr_order_name = objbmd.bmd_order_name;
                                result.trxr_order_date = objbmd.bmd_order_date;
                                result.trxr_result_date = objbmd.bmd_result_date;
                                result.trxr_overseen_by = objbmd.bmd_overseen_by;
                                result.trxr_result = objbmd.bmd_result;
                            }
                            return result;
                        case "UG":
                            var objug = con.trn_ugi_xrays
                                           .Where(x => x.tpr_id == _tpr_id &&
                                                       x.tug_en_no == en)
                                           .OrderByDescending(x => x.tug_result_date)
                                           .FirstOrDefault();
                            if (objug != null)
                            {
                                result.trxr_type = _trxr_type;
                                result.trxr_order_code = objug.tug_order_code;
                                result.trxr_order_name = objug.tug_order_name;
                                result.trxr_order_date = objug.tug_order_date;
                                result.trxr_result_date = objug.tug_result_date;
                                result.trxr_overseen_by = objug.tug_overseen_by;
                                result.trxr_result = objug.tug_result;
                            }
                            return result;
                    }
                }
            }
            catch
            {

            }
            return result;
        }

        public void Clear()
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewImage(pb.Image);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteImage();
        }

        UserControlLibrary.ClipboardMonitor clipBoard;
        private void linkPac_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPacURL.Text))
            {

            }
            else
            {
                System.Diagnostics.Process.Start("IExplore.exe", txtPacURL.Text);

                if (_trxr_type == "XR")
                {
                    clipBoard = new UserControlLibrary.ClipboardMonitor();
                    clipBoard.ClipboardChanged += clipBoard_ClipboardChanged;
                }
            }
        }
        private void clipBoard_ClipboardChanged(object sender, UserControlLibrary.EventClipBoardArg e)
        {
            if (e.ClipboardOwnerName.ToUpper().StartsWith("WEBVIE"))
            {
                Image img = Clipboard.GetImage();
                if (img != null)
                {
                    clipBoard.Dispose();
                    int inx = ImageIndexOf(img);
                    if (inx < 0)
                    {
                        AddImage(img, true);
                    }
                    else
                    {
                        listViewImgChest.Items[inx].Checked = true;
                    }
                }
            }
        }
        private void btndelimg_Click(object sender, EventArgs e)
        {
            DeleteImage();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";

                DateTime dateNow = Program.GetServerDateTime();
                string user = Program.CurrentUser == null ? "" : Program.CurrentUser.mut_username;

                trn_patient_book_result bookResult = cdc.trn_patient_book_results
                                                        .Where(x => x.tpr_id == (int)_tpr_id &&
                                                                    x.tpbr_radiology == _trxr_type)
                                                        .FirstOrDefault();
                if (bookResult == null)
                {
                    bookResult = new trn_patient_book_result()
                    {
                        tpr_id = (int)_tpr_id,
                        tpbr_radiology = _trxr_type,
                        tpbr_create_by = user,
                        tpbr_create_date = dateNow
                    };
                    cdc.trn_patient_book_results.InsertOnSubmit(bookResult);
                }
                bookResult.tpbr_flag_saved = true;
                bookResult.tpbr_show_sections = true;
                bookResult.tpbr_show_summary = true;
                bookResult.tpbr_not_show_report = false;
                bookResult.tpbr_active = true;
                bookResult.tpbr_update_by = user;
                bookResult.tpbr_update_date = dateNow;

                var xray = DoctorXrayBS.OfType<trn_doctor_xray>().FirstOrDefault();
                if (xray.trxr_create_date == null)
                {
                    xray.trxr_create_by = user;
                    xray.trxr_create_date = dateNow;
                }
                xray.trxr_update_by = user;
                xray.trxr_update_date = dateNow;
                cdc.trn_doctor_xray_images.DeleteAllOnSubmit(xray.trn_doctor_xray_images);
                for (int i = 0; i < listViewImgChest.Items.Count; i++)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    ListImage[i].Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    xray.trn_doctor_xray_images.Add(new trn_doctor_xray_image
                    {
                        trxm_image = ms.ToArray(),
                        trxm_sel_image = listViewImgChest.Items[i].Checked ? 'S' : 'N',
                        trxm_status = 'A',
                        trxm_create_by = user,
                        trxm_create_date = dateNow,
                        trxm_update_by = user,
                        trxm_update_date = dateNow
                    });
                }

                var retri = cdc.trn_patient_retrieves.Where(x => x.tpr_id == _tpr_id && x.tpr_image_type == _trxr_type).FirstOrDefault();
                if (retri == null)
                {
                    retri = new trn_patient_retrieve();
                    retri.tpr_id = _tpr_id;
                    retri.tpr_image_type = _trxr_type;
                    cdc.trn_patient_retrieves.InsertOnSubmit(retri);
                }
                retri.tpr_flag_retrieve = false;
                cdc.SubmitChanges();
                lblMsg.Text = "Save Data Completed.";
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Please try again.";
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void AddImage(Image img, bool selected)
        {
            var imgDisplay = ResizeImageWithBorder(img, 300, 300);
            imageListSmall.Images.Add(imgDisplay);
            ListImageDisplay.Add(imgDisplay);
            ListImage.Add(imgDisplay);
            listViewImgChest.Items.Add(new ListViewItem
            {
                ImageIndex = listViewImgChest.Items.Count,
                Text = "Select Image.",
                Checked = selected
            });
        }
        private void AddNewImage(Image img)
        {
            int inx = ImageIndexOf(img);
            if (inx < 0)
            {
                AddImage(img, true);
            }
            else
            {
                var result = MessageBox.Show("คุณได้เพิ่มรูปภาพนี้ไปแล้ว คุณต้องการเพิ่มอีก หรือไม่?", "Same Image!!!", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    AddImage(img, true);
                }
            }
        }
        private int ImageIndexOf(Image img)
        {
            var imgData = ResizeImageWithBorder(img, 300, 300);
            for (int i = 0; i < ListImage.Count; i++)
            {
                if (Utility.CompareImage(imgData, ListImage[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        private void DeleteImage()
        {
            for (int i = 0; i < listViewImgChest.Items.Count; i++)
            {
                if (listViewImgChest.Items[i].Checked)
                {
                    listViewImgChest.Items.RemoveAt(i);
                    ListImage.RemoveAt(i);
                    ListImageDisplay.RemoveAt(i);
                    imageListSmall.Images.RemoveAt(i);
                }
            }
            if (listViewImgChest.Items.Count > 0)
            {
                listViewImgChest.Items[0].Checked = true;
            }
            else
            {
                pb.Image = null;
            }
        }

        private Image ResizeImageWithRatio(Image img, Size NewSize)
        {
            double perW = (double)NewSize.Width / (double)img.Size.Width;
            double perH = (double)NewSize.Height / (double)img.Size.Height;

            Size _NewSize = new Size();
            if (perW < perH)
            {
                _NewSize.Width = NewSize.Width;
                _NewSize.Height = Convert.ToInt32((double)img.Height * perW);
            }
            else
            {
                _NewSize.Width = Convert.ToInt32((double)img.Width * perH);
                _NewSize.Height = NewSize.Height;
            }

            return new Bitmap(img, _NewSize);
        }
        private Image ResizeImageWithBorder(Image img, int width, int height)
        {
            double perW = (double)width / (double)img.Size.Width;
            double perH = (double)height / (double)img.Size.Height;

            Size newSize = new Size();
            Point newPoint = new Point();
            if (perW < perH)
            {
                newSize.Width = width;
                newSize.Height = Convert.ToInt32((double)img.Height * perW);

                newPoint.X = 0;
                newPoint.Y = (height - newSize.Height) / 2;
            }
            else
            {
                newSize.Width = Convert.ToInt32((double)img.Width * perH);
                newSize.Height = height;

                newPoint.X = (width - newSize.Width) / 2;
                newPoint.Y = 0;
            }

            Bitmap newImg = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(newImg);
            g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#ffffff")), new Rectangle(Point.Empty, newImg.Size));
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(img, new Rectangle(newPoint, newSize),
                    new Rectangle(new Point(), img.Size), GraphicsUnit.Pixel);
            return newImg;
        }
    }
}
