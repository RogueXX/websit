using Mor.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Mor.Web
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class JsonParamAttribute : FilterAttribute, IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public JsonParamAttribute(string name, Type type)
        {
            this._name = name;
            this._type = type;
        }

        private string _name { get; set; }
        private Type _type { get; set; }


        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // do something...
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var str = filterContext.RequestContext.HttpContext.Request[this._name];
            if (str == null) return;

            var obj = JsonConvert.DeserializeObject(str, this._type);
            filterContext.ActionParameters[this._name] = obj;
        }

    }
}
