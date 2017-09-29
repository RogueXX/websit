using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Mor.Common
{
    public class MD5Helper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string EncryptFile(string fileName)
        {
            FileStream file = new FileStream(fileName, FileMode.Open);
            string str = EncryptFile(file);
            file.Close();
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Encrypt(string source)
        {
            byte[] result = System.Text.UTF8Encoding.UTF8.GetBytes(source);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string EncryptFile(Stream stream)
        {

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(stream);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
