using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckupBO
{
    public partial class frmPopupRemark : Form
    {
        public frmPopupRemark()
        {
            InitializeComponent();
        }
        public string strTextValue;
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtValue.Text == "")
            {
                //txtValue.Text = ".";
            }
            strTextValue = txtValue.Text;
            this.Close();
        }

        private void frmPopupRemark_Load(object sender, EventArgs e)
        {
            txtValue.Focus();
        }
    }
}
