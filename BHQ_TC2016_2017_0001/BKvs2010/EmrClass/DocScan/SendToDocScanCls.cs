using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using DBCheckup;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace BKvs2010.EmrClass.DocScan
{
    public partial class SendToDocScanCls
    {
        public SendToDocScanCls()
        {

        }

        public string Send(int tpr_id, string mrt_code, string mhs_code, string username)
        {
            try
            {
                using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                {
                    return ws.SendToDocScan(tpr_id, mrt_code, mhs_code, username);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
