using System;
using Mor.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;


namespace Mor.Common
{
    public class ExportHelper
    {

        /// <summary>
        /// 生成CSV文件
        /// </summary>
        /// <param name="arr">数据</param>
        /// <param name="columns">列</param>
        /// <returns></returns>
        public static string CreateCsvFile(DbArray arr, Dictionary<string, string> columns)
        {

            var fileName = string.Format("{0}.{1}", CommonHelper.ConvertDateTimeInt(DateTime.Now), "csv");
            var dirPath = "/tmp/data/output/";

            // create dir;
            Directory.CreateDirectory(CommonHelper.GetFullPath(dirPath));

            // get file path;
            var filePath = CommonHelper.GetFullPath(dirPath + fileName);

            // 生成文件 
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {

                // write 
                using (StreamWriter sw = new StreamWriter(fs, System.Text.UTF8Encoding.UTF8))
                {
                    var aa = columns.Select(m => m.Value).ToArray();

                    // 标题
                    sw.WriteLine(string.Join(",", aa));

                    // 内容
                    foreach (var obj in arr)
                    {
                        var line = columns.Select(m =>string.IsNullOrEmpty(obj.GetString(m.Key))?"":obj.GetString(m.Key).Replace(',','，')).ToArray();
                        sw.WriteLine(string.Join(",", line));
                    }
                    sw.Close();
                }

            }

            return dirPath + fileName;

        }


    }
}
