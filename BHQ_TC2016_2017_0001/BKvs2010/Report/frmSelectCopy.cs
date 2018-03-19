using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace BKvs2010.Report
{
    public partial class frmSelectCopy : Form
    {
        public frmSelectCopy()
        {
            InitializeComponent();
        }
        public int copyValue = 0;
        private void frmSelectCopy_Load(object sender, EventArgs e)
        {

        }

        public string PrinterName { get; private set; }
        public string PaperName { get; private set; }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.Directory.Exists(@"D:\"))
                {
                    if (!System.IO.Directory.Exists(@"D:\EmrCheckup"))
                    {
                        System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(@"D:\EmrCheckup");
                        di.Attributes = System.IO.FileAttributes.Directory | System.IO.FileAttributes.Hidden;
                    }
                    if (!System.IO.File.Exists(@"D:\EmrCheckup\PrinterSticker.xml"))
                    {
                        PrinterName = "";
                        PaperName = "";
                    }
                    else
                    {
                        System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Load(@"D:\EmrCheckup\PrinterSticker.xml");
                        var wb = xml.Element("PrinterCls").Elements("Report").Where(x => x.Attribute("id").Value == "Wristband").FirstOrDefault();
                        if (wb != null)
                        {
                            PrinterName = wb.Element("Printer").Value;
                            PaperName = wb.Element("Paper").Value;
                        }
                    }
                }
            }
            catch
            {
                PrinterName = "";
                PaperName = "";
            }
            //try
            //{
            //    if (!System.IO.Directory.Exists(@"D:\"))
            //    {
            //        Program.MessageError("Sticker", "Drive D:", "Not found.", false);
            //    }
            //    else
            //    {
            //        if (!System.IO.Directory.Exists(@"D:\EmrCheckup"))
            //        {
            //            System.IO.DirectoryInfo di = System.IO.Directory.CreateDirectory(@"D:\EmrCheckup");
            //            di.Attributes = System.IO.FileAttributes.Directory | System.IO.FileAttributes.Hidden;
            //        }
            //        if (!System.IO.File.Exists(@"D:\EmrCheckup\PrintSticker.xml"))
            //        {
            //            new System.Xml.Linq.XDocument(
            //                new System.Xml.Linq.XElement("PrinterCls",
            //                    new System.Xml.Linq.XElement("SelectedPrinter", comboBox1.SelectedItem.ToString())
            //                )
            //            ).Save(@"D:\EmrCheckup\PrintSticker.xml");
            //            SelectedPrinter = comboBox1.SelectedItem.ToString();
            //        }
            //        else
            //        {
            //            System.Xml.Linq.XDocument xml = System.Xml.Linq.XDocument.Load(@"D:\EmrCheckup\PrintSticker.xml");
            //            xml.Root.Element("SelectedPrinter").Value = comboBox1.SelectedItem.ToString();
            //            xml.Save(@"D:\EmrCheckup\PrintSticker.xml");
            //            SelectedPrinter = comboBox1.SelectedItem.ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

            copyValue = Convert.ToInt32(cbPrintCopy.SelectedValue);
            DialogResult = DialogResult.OK;
        }


        public int loadSelectCopyFrm(int numberDefault)
        {
            List<ComboboxItem> newitem = new List<ComboboxItem>();
            for (int i = 1; i < 100; i++)
            {
                newitem.Add(new ComboboxItem(i.ToString(), i.ToString()));
            }

            cbPrintCopy.DataSource = (from t1 in newitem select new { Display = t1.Text, Valuedata = t1.Value }).ToList();
            cbPrintCopy.DisplayMember = "Display";
            cbPrintCopy.ValueMember = "Valuedata";
            cbPrintCopy.SelectedValue =  numberDefault.ToString();
            this.ShowDialog();
            return copyValue;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            copyValue = 0;
            this.Close();
        }

        private void frmSelectCopy_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (TestPrinterFrm frm = new TestPrinterFrm())
            {
                frm.ShowDialog();
            }
        }
    }
}
