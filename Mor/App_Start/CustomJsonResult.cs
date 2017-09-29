using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace Mor.Web
{
    public class CustomJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!string.IsNullOrEmpty(ContentType))
                response.ContentType = ContentType;
            else
                response.ContentType = "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                string jsonData = Mor.Common.JsonHelper.SerializeObject(Data);
                response.Write(jsonData);
            }
        }
    }


}