using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace CallDataTakeCare
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
            this.Text = "Call Data From Take Care V.1.5 (Appointment)";
            this.GetPTAppointment();
        }
        private bool _enable = true;
        private bool enable
        {
            get { return _enable; }
            set
            {
                if (value != _enable)
                {
                    if (value)
                    {
                        btnStartTime.Text = "Stop Retrieve";
                        this.GetPTAppointment();
                    }
                    else
                    {
                        btnStartTime.Text = "Start Retrieve";
                    }
                    _enable = value;
                }
            }
        }
        private void btnStartTime_Click(object sender, EventArgs e)
        {
            enable = !_enable;
        }
        private void GetPTAppointment()
        {
            if (DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 00, 0, 0))
            {
                System.Environment.Exit(1);
            }
            else
            {
                if (_enable)
                {
                    try
                    {
                        using (InhCheckupDataContext dct = new InhCheckupDataContext())
                        {
                            string Datesearch = DateTime.Now.Date.ToString("yyyy-MM-dd");
                            List<string> listSite = dct.mst_hpc_sites.Where(x => x.mhs_status == 'A' && x.mhs_type == 'P').Select(x => x.mhs_code).ToList();
                            listSite.AddRange(new List<string> { "01AMSCHK" });
                            foreach (string siteCode in listSite)
                            {
                                Application.DoEvents();
                                if (_enable)
                                {
                                    List<string> listRowID = dct.tmp_getptappointments.Select(y => y.appt_rowid).ToList();
                                    try
                                    {
                                        using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                                        {
                                            var appointment = ws.GetPTAppointment(siteCode, Datesearch).AsEnumerable();
                                            List<tmp_getptappointment> result = appointment
                                                                                .Where(x => !listRowID.Contains(x.Field<string>("APPT_RowId")))
                                                                                .Select(x => new tmp_getptappointment
                                                                                {
                                                                                    as_date = x.Field<DateTime>("AS_Date"),
                                                                                    papmi_no = x.Field<string>("PAPMI_No"),
                                                                                    papmi_deceased = x.Field<string>("PAPMI_Deceased"),
                                                                                    paper_name = x.Field<string>("PAPER_NAME"),
                                                                                    paper_name2 = x.Field<string>("PAPER_NAME2"),
                                                                                    paper_name3 = x.Field<string>("PAPER_NAME3"),
                                                                                    ttl_desc = x.Field<string>("TTL_Desc"),
                                                                                    as_sessstarttime = x.Field<TimeSpan>("AS_SessStartTime").ToString(),
                                                                                    ctloc_desc = x.Field<string>("CTLOC_Desc"),
                                                                                    ctloc_code = x.Field<string>("CTLOC_Code") == "01AMSCHK" ? "01AMS" : x.Field<string>("CTLOC_Code"),
                                                                                    appt_rowid = x.Field<string>("APPT_RowId"),
                                                                                    paadm_visitstatus = x.Field<string>("PAADM_VisitStatus"),
                                                                                    appt_status = x.Field<string>("APPT_Status"),
                                                                                    fullname = x.Field<string>("Fullname")
                                                                                }).ToList();
                                            dct.tmp_getptappointments.InsertAllOnSubmit(result);
                                            List<string> listOldRowIDToday = appointment.Select(x => x.Field<string>("APPT_RowId"))
                                                                             .Where(x => listRowID.Contains(x)).ToList();
                                            dct.tmp_getptappointments.Where(x => listOldRowIDToday.Contains(x.appt_rowid)).ToList()
                                                                     .ForEach(x => x.appt_status =
                                                                         appointment.Where(y => y.Field<string>("APPT_RowId") == x.appt_rowid).Select(y => y.Field<string>("APPT_Status")).FirstOrDefault());
                                            dct.SubmitChanges();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        lberror.Text += Environment.NewLine + ex.Message;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lberror.Text += Environment.NewLine + ex.Message;
                    }
                    Timer timer = new Timer();
                    timer.Enabled = true;
                    timer.Interval = 1000;
                    timer.Tick += new EventHandler(timer_Tick);
                    timer.Start();
                }
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            timer.Stop();
            timer.Dispose();
            if (!this.IsDisposed)
            {
                this.CheckDateAndUpdateCurrentDate();
                this.GetPTAppointment();
            }
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
    }
}
