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
    public partial class frmAllRoom : Form
    {
        public frmAllRoom()
        {
            InitializeComponent();
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            frmPatient frmReg = new frmPatient();
            //frmReg.MdiParent = this.MdiParent;
            frmReg.ShowDialog();
        }
        private void btnUserData_Click(object sender, EventArgs e)
        {
            frmSetupUser frmSetUpUser = new frmSetupUser();
            //frmSetUpUser.MdiParent = this.MdiParent;
            frmSetUpUser.ShowDialog();
        }
        private void btnPackagemenu_Click(object sender, EventArgs e)
        {
            frmMappingItem frmmapitem = new frmMappingItem();
            //frmmapitem.MdiParent = this.MdiParent;
            frmmapitem.ShowDialog();
        }

        private void btnRoomEvent_Click(object sender, EventArgs e)
        {
            frmSetupRoom frmroom = new frmSetupRoom();
            //frmroom.MdiParent = this.MdiParent;
            frmroom.ShowDialog();
        }

        private void btnRoomUnlock_Click(object sender, EventArgs e)
        {
            frmUnlockRoom frmUnRoom = new frmUnlockRoom();
            //frmUnRoom.MdiParent = this.MdiParent;
            frmUnRoom.ShowDialog();
        }

        private void btnsetupdata_Click(object sender, EventArgs e)
        {
            frmSetupData frmdt = new frmSetupData();
            //frmdt.MdiParent = this.MdiParent;
            frmdt.ShowDialog();
        }

        private void btnLab_Click(object sender, EventArgs e)
        {
            frmLabCalculate frmlbcal = new frmLabCalculate();
            //frmlbcal.MdiParent = this.MdiParent;
            frmlbcal.ShowDialog();
        }

        private void btnEditQueue_Click(object sender, EventArgs e)
        {
            frmQueueManage frmlbQueue = new frmQueueManage();
            //frmlbcal.MdiParent = this.MdiParent;
            frmlbQueue.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManagePlanFrm frm = new ManagePlanFrm();
            frm.ShowDialog();
        }


    }
}
