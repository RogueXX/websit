using System;
using Mor.Common;
using Mor.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mor.Models.Site;
using System.Text;
using System.Net.Mail;
using Mor.Web.Areas.API.Models;

namespace Mor.Web.Areas.API.Controllers
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
        /// <summary>
        /// 邮箱发送
        /// </summary>
        /// <param name="to">收件人地址</param> 
        /// <param name="body">邮件正文</param> 
        /// <param name="subject">邮件的主题</param> 
        /// <returns></returns>
        [HttpPost]
        public ActionResult SendEmail(string to, string body, string subject)
        {
            //var data=new MailSendr("928710215@qq.com", "634428127@qq.com","测试","门票下单", "634428127@qq.com", "ncabmocrjzxtbbab");
            var data = new MailSendr(to, "634428127@qq.com", body, subject, "634428127@qq.com", "ncabmocrjzxtbbab");
            return Json(new { code = 1, message = "下单成功" });
        }
    }
}
