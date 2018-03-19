using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
namespace BKvs2010.Usercontrols
{
    public partial class UIQueue : UserControl
    {
        public UIQueue()
        {
            InitializeComponent();
        }
        public void LoadData(CheckupDataContext dbc,int mshID)
        {
            // int mhs_id = string.IsNullOrEmpty(Program.CurrentSite.mhs_id.ToString()) ? int.MinValue : Convert.ToInt32(Program.CurrentSite.mhs_id.ToString());
            var waitingList = (from view1 in dbc.vw_waiting_rooms
                               where view1.mhs_id == mshID
                               orderby view1.mrm_seq_show
                               select new 
                               {
                                   Ename = view1.mrm_ename,
                                   Waiting =(view1.waiting_ps==null)?0:view1.waiting_ps,
                                   time = (view1.waiting_time == null) ? 0 : view1.waiting_time 
                               }).ToList();
            DGVStation.DataSource = new SortableBindingList<waintingroomqueue>(waitingList.Select((item, index) => new waintingroomqueue
            {
                No = index + 1,
                Ename = item.Ename,
                Waiting =Convert.ToInt32( item.Waiting),
                time = Convert.ToInt32(item.time)
            }).ToList());
            lbsummaryQTY.Text = waitingList.Sum(x=>x.Waiting).ToString();
            lbSummaryTime.Text = waitingList.Sum(x => x.time).ToString();
        }
    }
    class waintingroomqueue
    {
        public int No { get; set; }
        public string Ename { get; set; }
        public int Waiting { get; set; }
        public int time { get; set; }
    }
}
