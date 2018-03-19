using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BKvs2010.Forms
{
    public partial class WaitingListFrm : Form
    {
        public WaitingListFrm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            newWaitingListUC1.mhs_id = 1;
            newWaitingListUC2.mhs_id = 1;
            newWaitingListUC3.mhs_id = 1;
            newWaitingListUC4.mhs_id = 1;
            newWaitingListUC5.mhs_id = 1;
            newWaitingListUC6.mhs_id = 1;
            newWaitingListUC7.mhs_id = 1;
            newWaitingListUC8.mhs_id = 1;
            newWaitingListUC9.mhs_id = 1;
            newWaitingListUC10.mhs_id = 1;
            newWaitingListUC11.mhs_id = 1;
            newWaitingListUC12.mhs_id = 1;
            newWaitingListUC13.mhs_id = 1;
            newWaitingListUC14.mhs_id = 1;
            newWaitingListUC15.mhs_id = 1;
            newWaitingListUC16.mhs_id = 1;
            newWaitingListUC17.mhs_id = 1;
            newWaitingListUC18.mhs_id = 1;


            newWaitingListUC1.LoadData();
            newWaitingListUC2.LoadData();
            newWaitingListUC3.LoadData();
            newWaitingListUC4.LoadData();
            newWaitingListUC5.LoadData();
            newWaitingListUC6.LoadData();
            newWaitingListUC7.LoadData();
            newWaitingListUC8.LoadData();
            newWaitingListUC9.LoadData();
            newWaitingListUC10.LoadData();
            newWaitingListUC11.LoadData();
            newWaitingListUC12.LoadData();
            newWaitingListUC13.LoadData();
            newWaitingListUC14.LoadData();
            newWaitingListUC15.LoadData();
            newWaitingListUC16.LoadData();
            newWaitingListUC17.LoadData();
            newWaitingListUC18.LoadData();
        }
    }
}
