using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using DBCheckup;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace BKvs2010.Class
{
    /// <summary>
    /// Use Send to DocScan 
    /// </summary>
    /// 

    class DocScan
    {
        Service.WS_SendToDocscanCls wsSaveDocscan = new Service.WS_SendToDocscanCls();
        
        #region SetToDocscan 
        public void Send(string roomcode, int tprid)
        {
            try
            {
                CreateFolder();
                int count = Directory.GetFiles(@"Forms").Length;
                if (count != 0)
                {
                    string[] filePath = Directory.GetFiles(@"Forms\");
                    DeleteFile(filePath);
                }
                ReportDocument rpt = Report.ClsReport.rptDoc(roomcode);
                if (rpt != null)
                {
                    rpt.SetParameterValue("@tpr_id", tprid);
                    rpt.SetParameterValue("@print_User", Program.CurrentUser.mut_fullname);
                    rpt.SetParameterValue("@mrt_code", roomcode);

                    //check doc scan : Pang 15/7/2015
                    for (int i = 0; i < rpt.ParameterFields.Count; i++)
                    {
                        if (rpt.ParameterFields[i].Name == "@isDocScan")
                        {
                            rpt.SetParameterValue("@isDocScan", true);
                            break;
                        }
                    }
                }
                int pageNo = rpt.FormatEngine.GetLastPageNumber(new CrystalDecisions.Shared.ReportPageRequestContext());
                //List<string> rptCode = new List<string> { roomcode };
                //List<ReportDocument> listRpt = new List<ReportDocument>();
                //ReportDocument rpt = ClsReport.rptDoc(roomcode);
                //foreach (string code in rptCode)
                //{
                    //if (rpt != null)
                    //{
                    //    rpt.SetParameterValue("@tpr_id", tprid);
                    //    rpt.SetParameterValue("@print_User", Program.CurrentUser.mut_fullname);
                    //    rpt.SetParameterValue("@mrt_code", roomcode);
                    //    listRpt.Add(rpt);
                    //}
                //}
                //if (listRpt.Count > 0)
                //{
                    //cryRpt = listRpt[0];
                //}
                //ExportOptions CrExportOptions;
                //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                //CrDiskFileDestinationOptions.DiskFileName = Path.GetFullPath(@"Forms/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + ".pdf");
                //CrExportOptions = rpt.ExportOptions;
                //{ 
                //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                //}
                //rpt.Export();
                string pdffile = Path.GetFullPath(@"Forms/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + ".pdf");
                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, pdffile);
                string folderName = @"Imgs";
                string pathString = System.IO.Path.Combine(folderName, "Sub");
                Directory.CreateDirectory(pathString);
                string path = Path.GetFullPath(@"Imgs/Sub/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                //string pdffile = Path.GetFullPath(@"Forms/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + ".pdf");
                PdfToJpg(pdffile, path, roomcode);
            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        #region ConvertPdffileCommand
        public void PdfToJpg(string inputPDFFile, string outputImagesPath, string roomCode)
        {
            string AppPath = Application.StartupPath;
            if (System.Diagnostics.Debugger.IsAttached)
            {
                AppPath = AppPath.Remove(AppPath.Length - 10, 10);
            }
            string appDir = AppPath + "\\DLL\\gswin32.exe";
            //string ghostScriptPath = Path.GetFullPath(@"DLL/gswin32.exe");
            string ghostScriptPath = appDir;
            String ars = "-dNOPAUSE -sDEVICE=jpeg -r150 -o" + outputImagesPath + "%d.jpg -sPAPERSIZE=a4 " + inputPDFFile;
            Process proc = new Process();
            proc.StartInfo.FileName = ghostScriptPath;
            proc.StartInfo.Arguments = ars;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
        #endregion

        #region ConvertImage
        public byte[] ImageToBinary(string imagePath)
        {
            FileStream fS = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            return b;
        }
        #endregion

        #region QueryCode
        private void QueryCode(string roomCode)
        {
           // getDocCode = dbc.pw_Get_DocCode(roomCode).ToList();
        }
        #endregion

        #region SaveToDocscan interface webservice
        public bool SaveToDocScan(CheckupDataContext dbc, string roomcode, int tprid, string enCurrent, string careProvider)
        {
            Boolean isCompleted = false; 
            try
            {

                DateTime datenow = Program.GetServerDateTime();
                var getDocCode = dbc.mst_report_docscans.Where(x => x.mst_report.mrt_code == roomcode
                                                                 && x.mst_report.mrt_status == 'A'
                                                                 && datenow >= x.mst_report.mrt_effective_date.Value
                                                                 && (x.mst_report.mrt_expire_date != null ? (datenow <= x.mst_report.mrt_expire_date.Value) : true))
                                    .OrderBy(x => x.mds_page_no).ToList();

                int count = Directory.GetFiles(@"Imgs\Sub").Length;
                for (int i = 0; i < count; i++)
                {
                    #region old code
                    //byte[] img;
                    //if (roomcode == "PT101" || roomcode == "PT102" || roomcode == "PT103" || roomcode == "PT104")
                    //    ////Specific 1 page
                    //    img = ImageToBinary(@"Imgs/Sub/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "1.jpg");
                    
                    //else
                    //    img = ImageToBinary(@"Imgs/Sub/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + i + ".jpg");
                    
                    ////SetData to Save
                    //var getDocCode = (from t1 in dbc.mst_reports
                    //                 join t2 in dbc.mst_report_docscans
                    //                 on t1.mrt_id equals t2.mrt_id
                    //                 where t1.mrt_code == roomcode
                    //                 && t1.mrt_status == 'A'
                    //                 && datenow >= t1.mrt_effective_date.Value
                    //                 && (t1.mrt_expire_date != null ? (datenow <= t1.mrt_expire_date.Value) : true)
                    //                 orderby t2.mds_page_no
                    //                 select new
                    //                 {
                    //                     filereport = t1.mrt_path_file + "/" + t1.mrt_file_name,
                    //                     t2.mds_doc_code,
                    //                     t2.mds_page_code,
                    //                     t2.mds_page_no,
                    //                 }).ToList();

                   // getDocCode = dbc.pw_Get_DocCode(roomcode).ToList();
                    //string hn = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1.trn_patient.tpt_hn_no).FirstOrDefault();
                    //string strEn = enCurrent;
                    //string strHn = hn;
                    //string[] sAryHN = strHn.Split('-');
                    //string[] sAryEN = strEn.Split('-');
                    //string HN = sAryHN[0] + sAryHN[1] + sAryHN[2];
                    //string EN = sAryEN[0] + sAryEN[1] + sAryEN[2];
                    //DataTable Tmptable = new DataTable(); DataSet tmpds = new DataSet("dss");
                    //Tmptable.Columns.Add("code"); Tmptable.Columns.Add("page");
                    //tmpds.Tables.Add(Tmptable);
                    //foreach (var item in getDocCode)////not confirm
                    //{
                    //    var row = Tmptable.NewRow();
                    //    row["code"] = item.mds_doc_code;
                    //    row["page"] = item.mds_page_code + item.mds_page_no;
                    //    Tmptable.Rows.Add(row);
                    //}
                    //string page = TgetDocCode[i - 1]["page"].ToString();
                    //string doctorCode = Program.CurrentUser.mut_carevider_code;
                    //string locationCode = Program.CurrentSite.mhs_code;
                    //string documentCode = Tmptable.Rows[i - 1]["code"].ToString();
                    //string userId = "EMRCHECKUP";
                    //string programId = "EMRCHECKUP";
                    #endregion

                    byte[] img;
                    //if (roomcode == "PT101" || roomcode == "PT102" || roomcode == "PT103" || roomcode == "PT104")
                    //    ////Specific 1 page
                    //    img = ImageToBinary(@"Imgs/Sub/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "1.jpg");

                    //else
                        img = ImageToBinary(@"Imgs/Sub/" + tprid + roomcode + "_" + DateTime.Now.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + (i + 1) + ".jpg");

                    string hn = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1.trn_patient.tpt_hn_no).FirstOrDefault();
                    string HN = hn.Replace("-", "");
                    string EN = enCurrent.Replace("-", "");
                    string page =  getDocCode[i].mds_page_code + getDocCode[i].mds_page_no;
                    string doctorCode = careProvider;
                    string locationCode = Program.CurrentSite.mhs_code;
                    string documentCode = getDocCode[i].mds_doc_code;
                    string userId = "EMRCHECKUP";
                    string programId = "EMRCHECKUP";
                    string DocscanResult =  wsSaveDocscan.SaveToDocscan(img, HN, EN, page, careProvider, locationCode, documentCode, userId, programId);
                    if  (string.IsNullOrEmpty(DocscanResult )){
                        
                    }else{
                    MessageBox.Show (DocscanResult);
                    }
                    
                }
                isCompleted = true;

                if (isCompleted == true)
                    DeleteFile();
            }
            catch 
            {
                DeleteFile();
            }

            return isCompleted;
        }
        #endregion

        #region GetHistory
        public void GetHistory(string roomCode, string enCurrent, int tprid)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    frmBGScreen frmbg = new frmBGScreen();
                    frmbg.Show();
                    Application.DoEvents();
                    HistoryData.filelength = 0;
                    HistoryData.newImage = null;
                    HistoryData.bmpData = null;
                    HistoryData.arrlist.Clear();
                    // getDocCode = dbc.pw_Get_DocCode(roomCode).ToList();
                    DateTime datenow = Program.GetServerDateTime();
                    var getDocCode = (from t1 in dbc.mst_reports
                                      join t2 in dbc.mst_report_docscans
                                      on t1.mrt_id equals t2.mrt_id
                                      where t1.mrt_code == roomCode
                                      && t1.mrt_status == 'A'
                                      && datenow >= t1.mrt_effective_date.Value
                                      && (t1.mrt_expire_date != null ? (datenow <= t1.mrt_expire_date.Value) : true)
                                      orderby t2.mds_page_no
                                      select new
                                      {
                                          filereport = t1.mrt_path_file + "/" + t1.mrt_file_name,
                                          t2.mds_doc_code,
                                          t2.mds_page_code,
                                          t2.mds_page_no,
                                      }).ToList();
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    if (getDocCode.Count != 0)
                    {
                        for (int i = 0; i <= getDocCode.Count - 1; i++)
                        {
                            string hn = (from t1 in dbc.trn_patient_regis where t1.tpr_id == tprid select t1.trn_patient.tpt_hn_no).FirstOrDefault();
                            string strEn = enCurrent;
                            string strHn = hn;
                            string[] sAryHN = strHn.Split('-');
                            string[] sAryEN = strEn.Split('-');
                            HistoryData.HN = sAryHN[0] + sAryHN[1] + sAryHN[2];
                            HistoryData.EN = sAryEN[0] + sAryEN[1] + sAryEN[2];
                            HistoryData.page = getDocCode[i].mds_page_code + getDocCode[i].mds_page_no;
                            HistoryData.doctorCode = Program.CurrentUser.mut_carevider_code;
                            HistoryData.locationCode = Program.CurrentSite.mhs_code;
                            HistoryData.documentCode = getDocCode[i].mds_doc_code;
                            //ds = wsSaveDocscan.getDocumentList(HistoryData.HN, HistoryData.EN, "", HistoryData.documentCode, HistoryData.page);
                            // ds = wsSaveDocscan.getDocumentList("0111006591", "O0113450939", "", "VVF", "A1");
                            if (ds != null)
                            {
                                dt = ds.Tables[0];
                                HistoryData.Totalpage = dt.Rows.Count;
                                for (int j = 0; j <= dt.Rows.Count - 1; j++)
                                {
                                    HistoryData.item = dt.Rows[j][1].ToString();
                                    HistoryData.img = wsSaveDocscan.getImage(HistoryData.HN, HistoryData.EN, HistoryData.item);
                                    //HistoryData.img = wsSaveDocscan.getImage("0111006591", "O0113450939", "1"); 
                                    byte[] buffer = HistoryData.img;
                                    MemoryStream ms2 = new MemoryStream(buffer);
                                    Bitmap bmp = new Bitmap(ms2);
                                    Image img = (Image)bmp;
                                    HistoryData.bmpData = bmp;
                                    HistoryData.arrlist.Add(img);
                                    HistoryData.filelength = HistoryData.filelength + 1;
                                    HistoryData.newImage = (Image)HistoryData.arrlist[j];
                                }
                                HistoryData.count = 0;
                            }
                        }
                    }
                    if (dt.Rows.Count != 0)
                    {
                        if (HistoryData.showform != 'N')
                        {
                            frmViewDocScan frm = new frmViewDocScan();
                            frm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("View docscan Failed ! Please contact administrator", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    frmbg.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region CreatePath
        private void CreateFolder()
        {
            try
            {
                if (!Directory.Exists("Forms"))
                    Directory.CreateDirectory("Forms");
                if (!Directory.Exists("Imgs"))
                    Directory.CreateDirectory("Imgs");
                if (!Directory.Exists("DLL"))
                    Directory.CreateDirectory("DLL");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OtherFunc
        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void DeleteFile(string[] filePaths)
        {
            foreach (string filePath in filePaths)
                File.Delete(filePath);
        }

        private void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }

        private void MakeCopy()
        {
            string AppPath = Application.StartupPath;
            AppPath = AppPath.Remove(AppPath.Length - 10, 10);
            string appDir = AppPath + "\\DLL";
            CopyFolder(appDir, @"DLL");
        }

        public bool CheckPath(CheckupDataContext dbc, string roomcode)
        {
            Boolean isCheck = false;
            var chkpath = (from t in dbc.mst_reports
                           where t.mrt_code == roomcode
                           select t.mrt_path_file).FirstOrDefault();
            if (chkpath != null) //Exist report
                isCheck = true;
            else //Not exist report
                isCheck = false;

            return isCheck;
        }

        private void DeleteFile()
        {
            try
            {
                string[] filePath = Directory.GetFiles(@"Forms\");
                DeleteFile(filePath);
                var Directories = Directory.GetDirectories(@"Imgs\");
                foreach (var directory in Directories)
                    Directory.Delete(directory, true);
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region SaveAndSendtoDocscan
        public bool SendtoDocscan(string roomcode, int tprid, string en, string careProvider)
        {
            Boolean isCompleted = false; 
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    if (CheckPath(dbc, roomcode) == true)
                    {
                        frmBGScreen frmbg = new frmBGScreen();
                        frmbg.Show();
                        Application.DoEvents();
                        Send(roomcode, tprid);

                        if (SaveToDocScan(dbc, roomcode, tprid, en, careProvider))
                        {
                            //dbc.pw_Update_Docscan(tprid, roomcode);
                            dbc.SubmitChanges();
                            HistoryData.savestatus = "Send To Docscan Completed";
                            isCompleted = true;
                        }
                        frmbg.Close();
                    }
                    else
                    {
                        HistoryData.savestatus = "Not found report !";
                    }
                }
            }
            catch 
            {
                DeleteFile();
            }
            return isCompleted;
        }
        #endregion



        #region morn function getRpt
        public ReportDocument getRpt(string rptCode)
        {
            ReportDocument rpt = Report.ClsReport.rptDoc(rptCode);
            return rpt;
        }
        #endregion

        #region morn function getRptToPdf
        //public Stream convRptToPDF(ReportDocument rptDoc)
        //{
            
        //    Stream stream = rptDoc.ExportToStream(ExportFormatType.PortableDocFormat);
        //    FileStream fs = new FileStream();
        //    stream.CopyTo(fs);
        //    return stream;

        //    StreamReader sr = new StreamReader(stream);
        //    StreamWriter sw;
        //    EncoderParameter e;

        //}


        #endregion

        #region morn function getPdfToJpg
        public void streamToJpg(Stream srcStream)
        {
            //Document pdfDocument = new Document("input.pdf");

            //for (int pageCount = 1; pageCount <= pdfDocument.Pages.Count; pageCount++)
            //{
            //    using (FileStream imageStream = new FileStream("image" + pageCount + ".jpg", FileMode.Create))
            //    {
            //        //create Resolution object
            //        Resolution resolution = new Resolution(300);
            //        //create JPEG device with specified attributes (Width, Height, Resolution, Quality)
            //        // where Quality [0-100], 100 is Maximum
            //        JpegDevice jpegDevice = new JpegDevice(resolution, 100);

            //        //convert a particular page and save the image to stream
            //        jpegDevice.Process(pdfDocument.Pages[pageCount], imageStream);
            //        //close stream
            //        imageStream.Close();
            //    }
            //}


            //Stream st;
            //FileStream f = new FileStream(@"D:\samp\rt.jpg", FileMode.Create);
            //byte[] b = new byte[10000];

            //st.Read(b, 0, b.Length);

            //st.Close();
            //f.Write(b, 0, b.Length);
            //f.Flush();
            //f.Close(); 

            //Image img = Image.FromStream(srcStream);
            //img.Save(@"C:\abc.jpg");
        }
        #endregion
    }

    #region StaticMembers
    public static class HistoryData
    {
        public static string HN;
        public static string EN;
        public static string page;
        public static string doctorCode;
        public static string locationCode;
        public static string documentCode;
        public static string item;
        public static byte[] img;
        public static Bitmap bmpData;
        public static Image newImage;
        public static int Totalpage;
        public static ArrayList arrlist = new ArrayList();
        public static int count = 0;
        public static int filelength = 0;
        public static string savestatus;
        public static char showform; //'N' is not show form ViewDocscan || '!=N' is Show form ViewDocscan
    }
    #endregion
}
