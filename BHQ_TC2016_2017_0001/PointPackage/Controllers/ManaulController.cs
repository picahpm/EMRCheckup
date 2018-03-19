using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace PointPackage.Controllers
{
    public class ManaulController : Controller
    {
        //
        // GET: /Manaul/

        public ActionResult UserManaul()
        {
            string manaulPath = Request.MapPath(@"..\Manaul\User Manual - EMR Checkup (Add Point Manual).pdf");
            return File(manaulPath, "application/pdf");
        }

    }
}
