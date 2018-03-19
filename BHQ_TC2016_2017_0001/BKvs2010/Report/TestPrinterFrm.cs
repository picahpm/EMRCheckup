using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Management;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace BKvs2010.Report
{
    public partial class TestPrinterFrm : Form
    {
        public TestPrinterFrm()
        {
            InitializeComponent();
        }


        private class Printer
        {
            public string dis { get; set; }
            public string val { get; set; }
        }
        private class PrinterPaperSize
        {
            public string Name { get; set; }
            public int Kind { get; set; }
        }

        private void TestPrinterFrm_Load(object sender, EventArgs e)
        {
            try
            {
                string pathReport = @"\\10.88.26.55\Report\RG\Wristband.rpt";
                ReportDocument doc = new ReportDocument();
                doc.Load(pathReport);
                doc.SetParameterValue("@tpr_id", 154524);
                doc.SetParameterValue("@print_user", "TEST01");
                doc.SetParameterValue("@mrt_code", "RG120");
                SetDBLogonForReport(doc);
                crystalReportViewer1.ReportSource = doc;

                var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");

                List<Printer> pt = new List<Printer>();
                foreach (var printer in printerQuery.Get())
                {
                    pt.Add(new Printer { dis = printer.GetPropertyValue("Name").ToString(), val = printer.GetPropertyValue("Name").ToString() });
                }
                comboAutoDropDownWidth1.DataSource = pt;
                comboAutoDropDownWidth1.DisplayMember = "dis";
                comboAutoDropDownWidth1.ValueMember = "val";

                if (System.IO.File.Exists(@"D:\EmrCheckup\PrinterSticker.xml"))
                {
                    System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Load(@"D:\EmrCheckup\PrinterSticker.xml");
                    var wb = xml.Element("PrinterCls").Elements("Report").Where(x => x.Attribute("id").Value == "Wristband").FirstOrDefault();
                    if (wb != null)
                    {
                        string prn = wb.Element("Printer").Value;
                        string pan = wb.Element("Paper").Value;

                        comboAutoDropDownWidth1.SelectedValue = prn;

                        comboAutoDropDownWidth1_SelectionChangeCommitted(null, null);

                        for (int i = 0; i < comboAutoDropDownWidth2.Items.Count; i++)
                        {
                            if (((PrinterPaperSize)comboAutoDropDownWidth2.Items[i]).Name == pan)
                            {
                                comboAutoDropDownWidth2.SelectedValue = ((PrinterPaperSize)comboAutoDropDownWidth2.Items[i]).Kind;
                            }
                        }
                        //for (int i = 0; i < comboAutoDropDownWidth1.Items.Count; i++)
                        //{

                        //    if (comboAutoDropDownWidth1.Items[i].ToString() == wb.Element("Printer").Value)
                        //    {
                        //        comboAutoDropDownWidth1.SelectedValue = wb.Element("Printer").Value;

                        //        for (int j = 0; j < comboAutoDropDownWidth2.Items.Count; j++)
                        //        {
                        //            if (((PrinterPaperSize)comboAutoDropDownWidth2.Items[j]).Name == wb.Element("Paper").Value)
                        //            {
                        //                comboAutoDropDownWidth2.SelectedIndex = j;
                        //                break;
                        //            }
                        //        }
                        //        break;
                        //    }
                        //}
                    }
                }
            }
            catch
            {

            }
        }

        private void comboAutoDropDownWidth1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<PrinterPaperSize> ps = new List<PrinterPaperSize>();

            var a = new System.Drawing.Printing.PrintDocument();
            a.PrinterSettings.PrinterName = comboAutoDropDownWidth1.Text;
            foreach (System.Drawing.Printing.PaperSize b in a.PrinterSettings.PaperSizes)
            {
                ps.Add(new PrinterPaperSize { Kind = (int)b.Kind, Name = b.PaperName });
            }
            comboAutoDropDownWidth2.DataSource = ps;
            comboAutoDropDownWidth2.DisplayMember = "Name";
            comboAutoDropDownWidth2.ValueMember = "Kind";
        }

        private void SetDBLogonForReport(ReportDocument reportDocument)
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = "10.88.17.43";
            connectionInfo.DatabaseName = "PathWay";
            connectionInfo.UserID = "sa";
            connectionInfo.Password = "sa1234";
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportDocument doc = (ReportDocument)crystalReportViewer1.ReportSource;

            doc.PrintOptions.PrinterName = comboAutoDropDownWidth1.Text;
            doc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
            doc.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
            doc.PrintOptions.PaperSize = (PaperSize)((PrinterPaperSize)comboAutoDropDownWidth2.SelectedItem).Kind;
            doc.PrintToPrinter(1, false, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.Directory.Exists(@"D:\"))
                {
                    Program.MessageError("Sticker", "Drive D:", "Not found.", false);
                }
                else
                {
                    if (!System.IO.Directory.Exists(@"D:\EmrCheckup"))
                    {
                        System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(@"D:\EmrCheckup");
                        di.Attributes = System.IO.FileAttributes.Directory | System.IO.FileAttributes.Hidden;
                    }
                    if (!System.IO.File.Exists(@"D:\EmrCheckup\PrinterSticker.xml"))
                    {
                        new XDocument(
                            new XElement("PrinterCls",
                                new XElement("Report",
                                    new XAttribute("id", "Wristband"),
                                    new XElement("Printer", comboAutoDropDownWidth1.Text),
                                    new XElement("Paper", ((PrinterPaperSize)comboAutoDropDownWidth2.SelectedItem).Name)
                                )
                            )
                        ).Save(@"D:\EmrCheckup\PrinterSticker.xml");
                    }
                    else
                    {
                        System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Load(@"D:\EmrCheckup\PrinterSticker.xml");
                        var wb = xml.Element("PrinterCls").Elements("Report").Where(x => x.Attribute("id").Value == "Wristband").FirstOrDefault();

                        if (wb == null)
                        {
                            xml.Element("PrinterCls").Add(
                                new XElement("Report",
                                    new XAttribute("id", "Wristband"),
                                    new XElement("Printer", comboAutoDropDownWidth1.Text),
                                    new XElement("Paper", ((PrinterPaperSize)comboAutoDropDownWidth2.SelectedItem).Name)
                                )
                            );
                        }
                        else
                        {
                            wb.Element("Printer").Value = comboAutoDropDownWidth1.Text;
                            wb.Element("Paper").Value = ((PrinterPaperSize)comboAutoDropDownWidth2.SelectedItem).Name;
                        }
                        xml.Save(@"D:\EmrCheckup\PrinterSticker.xml");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
