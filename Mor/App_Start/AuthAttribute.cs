using Mor.Common;
using Mor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mor.Web
{

    /// <summary>
    /// 控制器授权验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class AuthAttribute : FilterAttribute, IAuthorizationFilter
    {

        /// <summary>
        /// 非菜单，仅验证是否登录
        /// </summary>
        public AuthAttribute()
        {

        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {

            var token = filterContext.RequestContext.HttpContext.Request["token"];

            if (UserEnvironment.UserInfo == null)
            {
                // 判断是否登录
                var reurl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                reurl = filterContext.RequestContext.HttpContext.Server.UrlEncode(reurl);
                filterContext.Result = new RedirectResult("/login?reurl=" + reurl);
            }
        }


    }
}
