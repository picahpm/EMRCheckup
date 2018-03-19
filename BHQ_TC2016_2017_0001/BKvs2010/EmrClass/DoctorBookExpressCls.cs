using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class DoctorBookExpressCls
    {
        public DoctorBookExpressCls()
        {
            list_en = new List<string>();
            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += timer_Tick;
        }
        public event OnExpressBookChanged ExpressBookChanged;
        public delegate void OnExpressBookChanged(object sender, int numberExpress, int numberNotExpress);
        private void _ExpressBookChanged()
        {
            // Make sure someone is listening to event
            if (ExpressBookChanged == null) return;
            ExpressBookChanged(this, _NumberExpress, _NumberNotExpress);
        }

        public void Start()
        {
            CurrentSecond = 0;
            timer.Start();
        }
        public void Stop()
        {
            CurrentSecond = 0;
            timer.Stop();
        }

        public string Username { get; set; }

        private int _NumberNotExpress = 0;
        public int NumberNotExpress
        {
            get { return _NumberNotExpress; }
        }
        private int _NumberExpress = 0;
        public int NumberExpress
        {
            get { return _NumberExpress; }
        }
        int CurrentSecond = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            CurrentSecond++;
            if (CurrentSecond == SecondsToRefresh)
            {
                timer.Stop();
                CurrentSecond = 0;
                CheckBookExpress();
            }
        }
        private List<string> list_en;
        private void CheckBookExpress()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<string> list_new_en = cdc.trn_patient_regis
                                                  .Where(x => x.tpr_send_book == '1' &&
                                                              x.trn_patient_doctor.tpd_doctor_code == Username &&
                                                              x.trn_patient_doctor_approve.tpda_status == "WFA")
                                                  .Select(x => x.tpr_en_no).ToList();
                    if (list_new_en.Any(x => !list_en.Contains(x)) || list_new_en.Count() != list_en.Count())
                    {
                        list_en = list_new_en;
                        _NumberExpress = list_new_en.Count();
                        _ExpressBookChanged();
                    }
                }
            }
            catch
            {

            }
            timer.Start();
        }

        private Timer timer;
        public int SecondsToRefresh { get; set; }
    }
}
