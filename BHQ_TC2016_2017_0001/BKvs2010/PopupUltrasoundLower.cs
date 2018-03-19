using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKvs2010
{
    public partial class PopupUltrasoundLower : Form
    {
        public enum ResultPopupUltrasoundLower
        {
            BeforeStation,
            AfterStation,
            AskMeLater
        }

        private ResultPopupUltrasoundLower result = ResultPopupUltrasoundLower.AskMeLater;
        //private bool result = false;

        public PopupUltrasoundLower()
        {

            InitializeComponent();
            pictureBox1.Image = SystemIcons.Exclamation.ToBitmap();
        }

        public ResultPopupUltrasoundLower questionUltrasoundLower
        {
            get
            {
                this.ShowDialog();
                return result;
            }
        }

        private void btnBeforeStation_Click(object sender, EventArgs e)
        {
            result = ResultPopupUltrasoundLower.BeforeStation;
            this.Close();
        }

        private void btnAskMeLater_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAferStation_Click(object sender, EventArgs e)
        {
            result = ResultPopupUltrasoundLower.AfterStation;
            this.Close();
        }
    }
}
