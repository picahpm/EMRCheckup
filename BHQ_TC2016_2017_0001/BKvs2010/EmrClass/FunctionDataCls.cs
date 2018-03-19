using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class FunctionDataCls
    {
        public List<mst_doc_result> getDoctorResult(int mhs_id, string mrm_code, string mrh_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int mrm_id = cdc.mst_room_hdrs.Where(x => x.mhs_id == 1 && x.mrm_code == mrm_code).Select(x => x.mrm_id).FirstOrDefault();
                    List<mst_doc_result> result = cdc.mst_doc_results
                                                  .Where(x => x.mst_doc_result_hdr.mrm_id == mrm_id &&
                                                              x.mst_doc_result_hdr.mrh_code == mrh_code &&
                                                              x.mdr_status == 'A').ToList();

                    return result;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("EmrClass.FunctionDataCls", "getDoctorResult", ex, false);
                return new List<mst_doc_result>();
            }
        }
        public List<clsSourceAutoCompleteDoctor> getSourceDoctor()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<clsSourceAutoCompleteDoctor> sourceDoctor = cdc.mst_user_types
                                                                     .Where(x => x.mut_type == 'D' &&
                                                                                 (x.mut_username != null || x.mut_username != "") &&
                                                                                 (x.mut_fullname != null || x.mut_fullname != ""))
                                                                     .Select(x => new clsSourceAutoCompleteDoctor
                                                                     {
                                                                         val = x.mut_username,
                                                                         dis = x.mut_fullname
                                                                     }).ToList();
                    return sourceDoctor;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("EmrClass.FunctionDataCls", "getSourceDoctor", ex, false);
                return new List<clsSourceAutoCompleteDoctor>();
            }
        }
        public bool RegisterPatient(tmp_getptarrived arrivedData)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();

                    EmrClass.GetDataFromWSTrakCare cls = new EmrClass.GetDataFromWSTrakCare();
                    trn_patient tpt = cdc.trn_patients.Where(x => x.tpt_hn_no == arrivedData.papmi_no).FirstOrDefault();
                    bool flagNewPatient = true;
                    if (tpt != null) flagNewPatient = false;
                    cls.otherClinicSkipToCheckB(arrivedData, ref tpt, Program.CurrentSite.mhs_id);//add Program.CurrentSite.mhs_id suriya 03/04/2015
                    List<trn_patient_regi> listRegis = tpt.trn_patient_regis.Where(x => x.tpr_en_no == arrivedData.paadm_admno).ToList();
                    foreach (trn_patient_regi regis in listRegis)
                    {
                        trn_doctor_hdr docHdr = regis.trn_doctor_hdrs.FirstOrDefault();
                        if (docHdr == null)
                        {
                            docHdr = new trn_doctor_hdr();
                            regis.trn_doctor_hdrs.Add(docHdr);
                            docHdr.trh_create_date = dateNow;
                            docHdr.trh_create_by = "SysImp";
                            docHdr.trh_update_date = dateNow;
                            docHdr.trh_update_by = "SysImp";
                        }
                    }
                    if (flagNewPatient) cdc.trn_patients.InsertOnSubmit(tpt);
                    cdc.SubmitChanges();
                    trn_patient_regi PatientRegis = tpt.trn_patient_regis.OrderByDescending(x => x.tpr_id).FirstOrDefault();
                    if (PatientRegis != null)
                    {
                        new EmrClass.GetPTPackageCls().setRelationOrderSet(ref PatientRegis);
                        cdc.SubmitChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("EmrClass.FunctionDataCls", "RegisterPatient", ex, false);
                return false;
            }
        }
    }
}
