using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class ObjectiveUC : UserControl
    {
        private class mstEvent
        {
            public int id { get; set; }
            public string code { get; set; }
        }
        private List<mstEvent> masterEvent;
        public ObjectiveUC()
        {
            InitializeComponent();
            gvCurrentLab.AutoGenerateColumns = false;
        }

        public string username { get; set; }
        private trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                if (value == null)
                {
                    Clear();
                }
                else
                {
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            masterEvent = cdc.mst_events
                                             .Select(x => new mstEvent
                                             {
                                                 id = x.mvt_id,
                                                 code = x.mvt_code
                                             }).ToList();
                        }
                        SetRadiologyABI(value.tpr_id);
                        SetRadiologyDental(value.tpr_id);
                        SetRadiologyEcho(value.tpr_id);
                        SetRadiologyEKG(value.tpr_id);
                        SetRadiologyEST(value.tpr_id);
                        SetRadiologyEyes(value.tpr_id);
                        SetRadiologyHearing(value.tpr_id);
                        SetRadiologyOtherExam(value.tpr_id);
                        SetRadiologyPhyExam(value.tpr_id);
                        SetRadiologyXray(value.tpr_id);
                        trn_basic_measure_hdr basicHdr = value.trn_basic_measure_hdrs.FirstOrDefault();
                        if (basicHdr != null)
                        {
                            trn_basic_measure_dtl basicDtl = basicHdr.trn_basic_measure_dtls.OrderByDescending(x => x.tbd_date).FirstOrDefault();
                            if (basicDtl != null)
                            {
                                txtBodyWeight.Text = basicDtl.tbd_weight;
                                txtBMI.Text = basicDtl.tbd_bmi;
                                txtTemperature.Text = basicDtl.tbd_temp;
                                txtbodyHeight.Text = basicDtl.tbd_height;
                                txtPulse.Text = basicDtl.tbd_pulse;
                                txtRespiratory.Text = basicDtl.tbd_rr;
                                txtBloodPressure.Text = basicDtl.tbd_systolic;
                                txtBloodPressure2.Text = basicDtl.tbd_diastolic;
                            }
                        }

                        using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                        {
                            var result = contxt.trn_patient_ass_hdrs
                                               .Where(x => x.trn_patient_ass_grp.tpr_id == value.tpr_id)
                                               .Select(x => new LabProfileResultsort
                                               {
                                                   Code = x.tpeh_order_code,
                                                   Name = x.tpeh_order_name,
                                                   Status = (x.tpeh_status == 'E') ? "Complete" : "In Progress",
                                                   Result = (x.tpeh_status == 'E') ? "Results" : "",
                                                   Cumulative = (x.tpeh_status == 'E') ? "C" : "",
                                               }).ToList();
                            gvCurrentLab.DataSource = result;
                        }
                        _PatientRegis = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
        }
        private class LabProfileResultsort
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public string Result { get; set; }
            public string Cumulative { get; set; }
        }

        private void SetRadiologyABI(int? tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int abi = cdc.trn_patient_events
                                 .Where(x => x.tpr_id == tpr_id &&
                                             x.mst_event.mvt_code == "AB")
                                 .Count();
                    if (abi > 0)
                    {
                        Usercontrols.RadiologyUC.BtnRadiology ABIbtn = new Usercontrols.RadiologyUC.BtnRadiology();
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
            }
        }

        private void SetRadiologyDental(int? tpr_id)
        {
            try
            {
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
            }
        }

        private void SetRadiologyEcho(int? tpr_id)
        {
            try
            {
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
            }
        }

        private void SetRadiologyEKG(int? tpr_id)
        {
            try
            {
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
            }
        }

        private void SetRadiologyEST(int? tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int est = cdc.trn_patient_events
                                 .Where(x => x.tpr_id == tpr_id &&
                                             x.mst_event.mvt_code == "ES")
                                 .Count();
                    if (est > 0)
                    {
                        Usercontrols.RadiologyUC.BtnRadiology ESTBtn = new Usercontrols.RadiologyUC.BtnRadiology();
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
            }
        }

        private void SetRadiologyEyes(int? tpr_id)
        {
            try
            {
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
            }
        }

        private void SetRadiologyHearing(int? tpr_id)
        {
            try
            {
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
            }
        }

        private void SetRadiologyOtherExam(int? tpr_id)
        {
            try
            {
                Usercontrols.RadiologyUC.BtnRadiology OtherExamBtn = new Usercontrols.RadiologyUC.BtnRadiology();
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
            }
        }

        private void SetRadiologyPhyExam(int? tpr_id)
        {
            try
            {
                Usercontrols.RadiologyUC.BtnRadiology PhyExamBtn = new Usercontrols.RadiologyUC.BtnRadiology();
                PhyExamBtn.btnRadiologyClick += new Usercontrols.RadiologyUC.BtnRadiology.OnBtnRadiologyClick(PhyExamBtn_btnRadiologyClick);
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
            }
        }

        private void SetRadiologyXray(int? tpr_id)
        {
            try
            {
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
            //    dialog.Text = PrePareData.StaticDataCls.ProjectName + " [Chest X-Ray]";
            //    dialog.PageTag = "XR";
            //    dialog.ShowDialog();
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
            //}
        }

        private void btnCumulativeVital_Click(object sender, EventArgs e)
        {
            using (frmCumulativeVitalSigns frm = new frmCumulativeVitalSigns())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }

        public void Clear()
        {
            rlgABI.ClearButtonRadiology();
            rlgDental.ClearButtonRadiology();
            rlgEcho.ClearButtonRadiology();
            rlgEKG.ClearButtonRadiology();
            rlgEST.ClearButtonRadiology();
            rlgEyes.ClearButtonRadiology();
            rlgHearing.ClearButtonRadiology();
            rlgOtherExam.ClearButtonRadiology();
            rlgPhyEx.ClearButtonRadiology();
            rlgXray.ClearButtonRadiology();
            this.Enabled = false;
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SaveData", ex, false);
            }
        }

        private void gvCurrentLab_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string pHN_no = _PatientRegis.trn_patient.tpt_hn_no;
                string pHeadLabNo = gvCurrentLab["LabCode", e.RowIndex].Value.ToString();

                if (gvCurrentLab.CurrentCell.Value.ToString() == "Results")
                {
                    frmLabProfile flabprofile = new frmLabProfile();
                    flabprofile.ptpr_id = _PatientRegis.tpr_id;
                    flabprofile.LabNo = pHeadLabNo;
                    flabprofile.ShowDialog();
                }

                if (gvCurrentLab.CurrentCell.Value.ToString() == "C")
                {
                    frmCumulative frm = new frmCumulative();
                    //frm.WindowState = FormWindowState.Maximized;
                    frm.ptpr_id = _PatientRegis.tpr_id;
                    frm.pHN_no = pHN_no;
                    frm.pHeadLabNo = pHeadLabNo;
                    frm.ShowDialog();
                }
            }
        }
    }
}
