using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Data.Common;

namespace BKvs2010
{
    public partial class frmChoicePRM : Form
    {
        public enum resultSendTo
        {
            SendToCheckPointC,
            SendToBook
        }

        public resultSendTo Result { get; set; }

        public frmChoicePRM()
        {
            InitializeComponent();
        }

        DateTime dtnow = Program.GetServerDateTime();
        string roomname = String.Empty;

        private string SetSendC
        {
            set { btnToC.Text = value; }
        }

        private string SetSendBook
        {
            set { btnSendBook.Text = value; }
        }

        private void btnToC_Click(object sender, EventArgs e)
        {
            Result = resultSendTo.SendToCheckPointC;
            this.DialogResult = DialogResult.Yes;
        }

        private void btnSendBook_Click(object sender, EventArgs e)
        {
            Result = resultSendTo.SendToBook;
            this.DialogResult = DialogResult.No;
        }
    }
}
