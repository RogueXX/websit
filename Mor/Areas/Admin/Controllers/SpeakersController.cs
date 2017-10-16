using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mor.Areas.Admin.Controllers
{
    public class SpeakersController : Controller
    {
        //
        // GET: /Admin/Speakers/

        public ActionResult Index()
        {
            return View();
        }

    }
}
