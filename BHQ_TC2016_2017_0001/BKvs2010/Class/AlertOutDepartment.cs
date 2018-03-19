using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Windows.Forms;
namespace BKvs2010
{
    public class AlertOutDepartment 
    {
        private static Timer timeralert = new Timer();
        public static void prepairTimer()
        {
            timeralert.Tick += new EventHandler(timeralert_Tick);
        }
        private static int AlertSiteTime = 5;//เตือนก่อน ถึงเวลา xx นาที
        private static bool isCheckShow =false;

        public static void LoadTime()
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    if (Program.CurrentSite != null)
                    {
                        var objtimedelay = (from t1 in dbc.mst_config_dtls
                                            where t1.mst_config_hdr.mfh_code == "OD"
                                            && t1.mst_config_hdr.mhs_id == Program.CurrentSite.mhs_id
                                            select t1);
                        if (objtimedelay.Count() > 0)
                        {
                            AlertSiteTime = Convert.ToInt32(objtimedelay.FirstOrDefault().mfd_value);
                        }
                    }
                }
                timeralert.Interval = 1;//ทำmทุกๆ 15วินาที
                //timeralert.Tick -= new System.EventHandler(timeralert_Tick);
                //timeralert.Tick += new System.EventHandler(timeralert_Tick);

                timeralert.Start();
                isCheckShow = false;
            }
            catch (Exception ex)
            {
                Program.MessageError("AlertOutDepartment", "LoadTime", ex, false);
            }
        }
        private static void timeralert_Tick(object sender, EventArgs e)
        {
            timeralert.Stop();
            timeralert.Interval = 1000;//2 นาทีทำอีก 1ครั้ง
            //if (Program.RefreshWaiting && Program.CurrentRegis != null && !(Program.IsViewHistory==true) && isCheckShow==false)
            if (Program.RefreshWaiting && Program.CurrentRegis != null && isCheckShow == false)
            {
                TimeSpan currenttime = Program.GetServerDateTime().TimeOfDay;
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    var objoutDeparts = (from t1 in dbc.trn_out_departments
                                         where t1.tpr_id == Program.CurrentRegis.tpr_id
                                         && t1.tod_start_date.Value.TimeOfDay.Subtract(currenttime).TotalMinutes > 0
                                         && t1.tod_start_date.Value.TimeOfDay.Subtract(currenttime).TotalMinutes < AlertSiteTime
                                         orderby t1.tod_start_date
                                         select t1).ToList();
                    if (objoutDeparts.Count() > 0)
                    {
                        string alertmsg = "ผู้รับบริการ มีการตรวจนอกแผนก ";
                        foreach (trn_out_department item in objoutDeparts)
                        {
                            alertmsg += Environment.NewLine + "- ที่ " + item.tod_location + " " + item.tod_desc + " เวลา [ " + item.tod_start_date.Value.ToString("HH:mm") + " ]";
                        }
                        timeralert.Stop();
                        isCheckShow = true;
                        System.Windows.Forms.MessageBox.Show(alertmsg, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else
            {
                timeralert.Stop();
            }
            timeralert.Start();
        }

        public static void StopTime()
        {
            timeralert.Stop();
        }

    }
}
