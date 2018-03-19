using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BKvs2010
{
    public partial class frmDashBoardcs : Form
    {
        public frmDashBoardcs()
        {
            InitializeComponent();
        }

        private void frmDashBoardcs_Load(object sender, EventArgs e)
        {
            timerRefreshDashBoard.Enabled = true;
            int mhs_id = Program.CurrentSite.mhs_id;
            uiDashBoard.loadDashBoard(mhs_id);
            timerRefreshDashBoard.Start();
        }

        private void uiDashBoard_ChangeDoctorComplete(object sender)
        {
            timerRefreshDashBoard.Stop();
            //int mhs_id = Program.CurrentSite.mhs_id;
            //uiDashBoard.loadDashBoard(mhs_id);
            uiDashBoard.Refresh();
            timerRefreshDashBoard.Start();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            timerRefreshDashBoard.Stop();
            //int mhs_id = Program.CurrentSite.mhs_id;
            //uiDashBoard.loadDashBoard(mhs_id);
            uiDashBoard.Refresh();
            timerRefreshDashBoard.Start();
        }

        private void uiDashBoard_Click(object sender, EventArgs e)
        {

        }

        private void timerRefreshDashBoard_Tick(object sender, EventArgs e)
        {
            timerRefreshDashBoard.Stop();
            //int mhs_id = Program.CurrentSite.mhs_id;
            //uiDashBoard.loadDashBoard(mhs_id);
            uiDashBoard.Refresh();
            timerRefreshDashBoard.Start();
        }

        private void frmDashBoardcs_FormClosed(object sender, FormClosedEventArgs e)
        {
            timerRefreshDashBoard.Stop();
            timerRefreshDashBoard.Enabled = false;
        }

        private void uiDashBoard_ChangingDoctor(object sender)
        {
            timerRefreshDashBoard.Stop();
        }

        private void btnPointMaster_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo("iexplore.exe", "http://10.88.26.55/pointpackage");
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Program.MessageError("UIMenuBar", "PointMasterMenuItem_Click", ex, false);
            }
        }

       
    }
}
