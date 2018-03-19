using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.IO;
using System.Configuration;//add suriya 18/03/2015

namespace CallDataTakeCareArrived
{
    public partial class Form1 : Form
    {
        //add suriya 18/03/2015
        string SourcePath;
        string LocalPath;
        string LocalPathSuccess;
        string LocalPathError;
        string LocalPathLog;
        //end suriya 18/03/2015
        string LocalPathRerun;//add suriya 22/05/2015
        string IsRerun;//add suriya 22/05/2015

        string LocalPathRegOtherLocation;

        public Form1()
        {
            InitializeComponent();

            //add suriya 18/03/2015
            SourcePath = System.Configuration.ConfigurationManager.AppSettings["SourceFilePathReg"];
            LocalPath = System.Configuration.ConfigurationManager.AppSettings["LocalPathReg"];
            LocalPathSuccess = System.Configuration.ConfigurationManager.AppSettings["LocalPathRegSuccess"];
            LocalPathError = System.Configuration.ConfigurationManager.AppSettings["LocalPathRegError"];
            LocalPathLog = System.Configuration.ConfigurationManager.AppSettings["LocalPathRegLog"];
            LocalPathRerun = System.Configuration.ConfigurationManager.AppSettings["LocalPathRegRerun"];
            IsRerun = System.Configuration.ConfigurationManager.AppSettings["IsRerun"];
            LocalPathRegOtherLocation = System.Configuration.ConfigurationManager.AppSettings["LocalPathRegOtherLocation"];

            LocalPath = Application.StartupPath + LocalPath;
            LocalPathSuccess = Application.StartupPath + LocalPathSuccess;
            LocalPathError = Application.StartupPath + LocalPathError;
            LocalPathLog = Application.StartupPath + LocalPathLog;
            LocalPathRerun = Application.StartupPath + LocalPathRerun;
            LocalPathRegOtherLocation = Application.StartupPath + LocalPathRegOtherLocation;
            //end suriya 18/03/2015
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            this.Text = "Call Data From Take Care V.1.5" + " (Arrived) IsRerun = " + IsRerun;
            btnStartTime_Click(null, null);
        }

        private class site
        {
            public string sub_site { get; set; }
            public string main_site { get; set; }
        }

        private List<site> mst_site = new List<site>
        {
            new site { sub_site = "01AMSCHK", main_site = "01AMS" }
        };

        //private void GetPTArrived()// del suriya 18/03/2015
        private void GetPTArrived(string SourcePath, string LocalPath, string LocalPathSuccess, string LocalPathError, string LocalPathLog)// add suriya 18/03/2015
        {
            using (InhCheckupDataContext dct = new InhCheckupDataContext())
            {
                DateTime dateNow = Program.GetServerDateTime();
                new Class.RetrieveArrivedCls().processAll(SourcePath, LocalPath, LocalPathSuccess, LocalPathError, LocalPathLog, dateNow, LocalPathRerun, IsRerun, LocalPathRegOtherLocation, chkWebservice.Checked);// add suriya 18/03/2015
            }
        }
        //****************

        private void btnCallData_Click(object sender, EventArgs e)
        {
            timer1_Tick(null, null);
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
                strerror = "GetPTArrived=>";
                this.GetPTArrived(SourcePath, LocalPath, LocalPathSuccess, LocalPathError, LocalPathLog);//edit suriya 18/03/2015
            }
            catch (Exception ex)
            {
                //ไม่แสดง Error กรณี Error
                lberror.Text = "Call Data Form Webservice :" + strerror + ex.Message;
            }
            if (DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 00, 0, 0))
            {
                try
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                         cdc.proc_Clear_PatientArrived();
                        //cdc.tmp_getptarriveds.DeleteAllOnSubmit(cdc.tmp_getptarriveds);
                    }
                }
                catch
                {

                }
                System.Environment.Exit(1);
            }
            timer1.Start();
            GC.Collect();
        }
        private void CheckDateAndUpdateCurrentDate()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_cur_date objcurrentDate = (from t1 in cdc.mst_cur_dates select t1).FirstOrDefault();
                    DateTime currentddate = objcurrentDate.mcd_cur_date;
                    DateTime NowDate = DateTime.Now;
                    DateTime ResetDate = new DateTime(NowDate.Year, NowDate.Month, NowDate.Day, 4, 0, 0);
                    if (currentddate.Date != ResetDate.Date)
                    {
                        cdc.ExecuteCommand("update mst_cur_date set mcd_cur_date={0}", ResetDate.ToString());
                        cdc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                lberror.Text = ex.Message;
            }
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
    }//end Class
}//End Name Spec
