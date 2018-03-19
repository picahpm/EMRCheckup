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
using DBCheckup;

namespace BKvs2010.Report
{
    public partial class frmReport : Form
    {
        public List<trn_book_print> listObjLog { get; set; }

        private List<ReportDocument> listRpt;
        private Boolean isListband;
        //private const string printBarcodeName = "ZDesigner LP 2844-Z";
        private const string printBarcodeName = "CHK_BAR";

        private int qtyBarcode
        {
            get
            {
                if (Program.CurrentRegis.tpr_patient_type.ToString() == "3")
                {
                    return 15;
                }
                else
                {
                    return 25; // Change sumit 06/12/2013
                    //return 20;
                }
            }
        }

        public frmReport()
        {
            InitializeComponent();
        }




        public void printWristband(ReportDocument _rpt)
        {
            isListband = true;
            listRpt = new List<ReportDocument>();
            listRpt.Add(_rpt);
            this.crystalReportViewer1.ReportSource = listRpt[0];
            disableButton();
            this.ShowDialog();
        }

        public void previewRpt(ReportDocument _listRpt)
        {
            isListband = false;
            listRpt = new List<ReportDocument>();
            listRpt.Add(_listRpt);
            this.crystalReportViewer1.ReportSource = listRpt[0];
            disableButton();
            this.ShowDialog();
            this.WindowState = FormWindowState.Maximized;
        }

        public void previewRpt(List<ReportDocument> _listRpt)
        {
            isListband = false;
            listRpt = new List<ReportDocument>();
            listRpt = _listRpt;
            this.crystalReportViewer1.ReportSource = listRpt[0];
            disableButton();
            this.ShowDialog();
            this.WindowState = FormWindowState.Maximized;
        }

