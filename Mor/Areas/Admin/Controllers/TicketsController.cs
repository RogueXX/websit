using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mor.Web.Areas.Admin.Controllers
{
    public class TicketsController : Controller
    {
        //
        // GET: /Admin/Tickets/

       [Auth]
        public ActionResult Index()
        {
            return View();
        }

    }
}
