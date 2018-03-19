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
    class ClsReport : IDisposable
    {
        #region property for type aviation for eye report
        public static List<int> typeThai = new List<int> { 1, 5 };//Update.Akkaradech <27/11/2013> <old 1, 2, 6, 7>
        public static List<int> typeAus = new List<int> { 4 };//Update.Akkaradech <27/11/2013> <old 5>
        public static List<int> typeCan = new List<int> { 3 };//Update.Akkaradech <27/11/2013> <old 4>
        public static List<int> typeFAA = new List<int> { 2 };//Update.Akkaradech <27/11/2013> <old 3>
        #endregion

        #region parameter for call report
        //public static string domainConnServer = "bdms";
        //public static string usernameConnServer = "sumit.po";
        //public static string passwordConnServer = "p@ssw0rd";

        //public static string domainConnServer = @".\";
        public static string usernameConnServer = "reportuser"; //BMC-CHKUP2-DEV\
        public static string passwordConnServer = "reportuser";
        #endregion

        #region call report
        public static void SetDBLogonForReport(ReportDocument reportDocument)
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
        }

        public static void openConnectionReport()
        {
            Thread currentTimeThread = new Thread(new ThreadStart(openConnection));
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start(); 
        }

        private static void openConnection()
        {
            clearConnection();
            createConnection();
        }

        public static ReportDocument rptDoc(string rptCode)
        {
            ReportDocument cryRpt = new ReportDocument();
            cryRpt = null;
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                if (Program.CurrentSite != null)
                {
                    var result = (from row in dbc.mst_reports
                                  where row.mrt_code == rptCode
                                  select row).FirstOrDefault();
                    if (result != null)
                    {
                        try
                        {
                            //using (Class.NetworkShareAccesser.Access(Program.serverIP, domainConnServer, usernameConnServer, passwordConnServer))
                            //using (Class.NetworkShareAccesser.Access(Program.serverIP, usernameConnServer, passwordConnServer))
                            //{
                            string pathRpt = @"\\" + PrePareData.StaticDataCls.ServerReport + @"\" + result.mrt_path_file + @"\" + result.mrt_file_name;
                            if (pathRpt != string.Empty)
                            {
                                if (File.Exists(pathRpt))
                                {
                                    cryRpt = new ReportDocument();
                                    cryRpt.Load(pathRpt);
                                    SetDBLogonForReport(cryRpt);
                                }
                                else
                                {
                                    MessageBox.Show("Connection Server Error : Path File '" + result.mrt_file_name + "(" + result.mrt_code + ")' is not found.");
                                    cryRpt = null;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Database Error : Path Name is null.");
                            }
                            //} 
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
            }
            //clearConnection();
            if (cryRpt != null)
            {
                try
                {
                    cryRpt.Refresh();
                    cryRpt.VerifyDatabase();

                }
                catch (Exception)
                {
                    
                   
                }
            }
            return cryRpt;
        }
        #endregion

        #region for clear connection execute command
        public static int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };
            
            var process = Process.Start(processInfo);
            try
            {
                process.WaitForExit(timeout);
                var exitCode = process.ExitCode;
                process.Close();
                process.Dispose();
                return exitCode;
            }
            catch (Exception)
            {
                process.Close();
                process.Dispose();
                return -1;
            }
        }

        public static void clearConnection()
        {
            //var command = "NET USE //" + Program.serverIP + "/delete *";
            var command = @"NET USE \\" + PrePareData.StaticDataCls.ServerReport + @"\Report /delete";
            //var command = @"NET USE \\" + Program.serverIP + @" /delete";
            ExecuteCommand(command, 60000);

            var commandBook = @"NET USE \\" + PrePareData.StaticDataCls.ServerReport + @"\Book /delete";
            ExecuteCommand(commandBook, 60000);

            //command = "NET USE //" + Program.serverIP +  "/defect /delete";
            //ExecuteCommand(command, 5000);
        }

        public static void createConnection()
        {
            var command = @"net use \\" + PrePareData.StaticDataCls.ServerReport + @"\Report " + passwordConnServer + @" /user:" + PrePareData.StaticDataCls.ServerReport + @"\" + usernameConnServer;
            int checkError = ExecuteCommand(command, 60000);
            if (checkError == -1) MessageBox.Show("Warning : Cannot Connect Report Server. Please Contact Administrator.");
            var commandBook = @"net use \\" + PrePareData.StaticDataCls.ServerReport + @"\Book " + passwordConnServer + @" /user:" + PrePareData.StaticDataCls.ServerReport + @"\" + usernameConnServer;
            int checkErrorBook = ExecuteCommand(commandBook, 60000);
            if (checkErrorBook == -1) MessageBox.Show("Warning : Cannot Connect Book Server. Please Contact Administrator.");
        }
        #endregion

        #region call Eye report

        private static ReportDocument getEyeMain()
        {
            string code;
            ReportDocument standRpt = new ReportDocument();
            if (!new List<int> { 4 }.Any(x => x == Program.CurrentRegis.tpr_patient_type))
            {
                code = "EN101";
                standRpt = rptDoc(code);
            }
            else
            {
                code = "EN106";
                standRpt = rptDoc(code);
            }
            if (standRpt != null)
            {
                standRpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                standRpt.SetParameterValue("@print_user", Program.CurrentUser.mut_fullname);
                standRpt.SetParameterValue("@mrt_code", code);
            }
            return standRpt;
        }

        private static ReportDocument getEyeAvia()
        {
            string code;
            ReportDocument aviaRpt = new ReportDocument();
            if (typeThai.Any(x => x == Program.CurrentRegis.mac_id))
            {
                code = "EN102";
                aviaRpt = rptDoc(code);
            }
            else if (typeAus.Any(x => x == Program.CurrentRegis.mac_id))
            {
                code = "EN103";
                aviaRpt = rptDoc(code);
            }
            else if (typeCan.Any(x => x == Program.CurrentRegis.mac_id))
            {
                code = "EN104";
                aviaRpt = rptDoc(code);
            }
            else if (typeFAA.Any(x => x == Program.CurrentRegis.mac_id))
            {
                code = "EN105";
                aviaRpt = rptDoc(code);
            }
            else
            {
                code = "";
                aviaRpt = null;
            }
            if (aviaRpt != null)
            {
                aviaRpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                aviaRpt.SetParameterValue("@print_user", Program.CurrentUser.mut_fullname);
                aviaRpt.SetParameterValue("@mrt_code", code);
            }
            return aviaRpt;
        }

        public static void previewRptEye()
        {
            List<ReportDocument> listRpt = new List<ReportDocument>();
            ReportDocument mainRpt = getEyeMain();
            if (mainRpt != null)
            {
                listRpt.Add(mainRpt);
            }

            //ReportDocument aviaRpt = new ReportDocument();
            //aviaRpt = getEyeAvia();
            //if (mainRpt != null)
            //{
            //    listRpt.Add(aviaRpt);
            //}

            if (listRpt.Count > 0)
            {
                Report.frmReport frm = new Report.frmReport();
                frm.previewRpt(listRpt);
            }
        }

        public static void printRptEye(int qtyCopy)
        {
            List<ReportDocument> listRpt = new List<ReportDocument>();
            ReportDocument mainRpt = getEyeMain();
            if (mainRpt != null)
            {
                listRpt.Add(mainRpt);
            }

            //ReportDocument aviaRpt = new ReportDocument();
            //aviaRpt = getEyeAvia();
            //if (mainRpt != null)
            //{
            //    listRpt.Add(aviaRpt);
            //}

            foreach (ReportDocument rpt in listRpt)
            {
                rpt.PrintToPrinter(qtyCopy, false, 0, 0);
            }
        }
        #endregion

        #region call wristband
        public static void printWristband()
        {
            ReportDocument rpt = rptDoc("RG120");
            if (rpt != null)
            {
                rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                rpt.SetParameterValue("@print_user", Program.CurrentUser.mut_fullname);
                rpt.SetParameterValue("@mrt_code", "RG120");
                Report.frmReport frm = new Report.frmReport();
                frm.printWristband(rpt);
            }
        }
        #endregion

        #region call general report
        public static void previewRpt(int tpr_id, List<string> rptCode)
        {
            try
            {
                List<ReportDocument> listRpt = new List<ReportDocument>();
                foreach (string code in rptCode)
                {
                    ReportDocument rpt = rptDoc(code);
                    if (rpt != null)
                    {
                        if (new List<string> { }.Contains(code))
                        {
                            //rpt.SetParameterValue("@tpr_id", tpr_id);
                            //rpt.SetParameterValue("@print_User", Program.CurrentUser.mut_fullname);
                            //rpt.SetParameterValue("@mrt_code", code);
                        }
                        else
                        {
                            rpt.SetParameterValue("@tpr_id", tpr_id);
                            rpt.SetParameterValue("@print_User", Program.CurrentUser.mut_fullname);
                            rpt.SetParameterValue("@mrt_code", code);
                        }
                        listRpt.Add(rpt);
                    }
                }
                if (listRpt.Count > 0)
                {
                    Report.frmReport frm = new Report.frmReport();
                    frm.previewRpt(listRpt);
                }
            }
            catch (Exception)
            {

            }
        }

        public static void previewRpt(List<string> rptCode)
        {
            try
            {
                List<ReportDocument> listRpt = new List<ReportDocument>();
                foreach (string code in rptCode)
                {
                    ReportDocument rpt = rptDoc(code);
                    if (rpt != null)
                    {
                        rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                        rpt.SetParameterValue("@print_User", Program.CurrentUser.mut_fullname);
                        rpt.SetParameterValue("@mrt_code", code);
                        listRpt.Add(rpt);
                    }
                }
                if (listRpt.Count > 0)
                {
                    Report.frmReport frm = new Report.frmReport();
                    frm.previewRpt(listRpt);
                }
            }
            catch (Exception)
            {
                
            }
        }

        public static void printRpt(List<string> rptCode)
        {
            List<ReportDocument> listRpt = new List<ReportDocument>();
            foreach (string code in rptCode)
            {
                ReportDocument rpt = rptDoc(code);
                if (rpt != null)
                {
                    rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                    rpt.SetParameterValue("@print_User", Program.CurrentUser.mut_fullname);
                    rpt.SetParameterValue("@mrt_code", code);
                    rpt.PrintToPrinter(1, false, 0, 0);
                }
            }
        }
        #endregion

        #region corp
        public static void previewBMICorp(string tcd_code, string month, string printdate, string month_2)
        {
            ReportDocument rpt = rptDoc("CO101");
            if (rpt != null)
            {
                rpt.SetParameterValue("@tcd_code", tcd_code);
                rpt.SetParameterValue("@month", month);
                rpt.SetParameterValue("printdate", printdate);
                rpt.SetParameterValue("month", month_2);
                Report.frmPatientBook frm = new Report.frmPatientBook();
                frm.previewRpt(rpt);
            }
        }
        public static void previewCorp(int tcd_id)
        {
            ReportDocument rpt = rptDoc("CO102");
            if (rpt != null)
            {
                rpt.SetParameterValue("@tcd_id", tcd_id);
                Report.frmPatientBook frm = new Report.frmPatientBook();
                frm.previewRpt(rpt);
            }
        }
        #endregion

        #region book

        public static void previewOnePageTH()
        {
            ReportDocument rpt = rptDoc("BK302");
            if (rpt != null)
            {
                rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                //rpt.SetParameterValue("@lang", "T");
                Report.frmPatientBook frm = new Report.frmPatientBook();
                frm.previewRpt(rpt);
            }
        }
        public static void previewOnePageEN()
        {
            ReportDocument rpt = rptDoc("BK303");
            if (rpt != null)
            {
                rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                //rpt.SetParameterValue("@lang", "E");
                Report.frmPatientBook frm = new Report.frmPatientBook();
                frm.previewRpt(rpt);
            }
        }
        public static void previewWristbandRpt(string rptCode)
        {
            ReportDocument rpt = rptDoc(rptCode);
            if (rpt != null)
            {
                rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                rpt.SetParameterValue("@print_User", Program.CurrentUser.mut_fullname);
                rpt.SetParameterValue("@mrt_code", rptCode);
                Report.frmPatientBook frm = new Report.frmPatientBook();
                frm.previewRpt(rpt);
            }
        }
        public static void previewBookRpt(string rptCode)
        {
            ReportDocument rpt = rptDoc(rptCode);
            if (rpt != null)
            {
                rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                Report.frmPatientBook frm = new Report.frmPatientBook();
                frm.previewRpt(rpt);
            }
        }
        public static bool pdfBookToServer(string rptCode, string destinationPath, string fileName, string oldFileName)
        {
            try
            {
                if (Directory.Exists(destinationPath))
                {
                    ReportDocument rpt = rptDoc(rptCode);
                    if (rpt != null)
                    {
                        rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                        string desPath = destinationPath + @"\" + fileName;
                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, desPath);
                        if (oldFileName != null)
                        {
                            string oldDesPath = destinationPath + @"\" + oldFileName;
                            if (File.Exists(oldDesPath))
                            {
                                File.Delete(oldDesPath);
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
            return true;
        }
        public static bool pdfBookOnePageTHToServer(string destinationPath, string fileName, string oldFileName)
        {
            try
            {
                if (Directory.Exists(destinationPath))
                {
                    ReportDocument rpt = rptDoc("BK302");
                    if (rpt != null)
                    {
                        rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                        //rpt.SetParameterValue("@lang", "T");
                        string desPath = destinationPath + @"\" + fileName;
                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, desPath);
                        if (oldFileName != null)
                        {
                            string oldDesPath = destinationPath + @"\" + oldFileName;
                            if (File.Exists(oldDesPath))
                            {
                                File.Delete(oldDesPath);
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
            return true;
        }
        public static bool pdfBookOnePageENToServer(string destinationPath, string fileName, string oldFileName)
        {
            try
            {
                if (Directory.Exists(destinationPath))
                {
                    ReportDocument rpt = rptDoc("BK303");
                    if (rpt != null)
                    {
                        rpt.SetParameterValue("@tpr_id", Program.CurrentRegis.tpr_id);
                        //rpt.SetParameterValue("@lang", "E");
                        string desPath = destinationPath + @"\" + fileName;
                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, desPath);
                        if (oldFileName != null)
                        {
                            string oldDesPath = destinationPath + @"\" + oldFileName;
                            if (File.Exists(oldDesPath))
                            {
                                File.Delete(oldDesPath);
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
            return true;
        }
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
