using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;

namespace Mor.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionHelper
    {

        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Remove(string key)
        {
            try
            {   
                if (HttpContext.Current != null) 
                    HttpContext.Current.Session.Remove(key);    //

                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("Key is Null or Empty");
            }

            if (HttpContext.Current != null)
                HttpContext.Current.Session[key] = value;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("Key is Null or Epmty");
            }

            if (HttpContext.Current != null)
            {
                object obj = HttpContext.Current.Session[key];
                if (obj != null) return (T)obj;
            }

            return default(T);
        }

        #region gets
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key) { return Get<string>(key); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string key) { return Get<DateTime>(key); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Int16 GetInt16(string key) { return Get<Int16>(key); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Int32 GetInt32(string key) { return Get<Int32>(key); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Int64 GetInt64(string key) { return Get<Int64>(key); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Boolean GetBoolean(string key) { return Get<Boolean>(key); }

        #endregion
    }
}
