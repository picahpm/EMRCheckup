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
    public partial class DialogPAP : Form
    {
        public DialogPAP()
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
                    this.tabObstetricsCKUC1.PatientRegis = null;
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
                            this.tabObstetricsCKUC1.PatientRegis = null;
                            _tpr_id = null;
                        }
                        else
                        {
                            this.tabObstetricsCKUC1.PatientRegis = patient_regis;
                            _tpr_id = value;
                            btnSave.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        btnSave.Enabled = false;
                        this.tabObstetricsCKUC1.PatientRegis = null;
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
                tabObstetricsCKUC1.EndEdit();
                try
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient_book_result bookResult = dbc.trn_patient_book_results
                                                            .Where(x => x.tpr_id == this._tpr_id &&
                                                                        x.tpbr_radiology == "PT")
                                                            .FirstOrDefault();
                    if (bookResult == null)
                    {
                        bookResult = new trn_patient_book_result()
                        {
                            tpr_id = (int)this._tpr_id,
                            tpbr_radiology = "PT",
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
                    lbAlertMsg.Text = "Save Data Complete.";
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                    {
                        dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    dbc.SubmitChanges();
                }
                
            }
            catch (Exception ex)
            {
                lbAlertMsg.Text = "โปรดลองอีกครั้ง";
                Program.MessageError(this.Name, "btnSave_Click", ex, false);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_tpr_id != null)
            {
                List<string> rptCode = new List<string> { "PT101" };

                switch (tabObstetricsCKUC1.TabSelected  )
                {
                    case 0:
                        rptCode = new List<string> { "PT101" };
                        break;
                    case 1:
                        rptCode = new List<string> { "PT101" };
                        break;
                    case 2:

                        switch (cmbLang.SelectedIndex)
                        {
                            case 0: //ผลการตรวจมะเร็งปากมดลูก (EN)
                                rptCode =new List<string> {  "PT102"};
                                break;
                            case 1: //ผลการตรวจมะเร็งปากมดลูก (ARB)
                                rptCode =new List<string> {  "PT103"};
                                break;
                            case 2: //ผลการตรวจมะเร็งปากมดลูก (JAP)
                                //rptCode = IsThai ? new List<string> { "PE701" } : new List<string> { "PE702" };
                                rptCode =new List<string> {  "PT104"};
                                break;
                            default:
                                rptCode = new List<string> { "PT102"};
                                break;
                        }
                        break;
                    default:
                        rptCode = new List<string> { "PT101" };
                        break;
                }
                Report.frmPreviewReport frm = new Report.frmPreviewReport((int)_tpr_id, rptCode);
                frm.previewReport();
            }
        }

        private void btnSendToDocscan_Click(object sender, EventArgs e)
        {

            string rptCode = "";
            switch (tabObstetricsCKUC1.TabSelected)
            {
                case 0:
                    rptCode =  "PT101" ;
                    break;
                case 1:
                    rptCode =  "PT101";
                    break;
                case 2:

                    switch (cmbLang.SelectedIndex)
                    {
                        case 0: //ผลการตรวจมะเร็งปากมดลูก (EN)
                            rptCode = "PT102";
                            break;
                        case 1: //ผลการตรวจมะเร็งปากมดลูก (ARB)
                            rptCode = "PT103";
                            break;
                        case 2: //ผลการตรวจมะเร็งปากมดลูก (JAP)
                            //rptCode = IsThai ? new List<string> { "PE701" } : new List<string> { "PE702" };
                            rptCode = "PT104";
                            break;
                        default:
                            rptCode = "PT102";
                            break;
                    }
                    break;
                default:
                    rptCode =  "PT101" ;
                    break;
            }

            string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, rptCode, Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
            lbAlertMsg.Text = result;

            //if (docscan.SendtoDocscan(rptCode, Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
            //{
            //    lbAlertMsg.Text = HistoryData.savestatus;
            //}
            //else
            //    lbAlertMsg.Text = "Cannot send to docsan user authentication failed";
        }
    }
}