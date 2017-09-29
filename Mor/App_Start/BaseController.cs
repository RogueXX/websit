using Mor.Common;
using Mor.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc; 

namespace Mor.Web
{

    public class __BaseController : Controller
    {


        /// <summary>
        /// 返回成功JSON结果
        /// </summary>
        /// <returns></returns>
        protected JsonResult JsonSucceed()
        {
            return JsonSucceed(null);
        }

        /// <summary>
        /// 返回成功JSON结果
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected JsonResult JsonSucceed(object obj)
        {

            return Json(new ResultModel(obj));
        }

        /// <summary>
        /// 返回失败JSON结果
        /// </summary>
        /// <param name="errMessage"></param>
        /// <returns></returns>
        protected JsonResult JsonFailed(string errMessage)
        {

            return Json(new ResultModel(errMessage));
        }

        /// <summary>
        /// 重写JSON转
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType
            };
        }


        /// <summary>
        /// 重写JSON转
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <returns></returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentEncoding = contentEncoding,
                ContentType = contentType,
                JsonRequestBehavior = behavior
            };

        }

    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseAPIController : __BaseController
    {

        protected override void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
        {
            // base.OnAuthentication(filterContext);

            if (UserEnvironment.UserInfo == null || UserEnvironment.UserInfo.UserId <= 0)
            {
                throw new Exception("用户未登录！");
            }

        }

    }


    /// <summary>
    /// 通用的保存和删除方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseAPIController<T> : BaseAPIController
        where T : Mor.DataAccess.IDbTableModel
    {

        [HttpPost]
        public virtual ActionResult Get(T obj)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM {0} WHERE 1=1 ", obj.GetTableName());

            var pp = obj.GetType().GetProperty("token", System.Reflection.BindingFlags.IgnoreCase);
            if (pp != null) sb.Append("AND token=@Token ");

            foreach (var key in obj.GetPrimaryKeys())
                sb.AppendFormat(" AND [{0}]=@{0}", key);

            using (var db = new SqlHelper())
            {
                var res = db.Single(sb.ToString(), obj);
                if (res == null) return JsonFailed("找不到相关记录或已被删除！");

                return Json(res);
            }
        }


        [HttpPost]
        public virtual ActionResult Save(T obj)
        {
            using (var db = new SqlHelper())
            {
                var n = db.InsertOrUpdate(obj);
                return JsonSucceed();
            }
        }

        [HttpPost]
        public virtual ActionResult Delete(T obj)
        {
            using (var db = new SqlHelper())
            {
                db.BeginTransaction();

                var primaryKey = obj.GetPrimaryKeys()[0];
                var sql = string.Format("DELETE FROM {0} WHERE {1}=@p0", obj.GetTableName(), primaryKey);
                var value = obj.GetType().GetProperty(primaryKey).GetValue(obj, null);

                foreach (var id in value.ToString().Split(','))
                {
                    db.Execute(sql, id);
                }

                db.Commit();

                return Json(new { error = false });
            }

        }


    }
}