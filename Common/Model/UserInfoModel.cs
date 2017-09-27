using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mor.Common
{

    /// <summary>
    /// 当前已登录的用户信息
    /// </summary>
    [Serializable]
    public class UserInfoModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 等级ID
        /// </summary>
        public int GradeId { get; set; }
        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// MemberName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// WangWang
        /// </summary>
        public string WangWang { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// CellPhone
        /// </summary>
        public string CellPhone { get; set; }
        /// <summary>
        /// EmailAddress
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VipCardNumber { get; set; }
        /// <summary>
        /// 该会员对应的店铺
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// 该会员的头像
        /// </summary>
        public string HeaderPic { get; set; }
        /// <summary>
        /// 当前会员属于哪个商城
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 推荐人
        /// </summary>
        public long ReferralUserId { get; set; }
        /// <summary>
        /// 推荐人店铺
        /// </summary>
        public long ReferralShopId { get; set; }

        //public int UserCity { get; set; }
        //public int UserRegion { get; set; }

        ///// <summary>
        ///// 0--过期1--使用中 
        ///// </summary>
        //public int Status { get; set; }

        /// <summary>
        /// 过期时间 
        /// </summary>
        public DateTime OverdueTime { get; set; }
         
        /// <summary>
        /// 有效期天数(只读)
        /// </summary>
        public int OverdueDays
        {
            get
            {
                var days = (OverdueTime - DateTime.Now).Days+1;
                return days > 0 ? days : 0;
            }
        }


        private int shoppingDiscount;
        /// <summary>
        /// 购物折扣 
        /// </summary>
        public int ShoppingDiscount
        {
            get
            {
                if (shoppingDiscount == 0)
                {
                    return 100;
                }
                else
                    return shoppingDiscount;
            }
            set { shoppingDiscount = value; }
        }

        public bool HasSharePower { get; set; }

    }
}
