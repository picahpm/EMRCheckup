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

namespace BKvs2010.Report
{
    public partial class frmPatientBook : Form
    {
        public frmPatientBook()
        {
            InitializeComponent();
        }

        public void previewRpt(ReportDocument reportDoc)
        {
            this.crystalReportViewer1.ReportSource = reportDoc;
            this.ShowDialog();
        }
    }
}
