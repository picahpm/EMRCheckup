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
    public partial class frmChoicePopUp : Form
    {
        public frmChoicePopUp()
        {
            InitializeComponent();
        }
        private string SetTitle
        {
            set
            {
                lbQuestions.Text = value;
            }
        }
        private string SetButtonAText
        {
            set
            {
                btnToback.Text = value;
            }
        }
        private string SetButtonBText
        {
            set
            {
                btnSendtonext.Text = value;
            }
        }
        private void btnToback_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void btnSendtonext_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
