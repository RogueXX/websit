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


        [HttpPost]
        public ActionResult SendEmail()
        {
            SendToEmail();
            return Json(new { code = 1, message = "下单成功" });
        }

        public class Email
        {
            /// <summary>
            /// 发送者
            /// </summary>
            public string mailFrom { get; set; }

            /// <summary>
            /// 收件人
            /// </summary>
            public string[] mailToArray { get; set; }

            /// <summary>
            /// 抄送
            /// </summary>
            public string[] mailCcArray { get; set; }

            /// <summary>
            /// 标题
            /// </summary>
            public string mailSubject { get; set; }

            /// <summary>
            /// 正文
            /// </summary>
            public string mailBody { get; set; }

            /// <summary>
            /// 发件人密码
            /// </summary>
            public string mailPwd { get; set; }

            /// <summary>
            /// SMTP邮件服务器
            /// </summary>
            public string host { get; set; }

            /// <summary>
            /// 正文是否是html格式
            /// </summary>
            public bool isbodyHtml { get; set; }

            /// <summary>
            /// 附件
            /// </summary>
            public string[] attachmentsPath { get; set; }

            public bool Send()
            {
                //使用指定的邮件地址初始化MailAddress实例
                MailAddress maddr = new MailAddress(mailFrom);
                //初始化MailMessage实例
                MailMessage myMail = new MailMessage();


                //向收件人地址集合添加邮件地址
                if (mailToArray != null)
                {
                    for (int i = 0; i < mailToArray.Length; i++)
                    {
                        myMail.To.Add(mailToArray[i].ToString());
                    }
                }

                //向抄送收件人地址集合添加邮件地址
                if (mailCcArray != null)
                {
                    for (int i = 0; i < mailCcArray.Length; i++)
                    {
                        myMail.CC.Add(mailCcArray[i].ToString());
                    }
                }
                //发件人地址
                myMail.From = maddr;

                //电子邮件的标题
                myMail.Subject = mailSubject;

                //电子邮件的主题内容使用的编码
                myMail.SubjectEncoding = Encoding.UTF8;

                //电子邮件正文
                myMail.Body = mailBody;

                //电子邮件正文的编码
                myMail.BodyEncoding = Encoding.Default;

                myMail.Priority = MailPriority.High;

                myMail.IsBodyHtml = isbodyHtml;

                //在有附件的情况下添加附件
                try
                {
                    if (attachmentsPath != null && attachmentsPath.Length > 0)
                    {
                        Attachment attachFile = null;
                        foreach (string path in attachmentsPath)
                        {
                            attachFile = new Attachment(path);
                            myMail.Attachments.Add(attachFile);
                        }
                    }
                }
                catch (Exception err)
                {
                    throw new Exception("在添加附件时有错误:" + err);
                }

                SmtpClient smtp = new SmtpClient();
                //指定发件人的邮件地址和密码以验证发件人身份
                smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);


                //设置SMTP邮件服务器
                smtp.Host = host;

                try
                {
                    //将邮件发送到SMTP邮件服务器
                    smtp.Send(myMail);
                    return true;

                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    return false;
                }

            }
        }

        protected object  SendToEmail()
        {
            Email email = new Email();
            email.mailFrom = "634428127@qq.com";
            email.mailPwd = "13570222904/..,,";
            email.mailSubject = "购票";
            email.mailBody = "购票测试";
            email.isbodyHtml = true;    //是否是HTML
            email.host = "smtp.126.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
            email.mailToArray = new string[] { "492026207@qq.com", "928710215@qq.com" };//接收者邮件集合
            email.mailCcArray = new string[] { "928710215@qq.com" };//抄送者邮件集合
            if (email.Send())
            {
                return new { code = 1, message = "发送成功" };
            }
            else
            {
                return new { code = 0, message = "发送失败" };
            }
        }

    }
}
