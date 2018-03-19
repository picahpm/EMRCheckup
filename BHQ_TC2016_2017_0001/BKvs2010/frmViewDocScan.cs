using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BKvs2010.Class;

namespace BKvs2010
{
    public partial class frmViewDocScan : Form
    {
        public frmViewDocScan()
        {
            InitializeComponent();
        }

        int countpage = 0;
        public int H_countpage = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            countpage++;
            lblPage.Text = HistoryData.count + 1 + "/" + HistoryData.filelength.ToString();
            CurrentPage.currentPage = HistoryData.count + 1;
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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        { 
            this.Close();
            frmViewscan newFrm = new frmViewscan();
            newFrm.Show();
        }

        DocScan doc = new DocScan();
        private void btnFirst_Click(object sender, EventArgs e)
        {
            #region Commented
            //if (HistoryData.count - 1 >= 0)
            //{
            //    pictureBox1.Image = doc.resizeImage((Image)HistoryData.arrlist[HistoryData.count - 1], new Size(450, 500));
            //    //HistoryData.count = HistoryData.count - 1 + 1;
            //    c = HistoryData.count - 1 + 1;
            //    //lblPage.Text = lblPage.Text = HistoryData.count + "/" + HistoryData.filelength.ToString();
            //    lblPage.Text = lblPage.Text = c + "/" + HistoryData.filelength.ToString();
            //}
            #endregion

            try
            {
                if (countpage >= 0)
                {
                    countpage--;
                    //pictureBox1.Image = doc.resizeImage((Image)HistoryData.arrlist[countpage - 1], new Size(450, 500));
                    pictureBox1.Image = doc.resizeImage((Image)HistoryData.arrlist[countpage - 1], new Size(450, 500));
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
            #region Commented
            //if (HistoryData.count + 1 < HistoryData.filelength)
            //{
            //    pictureBox1.Image = doc.resizeImage((Image)HistoryData.arrlist[HistoryData.count + 1], new Size(450, 500));
            //   // HistoryData.count = HistoryData.count + 1;
            //    c = HistoryData.count + 1;
            //    //lblPage.Text = HistoryData.count + 1 + "/" + HistoryData.filelength.ToString();
            //    lblPage.Text = c + 1 + "/" + HistoryData.filelength.ToString();
            //}
            #endregion
            try
            {
                if (countpage < HistoryData.filelength)
                {
                    countpage++;
                    //pictureBox1.Image = doc.resizeImage((Image)HistoryData.arrlist[countpage + 1], new Size(450, 500));
                    pictureBox1.Image = doc.resizeImage((Image)HistoryData.arrlist[countpage-1], new Size(450, 500));
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

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pictureBox1, "Zoom");
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
