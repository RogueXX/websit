using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mor.Web
{
    /// <summary>
    ///  
    /// </summary>
    internal class AuthException : ApplicationException
    {

        /// <summary>
        /// 异常类型
        /// </summary>
        public AuthExceptionType Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public AuthException(AuthExceptionType type, string message)
            : base(message)
        {
            this.Type = type;
        }
    }
}