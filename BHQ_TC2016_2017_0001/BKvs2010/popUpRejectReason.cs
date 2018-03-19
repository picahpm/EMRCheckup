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
    public partial class popUpRejectReason : Form
    {
        public popUpRejectReason()
        {
            InitializeComponent();
        }

        public popUpRejectReason(string reason)
        {
            InitializeComponent();
            txtRejectReason.Text = reason;
            this.ShowDialog();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
