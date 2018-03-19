using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace CallDataTakeCareGetTextFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            this.Text = "Call Data From Take Care V.1.5" + " (GetTextFile)";
            btnCallData_Click(null, null);
        }
        private void btnCallData_Click(object sender, EventArgs e)
        {
            timer1_Tick(null, null);
        }
        private void btnStartTime_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                btnStartTime.Text = "Start Timer";
                timer1.Enabled = false;
            }
            else
            {
                btnStartTime.Text = "Stop Timer";
                timer1.Enabled = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            string strerror = "";
            try
            {
                lberror.Text = "";
                strerror = "Update Date =>";
                CheckDateAndUpdateCurrentDate();
                //strerror = "GetPTAppointment=>";
                //this.GetPTAppointment();
                //strerror = "GetPTArrived=>";
                //this.GetPTArrived();
                strerror = "GetTextFile=>";
                new Class.GetResultTextFileCls().retrieveHistory();
                //this.GetTextFile();
            }
            catch (Exception ex)
            {
                //ไม่แสดง Error กรณี Error
                lberror.Text = "Call Data Form Webservice :" + strerror + ex.Message;
            }
            if (DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 00, 0, 0))
            {
                System.Environment.Exit(1);
            }
            timer1.Start();
            GC.Collect();
        }
        private void CheckDateAndUpdateCurrentDate()
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    mst_cur_date objcurrentDate = (from t1 in dbc.mst_cur_dates select t1).FirstOrDefault();
                    DateTime currentddate = objcurrentDate.mcd_cur_date;
                    DateTime NowDate = DateTime.Now;
                    DateTime ResetDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day, 4, 0, 0);
                    if (currentddate < ResetDate)
                    {
                        dbc.ExecuteCommand("update mst_cur_date set mcd_cur_date={0}", ResetDate.ToString());
                        dbc.SubmitChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                lberror.Text = ex.Message;
            }
        }
    }
}
