using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mor.Web.Areas.Site.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Site/Index/

        public ActionResult meeting()
        {
            return View();
        }

        public ActionResult TicketInfo()
        {
            return View();
        }

    }
}
