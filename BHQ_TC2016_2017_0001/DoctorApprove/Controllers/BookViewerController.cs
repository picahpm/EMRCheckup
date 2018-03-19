using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using DoctorApprove.Models;
using System.Globalization;
using DBCheckup;

namespace DoctorApprove.Controllers
{
    public class BookViewerController : Controller
    {
        public ActionResult PatientSearch(PatientSearchModels model)
        {
            string username = "";
            var sUser = Session["username"];
            if (sUser == null)
            {
                return RedirectToAction("LogOn", "Account");
            }
            else
            {
                username = sUser.ToString();
                if (string.IsNullOrEmpty(username))
                {
                    return RedirectToAction("LogOn", "Account");
                }
            }

            model.DateSearch = model.DateSearch == null ? DateTime.Now : model.DateSearch;
            string TextSearch = model.TextSearch == null ? "" : model.TextSearch.ToUpper();
            DateTime? dateSearch = model.DateSearch == null ? DateTime.Now : model.DateSearch;

            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    model.patientSearch = cdc.trn_patient_regis
                                             .Where(x => (TextSearch == ""
                                                          ? true
                                                          : (x.trn_patient.tpt_othername.ToUpper().Contains(TextSearch) ||
                                                             x.trn_patient.tpt_hn_no.ToUpper().Contains(TextSearch) ||
                                                             x.trn_patient.tpt_hn_no.ToUpper().Replace("-", "").Contains(TextSearch) ||
                                                             x.tpr_en_no.ToUpper().Contains(TextSearch) ||
                                                             x.tpr_en_no.ToUpper().Replace("-", "").Contains(TextSearch))) &&
                                                         (!model.isCheckDate ? true :
                                                          (x.trn_patient_regis_detail.tpr_real_arrived_date == null ? false
                                                             : x.trn_patient_regis_detail.tpr_real_arrived_date.Value.Date == dateSearch.Value.Date)
                                                         ) &&
                                                         x.trn_patient_doctor.tpd_doctor_code == username &&
                                                         new List<string> { "WFA", "API" }.Contains(x.trn_patient_doctor_approve.tpda_status)
                                                   ).Select(x => new PatientSearchModels.PatientSearch
                                                   {
                                                       hn = x.trn_patient.tpt_hn_no,
                                                       name = x.trn_patient.tpt_othername,
                                                       tpr_id = x.tpr_id,
                                                       status = x.trn_patient_doctor_approve.tpda_status == "WFA" ? "Waiting." : "Approving.",
                                                       express = x.tpr_send_book == '1' ? 1 : 0,
                                                       rpt_code = !string.IsNullOrEmpty(x.trn_patient_doctor_approve.tpda_rpt_code) ? x.trn_patient_doctor_approve.tpda_rpt_code : "BK306"
                                                   }).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult PatientSearch()
        {
            try
            {
                var sUser = Session["username"];
                if (sUser == null)
                {
                    return RedirectToAction("LogOn", "Account");
                }
                else
                {
                    string username = Session["username"].ToString();
                    if (string.IsNullOrEmpty(username))
                    {
                        return RedirectToAction("LogOn", "Account");
                    }
                }

                var AcType = Request.Params["AcType"];
                if (AcType != null)
                {
                    var param_tpr_id = Request.Params["tpr_id"];
                    if (param_tpr_id != null)
                    {
                        int tpr_id = Convert.ToInt32(param_tpr_id);
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            trn_patient_doctor_approve patientDoctorApprove = cdc.trn_patient_doctor_approves
                                                                                 .Where(x => x.tpr_id == tpr_id)
                                                                                 .FirstOrDefault();
                            if (patientDoctorApprove != null)
                            {
                                if (AcType == "Select")
                                {
                                    //var files = Directory.GetFiles(Request.MapPath(@"..\BookImages\" + patientDoctorApprove.tpda_path_image));

                                    //var result = files.Select(x => new
                                    //{
                                    //    name = Path.GetFileNameWithoutExtension(x),
                                    //    path = @"../BookImages/" + patientDoctorApprove.tpda_path_image + @"/" + Path.GetFileName(x)
                                    //}).ToArray();

                                    if (patientDoctorApprove.tpda_status == "WFA")
                                    {
                                        patientDoctorApprove.tpda_status = "API";
                                        cdc.SubmitChanges();
                                        return Json(new { success = true, msgjson = "", status = "Approving." });
                                    }
                                    else if (patientDoctorApprove.tpda_status == "CBB")
                                    {
                                        return Json(new { success = true, msgjson = "", status = "Recall." });
                                    }
                                    else if (patientDoctorApprove.tpda_status == "API")
                                    {
                                        return Json(new { success = true, msgjson = "", status = "Approving." });
                                    }
                                    else if (patientDoctorApprove.tpda_status == "APD")
                                    {
                                        return Json(new { success = true, msgjson = "", status = "Approving." });
                                    }
                                    else
                                    {
                                        return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                                    }
                                }
                                else if (AcType == "Approve")
                                {
                                    if (patientDoctorApprove.tpda_status == "API")
                                    {
                                        patientDoctorApprove.tpda_status = "APD";
                                        cdc.SubmitChanges();
                                        return Json(new { success = true, msgjson = "Success.", status = "Approved." });
                                    }
                                    else
                                    {
                                        return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                                    }
                                }
                                else if (AcType == "NonApprove")
                                {
                                    if (patientDoctorApprove.tpda_status == "API")
                                    {
                                        patientDoctorApprove.tpda_status = "WFA";
                                        cdc.SubmitChanges();
                                        return Json(new { success = true, msgjson = "Success.", status = "Waiting." });
                                    }
                                    else
                                    {
                                        return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                                    }
                                }
                                else if (AcType == "Reject")
                                {
                                    var Reason = Request.Params["Reason"];
                                    if (patientDoctorApprove.tpda_status == "API")
                                    {
                                        patientDoctorApprove.tpda_status = "NAP";
                                        patientDoctorApprove.tpda_reject_reason = Reason;
                                        cdc.SubmitChanges();
                                        return Json(new { success = true, msgjson = "Success.", status = "Reject." });
                                    }
                                    else
                                    {
                                        return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                            }
                        }
                    }
                    else
                    {
                        return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                    }
                }
                else
                {
                    return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
                }
            }
            catch
            {
                return Json(new { success = false, msgjson = "The Transaction is fail. Please Try Again.", status = "" });
            }
        }
    }
}
