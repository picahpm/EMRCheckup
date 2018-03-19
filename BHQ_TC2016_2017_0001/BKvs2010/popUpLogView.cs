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
    public partial class popUpLogView : Form
    {
        public popUpLogView()
        {
            InitializeComponent();
        }

        public void showLog(int mut_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                var log = cdc.log_user_logins
                             .Where(x => x.mut_id == mut_id &&
                                         x.lug_start_date.Value.Date == dateNow.Date &&
                                         cdc.mst_room_dtls.Where(y => y.mrd_id == x.mrd_id).Select(y => y.mrd_room_no).FirstOrDefault() != null &&
                                         x.mrd_id != null)
                             .Select(x => new
                             {
                                 room = cdc.mst_room_dtls.Where(y => y.mrd_id == x.mrd_id).Select(y => y.mrd_room_no).FirstOrDefault(),
                                 ip = x.lug_ip_address,
                                 time_in = x.lug_start_date,
                                 time_out = x.lug_end_date,
                                 Sitename = cdc.mst_hpc_sites.Where(z=>z.mhs_id==x.mhs_id).Select(z=>z.mhs_ename).FirstOrDefault()
                             }).ToList();
                dataGridView1.DataSource = log;
            }
            this.ShowDialog();
        }
    }
}
