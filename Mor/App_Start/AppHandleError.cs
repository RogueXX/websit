using Mor.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mor.Web
{


    /// <summary>
    /// 全局异常处理类
    /// </summary>
    public class AppHandleErrorAttribute : HandleErrorAttribute
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {

            var ex = filterContext.Exception.InnerException ?? filterContext.Exception;
            ActionResult result = null;

            if (CommonHelper.IsAjaxRequest())
            {
                // 是Ajax请求出错，返回JSON错误信息
                result = new CustomJsonResult
                {
                    Data = new ResultModel
                    {
                        status = ex is AuthException ? -2 : -1,
                        msg = ex.Message,
                        obj = new
                        {
                            // Message = ex.Message,
                            StackTrace = ex.StackTrace,
                            Type = ex is AuthException ? ((AuthException)ex).Type.ToString() : "Exception"
                        }
                    }
                };
            }
            else
            {
                string redirectUrl = "~/Login?reurl=" + HttpUtility.UrlEncode(
                        filterContext.RequestContext.HttpContext.Request.RawUrl);

                result = new RedirectResult(redirectUrl); 

            }

            // write exception log
            // 记录非用户未登录问题引起的异常信息。   
            if (!(ex is AuthException))
            {
                LogHelper.write(ex);
            }

            // return error message
            filterContext.Result = result;
            filterContext.ExceptionHandled = true;

        }

    }
}