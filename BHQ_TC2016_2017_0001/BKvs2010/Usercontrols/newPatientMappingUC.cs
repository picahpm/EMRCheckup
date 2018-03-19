using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DBCheckup;

namespace BKvs2010.Usercontrols
{
    public partial class newPatientMappingUC : UserControl
    {
        public newPatientMappingUC()
        {
            InitializeComponent();
        }
        private Color BgColor = Color.White;
        public Color BackgroundColor { set { BgColor = value; this.BackColor = BgColor; } }

        private int? _tpr_id = null;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value == null)
                {
                    PanelMapping.Controls.Clear();
                }
                else
                {
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            var objlist = cdc.trn_patient_queues
                                             .Where(x => x.tpr_id == value)
                                             .OrderBy(x => x.tps_create_date)
                                             .Select(x => new Mappingimage
                                             {
                                                 Img1 = x.mst_room_hdr.mrm_room_image,
                                                 Img2 = x.mst_room_hdr.mrm_wk_room_image,
                                                 Status = (x.tps_status == "NS") ? "WK" : x.tps_status,
                                                 RoomName = x.mst_room_hdr.mrm_ename
                                             }).ToList();

                            int icount = 0;
                            int widthsum = 0;
                            int heightsum = 0;
                            int imgsize = 24;

                            PanelMapping.Controls.Clear();
                            foreach (Mappingimage item in objlist)
                            {
                                int indexcount = icount % 10;
                                switch (indexcount)
                                {
                                    case 0:
                                        if (icount / 10 > 0)
                                        {
                                            heightsum = heightsum + (imgsize);
                                            widthsum = 0;

                                            this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[2]);
                                            heightsum = heightsum + (imgsize);
                                        }
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                    case 1:
                                        //noina test
                                        widthsum = widthsum + (imgsize);
                                        this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[1]);
                                        widthsum = widthsum + (imgsize);
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                    case 2:
                                        //noina test
                                        widthsum = widthsum + (imgsize);
                                        this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[1]);
                                        widthsum = widthsum + (imgsize);
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                    case 3:
                                        //noina test
                                        widthsum = widthsum + (imgsize);
                                        this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[1]);
                                        widthsum = widthsum + (imgsize);
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                    case 4:
                                        //noina test
                                        widthsum = widthsum + (imgsize);
                                        this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[1]);
                                        widthsum = widthsum + (imgsize);
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                    case 5:
                                        heightsum = heightsum + (imgsize);
                                        this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[2]);
                                        heightsum = heightsum + (imgsize);
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                    case 6:
                                    case 7:
                                    case 8:
                                        widthsum = widthsum - (imgsize);
                                        this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[3]);
                                        widthsum = widthsum - (imgsize);
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                    case 9:
                                        widthsum = widthsum - (imgsize);
                                        this.ShowAllow(widthsum, heightsum, imgsize, icount, imageList1.Images[3]);
                                        widthsum = 0;
                                        this.ShowImage(widthsum, heightsum, imgsize, item, icount);
                                        break;
                                }
                                icount = icount + 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "tpr_id", ex, false); 
                    }
                }
            }
        }

        private void ShowImage(int widthsum, int heightsum, int imgsize, Mappingimage item, int icount)
        {
            PictureBox imgroom5 = new PictureBox();
            imgroom5.Name = "imgroom5" + icount.ToString();
            imgroom5.SizeMode = PictureBoxSizeMode.StretchImage;
            if (item.Status.ToUpper() == "WK")
            {
                imgroom5.Image = Program.byteArrayToImage(item.Img2);
            }
            else
            {
                imgroom5.Image = Program.byteArrayToImage(item.Img1);
            }
            imgroom5.Size = new Size(imgsize, imgsize);
            imgroom5.Tag = item;
            ToolTip ToolTip2 = new ToolTip(); ToolTip2.IsBalloon = true;
            ToolTip2.SetToolTip(imgroom5, item.RoomName.ToString());
            PanelMapping.Controls.Add(imgroom5);
            imgroom5.Left = widthsum;
            imgroom5.Top = heightsum;
        }
        private void ShowAllow(int widthsum, int heightsum, int imgsize, int icount, Image img)
        {
            PictureBox itemAllow = new PictureBox();
            itemAllow.Name = "itemAllow" + icount.ToString();
            itemAllow.SizeMode = PictureBoxSizeMode.StretchImage;
            itemAllow.Image = img;
            itemAllow.Size = new Size(imgsize, imgsize);
            PanelMapping.Controls.Add(itemAllow);
            itemAllow.Left = widthsum;
            itemAllow.Top = heightsum;
        }

        class Mappingimage
        {
            private string status;
            private byte[] img1;
            private byte[] img2;
            private string roomName;

            public byte[] Img1 { get { return img1; } set { img1 = value; } }
            public byte[] Img2 { get { return img2; } set { img2 = value; } }
            public string Status { get { return status; } set { status = value; } }
            public string RoomName { get { return roomName; } set { roomName = value; } }
        }
    }
}
