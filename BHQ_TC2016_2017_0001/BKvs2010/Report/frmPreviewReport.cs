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
    public partial class frmPreviewReport : Form
    {
        public class _propertyReport
        {
            public int tpr_id { get; set; }
            public List<string> reportCode { get; set; }
            private bool _showSelectCopy = false;
            public bool showSelectCopy
            {
                get { return _showSelectCopy; }
                set { _showSelectCopy = value; }
            }
            private int? _defaultCopy = null;
            public int? defaultCopy
            {
                get { return _defaultCopy; }
                set { _defaultCopy = value; }
            }
            private bool _printOutBarcodePrinter = false;
            public bool printOutBarcodePrinter
            {
                get { return _printOutBarcodePrinter; }
                set { _printOutBarcodePrinter = value; }
            }
        }

        private const string printBarcodeName = "CHK_BAR";
        private class struReport
        {
            public string reportNameTH { get; set; }
            public string reportNameEng { get; set; }
            public string reportCode { get; set; }
            public ReportDocument reportDoc { get; set; }
        }
        private List<struReport> report;
        private struReport _currentReport = new struReport();
        private struReport currentReport
        {
            get
            {
                return _currentReport;
            }
            set
            {
                _currentReport = value;
                int currentIndex = report.IndexOf(_currentReport);
                int lastIndex = report.IndexOf(report.Last());
                this.crystalReportViewer1.ReportSource = value.reportDoc;
                this.lblReportName.Text = value.reportNameTH;

                btnMoveFirst.Enabled = !(currentIndex == 0);
                btnMoveBack.Enabled = !(currentIndex == 0);
                btnMoveNext.Enabled = !(currentIndex == lastIndex);
                btnMoveLast.Enabled = !(currentIndex == lastIndex);
            }
        }

        private _propertyReport _propReport = new _propertyReport();
        public _propertyReport propertyReport
        {
            get { return _propReport; }
            set { _propReport = value; }
        }

        public frmPreviewReport()
        {
            InitializeComponent();
        }

        public frmPreviewReport(int id, string reportCode, string site)
        {
            InitializeComponent();
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                ClsReportDocument.reportDoc rptDoc = new ClsReportDocument.reportDoc();
                ClsReportDocument cls = new ClsReportDocument();
                ReportDocument rpt = new ReportDocument();
                if (reportCode == "AV103")
                {
                    rptDoc = cls.rptDoc("AV103");
                    rpt = rptDoc.report;
                    rpt.SetParameterValue("@tram_id", id);
                }
                else if (reportCode == "AV104")
                {
                    rptDoc = cls.rptDoc("AV104");
                    rpt = rptDoc.report;
                    rpt.SetParameterValue("@trvh_id", id);
                    rpt.SetParameterValue("@dept", site);
                }
                struReport stRpt = cdc.mst_reports.Where(x => x.mrt_code == reportCode && x.mrt_status == 'A'
                                       && DateTime.Today.Date >= x.mrt_effective_date.Value.Date
                                       && DateTime.Today.Date <= (x.mrt_expire_date == null ? DateTime.Today.Date : x.mrt_expire_date.Value.Date))
                                       .Select(x => new struReport()
                                       {
                                           reportNameTH = x.mrt_tname,
                                           reportNameEng = x.mrt_ename,
                                           reportCode = reportCode,
                                           reportDoc = rpt
                                       }).FirstOrDefault();
                if (report == null) report = new List<struReport>();
                report.Add(stRpt);
                if (report != null)
                {
                    currentReport = report.First();
                    crystalReportViewer1.ReportSource = _currentReport.reportDoc;
                }
                cls.Dispose();
            }
            this.Top = Screen.PrimaryScreen.WorkingArea.Top;
            this.Left = Screen.PrimaryScreen.WorkingArea.Left;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            _propertyReport prop = new _propertyReport()
            {
                tpr_id = 0,
                showSelectCopy = false,
                defaultCopy = 0,
                printOutBarcodePrinter = false
            };
            this.propertyReport = prop;
            this.ShowDialog();
            
        }

        public frmPreviewReport(int tpr_id, string queueNo)
        {
            InitializeComponent();
            ClsReportDocument.reportDoc rptDoc = new ClsReportDocument.reportDoc();
            ClsReportDocument cls = new ClsReportDocument();
            rptDoc = cls.rptDoc("RG000");
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                ReportDocument rpt = rptDoc.report;
                rpt.SetParameterValue("@tpr_id", tpr_id);
                rpt.SetParameterValue("@print_user", getNameUser());
                rpt.SetParameterValue("@mrt_code", "RG000");
                //setParameter(ref rpt, _propReport.reportCode.First());
                rpt.SetParameterValue("@Queue", queueNo);
                struReport stRpt = cdc.mst_reports.Where(x => x.mrt_code == "RG000" && x.mrt_status == 'A'
                                   && DateTime.Today.Date >= x.mrt_effective_date.Value.Date
                                   && DateTime.Today.Date <= (x.mrt_expire_date == null ? DateTime.Today.Date : x.mrt_expire_date.Value.Date))
                                   .Select(x => new struReport()
                                   {
                                       reportNameTH = x.mrt_tname,
                                       reportNameEng = x.mrt_ename,
                                       reportCode = "RG000",
                                       reportDoc = rpt
                                   }).FirstOrDefault();
                if (report == null) report = new List<struReport>();
                report.Add(stRpt);
                if (report != null)
                {
                    currentReport = report.First();
                    crystalReportViewer1.ReportSource = _currentReport.reportDoc;
                }
            } 
            this.Top = Screen.PrimaryScreen.WorkingArea.Top;
            this.Left = Screen.PrimaryScreen.WorkingArea.Left;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            _propertyReport prop = new _propertyReport()
            {
                tpr_id = tpr_id,
                showSelectCopy = true,
                defaultCopy = 1,
                printOutBarcodePrinter = true
            };
            this.propertyReport = prop;
            this.Activated += new EventHandler(activateForShowCopy);
            this.ShowDialog();
            //return rpt;
            //using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            //{
            //    string path = cdc.mst_reports.Where(x => x.mrt_code == "").Select(x => x.mrt_path_file + x.mrt_file_name;
            //    rpt.Load("");
            //    rpt.SetParameterValue("", "");
            //    rpt.SetParameterValue("", "");
            //    rpt.SetParameterValue("", "");
            //    rpt.SetParameterValue("@Queue", queueNo);
            //}

        }

        public frmPreviewReport(int tpr_id, List<string> reportCode, bool showSelectCopy, int? defaultCopy, bool printOutBarcodePrinter)
        {
            InitializeComponent();
            _propertyReport prop = new _propertyReport()
            {
                tpr_id = tpr_id,
                reportCode = reportCode,
                showSelectCopy = showSelectCopy,
                defaultCopy = defaultCopy,
                printOutBarcodePrinter = printOutBarcodePrinter
            };
            this.propertyReport = prop;
            //previewReport();
        }

        public frmPreviewReport(int tpr_id, List<string> reportCode, bool showSelectCopy, int? defaultCopy)
        {
            InitializeComponent();
            _propertyReport prop = new _propertyReport()
            {
                tpr_id = tpr_id,
                reportCode = reportCode,
                showSelectCopy = showSelectCopy,
                defaultCopy = defaultCopy
            };
            this.propertyReport = prop;
            //previewReport();
        }

        public frmPreviewReport(int tpr_id, List<string> reportCode)
        {
            InitializeComponent();
            _propertyReport prop = new _propertyReport()
            {
                tpr_id = tpr_id,
                reportCode = reportCode
            };
            this.propertyReport = prop;
            //previewReport();
        }

        public frmPreviewReport(int tpr_id, List<string> reportCode, bool printOutBarcodePrinter)
        {
            InitializeComponent();
            _propertyReport prop = new _propertyReport()
            {
                tpr_id = tpr_id,
                reportCode = reportCode,
                printOutBarcodePrinter = printOutBarcodePrinter
            };
            this.propertyReport = prop;
            //previewReport();
        }

        private int? qtyBarcode
        {
            get
            {
                try
                {
                    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    {
                        char? type = dbc.trn_patient_regis.Where(x => x.tpr_id == _propReport.tpr_id).Select(x => x.tpr_patient_type).FirstOrDefault();
                        if (type == '3')
                        {
                            return 15;
                        }
                        else
                        {
                            return 25;
                        }
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        private void activateForShowCopy(object sender, EventArgs e)
        {
            this.Activated -= new EventHandler(activateForShowCopy);
            if (_propReport.defaultCopy == null)
            {
                _propReport.defaultCopy = qtyBarcode;
            }
            if (_propReport.defaultCopy == null)
            {
                // "Connect Database Error.";
            }
            else
            {
                frmSelectCopy frm = new frmSelectCopy();
                int copy = frm.loadSelectCopyFrm(Convert.ToInt32(_propReport.defaultCopy));
                if (copy > 0)
                {
                    string message = printReport(copy, frm.PrinterName, frm.PaperName);
                }
            }
        }

        public void previewReport()
        {
            if (_propReport.showSelectCopy == true) this.Activated += new EventHandler(activateForShowCopy);
            string message = setReport();
            this.Top = Screen.PrimaryScreen.WorkingArea.Top;
            this.Left = Screen.PrimaryScreen.WorkingArea.Left;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.ShowDialog();
        }

        public ReportDocument getReportDoc()
        {
            ClsReportDocument.reportDoc rptDoc = new ClsReportDocument.reportDoc();
            ClsReportDocument cls = new ClsReportDocument();
            rptDoc = cls.rptDoc(_propReport.reportCode.First());
            ReportDocument rpt = rptDoc.report;
            setParameter(ref rpt, _propReport.reportCode.First());
            return rpt;
        }

        public void printReport()
        {
            string message = setReport();
            printReport(1);
        }

        private string setReport()
        {
            List<string> message = null;
            ClsReportDocument cls = new ClsReportDocument();
            foreach (string code in _propReport.reportCode)
            {
                ClsReportDocument.reportDoc rptDoc = new ClsReportDocument.reportDoc();
                rptDoc = cls.rptDoc(code);
                if (rptDoc.report != null)
                {
                    if (report == null) report = new List<struReport>();
                    ReportDocument rpt = rptDoc.report;
                    setParameter(ref rpt, code);
                    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                    {
                        struReport stRpt = dbc.mst_reports.Where(x => x.mrt_code == code && x.mrt_status == 'A'
                                           && DateTime.Today.Date >= x.mrt_effective_date.Value.Date
                                           && DateTime.Today.Date <= (x.mrt_expire_date == null ? DateTime.Today.Date : x.mrt_expire_date.Value.Date))
                        .Select(x => new struReport()
                        {
                            reportNameTH = x.mrt_tname,
                            reportNameEng = x.mrt_ename,
                            reportCode = code,
                            reportDoc = rpt
                        }).FirstOrDefault();
                        if (report == null) report = new List<struReport>();
                        report.Add(stRpt);
                    }
                }
                else
                {
                    if (message == null) message = new List<string>();
                    message.Add(rptDoc.messege);
                }
            }
            if (report != null)
            {
                currentReport = report.First();
                crystalReportViewer1.ReportSource = _currentReport.reportDoc;
            }
            cls.Dispose();
            if (message != null)
            {
                return string.Join("/r/n", message);
            }
            else
            {
                return null;
            }
        }

        private string setParameter(ref ReportDocument rptDoc, string rptCode)
        {
            try
            {
                if (rptCode == "BK302")
                {
                    //rptDoc.SetParameterValue("@lang", "T");
                }
                else if (rptCode == "BK303")
                {
                    //rptDoc.SetParameterValue("@lang", "E");
                }
                rptDoc.SetParameterValue("@tpr_id", _propReport.tpr_id);
                rptDoc.SetParameterValue("@print_user", getNameUser());
                rptDoc.SetParameterValue("@mrt_code", rptCode);

                //check doc scan : Pang 10/7/2015
                for (int i = 0; i < rptDoc.ParameterFields.Count; i++)
                {
                    if (rptDoc.ParameterFields[i].Name == "@isDocScan")
                    {
                        rptDoc.SetParameterValue("@isDocScan", false);                       
                       break;
                    }
                }

                return "Success.";
            }
            catch
            {
                rptDoc = null;
                return rptCode + ": Set Parameter Error.";
            }
        }
        
        private string getNameUser()
        {
            string name = "";
            name = (string.IsNullOrEmpty(Program.CurrentUser.mut_t_pname) ? "" : Program.CurrentUser.mut_t_pname) +
                   (string.IsNullOrEmpty(Program.CurrentUser.mut_t_fname) ? "" : Program.CurrentUser.mut_t_fname) + " " +
                   (string.IsNullOrEmpty(Program.CurrentUser.mut_t_lname) ? "" : Program.CurrentUser.mut_t_lname);
            return name;
        }

        private string printReport(int qty)
        {
            List<string> message = null;
            foreach (struReport rpt in report)
            {
                try
                {
                    if (_propReport.printOutBarcodePrinter == true)
                    {
                        if (Report.CheckPrinterClass.hasPrinter(printBarcodeName))
                        {
                            rpt.reportDoc.PrintOptions.PrinterName = printBarcodeName;
                            rpt.reportDoc.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                            rpt.reportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                            rpt.reportDoc.PrintToPrinter(qty, false, 0, 0);
                        }
                        else
                        {
                            MessageBox.Show("Printer not found.");
                        }
                    }
                    else
                    {
                        rpt.reportDoc.PrintToPrinter(qty, false, 0, 0);
                    }
                }
                catch
                {
                    if (message == null) message = new List<string>();
                    message.Add(rpt.reportCode + ": Error Function PrintToPrinter.");
                }
            }
            if (message == null)
            {
                return "Seccess";
            }
            else
            {
                return string.Join("/r/n", message);
            }
        }
        private string printReport(int qty, string printername, string papername)
        {
            string printer = "";
            List<string> message = null;
            foreach (struReport rpt in report)
            {
                try
                {
                    if (_propReport.printOutBarcodePrinter == true)
                    {
                        if (string.IsNullOrEmpty(printername))
                        {
                            printer = printBarcodeName;
                        }
                        else
                        {
                            printer = printername;
                        }
                        if (Report.CheckPrinterClass.hasPrinter(printer))
                        {
                            rpt.reportDoc.PrintOptions.PrinterName = printer;
                            rpt.reportDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                            var pt = new System.Drawing.Printing.PrintDocument();
                            pt.PrinterSettings.PrinterName = printer;
                            foreach (System.Drawing.Printing.PaperSize pa in pt.PrinterSettings.PaperSizes)
                            {
                                if (pa.PaperName == papername)
                                {
                                    rpt.reportDoc.PrintOptions.PaperSize = (PaperSize)((int)pa.Kind);
                                    break;
                                }
                            }
                            rpt.reportDoc.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                            rpt.reportDoc.PrintToPrinter(qty, false, 0, 0);
                        }
                        else
                        {
                            MessageBox.Show("Printer not found.");
                        }
                    }
                    else
                    {
                        rpt.reportDoc.PrintToPrinter(qty, false, 0, 0);
                    }
                }
                catch
                {
                    if (message == null) message = new List<string>();
                    message.Add(rpt.reportCode + ": Error Function PrintToPrinter.");
                }
            }
            if (message == null)
            {
                return "Seccess";
            }
            else
            {
                return string.Join("/r/n", message);
            }
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            printReport(1);
        }

        private void btnMoveFirst_Click(object sender, EventArgs e)
        {
            if (!(report.First().Equals(currentReport)))
            {
                currentReport = report.First();
            }
        }

        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            if (!(report.Last().Equals(currentReport)))
            {
                currentReport = report.Last();
            }
        }

        private void btnMoveBack_Click(object sender, EventArgs e)
        {
            int currentIndex = report.IndexOf(currentReport);
            if (currentIndex > 0)
            {
                currentReport = report[currentIndex - 1];
            }
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            int currentIndex = report.IndexOf(currentReport);
            if (currentIndex < report.IndexOf(report.Last()))
            {
                currentReport = report[currentIndex + 1];
            }
        }

        private void frmPreviewReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            //foreach (ReportDocument rpt in report.Select(x => x.reportDoc))
            //{
            //    rpt.Dispose();
            //}
            //report = null;
            //_currentReport.reportDoc.Dispose();
            //_currentReport = null;
        }
    }
}
