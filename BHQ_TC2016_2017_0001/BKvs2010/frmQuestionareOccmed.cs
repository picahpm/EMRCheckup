using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.IO;
using BKvs2010.EmrClass;

namespace BKvs2010
{
    public partial class frmQuestionareOccmed : Form
    {  
        InhCheckupDataContext dbc = new InhCheckupDataContext();

        //bool IsSaveComplete = false;
        public int SetTprID { get; set; }      
        
        public frmQuestionareOccmed()
        {
            InitializeComponent();

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void frmQuestionareOccmed_Load(object sender, EventArgs e)
        {
            try
            {
                if (Program.CurrentRegis != null)
                {
                    //find HN
                    var objHN1 = (from t1 in dbc.trn_patient_regis join t2 in dbc.trn_patients on t1.tpt_id equals t2.tpt_id where t1.tpt_id == Program.CurrentRegis.tpt_id select new { t1, t2 }).OrderByDescending(c => c.t1.tpr_create_date).FirstOrDefault();

                 String vHnno = objHN1.t2.tpt_hn_no ;
                 String vName = objHN1.t2.tpt_othername;
                 String vAllergy = objHN1.t2.tpt_allergy;
                    this.Text = Program.GetRoomName("QuestionaireOCCMED");
                    //uiProfileHorizontal1.Loaddata();
                    timer1.Enabled = false;
                 this.LoadTransaction( vHnno,  vName,  vAllergy);  
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Load Page : " + ex.Message);
            }
	   

             

	
        }
        

        private void LoadTransaction(String vHnno, String vName, String vAllergy)
        {
            string url = SendInfotoOCCMEDbyHN(vHnno, vName, vAllergy);

            try
            {
                webBrowser1.Navigate(new Uri(url));
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }
        public string SendInfotoOCCMEDbyHN(string valHnno, string valName, string valAllergy)
        {
            
            string returnVal = PrePareData.StaticDataCls.QuesOccmedUrl+"web/mainPage.aspx?shn=" + valHnno + "&sName=" + valName + "&sAllergy=" + valAllergy;
            
            return returnVal;
        
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {

            if (radThai.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA301" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.previewReport();
            }
            else if (radEng.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA302" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.previewReport();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (radThai.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA301" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.printReport();
                //ClsReport.printRptEye(1);
            }
            else if (radEng.Checked == true)
            {
                List<string> rptCode = new List<string> { "QA302" };
                int tprID = 0;

                if (Program.CurrentRegis != null) tprID = Program.CurrentRegis.tpr_id;
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.printReport();
                //ClsReport.printRptEye(1);
            }
        }

        private void btnSendDoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.CurrentUser.mut_type.ToString() == "D")
                {
                    string code = "QA301";
                    if (radThai.Checked)
                    {
                        code = "QA301";
                    }
                    else
                    {
                        code = "QA302";
                    }
                    string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, code, Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                    lblMsg.Visible = true;
                    lblMsg.Text = result;

                    //if (docscan.SendtoDocscan(code, Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                    //{
                    //    lblMsg.Visible = true;
                    //    lblMsg.Text = "Send To Docscan Completed";

                    //    return;

                    //}
                    //else
                    //    lblMsg.Text = "Cannot send to docsan user authentication failed";
                }
                else
                    lblMsg.Text = "Cannot send to docscan";
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


    }
}
