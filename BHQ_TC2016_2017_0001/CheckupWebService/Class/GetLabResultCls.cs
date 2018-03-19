using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DBCheckup;
using Microsoft.VisualBasic;
using System.Globalization;

namespace CheckupWebService.Class
{
    public partial class GetLabResultCls
    {
        public void Insert_trn_ques_patient(int tpr_id, string username, char? pregnancy, char? smoke, bool? diabetes)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = globalCls.GetServerDateTime();
                trn_ques_patient quesPatient = cdc.trn_ques_patients
                                                  .Where(x => x.tpr_id == tpr_id)
                                                  .OrderByDescending(x => x.tqp_id)
                                                  .FirstOrDefault();
                if (quesPatient == null)
                {
                    quesPatient = new trn_ques_patient();
                    quesPatient.tpr_id = tpr_id;
                    quesPatient.tqp_create_by = username;
                    quesPatient.tqp_create_date = dateNow;
                    cdc.trn_ques_patients.InsertOnSubmit(quesPatient);
                }
                quesPatient.tqp_fwm_pregnancy = pregnancy;
                quesPatient.tqp_his_smok = smoke;
                quesPatient.tqp_ill_med_diab = diabetes;
                quesPatient.tqp_update_by = username;
                quesPatient.tqp_update_date = dateNow;
                cdc.SubmitChanges();
            }
        }

        public void Insert_trn_patient_ass(int tpr_id, string username = null)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = globalCls.GetServerDateTime();
                    trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (PatientRegis != null)
                    {
                        EnumerableRowCollection<DataRow> ResultWS_GetCheckupLab = retrieveLab(PatientRegis.trn_patient.tpt_hn_no, dateNow.AddYears(-5), dateNow);
                        List<trn_patient_lab> listPatientLab = getPatientLab(ResultWS_GetCheckupLab);

                        foreach (trn_patient_lab patientLab in listPatientLab)
                        {
                            trn_patient_lab pLab = PatientRegis.trn_patient_labs
                                                               .Where(x => x.tpl_lab_no == patientLab.tpl_lab_no &&
                                                                           x.tpl_en_no == patientLab.tpl_en_no)
                                                               .FirstOrDefault();
                            if (pLab == null)
                            {
                                pLab = new trn_patient_lab
                                {
                                    tpl_rowid = patientLab.tpl_rowid,
                                    tpl_hn_no = patientLab.tpl_hn_no,
                                    tpl_en_no = patientLab.tpl_en_no,
                                    tpl_create_date = patientLab.tpl_create_date
                                };
                                PatientRegis.trn_patient_labs.Add(pLab);
                            }
                            pLab.tpl_patient_age = patientLab.tpl_patient_age;
                            pLab.tpl_patient_sex = patientLab.tpl_patient_sex;
                            pLab.tpl_lab_result = patientLab.tpl_lab_value;
                            pLab.tpl_lab_date = patientLab.tpl_lab_date;
                            pLab.tpl_head_lab_no = patientLab.tpl_head_lab_no;
                            pLab.tpl_head_lab = patientLab.tpl_head_lab;
                            pLab.tpl_lab_no = patientLab.tpl_lab_no;
                            pLab.tpl_lab_name = patientLab.tpl_lab_name;
                            pLab.tpl_lab_value = patientLab.tpl_lab_value;
                            pLab.tpl_lab_unit = patientLab.tpl_lab_unit;
                            pLab.tpl_lab_range = patientLab.tpl_lab_range;
                            pLab.tpl_lab_rsl = patientLab.tpl_lab_rsl;
                            pLab.tpl_lab_hl = patientLab.tpl_lab_hl;
                            pLab.tpl_lab_frs = patientLab.tpl_lab_frs;
                            pLab.tpl_status = patientLab.tpl_status;
                            pLab.tpl_mhs_id = patientLab.tpl_mhs_id;
                            pLab.tpl_mhs_tname = patientLab.tpl_mhs_tname;
                            pLab.tpl_mhs_ename = patientLab.tpl_mhs_ename;
                            pLab.tpl_update_date = dateNow;
                            pLab.tpl_lab_result_seq = patientLab.tpl_lab_result_seq;
                        }
                        cdc.SubmitChanges();

                        trn_patient_ass_grp ass_grp = cdc.trn_patient_ass_grps
                                                            .Where(x => x.tpr_id == PatientRegis.tpr_id)
                                                            .FirstOrDefault();
                        if (ass_grp == null)
                        {
                            ass_grp = new trn_patient_ass_grp
                            {
                                tpr_id = PatientRegis.tpr_id,
                                tpeg_create_date = dateNow,
                                tpeg_create_by = username
                            };
                            cdc.trn_patient_ass_grps.InsertOnSubmit(ass_grp);
                        }
                        ass_grp.tpeg_update_date = dateNow;
                        ass_grp.tpeg_update_by = username;

                        List<LabResult> listLabResult = getLabResult(ResultWS_GetCheckupLab);
                        listLabResult.ForEach(x => x.dob = PatientRegis.trn_patient.tpt_dob);
                        double ages = new LabResult().calAge((DateTime)PatientRegis.trn_patient.tpt_dob, (DateTime)PatientRegis.tpr_arrive_date);
                        List<RealResultLab> listRealResult = getResult(tpr_id, listLabResult, PatientRegis.tpr_en_no, PatientRegis.trn_patient.tpt_gender, ages);
                        foreach (var tplc in PatientRegis.trn_patient_lab_charts)
                        {
                            tplc.tplc_active = false;
                            tplc.tplc_update_date = dateNow;
                        }
                        foreach (var grp in listRealResult.Where(x => x.isLabVitalSign == false).GroupBy(x => new { x.mlg_id, x.mlg_code, x.mlg_ename }).OrderBy(x => x.Key.mlg_id))
                        {
                            trn_patient_ass_hdr hdr = ass_grp.trn_patient_ass_hdrs
                                                             .Where(x => x.tpeh_order_code == grp.Key.mlg_code)
                                                             .FirstOrDefault();
                            if (hdr == null)
                            {
                                hdr = new trn_patient_ass_hdr
                                {
                                    tpeh_order_code = grp.Key.mlg_code,
                                    tpeh_order_name = grp.Key.mlg_ename,
                                    tpeh_create_date = dateNow,
                                    tpeh_create_by = username
                                };
                                ass_grp.trn_patient_ass_hdrs.Add(hdr);
                            }
                            hdr.tpeh_update_date = dateNow;
                            hdr.tpeh_update_by = username;
                            foreach (RealResultLab result in grp)
                            {
                                if (result.use_chart == true)
                                {
                                    trn_patient_lab_chart tplc = PatientRegis.trn_patient_lab_charts
                                                                             .Where(x => x.tplc_lab_no == result.labNo)
                                                                             .FirstOrDefault();
                                    if (tplc == null)
                                    {
                                        tplc = new trn_patient_lab_chart
                                        {
                                            tplc_lab_no = result.labNo,
                                            tplc_create_date = dateNow
                                        };
                                        PatientRegis.trn_patient_lab_charts.Add(tplc);
                                    }
                                    tplc.tplc_active = true;
                                    tplc.mlch_id = result.mlch_id;
                                    tplc.tplc_update_date = dateNow;
                                }
                                trn_patient_ass_dtl dtl = hdr.trn_patient_ass_dtls
                                                             .Where(x => x.tped_lab_code == result.labNo)
                                                             .FirstOrDefault();
                                if (dtl == null)
                                {
                                    dtl = new trn_patient_ass_dtl
                                    {
                                        tped_lab_code = result.labNo,
                                        tped_lab_name = result.labName,
                                        tped_create_date = dateNow,
                                        tped_create_by = username
                                    };
                                    hdr.trn_patient_ass_dtls.Add(dtl);
                                }
                                dtl.tped_lab_value = result.labValue;
                                dtl.mlr_id = result.mlr_id;
                                dtl.tped_summary = string.IsNullOrEmpty(result.status) ? (char?)null : Convert.ToChar(result.status);
                                dtl.tped_lab_unit = result.labUnit;
                                dtl.tped_lab_nrange = result.normalRange;
                                dtl.tped_lab_result_eng = result.descEn;
                                dtl.tped_lab_result_thai = result.descTh;
                                dtl.tped_update_date = dateNow;
                                dtl.tped_update_by = username;
                            }
                            List<trn_patient_ass_dtl> listAb = hdr.trn_patient_ass_dtls.Where(x => x.tped_summary == 'A').ToList();
                            List<string> en = listAb.Select(x => x.tped_lab_result_eng).ToList();
                            List<string> th = listAb.Select(x => x.tped_lab_result_thai).ToList();
                            hdr.tpeh_status = grp.Any(x => x.labStatus == "I") ? 'I' : 'E';
                            hdr.tpeh_summary = hdr.trn_patient_ass_dtls.Any(x => x.tped_summary == 'A') ? 'A' : 'N';
                            hdr.tpeh_pat_education = string.Join(", ", th);
                            hdr.tpeh_update_date = dateNow;
                        }

                        trn_basic_measure_hdr bsHdr = PatientRegis.trn_basic_measure_hdrs.FirstOrDefault();
                        List<trn_basic_measure_dtl> bsDtl = new List<trn_basic_measure_dtl>();
                        if (bsHdr != null)
                        {
                            bsDtl = bsHdr.trn_basic_measure_dtls.OrderByDescending(x => x.tbd_date).Take(3).ToList();
                        }

                        if (PatientRegis.trn_patient_vitalsign_labdate == null)
                        {
                            PatientRegis.trn_patient_vitalsign_labdate = new trn_patient_vitalsign_labdate();
                        }

                        trn_patient_vitalsign_labdate vitLabDate = PatientRegis.trn_patient_vitalsign_labdate;
                        vitLabDate.tpvl_result_date_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_date;
                        vitLabDate.tpvl_result_date_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_date;
                        vitLabDate.tpvl_result_date_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_date;

                        List<string> labVitCode = new List<string> { "BP001", "PS001", "TP001", "RP001", "BM001", "VS001" };
                        foreach (string code in labVitCode)
                        {
                            trn_patient_lab_vitalsign labVit = PatientRegis.trn_patient_lab_vitalsigns
                                                                           .Where(x => x.tplv_lab_code == code)
                                                                           .FirstOrDefault();
                            if (labVit == null)
                            {
                                labVit = new trn_patient_lab_vitalsign();
                                PatientRegis.trn_patient_lab_vitalsigns.Add(labVit);
                                labVit.tplv_create_date = dateNow;
                                labVit.tplv_lab_code = code;
                            }
                            switch (code)
                            {
                                case "BP001":
                                    labVit.tplv_lab_name_th = "ความดันโลหิต";
                                    labVit.tplv_lab_name_en = "Blood Pressure";
                                    labVit.tplv_lab_unit_th = "มิลลิเมตรปรอท";
                                    labVit.tplv_lab_unit_en = "mmHg";
                                    labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : (string.IsNullOrEmpty(bsDtl[0].tbd_systolic) ? "" : bsDtl[0].tbd_systolic) + "/" +
                                                                                        (string.IsNullOrEmpty(bsDtl[0].tbd_diastolic) ? "" : bsDtl[0].tbd_diastolic);
                                    labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : (string.IsNullOrEmpty(bsDtl[1].tbd_systolic) ? "" : bsDtl[1].tbd_systolic) + "/" +
                                                                                        (string.IsNullOrEmpty(bsDtl[1].tbd_diastolic) ? "" : bsDtl[1].tbd_diastolic);
                                    labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : (string.IsNullOrEmpty(bsDtl[2].tbd_systolic) ? "" : bsDtl[2].tbd_systolic) + "/" +
                                                                                        (string.IsNullOrEmpty(bsDtl[2].tbd_diastolic) ? "" : bsDtl[2].tbd_diastolic);
                                    break;
                                case "PS001":
                                    labVit.tplv_lab_name_th = "ชีพจร";
                                    labVit.tplv_lab_name_en = "Pulse rate";
                                    labVit.tplv_lab_unit_th = "ครั้งต่อนาที";
                                    labVit.tplv_lab_unit_en = "beat / minute";
                                    labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_pulse;
                                    labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_pulse;
                                    labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_pulse;
                                    break;
                                case "RP001":
                                    labVit.tplv_lab_name_th = "อัตราการหายใจ";
                                    labVit.tplv_lab_name_en = "Respiration Rate";
                                    labVit.tplv_lab_unit_th = "ครั้งต่อนาที";
                                    labVit.tplv_lab_unit_en = "beat / minute";
                                    labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_rr;
                                    labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_rr;
                                    labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_rr;
                                    break;
                                case "TP001":
                                    labVit.tplv_lab_name_th = "อุณหภูมิ";
                                    labVit.tplv_lab_name_en = "Temperature";
                                    labVit.tplv_lab_unit_th = "องศาเซลเซียส";
                                    labVit.tplv_lab_unit_en = "°C";
                                    labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_temp;
                                    labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_temp;
                                    labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_temp;
                                    break;
                            }
                            labVit.tplv_update_date = dateNow;
                        }

                        foreach (var grp in listRealResult.Where(x => x.isLabVitalSign == true))
                        {
                            trn_patient_lab_vitalsign labVit = PatientRegis.trn_patient_lab_vitalsigns
                                                                           .Where(x => x.tplv_lab_code == grp.labNo)
                                                                           .FirstOrDefault();
                            labVit.tplv_lab_result_th = grp.descTh;
                            labVit.tplv_lab_result_en = grp.descEn;
                            labVit.tplv_summary = grp.status;
                        }
                        cdc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("get_lab_result.Class.GetLabResultCls", "Insert_trn_patient_ass(int tpr_id)", ex.Message);
            }
        }
        public void Insert_trn_patient_assBackground(int tpr_id)
        {
            System.Threading.Thread currentTimeThread = new System.Threading.Thread(delegate()
            {
                Insert_trn_patient_ass(tpr_id);
            });
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start();
        }

        public void Insert_trn_patient_lab(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = globalCls.GetServerDateTime();
                    trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (PatientRegis != null)
                    {
                        EnumerableRowCollection<DataRow> ResultWS_GetCheckupLab = retrieveLab(PatientRegis.trn_patient.tpt_hn_no, dateNow.AddYears(-5), dateNow);
                        List<trn_patient_lab> listPatientLab = getPatientLab(ResultWS_GetCheckupLab);

                        foreach (trn_patient_lab patientLab in listPatientLab)
                        {
                            trn_patient_lab pLab = PatientRegis.trn_patient_labs
                                                               .Where(x => x.tpl_lab_no == patientLab.tpl_lab_no &&
                                                                           x.tpl_en_no == patientLab.tpl_en_no)
                                                               .FirstOrDefault();
                            if (pLab == null)
                            {
                                pLab = new trn_patient_lab
                                {
                                    tpl_rowid = patientLab.tpl_rowid,
                                    tpl_hn_no = patientLab.tpl_hn_no,
                                    tpl_en_no = patientLab.tpl_en_no,
                                    tpl_create_date = patientLab.tpl_create_date
                                };
                                PatientRegis.trn_patient_labs.Add(pLab);
                            }
                            pLab.tpl_patient_age = patientLab.tpl_patient_age;
                            pLab.tpl_patient_sex = patientLab.tpl_patient_sex;
                            pLab.tpl_lab_date = patientLab.tpl_lab_date;
                            pLab.tpl_head_lab_no = patientLab.tpl_head_lab_no;
                            pLab.tpl_head_lab = patientLab.tpl_head_lab;
                            pLab.tpl_lab_no = patientLab.tpl_lab_no;
                            pLab.tpl_lab_name = patientLab.tpl_lab_name;
                            pLab.tpl_lab_value = patientLab.tpl_lab_value;
                            pLab.tpl_lab_unit = patientLab.tpl_lab_unit;
                            pLab.tpl_lab_range = patientLab.tpl_lab_range;
                            pLab.tpl_lab_rsl = patientLab.tpl_lab_rsl;
                            pLab.tpl_lab_hl = patientLab.tpl_lab_hl;
                            pLab.tpl_lab_frs = patientLab.tpl_lab_frs;
                            pLab.tpl_status = patientLab.tpl_status;
                            pLab.tpl_mhs_id = patientLab.tpl_mhs_id;
                            pLab.tpl_mhs_tname = patientLab.tpl_mhs_tname;
                            pLab.tpl_mhs_ename = patientLab.tpl_mhs_ename;
                            pLab.tpl_update_date = dateNow;
                            pLab.tpl_lab_result_seq = patientLab.tpl_lab_result_seq;
                        }
                        cdc.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("get_lab_result.Class.GetLabResultCls", "Insert_trn_patient_lab(int tpr_id)", ex.Message);
            }
        }
        public void Insert_trn_patient_labBackground(int tpr_id)
        {
            System.Threading.Thread currentTimeThread = new System.Threading.Thread(delegate()
            {
                Insert_trn_patient_lab(tpr_id);
            });
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start();
        }

        private class RealResultLab
        {
            public int mlg_id { get; set; }
            public string mlg_code { get; set; }
            public string mlg_ename { get; set; }
            public string labNo { get; set; }
            public string labName { get; set; }
            public string labUnit { get; set; }
            public int mlb_id { get; set; }
            public string labValue { get; set; }
            public int? mlr_id { get; set; }
            public string normalRange { get; set; }
            public string status { get; set; }
            public string descEn { get; set; }
            public string descTh { get; set; }
            private bool _isLabVitalSign = false;
            public bool isLabVitalSign
            {
                get { return _isLabVitalSign; }
                set
                {
                    if (_isLabVitalSign != value)
                    {
                        _isLabVitalSign = value;
                    }
                }
            }
            public string labStatus { get; set; }
            public bool use_chart { get; set; }
            public int mlch_id { get; set; }
        }
        private List<RealResultLab> getResult(int? tpr_id, List<LabResult> chkupLab, string en, char? gender, double ages)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                DateTime dateNow = DateTime.Now;
                List<RealResultLab> listResult = new List<RealResultLab>();

                List<Lab_Config> labConfig = getLabConfig((int)tpr_id, en);

                foreach (LabResult lab in chkupLab.Where(x => x.en == en && x.labStatus == "E"))
                {
                    mst_lab mstLab = cdc.mst_labs.Where(x => x.mlb_code == lab.labNo && x.mlb_status == 'A').FirstOrDefault();
                    if (mstLab != null)
                    {
                        RealResultLab result = getResult(mstLab, lab, labConfig, ages, gender);
                        if (result != null) listResult.Add(result);
                    }
                }

                List<mst_lab> labSpecial = cdc.mst_labs.Where(x => x.mlb_type == 'S' && x.mlb_status == 'A').ToList();
                foreach (mst_lab ls in labSpecial)
                {
                    RealResultLab result = getResult(ls, null, labConfig, ages, gender);
                    try
                    {
                        if (result != null && result.mlr_id != null) listResult.Add(result);
                    }
                    catch
                    {

                    }
                }

                List<string> labVitalSign = new List<string> { "BP001", "PS001", "TP001", "RP001", "BM001", "VS001" };
                foreach (string lab in labVitalSign)
                {
                    mst_lab mstLab = cdc.mst_labs.Where(x => x.mlb_code == lab && x.mlb_status == 'A').FirstOrDefault();
                    if (mstLab != null)
                    {
                        RealResultLab result = getResult(mstLab, null, labConfig, ages, gender);
                        if (result != null)
                        {
                            result.isLabVitalSign = true;
                            listResult.Add(result);
                        }
                    }
                }
                return listResult;
            }
        }
        private RealResultLab getResult(mst_lab mstLab, LabResult lab, List<Lab_Config> labConfig, double arrived_date_ages, char? gender, bool isLabVitalSign = false)
        {
            RealResultLab result = new RealResultLab
            {
                mlg_id = (int)mstLab.mlg_id,
                mlg_code = mstLab.mst_lab_group.mlg_code,
                mlg_ename = mstLab.mst_lab_group.mlg_ename,
                mlb_id = mstLab.mlb_id,
                labUnit = lab == null ? null : lab.labUnit,
                labNo = mstLab.mlb_code,
                labName = mstLab.mlb_ename,
                labValue = lab == null ? null : lab.flagValue == "S" ? lab.labValueString : lab.labValueNumber,
                isLabVitalSign = isLabVitalSign,
                labStatus = lab == null ? null : lab.labStatus
            };
            try
            {
                bool val = false;
                double ages = 0;
                if (lab == null)
                {
                    ages = arrived_date_ages;
                }
                else
                {
                    ages = lab.ages;
                }

                string LabCalValue = "";
                if (lab != null)
                {
                    if (lab.flagValue == "S")
                    {
                        LabCalValue = @"""" + lab.labValueString + @"""";
                        result.labValue = lab.labValueString;
                    }
                    else
                    {
                        LabCalValue = lab.labValueNumber;
                        if (lab.labValueNumber.IndexOf('<') >= 0)
                        {
                            try
                            {
                                LabCalValue = (Convert.ToDouble(lab.labValueNumber.Replace("<", "")) - 0.1).ToString();
                            }
                            catch
                            {

                            }
                        }
                        else if (lab.labValueNumber.IndexOf('>') >= 0)
                        {
                            try
                            {
                                LabCalValue = (Convert.ToDouble(lab.labValueNumber.Replace(">", "")) + 0.1).ToString();
                            }
                            catch
                            {

                            }
                        }
                        result.labValue = InsertZeroBeforeDot(lab.labValueNumber);
                    }
                }

                mst_lab_age mstLabAge = mstLab.mst_lab_ages.Where(x => x.mla_max_age.Value + (x.mla_max_day.Value / 365) >= ages &&
                                                                       x.mla_min_age.Value + (x.mla_min_day.Value / 365) <= ages &&
                                                                       x.mla_sex == gender &&
                                                                       x.mla_status == 'A')
                                              .FirstOrDefault();
                if (mstLabAge != null)
                {
                    if (!new List<string> { "N7007", "N0390" }.Contains(mstLab.mlb_code)) //CEA
                    {
                        result.normalRange = mstLabAge.mla_vstand_nrange;
                    }
                    else
                    {
                        var smoke = labConfig.Where(x => x.mlc_code == "|SMOKE|").Select(x => x.mlc_value).FirstOrDefault();
                        if (smoke == "\"True\"")
                        {
                            result.normalRange = "Smoke(0-5.5)";
                        }
                        else
                        {
                            result.normalRange = mstLabAge.mla_vstand_nrange;
                        }
                    }
                    result.labUnit = mstLabAge.mla_vstand_unit;
                    if (mstLab.mlb_use_chart == true)
                    {
                        result.use_chart = true;
                        if (!new List<string> { "N7007", "N0390" }.Contains(mstLab.mlb_code)) //CEA
                        {
                            result.mlch_id = new MstLabChartCls().GetID(result.labNo, mstLabAge.mla_value_min, mstLabAge.mla_value_max, mstLabAge.mla_vstand_min, mstLabAge.mla_vstand_max, result.normalRange + ' ' + result.labUnit, LabCalValue, result.labValue);
                        }
                        else
                        {
                            var smoke = labConfig.Where(x => x.mlc_code == "|SMOKE|").Select(x => x.mlc_value).FirstOrDefault();
                            if (smoke == "\"True\"")
                            {
                                result.use_chart = true;
                                result.mlch_id = new MstLabChartCls().GetID(result.labNo, mstLabAge.mla_value_min, mstLabAge.mla_value_max, 0, 5.5, "Non-Smoke(0-5.5)", LabCalValue, result.labValue);
                            }
                            else
                            {
                                result.use_chart = true;
                                result.mlch_id = new MstLabChartCls().GetID(result.labNo, mstLabAge.mla_value_min, mstLabAge.mla_value_max, mstLabAge.mla_vstand_min, mstLabAge.mla_vstand_max, result.normalRange + ' ' + result.labUnit, LabCalValue, result.labValue);
                            }
                        }
                    }
                    else
                    {
                        result.use_chart = false;
                    }

                    foreach (mst_lab_result mstLabRes in mstLabAge.mst_lab_results.Where(x => x.mlp_status == 'A').OrderBy(x => x.mlp_cond_seq))
                    {
                        string condition = mstLabRes.mlp_condition;
                        if (labConfig.Count > 0)
                        {
                            foreach (Lab_Config config in labConfig)
                            {
                                condition = condition.Replace(config.mlc_code, config.mlc_value);
                            }
                        }
                        if (lab != null)
                        {
                            condition = condition.Replace("?", LabCalValue);
                        }
                        val = Class.CompilerStringCls.CheckCondition(condition);
                        if (val)
                        {
                            result.mlr_id = mstLabRes.mlr_id;
                            result.status = mstLabRes.mlp_summary == null ? null : mstLabRes.mlp_summary.ToString();
                            if (result.mlr_id != null)
                            {
                                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                                {
                                    result.descTh = cdc.mst_lab_recoms.Where(x => x.mlr_id == result.mlr_id).Select(x => x.mlr_th_name).FirstOrDefault();
                                    result.descEn = cdc.mst_lab_recoms.Where(x => x.mlr_id == result.mlr_id).Select(x => x.mlr_en_name).FirstOrDefault();
                                }
                            }
                            break;
                        }
                    }
                }
                //if (lab != null)
                //{
                //    if (result.normalRange == null)
                //    {
                //        result.normalRange = convertNormalRange(lab.labNormalRange);
                //    }
                //    if (result.labUnit == null)
                //    {
                //        result.labUnit = lab.labUnit;
                //    }
                //}
                return result;
            }
            catch
            {
                return result;
            }
        }
        private class LabResult
        {
            public string hn { get; set; }
            public string en { get; set; }
            private DateTime? _dob;
            public DateTime? dob
            {
                get { return _dob; }
                set
                {
                    if (value != null && _labDate != null)
                    {
                        ages = calAge((DateTime)value, (DateTime)_labDate);
                        //diffDate((DateTime)value, (DateTime)_labDate, ref _ageYears, ref _ageDays);
                    }
                    _dob = value;
                }
            }
            public double ages { get; set; }
            private DateTime? _labDate;
            public DateTime? labDate
            {
                get { return _labDate; }
                set
                {
                    if (value != null && _dob != null)
                    {
                        ages = calAge((DateTime)_dob, (DateTime)value);
                        //diffDate((DateTime)_dob, (DateTime)value, ref _ageYears, ref _ageDays);
                    }
                    _labDate = value;
                }
            }
            public string flagValue { get; set; }
            public string labNo { get; set; }
            public string labValueNumber { get; set; }
            public string labValueString { get; set; }
            public string labUnit { get; set; }
            public string labStatus { get; set; }
            public string labNormalRange { get; set; }

            public double calAge(DateTime StartDate, DateTime EndDate)
            {
                int years = 0;
                if (StartDate == null || EndDate == null)
                {
                    return 0;
                }
                years = EndDate.Year - StartDate.Year;
                if (EndDate < StartDate.AddYears(years) && years != 0) years--;
                StartDate = StartDate.AddYears(years);
                double days = Convert.ToDouble((EndDate - StartDate).Days) / Convert.ToDouble(365);
                double ages = years + days;
                return ages;
            }
        }
        private string convertNormalRange(string normalRange)
        {
            if (normalRange != null)
            {
                string newNormalRange = "";
                foreach (char chr in normalRange)
                {
                    if (chr == '.')
                    {
                        try
                        {
                            string lastString = newNormalRange.Substring(newNormalRange.Length - 1, 1);
                            int a = Convert.ToInt32(lastString);
                        }
                        catch
                        {
                            newNormalRange = newNormalRange + "0";
                        }
                    }
                    newNormalRange = newNormalRange + chr.ToString();
                }
                return newNormalRange;
            }
            else
            {
                return null;
            }
        }

        public EnumerableRowCollection<DataRow> retrieveLab(string hn, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var result = ws.GetCheckupLab(hn, dateFrom.ToString("yyyy-MM-dd", new CultureInfo("en-US")), dateTo.ToString("yyyy-MM-dd", new CultureInfo("en-US"))).AsEnumerable();
                    foreach (var res in result)
                    {
                        if (res.Field<string>("CTTC_CDE") == "T0701")
                        {
                            try
                            {
                                List<string> ls = new List<string>();
                                var str = res.Field<string>("RSL").Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var s in str)
                                {
                                    var tmp = s.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                    ls.Add(string.Join(" ", tmp));
                                }
                                var r = string.Join(Environment.NewLine, ls);
                                res["RSL"] = r;
                            }
                            catch
                            {

                            }
                        }
                        string[] en_split = res.Field<string>("EPI_NO").Split(new char[] { ',' });
                        if (en_split.Count() > 1)
                        {
                            res["EPI_NO"] = en_split[0];
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
            return null;
        }
        private class mapSite
        {
            public int? mhs_id { get; set; }
            public string mhs_tname { get; set; }
            public string mhs_ename { get; set; }
            public string mhs_code { get; set; }
            public string destiny_mhs_code { get; set; }
        }
        public List<trn_patient_lab> getPatientLab(EnumerableRowCollection<DataRow> ResultWS_GetCheckupLab)
        {
            try
            {
                List<mapSite> map_site;
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    map_site = cdc.mst_hpc_sites.Where(x => x.mhs_status == 'A' && x.mhs_other_clinic != true)
                                    .Select(x => new mapSite
                                    {
                                        mhs_id = x.mhs_id,
                                        mhs_code = x.mhs_code,
                                        destiny_mhs_code = x.mhs_code,
                                        mhs_tname = x.mhs_tname,
                                        mhs_ename = x.mhs_ename
                                    }).ToList();
                    mapSite siteAMS = map_site.Where(x => x.mhs_code == "01AMS").FirstOrDefault();
                    if (siteAMS != null)
                    {
                        map_site.Add(new mapSite
                        {
                            mhs_id = siteAMS.mhs_id,
                            mhs_code = "01AMSCHK",
                            destiny_mhs_code = siteAMS.destiny_mhs_code,
                            mhs_tname = siteAMS.mhs_tname,
                            mhs_ename = siteAMS.mhs_ename
                        });
                    }
                }

                List<trn_patient_lab> listPatientLab = new List<trn_patient_lab>();
                foreach (var x in ResultWS_GetCheckupLab)
                {
                    try
                    {
                        trn_patient_lab pl = new trn_patient_lab
                        {
                            tpl_rowid = x.Field<string>("EPVISVisitNumber"),
                            tpl_hn_no = x.Field<string>("DBT_CDE"),
                            tpl_en_no = x.Field<string>("EPI_NO"),
                            tpl_patient_age = Convert.ToInt32(x.Field<string>("EPVIS_AGE")),
                            tpl_patient_sex = x.Field<string>("EPVIS_SEX") == "Male" ? "M" : x.Field<string>("EPVIS_SEX") == "Female" ? "F" : "",
                            tpl_lab_date = new DateTime(
                                            Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(0, 4)),
                                            Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(5, 2)),
                                            Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(8, 2)),
                                            (int)(Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) / 60),
                                            (int)(Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) % 60),
                                            0
                                            ),
                            tpl_head_lab_no = x.Field<string>("CTTS_CDE"),
                            tpl_head_lab = x.Field<string>("CTTS_NME"),
                            tpl_lab_no = x.Field<string>("CTTC_CDE"),
                            tpl_lab_name = x.Field<string>("CTTC_DES"),
                            tpl_lab_value = x.Field<string>("CTTC_RSL_FMT").Contains("N") && x.Field<string>("TST_DTA").Trim().Length > 0 ?
                                                x.Field<string>("TST_DTA").Trim().Substring(0, 1) == "." ? "0" + x.Field<string>("TST_DTA")
                                                : x.Field<string>("TST_DTA")
                                            : x.Field<string>("TST_DTA"),
                            tpl_lab_unit = x.Field<string>("CTTC_UNT"),
                            tpl_lab_range = convertNormalRange(x.Field<string>("NML")),
                            tpl_lab_rsl = x.Field<string>("RSL") == null || x.Field<string>("RSL").Trim() == "" ? null : x.Field<string>("RSL").Trim(),
                            tpl_lab_hl = x.Field<string>("HL") == "H" ? 1 : x.Field<string>("HL") == "L" ? -1 : 0,
                            tpl_lab_frs = x.Field<string>("CTTC_RSL_FMT").Contains("N") ? 1 :
                                        x.Field<string>("CTTC_RSL_FMT") == "S" ? 2 :
                                        x.Field<string>("CTTC_RSL_FMT") == "X" ? 0 :
                                        -1,
                            tpl_status = x.Field<string>("Status") == "Executed" ? 'E' : 'I',
                            tpl_mhs_id = map_site.Where(y => y.mhs_code == x.Field<string>("CTHOS_CDE")).Select(y => y.mhs_id).FirstOrDefault(),
                            tpl_mhs_tname = map_site.Where(y => y.mhs_code == x.Field<string>("CTHOS_CDE")).Select(y => y.mhs_tname).FirstOrDefault(),
                            tpl_mhs_ename = map_site.Where(y => y.mhs_code == x.Field<string>("CTHOS_CDE")).Select(y => y.mhs_ename).FirstOrDefault(),
                            tpl_lab_result_seq = (x.Field<string>("Sequen") == null || x.Field<string>("Sequen") == "") ? (int?)null :
                                                !Information.IsNumeric(x.Field<string>("Sequen")) ? (int?)null :
                                                Convert.ToInt32(x.Field<string>("Sequen"))
                        };
                        listPatientLab.Add(pl);
                    }
                    catch
                    {

                    }
                }
                return listPatientLab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private List<LabResult> getLabResult(EnumerableRowCollection<DataRow> ResultWS_GetCheckupLab)
        {
            try
            {
                List<LabResult> listRealResultLab = ResultWS_GetCheckupLab.Select(x => new LabResult
                {
                    en = x.Field<string>("EPI_NO"),
                    hn = x.Field<string>("DBT_CDE"),
                    labDate = new DateTime(
                              Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(0, 4)),
                              Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(5, 2)),
                              Convert.ToInt32(x.Field<string>("EPIVISDateOfCollection").Substring(8, 2)),
                              (int)(Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) / 60),
                              (int)(Convert.ToInt32(x.Field<string>("EPIVISTimeOfCollection")) % 60),
                              0),
                    labNo = x.Field<string>("CTTC_CDE"),
                    labValueNumber = x.Field<string>("TST_DTA"),
                    labValueString = x.Field<string>("RSL") == null || x.Field<string>("RSL").Trim() == "" ? null : x.Field<string>("RSL").Trim(),
                    flagValue = x.Field<string>("CTTC_RSL_FMT"),
                    labUnit = x.Field<string>("CTTC_UNT"),
                    labStatus = x.Field<string>("Status") == "Executed" ? "E" : "I",
                    labNormalRange = convertNormalRange(x.Field<string>("NML"))
                }).ToList();
                return listRealResultLab;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public class Lab_Config
        {
            public string mlc_code { get; set; }
            public string mlc_value { get; set; }
        }
        public List<Lab_Config> getLabConfig(int tpr_id, string en)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<Lab_Config> labConfig = new List<Lab_Config>();
                    var mstLabConfig = cdc.mst_lab_configs.Where(x => x.mlc_status == 'A')
                                          .Select(x => new
                                          {
                                              x.mlc_code,
                                              x.mlc_type,
                                              x.mlc_sql
                                          }).ToList();
                    foreach (var config in mstLabConfig)
                    {
                        string sql = config.mlc_sql.Replace(@"|tpr_id|", tpr_id.ToString()).Replace(@"|en_no|", "'" + en + "'");
                        string res = cdc.ExecuteQuery<string>(sql).FirstOrDefault();
                        labConfig.Add(new Lab_Config
                        {
                            mlc_code = config.mlc_code,
                            mlc_value = config.mlc_type == 'N' ? res : @"""" + res + @""""
                        });
                    }
                    return labConfig;
                }
            }
            catch (Exception ex)
            {
                return new List<Lab_Config>();
            }
        }

        private List<RealResultLab> getResultVitalSign(int? tpr_id, char? gender, double ages, List<Lab_Config> labConfig)
        {
            List<RealResultLab> listResult = new List<RealResultLab>();
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<string> labVitalSign = new List<string> { "BP001", "PS001", "TP001", "RP001", "BM001", "VS001" };
                    foreach (string lab in labVitalSign)
                    {
                        mst_lab mstLab = cdc.mst_labs.Where(x => x.mlb_code == lab && x.mlb_status == 'A').FirstOrDefault();
                        if (mstLab != null)
                        {
                            RealResultLab result = getResult(mstLab, null, labConfig, ages, gender);
                            if (result != null)
                            {
                                result.isLabVitalSign = true;
                                listResult.Add(result);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("WebService", "getResultVitalSign", ex.Message);
            }
            return listResult;
        }
        public void InsertLabVitalSign(int tpr_id, string username = null)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                trn_patient_regi PatientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                if (PatientRegis != null)
                {
                    DateTime dateNow = globalCls.GetServerDateTime();
                    double ages = new LabResult().calAge(PatientRegis.trn_patient.tpt_dob.Value, PatientRegis.trn_patient_regis_detail.tpr_real_arrived_date.Value);
                    List<Lab_Config> LabConfig = getLabConfig(tpr_id, PatientRegis.tpr_en_no);
                    List<RealResultLab> InTerPretedLabs = getResultVitalSign(tpr_id, PatientRegis.trn_patient.tpt_gender, ages, LabConfig);

                    trn_basic_measure_hdr bsHdr = PatientRegis.trn_basic_measure_hdrs.FirstOrDefault();
                    List<trn_basic_measure_dtl> bsDtl = new List<trn_basic_measure_dtl>();
                    if (bsHdr != null)
                    {
                        bsDtl = bsHdr.trn_basic_measure_dtls.OrderByDescending(x => x.tbd_date).Take(3).ToList();
                    }

                    if (PatientRegis.trn_patient_vitalsign_labdate == null)
                    {
                        PatientRegis.trn_patient_vitalsign_labdate = new trn_patient_vitalsign_labdate();
                    }

                    trn_patient_vitalsign_labdate vitLabDate = PatientRegis.trn_patient_vitalsign_labdate;
                    vitLabDate.tpvl_result_date_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_date;
                    vitLabDate.tpvl_result_date_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_date;
                    vitLabDate.tpvl_result_date_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_date;


                    foreach (var interPretedLab in InTerPretedLabs)
                    {
                        trn_patient_lab_vitalsign labVit = PatientRegis.trn_patient_lab_vitalsigns
                                                                        .Where(x => x.tplv_lab_code == interPretedLab.labNo)
                                                                        .FirstOrDefault();
                        if (labVit == null)
                        {
                            labVit = new trn_patient_lab_vitalsign();
                            PatientRegis.trn_patient_lab_vitalsigns.Add(labVit);
                            labVit.tplv_create_date = dateNow;
                            labVit.tplv_lab_code = interPretedLab.labNo;
                        }
                        switch (interPretedLab.labNo)
                        {
                            case "BP001":
                                labVit.tplv_lab_name_th = "ความดันโลหิต";
                                labVit.tplv_lab_name_en = "Blood Pressure";
                                labVit.tplv_lab_unit_th = "มิลลิเมตรปรอท";
                                labVit.tplv_lab_unit_en = "mmHg";
                                labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : (string.IsNullOrEmpty(bsDtl[0].tbd_systolic) ? "" : bsDtl[0].tbd_systolic) + "/" +
                                                                                    (string.IsNullOrEmpty(bsDtl[0].tbd_diastolic) ? "" : bsDtl[0].tbd_diastolic);
                                labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : (string.IsNullOrEmpty(bsDtl[1].tbd_systolic) ? "" : bsDtl[1].tbd_systolic) + "/" +
                                                                                    (string.IsNullOrEmpty(bsDtl[1].tbd_diastolic) ? "" : bsDtl[1].tbd_diastolic);
                                labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : (string.IsNullOrEmpty(bsDtl[2].tbd_systolic) ? "" : bsDtl[2].tbd_systolic) + "/" +
                                                                                    (string.IsNullOrEmpty(bsDtl[2].tbd_diastolic) ? "" : bsDtl[2].tbd_diastolic);
                                break;
                            case "PS001":
                                labVit.tplv_lab_name_th = "ชีพจร";
                                labVit.tplv_lab_name_en = "Pulse rate";
                                labVit.tplv_lab_unit_th = "ครั้งต่อนาที";
                                labVit.tplv_lab_unit_en = "beat / minute";
                                labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_pulse;
                                labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_pulse;
                                labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_pulse;
                                break;
                            case "RP001":
                                labVit.tplv_lab_name_th = "อัตราการหายใจ";
                                labVit.tplv_lab_name_en = "Respiration Rate";
                                labVit.tplv_lab_unit_th = "ครั้งต่อนาที";
                                labVit.tplv_lab_unit_en = "beat / minute";
                                labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_rr;
                                labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_rr;
                                labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_rr;
                                break;
                            case "TP001":
                                labVit.tplv_lab_name_th = "อุณหภูมิ";
                                labVit.tplv_lab_name_en = "Temperature";
                                labVit.tplv_lab_unit_th = "องศาเซลเซียส";
                                labVit.tplv_lab_unit_en = "°C";
                                labVit.tplv_result_1 = bsDtl.Count() == 0 ? null : bsDtl[0].tbd_temp;
                                labVit.tplv_result_2 = bsDtl.Count() <= 1 ? null : bsDtl[1].tbd_temp;
                                labVit.tplv_result_3 = bsDtl.Count() <= 2 ? null : bsDtl[2].tbd_temp;
                                break;
                        }
                        labVit.tplv_lab_result_th = interPretedLab.descTh;
                        labVit.tplv_lab_result_en = interPretedLab.descEn;
                        labVit.tplv_summary = interPretedLab.status;
                        labVit.tplv_update_date = dateNow;
                    }

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
                }
            }
        }

        private string InsertZeroBeforeDot(string val)
        {
            try
            {
                string newVal = "";
                for (int i = 0; i < val.Length; i++)
                {
                    bool found = false;
                    string cur = val.Substring(i, 1);
                    int v;
                    if (!found)
                    {
                        if (cur == ".")
                        {
                            if (newVal.Length == 0 || !Int32.TryParse(newVal.Substring(newVal.Length - 1, 1), out v))
                            {
                                found = true;
                                newVal += "0";
                            }
                        }
                    }
                    newVal += cur;
                }
                return newVal;
            }
            catch
            {

            }
            return val;
        }
    }
}
