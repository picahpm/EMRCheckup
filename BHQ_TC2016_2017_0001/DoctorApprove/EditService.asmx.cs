using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DBCheckup;
using System.Web.Script.Services;

namespace DoctorApprove
{
    /// <summary>
    /// Summary description for EditService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    [Serializable]
    public class EditService : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string SaveDraft(int tpr_id, string reason)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_doctor_approve patientDoctorApprove = cdc.trn_patient_doctor_approves
                                                                         .Where(x => x.tpr_id == tpr_id)
                                                                         .FirstOrDefault();
                    if (patientDoctorApprove != null)
                    {
                        patientDoctorApprove.tpda_reject_reason = reason;
                        cdc.SubmitChanges();
                        return "Save Draft Completed.";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetReason(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_doctor_approve patientDoctorApprove = cdc.trn_patient_doctor_approves
                                                                         .Where(x => x.tpr_id == tpr_id)
                                                                         .FirstOrDefault();
                    if (patientDoctorApprove != null)
                    {
                        return patientDoctorApprove.tpda_reject_reason;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
