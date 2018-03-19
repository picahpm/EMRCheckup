using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBCheckup;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Data;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Deployment.Application;
using System.Net;
using setLogUser;

namespace BKvs2010
{
    public static partial class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            if (new PrePareData.PrepareDataCls().Prepared())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                AlertOutDepartment.prepairTimer();
                Application.Run(new frmMain());
            }
            else
            {
                MessageBox.Show("Connection Database Fail.", "EMR Checkup Pathway");
            }
        }
    }
}
