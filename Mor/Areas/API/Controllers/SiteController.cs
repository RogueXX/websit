using System;
using Mor.Common;
using Mor.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace okcopy.Areas.API.Controllers
{
    public class SiteController : Controller
    {
        //
        // GET: /API/Site/
         
        [HttpPost]
        public ActionResult Query( )
        {
            using (var db = new SqlHelper())
            {
                var strSql = "select * from events ";

                var list = db.Query(strSql); 

                return Json(list);
            }
        }


    }
}
