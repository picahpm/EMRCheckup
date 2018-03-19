using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Linq;
using PointPackage.Models;
using DBCheckup;

namespace PointPackage.Controllers
{
    public class PatientSearchController : Controller
    {
        [HttpGet]
        public ActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Main(PatientSearchModel model)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    PatientInformationModel pi = new PatientInformationModel();
                    pi.queue_no = model.queue_no;
                    pi.listPatientInformation = cdc.trn_patient_regis
                                                   .Where(x => x.tpr_queue_no == model.queue_no)
                                                   .Select(x => new PatientInformation
                                                   {
                                                       tpr_id = x.tpr_id,
                                                       name = x.trn_patient.tpt_first_name,
                                                       lastname = x.trn_patient.tpt_last_name,
                                                       hn = x.trn_patient.tpt_hn_no,
                                                       en = x.tpr_en_no,
                                                       arrived_date = x.trn_patient_regis_detail == null ? x.tpr_arrive_date.Value : x.trn_patient_regis_detail.tpr_real_arrived_date.Value
                                                   }).OrderByDescending(x => x.arrived_date)
                                                   .ToList();
                    if (pi.listPatientInformation.Count == 0)
                    {
                        ViewBag.QueueNo = model.queue_no + " is Not Found.";
                        ModelState.Clear();
                        return View("Main", new PatientSearchModel());
                    }
                    else
                    {
                        ViewBag.QueueNo = model.queue_no;
                        return View("PatientList", pi);
                    }
                }
            }
            catch
            {
                ViewBag.PageTitle = "Error (Please Try Again)";
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult PatientList(PatientInformationModel model)
        {
            return View("Main");
        }

        [HttpPost]
        public ActionResult PatientList(int tpr_id, string queue_no)
        {
            ViewBag.QueueNo = queue_no;
            return View("SelectAction", new SelectActionModel() { queue_no = queue_no, tpr_id = tpr_id });
        }

        [HttpGet]
        public ActionResult SelectAction(SelectActionModel model)
        {
            return View("Main");
        }

        [HttpPost]
        public ActionResult SelectAction(int tpr_id, string queue_no, int action)
        {
            switch (action)
            {
                case 1:
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        ViewPackageModel vp = new ViewPackageModel() { queue_no = queue_no, tpr_id = tpr_id };
                        vp.listPackage = cdc.trn_patient_order_sets
                                            .Where(x => x.tpr_id == tpr_id)
                                            .Select(x => new Package
                                            {
                                                package_code = x.tos_od_set_code,
                                                package_name = x.tos_od_set_name
                                            }).ToList();
                        ViewBag.QueueNo = queue_no;
                        return View("ViewPackage", vp);
                    }
                case 2:
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        ViewBag.QueueNo = queue_no;
                        var dummyModel = new AddDummyModel() { queue_no = queue_no, tpr_id = tpr_id };
                        ViewBag.selectpackage = cdc.mst_order_points
                                                   .Where(x => x.mot_set_code.StartsWith("GHOSTPACK"))
                                                   .Select(x => new SelectListItem
                                                   {
                                                       Text = x.mot_set_name,
                                                       Value = x.mot_set_code
                                                   }).ToList();
                        return View("AddDummyPackage", new AddDummyModel() { queue_no = queue_no, tpr_id = tpr_id });
                    }
                case 3:
                    try
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            List<trn_patient_order_set> listSet = cdc.trn_patient_order_sets
                                                                     .Where(x => x.tpr_id == tpr_id)
                                                                     .ToList();
                            listSet.ForEach(x => x.tos_status = null);
                            cdc.SubmitChanges();
                        }
                        return View("ResultPage", new ResultPageModel() { queue_no = queue_no, messege = "Cancel Package Queue No." + queue_no + " successful." });
                    }
                    catch
                    {
                        return View("ResultPage", new ResultPageModel() { queue_no = queue_no, messege = "Cancel Package Queue No." + queue_no + " not successful." });
                    }
                default:
                    return View("SelectAction", tpr_id);
            }
        }

        

        [HttpGet]
        public ActionResult ViewPackage(ViewPackageModel model)
        {
            return View("Main");
        }

        [HttpPost]
        public ActionResult ViewPackage(string queue_no, int tpr_id, string package_code)
        {
            string package_name = getPackageName(package_code);
            ViewBag.QueueNo = queue_no;
            ViewBag.selectpoint = new List<SelectListItem>
                                  {
                                      new SelectListItem { Text = "2.0", Value = "2.0" },
                                      new SelectListItem { Text = "1.5", Value = "1.5" },
                                      new SelectListItem { Text = "1.0", Value = "1.0" },
                                      new SelectListItem { Text = "0.5", Value = "0.5" }
                                  };
            return View("AddMasterPackage", new AddMasterPackageModel() { queue_no = queue_no, tpr_id = tpr_id, package_code = package_code, package_name = package_name , point = 0 });
        }

        [HttpGet]
        public ActionResult AddDummyPackage(AddDummyModel model)
        {
            return View("Main");
        }

        [HttpPost]
        public ActionResult AddDummyPackage(string queue_no, int tpr_id, string packagecode)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = DateTime.Now;
                    var package = cdc.mst_order_points
                                     .Where(x => x.mot_set_code == packagecode &&
                                                 x.mot_status == 'A')
                                     .Select(x => new
                                     {
                                         x.mot_set_code,
                                         x.mot_set_name
                                     }).FirstOrDefault();
                    if (package != null)
                    {
                        cdc.trn_patient_order_sets.InsertOnSubmit(new trn_patient_order_set
                        {
                            tpr_id = tpr_id,
                            tos_create_by = "PointProj",
                            tos_create_date = dateNow,
                            tos_od_set_code = package.mot_set_code,
                            tos_od_set_name = package.mot_set_name,
                            tos_status = true,
                            tos_update_by = "PointProj",
                            tos_update_date = dateNow
                        });
                        ViewBag.QueueNo = queue_no;
                        cdc.SubmitChanges();
                        return View("ResultPage", new ResultPageModel() { queue_no = queue_no, messege = "Add Package " + package.mot_set_name + " successful." });
                    }
                    else
                    {
                        ViewBag.QueueNo = queue_no;
                        return View("ResultPage", new ResultPageModel() { queue_no = queue_no, messege = "Error : Package Dummy is not found." });
                    }
                }
            }
            catch
            {
                ViewBag.QueueNo = queue_no;
                return View("ResultPage", new ResultPageModel() { messege = "Error : Please Try Again." });
            }
        }

        [HttpGet]
        public ActionResult AddMasterPackage(AddMasterPackageModel model)
        {
            return View("Main");
        }

        [HttpPost]
        public ActionResult AddMasterPackage(string queue_no, int tpr_id, string package_code, double point)
        {
            try
            {
                string package_name = getPackageName(package_code);
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = DateTime.Now;
                    mst_order_point mst = cdc.mst_order_points
                                             .Where(x => x.mot_set_code == package_code &&
                                                         x.mot_status == 'A')
                                             .FirstOrDefault();
                    if (mst == null)
                    {
                        mst = new mst_order_point()
                        {
                            mot_create_by = "PointProj",
                            mot_create_date = dateNow,
                            mot_status = 'A',
                            mot_set_code = package_code,
                            mhs_id = 1,
                            mot_set_name = package_name,
                        };
                        cdc.mst_order_points.InsertOnSubmit(mst);
                    }
                    mst.mot_point = point;
                    mst.mot_update_by = "PointProj";
                    mst.mot_update_date = dateNow;
                    ViewBag.QueueNo = queue_no;
                    cdc.SubmitChanges();
                    return View("ResultPage", new ResultPageModel() { queue_no = queue_no, messege = "Add Package " + package_name + " Point : " + point.ToString("0.0") + " to Master successful." });
                }
            }
            catch
            {
                ViewBag.QueueNo = queue_no;
                return View("ResultPage", new ResultPageModel() { queue_no = queue_no, messege = "Error : Please Try Again." });
            }
        }

        [HttpGet]
        public ActionResult ResultPage(ResultPageModel model)
        {
            if (string.IsNullOrEmpty(model.queue_no))
            {
                return View("Main");
            }
            else
            {
                ViewBag.QueueNo = model.queue_no;
                return View(model);
            }
        }

        private string getPackageName(string package_code)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    return cdc.trn_patient_order_sets
                              .Where(x => x.tos_od_set_code == package_code)
                              .Select(x => x.tos_od_set_name)
                              .FirstOrDefault();
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
