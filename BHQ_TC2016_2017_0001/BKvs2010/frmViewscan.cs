using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BKvs2010.Class;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using imgbox.Windows.Forms;

namespace BKvs2010
{
    public partial class frmViewscan : Form
    {
        public frmViewscan()
        {
            InitializeComponent();
            this.UpdateStatusBar();
        }

        DocScan doc = new DocScan();
        int c;
        int countpage = 0;
        public int H_countpage = 0;

        private void frmViewscan_Load(object sender, EventArgs e)
        {
            try
            {
                frmViewDocScan frmClass = new frmViewDocScan();
                c = CurrentPage.currentPage;
                countpage = c;
                lblPage.Text = c + "/" + HistoryData.filelength.ToString();
                if (c == 1 || c == 0)
                    imageBox.Image = doc.resizeImage((Image)HistoryData.arrlist[0], new Size(850, 940));
                else
                    imageBox.Image = doc.resizeImage((Image)HistoryData.arrlist[c - 1], new Size(850, 940)); //HistoryData.newImage;
                ////Check image page
                if (countpage == 1 && HistoryData.filelength > 1)
                {
                    btnNext.Enabled = true;
                    btnFirst.Enabled = false;
                }
                if (countpage == 1 && HistoryData.filelength == 1)
                {
                    btnNext.Enabled = false;
                    btnFirst.Enabled = false;
                }
                button1_Click(null, null);
            }
            catch
            {
            }
        }

        #region PrivateMethods

        private void DrawBox(Graphics graphics, Color color, Rectangle rectangle)
        {
            int offset;
            int penWidth;
            offset = 9;
            penWidth = 2;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, color)))
                graphics.FillRectangle(brush, rectangle);

            using (Pen pen = new Pen(color, penWidth))
            {
                pen.DashStyle = DashStyle.Dot;
                graphics.DrawLine(pen, rectangle.Left, rectangle.Top - offset, rectangle.Left, rectangle.Bottom + offset);
                graphics.DrawLine(pen, rectangle.Left + rectangle.Width, rectangle.Top - offset, rectangle.Left + rectangle.Width, rectangle.Bottom + offset);
                graphics.DrawLine(pen, rectangle.Left - offset, rectangle.Top, rectangle.Right + offset, rectangle.Top);
                graphics.DrawLine(pen, rectangle.Left - offset, rectangle.Bottom, rectangle.Right + offset, rectangle.Bottom);
            }
        }

        #region UpdateBar
        private void UpdateStatusBar()
        {
            positionToolStripStatusLabel.Text = imageBox.AutoScrollPosition.ToString();
            imageSizeToolStripStatusLabel.Text = imageBox.GetImageViewPort().ToString();
            zoomToolStripStatusLabel.Text = string.Format("{0}%", imageBox.Zoom);
        }
        #endregion

        #region ZoomResizeScoll

        private void imageBox_ZoomChanged(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void imageBox_Resize(object sender, EventArgs e)
        {
            this.UpdateStatusBar();
        }

        private void imageBox_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            this.UpdateStatusBar();
        }

        #endregion



        #endregion Private Methods

        private void btnFirst_Click(object sender, EventArgs e)
        {
            try
            {
                if (countpage >= 0)
                {
                    countpage--;
                    imageBox.Image = doc.resizeImage((Image)HistoryData.arrlist[countpage - 1], new Size(850, 940));
                    lblPage.Text = lblPage.Text = countpage + "/" + HistoryData.filelength.ToString();
                    int reduceC;
                    reduceC = countpage - 1;//H_countpage - 1;
                    CurrentPage.currentPage = reduceC;
                    H_countpage = countpage + 1;
                    button1_Click(null, null);
                }
            }
            catch
            {
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (countpage < HistoryData.filelength)
                {
                    countpage++;
                    imageBox.Image = doc.resizeImage((Image)HistoryData.arrlist[countpage - 1], new Size(850, 940));
                    lblPage.Text = countpage + "/" + HistoryData.filelength.ToString();
                    H_countpage = countpage + 1;
                    CurrentPage.currentPage = countpage;// H_countpage;
                    button1_Click(null, null);
                }
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (countpage >= HistoryData.filelength)
            {
                if (countpage == 1)
                {
                    btnNext.Enabled = true;
                    btnFirst.Enabled = true;
                }
                else
                {
                    btnNext.Enabled = false;
                    btnFirst.Enabled = true;
                }
            }
            if (countpage < HistoryData.filelength)
            {
                if (countpage == 1)
                {
                    btnNext.Enabled = true;
                    btnFirst.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                    btnFirst.Enabled = true;
                }
            }
        }

    }
}
