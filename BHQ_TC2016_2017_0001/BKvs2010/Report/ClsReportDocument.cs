using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;
using System.Windows.Forms;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Diagnostics;
using System.Threading;

namespace BKvs2010.Report
{
    class ClsReportDocument : IDisposable
    {
        public struct reportDoc
        {
            public ReportDocument report { get; set; }
            public string messege { get; set; }
        }

        private void SetDBLogonForReport(ReportDocument reportDocument)
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.ServerName = PrePareData.StaticDataCls.ServerDataBase;
            connectionInfo.DatabaseName = PrePareData.StaticDataCls.DataBaseName;
            connectionInfo.UserID = PrePareData.StaticDataCls.DataBaseUserName;
            connectionInfo.Password = PrePareData.StaticDataCls.DataBasePassword;
            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
            reportDocument.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
        }

        private string getFilePathReport(string rptCode)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    string pathFile = null;
                    string fileName = null;
                    var result = dbc.mst_reports.Where(x => x.mrt_code == rptCode && x.mrt_status == 'A'
                                         && DateTime.Today.Date >= x.mrt_effective_date.Value.Date
                                         && DateTime.Today.Date <= (x.mrt_expire_date == null ? DateTime.Today.Date : x.mrt_expire_date.Value.Date))
                    .Select(x => new
                    {
                        pathFile = x.mrt_path_file,
                        fileName = x.mrt_file_name
                    }).FirstOrDefault();
                    if (result != null)
                    {
                        pathFile = result.pathFile;
                        fileName = result.fileName;
                    }
                    if (pathFile == null || fileName == null || pathFile == "" || fileName == "")
                    {
                        return "";
                    }
                    else
                    {
                        return @"\\" + PrePareData.StaticDataCls.ServerReport + @"\" + pathFile + @"\" + fileName;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public reportDoc rptDoc(string rptCode)
        {
            string path = getFilePathReport(rptCode);
            if (path != null)
            {
                if (path != "")
                {
                    if (File.Exists(path))
                    {
                        try
                        {
                            ReportDocument rpt = new ReportDocument();
                            rpt.Load(path);
                            SetDBLogonForReport(rpt);
                            rpt.Refresh();
                            rpt.VerifyDatabase();
                            return new reportDoc
                            {
                                report = rpt,
                                messege = "Success."
                            };
                        }
                        catch
                        {
                            return new reportDoc
                            {
                                report = null,
                                messege = rptCode + ": Error on Function in Class ClsReportDocument(LoadDocument or SetDBLogonForReport or VerifyDatabase)"
                            };
                        }
                    }
                    else
                    {
                        return new reportDoc
                        {
                            report = null,
                            messege = rptCode + ": Not Found Report File on Server."
                        };
                    }
                }
                else
                {
                    return new reportDoc
                    {
                        report = null,
                        messege = rptCode + ": Not Found Path File or Report Name on Database."
                    };
                }
            }
            else
            {
                return new reportDoc
                {
                    report = null,
                    messege = rptCode + ": Connect Database Error."
                };
            }
        }

        public const string aviaThaiEyeRptCode = "EN102";
        public const string aviaFAAEyeRptCode = "EN105";
        public const string aviaCanEyeRptCode = "EN104";
        public const string aviaAusEyeRptCode = "EN103";

        public List<string> get_rptCodeEyeAviation(int tpr_id)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                List<string> rptCode = new List<string>();
                List<string> avia_type = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault().trn_patient_aviations
                                         .Select(x => x.mst_avia_cat_type.mst_aviation_category.mac_avia_type).Distinct().ToList();
                if (avia_type.Any(x => Program.aviaTypeThaiForEyes.Contains(x)))
                {
                    rptCode.Add(aviaThaiEyeRptCode);
                }
                if (avia_type.Any(x => Program.aviaTypeFAAForEyes.Contains(x)))
                {
                    rptCode.Add(aviaFAAEyeRptCode);
                }
                if (avia_type.Any(x => Program.aviaTypeCanForEyes.Contains(x)))
                {
                    rptCode.Add(aviaCanEyeRptCode);
                }
                if (avia_type.Any(x => Program.aviaTypeAusForEyes.Contains(x)))
                {
                    rptCode.Add(aviaAusEyeRptCode);
                }
                if (rptCode.Count == 0) rptCode = null;
                return rptCode;
            }
        }

        private const string rptCodePepENG = "PT102";
        private const string rptCodePepJMS = "PT103";
        private const string rptCodePepAMS = "PT104";

        private string getSite(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                string site = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.mst_hpc_site.mhs_code).FirstOrDefault();
                return site;
            }
        }

        public string getRptCodeBySite(int tpr_id)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                string site = getSite(tpr_id);
                if (site == "01JMSCK")
                {
                    return rptCodePepJMS;
                }
                else if (site == "01AMS")
                {
                    return rptCodePepAMS;
                }
                else
                {
                    return rptCodePepENG;
                }
            }
        }

        private const string rptCodeEyeAircrew = "EN106";
        private const string rptCodeEye = "EN101";

        public string getRptCodeEye(int tpr_id)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                trn_patient_regi tpr = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                if (tpr != null)
                {
                    if (Program.patientTypeForAircrew.Contains(tpr.tpr_patient_type.ToString()))
                    {
                        return "EN106";
                    }
                    else
                    {
                        return "EN101";
                    }
                }
                return null;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
