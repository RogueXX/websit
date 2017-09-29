using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mor.Common
{
    /// <summary>
    /// 通用返回结果类
    /// </summary>
    public class ResultModel
    {
        #region properties

        /// <summary>
        /// 是否错误 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public object obj { get; set; }
        #endregion

        #region structs

        /// <summary>
        /// struct
        /// </summary> 
        public ResultModel()
        {
            this.status = 1;
        }
        /// <summary>
        /// struct
        /// </summary>
        /// <param name="error"></param>
        public ResultModel(string error)
        {
            this.msg = error;
            this.status = -1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        public ResultModel(int status, string msg)
        {
            this.status = status;
            this.msg = msg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public ResultModel(object obj)
        {
            this.status = 1;
            this.obj = obj;
        }
        #endregion
    }
}
