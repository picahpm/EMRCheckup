using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.Forms
{
    public partial class PapResultFrm : Form
    {
        public PapResultFrm()
        {
            InitializeComponent();
            try
            {
                using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                {
                    var combo = contxt.mst_hpc_sites
                                      .Where(x => x.mhs_status == 'A')
                                      .Select(x => new site
                                      {
                                          mhs_id = x.mhs_id,
                                          mhs_ename = x.mhs_ename
                                      }).ToList();
                    combo.Insert(0, new site { mhs_id = null, mhs_ename = "All Location" });
                    cobLocation.DataSource = combo;
                    cobLocation.DisplayMember = "mhs_ename";
                    cobLocation.ValueMember = "mhs_id";
                }
            }
            catch
            {

            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            gvSearch.AutoGenerateColumns = false;
            txtVDate.Value = DateTime.Now;
        }
        private class site
        {
            public int? mhs_id { get; set; }
            public string mhs_ename { get; set; }
        }
        Class.DocScan docscan;

        private mst_user_type _user;
        public mst_user_type user
        {
            get { return _user; }
            set
            {
                if (value != _user)
                {
                    papResultUC1.user = value;
                    _user = value;
                }
            }
        }

        private InhCheckupDataContext cdc;
        private int? _Current_tpr_id;
        private int? Current_tpr_id
        {
            get { return _Current_tpr_id; }
            set
            {
                if (value != _Current_tpr_id)
                {
                    if (value == null)
                    {
                        if (cdc != null) cdc.Dispose();
                        btnSave.Enabled = false;
                        btnPrintPreview.Enabled = false;
                        btnSendToDocScan.Enabled = false;
                        papResultUC1.PatientRegis = null;
                        btnPatho.Enabled = false;
                        newPatientProfileLandscape1.PatientRegis = null;
                        tpt_id = null;
                        hn = "";
                        en = "";
                        pathPatho = "";
                        arrivedDate = DateTime.Now;
                        lblAlert.Text = "";
                    }
                    else
                    {
                        cdc = new InhCheckupDataContext();
                        trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == value).FirstOrDefault();
                        if (PatientRegis == null)
                        {
                            Current_tpr_id = null;
                        }
                        else
                        {
                            tpt_id = PatientRegis.tpt_id;
                            hn = PatientRegis.trn_patient.tpt_hn_no;
                            en = PatientRegis.tpr_en_no;
                            pathPatho = PatientRegis.trn_patient
                                                    .trn_patient_history_pathos
                                                    .Where(x => x.tphp_en_no == PatientRegis.tpr_en_no)
                                                    .Select(x => x.tphp_link).FirstOrDefault();
                            arrivedDate = PatientRegis.trn_patient_regis_detail.tpr_real_arrived_date.Value;
                            newPatientProfileLandscape1.PatientRegis = PatientRegis;
                            papResultUC1.PatientRegis = PatientRegis;
                            btnSave.Enabled = true;
                            btnPrintPreview.Enabled = true;
                            btnSendToDocScan.Enabled = true;
                            btnPatho.Enabled = true;
                        }
                    }
                    _Current_tpr_id = value;
                }
            }
        }
        private int? tpt_id { get; set; }
        private string hn { get; set; }
        private string en { get; set; }
        private string _pathPatho;
        private string pathPatho { get { return _pathPatho; } set
        {
            if (value != _pathPatho)
            {
                if (string.IsNullOrEmpty(value))
                {
                    btnPreviewPatho.Enabled = false;
                }
                else
                {
                    btnPreviewPatho.Enabled = true;
                }
                _pathPatho = value;
            }
        }}
        private DateTime arrivedDate { get; set; }

        private class patient
        {
            public int no { get; set; }
            public int tpr_id { get; set; }
            public string hn { get; set; }
            public string name { get; set; }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            site selectSite = (site)cobLocation.SelectedItem;
            searchPatient(txtHN.Text, txtName.Text, selectSite.mhs_id, txtVDate.Value);
        }
        private void searchPatient(string hn, string name, int? location, DateTime date)
        {
            using (InhCheckupDataContext contxt = new InhCheckupDataContext())
            {
                List<patient> listPatient = contxt.trn_patient_regis
                                                  .Where(x => x.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date == date.Date &&
                                                              x.trn_patient_events.Any(y => y.mst_event.mvt_code == "PT") &&
                                                              (hn == null || hn == "" ? true : x.trn_patient.tpt_hn_no.Replace("-", "").Contains(hn)) &&
                                                              (name == null || name == "" ? true : (x.trn_patient.tpt_othername == null ? false : x.trn_patient.tpt_othername.ToUpper().Contains(name))) &&
                                                              (location == null ? true : x.mhs_id == location))
                                                  .ToList()
                                                  .Select((x, inx) => new patient
                                                  {
                                                      no = inx + 1,
                                                      tpr_id = x.tpr_id,
                                                      hn = x.trn_patient.tpt_hn_no,
                                                      name = x.trn_patient.tpt_othername
                                                  }).ToList();
                gvSearch.DataSource = listPatient;
            }

        }

        private void gvSearch_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            if (gv.SelectedRows.Count == 1)
            {
                patient p = (patient)gv.SelectedRows[0].DataBoundItem;
                Current_tpr_id = p.tpr_id;
            }
            else
            {
                Current_tpr_id = null;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                papResultUC1.EndEdit();
                try
                {
                    cdc.SubmitChanges();
                }
                catch (System.Data.Linq.ChangeConflictException)
                {
                    foreach (System.Data.Linq.ObjectChangeConflict occ in cdc.ChangeConflicts)
                    {
                        cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                    }
                    cdc.SubmitChanges();
                }
                lblAlert.Text = "Save Data Complete.";
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSave_Click(object sender, EventArgs e)", ex, false);
                lblAlert.Text = "Please Try Again.";
            }
        }
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            List<string> rptCode = new List<string>();
            switch (cmbLang.SelectedIndex)
            {
                case 0: //ผลการตรวจมะเร็งปากมดลูก (EN)
                    rptCode = new List<string> { "PT102" };
                    break;
                case 1: //ผลการตรวจมะเร็งปากมดลูก (ARB)
                    rptCode = new List<string> { "PT103" };
                    break;
                case 2: //ผลการตรวจมะเร็งปากมดลูก (JAP)
                    //rptCode = IsThai ? new List<string> { "PE701" } : new List<string> { "PE702" };
                    rptCode = new List<string> { "PT104" };
                    break;
                default:
                    rptCode = new List<string> { "PT102" };
                    break;
            }
            // List<string> rptCode = new List<string> { "PT102" };

            Report.ClsReportDocument cls = new Report.ClsReportDocument();
            string otherRptCode = cls.getRptCodeBySite((int)_Current_tpr_id);
            if (!string.IsNullOrEmpty(otherRptCode)) rptCode.Add(cls.getRptCodeBySite((int)_Current_tpr_id));
            Report.frmPreviewReport frm = new Report.frmPreviewReport((int)_Current_tpr_id, rptCode);
            frm.previewReport();
        }
        private void btnSendToDocScan_Click(object sender, EventArgs e)
        {
            try
            {
                string rptCode = "PT102";
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

                string result = new EmrClass.DocScan.SendToDocScanCls().Send(Program.CurrentRegis.tpr_id, rptCode, Program.CurrentSite.mhs_code, Program.CurrentUser.mut_username);
                lblAlert.Text = result;




                //if (docscan.SendtoDocscan(rptCode, Program.CurrentRegis.tpr_id, Program.CurrentRegis.tpr_en_no, Program.getCurrentCareProvider))
                //{
                //    lblAlert.Text = Class.HistoryData.savestatus;
                //    Class.HistoryData.showform = 'Y';
                //    lblAlert.Text = "Send To Docscan Completed";
                //    return;
                //}
                //else
                //{
                //    lblAlert.Text = "Please Try Again.";
                //}
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSendToDocScan_Click(object sender, EventArgs e)", ex, false);
                lblAlert.Text = "Please Try Again.";
            }
        }
        private void btnPatho_Click(object sender, EventArgs e)
        {
            using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
            {
                ws.InsertDBEmrCheckupResultXray(hn, en, DateTime.Now.AddYears(-5), DateTime.Now, false);
            }
            using (InhCheckupDataContext contxt = new InhCheckupDataContext())
            {
                pathPatho = contxt.trn_patient_history_pathos.Where(x => x.tpt_id == tpt_id && x.tphp_en_no == en).Select(x => x.tphp_link).FirstOrDefault();
                if (string.IsNullOrEmpty(pathPatho))
                {
                    lblAlert.Text = "Pathology's result is not found.";
                }
                else
                {
                    System.Diagnostics.Process.Start("IExplore.exe", pathPatho);
                }
            }
        }
        private void btnPreviewPatho_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", pathPatho);
        }
    }
}
