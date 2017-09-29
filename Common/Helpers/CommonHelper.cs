using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using Mor.DataAccess;
using System.Net; 

namespace Mor.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonHelper
    {

        private static string _sessionId = null;
         
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetSessionId()
        {
            if (!IsWebProject())
            {
                // for c/s project;
                if (_sessionId == null)
                    _sessionId = Guid.NewGuid().ToString();
                return _sessionId;
            }
            else
            {
                if (HttpContext.Current.Request.Cookies["REDIS_SessionId"] != null)
                {
                    return HttpContext.Current.Request.Cookies["REDIS_SessionId"].Value.ToString();
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    HttpCookie cokie = new HttpCookie("REDIS_SessionId");
                    cokie.Value = guid.ToString();
                    cokie.HttpOnly = true;
                    HttpContext.Current.Response.Cookies.Add(cokie);
                    return guid.ToString();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsWebProject()
        {
            return System.Web.HttpContext.Current != null;
        }

        /// <summary>
        /// 是否为Ajax请求
        /// </summary>
        /// <returns></returns>
        public static bool IsAjaxRequest()
        {
            return IsWebProject() && HttpContext.Current.Request.HttpMethod != "GET";
            //HttpContext.Current.Request.Headers.Get("X-Requested-With") == "XMLHttpRequest";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetFullPath(string url, params object[] args)
        {

            if (args != null && args.Length > 0)
                url = string.Format(url, args);

            if (url.IndexOf(":\\") > -1)
            {
                return url;
            }
            else if (IsWebProject())
            {
                return HttpContext.Current.Server.MapPath(url);
            }
            else
            {
                var path = UserEnvironment.CurrentDirectory.TrimEnd('\\');
                return path + '\\' + url.Replace('/', '\\').TrimStart('\\');
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MaskText(string str)
        {
            if (str == null) return str;
            return str.Replace("'", "").Replace("\n", "").Replace("\r", "").Replace("\t", "").Replace("\t", "");
        }

        /// <summary>
        /// 日期处理
        /// </summary>
        /// <param name="val"></param>
        /// <param name="dt">为空时以该日期为准</param>
        /// <returns></returns>
        public static DateTime ParseDateTime(object val, DateTime dt)
        {
            if (val != null) DateTime.TryParseExact(Convert.ToString(val),
                    "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture, DateTimeStyles.None, out dt);
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DisplayDateTime(DateTime dt)
        {
            TimeSpan ts = DateTime.Now - dt;

            if (ts.TotalSeconds < 60)
                return "刚刚";
            if (ts.TotalMinutes < 60)
                return Math.Floor(ts.TotalMinutes) + "分钟前";
            else if (ts.TotalHours < 24)
                return Math.Floor(ts.TotalHours) + "小时前";
            else if (ts.TotalDays < 24)
                return Math.Floor(ts.TotalDays) + "小时前";
            else if (ts.TotalDays < 90)
                return Math.Floor(ts.TotalDays / 30) + "个月前";
            else
                return dt.ToString("yyyy-MM-dd HH:mm");
        }


        /// <summary>
        /// MD5加密 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string source)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(source);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');
            }
            return str.ToLower();
        }

        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeToTime(string timeStamp)
        {

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            if (string.IsNullOrEmpty(timeStamp)) return dtStart;

            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// 校验是否为数字
        /// </summary>
        /// <param name="str">要校验的字符串</param>
        /// <returns>是否为数字</returns>
        public static bool IsNum(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"(\d)$");
            //^[-]?\d+[.]?\d*$
            return reg1.IsMatch(str);
        }
        /// <summary>
        /// 校验是否为数字[支持小数]
        /// </summary>
        /// <param name="str">要校验的字符串</param>
        /// <returns>是否为数字</returns>
        public static bool IsNumeric(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[-]?(\d+\.?\d*|\.\d+)$");
            //^[-]?\d+[.]?\d*$
            return reg1.IsMatch(str);
        }

        /// <summary>
        /// 字符窜
        /// </summary>
        /// <returns></returns>
        public static string GenerateStringID()
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 取随机字符窜
        /// </summary>
        /// <param name="length"></param>
        /// <param name="number"></param>
        /// <param name="upperWord"></param>
        /// <param name="lowerWord"></param>
        /// <param name="specialCharacters"></param>
        /// <returns></returns>
        public static string GetRandomString(int length, bool number, bool upperWord, bool lowerWord, bool specialCharacters)
        {

            string str = "";

            if (number) str += "0123456789";
            if (upperWord) str += "ACBDEFGHIJKLMNOPQRSTUVWXYZ";
            if (lowerWord) str += "acbdefghijklmnopqrstuvwxyz";
            if (specialCharacters) str += "!#$%&'()*+,-./:;<=>@[\\]^_`{|}~";

            if (string.IsNullOrEmpty(str))
                throw new Exception("请至少使用一种加密字符!");


            char[] ch = new char[length];
            Random r = new Random(System.Guid.NewGuid().GetHashCode());
            for (int i = 0; i < ch.Length; i++)
            {
                ch[i] = str[r.Next(0, str.Length)];
            }
            return new string(ch);

        }
        /// <summary>
        /// 生成流水号（14位）
        /// </summary>
        /// <returns></returns>
        public static string GenerateSwiftNumber()
        {
            return string.Format("{0:yyMMdd}{1}", DateTime.Now, DateTime.Now.Ticks.ToString().Substring(6, 8));
            // return CommonHelper.ConvertDateTimeInt(DateTime.Now) + "" + new Random().Next(1000, 9999);
        }

        #region 取汉字拼音


        /// <summary>
        /// 取汉字拼音
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPYString(string str)
        {
            string tempStr = "";
            foreach (char c in str)//当前在text里的中文
            {
                if ((int)c >= 33 && (int)c <= 126)
                {
                    tempStr += c.ToString();
                }
                else
                {
                    tempStr += GetPYChar(c.ToString());
                }
            }
            return tempStr;
        }
        private static string GetPYChar(string c)
        {
            byte[] array = new byte[2];
            array = System.Text.Encoding.Default.GetBytes(c);
            int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));

            if (i < 0xB0A1) return "*";
            if (i < 0xB0C5) return "a";
            if (i < 0xB2C1) return "b";
            if (i < 0xB4EE) return "c";
            if (i < 0xB6EA) return "d";
            if (i < 0xB7A2) return "e";
            if (i < 0xB8C1) return "f";
            if (i < 0xB9FE) return "g";
            if (i < 0xBBF7) return "h";
            if (i < 0xBFA6) return "g";
            if (i < 0xC0AC) return "k";
            if (i < 0xC2E8) return "l";
            if (i < 0xC4C3) return "m";
            if (i < 0xC5B6) return "n";
            if (i < 0xC5BE) return "o";
            if (i < 0xC6DA) return "p";
            if (i < 0xC8BB) return "q";
            if (i < 0xC8F6) return "r";
            if (i < 0xCBFA) return "s";
            if (i < 0xCDDA) return "t";
            if (i < 0xCEF4) return "w";
            if (i < 0xD1B9) return "x";
            if (i < 0xD4D1) return "y";
            if (i < 0xD7FA) return "z";
            return "*";
        }
        #endregion

        #region Http工具

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHttpFullPath(string url, string token = "")
        {
            if (string.IsNullOrEmpty(url)) return null;

            if (string.IsNullOrEmpty(token))
            {
                return string.Format("{0}/{1}", UserEnvironment.CurrentServerBaseUrl.TrimEnd('/'), url.Trim().TrimStart('/'));
            }

            var sr = new System.Configuration.AppSettingsReader();
            try
            {
                var baseUrl = sr.GetValue("BaseUrl_" + token, typeof(string)) as String;
                if (baseUrl == null)
                {
                    return string.Format("{0}/{1}", UserEnvironment.CurrentServerBaseUrl.TrimEnd('/'), url.Trim().TrimStart('/'));
                }

                return string.Format("{0}/{1}", baseUrl.TrimEnd('/'), url.Trim().TrimStart('/'));
            }
            catch (Exception ex)
            {
                return string.Format("{0}/{1}", UserEnvironment.CurrentServerBaseUrl.TrimEnd('/'), url.Trim().TrimStart('/'));
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ParseQueryString(object obj)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var prop in obj.GetType().GetProperties())
            {
                var val = Convert.ToString(prop.GetValue(obj));
                sb.AppendFormat("&{0}={1}", prop.Name, val);
            }
            return sb.ToString().Substring(1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string PushUrlParams(string url, string name, string value)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}?{1}={2}", url.Split('?')[0], name, value);

            if (url.IndexOf('?') > -1)
            {
                var args = url.Split('?')[1].Split('&');

                foreach (string arg in args)
                {
                    var index = arg.IndexOf('=');
                    if (index == -1) continue;


                    var pName = arg.Substring(0, index);
                    if (string.Equals(pName, name, StringComparison.OrdinalIgnoreCase))
                        continue;  // 与需要加入的参数重复

                    var pValue = arg.Substring(index + 1);
                    sb.AppendFormat("&{0}={1}", pName, pValue);
                }
            }

            return sb.ToString();
        }

        #endregion


        #region 加密解密
        static DESHelper des = new DESHelper();

        /// <summary>
        /// 将普通文本转换成Base64编码的文本
        /// </summary>
        /// <param name="value">普通文本</param>
        /// <returns></returns>
        public static string StringToBase64String(String value)
        {
            byte[] binBuffer = (new UnicodeEncoding()).GetBytes(value);
            int base64ArraySize = (int)Math.Ceiling(binBuffer.Length / 3d) * 4;
            char[] charBuffer = new char[base64ArraySize];
            Convert.ToBase64CharArray(binBuffer, 0, binBuffer.Length, charBuffer, 0);
            string s = new string(charBuffer);
            return s;
        }
        /// <summary>
        /// 将Base64编码的文本转换成普通文本
        /// </summary>
        /// <param name="base64">Base64编码的文本</param>
        /// <returns></returns>
        public static string Base64StringToString(string base64)
        {
            char[] charBuffer = base64.ToCharArray();
            byte[] bytes = Convert.FromBase64CharArray(charBuffer, 0, charBuffer.Length);
            return (new UnicodeEncoding()).GetString(bytes);
        }

        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="str_sha1_in"></param>
        /// <returns></returns>
        public static string SHA1Encrypt(string str_sha1_in)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = UTF8Encoding.Default.GetBytes(str_sha1_in);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string str_sha1_out = BitConverter.ToString(bytes_sha1_out);
            str_sha1_out = str_sha1_out.Replace("-", "");
            return str_sha1_out;
        }

        #endregion

        #region Token处理

        static Hashtable token_cache = new Hashtable();

        /// <summary>
        /// Token加密 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ShopId"></param>
        /// <returns></returns>
        public static string EncryptToken(string token, long ShopId)
        {

            using (var db = new SqlHelper())
            {
                // 从数据库查询别名是否存在
                var aliasKey = db.ExecuteScalar<string>("select AliasKey from weixin_alias where token=@p0 and ShopId=@p1", token, ShopId);

                if (aliasKey == null)
                {
                    while (true)
                    {
                        // 生成别名（小写加数字）
                        aliasKey = CommonHelper.GetRandomString(8, true, false, true, false);

                        // 判断该别名是否存在！
                        if (!db.Exists("select aliasKey from weixin_alias where aliasKey=@p0", aliasKey))
                            break;   //
                    }
                    
                    var n = db.Insert("weixin_alias", new { aliasKey, token, ShopId });

                    LogHelper.debug(string.Format("shopid:{0},token:{1},aliaskey:{2}", ShopId, token, aliasKey));

                    if (n == 0) throw new Exception("alias key生成失败！");
                }

                return aliasKey;
            };

        }

        /// <summary>
        /// Token解密
        /// </summary>
        /// <param name="key">alias key </param>
        /// <returns></returns>
        public static TokenValues DecryptToken(string key)
        {

            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");

            // 从缓存中得到解密信息
            var values = token_cache[key] as TokenValues;

            if (values == null)
            {
                lock (token_cache.SyncRoot)
                {
                    using (var db = new SqlHelper())
                    {
                        // 从数据库查询别名是否存在
                        values = db.Single<TokenValues>("select Token,ShopId from weixin_alias where aliasKey=@p0", key);
                        if (values == null) throw new Exception("alias key is Invalid!");


                        // 保存到缓存 
                        token_cache[key] = values;
                    }
                }
            }

            return values;

        }
        #endregion

        /// <summary>
        /// 验证手机号码有效性
        /// </summary>
        /// <param name="mobile"></param>
        public static void CheckMobile(string mobile)
        {

            if (string.IsNullOrEmpty(mobile))
                throw new Exception("手机号码不能为空！");

            var regex = new System.Text.RegularExpressions.Regex("^[0-9]{11,11}$");
            if (!regex.IsMatch(mobile))
                throw new Exception("手机号码格式错误！");
        }

        /// <summary>
        /// 验证固定电话号码有效性
        /// </summary>
        /// <param name="telphone"></param>
        public static void CheckTel(string telphone)
        {

            if (string.IsNullOrEmpty(telphone))
                throw new Exception("电话号码不能为空！");
            string rg = @"^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$";
            var regex = new System.Text.RegularExpressions.Regex(rg);
            if (!regex.IsMatch(telphone))
                throw new Exception("电话号码格式错误！");
        }

        /// <summary>
        /// 验证邮箱地址
        /// </summary>
        /// <param name="email"></param>
        public static void CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("邮箱地址不能为空！");
            string rg = "^\\s*\\w+(?:\\.{0,1}[\\w-]+)*@[a-zA-Z0-9]+(?:[-.][a-zA-Z0-9]+)*\\.[a-zA-Z]+\\s*$";
            var regex = new System.Text.RegularExpressions.Regex(rg);
            if (!regex.IsMatch(email))
                throw new Exception("邮箱地址格式错误!");
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="httpUrl">远程地址</param>
        /// <param name="fileName">本地地址</param>
        public static void DownloadFile(string httpUrl, string fileName)
        {

            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httpUrl);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36";

                WebResponse response = request.GetResponse();
                Stream reader = response.GetResponseStream();
                FileStream writer = new FileStream(fileName, FileMode.Create, FileAccess.Write);

                byte[] buff = new byte[512];
                int c = 0; //实际读取的字节数
                while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                {
                    writer.Write(buff, 0, c);
                }
                writer.Close();
                writer.Dispose();

                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (Exception ex)
            {
                LogHelper.write(ex);
            }
        }

        /// <summary>
        /// 地区
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetRegionInfos(int regionId, SqlHelper db)
        {
            var pid = regionId;
            var rs = new Dictionary<int, string>();

            while (pid > 0)
            {
                var oo = db.Single("select pid,name from weixin_region where id=@p0", pid);
                if (oo == null) break;
                rs.Add(pid, oo.GetString("name"));

                pid = oo.GetInt32("pid");   //  move next;
            }

            return rs;
        }

        /// <summary>
        /// 得到缩略图的URL地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetThumbImageUrl(string url, int width, int height)
        {
            if (url == null) return "/Templates/forum/images/nophoto.jpg";
            if (url.StartsWith("http://")) return url;
            return string.Format("{0}.{1}x{2}._jpeg", url, width, height);

        }

    }


}
