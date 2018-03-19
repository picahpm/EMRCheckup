using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Net;
using DoctorApprove.Models;
using DBCheckup;

namespace DoctorApprove.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            string username = Request.QueryString["username"];
            string otp = Request.QueryString["password"];
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(otp))
            {
                return View();
            }
            else
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    DateTime dateNow = Utility.GetServerDateTime();
                    mst_user_otp mdo = cdc.mst_user_otps.Where(x => x.mst_user_type.mut_username == username).FirstOrDefault();
                    if (mdo != null && mdo.mut_otp == otp && mdo.mut_expire >= dateNow)
                    {
                        mdo.mut_expire = dateNow;
                        cdc.SubmitChanges();
                        Session["username"] = username;
                        return RedirectToAction("PatientSearch", "BookViewer");
                    }
                    else
                    {
                        return View(new LogOnModel() { strMsg = "Invalid Log in. Please try again." });
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool chk = getUser(model);
                if (chk)
                {
                    return RedirectToAction("PatientSearch", "BookViewer");
                }
                else
                {
                    model.strMsg = "The username or password provided is incorrect.";
                }
            }

            return View(model);
        }

        private bool getUser(LogOnModel model)
        {
            using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
            {
                DataTable dt = ws.LogonTrakcare(model.UserName, model.Password);
                if (dt.Rows.Count >= 1)
                {
                    Session["username"] = dt.Rows[0].Field<string>("USERID");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
