using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.EmrClass;
using BKvs2010.Forms;
namespace BKvs2010
{
    public partial class DialogPhysicalExam : Form
    {
        public DialogPhysicalExam()
        {
            InitializeComponent();
        }
        private InhCheckupDataContext dbc;
        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value == null)
                {
                    btnSave.Enabled = false;
                    this.tabPhyExamUC1.PatientRegis = null;
                    _tpr_id = null;
                }
                else
                {
                    try
                    {
                        dbc = new InhCheckupDataContext();
                        trn_patient_regi patient_regis = dbc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                        if (patient_regis == null)
                        {
                            btnSave.Enabled = false;
                            this.tabPhyExamUC1.PatientRegis = null;
                            _tpr_id = null;
                        }
                        else
                        {
                            this.tabPhyExamUC1.PatientRegis = patient_regis;
                            _tpr_id = value;
                            btnSave.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSave.Enabled = false;
                        this.tabPhyExamUC1.PatientRegis = null;
                        _tpr_id = null;
                        Program.MessageError(this.Name, "tpr_id", ex, false);
                    }
                }
            }
        }
        public bool IsThai { get; set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tabPhyExamUC1.EndEdit();
                try
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                            .Where(x => x.tpr_id == this._tpr_id &&
                                                                        x.tpbr_radiology == "PE")
                                                            .FirstOrDefault();
                    if (bookResult == null)
                    {
                        bookResult = new trn_patient_book_result()
                        {
                            tpr_id = (int)this._tpr_id,
                            tpbr_radiology = "PE",
                            tpbr_create_by = Program.CurrentUser.mut_username,
                            tpbr_create_date = dateNow
                        };
                        dbc.trn_patient_book_results.InsertOnSubmit(bookResult);
                    }
                    bookResult.tpbr_flag_saved = true;
                    bookResult.tpbr_show_sections = true;
                    bookResult.tpbr_show_summary = true;
                    bookResult.tpbr_not_show_report = false;
                    bookResult.tpbr_active = true;
                    bookResult.tpbr_update_by = Program.CurrentUser.mut_username;
                    bookResult.tpbr_update_date = dateNow;
                    dbc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                    {
                        dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    dbc.SubmitChanges();
                }
                lbAlertMsg.Text = "Save Data Complete.";
                btnSendToDocscan.Enabled = true;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSave_Click", ex, false);
                lbAlertMsg.Text = "โปรดลองอีกครั้ง";
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Program.MessageError("DialogPhysicalExam", "btnClose_Click", ex, false);
            }
        }
        private void btnSendToDocscan_Click(object sender, EventArgs e)
        {
            if (_tpr_id != null)
            {
                string rptCode = "PE101";
                switch (tabPhyExamUC1.TabSelected)
                {
                    case 1:
                        rptCode = "QA103";
                        break;
                    case 5: //off shore
                        rptCode = "PE601";
                        break;
                    //case 6: //fit to fly
                    //    //rptCode = IsThai ? new List<string> { "PE701" } : new List<string> { "PE702" };
                    //    rptCode = "AV103";
                    //    break;
                    case 7: // occ med
                        rptCode = "PE701";
                        break;
                    default:
                        rptCode = "PE101";
                        break;
                }

                string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, rptCode, Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                lbAlertMsg.Text = result;

                //DocScan docscan = new DocScan();
                //if (docscan.SendtoDocscan(rptCode, Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                //{
                //    lbAlertMsg.Visible = true;
                //    lbAlertMsg.Text = "Send To Docscan Completed";
                //    return;
                //}
                //else
                //    lbAlertMsg.Text = "Cannot send to docsan user authentication failed";
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // String getRtp ="";
            if (_tpr_id != null)
            {
                List<string> rptCode = new List<string>();
                switch (tabPhyExamUC1.TabSelected)
                {

                    case 1:
                        rptCode = new List<string> { "QA103" };
                        break;
                    case 5: //off shore
                        rptCode = new List<string> { "PE601" };
                        break;
                    case 6: //fit to fly
                        //rptCode = IsThai ? new List<string> { "PE701" } : new List<string> { "PE702" };
                        rptCode = new List<string> { "AV103" };
                        break;
                    case 7: // occ med
                        rptCode = new List<string> { "PE701"};
                        break;

                    default:
                        rptCode = new List<string> { "PE101" };
                        break;
                }
                Report.frmPreviewReport frm = new Report.frmPreviewReport((int)_tpr_id, rptCode);
                frm.previewReport();
            }
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

        private void btnHealth_Click(object sender, EventArgs e)
        {
            if (Program.CurrentRegis != null)
            {
                QuestionnaireFrm frm = new QuestionnaireFrm();
                frm.tpr_id = Program.CurrentRegis.tpr_id;
                frm.ShowDialog();
            }
        }

        private void btnRetrieveVS_Click(object sender, EventArgs e)
        {
            using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
            {
                ws.retrieveVitalSign(Program.CurrentRegis.tpr_id, Program.CurrentUser.mut_username);
            }
        }

        private void btnRetreiveLab_Click(object sender, EventArgs e)
        {
            try
            {
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
                        using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                        {
                            DateTime dateNow = Program.GetServerDateTime();
                            var patient = contxt.trn_patient_regis
                                                .Where(x => x.tpr_id == Program.CurrentRegis.tpr_id)
                                                .Select(x => new
                                                {
                                                    x.tpr_en_no,
                                                    x.trn_patient.tpt_hn_no
                                                }).FirstOrDefault();
                            ws.InsertDBEmrCheckupResultXray(patient.tpt_hn_no, patient.tpr_en_no, dateNow.AddYears(-5), dateNow, true);
                            //LoadInvestigation();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "RetrieveXrayBtn_Click", ex, false);
            }
        }
    }
}