        private void frmReport_Resize(object sender, EventArgs e)
        {
            button5.Location = new Point(this.ClientSize.Width - button5.Width - 4, this.ClientSize.Height - button5.Height - 4);
            button4.Location = new Point(button5.Left - button4.Width - 4, button5.Top);
            button3.Location = new Point(button4.Left - button4.Width - 4, button5.Top);
            button2.Location = new Point(button3.Left - button4.Width - 4, button5.Top);
            button1.Location = new Point(button2.Left - button4.Width - 4, button5.Top);
            crystalReportViewer1.Size = new Size(this.ClientSize.Width, button5.Top - 4);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportDocument rptDoc = (ReportDocument)crystalReportViewer1.ReportSource;
            if (!rptDoc.Equals(listRpt.First()))
            {
                crystalReportViewer1.ReportSource = listRpt.First();
            }
            disableButton();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportDocument rptDoc = (ReportDocument)crystalReportViewer1.ReportSource;
            if (!rptDoc.Equals(listRpt.First()))
            {
                if (listObjLog != null)
                {
                    int currentPage = crystalReportViewer1.GetCurrentPageNumber();
                    crystalReportViewer1.ShowLastPage();
                    int lastPage = crystalReportViewer1.GetCurrentPageNumber();
                    crystalReportViewer1.ShowNthPage(currentPage);

                    if ((lastPage > 1) && (currentPage != 1))
                    {
                        crystalReportViewer1.ShowPreviousPage();
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = listRpt[listRpt.IndexOf(rptDoc) - 1];
                    }
                }
                else
                {
                    crystalReportViewer1.ReportSource = listRpt[listRpt.IndexOf(rptDoc) - 1];
                }
            }
            disableButton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ReportDocument rptDoc = (ReportDocument)crystalReportViewer1.ReportSource;
            if (!rptDoc.Equals(listRpt.Last()))
            {
                if (listObjLog != null)
                {
                    int currentPage = crystalReportViewer1.GetCurrentPageNumber();
                    crystalReportViewer1.ShowLastPage();
                    int lastPage = crystalReportViewer1.GetCurrentPageNumber();
                    crystalReportViewer1.ShowNthPage(currentPage);

                    if ((lastPage > 1) && (lastPage != currentPage))
                    {
                        crystalReportViewer1.ShowNextPage();
                    }
                    else
                    {
                        crystalReportViewer1.ReportSource = listRpt[listRpt.IndexOf(rptDoc) + 1];
                    }
                }
                else
                {
                    crystalReportViewer1.ReportSource = listRpt[listRpt.IndexOf(rptDoc) + 1];
                }
            }
            disableButton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportDocument rptDoc = (ReportDocument)crystalReportViewer1.ReportSource;
            if (!rptDoc.Equals(listRpt.Last()))
            {
                crystalReportViewer1.ReportSource = listRpt.Last();
            }
            disableButton();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (isListband == false)
            {
                foreach (ReportDocument rptDoc in listRpt)
                {
                    try
                    {
                        rptDoc.PrintToPrinter(1, false, 0, 0);
                    }
                    catch (Exception)
                    {

                    }
                }
                PrintBookHistory();
            }
            else
            {
                string printer = "";
                frmSelectCopy frm = new frmSelectCopy();
                int copyNum = frm.loadSelectCopyFrm(qtyBarcode);
                if (copyNum > 0)
                {
                    foreach (ReportDocument rptDoc in listRpt)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(frm.PrinterName))
                            {
                                printer = printBarcodeName;
                            }
                            else
                            {
                                printer = frm.PrinterName;
                            }
                            if (Report.CheckPrinterClass.hasPrinter(printer))
                            {
                                rptDoc.PrintOptions.PrinterName = printer;
                                rptDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                                var pt = new System.Drawing.Printing.PrintDocument();
                                pt.PrinterSettings.PrinterName = printer;
                                //foreach (System.Drawing.Printing.PaperSize pa in pt.PrinterSettings.PaperSizes)
                                //{
                                //    if (pa.PaperName == papername)
                                //    {
                                //        rptDoc.PrintOptions.PaperSize = (PaperSize)((int)pa.Kind);
                                //        break;
                                //    }
                                //}
                                rptDoc.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                                rptDoc.PrintToPrinter(copyNum, false, 0, 0);
                            }
                            else
                            {
                                MessageBox.Show("Printer is not found.");
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }

        private void PrintBookHistory()
        {
            try
            {
                if (listObjLog != null)
                {
                    InhCheckupDataContext dbcBookLog = new InhCheckupDataContext();
                    dbcBookLog.Connection.Open();
                    dbcBookLog.trn_book_prints.InsertAllOnSubmit(listObjLog);
                    dbcBookLog.SubmitChanges();
                    dbcBookLog.Connection.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        private void disableButton()
        {
            ReportDocument rptDoc = (ReportDocument)crystalReportViewer1.ReportSource;
            if (rptDoc.Equals(listRpt.First()))
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }

            if (rptDoc.Equals(listRpt.Last()))
            {
                button3.Enabled = false;
                button4.Enabled = false;
            }

            if (!rptDoc.Equals(listRpt.First()))
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }

            if (!rptDoc.Equals(listRpt.Last()))
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void frmReport_Activated_for_Lisband(object sender, EventArgs e)
        {
            this.Activated -= new System.EventHandler(this.frmReport_Activated_for_Lisband);
            chkListband();
        }

        private void chkListband()
        {
            if (isListband == true)
            {
                string printer = "";
                frmSelectCopy frm = new frmSelectCopy();
                int copyNum = frm.loadSelectCopyFrm(qtyBarcode);
                if (copyNum > 0)
                {
                    foreach (ReportDocument rptDoc in listRpt)
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(frm.PrinterName))
                            {
                                printer = printBarcodeName;
                            }
                            else
                            {
                                printer = frm.PrinterName;
                            }
                            if (Report.CheckPrinterClass.hasPrinter(printer))
                            {
                                rptDoc.PrintOptions.PrinterName = printer;
                                rptDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                                var pt = new System.Drawing.Printing.PrintDocument();
                                pt.PrinterSettings.PrinterName = printer;
                                //foreach (System.Drawing.Printing.PaperSize pa in pt.PrinterSettings.PaperSizes)
                                //{
                                //    if (pa.PaperName == papername)
                                //    {
                                //        rptDoc.PrintOptions.PaperSize = (PaperSize)((int)pa.Kind);
                                //        break;
                                //    }
                                //}
                                rptDoc.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                                rptDoc.PrintToPrinter(copyNum, false, 0, 0);
                            }
                            else
                            {
                                MessageBox.Show("Printer is not found.");
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
        }

        public int printChkPage(ReportDocument _rpt, int? tprID, int StartPage)
        {
            listRpt = new List<ReportDocument>();
            _rpt.SetParameterValue("@tpr_id", tprID);
            _rpt.SetParameterValue("@StartPage", StartPage);
            listRpt.Add(_rpt);
            crystalReportViewer1.ReportSource = listRpt[0];
            crystalReportViewer1.ShowLastPage();
            int endPage = crystalReportViewer1.GetCurrentPageNumber();
            return endPage;
        }

        private void setDisplayBar()
        {
            if (listObjLog != null)
            {
                crystalReportViewer1.DisplayToolbar = false;
                crystalReportViewer1.DisplayStatusBar = false;
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            setDisplayBar();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            this.Activated += new System.EventHandler(this.frmReport_Activated_for_Lisband);
        }
    }
}