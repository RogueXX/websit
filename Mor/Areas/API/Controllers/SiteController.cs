using System;
using Mor.Common;
using Mor.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mor.Models.Site;

namespace Mor.Areas.API.Controllers
{
    public class SiteController : Controller
    {
        //
        // GET: /API/Site/

        [HttpPost]
        public ActionResult Query()
        {
            using (var db = new SqlHelper())
            {
                var strSql = "select * from events ";

                var list = db.Query(strSql);

                return Json(list);
            }
        }

        [HttpPost]
        public ActionResult CheckUserLogin(string username, string userpwd)
        {
            using (var db = new SqlHelper())
            {
                var strSql = string.Format("select * from a-user form username={0} and userpwd={1}", username, userpwd);

                var list = db.Single(strSql);

                return Json(list);
            }
        }

        /// <summary>
        /// 新增门票购买
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsertTicketInfo(AddTicketInfoModel model)
        {
            using (var db = new SqlHelper())
            {
                foreach (var i in model.ticketlist)
                {
                    TicketInfoModel ticket = new TicketInfoModel
                    {
                        oId=i.oId,
                        Name=i.Name ,
                        IdCard=i.IdCard ,
                        Phone=i.Phone,
                        Email =i.Email ,
                        Job =i.Job,
                        MeetingDateStr =i.MeetingDateStr ,
                        CorporateName=model.CorporateName ,
                        Industry=model.Industry,
                        CompanySize=model.CompanySize,
                        PayType=0,
                        IsPay = 0,
                        ThirdOrderNumber = string.Empty,
                        CreateDate = DateTime.Now,
                        Delete = 0,
                    };
                    var list = db.Insert("ticketinfo", ticket);
                }

                return Json(new { code = 1, message = "下单成功" });
            }
        }


    }
}
