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

namespace BKvs2010
{
    public partial class DialogEyes : Form
    {
        public DialogEyes()
        {
            InitializeComponent();
        }

        InhCheckupDataContext dbc;

        private int? _tpr_id;
        public int? tpr_id
        {
            get { return _tpr_id; }
            set
            {
                if (value == null)
                {
                    btnSave.Enabled = false;
                    _tpr_id = null;
                }
                else
                {
                    try
                    {
                        dbc = new InhCheckupDataContext();
                        trn_patient_regi patientRegis = dbc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                        if (patientRegis == null)
                        {
                            btnSave.Enabled = false;
                            _tpr_id = null;
                        }
                        else
                        {
                            tabEyesExamUC1.PatientRegis = patientRegis;
                            tabEyesExamUC1.isDoctorRoom = false;
                            _tpr_id = value;
                            btnSave.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSave.Enabled = false;
                        _tpr_id = null;
                        Program.MessageError(this.Name, "tpr_id", ex, false);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tabEyesExamUC1.EndEdit();
                try
                {
                    try
                    {
                        DateTime dateNow = Program.GetServerDateTime();
                        trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                                .Where(x => x.tpr_id == this._tpr_id &&
                                                                            x.tpbr_radiology == "EM")
                                                                .FirstOrDefault();
                        if (bookResult == null)
                        {
                            bookResult = new trn_patient_book_result()
                            {
                                tpr_id = (int)this._tpr_id,
                                tpbr_radiology = "EM",
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
                        trn_patient_regi patientRegis = dbc.trn_patient_regis
                                                           .Where(x => x.tpr_id == this._tpr_id)
                                                           .FirstOrDefault();
                        trn_eye_exam_hdr eyeHdr = patientRegis.trn_eye_exam_hdrs.FirstOrDefault();
                        if (eyeHdr != null)
                        {
                            eyeHdr.teh_is_eye_record = true;
                        }
                    }
                    catch
                    {

                    }
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
            }
            catch (Exception ex)
            {
                lbAlertMsg.Text = "โปรดลองอีกครั้ง";
                Program.MessageError(this.Name, "btnSave_Click", ex, false);
            }
        }

        private void btnSendToDocscan_Click(object sender, EventArgs e)
        {
            try
            {
                string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, "EN101", Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                lbAlertMsg.Text = result;


                //if (docscan.SendtoDocscan("EN101", Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                //{
                //    lbAlertMsg.Text = HistoryData.savestatus;
                //}
                //else
                //    lbAlertMsg.Text = "Cannot send to docsan user authentication failed";
            }
            catch (Exception ex)
            {
                lbAlertMsg.Text = ex.Message;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string>();
            int tprID = (int)_tpr_id;
            Report.ClsReportDocument cls = new Report.ClsReportDocument();
            string rptCodeEye = cls.getRptCodeEye(tprID);
            if (rptCodeEye != null) rptCode.Add(rptCodeEye);
            List<string> otherRptCode = cls.get_rptCodeEyeAviation(tprID);
            if (otherRptCode != null) rptCode.AddRange(otherRptCode);
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
            frm.previewReport();
        }
    }
}
