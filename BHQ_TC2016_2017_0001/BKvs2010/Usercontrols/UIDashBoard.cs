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
    public partial class UIDashBoard : UserControl
    {
        public event ChangeDoctorComplete changeComplete;
        public delegate void ChangeDoctorComplete(object sender);
        private void changeDoctorComplete()
        {
            // Make sure someone is listening to event
            if (changeComplete == null) return;
            changeComplete(this);
        }
        public event changingDoctor ChangingDoctor;
        public delegate void changingDoctor(object sender);
        private void changing()
        {
            // Make sure someone is listening to event
            if (ChangingDoctor == null) return;
            ChangingDoctor(this);
        }

        public UIDashBoard()
        {
            InitializeComponent();
        }

        public void loadDashBoard(int site_id)
        {
            pnDash.Controls.Clear();
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                List<int> list_mrm_id = cdc.mst_room_hdrs.Where(x => x.mrm_show_dashboard == true &&
                                                                     x.mhs_id == site_id)
                                           .Select(x => x.mrm_id).ToList();
                List<mst_room_dtl> listRoomDoctor = cdc.mst_room_dtls
                                                       .Where(x => list_mrm_id.Contains(x.mrm_id) &&
                                                                   (x.mrd_effective_date == null ? true : x.mrd_effective_date < dateNow) &&
                                                                   (x.mrd_expire_date == null ? true : x.mrd_expire_date > dateNow) &&
                                                                   x.mrd_status == 'A')
                                                       .OrderBy(x => x.mrd_room_no).ToList();

                for (int i = 0; i < listRoomDoctor.Count(); i++)
                {
                    UIGridChangeDoctor ui = new UIGridChangeDoctor();
                    ui.showDashBoard(listRoomDoctor[i].mrd_id);
                    ui.ChangeComplete += new UIGridChangeDoctor.changeDoctorComplete(ui_ChangeDoctorComplete);
                    ui.ChangingDoctor += new UIGridChangeDoctor.changingDoctor(ui_changingDoctor);
                    this.pnDash.Controls.Add(ui);
                    ui.Top = (int)Math.Floor((decimal)(i / 2)) * (new UIGridChangeDoctor().Height + 14);
                    ui.Left = (i % 2) * (new UIGridChangeDoctor().Width + 14);
                    Visible = true;
                    ui.BringToFront();
                }
            }
        }

        private void ui_ChangeDoctorComplete(object sender)
        {
            changeDoctorComplete();
        }

        private void ui_changingDoctor(object sender)
        {
            changing();
        }

        public override void Refresh()
        {
            foreach (Control ctrl in this.pnDash.Controls)
            {
                if (ctrl.GetType() == typeof(UIGridChangeDoctor))
                {
                    ((UIGridChangeDoctor)ctrl).Refresh();
                }
            }
            base.Refresh();
        }
    }
}