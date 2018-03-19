using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBCheckup;
using System.Configuration;
using ConfigEMRCheckUp;

namespace CallDataTakeCareArrived
{
    public static partial class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
