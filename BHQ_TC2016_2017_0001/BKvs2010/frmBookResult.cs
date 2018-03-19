using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DBCheckup;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using BKvs2010.EmrClass;

//add suriya 17/12/2014
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Net;
using System.Data.Linq;

//end suriya 17/12/2014

namespace BKvs2010
{
    public partial class frmBookResult : Form
    {
        public frmBookResult()
        {
            InitializeComponent();
            this.Text = PrePareData.StaticDataCls.ProjectName + "CK Report";
            gvSearch.AutoGenerateColumns = false;
            AutoCompleteDoctor obj = new AutoCompleteDoctor();
            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged +=
                new UserControlLibrary.AutoCompleteTextBox.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

            rlgPhyEx.BackgroundImage = BKvs2010.Properties.Resources.PhysicalExam;
            rlgABI.BackgroundImage = BKvs2010.Properties.Resources.ABI;
            rlgHearing.BackgroundImage = BKvs2010.Properties.Resources.Hearing;
            rlgEyes.BackgroundImage = BKvs2010.Properties.Resources.Eyes;
            rlgDental.BackgroundImage = BKvs2010.Properties.Resources.Dental;
            rlgEcho.BackgroundImage = BKvs2010.Properties.Resources.Echo;
            rlgEKG.BackgroundImage = BKvs2010.Properties.Resources.EKG;
            rlgEST.BackgroundImage = BKvs2010.Properties.Resources.EST;
            rlgXray.BackgroundImage = BKvs2010.Properties.Resources.Xray;
            rlgPFT.BackgroundImage = BKvs2010.Properties.Resources.PFT;
            rlgCarotid.BackgroundImage = BKvs2010.Properties.Resources.Carotid;
            rlgOtherExam.BackgroundImage = BKvs2010.Properties.Resources.OtherExam;
            rlgAllExam.BackgroundImage = BKvs2010.Properties.Resources.Lab;
            rlgPAP.BackgroundImage = BKvs2010.Properties.Resources.PAP;
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();
        Image DefaultImageProfile = null;

        private trn_patient_regi _PatientRegis;
        private trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                _PatientRegis = value;
                additionalItemUC1.PatientRegis = _PatientRegis;
                if (_PatientRegis == null)
                {
                    bookAddressUC1.book_cover = null;
                    bookTransaction1.ListBookEvent = null;
                    tabBasicMeasurementUC1.tpr_id = null;
                }
                else
                {
                    if (_PatientRegis.trn_patient_book_cover == null)
                    {
                        _PatientRegis.trn_patient_book_cover = new trn_patient_book_cover();
                    }
                    bookAddressUC1.book_cover = _PatientRegis.trn_patient_book_cover;
                    bookTransaction1.ListBookEvent = _PatientRegis.trn_book_events;
                    tabBasicMeasurementUC1.tpr_id = _PatientRegis.tpr_id;
                    try
                    {
                        string nation = _PatientRegis.trn_patient.tpt_nation_code;
                        if (nation == "TH")
                        {
                            rdPrtTH.Checked = true;
                        }
                        else
                        {
                            rdPrtEN.Checked = true;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, UserControlLibrary.EventAutoCompleteChangedArgs e)
        {
            try
            {
                _PatientRegis.trn_patient_doctor.tpd_doctor_code = e.SelectedValue == null ? null : e.SelectedValue.ToString();
                _PatientRegis.trn_patient_doctor.tpd_update_by = Program.CurrentUser.mut_username;
                _PatientRegis.trn_patient_doctor.tpd_update_date = DateTime.Now;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }

        //private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        //{
        //    try
        //    {
        //        if (e == null)
        //        {
        //            if (_PatientRegis != null)
        //            {
        //                _PatientRegis.trn_patient_doctor.tpd_doctor_code = null;
        //                _PatientRegis.trn_patient_doctor.tpd_update_by = null;
        //                _PatientRegis.trn_patient_doctor.tpd_update_date = null;
        //            }
        //        }
        //        else
        //        {
        //            if (_PatientRegis != null)
        //            {
        //                _PatientRegis.trn_patient_doctor.tpd_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
        //                _PatientRegis.trn_patient_doctor.tpd_update_by = Program.CurrentUser.mut_username;
        //                _PatientRegis.trn_patient_doctor.tpd_update_date = DateTime.Now;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
        //    }
        //}

        private void frmBookResult_Load(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            txtVDate.Value = DateTime.Today;
            this.Text = Program.GetRoomName();

            gvSearch.RowsDefaultCellStyle.SelectionBackColor = Color.Blue;
            getSite(cobLocation);

            searchPatiant();
        }

        private void gvSearch_SelectionChanged(object sender, EventArgs e)
        {
            lbAlert.Text = "";
            try
            {
                if (gvSearch.SelectedRows.Count > 0)
                {
                    int? tpr_id = Convert.ToInt32(gvSearch.SelectedRows[0].Cells["colRefID"].Value.ToString());
                    showPatient(tpr_id);
                }
                else
                {
                    showPatient(null);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lbAlert.Text = "";
            try
            {
                if (_PatientRegis != null)
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    string username = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;

                    var book_cover = _PatientRegis.trn_patient_book_cover;
                    if (book_cover != null)
                    {
                        if (book_cover.tpbc_create_date == null)
                        {
                            book_cover.tpbc_create_date = dateNow;
                        }
                        book_cover.tpbc_update_date = dateNow;
                    }

                    trn_book_hdr tkh = _PatientRegis.trn_book_hdrs.FirstOrDefault();
                    if (tkh == null)
                    {
                        tkh = new trn_book_hdr
                        {
                            tkh_create_by = username,
                            tkh_create_date = dateNow,
                            tkh_show_chart = 'N',
                            tkh_report_HIV = 'N',
                            tkh_suppress_lab = 'N'
                        };
                        _PatientRegis.trn_book_hdrs.Add(tkh);
                    }

                    tkh.tkh_update_by = username;
                    tkh.tkh_update_date = dateNow;

                    lbAlert.Text = "Save Data Complete.";

                    string langueue = (rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";

                    trn_patient_book tpb = _PatientRegis.trn_patient_books
                                           .Where(x => x.tpb_language == langueue &&
                                                       x.tpb_package_code == packageCode &&
                                                       x.tpb_type == bookType)
                                           .FirstOrDefault();

                    if (tpb == null)
                    {
                        tpb = new trn_patient_book();
                        _PatientRegis.trn_patient_books.Add(tpb);
                        tpb.tpb_status = "NB";
                        tpb.tpb_en_no = _PatientRegis.tpr_en_no;
                        tpb.tpb_hn_no = _PatientRegis.trn_patient.tpt_hn_no;
                        tpb.tpb_create_by = username;
                        tpb.tpb_create_date = dateNow;
                    }

                    if (chkBookPrintComplete.Checked) tpb.tpb_status = "BP";
                    tpb.tpb_type = bookType;
                    tpb.tpb_package_code = packageCode;

                    tpb.tpb_language = langueue;
                    tpb.tpb_update_by = username;
                    tpb.tpb_update_date = dateNow;
                    additionalItemUC1.EndEdit();
                    try
                    {
                        dbc.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        foreach (ObjectChangeConflict occ in dbc.ChangeConflicts)
                        {
                            dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                        }
                        dbc.SubmitChanges();
                    }
                    disableButton(tpb);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            lbAlert.Text = "Save Data Complete.";
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbAlert.Text = "";
            searchPatiant();
        }

        private void gvSearch_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            gvSearch.SetRuningNumber("colNo");
        }


        private void uiRadiologyPhyEx_btnRadiologyClick(object sender, EventArgs e)
        {
            //uiRadiologyPhyEx.IsThai = rdPrtTH.Checked;
        }

        private void uiRadiologyLab_btnRadiologyClick(object sender, EventArgs e)
        {
            
        }

        private void btnPrtPreview_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lbAlert.Text = "";
            if (rdBookColor.Checked == true)
            {
                if (rdPrtTH.Checked == true)
                {
                    int tprID = 0;
                    if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                    Program.OpenReport(tprID, "BK306");
                }
                else if (rdPrtEN.Checked == true)
                {
                    int tprID = 0;
                    if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                    Program.OpenReport(tprID, "BK307");
                }
            }
            else if (rdBook.Checked == true)
            {
                if (rdPrtTH.Checked == true)
                {
                    int tprID = 0;
                    if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                    Program.OpenReport(tprID, "BK301");
                    //List<string> rptCode = new List<string> { "BK301" };
                    //int tprID = 0;
                    //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    //frm.previewReport();
                    //ClsReport.previewBookRpt("BK301");
                }
                else if (rdPrtEN.Checked == true)
                {
                    int tprID = 0;
                    if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                    Program.OpenReport(tprID, "BK304");
                    //List<string> rptCode = new List<string> { "BK304" };
                    //int tprID = 0;
                    //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    //frm.previewReport();
                    //ClsReport.previewBookRpt("BK304");
                }
            }
            else if (rdOnePage.Checked == true)
            {
                if (rdPrtTH.Checked == true)
                {
                    int tprID = 0;
                    if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                    Program.OpenReport(tprID, "BK302");
                    //List<string> rptCode = new List<string> { "BK302" };
                    //int tprID = 0;
                    //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    //frm.previewReport();
                    //ClsReport.previewOnePageTH();
                }
                else if (rdPrtEN.Checked == true)
                {
                    int tprID = 0;
                    if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                    Program.OpenReport(tprID, "BK303");
                    //List<string> rptCode = new List<string> { "BK303" };
                    //int tprID = 0;
                    //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    //frm.previewReport();
                    //ClsReport.previewOnePageEN();
                }
                else if (rdPrtJP.Checked)
                {
                    int tprID = 0;
                    if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                    Program.OpenReport(tprID, "BK311");
                    //List<string> rptCode = new List<string> { "BK311" };
                    //int tprID = 0;
                    //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    //frm.previewReport();
                }
            }
            else if (rdOnePageJMS.Checked)
            {
                int tprID = 0;
                if (_PatientRegis != null) tprID = _PatientRegis.tpr_id;
                Program.OpenReport(tprID, "BK311");
                //List<string> rptCode = new List<string> { "BK311" };
                //int tprID = 0;
                //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                //frm.previewReport();
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {
            lbAlert.Text = "";
            this.Cursor = Cursors.WaitCursor;
            try
            {
                string langueue = (rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                string packageCode = "All";
                string pathFile = "Book";
                DateTime dateNow = Program.GetServerDateTime();
                string strDateTime = dateNow.ToString("yyMMdd") + "_" + dateNow.ToString("HHmmss");

                int tpr_id = Program.CurrentRegis.tpr_id;
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi tpr = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

                    EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
                    mst_hpc_site mhs = mst.GetMstHpcSite(tpr.mhs_id);
                    mst_room_hdr mrm_book = mst.GetMstRoomHdr("BK", mhs.mhs_code);
                    mst_event mvt_book = mst.GetMstEvent("BK");

                    trn_patient_queue tps =
                        tpr.trn_patient_queues.Where(x => x.mrm_id == mrm_book.mrm_id).FirstOrDefault();

                    if (tps == null)
                    {
                        tps = new trn_patient_queue();
                        tpr.trn_patient_queues.Add(tps);
                    }
                    //tps.mrd_id = mrd_finish.mrd_id;
                    tps.mrm_id = mrm_book.mrm_id;
                    tps.mvt_id = mvt_book.mvt_id;
                    tps.tps_bm_seq = null;
                    tps.tps_end_date = dateNow;
                    tps.tps_call_by = Program.CurrentUser.mut_username;
                    tps.tps_call_date = dateNow;
                    tps.tps_call_status = null;
                    tps.tps_update_by = Program.CurrentUser.mut_username;
                    tps.tps_update_date = dateNow;
                    tps.tps_ns_status = null;
                    tps.tps_status = "ED";


                    trn_patient_book tpb = tpr.trn_patient_books.Where(x => x.tpb_language == langueue
                                                                            && x.tpb_package_code == packageCode &&
                                                                            x.tpb_type == bookType).FirstOrDefault();

                    //bool exportComplete = false;
                    string fileName = langueue + "_" + bookType + "_" + tpb.tpb_hn_no + "_" + tpb.tpb_en_no + "_" +
                                      strDateTime + ".pdf";
                    string pathDes = @"\\" + PrePareData.StaticDataCls.ServerReport + @"\" + pathFile;
                    string oldFileName = (tpb != null) ? tpb.tpb_file_name : null;
                    string rptCode = "";

                    if (rdBookColor.Checked)
                    {
                        if (rdPrtTH.Checked)
                        {
                            rptCode = "BK306";
                        }
                        else if (rdPrtEN.Checked)
                        {
                            rptCode = "BK307";
                        }
                    }
                    if (rdBook.Checked)
                    {
                        if (rdPrtTH.Checked)
                        {
                            rptCode = "BK301";
                            //exportComplete = ClsReport.pdfBookToServer("BK301", pathDes, fileName, oldFileName);
                        }
                        else if (rdPrtEN.Checked)
                        {
                            rptCode = "BK304";
                            //exportComplete = ClsReport.pdfBookToServer("BK304", pathDes, fileName, oldFileName);
                        }
                    }
                    else if (rdOnePage.Checked)
                    {
                        if (rdPrtTH.Checked)
                        {
                            rptCode = "BK302";
                            //exportComplete = ClsReport.pdfBookOnePageTHToServer(pathDes, fileName, oldFileName);
                        }
                        else if (rdPrtEN.Checked)
                        {
                            rptCode = "BK303";
                            //exportComplete = ClsReport.pdfBookOnePageENToServer(pathDes, fileName, oldFileName);
                        }
                        else if (rdPrtJP.Checked)
                        {
                            rptCode = "BK311";
                        }
                    }
                    else if (rdOnePageJMS.Checked)
                    {
                        rptCode = "BK311";
                    }
                    try
                    {
                        if (Directory.Exists(pathDes))
                        {
                            Report.frmPreviewReport frm = new Report.frmPreviewReport(Program.CurrentRegis.tpr_id,
                                new List<string> { rptCode });
                            ReportDocument rptDoc = frm.getReportDoc();
                            if (rptDoc != null)
                            {
                                string desPath = pathDes + @"\" + fileName;
                                if (oldFileName != null)
                                {
                                    string oldDesPath = pathDes + @"\" + oldFileName;
                                    if (File.Exists(oldDesPath))
                                    {
                                        File.Delete(oldDesPath);
                                    }
                                }

                                rptDoc.ExportToDisk(ExportFormatType.PortableDocFormat, desPath);
                                tpb.tpb_status = "BC";
                                tpb.tpb_confirm_by = Program.CurrentUser.mut_username;
                                tpb.tpb_confirm_date = dateNow;
                                tpb.tpb_file_name = fileName;
                                tpb.tpb_path_file = pathFile;
                                tpb.tpb_server_path = PrePareData.StaticDataCls.ServerReport; // same server rerport
                                tpb.tpb_update_by = Program.CurrentUser.mut_username;
                                tpb.tpb_update_date = Program.GetServerDateTime();
                                cdc.SubmitChanges();
                                lbAlert.Text = "Export " +
                                    (rdBookColor.Checked ? "Book Color" : rdBook.Checked ? "Book" : rdOnePage.Checked ? "One Page" : rdOnePageJMS.Checked ? "One Page JMS" : "") +
                                    (rdPrtTH.Checked ? "(TH)" : rdPrtEN.Checked ? "(EN)" : rdPrtJP.Checked ? "(JP)" : "") +
                                               " to PDF file Success.";

                                var result =
                                    sourceGridSearch.Where(x => x.RefID == Program.CurrentRegis.tpr_id).FirstOrDefault();
                                if (result != null)
                                {
                                    result.Status = "ตรวจสอบแล้ว";
                                }
                            }
                        }
                    }
                    catch
                    {
                        lbAlert.Text = "Export " + (rdBookColor.Checked ? "Book Color" : rdBook.Checked ? "Book" : rdOnePage.Checked ? "One Page" : rdOnePageJMS.Checked ? "One Page JMS" : "") +
                                       (rdPrtTH.Checked ? "(TH)" : rdPrtEN.Checked ? "(EN)" : rdPrtJP.Checked ? "(JP)" : "") +
                                       " to PDF file Unsuccess.";
                    }
                    disableButton(tpb);
                }

                //if (exportComplete == true)
                //{
                //    tpb.tpb_status = "BC";
                //    tpb.tpb_confirm_by = Program.CurrentUser.mut_username;
                //    tpb.tpb_confirm_date = dateTime;
                //    tpb.tpb_file_name = fileName;
                //    tpb.tpb_path_file = pathFile;
                //    tpb.tpb_server_path = Program.serverReportIP; // same server rerport
                //    tpb.tpb_update_by = Program.CurrentUser.mut_username;
                //    tpb.tpb_update_date = Program.GetServerDateTime();
                //    dbc.SubmitChanges();
                //    lbAlert.Text = "Export " + (rdBook.Checked ? "Book" : rdOnePage.Checked ? "One Page" : "") + (rdPrtTH.Checked ? "(TH)" : rdPrtEN.Checked ? "(EN)" : "") + " to PDF file Success.";
                //}
                //else
                //{
                //    lbAlert.Text = "Export " + (rdBook.Checked ? "Book" : rdOnePage.Checked ? "One Page" : "") + (rdPrtTH.Checked ? "(TH)" : rdPrtEN.Checked ? "(EN)" : "") + " to PDF file Unsuccess.";
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            APIBackGround();
            


            this.Cursor = Cursors.Arrow;
        }

        private void APIBackGround()
        {
            Thread thread = new Thread(new ThreadStart(APIEHealthBook));
            thread.IsBackground = true;
            thread.Start();
        }
        private void APIEHealthBook()
        {
            //add suriya 17/12/2014
            try
            {
                string serviceURL = ConfigurationManager.AppSettings["EBookServiceURLBook"];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format("{0}", serviceURL)));
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    var tprID = string.Format("('tpr_id':'{0}')", Program.CurrentRegis.tpr_id.ToString());
                    tprID = tprID.Replace('(', '{');
                    tprID = tprID.Replace(')', '}');

                    var x = new InterfaceRequest { tpr_id = Convert.ToInt32(Program.CurrentRegis.tpr_id.ToString()) };
                    var ccc = Newtonsoft.Json.JsonConvert.SerializeObject(x);

                    streamWriter.Write(ccc);
                    streamWriter.Flush();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var streanReader = new StreamReader(response.GetResponseStream()))
                {
                    var result = streanReader.ReadToEnd();

                    if (result.Contains("Code") && result.Contains("0000"))
                    {
                        //MessageBox.Show("Success.");
                    }
                    else
                        //throw new Exception("Send interface failed.");
                        Program.MessageError(this.Name, "APIEHealthBook", "Send interface failed.", false);
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Program.MessageError(this.Name, "APIEHealthBook", ex, false);
            }
            //end suriya 17/12/2014
        }

        public class InterfaceRequest //add suriya 17/12/2014
        {
            public int tpr_id { get; set; }
        }

        private void btnPrtSticker_Click(object sender, EventArgs e)
        {
            using (PatientBookAddressFrm frm = new PatientBookAddressFrm())
            {
                frm.FlagName = bookAddressUC1.FlagName;
                frm.PatientNameTH = bookAddressUC1.PatientNameTH;
                frm.PatientNameEN = bookAddressUC1.PatientNameEN;
                frm.ContactPersonName = bookAddressUC1.ContactPersonName;
                frm.FlagAddress = bookAddressUC1.FlagAddress;
                frm.PatientAddress = bookAddressUC1.PatientAddress;
                frm.CompanyAddress = bookAddressUC1.CompanyAddress;
                frm.OtherAddress = bookAddressUC1.OtherAddress;
                frm.username = Program.CurrentUser.mut_username;
                frm.tpr_id = Program.CurrentRegis.tpr_id;
                frm.ShowDialog();
            }
            //ClsReport.previewWristbandRpt("RG120");
        }

        private void rdPrtTH_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPrtTH.Checked)
            {
                disableButton(null);
                if (Program.CurrentRegis != null)
                {
                    if (rdPrtTH.Checked == true) Program.flagLanguageToEditPE = "TH";
                    string langueue = (rdOnePageJMS.Checked ? "JP" : rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";
                    trn_patient_book tpb = Program.CurrentRegis.trn_patient_books.Where(x => x.tpb_language == langueue &&
                                                                                             x.tpb_package_code == packageCode &&
                                                                                             x.tpb_type == bookType)
                        .FirstOrDefault();
                    disableButton(tpb);
                }
            }
        }

        private void rdPrtEN_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPrtEN.Checked)
            {
                disableButton(null);
                if (Program.CurrentRegis != null)
                {
                    if (rdPrtTH.Checked == true) Program.flagLanguageToEditPE = "EN";
                    string langueue = (rdOnePageJMS.Checked ? "JP" : rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";
                    trn_patient_book tpb = Program.CurrentRegis.trn_patient_books.Where(x => x.tpb_language == langueue
                                                                                             &&
                                                                                             x.tpb_package_code ==
                                                                                             packageCode &&
                                                                                             x.tpb_type == bookType)
                        .FirstOrDefault();
                    disableButton(tpb);
                }
            }
        }

        private void rdPrtJP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPrtJP.Checked)
            {
                disableButton(null);
                if (Program.CurrentRegis != null)
                {
                    if (rdPrtTH.Checked == true) Program.flagLanguageToEditPE = "EN";
                    string langueue = (rdOnePageJMS.Checked ? "JP" : rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";
                    trn_patient_book tpb = Program.CurrentRegis.trn_patient_books.Where(x => x.tpb_language == langueue &&
                                                                                             x.tpb_package_code == packageCode &&
                                                                                             x.tpb_type == bookType)
                        .FirstOrDefault();
                    disableButton(tpb);
                }
            }
        }

        private void rdBookColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBookColor.Checked)
            {
                rdPrtTH.Checked = true;
                groupBox8.Enabled = true;
                disableButton(null);
                if (Program.CurrentRegis != null)
                {
                    string langueue = (rdOnePageJMS.Checked ? "JP" : rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";
                    trn_patient_book tpb = Program.CurrentRegis.trn_patient_books.Where(x => x.tpb_language == langueue &&
                                                                                             x.tpb_package_code == packageCode &&
                                                                                             x.tpb_type == bookType)
                        .FirstOrDefault();
                    disableButton(tpb);
                    if (rdPrtJP.Checked) rdPrtTH.Checked = true;
                    rdPrtJP.Enabled = rdOnePage.Checked;
                }
            }
        }

        private void rdBook_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBook.Checked)
            {
                rdPrtTH.Checked = true;
                groupBox8.Enabled = true;
                disableButton(null);
                if (Program.CurrentRegis != null)
                {
                    string langueue = (rdOnePageJMS.Checked ? "JP" : rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";
                    trn_patient_book tpb = Program.CurrentRegis.trn_patient_books.Where(x => x.tpb_language == langueue &&
                                                                                             x.tpb_package_code == packageCode &&
                                                                                             x.tpb_type == bookType)
                        .FirstOrDefault();
                    disableButton(tpb);
                    if (rdPrtJP.Checked) rdPrtTH.Checked = true;
                    rdPrtJP.Enabled = rdOnePage.Checked;
                }
            }
        }

        private void rdOnePage_CheckedChanged(object sender, EventArgs e)
        {
            if (rdOnePage.Checked)
            {
                rdPrtTH.Checked = true;
                groupBox8.Enabled = true;
                disableButton(null);
                if (Program.CurrentRegis != null)
                {
                    string langueue = (rdOnePageJMS.Checked ? "JP" : rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";
                    trn_patient_book tpb = Program.CurrentRegis.trn_patient_books.Where(x => x.tpb_language == langueue &&
                                                                                             x.tpb_package_code == packageCode &&
                                                                                             x.tpb_type == bookType)
                        .FirstOrDefault();
                    disableButton(tpb);
                    if (rdPrtJP.Checked) rdPrtTH.Checked = true;
                    rdPrtJP.Enabled = rdOnePage.Checked;
                }
            }
        }

        private void rdOnePageJMS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdOnePageJMS.Checked)
            {
                rdPrtJP.Checked = true;
                groupBox8.Enabled = false;
                disableButton(null);
                if (Program.CurrentRegis != null)
                {
                    string langueue = (rdOnePageJMS.Checked ? "JP" : rdPrtTH.Checked ? "TH" : rdPrtEN.Checked ? "EN" : rdPrtJP.Checked ? "JP" : "");
                    string bookType = (rdBookColor.Checked ? "BC" : rdBook.Checked ? "BK" : rdOnePage.Checked ? "OP" : rdOnePageJMS.Checked ? "OJ" : "");
                    string packageCode = "All";
                    trn_patient_book tpb = Program.CurrentRegis.trn_patient_books.Where(x => x.tpb_language == langueue &&
                                                                                             x.tpb_package_code == packageCode &&
                                                                                             x.tpb_type == bookType)
                        .FirstOrDefault();
                    disableButton(tpb);
                    //rdPrtJP.Enabled = rdOnePage.Checked;
                }
            }
        }

        private void btnReturnToPE_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    dbc.trn_patient_regis.Where(x => x.tpr_id == Program.CurrentRegis.tpr_id)
                        .FirstOrDefault()
                        .tpr_pe_status = "NR";
                    dbc.SubmitChanges();
                }
                btnSearch_Click(null, null);
            }
        }

        #region package waiting for confirm

        private void cbPackageName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cbPackageName.Text != "=> Select PackageName")
            //    {
            //        if (eventChk == 1 || eventChk == 3)
            //        {
            //            if (RefIDs != null)
            //            {
            //                var undoPackageNames = dbc.trn_patient_regis.Where(w => w.tpr_id == RefIDs).SingleOrDefault();
            //                undoPackageName = (undoPackageNames != null) ? undoPackageNames.mhc_id.ToString() : null;

            //                trn_patient_regi updPackageName = new trn_patient_regi();
            //                updPackageName = dbc.trn_patient_regis.Where(w => w.tpr_id == RefIDs).FirstOrDefault();
            //                updPackageName.mhc_id = Convert.ToInt32(cbPackageName.SelectedValue);
            //                updPackageName.tpr_update_by = Program.CurrentUser.mut_username;
            //                updPackageName.tpr_update_date = Program.GetServerDateTime();
            //                dbc.SubmitChanges();
            //            }
            //            else
            //            {
            //                MessageBox.Show(" Debug check => cbPackageName(Update PackageName) : RefIDs is null. ");
            //            }
            //        }

            //        eventChk = 3;
            //    }
            //    else
            //    {
            //        MessageBox.Show(" Debug check => cbPackageName(Update PackageName) : Please select PackageName. ");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

        #endregion

        #region object for bind datagrid

        private BindingList<memberAddition> memAddition = new BindingList<memberAddition>();

        private class memberAddition
        {
            public int cAddItem_RefID
            {
                get { return 0; }
            }

            private int _cNo;

            public int cNo
            {
                get { return _cNo; }
                set { _cNo = value; }
            }

            private string _cItem;

            public string cItem
            {
                get { return _cItem; }
                set { _cItem = value; }
            }

            public string cDel
            {
                get { return "Del"; }
            }
        }

        private BindingList<memberTransection> memTransection = new BindingList<memberTransection>();

        private class memberTransection
        {
            public int transIDs
            {
                get { return 0; }
            }

            private int _NoTrans;

            public int NoTrans
            {
                get { return _NoTrans; }
                set { _NoTrans = value; }
            }

            private string _Event;

            public string Event
            {
                get { return _Event; }
                set { _Event = value; }
            }

            private string _Transaction;

            public string Transaction
            {
                get { return _Transaction; }
                set { _Transaction = value; }
            }

            private DateTime _Date;

            public DateTime Date
            {
                get { return _Date; }
                set { _Date = value; }
            }

            private TimeSpan _Time;

            public TimeSpan Time
            {
                get { return _Time; }
                set { _Time = value; }
            }

            public string Delete
            {
                get { return "Del"; }
            }

            private int? _mbe_id;

            public int? mbe_id
            {
                get { return _mbe_id; }
                set { _mbe_id = value; }
            }

            private DateTime? _tktn_date;

            public DateTime? tktn_date
            {
                get { return _tktn_date; }
                set { _tktn_date = value; }
            }
        }

        private BindingList<memberPackage> memPackage = new BindingList<memberPackage>();

        private class memberPackage
        {
            private Boolean _check;

            public Boolean check
            {
                get { return _check; }
                set { _check = value; }
            }

            private string _PackageID;

            public string PackageID
            {
                get { return _PackageID; }
                set { _PackageID = value; }
            }

            private string _PackageName;

            public string PackageName
            {
                get { return _PackageName; }
                set { _PackageName = value; }
            }

            private string _PackageCode;

            public string PackageCode
            {
                get { return _PackageCode; }
                set { _PackageCode = value; }
            }
        }

        #endregion

        #region check box in tab package

        private void checkbox_addition(object sender, EventArgs e)
        {

        }

        private void removeMemberAddition(List<memberAddition> _memberAddition)
        {
            if (_memberAddition != null)
            {
                foreach (memberAddition ma in _memberAddition) memAddition.Remove(ma);
                int index = 0;
                foreach (memberAddition ma in memAddition) ma.cNo = index += 1;
            }
        }

        #endregion

        #region addCombo

        private void getSite(ComboBox cmb)
        {
            try
            {
                var site = dbc.mst_hpc_sites.Where(x => x.mhs_status == 'A').Select(x => new
                {
                    code = x.mhs_id,
                    name = x.mhs_ename
                }).ToList();
                site.Insert(0, new { code = 0, name = "All Location" });
                cmb.ValueMember = "code";
                cmb.DisplayMember = "name";
                cmb.DataSource = site;
            }
            catch
            {

            }
        }

        private void getEvent(ComboBox cmb)
        {
            try
            {
                var evnt = dbc.mst_book_events.Select(x => new
                {
                    code = x.mbe_id,
                    name = x.mbe_tname
                }).ToList();
                evnt.Insert(0, new { code = 0, name = "Select Event Transaction" });
                cmb.ValueMember = "code";
                cmb.DisplayMember = "name";
                cmb.DataSource = evnt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void getPackage(ComboBox cmb)
        {
            var pack = (from mhc in dbc.mst_health_checkups
                        where mhc.mhc_status == 'A'
                        select new
                        {
                            code = mhc.mhc_id,
                            name = mhc.mhc_tname
                        }).ToList();
            pack.Insert(0, new { code = 0, name = "Select Package Name" });
            cmb.ValueMember = "code";
            cmb.DisplayMember = "name";
            cmb.DataSource = pack;


        }

        //private void getAddDate(ComboBox cmb)
        //{
        //    var source = (new []
        //    {
        //        new { code = 0, name =  "" },
        //        new { code = 7, name =  "1 Week" },
        //        new { code = 365, name =  "1 Month" },
        //        new { code = 365, name =  "1 Year" }
        //    }).ToList();
        //    cmb.DataSource = source;
        //    cmb.ValueMember = "code";
        //    cmb.DisplayMember = "name";
        //}

        #endregion addCombo

        #region SearchPatienRegis

        private class gridSearch
        {
            public int RefID { get; set; }
            public string HN { get; set; }
            public string FullName { get; set; }
            public string Status { get; set; }
            public string statusCode { get; set; }
            public string reason { get; set; }
            public bool express { get; set; }
        }

        BindingList<gridSearch> sourceGridSearch;
        BindingSource bsGridSearch;

        private void searchPatiant()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string HN = string.IsNullOrEmpty(txtHN.Text) ? string.Empty : txtHN.Text.Trim().Replace("-", "");
                    string Name = string.IsNullOrEmpty(txtName.Text) ? string.Empty : txtName.Text.Trim().ToUpper();
                    string SurName = string.IsNullOrEmpty(txtSurName.Text) ? string.Empty : txtSurName.Text.Trim().ToUpper();
                    int LocationID = Convert.ToInt32(cobLocation.SelectedValue);
                    string sendBook = (chkSendBook.Checked) ? "1" : "";
                    bool approveStatus = chkApproved.Checked;
                    bool nonApproveStatus = chkNonApproved.Checked;
                    bool rejectStatus = chkReject.Checked;
                    
                    mst_event mvt_book = new EmrClass.GetDataMasterCls().GetMstEvent("BK");

                    List<gridSearch> result = cdc.trn_patient_regis
                        .Where(x => x.trn_patient.tpt_hn_no.Replace("-", "").Contains(HN) &&
                                    x.trn_patient.tpt_first_name.ToUpper().Contains(Name) &&
                                    x.trn_patient.tpt_last_name.ToUpper().Contains(SurName) &&
                                    (LocationID == 0 ? true : x.mhs_id == LocationID) &&
                                    (!chkDate.Checked ? true : (x.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date == txtVDate.Value.Date)) &&
                                    (chkSendBook.Checked ? (x.tpr_send_book == '1') : true) &&
                                    (chkApproved.Checked ? (x.trn_patient_doctor_approve.tpda_status == "APD") : true) &&
                                    (chkNonApproved.Checked ? ((new string[] { "WFA", "API" }).Contains(x.trn_patient_doctor_approve.tpda_status)) : true) &&
                                    (chkReject.Checked ? (x.trn_patient_doctor_approve.tpda_status == "NAP") : true) &&
                                    x.tpr_status == "WB" &&
                                    x.tpr_pe_status == "RS").Take(200)
                        .Select(x => new gridSearch
                        {
                            RefID = x.tpr_id,
                            HN = x.trn_patient.tpt_hn_no,
                            FullName = x.trn_patient.tpt_othername,
                            Status = x.trn_patient_doctor_approve != null
                                ? (x.trn_patient_doctor_approve.tpda_status == "WFA"
                                    ? "รอแพทย์ Approve"
                                    : x.trn_patient_doctor_approve.tpda_status == "APD"
                                        ? "Approved."
                                        : x.trn_patient_doctor_approve.tpda_status == "API"
                                            ? "แพทย์กำลัง Approve"
                                            : x.trn_patient_doctor_approve.tpda_status == "NAP"
                                                ? "แพทย์ให้แก้ไข"
                                                : "พิมพ์และตรวจสอบผล")
                                : x.tpr_status == null || x.tpr_status == "PD"
                                    ? "รอแพทย์สรุปผล"
                                    : x.trn_patient_queues.Any(y => y.mvt_id == mvt_book.mvt_id && y.tps_status == "ED")
                                        ? "ตรวจสอบแล้ว"
                                        : x.tpr_status == "WB"
                                            ? "พิมพ์และตรวจสอบผล"
                                            : x.tpr_status == "WP"
                                                ? "รอพิมพ์ผล"
                                                : x.tpr_status == "CP" ? "เสร็จเรียบร้อย" : "",
                            statusCode = x.trn_patient_doctor_approve.tpda_status,
                            reason = x.trn_patient_doctor_approve.tpda_reject_reason,
                            express = !(x.tpr_send_book == null || x.tpr_send_book != '1')
                        }).ToList();
                    sourceGridSearch = new BindingList<gridSearch>(result);
                    bsGridSearch = new BindingSource();
                    bsGridSearch.DataSource = sourceGridSearch;
                    gvSearch.DataSource = bsGridSearch;
                    gvSearch_SetBtnReason();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("frmBookResult", "searchPatient", ex, false);
            }
        }

        #endregion

        #region SelectPatienRegis

        private void disableButton(trn_patient_book tpb)
        {
            btnReturnToPE.Enabled = false;
            btnPrtSticker.Enabled = false;
            btnSave.Enabled = false;
            btnPrtPreview.Enabled = false;
            //chkBookPrintComplete.Checked = false;
            //chkBookPrintComplete.Enabled = false;
            btnPrtNewBook.Enabled = false; // newbook
            btnConfirm.Enabled = false;

            if (Program.CurrentRegis != null)
            {
                btnPrtSticker.Enabled = true;
                btnReturnToPE.Enabled = true; // newbook

                if (tpb != null)
                {
                    btnPrtPreview.Enabled = true;
                    btnPrtNewBook.Enabled = true;
                    switch (tpb.tpb_status)
                    {
                        case "NB": // not book
                            btnSave.Enabled = true;
                            btnPrtPreview.Enabled = true;
                            btnPrtNewBook.Enabled = true; // newbook
                            btnSendDoctorApprove.Enabled = true;
                            //chkBookPrintComplete.Checked = false;
                            //chkBookPrintComplete.Enabled = true;
                            btnConfirm.Enabled = true;
                            break;
                        case "BC": // book confirm
                            btnSave.Enabled = true;
                            btnPrtPreview.Enabled = true;
                            btnPrtNewBook.Enabled = true; // newbook
                            btnSendDoctorApprove.Enabled = false;
                            //chkBookPrintComplete.Checked = false;
                            //chkBookPrintComplete.Enabled = false;
                            btnConfirm.Enabled = true;
                            break;
                        case "BP": // book print complete
                            btnSave.Enabled = false;
                            btnPrtPreview.Enabled = true;
                            btnPrtNewBook.Enabled = true; // newbook
                            btnSendDoctorApprove.Enabled = false;
                            //chkBookPrintComplete.Checked = true;
                            //chkBookPrintComplete.Enabled = false;
                            btnConfirm.Enabled = false;
                            break;
                        case "BF": // book finish
                            btnSave.Enabled = false;
                            btnPrtPreview.Enabled = true;
                            btnPrtNewBook.Enabled = true; // newbook
                            btnSendDoctorApprove.Enabled = false;
                            //chkBookPrintComplete.Checked = true;
                            //chkBookPrintComplete.Enabled = false;
                            btnConfirm.Enabled = false;
                            break;
                    }
                }
                else
                {
                    btnSave.Enabled = true;
                    btnPrtPreview.Enabled = false;

                    btnSendDoctorApprove.Enabled = false;
                    //chkBookPrintComplete.Checked = false;
                    //chkBookPrintComplete.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnPrtPreview.Enabled = false;
                    btnPrtNewBook.Enabled = false; // newbook
                }
            }
        }

        private void showPatient(int? tpr_id)
        {
            lbAlert.Text = "";
            if (tpr_id == null)
            {
                this.PatientRegis = null;
                Program.CurrentRegis = null;
                Program.CurrentHDR = null;
                autoCompleteUC1.SelectedValue = null;
                autoCompleteUC1.Enabled = false;
            }
            else
            {
                dbc = new InhCheckupDataContext();
                this.PatientRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                Program.CurrentRegis = this.PatientRegis;
                Program.CurrentHDR = Program.CurrentRegis != null
                    ? Program.CurrentRegis.trn_doctor_hdrs.FirstOrDefault()
                    : null;

                if (_PatientRegis.trn_patient_doctor == null)
                {
                    _PatientRegis.trn_patient_doctor = new trn_patient_doctor();
                }
                _PatientRegis.trn_patient_doctor.PropertyChanged -=
                    new PropertyChangedEventHandler(trn_patient_doctor_PropertyChanged);
                _PatientRegis.trn_patient_doctor.PropertyChanged +=
                    new PropertyChangedEventHandler(trn_patient_doctor_PropertyChanged);
                trn_patient_doctor_PropertyChanged(_PatientRegis.trn_patient_doctor,
                    new PropertyChangedEventArgs("tpd_doctor_code"));
                autoCompleteUC1.Enabled = true;
            }

            if (Program.CurrentSite.mhs_code == "01JMSCK")
            {
                rdOnePageJMS.Checked = true;
            }
            else
            {
                rdBookColor.Checked = false;
                rdBookColor.Checked = true;
            }

            if (Program.CurrentRegis != null)
            {
                btnRetrieveVS.Enabled = true;
                btnRetreiveLab.Enabled = true;
                btnAssessment.Enabled = true;
                RetrieveXrayBtn.Enabled = true;
                //disableButton(
                //    Program.CurrentRegis.trn_patient_books.OrderByDescending(x => x.tpb_update_date).FirstOrDefault());
            }
            else
            {
                btnRetrieveVS.Enabled = false;
                btnRetreiveLab.Enabled = false;
                btnAssessment.Enabled = false;
                RetrieveXrayBtn.Enabled = false;
                disableButton(null);
            }
            showDataInformation();
            LoadInvestigation();

            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                string statusApprove =
                    cdc.trn_patient_doctor_approves.Where(x => x.tpr_id == tpr_id)
                        .Select(x => x.tpda_status)
                        .FirstOrDefault();
                switch (statusApprove)
                {
                    case "APD":
                    case "API":
                        btnSendDoctorApprove.Enabled = false;
                        btnRecallDoctorApprove.Enabled = false;
                        panel4.Enabled = false;
                        panel7.Enabled = false;
                        groupBox1.Enabled = false;
                        panel9.Enabled = false;
                        groupBox8.Enabled = false;
                        btnSave.Enabled = false;

                        //tabControl3.Enabled = false;
                        bookAddressUC1.Enabled = false;
                        additionalItemUC1.Enabled = false;
                        tabBasicMeasurementUC1.Enabled = false;
                        bookTransaction1.Enabled = false;
                        break;
                    case "WFA":
                        btnSendDoctorApprove.Enabled = false;
                        btnRecallDoctorApprove.Enabled = true;
                        panel4.Enabled = false;
                        panel7.Enabled = false;
                        groupBox1.Enabled = false;
                        panel9.Enabled = false;
                        groupBox8.Enabled = false;
                        btnSave.Enabled = false;

                        //tabControl3.Enabled = false;
                        bookAddressUC1.Enabled = false;
                        additionalItemUC1.Enabled = false;
                        tabBasicMeasurementUC1.Enabled = false;
                        bookTransaction1.Enabled = false;
                        break;
                    default:
                        btnRecallDoctorApprove.Enabled = false;
                        panel4.Enabled = true;
                        panel7.Enabled = true;
                        groupBox1.Enabled = true;
                        panel9.Enabled = true;
                        groupBox8.Enabled = true;
                        btnSave.Enabled = true;

                        //tabControl3.Enabled = true;
                        bookAddressUC1.Enabled = true;
                        additionalItemUC1.Enabled = true;
                        tabBasicMeasurementUC1.Enabled = true;
                        bookTransaction1.Enabled = true;
                        break;
                }
            }
        }

        private void showDataInformation()
        {
            lblHN.Text = "";
            lblFullName.Text = "";
            lblEN.Text = "";
            lblVisitDate.Text = "";
            lblDOB.Text = "";
            lblAge.Text = "";
            lblGender.Text = "";
            lblAddress.Text = "";
            pictureBox1.Image = DefaultImageProfile;

            try
            {
                trn_patient_regi tpr = Program.CurrentRegis;
                if (tpr != null)
                {
                    lblHN.Text = tpr.trn_patient.tpt_hn_no;
                    lblFullName.Text = Program.getFullName(Program.CurrentRegis.tpr_id);
                    lblEN.Text = tpr.tpr_en_no;
                    lblVisitDate.Text = tpr.trn_patient_regis_detail.tpr_real_arrived_date == null
                        ? "-"
                        : Program.GetFormattedString(tpr.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date);

                    lblDOB.Text = (tpr.trn_patient.tpt_dob.Value.ToString() == null)
                        ? "-"
                        : Program.GetFormattedString(tpr.trn_patient.tpt_dob.Value.Date);
                    lblAge.Text =
                        Program.CalculateAge(tpr.trn_patient.tpt_dob.Value, Program.GetServerDateTime()).ToString();
                    lblGender.Text = (tpr.trn_patient.tpt_gender == null)
                        ? "-"
                        : (tpr.trn_patient.tpt_gender == 'M') ? "Male" : "Female";
                    lblAddress.Text = (tpr.tpr_other_address == null) ? "-" : tpr.tpr_other_address.ToString();
                    pictureBox1.Image = Program.byteArrayToImage(tpr.trn_patient.tpt_image.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Investigetion Cr.Noina

        private void SetRadiologyPhyExam(int? tpr_id)
        {
            try
            {
                rlgPhyEx.ClearButtonRadiology();
                Usercontrols.RadiologyUC.BtnRadiology PhyExamBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                PhyExamBtn.StatusSaved = ChkSaveRad(tpr_id, "PE");
                PhyExamBtn.btnRadiologyClick +=
                    new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(PhyExamBtn_btnRadiologyClick);
                PhyExamBtn.tooltipText = "Physical Exam";
                rlgPhyEx.AddButtonRadiology(PhyExamBtn);

            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyPhyExam()", ex, false);
            }
        }

        private void PhyExamBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogPhysicalExam dialog = new DialogPhysicalExam())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyPhyExam(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyABI(int? tpr_id)
        {
            try
            {
                rlgABI.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string abi = cdc.trn_patient_events
                                    .Where(x => x.tpr_id == tpr_id &&
                                                x.mst_event.mvt_code == "AB")
                                    .Select(x => x.mst_event.mvt_ename)
                                    .FirstOrDefault();
                    if (!string.IsNullOrEmpty(abi))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology ABIbtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        ABIbtn.StatusSaved = ChkSaveRad(tpr_id, "AB");
                        ABIbtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(ABIbtn_btnRadiologyClick);
                        ABIbtn.tooltipText = "ABI";
                        rlgABI.AddButtonRadiology(ABIbtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyABI()", ex, false);
            }
        }
        private void ABIbtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogABI dialog = new DialogABI())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                try
                {
                    dbc.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, _PatientRegis.trn_patient_doctor);
                    trn_patient_doctor_PropertyChanged(_PatientRegis.trn_patient_doctor, new PropertyChangedEventArgs("tpd_doctor_code"));
                }
                catch
                { }
                SetRadiologyABI(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyHearing(int? tpr_id)
        {
            try
            {
                rlgHearing.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string hearing = cdc.trn_patient_events
                                        .Where(x => x.tpr_id == tpr_id &&
                                                    x.mst_event.mvt_code == "HS")
                                        .Select(x => x.mst_event.mvt_ename)
                                        .FirstOrDefault();
                    if (!string.IsNullOrEmpty(hearing))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology HearingBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        HearingBtn.StatusSaved = ChkSaveRad(tpr_id, "HS");
                        HearingBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(HearingBtn_btnRadiologyClick);
                        HearingBtn.tooltipText = hearing;
                        rlgHearing.AddButtonRadiology(HearingBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyHearing()", ex, false);
            }
        }
        private void HearingBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogAudio dialog = new DialogAudio())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyHearing(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyEyes(int? tpr_id)
        {
            try
            {
                rlgEyes.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string eyes = cdc.trn_patient_events
                                     .Where(x => x.tpr_id == tpr_id &&
                                                 x.mst_event.mvt_code == "EM")
                                     .Select(x => x.mst_event.mvt_ename)
                                     .FirstOrDefault();
                    if (!string.IsNullOrEmpty(eyes))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology EyesBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        EyesBtn.StatusSaved = ChkSaveRad(tpr_id, "EM");
                        EyesBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(EyesBtn_btnRadiologyClick);
                        EyesBtn.tooltipText = eyes;
                        rlgEyes.AddButtonRadiology(EyesBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyEyes()", ex, false);
            }
        }
        private void EyesBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogEyes dialog = new DialogEyes())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyEyes(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyDental(int? tpr_id)
        {
            try
            {
                rlgDental.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string teeh = cdc.trn_patient_events
                                     .Where(x => x.tpr_id == tpr_id &&
                                                 x.mst_event.mvt_code == "TE")
                                     .Select(x => x.mst_event.mvt_ename)
                                     .FirstOrDefault();
                    if (!string.IsNullOrEmpty(teeh))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology DentalBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        DentalBtn.StatusSaved = ChkSaveRad(tpr_id, "TE");
                        DentalBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(DentalBtn_btnRadiologyClick);
                        DentalBtn.tooltipText = teeh;
                        rlgDental.AddButtonRadiology(DentalBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyDental()", ex, false);
            }
        }
        private void DentalBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogDental dialog = new DialogDental())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyDental(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyEcho(int? tpr_id)
        {
            try
            {
                rlgEcho.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string echo = cdc.trn_patient_events
                                     .Where(x => x.tpr_id == tpr_id &&
                                                 x.mst_event.mvt_code == "EC")
                                     .Select(x => x.mst_event.mvt_ename)
                                     .FirstOrDefault();
                    if (!string.IsNullOrEmpty(echo))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology EchoBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        EchoBtn.StatusSaved = ChkSaveRad(tpr_id, "EC");
                        EchoBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(EchoBtn_btnRadiologyClick);
                        EchoBtn.tooltipText = echo;
                        rlgEcho.AddButtonRadiology(EchoBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyEcho()", ex, false);
            }
        }
        private void EchoBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogEcho dialog = new DialogEcho())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyEcho(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyEKG(int? tpr_id)
        {
            try
            {
                rlgEKG.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string ekg = cdc.trn_patient_events
                                    .Where(x => x.tpr_id == tpr_id &&
                                                x.mst_event.mvt_code == "EK")
                                    .Select(x => x.mst_event.mvt_ename)
                                    .FirstOrDefault();
                    if (!string.IsNullOrEmpty(ekg))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology EKGBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        EKGBtn.StatusSaved = ChkSaveRad(tpr_id, "EK");
                        EKGBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(EKGBtn_btnRadiologyClick);
                        EKGBtn.tooltipText = ekg;
                        rlgEKG.AddButtonRadiology(EKGBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyEKG()", ex, false);
            }
        }
        private void EKGBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogEKG dialog = new DialogEKG())//frmPE_DialogEKG dialog = new frmPE_DialogEKG()
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyEKG(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyEST(int? tpr_id)
        {
            try
            {
                rlgEST.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string est = cdc.trn_patient_events
                                     .Where(x => x.tpr_id == tpr_id &&
                                                 x.mst_event.mvt_code == "ES")
                                     .Select(x => x.mst_event.mvt_ename)
                                     .FirstOrDefault();
                    if (!string.IsNullOrEmpty(est))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology ESTBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        ESTBtn.StatusSaved = ChkSaveRad(tpr_id, "ES");
                        ESTBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(ESTBtn_btnRadiologyClick);
                        ESTBtn.tooltipText = "EST";
                        rlgEST.AddButtonRadiology(ESTBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyEST()", ex, false);
            }
        }
        private void ESTBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogEST dialog = new DialogEST())//frmPE_DialogEST dialog = new frmPE_DialogEST()
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyEST(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyXray(int? tpr_id)
        {
            try
            {
                rlgXray.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

                    string xr = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "XR")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(xr))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology XrayBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        XrayBtn.StatusSaved = ChkSaveRad(tpr_id, "XR");
                        XrayBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(XrayBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(XrayBtn);

                        trn_chest_xray chestXray = patientRegis.trn_chest_xrays.Where(x => x.tcx_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.tcx_result_date).FirstOrDefault();
                        if (chestXray != null)
                        {
                            XrayBtn.tooltipText = chestXray.tcx_result;
                        }
                        else
                        {
                            XrayBtn.tooltipText = xr;
                        }
                    }

                    string dm = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "DM")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(dm))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology mamBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        mamBtn.StatusSaved = ChkSaveRad(tpr_id, "DM");
                        mamBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(mamBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(mamBtn);

                        trn_mammogram mam = patientRegis.trn_mammograms.Where(x => x.tmg_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.tmg_result_date).FirstOrDefault();
                        if (mam != null)
                        {
                            mamBtn.tooltipText = mam.tmg_result;
                        }
                        else
                        {
                            mamBtn.tooltipText = dm;
                        }
                    }

                    string uw = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "UW")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(uw))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology uwBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        uwBtn.StatusSaved = ChkSaveRad(tpr_id, "UW");
                        uwBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(uwBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(uwBtn);

                        trn_ultrasound usUW = patientRegis.trn_ultrasounds.Where(x => x.tus_ultra_type == "UW" && x.tus_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.tus_result_date).FirstOrDefault();
                        if (usUW != null)
                        {
                            uwBtn.tooltipText = usUW.tus_result;
                        }
                        else
                        {
                            uwBtn.tooltipText = uw;
                        }
                    }

                    string uu = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "UU")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(uu))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology uuBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        uuBtn.StatusSaved = ChkSaveRad(tpr_id, "UU");
                        uuBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(uuBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(uuBtn);

                        trn_ultrasound usUU = patientRegis.trn_ultrasounds.Where(x => x.tus_ultra_type == "UU" && x.tus_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.tus_result_date).FirstOrDefault();
                        if (usUU != null)
                        {
                            uuBtn.tooltipText = usUU.tus_result;
                        }
                        else
                        {
                            uuBtn.tooltipText = uu;
                        }
                    }

                    string ul = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "UL")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(ul))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology ulBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        ulBtn.StatusSaved = ChkSaveRad(tpr_id, "UL");
                        ulBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(ulBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(ulBtn);

                        trn_ultrasound usUL = patientRegis.trn_ultrasounds.Where(x => x.tus_ultra_type == "UL" && x.tus_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.tus_result_date).FirstOrDefault();
                        if (usUL != null)
                        {
                            ulBtn.tooltipText = usUL.tus_result;
                        }
                        else
                        {
                            ulBtn.tooltipText = ul;
                        }
                    }

                    string ub = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "UB")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(ub))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology ubBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        ubBtn.StatusSaved = ChkSaveRad(tpr_id, "UB");
                        ubBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(ubBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(ubBtn);

                        trn_ultrasound usUB = patientRegis.trn_ultrasounds.Where(x => x.tus_ultra_type == "UB" && x.tus_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.tus_result_date).FirstOrDefault();
                        if (usUB != null)
                        {
                            ubBtn.tooltipText = usUB.tus_result;
                        }
                        else
                        {
                            ubBtn.tooltipText = ub;
                        }
                    }

                    string bd = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "BD")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(bd))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology bmdBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        bmdBtn.StatusSaved = ChkSaveRad(tpr_id, "BD");
                        bmdBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(bmdBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(bmdBtn);

                        trn_bmd bmd = patientRegis.trn_bmds.Where(x => x.bmd_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.bmd_result_date).FirstOrDefault();
                        if (bmd != null)
                        {
                            bmdBtn.tooltipText = bmd.bmd_result;
                        }
                        else
                        {
                            bmdBtn.tooltipText = bd;
                        }
                    }

                    string ug = cdc.trn_patient_events
                                   .Where(x => x.tpr_id == tpr_id &&
                                               x.mst_event.mvt_code == "UG")
                                   .Select(x => x.mst_event.mvt_ename)
                                   .FirstOrDefault();
                    if (!string.IsNullOrEmpty(ug))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology ugiBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        ugiBtn.StatusSaved = ChkSaveRad(tpr_id, "UG");
                        ugiBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(ugiBtn_btnRadiologyClick);
                        rlgXray.AddButtonRadiology(ugiBtn);

                        trn_ugi_xray ugi = patientRegis.trn_ugi_xrays.Where(x => x.tug_en_no == x.trn_patient_regi.tpr_en_no).OrderByDescending(x => x.tug_result_date).FirstOrDefault();
                        if (ugi != null)
                        {
                            ugiBtn.tooltipText = ugi.tug_result;
                        }
                        else
                        {
                            ugiBtn.tooltipText = ug;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyXray(ref trn_patient_regi _patientRegis)", ex, false);
            }
        }
        private void XrayBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                 try
                 {
                     dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Chest X-Ray]";
                     dialog.tpr_id = _PatientRegis.tpr_id;
                     dialog.trxr_type = "XR";
                     dialog.ShowDialog();
                     SetRadiologyXray(_PatientRegis.tpr_id);
                 }
                 catch (Exception ex)
                 {

                 }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    try
            //    {
            //        dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Chest X-Ray]";
            //        dialog.PageTag = "XR";
            //        dialog.ShowDialog();
            //        SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
        }
        private void mamBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                try
                {
                    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Mammogram]";
                    dialog.tpr_id = _PatientRegis.tpr_id;
                    dialog.trxr_type = "DM";
                    dialog.ShowDialog();
                    SetRadiologyXray(_PatientRegis.tpr_id);
                }
                catch (Exception ex)
                {

                }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Mammogram]";
            //    dialog.PageTag = "DM";
            //    dialog.ShowDialog();
            //    SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //}
        }
        private void uwBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                try
                {
                    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [CK-Ultrasound Whole Abdomen(Ps)]";
                    dialog.tpr_id = _PatientRegis.tpr_id;
                    dialog.trxr_type = "UW";
                    dialog.ShowDialog();
                    SetRadiologyXray(_PatientRegis.tpr_id);
                }
                catch (Exception ex)
                {

                }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [CK-Ultrasound Whole Abdomen(Ps)]";
            //    dialog.PageTag = "UW";
            //    dialog.ShowDialog();
            //    SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //}
        }
        private void uuBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                try
                {
                    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Ultrasound Upper]";
                    dialog.tpr_id = _PatientRegis.tpr_id;
                    dialog.trxr_type = "UU";
                    dialog.ShowDialog();
                    SetRadiologyXray(_PatientRegis.tpr_id);
                }
                catch (Exception ex)
                {

                }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Ultrasound Upper]";
            //    dialog.PageTag = "UU";
            //    dialog.ShowDialog();
            //    SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //}
        }
        private void ulBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                try
                {
                    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Ultrasound Lower]";
                    dialog.tpr_id = _PatientRegis.tpr_id;
                    dialog.trxr_type = "UL";
                    dialog.ShowDialog();
                    SetRadiologyXray(_PatientRegis.tpr_id);
                }
                catch (Exception ex)
                {

                }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Ultrasound Lower]";
            //    dialog.PageTag = "UL";
            //    dialog.ShowDialog();
            //    SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //}
        }
        private void ubBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                try
                {
                    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Ultrasound Breast]";
                    dialog.tpr_id = _PatientRegis.tpr_id;
                    dialog.trxr_type = "UB";
                    dialog.ShowDialog();
                    SetRadiologyXray(_PatientRegis.tpr_id);
                }
                catch (Exception ex)
                {

                }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Ultrasound Breast]";
            //    dialog.PageTag = "UB";
            //    dialog.ShowDialog();
            //    SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //}
        }
        private void bmdBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                try
                {
                    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [BMD]";
                    dialog.tpr_id = _PatientRegis.tpr_id;
                    dialog.trxr_type = "BD";
                    dialog.ShowDialog();
                    SetRadiologyXray(_PatientRegis.tpr_id);
                }
                catch (Exception ex)
                {

                }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [BMD]";
            //    dialog.PageTag = "BD";
            //    dialog.ShowDialog();
            //    SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //}
        }
        private void ugiBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogImaging dialog = new DialogImaging())
            {
                try
                {
                    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [UGI]";
                    dialog.tpr_id = _PatientRegis.tpr_id;
                    dialog.trxr_type = "UG";
                    dialog.ShowDialog();
                    SetRadiologyXray(_PatientRegis.tpr_id);
                }
                catch (Exception ex)
                {

                }
            }
            //using (frmReportChestXRay dialog = new frmReportChestXRay())
            //{
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [UGI]";
            //    dialog.PageTag = "UG";
            //    dialog.ShowDialog();
            //    SetRadiologyXray(Program.CurrentRegis.tpr_id);
            //}
        }

        private void SetRadiologyPFT(int? tpr_id)
        {
            try
            {
                rlgPFT.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string pft = cdc.trn_patient_events
                                    .Where(x => x.tpr_id == tpr_id &&
                                                x.mst_event.mvt_code == "PF")
                                    .Select(x => x.mst_event.mvt_ename)
                                    .FirstOrDefault();
                    if (!string.IsNullOrEmpty(pft))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology PftBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        PftBtn.StatusSaved = ChkSaveRad(tpr_id, "PF");
                        PftBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(PftBtn_btnRadiologyClick);
                        PftBtn.tooltipText = "PFT";
                        rlgPFT.AddButtonRadiology(PftBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyPFT()", ex, false);
            }

        }
        private void PftBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogPFT dialog = new DialogPFT())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyPFT(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyCarotid(int? tpr_id)
        {
            try
            {
                rlgCarotid.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string carotid = cdc.trn_patient_events
                                    .Where(x => x.tpr_id == tpr_id &&
                                                x.mst_event.mvt_code == "CD")
                                    .Select(x => x.mst_event.mvt_ename)
                                    .FirstOrDefault();
                    if (!string.IsNullOrEmpty(carotid))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology CarotidBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        CarotidBtn.StatusSaved = ChkSaveRad(tpr_id, "CD");
                        CarotidBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(CarotidBtn_btnRadiologyClick);
                        CarotidBtn.tooltipText = "Carotid";
                        rlgCarotid.AddButtonRadiology(CarotidBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyCarotid()", ex, false);
            }


        }
        private void CarotidBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogCarotid dialog = new DialogCarotid())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyCarotid(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyOtherExam(int? tpr_id)
        {
            try
            {
                rlgOtherExam.ClearButtonRadiology();
                Usercontrols.RadiologyUC.BtnRadiology OtherExamBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                OtherExamBtn.StatusSaved = ChkSaveRad(tpr_id, "OE");
                OtherExamBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(OtherExamBtn_btnRadiologyClick);
                OtherExamBtn.tooltipText = "Other Exam";
                rlgOtherExam.AddButtonRadiology(OtherExamBtn);
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyOtherExam()", ex, false);
            }
        }
        private void OtherExamBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (frmPE_DialogOtherExam dialog = new frmPE_DialogOtherExam())
            {
                dialog.ShowDialog();
                SetRadiologyOtherExam(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyAllExam(int? tpr_id)
        {
            try
            {
                rlgAllExam.ClearButtonRadiology();
                Usercontrols.RadiologyUC.BtnRadiology AllExamBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                AllExamBtn.StatusSaved = ChkSaveRad(tpr_id, "LB");
                AllExamBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(AllExamBtn_btnRadiologyClick);
                AllExamBtn.tooltipText = "Lab";
                rlgAllExam.AddButtonRadiology(AllExamBtn);
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyAllExam()", ex, false);
            }
        }
        private void AllExamBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogAllExam dialog = new DialogAllExam())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyAllExam(Program.CurrentRegis.tpr_id);
            }
        }

        private void SetRadiologyPAP(int? tpr_id)
        {
            try
            {
                rlgPAP.ClearButtonRadiology();
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string pap = cdc.trn_patient_events
                                    .Where(x => x.tpr_id == tpr_id &&
                                                x.mst_event.mvt_code == "PT")
                                    .Select(x => x.mst_event.mvt_ename)
                                    .FirstOrDefault();
                    if (!string.IsNullOrEmpty(pap))
                    {
                        Usercontrols.RadiologyUC.BtnRadiology PAPBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                        PAPBtn.StatusSaved = ChkSaveRad(tpr_id, "PT");
                        PAPBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(PAPBtn_btnRadiologyClick);
                        PAPBtn.tooltipText = pap;
                        rlgPAP.AddButtonRadiology(PAPBtn);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetRadiologyPAP()", ex, false);
            }
        }
        private void PAPBtn_btnRadiologyClick(object sender, EventArgs e)
        {
            using (DialogPAP dialog = new DialogPAP())
            {
                dialog.tpr_id = Program.CurrentRegis.tpr_id;
                dialog.ShowDialog();
                SetRadiologyPAP(Program.CurrentRegis.tpr_id);
            }
        }

        private void LoadInvestigation()
        {
            rlgPhyEx.ClearButtonRadiology();
            rlgABI.ClearButtonRadiology();
            rlgHearing.ClearButtonRadiology();
            rlgEyes.ClearButtonRadiology();
            rlgDental.ClearButtonRadiology();
            rlgEcho.ClearButtonRadiology();
            rlgEKG.ClearButtonRadiology();
            rlgEST.ClearButtonRadiology();
            rlgXray.ClearButtonRadiology();
            rlgPFT.ClearButtonRadiology();
            rlgCarotid.ClearButtonRadiology();
            rlgOtherExam.ClearButtonRadiology();
            rlgAllExam.ClearButtonRadiology();
            rlgPAP.ClearButtonRadiology();


            if (Program.CurrentRegis != null)
            {
                int? tpr_id = Program.CurrentRegis.tpr_id;
                SetRadiologyPhyExam(tpr_id);
                SetRadiologyABI(tpr_id);
                SetRadiologyHearing(tpr_id);
                SetRadiologyEyes(tpr_id);
                SetRadiologyDental(tpr_id);
                SetRadiologyEcho(tpr_id);
                SetRadiologyEKG(tpr_id);
                SetRadiologyEST(tpr_id);
                SetRadiologyXray(tpr_id);
                SetRadiologyPFT(tpr_id);
                SetRadiologyCarotid(tpr_id);
                SetRadiologyOtherExam(tpr_id);
                SetRadiologyAllExam(tpr_id);
                SetRadiologyPAP(tpr_id);
            }
        }
        #endregion

        private void btnQuestionaire_EnabledChanged(object sender, EventArgs e)
        {
            btnRetreiveLab.Enabled = ((Button)sender).Enabled;
            btnAssessment.Enabled = ((Button)sender).Enabled;
            RetrieveXrayBtn.Enabled = ((Button)sender).Enabled;
        }
        private void btnGetPatient_Click(object sender, EventArgs e)
        {
            ImportPatientToBookFrm frm = new ImportPatientToBookFrm();
            frm.ShowDialog();
            if (frm.status == StatusTransaction.True)
            {
                if (frm.tpr_id != null)
                {
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == frm.tpr_id).FirstOrDefault();
                            if (patient_regis != null)
                            {
                                txtHN.Text = patient_regis.trn_patient.tpt_hn_no;
                                txtName.Text = patient_regis.trn_patient.tpt_first_name;
                                txtSurName.Text = patient_regis.trn_patient.tpt_last_name;
                                cobLocation.SelectedValue = patient_regis.mhs_id;
                                txtVDate.Value = patient_regis.tpr_arrive_date.Value.Date;
                                btnSearch_Click(null, null);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "btnGetPatient_Click", ex, false);
                    }
                }
            }
            else if (frm.status == StatusTransaction.Error)
            {

            }
        }
        private void frmBookResult_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.CurrentRegis = null;
            Program.CurrentPatient_queue = null;
        }
        private void btnAssessment_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                DialogAssessmentAndPlan frm = new DialogAssessmentAndPlan();
                frm.tpr_id = Program.CurrentRegis.tpr_id;
                frm.Username = Program.CurrentUser.mut_username;
                frm.ShowDialog();
            }
        }

        private void btnRetreiveLab_Click(object sender, EventArgs e)
        {
            try


            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == Program.CurrentRegis.tpr_id).FirstOrDefault();
                   var lab_date = new DateTime(2017,11,01);
                   if (patient_regis.tpr_arrive_date < lab_date)
                   {
                       string message = "Case นี้จะถูกปรับเป็น Normal Range (1/11/2017) ใหม่" + Environment.NewLine + "ยืนยันจะกด Retrieve Lab ใช่ หรือ ไม่?";
                       string caption = "คำเตือน";
                       MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                       DialogResult result;

                       // Displays the MessageBox.

                       result = MessageBox.Show(message, caption, buttons);

                       if (result == System.Windows.Forms.DialogResult.Yes)
                       {

                           
                          // lbAlert.Text = patient_regis.tpr_arrive_date.ToString();
                           using (frmBGScreen frmbg = new frmBGScreen())
                           {
                               frmbg.Show();
                               using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                               {
                                   ws.getCheckUpLabResult("", Program.CurrentRegis.tpr_id);
                               }
                           }

                       }
                   }
                   else {

                       using (frmBGScreen frmbg = new frmBGScreen())
                       {
                           frmbg.Show();
                           using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                           {
                               ws.getCheckUpLabResult("", Program.CurrentRegis.tpr_id);
                           }
                       }
                   }
                }
                using (frmBGScreen frmbg = new frmBGScreen())
                {
                    frmbg.Show();
                    using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                    {
                        ws.getCheckUpLabResult("", Program.CurrentRegis.tpr_id);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnRetreiveLab_Click", ex, false);
            }
        }
        private void RetrieveXrayBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmBGScreen frmbg = new frmBGScreen())
                {
                    frmbg.Show();
                    using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                    {
                        ws.RetrieveImaging(Program.CurrentRegis.tpr_id, Program.CurrentUser != null ? Program.CurrentUser.mut_username : "");
                        LoadInvestigation();
                    }
                    //using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                    //{
                    //    using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                    //    {
                    //        DateTime dateNow = Program.GetServerDateTime();
                    //        var patient = contxt.trn_patient_regis
                    //                            .Where(x => x.tpr_id == Program.CurrentRegis.tpr_id)
                    //                            .Select(x => new
                    //                            {
                    //                                x.tpr_en_no,
                    //                                x.trn_patient.tpt_hn_no
                    //                            }).FirstOrDefault();
                    //        ws.InsertDBEmrCheckupResultXray(patient.tpt_hn_no, patient.tpr_en_no, dateNow.AddYears(-5), dateNow, true);
                    //        LoadInvestigation();
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "RetrieveXrayBtn_Click", ex, false);
            }
        }

        private void btnRetrieveVS_Click(object sender, EventArgs e)
        {
            using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
            {
                ws.retrieveVitalSign(Program.CurrentRegis.tpr_id, Program.CurrentUser.mut_username);
                //showVitalSign(Program.CurrentRegis == null ? (int?)null : Program.CurrentRegis.tpr_id);
            }
            tabBasicMeasurementUC1.Refresh();
        }

        private void lbRetrieve_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                int tprID = 0;
                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;

                bool getInfo = new APITrakcare.GetPatientInfoCls().GetInfo(tprID);
                if (getInfo)
                {
                    showPatient(tprID);
                }
            }
        }
        private void trn_patient_doctor_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "tpd_doctor_code")
            {
                trn_patient_doctor doc = (trn_patient_doctor)sender;
                if (doc.tpd_doctor_code == null)
                {
                    autoCompleteUC1.SelectedValue = null;
                }
                else
                {
                    autoCompleteUC1.SelectedValue = doc.tpd_doctor_code;
                }
            }
        }

        private Boolean ChkSaveRad(int? tpr_id, String radiology)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                Boolean? FlagSave = cdc.trn_patient_book_results.Where(x => x.tpr_id == tpr_id && x.tpbr_radiology == radiology && x.tpbr_active == true)
                             .Select(x => x.tpbr_flag_saved)
                             .FirstOrDefault();
                if (FlagSave == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        private void btnSendDoctorApprove_Click(object sender, EventArgs e)
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            string username = Program.CurrentUser.mut_username;
            string rptCode = "";

            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                trn_patient_doctor tpd = cdc.trn_patient_doctors.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                if (tpd == null || string.IsNullOrEmpty(tpd.tpd_doctor_code) || tpd.tpd_doctor_code != (autoCompleteUC1.SelectedValue == null ? "" : autoCompleteUC1.SelectedValue.ToString()))
                {
                    MessageBox.Show("Please insert Doctor or Click Save Button.");
                }
                else
                {
                    if (rdBookColor.Checked == true && rdPrtTH.Checked == true)
                    {
                        rptCode = "BK306";
                    }
                    else if (rdBookColor.Checked == true && rdPrtEN.Checked == true)
                    {
                        rptCode = "BK307";
                    }
                    else if (rdBook.Checked == true && rdPrtTH.Checked == true)
                    {
                        rptCode = "BK301";
                    }
                    else if (rdBook.Checked == true && rdPrtEN.Checked == true)
                    {
                        rptCode = "BK304";
                    }
                    else if (rdOnePage.Checked == true && rdPrtTH.Checked == true)
                    {
                        rptCode = "BK302";
                    }
                    else if (rdOnePage.Checked == true && rdPrtEN.Checked == true)
                    {
                        rptCode = "BK303";
                    }
                    using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                    {
                        bool? proc = ws.SendDoctorApprove(tpr_id, rptCode, username);
                        if (proc == true)
                        {
                            MessageBox.Show("Sent Dr.Approve Completed.", "Approve.");
                            gridSearch data = sourceGridSearch.Where(x => x.RefID == tpr_id).FirstOrDefault();
                            data.Status = "รอแพทย์ Approve";
                            data.statusCode = "WFA";
                            gvSearch_SetBtnReason();
                            gvSearch.Refresh();
                            showPatient(tpr_id);
                        }
                        else
                        {
                            MessageBox.Show("Sent Dr.Approve Incomplete. Please Try Again.", "Approve.");
                        }
                    }
                    DateTime dateNow = DateTime.Now;
                    _PatientRegis.trn_book_events.Add(new trn_book_event
                    {
                        mbe_id = GetEventApproved(),
                        tbe_remark = "",
                        tbe_date = dateNow,
                        tbe_active = true,
                        tbe_create_by = Program.CurrentUser.mut_username,
                        tbe_create_date = dateNow,
                        tbe_update_by = Program.CurrentUser.mut_username,
                        tbe_update_date = dateNow
                    });
                    bookTransaction1.ListBookEvent = _PatientRegis.trn_book_events;
                    dbc.SubmitChanges();
                }
            }
        }
        private int GetEventApproved()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    return cdc.mst_book_events.Where(x => x.mbe_code == "SDA").Select(x => x.mbe_id).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        private void btnRecallDoctorApprove_Click(object sender, EventArgs e)
        {
            int tpr_id = Program.CurrentRegis.tpr_id;
            string username = Program.CurrentUser.mut_username;
            using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
            {
                bool? proc = ws.RecallApprove(tpr_id, username);
                if (proc == true)
                {
                    gridSearch data = sourceGridSearch.Where(x => x.RefID == tpr_id).FirstOrDefault();
                    data.Status = "พิมพ์และตรวจสอบผล";
                    data.statusCode = "CBB";
                    gvSearch_SetBtnReason();
                    gvSearch.Refresh();
                    showPatient(tpr_id);
                }
                else if (proc == null)
                {
                    gridSearch data = sourceGridSearch.Where(x => x.RefID == tpr_id).FirstOrDefault();
                    data.Status = "แพทย์กาลัง Approve";
                    data.statusCode = "API";
                    gvSearch_SetBtnReason();
                    gvSearch.Refresh();
                    MessageBox.Show("ไม่สามารถ Recall Book ได้ แพทย์กาลัง Approve", "Recall.");
                    showPatient(tpr_id);
                }
            }
        }

        private void gvSearch_SetBtnReason()
        {
            DataGridView dgv = gvSearch;
            foreach (DataGridViewRow row in gvSearch.Rows)
            {
                gridSearch data = (gridSearch)row.DataBoundItem;
                switch (data.statusCode)
                {
                    case "NAP":
                    case "APD":
                        DataGridViewButtonCell btnCell = new DataGridViewButtonCell();
                        row.Cells["colStatus"] = btnCell;
                        break;
                }
                if (data.express)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fb9f9f");
                }
            }
        }

        public void gvSearch_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.Columns[e.ColumnIndex].Name == "colStatus")
            {
                gridSearch data = (gridSearch)dgv.Rows[e.RowIndex].DataBoundItem;
                switch (data.statusCode)
                {
                    case "NAP":
                        popUpRejectReason popUp = new popUpRejectReason(data.reason);
                        break;
                    case "APD":
                        popUpLogInBookAdmin logIn = new popUpLogInBookAdmin();
                        bool log = logIn.LogIn(data.RefID);
                        if (log)
                        {
                            showPatient(data.RefID);
                            data.statusCode = "CBB";
                            data.Status = "พิมพ์และตรวจสอบผล";
                            dgv.Rows[e.RowIndex].Cells["colStatus"] = new DataGridViewTextBoxCell();
                            gvSearch.Refresh();
                        }
                        break;
                }
            }
        }

        private void btnPrtNewBook_Click(object sender, EventArgs e)
        {
            lbAlert.Text = "";
            if (rdBook.Checked == true)
            {
                if (rdPrtTH.Checked == true)
                {
                    int tprID = 0;
                    if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    Program.OpenReport(tprID, "BK306");

                    //List<string> rptCode = new List<string> { "BK306" };
                    //int tprID = 0;
                    //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    //frm.previewReport();
                }
                else if (rdPrtEN.Checked == true)
                {
                    int tprID = 0;
                    if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    Program.OpenReport(tprID, "BK307");

                    //List<string> rptCode = new List<string> { "BK307" };
                    //int tprID = 0;
                    //if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    //Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    //frm.previewReport();
                }
            }
            else if (rdOnePage.Checked == true)
            {
                if (rdPrtTH.Checked == true)
                {
                    List<string> rptCode = new List<string> { "BK302" };
                    int tprID = 0;
                    if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    frm.previewReport();
                    //ClsReport.previewOnePageTH();
                }
                else if (rdPrtEN.Checked == true)
                {
                    List<string> rptCode = new List<string> { "BK303" };
                    int tprID = 0;
                    if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                    Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                    frm.previewReport();
                    //ClsReport.previewOnePageEN();
                }
            }
        }

        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDate.Checked)
                txtVDate.Enabled = false;
            else
                txtVDate.Enabled = true;
        }
    }
}