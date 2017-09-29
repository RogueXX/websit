using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using Mor.Common;
using Mor.DataAccess;
using Mor.Web;

namespace Mor.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        //
        // GET: /Admin/Main/

        [Auth]
        public ActionResult Index()
        {
            return View();
        }

    }
}
