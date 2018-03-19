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

namespace BKvs2010
{
    class ClsReportPatientBook
    {
        #region ClsReport.cs
        //-------------------- ClsReport.cs --------------------------------------------------

        public static string usernameConnServer = "reportuser";
        public static string passwordConnServer = "reportuser";

        private static void SetDBLogonForReport(ReportDocument reportDocument)
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

        private static ReportDocument rptDoc(string rptCode)
        {
            ReportDocument cryRpt = new ReportDocument();
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
                            //using (Class.NetworkShareAccesser.Access(Program.serverIP, usernameConnServer, passwordConnServer))
                            //{
                                //string pathRpt = result.mrt_path_file.Replace(@"C:\Checkup_Pathway", @"\\" + Program.serverIP) + @"\" + result.mrt_file_name;
                            string pathRpt = @"\\" + PrePareData.StaticDataCls.ServerReport + @"\" + result.mrt_path_file + @"\" + result.mrt_file_name;
                                if (pathRpt != string.Empty)
                                {
                                    if (File.Exists(pathRpt))
                                    {
                                        cryRpt.Load(pathRpt);
                                        SetDBLogonForReport(cryRpt);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Connection Server Error : Path File '" + result.mrt_file_name + "' is not found.");
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
            if (!cryRpt.Equals(new ReportDocument())) cryRpt.Refresh();
            return cryRpt;
        }

        public static int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
        }

        #endregion

        public static void previewRpt(List<string> rptCode, int? tprID)
        {
            try
            {
                Report.frmReport frm = new Report.frmReport();
                List<ReportDocument> listRpt = new List<ReportDocument>();
                List<trn_book_print> listObjLog = new List<trn_book_print>();
                InhCheckupDataContext dbcBookLog = new InhCheckupDataContext();
                int? objMax = (dbcBookLog.trn_book_prints.Max(m => m.tbp_print_runNum)) + 1;
                int UserID = Program.CurrentUser.mut_id;
                int StartPage = 0;
                int endPage = 0;
                int StatusPage = 0;
                foreach (string code in rptCode)
                {
                    ReportDocument rpt = rptDoc(code);
                    if (rpt != null)
                    {
                        switch (code)
                        {
                            #region Page TH
                            //--- TH ---------------------------------------

                            //case "BK101": StatusPage = 1; break;
                            //case "BK102": StatusPage = 1; break;
                            //case "BK103": StatusPage = 1; break;
                            //case "BK104": StatusPage = 1; break;
                            //case "BK1051": StatusPage = 1; break;
                            //case "BK1052": StatusPage = 1; break;
                            //case "BK1053": StatusPage = 1; break;
                            //case "BK106": StatusPage = 1; break;
                            //case "BK107": StatusPage = 1; break;
                            //case "BK108": StatusPage = 1; break;
                            //case "BK109": StatusPage = 0; break;
                            //case "BK110": StatusPage = 1; break;
                            //case "BK111": StatusPage = 1; break;
                            //case "BK112": StatusPage = 0; break;
                            //case "BK113": StatusPage = 0; break;

                            case "BK101":
                                StatusPage = BK101(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK102":
                                StatusPage = BK102(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK103":
                                StatusPage = BK103(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK104":
                                StatusPage = BK104(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK1051":
                                StatusPage = BK1051(tprID, dbcBookLog, StatusPage);
                                break;
                            case "BK1052":
                                StatusPage = BK1052(tprID, dbcBookLog, StatusPage);
                                break;
                            case "BK1053":
                                StatusPage = BK1053(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK106":
                                StatusPage = BK106(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK107":
                                StatusPage = BK107(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK108":
                                StatusPage = BK108(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK109":
                                StatusPage = BK109(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK110":
                                StatusPage = BK110(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK111":
                                StatusPage = BK111(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK112":
                                StatusPage = BK112(StatusPage);
                                break;

                            case "BK113":
                                StatusPage = BK113(tprID, dbcBookLog, StatusPage);
                                break;

                            #endregion

                            #region Page EN
                            //--- EN ---------------------------------------

                            //case "BK201": StatusPage = 1; break;
                            //case "BK202": StatusPage = 1; break;
                            //case "BK203": StatusPage = 1; break;
                            //case "BK204": StatusPage = 1; break;
                            //case "BK2051": StatusPage = 1; break;
                            //case "BK2052": StatusPage = 1; break;
                            //case "BK2053": StatusPage = 1; break;
                            //case "BK206": StatusPage = 1; break;
                            //case "BK207": StatusPage = 1; break;
                            //case "BK208": StatusPage = 1; break;
                            //case "BK209": StatusPage = 0; break;
                            //case "BK210": StatusPage = 1; break;
                            //case "BK211": StatusPage = 1; break;
                            //case "BK212": StatusPage = 0; break;
                            //case "BK213": StatusPage = 0; break;

                            case "BK201":
                                StatusPage = BK101(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK202":
                                StatusPage = BK102(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK203":
                                StatusPage = BK103(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK204":
                                StatusPage = BK104(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK2051":
                                StatusPage = BK1051(tprID, dbcBookLog, StatusPage);
                                break;
                            case "BK2052":
                                StatusPage = BK1052(tprID, dbcBookLog, StatusPage);
                                break;
                            case "BK2053":
                                StatusPage = BK1053(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK206":
                                StatusPage = BK106(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK207":
                                StatusPage = BK107(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK208":
                                StatusPage = BK108(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK209":
                                StatusPage = BK109(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK210":
                                StatusPage = BK110(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK211":
                                StatusPage = BK111(tprID, dbcBookLog, StatusPage);
                                break;

                            case "BK212":
                                StatusPage = BK112(StatusPage);
                                break;

                            case "BK213":
                                StatusPage = BK113(tprID, dbcBookLog, StatusPage);
                                break;

                            #endregion
                        }

                        SetPageNumber(tprID, frm, listRpt, listObjLog, objMax, UserID, ref StartPage, ref endPage, StatusPage, code, rpt);
                    }
                }
                if (listRpt.Count > 0)
                {
                    if (listObjLog.Count > 0)
                    {
                        frm.listObjLog = listObjLog;
                    }
                    else
                    {
                        frm.listObjLog = null;
                    }

                    frm.previewRpt(listRpt);
                }
            }
            catch (Exception)
            {
                //throw new Exception(ex.Message);
            }
        }

        #region Generate page number

        private static void SetPageNumber(int? tprID, Report.frmReport frm, List<ReportDocument> listRpt, List<trn_book_print> listObjLog, int? objMax, int UserID, ref int StartPage, ref int endPage, int StatusPage, string code, ReportDocument rpt)
        {
            try
            {
                if (StatusPage == 1)
                {
                    StartPage = (StartPage == 0) ? 1 : StartPage + endPage;
                    endPage = (endPage == 0) ? 1 : frm.printChkPage(rpt, tprID, StartPage);

                    trn_book_print ObjLog = new trn_book_print();
                    ObjLog.tpr_id = tprID;
                    ObjLog.mrt_code = code;
                    ObjLog.tbp_startPage = StartPage;
                    ObjLog.tbp_endPage = (endPage > 1) ? (StartPage + endPage) - 1 : StartPage;
                    ObjLog.tbp_remark = "V.101";
                    ObjLog.tbp_create_by = UserID;
                    ObjLog.tbp_update_date = Program.GetServerDateTime();
                    ObjLog.tbp_print_runNum = objMax;
                    listObjLog.Add(ObjLog);

                    rpt.SetParameterValue("@tpr_id", tprID);
                    rpt.SetParameterValue("@StartPage", StartPage);
                    listRpt.Add(rpt);
                }
            }
            catch (Exception)
            {
                //throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Check data display page is not null

        private static int BK113(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count113 = dbcBookLog.pw_CR_PatienBook_DentalScreeningRecord_hdr(tprID).Count();
            StatusPage = (count113 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK112(int StatusPage)
        {
            StatusPage = 0;
            return StatusPage;
        }

        private static int BK111(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count111 = dbcBookLog.pw_CR_PatienBook_HearingTest_hdr(tprID).Count();
            StatusPage = (count111 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK110(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count1101 = dbcBookLog.pw_CR_PatienBook_Summary_N(tprID).Count();
            int count1102 = dbcBookLog.pw_CR_PatienBook_Summary_A(tprID).Count();
            int count110 = count1101 + count1102;
            StatusPage = (count110 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK109(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count109 = dbcBookLog.pw_CR_PatienBook_OtherTest(tprID)
                    .Where(s => s.trxm_exam != null)
                        .Count();
            StatusPage = (count109 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK108(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count108 = dbcBookLog.pw_CR_PatienBook_UltrasoundBrest(tprID)
                    .Where(s => s.trxr_result != null)
                        .Count();
            StatusPage = (count108 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK107(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count107 = dbcBookLog.pw_CR_PatienBook_UltrasoundAbdomen(tprID)
                    .Where(s => s.trxr_result != null)
                        .Count();
            StatusPage = (count107 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK106(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count106 = dbcBookLog.pw_CR_PatienBook_ChestXRay(tprID)
                    .Where(s => s.trxr_result != null)
                        .Count();
            StatusPage = (count106 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK1053(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count1053 = dbcBookLog.pw_CR_PatienBook_LaboratoryComparison_hdr(tprID)
                    .Where(s => s.vLab1 != null ||
                                s.vLab2 != null ||
                                s.vLab3 != null ||
                                s.vLab4 != null ||
                                s.vLab5 != null
                                )
                                .Count();
            StatusPage = (count1053 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK1052(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count1052 = dbcBookLog.pw_CR_PatienBook_LaboratoryComparison_hdr(tprID)
                    .Where(s => s.vLab1 != null ||
                                s.vLab2 != null ||
                                s.vLab3 != null ||
                                s.vLab4 != null ||
                                s.vLab5 != null
                                )
                                .Count();
            StatusPage = (count1052 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK1051(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count1051 = dbcBookLog.pw_CR_PatienBook_LaboratoryComparison_hdr(tprID)
                    .Where(s => s.vLab1 != null ||
                                s.vLab2 != null ||
                                s.vLab3 != null ||
                                s.vLab4 != null ||
                                s.vLab5 != null
                                )
                                .Count();
            StatusPage = (count1051 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK104(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count104 = dbcBookLog.pw_CR_PatienBook_LaboratoryResult(tprID).Count();
            StatusPage = (count104 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK103(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count1031 = dbcBookLog.pw_CR_PatienBook_PhysicalExamination_hdr(tprID).Count();
            int count1032 = dbcBookLog.pw_CR_PatienBook_PhysicalExamination_dtl(tprID)
                    .Where(s => s.vLab1 != null ||
                                s.vLab2 != null ||
                                s.vLab3 != null
                                )
                                .Count();
            int count1033 = dbcBookLog.pw_CR_PatienBook_PhysicalExamination_dcd(tprID).Count();
            int count103 = count1031 + count1032 + count1033;
            StatusPage = (count103 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK102(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count102 = dbcBookLog.pw_CR_PatienBook_MedicalHistory(tprID).Count();
            StatusPage = (count102 > 0) ? 1 : 0;
            return StatusPage;
        }

        private static int BK101(int? tprID, CheckupDataContext dbcBookLog, int StatusPage)
        {
            int count101 = dbcBookLog.pw_CR_PatienBook_PatientProfile(tprID).Count();
            StatusPage = (count101 > 0) ? 1 : 0;
            return StatusPage;
        }

        #endregion
  
    }
}
