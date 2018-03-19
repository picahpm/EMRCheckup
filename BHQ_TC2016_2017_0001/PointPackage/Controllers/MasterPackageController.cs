using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PointPackage.Models;
using System.Data.Linq;
using DBCheckup;

namespace PointPackage.Controllers
{
    public class MasterPackageController : Controller
    {
        [HttpGet]
        public ActionResult Main()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Main(MasterPackageModel model)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    PackageInformationModel pi = new PackageInformationModel();
                    pi.string_search = model.string_search;
                    pi.listPackageInformation = ws.GetAllOrderSet(pi.string_search).AsEnumerable()
                                                  .Where(x => x.Field<string>("ARCOS_Code") != null && x.Field<string>("ARCOS_Code").Trim() != "" &&
                                                              x.Field<string>("ARCOS_Desc") != null && x.Field<string>("ARCOS_Desc").Trim() != "")
                                                  .Select(x => new PackageInformation
                                                  {
                                                      package_code = x.Field<string>("ARCOS_Code"),
                                                      package_name = x.Field<string>("ARCOS_Desc")
                                                  }).Distinct()
                                                  .ToList();
                    if (pi.listPackageInformation.Count == 0)
                    {
                        return View(model);
                    }
                    else
                    {
                        return View("PackageList", pi);
                    }
                }
                //using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                //{
                //    PackageInformationModel pi = new PackageInformationModel();
                //    pi.string_search = model.string_search;
                //    pi.listPackageInformation = cdc.trn_patient_order_sets
                //                                   .Where(x => x.tos_od_set_code.ToUpper().StartsWith(model.string_search.ToUpper()))
                //                                   .Select(x => new PackageInformation
                //                                   {
                //                                       package_code = x.tos_od_set_code,
                //                                       package_name = x.tos_od_set_name
                //                                   }).Distinct()
                //                                   .ToList();
                //    if (pi.listPackageInformation.Count == 0)
                //    {
                //        return View(model);
                //    }
                //    else
                //    {
                //        return View("PackageList", pi);
                //    }
                //}
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult PackageList(PackageInformationModel model)
        {
            return View("Main");
        }

        [HttpPost]
        public ActionResult PackageList(string string_search, string package_code)
        {
            string package_name = "";
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    package_name = cdc.trn_patient_order_sets
                                      .Where(x => x.tos_od_set_code == package_code)
                                      .Select(x => x.tos_od_set_name)
                                      .FirstOrDefault();

                    ViewBag.selectpoint = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "2.0", Value = "2.0" },
                        new SelectListItem { Text = "1.5", Value = "1.5" },
                        new SelectListItem { Text = "1.0", Value = "1.0" },
                        new SelectListItem { Text = "0.5", Value = "0.5" }
                    };
                }
            }
            catch
            {

            }
            return View("AddMasterPackage", new AddMasterPackageModel() { string_search = string_search, package_code = package_code, package_name = package_name, point = 0 });
        }

        [HttpGet]
        public ActionResult AddMasterPackage(AddMasterPackageModel model)
        {
            return View("Main");
        }

        [HttpPost]
        public ActionResult AddMasterPackage(string string_search, string package_code, double point)
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
                        };
                        cdc.mst_order_points.InsertOnSubmit(mst);
                    }
                    mst.mot_set_name = package_name;
                    mst.mot_point = point;
                    mst.mot_update_by = "PointProj";
                    mst.mot_update_date = dateNow;
                    cdc.SubmitChanges();
                    return View("ResultPage", new ResultPageModel() { string_search = string_search, messege = "Add Package " + package_name + " Point : " + point.ToString("0.0") + " to Master successful." });
                }
            }
            catch
            {
                return View("ResultPage", new ResultPageModel() { string_search = string_search, messege = "Error : Please Try Again." });
            }
        }

        [HttpGet]
        public ActionResult ResultPage(ResultPageModel model)
        {
            return View("Main");
        }

        private string getPackageName(string package_code)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    return ws.GetAllOrderSet(package_code).AsEnumerable()
                             .Where(x => x.Field<string>("ARCOS_Code") != null && x.Field<string>("ARCOS_Code").Trim() != "" &&
                                         x.Field<string>("ARCOS_Desc") != null && x.Field<string>("ARCOS_Desc").Trim() != "")
                             .Select(x => x.Field<string>("ARCOS_Desc")).FirstOrDefault();
                    //return cdc.trn_patient_order_sets
                    //          .Where(x => x.tos_od_set_code == package_code)
                    //          .Select(x => x.tos_od_set_name)
                    //          .FirstOrDefault();
                }
            }
            catch
            {
                return "";
            }
        }
    }
}
