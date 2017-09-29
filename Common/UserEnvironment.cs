using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;

namespace Mor.Common
{

    /// <summary>
    /// 用户环境 
    /// </summary>
    public class UserEnvironment
    {

        ///// <summary>
        ///// 会话ID（Session Id）
        ///// </summary>
        //public static String Token
        //{
        //    get { return Session.SessionId; }
        //}

        #region 系统信息

        /// <summary>
        /// 系统名称
        /// </summary>
        public static string ApplicationName { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public static string ApplicationVersion { get; set; }
        /// <summary>
        /// 版本信息
        /// </summary>
        public static string ApplicationCopyright { get; set; }
        /// <summary>
        /// 应用程序根目录
        /// </summary>
        public static string CurrentDirectory { get; set; }
        /// <summary>
        /// 应用程序主域名
        /// </summary>
        public static string CurrentServerBaseUrl { get; set; } 
        #endregion

        #region 商户

        ///// <summary>
        ///// 商户
        ///// </summary>
        //public static UserInfoModel UserInfo
        //{
        //    get { return SessionHelper.Get<UserInfoModel>("UserEnvironment.UserInfo"); }
        //    set { SessionHelper.Set("UserEnvironment.UserInfo", value); }
        //}

        /// <summary>
        /// 商户菜单
        /// </summary>
        public static Dictionary<string, List<MenuModel>> AdminMenus
        {
            get { return SessionHelper.Get<Dictionary<string, List<MenuModel>>>("UserEnvironment.AdminMenus"); }
            set { SessionHelper.Set("UserEnvironment.AdminMenus", value); }
        }

        #endregion

        #region 会员信息

        /// <summary>
        /// 会员信息
        /// </summary>
        public static UserInfoModel UserInfo
        {
            get { return CookieHelper.Get<UserInfoModel>("UE_U"); }
            set { CookieHelper.Set("UE_U", value); }
        }
        #endregion
         
          
    }
}
