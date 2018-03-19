using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using DBCheckup;
namespace BKvs2010.Class
{
    class countDownCls
    {
        public countDownCls()
        {
            time.Tick += new EventHandler(time_Tick);
            time.Interval = 1000;
        }
        public event TickHandler countDownTick;
        public delegate void TickHandler(object sender, timeArgs e);
        private void tickHandler(timeArgs e)
        {
            // Make sure someone is listening to event
            if (countDownTick == null) return;
            countDownTick(this, e);
        }

        public delegate void DisposeHandler(object sender, timeArgs e);
        public event DisposeHandler disposeCountDown;
        private void disposeHandler(timeArgs e)
        {
            // Make sure someone is listening to event
            if (disposeCountDown == null) return;
            disposeCountDown(this, e);
        }

        public delegate void SuccessHandler(object sender, successTypeArgs e);
        public event SuccessHandler successCountDown;
        private void successHandler(successTypeArgs e)
        {
            // Make sure someone is listening to event
            if (successCountDown == null) return;
            successCountDown(this, e);
        }

        Timer time = new Timer();
        int secCountDown;
        Label lblShowTime;
        public void startCountDown(int minutes)
        {
            secCountDown = minutes * 60;
            startCountDown();
        }
        public void startCountDown(int minutes, int seconds)
        {
            secCountDown = (minutes * 60) + seconds;
            startCountDown();
        }
        public void startCountDown(int minutes, int seconds, Label lableShowTime)
        {
            secCountDown = (minutes * 60) + seconds;
            startCountDown();
        }
        public void startCountDown(int minutes, Label lableShowTime)
        {
            lblShowTime = lableShowTime;
            secCountDown = (minutes * 60);
            startCountDown();
        }
        private void startCountDown()
        {
            time.Start();
        }
        private void time_Tick(object sender, EventArgs e)
        {
            secCountDown -= 1;
            timeStruct _getTime = getTime;
            if (lblShowTime != null) lblShowTime.Text = _getTime.timeString;
            tickHandler(new timeArgs(_getTime));
            if (secCountDown == 0)
            {
                time.Stop();
                successCountDown(this, new successTypeArgs(false, getTime));
            }
        }
        private timeStruct getTime
        {
            get
            {
                string min = Convert.ToInt32(secCountDown / 60).ToString() + "m";
                string sec = (secCountDown % 60).ToString("00") + "s";
                return new timeStruct(min + " " + sec, Convert.ToInt32(secCountDown / 60), (secCountDown % 60));
            }
        }
        public void finishCountDown()
        {
            time.Stop();
            string min = Convert.ToInt32(secCountDown / 60).ToString() + "m";
            string sec = (secCountDown % 60).ToString("00") + "s";
            successHandler(new successTypeArgs(true, getTime));
        }
        public void cancelCountDown()
        {
            time.Stop();
            string min = Convert.ToInt32(secCountDown / 60).ToString() + "m";
            string sec = (secCountDown % 60).ToString("00") + "s";
            disposeHandler(new timeArgs(getTime));
        }

        public int GetTimeCountDown()
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    int timeunit = (int)dbc.func_get_countdown_time_unit_display(Program.CurrentSite.mhs_id);
                    //timeunit = 1;
                    return timeunit;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("countDownCls", "GetTimeCountDown", ex, false);
                return 2;
            }
        }
    }

    class timeArgs : EventArgs
    {
        public timeStruct countDownTime { get; private set; }
        public timeArgs(timeStruct _countDownTime)
        {
            countDownTime = _countDownTime;
        }
    }
    class successTypeArgs : EventArgs
    {
        [Description("true = command from method quitCountDown(), false = command from end time")]
        public bool end { get; private set; } // true = command from method quitCountDown(), false = command from end time
        [Description("Remain Time")]
        public timeStruct countDownTime { get; private set; } // remain time
        public successTypeArgs(bool _end, timeStruct _countDownTime)
        {
            end = _end;
            countDownTime = _countDownTime;
        }
    }
    class timeStruct
    {
        [Description("Time format #0m 00s")]
        public string timeString { get; set; }
        public int timeMinutes { get; set; }
        public int timeSeconds { get; set; }
        public timeStruct(string _timeString, int _timeMin, int _timeSec)
        {
            timeString = _timeString;
            timeMinutes = _timeMin;
            timeSeconds = _timeSec;
        }
    }
}
