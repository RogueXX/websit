using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mor.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class CookieHelper
    {

        static DESHelper des = new DESHelper();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        public static void Set(string name, object obj)
        {
            if (HttpContext.Current == null) return;

            string value = obj == null ? "" : obj is string ? (string)obj : JsonHelper.SerializeObject(obj);

            HttpCookie cookie = HttpContext.Current.Request.Cookies[name] ?? new HttpCookie(name);
            cookie.Value = des.Encrypt(value); // 加密
            cookie.HttpOnly = false;

            System.Web.HttpContext.Current.Response.SetCookie(cookie);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T Get<T>(string name)
        {
            var value = GetSourceValue(name);
            if (value == null) return default(T);
            value = des.Decrypt(value); // 解密

            if (typeof(T).Equals(typeof(string)))
                return (T)(value as Object);

            return JsonHelper.DeserializeObject<T>(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSourceValue(string name)
        {
            HttpCookie cookie = null;
            if (HttpContext.Current != null)
                cookie = HttpContext.Current.Request.Cookies[name];

            return cookie == null ? null : cookie.Value;
        }


        /// <summary>
        /// 移除单个
        /// </summary>
        public static void Remove(string name)
        {
            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Response.Cookies[name];
                if (cookie == null) return;
                cookie.Expires = DateTime.Now.AddDays(-1);
            }
        }

        /// <summary>
        /// 清除所有站点内的cookie
        /// </summary>
        public static void RemoveAll()
        {
            var req = HttpContext.Current.Request;
            if (req == null || req.Cookies == null) return;
            req.Cookies.AllKeys.ToList().ForEach(key => Remove(key));
        }

    }
}
