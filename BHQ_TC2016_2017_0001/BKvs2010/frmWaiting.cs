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
    public partial class frmWaiting : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        public frmWaiting()
        {
            InitializeComponent();
            uiWaitingList1.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList2.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList3.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList4.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList5.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList6.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList7.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList8.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList9.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList10.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList11.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList12.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList13.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList14.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList15.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList16.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList17.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);
            uiWaitingList18.cancelQueueHandler += new Usercontrols.UIWaitingList.CancelQueueHandler(uiWaiting_cancelQueueHandler);

            uiWaitingList1.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList2.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList3.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList4.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList5.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList6.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList7.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList8.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList9.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList10.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList11.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList12.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList13.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList14.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList15.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList16.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList17.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
            uiWaitingList18.sendTocheckBHandler += new Usercontrols.UIWaitingList.SendToCheckpointBHandler(uiWaiting_sendTocheckBHandler);
        }

        private void frmWaiting_Load(object sender, EventArgs e)
        {
            this.Text = Program.GetRoomName("Waiting");
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            //Load Data
            if (Program.CurrentSite != null)
            {
                List<mst_hpc_site> objsite = (from t1 in dbc.mst_hpc_sites
                                              where t1.mhs_status == 'A' && t1.mhs_type == 'P'
                                              select t1).ToList();
                mst_hpc_site newselect = new mst_hpc_site();
                //newselect.mhs_id = 0;
                //newselect.mhs_ename = "Select All";
                //objsite.Add(newselect);

                DDSite.DataSource = objsite.OrderBy(x => x.mhs_id).ToList();
                DDSite.DisplayMember = "mhs_ename";
                DDSite.ValueMember = "mhs_id";
                DDSite.SelectedValue = Program.CurrentSite.mhs_id;

                uiQueue1.LoadData(dbc, Program.CurrentSite.mhs_id);
                BindData("", Program.CurrentSite.mhs_id);
            }

            frmbg.Close();
        }
        public void BindData(string strtxt,int mshID)
        {
            int[] a = { 1, 2, 3, 4, 5,6,7,8,9,10,11,12,13,14,15,16,17,18,19 };

            uiWaitingList1.ShowWaiting(a[0], strtxt, mshID, "RG");
            uiWaitingList2.ShowWaiting(a[1], strtxt, mshID, "BM");
            uiWaitingList3.ShowWaiting(a[2], strtxt, mshID, "SC");
            uiWaitingList4.ShowWaiting(a[3], strtxt, mshID, "DC");
            uiWaitingList5.ShowWaiting(a[4], strtxt, mshID, "CD");
            uiWaitingList6.ShowWaiting(a[5], strtxt, mshID, "XR");
            uiWaitingList7.ShowWaiting(a[6], strtxt, mshID, "US");
            uiWaitingList8.ShowWaiting(a[7], strtxt, mshID, "DM");
            uiWaitingList9.ShowWaiting(a[8], strtxt, mshID, "BD");
            uiWaitingList10.ShowWaiting(a[9], strtxt, mshID, "EM");
            uiWaitingList11.ShowWaiting(a[10], strtxt, mshID, "HS");
            uiWaitingList12.ShowWaiting(a[11], strtxt, mshID, "EK");
            uiWaitingList13.ShowWaiting(a[12], strtxt, mshID, "AB");
            uiWaitingList14.ShowWaiting(a[13], strtxt, mshID, "ES");
            uiWaitingList15.ShowWaiting(a[14], strtxt, mshID, "PT");
            uiWaitingList16.ShowWaiting(a[15], strtxt, mshID, "TE");
            uiWaitingList17.ShowWaiting(a[16], strtxt, mshID, "UG");
            uiWaitingList18.ShowWaiting(a[17], strtxt, mshID, "PH");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            int mshID = Convert.ToInt32(DDSite.SelectedValue);
            BindData(txtSearch.Text.Trim(), mshID);
            uiQueue1.LoadData(dbc, mshID);
            btnSearch.Enabled = true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        { 
            btnSearch.Enabled = false;
            int mshID = Convert.ToInt32(DDSite.SelectedValue);
            BindData(txtSearch.Text.Trim(), mshID);
            uiQueue1.LoadData(dbc, mshID);
            btnSearch.Enabled = true;
        }

        private void uiWaiting_cancelQueueHandler(object sender, Usercontrols.completeArgs e)
        {
            btnRefresh_Click(null, null);
        }

        private void uiWaiting_sendTocheckBHandler(object sender, Usercontrols.completeArgs e)
        {
            btnRefresh_Click(null, null);
        }
    }

   public class WaitQueue
    {
        public int msh_id { get; set; }
        public string coderoom { get; set; }
        public string RoomName { get; set; }
        public string QueueNo { get; set; }
        public string HN { get; set; }
        public string Name { get; set; }
        public string Callstatus { get; set; }
    }
}
