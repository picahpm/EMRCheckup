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
    public partial class frmPatientStatus : Form
    {
        public frmPatientStatus()
        {
            InitializeComponent();
        }

        private void frmPatientStatus_Load(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen(); 
            frmbg.Show();
            Application.DoEvents();

            uiFooter1.RoomCode = "XX";
            Program.FooterIsclick = true;
            uiFooter1.LoadData();

            frmbg.Close();
        }
    }
}
