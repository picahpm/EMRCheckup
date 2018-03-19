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
    public partial class frmPopupBookFinishRemark : Form
    {
        private bookFinish _bookFinish = new bookFinish();
        public struct bookFinish
        {
            public bool save;
            public dtlBoookFinish detail;
        }
        public struct dtlBoookFinish
        {
            public bool send_email;
            public bool send_post;
            public string remark;
        }

        public frmPopupBookFinishRemark()
        {
            InitializeComponent();
        }
        public bookFinish popupFinish()
        {
            this.ShowDialog();
            return _bookFinish;
        }
        private void chkEmail_CheckedChanged(object sender, EventArgs e)
        {
            _bookFinish.detail.send_email = chkEmail.Checked;
        }
        private void chkPost_CheckedChanged(object sender, EventArgs e)
        {
            _bookFinish.detail.send_post = chkPost.Checked;
        }
        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            _bookFinish.detail.remark = txtRemark.Text;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            _bookFinish.save = true;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _bookFinish.save = false;
            this.Close();
        }
    }
}
