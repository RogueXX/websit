using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Mor.Web
{
    /// <summary>
    /// 异常类型
    /// </summary>
    internal enum AuthExceptionType
    {
        /// <summary>
        /// 常规异常 
        /// </summary>
        General = 0,
        /// <summary>
        /// 商户授权（商户未登录） 
        /// </summary>
        MerchantAuthorize = 1000,
        /// <summary>
        /// 微信公众账号授权
        /// </summary>
        WeixinAccountAuthorize = 2000,
        /// <summary>
        /// 会员（或分销商）授权（会员未登录）
        /// </summary>
        MemberAuthorize = 3000,
        /// <summary>
        /// 菜单访问权
        /// </summary>
        MenuAccess = 4000,
        /// <summary>
        /// 分销店未开启
        /// </summary>
        FenxiaoUnopened = 5000,
        /// <summary>
        /// 分销店铺访问权
        /// <para>访问了别人的店铺管理界面</para>
        /// </summary>
        FenxiaoAccess = 5001,
        /// <summary>
        /// 洗车会员访问权
        /// <para>洗车会员未登录</para>
        /// </summary>
        CarMemberAuthorize = 6000,
        ///<summary>
        /// 洗车师傅访问权
        /// <para>洗车师傅未登陆</para>
        /// </summary>
        CarServerAuthorize = 6001,
        /// <summary>
        /// 未设置洗车城市区域
        /// </summary>
        CarNotRegion = 6002,
        /// <summary>
        /// 未购买资格权限
        /// </summary>
        NoSharePower = 7001
    }

}