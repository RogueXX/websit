using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mor.Web.Areas.API.Controllers
{
    public class BannerController : Controller
    {
        //
        // GET: /API/Banner/

        public ActionResult Index()
        {
            return View();
        }

    }
}
