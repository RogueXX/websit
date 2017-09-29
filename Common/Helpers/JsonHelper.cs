using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Mor.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class JsonHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            JsonSerializerSettings js = new JsonSerializerSettings();
            js.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            return JsonConvert.SerializeObject(obj, js);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string jsonString)
        { 
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

    }
}
