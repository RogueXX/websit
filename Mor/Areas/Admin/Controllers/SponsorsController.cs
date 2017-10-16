using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mor.Web.Areas.Admin.Controllers
{
    public class SponsorsController : Controller
    {
        //
        // GET: /Admin/Sponsors/

       [Auth]
        public ActionResult Index()
        {
            return View();
        }

    }
}
