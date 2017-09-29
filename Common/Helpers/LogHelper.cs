using Mor.DataAccess;
using System;
using System.IO;
using System.Web;

namespace Mor.Common
{

    /// <summary>
    /// 
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="db"></param>
        /// <param name="privilege"></param>
        /// <param name="token"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static int InsertLog(SqlHelper db, int Privilege, string Token, string Description)
        {
            return db.Insert("Operation_Logs", new
            {
                Token,
                Privilege,
                Description,
                PageUrl = HttpContext.Current != null ? HttpContext.Current.Request.RawUrl : "localhost",
                AddedTime = DateTime.Now,
                UserName = UserEnvironment.UserInfo != null ? UserEnvironment.UserInfo.UserName : "API",
                IPAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress.ToString() : "127.0.0.1"
            });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        public static void login(string CreateName, params object[] lines)
        {
            write("login", CreateName, lines);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        public static void info(params object[] lines)
        {
            write("info", "SYSTEM", lines);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        public static void warning(params object[] lines)
        {
            write("warning", "SYSTEM", lines);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        public static void error(params object[] lines)
        {
            write("error", "SYSTEM", lines);
        }
        public static void wdgj_error(params object[] lines)
        {
            write("error", "wdgj_api", lines);
        }

        public static void wdgj_info(params object[] lines)
        {
            write("info", "wdgj_api", lines);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        public static void debug(params object[] lines)
        {
            write("debug", "SYSTEM", lines);
        }

        /// <summary>
        /// 记录异常信息
        /// </summary>
        /// <param name="ex"></param>
        public static void write(Exception ex)
        {
            ex = ex.InnerException ?? ex;
            write("error", "SYSTEM", ex.Message, ex.StackTrace);
        }

        private static HttpRequest getHttpRequest()
        {
            try { return HttpContext.Current.Request; }
            catch { return null; }
        }


        private static void write(string type, string CreateName, params object[] lines)
        {

            using (var db = new SqlHelper())
            {
                var req = getHttpRequest();

                db.Insert("vshop_logs", new
                {
                    type = type,
                    Message = lines[0],
                    Description = lines.Length > 1 ? lines[1] : null,
                    Url = req == null ? "#" : req.RawUrl,
                    ClientIP = req == null ? "127.0.0.1" : req.UserHostAddress,
                    CreateDate = DateTime.Now,
                    CreateName = CreateName
                });
            }


        }

        public static void InsertLogToFile(string type, params object[] lines)
        {
            // create log directory
            var path = UserEnvironment.CurrentDirectory.TrimEnd('\\') + "\\logs";
            Directory.CreateDirectory(path);

            string filename = string.Format("{2}\\{0:yyyyMMdd}.{1}.txt", DateTime.Now, type, path);
            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.WriteLine("---------------{0:yyyy-MM-dd HH:mm:ss}---------------", DateTime.Now);
                foreach (object msg in lines)
                    sw.WriteLine(msg);

                sw.WriteLine("");
                sw.Close();
            }
        }

    }
}
