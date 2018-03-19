using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CheckupBO
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
            Aboutus frmab = new Aboutus();
            frmab.ShowDialog();

            Application.Run(new frmMain());
            /*Application.Run(new Form1());*/
        }
    }
}
