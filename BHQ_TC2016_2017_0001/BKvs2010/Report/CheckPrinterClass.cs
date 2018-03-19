using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;

namespace BKvs2010.Report
{
    public class CheckPrinterClass
    {
        public static Boolean hasPrinter(string printerName)
        {
            Boolean _hasPrinter = false;
            PrinterSettings ps = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                if (printer == printerName)
                {
                    _hasPrinter = true;
                    break;
                }
            }
            return _hasPrinter;
        }
    }
}
