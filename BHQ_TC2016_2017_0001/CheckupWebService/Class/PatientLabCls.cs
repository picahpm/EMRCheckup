using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public class PatientLabCls
    {
        public bool InsertByCheckupLabResult(int tpr_id, string user)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var pregis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    if (pregis != null)
                    {
                        DateTime dateNow = Class.globalCls.GetServerDateTime();
                        DateTime dateStart = pregis.trn_patient_regis_detail != null && pregis.trn_patient_regis_detail.tpr_real_arrived_date != null
                                             ? pregis.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date.AddYears(-5)
                                             : pregis.tpr_arrive_date == null
                                             ? dateNow.AddYears(-5)
                                             : pregis.tpr_arrive_date.Value.Date.AddYears(-5);
                        var checkuplabs = new APITrakcare.GetCheckupLabCls().ByGetCheckupLab(pregis.trn_patient.tpt_hn_no, dateStart, dateNow);
                        InsertByCheckupLabResult(ref pregis, checkuplabs, dateNow, user);
                        cdc.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("PatientLabCls", "InsertByCheckupLabResult", ex.Message);
                return false;
            }
        }
    public void InsertByCheckupLabResult(ref trn_patient_regi pregis, APITrakcare.CheckupLabResult checkuplabs, DateTime dateNow, string user)
        {
            try
            {
                foreach (var plab in pregis.trn_patient_labs)
                {
                    plab.tpl_status = 'D';
                }
                foreach (var ep in checkuplabs.episodes)
                {
                    foreach (var labdate in ep.labdates)
                    {
                        foreach (var lab in labdate.labs)
                        {
                            if (lab.status != 'D')
                            {
                                var plab = pregis.trn_patient_labs.Where(x => x.tpl_en_no == ep.en && x.tpl_lab_no == lab.code).FirstOrDefault();
                                if (plab == null)
                                {
                                    plab = new trn_patient_lab
                                    {
                                        tpl_rowid = lab.rowid,
                                        tpl_hn_no = checkuplabs.hn,
                                        tpl_en_no = ep.en,
                                        tpl_create_date = dateNow
                                    };
                                    pregis.trn_patient_labs.Add(plab);
                                }
                                plab.tpl_patient_age = lab.age;
                                plab.tpl_patient_sex = checkuplabs.sex;
                                plab.tpl_lab_result = lab.valuenumber;
                                plab.tpl_lab_date = labdate.labdate;
                                plab.tpl_head_lab_no = lab.headcode;
                                plab.tpl_head_lab = lab.headdesc;
                                plab.tpl_lab_no = lab.code;
                                plab.tpl_lab_name = lab.labname;
                                plab.tpl_lab_value = lab.valuenumber;
                                plab.tpl_lab_unit = lab.unit;
                                plab.tpl_lab_range = lab.range;
                                plab.tpl_lab_rsl = lab.valuestring;
                                plab.tpl_lab_hl = lab.hl;
                                plab.tpl_lab_frs = lab.frs;
                                plab.tpl_status = lab.status;
                                plab.tpl_mhs_id = lab.mhs_id;
                                plab.tpl_mhs_tname = lab.mhs_tname;
                                plab.tpl_mhs_ename = lab.mhs_ename;
                                plab.tpl_update_date = dateNow;
                                plab.tpl_lab_result_seq = lab.seq;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("PatientLabCls", "InsertByCheckupLabResult", ex.Message);
                throw ex;
            }
        }

        public void InsertByCheckupLabResultBackground(int tpr_id, string user)
        {
            System.Threading.Thread currentTimeThread = new System.Threading.Thread(delegate()
            {
                InsertByCheckupLabResult(tpr_id, user);
            });
            currentTimeThread.IsBackground = true;
            currentTimeThread.Start();
        }
    }
}